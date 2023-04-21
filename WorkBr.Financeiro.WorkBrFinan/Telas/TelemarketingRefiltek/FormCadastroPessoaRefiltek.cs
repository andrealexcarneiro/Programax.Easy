using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.EnderecoServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Cidades;
using Programax.Easy.View.Telas.Cadastros.Enderecos;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.Servico.Cadastros.OrigemClienteServ;
using Programax.Easy.Servico.Cadastros.RamoAtividadeServ;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Cadastros.OrigensClientes;
using Programax.Easy.View.Telas.Produtos.TabelaDePreco;
using Programax.Easy.View.Telas.Financeiro.FormasPagamento;
using Programax.Easy.View.Telas.Financeiro.CondicoesPagamento;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Cadastros.PaisServ;
using Programax.Easy.Negocio.Cadastros.PaisObj.ObjetoDeNegocio;
using System.Drawing;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Cadastros.Pessoas
{
    public partial class FormCadastroPessoaRefiltek : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Telefone> _listaDeTelefones;
        private List<EnderecoPessoa> _listaDeEnderecos;
        private Telefone _telefoneEmEdicao;
        private EnderecoPessoa _enderecoPessoaEmEdicao;

        private ServicoCidade _servicoCidade;
        private ServicoPessoa _servicoPessoa;
        private ServicoEndereco _servicoEndereco;

        private Pessoa _pessoaEmEdicao;

        private Cidade _cidadeNaturalidade;

        private string _cepAtual;

        private List<Cidade> _listaCidadesCbo;
        private Parametros _parametros;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroPessoaRefiltek(int CodCliente = 0) //CodCliente - Acrescentar
        {
            InitializeComponent();

            this.NomeDaTela = "Cadastro de Parceiros";

            _listaDeTelefones = new List<Telefone>();
            _listaDeEnderecos = new List<EnderecoPessoa>();

            tbcPessoa.TabPages.Remove(tbpFuncionario);

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            PreenchaOStatus();
            PreenchaLimiteCreditoPadrao();
            PreenchaOsTipoDePessoa();
            PreenchaCboOrigemCliente();
            PreenchaCboRamoAtividade();
            PreenchaCboPaises();
            PreenchaCboEstados();

            TrateUsuarioContemPermissaoAtalhos();

            if (CodCliente != 0) //If para Acrescentar
            {
                txtId.Text = CodCliente.ToString();
            }

            this.ActiveControl = txtId;
        }



        #endregion

        #region " EVENTOS CONTROLES "

        #region " LOAD "

        private void FormCadastroPessoa_Load(object sender, EventArgs e)
        {
            _servicoCidade = new ServicoCidade();
            _servicoPessoa = new ServicoPessoa();
            _servicoEndereco = new ServicoEndereco();

            PreenchaTiposTelefone();
            PreenchaOsBancos();
            PreenchaAsTabelasDePreco();
            PreenchaAsCondicoesDePagamento();
            PreenchaCboSexo();
            PreenchaCboEstadoCivil();
            PreenchaCboGrauDeInstrucao();
            PreenchaCboTipoEndereco();
            PreenchaCboFormaDePagamento();
            PreenchaCboResidenciaPropria();

            ExibirOuEsconderRadioConsumidorFinalOuRevenda(false);

            cboOrigemCliente.EditValue = null;
            cboParceiro.EditValue = null;
        }

        #endregion

        #region " TELEFONES "

        private void btnInserirTelefone_Click(object sender, EventArgs e)
        {
            AdicionarOuAlterarTelefoneNoGrid();
        }

        private void btnExcluirTelefone_Click(object sender, EventArgs e)
        {
            ExcluaTelefone();
        }

        private void btnCancelarTelefone_Click(object sender, EventArgs e)
        {
            LimpeCamposDoTelefone();
        }

        private void gcTelefones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditeTelefone();
            }
        }

        private void gcTelefones_DoubleClick(object sender, EventArgs e)
        {
            EditeTelefone();
        }

        private void txtObservacoesTelefone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AdicionarOuAlterarTelefoneNoGrid();
            }
        }

        private void txtNumeroTelefone_Properties_KeyUp(object sender, KeyEventArgs e)
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

            string numero = txtNumeroTelefone.Text;
            int posicaoCursor = txtNumeroTelefone.SelectionStart;

            numero = numero.Replace("-", "");

            if (numero.Length == 9)
            {
                numero = numero.Insert(5, "-");

                if (podeAlterarAPosicaoDoCursor && posicaoCursor >= 4)
                {
                    posicaoCursor++;
                }
            }
            else if (numero.Length > 4)
            {
                numero = numero.Insert(4, "-");

                if (podeAlterarAPosicaoDoCursor && posicaoCursor >= 5)
                {
                    posicaoCursor++;
                }
            }

            txtNumeroTelefone.Text = numero;
            txtNumeroTelefone.SelectionStart = posicaoCursor;
        }

        #endregion

        #region " DADOS GERAIS "

       
        private void cboTipoPessoa_EditValueChanged(object sender, EventArgs e)
        {
            AltereOTipoDaPessoa();
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
                        picFoto.Image = Properties.Resources.user_img;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void txtCpfCnpj_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCpfCnpj.Text))
            {
                txtCpfCnpj.SelectionStart = 0;
            }
        }

        private void txtCpfCnpj_InvalidValue(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;

            if ((EnumTipoPessoa)cboTipoPessoa.EditValue == EnumTipoPessoa.PESSOAFISICA)
            {
                e.ErrorText = "CPF inválido.";
            }
            else
            {
                e.ErrorText = "CNPJ inválido.";
            }

            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
        }

        private void txtCpfCnpj_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!txtCpfCnpj.Text.EstahVazio())
            {
                if ((EnumTipoPessoa)cboTipoPessoa.EditValue == EnumTipoPessoa.PESSOAFISICA)
                {
                    if (!ValidacoesGerais.CpfEstahValido(txtCpfCnpj.Text))
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    if (!ValidacoesGerais.CnpjEstahValido(txtCpfCnpj.Text))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void chkFuncionario_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEhFuncionario.Checked)
            {
                tbcPessoa.TabPages.Add(tbpFuncionario);
            }
            else
            {
                tbcPessoa.TabPages.Remove(tbpFuncionario);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                var pessoa = _servicoPessoa.Consulte(txtId.Text.ToInt());

                if (pessoa != null)
                {
                    EditePessoa(pessoa);
                }
                else
                {
                    MessageBox.Show("Pessoa não encontrada.", "Pessoa não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    txtId.Text = string.Empty;
                }
            }
        }

        private void chkEhCliente_CheckedChanged(object sender, EventArgs e)
        {
            ExibirOuEsconderRadioConsumidorFinalOuRevenda(chkEhCliente.Checked);
        }

        #endregion

        #region " ATENDIMENTO "

        private void btnPesquisaIndicador_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var indicador = formPessoaPesquisa.PesquisePessoaIndicadora();

            if (indicador != null)
            {
                PreenchaIndicador(indicador);
            }
        }

        private void btnPesquisaAtendente_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var atendente = formPessoaPesquisa.PesquisePessoaAtendente();

            if (atendente != null)
            {
                PreenchaAtendente(atendente);
            }
        }

        private void btnPesquisaVendedor_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var vendedor = formPessoaPesquisa.PesquisePessoaVendedora();

            if (vendedor != null)
            {
                PreenchaVendedor(vendedor);
            }
        }

        private void btnPesquisaSupervisor_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var supervisor = formPessoaPesquisa.PesquisePessoaSupervisora();

            if (supervisor != null)
            {
                PreenchaSupervisor(supervisor);
            }
        }

        private void txtIdVendedor_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdVendedor.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var vendedor = servicoPessoa.ConsulteVendedorAtivo(txtIdVendedor.Text.ToInt());

                PreenchaVendedor(vendedor, true);
            }
            else
            {
                PreenchaVendedor(null);
            }
        }

        private void txtIdAtendente_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdAtendente.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var atendente = servicoPessoa.ConsulteAtendenteAtivo(txtIdAtendente.Text.ToInt());

                PreenchaAtendente(atendente, true);
            }
            else
            {
                PreenchaAtendente(null);
            }
        }

        private void txtIdIndicador_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdIndicador.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var indicador = servicoPessoa.ConsulteIndicadorAtivo(txtIdIndicador.Text.ToInt());

                PreenchaIndicador(indicador, true);
            }
            else
            {
                PreenchaIndicador(null);
            }
        }

        private void txtIdSupervisor_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdSupervisor.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var supervisor = servicoPessoa.ConsulteSupervisorAtivo(txtIdSupervisor.Text.ToInt());

                PreenchaSupervisor(supervisor, true);
            }
            else
            {
                PreenchaSupervisor(null);
            }
        }

        private void btnAtalhoOrigemCliente_Click(object sender, EventArgs e)
        {
            FormCadastroOrigemCliente formCadastroOrigemCLiente = new FormCadastroOrigemCliente();
            formCadastroOrigemCLiente.ShowDialog();

            PreenchaCboOrigemCliente();
        }

        private void btnAtalhoTabelaPreco_Click(object sender, EventArgs e)
        {
            FormTabelaDePreco formTabelaDePreco = new FormTabelaDePreco();
            formTabelaDePreco.ShowDialog();

            PreenchaAsTabelasDePreco();
        }

        private void btnAtalhoFormaPagamento_Click(object sender, EventArgs e)
        {
            FormCadastroFormaPagamento formCadastroFormaPagamento = new FormCadastroFormaPagamento();
            formCadastroFormaPagamento.ShowDialog();

            PreenchaCboFormaDePagamento();
        }

        private void btnAtalhoCondicaoPagamento_Click(object sender, EventArgs e)
        {
            FormCadastroCondicaoPagamento formCadastroCondicaoPagamento = new FormCadastroCondicaoPagamento();
            formCadastroCondicaoPagamento.ShowDialog();

            PreenchaAsCondicoesDePagamento();
        }

        #endregion

        #region " EMPRESA "

        private void carregaInformacoesEmpresa()
        {
            var pessoaCarregada = _servicoPessoa.Consulte(txtId.Text.ToInt());



            if (pessoaCarregada.EmpresaPessoa != null && pessoaCarregada.EmpresaPessoa.TipoInscricaoICMS != null)
            {
                rdbContribuinte.Checked = pessoaCarregada.EmpresaPessoa.TipoInscricaoICMS == EnumTipoInscricaoICMS.CONTRIBUINTEICMS? true:false;
                rdbNaoContribuinte.Checked = pessoaCarregada.EmpresaPessoa.TipoInscricaoICMS == EnumTipoInscricaoICMS.NAOCONTRIBUINTEICMS? true:false;
            }
            else if (txtInscricaoEstadual.Text != string.Empty)
            {
                rdbContribuinte.Checked = true;
                rdbNaoContribuinte.Checked = false;
            }
            else
            {
                rdbContribuinte.Checked = false;
                rdbNaoContribuinte.Checked = true;
            }
        }

        #endregion

        #region " ENDEREÇO "

        private void btnCancelarEndereco_Click(object sender, EventArgs e)
        {
            LimpeCamposEndereco();
        }

        private void btnSalvarEndereco_Click(object sender, EventArgs e)
        {
            AdicionarOuAlterarEnderecoNoGrid();
        }

        private void gcEnderecos_DoubleClick(object sender, EventArgs e)
        {
            EditeEndereco();
        }

        private void gcEnderecos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditeEndereco();
            }
        }

        private void btnExcluirEndereco_Click(object sender, EventArgs e)
        {
            ExcluaEndereco();
        }

        private void txtComplementoEndereco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AdicionarOuAlterarEnderecoNoGrid();
            }
        }

        private void txtCepEndereco_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCepEndereco.Text))
            {
                txtCepEndereco.SelectionStart = 0;
            }
        }

        private void txtCepEndereco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCepEndereco.Text.EstahVazio())
                {
                    PesquiseCepPelaTelaDePesquisa();
                }
                else
                {
                    //PesquiseCep();
                }
            }
        }

        private void txtCepEndereco_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_cepAtual) && _cepAtual.Equals(txtCepEndereco.Text))
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

        private void chkParceiroResideExterior_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParceiroResideExterior.Checked)
            {
                pnlEndereco.Enabled = false;
                cboPaisParceiro.Enabled = true;
                LimpeCamposEndereco();
                _listaDeEnderecos.Clear();
                PreenchaGridEndereco();
            }
            else
            {
                pnlEndereco.Enabled = true;
                cboPaisParceiro.Enabled = false;
                cboPaisParceiro.EditValue = null;
            }
        }

        private void cboEstado_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCidades();

            cboCidade.EditValue = null;
        }

        #endregion

        #region " DADOS PESSOAIS "

        private void btnPesquisaNaturalidade_Click(object sender, EventArgs e)
        {
            FormCidadePesquisa formCidadePesquisa = new FormCidadePesquisa();
            var cidade = formCidadePesquisa.PesquiseCidadeAtiva();

            if (cidade != null)
            {
                PreenchaCidadeDaNaturalidade(cidade);
            }
        }

        private void txtNaturalidadeId_Leave(object sender, EventArgs e)
        {
            PesquiseCidadeDaNaturalidade();
        }

        #endregion

        #region " FUNCIONÁRIO "


        #endregion

        #region " OPERAÇÕES BÁSICAS "

        private void cboParceiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (cboParceiro.Text.Length <= 2)
            {
                if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up
                    && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
                {

                }
            }
        }

        private void cboOrigemCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (cboOrigemCliente.Text.Length <= 2)
            {
                if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up
                    && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
                {

                }
            }
        }

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if(rdbContribuinte.Checked && string.IsNullOrEmpty(txtInscricaoEstadual.Text))
                {
                    MessageBox.Show("Se o Parceiro é contribuinte, então, você tem que informar a inscrição estadual.", "Informe a Inscrição Estadual", MessageBoxButtons.OK);
                    return;
                }

                var pessoa = RetornePessoaEmEdicao();

                if (string.IsNullOrEmpty(txtId.Text))
                {
                    _servicoPessoa = new ServicoPessoa();

                    _servicoPessoa.Cadastre(pessoa);
                }
                else
                {
                    _servicoPessoa = new ServicoPessoa();

                    _servicoPessoa.Atualize(pessoa);
                }

                MessageBox.Show("Cadastro salvo com sucesso!", "Cadastro salvo.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                LimpeCampos();
            }
            catch (ExceptionInconsistencias ex)
            {
                StringBuilder textoComTodasAsInconsistencias = new StringBuilder("Foram encontradas as seguintes inconsistências:");

                foreach (var inconsistencia in ex.Inconsistencias.ListaDeInconsistencias)
                {
                    textoComTodasAsInconsistencias.Append("\n\n");

                    textoComTodasAsInconsistencias.Append(inconsistencia.Mensagem);
                }

                MessageBox.Show(textoComTodasAsInconsistencias.ToString(), "Inconsistências ao salvar item!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado - " + ex.Message);
            }
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoa();

            if (pessoa != null)
            {
                EditePessoa(pessoa);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeCampos();
        }

        #endregion

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " TELEFONES "

        private void AdicionarOuAlterarTelefoneNoGrid()
        {
            try
            {
                var telefone = RetorneTelefoneEmEdicao();

                _servicoPessoa.ValideTelefonePessoa(telefone);

                AdicionarOuAlterarTelefoneNaLista(telefone);

                gcTelefones.DataSource = _listaDeTelefones;

                gcTelefones.RefreshDataSource();

                LimpeCamposDoTelefone();
            }
            catch (ExceptionInconsistencias ex)
            {
                StringBuilder textoComTodasAsInconsistencias = new StringBuilder("Foram encontradas as seguintes inconsistências:");

                foreach (var inconsistencia in ex.Inconsistencias.ListaDeInconsistencias)
                {
                    textoComTodasAsInconsistencias.Append("\n\n");

                    textoComTodasAsInconsistencias.Append(inconsistencia.Mensagem);
                }

                MessageBox.Show(textoComTodasAsInconsistencias.ToString(), "Inconsistências ao inserir telefone!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado - " + ex.Message);
            }
        }

        private void AdicionarOuAlterarTelefoneNaLista(Telefone telefone)
        {
            if (!_listaDeTelefones.Exists(item => item.Id == telefone.Id))
            {
                _listaDeTelefones.Add(telefone);

                ReordenarOsIdsDosTelefones();
            }

            _telefoneEmEdicao = null;
        }

        private void ReordenarOsIdsDosTelefones()
        {
            for (int i = 0; i < _listaDeTelefones.Count; i++)
            {
                var item = _listaDeTelefones[i];

                item.Id = i + 1;
            }
        }

        private Telefone RetorneTelefoneEmEdicao()
        {
            Telefone telefone = null;

            if (_telefoneEmEdicao != null)
            {
                telefone = _telefoneEmEdicao;
            }

            telefone = telefone ?? new Telefone();

            telefone.Numero = txtNumeroTelefone.Text;

            if (string.IsNullOrEmpty(txtDdd.Text))
            {
                telefone.Ddd = null;
            }
            else
            {
                telefone.Ddd = Convert.ToInt32(txtDdd.Text);
            }

            if (cboTipoTelefone.EditValue == null)
            {
                telefone.TipoTelefone = null;
            }
            else
            {
                telefone.TipoTelefone = (EnumTipoTelefone)cboTipoTelefone.EditValue;
            }

            telefone.Observacao = txtObservacoesTelefone.Text;

            return telefone;
        }

        private void PreenchaTiposTelefone()
        {
            var listaTelefones = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoTelefone>();
            listaTelefones.Insert(0, null);

            cboTipoTelefone.Properties.DataSource = listaTelefones;
            cboTipoTelefone.Properties.ValueMember = "Valor";
            cboTipoTelefone.Properties.DisplayMember = "Descricao";

            cboTipoTelefone.EditValue = null;
        }

        private void EditeTelefone()
        {
            SelecioneTelefoneParaEdicao();

            if (_telefoneEmEdicao != null)
            {
                cboTipoTelefone.EditValue = _telefoneEmEdicao.TipoTelefone;
                txtDdd.Text = _telefoneEmEdicao.Ddd.GetValueOrDefault().ToString();
                txtNumeroTelefone.Text = _telefoneEmEdicao.Numero;
                txtObservacoesTelefone.Text = _telefoneEmEdicao.Observacao;

                cboTipoTelefone.Focus();

                btnSalvarTelefone.Image = Properties.Resources.icon_atualizar;
            }
        }

        private void ExcluaTelefone()
        {
            SelecioneTelefoneParaEdicao();

            if (_telefoneEmEdicao != null)
            {
                string numeroDeTelefoneComDdd = "(" + _telefoneEmEdicao.Ddd + ") " + _telefoneEmEdicao.Numero;

                if (MessageBox.Show("Deseja excluir o telefone " + numeroDeTelefoneComDdd + "?", "Exclusão de telefone", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    _listaDeTelefones.Remove(_telefoneEmEdicao);

                    gcTelefones.DataSource = _listaDeTelefones;
                    gcTelefones.RefreshDataSource();

                    LimpeCamposDoTelefone();
                }
            }
        }

        private void SelecioneTelefoneParaEdicao()
        {
            if (_listaDeTelefones != null && _listaDeTelefones.Count > 0)
            {
                _telefoneEmEdicao = _listaDeTelefones.FirstOrDefault(item => item.Id == Convert.ToInt32(colunaTelefoneId.View.GetFocusedRowCellValue(colunaTelefoneId)));
            }
        }

        private void LimpeCamposDoTelefone()
        {
            cboTipoTelefone.EditValue = null;
            txtDdd.Text = string.Empty;
            txtNumeroTelefone.Text = string.Empty;
            txtObservacoesTelefone.Text = string.Empty;

            _telefoneEmEdicao = null;

            cboTipoTelefone.Focus();

            btnSalvarTelefone.Image = Properties.Resources.icones2_19;
        }

        #endregion

        #region " CRUD "

        private Pessoa RetornePessoaEmEdicao()
        {
            _pessoaEmEdicao = _pessoaEmEdicao ?? new Pessoa();

            _pessoaEmEdicao.Id = string.IsNullOrEmpty(txtId.Text) ? 0 : Convert.ToInt32(txtId.Text);

            _pessoaEmEdicao.DadosGerais = RetorneDadosGeraisEmEdicao();

            _pessoaEmEdicao.Atendimento = RetorneAtendimentoEmEdicao();

            _pessoaEmEdicao.EmpresaPessoa = RetorneEmpresaPessoaEmEdicao();

            _pessoaEmEdicao.DadosPessoais = RetorneDadosPessoaisEmEdicao();

            _pessoaEmEdicao.Funcionario = RetorneFuncionarioEmEdicao();

            _pessoaEmEdicao.ListaDeEnderecos = _listaDeEnderecos;

            _pessoaEmEdicao.ListaDeTelefones = _listaDeTelefones;

            return _pessoaEmEdicao;
        }

        private DadosGerais RetorneDadosGeraisEmEdicao()
        {
            DadosGerais dadosGerais = new DadosGerais();

            dadosGerais.TipoPessoa = (EnumTipoPessoa)cboTipoPessoa.EditValue;

            dadosGerais.Razao = txtRazaoSocial.Text;
            dadosGerais.NomeFantasia = txtNomeFantasia.Text;

            dadosGerais.Status = cboStatus.EditValue.ToString();

            dadosGerais.CpfCnpj = txtCpfCnpj.Text;

            dadosGerais.DataCadastro = string.IsNullOrEmpty(txtDataCadastro.Text) ? DateTime.MinValue : Convert.ToDateTime(txtDataCadastro.Text);

            dadosGerais.TipoCliente = (EnumTipoCliente)painelRazaoSocial.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            List<Byte> listaBytesFoto = TratamentoDeImagens.ConvertImagemToByte(picFoto).ToList();
            List<Byte> listaBytesFotoPadrao = TratamentoDeImagens.ConvertImagemToByte(Properties.Resources.user_img).ToList();

            bool fotoEhAPadrao = true;

            if (listaBytesFotoPadrao.Count != listaBytesFoto.Count)
            {
                fotoEhAPadrao = false;
            }
            else
            {
                for (int i = 0; i < listaBytesFoto.Count; i++)
                {
                    if (listaBytesFoto[i] != listaBytesFotoPadrao[i])
                    {
                        fotoEhAPadrao = false;

                        break;
                    }
                }
            }

            if (fotoEhAPadrao)
            {
                dadosGerais.Foto = null;
            }

            else
            {
                dadosGerais.Foto = TratamentoDeImagens.ConvertImagemToByte(picFoto);
            }

            dadosGerais.EhCliente = chkEhCliente.Checked;
            dadosGerais.EhFornecedor = chkEhFornecedor.Checked;
            dadosGerais.EhFuncionario = chkEhFuncionario.Checked;
            dadosGerais.EhTransportadora = chkEhTransportadora.Checked;

            dadosGerais.PessoaResideExterior = chkParceiroResideExterior.Checked;
            dadosGerais.Pais = cboPaisParceiro.EditValue != null ? new Pais { Id = cboPaisParceiro.EditValue.ToInt() } : null;

            return dadosGerais;
        }

        private Atendimento RetorneAtendimentoEmEdicao()
        {
            Atendimento atendimento = new Atendimento();

            if (!string.IsNullOrEmpty(txtIdIndicador.Text))
            {
                atendimento.Indicador = new Pessoa { Id = txtIdIndicador.Text.ToInt() };
            }

            if (!string.IsNullOrEmpty(txtIdAtendente.Text))
            {
                atendimento.Atendente = new Pessoa { Id = txtIdAtendente.Text.ToInt() };
            }

            if (!string.IsNullOrEmpty(txtIdVendedor.Text))
            {
                atendimento.Vendedor = new Pessoa { Id = txtIdVendedor.Text.ToInt() };
            }

            if (!string.IsNullOrEmpty(txtIdSupervisor.Text))
            {
                atendimento.Supervisor = new Pessoa { Id = txtIdSupervisor.Text.ToInt() };
            }

            if (cboTabelaPrecos.EditValue == null)
            {
                atendimento.TabelaDePreco = null;
            }
            else
            {
                atendimento.TabelaDePreco = new TabelaPreco { Id = Convert.ToInt32(cboTabelaPrecos.EditValue) };
            }

            if (cboCondicaoPagamento.EditValue == null)
            {
                atendimento.CondicaoDePagamento = null;
            }
            else
            {
                atendimento.CondicaoDePagamento = new CondicaoPagamento { Id = Convert.ToInt32(cboCondicaoPagamento.EditValue) };
            }

            if (cboFormaPagamento.EditValue == null)
            {
                atendimento.FormaPagamento = null;
            }
            else
            {
                atendimento.FormaPagamento = new FormaPagamento { Id = cboFormaPagamento.EditValue.ToString().ToInt() };
            }

            atendimento.OrigemCliente = cboOrigemCliente.EditValue != null ? new OrigemCliente { Id = cboOrigemCliente.EditValue.ToInt() } : null;

            atendimento.Observacoes = txtObs.Text;

            return atendimento;
        }

        private EmpresaPessoa RetorneEmpresaPessoaEmEdicao()
        {
            EmpresaPessoa empresaPessoa = new EmpresaPessoa();

            empresaPessoa.NomeContato1 = txtNomeContato1.Text;
            empresaPessoa.RamalContato1 = txtRamalContato1.Text;
            empresaPessoa.DepartamentoContato1 = txtDepartamentoContato1.Text;
            empresaPessoa.FuncaoContato1 = txtFuncaoContato1.Text;

            empresaPessoa.NomeContato2 = txtNomeContato2.Text;
            empresaPessoa.RamalContato2 = txtRamalContato2.Text;
            empresaPessoa.DepartamentoContato2 = txtDepartamentoContato2.Text;
            empresaPessoa.FuncaoContato2 = txtFuncaoContato2.Text;

            empresaPessoa.NomeContato3 = txtNomeContato3.Text;
            empresaPessoa.RamalContato3 = txtRamalContato3.Text;
            empresaPessoa.DepartamentoContato3 = txtDepartamentoContato3.Text;
            empresaPessoa.FuncaoContato3 = txtFuncaoContato3.Text;

            empresaPessoa.RamoDeAtividade = cboRamoAtividade.EditValue != null ? new RamoAtividade { Id = cboRamoAtividade.EditValue.ToInt() } : null;

            empresaPessoa.InscricaoMunicipal = txtInscricaoMunicipal.Text;
            empresaPessoa.InscricaoEstadual = txtInscricaoEstadual.Text;

            empresaPessoa.TipoInscricaoICMS = rdbContribuinte.Checked? EnumTipoInscricaoICMS.CONTRIBUINTEICMS:EnumTipoInscricaoICMS.NAOCONTRIBUINTEICMS;

            empresaPessoa.EmailPrincipal = txtEmailPrincipal.Text;
            empresaPessoa.EmailCobranca = txtEmailCobranca.Text;

            return empresaPessoa;
        }

        private DadosPessoais RetorneDadosPessoaisEmEdicao()
        {
            DadosPessoais dadosPessoais = new DadosPessoais();

            dadosPessoais.DataDeNascimento = txtDataDeNascimento.Text.ToDateNullabel();

            dadosPessoais.Sexo = (EnumSexo?)cboSexo.EditValue;

            dadosPessoais.EstadoCivil = (EnumEstadoCivil?)cboEstadoCivil.EditValue;

            dadosPessoais.Naturalidade = _cidadeNaturalidade;

            dadosPessoais.Nacionalidade = txtNacionalidade.Text;

            dadosPessoais.GrauDeInstrucao = (EnumGrauDeInstrucao?)cboGrauDeInstrucao.EditValue;
            dadosPessoais.Formacao = txtFormacao.Text;

            dadosPessoais.Identidade = txtIdentidade.Text;
            dadosPessoais.OrgaoEmissor = txtOrgaoEmissor.Text;
            dadosPessoais.DataEmissao = txtDataEmissaoIdentidade.Text.ToDateNullabel();

            dadosPessoais.EmpresaQueTrabalha = txtEmpresaQueTrabalha.Text;
            dadosPessoais.PossuiResidenciaPropria = (bool?)cboResidenciaPropria.EditValue;
            dadosPessoais.TempoDeResidencia = txtTempoDeResidencia.Text;

            dadosPessoais.RendaComprovada = txtRendaComprovada.Text.ToDoubleNullabel();
            dadosPessoais.RendaFamiliar = txtRendaFamiliar.Text.ToDoubleNullabel();

            dadosPessoais.FuncaoExercida = txtFuncaoExercidaAtualmente.Text;

            dadosPessoais.NomeDaMae = txtNomeMae.Text;
            dadosPessoais.NomeDoPai = txtNomePai.Text;

            dadosPessoais.Hobby = txtHobby.Text;
            dadosPessoais.EsporteFavorito = txtEsporteFavorito.Text;
            dadosPessoais.TimeFavorito = txtTimeFavorito.Text;

            dadosPessoais.IdEstrangeiro = txtIdEstrangeiro.Text;

            return dadosPessoais;
        }

        private Funcionario RetorneFuncionarioEmEdicao()
        {
            Funcionario funcionario = new Funcionario();

            funcionario.DataDeAdmissao = txtDataDeAdmissao.Text.ToDateNullabel();
            funcionario.DataDeDemissao = txtDataDeDemissao.Text.ToDateNullabel();

            funcionario.Salario = txtSalario.Text.ToDoubleNullabel();

            funcionario.MotivoRescisao = txtMotivoRescisao.Text;
            funcionario.Matricula = txtMatricula.Text;
            funcionario.Cargo = txtCargoFuncao.Text;

            funcionario.Departamento = txtDepartamento.Text;

            funcionario.NumeroCtps = txtNumeroCtps.Text;
            funcionario.SerieCtps = txtSerieCtps.Text;
            funcionario.UfCtps = txtUFCtps.Text;
            funcionario.DataEmissaoCtps = txtDataEmissaoCtps.Text.ToDateNullabel();

            funcionario.Pis = txtPis.Text;

            funcionario.NumeroCertificadoMilitar = txtNumeroCertificadoMilitar.Text;
            funcionario.CategoriaCertificadoMilitar = txtCategoriaCertificadoMilitar.Text;

            funcionario.NumeroTituloEleitor = txtNumeroTituloEleitor.Text;
            funcionario.SerieTiuloEleitor = txtZonaTituloEleitor.Text;
            funcionario.SecaoTituloEleitor = txtSecaoTituloEleitor.Text;

            funcionario.Agencia = txtAgencia.Text;
            funcionario.Banco = cboBancos.EditValue == null ? null : new Banco { Id = Convert.ToInt32(cboBancos.EditValue) };
            funcionario.Conta = txtContaCorrente.Text;
            funcionario.Operacao = txtOperacao.Text;

            funcionario.NumeroCnh = txtNumeroCnh.Text;
            funcionario.CategoriaCnh = txtCategoriaCnh.Text;
            funcionario.ValidadeCnh = txtValidadeCnh.Text.ToDateNullabel();

            funcionario.ConselhoCarteiraConselho = txtConselhoCarteiraConselho.Text;
            funcionario.NumeroCarteiraConselho = txtNumeroCarteiraConselho.Text;
            funcionario.DataInscricaoCarteiraConselho = txtDataInscricaoCarteiraConselho.Text.ToDateNullabel();

            return funcionario;
        }

        #endregion

        #region " PREENCHIMENTO DE COMBOBOXS "

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = "A";
        }

        private void PreenchaLimiteCreditoPadrao()
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();

            txtLimiteDeCredito.Text = parametros.ParametrosFinanceiro.ValoPadraoCreditoInicial.ToString("#,###,##0.00");
        }

        private void PreenchaOsTipoDePessoa()
        {
            ObjetoParaComboBox objetoComboBoxPessoaFisica = new ObjetoParaComboBox();
            objetoComboBoxPessoaFisica.Valor = EnumTipoPessoa.PESSOAFISICA;
            objetoComboBoxPessoaFisica.Descricao = EnumTipoPessoa.PESSOAFISICA.Descricao();

            ObjetoParaComboBox objetoComboBoxPessoaJuridica = new ObjetoParaComboBox();
            objetoComboBoxPessoaJuridica.Valor = EnumTipoPessoa.PESSOAJURIDICA;
            objetoComboBoxPessoaJuridica.Descricao = EnumTipoPessoa.PESSOAJURIDICA.Descricao();

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxPessoaFisica);
            listaDeItensParaOComboBox.Add(objetoComboBoxPessoaJuridica);

            cboTipoPessoa.Properties.DataSource = listaDeItensParaOComboBox;
            cboTipoPessoa.Properties.ValueMember = "Valor";
            cboTipoPessoa.Properties.DisplayMember = "Descricao";

            cboTipoPessoa.EditValue = EnumTipoPessoa.PESSOAFISICA;
        }

        private void PreenchaOsBancos()
        {
            ServicoBanco servicoBanco = new ServicoBanco();

            var listaBancos = servicoBanco.ConsulteLista();

            listaBancos.Insert(0, null);

            cboBancos.Properties.DataSource = listaBancos;
            cboBancos.Properties.DisplayMember = "Descricao";
            cboBancos.Properties.ValueMember = "Id";
        }

        private void PreenchaAsTabelasDePreco()
        {
            ServicoTabelaPreco servicoTabelaPreco = new ServicoTabelaPreco();

            var listaDeTabelasDePrecos = servicoTabelaPreco.ConsulteListaTabelaPrecosAtivas();

            listaDeTabelasDePrecos.Insert(0, null);

            cboTabelaPrecos.Properties.DataSource = listaDeTabelasDePrecos;
            cboTabelaPrecos.Properties.ValueMember = "Id";
            cboTabelaPrecos.Properties.DisplayMember = "NomeTabela";

            if (string.IsNullOrEmpty(cboTabelaPrecos.Text))
            {
                cboTabelaPrecos.EditValue = null;
            }
        }

        private void PreenchaAsCondicoesDePagamento()
        {
            ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();

            var listaDeCondicoesDePagamento = servicoCondicaoPagamento.ConsulteListaCondicoesPagamentoAtivas();

            listaDeCondicoesDePagamento.Insert(0, null);

            cboCondicaoPagamento.Properties.DataSource = listaDeCondicoesDePagamento;
            cboCondicaoPagamento.Properties.ValueMember = "Id";
            cboCondicaoPagamento.Properties.DisplayMember = "Descricao";

            if (string.IsNullOrEmpty(cboCondicaoPagamento.Text))
            {
                cboCondicaoPagamento.EditValue = null;
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

        private void PreenchaCboEstadoCivil()
        {
            var listaDeValoresEnumerador = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumEstadoCivil>();
            listaDeValoresEnumerador.Insert(0, null);

            cboEstadoCivil.Properties.DataSource = listaDeValoresEnumerador;
            cboEstadoCivil.Properties.ValueMember = "Valor";
            cboEstadoCivil.Properties.DisplayMember = "Descricao";
        }

        private void PreenchaCboGrauDeInstrucao()
        {
            var listaDeValoresEnumerador = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumGrauDeInstrucao>();
            listaDeValoresEnumerador.Insert(0, null);

            cboGrauDeInstrucao.Properties.DataSource = listaDeValoresEnumerador;
            cboGrauDeInstrucao.Properties.ValueMember = "Valor";
            cboGrauDeInstrucao.Properties.DisplayMember = "Descricao";
        }

        private void PreenchaCboFormaDePagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            var listaFormaPagamento = servicoFormaPagamento.ConsulteListaFormasDePagamentoAtivas();

            listaFormaPagamento.Insert(0, null);

            cboFormaPagamento.Properties.DataSource = listaFormaPagamento;
            cboFormaPagamento.Properties.ValueMember = "Id";
            cboFormaPagamento.Properties.DisplayMember = "Descricao";

            if (string.IsNullOrEmpty(cboFormaPagamento.Text))
            {
                cboFormaPagamento.EditValue = null;
            }
        }

        private void PreenchaCboResidenciaPropria()
        {
            ObjetoDescricaoValor objetoDescricaoValorSim = new ObjetoDescricaoValor();
            objetoDescricaoValorSim.Descricao = "Sim";
            objetoDescricaoValorSim.Valor = true;

            ObjetoDescricaoValor objetoDescricaoValorNao = new ObjetoDescricaoValor();
            objetoDescricaoValorNao.Descricao = "Não";
            objetoDescricaoValorNao.Valor = false;

            ObjetoDescricaoValor objetoDescricaoValorNulo = new ObjetoDescricaoValor();
            objetoDescricaoValorNulo.Descricao = string.Empty;
            objetoDescricaoValorNulo.Valor = null;

            List<ObjetoDescricaoValor> listaDeObjetosValores = new List<ObjetoDescricaoValor>();
            listaDeObjetosValores.Add(objetoDescricaoValorNulo);
            listaDeObjetosValores.Add(objetoDescricaoValorSim);
            listaDeObjetosValores.Add(objetoDescricaoValorNao);

            cboResidenciaPropria.Properties.DataSource = listaDeObjetosValores;
            cboResidenciaPropria.Properties.ValueMember = "Valor";
            cboResidenciaPropria.Properties.DisplayMember = "Descricao";
        }

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

        private void PreenchaCboRamoAtividade()
        {
            ServicoRamoAtividade servicoRamoAtividade = new ServicoRamoAtividade();

            var listaRamosAtividades = servicoRamoAtividade.ConsulteListaAtiva();

            listaRamosAtividades.Insert(0, null);

            cboRamoAtividade.Properties.DataSource = listaRamosAtividades;
            cboRamoAtividade.Properties.ValueMember = "Id";
            cboRamoAtividade.Properties.DisplayMember = "Descricao";

            if (string.IsNullOrEmpty(cboRamoAtividade.Text))
            {
                cboRamoAtividade.EditValue = null;
            }
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

            _listaCidadesCbo = listaDeCidades.CloneCompleto();

            listaDeCidades.Insert(0, null);

            cboCidade.Properties.DataSource = listaDeCidades;
            cboCidade.Properties.DisplayMember = "Descricao";
            cboCidade.Properties.ValueMember = "Id";
        }

        #endregion

        #region " ATENDIMENTO "

        private void PreenchaIndicador(Pessoa indicador, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (indicador != null)
            {
                txtIdIndicador.Text = indicador.Id.ToString();
                txtNomeIndicador.Text = indicador.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Indicador nao encontrado!", "Indicador não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdIndicador.Focus();
                }

                txtIdIndicador.Text = string.Empty;
                txtNomeIndicador.Text = string.Empty;
            }
        }

        private void PreenchaAtendente(Pessoa atendente, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (atendente != null)
            {
                txtIdAtendente.Text = atendente.Id.ToString();
                txtNomeAtendente.Text = atendente.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Atendente nao encontrado!", "Atendente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdAtendente.Focus();
                }

                txtIdAtendente.Text = string.Empty;
                txtNomeAtendente.Text = string.Empty;
            }
        }

        private void PreenchaVendedor(Pessoa vendedor, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (vendedor != null)
            {
                txtIdVendedor.Text = vendedor.Id.ToString();
                txtNomeVendedor.Text = vendedor.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Vendedor nao encontrado!", "Vendedor não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdVendedor.Focus();
                }

                txtIdVendedor.Text = string.Empty;
                txtNomeVendedor.Text = string.Empty;
            }
        }

        private void PreenchaSupervisor(Pessoa supervisor, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (supervisor != null)
            {
                txtIdSupervisor.Text = supervisor.Id.ToString();
                txtNomeSupervisor.Text = supervisor.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Supervisor nao encontrado!", "Supervisor não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdSupervisor.Focus();
                }

                txtIdSupervisor.Text = string.Empty;
                txtNomeSupervisor.Text = string.Empty;
            }
        }

        #endregion

        #region " ENDEREÇO "

        private void PreenchaCboTipoEndereco()
        {
            var listaDeValoresEnumerador = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoEndereco>();
            listaDeValoresEnumerador.Insert(0, null);

            cboTipoEndereco.Properties.DataSource = listaDeValoresEnumerador;
            cboTipoEndereco.Properties.ValueMember = "Valor";
            cboTipoEndereco.Properties.DisplayMember = "Descricao";
        }

        private void AdicionarOuAlterarEnderecoNoGrid()
        {
            try
            {
                EnderecoPessoa endereco = RetorneEnderecoEmEdicao();

                _servicoPessoa.ValideEnderecoPessoa(endereco, _listaDeEnderecos);

                AdicionarOuAlterarEnderecoNaLista(endereco);

                PreenchaGridEndereco();

                LimpeCamposEndereco();
            }
            catch (ExceptionInconsistencias ex)
            {
                StringBuilder textoComTodasAsInconsistencias = new StringBuilder("Foram encontradas as seguintes inconsistências:");

                foreach (var inconsistencia in ex.Inconsistencias.ListaDeInconsistencias)
                {
                    textoComTodasAsInconsistencias.Append("\n\n");

                    textoComTodasAsInconsistencias.Append(inconsistencia.Mensagem);
                }

                MessageBox.Show(textoComTodasAsInconsistencias.ToString(), "Inconsistências ao inserir endereço!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado - " + ex.Message);
            }
        }

        private void AdicionarOuAlterarEnderecoNaLista(EnderecoPessoa endereco)
        {
            if (_enderecoPessoaEmEdicao != null)
            {
                var indice = _listaDeEnderecos.IndexOf(_enderecoPessoaEmEdicao);
                _listaDeEnderecos.Remove(_enderecoPessoaEmEdicao);

                _listaDeEnderecos.Insert(indice, endereco);
            }
            else
            {
                _listaDeEnderecos.Add(endereco);

                ReordenarOsIdsDosEnderecos();
            }

            _enderecoPessoaEmEdicao = null;
        }

        private void ReordenarOsIdsDosEnderecos()
        {
            for (int i = 0; i < _listaDeEnderecos.Count; i++)
            {
                var item = _listaDeEnderecos[i];

                item.Id = i + 1;
            }
        }

        private EnderecoPessoa RetorneEnderecoEmEdicao()
        {
            EnderecoPessoa endereco = null;

            if (_enderecoPessoaEmEdicao != null)
            {
                endereco = _enderecoPessoaEmEdicao.CloneCompleto();
            }

            endereco = endereco ?? new EnderecoPessoa();

            endereco.CEP = txtCepEndereco.Text;
            endereco.Bairro = txtBairro.Text;
            endereco.Rua = txtRua.Text;

            endereco.Cidade = cboCidade.EditValue != null ? _listaCidadesCbo.FirstOrDefault(x => x.Id == cboCidade.EditValue.ToInt()) : null;

            endereco.Complemento = txtComplementoEndereco.Text;
            endereco.Numero = txtNumeroEndereco.Text;
            endereco.TipoEndereco = (EnumTipoEndereco?)cboTipoEndereco.EditValue;

            return endereco;
        }

        private void LimpeCamposEndereco()
        {
            cboTipoEndereco.EditValue = null;

            PreenchaEndereco(null);

            cboTipoEndereco.Focus();

            btnSalvarEndereco.Image = Properties.Resources.icones2_19;
        }

        private void PreenchaEndereco(EnderecoPessoa endereco)
        {
            if (endereco != null)
            {
                _cepAtual = endereco.CEP;

                txtCepEndereco.Text = endereco.CEP;
                cboEstado.EditValue = endereco.Cidade.Estado.UF;
                cboCidade.EditValue = endereco.Cidade.Id;
                txtBairro.Text = endereco.Bairro;
                txtRua.Text = endereco.Rua;

                txtComplementoEndereco.Text = endereco.Complemento;
                txtNumeroEndereco.Text = endereco.Numero;
                cboTipoEndereco.EditValue = endereco.TipoEndereco;
            }
            else
            {
                _cepAtual = string.Empty;

                txtCepEndereco.Text = string.Empty;
                cboEstado.EditValue = null;
                cboCidade.EditValue = null;
                txtBairro.Text = string.Empty;
                txtRua.Text = string.Empty;

                txtComplementoEndereco.Text = string.Empty;
                txtNumeroEndereco.Text = string.Empty;
                cboTipoEndereco.EditValue = null;
            }

            _enderecoPessoaEmEdicao = endereco;
        }

        private void PreenchaDadosEnderecoCep(Endereco endereco)
        {
            if (endereco != null)
            {
                _cepAtual = txtCepEndereco.Text;

                txtCepEndereco.Text = endereco.CEP;
                cboEstado.EditValue = endereco.Cidade.Estado.UF;
                cboCidade.EditValue = endereco.Cidade.Id;
                txtBairro.Text = endereco.Bairro;
                txtRua.Text = endereco.Rua;
            }
        }

        private void EditeEndereco()
        {
            SelecioneEnderecoParaEdicao();

            if (_enderecoPessoaEmEdicao != null)
            {
                PreenchaEndereco(_enderecoPessoaEmEdicao);

                cboTipoEndereco.Focus();

                btnSalvarEndereco.Image = Properties.Resources.icon_atualizar;
            }
        }

        private void SelecioneEnderecoParaEdicao()
        {
            if (_listaDeEnderecos != null && _listaDeEnderecos.Count > 0)
            {
                _enderecoPessoaEmEdicao = _listaDeEnderecos.FirstOrDefault(item => item.Id == Convert.ToInt32(colunaEnderecoId.View.GetFocusedRowCellValue(colunaEnderecoId)));
            }
        }

        private void ExcluaEndereco()
        {
            SelecioneEnderecoParaEdicao();

            if (_enderecoPessoaEmEdicao != null)
            {
                if (MessageBox.Show("Deseja excluir este endereço?", "Exclusão de endereço", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    _listaDeEnderecos.Remove(_enderecoPessoaEmEdicao);

                    PreenchaGridEndereco();

                    LimpeCamposEndereco();
                }
            }
        }

        private void PreenchaGridEndereco()
        {
            List<EnderecoPessoaGrid> listaEnderecosGrid = new List<EnderecoPessoaGrid>();

            foreach (var endereco in _listaDeEnderecos)
            {
                var enderecoGrid = new EnderecoPessoaGrid();

                enderecoGrid.Bairro = endereco.Bairro;
                enderecoGrid.Cidade = endereco.Cidade != null ? endereco.Cidade.Descricao : null;
                enderecoGrid.Uf = endereco.Cidade != null ? endereco.Cidade.Estado.UF : null;
                enderecoGrid.Id = endereco.Id;
                enderecoGrid.Rua = endereco.Rua;
                enderecoGrid.Tipo = endereco.TipoEndereco.Value.Descricao();

                listaEnderecosGrid.Add(enderecoGrid);
            }

            gcEnderecos.DataSource = listaEnderecosGrid;
            gcEnderecos.RefreshDataSource();
        }

        private void PesquiseCep()
        {
            if (!txtCepEndereco.Text.EstahVazio())
            {
                var endereco = _servicoEndereco.ConsulteAtivo(txtCepEndereco.Text);

                PreenchaDadosEnderecoCep(endereco);
            }
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

        private void PreenchaCboPaises()
        {
            ServicoPais servicoPais = new ServicoPais();
            cboPaisParceiro.Properties.DataSource = servicoPais.ConsulteLista();
            cboPaisParceiro.Properties.ValueMember = "Id";
            cboPaisParceiro.Properties.DisplayMember = "NomePais";
        }

        #endregion

        #region " ALTERAÇÃO DO TIPO DA PESSOA "

        private void AltereOTipoDaPessoa()
        {
            if ((EnumTipoPessoa)cboTipoPessoa.EditValue == EnumTipoPessoa.PESSOAFISICA)
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
            lblCpfCnpj.Text = "CPF";
            txtCpfCnpj.Text = string.Empty;
            txtCpfCnpj.Properties.Mask.EditMask = "000.000.000-00";

            txtRazaoSocial.Properties.ReadOnly = true;
            txtRazaoSocial.TabStop = false;
        }

        private void FacaAlteracoesParaTipoPessoaJuridica()
        {
            lblCpfCnpj.Text = "CNPJ";
            txtCpfCnpj.Text = string.Empty;
            txtCpfCnpj.Properties.Mask.EditMask = "00.000.000/0000-00";

            txtInscricaoEstadual.Properties.ReadOnly = false;
            txtInscricaoEstadual.TabStop = true;

            txtInscricaoMunicipal.Properties.ReadOnly = false;
            txtInscricaoMunicipal.TabStop = true;

            txtRazaoSocial.Properties.ReadOnly = false;
            txtRazaoSocial.TabStop = true;
        }

        #endregion

        #region " EDIÇÃO DE PESSOA "

        public void EditePessoa(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                txtId.Enabled = false;
                txtId.Text = pessoa.Id.ToString();

                PreenchaDadosGerais(pessoa.DadosGerais);
                PreenchaAtendimento(pessoa.Atendimento);
                PreenchaEmpresaPessoa(pessoa.EmpresaPessoa);
                PreenchaDadosPessoais(pessoa.DadosPessoais);
                PreenchaFuncionario(pessoa.Funcionario);
                PreenchaEnderecosPessoa(pessoa.ListaDeEnderecos.ToList());
                PreenchaTelefones(pessoa.ListaDeTelefones.ToList());
                carregaInformacoesEmpresa();
                _pessoaEmEdicao = pessoa;

                this.ActiveControl = cboTipoPessoa;

                this.Show();
            }
        }

        private void PreenchaDadosGerais(DadosGerais dadosGerais)
        {
            chkEhCliente.Checked = dadosGerais.EhCliente;
            chkEhFornecedor.Checked = dadosGerais.EhFornecedor;
            chkEhFuncionario.Checked = dadosGerais.EhFuncionario;
            chkEhTransportadora.Checked = dadosGerais.EhTransportadora;

            cboTipoPessoa.EditValue = dadosGerais.TipoPessoa;

            txtCpfCnpj.Text = dadosGerais.CpfCnpj;
            txtDataCadastro.Text = dadosGerais.DataCadastro.ToString("dd/MM/yyyy");
            txtRazaoSocial.Text = dadosGerais.Razao;
            txtNomeFantasia.Text = dadosGerais.NomeFantasia;

            painelRazaoSocial.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Tag.ToInt() == (int)dadosGerais.TipoCliente).Checked = true;

            cboStatus.EditValue = dadosGerais.Status;

            if (dadosGerais.Foto == null)
            {
                picFoto.Image = Properties.Resources.user_img;
            }
            else
            {
                picFoto.Image = TratamentoDeImagens.ConvertByteToImagem(dadosGerais.Foto).Image;
            }

            chkParceiroResideExterior.Checked = dadosGerais.PessoaResideExterior;
            cboPaisParceiro.EditValue = dadosGerais.Pais != null ? (int?)dadosGerais.Pais.Id : null;
        }

        private void PreenchaAtendimento(Atendimento atendimento)
        {
            if (atendimento != null)
            {
                txtObs.Text = atendimento.Observacoes;

                cboOrigemCliente.EditValue = atendimento.OrigemCliente != null ? (int?)atendimento.OrigemCliente.Id : null;

                if (atendimento.CondicaoDePagamento == null)
                {
                    cboCondicaoPagamento.EditValue = null;
                }
                else
                {
                    cboCondicaoPagamento.EditValue = atendimento.CondicaoDePagamento.Id;
                }

                if (atendimento.TabelaDePreco == null)
                {
                    cboTabelaPrecos.EditValue = null;
                }
                else
                {
                    cboTabelaPrecos.EditValue = atendimento.TabelaDePreco.Id;
                }

                if (atendimento.FormaPagamento == null)
                {
                    cboFormaPagamento.EditValue = null;
                }
                else
                {
                    cboFormaPagamento.EditValue = atendimento.FormaPagamento.Id;
                }

                PreenchaIndicador(atendimento.Indicador);
                PreenchaAtendente(atendimento.Atendente);
                PreenchaVendedor(atendimento.Vendedor);
                PreenchaSupervisor(atendimento.Supervisor);
            }
            else
            {
                PreenchaIndicador(null);
                PreenchaAtendente(null);
                PreenchaVendedor(null);
                PreenchaSupervisor(null);

                cboCondicaoPagamento.EditValue = null;
                cboTabelaPrecos.EditValue = null;
                cboFormaPagamento.EditValue = null;

                cboOrigemCliente.EditValue = null;

                txtObs.Text = string.Empty;
            }
        }

        private void PreenchaEmpresaPessoa(EmpresaPessoa empresaPessoa)
        {
            if (empresaPessoa != null)
            {
                txtNomeContato1.Text = empresaPessoa.NomeContato1;
                txtRamalContato1.Text = empresaPessoa.RamalContato1;
                txtDepartamentoContato1.Text = empresaPessoa.DepartamentoContato1;
                txtFuncaoContato1.Text = empresaPessoa.FuncaoContato1;

                txtNomeContato2.Text = empresaPessoa.NomeContato2;
                txtRamalContato2.Text = empresaPessoa.RamalContato2;
                txtDepartamentoContato2.Text = empresaPessoa.DepartamentoContato2;
                txtFuncaoContato2.Text = empresaPessoa.FuncaoContato2;

                txtNomeContato3.Text = empresaPessoa.NomeContato3;
                txtRamalContato3.Text = empresaPessoa.RamalContato3;
                txtDepartamentoContato3.Text = empresaPessoa.DepartamentoContato3;
                txtFuncaoContato3.Text = empresaPessoa.FuncaoContato3;

                cboRamoAtividade.EditValue = empresaPessoa.RamoDeAtividade != null ? (int?)empresaPessoa.RamoDeAtividade.Id : null;

                txtInscricaoEstadual.Text = empresaPessoa.InscricaoEstadual;

                rdbContribuinte.Checked = empresaPessoa.TipoInscricaoICMS == EnumTipoInscricaoICMS.CONTRIBUINTEICMS ? true : false;

                rdbNaoContribuinte.Checked = empresaPessoa.TipoInscricaoICMS == EnumTipoInscricaoICMS.NAOCONTRIBUINTEICMS? true : false;

                txtInscricaoMunicipal.Text = empresaPessoa.InscricaoMunicipal;

                txtEmailPrincipal.Text = empresaPessoa.EmailPrincipal;
                txtEmailCobranca.Text = empresaPessoa.EmailCobranca;

                ServicoCrediario servicoAnaliseCredito = new ServicoCrediario(false, false);
                var analiseCredito = servicoAnaliseCredito.Consulte(txtId.Text.ToInt());

                if (analiseCredito != null)
                {
                    txtLimiteDeCredito.Text = analiseCredito.ValorLimiteCredito.ToString("#,###,##0.00");
                }
                else
                {
                    PreenchaLimiteCreditoPadrao();
                }
            }
            else
            {
                txtNomeContato1.Text = string.Empty;
                txtRamalContato1.Text = string.Empty;
                txtDepartamentoContato1.Text = string.Empty;
                txtFuncaoContato1.Text = string.Empty;

                txtNomeContato2.Text = string.Empty;
                txtRamalContato2.Text = string.Empty;
                txtDepartamentoContato2.Text = string.Empty;
                txtFuncaoContato2.Text = string.Empty;

                txtNomeContato3.Text = string.Empty;
                txtRamalContato3.Text = string.Empty;
                txtDepartamentoContato3.Text = string.Empty;
                txtFuncaoContato3.Text = string.Empty;

                cboRamoAtividade.EditValue = null;

                txtInscricaoEstadual.Text = string.Empty;
                txtInscricaoMunicipal.Text = string.Empty;

                txtEmailPrincipal.Text = string.Empty;
                txtEmailCobranca.Text = string.Empty;

                PreenchaLimiteCreditoPadrao();
            }
        }

        private void PreenchaDadosPessoais(DadosPessoais dadosPessoais)
        {
            if (dadosPessoais != null)
            {
                txtDataDeNascimento.Text = dadosPessoais.DataDeNascimento != null ? dadosPessoais.DataDeNascimento.Value.ToString("dd/MM/yyyy") : string.Empty;
                cboSexo.EditValue = dadosPessoais.Sexo;
                cboEstadoCivil.EditValue = dadosPessoais.EstadoCivil;

                PreenchaCidadeDaNaturalidade(dadosPessoais.Naturalidade);

                txtNacionalidade.Text = dadosPessoais.Nacionalidade;

                cboGrauDeInstrucao.EditValue = dadosPessoais.GrauDeInstrucao;

                txtFormacao.Text = dadosPessoais.Formacao;

                txtIdentidade.Text = dadosPessoais.Identidade;
                txtOrgaoEmissor.Text = dadosPessoais.OrgaoEmissor;
                txtDataEmissaoIdentidade.Text = dadosPessoais.DataEmissao != null ? dadosPessoais.DataEmissao.Value.ToString("dd/MM/yyyy") : string.Empty;

                txtEmpresaQueTrabalha.Text = dadosPessoais.EmpresaQueTrabalha;

                cboResidenciaPropria.EditValue = dadosPessoais.PossuiResidenciaPropria;
                txtTempoDeResidencia.Text = dadosPessoais.TempoDeResidencia;

                txtRendaComprovada.Text = dadosPessoais.RendaComprovada != null ? dadosPessoais.RendaComprovada.Value.ToString("0.00") : string.Empty;
                txtRendaFamiliar.Text = dadosPessoais.RendaComprovada != null ? dadosPessoais.RendaFamiliar.Value.ToString("0.00") : string.Empty;

                txtFuncaoExercidaAtualmente.Text = dadosPessoais.FuncaoExercida;
                txtNomeMae.Text = dadosPessoais.NomeDaMae;
                txtNomePai.Text = dadosPessoais.NomeDoPai;

                txtHobby.Text = dadosPessoais.Hobby;
                txtEsporteFavorito.Text = dadosPessoais.EsporteFavorito;
                txtTimeFavorito.Text = dadosPessoais.TimeFavorito;

                txtIdEstrangeiro.Text = dadosPessoais.IdEstrangeiro;
            }
            else
            {
                txtDataDeNascimento.Text = string.Empty;
                cboSexo.EditValue = null;
                cboEstadoCivil.EditValue = null;

                PreenchaCidadeDaNaturalidade(null);

                txtNacionalidade.Text = string.Empty;

                cboGrauDeInstrucao.EditValue = null;

                txtFormacao.Text = string.Empty;

                txtIdentidade.Text = string.Empty;
                txtOrgaoEmissor.Text = string.Empty;
                txtDataEmissaoIdentidade.Text = string.Empty;

                txtEmpresaQueTrabalha.Text = string.Empty;

                cboResidenciaPropria.EditValue = null;
                txtTempoDeResidencia.Text = string.Empty;

                txtRendaComprovada.Text = string.Empty;
                txtRendaFamiliar.Text = string.Empty;

                txtFuncaoExercidaAtualmente.Text = string.Empty;
                txtNomeMae.Text = string.Empty;
                txtNomePai.Text = string.Empty;

                txtHobby.Text = string.Empty;
                txtEsporteFavorito.Text = string.Empty;
                txtTimeFavorito.Text = string.Empty;

                txtIdEstrangeiro.Text = string.Empty;
            }
        }

        private void PreenchaFuncionario(Funcionario funcionario)
        {
            if (funcionario != null)
            {
                txtDataDeAdmissao.Text = funcionario.DataDeAdmissao != null ? funcionario.DataDeAdmissao.Value.ToString("dd/MM/yyyy") : string.Empty;
                txtDataDeDemissao.Text = funcionario.DataDeDemissao != null ? funcionario.DataDeDemissao.Value.ToString("dd/MM/yyyy") : string.Empty;

                txtSalario.Text = funcionario.Salario != null ? funcionario.Salario.Value.ToString("0.00") : string.Empty;

                txtMotivoRescisao.Text = funcionario.MotivoRescisao;
                txtMatricula.Text = funcionario.Matricula;

                txtCargoFuncao.Text = funcionario.Cargo;
                txtDepartamento.Text = funcionario.Departamento;

                txtNumeroCtps.Text = funcionario.NumeroCtps;
                txtSerieCtps.Text = funcionario.SerieCtps;
                txtUFCtps.Text = funcionario.UfCtps;
                txtDataEmissaoCtps.Text = funcionario.DataEmissaoCtps != null ? funcionario.DataEmissaoCtps.Value.ToString("dd/MM/yyyy") : string.Empty;

                txtPis.Text = funcionario.Pis;

                txtNumeroCertificadoMilitar.Text = funcionario.NumeroCertificadoMilitar;
                txtCategoriaCertificadoMilitar.Text = funcionario.CategoriaCertificadoMilitar;

                txtNumeroTituloEleitor.Text = funcionario.NumeroTituloEleitor;
                txtZonaTituloEleitor.Text = funcionario.SerieTiuloEleitor;
                txtSecaoTituloEleitor.Text = funcionario.SecaoTituloEleitor;

                cboBancos.EditValue = funcionario.Banco != null ? (int?)funcionario.Banco.Id : null;
                txtOperacao.Text = funcionario.Operacao;
                txtAgencia.Text = funcionario.Agencia;
                txtContaCorrente.Text = funcionario.Conta;

                txtNumeroCnh.Text = funcionario.NumeroCnh;
                txtCategoriaCnh.Text = funcionario.CategoriaCnh;
                txtValidadeCnh.Text = funcionario.ValidadeCnh != null ? funcionario.ValidadeCnh.Value.ToString("dd/MM/yyyy") : string.Empty;

                txtConselhoCarteiraConselho.Text = funcionario.ConselhoCarteiraConselho;
                txtNumeroCarteiraConselho.Text = funcionario.NumeroCarteiraConselho;
                txtDataInscricaoCarteiraConselho.Text = funcionario.DataInscricaoCarteiraConselho != null ? funcionario.DataInscricaoCarteiraConselho.Value.ToString("dd/MM/yyyy") : string.Empty;
            }
            else
            {
                txtDataDeAdmissao.Text = string.Empty;
                txtDataDeDemissao.Text = string.Empty;

                txtSalario.Text = string.Empty;

                txtMotivoRescisao.Text = string.Empty;
                txtMatricula.Text = string.Empty;

                txtCargoFuncao.Text = string.Empty;
                txtDepartamento.Text = string.Empty;

                txtNumeroCtps.Text = string.Empty;
                txtSerieCtps.Text = string.Empty;
                txtUFCtps.Text = string.Empty;
                txtDataEmissaoCtps.Text = string.Empty;

                txtPis.Text = string.Empty;

                txtNumeroCertificadoMilitar.Text = string.Empty;
                txtCategoriaCertificadoMilitar.Text = string.Empty;

                txtNumeroTituloEleitor.Text = string.Empty;
                txtZonaTituloEleitor.Text = string.Empty;
                txtSecaoTituloEleitor.Text = string.Empty;

                cboBancos.EditValue = null;
                txtOperacao.Text = string.Empty;
                txtAgencia.Text = string.Empty;
                txtContaCorrente.Text = string.Empty;

                txtNumeroCnh.Text = string.Empty;
                txtCategoriaCnh.Text = string.Empty;
                txtValidadeCnh.Text = string.Empty;

                txtConselhoCarteiraConselho.Text = string.Empty;
                txtNumeroCarteiraConselho.Text = string.Empty;
                txtDataInscricaoCarteiraConselho.Text = string.Empty;
            }
        }

        private void PreenchaEnderecosPessoa(List<EnderecoPessoa> listaDeEnderecos)
        {
            _listaDeEnderecos = listaDeEnderecos;

            PreenchaGridEndereco();
        }

        private void PreenchaTelefones(List<Telefone> listaDeTelefones)
        {
            _listaDeTelefones.Clear();

            foreach (var telefone in listaDeTelefones)
            {
                var telefoneClonado = telefone.CloneCompleto();


                _listaDeTelefones.Add(telefoneClonado);
            }

            gcTelefones.DataSource = _listaDeTelefones;
            gcTelefones.RefreshDataSource();
        }

        private void LimpeCampos()
        {
            Pessoa pessoa = new Pessoa();

            txtId.Text = string.Empty;
            txtId.Enabled = true;

            pessoa.DadosGerais.TipoPessoa = EnumTipoPessoa.PESSOAFISICA;
            //pessoa.EmpresaPessoa.InscricaoEstadual = pessoa.EmpresaPessoa.InscricaoMunicipal = "ISENTO";
            pessoa.DadosGerais.DataCadastro = DateTime.Now.Date;
            pessoa.DadosGerais.Status = "A";

            PreenchaDadosGerais(pessoa.DadosGerais);
            PreenchaAtendimento(pessoa.Atendimento);
            PreenchaEmpresaPessoa(pessoa.EmpresaPessoa);
            PreenchaDadosPessoais(pessoa.DadosPessoais);
            PreenchaFuncionario(pessoa.Funcionario);
            PreenchaTelefones(pessoa.ListaDeTelefones.ToList());
            PreenchaEnderecosPessoa(pessoa.ListaDeEnderecos.ToList());

            txtObs.Text = string.Empty;

            LimpeCamposEndereco();

            this.ActiveControl = txtId;

            _pessoaEmEdicao = null;
        }

        #endregion

        #region " NATURALIDADE "

        private void PesquiseCidadeDaNaturalidade()
        {
            ServicoCidade servicoDeCidade = new ServicoCidade();

            var cidade = servicoDeCidade.ConsultePeloCodigoIbgeAtivo(txtNaturalidadeIbge.Text);

            PreenchaCidadeDaNaturalidade(cidade);
        }

        private void PreenchaCidadeDaNaturalidade(Cidade cidade)
        {
            _cidadeNaturalidade = cidade;

            if (cidade != null)
            {
                txtCidadeEstadoNaturalidade.Text = cidade.Descricao + (cidade.Estado != null ? " - " + cidade.Estado.UF : string.Empty);
                txtNaturalidadeIbge.Text = cidade.CodigoIbge;
            }
            else
            {
                txtCidadeEstadoNaturalidade.Text = string.Empty;
                txtNaturalidadeIbge.Text = string.Empty;
            }
        }

        #endregion

        #region " TRATAMENTO DE TE PERMISSÕES ATALHOS"

        private void TrateUsuarioContemPermissaoAtalhos()
        {
            TrateUsuarioNaoTemPermissaoAtalho(pnlTabelaPreco, cboTabelaPrecos, btnAtalhoTabelaPreco, EnumFuncionalidade.TABELAPRECO);
            TrateUsuarioNaoTemPermissaoAtalho(pnlFormaPagamento, cboFormaPagamento, btnAtalhoFormaPagamento, EnumFuncionalidade.FORMAPAGAMENTO);
            TrateUsuarioNaoTemPermissaoAtalho(pnlCondicaoPagamento, cboCondicaoPagamento, btnAtalhoCondicaoPagamento, EnumFuncionalidade.CONDICOESPAGAMENTO);
            TrateUsuarioNaoTemPermissaoAtalho(pnlCondicaoPagamento, cboCondicaoPagamento, btnAtalhoCondicaoPagamento, EnumFuncionalidade.CONDICOESPAGAMENTO);
        }

        #endregion

        #region " EXIBIR OU ESCONDER RADIO CONSUMIDOR FINAL OU REVENDA "

        private void ExibirOuEsconderRadioConsumidorFinalOuRevenda(bool exibir)
        {
            if (exibir)
            {
                rdbConsumidorFinal.Visible = true;
                rdbRevenda.Visible = true;
                txtRazaoSocial.Size = new Size(302, 22);
            }
            else
            {
                rdbConsumidorFinal.Visible = false;
                rdbRevenda.Visible = false;
                rdbConsumidorFinal.Checked = true;

                txtRazaoSocial.Size = painelRazaoSocial.Size;
            }
        }

        #endregion

        #endregion

        #region " CLASSES AUXILIARES "

        private class EnderecoPessoaGrid
        {
            public int Id { get; set; }

            public string Tipo { get; set; }

            public string Rua { get; set; }

            public string Bairro { get; set; }

            public string Cidade { get; set; }

            public string Uf { get; set; }
        }


        #endregion

        private void txtNomeFantasia_EditValueChanged(object sender, EventArgs e)
        {
            if ((EnumTipoPessoa)(cboTipoPessoa.EditValue) == EnumTipoPessoa.PESSOAFISICA)
            {
                txtRazaoSocial.Text = txtNomeFantasia.Text;
            }
        }
    }
}
