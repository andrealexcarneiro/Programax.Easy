using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.SubGrupoServ
{
    public class ConversorSubGrupo : ConversorDeObjetoBasico<SubGrupo>, IConversorDeObjeto<SubGrupo>
    {
        public SubGrupo CopieObjetoParaPersistencia(SubGrupo objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioSubGrupo>();

            var linhaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new SubGrupo();

            CopieTodasAsPropriedades(objetoDeNegocio, linhaDaBase);

            return linhaDaBase;
        }
    }
}
