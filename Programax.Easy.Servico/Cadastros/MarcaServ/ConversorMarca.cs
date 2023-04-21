using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.MarcaObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.MarcaServ
{
    public class ConversorMarca : ConversorDeObjetoBasico<Marca>, IConversorDeObjeto<Marca>
    {
        public Marca CopieObjetoParaPersistencia(Marca objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioMarca>();

            var marcaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Marca();

            CopieTodasAsPropriedades(objetoDeNegocio, marcaDaBase);

            return marcaDaBase;
        }
    }
}
