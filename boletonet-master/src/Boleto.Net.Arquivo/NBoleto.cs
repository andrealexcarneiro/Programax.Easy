using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using BoletoNet;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.Financeiro.ContaBancariaServ;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.AgenciaServ;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.PerfilEmissaoBoletoSev;
using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ConfiguracaoBoletoSev;
using Programax.Easy.Servico.Cadastros.EnderecoServ;

namespace BoletoNet.Arquivo
{
    public partial class NBoleto : Form
    {
        #region Variaveis Globais

        private short _codigoBanco = 0;
        //private Progresso _progresso;
        string _arquivo = string.Empty;
        private ImpressaoBoleto _impressaoBoleto = new ImpressaoBoleto();
        private Empresa _empresaCedente = new Empresa();
        private ServicoEmpresa _servicoEmpresa = new ServicoEmpresa();
        private string _contaBancaria;
        private string _digitoContaBancaria;
        private string _agenciaBancaria;
        private string _digitoAgenciaBancaria;
        public string _localDePagamento;
        private List<InstrucoesBoleto> _listaInstrucao = new List<InstrucoesBoleto>();
        private int _intLinhaGrid;
        private string _carteira;
        public string _variacao;
        private string _codigoCedente;
        private int _digitoCedente;
        private string _especie;
        private string _NossoNumero;
        private string _modalidade;
        private int _padraoRemessa;        
        private string _tipoDocumentoRemessa;
        private string _convenioRemessa;
        private int _proximoLote;
        private string _caminhoArquivoRemessa;
        private List<PerfilEmissaoBoleto> _listaDePerfis = new List<PerfilEmissaoBoleto>();
                
        public short CodigoBanco
        {
            get { return _codigoBanco; }
            set { _codigoBanco = value; }
        }

        public int _QuantidadeBoletos { get; set; }
        public List<ContaPagarReceber> _listaClientesEmissao = new List<ContaPagarReceber>();

# endregion

        public NBoleto()
        {
            InitializeComponent();
            
            _impressaoBoleto.FormClosed += new FormClosedEventHandler(_impressaoBoleto_FormClosed);

            _empresaCedente = _servicoEmpresa.ConsulteUltimaEmpresa();

            PreencheCboPerfisEmissaoBoletos();

            ObterBanco();
            //PreenchaCboAgencias();
            //carregaInstrucoesBancos();
            //carregaEspecieDocumento();
            //carregaTipoValor();

            //Carrega Perfil Padrão
            if (_listaDePerfis.Count != 0 && _listaDePerfis.Exists(x => x.EhPerfilPadrao == true))
                cboPerfisEmissaoBoleto.EditValue = _listaDePerfis.Find(x => x.EhPerfilPadrao == true).Id;
        }

        void _impressaoBoleto_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
            Dispose();            
        }

        #region Métodos Auxiliares

        private static double RetorneValorJuros(ContaPagarReceber contaPagarReceber, DateTime dataCalcularValor, bool ehCalculoManual)
        {
            double valorParcela = contaPagarReceber.ListaHistoricoAlteracoesVencimento.Count != 0 ? contaPagarReceber.ListaHistoricoAlteracoesVencimento.FirstOrDefault().Valor : 0; ;
            var DataVencimento = contaPagarReceber.ListaHistoricoAlteracoesVencimento.Count != 0 ? contaPagarReceber.ListaHistoricoAlteracoesVencimento.FirstOrDefault().DataVencimento : DateTime.Parse("01 / 01 / 0001 00:00:00");

            if (dataCalcularValor > DataVencimento || ehCalculoManual)
            {
                var TotalDiasVencido = (dataCalcularValor - DataVencimento);

                int totalDiasVencido = (int)TotalDiasVencido.TotalDays;

                int contaDias = 1;

                double valorJuros = 0;

                while (totalDiasVencido >= contaDias)
                {
                    if (contaPagarReceber.JurosEhPercentual)
                    {
                        valorJuros = valorJuros + valorParcela * (contaPagarReceber.Juros / 30) / (double)100;

                        valorParcela = valorParcela + valorJuros;
                    }
                    else
                    {
                        valorJuros = contaPagarReceber.Juros;

                        double valorJurosPorDia = valorJuros / 30;

                        return valorJurosPorDia * totalDiasVencido;
                    }

                    contaDias++;
                }

                return Math.Round(valorJuros,2);
            }

            return 0;
        }

        private static double RetorneValorMulta(ContaPagarReceber contaPagarReceber, DateTime dataCalcularValor, bool ehCalculoManual)
        {
            double valorParcela = contaPagarReceber.ListaHistoricoAlteracoesVencimento.Count != 0 ? contaPagarReceber.ListaHistoricoAlteracoesVencimento.FirstOrDefault().Valor : 0; ;
            var dataVencimento = contaPagarReceber.ListaHistoricoAlteracoesVencimento.Count != 0 ? contaPagarReceber.ListaHistoricoAlteracoesVencimento.FirstOrDefault().DataVencimento : DateTime.Parse("01 / 01 / 0001 00:00:00");

            if (dataCalcularValor > dataVencimento || ehCalculoManual)
            {
                if (contaPagarReceber.MultaEhPercentual)
                {
                    return Math.Round(valorParcela * contaPagarReceber.Multa / (double)100);
                }
                else
                {
                    return Math.Round(contaPagarReceber.Multa,2);
                }
            }

            return 0;
        }

        public static List<ObjetoDescricaoValor> RetorneListaDeValoresEnumeradorInstrucoes<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            List<ObjetoDescricaoValor> listaDeEnumeradores = new List<ObjetoDescricaoValor>();

            Array enumValores = Enum.GetValues(typeof(T));

            foreach (Enum valor in enumValores)
            {
                listaDeEnumeradores.Add(new ObjetoDescricaoValor
                {
                    Descricao = valor.Descricao(),
                    Valor = valor.GetHashCode()
                });
            }

            return listaDeEnumeradores;
        }

        private void carregaInstrucoesBancos()
        {
            List<ObjetoDescricaoValor> lista = new List<ObjetoDescricaoValor>();

            if (radioButtonItau.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_Itau>();
            else if (radioButtonUnibanco.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_Itau>();
            else if (radioButtonSudameris.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_Sudameris>();
            else if (radioButtonSafra.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_Safra>();
            else if (radioButtonReal.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_Real>();
            else if (radioButtonHsbc.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_HSBC>();
            else if (radioButtonBancoBrasil.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_BancoBrasil>();
            else if (radioButtonBradesco.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_Bradesco>();
            else if (radioButtonCaixa.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_Caixa>();
            else if (radioButtonBNB.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_BancoNordeste>();
            else if (radioButtonSantander.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_Santander>();
            else if (radioButtonSicredi.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_Sicredi>();
            else if (radioButtonSicoob.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumInstrucoes_Sicoob>();

            cboInstrucao.Properties.DataSource = lista;
            cboInstrucao.Properties.DisplayMember = "Descricao";
            cboInstrucao.Properties.ValueMember = "Valor";
        }

        private void carregaEspecieDocumento()
        {
            List<ObjetoDescricaoValor> lista = new List<ObjetoDescricaoValor>();

            if (radioButtonItau.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_Itau>();
            else if (radioButtonUnibanco.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_Itau>();
            else if (radioButtonSudameris.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_Sudameris>();
            else if (radioButtonReal.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_Real>();
            else if (radioButtonHsbc.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_HSBC>();
            else if (radioButtonBancoBrasil.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_BancoBrasil>();
            else if (radioButtonBradesco.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_Bradesco>();
            else if (radioButtonCaixa.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_Caixa>();
            else if (radioButtonBNB.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_Nordeste>();
            else if (radioButtonSantander.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_Santander>();
            else if (radioButtonSicredi.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_Sicredi>();
            else if (radioButtonSicoob.Checked)
                lista = RetorneListaDeValoresEnumeradorInstrucoes<EnumEspecieDocumento_Sicoob>();
            
            cboEspecie.Properties.DataSource = lista;
            cboEspecie.Properties.DisplayMember = "Descricao";
            cboEspecie.Properties.ValueMember = "Valor";
        }

        private void carregaTipoValor()
        {            
            List<ObjetoDescricaoValor> listatipoValor = new List <ObjetoDescricaoValor>();

            ObjetoDescricaoValor tipoValor = new ObjetoDescricaoValor();

            tipoValor.Descricao = "Reais";
            tipoValor.Valor = 0;

            listatipoValor.Add(tipoValor);

            tipoValor = new ObjetoDescricaoValor();

            tipoValor.Descricao = "Percentual";
            tipoValor.Valor = 1;

            listatipoValor.Add(tipoValor);

            cboTipoValor.Properties.DataSource = listatipoValor;
            cboTipoValor.Properties.DisplayMember = "Descricao";
            cboTipoValor.Properties.ValueMember = "Valor";
        }

        private void PreenchaGrid(bool selecao = true)
        {
            List<ItemInstrucaoGrid> listaInstrucoes = new List<ItemInstrucaoGrid>();

            for (int i = 0; i < _listaInstrucao.Count; i++)
            {
                var item = _listaInstrucao[i];

                ItemInstrucaoGrid itemInstrucaoGrid = new ItemInstrucaoGrid();

                itemInstrucaoGrid.Item = (i + 1).ToString();
                itemInstrucaoGrid.Codigo = item.CodigoInstrucao.ToString();
                itemInstrucaoGrid.Descricao = item.DescricaoInstrucao;
                itemInstrucaoGrid.Dias = item.Dias.ToString();
                itemInstrucaoGrid.valor = item.Valor.ToString();
                itemInstrucaoGrid.tipoValor = item.TipoValor.ToString();

                listaInstrucoes.Add(itemInstrucaoGrid);
                _intLinhaGrid = i;
            }

            gridinstrucoes.DataSource = listaInstrucoes;
            if (_listaInstrucao.Count > 0)
            {
                gridinstrucoes.FirstDisplayedScrollingRowIndex = _listaInstrucao.Count - 1;
                gridinstrucoes[0, 0].Selected = false;

                if (selecao) 
                    gridinstrucoes[0, _intLinhaGrid].Selected = true;
            }
        }

        private void InsiraInstrucao()
        {

            if (cboInstrucao.Text == string.Empty && cboInstrucao.Enabled)
            {
                MessageBox.Show("Para adicionar um item, por favor, informe uma instrução.", "Para Continuar",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboInstrucao.Focus();
                return;
            }

            InstrucoesBoleto itemInstrucao = new InstrucoesBoleto();
            
            itemInstrucao.DescricaoInstrucao = cboInstrucao.Text;

            itemInstrucao.CodigoInstrucao = cboInstrucao.EditValue.ToInt();

            itemInstrucao.Dias = numDias.Value.ToInt();

            itemInstrucao.Valor = txtValor.Text.ToDouble();

            itemInstrucao.TipoValor = cboTipoValor.Text.ToInt();

            _listaInstrucao.Add(itemInstrucao);

            PreenchaGrid();

            limpeInstrucoesParaGrid();
        }

        private void limpeInstrucoesParaGrid()
        {
            cboInstrucao.EditValue = string.Empty;
            numDias.Value = 1;
            txtValor.Text = string.Empty;
            cboTipoValor.EditValue = string.Empty;
        }

        private void ObterBanco()
        {
            if (radioButtonItau.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonItau.Tag);
            else if (radioButtonUnibanco.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonUnibanco.Tag);
            else if (radioButtonSudameris.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonSudameris.Tag);
            else if (radioButtonSafra.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonSafra.Tag);
            else if (radioButtonReal.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonReal.Tag);
            else if (radioButtonHsbc.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonHsbc.Tag);
            else if (radioButtonBancoBrasil.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonBancoBrasil.Tag);
            else if (radioButtonBradesco.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonBradesco.Tag);
            else if (radioButtonCaixa.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonCaixa.Tag);
            else if (radioButtonBNB.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonBNB.Tag);
            else if (radioButtonSantander.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonSantander.Tag);
            else if (radioButtonSicredi.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonSicredi.Tag);
            else if (radioButtonSicoob.Checked)
                CodigoBanco = Convert.ToInt16(radioButtonSicoob.Tag);
        }

        private void CarregaDadosParaEmissaoBoleto()
        {   
            numericUpDown.Value = _QuantidadeBoletos;
            ObterBanco();

            var VetorConta = cboConta.Text.Split('-');
            var VetorAgencia = cboAgencias.Text.Split('-');

            _contaBancaria = VetorConta[0].Trim();
            _digitoContaBancaria = VetorConta[1].Trim();

            _agenciaBancaria = VetorAgencia[0].Trim();
            _digitoAgenciaBancaria = VetorAgencia[1].Trim();
            _localDePagamento = txtLocalPagamento.Text;

            _carteira = txtCarteira.Text;            
            _codigoCedente = txtCodigoCedente.Text;
            _digitoCedente = txtDigitoCedente.Text.ToInt();
            _especie = cboEspecie.EditValue.ToString();
            _modalidade = txtModalidade.Text;
        }

        private void CarregaDadosParaEmissaoBoletoDoPerfil()
        {
            ServicoConfiguracaoBoleto servicoConfiguracao = new ServicoConfiguracaoBoleto();

            var configuracao = servicoConfiguracao.ConsultePeloPerfil(cboPerfisEmissaoBoleto.EditValue.ToInt());
            
            //Banco
            ServicoBanco servicoBanco = new ServicoBanco();
            var banco = servicoBanco.Consulte(configuracao.Banco.Id);
            CodigoBanco = short.Parse(banco.Codigo);

            //Conta
            ServicoContaBancaria servicoConta = new ServicoContaBancaria();
            var contaBancara = servicoConta.Consulte(configuracao.ContaBancaria.Id);
            var VetorConta = contaBancara.NumeroConta.Split('-');
            _contaBancaria = VetorConta[0].Trim();
            _digitoContaBancaria = VetorConta[1].Trim();

            //Agência
            ServicoAgencia servicoAgencia = new ServicoAgencia();
            var agencia = servicoAgencia.Consulte(configuracao.Agencia.Id);            
            _agenciaBancaria = agencia.NumeroAgencia;
            _digitoAgenciaBancaria = agencia.DigitoAgencia;

            //Local de Pagamento
            _localDePagamento = configuracao.LocalPagamento;

            _carteira = configuracao.Carteira;
            _variacao = configuracao.Variacao;
            _codigoCedente = configuracao.CodigoCedente;
            _digitoCedente = configuracao.DigitoCedente.ToInt();
            _especie = configuracao.EspecieDocumento.ToString();
            _modalidade = configuracao.Modalidade.ToString();
            _NossoNumero = configuracao.NossoNumero;
            _padraoRemessa = configuracao.Padrao;
            _tipoDocumentoRemessa = configuracao.TipoDocumentoRemessa;
            _convenioRemessa = configuracao.ConvenioRemessa;
            _caminhoArquivoRemessa = configuracao.CaminhoRemessa;
            _proximoLote = configuracao.ProximoLote == 0? 1: configuracao.ProximoLote;
           
            var configuracao2 = servicoConfiguracao.Consulte(configuracao.Id);

            _listaInstrucao = configuracao2.ListaInstrucoes.ToList();
        }

        private void PreencheCboPerfisEmissaoBoletos()
        {
            ServicoPerfilEmissaoBoleto servicoPerfil = new ServicoPerfilEmissaoBoleto();

            var listaPerfis = servicoPerfil.ConsulteLista();

            _listaDePerfis = listaPerfis;
            
            cboPerfisEmissaoBoleto.Properties.DataSource = listaPerfis;
            cboPerfisEmissaoBoleto.Properties.DisplayMember = "Descricao";
            cboPerfisEmissaoBoleto.Properties.ValueMember = "Id";

            if (cboPerfisEmissaoBoleto.EditValue != null)
            {
                if (!listaPerfis.Exists(perfil => perfil != null && perfil.Id == cboPerfisEmissaoBoleto.EditValue.ToInt()))
                {
                    cboPerfisEmissaoBoleto.EditValue = null;
                }
            }
        }

        private void PreenchaCboAgencias()
        {   
                ServicoAgencia servicoAgencia = new ServicoAgencia();

                ServicoBanco servicoBanco = new ServicoBanco();

                var bancoSelecionado = servicoBanco.ConsultePeloCodigoBanco(CodigoBanco.ToString());

                if (bancoSelecionado == null) return;

                var listaAgencias = servicoAgencia.ConsulteLista(new Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio.Banco { Id = bancoSelecionado.Id }, string.Empty, "A");

                List<AgenciaAuxiliarComboBox> listaAgenciasAuxiliares = new List<AgenciaAuxiliarComboBox>();

                listaAgenciasAuxiliares.Add(null);

                foreach (var agencia in listaAgencias)
                {
                    AgenciaAuxiliarComboBox contaBancariaAuxiliar = new AgenciaAuxiliarComboBox();

                    contaBancariaAuxiliar.Id = agencia.Id;

                    contaBancariaAuxiliar.Descricao = agencia.NumeroAgencia + " - " + agencia.DigitoAgencia + " - " + agencia.NomeAgencia;

                    listaAgenciasAuxiliares.Add(contaBancariaAuxiliar);
                }

                cboAgencias.Properties.DataSource = listaAgenciasAuxiliares;
                cboAgencias.Properties.DisplayMember = "Descricao";
                cboAgencias.Properties.ValueMember = "Id";
        }

        private void PreenchaCboContas()
        {
            ServicoContaBancaria servicoContaBancaria = new ServicoContaBancaria();

            ServicoBanco servicoBanco = new ServicoBanco();

            var bancoSelecionado = servicoBanco.ConsultePeloCodigoBanco(CodigoBanco.ToString());

            var listaContas = servicoContaBancaria.ConsulteLista(new Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio.Banco { Id = bancoSelecionado.Id}, new Agencia { Id = cboAgencias.EditValue.ToInt() }, string.Empty, "A", null);

            List<AgenciaAuxiliarComboBox> listaAgenciasAuxiliares = new List<AgenciaAuxiliarComboBox>();

            listaAgenciasAuxiliares.Add(null);

            foreach (var Conta in listaContas)
            {
                AgenciaAuxiliarComboBox contaBancariaAuxiliar = new AgenciaAuxiliarComboBox();

                contaBancariaAuxiliar.Id = Conta.Id;

                contaBancariaAuxiliar.Descricao = Conta.NumeroConta;

                listaAgenciasAuxiliares.Add(contaBancariaAuxiliar);
            }

            cboConta.Properties.DataSource = listaAgenciasAuxiliares;
            cboConta.Properties.DisplayMember = "Descricao";
            cboConta.Properties.ValueMember = "Id";
        }

        private void HabilitarDesabilitarControlesInstrucoes(bool habilitarCboInstrucao=true, bool habilitaNumDias = true, bool habilitaTxtValor =true, bool habilitarCboTipoValor=true)
        {   
            cboInstrucao.Enabled = habilitarCboInstrucao;
            numDias.Enabled = habilitaNumDias;
            txtValor.Enabled = habilitaTxtValor;
            cboTipoValor.Enabled = habilitarCboTipoValor;            
        }

        #endregion

        #region GERA LAYOUT DO BOLETO

        private void GeraLayout(List<BoletoBancario> boletos)
        {
            StringBuilder html = new StringBuilder();
            foreach (BoletoBancario o in boletos)
            {
                html.Append(o.MontaHtml());
                html.Append("</br></br></br></br></br></br></br></br></br></br>");
            }

            _arquivo = System.IO.Path.GetTempFileName();

            using (FileStream f = new FileStream(_arquivo, FileMode.Create))
            {
                StreamWriter w = new StreamWriter(f, System.Text.Encoding.UTF8);
                w.Write(html.ToString());
                w.Close();
                f.Close();
            }
        }
        #endregion

        #region BOLETO Caixa

        private void GeraBoletoCaixa(int qtde)
        {
            // Cria o boleto, e passa os parâmetros usuais
            BoletoBancario bb;
            bb = new BoletoBancario();

            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {                
                bb.CodigoBanco = _codigoBanco;
                bb.MostrarEnderecoCedente = true;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();
                DateTime vencimento = dataVencimento.ToDate();
                
                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _contaBancaria);
                
                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                string nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();

                Boleto b = new Boleto(vencimento, (decimal) _listaClientesEmissao[i].ValorParcela, _carteira, nossoNumero, c);

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;

                Endereco endCed = new Endereco();

                endCed.End = _empresaCedente.DadosEmpresa.Endereco.Rua;
                endCed.Bairro = _empresaCedente.DadosEmpresa.Endereco.Bairro;
                endCed.Cidade = _empresaCedente.DadosEmpresa.Endereco.Cidade.Descricao;
                endCed.CEP = _empresaCedente.DadosEmpresa.Endereco.CEP;
                endCed.UF = _empresaCedente.DadosEmpresa.Endereco.Cidade.Estado.UF;
                b.Cedente.Endereco = endCed;

                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua:null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                for (int x = 0; x < _listaInstrucao.Count; x++)
                {
                    Instrucao_Caixa item = new Instrucao_Caixa();

                    if (_listaInstrucao[x].TipoValor != null)
                    {
                        AbstractInstrucao.EnumTipoValor tipoValor = _listaInstrucao[x].TipoValor == 0 ? AbstractInstrucao.EnumTipoValor.Reais : AbstractInstrucao.EnumTipoValor.Percentual;

                        item = new Instrucao_Caixa(_listaInstrucao[x].CodigoInstrucao.ToInt(), (decimal)_listaInstrucao[x].Valor, tipoValor);
                    }
                    else
                        item = new Instrucao_Caixa(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Dias.ToInt());

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 81)
                    {
                        item.Descricao += " " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";
                    }
                    // juros/descontos

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 999)
                    {  
                        item.Descricao += " " + _listaInstrucao[x].Valor + (" por dia de antecipação.");
                    }

                    b.Instrucoes.Add(item);
                }

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                b.Banco = new Banco(_codigoBanco);
                
                //Remessa
                b.Remessa = new Remessa();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa; //"2"; // SIGCB - SEM REGISTRO
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa

                bb.Boleto = b;

                bb.Boleto.Valida();

                boletos.Add(bb);
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                _caminhoArquivoRemessa += "\\ArquivoRemessa_Caixa_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".rem";

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa, _proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }
        #endregion

        #region BOLETO ITAÚ
        private void GeraBoletoItau(int qtde)
        {
            // Cria o boleto, e passa os parâmetros usuais
            BoletoBancario bb;
            bb = new BoletoBancario();

            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {   
                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _contaBancaria);

                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                string nossoNumero = "";
                if (_codigoBanco == 341)
                {
                    nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString("00000000");
                }
                else
                {
                     nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();
                }
           

                //Na carteira 198 o código do Cedente é a conta bancária
                c.Codigo = _codigoCedente != "" & _codigoCedente != null? _codigoCedente : _contaBancaria;
               
                Boleto b = new Boleto(vencimento, (decimal)_listaClientesEmissao[i].ValorParcela, _carteira, nossoNumero, c, new EspecieDocumento(_codigoBanco, _especie));

                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento;

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                for (int x = 0; x < _listaInstrucao.Count; x++)
                {
                    Instrucao_Itau item = new Instrucao_Itau();

                    if (_listaInstrucao[x].TipoValor != null)
                    {
                        AbstractInstrucao.EnumTipoValor tipoValor = _listaInstrucao[x].TipoValor == 0 ? AbstractInstrucao.EnumTipoValor.Reais : AbstractInstrucao.EnumTipoValor.Percentual;

                        item = new Instrucao_Itau(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Valor.ToDouble(), tipoValor);
                    }
                    else
                        item = new Instrucao_Itau(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Dias.ToInt());

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 81)
                    {
                        item.Descricao += " " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";

                    }
                    // juros/descontos

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 999)
                    {
                        item.Descricao += " " + _listaInstrucao[x].Valor + (" por dia de antecipação.");
                    }

                    b.Instrucoes.Add(item);
                }

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                b.Banco = new Banco(_codigoBanco);

                //Remessa
                b.Remessa = new Remessa();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa; //"2"; // SIGCB - SEM REGISTRO
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa

                bb.Boleto = b;
                bb.Boleto.Valida();

                boletos.Add(bb);
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                _caminhoArquivoRemessa += "\\ArquivoRemessa_Itau_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".rem";

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa, _proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }

        #endregion


        #region BOLETO UNIBANCO
        public void GeraBoletoUnibanco(int qtde)
        {
            // Cria o boleto, e passa os parâmetros usuais
            BoletoBancario bb;
            bb = new BoletoBancario();

            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {   
                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                Instrucao instr = new Instrucao(001);

                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _contaBancaria, _digitoContaBancaria);
                c.Codigo = _codigoCedente != "" & _codigoCedente != null ? _codigoCedente : _contaBancaria.ToString();

                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                string nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();

                Boleto b = new Boleto(vencimento, (decimal)_listaClientesEmissao[i].ValorPago, _carteira, nossoNumero, c);

                b.NumeroDocumento = b.NossoNumero;

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;
                b.Sacado.Endereco.Numero = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Numero : null;
                b.Sacado.Endereco.Complemento = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Complemento : null;

                instr.Descricao = "Não Receber após o vencimento";
                b.Instrucoes.Add(instr);

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                //Remessa
                b.Remessa = new Remessa();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa; //"2"; // SIGCB - SEM REGISTRO
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa

                bb.Boleto = b;
                bb.Boleto.Valida();

                boletos.Add(bb);
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                _caminhoArquivoRemessa += "\\ArquivoRemessa_Unibanco_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".rem";

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa, _proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }
        #endregion


        #region BOLETO SUDAMERIS
        public void GeraBoletoSudameris(int qtde)
        {
            // Cria o boleto, e passa os parâmetros usuais
            BoletoBancario bb;
            bb = new BoletoBancario();

            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {

                bb = new BoletoBancario();
                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                Instrucao instr = new Instrucao(001);

                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                string nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();

                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _contaBancaria);
                c.Codigo = _codigoCedente != "" & _codigoCedente != null ? _codigoCedente : _contaBancaria.ToString();

                //Nosso número com 7 dígitos
                //string nn = "0003020";
                //Nosso número com 13 dígitos
                //nn = "0000000003025";

                Boleto b = new Boleto(vencimento, (decimal) _listaClientesEmissao[i].ValorParcela, _carteira, nossoNumero, c);// EnumEspecieDocumento_Sudameris.DuplicataMercantil);
                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento;

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                instr.Descricao = "Não Receber após o vencimento";
                b.Instrucoes.Add(instr);

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                //Remessa
                b.Remessa = new Remessa();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa; //"2"; // SIGCB - SEM REGISTRO
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa

                bb.Boleto = b;
                bb.Boleto.Valida();

                boletos.Add(bb);
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                _caminhoArquivoRemessa += "\\ArquivoRemessa_Sudameris_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".rem";

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa,_proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }
        #endregion
        
        #region BOLETO SAFRA
        public void GeraBoletoSafra(int qtde)
        {
            // Cria o boleto, e passa os parâmetros usuais
            BoletoBancario bb;
            bb = new BoletoBancario();

            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();
            
            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {
                bb = new BoletoBancario();
                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                Instrucao instr = new Instrucao(001);

                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _contaBancaria);
                c.Codigo = _codigoCedente != "" & _codigoCedente != null ? _codigoCedente : _contaBancaria.ToString();

                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                string nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();
                nossoNumero = nossoNumero.ToInt().ToString("000000000000000");

                Boleto b = new Boleto(vencimento, (decimal)_listaClientesEmissao[i].ValorParcela, _carteira, nossoNumero, c);
                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento;

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                instr.Descricao = "Não Receber após o vencimento";
                b.Instrucoes.Add(instr);

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                b.Banco = new Banco(_codigoBanco);

                //Remessa
                b.Remessa = new Remessa();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa; //"2"; // SIGCB - SEM REGISTRO
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa

                bb.Boleto = b;
                bb.Boleto.Valida();
                boletos.Add(bb);
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                _caminhoArquivoRemessa += "\\ArquivoRemessa_Safra_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".rem";

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa, _proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }
        #endregion


        #region BOLETO REAL
        public void GeraBoletoReal(int qtde)
        {
            // Cria o boleto, e passa os parâmetros usuais
            BoletoBancario bb;
            bb = new BoletoBancario();

            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {
                bb = new BoletoBancario();
                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                Instrucao instr = new Instrucao(001);
                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _contaBancaria);
                c.Codigo = _codigoCedente != "" & _codigoCedente != null ? _codigoCedente : _contaBancaria.ToString();

                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                string nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();

                Boleto b = new Boleto(vencimento, (decimal)_listaClientesEmissao[i].ValorParcela, _carteira, nossoNumero, c);
                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento;

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                instr.Descricao = "Não Receber após o vencimento";
                b.Instrucoes.Add(instr);

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                //Remessa
                b.Remessa = new Remessa();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa; //"2"; // SIGCB - SEM REGISTRO
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa

                bb.Boleto = b;
                bb.Boleto.Valida();

                boletos.Add(bb);
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                _caminhoArquivoRemessa += "\\ArquivoRemessa_Real_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".rem";

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa, _proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }
        #endregion


        #region BOLETO HSBC
        public void GeraBoletoHsbc(int qtde)
        {
            // Cria o boleto, e passa os parâmetros usuais
            BoletoBancario bb;
            bb = new BoletoBancario();

            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {
                bb = new BoletoBancario();
                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                Instrucao instr = new Instrucao(001);
                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _digitoAgenciaBancaria, _contaBancaria, _digitoContaBancaria);
                
                // Código fornecido pela agencia, NÃO é o numero da conta
                c.Codigo = _codigoCedente != "" & _codigoCedente != null ? _codigoCedente : _contaBancaria.ToString(); // 7 posicoes

                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                string nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();

                Boleto b = new Boleto(vencimento, (decimal) _listaClientesEmissao[i].ValorParcela, _carteira, nossoNumero, c); //cod documento
                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento; // nr documento

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                instr.Descricao = "Não Receber após o vencimento";
                b.Instrucoes.Add(instr);

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                //Remessa
                b.Remessa = new Remessa();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa; //"2"; // SIGCB - SEM REGISTRO
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa

                bb.Boleto = b;
                bb.Boleto.Valida();

                boletos.Add(bb);
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                _caminhoArquivoRemessa += "\\ArquivoRemessa_HSBC_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".rem";

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa, _proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }
        #endregion


        #region BOLETO BANCO DO BRASIL

        public void GeraBoletoBB(int qtde)
        {
            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();

            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {
                // Cria o boleto, e passa os parâmetros usuais
                BoletoBancario bb;

                bb = new BoletoBancario();

                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _digitoAgenciaBancaria, _contaBancaria, _digitoContaBancaria);
                c.MostrarCNPJnoBoleto = true;
                c.Endereco = new Endereco { End = _empresaCedente.DadosEmpresa.Endereco.Rua, Cidade = _empresaCedente.DadosEmpresa.Endereco.Cidade.Descricao,
                                                Bairro = _empresaCedente.DadosEmpresa.Endereco.Bairro, CEP = _empresaCedente.DadosEmpresa.Endereco.CEP,
                                                UF = _empresaCedente.DadosEmpresa.Endereco.Cidade.Estado.UF, Complemento = _empresaCedente.DadosEmpresa.Endereco.Complemento,
                                                Numero = _empresaCedente.DadosEmpresa.Endereco.Numero
                                           };
                
                c.Codigo = _codigoCedente != "" & _codigoCedente != null ? _codigoCedente : _contaBancaria.ToString();
                c.Convenio = _convenioRemessa.ToLong();
                c.Carteira = _carteira;
                c.Variacao = _variacao;
                
                if (_NossoNumero == string.Empty)
                {
                    string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                    _NossoNumero = numeroDocumento.Trim() + _listaClientesEmissao[i].Pessoa.Id.ToString();
                }
                else
                {
                    _NossoNumero = (_NossoNumero.ToInt() + 1).ToString();
                }

                //string nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();

                Boleto b = new Boleto(vencimento, (decimal)_listaClientesEmissao[i].ValorParcela, _carteira, _NossoNumero, c);

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);                
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id); 
                
                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos.Count > 0 ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos.Count > 0 ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos.Count > 0 ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos.Count > 0 ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos.Count > 0 ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                for (int x = 0; x < _listaInstrucao.Count; x++)
                {
                    Instrucao_BancoBrasil item = new Instrucao_BancoBrasil();

                    if (_listaInstrucao[x].TipoValor != null)
                    {
                        AbstractInstrucao.EnumTipoValor tipoValor = _listaInstrucao[x].TipoValor == 0 ? AbstractInstrucao.EnumTipoValor.Reais : AbstractInstrucao.EnumTipoValor.Percentual;

                        item = new Instrucao_BancoBrasil(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Valor.ToDouble(), tipoValor);
                    }
                    else
                        item = new Instrucao_BancoBrasil(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Dias.ToInt());

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 81)
                    {
                        item.Descricao += " " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";

                    }
                    // juros/descontos

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 999)
                    {
                        item = new Instrucao_BancoBrasil(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Dias.ToInt());
                        item.Descricao += " " + _listaInstrucao[x].Valor + (" por dia de antecipação.");
                    }

                    b.Instrucoes.Add(item);
                }

                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento;

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                b.Banco = new Banco (_codigoBanco);

                //Remessa
                b.Remessa = new Remessa();                
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa;
                b.Remessa.NumeroLote = _proximoLote;
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa

                bb.MostrarEnderecoCedente = true;
                
                bb.Boleto = b;

                bb.Boleto.Valida();

                boletos.Add(bb);                
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                buscaNomeDoArquivoBancoBrasil();

                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                Banco banco = new Banco(_codigoBanco);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(banco, c, boletosRemessa, _caminhoArquivoRemessa, _proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);

            //****Vamos atualizar para o proximo lote e nosso número de arquivo*****
            ServicoConfiguracaoBoleto servicoConfiguracao = new ServicoConfiguracaoBoleto();
            var configuracao = servicoConfiguracao.ConsultePeloPerfil(cboPerfisEmissaoBoleto.EditValue.ToInt());
            configuracao.ProximoLote = configuracao.ProximoLote == 0 ? 0 : _proximoLote + 1;
            configuracao.NossoNumero = string.IsNullOrEmpty(configuracao.NossoNumero)? "" : _NossoNumero;
            servicoConfiguracao.Atualize(configuracao);            
        }

        private void buscaNomeDoArquivoBancoBrasil()
        {
           _caminhoArquivoRemessa += "\\" + "CBR_" +"Lote_" + _proximoLote + ".REM";           
        }

        #endregion


        #region BOLETO BRADESCO
        public void GeraBoletoBradesco(int qtde)
        {
            // Cria o boleto, e passa os parâmetros usuais
            BoletoBancario bb;
            bb = new BoletoBancario();

            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {
                bb = new BoletoBancario();
                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _digitoAgenciaBancaria, _contaBancaria, _digitoContaBancaria);
                c.Codigo = _codigoCedente != "" & _codigoCedente != null ? _codigoCedente : _contaBancaria.ToString();

                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                string nossoNumero = numeroDocumento.ToInt().ToString("00000000000");//_listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();

                Boleto b = new Boleto(vencimento, (decimal)_listaClientesEmissao[i].ValorParcela, _carteira, nossoNumero, c);
                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ","");
                b.NumeroDocumento = b.NumeroDocumento.ToInt().ToString("0000000000");

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;
                b.Sacado.Endereco.Numero = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Numero : null;
                b.Sacado.Endereco.Complemento = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Complemento : null;

                for (int x = 0; x < _listaInstrucao.Count; x++)
                {
                    Instrucao_Bradesco item = new Instrucao_Bradesco();

                    if (_listaInstrucao[x].TipoValor != null)
                    {
                        AbstractInstrucao.EnumTipoValor tipoValor = _listaInstrucao[x].TipoValor == 0 ? AbstractInstrucao.EnumTipoValor.Reais : AbstractInstrucao.EnumTipoValor.Percentual;

                        item = new Instrucao_Bradesco(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Valor.ToDouble(), tipoValor);
                    }
                    else
                        item = new Instrucao_Bradesco(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Dias.ToInt());

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 81)
                    {
                        item.Descricao += " " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";

                    }
                    // juros/descontos

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 999)
                    {
                        item.Descricao += " " + _listaInstrucao[x].Valor + (" por dia de antecipação.");
                    }

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 9)
                    {
                        item.Descricao += " após " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";
                    }

                        b.Instrucoes.Add(item);
                }

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;
                
                b.Banco = new Banco(_codigoBanco);

                bb.Boleto = b;
                bb.Boleto.Valida();

                boletos.Add(bb);

                //Remessa
                b.Remessa = new Remessa();
                var NossoNumeroSemDigito = b.NossoNumero.Split('-');
                b.NossoNumero = NossoNumeroSemDigito[0].RemoverCaracteresDeMascara();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa;
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa

                
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {   
                buscaNomeDoArquivoBradesco();

                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa, _proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }

        private void buscaNomeDoArquivoBradesco()
        {
            var quantidadeDeArquivosHoje = verificaSeGerouArquivoHoje();

            if (quantidadeDeArquivosHoje == 0)
                _caminhoArquivoRemessa += "\\" + "CB"+ DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + "01"+ ".REM";
            else
            {
                quantidadeDeArquivosHoje += 1;
                _caminhoArquivoRemessa += "\\" + "CB" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + quantidadeDeArquivosHoje.ToString("00") + ".REM";
            }
        }

        #endregion

        #region BOLETO NORDESTE
        public void GeraBoletoBNB(int qtde)
        {
            // Cria o boleto, e passa os parâmetros usuais
            BoletoBancario bb;
            bb = new BoletoBancario();

            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {
                bb = new BoletoBancario();
                bb.CodigoBanco = _codigoBanco;
                
                ContaBancaria conta = new ContaBancaria();
                conta.Agencia = _agenciaBancaria;
                conta.DigitoAgencia = _digitoAgenciaBancaria;
                conta.Conta =  _contaBancaria;
                conta.DigitoConta = _digitoContaBancaria;

                c = new Cedente();
                c.ContaBancaria = conta;
                c.CPFCNPJ = _empresaCedente.DadosEmpresa.Cnpj;
                c.Nome = _empresaCedente.DadosEmpresa.RazaoSocial;
                
                Boleto b = new Boleto();
                b.Cedente = c;
                //
                b.DataProcessamento = DateTime.Now;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                string nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();

                b.ValorBoleto = (decimal)_listaClientesEmissao[i].ValorParcela;
                b.Carteira = _carteira;
                b.NossoNumero = nossoNumero;
                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                b.Banco = new Banco(004);

                EspecieDocumento especiedocumento = new EspecieDocumento(_codigoBanco, _especie);//Duplicata Mercantil
                b.EspecieDocumento = especiedocumento;

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                //Remessa
                b.Remessa = new Remessa();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa;
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa

                bb.Boleto = b;
                bb.Boleto.Valida();
                boletos.Add(bb);
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                _caminhoArquivoRemessa += "\\ArquivoRemessa_BcoNordeste_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".rem";

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa,_proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }
        #endregion

        #region Boleto Santander

        private void GeraBoletoSantander(int qtde)
        {
            // Cria o boleto, e passa os parâmetros usuais
            BoletoBancario bb;
            bb = new BoletoBancario();

            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {
                bb = new BoletoBancario();
                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _contaBancaria);

                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                string nossoNumero = _listaClientesEmissao[i].Pessoa.Id.ToString() + numeroDocumento.Trim();
                
                c.Codigo = _codigoCedente != "" & _codigoCedente != null? _codigoCedente : _contaBancaria;

                Boleto b = new Boleto(vencimento, (decimal)_listaClientesEmissao[i].ValorParcela, _carteira, nossoNumero, c, new EspecieDocumento(_codigoBanco, _especie));

                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento;

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                for (int x = 0; x < _listaInstrucao.Count; x++)
                {
                    Instrucao_Santander item = new Instrucao_Santander();

                    if (_listaInstrucao[x].TipoValor != null)
                    {
                        AbstractInstrucao.EnumTipoValor tipoValor = _listaInstrucao[x].TipoValor == 0 ? AbstractInstrucao.EnumTipoValor.Reais : AbstractInstrucao.EnumTipoValor.Percentual;

                        item = new Instrucao_Santander(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Valor.ToDouble(), tipoValor);
                    }
                    else
                        item = new Instrucao_Santander(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Dias.ToInt());

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 81)
                    {
                        item.Descricao += " " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";

                    }
                    // juros/descontos

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 999)
                    {
                        item.Descricao += " " + _listaInstrucao[x].Valor + (" por dia de antecipação.");
                    }

                    b.Instrucoes.Add(item);
                }

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                //Remessa
                b.Remessa = new Remessa();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa;
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa
                
                bb.Boleto = b;
                bb.Boleto.Valida();

                boletos.Add(bb);
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                _caminhoArquivoRemessa += "\\ArquivoRemessa_Santander_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".rem";

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa, _proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(bb.Banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }

        #endregion

        #region Boleto Sicred

        private void GeraBoletoSicred(int qtde)
        {
            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {
                // Cria o boleto, e passa os parâmetros usuais
                BoletoBancario bb;
                bb = new BoletoBancario();

                bb = new BoletoBancario();
                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();

                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _digitoAgenciaBancaria, _contaBancaria, _digitoContaBancaria);

                string nossoNumero;
                                
                string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                nossoNumero = numeroDocumento.ToInt().ToString("00000000");
                
                c.Codigo = _codigoCedente != "" & _codigoCedente != null ? _codigoCedente : _contaBancaria;

                Boleto b = new Boleto(vencimento, (decimal)_listaClientesEmissao[i].ValorParcela, _carteira, nossoNumero, c, new EspecieDocumento(_codigoBanco, _especie));

                //Se tiver juros e multa ou descontos               
                b.ValorMulta = (decimal)(_listaClientesEmissao[i].ValorTotal - _listaClientesEmissao[i].ValorParcela);
                b.ValorDesconto = (decimal)_listaClientesEmissao[i].Desconto;
                b.ValorCobrado = b.ValorMulta != 0 ? (decimal)_listaClientesEmissao[i].ValorTotal : 0;
                
                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento;

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                for (int x = 0; x < _listaInstrucao.Count; x++)
                {
                    Instrucao_Sicredi item = new Instrucao_Sicredi();

                    if (_listaInstrucao[x].TipoValor != null)
                    {
                        AbstractInstrucao.EnumTipoValor tipoValor = _listaInstrucao[x].TipoValor == 0 ? AbstractInstrucao.EnumTipoValor.Reais : AbstractInstrucao.EnumTipoValor.Percentual;

                        item = new Instrucao_Sicredi(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Valor.ToDouble(), tipoValor);
                    }
                    else
                        item = new Instrucao_Sicredi(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Dias.ToInt());

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 81)
                    {
                        item.Descricao += " " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";

                    }
                    // juros/descontos

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 999)
                    {
                        item.Descricao += " " + _listaInstrucao[x].Valor + (" por dia de antecipação.");
                    }

                    b.Instrucoes.Add(item);
                }

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;

                //Código da Operação
                b.Cedente.ContaBancaria.OperacaConta = b.Cedente.Codigo.Substring(4,2);

                //Conta Bancária do Boleto
                b.ContaBancaria.Agencia = _agenciaBancaria;

                //******Beneficiario**************************************************************************************

                var cedente = _codigoCedente.Substring(6);

                b.Cedente.ContaBancaria.Conta = cedente;

                //*****Fim ***********************************************************************************************

                bb.Boleto = b;                
                bb.Boleto.Valida();
                
                boletos.Add(bb);

                //Remessa
                b.Remessa = new Remessa();
                var NossoNumeroSemDigito = b.NossoNumero.Split('-');
                b.NossoNumero = NossoNumeroSemDigito[0].RemoverCaracteresDeMascara();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa;
                b.Remessa.CodigoOcorrencia = string.Empty;
                boletosRemessa.Add(b);
                //Fim Remessa
                
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
                buscaNomeDoArquivoSicred();

                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                Banco banco = new Banco(_codigoBanco);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(banco, c, boletosRemessa, _caminhoArquivoRemessa,_proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;                       
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);
        }

        int quantidadeDeArquivosGeradosHoje;
        private int verificaSeGerouArquivoHoje()
        {
            quantidadeDeArquivosGeradosHoje = 0;

            DirectoryInfo diretorio = new DirectoryInfo(_caminhoArquivoRemessa);
            FileInfo[] arquivos = diretorio.GetFiles();

            Array.Sort(arquivos, delegate (FileInfo a, FileInfo b) { return DateTime.Compare(a.CreationTime, b.CreationTime); });
            
            foreach (FileInfo arquivo in arquivos)
            {               
                if (arquivo.CreationTime.ToString("ddMMyyyy") == DateTime.Today.ToString("ddMMyyyy"))
                    quantidadeDeArquivosGeradosHoje ++;
            }

            return quantidadeDeArquivosGeradosHoje;
        }
        
        private void buscaNomeDoArquivoSicred()
        {   
            var quantidadeDeArquivosHoje = verificaSeGerouArquivoHoje();

            if (quantidadeDeArquivosHoje == 0)
                _caminhoArquivoRemessa += "\\" + _codigoCedente.Substring(6) + retornaOMesDoArquivo() + DateTime.Now.ToString("dd") + ".CRM";
            else
            {
                quantidadeDeArquivosHoje +=1;
                _caminhoArquivoRemessa += "\\" + _codigoCedente.Substring(6) + retornaOMesDoArquivo() + DateTime.Now.ToString("dd") + ".RM" + quantidadeDeArquivosHoje;
            }
        }

        private string retornaOMesDoArquivo()
        {
            string mes = DateTime.Now.ToString("MM");
            
            switch (mes)
            {
                case "01":
                    mes = "1";
                    break;
                case "02":
                    mes= "2";
                    break;
                case "03":
                    mes = "3";
                    break;
                case "04":
                    mes = "4";
                    break;
                case "05":
                    mes = "5";
                    break;
                case "06":
                    mes = "6";
                    break;
                case "07":
                    mes = "7";
                    break;
                case "08":
                    mes = "8";
                    break;
                case "09":
                    mes = "9";
                    break;
                case "10":
                    mes = "O";
                    break;
                case "11":
                    mes = "N";
                    break;
                case "12":
                    mes = "D";
                    break;
            }
            
            return mes;
        }

        #region Boleto Sicoob

        private void GeraBoletoSicoob(int qtde)
        {
            Cedente c = new Cedente();

            List<BoletoBancario> boletos = new List<BoletoBancario>();
            Boletos boletosRemessa = new Boletos();

            for (int i = 0; i < _listaClientesEmissao.Count; i++)
            {
                // Cria o boleto, e passa os parâmetros usuais
                BoletoBancario bb;
                bb = new BoletoBancario();

                bb = new BoletoBancario();
                bb.CodigoBanco = _codigoBanco;

                string dataVencimento = _listaClientesEmissao[i].DataVencimento.ToString();

                DateTime vencimento = dataVencimento.ToDate();
                c = new Cedente(_empresaCedente.DadosEmpresa.Cnpj, _empresaCedente.DadosEmpresa.RazaoSocial, _agenciaBancaria, _digitoAgenciaBancaria, _contaBancaria, _digitoContaBancaria);

                if (_NossoNumero == string.Empty)
                {
                    string numeroDocumento = _listaClientesEmissao[i].NumeroDocumento.RemoverCaracteresDeMascara().Replace(" ", "");
                    _NossoNumero = numeroDocumento.Trim() + _listaClientesEmissao[i].Pessoa.Id.ToString();
                }
                else
                {
                    _NossoNumero = (_NossoNumero.ToInt() + 1).ToString();
                }

                c.Codigo = _codigoCedente != "" & _codigoCedente != null ? _codigoCedente.PadLeft(6,'0') : _contaBancaria.PadLeft(6,'0');
                c.DigitoCedente = _digitoCedente;
                c.Carteira = _carteira;

                Boleto b = new Boleto(vencimento, (decimal)_listaClientesEmissao[i].ValorTotal, _carteira, _NossoNumero, c, new EspecieDocumento(_codigoBanco, _especie));
                
                //var TotalJuros = (decimal)RetorneValorJuros(_listaClientesEmissao[i], vencimento, false);
                //var TotalMulta = (decimal)RetorneValorMulta(_listaClientesEmissao[i], vencimento, false);
                //var TotalDesconto = (decimal)_listaClientesEmissao[i].Desconto;

                //if (TotalJuros> 0 || TotalMulta > 0 || TotalDesconto > 0)
                //{
                //    Instrucao_Sicoob item = new Instrucao_Sicoob();

                //    item = new Instrucao_Sicoob(EnumInstrucoes_Sicoob.JurosCobrados.ToInt(),30, _listaClientesEmissao[i].Multa);

                //    b.Instrucoes.Add(item);
                //}
                    
                b.NumeroDocumento = _listaClientesEmissao[i].NumeroDocumento;
                b.TipoModalidade= _modalidade;               

                //***Calcular a parcela
                var vetorParcela = _listaClientesEmissao[i].NumeroDocumento.Split('-');
                var parcela = vetorParcela[1].ToString().Split('/');

                b.NumeroParcela = parcela[0].ToString().Trim().ToInt();
                //***Fim parcela

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                _listaClientesEmissao[i].Pessoa = servicoPessoa.ConsulteClienteAtivo(_listaClientesEmissao[i].Pessoa.Id);

                b.Sacado = new Sacado(_listaClientesEmissao[i].Pessoa.DadosGerais.CpfCnpj, _listaClientesEmissao[i].Pessoa.DadosGerais.Razao);
                b.Sacado.Endereco.End = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Rua : null;
                b.Sacado.Endereco.Bairro = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Bairro : null;
                b.Sacado.Endereco.Cidade = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;
                b.Sacado.Endereco.CEP = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].CEP : null;
                b.Sacado.Endereco.UF = _listaClientesEmissao[i].Pessoa.ListaDeEnderecos != null ? _listaClientesEmissao[i].Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                decimal jurosMora = 0;

                for (int x = 0; x < _listaInstrucao.Count; x++)
                {
                    Instrucao_Sicoob item = new Instrucao_Sicoob();

                    if (_listaInstrucao[x].Valor != 0 && _listaInstrucao[x].Dias != 0)
                    {
                       item = new Instrucao_Sicoob(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Dias.ToInt(), _listaInstrucao[x].Valor.ToDouble());
                    }
                    else if (_listaInstrucao[x].TipoValor != null)
                        item = new Instrucao_Sicoob(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Valor.ToDouble());
                    else if(_listaInstrucao[x].Dias != 0)
                        item = new Instrucao_Sicoob(_listaInstrucao[x].CodigoInstrucao.ToInt(), _listaInstrucao[x].Dias.ToInt());
                    else
                        item = new Instrucao_Sicoob(_listaInstrucao[x].CodigoInstrucao.ToInt());

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 81)
                    {
                        item.Descricao += " " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";

                    }
                    // juros/descontos

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 999)
                    {
                        item.Descricao += " " + _listaInstrucao[x].Valor + (" por dia de antecipação.");
                    }

                    if (_listaInstrucao[x].CodigoInstrucao.ToInt() == 1)
                    {
                        jurosMora = (decimal)_listaInstrucao[x].Valor;
                    }


                    b.Instrucoes.Add(item);
                }

                //Local de Pagamento
                b.LocalPagamento = _localDePagamento;
                
                //Remessa
                b.Remessa = new Remessa();
                b.Remessa.TipoDocumento = _tipoDocumentoRemessa;
                b.Remessa.NumeroLote = _proximoLote;
                b.Remessa.CodigoOcorrencia = "1";
                b.JurosMora = jurosMora;

                boletosRemessa.Add(b);
                //Fim Remessa

                bb.Boleto = b;
                bb.Boleto.Valida();

                boletos.Add(bb);
            }

            if (_caminhoArquivoRemessa != string.Empty)
            {
               trataInformacoesCamposFalhos(boletosRemessa);

                TipoArquivo tipoArquivo = _padraoRemessa == 400 ? TipoArquivo.CNAB400 : TipoArquivo.CNAB240;

                _caminhoArquivoRemessa += "\\ArquvioRemessa_Sicoob_Lote_" + DateTime.Now.ToString("dd_MM_yyyy_ss") + ".rem";

                Banco banco = new Banco(_codigoBanco);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        GeraArquivoCNAB240(banco, c, boletosRemessa, _caminhoArquivoRemessa, _proximoLote);
                        break;
                    case TipoArquivo.CNAB400:
                        if (!GeraArquivoCNAB400(banco, c, boletosRemessa, _caminhoArquivoRemessa)) return;
                        break;
                    default:
                        break;
                }
            }

            GeraLayout(boletos);

            //****Vamos atualizar para o proximo **LOTE** e **NOSSO NUMERO** de arquivo*****
            ServicoConfiguracaoBoleto servicoConfiguracao = new ServicoConfiguracaoBoleto();
            var configuracao = servicoConfiguracao.ConsultePeloPerfil(cboPerfisEmissaoBoleto.EditValue.ToInt());
            configuracao.ProximoLote = configuracao.ProximoLote == 0? 0:_proximoLote + 1;
            configuracao.NossoNumero = configuracao.NossoNumero == ""?"" : _NossoNumero;            
            servicoConfiguracao.Atualize(configuracao);
        }

        private void trataInformacoesCamposFalhos(Boletos boletosRemessa)
        {
            foreach(var boleto in boletosRemessa)
            {
                boleto.NossoNumero = boleto.NossoNumero.RemoverCaracteresDeMascara();
                boleto.ContaBancaria.Agencia = _agenciaBancaria;
                boleto.ContaBancaria.DigitoAgencia = _digitoAgenciaBancaria;
                boleto.ContaBancaria.Conta = _contaBancaria;
                boleto.ContaBancaria.DigitoConta = _digitoContaBancaria;
            }                
        }

        #endregion

        #endregion


        #region Remessa

        public bool GeraArquivoCNAB400(IBanco banco, Cedente cedente, Boletos boletos, string caminhoArquivo)
        {
            try
            {   
                    ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB400);

                    //Valida a Remessa Correspondentes antes de Gerar a mesma...
                string vMsgRetorno = string.Empty;
                    bool vValouOK = arquivo.ValidarArquivoRemessa(_convenioRemessa, banco, cedente, boletos, 1, out vMsgRetorno);
                    if (!vValouOK)
                    {
                        MessageBox.Show(String.Concat("Foram localizados inconsistências na validação da remessa!", Environment.NewLine, vMsgRetorno),
                                        "Arquivo de Remessa",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        return false;
                    }
                    else
                    {
                        var arquivoRemessa = System.IO.File.Create(caminhoArquivo);

                        arquivo.GerarArquivoRemessa(_convenioRemessa, banco, cedente, boletos, arquivoRemessa, 1);

                            MessageBox.Show("Arquivo gerado com sucesso!", "Arquivo de Remessa",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }

        public void GeraArquivoCNAB240(IBanco banco, Cedente cedente, Boletos boletos, string caminhoArquivo, int numeroLoteArquivo)
        {
            try
            {
                var arquivoRemessa = System.IO.File.Create(caminhoArquivo);

                ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB240);
                arquivo.GerarArquivoRemessa(cedente.Convenio.ToString(), banco, cedente, boletos, arquivoRemessa, numeroLoteArquivo);

                MessageBox.Show("Arquivo gerado com sucesso!", "Arquivo de Remessa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                arquivoRemessa.Close();
            }
            catch (Exception ex)
            {   
                   MessageBox.Show(ex.Message,"Arquivo Remessa",MessageBoxButtons.OK,MessageBoxIcon.Error);                   
            }
            
        }
       
    #endregion
        
       #region Eventos do BackgroundWorker

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            switch (CodigoBanco)
            {
                case 1: // Banco do Brasil
                    GeraBoletoBB((int)numericUpDown.Value);
                    break;

                case 409: // Unibanco
                    GeraBoletoUnibanco((int)numericUpDown.Value);
                    break;

                case 347: // Sudameris
                    GeraBoletoSudameris((int)numericUpDown.Value);
                    break;

                case 422: // Safra
                    GeraBoletoSafra((int)numericUpDown.Value);
                    break;

                case 341: // Itau
                    GeraBoletoItau((int)numericUpDown.Value);
                    break;

                case 356: // Real
                    GeraBoletoReal((int)numericUpDown.Value);
                    break;

                case 399: // Hsbc
                    GeraBoletoItau((int)numericUpDown.Value);
                    break;

                case 237: // Bradesco
                    GeraBoletoBradesco((int)numericUpDown.Value);
                    break;

                case 104: // Caixa
                    GeraBoletoCaixa((int)numericUpDown.Value);
                    break;
                case 4: //BNB
                    GeraBoletoBNB((int)numericUpDown.Value);
                    break;
                case 33: //Santander
                    GeraBoletoSantander((int)numericUpDown.Value);
                    break;
                case 748: //Sicred
                    GeraBoletoSicred((int)numericUpDown.Value);
                    break;
                case 756: //Sicoob
                    GeraBoletoSicoob((int)numericUpDown.Value);
                    break;
            }

        }

        private void GeraBoletoBanco()
        {
            switch (CodigoBanco)
            {
                case 1: // Banco do Brasil
                    GeraBoletoBB((int)numericUpDown.Value);
                    break;

                case 409: // Unibanco
                    GeraBoletoUnibanco((int)numericUpDown.Value);
                    break;

                case 347: // Sudameris
                    GeraBoletoSudameris((int)numericUpDown.Value);
                    break;

                case 422: // Safra
                    GeraBoletoSafra((int)numericUpDown.Value);
                    break;

                case 341: // Itau
                    GeraBoletoItau((int)numericUpDown.Value);
                    break;

                case 356: // Real
                    GeraBoletoReal((int)numericUpDown.Value);
                    break;

                case 399: // Hsbc
                    GeraBoletoItau((int)numericUpDown.Value);
                    break;

                case 237: // Bradesco
                    GeraBoletoBradesco((int)numericUpDown.Value);
                    break;

                case 104: // Caixa
                    GeraBoletoCaixa((int)numericUpDown.Value);
                    break;
                case 4: //BNB
                    GeraBoletoBNB((int)numericUpDown.Value);
                    break;
                case 33: //Santander
                    GeraBoletoSantander((int)numericUpDown.Value);
                    break;
                case 748: //Sicred
                    GeraBoletoSicred((int)numericUpDown.Value);
                    break;
                case 756: //Sicoob
                    GeraBoletoSicoob((int)numericUpDown.Value);
                    break;
            }

        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //_progresso.Close();

            // Cria um formulário com um componente WebBrowser dentro
            _impressaoBoleto.webBrowser.Navigate(_arquivo);
            _impressaoBoleto.ShowDialog();

        }
        #endregion Eventos do BackgroundWorker



        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarregaDadosParaEmissaoBoletoDoPerfil();
            
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            GeraBoletoBanco();
            //backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            backgroundWorker.RunWorkerAsync();
            //_progresso = new Progresso();
            //_progresso.ShowDialog();
        }

        #region Classes Auxiliares

        private class AgenciaAuxiliarComboBox
        {
            public int Id { get; set; }

            public string Descricao { get; set; }
        }
        
        private class ItemInstrucaoGrid
        {
            public string Item { get; set; }
            public string Codigo { get; set; }
            public string Descricao { get; set; }
            public string Dias { get; set; }
            public string valor { get; set; }
            public string tipoValor { get; set; }
        }

        #endregion

#region Eventos Gerais de controles


        private void cboAgencias_EditValueChanged(object sender, EventArgs e)
        {
            if (cboAgencias.EditValue != null)
            {
                PreenchaCboContas();
            }
        }

        private void radioButtonItau_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
        }

        private void radioButtonBancoBrasil_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
        }

        private void radioButtonBradesco_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
        }

        private void radioButtonSudameris_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(false, false,false,false);
        }

        private void radioButtonSantander_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
        }

        private void radioButtonCaixa_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
        }

        private void radioButtonBNB_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
        }

        private void radioButtonHsbc_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(false, false, false, false);
        }

        private void radioButtonReal_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(false, false, false, false);
        }

        private void radioButtonSafra_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(false, false, false, false);
        }

        private void radioButtonUnibanco_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(false, false, false, false);
        }

        private void radioButtonBanrisul_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
        }

        private void radioButtonSicredi_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
        }

        private void radioButtonSicoob_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(true,true,true,false);
        }

        private void btnInserirInstrucoes_Click(object sender, EventArgs e)
        {
            InsiraInstrucao();
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == '.')
                {
                    e.Handled = (txtValor.Text.Contains("."));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtDigitoCedente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == '.')
                {
                    e.Handled = (txtValor.Text.Contains("."));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtModalidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == '.')
                {
                    e.Handled = (txtValor.Text.Contains("."));
                }
                else
                    e.Handled = true;
            }
        }

        #endregion

        private void MenuGrid_Click(object sender, EventArgs e)
        {
            if (gridinstrucoes.CurrentRow == null) return;

            _listaInstrucao.RemoveAt(gridinstrucoes.CurrentRow.Index);
            PreenchaGrid();
        }

        private void gridinstrucoes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Tem certeza que deseja excluir este item!", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _listaInstrucao.RemoveAt(gridinstrucoes.CurrentRow.Index);
                    PreenchaGrid();
                }
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            List<InstrucoesBoleto> linhaTempAtual = new List<InstrucoesBoleto>();
            List<InstrucoesBoleto> linhaTempAnterior = new List<InstrucoesBoleto>();

            try
            {
                int indexAtual = gridinstrucoes.SelectedCells[0].OwningRow.Index;

                if (indexAtual <= 0) return;
                
                linhaTempAtual.Add(_listaInstrucao[indexAtual]);
                _listaInstrucao.RemoveAt(indexAtual);
                
                linhaTempAnterior.Add(_listaInstrucao[indexAtual - 1]);
                _listaInstrucao.RemoveAt(indexAtual - 1);
                
                
                _listaInstrucao.Insert(indexAtual - 1, linhaTempAtual[0]);
                _listaInstrucao.Insert(indexAtual, linhaTempAnterior[0]);

                PreenchaGrid(false);

                gridinstrucoes[0, indexAtual - 1].Selected = true;
            }
            catch {}
        }

        private void btnPosterior_Click(object sender, EventArgs e)
        {
            List<InstrucoesBoleto> linhaTempAtual = new List<InstrucoesBoleto>();
            List<InstrucoesBoleto> linhaTempAnterior = new List<InstrucoesBoleto>();

            try
            {
                int indexAtual = gridinstrucoes.SelectedCells[0].OwningRow.Index;

                if (indexAtual == gridinstrucoes.Rows.Count-1) return;
                
                linhaTempAtual.Add(_listaInstrucao[indexAtual]);
                _listaInstrucao.RemoveAt(indexAtual);
                
                linhaTempAnterior.Add(_listaInstrucao[indexAtual]);
                _listaInstrucao.RemoveAt(indexAtual);
                                
                _listaInstrucao.Insert(indexAtual,linhaTempAnterior[0]);
                _listaInstrucao.Insert(indexAtual + 1,linhaTempAtual[0]);

                PreenchaGrid(false);

                gridinstrucoes[0, indexAtual + 1].Selected = true;
            }
            catch { }
        }
    }
}
