using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using System.Collections.Generic;

namespace Programax.Easy.Servico.Cadastros.GrupoTributacaoFederalServ
{
    public class ConversorGrupoTributacaoFederal : ConversorDeObjetoBasico<GrupoTributacaoFederal>, IConversorDeObjeto<GrupoTributacaoFederal>
    {
        public GrupoTributacaoFederal CopieObjetoParaPersistencia(GrupoTributacaoFederal objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioGrupoTributacaoFederal>();

            var grupoTributacaoFederalDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new GrupoTributacaoFederal();

            var listaDeTributacoesFederalCofins = CopieListaDeTributacoesCofins(objetoDeNegocio, grupoTributacaoFederalDaBase);
            var listaDeTributacoesFederalPis = CopieListaDeTributacoesPis(objetoDeNegocio, grupoTributacaoFederalDaBase);
            var listaDeTributacoesFederalIpi = CopieListaDeTributacoesIpi(objetoDeNegocio, grupoTributacaoFederalDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, grupoTributacaoFederalDaBase);

            grupoTributacaoFederalDaBase.ListaCofins = listaDeTributacoesFederalCofins;
            grupoTributacaoFederalDaBase.ListaPis = listaDeTributacoesFederalPis;
            grupoTributacaoFederalDaBase.ListaIpi = listaDeTributacoesFederalIpi;

            return grupoTributacaoFederalDaBase;
        }
        
        private IList<CofinsNotaFiscal> CopieListaDeTributacoesCofins(GrupoTributacaoFederal objetoDeNegocio, GrupoTributacaoFederal grupoTributacaoFederalBase)
        {
            var listaDeTributacoesCofins = grupoTributacaoFederalBase.ListaCofins;

            listaDeTributacoesCofins.Clear();

            foreach (var item in objetoDeNegocio.ListaCofins)
            {
                var itemCopiado = new CofinsNotaFiscal();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.GrupoTributacaoFederal = grupoTributacaoFederalBase;
                listaDeTributacoesCofins.Add(itemCopiado);
            }

            return listaDeTributacoesCofins;
        }

        private IList<PisNotaFiscal> CopieListaDeTributacoesPis(GrupoTributacaoFederal objetoDeNegocio, GrupoTributacaoFederal grupoTributacaoFederalBase)
        {
            var listaDeTributacoesPis = grupoTributacaoFederalBase.ListaPis;

            listaDeTributacoesPis.Clear();

            foreach (var item in objetoDeNegocio.ListaPis)
            {
                var itemCopiado = new PisNotaFiscal();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.GrupoTributacaoFederal = grupoTributacaoFederalBase;
                listaDeTributacoesPis.Add(itemCopiado);
            }

            return listaDeTributacoesPis;
        }

        private IList<IpiNotaFiscal> CopieListaDeTributacoesIpi(GrupoTributacaoFederal objetoDeNegocio, GrupoTributacaoFederal grupoTributacaoFederalBase)
        {
            var listaDeTributacoesIpi = grupoTributacaoFederalBase.ListaIpi;

            listaDeTributacoesIpi.Clear();

            foreach (var item in objetoDeNegocio.ListaIpi)
            {
                var itemCopiado = new IpiNotaFiscal();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.GrupoTributacaoFederal = grupoTributacaoFederalBase;
                listaDeTributacoesIpi.Add(itemCopiado);
            }

            return listaDeTributacoesIpi;
        }
    }
}
