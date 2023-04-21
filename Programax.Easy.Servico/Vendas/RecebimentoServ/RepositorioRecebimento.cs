using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.Repositorio;
using NHibernate;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Vendas.RecebimentoServ
{
    public class RepositorioRecebimento : RepositorioBase<Recebimento>, IRepositorioRecebimento
    {
        public RepositorioRecebimento(ISession sessao)
            : base(sessao)
        {

        }

        public List<Recebimento> ConsulteListaPorDataElaboracao(DateTime dataInicial, DateTime dataFinal, EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento)
        {
            Expression<Func<Recebimento, bool>> expressaoParaConsulta = recebimento => recebimento.Id > 0;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.DataElaboracao >= dataInicial);
            expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.DataElaboracao <= dataFinal);

            if (tipoDocumentoRecebimento != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.TipoDocumento == tipoDocumentoRecebimento);
            }

            return _session.QueryOver<Recebimento>().Where(expressaoParaConsulta).List().ToList();
        }


        public List<Recebimento> ConsulteListaPorDataFechamento(DateTime dataInicial, DateTime dataFinal, EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento)
        {
            Expression<Func<Recebimento, bool>> expressaoParaConsulta = recebimento => recebimento.Id > 0;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.DataFechamento >= dataInicial);
            expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.DataFechamento <= dataFinal);

            if (tipoDocumentoRecebimento != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.TipoDocumento == tipoDocumentoRecebimento);
            }

            return _session.QueryOver<Recebimento>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<RecebimentoNf> ConsulteListaPorDataElaboracaoNf(DateTime dataInicial, DateTime dataFinal, EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento)
        {
            Expression<Func<RecebimentoNf, bool>> expressaoParaConsulta = recebimento => recebimento.Id > 0;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.DataElaboracao >= dataInicial);
            expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.DataElaboracao <= dataFinal);

            if (tipoDocumentoRecebimento != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.TipoDocumento == tipoDocumentoRecebimento);
            }

            return _session.QueryOver<RecebimentoNf>().Where(expressaoParaConsulta).List().ToList();
        }


        public List<RecebimentoNf> ConsulteListaPorDataFechamentoNf(DateTime dataInicial, DateTime dataFinal, EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento)
        {
            Expression<Func<RecebimentoNf, bool>> expressaoParaConsulta = recebimento => recebimento.Id > 0;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.DataFechamento >= dataInicial);
            expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.DataFechamento <= dataFinal);

            if (tipoDocumentoRecebimento != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(x => x.TipoDocumento == tipoDocumentoRecebimento);
            }

            return _session.QueryOver<RecebimentoNf>().Where(expressaoParaConsulta).List().ToList();
        }


        public Recebimento Consulte(int id, EnumTipoDocumentoRecebimento tipoDocumentoRecebimento)
        {
            return _session.QueryOver<Recebimento>().Where(recebimento => recebimento.Id == id && recebimento.TipoDocumento == tipoDocumentoRecebimento).Take(1).SingleOrDefault();
        }

        public RecebimentoNf ConsulteNf(int id, EnumTipoDocumentoRecebimento tipoDocumentoRecebimento)
        {
            return _session.QueryOver<RecebimentoNf>().Where(recebimento => recebimento.Id == id && recebimento.TipoDocumento == tipoDocumentoRecebimento).Take(1).SingleOrDefault();
        }
    }
}
