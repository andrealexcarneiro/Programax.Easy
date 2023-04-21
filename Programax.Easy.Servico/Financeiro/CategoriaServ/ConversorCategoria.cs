using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.Repositorio;

namespace Programax.Easy.Servico.Financeiro.CategoriaServ
{
    public class ConversorCategoria : ConversorDeObjetoBasico<CategoriaFinanceira>, IConversorDeObjeto<CategoriaFinanceira>
    {
        public CategoriaFinanceira CopieObjetoParaPersistencia(CategoriaFinanceira objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCategoria>();

            var linhaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new CategoriaFinanceira();

            CopieTodasAsPropriedades(objetoDeNegocio, linhaDaBase);

            return linhaDaBase;
        }
    }
}
