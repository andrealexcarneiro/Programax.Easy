using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BoletoNet;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Infraestrutura.Negocio.Utils;
using System.Linq;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.PerfilEmissaoBoletoSev;
using Programax.Easy.Servico.Financeiro.ConfiguracaoBoletoSev;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;

namespace Programax.Easy.View.Telas.Financeiro.RetornoBoletos
{
    public partial class FormRetornoBoletos : FormularioPadrao
    {
        private List<ConciliacaoBoletosGrid> _listaConciliacaoGrid = new List<ConciliacaoBoletosGrid>();

        public FormRetornoBoletos()
        {
            InitializeComponent();
            carregaConfiguracoesPadrao();
        }
        
        #region Remessa
        public void GeraArquivoCNAB400(IBanco banco, Cedente cedente, BoletoNet.Boletos boletos)
        {
            try
            {
                saveFileDialog.Filter = "Arquivos de Retorno (*.rem)|*.rem|Todos Arquivos (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB400);

                    //Valida a Remessa Correspondentes antes de Gerar a mesma...
                    string vMsgRetorno = string.Empty;
                    bool vValouOK = arquivo.ValidarArquivoRemessa(cedente.Convenio.ToString(), banco, cedente, boletos, 1, out vMsgRetorno);
                    if (!vValouOK)
                    {
                        MessageBox.Show(String.Concat("Foram localizados inconsistências na validação da remessa!", Environment.NewLine, vMsgRetorno),
                                        "Teste",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        arquivo.GerarArquivoRemessa("0", banco, cedente, boletos, saveFileDialog.OpenFile(), 1);

                        MessageBox.Show("Arquivo gerado com sucesso!", "Teste",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void GeraArquivoCNAB240(IBanco banco, Cedente cedente, BoletoNet.Boletos boletos)
        {
            saveFileDialog.Filter = "Arquivos de Retorno (*.rem)|*.rem|Todos Arquivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB240);
                arquivo.GerarArquivoRemessa("1200303001417053", banco, cedente, boletos, saveFileDialog.OpenFile(), 1);

                MessageBox.Show("Arquivo gerado com sucesso!", "Teste",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
        //
        public void GeraDadosItau(TipoArquivo tipoArquivo)
        {
            DateTime vencimento = new DateTime(2007, 9, 10);

            Instrucao_Itau item1 = new Instrucao_Itau(9, 5);
            Instrucao_Itau item2 = new Instrucao_Itau(81, 10);
            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "0542", "13000");
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = "13000";

            Boleto b = new Boleto(vencimento, 1642, "198", "92082835", c);
            b.NumeroDocumento = "1008073";

            b.DataVencimento = Convert.ToDateTime("12-12-12");

            b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "DF";

            item2.Descricao += item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            b.Instrucoes.Add(item2);
            b.Cedente.ContaBancaria.DigitoAgencia = "1";
            b.Cedente.ContaBancaria.DigitoAgencia = "2";

            b.Banco = new Banco(341);

            BoletoNet.Boletos boletos = new BoletoNet.Boletos();
            boletos.Add(b);

            Boleto b2 = new Boleto(vencimento, 1642, "198", "92082835", c);
            b2.NumeroDocumento = "1008073";

            b2.DataVencimento = Convert.ToDateTime("12-12-12");

            b2.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b2.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b2.Sacado.Endereco.Bairro = "Testando";
            b2.Sacado.Endereco.Cidade = "Testelândia";
            b2.Sacado.Endereco.CEP = "70000000";
            b2.Sacado.Endereco.UF = "DF";

            item2.Descricao += item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b2.Instrucoes.Add(item1);
            b2.Instrucoes.Add(item2);
            b2.Cedente.ContaBancaria.DigitoAgencia = "1";
            b2.Cedente.ContaBancaria.DigitoAgencia = "2";

            b2.Banco = new Banco(341);

            boletos.Add(b2);

            switch (tipoArquivo)
            {
                case TipoArquivo.CNAB240:
                    GeraArquivoCNAB240(b2.Banco, c, boletos);
                    break;
                case TipoArquivo.CNAB400:
                    GeraArquivoCNAB400(b2.Banco, c, boletos);
                    break;
                default:
                    break;
            }

        }
        public void GeraDadosBanrisul()
        {
            ContaBancaria conta = new ContaBancaria();
            conta.Agencia = "051";
            conta.DigitoAgencia = "2";
            conta.Conta = "13000";
            conta.DigitoConta = "3";
            //
            Cedente c = new Cedente();
            c.ContaBancaria = conta;
            c.CPFCNPJ = "00.000.000/0000-00";
            c.Nome = "Empresa de Atacado";
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = "513035600299";//No Banrisul, esse código está no manual como 12 caracteres, por eu(sidneiklein) isso tive que alterar o tipo de int para string;
            c.Convenio = 124522;
            //
            Boleto b = new Boleto();
            b.Cedente = c;
            //
            b.DataProcessamento = DateTime.Now;
            b.DataVencimento = DateTime.Now.AddDays(15);
            b.ValorBoleto = Convert.ToDecimal(2469.69);
            b.Carteira = "1";
            b.VariacaoCarteira = "02";
            b.NossoNumero = string.Empty; //"92082835"; //** Para o "Remessa.TipoDocumento = "06", não poderá ter NossoNúmero Gerado!
            b.NumeroDocumento = "1008073";
            //
            b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "RS";

            Instrucao_Banrisul item1 = new Instrucao_Banrisul(9, 5, 0);
            b.Instrucoes.Add(item1);
            //b.Instrucoes.Add(item2);
            b.Banco = new Banco(041);

            #region Dados para Remessa:
            b.Remessa = new Remessa();
            b.Remessa.TipoDocumento = "06"; //06 - COBRANÇA ESCRITURAL
            #endregion

            //
            BoletoNet.Boletos boletos = new BoletoNet.Boletos();
            boletos.Add(b);

            GeraArquivoCNAB400(b.Banco, c, boletos);
        }
        public void GeraDadosSicredi()
        {
            ContaBancaria conta = new ContaBancaria();
            conta.Agencia = "051";
            conta.DigitoAgencia = "2";
            conta.Conta = "13000";
            conta.DigitoConta = "3";
            //
            Cedente c = new Cedente();
            c.ContaBancaria = conta;
            c.CPFCNPJ = "00000000000000";
            c.Nome = "Empresa de Atacado";
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = "12345";//No Banrisul, esse código está no manual como 12 caracteres, por eu(sidneiklein) isso tive que alterar o tipo de int para string;
            c.Convenio = 124522;
            //
            Boleto b = new Boleto();
            b.Cedente = c;
            //
            b.DataProcessamento = DateTime.Now;
            b.DataVencimento = DateTime.Now.AddDays(15);
            b.ValorBoleto = Convert.ToDecimal(2469.69);
            b.Carteira = "1";
            b.VariacaoCarteira = "02";
            b.NossoNumero = string.Empty; //"92082835"; //** Para o "Remessa.TipoDocumento = "06", não poderá ter NossoNúmero Gerado!
            b.NumeroDocumento = "1008073";
            //
            b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "RS";

            Instrucao_Sicredi item1 = new Instrucao_Sicredi(9, 5);
            b.Instrucoes.Add(item1);
            //b.Instrucoes.Add(item2);
            b.Banco = new Banco(748);

            //
            EspecieDocumento especiedocumento = new EspecieDocumento(748, "A");//(341, 1);
            b.EspecieDocumento = especiedocumento;


            #region Dados para Remessa:
            b.Remessa = new Remessa();
            b.Remessa.TipoDocumento = "A"; //A = 'A' - SICREDI com Registro
            #endregion

            //
            BoletoNet.Boletos boletos = new BoletoNet.Boletos();
            boletos.Add(b);

            GeraArquivoCNAB400(b.Banco, c, boletos);
        }
        public void GeraDadosSantander()
        {
            BoletoNet.Boletos boletos = new BoletoNet.Boletos();

            DateTime vencimento = new DateTime(2003, 5, 15);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "2269", "130000946");
            c.Codigo = "1795082";

            Boleto b = new Boleto(vencimento, 0.20m, "101", "566612457800", c);

            //NOSSO NÚMERO
            //############################################################################################################################
            //Número adotado e controlado pelo Cliente, para identificar o título de cobrança.
            //Informação utilizada pelos Bancos para referenciar a identificação do documento objeto de cobrança.
            //Poderá conter número da duplicata, no caso de cobrança de duplicatas, número de apólice, no caso de cobrança de seguros, etc.
            //Esse campo é devolvido no arquivo retorno.
            b.NumeroDocumento = "0282033";

            b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "DF";

            //b.Instrucoes.Add("Não Receber após o vencimento");
            //b.Instrucoes.Add("Após o Vencimento pague somente no Bradesco");
            //b.Instrucoes.Add("Instrução 2");
            //b.Instrucoes.Add("Instrução 3");

            //Espécie Documento - [R] Recibo
            b.EspecieDocumento = new EspecieDocumento_Santander("17");

            boletos.Add(b);

            GeraArquivoCNAB240(new Banco(33), c, boletos);
        }
        public void GeraDadosCaixa()
        {
            ContaBancaria conta = new ContaBancaria();
            conta.OperacaConta = "OPE";
            conta.Agencia = "345";
            conta.DigitoAgencia = "6";
            conta.Conta = "87654321";
            conta.DigitoConta = "0";
            //
            Cedente c = new Cedente();
            c.ContaBancaria = conta;
            c.CPFCNPJ = "00.000.000/0000-00";
            c.Nome = "Empresa de Atacado";
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = String.Concat(conta.Agencia, conta.DigitoAgencia, conta.OperacaConta, conta.Conta, conta.DigitoConta); //Na Caixa, esse código está no manual como 16 caracteres AAAAOOOCCCCCCCCD;
            //
            Boleto b = new Boleto();
            b.Cedente = c;
            //
            b.DataProcessamento = DateTime.Now;
            b.DataVencimento = DateTime.Now.AddDays(15);
            b.ValorBoleto = Convert.ToDecimal(2469.69);
            b.Carteira = "SR";
            b.NossoNumero = "92082835";
            b.NumeroDocumento = "1008073";
            EspecieDocumento ED = new EspecieDocumento(104);
            b.EspecieDocumento = ED;

            //
            b.Sacado = new Sacado("Fulano de Silva");
            b.Sacado.CPFCNPJ = "000.000.000-00";
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "RS";

            Instrucao_Caixa item1 = new Instrucao_Caixa(9, 5);
            b.Instrucoes.Add(item1);
            //b.Instrucoes.Add(item2);
            b.Banco = new Banco(104);

            #region Dados para Remessa:
            b.Remessa = new Remessa();
            b.Remessa.TipoDocumento = "2"; // SIGCB - SEM REGISTRO
            b.Remessa.CodigoOcorrencia = string.Empty;
            #endregion

            //
            BoletoNet.Boletos boletos = new BoletoNet.Boletos();
            boletos.Add(b);

            GeraArquivoCNAB240(b.Banco, c, boletos);
        }
        public void GeraDadosBancoDoNordeste()
        {
            ContaBancaria conta = new ContaBancaria();
            conta.Agencia = "21";
            conta.DigitoAgencia = "0";
            conta.Conta = "12717";
            conta.DigitoConta = "8";

            Cedente c = new Cedente();
            c.ContaBancaria = conta;
            c.CPFCNPJ = "00.000.000/0000-00";
            c.Nome = "Empresa de Atacado";

            Boleto b = new Boleto();
            b.Cedente = c;
            //
            b.DataProcessamento = DateTime.Now;
            b.DataVencimento = DateTime.Now.AddDays(15);
            b.ValorBoleto = Convert.ToDecimal(1);
            b.Carteira = "4";
            b.NossoNumero = "7777777";
            b.NumeroDocumento = "2525";
            //
            b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "RS";

            b.Banco = new Banco(004);

            EspecieDocumento especiedocumento = new EspecieDocumento(004, "1");//Duplicata Mercantil
            b.EspecieDocumento = especiedocumento;

            #region Dados para Remessa:
            b.Remessa = new Remessa();
            b.Remessa.TipoDocumento = "A";
            #endregion


            BoletoNet.Boletos boletos = new BoletoNet.Boletos();
            boletos.Add(b);

            GeraArquivoCNAB400(b.Banco, c, boletos);
        }
        #endregion Remessa

        #region Retorno
        private void LerRetorno(int codigo)
        {
            try
            {
                Banco bco = new Banco(codigo);

                openFileDialog.FileName = "";
                openFileDialog.Title = "Selecione um arquivo de retorno";
                openFileDialog.Filter = "Arquivos de Retorno (*.ret;*.crt)|*.ret;*.crt|Todos Arquivos (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (radioButtonCNAB400.Checked)
                    {
                        ArquivoRetornoCNAB400 cnab400 = null;
                        if (openFileDialog.CheckFileExists == true)
                        {
                            cnab400 = new ArquivoRetornoCNAB400();
                            //cnab400.LinhaDeArquivoLida += new EventHandler<LinhaDeArquivoLidaArgs>(cnab400_LinhaDeArquivoLida);
                            cnab400.LerArquivoRetorno(bco, openFileDialog.OpenFile());
                        }

                        if (cnab400 == null)
                        {
                            MessageBox.Show("Arquivo não processado!");
                            return;
                        }

                        //lstReturnFields.Items.Clear();
                        _listaConciliacaoGrid = new List<ConciliacaoBoletosGrid>();

                        foreach (DetalheRetorno detalhe in cnab400.ListaDetalhe)
                        {
                            ConciliacaoBoletosGrid itemGrid400 = new ConciliacaoBoletosGrid();

                            itemGrid400.Sacado = detalhe.NomeSacado.ToString().Trim();
                            itemGrid400.DataVencimento = detalhe.DataVencimento.ToString("dd/MM/yyyy");
                            itemGrid400.DataPagamento = detalhe.DataCredito.ToString("dd/MM/yyyy");
                            itemGrid400.ValorTitulo = detalhe.ValorTitulo.ToDouble();
                            itemGrid400.ValorPago = detalhe.ValorPago.ToDouble();
                            itemGrid400.NossoNumero = detalhe.NossoNumeroComDV;
                            itemGrid400.NumeroDocumento = detalhe.NumeroDocumento;
                            itemGrid400.Juros = detalhe.JurosMora.ToDouble();
                            itemGrid400.Multa = detalhe.ValorMulta.ToDouble();
                            itemGrid400.Status = retornaStatusConciliacao(itemGrid400.NumeroDocumento, itemGrid400.DataVencimento.ToDate()).Descricao();

                            if (itemGrid400.Status == (EnumStatusContaPagarReceber.CONCILIADOQUITADO.Descricao()))
                                itemGrid400.Imagem = Properties.Resources.icone_verde;
                            else if (itemGrid400.Status == (EnumStatusContaPagarReceber.QUITADO.Descricao()))
                                itemGrid400.Imagem = Properties.Resources.icone_azul;
                            else
                                itemGrid400.Imagem = Properties.Resources.icone_vermelho;

                            _listaConciliacaoGrid.Add(itemGrid400);

                            //***Detalhes list View ***Ignorar 

                            ListViewItem li = new ListViewItem(detalhe.NomeSacado.ToString().Trim());
                            li.Tag = detalhe;

                            li.SubItems.Add(detalhe.DataVencimento.ToString("dd/MM/yy"));
                            li.SubItems.Add(detalhe.DataCredito.ToString("dd/MM/yy"));

                            li.SubItems.Add(detalhe.ValorTitulo.ToString("###,###.00"));

                            li.SubItems.Add(detalhe.ValorPago.ToString("###,###.00"));
                            li.SubItems.Add(detalhe.CodigoOcorrencia.ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(detalhe.NossoNumeroComDV); // = detalhe.NossoNumero.ToString() + "-" + detalhe.DACNossoNumero.ToString());
                            li.SubItems.Add(detalhe.NumeroDocumento);
                            //lstReturnFields.Items.Add(li);

                            //Fim listview ***** Ignorar

                        }
                        preencheGrid();

                    }
                    else if (radioButtonCNAB240.Checked)
                    {
                        ArquivoRetornoCNAB240 cnab240 = null;
                        if (openFileDialog.CheckFileExists == true)
                        {
                            _listaConciliacaoGrid = new List<ConciliacaoBoletosGrid>();

                            foreach (var item in openFileDialog.FileNames)
                            {
                                openFileDialog.FileName = item;

                                cnab240 = new ArquivoRetornoCNAB240();
                                //cnab240.LinhaDeArquivoLida += new EventHandler<LinhaDeArquivoLidaArgs>()(cnab240_LinhaDeArquivoLida);
                                cnab240.LerArquivoRetorno(bco, openFileDialog.OpenFile());
                                
                                if (cnab240 == null)
                                {
                                    MessageBox.Show("Arquivo não processado!");
                                    return;
                                }
                                
                                foreach (DetalheRetornoCNAB240 detalhe in cnab240.ListaDetalhes)
                                {
                                    if (detalhe.SegmentoT.idCodigoMovimento == 6)
                                    {
                                        ConciliacaoBoletosGrid itemGrid240 = new ConciliacaoBoletosGrid();

                                        itemGrid240.Sacado = detalhe.SegmentoT.NomeSacado.ToString().Trim();
                                        itemGrid240.DataVencimento = detalhe.SegmentoT.DataVencimento.ToString("dd/MM/yyyy");
                                        itemGrid240.DataPagamento = detalhe.SegmentoU.DataCredito.ToString("dd/MM/yyyy");
                                        itemGrid240.ValorTitulo = detalhe.SegmentoT.ValorTitulo.ToDouble();
                                        itemGrid240.ValorPago = detalhe.SegmentoU.ValorPagoPeloSacado != 0 ? detalhe.SegmentoU.ValorPagoPeloSacado.ToDouble() : detalhe.SegmentoT.ValorTitulo.ToDouble();
                                        itemGrid240.NossoNumero = detalhe.SegmentoT.NossoNumero;
                                        itemGrid240.NumeroDocumento = detalhe.SegmentoT.NumeroDocumento.TrimStart('0').Trim(); // Numero do Documento
                                        itemGrid240.Juros = detalhe.SegmentoU.JurosMultaEncargos.ToDouble(); // Juros/Multas/Encargos

                                        itemGrid240.Status = retornaStatusConciliacao(itemGrid240.NumeroDocumento, itemGrid240.DataVencimento.ToDate()).Descricao();

                                        if (itemGrid240.Status == (EnumStatusContaPagarReceber.CONCILIADOQUITADO.Descricao()))
                                            itemGrid240.Imagem = Properties.Resources.icone_verde;
                                        else if (itemGrid240.Status == EnumStatusContaPagarReceber.QUITADO.Descricao() ||
                                                 itemGrid240.Status == EnumStatusContaPagarReceber.INATIVO.Descricao() ||
                                                 itemGrid240.Status == EnumStatusContaPagarReceber.CANCELADO.Descricao())
                                            itemGrid240.Imagem = Properties.Resources.icone_azul;
                                        else
                                            itemGrid240.Imagem = Properties.Resources.icone_vermelho;

                                        _listaConciliacaoGrid.Add(itemGrid240);

                                        //***listView -> Desativado

                                        ListViewItem li = new ListViewItem(detalhe.SegmentoT.NomeSacado.Trim());
                                        li.Tag = detalhe;

                                        li.SubItems.Add(detalhe.SegmentoT.DataVencimento.ToString("dd/MM/yyyy"));
                                        li.SubItems.Add(detalhe.SegmentoU.DataCredito.ToString("dd/MM/yyyy"));
                                        li.SubItems.Add(detalhe.SegmentoT.ValorTitulo.ToString("###,###.00"));
                                        li.SubItems.Add(detalhe.SegmentoU.ValorPagoPeloSacado.ToString("###,###.00"));
                                        li.SubItems.Add(detalhe.SegmentoU.CodigoOcorrenciaSacado.ToString());
                                        li.SubItems.Add("");
                                        li.SubItems.Add(detalhe.SegmentoT.NossoNumero);
                                        // lstReturnFields.Items.Add(li);

                                        //**Fim listView -> Desativado
                                    }
                                }                                
                            }
                            preencheGrid();
                        }
                    }
                    MessageBox.Show("Arquivo aberto com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao abrir arquivo de retorno.");
                MessageBoxAkil.Show ("Você pode ter escolhido o Padrão ou o Banco diferente do conteúdo do arquivo.", MessageBoxButtons.OK);
            }
        }

        private void GereIdParaGrid()
        {
            for (int i = 0; i < _listaConciliacaoGrid.Count; i++)
            {
                _listaConciliacaoGrid[i].Id = i + 1;
            }
        }

        private void preencheGrid()
        {
            GereIdParaGrid();

            gcConciliacaoBoletos.DataSource = _listaConciliacaoGrid;
            gcConciliacaoBoletos.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            gcConciliacaoBoletos.RefreshDataSource();
        }

        private EnumStatusContaPagarReceber retornaStatusConciliacao(string NumeroDocumento, DateTime dataVencimento)
        {
            ServicoContasReceber servicoContasPagarReceber = new ServicoContasReceber();

            var listaContaParaBaixar = servicoContasPagarReceber.ConsulteLista(null,
                                        Programax.Easy.Negocio.Financeiro.Enumeradores.EnumTipoOperacaoContasPagarReceber.RECEBER,
                                        null,
                                        new FormaPagamento
                                        {
                                            Id = 2,
                                            TipoFormaPagamento = Programax.Easy.Negocio.Financeiro.Enumeradores.EnumTipoFormaPagamento.BOLETOBANCARIO
                                        }
                                        , null,
                                        Programax.Easy.Negocio.Financeiro.Enumeradores.EnumDataFiltrarContasPagarReceber.VENCIMENTO,
                                        dataVencimento, dataVencimento);

            if (listaContaParaBaixar != null)
            {
                foreach (var itemBaixar in listaContaParaBaixar)
                {
                    //***Doc de retorno 
                    var NumeroDocRetornoSplit = NumeroDocumento.Split('-');

                    string NumeroDocumentRetorno = NumeroDocRetornoSplit[0];

                    //******Fim doc retorno

                    //***Doc do contas pagar receber
                    var NumeroDocumentoComparar = itemBaixar.NumeroDocumento.Split('-');

                    //if (NumeroDocumento.Trim() == "998 - 3 3")
                    //    return EnumStatusContaPagarReceber.QUITADO;

                    if (NumeroDocumentoComparar[0].Trim() == NumeroDocumentRetorno.Trim())
                    {
                        if (itemBaixar.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO)
                            return EnumStatusContaPagarReceber.CONCILIADOQUITADO;
                        else if (itemBaixar.Status == EnumStatusContaPagarReceber.QUITADO)
                            return EnumStatusContaPagarReceber.QUITADO;
                        else if (itemBaixar.Status == EnumStatusContaPagarReceber.INATIVO)
                            return EnumStatusContaPagarReceber.INATIVO;
                        else if (itemBaixar.Status == EnumStatusContaPagarReceber.CANCELADO)
                            return EnumStatusContaPagarReceber.INATIVO;
                    }
                    else if(itemBaixar.NumeroDocumento.Trim() == NumeroDocumento.Trim())
                    {
                        if (itemBaixar.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO)
                            return EnumStatusContaPagarReceber.CONCILIADOQUITADO;
                        else if (itemBaixar.Status == EnumStatusContaPagarReceber.QUITADO)
                            return EnumStatusContaPagarReceber.QUITADO;
                    }
                    else if(itemBaixar.NumeroDocumento.Replace('/',' ') == NumeroDocumento.Trim())
                    {
                        if (itemBaixar.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO)
                            return EnumStatusContaPagarReceber.CONCILIADOQUITADO;
                        else if (itemBaixar.Status == EnumStatusContaPagarReceber.QUITADO)
                            return EnumStatusContaPagarReceber.QUITADO;
                    }
                }
            }

            return EnumStatusContaPagarReceber.NAOCONCILIADO;
        }
        
        private List<DadosConciliacao> retorneListaConciliacaoDesconciliacao()
        {
            List<DadosConciliacao> listaConciliacaoDesconciliacao = new List<DadosConciliacao>();

            if (gridViewConciliacaoBoletos.SelectedRowsCount > 0)
            {
                var linhasSelecionadas = gridViewConciliacaoBoletos.GetSelectedRows();

                foreach (var item in gridViewConciliacaoBoletos.GetSelectedRows())
                {  
                    DadosConciliacao dadosConciliacao = new DadosConciliacao();

                    var lancamento = colunaId.View.GetRowCellValue(item, colunaId);

                    var itemDoGrid = _listaConciliacaoGrid.FirstOrDefault(x => x.Id == lancamento.ToInt());

                    if (itemDoGrid != null)
                    {
                        dadosConciliacao.DataDePagamento = DateTime.Parse(itemDoGrid.DataPagamento.ToString());
                        dadosConciliacao.DataDeVencimento = DateTime.Parse(itemDoGrid.DataVencimento.ToString());
                        dadosConciliacao.ValorPago = itemDoGrid.ValorPago.ToDouble();
                        dadosConciliacao.Multa = itemDoGrid.Multa;
                        dadosConciliacao.Juros = itemDoGrid.Juros;
                        dadosConciliacao.Desconto = itemDoGrid.Desconto;
                        dadosConciliacao.NumeroDoDocumento = itemDoGrid.NumeroDocumento;
                        dadosConciliacao.Status = retorneStatusContasPagarReceber(itemDoGrid.Status);

                        listaConciliacaoDesconciliacao.Add(dadosConciliacao);
                    }
                }
            }
            else
            {
                foreach (var item in _listaConciliacaoGrid)
                {
                    DadosConciliacao dadosConciliacao = new DadosConciliacao();

                    dadosConciliacao.DataDePagamento = DateTime.Parse(item.DataPagamento);
                    dadosConciliacao.DataDeVencimento = DateTime.Parse(item.DataVencimento);
                    dadosConciliacao.ValorPago = item.ValorPago.ToDouble();
                    dadosConciliacao.Desconto = item.Desconto;
                    dadosConciliacao.Juros = item.Juros;
                    dadosConciliacao.Multa = item.Multa;
                    dadosConciliacao.NumeroDoDocumento = item.NumeroDocumento;
                    dadosConciliacao.Status = retorneStatusContasPagarReceber(item.Status);

                    listaConciliacaoDesconciliacao.Add(dadosConciliacao);
                }
            }

            return listaConciliacaoDesconciliacao;
        }

        private List<ContaPagarReceberPagamento> retorneListaHistoricoDePagamentos(DateTime DataPagamento, double Valor, FormaPagamento 
                                                                                   Boleto,ContaPagarReceber ContaPagarReceber, bool EstahEstornado)
        {
            ContaPagarReceberPagamento item = new ContaPagarReceberPagamento();

            List<ContaPagarReceberPagamento> Lista = new List<ContaPagarReceberPagamento>();

            item.DataPagamento = DataPagamento;
            item.Valor = Valor;
            item.Observacoes = "Conciliação por arquivo de retorno";
            item.Responsavel = new ServicoPessoa().Consulte(Sessao.PessoaLogada.Id);
            item.FormaPagamento = Boleto;
            item.ContaPagarReceber = ContaPagarReceber;
            item.EstahEstornado = EstahEstornado;

            Lista.Add(item);

            return Lista;
        }

        private void EstorneConciliacao(ContaPagarReceber ContaPagarReceber)
        {
            var lista = ContaPagarReceber.ListaContasPagarReceberParcial.ToList();

            foreach (var item in lista)
            {  
                if(!item.EstahEstornado)
                    new ServicoContasPagarReceberPagamento().EstorneRegistro(item);
            }
        }

        private EnumStatusContaPagarReceber retorneStatusContasPagarReceber(string DescricaoContasPagarReceber)
        {
            return DescricaoContasPagarReceber == EnumStatusContaPagarReceber.CONCILIADOQUITADO.Descricao() ?
                                                  EnumStatusContaPagarReceber.CONCILIADOQUITADO://Conciliado/Quitado
                                                  DescricaoContasPagarReceber == EnumStatusContaPagarReceber.QUITADO.Descricao() ?
                                                  EnumStatusContaPagarReceber.QUITADO: //Quitado
                                                  DescricaoContasPagarReceber == EnumStatusContaPagarReceber.INATIVO.Descricao() ?
                                                  EnumStatusContaPagarReceber.INATIVO: //Inativo
                                                  DescricaoContasPagarReceber == EnumStatusContaPagarReceber.CANCELADO.Descricao() ?
                                                  EnumStatusContaPagarReceber.CANCELADO:
                                                  EnumStatusContaPagarReceber.NAOCONCILIADO;//Não Conciliado
        }

        private void carregaConfiguracoesPadrao()
        {
            ServicoPerfilEmissaoBoleto servicoPerfil = new ServicoPerfilEmissaoBoleto();

            var listaPerfis = servicoPerfil.ConsulteLista();

            foreach (var item in listaPerfis)
            {
                if (item.EhPerfilPadrao)
                {
                    ServicoConfiguracaoBoleto servicoConfiguracaoBoleto = new ServicoConfiguracaoBoleto();

                    var configuracoesBoleto = servicoConfiguracaoBoleto.ConsultePeloPerfil(item.Id);

                    radioButtonCNAB400.Checked = configuracoesBoleto.Padrao == 400 ? true : false;
                    radioButtonCNAB240.Checked = configuracoesBoleto.Padrao == 240 ? true : false;

                    ServicoBanco servicoBanco = new ServicoBanco();
                    var banco = servicoBanco.Consulte(configuracoesBoleto.Banco.Id);

                    CarregarBanco(banco.Codigo.ToInt());

                    return;
                }
            }
           
        }

        private void CarregarBanco(int CodigoBancoPerfil)
        {
            if (CodigoBancoPerfil == Convert.ToInt16(radioButtonItau.Tag))
                radioButtonItau.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonUnibanco.Tag))
                radioButtonUnibanco.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonSudameris.Tag))
                radioButtonSudameris.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonSafra.Tag))
                radioButtonSafra.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonReal.Tag))
                radioButtonReal.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonHsbc.Tag))
                radioButtonHsbc.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonBancoBrasil.Tag))
                radioButtonBancoBrasil.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonBradesco.Tag))
                radioButtonBradesco.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonCaixa.Tag))
                radioButtonCaixa.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonBNB.Tag))
                radioButtonBNB.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonSantander.Tag))
                radioButtonSantander.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonSicredi.Tag))
                radioButtonSicredi.Checked = true;
            else if (CodigoBancoPerfil == Convert.ToInt16(radioButtonSicoob.Tag))
                radioButtonSicoob.Checked = true;
        }

        private void ConciliarBoletoSicob(List<ContaPagarReceber> listaContasPagarReceber)
        {

        }

        //void cnab240_LinhaDeArquivoLida(object sender, LinhaDeArquivoLidaArgs e)
        //{
        //    MessageBox.Show(e.Linha);
        //}

        //void cnab400_LinhaDeArquivoLida(object sender, LinhaDeArquivoLidaArgs e)
        //{
        //    MessageBox.Show(e.Linha);
        //}

        #endregion Retorno

        #region Exemplos de arquivos de retorno
        public void GeraArquivoCNAB400Itau(Stream arquivo)
        {
            try
            {
                StreamWriter gravaLinha = new StreamWriter(arquivo);

                #region Variáveis

                string _header;
                string _detalhe1;
                string _detalhe2;
                string _detalhe3;
                string _trailer;

                string n275 = new string(' ', 275);
                string n025 = new string(' ', 25);
                string n023 = new string(' ', 23);
                string n039 = new string('0', 39);
                string n026 = new string('0', 26);
                string n090 = new string(' ', 90);
                string n160 = new string(' ', 160);

                #endregion

                #region HEADER

                _header = "02RETORNO01COBRANCA       347700232610        ALLMATECH TECNOLOGIA DA INFORM341BANCO ITAU SA  ";
                _header += "08010800000BPI00000201207";
                _header += n275;
                _header += "000001";

                gravaLinha.WriteLine(_header);

                #endregion

                #region DETALHE

                _detalhe1 = "10201645738000250097700152310        " + n025 + "00000001            112000000000             ";
                _detalhe1 += "I06201207000000000100000000            261207000000002000034134770010000000000500" + n025 + " ";
                _detalhe1 += n039 + "0000000020000" + n026 + "   2112070000      0000000000000POLITEC LTDA                  " + n023 + "               ";
                _detalhe1 += "AA000002";

                gravaLinha.WriteLine(_detalhe1);

                _detalhe2 = "10201645738000250097700152310        " + n025 + "00000002            112000000000             ";
                _detalhe2 += "I06201207000000000100000000            261207000000002000034134770010000000000500" + n025 + " ";
                _detalhe2 += n039 + "0000000020000" + n026 + "   2112070000      0000000000000POLITEC LTDA                  " + n023 + "               ";
                _detalhe2 += "AA000003";

                gravaLinha.WriteLine(_detalhe2);

                _detalhe3 = "10201645738000250097700152310        " + n025 + "00000003            112000000000             ";
                _detalhe3 += "I06201207000000000100000000            261207000000002000034134770010000000000500" + n025 + " ";
                _detalhe3 += n039 + "0000000020000" + n026 + "   2112070000      0000000000000POLITEC LTDA                  " + n023 + "               ";
                _detalhe3 += "AA000004";

                gravaLinha.WriteLine(_detalhe3);

                #endregion

                #region TRAILER

                _trailer = "9201341          0000000300000000060000                  0000000000000000000000        ";
                _trailer += n090 + "0000000000000000000000        000010000000300000000060000" + n160 + "000005";
                ;

                gravaLinha.WriteLine(_trailer);

                #endregion

                gravaLinha.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar arquivo.", ex);
            }
        }
    #endregion Exemplos de arquivos de retorno
        

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            if (radioButtonItau.Checked)
                LerRetorno(341);
            else if (radioButtonSudameris.Checked)
                LerRetorno(347);
            else if (radioButtonSantander.Checked)
                LerRetorno(33);
            else if (radioButtonReal.Checked)
                LerRetorno(356);
            else if (radioButtonCaixa.Checked)
                LerRetorno(104);
            else if (radioButtonBradesco.Checked)
                LerRetorno(237);
            else if (radioButtonSicredi.Checked)
                LerRetorno(748);
            else if (radioButtonBanrisul.Checked)
                LerRetorno(041);
            else if (radioButtonBNB.Checked)
                LerRetorno(4);
            else if (radioButtonBancoBrasil.Checked)
                LerRetorno(1);
            else if (radioButtonSicoob.Checked)
                LerRetorno(756);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string caminhoArquivo = "C:\\Bases de Clientes\\Ms Vidros\\ArquivoRetorno.RET";

            var arquivo = System.IO.File.Create(caminhoArquivo);

            GeraArquivoCNAB400Itau(arquivo);
        }

        private void btnConciliar_Click(object sender, EventArgs e)
        {
            var listaConciliacao = retorneListaConciliacaoDesconciliacao();
            string MensagemResposta = "Conciliação realizada com sucesso!";

            try
            {
                foreach (var item in listaConciliacao)
                {
                    ServicoContasReceber servicoContasPagarReceber = new ServicoContasReceber();
                        
                    var listaContaParaBaixar = servicoContasPagarReceber.ConsulteLista(null,
                                                Programax.Easy.Negocio.Financeiro.Enumeradores.EnumTipoOperacaoContasPagarReceber.RECEBER,
                                                null,
                                                new FormaPagamento
                                                {
                                                    Id = 2,
                                                    TipoFormaPagamento = Programax.Easy.Negocio.Financeiro.Enumeradores.EnumTipoFormaPagamento.BOLETOBANCARIO
                                                }
                                                , null,
                                                Programax.Easy.Negocio.Financeiro.Enumeradores.EnumDataFiltrarContasPagarReceber.VENCIMENTO,
                                                item.DataDeVencimento, item.DataDeVencimento);

                    if (listaContaParaBaixar != null)
                    {
                        foreach (var itemBaixar in listaContaParaBaixar)
                        {
                            //****Numero do documento origem CONTAS A RECEBER ****INICIO
                            var NumeroDocumentoComparar = itemBaixar.NumeroDocumento.Split('-');

                            string NumeroDoc = NumeroDocumentoComparar[0].Trim();

                            //****FIM

                            //****Numero do documento origem ARQUIVO DE RETORNO DO BANCO ****INICIO
                            string NumeroDocumentoDeRetorno = item.NumeroDoDocumento;

                            if (item.NumeroDoDocumento.Contains("-")) 
                            {
                                var DocRetSplit = item.NumeroDoDocumento.Split('-');

                                NumeroDocumentoDeRetorno = DocRetSplit[0].Trim();                                
                            }
                            //****FIM

                            if ((NumeroDoc == NumeroDocumentoDeRetorno) || item.NumeroDoDocumento.Trim() == itemBaixar.NumeroDocumento.Trim())
                            {
                                if(item.Status != EnumStatusContaPagarReceber.CONCILIADOQUITADO 
                                            && item.Status != EnumStatusContaPagarReceber.QUITADO 
                                            && item.Status != EnumStatusContaPagarReceber.INATIVO
                                            && item.Status != EnumStatusContaPagarReceber.CANCELADO)
                                {
                                    itemBaixar.Status = Programax.Easy.Negocio.Financeiro.Enumeradores.EnumStatusContaPagarReceber.CONCILIADOQUITADO;
                                    itemBaixar.MultaEhPercentual = false;
                                    itemBaixar.Multa = item.Multa;
                                    itemBaixar.JurosEhPercentual = false;

                                    itemBaixar.Juros = item.Juros;
                                    itemBaixar.Desconto = item.Desconto;

                                    itemBaixar.EhConciliacao = true;
                                    itemBaixar.ValorPago = item.ValorPago;
                                    itemBaixar.DataPagamento = item.DataDePagamento;

                                    CalculoValorTotalContaPagarReceber.CalculeValorTotalContaPagarReceber(itemBaixar);

                                    servicoContasPagarReceber.Atualize(itemBaixar);

                                    itemBaixar.ListaContasPagarReceberParcial = retorneListaHistoricoDePagamentos(item.DataDePagamento, itemBaixar.ValorPago,
                                                                               itemBaixar.FormaPagamento, itemBaixar, false);

                                    new ServicoContasPagarReceberPagamento().CadastreLista(itemBaixar.ListaContasPagarReceberParcial.ToList());

                                    //Movimentação Bancária******
                                    var bancoMov = new ServicoBancoParaMovimento().ConsulteLista(string.Empty, "A");

                                    var banco = bancoMov.Find(x => x.TornarPadrao == true);

                                    new ServicoItemMovimentacaoBanco().InsiraMovimentacaoBancaria(itemBaixar, false,
                                                                EnumTipoOperacaoContasPagarReceber.RECEBER, itemBaixar.DataPagamento.Value,
                                                                new CategoriaFinanceira { Id = 2 },
                                                                banco,
                                                               itemBaixar.ValorPago, new Pessoa { Id = Sessao.PessoaLogada.Id });                                    
                                    //*************

                                    _listaConciliacaoGrid.FirstOrDefault(x => x.NumeroDocumento == item.NumeroDoDocumento).Status = EnumStatusContaPagarReceber.CONCILIADOQUITADO.Descricao();
                                    _listaConciliacaoGrid.FirstOrDefault(x => x.NumeroDocumento == item.NumeroDoDocumento).Imagem = Properties.Resources.icone_verde;

                                    MensagemResposta = "Conciliação realizada com sucesso!";
                                }
                                else
                                {
                                    MensagemResposta = "Houve(ram) registro(s) não conciliado(s). Possivelmente, porque foram Quitados, Inativados, Cancelados direto do Contas a Receber " +
                                                        "ou não foi(ram) encontrado(s).";
                                }
                            }
                        }
                    }
                }

                MessageBox.Show(MensagemResposta, "Conciliar Boletos", MessageBoxButtons.OK);
                preencheGrid();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnDesfazerConciliacao_Click(object sender, EventArgs e)
        {
            if (gridViewConciliacaoBoletos.SelectedRowsCount == 0)
            {
                MessageBox.Show("Para desfazer a conciliação você deve selecionar pelos menos uma conta. <Para selecionar: <Tecla control + Clique do mouse>>", "Desfazendo a Conciliação", MessageBoxButtons.OK);
                return;
            }

            var listaDesconciliacao = retorneListaConciliacaoDesconciliacao();
            string MensagemResposta = "A Conciliação foi <<desfeita>> com sucesso!";

            try
            {
                foreach (var item in listaDesconciliacao)
                {
                    ServicoContasReceber servicoContasPagarReceber = new ServicoContasReceber();

                    var listaContaParaBaixar = servicoContasPagarReceber.ConsulteLista(null,
                                                Programax.Easy.Negocio.Financeiro.Enumeradores.EnumTipoOperacaoContasPagarReceber.RECEBER,
                                                null,
                                                new FormaPagamento
                                                {
                                                    Id = 2,
                                                    TipoFormaPagamento = Programax.Easy.Negocio.Financeiro.Enumeradores.EnumTipoFormaPagamento.BOLETOBANCARIO
                                                }
                                                , null,
                                                Programax.Easy.Negocio.Financeiro.Enumeradores.EnumDataFiltrarContasPagarReceber.VENCIMENTO,
                                                item.DataDeVencimento, item.DataDeVencimento);

                    if (listaContaParaBaixar != null)
                    {
                        foreach (var itemBaixar in listaContaParaBaixar)
                        {
                            //***Documento do Contas a Receber
                            var NumeroDocumentoComparar = itemBaixar.NumeroDocumento.Split('-');

                            string NumeroDoc = NumeroDocumentoComparar[0].Trim();
                            //*

                            //***Documento do Arquivo Retorno
                            var NumeroDocumentoRetornoSplit = item.NumeroDoDocumento.Split('-');

                            string NumeroDocumentoRetorno = NumeroDocumentoRetornoSplit[0].Trim();
                            //*
                            
                            if (NumeroDoc == NumeroDocumentoRetorno && item.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO)
                            {
                                itemBaixar.Juros = 0;
                                itemBaixar.Multa = 0;
                                itemBaixar.Desconto = 0;

                                EstorneConciliacao(itemBaixar);

                                //Se tiver movimentação no banco, vamos excluir
                                new ServicoItemMovimentacaoBanco().ExcluaParcialOrigemPagarReceber(itemBaixar);

                                _listaConciliacaoGrid.FirstOrDefault(x => x.NumeroDocumento == item.NumeroDoDocumento).Status = EnumStatusContaPagarReceber.NAOCONCILIADO.Descricao();
                                _listaConciliacaoGrid.FirstOrDefault(x => x.NumeroDocumento == item.NumeroDoDocumento).Imagem = Properties.Resources.icone_vermelho;

                                MensagemResposta = "A Conciliação foi <<desfeita>> com sucesso!";
                            }
                            else
                            {
                                MensagemResposta = "Houve(ram) registro(s) não estornado(s). Possivelmente, porque foram Quitados direto do Contas a Receber " +
                                                    "ou não foi(ram) encontrado(s).";
                            }                            
                        }
                    }
                }

                MessageBox.Show(MensagemResposta, "<Desfazendo> Conciliação", MessageBoxButtons.OK);
                preencheGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    #region "Classes Auxiliares"

    public class DadosConciliacao
        {
            public double ValorPago { get; set; }
            public DateTime DataDePagamento { get; set; }
            public DateTime DataDeVencimento { get; set; }
            public string NumeroDoDocumento { get; set; }
            public EnumStatusContaPagarReceber Status { get; set; }
            public double Multa { get; set; }
            public double Juros { get; set; }
            public double Desconto { get; set; }
    }

        public class ConciliacaoBoletosGrid
        {
            public int Id { get; set; }

            public string Sacado { get; set; }

            public string NumeroDocumento { get; set; }

            public string NossoNumero { get; set; }

            public double ValorTitulo { get; set; }

            public double Multa { get; set; }

            public double Juros { get; set; }

            public double Desconto { get; set; }            

            public double ValorPago { get; set; }           

            public string DataPagamento { get; set; }
                   
            public string DataVencimento { get; set; }

            public string  Status { get; set; }
        
            public Image Imagem { get; set; } 
    }

    #endregion
}