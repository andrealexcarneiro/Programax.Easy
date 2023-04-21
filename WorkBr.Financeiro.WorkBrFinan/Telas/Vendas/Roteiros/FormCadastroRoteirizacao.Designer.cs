using Programax.Easy.View.Componentes;
namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    partial class FormCadastroRoteirizacao
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
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcRoteiros = new Programax.Easy.View.Componentes.AkilGridControl();
            this.gridViewRoteiro = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaMarcador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataElaboracao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaPeriodo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaPedido = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaLigarFone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaObservacao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaEndereco = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colunaDataConclusao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnExcluirRoteiro = new System.Windows.Forms.Button();
            this.txtNomePessoa = new DevExpress.XtraEditors.TextEdit();
            this.btnPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.labelControl67 = new DevExpress.XtraEditors.LabelControl();
            this.txtIdPessoa = new DevExpress.XtraEditors.TextEdit();
            this.labelControl66 = new DevExpress.XtraEditors.LabelControl();
            this.btnPesquisaPedidoVendas = new System.Windows.Forms.PictureBox();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroPedidoVendas = new DevExpress.XtraEditors.TextEdit();
            this.txtDataConclusao = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSalvarRoteiro = new System.Windows.Forms.Button();
            this.btnImprimirRelatorio = new System.Windows.Forms.Button();
            this.btnDetalhes = new System.Windows.Forms.Button();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.btnEditarPedido = new System.Windows.Forms.Button();
            this.txtCodigoRoteiro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.btnAddAgendaDoDia = new System.Windows.Forms.Button();
            this.btnAdicionarRoteiro = new System.Windows.Forms.Button();
            this.txtDataRoteiro = new DevExpress.XtraEditors.DateEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRoteiros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoteiro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomePessoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPessoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdPessoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPedidoVendas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPedidoVendas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataConclusao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataConclusao.Properties)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoRoteiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataRoteiro.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataRoteiro.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.btnEditarPedido);
            this.painelBotoes.Controls.Add(this.flowLayoutPanel2);
            this.painelBotoes.Controls.Add(this.btnExcluirRoteiro);
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(1030, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(183)))), ((int)(((byte)(218)))));
            this.panelConteudo.Controls.Add(this.txtDataRoteiro);
            this.panelConteudo.Controls.Add(this.btnAdicionarRoteiro);
            this.panelConteudo.Controls.Add(this.btnAddAgendaDoDia);
            this.panelConteudo.Controls.Add(this.txtCodigoRoteiro);
            this.panelConteudo.Controls.Add(this.labelControl15);
            this.panelConteudo.Controls.Add(this.txtDataConclusao);
            this.panelConteudo.Controls.Add(this.labelControl5);
            this.panelConteudo.Controls.Add(this.labDataCadastro);
            this.panelConteudo.Controls.Add(this.gcRoteiros);
            this.panelConteudo.Controls.Add(this.labelControl66);
            this.panelConteudo.Controls.Add(this.txtNumeroPedidoVendas);
            this.panelConteudo.Controls.Add(this.txtIdPessoa);
            this.panelConteudo.Controls.Add(this.txtNomePessoa);
            this.panelConteudo.Controls.Add(this.labelControl10);
            this.panelConteudo.Controls.Add(this.labelControl67);
            this.panelConteudo.Controls.Add(this.btnPesquisaPessoa);
            this.panelConteudo.Controls.Add(this.btnPesquisaPedidoVendas);
            this.panelConteudo.Margin = new System.Windows.Forms.Padding(4);
            this.panelConteudo.Size = new System.Drawing.Size(994, 458);
            // 
            // colunaStatus
            // 
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "Status";
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 9;
            this.colunaStatus.Width = 89;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcRoteiros;
            this.gridView2.Name = "gridView2";
            // 
            // gcRoteiros
            // 
            this.gcRoteiros.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcRoteiros.Location = new System.Drawing.Point(3, 84);
            this.gcRoteiros.MainView = this.gridViewRoteiro;
            this.gcRoteiros.ModoImpressao = Programax.Easy.View.Componentes.Enumeradores.EnumAkilGridControlModoImpressao.PAISAGEM;
            this.gcRoteiros.Name = "gcRoteiros";
            this.gcRoteiros.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.gcRoteiros.Size = new System.Drawing.Size(1013, 329);
            this.gcRoteiros.TabIndex = 10;
            this.gcRoteiros.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRoteiro,
            this.gridView2});
            this.gcRoteiros.Click += new System.EventHandler(this.gcRoteiros_Click);
            this.gcRoteiros.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gcRoteiros_KeyUp);
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
            this.colunaLigarFone,
            this.colunaObservacao,
            this.colunaEndereco,
            this.colunaDataConclusao,
            this.colunaStatus});
            this.gridViewRoteiro.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridViewRoteiro.GridControl = this.gcRoteiros;
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
            this.gridViewRoteiro.ViewCaption = "Roteiros";
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
            this.colunaDataElaboracao.VisibleIndex = 7;
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
            // colunaLigarFone
            // 
            this.colunaLigarFone.Caption = "LigarFone";
            this.colunaLigarFone.FieldName = "LigarFone";
            this.colunaLigarFone.MinWidth = 19;
            this.colunaLigarFone.Name = "colunaLigarFone";
            this.colunaLigarFone.OptionsColumn.AllowEdit = false;
            this.colunaLigarFone.OptionsColumn.AllowFocus = false;
            this.colunaLigarFone.OptionsFilter.AllowFilter = false;
            this.colunaLigarFone.Visible = true;
            this.colunaLigarFone.VisibleIndex = 4;
            this.colunaLigarFone.Width = 70;
            // 
            // colunaObservacao
            // 
            this.colunaObservacao.Caption = "Observação";
            this.colunaObservacao.FieldName = "Observacao";
            this.colunaObservacao.MinWidth = 19;
            this.colunaObservacao.Name = "colunaObservacao";
            this.colunaObservacao.OptionsColumn.AllowEdit = false;
            this.colunaObservacao.OptionsColumn.AllowFocus = false;
            this.colunaObservacao.OptionsFilter.AllowFilter = false;
            this.colunaObservacao.Visible = true;
            this.colunaObservacao.VisibleIndex = 5;
            this.colunaObservacao.Width = 70;
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
            this.colunaEndereco.VisibleIndex = 6;
            this.colunaEndereco.Width = 229;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // colunaDataConclusao
            // 
            this.colunaDataConclusao.Caption = "Dt Conclusão";
            this.colunaDataConclusao.FieldName = "DataConclusao";
            this.colunaDataConclusao.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateMonth;
            this.colunaDataConclusao.Name = "colunaDataConclusao";
            this.colunaDataConclusao.OptionsColumn.AllowEdit = false;
            this.colunaDataConclusao.OptionsColumn.AllowFocus = false;
            this.colunaDataConclusao.OptionsFilter.AllowFilter = false;
            this.colunaDataConclusao.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.colunaDataConclusao.Visible = true;
            this.colunaDataConclusao.VisibleIndex = 8;
            this.colunaDataConclusao.Width = 63;
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
            // btnExcluirRoteiro
            // 
            this.btnExcluirRoteiro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluirRoteiro.FlatAppearance.BorderSize = 0;
            this.btnExcluirRoteiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluirRoteiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluirRoteiro.Image = global::Programax.Easy.View.Properties.Resources.icone_inativar;
            this.btnExcluirRoteiro.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExcluirRoteiro.Location = new System.Drawing.Point(303, 0);
            this.btnExcluirRoteiro.Margin = new System.Windows.Forms.Padding(0);
            this.btnExcluirRoteiro.Name = "btnExcluirRoteiro";
            this.btnExcluirRoteiro.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnExcluirRoteiro.Size = new System.Drawing.Size(92, 40);
            this.btnExcluirRoteiro.TabIndex = 1049;
            this.btnExcluirRoteiro.Text = " Excluir";
            this.btnExcluirRoteiro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluirRoteiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcluirRoteiro.UseVisualStyleBackColor = true;
            this.btnExcluirRoteiro.Visible = false;
            this.btnExcluirRoteiro.Click += new System.EventHandler(this.btnExcluirRoteiro_Click);
            // 
            // txtNomePessoa
            // 
            this.txtNomePessoa.EnterMoveNextControl = true;
            this.txtNomePessoa.Location = new System.Drawing.Point(154, 17);
            this.txtNomePessoa.Name = "txtNomePessoa";
            this.txtNomePessoa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomePessoa.Properties.Appearance.Options.UseFont = true;
            this.txtNomePessoa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomePessoa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomePessoa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomePessoa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomePessoa.Properties.Mask.EditMask = "99/";
            this.txtNomePessoa.Properties.ReadOnly = true;
            this.txtNomePessoa.Size = new System.Drawing.Size(860, 22);
            this.txtNomePessoa.TabIndex = 2;
            this.txtNomePessoa.TabStop = false;
            // 
            // btnPesquisaPessoa
            // 
            this.btnPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaPessoa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaPessoa.Location = new System.Drawing.Point(126, 17);
            this.btnPesquisaPessoa.Name = "btnPesquisaPessoa";
            this.btnPesquisaPessoa.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaPessoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaPessoa.TabIndex = 739;
            this.btnPesquisaPessoa.TabStop = false;
            this.btnPesquisaPessoa.Click += new System.EventHandler(this.btnPesquisaPessoa_Click);
            // 
            // labelControl67
            // 
            this.labelControl67.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl67.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl67.Appearance.Options.UseFont = true;
            this.labelControl67.Appearance.Options.UseForeColor = true;
            this.labelControl67.Location = new System.Drawing.Point(3, 2);
            this.labelControl67.Name = "labelControl67";
            this.labelControl67.Size = new System.Drawing.Size(109, 13);
            this.labelControl67.TabIndex = 737;
            this.labelControl67.Text = "Código Funcionário";
            // 
            // txtIdPessoa
            // 
            this.txtIdPessoa.EnterMoveNextControl = true;
            this.txtIdPessoa.Location = new System.Drawing.Point(3, 18);
            this.txtIdPessoa.Name = "txtIdPessoa";
            this.txtIdPessoa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdPessoa.Properties.Appearance.Options.UseFont = true;
            this.txtIdPessoa.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIdPessoa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIdPessoa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtIdPessoa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtIdPessoa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIdPessoa.Properties.MaxLength = 10;
            this.txtIdPessoa.Size = new System.Drawing.Size(117, 22);
            this.txtIdPessoa.TabIndex = 1;
            this.txtIdPessoa.EditValueChanged += new System.EventHandler(this.txtIdPessoa_EditValueChanged);
            this.txtIdPessoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdPessoa_KeyDown);
            this.txtIdPessoa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSomenteNumeros_KeyPress);
            this.txtIdPessoa.Leave += new System.EventHandler(this.txtIdPessoa_Leave);
            // 
            // labelControl66
            // 
            this.labelControl66.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl66.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl66.Appearance.Options.UseFont = true;
            this.labelControl66.Appearance.Options.UseForeColor = true;
            this.labelControl66.Location = new System.Drawing.Point(154, 2);
            this.labelControl66.Name = "labelControl66";
            this.labelControl66.Size = new System.Drawing.Size(101, 13);
            this.labelControl66.TabIndex = 738;
            this.labelControl66.Text = "Nome Funcionário";
            // 
            // btnPesquisaPedidoVendas
            // 
            this.btnPesquisaPedidoVendas.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPedidoVendas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaPedidoVendas.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaPedidoVendas.Location = new System.Drawing.Point(492, 60);
            this.btnPesquisaPedidoVendas.Name = "btnPesquisaPedidoVendas";
            this.btnPesquisaPedidoVendas.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaPedidoVendas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaPedidoVendas.TabIndex = 768;
            this.btnPesquisaPedidoVendas.TabStop = false;
            this.btnPesquisaPedidoVendas.Click += new System.EventHandler(this.btnPesquisaPedidoVendas_Click);
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Appearance.Options.UseForeColor = true;
            this.labelControl10.Location = new System.Drawing.Point(360, 45);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(124, 13);
            this.labelControl10.TabIndex = 767;
            this.labelControl10.Text = "Nr. Pedido de Vendas";
            // 
            // txtNumeroPedidoVendas
            // 
            this.txtNumeroPedidoVendas.EnterMoveNextControl = true;
            this.txtNumeroPedidoVendas.Location = new System.Drawing.Point(360, 60);
            this.txtNumeroPedidoVendas.Name = "txtNumeroPedidoVendas";
            this.txtNumeroPedidoVendas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroPedidoVendas.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroPedidoVendas.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNumeroPedidoVendas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNumeroPedidoVendas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroPedidoVendas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroPedidoVendas.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumeroPedidoVendas.Properties.Mask.EditMask = "([0-9]{1})(([\\.][0-9]{2})(([\\.][0-9]{3})([\\.][0-9]{4})?)?)?";
            this.txtNumeroPedidoVendas.Properties.MaxLength = 50;
            this.txtNumeroPedidoVendas.Size = new System.Drawing.Size(126, 22);
            this.txtNumeroPedidoVendas.TabIndex = 7;
            this.txtNumeroPedidoVendas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumeroPedidoVendas_KeyDown);
            // 
            // txtDataConclusao
            // 
            this.txtDataConclusao.EditValue = "";
            this.txtDataConclusao.Enabled = false;
            this.txtDataConclusao.EnterMoveNextControl = true;
            this.txtDataConclusao.Location = new System.Drawing.Point(256, 60);
            this.txtDataConclusao.Name = "txtDataConclusao";
            this.txtDataConclusao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataConclusao.Properties.Appearance.Options.UseFont = true;
            this.txtDataConclusao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataConclusao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataConclusao.Properties.ReadOnly = true;
            this.txtDataConclusao.Size = new System.Drawing.Size(96, 22);
            this.txtDataConclusao.TabIndex = 6;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(258, 44);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(90, 13);
            this.labelControl5.TabIndex = 10037;
            this.labelControl5.Text = "Data Conclusão";
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Appearance.Options.UseFont = true;
            this.labDataCadastro.Location = new System.Drawing.Point(152, 45);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(72, 13);
            this.labDataCadastro.TabIndex = 10036;
            this.labDataCadastro.Text = "Data Roteiro";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnSalvarRoteiro);
            this.flowLayoutPanel2.Controls.Add(this.btnImprimirRelatorio);
            this.flowLayoutPanel2.Controls.Add(this.btnDetalhes);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(547, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(472, 40);
            this.flowLayoutPanel2.TabIndex = 1050;
            // 
            // btnSalvarRoteiro
            // 
            this.btnSalvarRoteiro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvarRoteiro.FlatAppearance.BorderSize = 0;
            this.btnSalvarRoteiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvarRoteiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvarRoteiro.Image = global::Programax.Easy.View.Properties.Resources.icone_baixar;
            this.btnSalvarRoteiro.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSalvarRoteiro.Location = new System.Drawing.Point(352, 0);
            this.btnSalvarRoteiro.Margin = new System.Windows.Forms.Padding(0);
            this.btnSalvarRoteiro.Name = "btnSalvarRoteiro";
            this.btnSalvarRoteiro.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSalvarRoteiro.Size = new System.Drawing.Size(120, 40);
            this.btnSalvarRoteiro.TabIndex = 1052;
            this.btnSalvarRoteiro.Text = " Salvar";
            this.btnSalvarRoteiro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvarRoteiro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalvarRoteiro.UseVisualStyleBackColor = true;
            this.btnSalvarRoteiro.Click += new System.EventHandler(this.btnSalvarRoteiro_Click);
            // 
            // btnImprimirRelatorio
            // 
            this.btnImprimirRelatorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimirRelatorio.FlatAppearance.BorderSize = 0;
            this.btnImprimirRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirRelatorio.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnImprimirRelatorio.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImprimirRelatorio.Location = new System.Drawing.Point(251, 0);
            this.btnImprimirRelatorio.Margin = new System.Windows.Forms.Padding(0);
            this.btnImprimirRelatorio.Name = "btnImprimirRelatorio";
            this.btnImprimirRelatorio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnImprimirRelatorio.Size = new System.Drawing.Size(101, 40);
            this.btnImprimirRelatorio.TabIndex = 1051;
            this.btnImprimirRelatorio.Text = " Imprimir";
            this.btnImprimirRelatorio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimirRelatorio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimirRelatorio.UseVisualStyleBackColor = true;
            this.btnImprimirRelatorio.Click += new System.EventHandler(this.btnImprimirRelatorio_Click);
            // 
            // btnDetalhes
            // 
            this.btnDetalhes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetalhes.FlatAppearance.BorderSize = 0;
            this.btnDetalhes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetalhes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetalhes.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnDetalhes.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDetalhes.Location = new System.Drawing.Point(97, 0);
            this.btnDetalhes.Margin = new System.Windows.Forms.Padding(0);
            this.btnDetalhes.Name = "btnDetalhes";
            this.btnDetalhes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDetalhes.Size = new System.Drawing.Size(154, 40);
            this.btnDetalhes.TabIndex = 1053;
            this.btnDetalhes.Text = " Imprimir Detalhes";
            this.btnDetalhes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetalhes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDetalhes.UseVisualStyleBackColor = true;
            this.btnDetalhes.Click += new System.EventHandler(this.btnDetalhes_Click);
            // 
            // btnEditarPedido
            // 
            this.btnEditarPedido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditarPedido.FlatAppearance.BorderSize = 0;
            this.btnEditarPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarPedido.Image = global::Programax.Easy.View.Properties.Resources.icon_edit;
            this.btnEditarPedido.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnEditarPedido.Location = new System.Drawing.Point(410, 0);
            this.btnEditarPedido.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditarPedido.Name = "btnEditarPedido";
            this.btnEditarPedido.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnEditarPedido.Size = new System.Drawing.Size(95, 40);
            this.btnEditarPedido.TabIndex = 1051;
            this.btnEditarPedido.Text = " Pedido";
            this.btnEditarPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditarPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditarPedido.UseVisualStyleBackColor = true;
            this.btnEditarPedido.Visible = false;
            this.btnEditarPedido.Click += new System.EventHandler(this.btnEditarPedido_Click);
            // 
            // txtCodigoRoteiro
            // 
            this.txtCodigoRoteiro.EnterMoveNextControl = true;
            this.txtCodigoRoteiro.Location = new System.Drawing.Point(3, 60);
            this.txtCodigoRoteiro.Name = "txtCodigoRoteiro";
            this.txtCodigoRoteiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoRoteiro.Properties.Appearance.Options.UseFont = true;
            this.txtCodigoRoteiro.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCodigoRoteiro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCodigoRoteiro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCodigoRoteiro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCodigoRoteiro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCodigoRoteiro.Properties.Mask.EditMask = "([0-9]{1})(([\\.][0-9]{2})(([\\.][0-9]{3})([\\.][0-9]{4})?)?)?";
            this.txtCodigoRoteiro.Properties.MaxLength = 50;
            this.txtCodigoRoteiro.Properties.ReadOnly = true;
            this.txtCodigoRoteiro.Size = new System.Drawing.Size(140, 22);
            this.txtCodigoRoteiro.TabIndex = 10064;
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl15.Appearance.Options.UseFont = true;
            this.labelControl15.Appearance.Options.UseForeColor = true;
            this.labelControl15.Location = new System.Drawing.Point(5, 43);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(84, 13);
            this.labelControl15.TabIndex = 10065;
            this.labelControl15.Text = "Código Roteiro";
            // 
            // btnAddAgendaDoDia
            // 
            this.btnAddAgendaDoDia.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAddAgendaDoDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddAgendaDoDia.FlatAppearance.BorderSize = 0;
            this.btnAddAgendaDoDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAgendaDoDia.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddAgendaDoDia.Location = new System.Drawing.Point(562, 57);
            this.btnAddAgendaDoDia.Name = "btnAddAgendaDoDia";
            this.btnAddAgendaDoDia.Size = new System.Drawing.Size(106, 24);
            this.btnAddAgendaDoDia.TabIndex = 10066;
            this.btnAddAgendaDoDia.Text = "  Agendas do Dia";
            this.btnAddAgendaDoDia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddAgendaDoDia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddAgendaDoDia.UseVisualStyleBackColor = false;
            this.btnAddAgendaDoDia.Click += new System.EventHandler(this.btnAddAgendaDoDia_Click);
            // 
            // btnAdicionarRoteiro
            // 
            this.btnAdicionarRoteiro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarRoteiro.FlatAppearance.BorderSize = 0;
            this.btnAdicionarRoteiro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarRoteiro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarRoteiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarRoteiro.Image = global::Programax.Easy.View.Properties.Resources.icone_adicionar_branco;
            this.btnAdicionarRoteiro.Location = new System.Drawing.Point(520, 55);
            this.btnAdicionarRoteiro.Name = "btnAdicionarRoteiro";
            this.btnAdicionarRoteiro.Size = new System.Drawing.Size(23, 23);
            this.btnAdicionarRoteiro.TabIndex = 10071;
            this.btnAdicionarRoteiro.TabStop = false;
            this.btnAdicionarRoteiro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdicionarRoteiro.UseVisualStyleBackColor = true;
            this.btnAdicionarRoteiro.Click += new System.EventHandler(this.btnAdicionarRoteiro_Click);
            // 
            // txtDataRoteiro
            // 
            this.txtDataRoteiro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtDataRoteiro.EditValue = "";
            this.txtDataRoteiro.EnterMoveNextControl = true;
            this.txtDataRoteiro.Location = new System.Drawing.Point(152, 60);
            this.txtDataRoteiro.Name = "txtDataRoteiro";
            this.txtDataRoteiro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataRoteiro.Properties.Appearance.Options.UseFont = true;
            this.txtDataRoteiro.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataRoteiro.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataRoteiro.Size = new System.Drawing.Size(97, 22);
            this.txtDataRoteiro.TabIndex = 10072;
            // 
            // FormCadastroRoteirizacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 569);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "FormCadastroRoteirizacao";
            this.NomeDaTela = "Cadastro de Roteiros";
            this.Text = "Cadastro de Roteiros";
            this.Load += new System.EventHandler(this.FormRoteiroPesquisa_Load);
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRoteiros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRoteiro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNomePessoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPessoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdPessoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPedidoVendas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPedidoVendas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataConclusao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataConclusao.Properties)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoRoteiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataRoteiro.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataRoteiro.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRoteiro;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCliente;
        private DevExpress.XtraEditors.TextEdit txtNomePessoa;
        private System.Windows.Forms.PictureBox btnPesquisaPessoa;
        private DevExpress.XtraEditors.LabelControl labelControl67;
        private DevExpress.XtraEditors.TextEdit txtIdPessoa;
        private DevExpress.XtraEditors.LabelControl labelControl66;
        private System.Windows.Forms.PictureBox btnPesquisaPedidoVendas;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtNumeroPedidoVendas;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.DateEdit txtDataConclusao;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraGrid.Columns.GridColumn colunaPedido;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataElaboracao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataConclusao;
        private System.Windows.Forms.Button btnExcluirRoteiro;
        public AkilGridControl gcRoteiros;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaMarcador;
        private System.Windows.Forms.Button btnImprimirRelatorio;
        private System.Windows.Forms.Button btnSalvarRoteiro;
        private DevExpress.XtraGrid.Columns.GridColumn colunaEndereco;
        private DevExpress.XtraGrid.Columns.GridColumn colunaPeriodo;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private System.Windows.Forms.Button btnEditarPedido;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaLigarFone;
        private DevExpress.XtraGrid.Columns.GridColumn colunaObservacao;
        private DevExpress.XtraEditors.TextEdit txtCodigoRoteiro;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private System.Windows.Forms.Button btnAddAgendaDoDia;
        private System.Windows.Forms.Button btnAdicionarRoteiro;
        private System.Windows.Forms.Button btnDetalhes;
        private DevExpress.XtraEditors.DateEdit txtDataRoteiro;
    }
}