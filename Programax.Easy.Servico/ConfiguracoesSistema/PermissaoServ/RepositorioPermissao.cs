using System.Linq;
using NHibernate;
using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate.Transform;
using System.Collections.Generic;
using System;
using System.Transactions;

namespace Programax.Easy.Servico.ConfiguracoesSistema.PermissaoServ
{
    public class RepositorioPermissao : RepositorioBase<Permissao>, IRepositorioPermissao
    {
        public RepositorioPermissao(ISession sesao)
            : base(sesao)
        {
        }

        public List<Permissao> ConsulteLista(int idGrupoAcesso)
        {
            return _session.QueryOver<Permissao>().Where(permissao => permissao.GrupoAcesso.Id == idGrupoAcesso).List().ToList();
        }

        public override void CadastreLista(List<Permissao> listaObjetos)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    var idGrupoAcesso = listaObjetos.FirstOrDefault().GrupoAcesso.Id;

                    _session.CreateSQLQuery("DELETE FROM PERMISSOES WHERE PERM_GRPACESSO_ID = " + idGrupoAcesso).ExecuteUpdate();

                    base.CadastreLista(listaObjetos);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _session.Clear();
                }
            }
        }
    }
}
