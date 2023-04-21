using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.CrediarioServ
{
    public class CrediarioMap : MapeamentoBase<Crediario>
    {
        public CrediarioMap()
        {
            Table("ANALISESCREDITOS");

            Id(x => x.Id).GeneratedBy.Assigned().Column("ANALISE_ID");

            Map(condicao => condicao.DataUltimaAlteracao).Column("ANALISE_DATA_ULTIMA_ALTERACAO");
            Map(condicao => condicao.PodeAlterarMultaEJuros).Column("ANALISE_PODE_ALTERAR_MULTA_JUROS");
            Map(condicao => condicao.StatusAnaliseCredito).Column("ANALISE_STATUS_ANALISE_CREDITO").CustomType<EnumStatusCrediario>();
            Map(condicao => condicao.ValorLimiteCredito).Column("ANALISE_VALOR_LIMITE_CREDITO");
            Map(condicao => condicao.DataValidade).Column("ANALISE_CREDITO_VALIDADE");
            

            References(condicao => condicao.CondicaoPagamento).Column("ANALISE_CONDICAO_PAGAMENTO_ID");
            References(condicao => condicao.FormaPagamento).Column("ANALISE_FORMA_PAGAMENTO_ID");
            References(condicao => condicao.Pessoa).Column("ANALISE_PESSOA_ID");
            References(condicao => condicao.TabelaPreco).Column("ANALISE_TABELA_PRECO_ID");
            References(condicao => condicao.UsuarioUltimaAlteracao).Column("ANALISE_USUARIO_ULTIMA_ALTERACAO_ID");
        }
    }
}
