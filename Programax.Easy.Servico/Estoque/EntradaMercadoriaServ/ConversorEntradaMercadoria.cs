using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Estoque.EntradaMercadoriaServ
{
    public class ConversorEntradaMercadoria : ConversorDeObjetoBasico<EntradaMercadoria>, IConversorDeObjeto<EntradaMercadoria>
    {
        public EntradaMercadoria CopieObjetoParaPersistencia(EntradaMercadoria objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioEntradaMercadoria>();

            var entradaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new EntradaMercadoria();

            var listaDeItens = CopieListaDeItens(objetoDeNegocio, entradaBase);
            var listaFinanceiro = CopieListaFinanceiro(objetoDeNegocio, entradaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, entradaBase);

            entradaBase.ListaDeItens = listaDeItens;
            entradaBase.ListaFinanceiroEntrada = listaFinanceiro;

            return entradaBase;
        }

        private IList<ItemEntrada> CopieListaDeItens(EntradaMercadoria objetoDeNegocio, EntradaMercadoria entradaBase)
        {
            var listaDeItens = entradaBase.ListaDeItens;

            listaDeItens.Clear();

            foreach (var item in objetoDeNegocio.ListaDeItens)
            {
                var itemCopiado = item.CloneCompleto();

                itemCopiado.Id = 0;
                itemCopiado.EntradaMercadoria = entradaBase;
                listaDeItens.Add(itemCopiado);
            }

            return listaDeItens;
        }

        private IList<FinanceiroEntrada> CopieListaFinanceiro(EntradaMercadoria objetoDeNegocio, EntradaMercadoria entradaBase)
        {
            var listaFinanceiro = entradaBase.ListaFinanceiroEntrada;

            listaFinanceiro.Clear();

            foreach (var financeiro in objetoDeNegocio.ListaFinanceiroEntrada)
            {
                var financeiroCopiado = financeiro.CloneCompleto();

                financeiroCopiado.Id = 0;
                financeiroCopiado.EntradaMercadoria = entradaBase;
                listaFinanceiro.Add(financeiroCopiado);
            }

            return listaFinanceiro;
        }
    }
}
