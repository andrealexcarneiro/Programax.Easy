using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.Repositorio
{
    public interface IRepositorioConciliacaoBancaria : IRepositorioBase<ConciliacaoBancaria>
    {
        List<ConciliacaoBancaria> ConsulteLista(EnumOrigemMovimentacaoBanco? Status, EnumDataFiltrarConciliacaoBancaria? DataFiltrar,
                                                  DateTime? dataInicialPeriodo,
                                                  DateTime? dataFinalPeriodo, int? MovimentacaoId);

        List<vw_fluxo_caixa> ConsulteFluxoCaixa(DateTime? dataInicialPeriodo, DateTime? dataFinalPeriodo);
    }
}
