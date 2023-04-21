using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NFe.Servicos;
using NFe.Classes;

using NFe.Danfe.Fast.NFCe;
using NFe.Servicos.Retorno;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Fiscal.CfopServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using System.IO;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using System.Drawing;
using Programax.Easy.Servico.Fiscal.IcmsInterestadualServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.View.Telas.Fiscal.CartasCorrecoes;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using NFe.Utils.NFe;

namespace Programax.Easy.View.Telas.Fiscal.NotasFiscais
{
    public partial class FormCadastroNotaFiscal : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Cfop> _listaCfops = null;
        private List<Ncm> _listaNcm = null;

        private DuplicataNotaFiscal _duplicataEmEdicao = null;
        private string _cepDestinatarioAtual;

        private string _ufDestinatario;
        private string _codigoMunicipioDestinatario;

        private NotaFiscal _notaFiscalEmEdicao;
        private Thread _threadFormAviso;
        private FormAvisoGerandoEEnviandoNfe _formAvisoGerandoEEnviandoNfe;
        private bool _exibaFormAviso;
        private bool _EhRejeicao;

        private ServicoNcm _servicoNcm;
        private ServicoCfop _servicoCfop;
        private List<NotaFiscalReferenciada> _notasFiscaisReferenciadas;
        private EnumModeloNotaFiscal _modeloEmissaoFiscal;
        private bool _notaFicalComDesconto;
        private EnumStatusNotaFiscal _enviarStatusNotaFiscal;

        private static readonly char[] s_Diacritics = GetDiacritics();

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroNotaFiscal()
        {
            InitializeComponent();

            _servicoCfop = new ServicoCfop();
            _servicoNcm = new ServicoNcm();
            _notasFiscaisReferenciadas = new List<NotaFiscalReferenciada>();

            Control.CheckForIllegalCrossThreadCalls = false;

            EditeItem(null);
        }

        #endregion

        #region " EVENTOS CONTROLES "

        #region " GUIA GERAL "

        private void txtObservacoesDaNotaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
            }
        }

        private void txtObservacoesDaNotaFiscal_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue.ToString().IndexOf('\n') > -1)
            {
                txtObservacoesDaNotaFiscal.Text = e.NewValue.ToString().Replace("\r\n", ", ");
                txtObservacoesDaNotaFiscal.Text = e.NewValue.ToString().Replace("\n", ", ");
            }

        }

        #endregion

        #region " GUIA ITENS "

        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            SelecioneItem();
        }

        private void gcItens_KeyUp(object sender, KeyEventArgs e)
        {
            SelecioneItem();
        }

        private void gcItens_Click(object sender, EventArgs e)
        {
            SelecioneItem();
        }

        #endregion

        #region " GUIA FINANCEIRO "

        private void btnAdicionarFinanceiro_Click(object sender, EventArgs e)
        {
            InsiraOuAtualizeParcelaFinanceiro();
        }

        private void btnCancelarFinanceiro_Click(object sender, EventArgs e)
        {
            LimpeCamposFinanceiro();
        }

        private void gcParcelasFinanceiro_DoubleClick(object sender, EventArgs e)
        {
            var duplicataDaLista = _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas.FirstOrDefault(financeiro => financeiro.Id == colunaIdFinanceiro.View.GetFocusedRowCellValue(colunaIdFinanceiro).ToInt());

            EditeDuplicataFinanceiro(duplicataDaLista);
        }

        private void gcParcelasFinanceiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var duplicataDaLista = _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas.ToList().FirstOrDefault(financeiro => financeiro.Id == colunaIdFinanceiro.View.GetFocusedRowCellValue(colunaIdFinanceiro).ToInt());

                EditeDuplicataFinanceiro(duplicataDaLista);
            }
        }

        private void txtValorDuplicata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsiraOuAtualizeParcelaFinanceiro();
            }
        }

        #endregion

        #region " BARRA DE OPERAÇÕES "

        private void btnSalvarEnviar_Click(object sender, EventArgs e)
        {
            SalveEEnvieNota();
        }

        private void btnImprimirDanfe_Click(object sender, EventArgs e)
        {
            ImprimirNota();
        }


        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnEmitirCartaCorrecao_Click(object sender, EventArgs e)
        {
            FormCadastroCartaCorrecao formCadastroCartaCorrecao = new FormCadastroCartaCorrecao();
            var resultado = formCadastroCartaCorrecao.EnvieCartaCorrecao(_notaFiscalEmEdicao.Id);

            if (resultado == DialogResult.OK)
            {
                ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
                _notaFiscalEmEdicao = servicoNotaFiscal.Consulte(_notaFiscalEmEdicao.Id);

                PreenchaNotaFiscalEmEdicao();
            }
        }

        private void btnSalvarXml_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            var resultado = folderBrowserDialog.ShowDialog();

            if (resultado != DialogResult.OK)
            {
                return;
            }

            string diretorio = folderBrowserDialog.SelectedPath;

            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();
            servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus((EnumModeloNotaFiscal)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

            ConversorNotaFiscalAkilParaZeus conversorNotaFiscalAkilParaZeus = new ConversorNotaFiscalAkilParaZeus();
            var notaFiscalZeus = conversorNotaFiscalAkilParaZeus.ConvertaNotaAutorizadaAkilParaZeus(_notaFiscalEmEdicao);

            notaFiscalZeus.NFe.Assina();

            string nomeNotaFiscal = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.ChaveDeAcesso + ".xml";
            notaFiscalZeus.SalvarArquivoXml(diretorio + @"\" + nomeNotaFiscal);
        }

        #endregion

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void EditeVwNotaDocumento(NotaFiscal vwNotaDocumento)
        {
            this.Show();

            if (vwNotaDocumento.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.PEDIDODEVENDAS && (
                vwNotaDocumento.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.DISPONIVEL))
            {
                EditePedidoDeVenda(vwNotaDocumento);                
            }
            else
            {
                if ((vwNotaDocumento.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA
                    && vwNotaDocumento.InformacoesDocumentoOrigemNotaFiscal.Origem != EnumTipoDocumento.OUTRASSAIDAS) ||
                        (vwNotaDocumento.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.PROCESSANDO
                        && vwNotaDocumento.InformacoesDocumentoOrigemNotaFiscal.Origem != EnumTipoDocumento.OUTRASSAIDAS))
                {
                    _enviarStatusNotaFiscal = vwNotaDocumento.InformacoesGeraisNotaFiscal.Status;
                    _modeloEmissaoFiscal = (EnumModeloNotaFiscal)vwNotaDocumento.IdentificacaoNotaFiscal.ModeloDocumentoFiscal;
                    EditeNotaFiscalRejeitadaDoPedido(vwNotaDocumento);                   
                }
                else
                    EditeNotaFiscal(vwNotaDocumento);
            }
        }

        public void EditePedidoDeVendaId(int idPedidoDeVenda)
        {
            this.Show();

            EditePedidoDeVenda(idPedidoDeVenda);
        }

        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] > 255)
                    sb.Append(text[i]);
                else
                    sb.Append(s_Diacritics[text[i]]);
            }

            return sb.ToString();
        }

        private static char[] GetDiacritics()
        {
            char[] accents = new char[256];

            for (int i = 0; i < 256; i++)
                accents[i] = (char)i;

            accents[(byte)'á'] = accents[(byte)'à'] = accents[(byte)'ã'] = accents[(byte)'â'] = accents[(byte)'ä'] = 'a';
            accents[(byte)'Á'] = accents[(byte)'À'] = accents[(byte)'Ã'] = accents[(byte)'Â'] = accents[(byte)'Ä'] = 'A';

            accents[(byte)'é'] = accents[(byte)'è'] = accents[(byte)'ê'] = accents[(byte)'ë'] = 'e';
            accents[(byte)'É'] = accents[(byte)'È'] = accents[(byte)'Ê'] = accents[(byte)'Ë'] = 'E';

            accents[(byte)'í'] = accents[(byte)'ì'] = accents[(byte)'î'] = accents[(byte)'ï'] = 'i';
            accents[(byte)'Í'] = accents[(byte)'Ì'] = accents[(byte)'Î'] = accents[(byte)'Ï'] = 'I';

            accents[(byte)'ó'] = accents[(byte)'ò'] = accents[(byte)'ô'] = accents[(byte)'õ'] = accents[(byte)'ö'] = 'o';
            accents[(byte)'Ó'] = accents[(byte)'Ò'] = accents[(byte)'Ô'] = accents[(byte)'Õ'] = accents[(byte)'Ö'] = 'O';

            accents[(byte)'ú'] = accents[(byte)'ù'] = accents[(byte)'û'] = accents[(byte)'ü'] = 'u';
            accents[(byte)'Ú'] = accents[(byte)'Ù'] = accents[(byte)'Û'] = accents[(byte)'Ü'] = 'U';

            accents[(byte)'ç'] = 'c';
            accents[(byte)'Ç'] = 'C';

            accents[(byte)'ñ'] = 'n';
            accents[(byte)'Ñ'] = 'N';

            accents[(byte)'ÿ'] = accents[(byte)'ý'] = 'y';
            accents[(byte)'Ý'] = 'Y';


            return accents;
        }

        private static string RemoveSpecialCharacters(string text)
        {
            string ret = text;

            return ret.Replace("\r\n", " ");

        }

        public bool ARejeicaoNotaEhDuplicao(NotaFiscal notaFiscal)
        {
            if (notaFiscal.InformacoesGeraisNotaFiscal.MensagemDeErro != null)
            {
                if (notaFiscal.InformacoesGeraisNotaFiscal.MensagemDeErro.Contains("204") ||
                    notaFiscal.InformacoesGeraisNotaFiscal.MensagemDeErro.Contains("539"))
                    return true;
            }

            return false;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " MÉTODOS RELACIONADOS AS PARCELAS FINANCEIRO "

        private void InsiraOuAtualizeParcelaFinanceiro()
        {
            Action actionInserirItem = () =>
            {
                DuplicataNotaFiscal duplicataNotaFiscal = new DuplicataNotaFiscal();

                duplicataNotaFiscal.DataVencimento = txtDataVencimentoParcela.Text.ToDate();
                duplicataNotaFiscal.NumeroDuplicata = txtNumeroDuplicata.Text;
                duplicataNotaFiscal.ValorDuplicata = txtValorDuplicata.Text.ToDouble();

                //ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                //servicoEntradaMercadoria.ValideFinanceiro(duplicataNotaFiscal);

                if (_duplicataEmEdicao != null)
                {
                    int indexItem = _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas.IndexOf(_duplicataEmEdicao);

                    _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas.Remove(_duplicataEmEdicao);

                    _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas.Insert(indexItem, duplicataNotaFiscal);
                }
                else
                {
                    _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas.Add(duplicataNotaFiscal);
                }

                LimpeCamposFinanceiro();
                GereIdParaCadaDuplicataFinanceiro();
                PreenchaGridFinanceiro();
            };

            string mensagemDeSucesso = "Parcela inserida com sucesso.";
            string tituloMensagemDeSucesso = "Parcela inserida.";
            string tituloMensagemDeErro = "Inconsistências ao inserir parcela.";

            if (_duplicataEmEdicao != null)
            {
                mensagemDeSucesso = "Parcela atualizada com sucesso.";
                tituloMensagemDeSucesso = "Parcela atualizada.";
                tituloMensagemDeErro = "Inconsistências ao atualizar parcela.";
            }

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirItem, mensagemDeSucesso, tituloMensagemDeSucesso, tituloMensagemDeErro);
        }

        private void LimpeCamposFinanceiro()
        {
            EditeDuplicataFinanceiro(null);
        }

        private void EditeDuplicataFinanceiro(DuplicataNotaFiscal duplicataNotaFiscal)
        {
            _duplicataEmEdicao = duplicataNotaFiscal;

            if (duplicataNotaFiscal != null)
            {
                txtParcela.Text = duplicataNotaFiscal.Parcela;
                txtNumeroDuplicata.Text = duplicataNotaFiscal.NumeroDuplicata;
                txtDataVencimentoParcela.Text = duplicataNotaFiscal.DataVencimento.ToString("dd/MM/yyyy");
                txtValorDuplicata.Text = duplicataNotaFiscal.ValorDuplicata.ToString("#,##0.00");

                btnAdicionarFinanceiro.Enabled = true;
                btnAdicionarFinanceiro.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                txtParcela.Text = string.Empty;

                txtNumeroDuplicata.Text = string.Empty;
                txtDataVencimentoParcela.Text = string.Empty;
                txtValorDuplicata.Text = string.Empty;

                btnAdicionarFinanceiro.Enabled = false;
            }
        }

        private void GereIdParaCadaDuplicataFinanceiro()
        {
            int quantidadeParcelas = _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas.Count;

            for (int i = 0; i < quantidadeParcelas; i++)
            {
                var financeiro = _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas[i];

                financeiro.Id = i + 1;
                financeiro.Parcela = (i + 1) + "/" + quantidadeParcelas;
            }
        }

        private void PreenchaGridFinanceiro()
        {
            List<DuplicataGrid> listaDuplicatasGrid = new List<DuplicataGrid>();

            foreach (var financeiro in _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas)
            {
                DuplicataGrid duplicataGrid = new DuplicataGrid();

                duplicataGrid.Id = financeiro.Id;
                duplicataGrid.DataVencimento = financeiro.DataVencimento.ToString("dd/MM/yyyy");
                duplicataGrid.NumeroDuplicata = financeiro.NumeroDuplicata;
                duplicataGrid.Parcela = financeiro.Parcela;
                duplicataGrid.Valor = financeiro.ValorDuplicata.ToString("0.00");

                listaDuplicatasGrid.Add(duplicataGrid);
            }

            gcParcelasFinanceiro.DataSource = listaDuplicatasGrid;
            gcParcelasFinanceiro.RefreshDataSource();
        }

        #endregion

        #region " MÉTODOS RELACIONADOS AOS ITENS "

        private void SelecioneItem()
        {
            var itemDaLista = _notaFiscalEmEdicao.ListaItens.ToList().FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

            EditeItem(itemDaLista);
        }

        private void AtualizeGridItens()
        {
            GeraIdParaCadaItem();

            List<ItemGrid> listaItensGrid = new List<ItemGrid>();

            foreach (var item in _notaFiscalEmEdicao.ListaItens)
            {
                ItemGrid itemGrid = new ItemGrid();

                var cfop = _listaCfops.FirstOrDefault(x => x.Codigo == item.Cfop.ToString());

                if (string.IsNullOrEmpty(item.NomeProduto))
                {
                    item.NomeProduto = item.Produto.DadosGerais.Descricao;
                }
                if (string.IsNullOrEmpty(item.UnidadeProduto))
                {
                    item.UnidadeProduto = item.Produto.DadosGerais.Unidade.Abreviacao;
                }

                itemGrid.Id = item.Id;
                itemGrid.IdProduto = item.Produto.Id;
                itemGrid.Descricao = item.NomeProduto;
                itemGrid.Cfop = cfop != null ? cfop.Codigo + " - " + cfop.Descricao : string.Empty;
                itemGrid.CstCsosn = item.Impostos.Icms.CstCsosn.Descricao();
                itemGrid.Desconto = item.ValorDesconto.GetValueOrDefault().ToString("#0.00");
                itemGrid.Frete = item.ValorFrete.GetValueOrDefault().ToString("#0.00");
                itemGrid.Quantidade = item.Quantidade.ToString();
                itemGrid.Unidade = item.UnidadeProduto;
                itemGrid.ValorTotal = item.ValorTotal.ToString("#0.00");
                itemGrid.ValorUnitario = item.ValorUnitario.ToString("#0.00######");

                listaItensGrid.Add(itemGrid);
            }

            gcItens.DataSource = listaItensGrid;
            gcItens.RefreshDataSource();
        }

        private void EditeItem(ItemNotaFiscal itemNotaFiscal)
        {
            if (itemNotaFiscal != null)
            {
                txtIdProduto.Text = itemNotaFiscal.Produto.Id.ToString();
                txtCodigoDeBarrasProduto.Text = itemNotaFiscal.CodigoBarrasProduto;
                txtItemDescricaoProduto.Text = itemNotaFiscal.NomeProduto;
                txtUnidade.Text = itemNotaFiscal.UnidadeProduto;
                txtNcm.Text = itemNotaFiscal.Ncm;
                
                var cfop = _listaCfops.FirstOrDefault(x => x.Codigo == itemNotaFiscal.Cfop.ToString());
                if (cfop != null)
                {
                    txtCfop.Text = cfop.Codigo + " - " + cfop.Descricao;
                }

                var ncm = _listaNcm.FirstOrDefault(x => x.CodigoNcm == itemNotaFiscal.Ncm);
                if (ncm != null)
                {
                    txtNcm.Text = ncm.CodigoNcm + " - " + ncm.Descricao;
                }

                txtValorUnitario.Text = itemNotaFiscal.ValorUnitario.ToString("#0.00######");
                txtQuantidade.Text = itemNotaFiscal.Quantidade.ToString();
                txtValorDesconto.Text = itemNotaFiscal.ValorDesconto != null ? itemNotaFiscal.ValorDesconto.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorFreteItem.Text = itemNotaFiscal.ValorFrete != null ? itemNotaFiscal.ValorFrete.GetValueOrDefault().ToString("#0.00") : string.Empty;

                txtValorTotal.Text = itemNotaFiscal.ValorTotal.ToString("0.00");

                if (itemNotaFiscal.Impostos != null)
                {
                    PreenchaCamposIcms(itemNotaFiscal.Impostos.Icms);
                    PreenchaCamposIpi(itemNotaFiscal.Impostos.Ipi);
                }
                else
                {
                    PreenchaCamposIcms(null);
                    PreenchaCamposIpi(null);
                }
            }
            else
            {
                txtIdProduto.Text = string.Empty;
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtItemDescricaoProduto.Text = string.Empty;
                txtUnidade.Text = string.Empty;
                txtNcm.Text = string.Empty;

                txtCfop.Text = string.Empty;
                txtNcm.Text = string.Empty;

                txtValorUnitario.Text = string.Empty;
                txtQuantidade.Text = string.Empty;
                txtValorDesconto.Text = string.Empty;
                txtValorFreteItem.Text = string.Empty;

                txtValorTotal.Text = string.Empty;

                PreenchaCamposIcms(null);
                PreenchaCamposIpi(null);
            }
        }

        private void PreenchaCamposIpi(IpiNotaFiscal ipi)
        {
            if (ipi != null)
            {
                txtPercentualIpi.Text = ipi.AliquotaIpi != null ? ipi.AliquotaIpi.Value.ToString("#0.00") : string.Empty;
                txtValorIpi.Text = ipi.ValorIpi != null ? ipi.ValorIpi.Value.ToString("#0.00") : string.Empty;
            }
            else
            {
                txtPercentualIpi.Text = string.Empty;
                txtValorIpi.Text = string.Empty;
            }
        }

        private void PreenchaCamposIcms(IcmsNotaFiscal icms)
        {
            if (icms != null)
            {
                txtOrigemProduto.Text = icms.Origem.Descricao().ToUpper();
                txtCstCsosn.Text = icms.CstCsosn.Descricao().ToUpper();

                txtAliquotaSimplesNacional.Text = icms.AliquotaSimplesNacional != null ? icms.AliquotaSimplesNacional.Value.ToString("#0.00") : string.Empty;
                txtValorIcmsSimplesNacional.Text = icms.ValorIcmsSimplesNacional != null ? icms.ValorIcmsSimplesNacional.Value.ToString("#,##0.00") : string.Empty;

                txtBaseIcms.Text = icms.BaseCalculoIcms != null ? icms.BaseCalculoIcms.Value.ToString("#,##0.00") : string.Empty;
                txtPercentualReducaoIcms.Text = icms.AliquotaReducaoIcms != null ? icms.AliquotaReducaoIcms.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtPercentualIcms.Text = icms.AliquotaIcms != null ? icms.AliquotaIcms.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorIcms.Text = icms.ValorIcms != null ? icms.ValorIcms.GetValueOrDefault().ToString("#,##0.00") : string.Empty;

                txtMotivoDesoneracaoIcmsProduto.Text = icms.MotivoDesoneracaoProduto != null ? icms.MotivoDesoneracaoProduto.Value.Descricao() : string.Empty;
                txtIcmsDesoneracaoProduto.Text = icms.ValorDesoneracaoProduto != null ? icms.ValorDesoneracaoProduto.Value.ToString("#,##0.00") : string.Empty;

                txtBaseIcmsSt.Text = icms.BaseIcmsSubstituicaoTributaria != null ? icms.BaseIcmsSubstituicaoTributaria.GetValueOrDefault().ToString("#,##0.00") : string.Empty;
                txtPercentualIVA.Text = icms.AliquotaIva != null ? icms.AliquotaIva.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text = icms.AliquotaReducaoIcmsSubstituicaoTributaria != null ? icms.AliquotaReducaoIcmsSubstituicaoTributaria.Value.ToString("0.00") : string.Empty;
                txtAliquotaSt.Text = icms.AliquotaSubstituicaoTributaria != null ? icms.AliquotaSubstituicaoTributaria.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorIcmsSt.Text = icms.ValorSubstituicaoTributaria != null ? icms.ValorSubstituicaoTributaria.GetValueOrDefault().ToString("#,##0.00") : string.Empty;
            }
            else
            {
                txtOrigemProduto.Text = string.Empty;
                txtCstCsosn.Text = string.Empty;

                txtAliquotaSimplesNacional.Text = string.Empty;
                txtValorIcmsSimplesNacional.Text = string.Empty;

                txtBaseIcms.Text = string.Empty;
                txtPercentualReducaoIcms.Text = string.Empty;
                txtPercentualIcms.Text = string.Empty;
                txtValorIcms.Text = string.Empty;

                txtMotivoDesoneracaoIcmsProduto.Text = string.Empty;
                txtIcmsDesoneracaoProduto.Text = string.Empty;

                txtBaseIcmsSt.Text = string.Empty;
                txtPercentualIVA.Text = string.Empty;
                txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text = string.Empty;
                txtAliquotaSt.Text = string.Empty;
                txtValorIcmsSt.Text = string.Empty;
            }
        }

        private void GeraIdParaCadaItem()
        {
            int id = 0;

            foreach (var item in _notaFiscalEmEdicao.ListaItens)
            {
                item.Id = id;

                id++;
            }
        }
        
        #endregion

        #region " SALVAR E ENVIAR NOTA "

        private void SalveEEnvieNota()
        {
            if (MessageBox.Show("Deseja enviar esta nota fiscal?", "Enviar nota fiscal", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            _notaFiscalEmEdicao.IdentificacaoNotaFiscal.DataHoraSaida = DateTime.Now;
            _notaFiscalEmEdicao.IdentificacaoNotaFiscal.DataHoraEmissao = DateTime.Now;

            //Verifica qual o tipo de emissão e muda conforme check box marcada
            VerificaEMudaTipoEmissaoNotaFiscal();
            
            if (_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem != EnumTipoDocumento.OUTRASSAIDAS)
            {
                var pedidoDeVenda = new ServicoPedidoDeVenda().Consulte(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);
                if (pedidoDeVenda.Cliente.DadosGerais.Razao.Length.ToInt() > 80) 
                        {
                    pedidoDeVenda.Cliente.DadosGerais.Razao = pedidoDeVenda.Cliente.DadosGerais.Razao.Substring(0, 60);
                    pedidoDeVenda.Cliente.DadosGerais.NomeFantasia = pedidoDeVenda.Cliente.DadosGerais.NomeFantasia.Substring(0, 60);
                }
           

                var statusParaEmissao = new ServicoParametros().ConsulteParametros().ParametrosFiscais.EmitirNotaSemReceber? EnumStatusPedidoDeVenda.RESERVADO: EnumStatusPedidoDeVenda.FATURADO;
               
                if (pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMITIDONFE || pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO 
                        || _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.OUTRASSAIDAS || pedidoDeVenda.StatusPedidoVenda == statusParaEmissao)
                {
                    //Não faz nada...                  
                }
                else
                {
                    MessageBox.Show("O pedido de número: " + pedidoDeVenda.Id + ". Precisa estar COM STATUS: 'PAGO' ou 'EMITIDONFE' para reemitir a nota fiscal.", "Enviar nota fiscal", MessageBoxButtons.OK);
                    return;
                }
            }
            else
                _notaFiscalEmEdicao.IdentificacaoNotaFiscal.IrProximoNumero = chkProximaNota.Visible == true ? chkProximaNota.Checked : false;

            Action actionSalvar = () =>
            {
                try
                {
                    ExibaFormAviso();

                    //Se a nota fiscal estiver processando, vamos verificar como está na sefaz novamente
                    if (_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.PROCESSANDO || 
                        _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA )
                    {
                        if (ARejeicaoNotaEhDuplicao(_notaFiscalEmEdicao))
                        {
                            _EhRejeicao = true;

                            if (_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 55)
                                ValideNotaFiscalRejeitada();
                            else
                                PreenchaNotaFiscalEmEdicao();

                            _EhRejeicao = false;
                        }
                    }

                    if (_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.AUTORIZADA || chkEmitirNotaFiscalContigencia.Enabled == false)
                    {
                        EnvieNotaFiscal();

                        if (_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 55)
                            ValideNotaFiscal();
                        else
                            PreenchaNotaFiscalEmEdicao();

                    }

                    GereMovimentacaoNotaFiscalOutrasSaidas();

                    FecheFormAvisoThread();

                    if (MessageBox.Show("Deseja imprimir o DANFE?", "Imprimir DANFE", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        ImprimirNota();
                    }                    
                }
                catch (Exception e)
                {                    
                    FecheFormAvisoThread();
                    throw e;
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Nota Fiscal Emitida com sucesso.");
        }

        #endregion

        #region " EDIÇÃO VW NOTA DOCUMENTO "

        private void EditePedidoDeVenda(NotaFiscal vwNotaDocumento)
        {
            EditePedidoDeVenda(vwNotaDocumento.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);
        }

        private void NotaFiscalComDesconto(ref NotaFiscal notaFiscal)
        {
            foreach (var item in notaFiscal.ListaItens)
            {
                item.ValorUnitario = Math.Round(item.ValorUnitario / 2, 2);
                item.ValorTotal = Math.Round(item.ValorTotal / 2, 2);
                item.OutrasDespesas = item.OutrasDespesas / 2;
                item.Seguro = item.Seguro / 2;
                item.ValorFrete = item.ValorFrete / 2;
                item.ValorDesconto = item.ValorDesconto / 2;

                item.Impostos.TotalTributacao = Math.Round(item.Impostos.TotalTributacao / 2, 2);
                item.Impostos.TotalTributacaoEstadual = Math.Round(item.Impostos.TotalTributacaoEstadual / 2, 2);
                item.Impostos.TotalTributacaoFederal = Math.Round(item.Impostos.TotalTributacaoFederal / 2, 2);

                //item.Impostos.TributosDevolvidos.ValorDoIpiDevolvido = Math.Round(item.Impostos.TributosDevolvidos.ValorDoIpiDevolvido / 2, 2);
                if (item.Impostos.Cofins != null)
                {
                    item.Impostos.Cofins.BaseDeCalculo = item.Impostos.Cofins.BaseDeCalculo / 2;
                    item.Impostos.Cofins.BaseDeCalculoST = item.Impostos.Cofins.BaseDeCalculoST / 2;
                    item.Impostos.Cofins.ValorCofins = item.Impostos.Cofins.ValorCofins / 2;
                    if (item.Impostos.Cofins.ValorCofinsST != null)
                    {
                        item.Impostos.Cofins.ValorCofins = item.Impostos.Cofins.ValorCofinsST / 2;
                    }
             
                }

                if (item.Impostos.Fcp != null)
                {
                    item.Impostos.Fcp.ValorBaseFCP = Math.Round(item.Impostos.Fcp.ValorBaseFCP / 2, 2);
                    item.Impostos.Fcp.ValorBCSTFCP = Math.Round(item.Impostos.Fcp.ValorBCSTFCP / 2, 2);
                    item.Impostos.Fcp.ValorFCP = Math.Round(item.Impostos.Fcp.ValorFCP / 2, 2);
                    item.Impostos.Fcp.ValorFCPST = Math.Round(item.Impostos.Fcp.ValorFCPST / 2, 2);
                }

                if (item.Impostos.Icms != null)
                {
                    item.Impostos.Icms.ValorDesoneracaoProduto = item.Impostos.Icms.ValorDesoneracaoProduto / 2;
                    item.Impostos.Icms.ValorIcms = item.Impostos.Icms.ValorIcms / 2;
                    item.Impostos.Icms.ValorIcmsSimplesNacional = item.Impostos.Icms.ValorIcmsSimplesNacional / 2;
                    item.Impostos.Icms.ValorSubstituicaoTributaria = item.Impostos.Icms.ValorSubstituicaoTributaria / 2;
                    item.Impostos.Icms.BaseCalculoIcms = item.Impostos.Icms.BaseCalculoIcms / 2;
                    item.Impostos.Icms.BaseIcmsSubstituicaoTributaria = item.Impostos.Icms.BaseIcmsSubstituicaoTributaria / 2;
                }

                if (item.Impostos.IcmsInterestadual != null)
                {
                    item.Impostos.IcmsInterestadual.ValorFCP = Math.Round(item.Impostos.IcmsInterestadual.ValorFCP / 2, 2);
                    item.Impostos.IcmsInterestadual.ValorIcmsDestino = Math.Round(item.Impostos.IcmsInterestadual.ValorIcmsDestino / 2, 2);
                    item.Impostos.IcmsInterestadual.ValorIcmsOrigem = Math.Round(item.Impostos.IcmsInterestadual.ValorIcmsOrigem / 2, 2);
                    item.Impostos.IcmsInterestadual.BaseDeCalculo = Math.Round(item.Impostos.IcmsInterestadual.BaseDeCalculo / 2, 2);
                }

                if (item.Impostos.Ipi != null)
                {
                    item.Impostos.Ipi.ValorIpi = item.Impostos.Ipi.ValorIpi / 2;
                    item.Impostos.Ipi.BaseDeCalculo = item.Impostos.Ipi.BaseDeCalculo / 2;
                }

                if (item.Impostos.Pis != null)
                {
                    item.Impostos.Pis.ValorPis = item.Impostos.Pis.ValorPis / 2;
                    item.Impostos.Pis.ValorPisST = item.Impostos.Pis.ValorPisST / 2;
                    item.Impostos.Pis.BaseDeCalculo = item.Impostos.Pis.BaseDeCalculo/ 2;
                    item.Impostos.Pis.BaseDeCalculoST = item.Impostos.Pis.BaseDeCalculoST / 2;

                }

            }

            notaFiscal.TotaisNotaFiscal.BaseCalculoIcms = Math.Round(notaFiscal.TotaisNotaFiscal.BaseCalculoIcms / 2, 2);
            notaFiscal.TotaisNotaFiscal.BaseCalculoIcmsST = Math.Round(notaFiscal.TotaisNotaFiscal.BaseCalculoIcmsST / 2, 2);
            notaFiscal.TotaisNotaFiscal.Cofins = Math.Round(notaFiscal.TotaisNotaFiscal.Cofins / 2, 2);
            notaFiscal.TotaisNotaFiscal.Desconto = Math.Round(notaFiscal.TotaisNotaFiscal.Desconto / 2, 2);
            notaFiscal.TotaisNotaFiscal.Frete = Math.Round(notaFiscal.TotaisNotaFiscal.Frete / 2, 2);
            notaFiscal.TotaisNotaFiscal.Icms = Math.Round(notaFiscal.TotaisNotaFiscal.Icms / 2, 2);
            notaFiscal.TotaisNotaFiscal.IcmsDesoneracao = Math.Round(notaFiscal.TotaisNotaFiscal.IcmsDesoneracao / 2, 2);
            notaFiscal.TotaisNotaFiscal.ImpostoDeImportacao = Math.Round(notaFiscal.TotaisNotaFiscal.ImpostoDeImportacao / 2, 2);
            notaFiscal.TotaisNotaFiscal.Ipi = Math.Round(notaFiscal.TotaisNotaFiscal.Ipi / 2, 2);
            notaFiscal.TotaisNotaFiscal.OutrosValores = Math.Round(notaFiscal.TotaisNotaFiscal.OutrosValores / 2, 2);
            notaFiscal.TotaisNotaFiscal.Pis = Math.Round(notaFiscal.TotaisNotaFiscal.Pis / 2, 2);
            notaFiscal.TotaisNotaFiscal.Produtos = Math.Round(notaFiscal.TotaisNotaFiscal.Produtos / 2, 2);
            notaFiscal.TotaisNotaFiscal.TotalFCPNf = notaFiscal.TotaisNotaFiscal.TotalFCPNf / 2;
            notaFiscal.TotaisNotaFiscal.TotalTributacao = Math.Round(notaFiscal.TotaisNotaFiscal.TotalTributacao / 2, 2);
            notaFiscal.TotaisNotaFiscal.TotalTributacaoEstadual = Math.Round(notaFiscal.TotaisNotaFiscal.TotalTributacaoEstadual / 2, 2);
            notaFiscal.TotaisNotaFiscal.TotalTributacaoFederal = Math.Round(notaFiscal.TotaisNotaFiscal.TotalTributacaoFederal / 2, 2);
            notaFiscal.TotaisNotaFiscal.ValorFCP = notaFiscal.TotaisNotaFiscal.ValorFCP / 2;
            notaFiscal.TotaisNotaFiscal.ValorFCPSt = notaFiscal.TotaisNotaFiscal.ValorFCPSt / 2;
            notaFiscal.TotaisNotaFiscal.ValorFCPSTRet = notaFiscal.TotaisNotaFiscal.ValorFCPSTRet / 2;
            notaFiscal.TotaisNotaFiscal.ValorInterestadualDestino = notaFiscal.TotaisNotaFiscal.ValorInterestadualDestino / 2;
            notaFiscal.TotaisNotaFiscal.ValorInterestadualOrigem = notaFiscal.TotaisNotaFiscal.ValorInterestadualOrigem / 2;
            notaFiscal.TotaisNotaFiscal.ValorNotaFiscal = Math.Round(notaFiscal.TotaisNotaFiscal.ValorNotaFiscal / 2, 2);
            notaFiscal.TotaisNotaFiscal.ValorSeguro = Math.Round(notaFiscal.TotaisNotaFiscal.ValorSeguro / 2, 2);
            notaFiscal.TotaisNotaFiscal.ValorSubstituicaoTributaria = Math.Round(notaFiscal.TotaisNotaFiscal.ValorSubstituicaoTributaria / 2, 2);

            foreach (var itemParcela in notaFiscal.ListaFormasPagamentoNFCe)
            {
                itemParcela.ValorPagamento = Math.Round(itemParcela.ValorPagamento / 2, 2);
            }

            notaFiscal.DadosCobranca.TotalDePagamento = Math.Round(notaFiscal.DadosCobranca.TotalDePagamento / 2, 2);

            foreach (var itemParcela in notaFiscal.DadosCobranca.ListaDeParcelasVendas)
            {
                itemParcela.Valor = Math.Round(itemParcela.Valor / 2, 2);
            }

            foreach (var itemParcela in notaFiscal.DadosCobranca.ListaDuplicatas)
            {
                itemParcela.ValorDuplicata = Math.Round(itemParcela.ValorDuplicata / 2, 2);
            }

            notaFiscal.InformacoesGeraisNotaFiscal.Observacoes = string.Concat("Trib aprox R$: ",
                                                                                                                        notaFiscal.TotaisNotaFiscal.TotalTributacaoFederal.ToString("0.00"),
                                                                                                                        " Federal e ",
                                                                                                                        notaFiscal.TotaisNotaFiscal.TotalTributacaoEstadual.ToString("0.00"),
                                                                                                                        " Estadual - Fonte: IBPT");

        }

        private void EditePedidoDeVenda(int numeroPedidoDeVenda)
        {
            FormConfirmacaoDadosNotaFiscal formConfirmacaoDadosNotaFiscal = new FormConfirmacaoDadosNotaFiscal(numeroPedidoDeVenda, _enviarStatusNotaFiscal, _modeloEmissaoFiscal);
            var resultado = formConfirmacaoDadosNotaFiscal.ConfirmeDadosDestinatario();

            if (resultado != DialogResult.OK)
            {
                this.Close();

                return;
            }

            this.Cursor = Cursors.WaitCursor;
            
            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
            var nota = servicoNotaFiscal.RetorneNotaAPartirDePedido(numeroPedidoDeVenda, formConfirmacaoDadosNotaFiscal.DestinatarioAuxiliarNotaFiscal);

            _notaFicalComDesconto = formConfirmacaoDadosNotaFiscal.notaComDesconto;
            _modeloEmissaoFiscal = formConfirmacaoDadosNotaFiscal.modeloEmissaoNotaFiscal;

            if (_notaFicalComDesconto)
            {
                NotaFiscalComDesconto(ref nota);
            }            

            this.Cursor = Cursors.Default;
            
            _notaFiscalEmEdicao = nota;
            _notaFiscalEmEdicao.ListaNotasReferenciadas = _notaFiscalEmEdicao.ListaNotasReferenciadas.Count == 0 ? _notasFiscaisReferenciadas : _notaFiscalEmEdicao.ListaNotasReferenciadas;
                        
            if (PrecisaCalcularPartilhaDeIcms())
            {
                if (!NcmsInterestaduaisEstaoCadastrados())
                {
                    this.Close();
                    return;
                }

                servicoNotaFiscal.CalculePartilhaDeIcms(_notaFiscalEmEdicao);
            }
            
            if(PrecisaCalcularFCP())
            {
                servicoNotaFiscal.CalculeFCP(_notaFiscalEmEdicao);
            }

            PreenchaNotaFiscalEmEdicao();            
        }
        
        private bool NcmsInterestaduaisEstaoCadastrados()
        {
            if (PrecisaCalcularPartilhaDeIcms())
            {
                PreenchaListaNcmECfop();

                List<Ncm> listaNcmSemAliquotaEstadoDestino = new List<Ncm>();

                ServicoIcmsInterestadual servicoIcmsInterestadual = new ServicoIcmsInterestadual(false, false);

                foreach (var ncm in _listaNcm)
                {
                    bool ncmExistente = servicoIcmsInterestadual.ExisteAliquotaParaONcmEOEstado(ncm.Id, _notaFiscalEmEdicao.Destinatario.UF);

                    if (!ncmExistente)
                    {
                        listaNcmSemAliquotaEstadoDestino.Add(ncm);
                    }
                }

                if (listaNcmSemAliquotaEstadoDestino.Count > 0)
                {
                    FormNcmSemAliquotaInternaEstadoDestino formNcmSemAliquotaInternaEstadoDestino = new FormNcmSemAliquotaInternaEstadoDestino();

                    var resultado = formNcmSemAliquotaInternaEstadoDestino.InsiraAliquotasNcmEstadoDestino(listaNcmSemAliquotaEstadoDestino, _notaFiscalEmEdicao.Destinatario.UF);

                    return resultado == DialogResult.OK;
                }
            }

            return true;
        }

        private bool PrecisaCalcularPartilhaDeIcms()
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();

            if (parametros.ParametrosFiscais == null || !parametros.ParametrosFiscais.CalcularPartilhaIcms)
            {
                return false;
            }

            return _notaFiscalEmEdicao.Emitente.UF != _notaFiscalEmEdicao.Destinatario.UF &&
                      _notaFiscalEmEdicao.Destinatario.UF != "EX" &&
                      _notaFiscalEmEdicao.IdentificacaoNotaFiscal.ConsumidorFinal 
                      && _notaFiscalEmEdicao.Destinatario.IndicadorIEDestinatario == EnumIndicadorIEDestinatario.NAOCONTRIBUINTE;
                     
        }

        private bool PrecisaCalcularFCP()
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();

            if (parametros.ParametrosFiscais == null || !parametros.ParametrosFiscais.CalcularFCP)
            {
                return false;
            }

            return true;
        }

        private void EditeDevolucao(VwNotasDocumentos vwNotaDocumento)
        {

        }

        #region " EDIÇÃO NOTA FISCAL "

        private void EditeNotaFiscal(NotaFiscal vwNotaDocumento)
        {
            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();

            _notaFiscalEmEdicao = servicoNotaFiscal.Consulte(vwNotaDocumento.Id);

            //Bloco exclusivo para notas cadastradas em OUTRAS SAÍDAS / ENTRADAS ********************************************************
            if ((_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.OUTRASSAIDAS && 
                _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.DISPONIVEL)||
                (_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.OUTRASSAIDAS && 
                _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA))
            {
                _modeloEmissaoFiscal = EnumModeloNotaFiscal.NFE;
                _notaFiscalEmEdicao.ListaNotasReferenciadas = _notaFiscalEmEdicao.ListaNotasReferenciadas.Count ==0? 
                                                              _notasFiscaisReferenciadas: _notaFiscalEmEdicao.ListaNotasReferenciadas;
                _notaFiscalEmEdicao.IdentificacaoNotaFiscal.DataHoraEmissao = DateTime.Now;
                _notaFiscalEmEdicao.IdentificacaoNotaFiscal.DataHoraSaida = DateTime.Now;
                chkProximaNota.Visible = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA ? true : false;

                if (PrecisaCalcularPartilhaDeIcms())
                {
                    if (!NcmsInterestaduaisEstaoCadastrados())
                    {
                        this.Close();
                        return;
                    }

                    servicoNotaFiscal.CalculePartilhaDeIcms(_notaFiscalEmEdicao);
                } 
                
                if(PrecisaCalcularFCP())
                {
                    servicoNotaFiscal.CalculeFCP(_notaFiscalEmEdicao);
                }
            }
            //***************************************************************************************************************************

            foreach (var item in _notaFiscalEmEdicao.ListaItens)
            {
                item.Produto.CarregueLazyLoad();
            }

            foreach (var item in _notaFiscalEmEdicao.ListaCartasCorrecoes)
            {
                item.CarregueLazyLoad();
            }

            PreenchaNotaFiscalEmEdicao();
        }

        private void EditeNotaFiscalRejeitadaDoPedido(NotaFiscal vwNotaDocumento)
        {
            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();

            _notaFiscalEmEdicao = servicoNotaFiscal.Consulte(vwNotaDocumento.Id);
            
            FormConfirmacaoDadosNotaFiscal formConfirmacaoDadosNotaFiscal = new FormConfirmacaoDadosNotaFiscal(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId, _enviarStatusNotaFiscal, _modeloEmissaoFiscal);
            var resultado = formConfirmacaoDadosNotaFiscal.ConfirmeDadosDestinatario();

            if (resultado != DialogResult.OK)
            {
                this.Close();

                return;
            }

            servicoNotaFiscal = new ServicoNotaFiscal();
            var nota = servicoNotaFiscal.RetorneNotaAPartirDePedido(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId, formConfirmacaoDadosNotaFiscal.DestinatarioAuxiliarNotaFiscal);

            _notaFicalComDesconto = formConfirmacaoDadosNotaFiscal.notaComDesconto;

            if (_notaFicalComDesconto)
            {
                NotaFiscalComDesconto(ref nota);
            }

            _notaFiscalEmEdicao.Destinatario = nota.Destinatario;
            _notaFiscalEmEdicao.InformacoesComercioExteriorNotaFiscal = nota.InformacoesComercioExteriorNotaFiscal;
            _notaFiscalEmEdicao.ListaItens = nota.ListaItens;
            _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas = nota.DadosCobranca.ListaDuplicatas;
            _notaFiscalEmEdicao.TotaisNotaFiscal = nota.TotaisNotaFiscal;

            if (PrecisaCalcularPartilhaDeIcms())
            {
                if (!NcmsInterestaduaisEstaoCadastrados())
                {
                    this.Close();
                    return;
                }

                servicoNotaFiscal.CalculePartilhaDeIcms(_notaFiscalEmEdicao);
            }

            if (PrecisaCalcularFCP())
            {
                servicoNotaFiscal.CalculeFCP(_notaFiscalEmEdicao);
            }

            foreach (var item in _notaFiscalEmEdicao.ListaItens)
            {
                item.Produto.CarregueLazyLoad();
            }
            
            PreenchaNotaFiscalEmEdicao();
        }

        private void PreenchaNotaFiscalEmEdicao()
        {   
            PreenchaListaNcmECfop();

            
            PreenchaItensNotaFiscal(_notaFiscalEmEdicao.ListaItens);

            PreenchaDuplicatasNotaFiscal(_notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas);

            PreenchaInformacoesGeraisNotaFiscal(_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal);
            PreenchaIdentificacaoNotaFiscal(_notaFiscalEmEdicao.IdentificacaoNotaFiscal);
            PreenchaInformacoesDocumentoOrigemNotaFiscal(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal);
            
            _notaFiscalEmEdicao.ListaNotasReferenciadas = _notaFiscalEmEdicao.ListaNotasReferenciadas.Count == 0 ? _notasFiscaisReferenciadas : _notaFiscalEmEdicao.ListaNotasReferenciadas;

            //Só vai entrar aqui se o destinatário não for nulo
            if(_notaFiscalEmEdicao.Destinatario != null)
                PreenchaDestinatarioNotaFiscal(_notaFiscalEmEdicao.Destinatario);

            PreenchaTotaisNotaFiscal(_notaFiscalEmEdicao.TotaisNotaFiscal);
            PreenchaGridCartasCorrecoes();

            PreenchaInformacoesPagamentosNotaVersao4_00();

            ExibaCamposDeAcordoComStatus();
        }

        private string BuscaObservacaoNoPedidoParaNota(int numeroPedido, EnumTipoDocumento tipoDoc = EnumTipoDocumento.PEDIDODEVENDAS)
        {
            if (tipoDoc == EnumTipoDocumento.OUTRASSAIDAS) return string.Empty;

            PedidoDeVenda pedidoVendaPesquisado = new PedidoDeVenda();
            ServicoPedidoDeVenda pedidoVenda = new ServicoPedidoDeVenda();

            pedidoVendaPesquisado = pedidoVenda.Consulte(numeroPedido);

            if (pedidoVendaPesquisado != null)
                return pedidoVendaPesquisado.ObservacoesNotaFiscal;
            else
                return string.Empty;
        }

        private void PreenchaListaNcmECfop()
        {
            if (_listaNcm == null || _listaCfops == null)
            {
                List<string> listaCodigosCfop = new List<string>();
                List<string> listaCodigosNcm = new List<string>();

                foreach (var item in _notaFiscalEmEdicao.ListaItens)
                {
                    listaCodigosCfop.Add(item.Cfop.ToString());
                    listaCodigosNcm.Add(item.Ncm);
                }

                ServicoNcm servicoNcm = new ServicoNcm(false, false);
                ServicoCfop servicoCfop = new ServicoCfop(false, false);

                _listaNcm = servicoNcm.ConsulteListaDeCodigosNcm(listaCodigosNcm);
                _listaCfops = servicoCfop.ConsulteListaDeCodigosCfop(listaCodigosCfop);
            }
        }

        private void PreenchaInformacoesGeraisNotaFiscal(InformacoesGeraisNotaFiscal informacoesGeraisNotaFiscal)
        {
            txtChaveAcessoNota.Text = informacoesGeraisNotaFiscal.ChaveDeAcesso;
            txtMensagemErro.Text = informacoesGeraisNotaFiscal.MensagemDeErro;
            txtMensagemDevolvida.Text = informacoesGeraisNotaFiscal.MensagemDevolvida;

            //Se txtObservacoesDaNotaFiscal estiver preenchida vai retornar apenas o mesmo valor abaixo.
            if (string.IsNullOrEmpty(txtObservacoesDaNotaFiscal.Text))

                //Se txtObservacoesDaNotaFiscal não estiver preenchido vai entrar aqui e verificar se existe observações da nota.
                if (!string.IsNullOrEmpty(BuscaObservacaoNoPedidoParaNota(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId, (EnumTipoDocumento)_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem)))
                {
                    //Aqui testa se a nota fiscal está gerada. Se estiver gerada apenas preenche com o valor quando foi gerada.
                    if (_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.DISPONIVEL)
                    {
                        txtObservacoesDaNotaFiscal.Text = informacoesGeraisNotaFiscal.Observacoes;
                    }
                    //Vai retirar o valor fixo e colocar os valores variáveis, pois este Status NF pode estornar e alterar o pedido.
                    else if (_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA)
                    {
                        string observacoes;
                        observacoes = informacoesGeraisNotaFiscal.Observacoes.Remove(0, 58);
                        observacoes = informacoesGeraisNotaFiscal.Observacoes.Remove(58, observacoes.Length.ToInt());
                        informacoesGeraisNotaFiscal.Observacoes = observacoes + " - " + BuscaObservacaoNoPedidoParaNota(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId, (EnumTipoDocumento)_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem);
                        txtObservacoesDaNotaFiscal.Text = informacoesGeraisNotaFiscal.Observacoes;
                    }
                    //Vai preencher a NF disponível para emissão.
                    else
                    {
                        txtObservacoesDaNotaFiscal.Text = informacoesGeraisNotaFiscal.Observacoes
                            + Environment.NewLine +
                                BuscaObservacaoNoPedidoParaNota
                                    (_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId, (EnumTipoDocumento)_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem);

                        informacoesGeraisNotaFiscal.Observacoes += " - " + BuscaObservacaoNoPedidoParaNota(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId, (EnumTipoDocumento)_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem);
                    }
                }
                else
                    txtObservacoesDaNotaFiscal.Text = informacoesGeraisNotaFiscal.Observacoes;
            
            var parametros = new ServicoParametros(false).ConsulteParametros();

            if(!string.IsNullOrEmpty(parametros.ParametrosFiscais.ObservacoesGeraisNotaFiscal))
            {
                txtObservacoesDaNotaFiscal.Text = informacoesGeraisNotaFiscal.Observacoes
                            + Environment.NewLine + parametros.ParametrosFiscais.ObservacoesGeraisNotaFiscal;
            }

            txtStatusNota.Text = informacoesGeraisNotaFiscal.Status.Descricao();

            if (informacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA ||
                informacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.DENEGADA)
            {
                txtStatusNota.ForeColor = Color.Red;
            }
            else if (informacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.AUTORIZADA)
            {
                txtStatusNota.ForeColor = Color.Green;
            }
           
        }

        private void PreenchaIdentificacaoNotaFiscal(IdentificacaoNotaFiscal identificacaoNotaFiscal)
        {
            txtNaturezaOperacao.Text = identificacaoNotaFiscal.DescricaoNaturezaOperacao;

            txtNotaDe.Text = identificacaoNotaFiscal.NotaSaida ? "SAÍDA" : "ENTRADA";
            txtConsumidorFinalOuRevenda.Text = identificacaoNotaFiscal.ConsumidorFinal ? "CONSUMIDOR FINAL" : "REVENDA";

            txtNumeroNota.Text = identificacaoNotaFiscal.NumeroNota > 0 ? identificacaoNotaFiscal.NumeroNota.ToString() : string.Empty;
            txtSerieNota.Text = identificacaoNotaFiscal.Serie > 0 ? identificacaoNotaFiscal.Serie.ToString() : string.Empty;

            txtDataEmissaoNota.Text = identificacaoNotaFiscal.DataHoraEmissao.ToString("dd/MM/yyyy HH:mm");            
            txtDataSaidaNota.Text = identificacaoNotaFiscal.DataHoraSaida.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm");

            txtCondicaoPagamentoNotaFiscal.Text = identificacaoNotaFiscal.FormaPagamento.Descricao();
        }

        private void PreenchaInformacoesDocumentoOrigemNotaFiscal(InformacoesDocumentoOrigemNotaFiscal informacoesDocumentoOrigemNotaFiscal)
        {
            txtOrigemNota.Text = informacoesDocumentoOrigemNotaFiscal.Origem.Descricao();
            txtNumeroDocumentoOrigem.Text = informacoesDocumentoOrigemNotaFiscal.DocumentoId.ToString();
        }

        private void PreenchaDestinatarioNotaFiscal(DestinatarioNotaFiscal destinatarioNotaFiscal)
        {
            txtNomeParceiroNota.Text = destinatarioNotaFiscal.RazaoSocialOuNomeDestinatario;
            txtCpfCnpjDestinatarioNota.Text = destinatarioNotaFiscal.CnpjCpf;
            txtInscricaoEstadualDestinatarioNota.Text = destinatarioNotaFiscal.InscricaoEstadual;

            if (destinatarioNotaFiscal.ParceiroResideExterior)
            {
                txtEndereco.Text = "PARCEIRO RESIDE NO EXTERIOR - " + destinatarioNotaFiscal.NomePais;
            }
            else
            {
                _ufDestinatario = destinatarioNotaFiscal.UF;
                _codigoMunicipioDestinatario = destinatarioNotaFiscal.CodigoMunicipio.ToString();

                _cepDestinatarioAtual = destinatarioNotaFiscal.Cep;

                txtEndereco.Text = destinatarioNotaFiscal.Cep + ", " + destinatarioNotaFiscal.Logradouro + ", " + destinatarioNotaFiscal.Bairro + ", ";

                if (!string.IsNullOrWhiteSpace(destinatarioNotaFiscal.Complemento))
                {
                    txtEndereco.Text += destinatarioNotaFiscal.Complemento + ", ";
                }

                //Quando o endereço for nulo vamos preencher com "." traço
                if (!string.IsNullOrWhiteSpace(destinatarioNotaFiscal.Numero))
                {
                    txtEndereco.Text += destinatarioNotaFiscal.Numero + " - ";
                }
                else
                    {
                        txtEndereco.Text += destinatarioNotaFiscal.Numero + "." + " - ";
                    }

                txtEndereco.Text += destinatarioNotaFiscal.NomeMunicipio + " - " + destinatarioNotaFiscal.UF;
                txtEndereco.Text = txtEndereco.Text.ToUpper();
            }
        }

        private void PreenchaTotaisNotaFiscal(TotaisNotaFiscal totaisNotaFiscal)
        {
            lblBaseCalculoIcms.Text = totaisNotaFiscal.BaseCalculoIcms.ToString("0.00");
            lblValorIcms.Text = totaisNotaFiscal.Icms.ToString("0.00");

            lblBaseCalculoIcmsST.Text = totaisNotaFiscal.BaseCalculoIcmsST.ToString("0.00");
            lblValorIcmsST.Text = totaisNotaFiscal.ValorSubstituicaoTributaria.ToString("0.00");

            lblValorFrete.Text = totaisNotaFiscal.Frete.ToString("0.00");
            lblValorSeguro.Text = totaisNotaFiscal.ValorSeguro.ToString("0.00");

            lblValorOutrasDespesas.Text = totaisNotaFiscal.OutrosValores.ToString("0.00");
            lblValorDesconto.Text = totaisNotaFiscal.Desconto.ToString("0.00");

            lblValorIpi.Text = totaisNotaFiscal.Ipi.ToString("0.00");
            lblValorPis.Text = totaisNotaFiscal.Pis.ToString("0.00");
            lblValorCofins.Text = totaisNotaFiscal.Cofins.ToString("0.00");
            lblValorDeImportacao.Text = totaisNotaFiscal.ImpostoDeImportacao.ToString("0.00");

            lblValorFCP.Text = totaisNotaFiscal.ValorFCP.ToDouble() != 0 ? totaisNotaFiscal.ValorFCP.GetValueOrDefault().ToString("0.00"): totaisNotaFiscal.TotalFCPNf.GetValueOrDefault().ToString("0.00");
            lblValorIcmsPartDestino.Text = totaisNotaFiscal.ValorInterestadualDestino.GetValueOrDefault().ToString("0.00");
            lblValorIcmsPartOrigem.Text = totaisNotaFiscal.ValorInterestadualOrigem.GetValueOrDefault().ToString("0.00");

            lblValorTotalProdutos.Text = totaisNotaFiscal.Produtos.ToString("0.00");
            lblValorTotalNotaFiscal.Text = totaisNotaFiscal.ValorNotaFiscal.ToString("0.00");
        } 

        private void PreenchaItensNotaFiscal(IList<ItemNotaFiscal> listaItens)
        {
            _notaFiscalEmEdicao.ListaItens = listaItens.ToList();

            AtualizeGridItens();
        }

        private void PreenchaDuplicatasNotaFiscal(IList<DuplicataNotaFiscal> listaDuplicatas)
        {
            if (listaDuplicatas.Count == 0)
            {
                pnlDuplicatas.Enabled = false;

                LimpeCamposFinanceiro();

                PreenchaGridFinanceiro();
            }
            else
            {
                pnlDuplicatas.Enabled = true;
            }

            PreenchaGridFinanceiro();
        }

        private void PreenchaInformacoesPagamentosNotaVersao4_00()
        {
            var pedidoDeVenda = new ServicoPedidoDeVenda().Consulte(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);

            if (pedidoDeVenda == null) return;

            _notaFiscalEmEdicao.DadosCobranca.ListaDeParcelasVendas = pedidoDeVenda.ListaParcelasPedidoDeVenda.ToList();

            var formaPgtoPesquisa = pedidoDeVenda.ListaParcelasPedidoDeVenda.Select(x => x.FormaPagamento.TipoFormaPagamento).FirstOrDefault();

            _notaFiscalEmEdicao.DadosCobranca.TotalDePagamento = pedidoDeVenda.ListaParcelasPedidoDeVenda.Sum(x=>x.Valor);
            _notaFiscalEmEdicao.DadosCobranca.FormaPagamentoNF = new ServicoNotaFiscal().RetorneFormaPagamentoParaNF(formaPgtoPesquisa);
            _notaFiscalEmEdicao.DadosCobranca.CondicaoVistaPrazo = retorneCondicaoPagamentoVistaPrazo(pedidoDeVenda.ListaParcelasPedidoDeVenda.ToList());           
        }

        private EnumCondicaoVistaPrazo retorneCondicaoPagamentoVistaPrazo(List<ParcelaPedidoDeVenda> ListaParcelas)
        {
            var idCondicao = ListaParcelas.Select(x =>x.CondicaoPagamento.Id).FirstOrDefault();

            var condicao = new ServicoCondicaoPagamento().Consulte(idCondicao);

            var nDias = condicao.ListaDeParcelas.Select(x => x.Dias).FirstOrDefault();

            if (nDias > 1)
            {
                return EnumCondicaoVistaPrazo.APrazo;
            }
            else
            {
                return EnumCondicaoVistaPrazo.AVista;
            }
        }

        private void carregaNovasInformaçõesDoCampoObservaçãoNotaFiscal()
        {
            _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Observacoes = RemoveDiacritics(RemoveSpecialCharacters(txtObservacoesDaNotaFiscal.Text))
                                                                          .RemovaEspacosEmBrancoDoInicioEFim()
                                                                          .RemovaAcentos();
        }

        #endregion

        private void ExibaCamposDeAcordoComStatus()
        {
            chkEmitirNotaFiscalContigencia.Enabled = true;
            if (_notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe != EnumTipoEmissaoDanfe.CONTINGENCIAOFFLINE)
            {
                if (_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.AUTORIZADA || _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.CANCELADA)
                {
                    btnSalvarEnviar.Visible = false;
                    btnImprimirDanfe.Visible = true;

                    btnAdicionarFinanceiro.Visible = false;

                    btnEmitirCartaCorrecao.Visible = true;
                    btnSalvarXml.Visible = true;
                }
            }
            else
                chkEmitirNotaFiscalContigencia.Enabled = false;
        }

        #endregion

        #region " CARTAS DE CORREÇÕES "

        private void PreenchaGridCartasCorrecoes()
        {
            _notaFiscalEmEdicao.ListaCartasCorrecoes = _notaFiscalEmEdicao.ListaCartasCorrecoes?? new List<CartaCorrecao>();
            var lista = _notaFiscalEmEdicao.ListaCartasCorrecoes.OrderByDescending(carta => carta.SequenciaEvento);

            gcCartasCorrecoes.DataSource = lista;
            gcCartasCorrecoes.RefreshDataSource();
        }

        #endregion

        #region " SALVAR, ENVIAR E VALIDAR NOTA "

        private void EnvieNotaFiscal()
        {
            //Carrega as últimas informações digitadas na observação da nota fiscal
            carregaNovasInformaçõesDoCampoObservaçãoNotaFiscal();

            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();

            _modeloEmissaoFiscal = _modeloEmissaoFiscal != 0 ? _modeloEmissaoFiscal : (EnumModeloNotaFiscal)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal;

            //Envia para emitir Nota Fiscal número 65 NFCe
            if (_modeloEmissaoFiscal == EnumModeloNotaFiscal.NFCE)
            {
                //Se o cpf/cnpj no modelo NFCe(65) for nulo então limpa o destinatário
                //Na emissão da nota sairá "CONSUMIDOR NÃO IDENTIFICADO"
                if (txtCpfCnpjDestinatarioNota.Text == "")
                {
                    _notaFiscalEmEdicao.Destinatario.CnpjCpf = null;
                }
                else
                {//Caso tiver o cpf/cnpj tem que validar
                    if (!ValidaCpfCnpjNFCe(txtCpfCnpjDestinatarioNota.Text))
                        return;
                }
                //Envia a NFCe                
                _notaFiscalEmEdicao = servicoNotaFiscal.EnviaNFCe(_notaFiscalEmEdicao);
            }
            //Envia para emitir a Nota Fiscal número 55 NFe
            else
            {   
                servicoNotaFiscal.SalveEEnvieNota(_notaFiscalEmEdicao);
            }
        }

        private void ValideNotaFiscal()
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();
            var configuracoesZeus = servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus((EnumModeloNotaFiscal)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

            if (_notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe != EnumTipoEmissaoDanfe.EMISSAONORMAL)
            {
                configuracoesZeus.tpEmis = (NFe.Classes.Informacoes.Identificacao.Tipos.TipoEmissao)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe;
            }

            var servicoNFe = new ServicosNFe(configuracoesZeus);

            RetornoNFeRetAutorizacao retornoConsulta = null;

            while (retornoConsulta == null || retornoConsulta.Retorno.cStat == 105)
            {
                Thread.Sleep(200);
                
                retornoConsulta = servicoNFe.NFeRetAutorizacao(_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.NumeroReciboLote);
            }   

            if (retornoConsulta.Retorno.cStat == 104)
            {
                if (retornoConsulta.Retorno.protNFe[0].infProt.cStat != 100)
                {
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDevolvida = retornoConsulta.Retorno.cStat + " - " + retornoConsulta.Retorno.xMotivo;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro = retornoConsulta.Retorno.protNFe[0].infProt.cStat + " - " + retornoConsulta.Retorno.protNFe[0].infProt.xMotivo;

                    if (retornoConsulta.Retorno.protNFe[0].infProt.cStat == 301 ||
                        retornoConsulta.Retorno.protNFe[0].infProt.cStat == 302 ||
                        retornoConsulta.Retorno.protNFe[0].infProt.cStat == 303)
                    {
                        _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.DENEGADA;
                    }
                    else
                    {
                        _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.REJEITADA;
                    }
                }
                else
                {
                    var informacoesProtocolo = retornoConsulta.Retorno.protNFe[0].infProt;

                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDevolvida = informacoesProtocolo.cStat + " - " + retornoConsulta.Retorno.protNFe[0].infProt.xMotivo;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro = string.Empty;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.AUTORIZADA;

                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal = _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal ?? new InformacoesProtocoloAutorizacaoNotaFiscal();

                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.Id = informacoesProtocolo.Id;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.ChaveNfe = informacoesProtocolo.chNFe;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.DataHoraRecibo = informacoesProtocolo.dhRecbto.Date;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.DigestValue = informacoesProtocolo.digVal;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.Motivo = informacoesProtocolo.xMotivo;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.NumeroProtocolo = informacoesProtocolo.nProt;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.Status = informacoesProtocolo.cStat;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.TipoAmbiente = (int)informacoesProtocolo.tpAmb;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoAplicativo = informacoesProtocolo.verAplic;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoNota = "4.00";

                    //ConversorNotaFiscalAkilParaZeus conversorNotaFiscalAkilParaZeus = new ConversorNotaFiscalAkilParaZeus();
                    //var notaFiscalZeus = conversorNotaFiscalAkilParaZeus.ConvertaNotaAutorizadaAkilParaZeus(_notaFiscalEmEdicao);

                    //notaFiscalZeus.NFe.Assina();

                    //_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Xml = notaFiscalZeus.ObterXmlString();
                }
            }
            else
            {
                _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDevolvida = retornoConsulta.Retorno.cStat + " - " + retornoConsulta.Retorno.xMotivo;

                if (retornoConsulta.Retorno.protNFe.Count > 0)
                {
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro = retornoConsulta.Retorno.protNFe[0].infProt.cStat + " - " + retornoConsulta.Retorno.protNFe[0].infProt.xMotivo;
                }

                _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.REJEITADA;
            }

            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
            servicoNotaFiscal.Atualize(_notaFiscalEmEdicao);

            PreenchaNotaFiscalEmEdicao();
            
            if (_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.AUTORIZADA)
            {   
                throw new Exception("Ocorreu o seguinte erro ao validar a nota: " + _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro);
            }

            if (_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.PEDIDODEVENDAS)
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                var pedido = servicoPedidoDeVenda.Consulte(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);

                pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.EMITIDONFE;

                servicoPedidoDeVenda.Atualize(pedido);
            }
            
            servicoNotaFiscal.Atualize(_notaFiscalEmEdicao);

            PreenchaNotaFiscalEmEdicao();
        }

        private void ValideNotaFiscalRejeitada()
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();
            var configuracoesZeus = servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus((EnumModeloNotaFiscal)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

            if (_notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe != EnumTipoEmissaoDanfe.EMISSAONORMAL)
            {
                configuracoesZeus.tpEmis = (NFe.Classes.Informacoes.Identificacao.Tipos.TipoEmissao)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe;
            }

            var servicoNFe = new ServicosNFe(configuracoesZeus);

            RetornoNfeConsultaProtocolo retornoConsulta = null;

            while (retornoConsulta == null || retornoConsulta.Retorno.cStat == 105)
            {
                Thread.Sleep(200);

                if (!string.IsNullOrEmpty(_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.ChaveDeAcesso))
                {  
                    retornoConsulta = servicoNFe.NfeConsultaProtocolo(_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.ChaveDeAcesso.ToString());
                }
                else
                {
                    return;
                }
            }

            if (retornoConsulta.Retorno.cStat == 100)
            {
                if (retornoConsulta.Retorno.protNFe.infProt.cStat != 100)
                {
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDevolvida = retornoConsulta.Retorno.cStat + " - " + retornoConsulta.Retorno.xMotivo;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro = retornoConsulta.Retorno.protNFe.infProt.cStat + " - " + retornoConsulta.Retorno.protNFe.infProt.xMotivo;

                    if (retornoConsulta.Retorno.protNFe.infProt.cStat == 301 ||
                        retornoConsulta.Retorno.protNFe.infProt.cStat == 302 ||
                        retornoConsulta.Retorno.protNFe.infProt.cStat == 303)
                    {
                        _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.DENEGADA;
                    }
                    else
                    {
                        _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.REJEITADA;
                    }
                }
                else
                {
                    var informacoesProtocolo = retornoConsulta.Retorno.protNFe.infProt;

                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDevolvida = informacoesProtocolo.cStat + " - " + retornoConsulta.Retorno.protNFe.infProt.xMotivo;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro = string.Empty;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.AUTORIZADA;

                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal = _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal ?? new InformacoesProtocoloAutorizacaoNotaFiscal();

                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.Id = informacoesProtocolo.Id;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.ChaveNfe = informacoesProtocolo.chNFe;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.DataHoraRecibo = informacoesProtocolo.dhRecbto.Date;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.DigestValue = informacoesProtocolo.digVal;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.Motivo = informacoesProtocolo.xMotivo;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.NumeroProtocolo = informacoesProtocolo.nProt;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.Status = informacoesProtocolo.cStat;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.TipoAmbiente = (int)informacoesProtocolo.tpAmb;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoAplicativo = informacoesProtocolo.verAplic;
                    _notaFiscalEmEdicao.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoNota = "4.00";

                    //ConversorNotaFiscalAkilParaZeus conversorNotaFiscalAkilParaZeus = new ConversorNotaFiscalAkilParaZeus();
                    //var notaFiscalZeus = conversorNotaFiscalAkilParaZeus.ConvertaNotaAutorizadaAkilParaZeus(_notaFiscalEmEdicao);

                    //notaFiscalZeus.NFe.Assina();

                    //_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Xml = notaFiscalZeus.ObterXmlString();
                }
            }            
            else
            {
                _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDevolvida = retornoConsulta.Retorno.cStat + " - " + retornoConsulta.Retorno.xMotivo;

                if (retornoConsulta.Retorno.protNFe != null)
                {
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro = retornoConsulta.Retorno.protNFe.infProt.cStat + " - " + retornoConsulta.Retorno.protNFe.infProt.xMotivo;
                }

                _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.REJEITADA;
            }

            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
            servicoNotaFiscal.Atualize(_notaFiscalEmEdicao);

            PreenchaNotaFiscalEmEdicao();

            if (_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.AUTORIZADA)
            {
                if (!_EhRejeicao)
                    throw new Exception("Ocorreu o seguinte erro ao validar a nota: " + _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDeErro);
                else
                    return;
            }

            if (_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.PEDIDODEVENDAS)
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                var pedido = servicoPedidoDeVenda.Consulte(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);

                pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.EMITIDONFE;

                servicoPedidoDeVenda.Atualize(pedido);
            }

            servicoNotaFiscal.Atualize(_notaFiscalEmEdicao);

            PreenchaNotaFiscalEmEdicao();
        }


        private void ExibaFormAviso()
        {
            _exibaFormAviso = true;

            _threadFormAviso = new Thread(new ThreadStart(this.ExibaFormAvisoThread));
            _threadFormAviso.Start();
        }

        private void ExibaFormAvisoThread()
        {
            Thread.Sleep(150);

            if (_exibaFormAviso)
            {
                _formAvisoGerandoEEnviandoNfe = new FormAvisoGerandoEEnviandoNfe();
                _formAvisoGerandoEEnviandoNfe.AbrirTelaModal();
            }
        }

        private void GereMovimentacaoNotaFiscalOutrasSaidas()
        {
            if(_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.AUTORIZADA && 
                _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.OUTRASSAIDAS)
            {
                ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();

                EnumTipoMovimentacao tipoMovimentacao = txtNotaDe.Text == "ENTRADA"? EnumTipoMovimentacao.ENTRADA: EnumTipoMovimentacao.SAIDA;
                servicoNotaFiscal.MovimenteEstoqueNotaFiscalOutrasSaidas(_notaFiscalEmEdicao, tipoMovimentacao);

                servicoNotaFiscal.InativeContasAReceberDaNotaFiscalOutrasSaidas(_notaFiscalEmEdicao);
            }
        }
       

        private void FecheFormAvisoThread()
        {
            _exibaFormAviso = false;

            if (_formAvisoGerandoEEnviandoNfe != null)
            {
                _formAvisoGerandoEEnviandoNfe.Close();
                _formAvisoGerandoEEnviandoNfe.Dispose();
                _formAvisoGerandoEEnviandoNfe = null;
            }

            this.Focus();
        }

        //Método que retorna se está configurado para NFCe para não validar o CPF/CNPJ
        private bool ValidaCpfCnpjNFCe(String CpfCnpj)
        {
            if (_notaFiscalEmEdicao.Destinatario.TipoPessoa == EnumTipoPessoa.PESSOAFISICA)
            {
                if (!ValidacoesGerais.CpfEstahValido(CpfCnpj))
                {
                    MessageBox.Show("CPF inconsistente.");

                    return false;
                }
            }
            else
            {
                if (!ValidacoesGerais.CnpjEstahValido(CpfCnpj))
                {
                    MessageBox.Show("CNPJ inconsistente.");

                    return false;
                }
            }

            return true;
        }

        private void VerificaEMudaTipoEmissaoNotaFiscal()
        {
            _modeloEmissaoFiscal = _modeloEmissaoFiscal != 0 ? _modeloEmissaoFiscal : (EnumModeloNotaFiscal)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal;

            if (_modeloEmissaoFiscal == EnumModeloNotaFiscal.NFE)
                _notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe = chkEmitirNotaFiscalContigencia.Checked == true ? EnumTipoEmissaoDanfe.CONTINGENCIASVCRS : EnumTipoEmissaoDanfe.EMISSAONORMAL;
            else
                _notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe = chkEmitirNotaFiscalContigencia.Checked == true ? EnumTipoEmissaoDanfe.CONTINGENCIAOFFLINE : EnumTipoEmissaoDanfe.EMISSAONORMAL;
        }

        #endregion

        #region " IMPRESSÃO NOTA "

        public NFe.Danfe.Base.NFCe.ConfiguracaoDanfeNfce RetorneConfiguracaoDanfeNfceZeus()
        {
            NFe.Danfe.Base.NFCe.ConfiguracaoDanfeNfce configuracaoDanfeNfce;

            ServicoEmpresa servicoEmpresa = new ServicoEmpresa(false, false);
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            configuracaoDanfeNfce = new NFe.Danfe.Base.NFCe.ConfiguracaoDanfeNfce(detalheVendaNormal: NFe.Danfe.Base.NfceDetalheVendaNormal.DuasLinhas,
                detalheVendaContigencia: NFe.Danfe.Base.NfceDetalheVendaContigencia.DuasLinhas,
                logomarca: empresa.DadosEmpresa.Foto,
                imprimeDescontoItem: true);


            return configuracaoDanfeNfce;
        }
        
        public NFe.Impressao.NFCe.ConfiguracaoDanfeNfce RetorneOutrasConfiguracoes()
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa(false, false);
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            var parametros = servicoParametros.ConsulteParametros();

            if (string.IsNullOrEmpty(parametros.ParametrosFiscais.IdCsc))
            {
                ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);
                var csc = servicoParametros.GereCodigoCsc();

                parametros.ParametrosFiscais.IdCsc = csc.IdCsc;
                parametros.ParametrosFiscais.CodigoCsc = csc.CodigoCsc;

                servicoParametros.Atualize(parametros);
            }

            NFe.Impressao.NFCe.ConfiguracaoDanfeNfce configuracaoDanfeNfce = new NFe.Impressao.NFCe.ConfiguracaoDanfeNfce(NFe.Impressao.NfceDetalheVendaNormal.UmaLinha,
            NFe.Impressao.NfceDetalheVendaContigencia.UmaLinha,
            parametros.ParametrosFiscais.IdCsc.PadLeft(6, '0'),
            parametros.ParametrosFiscais.CodigoCsc,
            logomarca: empresa.DadosEmpresa.Foto,
            imprimeDescontoItem: true);

            return configuracaoDanfeNfce;
        }

        private void ImprimirNota()
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();
            string caminhoACBR = "C:\\Programax";

            if (!string.IsNullOrEmpty(parametros.ParametrosCadastros.CaminhoACBR))
            {
                caminhoACBR = parametros.ParametrosCadastros.CaminhoACBR;
            }

            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();
            servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus((EnumModeloNotaFiscal)_notaFiscalEmEdicao.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

            ConversorNotaFiscalAkilParaZeus conversorNotaFiscalAkilParaZeus = new ConversorNotaFiscalAkilParaZeus();
            var notaFiscalZeus = conversorNotaFiscalAkilParaZeus.ConvertaNotaAutorizadaAkilParaZeus(_notaFiscalEmEdicao);

            notaFiscalZeus.NFe.Assina();

            string nomeNotaFiscal = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.ChaveDeAcesso + ".xml";

            if (!Directory.Exists(@caminhoACBR+"\\NotaFiscal"))
            {
                Directory.CreateDirectory(@caminhoACBR+"\\NotaFiscal");
            }

            if (!Directory.Exists(@caminhoACBR+"\\NotaFiscal\\XML\\"))
            {
                Directory.CreateDirectory(@caminhoACBR+"\\NotaFiscal\\XML\\");
            }

            if (!Directory.Exists(@caminhoACBR+"\\NotaFiscal\\ACBR\\"))
            {
                Directory.CreateDirectory(@caminhoACBR+"\\NotaFiscal\\ACBR\\");
            }

            if (!Directory.Exists(@caminhoACBR+"\\NotaFiscal\\ACBR\\SAIDA\\"))
            {
                Directory.CreateDirectory(@caminhoACBR+"\\NotaFiscal\\ACBR\\SAIDA\\");
            }

            if (!Directory.Exists(@caminhoACBR+"\\NotaFiscal\\ACBR\\ENTRADA\\"))
            {
                Directory.CreateDirectory(@caminhoACBR+"\\NotaFiscal\\ACBR\\ENTRADA\\");
            }

            notaFiscalZeus.SalvarArquivoXml(@caminhoACBR+"\\NotaFiscal\\XML\\" + nomeNotaFiscal);

            string textoArquivo;

            if (System.IO.File.Exists(@caminhoACBR+"\\NotaFiscal\\XML\\" + nomeNotaFiscal))
            {
                textoArquivo = System.IO.File.ReadAllText(@caminhoACBR+"\\NotaFiscal\\XML\\" + nomeNotaFiscal);

                if (textoArquivo != string.Empty)
                {   
                    string lines = "<?xml version="+'"'+"1.0"+'"'+"encoding="+'"'+"UTF-8"+'"'+"?>" + textoArquivo.ToString();

                    System.IO.File.WriteAllText(@caminhoACBR+"\\NotaFiscal\\XML\\" + nomeNotaFiscal, lines);
                }
            }

            if (_notaFiscalEmEdicao.IdentificacaoNotaFiscal.TipoEmissaoDanfe == EnumTipoEmissaoDanfe.CONTINGENCIAOFFLINE)
            {
                var configuracoes = RetorneConfiguracaoDanfeNfceZeus();
                var outrasConfiguracoes = RetorneOutrasConfiguracoes();

                string arquivoXML = @caminhoACBR+"\\NotaFiscal\\XML\\" + nomeNotaFiscal;

                nfeProc proc = new nfeProc();

                var nproc = ExtNfeProc.CarregarDeArquivoXml(proc, arquivoXML);

                var danfe = new DanfeFrNfce(nproc, configuracoes, outrasConfiguracoes.cIdToken, outrasConfiguracoes.CSC);

                danfe.Visualizar(true);

                //danfe.Imprimir(true, "");
            }
            else
            {   
                string comandoAcbrImprimirNota = "NFE.IMPRIMIRDANFE(\"" + @caminhoACBR+"\\NotaFiscal\\XML\\" + nomeNotaFiscal + "\")";                
                //string comandoAcbrImprimirNota = "NFE.IMPRIMIRDANFEPDF(\"" + @"C:\Programax\NotaFiscal\XML\" + nomeNotaFiscal + "\")";

                System.IO.File.WriteAllText(@caminhoACBR+"\\NotaFiscal\\ACBR\\ENTRADA\\IMPRIMIR_NOTA.txt", comandoAcbrImprimirNota);
                
                //var arquivoAbrir = nomeNotaFiscal.Split('.');

                //System.IO.File.Open(@"C:\Programax\NotaFiscal\" + arquivoAbrir[0] + "-nfe.pdf", FileMode.Open);

                //System.Diagnostics.Process.Start(@"C:\Programax\NotaFiscal\" + arquivoAbrir[0] + "-nfe.pdf");

            }
        }

        #endregion

        #endregion

        #region " CLASSES AUXILIARES "

        private class ItemGrid
        {
            public int Id { get; set; }

            public int IdProduto { get; set; }

            public string Descricao { get; set; }

            public string CstCsosn { get; set; }

            public string Cfop { get; set; }

            public string Unidade { get; set; }

            public string ValorUnitario { get; set; }

            public string Quantidade { get; set; }

            public string QuantidadeEstocar { get; set; }

            public string Desconto { get; set; }

            public string Frete { get; set; }

            public string ValorTotal { get; set; }
        }

        private class CfopAuxiliar
        {
            public int Id { get; set; }

            public string DescricaoFormatada { get; set; }
        }

        private class DuplicataGrid
        {
            public int Id { get; set; }

            public string Parcela { get; set; }

            public string NumeroDuplicata { get; set; }

            public string DataVencimento { get; set; }

            public string Valor { get; set; }
        }

        #endregion
               
    }
}
