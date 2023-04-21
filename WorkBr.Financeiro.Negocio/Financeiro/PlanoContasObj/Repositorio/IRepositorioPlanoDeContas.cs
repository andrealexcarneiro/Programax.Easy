using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.PlanoContasObj.Repositorio
{
    public interface IRepositorioPlanoDeContas : IRepositorioBase<PlanoDeContas>
    {
        List<PlanoDeContas> ConsulteLista(string numeroPlanoContas,
                                                           string descricao,
                                                           string status, 
                                                           Enumeradores.EnumNaturezaPlanoContas? naturezaPlanoContas, 
                                                           Enumeradores.EnumTipoPlanoContas? tipoPlanoContas,
                                                           string numeroPlanoContasContador);

        PlanoDeContas ConsultePlanoDeContasPeloNumeroDiferenteDoIdInformado(int idPlanoDeContas, string numeroPlanoDeContas);

        PlanoDeContas ConsultePlanoDeContasAtivoPeloNumero(string numeroPlanoContas);

        PlanoDeContas ConsultePlanoDeContasPeloNumero(string numeroPlanoContas);
    }
}
