using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormCancelarItemPdv : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private Parametros _parametros;
        private List<ItemPedidoDeVenda> _listaItensPedidoVenda;
        private DialogResult _resultado;

        private bool _pesquiseProdutoPeloId;

        #endregion

        #region " PROPRIEDADES "

        public List<ItemPedidoDeVenda> ListaItens
        {
            get
            {
                return _listaItensPedidoVenda;
            }
        }

        #endregion

        #region " CONSTRUTOR "

        public FormCancelarItemPdv(List<ItemPedidoDeVenda> listaItensPedidoVenda)
        {
            InitializeComponent();

            _listaItensPedidoVenda = listaItensPedidoVenda.CloneCompleto();
            PreenchaParametros();

            this.ActiveControl = txtCodigoBarras;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult CanceleItem()
        {
            this.AbrirTelaModal(true);

            return _resultado;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormCancelarItemPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Sair();
            }
            else if (e.KeyCode == Keys.F2)
            {
                AltereModoPesquisa();
            }
            if (e.KeyCode == Keys.F3)
            {
                ExcluaProduto();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            ExcluaProduto();
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExcluaProduto();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void ExcluaProduto()
        {
            
            bool pesquiseProdutoPeloId = _pesquiseProdutoPeloId;

            var indiceSeparador = txtCodigoBarras.Text.IndexOf("*");
                        
            var codigoBarrasOuIdProduto = txtCodigoBarras.Text.Substring(indiceSeparador + 1);
            
            double quantidade = 1;

            if (indiceSeparador > -1)
            {
                quantidade = txtCodigoBarras.Text.Substring(0, indiceSeparador).ToDouble();
            }

            if (codigoBarrasOuIdProduto.Length == 13 && codigoBarrasOuIdProduto[0].ToInt() == _parametros.ParametrosCadastros.PrefixoEan13CodigoBarras)
            {
                pesquiseProdutoPeloId = _parametros.ParametrosCadastros.VinculoProdutoCodigoBarrasBalanca == EnumVinculoProdutoCodigoBarrasBalanca.CODIGOPRODUTO;

                string codigoBarrasOriginal = codigoBarrasOuIdProduto;

                codigoBarrasOuIdProduto = codigoBarrasOuIdProduto.CodigoProdutoCodigoBarrasBalanca(_parametros.ParametrosCadastros.PrefixoEan13CodigoBarras,
                                                                                                                                                        _parametros.ParametrosCadastros.TamahoCodigoBarras).ToInt().ToString();

                double pesoOuValor = codigoBarrasOriginal.ValorTotalOuPesoCodigoBarrasBalanca(_parametros.ParametrosCadastros.PrefixoEan13CodigoBarras,
                                                                                                                                                  _parametros.ParametrosCadastros.TamahoCodigoBarras,
                                                                                                                                                  _parametros.ParametrosCadastros.TipoCodigoBarrasBalanca == EnumTipoCodigoBarrasBalanca.VALORTOTAL);

                if (_parametros.ParametrosCadastros.TipoCodigoBarrasBalanca == EnumTipoCodigoBarrasBalanca.VALORTOTAL)
                {
                    ItemPedidoDeVenda item = null;

                    if (pesquiseProdutoPeloId)
                    {
                        item = _listaItensPedidoVenda.FirstOrDefault(x => x.Produto.Id == codigoBarrasOuIdProduto.ToInt());
                    }
                    else
                    {
                        item = _listaItensPedidoVenda.FirstOrDefault(x => x.Produto.DadosGerais.CodigoDeBarras == codigoBarrasOuIdProduto);
                    }

                    if (item != null)
                    {
                        double valorUnitario = item.ValorUnitario;
                        quantidade = Math.Round(pesoOuValor / (double)valorUnitario, 4);
                    }
                }
                else
                {
                    quantidade = pesoOuValor;
                }
            }

            List<ItemPedidoDeVenda> itens = null;

            if (pesquiseProdutoPeloId)
            {
                itens = _listaItensPedidoVenda.FindAll(item => item.Produto.Id == codigoBarrasOuIdProduto.ToInt());
            }
            else
            {
                itens = _listaItensPedidoVenda.FindAll(item => item.Produto.DadosGerais.CodigoDeBarras == codigoBarrasOuIdProduto);
            }

            if (itens == null || itens.Count == 0)
            {
                MessageBoxAkil.Show("Produto não encontrado.", "Aviso");

                return;
            }

            string mensagem = "Deseja excluir " + quantidade + " " + itens.First().Produto.DadosGerais.Descricao;

            var resultado = MessageBoxAkil.Show(mensagem, "Excluir Produto", MessageBoxButtons.OKCancel);

            if (resultado == DialogResult.OK)
            {
                foreach (var item in itens)
                {
                    if (item.Quantidade == quantidade)
                    {
                        _listaItensPedidoVenda.Remove(item);
                        quantidade = 0;
                    }
                    else if (item.Quantidade <= quantidade)
                    {
                        _listaItensPedidoVenda.Remove(item);
                        quantidade -= item.Quantidade;
                    }
                    else
                    {
                        double quantidadeAux = item.Quantidade;

                        item.Quantidade -= quantidade;

                        quantidade -= quantidadeAux;
                    }

                    if (quantidade == 0)
                    {
                        break;
                    }
                }

                txtCodigoBarras.Text = string.Empty;

                _resultado = DialogResult.OK;

                this.Close();
            }
        }

        private void Sair()
        {
            _resultado = DialogResult.Cancel;

            this.Close();
        }

        private void AltereModoPesquisa()
        {
            if (_pesquiseProdutoPeloId)
            {
                _pesquiseProdutoPeloId = false;
                lblDescricaoCodigoProduto.Text = "Código de Barras do Produto (F2 Para Alterar Modo de Pesquisa)";
            }
            else
            {
                _pesquiseProdutoPeloId = true;
                lblDescricaoCodigoProduto.Text = "Código do Produto (F2 Para Alterar Modo de Pesquisa)";
            }
        }

        private void PreenchaParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();
            _parametros = servicoParametros.ConsulteParametros();
        }

        #endregion
    }
}
