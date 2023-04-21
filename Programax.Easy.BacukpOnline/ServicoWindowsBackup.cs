using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using Programax.Easy.Servico.ConfiguracoesSistema.BackupServ;
using SevenZip;
using System.Net;
using Programax.Easy.Servico;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections;
using System.Timers;
using Newtonsoft.Json;

namespace Programax.Easy.BacukpOnline
{
    partial class ServicoWindowsBackup : ServiceBase
    {
        #region " CONSTANTES "

        private const string FTPUSERNAME = "akilsistemas1";
        private const string FTPPASSWORD = "Gyn4k1l2015";

        #endregion

        #region " VARIÁVEIS PRIVADAS "

        private string _diretorioUpload;
        private string _diretorioLocalBackup;
        private string _enderecoCompletoArquivoLog;
        private Timer _timer;
        private bool _emExecucao;
        private bool _primeiroAcesso;
        private List<string> _listaLog;
        private DateTime? _executarBackupDeterminadoHorario;

        #endregion

        #region " CONSTRUTOR "

        public ServicoWindowsBackup()
        {
            InitializeComponent();

            _timer = new Timer(13000);
            _timer.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
        }

        #endregion

        #region " MÉTODOS DE START E STOP SERVIÇO E TIMER"

        protected override void OnStart(string[] args)
        {
            _primeiroAcesso = true;

            _enderecoCompletoArquivoLog = RetorneDiretorioBackup() + @"\log.txt";

            MapeieBanco();

            _timer.Start();
        }

        protected override void OnStop()
        {

        }

        private void timer1_Elapsed(object sender, EventArgs e)
        {
            if (!_emExecucao)
            {
                if (PodeExecutarBackup())
                {
                    _emExecucao = true;

                    EfetueBackup();

                    _emExecucao = false;
                }
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void EfetueBackup()
        {
            EscrevaLog("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] Realizando Backup");

            string diretorioBackup = RetorneDiretorioBackup();

            if (!Directory.Exists(diretorioBackup))
            {
                Directory.CreateDirectory(diretorioBackup);
            }

            string nomeArquivoBakcup = "bkpAkil-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sql";

            ServicoBackup servicoBackup = new ServicoBackup(false, false);
            var backupGeradoComSucesso = servicoBackup.EfetueBackup(diretorioBackup, nomeArquivoBackup: nomeArquivoBakcup, estiloJanelaProcesso: ProcessWindowStyle.Hidden);

            if (backupGeradoComSucesso)
            {
                EscrevaLog("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] O Backup foi realizado com sucesso.");

                bool comprimidoComSucesso = ComprimaBackup(diretorioBackup, nomeArquivoBakcup);

                if (comprimidoComSucesso)
                {
                    EnvieBackupComprimidoParaServidor(diretorioBackup, nomeArquivoBakcup);
                }
            }
            else
            {
                EscrevaLog("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] O Backup não foi gerado ou foi gerado com erros.");
                EscrevaLog(string.Empty);
                EscrevaLog(string.Empty);

                AgendeBackupParaExecutarNovamente();
            }
        }

        private bool ComprimaBackup(string diretorioBackup, string nomeArquivoBackup)
        {
            try
            {
                EscrevaLog("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] Comprimindo Backup.");

                SevenZipCompressor.SetLibraryPath(InfraUtils.RetorneDiretorioAplicacao() + @"\7z.dll");
                SevenZipCompressor sevenZipCompressor = new SevenZipCompressor();
                sevenZipCompressor.CompressionLevel = SevenZip.CompressionLevel.Ultra;
                sevenZipCompressor.CompressionMethod = CompressionMethod.Lzma;

                sevenZipCompressor.CompressFiles(diretorioBackup + @"\" + nomeArquivoBackup.Replace(".sql", ".7z"), diretorioBackup + @"\" + nomeArquivoBackup);

                File.Delete(diretorioBackup + @"\" + nomeArquivoBackup);

                return true;
            }
            catch (Exception)
            {
                EscrevaLog("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] Erro ao Comprimir Backup.");

                AgendeBackupParaExecutarNovamente();

                return false;
            }
        }

        private void EnvieBackupComprimidoParaServidor(string diretorioBackup, string nomeArquivoBackup)
        {
            string textoInicialLtbLog = "[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] Enviando Backup para Nossos Servidores.";

            EscrevaLog(textoInicialLtbLog);

            try
            {
                CrieDiretorioServidor();

                FtpWebRequest ftpClient = (FtpWebRequest)FtpWebRequest.Create(RetorneDiretorioUpload() + @"\" + nomeArquivoBackup.Replace(".sql", ".7z"));
                ftpClient.Credentials = new System.Net.NetworkCredential(FTPUSERNAME, FTPPASSWORD);

                ftpClient.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                ftpClient.UseBinary = true;
                ftpClient.KeepAlive = true;
                System.IO.FileInfo fi = new System.IO.FileInfo(diretorioBackup + @"\" + nomeArquivoBackup.Replace(".sql", ".7z"));
                ftpClient.ContentLength = fi.Length;
                byte[] buffer = new byte[4097];
                int bytes = 0;
                int total_bytes = (int)fi.Length;
                int total_bytes_original = total_bytes;
                System.IO.FileStream fs = fi.OpenRead();
                System.IO.Stream rs = ftpClient.GetRequestStream();
                int contadorIteracoes = 0;
                while (total_bytes > 0)
                {
                    bytes = fs.Read(buffer, 0, buffer.Length);
                    rs.Write(buffer, 0, bytes);
                    total_bytes = total_bytes - bytes;

                    if (contadorIteracoes % 45 == 0)
                    {
                        int porcentagem = total_bytes * 100 / total_bytes_original;
                        porcentagem = 100 - porcentagem;

                        RemovaUltimaLinhaLog();
                        EscrevaLog(textoInicialLtbLog + "[" + porcentagem + " %]");
                    }

                    contadorIteracoes++;
                }

                RemovaUltimaLinhaLog();
                EscrevaLog(textoInicialLtbLog + "[100 %]");

                fs.Close();
                rs.Close();
                FtpWebResponse uploadResponse = (FtpWebResponse)ftpClient.GetResponse();
                var value = uploadResponse.StatusDescription;
                uploadResponse.Close();

                EscrevaLog("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] Backup Enviado para Nossos Servidores.");
                EscrevaLog(string.Empty);
                EscrevaLog(string.Empty);
                EscrevaLog("|");
            }
            catch
            {
                EscrevaLog("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] Erro ao Enviar Backup para Nossos Servidores.");
                EscrevaLog(string.Empty);
                EscrevaLog(string.Empty);
                AgendeBackupParaExecutarNovamente();
            }
        }

        private void CrieDiretorioServidor()
        {
            var request = (FtpWebRequest)WebRequest.Create(RetorneDiretorioUpload());
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);

            try
            {
                using (request.GetResponse())
                {
                    request.Abort();

                    return;
                }
            }
            catch (WebException)
            {
                request.Abort();

                FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(RetorneDiretorioUpload()));

                requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestDir.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                requestDir.UsePassive = true;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
        }

        private string RetorneDiretorioUpload()
        {
            if (string.IsNullOrEmpty(_diretorioUpload))
            {
                ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
                var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

                _diretorioUpload = "ftp://" + "backup.akilsistemas.com.br/backups/" + empresa.DadosEmpresa.Cnpj.RemoverCaracteresDeMascara();
            }

            return _diretorioUpload;
        }

        private string RetorneDiretorioBackup()
        {
            if (string.IsNullOrEmpty(_diretorioLocalBackup))
            {
                _diretorioLocalBackup = InfraUtils.RetorneDiretorioAplicacao() + @"\..\BkpAkilSB";

                if (!Directory.Exists(_diretorioLocalBackup))
                {
                    Directory.CreateDirectory(_diretorioLocalBackup);
                }
            }

            return _diretorioLocalBackup;
        }

        private void MapeieBanco()
        {
            while (true)
            {
                try
                {
                    StructureMap.ObjectFactory.Initialize(
                               x =>
                               {
                                   x.Scan(scan =>
                                   {
                                       scan.Assembly(typeof(RegistroDeMapeamentos).Assembly);
                                       scan.WithDefaultConventions();
                                   }
                                       );
                                   x.AddRegistry<RegistroDeMapeamentos>();
                               });

                    break;
                }
                catch
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        private void EscrevaLog(string texto)
        {
            if (_listaLog == null)
            {
                _listaLog = new List<string>();
            }

            _listaLog.Add(texto);

            try
            {
                System.IO.File.WriteAllText(_enderecoCompletoArquivoLog, string.Join("\r\n", _listaLog));
            }
            catch
            {

            }
        }

        private void RemovaUltimaLinhaLog()
        {
            _listaLog.RemoveAt(_listaLog.Count - 1);
        }

        private bool PodeExecutarBackup()
        {
            List<ConfiguracoesBackup> configuracaoBackup = null;

            if (File.Exists(_diretorioLocalBackup + @"\config.json"))
            {
                string configuracoesString = System.IO.File.ReadAllText(_diretorioLocalBackup + @"\config.json");

                configuracaoBackup = JsonConvert.DeserializeObject<List<ConfiguracoesBackup>>(configuracoesString);
            }
            else
            {
                configuracaoBackup = new List<ConfiguracoesBackup>();

                configuracaoBackup.Add(new ConfiguracoesBackup { HoraEMinutos = "03:00" });
                configuracaoBackup.Add(new ConfiguracoesBackup { HoraEMinutos = "07:00" });
                configuracaoBackup.Add(new ConfiguracoesBackup { HoraEMinutos = "12:00" });
                configuracaoBackup.Add(new ConfiguracoesBackup { HoraEMinutos = "18:00" });
                configuracaoBackup.Add(new ConfiguracoesBackup { HoraEMinutos = "19:00" });

                var stringConfig = JsonConvert.SerializeObject(configuracaoBackup);

                System.IO.File.WriteAllText(_diretorioLocalBackup + @"\config.json", stringConfig);
            }

            if (_primeiroAcesso || (
                _executarBackupDeterminadoHorario != null &&
                _executarBackupDeterminadoHorario.Value.Hour == DateTime.Now.Hour &&
                _executarBackupDeterminadoHorario.Value.Minute == DateTime.Now.Minute))
            {
                _primeiroAcesso = false;
                _executarBackupDeterminadoHorario = null;

                return true;
            }

            List<int> listaHoras = new List<int>();
            List<int> listaMinutos = new List<int>();

            foreach (var item in configuracaoBackup)
            {
                var horaEMinuto = item.HoraEMinutos.Split(':').ToList();

                listaHoras.Add(horaEMinuto[0].ToInt());
                listaMinutos.Add(horaEMinuto[1].ToInt());
            }

            if (listaHoras.Exists(x => x == DateTime.Now.Hour) && listaMinutos.Exists(y => y == DateTime.Now.Minute))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AgendeBackupParaExecutarNovamente()
        {
            _executarBackupDeterminadoHorario = DateTime.Now.AddMinutes(5);

            EscrevaLog("O backup será executado novamente às " + _executarBackupDeterminadoHorario.Value.ToString("HH:mm"));

            EscrevaLog(string.Empty);
            EscrevaLog(string.Empty);
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ConfiguracoesBackup
        {
            public string HoraEMinutos { get; set; }
        }

        #endregion
    }
}
