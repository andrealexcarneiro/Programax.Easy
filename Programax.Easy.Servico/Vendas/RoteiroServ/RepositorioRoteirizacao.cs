using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate.Transform;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Servico.Vendas.RoteiroServ
{
    public class RepositorioRoteirizacao : RepositorioBase<Roteirizacao>, IRepositorioRoteirizacao
    {
        public RepositorioRoteirizacao(ISession sessao)
            : base(sessao)
        {

        }

        public List<Roteirizacao> ConsulteLista(Pessoa pessoa, EnumStatusRoteiro? statusRoteiro, EnumDataFiltrarRoteiro?
                                    tipoDataFiltrar, DateTime? dataInicialPeriodo, DateTime? dataFinalPeriodo)
        {
            Expression<Func<Roteirizacao, bool>> expressaoParaConsulta = roteiro => roteiro.Id > 0;

            if (pessoa != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.PessoaFuncionario.Id == pessoa.Id);
            }

            if (statusRoteiro != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.Status == statusRoteiro);
            }

            if (tipoDataFiltrar != null)
            {
                if (dataInicialPeriodo != null)
                {
                    if (tipoDataFiltrar == EnumDataFiltrarRoteiro.ELABORACAO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.DataCriacao >= dataInicialPeriodo.GetValueOrDefault());
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarRoteiro.CONCLUSAO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.DataConclusao >= dataInicialPeriodo.GetValueOrDefault());
                    }

                }

                if (dataFinalPeriodo != null)
                {
                    if (tipoDataFiltrar == EnumDataFiltrarRoteiro.ELABORACAO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.DataCriacao <= dataFinalPeriodo.Value.AddHours(23).AddMinutes(59).AddSeconds(59));
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarRoteiro.CONCLUSAO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.DataConclusao <= dataFinalPeriodo.Value.AddHours(23).AddMinutes(59));
                    }
                }
            }

            return _session.QueryOver<Roteirizacao>()
                 .TransformUsing(Transformers.DistinctRootEntity)
                 .Where(expressaoParaConsulta).List().ToList();
        }

        public List<Roteirizacao> ConsulteListaCodigoRoteiro(int? numeroRoteiro)
        {
            Expression<Func<Roteirizacao, bool>> expressaoParaConsulta = roteiro => roteiro.Id == numeroRoteiro;

            return _session.QueryOver<Roteirizacao>()
                 .TransformUsing(Transformers.DistinctRootEntity)
                 .Where(expressaoParaConsulta).List().ToList();
        }
    }
}
