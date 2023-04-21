namespace Programax.Easy.View.Telas.TeleMarketing
{
    partial class FormBuscaAtendimentos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBuscaAtendimentos));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAtender = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnHistorico = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.gcAtendimentos = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CodigoCliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNovo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataCompra = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataAgedamento = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Cor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.cboStatusAtendimento = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDataFinal = new DevExpress.XtraEditors.DateEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicial = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl67 = new DevExpress.XtraEditors.LabelControl();
            this.txtIdCliente = new DevExpress.XtraEditors.TextEdit();
            this.btnPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCpfCnpj = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNomeCliente = new DevExpress.XtraEditors.TextEdit();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.cboPeriodoPreDeterminado = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cboMarcas = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.btnPesquisa = new System.Windows.Forms.PictureBox();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtQtdePedidos = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.cboCarteiras = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cboVendedores = new DevExpress.XtraEditors.LookUpEdit();
            this.btnpesquisacarteiras = new System.Windows.Forms.PictureBox();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAtendimentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusAtendimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPessoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCpfCnpj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPeriodoPreDeterminado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMarcas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQtdePedidos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCarteiras.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVendedores.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnpesquisacarteiras)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel2);
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(888, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.btnpesquisacarteiras);
            this.panelConteudo.Controls.Add(this.cboVendedores);
            this.panelConteudo.Controls.Add(this.labelControl6);
            this.panelConteudo.Controls.Add(this.labelControl12);
            this.panelConteudo.Controls.Add(this.cboCarteiras);
            this.panelConteudo.Controls.Add(this.labelControl5);
            this.panelConteudo.Controls.Add(this.txtQtdePedidos);
            this.panelConteudo.Controls.Add(this.labCodigo);
            this.panelConteudo.Controls.Add(this.txtId);
            this.panelConteudo.Controls.Add(this.btnPesquisa);
            this.panelConteudo.Controls.Add(this.cboMarcas);
            this.panelConteudo.Controls.Add(this.labelControl8);
            this.panelConteudo.Controls.Add(this.cboPeriodoPreDeterminado);
            this.panelConteudo.Controls.Add(this.labelControl4);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.txtCpfCnpj);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.txtNomeCliente);
            this.panelConteudo.Controls.Add(this.btnPesquisaPessoa);
            this.panelConteudo.Controls.Add(this.labelControl67);
            this.panelConteudo.Controls.Add(this.txtIdCliente);
            this.panelConteudo.Controls.Add(this.txtDataFinal);
            this.panelConteudo.Controls.Add(this.labelControl10);
            this.panelConteudo.Controls.Add(this.txtDataInicial);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.cboStatusAtendimento);
            this.panelConteudo.Controls.Add(this.gcAtendimentos);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(4);
            this.panelConteudo.Size = new System.Drawing.Size(1314, 544);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnAtender);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnHistorico);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(544, 48);
            this.flowLayoutPanel1.TabIndex = 1050;
            // 
            // btnAtender
            // 
            this.btnAtender.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtender.FlatAppearance.BorderSize = 0;
            this.btnAtender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtender.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnAtender.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAtender.Location = new System.Drawing.Point(0, 0);
            this.btnAtender.Margin = new System.Windows.Forms.Padding(0);
            this.btnAtender.Name = "btnAtender";
            this.btnAtender.Size = new System.Drawing.Size(120, 40);
            this.btnAtender.TabIndex = 1047;
            this.btnAtender.Text = " Atender";
            this.btnAtender.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAtender.UseVisualStyleBackColor = true;
            this.btnAtender.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.FlatAppearance.BorderSize = 0;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Image = global::Programax.Easy.View.Properties.Resources.iconLimpar;
            this.btnLimpar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnLimpar.Location = new System.Drawing.Point(120, 0);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(0);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(88, 40);
            this.btnLimpar.TabIndex = 10038;
            this.btnLimpar.Text = " Limpar";
            this.btnLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpar.UseVisualStyleBackColor = true;
            // 
            // btnHistorico
            // 
            this.btnHistorico.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorico.FlatAppearance.BorderSize = 0;
            this.btnHistorico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorico.Image = global::Programax.Easy.View.Properties.Resources.icone_tres_pontos;
            this.btnHistorico.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnHistorico.Location = new System.Drawing.Point(208, 0);
            this.btnHistorico.Margin = new System.Windows.Forms.Padding(0);
            this.btnHistorico.Name = "btnHistorico";
            this.btnHistorico.Size = new System.Drawing.Size(120, 40);
            this.btnHistorico.TabIndex = 10039;
            this.btnHistorico.Text = " Histórico";
            this.btnHistorico.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHistorico.UseVisualStyleBackColor = true;
            // 
            // btnFechar
            // 
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFechar.Location = new System.Drawing.Point(328, 0);
            this.btnFechar.Margin = new System.Windows.Forms.Padding(0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(100, 40);
            this.btnFechar.TabIndex = 1048;
            this.btnFechar.Text = " Sair";
            this.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // gcAtendimentos
            // 
            this.gcAtendimentos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gcAtendimentos.Location = new System.Drawing.Point(4, 123);
            this.gcAtendimentos.MainView = this.gridView5;
            this.gcAtendimentos.Name = "gcAtendimentos";
            this.gcAtendimentos.Size = new System.Drawing.Size(1300, 418);
            this.gcAtendimentos.TabIndex = 10;
            this.gcAtendimentos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcAtendimentos.Click += new System.EventHandler(this.gcPedidosDeVenda_Click);
            this.gcAtendimentos.DoubleClick += new System.EventHandler(this.gcPedidosDeVenda_DoubleClick);
            this.gcAtendimentos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcPedidosDeVenda_KeyDown);
            // 
            // gridView5
            // 
            this.gridView5.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridView5.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridView5.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView5.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView5.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView5.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.CodigoCliente,
            this.colunaCliente,
            this.colunaNovo,
            this.colunaId,
            this.colunaDataCompra,
            this.colunaDataAgedamento,
            this.Cor,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcAtendimentos;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Atendimentos";
            // 
            // CodigoCliente
            // 
            this.CodigoCliente.Caption = "Codigo Cliente";
            this.CodigoCliente.FieldName = "CodigoCliente";
            this.CodigoCliente.MinWidth = 19;
            this.CodigoCliente.Name = "CodigoCliente";
            this.CodigoCliente.OptionsColumn.AllowEdit = false;
            this.CodigoCliente.OptionsColumn.AllowFocus = false;
            this.CodigoCliente.OptionsFilter.AllowFilter = false;
            this.CodigoCliente.Visible = true;
            this.CodigoCliente.VisibleIndex = 0;
            this.CodigoCliente.Width = 83;
            // 
            // colunaCliente
            // 
            this.colunaCliente.Caption = "Cliente";
            this.colunaCliente.FieldName = "Cliente";
            this.colunaCliente.Name = "colunaCliente";
            this.colunaCliente.OptionsColumn.AllowEdit = false;
            this.colunaCliente.OptionsColumn.AllowFocus = false;
            this.colunaCliente.OptionsColumn.ReadOnly = true;
            this.colunaCliente.OptionsFilter.AllowFilter = false;
            this.colunaCliente.Visible = true;
            this.colunaCliente.VisibleIndex = 1;
            this.colunaCliente.Width = 270;
            // 
            // colunaNovo
            // 
            this.colunaNovo.Caption = "Pedido Novo";
            this.colunaNovo.FieldName = "NumPedidoNovo";
            this.colunaNovo.Name = "colunaNovo";
            this.colunaNovo.OptionsColumn.AllowEdit = false;
            this.colunaNovo.OptionsColumn.ReadOnly = true;
            this.colunaNovo.Visible = true;
            this.colunaNovo.VisibleIndex = 2;
            this.colunaNovo.Width = 79;
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colunaId.Caption = "Nr. Pedido";
            this.colunaId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaId.FieldName = "Id";
            this.colunaId.MinWidth = 45;
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colunaId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaId.Visible = true;
            this.colunaId.VisibleIndex = 3;
            this.colunaId.Width = 90;
            // 
            // colunaDataCompra
            // 
            this.colunaDataCompra.AppearanceCell.Options.UseTextOptions = true;
            this.colunaDataCompra.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colunaDataCompra.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaDataCompra.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colunaDataCompra.Caption = "Data da Compra";
            this.colunaDataCompra.FieldName = "DataCompra";
            this.colunaDataCompra.Name = "colunaDataCompra";
            this.colunaDataCompra.OptionsColumn.AllowEdit = false;
            this.colunaDataCompra.OptionsColumn.AllowFocus = false;
            this.colunaDataCompra.OptionsFilter.AllowFilter = false;
            this.colunaDataCompra.Visible = true;
            this.colunaDataCompra.VisibleIndex = 4;
            this.colunaDataCompra.Width = 96;
            // 
            // colunaDataAgedamento
            // 
            this.colunaDataAgedamento.AppearanceCell.Options.UseTextOptions = true;
            this.colunaDataAgedamento.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colunaDataAgedamento.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaDataAgedamento.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colunaDataAgedamento.Caption = "Agendamento";
            this.colunaDataAgedamento.FieldName = "Agendamento";
            this.colunaDataAgedamento.Name = "colunaDataAgedamento";
            this.colunaDataAgedamento.Visible = true;
            this.colunaDataAgedamento.VisibleIndex = 5;
            this.colunaDataAgedamento.Width = 90;
            // 
            // Cor
            // 
            this.Cor.FieldName = "Cor";
            this.Cor.Name = "Cor";
            this.Cor.Visible = true;
            this.Cor.VisibleIndex = 7;
            this.Cor.Width = 44;
            // 
            // colunaStatus
            // 
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "Status";
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.ReadOnly = true;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 6;
            this.colunaStatus.Width = 112;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcAtendimentos;
            this.gridView2.Name = "gridView2";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Appearance.Options.UseFont = true;
            this.labStatus.Location = new System.Drawing.Point(1109, 1);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 652;
            this.labStatus.Text = "Status";
            // 
            // cboStatusAtendimento
            // 
            this.cboStatusAtendimento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboStatusAtendimento.Location = new System.Drawing.Point(1109, 16);
            this.cboStatusAtendimento.Name = "cboStatusAtendimento";
            this.cboStatusAtendimento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatusAtendimento.Properties.Appearance.Options.UseFont = true;
            this.cboStatusAtendimento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatusAtendimento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatusAtendimento.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatusAtendimento.Properties.DropDownRows = 8;
            this.cboStatusAtendimento.Properties.NullText = "";
            this.cboStatusAtendimento.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatusAtendimento.Size = new System.Drawing.Size(173, 22);
            this.cboStatusAtendimento.TabIndex = 9;
            this.cboStatusAtendimento.EditValueChanged += new System.EventHandler(this.cboStatusAtendimento_EditValueChanged);
            this.cboStatusAtendimento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboSituacao_KeyDown);
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtDataFinal.EditValue = "";
            this.txtDataFinal.EnterMoveNextControl = true;
            this.txtDataFinal.Location = new System.Drawing.Point(586, 59);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinal.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinal.Size = new System.Drawing.Size(117, 22);
            this.txtDataFinal.TabIndex = 2;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(586, 43);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 13);
            this.labelControl10.TabIndex = 10024;
            this.labelControl10.Text = "Data Final";
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtDataInicial.EditValue = "";
            this.txtDataInicial.EnterMoveNextControl = true;
            this.txtDataInicial.Location = new System.Drawing.Point(462, 59);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicial.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicial.Size = new System.Drawing.Size(117, 22);
            this.txtDataInicial.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(462, 43);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(53, 13);
            this.labelControl1.TabIndex = 10023;
            this.labelControl1.Text = "Data Inicial";
            // 
            // labelControl67
            // 
            this.labelControl67.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl67.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl67.Appearance.Options.UseFont = true;
            this.labelControl67.Appearance.Options.UseForeColor = true;
            this.labelControl67.Location = new System.Drawing.Point(6, 1);
            this.labelControl67.Name = "labelControl67";
            this.labelControl67.Size = new System.Drawing.Size(83, 13);
            this.labelControl67.TabIndex = 741;
            this.labelControl67.Text = "Código do Cliente";
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtIdCliente.EnterMoveNextControl = true;
            this.txtIdCliente.Location = new System.Drawing.Point(6, 16);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdCliente.Properties.Appearance.Options.UseFont = true;
            this.txtIdCliente.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIdCliente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIdCliente.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtIdCliente.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtIdCliente.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIdCliente.Properties.MaxLength = 10;
            this.txtIdCliente.Size = new System.Drawing.Size(99, 22);
            this.txtIdCliente.TabIndex = 5;
            this.txtIdCliente.EditValueChanged += new System.EventHandler(this.txtIdCliente_EditValueChanged);
            this.txtIdCliente.Leave += new System.EventHandler(this.txtIdCliente_Leave);
            // 
            // btnPesquisaPessoa
            // 
            this.btnPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaPessoa.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisaPessoa.Image")));
            this.btnPesquisaPessoa.Location = new System.Drawing.Point(111, 16);
            this.btnPesquisaPessoa.Name = "btnPesquisaPessoa";
            this.btnPesquisaPessoa.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaPessoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaPessoa.TabIndex = 10027;
            this.btnPesquisaPessoa.TabStop = false;
            this.btnPesquisaPessoa.Click += new System.EventHandler(this.btnPesquisaPessoa_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(866, 1);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 13);
            this.labelControl2.TabIndex = 10031;
            this.labelControl2.Text = "CPF/CNPJ";
            // 
            // txtCpfCnpj
            // 
            this.txtCpfCnpj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtCpfCnpj.EnterMoveNextControl = true;
            this.txtCpfCnpj.Location = new System.Drawing.Point(866, 16);
            this.txtCpfCnpj.Name = "txtCpfCnpj";
            this.txtCpfCnpj.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCpfCnpj.Properties.Appearance.Options.UseFont = true;
            this.txtCpfCnpj.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCpfCnpj.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCpfCnpj.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCpfCnpj.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtCpfCnpj.Properties.Mask.EditMask = "99/";
            this.txtCpfCnpj.Properties.ReadOnly = true;
            this.txtCpfCnpj.Size = new System.Drawing.Size(204, 22);
            this.txtCpfCnpj.TabIndex = 7;
            this.txtCpfCnpj.TabStop = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(174, 1);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(78, 13);
            this.labelControl3.TabIndex = 10030;
            this.labelControl3.Text = "Nome do Cliente";
            // 
            // txtNomeCliente
            // 
            this.txtNomeCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtNomeCliente.EnterMoveNextControl = true;
            this.txtNomeCliente.Location = new System.Drawing.Point(175, 16);
            this.txtNomeCliente.Name = "txtNomeCliente";
            this.txtNomeCliente.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeCliente.Properties.Appearance.Options.UseFont = true;
            this.txtNomeCliente.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomeCliente.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomeCliente.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeCliente.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomeCliente.Properties.Mask.EditMask = "99/";
            this.txtNomeCliente.Properties.ReadOnly = true;
            this.txtNomeCliente.Size = new System.Drawing.Size(659, 22);
            this.txtNomeCliente.TabIndex = 6;
            this.txtNomeCliente.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(447, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(441, 41);
            this.flowLayoutPanel2.TabIndex = 10041;
            // 
            // cboPeriodoPreDeterminado
            // 
            this.cboPeriodoPreDeterminado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboPeriodoPreDeterminado.Location = new System.Drawing.Point(111, 59);
            this.cboPeriodoPreDeterminado.Name = "cboPeriodoPreDeterminado";
            this.cboPeriodoPreDeterminado.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPeriodoPreDeterminado.Properties.Appearance.Options.UseFont = true;
            this.cboPeriodoPreDeterminado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPeriodoPreDeterminado.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboPeriodoPreDeterminado.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboPeriodoPreDeterminado.Properties.DropDownRows = 8;
            this.cboPeriodoPreDeterminado.Properties.NullText = "";
            this.cboPeriodoPreDeterminado.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboPeriodoPreDeterminado.Size = new System.Drawing.Size(285, 22);
            this.cboPeriodoPreDeterminado.TabIndex = 10032;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(111, 44);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(120, 13);
            this.labelControl4.TabIndex = 10033;
            this.labelControl4.Text = "Período Pré-Determinado";
            // 
            // cboMarcas
            // 
            this.cboMarcas.EnterMoveNextControl = true;
            this.cboMarcas.Location = new System.Drawing.Point(737, 59);
            this.cboMarcas.Name = "cboMarcas";
            this.cboMarcas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMarcas.Properties.Appearance.Options.UseFont = true;
            this.cboMarcas.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMarcas.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboMarcas.Properties.DropDownRows = 5;
            this.cboMarcas.Properties.NullText = "";
            this.cboMarcas.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboMarcas.Size = new System.Drawing.Size(233, 22);
            this.cboMarcas.TabIndex = 10034;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Location = new System.Drawing.Point(737, 44);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(30, 13);
            this.labelControl8.TabIndex = 10035;
            this.labelControl8.Text = "Marca";
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisa.Location = new System.Drawing.Point(976, 58);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisa.TabIndex = 10036;
            this.btnPesquisa.TabStop = false;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Appearance.Options.UseFont = true;
            this.labCodigo.Location = new System.Drawing.Point(6, 43);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(50, 13);
            this.labCodigo.TabIndex = 10038;
            this.labCodigo.Text = "Nr. Pedido";
            // 
            // txtId
            // 
            this.txtId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtId.EnterMoveNextControl = true;
            this.txtId.Location = new System.Drawing.Point(6, 58);
            this.txtId.Name = "txtId";
            this.txtId.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Properties.Appearance.Options.UseFont = true;
            this.txtId.Properties.Appearance.Options.UseTextOptions = true;
            this.txtId.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtId.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtId.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtId.Properties.Mask.EditMask = "99/";
            this.txtId.Properties.MaxLength = 8;
            this.txtId.Size = new System.Drawing.Size(101, 22);
            this.txtId.TabIndex = 10037;
            this.txtId.Leave += new System.EventHandler(this.txtId_Leave);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(1200, 43);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(64, 13);
            this.labelControl5.TabIndex = 10040;
            this.labelControl5.Text = "Qtde.Pedidos";
            // 
            // txtQtdePedidos
            // 
            this.txtQtdePedidos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtQtdePedidos.EnterMoveNextControl = true;
            this.txtQtdePedidos.Location = new System.Drawing.Point(1202, 58);
            this.txtQtdePedidos.Name = "txtQtdePedidos";
            this.txtQtdePedidos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQtdePedidos.Properties.Appearance.Options.UseFont = true;
            this.txtQtdePedidos.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtQtdePedidos.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtQtdePedidos.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQtdePedidos.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtQtdePedidos.Properties.Mask.EditMask = "99/";
            this.txtQtdePedidos.Properties.ReadOnly = true;
            this.txtQtdePedidos.Size = new System.Drawing.Size(80, 22);
            this.txtQtdePedidos.TabIndex = 10039;
            this.txtQtdePedidos.TabStop = false;
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Appearance.Options.UseForeColor = true;
            this.labelControl12.Location = new System.Drawing.Point(6, 81);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(41, 13);
            this.labelControl12.TabIndex = 10219;
            this.labelControl12.Text = "Carteiras";
            // 
            // cboCarteiras
            // 
            this.cboCarteiras.EnterMoveNextControl = true;
            this.cboCarteiras.Location = new System.Drawing.Point(6, 96);
            this.cboCarteiras.Name = "cboCarteiras";
            this.cboCarteiras.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCarteiras.Properties.Appearance.Options.UseFont = true;
            this.cboCarteiras.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCarteiras.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Id", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Carteira", "Carteiras")});
            this.cboCarteiras.Properties.DropDownRows = 5;
            this.cboCarteiras.Properties.NullText = "";
            this.cboCarteiras.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboCarteiras.Size = new System.Drawing.Size(298, 22);
            this.cboCarteiras.TabIndex = 10218;
            this.cboCarteiras.EditValueChanged += new System.EventHandler(this.cboCarteiras_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(350, 81);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(46, 13);
            this.labelControl6.TabIndex = 10221;
            this.labelControl6.Text = "Vendedor";
            // 
            // cboVendedores
            // 
            this.cboVendedores.EnterMoveNextControl = true;
            this.cboVendedores.Location = new System.Drawing.Point(350, 95);
            this.cboVendedores.Name = "cboVendedores";
            this.cboVendedores.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVendedores.Properties.Appearance.Options.UseFont = true;
            this.cboVendedores.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboVendedores.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Id", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Vendedor", "Vendedores")});
            this.cboVendedores.Properties.DropDownRows = 5;
            this.cboVendedores.Properties.NullText = "";
            this.cboVendedores.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboVendedores.Size = new System.Drawing.Size(298, 22);
            this.cboVendedores.TabIndex = 10222;
            this.cboVendedores.EditValueChanged += new System.EventHandler(this.cboVendedores_EditValueChanged);
            // 
            // btnpesquisacarteiras
            // 
            this.btnpesquisacarteiras.BackColor = System.Drawing.Color.Transparent;
            this.btnpesquisacarteiras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnpesquisacarteiras.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnpesquisacarteiras.Location = new System.Drawing.Point(310, 96);
            this.btnpesquisacarteiras.Name = "btnpesquisacarteiras";
            this.btnpesquisacarteiras.Size = new System.Drawing.Size(22, 22);
            this.btnpesquisacarteiras.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnpesquisacarteiras.TabIndex = 10223;
            this.btnpesquisacarteiras.TabStop = false;
            this.btnpesquisacarteiras.Click += new System.EventHandler(this.btnpesquisacarteiras_Click);
            // 
            // FormBuscaAtendimentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 655);
            this.Location = new System.Drawing.Point(3, 65);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormBuscaAtendimentos";
            this.NomeDaTela = "Telemarketing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Telemarketing";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAtendimentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatusAtendimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPessoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCpfCnpj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomeCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPeriodoPreDeterminado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMarcas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQtdePedidos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCarteiras.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVendedores.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnpesquisacarteiras)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAtender;
        private System.Windows.Forms.Button btnFechar;
        private DevExpress.XtraGrid.GridControl gcAtendimentos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataCompra;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCliente;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.LookUpEdit cboStatusAtendimento;
        private DevExpress.XtraEditors.DateEdit txtDataFinal;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit txtDataInicial;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl67;
        private DevExpress.XtraEditors.TextEdit txtIdCliente;
        private System.Windows.Forms.PictureBox btnPesquisaPessoa;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtCpfCnpj;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtNomeCliente;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private DevExpress.XtraEditors.LookUpEdit cboPeriodoPreDeterminado;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit cboMarcas;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private System.Windows.Forms.PictureBox btnPesquisa;
        private DevExpress.XtraGrid.Columns.GridColumn CodigoCliente;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Columns.GridColumn Cor;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNovo;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private DevExpress.XtraEditors.TextEdit txtId;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtQtdePedidos;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LookUpEdit cboCarteiras;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataAgedamento;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit cboVendedores;
        private System.Windows.Forms.PictureBox btnpesquisacarteiras;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnHistorico;
    }
}