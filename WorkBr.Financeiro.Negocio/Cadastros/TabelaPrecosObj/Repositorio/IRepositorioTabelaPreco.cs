using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.Repositorio
{
    public interface IRepositorioTabelaPreco : IRepositorioBase<TabelaPreco>
    {
        List<TabelaPreco> ConsulteListaTabelaPrecosAtivas();

        List<TabelaPreco> ConsulteLista(string descricao, string status, DateTime? dataValidade = null);
    }
}
