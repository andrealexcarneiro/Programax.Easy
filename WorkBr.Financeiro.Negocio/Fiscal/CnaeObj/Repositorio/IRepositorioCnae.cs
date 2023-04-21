using System.Collections.Generic;
using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.CnaeObj.Repositorio
{
    public interface IRepositorioCnae : IRepositorioBase<Cnae>
    {
        Cnae ConsultePeloCodigo(string codigo);

        List<Cnae> ConsulteLista(string codigoCnae, string descricao, EnumAtividadeCnae? atividade, string status);
    }
}
