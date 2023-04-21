using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.EnderecoObj.Repositorio
{
    public interface IRepositorioEndereco : IRepositorioBase<Endereco>
    {
        Endereco Consulte(string cep);

        List<Endereco> ConsulteLista(string cep, Estado estado, Cidade cidade, string bairro, string rua, string status);

        Endereco ConsulteAtivo(string cep);
    }
}
