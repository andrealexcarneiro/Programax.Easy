using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Cadastros.Cidades
{
    public partial class FormCadastroCidade : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idCidade;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroCidade()
        {
            InitializeComponent();

            this.NomeDaTela = "Cadastro de Cidade";

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.ActiveControl = txtCodigoIbge;
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
            Cidade cidade = new Cidade();

            cidade.Id = _idCidade;
            cidade.Descricao = txtDescricao.Text;
            cidade.CodigoIbge = txtCodigoIbge.Text;
            cidade.DataCadastro = txtDataCadastro.Text.ToDate();
            cidade.Status = cboStatus.EditValue.ToString();

            if (cboEstados.EditValue != null)
            {
                cidade.Estado = new Estado { UF = cboEstados.EditValue.ToString() };
            }

            Action actionSalvar = () =>
            {
                ServicoCidade servicoCidade = new ServicoCidade();

                if (cidade.Id == 0)
                {
                    servicoCidade.Cadastre(cidade);
                }
                else
                {
                    servicoCidade.Atualize(cidade);
                }

                PesquiseCidadePeloCodigoIbge();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormCidadePesquisa formPesquisaDeCidadees = new FormCidadePesquisa();

            var cidade = formPesquisaDeCidadees.PesquiseCidade();

            if (cidade != null)
            {
                EditeCidade(cidade);
            }
        }

        private void FormCadastroCidade_Load(object sender, EventArgs e)
        {
            PreenchaOStatus();
            PreenchaCboEstados();
        }

        private void txtCodigoIbge_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodigoIbge.Text))
            {
                PesquiseCidadePeloCodigoIbge();
            }
            else
            {
                EditeCidade(null);
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

        private void PreenchaCboEstados()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var categorias = servicoEstado.ConsulteListaEstados();

            categorias.Insert(0, null);

            cboEstados.Properties.DataSource = categorias;
            cboEstados.Properties.DisplayMember = "Nome";
            cboEstados.Properties.ValueMember = "UF";
        }

        private void LimpeFormulario()
        {
            EditeCidade(null, limparCodigoIbge: true, focoNoCodigoIbge: true);
        }

        private void EditeCidade(Cidade cidade, bool limparCodigoIbge = false, bool focoNoCodigoIbge = false)
        {
            if (cidade != null)
            {
                _idCidade = cidade.Id;
                txtDescricao.Text = cidade.Descricao;
                txtCodigoIbge.Text = cidade.CodigoIbge;

                if (cidade.Estado != null)
                {
                    cboEstados.EditValue = cidade.Estado.UF;
                }
                else
                {
                    cboEstados.EditValue = null;
                }

                txtDataCadastro.Text = cidade.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = cidade.Status;

                txtDescricao.Focus();
            }
            else
            {
                _idCidade = 0;
                
                txtDescricao.Text = string.Empty;

                if (limparCodigoIbge)
                {
                    txtCodigoIbge.Text = string.Empty;
                }

                cboEstados.EditValue = null;

                cboStatus.EditValue = "A";
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                if (focoNoCodigoIbge)
                {
                    txtCodigoIbge.Focus();
                }
                else
                {
                    txtDescricao.Focus();
                }
            }
        }

        private void PesquiseCidadePeloCodigoIbge()
        {
            ServicoCidade servicoCidade = new ServicoCidade();
            var cidade = servicoCidade.ConsultePeloCodigoIbge(txtCodigoIbge.Text);

            EditeCidade(cidade);
        }

        #endregion
    }
}
