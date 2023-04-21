using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.LiberacaoDocumentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Vendas.LiberacaoDocumentoServ
{
    public class LiberacaoDocumentoMap : MapeamentoBase<LiberacaoDocumento>
    {
        public LiberacaoDocumentoMap()
        {
            Table("VW_LIBERACAO_DOCUMENTO");

            CompositeId()
                        .KeyProperty(x => x.Id, "NUMERO_DOCUMENTO")
                        .KeyProperty(x => x.TipoDocumento, "DOCUMENTO_TIPO_DOCUMENTO").CustomType<EnumTipoDocumentoLiberacao>();

            Map(item => item.ClienteId).Column("DOCUMENTO_CLIENTE_ID");
            Map(item => item.ClienteNomeFantasia).Column("DOCUMENTO_CLIENTE_NOME_FANTASIA");
            Map(item => item.EnderecoCep).Column("DOCUMENTO_ENDERECO_CEP");
            Map(item => item.CidadeId).Column("DOCUMENTO_ENDERECO_CIDADE_ID");
            Map(item => item.CidadeDescricao).Column("DOCUMENTO_ENDERECO_CIDADE_DESCRICAO");
            Map(item => item.EstadoUf).Column("DOCUMENTO_ENDERECO_UF");
            Map(item => item.EstadoNome).Column("DOCUMENTO_ENDERECO_NOME");
            Map(item => item.ClienteInscricaoEstadual).Column("DOCUMENTO_CLIENTE_INSC_ESTADUAL");
            Map(item => item.ClienteInscricaoMunicipal).Column("DOCUMENTO_CLIENTE_INSC_MUNICIPAL");
            Map(item => item.ClienteTipoPessoa).Column("DOCUMENTO_CLIENTE_TIPO_PESSOA").CustomType<EnumTipoPessoa>();
            Map(item => item.ClienteDataCadastro).Column("DOCUMENTO_CLIENTE_DATA_CADASTRO");
            Map(item => item.ClienteStatus).Column("DOCUMENTO_CLIENTE_STATUS");
            Map(item => item.ClienteCpfCnpj).Column("DOCUMENTO_CLIENTE_CPF_CNPJ");
            Map(item => item.DataElaboracao).Column("DOCUMENTO_DATA_ELABORACAO");
            Map(item => item.DataFechamento).Column("DOCUMENTO_DATA_FECHAMENTO");
            Map(item => item.AtendenteId).Column("DOCUMENTO_ATENDENTE_ID");
            Map(item => item.AtendenteNomeFantasia).Column("DOCUMENTO_ATENDENTE_NOME_FANTASIA");
            Map(item => item.VendedorId).Column("DOCUMENTO_VENDEDOR_ID");
            Map(item => item.VendedorNomeFantasia).Column("DOCUMENTO_VENDEDOR_NOME_FANTASIA");
            Map(item => item.Desconto).Column("DOCUMENTO_DESCONTO");
            Map(item => item.DescontoEhPercentual).Column("DOCUMENTO_DESCONTO_EH_PERCENTUAL");
            Map(item => item.ValorTotal).Column("DOCUMENTO_VALOR_TOTAL");
            Map(item => item.UsuarioId).Column("DOCUMENTO_USUARIO_DOCUMENTO_ID");
            Map(item => item.UsuarioNomeFantasia).Column("DOCUMENTO_USUARIO_DOCUMENTO_NOME");

            Map(item => item.MotivoBloqueioClienteBloqueado).Column("DOCUMENTO_MOTIVO_CLIENTE_BLOQUEADO");
            Map(item => item.MotivoBloqueioDescontoAcima).Column("DOCUMENTO_MOTIVO_DESCONTO_ACIMA");
            Map(item => item.MotivoBloqueioFinanceiroEmAberto).Column("DOCUMENTO_MOTIVO_FINANCEIRO_ABERTO");
            Map(item => item.MotivoBloqueioItemSemEstoque).Column("DOCUMENTO_MOTIVO_ITEM_SEM_ESTOQUE");
            Map(item => item.MotivoBloqueioSemSaldoCredito).Column("DOCUMENTO_MOTIVO_SEM_SALDO_CREDITO");
        }
    }
}
