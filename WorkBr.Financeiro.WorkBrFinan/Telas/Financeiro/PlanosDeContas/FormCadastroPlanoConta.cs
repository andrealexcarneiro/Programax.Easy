using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.PlanoContasServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.View.Telas.Financeiro.PlanosDeContas
{
    public partial class FormCadastroPlanoConta : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<PlanoDeContas> _listaDePlanosDeContas;
        private int _idPlanoContas;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroPlanoConta()
        {
            InitializeComponent();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            PreenchaOStatus();
            PreenchaCboTipo();
            PreenchaCboNatureza();

            AtualizarGridDePlanoDeContas();

            this.ActiveControl = txtNumeroPlanoContas;

            this.NomeDaTela = "Plano de Contas";
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
            Salve();
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPlanosContasPesquisa formPlanoDeContassPesquisa = new FormPlanosContasPesquisa();

            var planoDeContas = formPlanoDeContassPesquisa.ExibaPesquisaDePlanoDeContas();

            if (planoDeContas != null)
            {
                EditePlanoDeContas(planoDeContas);
            }
        }

        private void gcPlanoDeContas_DoubleClick(object sender, EventArgs e)
        {
            SelecionePlanoDeContas();
        }

        private void gcPlanoDeContas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecionePlanoDeContas();
            }
        }

        private void txtNumeroPlanoContas_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNumeroPlanoContas.Text))
            {
                ServicoPlanoDeContas servicoPlanoContas = new ServicoPlanoDeContas();

                var planoContas = servicoPlanoContas.ConsultePlanoDeContasPeloNumero(txtNumeroPlanoContas.Text);

                EditePlanoDeContas(planoContas);
            }
            else
            {
                EditePlanoDeContas(null);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void LimpeFormulario()
        {
            AtualizarGridDePlanoDeContas();
            EditePlanoDeContas(null, limparNumeroPlanoContas: true, focoNumeroPlanoContas: true);
        }

        private void EditePlanoDeContas(PlanoDeContas planoDeContas, bool limparNumeroPlanoContas = false, bool focoNumeroPlanoContas = false)
        {
            if (planoDeContas != null)
            {
                _idPlanoContas = planoDeContas.Id;
                txtDescricao.Text = planoDeContas.Descricao;
                txtDataCadastro.Text = planoDeContas.DataCadastro.ToString("dd/MM/yyyy");
                txtNumeroPlanoContas.Text = planoDeContas.NumeroPlanoDeContas;
                cboNaturezaPlanoDeContas.EditValue = planoDeContas.NaturezaPlanoContas;
                cboTipoPlanoDeContas.EditValue = planoDeContas.TipoPlanoContas;

                cboStatus.EditValue = planoDeContas.Status;

                txtNumeroPlanoContasContador.Text = planoDeContas.NumeroPlanoContasContador;

                if (planoDeContas.PlanoDeContasPadrao)
                {
                    txtDescricao.Enabled = false;
                    txtNumeroPlanoContas.Enabled = false;
                    txtDataCadastro.Enabled = false;

                    cboStatus.Enabled = false;
                    cboNaturezaPlanoDeContas.Enabled = false;
                    cboTipoPlanoDeContas.Enabled = false;
                }

                txtDescricao.Focus();
            }
            else
            {
                _idPlanoContas = 0;
                txtDescricao.Text = string.Empty;

                if (limparNumeroPlanoContas)
                {
                    txtNumeroPlanoContas.Text = string.Empty;
                }

                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                cboStatus.EditValue = "A";
                cboNaturezaPlanoDeContas.EditValue = null;
                cboTipoPlanoDeContas.EditValue = null;

                txtDescricao.Enabled = true;
                txtNumeroPlanoContas.Enabled = true;
                txtDataCadastro.Enabled = true;

                cboStatus.Enabled = true;
                cboNaturezaPlanoDeContas.Enabled = true;
                cboTipoPlanoDeContas.Enabled = true;

                txtNumeroPlanoContasContador.Text = string.Empty;

                if (focoNumeroPlanoContas)
                {
                    txtNumeroPlanoContas.Focus();
                }
            }
        }

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

        private void PreenchaCboNatureza()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumNaturezaPlanoContas>();

            lista.Insert(0, null);

            cboNaturezaPlanoDeContas.Properties.DataSource = lista;
            cboNaturezaPlanoDeContas.Properties.ValueMember = "Valor";
            cboNaturezaPlanoDeContas.Properties.DisplayMember = "Descricao";
        }

        private void PreenchaCboTipo()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPlanoContas>();

            lista.Insert(0, null);

            cboTipoPlanoDeContas.Properties.DataSource = lista;
            cboTipoPlanoDeContas.Properties.ValueMember = "Valor";
            cboTipoPlanoDeContas.Properties.DisplayMember = "Descricao";
        }

        private void AtualizarGridDePlanoDeContas()
        {
            ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();
            _listaDePlanosDeContas = servicoPlanoDeContas.ConsulteLista();

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<PlanoDeContasAuxiliar> listaDePlanosDeContasAuxiliares = new List<PlanoDeContasAuxiliar>();

            foreach (var planoDeContas in _listaDePlanosDeContas)
            {
                PlanoDeContasAuxiliar planoDeContasAuxiliar = new PlanoDeContasAuxiliar();

                planoDeContasAuxiliar.Descricao = planoDeContas.Descricao;
                planoDeContasAuxiliar.Id = planoDeContas.Id;
                planoDeContasAuxiliar.Natureza = planoDeContas.NaturezaPlanoContas != null ? planoDeContas.NaturezaPlanoContas.GetValueOrDefault().Descricao() : string.Empty;
                planoDeContasAuxiliar.NumeroPlanoDeContas = planoDeContas.NumeroPlanoDeContas;
                planoDeContasAuxiliar.Tipo = planoDeContas.TipoPlanoContas != null ? planoDeContas.TipoPlanoContas.GetValueOrDefault().Descricao() : string.Empty;
                planoDeContasAuxiliar.Status = planoDeContas.Status == "A" ? "ATIVO" : "INATIVO";
                planoDeContasAuxiliar.NumeroPlanoDeContasContador = planoDeContas.NumeroPlanoContasContador;

                listaDePlanosDeContasAuxiliares.Add(planoDeContasAuxiliar);
            }

            gcPlanoDeContas.DataSource = listaDePlanosDeContasAuxiliares;
            gcPlanoDeContas.RefreshDataSource();
        }

        private void SelecionePlanoDeContas()
        {
            if (_listaDePlanosDeContas != null && _listaDePlanosDeContas.Count > 0)
            {
                ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();

                var planoDeContas = servicoPlanoDeContas.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                EditePlanoDeContas(planoDeContas);
            }
        }

        private void PesquisePeloId()
        {
            ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();
            var planoDeContas = servicoPlanoDeContas.Consulte(_idPlanoContas);

            EditePlanoDeContas(planoDeContas);
        }

        private void Salve()
        {
            Action actionSalvar = () =>
            {
                var planoDeContas = RetornePlanoDeContasEmEdicao();

                ServicoPlanoDeContas servicoPlanoDeContas = new ServicoPlanoDeContas();

                if (planoDeContas.Id == 0)
                {
                    servicoPlanoDeContas.Cadastre(planoDeContas);
                }
                else
                {
                    servicoPlanoDeContas.Atualize(planoDeContas);
                }

                _idPlanoContas = planoDeContas.Id;

                AtualizarGridDePlanoDeContas();
                PesquisePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private PlanoDeContas RetornePlanoDeContasEmEdicao()
        {
            PlanoDeContas planoDeContas = new PlanoDeContas();

            planoDeContas.Id = _idPlanoContas;
            planoDeContas.Descricao = txtDescricao.Text;
            planoDeContas.DataCadastro = txtDataCadastro.Text.ToDate();
            planoDeContas.NaturezaPlanoContas = (EnumNaturezaPlanoContas?)cboNaturezaPlanoDeContas.EditValue;
            planoDeContas.NumeroPlanoDeContas = txtNumeroPlanoContas.Text;
            planoDeContas.TipoPlanoContas = (EnumTipoPlanoContas?)cboTipoPlanoDeContas.EditValue;
            planoDeContas.Status = cboStatus.EditValue.ToString();
            planoDeContas.NumeroPlanoContasContador = txtNumeroPlanoContasContador.Text;

            return planoDeContas;
        }

        #endregion

        private void gcPlanoDeContas_Click(object sender, EventArgs e)
        {

        }
    }
}
