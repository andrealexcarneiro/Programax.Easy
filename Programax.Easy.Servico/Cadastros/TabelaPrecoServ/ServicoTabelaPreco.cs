using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;

namespace Programax.Easy.Servico.Cadastros.TabelaPrecoServ
{
    [Funcionalidade(EnumFuncionalidade.TABELAPRECO)]
    public class ServicoTabelaPreco : ServicoAkilSmallBusiness<TabelaPreco, ValidacaoTabelaPreco, ConversorTabelaPreco>
    {
        IRepositorioTabelaPreco _repositorioTabelaPreco;

        public ServicoTabelaPreco()
        {
            RetorneRepositorio();
        }

        public ServicoTabelaPreco(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override Infraestrutura.Negocio.ObjetosDeNegocio.IRepositorioBase<TabelaPreco> RetorneRepositorio()
        {
            if (_repositorioTabelaPreco == null)
            {
                _repositorioTabelaPreco = FabricaDeRepositorios.Crie<IRepositorioTabelaPreco>();
            }

            return _repositorioTabelaPreco;
        }

        public List<TabelaPreco> ConsulteListaTabelaPrecosAtivas()
        {
            return _repositorioTabelaPreco.ConsulteListaTabelaPrecosAtivas();
        }

        public List<TabelaPreco> ConsulteLista()
        {
            return _repositorioTabelaPreco.ConsulteLista();
        }

        public List<TabelaPreco> ConsulteLista(string descricao, string status, DateTime? dataValidade = null)
        {
            return _repositorioTabelaPreco.ConsulteLista(descricao, status, dataValidade);
        }
    }
}
