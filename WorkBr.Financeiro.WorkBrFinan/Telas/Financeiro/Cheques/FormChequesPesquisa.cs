using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.ChequeServ;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Report.RelatoriosDevExpress.Financeiro;
using Programax.Easy.View.ClassesAuxiliares;
using DevExpress.XtraGrid.Views.Grid;

namespace Programax.Easy.View.Telas.Financeiro.Cheques
{
    public partial class FormChequesPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Cheque> _listaCheques;
        private string _colunaOrdenacao;
        private string _tipoOrdenacao;        

        #endregion

        #region " CONSTRUTOR "

        public FormChequesPesquisa()
        {
            InitializeComponent();

            PreenchaCboBancos();
            PreenchaCboDataFiltrar();

            this.ActiveControl = txtIdPessoa;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            FormCadastroCheque formCadastroCheque = new FormCadastroCheque(0,null,0,0,null,false,true);
            
            formCadastroCheque.Show();

            formCadastroCheque.Height = 400;
        }

        private void btnEditarCheque_Click(object sender, EventArgs e)
        {
            EditeCheque();
        }

        private void btnPesquisaContaBancaria_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoaClienteAtiva();

            if (pessoa != null)
            {
                PreenchaPessoa(pessoa);
            }
        }

        private void txtIdPessoa_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdPessoa.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var vendedor = servicoPessoa.ConsulteClienteAtivo(txtIdPessoa.Text.ToInt());

                PreenchaPessoa(vendedor, true);
            }
            else
            {
                PreenchaPessoa(null);
            }
        }

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void cboDataFiltrar_EditValueChanged(object sender, EventArgs e)
        {
            if (cboDataFiltrar.EditValue != null)
            {
                txtDataInicialPeriodo.Enabled = true;
                txtDataFinalPeriodo.Enabled = true;
            }
        }

        private void chkInativo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void txtIdPessoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirCheques();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void Pesquise()
        {
            this.Cursor = Cursors.WaitCursor;
            Pessoa pessoa = !string.IsNullOrEmpty(txtIdPessoa.Text) ? new Pessoa { Id = txtIdPessoa.Text.ToInt() } : null;
            EnumDataFiltrarCheque? dataFiltrarCheque = (EnumDataFiltrarCheque?)cboDataFiltrar.EditValue;
            DateTime? dataInicialPeriodo = txtDataInicialPeriodo.Text.ToDateNullabel();
            DateTime? dataFinalPeriodo = txtDataFinalPeriodo.Text.ToDateNullabel();

            Banco banco = cboBancos.EditValue != null ? new Banco { Id = cboBancos.EditValue.ToInt() } : null;

            string numeroCheque = txtNumeroCheque.Text;

            bool statusAbertoDepositado = chkAbertoDepositado.Checked;
            bool statusRecebido = chkRecebido.Checked;
            bool statusDevolvidoPrimeira = chkDevolvidoPrimeira.Checked;
            bool statusDevolvidoSegunda = chkDevolvidoSegunda.Checked;
            bool statusReapresentado = chkReapresentado.Checked;
            bool statusCustodiadoFactoring = chkCustodiadoFactoring.Checked;
            bool statusInativo = chkInativo.Checked;

            if (!statusAbertoDepositado &&
                !statusRecebido &&
                !statusDevolvidoPrimeira &&
                !statusDevolvidoSegunda &&
                !statusReapresentado &&
                !statusCustodiadoFactoring &&
                !statusInativo)
            {
                MessageBox.Show("Infome uma situação para a pesquisa!");
                this.Cursor = Cursors.Default;
                return;
            }

            ServicoCheque servicoCheque = new ServicoCheque();

            _listaCheques = servicoCheque.ConsulteLista( pessoa,
                                                                              dataFiltrarCheque,
                                                                              dataInicialPeriodo,
                                                                              dataFinalPeriodo,
                                                                              banco,
                                                                              numeroCheque,
                                                                              statusAbertoDepositado,
                                                                              statusRecebido,
                                                                              statusDevolvidoPrimeira,
                                                                              statusDevolvidoSegunda,
                                                                              statusReapresentado,
                                                                              statusCustodiadoFactoring,
                                                                              statusInativo );

            PreenchaGrid();
            PreenchaTotais();
            this.Cursor = Cursors.Default;
        }

        private void EditeCheque()
        {
            var chequeSelecionado = RetorneChequeSelecionado();

            if (chequeSelecionado != null)
            {
                FormCadastroCheque formCadastroCheque = new FormCadastroCheque();

                formCadastroCheque.EditeCheque(chequeSelecionado);
            }
        }

        private void PreenchaGrid()
        {
            List<ChequeGrid> listaDeChequesGrid = new List<ChequeGrid>();

            foreach (var cheque in _listaCheques)
            {
                ChequeGrid chequeGrid = new ChequeGrid();

                chequeGrid.Id = cheque.Id;
                chequeGrid.Cliente = cheque.Pessoa != null ? cheque.Pessoa.Id + " - " + cheque.Pessoa.DadosGerais.Razao : string.Empty;
                chequeGrid.Agencia = cheque.Agencia;
                chequeGrid.Banco = cheque.Banco != null ? cheque.Banco.Descricao : string.Empty;
                chequeGrid.Conta = cheque.Conta;

                chequeGrid.DataEmissao = cheque.DataEmissao != null ? cheque.DataEmissao: null;
                chequeGrid.DataRecebimento = cheque.DataRecebimento != null ? cheque.DataRecebimento : null;
                chequeGrid.DataVencimento = cheque.DataVencimento != null ? cheque.DataVencimento : null;
                chequeGrid.NumeroCheque = cheque.NumeroCheque;
                chequeGrid.Status = cheque.StatusCheque.Value.Descricao();
                chequeGrid.Observacoes = cheque.Observacoes;
                                
                chequeGrid.Valor = cheque.ValorCheque;
                chequeGrid.DataVencimento = cheque.DataVencimento != null ? cheque.DataVencimento : null;

                listaDeChequesGrid.Add(chequeGrid);
            }

            gcCheques.DataSource = listaDeChequesGrid;
            gcCheques.RefreshDataSource();
        }

        private void PreenchaTotais()
        {
            double totalAbertoDepositado = 0;
            int qtdAbertoDepositado = 0;

            double totalRecebido = 0;
            int qtdRecebido = 0;

            double totalDevolvidoPrimeira = 0;
            int qtdDevolvidoPrimeira = 0;

            double totalDevolvidoSegunda = 0;
            int qtdDevolvidoSegunda = 0;

            double totalReapresentado = 0;
            int qtdReapresentado = 0;

            double totalCustodiadoFactoring = 0;
            int qtdCustodiadoFactoring = 0;

            double totalInativo = 0;
            int qtdInativo = 0;

            double total = 0;
            int qtdTotal = 0;

            double totalAVencer = 0;
            int qtdAVencer = 0;

            double totalVencido = 0;
            int qtdVencido = 0;

            foreach (var cheque in _listaCheques)
            {
                if (cheque.StatusCheque == EnumStatusCheque.ABERTODEPOSITADO)
                {
                    totalAbertoDepositado += cheque.ValorCheque;
                    qtdAbertoDepositado++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.CUSTODIADOFACTORING)
                {
                    totalCustodiadoFactoring += cheque.ValorCheque;
                    qtdCustodiadoFactoring++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.DEVOLVIDO1)
                {
                    totalDevolvidoPrimeira += cheque.ValorCheque;
                    qtdDevolvidoPrimeira++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.DEVOLVIDO2)
                {
                    totalDevolvidoSegunda += cheque.ValorCheque;
                    qtdDevolvidoSegunda++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.INATIVO)
                {
                    totalInativo += cheque.ValorCheque;
                    qtdInativo++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.REAPRESENTADO)
                {
                    totalReapresentado += cheque.ValorCheque;
                    qtdReapresentado++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.RECEBIDO)
                {
                    totalRecebido += cheque.ValorCheque;
                    qtdRecebido++;
                }

                total += cheque.ValorCheque;
                qtdTotal++;

                if (cheque.StatusCheque != EnumStatusCheque.RECEBIDO && cheque.StatusCheque != EnumStatusCheque.INATIVO)
                {
                    if (cheque.DataVencimento >= DateTime.Now.Date.Date)
                    {
                        totalAVencer += cheque.ValorCheque;
                        qtdAVencer++;
                    }
                    else
                    {
                        totalVencido += cheque.ValorCheque;
                        qtdVencido++;
                    }
                }
            }

            lblTotalAbertoDepositado.Text = totalAbertoDepositado.ToString("#,###,##0.00") + "(" + qtdAbertoDepositado + ")";
            lblTotalRecebido.Text = totalRecebido.ToString("#,###,##0.00") + "(" + qtdRecebido + ")";
            lblTotalDevolvidaPrimeira.Text = totalDevolvidoPrimeira.ToString("#,###,##0.00") + "(" + qtdDevolvidoPrimeira + ")";
            lblTotalDevolvidaSegunda.Text = totalDevolvidoSegunda.ToString("#,###,##0.00") + "(" + qtdDevolvidoSegunda + ")";
            lblTotalReapresentado.Text = totalReapresentado.ToString("#,###,##0.00") + "(" + qtdReapresentado + ")";
            lblTotalCustodiadoFactoring.Text = totalCustodiadoFactoring.ToString("#,###,##0.00") + "(" + qtdCustodiadoFactoring + ")";
            lblTotalInativo.Text = totalInativo.ToString("#,###,##0.00") + "(" + qtdInativo + ")";
            lblTotal.Text = total.ToString("#,###,##0.00") + "(" + qtdTotal + ")";

            lblTotalAVencer.Text = totalAVencer.ToString("#,###,##0.00") + "(" + qtdAVencer + ")";
            lblTotalVencido.Text = totalVencido.ToString("#,###,##0.00") + "(" + qtdVencido + ")";
        }

        private Cheque RetorneChequeSelecionado()
        {
            Cheque chequeSelecionado = null;

            if (_listaCheques != null && _listaCheques.Count > 0)
            {
                ServicoCheque servicoCheque = new ServicoCheque();

                chequeSelecionado = servicoCheque.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                chequeSelecionado.Pessoa.CarregueLazyLoad();
                chequeSelecionado.Banco.CarregueLazyLoad();
            }

            return chequeSelecionado;
        }

        private void PreenchaPessoa(Pessoa pessoa, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (pessoa != null)
            {
                txtIdPessoa.Text = pessoa.Id.ToString();
                txtNomePessoa.Text = pessoa.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Parceiro nao encontrado!", "Parceiro não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdPessoa.Focus();
                }

                txtIdPessoa.Text = string.Empty;
                txtNomePessoa.Text = string.Empty;
            }
        }

        private void PreenchaCboBancos()
        {
            ServicoBanco servicoBanco = new ServicoBanco();

            var listaBancos = servicoBanco.ConsulteLista();

            listaBancos.Insert(0, null);

            cboBancos.Properties.DataSource = listaBancos;
            cboBancos.Properties.DisplayMember = "Descricao";
            cboBancos.Properties.ValueMember = "Id";
        }

        private void gcCheques_DoubleClick(object sender, EventArgs e)
        {
            EditeCheque();
        }

        private void gcCheques_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditeCheque();
            }
        }

        private void PreenchaCboDataFiltrar()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumDataFiltrarCheque>();

            lista.Insert(0, null);

            cboDataFiltrar.Properties.DataSource = lista;
            cboDataFiltrar.Properties.ValueMember = "Valor";
            cboDataFiltrar.Properties.DisplayMember = "Descricao";
        }
        
        private void ImprimirCheques()
        {
            this.Cursor = Cursors.WaitCursor;

            if (_listaCheques != null && _listaCheques.Count > 0)
            {
                int contador = 0;                
                while (contador <= 10)
                {
                    int index = ((GridView)gcCheques.MainView).Columns[contador].SortIndex;
                    if (index == 0)
                    {
                        _tipoOrdenacao = ((GridView)gcCheques.MainView).Columns[contador].SortOrder.ToString();
                        _colunaOrdenacao = ((GridView)gcCheques.MainView).Columns[contador].FieldName;
                    }
                    contador += 1;
                }

                OrdeneListaCheques();
                    
                RelatorioDeCheques relatorioDeCheques = new RelatorioDeCheques(_listaCheques);

                TratamentosDeTela.ExibirRelatorio(relatorioDeCheques);
            }

            this.Cursor = Cursors.Default;
        }

        private void OrdeneListaCheques()
        {
            switch (_colunaOrdenacao)
            {
                case "Id":
                    break;
                case "Banco":
                    if (_tipoOrdenacao == "Descending")
                    {
                        _listaCheques = _listaCheques.OrderByDescending(x => x.Banco.Descricao).ToList();
                    }
                    else
                        _listaCheques = _listaCheques.OrderBy(x => x.Banco.Descricao).ToList();
                    break;

                case "Agencia":
                    if (_tipoOrdenacao == "Descending")
                    {
                        _listaCheques = _listaCheques.OrderByDescending(x => x.Agencia).ToList();
                    }
                    else
                        _listaCheques = _listaCheques.OrderBy(x => x.Agencia).ToList();
                    break;

                case "Conta":
                    if (_tipoOrdenacao == "Descending")
                    {
                        _listaCheques = _listaCheques.OrderByDescending(x => x.Conta).ToList();
                    }
                    else
                        _listaCheques = _listaCheques.OrderBy(x => x.Conta).ToList();
                    break;

                case "NumeroCheque":
                    if (_tipoOrdenacao == "Descending")
                    {
                        _listaCheques = _listaCheques.OrderByDescending(x => x.NumeroCheque).ToList();
                    }
                    else
                        _listaCheques = _listaCheques.OrderBy(x => x.NumeroCheque).ToList();
                    break;

                case "Valor":
                    if (_tipoOrdenacao == "Descending")
                    {
                        _listaCheques = _listaCheques.OrderByDescending(x => x.ValorCheque).ToList();
                    }
                    else
                        _listaCheques = _listaCheques.OrderBy(x => x.ValorCheque).ToList();
                    break;

                case "Cliente":

                    if (_tipoOrdenacao == "Descending")
                    {
                        _listaCheques = _listaCheques.OrderByDescending(x => (x.Pessoa.DadosPessoais.Id) + (x.Pessoa.DadosGerais.Razao)).ToList();
                    }
                    else
                        _listaCheques = _listaCheques.OrderBy(x => (x.Pessoa.DadosPessoais.Id) + (x.Pessoa.DadosGerais.Razao)).ToList();
                    break;

                case "DataEmissao":

                    if (_tipoOrdenacao == "Descending")
                    {
                        _listaCheques = _listaCheques.OrderByDescending(x => x.DataEmissao).ToList();
                    }
                    else
                        _listaCheques = _listaCheques.OrderBy(x => x.DataEmissao).ToList();
                    break;

                case "DataVencimento":

                    if (_tipoOrdenacao == "Descending")
                    {
                        _listaCheques = _listaCheques.OrderByDescending(x => x.DataVencimento).ToList();
                    }
                    else
                        _listaCheques = _listaCheques.OrderBy(x => x.DataVencimento).ToList();
                    break;

                case "DataRecebimento":

                    if (_tipoOrdenacao == "Descending")
                    {
                        _listaCheques = _listaCheques.OrderByDescending(x => x.DataRecebimento).ToList();
                    }
                    else
                        _listaCheques = _listaCheques.OrderBy(x => x.DataRecebimento).ToList();
                    break;

                case "Status":

                    if (_tipoOrdenacao == "Descending")
                    {
                        _listaCheques = _listaCheques.OrderByDescending(x => x.StatusCheque).ToList();
                    }
                    else
                        _listaCheques = _listaCheques.OrderBy(x => x.StatusCheque).ToList();
                    break;
            }           
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ChequeGrid
        {
            public int Id { get; set; }

            public string Banco { get; set; }

            public string Agencia { get; set; }

            public string Conta { get; set; }

            public string NumeroCheque { get; set; }

            public double Valor { get; set; }

            public string Cliente { get; set; }

            public DateTime? DataEmissao { get; set; }

            public DateTime? DataVencimento { get; set; }

            public DateTime? DataRecebimento { get; set; }

            public string Status { get; set; }

            public string Observacoes { get; set; }
        }

        #endregion
               
    }
}
