using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Report.RelatoriosDevExpress.Estoque;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.FabricanteServ;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.CategoriaServ;


namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioMovimentacaoItens : FormularioBase
    {
        #region " CONSTRUTOR "

        public FormRelatorioMovimentacaoItens()
        {
            InitializeComponent();

            PreenchaCboOrigem();
            PreenchaCboTipo();
            PreenchaCboFabricantes();
            PreenchaCboMarcas();
            PreenchaCboCategorias();

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
            string descricao = txtDescricaoProduto.Text;
            Marca marca = cboMarcas.EditValue != null ? new Marca { Id = cboMarcas.EditValue.ToInt() } : null;
            if (marca != null)
                marca.Descricao = cboMarcas.Text;
            
            
            Fabricante fabricante = cboFabricantes.EditValue != null ? new Fabricante { Id = cboFabricantes.EditValue.ToInt() } : null;
            if (fabricante != null)            
                fabricante.Descricao = cboFabricantes.Text;
            
            
            Categoria categoria = cboCategorias.EditValue != null ? new Categoria { Id = cboCategorias.EditValue.ToInt() } : null;
            if (categoria != null)            
                categoria.Descricao = cboCategorias.Text;
                        
            Grupo grupo = cboGrupos.EditValue != null ? new Grupo { Id = cboGrupos.EditValue.ToInt() } : null;
            if (grupo != null)            
                grupo.Descricao = cboGrupos.Text;
            
            
            SubGrupo subgrupo = cboSubGrupos.EditValue != null ? new SubGrupo { Id = cboSubGrupos.EditValue.ToInt() } : null;
            if (subgrupo != null)            
                subgrupo.Descricao = cboSubGrupos.Text;
            
            
            int? produtoId = txtIdProduto.Text.ToIntNullabel();
            DateTime? dataInicial = txtDataInicialMovimentacao.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinalMovimentacao.Text.ToDateNullabel();
            EnumTipoMovimentacao? tipoMovimentacao = (EnumTipoMovimentacao?)cboTipoMovimentacao.EditValue;
            EnumOrigemMovimentacao? origemMovimentacao = (EnumOrigemMovimentacao?)cboOrigemMovimentacao.EditValue;

            RelatorioMovimentacaoItens relatorioMovimentacaoItens = new RelatorioMovimentacaoItens(produtoId, dataInicial, dataFinal, tipoMovimentacao, origemMovimentacao,
                                                                                                    descricao, marca, fabricante, subgrupo, categoria, grupo);

            TratamentosDeTela.ExibirRelatorio(relatorioMovimentacaoItens);
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

        private void PreenchaCboTipo()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoMovimentacao>();

            lista.RemoveAt(2);

            lista.Insert(0, null);

            cboTipoMovimentacao.Properties.DataSource = lista;
            cboTipoMovimentacao.Properties.DisplayMember = "Descricao";
            cboTipoMovimentacao.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboOrigem()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumOrigemMovimentacao>();

            lista.Insert(0, null);

            cboOrigemMovimentacao.Properties.DataSource = lista;
            cboOrigemMovimentacao.Properties.DisplayMember = "Descricao";
            cboOrigemMovimentacao.Properties.ValueMember = "Valor";
        }

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

        private void PreenchaCboCategorias()
        {
            ServicoCategoria servicoCategoria = new ServicoCategoria();

            var categorias = servicoCategoria.ConsulteListaAtiva();

            categorias.Insert(0, null);

            cboCategorias.Properties.DataSource = categorias;
            cboCategorias.Properties.DisplayMember = "Descricao";
            cboCategorias.Properties.ValueMember = "Id";
        }

        private void PreenchaCboGrupos()
        {
            ServicoGrupo servicoGrupo = new ServicoGrupo();

            var grupos = servicoGrupo.ConsulteListaAtivos(new Categoria { Id = cboCategorias.EditValue.ToInt() });

            grupos.Insert(0, null);

            cboGrupos.Properties.DataSource = grupos;
            cboGrupos.Properties.DisplayMember = "Descricao";
            cboGrupos.Properties.ValueMember = "Id";

            if (string.IsNullOrEmpty(cboGrupos.Text))
            {
                cboGrupos.EditValue = null;
            }

            PreenchaCboSubGrupos();
        }

        private void PreenchaCboSubGrupos()
        {
            Grupo grupo = new Grupo();

            if (cboGrupos.EditValue != null)
            {
                grupo.Id = cboGrupos.EditValue.ToInt();
            }

            ServicoSubGrupo servicoSubGrupo = new ServicoSubGrupo();

            var grupos = servicoSubGrupo.ConsulteListaAtiva(grupo);

            grupos.Insert(0, null);

            cboSubGrupos.Properties.DataSource = grupos;
            cboSubGrupos.Properties.DisplayMember = "Descricao";
            cboSubGrupos.Properties.ValueMember = "Id";

            if (string.IsNullOrEmpty(cboSubGrupos.Text))
            {
                cboSubGrupos.EditValue = null;
            }
        }

        private void PreenchaCboMarcas()
        {
            ServicoMarca servicoMarcas = new ServicoMarca();

            var marcas = servicoMarcas.ConsulteListaAtiva();

            marcas.Insert(0, null);

            cboMarcas.Properties.DataSource = marcas;
            cboMarcas.Properties.DisplayMember = "Descricao";
            cboMarcas.Properties.ValueMember = "Id";

            if (cboMarcas.EditValue != null)
            {
                if (!marcas.Exists(marca => marca != null && marca.Id == cboMarcas.EditValue.ToInt()))
                {
                    cboMarcas.EditValue = null;
                }
            }
        }


        private void PreenchaCboFabricantes()
        {
            ServicoFabricante servicoFabricante = new ServicoFabricante();

            var fabricantes = servicoFabricante.ConsulteListaAtiva();

            fabricantes.Insert(0, null);

            cboFabricantes.Properties.DataSource = fabricantes;
            cboFabricantes.Properties.DisplayMember = "Descricao";
            cboFabricantes.Properties.ValueMember = "Id";

            if (cboFabricantes.EditValue != null)
            {
                if (!fabricantes.Exists(fabricante => fabricante != null && fabricante.Id == cboFabricantes.EditValue.ToInt()))
                {
                    cboFabricantes.EditValue = null;
                }
            }
        }

        #endregion

        private void cboMarcas_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void cboCategorias_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboGrupos();
        }

        private void cboGrupos_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboSubGrupos();
        }
    }
}
