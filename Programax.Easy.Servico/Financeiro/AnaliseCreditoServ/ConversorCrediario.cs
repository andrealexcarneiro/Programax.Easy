using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.Repositorio;

namespace Programax.Easy.Servico.Financeiro.CrediarioServ
{
    public class ConversorCrediario : ConversorDeObjetoBasico<Crediario>, IConversorDeObjeto<Crediario>
    {
        public Crediario CopieObjetoParaPersistencia(Crediario objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCrediario>();

            var analiseCreditoBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Crediario();

            CopieTodasAsPropriedades(objetoDeNegocio, analiseCreditoBase);

            return analiseCreditoBase;
        }
    }
}
