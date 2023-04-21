using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.Repositorio
{
    public interface IRepositorioTabelasAtualizadasIntegracaoDJ : IRepositorioBase<TabelasAtualizadasIntegracaoDJ>
    {
        void ExportaTodosDados();

        void ExcluaLista(List<TabelasAtualizadasIntegracaoDJ> atualizacoes);
    }
}
