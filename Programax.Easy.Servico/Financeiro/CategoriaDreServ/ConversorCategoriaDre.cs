using Programax.Easy.Negocio.Financeiro.CategoriaDreObj.ObjetoDeNeocio;
using Programax.Easy.Negocio.Financeiro.CategoriaDreObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.CategoriaDreServ
{
    public class ConversorCategoriaDre : ConversorDeObjetoBasico<CategoriaDre>, IConversorDeObjeto<CategoriaDre>
    {
        public CategoriaDre CopieObjetoParaPersistencia(CategoriaDre objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCategoriaDre>();

            var linhaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new CategoriaDre();

            CopieTodasAsPropriedades(objetoDeNegocio, linhaDaBase);

            return linhaDaBase;
        }
    }
}
