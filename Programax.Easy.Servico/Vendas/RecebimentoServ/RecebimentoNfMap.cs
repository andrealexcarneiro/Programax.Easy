using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Vendas.RecebimentoServ
{
    public class RecebimentoNfMap : MapeamentoBase<RecebimentoNf>
    {
        public RecebimentoNfMap()
        {
            Table("VW_RECEBIMENTO_NF");
            ReadOnly();

            CompositeId()
                        .KeyProperty(x => x.Id, "NUMERO_DOCUMENTO")
                        .KeyProperty(x => x.TipoDocumento, "DOCUMENTO_TIPO_DOCUMENTO").CustomType<EnumTipoDocumentoRecebimento>();

            Map(item => item.ClienteId).Column("DOCUMENTO_CLIENTE_ID");
            Map(item => item.StatusDocumento).Column("DOCUMENTO_STATUS").CustomType<EnumStatusPedidoDeVenda>();
            Map(item => item.NaturezaId).Column("DOCUMENTO_NATUREZA_OPERACAO_ID");
            Map(item => item.NaturezaDescricao).Column("DOCUMENTO_NATUREZA_OPERACAO_DESCRICAO");
            Map(item => item.ClienteNomeFantasia).Column("DOCUMENTO_CLIENTE_NOME_FANTASIA");
            Map(item => item.EnderecoCep).Column("DOCUMENTO_ENDERECO_CEP");
            Map(item => item.CidadeId).Column("DOCUMENTO_ENDERECO_CIDADE_ID");
            Map(item => item.CidadeDescricao).Column("DOCUMENTO_ENDERECO_CIDADE_DESCRICAO");
            Map(item => item.EstadoUf).Column("DOCUMENTO_ENDERECO_UF");
            Map(item => item.EstadoNome).Column("DOCUMENTO_ENDERECO_NOME");
            Map(item => item.ClienteInscricaoEstadual).Column("DOCUMENTO_CLIENTE_INSC_ESTADUAL");
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

            HasMany(pessoa => pessoa.ListaParcelasRecebimento).KeyColumns.Add("PARCELA_RECEBIMENTO_ID", "PARCELA_TIPO_DOCUMENTO").AsBag();
        }
    }
}
