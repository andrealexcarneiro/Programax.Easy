using System;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using System.Transactions;
using System.Collections.Generic;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.Repositorio;

namespace Programax.Easy.Servico.Financeiro.ConciliacaoBancariaServ
{
    [Funcionalidade(EnumFuncionalidade.CONCILIACAOBANCARIA)]
    public class ServicoConciliacaoBancaria : ServicoAkilSmallBusiness<ConciliacaoBancaria, ValidacaoConcilicaoBancaria, ConversorConciliacaoBancaria>
    {
        IRepositorioConciliacaoBancaria _repositorioConciliacaoBancaria;

        public ServicoConciliacaoBancaria()
        {
            RetorneRepositorio();
        }

        public ServicoConciliacaoBancaria(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<ConciliacaoBancaria> RetorneRepositorio()
        {
            if (_repositorioConciliacaoBancaria == null)
            {
                _repositorioConciliacaoBancaria = FabricaDeRepositorios.Crie<IRepositorioConciliacaoBancaria>();
            }

            return _repositorioConciliacaoBancaria;
        }

        public override int Cadastre(ConciliacaoBancaria objetoDeNegocio)
        {
            return base.Cadastre(objetoDeNegocio);
        }

        public List<ConciliacaoBancaria> ConsulteLista(EnumOrigemMovimentacaoBanco? Status, EnumDataFiltrarConciliacaoBancaria? DataFiltrar,
                                                        DateTime? dataInicialPeriodo, DateTime? dataFinalPeriodo, int? movimentacaoId)
        {
            return _repositorioConciliacaoBancaria.ConsulteLista(Status, DataFiltrar, dataInicialPeriodo, dataFinalPeriodo, movimentacaoId);
        }

        public List<vw_fluxo_caixa> ConsulteFluxoCaixa(DateTime? dataInicialPeriodo, DateTime? dataFinalPeriodo)
        {
            return _repositorioConciliacaoBancaria.ConsulteFluxoCaixa(dataInicialPeriodo, dataFinalPeriodo);
        }
    }
}
