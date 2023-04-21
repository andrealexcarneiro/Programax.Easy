using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.Cadastros.EnderecoServ;
using Programax.Easy.Servico.Cadastros.OrigemClienteServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Enderecos;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio;

namespace Programax.Easy.View.Telas.Vendas.VendaRapida
{
    public partial class FormCadastroClienteVendaRapida : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoaCadastrada;

        private int _idCliente;

        private string _cpfCnpjAntigo;

        private bool _pdv;

        private ServicoCidade _servicoCidade;

        private string _cepAtual;
        private string strEmpresa;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroClienteVendaRapida()
        {
            InitializeComponent();

            _cepAtual = string.Empty;

            AltereOTipoDaPessoa();

            PreenchaCboOrigemCliente();
            PreenchaCboSexo();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            PreenchaCboEstados();

            _servicoCidade = new ServicoCidade();

            this.ActiveControl = txtCpfCnpj;
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();

            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            if (empresa.DadosEmpresa.NomeFantasia.Length < 8)
            {
                strEmpresa = empresa.DadosEmpresa.NomeFantasia.Substring(0, empresa.DadosEmpresa.NomeFantasia.Length);
            }
            else
            {
                strEmpresa = empresa.DadosEmpresa.NomeFantasia.Substring(0, 8);
            }
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void txtCepEndereco_Leave(object sender, EventArgs e)
        {
            if (_cepAtual == null) return;

            if (_cepAtual.Equals(txtCepEndereco.Text))
            {
                return;
            }

            _cepAtual = txtCepEndereco.Text;

            PesquiseCep();
        }

        private void btnPesquisaEndereco_Click(object sender, EventArgs e)
        {
            PesquiseCepPelaTelaDePesquisa();
        }

        private void rdbCnpj_CheckedChanged(object sender, EventArgs e)
        {
            AltereOTipoDaPessoa();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void txtTelefoneCelular_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ApliqueMascaraTelefone(txtTelefoneCelular, e);
        }

        private void txtTelefoneFixo_KeyUp(object sender, KeyEventArgs e)
        {
            ApliqueMascaraTelefone(txtTelefoneFixo, e);
        }

        private void txtCpfCnpj_Leave(object sender, EventArgs e)
        {
            FormPessoasComMesmoCpfCnpj formPessoasComMesmoCpfCnpj = new FormPessoasComMesmoCpfCnpj();

            if (!string.IsNullOrEmpty(txtCpfCnpj.Text) && _cpfCnpjAntigo != txtCpfCnpj.Text)
            {
                var existePessoasComMesmoCpfOuCnpj = formPessoasComMesmoCpfCnpj.ExistePessoasComMesmoCpfOuCnpj(txtCpfCnpj.Text, _idCliente);

                if (existePessoasComMesmoCpfOuCnpj)
                {
                    Pessoa pessoa = null;

                    ServicoParametros servicoParametros = new ServicoParametros();
                    var parametros = servicoParametros.ConsulteParametros();

                    if (parametros.ParametrosCadastros.PermiteCadastroParceiroComMesmoCpfCnpj)
                    {
                        pessoa = formPessoasComMesmoCpfCnpj.ListePessoasComMesmoCpfCnpj(txtCpfCnpj.Text);
                    }
                    else
                    {
                        ServicoPessoa servicoPessoa = new ServicoPessoa();
                        pessoa = servicoPessoa.ConsultePessoaPeloCnpjOuCpf(txtCpfCnpj.Text);
                    }
                    
                    if (pessoa != null)
                    {
                        if (pessoa.DadosGerais.Status == "I")
                        {
                            if (MessageBox.Show("O cliente selecionado está INATIVO. Deseja continuar mesmo assim?", "Deseja Continuar?", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }
                        }

                        _idCliente = pessoa.Id;

                        btnGravar.Text = " Atualizar Cliente (F3)";

                        PreenchaCamposCliente(pessoa);
                    }
                }
            }
        }

        private void FormCadastroClienteVendaRapida_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F3)
            {
                Selecione();
            }
        }

        private void cboEstado_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCidades();

            cboCidade.EditValue = null;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public Pessoa CadastreClienteModal(Pessoa cliente)
        {
            return AbraTelaModal(cliente, false);
        }

        public Pessoa CadastreClienteModalPdv(Pessoa cliente)
        {
            return AbraTelaModal(cliente, true);
        }

        private Pessoa AbraTelaModal(Pessoa cliente, bool pdv)
        {
            _pdv = pdv;

            if (cliente != null)
            {
                _idCliente = cliente.Id;

                btnGravar.Text = " Atualizar Cliente";

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                cliente = servicoPessoa.Consulte(cliente.Id);

                PreenchaCamposCliente(cliente);
            }
            else
            {
                ServicoParametros servicoParametros = new ServicoParametros();
                var parametros = servicoParametros.ConsulteParametros();

                if (parametros.ParametrosVenda.AproveitarEnderecoEmpresaParaCadastroRapidoCliente)
                {
                    //ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
                    //var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

                    //PreenchaDadosEnderecoCep(null);

                    //txtCepEndereco.Text = empresa.DadosEmpresa.Endereco.CEP;
                    //cboEstado.EditValue = empresa.DadosEmpresa.Endereco.Cidade.Estado.UF;
                    //cboCidade.EditValue = empresa.DadosEmpresa.Endereco.Cidade.Id;

                    //txtBairro.Text = empresa.DadosEmpresa.Endereco.Bairro;
                    //txtRua.Text = empresa.DadosEmpresa.Endereco.Rua;

                    //txtComplementoEndereco.Text = empresa.DadosEmpresa.Endereco.Complemento;
                    //txtNumeroEndereco.Text = empresa.DadosEmpresa.Endereco.Numero;
                }

                if (pdv)
                {
                    txtNomeFantasia.Text = "(CLIENTE DO PDV - INSIRA SEU NOME)";

                    txtRazaoSocial.Text = "(CLIENTE DO PDV - INSIRA SEU NOME)";
                }
            }

            this.AbrirTelaModal(pdv);

            return _pessoaCadastrada;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboOrigemCliente()
        {
            ServicoOrigemCliente servicoOrigemCliente = new ServicoOrigemCliente();

            var listaOrigensClientes = servicoOrigemCliente.ConsulteListaAtiva();

            listaOrigensClientes.Insert(0, null);

            cboOrigemCliente.Properties.DataSource = listaOrigensClientes;
            cboOrigemCliente.Properties.ValueMember = "Id";
            cboOrigemCliente.Properties.DisplayMember = "Descricao";

            if (string.IsNullOrEmpty(cboOrigemCliente.Text))
            {
                cboOrigemCliente.EditValue = null;
            }
        }

        private void PreenchaCboSexo()
        {
            var listaDeValoresEnumerador = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumSexo>();
            listaDeValoresEnumerador.Insert(0, null);

            cboSexo.Properties.DataSource = listaDeValoresEnumerador;
            cboSexo.Properties.ValueMember = "Valor";
            cboSexo.Properties.DisplayMember = "Descricao";
        }

        private void PreenchaCboEstados()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var listaDeEstados = servicoEstado.ConsulteListaEstados();

            listaDeEstados.Insert(0, null);

            cboEstado.Properties.DataSource = listaDeEstados;
            cboEstado.Properties.DisplayMember = "Nome";
            cboEstado.Properties.ValueMember = "UF";
        }

        private void PreenchaCboCidades()
        {
            string uf = cboEstado.EditValue == null ? string.Empty : cboEstado.EditValue.ToString();

            var listaDeCidades = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(uf);

            listaDeCidades.Insert(0, null);

            cboCidade.Properties.DataSource = listaDeCidades;
            cboCidade.Properties.DisplayMember = "Descricao";
            cboCidade.Properties.ValueMember = "Id";
        }

        private void PesquiseCep()
        {
            ServicoEndereco servicoEndereco = new ServicoEndereco();

            var endereco = servicoEndereco.ConsulteAtivo(txtCepEndereco.Text);

            PreenchaDadosEnderecoCep(endereco);
        }

        private void PesquiseCepPelaTelaDePesquisa()
        {
            FormEnderecoPesquisa formEnderecoPesquisa = new FormEnderecoPesquisa();

            var endereco = formEnderecoPesquisa.PesquiseEnderecoAtivo();

            if (endereco != null)
            {
                PreenchaDadosEnderecoCep(endereco);
            }
        }

        private void PreenchaDadosEnderecoCep(Endereco endereco)
        {
            if (endereco != null)
            {
                _cepAtual = endereco.CEP;

                txtCepEndereco.Text = endereco.CEP;
                cboEstado.EditValue = endereco.Cidade.Estado.UF;
                cboCidade.EditValue = endereco.Cidade.Id;
                txtBairro.Text = endereco.Bairro;
                txtRua.Text = endereco.Rua;
            }
        }

        private void AltereOTipoDaPessoa()
        {
            if (rdbCpf.Checked)
            {
                FacaAlteracoesParaTipoPessoaFisica();
            }
            else
            {
                FacaAlteracoesParaTipoPessoaJuridica();
            }
        }

        private void FacaAlteracoesParaTipoPessoaFisica()
        {
            txtCpfCnpj.Text = string.Empty;
            txtCpfCnpj.Properties.Mask.EditMask = "000.000.000-00";

            txtRazaoSocial.Properties.ReadOnly = true;
            txtRazaoSocial.TabStop = false;
        }

        private void FacaAlteracoesParaTipoPessoaJuridica()
        {
            txtCpfCnpj.Text = string.Empty;
            txtCpfCnpj.Properties.Mask.EditMask = "00.000.000/0000-00";

            txtRazaoSocial.Properties.ReadOnly = false;
            txtRazaoSocial.TabStop = true;
        }

        private Pessoa RetorneClienteEmEdicao()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var pessoa = servicoPessoa.Consulte(_idCliente);

            pessoa = pessoa ?? new Pessoa();

            pessoa.DadosGerais.CpfCnpj = txtCpfCnpj.Text;
            pessoa.DadosGerais.DataCadastro = txtDataCadastro.Text.ToDate();
            pessoa.DadosGerais.EhCliente = true;
            pessoa.DadosGerais.NomeFantasia = txtNomeFantasia.Text;
            pessoa.DadosGerais.Razao = txtRazaoSocial.Text;
            pessoa.DadosGerais.Status = "A";
            pessoa.DadosGerais.TipoPessoa = rdbCpf.Checked ? EnumTipoPessoa.PESSOAFISICA : EnumTipoPessoa.PESSOAJURIDICA;
            pessoa.DadosGerais.TipoCliente = (EnumTipoCliente)this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            pessoa.EmpresaPessoa = pessoa.EmpresaPessoa ?? new EmpresaPessoa();
                
            pessoa.EmpresaPessoa.InscricaoEstadual = txtInscricaoEstadual.Text;

            pessoa.EmpresaPessoa.TipoInscricaoICMS = txtInscricaoEstadual.Text == string.Empty ? 
                                EnumTipoInscricaoICMS.NAOCONTRIBUINTEICMS : EnumTipoInscricaoICMS.CONTRIBUINTEICMS;

            pessoa.EmpresaPessoa.InscricaoMunicipal = txtInscricaoMunicipal.Text;
            pessoa.EmpresaPessoa.EmailCobranca = txtEmail.Text;
            pessoa.EmpresaPessoa.EmailPrincipal = txtEmail.Text;

            pessoa.Atendimento = pessoa.Atendimento ?? new Atendimento();

            pessoa.Atendimento.OrigemCliente = cboOrigemCliente.EditValue != null ? new OrigemCliente { Id = cboOrigemCliente.EditValue.ToInt() } : null;

            pessoa.DadosPessoais = pessoa.DadosPessoais ?? new DadosPessoais();

            pessoa.DadosPessoais.DataDeNascimento = txtDataDeNascimento.Text.ToDateNullabel();
            pessoa.DadosPessoais.Hobby = txtHobby.Text;
            pessoa.DadosPessoais.Sexo = (EnumSexo?)cboSexo.EditValue;

            var enderecoPrincipal = pessoa.ListaDeEnderecos.FirstOrDefault();
            pessoa.ListaDeEnderecos.Remove(enderecoPrincipal);

            EnderecoPessoa enderecoPessoa = new EnderecoPessoa();
            enderecoPessoa.Complemento = txtComplementoEndereco.Text;
            enderecoPessoa.CEP = txtCepEndereco.Text;
            enderecoPessoa.Bairro = txtBairro.Text;
            enderecoPessoa.Rua = txtRua.Text;
            enderecoPessoa.Cidade = cboCidade.EditValue != null ? new Cidade { Id = cboCidade.EditValue.ToInt() } : null;
            enderecoPessoa.Numero = txtNumeroEndereco.Text;
            enderecoPessoa.TipoEndereco = EnumTipoEndereco.PRINCIPAL;

            pessoa.ListaDeEnderecos.Add(enderecoPessoa);

            var celular = pessoa.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.CELULAR);
            var residencial = pessoa.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.RESIDENCIAL);

            pessoa.ListaDeTelefones.Remove(celular);
            pessoa.ListaDeTelefones.Remove(residencial);

            if (!string.IsNullOrWhiteSpace(txtTelefoneFixo.Text))
            {
                Telefone telefone = new Telefone();

                telefone.TipoTelefone = EnumTipoTelefone.RESIDENCIAL;
                telefone.Ddd = txtTelefoneFixo.Text.Substring(0, 4).Replace("(", "").Replace(")", "").ToInt();
                telefone.Numero = txtTelefoneFixo.Text.Substring(4);

                pessoa.ListaDeTelefones.Add(telefone);
            }

            if (!string.IsNullOrWhiteSpace(txtTelefoneCelular.Text))
            {
                Telefone telefone = new Telefone();

                telefone.TipoTelefone = EnumTipoTelefone.CELULAR;
                telefone.Ddd = txtTelefoneCelular.Text.Substring(0, 4).Replace("(", "").Replace(")", "").ToInt();
                telefone.Numero = txtTelefoneCelular.Text.Substring(4);

                pessoa.ListaDeTelefones.Add(telefone);
            }

            return pessoa;
        }

        private void PreenchaCamposCliente(Pessoa cliente)
        {
            txtId.Text = cliente.Id.ToString();

            _cpfCnpjAntigo = cliente.DadosGerais.CpfCnpj;

            if (cliente.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAFISICA)
            {
                rdbCpf.Checked = true;
                txtCpfCnpj.Text.RemoverCaracteresDeMascara();
            }
            else
            {
                rdbCnpj.Checked = true;
            }

            rdbCnpj.Checked = cliente.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAJURIDICA;
            txtCpfCnpj.Text = cliente.DadosGerais.CpfCnpj;
            txtRazaoSocial.Text = cliente.DadosGerais.Razao;
            txtNomeFantasia.Text = cliente.DadosGerais.NomeFantasia;

            txtInscricaoEstadual.Text = cliente.EmpresaPessoa != null? cliente.EmpresaPessoa.InscricaoEstadual:null;
            txtInscricaoMunicipal.Text = cliente.EmpresaPessoa != null? cliente.EmpresaPessoa.InscricaoMunicipal:null;

            txtDataCadastro.Text = cliente.DadosGerais.DataCadastro.ToString() != "{01/01/0001 00:00:00}"? cliente.DadosGerais.DataCadastro.ToString("dd/MM/yyyy"):string.Empty;
            txtEmail.Text = cliente.EmpresaPessoa != null? cliente.EmpresaPessoa.EmailPrincipal:null;

            cboOrigemCliente.EditValue = cliente.Atendimento!=null? cliente.Atendimento.OrigemCliente != null ? (int?)cliente.Atendimento.OrigemCliente.Id : null:null;

            this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Tag.ToInt() == (int)cliente.DadosGerais.TipoCliente).Checked = true;

            txtDataDeNascimento.DateTime = cliente.DadosPessoais != null? cliente.DadosPessoais.DataDeNascimento.GetValueOrDefault(): default(DateTime);

            if (cliente.DadosPessoais != null)
            if (cliente.DadosPessoais.DataDeNascimento == null)
            {
                txtDataDeNascimento.Text = string.Empty;
            }

            cboSexo.EditValue = cliente.DadosPessoais !=null? cliente.DadosPessoais.Sexo: null;

            txtHobby.Text = cliente.DadosPessoais != null ? cliente.DadosPessoais.Hobby:null;

            var celular = cliente.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.CELULAR);
            var residencial = cliente.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.RESIDENCIAL);

            if (celular != null)
            {
                txtTelefoneCelular.Text = "(" + celular.Ddd.Value.ToString("00") + ")" + celular.Numero;
            }

            if (residencial != null)
            {
                txtTelefoneFixo.Text = "(" + residencial.Ddd.Value.ToString("00") + ")" + residencial.Numero;
            }

            var endereco = cliente.ListaDeEnderecos.FirstOrDefault(end => end.TipoEndereco == EnumTipoEndereco.PRINCIPAL);

            endereco = endereco != null ? endereco : cliente.ListaDeEnderecos.FirstOrDefault();

            if (endereco != null)
            {
                _cepAtual = endereco.CEP;

                txtRua.Text = endereco.Rua;
                txtBairro.Text = endereco.Bairro;
                txtCepEndereco.Text = endereco.CEP;

                cboEstado.EditValue = endereco.Cidade != null? endereco.Cidade.Estado.UF:null;
                cboCidade.EditValue = endereco.Cidade != null? endereco.Cidade.Id: 0;

                txtNumeroEndereco.Text = endereco.Numero;
                txtComplementoEndereco.Text = endereco.Complemento;
            }
            else
            {   
                txtRua.Text = string.Empty;
                txtBairro.Text = string.Empty;
                txtCepEndereco.Text = string.Empty;

                cboEstado.EditValue = null;
                cboCidade.EditValue = null;

                txtNumeroEndereco.Text = string.Empty;
                txtComplementoEndereco.Text = string.Empty;
            }
            if (strEmpresa != "SHOPPING")
            {
                if (Sessao.GrupoAcesso.Id == 4 || Sessao.GrupoAcesso.Id == 5)
                {
                    txtCpfCnpj.Enabled = false;
                    txtNomeFantasia.Enabled = false;
                    txtRazaoSocial.Enabled = false;
                }
            }
        }

        private void ApliqueMascaraTelefone(TextEdit campoTexto, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
            {
                return;
            }

            bool podeAlterarAPosicaoDoCursor = true;

            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                podeAlterarAPosicaoDoCursor = false;
            }

            string numero = campoTexto.Text;
            int posicaoCursor = campoTexto.SelectionStart;

            numero = numero.RemoverCaracteresDeMascara();

            if (numero.Length == 11)
            {
                numero = numero.Insert(7, "-");

                if (podeAlterarAPosicaoDoCursor && posicaoCursor >= 6)
                {
                    posicaoCursor++;
                }
            }
            else if (numero.Length > 6)
            {
                numero = numero.Insert(6, "-");

                if (podeAlterarAPosicaoDoCursor && posicaoCursor >= 7)
                {
                    posicaoCursor++;
                }
            }

            if (numero.Length > 0)
            {
                numero = numero.Insert(0, "(");

                if (podeAlterarAPosicaoDoCursor)
                {
                    posicaoCursor++;
                }

                if (numero.Length > 2)
                {
                    numero = numero.Insert(3, ")");

                    if (podeAlterarAPosicaoDoCursor)
                    {
                        posicaoCursor++;
                    }
                }
            }

            campoTexto.Text = numero;
            campoTexto.SelectionStart = posicaoCursor;
        }

        private void Selecione()
        {
            Action actionSalvar = () =>
            {
                if (txtCpfCnpj.IsEditorActive)
                {
                    txtNomeFantasia.Focus();
                }

                Pessoa cliente = RetorneClienteEmEdicao();

                ServicoClienteRapido servicoClienteRapido = new ServicoClienteRapido();

                if (cliente.Id == 0)
                {
                    servicoClienteRapido.Cadastre(cliente);
                }
                else
                {
                    servicoClienteRapido.Atualize(cliente);
                }

                _pessoaCadastrada = cliente;

                this.Close();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        #endregion

        private void txtNomeFantasia_EditValueChanged(object sender, EventArgs e)
        {
            if (rdbCpf.Checked)
            {
                txtRazaoSocial.Text = txtNomeFantasia.Text;
            }
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();            
            var pessoa = formPessoaPesquisa.PesquisePessoaAtiva();

            if (pessoa != null)
            {
                _idCliente = pessoa.Id;
                btnGravar.Text = " Atualizar Cliente";
                PreenchaCamposCliente(new Pessoa());
                PreenchaCamposCliente(pessoa);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var pessoa = servicoPessoa.ConsultePessoaAtiva(txtId.Text.ToInt());

                if (pessoa != null)
                {
                    _idCliente = pessoa.Id;
                    btnGravar.Text = " Atualizar Cliente";
                    PreenchaCamposCliente(new Pessoa());
                    PreenchaCamposCliente(pessoa);
                }
                else
                {
                    MessageBox.Show("Pessoa não encontrada.", "Pessoa não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtId.Text = string.Empty;
                }
            }
        }

        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
