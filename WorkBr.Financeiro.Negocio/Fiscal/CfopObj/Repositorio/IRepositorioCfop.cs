using System.Collections.Generic;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.CfopObj.Repositorio
{
    public interface IRepositorioCfop : IRepositorioBase<Cfop>
    {
        List<Cfop> ConsulteLista(string codigoCfop, string descricao, string status, EnumTipoCfop tipoCfop);

        List<Cfop> ConsulteListaAtiva(EnumOrigemDestino origemDestino, EnumTipoMovimentacaoNaturezaOperacao tipoMovimentacaoNaturezaOperacao);

        Cfop ConsultePeloCodigoCfop(string codigoCfop);

        List<Cfop> ConsulteListaAtiva();

        List<Cfop> ConsulteListaDeCodigosCfop(List<string> listaCodigosCfop);
    }
}
