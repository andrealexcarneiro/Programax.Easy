using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.CartaCorrecaoServ;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.ObjetoDeNegocio;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using NFe.Servicos;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Report.RelatoriosDevExpress.Fiscal;

namespace Programax.Easy.View.Telas.Fiscal.CartasCorrecoes
{
    public partial class FormCadastroCartaCorrecao : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private NotaFiscal _notaFiscal;
        private DialogResult _resultado;
        private int _numeroSequencial;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroCartaCorrecao()
        {
            InitializeComponent();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            _resultado = DialogResult.Cancel;

            this.Close();
        }

        private void btnEnviarCCe_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            Action actionSalvar = () =>
            {
                CartaCorrecao cartaCorrecao = new CartaCorrecao();

                cartaCorrecao.Correcao = txtInformacoesComplementares.Text;
                cartaCorrecao.SequenciaEvento = _numeroSequencial;
                cartaCorrecao.NotaFiscal = _notaFiscal;

                //******BEGIN

                ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
                var notaFiscal = servicoNotaFiscal.Consulte(_notaFiscal.Id);

                ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);
                var configuracoesZeus = servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus((EnumModeloNotaFiscal)notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);
                
                var servicoNFe = new ServicosNFe(configuracoesZeus);
                var retornoEnvio = servicoNFe.RecepcaoEventoCartaCorrecao(notaFiscal.IdentificacaoNotaFiscal.NumeroNota,
                                                                                                                 cartaCorrecao.SequenciaEvento,
                                                                                                                 notaFiscal.InformacoesGeraisNotaFiscal.ChaveDeAcesso,
                                                                                                                 cartaCorrecao.Correcao.RemovaEspacosEmBrancoDoInicioEFim(),
                                                                                                                 notaFiscal.Emitente.CNPJ.RemoverCaracteresDeMascara());

                this.Cursor = Cursors.Default;

                if (retornoEnvio.Retorno.cStat == 128)
                    {
                    if (retornoEnvio.Retorno.retEvento[0].infEvento.cStat == 135)
                    {
                        cartaCorrecao.DataHoraEmissao = retornoEnvio.Retorno.retEvento[0].infEvento.dhRegEvento;
                        cartaCorrecao.NumeroProtocolo = retornoEnvio.Retorno.retEvento[0].infEvento.nProt;

                        ServicoCartaCorrecao servicoCartaCorrecao = new ServicoCartaCorrecao();
                        servicoCartaCorrecao.CadastreCartaCorrecao(cartaCorrecao);

                        RelatorioCartaCorrecao relatorioCartaCorrecao = new RelatorioCartaCorrecao(retornoEnvio.ProcEventosNFe[0], notaFiscal.IdentificacaoNotaFiscal.NumeroNota);
                            TratamentosDeTela.ExibirRelatorio(relatorioCartaCorrecao);

                        _resultado = DialogResult.OK;

                            this.Close();
                    }
                        else
                        {
                            throw new Exception(retornoEnvio.Retorno.retEvento[0].infEvento.xMotivo);
                        }
                    }
                    else
                    {
                        throw new Exception(retornoEnvio.Retorno.xMotivo);
                    }

                    this.Cursor = Cursors.Default;

                //****END


                //ServicoCartaCorrecao servicoCartaCorrecao = new ServicoCartaCorrecao();
                //    servicoCartaCorrecao.Cadastre(cartaCorrecao);

                //    _resultado = DialogResult.OK;

                //    this.Close();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
            this.Cursor = Cursors.Default;
        }

        private void txtInformacoesComplementares_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult EnvieCartaCorrecao(int notaId)
        {
            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
            _notaFiscal = servicoNotaFiscal.Consulte(notaId);

            if (_notaFiscal.ListaCartasCorrecoes.Count > 0)
            {
                _numeroSequencial = _notaFiscal.ListaCartasCorrecoes.Max(carta => carta.SequenciaEvento) + 1;
            }
            else
            {
                _numeroSequencial = 1;
            }

            this.AbrirTelaModal();

            return _resultado;
        }

        #endregion

        
    }
}
