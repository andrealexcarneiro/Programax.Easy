using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.ProdutoServ
{
    public class ConversorProduto : ConversorDeObjetoBasico<Produto>, IConversorDeObjeto<Produto>
    {
        public Produto CopieObjetoParaPersistencia(Produto objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            var produtoDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Produto();

            if (objetoDeNegocio == produtoDaBase)
            {
                return produtoDaBase;
            }

            //Foi feito isso para a propriedade Principal sempre vir instanciada.
            if (objetoDeNegocio.Principal == null || objetoDeNegocio.Principal.CodigoFabricante == null)
            {
                objetoDeNegocio.Principal.CodigoFabricante = string.Empty;
            }

            var listaFornecedores = CopieListaFornecedores(objetoDeNegocio, produtoDaBase);

            objetoDeNegocio.DadosGerais.ProdutoEmInventario = produtoDaBase.DadosGerais.ProdutoEmInventario;

            objetoDeNegocio.FormacaoPreco.EstoqueReservado = produtoDaBase.FormacaoPreco.EstoqueReservado;
            //objetoDeNegocio.FormacaoPreco.Estoque = produtoDaBase.FormacaoPreco.Estoque; Foi retirado para salvar o estoque

            CopieTodasAsPropriedades(objetoDeNegocio, produtoDaBase);

            produtoDaBase.ListaFornecedores = listaFornecedores;

            return produtoDaBase;
        }

        private IList<FornecedorProduto> CopieListaFornecedores(Produto objetoDeNegocio, Produto produtoDaBase)
        {
            var listaFornecedores = produtoDaBase.ListaFornecedores;

            listaFornecedores.Clear();

            foreach (var item in objetoDeNegocio.ListaFornecedores)
            {
                var itemCopiado = new FornecedorProduto();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.Produto = produtoDaBase;
                listaFornecedores.Add(itemCopiado);
            }

            return listaFornecedores;
        }
    }
}
