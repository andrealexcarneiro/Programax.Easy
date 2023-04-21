using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;
using Programax.Easy.Servico.Cadastros.CashBack.CashBackServ;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Data;


namespace Programax.Easy.View.Telas.Financeiro.Bancos
{
    public partial class FormCadastroCashBack : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private string ConectionString;
        private DataSet mDataSet;
        private MySqlDataAdapter mAdapter;
        #endregion

        #region " CONSTRUTOR "

        public FormCadastroCashBack()
        {
            InitializeComponent();

            mostraResultados();

            this.NomeDaTela = "Cadastro de Configurações CashBack";
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
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
            int Codigo = 1;
            bool start = chkStart.Checked;
            int dias = txtDias.Text.ToInt();
            int validade = txtValidade.Text.ToInt();
            string Percentual = txtPercentual.Text.ToString().Replace(",", ".");
            string valor = txtValor.Text.ToString().Replace(",", ".");
            if (txtcodigo.Text == "")
            {

           
                using (var conn = new MySqlConnection(ConectionString))

                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand("INSERT INTO configcashback (idcash,habilitar, dias,validade, percentual,valor)" +
                    "VALUES(" + Codigo + "," + start + "," + dias + "," + validade + "," + Percentual + "," + valor + ")", conn);

                    command.ExecuteNonQuery();
                    conn.Close();


                    MessageBox.Show("Registro Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    mostraResultados();
                }
            }
            else
            {
                 using (var conn = new MySqlConnection(ConectionString))
                {

                    conn.Open();

                    string Sql = "update configcashback set habilitar= " + this.chkStart.Checked + "," +
                       "dias=" + dias + ",validade=" + validade + "," +
                       "percentual=" + Percentual + ",valor=" + valor +
                       " where idcash=" + Codigo+ ";";


                    MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                    MySqlDataReader MyReader2;


                    var returnValue = MyCommand.ExecuteReader();
                    while (returnValue.Read())
                    {
                        int habilitar = returnValue["habilitar"].ToInt(); 
                        txtcodigo.Text = returnValue["idcash"].ToString();
                        chkStart.Checked = Convert.ToBoolean(habilitar);
                        txtDias.Text = returnValue["dias"].ToString();
                        txtValidade.Text = returnValue["validade"].ToString();
                        txtPercentual.Text = string.Format("{0:N}", returnValue["percentual"].ToString());
                        txtValor.Text = string.Format("{0:N}", returnValue["valor"].ToString());
                    }
                    MessageBox.Show("Registro Alterado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void mostraResultados()
        {
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
            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
                string Sql = "Select * from configcashback";
                // string Query = "update student.studentinfo set idStudentInfo='" + this.IdTextBox.Text + "',Name='" + this.NameTextBox.Text + "',Father_Name='" + this.FnameTextBox.Text + "',Age='" + this.AgeTextBox.Text + "',Semester='" + this.SemesterTextBox.Text + "' where idStudentInfo='" + this.IdTextBox.Text + "';";

                
                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;
         
               
                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    txtcodigo.Text = returnValue["idcash"].ToString();
                    txtDias.Text = returnValue["dias"].ToString();
                    txtValidade.Text = returnValue["validade"].ToString();
                    txtPercentual.Text = returnValue["percentual"].ToString();
                    txtValor.Text = string.Format("{0:N}", returnValue["valor"].ToString());
                    int habilitar = returnValue["habilitar"].ToInt();
                    chkStart.Checked = Convert.ToBoolean(habilitar);
                }
               
            }

          

        }
        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormBancosPesquisa formBancosPesquisa = new FormBancosPesquisa();

            var banco = formBancosPesquisa.ExibaPesquisaDeBancos();

            if (banco != null)
            {
                EditeBanco(banco);
            }
        }

        

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void LimpeFormulario()
        {
            EditeBanco(null);

            txtDias.Focus();
            txtDias.Text = string.Empty;
            txtPercentual.Text = string.Empty;
            txtValor.Text = string.Empty;
            txtValidade.Text = string.Empty;
            
        }

        private void EditeBanco(Banco banco)
        {
            //if (banco != null)
            //{
            //    _idBanco = banco.Id;

            //    txtDescricao.Text = banco.Descricao;
            //    txtDataCadastro.Text = banco.DataCadastro.ToString("dd/MM/yyyy");
            //    txtCodigoCompensacao.Text = banco.Codigo;
            //    txtValor.Text = banco.Site;

            //    cboStatus.EditValue = banco.Status;

            //    txtDescricao.Focus();
            //}
            //else
            //{
            //    _idBanco = 0;
            //    txtDescricao.Text = string.Empty;
            //    txtCodigoCompensacao.Text = string.Empty;
            //    txtValor.Text = string.Empty;
            //    txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            //}
        }

       

        private void PesquisePeloCodigoDoBanco()
        {
            ServicoBanco servicoBanco = new ServicoBanco();
            var banco = servicoBanco.ConsultePeloCodigoBanco(txtcodigo.Text);

            EditeBanco(banco);
        }

        #endregion

      
    }
}
