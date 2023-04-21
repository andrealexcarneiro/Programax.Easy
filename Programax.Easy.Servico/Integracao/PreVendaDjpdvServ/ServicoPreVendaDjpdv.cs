using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Servicos;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using System.Transactions;

namespace Programax.Easy.Servico.Integracao.PreVendaDjpdvServ
{
    public class ServicoPreVendaDjpdv : ServicoBase<PreVendaDjpdv, ValidacaoPreVendaDjpdv, ConversorPreVendaDjpdv>
    {
        IRepositorioPreVendaDjpdv _repositorioPreVendaDjpdv;

        public ServicoPreVendaDjpdv()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<PreVendaDjpdv> RetorneRepositorio()
        {
            if (_repositorioPreVendaDjpdv == null)
            {
                _repositorioPreVendaDjpdv = FabricaDeRepositorios.Crie<IRepositorioPreVendaDjpdv>();
            }

            return _repositorioPreVendaDjpdv;
        }

        public override int Cadastre(PreVendaDjpdv objetoDeNegocio)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda(false, false);
                var venda = servicoPedidoDeVenda.Consulte(objetoDeNegocio.PedidoDeVendaId);

                venda.PedidoExportadoParaPdvEcf = true;

                servicoPedidoDeVenda.Atualize(venda);

                int id = base.Cadastre(objetoDeNegocio);

                scope.Complete();

                return id;
            }
        }

        public List<PreVendaDjpdv> ConsulteLista()
        {
            return _repositorioPreVendaDjpdv.ConsulteLista();
        }

        public void Exclua(PreVendaDjpdv objeto)
        {
            _repositorioPreVendaDjpdv.Exclua(objeto);
        }

        public void ExcluaLista(List<PreVendaDjpdv> atualizacoes)
        {
            _repositorioPreVendaDjpdv.ExcluaLista(atualizacoes);
        }
    }
}
