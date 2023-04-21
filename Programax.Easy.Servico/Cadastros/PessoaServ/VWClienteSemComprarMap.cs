using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class VWClienteSemComprarMap : MapeamentoBase<VWClienteSemComprar>
    {
        public VWClienteSemComprarMap()
        {
            Table("VW_CLIENTES_SEM_COMPRAR");

            ReadOnly();

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("VWCLIENTESC_CLIENTE_ID");

            Map(x => x.CpfCnpj).Column("VWCLIENTESC_CLIENTE_CPF_CNPJ");
            Map(x => x.Nome).Column("VWCLIENTESC_CLIENTE_NOME");
            Map(x => x.Celular).Column("VWCLIENTESC_CLIENTE_CELULAR");
            Map(x => x.Telefone).Column("VWCLIENTESC_CLIENTE_TELEFONE");

            Map(x => x.AtendenteId).Column("VWCLIENTESC_ATENDENTE_ID");
            Map(x => x.Atendente).Column("VWCLIENTESC_ATENDENTE_NOME");

            Map(x => x.VendedorId).Column("VWCLIENTESC_VENDEDOR_ID");
            Map(x => x.Vendedor).Column("VWCLIENTESC_VENDEDOR_NOME");

            Map(x => x.DataUltimoPedido).Column("VWCLIENTESC_DATA_ULTIMO_PEDIDO");
            Map(x => x.ValorUltimoPedido).Column("VWCLIENTESC_VALOR_ULTIMO_PEDIDO");
            Map(x => x.DiasSemComprar).Column("VWCLIENTESC_DIAS_SEM_COMPRAR");

            Map(x => x.JahComprou).Column("VWCLIENTESC_JAH_COMPROU");

            HasMany(x => x.ListaDeEnderecos).KeyColumn("VWENDPES_PES_ID").Table("VW_ENDERECOS_PESSOAS").Not.LazyLoad().Fetch.Join();
        }
    }
}
