using System;
using System.Collections.Generic;
using System.Linq;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using NFe.Classes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using NFe.Utils.NFe;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using NFe.Classes.Servicos.Tipos;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using NFe.Servicos;
using NFe.Utils;
using Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ;
using System.Transactions;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ.ClassesAuxiliares;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Fiscal.IcmsInterestadualServ;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio;
using NFe.Impressao.NFCe;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using DFe.Classes.Flags;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ
{
    [Funcionalidade(EnumFuncionalidade.NOTAFISCAL)]
    public class ServicoNotaFiscal : ServicoAkilSmallBusiness<NotaFiscal, ValidacaoNotaFiscal, ConversorNotaFiscal>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioNotaFiscal _repositorioNotaFiscal;
        private Empresa _empresa;
        private bool _incrementeNumeroNota;
        
        #endregion

        #region " CONSTRUTOR "

        public ServicoNotaFiscal()
        {
            RetorneRepositorio();
        }

        public ServicoNotaFiscal(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<NotaFiscal> RetorneRepositorio()
        {
            if (_repositorioNotaFiscal == null)
            {
                _repositorioNotaFiscal = FabricaDeRepositorios.Crie<IRepositorioNotaFiscal>();
            }

            return _repositorioNotaFiscal;
        }

        public override int Cadastre(NotaFiscal objetoDeNegocio)
        {
            objetoDeNegocio.InformacoesGeraisNotaFiscal.DataCadastro = DateTime.Now;

            if (objetoDeNegocio.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.OUTRASSAIDAS)
            {
                objetoDeNegocio.InformacoesDocumentoOrigemNotaFiscal.DataElaboracao = DateTime.Now;
            }

            base.Cadastre(objetoDeNegocio);

            if (objetoDeNegocio.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.OUTRASSAIDAS)
            {
                objetoDeNegocio.InformacoesDocumentoOrigemNotaFiscal.DocumentoId = objetoDeNegocio.Id;
                this.Atualize(objetoDeNegocio);
            }
            else if (objetoDeNegocio.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.PEDIDODEVENDAS)
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda(false, false);

                var pedido = servicoPedidoDeVenda.Consulte(objetoDeNegocio.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);

                pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.EMITIDONFE;
                pedido.NotaFiscal = objetoDeNegocio;

                servicoPedidoDeVenda.Atualize(pedido);
            }

            return objetoDeNegocio.Id;
        }

        #endregion

        #region " SALVAR E ENVIAR NOTA "

        public void SalveEEnvieNota(NotaFiscal notaFiscal)
        {
            var configuracoesZeus = RetorneConfiguracoesZeus((EnumModeloNotaFiscal)notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

            if (notaFiscal.IdentificacaoNotaFiscal.TipoEmissaoDanfe != EnumTipoEmissaoDanfe.EMISSAONORMAL)
            {
                configuracoesZeus.tpEmis = (NFe.Classes.Informacoes.Identificacao.Tipos.TipoEmissao)notaFiscal.IdentificacaoNotaFiscal.TipoEmissaoDanfe;
            }

            CarregueEmpresa();

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                PreenchaNumeroNotaSerieChaveAcessoEDemaisCamposRelacionados(notaFiscal);

                var notaFiscalZeus = ConvertaNotaFiscalAkilParaZeus(notaFiscal);

                AssineEGereQrCode(notaFiscalZeus, notaFiscal);

                bool cadastrandoNota = notaFiscal.Id == 0;

                if (cadastrandoNota)
                {
                    Cadastre(notaFiscal);
                }
                else
                {
                    Atualize(notaFiscal);
                }

                try
                {
                    EnvieNota(notaFiscalZeus, configuracoesZeus, notaFiscal);
                }
                catch (Exception ex)
                {
                    if (cadastrandoNota)
                    {
                        notaFiscal.Id = 0;
                    }

                    throw ex;
                }

                notaFiscal.InformacoesGeraisNotaFiscal.ChaveDeAcesso = notaFiscalZeus.infNFe.Id.Replace("NFe", "");

           
                //EstorneProdutosParaEstoque(notaFiscal);

                //ServicoPedidoDeVenda.InativeContasAReceberDoPedido(pedidoDeVenda);

                //ServicoPedidoDeVenda.GereSaidaDeCaixaDoPedido(pedidoDeVenda);

                IncrementeProximoNumeroNotaConfiguracoesNfe(notaFiscal);

                Atualize(notaFiscal);

                scope.Complete();
            }

            LanceExcecaoCasoStatusNotaDiferenteDeProcessando(notaFiscal);
        }

        public void EstorneProdutosParaEstoque(NotaFiscal notafiscal)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = DateTime.Now;
            //movimentacaoBase.FornecedorOuCliente = notafisal.Destinatario.Pessoa.DadosGerais.Razao;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.VENDA;

            movimentacaoBase.Observacoes = "Estornado Nr. Pedido: " + notafiscal.IdentificacaoNotaFiscal;
            movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.ENTRADA;

            foreach (var item in notafiscal.ListaItens)
            {
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                itemMovimentacao.Produto = item.Produto;
                itemMovimentacao.Quantidade = item.Quantidade;
                //itemMovimentacao.PedidoVenda_Id = pedidoVenda.Id;

                movimentacaoBase.ListaDeItens.Add(itemMovimentacao);
            }

            servicoMovimentacao.Cadastre(movimentacaoBase);
        }


        //Método que trata e envia a NFCe - Modelo: 65
        public NotaFiscal EnviaNFCe(NotaFiscal notaFiscal)
        {            
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                //if (notaFiscal.DadosCobranca.ListaDuplicatas != null && notaFiscal.DadosCobranca.ListaDuplicatas.Count == 0)
                notaFiscal.DadosCobranca.ListaDuplicatas = new List<DuplicataNotaFiscal>(); ///***Não pode enviar duplicatas para a sefaz para modelo 65 (Rejeição 760)                          

                //Vai entrar aqui caso houver destinatário <Verificar este código mais tarde - Rildo 12/08/2016-> Imple... NFCe>>
                //if (notaFiscal.Destinatario != null)
                //if (notaFiscal.Destinatario.Pessoa.ListaDeComissoes != null && notaFiscal.Destinatario.Pessoa.ListaDeComissoes.Count == 0)
                    //notaFiscal.Destinatario.Pessoa.ListaDeComissoes = new List<Comissao>();

                notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal = 65;
                notaFiscal.IdentificacaoNotaFiscal.DataHoraSaida = DateTime.Now;
                notaFiscal.InformacoesGeraisNotaFiscal.TipoFrete = EnumTipoFrete.SEMCOBRANCADEFRETE;
                notaFiscal.ListaNotasReferenciadas = notaFiscal.ListaNotasReferenciadas?? new List<NotaFiscalReferenciada>();
                notaFiscal.ListaCartasCorrecoes = notaFiscal.ListaCartasCorrecoes ?? new List<CartaCorrecao>();                

                var pedidoDeVenda = new ServicoPedidoDeVenda().Consulte(notaFiscal.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);

                foreach (var parcela in pedidoDeVenda.ListaParcelasPedidoDeVenda)
                {
                    var formaPagamentoNfce = notaFiscal.ListaFormasPagamentoNFCe.FirstOrDefault(x => x.FormaPagamentoNfce.Descricao() == parcela.FormaPagamento.TipoFormaPagamento.Descricao());

                    if (formaPagamentoNfce == null)
                    {
                        formaPagamentoNfce = new FormaPagamentoNotaFiscal();
                        notaFiscal.ListaFormasPagamentoNFCe.Add(formaPagamentoNfce);
                    }

                    formaPagamentoNfce.ValorPagamento += parcela.Valor;
                    formaPagamentoNfce.FormaPagamentoNfce = parcela.FormaPagamento.FormaPagamentoNfce;


                    if (formaPagamentoNfce.FormaPagamentoNfce == EnumFormaPagamentoNfce.CARTAOCREDITO ||
                        formaPagamentoNfce.FormaPagamentoNfce == EnumFormaPagamentoNfce.CARTAODEBITO)
                    {
                        formaPagamentoNfce.Cartao = new CartaoFormaPagamentoNotaFiscal();
                        formaPagamentoNfce.Cartao.TipoIntegracaoPagamento = EnumTipoIntegracaoPagamento.POS;
                    }
                }

                SalveEEnvieNota(notaFiscal);

                scope.Complete();

                return notaFiscal;                                
            }
        }

        private void LanceExcecaoCasoStatusNotaDiferenteDeProcessando(NotaFiscal notaFiscal)
        {
            if (notaFiscal.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.PROCESSANDO)
            {
                if(notaFiscal.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.AUTORIZADA)
                    throw new Exception("Erro ao enviar nota fiscal: " + notaFiscal.InformacoesGeraisNotaFiscal.MensagemDeErro);
            }            
        }

        private ConfiguracaoServico RetorneConfiguracoesZeus(EnumModeloNotaFiscal modeloNotaFiscal)
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);
            var configuracoesZeus = servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus(modeloNotaFiscal);

            return configuracoesZeus;
        }

        private ConfiguracaoDanfeNfce RetorneConfiguracoesDanfeNfceZeus()
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);
            var configuracaoDanfeNfce = servicoConfiguracaoNfe.RetorneConfiguracaoDanfeNfceZeus();

            return configuracaoDanfeNfce;
        }

        private void CarregueEmpresa()
        {
            if (_empresa == null)
            {
                ServicoEmpresa servicoEmpresa = new ServicoEmpresa(false, false);
                _empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            }
        }

        #endregion

        #region " CANCELAMENTO NOTA"

        public void CanceleNotaFiscal(int idNotaFiscal, string justificativaCancelamento)
        {
            var notaFiscal = Consulte(idNotaFiscal);
            
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                
                string protocolo = CanceleNotaNaSefaz(idNotaFiscal, justificativaCancelamento, notaFiscal);
                List<ItemNotaFiscal> listaItensNF = new List<ItemNotaFiscal>();
                listaItensNF = notaFiscal.ListaItens.ToList();

                if(notaFiscal.DadosCobranca.ListaDuplicatas.Count==0)
                    notaFiscal.DadosCobranca.ListaDuplicatas = new List<DuplicataNotaFiscal>();
                if (notaFiscal.ListaCartasCorrecoes.Count==0)
                    notaFiscal.ListaCartasCorrecoes = new List<CartaCorrecao>();
                if(notaFiscal.ListaNotasReferenciadas.Count==0)
                    notaFiscal.ListaNotasReferenciadas = new List<NotaFiscalReferenciada>();
                if (notaFiscal.ListaItens.Count == 0)
                {
                    notaFiscal.ListaItens = new List<ItemNotaFiscal>();
                    notaFiscal.ListaItens = listaItensNF;                 
                }
                
                CanceleDocumentoVinculado(notaFiscal); //Alterado a ordem para cancelar a nota fiscal (nota não cancelava)

                CanceleNotaNoSistema(notaFiscal, justificativaCancelamento, protocolo);

                scope.Complete();
            }
        }

        #endregion

        #region " CONSULTAS "

        public NotaFiscal Consulte(int serie, int numero, EnumStatusNotaFiscal? status, EnumModeloNotaFiscal modelo = EnumModeloNotaFiscal.NFE)
        {
            return _repositorioNotaFiscal.Consulte(serie, numero, status,modelo);
        }

        public List<VwNotasDocumentos> ConsulteListaVwNotasDocumentos(int? numeroDocumento, DateTime? dataInicial, DateTime? dataFinal, Negocio.Fiscal.Enumeradores.EnumTipoDocumento? tipoDocumento, Negocio.Fiscal.Enumeradores.EnumStatusNotaFiscal? statusNotaFiscal,EnumModeloNotaFiscal? modelo)
        {
            return _repositorioNotaFiscal.ConsulteListaVwNotasDocumentos(numeroDocumento, dataInicial, dataFinal, tipoDocumento, statusNotaFiscal,modelo);
        }

        public List<NotaFiscal> ConsulteListaDocumentos(int? numeroDocumento, DateTime? dataInicial, DateTime? dataFinal, Negocio.Fiscal.Enumeradores.EnumTipoDocumento? tipoDocumento, Negocio.Fiscal.Enumeradores.EnumStatusNotaFiscal? statusNotaFiscal, EnumModeloNotaFiscal? modelo, EnumTipoDeEmissaoPesquisa? tipoEmissao, int? numeroNF = null)
        {
            return _repositorioNotaFiscal.ConsulteListaDocumentos(numeroDocumento, dataInicial, dataFinal, tipoDocumento, statusNotaFiscal, modelo, tipoEmissao, numeroNF);
        }

        public List<NotaFiscal> ConsulteListaComJoinItens(List<int> listaIds)
        {
            return _repositorioNotaFiscal.ConsulteListaComJoinItens(listaIds);
        }

        #endregion

        #region " RETORNE NOTA A PARTIR DE PEDIDO "

        public NotaFiscal RetorneNotaAPartirDePedido(int pedidoId, DestinatarioAuxiliarNotaFiscal destinatarioAuxiliar)
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda(false, false);
            var pedido = servicoPedidoDeVenda.ConsulteJoinComItens(pedidoId);

            CarregueEmpresa();

            //NotaFiscal notaFiscal = pedido.NotaFiscal ?? new NotaFiscal(); ***Se não instanciasse novamente, o objeto notaFiscal, duplicava as informações de cobrança;
            NotaFiscal notaFiscal = new NotaFiscal();

            //Caso a nota for NFCe(Modelo 65) e sem destinatário vamos instanciar novamente o destinatário para que não ocorra erros
            //Pois as informações de destinatário são pegas do pedido de venda da NFCe (modelo 65), uma vez que o objeto "notaFiscal" não contém as mesmas.
            if (notaFiscal != null &&
                                    notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 65 &&
                                    notaFiscal.Destinatario == null)
                notaFiscal.Destinatario = new DestinatarioNotaFiscal();

            PreenchaCamposNotaFiscalEmpresaEmitente(notaFiscal);
            PreenchaCamposNotaFiscalDestinatarioPedidoDeVenda(notaFiscal, pedido, destinatarioAuxiliar);
            PreenchaCamposIdentificacaoNotaPedidoDeVenda(notaFiscal, pedido, destinatarioAuxiliar);

            if (_empresa.DadosEmpresa.CodigoRegimeTributario != EnumCodigoRegimeTributario.REGIMENORMAL)
                PreenchaCamposProdutosPedidoDeVenda(notaFiscal, pedido);
            else
                PreenchaCamposProdutosPedidoDeVendaRegimeNormal(notaFiscal, pedido);

            PreenchaTotaisNotaFiscal(notaFiscal);
            PreenchaIndicadorInscricaoEstadualNotaFiscalPedidoDeVenda(notaFiscal, destinatarioAuxiliar.TipoContribuinteICMS );
            PreenchaParcelasNotaFiscalPedidoDeVenda(notaFiscal, pedido);
            PreenchaInformacoesDocumentoOrigemNotaFiscal(notaFiscal, pedido.DataElaboracao, pedido.Id, EnumTipoDocumento.PEDIDODEVENDAS);
            PreenchaInformacoesGeraisNotaFiscalPedidoDeVenda(notaFiscal, pedido);

            return notaFiscal;
        }
        
        public NotaFiscal RetorneNotaSoPorPedido(int pedidoId, bool podeEstonar=false)
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda(false, false);
            var pedido = servicoPedidoDeVenda.ConsulteJoinComItens(pedidoId);

            CarregueEmpresa();

            NotaFiscal notaFiscal = pedido.NotaFiscal ?? new NotaFiscal();

            PreenchaCamposNotaFiscalEmpresaEmitente(notaFiscal);

            if (_empresa.DadosEmpresa.CodigoRegimeTributario != EnumCodigoRegimeTributario.REGIMENORMAL)
                PreenchaCamposProdutosPedidoDeVenda(notaFiscal, pedido);
            else
                PreenchaCamposProdutosPedidoDeVendaRegimeNormal(notaFiscal, pedido);

            PreenchaTotaisNotaFiscal(notaFiscal, podeEstonar);            
            PreenchaParcelasNotaFiscalPedidoDeVenda(notaFiscal, pedido);
            PreenchaInformacoesDocumentoOrigemNotaFiscal(notaFiscal, pedido.DataElaboracao, pedido.Id, EnumTipoDocumento.PEDIDODEVENDAS);
            PreenchaInformacoesGeraisNotaFiscalPedidoDeVenda(notaFiscal, pedido);

            return notaFiscal;
        }

        private void PreenchaCamposProdutosPedidoDeVendaRegimeNormal(NotaFiscal notaFiscal, PedidoDeVenda pedidoDeVenda)
        {
            CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

            string ufDestino = "IE";

            var tipoInscricaoIcms = new PessoaMetodosAuxiliares().RetorneTipoInscricaoIcms(pedidoDeVenda.Cliente.Id);

            //Vai entrar aqui quando o destinatário for diferente de nulo. Pois a NFCe(Modelo 65) foi enviada sem destinatário
            if (notaFiscal.Destinatario != null)
                if (notaFiscal.Destinatario.UF == "EX")
                {
                    ufDestino = _empresa.DadosEmpresa.Endereco.Cidade.Estado.UF;
                }
                else
                {
                    ufDestino = notaFiscal.Destinatario.UF;
                }

            //Caso a nota for "rejeitada" vamos atualizar com os novos dados do pedido, senão duplica os itens
            if (notaFiscal.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA || notaFiscal.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.PROCESSANDO)
                notaFiscal.ListaItens.Clear();


            var listaIdsItens = pedidoDeVenda.ListaItens.Select<ItemPedidoDeVenda, int>(x => x.Produto.Id).ToList();

            ServicoProduto servicoProduto = new ServicoProduto(false, false);
            var listaProdutos = servicoProduto.ConsulteListaPorId(listaIdsItens);

            foreach (var item in pedidoDeVenda.ListaItens)
            {
                ItemNotaFiscal itemNotaFiscal = new ItemNotaFiscal();

                #region " INFORMAÇÕES DO PRODUTO "

                var produto = listaProdutos.FirstOrDefault(x => x.Id == item.Produto.Id);

                itemNotaFiscal.CodigoBarrasProduto = produto.DadosGerais.CodigoDeBarras;
                //itemNotaFiscal.CodigoGtinProduto = produto.ContabilFiscal != null ? produto.ContabilFiscal.CodigoGtin : "SEM GTIN";
                itemNotaFiscal.CodigoGtinProduto =  "SEM GTIN";
                itemNotaFiscal.NomeProduto = produto.DadosGerais.Descricao;
                itemNotaFiscal.UnidadeProduto = produto.DadosGerais.Unidade.Abreviacao;
                itemNotaFiscal.Ncm = produto.ContabilFiscal != null && produto.ContabilFiscal.Ncm != null ? produto.ContabilFiscal.Ncm.CodigoNcm : null;                

                itemNotaFiscal.Produto = produto;

                #endregion

                #region " CALCULOS IMPOSTOS "

                itemNotaFiscal.Impostos = itemNotaFiscal.Impostos ?? new ImpostosNotaFiscal();
                itemNotaFiscal.Impostos.Icms = itemNotaFiscal.Impostos.Icms ?? new IcmsNotaFiscal();

                itemNotaFiscal.Impostos.Icms.Origem = produto.ContabilFiscal.OrigemProduto;

                double? icmsBaseCalculo = null;
                double? icmsReducaoBaseCalculo = null;
                double? percentualMargemAdicST = null;
                double? aliquotaIcmsST = null;
                double? reducaoBaseST = null;

                EnumModBC? modalidadeBC = null;
                EnumModBCST? modalidadeST = null;

                EnumCstCsosn? cstCsosn = null;
                Cfop cfop = null;
                
                calculosNotaFiscal.DefinaCstCsosnCfopEAliquotasRegimeNormal(item.Produto,
                                                                                           EnumTipoSaidaTributacaoIcms.SAIDAVENDA,
                                                                                           ufDestino,
                                                                                           pedidoDeVenda.TipoCliente,
                                                                                           tipoInscricaoIcms,
                                                                                           ref cstCsosn,
                                                                                           ref cfop,
                                                                                           ref icmsBaseCalculo,
                                                                                           ref icmsReducaoBaseCalculo,
                                                                                           ref percentualMargemAdicST,
                                                                                           ref aliquotaIcmsST,
                                                                                           ref reducaoBaseST,
                                                                                           ref modalidadeBC,
                                                                                           ref modalidadeST);



                itemNotaFiscal.Impostos.Icms.CstCsosn = cstCsosn.Value;

                if (cfop != null)
                {
                    itemNotaFiscal.Cfop = cfop.Codigo.ToInt();
                    
                        double? baseDeCalculoIcms = null;
                        double? valorIcms = null;

                        double? baseDeCalculoIcmsST = null;
                        double? valorIcmsST = null;

                        calculosNotaFiscal.CalculeValorIcmsRegimeNormal(item.Produto,
                                                                     item.ValorUnitario,
                                                                     item.Quantidade,
                                                                     item.TotalDesconto,
                                                                     item.ValorFrete,
                                                                     item.ValorIpi,
                                                                     cstCsosn,
                                                                     percentualMargemAdicST,
                                                                     aliquotaIcmsST,
                                                                     reducaoBaseST,
                                                                     icmsBaseCalculo,
                                                                     icmsReducaoBaseCalculo,
                                                                     ref baseDeCalculoIcmsST,
                                                                     ref valorIcmsST,
                                                                     ref baseDeCalculoIcms,
                                                                     ref valorIcms,
                                                                     item.ValorSeguro,
                                                                     item.ValorOutrasDespesas, 
                                                                     itemNotaFiscal.Cfop);

                       
                        //-A modalidade da BC - ICMS não tem pois é Isenta.
                        //- A modalidade do BC - ICMS ST é 4 - Margem Valor Agregado (%) - MVA
                        itemNotaFiscal.Impostos.Icms.ModBCST = modalidadeST;
                        itemNotaFiscal.Impostos.Icms.ModBC = modalidadeBC;
                        itemNotaFiscal.Impostos.Icms.PercentualMargemValorAdicST = percentualMargemAdicST;
                        itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria = reducaoBaseST;
                        itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria = aliquotaIcmsST;
                        itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria = baseDeCalculoIcmsST;
                        itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria = valorIcmsST;
                                                
                        itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcms = icmsReducaoBaseCalculo;
                        itemNotaFiscal.Impostos.Icms.AliquotaIcms = icmsBaseCalculo;
                        itemNotaFiscal.Impostos.Icms.BaseCalculoIcms = baseDeCalculoIcms;
                        itemNotaFiscal.Impostos.Icms.ValorIcms = valorIcms;

                }

                //IPI

                CalculosPedidoDeVenda calculosPedidoVenda = new CalculosPedidoDeVenda();

                calculosPedidoVenda.DefinaIpi(item, ufDestino, item.PedidoDeVenda.TipoCliente);

                itemNotaFiscal.Impostos.Ipi = itemNotaFiscal.Impostos.Ipi ?? new IpiNotaFiscal();

                itemNotaFiscal.Impostos.Ipi.ValorIpi = item.ValorIpi;
                itemNotaFiscal.Impostos.Ipi.CstIpi = (EnumCstIpi)item.Ipi.CstIpi;
                itemNotaFiscal.Impostos.Ipi.AliquotaIpi = item.Ipi.AliquotaIpi;
                itemNotaFiscal.Impostos.Ipi.BaseDeCalculo = item.Ipi.BaseDeCalculo;

                if (itemNotaFiscal.Cfop == 6107)
                {
                    itemNotaFiscal.Impostos.Icms.BaseCalculoIcms += item.ValorIpi;
                    itemNotaFiscal.Impostos.Icms.ValorIcms = (itemNotaFiscal.Impostos.Icms.BaseCalculoIcms * itemNotaFiscal.Impostos.Icms.AliquotaIcms)/100;
                }

                    //***Fim IPI

                    //PIS

                    calculosPedidoVenda.DefinaPis(item, ufDestino, item.PedidoDeVenda.TipoCliente);

                itemNotaFiscal.Impostos.Pis = itemNotaFiscal.Impostos.Pis ?? new PisNotaFiscal();

                itemNotaFiscal.Impostos.Pis.ValorPis = item.Pis.ValorPis;
                itemNotaFiscal.Impostos.Pis.CstPis = (EnumCstPis)item.Pis.CstPis;
                itemNotaFiscal.Impostos.Pis.AliquotaPercentual = item.Pis.AliquotaPercentual;
                itemNotaFiscal.Impostos.Pis.AliquotaReais = item.Pis.AliquotaReais;
                itemNotaFiscal.Impostos.Pis.BaseDeCalculo = item.Pis.BaseDeCalculo;

                //***Fim Pis

                //Cofins

                calculosPedidoVenda.DefinaCofins(item, ufDestino, item.PedidoDeVenda.TipoCliente);

                itemNotaFiscal.Impostos.Cofins = itemNotaFiscal.Impostos.Cofins ?? new CofinsNotaFiscal();

                itemNotaFiscal.Impostos.Cofins.ValorCofins = item.Cofins.ValorCofins;
                itemNotaFiscal.Impostos.Cofins.CstCofins = (EnumCstCofins)item.Cofins.CstCofins;
                itemNotaFiscal.Impostos.Cofins.AliquotaPercentual = item.Cofins.AliquotaPercentual;
                itemNotaFiscal.Impostos.Cofins.AliquotaReais = item.Cofins.AliquotaReais;
                itemNotaFiscal.Impostos.Cofins.BaseDeCalculo = item.Cofins.BaseDeCalculo;

                //***Fim Cofins


                #endregion

                #region " CEST "

                itemNotaFiscal.Cest = (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL10 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL30 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL60 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL70 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL90 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES201 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES202 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES203 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES500) &&
                                                  (item.Produto.ContabilFiscal != null && produto.ContabilFiscal.Ncm != null) ? produto.ContabilFiscal.Ncm.Cest : null;

                #endregion

                #region " CALCULA VALORES DE OLHO NO IMPOSTO "

                if (item.Produto.ContabilFiscal != null && produto.ContabilFiscal.Ncm != null)
                {
                    itemNotaFiscal.Impostos.TotalTributacaoEstadual = item.ValorTotal * produto.ContabilFiscal.Ncm.ImpostoIbptEstadual / 100;

                    if (itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOINFERIOR40PORCENTO ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOSUPERIOR40PORCENTO ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOSUPERIOR70PORCENTO ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCUJAPRODUCAOEMCONFORMIDADECOMOSPROCESSOSPRODUTIVOSBASICOS ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALEXCETOASINDICADASNOSCODIGOS3E5)
                    {
                        itemNotaFiscal.Impostos.TotalTributacaoFederal = Math.Round(item.ValorTotal * produto.ContabilFiscal.Ncm.ImpostoIbptFederalNacional / 100, 2);
                    }
                    else
                    {
                        itemNotaFiscal.Impostos.TotalTributacaoFederal = Math.Round(item.ValorTotal * produto.ContabilFiscal.Ncm.ImpostoIbptFederalImportados / 100, 2);
                    }
                }

                itemNotaFiscal.Impostos.TotalTributacao = Math.Round(itemNotaFiscal.Impostos.TotalTributacaoEstadual + itemNotaFiscal.Impostos.TotalTributacaoFederal, 2);


                #endregion

                #region " PREENCHE VALORES "

                itemNotaFiscal.ValorDesconto = item.TotalDesconto;
                itemNotaFiscal.ValorFrete = item.ValorFrete;
                itemNotaFiscal.ValorUnitario = item.ValorUnitario;
                itemNotaFiscal.Quantidade = item.Quantidade;
                itemNotaFiscal.ValorTotal = itemNotaFiscal.ValorBruto -
                                                        itemNotaFiscal.ValorDesconto.GetValueOrDefault() +
                                                        item.ValorFrete +
                                                        itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria.GetValueOrDefault();

                #endregion

                notaFiscal.ListaItens.Add(itemNotaFiscal);
            }
        }

        private void PreenchaCamposProdutosPedidoDeVenda(NotaFiscal notaFiscal, PedidoDeVenda pedidoDeVenda)
        {
            CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

            notaFiscal.ListaItens = new List<ItemNotaFiscal>();

            string ufDestino = "IE";

            var tipoInscricaoIcms = new PessoaMetodosAuxiliares().RetorneTipoInscricaoIcms(pedidoDeVenda.Cliente.Id);

            //Vai entrar aqui quando o destinatário for diferente de nulo. Pois a NFCe(Modelo 65) foi enviada sem destinatário
            if (notaFiscal.Destinatario!=null)
            if (notaFiscal.Destinatario.UF == "EX")
            {
                ufDestino = _empresa.DadosEmpresa.Endereco.Cidade.Estado.UF;
            }
            else
            {
                ufDestino = notaFiscal.Destinatario.UF;
            }

            //***Inicializou a lista de itens no início do método***
            ////Caso a nota for "rejeitada" vamos atualizar com os novos dados do pedido, senão duplica os itens
            //if (notaFiscal.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA || notaFiscal.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.PROCESSANDO)
            //{
            //    notaFiscal.ListaItens = new List<ItemNotaFiscal>();
            //    notaFiscal.ListaItens.Clear();
            //}

            var listaIdsItens = pedidoDeVenda.ListaItens.Select<ItemPedidoDeVenda, int>(x => x.Produto.Id).ToList();

            ServicoProduto servicoProduto = new ServicoProduto(false, false);
            var listaProdutos = servicoProduto.ConsulteListaPorId(listaIdsItens);

            foreach (var item in pedidoDeVenda.ListaItens)
            {
                ItemNotaFiscal itemNotaFiscal = new ItemNotaFiscal();

                #region " INFORMAÇÕES DO PRODUTO "

                var produto = listaProdutos.FirstOrDefault(x => x.Id == item.Produto.Id);

                itemNotaFiscal.CodigoBarrasProduto = produto.DadosGerais.CodigoDeBarras;
                itemNotaFiscal.CodigoGtinProduto = produto.ContabilFiscal != null ? produto.ContabilFiscal.CodigoGtin : null;
                itemNotaFiscal.NomeProduto = produto.DadosGerais.Descricao;
                itemNotaFiscal.UnidadeProduto = produto.DadosGerais.Unidade.Abreviacao;
                itemNotaFiscal.Ncm = produto.ContabilFiscal != null && produto.ContabilFiscal.Ncm != null ? produto.ContabilFiscal.Ncm.CodigoNcm : null;

                itemNotaFiscal.Produto = produto;

                #endregion

                #region " CALCULOS IMPOSTOS "

                itemNotaFiscal.Impostos = itemNotaFiscal.Impostos ?? new ImpostosNotaFiscal();
                itemNotaFiscal.Impostos.Icms = itemNotaFiscal.Impostos.Icms ?? new IcmsNotaFiscal();

                itemNotaFiscal.Impostos.Icms.Origem = produto.ContabilFiscal.OrigemProduto;

                double? aliquotaSimplesNacional = null;
                double? aliquotaCreditoST = null;
                double? aliquotaDebitoST = null;
                double? mva = null;
                double? reducaoBaseCalculoST = null;
                double? icmsBaseCalculo = null;
                double? icmsReducaoBaseCalculo = null;

                EnumCstCsosn cstCsosn = EnumCstCsosn.NORMAL00;
                Cfop cfop = null;
                
                calculosNotaFiscal.DefinaCstCsosnCfopEAliquotas(item.Produto,
                                                                                           EnumTipoSaidaTributacaoIcms.SAIDAVENDA,
                                                                                           ufDestino,
                                                                                           pedidoDeVenda.TipoCliente,
                                                                                           tipoInscricaoIcms,
                                                                                           ref cstCsosn,
                                                                                           ref cfop,
                                                                                           ref aliquotaSimplesNacional,
                                                                                           ref aliquotaCreditoST,
                                                                                           ref aliquotaDebitoST,
                                                                                           ref mva,
                                                                                           ref reducaoBaseCalculoST,
                                                                                           ref icmsBaseCalculo,
                                                                                           ref icmsReducaoBaseCalculo);

                itemNotaFiscal.Impostos.Icms.CstCsosn = cstCsosn;

                if (cfop != null)
                {
                    itemNotaFiscal.Cfop = cfop.Codigo.ToInt();

                    if (cstCsosn == EnumCstCsosn.SIMPLES101 || cstCsosn == EnumCstCsosn.SIMPLES201 || cstCsosn == EnumCstCsosn.SIMPLES900)
                    {
                        itemNotaFiscal.Impostos.Icms.AliquotaSimplesNacional = aliquotaSimplesNacional;
                        itemNotaFiscal.Impostos.Icms.ValorIcmsSimplesNacional = (item.Quantidade * item.ValorUnitario - item.TotalDesconto) * (aliquotaSimplesNacional / (double)100);
                    }

                    if (cstCsosn == EnumCstCsosn.SIMPLES201 || cstCsosn == EnumCstCsosn.SIMPLES202 || cstCsosn == EnumCstCsosn.SIMPLES203 || cstCsosn == EnumCstCsosn.SIMPLES900)
                    {
                        double? baseDeCalculoIcmsST = 0;
                        double? valorIcmsST = 0;
                        double? ValorIcms = 0;
                        double? baseDeCalculoIcms = 0;

                        calculosNotaFiscal.CalculeValorIcmsST(item.Produto, 
                                                                                  item.ValorUnitario,
                                                                                  item.Quantidade,
                                                                                  item.TotalDesconto,
                                                                                  item.ValorFrete,
                                                                                  item.ValorIpi,
                                                                                  cstCsosn,
                                                                                  aliquotaCreditoST,
                                                                                  aliquotaDebitoST, 
                                                                                  mva,
                                                                                  reducaoBaseCalculoST, 
                                                                                  ref ValorIcms, 
                                                                                  ref baseDeCalculoIcms,
                                                                                  ref baseDeCalculoIcmsST,
                                                                                  ref valorIcmsST);
                        //Empresa simples nacional para o CST 030 
                        //(Isenta ou não tributada e com cobrança do ICMS por substituição tributária).
                        //-A modalidade da BC - ICMS não tem pois é Isenta.
                        //- A modalidade do BC - ICMS ST é 4 - Margem Valor Agregado (%) - MVA
                        itemNotaFiscal.Impostos.Icms.ModBCST = EnumModBCST.MARGEMVALORAGREGADOPERCENTUAL;
                        itemNotaFiscal.Impostos.Icms.AliquotaIva = mva;
                        itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria = reducaoBaseCalculoST;
                        itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria = aliquotaDebitoST;
                        itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria = baseDeCalculoIcmsST;
                        itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria = valorIcmsST;
                    }
                }

                //IPI

                //CalculosPedidoDeVenda calculosPedidoVenda = new CalculosPedidoDeVenda();

                //calculosPedidoVenda.DefinaIpi(item, ufDestino, item.PedidoDeVenda.TipoCliente);

                //itemNotaFiscal.Impostos.Ipi = itemNotaFiscal.Impostos.Ipi ?? new IpiNotaFiscal();

                //itemNotaFiscal.Impostos.Ipi.ValorIpi = item.ValorIpi;
                //itemNotaFiscal.Impostos.Ipi.CstIpi = (EnumCstIpi)item.Ipi.CstIpi;
                //itemNotaFiscal.Impostos.Ipi.AliquotaIpi = item.Ipi.AliquotaIpi;

                //***Fim IPI

                #endregion

                #region " CEST "

                itemNotaFiscal.Cest = (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL10 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL30 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL60 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL70 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES201 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES202 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES203 ||
                                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES500) &&
                                                  (item.Produto.ContabilFiscal != null && produto.ContabilFiscal.Ncm != null) ? produto.ContabilFiscal.Ncm.Cest : null;

                #endregion

                #region " CALCULA VALORES DE OLHO NO IMPOSTO "

                if (item.Produto.ContabilFiscal != null && produto.ContabilFiscal.Ncm != null)
                {
                    itemNotaFiscal.Impostos.TotalTributacaoEstadual = item.ValorTotal * produto.ContabilFiscal.Ncm.ImpostoIbptEstadual / 100;

                    if (itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOINFERIOR40PORCENTO ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOSUPERIOR40PORCENTO ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOSUPERIOR70PORCENTO ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCUJAPRODUCAOEMCONFORMIDADECOMOSPROCESSOSPRODUTIVOSBASICOS ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALEXCETOASINDICADASNOSCODIGOS3E5)
                    {
                        itemNotaFiscal.Impostos.TotalTributacaoFederal = Math.Round(item.ValorTotal * produto.ContabilFiscal.Ncm.ImpostoIbptFederalNacional / 100, 2);
                    }
                    else
                    {
                        itemNotaFiscal.Impostos.TotalTributacaoFederal = Math.Round(item.ValorTotal * produto.ContabilFiscal.Ncm.ImpostoIbptFederalImportados / 100, 2);
                    }
                }

                itemNotaFiscal.Impostos.TotalTributacao = Math.Round(itemNotaFiscal.Impostos.TotalTributacaoEstadual + itemNotaFiscal.Impostos.TotalTributacaoFederal, 2);


                #endregion

                #region " PREENCHE VALORES "

                itemNotaFiscal.ValorDesconto = item.TotalDesconto;
                itemNotaFiscal.ValorFrete = item.ValorFrete;
                itemNotaFiscal.ValorUnitario = item.ValorUnitario;
                itemNotaFiscal.Quantidade = item.Quantidade;
                itemNotaFiscal.ValorTotal = itemNotaFiscal.ValorBruto -
                                                        itemNotaFiscal.ValorDesconto.GetValueOrDefault() +
                                                        item.ValorFrete +
                                                        itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria.GetValueOrDefault();

                #endregion
                
                notaFiscal.ListaItens.Add(itemNotaFiscal);
            }
        }

        private void PreenchaCamposIdentificacaoNotaPedidoDeVenda(NotaFiscal notaFiscal, PedidoDeVenda pedidoDeVenda, DestinatarioAuxiliarNotaFiscal destinatarioAuxilar)
        {
            notaFiscal.IdentificacaoNotaFiscal.Cidade = _empresa.DadosEmpresa.Endereco.Cidade;
            notaFiscal.IdentificacaoNotaFiscal.CodigoNumericoNota = ((new Random()).Next(0, 99999999)).ToString().PadLeft(8, '0').ToLong();

            notaFiscal.IdentificacaoNotaFiscal.IdentificacaoOperacaoNotaFiscal = notaFiscal.Destinatario.UF == _empresa.DadosEmpresa.Endereco.Cidade.Estado.UF ? EnumIdenficacaoOperacaoNotaFiscal.OPERACAOINTERNA :
                                                                                                                   notaFiscal.Destinatario.UF == "EX" && notaFiscal.IdentificacaoNotaFiscal.ConsumidorFinal ? EnumIdenficacaoOperacaoNotaFiscal.OPERACAOINTERNA :
                                                                                                                   notaFiscal.Destinatario.UF == "EX" ? EnumIdenficacaoOperacaoNotaFiscal.OPERACAOCOMEXTERIOR :
                                                                                                                                                                          EnumIdenficacaoOperacaoNotaFiscal.OPERACAOINTERESTADUAL;

            notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal = 55;

            notaFiscal.IdentificacaoNotaFiscal.ConsumidorFinal = destinatarioAuxilar.TipoCliente == EnumTipoCliente.CONSUMIDOR;
            notaFiscal.IdentificacaoNotaFiscal.DataHoraEmissao = DateTime.Now;
            notaFiscal.IdentificacaoNotaFiscal.DataHoraSaida = notaFiscal.IdentificacaoNotaFiscal.DataHoraEmissao;
            notaFiscal.IdentificacaoNotaFiscal.IndicacaoPresenca = EnumIndicacaoPresenca.OPERACAOPRESENCIAL;
            notaFiscal.IdentificacaoNotaFiscal.NotaSaida = true;
            notaFiscal.IdentificacaoNotaFiscal.FormaPagamento = pedidoDeVenda.ListaParcelasPedidoDeVenda.ToList().Exists(x => !string.IsNullOrEmpty(x.NumeroDocumento)) ? EnumCondicaoPagamentoNota.APRAZO : EnumCondicaoPagamentoNota.AVISTA;
            notaFiscal.IdentificacaoNotaFiscal.FinalidadeEmissaoNFe = EnumFinalidadeEmissaoNfe.NFENORMAL;

            notaFiscal.IdentificacaoNotaFiscal.IrProximoNumero = destinatarioAuxilar.IrProximoNumero;

            ServicoNaturezaOperacao servicoNaturezaOperacao = new ServicoNaturezaOperacao(false, false);

            if (notaFiscal.Destinatario.UF == _empresa.DadosEmpresa.Endereco.Cidade.Estado.UF)
            {

                var naturezaOperacao = servicoNaturezaOperacao.Consulte(5100);

                notaFiscal.IdentificacaoNotaFiscal.DescricaoNaturezaOperacao = naturezaOperacao.Descricao.PadRight(60, ' ').Substring(0, 60).TrimEnd(); ;
                notaFiscal.IdentificacaoNotaFiscal.NaturezaOperacao = naturezaOperacao;
            }
            else
            {
                var naturezaOperacao = servicoNaturezaOperacao.Consulte(6100);

                notaFiscal.IdentificacaoNotaFiscal.DescricaoNaturezaOperacao = naturezaOperacao.Descricao.PadRight(60, ' ').Substring(0, 60).TrimEnd(); ;
                notaFiscal.IdentificacaoNotaFiscal.NaturezaOperacao = naturezaOperacao;
            }

            notaFiscal.IdentificacaoNotaFiscal.VersaoAplicativo = "1.0.0.0";
        }
        
        private void PreenchaCamposNotaFiscalDestinatarioPedidoDeVenda(NotaFiscal notaFiscal,
                                                                                                             PedidoDeVenda pedidoDeVenda,
                                                                                                             DestinatarioAuxiliarNotaFiscal destinatarioAuxiliarNotaFiscal)
        {
            notaFiscal.Destinatario.Pessoa = pedidoDeVenda.Cliente;

            notaFiscal.Destinatario.EhPessoaFisica = destinatarioAuxiliarNotaFiscal.TipoPessoa == EnumTipoPessoa.PESSOAFISICA;
            notaFiscal.Destinatario.Email = destinatarioAuxiliarNotaFiscal.Email == string.Empty ? null : destinatarioAuxiliarNotaFiscal.Email;
            notaFiscal.Destinatario.RazaoSocialOuNomeDestinatario = destinatarioAuxiliarNotaFiscal.Nome;
            notaFiscal.Destinatario.CnpjCpf = destinatarioAuxiliarNotaFiscal.CpfCnpj == string.Empty ? null : destinatarioAuxiliarNotaFiscal.CpfCnpj;
            notaFiscal.Destinatario.InscricaoEstadual = destinatarioAuxiliarNotaFiscal.InscricaoEstadual;
            notaFiscal.Destinatario.TipoPessoa = destinatarioAuxiliarNotaFiscal.TipoPessoa;
            notaFiscal.Destinatario.DataCadastroPessoa = pedidoDeVenda.Cliente.DadosGerais.DataCadastro;
            notaFiscal.Destinatario.StatusPessoa = destinatarioAuxiliarNotaFiscal.StatusPessoa;

            notaFiscal.Destinatario.ParceiroResideExterior = destinatarioAuxiliarNotaFiscal.ResideNoExterior;

            if (!destinatarioAuxiliarNotaFiscal.ResideNoExterior)
            {
                notaFiscal.Destinatario.Bairro = destinatarioAuxiliarNotaFiscal.Bairro == string.Empty ? null : destinatarioAuxiliarNotaFiscal.Bairro;
                notaFiscal.Destinatario.Cep = destinatarioAuxiliarNotaFiscal.Cep == string.Empty ? null : destinatarioAuxiliarNotaFiscal.Cep;
                notaFiscal.Destinatario.Complemento = destinatarioAuxiliarNotaFiscal.Complemento == string.Empty ? null : destinatarioAuxiliarNotaFiscal.Complemento;
                notaFiscal.Destinatario.Logradouro = destinatarioAuxiliarNotaFiscal.Logradouro == string.Empty ? null : destinatarioAuxiliarNotaFiscal.Logradouro;
                notaFiscal.Destinatario.NomeMunicipio = destinatarioAuxiliarNotaFiscal.NomeCidade == string.Empty ? null : destinatarioAuxiliarNotaFiscal.NomeCidade;
                notaFiscal.Destinatario.Numero = destinatarioAuxiliarNotaFiscal.Numero == string.Empty ? null : destinatarioAuxiliarNotaFiscal.Numero;

                notaFiscal.Destinatario.UF = destinatarioAuxiliarNotaFiscal.UF == string.Empty ? null : destinatarioAuxiliarNotaFiscal.UF;
                notaFiscal.Destinatario.CodigoMunicipio = destinatarioAuxiliarNotaFiscal.CodigoCidade;

                notaFiscal.Destinatario.CodigoPais = 1058;
                notaFiscal.Destinatario.NomePais = "BRASIL";
            }
            else
            {
                notaFiscal.Destinatario.CodigoMunicipio = 999999999;
                notaFiscal.Destinatario.UF = "EX";
                notaFiscal.Destinatario.NomeMunicipio = "EXTERIOR";
                notaFiscal.Destinatario.NomePais = destinatarioAuxiliarNotaFiscal.NomePais == string.Empty ? null : destinatarioAuxiliarNotaFiscal.NomePais;

                notaFiscal.Destinatario.CodigoPais = destinatarioAuxiliarNotaFiscal.CodigoPais;

                notaFiscal.Destinatario.IdEstrangeiro = destinatarioAuxiliarNotaFiscal.IdEstrangeiro == string.Empty ? null : destinatarioAuxiliarNotaFiscal.IdEstrangeiro;

                //Informações de Embarque da Nota Fiscal para o Exterior
                notaFiscal.InformacoesComercioExteriorNotaFiscal = new InformacoesComercioExteriorNotaFiscal();
                               
                notaFiscal.InformacoesComercioExteriorNotaFiscal.DescricaoLocalEmbarque = destinatarioAuxiliarNotaFiscal.DescricaoLocalEmbarque;
                notaFiscal.InformacoesComercioExteriorNotaFiscal.UFEmbarque = destinatarioAuxiliarNotaFiscal.UFEmbarque;
                notaFiscal.InformacoesComercioExteriorNotaFiscal.DescricaoLocalDespacho = destinatarioAuxiliarNotaFiscal.DescricaoLocalDespacho;
            }

            if (destinatarioAuxiliarNotaFiscal.DddTelefone != null || !string.IsNullOrEmpty(destinatarioAuxiliarNotaFiscal.NumeroTelefone))
            {
                notaFiscal.Destinatario.Telefone = (destinatarioAuxiliarNotaFiscal.DddTelefone + destinatarioAuxiliarNotaFiscal.NumeroTelefone.RemoverCaracteresDeMascara()).ToLong();
            }
        }

        private void PreenchaIndicadorInscricaoEstadualNotaFiscalPedidoDeVenda(NotaFiscal notaFiscal, EnumTipoInscricaoICMS TipoInscricaoICMS)
        {
            if (TipoInscricaoICMS == EnumTipoInscricaoICMS.CONTRIBUINTEICMS)
            {
                if (notaFiscal.Destinatario.InscricaoEstadual == "")
                {
                    notaFiscal.Destinatario.IndicadorIEDestinatario = EnumIndicadorIEDestinatario.NAOCONTRIBUINTE;
                }
                else
                {
                    notaFiscal.Destinatario.IndicadorIEDestinatario = EnumIndicadorIEDestinatario.CONTRIBUINTEICMS;
                }
                
            }
            else if (notaFiscal.TotaisNotaFiscal.ValorSubstituicaoTributaria > 0)
            {
                notaFiscal.Destinatario.IndicadorIEDestinatario = EnumIndicadorIEDestinatario.ISENTO;
            }
            else
            {
                notaFiscal.Destinatario.IndicadorIEDestinatario = EnumIndicadorIEDestinatario.NAOCONTRIBUINTE;
            }
        }

        private void PreenchaParcelasNotaFiscalPedidoDeVenda(NotaFiscal notaFiscal, PedidoDeVenda pedidoDeVenda)
        {
            pedidoDeVenda = new ServicoPedidoDeVenda().ConsulteJoinComItens(pedidoDeVenda.Id);

            if (notaFiscal.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA)
                notaFiscal.DadosCobranca.ListaDuplicatas = new List<DuplicataNotaFiscal>();

            foreach (var parcela in pedidoDeVenda.ListaParcelasPedidoDeVenda)
            {
                if (!string.IsNullOrWhiteSpace(parcela.NumeroDocumento))
                {
                    DuplicataNotaFiscal duplicata = new DuplicataNotaFiscal();
                    duplicata.DataVencimento = parcela.DataVencimento;
                    duplicata.NumeroDuplicata = parcela.NumeroDocumento;
                    duplicata.ValorDuplicata = parcela.Valor;

                    notaFiscal.DadosCobranca.ListaDuplicatas.Add(duplicata);
                }

                for (int i = 0; i < notaFiscal.DadosCobranca.ListaDuplicatas.Count; i++)
                {
                    notaFiscal.DadosCobranca.ListaDuplicatas[i].Parcela = (i + 1) + "/" + notaFiscal.DadosCobranca.ListaDuplicatas.Count;
                }
            }
        }
        
        private void PreenchaInformacoesGeraisNotaFiscalPedidoDeVenda(NotaFiscal notaFiscal, PedidoDeVenda pedidoDeVenda)
        {
            notaFiscal.InformacoesGeraisNotaFiscal.DataCadastro = DateTime.Now;

            if (pedidoDeVenda == null)
            {
                notaFiscal.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.DISPONIVEL;
            }

            notaFiscal.InformacoesGeraisNotaFiscal.TipoFrete = pedidoDeVenda.TipoFrete;

            notaFiscal.InformacoesGeraisNotaFiscal.TransportadoraId = pedidoDeVenda.Transportadora != null? pedidoDeVenda.Transportadora.Id: notaFiscal.InformacoesGeraisNotaFiscal.TransportadoraId;

            notaFiscal.InformacoesGeraisNotaFiscal.Volume = pedidoDeVenda.Volume;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " PREENCHA CAMPOS NOTA "

        private void PreenchaNumeroNotaSerieChaveAcessoEDemaisCamposRelacionados(NotaFiscal notaFiscal)
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);
            var configuracaoNfe = servicoConfiguracaoNfe.ConsulteConfiguracoesNfe((EnumModeloNotaFiscal)notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

            if (notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 55)
            {
                notaFiscal.IdentificacaoNotaFiscal.FormatoImpressaoDanfe = configuracaoNfe.FormatoImpressaoDanfe;
            }
            else
            {
                notaFiscal.IdentificacaoNotaFiscal.FormatoImpressaoDanfe = EnumFormatoImpressaoDanfe.DANFENFCE;
            }

            _incrementeNumeroNota = false;

            if (notaFiscal.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.REJEITADA & notaFiscal.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.PROCESSANDO)
            {
                notaFiscal.IdentificacaoNotaFiscal.NumeroNota = configuracaoNfe.NumeroNota;
                notaFiscal.IdentificacaoNotaFiscal.Serie = configuracaoNfe.Serie;
            }

            //***Regra para evitar duplicação de notas fiscais: 
            //1 - O numero da nota deve ser maior que 1(um);  
            //2 - Se a nota fiscal anterior for rejeitada, pego o número desta anterior rejeitada, senão, pega o atual e incrementa o novo número;
            //3 - Se a nota fiscal atual for rejeitada, verifica se ela foi autorizada, se foi, pega o próximo numero e incrementa o novo número;
            //4 - Se for processando continua a mesma regra - deixa o mesmo número;

            if (notaFiscal.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.PROCESSANDO)//4
            {
                if (notaFiscal.InformacoesGeraisNotaFiscal.Status != EnumStatusNotaFiscal.REJEITADA)
                {
                    var listaNFAutorizada = ConsulteListaDocumentos(null, null, null, null, EnumStatusNotaFiscal.AUTORIZADA, (EnumModeloNotaFiscal)notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal, null, notaFiscal.IdentificacaoNotaFiscal.NumeroNota - 1);

                    if (listaNFAutorizada.Count == 0)
                    {
                        var listaNFRejeitada = ConsulteListaDocumentos(null, null, null, null, EnumStatusNotaFiscal.REJEITADA, (EnumModeloNotaFiscal)notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal, null, notaFiscal.IdentificacaoNotaFiscal.NumeroNota - 1);

                        if (listaNFRejeitada.Count > 0 && !notaFiscal.IdentificacaoNotaFiscal.IrProximoNumero) //2                    
                            notaFiscal.IdentificacaoNotaFiscal.NumeroNota = notaFiscal.IdentificacaoNotaFiscal.NumeroNota - 1;
                        else
                        {
                            notaFiscal.IdentificacaoNotaFiscal.NumeroNota = configuracaoNfe.NumeroNota;
                            _incrementeNumeroNota = true;
                        }
                    }
                    else
                        _incrementeNumeroNota = true;
                }
                //3
                else
                {
                    var listaNFAutorizada = ConsulteListaDocumentos(null, null, null, null, EnumStatusNotaFiscal.AUTORIZADA, (EnumModeloNotaFiscal)notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal, null ,notaFiscal.IdentificacaoNotaFiscal.NumeroNota);

                    if (listaNFAutorizada.Count > 0 || notaFiscal.IdentificacaoNotaFiscal.IrProximoNumero)
                    {
                        notaFiscal.IdentificacaoNotaFiscal.NumeroNota = configuracaoNfe.NumeroNota;
                        _incrementeNumeroNota = true;
                    }
                }
            }

            notaFiscal.IdentificacaoNotaFiscal.Serie = configuracaoNfe.Serie;
            notaFiscal.IdentificacaoNotaFiscal.TipoAmbiente = configuracaoNfe.TipoAmbiente;

            notaFiscal.IdentificacaoNotaFiscal.DigitoVerificadorChaveAcesso = RetorneDigitoVerificadorChaveAcesso(notaFiscal);
            
            notaFiscal.IdentificacaoNotaFiscal.VersaoAplicativo = "1.0.0.0";
        }

        private void PreenchaCamposNotaFiscalEmpresaEmitente(NotaFiscal notaFiscal)
        {
            EmitenteNotaFiscal emitente = new EmitenteNotaFiscal();

            emitente.CNAE = _empresa.DadosEmpresa.Cnae != null ? _empresa.DadosEmpresa.Cnae.Codigo : string.Empty;
            emitente.CNPJ = _empresa.DadosEmpresa.Cnpj;
            emitente.CRT = _empresa.DadosEmpresa.CodigoRegimeTributario.GetValueOrDefault();

            emitente.Logradouro = _empresa.DadosEmpresa.Endereco.Rua;
            emitente.Numero = _empresa.DadosEmpresa.Endereco.Numero;
            emitente.Complemento = _empresa.DadosEmpresa.Endereco.Complemento;
            emitente.Bairro = _empresa.DadosEmpresa.Endereco.Bairro;
            emitente.CodigoMunicipio = _empresa.DadosEmpresa.Endereco.Cidade.CodigoIbge.ToLong();
            emitente.NomeMunicipio = _empresa.DadosEmpresa.Endereco.Cidade.Descricao;
            emitente.UF = _empresa.DadosEmpresa.Endereco.Cidade.Estado.UF;

            emitente.CodigoDoEstado = _empresa.DadosEmpresa.Endereco.Cidade.Estado.CodigoEstado;

            emitente.Cep = _empresa.DadosEmpresa.Endereco.CEP;
            emitente.CodigoPais = 1058;
            emitente.NomePais = "BRASIL";

            _empresa.DadosEmpresa.Telefone = _empresa.DadosEmpresa.Telefone.Replace('(', ' ').Replace(')', ' ').Replace('-', ' ').Replace(" ", "");
            emitente.Telefone = _empresa.DadosEmpresa.Telefone.ToLongNullabel();

            emitente.InscricaoEstadual = _empresa.DadosEmpresa.InscricaoEstadual;
            emitente.InscricaoMunicipal = _empresa.DadosEmpresa.InscricaoMunicipal;

            emitente.NomeFantasia = _empresa.DadosEmpresa.NomeFantasia;
            emitente.RazaoSocial = _empresa.DadosEmpresa.RazaoSocial;

            notaFiscal.Emitente = emitente;
        }

        private void PreenchaTotaisNotaFiscal(NotaFiscal notaFiscal, bool PodeEstornar=false)
        {
            string NumeroDeItens=null;
            ServicoNcm servicoNcm = new ServicoNcm(false, false);

            notaFiscal.TotaisNotaFiscal.Produtos = 0;
            notaFiscal.TotaisNotaFiscal.ValorNotaFiscal = 0;
            notaFiscal.TotaisNotaFiscal.Desconto = 0;

            notaFiscal.TotaisNotaFiscal.TotalTributacaoEstadual = 0;
            notaFiscal.TotaisNotaFiscal.TotalTributacaoFederal = 0;
            notaFiscal.TotaisNotaFiscal.TotalTributacao = 0;

            notaFiscal.TotaisNotaFiscal.BaseCalculoIcmsST = 0;
            notaFiscal.TotaisNotaFiscal.ValorSubstituicaoTributaria = 0;
            notaFiscal.TotaisNotaFiscal.BaseCalculoIcms = 0;
            notaFiscal.TotaisNotaFiscal.Icms = 0;

            notaFiscal.TotaisNotaFiscal.Frete = 0;

            string chaveTabelaIbpt = string.Empty;

            for (int i = 0; i < notaFiscal.ListaItens.Count; i++)
            {
                
                var item = notaFiscal.ListaItens[i];
                var ncm = servicoNcm.ConsultePeloCodigoNcm(item.Ncm);

                if (item.Ncm==null)
                {
                    NumeroDeItens += item.Produto.Id.ToString() + " ";
                }
                else
                {
                    chaveTabelaIbpt = ncm.ChaveTabelaIbpt;
                }                

                notaFiscal.TotaisNotaFiscal.Produtos += item.ValorBruto;                
                notaFiscal.TotaisNotaFiscal.Desconto += item.ValorDesconto.GetValueOrDefault();
                notaFiscal.TotaisNotaFiscal.Frete += item.ValorFrete.GetValueOrDefault();

                notaFiscal.TotaisNotaFiscal.TotalTributacaoEstadual += item.Impostos.TotalTributacaoEstadual;
                notaFiscal.TotaisNotaFiscal.TotalTributacaoFederal += item.Impostos.TotalTributacaoFederal;
                notaFiscal.TotaisNotaFiscal.TotalTributacao += item.Impostos.TotalTributacao;

                notaFiscal.TotaisNotaFiscal.BaseCalculoIcmsST += item.Impostos.Icms.BaseIcmsSubstituicaoTributaria.GetValueOrDefault();
                notaFiscal.TotaisNotaFiscal.ValorSubstituicaoTributaria += item.Impostos.Icms.ValorSubstituicaoTributaria.GetValueOrDefault();

                notaFiscal.TotaisNotaFiscal.BaseCalculoIcms += item.Impostos.Icms.BaseCalculoIcms.GetValueOrDefault();
                notaFiscal.TotaisNotaFiscal.Icms += item.Impostos.Icms.ValorIcms.GetValueOrDefault();
                
                notaFiscal.TotaisNotaFiscal.Ipi += item.Impostos.Ipi != null ? item.Impostos.Ipi.ValorIpi.GetValueOrDefault():0;
                                
                notaFiscal.TotaisNotaFiscal.Pis += item.Impostos.Pis != null ? item.Impostos.Pis.ValorPis.GetValueOrDefault() : 0;
                //item.Impostos.Cofins.ValorCofins = 16.00;
                notaFiscal.TotaisNotaFiscal.Cofins += item.Impostos.Cofins != null ? item.Impostos.Cofins.ValorCofins.GetValueOrDefault() : 0;
                



                if (notaFiscal.Emitente.CRT == EnumCodigoRegimeTributario.REGIMENORMAL)
                {

                    double valorTotalItem = (item.ValorBruto + item.ValorFrete.ToDouble() + item.Impostos.Ipi.ValorIpi.ToDouble() +
                                               item.Impostos.Icms.ValorSubstituicaoTributaria.ToDouble() +
                                               item.Seguro.ToDouble() + item.OutrasDespesas.ToDouble()) - item.ValorDesconto.ToDouble();




                    notaFiscal.TotaisNotaFiscal.ValorNotaFiscal += Math.Round(valorTotalItem,2);
                }
                else
                {
                    notaFiscal.TotaisNotaFiscal.ValorNotaFiscal += Math.Round(item.ValorTotal,2);
                }
               
            }

            if (!string.IsNullOrEmpty(NumeroDeItens)&& !PodeEstornar)
            {
                throw new Exception("Este pedido possui itens sem NCM. Para emitir a nota fiscal informe o NCM para os itens: " + NumeroDeItens);                    
            }

            if (notaFiscal.IdentificacaoNotaFiscal.ConsumidorFinal && notaFiscal.TotaisNotaFiscal.TotalTributacao > 0)
            {
                notaFiscal.InformacoesGeraisNotaFiscal.Observacoes = string.Concat("Trib aprox R$: ",
                                                                                                                        notaFiscal.TotaisNotaFiscal.TotalTributacaoFederal.ToString("0.00"),
                                                                                                                        " Federal e ",
                                                                                                                        notaFiscal.TotaisNotaFiscal.TotalTributacaoEstadual.ToString("0.00"),
                                                                                                                        " Estadual - Fonte: IBPT" + chaveTabelaIbpt);
            }
        }

        private void PreenchaInformacoesDocumentoOrigemNotaFiscal(NotaFiscal notaFiscal, DateTime dataElaboracao, int documentoId, EnumTipoDocumento tipoDocumento)
        {
            notaFiscal.InformacoesDocumentoOrigemNotaFiscal.DataElaboracao = dataElaboracao;
            notaFiscal.InformacoesDocumentoOrigemNotaFiscal.DocumentoId = documentoId;
            notaFiscal.InformacoesDocumentoOrigemNotaFiscal.Origem = tipoDocumento;
            notaFiscal.InformacoesDocumentoOrigemNotaFiscal.UsuarioId = Sessao.PessoaLogada.Id;
            notaFiscal.InformacoesDocumentoOrigemNotaFiscal.UsurioNome = Sessao.PessoaLogada.DadosGerais.Razao;
        }

        private int RetorneDigitoVerificadorChaveAcesso(NotaFiscal notaFiscal)
        {
            string chaveAcesso = _empresa.DadosEmpresa.Endereco.Cidade.Estado.CodigoEstado +
                                            notaFiscal.IdentificacaoNotaFiscal.DataHoraEmissao.ToString("yy") + notaFiscal.IdentificacaoNotaFiscal.DataHoraEmissao.ToString("MM") +
                                            _empresa.DadosEmpresa.Cnpj.RemoverCaracteresDeMascara() + "55" +
                                            notaFiscal.IdentificacaoNotaFiscal.Serie +
                                            notaFiscal.IdentificacaoNotaFiscal.NumeroNota + "1" +
                                            notaFiscal.IdentificacaoNotaFiscal.CodigoNumericoNota;

            int soma = 0; // Vai guardar a Soma
            int mod = -1; // Vai guardar o Resto da divisão
            int dv = -1;  // Vai guardar o DigitoVerificador
            int pesso = 2; // vai guardar o pesso de multiplicacao

            //percorrendo cada caracter da chave da direita para esquerda para fazer os calculos com o pesso
            for (int i = chaveAcesso.Length - 1; i != -1; i--)
            {
                int ch = Convert.ToInt32(chaveAcesso[i].ToString());
                soma += ch * pesso;
                //sempre que for 9 voltamos o pesso a 2
                if (pesso < 9)
                    pesso += 1;
                else
                    pesso = 2;
            }

            //Agora que tenho a soma vamos pegar o resto da divisão por 11
            mod = soma % 11;
            //Aqui temos uma regrinha, se o resto da divisão for 0 ou 1 então o dv vai ser 0
            if (mod == 0 || mod == 1)
                dv = 0;
            else
                dv = 11 - mod;

            return dv;
        }

        #endregion

        #region " CONVERTENDO NOTA AKIL PARA ZEUS "

        private NFe.Classes.NFe ConvertaNotaFiscalAkilParaZeus(NotaFiscal notaFiscal)
        {
            ConversorNotaFiscalAkilParaZeus conversorNotaFiscalAkilParaZeus = new ConversorNotaFiscalAkilParaZeus();
            var notaFiscalZeus = conversorNotaFiscalAkilParaZeus.ConvertaNotaAkilParaZeus(notaFiscal);

            return notaFiscalZeus;
        }

        #endregion

        #region "CONVERTER FORMAS DE PAGAMENTO PARA NF"

        public EnumFormaPagamentoNfce RetorneFormaPagamentoParaNF(EnumTipoFormaPagamento formaPgtoOrigem)
        {

            switch (formaPgtoOrigem)
            {
                case EnumTipoFormaPagamento.OUTROS:
                    return EnumFormaPagamentoNfce.OUTROS;                    
                case EnumTipoFormaPagamento.DINHEIRO:
                    return EnumFormaPagamentoNfce.DINHEIRO;
                case EnumTipoFormaPagamento.BOLETOBANCARIO:
                    return EnumFormaPagamentoNfce.BOLETOBANCARIO;
                case EnumTipoFormaPagamento.DEPOSITOBANCARIO:
                    return EnumFormaPagamentoNfce.OUTROS;
                case EnumTipoFormaPagamento.CHEQUE:
                    return EnumFormaPagamentoNfce.CHEQUE;
                case EnumTipoFormaPagamento.DUPLICATA:
                    return EnumFormaPagamentoNfce.DUPLICATA;
                case EnumTipoFormaPagamento.CREDIARIOPROPRIO:
                    return EnumFormaPagamentoNfce.CREDITOLOJA;
                case EnumTipoFormaPagamento.CARTAOCREDITO:
                    return EnumFormaPagamentoNfce.CARTAOCREDITO;
                case EnumTipoFormaPagamento.CARTAODEBITO:
                    return EnumFormaPagamentoNfce.CARTAODEBITO;
                case EnumTipoFormaPagamento.CREDITOINTERNO:
                    return EnumFormaPagamentoNfce.CREDITOLOJA;
                case EnumTipoFormaPagamento.PIX:
                    return EnumFormaPagamentoNfce.PIX;

                default:
                    break;
            }

            return EnumFormaPagamentoNfce.SEMPAGAMENTO;
        }

        # endregion

        private void AssineEGereQrCode(NFe.Classes.NFe notaFiscalZeus, NotaFiscal notaFiscal)
        {
            notaFiscalZeus.Assina();

            if (notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 65)
            {
                try
                {
                    notaFiscalZeus.infNFeSupl = new infNFeSupl() { qrCode = NFe.Utils.InformacoesSuplementares.ExtinfNFeSupl.ObterUrlQrCode(notaFiscalZeus.infNFeSupl, notaFiscalZeus, VersaoQrCode.QrCodeVersao2, RetorneConfiguracoesDanfeNfceZeus().cIdToken, RetorneConfiguracoesDanfeNfceZeus().CSC) }; //Define a URL do QR-Code.
                   
                    notaFiscalZeus.infNFeSupl.urlChave = NFe.Utils.InformacoesSuplementares.ExtinfNFeSupl.ObterUrl(notaFiscalZeus.infNFeSupl, notaFiscalZeus.infNFe.ide.tpAmb, DFe.Classes.Entidades.Estado.GO, TipoUrlConsultaPublica.UrlConsulta, VersaoServico.Versao400, VersaoQrCode.QrCodeVersao2);

                    notaFiscal.InformacoesSuplementaresNotaFiscal = new InformacoesSuplementaresNotaFiscal();
                    notaFiscal.InformacoesSuplementaresNotaFiscal.QrCode = notaFiscalZeus.infNFeSupl.qrCode;                    
                }
                catch (Exception)
                {
                    throw new Exception("O código CSC não foi definido para geração de NFCe! Vá até menu ADM SISTEMA, submenu PARAMETROS, procure pela guia FISCAL e informe o CSC. Qualquer dúvida contate o suporte!"); 
                }
                              
            }
        }

        private void EnvieNota(NFe.Classes.NFe notaFiscalZeus, ConfiguracaoServico configuracoesZeus, NotaFiscal notaFiscal)
        {
            var servicoNFe = new ServicosNFe(configuracoesZeus);

            var retornoEnvio = notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 65?

                //Se for NFCe = Sincrono
                servicoNFe.NFeAutorizacao(Convert.ToInt32(notaFiscalZeus.infNFe.ide.nNF), 
                IndicadorSincronizacao.Sincrono,
                new List<NFe.Classes.NFe> { notaFiscalZeus }):

                //Se for NFe = Assincrono
                servicoNFe.NFeAutorizacao((int)notaFiscalZeus.infNFe.ide.nNF, IndicadorSincronizacao.Assincrono,
                new List<NFe.Classes.NFe> { notaFiscalZeus });

            if (retornoEnvio.Retorno.cStat == 103)
            {
                notaFiscal.InformacoesGeraisNotaFiscal.NumeroReciboLote = retornoEnvio.Retorno.infRec.nRec;
                notaFiscal.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.PROCESSANDO;
            }            
            else if(retornoEnvio.Retorno.cStat == 104)
            {
               if(retornoEnvio.Retorno.protNFe.infProt.cStat == 100)
               {
                    notaFiscal.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.AUTORIZADA;

                    var informacoesProtocolo = retornoEnvio.Retorno.protNFe.infProt;

                    notaFiscal.InformacoesGeraisNotaFiscal.MensagemDevolvida = informacoesProtocolo.cStat + " - " + retornoEnvio.Retorno.protNFe.infProt.xMotivo;
                    notaFiscal.InformacoesGeraisNotaFiscal.MensagemDeErro = string.Empty;
                    notaFiscal.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.AUTORIZADA;

                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal ?? new InformacoesProtocoloAutorizacaoNotaFiscal();

                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.Id = informacoesProtocolo.Id;
                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.ChaveNfe = informacoesProtocolo.chNFe;
                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.DataHoraRecibo = informacoesProtocolo.dhRecbto.Date;
                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.DigestValue = informacoesProtocolo.digVal;
                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.Motivo = informacoesProtocolo.xMotivo;
                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.NumeroProtocolo = informacoesProtocolo.nProt;
                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.Status = informacoesProtocolo.cStat;
                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.TipoAmbiente = (int)informacoesProtocolo.tpAmb;
                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoAplicativo = informacoesProtocolo.verAplic;
                    notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoNota = "4.00";

                }
                else
                {
                    notaFiscal.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.REJEITADA;
                    notaFiscal.InformacoesGeraisNotaFiscal.MensagemDeErro = retornoEnvio.Retorno.protNFe.infProt.xMotivo;
                }                
            }
            else
            {
                notaFiscal.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.PROBLEMATRANSMISSAO;
                notaFiscal.InformacoesGeraisNotaFiscal.MensagemDeErro = retornoEnvio.Retorno.xMotivo;
            }
        }

        private void IncrementeProximoNumeroNotaConfiguracoesNfe(NotaFiscal notaFiscal)
        {
            if (_incrementeNumeroNota)
            {
                ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);

                var configuracoes = servicoConfiguracaoNfe.
                    ConsulteConfiguracoesNfe((EnumModeloNotaFiscal)notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

                configuracoes.NumeroNota++;

                servicoConfiguracaoNfe.Atualize(configuracoes);
            }
        }

        #region " CANCELAMENTO NOTA "

        private string CanceleNotaNaSefaz(int idNotaFiscal, string justificativaCancelamento, NotaFiscal notaFiscal)
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);
            var configuracoesZeus = servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus((EnumModeloNotaFiscal)notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

            var servicoNFe = new ServicosNFe(configuracoesZeus);

            var retorno = servicoNFe.RecepcaoEventoCancelamento(notaFiscal.IdentificacaoNotaFiscal.NumeroNota,
                                                                                                1,
                                                                                                notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.NumeroProtocolo,
                                                                                                notaFiscal.InformacoesGeraisNotaFiscal.ChaveDeAcesso,
                                                                                                justificativaCancelamento,
                                                                                                notaFiscal.Emitente.CNPJ.RemoverCaracteresDeMascara());

            if (retorno.ProcEventosNFe[0].retEvento != null)
            if (retorno.ProcEventosNFe[0].retEvento.infEvento.cStat != 135)
            {
                throw new Exception("Ocorreu o seguinte erro ao cancelar nota: " + retorno.ProcEventosNFe[0].retEvento.infEvento.cStat + " - " + retorno.ProcEventosNFe[0].retEvento.infEvento.xMotivo);
            }

            if (retorno.ProcEventosNFe[0].retEvento != null)
                return retorno.ProcEventosNFe[0].retEvento.infEvento.nProt;
            else
                return null;
        }

        private void CanceleNotaNoSistema(NotaFiscal notaFiscal, string justificativa, string protocolo)
        {
            notaFiscal.InformacoesCancelamentoNotaFiscal = notaFiscal.InformacoesCancelamentoNotaFiscal ?? new InformacoesCancelamentoNotaFiscal();

            notaFiscal.InformacoesCancelamentoNotaFiscal.JustificativaCancelamento = justificativa;
            notaFiscal.InformacoesCancelamentoNotaFiscal.ProtocoloCancelamento = protocolo;

            notaFiscal.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.CANCELADA;
            
            this.Atualize(notaFiscal);
        }

        private void CanceleDocumentoVinculado(NotaFiscal notaFiscal)
        {
            if (notaFiscal.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.PEDIDODEVENDAS)
            {
                CancelePedidoDeVenda(notaFiscal);
            }
        }

        private void CancelePedidoDeVenda(NotaFiscal notaFiscal)
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda(false, false);

            servicoPedidoDeVenda.CancelePedidoDeVenda(notaFiscal.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);
        }

        #endregion

        #endregion

        public void CalculePartilhaDeIcms(NotaFiscal notaFiscalEmEdicao)
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();

            if (parametros.ParametrosFiscais == null || !parametros.ParametrosFiscais.CalcularPartilhaIcms)
            {
                return;
            }

            ServicoIcmsInterestadual servicoIcmsInterestadual = new ServicoIcmsInterestadual(false, false);

            List<string> listaCodigosNcm = new List<string>();

            foreach (var item in notaFiscalEmEdicao.ListaItens)
            {
                if (!listaCodigosNcm.Exists(x => x == item.Ncm))
                {
                    listaCodigosNcm.Add(item.Ncm);
                }
            }

            ServicoNcm servicoNcm = new ServicoNcm(false, false);

            List<IcmsInterestadualEstado> listaIcmsInterestadual = servicoIcmsInterestadual.ConsulteListaIcmsEstadoPorCodigosNcmsEUF(listaCodigosNcm, notaFiscalEmEdicao.Destinatario.UF);

            foreach (var item in notaFiscalEmEdicao.ListaItens)
            {
                CalculeIcmsPartilhaItem(notaFiscalEmEdicao, item, listaIcmsInterestadual);

                if (item.Impostos.IcmsInterestadual != null)
                {
                    notaFiscalEmEdicao.TotaisNotaFiscal.ValorFCP = notaFiscalEmEdicao.TotaisNotaFiscal.ValorFCP.GetValueOrDefault() + item.Impostos.IcmsInterestadual.ValorFCP;
                    notaFiscalEmEdicao.TotaisNotaFiscal.ValorInterestadualDestino = notaFiscalEmEdicao.TotaisNotaFiscal.ValorInterestadualDestino.GetValueOrDefault() + item.Impostos.IcmsInterestadual.ValorIcmsDestino;
                    notaFiscalEmEdicao.TotaisNotaFiscal.ValorInterestadualOrigem = notaFiscalEmEdicao.TotaisNotaFiscal.ValorInterestadualOrigem.GetValueOrDefault() + item.Impostos.IcmsInterestadual.ValorIcmsOrigem;             
                }
            }

            notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Observacoes += "// ICMS Interestadual UF Destino: R$ " + notaFiscalEmEdicao.TotaisNotaFiscal.ValorInterestadualDestino.GetValueOrDefault().ToString("#,##0.00");
            notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Observacoes += "// FCP: R$ " + notaFiscalEmEdicao.TotaisNotaFiscal.ValorFCP.GetValueOrDefault().ToString("#,##0.00");
        }

        private void CalculeIcmsPartilhaItem(NotaFiscal notafiscal, ItemNotaFiscal itemNotaFiscal, List<IcmsInterestadualEstado> listaIcmsInterestadual)
        {
            IcmsInterestadualEstado icmsEstado = listaIcmsInterestadual.FirstOrDefault(x => x.IcmsInterestadual.Ncm.CodigoNcm == itemNotaFiscal.Ncm);

            if (icmsEstado == null)
            {
                return;
            }

            IcmsInterestadualNotaFiscal icmsInterestadualNotaFiscal = new IcmsInterestadualNotaFiscal();

            double aliquotaInterestadual = 0;
            double aliquotaInternaDestino = icmsEstado.AliquotaInterna;
            double aliquotaFcp = icmsEstado.FCP;

            if (itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.ESTRANGEIRAIMPORTACAODIRETA ||
                itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.ESTRANGEIRAADQUIRIDANOMERCADOINTERNO ||
                itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOSUPERIOR40PORCENTO ||
                itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOSUPERIOR70PORCENTO)
            {
                aliquotaInterestadual = 4;
            }
            else
            {
                aliquotaInterestadual = 12;

                var estadosSulESudesteExcetoEspiritoSanto = RetorneEstadosSulESudesteExcetoEspiritoSanto();
                var estadosNorteNordesteCentroOesteEEspiritoSanto = RetorneEstadosNorteNordesteCentroOesteEEspiritoSanto();

                if (estadosSulESudesteExcetoEspiritoSanto.Exists(x => x == notafiscal.Emitente.UF))
                {
                    if (estadosNorteNordesteCentroOesteEEspiritoSanto.Exists(x => x == notafiscal.Destinatario.UF))
                    {
                        aliquotaInterestadual = 7;
                    }
                }
            }

            double baseDeCalculo = itemNotaFiscal.ValorBruto - itemNotaFiscal.ValorDesconto.GetValueOrDefault() + itemNotaFiscal.ValorFrete.GetValueOrDefault();

            double diferencialDeAliquota = Math.Abs(aliquotaInternaDestino - aliquotaInterestadual);

            double icmsCheio = baseDeCalculo * (diferencialDeAliquota / (double)100);

            double percentualProvisorioPartilha = DateTime.Now.Date.Year == 2016 ? 40 : DateTime.Now.Date.Year == 2017 ? 60 : DateTime.Now.Date.Year == 2018 ? 80 : 100;

            double icmsDestino = Math.Round(icmsCheio * (percentualProvisorioPartilha / (double)100), 2);
            double valorFcp = Math.Round(baseDeCalculo * (aliquotaFcp / (double)100),2);
            double icmsOrigem = Math.Round(icmsCheio * ((100 - percentualProvisorioPartilha) / (double)100), 2);

            icmsInterestadualNotaFiscal.AliquotaFCP = aliquotaFcp;
            icmsInterestadualNotaFiscal.AliquotaInterestadual = aliquotaInterestadual;
            icmsInterestadualNotaFiscal.AliquotaInterna = aliquotaInternaDestino;
            icmsInterestadualNotaFiscal.BaseDeCalculo = baseDeCalculo;
            icmsInterestadualNotaFiscal.PercentualProvisorioPartilha = percentualProvisorioPartilha;
            icmsInterestadualNotaFiscal.ValorFCP = valorFcp;
            icmsInterestadualNotaFiscal.ValorIcmsDestino = icmsDestino;
            icmsInterestadualNotaFiscal.ValorIcmsOrigem = icmsOrigem;
            
            itemNotaFiscal.Impostos.IcmsInterestadual = icmsInterestadualNotaFiscal;
        }

        public void CalculeFCP(NotaFiscal notaFiscalEmEdicao)
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();

            if (parametros.ParametrosFiscais == null || !parametros.ParametrosFiscais.CalcularFCP)
            {
                return;
            }

            ServicoIcmsInterestadual servicoIcmsInterestadual = new ServicoIcmsInterestadual(false, false);

            List<string> listaCodigosNcm = new List<string>();

            foreach (var item in notaFiscalEmEdicao.ListaItens)
            {
                if (!listaCodigosNcm.Exists(x => x == item.Ncm))
                {
                    listaCodigosNcm.Add(item.Ncm);
                }
            }

            ServicoNcm servicoNcm = new ServicoNcm(false, false);

            List<IcmsInterestadualEstado> listaIcmsInterestadual = servicoIcmsInterestadual.ConsulteListaIcmsEstadoPorCodigosNcmsEUF(listaCodigosNcm, notaFiscalEmEdicao.Destinatario.UF);

            foreach (var item in notaFiscalEmEdicao.ListaItens)
            {
                CalculeFCPItem(notaFiscalEmEdicao, item, listaIcmsInterestadual);

                if (item.Impostos.Fcp != null)
                {
                    notaFiscalEmEdicao.TotaisNotaFiscal.TotalFCPNf = notaFiscalEmEdicao.TotaisNotaFiscal.TotalFCPNf.GetValueOrDefault() + item.Impostos.Fcp.ValorFCP;
                    notaFiscalEmEdicao.TotaisNotaFiscal.ValorFCPSt = notaFiscalEmEdicao.TotaisNotaFiscal.ValorFCPSt.GetValueOrDefault() + item.Impostos.Fcp.ValorFCPST;
                }
            }
        }

        private void CalculeFCPItem(NotaFiscal notafiscal, ItemNotaFiscal itemNotaFiscal, List<IcmsInterestadualEstado> listaIcmsInterestadual)
        {
            IcmsInterestadualEstado icmsEstado = listaIcmsInterestadual.FirstOrDefault(x => x.IcmsInterestadual.Ncm.CodigoNcm == itemNotaFiscal.Ncm);

            if (icmsEstado == null)
            {
                return;
            }            
            
            double aliquotaFcp = icmsEstado.FCP;
            double aliquotaFcpSt = itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria.ToDouble() != 0 ? icmsEstado.FCP : 0;
            
            double baseDeCalculo = itemNotaFiscal.ValorBruto - itemNotaFiscal.ValorDesconto.GetValueOrDefault() + itemNotaFiscal.ValorFrete.GetValueOrDefault();
            double baseDeCalculoSt = itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria.ToDouble();
            
            double valorFcp = Math.Round(baseDeCalculo * (aliquotaFcp / (double)100),2);
            double valorFcpSt = Math.Round(baseDeCalculoSt * (aliquotaFcpSt / (double)100), 2);

            itemNotaFiscal.Impostos.Fcp = new FCP();

            itemNotaFiscal.Impostos.Fcp.ValorBaseFCP = baseDeCalculo;
            itemNotaFiscal.Impostos.Fcp.PercentualFCP = aliquotaFcp;
            itemNotaFiscal.Impostos.Fcp.ValorFCP = valorFcp;

            itemNotaFiscal.Impostos.Fcp.PercentualBCSTFCP = aliquotaFcpSt;
            itemNotaFiscal.Impostos.Fcp.ValorFCPST = valorFcpSt;
        }

        private List<String> RetorneEstadosSulESudesteExcetoEspiritoSanto()
        {
            List<string> estadosSulESudesteExcetoEspiritoSanto = new List<string>();

            //Estados Sul
            estadosSulESudesteExcetoEspiritoSanto.Add("PR");
            estadosSulESudesteExcetoEspiritoSanto.Add("RS");
            estadosSulESudesteExcetoEspiritoSanto.Add("SC");

            //Estados Sudeste exceto Espírito Santo
            estadosSulESudesteExcetoEspiritoSanto.Add("SP");
            estadosSulESudesteExcetoEspiritoSanto.Add("RJ");
            estadosSulESudesteExcetoEspiritoSanto.Add("MG");

            return estadosSulESudesteExcetoEspiritoSanto;
        }

        private List<String> RetorneEstadosNorteNordesteCentroOesteEEspiritoSanto()
        {
            List<string> estadosNorteNordesteCentroOesteEEspiritoSanto = new List<string>();

            //Estados dos Norte
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("AM");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("RR");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("AP");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("PA");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("TO");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("RO");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("AC");

            //Estados Nordeste
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("MA");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("PI");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("CE");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("RN");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("PE");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("PB");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("SE");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("AL");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("BA");

            //Estados Centro Oeste
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("MT");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("MS");
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("GO");

            //Espírito Santo
            estadosNorteNordesteCentroOesteEEspiritoSanto.Add("ES");

            return estadosNorteNordesteCentroOesteEEspiritoSanto;
        }

        #region "Outras Saídas"

        public void MovimenteEstoqueNotaFiscalOutrasSaidas(NotaFiscal notaFiscal, EnumTipoMovimentacao tipoMovimentacao)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = DateTime.Now;
            movimentacaoBase.FornecedorOuCliente = notaFiscal.Destinatario.Pessoa;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.ENTRADADEMERCADORIA;

            movimentacaoBase.Observacoes = "NR NF Outras Saídas/Entrada: " + notaFiscal.Id;
            movimentacaoBase.TipoMovimentacao = tipoMovimentacao;

            foreach (var item in notaFiscal.ListaItens)
            {
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                itemMovimentacao.Produto = item.Produto;
                itemMovimentacao.Quantidade = item.Quantidade;

                movimentacaoBase.ListaDeItens.Add(itemMovimentacao);
            }

            servicoMovimentacao.Cadastre(movimentacaoBase);

            var repositorioProdutos = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            List<Produto> listaProdutos = new List<Produto>();

            foreach (var item in notaFiscal.ListaItens)
            {
                var produto = repositorioProdutos.Consulte(item.Produto.Id);

                produto.FormacaoPreco.EstoqueReservado -= item.Quantidade;

                listaProdutos.Add(produto);
            }

            repositorioProdutos.AtualizeLista(listaProdutos);
        }

        public void InativeContasAReceberDaNotaFiscalOutrasSaidas(NotaFiscal notaFiscal)
        {
            if (notaFiscal.DadosCobranca == null)
                return;
            
            ServicoPedidoDeVenda servicoPedidoVenda = new ServicoPedidoDeVenda();
            var pedidoVenda = servicoPedidoVenda.Consulte(notaFiscal.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);
            servicoPedidoVenda.InativeContasAReceberDoPedido(pedidoVenda);  
        }

        #endregion


    }
}
