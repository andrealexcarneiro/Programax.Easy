using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.Repositorio;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ
{
    public class RepositorioNotaFiscal : RepositorioBase<NotaFiscal>, IRepositorioNotaFiscal
    {
        public RepositorioNotaFiscal(ISession sessao)
            : base(sessao)
        {

        }

        public List<VwNotasDocumentos> ConsulteListaVwNotasDocumentos(int? numeroDocumento,
                                                                                                            DateTime? dataInicial,
                                                                                                            DateTime? dataFinal,
                                                                                                            EnumTipoDocumento? tipoDocumento,
                                                                                                            EnumStatusNotaFiscal? statusNotaFiscal, EnumModeloNotaFiscal? modelo)
        {
            Expression<Func<VwNotasDocumentos, bool>> expressaoParaConsulta = vwnota => vwnota.Id >= 0;

            if (numeroDocumento != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.NumeroDocumento == numeroDocumento.Value);
            }
            if (statusNotaFiscal == EnumStatusNotaFiscal.DISPONIVEL)
            {
                if (dataInicial != null)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.DataElaboracao >= dataInicial.Value);
                }

                if (dataFinal != null)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.DataElaboracao <= dataFinal.Value);
                }
            }
            else
            {
                if (dataInicial != null)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.DataEmissao >= dataInicial.Value);
                }

                if (dataFinal != null)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.DataEmissao <= dataFinal.Value);
                }
            }

            if (tipoDocumento != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.TipoDocumento == tipoDocumento.Value);
            }

            if (statusNotaFiscal != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.StatusNotaFiscal == statusNotaFiscal);
            }

            if (modelo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.Modelo == modelo);
            }

            return _session.QueryOver<VwNotasDocumentos>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<NotaFiscal> ConsulteListaDocumentos(int? numeroDocumento,
                                                                                                            DateTime? dataInicial,
                                                                                                            DateTime? dataFinal,
                                                                                                            EnumTipoDocumento? tipoDocumento,
                                                                                                            EnumStatusNotaFiscal? statusNotaFiscal, EnumModeloNotaFiscal? modelo, EnumTipoDeEmissaoPesquisa? tipoEmissao, int? numeroNF = null)
        {            
            Expression<Func<NotaFiscal, bool>> expressaoParaConsulta = vwnota => vwnota.Id >= 0;

            if (numeroDocumento != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.InformacoesDocumentoOrigemNotaFiscal.DocumentoId == numeroDocumento);
            }
            if (statusNotaFiscal == EnumStatusNotaFiscal.DISPONIVEL)
            {
                if (dataInicial != null)
                {
                    
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.InformacoesDocumentoOrigemNotaFiscal.DataElaboracao >= dataInicial.Value);
                }

                if (dataFinal != null)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.InformacoesDocumentoOrigemNotaFiscal.DataElaboracao <= dataFinal.Value);
                }
            }
            else
            {
                if (dataInicial != null)
                {   
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.InformacoesGeraisNotaFiscal.DataCadastro >= dataInicial.Value);
                }

                if (dataFinal != null)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.InformacoesGeraisNotaFiscal.DataCadastro <= dataFinal.Value);
                }
            }

            if (tipoDocumento != null)
            {
                 expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.InformacoesDocumentoOrigemNotaFiscal.Origem == tipoDocumento.Value);
            }

            if (statusNotaFiscal != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.InformacoesGeraisNotaFiscal.Status == statusNotaFiscal);
            }

            if (modelo != null)
            {                
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == modelo.GetHashCode().ToInt());
            }

            if (tipoEmissao != null)
            {
                if (tipoEmissao == EnumTipoDeEmissaoPesquisa.CONTIGENCIA)
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.IdentificacaoNotaFiscal.TipoEmissaoDanfe == EnumTipoEmissaoDanfe.CONTINGENCIAOFFLINE);
            }

            if (numeroNF != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwnota => vwnota.IdentificacaoNotaFiscal.NumeroNota == numeroNF);
            }

            return _session.QueryOver<NotaFiscal>().Where(expressaoParaConsulta).List().ToList();                        
        }

        public NotaFiscal Consulte(int serie, int numero, EnumStatusNotaFiscal? status, EnumModeloNotaFiscal modelo = EnumModeloNotaFiscal.NFE)
        {
            int modeloNF;
            modeloNF = modelo == EnumModeloNotaFiscal.NFE ? 55 : 65;

            if (status != null)
                return _session.QueryOver<NotaFiscal>().Where
                                                            (nota => nota.IdentificacaoNotaFiscal.Serie == serie &&
                                                            nota.IdentificacaoNotaFiscal.NumeroNota == numero
                                                            && nota.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == modeloNF &&
                                                            nota.InformacoesGeraisNotaFiscal.Status == status).Take(1).SingleOrDefault();
            else
                return _session.QueryOver<NotaFiscal>().Where
                                                            (nota => nota.IdentificacaoNotaFiscal.Serie == serie &&
                                                            nota.IdentificacaoNotaFiscal.NumeroNota == numero &&
                                                            nota.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == modeloNF).Take(1).SingleOrDefault();
        }

        public List<NotaFiscal> ConsulteListaComJoinItens(List<int> listaIds)
        {
            return _session.QueryOver<NotaFiscal>().Where(nota => nota.Id.IsIn(listaIds)).List().ToList();
        }
    }
}
