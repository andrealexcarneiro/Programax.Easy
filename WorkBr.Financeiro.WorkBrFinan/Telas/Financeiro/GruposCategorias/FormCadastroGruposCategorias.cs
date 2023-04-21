using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.GruposCategoriasServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Cadastros.GruposCategorias
{
    public partial class FormCadastroGruposCategorias : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroGruposCategorias()
        {
            InitializeComponent();
            
            this.ActiveControl = txtId;

            this.NomeDaTela = "Cadastro de Grupos de Categorias Financeiras";
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
                GrupoCategoria grupo = new GrupoCategoria();

                grupo.Id = txtId.Text.ToInt();
                grupo.Descricao = txtDescricao.Text;
                grupo.DataCadastro = txtDataCadastro.Text.ToDate();
                grupo.Ativo = cboStatus.EditValue.ToString();

                ServicoGrupoCategoria servicoGrupo = new ServicoGrupoCategoria();

                if (grupo.Id == 0)
                {
                    servicoGrupo.Cadastre(grupo);
                }
                else
                {
                    servicoGrupo.Atualize(grupo);
                }

                txtId.Text = grupo.Id.ToString();
                PesquisePeloId();
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
            FormGruposCategoriasPesquisa formGruposCategoriasPesquisa = new FormGruposCategoriasPesquisa();

            var grupo = formGruposCategoriasPesquisa.ExibaPesquisaDeGrupo();

            if (grupo != null)
            {
                EditeGrupo(grupo);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void FormCadastroGruposCategorias_Load(object sender, EventArgs e)
        {
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

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
            txtId.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            cboStatus.EditValue = "A";

            txtId.Enabled = true;
            txtId.Focus();
        }

        private void EditeGrupo(GrupoCategoria grupo)
        {
            if (grupo != null)
            {
                txtId.Text = grupo.Id.ToString();
                txtDescricao.Text = grupo.Descricao;
                txtDataCadastro.Text = grupo.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = grupo.Ativo;

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                txtId.Focus();

                MessageBox.Show("Grupo não encontrada", "Grupo não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PesquisePeloId()
        {
            ServicoGrupoCategoria servicoGrupoCategorias = new ServicoGrupoCategoria();
            var grupo = servicoGrupoCategorias.Consulte(txtId.Text.ToInt());

            EditeGrupo(grupo);
        }

        #endregion
    }
}
