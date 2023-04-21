using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using System.Collections.Generic;
using System;

namespace Programax.Easy.Servico.Movimentacao.MovimentacaoServ
{
    public class ConversorMovimentacao : ConversorDeObjetoBasico<MovimentacaoBase>, IConversorDeObjeto<MovimentacaoBase>
    {
        public virtual MovimentacaoBase CopieObjetoParaPersistencia(MovimentacaoBase objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioMovimentacao>();

            var movimentacaoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new MovimentacaoBase();

            var listaDeItens = CopieListaDeItens(objetoDeNegocio, movimentacaoDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, movimentacaoDaBase);

            movimentacaoDaBase.ListaDeItens = listaDeItens;

            return movimentacaoDaBase;
        }

        protected IList<ItemMovimentacao> CopieListaDeItens(MovimentacaoBase objetoDeNegocio, MovimentacaoBase movimentacaoDaBase)
        {
            var listaDeItens = movimentacaoDaBase.ListaDeItens;

            listaDeItens.Clear();

            foreach (var item in objetoDeNegocio.ListaDeItens)
            {
                var itemCopiado = new ItemMovimentacao();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.MovimentacaoBase = movimentacaoDaBase;
                listaDeItens.Add(itemCopiado);
            }

            return listaDeItens;
        }
    }
}
