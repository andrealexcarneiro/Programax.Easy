using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.CfopServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.View.Telas.Fiscal.Cfops
{
    public partial class FormCadastroDeCfop : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idCfopEdicao;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroDeCfop()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboCfopConversao();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.NomeDaTela = "Cadastro de CFOP";

            this.ActiveControl = txtCodigoCfop;
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
            Action actionSalvar = () =>
            {
                Cfop cfop = new Cfop();

                cfop.Id = _idCfopEdicao;
                cfop.Descricao = txtDescricao.Text;
                cfop.Codigo = txtCodigoCfop.Text;
                cfop.DataCadastro = txtDataCadastro.Text.ToDate();
                cfop.Status = cboStatus.EditValue.ToString();
                cfop.InformacoesComplementaresNFe = txtInformacoesComplementaresNFe.Text;

                cfop.CfopDeConversao = cboCfopConversao.EditValue != null ? new Cfop { Id = cboCfopConversao.EditValue.ToInt() } : null;

                ServicoCfop servicoCfop = new ServicoCfop();

                if (cfop.Id == 0)
                {
                    servicoCfop.Cadastre(cfop);
                }
                else
                {
                    servicoCfop.Atualize(cfop);
                }

                PesquisePeloCodigoCfop();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormCfopPesquisa formCfopPesquisa = new FormCfopPesquisa();

            var cfop = formCfopPesquisa.PesquiseUmCfop();

            if (cfop != null)
            {
                EditeCfop(cfop);
            }
        }

        private void txtCodigoCfop_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodigoCfop.Text))
            {
                PesquisePeloCodigoCfop();
            }
        }

        private void txtCodigoCfop_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
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

        private void LimpeFormulario(bool focoNoCodigoCfop = true)
        {
            _idCfopEdicao = 0;
            txtDescricao.Text = string.Empty;

            txtCodigoCfop.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            cboStatus.EditValue = "A";
            cboCfopConversao.EditValue = null;
            txtInformacoesComplementaresNFe.Text = string.Empty;

            if (focoNoCodigoCfop)
            {
                txtCodigoCfop.Focus();
            }
        }

        private void EditeCfop(Cfop cfop)
        {
            if (cfop != null)
            {
                _idCfopEdicao = cfop.Id;
                txtDescricao.Text = cfop.Descricao;

                txtCodigoCfop.Text = cfop.Codigo;
                txtDescricao.Text = cfop.Descricao;
                txtDataCadastro.Text = cfop.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = cfop.Status;
                cboCfopConversao.EditValue = cfop.CfopDeConversao != null ? (int?)cfop.CfopDeConversao.Id : null;

                txtInformacoesComplementaresNFe.Text = cfop.InformacoesComplementaresNFe;

                txtDescricao.Focus();
            }
            else
            {
                _idCfopEdicao = 0;
                txtDescricao.Text = string.Empty;

                txtCodigoCfop.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                cboStatus.EditValue = "A";
                cboCfopConversao.EditValue = null;

                txtInformacoesComplementaresNFe.Text = string.Empty;

                txtCodigoCfop.Focus();
            }
        }

        private void PesquisePeloCodigoCfop()
        {
            ServicoCfop servicoCfop = new ServicoCfop();
            var cfop = servicoCfop.ConsultePeloCodigoCfop(txtCodigoCfop.Text);

            if (cfop != null)
            {
                EditeCfop(cfop);
            }
            else
            {
                var codigoCfop = txtCodigoCfop.Text;

                LimpeFormulario();

                txtDescricao.Focus();

                txtCodigoCfop.Text = codigoCfop;
            }
        }

        private void PreenchaCboCfopConversao()
        {
            ServicoCfop servicoCfop = new ServicoCfop();

            var lista = servicoCfop.ConsulteListaAtiva();

            List<ObjetoDescricaoValor> listaObjetoDescricaoValor = new List<ObjetoDescricaoValor>();

            listaObjetoDescricaoValor.Add(null);

            foreach (var cfop in lista)
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor();
                objetoDescricaoValor.Descricao = cfop.Codigo + " - " + cfop.Descricao;
                objetoDescricaoValor.Valor = cfop.Id;

                listaObjetoDescricaoValor.Add(objetoDescricaoValor);
            }

            cboCfopConversao.Properties.DataSource = listaObjetoDescricaoValor;
            cboCfopConversao.Properties.ValueMember = "Valor";
            cboCfopConversao.Properties.DisplayMember = "Descricao";
        }

        #endregion
    }
}
