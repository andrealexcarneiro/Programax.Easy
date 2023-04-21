using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using System.Linq;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.TransferenciaServ
{
    public class ConversorTransferencia : ConversorDeObjetoBasico<Transferencia>, IConversorDeObjeto<Transferencia>
    {
        public Transferencia CopieObjetoParaPersistencia(Transferencia objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioTransferencia>();

            var transferenciaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Transferencia();

            //var listaDeItens = CopieListaDeItens(objetoDeNegocio, transferenciaDaBase);

            CopiePropriedadesEspecificas(objetoDeNegocio, transferenciaDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, transferenciaDaBase);

            //transferenciaDaBase.ListaDeItens = listaDeItens;

            return transferenciaDaBase;
        }

        //protected IList<ItemTransferencia> CopieListaDeItens(Transferencia objetoDeNegocio, Transferencia transferenciaDaBase)
        //{
        //    var listaDeItens = transferenciaDaBase.ListaDeItens;

        //    foreach (var item in objetoDeNegocio.ListaDeItens)
        //    {
        //        ItemTransferencia itemTransferencia = null;

        //        if (item.Id > 0)
        //        {
        //            itemTransferencia = listaDeItens.FirstOrDefault(itemDaLista => itemDaLista.Id == item.Id);
        //        }

        //        itemTransferencia = itemTransferencia ?? new ItemTransferencia();

        //        CopieTodasAsPropriedades(item, itemTransferencia);

        //        itemTransferencia.Transferencia = transferenciaDaBase;

        //        if (itemTransferencia.Id == 0)
        //        {
        //            listaDeItens.Add(itemTransferencia);
        //        }
        //    }

        //    return listaDeItens;
        //}

        private void CopiePropriedadesEspecificas(Transferencia objetoDeNegocio, Transferencia transferenciaDaBase)
        {
            if (objetoDeNegocio.ContagemAtual == 0)
            {
                objetoDeNegocio.ContagemAtual = transferenciaDaBase.ContagemAtual;
            }
        }
    }
}
