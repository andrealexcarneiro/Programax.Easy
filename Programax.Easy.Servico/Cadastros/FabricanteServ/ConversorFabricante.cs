using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.FabricanteServ
{
    public class ConversorFabricante : ConversorDeObjetoBasico<Fabricante>, IConversorDeObjeto<Fabricante>
    {
        public Fabricante CopieObjetoParaPersistencia(Fabricante objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioFabricante>();

            var fabricanteDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Fabricante();

            CopieTodasAsPropriedades(objetoDeNegocio, fabricanteDaBase);

            return fabricanteDaBase;
        }
    }
}
