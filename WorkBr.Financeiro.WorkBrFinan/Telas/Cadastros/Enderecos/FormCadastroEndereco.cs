using System;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.EnderecoServ;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Cadastros.Cidades;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Cadastros.Enderecos
{
    public partial class FormCadastroEndereco : FormularioPadrao
    {
        #region " VARIÁVEIS PRIAVADAS "

        private ServicoCidade _servicoCidade;
        private int _idCidade;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroEndereco()
        {
            InitializeComponent();

            this.NomeDaTela = "Cadastro de Endereço";

            _servicoCidade = new ServicoCidade();

            PreenchaOStatus();
            PreenchaOsEstados();
            TrateUsuarioNaoTemPermissaoCadastroBanco();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.ActiveControl = txtCep;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void cboEstado_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCidades();

            cboCidade.EditValue = null;
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Salve();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormEnderecoPesquisa formEnderecoPesquisa = new FormEnderecoPesquisa();
            var endereco = formEnderecoPesquisa.PesquiseEndereco();

            if (endereco != null)
            {
                EditeEndereco(endereco);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnAtalhoCidade_Click(object sender, EventArgs e)
        {
            FormCadastroCidade formCadastroCidade = new FormCadastroCidade();
            formCadastroCidade.ShowDialog();

            PreenchaCboCidades();
        }

        private void txtCep_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCep.Text))
            {
                PesquiseEnderecoPeloCep();
            }
            else
            {
                EditeEndereco(null);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public void EditeEndereco(Endereco endereco, bool limparCep = false, bool focoNoCep = false)
        {
            if (endereco != null)
            {
                _idCidade = endereco.Id;
                txtBairro.Text = endereco.Bairro;
                txtRua.Text = endereco.Rua;
                txtCep.Text = endereco.CEP;

                if (endereco.Cidade != null)
                {
                    if (endereco.Cidade.Estado != null)
                    {
                        cboEstado.EditValue = endereco.Cidade.Estado.UF;
                    }

                    cboCidade.EditValue = endereco.Cidade.Id;
                }

                cboStatus.EditValue = endereco.Status;
                txtDataCadastro.Text = endereco.DataCadastro.ToString("dd/MM/yyyy");

                cboEstado.Focus();

                this.Show();
            }
            else
            {
                _idCidade = 0;

                if (limparCep)
                {
                    txtCep.Text = string.Empty;
                }
                cboEstado.EditValue = null;
                cboCidade.EditValue = null;
                txtBairro.Text = string.Empty;
                txtRua.Text = string.Empty;

                cboStatus.EditValue = "A";
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                if (focoNoCep)
                {
                    txtCep.Focus();
                }
                else
                {
                    cboEstado.Focus();
                }
            }
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

        private void Salve()
        {
            Action actionSalvar = () =>
            {
                var endereco = RetorneEnderecoEmEdicao();

                ServicoEndereco servicoEndereco = new ServicoEndereco();

                if (endereco.Id == 0)
                {
                    servicoEndereco.Cadastre(endereco);
                }
                else
                {
                    servicoEndereco.Atualize(endereco);
                }

                PesquiseEnderecoPeloCep();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private Endereco RetorneEnderecoEmEdicao()
        {
            Endereco endereco = new Endereco();

            endereco.Id = _idCidade;

            endereco.Bairro = txtBairro.Text;
            endereco.CEP = txtCep.Text;
            endereco.Rua = txtRua.Text;

            if (cboCidade.EditValue != null)
            {
                endereco.Cidade = new Cidade { Id = Convert.ToInt32(cboCidade.EditValue) };
            }

            endereco.Status = cboStatus.EditValue.ToString();
            endereco.DataCadastro = txtDataCadastro.Text.ToDate();

            return endereco;
        }

        private void LimpeFormulario()
        {
            EditeEndereco(null, limparCep: true, focoNoCep: true);
        }

        private void PreenchaCboCidades()
        {
            string uf = cboEstado.EditValue == null ? string.Empty : cboEstado.EditValue.ToString();

            var listaDeCidades = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(uf);

            listaDeCidades.Insert(0, null);

            cboCidade.Properties.DataSource = listaDeCidades;
            cboCidade.Properties.DisplayMember = "Descricao";
            cboCidade.Properties.ValueMember = "Id";
        }

        private void PesquiseEnderecoPeloCep()
        {
            ServicoEndereco servicoEndereco = new ServicoEndereco();
            var endereco = servicoEndereco.Consulte(txtCep.Text);

            EditeEndereco(endereco);
        }

        private void TrateUsuarioNaoTemPermissaoCadastroBanco()
        {
            var permissao = Sessao.ListaDePermissoes.FirstOrDefault(x => x.Funcionalidade == EnumFuncionalidade.CIDADES);

            if (!permissao.Alterar)
            {
                cboCidade.Size = new Size(pnlCidade.Width, cboCidade.Size.Height);

                btnAtalhoCidade.Visible = false;
            }
        }

        #endregion

        private void txtCep_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
