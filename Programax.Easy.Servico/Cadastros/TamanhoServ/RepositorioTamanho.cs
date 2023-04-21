using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Linq.Expressions;
using System;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Cadastros.TamanhoServ
{
    public class RepositorioTamanho : RepositorioBase<Tamanho>, IRepositorioTamanho
    {
        public RepositorioTamanho(ISession sessao)
            : base(sessao)
        {

        }

        public List<Tamanho> ConsulteLista(string descricao, string status)
        {
            Expression<Func<Tamanho, bool>> expressaoParaConsulta = tamanho => tamanho.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(tamanho => tamanho.Status == status);
            }

            return _session.QueryOver<Tamanho>().Where(cor => cor.Descricao.IsLike("%" + descricao + "%")).List<Tamanho>().ToList();
        }


        public List<Tamanho> ConsulteListaAtiva()
        {
            return _session.QueryOver<Tamanho>().Where(tamanho => tamanho.Status == "A").List().ToList();
        }
    }
}
