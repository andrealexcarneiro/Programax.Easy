using System;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Estoque.Enumeradores;

namespace Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.Repositorio
{
    public interface IRepositorioEntradaMercadoria : IRepositorioBase<EntradaMercadoria>
    {
        EntradaMercadoria Consulte(string numeroNota, string serie, Pessoa fornecedor);

        List<EntradaMercadoria> ConsulteLista(DateTime? dataInicialEmissao, DateTime? dataFinalEmissao, DateTime? dataInicialEntrada, DateTime? dataFinalEntrada, string numeroNota, string razaoSocialFornecedor, EnumStatusEntrada? status, int tipo);

        List<EntradaMercadoria> ConsulteListaNumero(string numeroNota, int tipo);
        EntradaMercadoria ConsulteNotaEntrada(string numeroNota);

        ItemEntrada ConsulteUltimaEntradaProduto(Produto produto);
    }
}
