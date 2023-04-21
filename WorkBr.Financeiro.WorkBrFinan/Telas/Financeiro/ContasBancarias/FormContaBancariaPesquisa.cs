using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ContaBancariaServ;
using Programax.Easy.Servico.Financeiro.BancoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Financeiro.AgenciaServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Financeiro.ContasBancarias
{
    public partial class FormContaBancariaPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<ContaBancaria> _listaDeContasBancarias;
        private ContaBancaria _contaBancariaSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormContaBancariaPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboBanco();

            this.ActiveControl = cboBancos;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcNcms_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcNcms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboBancos_EditValueChanged(object sender, EventArgs e)
        {
            if (cboBancos.EditValue != null)
            {
                cboAgencias.Enabled = true;

                ServicoAgencia servicoAgencia = new ServicoAgencia();

                var listaAgencias = servicoAgencia.ConsulteLista(new Banco { Id = cboBancos.EditValue.ToInt() }, string.Empty, "A");

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
            else
            {
                cboAgencias.EditValue = null;
                cboAgencias.Enabled = false;
            }
        }

        private void txtIdPessoa_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdPessoa.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var vendedor = servicoPessoa.ConsultePessoaAtiva(txtIdPessoa.Text.ToInt());

                PreenchaPessoa(vendedor, true);
            }
            else
            {
                PreenchaPessoa(null);
            }

            Pesquise();
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var pessoa = formPessoaPesquisa.PesquisePessoa();

            if (pessoa != null)
            {
                PreenchaPessoa(pessoa);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public ContaBancaria ExibaPesquisaDeContasBancarias()
        {
            this.ShowDialog();

            return _contaBancariaSelecionada;
        }

        private void Pesquise()
        {
            ServicoContaBancaria servicoContaBancaria = new ServicoContaBancaria();

            Banco banco = cboBancos.EditValue != null ? new Banco { Id = cboBancos.EditValue.ToInt() } : null;
            Agencia agencia = cboAgencias.EditValue != null ? new Agencia { Id = cboAgencias.EditValue.ToInt() } : null;

            string numeroConta = txtNumeroConta.Text;
            string status = cboStatus.EditValue.ToStringEmpty();

            Pessoa pessoaTitular = !string.IsNullOrEmpty(txtIdPessoa.Text) ? new Pessoa { Id = txtIdPessoa.Text.ToInt() } : null;

            _listaDeContasBancarias = servicoContaBancaria.ConsulteLista(banco, agencia, numeroConta, status, pessoaTitular);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<ContaBancariaAuxiliar> listaDeContasBancariasAuxiliares = new List<ContaBancariaAuxiliar>();

            foreach (var contaBancaria in _listaDeContasBancarias)
            {
                ContaBancariaAuxiliar contaBancariaAuxiliar = new ContaBancariaAuxiliar();

                contaBancariaAuxiliar.Id = contaBancaria.Id;

                contaBancariaAuxiliar.Banco = contaBancaria.Agencia.Banco.Descricao;
                contaBancariaAuxiliar.Agencia = contaBancaria.Agencia.NomeAgencia;

                contaBancariaAuxiliar.NumeroContaBancaria = contaBancaria.NumeroConta;
                contaBancariaAuxiliar.Pessoa = contaBancariaAuxiliar.Pessoa != null? contaBancaria.Pessoa.Id + " - " + contaBancaria.Pessoa.DadosGerais.Razao:null;

                contaBancariaAuxiliar.Status = contaBancaria.Status == "A" ? "ATIVO" : "INATIVO";

                contaBancariaAuxiliar.TipoContaBancaria = contaBancaria.TipoContaBancaria.GetValueOrDefault().Descricao();

                listaDeContasBancariasAuxiliares.Add(contaBancariaAuxiliar);
            }

            gcContasBancarias.DataSource = listaDeContasBancariasAuxiliares;
            gcContasBancarias.RefreshDataSource();
        }

        private void Selecione()
        {
            _contaBancariaSelecionada = null;

            if (_listaDeContasBancarias != null && _listaDeContasBancarias.Count > 0)
            {
                ServicoContaBancaria servicoContaBancaria = new ServicoContaBancaria();

                _contaBancariaSelecionada = servicoContaBancaria.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivoOuInativo = new ObjetoParaComboBox();
            objetoComboBoxAtivoOuInativo.Valor = string.Empty;
            objetoComboBoxAtivoOuInativo.Descricao = "Ativo ou Inativo";

            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivoOuInativo);
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = string.Empty;
        }

        private void PreenchaCboBanco()
        {
            ServicoBanco servicoBanco = new ServicoBanco();

            var listaBancos = servicoBanco.ConsulteLista();

            listaBancos.Insert(0, null);

            cboBancos.Properties.DataSource = listaBancos;
            cboBancos.Properties.DisplayMember = "Descricao";
            cboBancos.Properties.ValueMember = "Id";
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

        #endregion

        #region " CLASSES AUXILIARES "

        private class ContaBancariaAuxiliar
        {
            public int Id { get; set; }

            public string TipoContaBancaria { get; set; }

            public string Status { get; set; }

            public string Banco { get; set; }

            public string Agencia { get; set; }

            public string NumeroContaBancaria { get; set; }

            public string Pessoa { get; set; }
        }

        private class AgenciaAuxiliarComboBox
        {
            public int Id { get; set; }

            public string Descricao { get; set; }
        }

        #endregion
    }
}
