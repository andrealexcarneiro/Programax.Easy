namespace Programax.Easy.View.Telas.Cadastros.Pessoas
{
    partial class UcPessoaPesquisa
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
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.colunCpfCnpj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaNomeRazao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNomeFantasia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaUf = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaCidade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaEhCliente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaEhFornecedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaEhFuncionario = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaEhTransportadora = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaEhVendedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.conulaEhAtendente = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaEhSupervisor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaIndicador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPessoas = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkEhFornecedor = new DevExpress.XtraEditors.CheckEdit();
            this.pbPesquisaCadastro = new System.Windows.Forms.PictureBox();
            this.chkEhCliente = new DevExpress.XtraEditors.CheckEdit();
            this.cboCidadeEndereco = new DevExpress.XtraEditors.LookUpEdit();
            this.cboEstadoEndereco = new DevExpress.XtraEditors.LookUpEdit();
            this.labCidade = new DevExpress.XtraEditors.LabelControl();
            this.labEstado = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.lblDescricao = new DevExpress.XtraEditors.LabelControl();
            this.cboFiltro = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.chkEhFuncionario = new DevExpress.XtraEditors.CheckEdit();
            this.chkEhVendedor = new DevExpress.XtraEditors.CheckEdit();
            this.cboTipoEndereco = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkEhIndicador = new DevExpress.XtraEditors.CheckEdit();
            this.chkEhTransportadora = new DevExpress.XtraEditors.CheckEdit();
            this.chkEhAtendente = new DevExpress.XtraEditors.CheckEdit();
            this.chkEhSupervisor = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPessoas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhFornecedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhCliente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCidadeEndereco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstadoEndereco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFiltro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhFuncionario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhVendedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoEndereco.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhIndicador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhTransportadora.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhAtendente.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhSupervisor.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(306, 17);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DropDownRows = 3;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Size = new System.Drawing.Size(148, 20);
            this.cboStatus.TabIndex = 459;
            // 
            // colunCpfCnpj
            // 
            this.colunCpfCnpj.Caption = "Cpf/Cnpj";
            this.colunCpfCnpj.FieldName = "CpfCnpj";
            this.colunCpfCnpj.Name = "colunCpfCnpj";
            this.colunCpfCnpj.OptionsColumn.AllowEdit = false;
            this.colunCpfCnpj.OptionsColumn.AllowFocus = false;
            this.colunCpfCnpj.OptionsFilter.AllowFilter = false;
            this.colunCpfCnpj.Visible = true;
            this.colunCpfCnpj.VisibleIndex = 1;
            this.colunCpfCnpj.Width = 61;
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
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colunaId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaId.Visible = true;
            this.colunaId.VisibleIndex = 0;
            this.colunaId.Width = 45;
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
            this.gridView5.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridView5.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gridView5.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridView5.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunCpfCnpj,
            this.colunaNomeRazao,
            this.colunaNomeFantasia,
            this.colunaUf,
            this.colunaCidade,
            this.gridStatus,
            this.colunaEhCliente,
            this.colunaEhFornecedor,
            this.colunaEhFuncionario,
            this.colunaEhTransportadora,
            this.colunaEhVendedor,
            this.conulaEhAtendente,
            this.colunaEhSupervisor,
            this.colunaIndicador});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcPessoas;
            this.gridView5.GroupPanelText = "Arraste as colunas para agrupar as colunas";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Parceiros";
            // 
            // colunaNomeRazao
            // 
            this.colunaNomeRazao.AppearanceCell.Options.UseTextOptions = true;
            this.colunaNomeRazao.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaNomeRazao.Caption = "Nome/Razão Social";
            this.colunaNomeRazao.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaNomeRazao.FieldName = "RazaoSocial";
            this.colunaNomeRazao.MinWidth = 10;
            this.colunaNomeRazao.Name = "colunaNomeRazao";
            this.colunaNomeRazao.OptionsColumn.AllowEdit = false;
            this.colunaNomeRazao.OptionsColumn.AllowFocus = false;
            this.colunaNomeRazao.OptionsFilter.AllowFilter = false;
            this.colunaNomeRazao.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaNomeRazao.Visible = true;
            this.colunaNomeRazao.VisibleIndex = 2;
            this.colunaNomeRazao.Width = 97;
            // 
            // colunaNomeFantasia
            // 
            this.colunaNomeFantasia.Caption = "Nome Fantasia";
            this.colunaNomeFantasia.FieldName = "NomeFantasia";
            this.colunaNomeFantasia.Name = "colunaNomeFantasia";
            this.colunaNomeFantasia.OptionsColumn.AllowEdit = false;
            this.colunaNomeFantasia.OptionsColumn.AllowFocus = false;
            this.colunaNomeFantasia.OptionsFilter.AllowFilter = false;
            this.colunaNomeFantasia.Visible = true;
            this.colunaNomeFantasia.VisibleIndex = 3;
            this.colunaNomeFantasia.Width = 80;
            // 
            // colunaUf
            // 
            this.colunaUf.Caption = "UF";
            this.colunaUf.FieldName = "UF";
            this.colunaUf.Name = "colunaUf";
            this.colunaUf.OptionsColumn.AllowEdit = false;
            this.colunaUf.OptionsColumn.AllowFocus = false;
            this.colunaUf.OptionsFilter.AllowAutoFilter = false;
            this.colunaUf.OptionsFilter.AllowFilter = false;
            this.colunaUf.Visible = true;
            this.colunaUf.VisibleIndex = 4;
            this.colunaUf.Width = 20;
            // 
            // colunaCidade
            // 
            this.colunaCidade.Caption = "Cidade";
            this.colunaCidade.FieldName = "Cidade";
            this.colunaCidade.Name = "colunaCidade";
            this.colunaCidade.OptionsColumn.AllowEdit = false;
            this.colunaCidade.OptionsColumn.AllowFocus = false;
            this.colunaCidade.OptionsFilter.AllowAutoFilter = false;
            this.colunaCidade.OptionsFilter.AllowFilter = false;
            this.colunaCidade.Visible = true;
            this.colunaCidade.VisibleIndex = 5;
            this.colunaCidade.Width = 42;
            // 
            // gridStatus
            // 
            this.gridStatus.Caption = "Status";
            this.gridStatus.FieldName = "Status";
            this.gridStatus.Name = "gridStatus";
            this.gridStatus.OptionsColumn.AllowEdit = false;
            this.gridStatus.OptionsColumn.AllowFocus = false;
            this.gridStatus.OptionsFilter.AllowAutoFilter = false;
            this.gridStatus.OptionsFilter.AllowFilter = false;
            this.gridStatus.Visible = true;
            this.gridStatus.VisibleIndex = 6;
            this.gridStatus.Width = 32;
            // 
            // colunaEhCliente
            // 
            this.colunaEhCliente.Caption = "Cliente";
            this.colunaEhCliente.FieldName = "EhCliente";
            this.colunaEhCliente.Name = "colunaEhCliente";
            this.colunaEhCliente.OptionsColumn.AllowEdit = false;
            this.colunaEhCliente.OptionsColumn.AllowFocus = false;
            this.colunaEhCliente.OptionsFilter.AllowAutoFilter = false;
            this.colunaEhCliente.OptionsFilter.AllowFilter = false;
            this.colunaEhCliente.Visible = true;
            this.colunaEhCliente.VisibleIndex = 7;
            this.colunaEhCliente.Width = 30;
            // 
            // colunaEhFornecedor
            // 
            this.colunaEhFornecedor.Caption = "Fornecedor";
            this.colunaEhFornecedor.FieldName = "EhFornecedor";
            this.colunaEhFornecedor.Name = "colunaEhFornecedor";
            this.colunaEhFornecedor.OptionsColumn.AllowEdit = false;
            this.colunaEhFornecedor.OptionsColumn.AllowFocus = false;
            this.colunaEhFornecedor.OptionsFilter.AllowAutoFilter = false;
            this.colunaEhFornecedor.OptionsFilter.AllowFilter = false;
            this.colunaEhFornecedor.Visible = true;
            this.colunaEhFornecedor.VisibleIndex = 8;
            this.colunaEhFornecedor.Width = 49;
            // 
            // colunaEhFuncionario
            // 
            this.colunaEhFuncionario.Caption = "Funcionário";
            this.colunaEhFuncionario.FieldName = "EhFuncionario";
            this.colunaEhFuncionario.Name = "colunaEhFuncionario";
            this.colunaEhFuncionario.OptionsColumn.AllowEdit = false;
            this.colunaEhFuncionario.OptionsColumn.AllowFocus = false;
            this.colunaEhFuncionario.OptionsFilter.AllowFilter = false;
            this.colunaEhFuncionario.Visible = true;
            this.colunaEhFuncionario.VisibleIndex = 9;
            this.colunaEhFuncionario.Width = 51;
            // 
            // colunaEhTransportadora
            // 
            this.colunaEhTransportadora.Caption = "Transportadora";
            this.colunaEhTransportadora.FieldName = "EhTransportadora";
            this.colunaEhTransportadora.Name = "colunaEhTransportadora";
            this.colunaEhTransportadora.OptionsColumn.AllowEdit = false;
            this.colunaEhTransportadora.OptionsColumn.AllowFocus = false;
            this.colunaEhTransportadora.OptionsFilter.AllowFilter = false;
            this.colunaEhTransportadora.Visible = true;
            this.colunaEhTransportadora.VisibleIndex = 10;
            this.colunaEhTransportadora.Width = 61;
            // 
            // colunaEhVendedor
            // 
            this.colunaEhVendedor.Caption = "Vendedor";
            this.colunaEhVendedor.FieldName = "EhVendedor";
            this.colunaEhVendedor.Name = "colunaEhVendedor";
            this.colunaEhVendedor.OptionsColumn.AllowEdit = false;
            this.colunaEhVendedor.OptionsColumn.AllowFocus = false;
            this.colunaEhVendedor.OptionsFilter.AllowFilter = false;
            this.colunaEhVendedor.Visible = true;
            this.colunaEhVendedor.VisibleIndex = 11;
            this.colunaEhVendedor.Width = 43;
            // 
            // conulaEhAtendente
            // 
            this.conulaEhAtendente.Caption = "Atendente";
            this.conulaEhAtendente.FieldName = "EhAtendente";
            this.conulaEhAtendente.Name = "conulaEhAtendente";
            this.conulaEhAtendente.OptionsColumn.AllowEdit = false;
            this.conulaEhAtendente.OptionsColumn.AllowFocus = false;
            this.conulaEhAtendente.OptionsFilter.AllowFilter = false;
            this.conulaEhAtendente.Visible = true;
            this.conulaEhAtendente.VisibleIndex = 12;
            this.conulaEhAtendente.Width = 45;
            // 
            // colunaEhSupervisor
            // 
            this.colunaEhSupervisor.Caption = "Supervisor";
            this.colunaEhSupervisor.FieldName = "EhSupervisor";
            this.colunaEhSupervisor.Name = "colunaEhSupervisor";
            this.colunaEhSupervisor.OptionsColumn.AllowEdit = false;
            this.colunaEhSupervisor.OptionsColumn.AllowFocus = false;
            this.colunaEhSupervisor.OptionsFilter.AllowFilter = false;
            this.colunaEhSupervisor.Visible = true;
            this.colunaEhSupervisor.VisibleIndex = 13;
            this.colunaEhSupervisor.Width = 48;
            // 
            // colunaIndicador
            // 
            this.colunaIndicador.Caption = "Indicador";
            this.colunaIndicador.FieldName = "EhIndicador";
            this.colunaIndicador.Name = "colunaIndicador";
            this.colunaIndicador.OptionsColumn.AllowEdit = false;
            this.colunaIndicador.OptionsColumn.AllowFocus = false;
            this.colunaIndicador.OptionsFilter.AllowFilter = false;
            this.colunaIndicador.Visible = true;
            this.colunaIndicador.VisibleIndex = 14;
            this.colunaIndicador.Width = 38;
            // 
            // gcPessoas
            // 
            this.gcPessoas.CausesValidation = false;
            this.gcPessoas.Location = new System.Drawing.Point(0, 80);
            this.gcPessoas.MainView = this.gridView5;
            this.gcPessoas.Name = "gcPessoas";
            this.gcPessoas.Size = new System.Drawing.Size(1040, 225);
            this.gcPessoas.TabIndex = 471;
            this.gcPessoas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcPessoas.Click += new System.EventHandler(this.gcPessoas_Click);
            this.gcPessoas.DoubleClick += new System.EventHandler(this.gcPessoas_DoubleClick);
            this.gcPessoas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcPessoas_KeyDown);
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcPessoas;
            this.gridView2.Name = "gridView2";
            // 
            // chkEhFornecedor
            // 
            this.chkEhFornecedor.EditValue = true;
            this.chkEhFornecedor.EnterMoveNextControl = true;
            this.chkEhFornecedor.Location = new System.Drawing.Point(6, 43);
            this.chkEhFornecedor.Name = "chkEhFornecedor";
            this.chkEhFornecedor.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEhFornecedor.Properties.Appearance.Options.UseFont = true;
            this.chkEhFornecedor.Properties.Caption = "Fornecedores";
            this.chkEhFornecedor.Size = new System.Drawing.Size(87, 19);
            this.chkEhFornecedor.TabIndex = 458;
            // 
            // pbPesquisaCadastro
            // 
            this.pbPesquisaCadastro.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaCadastro.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaCadastro.Location = new System.Drawing.Point(1013, 50);
            this.pbPesquisaCadastro.Name = "pbPesquisaCadastro";
            this.pbPesquisaCadastro.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaCadastro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaCadastro.TabIndex = 469;
            this.pbPesquisaCadastro.TabStop = false;
            this.pbPesquisaCadastro.Click += new System.EventHandler(this.pbPesquisaCadastro_Click);
            // 
            // chkEhCliente
            // 
            this.chkEhCliente.EditValue = true;
            this.chkEhCliente.EnterMoveNextControl = true;
            this.chkEhCliente.Location = new System.Drawing.Point(6, 12);
            this.chkEhCliente.Name = "chkEhCliente";
            this.chkEhCliente.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEhCliente.Properties.Appearance.Options.UseFont = true;
            this.chkEhCliente.Properties.Caption = "Clientes";
            this.chkEhCliente.Size = new System.Drawing.Size(75, 19);
            this.chkEhCliente.TabIndex = 457;
            // 
            // cboCidadeEndereco
            // 
            this.cboCidadeEndereco.EnterMoveNextControl = true;
            this.cboCidadeEndereco.Location = new System.Drawing.Point(306, 54);
            this.cboCidadeEndereco.Name = "cboCidadeEndereco";
            this.cboCidadeEndereco.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCidadeEndereco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboCidadeEndereco.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCidadeEndereco.Properties.NullText = "";
            this.cboCidadeEndereco.Size = new System.Drawing.Size(147, 20);
            this.cboCidadeEndereco.TabIndex = 466;
            // 
            // cboEstadoEndereco
            // 
            this.cboEstadoEndereco.EnterMoveNextControl = true;
            this.cboEstadoEndereco.Location = new System.Drawing.Point(124, 54);
            this.cboEstadoEndereco.Name = "cboEstadoEndereco";
            this.cboEstadoEndereco.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEstadoEndereco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboEstadoEndereco.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UF", "UF", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nome", "Nome")});
            this.cboEstadoEndereco.Properties.NullText = "";
            this.cboEstadoEndereco.Size = new System.Drawing.Size(176, 20);
            this.cboEstadoEndereco.TabIndex = 465;
            this.cboEstadoEndereco.EditValueChanged += new System.EventHandler(this.cboEstadoEndereco_EditValueChanged);
            // 
            // labCidade
            // 
            this.labCidade.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCidade.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labCidade.Appearance.Options.UseFont = true;
            this.labCidade.Appearance.Options.UseForeColor = true;
            this.labCidade.Location = new System.Drawing.Point(306, 39);
            this.labCidade.Name = "labCidade";
            this.labCidade.Size = new System.Drawing.Size(33, 13);
            this.labCidade.TabIndex = 468;
            this.labCidade.Text = "Cidade";
            // 
            // labEstado
            // 
            this.labEstado.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEstado.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labEstado.Appearance.Options.UseFont = true;
            this.labEstado.Appearance.Options.UseForeColor = true;
            this.labEstado.Location = new System.Drawing.Point(124, 39);
            this.labEstado.Name = "labEstado";
            this.labEstado.Size = new System.Drawing.Size(33, 13);
            this.labEstado.TabIndex = 467;
            this.labEstado.Text = "Estado";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(124, 17);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "n0";
            this.txtDescricao.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDescricao.Properties.MaxLength = 80;
            this.txtDescricao.Properties.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_Properties_KeyUp);
            this.txtDescricao.Size = new System.Drawing.Size(176, 20);
            this.txtDescricao.TabIndex = 463;
            this.txtDescricao.EditValueChanged += new System.EventHandler(this.txtDescricao_EditValueChanged);
            this.txtDescricao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_KeyDown);
            this.txtDescricao.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_Properties_KeyUp);
            // 
            // lblDescricao
            // 
            this.lblDescricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricao.Appearance.Options.UseFont = true;
            this.lblDescricao.Location = new System.Drawing.Point(124, 2);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(48, 13);
            this.lblDescricao.TabIndex = 464;
            this.lblDescricao.Text = "Descrição";
            // 
            // cboFiltro
            // 
            this.cboFiltro.EnterMoveNextControl = true;
            this.cboFiltro.Location = new System.Drawing.Point(0, 17);
            this.cboFiltro.Name = "cboFiltro";
            this.cboFiltro.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFiltro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboFiltro.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descricao")});
            this.cboFiltro.Properties.DropDownRows = 5;
            this.cboFiltro.Properties.NullText = "";
            this.cboFiltro.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboFiltro.Size = new System.Drawing.Size(118, 20);
            this.cboFiltro.TabIndex = 461;
            this.cboFiltro.EditValueChanged += new System.EventHandler(this.cboFiltro_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(0, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 13);
            this.labelControl1.TabIndex = 462;
            this.labelControl1.Text = "Filtro";
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Appearance.Options.UseFont = true;
            this.labStatus.Location = new System.Drawing.Point(306, 2);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 460;
            this.labStatus.Text = "Status";
            // 
            // chkEhFuncionario
            // 
            this.chkEhFuncionario.EditValue = true;
            this.chkEhFuncionario.EnterMoveNextControl = true;
            this.chkEhFuncionario.Location = new System.Drawing.Point(124, 12);
            this.chkEhFuncionario.Name = "chkEhFuncionario";
            this.chkEhFuncionario.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEhFuncionario.Properties.Appearance.Options.UseFont = true;
            this.chkEhFuncionario.Properties.Caption = "Funcionários";
            this.chkEhFuncionario.Size = new System.Drawing.Size(87, 19);
            this.chkEhFuncionario.TabIndex = 472;
            // 
            // chkEhVendedor
            // 
            this.chkEhVendedor.EditValue = true;
            this.chkEhVendedor.EnterMoveNextControl = true;
            this.chkEhVendedor.Location = new System.Drawing.Point(274, 11);
            this.chkEhVendedor.Name = "chkEhVendedor";
            this.chkEhVendedor.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEhVendedor.Properties.Appearance.Options.UseFont = true;
            this.chkEhVendedor.Properties.Caption = "Vendedor";
            this.chkEhVendedor.Size = new System.Drawing.Size(100, 19);
            this.chkEhVendedor.TabIndex = 473;
            // 
            // cboTipoEndereco
            // 
            this.cboTipoEndereco.EnterMoveNextControl = true;
            this.cboTipoEndereco.Location = new System.Drawing.Point(0, 54);
            this.cboTipoEndereco.Name = "cboTipoEndereco";
            this.cboTipoEndereco.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipoEndereco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboTipoEndereco.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboTipoEndereco.Properties.DropDownRows = 6;
            this.cboTipoEndereco.Properties.NullText = "";
            this.cboTipoEndereco.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboTipoEndereco.Size = new System.Drawing.Size(118, 20);
            this.cboTipoEndereco.TabIndex = 476;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(0, 39);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 13);
            this.labelControl2.TabIndex = 477;
            this.labelControl2.Text = "Tipo Endereço";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEhIndicador);
            this.groupBox1.Controls.Add(this.chkEhTransportadora);
            this.groupBox1.Controls.Add(this.chkEhAtendente);
            this.groupBox1.Controls.Add(this.chkEhSupervisor);
            this.groupBox1.Controls.Add(this.chkEhCliente);
            this.groupBox1.Controls.Add(this.chkEhFornecedor);
            this.groupBox1.Controls.Add(this.chkEhFuncionario);
            this.groupBox1.Controls.Add(this.chkEhVendedor);
            this.groupBox1.Location = new System.Drawing.Point(497, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 68);
            this.groupBox1.TabIndex = 478;
            this.groupBox1.TabStop = false;
            // 
            // chkEhIndicador
            // 
            this.chkEhIndicador.EditValue = true;
            this.chkEhIndicador.EnterMoveNextControl = true;
            this.chkEhIndicador.Location = new System.Drawing.Point(404, 43);
            this.chkEhIndicador.Name = "chkEhIndicador";
            this.chkEhIndicador.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEhIndicador.Properties.Appearance.Options.UseFont = true;
            this.chkEhIndicador.Properties.Caption = "Indicador";
            this.chkEhIndicador.Size = new System.Drawing.Size(87, 19);
            this.chkEhIndicador.TabIndex = 477;
            // 
            // chkEhTransportadora
            // 
            this.chkEhTransportadora.EditValue = true;
            this.chkEhTransportadora.EnterMoveNextControl = true;
            this.chkEhTransportadora.Location = new System.Drawing.Point(124, 43);
            this.chkEhTransportadora.Name = "chkEhTransportadora";
            this.chkEhTransportadora.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEhTransportadora.Properties.Appearance.Options.UseFont = true;
            this.chkEhTransportadora.Properties.Caption = "Transportadoras";
            this.chkEhTransportadora.Size = new System.Drawing.Size(108, 19);
            this.chkEhTransportadora.TabIndex = 476;
            // 
            // chkEhAtendente
            // 
            this.chkEhAtendente.EditValue = true;
            this.chkEhAtendente.EnterMoveNextControl = true;
            this.chkEhAtendente.Location = new System.Drawing.Point(404, 12);
            this.chkEhAtendente.Name = "chkEhAtendente";
            this.chkEhAtendente.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEhAtendente.Properties.Appearance.Options.UseFont = true;
            this.chkEhAtendente.Properties.Caption = "Atendente";
            this.chkEhAtendente.Size = new System.Drawing.Size(87, 19);
            this.chkEhAtendente.TabIndex = 475;
            // 
            // chkEhSupervisor
            // 
            this.chkEhSupervisor.EditValue = true;
            this.chkEhSupervisor.EnterMoveNextControl = true;
            this.chkEhSupervisor.Location = new System.Drawing.Point(274, 43);
            this.chkEhSupervisor.Name = "chkEhSupervisor";
            this.chkEhSupervisor.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEhSupervisor.Properties.Appearance.Options.UseFont = true;
            this.chkEhSupervisor.Properties.Caption = "Supervisor";
            this.chkEhSupervisor.Size = new System.Drawing.Size(87, 19);
            this.chkEhSupervisor.TabIndex = 474;
            // 
            // UcPessoaPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboTipoEndereco);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.gcPessoas);
            this.Controls.Add(this.pbPesquisaCadastro);
            this.Controls.Add(this.cboCidadeEndereco);
            this.Controls.Add(this.cboEstadoEndereco);
            this.Controls.Add(this.labCidade);
            this.Controls.Add(this.labEstado);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.cboFiltro);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labStatus);
            this.Name = "UcPessoaPesquisa";
            this.Size = new System.Drawing.Size(1040, 308);
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPessoas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhFornecedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaCadastro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhCliente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCidadeEndereco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEstadoEndereco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFiltro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhFuncionario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhVendedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoEndereco.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkEhIndicador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhTransportadora.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhAtendente.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEhSupervisor.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colunCpfCnpj;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeRazao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNomeFantasia;
        private DevExpress.XtraGrid.Columns.GridColumn colunaUf;
        private DevExpress.XtraGrid.Columns.GridColumn colunaCidade;
        private DevExpress.XtraGrid.Columns.GridColumn gridStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colunaEhCliente;
        private DevExpress.XtraGrid.Columns.GridColumn colunaEhFornecedor;
        private DevExpress.XtraGrid.GridControl gcPessoas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.PictureBox pbPesquisaCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboCidadeEndereco;
        private DevExpress.XtraEditors.LookUpEdit cboEstadoEndereco;
        private DevExpress.XtraEditors.LabelControl labCidade;
        private DevExpress.XtraEditors.LabelControl labEstado;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl lblDescricao;
        private DevExpress.XtraEditors.LookUpEdit cboFiltro;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colunaEhFuncionario;
        private DevExpress.XtraGrid.Columns.GridColumn colunaEhVendedor;
        public DevExpress.XtraEditors.CheckEdit chkEhFornecedor;
        public DevExpress.XtraEditors.CheckEdit chkEhCliente;
        public DevExpress.XtraEditors.CheckEdit chkEhFuncionario;
        public DevExpress.XtraEditors.CheckEdit chkEhVendedor;
        private DevExpress.XtraEditors.LookUpEdit cboTipoEndereco;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.CheckEdit chkEhAtendente;
        public DevExpress.XtraEditors.CheckEdit chkEhSupervisor;
        public DevExpress.XtraEditors.CheckEdit chkEhTransportadora;
        private DevExpress.XtraGrid.Columns.GridColumn colunaEhTransportadora;
        private DevExpress.XtraGrid.Columns.GridColumn colunaEhSupervisor;
        private DevExpress.XtraGrid.Columns.GridColumn conulaEhAtendente;
        public DevExpress.XtraEditors.CheckEdit chkEhIndicador;
        private DevExpress.XtraGrid.Columns.GridColumn colunaIndicador;
        public DevExpress.XtraEditors.LookUpEdit cboStatus;
    }
}
