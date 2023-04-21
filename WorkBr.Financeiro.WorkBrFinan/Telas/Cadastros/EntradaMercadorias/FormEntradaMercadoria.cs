using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using CTe.Utils; - Implementação futura
//using CTe.Classes; Implementação futura
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.AjusteFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Servico.Cadastros.UnidadeMedidaServ;
using Programax.Easy.Servico.Fiscal.CfopServ;
using Programax.Easy.Servico.Movimentacao.AjusteFiscalServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Nfe.NFEObj;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.View.Telas.Fiscal.Ncms;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EnderecoServ;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Report.RelatoriosDevExpress.Estoque;
using NFe.Classes;
using NFe.Utils.NFe;
using NFe.Utils.Tributacao.Estadual;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using NFe.Classes.Informacoes.Emitente;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;

namespace Programax.Easy.View.Telas.Cadastros.EntradaMercadorias
{
    public partial class FormEntradaMercadoria : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Produto _produtoEmEdicao;
        private Ncm _ncmProdutoEmEdicao;
        private EntradaMercadoria _entradaMercadoriaSelecionada;

        private Empresa _empresa;

        private ItemEntrada _itemEntradaEmEdicao;
        private List<ItemEntrada> _listaItensEntrada;
        private List<AjusteFiscal> _listaAjustesFiscais;

        private List<Cfop> _listaCfops;
        private List<UnidadeMedida> _listaUnidadesMedida;

        private FinanceiroEntrada _financeiroEntradaEmEdicao;
        private List<FinanceiroEntrada> _listaFinanceiroEntrada;

        private int _idEntradaMercadoria;

        private bool _variavelControleConteudoAlteradoDiretoNoCampotxtValorFreteTotalNota;

        private double _ValorTotalDaNotaOriginal;

        #endregion

        #region " CONSTRUTOR "

        public FormEntradaMercadoria()
        {
            InitializeComponent();

            this.NomeDaTela = "Entrada de Mercadorias";

            _listaItensEntrada = new List<ItemEntrada>();
            _listaFinanceiroEntrada = new List<FinanceiroEntrada>();

            PreenchaCboAjustesFiscais();

            PreenchaCboOrigem();
            PreenchaCboCstCsosn();
            PreenchaCboUnidades();
            PreenchaCboCfop();
            PreenchaCboModelosDocumentoFiscal();
            PreenchaCboTipoFrete();
            PreenchaCboNaturezaOperacao();
            PreenchaCboFormaPagamento();
            PreenchaCboCondicaoPagamento();
            PreenchaCbMotivosDesoneracao();

            CarregaEmpresa();

            AtualizeGridFechamento();

            txtDataEmissao.DateTime = DateTime.Now.Date;
            txtDataEntrada.DateTime = DateTime.Now.Date;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            LimpeFormulario();

            this.ActiveControl = txtNumeroNfe;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        #region " DADOS PRINCIPAIS "

        private void txtNumeroNfe_Leave(object sender, EventArgs e)
        {
            if (_idEntradaMercadoria == 0)
            {
                LibereFormulario();
                if (txtNumeroNfe.Text != string.Empty)
                {
                    int codigoConsulta = txtNumeroNfe.Text.ToInt();
                    ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                    


                    var entrada = servicoEntradaMercadoria.ConsulteListaNumero(txtNumeroNfe.Text, 0);

                    if (entrada != null)
                    {
                        if (entrada.Count == 1)
                        {
                            _entradaMercadoriaSelecionada = servicoEntradaMercadoria.ConsulteNotaEntrada(txtNumeroNfe.Text);
                            EditeEntrada(_entradaMercadoriaSelecionada);
                        }

                    }
                    else
                    {
                        FormPesquisaDeEntradaDeMercadorias formPesquisaDeEntrada = new FormPesquisaDeEntradaDeMercadorias(0);

                        var entradaMercadoria = formPesquisaDeEntrada.ExibaPesquisaDeEntradas(0);
                        if (entradaMercadoria != null)
                        {
                            EditeEntrada(entradaMercadoria);
                        }
                    }
                }
            }
        }

       

        private void txtSerie_Leave(object sender, EventArgs e)
        {
            if (_idEntradaMercadoria == 0)
            {
                LibereFormulario();
            }
        }

        private void btnPesquisaFornecedor_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var fornecedor = formPessoaPesquisa.PesquisePessoaFornecedoraAtiva();

            if (fornecedor != null)
            {
                PreenchaFornecedor(fornecedor);
            }
        }

        private void txtIdFornecedor_Leave(object sender, EventArgs e)
        {
            if (_idEntradaMercadoria == 0)
            {
                if (!string.IsNullOrEmpty(txtIdFornecedor.Text))
                {
                    ServicoPessoa servicoPessoa = new ServicoPessoa();

                    var fornecedor = servicoPessoa.ConsulteFornecedorAtivo(txtIdFornecedor.Text.ToInt());

                    PreenchaFornecedor(fornecedor, true);
                }
                else
                {
                    PreenchaFornecedor(null);
                }
            }
        }

        private void btnPesquisaEntrada_Click(object sender, EventArgs e)
        {
            FormPesquisaDeEntradaDeMercadorias formPesquisaDeEntrada = new FormPesquisaDeEntradaDeMercadorias(0);

            var entrada = formPesquisaDeEntrada.ExibaPesquisaDeEntradas(0);

            if (entrada != null)
            {
                EditeEntrada(entrada);
            }
        }

        #endregion

        #region " GUIA GERAL "

        private void cboAjustesFiscais_EditValueChanged(object sender, EventArgs e)
        {
            if (cboAjustesFiscais.EditValue == null)
            {
                txtDescricaoAjustesFiscais.Text = string.Empty;
            }
            else
            {
                var ajusteFiscal = _listaAjustesFiscais.FirstOrDefault(ajuste => ajuste != null && ajuste.Id == cboAjustesFiscais.EditValue.ToInt());

                txtDescricaoAjustesFiscais.Text = ajusteFiscal.Descricao;
            }
        }

        private void txtIdTransportadora_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdTransportadora.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var transportadora = servicoPessoa.ConsulteTransportadoraAtiva(txtIdTransportadora.Text.ToInt());

                PreenchaTransportadora(transportadora, true);
            }
            else
            {
                PreenchaTransportadora(null);
            }
        }

        private void btnPesquisaTransportadora_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var transportadora = formPessoaPesquisa.PesquisePessoaTransportadora();

            if (transportadora != null)
            {
                PreenchaTransportadora(transportadora);
            }
        }

        private void txtValoresCapa_Leave(object sender, EventArgs e)
        {
            AtualizeGridFechamento();
        }

        private void cboTipoFrete_EditValueChanged(object sender, EventArgs e)
        {
            if (cboTipoFrete.EditValue == null)
            {
                txtIdTransportadora.Text = string.Empty;
                txtIdTransportadora.Enabled = false;

                btnPesquisaTransportadora.Enabled = false;

                txtNomeTransportadora.Text = string.Empty;
                txtNomeTransportadora.Enabled = false;

                txtNumeroConhecimentoFrete.Text = string.Empty;
                txtNumeroConhecimentoFrete.Enabled = false;

                txtValorFreteTotalNota.Text = string.Empty;
                txtValorFreteTotalNota.Enabled = false;
            }
            else
            {
                EnumTipoFrete tipoFrete = (EnumTipoFrete)cboTipoFrete.EditValue;

                if (tipoFrete == EnumTipoFrete.SEMCOBRANCADEFRETE)
                {
                    txtIdTransportadora.Text = string.Empty;
                    txtIdTransportadora.Enabled = false;

                    btnPesquisaTransportadora.Enabled = false;

                    txtNomeTransportadora.Text = string.Empty;
                    txtNomeTransportadora.Enabled = false;

                    txtNumeroConhecimentoFrete.Text = string.Empty;
                    txtNumeroConhecimentoFrete.Enabled = false;

                    txtValorFreteTotalNota.Text = string.Empty;
                    txtValorFreteTotalNota.Enabled = false;
                }
                else
                {
                    txtIdTransportadora.Enabled = true;
                    btnPesquisaTransportadora.Enabled = true;
                    txtNomeTransportadora.Enabled = true;

                    txtNumeroConhecimentoFrete.Enabled = true;
                    txtValorFreteTotalNota.Enabled = true;
                }
            }

            AtualizeGridFechamento();
        }

        #endregion

        #region " GUIA ITENS "

        private void btnPesquisaProduto2_Click(object sender, EventArgs e)
        {
            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProduto();

            if (produto != null)
            {
                PreenchaCamposDoProduto(produto);
            }
        }

        private void txtCodigoDeBarrasProduto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoDeBarrasProduto.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(txtCodigoDeBarrasProduto.Text);
                PreenchaCamposDoProduto(produto);
            }
        }

        private void txtIdProduto_Leave(object sender, EventArgs e)
        {
            ServicoProduto servicoProduto = new ServicoProduto();

            var produto = servicoProduto.ConsulteProdutoAtivo(txtIdProduto.Text.ToInt());

            PreenchaCamposDoProduto(produto);
        }

        private void cboCstCsosn_EditValueChanged(object sender, EventArgs e)
        {
            DesabiliteCamposIcmsESubstituicaoTributaria();

            if (cboCstCsosn.EditValue == null)
            {
                return;
            }

            EnumCstCsosn cstCsosn = (EnumCstCsosn)cboCstCsosn.EditValue;

            if (cstCsosn == EnumCstCsosn.NORMAL00 ||
                cstCsosn == EnumCstCsosn.NORMAL90 ||
                cstCsosn == EnumCstCsosn.SIMPLES101)
            {
                HabiliteCamposIcms(false);
            }

            else if (cstCsosn == EnumCstCsosn.NORMAL10 ||
                      cstCsosn == EnumCstCsosn.NORMAL60 ||
                      cstCsosn == EnumCstCsosn.SIMPLES201)
            {
                HabiliteCamposIcms(false);
                HabiliteCamposSubstituicaoTributaria();
            }

            else if (cstCsosn == EnumCstCsosn.NORMAL20)
            {
                HabiliteCamposIcms(true);
            }

            else if (cstCsosn == EnumCstCsosn.NORMAL30 ||
                      cstCsosn == EnumCstCsosn.SIMPLES202 ||
                      cstCsosn == EnumCstCsosn.SIMPLES203 ||
                      cstCsosn == EnumCstCsosn.SIMPLES900)
            {
                HabiliteCamposSubstituicaoTributaria();
            }

            else if (cstCsosn == EnumCstCsosn.NORMAL70)
            {
                HabiliteCamposIcms(true);
                HabiliteCamposSubstituicaoTributaria();
            }

            if (cstCsosn == EnumCstCsosn.NORMAL20 ||
                cstCsosn == EnumCstCsosn.NORMAL30 ||
                cstCsosn == EnumCstCsosn.NORMAL40 ||
                cstCsosn == EnumCstCsosn.NORMAL41 ||
                cstCsosn == EnumCstCsosn.NORMAL50 ||
                cstCsosn == EnumCstCsosn.NORMAL70 ||
                cstCsosn == EnumCstCsosn.NORMAL90)
            {
                HabiliteCamposIcmsDesoneracao();
            }

        }

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            InsiraOuAtualizeItemNaLista();
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeItem();
        }

        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            var itemDaLista = _listaItensEntrada.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

            EditeItem(itemDaLista);
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir este item?", "Exclusão de item", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _listaItensEntrada.Remove(_itemEntradaEmEdicao);

                LimpeItem();

                GeraIdParaCadaItem();

                AtualizeGridItens();
            }
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

        private void btnExcluirFinanceiro_Click(object sender, EventArgs e)
        {
            var financeiroDaLista = _listaFinanceiroEntrada.FirstOrDefault(financeiro => financeiro.Id == colunaIdFinanceiro.View.GetFocusedRowCellValue(colunaIdFinanceiro).ToInt());

            string mensagem = "Deseja excluir a parcela " + financeiroDaLista.Parcela + "?";

            if (MessageBox.Show(mensagem, "Exclusão de parcela", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _listaFinanceiroEntrada.Remove(financeiroDaLista);

                LimpeCamposFinanceiro();
                GereIdParaCadaParcelaFinanceiro();
                PreenchaGridFinanceiro();
            }
        }

        private void gcParcelasFinanceiro_DoubleClick(object sender, EventArgs e)
        {
            var financeiroDaLista = _listaFinanceiroEntrada.FirstOrDefault(financeiro => financeiro.Id == colunaIdFinanceiro.View.GetFocusedRowCellValue(colunaIdFinanceiro).ToInt());

            EditeParcelaFinanceiro(financeiroDaLista);
        }

        private void txtValorDuplicata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsiraOuAtualizeParcelaFinanceiro();
            }
        }

        #endregion

        #region " EVENTOS ELEMENTOS DA BARRA DE BOTÕES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Salve();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnImportarXml_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                string caminhoArquivo = openFileDialog1.FileName;

                var nfeproc = new nfeProc().CarregarDeArquivoXml(caminhoArquivo);

                var chaveDeAcesso = nfeproc.protNFe.infProt.chNFe;

                var notaFiscal = nfeproc.NFe.infNFe;

                //notaFiscal.CarregueNotaFiscal(caminhoArquivo);

                //var xmlCte = CTe.Classes.CTe.LoadXmlArquivo(caminhoArquivo);

                //var NotaCte = CTe.Utils.CTe.ExtCTe.CarregarDeArquivoXml(xmlCte, caminhoArquivo);

                CarregueXml(notaFiscal, chaveDeAcesso);

                this.Cursor = Cursors.Default;
            }
        }

        private void btnConcluirEntrada_Click(object sender, EventArgs e)
        {
            ConcluirEntrada();
        }

        private void btnCancelarEntrada_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ao cancelar a entrada, os itens irão sair do estoque e as contas a pagar serão canceladas. Deseja Continuar?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            Action actionCancelar = () =>
            {
                ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();
                servicoEntradaMercadoria.CancelamentoEntrada(_idEntradaMercadoria);

                LimpeFormulario();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionCancelar);
        }

        #endregion

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " PREENCHIMENTO DE FORNECEDOR, TRANSPORTADORA E CFOP "

        private void PreenchaFornecedor(Pessoa fornecedor, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (fornecedor != null)
            {
                txtIdFornecedor.Text = fornecedor.Id.ToString();
                txtCnpj.Text = fornecedor.DadosGerais.CpfCnpj;
                txtNomeFornecedor.Text = fornecedor.DadosGerais.Razao;

                LibereFormulario();
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Fornecedor nao encontrado!", "Fornecedor não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdFornecedor.Focus();
                }

                txtIdFornecedor.Text = string.Empty;
                txtCnpj.Text = string.Empty;
                txtNomeFornecedor.Text = string.Empty;
            }
        }

        private void PreenchaTransportadora(Pessoa transportadora, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (transportadora != null)
            {
                txtIdTransportadora.Text = transportadora.Id.ToString();
                txtNomeTransportadora.Text = transportadora.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Transportadora nao encontrada!", "Transportadora não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdTransportadora.Focus();
                }

                txtIdTransportadora.Text = string.Empty;
                txtNomeTransportadora.Text = string.Empty;
            }
        }

        #endregion

        #region " PREENCHIMENTO DE COMBO BOX "

        private void CarregaEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();

            _empresa = servicoEmpresa.ConsulteUltimaEmpresa();
        }

        private void PreenchaCboModelosDocumentoFiscal()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModeloDocumentoFiscal>();

            var notaFiscalAvulsa = lista.Find(x => (EnumModeloDocumentoFiscal)x.Valor == EnumModeloDocumentoFiscal.NOTAFISCALAVULSA);

            lista.Remove(notaFiscalAvulsa);
            lista.Insert(1, notaFiscalAvulsa);

            lista.Insert(0, null);

            cboModelosDocumentoFiscal.Properties.DataSource = lista;
            cboModelosDocumentoFiscal.Properties.DisplayMember = "Descricao";
            cboModelosDocumentoFiscal.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboTipoFrete()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoFrete>();
            lista.Insert(0, null);

            cboTipoFrete.Properties.DataSource = lista;
            cboTipoFrete.Properties.DisplayMember = "Descricao";
            cboTipoFrete.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboAjustesFiscais()
        {
            ServicoAjusteFiscal servicoAjusteFiscal = new ServicoAjusteFiscal();

            _listaAjustesFiscais = servicoAjusteFiscal.ConsulteLista();
            _listaAjustesFiscais.Insert(0, null);

            cboAjustesFiscais.Properties.DataSource = _listaAjustesFiscais;
            cboAjustesFiscais.Properties.DisplayMember = "Codigo";
            cboAjustesFiscais.Properties.ValueMember = "Id";
        }

        private void PreenchaCboOrigem()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumOrigem>();
            lista.Insert(0, null);

            cboOrigem.Properties.DataSource = lista;
            cboOrigem.Properties.DisplayMember = "Descricao";
            cboOrigem.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboCstCsosn()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCstCsosn>();
            lista.Insert(0, null);

            cboCstCsosn.Properties.DataSource = lista;
            cboCstCsosn.Properties.DisplayMember = "Descricao";
            cboCstCsosn.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboUnidades()
        {
            ServicoUnidadeMedida servicoUnidadeMedida = new ServicoUnidadeMedida();

            var unidades = servicoUnidadeMedida.ConsulteLista();

            _listaUnidadesMedida = new List<UnidadeMedida>();
            _listaUnidadesMedida.AddRange(unidades);

            unidades.Insert(0, null);

            cboUnidade.Properties.DataSource = unidades;
            cboUnidade.Properties.DisplayMember = "Abreviacao";
            cboUnidade.Properties.ValueMember = "Id";
        }

        private void PreenchaCboCfop()
        {
            ServicoCfop servicoCfop = new ServicoCfop();

            _listaCfops = servicoCfop.ConsulteLista("", "", "A", EnumTipoCfop.ENTRADA);

            List<CfopAuxiliar> listaCfopsAuxiliares = new List<CfopAuxiliar>();

            foreach (var cfop in _listaCfops)
            {
                CfopAuxiliar cfopAuxiliar = new CfopAuxiliar();
                cfopAuxiliar.Id = cfop.Id;
                cfopAuxiliar.DescricaoFormatada = cfop.Codigo + " - " + cfop.Descricao;

                listaCfopsAuxiliares.Add(cfopAuxiliar);
            }

            listaCfopsAuxiliares.Insert(0, null);

            cboCfop.Properties.DataSource = listaCfopsAuxiliares;
            cboCfop.Properties.DisplayMember = "DescricaoFormatada";
            cboCfop.Properties.ValueMember = "Id";
        }

        private void PreenchaCboNaturezaOperacao()
        {
            ServicoNaturezaOperacao servicoNaturezaOperacao = new ServicoNaturezaOperacao();

            var lista = servicoNaturezaOperacao.ConsulteListaAtiva();

            lista.Insert(0, null);

            cboNaturezaOperacao.Properties.DataSource = lista;
            cboNaturezaOperacao.Properties.DisplayMember = "Descricao";
            cboNaturezaOperacao.Properties.ValueMember = "Id";
        }

        private void PreenchaCboCondicaoPagamento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCondicaoPagamentoNota>();

            cboCondicaoPagamento.Properties.DataSource = lista;
            cboCondicaoPagamento.Properties.DisplayMember = "Descricao";
            cboCondicaoPagamento.Properties.ValueMember = "Valor";

            cboCondicaoPagamento.EditValue = EnumCondicaoPagamentoNota.AVISTA;
        }

        private void PreenchaCbMotivosDesoneracao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumMotivoDesoneracaoProduto>();

            lista.Insert(0, null);

            cboMotivoDesoneracao.Properties.DataSource = lista;
            cboMotivoDesoneracao.Properties.DisplayMember = "Descricao";
            cboMotivoDesoneracao.Properties.ValueMember = "Valor";
        }

        #endregion

        #region " MÉTODOS RESPONSÁVEIS POR SALVAR "

        private void ConcluirEntrada()
        {
            if (MessageBox.Show("Deseja concluir esta entrada?", "Concluir entrada", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            Action actionSalvarEValidarEntrada = () =>
            {
                ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                var entradaMercadoria = RetorneEntradaMercadoriaEmEdicao();

                servicoEntradaMercadoria.ValideConculsao(entradaMercadoria);

                if (!VerifiqueSeUsuarioDesejaProsseguirQdTotalNotaDiferenteDeTotalParcelas(entradaMercadoria) ||
                    !VerifiqueSeUsuarioDesejaProsseguirQdNaoHouverParcelasECompraAPrazo(entradaMercadoria) ||
                    !VerifiqueSeUsuarioDesejaProsseguirQdHouverParcelasECompraAVista(entradaMercadoria))
                {
                    return;
                }

                Action actionSalvar = () =>
                {
                    servicoEntradaMercadoria.ConcluaEntrada(entradaMercadoria);

                    if (MessageBox.Show("Deseja imprimir entrada?", "Impressão entrada", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    {
                        _idEntradaMercadoria = entradaMercadoria.Id;

                        ImprimaRelatorio();
                    }

                    LimpeFormulario();
                };

                TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Entrada concluída com sucesso!");
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvarEValidarEntrada, exibirMensagemDeSucesso: false);
        }

        private bool VerifiqueSeUsuarioDesejaProsseguirQdTotalNotaDiferenteDeTotalParcelas(EntradaMercadoria entrada)
        {
            if (entrada.ListaFinanceiroEntrada.Count > 0)
            {
                var totalParcelas = entrada.ListaFinanceiroEntrada.Sum(x => x.ValorDuplicata);

                if (Math.Round(totalParcelas, 2) != Math.Round(entrada.ValorTotalNota, 2))
                {
                    if (MessageBox.Show("O Total da nota está diferente da soma das parcelas, deseja prosseguir?", "Total nota diferente do total das parcelas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool VerifiqueSeUsuarioDesejaProsseguirQdNaoHouverParcelasECompraAPrazo(EntradaMercadoria entrada)
        {
            if (entrada.CondicaoPagamentoEntrada != EnumCondicaoPagamentoNota.AVISTA && entrada.ListaFinanceiroEntrada.Count == 0)
            {
                if (MessageBox.Show("A compra é a prazo e nenhuma parcela foi informada, deseja prosseguir?", "Nenhuma parcela informada", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private bool VerifiqueSeUsuarioDesejaProsseguirQdHouverParcelasECompraAVista(EntradaMercadoria entrada)
        {
            if (entrada.CondicaoPagamentoEntrada == EnumCondicaoPagamentoNota.AVISTA && entrada.ListaFinanceiroEntrada.Count > 0)
            {
                if (MessageBox.Show("A compra é a vista e existem parcelas informadas, deseja prosseguir?", "Parcela informada", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private void Salve()
        {
            Action actionSalvar = () =>
            {
                var entradaMercadoria = RetorneEntradaMercadoriaEmEdicao();

                ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                if (_idEntradaMercadoria == 0)
                {
                    servicoEntradaMercadoria.Cadastre(entradaMercadoria);
                }
                else
                {
                    servicoEntradaMercadoria.Atualize(entradaMercadoria);
                }

                if (MessageBox.Show("Deseja imprimir entrada?", "Impressão entrada", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    _idEntradaMercadoria = entradaMercadoria.Id;

                    ImprimaRelatorio();
                }

                LimpeFormulario();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private EntradaMercadoria RetorneEntradaMercadoriaEmEdicao()
        {
            EntradaMercadoria entradaMercadoria = new EntradaMercadoria();

            entradaMercadoria.Id = _idEntradaMercadoria;

            entradaMercadoria.CondicaoPagamentoEntrada = (EnumCondicaoPagamentoNota)cboCondicaoPagamento.EditValue;

            entradaMercadoria.ListaDeItens = _listaItensEntrada;
            entradaMercadoria.ListaFinanceiroEntrada = _listaFinanceiroEntrada;

            entradaMercadoria.NumeroNota = txtNumeroNfe.Text;
            entradaMercadoria.Serie = txtSerie.Text;
            entradaMercadoria.ChaveDeAcesso = txtChaveAcesso.Text;

            entradaMercadoria.DataMovimentacao = txtDataEntrada.Text.ToDate();
            entradaMercadoria.DataEmissao = txtDataEmissao.Text.ToDate();
            entradaMercadoria.DataCadastro = txtDataCadastro.Text.ToDate();

            entradaMercadoria.Fornecedor = !string.IsNullOrEmpty(txtIdFornecedor.Text) ? new Pessoa { Id = txtIdFornecedor.Text.ToInt() } : null;

            entradaMercadoria.NaturezaOperacao = cboNaturezaOperacao.EditValue != null ? new NaturezaOperacao { Id = cboNaturezaOperacao.EditValue.ToInt() } : null;
            entradaMercadoria.NaturezaOperacaoNota = txtNaturezaOperacao.Text;

            entradaMercadoria.ModeloDocumentoFiscal = (EnumModeloDocumentoFiscal?)cboModelosDocumentoFiscal.EditValue;

            entradaMercadoria.AjusteFiscal = cboAjustesFiscais.EditValue != null ? new AjusteFiscal { Id = cboAjustesFiscais.EditValue.ToInt() } : null;

            entradaMercadoria.Observacoes = txtObservacoesDaNotaFiscal.Text;

            entradaMercadoria.AtualizaPrecoCusto = rdbAtualizaPrecoCusto.Checked;

            entradaMercadoria.TipoFrete = (EnumTipoFrete?)cboTipoFrete.EditValue;

            entradaMercadoria.Transportadora = !string.IsNullOrEmpty(txtIdTransportadora.Text) ? new Pessoa { Id = txtIdTransportadora.Text.ToInt() } : null;

            entradaMercadoria.NumeroConhecimentoFrete = txtNumeroConhecimentoFrete.Text;

            entradaMercadoria.ValorFrete = txtValorFreteTotalNota.Text.ToDouble();

            entradaMercadoria.BaseIcms = txtBaseIcmsTotalNota.Text.ToDouble();
            entradaMercadoria.AliquotaIcms = txtAliquotaIcmsTotalNota.Text.ToDouble();
            entradaMercadoria.ValorIcms = txtValorIcmsTotalNota.Text.ToDouble();


            entradaMercadoria.BaseIcmsSt = txtBaseIcmsStTotalNota.Text.ToDouble();
            entradaMercadoria.ValorIcmsSt = txtValorIcmsStTotalNota.Text.ToDouble();

            entradaMercadoria.ValorDesconto = txtValorDescontoTotalNota.Text.ToDouble();
            entradaMercadoria.ValorOutrasDespesas = txtValorOutrasDespesasTotalNota.Text.ToDouble();
            entradaMercadoria.ValorIpi = txtValorIpiTotalNota.Text.ToDouble();
            entradaMercadoria.ValorTotalNota = txtValorTotalNota.Text.ToDouble();

            return entradaMercadoria;
        }

        private FechamentoGrid RetorneFechamentoCapa()
        {
            FechamentoGrid fechamentoCapa = new FechamentoGrid();

            fechamentoCapa.NomeTotalizacao = "Totais do Cabeçalho";

            fechamentoCapa.BaseIcms = txtBaseIcmsTotalNota.Text.ToDouble().ToString("0.00");
            fechamentoCapa.ValorIcms = txtValorIcmsTotalNota.Text.ToDouble().ToString("0.00");
            
            fechamentoCapa.BaseSt = txtBaseIcmsStTotalNota.Text.ToDouble().ToString("0.00");
            fechamentoCapa.ValorSt = txtValorIcmsStTotalNota.Text.ToDouble().ToString("0.00");

            fechamentoCapa.ValorIpi = txtValorIpiTotalNota.Text.ToDouble().ToString("0.00");

            fechamentoCapa.ValorTotalNota = txtValorTotalNota.Text.ToDouble().ToString("0.00");

            return fechamentoCapa;
        }

        private FechamentoGrid RetorneFechamentoItens()
        {
            FechamentoGrid fechamentoItens = new FechamentoGrid();
            fechamentoItens.NomeTotalizacao = "Totais dos Itens";

            double baseIcms = 0;
            double valorIcms = 0;
            double baseSt = 0;
            double valorSt = 0;
            double valorIpi = 0;
            double valorTotal = 0;
           
            foreach (var item in _listaItensEntrada)
            {
                baseIcms += item.BaseIcms.GetValueOrDefault();
                valorIcms += item.ValorIcms.GetValueOrDefault();

                baseSt += item.BaseIcmsSt.GetValueOrDefault();
                valorSt += item.ValorIcmsSt.GetValueOrDefault();

                valorIpi += item.ValorIpi.GetValueOrDefault();

                valorTotal += item.ValorTotal.GetValueOrDefault();
                valorTotal += item.ValorIcmsSt.GetValueOrDefault();
                valorTotal -= item.ValorDesoneracaoProduto.GetValueOrDefault();
                valorTotal += item.ValorIpi.GetValueOrDefault();
            }

            fechamentoItens.BaseIcms = baseIcms.ToString("#0.00");
            fechamentoItens.ValorIcms = valorIcms.ToString("#0.00");

            fechamentoItens.BaseSt = baseSt.ToString("#0.00");
            fechamentoItens.ValorSt = valorSt.ToString("#0.00");

            fechamentoItens.ValorIpi = valorIpi.ToString("#0.00");

            fechamentoItens.ValorTotalNota = valorTotal.ToString("#0.00");

            return fechamentoItens;
        }

        #endregion

        #region " ATUALIZAÇÃO DO GRID FECHAMENTO E LIBERAÇÃO DO FORMULÁRIO "

        private void AtualizeGridFechamento()
        {
            FechamentoGrid fechamentoItens = RetorneFechamentoItens();
            FechamentoGrid fechamentoCapa = RetorneFechamentoCapa();

            List<FechamentoGrid> listaFechamentoGrid = new List<FechamentoGrid>();

            listaFechamentoGrid.Add(fechamentoCapa);
            listaFechamentoGrid.Add(fechamentoItens);

            gcFechamento.DataSource = listaFechamentoGrid;
            gcFechamento.RefreshDataSource();
        }

        private void LibereFormulario()
        {
            if (!string.IsNullOrEmpty(txtIdFornecedor.Text) && !string.IsNullOrEmpty(txtNumeroNfe.Text) && !string.IsNullOrEmpty(txtSerie.Text))
            {
                tbcAbas.Visible = true;
                imgLinhaDeCima.Visible = true;

                txtIdFornecedor.Enabled = false;
                btnPesquisaFornecedor.Enabled = false;
                txtNumeroNfe.Enabled = false;
                txtSerie.Enabled = false;

                btnSalvar.Visible = true;
                btnConcluirEntrada.Visible = true;

                if (_idEntradaMercadoria > 0)
                {
                    btnImprimirEntrada.Visible = true;
                }
            }
        }

        #endregion

        #region " MÉTODOS RELACIONADOS AOS ITENS "

        private void CalculeSubTotalItem()
        {
            double quantidade = 1;
            var descontoEmValor = CalculosPedidoDeVenda.CalculeTotalDesconto(txtValorUnitario.Text.ToDouble(),
                                                                                                              quantidade,
                                                                                                              txtPercentualDesconto.Text != "0,00" &
                                                                                                              txtPercentualDesconto.Text != string.Empty ? txtValorDesconto.Text.ToDouble(): txtPercentualDesconto.Text.ToDouble(),
                                                                                                              txtPercentualDesconto.Text!="0,00" & 
                                                                                                              txtPercentualDesconto.Text != string.Empty ? true:false);

            txtValorTotal.Text = (CalculosNotaFiscal.CalculeValorTotalItem(txtValorUnitario.Text.ToDouble(), txtQuantidade.Text.ToDouble(),
                                                                                descontoEmValor, txtFrete.Text.ToDouble(),
                                                                                0, txtOutrasDespesas.Text.ToDouble())).ToString("0.00");
            
        }

        private void PreenchaCamposDoProduto(Produto produto)
        {
            _produtoEmEdicao = produto;

            if (produto != null)
            {
                txtIdProduto.Text = produto.Id.ToString();
                txtCodigoDeBarrasProduto.Text = produto.DadosGerais.CodigoDeBarras;
                txtItemDescricaoProduto.Text = produto.DadosGerais.Descricao;
                txtUnidadeEstocar.Text = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                AltereMascaraQuantidadeProduto();

                cboUnidade.Focus();
            }
            else
            {
                txtIdProduto.Text = string.Empty;
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtItemDescricaoProduto.Text = string.Empty;
                txtUnidadeEstocar.Text = string.Empty;
            }
        }

        private void HabiliteCamposIcms(bool habilitarPercentualDeReducao)
        {
            txtBaseIcms.Enabled = true;
            txtPercentualIcms.Enabled = true;
            txtValorIcms.Enabled = true;

            if (habilitarPercentualDeReducao)
            {
                txtPercentualReducaoIcms.Enabled = true;
            }
        }

        private void HabiliteCamposSubstituicaoTributaria()
        {
            txtBaseIcmsSt.Enabled = true;
            txtPercentualIVA.Enabled = true;
            txtAliquotaSt.Enabled = true;
            txtValorIcmsSt.Enabled = true;
        }

        private void HabiliteCamposIcmsDesoneracao()
        {
            txtIcmsDesoneracaoProduto.Enabled = true;
            cboMotivoDesoneracao.Enabled = true;
        }

        private void DesabiliteCamposIcmsESubstituicaoTributaria()
        {
            txtBaseIcms.Text = string.Empty; ;
            txtBaseIcms.Enabled = false;

            txtPercentualIcms.Text = string.Empty;
            txtPercentualIcms.Enabled = false;

            txtPercentualReducaoIcms.Text = string.Empty;
            txtPercentualReducaoIcms.Enabled = false;

            txtValorIcms.Text = string.Empty;
            txtValorIcms.Enabled = false;

            txtBaseIcmsSt.Text = string.Empty;
            txtBaseIcmsSt.Enabled = false;

            txtPercentualIVA.Text = string.Empty;
            txtPercentualIVA.Enabled = false;

            txtAliquotaSt.Text = string.Empty;
            txtAliquotaSt.Enabled = false;

            txtValorIcmsSt.Text = string.Empty;
            txtValorIcmsSt.Enabled = false;

            txtIcmsDesoneracaoProduto.Text = string.Empty;
            txtIcmsDesoneracaoProduto.Enabled = false;

            cboMotivoDesoneracao.EditValue = null;
            cboMotivoDesoneracao.Enabled = false;
        }

        private void InsiraOuAtualizeItemNaLista()
        {
            if (_produtoEmEdicao != null)
            {
                ServicoProduto servicoProduto = new ServicoProduto();
                var produto = servicoProduto.Consulte(_produtoEmEdicao.Id);

                if (produto.ContabilFiscal != null &&
                    ((produto.ContabilFiscal.Ncm == null && _ncmProdutoEmEdicao != null) ||
                    (produto.ContabilFiscal.Ncm != null && _ncmProdutoEmEdicao == null) ||
                    produto.ContabilFiscal.Ncm.Id != _ncmProdutoEmEdicao.Id))
                {
                    if (MessageBox.Show("Ncm Informado na nota está diferente do cadastro do item. Deseja prosseguir?", "Ncm inconsistente", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

            Action actionInserirItem = () =>
            {
                ItemEntrada itemEntrada = new ItemEntrada();

                itemEntrada.Produto = _produtoEmEdicao;
                itemEntrada.Ncm = _ncmProdutoEmEdicao;
                itemEntrada.Unidade = cboUnidade.EditValue != null ? new UnidadeMedida { Id = cboUnidade.EditValue.ToInt(), Abreviacao = cboUnidade.Text } : null;
                itemEntrada.Origem = (EnumOrigem?)cboOrigem.EditValue;
                itemEntrada.CstCsosn = (EnumCstCsosn?)cboCstCsosn.EditValue;
                itemEntrada.Cfop = cboCfop.EditValue != null ? new Cfop { Id = cboCfop.EditValue.ToInt(), Descricao = cboCfop.Text.Remove(0, 7), Codigo = cboCfop.Text.Remove(4) } : null;
                itemEntrada.ValorUnitario = txtValorUnitario.Text.ToDoubleNullabel();
                itemEntrada.QuantidadeBruta = txtQuantidade.Text.ToDouble();
                itemEntrada.Quantidade = txtQuantidadeEstocar.Text.ToDouble();
                itemEntrada.PercentualDesconto = txtPercentualDesconto.Text.ToDoubleNullabel();
                itemEntrada.ValorDesconto = txtValorDesconto.Text.ToDoubleNullabel();
                itemEntrada.OutrasDespesas = txtOutrasDespesas.Text.ToDoubleNullabel();
                itemEntrada.ValorFrete = txtFrete.Text.ToDoubleNullabel();
                itemEntrada.ValorTotal = txtValorTotal.Text.ToDoubleNullabel();
                itemEntrada.PercentualIpi = txtPercentualIpi.Text.ToDoubleNullabel();
                itemEntrada.ValorIpi = txtValorIpi.Text.ToDoubleNullabel();
                itemEntrada.BaseIcms = txtBaseIcms.Text.ToDoubleNullabel();
                itemEntrada.PercentualReducao = txtPercentualReducaoIcms.Text.ToDoubleNullabel();
                itemEntrada.PercentualIcms = txtPercentualIcms.Text.ToDoubleNullabel();
                itemEntrada.MotivoDesoneracaoProduto = (EnumMotivoDesoneracaoProduto?)cboMotivoDesoneracao.EditValue;
                itemEntrada.ValorDesoneracaoProduto = txtIcmsDesoneracaoProduto.Text.ToDoubleNullabel();
                itemEntrada.ValorIcms = txtValorIcms.Text.ToDoubleNullabel();
                itemEntrada.BaseIcmsSt = txtBaseIcmsSt.Text.ToDoubleNullabel();
                itemEntrada.PercentualIva = txtPercentualIVA.Text.ToDoubleNullabel();
                itemEntrada.AliquotaST = txtAliquotaSt.Text.ToDoubleNullabel();
                itemEntrada.ValorIcmsSt = txtValorIcmsSt.Text.ToDoubleNullabel();

                ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                servicoEntradaMercadoria.ValideItem(itemEntrada);

                if (_itemEntradaEmEdicao != null)
                {
                    int indexItem = _listaItensEntrada.IndexOf(_itemEntradaEmEdicao);

                    _listaItensEntrada.Remove(_itemEntradaEmEdicao);

                    _listaItensEntrada.Insert(indexItem, itemEntrada);
                }
                else
                {
                    _listaItensEntrada.Add(itemEntrada);
                }

                LimpeItem();
                GeraIdParaCadaItem();
                AtualizeGridItens();

                btnEditarProduto.Visible = true;
            };

            string mensagemDeSucesso = "Item inserido com sucesso.";
            string tituloMensagemDeSucesso = "Item inserido.";
            string tituloMensagemDeErro = "Inconsistências ao inserir item.";

            if (_itemEntradaEmEdicao != null)
            {
                mensagemDeSucesso = "Item atualizado com sucesso.";
                tituloMensagemDeSucesso = "Item atualizado.";
                tituloMensagemDeErro = "Inconsistências ao atualizar item.";
            }

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirItem, mensagemDeSucesso, tituloMensagemDeSucesso, tituloMensagemDeErro);
        }

        private void AtualizeGridItens()
        {
            List<ItemGrid> listaItensGrid = new List<ItemGrid>();

            foreach (var item in _listaItensEntrada)
            {
                ItemGrid itemGrid = new ItemGrid();

                itemGrid.Id = item.Id;
                itemGrid.IdProduto = item.Produto.Id;
                itemGrid.Descricao = item.Produto.DadosGerais.Descricao;
                itemGrid.Cfop = item.Cfop != null ? item.Cfop.Codigo + " - " + item.Cfop.Descricao : string.Empty;
                itemGrid.CstCsosn = item.CstCsosn.Value.Descricao();
                itemGrid.Desconto = item.ValorDesconto.GetValueOrDefault().ToString("#0.00");
                itemGrid.Quantidade = item.QuantidadeBruta.ToString();
                itemGrid.QuantidadeEstocar = item.Quantidade.ToString();
                itemGrid.Unidade = item.Unidade != null ? item.Unidade.Abreviacao : string.Empty;
                itemGrid.UnidadeEstocar = item.Produto.DadosGerais.Unidade.Abreviacao;
                itemGrid.ValorTotal = item.ValorTotal.GetValueOrDefault().ToString("#0.00");
                itemGrid.ValorUnitario = item.ValorUnitario.GetValueOrDefault().ToString("#0.00######");

                listaItensGrid.Add(itemGrid);
            }

            gcItens.DataSource = listaItensGrid;
            gcItens.RefreshDataSource();

            AtualizeGridFechamento();
        }

        private void EditeItem(ItemEntrada itemEntrada)
        {
            _itemEntradaEmEdicao = itemEntrada;

            if (itemEntrada != null)
            {
                PreenchaCamposDoProduto(itemEntrada.Produto);

                cboUnidade.EditValue = itemEntrada.Unidade.Id;
                cboOrigem.EditValue = itemEntrada.Origem;
                cboCstCsosn.EditValue = itemEntrada.CstCsosn;
                cboCfop.EditValue = itemEntrada.Cfop != null ? (int?)itemEntrada.Cfop.Id : null;

                if (itemEntrada.Ncm != null)
                {
                    ServicoNcm servicoNcm = new ServicoNcm();
                    var ncm = servicoNcm.Consulte(itemEntrada.Ncm.Id);
                    PreenchaNcm(ncm);
                }
                else
                {
                    PreenchaNcm(null);
                }

                txtValorUnitario.Text = itemEntrada.ValorUnitario != null ? itemEntrada.ValorUnitario.GetValueOrDefault().ToString("#0.00######") : string.Empty;
                txtQuantidade.Text = itemEntrada.QuantidadeBruta != null ? itemEntrada.QuantidadeBruta.ToString() : string.Empty;
                txtQuantidadeEstocar.Text = itemEntrada.Quantidade != null ? itemEntrada.Quantidade.ToString() : string.Empty;
                txtPercentualDesconto.Text = itemEntrada.PercentualDesconto != null ? itemEntrada.PercentualDesconto.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorDesconto.Text = itemEntrada.ValorDesconto != null ? itemEntrada.ValorDesconto.GetValueOrDefault().ToString("#0.00") : string.Empty;

                txtOutrasDespesas.Text = itemEntrada.OutrasDespesas != null ? itemEntrada.OutrasDespesas.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtFrete.Text = itemEntrada.ValorFrete != null ? itemEntrada.ValorFrete.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorTotal.Text = itemEntrada.ValorTotal != null ? itemEntrada.ValorTotal.GetValueOrDefault().ToString("#0.00") : string.Empty;

                txtPercentualIpi.Text = itemEntrada.PercentualIpi != null ? itemEntrada.PercentualIpi.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorIpi.Text = itemEntrada.ValorIpi != null ? itemEntrada.ValorIpi.GetValueOrDefault().ToString("#0.00") : string.Empty;

                txtBaseIcms.Text = itemEntrada.BaseIcms != null ? itemEntrada.BaseIcms.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtPercentualReducaoIcms.Text = itemEntrada.PercentualReducao != null ? itemEntrada.PercentualReducao.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtPercentualIcms.Text = itemEntrada.PercentualIcms != null ? itemEntrada.PercentualIcms.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorIcms.Text = itemEntrada.ValorIcms != null ? itemEntrada.ValorIcms.GetValueOrDefault().ToString("#0.00") : string.Empty;

                cboMotivoDesoneracao.EditValue = itemEntrada.MotivoDesoneracaoProduto;
                txtIcmsDesoneracaoProduto.Text = itemEntrada.ValorDesoneracaoProduto != null ? itemEntrada.ValorDesoneracaoProduto.Value.ToString("#0.00") : string.Empty;

                txtBaseIcmsSt.Text = itemEntrada.BaseIcmsSt != null ? itemEntrada.BaseIcmsSt.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtPercentualIVA.Text = itemEntrada.PercentualIva != null ? itemEntrada.PercentualIva.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtAliquotaSt.Text = itemEntrada.AliquotaST != null ? itemEntrada.AliquotaST.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorIcmsSt.Text = itemEntrada.ValorIcmsSt != null ? itemEntrada.ValorIcmsSt.GetValueOrDefault().ToString("#0.00") : string.Empty;

                //btnInserirAtualizarItem.Image = Properties.Resources.icones2_07;
                hint.SetToolTip(btnInserirAtualizarItem, "Atualizar");
                btnExcluirItem.Visible = true;
                btnInserirAtualizarItem.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                PreenchaCamposDoProduto(null);

                cboUnidade.EditValue = null;
                cboOrigem.EditValue = null;
                cboCstCsosn.EditValue = null;
                cboCfop.EditValue = null;

                PreenchaNcm(null);

                txtValorUnitario.Text = string.Empty;
                txtQuantidade.Text = string.Empty;
                txtQuantidadeEstocar.Text = string.Empty;
                txtPercentualDesconto.Text = string.Empty;
                txtValorDesconto.Text = string.Empty;

                txtOutrasDespesas.Text = string.Empty;
                txtFrete.Text = string.Empty;
                txtValorTotal.Text = string.Empty;

                txtPercentualIpi.Text = string.Empty;
                txtValorIpi.Text = string.Empty;

                txtBaseIcms.Text = string.Empty;
                txtPercentualReducaoIcms.Text = string.Empty;
                txtPercentualIcms.Text = string.Empty;
                txtValorIcms.Text = string.Empty;

                txtIcmsDesoneracaoProduto.Text = string.Empty;

                txtBaseIcmsSt.Text = string.Empty;
                txtPercentualIVA.Text = string.Empty;
                txtAliquotaSt.Text = string.Empty;
                txtValorIcmsSt.Text = string.Empty;

                txtIdProduto.Focus();

                hint.SetToolTip(btnInserirAtualizarItem, "Inserir");
                btnInserirAtualizarItem.Image = Properties.Resources.icones2_19;
                btnExcluirItem.Visible = false;
            }
        }

        private void LimpeItem()
        {
            EditeItem(null);
        }

        private void GeraIdParaCadaItem()
        {
            int id = 0;

            foreach (var item in _listaItensEntrada)
            {
                item.Id = id;

                id++;
            }
        }

        private void AltereMascaraQuantidadeProduto()
        {
            if (_produtoEmEdicao.DadosGerais.PermiteVendaFracionada)
            {
                txtQuantidade.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
                txtQuantidadeEstocar.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtQuantidade.Properties.Mask.EditMask = @"[0-9]{1,11}";
                txtQuantidadeEstocar.Properties.Mask.EditMask = @"[0-9]{1,11}";
            }
        }

        private void RateieFrete()
        {
            ServicoEntradaMercadoria servicoEntrada = new ServicoEntradaMercadoria();
            var totalFreteRateioComPrecisao = servicoEntrada.RateieFreteEntrada(txtValorFreteTotalNota.Text.ToDouble(), _listaItensEntrada);

            AtualizeGridItens();
            txtValorTotalNota.Text = (_ValorTotalDaNotaOriginal + totalFreteRateioComPrecisao).ToString("0.00");
        }

        #endregion

        #region " MÉTODOS RELACIONADOS AS PARCELAS FINANCEIRO "

        private void InsiraOuAtualizeParcelaFinanceiro()
        {
            Action actionInserirItem = () =>
            {
                FinanceiroEntrada financeiroEntrada = new FinanceiroEntrada();

                financeiroEntrada.DataVencimento = txtDataVencimentoParcela.Text.ToDate();
                financeiroEntrada.NumeroDocumento = txtNumeroDuplicata.Text;
                financeiroEntrada.ValorDuplicata = txtValorDuplicata.Text.ToDouble();
                financeiroEntrada.FormaPagamento = cboFormaPagamento.EditValue != null ? 
                                                                    new FormaPagamento {
                                                                                         Id = cboFormaPagamento.EditValue.ToInt(),
                                                                                         Descricao = cboFormaPagamento.Text,
                                                                                         TipoFormaPagamento = (EnumTipoFormaPagamento)cboFormaPagamento.EditValue.ToInt()
                                                                    } : null;

                ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                servicoEntradaMercadoria.ValideFinanceiro(financeiroEntrada);

                if (_financeiroEntradaEmEdicao != null)
                {
                    int indexItem = _listaFinanceiroEntrada.IndexOf(_financeiroEntradaEmEdicao);

                    _listaFinanceiroEntrada.Remove(_financeiroEntradaEmEdicao);

                    _listaFinanceiroEntrada.Insert(indexItem, financeiroEntrada);

                    if(chkTodasParcelas.Checked)
                    {
                        foreach (var item in _listaFinanceiroEntrada)
                        {
                            item.FormaPagamento = financeiroEntrada.FormaPagamento;
                        }
                    }
                }
                else
                {
                    _listaFinanceiroEntrada.Add(financeiroEntrada);
                }

                LimpeCamposFinanceiro();
                GereIdParaCadaParcelaFinanceiro();
                PreenchaGridFinanceiro();
            };

            string mensagemDeSucesso = "Parcela inserida com sucesso.";
            string tituloMensagemDeSucesso = "Parcela inserida.";
            string tituloMensagemDeErro = "Inconsistências ao inserir parcela.";

            if (_financeiroEntradaEmEdicao != null)
            {
                mensagemDeSucesso = "Parcela atualizada com sucesso.";
                tituloMensagemDeSucesso = "Parcela atualizada.";
                tituloMensagemDeErro = "Inconsistências ao atualizar parcela.";
            }

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirItem, mensagemDeSucesso, tituloMensagemDeSucesso, tituloMensagemDeErro);
        }

        private void LimpeCamposFinanceiro()
        {
            EditeParcelaFinanceiro(null);
        }

        private void EditeParcelaFinanceiro(FinanceiroEntrada financeiroEntrada)
        {
            _financeiroEntradaEmEdicao = financeiroEntrada;

            if (financeiroEntrada != null)
            {
                txtParcela.Text = financeiroEntrada.Parcela;
                cboFormaPagamento.EditValue = financeiroEntrada.FormaPagamento != null ? (int?)financeiroEntrada.FormaPagamento.Id : null;
                txtNumeroDuplicata.Text = financeiroEntrada.NumeroDocumento;
                txtDataVencimentoParcela.DateTime = financeiroEntrada.DataVencimento;
                txtValorDuplicata.Text = financeiroEntrada.ValorDuplicata.ToString("0.00");

                btnAdicionarFinanceiro.Image = Properties.Resources.icon_atualizar;
                chkTodasParcelas.Visible = true;
                chkTodasParcelas.Checked = false;

                cboFormaPagamento.Focus();
            }
            else
            {
                txtParcela.Text = string.Empty;
                cboFormaPagamento.EditValue = null;
                txtNumeroDuplicata.Text = string.Empty;
                txtDataVencimentoParcela.Text = string.Empty;
                txtValorDuplicata.Text = string.Empty;

                cboFormaPagamento.Focus();
                btnAdicionarFinanceiro.Image = Properties.Resources.icones2_19;

                chkTodasParcelas.Visible = false;
            }
        }

        private void GereIdParaCadaParcelaFinanceiro()
        {
            _listaFinanceiroEntrada = _listaFinanceiroEntrada.OrderBy(finan => finan.DataVencimento).ToList();

            int quantidadeParcelas = _listaFinanceiroEntrada.Count;

            for (int i = 0; i < quantidadeParcelas; i++)
            {
                var financeiro = _listaFinanceiroEntrada[i];

                financeiro.Id = i + 1;
                financeiro.Parcela = (i + 1) + "/" + quantidadeParcelas;
            }
        }

        private void PreenchaGridFinanceiro()
        {
            List<FinanceiroGrid> listaFinanceiroGrid = new List<FinanceiroGrid>();

            foreach (var financeiro in _listaFinanceiroEntrada)
            {
                FinanceiroGrid financeiroGrid = new FinanceiroGrid();

                financeiroGrid.Id = financeiro.Id;
                financeiroGrid.DataVencimento = financeiro.DataVencimento.ToString("dd/MM/yyyy");
                financeiroGrid.FormaPagamento = financeiro.FormaPagamento != null ? financeiro.FormaPagamento.TipoFormaPagamento.Descricao() : string.Empty;
                financeiroGrid.NumeroDuplicata = financeiro.NumeroDocumento;
                financeiroGrid.Parcela = financeiro.Parcela;
                financeiroGrid.Valor = financeiro.ValorDuplicata.ToString("0.00");

                listaFinanceiroGrid.Add(financeiroGrid);
            }

            gcParcelasFinanceiro.DataSource = listaFinanceiroGrid;
            gcParcelasFinanceiro.RefreshDataSource();
        }

        private void PreenchaCboFormaPagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            var lista = servicoFormaPagamento.ConsulteListaAtivos();

            lista.Insert(0, null);

            cboFormaPagamento.Properties.DisplayMember = "Descricao";
            cboFormaPagamento.Properties.ValueMember = "Id";
            cboFormaPagamento.Properties.DataSource = lista;

            if (string.IsNullOrEmpty(cboFormaPagamento.Text))
            {
                cboFormaPagamento.EditValue = null;
            }
        }

        #endregion

        #region " EDIÇÃO E LIMPEZA DO FORMULÁRIO "

        private void LimpeFormulario()
        {
            EditeEntrada(null);
        }

        private void EditeEntrada(EntradaMercadoria entradaMercadoria)
        {
            if (entradaMercadoria != null)
            {
                _idEntradaMercadoria = entradaMercadoria.Id;

                entradaMercadoria.ListaFinanceiroEntrada.CarregueLazyLoad();
                entradaMercadoria.ListaDeItens.CarregueLazyLoad();

                txtNumeroNfe.Text = entradaMercadoria.NumeroNota;
                txtSerie.Text = entradaMercadoria.Serie;
                txtChaveAcesso.Text = entradaMercadoria.ChaveDeAcesso;
                txtDataEmissao.DateTime = entradaMercadoria.DataEmissao.GetValueOrDefault();
                txtDataEntrada.DateTime = entradaMercadoria.DataMovimentacao.GetValueOrDefault();
                txtDataCadastro.Text = entradaMercadoria.DataCadastro.ToString("dd/MM/yyyy");

                txtStatusEntrada.Text = entradaMercadoria.StatusEntrada.Descricao();

                if (entradaMercadoria.Fornecedor != null)
                {
                    txtIdFornecedor.Text = entradaMercadoria.Fornecedor.Id.ToString();
                    txtCnpj.Text = entradaMercadoria.Fornecedor.DadosGerais.CpfCnpj;
                    txtNomeFornecedor.Text = entradaMercadoria.Fornecedor.DadosGerais.Razao;
                }
                else
                {
                    txtIdFornecedor.Text = string.Empty;
                    txtCnpj.Text = string.Empty;
                    txtNomeFornecedor.Text = string.Empty;
                }

                cboNaturezaOperacao.EditValue = entradaMercadoria.NaturezaOperacao != null ? (int?)entradaMercadoria.NaturezaOperacao.Id : null;
                txtNaturezaOperacao.Text = entradaMercadoria.NaturezaOperacaoNota;

                if (entradaMercadoria.NaturezaOperacao == null)
                {
                    cboNaturezaOperacao.Visible = false;
                    txtNaturezaOperacao.Visible = true;
                }
                else
                {
                    cboNaturezaOperacao.Visible = true;
                    txtNaturezaOperacao.Visible = false;
                }

                cboCondicaoPagamento.EditValue = entradaMercadoria.CondicaoPagamentoEntrada;

                cboModelosDocumentoFiscal.EditValue = entradaMercadoria.ModeloDocumentoFiscal;
                cboAjustesFiscais.EditValue = null;
                cboAjustesFiscais.EditValue = entradaMercadoria.AjusteFiscal != null ? (int?)entradaMercadoria.AjusteFiscal.Id : null;

                txtObservacoesDaNotaFiscal.Text = entradaMercadoria.Observacoes;

                rdbAtualizaPrecoCusto.Checked = !entradaMercadoria.AtualizaPrecoCusto;
                rdbNaoAtualizaPrecoCusto.Checked = entradaMercadoria.AtualizaPrecoCusto;

                cboTipoFrete.EditValue = null;
                cboTipoFrete.EditValue = entradaMercadoria.TipoFrete;

                txtIdTransportadora.Text = entradaMercadoria.Transportadora != null ? entradaMercadoria.Transportadora.Id.ToString() : string.Empty;
                txtNomeTransportadora.Text = entradaMercadoria.Transportadora != null ? entradaMercadoria.Transportadora.DadosGerais.Razao : string.Empty;

                txtNumeroConhecimentoFrete.Text = entradaMercadoria.NumeroConhecimentoFrete;

                txtValorFreteTotalNota.Text = entradaMercadoria.ValorFrete.ToString("#0.00");
                txtBaseIcmsTotalNota.Text = entradaMercadoria.BaseIcms.ToString("#0.00");
                txtAliquotaIcmsTotalNota.Text = entradaMercadoria.AliquotaIcms.ToString("#0.00");
                txtValorIcmsTotalNota.Text = entradaMercadoria.ValorIcms.ToString("#0.00");
                txtValorIcmsDesoneracao.Text = _empresa.DadosEmpresa.CodigoRegimeTributario == EnumCodigoRegimeTributario.REGIMENORMAL? 
                                                                                              entradaMercadoria.ValorIcmsDesoneracao.ToString("#0.00"):string.Format("0.00");
                txtBaseIcmsStTotalNota.Text = entradaMercadoria.BaseIcmsSt.ToString("#0.00");
                txtValorIcmsStTotalNota.Text = entradaMercadoria.ValorIcmsSt.ToString("#0.00");
                txtValorDescontoTotalNota.Text = entradaMercadoria.ValorDesconto.ToString("#0.00");
                txtValorIpiTotalNota.Text = entradaMercadoria.ValorIpi.ToString("0.00");
                txtValorOutrasDespesasTotalNota.Text = entradaMercadoria.ValorOutrasDespesas.ToString("#0.00");
                txtValorTotalNota.Text = entradaMercadoria.ValorTotalNota.ToString("#0.00");
                _ValorTotalDaNotaOriginal = entradaMercadoria.ValorTotalNota;

                LibereFormulario();

                _listaItensEntrada.Clear();
                _listaItensEntrada.AddRange(entradaMercadoria.ListaDeItens);
                
                LimpeItem();
                GeraIdParaCadaItem();
                AtualizeGridItens();

                _listaFinanceiroEntrada = entradaMercadoria.ListaFinanceiroEntrada.ToList();
                LimpeCamposFinanceiro();
                GereIdParaCadaParcelaFinanceiro();
                PreenchaGridFinanceiro();

                AtualizeGridFechamento();

                cboNaturezaOperacao.Focus();

                if (entradaMercadoria.StatusEntrada == EnumStatusEntrada.ABERTA)
                {
                    btnSalvar.Visible = true;
                    btnConcluirEntrada.Visible = true;

                    if (entradaMercadoria.Id > 0)//No caso da importação pelo xml é o id será 0
                    {
                        btnCancelarEntrada.Visible = true;
                    }
                }
                else if (entradaMercadoria.StatusEntrada == EnumStatusEntrada.CONCLUIDA)
                {
                    btnSalvar.Visible = false;
                    btnConcluirEntrada.Visible = false;
                    btnCancelarEntrada.Visible = true;
                }
                else
                {
                    btnSalvar.Visible = false;
                    btnConcluirEntrada.Visible = false;
                    btnCancelarEntrada.Visible = false;
                }
            }
            else
            {
                btnSalvar.Visible = true;
                btnConcluirEntrada.Visible = true;
                btnCancelarEntrada.Visible = false;

                txtNumeroNfe.Text = string.Empty;
                txtNumeroNfe.Enabled = true;

                txtSerie.Text = string.Empty;
                txtSerie.Enabled = true;

                txtChaveAcesso.Text = string.Empty;

                txtDataEmissao.DateTime = DateTime.Now.Date;
                txtDataEntrada.DateTime = DateTime.Now.Date;

                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                txtStatusEntrada.Text = EnumStatusEntrada.ABERTA.Descricao();

                txtIdFornecedor.Text = string.Empty;
                txtIdFornecedor.Enabled = true;
                btnPesquisaFornecedor.Enabled = true;
                txtCnpj.Text = string.Empty;
                txtNomeFornecedor.Text = string.Empty;

                cboNaturezaOperacao.EditValue = null;
                txtNaturezaOperacao.Text = string.Empty;

                txtNaturezaOperacao.Visible = false;
                cboNaturezaOperacao.Visible = true;

                cboCondicaoPagamento.EditValue = EnumCondicaoPagamentoNota.AVISTA;

                cboModelosDocumentoFiscal.EditValue = null;
                cboAjustesFiscais.EditValue = null;

                txtObservacoesDaNotaFiscal.Text = string.Empty;

                rdbAtualizaPrecoCusto.Checked = true;
                rdbNaoAtualizaPrecoCusto.Checked = false;

                cboTipoFrete.EditValue = null;

                txtIdTransportadora.Text = string.Empty;
                txtNomeTransportadora.Text = string.Empty;

                txtNumeroConhecimentoFrete.Text = string.Empty;

                txtValorFreteTotalNota.Text = string.Empty;
                txtBaseIcmsTotalNota.Text = string.Empty;
                txtAliquotaIcmsTotalNota.Text = string.Empty;
                txtValorIcmsTotalNota.Text = string.Empty;
                txtValorIcmsDesoneracao.Text = string.Empty;
                txtBaseIcmsStTotalNota.Text = string.Empty;
                txtValorIcmsStTotalNota.Text = string.Empty;
                txtValorDescontoTotalNota.Text = string.Empty;
                txtValorIpiTotalNota.Text = string.Empty;
                txtValorOutrasDespesasTotalNota.Text = string.Empty;
                txtValorTotalNota.Text = string.Empty;

                _listaItensEntrada.Clear();

                LimpeItem();
                GeraIdParaCadaItem();
                AtualizeGridItens();

                _listaFinanceiroEntrada.Clear();
                LimpeCamposFinanceiro();
                GereIdParaCadaParcelaFinanceiro();
                PreenchaGridFinanceiro();

                AtualizeGridFechamento();

                tbcAbas.SelectedTab = tabPage1;
                tbcAbas.Visible = false;
                imgLinhaDeCima.Visible = false;

                btnSalvar.Visible = false;
                btnConcluirEntrada.Visible = false;
                btnCancelarEntrada.Visible = false;
                btnImprimirEntrada.Visible = false;
                _idEntradaMercadoria = 0;

                txtNumeroNfe.Focus();
            }
        }

        #endregion

        #region " CARREGAR XML "

        private void CarregueXml(NFe.Classes.Informacoes.infNFe notaFiscalEletronica, string chaveDeAcesso)
        {
            EntradaMercadoria entradaMercadoria = new EntradaMercadoria();

            if (!XmlEhParaAEmpresaLogada(notaFiscalEletronica))
            {
                MessageBox.Show("Esta nota fiscal não é para a empresa em questão.", "Empresa incorreta na nota fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!FornecedorEstahCadastrado(notaFiscalEletronica, entradaMercadoria) ||
                !TransportadoraEstahCadastrada(notaFiscalEletronica, entradaMercadoria) ||
                !PreenchaItensXml(notaFiscalEletronica, entradaMercadoria))
            {
                return;
            }

            PreenchaDadosGeraisComXml(notaFiscalEletronica, entradaMercadoria, chaveDeAcesso);

            PreenchaFinanceiroXml(notaFiscalEletronica, entradaMercadoria);

            EditeEntrada(entradaMercadoria);
        }
        
        //private void CarregueXmlCTe(CTe.Classes.CTe notaFiscalEletronica)
        //{
        //    EntradaMercadoria entradaMercadoria = new EntradaMercadoria();

        //    if (!XmlEhParaAEmpresaLogadaCTe(notaFiscalEletronica))
        //    {
        //        MessageBox.Show("Esta nota fiscal não é para a empresa em questão.", "Empresa incorreta na nota fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //        return;
        //    }

        //    if (!FornecedorEstahCadastrado(notaFiscalEletronica, entradaMercadoria) ||
        //        !TransportadoraEstahCadastrada(notaFiscalEletronica, entradaMercadoria) ||
        //        !PreenchaItensXml(notaFiscalEletronica, entradaMercadoria))
        //    {
        //        return;
        //    }

        //    PreenchaDadosGeraisComXml(notaFiscalEletronica, entradaMercadoria);

        //    PreenchaFinanceiroXml(notaFiscalEletronica, entradaMercadoria);

        //    EditeEntrada(entradaMercadoria);
        //}

        private void PreenchaFinanceiroXml(NFe.Classes.Informacoes.infNFe notaFiscalEletronica, EntradaMercadoria entradaMercadoria)
        {
            EnumTipoFormaPagamento formaPag=EnumTipoFormaPagamento.OUTROS;

            if (notaFiscalEletronica != null && notaFiscalEletronica.cobr != null)
            {
                if(notaFiscalEletronica.pag != null && notaFiscalEletronica.pag.Count > 0)
                    if(notaFiscalEletronica.pag.FirstOrDefault().detPag != null && notaFiscalEletronica.pag.FirstOrDefault().detPag.Count > 0)
                    {
                        var pagamento = notaFiscalEletronica.pag.FirstOrDefault().detPag.FirstOrDefault().tPag;
                        formaPag = retornaFormaPagamentoZeus(pagamento);
                    }                        

                foreach (var duplicata in notaFiscalEletronica.cobr.dup)
                {
                    FinanceiroEntrada financeiro = new FinanceiroEntrada();
                    financeiro.DataVencimento = duplicata.dVenc.Value;
                    financeiro.NumeroDocumento = duplicata.nDup;
                    financeiro.ValorDuplicata = duplicata.vDup.ToDouble();

                    financeiro.FormaPagamento = formaPag != EnumTipoFormaPagamento.OUTROS ? new FormaPagamento { Id = formaPag.GetHashCode(),TipoFormaPagamento=formaPag }:null;

                    entradaMercadoria.ListaFinanceiroEntrada.Add(financeiro);
                }
            }
        }

        private EnumTipoFormaPagamento retornaFormaPagamentoZeus(NFe.Classes.Informacoes.Pagamento.FormaPagamento FormaPagamentoNF)
        {
            if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpDinheiro == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.DINHEIRO;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpCheque == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.CHEQUE;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpCartaoCredito == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.CARTAOCREDITO;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpCartaoDebito == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.CARTAODEBITO;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpCreditoLoja == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.CREDIARIOPROPRIO;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpValeAlimentacao == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.OUTROS;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpValePresente == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.OUTROS;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpValeRefeicao == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.OUTROS;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpValeCombustivel == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.OUTROS;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpBoletoBancario == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.BOLETOBANCARIO;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpDuplicataMercantil == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.DUPLICATA;
            }
            else if (NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpOutro == FormaPagamentoNF)
            {
                return EnumTipoFormaPagamento.OUTROS;
            }
            else
                return EnumTipoFormaPagamento.OUTROS;
        }

        private bool XmlEhParaAEmpresaLogada(NFe.Classes.Informacoes.infNFe notaFiscalEletronica)
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();

            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            return empresa.DadosEmpresa.Cnpj == notaFiscalEletronica.dest.CNPJ.FormatarParaMascaraCnpj();
        }

        //private bool XmlEhParaAEmpresaLogadaCTe(CTe.Classes.CTe notaFiscalEletronica)
        //{
        //    ServicoEmpresa servicoEmpresa = new ServicoEmpresa();

        //    var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

        //    return empresa.DadosEmpresa.Cnpj == notaFiscalEletronica.infCte.dest.CNPJ.FormatarParaMascaraCnpj();
        //}

        private bool FornecedorEstahCadastrado(NFe.Classes.Informacoes.infNFe notaFiscalEletronica, EntradaMercadoria entradaMercadoria)
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            ServicoCidade servicoCidade = new ServicoCidade();

            var pessoa = servicoPessoa.ConsultePessoaPeloCnpjOuCpf(notaFiscalEletronica.emit.CNPJ.FormatarParaMascaraCnpj());

            if (pessoa == null)
            {
                if (MessageBox.Show("O fornecedor da nota não está cadastrado, deseja cadastra-lo?", "Fornecedor não cadastrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    pessoa = new Pessoa();

                    pessoa.DadosGerais.CpfCnpj = notaFiscalEletronica.emit.CNPJ.FormatarParaMascaraCnpj();
                    pessoa.DadosGerais.DataCadastro = DateTime.Now.Date;
                    pessoa.DadosGerais.EhFornecedor = true;
                    pessoa.DadosGerais.Razao = notaFiscalEletronica.emit.xNome.ToStringEmpty().ToUpper();
                    pessoa.DadosGerais.NomeFantasia = notaFiscalEletronica.emit.xFant.ToStringEmpty().ToUpper();
                    pessoa.DadosGerais.Status = "A";
                    pessoa.DadosGerais.TipoPessoa = EnumTipoPessoa.PESSOAJURIDICA;

                    pessoa.EmpresaPessoa.InscricaoEstadual = notaFiscalEletronica.emit.IEST;
                    pessoa.EmpresaPessoa.InscricaoMunicipal = notaFiscalEletronica.emit.IM;

                    if (!string.IsNullOrEmpty(notaFiscalEletronica.emit.enderEmit.fone.ToString()))
                    {
                        Telefone telefone = new Telefone();
                        telefone.TipoTelefone = EnumTipoTelefone.COMERCIAL;
                        telefone.Ddd = notaFiscalEletronica.emit.enderEmit.fone.ToString().Remove(2).ToInt();
                        telefone.Numero = notaFiscalEletronica.emit.enderEmit.fone.ToString().Remove(0, 2);

                        telefone.Numero = telefone.Numero.Length == 8 ? telefone.Numero.Insert(4, "-") : telefone.Numero.Insert(5, "-");

                        pessoa.ListaDeTelefones.Add(telefone);
                    }

                    EnderecoPessoa enderecoPessoa = new EnderecoPessoa();
                    enderecoPessoa.TipoEndereco = EnumTipoEndereco.PRINCIPAL;
                    enderecoPessoa.Complemento = notaFiscalEletronica.emit.enderEmit.xCpl;
                    enderecoPessoa.Numero = notaFiscalEletronica.emit.enderEmit.nro;

                    enderecoPessoa.Bairro = notaFiscalEletronica.emit.enderEmit.xBairro;
                    enderecoPessoa.CEP = notaFiscalEletronica.emit.enderEmit.CEP.Insert(5, "-");
                    enderecoPessoa.Rua = notaFiscalEletronica.emit.enderEmit.xLgr;
                    enderecoPessoa.Cidade = servicoCidade.ConsultePeloCodigoIbgeAtivo(notaFiscalEletronica.emit.enderEmit.cMun.ToString());

                    pessoa.ListaDeEnderecos.Add(enderecoPessoa);

                    Action atualizarFornecedor = () =>
                    {
                        servicoPessoa.Cadastre(pessoa);
                    };

                    TratamentosDeTela.TrateInclusaoEAtualizacao(atualizarFornecedor, exibirMensagemDeSucesso: false);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string mensagem = string.Empty;

                if (!pessoa.DadosGerais.EhFornecedor)
                {
                    mensagem = "O emitente está cadastrado mas ainda não é fornecedor, deseja torná-lo fornecedor?";
                }

                if (pessoa.DadosGerais.Status != "A")
                {
                    mensagem += string.IsNullOrEmpty(mensagem) ? string.Empty : "\n";
                    mensagem += " O fornecedor está cadastrado mas não está ativo, deseja ativá-lo?";
                }

                if (!string.IsNullOrEmpty(mensagem))
                {
                    if (MessageBox.Show(mensagem, "Alterações no fornecedor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        pessoa.Atendimento.CarregueLazyLoad();
                        pessoa.DadosPessoais.CarregueLazyLoad();
                        pessoa.EmpresaPessoa.CarregueLazyLoad();
                        pessoa.Funcionario.CarregueLazyLoad();
                        pessoa.ListaDeEnderecos.CarregueLazyLoad();
                        pessoa.ListaDeTelefones.CarregueLazyLoad();
                        pessoa.ListaDeComissoes.CarregueLazyLoad();

                        pessoa.DadosPessoais = pessoa.DadosPessoais ?? new DadosPessoais();

                        pessoa.EmpresaPessoa = pessoa.EmpresaPessoa ?? new EmpresaPessoa();
                        
                        pessoa.DadosGerais.Status = "A";
                        pessoa.DadosGerais.EhFornecedor = true;
                        
                        Action atualizarFornecedor = () =>
                        {
                            servicoPessoa.Atualize(pessoa);
                        };

                        TratamentosDeTela.TrateInclusaoEAtualizacao(atualizarFornecedor, exibirMensagemDeSucesso: false);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            entradaMercadoria.Fornecedor = pessoa;

            return true;
        }

        private bool TransportadoraEstahCadastrada(NFe.Classes.Informacoes.infNFe notaFiscalEletronica, EntradaMercadoria entradaMercadoria)
        {
            if (notaFiscalEletronica.transp.transporta == null)
            {
                return true;
            }

            ServicoPessoa servicoPessoa = new ServicoPessoa();

            var pessoa = servicoPessoa.ConsultePessoaPeloCnpjOuCpf(notaFiscalEletronica.transp.transporta.CNPJ.FormatarParaMascaraCnpj());

            if (pessoa == null)
            {
                if (MessageBox.Show("A transportadora da nota não está cadastrada, deseja cadastra-la?", "Transportadora não cadastrada", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    pessoa = new Pessoa();

                    pessoa.DadosGerais.CpfCnpj = notaFiscalEletronica.transp.transporta.CNPJ.FormatarParaMascaraCnpj();
                    pessoa.DadosGerais.DataCadastro = DateTime.Now.Date;
                    pessoa.DadosGerais.EhTransportadora = true;
                    pessoa.DadosGerais.Razao = notaFiscalEletronica.transp.transporta.xNome.ToStringEmpty().ToUpper();
                    pessoa.DadosGerais.NomeFantasia = notaFiscalEletronica.transp.transporta.xNome.ToStringEmpty().ToUpper();
                    pessoa.DadosGerais.Status = "A";
                    pessoa.DadosGerais.TipoPessoa = EnumTipoPessoa.PESSOAJURIDICA;

                    pessoa.EmpresaPessoa.InscricaoEstadual = notaFiscalEletronica.transp.transporta.IE;

                    Action cadastrarTransportadora = () =>
                    {
                        servicoPessoa.Cadastre(pessoa);
                    };

                    TratamentosDeTela.TrateInclusaoEAtualizacao(cadastrarTransportadora, exibirMensagemDeSucesso: false);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string mensagem = string.Empty;

                if (!pessoa.DadosGerais.EhTransportadora)
                {
                    mensagem = "A transportadora está cadastrada no sistema, mas ainda não é uma transportadora, deseja torná-la transportadora?";
                }

                if (pessoa.DadosGerais.Status != "A")
                {
                    mensagem += string.IsNullOrEmpty(mensagem) ? string.Empty : "\n";
                    mensagem += " A transportadora está cadastrada mas não está ativa, deseja ativá-la?";
                }

                if (!string.IsNullOrEmpty(mensagem))
                {
                    if (MessageBox.Show(mensagem, "Alterações na transportadora", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        pessoa.Atendimento.CarregueLazyLoad();
                        pessoa.DadosPessoais.CarregueLazyLoad();
                        pessoa.EmpresaPessoa.CarregueLazyLoad();
                        pessoa.Funcionario.CarregueLazyLoad();
                        pessoa.ListaDeEnderecos.CarregueLazyLoad();
                        pessoa.ListaDeTelefones.CarregueLazyLoad();

                        pessoa.DadosPessoais = pessoa.DadosPessoais ?? new DadosPessoais();

                        pessoa.EmpresaPessoa = pessoa.EmpresaPessoa ?? new EmpresaPessoa();

                        pessoa.DadosGerais.Status = "A";
                        pessoa.DadosGerais.EhTransportadora = true;

                        Action atualizarFornecedor = () =>
                        {
                            servicoPessoa.Atualize(pessoa);
                        };

                        TratamentosDeTela.TrateInclusaoEAtualizacao(atualizarFornecedor, exibirMensagemDeSucesso: false);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            entradaMercadoria.Transportadora = pessoa;

            return true;
        }

        private void PreenchaDadosGeraisComXml(NFe.Classes.Informacoes.infNFe notaFiscalEletronica, EntradaMercadoria entradaMercadoria, string chaveDeAcesso)
        {
            entradaMercadoria.DataCadastro = DateTime.Now;

            entradaMercadoria.CondicaoPagamentoEntrada = notaFiscalEletronica.ide.indPag != null? (EnumCondicaoPagamentoNota)notaFiscalEletronica.ide.indPag:EnumCondicaoPagamentoNota.OUTROS;

            entradaMercadoria.NumeroNota = notaFiscalEletronica.ide.nNF.ToString();
            entradaMercadoria.Serie = notaFiscalEletronica.ide.serie.ToString();
            entradaMercadoria.ChaveDeAcesso = chaveDeAcesso;

            entradaMercadoria.DataEmissao = notaFiscalEletronica.ide.dhEmi.DateTime;
            entradaMercadoria.DataMovimentacao = DateTime.Now.Date;

            entradaMercadoria.NaturezaOperacaoNota = notaFiscalEletronica.ide.natOp;

            entradaMercadoria.ModeloDocumentoFiscal = (EnumModeloDocumentoFiscal)notaFiscalEletronica.ide.mod.ToInt();

            var obs1 = notaFiscalEletronica.infAdic != null? notaFiscalEletronica.infAdic.infAdFisco : null;
            var obs2 = notaFiscalEletronica.infAdic != null ? notaFiscalEletronica.infAdic.infCpl : null;

            entradaMercadoria.Observacoes = obs1 + obs2;

            entradaMercadoria.TipoFrete = notaFiscalEletronica.transp.modFrete == NFe.Classes.Informacoes.Transporte.ModalidadeFrete .mfSemFrete? 
                                          EnumTipoFrete.SEMCOBRANCADEFRETE: 
                                          notaFiscalEletronica.transp.modFrete == NFe.Classes.Informacoes.Transporte.ModalidadeFrete.mfProprioContaDestinatario?
                                           EnumTipoFrete.PORCONTADODESTINATARIOREMETENTE: (EnumTipoFrete)notaFiscalEletronica.transp.modFrete;

            entradaMercadoria.ValorFrete = notaFiscalEletronica.total.ICMSTot.vFrete.ToDouble();
            entradaMercadoria.BaseIcms = notaFiscalEletronica.total.ICMSTot.vBC.ToDouble();

            entradaMercadoria.AliquotaIcms = notaFiscalEletronica.total.ICMSTot.vBC.ToDouble() > 0 ? 
                                             Math.Round(notaFiscalEletronica.total.ICMSTot.vICMS.ToDouble() / 
                                             (double)notaFiscalEletronica.total.ICMSTot.vBC.ToDouble(), 4).ToDouble() * 100 : 0;

            entradaMercadoria.ValorIcms = notaFiscalEletronica.total.ICMSTot.vICMS.ToDouble();
            entradaMercadoria.ValorIcmsDesoneracao = _empresa.DadosEmpresa.CodigoRegimeTributario == EnumCodigoRegimeTributario.REGIMENORMAL? 
                                                                                                    notaFiscalEletronica.total.ICMSTot.vICMSDeson.ToDouble():0;
            entradaMercadoria.BaseIcmsSt = notaFiscalEletronica.total.ICMSTot.vBCST.ToDouble();
            entradaMercadoria.ValorIcmsSt = notaFiscalEletronica.total.ICMSTot.vST.ToDouble();
            entradaMercadoria.ValorDesconto = notaFiscalEletronica.total.ICMSTot.vDesc.ToDouble();
            entradaMercadoria.ValorIpi = notaFiscalEletronica.total.ICMSTot.vIPI.ToDouble();
            entradaMercadoria.ValorOutrasDespesas = notaFiscalEletronica.total.ICMSTot.vOutro.ToDouble();
            entradaMercadoria.ValorTotalNota = notaFiscalEletronica.total.ICMSTot.vNF.ToDouble();
        }

        private bool PreenchaItensXml(NFe.Classes.Informacoes.infNFe notaFiscalEletronica, EntradaMercadoria entradaMercadoria)
        {
            StringBuilder inconsistencias = new StringBuilder();
            StringBuilder ncmsDiferentes = new StringBuilder();

            ServicoProduto servicoProduto = new ServicoProduto();
            ServicoNcm servicoNcm = new ServicoNcm();
            ServicoCfop servicoCfop = new ServicoCfop();

            CadastreProdutosNaoEncontrados(notaFiscalEletronica, entradaMercadoria);

            AtiveProdutosInativos(notaFiscalEletronica, entradaMercadoria);

            foreach (var item in notaFiscalEletronica.det)
            {
                var produto = servicoProduto.ConsulteProdutoPeloCodigoFornecedor(item.prod.cProd, entradaMercadoria.Fornecedor);

                if (produto == null)
                {
                    if ((item.prod.cEAN == "SEM GTIN") || string.IsNullOrEmpty(item.prod.cEAN))
                    {
                        produto = null;
                    }
                    else
                    {
                        produto = servicoProduto.ConsulteProdutoPeloCodigoGtin(item.prod.cEAN);
                    }

                    if (produto == null)
                    {
                        inconsistencias.AppendLine("O produto " + item.prod.xProd + " com código do fornecedor " + item.prod.xProd + " não foi encontrado.\n\n");
                        continue;
                    }
                }

                if (produto.DadosGerais.Status != "A")
                {
                    inconsistencias.AppendLine("O produto " + produto.DadosGerais.Descricao + " com ID " + produto.Id + " não foi encontrado.\n\n");
                    continue;
                }

                UnidadeMedida unidade = _listaUnidadesMedida.FirstOrDefault(und => und.Abreviacao.ToUpper() == item.prod.uCom.ToUpper());

                if (unidade == null)
                {
                    inconsistencias.Append("A unidade " + item.prod.uCom + " não foi encontrada.\n\n");
                    continue;
                }

                if (produto.ContabilFiscal != null &&
                    ((produto.ContabilFiscal.Ncm == null && !string.IsNullOrEmpty(item.prod.NCM)) ||
                    (produto.ContabilFiscal.Ncm != null && string.IsNullOrEmpty(item.prod.NCM)) ||
                    produto.ContabilFiscal.Ncm.CodigoNcm != item.prod.NCM))
                {
                    ncmsDiferentes.AppendLine("Produto " + produto.Id + " - " + produto.DadosGerais.Descricao);
                }

                produto.DadosGerais.Unidade.CarregueLazyLoad();

                ItemEntrada itemEntrada = new ItemEntrada();

                itemEntrada.Produto = produto;

                itemEntrada.Ncm = servicoNcm.ConsultePeloCodigoNcm(item.prod.NCM);

                ICMSGeral icmsGeral = new ICMSGeral(item.imposto.ICMS.TipoICMS);

                itemEntrada.CstCsosn = (EnumCstCsosn)icmsGeral.CSOSN;

                var cfopSaida = servicoCfop.ConsultePeloCodigoCfop(item.prod.CFOP.ToString());
                itemEntrada.Cfop = cfopSaida.CfopDeConversao;

                //string codigoCfopEntrada = item.Produto.CFOP[0] == '5' ? "1" : item.Produto.CFOP[0] == '6' ? "2" : "3";
                //codigoCfopEntrada += item.Produto.CFOP.Remove(0, 1);

                //itemEntrada.Cfop = _listaCfops.FirstOrDefault(cfop => cfop.Codigo == codigoCfopEntrada);

                itemEntrada.Unidade = unidade;

                itemEntrada.BaseIcms = icmsGeral.vBC.ToDouble();

                itemEntrada.PercentualIcms = icmsGeral.pICMS.ToDouble();
                itemEntrada.PercentualReducao = icmsGeral.pRedBC.ToDouble();
                itemEntrada.ValorIcms = icmsGeral.vICMS.ToDouble();

                itemEntrada.MotivoDesoneracaoProduto = _empresa.DadosEmpresa.CodigoRegimeTributario == EnumCodigoRegimeTributario.REGIMENORMAL ?
                                                       (EnumMotivoDesoneracaoProduto?)icmsGeral.motDesICMS:null;
                itemEntrada.ValorDesoneracaoProduto = _empresa.DadosEmpresa.CodigoRegimeTributario == EnumCodigoRegimeTributario.REGIMENORMAL ? icmsGeral.vICMSDeson.ToDouble():0;

                itemEntrada.BaseIcmsSt = icmsGeral.vBCST.ToDouble();
                itemEntrada.AliquotaST = icmsGeral.pICMSST.ToDouble();
                itemEntrada.PercentualIva = icmsGeral.pMVAST.ToDouble();
                itemEntrada.ValorIcmsSt = icmsGeral.vICMSST.ToDouble();

                if(item.imposto != null && item.imposto.IPI != null)
                {
                    var ipi = item.imposto.IPI.TipoIPI as IPITrib;

                    itemEntrada.PercentualIpi = ipi !=null && ipi.pIPI !=null ? ipi.pIPI.ToDouble():0;
                    itemEntrada.ValorIpi = ipi != null && ipi.vIPI != null ? ipi.vIPI.ToDouble() : 0;
                }
                
                itemEntrada.Origem = (EnumOrigem)icmsGeral.orig;

                itemEntrada.PercentualDesconto = (item.prod.vDesc.ToDouble() / (double)item.prod.vProd).ToDouble();
                itemEntrada.ValorDesconto = item.prod.vDesc.ToDouble();
                itemEntrada.OutrasDespesas = item.prod.vOutro.ToDouble();
                itemEntrada.ValorUnitario = item.prod.vUnCom.ToDouble();
                itemEntrada.Quantidade = item.prod.qCom.ToDouble();
                itemEntrada.QuantidadeBruta = item.prod.qCom.ToDouble();
                itemEntrada.ValorFrete = item.prod.vFrete.ToDouble();
                itemEntrada.ValorTotal = item.prod.vProd.ToDouble() - item.prod.vDesc.ToDouble();

                itemEntrada.ValorTotal = CalculosNotaFiscal.CalculeValorTotalItem(item.prod.vUnCom.ToDouble(), item.prod.qCom.ToDouble(), item.prod.vDesc.ToDouble(), item.prod.vFrete.ToDouble(),0, item.prod.vOutro.ToDouble());
                
                entradaMercadoria.ListaDeItens.Add(itemEntrada);
            }

            if (!string.IsNullOrEmpty(inconsistencias.ToString()))
            {
                inconsistencias.Insert(0, "Não será possível importar a nota até que os seguintes itens sejam corrigidos:\n\n");

                MessageBox.Show(inconsistencias.ToString(), "Itens não encontrados", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (string.IsNullOrEmpty(inconsistencias.ToString()) && !string.IsNullOrEmpty(ncmsDiferentes.ToString()))
            {
                ncmsDiferentes.Insert(0, "Os seguintes produtos contém ncms diferentes da nota, deseja continuar?\n\n");

                if (MessageBox.Show(ncmsDiferentes.ToString(), "Ncms diferentes", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        private void CadastreProdutosNaoEncontrados(NFe.Classes.Informacoes.infNFe notaFiscalEletronica, EntradaMercadoria entradaMercadoria)
        {
            ServicoProduto servicoProduto = new ServicoProduto();
            ServicoNcm servicoNcm = new ServicoNcm();
            ServicoUnidadeMedida servicoUnidadeMedida = new ServicoUnidadeMedida();

            List<Produto> listaProdutosNaoCadastrados = new List<Produto>();

            foreach (var item in notaFiscalEletronica.det)
            {
                var unidadeMedida = _listaUnidadesMedida.FirstOrDefault(unidade => unidade.Abreviacao.ToUpper() == item.prod.uCom.ToUpper());

                if (unidadeMedida == null)
                {
                    if (MessageBox.Show("Unidade de medida " + item.prod.uCom + " não foi encontrada, deseja cadastra-la automaticamente?", "Unidade não encontrada", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    {
                        unidadeMedida = new UnidadeMedida();

                        unidadeMedida.Abreviacao = item.prod.uCom.ToStringEmpty().ToUpper();
                        unidadeMedida.Descricao = item.prod.uCom.ToStringEmpty().ToUpper();
                        unidadeMedida.DataCadastro = DateTime.Now;
                        unidadeMedida.Status = "A";

                        servicoUnidadeMedida.Cadastre(unidadeMedida);

                        PreenchaCboUnidades();
                    }
                }

                var produto = servicoProduto.ConsulteProdutoPeloCodigoFornecedor(item.prod.cProd, entradaMercadoria.Fornecedor);

                if (produto == null)
                {
                    if ((item.prod.cEAN == "SEM GTIN") || string.IsNullOrEmpty(item.prod.cEAN))
                    {
                        produto = null;
                    }
                    else
                    {
                        produto = servicoProduto.ConsulteProdutoPeloCodigoGtin(item.prod.cEAN);
                    }
                    
                    produto = produto == null? new Produto(): produto;

                    if (produto.ContabilFiscal.CodigoGtin == null)
                    {
                        produto = new Produto();

                        produto.DadosGerais.CodigoDeBarras = item.prod.cEAN !="SEM GTIN"? item.prod.cEAN.ToStringEmpty().ToUpper():string.Empty;
                        produto.ContabilFiscal.CodigoGtin = item.prod.cEAN != "SEM GTIN" ? item.prod.cEAN.ToStringEmpty().ToUpper() : string.Empty;
                        produto.DadosGerais.DataCadastro = DateTime.Now;
                        produto.DadosGerais.Descricao = item.prod.xProd.ToStringEmpty().ToUpper();
                        produto.DadosGerais.Status = "A";

                        produto.ContabilFiscal.Ncm = servicoNcm.ConsultePeloCodigoNcm(item.prod.NCM);

                        FornecedorProduto fornecedorProduto = new FornecedorProduto();

                        fornecedorProduto.Fornecedor = entradaMercadoria.Fornecedor;
                        fornecedorProduto.CodigoProduto = item.prod.cProd;

                        produto.ListaFornecedores.Add(fornecedorProduto);

                        produto.DadosGerais.Unidade = unidadeMedida;

                        listaProdutosNaoCadastrados.Add(produto);
                    }
                }
            }

            if (listaProdutosNaoCadastrados.Count > 0)
            {
                if (MessageBox.Show("Existem produtos que não foram encontrados, você deseja cadastra-los automaticamente?", "Produtos não encontrados", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    servicoProduto.CadastreLista(listaProdutosNaoCadastrados);
                }
            }
        }

        private void AtiveProdutosInativos(NFe.Classes.Informacoes.infNFe notaFiscalEletronica, EntradaMercadoria entradaMercadoria)
        {
            ServicoProduto servicoProduto = new ServicoProduto();

            List<Produto> listaProdutosNaoCadastrados = new List<Produto>();

            foreach (var item in notaFiscalEletronica.det)
            {
                var produto = servicoProduto.ConsulteProdutoPeloCodigoFornecedor(item.prod.cProd, entradaMercadoria.Fornecedor);

                if (produto != null && produto.DadosGerais.Status != "A")
                {
                    produto.DadosGerais.Status = "A";

                    listaProdutosNaoCadastrados.Add(produto);
                }
            }

            if (listaProdutosNaoCadastrados.Count > 0)
            {
                if (MessageBox.Show("Existem produtos inativos, deseja ativá-los?", "Produtos inativos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (var produto in listaProdutosNaoCadastrados)
                    {
                        servicoProduto.Atualize(produto);
                    }
                }
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

            public string UnidadeEstocar { get; set; }

            public string ValorUnitario { get; set; }

            public string Quantidade { get; set; }

            public string QuantidadeEstocar { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }
        }

        private class CfopAuxiliar
        {
            public int Id { get; set; }

            public string DescricaoFormatada { get; set; }
        }

        private class FechamentoGrid
        {
            public string NomeTotalizacao { get; set; }

            public string BaseIcms { get; set; }

            public string ValorIcms { get; set; }

            public string BaseSt { get; set; }

            public string ValorSt { get; set; }

            public string ValorIpi { get; set; }

            public string Frete { get; set; }

            public string ValorTotalNota { get; set; }
        }

        private class FinanceiroGrid
        {
            public int Id { get; set; }

            public string Parcela { get; set; }

            public string FormaPagamento { get; set; }

            public string NumeroDuplicata { get; set; }

            public string DataVencimento { get; set; }

            public string Valor { get; set; }
        }

        #endregion

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            FormNcmPesquisa formNcmPesquisa = new FormNcmPesquisa();
            var ncm = formNcmPesquisa.ExibaPesquisaDeNcm();

            if (ncm != null)
            {
                PreenchaNcm(ncm);
            }
        }

        private void PreenchaNcm(Ncm ncm, bool manterFocusCasoNaoTenhaNcm = true)
        {
            if (ncm != null)
            {
                txtNcmId.Text = ncm.CodigoNcm.ToString();
                txtNcmDescricao.Text = ncm.Descricao;
            }
            else
            {
                txtNcmId.Text = string.Empty;
                txtNcmDescricao.Text = string.Empty;

                if (manterFocusCasoNaoTenhaNcm)
                {
                    txtNcmId.Focus();
                }
            }
            _ncmProdutoEmEdicao = ncm;
        }

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

        private void btnEditarProduto_Click(object sender, EventArgs e)
        {
            var itemDaLista = _listaItensEntrada.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

            if (itemDaLista == null)
            {
                return;
            }

            FormCadastroDeProduto formCadastroDeProduto = new FormCadastroDeProduto();

            ServicoProduto servicoProduto = new ServicoProduto();
            var produto = servicoProduto.Consulte(itemDaLista.Produto.Id);

            formCadastroDeProduto.EditeProduto(produto);
        }

        private void tbcAbas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcAbas.SelectedIndex == 1 && _listaItensEntrada.Count > 0)
            {
                btnEditarProduto.Visible = true;
            }
            else
            {
                btnEditarProduto.Visible = false;
            }
        }

        private void btnImprimirEntrada_Click(object sender, EventArgs e)
        {
            ImprimaRelatorio();
        }

        private void ImprimaRelatorio()
        {
            RelatorioEntrada relatorioEntrada = new RelatorioEntrada(_idEntradaMercadoria);

            TratamentosDeTela.ExibirRelatorio(relatorioEntrada);
        }

        private void txtFrete_EditValueChanged(object sender, EventArgs e)
        {            
            CalculeSubTotalItem();            
        }

        private void txtPercentualDesconto_EditValueChanged(object sender, EventArgs e)
        {
            CalculeSubTotalItem();
        }

        private void txtValorDesconto_EditValueChanged(object sender, EventArgs e)
        {
            CalculeSubTotalItem();
        }

        private void txtOutrasDespesas_EditValueChanged(object sender, EventArgs e)
        {
            CalculeSubTotalItem();
        }

        private void txtValorFreteTotalNota_KeyDown(object sender, KeyEventArgs e)
        {
            _variavelControleConteudoAlteradoDiretoNoCampotxtValorFreteTotalNota = true;
        }

        private void txtValorFreteTotalNota_EditValueChanged(object sender, EventArgs e)
        {
            if (_variavelControleConteudoAlteradoDiretoNoCampotxtValorFreteTotalNota)
            {
                RateieFrete();
                _variavelControleConteudoAlteradoDiretoNoCampotxtValorFreteTotalNota = false;
            }
        }

        private void txtNumeroNfe_EditValueChanged(object sender, EventArgs e)
        {

        }        
    }
}
