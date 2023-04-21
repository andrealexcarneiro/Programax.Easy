using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Servico.ConfiguracoesSistema.BackupServ;
using System.IO;
using Programax.Easy.Servico;
using System.Net;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Infraestrutura.Negocio.Utils;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Programax.Easy.ViewBackupOnline
{
    public partial class FormPainelBackupOnline : Form
    {
        #region " CONSTANTES "

        private const string FTPUSERNAME = "akilsistemas1";
        private const string FTPPASSWORD = "Gyn4k1l2015";

        #endregion

        #region " VARIÁVEIS PRIVADAS "

        private string _diretorioUpload;
        private string _diretorioLocalBackup;

        #endregion

        #region " CONSTRUTOR "

        public FormPainelBackupOnline()
        {
            InitializeComponent();

            RegistreSistemaParaRodarAoIniciarWindows();

            RetorneDiretorioBackup();

            dtgArquivos.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            MapeieBanco();

            MonitoreArquivoLog();

            Control.CheckForIllegalCrossThreadCalls = false;

            PreenchaGridBackups();

            CarregueHorarios();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExportarTodosDados_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja baixar este backup?", "Download de Backup", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                string nomeArquivo = dtgArquivos.Rows[dtgArquivos.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                int tamanhoArquivoBytes = dtgArquivos.Rows[dtgArquivos.SelectedCells[0].RowIndex].Cells[4].Value.ToInt();

                string diretorioDownload = string.Empty;

                dialogSalvar.FileName = nomeArquivo;
                dialogSalvar.InitialDirectory = RetorneDiretorioBackup();

                if (dialogSalvar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    diretorioDownload = dialogSalvar.FileName;
                }
                else
                {
                    return;
                }

                Thread threadDownload = new Thread(() => BaixeArquivo(nomeArquivo, diretorioDownload, tamanhoArquivoBytes));
                threadDownload.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show("Ocorreu o seguinte erro: " + err.Message, "Erro ao baixar backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void notifyIconBackupOnline_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
                StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void FormPainelBackupOnline_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void FormPainelBackupOnline_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void btnAtualizarHorariosBackup_Click(object sender, EventArgs e)
        {
            AtualizeHorasBackup();
        }

        private void FormPainelBackupOnline_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                if (MessageBox.Show("Deseja Encerrar a Aplicação?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public void PreenchaGridBackups()
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(RetorneDiretorioUpload());
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, true);

                List<BackupGrid> listaBackupGrid = new List<BackupGrid>();

                string arquivo;
                while ((arquivo = reader.ReadLine()) != null)
                {
                    if (arquivo.IndexOf("bkpAkil-") > -1)
                    {
                        List<string> listaInformacoesArquivo = arquivo.Split(' ').ToList();

                        string dataNomeArquivo = listaInformacoesArquivo[14].Replace("bkpAkil-", "").Replace(".7z", "");

                        DateTime dataBackup = new DateTime(dataNomeArquivo.Substring(0, 4).ToInt(), dataNomeArquivo.Substring(4, 2).ToInt(), dataNomeArquivo.Substring(6, 2).ToInt(), dataNomeArquivo.Substring(8, 2).ToInt(), dataNomeArquivo.Substring(10, 2).ToInt(), dataNomeArquivo.Substring(12, 2).ToInt());

                        BackupGrid backupGrid = new BackupGrid();
                        backupGrid.NomeArquivo = listaInformacoesArquivo[14];
                        backupGrid.Data = dataBackup;
                        backupGrid.DataBackup = dataBackup.ToString("dd/MM/yyyy HH:mm:ss");
                        backupGrid.TamanhoArquivo = (listaInformacoesArquivo[10].ToInt() / (double)1048576).ToString("###,##0.000");
                        backupGrid.TamanhoArquivoEmBytes = listaInformacoesArquivo[10];

                        listaBackupGrid.Add(backupGrid);
                    }
                }

                listaBackupGrid = listaBackupGrid.OrderByDescending(x => x.Data).ToList();

                dtgArquivos.DataSource = listaBackupGrid;
                dtgArquivos.Refresh();
            }
            catch
            {
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
                if (!File.Exists(InfraUtils.RetorneDiretorioAplicacao() + @"\diretorioBackup.txt"))
                {
                    _diretorioLocalBackup = InfraUtils.RetorneDiretorioAplicacao() + @"\..\BkpAkilSB";
                    System.IO.File.WriteAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\diretorioBackup.txt", _diretorioLocalBackup);
                }
                else
                {
                    _diretorioLocalBackup = File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\diretorioBackup.txt");
                }

                if (!Directory.Exists(_diretorioLocalBackup))
                {
                    Directory.CreateDirectory(_diretorioLocalBackup);
                }
            }

            return _diretorioLocalBackup;
        }

        private void BaixeArquivo(string nomeArquivo, string diretorioDownload, int tamanhoArquivoEmBytes)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[2048];

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RetorneDiretorioUpload() + @"\" + nomeArquivo);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);

            Stream reader = request.GetResponse().GetResponseStream();
            FileStream fileStream = new FileStream(diretorioDownload, FileMode.Create);

            int totalBytesBaixados = 0;
            int contadorIteracoes = 0;

            while (true)
            {
                bytesRead = reader.Read(buffer, 0, buffer.Length);

                totalBytesBaixados += bytesRead;

                if (bytesRead == 0)
                    break;

                fileStream.Write(buffer, 0, bytesRead);

                if (contadorIteracoes % 45 == 0)
                {
                    int porcentagem = ((totalBytesBaixados / (double)tamanhoArquivoEmBytes) * 100).ToInt();

                    lblTransferenciaDownload.Text = "[" + porcentagem + " %] Baixando " + nomeArquivo + " Para " + diretorioDownload;
                }

                contadorIteracoes++;
            }

            lblTransferenciaDownload.Text = "[100 %] Baixando " + nomeArquivo + " Para " + diretorioDownload;

            fileStream.Close();
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
                    Thread.Sleep(100);
                }
            }
        }

        private void MonitoreArquivoLog()
        {
            System.IO.FileSystemWatcher watcherLog = new System.IO.FileSystemWatcher();
            watcherLog.Path = _diretorioLocalBackup;
            watcherLog.Filter = "log.txt";
            watcherLog.IncludeSubdirectories = false;
            watcherLog.EnableRaisingEvents = true;

            watcherLog.Changed += new FileSystemEventHandler(EventoAoAtualizarLog);

            AtualizeLog(podeAtualizarGridBackup: false);
        }

        private void EventoAoAtualizarLog(object sender, FileSystemEventArgs e)
        {
            AtualizeLog();
        }

        private void AtualizeLog(bool podeAtualizarGridBackup = true)
        {
            while (true)
            {
                try
                {
                    if (File.Exists(_diretorioLocalBackup + @"\log.txt"))
                    {
                        string conteudo = File.ReadAllText(_diretorioLocalBackup + @"\log.txt"); ;

                        if (!string.IsNullOrEmpty(conteudo))
                        {
                            txtLog.Text = conteudo.Replace("|", "");

                            if (podeAtualizarGridBackup)
                            {
                                if (conteudo[conteudo.Length - 1] == '|')
                                {
                                    PreenchaGridBackups();
                                }
                            }
                        }
                    }

                    break;
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }
        }

        private void CarregueHorarios()
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

            List<int> listaHoras = new List<int>();
            List<int> listaMinutos = new List<int>();

            foreach (var item in configuracaoBackup)
            {
                var horaEMinuto = item.HoraEMinutos.Split(':').ToList();

                listaHoras.Add(horaEMinuto[0].ToInt());
                listaMinutos.Add(horaEMinuto[1].ToInt());
            }

            cboHorasUm.Text = listaHoras[0].ToString("00");
            cboHorasDois.Text = listaHoras[1].ToString("00");
            cboHorasTres.Text = listaHoras[2].ToString("00");
            cboHorasQuatro.Text = listaHoras[3].ToString("00");
            cboHorasCinco.Text = listaHoras[4].ToString("00");

            cboMinutosUm.Text = listaMinutos[0].ToString("00");
            cboMinutosDois.Text = listaMinutos[1].ToString("00");
            cboMinutosTres.Text = listaMinutos[2].ToString("00");
            cboMinutosQuatro.Text = listaMinutos[3].ToString("00");
            cboMinutosCinco.Text = listaMinutos[4].ToString("00");
        }

        private void AtualizeHorasBackup()
        {
            if (MessageBox.Show("Deseja Atualizar as horas de backup?", "Atualização de Horas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                List<ConfiguracoesBackup> configuracaoBackup = new List<ConfiguracoesBackup>();

                configuracaoBackup.Add(new ConfiguracoesBackup { HoraEMinutos = cboHorasUm.Text + ":" + cboMinutosUm.Text });
                configuracaoBackup.Add(new ConfiguracoesBackup { HoraEMinutos = cboHorasDois.Text + ":" + cboMinutosDois.Text });
                configuracaoBackup.Add(new ConfiguracoesBackup { HoraEMinutos = cboHorasTres.Text + ":" + cboMinutosTres.Text });
                configuracaoBackup.Add(new ConfiguracoesBackup { HoraEMinutos = cboHorasQuatro.Text + ":" + cboMinutosQuatro.Text });
                configuracaoBackup.Add(new ConfiguracoesBackup { HoraEMinutos = cboHorasCinco.Text + ":" + cboMinutosCinco.Text });

                var stringConfig = JsonConvert.SerializeObject(configuracaoBackup);

                System.IO.File.WriteAllText(_diretorioLocalBackup + @"\config.json", stringConfig);

                MessageBox.Show("Horários Atualizados!");
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu o seguinte erro: " + e.Message, "Erro ao atualizar horários", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegistreSistemaParaRodarAoIniciarWindows()
        {
            string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

            var startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);

            startupKey.SetValue("AkilBackupOnline WinForms", Application.ExecutablePath.ToString());
            startupKey.Close();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class BackupGrid
        {
            public string DataBackup { get; set; }

            public string NomeArquivo { get; set; }

            public string TamanhoArquivo { get; set; }

            public DateTime Data { get; set; }

            public string TamanhoArquivoEmBytes { get; set; }
        }

        private class ConfiguracoesBackup
        {
            public string HoraEMinutos { get; set; }
        }

        #endregion
    }
}
