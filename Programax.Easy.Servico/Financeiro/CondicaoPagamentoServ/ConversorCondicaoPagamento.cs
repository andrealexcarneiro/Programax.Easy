using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using System.Collections.Generic;

namespace Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ
{
    public class ConversorCondicaoPagamento : ConversorDeObjetoBasico<CondicaoPagamento>, IConversorDeObjeto<CondicaoPagamento>
    {
        public CondicaoPagamento CopieObjetoParaPersistencia(CondicaoPagamento objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCondicaoPagamento>();

            var condicaoPagamentoBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new CondicaoPagamento();

            var listaDeParcelas = CopieListaDeParcelas(objetoDeNegocio, condicaoPagamentoBase);

            CopieTodasAsPropriedades(objetoDeNegocio, condicaoPagamentoBase);

            condicaoPagamentoBase.ListaDeParcelas = listaDeParcelas;

            return condicaoPagamentoBase;
        }

        private IList<ParcelaCondicaoPagamento> CopieListaDeParcelas(CondicaoPagamento objetoDeNegocio, CondicaoPagamento condicaoPagamentoBase)
        {
            var listaParcelas = condicaoPagamentoBase.ListaDeParcelas;

            listaParcelas.Clear();

            foreach (var item in objetoDeNegocio.ListaDeParcelas)
            {
                var itemCopiado = new ParcelaCondicaoPagamento();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.CondicaoPagamento = condicaoPagamentoBase;
                listaParcelas.Add(itemCopiado);
            }

            return listaParcelas;
        }
    }
}
