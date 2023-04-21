using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl
{
    [Funcionalidade(EnumFuncionalidade.BANCOPARAMOVIMENTO)]
    public class ServicoBancoParaMovimento:ServicoAkilSmallBusiness<BancoParaMovimento,ValidacaoBancoParaMovimento, BancoParaMovimentoServ.ConversorBancoParaMovimento>
    {
        private IRepositorioBancoParaMovimento _repositorioBancoParaMovimento;

        public ServicoBancoParaMovimento()
        {
            RetorneRepositorio();
        }

        public ServicoBancoParaMovimento(bool verificarPermissao, bool limparSessao)
           : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<BancoParaMovimento> RetorneRepositorio()
        {
            if (_repositorioBancoParaMovimento == null)
            {
                _repositorioBancoParaMovimento = FabricaDeRepositorios.Crie<IRepositorioBancoParaMovimento>();
            }

            return _repositorioBancoParaMovimento;
        }

        public List<BancoParaMovimento> ConsulteLista(string nomeBanco, string status)
        {
            return _repositorioBancoParaMovimento.ConsulteLista(nomeBanco, status);
        }

        public BancoParaMovimento ConsulteBanco()
        {
            return _repositorioBancoParaMovimento.ConsulteBanco();
        }

        public BancoParaMovimento ConsulteBanco(bool ehPadrao)
        {
            return _repositorioBancoParaMovimento.ConsulteBanco(ehPadrao);
        }
    }
}
