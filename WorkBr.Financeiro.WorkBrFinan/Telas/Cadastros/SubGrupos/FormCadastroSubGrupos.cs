using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Cadastros.Grupos;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using System.Drawing;
using System.Linq;

namespace Programax.Easy.View.Telas.Cadastros.SubGrupos
{
    public partial class FormCadastroSubGrupos : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroSubGrupos()
        {
            InitializeComponent();
            
            this.NomeDaTela = "Cadastro Subgrupo dos Produtos";

            TrateUsuarioNaoTemPermissaoCadastroGrupo();

            this.ActiveControl = txtId;
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
                SubGrupo subGrupo = new SubGrupo();

                subGrupo.Id = txtId.Text.ToInt();
                subGrupo.Descricao = txtDescricao.Text;
                subGrupo.DataCadastro = txtDataCadastro.Text.ToDate();
                subGrupo.Status = cboStatus.EditValue.ToString();

                if (cboGrupos.EditValue != null)
                {
                    subGrupo.Grupo = new Grupo { Id = cboGrupos.EditValue.ToInt() };
                }

                ServicoSubGrupo servicoSubGrupo = new ServicoSubGrupo();

                if (subGrupo.Id == 0)
                {
                    servicoSubGrupo.Cadastre(subGrupo);
                }
                else
                {
                    servicoSubGrupo.Atualize(subGrupo);
                }

                txtId.Text = subGrupo.Id.ToString();
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
            FormPesquisaSubGrupos formPesquisaSubGrupos = new FormPesquisaSubGrupos();

            var subGrupo = formPesquisaSubGrupos.PesquiseUmSubGrupo();

            if (subGrupo != null)
            {
                EditeSubGrupo(subGrupo);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void FormCadastroGrupo_Load(object sender, EventArgs e)
        {
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            PreenchaOStatus();
            PreenchaCboGrupos();
        }

        private void btnAtalhoGrupo_Click(object sender, EventArgs e)
        {
            ExibaCadastroCategoria();
        }

        #endregion

        #region " MÉTODOS PUBLICOS "

        public void ExibaCadastroSubGrupo(Grupo grupo)
        {
            cboGrupos.EditValue = grupo != null ? (int?)grupo.Id : null;
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

        private void PreenchaCboGrupos()
        {
            ServicoGrupo servicoGrupo = new ServicoGrupo();

            var grupos = servicoGrupo.ConsulteListaAtivos();

            grupos.Insert(0, null);

            cboGrupos.Properties.DataSource = grupos;
            cboGrupos.Properties.DisplayMember = "Descricao";
            cboGrupos.Properties.ValueMember = "Id";

            if (cboGrupos.EditValue != null)
            {
                if (!grupos.Exists(grupo => grupo != null && grupo.Id == cboGrupos.EditValue.ToInt()))
                {
                    cboGrupos.EditValue = null;
                }
            }
        }

        private void LimpeFormulario()
        {
            txtId.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            cboStatus.EditValue = "A";
            cboGrupos.EditValue = null;

            txtId.Enabled = true;
            txtId.Focus();
        }

        private void EditeSubGrupo(SubGrupo subGrupo)
        {
            if (subGrupo != null)
            {
                txtId.Text = subGrupo.Id.ToString();
                txtDescricao.Text = subGrupo.Descricao;
                txtDataCadastro.Text = subGrupo.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = subGrupo.Status;

                if (subGrupo.Grupo != null)
                {
                    cboGrupos.EditValue = subGrupo.Grupo.Id;
                }
                else
                {
                    cboGrupos.EditValue = null;
                }

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                txtId.Focus();

                MessageBox.Show("SubGrupo não encontrado", "SubGrupo não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ExibaCadastroCategoria()
        {
            FormCadastroGrupo formCadastroGrupo = new FormCadastroGrupo();
            formCadastroGrupo.ShowDialog();

            PreenchaCboGrupos();
        }

        private void TrateUsuarioNaoTemPermissaoCadastroGrupo()
        {
            var permissao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.GRUPODEPRODUTOS);

            if (!permissao.Alterar)
            {
                cboGrupos.Size = new Size(pnlGrupo.Width, cboGrupos.Size.Height);

                btnAtalhoGrupo.Visible = false;
            }
        }

        private void PesquisePeloId()
        {
            ServicoSubGrupo servicoSubGrupo = new ServicoSubGrupo();
            var grupo = servicoSubGrupo.Consulte(txtId.Text.ToInt());

            EditeSubGrupo(grupo);
        }

        #endregion
    }
}
