using NHibernate;
using Programax.Easy.Negocio.Movimentacao.AjusteFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.AjusteFiscalObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Movimentacao.AjusteFiscalServ
{
    public class RepositorioAjusteFiscal: RepositorioBase<AjusteFiscal>, IRepositorioAjusteFiscal
    {
        public RepositorioAjusteFiscal(ISession sessao)
            : base(sessao)
        {  
        }
    }
}
