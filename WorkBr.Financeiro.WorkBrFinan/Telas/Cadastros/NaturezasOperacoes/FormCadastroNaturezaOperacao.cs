using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Financeiro.PlanoContasServ;
using Programax.Easy.View.Telas.Financeiro.PlanosDeContas;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.Fiscal.CfopServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using System.Linq;

namespace Programax.Easy.View.Telas.Cadastros.NaturezasOperacoes
{
    public partial class FormCadastroNaturezaOperacao : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private PlanoDeContas _planoDeContas;

        private List<Cfop> _listaCfopsComboBox;
        private List<NaturezaOperacaoCfop> _listaCfopsNaturezaOperacao;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroNaturezaOperacao()
        {
            InitializeComponent();

            this.ActiveControl = txtId;

            this.NomeDaTela = "Cadastro de Natureza Operação";

            _listaCfopsNaturezaOperacao = new List<NaturezaOperacaoCfop>();
            _listaCfopsComboBox = new List<Cfop>();

            PreenchaCboTipoMovimentacao();
            PreenchaCboOrigemDestino();

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                var naturezaOperacao = RetorneNaturezaOperacaoEmEdicao();

                ServicoNaturezaOperacao servicoNaturezaOperacao = new ServicoNaturezaOperacao();

                if (naturezaOperacao.Id == 0)
                {
                    servicoNaturezaOperacao.Cadastre(naturezaOperacao);
                }
                else
                {
                    servicoNaturezaOperacao.Atualize(naturezaOperacao);
                }

                txtId.Text = naturezaOperacao.Id.ToString();
                PesquisePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormNaturezasOperacoesPesquisa formNaturezasOperacoesPesquisa = new FormNaturezasOperacoesPesquisa();

            var naturezaOperacao = formNaturezasOperacoesPesquisa.ExibaPesquisaDeNaturezaOperacao();

            if (naturezaOperacao != null)
            {
                PreenchaCamposNaturezaOperacao(naturezaOperacao);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePeloId();
            }
        }

        private void FormCadastroNaturezaOperacao_Load(object sender, EventArgs e)
        {
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            PreenchaOStatus();
        }

        private void txtNumeroPlanoDeContas_Leave(object sender, EventArgs e)
        {
            ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();

            var planoDeContas = servicoPlanoDeContas.ConsultePlanoDeContasAtivoPeloNumero(txtNumeroPlanoDeContas.Text);

            PreenchaPlanoDeContas(planoDeContas);
        }

        private void btnPesquisaPlanoDeContas_Click(object sender, EventArgs e)
        {
            FormPlanosContasPesquisa formPlanosContasPesquisa = new FormPlanosContasPesquisa();

            var planoDeContas = formPlanosContasPesquisa.ExibaPesquisaDePlanoDeContasAtivos();

            if (planoDeContas != null)
            {
                PreenchaPlanoDeContas(planoDeContas);
            }
        }

        private void cboTipoMovimentacao_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCfop();
        }

        private void cboOrigemDestino_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCfop();
        }

        private void btnInserirCfop_Click(object sender, EventArgs e)
        {
            AdicioneCfop();
        }

        private void btnExcluirCfop_Click(object sender, EventArgs e)
        {
            var naturezaCfop = _listaCfopsNaturezaOperacao.FirstOrDefault(cfopNatureza => cfopNatureza != null &&
                                                                                                                    cfopNatureza.Cfop.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

            if (naturezaCfop != null)
            {
                string mensagemCfop = "Deseja excluir o Cfop " + naturezaCfop.Cfop.Codigo + " - " + naturezaCfop.Cfop.Descricao + " ?";

                if (MessageBox.Show(mensagemCfop, "Excluir cfop", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    _listaCfopsNaturezaOperacao.Remove(naturezaCfop);

                    PreenchaGrid();
                }
            }
        }

        private void cboCfop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AdicioneCfop();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = "A";
        }

        private void PreenchaCboTipoMovimentacao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoMovimentacaoNaturezaOperacao>();

            cboTipoMovimentacao.Properties.DataSource = lista;
            cboTipoMovimentacao.Properties.DisplayMember = "Descricao";
            cboTipoMovimentacao.Properties.ValueMember = "Valor";

            cboTipoMovimentacao.EditValue = EnumTipoMovimentacaoNaturezaOperacao.ENTRADA;
        }

        private void PreenchaCboOrigemDestino()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumOrigemDestino>();

            cboOrigemDestino.Properties.DataSource = lista;
            cboOrigemDestino.Properties.DisplayMember = "Descricao";
            cboOrigemDestino.Properties.ValueMember = "Valor";

            cboOrigemDestino.EditValue = EnumOrigemDestino.ESTADUAL;
        }

        private void PreenchaCboCfop()
        {
            if (cboTipoMovimentacao.EditValue == null || cboOrigemDestino.EditValue == null)
            {
                return;
            }

            ServicoCfop servicoCfop = new ServicoCfop();
            _listaCfopsComboBox = servicoCfop.ConsulteListaAtiva((EnumOrigemDestino)cboOrigemDestino.EditValue, (EnumTipoMovimentacaoNaturezaOperacao)cboTipoMovimentacao.EditValue);

            List<ObjetoDescricaoValor> listaObjetoDescricaoValor = new List<ObjetoDescricaoValor>();

            listaObjetoDescricaoValor.Add(null);

            _listaCfopsComboBox.ForEach(cfop =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor();

                objetoDescricaoValor.Valor = cfop.Id;
                objetoDescricaoValor.Descricao = cfop.Codigo + " - " + cfop.Descricao;

                listaObjetoDescricaoValor.Add(objetoDescricaoValor);
            });

            cboCfop.Properties.DataSource = listaObjetoDescricaoValor;
            cboCfop.Properties.DisplayMember = "Descricao";
            cboCfop.Properties.ValueMember = "Valor";
        }

        private void LimpeFormulario()
        {
            PreenchaCamposNaturezaOperacao(null);
        }

        private void PreenchaCamposNaturezaOperacao(NaturezaOperacao naturezaOperacao, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (naturezaOperacao != null)
            {
                naturezaOperacao.ListaCfops.CarregueLazyLoad();
                naturezaOperacao.PlanoDeContas.CarregueLazyLoad();

                txtId.Text = naturezaOperacao.Id.ToString();
                txtDescricao.Text = naturezaOperacao.Descricao;
                txtDataCadastro.Text = naturezaOperacao.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = naturezaOperacao.Status;

                cboTipoMovimentacao.EditValue = naturezaOperacao.TipoMovimentacao;
                cboOrigemDestino.EditValue = naturezaOperacao.OrigemDestino;

                PreenchaPlanoDeContas(naturezaOperacao.PlanoDeContas);

                chkGeraTitulosFinanceiro.Checked = naturezaOperacao.GeraTitulosFinanceiro;
                chkRealizaMovimentacaoEstoque.Checked = naturezaOperacao.RealizaMovimentacaoEstoque;
                chkObrigatorioExistirPedidoVenda.Checked = naturezaOperacao.ObrigatorioExistirPedidoVenda;

                _listaCfopsNaturezaOperacao = naturezaOperacao.ListaCfops.ToList();

                PreenchaGrid();

                txtId.Enabled = false;

                txtDescricao.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                cboStatus.EditValue = "A";

                cboTipoMovimentacao.EditValue = EnumTipoMovimentacaoNaturezaOperacao.ENTRADA;
                cboOrigemDestino.EditValue = EnumOrigemDestino.ESTADUAL;

                PreenchaPlanoDeContas(null);

                chkGeraTitulosFinanceiro.Checked = false;
                chkRealizaMovimentacaoEstoque.Checked = false;
                chkObrigatorioExistirPedidoVenda.Checked = false;

                _listaCfopsNaturezaOperacao.Clear();

                cboCfop.EditValue = null;

                PreenchaGrid();

                txtId.Enabled = true;
                txtId.Focus();

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Natureza Operacao não encontrada", "Natureza Operacao não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private NaturezaOperacao RetorneNaturezaOperacaoEmEdicao()
        {
            NaturezaOperacao naturezaOperacao = new NaturezaOperacao();

            naturezaOperacao.Id = txtId.Text.ToInt();
            naturezaOperacao.Descricao = txtDescricao.Text;
            naturezaOperacao.DataCadastro = txtDataCadastro.Text.ToDate();
            naturezaOperacao.Status = cboStatus.EditValue.ToString();

            naturezaOperacao.TipoMovimentacao = (EnumTipoMovimentacaoNaturezaOperacao)cboTipoMovimentacao.EditValue;
            naturezaOperacao.OrigemDestino = (EnumOrigemDestino)cboOrigemDestino.EditValue;
            naturezaOperacao.PlanoDeContas = _planoDeContas;

            naturezaOperacao.GeraTitulosFinanceiro = chkGeraTitulosFinanceiro.Checked;
            naturezaOperacao.RealizaMovimentacaoEstoque = chkRealizaMovimentacaoEstoque.Checked;
            naturezaOperacao.ObrigatorioExistirPedidoVenda = chkObrigatorioExistirPedidoVenda.Checked;

            naturezaOperacao.ListaCfops = _listaCfopsNaturezaOperacao;

            return naturezaOperacao;
        }

        private void PreenchaPlanoDeContas(PlanoDeContas planoDeContas, bool exibirMensagemDeNaoEncontrado = false)
        {
            _planoDeContas = planoDeContas;

            if (planoDeContas != null)
            {
                txtNumeroPlanoDeContas.Text = planoDeContas.NumeroPlanoDeContas;
                txtDescricaoPlanoContas.Text = planoDeContas.Descricao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Plano de Contas nao encontrado!", "Plano de Contas não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNumeroPlanoDeContas.Focus();
                }

                txtNumeroPlanoDeContas.Text = string.Empty;
                txtDescricaoPlanoContas.Text = string.Empty;
            }
        }

        private void AdicioneCfop()
        {
            if (cboCfop.EditValue == null)
            {
                MessageBox.Show("Selecione um Cfop.");

                return;
            }

            var cfop = _listaCfopsComboBox.FirstOrDefault(cfopComboBox => cfopComboBox.Id == cboCfop.EditValue.ToInt());

            if (cfop == null)
            {
                MessageBox.Show("Cfop não encontrado.");

                return;
            }

            if (_listaCfopsNaturezaOperacao.Exists(cfopNatureza => cfopNatureza.Cfop.Id == cfop.Id))
            {
                MessageBox.Show("Este Cfop já está na grid.");

                return;
            }

            if (cfop != null)
            {
                NaturezaOperacaoCfop naturezaOperacaoCfop = new NaturezaOperacaoCfop();
                naturezaOperacaoCfop.Cfop = cfop;

                _listaCfopsNaturezaOperacao.Add(naturezaOperacaoCfop);
            }

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<CfopGrid> listaCfopGrid = new List<CfopGrid>();

            foreach (var naturezaCfop in _listaCfopsNaturezaOperacao)
            {
                var cfop = naturezaCfop.Cfop;

                CfopGrid cfopGrid = new CfopGrid();

                cfopGrid.Id = cfop.Id;
                cfopGrid.Cfop = cfop.Codigo;
                cfopGrid.Descricao = cfop.Descricao;
                cfopGrid.Status = cfop.Status == "A" ? "ATIVO" : "INATIVO";

                listaCfopGrid.Add(cfopGrid);
            }

            gcCfop.DataSource = listaCfopGrid;
            gcCfop.RefreshDataSource();
        }

        private void PesquisePeloId()
        {
            ServicoNaturezaOperacao servicoNaturezaOperacao = new ServicoNaturezaOperacao();
            var naturezaOperacao = servicoNaturezaOperacao.Consulte(txtId.Text.ToInt());

            PreenchaCamposNaturezaOperacao(naturezaOperacao, exibirMensagemDeNaoEncontrado: true);
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class CfopGrid
        {
            public int Id { get; set; }

            public string Cfop { get; set; }

            public string Descricao { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
