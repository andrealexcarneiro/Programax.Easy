using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.GrupoObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.GrupoServ
{
    public class ConversorGrupo : ConversorDeObjetoBasico<Grupo>, IConversorDeObjeto<Grupo>
    {
        public Grupo CopieObjetoParaPersistencia(Grupo objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioGrupo>();

            var linhaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Grupo();

            CopieTodasAsPropriedades(objetoDeNegocio, linhaDaBase);

            return linhaDaBase;
        }
    }
}
