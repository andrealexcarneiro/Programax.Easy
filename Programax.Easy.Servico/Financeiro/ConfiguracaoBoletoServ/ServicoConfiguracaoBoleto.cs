using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Servico.Financeiro.ConfiguracaoBoletoServ;

namespace Programax.Easy.Servico.Financeiro.ConfiguracaoBoletoSev
{
   
    [Funcionalidade(EnumFuncionalidade.CONFIGURACAOEMISSAOBOLETO)]
    public class ServicoConfiguracaoBoleto : ServicoAkilSmallBusiness<ConfiguracaoBoleto, ValidacaoConfiguracaoBoleto, ConversorConfiguracaoBoleto>
    {
        IRepositorioConfiguracaoBoleto _repositorioPerfilEmissaoBoleto;

        public override IRepositorioBase<ConfiguracaoBoleto> RetorneRepositorio()
        {
            if (_repositorioPerfilEmissaoBoleto == null)
            {
                _repositorioPerfilEmissaoBoleto = FabricaDeRepositorios.Crie<IRepositorioConfiguracaoBoleto>();
            }

            return _repositorioPerfilEmissaoBoleto;
        }

        public ServicoConfiguracaoBoleto()
        {
            RetorneRepositorio();
        }
        
        public List<ConfiguracaoBoleto> ConsulteLista(int idPerfil)
        {
            return _repositorioPerfilEmissaoBoleto.ConsulteLista(idPerfil);
        }
        
        public ConfiguracaoBoleto ConsultePeloPerfil(int IdPerfil)
        {
            return _repositorioPerfilEmissaoBoleto.ConsultePeloPerfil(IdPerfil);
        }

        public ConfiguracaoBoleto Consulte(int Id)
        {
            return _repositorioPerfilEmissaoBoleto.Consulte(Id);
        }

    }
}
