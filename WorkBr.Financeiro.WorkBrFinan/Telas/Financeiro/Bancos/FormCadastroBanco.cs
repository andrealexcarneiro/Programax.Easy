using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Financeiro.Bancos
{
    public partial class FormCadastroBanco : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idBanco;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroBanco()
        {
            InitializeComponent();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            PreenchaOStatus();

            this.ActiveControl = txtCodigoCompensacao;

            this.NomeDaTela = "Cadastro de Banco";
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Banco banco = new Banco();

            banco.Id = _idBanco;
            banco.Descricao = txtDescricao.Text;
            banco.DataCadastro = txtDataCadastro.Text.ToDate();
            banco.Site = txtSite.Text;
            banco.Codigo = txtCodigoCompensacao.Text;
            banco.Status = cboStatus.EditValue.ToString();

            Action actionSalvar = () =>
            {
                ServicoBanco servicoBanco = new ServicoBanco();

                if (banco.Id == 0)
                {
                    servicoBanco.Cadastre(banco);
                }
                else
                {
                    servicoBanco.Atualize(banco);
                }

                PesquisePeloCodigoDoBanco();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormBancosPesquisa formBancosPesquisa = new FormBancosPesquisa();

            var banco = formBancosPesquisa.ExibaPesquisaDeBancos();

            if (banco != null)
            {
                EditeBanco(banco);
            }
        }

        private void txtCodigoCompensacao_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoCompensacao.Text))
            {
                var codigoBanco = txtCodigoCompensacao.Text;

                PesquisePeloCodigoDoBanco();

                txtDescricao.Focus();

                txtCodigoCompensacao.Text = codigoBanco;
            }
            else
            {
                EditeBanco(null);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void LimpeFormulario()
        {
            EditeBanco(null);

            txtCodigoCompensacao.Focus();
        }

        private void EditeBanco(Banco banco)
        {
            if (banco != null)
            {
                _idBanco = banco.Id;

                txtDescricao.Text = banco.Descricao;
                txtDataCadastro.Text = banco.DataCadastro.ToString("dd/MM/yyyy");
                txtCodigoCompensacao.Text = banco.Codigo;
                txtSite.Text = banco.Site;

                cboStatus.EditValue = banco.Status;

                txtDescricao.Focus();
            }
            else
            {
                _idBanco = 0;
                txtDescricao.Text = string.Empty;
                txtCodigoCompensacao.Text = string.Empty;
                txtSite.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
        }

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

        private void PesquisePeloCodigoDoBanco()
        {
            ServicoBanco servicoBanco = new ServicoBanco();
            var banco = servicoBanco.ConsultePeloCodigoBanco(txtCodigoCompensacao.Text);

            EditeBanco(banco);
        }

        #endregion
    }
}
