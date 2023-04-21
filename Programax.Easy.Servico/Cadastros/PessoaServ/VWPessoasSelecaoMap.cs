using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class VWPessoasSelecaoMap : MapeamentoBase<VWPessoasSelecao>
    {
        public VWPessoasSelecaoMap()
        {
            Table("VW_PESSOAS_SELECAO");

            ReadOnly();

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PES_ID");
                      
            Map(x => x.Razao).Column("PES_RAZAO");
        }
    }
}
