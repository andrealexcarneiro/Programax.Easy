using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Report.RelatoriosDevExpress.Estoque;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Windows.Forms;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioMovimentacaoSaidaItens : FormularioBase
    {
        #region " CONSTRUTOR "

        public FormRelatorioMovimentacaoSaidaItens()
        {
            InitializeComponent();
            
            this.ActiveControl = txtCodigoDeBarrasProduto;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            int? produtoId = txtIdProduto.Text.ToIntNullabel();
            DateTime? dataInicial = txtDataInicialMovimentacao.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinalMovimentacao.Text.ToDateNullabel();           

            RelatorioMovimentacaoSaidas relatorioMovimentacaoSaidaItens = new RelatorioMovimentacaoSaidas(produtoId, dataInicial, dataFinal);

            TratamentosDeTela.ExibirRelatorio(relatorioMovimentacaoSaidaItens);
            this.Cursor = Cursors.Default;
        }

        private void btnPesquisaProduto2_Click(object sender, EventArgs e)
        {
            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProduto();

            if (produto != null)
            {
                PreenchaProduto(produto);
            }
        }

        private void txtIdProduto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdProduto.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoAtivo(txtIdProduto.Text.ToInt());

                PreenchaProduto(produto, exibirMensagemDeNaoEncontrado: true);
            }
            else
            {
                PreenchaProduto(null);
            }
        }

        private void txtCodigoDeBarrasProduto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoDeBarrasProduto.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(txtCodigoDeBarrasProduto.Text);

                PreenchaProduto(produto, exibirMensagemDeNaoEncontrado: true);
            }
            else
            {
                PreenchaProduto(null);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "
        
        private void PreenchaProduto(Produto produto, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (produto != null)
            {
                txtIdProduto.Text = produto.Id.ToString();
                txtCodigoDeBarrasProduto.Text = produto.DadosGerais.CodigoDeBarras;
                txtDescricaoProduto.Text = produto.DadosGerais.Descricao;
            }
            else
            {
                txtIdProduto.Text = string.Empty;
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtDescricaoProduto.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Produto nao encontrado!", "Produto não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoDeBarrasProduto.Focus();
                }
            }
        }
        
        #endregion
        
    }
}
