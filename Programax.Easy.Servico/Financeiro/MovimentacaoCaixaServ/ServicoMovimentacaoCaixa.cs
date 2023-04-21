using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using System.Transactions;
using System;
using System.Collections.Generic;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ
{
    [Funcionalidade(EnumFuncionalidade.MOVIMENTACAOCAIXA)]
    public class ServicoMovimentacaoCaixa : ServicoAkilSmallBusiness<MovimentacaoCaixa, ValidacaoMovimentacaoCaixa, ConversorMovimentacaoCaixa>
    {
        IRepositorioMovimentacaoCaixa _repositorioMovimentacaoCaixa;

        public ServicoMovimentacaoCaixa()
        {
            RetorneRepositorio();
        }

        public ServicoMovimentacaoCaixa(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<MovimentacaoCaixa> RetorneRepositorio()
        {
            if (_repositorioMovimentacaoCaixa == null)
            {
                _repositorioMovimentacaoCaixa = FabricaDeRepositorios.Crie<IRepositorioMovimentacaoCaixa>();
            }

            return _repositorioMovimentacaoCaixa;
        }

        public MovimentacaoCaixa ConsulteCaixaAberto(Caixa caixa)
        {
            return _repositorioMovimentacaoCaixa.ConsulteCaixaAberto(caixa);
        }

        public VWSaldoAtualCaixa ConsulteSaldoAtualCaixa()
        {
            return _repositorioMovimentacaoCaixa.ConsulteSaldoAtualCaixa();
        }

        public override int Cadastre(MovimentacaoCaixa objetoDeNegocio)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                int idCadastro = base.Cadastre(objetoDeNegocio);
                
                ServicoItemMovimentacaoCaixa servicoItemMovimentacaoCaixa = new ServicoItemMovimentacaoCaixa(false, false);
                               
                ItemMovimentacaoCaixa itemMovimentacaoCaixa = new ItemMovimentacaoCaixa();

                itemMovimentacaoCaixa.DataHora = objetoDeNegocio.DataHoraAbertura.GetValueOrDefault();
                itemMovimentacaoCaixa.EstahEstornado = false;
               
                itemMovimentacaoCaixa.MovimentacaoCaixa = objetoDeNegocio;
               
                itemMovimentacaoCaixa.TipoMovimentacao = EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO;                
                itemMovimentacaoCaixa.Parceiro = objetoDeNegocio.UsuarioAbertura;
                itemMovimentacaoCaixa.ItemDeEntrada = true;
                                               
                itemMovimentacaoCaixa.FormaPagamento = new FormaPagamento { Id = 1 };
                itemMovimentacaoCaixa.HistoricoMovimentacoes = "Saldo inicial dinheiro na abertura.";
                itemMovimentacaoCaixa.Valor = objetoDeNegocio.SaldoInicialDinheiro;
                itemMovimentacaoCaixa.CategoriaFinaceira = objetoDeNegocio.CategoriaFinanceira;

                servicoItemMovimentacaoCaixa.Cadastre(itemMovimentacaoCaixa);
                
                scope.Complete();

                return idCadastro;
            }
        }

        public void CadastreCheque(MovimentacaoCaixa objetoDeNegocio)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {  
                ServicoItemMovimentacaoCaixa servicoItemMovimentacaoCaixa = new ServicoItemMovimentacaoCaixa(false, false);

                ItemMovimentacaoCaixa itemMovimentacaoCaixa = new ItemMovimentacaoCaixa();

                itemMovimentacaoCaixa.DataHora = objetoDeNegocio.DataHoraAbertura.GetValueOrDefault();
                itemMovimentacaoCaixa.EstahEstornado = false;

                itemMovimentacaoCaixa.MovimentacaoCaixa = objetoDeNegocio;

                itemMovimentacaoCaixa.TipoMovimentacao = EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO;
                itemMovimentacaoCaixa.Parceiro = objetoDeNegocio.UsuarioAbertura;
                itemMovimentacaoCaixa.ItemDeEntrada = true;

                itemMovimentacaoCaixa.FormaPagamento = new FormaPagamento { Id = 4 };
                itemMovimentacaoCaixa.HistoricoMovimentacoes = "Saldo inicial cheque na abertura.";
                itemMovimentacaoCaixa.Valor = objetoDeNegocio.SaldoInicialCheque;

                servicoItemMovimentacaoCaixa.Cadastre(itemMovimentacaoCaixa);

                scope.Complete();
            }
        }

        public List<MovimentacaoCaixa> ConsulteLista(Caixa caixa,
                                                                            EnumDataFiltrarMovimentacaoCaixa? dataFiltrarMovimentacaoCaixa,
                                                                            DateTime? dataInicial,
                                                                            DateTime? dataFinal,
                                                                            EnumStatusMovimentacaoCaixa? statusMovimentacao)
        {
            return _repositorioMovimentacaoCaixa.ConsulteLista(caixa, dataFiltrarMovimentacaoCaixa, dataInicial, dataFinal, statusMovimentacao);
        }

        public ItemMovimentacaoCaixa ConsulteMovimentacaoNumeroItemCaixa(DateTime? dataInicial, DateTime? dataFinal, int pedido, int formaPagamento)
        {
            return _repositorioMovimentacaoCaixa.ConsulteMovimentacaoNumeroItemCaixa(dataInicial, dataFinal, pedido, formaPagamento);
        }

    }
}
