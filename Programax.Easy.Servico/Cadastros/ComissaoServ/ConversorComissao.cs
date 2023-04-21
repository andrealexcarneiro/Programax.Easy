using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.ComissaoServ
{
    public class ConversorComissao : ConversorDeObjetoBasico<Comissao>, IConversorDeObjeto<Comissao>
    {
        public Comissao CopieObjetoParaPersistencia(Comissao objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioComissao>();

            var comissaoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Comissao();

            CopieTodasAsPropriedades(objetoDeNegocio, comissaoDaBase);

            return comissaoDaBase;
        }
    }
}
