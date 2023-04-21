using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;

namespace Programax.Easy.Servico.ConfiguracoesSistema.ConfiguracoesPdvServ
{
    [Funcionalidade(EnumFuncionalidade.CONFIGURACOESPDV)]
    public class ServicoConfiguracoesPdv : ServicoAkilSmallBusiness<ConfiguracoesPdv, ValidacaoConfiguracoesPdv, ConversorConfiguracoesPdv>
    {
        IRepositorioConfiguracoesPdv _repositorioConfiguracoesPdv;

        public ServicoConfiguracoesPdv()
        {
            RetorneRepositorio();
        }

        public ServicoConfiguracoesPdv(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<ConfiguracoesPdv> RetorneRepositorio()
        {
            if (_repositorioConfiguracoesPdv == null)
            {
                _repositorioConfiguracoesPdv = FabricaDeRepositorios.Crie<IRepositorioConfiguracoesPdv>();
            }

            return _repositorioConfiguracoesPdv;
        }

        public ConfiguracoesPdv ConsulteUltimaConfiguracaoPdv()
        {
            var configuracao = _repositorioConfiguracoesPdv.ConsulteUltimaConfiguracaoPdv();

            if (configuracao == null)
            {
                configuracao = new ConfiguracoesPdv();
            }

            return configuracao;
        }
    }
}
