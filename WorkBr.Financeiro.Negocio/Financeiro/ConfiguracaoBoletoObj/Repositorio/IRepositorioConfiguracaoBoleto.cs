using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.Repositorio
{
    public interface IRepositorioConfiguracaoBoleto : IRepositorioBase<ConfiguracaoBoleto>
    {   
        ConfiguracaoBoleto ConsultePeloPerfil(int idPerfil);

        new ConfiguracaoBoleto Consulte(int Id);

        List<ConfiguracaoBoleto> ConsulteLista(int idPerfil);
    }
    
}
