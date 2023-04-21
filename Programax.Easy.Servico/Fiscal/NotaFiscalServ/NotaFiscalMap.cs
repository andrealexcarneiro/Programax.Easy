using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ
{
    class NotaFiscalMap : MapeamentoBase<NotaFiscal>
    {
        public NotaFiscalMap()
        {
            Table("NOTASFISCAIS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("NOTA_ID");

            MapeieIdentificacaoNota();
            MapeieEmitenteNota();
            MapeieDestinatarioNota();
            MapeieLocalEntregaNota();
            MapeieLocalRetiradaNota();
            MapeieTotaisNota();

            MapeieInformacoesGeraisNotaFiscal();
            MapeieInformacoesDocumentoOrigemNotaFiscal();
            MapeieInformacoesProtocoloAutorizacaoNotaFiscal();

            MapeieInformacoesCompraNotaFiscal();
            MapeieInformacoesComercioExteriorNotaFiscal();
            MapeieInformacoesSuplementaresNotaFiscal();

            MapeieDadosCobranca();
            MapeieListaItens();
            MapeieListaNotasReferenciadas();

            MapeieListaCartasCorrecoes();
        }

        private void MapeieIdentificacaoNota()
        {
            Component(nota => nota.IdentificacaoNotaFiscal, identificacaoNota =>
         {
             identificacaoNota.Map(ide => ide.CodigoNumericoNota).Column("NOTA_IDE_CODIGO_NUMERICO");
             identificacaoNota.Map(ide => ide.ConsumidorFinal).Column("NOTA_IDE_CONSUMIDOR_FINAL");
             identificacaoNota.Map(ide => ide.DataHoraEmissao).Column("NOTA_IDE_DATA_HORA_EMISSAO");
             identificacaoNota.Map(ide => ide.DataHoraSaida).Column("NOTA_IDE_DATA_HORA_SAIDA");
             identificacaoNota.Map(ide => ide.DigitoVerificadorChaveAcesso).Column("NOTA_IDE_DIGITO_VERIF_CHAVE_ACESO");
             identificacaoNota.Map(ide => ide.FinalidadeEmissaoNFe).Column("NOTA_IDE_FINALIDADE_EMISSAO").CustomType<EnumFinalidadeEmissaoNfe>();
             identificacaoNota.Map(ide => ide.FormaPagamento).Column("NOTA_IDE_FORMA_PAGAMENTO").CustomType<EnumCondicaoPagamentoNota>();
             identificacaoNota.Map(ide => ide.FormatoImpressaoDanfe).Column("NOTA_IDE_FORMATO_IMPRESSAO_DANFE").CustomType<EnumFormatoImpressaoDanfe>();
             identificacaoNota.Map(ide => ide.IdentificacaoOperacaoNotaFiscal).Column("NOTA_IDE_IDENT_OPERACAO_NFE").CustomType<EnumIdenficacaoOperacaoNotaFiscal>();
             identificacaoNota.Map(ide => ide.IndicacaoPresenca).Column("NOTA_IDE_INDICACAO_PRESENCA").CustomType<EnumIndicacaoPresenca>();
             identificacaoNota.Map(ide => ide.ModeloDocumentoFiscal).Column("NOTA_IDE_MODELO_DOCUMENTO_FISCAL");

             identificacaoNota.Map(ide => ide.NotaSaida).Column("NOTA_IDE_NOTA_SAIDA");
             identificacaoNota.Map(ide => ide.NumeroNota).Column("NOTA_IDE_NUMERO_NOTA");
             identificacaoNota.Map(ide => ide.Serie).Column("NOTA_IDE_SERIE");
             identificacaoNota.Map(ide => ide.TipoAmbiente).Column("NOTA_IDE_TIPO_AMBIENTE").CustomType<EnumTipoAmbiente>();
             identificacaoNota.Map(ide => ide.TipoEmissaoDanfe).Column("NOTA_IDE_TIPO_EMISSAO_DANFE").CustomType<EnumTipoEmissaoDanfe>();
             identificacaoNota.Map(ide => ide.VersaoAplicativo).Column("NOTA_IDE_VERSAO_APLICATIVO");
             identificacaoNota.Map(ide => ide.DescricaoNaturezaOperacao).Column("NOTA_IDE_DESCRICAO_NATUREZA_OPERACAO").Not.LazyLoad();

             identificacaoNota.References(ide => ide.Cidade).Column("NOTA_IDE_CIDADE_ID").Not.LazyLoad();
             identificacaoNota.References(ide => ide.NaturezaOperacao).Column("NOTA_IDE_NATUREZA_OPERACAO_ID").Not.LazyLoad();
         });
        }

        private void MapeieEmitenteNota()
        {
            Component(nota => nota.Emitente, emitenteNota =>
         {
             emitenteNota.Map(emit => emit.Bairro).Column("NOTA_EMIT_BAIRRO");
             emitenteNota.Map(emit => emit.Cep).Column("NOTA_EMIT_CEP");
             emitenteNota.Map(emit => emit.CNAE).Column("NOTA_EMIT_CNAE");
             emitenteNota.Map(emit => emit.CNPJ).Column("NOTA_EMIT_CNPJ");
             emitenteNota.Map(emit => emit.CodigoMunicipio).Column("NOTA_EMIT_CODIGO_MUNICIPIO");
             emitenteNota.Map(emit => emit.CodigoPais).Column("NOTA_EMIT_CODIGO_PAIS");
             emitenteNota.Map(emit => emit.Complemento).Column("NOTA_EMIT_COMPLEMENTO");
             emitenteNota.Map(emit => emit.CRT).Column("NOTA_EMIT_CRT").CustomType<EnumCodigoRegimeTributario>();
             emitenteNota.Map(emit => emit.InscricaoEstadual).Column("NOTA_EMIT_INSCRICAO_ESTADUAL");
             emitenteNota.Map(emit => emit.InscricaoMunicipal).Column("NOTA_EMIT_INSCRICAO_MUNICIPAL");
             emitenteNota.Map(emit => emit.Logradouro).Column("NOTA_EMIT_LOGRADOURO");
             emitenteNota.Map(emit => emit.NomeFantasia).Column("NOTA_EMIT_NOME_FANTASIA");
             emitenteNota.Map(emit => emit.NomeMunicipio).Column("NOTA_EMIT_NOME_MUNICIPIO");
             emitenteNota.Map(emit => emit.NomePais).Column("NOTA_EMIT_NOME_PAIS");
             emitenteNota.Map(emit => emit.Numero).Column("NOTA_EMIT_NUMERO");
             emitenteNota.Map(emit => emit.RazaoSocial).Column("NOTA_EMIT_RAZAO_SOCIAL");
             emitenteNota.Map(emit => emit.Telefone).Column("NOTA_EMIT_TELEFONE");
             emitenteNota.Map(emit => emit.UF).Column("NOTA_EMIT_UF");
         });
        }

        private void MapeieDestinatarioNota()
        {
            Component(nota => nota.Destinatario, destinatarioNota =>
         {
             destinatarioNota.Map(dest => dest.Bairro).Column("NOTA_DEST_BAIRRO");
             destinatarioNota.Map(dest => dest.Cep).Column("NOTA_DEST_CEP");
             destinatarioNota.Map(dest => dest.CnpjCpf).Column("NOTA_DEST_CPF_CNPJ");
             destinatarioNota.Map(dest => dest.CodigoMunicipio).Column("NOTA_DEST_CODIGO_MUNICIPIO");
             destinatarioNota.Map(dest => dest.CodigoPais).Column("NOTA_DEST_PAIS");
             destinatarioNota.Map(dest => dest.Complemento).Column("NOTA_DEST_COMPLEMENTO");
             destinatarioNota.Map(dest => dest.EhPessoaFisica).Column("NOTA_DEST_EH_PESSOA_FISICA");
             destinatarioNota.Map(dest => dest.Email).Column("NOTA_DEST_EMAIL");
             destinatarioNota.Map(dest => dest.IdEstrangeiro).Column("NOTA_DEST_IDESTRANGEIRO");
             destinatarioNota.Map(dest => dest.IndicadorIEDestinatario).Column("NOTA_DEST_INDICADOR_IE").CustomType<EnumIndicadorIEDestinatario>();
             destinatarioNota.Map(dest => dest.InscricaoEstadual).Column("NOTA_DEST_INSCRICAO_ESTADUAL");
             destinatarioNota.Map(dest => dest.InscricaoSuframa).Column("NOTA_DEST_INSCRICAO_SUFRAMA");
             destinatarioNota.Map(dest => dest.Logradouro).Column("NOTA_DEST_LOGRADOURO");
             destinatarioNota.Map(dest => dest.NomeMunicipio).Column("NOTA_DEST_NOME_MUNICIPIO");
             destinatarioNota.Map(dest => dest.NomePais).Column("NOTA_DEST_NOME_PAIS");
             destinatarioNota.Map(dest => dest.Numero).Column("NOTA_DEST_NUMERO");
             destinatarioNota.Map(dest => dest.ParceiroResideExterior).Column("NOTA_DEST_PARC_RESIDE_EXTERIOR");
             destinatarioNota.Map(dest => dest.RazaoSocialOuNomeDestinatario).Column("NOTA_DEST_RAZAO_SOCIAL");
             destinatarioNota.Map(dest => dest.Telefone).Column("NOTA_DEST_TELEFONE");
             destinatarioNota.Map(dest => dest.UF).Column("NOTA_DEST_UF");
             destinatarioNota.Map(dest => dest.TipoPessoa).Column("NOTA_DEST_TIPO_PESSOA").CustomType<EnumTipoPessoa>();
             destinatarioNota.Map(dest => dest.DataCadastroPessoa).Column("NOTA_DEST_DATA_CADASTRO_PESSOA");
             destinatarioNota.Map(dest => dest.StatusPessoa).Column("NOTA_DEST_STATUS_PESSOA");

             destinatarioNota.References(dest => dest.Pessoa).Column("NOTA_DEST_PESSOA_ID");
         });
        }

        private void MapeieLocalEntregaNota()
        {
            Component(nota => nota.LocalEntrega, localEntrega =>
            {
                localEntrega.Map(local => local.TipoPessoa).Column("NOTA_ENT_CRT").CustomType<EnumTipoPessoa>();

                localEntrega.Map(local => local.CpfCnpj).Column("NOTA_ENT_CPF_CNPJ");
                localEntrega.Map(local => local.Logradouro).Column("NOTA_ENT_LOGRADOURO");
                localEntrega.Map(local => local.Numero).Column("NOTA_ENT_NUMERO");
                localEntrega.Map(local => local.Complemento).Column("NOTA_ENT_COMPLEMENTO");
                localEntrega.Map(local => local.Bairro).Column("NOTA_ENT_BAIRRO");
                localEntrega.Map(local => local.CodigoMunicipio).Column("NOTA_ENT_CODIGO_MUNICIPIO");
                localEntrega.Map(local => local.NomeMunicipio).Column("NOTA_ENT_NOME_MUNICIPIO");
                localEntrega.Map(local => local.UF).Column("NOTA_ENT_UF");
            });
        }

        private void MapeieLocalRetiradaNota()
        {
            Component(nota => nota.LocalRetirada, localRetirada =>
            {
                localRetirada.Map(local => local.TipoPessoa).Column("NOTA_RET_CRT").CustomType<EnumTipoPessoa>();

                localRetirada.Map(local => local.CpfCnpj).Column("NOTA_RET_CPF_CNPJ");
                localRetirada.Map(local => local.Logradouro).Column("NOTA_RET_LOGRADOURO");
                localRetirada.Map(local => local.Numero).Column("NOTA_RET_NUMERO");
                localRetirada.Map(local => local.Complemento).Column("NOTA_RET_COMPLEMENTO");
                localRetirada.Map(local => local.Bairro).Column("NOTA_RET_BAIRRO");
                localRetirada.Map(local => local.CodigoMunicipio).Column("NOTA_RET_CODIGO_MUNICIPIO");
                localRetirada.Map(local => local.NomeMunicipio).Column("NOTA_RET_NOME_MUNICIPIO");
                localRetirada.Map(local => local.UF).Column("NOTA_RET_UF");
            });
        }

        private void MapeieTotaisNota()
        {
            Component(nota => nota.TotaisNotaFiscal, totaisNota =>
            {
                totaisNota.Map(total => total.BaseCalculoIcms).Column("NOTA_TOTAL_BASE_ICMS");
                totaisNota.Map(total => total.BaseCalculoIcmsST).Column("NOTA_TOTAL_BASE_ICMS_ST");
                totaisNota.Map(total => total.Cofins).Column("NOTA_TOTAL_COFINS");
                totaisNota.Map(total => total.Desconto).Column("NOTA_TOTAL_DESCONTO");
                totaisNota.Map(total => total.Frete).Column("NOTA_TOTAL_FRETE");
                totaisNota.Map(total => total.Icms).Column("NOTA_TOTAL_ICMS");
                totaisNota.Map(total => total.IcmsDesoneracao).Column("NOTA_TOTAL_ICMS_DESONERACAO");
                totaisNota.Map(total => total.ImpostoDeImportacao).Column("NOTA_TOTAL_II");
                totaisNota.Map(total => total.Ipi).Column("NOTA_TOTAL_IPI");
                totaisNota.Map(total => total.OutrosValores).Column("NOTA_TOTAL_OUTROS_VALORES");
                totaisNota.Map(total => total.Pis).Column("NOTA_TOTAL_PIS");
                totaisNota.Map(total => total.Produtos).Column("NOTA_TOTAL_PRODUTOS");
                totaisNota.Map(total => total.TotalTributacao).Column("NOTA_TOTAL_TRIBUTACAO");
                totaisNota.Map(total => total.ValorNotaFiscal).Column("NOTA_TOTAL_NOTA_FISCAL");
                totaisNota.Map(total => total.ValorSeguro).Column("NOTA_TOTAL_SEGURO");
                totaisNota.Map(total => total.ValorSubstituicaoTributaria).Column("NOTA_TOTAL_SUBSTITUICAO_TRIBUTARIA");

                totaisNota.Map(total => total.ValorFCP).Column("NOTA_TOTAL_FCP");
                totaisNota.Map(total => total.ValorInterestadualDestino).Column("NOTA_TOTAL_ICMS_INTERESTADUAL_DESTINO");
                totaisNota.Map(total => total.ValorInterestadualOrigem).Column("NOTA_TOTAL_ICMS_INTERESTADUAL_ORIGEM");

                totaisNota.Map(total => total.TotalTributacaoEstadual).Column("NOTA_TOTAL_TRIBUTACAO_ESTADUAL");
                totaisNota.Map(total => total.TotalTributacaoFederal).Column("NOTA_TOTAL_TRIBUTACAO_FEDERAL");

                totaisNota.Component(total => total.RetencaoTributosNotaFiscal, totalRetencao =>
            {
                totalRetencao.Map(total => total.BaseCalculoIRRF).Column("NOTA_TOTAL_BASE_IRRF");
                totalRetencao.Map(total => total.BaseCalculoRetencaoPrevidenciaSocial).Column("NOTA_TOTAL_BASE_CALC_RET_PREVIDENCIA");
                totalRetencao.Map(total => total.ValorRetencaoPrevidenciaSocial).Column("NOTA_TOTAL_RET_PREVIDENCIA");
                totalRetencao.Map(total => total.ValorRetidoCofins).Column("NOTA_TOTAL_VLR_RETIDO_COFINS");
                totalRetencao.Map(total => total.ValorRetidoCsll).Column("NOTA_TOTAL_VLR_RETIDO_CSLL");
                totalRetencao.Map(total => total.ValorRetidoPis).Column("NOTA_TOTAL_VLR_RETIDO_PIS");
            });
            });
        }

        private void MapeieInformacoesGeraisNotaFiscal()
        {
            Component(nota => nota.InformacoesGeraisNotaFiscal, informacoesGeraisNotaFiscal =>
            {
                informacoesGeraisNotaFiscal.Map(info => info.DataCadastro).Column("NOTA_GERAL_DATA_CADASTRO");
                informacoesGeraisNotaFiscal.Map(info => info.ChaveDeAcesso).Column("NOTA_GERAL_CHAVE_ACESSO");
                informacoesGeraisNotaFiscal.Map(info => info.MensagemDeErro).Column("NOTA_GERAL_MENSAGEM_ERRO");
                informacoesGeraisNotaFiscal.Map(info => info.MensagemDevolvida).Column("NOTA_GERAL_MENSAGEM_DEVOLVIDA");
                informacoesGeraisNotaFiscal.Map(info => info.Observacoes).Column("NOTA_GERAL_OBSERVACOES");
                informacoesGeraisNotaFiscal.Map(info => info.ObservacoesFisco).Column("NOTA_GERAL_OBSERVACOES_FISCO");
                informacoesGeraisNotaFiscal.Map(info => info.NumeroReciboLote).Column("NOTA_GERAL_NUMERO_RECIBO_LOTE");
                informacoesGeraisNotaFiscal.Map(info => info.Status).Column("NOTA_GERAL_STATUS").CustomType<EnumStatusNotaFiscal>();
                informacoesGeraisNotaFiscal.Map(info => info.TipoFrete).Column("NOTA_GERAL_TIPO_FRETE").CustomType<EnumTipoFrete>();
                informacoesGeraisNotaFiscal.Map(info => info.TransportadoraId).Column("NOTA_GERAL_TRANSPORTADORA_ID");
                informacoesGeraisNotaFiscal.Map(info => info.Volume).Column("NOTA_GERAL_VOLUME");
                
            });
        }

        private void MapeieInformacoesDocumentoOrigemNotaFiscal()
        {
            Component(nota => nota.InformacoesDocumentoOrigemNotaFiscal, informacoesDocumentoOrigemNotaFiscal =>
            {
                informacoesDocumentoOrigemNotaFiscal.Map(info => info.DataElaboracao).Column("NOTA_DOCUMENTO_DATA_ELABORACAO");
                informacoesDocumentoOrigemNotaFiscal.Map(info => info.DocumentoId).Column("NOTA_DOCUMENTO_ID");
                informacoesDocumentoOrigemNotaFiscal.Map(info => info.Origem).Column("NOTA_DOCUMENTO_ORIGEM").CustomType<EnumTipoDocumento>();
                informacoesDocumentoOrigemNotaFiscal.Map(info => info.UsuarioId).Column("NOTA_DOCUMENTO_USUARIO_ID");
                informacoesDocumentoOrigemNotaFiscal.Map(info => info.UsurioNome).Column("NOTA_DOCUMENTO_USUARIO_NOME");
            });
        }

        private void MapeieInformacoesProtocoloAutorizacaoNotaFiscal()
        {
            Component(nota => nota.InformacoesProtocoloAutorizacaoNotaFiscal, informacoesProtocolo =>
            {
                informacoesProtocolo.Map(info => info.Id).Column("NOTA_PROTOCOLO_ID");
                informacoesProtocolo.Map(info => info.ChaveNfe).Column("NOTA_PROTOCOLO_CHAVE_NFE");
                informacoesProtocolo.Map(info => info.DataHoraRecibo).Column("NOTA_PROTOCOLO_DATA_HORA_RECIBO");
                informacoesProtocolo.Map(info => info.DigestValue).Column("NOTA_PROTOCOLO_DIGEST_VALUE");
                informacoesProtocolo.Map(info => info.Motivo).Column("NOTA_PROTOCOLO_MOTIVO");
                informacoesProtocolo.Map(info => info.NumeroProtocolo).Column("NOTA_PROTOCOLO_NUMERO_PROTOCOLO");
                informacoesProtocolo.Map(info => info.Status).Column("NOTA_PROTOCOLO_STATUS");
                informacoesProtocolo.Map(info => info.TipoAmbiente).Column("NOTA_PROTOCOLO_TIPO_AMBIENTE");
                informacoesProtocolo.Map(info => info.VersaoAplicativo).Column("NOTA_PROTOCOLO_VERSAO_APLICATIVO");
                informacoesProtocolo.Map(info => info.VersaoNota).Column("NOTA_PROTOCOLO_VERSAO_NOTA");
            });
        }

        private void MapeieInformacoesCancelamentoNotaFiscal()
        {
            Component(nota => nota.InformacoesCancelamentoNotaFiscal, informacoesCancelamentoNotaFiscal =>
            {
                informacoesCancelamentoNotaFiscal.Map(info => info.JustificativaCancelamento).Column("NOTA_CANCELAMENTO_JUSTIFICATIVA");
                informacoesCancelamentoNotaFiscal.Map(info => info.ProtocoloCancelamento).Column("NOTA_CANCELAMENTO_PROTOCOLO");
            });
        }

        private void MapeieListaItens()
        {
            HasMany(nota => nota.ListaItens).KeyColumn("ITEM_NOTA_FISCAL_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("NOTAS_FISCAIS_ITENS").OrderBy("ITEM_ID");
        }

        private void MapeieListaNotasReferenciadas()
        {
            HasMany(nota => nota.ListaNotasReferenciadas).KeyColumn("NOTA_REF_NOTA_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("NOTASFISCAIS_REFERENCIADAS").OrderBy("NOTA_REF_ID").Not.LazyLoad();
        }

        private void MapeieListaCartasCorrecoes()
        {
            HasMany(nota => nota.ListaCartasCorrecoes).KeyColumn("CARTA_CORRECAO_NOTA_ID").AsBag().Table("CARTASCORRECOES");
        }

        private void MapeieListaFormasPagamento()
        {
            HasMany(nota => nota.ListaFormasPagamentoNFCe).KeyColumn("FP_NOTA_FISCAL_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("NOTAS_FISCAIS_FORMA_PAGAMENTO");
        }

        private void MapeieDadosCobranca()
        {
            Component(nota => nota.DadosCobranca, dadosCobranca =>
            {
                dadosCobranca.Component(dados => dados.FaturaNotaFiscal, faturaNotaFiscal =>
            {
                faturaNotaFiscal.Map(fat => fat.NumeroFatura).Column("NOTA_FATURA_NUMERO_FATURA");
                faturaNotaFiscal.Map(fat => fat.ValorDesconto).Column("NOTA_FATURA_VALOR_DESCONTO");
                faturaNotaFiscal.Map(fat => fat.ValorLiquido).Column("NOTA_FATURA_VALOR_LIQUIDO");
                faturaNotaFiscal.Map(fat => fat.ValorOriginalFatura).Column("NOTA_FATURA_VALOR_ORIGINAL_FATURA");
            });

                dadosCobranca.HasMany(nota => nota.ListaDuplicatas).KeyColumn("DUPLICATA_NOTA_FISCAL_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("NOTAS_FISCAIS_DUPLICATAS").Not.LazyLoad();
            });
        }

        private void MapeieInformacoesCompraNotaFiscal()
        {
            Component(nota => nota.InformacoesCompraNotaFiscal, informacoesCompraNotaFiscal =>
            {
                informacoesCompraNotaFiscal.Map(info => info.Contrato).Column("NOTA_INFOCOMPRA_CONTRATO");
                informacoesCompraNotaFiscal.Map(info => info.NotaEmpenho).Column("NOTA_INFOCOMPRA_NOTA_EMPENHO");
                informacoesCompraNotaFiscal.Map(info => info.Pedido).Column("NOTA_INFOCOMPRA_PEDIDO");
            });
        }

        private void MapeieInformacoesComercioExteriorNotaFiscal()
        {
            Component(nota => nota.InformacoesComercioExteriorNotaFiscal, informacoesComercioExteriorNotaFiscal =>
            {
                informacoesComercioExteriorNotaFiscal.Map(info => info.DescricaoLocalDespacho).Column("NOTA_COMERCIOEXT_LOCAL_DESPACHO");
                informacoesComercioExteriorNotaFiscal.Map(info => info.DescricaoLocalEmbarque).Column("NOTA_COMERCIOEXT_LOCAL_EMBARQUE");
                informacoesComercioExteriorNotaFiscal.Map(info => info.UFEmbarque).Column("NOTA_COMERCIOEXT_UF_EMBARQUE");
            });
        }

        private void MapeieInformacoesSuplementaresNotaFiscal()
        {
            Component(nota => nota.InformacoesSuplementaresNotaFiscal, informacoesSuplementaresNotaFiscal =>
            {
                informacoesSuplementaresNotaFiscal.Map(info => info.QrCode).Column("NOTA_INFOSUP_QRCODE");
            });
        }
    }
}
