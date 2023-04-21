using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.FabricanteServ;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioDeTransportes : FormularioBase
    {
        #region "VARIÁVEIS GLOBAIS"

        public List<RelatorioDeTransportes.ItemPedidoRelatorio> _listaPedidos;
        private int _intLinhaGrid;

        #endregion

        #region " CONSTRUTOR "

        public FormRelatorioDeTransportes()
        {
            InitializeComponent();

            PreenchaCboFuncoes();
            PreenchaPrimeiroEUltimoDiaMes();
            PreenchaCboFabricantes();
            PreenchaCboMarcas();
            PreenchaCboCategorias();
            txtDataInicialEmissao.Text = string.Empty;
            txtDataFinalEmissao.Text = string.Empty;

            _listaPedidos = new List<RelatorioDeTransportes.ItemPedidoRelatorio>();

            DesabiliteSelecaoParceiroSenaoPermitido();
            txtPedido.Focus();
        }

        #endregion
        
        #region " EVENTOS CONTROLES "

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            EnumPesquisaPorFuncao? funcao = (EnumPesquisaPorFuncao?)cboFuncao.EditValue;

            bool statusAberto = chkStatusAberto.Checked;
            bool statusOrcamento = chkStatusOrcamento.Checked;
            bool statusCancelado = chkStatusCancelado.Checked;
            bool statusEmLiberacao = chkStatusEmLiberacao.Checked;
            bool statusRecusado = chkStatusRecusado.Checked;
            bool statusReservado = chkStatusReservado.Checked;
            bool statusFaturado = chkStatusFaturado.Checked;
            bool statusEmitidoNFe = chkStatusEmitidoNFe.Checked;

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

            Pessoa pessoa = cboColaboradores.EditValue != null ? new Pessoa { Id = cboColaboradores.EditValue.ToInt() } : null;

            DateTime dataInicial = rdbPeriodoFaturamento.Checked ? txtDataInicialFaturamento.Text.ToDate() : txtDataInicialEmissao.Text.ToDate();
            DateTime dataFinal = rdbPeriodoFaturamento.Checked ? txtDataFinalFaturamento.Text.ToDate() : txtDataFinalEmissao.Text.ToDate();

            radioButton1.Checked = true; //Vamos deixar sempre ordenado por data de transporte
            EnumOrdenacaoPesquisaVwVendas ordenacao = (EnumOrdenacaoPesquisaVwVendas)pnlOrdenacao.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            string inconsistencias = string.Empty;

            //inconsistencias += funcao == null ? "Informe uma função.\n\n" : string.Empty;
            if (chkPeriodoEntrega.Checked)
            {
                inconsistencias += dataInicial == DateTime.MinValue ? "Informe a data inicial do período.\n\n" : string.Empty;
                inconsistencias += dataFinal == DateTime.MinValue ? "Informe a data final do período.\n\n" : string.Empty;
            }

            if (!string.IsNullOrEmpty(inconsistencias))
            {
                MessageBox.Show(inconsistencias);
                this.Cursor = Cursors.Default;
                return;
            }

            RelatorioDeTransportes relatorioVendas = new RelatorioDeTransportes(funcao.GetValueOrDefault(),
                                                                                               pessoa, 
                                                                                               rdbPeriodoFaturamento.Checked, 
                                                                                               dataInicial, 
                                                                                               dataFinal, 
                                                                                               statusAberto,
                                                                                               statusOrcamento,
                                                                                               statusCancelado,
                                                                                               statusEmLiberacao,
                                                                                               statusRecusado,
                                                                                               statusReservado,
                                                                                               statusFaturado,
                                                                                               statusEmitidoNFe,
                                                                                               chkPeriodoEntrega.Checked,
                                                                                               ordenacao, marca,
                                                                                               fabricante,subgrupo,
                                                                                               categoria,grupo,_listaPedidos);

            TratamentosDeTela.ExibirRelatorio(relatorioVendas);
            this.Cursor = Cursors.Default;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboFuncao_EditValueChanged(object sender, EventArgs e)
        {
            EnumPesquisaPorFuncao? funcao = (EnumPesquisaPorFuncao?)cboFuncao.EditValue;

            if (funcao == null)
            {
                cboColaboradores.Properties.DataSource = null;
                cboColaboradores.EditValue = null;
            }

            else if (funcao == EnumPesquisaPorFuncao.INDICADOR)
            {
                PreenchaCboIndicadores();
            }

            else if (funcao == EnumPesquisaPorFuncao.ATENDENTE)
            {
                PreenchaCboAtendentes();
            }

            else if (funcao == EnumPesquisaPorFuncao.VENDEDOR)
            {
                PreenchaCboVendedores();
            }

            else if (funcao == EnumPesquisaPorFuncao.SUPERVISOR)
            {
                PreenchaCboSupervisores();
            }
        }

        private void rdbPeriodoFaturamento_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPeriodoFaturamento.Checked)
            {
                txtDataInicialEmissao.Enabled = false;
                txtDataInicialEmissao.Text = string.Empty;

                txtDataFinalEmissao.Enabled = false;
                txtDataFinalEmissao.Text = string.Empty;

                txtDataInicialFaturamento.Enabled = true;
                txtDataFinalFaturamento.Enabled = true;
            }
            else
            {
                txtDataInicialEmissao.Enabled = true;
                txtDataFinalEmissao.Enabled = true;

                txtDataInicialFaturamento.Enabled = false;
                txtDataInicialFaturamento.Text = string.Empty;

                txtDataFinalFaturamento.Enabled = false;
                txtDataFinalFaturamento.Text = string.Empty;
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboFuncoes()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumPesquisaPorFuncao>();

            lista.Insert(0, null);

            cboFuncao.Properties.DataSource = lista;
            cboFuncao.Properties.ValueMember = "Valor";
            cboFuncao.Properties.DisplayMember = "Descricao";
        }

        private void PreenchaCboIndicadores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaIndicadoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboColaboradores.Properties.DisplayMember = "Descricao";
            cboColaboradores.Properties.ValueMember = "Valor";
            cboColaboradores.Properties.DataSource = listaObjetoValor;
        }

        private void PreenchaCboAtendentes()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaAtendentesAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboColaboradores.Properties.DisplayMember = "Descricao";
            cboColaboradores.Properties.ValueMember = "Valor";
            cboColaboradores.Properties.DataSource = listaObjetoValor;
        }

        private void PreenchaCboVendedores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaVendedoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboColaboradores.Properties.DisplayMember = "Descricao";
            cboColaboradores.Properties.ValueMember = "Valor";
            cboColaboradores.Properties.DataSource = listaObjetoValor;
        }

        private void PreenchaCboSupervisores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaSupervisoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboColaboradores.Properties.DisplayMember = "Descricao";
            cboColaboradores.Properties.ValueMember = "Valor";
            cboColaboradores.Properties.DataSource = listaObjetoValor;
        }

        private void PreenchaPrimeiroEUltimoDiaMes()
        {
            var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

            txtDataInicialEmissao.DateTime = primeiroDiaMes;
            txtDataFinalEmissao.DateTime = ultimoDiaMes;
        }

        private void DesabiliteSelecaoParceiroSenaoPermitido()
        {
            var permissaoVisualizarTodosVendedores = Sessao.ListaDePermissoes.FirstOrDefault(permissao => permissao.Funcionalidade == EnumFuncionalidade.RELATORIOVENDASVENDEDORVISUALIZARTODOSVENDEDORES);

            if (permissaoVisualizarTodosVendedores == null || !permissaoVisualizarTodosVendedores.Acessar)
            {
                if (Sessao.PessoaLogada.Vendedor.EhAtendente)
                {
                    cboFuncao.EditValue = EnumPesquisaPorFuncao.ATENDENTE;
                }
                else if (Sessao.PessoaLogada.Vendedor.EhIndicador)
                {
                    cboFuncao.EditValue = EnumPesquisaPorFuncao.INDICADOR;
                }
                else if (Sessao.PessoaLogada.Vendedor.EhVendedor)
                {
                    cboFuncao.EditValue = EnumPesquisaPorFuncao.VENDEDOR;
                }
                else if (Sessao.PessoaLogada.Vendedor.EhSupervisor)
                {
                    cboFuncao.EditValue = EnumPesquisaPorFuncao.SUPERVISOR;
                }

                cboColaboradores.EditValue = Sessao.PessoaLogada.Id;

                cboFuncao.Properties.ReadOnly = true;
                cboColaboradores.Properties.ReadOnly = true;
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

        private void InsiraPedido()
        {
            var pedidoExiste = _listaPedidos.Find(x => x.NumeroDoPedido == txtPedido.Text.ToInt());
            if (pedidoExiste != null)
            {
                MessageBoxAkil.Show("O Pedido digitado está na lista. Informe outro, por favor!", " Informe outro Pedido.");
                return;
            }

            if (string.IsNullOrEmpty(txtPedido.Text))
            {
                MessageBoxAkil.Show("Pedido não informado.", "Pedido não informado.");
                return;
            }

            var pedido = RetornePedido();

            if (pedido == null)
            {
                MessageBoxAkil.Show("Pedido não encontrado! Talvez, não esteja Faturado ou Emitido Nota Fiscal.", "Aviso");

                return;
            }

            RelatorioDeTransportes.ItemPedidoRelatorio itemPedidoRelatorio = new RelatorioDeTransportes.ItemPedidoRelatorio();
            itemPedidoRelatorio.NumeroDoPedido = pedido.Id;
            itemPedidoRelatorio.Cliente = pedido.Cliente.DadosGerais.Razao;
            itemPedidoRelatorio.CodigoDoCliente = pedido.Cliente.Id;
            itemPedidoRelatorio.IndicadorNome = pedido.Indicador != null? pedido.Indicador.DadosGerais.Razao.ToString(): null;
            itemPedidoRelatorio.AtendenteNome = pedido.Atendente != null? pedido.Atendente.DadosGerais.Razao.ToString(): null;
            itemPedidoRelatorio.VendedorNome = pedido.Vendedor != null? pedido.Vendedor.DadosGerais.Razao.ToString(): null;
            itemPedidoRelatorio.SupervisorNome = pedido.Supervisor != null? pedido.Supervisor.DadosGerais.Razao.ToString(): null;

            _listaPedidos.Add(itemPedidoRelatorio);

            PreenchaGrid();
            
            txtPedido.Text = string.Empty;            
        }

        private PedidoDeVenda RetornePedido()
        {            
            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                        
            return servicoPedido.ConsultePedidoFaturadoOuEmitidoNfe(txtPedido.Text.ToInt());            
        }

        private void PreenchaGrid()
        {
            List<RelatorioDeTransportes.ItemPedidoRelatorio> listaPedidos = new List<RelatorioDeTransportes.ItemPedidoRelatorio>();

            for (int i = 0; i < _listaPedidos.Count; i++)
            {
                var item = _listaPedidos[i];

                RelatorioDeTransportes.ItemPedidoRelatorio itemPedidoGrid = new RelatorioDeTransportes.ItemPedidoRelatorio();

                itemPedidoGrid.Item = (i + 1).ToString();
                itemPedidoGrid.NumeroDoPedido = item.NumeroDoPedido;
                itemPedidoGrid.Cliente = item.Cliente;
                
                listaPedidos.Add(itemPedidoGrid);
                _intLinhaGrid = i;
            }

            gridProdutos.DataSource = listaPedidos;
            if (_listaPedidos.Count > 0)
            {
                gridProdutos.FirstDisplayedScrollingRowIndex = _listaPedidos.Count - 1;
                gridProdutos[0, 0].Selected = false;
                gridProdutos[0, _intLinhaGrid].Selected = true;
            }
        }

        private void LimpeLista()
        {
            _listaPedidos = new List<RelatorioDeTransportes.ItemPedidoRelatorio>();
            PreenchaGrid();
        }
        
        #endregion

        #region "EVENTOS DOS CONTROLES DO FORMULÁRIO"


        private void cboCategorias_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboGrupos();
        }

        private void cboSubGrupos_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cboGrupos_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboSubGrupos();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl15_Click(object sender, EventArgs e)
        {

        }

        private void txtPedido_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void txtPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsiraPedido();
            }
        }

        private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void chkPeriodoEntrega_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPeriodoEntrega.Checked)
            {
                pnlPeriodoDataEntrega.Enabled = true;
                LimpeLista();
                pnlListaPedidos.Enabled = false;
            }
            else
            {
                pnlPeriodoDataEntrega.Enabled = false;
                pnlListaPedidos.Enabled = true;
                txtPedido.Focus();
            }
        }

        private void btnLimparLista_Click(object sender, EventArgs e)
        {
            LimpeLista();
            txtPedido.Focus();
        }

        private void btnInserirPedidos_Click(object sender, EventArgs e)
        {
            InsiraPedido();
        }

        private void txtDataFinalEmissao_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void MenuGrid_Click(object sender, EventArgs e)
        {
            if (gridProdutos.CurrentRow == null) return;

            _listaPedidos.RemoveAt(gridProdutos.CurrentRow.Index);                       
            PreenchaGrid();
        }

        private void btnPesquisaDocumentos_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda();

            var pedidoDeVenda = formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVenda();

            if (pedidoDeVenda != null)
            {
                txtPedido.Text= pedidoDeVenda.Id.ToString();
            }
            this.Cursor = Cursors.Default;
        }

        private void gridProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Tem certeza que deseja excluir este item!", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _listaPedidos.RemoveAt(gridProdutos.CurrentRow.Index);
                    PreenchaGrid();
                }
            }           
        }
    }

    #endregion


}

