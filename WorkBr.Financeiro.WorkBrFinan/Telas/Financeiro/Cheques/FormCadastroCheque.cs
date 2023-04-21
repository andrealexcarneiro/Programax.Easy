using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ChequeServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.View.Telas.Financeiro.ContasBancarias;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Financeiro.VincularChequePedidosObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Financeiro.Cheques
{
    public partial class FormCadastroCheque : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idChequeEdicao;
        private Cheque _itemChequeEdicao;
        private int _numeroPedido;
        private double _valorTotalCheques;
        private bool _CadastrandoCheque = false;
        private ContaPagarReceber _ContasPagarReceber = null;
        private bool _ehContaAPagar;
        private bool _ehNovoChequePoderaVincular;
        private int IdCliente = 0;
        private string strCliente;

        List<Cheque> _listaCheques = new List<Cheque>();

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroCheque(int NumeroPedido=0, List<ParcelaRecebimento> parcelas = null, double valorTotalCheques=0,  int clienteId = 0, string NomeCliente = null,bool ehContaAPagar=false, bool ehNovoChequePoderaVincular=false)
        {
            InitializeComponent();

            PreenchaCboBancos();
            PreenchaCboStatus();
            IdCliente = clienteId;
            strCliente = NomeCliente;

            _ehNovoChequePoderaVincular = ehNovoChequePoderaVincular;
            _numeroPedido = NumeroPedido;
            _valorTotalCheques = valorTotalCheques;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            if (parcelas != null)
            {
                _listaCheques = new ServicoCheque().ConsulteListaChequePorPedido(_numeroPedido);
                
                if (_listaCheques.Count == 0)
                {
                    _CadastrandoCheque = true;
                    ConverteParcelasEmCheques(parcelas);
                }

                PreenchaGrid();
                AlteraBotaoAtualizarParaAtualizar();
            }

            if(ehContaAPagar)
            {
                _ehContaAPagar = ehContaAPagar;
                lblCodigo.Text = "Cod Fornecedor";
                lblNome.Text = "Nome do Fornecedor";
                btnPesquiseContaBancaria.Enabled = true;
            }

            this.ActiveControl = txtIdPessoa;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salve();
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            Pessoa pessoa = new Pessoa();

            if (!_ehContaAPagar)
                pessoa = formPessoaPesquisa.PesquisePessoaClienteAtiva();
            else
                pessoa = formPessoaPesquisa.PesquisePessoaFornecedoraAtiva();

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

                Pessoa vendedor = new Pessoa();
                if (!_ehContaAPagar)
                    vendedor = servicoPessoa.ConsulteClienteAtivo(txtIdPessoa.Text.ToInt());
                else
                    vendedor = servicoPessoa.ConsulteFornecedorAtivo(txtIdPessoa.Text.ToInt());

                PreenchaPessoa(vendedor, true);

                if(_ehNovoChequePoderaVincular)
                {
                    lstPedidos.Items.Clear();
                    PreenchalstPedidos();
                }
            }
            else
            {
                PreenchaPessoa(null);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void rdbCpf_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCpf.Checked)
            {
                txtCpfCnpj.Properties.Mask.EditMask = "000.000.000-00";
            }
        }

        private void rdbCnpj_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCnpj.Checked)
            {
                txtCpfCnpj.Properties.Mask.EditMask = "00.000.000/0000-00";
            }
        }

        private void txtCpfCnpj_Validating(object sender, CancelEventArgs e)
        {
            if (!txtCpfCnpj.Text.EstahVazio())
            {
                if (rdbCpf.Checked)
                {
                    if (!ValidacoesGerais.CpfEstahValido(txtCpfCnpj.Text))
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    if (!ValidacoesGerais.CnpjEstahValido(txtCpfCnpj.Text))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void txtCpfCnpj_InvalidValue(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;

            if (rdbCpf.Checked)
            {
                e.ErrorText = "CPF inválido.";
            }
            else
            {
                e.ErrorText = "CNPJ inválido.";
            }

            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.DisplayError;
        }

        private void txtCpfCnpj_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCpfCnpj.Text))
            {
                txtCpfCnpj.SelectionStart = 0;
            }
        }

        private void cboStatus_EditValueChanged(object sender, EventArgs e)
        {
            if ((EnumStatusCheque?)cboStatus.EditValue == EnumStatusCheque.RECEBIDO)
            {
                txtDataRecebimento.Enabled = true;
            }
            else
            {
                txtDataRecebimento.Enabled = false;
                txtDataRecebimento.Text = string.Empty;
            }
        }

        private void rdbCpf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCpfCnpj.Focus();
            }
        }

        private void rdbCnpj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCpfCnpj.Focus();
            }
        }

        private void gcCheques_DoubleClick(object sender, EventArgs e)
        {
            var itemDaLista = _listaCheques.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

            EditeCheque(itemDaLista);            
        }

        private void btnInserirAtualizarCheque_Click(object sender, EventArgs e)
        {
            InsiraOuAtualizeChequeNaLista();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public void EditeCheque(Cheque cheque, ContaPagarReceber contapagarReceber = null)
        {
            btnRecarregar.Visible = true;
            _ContasPagarReceber = contapagarReceber;
            PreenchaCamposCheque(cheque);

            //Este código foi retirado porque o usuário poderá errar o Status e não conseguirá salvá-lo novamente
            //if (cheque.StatusCheque == EnumStatusCheque.INATIVO || cheque.StatusCheque == EnumStatusCheque.RECEBIDO)
            //{
            //btnSalvar.Visible = false;
            //}

            AlteraBotaoAtualizarParaAtualizar();

            if (_listaCheques.Count == 0)
            {
                this.Height = 400;
            }

                this.Show();
        }

        public void AlteraBotaoAtualizarParaAtualizar()
        {
            btnInserirAtualizarCheque.Image = Properties.Resources.icones2_07;            
            btnInserirAtualizarCheque.Image = Properties.Resources.icon_atualizar;
        }

        public void CadastreNovoCheque(Pessoa cliente, ContaPagarReceber contaPagarReceber=null)
        {
            if (contaPagarReceber != null)
                contaPagarReceber = new ServicoContasPagarReceber().Consulte(contaPagarReceber.Id);

            PreenchaPessoa(cliente);

            if (contaPagarReceber != null)
            {
                CarregueContaPagarReceberEmCheque(contaPagarReceber);
                _ContasPagarReceber = contaPagarReceber;
            }

            if (_listaCheques.Count !=0)
            {   
                this.ShowDialog();
            }
            else
                this.Height = 400;
        }

        private void CarregueContaPagarReceberEmCheque(ContaPagarReceber contaPagarReceber)
        {      
            Cheque cheque = new Cheque();

            cheque.Pessoa = new Pessoa();
            cheque.ValorCheque = contaPagarReceber.ValorParcela;
            cheque.DataVencimento = contaPagarReceber.DataVencimento;
            cheque.Pessoa.Id = contaPagarReceber.Pessoa.Id;
            cheque.Pessoa.DadosGerais.Razao = contaPagarReceber.Pessoa.DadosGerais.Razao;
            cheque.DataEmissao = contaPagarReceber.DataEmissao;
            cheque.NumeroDocumento = contaPagarReceber.NumeroDocumento;
            cheque.DataCadastro = DateTime.Now;

            EditeCheque(cheque);
           
        }

        private void AtualizeChequeEmContaPagarReceber(Cheque cheque)
        {

            _ContasPagarReceber.ValorParcela = cheque.ValorCheque;
            _ContasPagarReceber.DataVencimento = cheque.DataVencimento;
            _ContasPagarReceber.DataEmissao = cheque.DataEmissao.GetValueOrDefault();        
            _ContasPagarReceber.ChequeId = cheque.Id;
            
            ServicoContasPagarReceber servicoContaPagarReceber = new ServicoContasPagarReceber();
            
            servicoContaPagarReceber.Atualize(_ContasPagarReceber);
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

        private void PreenchaCboStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusCheque>();

            lista.Insert(0, null);

            cboStatus.Properties.DataSource = lista;
            cboStatus.Properties.DisplayMember = "Descricao";
            cboStatus.Properties.ValueMember = "Valor";
        }

        private void Salve()
        {
            Action actionSalvar = () =>
            {
                if (_listaCheques.Count == 0)
                {
                    Cheque cheque = RetorneChequeEmEdicao();

                    ServicoCheque servicoCheque = new ServicoCheque();

                    if (cheque.Id == 0)
                    {
                        servicoCheque.Cadastre(cheque);
                    }
                    else
                    {
                        servicoCheque.Atualize(cheque);
                    }

                    //if (_ContasPagarReceber !=null)
                    //    AtualizeChequeEmContaPagarReceber(cheque);
                }
                else
                {
                    var calculoTotalCheques = _listaCheques.Sum(x => x.ValorCheque);

                    if(_valorTotalCheques != calculoTotalCheques)
                    {
                        MessageBox.Show("O valor total dos cheques está diferente do informado para o recebimento.","Cadastro de Cheques");
                        return;
                    }

                    foreach (var item in _listaCheques)
                    {
                        if (_CadastrandoCheque)
                            item.Id = 0;

                        ServicoCheque servicoCheque = new ServicoCheque();

                        if (item.Id == 0)
                            servicoCheque.Cadastre(item);
                        else
                            servicoCheque.Atualize(item);
                    }
                }
               
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, this, fecharFormAoConcluirOperacao: true);
        }

        private Cheque RetorneChequeEmEdicao()
        {
            Cheque cheque = new Cheque();

            cheque.Banco = cboBancos.EditValue != null ? new Banco { Id = cboBancos.EditValue.ToInt() } : null;
            cheque.CpfCnpj = txtCpfCnpj.Text;
            cheque.DataCadastro = txtDataCadastro.Text.ToDate();
            cheque.DataEmissao = txtDataEmissao.Text.ToDateNullabel();
            cheque.DataVencimento = txtDataVencimento.Text.ToDateNullabel();
            cheque.Id = _idChequeEdicao;
            cheque.NomeCheque = txtNomeEmitente.Text;
            cheque.Observacoes = txtObs.Text;
            cheque.Pessoa = !string.IsNullOrEmpty(txtIdPessoa.Text) ? new Pessoa { Id = txtIdPessoa.Text.ToInt() } : null;
            cheque.Pessoa.DadosGerais.Razao = txtNomePessoa.Text;
            cheque.StatusCheque = (EnumStatusCheque?)cboStatus.EditValue;
            cheque.ValorCheque = txtValor.Text.ToDouble();

            cheque.EhCnpj = rdbCnpj.Checked;
            cheque.DataRecebimento = txtDataRecebimento.Text.ToDateNullabel();
            cheque.Agencia = txtAgencia.Text;
            cheque.Conta = txtConta.Text;
            cheque.Digito = txtDigito.Text;
            cheque.Serie = txtSerieCheque.Text;
            cheque.NumeroCheque = txtNumeroCheque.Text;
            cheque.NumeroPedidoVenda = txtNumeroPedidoVenda.Text.ToInt();
            cheque.NumeroDocumento = txtNumeroDocumento.Text;

            foreach (var item in lstPedidos.SelectedItems)
            {
                VincularChequePedidos itemVincularCheque = new VincularChequePedidos();

                itemVincularCheque.Cheque = cheque;
                itemVincularCheque.Pessoa = cheque.Pessoa;
                itemVincularCheque.NumeroPedidos = item.ToInt();

                cheque.ListaVinculosDePedidos.Add(itemVincularCheque);                
            }

            return cheque;
        }

        protected virtual void PreenchaPessoa(Pessoa pessoa, bool exibirMensagemDeNaoEncontrado = false)
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

        private void PreenchaCamposCheque(Cheque cheque)
        {
            _idChequeEdicao = cheque.Id;
            _itemChequeEdicao = cheque;

            PreenchaPessoa(cheque.Pessoa);

            txtDataCadastro.Text = cheque.DataCadastro.ToString("dd/MM/yyyy");

            txtNomeEmitente.Text = cheque.NomeCheque;
            rdbCnpj.Checked = cheque.EhCnpj;
            txtCpfCnpj.Text = cheque.CpfCnpj;

            if (cheque.DataEmissao != null)
            {
                txtDataEmissao.DateTime = cheque.DataEmissao.GetValueOrDefault();
            }
            else
            {
                txtDataEmissao.Text = string.Empty;
            }

            if (cheque.DataVencimento != null)
            {
                txtDataVencimento.DateTime = cheque.DataVencimento.GetValueOrDefault();
            }
            else
            {
                txtDataVencimento.Text = string.Empty;
            }

            if (cheque.DataRecebimento != null)
            {
                txtDataRecebimento.DateTime = cheque.DataRecebimento.GetValueOrDefault();
            }
            else
            {
                txtDataRecebimento.Text = string.Empty;
            }

            cboBancos.EditValue = cheque.Banco != null ? (int?)cheque.Banco.Id : null;

            txtAgencia.Text = cheque.Agencia;
            txtConta.Text = cheque.Conta;
            txtDigito.Text = cheque.Digito;
            txtSerieCheque.Text = cheque.Serie;
            txtNumeroCheque.Text = cheque.NumeroCheque;
            txtNumeroPedidoVenda.Text = cheque.NumeroPedidoVenda.ToString();
            txtNumeroDocumento.Text = cheque.NumeroDocumento;

            txtValor.Text = cheque.ValorCheque.ToString("0.00");

            cboStatus.EditValue = cheque.StatusCheque;

            txtObs.Text = cheque.Observacoes;

            cheque = new ServicoCheque().ConsulteJoinComItens(cheque.Id);

            if (cheque != null)
            {
                foreach (var item in cheque.ListaVinculosDePedidos)
                {
                    lstPedidos.Items.Add(item.NumeroPedidos);                    
                }
                lstPedidos.SelectAll();
            }
        }

        private void ConverteParcelasEmCheques(List<ParcelaRecebimento> parcelas)
        {   
            foreach (var item in parcelas)
            {
                if ((EnumTipoFormaPagamento)item.FormaPagamentoId == EnumTipoFormaPagamento.CHEQUE)
                {
                    Cheque cheque = new Cheque();
                    cheque.Pessoa = new Pessoa();
                    cheque.ValorCheque = item.Valor;
                    cheque.DataVencimento = item.DataVencimento;
                    cheque.Pessoa.Id = IdCliente.ToInt();
                    cheque.Pessoa.DadosGerais.Razao = strCliente.ToString();
                    cheque.DataEmissao = DateTime.Now;
                    //cheque.Pessoa.Id = item.Recebimento.ClienteId;
                    //cheque.Pessoa.DadosGerais.Razao = item.Recebimento.ClienteNomeFantasia;
                    //cheque.DataEmissao = item.Recebimento.DataElaboracao;
                    cheque.DataCadastro = DateTime.Now;
                    cheque.NumeroPedidoVenda = _numeroPedido;

                    _listaCheques.Add(cheque);
                }
            }
        }

        private void PreenchaGrid()
        {
            GereIdFalsoParaOsCheques();

            List<ChequeGrid> listaDeChequesGrid = new List<ChequeGrid>();

            foreach (var cheque in _listaCheques)
            {
                ChequeGrid chequeGrid = new ChequeGrid();

                chequeGrid.Id = cheque.Id;
                chequeGrid.Cliente = cheque.Pessoa != null ? cheque.Pessoa.Id + " - " + cheque.Pessoa.DadosGerais.Razao : string.Empty;
                chequeGrid.Agencia = cheque.Agencia;
                chequeGrid.Banco = cheque.Banco != null ? cheque.Banco.Descricao : string.Empty;
                chequeGrid.Conta = cheque.Conta;

                chequeGrid.DataEmissao = cheque.DataEmissao != null ? cheque.DataEmissao : null;
                chequeGrid.DataRecebimento = cheque.DataRecebimento != null ? cheque.DataRecebimento : null;
                chequeGrid.DataVencimento = cheque.DataVencimento != null ? cheque.DataVencimento : null;
                chequeGrid.NumeroCheque = cheque.NumeroCheque;
                chequeGrid.Status = cheque.StatusCheque == null? null: cheque.StatusCheque.Value.Descricao();
                chequeGrid.Observacoes = cheque.Observacoes;

                chequeGrid.Valor = cheque.ValorCheque;
                chequeGrid.DataVencimento = cheque.DataVencimento != null ? cheque.DataVencimento : null;

                listaDeChequesGrid.Add(chequeGrid);
            }

            gcCheques.DataSource = listaDeChequesGrid;
            gcCheques.RefreshDataSource();
        }

        private void InsiraOuAtualizeChequeNaLista()
        {
            var itemCheque = RetorneChequeEmEdicao();

            ValidacaoCheque validacaoCheque = new ValidacaoCheque();

            validacaoCheque.ValideInclusao();

            try
            {
                validacaoCheque.Valide(itemCheque).AssegureSucesso();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            Action actionInserirCheque = () =>
            {   
                _listaCheques.Remove(_itemChequeEdicao);

                int posicao = itemCheque.Id - 1;
                
                _listaCheques.Insert(posicao,itemCheque);
                
                PreenchaGrid();               
            };
            
            string mensagemDeSucesso = "Cheque preenchido com sucesso.";
            string tituloMensagemDeSucesso = "Cheque atualizado.";
            string tituloMensagemDeErro = "Inconsistências ao atualizar Cheque.";

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirCheque, mensagemDeSucesso, tituloMensagemDeSucesso, tituloMensagemDeErro);                       
        }
        
        private void GereIdFalsoParaOsCheques()
        {
            for (int i = 0; i < _listaCheques.Count; i++)
            {
                _listaCheques[i].Id = i + 1;
            }
        }

        private void PreenchalstPedidos()
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            var listaPedidos = servicoPedidoDeVenda.ConsulteLista(null,null, null,null, new Pessoa{Id = txtIdPessoa.Text.ToInt()}, Negocio.Vendas.Enumeradores.EnumTipoPedidoDeVenda.PEDIDOVENDA, null);

            var listaContasPagarReceber = new ServicoContasPagarReceber().ConsulteLista(new Pessoa {Id = txtIdPessoa.Text.ToInt() }, EnumTipoOperacaoContasPagarReceber.RECEBER, EnumStatusContaPagarReceber.ABERTO,null,null,null,null,null);

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            if (listaContasPagarReceber == null) return;
            
            foreach (var item in listaContasPagarReceber)
            {                
                if (item.OrigemDocumento == EnumOrigemDocumento.PEDIDODEVENDAS)
                {
                    var nDocumento = item.NumeroDocumento.Split('-');
                    ObjetoDescricaoValor itemObjetoValor = new ObjetoDescricaoValor();

                    int numeroDoPedido = nDocumento[0].Trim().TrimEnd().ToInt();

                    if (listaPedidos.Exists(x=>x.Id == numeroDoPedido))
                    {
                        itemObjetoValor.Valor = numeroDoPedido;
                        itemObjetoValor.Descricao = itemObjetoValor.Valor.ToString();
                    }
                                       
                    listaObjetoValor.Add(itemObjetoValor);
                }
            }

            var listaAgrupada = listaObjetoValor.GroupBy(x=>x.Valor).ToList();

            foreach (var item in listaAgrupada)
            {  
                lstPedidos.Items.Add(item.Key);                
            }
            lstPedidos.UnSelectAll();
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

        private void btnPesquiseContaBancaria_Click(object sender, EventArgs e)
        {
            FormContaBancariaPesquisa formContaBancariaPesquisa = new FormContaBancariaPesquisa();

            var contaBancaria = formContaBancariaPesquisa.ExibaPesquisaDeContasBancarias();

            if (contaBancaria != null)
            {
                cboBancos.EditValue = contaBancaria.Agencia != null ? (int?)contaBancaria.Agencia.Banco.Id : null;
                txtAgencia.Text = contaBancaria.Agencia.NumeroAgencia;

                txtConta.Text = contaBancaria.NumeroConta;

                txtDigito.Text = contaBancaria.Agencia.DigitoAgencia;
            }
        }

        private void btnRecarregar_Click(object sender, EventArgs e)
        {
            lstPedidos.Items.Clear();
            PreenchalstPedidos();
        }

        private void btnLimparLista_Click(object sender, EventArgs e)
        {
            lstPedidos.Items.Clear();
        }
    }
}
