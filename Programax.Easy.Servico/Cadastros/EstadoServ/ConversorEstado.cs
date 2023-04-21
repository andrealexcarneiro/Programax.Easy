using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.EstadoObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.EstadoServ
{
    public class ConversorEstado : ConversorDeObjetoBasico<Estado>, IConversorDeObjeto<Estado>
    {
        public Estado CopieObjetoParaPersistencia(Estado objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioEstado>();

            var cidadeDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Estado();

            CopieTodasAsPropriedades(objetoDeNegocio, cidadeDaBase);

            return cidadeDaBase;
        }
    }
}
