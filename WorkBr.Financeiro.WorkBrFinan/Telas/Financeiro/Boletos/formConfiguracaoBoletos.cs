using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using BoletoNet;
using Programax.Easy.View.Telas.Financeiro.Boletos;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio;
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
using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.PerfilEmissaoBoletoSev;
using Programax.Easy.Negocio.Financeiro.ConfiguracaoBoletoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ConfiguracaoBoletoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Cadastros.EnderecoServ;
using Programax.Easy.Servico.Financeiro.ConfiguracaoBoletoSev;

namespace Programax.Easy.View.Telas.Financeiro.Boletos
{
    public partial class FormConfiguracaoBoletos : FormularioPadrao
    {
        #region Variaveis Globais

        private short _codigoBanco = 0;
        
        string _arquivo = string.Empty;
       
        private Empresa _empresaCedente = new Empresa();
        private ServicoEmpresa _servicoEmpresa = new ServicoEmpresa();
        private string _contaBancaria;
        private string _digitoContaBancaria;
        private string _agenciaBancaria;
        private string _digitoAgenciaBancaria;
        public string _localDePagamento;
        private List<InstrucoesBoleto> _listaInstrucaoEmEdicao = new List<InstrucoesBoleto>();
        private List<PerfilEmissaoBoleto> _listaDePerfis = new List<PerfilEmissaoBoleto>();
        private int _intLinhaGrid;
        private string _carteira;
        private string _variacao;
        private string _codigoCedente;
        private int? _digitoCedente;
        private string _nossoNumero;
        private string _especie;
        private string _modalidade;
        
        private int _idConfiguracao;

        public short CodigoBanco
        {
            get { return _codigoBanco; }
            set { _codigoBanco = value; }
        }

        public int _QuantidadeBoletos { get; set; }
        public List<ContaPagarReceber> _listaClientesEmissao = new List<ContaPagarReceber>();        

# endregion

        public FormConfiguracaoBoletos()
        {
            InitializeComponent();

            _empresaCedente = _servicoEmpresa.ConsulteUltimaEmpresa();

            ObterBanco();
            PreenchaCboAgencias();
            PreencheCboPerfisEmissaoBoletos();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            carregaTipoValor();
           
            //Carrega Perfil Padrão
            //if (_listaDePerfis.Count != 0 && _listaDePerfis.Exists(x => x.EhPerfilPadrao == true))
                //cboPerfisEmissaoBoleto.EditValue = _listaDePerfis.Find(x => x.EhPerfilPadrao == true).Id.ToIntNullabel();
        }

        void _impressaoBoleto_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
            Dispose();            
        }

        #region Métodos Auxiliares

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

            for (int i = 0; i < _listaInstrucaoEmEdicao.Count; i++)
            {
                var item = _listaInstrucaoEmEdicao[i];

                ItemInstrucaoGrid itemInstrucaoGrid = new ItemInstrucaoGrid();
                
                itemInstrucaoGrid.Item = (i + 1).ToString();
                _listaInstrucaoEmEdicao[i].Item = itemInstrucaoGrid.Item.ToInt();
                itemInstrucaoGrid.Codigo = item.CodigoInstrucao.ToString();
                itemInstrucaoGrid.Descricao = item.DescricaoInstrucao;
                itemInstrucaoGrid.Dias = item.Dias.ToString();
                itemInstrucaoGrid.valor = item.Valor.ToString();
                itemInstrucaoGrid.tipoValor = item.TipoValor.ToString();

                listaInstrucoes.Add(itemInstrucaoGrid);
                _intLinhaGrid = i;
            }

            gridinstrucoes.DataSource = listaInstrucoes;
            if (_listaInstrucaoEmEdicao.Count > 0)
            {
                gridinstrucoes.FirstDisplayedScrollingRowIndex = _listaInstrucaoEmEdicao.Count - 1;
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
            
            retorneItemInstrucaoEmEdicao();
            
            PreenchaGrid();

            limpeInstrucoesParaGrid();
        }

        private void retorneItemInstrucaoEmEdicao()
        {
            InstrucoesBoleto itemInstrucao = new InstrucoesBoleto();

            itemInstrucao.DescricaoInstrucao = cboInstrucao.Text;

            itemInstrucao.CodigoInstrucao = cboInstrucao.EditValue.ToInt();

            itemInstrucao.Dias = numDias.Value.ToInt();

            itemInstrucao.Valor = txtValor.Text.ToDouble();

            itemInstrucao.TipoValor = cboTipoValor.EditValue.ToIntNullabel() != null? cboTipoValor.EditValue.ToIntNullabel(): null;

            itemInstrucao.ConfiguracaoBoleto = retornaConfiguracoesEmEdicao();

            _listaInstrucaoEmEdicao.Add(itemInstrucao);
            
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

        private void PreencheCboPerfisEmissaoBoletos()
        {  
            ServicoPerfilEmissaoBoleto servicoPerfil = new ServicoPerfilEmissaoBoleto();

            var listaPerfis = servicoPerfil.ConsulteLista();

            _listaDePerfis.Clear();
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
        
        private void CarregaDadosParaEmissaoBoleto()
        {   
            ObterBanco();

            var VetorConta = cboConta.Text.Split('-');
            var VetorAgencia = cboAgencias.Text.Split('-');

            _contaBancaria = VetorConta[0].Trim();
            _digitoContaBancaria = VetorConta[1].Trim();

            _agenciaBancaria = VetorAgencia[0].Trim();
            _digitoAgenciaBancaria = VetorAgencia[1].Trim();
            _localDePagamento = txtLocalPagamento.Text;

            _carteira = txtCarteira.Text;
            _variacao = txtVariacao.Text;
            _codigoCedente = txtCodigoCedente.Text;
            _digitoCedente = txtDigitoCedente.Text.ToInt();
            _nossoNumero = txtNossoNumero.Text;
            _especie = cboEspecie.EditValue.ToString();
            _modalidade = txtModalidade.Text;
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

        private List<InstrucoesBoleto> carregaInstrucoesParaSalvar()
        {
            List<InstrucoesBoleto> listaDeInstrucoes = new List<InstrucoesBoleto>();
            
            if (_listaInstrucaoEmEdicao.Count == 0) return null;

            for(int i=0; i < _listaInstrucaoEmEdicao.Count; i++)
            {
                InstrucoesBoleto instrucoes = new InstrucoesBoleto();
                //instrucoes.Id = _listaInstrucaoEmEdicao[i].Item.ToInt();
                instrucoes.Item = _listaInstrucaoEmEdicao[i].Item.ToInt();
                instrucoes.CodigoInstrucao = _listaInstrucaoEmEdicao[i].CodigoInstrucao;
                instrucoes.DescricaoInstrucao = _listaInstrucaoEmEdicao[i].DescricaoInstrucao.ToString();
                instrucoes.Dias = _listaInstrucaoEmEdicao[i].Dias;
                instrucoes.Valor = _listaInstrucaoEmEdicao[i].Valor;
                instrucoes.TipoValor = _listaInstrucaoEmEdicao[i].TipoValor.ToInt();
                instrucoes.ConfiguracaoBoleto = new ConfiguracaoBoleto();
                

                listaDeInstrucoes.Add(instrucoes);
            }

            return listaDeInstrucoes;
        }

        private ConfiguracaoBoleto retornaConfiguracoesEmEdicao()
        {
            ConfiguracaoBoleto Configuracao = new ConfiguracaoBoleto();

            Configuracao.Id = _idConfiguracao;
            Configuracao.Perfil = new PerfilEmissaoBoleto { Id = cboPerfisEmissaoBoleto.EditValue.ToInt() };
            Configuracao.Padrao = radioButtonCNAB400.Checked ? Convert.ToInt16(radioButtonCNAB400.Tag) : Convert.ToInt16(radioButtonCNAB240.Tag);

            ServicoBanco servicoBanco = new ServicoBanco();

            var bancoSelecionado = servicoBanco.ConsultePeloCodigoBanco(CodigoBanco.ToString());

            Configuracao.Banco = new Negocio.Financeiro.BancoObj.ObjetoDeNegocio.Banco { Id = bancoSelecionado.Id };
            Configuracao.Agencia = new Agencia { Id = cboAgencias.EditValue.ToInt() };
            Configuracao.ContaBancaria = new Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio.ContaBancaria { Id = cboConta.EditValue.ToInt() };
            Configuracao.Carteira = txtCarteira.Text;
            Configuracao.Variacao = txtVariacao.Text;
            Configuracao.CodigoCedente = txtCodigoCedente.Text != "" ? txtCodigoCedente.Text : null;
            Configuracao.DigitoCedente = txtDigitoCedente.Text.ToIntNullabel();
            Configuracao.NossoNumero = txtNossoNumero.Text;
            Configuracao.Modalidade = txtModalidade.Text;
            Configuracao.EspecieDocumento = cboEspecie.EditValue.ToIntNullabel();
            Configuracao.LocalPagamento = txtLocalPagamento.Text;
            Configuracao.CaminhoRemessa = txtDiretorioPadrao.Text;
            Configuracao.TipoDocumentoRemessa = txtTipoDocumentoRemessa.Text;
            Configuracao.ConvenioRemessa = txtConvenioRemessa.Text;
            Configuracao.ProximoLote = txtProximoLoteRemessa.Text.ToInt();

            Configuracao.ListaInstrucoes = _listaInstrucaoEmEdicao;

            return Configuracao;
        }

        private void carregaConfiguracoesDoPerfil(int idPerfil)
        {
            ServicoConfiguracaoBoleto servicoConfiguracaoBoleto = new ServicoConfiguracaoBoleto();

            var configuracoesBoleto = servicoConfiguracaoBoleto.ConsultePeloPerfil(idPerfil);

            if (configuracoesBoleto != null)
            {
                radioButtonCNAB240.Checked = radioButtonCNAB240.Tag.ToInt() == configuracoesBoleto.Padrao ? true : false;
                radioButtonCNAB400.Checked = radioButtonCNAB400.Tag.ToInt() == configuracoesBoleto.Padrao ? true : false;

                //Banco
                ServicoBanco servicoBanco = new ServicoBanco();
                var banco = servicoBanco.Consulte(configuracoesBoleto.Banco.Id);

                CarregarBanco(banco.Codigo.ToInt());

                //Agência
                ServicoAgencia servicoAgencia = new ServicoAgencia();
                var agencia = servicoAgencia.Consulte(configuracoesBoleto.Agencia.Id);
                cboAgencias.EditValue = agencia.Id;

                //Conta
                ServicoContaBancaria servicoConta = new ServicoContaBancaria();
                var contaBancara = servicoConta.Consulte(configuracoesBoleto.ContaBancaria.Id);
                cboConta.EditValue = contaBancara.Id;

                txtCarteira.Text = configuracoesBoleto.Carteira;
                txtVariacao.Text = configuracoesBoleto.Variacao;
                txtCodigoCedente.Text = configuracoesBoleto.CodigoCedente;
                txtDigitoCedente.Text = configuracoesBoleto.DigitoCedente.ToString();
                txtModalidade.Text = configuracoesBoleto.Modalidade.ToString();
                cboEspecie.EditValue = configuracoesBoleto.EspecieDocumento;
                txtNossoNumero.Text = configuracoesBoleto.NossoNumero;
                txtLocalPagamento.Text = configuracoesBoleto.LocalPagamento;
                txtDiretorioPadrao.Text = configuracoesBoleto.CaminhoRemessa;
                txtTipoDocumentoRemessa.Text = configuracoesBoleto.TipoDocumentoRemessa;
                txtConvenioRemessa.Text = configuracoesBoleto.ConvenioRemessa;
                txtProximoLoteRemessa.Text = configuracoesBoleto.ProximoLote.ToString();

                rdbPerfilPadraoSim.Checked = _listaDePerfis.Find(x => x.Id == cboPerfisEmissaoBoleto.EditValue.ToInt()).EhPerfilPadrao ? true : false;

                configuracoesBoleto = servicoConfiguracaoBoleto.Consulte(configuracoesBoleto.Id);

                _idConfiguracao = configuracoesBoleto.Id;

                _listaInstrucaoEmEdicao = configuracoesBoleto.ListaInstrucoes.ToList();

                PreenchaGrid();

            }
            else
            {
                limpaPerfil();
                zerarIdConfiguracoes();
            }
        }
        
        private void limpaPerfil()
        {   
            cboAgencias.EditValue = null;

            cboConta.EditValue = null;

            txtCarteira.Text = string.Empty;
            txtVariacao.Text = string.Empty;
            txtCodigoCedente.Text = string.Empty;
            txtDigitoCedente.Text = string.Empty;
            txtModalidade.Text = string.Empty;
            cboEspecie.EditValue = string.Empty;
            txtNossoNumero.Text = string.Empty;
            txtLocalPagamento.Text = string.Empty;
            txtTipoDocumentoRemessa.Text = string.Empty;
            txtConvenioRemessa.Text = string.Empty;
            txtProximoLoteRemessa.Text = string.Empty;
            rdbPerfilPadraoSim.Checked = false;
            rdbPerfilPadraoNao.Checked = true;

            _listaInstrucaoEmEdicao = new List<InstrucoesBoleto>();

            PreenchaGrid();
        }
        
        private void zerarIdConfiguracoes()
        {
            _idConfiguracao = 0;
        }

        private bool valideCamposObrigatorios()
        {
            //Sicred
            if (CodigoBanco == radioButtonSicredi.Tag.ToInt())
            {
                if (txtCodigoCedente.Text == string.Empty)
                {
                    MessageBoxAkil.Show("O codigo do cedente deve ser informado no formato AAAAPPCCCCC, " +
                                        "onde: AAAA = Número da agência, PP = Posto do beneficiário, CCCCC = Código do beneficiário", "Validação Cedente", MessageBoxButtons.OK);
                    return false;
                }

                if (txtCarteira.Text == string.Empty || txtCarteira.Text != "1" & txtCarteira.Text != "3")
                {
                    MessageBoxAkil.Show("Informe a Carteira, " +
                                        "sendo: 1 - Com registro, 3 - Sem registro", "Validação Carteira", MessageBoxButtons.OK);
                    return false;
                }

                var numeroAgencia = cboAgencias.Text.Split('-');
                var numeroConta = cboConta.Text.Split('-');

                if (!txtCodigoCedente.Text.StartsWith(numeroAgencia[0].Trim())) //|| !txtCodigoCedente.Text.EndsWith(numeroConta[0].Trim()))
                 {
                    MessageBoxAkil.Show("O codigo do cedente deve ser informado no formato AAAAPPCCCCC, " +
                                       "onde: AAAA = Número da agência, PP = Posto do beneficiário, CCCCC = Código do beneficiário", "Validação Cedente", MessageBoxButtons.OK);
                    return false;
                }

                if (txtTipoDocumentoRemessa.Text == string.Empty)
                {
                    MessageBoxAkil.Show("Informe o Tipo de Documento! Deverão ser: A = SICREDI com Registro; C1 = SICREDI sem Registro Impressão Completa pelo Sicredi; " +
                                        "C2 = SICREDI sem Registro Pedido de bloquetos pré-impressos", MessageBoxButtons.OK);
                    return false;
                }

                if (!txtTipoDocumentoRemessa.Text.Equals("A") && !txtTipoDocumentoRemessa.Text.Equals("C1") && !txtTipoDocumentoRemessa.Text.Equals("C2"))
                {
                    MessageBoxAkil.Show("Tipo de Documento Inválido! Deverão ser: A = SICREDI com Registro; C1 = SICREDI sem Registro Impressão Completa pelo Sicredi; " +
                                        "C2 = SICREDI sem Registro Pedido de bloquetos pré-impressos", MessageBoxButtons.OK);
                    return false;
                }
                

            }

            //Santander
            if (CodigoBanco == radioButtonSantander.Tag.ToInt())
            {
                if (txtCarteira.Text == string.Empty)
                {
                    MessageBoxAkil.Show("Informe a Carteira, " +
                                        " Disponíveis: 101, 102 e 201", "Validação Carteira", MessageBoxButtons.OK);
                    return false;
                }
            }

            //Banco do Brasil
            if (CodigoBanco == radioButtonBancoBrasil.Tag.ToInt())
            {
                if (txtCarteira.Text == string.Empty)
                {
                    MessageBoxAkil.Show("Carteira não informada. Informe uma das carteiras 11, 16, 17, 17-019, 17-027, 18, 18-019, 18-027, 18-035, 18-140, 17-159, 17-140, 17-067 ou 31."
                                        , "Validação Carteira", MessageBoxButtons.OK);
                    return false;
                }
            }

            //Banco Itaú
            if (CodigoBanco == radioButtonItau.Tag.ToInt())
            {
                if (txtCarteira.Text == string.Empty)
                {
                    MessageBoxAkil.Show("Carteira não informada. Informe umas das carteiras 175, 176, 178, 109, 198, 107, 122, 142, 143, 196, 126, 131, 146, 150, 169, 121, 112."
                                        , "Validação Carteira", MessageBoxButtons.OK);
                    return false;
                }
            }

            //Banco Itaú
            if (CodigoBanco == radioButtonBradesco.Tag.ToInt())
            {
                if (txtCarteira.Text == string.Empty)
                {
                    MessageBoxAkil.Show("Carteira não informada. Informe umas das carteiras 02, 03, 06, 09, 16, 19, 25, 26."
                                        , "Validação Carteira", MessageBoxButtons.OK);
                    return false;
                }
            }

            return true;
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
            limpaPerfil();
        }

        private void radioButtonBancoBrasil_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
            limpaPerfil();           
        }

        private void radioButtonBradesco_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
            limpaPerfil();            
        }

        private void radioButtonSudameris_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(false, false,false,false);
            limpaPerfil();            
        }

        private void radioButtonSantander_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
            limpaPerfil();            
        }

        private void radioButtonCaixa_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
            limpaPerfil();           
        }

        private void radioButtonBNB_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
            limpaPerfil();            
        }

        private void radioButtonHsbc_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(false, false, false, false);
            limpaPerfil();           
        }

        private void radioButtonReal_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(false, false, false, false);
            limpaPerfil();            
        }

        private void radioButtonSafra_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(false, false, false, false);
            limpaPerfil();            
        }

        private void radioButtonUnibanco_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(false, false, false, false);
            limpaPerfil();            
        }

        private void radioButtonBanrisul_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
            limpaPerfil();            
        }

        private void radioButtonSicredi_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes();
            limpaPerfil();            
        }

        private void radioButtonSicoob_CheckedChanged(object sender, EventArgs e)
        {
            ObterBanco();
            PreenchaCboAgencias();
            carregaInstrucoesBancos();
            carregaEspecieDocumento();
            HabilitarDesabilitarControlesInstrucoes(true,true,true,false);
            limpaPerfil();            
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

        private void txtNossoNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
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

        private void MenuGrid_Click(object sender, EventArgs e)
        {
            if (gridinstrucoes.CurrentRow == null) return;

            _listaInstrucaoEmEdicao.RemoveAt(gridinstrucoes.CurrentRow.Index);
            PreenchaGrid();
        }

        private void gridinstrucoes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && gridinstrucoes.Rows.Count > 0)
            {
                if (MessageBox.Show("Tem certeza que deseja excluir este item!", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _listaInstrucaoEmEdicao.RemoveAt(gridinstrucoes.CurrentRow.Index);
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

                linhaTempAtual.Add(_listaInstrucaoEmEdicao[indexAtual]);
                _listaInstrucaoEmEdicao.RemoveAt(indexAtual);

                linhaTempAnterior.Add(_listaInstrucaoEmEdicao[indexAtual - 1]);
                _listaInstrucaoEmEdicao.RemoveAt(indexAtual - 1);


                _listaInstrucaoEmEdicao.Insert(indexAtual - 1, linhaTempAtual[0]);
                _listaInstrucaoEmEdicao.Insert(indexAtual, linhaTempAnterior[0]);

                PreenchaGrid(false);

                gridinstrucoes[0, indexAtual - 1].Selected = true;
            }
            catch { }
        }

        private void btnPosterior_Click(object sender, EventArgs e)
        {
            List<InstrucoesBoleto> linhaTempAtual = new List<InstrucoesBoleto>();
            List<InstrucoesBoleto> linhaTempAnterior = new List<InstrucoesBoleto>();

            try
            {
                int indexAtual = gridinstrucoes.SelectedCells[0].OwningRow.Index;

                if (indexAtual == gridinstrucoes.Rows.Count - 1) return;

                linhaTempAtual.Add(_listaInstrucaoEmEdicao[indexAtual]);
                _listaInstrucaoEmEdicao.RemoveAt(indexAtual);

                linhaTempAnterior.Add(_listaInstrucaoEmEdicao[indexAtual]);
                _listaInstrucaoEmEdicao.RemoveAt(indexAtual);

                _listaInstrucaoEmEdicao.Insert(indexAtual, linhaTempAnterior[0]);
                _listaInstrucaoEmEdicao.Insert(indexAtual + 1, linhaTempAtual[0]);

                PreenchaGrid(false);

                gridinstrucoes[0, indexAtual + 1].Selected = true;
            }
            catch { }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void btnAdicionarPerfil_Click(object sender, EventArgs e)
        {
            FormAdicionarPerfilEmissaoBoletos formPerfil = new FormAdicionarPerfilEmissaoBoletos();
            formPerfil.ShowDialog();

            limpaPerfil();

            zerarIdConfiguracoes();

            PreencheCboPerfisEmissaoBoletos();
            
            cboPerfisEmissaoBoleto.EditValue = null;
        }

        private void btnAtualizarPerfil_Click(object sender, EventArgs e)
        {
            FormAdicionarPerfilEmissaoBoletos formPerfil = new FormAdicionarPerfilEmissaoBoletos(cboPerfisEmissaoBoleto.EditValue.ToInt(), cboPerfisEmissaoBoleto.Text);
            formPerfil.ShowDialog();

            PreencheCboPerfisEmissaoBoletos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            if (!valideCamposObrigatorios()) return;

            Action actionSalvar = () =>
            {
                ServicoConfiguracaoBoleto servicoConfiguracao = new ServicoConfiguracaoBoleto();

                ConfiguracaoBoleto configuracao = retornaConfiguracoesEmEdicao();

                if (configuracao.Id == 0)
                {
                    servicoConfiguracao.Cadastre(configuracao);
                }
                else
                {   
                    servicoConfiguracao.Atualize(configuracao);
                }

                if (rdbPerfilPadraoSim.Checked)
                {
                    ServicoPerfilEmissaoBoleto servicoPerfilEmissao = new ServicoPerfilEmissaoBoleto();

                    PerfilEmissaoBoleto perfil = new PerfilEmissaoBoleto();

                    if (_listaDePerfis.Count > 0)
                    {
                        for (int i = 0; i < _listaDePerfis.Count; i++)
                        {
                            if (_listaDePerfis[i].Id == cboPerfisEmissaoBoleto.EditValue.ToInt())
                            {
                                perfil.Id = _listaDePerfis[i].Id;
                                perfil.Descricao = _listaDePerfis[i].Descricao;
                                perfil.EhPerfilPadrao = rdbPerfilPadraoSim.Checked ? true : false;
                                servicoPerfilEmissao.Atualize(perfil);
                            }
                            else
                            {
                                perfil.Id = _listaDePerfis[i].Id;
                                perfil.Descricao = _listaDePerfis[i].Descricao;
                                perfil.EhPerfilPadrao = false;
                                servicoPerfilEmissao.Atualize(perfil);
                            }
                        }

                    }
                    else if (cboPerfisEmissaoBoleto.EditValue != null)
                    {
                        perfil.Id = cboPerfisEmissaoBoleto.EditValue.ToInt();
                        perfil.Descricao = cboPerfisEmissaoBoleto.Text;
                        perfil.EhPerfilPadrao = rdbPerfilPadraoSim.Checked ? true : false;
                        servicoPerfilEmissao.Atualize(perfil);
                    }
                    PreencheCboPerfisEmissaoBoletos();
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void cboPerfisEmissaoBoleto_EditValueChanged(object sender, EventArgs e)
        {
            carregaConfiguracoesDoPerfil(cboPerfisEmissaoBoleto.EditValue.ToInt());
        }

        private void btnExcluirPerfil_Click(object sender, EventArgs e)
        {

           if(MessageBox.Show("Deseja realmente excluir este perfil?","Excluindo Perfil de Emissão de Boleto", MessageBoxButtons.YesNo)==DialogResult.No)
            {
                return;
            }

            if (_idConfiguracao != 0)
            {
                ServicoConfiguracaoBoleto servicoConfiguracao = new ServicoConfiguracaoBoleto();

                servicoConfiguracao.Exclua(_idConfiguracao);
            }

            if (cboPerfisEmissaoBoleto.EditValue.ToInt() != 0)
            {
                ServicoPerfilEmissaoBoleto servicoPerfil = new ServicoPerfilEmissaoBoleto();

                servicoPerfil.Exclua(cboPerfisEmissaoBoleto.EditValue.ToInt());
            }

            PreencheCboPerfisEmissaoBoletos();
        }

        #endregion

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

        private void btnSelecionarDiretorio_Click(object sender, EventArgs e)
        {
            if (dialogDiretorio.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtDiretorioPadrao.Text = dialogDiretorio.SelectedPath;
            }
        }

        private void txtDiretorioPadrao_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
