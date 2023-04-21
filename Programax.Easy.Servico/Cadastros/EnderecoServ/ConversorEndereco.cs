using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.EnderecoServ
{
    public class ConversorEndereco : ConversorDeObjetoBasico<Endereco>, IConversorDeObjeto<Endereco>
    {
        public Endereco CopieObjetoParaPersistencia(Endereco objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioEndereco>();

            var enderecoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Endereco();

            CopieTodasAsPropriedades(objetoDeNegocio, enderecoDaBase);

            return enderecoDaBase;
        }
    }
}
