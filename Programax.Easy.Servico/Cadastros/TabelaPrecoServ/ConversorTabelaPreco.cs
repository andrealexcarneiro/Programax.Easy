using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.TabelaPrecoServ
{
    public class ConversorTabelaPreco : ConversorDeObjetoBasico<TabelaPreco>, IConversorDeObjeto<TabelaPreco>
    {
        public TabelaPreco CopieObjetoParaPersistencia(TabelaPreco objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioTabelaPreco>();

            var tabelaPrecoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new TabelaPreco();

            CopieTodasAsPropriedades(objetoDeNegocio, tabelaPrecoDaBase);

            return tabelaPrecoDaBase;
        }
    }
}
