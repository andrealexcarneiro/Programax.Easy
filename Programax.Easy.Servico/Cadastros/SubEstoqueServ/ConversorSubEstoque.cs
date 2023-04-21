using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.SubEstoqueServ
{
    public class ConversorSubEstoque : ConversorDeObjetoBasico<SubEstoque>, IConversorDeObjeto<SubEstoque>
    {
        public SubEstoque CopieObjetoParaPersistencia(SubEstoque objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioSubEstoque>();

            var subestoqueDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new SubEstoque();

            CopieTodasAsPropriedades(objetoDeNegocio, subestoqueDaBase);

            return subestoqueDaBase;
        }
    }
}
