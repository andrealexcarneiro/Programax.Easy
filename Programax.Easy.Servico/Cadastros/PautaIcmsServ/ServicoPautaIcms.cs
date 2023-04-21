using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Cadastros.PautaIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PautaIcmsObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.PautaIcmsServ
{
    [Funcionalidade(EnumFuncionalidade.PAUTAICMS)]
    public class ServicoPautaIcms : ServicoAkilSmallBusiness<PautaIcms, ValidacaoPautaIcms, ConversorPautaIcms>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioPautaIcms _repositorioPautaIcms;

        #endregion

        #region " CONSTRUTOR "

        public ServicoPautaIcms()
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<PautaIcms> RetorneRepositorio()
        {
            if (_repositorioPautaIcms == null)
            {
                _repositorioPautaIcms = FabricaDeRepositorios.Crie<IRepositorioPautaIcms>();
            }

            return _repositorioPautaIcms;
        }

        #endregion

        #region " CONSULTAS "

        public PautaIcms Consulte(Produto produto, Estado estado, Cidade cidade)
        {
            return _repositorioPautaIcms.Consulte(produto, estado, cidade);
        }

        #endregion
    }
}
