using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.Repositorio
{
    public interface IRepositorioPlanoDeContasDre : IRepositorioBase<PlanoContaDre>
    {
        List<PlanoContaDre> ConsulteLista(string numeroPlanoContas,
                                                          string descricao,
                                                          string status,
                                                          Enumeradores.EnumNaturezaPlanoContas? naturezaPlanoContas,
                                                          Enumeradores.EnumTipoPlanoContas? tipoPlanoContas,
                                                          string numeroPlanoContasContador, string Grau);

        PlanoContaDre ConsultePlanoDeContasPeloNumeroDiferenteDoIdInformado(int idPlanoDeContas, string numeroPlanoDeContas);

        PlanoContaDre ConsultePlanoDeContasAtivoPeloNumero(string numeroPlanoContas);

        PlanoContaDre ConsultePlanoDeContasPeloNumero(string numeroPlanoContas);
    }
}
