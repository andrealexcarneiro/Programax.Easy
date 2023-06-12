namespace Programax.Easy.View.Telas.Cadastros.Inventarios
{
    partial class FormReservasPedidos
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.btnPesquisaInventario = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gcItens = new Programax.Easy.View.Componentes.AkilGridControl();
            this.gridViewRoteiro = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.gcAcompanhamentoOnline = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricaoProduto = new DevExpress.XtraEditors.TextEdit();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnImprimirRelatorio = new System.Windows.Forms.Button();
            this.txtreserva = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaInventario)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoteiro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAcompanhamentoOnline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoProduto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtreserva.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.labelControl2);
            this.painelBotoes.Controls.Add(this.txtreserva);
            this.painelBotoes.Controls.Add(this.btnImprimirRelatorio);
            this.painelBotoes.Controls.Add(this.btnSair);
            this.painelBotoes.Location = new System.Drawing.Point(7, 0);
            this.painelBotoes.Size = new System.Drawing.Size(1440, 60);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.txtDescricaoProduto);
            this.panelConteudo.Controls.Add(this.groupBox2);
            this.panelConteudo.Controls.Add(this.txtId);
            this.panelConteudo.Controls.Add(this.labCodigo);
            this.panelConteudo.Controls.Add(this.btnPesquisaInventario);
            this.panelConteudo.Size = new System.Drawing.Size(782, 399);
            // 
            // txtId
            // 
            this.txtId.EnterMoveNextControl = true;
            this.txtId.Location = new System.Drawing.Point(3, 20);
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
            this.txtId.Size = new System.Drawing.Size(96, 22);
            this.txtId.TabIndex = 1;
            this.txtId.EditValueChanged += new System.EventHandler(this.txtId_EditValueChanged);
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSomenteNumeros_KeyPress);
            this.txtId.Leave += new System.EventHandler(this.txtId_Leave);
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Appearance.Options.UseFont = true;
            this.labCodigo.Location = new System.Drawing.Point(3, 3);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(56, 13);
            this.labCodigo.TabIndex = 501;
            this.labCodigo.Text = "Código Item";
            // 
            // btnPesquisaInventario
            // 
            this.btnPesquisaInventario.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaInventario.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaInventario.Location = new System.Drawing.Point(105, 20);
            this.btnPesquisaInventario.Name = "btnPesquisaInventario";
            this.btnPesquisaInventario.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaInventario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaInventario.TabIndex = 502;
            this.btnPesquisaInventario.TabStop = false;
            this.btnPesquisaInventario.Click += new System.EventHandler(this.btnPesquisaInventario_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gcItens);
            this.groupBox2.Location = new System.Drawing.Point(4, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(773, 346);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            // 
            // gcItens
            // 
            this.gcItens.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcItens.Location = new System.Drawing.Point(4, 10);
            this.gcItens.MainView = this.gridViewRoteiro;
            this.gcItens.ModoImpressao = Programax.Easy.View.Componentes.Enumeradores.EnumAkilGridControlModoImpressao.PAISAGEM;
            this.gcItens.Name = "gcItens";
            this.gcItens.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.gcItens.Size = new System.Drawing.Size(764, 328);
            this.gcItens.TabIndex = 10067;
            this.gcItens.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRoteiro,
            this.gridView4});
            this.gcItens.Click += new System.EventHandler(this.gcItens_Click_1);
            this.gcItens.DoubleClick += new System.EventHandler(this.gcItens_DoubleClick);
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
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gridViewRoteiro.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridViewRoteiro.GridControl = this.gcItens;
            this.gridViewRoteiro.GroupPanelText = "Arraste para agrupar";
            this.gridViewRoteiro.Name = "gridViewRoteiro";
            this.gridViewRoteiro.OptionsPrint.EnableAppearanceOddRow = true;
            this.gridViewRoteiro.OptionsSelection.MultiSelect = true;
            this.gridViewRoteiro.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewRoteiro.OptionsView.RowAutoHeight = true;
            this.gridViewRoteiro.OptionsView.ShowGroupPanel = false;
            this.gridViewRoteiro.OptionsView.ShowIndicator = false;
            this.gridViewRoteiro.OptionsView.ShowViewCaption = true;
            this.gridViewRoteiro.PaintStyleName = "Skin";
            this.gridViewRoteiro.ViewCaption = "Roteiros";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Cód. Pedido";
            this.gridColumn5.FieldName = "IdPedido";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 90;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Data";
            this.gridColumn6.FieldName = "Data";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 94;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Vendedor";
            this.gridColumn7.FieldName = "Vendedor";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            this.gridColumn7.Width = 399;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Status";
            this.gridColumn8.FieldName = "Status";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            this.gridColumn8.Width = 179;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // gridView4
            // 
            this.gridView4.GridControl = this.gcItens;
            this.gridView4.Name = "gridView4";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Appearance.Options.UseForeColor = true;
            this.labelControl12.Location = new System.Drawing.Point(202, 3);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(0, 13);
            this.labelControl12.TabIndex = 1064;
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl13.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Appearance.Options.UseForeColor = true;
            this.labelControl13.Location = new System.Drawing.Point(289, 3);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(0, 13);
            this.labelControl13.TabIndex = 1065;
            // 
            // gcAcompanhamentoOnline
            // 
            this.gcAcompanhamentoOnline.Location = new System.Drawing.Point(3, 22);
            this.gcAcompanhamentoOnline.MainView = this.gridView1;
            this.gcAcompanhamentoOnline.Name = "gcAcompanhamentoOnline";
            this.gcAcompanhamentoOnline.Size = new System.Drawing.Size(474, 65);
            this.gcAcompanhamentoOnline.TabIndex = 1062;
            this.gcAcompanhamentoOnline.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView1.GridControl = this.gcAcompanhamentoOnline;
            this.gridView1.GroupPanelText = "Enderecos";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.ViewCaption = "Itens";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcAcompanhamentoOnline;
            this.gridView2.Name = "gridView2";
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn1.AppearanceHeader.BackColor = System.Drawing.Color.Gray;
            this.gridColumn1.AppearanceHeader.BackColor2 = System.Drawing.Color.Red;
            this.gridColumn1.AppearanceHeader.BorderColor = System.Drawing.Color.Transparent;
            this.gridColumn1.AppearanceHeader.Options.UseBackColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Acomp. Online";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "AcompanhamentoOnline";
            this.gridColumn1.MinWidth = 45;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "tele_id", "{0}")});
            this.gridColumn1.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 104;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn2.Caption = "Evolução";
            this.gridColumn2.FieldName = "Evolucao";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 142;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn3.Caption = "Divergência";
            this.gridColumn3.FieldName = "Divergencia";
            this.gridColumn3.MinWidth = 10;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 101;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Início da Contagem";
            this.gridColumn4.FieldName = "DataInicio";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 124;
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Appearance.Options.UseForeColor = true;
            this.labelControl15.Location = new System.Drawing.Point(356, 3);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(0, 13);
            this.labelControl15.TabIndex = 1066;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl11.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Appearance.Options.UseForeColor = true;
            this.labelControl11.Location = new System.Drawing.Point(3, 3);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(0, 13);
            this.labelControl11.TabIndex = 1063;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(133, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 758;
            this.labelControl1.Text = "Descrição ";
            // 
            // txtDescricaoProduto
            // 
            this.txtDescricaoProduto.EnterMoveNextControl = true;
            this.txtDescricaoProduto.Location = new System.Drawing.Point(133, 20);
            this.txtDescricaoProduto.Name = "txtDescricaoProduto";
            this.txtDescricaoProduto.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoProduto.Properties.Appearance.Options.UseFont = true;
            this.txtDescricaoProduto.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricaoProduto.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricaoProduto.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricaoProduto.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricaoProduto.Properties.Mask.EditMask = "99/";
            this.txtDescricaoProduto.Properties.MaxLength = 80;
            this.txtDescricaoProduto.Properties.ReadOnly = true;
            this.txtDescricaoProduto.Size = new System.Drawing.Size(389, 22);
            this.txtDescricaoProduto.TabIndex = 757;
            this.txtDescricaoProduto.TabStop = false;
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(720, 3);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(76, 40);
            this.btnSair.TabIndex = 10037;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click_1);
            // 
            // btnImprimirRelatorio
            // 
            this.btnImprimirRelatorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimirRelatorio.FlatAppearance.BorderSize = 0;
            this.btnImprimirRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirRelatorio.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnImprimirRelatorio.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImprimirRelatorio.Location = new System.Drawing.Point(578, 0);
            this.btnImprimirRelatorio.Margin = new System.Windows.Forms.Padding(0);
            this.btnImprimirRelatorio.Name = "btnImprimirRelatorio";
            this.btnImprimirRelatorio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnImprimirRelatorio.Size = new System.Drawing.Size(101, 40);
            this.btnImprimirRelatorio.TabIndex = 10038;
            this.btnImprimirRelatorio.Text = " Imprimir";
            this.btnImprimirRelatorio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimirRelatorio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimirRelatorio.UseVisualStyleBackColor = true;
            this.btnImprimirRelatorio.Click += new System.EventHandler(this.btnImprimirRelatorio_Click);
            // 
            // txtreserva
            // 
            this.txtreserva.EnterMoveNextControl = true;
            this.txtreserva.Location = new System.Drawing.Point(440, 12);
            this.txtreserva.Name = "txtreserva";
            this.txtreserva.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtreserva.Properties.Appearance.Options.UseFont = true;
            this.txtreserva.Properties.Appearance.Options.UseTextOptions = true;
            this.txtreserva.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtreserva.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtreserva.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtreserva.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtreserva.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtreserva.Properties.Mask.EditMask = "99/";
            this.txtreserva.Properties.MaxLength = 80;
            this.txtreserva.Properties.ReadOnly = true;
            this.txtreserva.Size = new System.Drawing.Size(85, 22);
            this.txtreserva.TabIndex = 10039;
            this.txtreserva.TabStop = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(410, 17);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 13);
            this.labelControl2.TabIndex = 10040;
            this.labelControl2.Text = "Total";
            // 
            // FormReservasPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 510);
            this.Name = "FormReservasPedidos";
            this.painelBotoes.ResumeLayout(false);
            this.painelBotoes.PerformLayout();
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaInventario)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoteiro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAcompanhamentoOnline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoProduto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtreserva.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtId;
        private System.Windows.Forms.PictureBox btnPesquisaInventario;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraGrid.GridControl gcAcompanhamentoOnline;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDescricaoProduto;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnImprimirRelatorio;
        public Componentes.AkilGridControl gcItens;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRoteiro;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtreserva;
    }
}
