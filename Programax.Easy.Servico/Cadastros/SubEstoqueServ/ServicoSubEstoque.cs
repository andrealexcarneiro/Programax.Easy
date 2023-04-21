using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.SubEstoqueServ
{
    [Funcionalidade(EnumFuncionalidade.SUBESTOQUE)]
    public class ServicoSubEstoque : ServicoAkilSmallBusiness<SubEstoque, ValidacaoSubEstoque, ConversorSubEstoque>
    {
        IRepositorioSubEstoque _repositoriosubestoque;

        public ServicoSubEstoque()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<SubEstoque> RetorneRepositorio()
        {
            if (_repositoriosubestoque == null)
            {
                _repositoriosubestoque = FabricaDeRepositorios.Crie<IRepositorioSubEstoque>();
            }

            return _repositoriosubestoque;
        }

        public List<SubEstoque> ConsulteListaAtiva()
        {
            return _repositoriosubestoque.ConsulteListaAtiva();
        }

        public List<SubEstoque> ConsulteLista(int? id, string descricao, string status)
        {
            return _repositoriosubestoque.ConsulteLista(id, descricao, status);
        }
    }
}
