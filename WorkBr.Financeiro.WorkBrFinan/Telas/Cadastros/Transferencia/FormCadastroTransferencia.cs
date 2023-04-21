using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.InventarioServ;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Relatorios;
using Programax.Easy.Report.Relatorios.Estoque;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using Programax.Easy.Servico.Cadastros.SubEstoqueServ;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Easy.Servico.Cadastros.TransferenciaServ;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Cadastros.Inventarios
{
    public partial class FormCadastroTransferencia : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<ItemTransferencia> _listaItemInventario;
        private bool _itensBloqueados;
        private int Id;
        private double quantidadesub = 0;
        private double quantidadeanterior = 0;


        #endregion

        #region " CONSTRUTOR "

        public FormCadastroTransferencia()
        {
            InitializeComponent();

            _listaItemInventario = new List<ItemTransferencia>();


            BloquearLancamentoContagem();

            rdnSubEstoque.Checked = true;
            _itensBloqueados = true;

            this.NomeDaTela = "Transferência Sub Estoque/Estoque";

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        #region " EVENTOS BARRAS DE BOTÕES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

       
        private bool VerificaSeExisteInventarioAberto()
        {
            ServicoInventario servicoInventarioVericacao = new ServicoInventario();
            var listaInventario = servicoInventarioVericacao.ConsulteLista(DateTime.Now.AddYears(-1), EnumStatusInventario.ABERTO,null,null,null,null,null);

            if (listaInventario != null)
                return true;

            return false;
        }

       
       
       

      

        #endregion

        #region " EVENTOS CAPA "


        private void txtId_Leave(object sender, EventArgs e)
        {
            consultaSubestoque(txtId.Text.ToInt());
            
        }

        private void btnPesquisaInventario_Click(object sender, EventArgs e)
        {
            FormTransferenciaPesquisa formTransferenciaPesquisa = new FormTransferenciaPesquisa();

            var transferencia = formTransferenciaPesquisa.ExibaPesquisaDeInventarios();

            if (transferencia != null)
            {
                consultaSubestoque(transferencia.Id);
                btnSalvar.Visible = true;
            }
        }

        #endregion

        #region " EVENTOS LANÇAMENTO CONTAGEM "

        private void txtIdProduto_Leave(object sender, EventArgs e)
        {
                PreenchaCamposDoProduto(txtIdProduto.Text.ToInt());
        }

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            InsereItens();
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeCamposLancamentoProduto();
        }

        private void txtQuantidadeContada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsereItens();
            }
        }

        #endregion

        #region " EVENTOS COMUNS "

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        #endregion

        #endregion

        #region " MÉTODOS AUXILIARES "



        #region " LIMPAR, EDITAR E RETORNAR INVENTARIO EM EDIÇÃO "

        private void LimpeFormulario()
        {
            txtId.Text = "";
            txtDescSubEstoque.Text = "";
            txtIdProduto.Text = "";
            txtItemDescricaoProduto.Text = "";
            txtdisponivel.Text = "";
            txtUnidadeEstocar.Text = "";
            txtQuantidadeContada.Text = "";
            consultaSubestoque(0);
            rdnSubEstoque.Checked = true;
            txtId.Focus();
        }
        private void consultaSubestoque(int Codigo)
        {
            
                ServicoSubEstoque servicoSubEstoque = new ServicoSubEstoque();
                var subestoque = servicoSubEstoque.Consulte(Codigo);

                ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
                var ItemTransferencia = servicoItemTransferencia.ConsulteListas(Codigo);

                _listaItemInventario = ItemTransferencia;
            

                List<ItemTransferenciaGrid> listaItemGrid = new List<ItemTransferenciaGrid>();

                foreach (var item in ItemTransferencia)
                {
                    ItemTransferenciaGrid itemInventarioGrid = new ItemTransferenciaGrid();

                    itemInventarioGrid.Descricao = item.Descricao;
                    itemInventarioGrid.IdProduto = item.produto;
                    itemInventarioGrid.QuantidadeEstoque = item.QuantidadeEstoque.ToString("0.00");
                    itemInventarioGrid.Unidade = item.Unidade;

                    ServicoProduto servicoProduto = new ServicoProduto();
                    var produto = servicoProduto.Consulte(item.produto);
                    var Estoqueproduo = produto.FormacaoPreco.Estoque - item.QuantidadeEstoque;

                    itemInventarioGrid.QuantidadeProduto = Estoqueproduo.ToString("0.00");
                    listaItemGrid.Add(itemInventarioGrid);
                   

                }
                listaItemGrid.OrderBy(x => x.Descricao).ToList();

            
                gcItens.DataSource = listaItemGrid;
                gcItens.RefreshDataSource();

                EditeInventario(subestoque);
        }
        private void EditeInventario(SubEstoque subestoque)
        {
            if (subestoque != null)
            {
                txtId.Text = subestoque.Id.ToString();
                txtDescSubEstoque.Text = subestoque.Descricao.ToString();

                DesbloquearCapa();
                DesbloquearLancamentoContagem();

                txtIdProduto.Enabled = true;
                txtIdProduto.ReadOnly = false;

                txtId.Enabled = true;

                txtIdProduto.Focus();
            }
        }

        private Inventario RetorneInventarioEmEdicao()
        {
            Inventario inventario = new Inventario();

            inventario.Id = txtId.Text.ToInt();

            return inventario;
        }

        #endregion

        #region " BLOQUEAR E DESBLOQUEAR CAPA "

        private void DesbloquearCapa()
        {
            txtId.Enabled = true;

        }

        private void BloquearCapa()
        {
            txtId.Enabled = false;

        }

        #endregion

        #region " LANÇAMENTO CONTAGEM "

        private void InsereItens()
        {
            if (txtdisponivel.Text.ToInt() <= 0 )
            {
                MessageBox.Show("Produto com estoque zerado, não pode ser transferido.", "Informe outro produto", MessageBoxButtons.OK);
                LimpeCamposLancamentoProduto();
                return;
            }

            if (txtIdProduto.Text.ToString() == "")
            {
                MessageBox.Show("Selecione o Produto para Transferir.", "Informe o produto", MessageBoxButtons.OK);
                LimpeCamposLancamentoProduto();
                return;
            }

            if (rdnSubEstoque.Checked == true)
            {
                if(txtQuantidadeContada.Text.ToInt()> txtdisponivel.Text.ToInt())
                {
                    MessageBox.Show("Quantidade Inserida é maior que Quantidade Disponível, altere a quantidade Inserida.", "Informe outro produto", MessageBoxButtons.OK);
                    txtQuantidadeContada.Text = string.Empty;
                    txtQuantidadeContada.Focus();
                    return;
                }
            }

            if (rdnEstoque.Checked == true)
            {
                if (txtQuantidadeContada.Text.ToInt() > txtdisponivel.Text.ToInt())
                {
                    MessageBox.Show("Quantidade Inserida é maior que Quantidade Disponível, altere a quantidade Inserida.", "Informe outro produto", MessageBoxButtons.OK);
                    txtQuantidadeContada.Text = string.Empty;
                    txtQuantidadeContada.Focus();
                    return;
                }
            }

            double quantidade = 0;


            Action actionAatualizarItem = () =>
                {

                    string Mensagem = "";

                    ItemTransferencia itemTransferencia = new ItemTransferencia();


                    itemTransferencia.Id = Id;

                    if (rdnSubEstoque.Checked == true)
                    {
                        Mensagem = "Deseja Incluir este Item no Sub Estoque?";
                    }
                    else
                    {
                        Mensagem = "Deseja Retornar este Item para o Estoque?";
                    }

                    if (MessageBox.Show(Mensagem, "Inclusão de item", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        itemTransferencia.SubEstoque = txtId.Text.ToInt();
                        if (rdnSubEstoque.Checked == true)
                        {
                            quantidade = quantidadeanterior + txtQuantidadeContada.Text.ToDouble();
                        }
                        else
                        {
                            quantidade = quantidadesub - txtQuantidadeContada.Text.ToDouble();
                        }
                        if (rdnSubEstoque.Checked == true)
                        {
                            if (txtQuantidadeContada.Text.ToInt() > txtdisponivel.Text.ToInt() - quantidadesub.ToInt())
                            {
                                MessageBox.Show("Quantidade Inserida é maior que Quantidade Disponível, altere a quantidade Inserida.", "Informe outro produto", MessageBoxButtons.OK);
                                txtQuantidadeContada.Text = string.Empty;
                                LimpeCamposLancamentoProduto();
                                txtQuantidadeContada.Focus();
                                return;
                            }
                        }

                        else
                        {
                            if (txtQuantidadeContada.Text.ToInt() > quantidadesub.ToInt())
                            {
                                MessageBox.Show("Quantidade Inserida é maior que Quantidade Disponível, altere a quantidade Inserida.", "Informe outro produto", MessageBoxButtons.OK);
                                txtQuantidadeContada.Text = string.Empty;
                                LimpeCamposLancamentoProduto();
                                txtQuantidadeContada.Focus();
                                return;
                            }
                        }
                       

                        itemTransferencia.QuantidadeEstoque = quantidade;
                        itemTransferencia.DataCadastro = DateTime.Now;
                        itemTransferencia.produto = txtIdProduto.Text.ToInt();
                        itemTransferencia.Descricao = txtItemDescricaoProduto.Text.ToString();
                        itemTransferencia.Unidade = txtUnidadeEstocar.Text.ToString();

                        quantidadeanterior = 0;
                        Id = 0;

                        if (itemTransferencia != null)
                        {

                            ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
                            if (Id != 0)
                            {
                                servicoItemTransferencia.Atualize(itemTransferencia);
                            }
                            else
                            {
                                servicoItemTransferencia.Cadastre(itemTransferencia);
                            }


                            PreenchaGridLancamento();

                            LimpeCamposLancamentoProduto();
                        }
                    }
                };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionAatualizarItem, exibirMensagemDeSucesso: false);
        }

        private void DesbloquearLancamentoContagem()
        {
            txtIdProduto.Enabled = true;
            txtQuantidadeContada.Enabled = true;

            btnInserirAtualizarItem.Enabled = true;
            btnCancelarItem.Enabled = true;

            //_itensBloqueados = true;
        }

        private void BloquearLancamentoContagem()
        {
            txtIdProduto.Enabled = false;
            txtQuantidadeContada.Enabled = false;

            btnInserirAtualizarItem.Enabled = false;
            btnCancelarItem.Enabled = false;

        }

        private void PreenchaCamposDoProduto(int item)
        {

            if (item != 0 )
            {
                if(rdnSubEstoque.Checked == true)
                {
                    foreach (var itemproduto in _listaItemInventario)
                    {
                        if (itemproduto.produto == txtIdProduto.Text.ToInt() && itemproduto.QuantidadeEstoque != 0)
                        {
                            MessageBox.Show("Produto já conta na lista de transferências.", "Selecione o Item", MessageBoxButtons.OK);
                            LimpeCamposLancamentoProduto();
                            return;
                        }
                    }
                }
               

                ServicoProduto servicoproduto = new ServicoProduto();
                var produto = servicoproduto.Consulte(item);
               if (produto == null)
                {
                    return;
                }

                txtIdProduto.Text = produto.Id.ToString();
                txtEstoqueTotal.Text = produto.FormacaoPreco.Estoque.ToString("#0.00");
                
                txtItemDescricaoProduto.Text = produto.DadosGerais.Descricao;
                if (rdnSubEstoque.Checked == true)
                {
                    txtdisponivel.Text = produto.FormacaoPreco.Estoque.ToString("#0.00");
                }
                else
                {
                    var itemproduto = _listaItemInventario.FirstOrDefault(x => x.produto == gridColunaProdutoId.View.GetFocusedRowCellValue(gridColunaProdutoId).ToInt());

                    txtdisponivel.Text = itemproduto.QuantidadeEstoque.ToString();
                }
                
                txtUnidadeEstocar.Text = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                AltereMascaraQuantidadeProduto(produto);
               

                txtQuantidadeContada.Focus();
            }
            else
            {
                txtIdProduto.Text = string.Empty;
                txtEstoqueTotal.Text = string.Empty;
                txtItemDescricaoProduto.Text = string.Empty;
                txtUnidadeEstocar.Text = string.Empty;
                txtdisponivel.Text = string.Empty;
                txtQuantidadeContada.Text = string.Empty;

                //if (casoNuloFocusNoIdProduto)
                //{
                //    txtIdProduto.Focus();
                //}
            }
        }

        private void AltereMascaraQuantidadeProduto(Produto produto)
        {
            if (produto.DadosGerais.PermiteVendaFracionada)
            {
                txtQuantidadeContada.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtQuantidadeContada.Properties.Mask.EditMask = @"[0-9]{1,11}";
            }
        }

        private void LimpeCamposLancamentoProduto()
        {
            PreenchaCamposDoProduto(0);
            quantidadeanterior = 0;
            quantidadesub = 0;
            txtIdProduto.Focus();
        }

        private void PreenchaGridLancamento()
        {

            ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
            var ItemTransferencia = servicoItemTransferencia.ConsulteListas(txtId.Text.ToInt());

            

            List<ItemTransferenciaGrid> listaItemGrid = new List<ItemTransferenciaGrid>();

            foreach (var item in ItemTransferencia)
            {
                ItemTransferenciaGrid itemInventarioGrid = new ItemTransferenciaGrid();

                itemInventarioGrid.Descricao = item.Descricao;
                itemInventarioGrid.IdProduto = item.produto;
                itemInventarioGrid.QuantidadeEstoque = item.QuantidadeEstoque.ToString("#0.00");
                itemInventarioGrid.Unidade = item.Unidade;
                listaItemGrid.Add(itemInventarioGrid);
               
            }

           
            _listaItemInventario = ItemTransferencia;
            

            gcItens.DataSource = listaItemGrid;
            gcItens.RefreshDataSource();

        }

        private void PreenchaGridSub()
        {

            ServicoSubEstoque servicoSubestoque = new ServicoSubEstoque();
            var subestoqueitem = servicoSubestoque.Consulte(txtId.Text.ToInt());

            List<ItemTransferenciaGrid> listaItemGrid = new List<ItemTransferenciaGrid>();

            foreach (var item in _listaItemInventario)
            {
                ItemTransferenciaGrid itemInventarioGrid = new ItemTransferenciaGrid();

                //itemInventarioGrid.Descricao = item.Produto.DadosGerais.Descricao;
                //itemInventarioGrid.IdProduto = item.Produto.Id;

              
                itemInventarioGrid.QuantidadeEstoque = item.QuantidadeEstoque.ToString("#0.00"); ;

                //itemInventarioGrid.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                listaItemGrid.Add(itemInventarioGrid);
            }

            gcItens.DataSource = listaItemGrid;
            gcItens.RefreshDataSource();

            
        }


        #endregion

        #endregion

        #region " CLASSES AUXILIARES "

        private class AgenciaAuxiliarComboBox
        {
            public int Id { get; set; }

            public string Descricao { get; set; }
        }

        private class ItemTransferenciaGrid
        {
            public int IdProduto { get; set; }

            public string Descricao { get; set; }

            public string Unidade { get; set; }

            public string QuantidadeEstoque { get; set; }
            public string QuantidadeProduto { get; set; }

            public int SubEstoque { get; set; }

           
        }

        private class Acompanhamento
        {
            public string AcompanhamentoOnline { get; set; }

            public string Evolucao { get; set; }

            public string Divergencia { get; set; }

            public string DataInicio { get; set; }

           
        }

        #endregion

        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtIdProduto_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            PesquiseProduto();
        }
        private void PesquiseProduto()
        {
            string chavePesquisa = "";

            if (txtItemDescricaoProduto.Text != string.Empty)
                chavePesquisa = txtItemDescricaoProduto.Text.Substring(0, 5).Trim();

            FormPesquisaProdutoT formPesquisaProduto = new FormPesquisaProdutoT();
            var produto = formPesquisaProduto.ExibaPesquisaDeProduto(chavePesquisa);

            if (produto != null)
            {
                PreenchaCamposDoProduto(produto.Id);
            }
        }

        private void rdnSubEstoque_click(object sender, EventArgs e)
        {
            DesbloquearLancamentoContagem();
            PreenchaCamposDoProduto(0);
        }

        private void rdnEstoque_Click(object sender, EventArgs e)
        {
            BloquearLancamentoContagem();
            txtQuantidadeContada.Enabled = true;
            txtQuantidadeContada.ReadOnly = false;
            btnInserirAtualizarItem.Enabled = true;
            btnCancelarItem.Enabled = true;
            PreenchaCamposDoProduto(0);
        }
        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            
            quantidadesub = 0;
            Id = 0;
            var item = _listaItemInventario.FirstOrDefault(x => x.produto == gridColunaProdutoId.View.GetFocusedRowCellValue(gridColunaProdutoId).ToInt());
            
            Id = item.Id;
            quantidadesub = item.QuantidadeEstoque;
            quantidadeanterior = item.QuantidadeEstoque;
            PreenchaCamposDoProduto(item.produto);
        }
        private void gcItens_Click(object sender, EventArgs e)
        {
            //var item = _listaItemInventario.FirstOrDefault(x => x.produto == gridColunaProdutoId.View.GetFocusedRowCellValue(gridColunaProdutoId).ToInt());

            //PreenchaCamposDoProduto(item.produto);
           
            
            txtIdProduto.Text = "";
            txtEstoqueTotal.Text = "";
            txtItemDescricaoProduto.Text = "";
            txtdisponivel.Text = "";
            txtUnidadeEstocar.Text = "";
            txtQuantidadeContada.Text = "";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "" && txtId.Text.ToInt() != 0)
            {
                MessageBox.Show("Sub Estoque Incluido com Sucesso.", "Cadastro salvo", MessageBoxButtons.OK);
            }
            
        }
    }
}
