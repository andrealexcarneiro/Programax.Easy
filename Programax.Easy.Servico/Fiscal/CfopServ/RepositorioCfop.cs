using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.CfopServ
{
    public class RepositorioCfop : RepositorioBase<Cfop>, IRepositorioCfop
    {
        public RepositorioCfop(ISession sessao)
            : base(sessao)
        {
        }

        public List<Cfop> ConsulteLista(string codigoCfop, string descricao, string status, EnumTipoCfop tipoCfop)
        {
            Expression<Func<Cfop, bool>> expressaoParaConsulta = cfop => cfop.Descricao.IsLike("%" + descricao + "%") && cfop.Codigo.IsLike("%" + codigoCfop + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cfop => cfop.Status == status);
            }

            if (tipoCfop != EnumTipoCfop.ENTRADAESAIDA)
            {
                if (tipoCfop == EnumTipoCfop.ENTRADA)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(cfop => (cfop.Codigo.IsLike("1%") || cfop.Codigo.IsLike("2%") || cfop.Codigo.IsLike("3%")));
                }
                else
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(cfop => (cfop.Codigo.IsLike("5%") || cfop.Codigo.IsLike("6%") || cfop.Codigo.IsLike("7%")));
                }
            }

            return _session.QueryOver<Cfop>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Cfop> ConsulteListaAtiva(EnumOrigemDestino origemDestino, EnumTipoMovimentacaoNaturezaOperacao tipoMovimentacaoNaturezaOperacao)
        {
            Expression<Func<Cfop, bool>> expressaoParaConsulta = cfop => cfop.Status == "A";

            if (origemDestino == EnumOrigemDestino.ESTADUAL && tipoMovimentacaoNaturezaOperacao == EnumTipoMovimentacaoNaturezaOperacao.ENTRADA)
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cfop => cfop.Codigo.IsLike("1%"));
            else if (origemDestino == EnumOrigemDestino.INTERESTADUAL && tipoMovimentacaoNaturezaOperacao == EnumTipoMovimentacaoNaturezaOperacao.ENTRADA)
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cfop => cfop.Codigo.IsLike("2%"));
            else if (origemDestino == EnumOrigemDestino.EXTERIOR && tipoMovimentacaoNaturezaOperacao == EnumTipoMovimentacaoNaturezaOperacao.ENTRADA)
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cfop => cfop.Codigo.IsLike("3%"));

            else if (origemDestino == EnumOrigemDestino.ESTADUAL && tipoMovimentacaoNaturezaOperacao == EnumTipoMovimentacaoNaturezaOperacao.SAIDA)
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cfop => cfop.Codigo.IsLike("5%"));
            else if (origemDestino == EnumOrigemDestino.INTERESTADUAL && tipoMovimentacaoNaturezaOperacao == EnumTipoMovimentacaoNaturezaOperacao.SAIDA)
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cfop => cfop.Codigo.IsLike("6%"));
            else if (origemDestino == EnumOrigemDestino.EXTERIOR && tipoMovimentacaoNaturezaOperacao == EnumTipoMovimentacaoNaturezaOperacao.SAIDA)
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cfop => cfop.Codigo.IsLike("7%"));

            return _session.QueryOver<Cfop>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Cfop> ConsulteListaAtiva()
        {
            return _session.QueryOver<Cfop>().Where(cfop => cfop.Status == "A").List().ToList();
        }

        public Cfop ConsultePeloCodigoCfop(string codigoCfop)
        {
            return _session.QueryOver<Cfop>().Where(cfop => cfop.Codigo == codigoCfop).Take(1).SingleOrDefault();
        }

        public List<Cfop> ConsulteListaDeCodigosCfop(List<string> listaCodigosCfop)
        {
            return _session.QueryOver<Cfop>().Where(cfop => cfop.Codigo.IsIn(listaCodigosCfop)).List().ToList();
        }
    }
}
