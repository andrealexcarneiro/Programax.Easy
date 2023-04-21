using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using System.Linq;

namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    public partial class FormCadastroRoteiros : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoaCadastro;
        private List<Roteiro> _listaDeRoteiros;
               
        #endregion

        #region " CONSTRUTOR "

        public FormCadastroRoteiros()
        {
            InitializeComponent();

            PreenchaPeriodos();

            _listaDeRoteiros = new List<Roteiro>();

            InformeUsuarioContasAPagarReceber(Sessao.PessoaLogada);

            txtDataEmissao.DateTime = DateTime.Now.Date;
            txtDataVencimento.DateTime = DateTime.Now.Date;
            txtDataAgenda.DateTime = DateTime.Now.Date;

            this.ActiveControl = txtIdPessoa;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (_listaDeRoteiros == null || _listaDeRoteiros.Count == 0)
            {
                MessageBox.Show("Não foi informado nenhum roteiro.\n\nPor favor crie pelo menos um roteiro.", "Nenhum Roteiro Criado!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            Action actionSalvar = () =>
            {
                ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

                foreach (var item in _listaDeRoteiros)
                {
                    servicoRoteiro.Atualize(item);
                }               

                ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

                foreach (var item in _listaDeRoteiros)
                {
                    var pedidoAtualizar = servicoPedido.Consulte(item.PedidoVenda.Id);

                    pedidoAtualizar.StatusRoteiro = EnumStatusRoteiro.EMROTA;

                    servicoPedido.Atualize(pedidoAtualizar);
                }

                LimpeFormulario();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void txtIdPessoa_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdPessoa.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var vendedor = servicoPessoa.ConsultePessoaAtiva(txtIdPessoa.Text.ToInt());

                PreenchaPessoa(vendedor, true);
            }
            else
            {
                PreenchaPessoa(null);
            }
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoaFuncionarioAtiva();

            if (pessoa != null)
            {
                PreenchaPessoa(pessoa);
            }
        }

        private void btnPesquisaPedidoVendas_Click(object sender, EventArgs e)
        {
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda();

            var pedidoDeVenda = formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVenda();

            if (pedidoDeVenda != null)
            {   
                txtNumeroPedidoVendas.Text = pedidoDeVenda.Id.ToString();
            }
        }

        private void btnAdicionarRoteiro_Click(object sender, EventArgs e)
        {
            AdicionarRoteiro(txtNumeroPedidoVendas.Text.ToInt());
        }

        private void btnCriarRoteiros_Click(object sender, EventArgs e)
        {
            //AdicionarRoteiro();
        }

        private void btnExcluirRoteiro_Click(object sender, EventArgs e)
        {
            var listaParaExcluir = carregaListaParaConcluirRoteiro();

            if (listaParaExcluir == null) return;

            if (listaParaExcluir.Count() == 0) return;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                foreach (var item in listaParaExcluir)
                {
                    var roteiroSelecionado = RetorneRoteiroSelecionado();

                    if (roteiroSelecionado != null)
                    {
                        _listaDeRoteiros.Remove(roteiroSelecionado);
                        PreenchaGridRoteiros();
                    }   
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }

            this.Cursor = Cursors.Default;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private List<Roteiro> carregaListaParaConcluirRoteiro()
        {
            if (gridViewRoteiro.SelectedRowsCount == 0) return null;

            Roteiro itemParaExclusao = new Roteiro();
            List<Roteiro> listaItemParaExclusao = new List<Roteiro>();

            foreach (var item in gridViewRoteiro.GetSelectedRows())
            {
                var lancamento = colunaPedido.View.GetRowCellValue(item, colunaPedido);

                itemParaExclusao = _listaDeRoteiros.FirstOrDefault(x =>x.PedidoVenda.Id == lancamento.ToInt());

                if (itemParaExclusao != null)
                    listaItemParaExclusao.Add(itemParaExclusao);
            }

            return listaItemParaExclusao;
        }

        protected virtual Roteiro RetorneRoteiroSelecionado()
        {
            Roteiro roteiro = null;

            if (_listaDeRoteiros != null && _listaDeRoteiros.Count > 0)
            {                
                roteiro = _listaDeRoteiros.Find(x=>x.PedidoVenda.Id == Convert.ToInt32(colunaPedido.View.GetFocusedRowCellValue(colunaPedido)));
            }

            return roteiro;
        }

        private void PreenchaPeriodos()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumPeriodo>();
           
            cboPeriodo.Properties.DataSource = lista;
            cboPeriodo.Properties.ValueMember = "Valor";
            cboPeriodo.Properties.DisplayMember = "Descricao";

            cboPeriodo.EditValue = EnumPeriodo.MANHA;
        }

        private void AdicionarRoteiro(int pedidoId)
        {
            List<Roteiro> _listaDeRoteiros = new List<Roteiro>();

            //Consultar se o pedido está agendado
            var roteiro = new ServicoRoteiro().ConsultePorPedido(pedidoId);

            if (roteiro == null)
            {   
                MessageBox.Show("Agendamento não encontrado para o pedido: " + pedidoId + 
                                " Pode não ter sido feito o agendamento. Por favor, verifique, clique no Pedido.",
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if(roteiro.Status == EnumStatusRoteiro.CONCLUIDO)
                {
                    MessageBox.Show("Roteiro Concluído para o pedido: " + pedidoId,
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if(roteiro.Status == EnumStatusRoteiro.EMROTA || roteiro.Status == EnumStatusRoteiro.INCONCLUSO)
                {
                    MessageBox.Show("Roteiro Criado para o pedido: "+ pedidoId,
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            
            if (txtIdPessoa.Text == string.Empty)
            {
                MessageBox.Show("Informe o Funcionário para continuar.",
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtDataAgenda.Text == string.Empty)
            {
                MessageBox.Show("Informe a Data da Agenda para continuar. ",
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (VerifiqueSePedidoEstahNaLista(pedidoId))
            {
                MessageBox.Show("Este pedido consta na lista. Por favor, informe outro para continuar. ",
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            adicionaItemRoteiro(new PedidoDeVenda { Id = pedidoId }, roteiro.Id);
            
            PreenchaGridRoteiros();
            txtNumeroPedidoVendas.Text = string.Empty;
        }
        
        private bool VerifiqueSePedidoEstahNaLista(int IdPedido)
        {
            return _listaDeRoteiros.Exists(x=>x.PedidoVenda.Id == IdPedido);
        }

        private void adicionaItemRoteiro(PedidoDeVenda pedido, int roteiroId)
        {
            Roteiro itemRoteiro = new Roteiro();

            var usu = txtUsuario.Text.Split('-');

            itemRoteiro.Id = roteiroId;

            itemRoteiro.PessoaFuncionario = txtIdPessoa.Text != string.Empty ? new Pessoa { Id = txtIdPessoa.Text.ToInt() } :
                                            new Pessoa { Id = usu[0].Trim().ToInt() };

            itemRoteiro.PessoaFuncionario.DadosGerais.Razao = txtIdPessoa.Text != string.Empty ? txtNomePessoa.Text : usu[1];

            itemRoteiro.PedidoVenda = new ServicoPedidoDeVenda().Consulte(pedido.Id);

            itemRoteiro.Periodo = (EnumPeriodo)cboPeriodo.EditValue;

            itemRoteiro.DataElaboracao = txtDataAgenda.DateTime;

            itemRoteiro.Status = EnumStatusRoteiro.EMROTA;

            itemRoteiro.Usuario = new Pessoa { Id = usu[0].Trim().ToInt() };

            _listaDeRoteiros.Add(itemRoteiro);
        }

        protected virtual ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return null;
        }

        private void LimpeFormulario()
        {   
            _listaDeRoteiros.Clear();
            PreenchaGridRoteiros();

            PreenchaPessoa(null);
            
            txtDataEmissao.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDataVencimento.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            txtIdPessoa.Focus();
        }

        protected virtual void PreenchaPessoa(Pessoa pessoa, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (pessoa != null)
            {
                txtIdPessoa.Text = pessoa.Id.ToString();
                txtNomePessoa.Text = pessoa.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Parceiro nao encontrado!", "Parceiro não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdPessoa.Focus();
                }

                txtIdPessoa.Text = string.Empty;
                txtNomePessoa.Text = string.Empty;
            }
        }

        protected virtual void InformeUsuarioContasAPagarReceber(Pessoa pessoaQueCadastrou)
        {
            _pessoaCadastro = pessoaQueCadastrou;
            txtUsuario.Text = pessoaQueCadastrou.Id + " - " + pessoaQueCadastrou.DadosGerais.Razao;
        }

        private void PreenchaGridRoteiros()
        {
            List<RoteiroGrid> listaRoteirosGrid = new List<RoteiroGrid>();

            foreach (var titulo in _listaDeRoteiros)
            {
                RoteiroGrid tituloGrid = new RoteiroGrid();

                tituloGrid.Funcionario = titulo.PessoaFuncionario != null? 
                                         titulo.PessoaFuncionario.Id + " - " + titulo.PessoaFuncionario.DadosGerais.Razao:string.Empty;

                tituloGrid.Pedido = titulo.PedidoVenda.Id.ToString();

                tituloGrid.Cliente = titulo.PedidoVenda !=null && titulo.PedidoVenda .Cliente != null? 
                                     titulo.PedidoVenda.Cliente.Id + " - " + titulo.PedidoVenda.Cliente.DadosGerais.Razao:string.Empty;


                tituloGrid.Endereco = titulo.PedidoVenda != null && titulo.PedidoVenda.EnderecoPedidoDeVenda != null && titulo.PedidoVenda.EnderecoPedidoDeVenda.CEP != null ? "Logradouro: " +
                                          titulo.PedidoVenda.EnderecoPedidoDeVenda.Rua + " - " + "N.: " +
                                          titulo.PedidoVenda.EnderecoPedidoDeVenda.Numero + " - " + "Bairro: " +
                                          titulo.PedidoVenda.EnderecoPedidoDeVenda.Bairro + " - " + "Cidade: " +
                                          titulo.PedidoVenda.EnderecoPedidoDeVenda.Cidade.Descricao : string.Empty;

                tituloGrid.DataElaboracao = titulo.DataElaboracao.ToString("dd/MM/yyyy");

                tituloGrid.Periodo = titulo.Periodo.Descricao();

                listaRoteirosGrid.Add(tituloGrid);
            }

            gcRoteiros.DataSource = listaRoteirosGrid;
            gcRoteiros.RefreshDataSource();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class RoteiroGrid
        {
            public string Funcionario { get; set; }

            public string Pedido { get; set; }

            public string Cliente { get; set; }

            public string Endereco { get; set; }

            public string DataElaboracao { get; set; }

            public string Periodo { get; set; }
        }

        #endregion

        private void btnAddAgendaDoDia_Click(object sender, EventArgs e)
        {
            var roteiro = new ServicoRoteiro().ConsulteLista(null, null, EnumStatusRoteiro.EMAGENDA, EnumDataFiltrarRoteiro.ELABORACAO,
                                                                DateTime.Now.Date, DateTime.Now.Date, 0, false);
            if (roteiro == null) return;

            if (txtIdPessoa.Text == string.Empty)
            {
                MessageBox.Show("Informe o Funcionário para continuar.",
                                "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var item in roteiro)
            {
                if (VerifiqueSePedidoEstahNaLista(item.PedidoVenda.Id))
                {
                    MessageBox.Show("Este pedido: "+ item.PedidoVenda.Id +" consta na lista. Por favor, informe outro para continuar. ",
                                    "Criação de Roteiros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    continue;
                }

                adicionaItemRoteiro(item.PedidoVenda, item.Id);
            }

            PreenchaGridRoteiros();
        }

        private void btnEditarPedido_Click(object sender, EventArgs e)
        {
            if (txtNumeroPedidoVendas.Text != string.Empty)
            {
                FormCadastroPedidoDeVenda formCadastroPedidoDeVenda = new FormCadastroPedidoDeVenda(txtNumeroPedidoVendas.Text.ToInt());

                formCadastroPedidoDeVenda.Show();
            }
        }
    }
}
