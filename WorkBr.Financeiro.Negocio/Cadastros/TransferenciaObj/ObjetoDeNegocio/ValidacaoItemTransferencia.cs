using FluentValidation;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;

namespace Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio
{
    public class ValidacaoItemTransferencia: ValidacaoBase<ItemTransferencia>
    {
        #region " PROPRIEDADES "

        public Transferencia Transferencia { get; set; }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraPrimeiraContagemPreenchida();
            //AssineRegraItemJaEstahEmInventario();
        }

        public override void ValideAtualizacao()
        {
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraPrimeiraContagemPreenchida()
        {
            //Foi retirada esta validação, pois caso o cliente esqueça de colocar os itens retornará, ou seja, irá zerar o estoque

            //MensagemComposta mensagemComposta = new MensagemComposta("Não foi informado a quantidade da 1ª contagem para o item {0} - {1}");

            //RuleFor(item => item.QuantidadeContagemUm)
            //    .Must((item, QuantidadeContagemUm) =>
            //    {
            //        if (item.QuantidadeContagemUm == null)
            //        {
            //            mensagemComposta.ListaDeParametros.Add(item.Produto.Id);
            //            mensagemComposta.ListaDeParametros.Add(item.Produto.DadosGerais.Descricao);
            //        }
            //        return QuantidadeContagemUm != null;
            //    })
            //    .WithMessage(mensagemComposta)
            //    .When(item => Inventario.ContagemAtual > 1 || Inventario.Status == EnumStatusInventario.CONSOLIDADO);
        }

        //private void AssineRegraItemJaEstahEmInventario()
        //{
        //    MensagemComposta mensagemComposta = new MensagemComposta("Item {0} - {1} já está em inventário.");

        //    RuleFor(itemTransferencia => itemTransferencia.Produto)
        //        .Must(produto =>{

        //            mensagemComposta.ListaDeParametros.Add(produto.Id);
        //            mensagemComposta.ListaDeParametros.Add(produto.DadosGerais.Descricao);

        //            return ProdutoNaoEstahEmInventario(produto);
        //        })
        //        .WithMessage(mensagemComposta)
        //        .When(itemTransferencia => Transferencia.Id == 0);
        //}

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        private bool ProdutoNaoEstahEmInventario(Produto produto)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioInventario>();

            bool produtoEstahEmInventario = repositorio.ConsulteProdutoEstahEmInventario(produto);

            return !produtoEstahEmInventario;
        }

        #endregion
    }
}
