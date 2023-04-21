using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.Repositorio;

namespace Programax.Easy.Servico.Fiscal.CartaCorrecaoServ
{
    public class ConversorCartaCorrecao : ConversorDeObjetoBasico<CartaCorrecao>, IConversorDeObjeto<CartaCorrecao>
    {
        public CartaCorrecao CopieObjetoParaPersistencia(CartaCorrecao objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCartaCorrecao>();

            var cartaCorrecaoBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new CartaCorrecao();

            CopieTodasAsPropriedades(objetoDeNegocio, cartaCorrecaoBase);

            return cartaCorrecaoBase;
        }
    }
}
