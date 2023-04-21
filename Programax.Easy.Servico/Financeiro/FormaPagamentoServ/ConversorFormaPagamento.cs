using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using System.Collections.Generic;

namespace Programax.Easy.Servico.Financeiro.FormaPagamentoServ
{
    public class ConversorFormaPagamento : ConversorDeObjetoBasico<FormaPagamento>, IConversorDeObjeto<FormaPagamento>
    {
        public FormaPagamento CopieObjetoParaPersistencia(FormaPagamento objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioFormaPagamento>();

            var formaPagamentoBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new FormaPagamento();

            if (formaPagamentoBase.TipoFormaPagamento != EnumTipoFormaPagamento.OUTROS)
            {
                objetoDeNegocio.TipoFormaPagamento = formaPagamentoBase.TipoFormaPagamento;
            }

            objetoDeNegocio.FormaPagamentoNfce = formaPagamentoBase.FormaPagamentoNfce;

            var condicoes = CopieListaDeCondicoes(objetoDeNegocio, formaPagamentoBase);

            CopieTodasAsPropriedades(objetoDeNegocio, formaPagamentoBase);

            formaPagamentoBase.ListaCondicoesPagamento = condicoes;

            return formaPagamentoBase;
        }

        private IList<CondicaoDePagamentoDaForma> CopieListaDeCondicoes(FormaPagamento objetoDeNegocio, FormaPagamento formaPagamentoBase)
        {
            var listaCondicoes = formaPagamentoBase.ListaCondicoesPagamento;

            listaCondicoes.Clear();

            foreach (var item in objetoDeNegocio.ListaCondicoesPagamento)
            {
                var itemCopiado = new CondicaoDePagamentoDaForma();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.FormaPagamento = formaPagamentoBase;
                listaCondicoes.Add(itemCopiado);
            }

            return listaCondicoes;
        }
    }
}
