using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.Repositorio;
using NHibernate;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ
{
    public class RepositorioContaPagarReceberPagamento : RepositorioBase<ContaPagarReceberPagamento>, IRepositorioContaPagarReceberPagamento
    {
        public RepositorioContaPagarReceberPagamento(ISession sessao)
            : base(sessao)
        {

        }
    }
}
