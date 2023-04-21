using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.Fiscal.CnaeServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Fiscal.Cnaes
{
    public partial class FormCadastroDeCnae : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idCnaeEdicao;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroDeCnae()
        {
            InitializeComponent();

            PreenchaCboAtividades();

            PreenchaOStatus();

            this.NomeDaTela = "Cadastro de CNAE";

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.ActiveControl = txtCodigoCnae;
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
                Cnae cnae = new Cnae();

                cnae.Id = _idCnaeEdicao;
                cnae.Descricao = txtDescricao.Text;
                cnae.Codigo = txtCodigoCnae.Text;
                cnae.DataCadastro = txtDataCadastro.Text.ToDate();
                cnae.Atividade = (EnumAtividadeCnae?)cboAtividades.EditValue;
                cnae.Status = cboStatus.EditValue.ToString();

                ServicoCnae servicoCnae = new ServicoCnae();

                if (cnae.Id == 0)
                {
                    servicoCnae.Cadastre(cnae);
                }
                else
                {
                    servicoCnae.Atualize(cnae);
                }

                _idCnaeEdicao = cnae.Id;
                PesquisePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormCnaePesquisa formCnaePesquisa = new FormCnaePesquisa();

            var cnae = formCnaePesquisa.PesquiseCnae();

            if (cnae != null)
            {
                EditeCnae(cnae);
            }
        }

        private void txtCodigoCnae_Leave(object sender, EventArgs e)
        {
            if (!txtCodigoCnae.Text.EstahVazio())
            {
                ServicoCnae servicoCnae = new ServicoCnae();

                var cnae = servicoCnae.ConsultePeloCodigo(txtCodigoCnae.Text);

                EditeCnae(cnae);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboAtividades()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumAtividadeCnae>();
            lista.Insert(0, null);

            cboAtividades.Properties.DataSource = lista;
            cboAtividades.Properties.DisplayMember = "Descricao";
            cboAtividades.Properties.ValueMember = "Valor";
        }

        private void LimpeFormulario()
        {
            _idCnaeEdicao = 0;
            txtDescricao.Text = string.Empty;

            txtCodigoCnae.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            cboAtividades.EditValue = null;

            cboStatus.EditValue = "A";

            txtCodigoCnae.Focus();
        }

        private void EditeCnae(Cnae cnae)
        {
            if (cnae != null)
            {
                _idCnaeEdicao = cnae.Id;
                txtDescricao.Text = cnae.Descricao;

                txtCodigoCnae.Text = cnae.Codigo;
                txtDescricao.Text = cnae.Descricao;
                txtDataCadastro.Text = cnae.DataCadastro.ToString("dd/MM/yyyy");
                cboAtividades.EditValue = cnae.Atividade;

                cboStatus.EditValue = cnae.Status;

                txtDescricao.Focus();
            }
            else
            {
                _idCnaeEdicao = 0;
                txtDescricao.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                cboAtividades.EditValue = null;
                cboStatus.EditValue = "A";

                txtDescricao.Focus();
            }
        }

        private void PesquisePeloId()
        {
            ServicoCnae servicoCnae = new ServicoCnae();
            var cnae = servicoCnae.Consulte(_idCnaeEdicao);

            EditeCnae(cnae);
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

        #endregion
    }
}
