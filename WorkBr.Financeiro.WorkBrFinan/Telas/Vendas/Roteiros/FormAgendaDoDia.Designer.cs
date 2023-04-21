using Programax.Easy.View.Componentes;
namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    partial class FormAgendaDoDia
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
            this.components = new System.ComponentModel.Container();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcAgendas = new Programax.Easy.View.Componentes.AkilGridControl();
            this.gridViewRoteiro = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaMarcador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataElaboracao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaPeriodo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaPedido = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaEndereco = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFechar = new System.Windows.Forms.Button();
            this.txtDia = new DevExpress.XtraEditors.DateEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdicionarSelecao = new System.Windows.Forms.Button();
            this.btnImprimirRelatorio = new System.Windows.Forms.Button();
            this.txtQuantidade = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.cboPeriodo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnAdicionarRoteiro = new System.Windows.Forms.Button();
            this.btnDetalhes = new System.Windows.Forms.Button();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAgendas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoteiro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDia.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDia.Properties)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantidade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPeriodo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.btnDetalhes);
            this.painelBotoes.Controls.Add(this.flowLayoutPanel2);
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(1030, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.panelConteudo.Controls.Add(this.btnAdicionarRoteiro);
            this.panelConteudo.Controls.Add(this.cboPeriodo);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.txtQuantidade);
            this.panelConteudo.Controls.Add(this.labelControl8);
            this.panelConteudo.Controls.Add(this.txtDia);
            this.panelConteudo.Controls.Add(this.labDataCadastro);
            this.panelConteudo.Controls.Add(this.gcAgendas);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(4);
            this.panelConteudo.Size = new System.Drawing.Size(979, 458);
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcAgendas;
            this.gridView2.Name = "gridView2";
            // 
            // gcAgendas
            // 
            this.gcAgendas.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcAgendas.Location = new System.Drawing.Point(3, 46);
            this.gcAgendas.MainView = this.gridViewRoteiro;
            this.gcAgendas.ModoImpressao = Programax.Easy.View.Componentes.Enumeradores.EnumAkilGridControlModoImpressao.PAISAGEM;
            this.gcAgendas.Name = "gcAgendas";
            this.gcAgendas.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.gcAgendas.Size = new System.Drawing.Size(976, 366);
            this.gcAgendas.TabIndex = 10;
            this.gcAgendas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRoteiro,
            this.gridView2});
            this.gcAgendas.Click += new System.EventHandler(this.gcContasPagarReceber_Click);
            this.gcAgendas.DoubleClick += new System.EventHandler(this.gcContasPagarReceber_DoubleClick);
            this.gcAgendas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcContasPagarReceber_KeyDown);
            this.gcAgendas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gcContasPagarReceber_KeyUp);
            // 
            // gridViewRoteiro
            // 
            this.gridViewRoteiro.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridViewRoteiro.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridViewRoteiro.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewRoteiro.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridViewRoteiro.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridViewRoteiro.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewRoteiro.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewRoteiro.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridViewRoteiro.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridViewRoteiro.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gridViewRoteiro.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridViewRoteiro.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridViewRoteiro.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridViewRoteiro.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewRoteiro.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridViewRoteiro.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaMarcador,
            this.colunaId,
            this.colunaDataElaboracao,
            this.colunaPeriodo,
            this.colunaPedido,
            this.colunaCliente,
            this.colunaEndereco});
            this.gridViewRoteiro.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridViewRoteiro.GridControl = this.gcAgendas;
            this.gridViewRoteiro.GroupPanelText = "Arraste para agrupar";
            this.gridViewRoteiro.Name = "gridViewRoteiro";
            this.gridViewRoteiro.OptionsSelection.MultiSelect = true;
            this.gridViewRoteiro.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewRoteiro.OptionsView.RowAutoHeight = true;
            this.gridViewRoteiro.OptionsView.ShowGroupPanel = false;
            this.gridViewRoteiro.OptionsView.ShowIndicator = false;
            this.gridViewRoteiro.OptionsView.ShowViewCaption = true;
            this.gridViewRoteiro.PaintStyleName = "Skin";
            this.gridViewRoteiro.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewRoteiro.ViewCaption = "Agendas";
            // 
            // colunaMarcador
            // 
            this.colunaMarcador.Caption = " ";
            this.colunaMarcador.FieldName = "Imagem";
            this.colunaMarcador.Name = "colunaMarcador";
            this.colunaMarcador.OptionsColumn.AllowEdit = false;
            this.colunaMarcador.OptionsColumn.AllowFocus = false;
            this.colunaMarcador.OptionsFilter.AllowFilter = false;
            this.colunaMarcador.Visible = true;
            this.colunaMarcador.VisibleIndex = 0;
            this.colunaMarcador.Width = 38;
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Nr. Roteiro";
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
            this.colunaId.Width = 70;
            // 
            // colunaDataElaboracao
            // 
            this.colunaDataElaboracao.Caption = "Dt Agenda";
            this.colunaDataElaboracao.FieldName = "DataElaboracao";
            this.colunaDataElaboracao.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateMonth;
            this.colunaDataElaboracao.Name = "colunaDataElaboracao";
            this.colunaDataElaboracao.OptionsColumn.AllowEdit = false;
            this.colunaDataElaboracao.OptionsColumn.AllowFocus = false;
            this.colunaDataElaboracao.OptionsFilter.AllowFilter = false;
            this.colunaDataElaboracao.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.colunaDataElaboracao.Visible = true;
            this.colunaDataElaboracao.VisibleIndex = 5;
            this.colunaDataElaboracao.Width = 70;
            // 
            // colunaPeriodo
            // 
            this.colunaPeriodo.Caption = "Período";
            this.colunaPeriodo.FieldName = "Periodo";
            this.colunaPeriodo.MinWidth = 19;
            this.colunaPeriodo.Name = "colunaPeriodo";
            this.colunaPeriodo.OptionsColumn.AllowEdit = false;
            this.colunaPeriodo.OptionsColumn.AllowFocus = false;
            this.colunaPeriodo.OptionsFilter.AllowFilter = false;
            this.colunaPeriodo.Visible = true;
            this.colunaPeriodo.VisibleIndex = 1;
            this.colunaPeriodo.Width = 54;
            // 
            // colunaPedido
            // 
            this.colunaPedido.Caption = "Pedido";
            this.colunaPedido.FieldName = "Pedido";
            this.colunaPedido.Name = "colunaPedido";
            this.colunaPedido.OptionsColumn.AllowEdit = false;
            this.colunaPedido.OptionsColumn.AllowFocus = false;
            this.colunaPedido.OptionsFilter.AllowFilter = false;
            this.colunaPedido.Visible = true;
            this.colunaPedido.VisibleIndex = 2;
            this.colunaPedido.Width = 62;
            // 
            // colunaCliente
            // 
            this.colunaCliente.Caption = "Cliente";
            this.colunaCliente.FieldName = "Cliente";
            this.colunaCliente.Name = "colunaCliente";
            this.colunaCliente.OptionsColumn.AllowEdit = false;
            this.colunaCliente.OptionsColumn.AllowFocus = false;
            this.colunaCliente.OptionsFilter.AllowFilter = false;
            this.colunaCliente.Visible = true;
            this.colunaCliente.VisibleIndex = 3;
            this.colunaCliente.Width = 148;
            // 
            // colunaEndereco
            // 
            this.colunaEndereco.Caption = "Endereço";
            this.colunaEndereco.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colunaEndereco.FieldName = "Endereco";
            this.colunaEndereco.MinWidth = 19;
            this.colunaEndereco.Name = "colunaEndereco";
            this.colunaEndereco.OptionsColumn.AllowEdit = false;
            this.colunaEndereco.OptionsColumn.AllowFocus = false;
            this.colunaEndereco.OptionsFilter.AllowAutoFilter = false;
            this.colunaEndereco.Visible = true;
            this.colunaEndereco.VisibleIndex = 4;
            this.colunaEndereco.Width = 229;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(300, 46);
            this.flowLayoutPanel1.TabIndex = 1049;
            // 
            // btnFechar
            // 
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFechar.Location = new System.Drawing.Point(0, 0);
            this.btnFechar.Margin = new System.Windows.Forms.Padding(0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(100, 40);
            this.btnFechar.TabIndex = 1048;
            this.btnFechar.Text = " Sair";
            this.btnFechar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // txtDia
            // 
            this.txtDia.EditValue = "";
            this.txtDia.EnterMoveNextControl = true;
            this.txtDia.Location = new System.Drawing.Point(8, 20);
            this.txtDia.Name = "txtDia";
            this.txtDia.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDia.Properties.Appearance.Options.UseFont = true;
            this.txtDia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDia.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDia.Properties.ReadOnly = true;
            this.txtDia.Size = new System.Drawing.Size(104, 22);
            this.txtDia.TabIndex = 5;
            this.txtDia.TabStop = false;
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Appearance.ForeColor = System.Drawing.Color.White;
            this.labDataCadastro.Appearance.Options.UseFont = true;
            this.labDataCadastro.Appearance.Options.UseForeColor = true;
            this.labDataCadastro.Location = new System.Drawing.Point(10, 4);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(16, 13);
            this.labDataCadastro.TabIndex = 10036;
            this.labDataCadastro.Text = "Dia";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnAdicionarSelecao);
            this.flowLayoutPanel2.Controls.Add(this.btnImprimirRelatorio);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(513, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(472, 40);
            this.flowLayoutPanel2.TabIndex = 1050;
            // 
            // btnAdicionarSelecao
            // 
            this.btnAdicionarSelecao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarSelecao.FlatAppearance.BorderSize = 0;
            this.btnAdicionarSelecao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarSelecao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionarSelecao.Image = global::Programax.Easy.View.Properties.Resources.icone_baixar;
            this.btnAdicionarSelecao.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAdicionarSelecao.Location = new System.Drawing.Point(360, 0);
            this.btnAdicionarSelecao.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdicionarSelecao.Name = "btnAdicionarSelecao";
            this.btnAdicionarSelecao.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAdicionarSelecao.Size = new System.Drawing.Size(112, 40);
            this.btnAdicionarSelecao.TabIndex = 1052;
            this.btnAdicionarSelecao.Text = " Adicionar";
            this.btnAdicionarSelecao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarSelecao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdicionarSelecao.UseVisualStyleBackColor = true;
            this.btnAdicionarSelecao.Click += new System.EventHandler(this.btnAdicionarSelecao_Click);
            // 
            // btnImprimirRelatorio
            // 
            this.btnImprimirRelatorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimirRelatorio.FlatAppearance.BorderSize = 0;
            this.btnImprimirRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirRelatorio.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnImprimirRelatorio.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImprimirRelatorio.Location = new System.Drawing.Point(249, 0);
            this.btnImprimirRelatorio.Margin = new System.Windows.Forms.Padding(0);
            this.btnImprimirRelatorio.Name = "btnImprimirRelatorio";
            this.btnImprimirRelatorio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnImprimirRelatorio.Size = new System.Drawing.Size(111, 40);
            this.btnImprimirRelatorio.TabIndex = 1051;
            this.btnImprimirRelatorio.Text = " Imprimir";
            this.btnImprimirRelatorio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimirRelatorio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimirRelatorio.UseVisualStyleBackColor = true;
            this.btnImprimirRelatorio.Click += new System.EventHandler(this.btnImprimirRelatorio_Click);
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.EnterMoveNextControl = true;
            this.txtQuantidade.Location = new System.Drawing.Point(872, 431);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidade.Properties.Appearance.Options.UseFont = true;
            this.txtQuantidade.Properties.Appearance.Options.UseTextOptions = true;
            this.txtQuantidade.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtQuantidade.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtQuantidade.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtQuantidade.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtQuantidade.Properties.MaxLength = 50;
            this.txtQuantidade.Properties.ReadOnly = true;
            this.txtQuantidade.Size = new System.Drawing.Size(105, 24);
            this.txtQuantidade.TabIndex = 10048;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Location = new System.Drawing.Point(872, 415);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(81, 13);
            this.labelControl8.TabIndex = 10049;
            this.labelControl8.Text = "Qtde Agendas";
            // 
            // cboPeriodo
            // 
            this.cboPeriodo.EnterMoveNextControl = true;
            this.cboPeriodo.Location = new System.Drawing.Point(117, 20);
            this.cboPeriodo.Name = "cboPeriodo";
            this.cboPeriodo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPeriodo.Properties.Appearance.Options.UseFont = true;
            this.cboPeriodo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPeriodo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboPeriodo.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboPeriodo.Properties.DropDownRows = 6;
            this.cboPeriodo.Properties.NullText = "";
            this.cboPeriodo.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboPeriodo.Size = new System.Drawing.Size(123, 22);
            this.cboPeriodo.TabIndex = 10069;
            this.cboPeriodo.EditValueChanged += new System.EventHandler(this.cboPeriodo_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(118, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 13);
            this.labelControl2.TabIndex = 10068;
            this.labelControl2.Text = "Período do Dia";
            // 
            // btnAdicionarRoteiro
            // 
            this.btnAdicionarRoteiro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarRoteiro.FlatAppearance.BorderSize = 0;
            this.btnAdicionarRoteiro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarRoteiro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarRoteiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarRoteiro.Image = global::Programax.Easy.View.Properties.Resources.icone_adicionar_branco;
            this.btnAdicionarRoteiro.Location = new System.Drawing.Point(247, 19);
            this.btnAdicionarRoteiro.Name = "btnAdicionarRoteiro";
            this.btnAdicionarRoteiro.Size = new System.Drawing.Size(23, 23);
            this.btnAdicionarRoteiro.TabIndex = 10070;
            this.btnAdicionarRoteiro.TabStop = false;
            this.btnAdicionarRoteiro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarRoteiro.UseVisualStyleBackColor = true;
            this.btnAdicionarRoteiro.Click += new System.EventHandler(this.btnAdicionarRoteiro_Click);
            // 
            // btnDetalhes
            // 
            this.btnDetalhes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetalhes.FlatAppearance.BorderSize = 0;
            this.btnDetalhes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetalhes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetalhes.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnDetalhes.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDetalhes.Location = new System.Drawing.Point(608, 0);
            this.btnDetalhes.Margin = new System.Windows.Forms.Padding(0);
            this.btnDetalhes.Name = "btnDetalhes";
            this.btnDetalhes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDetalhes.Size = new System.Drawing.Size(154, 40);
            this.btnDetalhes.TabIndex = 1052;
            this.btnDetalhes.Text = " Imprimir Detalhes";
            this.btnDetalhes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetalhes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDetalhes.UseVisualStyleBackColor = true;
            this.btnDetalhes.Click += new System.EventHandler(this.btnDetalhes_Click);
            // 
            // FormAgendaDoDia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 569);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "FormAgendaDoDia";
            this.NomeDaTela = "Agenda do Dia";
            this.Text = "Agenda do Dia";
            this.Load += new System.EventHandler(this.FormRoteiroPesquisa_Load);
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAgendas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoteiro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDia.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDia.Properties)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantidade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPeriodo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRoteiro;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCliente;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.DateEdit txtDia;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraGrid.Columns.GridColumn colunaPedido;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataElaboracao;
        public AkilGridControl gcAgendas;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private DevExpress.XtraEditors.TextEdit txtQuantidade;
        public DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraGrid.Columns.GridColumn colunaMarcador;
        private System.Windows.Forms.Button btnImprimirRelatorio;
        private System.Windows.Forms.Button btnAdicionarSelecao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaEndereco;
        private DevExpress.XtraGrid.Columns.GridColumn colunaPeriodo;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraEditors.LookUpEdit cboPeriodo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Button btnAdicionarRoteiro;
        private System.Windows.Forms.Button btnDetalhes;
    }
}