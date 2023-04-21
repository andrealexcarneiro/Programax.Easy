using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ
{
    public class ParametrosMap : MapeamentoBase<Parametros>
    {
        public ParametrosMap()
        {
            Table("PARAMETROS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PARAM_ID");

            //Map(cad => cad.HabilitarRecursosEspeciais).Column("PARAM_HABILITAR_RECURSOS_ESPECIAIS");

            MapeieCadastros();
            MapeieFinanceiro();
            MapeieVenda();
            MapeieFiscais();
        }

        private void MapeieCadastros()
        {
            Component(produto => produto.ParametrosCadastros, cadastros =>
            {
                cadastros.Map(cad => cad.PermiteCadastroParceiroSemCpfCnpj).Column("PARAM_CADASTRO_PARCEIRO_SEM_CPFCNPJ");
                cadastros.Map(cad => cad.PermiteCadastroParceiroComMesmoCpfCnpj).Column("PARAM_CADASTRO_PARCEIRO_COM_MESMO_CPFCNPJ");
                cadastros.Map(cad => cad.PermiteVendaDiretaNoPDV).Column("PARAM_HABILITAR_RECURSOS_ESPECIAIS");

                cadastros.Map(cad => cad.ValidaEndereco).Column("PARAM_VALIDA_ENDERECO");

                cadastros.Map(cad => cad.CaminhoACBR).Column("PARAM_CAMINHO_ACBR");

                cadastros.Map(cad => cad.ValorVendaManual).Column("PARAM_VALOR_VENDA_MANUAL");
                cadastros.Map(cad => cad.LiberarCampoQtde).Column("PARAM_LIBERAR_CAMPO_QTDE");

                cadastros.Map(cad => cad.AbrirQuantEstoqueItens).Column("PARAM_ABRIR_QUANT_ESTOQUE_ITENS");
                cadastros.Map(cad => cad.MostrarGrupoTribPesquisaItens).Column("PARAM_MOSTRAR_GRUPOTRIB_PESQUISA_ITENS");

                cadastros.Map(cad => cad.PrefixoEan13CodigoBarras).Column("PARAM_CADASTRO_PREFIXO_CODIGO_BARRAS");
                cadastros.Map(cad => cad.TamahoCodigoBarras).Column("PARAM_CADASTRO_TAMANHO_CODIGO_BARRAS");
                cadastros.Map(cad => cad.TipoCodigoBarrasBalanca).Column("PARAM_CADASTRO_TIPO_CODIGO_BARRAS").CustomType<EnumTipoCodigoBarrasBalanca>();
                cadastros.Map(cad => cad.VinculoProdutoCodigoBarrasBalanca).Column("PARAM_CADASTRO_VINCULO_CODIGO_BARRAS_BALANCA").CustomType<EnumVinculoProdutoCodigoBarrasBalanca>();
            });
        }

        private void MapeieFinanceiro()
        {
            Component(produto => produto.ParametrosFinanceiro, financeiro =>
            {
                financeiro.Map(finan => finan.PercentualComissoes).Column("PARAM_PERCENTUAL_COMISSOES");
                financeiro.Map(finan => finan.PercentualDespesasFixas).Column("PARAM_DESPESAS_FIXAS");
                financeiro.Map(finan => finan.PercentualDespesasVariaveis).Column("PARAM_DESPESAS_VARIAVEIS");
                financeiro.Map(finan => finan.PercentualFrete).Column("PARAM_FRETE");
                financeiro.Map(finan => finan.PercentualImpostos).Column("PARAM_PERCENTUAL_IMPOSTOS");
                financeiro.Map(finan => finan.PercentualLucro).Column("PARAM_LUCRO");
                financeiro.Map(finan => finan.PercentualOutrasDespesas).Column("PARAM_OUTRAS_DESPESAS");
                financeiro.Map(finan => finan.MultaContasReceber).Column("PARAM_MULTA_CONTAS_RECEBER");
                financeiro.Map(finan => finan.JurosContasReceber).Column("PARAM_JUROS_CONTAS_RECEBER");
                financeiro.Map(finan => finan.QuestionarSeDesejaEmitirNotaAoReceberPedido).Column("PARAM_QUESTIONA_DESEJA_NOTA_RECEBER_PEDIDO");
                financeiro.Map(finan => finan.QuestionarSeDesejaExportarVendaParaPdv).Column("PARAM_QUESTIONA_DESEJA_EXPORTAR_PARA_PDV");

                financeiro.Map(finan => finan.IgnorarCreditoInicial).Column("PARAM_IGNORAR_CREDITO_INICIAL");

                financeiro.Map(finan => finan.HabilitarResumoFinanceiro).Column("PARAM_HABILITAR_RESUMO_FINANCEIRO");
                financeiro.Map(finan => finan.AbrirResumoFinanceiroAoIniciarAkil).Column("PARAM_RESUMO_FINANCEIRO_AO_INICIAR");

                //Conciliação Bancária
                financeiro.Map(finan => finan.HabilitarConciliacaoBancaria).Column("PARAM_HABILITAR_CONCILIACAO");
                financeiro.Map(finan => finan.ImportacaoAutomaticaExtrato).Column("PARAM_IMPORTACAO_AUTOMATICA_EXTRATO");
                financeiro.Map(finan => finan.DiasAntes).Column("PARAM_DIAS_ANTES");
                financeiro.Map(finan => finan.DiasDepois).Column("PARAM_DIAS_DEPOIS");

                financeiro.Map(finan => finan.ObservacoesCarnePagamento).Column("PARAM_OBSERVACOES_CARNE_PAGAMENTO");

                financeiro.Map(finan => finan.ValoPadraoCreditoInicial).Column("PARAM_VALOR_CREDITO_INICIAL");
            });
        }

        private void MapeieVenda()
        {
            Component(produto => produto.ParametrosVenda, parametrosVenda =>
            {
                parametrosVenda.Map(venda => venda.PermiteAlterarAtendente).Column("PARAM_PERMITE_ALTERAR_ATENDENTE");
                parametrosVenda.Map(venda => venda.PermiteAlterarIndicador).Column("PARAM_PERMITE_ALTERAR_INDICADOR");
                parametrosVenda.Map(venda => venda.PermiteAlterarSupervisor).Column("PARAM_PERMITE_ALTERAR_SUPERVISOR");
                parametrosVenda.Map(venda => venda.PermiteAlterarVendedor).Column("PARAM_PERMITE_ALTERAR_VENDEDOR");
                parametrosVenda.Map(venda => venda.PermiteAlterarValorUnitario).Column("PARAM_PERMITE_ALTERAR_VALOR_UNITARIO");
                parametrosVenda.Map(venda => venda.ExibirTodasAsTabelasPrecoPedidoVenda).Column("PARAM_EXIBIR_TABELAS_PEDIDO_VENDA");
                parametrosVenda.Map(venda => venda.PermiteDescontoNoTotalVenda).Column("PARAM_PERMITE_DESCONTO_TOTAL_VENDA");

                parametrosVenda.Map(venda => venda.PermiteMostrarValorVenda).Column("PARAM_PERMITE_MOSTRAR_VALOR_VENDA");

                parametrosVenda.Map(venda => venda.PermiteAlterarValorUnitarioVendaRapida).Column("PARAM_PERMITE_ALTERAR_VALOR_UNITARIO_VENDA_RAPIDA");
                parametrosVenda.Map(venda => venda.PermiteBaixarEstoqueNaSaida).Column("PARAM_PERMITE_BAIXAR_ESTOQUE_NA_SAIDA");
                parametrosVenda.Map(venda => venda.NaoAceitarEstoqueNegativo).Column("PARAM_NAO_ACEITAR_ESTOQUE_NEGATIVO");
                parametrosVenda.Map(venda => venda.ReserveEstoqueAoFaturarPedido).Column("PARAM_RESERVE_ESTOQUE_AO_FATURAR_PEDIDO");
                parametrosVenda.Map(venda => venda.TrabalharComEstoqueReservado).Column("PARAM_TRABALHAR_COM_ESTOQUE_RESERVADO");
                parametrosVenda.Map(venda => venda.PedidoEmImpressoraTermica).Column("PARAM_IMPRESSORA_TERMICA");

                parametrosVenda.Map(venda => venda.PedidoEmDuasVias).Column("PARAM_IMPRIMIR_DUAS_VIAS");

                parametrosVenda.Map(venda => venda.PedidosPorVendedor).Column("PARAM_PEDIDO_POR_VENDEDOR");

                parametrosVenda.Map(venda => venda.ObservacoesVendaRapida).Column("PARAM_OBSERVACOES_VENDA_RAPIDA");

                parametrosVenda.Map(venda => venda.TermosContrato).Column("PARAM_TERMOS_CONTRATO");
                parametrosVenda.Map(venda => venda.NomeContrato).Column("PARAM_NOME_CONTRATO");

                parametrosVenda.Map(venda => venda.LimiteDiarioManha).Column("PARAM_LIMITE_DIARIO_MANHA");
                parametrosVenda.Map(venda => venda.LimiteDiarioTarde).Column("PARAM_LIMITE_DIARIO_TARDE");
                parametrosVenda.Map(venda => venda.ExibirTelefonePedido).Column("PARAM_TELE_PEDIDO");
                parametrosVenda.Map(venda => venda.ExibirInfoPedido).Column("PARAM_INFO_PEDIDO");
                parametrosVenda.Map(venda => venda.Refiltek).Column("PARAM_REFILTEK");
                parametrosVenda.Map(venda => venda.BaixarFaturamento).Column("PARAM_BAIXAR_PEDIDO_FATURADO");
                parametrosVenda.Map(venda => venda.StatusFaturado).Column("PARAM_STATUS_FATURADO");

                parametrosVenda.Map(venda => venda.TipoFrete).Column("PARAM_TIPO_FRETE").CustomType<EnumTipoFrete>();
                parametrosVenda.Map(venda => venda.ExibirTodasAsTabelasPrecoVendaRapida).Column("PARAM_EXIBIR_TABELAS_VENDA_RAPIDA");
                parametrosVenda.Map(venda => venda.AproveitarEnderecoEmpresaParaCadastroRapidoCliente).Column("PARAM_APROVEITAR_END_ESTAB_CLIENTE_RAPIDO");
                parametrosVenda.References(venda => venda.TabelaPreco).Column("PARAM_TABELA_PRECO_ID");
                parametrosVenda.References(venda => venda.Atendente).Column("PARAM_ATENDENTE_ID");
                parametrosVenda.References(venda => venda.Vendedor).Column("PARAM_VENDEDOR_ID");
                parametrosVenda.References(venda => venda.Transportadora).Column("PARAM_TRANSPORTADORA_ID");
                parametrosVenda.References(venda => venda.FormaPagamento).Column("PARAM_FORMA_PAGAMENTO_ID");
                parametrosVenda.References(venda => venda.CondicaoPagamento).Column("PARAM_CONDICAO_PAGAMENTO_ID");
                


            });
        }

        private void MapeieFiscais()
        {
            Component(produto => produto.ParametrosFiscais, fiscais =>
            {
                fiscais.References(fiscal => fiscal.GrupoTributacaoIcmsTerceiros).Column("PARAM_FISCAL_GRUPO_TRIBUTACAO_ICMS_TERCEIROS_ID");
                fiscais.References(fiscal => fiscal.GrupoTributacaoIcmsProducaoPropria).Column("PARAM_FISCAL_GRUPO_TRIBUTACAO_ICMS_PROD_PROPRIA_ID");

                fiscais.References(fiscal => fiscal.GrupoTributacaoFederalTerceiros).Column("PARAM_FISCAL_GRUPO_TRIBUTACAO_FEDERAL_TERCEIROS_ID");
                fiscais.References(fiscal => fiscal.GrupoTributacaoFederalProducaoPropria).Column("PARAM_FISCAL_GRUPO_TRIBUTACAO_FEDERAL_PROD_PROPRIA_ID");

                fiscais.Map(fiscal => fiscal.CodigoCsc).Column("PARAM_FISCAL_CODIGO_CSC");
                fiscais.Map(fiscal => fiscal.IdCsc).Column("PARAM_FISCAL_ID_CSC");

                fiscais.Map(fiscal => fiscal.CalcularPartilhaIcms).Column("PARAM_FISCAL_CALCULAR_PARTILHA_ICMS");

                fiscais.Map(fiscal => fiscal.CalcularFCP).Column("PARAM_FISCAL_CALCULAR_FCP");

                fiscais.Map(fiscal => fiscal.AvisarQuandoHouverNcmForaDoPrazoValidade).Column("PARAM_FISCAL_AVISO_NCM_FORA_PRAZO");

                fiscais.Map(fiscal => fiscal.EmitirNotaSemReceber).Column("PARAM_EMITIR_SEM_RECEBER");

                fiscais.Map(fiscal => fiscal.ObservacoesGeraisNotaFiscal).Column("PARAM_OBSERVACOES_NOTAFISCAL");

            });
        }
    }
}
