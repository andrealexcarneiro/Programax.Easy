using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.Repositorio
{
    public interface IRepositorioNaturezaOperacao : IRepositorioBase<NaturezaOperacao>
    {
        List<NaturezaOperacao> ConsulteListaAtiva();

        List<NaturezaOperacao> ConsulteLista(int? id, string descricao, string status);

        NaturezaOperacao ConsulteNaturezaOperacaoPorCfop(string codigoCfop);
    }
}
