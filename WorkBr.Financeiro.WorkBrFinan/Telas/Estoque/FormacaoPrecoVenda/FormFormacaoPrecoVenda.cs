using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.FabricanteServ;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Estoque.FormacaoPrecoVenda
{
    public partial class FormFormacaoPrecoVenda : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Produto> _listaProdutos;

        #endregion

        #region " CONSTRUTOR "

        public FormFormacaoPrecoVenda()
        {
            InitializeComponent();

            _listaProdutos = new List<Produto>();

            PreenchaCboFabricantes();
            PreenchaCboMarcas();
            PreenchaCboCategorias();

            this.ActiveControl = txtNomeProduto;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboCategorias_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboGrupos();
        }

        private void cboGrupos_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboSubGrupos();
        }

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            var produto = _listaProdutos.FirstOrDefault(produtoLista => produtoLista.Id == txtIdProduto.Text.ToInt());

            if (produto != null)
            {
                produto.FormacaoPreco = produto.FormacaoPreco ?? new FormacaoPrecoProduto();

                if (rdbNovoValorProduto.Checked)
                {
                    produto.FormacaoPreco.EhPromocao = false;
                    produto.FormacaoPreco.ValorVenda = txtValorVendaProduto.Text.ToDouble();
                }
                else
                {
                    produto.FormacaoPreco.EhPromocao = true;
                    produto.FormacaoPreco.ValorPromocao = txtValorVendaProduto.Text.ToDouble();
                }

                LimpeCamposProduto();

                PreenchaGrid();
            }
            else
            {
                MessageBox.Show("Produto não encontrado no grid.");
            }
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeCamposProduto();
        }

        private void btnAtualizarValorVendaTodosProdutos_Click(object sender, EventArgs e)
        {
            foreach (var produto in _listaProdutos)
            {
                produto.FormacaoPreco = produto.FormacaoPreco ?? new FormacaoPrecoProduto();

                if (rdbPromocaoTodosProdutos.Checked)
                {
                    produto.FormacaoPreco.ValorPromocao = txtValorVendaTodosProdutos.Text.ToDouble();
                    produto.FormacaoPreco.EhPromocao = true;
                }
                else
                {
                    produto.FormacaoPreco.ValorVenda = txtValorVendaTodosProdutos.Text.ToDouble();
                    produto.FormacaoPreco.EhPromocao = false;
                }
            }

            PreenchaGrid();
        }

        private void txtIdProduto_Properties_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdProduto.Text))
            {
                var produto = _listaProdutos.FirstOrDefault(produtoLista => produtoLista.Id == txtIdProduto.Text.ToInt());

                PreenchaCamposProduto(produto);
            }
        }

        private void txtCodigoDeBarrasProduto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoDeBarrasProduto.Text))
            {
                var produto = _listaProdutos.FirstOrDefault(produtoLista => produtoLista.DadosGerais.CodigoDeBarras == txtCodigoDeBarrasProduto.Text);

                PreenchaCamposProduto(produto);
            }
        }

        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            SelecioneProdutoGrid();
        }

        private void gcItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneProdutoGrid();
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (_listaProdutos.Count == 0)
            {
                MessageBox.Show("Nenhum produto adicionado no grid.");

                return;
            }

            if (MessageBox.Show("Deseja atualizar o preço desses produtos", "Atualizar preço de venda", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Action actionSalvar = () =>
                {
                    ServicoProduto servicoProduto = new ServicoProduto();
                    servicoProduto.AtualizePrecoVendaProdutos(_listaProdutos);
                };

                TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Foram atualizados os preços de todos os itens.");
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeCamposProduto();
            _listaProdutos.Clear();
            PreenchaGrid();
            rdbValorVendaTodosProdutos.Checked = true;
            txtValorVendaTodosProdutos.Text = string.Empty;
            txtNomeProduto.Text = string.Empty;
            cboMarcas.EditValue = null;
            cboFabricantes.EditValue = null;
            cboCategorias.EditValue = null;
            cboGrupos.EditValue = null;
            cboSubGrupos.EditValue = null;

            txtNomeProduto.Focus();
        }

        private void btnPesquisaProdutos_Click(object sender, EventArgs e)
        {
            string descricao = txtNomeProduto.Text;
            Marca marca = cboMarcas.EditValue != null ? new Marca { Id = cboMarcas.EditValue.ToInt() } : null;
            Fabricante fabricante = cboFabricantes.EditValue != null ? new Fabricante { Id = cboFabricantes.EditValue.ToInt() } : null;
            Categoria categoria = cboCategorias.EditValue != null ? new Categoria { Id = cboCategorias.EditValue.ToInt() } : null;
            Grupo grupo = cboGrupos.EditValue != null ? new Grupo { Id = cboGrupos.EditValue.ToInt() } : null;
            SubGrupo subgrupo = cboSubGrupos.EditValue != null ? new SubGrupo { Id = cboSubGrupos.EditValue.ToInt() } : null;

            ServicoProduto servicoProduto = new ServicoProduto();
            _listaProdutos = servicoProduto.ConsulteListasProdutosAtivos(descricao, marca, fabricante, categoria, grupo, subgrupo);

            PreenchaGrid();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

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

        private void PreenchaGrid()
        {
            List<ItemGrid> listaItensGrid = new List<ItemGrid>();

            foreach (var produto in _listaProdutos)
            {
                ItemGrid itemGrid = new ItemGrid();

                itemGrid.Id = produto.Id;
                itemGrid.CodigoDeBarras = produto.DadosGerais.CodigoDeBarras;
                itemGrid.IdProduto = produto.Id;
                itemGrid.Cor = produto.Vestuario != null && produto.Vestuario.Cor != null ? produto.Vestuario.Cor.Descricao : string.Empty;
                itemGrid.Descricao = produto.DadosGerais.Descricao;

                itemGrid.MarcaFabricante = produto.Principal != null && produto.Principal != null && produto.Principal.Marca != null ? produto.Principal.Marca.Descricao : string.Empty;
                itemGrid.Modelo = produto.Vestuario != null ? produto.Vestuario.Modelo : string.Empty;
                itemGrid.Sexo = produto.Vestuario != null && produto.Vestuario.SexoProduto != null ? produto.Vestuario.SexoProduto.Value.Descricao() : string.Empty;
                itemGrid.Tamanho = produto.Vestuario != null && produto.Vestuario.Tamanho != null ? produto.Vestuario.Tamanho.Descricao : string.Empty;
                itemGrid.Unidade = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Descricao : string.Empty;
                itemGrid.ValorVenda = produto.FormacaoPreco.EhPromocao.GetValueOrDefault() ? produto.FormacaoPreco.ValorPromocao.GetValueOrDefault().ToString("#,##0.00") : produto.FormacaoPreco.ValorVenda.GetValueOrDefault().ToString("#,##0.00");
                itemGrid.Promocao = produto.FormacaoPreco.EhPromocao.GetValueOrDefault();

                listaItensGrid.Add(itemGrid);
            }

            gcItens.DataSource = listaItensGrid;
            gcItens.RefreshDataSource();
        }

        private void LimpeCamposProduto()
        {
            PreenchaCamposProduto(null);
        }

        private void PreenchaCamposProduto(Produto produto)
        {
            if (produto != null)
            {
                txtCodigoDeBarrasProduto.Text = produto.DadosGerais.CodigoDeBarras;
                txtIdProduto.Text = produto.Id.ToString();
                txtDescricaoProduto.Text = produto.DadosGerais.Descricao;

                produto.FormacaoPreco = produto.FormacaoPreco ?? new FormacaoPrecoProduto();

                if (produto.FormacaoPreco.EhPromocao.GetValueOrDefault())
                {
                    rdbPromocaoProduto.Checked = true;
                    txtValorVendaProduto.Text = produto.FormacaoPreco.ValorPromocao.GetValueOrDefault().ToString("0.00");
                }
                else
                {
                    rdbNovoValorProduto.Checked = true;
                    txtValorVendaProduto.Text = produto.FormacaoPreco.ValorVenda.GetValueOrDefault().ToString("0.00");
                }

                txtValorVendaProduto.Focus();
            }
            else
            {
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtIdProduto.Text = string.Empty;
                txtDescricaoProduto.Text = string.Empty;
                rdbNovoValorProduto.Checked = true;
                txtValorVendaProduto.Text = string.Empty;

                txtCodigoDeBarrasProduto.Focus();
            }
        }

        private void SelecioneProdutoGrid()
        {
            if (_listaProdutos != null && _listaProdutos.Count > 0)
            {
                var produto = _listaProdutos.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

                PreenchaCamposProduto(produto);
            }
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ItemGrid
        {
            public int Id { get; set; }

            public int IdProduto { get; set; }

            public string CodigoDeBarras { get; set; }

            public string Descricao { get; set; }

            public string Unidade { get; set; }

            public string MarcaFabricante { get; set; }

            public string Tamanho { get; set; }

            public string Cor { get; set; }

            public string Sexo { get; set; }

            public string Modelo { get; set; }

            public bool Promocao { get; set; }

            public string ValorVenda { get; set; }
        }

        #endregion
    }
}
