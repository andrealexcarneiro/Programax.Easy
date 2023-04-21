using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.IO;
using System.Linq;
using System.Text;
using Programax.Easy.View.Telas.Fiscal.Cests;
using Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio;
using System.Drawing;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Fiscal.Ncms
{
    public partial class FormSpedFiscal : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idNcm;
        private string mensagemSucesso = "Arquivo Importado com Sucesso!";
        private Empresa _empresa;

        private List<EntradaMercadoria> _listaEntradasMercadorias;

        private Cnae _cnaeSelecionado;
        FileStream fs = null;
        private string NomeArq;
        private Int32 iTotLinhaBloco0 = 0;
        private Int32 iTotLinhaBlocoC = 0;
        private Int32 iTotLinhaBlocoE = 0;
        private Int32 iTotLinhaBlocoH = 0;

        private int _intNumItemEnt = 0;

        private Int32 intTotReg0000 = 0;
        private Int32 intTotReg0001 = 0;
        private Int32 intTotReg0005 = 0;
        private Int32 intTotReg0100 = 0;
        private Int32 intTotReg0150 = 0;
        private Int32 intTotReg0190 = 0;
        private Int32 intTotReg0200 = 0;
        private Int32 intTotReg0990 = 0;

        private Int32 intTotRegC001 = 0;
        private Int32 intTotRegC100E = 0;
        private Int32 intTotRegC170E = 0;
        private Int32 intTotRegC170S = 0;
        private Int32 intTotRegC170D = 0;
        private Int32 intTotRegC190S = 0;
        private Int32 intTotRegC990 = 0;

        private Int32 intTotRegD001 = 0;
        private Int32 intTotRegD990 = 0;

        private Int32 intTotRegE001 = 0;
        private Int32 intTotRegE100 = 0;
        private Int32 intTotRegE110 = 0;
        private Int32 intTotRegE200 = 0;
        private Int32 intTotRegE210 = 0;
        private Int32 intTotRegE250 = 0;
        private Int32 intTotRegE990 = 0;

        private Int32 intTotRegG001 = 0;
        private Int32 intTotRegG990 = 0;

        private Int32 intTotRegH001 = 0;
        private Int32 intTotRegH005 = 0;
        private Int32 intTotRegH010 = 0;
        private Int32 intTotRegH990 = 0;

        private Int32 intTotRegK001 = 0;
        private Int32 intTotRegK990 = 0;

        private Int32 intTotReg1001 = 0;
        private Int32 intTotReg1010 = 0;
        private Int32 intTotReg1990 = 0;
        private Int32 intTotReg9001 = 0;
        private Int32 intTotReg9900 = 0;

        private string  REG0000 = "0000";
        private string REG0001 = "0001";
        private string REG0005 = "0005";
        private string REG0100 = "0100";
        private string REG0150 = "0150";
        private string REG0190 = "0190";
        private string REG0200 = "0200";
        private string REG0990 = "0990";

        private string REGC001 = "C001";
        private string REGC100 = "C100";
        private string REGC170 = "C170";
        private string REGC190 = "C190";
        private string REGC990 = "C990";

        private string REGD001 = "D001";
        private string REGD990 = "D990";

        private string REGE001 = "E001";
        private string REGE100 = "E100";
        private string REGE110 = "E110";
        private string REGE200 = "E200";
        private string REGE210 = "E210";
        private string REGE250 = "E250";
        private string REGE990 = "E990";

        private string REGG001 = "G001";
        private string REGG990 = "G990";

        private string REGH001 = "H001";
        private string REGH005 = "H005";
        private string REGH010 = "H010";
        private string REGH990 = "H990";

        private string REGK001 = "K001";
        private string REGK990 = "K990";

        private string REG1001 = "1001";
        private string REG1010 = "1010";
        private string REG1990 = "1990";
        private string REG9001 = "9001";
        private string REG9900 = "9900";
        private string REG9999 = "9999";


        private Int32 intTotReg9999 = 0;
        private Int64 _IndReg9999 = 0;
        private Int64 intTotalRegistros = 0;
        #endregion

        #region " CONSTRUTOR "

        public FormSpedFiscal()
        {
            InitializeComponent();

            PreenchaPerfil();
            PreenchaFinalidade();
            PreenchaMotivo();
            PreenchaVersao();
            PreenchaIndicador();
            AtualizaEmpresa();
            //txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            //this.ActiveControl = txtCodigoNcm;

            this.NomeDaTela = "Sped Fiscal";

           
        }

        #endregion

        #region " EVENTOS CONTROLES "
        private void ZeraRegistros()
        {
            try
            {
                iTotLinhaBloco0 = 0;
                iTotLinhaBlocoC = 0;
                iTotLinhaBlocoE = 0;
                iTotLinhaBlocoH = 0;

                intTotReg0000 = 0;
                intTotReg0001 = 0;
                intTotReg0005 = 0;
                intTotReg0100 = 0;
                intTotReg0150 = 0;
                intTotReg0190 = 0;
                intTotReg0200 = 0;
                intTotReg0990 = 0;

                intTotRegC001 = 0;
                intTotRegC100E = 0;
                intTotRegC170E = 0;
                intTotRegC170S = 0;
                intTotRegC170D = 0;
                intTotRegC190S = 0;
                intTotRegC990 = 0;

                intTotRegD001 = 0;
                intTotRegD990 = 0;

                intTotRegE001 = 0;
                intTotRegE100 = 0;
                intTotRegE110 = 0;
                intTotRegE200 = 0;
                intTotRegE210 = 0;
                intTotRegE250 = 0;
                intTotRegE990 = 0;

                intTotRegG001 = 0;
                intTotRegG990 = 0;

                intTotRegH001 = 0;
                intTotRegH005 = 0;
                intTotRegH010 = 0;
                intTotRegH990 = 0;

                intTotRegK001 = 0;
                intTotRegK990 = 0;

                intTotReg1001 = 0;
                intTotReg1010 = 0;
                intTotReg1990 = 0;
                intTotReg9001 = 0;
                intTotReg9900 = 0;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            //LimpeFormulario();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "
        private void AtualizaEmpresa()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
                _empresa = servicoEmpresa.ConsulteUltimaEmpresa();


                CarregueCnae(false);

              

                NomeArq = "SpedFiscalEFD_" ;

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Cursor.Current = Cursors.Default;
                return;
            }
        }
        private void CarregueCnae(bool mostrarMensagemDeCnaeNaoEncontrado)
        {
            if (_cnaeSelecionado != null)
            {
                txtCodCnae.Text = _cnaeSelecionado.Codigo;
                txtDescCnae.Text = _cnaeSelecionado.Descricao;
            }
            else
            {
                txtDescCnae.Text = string.Empty;
                txtCodCnae.Text = string.Empty;

                if (mostrarMensagemDeCnaeNaoEncontrado)
                {
                    MessageBox.Show("CNAE informado não foi encontrado.");

                    txtCodCnae.Focus();
                }
            }
        }
        private void PreenchaPerfil()
        {
            List<PerfilCBO> listaPERFIL  = new List<PerfilCBO>();


            PerfilCBO listperfil = new PerfilCBO();
            {
                listperfil.Descricao = "Entre com o Perfil".ToString();
                listperfil.ID = "";
                listaPERFIL.Add(listperfil);
            }
            listperfil = new PerfilCBO();
            {
                listperfil.Descricao = "A".ToString();
                listperfil.ID = "A";
                listaPERFIL.Add(listperfil);
            }
            listperfil = new PerfilCBO();
            {
                listperfil.Descricao = "B".ToString();
                listperfil.ID = "B";
                listaPERFIL.Add(listperfil);
            }
            listperfil = new PerfilCBO();
            {
                listperfil.Descricao = "C".ToString();
                listperfil.ID = "C";
                listaPERFIL.Add(listperfil);
            }
            var lista = listaPERFIL;

            lista.Insert(0, null);


            cboPerfilapresentacao.Properties.DataSource = lista;
            cboPerfilapresentacao.Properties.DisplayMember = "Descricao";
            cboPerfilapresentacao.Properties.ValueMember = "ID";

            cboPerfilapresentacao.EditValue = "A";
        }
        private void PreenchaFinalidade()
        {
            List<finalidadeCBO> listaFINALIDADE = new List<finalidadeCBO>();


            finalidadeCBO listfinalidade = new finalidadeCBO();
            {
                listfinalidade.Descricao = "Entre com a Finalidade".ToString();
                listfinalidade.ID = "";
                listaFINALIDADE.Add(listfinalidade);
            }
            listfinalidade = new finalidadeCBO();
            {
                listfinalidade.Descricao = "Arquivo Original".ToString();
                listfinalidade.ID = "0";
                listaFINALIDADE.Add(listfinalidade);
            }
            listfinalidade = new finalidadeCBO();
            {
                listfinalidade.Descricao = "Arquivo Substituto".ToString();
                listfinalidade.ID = "1";
                listaFINALIDADE.Add(listfinalidade);
            }
            
            var lista = listaFINALIDADE;

            lista.Insert(0, null);


            cboFinalidade.Properties.DataSource = lista;
            cboFinalidade.Properties.DisplayMember = "Descricao";
            cboFinalidade.Properties.ValueMember = "ID";

            cboFinalidade.EditValue = "0";
        }
        private void PreenchaMotivo()
        {
            List<motivoCBO> listaMOTIVO = new List<motivoCBO>();


            motivoCBO listmotivo = new motivoCBO();
            {
                listmotivo.Descricao = "Entre com o filtro".ToString();
                listmotivo.ID = "";
                listaMOTIVO.Add(listmotivo);
            }
            listmotivo = new motivoCBO();
            {
                listmotivo.Descricao = "Final do Período".ToString();
                listmotivo.ID = "1";
                listaMOTIVO.Add(listmotivo);
            }
            listmotivo = new motivoCBO();
            {
                listmotivo.Descricao = "Mudança de Forma Trib.".ToString();
                listmotivo.ID = "2";
                listaMOTIVO.Add(listmotivo);
            }
            listmotivo = new motivoCBO();
            {
                listmotivo.Descricao = "Baixa Cadastral".ToString();
                listmotivo.ID = "3";
                listaMOTIVO.Add(listmotivo);
            }
            listmotivo = new motivoCBO();
            {
                listmotivo.Descricao = "Alteração Regime Pag.".ToString();
                listmotivo.ID = "4";
                listaMOTIVO.Add(listmotivo);
            }
            listmotivo = new motivoCBO();
            {
                listmotivo.Descricao = "Determinação dos Fisco".ToString();
                listmotivo.ID = "5";
                listaMOTIVO.Add(listmotivo);
            }
            var lista = listaMOTIVO;

            lista.Insert(0, null);


            cbomotivo.Properties.DataSource = lista;
            cbomotivo.Properties.DisplayMember = "Descricao";
            cbomotivo.Properties.ValueMember = "ID";

            cbomotivo.EditValue = "1";
        }
        private void PreenchaVersao()
        {
            List<versaoCBO> listaVERSAO = new List<versaoCBO>();


            versaoCBO listversao = new versaoCBO();
            {
                listversao.Descricao = "Entre com a Versão".ToString();
                listversao.ID = "0";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "001 - 100".ToString();
                listversao.ID = "1";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "002 - 101".ToString();
                listversao.ID = "2";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "003 - 102".ToString();
                listversao.ID = "3";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "004 - 103".ToString();
                listversao.ID = "4";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "005 - 104".ToString();
                listversao.ID = "5";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "006 - 105".ToString();
                listversao.ID = "6";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "007 - 106".ToString();
                listversao.ID = "7";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "008 - 107".ToString();
                listversao.ID = "8";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "009 - 108".ToString();
                listversao.ID = "9";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "010 - 109".ToString();
                listversao.ID = "10";
                listaVERSAO.Add(listversao);
            }
            listversao = new versaoCBO();
            {
                listversao.Descricao = "011 - 110".ToString();
                listversao.ID = "11";
                listaVERSAO.Add(listversao);
            }

            
            var lista = listaVERSAO;

            lista.Insert(0, null);


            cboVersao.Properties.DataSource = lista;
            cboVersao.Properties.DisplayMember = "Descricao";
            cboVersao.Properties.ValueMember = "ID";

            cboVersao.EditValue = "0";
        }
        private void PreenchaIndicador()
        {
            List<indicadorCBO> listaINDICADOR = new List<indicadorCBO>();


            indicadorCBO listindicador = new indicadorCBO();
            {
                listindicador.Descricao = "Entre com o Indicador".ToString();
                listindicador.ID = "";
                listaINDICADOR.Add(listindicador);
            }
            listindicador = new indicadorCBO();
            {
                listindicador.Descricao = "0 - Industrial ou equiparado a industrial".ToString();
                listindicador.ID = "0";
                listaINDICADOR.Add(listindicador);
            }
            listindicador = new indicadorCBO();
            {
                listindicador.Descricao = "1 - Outros".ToString();
                listindicador.ID = "1";
                listaINDICADOR.Add(listindicador);
            }
           

            var lista = listaINDICADOR;

            lista.Insert(0, null);


            cboIndicador.Properties.DataSource = lista;
            cboIndicador.Properties.DisplayMember = "Descricao";
            cboIndicador.Properties.ValueMember = "ID";

            cboIndicador.EditValue = "1";
        }
        
        #endregion

        
       
        private void btnSelecionarDiretorio_Click(object sender, EventArgs e)
        {
          
        }
        private class PerfilCBO
        {
            public string ID { get; set; }
            public string Descricao { get; set; }

        }
        private class finalidadeCBO
        {
            public string ID { get; set; }
            public string Descricao { get; set; }

        }
        private class motivoCBO
        {
            public string ID { get; set; }
            public string Descricao { get; set; }

        }
        private class versaoCBO
        {
            public string ID { get; set; }
            public string Descricao { get; set; }

        }
        private class indicadorCBO
        {
            public string ID { get; set; }
            public string Descricao { get; set; }

        }

        private void btnAtender_Click(object sender, EventArgs e)
        {
            string _strArquivo = txtDiretorioPadrao.Text + @"\" + NomeArq + "_" + DateTime.Parse(txtDataInicial.Text).ToString("ddMMyyyy") + "_" + DateTime.Parse(txtDataFinal.Text).ToString("ddMMyyyy") + ".txt";
            ZeraRegistros();
            Montando_Arquivo(_strArquivo);
            
        }
        private bool Montando_Arquivo(string _strArquivo)
        {
            try
            {
                string sNomeArq = null;

                //Montando_Arquivo = false;
                Application.DoEvents();
                fs = File.Open(_strArquivo, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);

                //TolProgresso.Value = 0;
                //TolProgresso.Maximum = 1000;
                Cursor.Current = Cursors.WaitCursor;

                Application.DoEvents();
                if (!this.Registro_0000(sr))
                {
                    sr.Close();
                    return false;
                }


                Application.DoEvents();

                if (!this.Registro_0001(sr))
                {
                    sr.Close();
                    return false;
                }
                Application.DoEvents();

                if (!this.Registro_0005(sr))
                {
                    sr.Close();
                    return false;
                }
                Application.DoEvents();

                if (!this.Registro_0100(sr))
                {
                    sr.Close();
                    return false;
                }
                Application.DoEvents();

                if (!this.Registro_0150(sr))
                {
                    sr.Close();
                    return false;
                }
                Application.DoEvents();

                if (!this.Registro_0190(sr))
                {
                    sr.Close();
                    return false;
                }
                Application.DoEvents();

                //if (!this.Registro_0200(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_0990(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_C001(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_C100E(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_C100S(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_C100D(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_C100Canc(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_C990(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_D001(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_D990(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_E001(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_E110(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_E200(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_E990(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_G001(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_G990(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_H001(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_H005(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_H010(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_H990(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_K001(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_K990(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_1001(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_1010(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_1990(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro_9001(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!this.Registro9900(sr))
                //{
                //    sr.Close();
                //    return false;
                //}
                //Application.DoEvents();

                //if (!Registro_9999(sr))
                //{
                //    sr.Close();
                //    return false;
                //}


                sr.Close();


                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool Registro_0000(StreamWriter sr)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

               // tolStatus.Text = "Montando Registro : " + REG0000;

                string sLinha;

                string sVersao = cboVersao.EditValue.ToString();
                string sFinalidade = cboFinalidade.Text.Substring(0, 1);
                string sDataInicial = DateTime.Parse(txtDataInicial.Text).ToString("ddMMyyyy");
                string sDatFinal = DateTime.Parse(txtDataFinal.Text).ToString("ddMMyyyy");
                string sNomeEmpresa = _empresa.DadosEmpresa.RazaoSocial.ToString();
                string sCNPJ = _empresa.DadosEmpresa.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                string sUF = "GO";
                string sInscricaoEstadual = _empresa.DadosEmpresa.InscricaoEstadual.Replace(".", "").Replace("-", "");
                string sINscricaoMunicipal = _empresa.DadosEmpresa.InscricaoMunicipal.Replace(".", "").Replace("-", "");
                string sInscricaoJunta = "";
                string sPerfil = cboPerfilapresentacao.Text;
                string sINdAtiv = cboIndicador.Text.Substring(0, 1);



                sLinha = "|" + REG0000 + sVersao + "|" + sFinalidade + "|" + sDataInicial + "|" + sDatFinal + "|" + sNomeEmpresa + "|" + sCNPJ + "|" + sUF + "|" + sInscricaoEstadual + "|" + sINscricaoMunicipal + "|" + sInscricaoJunta + "|" + sPerfil + "|" + sINdAtiv + "|";

                sr.WriteLine(sLinha);
                intTotReg0000 += 1;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Cursor.Current = Cursors.Default;
                return false;
            }
        }
        private bool Registro_0001(StreamWriter sr)
        {
            try
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                tolStatus.Text = "Montando Registro : " + REG0001;

                string sLinha;

                sLinha = "|" + REG0001 + intTotReg0000 + "|";

                sr.WriteLine(sLinha);
                intTotReg0001 += 1;

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Cursor.Current = Cursors.Default;
                return false;
            }
        }
        private bool Registro_0005(StreamWriter sr)
        {
            try
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                tolStatus.Text = "Montando Registro : " + REG0005;

                string sLinha;


                string sFANTASIA_Nome = _empresa.DadosEmpresa.NomeFantasia;
                string sCEP_Código = _empresa.DadosEmpresa.Endereco.CEP;
                string sEndereco = _empresa.DadosEmpresa.Endereco.Rua;
                string sNumero = _empresa.DadosEmpresa.Endereco.Numero;
                string sComplemento = _empresa.DadosEmpresa.Endereco.Complemento;
                string sBAIRRO = _empresa.DadosEmpresa.Endereco.Bairro;
                string sFONE = _empresa.DadosEmpresa.Telefone;
                string sFAX = _empresa.DadosEmpresa.Fax;
                string sEMAIL = _empresa.DadosEmpresa.Endereco.Email;

                sLinha = "|" + REG0005 + sFANTASIA_Nome + "|" + REG0005 + sCEP_Código + "|" + REG0005 + sEndereco + "|" + REG0005 + sNumero + "|" + REG0005 + sComplemento + "|" + REG0005 + sBAIRRO + "|" + REG0005 + sFONE + "|" + REG0005 + sFAX + "|" + REG0005 + sEMAIL + "|";

                sr.WriteLine(sLinha);
                intTotReg0005 += 1;

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Cursor.Current = Cursors.Default;
                return false;
            }
        }
        private bool Registro_0100(StreamWriter sr)
        {
            try
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                tolStatus.Text = "Montando Registro : " + REG0100;

                string sLinha;


                string sNomeContador = _empresa.DadosContador.Nome;
                string sCpfContador = _empresa.DadosContador.CpfContador;
                string sCRCContador = _empresa.DadosContador.Crc;
                string sCNPJContador = "";
                string sCepContador = _empresa.DadosContador.Endereco.CEP;
                string sEndContador = _empresa.DadosContador.Endereco.Rua;
                string sNumeroContador = _empresa.DadosContador.Endereco.Numero;
                string sComplContador = _empresa.DadosContador.Endereco.Complemento;
                string sBairroContador = _empresa.DadosContador.Endereco.Bairro;
                string sFoneContador = _empresa.DadosContador.Celular;
                string sFaxContador = _empresa.DadosContador.Fax;
                string sEmailContador = _empresa.DadosContador.Endereco.Email;
                string sCodIbge = "5208707"
;



                sLinha = "|" + REG0100 + sNomeContador + "|" + sCpfContador + "|" + sCRCContador + "|" + sCNPJContador + "|" + sCepContador + "|" + sEndContador + "|" + sNumeroContador + "|" + sComplContador + "|" + sBairroContador + "|" + sFoneContador + "|" + sFaxContador + "|" + sEmailContador + "|" + sCodIbge + "|";

                sr.WriteLine(sLinha);
                intTotReg0100 += 1;

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Cursor.Current = Cursors.Default;
                return false;
            }
        }
        private bool Registro_0150(StreamWriter sr)
        {
            try
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                tolStatus.Text = "Montando Registro : " + REG0150;

                string sLinha;
                string strSql;

                string sCodPart = _empresa.Id.ToString();
                string sNome = _empresa.DadosEmpresa.RazaoSocial;
                string sCodPais = "1058";
                string sCNPJParticipante = _empresa.DadosEmpresa.Cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                string sCPFParticipante = "";
                string sIEParticipante = _empresa.DadosEmpresa.InscricaoEstadual;
                string sCodIBGEParticipante = _empresa.DadosEmpresa.Endereco.Cidade.CodigoIbge;
                string sSuframa = "";
                string sEndereco = _empresa.DadosEmpresa.Endereco.Rua;
                string sNumero = _empresa.DadosEmpresa.Endereco.Numero;
                string sCompl = _empresa.DadosEmpresa.Endereco.Complemento;
                string sBairro = _empresa.DadosEmpresa.Endereco.Bairro;
                DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();
                DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();

                ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();
               
                _listaEntradasMercadorias = servicoEntradaMercadoria.ConsulteLista(txtDataInicial.Text.ToDateNullabel(),
                                                                        txtDataFinal.Text.ToDateNullabel(),
                                                                        string.Empty.ToDateNullabel(),
                                                                        string.Empty.ToDateNullabel(),
                                                                        string.Empty,
                                                                        string.Empty,
                                                                        EnumStatusEntrada.CONCLUIDA, 0);



                foreach (var entrada in _listaEntradasMercadorias)
                {

                    sCodPart = entrada.Fornecedor.DadosGerais.CpfCnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                    sNome = entrada.Fornecedor.DadosGerais.Razao;
                    //sCNPJParticipante = dr.CNPJEmitente;
                    //sCPFParticipante = "";
                    //sIEParticipante = dr.InscEstEmitente;
                    //sCodIBGEParticipante = dr.CodIBgeEmitente;
                    sSuframa = "";
                    sNumero = entrada.Fornecedor.ListaDeEnderecos[0].Numero;
                    sCompl = "";
                    sBairro = entrada.Fornecedor.ListaDeEnderecos[0].Bairro;
                    //listaDeEntradasAuxiliares.Add(entradaAuxiliar);
                    sLinha = "|" + REG0150 + sCodPart + "|" + sNome + "|" + sCodPais + "|" + sCNPJParticipante + "|" + sCPFParticipante + "|" + sIEParticipante + "|" + sCodIBGEParticipante + "|" + sSuframa + "|" + sEndereco + "|" + sNumero + "|" + sCompl + "|" + sBairro + "|" + sCodPart + "|";

                    sr.WriteLine(sLinha);
                    intTotReg0150 += 1;
                }
                // TolProgresso.Value = 0
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Cursor.Current = Cursors.Default;
                return false;
            }
        }
        private bool Registro_0190(StreamWriter sr)
        {
            try
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                tolStatus.Text = "Montando Registro : " + REG0190;

                //Dados _objDados = new Dados(clsConfiguracao.getInstancia.ConexaoString);
                //SqlDataReader dr;

                //string sLinha;
                //string strSql;

                //string sSigla = "";
                //string sDescricao = "";


                //string strCnpjEmpresa = mskCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "");

                //strSql = "Select Distinct " + "tUnidade.Sigla, tUnidade.Descricao " + "From " + "tNotaFiscalEntrada, tNFEntradaItem, tUnidade " + "Where " + "tNotaFiscalEntrada.DataEntrada Between '" + mskDtInicio.Text + "'And '" + mskDtFim.Text + " 23:59:00' And " + "tNotaFiscalEntrada.CPFCNPJCliente = '" + strCnpjEmpresa + "' And " + "tNFEntradaItem.tNotaFiscalEntrada_ID = tNotaFiscalEntrada.ID And " + "tNFEntradaItem.Unidade = tUnidade.Sigla";

                //dr = _objDados.ConsultarDados(strSql, CommandBehavior.CloseConnection);

                //if (_objDados.Erro)
                //{
                //    Cursor.Current = Cursors.Default;
                //    clsMiscelanea.PopUpErroDados(_objDados.Mensagem);
                //    return false;
                //}


                //while (dr.Read)
                //{
                //    sSigla = dr.Sigla;
                //    sDescricao = dr.Descricao;

                //    sLinha = "|" + REG0190 + sSigla + "|" + sDescricao + "|";

                //    sr.WriteLine(sLinha);
                //    intTotReg0190 += 1;
                //}

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Cursor.Current = Cursors.Default;
                return false;
            }
        }

        private void btnSelecionarDiretorio_Click_1(object sender, EventArgs e)
        {
            if (dialogDiretorio.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtDiretorioPadrao.Text = dialogDiretorio.SelectedPath;
            }
        }
    }
}
