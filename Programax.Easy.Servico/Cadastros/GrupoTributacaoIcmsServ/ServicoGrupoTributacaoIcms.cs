using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.GrupoTributacaoIcmsServ
{
    [Funcionalidade(EnumFuncionalidade.GRUPOTRIBUTACAOICMS)]
    public class ServicoGrupoTributacaoIcms : ServicoAkilSmallBusiness<GrupoTributacaoIcms, ValidacaoGrupoTributacaoIcms, ConversorGrupoTributacaoIcms>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioGrupoTributacaoIcms _repositorioGrupoTributacaoIcms;

        #endregion

        #region " CONSTRUTOR "

        public ServicoGrupoTributacaoIcms()
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<GrupoTributacaoIcms> RetorneRepositorio()
        {
            if (_repositorioGrupoTributacaoIcms == null)
            {
                _repositorioGrupoTributacaoIcms = FabricaDeRepositorios.Crie<IRepositorioGrupoTributacaoIcms>();
            }

            return _repositorioGrupoTributacaoIcms;
        }

        #endregion

        #region " CONSULTAS "

        public List<GrupoTributacaoIcms> ConsulteLista(string descricao)
        {
            return _repositorioGrupoTributacaoIcms.ConsulteLista(descricao);
        }

        public GrupoTributacaoIcms ConsulteTributacaoTerceirosId(int id)
        {
            return _repositorioGrupoTributacaoIcms.ConsulteTributacaoTerceirosId(id);
        }

        public GrupoTributacaoIcms ConsulteTributacaoProducaoPropriaId(int id)
        {
            return _repositorioGrupoTributacaoIcms.ConsulteTributacaoProducaoPropriaId(id);
        }

        #endregion

        #region " VALIDAÇÕES "

        public void ValideTributacaoIcms(TributacaoIcms tributacaoIcms, List<TributacaoIcms> listaTributacoes)
        {
            ValidacaoTributacaoIcms validacaoTributacaoIcms = new ValidacaoTributacaoIcms();
            validacaoTributacaoIcms.ListaTributacoesIcms = listaTributacoes;
            validacaoTributacaoIcms.ValideInclusao();
            validacaoTributacaoIcms.Valide(tributacaoIcms).AssegureSucesso();
        }

        #endregion
    }
}
