using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Financeiro.ChequeServ
{
    public class ChequeMap : MapeamentoBase<Cheque>
    {
        public ChequeMap()
        {
            Table("CHEQUES");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CHQ_ID");

            Map(cheques => cheques.CpfCnpj).Column("CHQ_CPF_CNPJ");
            Map(cheques => cheques.DataCadastro).Column("CHQ_DATA_CADASTRO");
            Map(cheques => cheques.DataEmissao).Column("CHQ_DATA_EMISSAO");
            Map(cheques => cheques.DataVencimento).Column("CHQ_DATA_VENCIMENTO");
            Map(cheques => cheques.NomeCheque).Column("CHQ_NOME_CHEQUE");
            Map(cheques => cheques.Observacoes).Column("CHQ_OBSERVACOES");
            Map(cheques => cheques.StatusCheque).Column("CHQ_STATUS").CustomType<EnumStatusCheque>();
            Map(cheques => cheques.ValorCheque).Column("CHQ_VALOR_CHEQUE");

            Map(cheques => cheques.EhCnpj).Column("CHQ_EH_CNPJ");
            Map(cheques => cheques.DataRecebimento).Column("CHQ_DATA_RECEBIMENTO");
            Map(cheques => cheques.Agencia).Column("CHQ_AGENCIA");
            Map(cheques => cheques.Conta).Column("CHQ_CONTA");
            Map(cheques => cheques.Digito).Column("CHQ_DIGITO");
            Map(cheques => cheques.Serie).Column("CHQ_SERIE");
            Map(cheques => cheques.NumeroCheque).Column("CHQ_NUMERO_CHEQUE");
            Map(cheques => cheques.NumeroPedidoVenda).Column("CHQ_PEDIDO_ID");
            Map(cheques => cheques.NumeroDocumento).Column("CHQ_NUMERO_DOCUMENTO");

            References(cheques => cheques.Pessoa).Column("CHQ_PESSOA_ID");
            References(banco => banco.Banco).Column("CHQ_BANCO_ID");
                       
            HasMany(cheques => cheques.ListaVinculosDePedidos).KeyColumn("VINC_CHEQUE_ID").Cascade.AllDeleteOrphan().Inverse().AsBag();
        }
    }
}
