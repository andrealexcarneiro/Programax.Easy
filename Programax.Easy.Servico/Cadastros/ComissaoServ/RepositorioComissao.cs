using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using NHibernate;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.ComissaoServ
{
    public class RepositorioComissao : RepositorioBase<Comissao>, IRepositorioComissao
    {
        public RepositorioComissao(ISession sessao)
            : base(sessao)
        {

        }

        public List<Comissao> ConsultePorPessoaETabelaPreco(Pessoa pessoa, TabelaPreco tabelaPreco)
        {
            return _session.QueryOver<Comissao>().Where(comissao => comissao.Pessoa.Id == pessoa.Id && comissao.TabelaPreco.Id == tabelaPreco.Id).List().ToList();
        }

        public List<Comissao> ConsulteLista(Pessoa pessoa)
        {
            return _session.QueryOver<Comissao>().Where(comissao => comissao.Pessoa.Id == pessoa.Id ).List().ToList();
        }
    }
}
