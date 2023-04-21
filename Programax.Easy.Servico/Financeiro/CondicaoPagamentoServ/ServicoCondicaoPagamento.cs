using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Servicos;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.Repositorio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ
{
    [Funcionalidade(EnumFuncionalidade.CONDICOESPAGAMENTO)]
    public class ServicoCondicaoPagamento : ServicoAkilSmallBusiness<CondicaoPagamento, ValidacaoCondicaoPagamento, ConversorCondicaoPagamento>
    {
        IRepositorioCondicaoPagamento _repositorioCondicaoPagamento;

        public ServicoCondicaoPagamento()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<CondicaoPagamento> RetorneRepositorio()
        {
            if (_repositorioCondicaoPagamento == null)
            {
                _repositorioCondicaoPagamento = FabricaDeRepositorios.Crie<IRepositorioCondicaoPagamento>();
            }

            return _repositorioCondicaoPagamento;
        }

        public List<CondicaoPagamento> ConsulteLista()
        {
            return _repositorioCondicaoPagamento.ConsulteLista();
        }

        public List<CondicaoPagamento> ConsulteListaCondicoesPagamentoAtivas()
        {
            return _repositorioCondicaoPagamento.ConsulteListaDeCondicoesPagamentoAtivas();
        }

        public List<CondicaoPagamento> ConsulteLista(string descricao, string status)
        {
            return _repositorioCondicaoPagamento.ConsulteLista(descricao, status);
        }

        public CondicaoPagamento ConsulteCondicaoPagamentoAVistaPadrao()
        {
            return _repositorioCondicaoPagamento.ConsulteCondicaoPagamentoAVistaPadrao();
        }
    }
}
