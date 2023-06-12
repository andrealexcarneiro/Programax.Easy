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
    public class RepositorioRoteiro : RepositorioBase<Roteiro>, IRepositorioRoteiro
    {
        public RepositorioRoteiro(ISession sessao)
            : base(sessao)
        {

        }

        public Roteiro Consulte(int id, EnumPeriodo periodo)
        {
            return _session.QueryOver<Roteiro>().Where(rota => rota.Id == id && rota.Periodo == periodo).SingleOrDefault();
        }

        public Roteiro ConsultePorPedido(int pedidoId)
        {
            return _session.QueryOver<Roteiro>().Where(rota => rota.PedidoVenda.Id == pedidoId).SingleOrDefault();
        }

        public List<Roteiro> ConsulteLista(Pessoa pessoa,
                                                                           EnumPeriodo? periodo,
                                                                           EnumStatusRoteiro? statusRoteiro,                                                                          
                                                                           EnumDataFiltrarRoteiro? tipoDataFiltrar,
                                                                           DateTime? dataInicialPeriodo,
                                                                           DateTime? dataFinalPeriodo,
                                                                           int? idPedido, 
                                                                           int? idRoteiro,
                                                                           bool buscarConcluidos=true)
        {
            Expression<Func<Roteiro, bool>> expressaoParaConsulta = roteiro => roteiro.Id > 0;

            if (pessoa != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.PessoaFuncionario.Id == pessoa.Id);
            }

            if (periodo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.Periodo == periodo);
            }

            if (statusRoteiro != null)
            {
                if(buscarConcluidos)
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.Status == statusRoteiro);
                else
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.Status == statusRoteiro && 
                                                                            roteiro.Status != EnumStatusRoteiro.CONCLUIDO);
            }

            if (tipoDataFiltrar != null)
            {
                if (dataInicialPeriodo != null)
                {
                    if (tipoDataFiltrar == EnumDataFiltrarRoteiro.ELABORACAO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.DataElaboracao >= dataInicialPeriodo.GetValueOrDefault());
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
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.DataElaboracao <= dataFinalPeriodo.Value.AddHours(23).AddMinutes(59).AddSeconds(59));
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarRoteiro.CONCLUSAO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.DataConclusao <= dataFinalPeriodo.Value.AddHours(23).AddMinutes(59));
                    }
                }

                if(idPedido != 0)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.PedidoVenda.Id == idPedido);
                }

                if (idRoteiro != 0)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(roteiro => roteiro.RoteirizacaoId == idRoteiro);
                }

            }

            return _session.QueryOver<Roteiro>()
                 .TransformUsing(Transformers.DistinctRootEntity)
                 .Where(expressaoParaConsulta).List().ToList();
        }

        public Roteiro ConsultePeloNumeroPedidoParceiroEDataElaboracao(int PedidoId, Pessoa parceiro, DateTime dataElaboracao, EnumPeriodo periodo)
        {
            return _session.QueryOver<Roteiro>().Where(roteiro => roteiro.PedidoVenda.Id == PedidoId &&
                                                                                                                                        roteiro.PessoaFuncionario.Id == parceiro.Id &&
                                                                                                                                        roteiro.DataElaboracao == dataElaboracao &&
                                                                                                                                        roteiro.Periodo == periodo).SingleOrDefault();
        }

        public List<Roteiro> ConsulteListaPorRoteirizacao(int roteirizacaoId)
        {
            Expression<Func<Roteiro, bool>> expressaoParaConsulta = roteiro => roteiro.RoteirizacaoId == roteirizacaoId;

            return _session.QueryOver<Roteiro>()
                 .TransformUsing(Transformers.DistinctRootEntity)
                 .Where(expressaoParaConsulta).List().ToList();
        }
    }
}
