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
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;

namespace Programax.Easy.View.Telas.Cadastros.Inventarios
{
    public partial class FormReservasPedidos : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        
        private List<ItemProdutoGrid> _listaTmkGrid;
        private bool _itensBloqueados;
        private int Id;
        private double quantidadesub = 0;
        private double quantidadeanterior = 0;
        private string ConectionString;
        private List<ItemPedidoDeVenda> _listaItens;
        #endregion

        #region " CONSTRUTOR "

        public FormReservasPedidos()
        {
            InitializeComponent();

            
           
            this.NomeDaTela = "Pedidos/Reservas";

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
            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Enabled)
            {
                consultaProduto();
            }

        }

        

        #endregion

        #region " EVENTOS LANÇAMENTO CONTAGEM "

        private void txtIdProduto_Leave(object sender, EventArgs e)
        {
                //PreenchaCamposDoProduto(txtIdProduto.Text.ToInt());
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
            txtDescricaoProduto.Text = "";
            consultaProduto();
            txtId.Focus();
        }
        private void consultaProduto()
        {

            ServicoProduto servicoProduto = new ServicoProduto();
            var produto = servicoProduto.Consulte(txtId.Text.ToInt());

            if (produto != null)
            {
                txtDescricaoProduto.Text = produto.DadosGerais.Descricao;
            }
            else
            {
                MessageBox.Show("Produto com código " + txtId.Text + " não encontrado!", "Produto não encontrado");

                txtId.Text = string.Empty;
                txtId.Focus();
                return;
            }
            preencheGrid();

                //gcItens.DataSource = listaItemGrid;
                //gcItens.RefreshDataSource();


        }
       
        private void preencheGrid()
        {

            this.Cursor = Cursors.WaitCursor;


            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexaoII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }

            }



            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();
              
                var sql = "";

                sql = "SELECT pedido_id, pedido_data_elaboracao, pedido_vendedor_id, pes_razao, pedido_status FROM pedidosvendas " +
                                " Inner join pedidosvendasitens ON " +
                                " pedidosvendas.pedido_id = pedidosvendasitens.peditem_pedido_id " +
                                " Inner Join pessoas ON " +
                                " pedidosvendas.pedido_vendedor_id = pessoas.pes_id " +
                                " Inner join produtos ON " +
                                " pedidosvendasitens.peditem_produto_id = produtos.prod_id " +
                                " Where peditem_produto_id = " + txtId.Text + " And pedido_status<> 3 And " +
                                " PEDITEM_RESERVA > 0  And pedido_tipo_pedido_venda = 1 order by pedido_id desc LIMIT 50";

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                _listaItens = new List<ItemPedidoDeVenda>();
                List<ItemProdutoGrid> listaItemGrid = new List<ItemProdutoGrid>();
                

                List<ItemPedidoDeVenda> listaItensParaCarregar = new List<ItemPedidoDeVenda>();

                while (returnValue.Read())
                {
                    ItemProdutoGrid itemGrid = new ItemProdutoGrid();

                    itemGrid.IdPedido = returnValue["pedido_id"].ToInt();
                    var dt = DateTime.Parse(returnValue["pedido_data_elaboracao"].ToString()).ToString("dd-MM-yyyy");
                    itemGrid.Data = dt;
                    itemGrid.Vendedor = returnValue["pedido_vendedor_id"].ToString() + " - " + returnValue["pes_razao"].ToString();
                    itemGrid.Status = itemGrid.status != 0 ? itemGrid.Status.ToString() : null;
                    EnumStatusPedidoDeVenda? statusTmk = (EnumStatusPedidoDeVenda?)itemGrid.status;
                    itemGrid.Status = statusTmk.ToString();
                    ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                    
                    ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();

                    var movimentacao = servicoMovimentacao.ConsulteListaItensSaidaPorPedido(itemGrid.IdPedido);

                    var listaSaida = movimentacao.FindAll(x => x.TipoMovimentacao == EnumTipoMovimentacao.SAIDA);

                    var listaEntrada = movimentacao.FindAll(x => x.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA);
                    

                    
                    //Se não tiver nenhuma saída, carrega para a lista para baixar
                    if (listaSaida == null || listaSaida.Count == 0)
                    {
                        listaItemGrid.Add(itemGrid);
                    }
                    else
                    {
                        var pedidoDeVenda = servicoPedidoDeVenda.Consulte(itemGrid.IdPedido);
                        if (pedidoDeVenda != null)
                        {
                            listaItensParaCarregar = pedidoDeVenda.ListaItens.ToList();
                        }
                        if (listaSaida.Count > 0)
                        {
                            foreach (var itemBaixado in pedidoDeVenda.ListaItens)
                            {
                                if (listaSaida.Exists(x => x.Produto.Id == itemBaixado.Produto.Id))
                                {
                                    double quantEntrada = 0;

                                    if (listaEntrada.Exists(y => y.Produto.Id == itemBaixado.Produto.Id))
                                    {
                                        quantEntrada = listaEntrada.FindAll(x => x.Produto.Id == itemBaixado.Produto.Id).Sum(x => x.Quantidade);
                                    }

                                    var quantPedido = listaItensParaCarregar.FindAll(x => x.Produto.Id == itemBaixado.Produto.Id).Sum(x => x.Quantidade);
                                    var quantSaida = listaSaida.FindAll(x => x.Produto.Id == itemBaixado.Produto.Id).Sum(x => x.Quantidade);

                                    var diferencaQtde = quantPedido - quantSaida + quantEntrada;


                                    if (diferencaQtde > 0)
                                    {
                                        listaItemGrid.Add(itemGrid);
                                    }
                                }
                                else
                                {
                                    listaItemGrid.Add(itemGrid);
                                }
                            }
                        }
                    }
                }
                   
                _listaTmkGrid = listaItemGrid;
                gcItens.DataSource = listaItemGrid;
                gcItens.RefreshDataSource();

               

            }
            ConsultaReserva(txtId.Text.ToInt());
            this.Cursor = Cursors.Default;


            
        }

        private void carregaconexao()
        {
            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexaoII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }

            }

        }
        private void ConsultaReserva(int CodProduto)
        {

            carregaconexao();


            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                var sql = "";

                sql = "SELECT sum(PEDITEM_QUANTIDADE) as Reserva FROM pedidosvendasitens where peditem_produto_id = " + CodProduto + " And PEDITEM_RESERVA > 0 ";



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();

                while (returnValue.Read())
                {
                    txtreserva.Text = returnValue["Reserva"].ToString();
                }
            }
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

    

        #endregion

        #region " CLASSES AUXILIARES "

        private class AgenciaAuxiliarComboBox
        {
            public int Id { get; set; }

            public string Descricao { get; set; }
        }

        private class ItemProdutoGrid
        {
            public int IdPedido { get; set; }

            public string Data { get; set; }

            public string Vendedor { get; set; }

            public string Status { get; set; }
            public  virtual int status { get; set; }




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

            if (txtDescricaoProduto.Text != string.Empty)
                chavePesquisa = txtDescricaoProduto.Text.Substring(0, 5).Trim();

            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProduto(chavePesquisa);

            if (produto != null)
            {
                txtId.Text = produto.Id.ToString();
                txtDescricaoProduto.Text = produto.DadosGerais.Descricao;
            }
        }

       

        
        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            var tmk = _listaTmkGrid.Find(x => x.IdPedido == Convert.ToInt32(gridViewRoteiro.Columns.View.GetFocusedRowCellValue(gridColumn5)));


            
            if (tmk.IdPedido.ToInt() != 0)
            {
                FormCadastroPedidoDeVenda formCadastroPedidoDeVenda = new FormCadastroPedidoDeVenda(tmk.IdPedido.ToInt());

                formCadastroPedidoDeVenda.Show();
            }
        }
        private void gcItens_Click(object sender, EventArgs e)
        {
           
           
        }

        private void btnPesquisaInventario_Click(object sender, EventArgs e)
        {
            PesquiseProduto();
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnImprimirRelatorio_Click(object sender, EventArgs e)
        {
            gcItens.ExibaRelatorio();
        }

        private void gcItens_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
