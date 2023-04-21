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
    public class ServicoOperadorasCartao : ServicoAkilSmallBusiness<OperadorasCartao, ValidaOperadorasCartao, ConversorOperadorasCartao>
    {
        IRepositorioOperadorasCartao _repositorioOperadorasCartao;

        public ServicoOperadorasCartao()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<OperadorasCartao> RetorneRepositorio()
        {
            if (_repositorioOperadorasCartao == null)
            {
                _repositorioOperadorasCartao = FabricaDeRepositorios.Crie<IRepositorioOperadorasCartao>();
            }

            return _repositorioOperadorasCartao;
        }

        public List<OperadorasCartao> ConsulteLista(string descricao, string status)
        {
            return _repositorioOperadorasCartao.ConsulteLista(descricao, status);
        }

        public List<OperadorasCartao> ConsulteLista()
        {
            return _repositorioOperadorasCartao.ConsulteLista();
        }

        public OperadorasCartao ConsulteOperadorasPeloIdInformado(int idOperadora)
        {
           return _repositorioOperadorasCartao.ConsulteOperadorasPeloIdInformado(idOperadora);
        }
    }
}
