using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    public class CalculosNotaFiscal
    {
        private readonly IRepositorioProduto _repositorioProduto;
        private GrupoTributacaoIcms _grupoTributacaoIcmsParametrosTerceiros;
        private GrupoTributacaoFederal _grupoTributacaoFederalParametrosTerceiros;
        private GrupoTributacaoIcms _grupoTributacaoIcmsParametrosProducaoPropria;
        private GrupoTributacaoFederal _grupoTributacaoFederalParametrosProducaoPropria;
       

        public CalculosNotaFiscal()
        {
            _repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();
        }

        public void DefinaCstCsosnCfopEAliquotas(Produto produto,
                                                                        EnumTipoSaidaTributacaoIcms tipoSaida,
                                                                        string ufDestino,
                                                                        EnumTipoCliente tipoCliente,
                                                                        EnumTipoInscricaoICMS? tipoInscricaoIcms,
                                                                        ref EnumCstCsosn cstCsosn,
                                                                        ref Cfop cfop,
                                                                        ref double? aliquotaSimplesNacional,
                                                                        ref double? aliquotaCreditoST,
                                                                        ref double? aliquotaDebitoST,
                                                                        ref double? mva,
                                                                        ref double? ReducaoBaseCalculoST,
                                                                        ref double? icmsBaseCalculo,
                                                                        ref double? icmsReducaoBaseCalculo)
        {
            var produtoBase = _repositorioProduto.ConsulteComJoinGrupoTributacaoETributacoes(produto.Id);
                        
            var grupoTributacaoIcms = produtoBase.ContabilFiscal.GrupoTributacaoIcms;

            if (grupoTributacaoIcms == null)
            {
                grupoTributacaoIcms = RetorneGrupoTributacaoParametros(produto);

                if (grupoTributacaoIcms == null)
                {
                    return;
                }
            }
            
            var tributacaoIcms = grupoTributacaoIcms.ListaTributacoesIcms.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida &&
                                                                                            tributacao.TipoCliente == tipoCliente &&
                                                                                            tributacao.EstadoDestino == ufDestino &&
                                                                                            tributacao.TipoInscricaoICMS == tipoInscricaoIcms);


            if(tributacaoIcms == null)
            {
                tributacaoIcms = grupoTributacaoIcms.ListaTributacoesIcms.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida &&
                                                                                            tributacao.TipoCliente == tipoCliente &&
                                                                                            tributacao.EstadoDestino == ufDestino);
            }
            

            if (tributacaoIcms == null)
            {
                tributacaoIcms = grupoTributacaoIcms.ListaTributacoesIcms.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida && 
                                                                                            tributacao.TipoCliente == tipoCliente && 
                                                                                            tributacao.EstadoDestino == "IE" &&
                                                                                             tributacao.TipoInscricaoICMS == tipoInscricaoIcms);

                if(tributacaoIcms == null)
                {
                    tributacaoIcms = grupoTributacaoIcms.ListaTributacoesIcms.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida &&
                                                                                            tributacao.TipoCliente == tipoCliente &&
                                                                                            tributacao.EstadoDestino == "IE");
                }
            }
            
            if (cstCsosn != EnumCstCsosn.NORMAL00 && cfop != null)
            {
                EnumCstCsosn cstPesquisa= cstCsosn;
                Cfop cfopPesquisa = cfop;
                tributacaoIcms = grupoTributacaoIcms.ListaTributacoesIcms.FirstOrDefault(
                                            tributacao => tributacao.TipoSaida == tipoSaida &&
                                            tributacao.TipoCliente == tipoCliente &&
                                            tributacao.EstadoDestino == ufDestino &&
                                            tributacao.CstCsosn == cstPesquisa &&
                                            tributacao.Cfop.Id == cfopPesquisa.Id);
            }
                        
            if (tributacaoIcms == null)
            {
                return;
            }

            cstCsosn = tributacaoIcms.CstCsosn.GetValueOrDefault();
            cfop = tributacaoIcms.Cfop;
            aliquotaCreditoST = tributacaoIcms.AliquotaCreditoST;
            aliquotaDebitoST = tributacaoIcms.AliquotaDebitoST;
            mva = mva == null? tributacaoIcms.MVA: mva;
            ReducaoBaseCalculoST = tributacaoIcms.ReducaoBaseST;
            icmsBaseCalculo = tributacaoIcms.IcmsBaseCalculo;
            icmsReducaoBaseCalculo = tributacaoIcms.IcmsReducaoBaseCalculo;

            if (cstCsosn == EnumCstCsosn.SIMPLES101 || cstCsosn == EnumCstCsosn.SIMPLES201 || cstCsosn == EnumCstCsosn.SIMPLES900)
            {
                var repositorioConfiguracoesNfe = FabricaDeRepositorios.Crie<IRepositorioConfiguracaoNfe>();
                var configuracaoNfe = repositorioConfiguracoesNfe.ConsulteConfiguracoesNfe();

                aliquotaSimplesNacional = aliquotaSimplesNacional == null ? configuracaoNfe.AliquotaSimplesNacional: aliquotaSimplesNacional;
            }
        }

        public void CalculeValorIcmsST(Produto produto, double valorUnitario,
                                                       double quantidade,
                                                       double? totalDesconto,
                                                       double? valorFrete,
                                                       double? valorIpi,
                                                       EnumCstCsosn cstCsosn,
                                                       double? aliquotaCreditoST,
                                                       double? aliquotaDebitoST,
                                                       double? mva,
                                                       double? reducaoBaseCalculoST,
                                                       ref double? baseDeCalculoIcms,
                                                       ref double? valorIcms,
                                                       ref double? baseDeCalculoST,
                                                       ref double? valorIcmsST,
                                                       double? icmsBaseCalculo = 0,
                                                       double? icmsReducaoBaseCalculo=0,
                                                       double? valorSeguro=0,
                                                       double? valorOutrasDespesas=0)
        {
            if (cstCsosn != EnumCstCsosn.SIMPLES201 &&
                cstCsosn != EnumCstCsosn.SIMPLES202 &&
                cstCsosn != EnumCstCsosn.SIMPLES203 &&
                cstCsosn != EnumCstCsosn.SIMPLES900)
            {
                return;
            }

            var produtoBase = _repositorioProduto.ConsulteComJoinGrupoTributacaoETributacoes(produto.Id);

            if (cstCsosn == EnumCstCsosn.SIMPLES900)
            {                
                double TotalProdutos = (valorUnitario * quantidade - totalDesconto.GetValueOrDefault()) + valorIpi.GetValueOrDefault() + valorFrete.GetValueOrDefault() + valorSeguro.GetValueOrDefault() + valorOutrasDespesas.GetValueOrDefault();
                //*****************************ICMS***************************************************************************
                //***********Sem Redução
                //Cálculo ICMS sem Redução
                if (icmsReducaoBaseCalculo == 0 || icmsReducaoBaseCalculo == null)
                {
                    baseDeCalculoIcms = TotalProdutos;

                    valorIcms = baseDeCalculoIcms * icmsBaseCalculo / (double)100;
                }
                //**********Redução
                //**Cálculo ICMS com redução
                else
                {
                   baseDeCalculoIcms = (TotalProdutos - TotalProdutos * (icmsReducaoBaseCalculo.GetValueOrDefault() / (double)100));

                    valorIcms = baseDeCalculoIcms * icmsBaseCalculo / (double)100;
                }
            }

            if (produtoBase.ContabilFiscal.SituacaoTributariaProduto != EnumSituacaoTributariaProduto.SUBSTITUICAOTRIBUTARIA)
                return;

            double totalProdutos = (valorUnitario * quantidade - totalDesconto.GetValueOrDefault()) + valorIpi.GetValueOrDefault() + valorFrete.GetValueOrDefault() + valorSeguro.GetValueOrDefault() + valorOutrasDespesas.GetValueOrDefault();

            baseDeCalculoST = CalculeBaseCalculoIcmsST(valorUnitario, quantidade, totalDesconto, valorFrete, valorIpi, mva, reducaoBaseCalculoST, valorSeguro, valorOutrasDespesas);

            if (valorIcms == null || valorIcms == 0) //cst diferente de 900 cai aqui
            {
                double icmsNormal = totalProdutos * (aliquotaCreditoST.GetValueOrDefault() / (double)100);
                valorIcmsST = (baseDeCalculoST * (aliquotaDebitoST.GetValueOrDefault() / (double)100)) - icmsNormal;
            }
            else
            {
                //Este caso é se o cst for 900
                valorIcmsST = (baseDeCalculoST * (aliquotaDebitoST.GetValueOrDefault() / (double)100)) - valorIcms;//icmsNormal;
            }
        }

        public double CalculeBaseCalculoIcmsST(double valorUnitario,
                                                                     double quantidade,
                                                                     double? totalDesconto,
                                                                     double? valorFrete,
                                                                     double? valorIpi,
                                                                     double? mva,
                                                                     double? reducaoBaseCalculoST,
                                                                     double? valorSeguro=0,
                                                                     double? valorOutrasDespesas=0)
        {
            double totalProdutos = (valorUnitario * quantidade - totalDesconto.GetValueOrDefault());

            double baseDeCalculoST = totalProdutos + (valorFrete.GetValueOrDefault() + valorIpi.GetValueOrDefault() + valorSeguro.GetValueOrDefault() + valorOutrasDespesas.GetValueOrDefault());
            baseDeCalculoST = baseDeCalculoST + baseDeCalculoST * (mva.GetValueOrDefault() / (double)100);

            if (reducaoBaseCalculoST != null)
            {
                baseDeCalculoST = baseDeCalculoST - baseDeCalculoST * (reducaoBaseCalculoST.Value / (double)100);
            }

            return Math.Round(baseDeCalculoST, 2);
        }

        public static double CalculeValorTotalItem(double valorUnitario, double quantidade, double valorDesconto, double valorFrete, double valorSeguro=0, double valorOutrasDespesas=0, double valorIcmsSt=0)
        {
            double valorTotal = valorUnitario * quantidade - valorDesconto + valorFrete + valorSeguro + valorOutrasDespesas;

            return valorTotal;
        }

        public static double CalculeBaseDeCalculoIcms()
        {
            return 0;
        }

        private GrupoTributacaoIcms RetorneGrupoTributacaoParametros(Produto produto)
        {
            if (produto.ContabilFiscal.NaturezaProduto == EnumNaturezaProduto.FABRICACAOPROPRIA)
            {
                if (_grupoTributacaoIcmsParametrosProducaoPropria == null)
                {
                    var repositorioParametros = FabricaDeRepositorios.Crie<IRepositorioParametros>();
                    var parametros = repositorioParametros.ConsulteParametros();

                    if (parametros.ParametrosFiscais != null)
                    {
                        _grupoTributacaoIcmsParametrosProducaoPropria = parametros.ParametrosFiscais.GrupoTributacaoIcmsProducaoPropria;
                    }
                }

                return _grupoTributacaoIcmsParametrosProducaoPropria;
            }
            else
            {
                if (_grupoTributacaoIcmsParametrosTerceiros == null)
                {
                    var repositorioParametros = FabricaDeRepositorios.Crie<IRepositorioParametros>();
                    var parametros = repositorioParametros.ConsulteParametros();

                    if (parametros.ParametrosFiscais != null)
                    {
                        _grupoTributacaoIcmsParametrosTerceiros = parametros.ParametrosFiscais.GrupoTributacaoIcmsTerceiros;
                    }
                }

                return _grupoTributacaoIcmsParametrosTerceiros;
            }
        }

        private GrupoTributacaoFederal RetorneGrupoTributacaoParametrosFederal(Produto produto)
        {
            if (produto.ContabilFiscal.NaturezaProduto == EnumNaturezaProduto.FABRICACAOPROPRIA)
            {
                if (_grupoTributacaoIcmsParametrosProducaoPropria == null)
                {
                    var repositorioParametros = FabricaDeRepositorios.Crie<IRepositorioParametros>();
                    var parametros = repositorioParametros.ConsulteParametros();

                    if (parametros.ParametrosFiscais != null)
                    {
                        _grupoTributacaoIcmsParametrosProducaoPropria = parametros.ParametrosFiscais.GrupoTributacaoIcmsProducaoPropria;
                    }
                }

                return _grupoTributacaoFederalParametrosProducaoPropria;
            }
            else
            {
                if (_grupoTributacaoFederalParametrosTerceiros == null)
                {
                    var repositorioParametros = FabricaDeRepositorios.Crie<IRepositorioParametros>();
                    var parametros = repositorioParametros.ConsulteParametros();

                    if (parametros.ParametrosFiscais != null)
                    {
                        _grupoTributacaoFederalParametrosTerceiros = parametros.ParametrosFiscais.GrupoTributacaoFederalTerceiros;
                    }
                }

                return _grupoTributacaoFederalParametrosTerceiros;
            }
        }

        #region "Regime Normal"

        public void DefinaCstCsosnCfopEAliquotasRegimeNormal(Produto produto,
                                                                        EnumTipoSaidaTributacaoIcms tipoSaida,
                                                                        string ufDestino,
                                                                        EnumTipoCliente tipoCliente,
                                                                        EnumTipoInscricaoICMS? tipoInscricaoIcms,
                                                                        ref EnumCstCsosn? cstCsosn,
                                                                        ref Cfop cfop,
                                                                        ref double? icmsBaseCalculo,
                                                                        ref double? icmsReducaoBaseCalculo,
                                                                        ref double? percentualMargemAdicST,
                                                                        ref double? aliquotaIcmsST,
                                                                        ref double? reducaoBaseST,
                                                                        ref EnumModBC? modalidadeBC,
                                                                        ref EnumModBCST? modalidadeST)
        {
            var produtoBase = _repositorioProduto.ConsulteComJoinGrupoTributacaoETributacoes(produto.Id);

        

            


            var grupoTributacaoIcms = produtoBase.ContabilFiscal.GrupoTributacaoIcms;

            if (grupoTributacaoIcms == null)
            {
                grupoTributacaoIcms = RetorneGrupoTributacaoParametros(produto);

                if (grupoTributacaoIcms == null)
                {
                    return;
                }
            }

            var tributacaoIcms = grupoTributacaoIcms.ListaTributacoesIcms.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida && 
                                                                                        tributacao.TipoCliente == tipoCliente && 
                                                                                        tributacao.EstadoDestino == ufDestino &&
                                                                                        tributacao.TipoInscricaoICMS == tipoInscricaoIcms);

            if(tributacaoIcms == null)
            {
                tributacaoIcms = grupoTributacaoIcms.ListaTributacoesIcms.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida &&
                                                                                    tributacao.TipoCliente == tipoCliente &&
                                                                                    tributacao.EstadoDestino == ufDestino);
            }
            
            if (tributacaoIcms == null)
            {
                tributacaoIcms = grupoTributacaoIcms.ListaTributacoesIcms.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida &&
                                                                                        tributacao.TipoCliente == tipoCliente &&
                                                                                        tributacao.EstadoDestino == "IE" &&
                                                                                        tributacao.TipoInscricaoICMS == tipoInscricaoIcms);

                if(tributacaoIcms == null)
                {
                    tributacaoIcms = grupoTributacaoIcms.ListaTributacoesIcms.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida &&
                                                                                        tributacao.TipoCliente == tipoCliente &&
                                                                                        tributacao.EstadoDestino == "IE");
                }
            }

            if (cstCsosn != null && cfop != null)
            {
                EnumCstCsosn? cstPesquisa = cstCsosn;
                Cfop cfopPesquisa = cfop;
                tributacaoIcms = grupoTributacaoIcms.ListaTributacoesIcms.FirstOrDefault(
                                            tributacao => tributacao.TipoSaida == tipoSaida &&
                                            tributacao.TipoCliente == tipoCliente &&
                                            tributacao.EstadoDestino == ufDestino &&
                                            tributacao.CstCsosn == cstPesquisa &&
                                            tributacao.Cfop.Id == cfopPesquisa.Id);
            }

            if (tributacaoIcms == null)
            {
                return;
            }

            cstCsosn = tributacaoIcms.CstCsosn.GetValueOrDefault();
            cfop = tributacaoIcms.Cfop;

            if (cstCsosn == EnumCstCsosn.NORMAL00)
            {
                icmsBaseCalculo = tributacaoIcms.IcmsBaseCalculo;
                modalidadeBC = tributacaoIcms.ModalidadeBaseCalculo;
            }
            else if (cstCsosn == EnumCstCsosn.NORMAL10)
            {
                icmsBaseCalculo = tributacaoIcms.IcmsBaseCalculo;
                percentualMargemAdicST = tributacaoIcms.PercentualMargemAdicST;
                reducaoBaseST = tributacaoIcms.ReducaoBaseST;
                aliquotaIcmsST = tributacaoIcms.ReducaoBaseST;

                modalidadeST = tributacaoIcms.ModalidadeIcmsST;
            }
            else if (cstCsosn == EnumCstCsosn.NORMAL20 || cstCsosn == EnumCstCsosn.NORMAL51)
            {
                icmsReducaoBaseCalculo = tributacaoIcms.IcmsReducaoBaseCalculo;
                icmsBaseCalculo = tributacaoIcms.IcmsBaseCalculo;

                modalidadeBC = tributacaoIcms.ModalidadeBaseCalculo;
            }
            else if (cstCsosn == EnumCstCsosn.NORMAL30)
            {
                percentualMargemAdicST = tributacaoIcms.PercentualMargemAdicST;
                reducaoBaseST = tributacaoIcms.ReducaoBaseST;
                aliquotaIcmsST = tributacaoIcms.ReducaoBaseST;

                modalidadeST = tributacaoIcms.ModalidadeIcmsST;
            }
            else if (cstCsosn == EnumCstCsosn.NORMAL40 || cstCsosn == EnumCstCsosn.NORMAL41 || cstCsosn == EnumCstCsosn.NORMAL50)
            {
                modalidadeBC = tributacaoIcms.ModalidadeBaseCalculo;
            }
            else if (cstCsosn == EnumCstCsosn.NORMAL70 || cstCsosn == EnumCstCsosn.NORMAL90)
            {
                icmsBaseCalculo = tributacaoIcms.IcmsBaseCalculo;
                icmsReducaoBaseCalculo = tributacaoIcms.IcmsReducaoBaseCalculo;

                percentualMargemAdicST = tributacaoIcms.PercentualMargemAdicST;
                reducaoBaseST = tributacaoIcms.ReducaoBaseST;
                aliquotaIcmsST = tributacaoIcms.ReducaoBaseST;

                modalidadeST = tributacaoIcms.ModalidadeIcmsST;
                modalidadeBC = tributacaoIcms.ModalidadeBaseCalculo;
                if (produtoBase.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.TRIBUTADAPELOICMS)
                {
                    produtoBase.ContabilFiscal.SituacaoTributariaProduto = EnumSituacaoTributariaProduto.SUBSTITUICAOTRIBUTARIA;
                }
            }
        }


        public void DefinaCstIpiEAliquotas(Produto produto,
                                                                        EnumTipoSaidaTributacaoIcms tipoSaida,
                                                                        string ufDestino,
                                                                        EnumTipoCliente tipoCliente,
                                                                        ref EnumCstIpi? cstIpi, 
                                                                        ref double? aliquotaIpi)
        {
            var produtoBase = _repositorioProduto.ConsulteComJoinGrupoTributacaoETributacoes(produto.Id);

            var grupoTributacaoIpi = produtoBase.ContabilFiscal.GrupoTributacaoFederal;

            if (grupoTributacaoIpi == null)
            {
                grupoTributacaoIpi = RetorneGrupoTributacaoParametrosFederal(produto);

                if (grupoTributacaoIpi == null)
                {
                    return;
                }
            }

            var tributacaoIpi = grupoTributacaoIpi.ListaIpi.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida && tributacao.TipoCliente == tipoCliente && tributacao.EstadoDestino == ufDestino);

            if (tributacaoIpi == null)
            {
                tributacaoIpi = grupoTributacaoIpi.ListaIpi.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida && tributacao.TipoCliente == tipoCliente && tributacao.EstadoDestino == "IE");
            }

            if (cstIpi != null)
            {
                EnumCstIpi? cstPesquisa = cstIpi;
                
                tributacaoIpi = grupoTributacaoIpi.ListaIpi.FirstOrDefault(
                                            tributacao => tributacao.TipoSaida == tipoSaida &&
                                            tributacao.TipoCliente == tipoCliente &&
                                            tributacao.EstadoDestino == ufDestino &&
                                            tributacao.CstIpi == cstPesquisa);
            }

            if (tributacaoIpi == null)
            {
                return;
            }

            cstIpi = tributacaoIpi.CstIpi.GetValueOrDefault();
            aliquotaIpi = tributacaoIpi.AliquotaIpi;


        }

        public void DefinaCstPisEAliquotas(Produto produto,
                                                                        EnumTipoSaidaTributacaoIcms tipoSaida,
                                                                        string ufDestino,
                                                                        EnumTipoCliente tipoCliente,
                                                                        ref EnumCstPis? cstPis,
                                                                        ref double? aliquotaPercentual,
                                                                        ref double? aliquotaPercentualST,
                                                                        ref double? aliquotaReais,
                                                                        ref double? aliquotaReaisST)
        {
            var produtoBase = _repositorioProduto.ConsulteComJoinGrupoTributacaoETributacoes(produto.Id);

            var grupoTributacaoPis = produtoBase.ContabilFiscal.GrupoTributacaoFederal;

            if (grupoTributacaoPis == null)
            {
                grupoTributacaoPis = RetorneGrupoTributacaoParametrosFederal(produto);

                if (grupoTributacaoPis == null)
                {
                    return;
                }
            }

            var tributacaoPis = grupoTributacaoPis.ListaPis.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida && tributacao.TipoCliente == tipoCliente && tributacao.EstadoDestino == ufDestino);

            if (tributacaoPis == null)
            {
                tributacaoPis = grupoTributacaoPis.ListaPis.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida && tributacao.TipoCliente == tipoCliente && tributacao.EstadoDestino == "IE");
            }

            if (cstPis != null)
            {
                EnumCstPis? cstPesquisa = cstPis;

                tributacaoPis = grupoTributacaoPis.ListaPis.FirstOrDefault(
                                            tributacao => tributacao.TipoSaida == tipoSaida &&
                                            tributacao.TipoCliente == tipoCliente &&
                                            tributacao.EstadoDestino == ufDestino &&
                                            tributacao.CstPis == cstPesquisa);
            }

            if (tributacaoPis == null)
            {
                return;
            }

            cstPis = tributacaoPis.CstPis.GetValueOrDefault();
            aliquotaPercentual = tributacaoPis.AliquotaPercentual;
            aliquotaPercentualST = tributacaoPis.AliquotaPercentualST;
            aliquotaReais = tributacaoPis.AliquotaReais;
            aliquotaReaisST = tributacaoPis.AliquotaReaisST;

        }

        public void DefinaCstCofinsEAliquotas(Produto produto,
                                                                       EnumTipoSaidaTributacaoIcms tipoSaida,
                                                                       string ufDestino,
                                                                       EnumTipoCliente tipoCliente,
                                                                       ref EnumCstCofins? cstCofins,
                                                                       ref double? aliquotaPercentual,
                                                                       ref double? aliquotaPercentualST,
                                                                       ref double? aliquotaReais,
                                                                       ref double? aliquotaReaisST)
        {
            var produtoBase = _repositorioProduto.ConsulteComJoinGrupoTributacaoETributacoes(produto.Id);

            var grupoTributacaoCofins = produtoBase.ContabilFiscal.GrupoTributacaoFederal;

            if (grupoTributacaoCofins == null)
            {
                grupoTributacaoCofins = RetorneGrupoTributacaoParametrosFederal(produto);

                if (grupoTributacaoCofins == null)
                {
                    return;
                }
            }

            var tributacaoCofins = grupoTributacaoCofins.ListaCofins.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida && tributacao.TipoCliente == tipoCliente && tributacao.EstadoDestino == ufDestino);

            if (tributacaoCofins == null)
            {
                tributacaoCofins = grupoTributacaoCofins.ListaCofins.FirstOrDefault(tributacao => tributacao.TipoSaida == tipoSaida && tributacao.TipoCliente == tipoCliente && tributacao.EstadoDestino == "IE");
            }

            if (cstCofins != null)
            {
                EnumCstCofins? cstPesquisa = cstCofins;

                tributacaoCofins = grupoTributacaoCofins.ListaCofins.FirstOrDefault(
                                            tributacao => tributacao.TipoSaida == tipoSaida &&
                                            tributacao.TipoCliente == tipoCliente &&
                                            tributacao.EstadoDestino == ufDestino &&
                                            tributacao.CstCofins == cstPesquisa);
            }

            if (tributacaoCofins == null)
            {
                return;
            }

            cstCofins = tributacaoCofins.CstCofins.GetValueOrDefault();
            aliquotaPercentual = tributacaoCofins.AliquotaPercentual;
            aliquotaPercentualST = tributacaoCofins.AliquotaPercentualST;
            aliquotaReais = tributacaoCofins.AliquotaReais;
            aliquotaReaisST = tributacaoCofins.AliquotaReaisST;

        }

        public void CalculeValorIcmsRegimeNormal(Produto produto, double valorUnitario,
                                                       double quantidade,
                                                       double? totalDesconto,
                                                       double? valorFrete,
                                                       double? valorIpi,
                                                       EnumCstCsosn? cstCsosn,
                                                       double? percentualMargemAdicST,
                                                       double? aliquotaIcmsST,
                                                       double? reducaoBaseST,
                                                       double? icmsBaseCalculo,
                                                       double? icmsReducaoBaseCalculo,
                                                       ref double? baseDeCalculoIcmsST,
                                                       ref double? valorIcmsST,
                                                       ref double? baseDeCalculoIcms,
                                                       ref double? valorIcms,
                                                       double? valorSeguro = 0,
                                                       double? valorOutrasDespesas = 0, int? Cfopprod = 0
                                                       )
        {
            var produtoBase = _repositorioProduto.ConsulteComJoinGrupoTributacaoETributacoes(produto.Id);

            if (cstCsosn == EnumCstCsosn.NORMAL40 || cstCsosn == EnumCstCsosn.NORMAL41 || cstCsosn == EnumCstCsosn.NORMAL50 || cstCsosn == EnumCstCsosn.NORMAL60)
            {
                return;
            }
            else if (cstCsosn == EnumCstCsosn.NORMAL00)
            {
                //if (Cfopprod == 6107)
                //{
                //    baseDeCalculoIcms = (valorUnitario * quantidade - totalDesconto.GetValueOrDefault()) + valorIpi.GetValueOrDefault() + valorFrete.GetValueOrDefault() + valorSeguro.GetValueOrDefault() + valorOutrasDespesas.GetValueOrDefault();
                //    valorIcms = baseDeCalculoIcms * (icmsBaseCalculo.GetValueOrDefault() / (double)100);
                //    baseDeCalculoIcms = baseDeCalculoIcms + valorIcms;
                //}
                //else
                {
                    baseDeCalculoIcms = (valorUnitario * quantidade - totalDesconto.GetValueOrDefault()) + valorIpi.GetValueOrDefault() + valorFrete.GetValueOrDefault() + valorSeguro.GetValueOrDefault() + valorOutrasDespesas.GetValueOrDefault();
                    valorIcms = baseDeCalculoIcms * (icmsBaseCalculo.GetValueOrDefault() / (double)100);
                }
             
            }
            else if (cstCsosn == EnumCstCsosn.NORMAL20 || cstCsosn == EnumCstCsosn.NORMAL51)
            {
                double TotalProdutos = (valorUnitario * quantidade - totalDesconto.GetValueOrDefault()) + valorIpi.GetValueOrDefault() + valorFrete.GetValueOrDefault() + valorSeguro.GetValueOrDefault() + valorOutrasDespesas.GetValueOrDefault();
                //*****************************ICMS***************************************************************************
                //***********Sem Redução
                //Cálculo ICMS sem Redução
                if (icmsReducaoBaseCalculo == 0 || icmsReducaoBaseCalculo == null)
                {
                    baseDeCalculoIcms = TotalProdutos;

                    valorIcms = baseDeCalculoIcms * icmsBaseCalculo / (double)100;
                }
                //**********Redução
                //**Cálculo ICMS com redução
                else
                {
                    baseDeCalculoIcms = (TotalProdutos * (icmsReducaoBaseCalculo.GetValueOrDefault() / (double)100));

                    valorIcms = baseDeCalculoIcms * icmsBaseCalculo / (double)100;
                }
            }
            else if (cstCsosn == EnumCstCsosn.NORMAL30 &&  produtoBase.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.SUBSTITUICAOTRIBUTARIA)
            {
                double TotalProdutos = (valorUnitario * quantidade - totalDesconto.GetValueOrDefault()) + valorIpi.GetValueOrDefault() + valorFrete.GetValueOrDefault() + valorSeguro.GetValueOrDefault() + valorOutrasDespesas.GetValueOrDefault();
                //************************************ST************************************************************************
                //***********Sem Redução e Sem MVA                   
                if (reducaoBaseST == 0 || reducaoBaseST == null)
                {
                    if (percentualMargemAdicST == 0 || percentualMargemAdicST == null)
                    {
                        baseDeCalculoIcmsST = TotalProdutos;

                        valorIcmsST = (baseDeCalculoIcmsST * aliquotaIcmsST / (double)100) - valorIcms;
                    }
                    //********Sem Redução e Com MVA***
                    else
                    {
                        baseDeCalculoIcmsST = ((TotalProdutos * percentualMargemAdicST.GetValueOrDefault() / (double)100) + TotalProdutos);

                        valorIcmsST = (baseDeCalculoIcmsST * aliquotaIcmsST / (double)100) - valorIcms;
                    }
                }
                else
                {
                    //***************Redução e Sem MVA***                        
                    if (percentualMargemAdicST == 0 || percentualMargemAdicST == null)
                    {
                        baseDeCalculoIcmsST = TotalProdutos * reducaoBaseST.GetValueOrDefault() / (double)100;

                        valorIcmsST = (baseDeCalculoIcmsST * aliquotaIcmsST / (double)100) - valorIcms;
                    }
                    //**Redução e com MVA
                    else
                    {
                        baseDeCalculoIcmsST = ((TotalProdutos * percentualMargemAdicST.GetValueOrDefault() / (double)100) + TotalProdutos) * reducaoBaseST.GetValueOrDefault() / (double)100;

                        valorIcmsST = (baseDeCalculoIcmsST * aliquotaIcmsST / (double)100) - valorIcms;
                    }
                }
            }
            else if (cstCsosn == EnumCstCsosn.NORMAL70 || cstCsosn == EnumCstCsosn.NORMAL90 || cstCsosn == EnumCstCsosn.NORMAL10)
            {
                double TotalProdutos = (valorUnitario * quantidade - totalDesconto.GetValueOrDefault()) + valorIpi.GetValueOrDefault() + valorFrete.GetValueOrDefault() + valorSeguro.GetValueOrDefault() + valorOutrasDespesas.GetValueOrDefault();
                //*****************************ICMS***************************************************************************
                //***********Sem Redução
                    //Cálculo ICMS sem Redução
                if (icmsReducaoBaseCalculo ==0 || icmsReducaoBaseCalculo == null)
                {
                    baseDeCalculoIcms = TotalProdutos;

                    valorIcms = baseDeCalculoIcms * icmsBaseCalculo / (double)100;
                }
                //**********Redução
                    //**Cálculo ICMS com redução
                else
                {
                    baseDeCalculoIcms = (TotalProdutos * (icmsReducaoBaseCalculo.GetValueOrDefault() / (double)100));

                    valorIcms = baseDeCalculoIcms * icmsBaseCalculo / (double)100;
                }
                //************************************ST************************************************************************
                if (produtoBase.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.SUBSTITUICAOTRIBUTARIA)
                {
                    //***********Sem Redução e Sem MVA                   
                    if (reducaoBaseST == 0 || reducaoBaseST == null)
                    {
                        if (percentualMargemAdicST == 0 || percentualMargemAdicST == null)
                        {
                            baseDeCalculoIcmsST = TotalProdutos;

                            valorIcmsST = (baseDeCalculoIcmsST * icmsBaseCalculo / (double)100) - valorIcms;
                        }
                        //********Sem Redução e Com MVA***
                        else
                        {
                            baseDeCalculoIcmsST = ((TotalProdutos * percentualMargemAdicST.GetValueOrDefault() / (double)100) + TotalProdutos);

                            valorIcmsST = (baseDeCalculoIcmsST * icmsBaseCalculo / (double)100) - valorIcms;
                        }
                    }
                    else
                    {
                        //***************Redução e Sem MVA***                        
                        if (percentualMargemAdicST == 0 || percentualMargemAdicST == null)
                        {
                            baseDeCalculoIcmsST = TotalProdutos * reducaoBaseST.GetValueOrDefault() / (double)100;

                            valorIcmsST = (baseDeCalculoIcmsST * aliquotaIcmsST / (double)100) - valorIcms;
                        }
                        //**Redução e com MVA
                        else
                        {
                            baseDeCalculoIcmsST = ((TotalProdutos * percentualMargemAdicST.GetValueOrDefault() / (double)100) + TotalProdutos) * reducaoBaseST.GetValueOrDefault() / (double)100;

                            valorIcmsST = (baseDeCalculoIcmsST * aliquotaIcmsST / (double)100) - valorIcms;
                        }
                    }
                }
            }
        }

        //IPI
        public double CalculeValorIpi(double valorUnitario, double quantidade, double valorTotalDesconto, double valorFrete,
                     double valorSeguro, double valorOutrasDespesas, double percentualIpi, ref double baseCalculoIpi)
        {
            double totalSemDesconto = valorUnitario * quantidade;
            baseCalculoIpi = totalSemDesconto - valorTotalDesconto + valorFrete + valorSeguro + valorOutrasDespesas;

            double valorIpi = (baseCalculoIpi * percentualIpi) / 100;

            return valorIpi;
        }

        //PIS
        public double CalculeValorPis(double valorUnitario, double quantidade, double valorTotalDesconto, double valorFrete,
                     double valorSeguro, double valorOutrasDespesas, ref double baseCalculoPis, double aliquotaPercentualPis = 0, double aliquotaReaisPis = 0)
        {
            double valorPis = 0;
            if (aliquotaPercentualPis != 0)
            {
                double totalSemDesconto = valorUnitario * quantidade;
                baseCalculoPis = totalSemDesconto - valorTotalDesconto + valorFrete + valorSeguro + valorOutrasDespesas;

                valorPis = (baseCalculoPis * aliquotaPercentualPis) / 100;
            }
            else if (aliquotaReaisPis != 0)
            {
                valorPis = (quantidade * aliquotaReaisPis);
            }

            return valorPis;
        }

        //COFINS
        public double CalculeValorCofins(double valorUnitario, double quantidade, double valorTotalDesconto, double valorFrete,
                     double valorSeguro, double valorOutrasDespesas, ref double baseCalculoCofins, double aliquotaPercentualCofins = 0, double aliquotaReaisCofins = 0)
        {
            double valorCofins = 0;

            if (aliquotaPercentualCofins != 0)
            {
                double totalSemDesconto = valorUnitario * quantidade;
                baseCalculoCofins = totalSemDesconto - valorTotalDesconto + valorFrete + valorSeguro + valorOutrasDespesas;

                valorCofins = Math.Round((baseCalculoCofins * aliquotaPercentualCofins) / 100,3);
            }
            else if (aliquotaReaisCofins != 0)
            {
                valorCofins = (quantidade * aliquotaReaisCofins);
            }

            return valorCofins;
        }

        #endregion

    }
}
