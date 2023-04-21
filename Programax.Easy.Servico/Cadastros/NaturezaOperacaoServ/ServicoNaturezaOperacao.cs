using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ
{
    [Funcionalidade(EnumFuncionalidade.NATUREZAOPERACAO)]
    public class ServicoNaturezaOperacao : ServicoAkilSmallBusiness<NaturezaOperacao, ValidacaoNaturezaOperacao, ConversorNaturezaOperacao>
    {
        IRepositorioNaturezaOperacao _repositorioNaturezaOperacao;

        public ServicoNaturezaOperacao()
        {
            RetorneRepositorio();
        }

        public ServicoNaturezaOperacao(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<NaturezaOperacao> RetorneRepositorio()
        {
            if (_repositorioNaturezaOperacao == null)
            {
                _repositorioNaturezaOperacao = FabricaDeRepositorios.Crie<IRepositorioNaturezaOperacao>();
            }

            return _repositorioNaturezaOperacao;
        }

        public List<NaturezaOperacao> ConsulteListaAtiva()
        {
            return _repositorioNaturezaOperacao.ConsulteListaAtiva();
        }

        public List<NaturezaOperacao> ConsulteLista(int? id, string descricao, string status)
        {
            return _repositorioNaturezaOperacao.ConsulteLista(id, descricao, status);
        }

        public NaturezaOperacao ConsulteNaturezaOperacaoPorCfop(string codigoCfop)
        {
            return _repositorioNaturezaOperacao.ConsulteNaturezaOperacaoPorCfop(codigoCfop);
        }
    }
}
