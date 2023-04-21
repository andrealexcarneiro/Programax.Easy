using Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Programax.Easy.View.Telas.Fiscal.ConfiguracoesNfe
{
    public partial class FormConfiguracaoNfe : FormularioPadrao
    {
        public FormConfiguracaoNfe()
        {
            InitializeComponent();

            PreenchaCboFormatoImpressaoDanfe();
            PreenchaCboTipoAmbiente();
            PreenchaCboFormatoNFCeImpressaoDanfe();
            PreenchaCboTipoAmbienteNFCe();

            CarregueConfiguracoesNfe();

            this.ActiveControl = txtAliquotaSimplesNacional;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }
        
        private void btnPesquisaCertificado_Click(object sender, EventArgs e)
        {
            try
            {
                ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();

                var cert = servicoConfiguracaoNfe.ListareObterDoRepositorio();

                txtNumeroSerieCertificado.Text = cert.SerialNumber;
            }
            catch (Exception)
            {
                //Quando cancela a escolha do certificado ele da um erro, por isso existe este tratamento
            }
        }

        private void PreenchaCboFormatoImpressaoDanfe()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumFormatoImpressaoDanfe>();

            List<ObjetoDescricaoValor> listaDanfe = new List<ObjetoDescricaoValor>();
            
            foreach (var item in lista)
            {
                if ((EnumFormatoImpressaoDanfe)item.Valor == RetorneFormatoImpressaoNFe((EnumFormatoImpressaoDanfe)item.Valor))
                {
                    ObjetoDescricaoValor itemAdiciona = new ObjetoDescricaoValor();
                    itemAdiciona = item;
                    listaDanfe.Add(itemAdiciona);
                }
            }
            
            cboFormatoImpressaoDanfe.Properties.DataSource = listaDanfe;
            cboFormatoImpressaoDanfe.Properties.DisplayMember = "Descricao";
            cboFormatoImpressaoDanfe.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboFormatoNFCeImpressaoDanfe()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumFormatoImpressaoDanfe>();

            List<ObjetoDescricaoValor> listaNFCe = new List<ObjetoDescricaoValor>();

            foreach (var item in lista)
            {
                if((EnumFormatoImpressaoDanfe)item.Valor == RetorneFormatoImpressaoNFCe((EnumFormatoImpressaoDanfe)item.Valor))
                {
                    ObjetoDescricaoValor itemAdiciona = new ObjetoDescricaoValor();
                    itemAdiciona = item;
                    listaNFCe.Add(itemAdiciona);
                }
            }

            cboFormatoImpressaoDanfeNFCe.Properties.DataSource = listaNFCe;
            cboFormatoImpressaoDanfeNFCe.Properties.DisplayMember = "Descricao";
            cboFormatoImpressaoDanfeNFCe.Properties.ValueMember = "Valor";
        }

        private EnumFormatoImpressaoDanfe RetorneFormatoImpressaoNFe(EnumFormatoImpressaoDanfe FormatoNFe)
        {
            switch (FormatoNFe)
                {
                case EnumFormatoImpressaoDanfe.DANFENORMALRETRATO:
                    return EnumFormatoImpressaoDanfe.DANFENORMALRETRATO;

                case EnumFormatoImpressaoDanfe.DANFENORMALPAISAGEM:
                    return EnumFormatoImpressaoDanfe.DANFENORMALPAISAGEM;

                case EnumFormatoImpressaoDanfe.DANFESIMPLIFICADO:
                    return EnumFormatoImpressaoDanfe.DANFESIMPLIFICADO;

                }

            return EnumFormatoImpressaoDanfe.DANFENORMALRETRATO;

        }

        private EnumFormatoImpressaoDanfe RetorneFormatoImpressaoNFCe(EnumFormatoImpressaoDanfe FormatoNFe)
        {
            switch (FormatoNFe)
            {
                case EnumFormatoImpressaoDanfe.DANFENFCE:
                    return EnumFormatoImpressaoDanfe.DANFENFCE;
                case EnumFormatoImpressaoDanfe.DANFENFCEEMMENSAGEMELETRONICA:
                    return EnumFormatoImpressaoDanfe.DANFENFCEEMMENSAGEMELETRONICA;
            }

            return EnumFormatoImpressaoDanfe.DANFENFCE;
        }

        private void PreenchaCboTipoAmbiente()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoAmbiente>();

            cboTipoAmbiente.Properties.DataSource = lista;
            cboTipoAmbiente.Properties.DisplayMember = "Descricao";
            cboTipoAmbiente.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboTipoAmbienteNFCe()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoAmbiente>();

            cboTipoAmbienteNFCe.Properties.DataSource = lista;
            cboTipoAmbienteNFCe.Properties.DisplayMember = "Descricao";
            cboTipoAmbienteNFCe.Properties.ValueMember = "Valor";
        }

        private void CarregueConfiguracoesNfe()
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();
            var configuracaoNfe = servicoConfiguracaoNfe.ConsulteConfiguracoesNfe();

            if (configuracaoNfe != null)
            {   
                txtAliquotaSimplesNacional.Text = configuracaoNfe.AliquotaSimplesNacional != null ? configuracaoNfe.AliquotaSimplesNacional.Value.ToString("#.00") : string.Empty;
                txtNumeroProximaNota.Text = configuracaoNfe.NumeroNota.ToString();
                txtNumeroSerieNota.Text = configuracaoNfe.Serie.ToString();
                cboFormatoImpressaoDanfe.EditValue = configuracaoNfe.FormatoImpressaoDanfe;
                cboTipoAmbiente.EditValue = configuracaoNfe.TipoAmbiente;
                txtNumeroSerieCertificado.Text = configuracaoNfe.NumeroSerieCertificado;

                if (configuracaoNfe.PadraoModeloNF == EnumModeloNotaFiscal.NFE)
                    rdbNFe.Checked = true;
                else
                    rdbNFCe.Checked = true;
            }
            else
            {
                txtAliquotaSimplesNacional.Text = string.Empty;
                txtNumeroProximaNota.Text = string.Empty;
                txtNumeroSerieNota.Text = string.Empty;
                cboFormatoImpressaoDanfe.EditValue = string.Empty;
                cboTipoAmbiente.EditValue = string.Empty;
                txtNumeroSerieCertificado.Text = string.Empty;
            }
        }

        private void CarregueConfiguracoesNFCe()
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();
            var configuracaoNfe = servicoConfiguracaoNfe.ConsulteConfiguracoesNfe(EnumModeloNotaFiscal.NFCE);
            
            if (configuracaoNfe != null)
            {
                txtAliquotaSimplesNacionalNFCe.Text = configuracaoNfe.AliquotaSimplesNacional != null ? configuracaoNfe.AliquotaSimplesNacional.Value.ToString("#.00") : string.Empty;
                txtNumeroProximaNotaNFCe.Text = configuracaoNfe.NumeroNota.ToString();
                txtNumeroSerieNotaNFCe.Text = configuracaoNfe.Serie.ToString();
                cboFormatoImpressaoDanfeNFCe.EditValue = configuracaoNfe.FormatoImpressaoDanfe;
                cboTipoAmbienteNFCe.EditValue = configuracaoNfe.TipoAmbiente;
                txtNumeroSerieCertificadoNFCe.Text = configuracaoNfe.NumeroSerieCertificado;

                if (configuracaoNfe.PadraoModeloNF == EnumModeloNotaFiscal.NFE)
                    rdbNFe.Checked = true;
                else
                    rdbNFCe.Checked = true;
            }
            else
            {
                txtAliquotaSimplesNacionalNFCe.Text = string.Empty;
                txtNumeroProximaNotaNFCe.Text = string.Empty;
                txtNumeroSerieNotaNFCe.Text = string.Empty;
                cboFormatoImpressaoDanfeNFCe.EditValue = string.Empty;
                cboTipoAmbienteNFCe.EditValue = string.Empty;
                txtNumeroSerieCertificadoNFCe.Text = string.Empty;
            }
        }
        
        private void btnGravar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {                
                ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();

                if (tbcConfiguracoesNfe.SelectedTab == tbpNFe)
                {
                    var configuracaoNfe = servicoConfiguracaoNfe.ConsulteConfiguracoesNfe();

                    configuracaoNfe = configuracaoNfe ?? new ConfiguracaoNfe();

                    configuracaoNfe.AliquotaSimplesNacional = txtAliquotaSimplesNacional.Text.ToDouble();                    
                    configuracaoNfe.FormatoImpressaoDanfe = (EnumFormatoImpressaoDanfe)cboFormatoImpressaoDanfe.EditValue;
                    configuracaoNfe.NumeroNota = txtNumeroProximaNota.Text.ToInt();
                    configuracaoNfe.NumeroSerieCertificado = txtNumeroSerieCertificado.Text;
                    configuracaoNfe.Serie = txtNumeroSerieNota.Text.ToInt();
                    configuracaoNfe.TipoAmbiente = (EnumTipoAmbiente)cboTipoAmbiente.EditValue;
                    configuracaoNfe.PadraoModeloNF = rdbNFe.Checked ? EnumModeloNotaFiscal.NFE : EnumModeloNotaFiscal.NFCE;
                    configuracaoNfe.ModeloNF = EnumModeloNotaFiscal.NFE;

                    servicoConfiguracaoNfe.Atualize(configuracaoNfe);
                }
                else
                {
                    var configuracaoNfe = servicoConfiguracaoNfe.ConsulteConfiguracoesNfe(EnumModeloNotaFiscal.NFCE);

                    configuracaoNfe = configuracaoNfe ?? new ConfiguracaoNfe();

                    configuracaoNfe.AliquotaSimplesNacional = txtAliquotaSimplesNacionalNFCe.Text.ToDouble();

                    configuracaoNfe.FormatoImpressaoDanfe = (EnumFormatoImpressaoDanfe)cboFormatoImpressaoDanfeNFCe.EditValue;
                    configuracaoNfe.NumeroNota = txtNumeroProximaNotaNFCe.Text.ToInt();
                    configuracaoNfe.NumeroSerieCertificado = txtNumeroSerieCertificadoNFCe.Text;
                    configuracaoNfe.Serie = txtNumeroSerieNota.Text.ToInt();
                    configuracaoNfe.TipoAmbiente = (EnumTipoAmbiente)cboTipoAmbienteNFCe.EditValue;
                    configuracaoNfe.PadraoModeloNF = rdbNFe.Checked ? EnumModeloNotaFiscal.NFE : EnumModeloNotaFiscal.NFCE;
                    configuracaoNfe.ModeloNF = EnumModeloNotaFiscal.NFCE;

                    servicoConfiguracaoNfe.Atualize(configuracaoNfe);
                }
                                                
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }
        
        private void tbcConfiguracoesNFe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcConfiguracoesNfe.SelectedTab == tbpNFe)
            {
                tbcConfiguracoesNfe.TabPages[0].Font = new Font(tbcConfiguracoesNfe.TabPages[0].Font, FontStyle.Bold);
                tbcConfiguracoesNfe.TabPages[1].Font = new Font(tbcConfiguracoesNfe.TabPages[1].Font, FontStyle.Regular);
                CarregueConfiguracoesNfe();
            }
            else
            {
                tbcConfiguracoesNfe.TabPages[1].Font = new Font(tbcConfiguracoesNfe.TabPages[1].Font, FontStyle.Bold);
                tbcConfiguracoesNfe.TabPages[0].Font = new Font(tbcConfiguracoesNfe.TabPages[0].Font, FontStyle.Regular);
                CarregueConfiguracoesNFCe();
            }
                
        }

        private void btnPesquisaCertificadoNFCe_Click(object sender, EventArgs e)
        {
            try
            {
                ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();

                var cert = servicoConfiguracaoNfe.ListareObterDoRepositorio();

                txtNumeroSerieCertificadoNFCe.Text = cert.SerialNumber;
            }
            catch (Exception)
            {
                //Quando cancela a escolha do certificado ele da um erro, por isso existe este tratamento
            }
        }
    }
    }
