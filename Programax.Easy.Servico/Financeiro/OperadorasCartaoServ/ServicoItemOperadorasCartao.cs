using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.OperadorasCartaoServ
{
    [Funcionalidade(EnumFuncionalidade.OPERADORASCARTAO)]
    public class ServicoItemOperadorasCartao : ServicoAkilSmallBusiness<ItemOperadorasCartao, ValidaItemOperadorasCartao, ConversorItemOperadorasCartao>
    {
        IRepositorioItemOperadorasCartao _repositorioItemOperadorasCartao;

        public ServicoItemOperadorasCartao()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<ItemOperadorasCartao> RetorneRepositorio()
        {
            if (_repositorioItemOperadorasCartao == null)
            {
                _repositorioItemOperadorasCartao = FabricaDeRepositorios.Crie<IRepositorioItemOperadorasCartao>();
            }

            return _repositorioItemOperadorasCartao;
        }

        public List<ItemOperadorasCartao> ConsulteLista(int idOperadora)
        {
            return _repositorioItemOperadorasCartao.ConsulteLista(idOperadora);
        }
    }
}
