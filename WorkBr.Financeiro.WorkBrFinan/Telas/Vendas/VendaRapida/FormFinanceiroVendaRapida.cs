using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Easy.Servico.Financeiro.OperadorasCartaoServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Vendas.VendaRapida
{
    public partial class FormFinanceiroVendaRapida : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<ParcelaPedidoDeVenda> _listaParcelasPedidoDeVenda;
        private List<ParcelaPedidoDeVenda> _listaParcelasPedidoDeVendaClonada;
        private ParcelaPedidoDeVenda _parcelaPedidoDeVendaEmEdicao;
        private int _pessoaId;
        private bool _jahPassou = false;
        private int _parcela=0;
        private int _nParcelas;        
        private CondicaoPagamento _condicao;
        private double _valorOriginal;

        #endregion

        #region " CONSTRUTOR "

        public FormFinanceiroVendaRapida()
        {
            InitializeComponent();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAtualizarParcela_Click(object sender, EventArgs e)
        {
            AtualizeParcela();
        }

        private void gcFinanceiro_DoubleClick(object sender, EventArgs e)
        {
            EditeParcelaFinanceiro();
        }

        private void gcFinanceiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditeParcelaFinanceiro();
            }
        }

        private void btnCancelarParcela_Click(object sender, EventArgs e)
        {
            LimpeCamposParcela();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            _listaParcelasPedidoDeVenda = _listaParcelasPedidoDeVendaClonada;

            this.Close();
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja reiniciar a relação das parcelas?", "Reiniciando...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _listaParcelasPedidoDeVendaClonada.Clear();
                _parcela = 0;

                txtRestante.Text = _valorOriginal.ToString("0.00");
                PreenchaGridParcelasFinanceiro();
            }
        }

        private void cboFormaPagamentoFinanceiro_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCondicaoPagamento();
            CarregaComboOperadorasDebitoCredito();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public List<ParcelaPedidoDeVenda> EditeFinanceiro(List<ParcelaPedidoDeVenda> listaParcelasPedidoDeVenda, int pessoaId)
        {
            _pessoaId = pessoaId;

            PreenchaCboFormaPagamento();

            _listaParcelasPedidoDeVenda = listaParcelasPedidoDeVenda;

            _listaParcelasPedidoDeVendaClonada = _listaParcelasPedidoDeVenda.CloneCompleto();

            PreenchaGridParcelasFinanceiro();

            txtRestante.Text = _listaParcelasPedidoDeVendaClonada.Sum(x => x.Valor).ToString("0.00");
            _valorOriginal = _listaParcelasPedidoDeVendaClonada.Sum(x => x.Valor);
            _nParcelas = _listaParcelasPedidoDeVendaClonada.Count();
            _condicao = _listaParcelasPedidoDeVenda.Count !=0? _listaParcelasPedidoDeVenda[0].CondicaoPagamento:null;

            this.AbrirTelaModal();

            return _listaParcelasPedidoDeVenda;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void EditeParcelaFinanceiro()
        {
            if (_listaParcelasPedidoDeVendaClonada != null && _listaParcelasPedidoDeVendaClonada.Count > 0)
            {
                var parcela = _listaParcelasPedidoDeVendaClonada.FirstOrDefault(item => item.Id == colunaFinanceiroId.View.GetFocusedRowCellValue(colunaFinanceiroId).ToInt());

                PreenchaCamposParcela(parcela);
            }
        }

        private void PreenchaCamposParcela(ParcelaPedidoDeVenda parcelaPedidoDeVenda)
        {
            _parcelaPedidoDeVendaEmEdicao = parcelaPedidoDeVenda;

            if (parcelaPedidoDeVenda != null)
            {
                txtParcelaFinanceiro.Text = parcelaPedidoDeVenda.Parcela;
                cboFormaPagamentoFinanceiro.EditValue = parcelaPedidoDeVenda.FormaPagamento.Id;
                txtDataVencimento.DateTime = parcelaPedidoDeVenda.DataVencimento;
                txtValorFinanceiro.Text = parcelaPedidoDeVenda.Valor.ToString("0.00");
                cboOperadorasCredito.EditValue = parcelaPedidoDeVenda.Operadoras != null? parcelaPedidoDeVenda.Operadoras.Id:0;

                cboFormaPagamentoFinanceiro.Focus();
            }
            else
            {
                txtParcelaFinanceiro.Text = string.Empty;
                cboFormaPagamentoFinanceiro.EditValue = null;
                txtDataVencimento.Text = string.Empty;
                txtValorFinanceiro.Text = string.Empty;
                cboOperadorasCredito.EditValue = null;                
            }
        }

        private void LimpeCamposParcela()
        {
            PreenchaCamposParcela(null);

            cboFormaPagamentoFinanceiro.Focus();
        }

        private void AtualizeParcela()
        {
            if (_parcelaPedidoDeVendaEmEdicao != null)
            {
                _parcelaPedidoDeVendaEmEdicao.FormaPagamento = cboFormaPagamentoFinanceiro != null ? new FormaPagamento { Id = cboFormaPagamentoFinanceiro.EditValue.ToInt(), Descricao = cboFormaPagamentoFinanceiro.Text } : null;
                _parcelaPedidoDeVendaEmEdicao.DataVencimento = txtDataVencimento.Text.ToDate();
                _parcelaPedidoDeVendaEmEdicao.Valor = txtValorFinanceiro.Text.ToDouble();

                _parcelaPedidoDeVendaEmEdicao.Operadoras = cboOperadorasCredito.EditValue.ToInt() != 0? new OperadorasCartao { Id = cboOperadorasCredito.EditValue.ToInt(), Descricao = cboOperadorasCredito.Text}:null;
            }

            if (chkTodasOperadoras.Checked)
            {
                foreach (var itemParcelaOperadora in _listaParcelasPedidoDeVendaClonada)
                {
                    if ((EnumTipoFormaPagamento)itemParcelaOperadora.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAOCREDITO)
                    {
                        AtribuaAOperadora(itemParcelaOperadora);
                    }
                    else if ((EnumTipoFormaPagamento)itemParcelaOperadora.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAODEBITO)
                    {
                        AtribuaAOperadora(itemParcelaOperadora);
                    }
                }

                chkTodasOperadoras.Checked = false;
            }
            
            PreenchaGridParcelasFinanceiro();

            LimpeCamposParcela();
        }

        private void AtribuaAOperadora(ParcelaPedidoDeVenda parcelaOperadora)
        {
            if (cboOperadorasCredito != null)
                if (cboOperadorasCredito.EditValue != null)
                    parcelaOperadora.Operadoras = new OperadorasCartao { Id = cboOperadorasCredito.EditValue.ToInt(), Descricao = cboOperadorasCredito.Text };
        }

        private void InsiraParcela()
        {
            var nParcelasCondicao = new ServicoCondicaoPagamento().Consulte(cboCondicaoPagamento.EditValue.ToInt());

            if (txtRestante.Text.ToDouble() == _valorOriginal)
            {
                _listaParcelasPedidoDeVendaClonada.Clear();
                _nParcelas = nParcelasCondicao.ListaDeParcelas.Count == 0 ? 1 : nParcelasCondicao.ListaDeParcelas.Count ;
                _parcela = 1;
            }
            else
            {   
                var proximaParcela = nParcelasCondicao.ListaDeParcelas.Count == 0 ? 1 : nParcelasCondicao.ListaDeParcelas.Count;
                _nParcelas = _nParcelas + proximaParcela;
            }

            foreach (var itemParcela in nParcelasCondicao.ListaDeParcelas)
            {
                _parcelaPedidoDeVendaEmEdicao = new ParcelaPedidoDeVenda();
                _parcelaPedidoDeVendaEmEdicao.FormaPagamento = cboFormaPagamentoFinanceiro != null ? new FormaPagamento { Id = cboFormaPagamentoFinanceiro.EditValue.ToInt(), Descricao = cboFormaPagamentoFinanceiro.Text } : null;
                _parcelaPedidoDeVendaEmEdicao.DataVencimento = txtDataVencimento.Text.ToDate().AddDays(itemParcela.Dias);
                _parcelaPedidoDeVendaEmEdicao.Valor = Math.Round(txtValorFinanceiro.Text.ToDouble()*(itemParcela.PercentualRateio/100),2);

                _parcelaPedidoDeVendaEmEdicao.CondicaoPagamento = _parcelaPedidoDeVendaEmEdicao.CondicaoPagamento = cboCondicaoPagamento != null ? new CondicaoPagamento { Id = cboCondicaoPagamento.EditValue.ToInt(), Descricao = cboCondicaoPagamento.Text } : null;

                _parcelaPedidoDeVendaEmEdicao.Parcela = _parcela++ + "/" + _nParcelas;

                _parcelaPedidoDeVendaEmEdicao.Operadoras = cboOperadorasCredito.EditValue.ToInt() != 0 ? new OperadorasCartao { Id = cboOperadorasCredito.EditValue.ToInt(), Descricao = cboOperadorasCredito.Text } : null;

                _listaParcelasPedidoDeVendaClonada.Add(_parcelaPedidoDeVendaEmEdicao);
            }

            PreenchaGridParcelasFinanceiro();

            txtRestante.Text = (txtRestante.Text.ToDouble() - txtValorFinanceiro.Text.ToDouble()).ToString("0.00");

            LimpeCamposParcela();
        }

        private bool retornaParcela()
        {
            if (_parcela == 0)
            {
                _parcela = 1;
            }
            else
            {
                if (_parcela <= _nParcelas)
                {
                    _parcela++;
                    return true;
                }
                else
                    return false;
            }
           
            return true;
        }

        private void PreenchaGridParcelasFinanceiro()
        {
            List<ParcelaGrid> listaParcelasGrid = new List<ParcelaGrid>();

            foreach (var parcela in _listaParcelasPedidoDeVendaClonada)
            {
                ParcelaGrid parcelaGrid = new ParcelaGrid();

                parcelaGrid.CondicaoPagamento = parcela.CondicaoPagamento !=null? parcela.CondicaoPagamento.Id + " - " + parcela.CondicaoPagamento.Descricao:null;
                parcelaGrid.DataVencimento = parcela.DataVencimento.ToString("dd/MM/yyyy");
                parcelaGrid.FormaPagamento = parcela.FormaPagamento.Id.ToString() + " - " + parcela.FormaPagamento.Descricao;

                parcelaGrid.Operadora = parcela.Operadoras != null? parcela.Operadoras.Id.ToString() + " - " + parcela.Operadoras.Descricao:null;

                parcelaGrid.Id = parcela.Id;
                parcelaGrid.NumeroDocumento = parcela.NumeroDocumento;
                parcelaGrid.Parcela = parcela.Parcela;
                parcelaGrid.Valor = parcela.Valor.ToString("0.00");

                listaParcelasGrid.Add(parcelaGrid);
            }

            gcFinanceiro.DataSource = listaParcelasGrid;
            gcFinanceiro.RefreshDataSource();
        }

        private void PreenchaCboFormaPagamento()
        {
            List<FormaPagamento> lista = new List<Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio.FormaPagamento>();

            ServicoCrediario servicoAnaliseCredito = new ServicoCrediario();
            var analiseCredito = servicoAnaliseCredito.Consulte(_pessoaId);

            if (analiseCredito != null && analiseCredito.FormaPagamento != null)
            {
                lista.Add(analiseCredito.FormaPagamento);
            }
            else
            {
                ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

                lista = servicoFormaPagamento.ConsulteListaAtivos();
            }

            cboFormaPagamentoFinanceiro.Properties.DisplayMember = "Descricao";
            cboFormaPagamentoFinanceiro.Properties.ValueMember = "Id";
            cboFormaPagamentoFinanceiro.Properties.DataSource = lista;
        }

        private void CarregaComboOperadorasDebitoCredito()
        {
            ServicoOperadorasCartao servicoOperadorasCartao = new ServicoOperadorasCartao();

            var operadoras = servicoOperadorasCartao.ConsulteLista();

            List<ObjetoParaComboBox> listaDebito = new List<ObjetoParaComboBox>();
            List<ObjetoParaComboBox> listaCredito = new List<ObjetoParaComboBox>();

            foreach (var item in operadoras)
            {
                ObjetoParaComboBox objeto = new ObjetoParaComboBox();

                if (!item.PermiteParcelamento)
                {
                    objeto.Descricao = item.Descricao;
                    objeto.Valor = item.Id;

                    listaDebito.Add(objeto);
                }
                else
                {
                    objeto.Descricao = item.Descricao;
                    objeto.Valor = item.Id;

                    listaCredito.Add(objeto);
                }
            }

            if (cboFormaPagamentoFinanceiro.EditValue == null) return;

            if ((EnumTipoFormaPagamento)cboFormaPagamentoFinanceiro.EditValue == EnumTipoFormaPagamento.CARTAODEBITO)
            {
                cboOperadorasCredito.Properties.DisplayMember = "Descricao";
                cboOperadorasCredito.Properties.ValueMember = "Valor";
                cboOperadorasCredito.Properties.DataSource = listaDebito;
            }
            else if((EnumTipoFormaPagamento)cboFormaPagamentoFinanceiro.EditValue == EnumTipoFormaPagamento.CARTAOCREDITO)
            {
                cboOperadorasCredito.Properties.DisplayMember = "Descricao";
                cboOperadorasCredito.Properties.ValueMember = "Valor";
                cboOperadorasCredito.Properties.DataSource = listaCredito;
            }
            else
            {
                cboOperadorasCredito.Properties.DisplayMember = "Descricao";
                cboOperadorasCredito.Properties.ValueMember = "Id";
                cboOperadorasCredito.Properties.DataSource = null;
            }
        }

        private void PreenchaCboCondicaoPagamento()
        {
            List<CondicaoPagamento> listaCondicoes = new List<CondicaoPagamento>();

            ServicoCrediario servicoAnaliseCredito = new ServicoCrediario();
            var analiseCredito = servicoAnaliseCredito.Consulte(_pessoaId);

            if (analiseCredito != null && analiseCredito.CondicaoPagamento != null)
            {
                listaCondicoes.Add(analiseCredito.CondicaoPagamento);
            }
            else
            {
                ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
                var formaPagamento = servicoFormaPagamento.Consulte(cboFormaPagamentoFinanceiro.EditValue.ToInt());

                if (formaPagamento != null &&
                    formaPagamento.ListaCondicoesPagamento != null &&
                    formaPagamento.ListaCondicoesPagamento.Count > 0)
                {
                    foreach (var item in formaPagamento.ListaCondicoesPagamento)
                    {
                        if (item.CondicaoPagamento.Status == "A")
                        {
                            listaCondicoes.Add(item.CondicaoPagamento);
                        }
                    }
                }
            }

            listaCondicoes.Insert(0, null);

            cboCondicaoPagamento.Properties.DisplayMember = "Descricao";
            cboCondicaoPagamento.Properties.ValueMember = "Id";
            cboCondicaoPagamento.Properties.DataSource = listaCondicoes;

            if (string.IsNullOrEmpty(cboCondicaoPagamento.Text))
            {
                cboCondicaoPagamento.EditValue = null;
            }
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ParcelaGrid
        {
            public int Id { get; set; }

            public string Parcela { get; set; }

            public string NumeroDocumento { get; set; }

            public string CondicaoPagamento { get; set; }

            public string FormaPagamento { get; set; }

            public string DataVencimento { get; set; }

            public string Valor { get; set; }

            public string Operadora { get; set; }
        }

        #endregion
        
        private void btnAdicionarParcela_Click(object sender, EventArgs e)
        {
            if (txtRestante.Text.ToDouble() == 0)
            {
                return;
            }

            if(txtValorFinanceiro.Text == "" || cboFormaPagamentoFinanceiro.EditValue.ToInt() == 0 || cboCondicaoPagamento.EditValue.ToInt() == 0
                || txtDataVencimento.Text == "")
            {
                MessageBox.Show("Informe Valor, Forma de Pagamento e Vencimento para a Parcela que deseja relacionar.");
                return;
            }
                

            if((EnumTipoFormaPagamento)cboFormaPagamentoFinanceiro.EditValue == EnumTipoFormaPagamento.CARTAOCREDITO ||
                (EnumTipoFormaPagamento)cboFormaPagamentoFinanceiro.EditValue == EnumTipoFormaPagamento.CARTAODEBITO)
                {

                if (cboFormaPagamentoFinanceiro.Text == "")
                {
                    MessageBox.Show("Se é Crédito ou Débito. Você precisa informar a operadora!");
                    return;
                }                   
            }

            if (!_jahPassou)
            {
                _listaParcelasPedidoDeVendaClonada.Clear();
                _jahPassou = true;
            }

            InsiraParcela();
        }        
    }
}
