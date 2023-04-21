using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.Repositorio
{
    public interface IRepositorioCondicaoPagamento : IRepositorioBase<CondicaoPagamento>
    {
        List<CondicaoPagamento> ConsulteListaDeCondicoesPagamentoAtivas();

        List<CondicaoPagamento> ConsulteLista(string descricao, string status);

        CondicaoPagamento ConsulteCondicaoPagamentoAVistaPadrao();
    }
}
