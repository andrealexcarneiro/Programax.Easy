namespace Programax.Easy.View.Telas.Fiscal.InutilizacaoDeNumeracaoDeNota
{
    partial class FormCadastroInutilizacaoNumeroDeNota
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
            this.txtJustificativaCancelamento = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroInicial = new DevExpress.XtraEditors.TextEdit();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroFinal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAno = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labTipoInscricao = new DevExpress.XtraEditors.LabelControl();
            this.cboModeloNotaFiscal = new DevExpress.XtraEditors.LookUpEdit();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnInutilizarNumeracao = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.txtSerie = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gcInutilizacoes = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaAno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaModelo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaSerie = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumeroInicial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumeroFinal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaProtocolo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaJustificativa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.imgLinhaDeCima = new System.Windows.Forms.PictureBox();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtJustificativaCancelamento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboModeloNotaFiscal.Properties)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerie.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInutilizacoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLinhaDeCima)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel2);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.imgLinhaDeCima);
            this.panelConteudo.Controls.Add(this.gcInutilizacoes);
            this.panelConteudo.Controls.Add(this.txtSerie);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.labTipoInscricao);
            this.panelConteudo.Controls.Add(this.cboModeloNotaFiscal);
            this.panelConteudo.Controls.Add(this.txtAno);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.txtNumeroFinal);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.txtNumeroInicial);
            this.panelConteudo.Controls.Add(this.labCodigo);
            this.panelConteudo.Controls.Add(this.txtJustificativaCancelamento);
            this.panelConteudo.Controls.Add(this.labelControl6);
            this.panelConteudo.Size = new System.Drawing.Size(715, 386);
            // 
            // txtJustificativaCancelamento
            // 
            this.txtJustificativaCancelamento.Location = new System.Drawing.Point(3, 71);
            this.txtJustificativaCancelamento.Name = "txtJustificativaCancelamento";
            this.txtJustificativaCancelamento.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJustificativaCancelamento.Properties.Appearance.Options.UseFont = true;
            this.txtJustificativaCancelamento.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtJustificativaCancelamento.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtJustificativaCancelamento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJustificativaCancelamento.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtJustificativaCancelamento.Properties.MaxLength = 2000;
            this.txtJustificativaCancelamento.Size = new System.Drawing.Size(709, 105);
            this.txtJustificativaCancelamento.TabIndex = 6;
            this.txtJustificativaCancelamento.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.txtJustificativaCancelamento_EditValueChanging);
            this.txtJustificativaCancelamento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtJustificativaCancelamento_KeyPress);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(3, 52);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(111, 13);
            this.labelControl6.TabIndex = 10017;
            this.labelControl6.Text = "Justificativa Inutilização";
            // 
            // txtNumeroInicial
            // 
            this.txtNumeroInicial.EnterMoveNextControl = true;
            this.txtNumeroInicial.Location = new System.Drawing.Point(480, 18);
            this.txtNumeroInicial.Name = "txtNumeroInicial";
            this.txtNumeroInicial.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroInicial.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroInicial.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNumeroInicial.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNumeroInicial.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroInicial.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroInicial.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumeroInicial.Properties.Mask.EditMask = "[0-9]{1,11}";
            this.txtNumeroInicial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNumeroInicial.Properties.MaxLength = 8;
            this.txtNumeroInicial.Size = new System.Drawing.Size(113, 22);
            this.txtNumeroInicial.TabIndex = 4;
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Location = new System.Drawing.Point(480, 3);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(44, 13);
            this.labCodigo.TabIndex = 10031;
            this.labCodigo.Text = "Nr. Inicial";
            // 
            // txtNumeroFinal
            // 
            this.txtNumeroFinal.EnterMoveNextControl = true;
            this.txtNumeroFinal.Location = new System.Drawing.Point(599, 18);
            this.txtNumeroFinal.Name = "txtNumeroFinal";
            this.txtNumeroFinal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroFinal.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroFinal.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNumeroFinal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNumeroFinal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroFinal.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroFinal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumeroFinal.Properties.Mask.EditMask = "[0-9]{1,11}";
            this.txtNumeroFinal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNumeroFinal.Properties.MaxLength = 8;
            this.txtNumeroFinal.Size = new System.Drawing.Size(113, 22);
            this.txtNumeroFinal.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(599, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 13);
            this.labelControl1.TabIndex = 10033;
            this.labelControl1.Text = "Nr. Final";
            // 
            // txtAno
            // 
            this.txtAno.EnterMoveNextControl = true;
            this.txtAno.Location = new System.Drawing.Point(3, 18);
            this.txtAno.Name = "txtAno";
            this.txtAno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAno.Properties.Appearance.Options.UseFont = true;
            this.txtAno.Properties.Appearance.Options.UseTextOptions = true;
            this.txtAno.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtAno.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtAno.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtAno.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAno.Properties.Mask.EditMask = "[0-9]{1,2}";
            this.txtAno.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtAno.Properties.MaxLength = 8;
            this.txtAno.Size = new System.Drawing.Size(111, 22);
            this.txtAno.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(3, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(19, 13);
            this.labelControl2.TabIndex = 10035;
            this.labelControl2.Text = "Ano";
            // 
            // labTipoInscricao
            // 
            this.labTipoInscricao.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTipoInscricao.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labTipoInscricao.Location = new System.Drawing.Point(120, 3);
            this.labTipoInscricao.Name = "labTipoInscricao";
            this.labTipoInscricao.Size = new System.Drawing.Size(91, 13);
            this.labTipoInscricao.TabIndex = 10037;
            this.labTipoInscricao.Text = "Modelo Nota Fiscal";
            // 
            // cboModeloNotaFiscal
            // 
            this.cboModeloNotaFiscal.EnterMoveNextControl = true;
            this.cboModeloNotaFiscal.Location = new System.Drawing.Point(120, 18);
            this.cboModeloNotaFiscal.Name = "cboModeloNotaFiscal";
            this.cboModeloNotaFiscal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboModeloNotaFiscal.Properties.Appearance.Options.UseFont = true;
            this.cboModeloNotaFiscal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboModeloNotaFiscal.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboModeloNotaFiscal.Properties.DropDownRows = 5;
            this.cboModeloNotaFiscal.Properties.NullText = "";
            this.cboModeloNotaFiscal.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboModeloNotaFiscal.Size = new System.Drawing.Size(235, 22);
            this.cboModeloNotaFiscal.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnInutilizarNumeracao);
            this.flowLayoutPanel2.Controls.Add(this.btnSair);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 1);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(742, 41);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // btnInutilizarNumeracao
            // 
            this.btnInutilizarNumeracao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInutilizarNumeracao.FlatAppearance.BorderSize = 0;
            this.btnInutilizarNumeracao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInutilizarNumeracao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInutilizarNumeracao.Image = global::Programax.Easy.View.Properties.Resources.icone_enviar_nfe;
            this.btnInutilizarNumeracao.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnInutilizarNumeracao.Location = new System.Drawing.Point(0, 0);
            this.btnInutilizarNumeracao.Margin = new System.Windows.Forms.Padding(0);
            this.btnInutilizarNumeracao.Name = "btnInutilizarNumeracao";
            this.btnInutilizarNumeracao.Size = new System.Drawing.Size(128, 40);
            this.btnInutilizarNumeracao.TabIndex = 10038;
            this.btnInutilizarNumeracao.Text = "Inutilizar Numeração";
            this.btnInutilizarNumeracao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInutilizarNumeracao.UseVisualStyleBackColor = true;
            this.btnInutilizarNumeracao.Click += new System.EventHandler(this.btnInutilizarNumeracao_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(128, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10039;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // txtSerie
            // 
            this.txtSerie.EnterMoveNextControl = true;
            this.txtSerie.Location = new System.Drawing.Point(361, 18);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerie.Properties.Appearance.Options.UseFont = true;
            this.txtSerie.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSerie.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSerie.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSerie.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSerie.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtSerie.Properties.Mask.EditMask = "[0-9]{1,11}";
            this.txtSerie.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtSerie.Properties.MaxLength = 8;
            this.txtSerie.Size = new System.Drawing.Size(113, 22);
            this.txtSerie.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(361, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 13);
            this.labelControl3.TabIndex = 10039;
            this.labelControl3.Text = "Série";
            // 
            // gcInutilizacoes
            // 
            this.gcInutilizacoes.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcInutilizacoes.Location = new System.Drawing.Point(3, 204);
            this.gcInutilizacoes.MainView = this.gridView5;
            this.gcInutilizacoes.Name = "gcInutilizacoes";
            this.gcInutilizacoes.Size = new System.Drawing.Size(709, 180);
            this.gcInutilizacoes.TabIndex = 10060;
            this.gcInutilizacoes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
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
            this.gridView5.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridView5.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gridView5.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView5.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunaAno,
            this.colunaModelo,
            this.colunaSerie,
            this.colunaNumeroInicial,
            this.colunaNumeroFinal,
            this.colunaProtocolo,
            this.colunaJustificativa});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcInutilizacoes;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.ViewCaption = "Faixas de Numerações Inutilizadas";
            // 
            // colunaId
            // 
            this.colunaId.Caption = "Id";
            this.colunaId.FieldName = "Id";
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            // 
            // colunaAno
            // 
            this.colunaAno.AppearanceCell.Options.UseTextOptions = true;
            this.colunaAno.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaAno.Caption = "Ano";
            this.colunaAno.DisplayFormat.FormatString = "00";
            this.colunaAno.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaAno.FieldName = "Ano";
            this.colunaAno.MinWidth = 10;
            this.colunaAno.Name = "colunaAno";
            this.colunaAno.OptionsColumn.AllowEdit = false;
            this.colunaAno.OptionsColumn.AllowFocus = false;
            this.colunaAno.OptionsFilter.AllowFilter = false;
            this.colunaAno.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaAno.Visible = true;
            this.colunaAno.VisibleIndex = 0;
            this.colunaAno.Width = 69;
            // 
            // colunaModelo
            // 
            this.colunaModelo.Caption = "Modelo";
            this.colunaModelo.FieldName = "Modelo";
            this.colunaModelo.Name = "colunaModelo";
            this.colunaModelo.OptionsColumn.AllowEdit = false;
            this.colunaModelo.OptionsColumn.AllowFocus = false;
            this.colunaModelo.OptionsFilter.AllowFilter = false;
            this.colunaModelo.Visible = true;
            this.colunaModelo.VisibleIndex = 1;
            this.colunaModelo.Width = 85;
            // 
            // colunaSerie
            // 
            this.colunaSerie.Caption = "Serie";
            this.colunaSerie.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaSerie.FieldName = "Serie";
            this.colunaSerie.Name = "colunaSerie";
            this.colunaSerie.OptionsColumn.AllowEdit = false;
            this.colunaSerie.OptionsColumn.AllowFocus = false;
            this.colunaSerie.OptionsFilter.AllowFilter = false;
            this.colunaSerie.Visible = true;
            this.colunaSerie.VisibleIndex = 2;
            this.colunaSerie.Width = 73;
            // 
            // colunaNumeroInicial
            // 
            this.colunaNumeroInicial.Caption = "Nr Inicial";
            this.colunaNumeroInicial.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaNumeroInicial.FieldName = "NumeroInicial";
            this.colunaNumeroInicial.Name = "colunaNumeroInicial";
            this.colunaNumeroInicial.OptionsColumn.AllowEdit = false;
            this.colunaNumeroInicial.OptionsColumn.AllowFocus = false;
            this.colunaNumeroInicial.OptionsFilter.AllowFilter = false;
            this.colunaNumeroInicial.Visible = true;
            this.colunaNumeroInicial.VisibleIndex = 3;
            this.colunaNumeroInicial.Width = 108;
            // 
            // colunaNumeroFinal
            // 
            this.colunaNumeroFinal.Caption = "Nr Final";
            this.colunaNumeroFinal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaNumeroFinal.FieldName = "NumeroFinal";
            this.colunaNumeroFinal.Name = "colunaNumeroFinal";
            this.colunaNumeroFinal.OptionsColumn.AllowEdit = false;
            this.colunaNumeroFinal.OptionsColumn.AllowFocus = false;
            this.colunaNumeroFinal.OptionsFilter.AllowFilter = false;
            this.colunaNumeroFinal.Visible = true;
            this.colunaNumeroFinal.VisibleIndex = 4;
            this.colunaNumeroFinal.Width = 129;
            // 
            // colunaProtocolo
            // 
            this.colunaProtocolo.Caption = "Protocolo";
            this.colunaProtocolo.FieldName = "Protocolo";
            this.colunaProtocolo.Name = "colunaProtocolo";
            this.colunaProtocolo.OptionsColumn.AllowEdit = false;
            this.colunaProtocolo.OptionsColumn.AllowFocus = false;
            this.colunaProtocolo.OptionsFilter.AllowFilter = false;
            this.colunaProtocolo.Visible = true;
            this.colunaProtocolo.VisibleIndex = 5;
            this.colunaProtocolo.Width = 208;
            // 
            // colunaJustificativa
            // 
            this.colunaJustificativa.Caption = "Justificativa";
            this.colunaJustificativa.FieldName = "Justificativa";
            this.colunaJustificativa.Name = "colunaJustificativa";
            this.colunaJustificativa.OptionsColumn.AllowEdit = false;
            this.colunaJustificativa.OptionsColumn.AllowFocus = false;
            this.colunaJustificativa.OptionsFilter.AllowFilter = false;
            this.colunaJustificativa.Visible = true;
            this.colunaJustificativa.VisibleIndex = 6;
            this.colunaJustificativa.Width = 422;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcInutilizacoes;
            this.gridView2.Name = "gridView2";
            // 
            // imgLinhaDeCima
            // 
            this.imgLinhaDeCima.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLinhaDeCima.Image = global::Programax.Easy.View.Properties.Resources.linha_horizontal1;
            this.imgLinhaDeCima.Location = new System.Drawing.Point(-142, 185);
            this.imgLinhaDeCima.Name = "imgLinhaDeCima";
            this.imgLinhaDeCima.Size = new System.Drawing.Size(999, 10);
            this.imgLinhaDeCima.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgLinhaDeCima.TabIndex = 10061;
            this.imgLinhaDeCima.TabStop = false;
            // 
            // FormCadastroInutilizacaoNumeroDeNota
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 497);
            this.Name = "FormCadastroInutilizacaoNumeroDeNota";
            this.NomeDaTela = "Inutilização Numeração NF-e";
            this.Text = "Inutilização Numeração NF-e";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtJustificativaCancelamento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboModeloNotaFiscal.Properties)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSerie.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInutilizacoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLinhaDeCima)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtJustificativaCancelamento;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtNumeroInicial;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private DevExpress.XtraEditors.TextEdit txtNumeroFinal;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtAno;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labTipoInscricao;
        private DevExpress.XtraEditors.LookUpEdit cboModeloNotaFiscal;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnInutilizarNumeracao;
        private System.Windows.Forms.Button btnSair;
        private DevExpress.XtraEditors.TextEdit txtSerie;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl gcInutilizacoes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaAno;
        private DevExpress.XtraGrid.Columns.GridColumn colunaSerie;
        private DevExpress.XtraGrid.Columns.GridColumn colunaModelo;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroInicial;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroFinal;
        private DevExpress.XtraGrid.Columns.GridColumn colunaProtocolo;
        private DevExpress.XtraGrid.Columns.GridColumn colunaJustificativa;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.PictureBox imgLinhaDeCima;
    }
}