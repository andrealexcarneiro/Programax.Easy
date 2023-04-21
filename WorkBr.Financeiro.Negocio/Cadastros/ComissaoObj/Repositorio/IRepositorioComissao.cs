using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.ComissaoObj.Repositorio
{
    public interface IRepositorioComissao : IRepositorioBase<Comissao>
    {
        List<Comissao> ConsultePorPessoaETabelaPreco(Pessoa pessoa, TabelaPreco tabelaPreco);

        List<Comissao> ConsulteLista(Pessoa pessoa);
    }
}
