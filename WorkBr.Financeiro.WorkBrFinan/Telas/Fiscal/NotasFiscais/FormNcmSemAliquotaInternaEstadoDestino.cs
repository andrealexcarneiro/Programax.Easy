using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Servico.Fiscal.IcmsInterestadualServ;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.Fiscal.NotasFiscais
{
    public partial class FormNcmSemAliquotaInternaEstadoDestino : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Ncm> _listaNcms;
        private string _uf;
        private List<userControlPainelNcmAliquotaInterestadual> _listaUCNcm;

        private DialogResult _resultado;

        #endregion

        #region " CONSTRUTOR "

        public FormNcmSemAliquotaInternaEstadoDestino()
        {
            InitializeComponent();

            _listaUCNcm = new List<userControlPainelNcmAliquotaInterestadual>();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Conclua();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormNcmSemAliquotaInternaEstadoDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                Conclua();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

        #region " MÉTODOS PUBLICOS "

        public DialogResult InsiraAliquotasNcmEstadoDestino(List<Ncm> listaNcms, string uf)
        {
            _listaNcms = listaNcms;
            _uf = uf;

            ServicoEstado servicoEstado = new ServicoEstado();
            var estado = servicoEstado.Consulte(uf);

            lblInformacaoEstado.Text = "INFORME AS ALÍQUOTAS INTERNAS DOS SGUINTES NCMS PARA O ESTADO DE " + estado.Nome + "\n(CONSULTE SEU CONTADOR)";

            MontePaineisNcm();

            this.AbrirTelaModal();

            return _resultado;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void MontePaineisNcm()
        {
            foreach (var item in _listaNcms)
            {
                userControlPainelNcmAliquotaInterestadual controleNcm = new userControlPainelNcmAliquotaInterestadual();
                controleNcm.CodigoNcm = item.CodigoNcm;

                _listaUCNcm.Add(controleNcm);

                pnlAliquotasNcm.Controls.Add(controleNcm);
            }

            this.ActiveControl = _listaUCNcm.FirstOrDefault();

            pnlAliquotasNcm.Refresh();
        }

        private void Conclua()
        {
            Action actionSalvar = () =>
            {
                ServicoIcmsInterestadual servicoIcmsInterestadual = new ServicoIcmsInterestadual();

                List<IcmsInterestadual> listaIcmsInterestadual = servicoIcmsInterestadual.ConsulteListaPorNcms(_listaNcms);

                foreach (var item in _listaUCNcm)
                {
                    IcmsInterestadualEstado icmsInterestadualEstado = new IcmsInterestadualEstado();
                    icmsInterestadualEstado.AliquotaInterna = item.AliquotaInterna;
                    icmsInterestadualEstado.FCP = item.FCP;
                    icmsInterestadualEstado.UF = _uf;

                    var icmsInterestadual = listaIcmsInterestadual.FirstOrDefault(x => x.Ncm.CodigoNcm == item.CodigoNcm);

                    if (icmsInterestadual != null)
                    {
                        icmsInterestadual.ListaIcmsInterestadualEstado.Add(icmsInterestadualEstado);

                        servicoIcmsInterestadual.Atualize(icmsInterestadual);
                    }
                    else
                    {
                        icmsInterestadual = new IcmsInterestadual();
                        icmsInterestadual.Ncm = _listaNcms.FirstOrDefault(x => x.CodigoNcm == item.CodigoNcm);

                        icmsInterestadual.ListaIcmsInterestadualEstado.Add(icmsInterestadualEstado);

                        servicoIcmsInterestadual.Cadastre(icmsInterestadual);
                    }
                }

                _resultado = DialogResult.OK;
                this.Close();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, controleValidar: this);
        }

        #endregion
    }
}
