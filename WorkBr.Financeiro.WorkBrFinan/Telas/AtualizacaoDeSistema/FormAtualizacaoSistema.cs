using System.IO;
using Programax.Easy.Servico.AtualizacaoSistema;
using System;
using System.Windows.Forms;
using Programax.Easy.Servico.ConfiguracoesSistema.BackupServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Programax.Easy.View.Telas.AtualizacaoDeSistema
{
    public partial class FormAtualizacaoSistema : Form
    {
        public string versao = "";
        private string ConectionString;
        public FormAtualizacaoSistema(string Versao)
        {
            InitializeComponent();
            versao = Versao;
        }

        private void FormAtualizacaoSistema_Shown(object sender, EventArgs e)
        {
            //BaixarAtualizar();
            this.timer1.Start();
        }

        public void BaixarAtualizar()
        {
            try
            {
                /* Create Object Instance */
                FTP ftpClient = new FTP(@"ftp://site1393677047@ftp.site1393677047.hospedagemdesites.ws", "site1393677047", "PHD@2018");

                //ServicoParametros servicoParametos = new ServicoParametros(true);
                //Parametros parametros = new Parametros();

                //parametros = servicoParametos.ConsulteParametros();

                string caminhoAtualizacao = "";

                //if (string.IsNullOrEmpty(parametros.ParametrosCadastros.CaminhoACBR))
                //{
                var diretorioPadrao = DetectarDiretorioPadrao();

                if (!string.IsNullOrEmpty(diretorioPadrao))
                    caminhoAtualizacao = diretorioPadrao;//parametros.ParametrosCadastros.CaminhoACBR = diretorioPadrao;
                else
                    caminhoAtualizacao = "C:\\Programax";//parametros.ParametrosCadastros.CaminhoACBR = "C:\\Programax";

                    //servicoParametos.Atualize(parametros);
                //}

                if (!Directory.Exists(@caminhoAtualizacao))
                {
                    Directory.CreateDirectory(@caminhoAtualizacao);
                }

                FormProgress formProgress = new FormProgress(true);

                ChamarBarraProgresso(formProgress);

                /* Download do Scrit de Atualizacao */
                ftpClient.download("public_html/EXECUTAVEIS/Atualizacao/Scripts/Atualizacao_Atual.sql", @caminhoAtualizacao + "\\Atualizacao_Atual.sql");
                
                AtualizeBancoDeDados(@caminhoAtualizacao);
                
                /* Download do Executável */
                ftpClient.download("public_html/EXECUTAVEIS/Atualizacao/Akil_Atualizar.zip", @caminhoAtualizacao + "\\Akil_Atualizar.zip");

                FecharBarraProgreso(formProgress);
                //string Versao = "";
                //double versaoAnterior = Convert.ToDouble(versao.ToString().Substring(versao.Length - 4, 4));
                //versaoAnterior = versaoAnterior + 1;
                //Versao = versaoAnterior.ToString();

                //versao = "1.1." + Versao.Substring(0, 2) + "." + Versao.Substring(2, 1);
                //AlterarVersao(versao);
               
                ftpClient = null;

                FecharForms();
                ChamarInstalador(@caminhoAtualizacao);
            }
            catch (Exception ex)            
                { throw new Exception("Você pode estar DESCONECTADO DA INTERNET! FECHE o sistema, verifique suas conexões e tente novamente.", ex); };
                return;
        }
        private void AlterarVersao(string versao)
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


                using (var conn = new MySqlConnection(ConectionString))
                {

                    conn.Open();

                    string Sql = "update empresa set EMPR_VERSAO = '" + versao + "'  where empr_id = " + 1;


                    MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                    MySqlDataReader MyReader2;


                    var returnValue = MyCommand.ExecuteReader();


                }
                
        }
        public string DetectarDiretorioPadrao()
        {
            string caminhoApp = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

            int tamanhaTotal = caminhoApp.Length-8;

            string caminhoPadrao = caminhoApp.Remove(tamanhaTotal);//Retirar a pasta "Akil SB" = 7 caracteres

            return caminhoPadrao;
        }

        public bool AtualizeBancoDeDados(string caminhoArquivoSql)
        {
            if (File.Exists(caminhoArquivoSql+"\\Atualizacao_Atual.sql"))
            {
                ServicoBackup servicoBackup = new ServicoBackup(false, true);

                return servicoBackup.ExecuteScriptDeAtualizacao(caminhoArquivoSql + "\\Atualizacao_Atual.sql");
            }

            return false;
        }
        public void ChamarBarraProgresso(FormProgress formProgress)
        {
            //Barra de Progresso...
            this.Cursor = Cursors.WaitCursor;
            formProgress.Show();
            formProgress.ProgressBar.Increment(50);
        }

        public void FecharBarraProgreso(FormProgress formProgress)
        {
            //Fim da Barra de Progresso.
            this.Cursor = Cursors.Default;
            formProgress.ProgressBar.Increment(100);
            formProgress.Close();
        }

        public void FecharForms()
        {
            Application.OpenForms["FormAtualizacaoSistema"].Close();
            this.Close();

            Application.OpenForms["FormPrincipal"].Close();
            this.Close();
        }

        public void ChamarInstalador(string caminhoExecutavel)
        {
            System.Diagnostics.Process.Start(caminhoExecutavel+ "\\ExtrairArquivosAkil.exe");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BaixarAtualizar();
        }
    }
}
