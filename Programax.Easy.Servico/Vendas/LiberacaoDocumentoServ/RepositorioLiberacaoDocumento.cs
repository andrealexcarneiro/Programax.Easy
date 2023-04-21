using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Transform;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.LiberacaoDocumentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.LiberacaoDocumentoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Vendas.LiberacaoDocumentoServ
{
    public class RepositorioLiberacaoDocumento : RepositorioBase<LiberacaoDocumento>, IRepositorioLiberacaoDocumento
    {
        public RepositorioLiberacaoDocumento(ISession sessao)
            : base(sessao)
        {

        }

        public LiberacaoDocumento Consulte(int id, EnumTipoDocumentoLiberacao tipoDocumentoLiberacao)
        {
            return _session.QueryOver<LiberacaoDocumento>().Where(liberacao => liberacao.Id == id && liberacao.TipoDocumento == tipoDocumentoLiberacao).Take(1).SingleOrDefault();
        }

        public List<LiberacaoDocumento> ConsulteLista(DateTime? dataInicial, DateTime? dataFinal, Pessoa atendente, Pessoa vendedor, EnumTipoDocumentoLiberacao? tipoDocumentoLiberacao)
        {
            Expression<Func<LiberacaoDocumento, bool>> expressaoParaConsulta = liberacao => liberacao.Id > 0;

            if (dataInicial != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(liberacao => liberacao.DataElaboracao >= dataInicial.Value);
            }

            if (dataFinal != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(liberacao => liberacao.DataElaboracao <= dataFinal.Value);
            }

            if (atendente != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(liberacao => liberacao.AtendenteId == atendente.Id);
            }

            if (vendedor != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(liberacao => liberacao.VendedorId == vendedor.Id);
            }

            if (tipoDocumentoLiberacao != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(liberacao => liberacao.TipoDocumento == tipoDocumentoLiberacao.Value);
            }

            return _session.QueryOver<LiberacaoDocumento>()
                                        .TransformUsing(Transformers.DistinctRootEntity)
                                        .Where(expressaoParaConsulta).List().ToList();
        }
    }
}
