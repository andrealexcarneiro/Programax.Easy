namespace Programax.Easy.ViewBackupOnline
{
    partial class FormPainelBackupOnline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPainelBackupOnline));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblTransferenciaDownload = new System.Windows.Forms.Label();
            this.dtgArquivos = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cboMinutosCinco = new System.Windows.Forms.ComboBox();
            this.cboHorasCinco = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboMinutosQuatro = new System.Windows.Forms.ComboBox();
            this.cboHorasQuatro = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboMinutosTres = new System.Windows.Forms.ComboBox();
            this.cboHorasTres = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboMinutosDois = new System.Windows.Forms.ComboBox();
            this.cboHorasDois = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMinutosUm = new System.Windows.Forms.ComboBox();
            this.cboHorasUm = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExportarTodosDados = new System.Windows.Forms.Button();
            this.btnAtualizarHorariosBackup = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.dialogDiretorio = new System.Windows.Forms.FolderBrowserDialog();
            this.dialogSalvar = new System.Windows.Forms.SaveFileDialog();
            this.notifyIconBackupOnline = new System.Windows.Forms.NotifyIcon(this.components);
            this.columnDataBackup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colunaNomeArquivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colunaTamanhoArquivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colunaTamanhoArquivoEmBytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgArquivos)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl1.Location = new System.Drawing.Point(12, 23);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(719, 283);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(711, 254);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Geração e Upload de Backups";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(705, 248);
            this.txtLog.TabIndex = 0;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblTransferenciaDownload);
            this.tabPage2.Controls.Add(this.dtgArquivos);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(711, 254);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Download de Backups";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblTransferenciaDownload
            // 
            this.lblTransferenciaDownload.AutoSize = true;
            this.lblTransferenciaDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransferenciaDownload.Location = new System.Drawing.Point(3, 238);
            this.lblTransferenciaDownload.Name = "lblTransferenciaDownload";
            this.lblTransferenciaDownload.Size = new System.Drawing.Size(0, 16);
            this.lblTransferenciaDownload.TabIndex = 1;
            // 
            // dtgArquivos
            // 
            this.dtgArquivos.AllowUserToAddRows = false;
            this.dtgArquivos.AllowUserToDeleteRows = false;
            this.dtgArquivos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtgArquivos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgArquivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgArquivos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnDataBackup,
            this.colunaNomeArquivo,
            this.colunaTamanhoArquivo,
            this.Column1,
            this.colunaTamanhoArquivoEmBytes});
            this.dtgArquivos.Location = new System.Drawing.Point(2, 0);
            this.dtgArquivos.MultiSelect = false;
            this.dtgArquivos.Name = "dtgArquivos";
            this.dtgArquivos.ReadOnly = true;
            this.dtgArquivos.Size = new System.Drawing.Size(709, 229);
            this.dtgArquivos.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cboMinutosCinco);
            this.tabPage3.Controls.Add(this.cboHorasCinco);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.cboMinutosQuatro);
            this.tabPage3.Controls.Add(this.cboHorasQuatro);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.cboMinutosTres);
            this.tabPage3.Controls.Add(this.cboHorasTres);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.cboMinutosDois);
            this.tabPage3.Controls.Add(this.cboHorasDois);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cboMinutosUm);
            this.tabPage3.Controls.Add(this.cboHorasUm);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(711, 254);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Horários de Backups";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cboMinutosCinco
            // 
            this.cboMinutosCinco.FormattingEnabled = true;
            this.cboMinutosCinco.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cboMinutosCinco.Location = new System.Drawing.Point(343, 176);
            this.cboMinutosCinco.Name = "cboMinutosCinco";
            this.cboMinutosCinco.Size = new System.Drawing.Size(55, 21);
            this.cboMinutosCinco.TabIndex = 15;
            // 
            // cboHorasCinco
            // 
            this.cboHorasCinco.FormattingEnabled = true;
            this.cboHorasCinco.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23"});
            this.cboHorasCinco.Location = new System.Drawing.Point(283, 176);
            this.cboHorasCinco.Name = "cboHorasCinco";
            this.cboHorasCinco.Size = new System.Drawing.Size(55, 21);
            this.cboHorasCinco.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(280, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Quinto Horário";
            // 
            // cboMinutosQuatro
            // 
            this.cboMinutosQuatro.FormattingEnabled = true;
            this.cboMinutosQuatro.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cboMinutosQuatro.Location = new System.Drawing.Point(73, 176);
            this.cboMinutosQuatro.Name = "cboMinutosQuatro";
            this.cboMinutosQuatro.Size = new System.Drawing.Size(55, 21);
            this.cboMinutosQuatro.TabIndex = 12;
            // 
            // cboHorasQuatro
            // 
            this.cboHorasQuatro.FormattingEnabled = true;
            this.cboHorasQuatro.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23"});
            this.cboHorasQuatro.Location = new System.Drawing.Point(13, 176);
            this.cboHorasQuatro.Name = "cboHorasQuatro";
            this.cboHorasQuatro.Size = new System.Drawing.Size(55, 21);
            this.cboHorasQuatro.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Quarto Horário";
            // 
            // cboMinutosTres
            // 
            this.cboMinutosTres.FormattingEnabled = true;
            this.cboMinutosTres.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cboMinutosTres.Location = new System.Drawing.Point(644, 34);
            this.cboMinutosTres.Name = "cboMinutosTres";
            this.cboMinutosTres.Size = new System.Drawing.Size(55, 21);
            this.cboMinutosTres.TabIndex = 9;
            // 
            // cboHorasTres
            // 
            this.cboHorasTres.FormattingEnabled = true;
            this.cboHorasTres.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23"});
            this.cboHorasTres.Location = new System.Drawing.Point(584, 34);
            this.cboHorasTres.Name = "cboHorasTres";
            this.cboHorasTres.Size = new System.Drawing.Size(55, 21);
            this.cboHorasTres.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(581, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Terceiro Horário";
            // 
            // cboMinutosDois
            // 
            this.cboMinutosDois.FormattingEnabled = true;
            this.cboMinutosDois.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cboMinutosDois.Location = new System.Drawing.Point(343, 34);
            this.cboMinutosDois.Name = "cboMinutosDois";
            this.cboMinutosDois.Size = new System.Drawing.Size(55, 21);
            this.cboMinutosDois.TabIndex = 6;
            // 
            // cboHorasDois
            // 
            this.cboHorasDois.FormattingEnabled = true;
            this.cboHorasDois.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23"});
            this.cboHorasDois.Location = new System.Drawing.Point(283, 34);
            this.cboHorasDois.Name = "cboHorasDois";
            this.cboHorasDois.Size = new System.Drawing.Size(55, 21);
            this.cboHorasDois.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Segundo Horário";
            // 
            // cboMinutosUm
            // 
            this.cboMinutosUm.FormattingEnabled = true;
            this.cboMinutosUm.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.cboMinutosUm.Location = new System.Drawing.Point(73, 34);
            this.cboMinutosUm.Name = "cboMinutosUm";
            this.cboMinutosUm.Size = new System.Drawing.Size(55, 21);
            this.cboMinutosUm.TabIndex = 3;
            // 
            // cboHorasUm
            // 
            this.cboHorasUm.FormattingEnabled = true;
            this.cboHorasUm.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "21",
            "22",
            "23"});
            this.cboHorasUm.Location = new System.Drawing.Point(13, 34);
            this.cboHorasUm.Name = "cboHorasUm";
            this.cboHorasUm.Size = new System.Drawing.Size(55, 21);
            this.cboHorasUm.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Primeiro Horário";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnExportarTodosDados);
            this.flowLayoutPanel1.Controls.Add(this.btnAtualizarHorariosBackup);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(16, 312);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(438, 41);
            this.flowLayoutPanel1.TabIndex = 10041;
            // 
            // btnExportarTodosDados
            // 
            this.btnExportarTodosDados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarTodosDados.FlatAppearance.BorderSize = 0;
            this.btnExportarTodosDados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarTodosDados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarTodosDados.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExportarTodosDados.Location = new System.Drawing.Point(0, 0);
            this.btnExportarTodosDados.Margin = new System.Windows.Forms.Padding(0);
            this.btnExportarTodosDados.Name = "btnExportarTodosDados";
            this.btnExportarTodosDados.Size = new System.Drawing.Size(157, 40);
            this.btnExportarTodosDados.TabIndex = 10037;
            this.btnExportarTodosDados.Text = "Baixar Backup";
            this.btnExportarTodosDados.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportarTodosDados.UseVisualStyleBackColor = true;
            this.btnExportarTodosDados.Click += new System.EventHandler(this.btnExportarTodosDados_Click);
            // 
            // btnAtualizarHorariosBackup
            // 
            this.btnAtualizarHorariosBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizarHorariosBackup.FlatAppearance.BorderSize = 0;
            this.btnAtualizarHorariosBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizarHorariosBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizarHorariosBackup.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAtualizarHorariosBackup.Location = new System.Drawing.Point(157, 0);
            this.btnAtualizarHorariosBackup.Margin = new System.Windows.Forms.Padding(0);
            this.btnAtualizarHorariosBackup.Name = "btnAtualizarHorariosBackup";
            this.btnAtualizarHorariosBackup.Size = new System.Drawing.Size(129, 40);
            this.btnAtualizarHorariosBackup.TabIndex = 10038;
            this.btnAtualizarHorariosBackup.Text = " Atualizar Horários Backup";
            this.btnAtualizarHorariosBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAtualizarHorariosBackup.UseVisualStyleBackColor = true;
            this.btnAtualizarHorariosBackup.Click += new System.EventHandler(this.btnAtualizarHorariosBackup_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(286, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10036;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // dialogSalvar
            // 
            this.dialogSalvar.AddExtension = false;
            // 
            // notifyIconBackupOnline
            // 
            this.notifyIconBackupOnline.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconBackupOnline.Icon")));
            this.notifyIconBackupOnline.Text = "Backup Online";
            this.notifyIconBackupOnline.Visible = true;
            this.notifyIconBackupOnline.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconBackupOnline_MouseDoubleClick);
            // 
            // columnDataBackup
            // 
            this.columnDataBackup.DataPropertyName = "DataBackup";
            this.columnDataBackup.HeaderText = "Data Backup";
            this.columnDataBackup.Name = "columnDataBackup";
            this.columnDataBackup.ReadOnly = true;
            this.columnDataBackup.Width = 220;
            // 
            // colunaNomeArquivo
            // 
            this.colunaNomeArquivo.DataPropertyName = "NomeArquivo";
            this.colunaNomeArquivo.HeaderText = "Nome Arquivo";
            this.colunaNomeArquivo.Name = "colunaNomeArquivo";
            this.colunaNomeArquivo.ReadOnly = true;
            this.colunaNomeArquivo.Width = 220;
            // 
            // colunaTamanhoArquivo
            // 
            this.colunaTamanhoArquivo.DataPropertyName = "TamanhoArquivo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colunaTamanhoArquivo.DefaultCellStyle = dataGridViewCellStyle1;
            this.colunaTamanhoArquivo.HeaderText = "Tamanho Arquivo (MB)";
            this.colunaTamanhoArquivo.Name = "colunaTamanhoArquivo";
            this.colunaTamanhoArquivo.ReadOnly = true;
            this.colunaTamanhoArquivo.Width = 210;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Data";
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // colunaTamanhoArquivoEmBytes
            // 
            this.colunaTamanhoArquivoEmBytes.DataPropertyName = "TamanhoArquivoEmBytes";
            this.colunaTamanhoArquivoEmBytes.HeaderText = "TamanhoArquivoEmBytes";
            this.colunaTamanhoArquivoEmBytes.Name = "colunaTamanhoArquivoEmBytes";
            this.colunaTamanhoArquivoEmBytes.ReadOnly = true;
            this.colunaTamanhoArquivoEmBytes.Visible = false;
            // 
            // FormPainelBackupOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 358);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPainelBackupOnline";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup Online";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPainelBackupOnline_FormClosing);
            this.Load += new System.EventHandler(this.FormPainelBackupOnline_Load);
            this.Resize += new System.EventHandler(this.FormPainelBackupOnline_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgArquivos)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnExportarTodosDados;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.DataGridView dtgArquivos;
        private System.Windows.Forms.FolderBrowserDialog dialogDiretorio;
        private System.Windows.Forms.SaveFileDialog dialogSalvar;
        private System.Windows.Forms.Label lblTransferenciaDownload;
        private System.Windows.Forms.NotifyIcon notifyIconBackupOnline;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox cboMinutosCinco;
        private System.Windows.Forms.ComboBox cboHorasCinco;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboMinutosQuatro;
        private System.Windows.Forms.ComboBox cboHorasQuatro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboMinutosTres;
        private System.Windows.Forms.ComboBox cboHorasTres;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboMinutosDois;
        private System.Windows.Forms.ComboBox cboHorasDois;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMinutosUm;
        private System.Windows.Forms.ComboBox cboHorasUm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAtualizarHorariosBackup;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDataBackup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colunaNomeArquivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colunaTamanhoArquivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colunaTamanhoArquivoEmBytes;

    }
}