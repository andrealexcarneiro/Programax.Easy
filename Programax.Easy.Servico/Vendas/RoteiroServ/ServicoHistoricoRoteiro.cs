using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.RoteiroObj;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using System.Collections.Generic;

namespace Programax.Easy.Servico.Vendas.RoteiroServ
{
    [Funcionalidade(EnumFuncionalidade.ROTEIROS)]
    public class ServicoHistoricoRoteiro : ServicoAkilSmallBusiness<HistoricoRoteiro, ValidacaoHistoricoRoteiro, ConversorHistoricoRoteiro>
    {
        protected IRepositorioHistoricoRoteiro _repositorioRoteiro;        

        #region " CONSTRUTOR "

        public ServicoHistoricoRoteiro()
        {
            RetorneRepositorio();
        }

        public ServicoHistoricoRoteiro(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<HistoricoRoteiro> RetorneRepositorio()
        {
            if (_repositorioRoteiro == null)
            {
                _repositorioRoteiro = FabricaDeRepositorios.Crie<IRepositorioHistoricoRoteiro>();
            }

            return _repositorioRoteiro;
        }

        #endregion


        #region "Consultas"

        public List<HistoricoRoteiro> ConsulteLista(int idRoteiro)
        {
            return _repositorioRoteiro.ConsulteLista(idRoteiro);
        }

        #endregion

    }
}
