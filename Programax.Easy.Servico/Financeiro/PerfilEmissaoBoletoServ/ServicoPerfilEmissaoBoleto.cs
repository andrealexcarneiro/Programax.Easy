using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Financeiro.PerfilEmissaoBoletoSev
{
   
    [Funcionalidade(EnumFuncionalidade.CONFIGURACAOEMISSAOBOLETO)]
    public class ServicoPerfilEmissaoBoleto : ServicoAkilSmallBusiness<PerfilEmissaoBoleto, ValidacaoPerfilEmissaoBoleto, ConversorPerfilEmissaoBoleto>
    {
        IRepositorioPerfilEmissaoBoleto _repositorioPerfilEmissaoBoleto;

        public override IRepositorioBase<PerfilEmissaoBoleto> RetorneRepositorio()
        {
            if (_repositorioPerfilEmissaoBoleto == null)
            {
                _repositorioPerfilEmissaoBoleto = FabricaDeRepositorios.Crie<IRepositorioPerfilEmissaoBoleto>();
            }

            return _repositorioPerfilEmissaoBoleto;
        }

        public ServicoPerfilEmissaoBoleto()
        {
            RetorneRepositorio();
        }

        public List<PerfilEmissaoBoleto> ConsulteLista()
        {
            return _repositorioPerfilEmissaoBoleto.ConsulteLista();
        }

        public List<PerfilEmissaoBoleto> ConsulteLista(string descricao)
        {
            return _repositorioPerfilEmissaoBoleto.ConsulteLista(descricao);
        }
        
        public override PerfilEmissaoBoleto Consulte(int Id)
        {
            return _repositorioPerfilEmissaoBoleto.Consulte(Id);
        }

        
    }
}
