using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EnderecoServ;
using Programax.Easy.Servico.Financeiro.AgenciaServ;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Enderecos;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Financeiro.Bancos;
using Programax.Easy.Negocio;
using System.Linq;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using System.Drawing;

namespace Programax.Easy.View.Telas.Financeiro.Agencias
{
    public partial class FormCadastroAgencia : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Endereco _enderecoSelecionado;
        private int _idAgencia;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroAgencia()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboBanco();

            this.NomeDaTela = "Cadastro de Agência";

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            TrateUsuarioNaoTemPermissaoCadastroBanco();

            this.ActiveControl = cboBancos;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {          
            Action actionSalvar = () =>
            {
                Agencia agencia = RetorneAgenciaEmEdicao();

                ServicoAgencia servicoAgencia = new ServicoAgencia();

                if (agencia.Id == 0)
                {
                    servicoAgencia.Cadastre(agencia);
                }
                else
                {
                    servicoAgencia.Atualize(agencia);
                }

                PesquiseAgenciaPeloNumero();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void pbPesquisaFormaPagamento_Click(object sender, EventArgs e)
        {
            FormAgenciaPesquisa formAgenciaPesquisa = new FormAgenciaPesquisa();

            var agencia = formAgenciaPesquisa.ExibaPesquisaDeAgencias(cboBancos.EditValue.ToInt());

            if (agencia != null)
            {
                EditeAgencia(agencia);
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

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void txtNumeroAgencia_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNumeroAgencia.Text))
            {
                PesquiseAgenciaPeloNumero();
            }
            else
            {
                EditeAgencia(null);
            }
        }

        private void cboBancos_EditValueChanged(object sender, EventArgs e)
        {
            if (cboBancos.EditValue != null)
            {
                pnlInformacoesAgencia.Enabled = true;
            }
            else
            {
                pnlInformacoesAgencia.Enabled = false;
            }
        }

        private void btnAtalhoBanco_Click(object sender, EventArgs e)
        {
            ExibaCadastroBanco();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void ExibaCadastroAgencia(Banco banco)
        {
            cboBancos.EditValue = banco.Id;
            cboBancos.Enabled = false;

            this.ActiveControl = txtNumeroAgencia;

            this.ShowDialog();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

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

        private void PreenchaCboBanco()
        {
            ServicoBanco servicoBanco = new ServicoBanco();

            var listaBancos = servicoBanco.ConsulteListaAtiva();

            listaBancos.Insert(0, null);

            cboBancos.Properties.DataSource = listaBancos;
            cboBancos.Properties.DisplayMember = "Descricao";
            cboBancos.Properties.ValueMember = "Id";

            if (cboBancos.EditValue != null)
            {
                if (!listaBancos.Exists(banco => banco != null && banco.Id == cboBancos.EditValue.ToInt()))
                {
                    cboBancos.EditValue = null;
                }
            }
        }

        private void LimpeFormulario()
        {
            EditeAgencia(null, bloquearPainel: true, limparBanco: true, limparNumero: true, focoNoCboBanco: true);
        }

        private void EditeAgencia(Agencia agencia, bool bloquearPainel = false, bool limparBanco = false, bool limparNumero = false, bool focoNoCboBanco = false)
        {
            if (agencia != null)
            {
                _idAgencia = agencia.Id;

                pnlInformacoesAgencia.Enabled = true;

                txtNomeAgencia.Text = agencia.NomeAgencia;
                txtNumeroAgencia.Text = agencia.NumeroAgencia;
                txtDigito.Text = agencia.DigitoAgencia;
                cboStatus.EditValue = agencia.Status;

                cboBancos.EditValue = agencia.Banco != null ? (int?)agencia.Banco.Id : null;

                PreenchaDadosEndereco(agencia.Endereco);

                txtNumeroEndereco.Text = agencia.NumeroEndereco;
                txtComplementoEndereco.Text = agencia.ComplementoEndereco;

                txtTelefone1.Text = agencia.Telefone1;
                txtTelefone2.Text = agencia.Telefone2;
                txtNomeGerente.Text = agencia.NomeGerente;
                txtCelularGerente.Text = agencia.CelularGerente;

                txtDataCadastro.Text = agencia.DataCadastro.ToString("dd/MM/yyyy");

                txtObservacoes.Text = agencia.Observacoes;

                txtDigito.Focus();
            }
            else
            {
                if (bloquearPainel)
                {
                    pnlInformacoesAgencia.Enabled = false;
                }

                _idAgencia = 0;

                txtNomeAgencia.Text = string.Empty;

                if (limparNumero)
                {
                    txtNumeroAgencia.Text = string.Empty;
                }

                txtDigito.Text = string.Empty;
                cboStatus.EditValue = "A";

                if (limparBanco)
                {
                    cboBancos.EditValue = null;
                }

                PreenchaDadosEndereco(null);

                txtNumeroEndereco.Text = string.Empty;
                txtComplementoEndereco.Text = string.Empty;

                txtTelefone1.Text = string.Empty;
                txtTelefone2.Text = string.Empty;
                txtNomeGerente.Text = string.Empty;
                txtCelularGerente.Text = string.Empty;

                txtObservacoes.Text = string.Empty;

                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                if (focoNoCboBanco)
                {
                    cboBancos.Focus();
                }
                else
                {
                    txtDigito.Focus();
                }
            }
        }

        private void PesquiseCep()
        {
            ServicoEndereco servicoEndereco = new ServicoEndereco();

            var endereco = servicoEndereco.Consulte(txtCepEndereco.Text);

            PreenchaDadosEndereco(endereco);
        }

        private void PesquiseCepPelaTelaDePesquisa()
        {
            FormEnderecoPesquisa formEnderecoPesquisa = new FormEnderecoPesquisa();

            var endereco = formEnderecoPesquisa.PesquiseEndereco();

            if (endereco != null)
            {
                PreenchaDadosEndereco(endereco);
            }
        }

        private void PreenchaDadosEndereco(Endereco endereco)
        {
            _enderecoSelecionado = endereco;

            if (endereco != null)
            {
                txtCepEndereco.Text = endereco.CEP;
                txtEstadoEndereco.Text = endereco.Cidade.Estado.UF + " - " + endereco.Cidade.Estado.Nome;
                txtCidadeEndereco.Text = endereco.Cidade.Descricao;
                txtBairroEndereco.Text = endereco.Bairro;
                txtRuaEndereco.Text = endereco.Rua;
            }
            else
            {
                txtCepEndereco.Text = string.Empty;
                txtEstadoEndereco.Text = string.Empty;
                txtCidadeEndereco.Text = string.Empty;
                txtBairroEndereco.Text = string.Empty;
                txtRuaEndereco.Text = string.Empty;
            }
        }

        private Agencia RetorneAgenciaEmEdicao()
        {
            Agencia agencia = new Agencia();

            agencia.Id = _idAgencia;

            agencia.Status = cboStatus.EditValue.ToString();
            agencia.NomeAgencia = txtNomeAgencia.Text;
            agencia.NumeroAgencia = txtNumeroAgencia.Text;
            agencia.DigitoAgencia = txtDigito.Text;

            agencia.Banco = cboBancos.EditValue != null ? new Banco { Id = cboBancos.EditValue.ToInt() } : null;

            agencia.Endereco = _enderecoSelecionado;
            agencia.NumeroEndereco = txtNumeroEndereco.Text;
            agencia.ComplementoEndereco = txtComplementoEndereco.Text;

            agencia.Telefone1 = txtTelefone1.Text;
            agencia.Telefone2 = txtTelefone2.Text;
            agencia.NomeGerente = txtNomeGerente.Text;
            agencia.CelularGerente = txtCelularGerente.Text;

            agencia.Observacoes = txtObservacoes.Text;

            agencia.DataCadastro = txtDataCadastro.Text.ToDate();

            return agencia;
        }

        private void PesquiseAgenciaPeloNumero()
        {
            ServicoAgencia servicoAgencia = new ServicoAgencia();
            Agencia agencia = servicoAgencia.Consulte(cboBancos.EditValue.ToInt(), txtNumeroAgencia.Text);

            EditeAgencia(agencia);
        }

        private void ExibaCadastroBanco()
        {
            FormCadastroBanco formCadastroBanco = new FormCadastroBanco();
            formCadastroBanco.ShowDialog();

            PreenchaCboBanco();
        }

        private void TrateUsuarioNaoTemPermissaoCadastroBanco()
        {
            var permissao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.BANCOS);

            if (!permissao.Alterar)
            {
                cboBancos.Size = new Size(pnlBanco.Width, cboBancos.Size.Height);

                btnAtalhoBanco.Visible = false;
            }
        }

        #endregion
    }
}
