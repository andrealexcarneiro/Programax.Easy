using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Microsoft.Reporting.WinForms;

namespace Programax.Easy.Report.Relatorios.Estoque
{
    public class RelatorioInventario : RelatorioPadrao<Produto>
    {
        public RelatorioInventario()
        {
            //caminhoRelatorio = @"Estoque\RelatorioInventario.rdlc";
            //nomeRelatorio = "Relatório de Inventário";
            caminhoRelatorio = @"Estoque\estoqueII.rdlc";
           // nomeRelatorio = "Relatório de Inventário";
        }

        protected override void InformeDataSets(LocalReport localReport, List<Produto> listaDeObjetos)
        {
            List<ProdutoRelatorioInventarioAux> listaProdutosRelatorioInventario = new List<ProdutoRelatorioInventarioAux>();

            foreach (var item in listaDeObjetos)
            {
                ProdutoRelatorioInventarioAux produtoRelatorioInventario = new ProdutoRelatorioInventarioAux();

                produtoRelatorioInventario.Id = item.Id;
                produtoRelatorioInventario.Descricao = item.DadosGerais.Descricao;
                produtoRelatorioInventario.Unidade = item.DadosGerais.Unidade != null ? item.DadosGerais.Unidade.Abreviacao : string.Empty;
                produtoRelatorioInventario.Grupo = item.Principal.Grupo != null ? item.Principal.Grupo.Descricao : string.Empty;
                produtoRelatorioInventario.Marca = item.Principal.Marca != null ? item.Principal.Marca.Descricao : string.Empty;

                listaProdutosRelatorioInventario.Add(produtoRelatorioInventario);
            }

            localReport.DataSources.Add(new ReportDataSource("ProdutoInventario", listaProdutosRelatorioInventario));
        }
    }
}
