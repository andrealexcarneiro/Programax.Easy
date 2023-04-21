using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Fiscal.Ncms;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio;
using DevExpress.XtraEditors;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Fiscal.IcmsInterestadualServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.Cadastros.EstadoServ;

namespace Programax.Easy.View.Telas.Fiscal.FormsIcmsInterestadual
{
    public partial class FormCadastroIcmsInterestadual : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Ncm _ncm;
        private IcmsInterestadual _icmsInterestadual;

        private IcmsInterestadualEstado _icmsInterestadualEstadoEmEdicao;

        #endregion

        #region " CONSTRUTORES "

        public FormCadastroIcmsInterestadual()
        {
            InitializeComponent();

            PreenchaCboUF();

            this.ActiveControl = txtNcmId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void txtNcmId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNcmId.Text))
            {
                ServicoNcm servicoNcm = new ServicoNcm();

                var ncm = servicoNcm.ConsultePeloCodigoNcm(txtNcmId.Text);

                PreenchaNcm(ncm);
            }
            else
            {
                PreenchaNcm(null, false);
            }
        }

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            FormNcmPesquisa formNcmPesquisa = new FormNcmPesquisa();
            var ncm = formNcmPesquisa.ExibaPesquisaDeNcm();

            if (ncm != null)
            {
                PreenchaNcm(ncm);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                var icms = RetorneIcmsInterestadualEmEdicao();

                ServicoIcmsInterestadual servicoIcmsInterestadual = new ServicoIcmsInterestadual();

                if (icms.Id == 0)
                {
                    servicoIcmsInterestadual.Cadastre(icms);
                }
                else
                {
                    servicoIcmsInterestadual.Atualize(icms);
                }

                ServicoNcm servicoNcm = new ServicoNcm();
                var ncm = servicoNcm.ConsultePeloCodigoNcm(txtNcmId.Text);

                PreenchaNcm(ncm);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void gcAliquotas_DoubleClick(object sender, EventArgs e)
        {
            SelecioneIcmsEstado();
        }

        private void gcAliquotas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneIcmsEstado();
            }
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeCamposAliquotaInterna();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            InsiraOuAtualizeItem();
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir esta tributação?", "Exclusão", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                _icmsInterestadual.ListaIcmsInterestadualEstado.Remove(_icmsInterestadualEstadoEmEdicao);

                PreenchaGrid();
            }
        }

        private void txtAliquotaInterna_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsiraOuAtualizeItem();
            }
        }

        #endregion

        #region " MÉTODOS PRIVADOS "

        private void PreenchaCboUF()
        {
            ServicoEstado servicoEstado = new ServicoEstado();
            var estados = servicoEstado.ConsulteListaEstados();

            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            //string uf = empresa.DadosEmpresa.Endereco.Cidade.Estado.UF; NF 4.0

            //estados.Remove(estados.FirstOrDefault(x => x.UF == uf)); NF 4.0

            cboUF.Properties.DataSource = estados;
            cboUF.Properties.DisplayMember = "Nome";
            cboUF.Properties.ValueMember = "UF";
        }

        private void PreenchaNcm(Ncm ncm, bool manterFocusCasoNaoTenhaNcm = true)
        {
            if (ncm != null)
            {
                txtNcmId.Text = ncm.CodigoNcm.ToString();
                txtNcmDescricao.Text = ncm.Descricao;

                ServicoIcmsInterestadual servicoIcmsInterestadual = new ServicoIcmsInterestadual();
                _icmsInterestadual = servicoIcmsInterestadual.ConsultePorNcm(txtNcmId.Text);

                _icmsInterestadual = _icmsInterestadual ?? new IcmsInterestadual();

                PreenchaGrid();
            }
            else
            {
                _icmsInterestadual = null;

                txtNcmId.Text = string.Empty;
                txtNcmDescricao.Text = string.Empty;

                PreenchaGrid();

                if (manterFocusCasoNaoTenhaNcm)
                {
                    txtNcmId.Focus();
                }
            }

            _ncm = ncm;
        }

        private IcmsInterestadual RetorneIcmsInterestadualEmEdicao()
        {
            _icmsInterestadual.Ncm = _ncm;

            return _icmsInterestadual.CloneCompleto();
        }

        private void LimpeFormulario()
        {
            PreenchaNcm(null);
            LimpeCamposAliquotaInterna();
        }

        private void LimpeCamposAliquotaInterna()
        {
            EditeAliquotaInterna(null);
        }

        private void EditeAliquotaInterna(IcmsInterestadualEstado icms)
        {
            _icmsInterestadualEstadoEmEdicao = icms;

            if (icms != null)
            {
                cboUF.EditValue = icms.UF;
                txtFCP.Text = icms.FCP.ToString("0.00");
                txtAliquotaInterna.Text = icms.AliquotaInterna.ToString("0.00");

                btnExcluirItem.Visible = true;
                btnInserirAtualizarItem.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                cboUF.EditValue = null;
                txtFCP.Text = string.Empty;
                txtAliquotaInterna.Text = string.Empty;

                btnExcluirItem.Visible = false;
                btnInserirAtualizarItem.Image = Properties.Resources.icones2_19;
            }
        }

        private void PreenchaGrid()
        {
            GereIdFalsoParaCadaItem();

            gcAliquotas.DataSource = _icmsInterestadual != null ? _icmsInterestadual.ListaIcmsInterestadualEstado : null;
            gcAliquotas.RefreshDataSource();
        }

        private void SelecioneIcmsEstado()
        {
            if (_icmsInterestadual != null)
            {
                var itemDaLista = _icmsInterestadual.ListaIcmsInterestadualEstado.FirstOrDefault(item => item.UF == colunaId.View.GetFocusedRowCellValue(colunaId).ToString());

                EditeAliquotaInterna(itemDaLista);
            }
        }

        private void InsiraOuAtualizeItem()
        {
            if (_ncm == null)
            {
                MessageBox.Show("É obrigatório selecionar um NCM antes de incluir a alíquota interna.");

                return;
            }

            Action actionAtualizarIcms = () =>
            {
                IcmsInterestadualEstado icmsInterEstadualEstado = new IcmsInterestadualEstado();
                icmsInterEstadualEstado.AliquotaInterna = txtAliquotaInterna.Text.ToDouble();
                icmsInterEstadualEstado.FCP = txtFCP.Text.ToDouble();
                icmsInterEstadualEstado.UF = cboUF.EditValue.ToStringEmpty();

                icmsInterEstadualEstado.Id = _icmsInterestadual != null ? _icmsInterestadual.Id : 0;

                ServicoIcmsInterestadual servicoIcmsInterestadual = new ServicoIcmsInterestadual();
                servicoIcmsInterestadual.ValidaItemIcmsInterestadual(icmsInterEstadualEstado, _icmsInterestadual.ListaIcmsInterestadualEstado.ToList());

                if (_icmsInterestadualEstadoEmEdicao == null)
                {
                    _icmsInterestadual.ListaIcmsInterestadualEstado.Add(icmsInterEstadualEstado);
                }
                else
                {
                    _icmsInterestadualEstadoEmEdicao.UF = icmsInterEstadualEstado.UF;
                    _icmsInterestadualEstadoEmEdicao.FCP = icmsInterEstadualEstado.FCP;
                    _icmsInterestadualEstadoEmEdicao.AliquotaInterna = icmsInterEstadualEstado.AliquotaInterna;
                }

                LimpeCamposAliquotaInterna();
                PreenchaGrid();

                cboUF.Focus();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionAtualizarIcms, exibirMensagemDeSucesso: false);
        }

        private void GereIdFalsoParaCadaItem()
        {
            if (_icmsInterestadual != null)
            {
                for (int i = 0; i < _icmsInterestadual.ListaIcmsInterestadualEstado.Count; i++)
                {
                    _icmsInterestadual.ListaIcmsInterestadualEstado[i].Id = i + 1;
                }
            }
        }

        #endregion
    }
}
