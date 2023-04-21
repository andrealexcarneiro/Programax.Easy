using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.GrupoTributacaoFederalServ
{
    [Funcionalidade(EnumFuncionalidade.GRUPOTRIBUTACAOFEDERAL)]
    public class ServicoGrupoTributacaoFederal : ServicoAkilSmallBusiness<GrupoTributacaoFederal, ValidacaoGrupoTributacaoFederal, ConversorGrupoTributacaoFederal>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioGrupoTributacaoFederal _repositorioGrupoTributacaoFederal;

        #endregion

        #region " CONSTRUTOR "

        public ServicoGrupoTributacaoFederal()
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<GrupoTributacaoFederal> RetorneRepositorio()
        {
            if (_repositorioGrupoTributacaoFederal == null)
            {
                _repositorioGrupoTributacaoFederal = FabricaDeRepositorios.Crie<IRepositorioGrupoTributacaoFederal>();
            }

            return _repositorioGrupoTributacaoFederal;
        }

        #endregion

        #region " CONSULTAS "

        public List<GrupoTributacaoFederal> ConsulteLista(string descricao)
        {
            return _repositorioGrupoTributacaoFederal.ConsulteLista(descricao);
        }

        public GrupoTributacaoFederal ConsulteTributacaoTerceirosId(int id)
        {
            return _repositorioGrupoTributacaoFederal.ConsulteTributacaoTerceirosId(id);
        }

        public GrupoTributacaoFederal ConsulteTributacaoProducaoPropriaId(int id)
        {
            return _repositorioGrupoTributacaoFederal.ConsulteTributacaoProducaoPropriaId(id);
        }

        #endregion

        #region " VALIDAÇÕES "

        public void ValideTributacaoCofins(CofinsNotaFiscal tributacaoCofins, List<CofinsNotaFiscal> listaTributacoes)
        {
            ValidacaoTributacaoFederalCofins validacaoTributacaoCofins = new ValidacaoTributacaoFederalCofins();
            validacaoTributacaoCofins.ListaTributacoesFederalCofins = listaTributacoes;
            validacaoTributacaoCofins.ValideInclusao();
            validacaoTributacaoCofins.Valide(tributacaoCofins).AssegureSucesso();
        }

        public void ValideTributacaoPis(PisNotaFiscal tributacaoPis, List<PisNotaFiscal> listaTributacoes)
        {
            ValidacaoTributacaoFederalPis validacaoTributacaoPis = new ValidacaoTributacaoFederalPis();
            validacaoTributacaoPis.ListaTributacoesFederalPis = listaTributacoes;
            validacaoTributacaoPis.ValideInclusao();
            validacaoTributacaoPis.Valide(tributacaoPis).AssegureSucesso();
        }

        public void ValideTributacaoIpi(IpiNotaFiscal tributacaoIpi, List<IpiNotaFiscal> listaTributacoes)
        {
            ValidacaoTributacaoFederalIpi validacaoTributacaoIpi = new ValidacaoTributacaoFederalIpi();
            validacaoTributacaoIpi.ListaTributacoesFederalIpi = listaTributacoes;
            validacaoTributacaoIpi.ValideInclusao();
            validacaoTributacaoIpi.Valide(tributacaoIpi).AssegureSucesso();
        }

        #endregion
    }
}
