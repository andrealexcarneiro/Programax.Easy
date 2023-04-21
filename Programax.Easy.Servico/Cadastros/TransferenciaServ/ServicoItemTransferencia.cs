using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Collections.Generic;

namespace Programax.Easy.Servico.Cadastros.TransferenciaServ
{
    [Funcionalidade(EnumFuncionalidade.TRANSFERENCIASUBESTOQUE)]
    public class ServicoItemTransferencia : ServicoAkilSmallBusiness<ItemTransferencia, ValidacaoItemTransferencia, ConversorItemTransferencia>
    {
        private IRepositorioItemTransferencia _repositorioItemTransferencia;

        public ServicoItemTransferencia()
        {
            RetorneRepositorio();
        }

        public ServicoItemTransferencia(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public List<ItemTransferencia> ConsulteListas(int Id)
        {
            return _repositorioItemTransferencia.ConsulteLista(Id);
        }

        public List<ItemTransferencia> ConsulteProduto(int Id)
        {
            return _repositorioItemTransferencia.ConsulteProduto(Id);
        }

        public override IRepositorioBase<ItemTransferencia> RetorneRepositorio()
        {
            if (_repositorioItemTransferencia == null)
            {
                _repositorioItemTransferencia = FabricaDeRepositorios.Crie<IRepositorioItemTransferencia>();
            }

            return _repositorioItemTransferencia;
        }
    }
}
