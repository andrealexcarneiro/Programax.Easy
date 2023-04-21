using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;

namespace Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio
{
    public class ValidacaoItemMovimentacao : ValidacaoBase<ItemMovimentacao>
    {
        public override void ValideInclusao()
        {
            RegrasComunsAoInserirEAtualizar();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAoInserirEAtualizar();
        }

        protected virtual void RegrasComunsAoInserirEAtualizar()
        {
            AssineRegraProdutoEmInventario();
        }

        protected void AssineRegraProdutoEmInventario()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("Item com código {0} - {1} está em inventário.");

            RuleFor(item => item.Produto)
                .Must(produto =>
                {
                    var repositorio = FabricaDeRepositorios.Crie<IRepositorioProduto>();
                    
                    var produtoDaBase = repositorio.Consulte(produto.Id);

                    mensagemComposta.ListaDeParametros.Add(produtoDaBase.Id);
                    mensagemComposta.ListaDeParametros.Add(produtoDaBase.DadosGerais.Descricao);

                    return !produtoDaBase.DadosGerais.ProdutoEmInventario;
                })
                .WithMessage(mensagemComposta);
        }
    }
}
