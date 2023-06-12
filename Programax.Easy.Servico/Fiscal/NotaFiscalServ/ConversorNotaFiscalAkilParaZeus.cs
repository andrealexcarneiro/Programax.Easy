using System;
using System.Collections.Generic;
using System.Linq;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using NFe.Classes.Informacoes;
using NFe.Classes.Informacoes.Identificacao;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using Programax.Infraestrutura.Negocio.Utils;
using NFe.Classes.Informacoes.Emitente;
using NFe.Classes.Servicos.Tipos;
using NFe.Classes.Informacoes.Destinatario;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using NFe.Classes.Informacoes.Detalhe;
using NFe.Classes.Informacoes.Detalhe.Tributacao;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Cobranca;
using NFe.Classes.Informacoes.Transporte;
using NFe.Classes.Informacoes.Observacoes;
using NFe.Classes.Informacoes.Total;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using NFe.Classes;
using NFe.Classes.Protocolo;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ConfiguracoesPdvServ;
using NFe.Classes.Informacoes.Pagamento;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using DFe.Classes.Flags;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using System.ComponentModel;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ
{
    public class ConversorNotaFiscalAkilParaZeus
    {
    
    public NFe.Classes.NFe ConvertaNotaAkilParaZeus(NotaFiscal notaFiscal)
        {
            NFe.Classes.NFe notaFiscalZeus = new NFe.Classes.NFe();

            infNFe informacoesNotaFiscalZeus = new infNFe();

            ConvertaIdentificacaoNotaAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
            ConvertaEmitenteNotaAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
            
            //Vai entrar aqui caso houver destinatário
            if (notaFiscal.Destinatario.CnpjCpf != null || notaFiscal.Destinatario.ParceiroResideExterior)
                ConvertaDestinatarioNotaAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);

            ConvertaProdutosAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
            //ConvertaDuplicatasAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
            ConvertaTransporteAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
            ConvertaVolumeAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
            ConvertaInformacoesAdicionaisAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
            ConvertaTotaisAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
            ConvertaInformacoesComercioExteriorAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
            ConvertaInformacoesCompraAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);

            //Vai entrar aqui caso houver destinatário
            if (notaFiscal.Destinatario.CnpjCpf != null || notaFiscal.Destinatario.ParceiroResideExterior)
            {
                ConvertaLocalRetiradaAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
                ConvertaLocalEntregaAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);
            }
            //ConvertaFormasPagamentoAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus); -> NF Versão 3.10

            ConvertaPagamentoAkilNFeParaZeus(notaFiscal, informacoesNotaFiscalZeus);

            ConvertaNFReferenciadasAkilParaZeus(notaFiscal, informacoesNotaFiscalZeus);

            if (notaFiscal.Destinatario.TipoPessoa == EnumTipoPessoa.PESSOAFISICA)
            {
                informacoesNotaFiscalZeus.exporta = informacoesNotaFiscalZeus.exporta != null? informacoesNotaFiscalZeus.exporta:null;
                informacoesNotaFiscalZeus.compra = null;
                informacoesNotaFiscalZeus.cana = null;
            }

            notaFiscalZeus.infNFe = informacoesNotaFiscalZeus;

            ConvertaInformacoesSuplementaresAkilParaZeus(notaFiscal, notaFiscalZeus);
            
            return notaFiscalZeus;

        }

        private void ConvertaNFReferenciadasAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {   
            for (int i = 0; i < notaFiscal.ListaNotasReferenciadas.Count; i++)
            {   
                NFref itemZeus = new NFref();

                if (notaFiscal.ListaNotasReferenciadas[i].TipoNotaReferenciada == EnumTipoNotaReferenciada.NFEOUNFCE)
                {
                    itemZeus.refNFe = notaFiscal.ListaNotasReferenciadas[i].ChaveDeAcesso;                    
                }
                else if(notaFiscal.ListaNotasReferenciadas[i].TipoNotaReferenciada == EnumTipoNotaReferenciada.NOTAFISCAL1A)
                {
                    itemZeus.refNF.cUF = (DFe.Classes.Entidades.Estado)notaFiscal.ListaNotasReferenciadas[i].CodigoUF;
                    itemZeus.refNF.AAMM = notaFiscal.ListaNotasReferenciadas[i].AnoMesEmissao;
                    itemZeus.refNF.CNPJ = notaFiscal.ListaNotasReferenciadas[i].CnpjEmitente;
                    itemZeus.refNF.serie = notaFiscal.ListaNotasReferenciadas[i].SerieNota.ToInt();
                    itemZeus.refNF.nNF = notaFiscal.ListaNotasReferenciadas[i].NumeroNota.ToInt();               
                }
                else if(notaFiscal.ListaNotasReferenciadas[i].TipoNotaReferenciada == EnumTipoNotaReferenciada.NOTAFISCALPRODUTORRURAL)
                {
                    itemZeus.refNFP.cUF = (DFe.Classes.Entidades.Estado)notaFiscal.ListaNotasReferenciadas[i].CodigoUF;
                    itemZeus.refNFP.AAMM = notaFiscal.ListaNotasReferenciadas[i].AnoMesEmissao;
                    itemZeus.refNFP.CNPJ = notaFiscal.ListaNotasReferenciadas[i].CnpjEmitente;
                    itemZeus.refNFP.serie = notaFiscal.ListaNotasReferenciadas[i].SerieNota.ToInt();
                    itemZeus.refNFP.nNF = notaFiscal.ListaNotasReferenciadas[i].NumeroNota.ToInt();
                    itemZeus.refNFP.IE = notaFiscal.ListaNotasReferenciadas[i].InscricaoEstadual.RemovaEspacosEmBrancoDoInicioEFim().RemoverCaracteresDeMascara();                    
                    itemZeus.refNFP.mod = ((EnumModeloNotaFiscalReferenciada)notaFiscal.ListaNotasReferenciadas[i].ModeloDocumento).ToString();
                }
                else
                {
                    itemZeus.refECF.nECF = notaFiscal.ListaNotasReferenciadas[i].NumeroEcf.ToInt();
                    itemZeus.refECF.nCOO = notaFiscal.ListaNotasReferenciadas[i].Coo.ToInt();
                    itemZeus.refECF.mod = ((EnumModeloNotaFiscalReferenciada)notaFiscal.ListaNotasReferenciadas[i].ModeloDocumento).ToString();
                }


                informacoesNotaFiscalZeus.ide.NFref = informacoesNotaFiscalZeus.ide.NFref ?? new List<NFref>();
                informacoesNotaFiscalZeus.ide.NFref.Add(itemZeus);

            }
        }


        public nfeProc ConvertaNotaAutorizadaAkilParaZeus(NotaFiscal notaFiscal)
        {   
            nfeProc nfeProcessadaZeus = new nfeProc();

            nfeProcessadaZeus.NFe = ConvertaNotaAkilParaZeus(notaFiscal);           
            nfeProcessadaZeus.protNFe = ConvertaInformacoesProtocoloAutorizacaoNotaFiscalAkilParaZeus(notaFiscal);
            nfeProcessadaZeus.versao = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoNota;

            return nfeProcessadaZeus;
        }

        private protNFe ConvertaInformacoesProtocoloAutorizacaoNotaFiscalAkilParaZeus(NotaFiscal notaFiscal)
        {
            protNFe protocoloNfeZeus = new protNFe();
            protocoloNfeZeus.infProt = new infProt();

            protocoloNfeZeus.versao = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoNota;
            protocoloNfeZeus.infProt.chNFe = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.ChaveNfe;
            protocoloNfeZeus.infProt.cStat = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.Status;
            protocoloNfeZeus.infProt.dhRecbto = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.DataHoraRecibo;
            protocoloNfeZeus.infProt.digVal = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.DigestValue;
            protocoloNfeZeus.infProt.nProt = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.NumeroProtocolo;
            protocoloNfeZeus.infProt.tpAmb = (TipoAmbiente)notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.TipoAmbiente;
            protocoloNfeZeus.infProt.verAplic = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.VersaoAplicativo;
            protocoloNfeZeus.infProt.xMotivo = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.Motivo;
            protocoloNfeZeus.infProt.Id = notaFiscal.InformacoesProtocoloAutorizacaoNotaFiscal.Id;

            return protocoloNfeZeus;
        }

        #region " CONVERTA NOTA AKIL PARA ZEUS "

        private void ConvertaIdentificacaoNotaAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            informacoesNotaFiscalZeus.versao = "4.00";

            var identificacaoNota = new ide();

            ServicoCidade servicoCidade = new ServicoCidade(false, false);
            var cidade = servicoCidade.Consulte(notaFiscal.IdentificacaoNotaFiscal.Cidade.Id);

            identificacaoNota.cDV = notaFiscal.IdentificacaoNotaFiscal.DigitoVerificadorChaveAcesso;
            identificacaoNota.cMunFG = cidade.CodigoIbge.ToLong();
            identificacaoNota.cNF = notaFiscal.IdentificacaoNotaFiscal.NumeroNota + 1.ToString();
            identificacaoNota.cUF = (DFe.Classes.Entidades.Estado)cidade.Estado.CodigoEstado;
            identificacaoNota.dhEmi = notaFiscal.IdentificacaoNotaFiscal.DataHoraEmissao.ToString("yyyy-MM-ddTHH:mm:sszzz").ToDate();

            identificacaoNota.dhSaiEnt = notaFiscal.IdentificacaoNotaFiscal.DataHoraSaida != null ? notaFiscal.IdentificacaoNotaFiscal.DataHoraSaida.Value.ToString("yyyy-MM-ddTHH:mm:sszzz").ToDate():default(DateTime);
            identificacaoNota.dhSaiEnt = notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 65? null: identificacaoNota.dhSaiEnt; //NFCe não tem data de entrada/saída
            
            identificacaoNota.finNFe = (FinalidadeNFe)notaFiscal.IdentificacaoNotaFiscal.FinalidadeEmissaoNFe;
            //identificacaoNota.idDest = (DestinoOperacao)notaFiscal.IdentificacaoNotaFiscal.IdentificacaoOperacaoNotaFiscal;
            identificacaoNota.idDest = (DestinoOperacao)notaFiscal.IdentificacaoNotaFiscal.IdentificacaoOperacaoNotaFiscal;




            identificacaoNota.indFinal = notaFiscal.IdentificacaoNotaFiscal.ConsumidorFinal ? ConsumidorFinal.cfConsumidorFinal : ConsumidorFinal.cfNao;
            //identificacaoNota.indPag = (IndicadorPagamento)notaFiscal.IdentificacaoNotaFiscal.FormaPagamento; -> NF Versão 3.10
            identificacaoNota.indPres = (PresencaComprador)notaFiscal.IdentificacaoNotaFiscal.IndicacaoPresenca;
            identificacaoNota.mod = (DFe.Classes.Flags.ModeloDocumento)notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal;
            identificacaoNota.natOp = notaFiscal.IdentificacaoNotaFiscal.DescricaoNaturezaOperacao.RemovaEspacosEmBrancoDoInicioEFim();
            //identificacaoNota.NFref = notaFiscal.ListaNotasReferenciadas != null? (List<NFref>)notaFiscal.ListaNotasReferenciadas: null;
            identificacaoNota.nNF = notaFiscal.IdentificacaoNotaFiscal.NumeroNota;
            identificacaoNota.procEmi = (ProcessoEmissao)notaFiscal.IdentificacaoNotaFiscal.ProcessoEmissaoNfe;
            identificacaoNota.serie = notaFiscal.IdentificacaoNotaFiscal.Serie;
            identificacaoNota.tpAmb = (TipoAmbiente)notaFiscal.IdentificacaoNotaFiscal.TipoAmbiente;
            identificacaoNota.tpEmis = (TipoEmissao)notaFiscal.IdentificacaoNotaFiscal.TipoEmissaoDanfe;
            identificacaoNota.tpImp = (TipoImpressao)notaFiscal.IdentificacaoNotaFiscal.FormatoImpressaoDanfe;
            identificacaoNota.tpNF = notaFiscal.IdentificacaoNotaFiscal.NotaSaida ? TipoNFe.tnSaida : TipoNFe.tnEntrada;
            identificacaoNota.verProc = notaFiscal.IdentificacaoNotaFiscal.VersaoAplicativo;

            //Emissão no modo de contigência
            if ((TipoEmissao)notaFiscal.IdentificacaoNotaFiscal.TipoEmissaoDanfe != TipoEmissao.teNormal)
            {
                identificacaoNota.dhCont = notaFiscal.IdentificacaoNotaFiscal.DataHoraEmissao.ToString("yyyy-MM-ddTHH:mm:sszzz").ToDate();
                identificacaoNota.xJust = "PROBLEMAS DE CONEXAO";
            }

            informacoesNotaFiscalZeus.ide = identificacaoNota;
        }

        private void ConvertaEmitenteNotaAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            var emitente = new emit();

            //emitente.CNAE = notaFiscal.Emitente.CNAE;
            emitente.CNPJ = notaFiscal.Emitente.CNPJ.RemoverCaracteresDeMascara().RemovaEspacosEmBrancoDoInicioEFim();
            emitente.CRT = (CRT)notaFiscal.Emitente.CRT;
            emitente.xNome = notaFiscal.Emitente.RazaoSocial.RemovaEspacosEmBrancoDoInicioEFim();
            emitente.xFant = notaFiscal.Emitente.NomeFantasia.RemovaEspacosEmBrancoDoInicioEFim();

            emitente.enderEmit = new enderEmit();

            emitente.enderEmit.xLgr = notaFiscal.Emitente.Logradouro.RemovaEspacosEmBrancoDoInicioEFim();
            emitente.enderEmit.nro = notaFiscal.Emitente.Numero.RemovaEspacosEmBrancoDoInicioEFim();
            emitente.enderEmit.xCpl = notaFiscal.Emitente.Complemento.RemovaEspacosEmBrancoDoInicioEFim();
            emitente.enderEmit.xBairro = notaFiscal.Emitente.Bairro.RemovaEspacosEmBrancoDoInicioEFim();
            emitente.enderEmit.cMun = notaFiscal.Emitente.CodigoMunicipio;
            emitente.enderEmit.xMun = notaFiscal.Emitente.NomeMunicipio.RemovaEspacosEmBrancoDoInicioEFim();

            //emitente.enderEmit.UF = (DFe.Classes.Entidades.Estado)notaFiscal.Emitente.CodigoDoEstado;
            //----> Substituído por --> Atualização do Zeus em Setembro / 2021
            //Busca o código do estado do emitente
            emitente.enderEmit.UF = (DFe.Classes.Entidades.Estado)new ServicoEmpresa().ConsulteUltimaEmpresa().DadosEmpresa.Endereco.Cidade.Estado.CodigoEstado;

            emitente.enderEmit.CEP = notaFiscal.Emitente.Cep.ToStringEmpty().RemovaEspacosEmBrancoDoInicioEFim();
            emitente.enderEmit.cPais = notaFiscal.Emitente.CodigoPais;
            emitente.enderEmit.xPais = notaFiscal.Emitente.NomePais.RemovaEspacosEmBrancoDoInicioEFim();
            emitente.enderEmit.fone = notaFiscal.Emitente.Telefone;
            
            emitente.IE = notaFiscal.Emitente.InscricaoEstadual.RemovaEspacosEmBrancoDoInicioEFim().RemoverCaracteresDeMascara();
            
            //emitente.IEST
            //emitente.IM = notaFiscal.Emitente.InscricaoMunicipal;

            informacoesNotaFiscalZeus.emit = emitente;
        }

        private void ConvertaDestinatarioNotaAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            if (notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 65)
            {
                ServicoConfiguracoesPdv servicoConfiguracoesPdv = new ServicoConfiguracoesPdv(false,false);
                var configuracaoPdv = servicoConfiguracoesPdv.ConsulteUltimaConfiguracaoPdv();

                if (configuracaoPdv.Cliente != null && configuracaoPdv.Cliente.Id == notaFiscal.Destinatario.Pessoa.Id)
                {
                    if (notaFiscal.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.AUTORIZADA)
                        return;

                    if (configuracaoPdv.ClienteTemporario == null)
                        throw new Exception("Para continuar. Você deverá cadastrar o cliente temporário. Primeiro, no cadastro de Parceiros. " +
                                            "Segundo, informar em Adm --> Configurações PDV. Qualquer dúvida contate suporte.");

                        if (configuracaoPdv.ClienteTemporario.Id.ToInt() != 0)
                            notaFiscal.Destinatario.Pessoa.Id = configuracaoPdv.ClienteTemporario.Id;
                        else
                            return;
                }
            }

            var destinatario = new dest(VersaoServico.Versao400);

            if (!notaFiscal.Destinatario.ParceiroResideExterior)
            {
                if (notaFiscal.Destinatario.EhPessoaFisica)
                {
                    destinatario.CPF = notaFiscal.Destinatario.CnpjCpf.RemoverCaracteresDeMascara();
                }
                else
                {
                    destinatario.CNPJ = notaFiscal.Destinatario.CnpjCpf.RemoverCaracteresDeMascara();
                }
            }

            destinatario.xNome = notaFiscal.Destinatario.RazaoSocialOuNomeDestinatario.RemovaEspacosEmBrancoDoInicioEFim();

            destinatario.indIEDest = notaFiscal.Destinatario.IndicadorIEDestinatario == EnumIndicadorIEDestinatario.CONTRIBUINTEICMS ? indIEDest.ContribuinteICMS :
                                                notaFiscal.Destinatario.IndicadorIEDestinatario == EnumIndicadorIEDestinatario.ISENTO ? indIEDest.Isento : indIEDest.NaoContribuinte;

            destinatario.email = notaFiscal.Destinatario.Email.RemovaEspacosEmBrancoDoInicioEFim();
            destinatario.IE = notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 55? notaFiscal.Destinatario.InscricaoEstadual.RemovaEspacosEmBrancoDoInicioEFim().RemoverCaracteresDeMascara():string.Empty;

            destinatario.ISUF = notaFiscal.Destinatario.InscricaoSuframa.RemovaEspacosEmBrancoDoInicioEFim();

            destinatario.idEstrangeiro = notaFiscal.Destinatario.IdEstrangeiro.RemovaEspacosEmBrancoDoInicioEFim();

            if (notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 55 ||
                (notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 65)) //&&
                 //notaFiscal.IdentificacaoNotaFiscal.IndicacaoPresenca == EnumIndicacaoPresenca.NFCEEMOPERACAOCOMENTREGAEMDOMICILIO))
            {
                destinatario.enderDest = new enderDest();

                if (notaFiscal.Destinatario.Cep != null)
                {
                    destinatario.enderDest.CEP = notaFiscal.Destinatario.Cep.ToStringEmpty().RemoverCaracteresDeMascara();
                }
                
                destinatario.enderDest.cMun = notaFiscal.Destinatario.CodigoMunicipio;
                destinatario.enderDest.cPais = notaFiscal.Destinatario.CodigoPais;
                destinatario.enderDest.nro = notaFiscal.Destinatario.Numero.RemovaEspacosEmBrancoDoInicioEFim();
                destinatario.enderDest.UF = notaFiscal.Destinatario.UF.RemovaEspacosEmBrancoDoInicioEFim();
                destinatario.enderDest.xBairro = notaFiscal.Destinatario.Bairro.RemovaEspacosEmBrancoDoInicioEFim();
                destinatario.enderDest.xCpl = notaFiscal.Destinatario.Complemento.RemovaEspacosEmBrancoDoInicioEFim();
                destinatario.enderDest.xLgr = notaFiscal.Destinatario.Logradouro.RemovaEspacosEmBrancoDoInicioEFim();
                destinatario.enderDest.xMun = notaFiscal.Destinatario.NomeMunicipio.RemovaEspacosEmBrancoDoInicioEFim();
                destinatario.enderDest.xPais = notaFiscal.Destinatario.NomePais;
                destinatario.enderDest.fone = notaFiscal.Destinatario.Telefone;
            }

            informacoesNotaFiscalZeus.dest = destinatario;
        }

        private void ConvertaProdutosAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            for (int i = 0; i < notaFiscal.ListaItens.Count; i++)
            {
                var item = notaFiscal.ListaItens[i];

                det itemZeus = new det();

                itemZeus.nItem = i + 1;
                itemZeus.prod = new prod();
                itemZeus.prod.CFOP = item.Cfop;
                itemZeus.prod.cProd = item.Produto.Id.ToString();
                //itemZeus.prod.detExport
                //itemZeus.prod.DI
                //itemZeus.prod.EXTIPI
                //itemZeus.prod.indTot
                itemZeus.prod.indTot = IndicadorTotal.ValorDoItemCompoeTotalNF;
                itemZeus.prod.NCM = item.Ncm;
                itemZeus.prod.CEST = item.Cest != null ? item.Cest.RemovaEspacosEmBrancoDoInicioEFim().RemoverCaracteresDeMascara() : null;
                //itemZeus.prod.nFCI
                //itemZeus.prod.nItemPed
                //itemZeus.prod.nRECOPI
                //itemZeus.prod.NVE
                //itemZeus.prod.ProdutoEspecifico
                itemZeus.prod.qCom = (decimal)item.Quantidade;
                itemZeus.prod.qTrib = (decimal)item.Quantidade;
                itemZeus.prod.uCom = item.UnidadeProduto.RemovaEspacosEmBrancoDoInicioEFim();
                itemZeus.prod.uTrib = item.UnidadeProduto.RemovaEspacosEmBrancoDoInicioEFim();
                itemZeus.prod.vDesc = (decimal?)item.ValorDesconto;
                itemZeus.prod.vFrete = (decimal?)item.ValorFrete;
                itemZeus.prod.vProd = (decimal)item.ValorBruto;
                itemZeus.prod.vSeg = (decimal?)item.Seguro;
                itemZeus.prod.vOutro = (decimal?)item.OutrasDespesas;
                itemZeus.prod.vUnCom = (decimal)item.ValorUnitario;
                itemZeus.prod.vUnTrib = (decimal)item.ValorUnitario;
                itemZeus.prod.xProd = item.NomeProduto.RemovaEspacosEmBrancoDoInicioEFim();
                itemZeus.prod.cEAN = item.CodigoGtinProduto;
                itemZeus.prod.cEANTrib = item.CodigoGtinProduto;

                itemZeus.imposto = new imposto();
                itemZeus.imposto.ICMS = new ICMS();

                //if (notaFiscal.IdentificacaoNotaFiscal.FinalidadeEmissaoNFe == EnumFinalidadeEmissaoNfe.NFEDEVOLUCAO && item.Impostos.Ipi !=null)
                //{
                //    itemZeus.impostoDevol = new impostoDevol();
                //    itemZeus.impostoDevol.IPI = new IPIDevolvido();
                //    itemZeus.impostoDevol.IPI.vIPIDevol = (decimal)item.Impostos.Ipi.ValorIpi;
                //}
                //else
                //{
                //itemZeus.imposto.IPI = new IPI();
                //}

                itemZeus.imposto.IPI = new IPI();

                itemZeus.imposto.COFINS = new COFINS();
                itemZeus.imposto.PIS = new PIS();

                itemZeus.imposto.ICMS.TipoICMS = RetorneIcmsZeus(item, notaFiscal);
               
                itemZeus.imposto.ICMSUFDest = RetorneIcmsInterestadual(item);

                //IPI
                if (item.Impostos.Ipi != null && notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal != 65)
                {
                    //if (notaFiscal.IdentificacaoNotaFiscal.FinalidadeEmissaoNFe != EnumFinalidadeEmissaoNfe.NFEDEVOLUCAO)
                    
                        itemZeus.imposto.IPI.cEnq = (int)item.Impostos.Ipi.CodigoEnquadramento != 0? item.Impostos.Ipi.CodigoEnquadramento:999;

                        itemZeus.imposto.IPI.TipoIPI = RetorneIpiZeusIPITrib(item);

                        if (itemZeus.imposto.IPI.TipoIPI == null)
                            itemZeus.imposto.IPI.TipoIPI = RetorneIpiZeusIPINT(item);                   
                }
                else 
                {
                    itemZeus.imposto.IPI = RetorneIpiZeus(item);
                } 
                
                //COFINS
                itemZeus.imposto.COFINS = RetorneCofinsZeus(notaFiscal, item);
                //PIS
                itemZeus.imposto.PIS = RetornePisZeus(notaFiscal, item);

                itemZeus.imposto.vTotTrib = (decimal)item.Impostos.TotalTributacao;

                informacoesNotaFiscalZeus.det.Add(itemZeus);
            }
        }

        private void ConvertaDuplicatasAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            if (notaFiscal.DadosCobranca != null && (notaFiscal.DadosCobranca.ListaDuplicatas.Count > 0 || notaFiscal.DadosCobranca.FaturaNotaFiscal != null))
            {
                informacoesNotaFiscalZeus.cobr = new cobr();

                if (notaFiscal.DadosCobranca.FaturaNotaFiscal != null)
                {
                    informacoesNotaFiscalZeus.cobr.fat = new fat();
                    informacoesNotaFiscalZeus.cobr.fat.nFat = notaFiscal.DadosCobranca.FaturaNotaFiscal.NumeroFatura;
                    informacoesNotaFiscalZeus.cobr.fat.vDesc = (decimal?)notaFiscal.DadosCobranca.FaturaNotaFiscal.ValorDesconto;
                    informacoesNotaFiscalZeus.cobr.fat.vLiq = (decimal?)notaFiscal.DadosCobranca.FaturaNotaFiscal.ValorLiquido;
                    informacoesNotaFiscalZeus.cobr.fat.vOrig = (decimal?)notaFiscal.DadosCobranca.FaturaNotaFiscal.ValorOriginalFatura;
                }

                for (int i = 0; i < notaFiscal.DadosCobranca.ListaDuplicatas.Count; i++)
                {
                    if (informacoesNotaFiscalZeus.cobr.dup == null)
                    {
                        informacoesNotaFiscalZeus.cobr.dup = new List<dup>();
                    }

                    var duplicata = notaFiscal.DadosCobranca.ListaDuplicatas[i];

                    dup duplicataZeus = new dup();

                    duplicataZeus.dVenc = duplicata.DataVencimento.ToString("yyyy-MM-dd").ToDate();
                    duplicataZeus.nDup = duplicata.NumeroDuplicata;
                    duplicataZeus.vDup = (decimal)duplicata.ValorDuplicata;

                    informacoesNotaFiscalZeus.cobr.dup.Add(duplicataZeus);
                }
            }
        }

        private void ConvertaTransporteAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            transp transporteZeus = new transp();

            // Foi feito isso pois, no zeus o tipo frete 9 é o 3 no enumerador e
            // não tem como alterar do akil, pelo fato de outros locais estarem utilizando e ser o padrão da nfe.
            int tipoFrete = (int)notaFiscal.InformacoesGeraisNotaFiscal.TipoFrete;

            if (tipoFrete == 9)
            {
                //tipoFrete = 5;
            }

            transporteZeus.modFrete = (ModalidadeFrete)tipoFrete;

            if (notaFiscal.InformacoesGeraisNotaFiscal.TransportadoraId == null || notaFiscal.InformacoesGeraisNotaFiscal.TransportadoraId ==0)
            {
                informacoesNotaFiscalZeus.transp = transporteZeus;
                return;
            }

            ServicoPessoa servicoPessoa = new ServicoPessoa();
                        
            var transportadoraAkil = servicoPessoa.Consulte(notaFiscal.InformacoesGeraisNotaFiscal.TransportadoraId.ToInt());

            if (transportadoraAkil != null)
            {
                transporteZeus.transporta = new transporta();

                transporteZeus.transporta.xNome = transportadoraAkil.DadosGerais.Razao != null? transportadoraAkil.DadosGerais.Razao:null;
                transporteZeus.transporta.CNPJ = transportadoraAkil.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAJURIDICA ? transportadoraAkil.DadosGerais.CpfCnpj.RemoverCaracteresDeMascara() : null;
                transporteZeus.transporta.CPF = transportadoraAkil.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAFISICA ? transportadoraAkil.DadosGerais.CpfCnpj.RemoverCaracteresDeMascara() : null;
                transporteZeus.transporta.xEnder = transportadoraAkil.ListaDeEnderecos.Count != 0 ? transportadoraAkil.ListaDeEnderecos.FirstOrDefault().Rua : null;
                transporteZeus.transporta.xMun = transportadoraAkil.ListaDeEnderecos.Count != 0 ? transportadoraAkil.ListaDeEnderecos.FirstOrDefault().Cidade.Descricao : null;
                transporteZeus.transporta.UF = transportadoraAkil.ListaDeEnderecos.Count != 0 ? transportadoraAkil.ListaDeEnderecos.FirstOrDefault().Cidade.Estado.UF : null;
            }
            
            informacoesNotaFiscalZeus.transp = transporteZeus;
            
        }

        private void ConvertaVolumeAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            vol volumeZeus = new vol();

            volumeZeus.pesoB = (decimal)notaFiscal.ListaItens.Sum(x => x.Produto.Principal.PesoBruto * x.Quantidade);

            volumeZeus.qVol = notaFiscal.InformacoesGeraisNotaFiscal.Volume == 0 ? (int)notaFiscal.ListaItens.Count() : notaFiscal.InformacoesGeraisNotaFiscal.Volume.ToInt();

            informacoesNotaFiscalZeus.transp.vol = new List<vol>();
            informacoesNotaFiscalZeus.transp.vol.Add (volumeZeus);
        }


        private void ConvertaInformacoesAdicionaisAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            infAdic informacoesAdicionaisZeus = new infAdic();

            informacoesAdicionaisZeus.infCpl += notaFiscal.InformacoesGeraisNotaFiscal.Observacoes;

            if (notaFiscal.IdentificacaoNotaFiscal.TipoEmissaoDanfe == EnumTipoEmissaoDanfe.CONTINGENCIAOFFLINE)
                informacoesAdicionaisZeus.infAdFisco = "EMITIDA EM CONTINGÊNCIA Pendente de autorização";
            else
                informacoesAdicionaisZeus.infAdFisco = notaFiscal.InformacoesGeraisNotaFiscal.ObservacoesFisco == string.Empty ? null : notaFiscal.InformacoesGeraisNotaFiscal.ObservacoesFisco;
            
            informacoesNotaFiscalZeus.infAdic = informacoesAdicionaisZeus;
        }

        private void ConvertaTotaisAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            total totaisZeus = new total();

            totaisZeus.ICMSTot = new ICMSTot();

            totaisZeus.ICMSTot.vBC = (decimal)notaFiscal.TotaisNotaFiscal.BaseCalculoIcms;
            totaisZeus.ICMSTot.vBCST = (decimal)notaFiscal.TotaisNotaFiscal.BaseCalculoIcmsST;

            totaisZeus.ICMSTot.vCOFINS = (decimal)notaFiscal.TotaisNotaFiscal.Cofins;
            totaisZeus.ICMSTot.vPIS = (decimal)notaFiscal.TotaisNotaFiscal.Pis;

            //if (notaFiscal.IdentificacaoNotaFiscal.FinalidadeEmissaoNFe == EnumFinalidadeEmissaoNfe.NFEDEVOLUCAO)
            // {
            //totaisZeus.ICMSTot.vIPIDevol = (decimal)notaFiscal.TotaisNotaFiscal.Ipi;                
            //}
            //else

            totaisZeus.ICMSTot.vIPI = (decimal)notaFiscal.TotaisNotaFiscal.Ipi;
            totaisZeus.ICMSTot.vIPIDevol = 0;

            totaisZeus.ICMSTot.vDesc = (decimal)notaFiscal.TotaisNotaFiscal.Desconto;
            totaisZeus.ICMSTot.vFrete = (decimal)notaFiscal.TotaisNotaFiscal.Frete;
            totaisZeus.ICMSTot.vICMS = (decimal)notaFiscal.TotaisNotaFiscal.Icms;
            totaisZeus.ICMSTot.vICMSDeson = (decimal)notaFiscal.TotaisNotaFiscal.IcmsDesoneracao;
            totaisZeus.ICMSTot.vII = (decimal)notaFiscal.TotaisNotaFiscal.ImpostoDeImportacao;
            totaisZeus.ICMSTot.vNF = (decimal)notaFiscal.TotaisNotaFiscal.ValorNotaFiscal;
            totaisZeus.ICMSTot.vOutro = (decimal)notaFiscal.TotaisNotaFiscal.OutrosValores;
            totaisZeus.ICMSTot.vProd = (decimal)notaFiscal.TotaisNotaFiscal.Produtos;
            totaisZeus.ICMSTot.vSeg = (decimal)notaFiscal.TotaisNotaFiscal.ValorSeguro;
            totaisZeus.ICMSTot.vST = (decimal)notaFiscal.TotaisNotaFiscal.ValorSubstituicaoTributaria;
            totaisZeus.ICMSTot.vTotTrib = (decimal)notaFiscal.TotaisNotaFiscal.TotalTributacao;

            if (notaFiscal.IdentificacaoNotaFiscal.ConsumidorFinal && notaFiscal.Destinatario.IndicadorIEDestinatario ==
               EnumIndicadorIEDestinatario.NAOCONTRIBUINTE)
            {
                totaisZeus.ICMSTot.vFCP = 0;
                totaisZeus.ICMSTot.vFCPST = 0;
            }
            else
            {
                totaisZeus.ICMSTot.vFCP = (decimal)notaFiscal.TotaisNotaFiscal.TotalFCPNf.ToDouble();
                totaisZeus.ICMSTot.vFCPST = (decimal)notaFiscal.TotaisNotaFiscal.ValorFCPSt.ToDouble();
            }            

            totaisZeus.ICMSTot.vFCPSTRet = 0;
                        
            totaisZeus.ICMSTot.vFCPUFDest = (decimal?)notaFiscal.TotaisNotaFiscal.ValorFCP;
            totaisZeus.ICMSTot.vICMSUFDest = (decimal?)notaFiscal.TotaisNotaFiscal.ValorInterestadualDestino;
            totaisZeus.ICMSTot.vICMSUFRemet = (decimal?)notaFiscal.TotaisNotaFiscal.ValorInterestadualOrigem;

            if (notaFiscal.TotaisNotaFiscal.RetencaoTributosNotaFiscal != null)
            {
                totaisZeus.retTrib.vBCIRRF = (decimal?)notaFiscal.TotaisNotaFiscal.RetencaoTributosNotaFiscal.BaseCalculoIRRF;
                totaisZeus.retTrib.vBCRetPrev = (decimal?)notaFiscal.TotaisNotaFiscal.RetencaoTributosNotaFiscal.BaseCalculoRetencaoPrevidenciaSocial;
                //totaisZeus.retTrib.vIRRF = (decimal?)notaFiscal.TotaisNotaFiscal.RetencaoTributosNotaFiscal.
                totaisZeus.retTrib.vRetCOFINS = (decimal?)notaFiscal.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetidoCofins;
                totaisZeus.retTrib.vRetCSLL = (decimal?)notaFiscal.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetidoCsll;
                totaisZeus.retTrib.vRetPIS = (decimal?)notaFiscal.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetidoPis;
                totaisZeus.retTrib.vRetPrev = (decimal?)notaFiscal.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetencaoPrevidenciaSocial;
            }

            informacoesNotaFiscalZeus.total = totaisZeus;
        }

        private void ConvertaInformacoesComercioExteriorAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            if (notaFiscal.InformacoesComercioExteriorNotaFiscal != null)
            {
                exporta exportacaoZeus = new exporta();

                exportacaoZeus.UFSaidaPais = notaFiscal.InformacoesComercioExteriorNotaFiscal.UFEmbarque;
                exportacaoZeus.xLocDespacho = notaFiscal.InformacoesComercioExteriorNotaFiscal.DescricaoLocalDespacho;
                exportacaoZeus.xLocExporta = notaFiscal.InformacoesComercioExteriorNotaFiscal.DescricaoLocalEmbarque;

                informacoesNotaFiscalZeus.exporta = exportacaoZeus;
            }
        }

        private void ConvertaInformacoesCompraAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            if (notaFiscal.InformacoesCompraNotaFiscal != null)
            {
                compra copmraZeus = new compra();

                copmraZeus.xCont = notaFiscal.InformacoesCompraNotaFiscal.Contrato;
                copmraZeus.xNEmp = notaFiscal.InformacoesCompraNotaFiscal.NotaEmpenho;
                copmraZeus.xPed = notaFiscal.InformacoesCompraNotaFiscal.Pedido;

                informacoesNotaFiscalZeus.compra = copmraZeus;
            }
        }

        private void ConvertaLocalRetiradaAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            if (notaFiscal.LocalRetirada != null)
            {
                retirada retiradaZeus = new retirada();

                retiradaZeus.cMun = notaFiscal.LocalRetirada.CodigoMunicipio;

                if (notaFiscal.LocalRetirada.TipoPessoa == EnumTipoPessoa.PESSOAJURIDICA)
                {
                    retiradaZeus.CNPJ = notaFiscal.LocalRetirada.CpfCnpj;
                }
                else
                {
                    retiradaZeus.CPF = notaFiscal.LocalRetirada.CpfCnpj;
                }

                retiradaZeus.nro = notaFiscal.LocalRetirada.Numero;
                retiradaZeus.UF = notaFiscal.LocalRetirada.UF;
                retiradaZeus.xBairro = notaFiscal.LocalRetirada.Bairro;
                retiradaZeus.xCpl = notaFiscal.LocalRetirada.Complemento;
                retiradaZeus.xLgr = notaFiscal.LocalRetirada.Logradouro;
                retiradaZeus.xMun = notaFiscal.LocalRetirada.NomeMunicipio;

                informacoesNotaFiscalZeus.retirada = retiradaZeus;
            }
        }

        private void ConvertaLocalEntregaAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            if (notaFiscal.LocalRetirada != null)
            {
                entrega entregaZeus = new entrega();

                entregaZeus.cMun = notaFiscal.LocalRetirada.CodigoMunicipio;

                if (notaFiscal.LocalRetirada.TipoPessoa == EnumTipoPessoa.PESSOAJURIDICA)
                {
                    entregaZeus.CNPJ = notaFiscal.LocalRetirada.CpfCnpj;
                }
                else
                {
                    entregaZeus.CPF = notaFiscal.LocalRetirada.CpfCnpj;
                }

                entregaZeus.nro = notaFiscal.LocalRetirada.Numero;
                entregaZeus.UF = notaFiscal.LocalRetirada.UF;
                entregaZeus.xBairro = notaFiscal.LocalRetirada.Bairro;
                entregaZeus.xCpl = notaFiscal.LocalRetirada.Complemento;
                entregaZeus.xLgr = notaFiscal.LocalRetirada.Logradouro;
                entregaZeus.xMun = notaFiscal.LocalRetirada.NomeMunicipio;

                informacoesNotaFiscalZeus.entrega = entregaZeus;
            }
        }
        
        private void ConvertaPagamentoAkilNFeParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {   
            informacoesNotaFiscalZeus.pag = new List<pag>();
                        
            pag formaPagamentoZeus = new pag();

            formaPagamentoZeus.vPag = null;
            
            formaPagamentoZeus.vTroco = 0;
                    
            formaPagamentoZeus.tPag = null;
                    
            formaPagamentoZeus.card = null;

            informacoesNotaFiscalZeus.pag.Add(formaPagamentoZeus);
                
            foreach (var itemDetalhePag in informacoesNotaFiscalZeus.pag)
            {
                itemDetalhePag.detPag = new List<detPag>();

                if (notaFiscal.InformacoesDocumentoOrigemNotaFiscal.Origem != EnumTipoDocumento.OUTRASSAIDAS)
                {
                    foreach (var formaPagamento in notaFiscal.DadosCobranca.ListaDeParcelasVendas)
                    {
                        detPag detalhePagamento = new detPag();

                        detalhePagamento.vPag = Math.Round((decimal)formaPagamento.Valor,2);

                        detalhePagamento.indPag = (IndicadorPagamentoDetalhePagamento)notaFiscal.DadosCobranca.CondicaoVistaPrazo;

                        var formaPagamentoParaNfe2 = new ServicoNotaFiscal().RetorneFormaPagamentoParaNF(formaPagamento.FormaPagamento.TipoFormaPagamento);

                        if (formaPagamentoParaNfe2 == EnumFormaPagamentoNfce.CARTAOCREDITO ||
                               formaPagamentoParaNfe2 == EnumFormaPagamentoNfce.CARTAODEBITO)
                        {
                            detalhePagamento.card = new card { tpIntegra = TipoIntegracaoPagamento.TipNaoIntegrado, tBand = BandeiraCartao.bcOutros };
                        }

                        if (formaPagamento.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO)
                        {
                            detalhePagamento.xPag = "Depósito Bancário";
                        }
                        else if(formaPagamento.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.OUTROS)
                        {
                            detalhePagamento.xPag = "Outros não discriminado";
                        }

                        detalhePagamento.tPag = (FormaPagamento)retornaFormaPagamentoZeus(formaPagamentoParaNfe2);

                        itemDetalhePag.detPag.Add(detalhePagamento);

                        if (notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal != 65)//Nota modelo NFCe
                        {
                            cobr cobranca = new cobr();
                            cobranca.fat = null;

                            cobranca.dup = new List<dup>();
                        }
                    }
                }
                else
                {
                    foreach (var formaPagamento in notaFiscal.DadosCobranca.ListaDuplicatas)
                    {
                        detPag detalhePagamento = new detPag();

                        detalhePagamento.vPag = (decimal)formaPagamento.ValorDuplicata;

                        detalhePagamento.indPag = (IndicadorPagamentoDetalhePagamento)notaFiscal.DadosCobranca.CondicaoVistaPrazo;

                        var formaPagamentoParaNfe2 = new ServicoNotaFiscal().RetorneFormaPagamentoParaNF(formaPagamento.FormaPagamento);

                        if (formaPagamentoParaNfe2 == EnumFormaPagamentoNfce.CARTAOCREDITO ||
                               formaPagamentoParaNfe2 == EnumFormaPagamentoNfce.CARTAODEBITO)
                        {
                            detalhePagamento.card = new card { tpIntegra = TipoIntegracaoPagamento.TipNaoIntegrado, tBand = BandeiraCartao.bcOutros };
                        }

                        if (formaPagamento.FormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO)
                        {
                            detalhePagamento.xPag = "Depósito Bancário";
                        }
                        else if (formaPagamento.FormaPagamento == EnumTipoFormaPagamento.OUTROS)
                        {
                            detalhePagamento.xPag = "Outros não discriminado";
                        }

                        if (notaFiscal.IdentificacaoNotaFiscal.FinalidadeEmissaoNFe == EnumFinalidadeEmissaoNfe.NFEDEVOLUCAO)
                        {
                            detalhePagamento.tPag = FormaPagamento.fpSemPagamento;
                        }
                        else
                        {
                            detalhePagamento.tPag = (FormaPagamento)retornaFormaPagamentoZeus(formaPagamentoParaNfe2);
                        }

                        itemDetalhePag.detPag.Add(detalhePagamento);                        
                    }
                }                
            }

            IgualarPagamentosComValorTotalDaNotaFiscal(notaFiscal, informacoesNotaFiscalZeus);

        }

        private void IgualarPagamentosComValorTotalDaNotaFiscal(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            foreach (var itemDetalhePag in informacoesNotaFiscalZeus.pag)
            {
                foreach (var item in itemDetalhePag.detPag)
                {
                    var totalPagamentos = itemDetalhePag.detPag.Sum(x => x.vPag).ToDouble();

                    if (totalPagamentos == notaFiscal.TotaisNotaFiscal.ValorNotaFiscal)
                        return;
                    else if (totalPagamentos < notaFiscal.TotaisNotaFiscal.ValorNotaFiscal)
                    {
                        var valorMenor = Math.Round((notaFiscal.TotaisNotaFiscal.ValorNotaFiscal - totalPagamentos), 2);

                        item.vPag = (decimal)(item.vPag.ToDouble() + valorMenor.ToDouble());
                    }
                    else if (totalPagamentos > notaFiscal.TotaisNotaFiscal.ValorNotaFiscal)
                    {
                        var valorMaior = Math.Round((totalPagamentos - notaFiscal.TotaisNotaFiscal.ValorNotaFiscal),2);

                        item.vPag = (decimal)(item.vPag.ToDouble() - valorMaior.ToDouble());
                    }
                }
             }
        }

        private FormaPagamento retornaFormaPagamentoZeus(EnumFormaPagamentoNfce FormaPagamentoNF)
        {
            if (FormaPagamentoNF == EnumFormaPagamentoNfce.DINHEIRO)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpDinheiro;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.CHEQUE)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpCheque;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.CARTAOCREDITO)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpCartaoCredito;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.CARTAODEBITO)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpCartaoDebito;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.CREDITOLOJA)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpCreditoLoja;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.VALEALIMENTACAO)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpValeAlimentacao;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.VALEREFEICAO)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpValeRefeicao;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.VALEPRESENTE)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpValePresente;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.VALECOMBUSTIVEL)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpValeCombustivel;
            }
            else if(FormaPagamentoNF == EnumFormaPagamentoNfce.BOLETOBANCARIO)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpBoletoBancario;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.DUPLICATA)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpDuplicataMercantil;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.OUTROS)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpOutro;
            }
            else if (FormaPagamentoNF == EnumFormaPagamentoNfce.PIX)
            {
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpPagamentoInstantaneoPIX;
            }
            else
                return NFe.Classes.Informacoes.Pagamento.FormaPagamento.fpOutro;
        }

        private void ConvertaFormasPagamentoAkilParaZeus(NotaFiscal notaFiscal, infNFe informacoesNotaFiscalZeus)
        {
            if (notaFiscal.ListaFormasPagamentoNFCe == null || notaFiscal.ListaFormasPagamentoNFCe.Count == 0)
            {
                return;
            }

            informacoesNotaFiscalZeus.pag = new List<pag>();

            foreach (var formaPagamento in notaFiscal.ListaFormasPagamentoNFCe)
            {
                pag formaPagamentoZeus = new pag();

                formaPagamentoZeus.vPag = null;

                formaPagamentoZeus.tPag = null;

                formaPagamentoZeus.vTroco = 0;
                                
                formaPagamentoZeus.card = null;

                informacoesNotaFiscalZeus.pag.Add(formaPagamentoZeus);
            }
        }

        #region " IMPOSTOS "

        #region " IPI "

        private IPI RetorneIpiZeus(ItemNotaFiscal item)
        {
            //Para simples nacional tem que retornar nulo.
            return null;
        }

        private IPIBasico RetorneIpiZeusIPITrib(ItemNotaFiscal item)
        {
            if (item.Impostos.Ipi == null)
                return null;

            var iPITrib = new IPITrib();
            iPITrib.vBC = (decimal?)item.Impostos.Ipi.BaseDeCalculo;
            iPITrib.pIPI = (decimal?)item.Impostos.Ipi.AliquotaIpi;
            iPITrib.vIPI = (decimal?)item.Impostos.Ipi.ValorIpi;
            
            if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI00)
            {
                iPITrib.CST = CSTIPI.ipi00;                            
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI49)
            {
                iPITrib.CST = CSTIPI.ipi49;                           
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI50)
            {
                iPITrib.CST = CSTIPI.ipi50;                
            }
     
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI99)
            {
                iPITrib.CST = CSTIPI.ipi99;
            }
            else
            {
                iPITrib = null;
            }

            return iPITrib;
        }

        private IPIBasico RetorneIpiZeusIPINT(ItemNotaFiscal item)
        {
            if (item.Impostos.Ipi == null)
                return null;

            var iPINT = new IPINT();

            if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI01)
            {
                iPINT.CST = CSTIPI.ipi01;
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI02)
            {
                iPINT.CST = CSTIPI.ipi02;
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI03)
            {
                iPINT.CST = CSTIPI.ipi03;
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI04)
            {
                iPINT.CST = CSTIPI.ipi04;
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI05)
            {
                iPINT.CST = CSTIPI.ipi05;
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI51)
            {
                iPINT.CST = CSTIPI.ipi51;
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI52)
            {
                iPINT.CST = CSTIPI.ipi52;
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI53)
            {
                iPINT.CST = CSTIPI.ipi53;
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI54)
            {
                iPINT.CST = CSTIPI.ipi54;
            }
            else if (item.Impostos.Ipi.CstIpi == EnumCstIpi.IPI55)
            {
                iPINT.CST = CSTIPI.ipi55;
            }
            else
            {
                iPINT = null;
            }
            
            return iPINT;
        }

        #endregion

        #region " COFINS "

        private COFINS RetorneCofinsZeus(NotaFiscal notaFiscal, ItemNotaFiscal item)
        {
            //No caso de NFC-e Cofins é opcional.
            if (notaFiscal.Emitente.CRT != EnumCodigoRegimeTributario.REGIMENORMAL)
            {
                if (notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 65)
                {
                    return null;
                }
            }
    

            //TODO: só está implementado para o simples nacional.
            var tipoCofins = new COFINSOutr();
            var cofins = new COFINS();

            if (notaFiscal.Emitente.CRT != EnumCodigoRegimeTributario.REGIMENORMAL || item.Impostos.Cofins == null)
            {
                tipoCofins.CST = CSTCOFINS.cofins99;
                tipoCofins.qBCProd = 0;
                tipoCofins.vAliqProd = 0;
                tipoCofins.vCOFINS = 0;
            }
            else
            {   
                //Grupo não tributado *
                if (item.Impostos.Cofins.CstCofins == EnumCstCofins.cofins04 ||
                    item.Impostos.Cofins.CstCofins == EnumCstCofins.cofins07 || item.Impostos.Cofins.CstCofins == EnumCstCofins.cofins08 ||
                    item.Impostos.Cofins.CstCofins == EnumCstCofins.cofins09)
                {
                    RetorneCSTCofins(item.Impostos.Cofins.CstCofins, ref tipoCofins);
                }
                //Grupo Substituição tributária **
                else if (item.Impostos.Cofins.CstCofins == EnumCstCofins.cofins05)
                {
                    RetorneCSTCofins(item.Impostos.Cofins.CstCofins, ref tipoCofins);
                    tipoCofins.vBC = (decimal?)item.Impostos.Cofins.BaseDeCalculoST;
                    tipoCofins.pCOFINS = (decimal?)item.Impostos.Cofins.AliquotaPercentualST;

                    tipoCofins.vAliqProd = (decimal?)item.Impostos.Cofins.AliquotaReaisST;
                    tipoCofins.qBCProd = (decimal?)item.Impostos.Cofins.QuantidadeVendidaST;

                    //Enviar o Valor Cofins, caso Cofins for calculado por valor
                    tipoCofins.vCOFINS = item.Impostos.Cofins.AliquotaReaisST != null? (decimal?)item.Impostos.Cofins.ValorCofinsST: null;

                }
                else if (item.Impostos.Cofins.CstCofins == EnumCstCofins.cofins06)
                {
                    RetorneCSTCofins(item.Impostos.Cofins.CstCofins, ref tipoCofins);
                    tipoCofins.CST = CSTCOFINS.cofins99;
                    tipoCofins.vBC = (decimal?)item.Impostos.Cofins.BaseDeCalculoST;
                    tipoCofins.pCOFINS = (decimal?)item.Impostos.Cofins.AliquotaPercentualST;

                    tipoCofins.vAliqProd = (decimal?)item.Impostos.Cofins.AliquotaReaisST;
                    tipoCofins.qBCProd = (decimal?)item.Impostos.Cofins.QuantidadeVendidaST;

                    //Enviar o Valor Cofins, caso Cofins for calculado por valor
                    tipoCofins.vCOFINS = item.Impostos.Cofins.AliquotaReaisST != null ? (decimal?)item.Impostos.Cofins.ValorCofinsST : null;

                }
                //Grupo tributado pela alíquota ***
                else if (item.Impostos.Cofins.CstCofins == EnumCstCofins.cofins01 || item.Impostos.Cofins.CstCofins == EnumCstCofins.cofins02)
                {
                    RetorneCSTCofins(item.Impostos.Cofins.CstCofins, ref tipoCofins);
                    tipoCofins.CST = CSTCOFINS.cofins99;

                    tipoCofins.vBC = (decimal?)item.Impostos.Cofins.BaseDeCalculo;
                    tipoCofins.pCOFINS = (decimal?)item.Impostos.Cofins.AliquotaPercentual;
                    //item.Impostos.Cofins.ValorCofins = 1.00;

                    tipoCofins.vCOFINS = (decimal?)item.Impostos.Cofins.ValorCofins;
                   

    }

              
                //Grupo tributado pela quantidade ****
                else if (item.Impostos.Cofins.CstCofins == EnumCstCofins.cofins03)
                {
                    RetorneCSTCofins(item.Impostos.Cofins.CstCofins, ref tipoCofins);

                    tipoCofins.qBCProd = (decimal?)item.Impostos.Cofins.QuantidadeVendida;
                    tipoCofins.vAliqProd = (decimal?)item.Impostos.Cofins.AliquotaReais;

                    tipoCofins.vCOFINS = (decimal?)item.Impostos.Cofins.ValorCofins;
                }
                //Grupo Cofins Outras Operações *****
                else
                {
                    if(item.Impostos.Cofins.ValorCofins !=0)
                    {
                        RetorneCSTCofins(item.Impostos.Cofins.CstCofins, ref tipoCofins);
                        if (item.Impostos.Cofins.CstCofins == EnumCstCofins.cofins50)
                        {
                            tipoCofins.CST = CSTCOFINS.cofins50;
                        }
                        else 
                        {
                            tipoCofins.CST = CSTCOFINS.cofins99;
                        }
                            
                        //Cofins por percentual não envia o Valor da Cofins
                        tipoCofins.vBC = (decimal?)item.Impostos.Cofins.BaseDeCalculo;
                        tipoCofins.pCOFINS = item.Impostos.Cofins.AliquotaPercentual == null ? 0 : (decimal?)item.Impostos.Cofins.AliquotaPercentual;
                        if (item.Impostos.Cofins.CstCofins != EnumCstCofins.cofins50)
                        {
                            //Cofins por Valor -> neste caso envia o Valor da Cofins
                            tipoCofins.vAliqProd = item.Impostos.Cofins.AliquotaReais == null ? 0 : (decimal?)item.Impostos.Cofins.AliquotaReais;

                            tipoCofins.qBCProd = item.Impostos.Cofins.QuantidadeVendida == null ? 0 : (decimal?)item.Impostos.Cofins.QuantidadeVendida;
                        }
                     
                      
                        //Enviar o Valor Cofins, caso Cofins for calculado por valor
                        tipoCofins.vCOFINS = (decimal?)item.Impostos.Cofins.ValorCofins;
                        
                    }
                    else
                    {
                        tipoCofins.CST = CSTCOFINS.cofins99;
                        tipoCofins.qBCProd = 0;
                        tipoCofins.vAliqProd = 0;
                        tipoCofins.vCOFINS = 0;
                    }
                    
                }
            }                

            cofins.TipoCOFINS = tipoCofins;
            return cofins;
        }
        public enum CSTCOFINSII
        {

            [Description("01")]
            cofins01,

          
        }
        public enum CSTPISII
        {

            [Description("01")]
            pis01,


        }
        private CSTCOFINS? RetorneCSTCofins(EnumCstCofins? cstCofins, ref COFINSOutr tipoCofins )
        {
            switch (cstCofins)
            {
                case EnumCstCofins.cofins01:
                    return tipoCofins.CST = CSTCOFINS.cofins01;

                case EnumCstCofins.cofins02:
                    return tipoCofins.CST = CSTCOFINS.cofins02;

                case EnumCstCofins.cofins03:
                    return tipoCofins.CST = CSTCOFINS.cofins03;

                case EnumCstCofins.cofins04:
                    return tipoCofins.CST = CSTCOFINS.cofins04;

                case EnumCstCofins.cofins05:
                    return tipoCofins.CST = CSTCOFINS.cofins05;

                case EnumCstCofins.cofins06:
                    return tipoCofins.CST = CSTCOFINS.cofins06;

                case EnumCstCofins.cofins07:
                    return tipoCofins.CST = CSTCOFINS.cofins07;

                case EnumCstCofins.cofins08:
                    return tipoCofins.CST = CSTCOFINS.cofins08;

                case EnumCstCofins.cofins09:
                    return tipoCofins.CST = CSTCOFINS.cofins09;

                case EnumCstCofins.cofins49:
                    return tipoCofins.CST = CSTCOFINS.cofins49;

                case EnumCstCofins.cofins50:
                    return tipoCofins.CST = CSTCOFINS.cofins50;

                case EnumCstCofins.cofins51:
                    return tipoCofins.CST = CSTCOFINS.cofins51;

                case EnumCstCofins.cofins52:
                    return tipoCofins.CST = CSTCOFINS.cofins52;

                case EnumCstCofins.cofins53:
                    return tipoCofins.CST = CSTCOFINS.cofins53;

                case EnumCstCofins.cofins54:
                    return tipoCofins.CST = CSTCOFINS.cofins54;

                case EnumCstCofins.cofins55:
                    return tipoCofins.CST = CSTCOFINS.cofins55;

                case EnumCstCofins.cofins56:
                    return tipoCofins.CST = CSTCOFINS.cofins56;

                case EnumCstCofins.cofins60:
                    return tipoCofins.CST = CSTCOFINS.cofins60;

                case EnumCstCofins.cofins61:
                    return tipoCofins.CST = CSTCOFINS.cofins61;

                case EnumCstCofins.cofins62:
                    return tipoCofins.CST = CSTCOFINS.cofins62;

                case EnumCstCofins.cofins63:
                    return tipoCofins.CST = CSTCOFINS.cofins63;

                case EnumCstCofins.cofins64:
                    return tipoCofins.CST = CSTCOFINS.cofins64;

                case EnumCstCofins.cofins65:
                    return tipoCofins.CST = CSTCOFINS.cofins65;

                case EnumCstCofins.cofins66:
                    return tipoCofins.CST = CSTCOFINS.cofins66;

                case EnumCstCofins.cofins67:
                    return tipoCofins.CST = CSTCOFINS.cofins67;

                case EnumCstCofins.cofins70:
                    return tipoCofins.CST = CSTCOFINS.cofins70;

                case EnumCstCofins.cofins71:
                    return tipoCofins.CST = CSTCOFINS.cofins71;

                case EnumCstCofins.cofins72:
                    return tipoCofins.CST = CSTCOFINS.cofins72;

                case EnumCstCofins.cofins73:
                    return tipoCofins.CST = CSTCOFINS.cofins73;

                case EnumCstCofins.cofins74:
                    return tipoCofins.CST = CSTCOFINS.cofins74;

                case EnumCstCofins.cofins75:
                    return tipoCofins.CST = CSTCOFINS.cofins75;

                case EnumCstCofins.cofins98:
                    return tipoCofins.CST = CSTCOFINS.cofins98;

                case EnumCstCofins.cofins99:
                    return tipoCofins.CST = CSTCOFINS.cofins99;
            }

            return null;
        }

        #endregion

        #region " PIS "

        private PIS RetornePisZeus(NotaFiscal notaFiscal, ItemNotaFiscal item)
        {  
            var tipoPis = new PISOutr();
            var pis = new PIS();

            if (notaFiscal.Emitente.CRT != EnumCodigoRegimeTributario.REGIMENORMAL || item.Impostos.Pis == null)
            {
                tipoPis.CST = CSTPIS.pis99;
                tipoPis.qBCProd = 0;
                tipoPis.vAliqProd = 0;
                tipoPis.vPIS = 0;                
            }
            else
            {
                //Grupo PIS não tributado *
                if (item.Impostos.Pis.CstPis == EnumCstPis.pis04  ||
                    item.Impostos.Pis.CstPis == EnumCstPis.pis07 || item.Impostos.Pis.CstPis == EnumCstPis.pis08 ||
                    item.Impostos.Pis.CstPis == EnumCstPis.pis09)
                {
                    RetorneCSTPis(item.Impostos.Pis.CstPis, ref tipoPis);
                }

                //Grupo PIS Substituição Tributária **
                else if (item.Impostos.Pis.CstPis == EnumCstPis.pis05)
                {
                    RetorneCSTPis(item.Impostos.Pis.CstPis, ref tipoPis);

                    //Percentual
                    tipoPis.vBC = (decimal?)item.Impostos.Pis.BaseDeCalculoST;
                    tipoPis.pPIS = (decimal?)item.Impostos.Pis.AliquotaPercentualST;

                    //Valor
                    tipoPis.qBCProd = (decimal?)item.Impostos.Pis.QuantidadeVendidaST;
                    tipoPis.vAliqProd = (decimal?)item.Impostos.Pis.AliquotaReaisST;

                    //Enviar Valor do Pis caso o cálculo for por valor
                    tipoPis.vPIS = item.Impostos.Pis.AliquotaReaisST != null ? (decimal?)item.Impostos.Pis.ValorPisST : null;
                }
                else if (item.Impostos.Pis.CstPis == EnumCstPis.pis06)
                {
                    RetorneCSTPis(item.Impostos.Pis.CstPis, ref tipoPis);
                    tipoPis.CST = CSTPIS.pis99;
                    //Percentual
                    tipoPis.vBC = (decimal?)item.Impostos.Pis.BaseDeCalculoST;
                    tipoPis.pPIS = (decimal?)item.Impostos.Pis.AliquotaPercentualST;

                    //Valor
                    tipoPis.qBCProd = (decimal?)item.Impostos.Pis.QuantidadeVendidaST;
                    tipoPis.vAliqProd = (decimal?)item.Impostos.Pis.AliquotaReaisST;

                    //Enviar Valor do Pis caso o cálculo for por valor
                    tipoPis.vPIS = item.Impostos.Pis.AliquotaReaisST != null ? (decimal?)item.Impostos.Pis.ValorPisST : null;
                }

                //Grupo PIS tributado pela alíquota
                else if (item.Impostos.Pis.CstPis == EnumCstPis.pis01 || item.Impostos.Pis.CstPis == EnumCstPis.pis02)
                {
                    RetorneCSTPis(item.Impostos.Pis.CstPis, ref tipoPis);
                    tipoPis.CST = CSTPIS.pis99;
                    tipoPis.vBC = (decimal?)item.Impostos.Pis.BaseDeCalculo;
                    tipoPis.pPIS = (decimal?)item.Impostos.Pis.AliquotaPercentual;

                    tipoPis.vPIS = (decimal?)item.Impostos.Pis.ValorPis;
                    tipoPis.qBCProd = null;
                }
                //Grupo PIS tributado por Qtde
                else if (item.Impostos.Pis.CstPis == EnumCstPis.pis03)
                {
                    RetorneCSTPis(item.Impostos.Pis.CstPis, ref tipoPis);

                    tipoPis.qBCProd = (decimal?)item.Impostos.Pis.QuantidadeVendida;
                    tipoPis.vAliqProd = (decimal?)item.Impostos.Pis.AliquotaReais;

                    tipoPis.vPIS = (decimal?)item.Impostos.Pis.ValorPis;
                }
                else
                {
                    if (item.Impostos.Pis.ValorPis!=0)
                    {
                        //Grupo PIS não tributado
                        RetorneCSTPis(item.Impostos.Pis.CstPis, ref tipoPis);
                        if (item.Impostos.Pis.CstPis == EnumCstPis.pis50)
                        {
                            tipoPis.CST = CSTPIS.pis50;
                        }
                        else
                        {
                            tipoPis.CST = CSTPIS.pis99;
                        }
                  
                        //Percentual
                        tipoPis.vBC = (decimal?)item.Impostos.Pis.BaseDeCalculo;
                        tipoPis.pPIS = (decimal?)item.Impostos.Pis.AliquotaPercentual;

                        if (item.Impostos.Pis.CstPis != EnumCstPis.pis50)
                        {
                            tipoPis.qBCProd = item.Impostos.Pis.QuantidadeVendida == null ? 0 : (decimal?)item.Impostos.Pis.QuantidadeVendida;

                            tipoPis.vAliqProd = (decimal?)item.Impostos.Pis.AliquotaReais;
                        }

                      

                        //Enviar Valor do Pis caso o cálculo for por valor
                        tipoPis.vPIS = (decimal?)item.Impostos.Pis.ValorPis;
                  
                    }
                    else
                    {
                        tipoPis.CST = CSTPIS.pis99;
                        tipoPis.qBCProd = 0;
                        tipoPis.vAliqProd = 0;
                        tipoPis.vPIS = 0;
                    }
                   
                }
            }

            pis.TipoPIS = tipoPis;

            return pis;
        }

        private CSTPIS? RetorneCSTPis(EnumCstPis? cstPis, ref PISOutr tipoPis)
        {
            switch (cstPis)
            {
                case EnumCstPis.pis01:
                    return tipoPis.CST = CSTPIS.pis01;

                case EnumCstPis.pis02:
                    return tipoPis.CST = CSTPIS.pis02;

                case EnumCstPis.pis03:
                    return tipoPis.CST = CSTPIS.pis03;

                case EnumCstPis.pis04:
                    return tipoPis.CST = CSTPIS.pis04;

                case EnumCstPis.pis05:
                    return tipoPis.CST = CSTPIS.pis05;

                case EnumCstPis.pis06:
                    return tipoPis.CST = CSTPIS.pis06;

                case EnumCstPis.pis07:
                    return tipoPis.CST = CSTPIS.pis07;

                case EnumCstPis.pis08:
                    return tipoPis.CST = CSTPIS.pis08;

                case EnumCstPis.pis09:
                    return tipoPis.CST = CSTPIS.pis09;

                case EnumCstPis.pis49:
                    return tipoPis.CST = CSTPIS.pis49;

                case EnumCstPis.pis50:
                    return tipoPis.CST = CSTPIS.pis50;

                case EnumCstPis.pis51:
                    return tipoPis.CST = CSTPIS.pis51;

                case EnumCstPis.pis52:
                    return tipoPis.CST = CSTPIS.pis52;

                case EnumCstPis.pis53:
                    return tipoPis.CST = CSTPIS.pis53;

                case EnumCstPis.pis54:
                    return tipoPis.CST = CSTPIS.pis54;

                case EnumCstPis.pis55:
                    return tipoPis.CST = CSTPIS.pis55;

                case EnumCstPis.pis56:
                    return tipoPis.CST = CSTPIS.pis56;

                case EnumCstPis.pis60:
                    return tipoPis.CST = CSTPIS.pis60;

                case EnumCstPis.pis61:
                    return tipoPis.CST = CSTPIS.pis61;

                case EnumCstPis.pis62:
                    return tipoPis.CST = CSTPIS.pis62;

                case EnumCstPis.pis63:
                    return tipoPis.CST = CSTPIS.pis63;

                case EnumCstPis.pis64:
                    return tipoPis.CST = CSTPIS.pis64;

                case EnumCstPis.pis65:
                    return tipoPis.CST = CSTPIS.pis65;

                case EnumCstPis.pis66:
                    return tipoPis.CST = CSTPIS.pis66;

                case EnumCstPis.pis67:
                    return tipoPis.CST = CSTPIS.pis67;

                case EnumCstPis.pis70:
                    return tipoPis.CST = CSTPIS.pis70;

                case EnumCstPis.pis71:
                    return tipoPis.CST = CSTPIS.pis71;

                case EnumCstPis.pis72:
                    return tipoPis.CST = CSTPIS.pis72;

                case EnumCstPis.pis73:
                    return tipoPis.CST = CSTPIS.pis73;

                case EnumCstPis.pis74:
                    return tipoPis.CST = CSTPIS.pis74;

                case EnumCstPis.pis75:
                    return tipoPis.CST = CSTPIS.pis75;

                case EnumCstPis.pis98:
                    return tipoPis.CST = CSTPIS.pis98;

                case EnumCstPis.pis99:
                    return tipoPis.CST = CSTPIS.pis99;
            }

            return null;
        }

        #endregion

        #region " ICMS "

        private ICMSBasico RetorneIcmsZeus(ItemNotaFiscal itemNotaFiscal, NotaFiscal notafiscal)
        {
            //Vamos entrar no Regime Simples, senão for simples ele vai para o Regime Normal            
            
            if (RetorneIcmsSimplesNacional(itemNotaFiscal) != null)
                return RetorneIcmsSimplesNacional(itemNotaFiscal);
            else return RetorneIcmsRegimeNormal(itemNotaFiscal);
        }

        private ICMSBasico RetorneIcmsRegimeNormal(ItemNotaFiscal itemNotaFiscal)
        {
            if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL00)
            {
                var objeto = new ICMS00();

                objeto.CST = (Csticms)Csticms.Cst00;
               
                objeto.orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem;
                objeto.pICMS = itemNotaFiscal.Impostos.Icms.AliquotaIcms==null?0: (decimal)itemNotaFiscal.Impostos.Icms.AliquotaIcms;
                objeto.modBC = itemNotaFiscal.Impostos.Icms.ModBC == null? 
                               throw new Exception("Informe a Modalidade de Base de Cálculo e os Impostos. Caso não houver imposto, informe zero(0) nos campos. Ou contate suporte.")
                               :(DeterminacaoBaseIcms)itemNotaFiscal.Impostos.Icms.ModBC;
                objeto.vBC = (decimal)itemNotaFiscal.Impostos.Icms.BaseCalculoIcms;
                objeto.vICMS = (decimal)itemNotaFiscal.Impostos.Icms.ValorIcms;

                //objeto.pFCP = itemNotaFiscal.Impostos.Fcp == null ?0: (decimal)itemNotaFiscal.Impostos.Fcp.PercentualFCP;
                //objeto.vFCP = itemNotaFiscal.Impostos.Fcp == null ? 0 : (decimal)itemNotaFiscal.Impostos.Fcp.ValorFCP;
                                   
                return objeto;
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL10)
            {
                //cst importado
                return new ICMS10()
                {
                    CST = Csticms.Cst10,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    pICMS = (decimal)itemNotaFiscal.Impostos.Icms.AliquotaIcms,
                    //modBC = (DeterminacaoBaseIcms)itemNotaFiscal.Impostos.Icms.ModBC,
                    modBC = 0,
                    vBC = (decimal)itemNotaFiscal.Impostos.Icms.BaseCalculoIcms,
                    vICMS = (decimal)itemNotaFiscal.Impostos.Icms.ValorIcms,
                    pRedBCST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria,
                    pICMSST = (decimal)itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria,
                    pMVAST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaIva,
                   

                    vBCST = (decimal)itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria,
                    vICMSST = (decimal)itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria
                    //vBCFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorBaseFCP : 0,
                    //pFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.PercentualFCP : 0,
                    //vFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorFCP : 0,
                    //vBCFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorBCSTFCP : 0,
                    //pFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.PercentualBCSTFCP : 0,
                    //vFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorFCPST : 0
                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL20)
            {
                return new ICMS20
                {
                    CST = Csticms.Cst20,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    pICMS = (decimal)itemNotaFiscal.Impostos.Icms.AliquotaIcms,
                    modBC = (DeterminacaoBaseIcms)itemNotaFiscal.Impostos.Icms.ModBC,
                    vBC = (decimal)itemNotaFiscal.Impostos.Icms.BaseCalculoIcms,
                    vICMS = (decimal)itemNotaFiscal.Impostos.Icms.ValorIcms,
                    motDesICMS = (MotivoDesoneracaoIcms?)itemNotaFiscal.Impostos.Icms.MotivoDesoneracaoProduto,
                    pRedBC = (decimal)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcms,
                    vICMSDeson = (decimal?)itemNotaFiscal.Impostos.Icms.ValorDesoneracaoProduto
                    //vBCFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorBaseFCP:0,
                    //pFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.PercentualFCP:0,
                    //vFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorFCP:0
                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL30)
            {
                return new ICMS30
                {
                    CST = Csticms.Cst30,
                    modBCST = (DeterminacaoBaseIcmsSt)itemNotaFiscal.Impostos.Icms.ModBCST,
                    motDesICMS = (MotivoDesoneracaoIcms?)itemNotaFiscal.Impostos.Icms.MotivoDesoneracaoProduto,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    pICMSST = (decimal)itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria,
                    pMVAST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaIva,
                    pRedBCST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria,
                    vBCST = (decimal)itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria,
                    vICMSDeson = (decimal?)itemNotaFiscal.Impostos.Icms.ValorDesoneracaoProduto,
                    vICMSST = (decimal)itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria
                    //vBCFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorBCSTFCP : 0,
                    //pFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.PercentualBCSTFCP : 0,
                    //vFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorFCPST : 0
                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL40 ||
                      itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL41 ||
                      itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL50)
            {
                return new ICMS40
                {
                    CST = itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL40 ? Csticms.Cst40 :
                              itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL41 ? Csticms.Cst41 : Csticms.Cst50,

                    motDesICMS = (MotivoDesoneracaoIcms?)itemNotaFiscal.Impostos.Icms.MotivoDesoneracaoProduto,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    vICMSDeson = (decimal?)itemNotaFiscal.Impostos.Icms.ValorDesoneracaoProduto,                   
                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL51)
            {
                return new ICMS51
                {
                    CST = Csticms.Cst51,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    modBC = (DeterminacaoBaseIcms?)itemNotaFiscal.Impostos.Icms.ModBC
                    //vBCFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorBaseFCP : 0,
                    //pFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.PercentualFCP : 0,
                    //vFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorFCP : 0

                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL60)
            {
                return new ICMS60
                {
                    CST = Csticms.Cst60,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem
                    //vBCSTRet = (decimal?)itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria,
                    //vICMSSTRet
                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL70)
            {
                return new ICMS70
                {
                    
                    CST = Csticms.Cst70,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    pICMS = (decimal)itemNotaFiscal.Impostos.Icms.AliquotaIcms,
                    modBC = (DeterminacaoBaseIcms)itemNotaFiscal.Impostos.Icms.ModBC,
                    vBC = (decimal)itemNotaFiscal.Impostos.Icms.BaseCalculoIcms,
                    vICMS = (decimal)itemNotaFiscal.Impostos.Icms.ValorIcms,
                    pRedBCST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria,
                    modBCST = (DeterminacaoBaseIcmsSt)itemNotaFiscal.Impostos.Icms.ModBCST,
                    pICMSST = itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria != null? (decimal)itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria:0,
                    //pMVAST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaIva,
                    pMVAST = itemNotaFiscal.Impostos.Icms.AliquotaIva != null ? (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaIva : 0,
                    vBCST = (decimal)itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria,
                    vICMSST = (decimal)itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria,
                    motDesICMS = (MotivoDesoneracaoIcms?)itemNotaFiscal.Impostos.Icms.MotivoDesoneracaoProduto,
                    pRedBC = (decimal)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcms,
                    vICMSDeson = (decimal?)itemNotaFiscal.Impostos.Icms.ValorDesoneracaoProduto
                    //vBCFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorBaseFCP : 0,
                    //pFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.PercentualFCP : 0,
                    //vFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorFCP : 0,
                    //vBCFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorBCSTFCP : 0,
                    //pFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.PercentualBCSTFCP : 0,
                    //vFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorFCPST : 0
                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.NORMAL90)
            {   
                return new ICMS90
                {
                    CST = Csticms.Cst90,                      
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    pICMS = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaIcms,
                    modBC = (DeterminacaoBaseIcms?)itemNotaFiscal.Impostos.Icms.ModBC,
                    vBC = (decimal?)itemNotaFiscal.Impostos.Icms.BaseCalculoIcms,
                    vICMS = (decimal?)itemNotaFiscal.Impostos.Icms.ValorIcms,
                    pRedBCST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria,
                    modBCST = (DeterminacaoBaseIcmsSt?)itemNotaFiscal.Impostos.Icms.ModBCST,
                    pICMSST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria,
                    pMVAST = (decimal?)itemNotaFiscal.Impostos.Icms.PercentualMargemValorAdicST,
                    vBCST = (decimal?)itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria,
                    vICMSST = (decimal?)itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria,
                    motDesICMS = (MotivoDesoneracaoIcms?)itemNotaFiscal.Impostos.Icms.MotivoDesoneracaoProduto,
                    pRedBC = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcms,
                    vICMSDeson = (decimal?)itemNotaFiscal.Impostos.Icms.ValorDesoneracaoProduto
                    //vBCFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorBaseFCP : 0,
                    //pFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.PercentualFCP : 0,
                    //vFCP = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorFCP : 0,
                    //vBCFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorBCSTFCP : 0,
                    //pFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.PercentualBCSTFCP : 0,
                    //vFCPST = itemNotaFiscal.Impostos.Fcp != null ? (decimal)itemNotaFiscal.Impostos.Fcp.ValorFCPST : 0
                };
            }
            else
            {
                return null;
            }
        }

        private ICMSBasico RetorneIcmsSimplesNacional(ItemNotaFiscal itemNotaFiscal)
        {
            if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES101)
            {
                return new ICMSSN101
                {
                    CSOSN = Csosnicms.Csosn101,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    pCredSN = itemNotaFiscal.Impostos.Icms.AliquotaSimplesNacional != null ? (decimal)itemNotaFiscal.Impostos.Icms.AliquotaSimplesNacional : 0,
                    vCredICMSSN = itemNotaFiscal.Impostos.Icms.ValorIcmsSimplesNacional != null ? (decimal)itemNotaFiscal.Impostos.Icms.ValorIcmsSimplesNacional : 0
                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES102 ||
                      itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES103 ||
                      itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES300 ||
                      itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES400)
            {
                return new ICMSSN102
                {
                    CSOSN = itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES102 ? Csosnicms.Csosn102 :
                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES103 ? Csosnicms.Csosn103 :
                                  itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES300 ? Csosnicms.Csosn300 : Csosnicms.Csosn400,

                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem
                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES201)
            {
                return new ICMSSN201

                {
                    CSOSN = Csosnicms.Csosn201,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    pCredSN = (decimal)itemNotaFiscal.Impostos.Icms.AliquotaSimplesNacional,
                    vCredICMSSN = (decimal)itemNotaFiscal.Impostos.Icms.ValorIcmsSimplesNacional,
                    pICMSST = (decimal)itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria,
                    pMVAST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaIva,
                    pRedBCST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria,
                    vBCST = (decimal)itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria,
                    vICMSST = (decimal)itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria,

                    modBCST = itemNotaFiscal.Impostos.Icms.AliquotaIva != null || itemNotaFiscal.Impostos.Icms.AliquotaIva.ToDouble() != 0 ?
                              (DeterminacaoBaseIcmsSt)itemNotaFiscal.Impostos.Icms.ModBCST.GetValueOrDefault() :
                              DeterminacaoBaseIcmsSt.DbisPauta,
                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES202 ||
                      itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES203)
            {
                return new ICMSSN202
                {
                    CSOSN = itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES202 ? Csosnicms.Csosn202 : Csosnicms.Csosn203,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    pICMSST = itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria != null ? (decimal)itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria : 0,
                    pMVAST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaIva,
                    pRedBCST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria,
                    vBCST = (decimal)itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria,
                    vICMSST = (decimal)itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria,

                    modBCST = itemNotaFiscal.Impostos.Icms.AliquotaIva != null || itemNotaFiscal.Impostos.Icms.AliquotaIva.ToDouble() != 0 ?
                              (DeterminacaoBaseIcmsSt)itemNotaFiscal.Impostos.Icms.ModBCST.GetValueOrDefault() :
                              DeterminacaoBaseIcmsSt.DbisPauta

                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES500)
            {
                return new ICMSSN500
                {
                    CSOSN = Csosnicms.Csosn500,
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem
                };
            }
            else if (itemNotaFiscal.Impostos.Icms.CstCsosn == EnumCstCsosn.SIMPLES900)
            {
                return new ICMSSN900
                {
                    //(orig) da mercadoria: 0 (Nacional) ou 1 (Estrangeira –Importação Direta) ou 2
                    //(Estrangeira – Adquirida no Mercado Interno);
                    orig = (OrigemMercadoria)itemNotaFiscal.Impostos.Icms.Origem,
                    CSOSN = Csosnicms.Csosn900,

                    //No cálculo do ICMS Normal, a exemplo de Devoluções e importações, 
                    //preenchimento dos campos próprios da NFe:
                    modBC = (DeterminacaoBaseIcms?)itemNotaFiscal.Impostos.Icms.ModBC,
                    vBC = (decimal?)itemNotaFiscal.Impostos.Icms.BaseCalculoIcms,
                    pRedBC = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcms,
                    pICMS = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaIcms,
                    vICMS = (decimal?)itemNotaFiscal.Impostos.Icms.ValorIcms,

                    //No cálculo do ICMS ST, nas hipóteses de importação de mercadorias do
                    //exterior sujeitas a ST, preenchimento dos campos próprios da NF-e:
                    modBCST = itemNotaFiscal.Impostos.Icms.AliquotaIva != null? 
                              (DeterminacaoBaseIcmsSt)itemNotaFiscal.Impostos.Icms.ModBCST.GetValueOrDefault() :
                               DeterminacaoBaseIcmsSt.DbisPauta,
                    
                    pMVAST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaIva,
                    pRedBCST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria,
                    vBCST = (decimal?)itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria,
                    pICMSST = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria,
                    vICMSST = (decimal?)itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria,

                    //No cálculo do ICMS no Simples Nacional, preenchimento dos campos:
                    pCredSN = (decimal?)itemNotaFiscal.Impostos.Icms.AliquotaSimplesNacional,
                    vCredICMSSN = (decimal?)itemNotaFiscal.Impostos.Icms.ValorIcmsSimplesNacional
                    
                };
            }
            else
            {
                return null;
            }

        }

        #endregion

        #region " ICMS INTERESTADUAL "

        private ICMSUFDest RetorneIcmsInterestadual(ItemNotaFiscal itemNotaFiscal)
        {
            ICMSUFDest icmsUfDest = null;

            if (itemNotaFiscal.Impostos.IcmsInterestadual != null)
            {
                icmsUfDest = new ICMSUFDest();

                icmsUfDest.pFCPUFDest = (decimal)itemNotaFiscal.Impostos.IcmsInterestadual.AliquotaFCP;
                icmsUfDest.pICMSInter = (decimal)itemNotaFiscal.Impostos.IcmsInterestadual.AliquotaInterestadual;
                icmsUfDest.pICMSUFDest = (decimal)itemNotaFiscal.Impostos.IcmsInterestadual.AliquotaInterna;
                icmsUfDest.pICMSInterPart = (decimal)itemNotaFiscal.Impostos.IcmsInterestadual.PercentualProvisorioPartilha;
                icmsUfDest.vBCUFDest = (decimal)itemNotaFiscal.Impostos.IcmsInterestadual.BaseDeCalculo;
                icmsUfDest.vFCPUFDest = (decimal)itemNotaFiscal.Impostos.IcmsInterestadual.ValorFCP;
                icmsUfDest.vICMSUFDest = (decimal)itemNotaFiscal.Impostos.IcmsInterestadual.ValorIcmsDestino;
                icmsUfDest.vICMSUFRemet = (decimal)itemNotaFiscal.Impostos.IcmsInterestadual.ValorIcmsOrigem;
                icmsUfDest.vBCFCPUFDest = (decimal)itemNotaFiscal.ValorTotal;
            }

            return icmsUfDest;
        }
        
        #endregion

        #endregion

        private void ConvertaInformacoesSuplementaresAkilParaZeus(NotaFiscal notaFiscal, NFe.Classes.NFe notaFiscalZeus)
        {
            if (notaFiscal.InformacoesSuplementaresNotaFiscal != null)
            {
                notaFiscalZeus.infNFeSupl = new infNFeSupl();
                notaFiscalZeus.infNFeSupl.qrCode = notaFiscal.InformacoesSuplementaresNotaFiscal.QrCode;                                
            }
        }

        #endregion
    }
}
