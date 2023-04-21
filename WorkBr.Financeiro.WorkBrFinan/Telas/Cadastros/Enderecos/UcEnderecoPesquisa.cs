using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.EnderecoServ;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.Cadastros.Enderecos
{
    public partial class UcEnderecoPesquisa : UserControl
    {
        #region " VARIÁVEIS PRIVADAS "

        private Action<Endereco> _metodoAposASelecaoDoRegistro;
        private Endereco _enderecoSelecionado;
        private List<Endereco> _listaDeEnderecos;

        private ServicoCidade _servicoCidade;

        #endregion

        #region " CONSTRUTOR "

        public UcEnderecoPesquisa()
        {
            InitializeComponent();

            PreenchaOsEstados();
            PreenchaOStatus();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void UcEnderecoPesquisa_Load(object sender, EventArgs e)
        {
        }

        private void pbPesquisaCadastro_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void txtCep_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !txtCep.Text.EstahVazio())
            {
                Pesquise();
            }
        }

        private void gcEnderecos_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcEnderecos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void cboEstado_EditValueChanged(object sender, EventArgs e)
        {
            if (_servicoCidade == null)
            {
                _servicoCidade = new ServicoCidade();
            }

            string uf = cboEstado.EditValue == null ? string.Empty : cboEstado.EditValue.ToString();

            var listaDeCidades = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(uf);

            listaDeCidades.Insert(0, null);

            cboCidade.Properties.DataSource = listaDeCidades;
            cboCidade.Properties.DisplayMember = "Descricao";
            cboCidade.Properties.ValueMember = "Id";

            cboCidade.EditValue = null;
        }

        private void txtRua_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void Pesquise()
        {
            ServicoEndereco servicoEndereco = new ServicoEndereco();

            Estado estado = null;
            Cidade cidade = null;

            string cep = txtCep.Text.EstahVazio() ? string.Empty : txtCep.Text;
            string bairro = txtBairro.Text;
            string rua = txtRua.Text;

            if (cboEstado.EditValue != null)
            {
                estado = new Estado { UF = cboEstado.EditValue.ToString() };
            }

            if (cboCidade.EditValue != null)
            {
                cidade = new Cidade { Id = Convert.ToInt32(cboCidade.EditValue) };
            }

            var status = cboStatus.EditValue.ToStringEmpty();

            if (estado != null || cidade != null || !string.IsNullOrEmpty(cep) || !string.IsNullOrEmpty(bairro) || !string.IsNullOrEmpty(rua))
            {
                _listaDeEnderecos = servicoEndereco.ConsulteLista(cep, estado, cidade, bairro, rua, status);

                PreencherGrid(_listaDeEnderecos);
            }
            else
            {
                MessageBox.Show("É necessário informar algum filtro para a pesquisa.", "Informe algum filtro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PreencherGrid(List<Endereco> listaDeEnderecos)
        {
            List<EnderecoAux> listaDeEnderecosAuxiliares = new List<EnderecoAux>();

            foreach (var endereco in listaDeEnderecos)
            {
                var enderecoAux = new EnderecoAux();

                enderecoAux.Id = endereco.Id;

                enderecoAux.Bairro = endereco.Bairro;
                enderecoAux.CEP = endereco.CEP;
                enderecoAux.Cidade = endereco.Cidade.Descricao;
                enderecoAux.Rua = endereco.Rua;
                enderecoAux.UF = endereco.Cidade.Estado.UF;
                enderecoAux.Status = endereco.Status == "A" ? "ATIVO" : "INATIVO";

                listaDeEnderecosAuxiliares.Add(enderecoAux);
            }

            gcEnderecos.DataSource = listaDeEnderecosAuxiliares;
            gcEnderecos.RefreshDataSource();
        }

        public void InformarMetodoDeRetornoDoRegistro(Action<Endereco> metodoAposASelecaoDoRegistro)
        {
            _metodoAposASelecaoDoRegistro = metodoAposASelecaoDoRegistro;
        }

        public void Selecione()
        {
            _enderecoSelecionado = null;

            if (_listaDeEnderecos != null && _listaDeEnderecos.Count > 0)
            {
                ServicoEndereco servicoEndereco = new ServicoEndereco();

                _enderecoSelecionado = servicoEndereco.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            _metodoAposASelecaoDoRegistro(_enderecoSelecionado);
        }

        private void PreenchaOsEstados()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var listaDeEstados = servicoEstado.ConsulteListaEstados();

            listaDeEstados.Insert(0, null);

            cboEstado.Properties.DataSource = listaDeEstados;
            cboEstado.Properties.DisplayMember = "Nome";
            cboEstado.Properties.ValueMember = "UF";
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

        #endregion

        #region " CLASSES AUXILIARES "

        private class EnderecoAux
        {
            public int Id { get; set; }

            public string UF { get; set; }

            public string Cidade { get; set; }

            public string Bairro { get; set; }

            public string Rua { get; set; }

            public string CEP { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}
