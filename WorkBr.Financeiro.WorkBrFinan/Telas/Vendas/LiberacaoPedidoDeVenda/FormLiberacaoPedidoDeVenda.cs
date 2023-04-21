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
using Programax.Easy.Servico.Vendas.LiberacaoDocumentoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using System.Threading;
using System.ComponentModel;
using DevExpress.XtraEditors;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.LiberacaoDocumentoObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Vendas.TrocaPedidoDeVendas;
using Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;

namespace Programax.Easy.View.Telas.Vendas.LiberacaoPedidoDeVenda
{
    public partial class FormLiberacaoPedidoDeVenda : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<LiberacaoDocumento> _listaLiberacaoDocumentos;
        private Thread _threadConsulta;
        private List<LiberacaoDocumentoGrid> _listaLiberacaoDocumentosGrid;
        private LiberacaoDocumento _liberacaoDocumentoSelecionado;

        #endregion

        #region " CONSTRUTOR "

        public FormLiberacaoPedidoDeVenda()
        {
            InitializeComponent();

            _listaLiberacaoDocumentos = new List<LiberacaoDocumento>();
            _listaLiberacaoDocumentosGrid = new List<LiberacaoDocumentoGrid>();

            txtDataInicial.DateTime = DateTime.Now.Date;
            txtDataFinal.DateTime = DateTime.Now.Date;

            PreenchaCboAtendentes();
            PreenchaCboVendedores();
            PreenchaCboTipoDocumento();

            Control.CheckForIllegalCrossThreadCalls = false;

            this.ActiveControl = txtDataInicial;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void tmrAtualizarPedidosVenda_Tick(object sender, EventArgs e)
        {
            PesquiseLiberacoesEPreenchaGrid();
        }

        private void btnLiberarPedido_Click(object sender, EventArgs e)
        {
            Action actionLiberacaoPedido = () =>
            {
                var liberacaoDocumento = RetorneLiberacaoDocumentoSelecionado();

                if (liberacaoDocumento != null)
                {
                    ServicoLiberacaoDocumento servicoLiberacaoPedidoDeVenda = new ServicoLiberacaoDocumento();

                    servicoLiberacaoPedidoDeVenda.LibereDocumento(liberacaoDocumento);
                }

                PesquiseLiberacoesEPreenchaGrid();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionLiberacaoPedido, mensagemDeSucesso: "Documento Liberado com Sucesso!", tituloMensagemDeErro: "Documento Liberado");
        }

        private void btnRecusarPedido_Click(object sender, EventArgs e)
        {
            Action actionLiberacaoPedido = () =>
            {
                var liberacaoDocumento = RetorneLiberacaoDocumentoSelecionado();

                if (liberacaoDocumento != null)
                {
                    ServicoLiberacaoDocumento servicoLiberacaoPedidoDeVenda = new ServicoLiberacaoDocumento();

                    servicoLiberacaoPedidoDeVenda.RecuseDocumento(liberacaoDocumento);
                }

                PesquiseLiberacoesEPreenchaGrid();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionLiberacaoPedido, mensagemDeSucesso: "Documento Recusado com Sucesso!", tituloMensagemDeErro: "Documento Recusado");
        }

        private void btnVisualizarPedido_Click(object sender, EventArgs e)
        {
            var liberacaoDocumento = RetorneLiberacaoDocumentoSelecionado();

            if (liberacaoDocumento != null)
            {
                if (liberacaoDocumento.TipoDocumento == EnumTipoDocumentoLiberacao.PEDIDODEVENDAS)
                {
                    FormCadastroPedidoDeVenda formCadastroPedidoDeVenda = new FormCadastroPedidoDeVenda();

                    ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                    var pedido = servicoPedidoDeVenda.Consulte(liberacaoDocumento.Id);

                    formCadastroPedidoDeVenda.PreenchaPedidoDeVenda(pedido);

                    formCadastroPedidoDeVenda.Show();
                }
                else
                {
                    FormCadastroTrocaPedidoDeVenda formCadastroTrocaPedidoDeVenda = new FormCadastroTrocaPedidoDeVenda();

                    ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda();

                    var troca = servicoTrocaPedidoDeVenda.Consulte(liberacaoDocumento.Id);

                    formCadastroTrocaPedidoDeVenda.EditeTroca(troca);

                    formCadastroTrocaPedidoDeVenda.Show();
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            PesquiseLiberacoesEPreenchaGrid();
        }

        private void FormLiberacaoPedidoDeVenda_Load(object sender, EventArgs e)
        {
            _threadConsulta = new Thread(new ThreadStart(this.PesquisePelaThread));
            _threadConsulta.IsBackground = true;
            _threadConsulta.Start();
        }

        private void FormLiberacaoPedidoDeVenda_FormClosing(object sender, FormClosingEventArgs e)
        {
            _threadConsulta.Abort();
        }

        private void cboAtendentes_EditValueChanged(object sender, EventArgs e)
        {
            if (cboAtendentes.EditValue != null)
            {
                cboVendedores.EditValue = null;
                cboVendedores.Enabled = false;
            }
            else
            {
                cboVendedores.Enabled = true;
            }
        }

        private void cboVendedores_EditValueChanged(object sender, EventArgs e)
        {
            if (cboVendedores.EditValue != null)
            {
                cboAtendentes.EditValue = null;
                cboAtendentes.Enabled = false;
            }
            else
            {
                cboAtendentes.Enabled = true;
            }
        }

        private void gcItens_Click(object sender, EventArgs e)
        {
            SelecionePedidoDeVenda();
        }

        private void gcItens_KeyDown(object sender, KeyEventArgs e)
        {
            SelecionePedidoDeVenda();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboTipoDocumento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoDocumentoLiberacao>();

            lista.Insert(0, null);

            cboTipoDocumento.Properties.DataSource = lista;
            cboTipoDocumento.Properties.DisplayMember = "Descricao";
            cboTipoDocumento.Properties.ValueMember = "Valor";
        }

        private void PreenchaGridLiberacoes()
        {
            RemovaLiberacoesQueNaoExistemMais();
            AdicioneNovasLiberacoes();

            gcItens.DataSource = _listaLiberacaoDocumentosGrid;
            gcItens.RefreshDataSource();
        }

        private void RemovaLiberacoesQueNaoExistemMais()
        {
            int totalPedidosVendasGrid = _listaLiberacaoDocumentosGrid.Count;

            for (int i = 0; i < totalPedidosVendasGrid; i++)
            {
                var pedidoGrid = _listaLiberacaoDocumentosGrid[i];

                if (!_listaLiberacaoDocumentos.Exists(pedido => pedido.Id == pedidoGrid.Id))
                {
                    if (_liberacaoDocumentoSelecionado != null && _liberacaoDocumentoSelecionado.Id == pedidoGrid.Id)
                    {
                        PreenchaCamposLiberacaoDocumento(null);
                    }

                    _listaLiberacaoDocumentosGrid.Remove(pedidoGrid);

                    totalPedidosVendasGrid--;
                    i--;
                }
            }
        }

        private void AdicioneNovasLiberacoes()
        {
            foreach (var documento in _listaLiberacaoDocumentos)
            {
                if (!_listaLiberacaoDocumentosGrid.Exists(pedidoGrid => pedidoGrid.Id == documento.Id))
                {
                    LiberacaoDocumentoGrid liberacaoDocumentoGrid = new LiberacaoDocumentoGrid();

                    liberacaoDocumentoGrid.Id = documento.Id;

                    liberacaoDocumentoGrid.IdCliente = documento.ClienteId;
                    liberacaoDocumentoGrid.CnpjCpf = documento.ClienteCpfCnpj;
                    liberacaoDocumentoGrid.NomeCliente = documento.ClienteNomeFantasia;
                    liberacaoDocumentoGrid.Situacao = "EM LIBERAÇÃO";
                    liberacaoDocumentoGrid.TipoDocumento = documento.TipoDocumento.Descricao();
                    liberacaoDocumentoGrid.DataElaboracao = documento.DataElaboracao.ToString("dd/MM/yyyy");
                    liberacaoDocumentoGrid.Usuario = documento.UsuarioId + " - " + documento.UsuarioNomeFantasia;
                    liberacaoDocumentoGrid.Desconto = documento.Desconto.ToString("0.00");
                    liberacaoDocumentoGrid.ValorTotal = documento.ValorTotal.ToString("0.00");

                    _listaLiberacaoDocumentosGrid.Add(liberacaoDocumentoGrid);
                }
            }
        }

        private void PesquiseLiberacoesEPreenchaGrid()
        {
            PesquiseLiberacoes();

            PreenchaGridLiberacoes();
        }

        private void PesquiseLiberacoes()
        {
            ServicoLiberacaoDocumento servicoLiberacaoPedidoDeVenda = new ServicoLiberacaoDocumento();

            DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();

            Pessoa atendente = cboAtendentes.EditValue != null ? new Pessoa { Id = cboAtendentes.EditValue.ToInt() } : null;
            Pessoa vendedor = cboVendedores.EditValue != null ? new Pessoa { Id = cboVendedores.EditValue.ToInt() } : null;

            EnumTipoDocumentoLiberacao? tipoDocumento = (EnumTipoDocumentoLiberacao?)cboTipoDocumento.EditValue;

            _listaLiberacaoDocumentos = servicoLiberacaoPedidoDeVenda.ConsulteLista(dataInicial, dataFinal, atendente, vendedor, tipoDocumento);
        }

        private LiberacaoDocumento RetorneLiberacaoDocumentoSelecionado()
        {
            var liberacaoDocumentoSelecionado = _listaLiberacaoDocumentos.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

            return liberacaoDocumentoSelecionado;
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

            cboAtendentes.Properties.DisplayMember = "Descricao";
            cboAtendentes.Properties.ValueMember = "Valor";
            cboAtendentes.Properties.DataSource = listaObjetoValor;
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

            cboVendedores.Properties.DisplayMember = "Descricao";
            cboVendedores.Properties.ValueMember = "Valor";
            cboVendedores.Properties.DataSource = listaObjetoValor;
        }

        delegate void MetodoDelegateParaPreencherAGrid(bool manterRegistroSelecionado);

        private void PesquisePelaThread()
        {
            while (true)
            {
                try
                {
                    PesquiseLiberacoes();
                    PreenchaGridLiberacoes();

                    Thread.Sleep(200);
                }
                catch (Exception)
                {
                }
            }
        }

        private void SelecionePedidoDeVenda()
        {
            var pedidoDeVenda = RetorneLiberacaoDocumentoSelecionado();

            PreenchaCamposLiberacaoDocumento(pedidoDeVenda);
        }

        private void PreenchaCamposLiberacaoDocumento(LiberacaoDocumento liberacaoDocumento)
        {
            _liberacaoDocumentoSelecionado = liberacaoDocumento;

            DesativeCampoMotivo(txtMotivoClienteBloqueado);
            DesativeCampoMotivo(txtMotivoDescontoAcima);
            DesativeCampoMotivo(txtMotivoFinanceiroEmAberto);
            DesativeCampoMotivo(txtMotivoItemSemEstoque);
            DesativeCampoMotivo(txtMotivoSemSaldoCredito);

            if (liberacaoDocumento != null)
            {
                txtId.Text = liberacaoDocumento.Id.ToString();
                txtDataElaboracao.Text = liberacaoDocumento.DataElaboracao.ToString("dd/MM/yyyy");
                txtTipoDocumento.Text = liberacaoDocumento.TipoDocumento.Descricao();
                txtUsuario.Text = liberacaoDocumento.UsuarioId + " - " + liberacaoDocumento.UsuarioNomeFantasia;
                txtSituacao.Text = "EM LIBERAÇÃO";

                txtIdCliente.Text = liberacaoDocumento.ClienteId.ToString();
                txtNomeCliente.Text = liberacaoDocumento.ClienteNomeFantasia;
                txtCpfCnpj.Text = liberacaoDocumento.ClienteCpfCnpj;
                txtStatusCliente.Text = liberacaoDocumento.ClienteStatus == "A" ? "ATIVO" : "INATIVO";

                txtCidade.Text = liberacaoDocumento.CidadeDescricao;
                txtEstado.Text = liberacaoDocumento.EstadoUf;
                txtInscricaoEstadual.Text = liberacaoDocumento.ClienteInscricaoEstadual;
                txtInscricaoMunicipal.Text = liberacaoDocumento.ClienteInscricaoMunicipal;
                txtTipoPessoa.Text = liberacaoDocumento.ClienteTipoPessoa.Descricao();
                txtDataCadastroCliente.Text = liberacaoDocumento.ClienteDataCadastro.ToString("dd/MM/yyyy");

                if (liberacaoDocumento.MotivoBloqueioClienteBloqueado)
                    AtiveCampoMotivo(txtMotivoClienteBloqueado);

                if (liberacaoDocumento.MotivoBloqueioDescontoAcima)
                    AtiveCampoMotivo(txtMotivoDescontoAcima);

                if (liberacaoDocumento.MotivoBloqueioFinanceiroEmAberto)
                    AtiveCampoMotivo(txtMotivoFinanceiroEmAberto);

                if (liberacaoDocumento.MotivoBloqueioItemSemEstoque)
                    AtiveCampoMotivo(txtMotivoItemSemEstoque);

                if (liberacaoDocumento.MotivoBloqueioSemSaldoCredito)
                    AtiveCampoMotivo(txtMotivoSemSaldoCredito);

                if(Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.LIBERACAODOCUMENTO).Alterar)
                {
                    btnLiberarPedido.Visible = true;
                    btnRecusarPedido.Visible = true;
                }
                else
                {
                    btnLiberarPedido.Visible = false;
                    btnRecusarPedido.Visible = false;
                }

                if (liberacaoDocumento.TipoDocumento == EnumTipoDocumentoLiberacao.PEDIDODEVENDAS)
                {
                    btnVisualizarPedido.Text = " Visualizar Pedido";
                    btnLiberarPedido.Text = " Liberar Pedido";
                    btnRecusarPedido.Text = " Recusar Pedido";
                }
                else
                {
                    btnVisualizarPedido.Text = " Visualizar Troca";
                    btnLiberarPedido.Text = " Liberar Troca";
                    btnRecusarPedido.Text = " Recusar Troca";
                }
            }
            else
            {
                txtId.Text = string.Empty;
                txtDataElaboracao.Text = string.Empty;
                txtTipoDocumento.Text = string.Empty;
                txtUsuario.Text = string.Empty;
                txtSituacao.Text = string.Empty;

                txtIdCliente.Text = string.Empty;
                txtNomeCliente.Text = string.Empty;
                txtCpfCnpj.Text = string.Empty;
                txtStatusCliente.Text = string.Empty;

                txtCidade.Text = string.Empty;
                txtEstado.Text = string.Empty;
                txtInscricaoEstadual.Text = string.Empty;
                txtInscricaoMunicipal.Text = string.Empty;
                txtTipoPessoa.Text = string.Empty;
                txtDataCadastroCliente.Text = string.Empty;
            }
        }

        private void AtiveCampoMotivo(TextEdit textboxMotivo)
        {
            textboxMotivo.Font = new Font(textboxMotivo.Font, FontStyle.Bold);
            textboxMotivo.ForeColor = Color.Red;
        }

        private void DesativeCampoMotivo(TextEdit textBoxMotivo)
        {
            textBoxMotivo.Font = new Font(textBoxMotivo.Font, FontStyle.Regular);
            textBoxMotivo.ForeColor = Color.Gray;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class LiberacaoDocumentoGrid
        {
            public int Id { get; set; }

            public int IdCliente { get; set; }

            public string CnpjCpf { get; set; }

            public string NomeCliente { get; set; }

            public string Situacao { get; set; }

            public string DataElaboracao { get; set; }

            public string TipoDocumento { get; set; }

            public string Usuario { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }
        }

        #endregion
    }
}
