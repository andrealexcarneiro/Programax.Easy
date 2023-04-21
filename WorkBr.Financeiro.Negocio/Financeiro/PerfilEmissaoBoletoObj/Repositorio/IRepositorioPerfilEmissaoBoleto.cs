using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.Repositorio
{
    public interface IRepositorioPerfilEmissaoBoleto : IRepositorioBase<PerfilEmissaoBoleto>
    {
        List<PerfilEmissaoBoleto> ConsulteLista(string descricao);

        new PerfilEmissaoBoleto Consulte(int id);

    }
}
