using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CorrecaoEstoqueServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Programax.Easy.View.Telas.Estoque.SaidaEstoque
{
    public partial class FormEstornarEstoqueSaida : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "
              
        private List<ItemPedidoDeVenda> _listaItensPedidosVenda;
        private List<ItemPedidoDeVenda> _listaItensPedidosVendaMov;
        private Int64 ItemMov;
        private Parametros _parametros;
        private Pessoa _clienteSelecionado;
       
        private ServicoCidade _servicoCidade;
                
        private Empresa _empresa;
        private List<ItemEntrada> _listaItensEntrada;
        private string ConectionString;

        #endregion

        #region " CONSTRUTOR "

        public FormEstornarEstoqueSaida()
        {
            InitializeComponent();

            CarregueParametros();

            _listaItensPedidosVenda = new List<ItemPedidoDeVenda>();
            
            _servicoCidade = new ServicoCidade();

            PreenchaCboTipoDocumento();
            
            txtUsuario.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;            
           
            PesquiseEmpresa();
                        
            this.ActiveControl = txtId;            
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }
        
        private void cboTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            var tipoDocumento = (EnumTipoPedidoDeVenda?)cboTipoDocumento.EditValue;

            if (tipoDocumento == null || tipoDocumento.Value == EnumTipoPedidoDeVenda.ORCAMENTO)
            {
                //btnFecharVenda.Visible = false; //Estornar
            }
            else
            {
                if (txtSituacao.Text == EnumStatusPedidoDeVenda.ABERTO.Descricao() ||
                    txtSituacao.Text == EnumStatusPedidoDeVenda.ORCAMENTO.Descricao())
                {
                    //btnFecharVenda.Visible = true; //Estornar
                }
            }
        }
                                                        
        private void txtId_Leave(object sender, EventArgs e)
        {
            BusqueECarreguePedido();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpePedidoDeVenda();
        }
                        
        private void btnFecharVenda_Click(object sender, EventArgs e)
        {           
            Action actionFechamentoDePedido = () =>
            {
                if(VerifiqueSePedidoEstahBaixado()) return;
                
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                var pedido = servicoPedidoDeVenda.Consulte(txtId.Text.ToInt());
                  
                Action actionFechamentoPedido = () =>
                {
                    servicoPedidoDeVenda.EstorneProdutosParaEstoque(pedido);
                };

                TratamentosDeTela.TrateInclusaoEAtualizacao(actionFechamentoPedido, mensagemDeSucesso: "Pedido estornado com sucesso.");

                LimpePedidoDeVenda();               
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionFechamentoDePedido, exibirMensagemDeSucesso: false);
        }
                
        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda();

            var pedidoDeVenda = formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVenda();

            if (pedidoDeVenda != null)
            {
                PreenchaPedidoDeVenda(pedidoDeVenda);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "
        

        #region " APLICAÇÃO DE PARAMETROS "

        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

            _parametros = servicoParametros.ConsulteParametros();           
        }


        private void PesquiseEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            _empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            _empresa.DadosEmpresa.Endereco.Cidade.Estado.CarregueLazyLoad();
        }

        #endregion

        #region " PREENCHIMENTO DE COMBOBOX "

        private void PreenchaCboTipoDocumento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPedidoDeVenda>();

            lista.Insert(0, null);

            cboTipoDocumento.Properties.DisplayMember = "Descricao";
            cboTipoDocumento.Properties.ValueMember = "Valor";
            cboTipoDocumento.Properties.DataSource = lista;
        }
              
        
        #endregion

        #region " PREENCHIMENTO DE PESSOA INDICADOR, ATENDENTE, VENDEDOR, SUPERVISOR, TRANSPORTADORA E CLIENTE "

        private void PreenchaCliente(Pessoa cliente,
                                                 bool exibirMensagemDeNaoEncontrado = false,
                                                 bool preencherEndereco = true,
                                                 bool preencherEmpresa = true,
                                                 bool preencherIndicador = true,
                                                 bool preencherAtendente = true,
                                                 bool preencherVendedor = true,
                                                 bool preencherSupervisor = true,
                                                 bool preencherTabelaPreco = true,
                                                 bool preencherFormaPagamento = true,
                                                 bool preencherCondicaoPagamento = true,
                                                 bool preencherAReceberAberto = true,
                                                 bool preencherSaldoDisponivel = true,
                                                 bool preencherMaiorVenda = true,
                                                 bool preencherTipoCliente = true)
        {
            _clienteSelecionado = cliente;

            PreenchaDadosClienteCabecalho(cliente);

            if (preencherEndereco)
                PreenchaDadosClienteEndereco(cliente);

            if (preencherEmpresa)
                PreenchaDadosClienteEmpresa(cliente);
                        
            if (cliente != null)
            {
                txtIdCliente.Text = cliente.Id.ToString();
                txtNomeCliente.Text = cliente.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Cliente nao encontrado!", "Cliente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdCliente.Focus();
                }

                txtIdCliente.Text = string.Empty;
                txtNomeCliente.Text = string.Empty;                
            }
        }

        #endregion
              
        #region " PREENCHIMENTO CAMPOS"

        private void BusqueECarreguePedido()
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                var pedidoDeVenda = servicoPedidoDeVenda.Consulte(txtId.Text.ToInt());

                PreenchaPedidoDeVenda(pedidoDeVenda, exibirMensagemDeNaoEncontrado: true);
            }
        }

        private List<ItemPedidoDeVenda> RetorneitensPedidoVendaParaCarregar(int ItemPedido)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();

            //var movimentacao = servicoMovimentacao.ConsulteListaItensSaidaPorPedido(txtId.Text.ToInt());
            var movimentacao = servicoMovimentacao.ConsulteListaItensSaidaPorPedidoEItem(txtId.Text.ToInt(), ItemPedido);

            if (movimentacao == null || movimentacao.Count == 0)
            {
               // MessageBoxAkil.Show("Não existem itens baixados para serem estornados no estoque.", "Estorno de Itens", MessageBoxButtons.OK);
                return new List<ItemPedidoDeVenda>();
            }

            //if (!movimentacao.Exists(x => x.TipoMovimentacao == EnumTipoMovimentacao.SAIDA))
            //{
            //    MessageBoxAkil.Show("Não existem itens baixados para serem estornados no estoque.", "Estorno de Itens", MessageBoxButtons.OK);
            //    return new List<ItemPedidoDeVenda>();
            //}

            if (movimentacao.Exists(x => x.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA))                
            {   
                var listaSaida = movimentacao.FindAll(x => x.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA);
                
                return carregaItensDaMovimentacao(listaSaida);
            }

            return new List<ItemPedidoDeVenda>();
        }
        
        private List<ItemPedidoDeVenda> carregaItensDaMovimentacao(List<ItemMovimentacao> movimentacao)
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            List<ItemPedidoDeVenda> listaItens = new List<ItemPedidoDeVenda>();

            var pedidoDeVenda = servicoPedidoDeVenda.Consulte(txtId.Text.ToInt());

            foreach (var item in pedidoDeVenda.ListaItens)
            {
                if(movimentacao.Exists(x=>x.Produto.Id == item.Produto.Id))
                {
                    item.Quantidade = movimentacao.FindAll(x=>x.Produto.Id == item.Produto.Id).Sum(x=>x.Quantidade);
                    listaItens.Add(item);
                }
            }

            return listaItens;
        }

        private void PreenchaDadosClienteCabecalho(Pessoa cliente)
        {
            if (cliente != null)
            {
                txtCpfCnpj.Text = cliente.DadosGerais.CpfCnpj;
                txtStatusCliente.Text = cliente.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";
                txtRazaoSocial.Text = cliente.DadosGerais.Razao;
                txtTipoPessoa.Text = cliente.DadosGerais.TipoPessoa.Descricao();
                txtDataCadastroCliente.Text = cliente.DadosGerais.DataCadastro.ToString("dd/MM/yyyy");
            }
            else
            {
                txtCpfCnpj.Text = string.Empty;
                txtStatusCliente.Text = string.Empty;
                txtRazaoSocial.Text = string.Empty;
                txtTipoPessoa.Text = string.Empty;
                txtDataCadastroCliente.Text = string.Empty;
            }
        }

        private void PreenchaDadosClienteEndereco(Pessoa cliente, EnumTipoEndereco tipoEndereco = EnumTipoEndereco.PRINCIPAL )
        {
            if (cliente != null && cliente.ListaDeEnderecos != null && cliente.ListaDeEnderecos.Count > 0)
            {
                var endereco = cliente.ListaDeEnderecos.FirstOrDefault(end => end.TipoEndereco == tipoEndereco);

                if (endereco == null)
                {
                    endereco = cliente.ListaDeEnderecos.First();
                }
                                
            }            
        }

        private void PreenchaDadosClienteEmpresa(Pessoa cliente)
        {
            if (cliente != null && cliente.EmpresaPessoa != null)
            {
                ServicoClienteRapido servicoCliente = new ServicoClienteRapido();

                cliente = servicoCliente.Consulte(cliente.Id);
                                
                ServicoCrediario servicoAnaliseCredito = new ServicoCrediario(false, false);
                var analiseCredito = servicoAnaliseCredito.Consulte(cliente.Id);
                
            }            
        }
        
        #endregion
        
        #region " EDIÇÃO PEDIDO DE VENDA "

        public bool VerifiqueSePedidoEstahBaixado()
        {
            List<ItemPedidoDeVenda> listaItensParaCarregar = new List<ItemPedidoDeVenda>();

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            var pedidoDeVenda = servicoPedidoDeVenda.Consulte(txtId.Text.ToInt());

            if (pedidoDeVenda != null)
            {
                listaItensParaCarregar = pedidoDeVenda.ListaItens.ToList();
            }

            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();

            var movimentacao = servicoMovimentacao.ConsulteListaItensSaidaPorPedido(txtId.Text.ToInt());

            //Qual é o último movimento entrada ou saida
            var maiorIdMov = movimentacao.Count > 0 ? movimentacao.Max(x => x.Id):0;

            var maiorMovim = movimentacao.Count > 0 ? movimentacao.FirstOrDefault(x => x.Id == maiorIdMov):null;

            //Se for o de entrada vai emitir a msg - Terá que dar a saída primeiro
            if(maiorMovim == null || maiorMovim.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA)
            {
                MessageBoxAkil.Show("Para estornar é necessário que o pedido esteja Baixado.", "Estorno de Estoque", MessageBoxButtons.OK);
                return true;
            }

            List<ItemMovimentacao> listaMovimentacaoSaidaEntrada = new List<ItemMovimentacao>();
            
            var listaSaida = movimentacao.FindAll(x => x.TipoMovimentacao == EnumTipoMovimentacao.SAIDA);

            var listaDistinta = listaSaida.Select(x => x.Produto.Id).Distinct().ToList();

            foreach (var item in listaDistinta)
            {
                listaMovimentacaoSaidaEntrada.Add(listaSaida.Find(x => x.Produto.Id == item));
            }

            if (listaItensParaCarregar.Count != 0)
            {
                if (listaMovimentacaoSaidaEntrada.Count != 0)
                {
                    double quantidadeTotal = 0;
                    foreach (var item in listaItensParaCarregar)
                    {
                        if (listaMovimentacaoSaidaEntrada.Exists(x => x.Produto.Id == item.Produto.Id) &&
                            listaMovimentacaoSaidaEntrada.Where(x => x.Produto.Id == item.Produto.Id).Sum(x => x.Quantidade) == item.Quantidade)
                        {
                            quantidadeTotal += item.Quantidade;
                        }
                    }

                    if (listaItensParaCarregar.Sum(x => x.Quantidade) != quantidadeTotal)
                    {
                        MessageBoxAkil.Show("Para estornar é necessário que o pedido esteja Baixado", "Estorno de Estoque", MessageBoxButtons.OK);
                        return true;
                    }  
                }
            }
            else
            {
                MessageBoxAkil.Show("Pedido não encontrado", "Busca de Pedido", MessageBoxButtons.OK);
                return true;
            }

            return false;
        } 

        public void PreenchaPedidoDeVenda(PedidoDeVenda pedidoDeVenda, bool exibirMensagemDeNaoEncontrado = false)
        {     
            //btnFecharVenda.Visible = true; //Estornar

            cboTipoDocumento.Properties.ReadOnly = false;

            if (pedidoDeVenda != null)
            {
                pedidoDeVenda.Cliente.CarregueLazyLoad();
                pedidoDeVenda.Usuario.CarregueLazyLoad();
                pedidoDeVenda.Vendedor.CarregueLazyLoad();
                pedidoDeVenda.Indicador.CarregueLazyLoad();
                pedidoDeVenda.Atendente.CarregueLazyLoad();
                pedidoDeVenda.Supervisor.CarregueLazyLoad();

                pedidoDeVenda.Transportadora.CarregueLazyLoad();

                pedidoDeVenda.ListaItens.CarregueLazyLoad();
                pedidoDeVenda.ListaParcelasPedidoDeVenda.CarregueLazyLoad();
                

                foreach (var item in pedidoDeVenda.ListaItens)
                {

                    item.Produto.CarregueLazyLoad();
                }
                
                if (_parametros.ParametrosVenda.LimiteDiarioManha > 0)
                {
                    if(pedidoDeVenda.StatusRoteiro == EnumStatusRoteiro.EMAGENDA || pedidoDeVenda.StatusRoteiro == null)
                        btnAgendar.Visible = true;                   
                }
                
                if(!string.IsNullOrEmpty(_parametros.ParametrosVenda.NomeContrato))
                {
                    btnContrato.Visible = true;                   
                }

                CarregaNumeroNotaFiscal(pedidoDeVenda);

                txtId.Properties.ReadOnly = true;
            }
            else
            {
                txtId.Properties.ReadOnly = false;
                btnCancelarVenda.Visible = false;
                //btnFecharVenda.Visible = false; //Estornar

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Pedido de Venda nao encontrado!", "Pedido de Venda não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtId.Focus();
                }
                
                lblFinanceiro.Text = "<<Nenhum>>";
                lblFinanceiro.ForeColor = Color.Black;

                txtNotaFiscal.Text = string.Empty;               
            }
  
                PreenchaTopoEdicao(pedidoDeVenda);

                PreenchaItensEdicao(pedidoDeVenda);

                CarregaStatusRoteiro(pedidoDeVenda);

                CarregaStatusFinanceiro(pedidoDeVenda);
            
      
            
        }

        private void CarregaNumeroNotaFiscal(PedidoDeVenda pedido)
        {
            if(pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMITIDONFE)
            {
                var pedidoEncontrado = new ServicoNotaFiscal().ConsulteListaDocumentos(pedido.Id, null, null, EnumTipoDocumento.PEDIDODEVENDAS,
                                                                                       EnumStatusNotaFiscal.AUTORIZADA, null,
                                                                                       EnumTipoDeEmissaoPesquisa.NORMAL, null)
                                                                                       .FirstOrDefault();

                txtNotaFiscal.Text = pedidoEncontrado != null? pedidoEncontrado.IdentificacaoNotaFiscal.NumeroNota.ToString() : string.Empty;


            }
        }
        
        private void CarregaStatusRoteiro(PedidoDeVenda pedido)
        {
            if (pedido != null)
            {
                if (pedido.StatusRoteiro == EnumStatusRoteiro.EMROTA)
                {
                    lblStatusRoteiro.Text = EnumStatusRoteiro.EMROTA.Descricao();
                    lblStatusRoteiro.ForeColor = Color.Green;
                }
                else if (pedido.StatusRoteiro == EnumStatusRoteiro.CONCLUIDO)
                {
                    lblStatusRoteiro.Text = EnumStatusRoteiro.CONCLUIDO.Descricao();
                    lblStatusRoteiro.ForeColor = Color.Blue;
                }
                else if (pedido.StatusRoteiro == EnumStatusRoteiro.INCONCLUSO)
                {
                    lblStatusRoteiro.Text = EnumStatusRoteiro.INCONCLUSO.Descricao();
                    lblStatusRoteiro.ForeColor = Color.Red;
                }
                else if (pedido.StatusRoteiro == EnumStatusRoteiro.EMAGENDA)
                {
                    lblStatusRoteiro.Text = EnumStatusRoteiro.EMAGENDA.Descricao();
                    lblStatusRoteiro.ForeColor = Color.Black;                    
                }
                else
                {
                    lblStatusRoteiro.Text = "<<Nenhum>>";
                    lblStatusRoteiro.ForeColor = Color.Brown;
                }
            }
        }

        private void CarregaStatusFinanceiro(PedidoDeVenda pedido)
        {
            if (_parametros.ParametrosFiscais.EmitirNotaSemReceber)
            {
                lblFinanceiro.Visible = true;
                lblLabelFinanceiro.Visible = true;

                if(pedido != null)
                {
                    if (pedido.EstahPago==true)
                    {
                        lblFinanceiro.Text = "PAGO";
                        lblFinanceiro.ForeColor = Color.Blue;
                    }
                    else if(pedido.EstahPago==false)
                    {
                        lblFinanceiro.Text = "NÃO PAGO";
                        lblFinanceiro.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblFinanceiro.Text = "<<Nenhum>>";
                        lblFinanceiro.ForeColor = Color.Black;
                    }
                    
                }
            }
        }

        private void PreenchaTopoEdicao(PedidoDeVenda pedidoDeVenda)
        {
            if (pedidoDeVenda != null)
            {
                if(_parametros.ParametrosVenda.LimiteDiarioManha == 0) 
                    txtSituacao.Text = pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO? "FATURADO": pedidoDeVenda.StatusPedidoVenda.Descricao();
                else
                    txtSituacao.Text = pedidoDeVenda.StatusPedidoVenda.Descricao();

                cboTipoDocumento.EditValue = pedidoDeVenda.TipoPedidoVenda;
                txtId.Text = pedidoDeVenda.Id.ToString();
                txtUsuario.Text = pedidoDeVenda.Usuario.Id + " - " + pedidoDeVenda.Usuario.DadosGerais.Razao;
                txtDataElaboracao.Text = pedidoDeVenda.DataElaboracao.ToString("dd/MM/yyyy");

                bool preencherAReceberAbertoSaldoDisponivelMaiorVenda = pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.ABERTO;

                PreenchaCliente(pedidoDeVenda.Cliente,
                                        preencherEndereco: false,
                                        preencherEmpresa: true,
                                        preencherIndicador: false,
                                        preencherAtendente: false,
                                        preencherVendedor: false,
                                        preencherSupervisor: false,
                                        preencherTabelaPreco: false,
                                        preencherFormaPagamento: false,
                                        preencherCondicaoPagamento: false,
                                        preencherAReceberAberto: preencherAReceberAbertoSaldoDisponivelMaiorVenda,
                                        preencherSaldoDisponivel: preencherAReceberAbertoSaldoDisponivelMaiorVenda,
                                        preencherMaiorVenda: preencherAReceberAbertoSaldoDisponivelMaiorVenda);
            }
            else
            {
                cboTipoDocumento.EditValue = null;
                txtId.Text = string.Empty;
                txtUsuario.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;
                txtDataElaboracao.Text = string.Empty;
                txtSituacao.Text = string.Empty;

                PreenchaCliente(null);
            }
        }

        private void GereIdFalsoParaOsItens()
        {
            for (int i = 0; i < _listaItensPedidosVenda.Count; i++)
            {
                _listaItensPedidosVenda[i].Id = i + 1;
            }
        }

        private void PreenchaGridItens()
        {
            GereIdFalsoParaOsItens();
            
            List<ItemGrid> listaItemGrid = new List<ItemGrid>();

            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();

            var movimentacao = servicoMovimentacao.ConsulteListaItensSaidaPorPedido(txtId.Text.ToInt());



          

        


            if (movimentacao == null || movimentacao.Count == 0)
            {
                MessageBoxAkil.Show("Não existem itens baixados para serem estornados no estoque.", "Estorno de Itens", MessageBoxButtons.OK);
                return;
            }

            foreach (var item in _listaItensPedidosVenda)
            {
                ItemGrid itemGrid = new ItemGrid();

                itemGrid.Id = item.Id;
                itemGrid.CodigoDeBarras = item.Produto.DadosGerais.CodigoDeBarras;
                itemGrid.IdProduto = item.Produto.Id;
                itemGrid.Descricao = item.Produto.DadosGerais.Descricao;

                itemGrid.MarcaFabricante = item.Produto.Principal != null && item.Produto.Principal != null && item.Produto.Principal.Marca != null ? item.Produto.Principal.Marca.Descricao : string.Empty;

                itemGrid.Quantidade = item.Quantidade.ToDouble();

                itemGrid.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Descricao : string.Empty;
                itemGrid.ValorTotal = item.ValorTotal.ToString("0.00");
                itemGrid.ValorUnitario = item.ValorUnitario.ToString("0.00");

                //var movimentacaoEntrada = servicoMovimentacao.ConsulteListaItensSaidaPorPedidoEItem(txtId.Text.ToInt(), itemGrid.IdProduto);
                //var movimentacaoSaida = servicoMovimentacao.ConsulteListaItensSaidaPorPedidoEItem(txtId.Text.ToInt(), itemGrid.IdProduto);
                //if (movimentacaoSaida.Count != 0)
                //{
                //    if (movimentacaoSaida.Count <= movimentacaoSaida.Count)
                //    {
                //        listaItemGrid.Add(itemGrid);
                //    }
                //}
                var listaSaida = movimentacao.FindAll(x => x.TipoMovimentacao == EnumTipoMovimentacao.SAIDA);

                var listaEntrada = movimentacao.FindAll(x => x.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA);


                if (listaSaida.Exists(x => x.Produto.Id == item.Produto.Id))
                {
                    double quantEntrada = 0;

                    if (listaEntrada.Exists(y => y.Produto.Id == item.Produto.Id))
                    {
                        quantEntrada = listaEntrada.FindAll(x => x.Produto.Id == item.Produto.Id).Sum(x => x.Quantidade);
                    }

                    var quantPedido = listaItemGrid.FindAll(x => x.IdProduto == item.Produto.Id).Sum(x => x.Quantidade);
                    var quantSaida = listaSaida.FindAll(x => x.Produto.Id == item.Produto.Id).Sum(x => x.Quantidade);

                    var diferencaQtde =  quantSaida - quantEntrada;

                    if (diferencaQtde > 0)
                    {
                        item.Quantidade = diferencaQtde;
                        itemGrid.Quantidade = item.Quantidade;
                        listaItemGrid.Add(itemGrid);
                    }
                }
                //else
                //{
                //    listaItemGrid.Add(itemGrid);
                //}
            
                }

            gcProdutos.DataSource = listaItemGrid;
            gcProdutos.RefreshDataSource();
                        
        }

        private void PreenchaItensEdicao(PedidoDeVenda pedidoDeVenda)
        {
            if (pedidoDeVenda != null)
            {
               
                _listaItensPedidosVenda = pedidoDeVenda.ListaItens.ToList();
                PreenchaGridItens();
            }
            else
            {
                _listaItensPedidosVenda.Clear();
            }

            
        }
        
        private void LimpePedidoDeVenda()
        {
            PreenchaPedidoDeVenda(null);

            txtId.Focus();
        }

        #endregion


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
            
            public string Grupo { get; set; }

            public string Categoria { get; set; }

            public double Quantidade { get; set; }

            public string ValorUnitario { get; set; }
           
            public string ValorTotal { get; set; }           
        }
                
        #endregion

                
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnEstornarItem_Click(object sender, EventArgs e)
        {
            if (_listaItensPedidosVenda == null ||_listaItensPedidosVenda.Count == 0) return;

            var item = _listaItensPedidosVenda.Find(x => x.Id == Convert.ToInt32(coluna.View.GetFocusedRowCellValue(coluna)));
            
            if (MessageBox.Show("Deseja estornar o item: " + item.Produto.Id,"Estorno de item", 
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
                var pedido = servicoPedidoDeVenda.Consulte(txtId.Text.ToInt());
                try
                {
                    servicoPedidoDeVenda.EstorneItemDeEstoque(pedido, item);

                    if (_parametros.ParametrosVenda.TrabalharComEstoqueReservado == true)
                    {
                       
                       double ItemReservado =  item.Produto.FormacaoPreco.EstoqueReservado + item.Quantidade;
                        if (ItemReservado <= 0)
                        {
                            ItemReservado = 0;
                        }
                       AlteraReserva(item.Produto.Id, ItemReservado);
                    }


                    MessageBox.Show("Item estornado com sucesso!", "Estorno de Item");

                    _listaItensPedidosVenda.Remove(item);
                    PreenchaGridItens();

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }    
                

            }
        }
        private void AlteraReserva(int produto, double quantidade)
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
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }


            }
            string quant = quantidade.ToString();
            quant = quant.ToString().Replace(",", ".");

            if (quant.ToInt() < 0)
            {
                quant = "0.00";
            }
            using (var conn = new MySqlConnection(ConectionString))

            {
                conn.Open();

                string Sql = "update produtos set PROD_ESTOQUE_RESERVADO = " + quant +

                            " where prod_id=" + produto;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();


            }

        }

        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}

