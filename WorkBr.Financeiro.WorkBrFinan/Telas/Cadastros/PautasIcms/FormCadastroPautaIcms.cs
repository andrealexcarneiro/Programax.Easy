using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Easy.Negocio.Cadastros.PautaIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PautaIcmsServ;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.Cadastros.PautasIcms
{
    public partial class FormCadastroPautaIcms : FormularioPadrao
    {
        #region " VARIÁVIES PRIVADAS "

        private Produto _produtoEmEdicao;
        private PautaIcms _pautaIcms;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroPautaIcms()
        {
            InitializeComponent();

            PreenchaOsEstados();
            PreenchaOStatus();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.ActiveControl = txtCodigoDeBarrasProduto;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void cboEstado_EditValueChanged(object sender, EventArgs e)
        {
            ServicoCidade servicoCidade = new ServicoCidade();

            string uf = cboEstado.EditValue == null ? string.Empty : cboEstado.EditValue.ToString();

            var listaDeCidades = servicoCidade.ConsulteListaCidadesAtivasPorEstado(uf);

            listaDeCidades.Insert(0, null);

            cboCidade.Properties.DataSource = listaDeCidades;
            cboCidade.Properties.DisplayMember = "Descricao";
            cboCidade.Properties.ValueMember = "Id";

            cboCidade.EditValue = null;

            PesquisePautaIcms();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void txtCodigoDeBarrasProduto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoDeBarrasProduto.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(txtCodigoDeBarrasProduto.Text);

                PreenchaProduto(produto, exibirMensagemDeNaoEncontrado: true);
            }
            else
            {
                PreenchaProduto(null);
            }
        }

        private void txtIdProduto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdProduto.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoAtivo(txtIdProduto.Text.ToInt());

                PreenchaProduto(produto, exibirMensagemDeNaoEncontrado: true);
            }
            else
            {
                PreenchaProduto(null);
            }
        }

        private void btnPesquisaProduto2_Click(object sender, EventArgs e)
        {
            FormPesquisaProdutoResumida formPesquisaProdutoResumida = new FormPesquisaProdutoResumida();
            var produto = formPesquisaProdutoResumida.PesquiseProduto();

            if (produto != null)
            {
                PreenchaProduto(produto);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                var pautaIcms = RetornePautaIcmsEmEdicao();

                ServicoPautaIcms servicoPautaIcms = new ServicoPautaIcms();

                if (pautaIcms.Id == 0)
                {
                    servicoPautaIcms.Cadastre(pautaIcms);
                }
                else
                {
                    servicoPautaIcms.Atualize(pautaIcms);
                }

                PreenchaPautaIcms(pautaIcms);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void cboCidade_EditValueChanged(object sender, EventArgs e)
        {
            PesquisePautaIcms();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaOsEstados()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var listaDeEstados = servicoEstado.ConsulteListaEstados();

            listaDeEstados.Insert(0, null);

            cboEstado.Properties.DataSource = listaDeEstados;
            cboEstado.Properties.DisplayMember = "Nome";
            cboEstado.Properties.ValueMember = "UF";
        }

        private void PreenchaProduto(Produto produto, bool exibirMensagemDeNaoEncontrado = false)
        {
            _produtoEmEdicao = produto;

            if (produto != null)
            {
                txtIdProduto.Text = produto.Id.ToString();
                txtCodigoDeBarrasProduto.Text = produto.DadosGerais.CodigoDeBarras;
                txtDescricaoProduto.Text = produto.DadosGerais.Descricao;
                txtUnidade.Text = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Descricao : string.Empty;
                txtSituacao.Text = produto.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";
                txtFabricante.Text = produto.Principal.Fabricante != null ? produto.Principal.Fabricante.Descricao : string.Empty;

                cboEstado.Focus();
            }
            else
            {
                txtIdProduto.Text = string.Empty;
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtDescricaoProduto.Text = string.Empty;
                txtUnidade.Text = string.Empty;
                txtSituacao.Text = string.Empty;
                txtFabricante.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Produto nao encontrado!", "Produto não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoDeBarrasProduto.Focus();
                }
            }

            PesquisePautaIcms();
        }

        private void LimpeFormulario()
        {
            PreenchaPautaIcms(null);
            PreenchaProduto(null);
            cboEstado.EditValue = null;



            txtCodigoDeBarrasProduto.Focus();
        }

        private PautaIcms RetornePautaIcmsEmEdicao()
        {
            _pautaIcms = _pautaIcms ?? new PautaIcms();

            _pautaIcms.Produto = !string.IsNullOrEmpty(txtIdProduto.Text) ? new Produto { Id = txtIdProduto.Text.ToInt() } : null;
            _pautaIcms.Cidade = cboCidade.EditValue != null ? new Cidade { Id = cboCidade.EditValue.ToInt() } : null;
            _pautaIcms.Estado = cboEstado.EditValue != null ? new Estado { UF = cboEstado.EditValue.ToString() } : null;

            _pautaIcms.DataInicio = txtDataInicioVigencia.Text.ToDateNullabel();
            _pautaIcms.AliquotaSubstituicao = txtAliquotaSubstituicaoTributaria.Text.ToDouble();
            _pautaIcms.Codigo = txtCodigoPcms.Text;
            _pautaIcms.Instrucao = txtInstrucao.Text;
            _pautaIcms.PrecoPauta = txtPrecoPauta.Text.ToDouble();
            _pautaIcms.Status = cboStatus.EditValue.ToString();
            _pautaIcms.DataCadastro = txtDataCadastro.Text.ToDate();

            return _pautaIcms;
        }

        private void PesquisePautaIcms()
        {
            var produto = !string.IsNullOrEmpty(txtIdProduto.Text) ? new Produto { Id = txtIdProduto.Text.ToInt() } : null;
            var cidade = cboCidade.EditValue != null ? new Cidade { Id = cboCidade.EditValue.ToInt() } : null;
            var estado = cboEstado.EditValue != null ? new Estado { UF = cboEstado.EditValue.ToString() } : null;

            ServicoPautaIcms servicoPautaIcms = new ServicoPautaIcms();

            var pautaIcms = servicoPautaIcms.Consulte(produto, estado, cidade);

            PreenchaPautaIcms(pautaIcms);
        }

        private void PreenchaPautaIcms(PautaIcms pautaIcms)
        {
            _pautaIcms = pautaIcms;

            if (pautaIcms != null)
            {
                txtAliquotaSubstituicaoTributaria.Text = pautaIcms.AliquotaSubstituicao.ToString("0.00");
                txtCodigoPcms.Text = pautaIcms.Codigo;
                txtPrecoPauta.Text = pautaIcms.PrecoPauta.ToString("0.00");

                if (pautaIcms.DataInicio != null)
                {
                    txtDataInicioVigencia.DateTime = pautaIcms.DataInicio.Value;
                }
                else
                {
                    txtDataInicioVigencia.Text = string.Empty;
                }

                txtInstrucao.Text = pautaIcms.Instrucao;

                txtDataCadastro.Text = pautaIcms.DataCadastro.ToString("dd/MM/yyyy");

                cboStatus.EditValue = pautaIcms.Status;
            }
            else
            {
                txtAliquotaSubstituicaoTributaria.Text = string.Empty;
                txtCodigoPcms.Text = string.Empty;
                txtPrecoPauta.Text = string.Empty;
                txtDataInicioVigencia.Text = string.Empty;
                txtInstrucao.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                cboStatus.EditValue = "A";
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

        #endregion
    }
}