using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.ChequeObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.VincularChequePedidosObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.ChequeServ
{
    public class ConversorCheque : ConversorDeObjetoBasico<Cheque>, IConversorDeObjeto<Cheque>
    {
        public Cheque CopieObjetoParaPersistencia(Cheque objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCheque>();

            var chequeDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Cheque();

           var listaDeVinculos = CopieListaVinculos(objetoDeNegocio, chequeDaBase);
            
            CopieTodasAsPropriedades(objetoDeNegocio, chequeDaBase);

            chequeDaBase.ListaVinculosDePedidos = listaDeVinculos;

            return chequeDaBase;
        }

        private IList<VincularChequePedidos> CopieListaVinculos(Cheque objetoDeNegocio, Cheque chequeBase)
        {
            var listaVinculos = chequeBase.ListaVinculosDePedidos;

            listaVinculos.Clear();

            foreach (var item in objetoDeNegocio.ListaVinculosDePedidos)
            {
                var itemCopiado = new VincularChequePedidos();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.Cheque = chequeBase;
                listaVinculos.Add(itemCopiado);
            }

            return listaVinculos;
        }
    }
}
