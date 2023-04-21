using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.Repositorio;
using NHibernate;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using NHibernate.Criterion;
using NHibernate.Transform;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using System.Linq.Expressions;
using NHibernate.SqlCommand;
using System.Data.SqlClient;

namespace Programax.Easy.Servico.Cadastros.EnderecoServ
{
    public class RepositorioEndereco : RepositorioBase<Endereco>, IRepositorioEndereco
    {
        private Cidade _cidadeDaConsulta;

        public RepositorioEndereco(ISession sessao)
            : base(sessao)
        {

        }

        public Endereco Consulte(string cep)
        {
            return _session.QueryOver<Endereco>().Where(endereco => endereco.CEP == cep).SingleOrDefault();
        }

        public List<Endereco> ConsulteLista(string cep, Estado estado, Cidade cidade, string bairro, string rua, string status)
        {
            Expression<Func<Endereco, bool>> expressaoParaConsulta = endereco => endereco.Id != 0;

            if (!(string.IsNullOrEmpty(cep)))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(endereco => endereco.CEP == cep);
            }

            if (estado != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(endereco => _cidadeDaConsulta.Estado.UF == estado.UF);
            }

            if (cidade != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(endereco => _cidadeDaConsulta.Id == cidade.Id);
            }

            if (!bairro.EstahVazio())
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(endereco => endereco.Bairro.IsLike("%" + bairro + "%"));
            }

            if (!rua.EstahVazio())
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(endereco => endereco.Rua.IsLike("%" + rua + "%"));
            }

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(endereco => endereco.Status == status);
            }

            return _session.QueryOver<Endereco>()
                .JoinAlias(endereco => endereco.Cidade, () => _cidadeDaConsulta)
                .Where(expressaoParaConsulta)
                .TransformUsing(Transformers.DistinctRootEntity).List().ToList();
        }

        public Endereco ConsulteAtivo(string cep)
        {
            return _session.QueryOver<Endereco>().Where(endereco => endereco.CEP == cep && endereco.Status == "A").Take(1).SingleOrDefault();
        }
    }
}
