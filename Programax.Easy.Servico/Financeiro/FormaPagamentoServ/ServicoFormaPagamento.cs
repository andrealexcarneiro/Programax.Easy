using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.FormaPagamentoServ
{
    [Funcionalidade(EnumFuncionalidade.FORMAPAGAMENTO)]
    public class ServicoFormaPagamento : ServicoAkilSmallBusiness<FormaPagamento, ValidacaoFormaPagamento, ConversorFormaPagamento>
    {
        IRepositorioFormaPagamento _repositorioFormaPagamento;

        public ServicoFormaPagamento()
        {
            RetorneRepositorio();
        }

        public ServicoFormaPagamento(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<FormaPagamento> RetorneRepositorio()
        {
            if (_repositorioFormaPagamento == null)
            {
                _repositorioFormaPagamento = FabricaDeRepositorios.Crie<IRepositorioFormaPagamento>();
            }

            return _repositorioFormaPagamento;
        }

        public List<FormaPagamento> ConsulteListaFormasDePagamentoAtivas()
        {
            return _repositorioFormaPagamento.ConsulteListaFormasDePagamentoAtivas();
        }

        public List<FormaPagamento> ConsulteLista(string descricao, string status)
        {
            return _repositorioFormaPagamento.ConsulteLista(descricao, status);
        }

        public List<FormaPagamento> ConsulteListaAtivos()
        {
            return _repositorioFormaPagamento.ConsulteListaAtivos();
        }

        public FormaPagamento ConsultePeloTipo(EnumTipoFormaPagamento tipoFormaPagamento)
        {
            return _repositorioFormaPagamento.ConsultePeloTipo(tipoFormaPagamento);
        }
    }
}
