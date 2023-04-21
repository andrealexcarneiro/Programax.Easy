using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.RamoAtividadeServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Cadastros.RamosAtividades
{
    public partial class FormCadastroRamoAtividade : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroRamoAtividade()
        {
            InitializeComponent();

            this.ActiveControl = txtId;

            this.NomeDaTela = "Cadastro de ramo de atividade";

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
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
                RamoAtividade ramoAtividade = new RamoAtividade();

                ramoAtividade.Id = txtId.Text.ToInt();
                ramoAtividade.Descricao = txtDescricao.Text;
                ramoAtividade.Status = cboStatus.EditValue.ToString();
                ramoAtividade.DataCadatro = txtDataCadastro.Text.ToDate();

                ServicoRamoAtividade servicoRamoAtividade = new ServicoRamoAtividade();

                if (ramoAtividade.Id == 0)
                {
                    servicoRamoAtividade.Cadastre(ramoAtividade);
                }
                else
                {
                    servicoRamoAtividade.Atualize(ramoAtividade);
                }

                txtId.Text = ramoAtividade.Id.ToString();

                PesquiseRamoAtividadePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPesquisaRamoAtividade formPesquisaDeRamoAtividades = new FormPesquisaRamoAtividade();

            var ramoAtividade = formPesquisaDeRamoAtividades.PesquiseUmRamoAtividade();

            if (ramoAtividade != null)
            {
                EditeRamoAtividade(ramoAtividade);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquiseRamoAtividadePeloId();
            }
        }

        private void FormCadastroRamoAtividade_Load(object sender, EventArgs e)
        {
            PreenchaOStatus();
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

        private void LimpeFormulario()
        {
            EditeRamoAtividade(null);
        }

        private void EditeRamoAtividade(RamoAtividade ramoAtividade, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (ramoAtividade != null)
            {
                txtId.Text = ramoAtividade.Id.ToString();
                txtDescricao.Text = ramoAtividade.Descricao;
                cboStatus.EditValue = ramoAtividade.Status;

                txtDataCadastro.Text = ramoAtividade.DataCadatro.ToString("dd/MM/yyyy");

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                cboStatus.EditValue = "A";

                txtId.Enabled = true;
                txtId.Focus();

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Ramo de Atividade não encontrada", "Ramo de Atividade não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void PesquiseRamoAtividadePeloId()
        {
            ServicoRamoAtividade servicoRamoAtividade = new ServicoRamoAtividade();
            var ramoAtividade = servicoRamoAtividade.Consulte(txtId.Text.ToInt());

            EditeRamoAtividade(ramoAtividade, exibirMensagemDeNaoEncontrado: true);
        }

        #endregion
    }
}
