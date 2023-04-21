using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Report.RelatoriosDevExpress.Cadastros;
using Programax.Easy.Report.RelatoriosDevExpress.Financeiro;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;

using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CategoriaServ;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioContasPagarReceber : FormularioBase
    {
        #region " VARIÁVEIS PRIVADA "

        private EnumTipoOperacaoContasPagarReceber _tipoOperacaoContasPagarReceber;
        private List<CategoriaFinanceira> _listaDeCategoria;
        #endregion

        #region " CONSTRUTOR "

        public FormRelatorioContasPagarReceber(EnumTipoOperacaoContasPagarReceber tipoOperacaoContasPagarReceber)
        {
            InitializeComponent();

            _tipoOperacaoContasPagarReceber = tipoOperacaoContasPagarReceber;

            if (tipoOperacaoContasPagarReceber == EnumTipoOperacaoContasPagarReceber.PAGAR)
            {
                lblTituloRelatorio.Text = "RELATÓRIO DE CONTAS À PAGAR";
            }
            else
            {
                lblTituloRelatorio.Text = "RELATÓRIO DE CONTAS À RECEBER";
            }

            PreenchaCboStatus();
            PreenchaPrimeiroEUltimoDiaMes();
            PreenchaCboDataFiltrar();
            //PreenchaCboGrupos();
            PreenchaCboCategorias();
            this.ActiveControl = rdbOrdemListaCodigo;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DateTime? dataInicial = txtDataInicialPeriodo.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinalPeriodo.Text.ToDateNullabel();
            int? categoriaId = cboCategorias.EditValue.ToInt();

            Pessoa pessoa = !string.IsNullOrWhiteSpace(txtIdParceiro.Text) ? new Pessoa { Id = txtIdParceiro.Text.ToInt() } : null;

            EnumStatusContaPagarReceber? status = (EnumStatusContaPagarReceber?)cboStatusTitulo.EditValue;

            EnumOrdenacaoPesquisaContasPagarReceber ordenacaoPesquisaContasPagarReceber = (EnumOrdenacaoPesquisaContasPagarReceber)pnlOrdenacao.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();
            EnumDataFiltrarContasPagarReceber? dataFiltrar = (EnumDataFiltrarContasPagarReceber?)cboDataFiltrar.EditValue;

            RelatorioContasPagarReceber relatorioContasPagarReceber = new RelatorioContasPagarReceber(pessoa,
                                                                                                                                                    dataFiltrar,
                                                                                                                                                    dataInicial,
                                                                                                                                                    dataFinal,
                                                                                                                                                    status,
                                                                                                                                                    _tipoOperacaoContasPagarReceber,
                                                                                                                                                    ordenacaoPesquisaContasPagarReceber,
                                                                                                                                                    categoriaId);

            TratamentosDeTela.ExibirRelatorio(relatorioContasPagarReceber);
        }

        private void btnPesquisaParceiro_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var indicador = formPessoaPesquisa.PesquisePessoaAtiva();

            if (indicador != null)
            {
                PreenchaParceiro(indicador);
            }
        }

        private void txtIdParceiro_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdParceiro.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var indicador = servicoPessoa.ConsultePessoaAtiva(txtIdParceiro.Text.ToInt());

                PreenchaParceiro(indicador, true);
            }
            else
            {
                PreenchaParceiro(null);
            }
        }

        private void rdbOrdemListaCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDataInicialPeriodo.Focus();
            }
        }

        private void cboDataFiltrar_EditValueChanged(object sender, EventArgs e)
        {
            if ((EnumDataFiltrarContasPagarReceber?)cboDataFiltrar.EditValue == null)
            {
                txtDataInicialPeriodo.Enabled = false;
                txtDataFinalPeriodo.Enabled = false;

                txtDataInicialPeriodo.Text = string.Empty;
                txtDataFinalPeriodo.Text = string.Empty;
            }
            else
            {
                txtDataInicialPeriodo.Enabled = true;
                txtDataFinalPeriodo.Enabled = true;
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaParceiro(Pessoa par, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (par != null)
            {
                txtIdParceiro.Text = par.Id.ToString();
                txtNomeParceiro.Text = par.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Parceiro nao encontrado!", "Parceiro não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdParceiro.Focus();
                }

                txtIdParceiro.Text = string.Empty;
                txtNomeParceiro.Text = string.Empty;
            }
        }

        private void PreenchaCboDataFiltrar()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumDataFiltrarContasPagarReceber>();

            cboDataFiltrar.Properties.ValueMember = "Valor";
            cboDataFiltrar.Properties.DisplayMember = "Descricao";
            cboDataFiltrar.Properties.DataSource = lista;

            cboDataFiltrar.EditValue = EnumDataFiltrarContasPagarReceber.VENCIMENTO;
        }

        private void PreenchaCboStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusContaPagarReceber>();

            lista.Insert(0, null);

            cboStatusTitulo.Properties.ValueMember = "Valor";
            cboStatusTitulo.Properties.DisplayMember = "Descricao";
            cboStatusTitulo.Properties.DataSource = lista;
        }

        private void PreenchaPrimeiroEUltimoDiaMes()
        {
            var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

            txtDataInicialPeriodo.DateTime = primeiroDiaMes;
            txtDataFinalPeriodo.DateTime = ultimoDiaMes;
        }
        //private void PreenchaCboGrupos()
        //{
        //    ServicoGrupo servicoGrupo = new ServicoGrupo();

        //    var grupos = servicoGrupo.ConsulteListaAtivos();

        //    grupos.Insert(0, null);

        //    cboGrupos.Properties.DataSource = grupos;
        //    cboGrupos.Properties.DisplayMember = "Descricao";
        //    cboGrupos.Properties.ValueMember = "Id";

        //    if (cboGrupos.EditValue != null)
        //    {
        //        if (!grupos.Exists(grupo => grupo != null && grupo.Id == cboGrupos.EditValue.ToInt()))
        //        {
        //            cboGrupos.EditValue = null;
        //        }
        //    }
        //}
        //private void Pesquise()
        //{
        //    SubGrupoCategoria grupoCategoria = null;

        //    if (cboGrupos.EditValue != null)
        //    {
        //        grupoCategoria = new SubGrupoCategoria { Id = (int)cboGrupos.EditValue };
        //    }

        //    ServicoCategoria servicoGrupo = new ServicoCategoria();

        //    _listaDeCategoria = servicoGrupo.ConsulteLista("", grupoCategoria, "");

        //    PreenchaCboCategoria();
        //}
        private void PreenchaCboCategorias()
        {
            List<CategoriaFinanceira> categoria = new List<CategoriaFinanceira>();

            //if ((EnumTipoMovimentacaoCaixa)cboTiposMovimentacoes.EditValue == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
            //{
            //    categoria = new ServicoCategoria().ConsulteLista(string.Empty, null, "A", EnumTipoCategoria.RECEITA);
            //}
            //else
            //{
                categoria = new ServicoCategoria().ConsulteLista(string.Empty, null, "A");
            //}

            categoria.Insert(0, null);

            cboCategorias.Properties.DisplayMember = "Descricao";
            cboCategorias.Properties.ValueMember = "Id";
            cboCategorias.Properties.DataSource = categoria;

            if (cboCategorias.EditValue != null)
            {
                if (!categoria.Exists(categ => categ != null && categ.Id == cboCategorias.EditValue.ToInt()))
                {
                    cboCategorias.EditValue = null;
                }
            }
        }
        //private void PreenchaCboCategoria()
        //{
            
        //    var linhas = _listaDeCategoria;

        //    linhas.Insert(0, null);

        //    cboCategorias.Properties.DataSource = linhas;
        //    cboCategorias.Properties.DisplayMember = "Descricao";
        //    cboCategorias.Properties.ValueMember = "Id";
        //}
        #endregion

        //private void cboGrupos_EditValueChanged(object sender, EventArgs e)
        //{
        //    Pesquise();
        //}
    }
}
