using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Servico.Vendas.RoteiroServ
{
    public class RoteirizacaoMap : MapeamentoBase<Roteirizacao>
    {
        public RoteirizacaoMap()
        {
            Table("ROTEIRIZACAO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ROTEIRO_ID");

            Map(roteiro => roteiro.DataCriacao).Column("ROTEIRO_DATA_CRIACAO");
            Map(roteiro => roteiro.DataConclusao).Column("ROTEIRO_DATA_CONCLUSAO");
            Map(roteiro => roteiro.Status).Column("ROTEIRO_STATUS").CustomType<EnumStatusRoteiro>();
            
            References(roteiro => roteiro.PessoaFuncionario).Column("ROTEIRO_PES_FUNC_ID").Not.LazyLoad().Fetch.Join();

            References(roteiro => roteiro.Usuario).Column("ROTEIRO_PES_USUARIO_ID"); 
        }
    }
}
