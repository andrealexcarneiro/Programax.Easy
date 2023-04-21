using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ
{
    public class ConversorNotaFiscal : ConversorDeObjetoBasico<NotaFiscal>, IConversorDeObjeto<NotaFiscal>
    {
        public NotaFiscal CopieObjetoParaPersistencia(NotaFiscal objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioNotaFiscal>();

            var notaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new NotaFiscal();

            objetoDeNegocio = objetoDeNegocio.CloneCompleto();

            var listaDeItens = CopieListaDeItens(objetoDeNegocio, notaDaBase);

            var listaDeDuplicatas = CopieListaDeDuplicatas(objetoDeNegocio, notaDaBase);

            var listaNotasReferenciadas = CopieListaDeNotasReferenciadas(objetoDeNegocio, notaDaBase);

            var listaDeFormasPagamento = CopieListaDeFormasPagamento(objetoDeNegocio, notaDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, notaDaBase);

            notaDaBase.ListaItens = listaDeItens;

            notaDaBase.DadosCobranca.ListaDuplicatas = listaDeDuplicatas;

            notaDaBase.ListaNotasReferenciadas = listaNotasReferenciadas;

            notaDaBase.ListaFormasPagamentoNFCe = listaDeFormasPagamento;

            return notaDaBase;
        }

        private IList<DuplicataNotaFiscal> CopieListaDeDuplicatas(NotaFiscal objetoDeNegocio, NotaFiscal notaBase)
        {
            var listaDeDuplicatas = notaBase.DadosCobranca.ListaDuplicatas;

            listaDeDuplicatas.Clear();

            foreach (var duplicata in objetoDeNegocio.DadosCobranca.ListaDuplicatas)
            {
                var itemCopiado = new DuplicataNotaFiscal();

                CopieTodasAsPropriedades(duplicata, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.NotaFiscal = notaBase;
                listaDeDuplicatas.Add(itemCopiado);
            }

            return listaDeDuplicatas;
        }

        private IList<ItemNotaFiscal> CopieListaDeItens(NotaFiscal objetoDeNegocio, NotaFiscal notaBase)
        {
            var listaDeItens = notaBase.ListaItens;

            listaDeItens.Clear();

            foreach (var item in objetoDeNegocio.ListaItens)
            {
                var itemCopiado = new ItemNotaFiscal();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.NotaFiscal = notaBase;
                listaDeItens.Add(itemCopiado);
            }

            return listaDeItens;
        }

        private IList<FormaPagamentoNotaFiscal> CopieListaDeFormasPagamento(NotaFiscal objetoDeNegocio, NotaFiscal notaBase)
        {
            var listaFormasPagamento = notaBase.ListaFormasPagamentoNFCe;

            listaFormasPagamento.Clear();

            foreach (var item in objetoDeNegocio.ListaFormasPagamentoNFCe)
            {
                var itemCopiado = new FormaPagamentoNotaFiscal();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.NotaFiscal = notaBase;
                listaFormasPagamento.Add(itemCopiado);
            }

            return listaFormasPagamento;
        }

        private IList<NotaFiscalReferenciada> CopieListaDeNotasReferenciadas(NotaFiscal objetoDeNegocio, NotaFiscal notaBase)
        {
            var listaDeNotasReferenciadas = notaBase.ListaNotasReferenciadas;

            listaDeNotasReferenciadas.Clear();

            foreach (var item in objetoDeNegocio.ListaNotasReferenciadas)
            {
                var itemCopiado = new NotaFiscalReferenciada();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.NotaFiscal = notaBase;
                listaDeNotasReferenciadas.Add(itemCopiado);
            }

            return listaDeNotasReferenciadas;
        }
    }
}
