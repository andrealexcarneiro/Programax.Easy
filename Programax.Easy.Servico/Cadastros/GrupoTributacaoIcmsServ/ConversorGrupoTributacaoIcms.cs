using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.GrupoTributacaoIcmsServ
{
    public class ConversorGrupoTributacaoIcms : ConversorDeObjetoBasico<GrupoTributacaoIcms>, IConversorDeObjeto<GrupoTributacaoIcms>
    {
        public GrupoTributacaoIcms CopieObjetoParaPersistencia(GrupoTributacaoIcms objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioGrupoTributacaoIcms>();

            var grupoTributacaoIcmsDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new GrupoTributacaoIcms();

            var listaDeTributacoesIcms = CopieListaDeTributacoesIcms(objetoDeNegocio, grupoTributacaoIcmsDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, grupoTributacaoIcmsDaBase);

            grupoTributacaoIcmsDaBase.ListaTributacoesIcms = listaDeTributacoesIcms;

            return grupoTributacaoIcmsDaBase;
        }

        private IList<TributacaoIcms> CopieListaDeTributacoesIcms(GrupoTributacaoIcms objetoDeNegocio, GrupoTributacaoIcms grupoTributacaoIcmsBase)
        {
            var listaDeTributacoesIcms = grupoTributacaoIcmsBase.ListaTributacoesIcms;

            listaDeTributacoesIcms.Clear();

            foreach (var item in objetoDeNegocio.ListaTributacoesIcms)
            {
                var itemCopiado = new TributacaoIcms();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.GrupoTributacaoIcms = grupoTributacaoIcmsBase;
                listaDeTributacoesIcms.Add(itemCopiado);
            }

            return listaDeTributacoesIcms;
        }
    }
}
