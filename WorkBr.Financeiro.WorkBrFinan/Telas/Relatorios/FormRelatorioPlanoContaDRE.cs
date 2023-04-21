using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.PlanoContasServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.PlanoContasDreServ;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Programax.Easy.Servico;
using Newtonsoft.Json;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using MySql.Data.MySqlClient;
using Programax.Easy.Report.RelatoriosDevExpress.Financeiro;

namespace Programax.Easy.View.Telas.Financeiro.PlanosDeContas
{
    public partial class FormRelatorioPlanoContaDre : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "
        private MySqlConnection Conn;

        private MySqlDataAdapter Adapter;
        private List<PlanoContaDre> _listaDePlanosDeContas;
        private DevExpress.XtraGrid.GridControl gcDRE;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao;
        private DevExpress.XtraGrid.Columns.GridColumn Valor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LookUpEdit cboAno;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit cboMes;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Button button1;
        private Button button2;
        private Button button3;
        private int _idPlanoContas;
        private string ConectionString;

        #endregion

        #region " CONSTRUTOR "

        public FormRelatorioPlanoContaDre()
        {
            InitializeComponent();


            PreenchaMes();
            PreenchaAno();


            AtualizarGridDePlanoDeContas();


            this.NomeDaTela = "D.R.E";
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {

        }

        private void gcPlanoDeContas_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gcPlanoDeContas_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtNumeroPlanoContas_Leave(object sender, EventArgs e)
        {

        }

        #endregion

        #region " MÉTODOS AUXILIARES "

       
        private void PreenchaMes()
        {
            ObjetoParaComboBox objetoComboJaneiro = new ObjetoParaComboBox();
            objetoComboJaneiro.Valor = "01";
            objetoComboJaneiro.Descricao = "Janeiro";

            ObjetoParaComboBox objetoComboFevereiro = new ObjetoParaComboBox();
            objetoComboFevereiro.Valor = "02";
            objetoComboFevereiro.Descricao = "Fevereiro";

            ObjetoParaComboBox objetoComboMarço = new ObjetoParaComboBox();
            objetoComboMarço.Valor = "03";
            objetoComboMarço.Descricao = "Março";

            ObjetoParaComboBox objetoComboAbril = new ObjetoParaComboBox();
            objetoComboAbril.Valor = "04";
            objetoComboAbril.Descricao = "Abril";

            ObjetoParaComboBox objetoComboMaio = new ObjetoParaComboBox();
            objetoComboMaio.Valor = "05";
            objetoComboMaio.Descricao = "Maio";

            ObjetoParaComboBox objetoComboJunho = new ObjetoParaComboBox();
            objetoComboJunho.Valor = "06";
            objetoComboJunho.Descricao = "Junho";

            ObjetoParaComboBox objetoComboJulho = new ObjetoParaComboBox();
            objetoComboJulho.Valor = "07";
            objetoComboJulho.Descricao = "Julho";

            ObjetoParaComboBox objetoComboAgosto = new ObjetoParaComboBox();
            objetoComboAgosto.Valor = "08";
            objetoComboAgosto.Descricao = "Agosto";

            ObjetoParaComboBox objetoComboSetembro = new ObjetoParaComboBox();
            objetoComboSetembro.Valor = "09";
            objetoComboSetembro.Descricao = "Setembro";

            ObjetoParaComboBox objetoComboOutubro = new ObjetoParaComboBox();
            objetoComboOutubro.Valor = "10";
            objetoComboOutubro.Descricao = "Outubro";

            ObjetoParaComboBox objetoComboNovembro = new ObjetoParaComboBox();
            objetoComboNovembro.Valor = "11";
            objetoComboNovembro.Descricao = "Novembro";

            ObjetoParaComboBox objetoComboDezembro = new ObjetoParaComboBox();
            objetoComboDezembro.Valor = "12";
            objetoComboDezembro.Descricao = "Dezembro";

          


            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboJaneiro);
            listaDeItensParaOComboBox.Add(objetoComboFevereiro);
            listaDeItensParaOComboBox.Add(objetoComboMarço);
            listaDeItensParaOComboBox.Add(objetoComboAbril);
            listaDeItensParaOComboBox.Add(objetoComboMaio);
            listaDeItensParaOComboBox.Add(objetoComboJunho);
            listaDeItensParaOComboBox.Add(objetoComboJulho);
            listaDeItensParaOComboBox.Add(objetoComboAgosto);
            listaDeItensParaOComboBox.Add(objetoComboSetembro);
            listaDeItensParaOComboBox.Add(objetoComboOutubro);
            listaDeItensParaOComboBox.Add(objetoComboNovembro);
            listaDeItensParaOComboBox.Add(objetoComboDezembro);

            cboMes.Properties.DataSource = listaDeItensParaOComboBox;
            cboMes.Properties.ValueMember = "Valor";
            cboMes.Properties.DisplayMember = "Descricao";

            //cboStatus.EditValue = "A";
        }

        private void PreenchaAno()
        {
            //ObjetoParaComboBox objetoCombo2018 = new ObjetoParaComboBox();
            //objetoCombo2018.Valor = "1";
            //objetoCombo2018.Descricao = "2018";

            //ObjetoParaComboBox objetoCombo2019 = new ObjetoParaComboBox();
            //objetoCombo2019.Valor = "2";
            //objetoCombo2019.Descricao = "2019";

            ObjetoParaComboBox objetoCombo2020 = new ObjetoParaComboBox();
            objetoCombo2020.Valor = "3";
            objetoCombo2020.Descricao = "2020";

            ObjetoParaComboBox objetoCombo2021 = new ObjetoParaComboBox();
            objetoCombo2021.Valor = "4";
            objetoCombo2021.Descricao = "2021";

            ObjetoParaComboBox objetoCombo2022 = new ObjetoParaComboBox();
            objetoCombo2022.Valor = "5";
            objetoCombo2022.Descricao = "2022";

            ObjetoParaComboBox objetoCombo2023 = new ObjetoParaComboBox();
            objetoCombo2023.Valor = "6";
            objetoCombo2023.Descricao = "2023";



            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            //listaDeItensParaOComboBox.Add(objetoCombo2018);
            //listaDeItensParaOComboBox.Add(objetoCombo2019);
            listaDeItensParaOComboBox.Add(objetoCombo2020);
            listaDeItensParaOComboBox.Add(objetoCombo2021);
            listaDeItensParaOComboBox.Add(objetoCombo2022);
            listaDeItensParaOComboBox.Add(objetoCombo2023);

            cboAno.Properties.DataSource = listaDeItensParaOComboBox;
            cboAno.Properties.ValueMember = "Valor";
            cboAno.Properties.DisplayMember = "Descricao";

            //cboAno.EditValue = "5";
        }

        
        private void AtualizarGridDePlanoDeContas()
        {
            ServicoPlanoDeContasDre servicoPlanoDeContas = new ServicoPlanoDeContasDre();
            _listaDePlanosDeContas = servicoPlanoDeContas.ConsulteLista();
        }


        #endregion

        private void InitializeComponent()
        {
            this.gcDRE = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Valor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboAno = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboMes = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDRE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.button1);
            this.painelBotoes.Controls.Add(this.button2);
            this.painelBotoes.Controls.Add(this.button3);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.gcDRE);
            this.panelConteudo.Controls.Add(this.cboAno);
            this.panelConteudo.Controls.Add(this.labelControl5);
            this.panelConteudo.Controls.Add(this.cboMes);
            this.panelConteudo.Controls.Add(this.labelControl4);
            this.panelConteudo.Size = new System.Drawing.Size(846, 347);
            // 
            // gcDRE
            // 
            this.gcDRE.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcDRE.Location = new System.Drawing.Point(4, 53);
            this.gcDRE.MainView = this.gridView5;
            this.gcDRE.Name = "gcDRE";
            this.gcDRE.Size = new System.Drawing.Size(838, 291);
            this.gcDRE.TabIndex = 522;
            this.gcDRE.TabStop = false;
            this.gcDRE.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            // 
            // gridView5
            // 
            this.gridView5.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView5.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunaDescricao,
            this.Valor});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcDRE;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.ViewCaption = "D.R.E";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Cód Cadastro";
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
            this.colunaId.Width = 83;
            // 
            // colunaDescricao
            // 
            this.colunaDescricao.Caption = "Descrição";
            this.colunaDescricao.FieldName = "Descricao";
            this.colunaDescricao.Name = "colunaDescricao";
            this.colunaDescricao.OptionsColumn.AllowEdit = false;
            this.colunaDescricao.OptionsColumn.AllowFocus = false;
            this.colunaDescricao.OptionsFilter.AllowFilter = false;
            this.colunaDescricao.Visible = true;
            this.colunaDescricao.VisibleIndex = 0;
            this.colunaDescricao.Width = 181;
            // 
            // Valor
            // 
            this.Valor.Caption = "Valor";
            this.Valor.FieldName = "Valor";
            this.Valor.Name = "Valor";
            this.Valor.OptionsColumn.AllowEdit = false;
            this.Valor.OptionsColumn.AllowFocus = false;
            this.Valor.OptionsFilter.AllowFilter = false;
            this.Valor.Visible = true;
            this.Valor.VisibleIndex = 1;
            this.Valor.Width = 98;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcDRE;
            this.gridView2.Name = "gridView2";
            // 
            // cboAno
            // 
            this.cboAno.EnterMoveNextControl = true;
            this.cboAno.Location = new System.Drawing.Point(175, 25);
            this.cboAno.Name = "cboAno";
            this.cboAno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAno.Properties.Appearance.Options.UseFont = true;
            this.cboAno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboAno.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboAno.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descricao")});
            this.cboAno.Properties.DropDownRows = 3;
            this.cboAno.Properties.NullText = "";
            this.cboAno.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboAno.Size = new System.Drawing.Size(157, 22);
            this.cboAno.TabIndex = 511;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(175, 9);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(19, 13);
            this.labelControl5.TabIndex = 521;
            this.labelControl5.Text = "Ano";
            // 
            // cboMes
            // 
            this.cboMes.EnterMoveNextControl = true;
            this.cboMes.Location = new System.Drawing.Point(12, 25);
            this.cboMes.Name = "cboMes";
            this.cboMes.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMes.Properties.Appearance.Options.UseFont = true;
            this.cboMes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMes.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboMes.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descricao")});
            this.cboMes.Properties.DropDownRows = 4;
            this.cboMes.Properties.NullText = "";
            this.cboMes.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboMes.Size = new System.Drawing.Size(157, 22);
            this.cboMes.TabIndex = 510;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(12, 9);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(20, 13);
            this.labelControl4.TabIndex = 520;
            this.labelControl4.Text = "Mes";
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 40);
            this.button1.TabIndex = 1004;
            this.button1.Text = "Consultar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.Location = new System.Drawing.Point(100, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 40);
            this.button2.TabIndex = 1006;
            this.button2.Text = "Imprimir";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button3.Location = new System.Drawing.Point(214, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 40);
            this.button3.TabIndex = 1005;
            this.button3.TabStop = false;
            this.button3.Text = " Sair";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // FormRelatorioPlanoContaDre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(892, 458);
            this.Name = "FormRelatorioPlanoContaDre";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDRE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMes.Properties)).EndInit();
            this.ResumeLayout(false);

        }
       
       
        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cboAno.Text == "")
                {
                    Cursor.Current = Cursors.Default;
                    return;
                }
                if (cboMes.Text == "")
                {
                    Cursor.Current = Cursors.Default;
                    return;
                }

                string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

                ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

                var item = conexoes.Conexoes[IndiceBancoDados];

                string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
                string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
                string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
                string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
                int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }



               

            }
           
            string diaMes = "";
            string strMes = "";

            switch (cboMes.Text)
            {
                case "Janeiro":
                    strMes = "01";
                    break;
                case "Fevereiro":
                    strMes = "02";
                    break;
                case "Março":
                    strMes = "03";
                    break;
                case "Abril":
                    strMes = "04";
                    break;
                case "Maio":
                    strMes = "05";
                    break;
                case "Junho":
                    strMes = "06";
                    break;
                case "Julho":
                    strMes = "07";
                    break;
                case "Agosto":
                    strMes = "08";
                    break;
                case "Setembro":
                    strMes = "09";
                    break;
                case "Outubro":
                    strMes = "10";
                    break;
                case "Novembro":
                    strMes = "11";
                    break;
                case "Dezembro":
                    strMes = "12";
                    break;

            }
            int intAno = 0;

            intAno = cboAno.Text.ToInt();


            DateTime ultimoDiaDoMes = new DateTime(intAno, strMes.ToInt(), DateTime.DaysInMonth(intAno, strMes.ToInt()));
            diaMes = ultimoDiaDoMes.Day.ToString();
            if (DateTime.IsLeapYear(cboAno.Text.ToInt()))
            {
                diaMes = "28";
            }

            string DataInicial = cboAno.Text + "-" + strMes + "-01" ;
            string DataFinal = intAno.ToString() + "-" + strMes.ToString() + "-" + diaMes.ToString();

            DateTime dateTimeValue = DataInicial.ToDate();
            DateTime dateTimeValue2 = DataFinal.ToDate();

            List<PlanoDeContasAuxiliar> listaDePlanosDeContasAuxiliares = new List<PlanoDeContasAuxiliar>();
            double dblSalarios = 0;
            double dblFGTS = 0;
            double dblReceita = 0;
            double dblSimples = 0;
            double dblComissao = 0;
            double dblFerias = 0;
            double dblInss = 0;
            double dblDecimo = 0;
            double dblEncargos = 0;
            double dblLucroOperacional = 0 ;
            double dblDespesaOperacional = 0;
            double dblDespesaComercial = 0;
            double dblDespesaGeral = 0;
            double dblDespesaFinanceiras = 0;
            double dblLucroLiquido = 0;
            double dblMutuo = 0;
            double dblCustofixo = 0;
            double dblLucroLiquidoFinal = 0;

            foreach (var planoDeContas in _listaDePlanosDeContas)
            {
                PlanoDeContasAuxiliar planoDeContasAuxiliar = new PlanoDeContasAuxiliar();
                if (planoDeContas.Grau == "1")
                {
                    planoDeContasAuxiliar.Descricao = "        " + planoDeContas.Descricao;
                }
                else
                {
                    planoDeContasAuxiliar.Descricao = planoDeContas.Descricao;
                }

                planoDeContasAuxiliar.Id = planoDeContas.Id;
                //if (planoDeContas.Id == 20)
                //{
                //    MessageBox.Show("");
                //}

                //if (planoDeContas.Grau == "1")
                //{
                switch (planoDeContas.Id)
                    {
                        case 2:
                            using (var conn = new MySqlConnection(ConectionString))
                            {
                                MySqlCommand command = new MySqlCommand("DRE", conn);
                                command.CommandType = CommandType.StoredProcedure;


                                command.Parameters.AddWithValue("varDataInicial", dateTimeValue);
                                command.Parameters.AddWithValue("varDataFinal", dateTimeValue2);

                                conn.Open();
                                command.ExecuteNonQuery();

                                var returnValue = command.ExecuteReader();
                                double variavel = 0;
                                while (returnValue.Read())
                                {
                                    variavel = returnValue["Total"].ToDouble();
                                    planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", variavel);
                                    dblReceita = variavel.ToDouble();
                                }
                                conn.Close();
                            }
                            break;
                        case 7:
                                dblReceita = dblReceita - dblSimples;
                                planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblReceita);
                        break;

                        case 8:
                            using (var conn = new MySqlConnection(ConectionString))
                            {
                                MySqlCommand command = new MySqlCommand("Entradas", conn);
                                command.CommandType = CommandType.StoredProcedure;


                                command.Parameters.AddWithValue("varDataInicial", dateTimeValue);
                                command.Parameters.AddWithValue("varDataFinal", dateTimeValue2);

                                conn.Open();
                                command.ExecuteNonQuery();

                                var returnValue = command.ExecuteReader();
                                double variavel = 0;
                                while (returnValue.Read())
                                {
                                    variavel = returnValue["Valor"].ToDouble();
                                    dblCustofixo = variavel;
                                    planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", variavel);
                                    
                                }
                                conn.Close();
                            }
                            break;
                        case 10:
                            dblLucroOperacional = dblReceita - dblCustofixo;
                            planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblLucroOperacional);
                            break;
                       
                        case 16:
                            dblFGTS = (dblSalarios * 8) /100;
                            dblInss = (dblSalarios * 26) / 100;
                            dblFGTS = dblFGTS + dblInss;
                            planoDeContasAuxiliar.Valor = "R$ " +  string.Format("{0:N}", dblFGTS);
                            break;
                        case 17:
                            dblFerias = ((dblSalarios + dblComissao) * 33) / 100;
                            planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblFerias);
                            break;
                        case 18:
                            dblDecimo = (dblSalarios + dblComissao) / 12;
                            planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblDecimo);
                            break;
                        case 19:
                            dblEncargos =  (((dblFerias * 8) /100) + ((dblDecimo * 8) /100) );
                            planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblEncargos);
                            break;
                        case 42:
                            planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblDespesaOperacional);
                            break;
                        case 50:
                            planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblDespesaComercial);
                            break;
                        case 90:
                            planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblDespesaGeral);
                            break;
                        case 99:
                            planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblDespesaFinanceiras);
                            break;
                        case 104:
                            dblLucroLiquidoFinal = dblLucroOperacional - dblDespesaOperacional - dblDespesaGeral - dblDespesaFinanceiras - dblDespesaComercial - dblMutuo;
                            planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblLucroLiquidoFinal);
                            break;
                        case 107:
                            dblLucroLiquido = dblLucroOperacional - dblDespesaOperacional - dblDespesaGeral - dblDespesaFinanceiras - dblDespesaComercial - dblMutuo;    
                            planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", dblLucroLiquido);
                            break;
                    default:
                            double variavelImposto = 0;
                            string NumeroPlano;
                            NumeroPlano = planoDeContas.NumeroPlanoContasContador;
                               
                        using (var conn = new MySqlConnection(ConectionString))
                            {


                            if (planoDeContas.Id == 61) 
                            {
                                for (int i = 1; i <= 2; i++)
                                {
                                    MySqlCommand commandImpostoII = new MySqlCommand("DREImpostos", conn);
                                    commandImpostoII.CommandType = CommandType.StoredProcedure;


                                    commandImpostoII.Parameters.AddWithValue("varDataInicial", dateTimeValue);
                                    commandImpostoII.Parameters.AddWithValue("varDataFinal", dateTimeValue2);
                                    commandImpostoII.Parameters.AddWithValue("varCategoria", NumeroPlano);


                                    conn.Open();
                                    commandImpostoII.ExecuteNonQuery();

                                    var returnValueImpostoII = commandImpostoII.ExecuteReader();

                                    while (returnValueImpostoII.Read())
                                    {
                                        variavelImposto += returnValueImpostoII["Valor"].ToDouble();

                                    }
                                    conn.Close();
                                    NumeroPlano = "73";
                                }
                                planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", variavelImposto);
                            }
                            else if(planoDeContas.Id == 20)
                            {
                                for (int i = 1; i <= 2; i++)
                                {
                                    MySqlCommand commandImpostoII = new MySqlCommand("DREImpostos", conn);
                                    commandImpostoII.CommandType = CommandType.StoredProcedure;


                                    commandImpostoII.Parameters.AddWithValue("varDataInicial", dateTimeValue);
                                    commandImpostoII.Parameters.AddWithValue("varDataFinal", dateTimeValue2);
                                    commandImpostoII.Parameters.AddWithValue("varCategoria", NumeroPlano);


                                    conn.Open();
                                    commandImpostoII.ExecuteNonQuery();

                                    var returnValueImpostoII = commandImpostoII.ExecuteReader();

                                    while (returnValueImpostoII.Read())
                                    {
                                        variavelImposto += returnValueImpostoII["Valor"].ToDouble();

                                    }
                                    conn.Close();
                                    NumeroPlano = "69";
                                }
                                planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", variavelImposto);
                            }

                            else
                            {
                                MySqlCommand commandImposto = new MySqlCommand("DREImpostos", conn);
                                commandImposto.CommandType = CommandType.StoredProcedure;


                                commandImposto.Parameters.AddWithValue("varDataInicial", dateTimeValue);
                                commandImposto.Parameters.AddWithValue("varDataFinal", dateTimeValue2);
                                commandImposto.Parameters.AddWithValue("varCategoria", NumeroPlano);


                                conn.Open();
                                commandImposto.ExecuteNonQuery();

                                var returnValueImposto = commandImposto.ExecuteReader();

                                while (returnValueImposto.Read())
                                {
                                    variavelImposto = returnValueImposto["Valor"].ToDouble();

                                    if (variavelImposto == 0)
                                    {
                                        switch (planoDeContas.Id)
                                        {
                                            case 1:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 3:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 11:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 43:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 51:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 91:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 100:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                                //case 104:
                                                //    planoDeContasAuxiliar.Valor = "";
                                                break;
                                            default:
                                                planoDeContasAuxiliar.Valor = "R$ " + "0,00";
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        switch (planoDeContas.Id)
                                        {
                                            case 4:
                                                dblSimples = variavelImposto;
                                                break;
                                            case 12:
                                                dblSalarios = variavelImposto;
                                                dblDespesaOperacional += dblSalarios;
                                                break;
                                            case 13:
                                                dblComissao = variavelImposto;
                                                dblDespesaOperacional += dblComissao;
                                                break;

                                            default:
                                                if (15 <= planoDeContas.Id && planoDeContas.Id < 42)
                                                {
                                                    dblDespesaOperacional += variavelImposto;
                                                }

                                                if (44 <= planoDeContas.Id && planoDeContas.Id < 50)
                                                {
                                                    dblDespesaComercial += variavelImposto;
                                                }

                                                if (52 <= planoDeContas.Id && planoDeContas.Id < 90)
                                                {
                                                    dblDespesaGeral += variavelImposto;
                                                }

                                                if (92 <= planoDeContas.Id && planoDeContas.Id < 99)
                                                {
                                                    dblDespesaFinanceiras += variavelImposto;
                                                }

                                                //if (planoDeContas.Id == 103)
                                                //{
                                                //    dblMutuo += variavelImposto;
                                                //}
                                                break;
                                        }
                                    }
                                    planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", variavelImposto);
                                    }

                                }

                                conn.Close();
                            }
                            if (variavelImposto == 0)
                            {
                                using (var conn = new MySqlConnection(ConectionString))
                                {
                                    NumeroPlano = planoDeContas.NumeroPlanoDeContas;
                                    //if (planoDeContas.Id == 61)
                                    //{
                                    //    NumeroPlano = "71,72,73";
                                    //}
                                    MySqlCommand commandCaixa = new MySqlCommand("Caixa", conn);
                                    commandCaixa.CommandType = CommandType.StoredProcedure;


                                    commandCaixa.Parameters.AddWithValue("varDataInicial", dateTimeValue);
                                    commandCaixa.Parameters.AddWithValue("varDataFinal", dateTimeValue2);
                                    commandCaixa.Parameters.AddWithValue("varCategoria", planoDeContas.NumeroPlanoContasContador);


                                    conn.Open();
                                    commandCaixa.ExecuteNonQuery();

                                    var returnValueCaixa = commandCaixa.ExecuteReader();
                                    double variavelCaixa = 0;
                                    while (returnValueCaixa.Read())
                                    {
                                        variavelCaixa = returnValueCaixa["Valor"].ToDouble();

                                        if (variavelCaixa == 0 )
                                        {

                                        switch (planoDeContas.Id)
                                        {
                                            case 1:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 3:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 11:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 43:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 51:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 91:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                            case 100:
                                                planoDeContasAuxiliar.Valor = "";
                                                break;
                                           
                                            default:
                                                planoDeContasAuxiliar.Valor = "R$ " + "0,00";
                                                break;
                                        }
                                    }
                                        else
                                        {
                                            switch (planoDeContas.Id)
                                            {
                                                case 13:
                                                    dblComissao = variavelCaixa;
                                                    dblDespesaOperacional += dblComissao;
                                                    break;
                                                default:
                                                    if (15 <= planoDeContas.Id && planoDeContas.Id < 42)
                                                    {
                                                        dblDespesaOperacional += variavelCaixa;
                                                    }
                                                    if (44 <= planoDeContas.Id && planoDeContas.Id < 50)
                                                    {
                                                        dblDespesaComercial += variavelCaixa;
                                                    }
                                                    if (52 <= planoDeContas.Id && planoDeContas.Id < 90)
                                                    {
                                                        dblDespesaGeral += variavelCaixa;
                                                    }
                                                    if (92 <= planoDeContas.Id && planoDeContas.Id < 99)
                                                    {
                                                        dblDespesaFinanceiras += variavelCaixa;
                                                    }
                                                break;
                                            }
                                                planoDeContasAuxiliar.Valor = "R$ " + string.Format("{0:N}", variavelCaixa); ;
                                        }

                                    }

                                    conn.Close();
                                }

                                
                            }
                            break;
                    }

                //}
                listaDePlanosDeContasAuxiliares.Add(planoDeContasAuxiliar);
            }

            gcDRE.DataSource = listaDePlanosDeContasAuxiliares;
            gcDRE.RefreshDataSource();


            Cursor.Current = Cursors.Default;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cboAno.Text == "")
            {
                Cursor.Current = Cursors.Default;
                return;
            }
            if (cboMes.Text == "")
            {
                Cursor.Current = Cursors.Default;
                return;
            }

            string diaMes = "";
            string strMes = "";

            switch (cboMes.Text)
            {
                case "Janeiro":
                    strMes = "01";
                    break;
                case "Fevereiro":
                    strMes = "02";
                    break;
                case "Março":
                    strMes = "03";
                    break;
                case "Abril":
                    strMes = "04";
                    break;
                case "Maio":
                    strMes = "05";
                    break;
                case "Junho":
                    strMes = "06";
                    break;
                case "Julho":
                    strMes = "07";
                    break;
                case "Agosto":
                    strMes = "08";
                    break;
                case "Setembro":
                    strMes = "09";
                    break;
                case "Outubro":
                    strMes = "10";
                    break;
                case "Novembro":
                    strMes = "11";
                    break;
                case "Dezembro":
                    strMes = "12";
                    break;

            }
            int intAno = 0;

            intAno = cboAno.Text.ToInt();


            DateTime ultimoDiaDoMes = new DateTime(intAno, strMes.ToInt(), DateTime.DaysInMonth(intAno, strMes.ToInt()));
            diaMes = ultimoDiaDoMes.Day.ToString();
            if (DateTime.IsLeapYear(cboAno.Text.ToInt()))
            {
                diaMes = "28";
            }

            string DataInicial = cboAno.Text + "-" + strMes + "-01";
            string DataFinal = intAno.ToString() + "-" + strMes.ToString() + "-" + diaMes.ToString();

            DateTime dateTimeValue = DataInicial.ToDate();
            DateTime dateTimeValue2 = DataFinal.ToDate();
            string Referencia = cboMes.Text + "/" + cboAno.Text;

            RelatoriodeDRE relatorioDRE = new RelatoriodeDRE(dateTimeValue.ToString(), dateTimeValue2.ToString(), Referencia.ToString());

            TratamentosDeTela.ExibirRelatorio(relatorioDRE);
            Cursor.Current = Cursors.Default;
        }
    }
}
