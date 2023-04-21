using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.Repositorio;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Integracao.PreVendaDjpdvServ
{
    public class RepositorioPreVendaDjpdv : RepositorioBase<PreVendaDjpdv>, IRepositorioPreVendaDjpdv
    {
        public RepositorioPreVendaDjpdv(ISession sessao)
            : base(sessao)
        {

        }

        public void ExcluaLista(List<PreVendaDjpdv> atualizacoes)
        {
            if (atualizacoes.Count > 0)
            {
                string exclusoes = string.Empty;

                exclusoes += "DELETE FROM PREVENDADJPDV WHERE PREVENDA_ID IN(" + string.Join(",", atualizacoes.Select<PreVendaDjpdv, int>(x => x.Id)) + ");";

                _session.CreateSQLQuery(exclusoes).ExecuteUpdate();
            }
        }
    }
}
