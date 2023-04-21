using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.Vendas.ConsultaPrecoProduto
{
    public partial class FormConsultaPrecoProduto : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Produto> _listaDeProdutos;
        private TabelaPreco _tabelaPrecoSelecionado;

        #endregion

        #region " CONSTRUTOR "

        public FormConsultaPrecoProduto()
        {
            InitializeComponent();

            _listaDeProdutos = new List<Produto>();

            PreenchaCboConsultaPor();
            PreenchaCboTabelaPreco();

            this.ActiveControl = txtChave;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void txtComplementoEndereco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboConsultarPor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboTabelaPrecos_EditValueChanged(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void gcProdutos_Click(object sender, EventArgs e)
        {
            SelecioneProduto();
        }

        private void gcProdutos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.PageDown ||
                e.KeyCode == Keys.PageUp ||
                e.KeyCode == Keys.Home ||
                e.KeyCode == Keys.End)
            {
                SelecioneProduto();
            }
        }

        private void txtQuantidadeSimulacao_EditValueChanged(object sender, EventArgs e)
        {
            CalculeValorSimulacao();
        }

        private void txtPercentualDescontoSimulacao_EditValueChanged(object sender, EventArgs e)
        {
            CalculeValorSimulacao();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " PREENCHIMENTO DE CBOS "

        private void PreenchaCboTabelaPreco()
        {
            ServicoTabelaPreco servicoTabelaPreco = new ServicoTabelaPreco();

            var lista = servicoTabelaPreco.ConsulteListaTabelaPrecosAtivas();

            cboTabelaPrecos.Properties.DisplayMember = "NomeTabela";
            cboTabelaPrecos.Properties.ValueMember = "Id";
            cboTabelaPrecos.Properties.DataSource = lista;

            if (lista.Count > 0)
            {
                cboTabelaPrecos.EditValue = lista.FirstOrDefault().Id;
            }
        }

        private void PreenchaCboConsultaPor()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTiposPesquisa>();

            cboConsultarPor.Properties.DisplayMember = "Descricao";
            cboConsultarPor.Properties.ValueMember = "Valor";
            cboConsultarPor.Properties.DataSource = lista;

            cboConsultarPor.EditValue = EnumTiposPesquisa.DESCRICAO;
        }

        #endregion

        #region " PESQUISE E PREENCHA GRID "

        private void Pesquise()
        {
            _listaDeProdutos.Clear();

            EnumTiposPesquisa tipoPesquisa = (EnumTiposPesquisa)cboConsultarPor.EditValue;

            ServicoProduto servicoProduto = new ServicoProduto();

            Produto produtoPesquisa = null;

            switch (tipoPesquisa)
            {
                case EnumTiposPesquisa.CODIGOITEM:
                    produtoPesquisa = servicoProduto.ConsulteProdutoAtivo(txtChave.Text.ToInt());

                    break;
                case EnumTiposPesquisa.CODIGOBARRAS:
                    produtoPesquisa = servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(txtChave.Text);

                    break;
                case EnumTiposPesquisa.CODIGOFABRICANTE:
                    _listaDeProdutos = servicoProduto.ConsulteListaAtivaQueContemCodigoFabricante(txtChave.Text);

                    break;
                case EnumTiposPesquisa.DESCRICAO:
                    _listaDeProdutos = servicoProduto.ConsulteListaAtivaPelaDescricao(txtChave.Text);

                    break;
                case EnumTiposPesquisa.MARCA:
                    _listaDeProdutos = servicoProduto.ConsulteListaAtivaQueContemDescricaoMarca(txtChave.Text);

                    break;
                case EnumTiposPesquisa.FABRICANTE:
                    _listaDeProdutos = servicoProduto.ConsulteListaAtivaQueContemDescricaoFabricante(txtChave.Text);

                    break;
            }

            if (produtoPesquisa != null)
            {
                _listaDeProdutos.Add(produtoPesquisa);
            }

            _listaDeProdutos.ForEach(produtoDaLista =>
            {
                if (produtoDaLista.Principal != null)
                {
                    produtoDaLista.Principal.ProdutoSimilar.CarregueLazyLoad();
                    produtoDaLista.Principal.Categoria.CarregueLazyLoad();
                    produtoDaLista.Principal.Grupo.CarregueLazyLoad();
                    produtoDaLista.Principal.Marca.CarregueLazyLoad();
                    produtoDaLista.Principal.SubGrupo.CarregueLazyLoad();
                }

                produtoDaLista.DadosGerais.Unidade.CarregueLazyLoad();
                produtoDaLista.Vestuario = produtoDaLista.Vestuario ?? new VestuarioProduto();
                produtoDaLista.Vestuario.Cor.CarregueLazyLoad();
                produtoDaLista.Vestuario.Tamanho.CarregueLazyLoad();
            });

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<ProdutoGrid> listaProdutosGrid = new List<ProdutoGrid>();

            if (cboTabelaPrecos.EditValue != null)
            {
                ServicoTabelaPreco servicoTabelaPreco = new ServicoTabelaPreco();
                _tabelaPrecoSelecionado = servicoTabelaPreco.Consulte(cboTabelaPrecos.EditValue.ToInt());
            }
            else
            {
                _tabelaPrecoSelecionado = null;
            }

            foreach (var produto in _listaDeProdutos)
            {
                ProdutoGrid produtoGrid = new ProdutoGrid();

                produtoGrid.Id = produto.Id;

                produtoGrid.CodigoBarras = produto.DadosGerais.CodigoDeBarras;
                produtoGrid.Descricao = produto.DadosGerais.Descricao;
                produtoGrid.Disponivel = (produto.FormacaoPreco.Estoque - produto.FormacaoPreco.EstoqueReservado).ToString();
                produtoGrid.Reservado = produto.FormacaoPreco.EstoqueReservado.ToString();
                produtoGrid.Marca = produto.Principal != null && produto.Principal.Marca != null ? produto.Principal.Marca.Descricao : string.Empty;
                produtoGrid.Fabricante = produto.Principal != null && produto.Principal.Fabricante != null ? produto.Principal.Fabricante.Descricao : string.Empty;
                produtoGrid.Promocao = produto.FormacaoPreco.EhPromocao.GetValueOrDefault() ? produto.FormacaoPreco.ValorPromocao.GetValueOrDefault().ToString("0.00") : string.Empty;
                produtoGrid.Unidade = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                if (!produto.FormacaoPreco.EhPromocao.GetValueOrDefault() && _tabelaPrecoSelecionado != null)
                {
                    var valorUnitario = ServicoPedidoDeVenda.CalculePrecoUnitarioProduto(_tabelaPrecoSelecionado, produto, pesquisarTabelaPreco: false, pesquisarProduto: false);

                    produtoGrid.Venda = valorUnitario.ToString("0.00");
                }

                listaProdutosGrid.Add(produtoGrid);
            }

            gcProdutos.DataSource = listaProdutosGrid;
            gcProdutos.RefreshDataSource();

            SelecioneProduto();
        }

        private void SelecioneProduto()
        {
            var produto = RetorneProdutoSelecionado();

            PreenchaCamposProduto(produto);

            CalculeValorSimulacao();
        }

        private Produto RetorneProdutoSelecionado()
        {
            if (_listaDeProdutos != null && _listaDeProdutos.Count > 0)
            {
                return _listaDeProdutos.FirstOrDefault(x => x.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());
            }

            return null;
        }

        private void PreenchaCamposProduto(Produto produto)
        {
            if (produto != null)
            {
                if (produto.DadosGerais.Foto == null)
                {
                    picFoto.Image = Properties.Resources.produtos;
                }
                else
                {
                    picFoto.Image = TratamentoDeImagens.ConvertByteToImagem(produto.DadosGerais.Foto).Image;
                }

                txtId.Text = produto.Id.ToString();
                txtCodigoDeBarras.Text = produto.DadosGerais.CodigoDeBarras;
                txtCodigoFabricante.Text = produto.Principal != null ? produto.Principal.CodigoFabricante : string.Empty;
                txtUnidade.Text = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Abreviacao : string.Empty;
                txtSituacao.Text = produto.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";

                txtDescricao.Text = produto.DadosGerais.Descricao;
                txtPesoBruto.Text = produto.Principal != null && produto.Principal.PesoBruto != null ? produto.Principal.PesoBruto.Value.ToString("0.00") : string.Empty;
                txtPesoLiquido.Text = produto.Principal != null && produto.Principal.PesoLiquido != null ? produto.Principal.PesoLiquido.Value.ToString("0.00") : string.Empty;

                txtMarca.Text = produto.Principal != null && produto.Principal.Marca != null ? produto.Principal.Marca.Descricao : string.Empty;
                txtFabricante.Text = produto.Principal != null && produto.Principal.Fabricante != null ? produto.Principal.Fabricante.Descricao : string.Empty;
                txtCategoria.Text = produto.Principal != null && produto.Principal.Categoria != null ? produto.Principal.Categoria.Descricao : string.Empty;
                txtGrupo.Text = produto.Principal != null && produto.Principal.Grupo != null ? produto.Principal.Grupo.Descricao : string.Empty;
                txtSubGrupo.Text = produto.Principal != null && produto.Principal.SubGrupo != null ? produto.Principal.SubGrupo.Descricao : string.Empty;
                txtComposicao.Text = produto.Vestuario.Composicao;

                txtTamanho.Text = produto.Vestuario.Tamanho != null ? produto.Vestuario.Tamanho.Descricao : string.Empty;
                txtCor.Text = produto.Vestuario.Cor != null ? produto.Vestuario.Cor.Descricao : string.Empty;
                txtMaterialTecido.Text = produto.Vestuario.MaterialTecido;
                txtModelo.Text = produto.Vestuario.Modelo;
                txtColecao.Text = produto.Vestuario.Colecao;
                txtSexo.Text = produto.Vestuario.SexoProduto != null ? produto.Vestuario.SexoProduto.Value.Descricao() : string.Empty;
                txtReferencia.Text = produto.Vestuario.Referencia;

                txtIdProdutoSimilar.Text = produto.Principal != null && produto.Principal.ProdutoSimilar != null ? produto.Principal.ProdutoSimilar.Id.ToString() : string.Empty;
                txtDescricaoProdutoSimiliar.Text = produto.Principal != null && produto.Principal.ProdutoSimilar != null ? produto.Principal.ProdutoSimilar.DadosGerais.Descricao : string.Empty;

                PreenchaValorUnitarioProduto();
            }
            else
            {
                picFoto.Image = Properties.Resources.produtos;

                txtId.Text = string.Empty;
                txtCodigoDeBarras.Text = string.Empty;
                txtCodigoFabricante.Text = string.Empty;
                txtUnidade.Text = string.Empty;
                txtSituacao.Text = string.Empty;

                txtDescricao.Text = string.Empty;
                txtPesoBruto.Text = string.Empty;
                txtPesoLiquido.Text = string.Empty;

                txtMarca.Text = string.Empty;
                txtCategoria.Text = string.Empty;
                txtGrupo.Text = string.Empty;
                txtSubGrupo.Text = string.Empty;
                txtComposicao.Text = string.Empty;

                txtTamanho.Text = string.Empty;
                txtCor.Text = string.Empty;
                txtMaterialTecido.Text = string.Empty;
                txtModelo.Text = string.Empty;
                txtColecao.Text = string.Empty;
                txtSexo.Text = string.Empty;
                txtReferencia.Text = string.Empty;

                txtIdProdutoSimilar.Text = string.Empty;
                txtDescricaoProdutoSimiliar.Text = string.Empty;

                txtValorUnitario.Text = string.Empty;
            }
        }

        #endregion

        #region " CÁLCULO VALOR SIMULAÇÃO "

        private void PreenchaValorUnitarioProduto()
        {
            var produtoSelecionado = RetorneProdutoSelecionado();

            if (produtoSelecionado == null)
            {
                txtValorUnitario.Text = string.Empty;

                return;
            }

            double valorUnitario = 0;

            if (produtoSelecionado.FormacaoPreco.EhPromocao != null && produtoSelecionado.FormacaoPreco.EhPromocao.Value == true)
            {
                valorUnitario = produtoSelecionado.FormacaoPreco.ValorPromocao.GetValueOrDefault();
            }
            else
            {
                valorUnitario = ServicoPedidoDeVenda.CalculePrecoUnitarioProduto(_tabelaPrecoSelecionado, produtoSelecionado, pesquisarTabelaPreco: false, pesquisarProduto: false);
            }

            txtValorUnitario.Text = valorUnitario.ToString("#,###,##0.00");
        }

        private void CalculeValorSimulacao()
        {
            var produtoSelecionado = RetorneProdutoSelecionado();

            if (produtoSelecionado == null)
            {
                txtValorTotalSimulacao.Text = string.Empty;

                return;
            }

            double valorUnitario = txtValorUnitario.Text.ToDouble();

            var quantidade = txtQuantidadeSimulacao.Text.ToInt();

            var percentualDesconto = txtPercentualDescontoSimulacao.Text.ToDouble();

            double valor = valorUnitario * quantidade;

            valor -= valor * percentualDesconto / 100;

            txtValorTotalSimulacao.Text = valor.ToString("#,###,##0.00");
        }

        #endregion

        #endregion

        #region " ENUMERADORES "

        private enum EnumTiposPesquisa
        {
            [Description("Descrição")]
            DESCRICAO,

            [Description("Código Item")]
            CODIGOITEM,

            [Description("Código Barras")]
            CODIGOBARRAS,

            [Description("Código Fabricante (Serial Number)")]
            CODIGOFABRICANTE,

            [Description("Marca")]
            MARCA,

            [Description("Fabricante")]
            FABRICANTE
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ProdutoGrid
        {
            public int Id { get; set; }

            public string CodigoBarras { get; set; }

            public string Descricao { get; set; }

            public string Marca { get; set; }

            public string Fabricante { get; set; }

            public string Unidade { get; set; }

            public string Reservado { get; set; }

            public string Disponivel { get; set; }

            public string Promocao { get; set; }

            public string Venda { get; set; }
        }

        #endregion
    }
}
