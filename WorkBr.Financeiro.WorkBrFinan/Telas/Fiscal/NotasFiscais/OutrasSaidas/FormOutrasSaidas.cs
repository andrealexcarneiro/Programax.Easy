using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Easy.Servico.Fiscal.CfopServ;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.View.Telas.Fiscal.CancelamentosNotasFiscais;
using Programax.Easy.View.Telas.Cadastros.NaturezasOperacoes;
using Programax.Easy.View.Telas.Relatorios;
using Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Fiscal.Cfops;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Cadastros.PaisServ;
using Programax.Easy.Servico.Cadastros.TransferenciaServ;
using Programax.Easy.Servico.Cadastros.SubEstoqueServ;

namespace Programax.Easy.View.Telas.Fiscal.OutrasSaidas
{
    public partial class FormOutrasSaidas : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private NotaFiscal _notaFiscalEmEdicao = null;

        private Produto _produtoEmEdicao = null;
        private List<Cfop> _listaCfops = null;
        private List<ItemNotaFiscal> _listaItensNotaFiscal = new List<ItemNotaFiscal>();
        private ItemNotaFiscal _itemNotaFiscalEmEdicao = null;

        private DuplicataNotaFiscal _duplicataEmEdicao = null;
        private List<DuplicataNotaFiscal> _listaDuplicatas = null;

        private List<Estado> _listaEstados;
        private List<Cidade> _listaCidadesDestinatario;
        private List<Cidade> _listaCidadesLocalRetirada;
        private List<Cidade> _listaCidadesLocalEntrega;

        private ServicoCidade _servicoCidade;
        
        private Pessoa _destinatarioSelecionado;

        private readonly IRepositorioProduto _repositorioProduto;

        private NotaFiscalReferenciada _notaFiscalReferenciadaEmEdicao;
        private List<NotaFiscalReferenciada> _notasFiscaisReferenciadas;

        private Empresa _empresa;
        private Parametros _parametros;
        
        private bool _variavelControleAlterandoTipoDescontoFechamento;
        private bool _editandoMVA;
        private bool _editandoAliquotaSt;
        private bool _editandoPercentualIcms;
        private bool _editandoAliquotaSimplesNacional;
        private bool _editandoPercentualIpi;
        private bool _editandoPercentualPis;
        private bool _editandoEmReaisPis;
        private bool _editandoPercentualCofins;
        private bool _editandoEmReaisCofins;
        private double _BaseDeCalculoIpi;
        private double _BaseDeCalculoPis;
        private double _BaseDeCalculoPisST;
        private double _BaseDeCalculoCofins;
        private double _BaseDeCalculoCofinsST;
        private bool _btnSalvar;

        private static readonly char[] s_Diacritics = GetDiacritics();

        #endregion

        #region " CONSTRUTOR "

        public FormOutrasSaidas()
        {
            InitializeComponent();

            _servicoCidade = new ServicoCidade();
            _notasFiscaisReferenciadas = new List<NotaFiscalReferenciada>();
            _listaDuplicatas = new List<DuplicataNotaFiscal>();

            //tbpFinanceiro.Enabled = false;

            PesquiseEmpresa();
            CarregueParametros();
            PreenchaCboOrigem();           
            PreenchaCboCstIpi();
            PreenchaCboCstPis();
            PreenchaCboCstCofins();
            PesquiseTodosCfop();
            
            PreenchaCboCondicaoPagamento();
            PreenchaCboFormaPagamento();
            PreenchaCboIndicadorDePresenca();
            PreenchaCboTipoNotaReferenciada();

            AltereTipoNotaReferenciada();

            PreenchaCboEstadoDestinatario();
            PreenchaCboEstadoLocalEntrega();
            PreenchaCboEstadoLocalRetirada();

            PreenchaCidadesDestinatario();

            PreenchaCboPaises();

            PreenchaCboUFNotaReferenciada();
            PreenchaCboUFLocalRetirada();

            PreenchaCboTipoFrete();
            PreenchaCboTransportadoras();

            PreenchaCboFinalidaeNFe();

            PreenchaCboRegimeTributario();
            
            this.ActiveControl = txtIdOutrasSaidas;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void txtIdOutrasSaidas_Leave(object sender, EventArgs e)
        {
            if (txtIdOutrasSaidas.Text != string.Empty)
            {
                int codigoConsuta = txtIdOutrasSaidas.Text.ToInt();
                LimpeForm();
                ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
                _notaFiscalEmEdicao = servicoNotaFiscal.Consulte(codigoConsuta);
                
                EditeNotaFiscal();
            }
        }

        #region " GUIA IDENTIFICAÇÃO NFE "

        private void btnInserirAtualizarNotaReferenciada_Click(object sender, EventArgs e)
        {
            InsiraOuAtualizeNotaFiscalReferenciada();
        }

        private void btnLimparCamposNotaReferenciada_Click(object sender, EventArgs e)
        {
            LimpeCamposNotaFiscalReferenciada();
        }

        private void btnExcluirNotaReferenciada_Click(object sender, EventArgs e)
        {
            if (_notasFiscaisReferenciadas.Count > 0 && _notaFiscalReferenciadaEmEdicao != null)
            {
                if (MessageBox.Show("Deseja excluir esta Nota ?", "Deseja excluir esta Nota ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    _notasFiscaisReferenciadas.Remove(_notaFiscalReferenciadaEmEdicao);

                    PreenchaGridNotasReferenciadas();

                    LimpeCamposNotaFiscalReferenciada();
                }
            }
        }

        private void gcNotasReferenciadas_DoubleClick(object sender, EventArgs e)
        {
            SelecioneNotaFiscalReferenciada();
        }

        private void gcNotasReferenciadas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneNotaFiscalReferenciada();
            }
        }

        private void rdbTipoPessoaJuridicaNotaReferenciada_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTipoPessoaFisicaNotaReferenciada.Checked)
            {
                lblCnpjEmitenteNotaReferenciada.Text = "Cpf Emitente";
                txtCnpjEmitenteNotaReferenciada.Text = string.Empty;
                txtCnpjEmitenteNotaReferenciada.Properties.Mask.EditMask = "000.000.000-00";
            }
            else
            {
                lblCnpjEmitenteNotaReferenciada.Text = "Cnpj Emitente";
                txtCnpjEmitenteNotaReferenciada.Text = string.Empty;
                txtCnpjEmitenteNotaReferenciada.Properties.Mask.EditMask = "00.000.000/0000-00";
            }
        }

        private void cboTipoNotaReferenciada_EditValueChanged(object sender, EventArgs e)
        {
            AltereTipoNotaReferenciada();
        }

        #endregion

        #region " GUIA ITENS "

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProduto();

            if (produto != null)
            {
                PreenchaCamposDoProduto(produto);
            }
        }

        private void txtCodigoDeBarrasProduto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoDeBarrasProduto.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(txtCodigoDeBarrasProduto.Text);
                PreenchaCamposDoProduto(produto);
            }
        }

        private void txtIdProduto_Leave(object sender, EventArgs e)
        {
            ServicoProduto servicoProduto = new ServicoProduto();

            var produto = servicoProduto.ConsulteProdutoAtivo(txtIdProduto.Text.ToInt());

            PreenchaCamposDoProduto(produto);
        }

        private void cboCstCsosn_EditValueChanged(object sender, EventArgs e)
        {
            HabiliteOuDesabiliteCamposIcms();
            HabiliteOuDesabiliteCamposIcmsDesoneracao();
            HabiliteOuDesabiliteCamposST();
            //CalculeIcms();
        }

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            //Campos obrigatórios para impostos
            VerificaCamposCboImpostosPreenchidos();

            //Valida campos de impostos
            if (!ValideCamposImpostoICMSIPI())
                return;

            InsiraOuAtualizeItemNaLista();            
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeItem();
        }

        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            var itemDaLista = _listaItensNotaFiscal.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

            EditeItem(itemDaLista);

            calculeValorTotalDoItem();

            //CalculeIcms();
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir este item?", "Exclusão de item", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _listaItensNotaFiscal.Remove(_itemNotaFiscalEmEdicao);

                LimpeItem();

                GeraIdParaCadaItem();

                AtualizeGridItens();
            }
        }

        private void txtValorUnitarioItem_EditValueChanged(object sender, EventArgs e)
        {
            CalculeTotalBrutoItem();
        }

        private void txtQuantidadeItem_EditValueChanged(object sender, EventArgs e)
        {
            CalculeTotalBrutoItem();
        }

        private void txtValorDescontoItem_EditValueChanged(object sender, EventArgs e)
        {
            CalculeSubTotalItem();
        }

        private void txtValorFreteItem_EditValueChanged(object sender, EventArgs e)
        {
            CalculeSubTotalItem();
        }

        private void txtValorSeguroItem_EditValueChanged(object sender, EventArgs e)
        {
            CalculeSubTotalItem();
        }

        private void txtValorOutrasDespesasItem_EditValueChanged(object sender, EventArgs e)
        {
            CalculeSubTotalItem();
        }

        #endregion

        #region " GUIA FINANCEIRO "

        private void cboCondicaoPagamento_EditValueChanged(object sender, EventArgs e)
        {
            if (cboCondicaoPagamento.EditValue == null || (EnumCondicaoPagamentoNota)cboCondicaoPagamento.EditValue == EnumCondicaoPagamentoNota.AVISTA)
            {
                //tbpFinanceiro.Enabled = false;

                txtNumeroFaturaFinanceiro.Text = string.Empty;
                txtValorOriginalFaturaFinanceiro.Text = string.Empty;
                txtValorDescontoFaturaFinanceiro.Text = string.Empty;
                txtValorLiquidoFaturaFinanceiro.Text = string.Empty;

                LimpeCamposFinanceiro();

                _listaDuplicatas.Clear();
                PreenchaGridFinanceiro();
            }
            else
            {
                tbpFinanceiro.Enabled = true;
            }
        }

        private void btnAdicionarFinanceiro_Click(object sender, EventArgs e)
        {
            InsiraOuAtualizeParcelaFinanceiro();
        }

        private void btnCancelarFinanceiro_Click(object sender, EventArgs e)
        {
            LimpeCamposFinanceiro();
        }

        private void btnExcluirFinanceiro_Click(object sender, EventArgs e)
        {
            var duplicataDaLista = _listaDuplicatas.FirstOrDefault(financeiro => financeiro.Id == colunaIdFinanceiro.View.GetFocusedRowCellValue(colunaIdFinanceiro).ToInt());

            string mensagem = "Deseja excluir a duplicata " + duplicataDaLista.Parcela + "?";

            if (MessageBox.Show(mensagem, "Exclusão de parcela", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _listaDuplicatas.Remove(duplicataDaLista);

                LimpeCamposFinanceiro();
                GereIdParaCadaDuplicataFinanceiro();
                PreenchaGridFinanceiro();
            }
        }

        private void gcParcelasFinanceiro_DoubleClick(object sender, EventArgs e)
        {
            var duplicataDaLista = _listaDuplicatas.FirstOrDefault(financeiro => financeiro.Id == colunaIdFinanceiro.View.GetFocusedRowCellValue(colunaIdFinanceiro).ToInt());

            EditeDuplicataFinanceiro(duplicataDaLista);
        }

        private void gcParcelasFinanceiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var duplicataDaLista = _listaDuplicatas.FirstOrDefault(financeiro => financeiro.Id == colunaIdFinanceiro.View.GetFocusedRowCellValue(colunaIdFinanceiro).ToInt());

                EditeDuplicataFinanceiro(duplicataDaLista);
            }
        }

        private void txtValorDuplicata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsiraOuAtualizeParcelaFinanceiro();
            }
        }

        #endregion

        

        #region " DESTINATARIO "

        private void cboEstadoDestinatario_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCidadesDestinatario();           
            //CalculeIcms();
        }

        private void chkParceiroResideExteriorDestinatario_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParceiroResideExteriorDestinatario.Checked)
            {
                cboPaisDestinatario.Enabled = true;
                cboPaisDestinatario.Obrigatorio = true;

                #region " DESABILITA CAMPOS "

                txtCepDestinatario.Enabled = false;
                cboEstadoDestinatario.Enabled = false;
                cboEstadoDestinatario.Obrigatorio = false;
                cboCidadeDestinatario.Enabled = false;
                cboCidadeDestinatario.Obrigatorio = false;

                txtCpfCnpjDestinatario.Enabled = false;
                txtInscricaoEstadualDestinatario.Enabled = false;

                #endregion

                #region " REMOVE OBRIGATORIEDADE DE CAMPOS "

                cboEstadoDestinatario.Obrigatorio = false;
                cboCidadeDestinatario.Obrigatorio = false;
                txtBairroDestinatario.Obrigatorio = false;
                txtRuaDestinatario.Obrigatorio = false;
                txtNumeroDestinatario.Obrigatorio = false;
                txtCepDestinatario.Obrigatorio = false;

                txtCpfCnpjDestinatario.Obrigatorio = false;

                #endregion

                #region " LIMPA CAMPOS "

                txtCepDestinatario.Text = string.Empty;
                cboEstadoDestinatario.EditValue = null;
                cboCidadeDestinatario.EditValue = null;
                //txtBairroDestinatario.Text = string.Empty;
                //txtRuaDestinatario.Text = string.Empty;
                txtComplementoDestinatario.Text = string.Empty;
                //txtNumeroDestinatario.Text = string.Empty;

                txtCpfCnpjDestinatario.Text = string.Empty;
                txtInscricaoEstadualDestinatario.Text = string.Empty;

                #endregion
            }
            else
            {
                cboPaisDestinatario.Enabled = false;
                cboPaisDestinatario.EditValue = null;
                cboPaisDestinatario.Obrigatorio = false;

                txtIdEstrangeiroDestinatario.Text = string.Empty;
                txtIdEstrangeiroDestinatario.Obrigatorio = false;

                #region " HABILITA CAMPOS "

                txtCepDestinatario.Enabled = true;
                cboEstadoDestinatario.Enabled = true;
                cboCidadeDestinatario.Enabled = true;
                txtBairroDestinatario.Enabled = true;
                txtRuaDestinatario.Enabled = true;
                txtComplementoDestinatario.Enabled = true;
                txtNumeroDestinatario.Enabled = true;

                txtCpfCnpjDestinatario.Enabled = true;
                //txtInscricaoEstadualDestinatario.Enabled = true;

                #endregion

                #region " OBRIGA CAMPOS "

                txtCepDestinatario.Obrigatorio = true;
                cboEstadoDestinatario.Obrigatorio = true;
                cboCidadeDestinatario.Obrigatorio = true;
                txtBairroDestinatario.Obrigatorio = true;
                txtRuaDestinatario.Obrigatorio = true;
                txtNumeroDestinatario.Obrigatorio = true;
                txtCpfCnpjDestinatario.Obrigatorio = true;

                #endregion
            }
        }

        private void btnAtalhoCliente_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisaPessoaDestinatario_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var cliente = formPessoaPesquisa.PesquisePessoaAtiva();

            if (cliente != null)
            {
                PreenchaCliente(cliente);
            }
        }

        private void txtIdParceiroDestinatario_Leave(object sender, EventArgs e)
        {
            if (_destinatarioSelecionado == null || txtIdParceiroDestinatario.Text.ToInt() != _destinatarioSelecionado.Id)
            {
                if (!string.IsNullOrEmpty(txtIdParceiroDestinatario.Text))
                {
                    ServicoPessoa servicoPessoa = new ServicoPessoa();
                    var cliente = servicoPessoa.ConsultePessoaAtiva(txtIdParceiroDestinatario.Text.ToInt());

                    PreenchaCliente(cliente, true);
                }
                else
                {
                    PreenchaCliente(null);
                }
            }
        }

        private void rdbTipoPessoaJuridicaDestinatario_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTipoPessoaFisicaDestinatario.Checked)
            {
                txtCpfCnpjDestinatario.Text = string.Empty;
                txtCpfCnpjDestinatario.Properties.Mask.EditMask = "000.000.000-00";
            }
            else
            {
                txtCpfCnpjDestinatario.Text = string.Empty;
                txtCpfCnpjDestinatario.Properties.Mask.EditMask = "00.000.000/0000-00";
            }
        }

        #endregion

        #region " LOCAL RETIRADA "

        private void cboEstadoLocalRetirada_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCidadesLocalRetirada();
        }

        private void chkInformarLocalDeRetirada_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInformarLocalDeRetirada.Checked)
            {
                rdbTipoPessoaJuridicaLocalRetirada.Enabled = true;
                rdbTipoPessoaFisicaLocalRetirada.Enabled = true;

                txtCpfCnpjLocalRetirada.Enabled = true;
                txtCpfCnpjLocalRetirada.Obrigatorio = true;

                cboEstadoLocalRetirada.Enabled = true;
                cboEstadoLocalRetirada.Obrigatorio = true;

                cboCidadeLocalRetirada.Enabled = true;
                cboCidadeLocalRetirada.Obrigatorio = true;

                txtBairroLocalRetirada.Enabled = true;
                txtBairroLocalRetirada.Obrigatorio = true;

                txtRuaLocalRetirada.Enabled = true;
                txtRuaLocalRetirada.Obrigatorio = true;

                txtComplementoLocalRetirada.Enabled = true;

                txtNumeroLocalRetirada.Enabled = true;
                txtNumeroLocalRetirada.Obrigatorio = true;
            }
            else
            {
                rdbTipoPessoaJuridicaLocalRetirada.Enabled = false;
                rdbTipoPessoaFisicaLocalRetirada.Enabled = false;

                txtCpfCnpjLocalRetirada.Enabled = false;
                txtCpfCnpjLocalRetirada.Obrigatorio = false;

                cboEstadoLocalRetirada.Enabled = false;
                cboEstadoLocalRetirada.Obrigatorio = false;

                cboCidadeLocalRetirada.Enabled = false;
                cboCidadeLocalRetirada.Obrigatorio = false;

                txtBairroLocalRetirada.Enabled = false;
                txtBairroLocalRetirada.Obrigatorio = false;

                txtRuaLocalRetirada.Enabled = false;
                txtRuaLocalRetirada.Obrigatorio = false;

                txtComplementoLocalRetirada.Enabled = false;

                txtNumeroLocalRetirada.Enabled = false;
                txtNumeroLocalRetirada.Obrigatorio = false;
            }
        }

        private void rdbTipoPessoaJuridicaLocalRetirada_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTipoPessoaFisicaLocalRetirada.Checked)
            {
                txtCpfCnpjLocalRetirada.Text = string.Empty;
                txtCpfCnpjLocalRetirada.Properties.Mask.EditMask = "000.000.000-00";
            }
            else
            {
                txtCpfCnpjLocalRetirada.Text = string.Empty;
                txtCpfCnpjLocalRetirada.Properties.Mask.EditMask = "00.000.000/0000-00";
            }
        }

        #endregion

        #region " LOCAL ENTREGA "

        private void cboEstadoLocalEntrega_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCidadesLocalEntrega();
        }

        private void chkInformarLocalEntrega_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInformarLocalEntrega.Checked)
            {
                rdbTipoPessoaJuridicaLocalEntrega.Enabled = true;
                rdbTipoPessoaFisicaLocalEntrega.Enabled = true;

                txtCpfCnpjLocalEntrega.Enabled = true;
                txtCpfCnpjLocalEntrega.Obrigatorio = true;

                cboEstadoLocalEntrega.Enabled = true;
                cboEstadoLocalEntrega.Obrigatorio = true;

                cboCidadeLocalEntrega.Enabled = true;
                cboCidadeLocalEntrega.Obrigatorio = true;

                txtBairroLocalEntrega.Enabled = true;
                txtBairroLocalEntrega.Obrigatorio = true;

                txtRuaLocalEntrega.Enabled = true;
                txtRuaLocalEntrega.Obrigatorio = true;

                txtComplementoLocalEntrega.Enabled = true;

                txtNumeroLocalEntrega.Enabled = true;
                txtNumeroLocalEntrega.Obrigatorio = true;
            }
            else
            {
                rdbTipoPessoaJuridicaLocalEntrega.Enabled = false;
                rdbTipoPessoaFisicaLocalEntrega.Enabled = false;

                txtCpfCnpjLocalEntrega.Enabled = false;
                txtCpfCnpjLocalEntrega.Obrigatorio = false;

                cboEstadoLocalEntrega.Enabled = false;
                cboEstadoLocalEntrega.Obrigatorio = false;

                cboCidadeLocalEntrega.Enabled = false;
                cboCidadeLocalEntrega.Obrigatorio = false;

                txtBairroLocalEntrega.Enabled = false;
                txtBairroLocalEntrega.Obrigatorio = false;

                txtRuaLocalEntrega.Enabled = false;
                txtRuaLocalEntrega.Obrigatorio = false;

                txtComplementoLocalEntrega.Enabled = false;

                txtNumeroLocalEntrega.Enabled = false;
                txtNumeroLocalEntrega.Obrigatorio = false;
            }
        }

        private void rdbTipoPessoaJuridicaLocalEntrega_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTipoPessoaFisicaLocalEntrega.Checked)
            {
                txtCpfCnpjLocalEntrega.Text = string.Empty;
                txtCpfCnpjLocalEntrega.Properties.Mask.EditMask = "000.000.000-00";
            }
            else
            {
                txtCpfCnpjLocalEntrega.Text = string.Empty;
                txtCpfCnpjLocalEntrega.Properties.Mask.EditMask = "00.000.000/0000-00";
            }
        }

        #endregion

        #region " INFORMAÇÕES DE COMPRA "

        private void chkInformaDadosCompraInformacoesCompra_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInformaDadosCompraInformacoesCompra.Checked)
            {
                txtNotaEmpenhoInformacoesCompra.Enabled = true;
                txtPedidoInformacoesCompra.Enabled = true;
                txtContratoInformacoesCompra.Enabled = true;
            }
            else
            {
                txtNotaEmpenhoInformacoesCompra.Enabled = false;
                txtPedidoInformacoesCompra.Enabled = false;
                txtContratoInformacoesCompra.Enabled = false;

                txtNotaEmpenhoInformacoesCompra.Text = string.Empty;
                txtPedidoInformacoesCompra.Text = string.Empty;
                txtContratoInformacoesCompra.Text = string.Empty;
            }
        }

        #endregion

        #region " COMÉRCIO EXTERIOR "

        private void chkInformarDadosComercioExterior_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInformarDadosComercioExterior.Checked)
            {
                cboUFComercioExterior.Enabled = true;
                txtDescricaoLocalDespachoComercioExterior.Enabled = true;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Enabled = true;

                cboUFComercioExterior.Obrigatorio = true;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Obrigatorio = true;
            }
            else
            {
                cboUFComercioExterior.Enabled = false;
                txtDescricaoLocalDespachoComercioExterior.Enabled = false;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Enabled = false;

                cboUFComercioExterior.Obrigatorio = false;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Obrigatorio = false;

                cboUFComercioExterior.Text = string.Empty;
                txtDescricaoLocalDespachoComercioExterior.Text = string.Empty;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Text = string.Empty;
            }
        }

        #endregion

        #region " PAINEL BOTÕES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeForm();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            //Se o grid de itens estiver preenchido com apenas um item, deixa de ser obrigatório
            if (gcItens.DataSource != null) txtIdProduto.Obrigatorio = false; txtCodigoCfop.Obrigatorio = false;
            if (gcNotasReferenciadas.DataSource.ToInt() == 0 && txtChaveAcessoNotaReferenciada.Text != string.Empty)            
            {
                MessageBox.Show("A chave de acesso não foi adicionada. Clique no botão adicionar para continuar");
                return;
            }

            VerificaCamposCboPreenchidos();

            if (verificaFinanceiro())
            {
                MessageBox.Show("Adicione as formas e condições de pagamento ao FINANCEIRO.", "Financeiro - Outras Saídas", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                return;
            }

            _btnSalvar = true;

            Action actionSalvar = () =>
            {
                PreenchaObjetoNotaFiscal();

                ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();

                if (_notaFiscalEmEdicao.Id == 0)
                {
                    servicoNotaFiscal.Cadastre(_notaFiscalEmEdicao);
                    EditeNotaFiscal();
                }
                else if ((_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.DISPONIVEL ||
                        _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA) &&
                        _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.OUTRASSAIDAS)
                {
                    servicoNotaFiscal.Atualize(_notaFiscalEmEdicao);
                }
                else
                {
                    _notaFiscalEmEdicao.Id = 0;
                    _notaFiscalEmEdicao.IdentificacaoNotaFiscal.NumeroNota = 0;
                    _notaFiscalEmEdicao.IdentificacaoNotaFiscal.Serie = 0;
                    _notaFiscalEmEdicao.IdentificacaoNotaFiscal.DataHoraEmissao = DateTime.Parse("01/01/0001 00:00:00");
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.DISPONIVEL;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.ChaveDeAcesso = string.Empty;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.MensagemDevolvida = string.Empty;
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.NumeroReciboLote = string.Empty;
                    servicoNotaFiscal.Cadastre(_notaFiscalEmEdicao);
                    EditeNotaFiscal();
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, controleValidar: this);
            _btnSalvar = false;
        }

        #endregion

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " PREENCHA CBOS "

        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

            _parametros = servicoParametros.ConsulteParametros();
        }

        private void PreenchaCboOrigem()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumOrigem>();

            cboOrigem.Properties.DataSource = lista;
            cboOrigem.Properties.DisplayMember = "Descricao";
            cboOrigem.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboCstCsosn()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCstCsosn>();

            if ((EnumCodigoRegimeTributario)cboRegimeTributario.EditValue == EnumCodigoRegimeTributario.REGIMENORMAL)
                lista.RemoveRange(11, 10);
            else
                lista.RemoveRange(0, 11);

            cboCstCsosn.Properties.DataSource = lista;
            cboCstCsosn.Properties.DisplayMember = "Descricao";
            cboCstCsosn.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboCstIpi()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCstIpi>();

            lista.Insert(0, null);

            if (rdbTipoOperacaoEntrada.Checked)
                lista.RemoveRange(8, 7);
            else
                lista.RemoveRange(1, 7);

            cboCstIpi.Properties.DataSource = lista;
            cboCstIpi.Properties.DisplayMember = "Descricao";
            cboCstIpi.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboCstPis()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCstPis>();

            lista.Insert(0, null);
            
            cboCstPis.Properties.DataSource = lista;
            cboCstPis.Properties.DisplayMember = "Descricao";
            cboCstPis.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboCstCofins()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCstCofins>();

            lista.Insert(0, null);

            cboCstCofins.Properties.DataSource = lista;
            cboCstCofins.Properties.DisplayMember = "Descricao";
            cboCstCofins.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboModBCST()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModBCST>();
                       
            cboModBCST.Properties.DataSource = lista;
            cboModBCST.Properties.DisplayMember = "Descricao";
            cboModBCST.Properties.ValueMember = "Valor";

            cboModBCST.EditValue = EnumModBCST.MARGEMVALORAGREGADOPERCENTUAL;
        }

        private void PreenchaCboModBC()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModBC>();

            cboModBC.Properties.DataSource = lista;
            cboModBC.Properties.DisplayMember = "Descricao";
            cboModBC.Properties.ValueMember = "Valor";

            cboModBC.EditValue = EnumModBC.MARGEMVALORAGREGADO;
        }

        private void PreenchaCboMotivoDesoneracao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumMotivoDesoneracaoProduto>();

            cboMotivoDesoneracao.Properties.DataSource = lista;
            cboMotivoDesoneracao.Properties.DisplayMember = "Descricao";
            cboMotivoDesoneracao.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboFormaPagamento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoFormaPagamento>();

            cboFormaPagamento.Properties.DataSource = lista;
            cboFormaPagamento.Properties.DisplayMember = "Descricao";
            cboFormaPagamento.Properties.ValueMember = "Valor";
            
            
        }

        private void PesquiseTodosCfop()
        {
            ServicoCfop servicoCfop = new ServicoCfop();
            _listaCfops = servicoCfop.ConsulteListaAtiva();
        }

        private void PesquiseEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            _empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            _empresa.DadosEmpresa.Endereco.Cidade.Estado.CarregueLazyLoad();
        }
        
        private void PreenchaCboCondicaoPagamento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCondicaoPagamentoNota>();

            lista.Insert(0, null);

            cboCondicaoPagamento.Properties.DataSource = lista;
            cboCondicaoPagamento.Properties.DisplayMember = "Descricao";
            cboCondicaoPagamento.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboIndicadorDePresenca()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumIndicacaoPresenca>();
            
            lista.Insert(0, null);

            cboIndicadorDePresenca.Properties.DataSource = lista;
            cboIndicadorDePresenca.Properties.DisplayMember = "Descricao";
            cboIndicadorDePresenca.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboTipoNotaReferenciada()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoNotaReferenciada>();

            lista.Insert(0, null);

            cboTipoNotaReferenciada.Properties.DataSource = lista;
            cboTipoNotaReferenciada.Properties.DisplayMember = "Descricao";
            cboTipoNotaReferenciada.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboModeloDocumentoReferenciado()
        {
            cboModelODocumentoNotaReferenciada.EditValue = null;

            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModeloNotaFiscalReferenciada>();

            lista.Insert(0, null);

            EnumTipoNotaReferenciada? tipoNotaSelecionada = (EnumTipoNotaReferenciada?)cboTipoNotaReferenciada.EditValue;

            if (tipoNotaSelecionada == null)
            {
                return;
            }
            else if (tipoNotaSelecionada.Value == EnumTipoNotaReferenciada.NFEOUNFCE)
            {
                cboModelODocumentoNotaReferenciada.EditValue = null;
                lista.Clear();
            }
            else if (tipoNotaSelecionada.Value == EnumTipoNotaReferenciada.NOTAFISCAL1A)
            {
                cboModelODocumentoNotaReferenciada.EditValue = EnumModeloNotaFiscalReferenciada.MODELO01;
            }
            else if (tipoNotaSelecionada.Value == EnumTipoNotaReferenciada.NOTAFISCALPRODUTORRURAL)
            {
                lista.RemoveRange(2, 3);
            }
            else if (tipoNotaSelecionada.Value == EnumTipoNotaReferenciada.CUPOMFISCAL)
            {
                lista.RemoveAt(1);
                lista.RemoveAt(4);
            }

            cboModelODocumentoNotaReferenciada.Properties.DataSource = lista;
            cboModelODocumentoNotaReferenciada.Properties.DisplayMember = "Descricao";
            cboModelODocumentoNotaReferenciada.Properties.ValueMember = "Valor";
        }

        private void PreenchaCidadesDestinatario()
        {
            _listaCidadesDestinatario = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(cboEstadoDestinatario.EditValue.ToStringEmpty());

            var lista = _listaCidadesDestinatario.CloneCompleto();
            lista.Insert(0, null);

            cboCidadeDestinatario.Properties.DataSource = lista;
            cboCidadeDestinatario.Properties.DisplayMember = "Descricao";
            cboCidadeDestinatario.Properties.ValueMember = "CodigoIbge";

            cboCidadeDestinatario.EditValue = null;
        }

        private void PreenchaCidadesLocalRetirada()
        {
            if (cboEstadoLocalRetirada.EditValue != null)
            {
                _listaCidadesLocalRetirada = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(cboEstadoLocalRetirada.EditValue.ToString());

                var lista = _listaCidadesLocalRetirada.CloneCompleto();
                lista.Insert(0, null);

                cboCidadeLocalRetirada.Properties.DataSource = lista;
                cboCidadeLocalRetirada.Properties.DisplayMember = "Descricao";
                cboCidadeLocalRetirada.Properties.ValueMember = "CodigoIbge";
            }
            else
            {
                cboCidadeLocalRetirada.Properties.DataSource = null;
            }

            cboCidadeLocalRetirada.EditValue = null;
        }

        private void PreenchaCidadesLocalEntrega()
        {
            if (cboEstadoLocalEntrega.EditValue != null)
            {
                _listaCidadesLocalEntrega = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(cboEstadoLocalEntrega.EditValue.ToString());

                var lista = _listaCidadesLocalEntrega.CloneCompleto();
                lista.Insert(0, null);

                cboCidadeLocalEntrega.Properties.DataSource = lista;
                cboCidadeLocalEntrega.Properties.DisplayMember = "Descricao";
                cboCidadeLocalEntrega.Properties.ValueMember = "CodigoIbge";
            }
            else
            {
                cboCidadeLocalEntrega.Properties.DataSource = null;
            }

            cboCidadeLocalEntrega.EditValue = null;
        }

        private void PreenchaCboEstadoDestinatario()
        {
            if (_listaEstados == null)
            {
                ServicoEstado servicoEstado = new ServicoEstado();
                _listaEstados = servicoEstado.ConsulteListaEstados();
            }

            var lista = _listaEstados.CloneCompleto();

            lista.Insert(0, null);

            cboEstadoDestinatario.Properties.DataSource = lista;
            cboEstadoDestinatario.Properties.DisplayMember = "Nome";
            cboEstadoDestinatario.Properties.ValueMember = "UF";
        }

        private void PreenchaCboEstadoLocalEntrega()
        {
            if (_listaEstados == null)
            {
                ServicoEstado servicoEstado = new ServicoEstado();
                _listaEstados = servicoEstado.ConsulteListaEstados();
            }

            var lista = _listaEstados.CloneCompleto();

            lista.Insert(0, null);

            cboEstadoLocalEntrega.Properties.DataSource = lista;
            cboEstadoLocalEntrega.Properties.DisplayMember = "Nome";
            cboEstadoLocalEntrega.Properties.ValueMember = "UF";
        }

        private void PreenchaCboEstadoLocalRetirada()
        {
            if (_listaEstados == null)
            {
                ServicoEstado servicoEstado = new ServicoEstado();
                _listaEstados = servicoEstado.ConsulteListaEstados();
            }

            var lista = _listaEstados.CloneCompleto();

            lista.Insert(0, null);

            cboEstadoLocalRetirada.Properties.DataSource = lista;
            cboEstadoLocalRetirada.Properties.DisplayMember = "Nome";
            cboEstadoLocalRetirada.Properties.ValueMember = "UF";
        }

        private void PreenchaCboUFNotaReferenciada()
        {
            if (_listaEstados == null)
            {
                ServicoEstado servicoEstado = new ServicoEstado();
                _listaEstados = servicoEstado.ConsulteListaEstados();
            }

            var lista = _listaEstados.CloneCompleto();

            lista.Insert(0, null);

            cboUFEmitenteNotaReferenciada.Properties.DataSource = lista;
            cboUFEmitenteNotaReferenciada.Properties.DisplayMember = "UF";
            cboUFEmitenteNotaReferenciada.Properties.ValueMember = "CodigoEstado";
        }

        private void PreenchaCboUFLocalRetirada()
        {
            if (_listaEstados == null)
            {
                ServicoEstado servicoEstado = new ServicoEstado();
                _listaEstados = servicoEstado.ConsulteListaEstados();
            }

            var lista = _listaEstados.CloneCompleto();

            lista.Insert(0, null);

            cboUFComercioExterior.Properties.DataSource = lista;
            cboUFComercioExterior.Properties.DisplayMember = "UF";
            cboUFComercioExterior.Properties.ValueMember = "UF";
        }

        private void PreenchaCboTipoFrete()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoFrete>();

            lista.Insert(0, null);

            cboTipoFrete.Properties.DataSource = lista;
            cboTipoFrete.Properties.DisplayMember = "Descricao";
            cboTipoFrete.Properties.ValueMember = "Valor";

            if (_parametros.ParametrosVenda.TipoFrete != null)
                cboTipoFrete.EditValue = _parametros.ParametrosVenda.TipoFrete;
        }

        private void PreenchaCboTransportadoras()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaTransportadorasAtivas();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboTransportadoras.Properties.DisplayMember = "Descricao";
            cboTransportadoras.Properties.ValueMember = "Valor";
            cboTransportadoras.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboTransportadoras.Text))
            {
                cboTransportadoras.EditValue = null;
            }

            if (_parametros.ParametrosVenda.Transportadora != null)
            {   
                chkInformarTransportadora.Checked = true;               
                cboTransportadoras.EditValue = _parametros.ParametrosVenda.Transportadora.Id;
            }
        }

        private void PreenchaCboFinalidaeNFe()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumFinalidadeEmissaoNfe>();

            lista.RemoveAt(2);

            cboFinalidadeNFe.Properties.DataSource = lista;
            cboFinalidadeNFe.Properties.DisplayMember = "Descricao";
            cboFinalidadeNFe.Properties.ValueMember = "Valor";

            cboFinalidadeNFe.EditValue = EnumFinalidadeEmissaoNfe.NFENORMAL;
        }

        private void PreenchaCboRegimeTributario()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCodigoRegimeTributario>();

            cboRegimeTributario.Properties.DataSource = lista;
            cboRegimeTributario.Properties.DisplayMember = "Descricao";
            cboRegimeTributario.Properties.ValueMember = "Valor";

            if (_empresa.DadosEmpresa.CodigoRegimeTributario == EnumCodigoRegimeTributario.REGIMENORMAL)
                cboRegimeTributario.EditValue = EnumCodigoRegimeTributario.REGIMENORMAL;
            else
                cboRegimeTributario.EditValue = EnumCodigoRegimeTributario.SIMPLESNACIONAL;
            cboRegimeTributario.Properties.ReadOnly = false;
        }


        #endregion

        #region " 1 - IDENTIFICAÇÃO NF-E "

        private void AltereTipoNotaReferenciada()
        {
            PreenchaCboModeloDocumentoReferenciado();

            painelNotaReferenciada.Controls.Remove(pnlBotoesControleNotaFiscalReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlChaveAcessoNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlUFEmitenteNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlAnoMesNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlTipoPessoaNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlCpfCnpjEmitenteNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlSerieNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlNumeroNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlInscricaoEstadualNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlCTeNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlNumeroEcfNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlCooNotaReferenciada);
            painelNotaReferenciada.Controls.Remove(pnlModeloDocumentoNotaFiscalReferenciada);

            EditeNotaFiscalReferenciada(null, alterarNotaReferenciadaEmEdicao: false);

            painelNotaReferenciada.Controls.Remove(gcNotasReferenciadas);

            gcNotasReferenciadas.Size = new Size(924, 226);

            EnumTipoNotaReferenciada? tipoNotaSelecionada = (EnumTipoNotaReferenciada?)cboTipoNotaReferenciada.EditValue;

            if (tipoNotaSelecionada == null)
            {
                painelNotaReferenciada.Controls.Add(gcNotasReferenciadas);

                return;
            }

            if (tipoNotaSelecionada.Value == EnumTipoNotaReferenciada.NFEOUNFCE)
            {
                painelNotaReferenciada.Controls.Add(pnlChaveAcessoNotaReferenciada);
            }
            else if (tipoNotaSelecionada.Value == EnumTipoNotaReferenciada.NOTAFISCAL1A)
            {
                painelNotaReferenciada.Controls.Add(pnlUFEmitenteNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlAnoMesNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlCpfCnpjEmitenteNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlSerieNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlNumeroNotaReferenciada);
            }
            else if (tipoNotaSelecionada.Value == EnumTipoNotaReferenciada.NOTAFISCALPRODUTORRURAL)
            {
                painelNotaReferenciada.Controls.Add(pnlUFEmitenteNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlAnoMesNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlTipoPessoaNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlCpfCnpjEmitenteNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlSerieNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlNumeroNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlInscricaoEstadualNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlCTeNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlModeloDocumentoNotaFiscalReferenciada);

                gcNotasReferenciadas.Size = new Size(924, 199);
            }
            else if (tipoNotaSelecionada.Value == EnumTipoNotaReferenciada.CUPOMFISCAL)
            {
                painelNotaReferenciada.Controls.Add(pnlNumeroEcfNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlCooNotaReferenciada);
                painelNotaReferenciada.Controls.Add(pnlModeloDocumentoNotaFiscalReferenciada);
            }

            painelNotaReferenciada.Controls.Add(pnlBotoesControleNotaFiscalReferenciada);
            painelNotaReferenciada.Controls.Add(gcNotasReferenciadas);
        }

        private void InsiraOuAtualizeNotaFiscalReferenciada()
        {
            Action actionInsiraAtualizeNotaReferenciada = () =>
            {
                _notaFiscalReferenciadaEmEdicao = _notaFiscalReferenciadaEmEdicao ?? new NotaFiscalReferenciada();

                _notaFiscalReferenciadaEmEdicao.AnoMesEmissao = txtAnoMesEmissaoNotaReferenciada.Text.ToStringNull();
                _notaFiscalReferenciadaEmEdicao.ChaveDeAcesso = txtChaveAcessoNotaReferenciada.Text.ToStringNull();
                _notaFiscalReferenciadaEmEdicao.TipoPessoa = rdbTipoPessoaFisicaNotaReferenciada.Checked ? EnumTipoPessoa.PESSOAFISICA : EnumTipoPessoa.PESSOAJURIDICA;
                _notaFiscalReferenciadaEmEdicao.CnpjEmitente = txtCnpjEmitenteNotaReferenciada.Text.ToStringNull();
                _notaFiscalReferenciadaEmEdicao.CodigoUF = cboUFEmitenteNotaReferenciada.EditValue.ToIntNullabel();
                _notaFiscalReferenciadaEmEdicao.Coo = txtCOONotaReferenciada.Text.ToStringNull();
                _notaFiscalReferenciadaEmEdicao.CTe = txtCTeNotaFiscalReferenciada.Text.ToStringNull();
                _notaFiscalReferenciadaEmEdicao.InscricaoEstadual = txtInscricaoEstadualNotaReferenciada.Text.ToStringNull();
                _notaFiscalReferenciadaEmEdicao.ModeloDocumento = (EnumModeloNotaFiscalReferenciada?)cboModelODocumentoNotaReferenciada.EditValue;
                _notaFiscalReferenciadaEmEdicao.NumeroEcf = txtNumeroEcfNotaReferenciada.Text.ToStringNull();
                _notaFiscalReferenciadaEmEdicao.NumeroNota = txtNumeroNotaReferenciada.Text.ToStringNull();
                _notaFiscalReferenciadaEmEdicao.SerieNota = txtSerieNotaReferenciada.Text.ToStringNull();
                _notaFiscalReferenciadaEmEdicao.TipoNotaReferenciada = (EnumTipoNotaReferenciada)cboTipoNotaReferenciada.EditValue;

                if (_notaFiscalReferenciadaEmEdicao.Id == 0)
                {
                    _notasFiscaisReferenciadas.Add(_notaFiscalReferenciadaEmEdicao);
                }

                PreenchaGridNotasReferenciadas();

                LimpeCamposNotaFiscalReferenciada();
            };

            string mensagemDeSucesso = "Nota Referenciada inserida com sucesso.";
            string tituloMensagemDeSucesso = "Nota Referenciada inserido.";
            string tituloMensagemDeErro = "Inconsistências ao inserir...";

            if (_notaFiscalReferenciadaEmEdicao != null)
            {
                mensagemDeSucesso = "Nota Referenciada atualizada com sucesso.";
                tituloMensagemDeSucesso = "Nota Referenciada atualizada.";
                tituloMensagemDeErro = "Inconsistências ao atualizar...";
            }

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInsiraAtualizeNotaReferenciada, mensagemDeSucesso, tituloMensagemDeSucesso, tituloMensagemDeErro, controleValidar: painelNotaReferenciada);
        }

        private void LimpeCamposNotaFiscalReferenciada()
        {
            EditeNotaFiscalReferenciada(null);
            cboTipoNotaReferenciada.Focus();
        }

        private void SelecioneNotaFiscalReferenciada()
        {
            if (_notasFiscaisReferenciadas != null && _notasFiscaisReferenciadas.Count > 0)
            {
                var notaFiscal = _notasFiscaisReferenciadas.FirstOrDefault(item => item.Id == colunaIdIdNotaReferenciada.View.GetFocusedRowCellValue(colunaId).ToInt());

                EditeNotaFiscalReferenciada(notaFiscal);
            }
        }

        private void EditeNotaFiscalReferenciada(NotaFiscalReferenciada notaFiscalReferenciada, bool alterarNotaReferenciadaEmEdicao = true)
        {
            if (alterarNotaReferenciadaEmEdicao)
            {
                _notaFiscalReferenciadaEmEdicao = notaFiscalReferenciada;
            }

            if (notaFiscalReferenciada != null)
            {
                rdbTipoPessoaJuridicaNotaReferenciada.Checked = true;

                cboTipoNotaReferenciada.EditValue = notaFiscalReferenciada.TipoNotaReferenciada;

                txtChaveAcessoNotaReferenciada.Text = notaFiscalReferenciada.ChaveDeAcesso;
                cboUFEmitenteNotaReferenciada.EditValue = notaFiscalReferenciada.CodigoUF;
                txtAnoMesEmissaoNotaReferenciada.Text = notaFiscalReferenciada.AnoMesEmissao;
                rdbTipoPessoaFisicaNotaReferenciada.Checked = notaFiscalReferenciada.TipoPessoa == EnumTipoPessoa.PESSOAFISICA;
                txtCnpjEmitenteNotaReferenciada.Text = notaFiscalReferenciada.CnpjEmitente;
                txtSerieNotaReferenciada.Text = notaFiscalReferenciada.SerieNota;
                txtNumeroNotaReferenciada.Text = notaFiscalReferenciada.NumeroNota;
                txtInscricaoEstadualNotaReferenciada.Text = notaFiscalReferenciada.InscricaoEstadual;
                txtCTeNotaFiscalReferenciada.Text = notaFiscalReferenciada.CTe;
                txtNumeroEcfNotaReferenciada.Text = notaFiscalReferenciada.NumeroEcf;
                txtCOONotaReferenciada.Text = notaFiscalReferenciada.Coo;
                cboModelODocumentoNotaReferenciada.EditValue = notaFiscalReferenciada.ModeloDocumento;

                btnInserirAtualizarNotaReferenciada.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                if (alterarNotaReferenciadaEmEdicao)
                {
                    cboTipoNotaReferenciada.EditValue = null;
                    btnInserirAtualizarNotaReferenciada.Image = Properties.Resources.icones2_19;
                }

                txtChaveAcessoNotaReferenciada.Text = string.Empty;
                cboUFEmitenteNotaReferenciada.EditValue = null;
                txtAnoMesEmissaoNotaReferenciada.Text = string.Empty;
                rdbTipoPessoaJuridicaNotaReferenciada.Checked = true;
                txtCnpjEmitenteNotaReferenciada.Text = string.Empty;
                txtSerieNotaReferenciada.Text = string.Empty;
                txtNumeroNotaReferenciada.Text = string.Empty;
                txtInscricaoEstadualNotaReferenciada.Text = string.Empty;
                txtCTeNotaFiscalReferenciada.Text = string.Empty;
                txtNumeroEcfNotaReferenciada.Text = string.Empty;
                txtCOONotaReferenciada.Text = string.Empty;
                cboModelODocumentoNotaReferenciada.EditValue = null;
            }
        }

        private void PreenchaGridNotasReferenciadas()
        {
            List<NotaReferenciadaGrid> listaNotasReferenciadasGrid = new List<NotaReferenciadaGrid>();

            int contador = 0;

            foreach (var item in _notasFiscaisReferenciadas)
            {
                NotaReferenciadaGrid notaReferenciadaGrid = new NotaReferenciadaGrid();

                item.Id = contador + 1;

                notaReferenciadaGrid.Id = item.Id;
                notaReferenciadaGrid.ChaveDeAcesso = item.ChaveDeAcesso;
                notaReferenciadaGrid.CnpjCpfEmitente = item.CnpjEmitente;
                notaReferenciadaGrid.ModeloDocumentoFiscal = item.ModeloDocumento != null ? item.ModeloDocumento.Value.Descricao() : string.Empty;
                notaReferenciadaGrid.TipoNotaReferenciada = item.TipoNotaReferenciada.Descricao();

                listaNotasReferenciadasGrid.Add(notaReferenciadaGrid);

                contador++;
            }

            gcNotasReferenciadas.DataSource = listaNotasReferenciadasGrid;
            gcNotasReferenciadas.RefreshDataSource();
        }

        #endregion

        #region " 2 - DESTINATARIO "

        private void PreenchaCboPaises()
        {
            ServicoPais servicoPais = new ServicoPais();
            var listaPaises = servicoPais.ConsulteLista();

            var lista = listaPaises.CloneCompleto();
            lista.Insert(0, null);

            cboPaisDestinatario.Properties.DataSource = lista;
            cboPaisDestinatario.Properties.ValueMember = "Id";
            cboPaisDestinatario.Properties.DisplayMember = "NomePais";
        }

        private void PreenchaCliente(Pessoa cliente,
                                                 bool exibirMensagemDeNaoEncontrado = false,
                                                 bool alterarDadosDestinatario = true)
        {
            _destinatarioSelecionado = cliente;

            if (alterarDadosDestinatario)
            {
                PreenchaDadosDestinatario(cliente);
                PreenchaDadosDestinatarioEndereco(cliente);
            }

            if (cliente == null)
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Destinatário nao encontrado!", "Destinatário não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdParceiroDestinatario.Focus();
                }
            }
        }

        private void PreenchaDadosDestinatario(Pessoa cliente)
        {
            if (cliente != null)
            {
                txtIdParceiroDestinatario.Text = cliente.Id.ToString();

                tbpDestinatario.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Tag.ToInt() == (int)cliente.DadosGerais.TipoPessoa).Checked = true;
                txtInscricaoEstadualDestinatario.Text = cliente.EmpresaPessoa == null? string.Empty:cliente.EmpresaPessoa.InscricaoEstadual;
                txtEmailDestinatario.Text = cliente.EmpresaPessoa == null? string.Empty:cliente.EmpresaPessoa.EmailPrincipal;

                txtCpfCnpjDestinatario.Text = cliente.DadosGerais.CpfCnpj;
                txtRazaoSocialParceiroDestinatario.Text = cliente.DadosGerais.Razao;

                txtIdEstrangeiroDestinatario.Text = cliente.DadosPessoais != null ? cliente.DadosPessoais.IdEstrangeiro : string.Empty;

                var telefone = cliente.ListaDeTelefones.FirstOrDefault();

                if (telefone != null)
                {
                    txtDDDDestinatario.Text = telefone.Ddd.GetValueOrDefault().ToString();
                    txtNumeroTelefoneDestinatario.Text = telefone.Numero;
                }
            }
            else
            {
                txtIdParceiroDestinatario.Text = string.Empty;

                rdbTipoPessoaFisicaDestinatario.Checked = true;
                txtInscricaoEstadualDestinatario.Text = string.Empty;
                txtEmailDestinatario.Text = string.Empty;

                txtCpfCnpjDestinatario.Text = string.Empty;
                txtRazaoSocialParceiroDestinatario.Text = string.Empty;
                txtIdEstrangeiroDestinatario.Text = string.Empty;

                txtDDDDestinatario.Text = string.Empty;
                txtNumeroTelefoneDestinatario.Text = string.Empty;
            }
        }

        private void PreenchaDadosDestinatarioEndereco(Pessoa cliente)
        {
            if (cliente != null)
            {
                chkParceiroResideExteriorDestinatario.Checked = cliente.DadosGerais.PessoaResideExterior;
                cboPaisDestinatario.EditValue = cliente.DadosGerais.Pais != null ? (int?)cliente.DadosGerais.Pais.Id : null;
            }

            if (cliente != null && cliente.ListaDeEnderecos != null && cliente.ListaDeEnderecos.Count > 0)
            {
                var endereco = cliente.ListaDeEnderecos.FirstOrDefault(end => end.TipoEndereco == EnumTipoEndereco.PRINCIPAL);

                if (endereco == null)
                {
                    endereco = cliente.ListaDeEnderecos.FirstOrDefault();
                }


                if (endereco != null)
                {
                    txtNumeroDestinatario.Text = endereco.Numero;
                    txtComplementoDestinatario.Text = endereco.Complemento;

                    txtCepDestinatario.Text = endereco.CEP;

                    cboEstadoDestinatario.EditValue = endereco.Cidade != null? endereco.Cidade.Estado.UF:null;
                    cboCidadeDestinatario.EditValue = endereco.Cidade != null ? endereco.Cidade.CodigoIbge:null;

                    txtBairroDestinatario.Text = endereco.Bairro;
                    txtRuaDestinatario.Text = endereco.Rua;
                }
                else
                {
                    txtNumeroDestinatario.Text = string.Empty;
                    txtComplementoDestinatario.Text = string.Empty;

                    txtCepDestinatario.Text = string.Empty;

                    cboEstadoDestinatario.EditValue = null;
                    cboCidadeDestinatario.EditValue = null;

                    txtBairroDestinatario.Text = string.Empty;
                    txtRuaDestinatario.Text = string.Empty;
                }
            }
            else
            {
                txtNumeroDestinatario.Text = string.Empty;
                txtComplementoDestinatario.Text = string.Empty;

                txtCepDestinatario.Text = string.Empty;

                cboEstadoDestinatario.EditValue = null;
                cboCidadeDestinatario.EditValue = null;

                txtBairroDestinatario.Text = string.Empty;
                txtRuaDestinatario.Text = string.Empty;
            }
        }

        #endregion



        #region " 4 - ITENS "

        private void CalculeTotalBrutoItem()
        {
            txtValorTotalBrutoItem.Text = (txtValorUnitarioItem.Text.ToDouble() * txtQuantidadeItem.Text.ToDouble()).ToString("0.00");

            CalculeSubTotalItem();
        }

        private void CalculeSubTotalItem()
        {
            double quantidade = 1;
            var descontoEmValor = CalculosPedidoDeVenda.CalculeTotalDesconto(txtValorUnitarioItem.Text.ToDouble(),
                                                                                                              quantidade,
                                                                                                              txtValorDescontoItem.Text.ToDouble(),
                                                                                                              rdbDescontoItemPercentual.Checked);

            txtSubTotalItem.Text = (CalculosNotaFiscal.CalculeValorTotalItem(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
                                                                                descontoEmValor,txtValorFreteItem.Text.ToDouble(),
                                                                                txtValorSeguroItem.Text.ToDouble(), txtValorOutrasDespesasItem.Text.ToDouble())).ToString("0.00");
                       
            CalculeTotais();
        }

        private void CalculeTotais()
        {
            //CalculeIcms(); 
            calculeValorTotalDoItem();
            CalculeTotaisNotaFiscal();
        }

        private void CalculeAliquotaSimplesNacional(EnumCstCsosn cstCsosn, double descontoTotal)
        {
            if (cstCsosn == EnumCstCsosn.SIMPLES101 || cstCsosn == EnumCstCsosn.SIMPLES201 || cstCsosn == EnumCstCsosn.SIMPLES900)
            {  

                txtValorIcmsSimplesNacional.Text = ((txtQuantidadeItem.Text.ToDouble() * txtValorUnitarioItem.Text.ToDouble() -
                                                descontoTotal) * (txtAliquotaSimplesNacional.Text.ToDouble() / (double)100)).ToString("0.00");
            }
            else
                txtValorIcmsSimplesNacional.Text = string.Empty; 
        }

        private void CalculeTotaisNotaFiscal()
        {
            txtValorTotalNFeTotais.Text = _listaItensNotaFiscal != null? _listaItensNotaFiscal.Sum(i => i.ValorTotal).ToString("0.00"): "0.00";

            txtBaseCalculoIcmsTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.Icms.BaseCalculoIcms).Value.ToString("0.00") : "0.00";
            txtValorIcmsTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.Icms.ValorIcms).Value.ToString("0.00") : "0.00";
            txtValorIcmsDesoneradoTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.Icms.ValorDesoneracaoProduto).Value.ToString("0.00") : "0.00";
            txtBaseCalculoIcmsSTTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.Icms.BaseIcmsSubstituicaoTributaria).Value.ToString("0.00") : "0.00"; 
            txtValorIcmsSTTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.Icms.ValorSubstituicaoTributaria).Value.ToString("0.00") : "0.00";

            txtValorTotalProdutosTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.ValorBruto).ToString("0.00") : "0.00";          
            txtValorTotalFreteTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.ValorFrete).Value.ToString("0.00") : "0.00";
            txtValorTotalSeguroTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Seguro).Value.ToString("0.00") : "0.00";
            txtValorTotalDescontoTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.ValorDesconto).Value.ToString("0.00") : "0.00";

            txtValorTotalImpostoImportacaoTotais.Text = "0.00";
            
            txtValorTotalIPITotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.Ipi != null? i.Impostos.Ipi.ValorIpi:0).Value.ToString("0.00") : "0.00";

            txtValorTotalPISTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.Pis != null ? i.Impostos.Pis.ValorPis : 0).Value.ToString("0.00") : "0.00";
            txtValorTotalPisTotaisST.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.Pis != null ? i.Impostos.Pis.ValorPisST : 0).Value.ToString("0.00") : "0.00";

            txtValorTotalCofinsTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.Cofins != null? i.Impostos.Cofins.ValorCofins:0).Value.ToString("0.00") : "0.00";
            txtValorTotalCofinsTotaisST.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.Cofins != null ? i.Impostos.Cofins.ValorCofinsST : 0).Value.ToString("0.00") : "0.00";

            txtValorOutrasDespesasTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.OutrasDespesas).Value.ToString("0.00") : "0.00";
            
            txtTotalTributacaoEstadualTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.TotalTributacaoEstadual).ToString("0.00") : "0.00";
            txtTributacaoFederalTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.TotalTributacaoFederal).ToString("0.00") : "0.00";
            txtFCPTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.IcmsInterestadual != null? i.Impostos.IcmsInterestadual.ValorFCP:0).ToString("0.00") : "0.00";
            txtIcmsInterestadualDestinoTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.IcmsInterestadual != null ? i.Impostos.IcmsInterestadual.ValorIcmsDestino:0).ToString("0.00") : "0.00";
            txtIcmsInterestadualOrigemTotais.Text = _listaItensNotaFiscal != null ? _listaItensNotaFiscal.Sum(i => i.Impostos.IcmsInterestadual != null ? i.Impostos.IcmsInterestadual.ValorIcmsDestino:0).ToString("0.00") : "0.00";
            txtValorAproximadoTributosTotais.Text = (txtTotalTributacaoEstadualTotais.Text.ToDouble() + txtTributacaoFederalTotais.Text.ToDouble()).ToString("0.00");

            //txtInformacoesComplementares.Text = string.Concat("Trib aprox R$: ", txtTributacaoFederalTotais.Text,
                                      //" Federal e " + txtTotalTributacaoEstadualTotais.Text, " Estadual - Fonte: IBPT");
        }

        private bool ValideCalculoIcms(bool validarIsercaoItem = false)
        {
            if(cboEstadoDestinatario.EditValue == string.Empty)
            {
                MessageBox.Show("Para calcular você precisa informar o Estado do destinatário.", "Outras Saídas - Cálculo do Icms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if(txtCodigoCfop.Text == string.Empty)
            {
                MessageBox.Show("Para calcular você precisa informar a CFOP do Item.", "Outras Saídas - Cálculo do Icms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cboOrigem.EditValue == null)
            {
                MessageBox.Show("Para calcular você precisa informar a Origem do Item.", "Outras Saídas - Cálculo do Icms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboOrigem.Focus();
                return false;
            }

            if (cboCstCsosn.EditValue == null)
            {
                MessageBox.Show("Para calcular você precisa informar a CstCson do Item.", "Outras Saídas - Cálculo do Icms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboCstCsosn.Focus();
                return false;
            }

            var cstCsosn = (EnumCstCsosn?)cboCstCsosn.EditValue;

            if (cstCsosn != null && (                
                cstCsosn == EnumCstCsosn.NORMAL70 || cstCsosn == EnumCstCsosn.NORMAL20))
            {
                if (txtPercentualReducaoIcms.Text == string.Empty && txtValorIcms.Text ==string.Empty)
                {
                    MessageBox.Show("Para calcular você precisa informar o Percentual de Redução do Icms do Item.", "Outras Saídas - Cálculo do Icms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPercentualReducaoIcms.Focus();
                    return false;
                }
            }
            
            if (cstCsosn != null && (
                cstCsosn == EnumCstCsosn.NORMAL00 ||                
                cstCsosn == EnumCstCsosn.NORMAL10 ||                
                cstCsosn == EnumCstCsosn.NORMAL20 ||
                cstCsosn == EnumCstCsosn.NORMAL70))
            {  
                if (txtPercentualIcms.Text == string.Empty && txtValorIcms.Text == string.Empty)
                {
                    MessageBox.Show("Para calcular você precisa informar o Percentual Icms do Item.", "Outras Saídas - Cálculo do Icms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPercentualIcms.Focus();
                    return false;
                }

                if (cboModBC.EditValue == null)
                {
                    MessageBox.Show("Para calcular você precisa informar a Modalidade de Base de Cálculo do Item.", "Outras Saídas - Cálculo do Icms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboModBC.Focus();
                    return false;
                }
            }

            if (cstCsosn != null && (                
                cstCsosn == EnumCstCsosn.NORMAL10 ||
                cstCsosn == EnumCstCsosn.NORMAL30 ||
                cstCsosn == EnumCstCsosn.NORMAL70))
            {
                if (txtAliquotaIcmsSt.Text == string.Empty && txtValorIcmsSt.Text == string.Empty)
                {
                    MessageBox.Show("Para calcular você precisa informar a Aliquota Icms St do Item.", "Outras Saídas - Cálculo do Icms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAliquotaIcmsSt.Focus();
                    return false;
                }

                if (cboModBCST.EditValue == null)
                {
                    MessageBox.Show("Para calcular você precisa informar a Modalidade de Base de Cálculo St do Item.", "Outras Saídas - Cálculo do Icms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboModBCST.Focus();
                    return false;
                }
            }
            
            return true;
        }
        
        private void CalculeIcms()
        {
            if (string.IsNullOrEmpty(txtIdProduto.Text))
            {
                return;
            }

            string ufDestino = cboEstadoDestinatario.EditValue != null ? cboEstadoDestinatario.EditValue.ToString() : null;
            EnumTipoCliente tipoCliente = (EnumTipoCliente)pnlNotaPara.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            var tipoInscricaoIcms = new PessoaMetodosAuxiliares().RetorneTipoInscricaoIcms(txtIdParceiroDestinatario.Text.ToInt());

            CalculosPedidoDeVenda calculosPedidoDeVenda = new CalculosPedidoDeVenda();

            double quantidade = 1;
            var descontoTotal = CalculosPedidoDeVenda.CalculeTotalDesconto(txtValorUnitarioItem.Text.ToDouble(),
                                                                                                              quantidade,
                                                                                                              txtValorDescontoItem.Text.ToDouble(),
                                                                                                              rdbDescontoItemPercentual.Checked);

            var itemPedido = new ItemPedidoDeVenda();
            itemPedido.Produto = new Produto { Id = txtIdProduto.Text.ToInt() };
            itemPedido.ValorUnitario = txtValorUnitarioItem.Text.ToDouble();
            itemPedido.Quantidade = txtQuantidadeItem.Text.ToDouble();
            itemPedido.TotalDesconto = descontoTotal;
            itemPedido.ValorFrete = txtValorFreteItem.Text.ToDouble();
            itemPedido.ValorIpi = txtValorIpi.Text.ToDouble();            
            itemPedido.ValorSeguro = txtValorSeguroItem.Text.ToDouble();
            itemPedido.ValorOutrasDespesas = txtValorOutrasDespesasItem.Text.ToDouble();
            itemPedido.TributacaoIcms = new TributacaoIcms();

            if (rdbTipoOperacaoEntrada.Checked)
                itemPedido.TributacaoIcms.TipoSaida = (EnumTipoSaidaTributacaoIcms)rdbTipoOperacaoEntrada.Checked.ToInt();
            if (rdbTipoOperacaoNotaSaida.Checked)
                itemPedido.TributacaoIcms.TipoSaida = (EnumTipoSaidaTributacaoIcms)rdbTipoOperacaoEntrada.Checked.ToInt();

            itemPedido.TributacaoIcms.Cfop = txtCodigoCfop.Text != string.Empty ? new Cfop { Id = txtCodigoCfop.Text.ToInt() } : null;
            
            //Regime Simples Nacional
            if ((EnumCodigoRegimeTributario)cboRegimeTributario.EditValue != EnumCodigoRegimeTributario.REGIMENORMAL)
            {
                itemPedido.TributacaoIcms.CstCsosn = cboCstCsosn.EditValue != null ? (EnumCstCsosn)cboCstCsosn.EditValue : EnumCstCsosn.NORMAL00;                
                itemPedido.TributacaoIcms.MVA = txtPercentualMVA.Text != string.Empty ? txtPercentualMVA.Text.ToDouble() : 0;
                itemPedido.TributacaoIcms.aliquotaSimplesNacional = txtAliquotaSimplesNacional.Text != string.Empty ? txtAliquotaSimplesNacional.Text.ToDouble() : 0;
                itemPedido.TributacaoIcms.IcmsReducaoBaseCalculo = txtPercentualReducaoIcms.Text != string.Empty ? txtPercentualReducaoIcms.Text.ToDouble() : 0;
                itemPedido.TributacaoIcms.IcmsBaseCalculo = txtPercentualIcms.Text != string.Empty ? txtPercentualIcms.Text.ToDouble() : 0;

                itemPedido.TributacaoIcms.AliquotaCreditoST = txtAliquotaCrSt.Text != string.Empty ? txtAliquotaCrSt.Text.ToDouble() : 0;
                itemPedido.TributacaoIcms.AliquotaDebitoST = txtAliquotaDbSt.Text != string.Empty ? txtAliquotaDbSt.Text.ToDouble() : 0;
                itemPedido.TributacaoIcms.ReducaoBaseST = txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text != string.Empty ? txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text.ToDouble() : 0;
                                
                calculosPedidoDeVenda.DefinaIcmsST(itemPedido, ufDestino, tipoCliente, tipoInscricaoIcms);
            }

            //Regime Normal
            else
            {
                itemPedido.TributacaoIcms.CstCsosn = cboCstCsosn.EditValue != null ? (EnumCstCsosn?)cboCstCsosn.EditValue : null;              
                
                itemPedido.TributacaoIcms.PercentualMargemAdicST = txtPercentualMargemAdicST.Text != string.Empty? txtPercentualMargemAdicST.Text.ToDouble(): 0;

                itemPedido.TributacaoIcms.AliquotaIcmsST = txtAliquotaIcmsSt.Text != string.Empty ? txtAliquotaIcmsSt.Text.ToDouble() : 0;
                
                itemPedido.TributacaoIcms.ReducaoBaseST = txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text != string.Empty ? txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text.ToDouble() : 0;
                itemPedido.TributacaoIcms.IcmsBaseCalculo = txtPercentualIcms.Text != string.Empty ? txtPercentualIcms.Text.ToDouble() : 0;
                itemPedido.TributacaoIcms.IcmsReducaoBaseCalculo = txtPercentualReducaoIcms.Text != string.Empty ? txtPercentualReducaoIcms.Text.ToDouble() : 0;
                
                calculosPedidoDeVenda.DefinaIcmsRegimeNormal(itemPedido, ufDestino, tipoCliente, tipoInscricaoIcms);
            }
            
             cboCstCsosn.EditValue = (EnumCstCsosn)itemPedido.TributacaoIcms.CstCsosn;
            //cboModBC.EditValue = ;           
            //txtPercentualReducaoIcms.Text = ;
            //txtPercentualIcms.Text = ;            
            //cboMotivoDesoneracao.Text = ;
            //txtIcmsDesoneracaoProduto.Text =;
            //cboModBCST.Text = ;

            txtValorIcms.Text = itemPedido.ValorIcms != null ? itemPedido.ValorIcms.Value.ToString("0.00") : txtValorIcms.Text;
            txtBaseIcms.Text = itemPedido.TributacaoIcms.baseDeCalculoIcms != null & itemPedido.TributacaoIcms.baseDeCalculoIcms.ToDouble() != 0? itemPedido.TributacaoIcms.baseDeCalculoIcms.Value.ToString("0.00") : txtBaseIcms.Text;
            txtPercentualMargemAdicST.Text = itemPedido.TributacaoIcms.PercentualMargemAdicST != null ? itemPedido.TributacaoIcms.PercentualMargemAdicST.Value.ToString("0.00") : txtPercentualMargemAdicST.Text;
            txtPercentualMVA.Text = itemPedido.TributacaoIcms.MVA !=null? itemPedido.TributacaoIcms.MVA.Value.ToString("0.00"): string.Empty;
            txtPercentualReducaoIcms.Text = itemPedido.TributacaoIcms.IcmsReducaoBaseCalculo != null? itemPedido.TributacaoIcms.IcmsReducaoBaseCalculo.Value.ToString("0.00"): txtPercentualReducaoIcms.Text;
            txtBaseIcmsSt.Text = itemPedido.TributacaoIcms.baseDeCalculoIcmsST !=null & itemPedido.TributacaoIcms.baseDeCalculoIcmsST != 0? itemPedido.TributacaoIcms.baseDeCalculoIcmsST.Value.ToString("0.00"): txtBaseIcmsSt.Text;

            txtAliquotaDbSt.Text = itemPedido.TributacaoIcms.AliquotaDebitoST != null ? itemPedido.TributacaoIcms.AliquotaDebitoST.Value.ToString("0.00") : string.Empty;
            txtAliquotaCrSt.Text = itemPedido.TributacaoIcms.AliquotaCreditoST != null ? itemPedido.TributacaoIcms.AliquotaCreditoST.Value.ToString("0.00") : string.Empty;

            txtAliquotaIcmsSt.Text = itemPedido.TributacaoIcms.AliquotaIcmsST != null ? itemPedido.TributacaoIcms.AliquotaIcmsST.Value.ToString("0.00") : string.Empty;

            txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text = itemPedido.TributacaoIcms.ReducaoBaseST != null ? itemPedido.TributacaoIcms.ReducaoBaseST.Value.ToString("0.00") : txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text;

            txtPercentualIcms.Text = itemPedido.TributacaoIcms.IcmsBaseCalculo != null ? itemPedido.TributacaoIcms.IcmsBaseCalculo.Value.ToString("0.00") : txtPercentualIcms.Text;

            txtAliquotaSimplesNacional.Text = itemPedido.TributacaoIcms.aliquotaSimplesNacional != null? itemPedido.TributacaoIcms.aliquotaSimplesNacional.Value.ToString("0.00"):string.Empty;
            txtValorIcmsSt.Text = itemPedido.ValorIcmsST != null ? itemPedido.ValorIcmsST.Value.ToString("0.00") : txtValorIcmsSt.Text;
            //Percentual Diferimento
            //Valor Icms diferido
            //txtAliquotaSimplesNacional.Text = itemPedido.aliquotaSimplesNacional!= null? itemPedido.aliquotaSimplesNacional.Value.ToString("0.00"):string.Empty;
            if (itemPedido.TributacaoIcms.Cfop != null)
            {
                //_listaCfops.Add(itemPedido.TributacaoIcms.Cfop);                    
                //AltereCboCfop();
                txtCodigoCfop.Text = itemPedido.TributacaoIcms.Cfop.Id.ToString();
            }
            
            //****Criar outro método para visualizar item por item 

            //Calcula Aliquota Simples Nacional
            if ((EnumCodigoRegimeTributario)cboRegimeTributario.EditValue != EnumCodigoRegimeTributario.REGIMENORMAL)
                CalculeAliquotaSimplesNacional((EnumCstCsosn)itemPedido.TributacaoIcms.CstCsosn, descontoTotal);

            double valorTotalItem = calculosPedidoDeVenda.RetorneValorTotalItem(itemPedido.ValorUnitario,
                                                                                                                       itemPedido.Quantidade,
                                                                                                                       descontoTotal,
                                                                                                                       itemPedido.ValorFrete,
                                                                                                                       itemPedido.ValorIpi,
                                                                                                                       txtValorIcmsSt.Text.ToDoubleNullabel(),
                                                                                                                       itemPedido.ValorSeguro,
                                                                                                                       itemPedido.ValorOutrasDespesas);
            txtValorTotalItem.Text = valorTotalItem.ToString("0.00");
        }

        private void PreenchaCamposDoProduto(Produto produto)
        {
            _produtoEmEdicao = produto;

            if (produto != null)
            {
                txtIdProduto.Text = produto.Id.ToString();
                txtCodigoDeBarrasProduto.Text = produto.DadosGerais.CodigoDeBarras;
                txtItemDescricaoProduto.Text = produto.DadosGerais.Descricao;
                txtUnidade.Text = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Abreviacao : string.Empty;
                txtCodigoEanItem.Text = produto.ContabilFiscal.CodigoGtin;
                //txtCodigoCfop.Text = produto

                if (produto.ContabilFiscal != null && produto.ContabilFiscal.Ncm != null)
                {
                    txtCodigoNcm.Text = produto.ContabilFiscal.Ncm.CodigoNcm;
                    txtNcmDescricao.Text = produto.ContabilFiscal.Ncm.Descricao;
                }
                else
                {
                    txtCodigoNcm.Text = string.Empty;
                    txtNcmDescricao.Text = string.Empty;
                }

                cboOrigem.EditValue = produto.ContabilFiscal.OrigemProduto;                
                txtValorUnitarioItem.Text = produto.FormacaoPreco.EhPromocao.GetValueOrDefault() ? produto.FormacaoPreco.ValorPromocao.GetValueOrDefault().ToString("0.00") : produto.FormacaoPreco.ValorVenda.GetValueOrDefault().ToString("0.00");

                AltereMascaraQuantidadeProduto();

                cboOrigem.Focus();
            }
            else
            {
                txtIdProduto.Text = string.Empty;
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtItemDescricaoProduto.Text = string.Empty;
                txtUnidade.Text = string.Empty;

                txtCodigoEanItem.Text = string.Empty;

                txtCodigoNcm.Text = string.Empty;
                txtNcmDescricao.Text = string.Empty;
            }
        }

        private void HabiliteOuDesabiliteCamposIcms()
        {
            txtAliquotaSimplesNacional.Properties.ReadOnly = true;
            txtAliquotaSimplesNacional.TabStop = false;
            txtAliquotaSimplesNacional.Text = string.Empty;

            txtValorIcmsSimplesNacional.Properties.ReadOnly = true;
            txtValorIcmsSimplesNacional.TabStop = false;
            txtValorIcmsSimplesNacional.Text = string.Empty;

            var cstCsosn = (EnumCstCsosn?)cboCstCsosn.EditValue;

            if (cstCsosn != null && (
                cstCsosn == EnumCstCsosn.NORMAL00 ||
                cstCsosn == EnumCstCsosn.NORMAL90 ||
                cstCsosn == EnumCstCsosn.NORMAL10 ||
                cstCsosn == EnumCstCsosn.NORMAL51 ||                
                cstCsosn == EnumCstCsosn.NORMAL20 ||
                cstCsosn == EnumCstCsosn.NORMAL70 ||
                cstCsosn == EnumCstCsosn.SIMPLES900))
            {
                cboModBC.Properties.ReadOnly = false;
                txtPercentualIcms.Properties.ReadOnly = false;

                PreenchaCboModBC();
                                               
                cboModBC.TabStop = true;
                txtPercentualIcms.TabStop = true;

                if (cstCsosn == EnumCstCsosn.NORMAL00 ||
                    cstCsosn == EnumCstCsosn.NORMAL10 ||
                    cstCsosn == EnumCstCsosn.NORMAL20 ||
                    cstCsosn == EnumCstCsosn.NORMAL70 ||
                    cstCsosn == EnumCstCsosn.NORMAL90 ||
                    cstCsosn == EnumCstCsosn.NORMAL51 ||
                    cstCsosn == EnumCstCsosn.SIMPLES900)
                {
                    txtBaseIcms.Properties.ReadOnly = false;
                    txtValorIcms.Properties.ReadOnly = false;

                    txtBaseIcms.TabStop = true;
                    txtValorIcms.TabStop = true;
                }
                else
                {   
                    txtBaseIcms.Properties.ReadOnly = true;
                    txtValorIcms.Properties.ReadOnly = true;
                   
                    txtBaseIcms.TabStop = false;
                    txtValorIcms.TabStop = false;
                                       
                    txtBaseIcms.Text = string.Empty;
                    txtValorIcms.Text = string.Empty;
                }

                //**Somente os campos com Redução
                if (cstCsosn == EnumCstCsosn.NORMAL10 ||
                    cstCsosn == EnumCstCsosn.NORMAL20 ||
                    cstCsosn == EnumCstCsosn.NORMAL70 ||
                    cstCsosn == EnumCstCsosn.NORMAL90 ||
                    cstCsosn == EnumCstCsosn.NORMAL51 ||
                    cstCsosn == EnumCstCsosn.SIMPLES900)
                {
                    txtPercentualReducaoIcms.Properties.ReadOnly = false;
                    txtPercentualReducaoIcms.TabStop = true;
                }
                else
                {
                    txtPercentualReducaoIcms.Properties.ReadOnly = true;
                    txtPercentualReducaoIcms.TabStop = false;
                    txtPercentualReducaoIcms.Text = string.Empty;
                }
                
                    if (cstCsosn == EnumCstCsosn.NORMAL51)
                {
                    txtPercentualDiferimento.Properties.ReadOnly = false;

                    txtPercentualDiferimento.TabStop = true;

                    txtValorIcmsDiferido.Properties.ReadOnly = false;

                    txtValorIcmsDiferido.TabStop = true;
                }
                else
                {
                    txtPercentualDiferimento.Properties.ReadOnly = true;
                    txtPercentualDiferimento.TabStop = false;
                    txtPercentualDiferimento.Text = string.Empty;
                    txtValorIcmsDiferido.Properties.ReadOnly = true;
                    txtValorIcmsDiferido.TabStop = false;
                    txtValorIcmsDiferido.Text = string.Empty;
                }
            }
            else
            {
                cboModBC.Properties.ReadOnly = true;                
                txtPercentualIcms.Properties.ReadOnly = true;
                txtPercentualReducaoIcms.Properties.ReadOnly = true;
                cboModBC.EditValue = null;
                cboModBC.TabStop = false;
                txtPercentualIcms.TabStop = false;
                txtPercentualReducaoIcms.TabStop = false;

                txtPercentualDiferimento.Properties.ReadOnly = true;
                txtPercentualDiferimento.TabStop = false;
                txtPercentualDiferimento.Text = string.Empty;
                txtValorIcmsDiferido.Properties.ReadOnly = true;
                txtValorIcmsDiferido.TabStop = false;
                txtValorIcmsDiferido.Text = string.Empty;
            }

            if ((EnumCodigoRegimeTributario)cboRegimeTributario.EditValue != EnumCodigoRegimeTributario.REGIMENORMAL)
            {
                if (cstCsosn != null && (
                            cstCsosn == EnumCstCsosn.SIMPLES900))
                {   
                    txtPercentualReducaoIcms.Properties.ReadOnly = false;
                    txtPercentualIcms.Properties.ReadOnly = false;
                    
                    txtPercentualReducaoIcms.TabStop = true;
                    txtPercentualIcms.TabStop = true;
                }
                else
                {

                    txtAliquotaSimplesNacional.Properties.ReadOnly = true;
                    txtAliquotaSimplesNacional.TabStop = false;
                    txtAliquotaSimplesNacional.Text = string.Empty;

                    txtValorIcmsSimplesNacional.Properties.ReadOnly = true;
                    txtValorIcmsSimplesNacional.TabStop = false;
                    txtValorIcmsSimplesNacional.Text = string.Empty;

                    txtPercentualReducaoIcms.Text = string.Empty;
                    txtPercentualIcms.Text = string.Empty;
                   
                    txtPercentualReducaoIcms.Properties.ReadOnly = true;
                    txtPercentualIcms.Properties.ReadOnly = true;
                    
                    txtPercentualReducaoIcms.TabStop = false;
                    txtPercentualIcms.TabStop = false;
                }

                if (cstCsosn != null && (
                            cstCsosn == EnumCstCsosn.SIMPLES101 ||
                            cstCsosn == EnumCstCsosn.SIMPLES201 ||
                            cstCsosn == EnumCstCsosn.SIMPLES900))
                {
                    txtAliquotaSimplesNacional.Properties.ReadOnly = false;
                    txtAliquotaSimplesNacional.TabStop = true;

                    txtValorIcmsSimplesNacional.Properties.ReadOnly = false;
                    txtValorIcmsSimplesNacional.TabStop = true;

                    if (cstCsosn == EnumCstCsosn.SIMPLES101 ||
                            cstCsosn == EnumCstCsosn.SIMPLES201)
                    {
                        txtBaseIcms.Properties.ReadOnly = true;
                        txtValorIcms.Properties.ReadOnly = true;

                        txtBaseIcms.TabStop = false;
                        txtValorIcms.TabStop = false;
                    }
                }

            }
        }
                
        private void HabiliteOuDesabiliteCamposIcmsDesoneracao()
        {
            var cstCsosn = (EnumCstCsosn?)cboCstCsosn.EditValue;

            if (cstCsosn != null && (
                cstCsosn == EnumCstCsosn.NORMAL20 ||
                cstCsosn == EnumCstCsosn.NORMAL30 ||
                cstCsosn == EnumCstCsosn.NORMAL40 ||
                cstCsosn == EnumCstCsosn.NORMAL41 ||
                cstCsosn == EnumCstCsosn.NORMAL50 ||
                cstCsosn == EnumCstCsosn.NORMAL70 ||
                cstCsosn == EnumCstCsosn.NORMAL90))
            {
                txtIcmsDesoneracaoProduto.Properties.ReadOnly = false;
                cboMotivoDesoneracao.Properties.ReadOnly = false;

                PreenchaCboMotivoDesoneracao();

                txtIcmsDesoneracaoProduto.TabStop = true;
                cboMotivoDesoneracao.TabStop = true;
            }
            else
            {
                txtIcmsDesoneracaoProduto.Properties.ReadOnly = true;
                cboMotivoDesoneracao.Properties.ReadOnly = true;

                txtIcmsDesoneracaoProduto.TabStop = false;
                cboMotivoDesoneracao.TabStop = false;

                txtIcmsDesoneracaoProduto.Text = string.Empty;
                cboMotivoDesoneracao.EditValue = null;
            }

            if (cstCsosn != null && (
                cstCsosn == EnumCstCsosn.NORMAL50 ||
                cstCsosn == EnumCstCsosn.NORMAL60))
            {
                txtBaseIcms.Properties.ReadOnly = true;
                txtValorIcms.Properties.ReadOnly = true;

                txtBaseIcms.TabStop = false;
                txtValorIcms.TabStop = false;               
            }

        }

        private void HabiliteOuDesabiliteCamposST()
        {
            txtAliquotaIcmsSt.Properties.ReadOnly = true;
            txtAliquotaIcmsSt.TabStop = false;

            var cstCsosn = (EnumCstCsosn?)cboCstCsosn.EditValue;

            if (cstCsosn != null && (
                cstCsosn == EnumCstCsosn.SIMPLES201 ||
                cstCsosn == EnumCstCsosn.SIMPLES202 ||
                cstCsosn == EnumCstCsosn.SIMPLES203 ||
                cstCsosn == EnumCstCsosn.SIMPLES900 ||

                cstCsosn == EnumCstCsosn.NORMAL10 ||
                cstCsosn == EnumCstCsosn.NORMAL60 ||
                cstCsosn == EnumCstCsosn.NORMAL30 ||

                cstCsosn == EnumCstCsosn.NORMAL70 ||
                cstCsosn == EnumCstCsosn.NORMAL90))
            {
                cboModBCST.Properties.ReadOnly = false;
                                               
                txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Properties.ReadOnly = false;
                txtPercentualReducaoBaseCalculoSubstituicaoTributaria.TabStop = true;

                PreenchaCboModBCST();

                cboModBCST.TabStop = true;

                txtBaseIcmsSt.Properties.ReadOnly = false;
                txtValorIcmsSt.Properties.ReadOnly = false;

                txtBaseIcmsSt.TabStop = true;
                txtValorIcmsSt.TabStop = true;

                if ((EnumCodigoRegimeTributario)cboRegimeTributario.EditValue == EnumCodigoRegimeTributario.REGIMENORMAL)
                {
                    txtAliquotaIcmsSt.Properties.ReadOnly = false;
                    txtAliquotaIcmsSt.TabStop = true;

                    if (cstCsosn == EnumCstCsosn.NORMAL60)
                    {
                        txtAliquotaIcmsSt.Properties.ReadOnly = true;
                        txtAliquotaIcmsSt.TabStop = false;
                        txtAliquotaIcmsSt.Text = string.Empty;

                        cboModBC.Properties.ReadOnly = true;                        
                        cboModBC.EditValue = null;
                        cboModBC.TabStop = false;

                        cboModBCST.Properties.ReadOnly = true;                        
                        cboModBCST.EditValue = null;                        
                        cboModBCST.TabStop = false;
                    }
                    if(cstCsosn == EnumCstCsosn.NORMAL30)
                    {   
                        cboModBC.EditValue = null;
                       
                        txtBaseIcms.Text = string.Empty;
                        txtPercentualReducaoIcms.Text = string.Empty;
                        txtPercentualIcms.Text = string.Empty;
                        txtValorIcms.Text = string.Empty;

                        txtBaseIcms.Properties.ReadOnly = true;
                        txtValorIcms.Properties.ReadOnly = true;

                        txtBaseIcms.TabStop = false;
                        txtValorIcms.TabStop = false;
                    }
                }
                else
                {   
                    txtPercentualMVA.Properties.ReadOnly = false;
                    txtAliquotaCrSt.Properties.ReadOnly = false;
                    txtAliquotaDbSt.Properties.ReadOnly = false;
                    txtAliquotaCrSt.TabStop = true;
                    txtAliquotaDbSt.TabStop = true;
                    txtPercentualMVA.TabStop = true;
                }
                
                if (cstCsosn == EnumCstCsosn.NORMAL10 ||
                    cstCsosn == EnumCstCsosn.NORMAL70 ||
                    cstCsosn == EnumCstCsosn.NORMAL90 ||
                    cstCsosn == EnumCstCsosn.NORMAL30)
                {   
                    txtPercentualReducaoBaseCalculoSubstituicaoTributaria.ReadOnly = false;
                    txtPercentualReducaoBaseCalculoSubstituicaoTributaria.TabStop = true;
                    txtPercentualMVA.Properties.ReadOnly = true;
                    txtPercentualMVA.TabStop = false;
                    txtPercentualMVA.Text = string.Empty;

                    txtPercentualMargemAdicST.Properties.ReadOnly = false;
                    txtPercentualMargemAdicST.TabStop = true;
                    txtPercentualMargemAdicST.Text = string.Empty;
                }
                else
                {
                    if (cstCsosn != EnumCstCsosn.SIMPLES201 & cstCsosn != EnumCstCsosn.SIMPLES202 & cstCsosn != EnumCstCsosn.SIMPLES203 & cstCsosn != EnumCstCsosn.SIMPLES900)
                    {
                        txtPercentualReducaoBaseCalculoSubstituicaoTributaria.ReadOnly = true;
                        txtPercentualReducaoBaseCalculoSubstituicaoTributaria.TabStop = false;
                        txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text = string.Empty;
                    }

                    txtPercentualMargemAdicST.Properties.ReadOnly = true;
                    txtPercentualMargemAdicST.TabStop = false;
                    txtPercentualMargemAdicST.Text = string.Empty;
                }
            }
            else
            {
                cboModBCST.Properties.ReadOnly = true;                
                txtPercentualMVA.Properties.ReadOnly = true;
               
                cboModBCST.EditValue = null;
                txtPercentualMVA.Text = string.Empty;
               
                cboModBCST.TabStop = false;
                txtPercentualMVA.TabStop = false;
               
                txtPercentualReducaoBaseCalculoSubstituicaoTributaria.ReadOnly = true;
                txtPercentualReducaoBaseCalculoSubstituicaoTributaria.TabStop = false;
                txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text = string.Empty;

                txtPercentualMargemAdicST.Properties.ReadOnly = true;
                txtPercentualMargemAdicST.TabStop = false;
                txtPercentualMargemAdicST.Text = string.Empty;

                txtBaseIcmsSt.Properties.ReadOnly = true;
                txtValorIcmsSt.Properties.ReadOnly = true;

                txtBaseIcmsSt.TabStop = false;
                txtValorIcmsSt.TabStop = false;

                txtBaseIcmsSt.Text = string.Empty;
                txtValorIcmsSt.Text = string.Empty;

                if ((EnumCodigoRegimeTributario)cboRegimeTributario.EditValue == EnumCodigoRegimeTributario.REGIMENORMAL)
                {
                    txtAliquotaIcmsSt.Properties.ReadOnly = true;

                    txtAliquotaIcmsSt.TabStop = false;
                    
                    txtAliquotaIcmsSt.Text = string.Empty;
                }
                else
                {
                    txtAliquotaDbSt.Properties.ReadOnly = true;
                    txtAliquotaCrSt.Properties.ReadOnly = true;

                    txtAliquotaDbSt.Text = string.Empty;
                    txtAliquotaCrSt.Text = string.Empty;

                    txtAliquotaDbSt.TabStop = false;
                    txtAliquotaCrSt.TabStop = false;

                    if (cstCsosn == EnumCstCsosn.SIMPLES500)
                    {
                        txtBaseIcmsSt.Properties.ReadOnly = false;
                        txtValorIcmsSt.Properties.ReadOnly = false;

                        txtBaseIcmsSt.TabStop = true;
                        txtValorIcmsSt.TabStop = true;
                    }
                }                
            }           
        }

        private void HabiliteOuDesabiliteCstIpi()
        {
            var cstIpi = (EnumCstIpi?)cboCstIpi.EditValue;

            if (cstIpi != null && (
                cstIpi == EnumCstIpi.IPI00 ||
                cstIpi == EnumCstIpi.IPI49 ||
                cstIpi == EnumCstIpi.IPI50 ||
                cstIpi == EnumCstIpi.IPI99 ))
            {
                txtPercentualIpi.Properties.ReadOnly = false;
                txtPercentualIpi.TabStop = true;
                txtValorIpi.Properties.ReadOnly = false;
                txtValorIpi.TabStop = true;                
            }
            else
            {
                txtPercentualIpi.Properties.ReadOnly = true;
                txtPercentualIpi.TabStop = false;
                txtValorIpi.Properties.ReadOnly = true;
                txtValorIpi.TabStop = false;

                txtPercentualIpi.Text = string.Empty;
                txtValorIpi.Text = string.Empty;
            }
        }

        private void HabiliteOuDesabiliteCstPis()
        {
            var cstPis = (EnumCstPis?)cboCstPis.EditValue;
            
            //Limpa todos os campos
            txtValorBaseCalculoPis.Text = string.Empty;        
            txtAliquotaPisPercentual.Text = string.Empty;
            txtAliquotaPisReais.Text = string.Empty;
            txtQuantidadeVendidaPis.Text = string.Empty;
            txtValorPis.Text = string.Empty;

            txtBaseCalculoPisST.Text = string.Empty;
            txtAliquotaPisSTPercentual.Text = string.Empty;
            txtAliquotaPisSTReais.Text = string.Empty;                        
            txtQuantidadeVendidaPisST.Text = string.Empty;
            txtValorPisST.Text = string.Empty;
            
            //Desabilita todos os campos
            //BC
            txtValorBaseCalculoPis.Properties.ReadOnly = true;
            txtAliquotaPisPercentual.Properties.ReadOnly = true;
            txtAliquotaPisReais.Properties.ReadOnly = true;
            txtQuantidadeVendidaPis.Properties.ReadOnly = true;
            txtValorPis.Properties.ReadOnly = true;

            txtValorBaseCalculoPis.TabStop = false; 
            txtAliquotaPisPercentual.TabStop = false;
            txtAliquotaPisReais.TabStop = false;
            txtQuantidadeVendidaPis.TabStop = false;
            txtValorPis.TabStop = false;

            //ST
            txtBaseCalculoPisST.Properties.ReadOnly = true;
            txtAliquotaPisSTPercentual.Properties.ReadOnly = true;
            txtAliquotaPisSTReais.Properties.ReadOnly = true;
            txtQuantidadeVendidaPisST.Properties.ReadOnly = true;
            txtValorPisST.Properties.ReadOnly = true;

            txtBaseCalculoPisST.TabStop = false;
            txtAliquotaPisSTPercentual.TabStop = false;
            txtAliquotaPisSTReais.TabStop = false;
            txtQuantidadeVendidaPisST.TabStop = false;
            txtValorPisST.TabStop = false;

            if (cstPis != null && (
                cstPis == EnumCstPis.pis01 ||
                cstPis == EnumCstPis.pis02))
            {                
                txtAliquotaPisPercentual.Properties.ReadOnly = false;
                txtAliquotaPisPercentual.TabStop = true;              
            }
            else if (cstPis != null && (
                cstPis == EnumCstPis.pis03))
            {
                txtAliquotaPisReais.Properties.ReadOnly = false;
                txtAliquotaPisReais.TabStop = true;
            }
            else if (cstPis != null && (
                cstPis == EnumCstPis.pis05))
            {               
                txtAliquotaPisSTPercentual.Properties.ReadOnly = false;
                txtAliquotaPisSTReais.Properties.ReadOnly = false;
               
                txtAliquotaPisSTPercentual.TabStop = true;
                txtAliquotaPisSTReais.TabStop = true;
            }
            else if (cstPis != null && (
                cstPis == EnumCstPis.pis49 ||
                cstPis == EnumCstPis.pis50 ||
                cstPis == EnumCstPis.pis51 ||
                cstPis == EnumCstPis.pis52 ||
                cstPis == EnumCstPis.pis53 ||
                cstPis == EnumCstPis.pis54 ||
                cstPis == EnumCstPis.pis55 ||
                cstPis == EnumCstPis.pis56 ||
                cstPis == EnumCstPis.pis60 ||
                cstPis == EnumCstPis.pis61 ||
                cstPis == EnumCstPis.pis62 ||
                cstPis == EnumCstPis.pis63 ||
                cstPis == EnumCstPis.pis64 ||
                cstPis == EnumCstPis.pis65 ||
                cstPis == EnumCstPis.pis66 ||
                cstPis == EnumCstPis.pis67 ||
                cstPis == EnumCstPis.pis70 ||
                cstPis == EnumCstPis.pis71 ||
                cstPis == EnumCstPis.pis72 ||
                cstPis == EnumCstPis.pis73 ||
                cstPis == EnumCstPis.pis74 ||
                cstPis == EnumCstPis.pis75 ||
                cstPis == EnumCstPis.pis98 ||
                cstPis == EnumCstPis.pis99
                ))
            {                
                txtAliquotaPisPercentual.Properties.ReadOnly = false;
                txtAliquotaPisReais.Properties.ReadOnly = false;
                
                txtAliquotaPisPercentual.TabStop = true;
                txtAliquotaPisReais.TabStop = true;                              
            }
            }

        private void HabiliteOuDesabiliteCstCofins()
        {
            var cstCofins = (EnumCstCofins?)cboCstCofins.EditValue;

            //Limpa todos os campos
            txtBaseCalculoCofins.Text = string.Empty;
            txtAliquotaCofinsPercentual.Text = string.Empty;
            txtAliquotaCofinsReais.Text = string.Empty;
            txtQuantidadeVendidaCofins.Text = string.Empty;
            txtValorCofins.Text = string.Empty;

            txtBaseCalculoCofinsST.Text = string.Empty;
            txtAliquotaCofinsSTPercentual.Text = string.Empty;
            txtAliquotaCofinsSTReais.Text = string.Empty;
            txtQuantidadeVendidaCofinsST.Text = string.Empty;
            txtValorCofinsST.Text = string.Empty;

            //Desabilita todos os campos
            //BC
            txtBaseCalculoCofins.Properties.ReadOnly = true;
            txtAliquotaCofinsPercentual.Properties.ReadOnly = true;
            txtAliquotaCofinsReais.Properties.ReadOnly = true;
            txtQuantidadeVendidaCofins.Properties.ReadOnly = true;
            txtValorCofins.Properties.ReadOnly = true;

            txtBaseCalculoCofins.TabStop = false;
            txtAliquotaCofinsPercentual.TabStop = false;
            txtAliquotaCofinsReais.TabStop = false;
            txtQuantidadeVendidaCofins.TabStop = false;
            txtValorCofins.TabStop = false;

            //ST
            txtBaseCalculoCofinsST.Properties.ReadOnly = true;
            txtAliquotaCofinsSTPercentual.Properties.ReadOnly = true;
            txtAliquotaCofinsSTReais.Properties.ReadOnly = true;
            txtQuantidadeVendidaCofinsST.Properties.ReadOnly = true;
            txtValorCofinsST.Properties.ReadOnly = true;

            txtBaseCalculoCofinsST.TabStop = false;
            txtAliquotaCofinsSTPercentual.TabStop = false;
            txtAliquotaCofinsSTReais.TabStop = false;
            txtQuantidadeVendidaCofinsST.TabStop = false;            
            txtValorCofinsST.TabStop = false;

            if (cstCofins != null && (
                cstCofins == EnumCstCofins.cofins01 ||
                cstCofins == EnumCstCofins.cofins02))
            {               
                txtAliquotaCofinsPercentual.Properties.ReadOnly = false;
                txtAliquotaCofinsPercentual.TabStop = true;               
            }
            else if (cstCofins != null && (
                cstCofins == EnumCstCofins.cofins03))
            {
                txtAliquotaCofinsReais.Properties.ReadOnly = false;
                txtAliquotaCofinsReais.TabStop = true;                
            }
            else if (cstCofins != null && (
                cstCofins == EnumCstCofins.cofins05))
            {  
                txtAliquotaCofinsSTPercentual.Properties.ReadOnly = false;
                txtAliquotaCofinsSTReais.Properties.ReadOnly = false;
               
                txtAliquotaCofinsSTPercentual.TabStop = true;
                txtAliquotaCofinsSTReais.TabStop = true;
            }
            else if (cstCofins != null && (
                cstCofins == EnumCstCofins.cofins49 ||
                cstCofins == EnumCstCofins.cofins50 ||
                cstCofins == EnumCstCofins.cofins51 ||
                cstCofins == EnumCstCofins.cofins52 ||
                cstCofins == EnumCstCofins.cofins53 ||
                cstCofins == EnumCstCofins.cofins54 ||
                cstCofins == EnumCstCofins.cofins55 ||
                cstCofins == EnumCstCofins.cofins56 ||
                cstCofins == EnumCstCofins.cofins60 ||
                cstCofins == EnumCstCofins.cofins61 ||
                cstCofins == EnumCstCofins.cofins62 ||
                cstCofins == EnumCstCofins.cofins63 ||
                cstCofins == EnumCstCofins.cofins64 ||
                cstCofins == EnumCstCofins.cofins65 ||
                cstCofins == EnumCstCofins.cofins66 ||
                cstCofins == EnumCstCofins.cofins67 ||
                cstCofins == EnumCstCofins.cofins70 ||
                cstCofins == EnumCstCofins.cofins71 ||
                cstCofins == EnumCstCofins.cofins72 ||
                cstCofins == EnumCstCofins.cofins73 ||
                cstCofins == EnumCstCofins.cofins74 ||
                cstCofins == EnumCstCofins.cofins75 ||
                cstCofins == EnumCstCofins.cofins98 ||
                cstCofins == EnumCstCofins.cofins99
                ))
            {   
                txtAliquotaCofinsPercentual.Properties.ReadOnly = false;
                txtAliquotaCofinsReais.Properties.ReadOnly = false;
                
                txtAliquotaCofinsPercentual.TabStop = true;
                txtAliquotaCofinsReais.TabStop = true;
            }
        }

        private void CarregarParticularidadesDoRegime()
        {
            if ((EnumCodigoRegimeTributario)cboRegimeTributario.EditValue == EnumCodigoRegimeTributario.REGIMENORMAL)
            {
                lblAliquotaCrSt.Visible = false;
                lblAliquotaDbSt.Visible = false;
                txtAliquotaDbSt.Visible = false;
                txtAliquotaCrSt.Visible = false;

                txtAliquotaIcmsSt.Visible = true;
                lblAliquotaIcmsSt.Visible = true;
            }
            else
            {
                lblAliquotaCrSt.Visible = true;
                lblAliquotaDbSt.Visible = true;
                txtAliquotaDbSt.Visible = true;
                txtAliquotaCrSt.Visible = true;

                txtAliquotaIcmsSt.Visible = false;
                lblAliquotaIcmsSt.Visible = false;
            }
        }

        private void InsiraOuAtualizeItemNaLista()
        {
            //if (rdbTipoOperacaoNotaSaida.Checked == true)
            //{
            //    ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
            //    var ItemTransferencia = servicoItemTransferencia.ConsulteProduto(txtIdProduto.Text.ToInt());




            //    ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
            //    bool ReserveEstoqueAoFaturarPedido = _parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido;


            //    if (servicoPedido.VerifiqueItemQuantidadeEstoqueNegativo(txtQuantidadeItem.Text.ToDouble(), _produtoEmEdicao, ReserveEstoqueAoFaturarPedido))

            //    {
            //        if (ItemTransferencia.Count > 0)
            //        {
            //            ServicoSubEstoque servicoSubEstoque = new ServicoSubEstoque();
            //            var subestoque = servicoSubEstoque.Consulte(ItemTransferencia[0].SubEstoque);

            //            MessageBox.Show("Este item encontra-se no seguinte subestoque: " + subestoque.Descricao + " "
            //                             + "Produto não está disponível!",
            //                            "Verifique o estoque", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return;
            //        }
            //        else
            //            MessageBox.Show("O estoque do seguinte item: " + _produtoEmEdicao.Id + " - " + _produtoEmEdicao.DadosGerais.Descricao
            //                            + ". Pode estar zerado, Reservado ou a quantidade requerida não está disponível!",
            //                            "Verifique o estoque!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}
            Action actionInserirItem = () =>
            {
                var itemNotaFiscal = RetorneItemNotaFiscalEmEdicao();

                //ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                //servicoEntradaMercadoria.ValideItem(itemNotaFiscal);

                if (_itemNotaFiscalEmEdicao != null)
                {
                    int indexItem = _listaItensNotaFiscal.IndexOf(_itemNotaFiscalEmEdicao);

                    _listaItensNotaFiscal.Remove(_itemNotaFiscalEmEdicao);

                    _listaItensNotaFiscal.Insert(indexItem, itemNotaFiscal);

                    //CFOP para todos os itens
                    //if(chkTodosCFOPs.Checked)
                    {
                        foreach (var item in _listaItensNotaFiscal)
                        {
                            item.Cfop = itemNotaFiscal.Cfop;
                        }
                    }

                    //CST para todos os itens
                    if (chkTodosCSTs.Checked)
                    {
                        foreach (var item in _listaItensNotaFiscal)
                        {
                            item.Impostos.Icms.CstCsosn = itemNotaFiscal.Impostos.Icms.CstCsosn;
                        }
                    }

                    //IPI para todos os itens
                    if (chkTodosIPI.Checked)
                    {
                        foreach (var item in _listaItensNotaFiscal)
                        {
                            item.Impostos.Ipi = itemNotaFiscal.Impostos.Ipi;
                        }
                    }


                   


                }
                else
                {
                    _listaItensNotaFiscal = _listaItensNotaFiscal ?? new List<ItemNotaFiscal>();
                    _listaItensNotaFiscal.Add(itemNotaFiscal);
                }

                LimpeItem();
                GeraIdParaCadaItem();
                AtualizeGridItens();
            };

            string mensagemDeSucesso = "Item inserido com sucesso.";
            string tituloMensagemDeSucesso = "Item inserido.";
            string tituloMensagemDeErro = "Inconsistências ao inserir item.";

            if (_itemNotaFiscalEmEdicao != null)
            {
                mensagemDeSucesso = "Item atualizado com sucesso.";
                tituloMensagemDeSucesso = "Item atualizado.";
                tituloMensagemDeErro = "Inconsistências ao atualizar item.";
            }

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirItem, mensagemDeSucesso, tituloMensagemDeSucesso, tituloMensagemDeErro, controleValidar: tabControl2);
        }

        private ItemNotaFiscal RetorneItemNotaFiscalEmEdicao()
        {
            ItemNotaFiscal itemNotaFiscal = new ItemNotaFiscal();

            PreenchaInformacoesBasicasItemEmEdicao(itemNotaFiscal);
            PreenchaImpostosItemEmEdicao(itemNotaFiscal);

            return itemNotaFiscal;
        }

        private void PreenchaInformacoesBasicasItemEmEdicao(ItemNotaFiscal itemNotaFiscal)
        {
            itemNotaFiscal.Produto = _produtoEmEdicao;

            if (txtCodigoCfop.Text != string.Empty & txtCodigoCfop.Text !="0")
            {
                var cfop = _listaCfops.FirstOrDefault(x => x.Codigo == txtCodigoCfop.Text);
                                
                itemNotaFiscal.Cfop = cfop.Codigo.ToInt();
            }

            itemNotaFiscal.Ncm = txtCodigoNcm.Text;

            itemNotaFiscal.ValorUnitario = txtValorUnitarioItem.Text.ToDouble();
            itemNotaFiscal.Quantidade = txtQuantidadeItem.Text.ToDouble();
            itemNotaFiscal.ValorDesconto = txtValorDescontoItem.Text.ToDoubleNullabel();
            itemNotaFiscal.ValorFrete = txtValorFreteItem.Text.ToDoubleNullabel();
            itemNotaFiscal.Seguro = txtValorSeguroItem.Text.ToDoubleNullabel();
            itemNotaFiscal.OutrasDespesas = txtValorOutrasDespesasItem.Text.ToDoubleNullabel();
            itemNotaFiscal.ValorTotal = txtValorTotalItem.Text.ToDouble();
            itemNotaFiscal.CodigoGtinProduto = txtCodigoEanItem.Text;
            itemNotaFiscal.CodigoBarrasProduto = txtCodigoDeBarrasProduto.Text;
        }

        private void PreenchaImpostosItemEmEdicao(ItemNotaFiscal itemNotaFiscal)
        {
            PreenchaImpostoIcmsItemEdicao(itemNotaFiscal);
            PreenchaImpostoIpiItemEdicao(itemNotaFiscal);
            PreenchaImpostoIIItemEdicao(itemNotaFiscal);
            PreenchaImpostoPisItemEdicao(itemNotaFiscal);
            PreenchaImpostoCofinsItemEdicao(itemNotaFiscal);
            PreenchaImpostoTributosDevolvidosItemEdicao(itemNotaFiscal);
            PreenchaTributosEstaduaisFederais(itemNotaFiscal);           
        }
       
        private void PreenchaTributosEstaduaisFederais(ItemNotaFiscal itemNotaFiscal)
        {
            if (itemNotaFiscal.Produto.ContabilFiscal != null && itemNotaFiscal.Produto.ContabilFiscal.Ncm != null)
            {
                itemNotaFiscal.Impostos.TotalTributacaoEstadual = txtValorTotalItem.Text.ToDouble() * itemNotaFiscal.Produto.ContabilFiscal.Ncm.ImpostoIbptEstadual / 100;

                if (itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOINFERIOR40PORCENTO ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOSUPERIOR40PORCENTO ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCONTEUDODEIMPORTACAOSUPERIOR70PORCENTO ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALCUJAPRODUCAOEMCONFORMIDADECOMOSPROCESSOSPRODUTIVOSBASICOS ||
                        itemNotaFiscal.Impostos.Icms.Origem == EnumOrigem.NACIONALEXCETOASINDICADASNOSCODIGOS3E5)
                {
                    itemNotaFiscal.Impostos.TotalTributacaoFederal = Math.Round(txtValorTotalItem.Text.ToDouble() * itemNotaFiscal.Produto.ContabilFiscal.Ncm.ImpostoIbptFederalNacional / 100, 2);
                }
                else
                {
                    itemNotaFiscal.Impostos.TotalTributacaoFederal = Math.Round(txtValorTotalItem.Text.ToDouble() * itemNotaFiscal.Produto.ContabilFiscal.Ncm.ImpostoIbptFederalImportados / 100, 2);
                }

                itemNotaFiscal.Impostos.TotalTributacao = Math.Round(itemNotaFiscal.Impostos.TotalTributacaoEstadual + itemNotaFiscal.Impostos.TotalTributacaoFederal, 2);

            }
            else
            {
                MessageBox.Show("Para continuar você precisa informar o NCM para o item.","Outras Saídas - Inserindo Itens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
        
        private void PreenchaImpostoIcmsItemEdicao(ItemNotaFiscal itemNotaFiscal)
        {
            itemNotaFiscal.Impostos = new ImpostosNotaFiscal();
            itemNotaFiscal.Impostos.Icms = new IcmsNotaFiscal();

            itemNotaFiscal.Impostos.Icms.Origem = (EnumOrigem)cboOrigem.EditValue;
            itemNotaFiscal.Impostos.Icms.CstCsosn = (EnumCstCsosn)cboCstCsosn.EditValue;
            itemNotaFiscal.Impostos.Icms.AliquotaSimplesNacional = txtAliquotaSimplesNacional.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.ValorIcmsSimplesNacional = txtValorIcmsSimplesNacional.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.BaseCalculoIcms = txtBaseIcms.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcms = txtPercentualReducaoIcms.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.AliquotaIcms = txtPercentualIcms.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.MotivoDesoneracaoProduto = (EnumMotivoDesoneracaoProduto?)cboMotivoDesoneracao.EditValue;
            itemNotaFiscal.Impostos.Icms.ValorDesoneracaoProduto = txtIcmsDesoneracaoProduto.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.ValorIcms = txtValorIcms.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.BaseIcmsSubstituicaoTributaria = txtBaseIcmsSt.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.PercentualMargemValorAdicST = txtPercentualMargemAdicST.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.AliquotaIva = txtPercentualMVA.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria = txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text.ToDoubleNullabel();

            itemNotaFiscal.Impostos.Icms.AliquotaDbSt = txtAliquotaDbSt.Text.ToDoubleNullabel();
            itemNotaFiscal.Impostos.Icms.AliquotaCrSt = txtAliquotaCrSt.Text.ToDoubleNullabel();

            itemNotaFiscal.Impostos.Icms.AliquotaSubstituicaoTributaria = txtAliquotaIcmsSt.Text != string.Empty ? txtAliquotaIcmsSt.Text.ToDoubleNullabel() :
                                                                          txtAliquotaDbSt.Text != string.Empty ? txtAliquotaDbSt.Text.ToDoubleNullabel() :
                                                                          txtAliquotaCrSt.Text != string.Empty ? txtAliquotaCrSt.Text.ToDoubleNullabel():0;

            itemNotaFiscal.Impostos.Icms.ValorSubstituicaoTributaria = txtValorIcmsSt.Text.ToDoubleNullabel();

            itemNotaFiscal.Impostos.Icms.ModBC = (EnumModBC?)cboModBC.EditValue;
            itemNotaFiscal.Impostos.Icms.ModBCST = (EnumModBCST?)cboModBCST.EditValue;
        }

        private bool ValideCamposImpostoICMSIPI()
        {
            if (cboCstCsosn.EditValue != null)
            {
                if ((EnumCstCsosn)cboCstCsosn.EditValue == EnumCstCsosn.SIMPLES900 || (EnumCstCsosn)cboCstCsosn.EditValue == EnumCstCsosn.NORMAL90)
                {
                    if (txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text == string.Empty ||
                        txtPercentualReducaoIcms.Text == string.Empty ||
                        txtPercentualIcms.Text == string.Empty ||
                        txtBaseIcms.Text == string.Empty ||
                        txtValorIcms.Text == string.Empty ||
                        txtPercentualMVA.Text == string.Empty ||
                        txtAliquotaCrSt.Text == string.Empty ||
                        txtAliquotaDbSt.Text == string.Empty ||
                        txtBaseIcmsSt.Text == string.Empty ||
                        txtValorIcmsSt.Text == string.Empty ||
                        txtAliquotaSimplesNacional.Text == string.Empty ||
                        txtValorIcmsSimplesNacional.Text == string.Empty)
                    {
                        if (MessageBox.Show("Ainda existe(m) campo(s) de impostos de <ICMS> que precisam ser informados. " +
                                            "Caso não tenha imposto para o item em questão, preencha o campo com zero. " +
                                            "<Deseja continuar sem completar o(s) campo(s)>?",
                                            "Inclusão/Atualização de item", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                }
            }

            if (cboCstIpi.EditValue == null) return true;

            if ((EnumCstIpi)cboCstIpi.EditValue == EnumCstIpi.IPI99 || (EnumCstIpi)cboCstIpi.EditValue == EnumCstIpi.IPI50 ||
                (EnumCstIpi)cboCstIpi.EditValue == EnumCstIpi.IPI00 || (EnumCstIpi)cboCstIpi.EditValue == EnumCstIpi.IPI49)
            {
                if (txtPercentualIpi.Text == string.Empty || txtValorIpi.Text == string.Empty || txtCodigoEnquadramento.Text == string.Empty)
                {
                    if (MessageBox.Show("Ainda existe(m) campo(s) de <IPI> que precisam ser informados. " +
                                        "Caso não tenha imposto para o item em questão, preencha o campo com zero. " +
                                        "<Deseja continuar sem completar o(s) campo(s)>?",
                                        "Inclusão/Atualização de item", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void PreenchaImpostoIpiItemEdicao(ItemNotaFiscal itemNotaFiscal)
        {
            if (itemNotaFiscal.Impostos.Ipi == null) itemNotaFiscal.Impostos.Ipi = new IpiNotaFiscal();
            if (cboCstIpi.EditValue != null)
            {
                itemNotaFiscal.Impostos.Ipi.AliquotaIpi = txtPercentualIpi.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Ipi.ValorIpi = txtValorIpi.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Ipi.BaseDeCalculo = txtValorIpi.Text != string.Empty ? _BaseDeCalculoIpi : txtValorIpi.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Ipi.CstIpi = (EnumCstIpi)cboCstIpi.EditValue;
                itemNotaFiscal.Impostos.Ipi.CodigoEnquadramento = txtCodigoEnquadramento.Text.ToInt();
            }
            else
            {
                itemNotaFiscal.Impostos.Ipi.AliquotaIpi = null;
                itemNotaFiscal.Impostos.Ipi.ValorIpi = null;
                itemNotaFiscal.Impostos.Ipi.BaseDeCalculo = null;
                itemNotaFiscal.Impostos.Ipi.CstIpi = null;
                itemNotaFiscal.Impostos.Ipi.CodigoEnquadramento = 0;
            }
        }

        private void PreenchaImpostoIIItemEdicao(ItemNotaFiscal itemNotaFiscal)
        {
            if (chkInformarII.Checked)
            {
                itemNotaFiscal.Impostos.II = new IINotaFiscal();

                itemNotaFiscal.Impostos.II.BaseCalculoImpostoImportacao = txtBaseCalculoImpostoImportacao.Text.ToDouble();
                itemNotaFiscal.Impostos.II.ValorDespesasAduaneiras = txtValorDespesasAduaneirasImpostoImportacao.Text.ToDouble();
                itemNotaFiscal.Impostos.II.ValorImpostoImportacao = txtValorImpostoImportacao.Text.ToDouble();
                itemNotaFiscal.Impostos.II.ValorImpostoSobreOperacoesFinanceiras = txtIOFImpostosImportacao.Text.ToDouble();
            }
            else
            {
                itemNotaFiscal.Impostos.II = null;
            }
        }

        private void PreenchaImpostoPisItemEdicao(ItemNotaFiscal itemNotaFiscal)
        {
            if (itemNotaFiscal.Impostos.Pis == null) itemNotaFiscal.Impostos.Pis = new PisNotaFiscal();

            if (cboCstPis.EditValue != null)
            {
                itemNotaFiscal.Impostos.Pis.CstPis = (EnumCstPis)cboCstPis.EditValue;
                itemNotaFiscal.Impostos.Pis.BaseDeCalculo = txtValorBaseCalculoPis.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Pis.BaseDeCalculoST = txtBaseCalculoPisST.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Pis.QuantidadeVendida = txtQuantidadeVendidaPis.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Pis.QuantidadeVendidaST = txtQuantidadeVendidaPisST.Text.ToDoubleNullabel();

                itemNotaFiscal.Impostos.Pis.AliquotaPercentual = txtAliquotaPisPercentual.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Pis.AliquotaPercentualST = txtAliquotaPisSTPercentual.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Pis.AliquotaReais = txtAliquotaPisReais.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Pis.AliquotaReaisST = txtAliquotaPisSTReais.Text.ToDoubleNullabel();

                itemNotaFiscal.Impostos.Pis.ValorPis = txtValorPis.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Pis.ValorPisST = txtValorPisST.Text.ToDoubleNullabel();
            }
            else
            {
                itemNotaFiscal.Impostos.Pis.CstPis = null;
                itemNotaFiscal.Impostos.Pis.BaseDeCalculo = null;
                itemNotaFiscal.Impostos.Pis.BaseDeCalculoST = null;
                itemNotaFiscal.Impostos.Pis.QuantidadeVendida = null;
                itemNotaFiscal.Impostos.Pis.QuantidadeVendidaST = null;

                itemNotaFiscal.Impostos.Pis.AliquotaPercentual = null;
                itemNotaFiscal.Impostos.Pis.AliquotaPercentualST = null;
                itemNotaFiscal.Impostos.Pis.AliquotaReais = null;
                itemNotaFiscal.Impostos.Pis.AliquotaReaisST = null;

                itemNotaFiscal.Impostos.Pis.ValorPis = null;
                itemNotaFiscal.Impostos.Pis.ValorPisST = null;
            }        
        }
          
        private void PreenchaImpostoCofinsItemEdicao(ItemNotaFiscal itemNotaFiscal)
        {
            if (itemNotaFiscal.Impostos.Cofins == null) itemNotaFiscal.Impostos.Cofins = new CofinsNotaFiscal();

            if (cboCstCofins.EditValue != null)
            {
                itemNotaFiscal.Impostos.Cofins.CstCofins = (EnumCstCofins)cboCstCofins.EditValue;
                itemNotaFiscal.Impostos.Cofins.BaseDeCalculo = txtBaseCalculoCofins.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Cofins.BaseDeCalculoST = txtBaseCalculoCofinsST.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Cofins.QuantidadeVendida = txtQuantidadeVendidaCofins.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Cofins.QuantidadeVendidaST = txtQuantidadeVendidaCofinsST.Text.ToDoubleNullabel();

                itemNotaFiscal.Impostos.Cofins.AliquotaPercentual = txtAliquotaCofinsPercentual.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Cofins.AliquotaPercentualST = txtAliquotaCofinsSTPercentual.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Cofins.AliquotaReais = txtAliquotaCofinsReais.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Cofins.AliquotaReaisST = txtAliquotaCofinsSTReais.Text.ToDoubleNullabel();

                itemNotaFiscal.Impostos.Cofins.ValorCofins = txtValorCofins.Text.ToDoubleNullabel();
                itemNotaFiscal.Impostos.Cofins.ValorCofinsST = txtValorCofinsST.Text.ToDoubleNullabel();
            }
            else
            {
                itemNotaFiscal.Impostos.Cofins.CstCofins = null;
                itemNotaFiscal.Impostos.Cofins.BaseDeCalculo = null;
                itemNotaFiscal.Impostos.Cofins.BaseDeCalculoST = null;
                itemNotaFiscal.Impostos.Cofins.QuantidadeVendida = null;
                itemNotaFiscal.Impostos.Cofins.QuantidadeVendidaST = null;

                itemNotaFiscal.Impostos.Cofins.AliquotaPercentual = null;
                itemNotaFiscal.Impostos.Cofins.AliquotaPercentualST = null;
                itemNotaFiscal.Impostos.Cofins.AliquotaReais = null;
                itemNotaFiscal.Impostos.Cofins.AliquotaReaisST = null;

                itemNotaFiscal.Impostos.Cofins.ValorCofins = null;
                itemNotaFiscal.Impostos.Cofins.ValorCofinsST = null;
            }
        }

        private void PreenchaImpostoTributosDevolvidosItemEdicao(ItemNotaFiscal itemNotaFiscal)
        {
            if (chkInformarTributosDevolvidos.Checked)
            {
                itemNotaFiscal.Impostos.TributosDevolvidos = new TributosDevolvidosNotaFiscal();
                itemNotaFiscal.Impostos.TributosDevolvidos.PercentualDaMercadoriaDevolvida = txtPercentualMercadoriaTributosDevolvidos.Text.ToDouble();
                itemNotaFiscal.Impostos.TributosDevolvidos.ValorDoIpiDevolvido = txtValorIpiTributosDevolvidos.Text.ToDouble();
            }
            else
            {
                itemNotaFiscal.Impostos.TributosDevolvidos = null;
            }
        }

        private void AtualizeGridItens()
        {

            GereIdFalsoParaOsItens();


        

        List<ItemGrid> listaItensGrid = new List<ItemGrid>();

            foreach (var item in _listaItensNotaFiscal)
            {
                ItemGrid itemGrid = new ItemGrid();

                var cfop = _listaCfops.FirstOrDefault(x => x.Codigo == item.Cfop.ToString());

                if (string.IsNullOrEmpty(item.NomeProduto))
                {
                    item.NomeProduto = item.Produto.DadosGerais.Descricao;
                }
                if (string.IsNullOrEmpty(item.UnidadeProduto))
                {
                    item.UnidadeProduto = item.Produto.DadosGerais.Unidade.Abreviacao;
                }

                itemGrid.Id = item.Id;
                itemGrid.IdProduto = item.Produto.Id;
                itemGrid.Descricao = item.NomeProduto;
                itemGrid.Cfop = cfop != null ? cfop.Codigo + " - " + cfop.Descricao : string.Empty;
                itemGrid.CstCsosn = item.Impostos.Icms.CstCsosn.Descricao();
                itemGrid.Desconto = item.ValorDesconto.GetValueOrDefault().ToString("#0.00");
                itemGrid.Frete = item.ValorFrete.GetValueOrDefault().ToString("#0.00");
                itemGrid.Quantidade = item.Quantidade.ToString();
                itemGrid.Unidade = item.UnidadeProduto;
                itemGrid.ValorTotal = item.ValorTotal.ToString("#0.00");
                itemGrid.ValorUnitario = item.ValorUnitario.ToString("#0.00######");

                listaItensGrid.Add(itemGrid);
            }

            gcItens.DataSource = listaItensGrid;
            gcItens.RefreshDataSource();
        }

        private void GereIdFalsoParaOsItens()
        {
            for (int i = 0; i < _listaItensNotaFiscal.Count; i++)
            {
                _listaItensNotaFiscal[i].Id = i + 1;
            }
        }

        private void EditeItem(ItemNotaFiscal itemNotaFiscal)
        { 
            _itemNotaFiscalEmEdicao = itemNotaFiscal;

            if (itemNotaFiscal != null)
            {
                PreenchaCamposDoProduto(itemNotaFiscal.Produto);
               
                if (rdbTipoOperacaoEntrada.Checked)
                {
                    double? aliquotaSimplesNacional = null;
                    double? aliquotaCreditoST = null;
                    double? aliquotaDebitoST = null;
                    double? mva = null;
                    double? reducaoBaseCalculoST = null;
                    double? icmsBaseCalculo = null;
                    double? icmsReducaoBaseCalculo = null;

                    string ufDestino = cboEstadoDestinatario.EditValue != null ? cboEstadoDestinatario.EditValue.ToString() : string.Empty;

                    EnumCstCsosn cstCsosn = EnumCstCsosn.NORMAL00;
                    Cfop cfop = null;

                    CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();
                    EnumTipoCliente tipoCliente = rdbConsumidorFinal.Checked? EnumTipoCliente.CONSUMIDOR:EnumTipoCliente.REVENDA;

                    var tipoInscricaoIcms = new PessoaMetodosAuxiliares().RetorneTipoInscricaoIcms(txtIdParceiroDestinatario.Text.ToInt());


                    calculosNotaFiscal.DefinaCstCsosnCfopEAliquotas(itemNotaFiscal.Produto, EnumTipoSaidaTributacaoIcms.ENTRADADEVOLUCAOVENDA,
                                                                    ufDestino, tipoCliente, tipoInscricaoIcms, ref cstCsosn, ref cfop, 
                                                                    ref aliquotaSimplesNacional, ref aliquotaCreditoST,ref aliquotaDebitoST, 
                                                                    ref mva, ref reducaoBaseCalculoST, ref icmsBaseCalculo, ref icmsReducaoBaseCalculo);
                    
                    txtCodigoCfop.Text = cfop != null? cfop.Codigo.ToString() : itemNotaFiscal.Cfop.ToString() != "0"? itemNotaFiscal.Cfop.ToString() : string.Empty;
                    BuscaDescricaoCfop();
                    txtDescricaoCFOP.Text = cfop != null ? cfop.Descricao : txtDescricaoCFOP.Text;
                    
                }
                else
                {
                    if (_listaCfops != null)
                    {
                        var cfop = _listaCfops.FirstOrDefault(x => x.Codigo == itemNotaFiscal.Cfop.ToString());

                        txtCodigoCfop.Text = cfop != null ? cfop.Codigo.ToString() : string.Empty;
                        txtDescricaoCFOP.Text = cfop != null ? cfop.Descricao.ToString() : string.Empty;
                    }
                }
                                
                txtValorUnitarioItem.Text = itemNotaFiscal.ValorUnitario != null ? itemNotaFiscal.ValorUnitario.ToString("#0.00######") : string.Empty;
                txtQuantidadeItem.Text = itemNotaFiscal.Quantidade.ToString();
                txtValorDescontoItem.Text = itemNotaFiscal.ValorDesconto != null ? itemNotaFiscal.ValorDesconto.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorFreteItem.Text = itemNotaFiscal.ValorFrete != null ? itemNotaFiscal.ValorFrete.GetValueOrDefault().ToString("#0.00") : string.Empty;

                txtValorSeguroItem.Text = itemNotaFiscal.Seguro != null ? itemNotaFiscal.Seguro.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorOutrasDespesasItem.Text = itemNotaFiscal.OutrasDespesas != null ? itemNotaFiscal.OutrasDespesas.GetValueOrDefault().ToString("#0.00") : string.Empty;

                txtValorTotalItem.Text = itemNotaFiscal.ValorTotal.ToString("0.00");

                if (itemNotaFiscal.Impostos != null)
                {
                    PreenchaCamposIcms(itemNotaFiscal.Impostos.Icms);
                    PreenchaCamposIpi(itemNotaFiscal.Impostos.Ipi);
                    PreenchaCamposPis(itemNotaFiscal.Impostos.Pis);
                    PreenchaCamposCofins(itemNotaFiscal.Impostos.Cofins);
                }
                else
                {
                    PreenchaCamposIcms(null);
                    PreenchaCamposIpi(null);
                    PreenchaCamposPis(null);
                    PreenchaCamposCofins(null);
                }

                
                btnInserirAtualizarItem.Image = Properties.Resources.icones2_07;
                
                btnExcluirItem.Visible = true;
                btnInserirAtualizarItem.Image = Properties.Resources.icon_atualizar;

                chkTodosCFOPs.Visible = true;
                chkTodosCSTs.Visible = true;
                chkTodosIPI.Visible = true;
                chkTodosCFOPs.Checked = false;
                chkTodosCSTs.Checked = false;
                chkTodosIPI.Checked = false;

            }
            else
            {
                PreenchaCamposDoProduto(null);

                txtCodigoCfop.Text = string.Empty;
                txtDescricaoCFOP.Text = string.Empty;

                txtValorUnitarioItem.Text = string.Empty;
                txtQuantidadeItem.Text = string.Empty;
                txtValorDescontoItem.Text = string.Empty;

                txtValorTotalItem.Text = string.Empty;
                txtValorFreteItem.Text = string.Empty;
                txtValorSeguroItem.Text = string.Empty;
                txtValorOutrasDespesasItem.Text = string.Empty;
                txtSubTotalItem.Text = string.Empty;
                
                PreenchaCamposIcms(null);
                PreenchaCamposIpi(null);

                PreenchaCamposPis(null);
                PreenchaCamposCofins(null);

                txtIdProduto.Focus();

                btnInserirAtualizarItem.Image = Properties.Resources.icones2_19;
                btnExcluirItem.Visible = false;

                chkTodosCFOPs.Visible = false;
                chkTodosCSTs.Visible = false;
                chkTodosIPI.Visible = false;

                if (_btnSalvar)
                _listaItensNotaFiscal = new List<ItemNotaFiscal>();
            }
            //CalculeTotais();
        }

        private void PreenchaCamposIpi(IpiNotaFiscal ipi)
        {
            if (ipi != null)
            {
                txtPercentualIpi.Text = ipi.AliquotaIpi != null ? ipi.AliquotaIpi.Value.ToString("#0.00") : string.Empty;
                txtValorIpi.Text = ipi.ValorIpi != null ? ipi.ValorIpi.Value.ToString("#0.00") : string.Empty;
                cboCstIpi.EditValue = ipi.CstIpi;
                _BaseDeCalculoIpi = ipi.BaseDeCalculo.ToDouble() != 0? ipi.BaseDeCalculo.ToDouble():0;
                txtCodigoEnquadramento.Text = ipi.CodigoEnquadramento.ToString("000");
            }
            else
            {
                txtPercentualIpi.Text = string.Empty;
                txtValorIpi.Text = string.Empty;
                cboCstIpi.EditValue = null;
                _BaseDeCalculoIpi = 0;
                txtCodigoEnquadramento.Text = string.Empty;
            }
        }

        private void PreenchaCamposPis(PisNotaFiscal pis)
        {
            if (pis != null)
            {
                cboCstPis.EditValue = pis.CstPis;

                if (pis.AliquotaPercentual != null)                
                    txtValorBaseCalculoPis.Text = pis.BaseDeCalculo != 0 ? pis.BaseDeCalculo.Value.ToString("0.00") : "0.00";                   
                
                else if (pis.AliquotaPercentualST != null)                
                    txtBaseCalculoPisST.Text = pis.BaseDeCalculoST != 0 ? pis.BaseDeCalculoST.Value.ToString("0.00") : "0.00";
                
                else if (pis.AliquotaReais != null)                
                    txtQuantidadeVendidaPis.Text = pis.QuantidadeVendida != null ? pis.QuantidadeVendida.Value.ToString("#0.00") : string.Empty;                    
                
                else                
                    txtQuantidadeVendidaPisST.Text = pis.QuantidadeVendidaST != null ? pis.QuantidadeVendidaST.Value.ToString("#0.00") : string.Empty;
                
                txtAliquotaPisPercentual.Text = pis.AliquotaPercentual != null ? pis.AliquotaPercentual.Value.ToString("#0.00") : string.Empty;
                txtAliquotaPisSTPercentual.Text = pis.AliquotaPercentualST != null ? pis.AliquotaPercentualST.Value.ToString("#0.00") : string.Empty;
                txtAliquotaPisReais.Text = pis.AliquotaReais != null ? pis.AliquotaReais.Value.ToString("#0.00") : string.Empty;
                txtAliquotaPisSTReais.Text = pis.AliquotaReaisST != null ? pis.AliquotaReaisST.Value.ToString("#0.00") : string.Empty;

                txtValorPis.Text = pis.ValorPis != null ? pis.ValorPis.Value.ToString("#0.00") : string.Empty;
                txtValorPisST.Text = pis.ValorPisST != null ? pis.ValorPisST.Value.ToString("#0.00") : string.Empty;
            }
            else
            {
                cboCstPis.EditValue = null;
                _BaseDeCalculoPis = 0;
                _BaseDeCalculoPisST = 0;

                txtQuantidadeVendidaPis.Text = string.Empty;
                txtQuantidadeVendidaPisST.Text = string.Empty;

                txtAliquotaPisPercentual.Text = string.Empty;
                txtAliquotaPisSTPercentual.Text = string.Empty;
                txtAliquotaPisReais.Text = string.Empty;
                txtAliquotaPisSTReais.Text = string.Empty;

                txtValorPis.Text = string.Empty;
                txtValorPisST.Text = string.Empty;
            }
        }

        private void PreenchaCamposCofins(CofinsNotaFiscal cofins)
        {
            if (cofins != null)
            {
                cboCstCofins.EditValue = cofins.CstCofins;

                if (cofins.AliquotaPercentual != null)                
                    txtBaseCalculoCofins.Text = cofins.BaseDeCalculo != 0 ? cofins.BaseDeCalculo.Value.ToString("0.00") : "0.00";
                
                else if (cofins.AliquotaPercentualST != null)
                    txtBaseCalculoCofinsST.Text = cofins.BaseDeCalculoST != 0 ? cofins.BaseDeCalculoST.Value.ToString("0.00") : "0.00";

                else if (cofins.AliquotaReais != null)
                    txtQuantidadeVendidaCofins.Text = cofins.QuantidadeVendida != null ? cofins.QuantidadeVendida.Value.ToString("#0.00") : string.Empty;
                
                else
                    txtQuantidadeVendidaCofinsST.Text = cofins.QuantidadeVendidaST != null ? cofins.QuantidadeVendidaST.Value.ToString("#0.00") : string.Empty;

                txtAliquotaCofinsPercentual.Text = cofins.AliquotaPercentual != null ? cofins.AliquotaPercentual.Value.ToString("#0.00") : string.Empty;
                txtAliquotaCofinsSTPercentual.Text = cofins.AliquotaPercentualST != null ? cofins.AliquotaPercentualST.Value.ToString("#0.00") : string.Empty;
                txtAliquotaCofinsReais.Text = cofins.AliquotaReais != null ? cofins.AliquotaReais.Value.ToString("#0.00") : string.Empty;
                txtAliquotaCofinsSTReais.Text = cofins.AliquotaReaisST != null ? cofins.AliquotaReaisST.Value.ToString("#0.00") : string.Empty;

                txtValorCofins.Text = cofins.ValorCofins != null ? cofins.ValorCofins.Value.ToString("#0.00") : string.Empty;
                txtValorCofinsST.Text = cofins.ValorCofinsST != null ? cofins.ValorCofinsST.Value.ToString("#0.00") : string.Empty;
            }
            else
            {
                cboCstCofins.EditValue = null;
                _BaseDeCalculoCofins = 0;
                _BaseDeCalculoCofinsST = 0;

                txtQuantidadeVendidaCofins.Text = string.Empty;
                txtQuantidadeVendidaCofinsST.Text = string.Empty;

                txtAliquotaCofinsPercentual.Text = string.Empty;
                txtAliquotaCofinsSTPercentual.Text = string.Empty;
                txtAliquotaCofinsReais.Text = string.Empty;
                txtAliquotaCofinsSTReais.Text = string.Empty;

                txtValorCofins.Text = string.Empty;
                txtValorCofinsST.Text = string.Empty;
            }
        }

        private void PreenchaCamposIcms(IcmsNotaFiscal icms)
        {
            if (icms != null)
            {
                cboOrigem.EditValue = icms.Origem;
                cboCstCsosn.EditValue = icms.CstCsosn;
                cboModBC.EditValue = icms.ModBC;
                cboModBCST.EditValue = icms.ModBCST;

                txtAliquotaSimplesNacional.Text = icms.AliquotaSimplesNacional != null ? icms.AliquotaSimplesNacional.Value.ToString("#0.00") : string.Empty;
                txtValorIcmsSimplesNacional.Text = icms.ValorIcmsSimplesNacional != null ? icms.ValorIcmsSimplesNacional.Value.ToString("#0.00") : string.Empty;

                txtBaseIcms.Text = icms.BaseCalculoIcms != null ? icms.BaseCalculoIcms.Value.ToString("0.00") : string.Empty;
                txtPercentualReducaoIcms.Text = icms.AliquotaReducaoIcms != null ? icms.AliquotaReducaoIcms.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtPercentualIcms.Text = icms.AliquotaIcms != null ? icms.AliquotaIcms.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtValorIcms.Text = icms.ValorIcms != null ? icms.ValorIcms.GetValueOrDefault().ToString("#0.00") : string.Empty;

                cboMotivoDesoneracao.EditValue = icms.MotivoDesoneracaoProduto;
                txtIcmsDesoneracaoProduto.Text = icms.ValorDesoneracaoProduto != null ? icms.ValorDesoneracaoProduto.Value.ToString("#0.00") : string.Empty;
                                
                txtBaseIcmsSt.Text = icms.BaseIcmsSubstituicaoTributaria != null ? icms.BaseIcmsSubstituicaoTributaria.GetValueOrDefault().ToString("#0.00") : string.Empty;

                if ((EnumCodigoRegimeTributario)cboRegimeTributario.EditValue == EnumCodigoRegimeTributario.REGIMENORMAL)
                    txtPercentualMargemAdicST.Text = icms.PercentualMargemValorAdicST != null ? icms.PercentualMargemValorAdicST.GetValueOrDefault().ToString("#0.00") : string.Empty;
                else
                    txtPercentualMVA.Text = icms.AliquotaIva != null ? icms.AliquotaIva.GetValueOrDefault().ToString("#0.00") : string.Empty;

                txtPercentualReducaoBaseCalculoSubstituicaoTributaria.Text = icms.AliquotaReducaoIcmsSubstituicaoTributaria != null ? icms.AliquotaReducaoIcmsSubstituicaoTributaria.Value.ToString("0.00") : string.Empty;

                txtAliquotaDbSt.Text = icms.AliquotaDbSt != null ? icms.AliquotaDbSt.GetValueOrDefault().ToString("#0.00") : string.Empty;
                txtAliquotaCrSt.Text = icms.AliquotaCrSt != null ? icms.AliquotaCrSt.GetValueOrDefault().ToString("#0.00") : string.Empty;

                txtAliquotaIcmsSt.Text = icms.AliquotaSubstituicaoTributaria != null ? icms.AliquotaSubstituicaoTributaria.GetValueOrDefault().ToString("#0.00") : string.Empty;

                txtValorIcmsSt.Text = icms.ValorSubstituicaoTributaria != null ? icms.ValorSubstituicaoTributaria.GetValueOrDefault().ToString("#0.00") : string.Empty;
            }
            else
            {
                cboOrigem.EditValue = null;
                cboCstCsosn.EditValue = null;
                cboModBC.EditValue = null;
                cboModBCST.EditValue = null;

                txtBaseIcms.Text = string.Empty;
                txtPercentualReducaoIcms.Text = string.Empty;
                txtPercentualIcms.Text = string.Empty;
                txtValorIcms.Text = string.Empty;

                cboMotivoDesoneracao.EditValue = null;
                txtIcmsDesoneracaoProduto.Text = string.Empty;

                txtPercentualMargemAdicST.Text = string.Empty;
                txtBaseIcmsSt.Text = string.Empty;
                txtPercentualMVA.Text = string.Empty;
                txtAliquotaDbSt.Text = string.Empty;
                txtAliquotaCrSt.Text = string.Empty;
                txtAliquotaIcmsSt.Text = string.Empty;
                txtValorIcmsSt.Text = string.Empty;
            }
        }

        private void LimpeItem()
        {
            EditeItem(null);
        }

        private void GeraIdParaCadaItem()
        {
            int id = 0;

            foreach (var item in _listaItensNotaFiscal)
            {
                item.Id = id;

                id++;
            }
        }

        private void RateieFreteNosItens(double valorFrete)
        {
            if (_listaItensNotaFiscal == null)
            {
                return;
            }

            double totalBruto = _listaItensNotaFiscal.Sum(i => i.ValorBruto);
            double totalFreteAplicado = 0;

            for (int i = 0; i < _listaItensNotaFiscal.Count; i++)
            {
                var item = _listaItensNotaFiscal[i];

                item.ValorFrete = Math.Round(((item.ValorTotal) * valorFrete) / totalBruto, 2);
                totalFreteAplicado += item.ValorFrete.GetValueOrDefault();

                if (i == _listaItensNotaFiscal.Count - 1)
                {
                    var diferencaDesconto = valorFrete - totalFreteAplicado;

                    item.ValorFrete += diferencaDesconto;
                }

                item.ValorTotal = Math.Round(item.ValorBruto + item.ValorFrete.GetValueOrDefault() - item.ValorDesconto.GetValueOrDefault(), 2);
            }

            AtualizeGridItens();
        }

        private void AltereMascaraQuantidadeProduto()
        {
            if (_produtoEmEdicao.DadosGerais.PermiteVendaFracionada)
            {
                txtQuantidadeItem.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtQuantidadeItem.Properties.Mask.EditMask = @"[0-9]{1,11}";
            }
        }
       
        private void BuscaDescricaoCfop()
        {
            ServicoCfop servicoCfop = new ServicoCfop();

            var cfop = servicoCfop.ConsultePeloCodigoCfop(txtCodigoCfop.Text);

            if (cfop != null)
            {
                txtCodigoCfop.Text = cfop.Codigo;
                txtDescricaoCFOP.Text = cfop.Descricao;
            }
        }

        #endregion

        #region " MÉTODOS RELACIONADOS AS PARCELAS FINANCEIRO "

        private void InsiraOuAtualizeParcelaFinanceiro()
        {
            Action actionInserirItem = () =>
            {
                DuplicataNotaFiscal duplicataNotaFiscal = new DuplicataNotaFiscal();

                duplicataNotaFiscal.DataVencimento = txtDataVencimentoParcelaFinanceiro.Text.ToDate();
                duplicataNotaFiscal.NumeroDuplicata = txtNumeroDuplicataFinanceiro.Text;
                duplicataNotaFiscal.ValorDuplicata = txtValorDuplicataFinanceiro.Text.ToDouble();
                duplicataNotaFiscal.FormaPagamento = (EnumTipoFormaPagamento)cboFormaPagamento.EditValue;
                duplicataNotaFiscal.CondicaoPagamento = cboCodicaoPagamentoFinanceiro.EditValue.ToInt();

                //ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                //servicoEntradaMercadoria.ValideFinanceiro(duplicataNotaFiscal);

                if (_duplicataEmEdicao != null)
                {
                    int indexItem = _listaDuplicatas.IndexOf(_duplicataEmEdicao);

                    _listaDuplicatas.Remove(_duplicataEmEdicao);

                    _listaDuplicatas.Insert(indexItem, duplicataNotaFiscal);
                }
                else
                {
                    _listaDuplicatas.Add(duplicataNotaFiscal);
                }

                LimpeCamposFinanceiro();
                GereIdParaCadaDuplicataFinanceiro();
                PreenchaGridFinanceiro();
            };

            string mensagemDeSucesso = "Parcela inserida com sucesso.";
            string tituloMensagemDeSucesso = "Parcela inserida.";
            string tituloMensagemDeErro = "Inconsistências ao inserir parcela.";

            if (_duplicataEmEdicao != null)
            {
                mensagemDeSucesso = "Parcela atualizada com sucesso.";
                tituloMensagemDeSucesso = "Parcela atualizada.";
                tituloMensagemDeErro = "Inconsistências ao atualizar parcela.";
            }

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirItem, mensagemDeSucesso, tituloMensagemDeSucesso, tituloMensagemDeErro);
        }

        private void LimpeCamposFinanceiro()
        {
            EditeDuplicataFinanceiro(null);
        }

        private void LimpeForm()
        {
            _notaFiscalEmEdicao = null;                        
            gcNotasReferenciadas.DataSource = null;
            EditeItem(null);            
            EditeNotaFiscal();
            CalculeTotaisNotaFiscal();
        }

        private void EditeDuplicataFinanceiro(DuplicataNotaFiscal duplicataNotaFiscal)
        {
            _duplicataEmEdicao = duplicataNotaFiscal;

            if (duplicataNotaFiscal != null)
            {
                txtParcelaFinanceiro.Text = duplicataNotaFiscal.Parcela;
                txtNumeroDuplicataFinanceiro.Text = duplicataNotaFiscal.NumeroDuplicata;
                txtDataVencimentoParcelaFinanceiro.DateTime = duplicataNotaFiscal.DataVencimento;
                txtValorDuplicataFinanceiro.Text = duplicataNotaFiscal.ValorDuplicata.ToString("0.00");
                
                cboFormaPagamento.EditValue = ((EnumTipoFormaPagamento)duplicataNotaFiscal.FormaPagamento);
                cboCodicaoPagamentoFinanceiro.EditValue = duplicataNotaFiscal.CondicaoPagamento;

                btnAdicionarFinanceiro.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                txtParcelaFinanceiro.Text = string.Empty;

                cboCodicaoPagamentoFinanceiro.EditValue = null;
                cboFormaPagamento.EditValue = null;

                txtNumeroDuplicataFinanceiro.Text = string.Empty;
                txtDataVencimentoParcelaFinanceiro.Text = string.Empty;
                txtValorDuplicataFinanceiro.Text = string.Empty;

                btnAdicionarFinanceiro.Image = Properties.Resources.icones2_19;
            }
        }
        
        private int RetorneNumeroDaFormaDePagamento(EnumTipoFormaPagamento TipoFormaPagamento)
        {
            switch (TipoFormaPagamento)
            {
                case EnumTipoFormaPagamento.OUTROS:
                    return 0;
                case EnumTipoFormaPagamento.DINHEIRO:
                    return 1;
                case EnumTipoFormaPagamento.BOLETOBANCARIO:
                    return 2;
                case EnumTipoFormaPagamento.DEPOSITOBANCARIO:
                    return 3;
                case EnumTipoFormaPagamento.CHEQUE:
                    return 4;
                case EnumTipoFormaPagamento.DUPLICATA:
                    return 5;
                case EnumTipoFormaPagamento.CREDIARIOPROPRIO:
                    return 6;
                case EnumTipoFormaPagamento.CARTAOCREDITO:
                    return 7;
                case EnumTipoFormaPagamento.CARTAODEBITO:
                    return 8;
                case EnumTipoFormaPagamento.CREDITOINTERNO:
                    return 9;
                
            }

            return 0;
        }

        private void GereIdParaCadaDuplicataFinanceiro()
        {
            _listaDuplicatas = _listaDuplicatas.OrderBy(finan => finan.DataVencimento).ToList();

            int quantidadeParcelas = _listaDuplicatas.Count;

            for (int i = 0; i < quantidadeParcelas; i++)
            {
                var financeiro = _listaDuplicatas[i];

                financeiro.Id = i + 1;
                financeiro.Parcela = (i + 1) + "/" + quantidadeParcelas;
            }
        }

        private void PreenchaGridFinanceiro()
        {
            List<DuplicataGrid> listaDuplicatasGrid = new List<DuplicataGrid>();

            foreach (var financeiro in _listaDuplicatas)
            {
                DuplicataGrid duplicataGrid = new DuplicataGrid();

                duplicataGrid.Id = financeiro.Id;
                duplicataGrid.DataVencimento = financeiro.DataVencimento.ToString("dd/MM/yyyy");
                duplicataGrid.NumeroDuplicata = financeiro.NumeroDuplicata;
                duplicataGrid.Parcela = financeiro.Parcela;
                duplicataGrid.Valor = financeiro.ValorDuplicata.ToString("0.00");
                duplicataGrid.FormaPagamento = ((EnumTipoFormaPagamento)financeiro.FormaPagamento).Descricao().ToString();

                var condicao = new ServicoCondicaoPagamento().Consulte(financeiro.CondicaoPagamento);

                duplicataGrid.CondicaoPagamento = condicao != null? condicao.Descricao:"OUTROS";


                listaDuplicatasGrid.Add(duplicataGrid);
            }

            gcParcelasFinanceiro.DataSource = listaDuplicatasGrid;
            gcParcelasFinanceiro.RefreshDataSource();
        }

        #endregion

        #region " PREENCHA OBJETO NOTA FISCAL "

        private void PreenchaObjetoNotaFiscal()
        {
            _notaFiscalEmEdicao = _notaFiscalEmEdicao ?? new NotaFiscal();

            _notaFiscalEmEdicao.ListaNotasReferenciadas = _notasFiscaisReferenciadas;
            _notaFiscalEmEdicao.ListaItens = _listaItensNotaFiscal;
            _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.TipoFrete = (EnumTipoFrete)cboTipoFrete.EditValue;
            _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.TransportadoraId = cboTransportadoras.EditValue != null? cboTransportadoras.EditValue.ToIntNullabel(): cboTransportadoras.EditValue.ToIntNullabel();
            _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Volume = txtVolume.Text.ToInt();

            PreenchaCamposNotaFiscalEmpresaEmitente();
                        
            PreenchaObjetoIdentificacaoNotaFiscal();
            PreenchaObjetoDestinatarioNotaFiscal();
            PreenchaObjetoLocalEntregaNotaFiscal();
            PreenchaObjetoLocalRetiradaNotaFiscal();

            PreenchaObjetoDadosCobrancaNotaFiscal();

            PreenchaObjetoTotaisNotaFiscal();

            PreenchaObjetoInformacoesAdicionarNotaFiscal();

            PreenchaObjetoInformacoesDocumentoOrigemNotaFiscal();

            PreenchaObjetoInformacoesComercioExterior();
            PreenchaObjetoInformacoesCompra();
        }

        private void PreenchaCamposNotaFiscalEmpresaEmitente()
        {
            _notaFiscalEmEdicao.Emitente.CNAE = _empresa.DadosEmpresa.Cnae != null ? _empresa.DadosEmpresa.Cnae.Codigo : string.Empty;
            _notaFiscalEmEdicao.Emitente.CNPJ = _empresa.DadosEmpresa.Cnpj;
            _notaFiscalEmEdicao.Emitente.CRT = _empresa.DadosEmpresa.CodigoRegimeTributario.GetValueOrDefault();

            _notaFiscalEmEdicao.Emitente.Logradouro = _empresa.DadosEmpresa.Endereco.Rua;
            _notaFiscalEmEdicao.Emitente.Numero = _empresa.DadosEmpresa.Endereco.Numero;
            _notaFiscalEmEdicao.Emitente.Complemento = _empresa.DadosEmpresa.Endereco.Complemento;
            _notaFiscalEmEdicao.Emitente.Bairro = _empresa.DadosEmpresa.Endereco.Bairro;
            _notaFiscalEmEdicao.Emitente.CodigoMunicipio = _empresa.DadosEmpresa.Endereco.Cidade.CodigoIbge.ToLong();
            _notaFiscalEmEdicao.Emitente.NomeMunicipio = _empresa.DadosEmpresa.Endereco.Cidade.Descricao;
            _notaFiscalEmEdicao.Emitente.UF = _empresa.DadosEmpresa.Endereco.Cidade.Estado.UF;
            _notaFiscalEmEdicao.Emitente.Cep = _empresa.DadosEmpresa.Endereco.CEP;
            _notaFiscalEmEdicao.Emitente.CodigoPais = 1058;
            _notaFiscalEmEdicao.Emitente.NomePais = "BRASIL";

            _empresa.DadosEmpresa.Telefone = _empresa.DadosEmpresa.Telefone.Replace('(', ' ').Replace(')', ' ').Replace('-', ' ').Replace(" ", "");           
            _notaFiscalEmEdicao.Emitente.Telefone = _empresa.DadosEmpresa.Telefone.ToLongNullabel();

            _notaFiscalEmEdicao.Emitente.InscricaoEstadual = _empresa.DadosEmpresa.InscricaoEstadual;
            _notaFiscalEmEdicao.Emitente.InscricaoMunicipal = _empresa.DadosEmpresa.InscricaoMunicipal;

            _notaFiscalEmEdicao.Emitente.NomeFantasia = _empresa.DadosEmpresa.NomeFantasia;
            _notaFiscalEmEdicao.Emitente.RazaoSocial = _empresa.DadosEmpresa.RazaoSocial;
        }

        private void PreenchaObjetoIdentificacaoNotaFiscal()
        {
            _notaFiscalEmEdicao.IdentificacaoNotaFiscal = _notaFiscalEmEdicao.IdentificacaoNotaFiscal ?? new IdentificacaoNotaFiscal();

            var identificacaoNotaFiscal = _notaFiscalEmEdicao.IdentificacaoNotaFiscal;

            identificacaoNotaFiscal.ConsumidorFinal = rdbConsumidorFinal.Checked == true;

            if (txtNaturezaOperacao.Text.Length > 60)
            {
                int tamNaturezaOp = txtNaturezaOperacao.Text.Length - 60;
                identificacaoNotaFiscal.DescricaoNaturezaOperacao = txtNaturezaOperacao.Text.Remove(60, tamNaturezaOp);
            }
            else
                identificacaoNotaFiscal.DescricaoNaturezaOperacao = txtNaturezaOperacao.Text;

            identificacaoNotaFiscal.FinalidadeEmissaoNFe = (EnumFinalidadeEmissaoNfe)cboFinalidadeNFe.EditValue;
            identificacaoNotaFiscal.FormaPagamento = (EnumCondicaoPagamentoNota)cboCondicaoPagamento.EditValue;
            //identificacaoNotaFiscal.FormatoImpressaoDanfe = EnumFormatoImpressaoDanfe.DANFENORMALPAISAGEM;
            identificacaoNotaFiscal.IdentificacaoOperacaoNotaFiscal = _notaFiscalEmEdicao.Destinatario.UF == _empresa.DadosEmpresa.Endereco.Cidade.Estado.UF? EnumIdenficacaoOperacaoNotaFiscal.OPERACAOINTERNA :
                                                                                                                   //_notaFiscalEmEdicao.Destinatario.UF == "EX" && _notaFiscalEmEdicao.IdentificacaoNotaFiscal.ConsumidorFinal ? EnumIdenficacaoOperacaoNotaFiscal.OPERACAOINTERNA :
                                                                                                                   _notaFiscalEmEdicao.Destinatario.UF == "EX"? EnumIdenficacaoOperacaoNotaFiscal.OPERACAOCOMEXTERIOR :
                                                                                                                                                                          EnumIdenficacaoOperacaoNotaFiscal.OPERACAOINTERESTADUAL;

            identificacaoNotaFiscal.IndicacaoPresenca = (EnumIndicacaoPresenca)cboIndicadorDePresenca.EditValue;
            identificacaoNotaFiscal.ModeloDocumentoFiscal = 55;
            identificacaoNotaFiscal.NaturezaOperacao = new NaturezaOperacao();
            identificacaoNotaFiscal.NaturezaOperacao.Id = txtIdNaturezaOperacao.Text.ToInt();
            identificacaoNotaFiscal.NotaSaida = rdbTipoOperacaoNotaSaida.Checked == true;
            identificacaoNotaFiscal.Cidade = _empresa.DadosEmpresa.Endereco.Cidade;
            //identificacaoNotaFiscal.ProcessoEmissaoNfe
            //identificacaoNotaFiscal.Serie
            //identificacaoNotaFiscal.TipoAmbiente
            //identificacaoNotaFiscal.TipoEmissaoDanfe = EnumTipoEmissaoDanfe.
            //identificacaoNotaFiscal.VersaoAplicativo            
            //identificacaoNotaFiscal.CodigoNumericoNota
            //identificacaoNotaFiscal.DataHoraEmissao
            //identificacaoNotaFiscal.DataHoraSaida
            //identificacaoNotaFiscal.DigitoVerificadorChaveAcesso
            //identificacaoNotaFiscal.NumeroNota
        }

        private void PreenchaObjetoDestinatarioNotaFiscal()
        {
            _notaFiscalEmEdicao.Destinatario = _notaFiscalEmEdicao.Destinatario ?? new DestinatarioNotaFiscal();

            var destinatario = _notaFiscalEmEdicao.Destinatario;

            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var pessoa = servicoPessoa.Consulte(txtIdParceiroDestinatario.Text.ToInt());

            destinatario.DataCadastroPessoa = pessoa.DadosGerais.DataCadastro;
            destinatario.EhPessoaFisica = rdbTipoPessoaFisicaDestinatario.Checked;
            destinatario.Email = txtEmailDestinatario.Text;
            destinatario.IdEstrangeiro = txtIdEstrangeiroDestinatario.Text;
            destinatario.IndicadorIEDestinatario = !string.IsNullOrEmpty(txtInscricaoEstadualDestinatario.Text) ? EnumIndicadorIEDestinatario.CONTRIBUINTEICMS : EnumIndicadorIEDestinatario.NAOCONTRIBUINTE;
            destinatario.InscricaoEstadual = txtInscricaoEstadualDestinatario.Text;
            destinatario.InscricaoSuframa = txtInscricaoSuframaDestinatario.Text;
            destinatario.Numero = txtNumeroDestinatario.Text;
           
            
            destinatario.ParceiroResideExterior = chkParceiroResideExteriorDestinatario.Checked;
            destinatario.Pessoa = pessoa;
            destinatario.RazaoSocialOuNomeDestinatario = txtRazaoSocialParceiroDestinatario.Text;
            destinatario.StatusPessoa = pessoa.DadosGerais.Status;
            destinatario.TipoPessoa = rdbTipoPessoaFisicaDestinatario.Checked ? EnumTipoPessoa.PESSOAFISICA : EnumTipoPessoa.PESSOAJURIDICA;

            destinatario.CnpjCpf = txtCpfCnpjDestinatario.Text;

            if (!string.IsNullOrEmpty(txtDDDDestinatario.Text) && !string.IsNullOrEmpty(txtNumeroTelefoneDestinatario.Text))
            {
                destinatario.Telefone = (txtDDDDestinatario.Text + txtNumeroTelefoneDestinatario.Text.RemoverCaracteresDeMascara()).ToLong();
            }

            destinatario.UF = chkParceiroResideExteriorDestinatario.Checked ? "EX" : cboEstadoDestinatario.EditValue.ToString();
            destinatario.Bairro = txtBairroDestinatario.Text;
            destinatario.Cep = txtCepDestinatario.Text.ToStringNull();

            destinatario.CodigoMunicipio = chkParceiroResideExteriorDestinatario.Checked ? 9999999 : cboCidadeDestinatario.EditValue.ToInt();
            
            destinatario.NomeMunicipio = chkParceiroResideExteriorDestinatario.Checked ? "EXTERIOR" : cboCidadeDestinatario.AccessibilityObject.Value; 

            destinatario.Complemento = txtComplementoDestinatario.Text.ToStringNull();
            destinatario.Logradouro = txtRuaDestinatario.Text;

            destinatario.CodigoPais = chkParceiroResideExteriorDestinatario.Checked? cboPaisDestinatario.EditValue.ToIntNullabel():1058;
            destinatario.NomePais = chkParceiroResideExteriorDestinatario.Checked? cboPaisDestinatario.Text.ToStringNull():"Brasil";
            destinatario.IdEstrangeiro = chkParceiroResideExteriorDestinatario.Checked? "9999999" : null;
            destinatario.InscricaoSuframa = txtInscricaoSuframaDestinatario.Text != string.Empty ? txtInscricaoSuframaDestinatario.Text : null;
            destinatario.Email = txtEmailDestinatario.Text != string.Empty ? txtEmailDestinatario.Text : null;
            
        }

        private void PreenchaObjetoLocalEntregaNotaFiscal()
        {
            _notaFiscalEmEdicao.LocalEntrega = null;

            if (chkInformarLocalEntrega.Checked)
            {
                _notaFiscalEmEdicao.LocalEntrega = new LocalEntregaNotaFiscal();

                _notaFiscalEmEdicao.LocalEntrega.Bairro = txtBairroLocalEntrega.Text;
                _notaFiscalEmEdicao.LocalEntrega.CodigoMunicipio = cboCidadeLocalEntrega.EditValue.ToInt();
                _notaFiscalEmEdicao.LocalEntrega.Complemento = txtComplementoLocalEntrega.Text;
                _notaFiscalEmEdicao.LocalEntrega.CpfCnpj = txtCpfCnpjLocalEntrega.Text;
                _notaFiscalEmEdicao.LocalEntrega.Logradouro = txtRuaLocalEntrega.Text;
                _notaFiscalEmEdicao.LocalEntrega.NomeMunicipio = cboCidadeLocalEntrega.Text;
                _notaFiscalEmEdicao.LocalEntrega.Numero = txtNumeroLocalEntrega.Text;
                _notaFiscalEmEdicao.LocalEntrega.TipoPessoa = rdbTipoPessoaFisicaLocalEntrega.Checked ? EnumTipoPessoa.PESSOAFISICA : EnumTipoPessoa.PESSOAJURIDICA;
                _notaFiscalEmEdicao.LocalEntrega.UF = cboEstadoLocalEntrega.EditValue.ToString();
            }
            else
            {
                _notaFiscalEmEdicao.LocalEntrega = null;
            }
        }

        private void PreenchaObjetoLocalRetiradaNotaFiscal()
        {
            _notaFiscalEmEdicao.LocalRetirada = null;

            if (chkInformarLocalDeRetirada.Checked)
            {
                _notaFiscalEmEdicao.LocalRetirada = new LocalRetiradaNotaFiscal();

                _notaFiscalEmEdicao.LocalRetirada.Bairro = txtBairroLocalRetirada.Text;
                _notaFiscalEmEdicao.LocalRetirada.CodigoMunicipio = cboCidadeLocalRetirada.EditValue.ToInt();
                _notaFiscalEmEdicao.LocalRetirada.Complemento = txtComplementoLocalRetirada.Text;
                _notaFiscalEmEdicao.LocalRetirada.CpfCnpj = txtCpfCnpjLocalRetirada.Text;
                _notaFiscalEmEdicao.LocalRetirada.Logradouro = txtRuaLocalRetirada.Text;
                _notaFiscalEmEdicao.LocalRetirada.NomeMunicipio = cboCidadeLocalRetirada.Text;
                _notaFiscalEmEdicao.LocalRetirada.Numero = txtNumeroLocalRetirada.Text;
                _notaFiscalEmEdicao.LocalRetirada.TipoPessoa = rdbTipoPessoaFisicaLocalRetirada.Checked ? EnumTipoPessoa.PESSOAFISICA : EnumTipoPessoa.PESSOAJURIDICA;
                _notaFiscalEmEdicao.LocalRetirada.UF = cboEstadoLocalRetirada.EditValue.ToString();
            }
            else
            {
                _notaFiscalEmEdicao.LocalRetirada = null;
            }
        }

        private void PreenchaObjetoDadosCobrancaNotaFiscal()
        {
            string numeroFatura = txtNumeroFaturaFinanceiro.Text.ToStringNull();
            double? valorOriginalFatura = txtValorOriginalFaturaFinanceiro.Text.ToDoubleNullabel();
            double? valorDesconto = txtValorDescontoFaturaFinanceiro.Text.ToDoubleNullabel();
            double? valorLiquido = txtValorLiquidoFaturaFinanceiro.Text.ToDoubleNullabel();

            if (numeroFatura != null || valorOriginalFatura != null || valorDesconto != null || valorLiquido != null)
            {
                _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal = new FaturaNotaFiscal();

                _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.NumeroFatura = numeroFatura;
                _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.ValorDesconto = valorDesconto;
                _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.ValorLiquido = valorLiquido;
                _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.ValorOriginalFatura = valorOriginalFatura;
            }
            else
            {
                _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal = null;
            }

            _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas = _listaDuplicatas;
        }

        private void PreenchaObjetoInformacoesComercioExterior()
        {
            if (chkInformarDadosComercioExterior.Checked)
            {
                _notaFiscalEmEdicao.InformacoesComercioExteriorNotaFiscal = new InformacoesComercioExteriorNotaFiscal();

                _notaFiscalEmEdicao.InformacoesComercioExteriorNotaFiscal.DescricaoLocalDespacho = txtDescricaoLocalDespachoComercioExterior.Text.ToStringNull();
                _notaFiscalEmEdicao.InformacoesComercioExteriorNotaFiscal.DescricaoLocalEmbarque = txtDescricaoLocalEmbarqueFronteiraComercioExterior.Text.ToStringNull();
                _notaFiscalEmEdicao.InformacoesComercioExteriorNotaFiscal.UFEmbarque = cboUFComercioExterior.EditValue.ToStringNull();
            }
            else
            {
                _notaFiscalEmEdicao.InformacoesComercioExteriorNotaFiscal = null;
            }
        }

        private void PreenchaObjetoInformacoesCompra()
        {
            if (chkInformaDadosCompraInformacoesCompra.Checked)
            {
                _notaFiscalEmEdicao.InformacoesCompraNotaFiscal = new InformacoesCompraNotaFiscal();

                _notaFiscalEmEdicao.InformacoesCompraNotaFiscal.Contrato = txtContratoInformacoesCompra.Text.ToStringNull();
                _notaFiscalEmEdicao.InformacoesCompraNotaFiscal.NotaEmpenho = txtNotaEmpenhoInformacoesCompra.Text.ToStringNull();
                _notaFiscalEmEdicao.InformacoesCompraNotaFiscal.Pedido = txtPedidoInformacoesCompra.Text.ToStringNull();
            }
            else
            {
                _notaFiscalEmEdicao.InformacoesCompraNotaFiscal = null;
            }
        }

        private void PreenchaObjetoTotaisNotaFiscal()
        {
            _notaFiscalEmEdicao.TotaisNotaFiscal.BaseCalculoIcms = txtBaseCalculoIcmsTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.BaseCalculoIcmsST = txtBaseCalculoIcmsSTTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.Cofins = txtValorTotalCofinsTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.Desconto = txtValorTotalDescontoTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.Frete = txtValorTotalFreteTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.Icms = txtValorIcmsTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.ValorSubstituicaoTributaria = txtValorIcmsSTTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.IcmsDesoneracao = txtValorIcmsDesoneradoTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.ImpostoDeImportacao = txtValorTotalImpostoImportacaoTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.Ipi = txtValorTotalIPITotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.ValorSeguro = txtValorTotalSeguroTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.OutrosValores = txtValorOutrasDespesasTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.Pis = txtValorTotalPISTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.Produtos = txtValorTotalProdutosTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.TotalTributacao = txtValorAproximadoTributosTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.TotalTributacaoEstadual = txtTotalTributacaoEstadualTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.TotalTributacaoFederal = txtTributacaoFederalTotais.Text.ToDouble();
            _notaFiscalEmEdicao.TotaisNotaFiscal.ValorFCP = txtFCPTotais.Text.ToDoubleNullabel();
            _notaFiscalEmEdicao.TotaisNotaFiscal.ValorInterestadualDestino = txtIcmsInterestadualDestinoTotais.Text.ToDoubleNullabel();
            _notaFiscalEmEdicao.TotaisNotaFiscal.ValorInterestadualOrigem = txtIcmsInterestadualOrigemTotais.Text.ToDoubleNullabel();
            _notaFiscalEmEdicao.TotaisNotaFiscal.ValorNotaFiscal = txtValorTotalNFeTotais.Text.ToDouble();

            _notaFiscalEmEdicao.TotaisNotaFiscal.ValorSubstituicaoTributaria = txtValorIcmsSTTotais.Text.ToDouble();

            if (txtBaseCalculoImpostoRendaTotais.Text.ToDoubleNullabel() != null ||
                txtBaseCalculoRetencaoPrevidenciaSocialTotais.Text.ToDoubleNullabel() != null ||
                txtValorRetencaoPrevidenciaSocialTotais.Text.ToDoubleNullabel() != null ||
                txtValorRetidoCofinsTotais.Text.ToDoubleNullabel() != null ||
                txtValoRetidoCSLLTotais.Text.ToDoubleNullabel() != null ||
                txtValorRetidoPisTotais.Text.ToDoubleNullabel() != null)
            {
                _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal = new RetencaoTributosNotaFiscal();
                _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.BaseCalculoIRRF = txtBaseCalculoImpostoRendaTotais.Text.ToDoubleNullabel();
                _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.BaseCalculoRetencaoPrevidenciaSocial = txtBaseCalculoRetencaoPrevidenciaSocialTotais.Text.ToDoubleNullabel();
                _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetencaoPrevidenciaSocial = txtValorRetencaoPrevidenciaSocialTotais.Text.ToDoubleNullabel();
                _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetidoCofins = txtValorRetidoCofinsTotais.Text.ToDoubleNullabel();
                _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetidoCsll = txtValoRetidoCSLLTotais.Text.ToDoubleNullabel();
                _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetidoPis = txtValorRetidoPisTotais.Text.ToDoubleNullabel();
            }
            else
            {
                _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal = null;
            }
        }

        private static char[] GetDiacritics()
        {
            char[] accents = new char[256];

            for (int i = 0; i < 256; i++)
                accents[i] = (char)i;

            accents[(byte)'á'] = accents[(byte)'à'] = accents[(byte)'ã'] = accents[(byte)'â'] = accents[(byte)'ä'] = 'a';
            accents[(byte)'Á'] = accents[(byte)'À'] = accents[(byte)'Ã'] = accents[(byte)'Â'] = accents[(byte)'Ä'] = 'A';

            accents[(byte)'é'] = accents[(byte)'è'] = accents[(byte)'ê'] = accents[(byte)'ë'] = 'e';
            accents[(byte)'É'] = accents[(byte)'È'] = accents[(byte)'Ê'] = accents[(byte)'Ë'] = 'E';

            accents[(byte)'í'] = accents[(byte)'ì'] = accents[(byte)'î'] = accents[(byte)'ï'] = 'i';
            accents[(byte)'Í'] = accents[(byte)'Ì'] = accents[(byte)'Î'] = accents[(byte)'Ï'] = 'I';

            accents[(byte)'ó'] = accents[(byte)'ò'] = accents[(byte)'ô'] = accents[(byte)'õ'] = accents[(byte)'ö'] = 'o';
            accents[(byte)'Ó'] = accents[(byte)'Ò'] = accents[(byte)'Ô'] = accents[(byte)'Õ'] = accents[(byte)'Ö'] = 'O';

            accents[(byte)'ú'] = accents[(byte)'ù'] = accents[(byte)'û'] = accents[(byte)'ü'] = 'u';
            accents[(byte)'Ú'] = accents[(byte)'Ù'] = accents[(byte)'Û'] = accents[(byte)'Ü'] = 'U';

            accents[(byte)'ç'] = 'c';
            accents[(byte)'Ç'] = 'C';

            accents[(byte)'ñ'] = 'n';
            accents[(byte)'Ñ'] = 'N';

            accents[(byte)'ÿ'] = accents[(byte)'ý'] = 'y';
            accents[(byte)'Ý'] = 'Y';


            return accents;
        }

        private static string RemoveSpecialCharacters(string text)
        {
            string ret = text;

            return ret.Replace("\r\n", " ");

        }

        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] > 255)
                    sb.Append(text[i]);
                else
                    sb.Append(s_Diacritics[text[i]]);
            }

            return sb.ToString();
        }

        private void PreenchaObjetoInformacoesAdicionarNotaFiscal()
        {
            
            //Se não tiver nada digitado o sistema vai acrescentar o valor padrão
            if (txtInformacoesComplementares.Text == string.Empty)
                txtInformacoesComplementares.Text = string.Concat("Trib aprox R$: ", txtTributacaoFederalTotais.Text, " Federal e " + txtTotalTributacaoEstadualTotais.Text, " Estadual - Fonte: IBPT");
            
                if (txtInformacoesComplementares.Text.StartsWith("TRIB") || txtInformacoesComplementares.Text.StartsWith("Trib"))
                {
                    string fraseRemovida="";
                    string numeroDeCaracteresDefraseARemover = string.Concat("Trib aprox R$: ", txtTributacaoFederalTotais.Text, " Federal e " + txtTotalTributacaoEstadualTotais.Text, " Estadual - Fonte: IBPT");

                    if (txtInformacoesComplementares.Text.Length>=numeroDeCaracteresDefraseARemover.Length)
                            fraseRemovida = txtInformacoesComplementares.Text.Remove(0, numeroDeCaracteresDefraseARemover.Length);

                    if (fraseRemovida.Trim() != string.Empty)
                    {
                        fraseRemovida = fraseRemovida.Trim().RemovaEspacosEmBrancoDoInicioEFim();
                        txtInformacoesComplementares.Text = string.Concat("Trib aprox R$: ", txtTributacaoFederalTotais.Text, " Federal e " + txtTotalTributacaoEstadualTotais.Text, " Estadual - Fonte: IBPT") + " " + fraseRemovida.Trim();
                    }
                    else
                        txtInformacoesComplementares.Text = string.Concat("Trib aprox R$: ", txtTributacaoFederalTotais.Text, " Federal e " + txtTotalTributacaoEstadualTotais.Text, " Estadual - Fonte: IBPT");
                }
                else
                    txtInformacoesComplementares.Text = string.Concat("Trib aprox R$: ", txtTributacaoFederalTotais.Text, " Federal e " + txtTotalTributacaoEstadualTotais.Text, " Estadual - Fonte: IBPT") + " " + txtInformacoesComplementares.Text.RemovaEspacosEmBrancoDoInicioEFim();
            
            
            _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Observacoes = RemoveDiacritics(RemoveSpecialCharacters(txtInformacoesComplementares.Text));
            _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.ObservacoesFisco = RemoveDiacritics(RemoveSpecialCharacters(txtInformacoesFisco.Text));
        }

        private void PreenchaObjetoInformacoesDocumentoOrigemNotaFiscal()
        {
            _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal = _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal ?? new InformacoesDocumentoOrigemNotaFiscal();

            _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem = EnumTipoDocumento.OUTRASSAIDAS;
            _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.UsuarioId = Sessao.PessoaLogada.Id;
            _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.UsurioNome = Sessao.PessoaLogada.DadosGerais.Razao;
        }

        #endregion

        #region " EDIÇÃO NOTA FISCAL "

        private void EditeNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null)
            {
                _listaItensNotaFiscal = _notaFiscalEmEdicao.ListaItens.ToList();
            }

            PreenchaCamposIdentificacaoNotaFiscal();
            PreenchaCamposDestinatarioNotaFiscal();
            PreenchaCamposLocalEntregaRetiradaNotaFiscal();
            PreenchaCamposTransporteNotaFiscal();
            PreenchaCamposFinanceiroNotaFiscal();
            PreenchaCamposTotaisNotaFiscal();
            PreenchaCamposAdicionaisNotaFiscal();
            PreenchaCamposInformacoesComercioExteriorNotaFiscal();
            PreenchaCamposInformacoesCompraNotaFiscal();
            PreenchaOutrasInformacoesNotaFiscal();
            
            if (_notaFiscalEmEdicao != null)
            {   
                //_listaItensNotaFiscal = _notaFiscalEmEdicao.ListaItens.ToList();
                PreenchaCodigoNcm();
                AtualizeGridItens();
                CalculeTotaisNotaFiscal();
            }
            else
                gcItens.DataSource = null;
        }

        private void PreenchaCodigoNcm()
        {
            if (_listaItensNotaFiscal != null)
            {
                int indexLista = 0;
                foreach (var ncm in _listaItensNotaFiscal)
                {
                    ServicoProduto servicoProduto = new ServicoProduto();
                    
                    var produto = servicoProduto.ConsulteProdutoAtivo(_listaItensNotaFiscal[indexLista].Produto.Id);

                    if (produto != null)
                    {
                        _listaItensNotaFiscal[indexLista].Ncm = produto.ContabilFiscal.Ncm != null ? produto.ContabilFiscal.Ncm.CodigoNcm : null;
                        _listaItensNotaFiscal[indexLista].Cest = produto.ContabilFiscal.Ncm != null ? produto.ContabilFiscal.Ncm.Cest : null;
                    }
                    indexLista++;
                }
            }          
        }
        
        private void PreenchaCamposIdentificacaoNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null && _notaFiscalEmEdicao.IdentificacaoNotaFiscal != null)
            {                
                txtNaturezaOperacao.Text = _notaFiscalEmEdicao.IdentificacaoNotaFiscal.DescricaoNaturezaOperacao;
                txtIdNaturezaOperacao.Text = _notaFiscalEmEdicao.IdentificacaoNotaFiscal.NaturezaOperacao!=null? _notaFiscalEmEdicao.IdentificacaoNotaFiscal.NaturezaOperacao.Id.ToString():null;

                cboIndicadorDePresenca.EditValue = _notaFiscalEmEdicao.IdentificacaoNotaFiscal.IndicacaoPresenca;
                cboCondicaoPagamento.EditValue = _notaFiscalEmEdicao.IdentificacaoNotaFiscal.FormaPagamento;

                rdbTipoOperacaoEntrada.Checked = true;
                rdbTipoOperacaoNotaSaida.Checked = _notaFiscalEmEdicao.IdentificacaoNotaFiscal.NotaSaida;

                rdbRevenda.Checked = true;
                rdbConsumidorFinal.Checked = _notaFiscalEmEdicao.IdentificacaoNotaFiscal.ConsumidorFinal;

                cboFinalidadeNFe.EditValue = _notaFiscalEmEdicao.IdentificacaoNotaFiscal.FinalidadeEmissaoNFe;

                _notasFiscaisReferenciadas = _notaFiscalEmEdicao.ListaNotasReferenciadas.ToList();

                PreenchaGridNotasReferenciadas();
            }
            else
            {
                txtIdNaturezaOperacao.Text = string.Empty;
                txtNaturezaOperacao.Text = string.Empty;
                txtChaveAcessoNotaReferenciada.Text = string.Empty;
                cboTipoNotaReferenciada.EditValue = null;
                cboIndicadorDePresenca.EditValue = null;
                cboCondicaoPagamento.EditValue = null;

                rdbTipoOperacaoNotaSaida.Checked = true;
                rdbConsumidorFinal.Checked = true;

                cboFinalidadeNFe.EditValue = EnumFinalidadeEmissaoNfe.NFENORMAL;

                _notasFiscaisReferenciadas = new List<NotaFiscalReferenciada>();

                PreenchaGridNotasReferenciadas();
            }
        }

        private void PreenchaCamposDestinatarioNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null && _notaFiscalEmEdicao.Destinatario != null)
            {
                
                txtIdParceiroDestinatario.Text = _notaFiscalEmEdicao.Destinatario.Pessoa != null? _notaFiscalEmEdicao.Destinatario.Pessoa.Id.ToString():null;

                rdbTipoPessoaJuridicaDestinatario.Checked = true;
                rdbTipoPessoaFisicaDestinatario.Checked = _notaFiscalEmEdicao.Destinatario.TipoPessoa == EnumTipoPessoa.PESSOAFISICA;

                txtCpfCnpjDestinatario.Text = _notaFiscalEmEdicao.Destinatario.CnpjCpf;
                txtRazaoSocialParceiroDestinatario.Text = _notaFiscalEmEdicao.Destinatario.RazaoSocialOuNomeDestinatario;
                txtInscricaoEstadualDestinatario.Text = _notaFiscalEmEdicao.Destinatario.InscricaoEstadual;
                txtInscricaoSuframaDestinatario.Text = _notaFiscalEmEdicao.Destinatario.InscricaoSuframa;
                txtEmailDestinatario.Text = _notaFiscalEmEdicao.Destinatario.Email;
                txtIdEstrangeiroDestinatario.Text = _notaFiscalEmEdicao.Destinatario.IdEstrangeiro;

                if (_notaFiscalEmEdicao.Destinatario.Telefone != null)
                {
                    if (_notaFiscalEmEdicao.Destinatario.Telefone != 0)
                    {
                        txtDDDDestinatario.Text = _notaFiscalEmEdicao.Destinatario.Telefone.ToString().Substring(0, 2);
                        txtNumeroTelefoneDestinatario.Text = _notaFiscalEmEdicao.Destinatario.Telefone.ToString().Substring(2); ;
                    }
                    else
                    {
                        txtDDDDestinatario.Text = string.Empty;
                        txtNumeroTelefoneDestinatario.Text = string.Empty;
                    }
                }
                else
                {
                    txtDDDDestinatario.Text = string.Empty;
                    txtNumeroTelefoneDestinatario.Text = string.Empty;
                }

                txtCepDestinatario.Text = _notaFiscalEmEdicao.Destinatario.Cep;
                cboEstadoDestinatario.EditValue = _notaFiscalEmEdicao.Destinatario.UF;
                cboCidadeDestinatario.EditValue = _notaFiscalEmEdicao.Destinatario.CodigoMunicipio.ToString();
                txtBairroDestinatario.Text = _notaFiscalEmEdicao.Destinatario.Bairro;
                txtRuaDestinatario.Text = _notaFiscalEmEdicao.Destinatario.Logradouro;
                txtComplementoDestinatario.Text = _notaFiscalEmEdicao.Destinatario.Complemento;
                txtNumeroDestinatario.Text = _notaFiscalEmEdicao.Destinatario.Numero;

                chkParceiroResideExteriorDestinatario.Checked = _notaFiscalEmEdicao.Destinatario.ParceiroResideExterior;
                cboPaisDestinatario.EditValue = _notaFiscalEmEdicao.Destinatario.CodigoPais;
            }
            else
            {
                txtIdParceiroDestinatario.Text = string.Empty;

                rdbTipoPessoaFisicaDestinatario.Checked = true;

                txtCpfCnpjDestinatario.Text = string.Empty;
                txtRazaoSocialParceiroDestinatario.Text = string.Empty;
                txtInscricaoEstadualDestinatario.Text = string.Empty;
                txtInscricaoSuframaDestinatario.Text = string.Empty;
                txtEmailDestinatario.Text = string.Empty;
                txtIdEstrangeiroDestinatario.Text = string.Empty;
                txtDDDDestinatario.Text = string.Empty;
                txtNumeroTelefoneDestinatario.Text = string.Empty;

                txtCepDestinatario.Text = string.Empty;
                cboEstadoDestinatario.EditValue = null;
                cboCidadeDestinatario.EditValue = null;
                txtBairroDestinatario.Text = string.Empty;
                txtRuaDestinatario.Text = string.Empty;
                txtComplementoDestinatario.Text = string.Empty;
                txtNumeroDestinatario.Text = string.Empty;

                chkParceiroResideExteriorDestinatario.Checked = false;
                cboPaisDestinatario.EditValue = null;
            }
        }

        private void PreenchaCamposLocalEntregaRetiradaNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null && _notaFiscalEmEdicao.LocalEntrega != null)
            {
                chkInformarLocalEntrega.Checked = true;

                rdbTipoPessoaJuridicaLocalEntrega.Checked = true;
                rdbTipoPessoaFisicaLocalEntrega.Checked = _notaFiscalEmEdicao.LocalEntrega.TipoPessoa == EnumTipoPessoa.PESSOAFISICA;

                txtCpfCnpjLocalEntrega.Text = _notaFiscalEmEdicao.LocalEntrega.CpfCnpj;
                cboEstadoLocalEntrega.EditValue = _notaFiscalEmEdicao.LocalEntrega.UF;
                cboCidadeLocalEntrega.EditValue = _notaFiscalEmEdicao.LocalEntrega.CodigoMunicipio.ToString();
                txtBairroLocalEntrega.Text = _notaFiscalEmEdicao.LocalEntrega.Bairro;
                txtRuaLocalEntrega.Text = _notaFiscalEmEdicao.LocalEntrega.Logradouro;
                txtComplementoLocalEntrega.Text = _notaFiscalEmEdicao.LocalEntrega.Complemento;
                txtNumeroLocalEntrega.Text = _notaFiscalEmEdicao.LocalEntrega.Numero;
            }
            else
            {
                chkInformarLocalEntrega.Checked = false;

                rdbTipoPessoaFisicaLocalEntrega.Checked = true;

                txtCpfCnpjLocalEntrega.Text = string.Empty;
                cboEstadoLocalEntrega.EditValue = null;
                cboCidadeLocalEntrega.EditValue = null;
                txtBairroLocalEntrega.Text = string.Empty;
                txtRuaLocalEntrega.Text = string.Empty;
                txtComplementoLocalEntrega.Text = string.Empty;
                txtNumeroLocalEntrega.Text = string.Empty;
            }

            if (_notaFiscalEmEdicao != null && _notaFiscalEmEdicao.LocalRetirada != null)
            {
                chkInformarLocalDeRetirada.Checked = true;

                rdbTipoPessoaJuridicaLocalRetirada.Checked = true;
                rdbTipoPessoaFisicaLocalRetirada.Checked = _notaFiscalEmEdicao.LocalRetirada.TipoPessoa == EnumTipoPessoa.PESSOAFISICA;

                txtCpfCnpjLocalRetirada.Text = _notaFiscalEmEdicao.LocalRetirada.CpfCnpj;
                cboEstadoLocalRetirada.EditValue = _notaFiscalEmEdicao.LocalRetirada.UF;
                cboCidadeLocalRetirada.EditValue = _notaFiscalEmEdicao.LocalRetirada.CodigoMunicipio.ToString();
                txtBairroLocalRetirada.Text = _notaFiscalEmEdicao.LocalRetirada.Bairro;
                txtRuaLocalRetirada.Text = _notaFiscalEmEdicao.LocalRetirada.Logradouro;
                txtComplementoLocalRetirada.Text = _notaFiscalEmEdicao.LocalRetirada.Complemento;
                txtNumeroLocalRetirada.Text = _notaFiscalEmEdicao.LocalRetirada.Numero;
            }
            else
            {
                chkInformarLocalDeRetirada.Checked = false;

                rdbTipoPessoaFisicaLocalEntrega.Checked = true;

                txtCpfCnpjLocalRetirada.Text = string.Empty;
                cboEstadoLocalRetirada.EditValue = null;
                cboCidadeLocalRetirada.EditValue = null;
                txtBairroLocalRetirada.Text = string.Empty;
                txtRuaLocalRetirada.Text = string.Empty;
                txtComplementoLocalRetirada.Text = string.Empty;
                txtNumeroLocalRetirada.Text = string.Empty;
            }
        }

        private void PreenchaCamposTransporteNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null)
            {
                cboTipoFrete.EditValue = (EnumTipoFrete)(_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.TipoFrete);
                chkInformarTransportadora.Checked = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.TransportadoraId != null ? true: false;
                cboTransportadoras.EditValue = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.TransportadoraId != null ? _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.TransportadoraId:null;
                txtVolume.Text = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Volume.ToString();
            }
            else
            {
                cboTipoFrete.EditValue = EnumTipoFrete.SEMCOBRANCADEFRETE;
                chkInformarTransportadora.Checked = false;
                cboTransportadoras.EditValue = false;
                txtVolume.Text = string.Empty;
            }
        }

        private void PreenchaCamposFinanceiroNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null && _notaFiscalEmEdicao.DadosCobranca != null && _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal != null)
            {
                txtNumeroFaturaFinanceiro.Text = _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.NumeroFatura;
                txtValorOriginalFaturaFinanceiro.Text = _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.ValorOriginalFatura != null ? _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.ValorOriginalFatura.Value.ToString("0.00") : string.Empty;
                txtValorDescontoFaturaFinanceiro.Text = _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.ValorDesconto != null ? _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.ValorDesconto.Value.ToString("0.00") : string.Empty;
                txtValorLiquidoFaturaFinanceiro.Text = _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.ValorLiquido != null ? _notaFiscalEmEdicao.DadosCobranca.FaturaNotaFiscal.ValorLiquido.Value.ToString("0.00") : string.Empty;
            }
            else
            {
                txtNumeroFaturaFinanceiro.Text = string.Empty;
                txtValorOriginalFaturaFinanceiro.Text = string.Empty;
                txtValorDescontoFaturaFinanceiro.Text = string.Empty;
                txtValorLiquidoFaturaFinanceiro.Text = string.Empty;
            }

            if (_notaFiscalEmEdicao != null)
            {
                if (_notaFiscalEmEdicao.DadosCobranca != null)
                    if (_notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas != null)
                        _listaDuplicatas = _notaFiscalEmEdicao.DadosCobranca.ListaDuplicatas.ToList();
            }
            else
            {
                _listaDuplicatas = new List<DuplicataNotaFiscal>();
            }

            PreenchaGridFinanceiro();
        }

        private void PreenchaCamposTotaisNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null)
            {
                if (_notaFiscalEmEdicao.TotaisNotaFiscal != null)
                {
                    txtBaseCalculoIcmsTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.BaseCalculoIcms.ToString("0.00");
                    txtBaseCalculoIcmsSTTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.BaseCalculoIcmsST.ToString("0.00");
                    txtValorTotalCofinsTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.Cofins.ToString("0.00");
                    txtValorTotalDescontoTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.Desconto.ToString("0.00");
                    txtValorTotalFreteTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.Frete.ToString("0.00");
                    txtValorIcmsTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.Icms.ToString("0.00");
                    txtValorIcmsDesoneradoTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.IcmsDesoneracao.ToString("0.00");
                    txtValorTotalImpostoImportacaoTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.ImpostoDeImportacao.ToString("0.00");
                    txtValorTotalIPITotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.Ipi.ToString("0.00");
                    txtValorOutrasDespesasTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.OutrosValores.ToString("0.00");
                    txtValorTotalPISTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.Pis.ToString("0.00");
                    txtValorTotalProdutosTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.Produtos.ToString("0.00");
                    txtValorAproximadoTributosTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.TotalTributacao.ToString("0.00");
                    txtTotalTributacaoEstadualTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.TotalTributacaoEstadual.ToString("0.00");
                    txtTributacaoFederalTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.TotalTributacaoFederal.ToString("0.00");
                    txtFCPTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.ValorFCP.ToString();
                    txtIcmsInterestadualDestinoTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.ValorInterestadualDestino.ToString();
                    txtIcmsInterestadualOrigemTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.ValorInterestadualOrigem.ToString();
                    txtValorTotalNFeTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.ValorNotaFiscal.ToString("0.00");

                    if (_notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal != null)
                    {
                        txtBaseCalculoImpostoRendaTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.BaseCalculoIRRF.ToString();
                        txtBaseCalculoRetencaoPrevidenciaSocialTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.BaseCalculoRetencaoPrevidenciaSocial.ToString();
                        txtValorRetencaoPrevidenciaSocialTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetencaoPrevidenciaSocial.ToString();
                        txtValorRetidoCofinsTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetidoCofins.ToString();
                        txtValoRetidoCSLLTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetidoCsll.ToString();
                        txtValorRetidoPisTotais.Text = _notaFiscalEmEdicao.TotaisNotaFiscal.RetencaoTributosNotaFiscal.ValorRetidoPis.ToString();
                    }
                    else
                    {
                        txtBaseCalculoImpostoRendaTotais.Text = string.Empty;
                        txtBaseCalculoRetencaoPrevidenciaSocialTotais.Text = string.Empty;
                        txtValorRetencaoPrevidenciaSocialTotais.Text = string.Empty;
                        txtValorRetidoCofinsTotais.Text = string.Empty;
                        txtValoRetidoCSLLTotais.Text = string.Empty;
                        txtValorRetidoPisTotais.Text = string.Empty;
                    }
                }
                else
                {
                    txtBaseCalculoIcmsTotais.Text = string.Empty;
                    txtBaseCalculoIcmsSTTotais.Text = string.Empty;
                    txtValorTotalCofinsTotais.Text = string.Empty;
                    txtValorTotalDescontoTotais.Text = string.Empty;
                    txtValorTotalFreteTotais.Text = string.Empty;
                    txtValorIcmsTotais.Text = string.Empty;
                    txtValorIcmsDesoneradoTotais.Text = string.Empty;
                    txtValorTotalImpostoImportacaoTotais.Text = string.Empty;
                    txtValorTotalIPITotais.Text = string.Empty;
                    txtValorOutrasDespesasTotais.Text = string.Empty;
                    txtValorTotalPISTotais.Text = string.Empty;
                    txtValorTotalProdutosTotais.Text = string.Empty;
                    txtValorAproximadoTributosTotais.Text = string.Empty;
                    txtTotalTributacaoEstadualTotais.Text = string.Empty;
                    txtTributacaoFederalTotais.Text = string.Empty;
                    txtFCPTotais.Text = string.Empty;
                    txtIcmsInterestadualDestinoTotais.Text = string.Empty;
                    txtIcmsInterestadualOrigemTotais.Text = string.Empty;
                    txtValorTotalNFeTotais.Text = string.Empty;
                }
            }
        }

        private void PreenchaCamposAdicionaisNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null && _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal != null)
            {   
                txtInformacoesComplementares.Text = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Observacoes;

                txtInformacoesFisco.Text = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.ObservacoesFisco;
            }
            else
            {
                txtInformacoesComplementares.Text = string.Empty;
                txtInformacoesFisco.Text = string.Empty;
            }
        }

        private void PreenchaCamposInformacoesComercioExteriorNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null && _notaFiscalEmEdicao.InformacoesComercioExteriorNotaFiscal != null)
            {
                chkInformarDadosComercioExterior.Checked = true;

                cboUFComercioExterior.EditValue = _notaFiscalEmEdicao.InformacoesComercioExteriorNotaFiscal.UFEmbarque;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Text = _notaFiscalEmEdicao.InformacoesComercioExteriorNotaFiscal.DescricaoLocalEmbarque;
                txtDescricaoLocalDespachoComercioExterior.Text = _notaFiscalEmEdicao.InformacoesComercioExteriorNotaFiscal.DescricaoLocalDespacho;
            }
            else
            {
                chkInformarDadosComercioExterior.Checked = false;

                cboUFComercioExterior.EditValue = null;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Text = string.Empty;
                txtDescricaoLocalDespachoComercioExterior.Text = string.Empty;
            }
        }

        private void PreenchaCamposInformacoesCompraNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null && _notaFiscalEmEdicao.InformacoesCompraNotaFiscal != null)
            {
                chkInformaDadosCompraInformacoesCompra.Checked = true;

                txtNotaEmpenhoInformacoesCompra.Text = _notaFiscalEmEdicao.InformacoesCompraNotaFiscal.NotaEmpenho;
                txtPedidoInformacoesCompra.Text = _notaFiscalEmEdicao.InformacoesCompraNotaFiscal.Pedido;
                txtContratoInformacoesCompra.Text = _notaFiscalEmEdicao.InformacoesCompraNotaFiscal.Contrato;
            }
            else
            {
                chkInformaDadosCompraInformacoesCompra.Checked = false;

                txtNotaEmpenhoInformacoesCompra.Text = string.Empty;
                txtPedidoInformacoesCompra.Text = string.Empty;
                txtContratoInformacoesCompra.Text = string.Empty;
            }
        }

        private void PreenchaOutrasInformacoesNotaFiscal()
        {
            if (_notaFiscalEmEdicao != null)
            {
                if ((_notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.DISPONIVEL ||
                    _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA) &&
                    _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.Origem == EnumTipoDocumento.OUTRASSAIDAS)
                {
                    txtIdOutrasSaidas.Text = _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId.ToString();
                    txtDataCadastro.Text = _notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DataElaboracao.ToString("dd/MM/yyyy");
                    txtStatusNFe.Text = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.Status.Descricao();
                }
                else
                {
                    cboTipoNotaReferenciada.EditValue = EnumTipoNotaReferenciada.NFEOUNFCE;
                    txtChaveAcessoNotaReferenciada.Text = _notaFiscalEmEdicao.InformacoesGeraisNotaFiscal.ChaveDeAcesso;
                    txtInformacoesComplementares.Text = string.Empty;
                }
            }
            else
            {
                txtIdOutrasSaidas.Text = string.Empty;
                txtDataCadastro.Text = string.Empty;
                txtStatusNFe.Text = string.Empty;
            }
        }

        private void VerificaCamposCboPreenchidos()
        {   
            if (cboEstadoDestinatario.EditValue != null || chkParceiroResideExteriorDestinatario.Checked)
                cboEstadoDestinatario.Obrigatorio = false;
            else
                cboEstadoDestinatario.Obrigatorio = true;

            if (cboCidadeDestinatario.EditValue != null || chkParceiroResideExteriorDestinatario.Checked)
                cboCidadeDestinatario.Obrigatorio = false;
            else
                cboCidadeDestinatario.Obrigatorio = true;

            if (cboTipoFrete.EditValue != null)
                cboTipoFrete.Obrigatorio = false;
            else
                cboTipoFrete.Obrigatorio = true;
        }

        private bool verificaFinanceiro()
        {
            if (gcParcelasFinanceiro.DataSource == null || gcParcelasFinanceiro.Views.Count==0)
            {   
                return true;                
            }
            else
            {
                if (_listaDuplicatas == null) return true;
                
                foreach (var item in _listaDuplicatas)
                {
                    if (item.FormaPagamento == null || item.CondicaoPagamento == null )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void VerificaCamposCboImpostosPreenchidos()
        {
            if (cboOrigem.EditValue != null)
                cboOrigem.Obrigatorio = false;
            else
                cboOrigem.Obrigatorio = true;

            if (cboCstCsosn.EditValue != null)
                cboCstCsosn.Obrigatorio = false;
            else
                cboCstCsosn.Obrigatorio = true;            
        }

        private void calculeValorTotalDoItem()
        {
            CalculosPedidoDeVenda calculosPedidoDeVenda = new CalculosPedidoDeVenda();

            double quantidade = 1;
            var descontoTotal = CalculosPedidoDeVenda.CalculeTotalDesconto(txtValorUnitarioItem.Text.ToDouble(),
                                                                                                              quantidade,
                                                                                                              txtValorDescontoItem.Text.ToDouble(),
                                                                                                              rdbDescontoItemPercentual.Checked);


            double valorTotalItem = calculosPedidoDeVenda.RetorneValorTotalItem(txtValorUnitarioItem.Text.ToDouble(),
                                                                                txtQuantidadeItem.Text.ToDouble(),
                                                                                descontoTotal,
                                                                                txtValorFreteItem.Text.ToDouble(),
                                                                                txtValorIpi.Text.ToDouble(),
                                                                                txtValorIcmsSt.Text.ToDouble(),
                                                                                txtValorSeguroItem.Text.ToDouble(),
                                                                                txtValorOutrasDespesasItem.Text.ToDouble());
            if(txtQuantidadeItem.Text != string.Empty)
                txtValorTotalItem.Text = valorTotalItem.ToString("0.00");
        }

        private void PreenchaInformacoesPagamentosNotaVersao4_00()
        {
            var pedidoDeVenda = new ServicoPedidoDeVenda().Consulte(_notaFiscalEmEdicao.InformacoesDocumentoOrigemNotaFiscal.DocumentoId);

            if (pedidoDeVenda == null) return;

            _notaFiscalEmEdicao.DadosCobranca.ListaDeParcelasVendas = pedidoDeVenda.ListaParcelasPedidoDeVenda.ToList();

            var formaPgtoPesquisa = pedidoDeVenda.ListaParcelasPedidoDeVenda.Select(x => x.FormaPagamento.TipoFormaPagamento).FirstOrDefault();

            _notaFiscalEmEdicao.DadosCobranca.TotalDePagamento = pedidoDeVenda.ListaParcelasPedidoDeVenda.Sum(x => x.Valor);
            _notaFiscalEmEdicao.DadosCobranca.FormaPagamentoNF = new ServicoNotaFiscal().RetorneFormaPagamentoParaNF(formaPgtoPesquisa);
            _notaFiscalEmEdicao.DadosCobranca.CondicaoVistaPrazo = retorneCondicaoPagamentoVistaPrazo(pedidoDeVenda.ListaParcelasPedidoDeVenda.ToList());
        }

        private EnumCondicaoVistaPrazo retorneCondicaoPagamentoVistaPrazo(List<ParcelaPedidoDeVenda> ListaParcelas)
        {
            var idCondicao = ListaParcelas.Select(x => x.CondicaoPagamento.Id).FirstOrDefault();

            var condicao = new ServicoCondicaoPagamento().Consulte(idCondicao);

            var nDias = condicao.ListaDeParcelas.Select(x => x.Dias).FirstOrDefault();

            if (nDias > 1)
            {
                return EnumCondicaoVistaPrazo.APrazo;
            }
            else
            {
                return EnumCondicaoVistaPrazo.AVista;
            }
        }

        private void PreenchaCboCondicaoPagamentoFinanceiro()
        {
            List<CondicaoPagamento> listaCondicoes = new List<CondicaoPagamento>();
            
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            if (cboFormaPagamento.EditValue == null) return;

            int nFormaPgto = RetorneNumeroDaFormaDePagamento((EnumTipoFormaPagamento)cboFormaPagamento.EditValue);

            var formaPagamento = servicoFormaPagamento.Consulte(nFormaPgto);

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
            else
            {
                ObjetoDescricaoValor Outros = new ObjetoDescricaoValor();

                Outros.Descricao = "OUTROS";
                Outros.Valor = "0";

                List<ObjetoDescricaoValor> listaOutros = new List<ObjetoDescricaoValor>();

                listaOutros.Add(Outros);

                cboCodicaoPagamentoFinanceiro.Properties.DisplayMember = "Descricao";
                cboCodicaoPagamentoFinanceiro.Properties.ValueMember = "Valor";
                cboCodicaoPagamentoFinanceiro.Properties.DataSource = listaOutros;

                return;
            }
                      
            cboCodicaoPagamentoFinanceiro.Properties.DisplayMember = "Descricao";
            cboCodicaoPagamentoFinanceiro.Properties.ValueMember = "Id";
            cboCodicaoPagamentoFinanceiro.Properties.DataSource = listaCondicoes;
            
        }

        #endregion

        #endregion

        #region " CLASSES AUXILIARES "

        private class ItemGrid
        {
            public int Id { get; set; }

            public int IdProduto { get; set; }

            public string Descricao { get; set; }

            public string CstCsosn { get; set; }

            public string Cfop { get; set; }

            public string Unidade { get; set; }

            public string ValorUnitario { get; set; }

            public string Quantidade { get; set; }

            public string QuantidadeEstocar { get; set; }

            public string Desconto { get; set; }

            public string Frete { get; set; }

            public string ValorTotal { get; set; }
        }

        private class DuplicataGrid
        {
            public int Id { get; set; }

            public string Parcela { get; set; }

            public string NumeroDuplicata { get; set; }

            public string DataVencimento { get; set; }

            public string Valor { get; set; }

            public string FormaPagamento { get; set;}

            public string CondicaoPagamento { get; set; }
        }

        private class NotaReferenciadaGrid
        {
            public int Id { get; set; }

            public string TipoNotaReferenciada { get; set; }

            public string CnpjCpfEmitente { get; set; }

            public string ModeloDocumentoFiscal { get; set; }

            public string ChaveDeAcesso { get; set; }
        }

        #endregion

        private void chkInformarTributosDevolvidos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInformarTributosDevolvidos.Checked)
            {
                txtPercentualMercadoriaTributosDevolvidos.Enabled = true;
                txtValorIpiTributosDevolvidos.Enabled = true;
            }
            else
            {
                txtPercentualMercadoriaTributosDevolvidos.Enabled = false;
                txtValorIpiTributosDevolvidos.Enabled = false;
            }
        }

        private void chkInformarII_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInformarII.Checked)
            {
                txtBaseCalculoImpostoImportacao.Enabled = true;
                txtValorDespesasAduaneirasImpostoImportacao.Enabled = true;
                txtValorImpostoImportacao.Enabled = true;
                txtIOFImpostosImportacao.Enabled = true;
            }
            else
            {
                txtBaseCalculoImpostoImportacao.Enabled = false;
                txtValorDespesasAduaneirasImpostoImportacao.Enabled = false;
                txtValorImpostoImportacao.Enabled = false;
                txtIOFImpostosImportacao.Enabled = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FormPesquisaNotasFiscaisResumido formPesquisaNotasFiscaisResumido = new FormPesquisaNotasFiscaisResumido();
            var notaFiscal = formPesquisaNotasFiscaisResumido.PesquiseNotaFiscal();

            if (notaFiscal != null)
            {
                 LimpeForm();
                 _notaFiscalEmEdicao= notaFiscal;

                EditeNotaFiscal();
            }
            this.Cursor = Cursors.Default;
        }
        
        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormNaturezasOperacoesPesquisa formNaturezasOperacoesPesquisa = new FormNaturezasOperacoesPesquisa();

            var naturezaOperacao = formNaturezasOperacoesPesquisa.ExibaPesquisaDeNaturezaOperacao();

            if (naturezaOperacao != null)
            {
                txtIdNaturezaOperacao.Text = naturezaOperacao.Id.ToString();
                txtNaturezaOperacao.Text = naturezaOperacao.Descricao;
            }
        }
        
        private void txtIdNaturezaOperacao_Leave(object sender, EventArgs e)
        {
            ServicoNaturezaOperacao servicoNaturezaOperacao = new ServicoNaturezaOperacao();

            var natureza = servicoNaturezaOperacao.Consulte(txtIdNaturezaOperacao.Text.ToInt());
            
            if (natureza!=null)
            txtNaturezaOperacao.Text = natureza.Descricao;
        }
                
        private void rdbDescontoItemValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDescontoItemValor.Checked)
            {
                txtValorDescontoItem.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtValorDescontoItem.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
            }

            txtValorDescontoItem.Text = string.Empty;

            CalculeSubTotalItem();

            txtValorDescontoItem.Focus();
        }

        private void cboCfop_EditValueChanged(object sender, EventArgs e)
        {
            //CalculeIcms();
        }
        
        private void txtPercentualMVA_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoMVA)
            {
                //CalculeIcms();
                _editandoMVA = false;
            }
        }

        private void txtPercentualMVA_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoMVA = true;
        }

        private void txtPercentualMVA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtPercentualMVA.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtAliquotaSt_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoAliquotaSt)
            {
                //CalculeIcms();
                _editandoAliquotaSt = false;
            }
        }

        private void txtAliquotaSt_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoAliquotaSt = true;
        }

        private void txtAliquotaSt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaDbSt.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtAliquotaCrSt_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoAliquotaSt)
            {
                //CalculeIcms();
                _editandoAliquotaSt = false;
            }
        }

        private void txtAliquotaCrSt_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoAliquotaSt = true;
        }

        private void txtAliquotaCrSt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaDbSt.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtPercentualIcms_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoPercentualIcms)
            {
                //CalculeIcms();
                _editandoPercentualIcms = false;
            }
        }

        private void txtPercentualIcms_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoPercentualIcms = true;
        }

        private void txtPercentualIcms_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtPercentualIcms.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }
        
        private void txtAliquotaSimplesNacional_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoAliquotaSimplesNacional)
            {
                //CalculeIcms();
                _editandoAliquotaSimplesNacional = false;
            }
        }

        private void txtAliquotaSimplesNacional_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoAliquotaSimplesNacional = true;
        }

        private void txtAliquotaSimplesNacional_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaSimplesNacional.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtPercentualIpi_EditValueChanged(object sender, EventArgs e)
        {
          //  if (_editandoPercentualIpi)
          //{               
          //      CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

          //      txtValorIpi.Text = calculosNotaFiscal.CalculeValorIpi(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
          //                          txtValorDescontoItem.Text.ToDouble(), txtValorFreteItem.Text.ToDouble(), txtValorSeguroItem.Text.ToDouble(),
          //                          txtValorOutrasDespesasItem.Text.ToDouble(), txtPercentualIpi.Text.ToDouble(), ref _BaseDeCalculoIpi).ToString("0.00");

          //      _editandoPercentualIpi = false;
                
          //      CalculeTotais();
          //  }
        }

        private void txtPercentualIpi_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoPercentualIpi = true;
        }

        private void txtPercentualIpi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtPercentualIpi.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtAliquotaPisPercentual_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoPercentualPis)
            {
                CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

                txtValorPis.Text = calculosNotaFiscal.CalculeValorPis(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
                                    txtValorDescontoItem.Text.ToDouble(), txtValorFreteItem.Text.ToDouble(), txtValorSeguroItem.Text.ToDouble(),
                                    txtValorOutrasDespesasItem.Text.ToDouble(), ref _BaseDeCalculoPis, txtAliquotaPisPercentual.Text.ToDouble()).ToString("0.00");

                _editandoPercentualPis = false;
                txtValorBaseCalculoPis.Text = _BaseDeCalculoPis.ToString("0.00");
                txtAliquotaPisReais.Text = string.Empty;
                txtQuantidadeVendidaPis.Text = string.Empty;
                CalculeTotais();
            }
        }

        private void txtAliquotaPisPercentual_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoPercentualPis = true;
        }

        private void txtAliquotaPisPercentual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaPisPercentual.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtAliquotaPisSTPercentual_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoPercentualPis)
            {
                CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

                txtValorPisST.Text = calculosNotaFiscal.CalculeValorPis(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
                                    txtValorDescontoItem.Text.ToDouble(), txtValorFreteItem.Text.ToDouble(), txtValorSeguroItem.Text.ToDouble(),
                                    txtValorOutrasDespesasItem.Text.ToDouble(), ref _BaseDeCalculoPisST, txtAliquotaPisSTPercentual.Text.ToDouble()).ToString("0.00");

                _editandoPercentualPis = false;
                txtBaseCalculoPisST.Text = _BaseDeCalculoPisST.ToString("0.00");
                txtAliquotaPisSTReais.Text = string.Empty;
                txtQuantidadeVendidaPisST.Text = string.Empty;
                CalculeTotais();
            }
        }

        private void txtAliquotaPisSTPercentual_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoPercentualPis = true;
        }

        private void txtAliquotaPisSTPercentual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaPisSTPercentual.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtAliquotaPisReais_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoEmReaisPis)
            {
                CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

                txtValorPis.Text = calculosNotaFiscal.CalculeValorPis(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
                                    txtValorDescontoItem.Text.ToDouble(), txtValorFreteItem.Text.ToDouble(), txtValorSeguroItem.Text.ToDouble(),
                                    txtValorOutrasDespesasItem.Text.ToDouble(), ref _BaseDeCalculoPis,0,txtAliquotaPisReais.Text.ToDouble()).ToString("0.00");

                _editandoEmReaisPis = false;
                txtQuantidadeVendidaPis.Text = txtQuantidadeItem.Text;
                txtValorBaseCalculoPis.Text = string.Empty;
                txtAliquotaPisPercentual.Text = string.Empty;
                CalculeTotais();
            }
        }

        private void txtAliquotaPisReais_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoEmReaisPis = true;
        }

        private void txtAliquotaPisReais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaPisReais.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtAliquotaPisSTReais_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoEmReaisPis)
            {
                CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

                txtValorPisST.Text = calculosNotaFiscal.CalculeValorCofins(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
                                    txtValorDescontoItem.Text.ToDouble(), txtValorFreteItem.Text.ToDouble(), txtValorSeguroItem.Text.ToDouble(),
                                    txtValorOutrasDespesasItem.Text.ToDouble(), ref _BaseDeCalculoPis, 0, txtAliquotaPisSTReais.Text.ToDouble()).ToString("0.00");

                _editandoEmReaisPis = false;
                txtQuantidadeVendidaPisST.Text = txtQuantidadeItem.Text;
                txtAliquotaPisSTPercentual.Text = string.Empty;
                txtBaseCalculoPisST.Text = string.Empty;
                CalculeTotais();
            }
        }

        private void txtAliquotaPisSTReais_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoEmReaisPis = true;
        }

        private void txtAliquotaPisSTReais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaPisSTReais.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtAliquotaCofinsPercentual_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoPercentualCofins)
            {
                CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

                txtValorCofins.Text = calculosNotaFiscal.CalculeValorCofins(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
                                    txtValorDescontoItem.Text.ToDouble(), txtValorFreteItem.Text.ToDouble(), txtValorSeguroItem.Text.ToDouble(),
                                    txtValorOutrasDespesasItem.Text.ToDouble(), ref _BaseDeCalculoCofins, txtAliquotaCofinsPercentual.Text.ToDouble()).ToString("0.00");

                _editandoPercentualCofins = false;
                txtBaseCalculoCofins.Text = _BaseDeCalculoCofins.ToString("0.00");
                txtQuantidadeVendidaCofins.Text = string.Empty;
                txtAliquotaCofinsReais.Text = string.Empty;
                CalculeTotais();
            }
        }

        private void txtAliquotaCofinsPercentual_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoPercentualCofins = true;
        }

        private void txtAliquotaCofinsPercentual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaCofinsPercentual.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtAliquotaCofinsSTPercentual_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoPercentualCofins)
           {
                CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

                txtValorCofinsST.Text = calculosNotaFiscal.CalculeValorCofins(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
                                    txtValorDescontoItem.Text.ToDouble(), txtValorFreteItem.Text.ToDouble(), txtValorSeguroItem.Text.ToDouble(),
                                    txtValorOutrasDespesasItem.Text.ToDouble(), ref _BaseDeCalculoCofinsST, txtAliquotaCofinsSTPercentual.Text.ToDouble()).ToString("0.00");

                _editandoPercentualCofins = false;
                txtBaseCalculoCofinsST.Text = _BaseDeCalculoCofinsST.ToString("0.00");
                txtQuantidadeVendidaCofinsST.Text = string.Empty;
                txtAliquotaCofinsSTReais.Text = string.Empty;
                CalculeTotais();
            }
        }

        private void txtAliquotaCofinsSTPercentual_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoPercentualCofins = true;
        }

        private void txtAliquotaCofinsSTPercentual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaCofinsSTPercentual.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtAliquotaCofinsReais_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoEmReaisCofins)
            {
                CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

                txtValorCofins.Text = calculosNotaFiscal.CalculeValorCofins(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
                                    txtValorDescontoItem.Text.ToDouble(), txtValorFreteItem.Text.ToDouble(), txtValorSeguroItem.Text.ToDouble(),
                                    txtValorOutrasDespesasItem.Text.ToDouble(), ref _BaseDeCalculoCofins, 0, txtAliquotaCofinsReais.Text.ToDouble()).ToString("0.00");

                _editandoEmReaisCofins = false;
                txtQuantidadeVendidaCofins.Text = txtQuantidadeItem.Text;
                txtAliquotaCofinsPercentual.Text = string.Empty;
                txtBaseCalculoCofins.Text = string.Empty;
                CalculeTotais();
            }
        }

        private void txtAliquotaCofinsReais_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoEmReaisCofins = true;
        }

        private void txtAliquotaCofinsReais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaCofinsReais.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtAliquotaCofinsSTReais_EditValueChanged(object sender, EventArgs e)
        {
            if (_editandoEmReaisCofins)
            {
                CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

                txtValorCofinsST.Text = calculosNotaFiscal.CalculeValorCofins(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
                                    txtValorDescontoItem.Text.ToDouble(), txtValorFreteItem.Text.ToDouble(), txtValorSeguroItem.Text.ToDouble(),
                                    txtValorOutrasDespesasItem.Text.ToDouble(), ref _BaseDeCalculoCofins, 0, txtAliquotaCofinsSTReais.Text.ToDouble()).ToString("0.00");

                _editandoEmReaisCofins = false;
                txtQuantidadeVendidaCofinsST.Text = txtQuantidadeItem.Text;
                txtAliquotaCofinsSTPercentual.Text = string.Empty;
                txtBaseCalculoCofinsST.Text = string.Empty;
                CalculeTotais();
            }
        }

        private void txtAliquotaCofinsSTReais_KeyDown(object sender, KeyEventArgs e)
        {
            _editandoEmReaisCofins = true;
        }

        private void txtAliquotaCofinsSTReais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtAliquotaCofinsSTReais.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void rdbRevenda_CheckedChanged(object sender, EventArgs e)
        {
            //CalculeIcms();
        }
        
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            FormCfopPesquisa formCfopPesquisa = new FormCfopPesquisa();

            var cfop = formCfopPesquisa.PesquiseUmCfop();

            if (cfop != null)
            {
                txtCodigoCfop.Text = cfop.Codigo;
                txtDescricaoCFOP.Text = cfop.Descricao;
            }
        }
                
        private void txtCodigoCfop_Leave(object sender, EventArgs e)
        {
            BuscaDescricaoCfop();
        }

        private void txtCodigoCfop_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }
        
        private void rdbTipoOperacaoEntrada_CheckedChanged(object sender, EventArgs e)
        {
            PreenchaCboCstIpi();
        }

        private void rdbTipoOperacaoNotaSaida_CheckedChanged(object sender, EventArgs e)
        {
            PreenchaCboCstIpi();
        }

        private void cboCstIpi_EditValueChanged(object sender, EventArgs e)
        {
            HabiliteOuDesabiliteCstIpi();
        }

        private void cboCstPis_EditValueChanged(object sender, EventArgs e)
        {
            HabiliteOuDesabiliteCstPis();
        }
        private void cboCstCofins_EditValueChanged(object sender, EventArgs e)
        {
            HabiliteOuDesabiliteCstCofins();
        }
        
        private void pbPesquisaNotasImportadas_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            FormRelatorioEntrada formRelatorioEntrada = new FormRelatorioEntrada(true);

            var notaFiscal = formRelatorioEntrada.PesquiseNotaFiscal();

            if (notaFiscal != null)
            {
                LimpeForm();
                _notaFiscalEmEdicao = notaFiscal;

                EditeNotaFiscal();
            }

            this.Cursor = Cursors.Default;
        }

        private void btnCalculeIcms_Click(object sender, EventArgs e)
        {
            if (!ValideCalculoIcms()) return;

            CalculeIcms();
        }

        private void btnLimparCamposIcms_Click(object sender, EventArgs e)
        {
            PreenchaCamposIcms(null);
        }
        
        private void cboRegimeTributario_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCstCsosn();
            CarregarParticularidadesDoRegime();
        }

        private void txtValorIcmsSt_EditValueChanged(object sender, EventArgs e)
        {
            calculeValorTotalDoItem();
        }

        private void chkInformarTransportadora_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInformarTransportadora.Checked)
                cboTransportadoras.Enabled = true;
            else
            {
                cboTransportadoras.Enabled = false;
                cboTransportadoras.EditValue = string.Empty;
            }
        }
        
        private void cboFormaPagamento_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCondicaoPagamentoFinanceiro();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CalculosNotaFiscal calculosNotaFiscal = new CalculosNotaFiscal();

            txtValorIpi.Text = calculosNotaFiscal.CalculeValorIpi(txtValorUnitarioItem.Text.ToDouble(), txtQuantidadeItem.Text.ToDouble(),
                                txtValorDescontoItem.Text.ToDouble(), txtValorFreteItem.Text.ToDouble(), txtValorSeguroItem.Text.ToDouble(),
                                txtValorOutrasDespesasItem.Text.ToDouble(), txtPercentualIpi.Text.ToDouble(), ref _BaseDeCalculoIpi).ToString("0.00");

            _editandoPercentualIpi = false;

            CalculeTotais();
        }

        private void lblDescricaoLocalDespachoComercioExterior_Click(object sender, EventArgs e)
        {

        }

        private void txtDescricaoLocalDespachoComercioExterior_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lblDescricaoLocalEmbarqueComercioExterior_Click(object sender, EventArgs e)
        {

        }

        private void txtDescricaoLocalEmbarqueFronteiraComercioExterior_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboUFComercioExterior_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lblUFComercioExterior_Click(object sender, EventArgs e)
        {

        }

        private void txtIdOutrasSaidas_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
