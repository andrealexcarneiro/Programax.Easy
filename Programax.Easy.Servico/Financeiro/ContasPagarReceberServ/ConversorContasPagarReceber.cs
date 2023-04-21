using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class ConversorContasPagarReceber : ConversorDeObjetoBasico<ContaPagarReceber>, IConversorDeObjeto<ContaPagarReceber>
    {
        public ContaPagarReceber CopieObjetoParaPersistencia(ContaPagarReceber objetoDeNegocio)
        {
            objetoDeNegocio.ListaHistoricoAlteracoesVencimento.CarregueLazyLoad();
            objetoDeNegocio.ListaContasPagarReceberParcial.CarregueLazyLoad();

            var objetoDeNegocioCopiado = objetoDeNegocio.CloneCompleto();

            var repositorio = FabricaDeRepositorios.Crie<IRepositorioContasPagarReceber>();

            var contasPagarReceberBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ContaPagarReceber();

            var listaHistoricoAlteracoesVencimento = CopieListaHistoricoAlteracoes(objetoDeNegocioCopiado, contasPagarReceberBase);

            //var listaContasPagarReceberParcial = CopieListaContasPagarReceberParcial(objetoDeNegocioCopiado, contasPagarReceberBase);

            CopieTodasAsPropriedades(objetoDeNegocioCopiado, contasPagarReceberBase);

            //contasPagarReceberBase.ListaContasPagarReceberParcial = listaContasPagarReceberParcial;
            contasPagarReceberBase.ListaHistoricoAlteracoesVencimento = listaHistoricoAlteracoesVencimento;

            return contasPagarReceberBase;
        }

        protected IList<HistoricoAlteracaoVencimento> CopieListaHistoricoAlteracoes(ContaPagarReceber objetoDeNegocio, ContaPagarReceber contaPagarReceberDaBase)
        {
            var listaDeHistoricos = contaPagarReceberDaBase.ListaHistoricoAlteracoesVencimento;

            listaDeHistoricos.Clear();

            foreach (var item in objetoDeNegocio.ListaHistoricoAlteracoesVencimento)
            {
                var itemCopiado = new HistoricoAlteracaoVencimento();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.ContaPagarReceber = contaPagarReceberDaBase;
                listaDeHistoricos.Add(itemCopiado);
            }

            return listaDeHistoricos;
        }

        protected IList<ContaPagarReceberPagamento> CopieListaContasPagarReceberParcial(ContaPagarReceber objetoDeNegocio, ContaPagarReceber contaPagarReceberDaBase)
        {
            var listaContasPagarReceberParcial = contaPagarReceberDaBase.ListaContasPagarReceberParcial;

            listaContasPagarReceberParcial.Clear();

            foreach (var item in objetoDeNegocio.ListaHistoricoAlteracoesVencimento)
            {
                var itemCopiado = new ContaPagarReceberPagamento();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.ContaPagarReceber = contaPagarReceberDaBase;
                listaContasPagarReceberParcial.Add(itemCopiado);
            }

            return listaContasPagarReceberParcial;
        }
        
    }
}
