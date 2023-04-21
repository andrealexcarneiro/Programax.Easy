using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.MotivoCorrecaoEstoqueServ
{
    [Funcionalidade(EnumFuncionalidade.MOTIVOCORRECAOESTOQUE)]
    public class ServicoMotivoCorrecaoEstoque : ServicoAkilSmallBusiness<MotivoCorrecaoEstoque, ValidacaoMotivoCorrecaoEstoque, ConversorMotivoCorrecaoEstoque>
    {
        IRepositorioMotivoCorrecaoEstoque _repositorioMotivoCorrecaoEstoque;

        public ServicoMotivoCorrecaoEstoque()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<MotivoCorrecaoEstoque> RetorneRepositorio()
        {
            if (_repositorioMotivoCorrecaoEstoque == null)
            {
                _repositorioMotivoCorrecaoEstoque = FabricaDeRepositorios.Crie<IRepositorioMotivoCorrecaoEstoque>();
            }

            return _repositorioMotivoCorrecaoEstoque;
        }

        public List<MotivoCorrecaoEstoque> ConsulteLista()
        {
            return _repositorioMotivoCorrecaoEstoque.ConsulteLista();
        }

        public List<MotivoCorrecaoEstoque> ConsulteLista(string descricao, string status)
        {
            return _repositorioMotivoCorrecaoEstoque.ConsulteLista(descricao, status);
        }
    }
}
