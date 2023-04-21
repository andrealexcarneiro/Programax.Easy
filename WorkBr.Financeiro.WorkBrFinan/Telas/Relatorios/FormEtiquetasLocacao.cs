using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using System.Drawing.Printing;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Fiscal.CancelamentosNotasFiscais;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using DevExpress.Data.Extensions;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormEtiquetasLocacao : FormularioBase
    {
        #region " VARIÁVEIS GLOBAIS "

        public List<DadosEtiquetas> listaDadosEtiquetas;
        public Empresa _empresa;
        public NotaFiscal _notaFiscalEmEdicao = null;
        public EntradaMercadoria _notaEntrada = null;
        int _quantidadeEtiquetasImpressas; //Conta o numero de etiquetas que estão sendo impressas
        int _quantEtiquetasASeremImpressas;
        int _QuantidadeEtiquetaPorPagina;

        #endregion

        #region "INICIALIZAÇÃO"

        public FormEtiquetasLocacao()
        {
            InitializeComponent();

            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            _empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            lblEmpresa.Text = _empresa.DadosEmpresa.RazaoSocial;
            lblFone.Text = _empresa.DadosEmpresa.Telefone;

            LeiaArquivoTextoComAsConfiguracoes();
        }

        #endregion

        #region "EVENTOS CONTROLES"

        #region "CONTROLES IMPRESSAO"

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Variaveis das linhas

            float linesPerPage = 0;
            Font printFont = this.Font;

            //    string line = null;
            float x = float.Parse(txtHorizontal.Text); //posição horizontal inicial da etiqueta em mm
            float y = float.Parse(txtVertical.Text);//posição vertical inicial da etiqueta em mm
            float EspacoEntreLinhas = float.Parse(txtEspacoEntreLinhas.Text);
            //float largura = 66;//largura da etiqueta em mm
            //float altura = 30;//altura da etiqueta em mm

            int count = 0;

            SolidBrush myBrush = new SolidBrush(Color.Black);
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);

            //Variaveis das margens
            float MargemEsq = e.MarginBounds.Left;
            float MargemSuperior = e.MarginBounds.Top + 100;
            float MargemDireita = e.MarginBounds.Right;
            float MargemInferior = e.MarginBounds.Bottom;
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;

            //Variaveis das fontes
            Font FonteNegrito = default(Font);
            Font FonteTitulo = default(Font);
            Font FonteSubTitulo = default(Font);
            Font FonteRodape = default(Font);
            Font FonteNormal = default(Font);

            //define efeitos em fontes
            FonteNegrito = new Font("Arial", 9, FontStyle.Bold);
            FonteTitulo = new Font("Arial", 15, FontStyle.Bold);
            FonteSubTitulo = new Font("Arial", 12, FontStyle.Bold);
            FonteRodape = new Font("Arial", 8);
            if (txtTipoFonte.Text != string.Empty && txtTamanhoFonte.Text != string.Empty)
                FonteNormal = new Font(txtTipoFonte.Text, txtTamanhoFonte.Text.ToInt());
            else
                FonteNormal = new Font("Arial", 9);

            //define quantas linhas por pagina

            //Aqui sao lidos os dados
            if (listaDadosEtiquetas != null && listaDadosEtiquetas.Count > 0)
            {
                _quantEtiquetasASeremImpressas = listaDadosEtiquetas.Count;

                if (_quantEtiquetasASeremImpressas < _QuantidadeEtiquetaPorPagina)
                    _QuantidadeEtiquetaPorPagina = _quantEtiquetasASeremImpressas;
                
                for (int i = 0; i < _QuantidadeEtiquetaPorPagina; i++)
                {
                    //obtem os valores do datareader
                    string Empresa = listaDadosEtiquetas[i].Empresa;
                    string FoneEmpresa = listaDadosEtiquetas[i].FoneEmpresa;
                    string DescricaoItem = listaDadosEtiquetas[i].DescricaoItem;
                    string NumeroDaNota = listaDadosEtiquetas[i].NumeroNota;
                    string Locacao = listaDadosEtiquetas[i].Locacao;

                    //******Cria um Retângulo

                    //int tamCentroFone = FoneEmpresa.Length - Empresa.Length; ---> x = vai somar se for menor ao anterior e subtrair se for maior ao anterior

                    //int tamCentroLocacao = Locacao.Length - Empresa.Length; --->

                    //inicia a impressao

                    //defini um retangulo de 66.7x25.4 mm (6.67 x 2.54 cm)
                    //e.Graphics.DrawRectangle(new Pen(myBrush), x, y,largura,altura);
                    //****** Fim.

                    //imprime os dados
                    var stringFormat = retornaAlinhamentoDaLinha();

                    e.Graphics.DrawString(Empresa, FonteNormal, Brushes.Black, x, y + 0, stringFormat);
                    e.Graphics.DrawString(FoneEmpresa, FonteNormal, Brushes.Black, x, y + EspacoEntreLinhas, stringFormat);
                    e.Graphics.DrawString(DescricaoItem, FonteNormal, Brushes.Black, x, y + EspacoEntreLinhas * 2, stringFormat);
                    
                    if (NumeroDaNota != string.Empty)
                    {
                        e.Graphics.DrawString(NumeroDaNota, FonteNormal, Brushes.Black, x, y + EspacoEntreLinhas * 3, stringFormat);
                        e.Graphics.DrawString(Locacao, FonteNormal, Brushes.Black, x, y + EspacoEntreLinhas * 4, stringFormat);
                    }
                    else
                        e.Graphics.DrawString(Locacao, FonteNormal, Brushes.Black, x, y + EspacoEntreLinhas * 3, stringFormat);

                    //soma +  "txtEspacoEntreEtiquetas.Text" -> verticalmente
                    y += float.Parse(txtEspacoEntreEtiquetas.Text);
                    count++;
                    _quantidadeEtiquetasImpressas++;
                    // a cada "txtQuantidadeEtiquetasPorColuna.Text" linhas adicicone uma coluna
                    if (count == txtQuantidadeEtiquetasPorColuna.Text.ToInt())
                    {
                        x += float.Parse(txtLargura.Text); //adicione na posicao horiz. ****Determina a Quantidade de colunas
                        y = float.Parse(txtVertical.Text);  //volta para o topo da pagina

                        count = 0; // contador de linhas volta a ser zero}
                    }
                }
            }
            
            //verifica se continua imprimindo
            if (_quantEtiquetasASeremImpressas > _quantidadeEtiquetasImpressas) //se tiver mais do que 30 etiquetas na pagina, adicione uma nova pagina
            {
                if ((_quantEtiquetasASeremImpressas - _quantidadeEtiquetasImpressas) < _QuantidadeEtiquetaPorPagina)
                    _QuantidadeEtiquetaPorPagina = _quantEtiquetasASeremImpressas - _quantidadeEtiquetasImpressas;

                e.HasMorePages = true;
                return;
            }
            else
                e.HasMorePages = false;

            myBrush.Dispose();
        }
        
        private void ImpressoraMatricial(PrintPageEventArgs e, SolidBrush myBrush, Font FonteNormal, float EspacoEntreLinhas, float x, float y)
        {
            //define quantas linhas por pagina

            //Aqui sao lidos os dados
            if (listaDadosEtiquetas != null && listaDadosEtiquetas.Count > 0)
            {
                _quantEtiquetasASeremImpressas = listaDadosEtiquetas.Count;

                if (_quantEtiquetasASeremImpressas < _QuantidadeEtiquetaPorPagina)
                    _QuantidadeEtiquetaPorPagina = _quantEtiquetasASeremImpressas;

                for (int i = 0; i < _QuantidadeEtiquetaPorPagina; i++)
                {
                    //obtem os valores do datareader
                    string Empresa = listaDadosEtiquetas[i].Empresa;
                    string FoneEmpresa = listaDadosEtiquetas[i].FoneEmpresa;
                    string DescricaoItem = listaDadosEtiquetas[i].DescricaoItem;
                    string NumeroDaNota = listaDadosEtiquetas[i].NumeroNota;
                    string Locacao = listaDadosEtiquetas[i].Locacao;
                    
                    //imprime os dados
                    var stringFormat = retornaAlinhamentoDaLinha();

                    e.Graphics.DrawString(Empresa, FonteNormal, Brushes.Black, x, y + 0, stringFormat);
                    e.Graphics.DrawString(FoneEmpresa, FonteNormal, Brushes.Black, x, y + EspacoEntreLinhas, stringFormat);
                    e.Graphics.DrawString(DescricaoItem, FonteNormal, Brushes.Black, x, y + EspacoEntreLinhas * 2, stringFormat);

                    if (NumeroDaNota != string.Empty)
                    {
                        e.Graphics.DrawString(NumeroDaNota, FonteNormal, Brushes.Black, x, y + EspacoEntreLinhas * 3, stringFormat);
                        e.Graphics.DrawString(Locacao, FonteNormal, Brushes.Black, x, y + EspacoEntreLinhas * 4, stringFormat);
                    }
                    else
                        e.Graphics.DrawString(Locacao, FonteNormal, Brushes.Black, x, y + EspacoEntreLinhas * 3, stringFormat);

                    //soma +  "txtEspacoEntreEtiquetas.Text" -> verticalmente
                    y += float.Parse(txtEspacoEntreEtiquetas.Text);
                    //count++;
                    _quantidadeEtiquetasImpressas++;

                    // a cada "txtQuantidadeEtiquetasPorColuna.Text" linhas adicicone uma coluna
                    //if (count == txtQuantidadeEtiquetasPorColuna.Text.ToInt())
                    //{
                        x += float.Parse(txtLargura.Text); //adicione na posicao horiz. ****Determina a Quantidade de colunas
                        y = float.Parse(txtVertical.Text);  //volta para o topo da pagina

                        //count = 0; // contador de linhas volta a ser zero}
                    //}
                }
            }

            ////verifica se continua imprimindo
            //if (_quantEtiquetasASeremImpressas > _quantidadeEtiquetasImpressas) //se tiver mais do que 30 etiquetas na pagina, adicione uma nova pagina
            //{
            //    if ((_quantEtiquetasASeremImpressas - _quantidadeEtiquetasImpressas) < _QuantidadeEtiquetaPorPagina)
            //        _QuantidadeEtiquetaPorPagina = _quantEtiquetasASeremImpressas - _quantidadeEtiquetasImpressas;

            //    e.HasMorePages = true;
            //    return;
            //}
            //else

            e.HasMorePages = false;

            myBrush.Dispose();
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            _quantidadeEtiquetasImpressas = 0;
            _QuantidadeEtiquetaPorPagina = txtQuantidadeEtiquetasPorPagina.Text.ToInt();

            if (cboItens.EditValue != null)
            {
                int contadorDeLinhas = 0;
                listaDadosEtiquetas = new List<DadosEtiquetas>();
                DadosEtiquetas dadosEtiquetas = new DadosEtiquetas();

                while (txtTotalEtiquetas.Text.ToInt() > contadorDeLinhas)
                {
                    dadosEtiquetas.Empresa = lblEmpresa.Text;
                    dadosEtiquetas.FoneEmpresa = lblFone.Text;
                    dadosEtiquetas.DescricaoItem = cboItens.Text;
                    dadosEtiquetas.NumeroNota = txtNumeroNota.Text;
                    dadosEtiquetas.Locacao = txtLocacao.Text;

                    listaDadosEtiquetas.Add(dadosEtiquetas);
                    contadorDeLinhas++;
                }
            }
        }

        private void printDocument1_EndPrint(object sender, PrintEventArgs e)
        {


        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Action ImprimirEtiqueta = () =>
            {
                GravaArquivoTextoConfiguracoesEtiqueta();

                //define o objeto para visualizar a impressao
                PrintPreviewDialog objPrintPreview = new PrintPreviewDialog();
                try
                {
                    //define o formulário como maximizado e com Zoom
                    var _with1 = objPrintPreview;

                    //Tamanho do Papel
                    printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Paper", txtLarguraPagina.Text.ToInt(), txtAlturaPagina.Text.ToInt());
                    
                    //Impressora
                    printDocument1.PrinterSettings.PrinterName = txtImpressora.Text != string.Empty? txtImpressora.Text : printDocument1.PrinterSettings.PrinterName;

                    _with1.Document = printDocument1;
                    _with1.WindowState = FormWindowState.Maximized;
                    _with1.PrintPreviewControl.Zoom = 1;
                    _with1.Text = "Etiquetas";

                    _with1.ShowDialog();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            };

            string msg = "";

            TratamentosDeTela.TrateInclusaoEAtualizacao(ImprimirEtiqueta, msg, msg, msg, false, controleValidar: this);
        }

        #endregion

        #region"CONTROLES PESQUISA"

        private void btnPesquisaNotaFiscal_Click(object sender, EventArgs e)
        {
            limpaControles();

            this.Cursor = Cursors.WaitCursor;

            FormRelatorioEntrada formRelatorioEntrada = new FormRelatorioEntrada(true, true);

            var notaFiscal = formRelatorioEntrada.PesquiseNotaFiscal();

            if (notaFiscal != null)
            {
                txtNumeroNota.Text = notaFiscal.IdentificacaoNotaFiscal.NumeroNota.ToString();

                PreencheCboListaDeItens(notaFiscal);

                _notaFiscalEmEdicao = notaFiscal;
            }

            this.Cursor = Cursors.Default;

            txtNumeroNota.Focus();
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            txtNumeroNota.Text = "";
            limpaControles();

            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProduto();

            List<ObjetoDescricaoCodigo> listaObjetoValor = new List<ObjetoDescricaoCodigo>();
            ObjetoDescricaoCodigo objetoDescricaoValor = new ObjetoDescricaoCodigo();

            if (produto != null)
            {
                objetoDescricaoValor.Descricao = produto.DadosGerais.Descricao;
                objetoDescricaoValor.Codigo = produto.Id;

                txtLocacao.Text = produto.Principal.Locacao;

                listaObjetoValor.Add(objetoDescricaoValor);

                preenchePropriedadeCombo(listaObjetoValor);

                cboItens.ItemIndex = 0;
            }
            preenchePropriedadeCombo(listaObjetoValor);
        }

        private void txtNumeroNota_Leave(object sender, EventArgs e)
        {
            limpaControles();

            if (txtNumeroNota.Text != string.Empty)
            {
                string codigoConsuta = txtNumeroNota.Text;

                ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                var entrada = servicoEntradaMercadoria.ConsulteNotaEntrada(codigoConsuta);

                if (entrada != null)
                {
                    PreencheCboListaDeItensEntrada(entrada);
                    _notaEntrada = entrada;
                    carregaLocacao();
                }
                else
                    limpaControles();
            }
        }

        private void cboItens_EditValueChanged(object sender, EventArgs e)
        {
            carregaLocacao();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #endregion

        #region "MÉTODOS AUXILIARES"

        private StringFormat retornaAlinhamentoDaLinha()
        {
            StringFormat stringFormat = new StringFormat();
            
            if (rdbCentro.Checked)
            {   
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
            }
            else if (rdbEsquerdo.Checked)
            {   
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
            }
            else
            {   
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;
            }

            return stringFormat;
        }

        private void GravaArquivoTextoConfiguracoesEtiqueta()
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();
            string caminhoACBR = "C:\\Programax";

            if (!string.IsNullOrEmpty(parametros.ParametrosCadastros.CaminhoACBR))
            {
                caminhoACBR = parametros.ParametrosCadastros.CaminhoACBR;
            }

            string Alinhamento = rdbCentro.Checked? rdbCentro.Text: rdbEsquerdo.Checked? rdbEsquerdo.Text:rdbDireito.Text;

            string lines = txtEspacoEntreEtiquetas.Text + "," + txtLargura.Text + "," + txtEspacoEntreLinhas.Text + "," + txtQuantidadeEtiquetasPorColuna.Text + ","
                            + txtQuantidadeEtiquetasPorPagina.Text + "," + txtHorizontal.Text + "," + txtVertical.Text + "," + txtAlturaPagina.Text + "," 
                            + txtLarguraPagina.Text + "," + txtTipoFonte.Text + "," + txtTamanhoFonte.Text + "," + Alinhamento + "," + txtImpressora.Text;

            System.IO.File.WriteAllText(@caminhoACBR+"\\ConfiguracoesEtiquetas.txt", lines);
        }

        private void LeiaArquivoTextoComAsConfiguracoes()
        {
            ServicoParametros servicoParametros = new ServicoParametros(false);
            var parametros = servicoParametros.ConsulteParametros();
            string caminhoACBR = "C:\\Programax";

            if (!string.IsNullOrEmpty(parametros.ParametrosCadastros.CaminhoACBR))
            {
                caminhoACBR = parametros.ParametrosCadastros.CaminhoACBR;
            }

            string textoArquivo;

            if (System.IO.File.Exists(@caminhoACBR+"\\ConfiguracoesEtiquetas.txt"))
            {
                textoArquivo = System.IO.File.ReadAllText(@caminhoACBR+"\\ConfiguracoesEtiquetas.txt");

                if (textoArquivo != string.Empty)
                {
                    string[] Configuracoes = textoArquivo.Split(',');

                    txtEspacoEntreEtiquetas.Text = Configuracoes[0];
                    txtLargura.Text = Configuracoes[1];
                    txtEspacoEntreLinhas.Text = Configuracoes[2];
                    txtQuantidadeEtiquetasPorColuna.Text = Configuracoes[3];
                    txtQuantidadeEtiquetasPorPagina.Text = Configuracoes[4];
                    txtHorizontal.Text = Configuracoes[5];
                    txtVertical.Text = Configuracoes[6];
                    txtAlturaPagina.Text = Configuracoes[7];
                    txtLarguraPagina.Text = Configuracoes[8];
                    txtTipoFonte.Text = Configuracoes[9];
                    txtTamanhoFonte.Text = Configuracoes[10];
                    rdbCentro.Checked = Configuracoes[11] == "Centro" ? true:false;
                    rdbDireito.Checked = Configuracoes[11] == "Direto" ? true:false;
                    rdbEsquerdo.Checked = Configuracoes[11] == "Esquerdo" ? true:false;
                    txtImpressora.Text = Configuracoes.IsValidIndex(12)? Configuracoes[12] : string.Empty;
                }
            }
        }
        
        private void PreencheCboListaDeItens(NotaFiscal notaFiscal)
        {
            List<ObjetoDescricaoCodigo> listaObjetoValor = new List<ObjetoDescricaoCodigo>();

            if (notaFiscal != null)
            {
                var lista = notaFiscal.ListaItens.ToList();

                lista.ForEach(item =>
                {
                    ObjetoDescricaoCodigo objetoDescricaoCodigo = new ObjetoDescricaoCodigo { Descricao = item.NomeProduto, Codigo = item.Produto.Id };

                    listaObjetoValor.Add(objetoDescricaoCodigo);
                });

                preenchePropriedadeCombo(listaObjetoValor);
            }
            else
                preenchePropriedadeCombo(listaObjetoValor);
        }

        private void PreencheCboListaDeItensEntrada(EntradaMercadoria entradaMercadoria)
        {
            List<ObjetoDescricaoCodigo> listaObjetoValor = new List<ObjetoDescricaoCodigo>();

            if (entradaMercadoria != null)
            {
                var lista = entradaMercadoria.ListaDeItens.ToList();

                lista.ForEach(item =>
                {
                    ObjetoDescricaoCodigo objetoDescricaoCodigo = new ObjetoDescricaoCodigo { Descricao = item.Produto.DadosGerais.Descricao, Codigo = item.Produto.Id };

                    listaObjetoValor.Add(objetoDescricaoCodigo);
                });

                preenchePropriedadeCombo(listaObjetoValor);
            }
            else
                preenchePropriedadeCombo(listaObjetoValor);
        }

        private void limpaControles()
        {
            PreencheCboListaDeItensEntrada(null);
            txtLocacao.Text = string.Empty;
        }
     
        private void preenchePropriedadeCombo(List<ObjetoDescricaoCodigo> listaObjetoValor)
        {
            cboItens.Properties.DataSource = listaObjetoValor;
            cboItens.Properties.DisplayMember = "Descricao";
            cboItens.Properties.ValueMember = "Codigo";
            
        }

        private void carregaLocacao()
        {
            if (_notaFiscalEmEdicao != null && cboItens.EditValue != null)
            {
                var item = _notaFiscalEmEdicao.ListaItens.SingleOrDefault(x => x.Produto.Id == cboItens.EditValue.ToInt());

                if (item != null)
                    txtLocacao.Text = item.Produto.Principal.Locacao.ToStringEmpty();
            }
            else if(_notaEntrada != null && cboItens.EditValue != null)
            {
                var item = _notaEntrada.ListaDeItens.SingleOrDefault(x => x.Produto.Id == cboItens.EditValue.ToInt());

                if (item != null)
                    txtLocacao.Text = item.Produto.Principal.Locacao.ToStringEmpty();
            }
        }
        
        private void ImprimirMatricial(Queue<string> filaLinhas)
        {
            //Fila de linhas que devem ser impressas
            //Queue<string> filaLinhas = new Queue<string>();


            PrintDocument p = new PrintDocument();
            //Evento PrintPage do PrintDocument
            p.PrintPage += delegate (object sender1, PrintPageEventArgs ev)
            {
                //Define a fonte utilizada para impressão
                Font printFont = new Font("Consolas", 11);
                float linesPerPage = 0;
                float yPos = 0;
                int count = 0;
                float leftMargin = ev.MarginBounds.Left;
                float topMargin = ev.MarginBounds.Top;
                string line = null;

                //Calcular o número de linhas por página
                linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);

                //Imprime cada linha da página
                while (count < linesPerPage && filaLinhas.Count > 0)
                {
                    line = filaLinhas.Dequeue();
                    yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, 0, yPos, new StringFormat());
                    count++;
                }

                //Se existir mais linhas, gera outra página
                if (line != null && filaLinhas.Count > 0)
                    ev.HasMorePages = true;
                else
                    ev.HasMorePages = false;
            };

            //Exibe o dialogo de impressão (se não for necessário, só pular o ShowDialog e chamar o .Print(); (Lembre-se de definir a impressora, ou será utilizada a padrão do windows)
            PrintDialog diag = new PrintDialog();
            diag.Document = p;
            diag.PrinterSettings.PrinterName = txtImpressora.Text;
            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                p.Print();
            }
        }
        
        #endregion
        #region "CLASSES"

        public class DadosEtiquetas
        {
            public string Empresa { get; set; }

            public string FoneEmpresa { get; set; }

            public string DescricaoItem { get; set; }

            public string NumeroNota { get; set; }

            public string Locacao { get; set; }
        }


        public class ObjetoDescricaoCodigo
        {
            public string Descricao { get; set; }
            public object Codigo { get; set; }
        }

        #endregion
        

        private void button1_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();

            txtLarguraPagina.Text = pageSetupDialog1.Document.DefaultPageSettings.PaperSize.Width.ToString();
            txtAlturaPagina.Text = pageSetupDialog1.Document.DefaultPageSettings.PaperSize.Height.ToString();
        }

        private void btnFonte_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();

            txtTipoFonte.Text = fontDialog1.Font.Name.ToString();
            txtTamanhoFonte.Text = fontDialog1.Font.Size.ToString();
        }

        private void txtTipoFonte_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnImpressora_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();

            txtImpressora.Text = printDialog1.PrinterSettings.PrinterName;
        }
    }
}
