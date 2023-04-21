using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ
{
    public class ConversorNaturezaOperacao : ConversorDeObjetoBasico<NaturezaOperacao>, IConversorDeObjeto<NaturezaOperacao>
    {
        public NaturezaOperacao CopieObjetoParaPersistencia(NaturezaOperacao objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioNaturezaOperacao>();

            var naturezaOperacaoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new NaturezaOperacao();

            var lista = CopieListaDeCfops(objetoDeNegocio, naturezaOperacaoDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, naturezaOperacaoDaBase);

            naturezaOperacaoDaBase.ListaCfops = lista;

            return naturezaOperacaoDaBase;
        }

        private IList<NaturezaOperacaoCfop> CopieListaDeCfops(NaturezaOperacao objetoDeNegocio, NaturezaOperacao naturezaOperacaoBase)
        {
            var listaCfops = naturezaOperacaoBase.ListaCfops;

            listaCfops.Clear();

            foreach (var item in objetoDeNegocio.ListaCfops)
            {
                var itemCopiado = new NaturezaOperacaoCfop();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.NaturezaOperacao = naturezaOperacaoBase;
                listaCfops.Add(itemCopiado);
            }

            return listaCfops;
        }
    }
}
