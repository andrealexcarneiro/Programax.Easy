using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.LiberacaoDocumentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.LiberacaoDocumentoObj.Repositorio;
using Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ;

namespace Programax.Easy.Servico.Vendas.LiberacaoDocumentoServ
{
    [Funcionalidade(EnumFuncionalidade.LIBERACAODOCUMENTO)]
    public class ServicoLiberacaoDocumento : ServicoAkilSmallBusiness<LiberacaoDocumento, ValidacaoLiberacaoDocumento, ConversorLiberacaoDocumento>
    {
        #region " VARIÁVEIS PRIVADAS "

        IRepositorioLiberacaoDocumento _repositorioLiberacaoDocumento;

        #endregion

        #region " CONSTRUTOR "

        public ServicoLiberacaoDocumento()
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<LiberacaoDocumento> RetorneRepositorio()
        {
            if (_repositorioLiberacaoDocumento == null)
            {
                _repositorioLiberacaoDocumento = FabricaDeRepositorios.Crie<IRepositorioLiberacaoDocumento>();
            }

            return _repositorioLiberacaoDocumento;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void LibereDocumento(LiberacaoDocumento liberacao)
        {
            if (liberacao.TipoDocumento == EnumTipoDocumentoLiberacao.PEDIDODEVENDAS)
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda(false, false);

                var pedidoDeVenda = servicoPedidoDeVenda.Consulte(liberacao.Id);

                pedidoDeVenda.StatusPedidoVenda = EnumStatusPedidoDeVenda.RESERVADO;
                pedidoDeVenda.DataFechamento = DateTime.Now.Date;

                servicoPedidoDeVenda.Atualize(pedidoDeVenda);
            }
            else
            {
                ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda(false, false);

                var troca = servicoTrocaPedidoDeVenda.Consulte(liberacao.Id);

                troca.Status = EnumStatusTrocaPedidoDeVenda.RESERVADO;
                troca.DataFechamento = DateTime.Now.Date;

                servicoTrocaPedidoDeVenda.Atualize(troca);
            }
        }

        public void RecuseDocumento(LiberacaoDocumento liberacao)
        {
            if (liberacao.TipoDocumento == EnumTipoDocumentoLiberacao.PEDIDODEVENDAS)
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda(false, false);

                var pedidoDeVenda = servicoPedidoDeVenda.Consulte(liberacao.Id);

                servicoPedidoDeVenda.CanceleOuRecusePedidoDeVenda(pedidoDeVenda, EnumStatusPedidoDeVenda.RECUSADO);
            }
            else if (liberacao.TipoDocumento == EnumTipoDocumentoLiberacao.TROCAPEDIDODEVENDAS)
            {
                ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();
                var troca = servicoTrocaPedidoDeVenda.Consulte(liberacao.Id);

                servicoTrocaPedidoDeVenda.CanceleOuRecuseTrocaPedidoDeVenda(troca, EnumStatusTrocaPedidoDeVenda.RECUSADO);
            }
        }

        #endregion

        #region " CONSULTAS "

        public List<LiberacaoDocumento> ConsulteLista(DateTime? dataInicial, DateTime? dataFinal, Pessoa atendente, Pessoa vendedor, EnumTipoDocumentoLiberacao? tipoDocumentoLiberacao)
        {
            return _repositorioLiberacaoDocumento.ConsulteLista(dataInicial, dataFinal, atendente, vendedor, tipoDocumentoLiberacao);
        }

        #endregion
    }
}
