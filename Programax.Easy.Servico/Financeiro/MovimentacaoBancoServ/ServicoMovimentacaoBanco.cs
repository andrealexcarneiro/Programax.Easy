using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using System.Transactions;
using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;

namespace Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ
{
    [Funcionalidade(EnumFuncionalidade.MOVIMENTACAOBANCO)]
    public class ServicoMovimentacaoBanco : ServicoAkilSmallBusiness<MovimentacaoBanco, ValidacaoMovimentacaoBanco, ConversorMovimentacaoBanco>
    {
        IRepositorioMovimentacaoBanco _repositorioMovimentacaoBanco;

        public ServicoMovimentacaoBanco()
        {
            RetorneRepositorio();
        }

        public ServicoMovimentacaoBanco(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<MovimentacaoBanco> RetorneRepositorio()
        {
            if (_repositorioMovimentacaoBanco == null)
            {
                _repositorioMovimentacaoBanco = FabricaDeRepositorios.Crie<IRepositorioMovimentacaoBanco>();
            }

            return _repositorioMovimentacaoBanco;
        }

        public MovimentacaoBanco ConsulteBancoAberto(BancoParaMovimento banco)
        {
            return _repositorioMovimentacaoBanco.ConsulteBancoAberto(banco);
        }

        public override int Cadastre(MovimentacaoBanco objetoDeNegocio)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                int idCadastro = base.Cadastre(objetoDeNegocio);
                
                ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco(false, false);
                               
                ItemMovimentacaoBanco itemMovimentacaoBanco = new ItemMovimentacaoBanco();

                itemMovimentacaoBanco.DataHoraLancamento = objetoDeNegocio.DataHoraAbertura.GetValueOrDefault();
                itemMovimentacaoBanco.EstahEstornado = false;

                itemMovimentacaoBanco.MovimentacaoBanco = objetoDeNegocio;

                itemMovimentacaoBanco.TipoMovimentacao = EnumTipoMovimentacaoBanco.ENTRADA;
                itemMovimentacaoBanco.Parceiro = objetoDeNegocio.UsuarioAbertura;
                itemMovimentacaoBanco.ItemDeEntrada = true;

                itemMovimentacaoBanco.Categoria = objetoDeNegocio.Categoria;
                itemMovimentacaoBanco.DescricaoDaMovimentacao = "Saldo Inicial.";
                itemMovimentacaoBanco.OrigemMovimentacaoBanco = EnumOrigemMovimentacaoBanco.NENHUM;
                itemMovimentacaoBanco.Valor = objetoDeNegocio.SaldoInicial;

                servicoItemMovimentacaoBanco.Cadastre(itemMovimentacaoBanco);
                
                scope.Complete();

                return idCadastro;
            }
        }
        
        public List<MovimentacaoBanco> ConsulteLista(BancoParaMovimento banco,
                                                                            EnumDataFiltrarMovimentacaoCaixa? dataFiltrarMovimentacaoBanco,
                                                                            DateTime? dataInicial,
                                                                            DateTime? dataFinal,
                                                                            EnumStatusMovimentacaoCaixa? statusMovimentacao)
        {
            return _repositorioMovimentacaoBanco.ConsulteLista(banco, dataFiltrarMovimentacaoBanco, dataInicial, dataFinal, statusMovimentacao);
        }

        public List<int> ConsulteRegistrosDeMovimentoDoBanco(int bancoId, DateTime dataInicial, DateTime dataFinal)
        {
            return _repositorioMovimentacaoBanco.ConsulteRegistrosDeMovimentoDoBanco(bancoId, dataInicial, dataFinal);
        }

        public ItemMovimentacaoBanco ConsulteMovimentacaoNumeroItemBanco(DateTime? dataInicial, DateTime? dataFinal, int pedido, int categoria)
        {
            return _repositorioMovimentacaoBanco.ConsulteMovimentacaoNumeroItemBanco(dataInicial, dataFinal, pedido, categoria);
        }
        
    }
}
