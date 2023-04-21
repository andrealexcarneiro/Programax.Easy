using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.Repositorio
{
    public interface IRepositorioMovimentacaoBase<TObjeto> : IRepositorioBase<TObjeto>
        where TObjeto : MovimentacaoBase
    {
        
    }
}
