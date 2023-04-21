using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using System.Transactions;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.PessoaObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.ComissaoServ
{
    [Funcionalidade(EnumFuncionalidade.COMISSOES)]
    public class ServicoComissao : ServicoAkilSmallBusiness<Comissao, ValidacaoComissao, ConversorComissao>
    {
        #region " VARIÁVEIS PRIVADAS "

        IRepositorioPessoa _repositorioPessoa;
        IRepositorioComissao _repositorioComissao;
        private readonly ServicoPessoa _servicoPessoa = new ServicoPessoa();

        #endregion

        #region " CONSTRUTOR "

        public ServicoComissao()
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<Comissao> RetorneRepositorio()
        {
            if (_repositorioComissao == null)
            {
                _repositorioComissao = FabricaDeRepositorios.Crie<IRepositorioComissao>();

                _repositorioPessoa = FabricaDeRepositorios.Crie<IRepositorioPessoa>();
            }

            return _repositorioComissao;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void CadastreListaComissoes(Pessoa pessoa, List<Comissao> listaComissoes)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var pessoaDaBase = _repositorioPessoa.Consulte(pessoa.Id);

                RemovaComissoesPessoa(pessoaDaBase);

                CadastreNovasComissoes(pessoaDaBase, listaComissoes);

                AtualizeFuncoesPessoa(pessoaDaBase, listaComissoes);

                scope.Complete();
            }
        }

        public void ValideComissao(Comissao comissao, List<Comissao> listaComissoes)
        {
            ValidacaoComissao validacaoComissao = new ValidacaoComissao();

            validacaoComissao.ListaDeComissoes = listaComissoes;

            validacaoComissao.ValideInclusao();

            validacaoComissao.Valide(comissao).AssegureSucesso();
        }

        #endregion

        #region " MÉTODOS PRIVADOS "

        private void AtualizeFuncoesPessoa(Pessoa pessoa, List<Comissao> listaComissoes)
        {
            pessoa.Vendedor = pessoa.Vendedor ?? new Vendedor();

            pessoa.Vendedor.EhAtendente = listaComissoes.Exists(x => x.FuncaoPessoaComissao == EnumFuncaoPessoaComissao.ATENDENTE);
            pessoa.Vendedor.EhIndicador = listaComissoes.Exists(x => x.FuncaoPessoaComissao == EnumFuncaoPessoaComissao.INDICADOR);
            pessoa.Vendedor.EhSupervisor = listaComissoes.Exists(x => x.FuncaoPessoaComissao == EnumFuncaoPessoaComissao.SUPERVISOR);
            pessoa.Vendedor.EhVendedor = listaComissoes.Exists(x => x.FuncaoPessoaComissao == EnumFuncaoPessoaComissao.VENDEDOR);

            _repositorioPessoa.Atualize(pessoa);
        }

        private void RemovaComissoesPessoa(Pessoa pessoa)
        {
            foreach (var comissao in pessoa.ListaDeComissoes)
            {
                _repositorioComissao.Exclua(comissao);
            }
        }

        private void CadastreNovasComissoes(Pessoa pessoa, List<Comissao> listaComissoes)
        {
            listaComissoes.ForEach(comissao =>
            {
                comissao.Id = 0;
                comissao.Pessoa = pessoa;
            });

            CadastreLista(listaComissoes);
        }

        #endregion

        public List<Comissao> ConsulteLista(Pessoa pessoa)
        {
            return _repositorioComissao.ConsulteLista(pessoa);
        }
    }
}
