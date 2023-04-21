using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.Cadastros.EnderecoServ;
using Programax.Easy.Servico.Fiscal.CnaeServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Telas.Cadastros.Enderecos;
using Programax.Easy.View.Telas.Fiscal.Cnaes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Cadastros.Empresas
{
    public partial class FormCadastroEmpresa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Empresa _empresa;
        private Cnae _cnaeSelecionado;
        private bool _cadastroInicial;

        private ServicoEndereco _servicoEndereco;
        private ServicoCidade _servicoCidade;

        #endregion

        #region " PROPRIEDADES "

        public DialogResult Resultado { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroEmpresa()
        {
            InicializeForm(false);
        }

        public FormCadastroEmpresa(bool cadastroInicial)
        {
            InicializeForm(cadastroInicial);
        }

        private void InicializeForm(bool cadastroInicial)
        {
            InitializeComponent();

            _empresa = new Empresa();
            _cadastroInicial = cadastroInicial;

            Resultado = DialogResult.Cancel;

            this.NomeDaTela = "Cadastro da Empresa";

            _servicoEndereco = new ServicoEndereco();
            _servicoCidade = new ServicoCidade();

            PreenchaCboRegimeTributario();
            PreenchaCboEstados();
            PreenchaCboEstadosContador();

            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            _empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            CarregeEmpresa();

            if (Sessao.PessoaLogada.Id == 1)
            {
                chkAcqua.Visible = true;
            }
            else
            {
                chkAcqua.Visible = false;
            }

            _empresa = _empresa ?? new Empresa();

            this.ActiveControl = txtRazaoSocial;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Salve();
        }

        private void FormCadastroEmpresa_Load(object sender, EventArgs e)
        {

        }

        private void txtCepEndereco_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCepEndereco.Text.EstahVazio())
                {
                    PesquiseCepPelaTelaDePesquisa();
                }
            }
        }

        private void txtCepEndereco_Leave(object sender, EventArgs e)
        {
            PesquiseCep();
        }

        private void btnPesquisaEndereco_Click(object sender, EventArgs e)
        {
            PesquiseCepPelaTelaDePesquisa();
        }

        private void txtCepEnderecoContador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCepEnderecoContador.Text.EstahVazio())
                {
                    PesquiseCepContadorPelaTelaDePesquisa();
                }
            }
        }

        private void txtCepEnderecoContador_Leave(object sender, EventArgs e)
        {
            PesquiseCepContador();
        }

        private void btnPesquiseEnderecoContador_Click(object sender, EventArgs e)
        {
            PesquiseCepContadorPelaTelaDePesquisa();
        }

        private void btnAddImagem_Click(object sender, EventArgs e)
        {
            try
            {
                if (picFoto != null)
                {
                    TratamentoDeImagens.BuscaImagem(picFoto, "Imagens|*.jpg");
                    btnAddImagem.Enabled = true;
                }
                else
                    btnAddImagem.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void btnDelImagem_Click(object sender, EventArgs e)
        {
            try
            {
                if (picFoto != null)
                {
                    if (MessageBox.Show(this, "Tem certeza que deseja remover a foto?", "Atenção", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        btnAddImagem.Enabled = true;
                        btnDelImagem.Enabled = false;
                        picFoto.Image = Properties.Resources.Akil_03;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void txtInscricaoEstadual_Enter(object sender, EventArgs e)
        {
            if (txtInscricaoEstadual.Text == "ISENTO")
            {
                txtInscricaoEstadual.Text = string.Empty;
            }
        }

        private void txtInscricaoEstadual_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInscricaoEstadual.Text))
            {
                txtInscricaoEstadual.Text = "ISENTO";
            }
        }

        private void txtInscricaoEstadual_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void txtInscricaoMunicipal_Enter(object sender, EventArgs e)
        {
            if (txtInscricaoMunicipal.Text == "ISENTO")
            {
                txtInscricaoMunicipal.Text = string.Empty;
            }
        }

        private void txtInscricaoMunicipal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInscricaoMunicipal.Text))
            {
                txtInscricaoMunicipal.Text = "ISENTO";
            }
        }

        private void txtInscricaoMunicipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void txtCnpj_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!txtCnpj.Text.EstahVazio())
            {
                if (!ValidacoesGerais.CnpjEstahValido(txtCnpj.Text))
                {
                    e.Cancel = true;
                }
            }
        }

        private void txtCnpj_InvalidValue(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;

            e.ErrorText = "CNPJ inválido.";
        }

        private void txtCnpjContador_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!txtCnpjContador.Text.EstahVazio())
            {
                if (!ValidacoesGerais.CnpjEstahValido(txtCnpjContador.Text))
                {
                    e.Cancel = true;
                }
            }
        }

        private void txtCnpjContador_InvalidValue(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;

            e.ErrorText = "CNPJ inválido.";
        }

        private void txtCpfContador_InvalidValue(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;

            e.ErrorText = "CPF inválido.";
        }

        private void txtCpfContador_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!txtCpfContador.Text.EstahVazio())
            {
                if (!ValidacoesGerais.CpfEstahValido(txtCpfContador.Text))
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void txtTelefone_KeyUp(object sender, KeyEventArgs e)
        {
            MascaraTelefone(txtTelefone, e);
        }

        private void txtFax_KeyUp(object sender, KeyEventArgs e)
        {
            MascaraTelefone(txtFax, e);
        }

        private void txtTelefoneContador_KeyUp(object sender, KeyEventArgs e)
        {
            MascaraTelefone(txtTelefoneContador, e);
        }

        private void txtFaxContador_KeyUp(object sender, KeyEventArgs e)
        {
            MascaraTelefone(txtFaxContador, e);
        }

        private void txtCelularContador_KeyUp(object sender, KeyEventArgs e)
        {
            MascaraTelefone(txtCelularContador, e);
        }

        private void btnPesquisaCnae_Click(object sender, EventArgs e)
        {
            FormCnaePesquisa formCnaePesquisa = new FormCnaePesquisa();

            var cnae = formCnaePesquisa.PesquiseCnae();

            if (cnae != null)
            {
                _cnaeSelecionado = cnae;
                CarregueCnae(false);
            }
        }

        private void cboEstado_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCidades();

            cboCidade.EditValue = null;
        }

        private void cboEstadoEnderecoContador_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCidadesContador();

            cboCidade.EditValue = null;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " SALVAR EMPRESA "

        private void PreenchaEmpresa()
        {
            _empresa.Id = txtId.Text.ToInt();

            PreenchaDadosEmpresa();
            PreenchaDadosContador();
        }

        private void PreenchaDadosEmpresa()
        {
            int Checado = 0;
            _empresa.DadosEmpresa.Cnae = _cnaeSelecionado;
            _empresa.DadosEmpresa.Cnpj = txtCnpj.Text;
            _empresa.DadosEmpresa.CodigoRegimeTributario = (EnumCodigoRegimeTributario?)cboRegimeTributario.EditValue;
            _empresa.DadosEmpresa.DataCadastro = txtDataCadastro.Text.ToDateNullabel();
            _empresa.DadosEmpresa.Fax = txtFax.Text;
            _empresa.DadosEmpresa.WhatsApp = txtWhatsApp.Text;
            _empresa.DadosEmpresa.InscricaoEstadual = txtInscricaoEstadual.Text;
            _empresa.DadosEmpresa.InscricaoMunicipal = txtInscricaoMunicipal.Text;
            _empresa.DadosEmpresa.NomeFantasia = txtNomeFantasia.Text;
            _empresa.DadosEmpresa.RazaoSocial = txtRazaoSocial.Text;
            _empresa.DadosEmpresa.Telefone = txtTelefone.Text;
            
            if (chkAcqua.Checked == true)
            {
                Checado = 1;
            }
            else
            {
                Checado = 0;
            }
            _empresa.DadosEmpresa.Facebook = txtFacebook.Text;
            _empresa.DadosEmpresa.Twitter = txtTwitter.Text;
            _empresa.DadosEmpresa.Instagram = txtInstagram.Text;
            _empresa.DadosEmpresa.Config = Checado;
            _empresa.DadosEmpresa.AliqInd = txtaliqInd.Text.Replace(",",".");
            _empresa.DadosEmpresa.AliqCom = txtAliqCom.Text.Replace(",", ".");
            _empresa.DadosEmpresa.AliqServ = txtAliqServ.Text.Replace(",", ".");

            _empresa.DadosEmpresa.Foto = TratamentoDeImagens.ConvertImagemToByte(picFoto);

            PreenchaEnderecoEmpresa();
        }

        private void PreenchaEnderecoEmpresa()
        {
            EnderecoEmpresaComEmail endereco = new EnderecoEmpresaComEmail();

            endereco.CEP = txtCepEndereco.Text;
            endereco.Cidade = cboCidade.EditValue != null ? new Cidade { Id = cboCidade.EditValue.ToInt() } : null;
            endereco.Rua = txtRua.Text;
            endereco.Bairro = txtBairro.Text;

            endereco.Complemento = txtComplementoEndereco.Text;
            endereco.Numero = txtNumeroEndereco.Text;
            endereco.Email = txtEmail.Text;

            _empresa.DadosEmpresa.Endereco = endereco;
        }

        private void PreenchaDadosContador()
        {
            _empresa.DadosContador.Celular = txtCelularContador.Text;
            _empresa.DadosContador.Cnpj = txtCnpjContador.Text;
            _empresa.DadosContador.CpfContador = txtCpfContador.Text;
            _empresa.DadosContador.Crc = txtCrcContador.Text;
            _empresa.DadosContador.Escritorio = txtEscritorioContador.Text;
            _empresa.DadosContador.Fax = txtFaxContador.Text;
            _empresa.DadosContador.Nome = txtNomeContador.Text;
            _empresa.DadosContador.Telefone = txtTelefone.Text;

            PreenchaEnderecoContador();
        }

        private void PreenchaEnderecoContador()
        {
            EnderecoEmpresaComEmail endereco = new EnderecoEmpresaComEmail();

            endereco.CEP = txtCepEnderecoContador.Text;
            endereco.Cidade = cboCidadeEnderecoContador.EditValue != null ? new Cidade { Id = cboCidadeEnderecoContador.EditValue.ToInt() } : null;
            endereco.Rua = txtRuaEnderecoContador.Text;
            endereco.Bairro = txtBairroEnderecoContador.Text;

            endereco.Complemento = txtComplementoEnderecoContador.Text;
            endereco.Numero = txtNumeroEnderecoContador.Text;
            endereco.Email = txtEmailContador.Text;

            _empresa.DadosContador.Endereco = endereco;
        }

        private void Salve()
        {
            Action actionSalvar = () =>
            {
                PreenchaEmpresa();

                var empresa = _empresa;

                ServicoEmpresa servicoEmpresa = null;

                if (_cadastroInicial)
                {
                    servicoEmpresa = new ServicoEmpresa(false, true);
                }
                else
                {
                    servicoEmpresa = new ServicoEmpresa();
                }

                if (string.IsNullOrEmpty(txtId.Text))
                {
                    servicoEmpresa.Cadastre(empresa);
                }
                else
                {
                    servicoEmpresa.Atualize(empresa);
                }

                Resultado = DialogResult.OK;

                if (_cadastroInicial)
                {
                    this.Close();
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void txtCodigoCnae_Leave(object sender, EventArgs e)
        {
            if (!txtCodigoCnae.Text.EstahVazio())
            {
                ServicoCnae servicoCnae = new ServicoCnae();
                _cnaeSelecionado = servicoCnae.ConsultePeloCodigo(txtCodigoCnae.Text);

                CarregueCnae(true);
            }
            else
            {
                _cnaeSelecionado = null;

                CarregueCnae(false);
            }
        }

        #endregion

        #region " CARREGUE EMPRESA "

        private void CarregeEmpresa()
        {
            if (_empresa != null)
            {
                CarregueDadosDaEmpresa();
                CarregueDadosDoContador();
            }
        }

        private void CarregueDadosDoContador()
        {
            if (_empresa.DadosContador != null)
            {
                txtEscritorioContador.Text = _empresa.DadosContador.Escritorio;
                txtCnpjContador.Text = _empresa.DadosContador.Cnpj;
                txtNomeContador.Text = _empresa.DadosContador.Nome;
                txtCrcContador.Text = _empresa.DadosContador.Crc;
                txtCpfContador.Text = _empresa.DadosContador.CpfContador;

                txtTelefoneContador.Text = _empresa.DadosContador.Telefone;
                txtFaxContador.Text = _empresa.DadosContador.Fax;
                txtCelularContador.Text = _empresa.DadosContador.Celular;

                CarregueEnderecoContador();
            }
        }

        private void CarregueEnderecoContador()
        {
            if (_empresa.DadosContador.Endereco != null)
            {
                txtCepEnderecoContador.Text = _empresa.DadosContador.Endereco.CEP;
                txtBairroEnderecoContador.Text = _empresa.DadosContador.Endereco.Bairro;
                txtRuaEnderecoContador.Text = _empresa.DadosContador.Endereco.Rua;
                cboEstadoEnderecoContador.EditValue = _empresa.DadosContador.Endereco.Cidade != null ? _empresa.DadosContador.Endereco.Cidade.Estado.UF : null;
                cboCidadeEnderecoContador.EditValue = _empresa.DadosContador.Endereco.Cidade != null ? (int?)_empresa.DadosContador.Endereco.Cidade.Id : null;

                txtComplementoEnderecoContador.Text = _empresa.DadosContador.Endereco.Complemento;
                txtNumeroEnderecoContador.Text = _empresa.DadosContador.Endereco.Numero;
                txtEmailContador.Text = _empresa.DadosContador.Endereco.Email;
            }
        }

        private void CarregueDadosDaEmpresa()
        {
            
            txtId.Text = _empresa.Id.ToString();
            txtDataCadastro.Text = _empresa.DadosEmpresa.DataCadastro.Value.ToString("dd/MM/yyyy");
            txtRazaoSocial.Text = _empresa.DadosEmpresa.RazaoSocial;
            txtNomeFantasia.Text = _empresa.DadosEmpresa.NomeFantasia;
            txtTelefone.Text = _empresa.DadosEmpresa.Telefone;
            txtFax.Text = _empresa.DadosEmpresa.Fax;
            txtWhatsApp.Text = _empresa.DadosEmpresa.WhatsApp;

            txtCnpj.Text = _empresa.DadosEmpresa.Cnpj;
            txtInscricaoEstadual.Text = _empresa.DadosEmpresa.InscricaoEstadual;
            txtInscricaoMunicipal.Text = _empresa.DadosEmpresa.InscricaoMunicipal;

            txtFacebook.Text = _empresa.DadosEmpresa.Facebook;
            txtTwitter.Text = _empresa.DadosEmpresa.Twitter;
            txtInstagram.Text = _empresa.DadosEmpresa.Instagram;

            cboRegimeTributario.EditValue = _empresa.DadosEmpresa.CodigoRegimeTributario;

            _cnaeSelecionado = _empresa.DadosEmpresa.Cnae;

            switch (_empresa.DadosEmpresa.Config)
            {
                case 0:
                    chkAcqua.Checked = false;
                    break;
                case 1:
                    chkAcqua.Checked = true;
                    break;
                default:
                    chkAcqua.Checked = false;
                    break;
            }

            txtaliqInd.Text = _empresa.DadosEmpresa.AliqInd;
            txtAliqCom.Text = _empresa.DadosEmpresa.AliqCom;
            txtAliqServ.Text = _empresa.DadosEmpresa.AliqServ;


            CarregueCnae(false);

            if (_empresa.DadosEmpresa.Foto == null)
            {
                picFoto.Image = Properties.Resources.Akil_03;
            }
            else
            {
                //Andre
                //picFoto.Image = TratamentoDeImagens.ConvertByteToImagem(_empresa.DadosEmpresa.Foto).Image;
            }

            CarregueEnderecoEmpresa();
        }

        private void CarregueEnderecoEmpresa()
        {
            txtCepEndereco.Text = _empresa.DadosEmpresa.Endereco.CEP;
            txtBairro.Text = _empresa.DadosEmpresa.Endereco.Bairro;
            txtRua.Text = _empresa.DadosEmpresa.Endereco.Rua;
            cboEstado.EditValue = _empresa.DadosEmpresa.Endereco.Cidade.Estado.UF;
            cboCidade.EditValue = _empresa.DadosEmpresa.Endereco.Cidade.Id;

            txtComplementoEndereco.Text = _empresa.DadosEmpresa.Endereco.Complemento;
            txtNumeroEndereco.Text = _empresa.DadosEmpresa.Endereco.Numero;
            txtEmail.Text = _empresa.DadosEmpresa.Endereco.Email;
        }

        private void CarregueCnae(bool mostrarMensagemDeCnaeNaoEncontrado)
        {
            if (_cnaeSelecionado != null)
            {
                txtCodigoCnae.Text = _cnaeSelecionado.Codigo;
                txtDescricaoCnae.Text = _cnaeSelecionado.Descricao;
            }
            else
            {
                txtDescricaoCnae.Text = string.Empty;
                txtCodigoCnae.Text = string.Empty;

                if (mostrarMensagemDeCnaeNaoEncontrado)
                {
                    MessageBox.Show("CNAE informado não foi encontrado.");

                    txtCodigoCnae.Focus();
                }
            }
        }

        #endregion

        #region " PREENCHA CBOS "

        private void PreenchaCboRegimeTributario()
        {
            var listaDeValoresEnumerador = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCodigoRegimeTributario>();

            cboRegimeTributario.Properties.DataSource = listaDeValoresEnumerador;
            cboRegimeTributario.Properties.ValueMember = "Valor";
            cboRegimeTributario.Properties.DisplayMember = "Descricao";
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

        private void PreenchaCboEstadosContador()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var listaDeEstados = servicoEstado.ConsulteListaEstados();

            listaDeEstados.Insert(0, null);

            cboCidadeEnderecoContador.Properties.DataSource = listaDeEstados;
            cboCidadeEnderecoContador.Properties.DisplayMember = "Nome";
            cboCidadeEnderecoContador.Properties.ValueMember = "UF";
        }

        private void PreenchaCboCidadesContador()
        {
            string uf = cboEstadoEnderecoContador.EditValue == null ? string.Empty : cboEstadoEnderecoContador.EditValue.ToString();

            var listaDeCidades = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(uf);

            listaDeCidades.Insert(0, null);

            cboCidadeEnderecoContador.Properties.DataSource = listaDeCidades;
            cboCidadeEnderecoContador.Properties.DisplayMember = "Descricao";
            cboCidadeEnderecoContador.Properties.ValueMember = "Id";
        }

        #endregion

        #region " ENDEREÇO EMPRESA "

        private void PesquiseCep()
        {
            var endereco = _servicoEndereco.Consulte(txtCepEndereco.Text);

            PreenchaDadosEnderecoCep(endereco);
        }

        private void PesquiseCepPelaTelaDePesquisa()
        {
            FormEnderecoPesquisa formEnderecoPesquisa = new FormEnderecoPesquisa();

            var endereco = formEnderecoPesquisa.PesquiseEndereco();

            if (endereco != null)
            {
                PreenchaDadosEnderecoCep(endereco);
            }
        }

        private void PreenchaDadosEnderecoCep(Endereco endereco)
        {
            if (endereco != null)
            {
                txtCepEndereco.Text = endereco.CEP;
                cboEstado.EditValue = endereco.Cidade.Estado.UF;
                cboCidade.EditValue = endereco.Cidade.Id;
                txtBairro.Text = endereco.Bairro;
                txtRua.Text = endereco.Rua;
            }
        }

        #endregion

        #region " ENDEREÇO CONTADOR "

        private void PesquiseCepContador()
        {
            var endereco = _servicoEndereco.Consulte(txtCepEnderecoContador.Text);

            PreenchaDadosEnderecoContadorCep(endereco);
        }

        private void PesquiseCepContadorPelaTelaDePesquisa()
        {
            FormEnderecoPesquisa formEnderecoPesquisa = new FormEnderecoPesquisa();

            var endereco = formEnderecoPesquisa.PesquiseEndereco();

            if (endereco != null)
            {
                PreenchaDadosEnderecoContadorCep(endereco);
            }
        }

        private void PreenchaDadosEnderecoContadorCep(Endereco endereco)
        {
            if (endereco != null)
            {
                txtCepEnderecoContador.Text = endereco.CEP;
                cboEstadoEnderecoContador.EditValue = endereco.Cidade.Estado.UF;
                cboCidadeEnderecoContador.EditValue = endereco.Cidade.Id;
                txtBairroEnderecoContador.Text = endereco.Bairro;
                txtRuaEnderecoContador.Text = endereco.Rua;
            }
        }

        #endregion

        #region " MÁSCARA TELEFONE "

        private void MascaraTelefone(TextEdit textBox, KeyEventArgs e)
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

            string numero = textBox.Text.SoNumeros();
            int posicaoCursor = textBox.SelectionStart;

            if (numero.Length == 11)
            {
                numero = numero.Insert(7, "-");

                if (podeAlterarAPosicaoDoCursor && posicaoCursor >= 4)
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

            if (numero.Length >= 2)
            {
                numero = numero.Insert(2, " ");
                numero = numero.Insert(2, ")");

                if (podeAlterarAPosicaoDoCursor)
                {
                    posicaoCursor += 2;
                }
            }

            if (numero.Length >= 1)
            {
                numero = numero.Insert(0, "(");

                if (podeAlterarAPosicaoDoCursor)
                {
                    posicaoCursor++;
                }
            }

            textBox.Text = numero;
            textBox.SelectionStart = posicaoCursor;
        }

        #endregion

        #endregion
    }
}


