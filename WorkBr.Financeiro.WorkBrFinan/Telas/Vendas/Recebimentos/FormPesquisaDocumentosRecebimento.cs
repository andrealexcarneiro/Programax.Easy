using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Vendas.RecebimentoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;

namespace Programax.Easy.View.Telas.Vendas.Recebimentos
{
    public partial class FormPesquisaDocumentosRecebimento : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Recebimento> _listaRecebimentos;
        private bool _contemCaixa;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaDocumentosRecebimento()
        {
            InitializeComponent();

            PreenchaCboTipoDocumento();
            PreenchaCboTipoPesquisa();

            txtDataInicial.DateTime = DateTime.Now.Date;
            txtDataFinal.DateTime = DateTime.Now.Date;

            _listaRecebimentos = new List<Recebimento>();

            this.ActiveControl = cboTipoPesquisa;
        }

        #endregion

        #region "EVENTOS CONTROLES"

        private void FormPesquisaDocumentosRecebimento_Load(object sender, EventArgs e)
        {
            PreenchaInformacoesCaixa();

            if (_contemCaixa)
            {
                Pesquise();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void gcItens_KeyDown(object sender, KeyEventArgs e)
        {
            SelecioneRecebimento();
        }

        private void gcItens_Click(object sender, EventArgs e)
        {
            SelecioneRecebimento();
        }

        private void btnReceber_Click(object sender, EventArgs e)
        {
            RecebaDocumento();
        }

        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            if (HouveUmClickOuDuploClickNaLinha(gridControl2))
            {
                RecebaDocumento();
            }
        }

        private void cboControles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            var recebimento = RetorneRecebimentoSelecionado();

            if (recebimento != null && recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.PEDIDODEVENDAS)
            {
                ImprimaPedidoDeVenda(recebimento.Id);
            }            
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboTipoDocumento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoDocumentoRecebimento>();

            lista.Insert(0, null);

            cboTipoDocumento.Properties.DisplayMember = "Descricao";
            cboTipoDocumento.Properties.ValueMember = "Valor";
            cboTipoDocumento.Properties.DataSource = lista;

            cboTipoPesquisa.EditValue = EnumTipoPesquisa.ELABORACAO;
        }

        private void PreenchaCboTipoPesquisa()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPesquisa>();

            cboTipoPesquisa.Properties.DisplayMember = "Descricao";
            cboTipoPesquisa.Properties.ValueMember = "Valor";
            cboTipoPesquisa.Properties.DataSource = lista;

            cboTipoPesquisa.EditValue = EnumTipoPesquisa.ELABORACAO;
        }

        private void PreenchaInformacoesCaixa()
        {
            _contemCaixa = true;

            ServicoCaixa servicoCaixa = new ServicoCaixa();
            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null)
            {
                _contemCaixa = false;

                MessageBox.Show("Seu usuário não contém um caixa.");

                this.FecharFormulario();

                return;
            }

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();
            var movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

            if (movimentacaoCaixa == null)
            {
                _contemCaixa = false;

                MessageBox.Show("Você não possui um caixa aberto.");

                this.FecharFormulario();

                return;
            }

            txtNumeroCaixa.Text = caixa.Id.ToString();
            txtNomeCaixa.Text = caixa.NomeCaixa;
            txtDataCaixa.Text = movimentacaoCaixa.DataHoraAbertura.GetValueOrDefault().ToString("dd/MM/yyyy");
            txtUsuarioCaixa.Text = Sessao.PessoaLogada.DadosGerais.NomeFantasia;
            txtStatusCaixa.Text = movimentacaoCaixa.Status.Descricao();
        }

        private void Pesquise()
        {
            var parametro = new ServicoParametros().ConsulteParametros();

            ServicoRecebimento servicoRecebimento = new ServicoRecebimento();

            var tipoPesquisa = (EnumTipoPesquisa)cboTipoPesquisa.EditValue;

            DateTime dataInicial = txtDataInicial.Text.ToDate();
            DateTime dataFinal = txtDataFinal.Text.ToDate();

            EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento = (EnumTipoDocumentoRecebimento?)cboTipoDocumento.EditValue;

            if (!parametro.ParametrosFiscais.EmitirNotaSemReceber)
            {
                if (tipoPesquisa == EnumTipoPesquisa.ELABORACAO)
                {
                    _listaRecebimentos = servicoRecebimento.ConsulteListaPorDataElaboracao(dataInicial, dataFinal, tipoDocumentoRecebimento);
                }
                else
                {
                    _listaRecebimentos = servicoRecebimento.ConsulteListaPorDataFechamento(dataInicial, dataFinal, tipoDocumentoRecebimento);
                }
            }
            else
            {
                if (tipoPesquisa == EnumTipoPesquisa.ELABORACAO)
                {
                    var listaNf = servicoRecebimento.ConsulteListaPorDataElaboracaoNf(dataInicial, dataFinal, tipoDocumentoRecebimento);
                    converteListaRecebimento(listaNf);
                }
                else
                {
                    var listaNf = servicoRecebimento.ConsulteListaPorDataFechamentoNf(dataInicial, dataFinal, tipoDocumentoRecebimento);
                    converteListaRecebimento(listaNf);
                }
            }

            btnConcluirRecebimento.Visible = false;
            PreenchaCamposDocumento(null);

            PreenchaGrid();
        }

        public void converteListaRecebimento(List<RecebimentoNf> listaRecebimentoNf)
        {
            _listaRecebimentos = new List<Recebimento>();

            foreach (var item in listaRecebimentoNf)
            {
                Recebimento itemRecebimento = new Recebimento();

                itemRecebimento.AtendenteId = item.AtendenteId;
                itemRecebimento.AtendenteNomeFantasia = item.AtendenteNomeFantasia;
                itemRecebimento.BancoParaMovimento = item.BancoParaMovimento;
                itemRecebimento.CategoriaFinanceira = item.CategoriaFinanceira;
                itemRecebimento.CidadeDescricao = item.CidadeDescricao;
                itemRecebimento.CidadeId = item.CidadeId;
                itemRecebimento.ClienteCpfCnpj = item.ClienteCpfCnpj;
                itemRecebimento.ClienteDataCadastro = item.ClienteDataCadastro;
                itemRecebimento.ClienteId = item.ClienteId;
                itemRecebimento.ClienteInscricaoEstadual = item.ClienteInscricaoEstadual;
                itemRecebimento.ClienteNomeFantasia = item.ClienteNomeFantasia;
                itemRecebimento.ClienteStatus = item.ClienteStatus;
                itemRecebimento.ClienteTipoPessoa = item.ClienteTipoPessoa;
                itemRecebimento.DataElaboracao = item.DataElaboracao;
                itemRecebimento.DataFechamento = item.DataFechamento;
                itemRecebimento.Desconto = item.Desconto;
                itemRecebimento.DescontoEhPercentual = item.DescontoEhPercentual;
                itemRecebimento.EnderecoCep = item.EnderecoCep;
                itemRecebimento.EstadoNome = item.EstadoNome;
                itemRecebimento.EstadoUf = item.EstadoUf;
                itemRecebimento.Id = item.Id;
                itemRecebimento.ListaParcelasRecebimento = item.ListaParcelasRecebimento;
                itemRecebimento.NaturezaDescricao = item.NaturezaDescricao;
                itemRecebimento.NaturezaId = item.NaturezaId;
                itemRecebimento.StatusDocumento = item.StatusDocumento;
                itemRecebimento.TipoDocumento = item.TipoDocumento;
                itemRecebimento.UsuarioId = item.UsuarioId;
                itemRecebimento.UsuarioNomeFantasia = item.UsuarioNomeFantasia;
                itemRecebimento.ValorTotal = item.ValorTotal;
                itemRecebimento.VendedorId = item.VendedorId;
                itemRecebimento.VendedorNomeFantasia = item.VendedorNomeFantasia;
                                
                _listaRecebimentos.Add(itemRecebimento);
            }
        }


        private void PreenchaGrid()
        {
            List<RecebimentoGrid> listaRecebimentosGrid = new List<RecebimentoGrid>();

            foreach (var recebimento in _listaRecebimentos)
            {
                RecebimentoGrid recebimentoGrid = new RecebimentoGrid();

                recebimentoGrid.Id = recebimento.Id;
                recebimentoGrid.ClienteCpfCnpj = recebimento.ClienteCpfCnpj;
                recebimentoGrid.ClienteId = recebimento.ClienteId;
                recebimentoGrid.ClienteNomeFantasia = recebimento.ClienteNomeFantasia;
                recebimentoGrid.DataElaboracao = recebimento.DataElaboracao;
                recebimentoGrid.StatusDocumento = recebimento.StatusDocumento.Descricao();
                recebimentoGrid.TipoDocumento = recebimento.TipoDocumento.Descricao();
                recebimentoGrid.EnumTipoDocumento = recebimento.TipoDocumento;
                recebimentoGrid.UsuarioNomeFantasia = recebimento.UsuarioId + " - " + recebimento.UsuarioNomeFantasia;
                recebimentoGrid.ValorTotal = recebimento.ValorTotal;

                listaRecebimentosGrid.Add(recebimentoGrid);
            }

            gcItens.DataSource = listaRecebimentosGrid;
            gcItens.RefreshDataSource();
        }

        private void SelecioneRecebimento()
        {
            var recebimento = RetorneRecebimentoSelecionado();

            PreenchaCamposDocumento(recebimento);
            AltereNomeBotaoDeAcordoComTipoRecebimento(recebimento);
        }

        private Recebimento RetorneRecebimentoSelecionado()
        {
            var recebimentoSelecionado = _listaRecebimentos.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt() &&
                                                                                                                        item.TipoDocumento == (EnumTipoDocumentoRecebimento)colunaEnumTipoDocumento.View.GetFocusedRowCellValue(colunaEnumTipoDocumento));

            return recebimentoSelecionado;
        }

        private void PreenchaCamposDocumento(Recebimento recebimento)
        {
            if (recebimento != null)
            {
                txtIdParceiro.Text = recebimento.ClienteId.ToString();
                txtNomeFantasiaParceiro.Text = recebimento.ClienteNomeFantasia;
                txtCpfCnpjParceiro.Text = recebimento.ClienteCpfCnpj;
                txtStatusCliente.Text = recebimento.ClienteStatus == "A" ? "ATIVO" : "INATIVO";

                txtCep.Text = recebimento.EnderecoCep;
                txtCidade.Text = recebimento.CidadeDescricao;
                txtEstado.Text = recebimento.EstadoUf;

                txtInscricaoEstadual.Text = recebimento.ClienteInscricaoEstadual;
                txtNaturezaOperacao.Text = recebimento.NaturezaDescricao;

                txtTipoPessoa.Text = recebimento.ClienteTipoPessoa.Descricao();
                txtDataCadastro.Text = recebimento.ClienteDataCadastro.ToString("dd/MM/yyyy");

                txtAtendente.Text = recebimento.AtendenteId + " - " + recebimento.AtendenteNomeFantasia;
                txtVendedor.Text = recebimento.VendedorId + " - " + recebimento.VendedorNomeFantasia;

                txtDataElaboracao.Text = recebimento.DataElaboracao.ToString("dd/MM/yyyy");
                txtUsuario.Text = recebimento.UsuarioId + " - " + recebimento.UsuarioNomeFantasia;
            }
            else
            {
                txtIdParceiro.Text = string.Empty;
                txtNomeFantasiaParceiro.Text = string.Empty;
                txtCpfCnpjParceiro.Text = string.Empty;
                txtStatusCliente.Text = string.Empty;

                txtCep.Text = string.Empty;
                txtCidade.Text = string.Empty;
                txtEstado.Text = string.Empty;

                txtInscricaoEstadual.Text = string.Empty;
                txtNaturezaOperacao.Text = string.Empty;

                txtTipoPessoa.Text = string.Empty;
                txtDataCadastro.Text = string.Empty;

                txtAtendente.Text = string.Empty;
                txtVendedor.Text = string.Empty;

                txtDataElaboracao.Text = string.Empty;
                txtUsuario.Text = string.Empty;
            }
        }

        private void AltereNomeBotaoDeAcordoComTipoRecebimento(Recebimento recebimento)
        {
            if (recebimento != null)
            {
                btnConcluirRecebimento.Visible = true;

                if (recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.PEDIDODEVENDAS)
                {
                    btnConcluirRecebimento.Text = " Concluir Venda";
                }
            }
        }

        private void RecebaDocumento()
        {
            var recebimento = RetorneRecebimentoSelecionado();

            FormRecebimento formRecebimento = new FormRecebimento();
            formRecebimento.RecebaDocumento(recebimento);

            formRecebimento.ShowDialog();

            if (formRecebimento.FormCadastroNotaFiscal != null)
            {
                formRecebimento.FormCadastroNotaFiscal.Focus();
            }

            Pesquise();
        }

        private void ImprimaPedidoDeVenda(int idPedidoDeVenda)
        {
            RelatorioPedidoVenda relatorio = new RelatorioPedidoVenda(idPedidoDeVenda , Negocio.Cadastros.Enumeradores.EnumTipoEndereco.PRINCIPAL);
            relatorio.GereRelatorio();

            using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
            {
                // Invoke the Ribbon Print Preview form modally, 
                // and load the report document into it.
                printTool.ShowRibbonPreviewDialog();

                // Invoke the Ribbon Print Preview form
                // with the specified look and feel setting.
                printTool.ShowRibbonPreview(UserLookAndFeel.Default);
            }
        }

        #endregion

        #region " CLASSES E ENUMERADORES AUXILIARES "

        private class RecebimentoGrid
        {
            public int Id { get; set; }

            public DateTime DataElaboracao { get; set; }

            public int ClienteId { get; set; }

            public string ClienteNomeFantasia { get; set; }

            public string ClienteCpfCnpj { get; set; }

            public string TipoDocumento { get; set; }

            public double ValorTotal { get; set; }

            public string UsuarioNomeFantasia { get; set; }

            public string StatusDocumento { get; set; }

            public EnumTipoDocumentoRecebimento EnumTipoDocumento { get; set; }
        }

        private enum EnumTipoPesquisa
        {
            [Description("ELABORAÇÃO")]
            ELABORACAO,

            [Description("FECHAMENTO")]
            FECHAMENTO
        }

        #endregion

        
    }
}
