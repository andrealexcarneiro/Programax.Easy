using System;
using System.Windows.Forms;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco
{
    partial class FormCadastroMovimentacoesBanco
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCadastroMovimentacoesBanco));
            this.colunaEstornado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItens = new DevExpress.XtraGrid.GridControl();
            this.gridControl2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataHora = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaParceiro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricaoMovimentacao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaOrigemMovimentacao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumeroDocumento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCategoria = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaEntrada = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaSaida = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaSaldo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.txtSaldoFinal = new DevExpress.XtraEditors.TextEdit();
            this.txtSaidas = new DevExpress.XtraEditors.TextEdit();
            this.txtEntradas = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.txtStatusBanco = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.txtUsuarioBanco = new DevExpress.XtraEditors.TextEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataBanco = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricaoBanco = new DevExpress.XtraEditors.TextEdit();
            this.btnPesquisarBanco = new System.Windows.Forms.PictureBox();
            this.txtIdBanco = new DevExpress.XtraEditors.TextEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFecharBanco = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnPagarReceber = new System.Windows.Forms.Button();
            this.btnDadosCruzados = new System.Windows.Forms.Button();
            this.btnAbrirBanco = new System.Windows.Forms.Button();
            this.btnImprimirMovimentacao = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblDescricaoMovimentacao = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricaoDaMovimentacao = new DevExpress.XtraEditors.TextEdit();
            this.labComplementoEndereco = new DevExpress.XtraEditors.LabelControl();
            this.labBanco = new DevExpress.XtraEditors.LabelControl();
            this.btnInserirAtualizarItem = new System.Windows.Forms.Button();
            this.btnCancelarItem = new System.Windows.Forms.Button();
            this.btnEstornarItem = new System.Windows.Forms.Button();
            this.cboTiposMovimentacoes = new DevExpress.XtraEditors.LookUpEdit();
            this.btnAdicionarCategoria = new System.Windows.Forms.Button();
            this.txtNumeroDocumento = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cboCategoriaFinanceira = new DevExpress.XtraEditors.LookUpEdit();
            this.btnAdicionarParceiro = new System.Windows.Forms.Button();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboParceiro = new DevExpress.XtraEditors.LookUpEdit();
            this.cboOrigemMovimentacao = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataHoraMovimento = new DevExpress.XtraEditors.DateEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.label12 = new System.Windows.Forms.Label();
            this.AbrirOfx = new System.Windows.Forms.OpenFileDialog();
            this.BuscarCaminhoOfx = new System.Windows.Forms.FolderBrowserDialog();
            this.txtDataFinalPeriodo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.pbPesquisa = new System.Windows.Forms.PictureBox();
            this.txtDataInicialPeriodo = new DevExpress.XtraEditors.DateEdit();
            this.pbPesquisaAvancada = new System.Windows.Forms.PictureBox();
            this.btnEscolherBancos = new DevExpress.XtraEditors.SimpleButton();
            this.btnLimparSelecao = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnStatusAberto = new System.Windows.Forms.Button();
            this.btnStatusFechado = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlInserirEstornarItemMovimentacao = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.txtValor = new DevExpress.XtraEditors.TextEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaldoFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaidas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEntradas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusBanco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuarioBanco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataBanco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoBanco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisarBanco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdBanco.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoDaMovimentacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTiposMovimentacoes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategoriaFinanceira.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboParceiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrigemMovimentacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataHoraMovimento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataHoraMovimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaAvancada)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.pnlInserirEstornarItemMovimentacao.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Controls.Add(this.btnSair);
            this.painelBotoes.Controls.Add(this.btnImprimirMovimentacao);
            this.painelBotoes.Location = new System.Drawing.Point(16, 2);
            this.painelBotoes.Size = new System.Drawing.Size(1000, 37);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.tableLayoutPanel6);
            this.panelConteudo.Controls.Add(this.tableLayoutPanel5);
            this.panelConteudo.Controls.Add(this.pnlInserirEstornarItemMovimentacao);
            this.panelConteudo.Controls.Add(this.tableLayoutPanel4);
            this.panelConteudo.Controls.Add(this.tableLayoutPanel3);
            this.panelConteudo.Controls.Add(this.tableLayoutPanel2);
            this.panelConteudo.Controls.Add(this.btnLimparSelecao);
            this.panelConteudo.Controls.Add(this.btnEscolherBancos);
            this.panelConteudo.Controls.Add(this.txtDataInicialPeriodo);
            this.panelConteudo.Controls.Add(this.pbPesquisaAvancada);
            this.panelConteudo.Controls.Add(this.pbPesquisa);
            this.panelConteudo.Controls.Add(this.txtDataFinalPeriodo);
            this.panelConteudo.Controls.Add(this.labelControl8);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.None;
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(4);
            this.panelConteudo.Size = new System.Drawing.Size(1006, 561);
            // 
            // colunaEstornado
            // 
            this.colunaEstornado.Caption = "Estornado";
            this.colunaEstornado.FieldName = "EstahEstornado";
            this.colunaEstornado.Name = "colunaEstornado";
            this.colunaEstornado.OptionsColumn.AllowEdit = false;
            this.colunaEstornado.OptionsColumn.AllowFocus = false;
            this.colunaEstornado.OptionsFilter.AllowFilter = false;
            // 
            // gcItens
            // 
            this.gcItens.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcItens.Location = new System.Drawing.Point(3, 3);
            this.gcItens.MainView = this.gridControl2;
            this.gcItens.Name = "gcItens";
            this.gcItens.Size = new System.Drawing.Size(994, 268);
            this.gcItens.TabIndex = 14;
            this.gcItens.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridControl2,
            this.gridView3});
            this.gcItens.DoubleClick += new System.EventHandler(this.gcItens_DoubleClick);
            this.gcItens.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcItens_KeyDown);
            // 
            // gridControl2
            // 
            this.gridControl2.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridControl2.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridControl2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridControl2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridControl2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunaEstornado,
            this.colunaDataHora,
            this.colunaParceiro,
            this.colunaDescricaoMovimentacao,
            this.colunaOrigemMovimentacao,
            this.colunaNumeroDocumento,
            this.colunaCategoria,
            this.colunaEntrada,
            this.colunaSaida,
            this.colunaSaldo});
            this.gridControl2.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.White;
            styleFormatCondition1.Appearance.Options.HighPriority = true;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.colunaEstornado;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = true;
            this.gridControl2.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gridControl2.GridControl = this.gcItens;
            this.gridControl2.GroupPanelText = "Enderecos";
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.OptionsView.ShowGroupPanel = false;
            this.gridControl2.OptionsView.ShowIndicator = false;
            this.gridControl2.OptionsView.ShowViewCaption = true;
            this.gridControl2.PaintStyleName = "Skin";
            this.gridControl2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridControl2.ViewCaption = "Movimentação de Banco";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Id";
            this.colunaId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaId.FieldName = "Id";
            this.colunaId.MinWidth = 45;
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "tele_id", "{0}")});
            this.colunaId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaId.Width = 64;
            // 
            // colunaDataHora
            // 
            this.colunaDataHora.AppearanceCell.Options.UseTextOptions = true;
            this.colunaDataHora.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDataHora.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaDataHora.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDataHora.Caption = "Dt. Movimentação";
            this.colunaDataHora.FieldName = "DataHora";
            this.colunaDataHora.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateMonth;
            this.colunaDataHora.Name = "colunaDataHora";
            this.colunaDataHora.OptionsColumn.AllowEdit = false;
            this.colunaDataHora.OptionsColumn.AllowFocus = false;
            this.colunaDataHora.OptionsFilter.AllowFilter = false;
            this.colunaDataHora.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.colunaDataHora.Visible = true;
            this.colunaDataHora.VisibleIndex = 0;
            this.colunaDataHora.Width = 102;
            // 
            // colunaParceiro
            // 
            this.colunaParceiro.AppearanceCell.Options.UseTextOptions = true;
            this.colunaParceiro.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaParceiro.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaParceiro.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaParceiro.Caption = "Parceiro";
            this.colunaParceiro.FieldName = "Parceiro";
            this.colunaParceiro.MinWidth = 10;
            this.colunaParceiro.Name = "colunaParceiro";
            this.colunaParceiro.OptionsColumn.AllowEdit = false;
            this.colunaParceiro.OptionsColumn.AllowFocus = false;
            this.colunaParceiro.OptionsFilter.AllowFilter = false;
            this.colunaParceiro.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.colunaParceiro.Visible = true;
            this.colunaParceiro.VisibleIndex = 1;
            this.colunaParceiro.Width = 134;
            // 
            // colunaDescricaoMovimentacao
            // 
            this.colunaDescricaoMovimentacao.AppearanceCell.Options.UseTextOptions = true;
            this.colunaDescricaoMovimentacao.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDescricaoMovimentacao.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaDescricaoMovimentacao.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDescricaoMovimentacao.Caption = "Descrição da Movimentação";
            this.colunaDescricaoMovimentacao.FieldName = "DescricaoMovimentacao";
            this.colunaDescricaoMovimentacao.Name = "colunaDescricaoMovimentacao";
            this.colunaDescricaoMovimentacao.OptionsColumn.AllowEdit = false;
            this.colunaDescricaoMovimentacao.OptionsColumn.AllowFocus = false;
            this.colunaDescricaoMovimentacao.OptionsFilter.AllowFilter = false;
            this.colunaDescricaoMovimentacao.Visible = true;
            this.colunaDescricaoMovimentacao.VisibleIndex = 3;
            this.colunaDescricaoMovimentacao.Width = 225;
            // 
            // colunaOrigemMovimentacao
            // 
            this.colunaOrigemMovimentacao.AppearanceCell.Options.UseTextOptions = true;
            this.colunaOrigemMovimentacao.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaOrigemMovimentacao.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaOrigemMovimentacao.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaOrigemMovimentacao.Caption = "Origem";
            this.colunaOrigemMovimentacao.FieldName = "OrigemMovimentacao";
            this.colunaOrigemMovimentacao.Name = "colunaOrigemMovimentacao";
            this.colunaOrigemMovimentacao.OptionsColumn.AllowEdit = false;
            this.colunaOrigemMovimentacao.OptionsColumn.AllowFocus = false;
            this.colunaOrigemMovimentacao.OptionsFilter.AllowFilter = false;
            this.colunaOrigemMovimentacao.Visible = true;
            this.colunaOrigemMovimentacao.VisibleIndex = 4;
            this.colunaOrigemMovimentacao.Width = 155;
            // 
            // colunaNumeroDocumento
            // 
            this.colunaNumeroDocumento.AppearanceCell.Options.UseTextOptions = true;
            this.colunaNumeroDocumento.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaNumeroDocumento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaNumeroDocumento.Caption = "N° Documento";
            this.colunaNumeroDocumento.FieldName = "NumeroDocumento";
            this.colunaNumeroDocumento.Name = "colunaNumeroDocumento";
            this.colunaNumeroDocumento.OptionsColumn.AllowEdit = false;
            this.colunaNumeroDocumento.OptionsColumn.AllowFocus = false;
            this.colunaNumeroDocumento.OptionsFilter.AllowFilter = false;
            this.colunaNumeroDocumento.Visible = true;
            this.colunaNumeroDocumento.VisibleIndex = 5;
            this.colunaNumeroDocumento.Width = 108;
            // 
            // colunaCategoria
            // 
            this.colunaCategoria.AppearanceCell.Options.UseTextOptions = true;
            this.colunaCategoria.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaCategoria.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaCategoria.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaCategoria.Caption = "Categoria";
            this.colunaCategoria.FieldName = "Categoria";
            this.colunaCategoria.Name = "colunaCategoria";
            this.colunaCategoria.OptionsColumn.AllowEdit = false;
            this.colunaCategoria.OptionsColumn.AllowFocus = false;
            this.colunaCategoria.OptionsFilter.AllowFilter = false;
            this.colunaCategoria.Visible = true;
            this.colunaCategoria.VisibleIndex = 2;
            this.colunaCategoria.Width = 181;
            // 
            // colunaEntrada
            // 
            this.colunaEntrada.AppearanceCell.Options.UseTextOptions = true;
            this.colunaEntrada.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaEntrada.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaEntrada.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaEntrada.Caption = "Entrada";
            this.colunaEntrada.FieldName = "Entrada";
            this.colunaEntrada.Name = "colunaEntrada";
            this.colunaEntrada.OptionsColumn.AllowEdit = false;
            this.colunaEntrada.OptionsColumn.AllowFocus = false;
            this.colunaEntrada.OptionsFilter.AllowFilter = false;
            this.colunaEntrada.Visible = true;
            this.colunaEntrada.VisibleIndex = 6;
            this.colunaEntrada.Width = 84;
            // 
            // colunaSaida
            // 
            this.colunaSaida.AppearanceCell.Options.UseTextOptions = true;
            this.colunaSaida.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaSaida.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaSaida.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaSaida.Caption = "Saída";
            this.colunaSaida.FieldName = "Saida";
            this.colunaSaida.Name = "colunaSaida";
            this.colunaSaida.OptionsColumn.AllowEdit = false;
            this.colunaSaida.OptionsColumn.AllowFocus = false;
            this.colunaSaida.OptionsFilter.AllowFilter = false;
            this.colunaSaida.Visible = true;
            this.colunaSaida.VisibleIndex = 7;
            this.colunaSaida.Width = 81;
            // 
            // colunaSaldo
            // 
            this.colunaSaldo.AppearanceCell.Options.UseTextOptions = true;
            this.colunaSaldo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaSaldo.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaSaldo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaSaldo.Caption = "Saldo";
            this.colunaSaldo.FieldName = "Saldo";
            this.colunaSaldo.Name = "colunaSaldo";
            this.colunaSaldo.OptionsColumn.AllowEdit = false;
            this.colunaSaldo.OptionsColumn.AllowFocus = false;
            this.colunaSaldo.OptionsColumn.AllowMove = false;
            this.colunaSaldo.OptionsFilter.AllowFilter = false;
            this.colunaSaldo.Width = 127;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gcItens;
            this.gridView3.Name = "gridView3";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Appearance.Options.UseForeColor = true;
            this.labelControl15.Location = new System.Drawing.Point(597, 3);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(82, 13);
            this.labelControl15.TabIndex = 10083;
            this.labelControl15.Text = "(=) Saldo Final";
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Appearance.Options.UseForeColor = true;
            this.labelControl14.Location = new System.Drawing.Point(300, 3);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(46, 13);
            this.labelControl14.TabIndex = 10082;
            this.labelControl14.Text = "(-) Saídas";
            // 
            // txtSaldoFinal
            // 
            this.txtSaldoFinal.EnterMoveNextControl = true;
            this.txtSaldoFinal.Location = new System.Drawing.Point(597, 22);
            this.txtSaldoFinal.Name = "txtSaldoFinal";
            this.txtSaldoFinal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoFinal.Properties.Appearance.Options.UseFont = true;
            this.txtSaldoFinal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSaldoFinal.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSaldoFinal.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSaldoFinal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtSaldoFinal.Properties.Mask.EditMask = "99/";
            this.txtSaldoFinal.Properties.MaxLength = 50;
            this.txtSaldoFinal.Properties.ReadOnly = true;
            this.txtSaldoFinal.Size = new System.Drawing.Size(291, 22);
            this.txtSaldoFinal.TabIndex = 10081;
            this.txtSaldoFinal.TabStop = false;
            // 
            // txtSaidas
            // 
            this.txtSaidas.EnterMoveNextControl = true;
            this.txtSaidas.Location = new System.Drawing.Point(300, 22);
            this.txtSaidas.Name = "txtSaidas";
            this.txtSaidas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaidas.Properties.Appearance.Options.UseFont = true;
            this.txtSaidas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSaidas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSaidas.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSaidas.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtSaidas.Properties.Mask.EditMask = "99/";
            this.txtSaidas.Properties.MaxLength = 50;
            this.txtSaidas.Properties.ReadOnly = true;
            this.txtSaidas.Size = new System.Drawing.Size(291, 22);
            this.txtSaidas.TabIndex = 10077;
            this.txtSaidas.TabStop = false;
            // 
            // txtEntradas
            // 
            this.txtEntradas.EnterMoveNextControl = true;
            this.txtEntradas.Location = new System.Drawing.Point(3, 22);
            this.txtEntradas.Name = "txtEntradas";
            this.txtEntradas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntradas.Properties.Appearance.Options.UseFont = true;
            this.txtEntradas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtEntradas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtEntradas.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEntradas.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtEntradas.Properties.Mask.EditMask = "99/";
            this.txtEntradas.Properties.MaxLength = 50;
            this.txtEntradas.Properties.ReadOnly = true;
            this.txtEntradas.Size = new System.Drawing.Size(291, 22);
            this.txtEntradas.TabIndex = 10067;
            this.txtEntradas.TabStop = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(3, 3);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(57, 13);
            this.labelControl6.TabIndex = 10065;
            this.labelControl6.Text = "(+) Entradas";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(3, 3);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(90, 13);
            this.labelControl4.TabIndex = 10076;
            this.labelControl4.Text = "Nr. Registro Banco";
            // 
            // txtId
            // 
            this.txtId.EnterMoveNextControl = true;
            this.txtId.Location = new System.Drawing.Point(3, 22);
            this.txtId.Name = "txtId";
            this.txtId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Properties.Appearance.Options.UseFont = true;
            this.txtId.Properties.Appearance.Options.UseTextOptions = true;
            this.txtId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtId.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtId.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtId.Properties.MaxLength = 10;
            this.txtId.Properties.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(88, 22);
            this.txtId.TabIndex = 10075;
            this.txtId.TabStop = false;
            // 
            // labelControl20
            // 
            this.labelControl20.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl20.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl20.Appearance.Options.UseFont = true;
            this.labelControl20.Appearance.Options.UseForeColor = true;
            this.labelControl20.Location = new System.Drawing.Point(823, 3);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(64, 13);
            this.labelControl20.TabIndex = 10074;
            this.labelControl20.Text = "Status Banco";
            // 
            // txtStatusBanco
            // 
            this.txtStatusBanco.EnterMoveNextControl = true;
            this.txtStatusBanco.Location = new System.Drawing.Point(823, 22);
            this.txtStatusBanco.Name = "txtStatusBanco";
            this.txtStatusBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatusBanco.Properties.Appearance.Options.UseFont = true;
            this.txtStatusBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtStatusBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtStatusBanco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStatusBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtStatusBanco.Properties.Mask.EditMask = "99/";
            this.txtStatusBanco.Properties.MaxLength = 50;
            this.txtStatusBanco.Properties.ReadOnly = true;
            this.txtStatusBanco.Size = new System.Drawing.Size(128, 22);
            this.txtStatusBanco.TabIndex = 5;
            this.txtStatusBanco.TabStop = false;
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl19.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl19.Appearance.Options.UseFont = true;
            this.labelControl19.Appearance.Options.UseForeColor = true;
            this.labelControl19.Location = new System.Drawing.Point(604, 3);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(63, 13);
            this.labelControl19.TabIndex = 10072;
            this.labelControl19.Text = "Usuário Atual";
            // 
            // txtUsuarioBanco
            // 
            this.txtUsuarioBanco.EnterMoveNextControl = true;
            this.txtUsuarioBanco.Location = new System.Drawing.Point(604, 22);
            this.txtUsuarioBanco.Name = "txtUsuarioBanco";
            this.txtUsuarioBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioBanco.Properties.Appearance.Options.UseFont = true;
            this.txtUsuarioBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtUsuarioBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtUsuarioBanco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuarioBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtUsuarioBanco.Properties.Mask.EditMask = "99/";
            this.txtUsuarioBanco.Properties.MaxLength = 50;
            this.txtUsuarioBanco.Properties.ReadOnly = true;
            this.txtUsuarioBanco.Size = new System.Drawing.Size(213, 22);
            this.txtUsuarioBanco.TabIndex = 4;
            this.txtUsuarioBanco.TabStop = false;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(475, 3);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(71, 13);
            this.labelControl16.TabIndex = 10070;
            this.labelControl16.Text = "Data do Status";
            // 
            // txtDataBanco
            // 
            this.txtDataBanco.EnterMoveNextControl = true;
            this.txtDataBanco.Location = new System.Drawing.Point(475, 22);
            this.txtDataBanco.Name = "txtDataBanco";
            this.txtDataBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataBanco.Properties.Appearance.Options.UseFont = true;
            this.txtDataBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDataBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDataBanco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDataBanco.Properties.Mask.EditMask = "99/";
            this.txtDataBanco.Properties.MaxLength = 50;
            this.txtDataBanco.Properties.ReadOnly = true;
            this.txtDataBanco.Size = new System.Drawing.Size(123, 22);
            this.txtDataBanco.TabIndex = 3;
            this.txtDataBanco.TabStop = false;
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl17.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl17.Appearance.Options.UseFont = true;
            this.labelControl17.Appearance.Options.UseForeColor = true;
            this.labelControl17.Location = new System.Drawing.Point(99, 3);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(46, 13);
            this.labelControl17.TabIndex = 10066;
            this.labelControl17.Text = "Id. Banco";
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl18.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Appearance.Options.UseForeColor = true;
            this.labelControl18.Location = new System.Drawing.Point(217, 3);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(97, 13);
            this.labelControl18.TabIndex = 10068;
            this.labelControl18.Text = "Descrição do Banco";
            // 
            // txtDescricaoBanco
            // 
            this.txtDescricaoBanco.EnterMoveNextControl = true;
            this.txtDescricaoBanco.Location = new System.Drawing.Point(217, 22);
            this.txtDescricaoBanco.Name = "txtDescricaoBanco";
            this.txtDescricaoBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoBanco.Properties.Appearance.Options.UseFont = true;
            this.txtDescricaoBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricaoBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricaoBanco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricaoBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricaoBanco.Properties.Mask.EditMask = "99/";
            this.txtDescricaoBanco.Properties.ReadOnly = true;
            this.txtDescricaoBanco.Size = new System.Drawing.Size(252, 22);
            this.txtDescricaoBanco.TabIndex = 2;
            this.txtDescricaoBanco.TabStop = false;
            // 
            // btnPesquisarBanco
            // 
            this.btnPesquisarBanco.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisarBanco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisarBanco.Image = global::Programax.Easy.View.Properties.Resources.icone_pesquisar_branco;
            this.btnPesquisarBanco.Location = new System.Drawing.Point(189, 22);
            this.btnPesquisarBanco.Name = "btnPesquisarBanco";
            this.btnPesquisarBanco.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisarBanco.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisarBanco.TabIndex = 10067;
            this.btnPesquisarBanco.TabStop = false;
            this.btnPesquisarBanco.Click += new System.EventHandler(this.btnPesquisarBanco_Click);
            // 
            // txtIdBanco
            // 
            this.txtIdBanco.EnterMoveNextControl = true;
            this.txtIdBanco.Location = new System.Drawing.Point(99, 22);
            this.txtIdBanco.Name = "txtIdBanco";
            this.txtIdBanco.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdBanco.Properties.Appearance.Options.UseFont = true;
            this.txtIdBanco.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIdBanco.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIdBanco.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtIdBanco.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtIdBanco.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIdBanco.Properties.MaxLength = 10;
            this.txtIdBanco.Size = new System.Drawing.Size(84, 22);
            this.txtIdBanco.TabIndex = 1;
            this.txtIdBanco.Leave += new System.EventHandler(this.txtIdBanco_Leave);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnFecharBanco);
            this.flowLayoutPanel1.Controls.Add(this.btnImportar);
            this.flowLayoutPanel1.Controls.Add(this.btnPagarReceber);
            this.flowLayoutPanel1.Controls.Add(this.btnDadosCruzados);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(679, 34);
            this.flowLayoutPanel1.TabIndex = 1041;
            // 
            // btnFecharBanco
            // 
            this.btnFecharBanco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFecharBanco.FlatAppearance.BorderSize = 0;
            this.btnFecharBanco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFecharBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFecharBanco.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_safebox_3339041;
            this.btnFecharBanco.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFecharBanco.Location = new System.Drawing.Point(0, 0);
            this.btnFecharBanco.Margin = new System.Windows.Forms.Padding(0);
            this.btnFecharBanco.Name = "btnFecharBanco";
            this.btnFecharBanco.Size = new System.Drawing.Size(130, 40);
            this.btnFecharBanco.TabIndex = 16;
            this.btnFecharBanco.Text = " Fechar Banco";
            this.btnFecharBanco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFecharBanco.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFecharBanco.UseVisualStyleBackColor = true;
            this.btnFecharBanco.Visible = false;
            this.btnFecharBanco.Click += new System.EventHandler(this.btnFecharBanco_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.FlatAppearance.BorderSize = 0;
            this.btnImportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Image = global::Programax.Easy.View.Properties.Resources.icone_importar;
            this.btnImportar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImportar.Location = new System.Drawing.Point(130, 0);
            this.btnImportar.Margin = new System.Windows.Forms.Padding(0);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(100, 40);
            this.btnImportar.TabIndex = 18;
            this.btnImportar.Text = "Importar";
            this.btnImportar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnPagarReceber
            // 
            this.btnPagarReceber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPagarReceber.FlatAppearance.BorderSize = 0;
            this.btnPagarReceber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagarReceber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagarReceber.Image = global::Programax.Easy.View.Properties.Resources.icone_tres_pontos1;
            this.btnPagarReceber.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPagarReceber.Location = new System.Drawing.Point(230, 0);
            this.btnPagarReceber.Margin = new System.Windows.Forms.Padding(0);
            this.btnPagarReceber.Name = "btnPagarReceber";
            this.btnPagarReceber.Size = new System.Drawing.Size(138, 40);
            this.btnPagarReceber.TabIndex = 20;
            this.btnPagarReceber.Text = "Pagar/Receber";
            this.btnPagarReceber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPagarReceber.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPagarReceber.UseVisualStyleBackColor = true;
            this.btnPagarReceber.Click += new System.EventHandler(this.btnPagarReceber_Click);
            // 
            // btnDadosCruzados
            // 
            this.btnDadosCruzados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDadosCruzados.FlatAppearance.BorderSize = 0;
            this.btnDadosCruzados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDadosCruzados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDadosCruzados.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnDadosCruzados.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDadosCruzados.Location = new System.Drawing.Point(368, 0);
            this.btnDadosCruzados.Margin = new System.Windows.Forms.Padding(0);
            this.btnDadosCruzados.Name = "btnDadosCruzados";
            this.btnDadosCruzados.Size = new System.Drawing.Size(120, 40);
            this.btnDadosCruzados.TabIndex = 21;
            this.btnDadosCruzados.Text = "Importações Associadas";
            this.btnDadosCruzados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDadosCruzados.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDadosCruzados.UseVisualStyleBackColor = true;
            this.btnDadosCruzados.Click += new System.EventHandler(this.btnDadosCruzados_Click);
            // 
            // btnAbrirBanco
            // 
            this.btnAbrirBanco.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirBanco.FlatAppearance.BorderSize = 0;
            this.btnAbrirBanco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirBanco.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_strongbox_3339047;
            this.btnAbrirBanco.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAbrirBanco.Location = new System.Drawing.Point(0, 0);
            this.btnAbrirBanco.Margin = new System.Windows.Forms.Padding(0);
            this.btnAbrirBanco.Name = "btnAbrirBanco";
            this.btnAbrirBanco.Size = new System.Drawing.Size(160, 49);
            this.btnAbrirBanco.TabIndex = 15;
            this.btnAbrirBanco.Text = " Abrir Banco";
            this.btnAbrirBanco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbrirBanco.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAbrirBanco.UseVisualStyleBackColor = true;
            this.btnAbrirBanco.Click += new System.EventHandler(this.btnAbrirBanco_Click);
            // 
            // btnImprimirMovimentacao
            // 
            this.btnImprimirMovimentacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimirMovimentacao.FlatAppearance.BorderSize = 0;
            this.btnImprimirMovimentacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirMovimentacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirMovimentacao.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnImprimirMovimentacao.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImprimirMovimentacao.Location = new System.Drawing.Point(726, 0);
            this.btnImprimirMovimentacao.Margin = new System.Windows.Forms.Padding(0);
            this.btnImprimirMovimentacao.Name = "btnImprimirMovimentacao";
            this.btnImprimirMovimentacao.Size = new System.Drawing.Size(100, 40);
            this.btnImprimirMovimentacao.TabIndex = 17;
            this.btnImprimirMovimentacao.Text = " Imprimir";
            this.btnImprimirMovimentacao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimirMovimentacao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimirMovimentacao.UseVisualStyleBackColor = true;
            this.btnImprimirMovimentacao.Click += new System.EventHandler(this.btnImprimirMovimentacao_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(851, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(79, 40);
            this.btnSair.TabIndex = 19;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(468, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(97, 13);
            this.labelControl1.TabIndex = 10065;
            this.labelControl1.Text = "Categoria Financeira";
            // 
            // lblDescricaoMovimentacao
            // 
            this.lblDescricaoMovimentacao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricaoMovimentacao.Appearance.Options.UseFont = true;
            this.lblDescricaoMovimentacao.Location = new System.Drawing.Point(600, 4);
            this.lblDescricaoMovimentacao.Name = "lblDescricaoMovimentacao";
            this.lblDescricaoMovimentacao.Size = new System.Drawing.Size(136, 13);
            this.lblDescricaoMovimentacao.TabIndex = 10063;
            this.lblDescricaoMovimentacao.Text = "Descrição da Movimentação";
            // 
            // txtDescricaoDaMovimentacao
            // 
            this.txtDescricaoDaMovimentacao.EnterMoveNextControl = true;
            this.txtDescricaoDaMovimentacao.Location = new System.Drawing.Point(600, 24);
            this.txtDescricaoDaMovimentacao.Name = "txtDescricaoDaMovimentacao";
            this.txtDescricaoDaMovimentacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoDaMovimentacao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricaoDaMovimentacao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricaoDaMovimentacao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricaoDaMovimentacao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricaoDaMovimentacao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricaoDaMovimentacao.Properties.Mask.EditMask = "99/";
            this.txtDescricaoDaMovimentacao.Properties.MaxLength = 90;
            this.txtDescricaoDaMovimentacao.Size = new System.Drawing.Size(382, 22);
            this.txtDescricaoDaMovimentacao.TabIndex = 4;
            // 
            // labComplementoEndereco
            // 
            this.labComplementoEndereco.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labComplementoEndereco.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labComplementoEndereco.Appearance.Options.UseFont = true;
            this.labComplementoEndereco.Appearance.Options.UseForeColor = true;
            this.labComplementoEndereco.Location = new System.Drawing.Point(771, 3);
            this.labComplementoEndereco.Name = "labComplementoEndereco";
            this.labComplementoEndereco.Size = new System.Drawing.Size(24, 13);
            this.labComplementoEndereco.TabIndex = 10067;
            this.labComplementoEndereco.Text = "Valor";
            // 
            // labBanco
            // 
            this.labBanco.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBanco.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labBanco.Appearance.Options.UseFont = true;
            this.labBanco.Appearance.Options.UseForeColor = true;
            this.labBanco.Location = new System.Drawing.Point(436, 4);
            this.labBanco.Name = "labBanco";
            this.labBanco.Size = new System.Drawing.Size(109, 13);
            this.labBanco.TabIndex = 10061;
            this.labBanco.Text = "Tipo de Movimentação";
            // 
            // btnInserirAtualizarItem
            // 
            this.btnInserirAtualizarItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInserirAtualizarItem.FlatAppearance.BorderSize = 0;
            this.btnInserirAtualizarItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnInserirAtualizarItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnInserirAtualizarItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInserirAtualizarItem.Image = global::Programax.Easy.View.Properties.Resources.icones2_19;
            this.btnInserirAtualizarItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInserirAtualizarItem.Location = new System.Drawing.Point(897, 22);
            this.btnInserirAtualizarItem.Name = "btnInserirAtualizarItem";
            this.btnInserirAtualizarItem.Size = new System.Drawing.Size(33, 21);
            this.btnInserirAtualizarItem.TabIndex = 11;
            this.btnInserirAtualizarItem.TabStop = false;
            this.btnInserirAtualizarItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInserirAtualizarItem.UseVisualStyleBackColor = true;
            this.btnInserirAtualizarItem.Click += new System.EventHandler(this.btnInserirAtualizarItem_Click);
            // 
            // btnCancelarItem
            // 
            this.btnCancelarItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarItem.FlatAppearance.BorderSize = 0;
            this.btnCancelarItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarItem.Image = global::Programax.Easy.View.Properties.Resources.icones2_24;
            this.btnCancelarItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelarItem.Location = new System.Drawing.Point(936, 22);
            this.btnCancelarItem.Name = "btnCancelarItem";
            this.btnCancelarItem.Size = new System.Drawing.Size(26, 21);
            this.btnCancelarItem.TabIndex = 12;
            this.btnCancelarItem.TabStop = false;
            this.btnCancelarItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelarItem.UseVisualStyleBackColor = true;
            this.btnCancelarItem.Click += new System.EventHandler(this.btnCancelarItem_Click);
            // 
            // btnEstornarItem
            // 
            this.btnEstornarItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstornarItem.FlatAppearance.BorderSize = 0;
            this.btnEstornarItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEstornarItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEstornarItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstornarItem.Image = global::Programax.Easy.View.Properties.Resources.icones2_18;
            this.btnEstornarItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEstornarItem.Location = new System.Drawing.Point(968, 22);
            this.btnEstornarItem.Name = "btnEstornarItem";
            this.btnEstornarItem.Size = new System.Drawing.Size(28, 21);
            this.btnEstornarItem.TabIndex = 13;
            this.btnEstornarItem.TabStop = false;
            this.btnEstornarItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstornarItem.UseVisualStyleBackColor = true;
            this.btnEstornarItem.Click += new System.EventHandler(this.btnEstornarItem_Click);
            // 
            // cboTiposMovimentacoes
            // 
            this.cboTiposMovimentacoes.EnterMoveNextControl = true;
            this.cboTiposMovimentacoes.Location = new System.Drawing.Point(436, 24);
            this.cboTiposMovimentacoes.Name = "cboTiposMovimentacoes";
            this.cboTiposMovimentacoes.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTiposMovimentacoes.Properties.Appearance.Options.UseFont = true;
            this.cboTiposMovimentacoes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTiposMovimentacoes.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboTiposMovimentacoes.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboTiposMovimentacoes.Properties.DropDownRows = 3;
            this.cboTiposMovimentacoes.Properties.NullText = "";
            this.cboTiposMovimentacoes.Size = new System.Drawing.Size(157, 22);
            this.cboTiposMovimentacoes.TabIndex = 3;
            this.cboTiposMovimentacoes.EditValueChanged += new System.EventHandler(this.cboTiposMovimentacoes_EditValueChanged);
            // 
            // btnAdicionarCategoria
            // 
            this.btnAdicionarCategoria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarCategoria.FlatAppearance.BorderSize = 0;
            this.btnAdicionarCategoria.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarCategoria.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarCategoria.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_199_CircledPlus_183316;
            this.btnAdicionarCategoria.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionarCategoria.Location = new System.Drawing.Point(741, 22);
            this.btnAdicionarCategoria.Name = "btnAdicionarCategoria";
            this.btnAdicionarCategoria.Size = new System.Drawing.Size(24, 21);
            this.btnAdicionarCategoria.TabIndex = 9;
            this.btnAdicionarCategoria.TabStop = false;
            this.btnAdicionarCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarCategoria.UseVisualStyleBackColor = true;
            this.btnAdicionarCategoria.Click += new System.EventHandler(this.btnAdicionarCategoria_Click);
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.Location = new System.Drawing.Point(3, 22);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroDocumento.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroDocumento.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroDocumento.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroDocumento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroDocumento.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNumeroDocumento.Properties.Mask.EditMask = "[0-9]{1,11}([\\.\\,][0-9]{0,2})?";
            this.txtNumeroDocumento.Properties.MaxLength = 11;
            this.txtNumeroDocumento.Size = new System.Drawing.Size(107, 22);
            this.txtNumeroDocumento.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(3, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(110, 13);
            this.labelControl3.TabIndex = 10077;
            this.labelControl3.Text = "Número do Documento";
            // 
            // cboCategoriaFinanceira
            // 
            this.cboCategoriaFinanceira.EnterMoveNextControl = true;
            this.cboCategoriaFinanceira.Location = new System.Drawing.Point(468, 22);
            this.cboCategoriaFinanceira.Name = "cboCategoriaFinanceira";
            this.cboCategoriaFinanceira.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategoriaFinanceira.Properties.Appearance.Options.UseFont = true;
            this.cboCategoriaFinanceira.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCategoriaFinanceira.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboCategoriaFinanceira.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "ID", 6, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCategoriaFinanceira.Properties.NullText = "";
            this.cboCategoriaFinanceira.Size = new System.Drawing.Size(267, 22);
            this.cboCategoriaFinanceira.TabIndex = 8;
            // 
            // btnAdicionarParceiro
            // 
            this.btnAdicionarParceiro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarParceiro.FlatAppearance.BorderSize = 0;
            this.btnAdicionarParceiro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarParceiro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarParceiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarParceiro.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_199_CircledPlus_183316;
            this.btnAdicionarParceiro.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdicionarParceiro.Location = new System.Drawing.Point(436, 22);
            this.btnAdicionarParceiro.Name = "btnAdicionarParceiro";
            this.btnAdicionarParceiro.Size = new System.Drawing.Size(26, 21);
            this.btnAdicionarParceiro.TabIndex = 7;
            this.btnAdicionarParceiro.TabStop = false;
            this.btnAdicionarParceiro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarParceiro.UseVisualStyleBackColor = true;
            this.btnAdicionarParceiro.Click += new System.EventHandler(this.btnAdicionarParceiro_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(119, 3);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(39, 13);
            this.labelControl5.TabIndex = 10081;
            this.labelControl5.Text = "Parceiro";
            // 
            // cboParceiro
            // 
            this.cboParceiro.Location = new System.Drawing.Point(119, 22);
            this.cboParceiro.Name = "cboParceiro";
            this.cboParceiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboParceiro.Properties.Appearance.Options.UseFont = true;
            this.cboParceiro.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboParceiro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboParceiro.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Valor", "Id", 8, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Razão Social", 40, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cboParceiro.Properties.NullText = "";
            this.cboParceiro.Properties.PopupSizeable = false;
            this.cboParceiro.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboParceiro.Size = new System.Drawing.Size(311, 22);
            this.cboParceiro.TabIndex = 6;
            this.cboParceiro.EditValueChanged += new System.EventHandler(this.cboParceiro_EditValueChanged);
            this.cboParceiro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboParceiro_KeyDown);
            // 
            // cboOrigemMovimentacao
            // 
            this.cboOrigemMovimentacao.EnterMoveNextControl = true;
            this.cboOrigemMovimentacao.Location = new System.Drawing.Point(136, 24);
            this.cboOrigemMovimentacao.Name = "cboOrigemMovimentacao";
            this.cboOrigemMovimentacao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboOrigemMovimentacao.Properties.Appearance.Options.UseFont = true;
            this.cboOrigemMovimentacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboOrigemMovimentacao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboOrigemMovimentacao.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboOrigemMovimentacao.Properties.DropDownRows = 3;
            this.cboOrigemMovimentacao.Properties.NullText = "";
            this.cboOrigemMovimentacao.Properties.ReadOnly = true;
            this.cboOrigemMovimentacao.Size = new System.Drawing.Size(293, 22);
            this.cboOrigemMovimentacao.TabIndex = 2;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Appearance.Options.UseForeColor = true;
            this.labelControl7.Location = new System.Drawing.Point(136, 4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(147, 13);
            this.labelControl7.TabIndex = 10085;
            this.labelControl7.Text = "Origem / Status Movimentação";
            // 
            // txtDataHoraMovimento
            // 
            this.txtDataHoraMovimento.EditValue = new System.DateTime(2014, 12, 5, 8, 26, 24, 0);
            this.txtDataHoraMovimento.EnterMoveNextControl = true;
            this.txtDataHoraMovimento.Location = new System.Drawing.Point(4, 24);
            this.txtDataHoraMovimento.Name = "txtDataHoraMovimento";
            this.txtDataHoraMovimento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataHoraMovimento.Properties.Appearance.Options.UseFont = true;
            this.txtDataHoraMovimento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataHoraMovimento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataHoraMovimento.Size = new System.Drawing.Size(125, 22);
            this.txtDataHoraMovimento.TabIndex = 1;
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Appearance.Options.UseFont = true;
            this.labDataCadastro.Location = new System.Drawing.Point(4, 4);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(96, 13);
            this.labDataCadastro.TabIndex = 10083;
            this.labDataCadastro.Text = "Data Movimentação";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(946, 20);
            this.label12.TabIndex = 10075;
            this.label12.Text = "F1 Sair | F2 Pesquisar  |  F3 Pesquisar Todos  |  F4 Limpar  |  F5 Abrir /Fechar " +
    "Banco  |  F6 Imprimir  |  F7 Importar | F8 Pagar/Receber | F9 Associações | <Ent" +
    "er> Incluir ou Atualizar";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDataFinalPeriodo
            // 
            this.txtDataFinalPeriodo.EditValue = new System.DateTime(2014, 12, 5, 8, 26, 24, 0);
            this.txtDataFinalPeriodo.EnterMoveNextControl = true;
            this.txtDataFinalPeriodo.Location = new System.Drawing.Point(395, 165);
            this.txtDataFinalPeriodo.Name = "txtDataFinalPeriodo";
            this.txtDataFinalPeriodo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinalPeriodo.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinalPeriodo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinalPeriodo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinalPeriodo.Size = new System.Drawing.Size(117, 22);
            this.txtDataFinalPeriodo.TabIndex = 10084;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(7, 171);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(249, 13);
            this.labelControl8.TabIndex = 10085;
            this.labelControl8.Text = "Informe o Período da Movimentação para Pesquisar:";
            // 
            // pbPesquisa
            // 
            this.pbPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisa.Location = new System.Drawing.Point(663, 166);
            this.pbPesquisa.Name = "pbPesquisa";
            this.pbPesquisa.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisa.TabIndex = 10086;
            this.pbPesquisa.TabStop = false;
            this.pbPesquisa.Click += new System.EventHandler(this.pbPesquisa_Click);
            // 
            // txtDataInicialPeriodo
            // 
            this.txtDataInicialPeriodo.EditValue = new System.DateTime(2014, 12, 5, 8, 26, 24, 0);
            this.txtDataInicialPeriodo.EnterMoveNextControl = true;
            this.txtDataInicialPeriodo.Location = new System.Drawing.Point(272, 165);
            this.txtDataInicialPeriodo.Name = "txtDataInicialPeriodo";
            this.txtDataInicialPeriodo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicialPeriodo.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicialPeriodo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicialPeriodo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicialPeriodo.Size = new System.Drawing.Size(117, 22);
            this.txtDataInicialPeriodo.TabIndex = 10088;
            // 
            // pbPesquisaAvancada
            // 
            this.pbPesquisaAvancada.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaAvancada.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaAvancada.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_advanced_search_49813;
            this.pbPesquisaAvancada.Location = new System.Drawing.Point(690, 166);
            this.pbPesquisaAvancada.Name = "pbPesquisaAvancada";
            this.pbPesquisaAvancada.Size = new System.Drawing.Size(24, 24);
            this.pbPesquisaAvancada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaAvancada.TabIndex = 10087;
            this.pbPesquisaAvancada.TabStop = false;
            this.pbPesquisaAvancada.Click += new System.EventHandler(this.pbPesquisaAvancada_Click);
            // 
            // btnEscolherBancos
            // 
            this.btnEscolherBancos.Location = new System.Drawing.Point(524, 165);
            this.btnEscolherBancos.Name = "btnEscolherBancos";
            this.btnEscolherBancos.Size = new System.Drawing.Size(106, 23);
            this.btnEscolherBancos.TabIndex = 10090;
            this.btnEscolherBancos.Text = "Escolher Bancos ...";
            this.btnEscolherBancos.Click += new System.EventHandler(this.btnEscolherBancos_Click);
            // 
            // btnLimparSelecao
            // 
            this.btnLimparSelecao.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimparSelecao.Appearance.Options.UseFont = true;
            this.btnLimparSelecao.Enabled = false;
            this.btnLimparSelecao.Location = new System.Drawing.Point(631, 166);
            this.btnLimparSelecao.Name = "btnLimparSelecao";
            this.btnLimparSelecao.Size = new System.Drawing.Size(19, 21);
            this.btnLimparSelecao.TabIndex = 10091;
            this.btnLimparSelecao.Text = "x";
            this.btnLimparSelecao.Click += new System.EventHandler(this.btnLimparSelecao_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.labelControl4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtId, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtIdBanco, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtStatusBanco, 6, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl20, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnPesquisarBanco, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl18, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtUsuarioBanco, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl19, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtDescricaoBanco, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl17, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtDataBanco, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl16, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel9, 7, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1000, 54);
            this.tableLayoutPanel2.TabIndex = 10092;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.panel9.Controls.Add(this.btnStatusAberto);
            this.panel9.Controls.Add(this.btnStatusFechado);
            this.panel9.Controls.Add(this.pictureBox1);
            this.panel9.Location = new System.Drawing.Point(957, 22);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(41, 31);
            this.panel9.TabIndex = 10073;
            // 
            // btnStatusAberto
            // 
            this.btnStatusAberto.BackColor = System.Drawing.Color.White;
            this.btnStatusAberto.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnStatusAberto.FlatAppearance.BorderSize = 0;
            this.btnStatusAberto.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnStatusAberto.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_strongbox_3339047;
            this.btnStatusAberto.Location = new System.Drawing.Point(5, 1);
            this.btnStatusAberto.Name = "btnStatusAberto";
            this.btnStatusAberto.Size = new System.Drawing.Size(31, 27);
            this.btnStatusAberto.TabIndex = 10078;
            this.btnStatusAberto.TabStop = false;
            this.btnStatusAberto.UseVisualStyleBackColor = false;
            this.btnStatusAberto.Visible = false;
            this.btnStatusAberto.Click += new System.EventHandler(this.btnStatusAberto_Click);
            // 
            // btnStatusFechado
            // 
            this.btnStatusFechado.BackColor = System.Drawing.Color.White;
            this.btnStatusFechado.FlatAppearance.BorderSize = 0;
            this.btnStatusFechado.ForeColor = System.Drawing.Color.White;
            this.btnStatusFechado.Image = global::Programax.Easy.View.Properties.Resources.iconfinder_safebox_3339041;
            this.btnStatusFechado.Location = new System.Drawing.Point(2, 2);
            this.btnStatusFechado.Name = "btnStatusFechado";
            this.btnStatusFechado.Size = new System.Drawing.Size(31, 26);
            this.btnStatusFechado.TabIndex = 10077;
            this.btnStatusFechado.UseVisualStyleBackColor = false;
            this.btnStatusFechado.Visible = false;
            this.btnStatusFechado.Click += new System.EventHandler(this.btnStatusFechado_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-16, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10071;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.cboOrigemMovimentacao, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.labDataCadastro, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelControl7, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtDataHoraMovimento, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.labBanco, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.cboTiposMovimentacoes, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblDescricaoMovimentacao, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtDescricaoDaMovimentacao, 3, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 60);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(997, 49);
            this.tableLayoutPanel3.TabIndex = 10093;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 9;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.txtValor, 5, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnEstornarItem, 8, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnAdicionarCategoria, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnCancelarItem, 7, 1);
            this.tableLayoutPanel4.Controls.Add(this.cboCategoriaFinanceira, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnInserirAtualizarItem, 6, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelControl3, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.labComplementoEndereco, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnAdicionarParceiro, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.txtNumeroDocumento, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelControl5, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.cboParceiro, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelControl1, 3, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2, 112);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(998, 47);
            this.tableLayoutPanel4.TabIndex = 10094;
            // 
            // pnlInserirEstornarItemMovimentacao
            // 
            this.pnlInserirEstornarItemMovimentacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.pnlInserirEstornarItemMovimentacao.ColumnCount = 3;
            this.pnlInserirEstornarItemMovimentacao.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlInserirEstornarItemMovimentacao.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlInserirEstornarItemMovimentacao.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlInserirEstornarItemMovimentacao.Controls.Add(this.labelControl6, 0, 0);
            this.pnlInserirEstornarItemMovimentacao.Controls.Add(this.txtEntradas, 0, 1);
            this.pnlInserirEstornarItemMovimentacao.Controls.Add(this.labelControl14, 1, 0);
            this.pnlInserirEstornarItemMovimentacao.Controls.Add(this.txtSaldoFinal, 2, 1);
            this.pnlInserirEstornarItemMovimentacao.Controls.Add(this.labelControl15, 2, 0);
            this.pnlInserirEstornarItemMovimentacao.Controls.Add(this.txtSaidas, 1, 1);
            this.pnlInserirEstornarItemMovimentacao.Location = new System.Drawing.Point(55, 472);
            this.pnlInserirEstornarItemMovimentacao.Margin = new System.Windows.Forms.Padding(2);
            this.pnlInserirEstornarItemMovimentacao.Name = "pnlInserirEstornarItemMovimentacao";
            this.pnlInserirEstornarItemMovimentacao.RowCount = 2;
            this.pnlInserirEstornarItemMovimentacao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlInserirEstornarItemMovimentacao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlInserirEstornarItemMovimentacao.Size = new System.Drawing.Size(893, 51);
            this.pnlInserirEstornarItemMovimentacao.TabIndex = 10095;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(26, 529);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(951, 24);
            this.tableLayoutPanel5.TabIndex = 10096;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.gcItens, 0, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(2, 193);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(998, 275);
            this.tableLayoutPanel6.TabIndex = 10097;
            // 
            // txtValor
            // 
            this.txtValor.EnterMoveNextControl = true;
            this.txtValor.Location = new System.Drawing.Point(771, 22);
            this.txtValor.Name = "txtValor";
            this.txtValor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.Properties.Appearance.Options.UseFont = true;
            this.txtValor.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtValor.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtValor.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValor.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtValor.Properties.Mask.EditMask = "99/";
            this.txtValor.Properties.MaxLength = 90;
            this.txtValor.Size = new System.Drawing.Size(120, 22);
            this.txtValor.TabIndex = 10;
            // 
            // FormCadastroMovimentacoesBanco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 661);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormCadastroMovimentacoesBanco";
            this.NomeDaTela = "Movimentação Banco";
            this.Text = "Movimentação Banco";
            this.Load += new System.EventHandler(this.FormCadastroMovimentacoesBanco_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormCadastroMovimentacoesBanco_KeyDown);
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaldoFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaidas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEntradas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusBanco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuarioBanco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataBanco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoBanco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisarBanco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdBanco.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoDaMovimentacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTiposMovimentacoes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategoriaFinanceira.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboParceiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOrigemMovimentacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataHoraMovimento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataHoraMovimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalPeriodo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialPeriodo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaAvancada)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.pnlInserirEstornarItemMovimentacao.ResumeLayout(false);
            this.pnlInserirEstornarItemMovimentacao.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gcItens;
        private DevExpress.XtraGrid.Views.Grid.GridView gridControl2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataHora;
        private DevExpress.XtraGrid.Columns.GridColumn colunaParceiro;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricaoMovimentacao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCategoria;
        private DevExpress.XtraGrid.Columns.GridColumn colunaEntrada;
        private DevExpress.XtraGrid.Columns.GridColumn colunaSaida;
        private DevExpress.XtraGrid.Columns.GridColumn colunaSaldo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.TextEdit txtSaldoFinal;
        private DevExpress.XtraEditors.TextEdit txtSaidas;
        private DevExpress.XtraEditors.TextEdit txtEntradas;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraGrid.Columns.GridColumn colunaEstornado;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.TextEdit txtStatusBanco;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.TextEdit txtUsuarioBanco;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.TextEdit txtDataBanco;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.TextEdit txtDescricaoBanco;
        private System.Windows.Forms.PictureBox btnPesquisarBanco;
        private DevExpress.XtraEditors.TextEdit txtIdBanco;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAbrirBanco;
        private System.Windows.Forms.Button btnFecharBanco;
        private System.Windows.Forms.Button btnImprimirMovimentacao;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaOrigemMovimentacao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroDocumento;
        private System.Windows.Forms.Button btnAdicionarParceiro;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtNumeroDocumento;
        private System.Windows.Forms.Button btnAdicionarCategoria;
        private DevExpress.XtraEditors.LookUpEdit cboTiposMovimentacoes;
        private System.Windows.Forms.Button btnEstornarItem;
        private System.Windows.Forms.Button btnCancelarItem;
        private System.Windows.Forms.Button btnInserirAtualizarItem;
        private DevExpress.XtraEditors.LabelControl labBanco;
        private DevExpress.XtraEditors.LabelControl labComplementoEndereco;
        private DevExpress.XtraEditors.TextEdit txtDescricaoDaMovimentacao;
        private DevExpress.XtraEditors.LabelControl lblDescricaoMovimentacao;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cboCategoriaFinanceira;
        private DevExpress.XtraEditors.LookUpEdit cboParceiro;
        private DevExpress.XtraEditors.DateEdit txtDataHoraMovimento;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboOrigemMovimentacao;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private Label label12;
        private OpenFileDialog AbrirOfx;
        private FolderBrowserDialog BuscarCaminhoOfx;
        private Button btnPagarReceber;
        private Button btnDadosCruzados;
        private DevExpress.XtraEditors.DateEdit txtDataInicialPeriodo;
        private PictureBox pbPesquisa;
        private DevExpress.XtraEditors.DateEdit txtDataFinalPeriodo;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private PictureBox pbPesquisaAvancada;
        private DevExpress.XtraEditors.SimpleButton btnEscolherBancos;
        private DevExpress.XtraEditors.SimpleButton btnLimparSelecao;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel9;
        private Button btnStatusFechado;
        private PictureBox pictureBox1;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnStatusAberto;
        private TableLayoutPanel pnlInserirEstornarItemMovimentacao;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel5;
        private DevExpress.XtraEditors.TextEdit txtValor;
    }
}