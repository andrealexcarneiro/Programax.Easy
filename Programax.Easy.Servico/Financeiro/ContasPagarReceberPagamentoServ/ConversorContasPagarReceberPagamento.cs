using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ
{
    public class ConversorContasPagarReceberPagamento : ConversorDeObjetoBasico<ContaPagarReceberPagamento>, IConversorDeObjeto<ContaPagarReceberPagamento>
    {
        public ContaPagarReceberPagamento CopieObjetoParaPersistencia(ContaPagarReceberPagamento objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioContaPagarReceberPagamento>();
            var contasPagarReceberPagamentoBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ContaPagarReceberPagamento();

            CopieTodasAsPropriedades(objetoDeNegocio, contasPagarReceberPagamentoBase);

            var repositorioContaPagarReceber = FabricaDeRepositorios.Crie<IRepositorioContasPagarReceber>();
            var contaPagarReceber = repositorioContaPagarReceber.Consulte(objetoDeNegocio.ContaPagarReceber.Id) ?? new ContaPagarReceber();

            contasPagarReceberPagamentoBase.ContaPagarReceber = contaPagarReceber;

            return contasPagarReceberPagamentoBase;
        }
    }
}
