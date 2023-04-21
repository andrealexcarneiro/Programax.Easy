using System;
using System.Windows.Forms;
using Programax.Easy.Servico.ConfiguracoesSistema.UsuarioServ;
using System.Threading;
using Programax.Easy.Servico;
using Programax.Easy.Servico.Cadastros.RamoAtividadeServ;
using Programax.Easy.Servico.Cadastros.OrigemClienteServ;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using Programax.Easy.Servico.ConfiguracoesSistema.LicencaDeUsoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.ConfiguracoesSistema.Liberacao;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.AtualizacaoSistema;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using System.IO;
using System.Net;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.Login
{
    public partial class FormLogin : Form
    {
        private bool _carregarConfiguracoesBanco;
        private Empresa _empresa;
        public FormLogin()
        {
            InitializeComponent();

           
            Control.CheckForIllegalCrossThreadCalls = false;

            _carregarConfiguracoesBanco = true;

            this.ActiveControl = txtLogin;

         
        }

        public FormLogin(bool carregarConfiguracoesBanco)
        {
            InitializeComponent();

            this.ActiveControl = txtLogin;

            _carregarConfiguracoesBanco = carregarConfiguracoesBanco;

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private bool CarregueConfiguracoesDoBanco()
        {
            FormEscolhaBancoDeDados formEscolhaBancoDeDados = new FormEscolhaBancoDeDados();
            formEscolhaBancoDeDados.ShowDialog();

            var splashScreen = new SplashScreen();
            splashScreen.ShowDialog();

            if (splashScreen.Resultado == DialogResult.Cancel)
            {
                this.Close();

                return false;
            }

            return true;
        }

        private void PesquiseCadastrosBasicos()
        {
            ServicoRamoAtividade servicoRamoAtividade = new ServicoRamoAtividade();
            servicoRamoAtividade.ConsulteListaAtiva();

            ServicoOrigemCliente servicoOrigemCliente = new ServicoOrigemCliente();
            servicoOrigemCliente.ConsulteListaAtiva();

            ServicoTabelaPreco servicoTabelaPreco = new ServicoTabelaPreco();
            servicoTabelaPreco.ConsulteListaTabelaPrecosAtivas();

            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            servicoFormaPagamento.ConsulteListaFormasDePagamentoAtivas();

            ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();
            servicoCondicaoPagamento.ConsulteListaCondicoesPagamentoAtivas();


           

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            ServicoLicencaDeUso servicoLicencaDeUso = new ServicoLicencaDeUso();
            var licenca = servicoLicencaDeUso.ConsulteUltimaLicencaDeUso();

            if (!string.IsNullOrEmpty(licenca.ChaveLiberacao))
            {
                var diasParaExpirar = licenca.LiberadoAte - DateTime.Now;

                if ((diasParaExpirar.Days.ToInt() >= 0 && diasParaExpirar.Days.ToInt() <= 3))
                {
                    MessageBox.Show("Faltam " + diasParaExpirar.Days + " (DIAS) para EXPIRAR sua <Chave de Liberação>!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
            ServicoUsuario servicoUsurio = new ServicoUsuario();

            var usuarioEstahAutenticado = servicoUsurio.AutenticarUsuario(txtLogin.Text, txtSenha.Text);

            if (usuarioEstahAutenticado)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
            }
            else
            {
                lblUsuarioOuSenhaIncorretos.Show();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            if (_carregarConfiguracoesBanco)
            {
                bool bancoCarregado = CarregueConfiguracoesDoBanco();

                if (!bancoCarregado)
                {
                    return;
                }
            }

            ServicoLicencaDeUso servicoLicencaDeUso = new ServicoLicencaDeUso();
            var licenca = servicoLicencaDeUso.ConsulteUltimaLicencaDeUso();
            
            if (!string.IsNullOrEmpty(licenca.ChaveLiberacao))
            {
                BusqueEAtualizaeChaveDeLiberacao();
                InsiraChaveDeLiberacao();
                _empresa = new Empresa();


                ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
                _empresa = servicoEmpresa.ConsulteUltimaEmpresa();

                if (_empresa != null)
                {
                    switch (_empresa.DadosEmpresa.Config)
                    {
                        case 0:
                            panel1.BackgroundImage = global::Programax.Easy.View.Properties.Resources.Tela_login2;
                            break;
                        case 1:
                            panel1.BackgroundImage = global::Programax.Easy.View.Properties.Resources.Tela_login3;
                            break;
                        default:
                            panel1.BackgroundImage = global::Programax.Easy.View.Properties.Resources.Tela_login2;
                            break;
                    }
                }
            }
            else
            {
              
                ExibaTelaDeBoasVindas();
            }
        }

        private void InsiraChaveDeLiberacao()
        {
            ServicoLicencaDeUso servicoLicencaDeUso = new ServicoLicencaDeUso();
            var licenca = servicoLicencaDeUso.ConsulteUltimaLicencaDeUso();

            var chaveDescriptografada = licenca.ChaveLiberacao.ToStringEmpty().Descriptografar();

            var parametrosChave = chaveDescriptografada.Split('|');

            DateTime dataLiberacao = DateTime.MinValue;

            if (parametrosChave.Length == 3)
            {
                dataLiberacao = parametrosChave[1].ToDate();
            }

            if (DateTime.Now.Date > dataLiberacao)
            {
                FormLiberacao formLiberacao = new FormLiberacao();
                var resultado = formLiberacao.AlterarChaveLiberacao();

                if (resultado == DialogResult.Cancel)
                {
                    this.Close();

                    return;
                }
            }
        }

        private void BusqueEAtualizaeChaveDeLiberacao()
        {
            ServicoLicencaDeUso servicolicenca = new ServicoLicencaDeUso();

            var dadosLicenca = servicolicenca.ConsulteUltimaLicencaDeUso();

            var chaveDescriptografada = dadosLicenca.ChaveLiberacao.ToStringEmpty().Descriptografar();

            var parametrosChave = chaveDescriptografada.Split('|');

            DateTime dataLiberacao = DateTime.MinValue;

            if (parametrosChave.Length == 3)
            {
                dataLiberacao = parametrosChave[1].ToDate();
            }

            DateTime dataModificacaoDoArquivoLiberacao = DateTime.Now.AddDays(1);

            VerificaDataDeArquivoLiberacao(ref dataModificacaoDoArquivoLiberacao);

            if (DateTime.Now.Date >= dataLiberacao.AddDays(-3) || dataModificacaoDoArquivoLiberacao.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
            {
                var NomeArquivoLiberacao = BuscaNomeDoArquivo();

                BaixarArquivoComChaveLiberacao(NomeArquivoLiberacao);

                var chaveDeLiberacao = LeiaArquivoERetorneChaveDeLiberacao(NomeArquivoLiberacao);

                if (chaveDeLiberacao != string.Empty)
                {
                    try
                    {
                        dadosLicenca.ChaveLiberacao = chaveDeLiberacao;

                        servicolicenca.Atualize(dadosLicenca);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    
                }
            }
        }
        
        private void BaixarArquivoComChaveLiberacao(string NomeArquivo)
        {
            try
            {
                /* Create Object Instance */
                FTP ftpClient = new FTP(@"ftp://site1393677047@ftp.site1393677047.hospedagemdesites.ws", "site1393677047", "PHD@2018");

                var parametros = new ServicoParametros().ConsulteParametros().ParametrosCadastros;

                if (string.IsNullOrEmpty(parametros.CaminhoACBR))
                    parametros.CaminhoACBR = "C:\\Programax";

                if (!Directory.Exists(@parametros.CaminhoACBR))
                {
                    Directory.CreateDirectory(@parametros.CaminhoACBR);
                }
                
                /* Download a File */
                ftpClient.download("public_html/Libera/"+NomeArquivo, @parametros.CaminhoACBR+ "\\" + NomeArquivo);

                ftpClient = null;
            }
            catch (Exception)
            {
                return;
            }    
        }

        private string LeiaArquivoERetorneChaveDeLiberacao(string NomeArquivo)
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();
            string caminhoACBR = "C:\\Programax";

            if (!string.IsNullOrEmpty(parametros.ParametrosCadastros.CaminhoACBR))
            {
                caminhoACBR = parametros.ParametrosCadastros.CaminhoACBR;
            }

            string textoArquivo;

            if (System.IO.File.Exists(@caminhoACBR+"\\"+ NomeArquivo))
            {
                textoArquivo = System.IO.File.ReadAllText(@caminhoACBR +"\\"+ NomeArquivo);

                if (textoArquivo != string.Empty)
                {
                    return textoArquivo;
                }
            }

            return string.Empty;
        }
        
        public void VerificaDataDeArquivoLiberacao(ref DateTime dataModificacaoArquivoLiberacao)
        {
            try
            {
                var NomeArquivo = BuscaNomeDoArquivo();

                FtpWebRequest request = FtpWebRequest.Create(@"ftp://site1393677047@ftp.site1393677047.hospedagemdesites.ws" + "/" + "public_html/Libera/" + NomeArquivo) as FtpWebRequest;
                request.Credentials = new NetworkCredential("site1393677047", "PHD@2018");

                //Get the DATE & TIME stamp of the file
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                dataModificacaoArquivoLiberacao = response.LastModified;
            }
            catch (Exception)
            {
                return;
            }
        }

        private string BuscaNomeDoArquivo()
        {
            ServicoEmpresa empresa = new ServicoEmpresa();
            var dadosEmpresa = empresa.ConsulteUltimaEmpresa();
   
            return dadosEmpresa.DadosEmpresa.Cnpj.RemoverCaracteresDeMascara() + ".txt";
        }

        private void ExibaTelaDeBoasVindas()
        {
            FormBoasVindas formBoasVindas = new FormBoasVindas();
            formBoasVindas.ShowDialog();

            if (formBoasVindas.Resultado == DialogResult.Cancel)
            {
                this.Close();
            }
        }
    }
}
