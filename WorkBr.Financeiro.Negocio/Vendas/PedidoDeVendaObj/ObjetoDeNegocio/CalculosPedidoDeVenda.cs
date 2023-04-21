using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    public class CalculosPedidoDeVenda
    {
        private readonly CalculosNotaFiscal _calculosNotaFiscal;

        public CalculosPedidoDeVenda()
        {
            _calculosNotaFiscal = new CalculosNotaFiscal();
        }

        public static double CalculePrecoUnitarioProduto(TabelaPreco tabelaPreco, Produto produto)
        {
            if (produto.FormacaoPreco.EhPromocao.GetValueOrDefault())
            {
                return produto.FormacaoPreco.ValorPromocao.GetValueOrDefault();
            }

            double? valorProdutoSemTabelaPreco = produto.FormacaoPreco.ValorVenda;

            double valorUnitario = valorProdutoSemTabelaPreco.GetValueOrDefault();

            if (tabelaPreco == null)
                return valorUnitario;

            if (tabelaPreco.Acrescimo > 0)
            {
                if (tabelaPreco.AcrescimoEhPercentual)
                {
                    valorUnitario += valorUnitario * tabelaPreco.Acrescimo / (double)100;
                }
                else
                {
                    valorUnitario += tabelaPreco.Acrescimo;
                }
            }
            else if (tabelaPreco.Decrescimo > 0)
            {
                if (tabelaPreco.DecrescimoEhPercentual)
                {
                    valorUnitario -= valorUnitario * tabelaPreco.Decrescimo / (double)100;
                }
                else
                {
                    valorUnitario -= tabelaPreco.Decrescimo;
                }
            }

            return valorUnitario;
        }

        public static bool DescontoEstahIgualOuAbaixoDoPermitido(PedidoDeVenda pedidoDeVenda)
        {
            var repositorioComissao = FabricaDeRepositorios.Crie<IRepositorioComissao>();
            var repositorioTabelaPreco = FabricaDeRepositorios.Crie<IRepositorioTabelaPreco>();

            var tabelaPreco = repositorioTabelaPreco.Consulte(pedidoDeVenda.TabelaPreco.Id);

            var valorTotalSemDesconto = pedidoDeVenda.ListaItens.Sum(item => item.ValorUnitario * item.Quantidade);

            var listaComissoes = repositorioComissao.ConsultePorPessoaETabelaPreco(pedidoDeVenda.Usuario, tabelaPreco);

            Comissao comissaoMaiorDesconto = listaComissoes.FirstOrDefault();

            comissaoMaiorDesconto = comissaoMaiorDesconto ?? new Comissao();

            for (int i = 1; i < listaComissoes.Count; i++)
            {
                var comissao = listaComissoes[i];

                var valorPrimeiroDesconto = ApliqueDescontoComissao(valorTotalSemDesconto, comissaoMaiorDesconto);
                var valorSegundoDesconto = ApliqueDescontoComissao(valorTotalSemDesconto, comissao);

                if (valorSegundoDesconto > valorPrimeiroDesconto)
                {
                    comissaoMaiorDesconto = comissao;
                }
            }

            if (comissaoMaiorDesconto.DescontoMaximoEhPercentual)
            {
                if (pedidoDeVenda.DescontoEhPercentual && pedidoDeVenda.Desconto > comissaoMaiorDesconto.DescontoMaximo)
                {
                    return false;
                }

                var percentualDescontoAplicado = Math.Round(100 - ((pedidoDeVenda.ValorTotal / (double)valorTotalSemDesconto) * 100), 5);

                return comissaoMaiorDesconto.DescontoMaximo >= percentualDescontoAplicado;
            }
            else
            {
                var valorDescontoAplicado = valorTotalSemDesconto - pedidoDeVenda.ValorTotal;

                return comissaoMaiorDesconto.DescontoMaximo >= valorDescontoAplicado;
            }
        }

        public static double ApliqueDescontoComissao(double valor, Comissao comissao)
        {
            if (comissao.DescontoMaximoEhPercentual)
            {
                return valor * comissao.DescontoMaximo / (double)100;
            }
            else
            {
                return valor - comissao.DescontoMaximo;
            }
        }

        public static double CalculeTotalDesconto(double valorUnitario, double quantidade, double desconto, bool descontoEhPercentual)
        {
            if (descontoEhPercentual)
            {
                double valorTotalSemDesconto = Math.Round(valorUnitario * quantidade, 2);
                return Math.Round(valorTotalSemDesconto * (desconto / (double)100), 2);
            }
            else
            {
                return Math.Round(desconto * quantidade, 2);
            }
        }

        public void RecalculeValoresItens(bool ratearDesconto, bool descontoEhPercentual, double desconto, double valorFrete, List<ItemPedidoDeVenda> listaItens, string ufDestino, EnumTipoCliente tipoCliente, EnumTipoInscricaoICMS? tipoInscricaoIcms)
        {
            double totalBruto = listaItens.Sum(i => i.ValorUnitario * i.Quantidade);
            double totalFreteAplicado = 0;
            double totalDescontoAplicado = 0;
            int totalItens = listaItens.Count;

            ValidacaoItemPedidoDeVenda validacaoItemPedidoDeVenda = new ValidacaoItemPedidoDeVenda();
            validacaoItemPedidoDeVenda.ValideItemLiberacao();

            for (int contadorItens = 0; contadorItens < totalItens; contadorItens++)
            {
                ItemPedidoDeVenda item = listaItens[contadorItens];

                if (ratearDesconto)
                {
                    if (descontoEhPercentual)
                    {
                        item.DescontoEhPercentual = true;
                        item.DescontoUnitario = desconto;
                        item.TotalDesconto = CalculosPedidoDeVenda.CalculeTotalDesconto(item.ValorUnitario, item.Quantidade, item.DescontoUnitario, item.DescontoEhPercentual);
                    }
                    else
                    {
                        item.DescontoEhPercentual = false;
                        item.TotalDesconto = Math.Round((item.Quantidade * item.ValorUnitario * desconto) / totalBruto, 2);

                        totalDescontoAplicado += item.TotalDesconto;

                        if (contadorItens == totalItens - 1)
                        {
                            var diferencaDesconto = desconto - totalDescontoAplicado;

                            item.TotalDesconto += diferencaDesconto;
                        }

                        item.DescontoUnitario = Math.Round(item.TotalDesconto / (double)item.Quantidade, 4);
                    }
                }

                item.ValorFrete = Math.Round((item.Quantidade * item.ValorUnitario * valorFrete) / totalBruto, 2);
                totalFreteAplicado += item.ValorFrete;

                if (contadorItens == totalItens - 1)
                {
                    var diferencaFrete = valorFrete - totalFreteAplicado;

                    item.ValorFrete += diferencaFrete;
                }

                DefinaIcmsST(item, ufDestino, tipoCliente, tipoInscricaoIcms);

                item.ValorTotal = RetorneValorTotalItem(item.ValorUnitario, item.Quantidade, item.TotalDesconto, item.ValorFrete, item.ValorIpi, item.ValorIcmsST);

                try
                {
                    validacaoItemPedidoDeVenda.Valide(item).AssegureSucesso();

                    item.ItemEstahInconsistente = false;
                }
                catch (Exception)
                {
                    item.ItemEstahInconsistente = true;
                }
            }
        }

        public void DefinaIcmsST(ItemPedidoDeVenda itemPedidoDeVenda, string ufDestino, EnumTipoCliente tipoCliente, EnumTipoInscricaoICMS? tipoInscricaoIcms)
        {
            double? aliquotaSimplesNacional = null;
            double? aliquotaCreditoST = null;
            double? aliquotaDebitoST = null;
            double? mva = null;
            double? reducaoBaseCalculoST = null;
            double? icmsBaseCalculo = null;
            double? icmsReducaoBaseCalculo = null;

            EnumCstCsosn cstCsosn = EnumCstCsosn.NORMAL00;
            Cfop cfop = null;

            if (itemPedidoDeVenda.TributacaoIcms != null)
            {
                cstCsosn = (EnumCstCsosn)itemPedidoDeVenda.TributacaoIcms.CstCsosn != EnumCstCsosn.NORMAL00? (EnumCstCsosn)itemPedidoDeVenda.TributacaoIcms.CstCsosn: EnumCstCsosn.NORMAL00; 
                cfop = itemPedidoDeVenda.TributacaoIcms.Cfop != null? itemPedidoDeVenda.TributacaoIcms.Cfop: null;
                mva = itemPedidoDeVenda.TributacaoIcms.MVA != 0 ? itemPedidoDeVenda.TributacaoIcms.MVA : null;
                aliquotaSimplesNacional = itemPedidoDeVenda.TributacaoIcms.aliquotaSimplesNacional != 0 ? itemPedidoDeVenda.TributacaoIcms.aliquotaSimplesNacional : null;
                aliquotaCreditoST = itemPedidoDeVenda.TributacaoIcms.AliquotaCreditoST != 0 ? itemPedidoDeVenda.TributacaoIcms.AliquotaCreditoST : null;
                aliquotaDebitoST = itemPedidoDeVenda.TributacaoIcms.AliquotaDebitoST != 0 ? itemPedidoDeVenda.TributacaoIcms.AliquotaDebitoST : null;
                reducaoBaseCalculoST = itemPedidoDeVenda.TributacaoIcms.ReducaoBaseST != 0 ? itemPedidoDeVenda.TributacaoIcms.ReducaoBaseST : null;
                icmsReducaoBaseCalculo = itemPedidoDeVenda.TributacaoIcms.IcmsReducaoBaseCalculo != 0 ? itemPedidoDeVenda.TributacaoIcms.IcmsReducaoBaseCalculo : null;
                icmsBaseCalculo = itemPedidoDeVenda.TributacaoIcms.IcmsBaseCalculo != 0 ? itemPedidoDeVenda.TributacaoIcms.IcmsBaseCalculo : null;

                _calculosNotaFiscal.DefinaCstCsosnCfopEAliquotas(itemPedidoDeVenda.Produto,
                                                                                       itemPedidoDeVenda.TributacaoIcms.TipoSaida,
                                                                                       ufDestino,
                                                                                       tipoCliente,
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
            }
            else
            {
                _calculosNotaFiscal.DefinaCstCsosnCfopEAliquotas(itemPedidoDeVenda.Produto,
                                                                                           EnumTipoSaidaTributacaoIcms.SAIDAVENDA,
                                                                                           ufDestino,
                                                                                           tipoCliente,
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

                
            }

            double? baseDeCalculoIcmsST = null;
            double? valorIcmsST = null;
            double? valorIcms = null;
            double? baseDeCalculoIcms = null;
            

            _calculosNotaFiscal.CalculeValorIcmsST(itemPedidoDeVenda.Produto, itemPedidoDeVenda.ValorUnitario,
                                                                      itemPedidoDeVenda.Quantidade,
                                                                      itemPedidoDeVenda.TotalDesconto,
                                                                      itemPedidoDeVenda.ValorFrete,
                                                                      itemPedidoDeVenda.ValorIpi,
                                                                      cstCsosn,
                                                                      aliquotaCreditoST,
                                                                      aliquotaDebitoST,
                                                                      mva,
                                                                      reducaoBaseCalculoST, 
                                                                      ref baseDeCalculoIcms, 
                                                                      ref valorIcms,
                                                                      ref baseDeCalculoIcmsST,
                                                                      ref valorIcmsST, 
                                                                      icmsBaseCalculo,
                                                                      icmsReducaoBaseCalculo,
                                                                      itemPedidoDeVenda.ValorSeguro,
                                                                      itemPedidoDeVenda.ValorOutrasDespesas);

            //Alimenta o objeto com os valores dos impostos
            itemPedidoDeVenda.ValorIcmsST = valorIcmsST;
            if (itemPedidoDeVenda.TributacaoIcms != null)
            {
                itemPedidoDeVenda.TributacaoIcms.CstCsosn = cstCsosn;
                itemPedidoDeVenda.TributacaoIcms.Cfop = cfop;
                itemPedidoDeVenda.TributacaoIcms.aliquotaSimplesNacional = aliquotaSimplesNacional;
                itemPedidoDeVenda.TributacaoIcms.AliquotaCreditoST = aliquotaCreditoST;
                itemPedidoDeVenda.TributacaoIcms.AliquotaDebitoST = aliquotaDebitoST;
                itemPedidoDeVenda.TributacaoIcms.MVA = mva;
                itemPedidoDeVenda.TributacaoIcms.ReducaoBaseST = reducaoBaseCalculoST;
                itemPedidoDeVenda.TributacaoIcms.baseDeCalculoIcmsST = baseDeCalculoIcmsST;
                itemPedidoDeVenda.TributacaoIcms.baseDeCalculoIcms = baseDeCalculoIcms;
                itemPedidoDeVenda.ValorIcms = valorIcms;
                itemPedidoDeVenda.ValorIcmsST = valorIcmsST;
            }
        }

        public double RetorneValorTotalItem(double valorUnitario, double quantidade, double valorTotalDesconto, double valorFrete, double? valorIpi, double? valorIcmsST, double? valorSeguro=0, double? valorOutrasDespesas=0)
        {
            var totalSemDesconto = Math.Round((valorUnitario * quantidade),2);
            double total = totalSemDesconto - valorTotalDesconto + valorFrete +valorSeguro.GetValueOrDefault() + valorOutrasDespesas.GetValueOrDefault() + valorIpi.GetValueOrDefault() + valorIcmsST.GetValueOrDefault();
     
            return total;
        }

        #region "REGIME NORMAL"


        public void DefinaIcmsRegimeNormal(ItemPedidoDeVenda itemPedidoDeVenda, string ufDestino, EnumTipoCliente tipoCliente, EnumTipoInscricaoICMS? tipoInscricaoIcms)
        {
            double? icmsBaseCalculo = null;
            double? icmsReducaoBaseCalculo = null;
            double? percentualMargemAdicST = null;
            double? aliquotaIcmsST = null;            
            double? reducaoBaseST = null;

            EnumModBC? modalidadeBC = null;
            EnumModBCST? modalidadeST = null;          

            EnumCstCsosn? cstCsosn = null;
            Cfop cfop = null;

            if (itemPedidoDeVenda.TributacaoIcms != null)
            {
                cstCsosn = (EnumCstCsosn?)itemPedidoDeVenda.TributacaoIcms.CstCsosn != null ? (EnumCstCsosn?)itemPedidoDeVenda.TributacaoIcms.CstCsosn : null;
                cfop = itemPedidoDeVenda.TributacaoIcms.Cfop != null ? itemPedidoDeVenda.TributacaoIcms.Cfop : null;
                icmsBaseCalculo = itemPedidoDeVenda.TributacaoIcms.IcmsBaseCalculo != 0 ? itemPedidoDeVenda.TributacaoIcms.IcmsBaseCalculo : null;
                icmsReducaoBaseCalculo = itemPedidoDeVenda.TributacaoIcms.IcmsReducaoBaseCalculo != 0 ? itemPedidoDeVenda.TributacaoIcms.IcmsReducaoBaseCalculo : null;
                percentualMargemAdicST = itemPedidoDeVenda.TributacaoIcms.PercentualMargemAdicST != 0 ? itemPedidoDeVenda.TributacaoIcms.PercentualMargemAdicST : null;
                aliquotaIcmsST = itemPedidoDeVenda.TributacaoIcms.AliquotaIcmsST != 0 ? itemPedidoDeVenda.TributacaoIcms.AliquotaIcmsST : null;
                reducaoBaseST = itemPedidoDeVenda.TributacaoIcms.ReducaoBaseST != 0 ? itemPedidoDeVenda.TributacaoIcms.ReducaoBaseST : null;


                _calculosNotaFiscal.DefinaCstCsosnCfopEAliquotasRegimeNormal(itemPedidoDeVenda.Produto,
                                                                                       itemPedidoDeVenda.TributacaoIcms.TipoSaida,
                                                                                       ufDestino,
                                                                                       tipoCliente,
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
            }
            else
            {
                _calculosNotaFiscal.DefinaCstCsosnCfopEAliquotasRegimeNormal(itemPedidoDeVenda.Produto,
                                                                                           EnumTipoSaidaTributacaoIcms.SAIDAVENDA,
                                                                                           ufDestino,
                                                                                           tipoCliente,
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


            }

            double? baseDeCalculoIcms = null;
            double? valorIcms = null;

            double? baseDeCalculoIcmsST = null;
            double? valorIcmsST = null;

            _calculosNotaFiscal.CalculeValorIcmsRegimeNormal(itemPedidoDeVenda.Produto, 
                                                                      itemPedidoDeVenda.ValorUnitario,
                                                                      itemPedidoDeVenda.Quantidade,
                                                                      itemPedidoDeVenda.TotalDesconto,
                                                                      itemPedidoDeVenda.ValorFrete,
                                                                      itemPedidoDeVenda.ValorIpi,
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
                                                                      itemPedidoDeVenda.ValorSeguro,
                                                                      itemPedidoDeVenda.ValorOutrasDespesas);

            //Alimenta o objeto com os valores dos impostos
            if (itemPedidoDeVenda.TributacaoIcms == null)
                itemPedidoDeVenda.TributacaoIcms = new TributacaoIcms();

                itemPedidoDeVenda.TributacaoIcms.CstCsosn = cstCsosn;
                itemPedidoDeVenda.TributacaoIcms.Cfop = cfop;
                itemPedidoDeVenda.TributacaoIcms.PercentualMargemAdicST = percentualMargemAdicST;
                itemPedidoDeVenda.TributacaoIcms.AliquotaIcmsST = aliquotaIcmsST;
                itemPedidoDeVenda.TributacaoIcms.ReducaoBaseST = reducaoBaseST;
                itemPedidoDeVenda.TributacaoIcms.IcmsBaseCalculo = icmsBaseCalculo;
                itemPedidoDeVenda.TributacaoIcms.IcmsReducaoBaseCalculo = icmsReducaoBaseCalculo;

                itemPedidoDeVenda.TributacaoIcms.baseDeCalculoIcmsST = baseDeCalculoIcmsST;
                itemPedidoDeVenda.ValorIcmsST = valorIcmsST;
                itemPedidoDeVenda.TributacaoIcms.baseDeCalculoIcms = baseDeCalculoIcms;
                itemPedidoDeVenda.ValorIcms = valorIcms;

            
        }

        public void DefinaIpi(ItemPedidoDeVenda itemPedidoDeVenda, string ufDestino, EnumTipoCliente tipoCliente)
        {
            double? IpiBaseCalculo = null;
            double? aliquotaIpi = null;
            
            EnumCstIpi? cstIpi = null;
            
            if (itemPedidoDeVenda.Ipi != null)
            {
                cstIpi = (EnumCstIpi?)itemPedidoDeVenda.Ipi.CstIpi != null ? (EnumCstIpi?)itemPedidoDeVenda.Ipi.CstIpi : null;
                IpiBaseCalculo = itemPedidoDeVenda.Ipi.BaseDeCalculo != 0 ? itemPedidoDeVenda.Ipi.BaseDeCalculo : null;
                aliquotaIpi = itemPedidoDeVenda.Ipi.AliquotaIpi != 0 ? itemPedidoDeVenda.Ipi.AliquotaIpi : null;
                
                _calculosNotaFiscal.DefinaCstIpiEAliquotas(itemPedidoDeVenda.Produto,
                                                                                       itemPedidoDeVenda.TributacaoIcms.TipoSaida,
                                                                                       ufDestino,
                                                                                       tipoCliente,
                                                                                       ref cstIpi,                                                                                       
                                                                                       ref aliquotaIpi);
            }
            else
            {
                _calculosNotaFiscal.DefinaCstIpiEAliquotas(itemPedidoDeVenda.Produto,
                                                                                           EnumTipoSaidaTributacaoIcms.SAIDAVENDA,
                                                                                           ufDestino,
                                                                                           tipoCliente,
                                                                                           ref cstIpi,                                                                                           
                                                                                           ref aliquotaIpi);


            }

            double baseDeCalculoIpi=0;
            double? valorIpi = null;
            
            valorIpi = _calculosNotaFiscal.CalculeValorIpi(itemPedidoDeVenda.ValorUnitario,
                                                                      itemPedidoDeVenda.Quantidade,
                                                                      itemPedidoDeVenda.TotalDesconto,
                                                                      itemPedidoDeVenda.ValorFrete,
                                                                      itemPedidoDeVenda.ValorSeguro.ToDouble(),
                                                                      itemPedidoDeVenda.ValorOutrasDespesas.ToDouble(),
                                                                      aliquotaIpi.ToDouble(),
                                                                      ref baseDeCalculoIpi);

            //Alimenta o objeto com os valores dos impostos
            if (itemPedidoDeVenda.Ipi == null)
                itemPedidoDeVenda.Ipi = new IpiNotaFiscal();

            itemPedidoDeVenda.Ipi.CstIpi= cstIpi; 
            itemPedidoDeVenda.Ipi.AliquotaIpi = aliquotaIpi;
            itemPedidoDeVenda.Ipi.BaseDeCalculo = baseDeCalculoIpi;
           
            itemPedidoDeVenda.ValorIpi = valorIpi;
        }

        public void DefinaPis(ItemPedidoDeVenda itemPedidoDeVenda, string ufDestino, EnumTipoCliente tipoCliente)
        {
            double? PisBaseCalculo = null;
            double? PisBaseCalculoST = null;
            double? aliquotaPercentual = null;
            double? aliquotaPercentualST = null;
            double? aliquotaReais = null;
            double? aliquotaReaisST = null;

            EnumCstPis? cstPis = null;

            if (itemPedidoDeVenda.Pis != null)
            {
                cstPis = itemPedidoDeVenda.Pis != null ? (EnumCstPis?)itemPedidoDeVenda.Pis.CstPis != null ? (EnumCstPis?)itemPedidoDeVenda.Pis.CstPis : null:null;
                PisBaseCalculo = itemPedidoDeVenda.Pis != null ? itemPedidoDeVenda.Pis.BaseDeCalculo != 0 ? itemPedidoDeVenda.Pis.BaseDeCalculo : null: null;
                PisBaseCalculoST = itemPedidoDeVenda.Pis != null ? itemPedidoDeVenda.Pis.BaseDeCalculoST != 0 ? itemPedidoDeVenda.Pis.BaseDeCalculoST : null:null;
                aliquotaPercentual = itemPedidoDeVenda.Pis != null ? itemPedidoDeVenda.Pis.AliquotaPercentual != 0 ? itemPedidoDeVenda.Pis.AliquotaPercentual : null:null;
                aliquotaPercentualST = itemPedidoDeVenda.Pis != null ? itemPedidoDeVenda.Pis.AliquotaPercentualST != 0 ? itemPedidoDeVenda.Pis.AliquotaPercentualST : null:null;
                aliquotaReais = itemPedidoDeVenda.Pis != null ? itemPedidoDeVenda.Pis.AliquotaReais != 0 ? itemPedidoDeVenda.Pis.AliquotaReais : null:null;
                aliquotaReaisST = itemPedidoDeVenda.Pis != null ? itemPedidoDeVenda.Pis.AliquotaReaisST != 0 ? itemPedidoDeVenda.Pis.AliquotaReaisST : null:null;

                _calculosNotaFiscal.DefinaCstPisEAliquotas(itemPedidoDeVenda.Produto,
                                                                                       itemPedidoDeVenda.TributacaoIcms.TipoSaida,
                                                                                       ufDestino,
                                                                                       tipoCliente,
                                                                                       ref cstPis,
                                                                                       ref aliquotaPercentual,
                                                                                       ref aliquotaPercentualST,
                                                                                       ref aliquotaReais,
                                                                                       ref aliquotaReaisST);
            }
            else
            {
                _calculosNotaFiscal.DefinaCstPisEAliquotas(itemPedidoDeVenda.Produto,
                                                                                           EnumTipoSaidaTributacaoIcms.SAIDAVENDA,
                                                                                           ufDestino,
                                                                                           tipoCliente,
                                                                                           ref cstPis,
                                                                                           ref aliquotaPercentual,
                                                                                           ref aliquotaPercentualST,
                                                                                           ref aliquotaReais,
                                                                                           ref aliquotaReaisST);


            }

            double baseDeCalculoPis = 0;
            double? valorPis = null;

            valorPis = _calculosNotaFiscal.CalculeValorPis(itemPedidoDeVenda.ValorUnitario,
                                                                      itemPedidoDeVenda.Quantidade,
                                                                      itemPedidoDeVenda.TotalDesconto,
                                                                      itemPedidoDeVenda.ValorFrete,
                                                                      itemPedidoDeVenda.ValorSeguro.ToDouble(),
                                                                      itemPedidoDeVenda.ValorOutrasDespesas.ToDouble(),
                                                                      ref baseDeCalculoPis,
                                                                      aliquotaPercentual.ToDouble(),
                                                                      aliquotaReais.ToDouble());

            //Alimenta o objeto com os valores dos impostos
            if (itemPedidoDeVenda.Pis == null)
                itemPedidoDeVenda.Pis = new PisNotaFiscal();

            itemPedidoDeVenda.Pis.CstPis = cstPis;
            itemPedidoDeVenda.Pis.AliquotaPercentual = aliquotaPercentual;
            itemPedidoDeVenda.Pis.AliquotaPercentualST = aliquotaPercentualST;
            itemPedidoDeVenda.Pis.AliquotaReais = aliquotaReais;
            itemPedidoDeVenda.Pis.AliquotaReaisST = aliquotaReaisST;
            itemPedidoDeVenda.Pis.BaseDeCalculo = baseDeCalculoPis;

            itemPedidoDeVenda.Pis.ValorPis = valorPis;
        }

        public void DefinaCofins(ItemPedidoDeVenda itemPedidoDeVenda, string ufDestino, EnumTipoCliente tipoCliente)
        {
            double? CofinsBaseCalculo = null;
            double? CofinsBaseCalculoST = null;
            double? aliquotaPercentual = null;
            double? aliquotaPercentualST = null;
            double? aliquotaReais = null;
            double? aliquotaReaisST = null;

            EnumCstCofins? cstCofins = null;

            if (itemPedidoDeVenda.Cofins != null)
            {
                cstCofins = (EnumCstCofins?)itemPedidoDeVenda.Cofins.CstCofins != null ? (EnumCstCofins?)itemPedidoDeVenda.Cofins.CstCofins : null;
                CofinsBaseCalculo = itemPedidoDeVenda.Cofins.BaseDeCalculo != 0 ? itemPedidoDeVenda.Cofins.BaseDeCalculo : null;
                CofinsBaseCalculoST = itemPedidoDeVenda.Cofins.BaseDeCalculoST != 0 ? itemPedidoDeVenda.Cofins.BaseDeCalculoST : null;
                aliquotaPercentual = itemPedidoDeVenda.Pis.AliquotaPercentual != 0 ? itemPedidoDeVenda.Pis.AliquotaPercentual : null;
                aliquotaPercentualST = itemPedidoDeVenda.Cofins.AliquotaPercentualST != 0 ? itemPedidoDeVenda.Cofins.AliquotaPercentualST : null;
                aliquotaReais = itemPedidoDeVenda.Cofins.AliquotaReais != 0 ? itemPedidoDeVenda.Cofins.AliquotaReais : null;
                aliquotaReaisST = itemPedidoDeVenda.Cofins.AliquotaReaisST != 0 ? itemPedidoDeVenda.Cofins.AliquotaReaisST : null;

                _calculosNotaFiscal.DefinaCstCofinsEAliquotas(itemPedidoDeVenda.Produto,
                                                                                       itemPedidoDeVenda.TributacaoIcms.TipoSaida,
                                                                                       ufDestino,
                                                                                       tipoCliente,
                                                                                       ref cstCofins,
                                                                                       ref aliquotaPercentual,
                                                                                       ref aliquotaPercentualST,
                                                                                       ref aliquotaReais,
                                                                                       ref aliquotaReaisST);
            }
            else
            {
                _calculosNotaFiscal.DefinaCstCofinsEAliquotas(itemPedidoDeVenda.Produto,
                                                                                           EnumTipoSaidaTributacaoIcms.SAIDAVENDA,
                                                                                           ufDestino,
                                                                                           tipoCliente,
                                                                                           ref cstCofins,
                                                                                           ref aliquotaPercentual,
                                                                                           ref aliquotaPercentualST,
                                                                                           ref aliquotaReais,
                                                                                           ref aliquotaReaisST);


            }

            double baseDeCalculoCofins = 0;
            double? valorCofins = null;

            valorCofins = _calculosNotaFiscal.CalculeValorCofins(itemPedidoDeVenda.ValorUnitario,
                                                                      itemPedidoDeVenda.Quantidade,
                                                                      itemPedidoDeVenda.TotalDesconto,
                                                                      itemPedidoDeVenda.ValorFrete,
                                                                      itemPedidoDeVenda.ValorSeguro.ToDouble(),
                                                                      itemPedidoDeVenda.ValorOutrasDespesas.ToDouble(),
                                                                      ref baseDeCalculoCofins,
                                                                      aliquotaPercentual.ToDouble(),
                                                                      aliquotaReais.ToDouble());

            //Alimenta o objeto com os valores dos impostos
            if (itemPedidoDeVenda.Cofins == null)
                itemPedidoDeVenda.Cofins = new CofinsNotaFiscal();

            itemPedidoDeVenda.Cofins.CstCofins = cstCofins;
            itemPedidoDeVenda.Cofins.AliquotaPercentual = aliquotaPercentual;
            itemPedidoDeVenda.Cofins.AliquotaPercentualST = aliquotaPercentualST;
            itemPedidoDeVenda.Cofins.AliquotaReais = aliquotaReais;
            itemPedidoDeVenda.Cofins.AliquotaReaisST = aliquotaReaisST;
            itemPedidoDeVenda.Cofins.BaseDeCalculo = baseDeCalculoCofins;

            itemPedidoDeVenda.Cofins.ValorCofins = valorCofins;
        }

        #endregion

    }
}
