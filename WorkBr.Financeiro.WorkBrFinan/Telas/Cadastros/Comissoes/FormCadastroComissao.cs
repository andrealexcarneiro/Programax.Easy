using System;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using System.Collections.Generic;
using Programax.Easy.Servico.Cadastros.ComissaoServ;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using System.Linq;

namespace Programax.Easy.View.Telas.Cadastros.Comissoes
{
    public partial class FormCadastroComissao : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoaSelecionada;
        private List<Comissao> _listaComissoes;
        private Comissao _comissaoEmEdicao;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroComissao()
        {
            InitializeComponent();

            _listaComissoes = new List<Comissao>();

            this.NomeDaTela = "Cadastro de Comissão";

            PreenchaCboFuncoes();
            PreenchaCboTabelaPrecos();

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPesquisaPessoaResumida formPesquisaPessoaResumida = new FormPesquisaPessoaResumida();

            var pessoa = formPesquisaPessoaResumida.PesquisePessoa();

            if (pessoa != null)
            {
                PreenchaCamposPessoa(pessoa);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                PesquisePessoaPeloId();
            }
            else
            {
                LimpeFormulario();
            }
        }

        private void PesquisePessoaPeloId()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();

            var pessoa = servicoPessoa.Consulte(txtId.Text.ToInt());

            if (pessoa != null)
            {
                PreenchaCamposPessoa(pessoa);
            }
            else
            {
                MessageBox.Show("Pessoa não encontrada.", "Pessoa não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                LimpeFormulario();
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (_pessoaSelecionada == null)
            {
                MessageBox.Show("É necessário selecionar um parceiro já cadastrada", "Selecione um parceiro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            Action actionSalvar = () =>
            {
                ServicoComissao servicoComissao = new ServicoComissao();

                servicoComissao.CadastreListaComissoes(_pessoaSelecionada, _listaComissoes);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void btnInserirAtualizarComissao_Click(object sender, EventArgs e)
        {
            InsiraOuAtualizeComissao();
        }

        private void btnExcluirComissao_Click(object sender, EventArgs e)
        {
            ExcluaComissao();
        }

        private void gcComissoes_DoubleClick(object sender, EventArgs e)
        {
            EditeComissao();
        }

        private void gcComissoes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditeComissao();
            }
        }

        private void btnCancelarComissao_Click(object sender, EventArgs e)
        {
            LimpeCamposComissao();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " PREENCHIMENTO DE PESSOA "

        private void PreenchaCamposPessoa(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                _pessoaSelecionada = pessoa;

                pessoa.Atendimento.CarregueLazyLoad();
                pessoa.DadosPessoais.CarregueLazyLoad();
                pessoa.EmpresaPessoa.CarregueLazyLoad();
                pessoa.Funcionario.CarregueLazyLoad();

                pessoa.ListaDeEnderecos.CarregueLazyLoad();

                pessoa.ListaDeTelefones.CarregueLazyLoad();

                txtId.Text = pessoa.Id.ToString();

                PreenchaCamposDadosGerais(pessoa.DadosGerais);

                PreenchaCamposComissao(null);

                _listaComissoes = pessoa.ListaDeComissoes.ToList();

                PreenchaGridComissoes();

                cboFuncao.Focus();
            }
            else
            {
                _pessoaSelecionada = null;

                txtId.Text = string.Empty;

                PreenchaCamposDadosGerais(null);

                PreenchaCamposComissao(null);

                _listaComissoes.Clear();

                PreenchaGridComissoes();

                txtId.Focus();
            }
        }

        private void PreenchaCamposDadosGerais(DadosGerais dadosGerais)
        {
            if (dadosGerais != null)
            {
                txtTipoPessoa.Text = dadosGerais.TipoPessoa.Descricao();
                txtDataCadastro.Text = dadosGerais.DataCadastro.ToString("dd/MM/yyyy");
                txtStatus.Text = dadosGerais.Status == "A" ? "ATIVO" : "INATIVO";

                txtRazaoSocial.Text = dadosGerais.Razao;
                txtNomeFantasia.Text = dadosGerais.NomeFantasia;

                txtCpfCnpj.Text = dadosGerais.CpfCnpj;

                if (dadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAFISICA)
                {
                    lblCpfCnpj.Text = "CPF";
                }
                else
                {
                    lblCpfCnpj.Text = "CNPJ";
                }

                if (dadosGerais.Foto == null)
                {
                    picFoto.Image = Properties.Resources.user_img;
                }
                else
                {
                    picFoto.Image = TratamentoDeImagens.ConvertByteToImagem(dadosGerais.Foto).Image;
                }
            }
            else
            {
                txtTipoPessoa.Text = string.Empty;
                txtDataCadastro.Text = string.Empty;
                txtStatus.Text = string.Empty;

                txtRazaoSocial.Text = string.Empty;
                txtNomeFantasia.Text = string.Empty;

                txtCpfCnpj.Text = string.Empty;

                picFoto.Image = Properties.Resources.user_img;
            }
        }

        #endregion

        #region " PREENCHIMENTO DE CBOS "

        private void PreenchaCboFuncoes()
        {
            var listaFuncoes = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumFuncaoPessoaComissao>();

            listaFuncoes.Insert(0, null);

            cboFuncao.Properties.DataSource = listaFuncoes;
            cboFuncao.Properties.DisplayMember = "Descricao";
            cboFuncao.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboTabelaPrecos()
        {
            ServicoTabelaPreco servicoTabelaPreco = new ServicoTabelaPreco();

            var lista = servicoTabelaPreco.ConsulteListaTabelaPrecosAtivas();

            lista.Insert(0, null);

            cboTabelaPrecos.Properties.DataSource = lista;
            cboTabelaPrecos.Properties.DisplayMember = "NomeTabela";
            cboTabelaPrecos.Properties.ValueMember = "Id";
        }

        #endregion

        private void LimpeFormulario()
        {
            PreenchaCamposPessoa(null);
            PreenchaCamposComissao(null);

            txtId.Focus();
        }

        #region " MÉTODOS MANUTENÇÃO LISTA COMISSOES "

        private void InsiraOuAtualizeComissao()
        {
            Action actionInserirComissao = () =>
            {
                var comissao = RetorneComissaoEmEdicao();

                ServicoComissao servicoComissao = new ServicoComissao();
                servicoComissao.ValideComissao(comissao, _listaComissoes);

                if (comissao.Id == 0)
                {
                    _listaComissoes.Add(comissao);
                }
                else
                {
                    int posicaoItem = _listaComissoes.IndexOf(_comissaoEmEdicao);

                    _listaComissoes.Remove(_comissaoEmEdicao);

                    _listaComissoes.Insert(posicaoItem, comissao);
                }

                GereIdFalsoParaComissoes();

                PreenchaGridComissoes();
                LimpeCamposComissao();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirComissao, exibirMensagemDeSucesso: false);
        }

        private void GereIdFalsoParaComissoes()
        {
            int contador = 1;

            foreach (var comissao in _listaComissoes)
            {
                comissao.Id = contador;

                contador++;
            }
        }

        private Comissao RetorneComissaoEmEdicao()
        {
            _comissaoEmEdicao = _comissaoEmEdicao ?? new Comissao();

            var comissao = _comissaoEmEdicao.CloneCompleto();

            comissao.DescontoMaximo = txtDescontoMaximo.Text.ToDouble();
            comissao.FuncaoPessoaComissao = (EnumFuncaoPessoaComissao?)cboFuncao.EditValue;
            comissao.MetaFaturamento = txtMeta.Text.ToDouble();
            comissao.Pessoa = _pessoaSelecionada;
            comissao.TabelaPreco = cboTabelaPrecos.EditValue != null ? new TabelaPreco { Id = cboTabelaPrecos.EditValue.ToInt(), NomeTabela = cboTabelaPrecos.Text } : null;
            comissao.ValorComissao = txtComissao.Text.ToDouble();
            comissao.DescontoMaximoEhPercentual = rdbDescontoMaximoPercentual.Checked;
            comissao.ValorComissaoEhPercentual = rdbComissaoPercentual.Checked;

            return comissao;
        }

        private void ExcluaComissao()
        {
            if (_listaComissoes.Count > 0)
            {
                var idItem = colunaId.View.GetFocusedRowCellValue(colunaId).ToInt();

                var itemASerExcluido = _listaComissoes.FirstOrDefault(x => x.Id == idItem);

                var mensagemConfirmacaoExclusao = "Deseja excluir a comissão do " + itemASerExcluido.FuncaoPessoaComissao.Descricao() + " e tabela de preço  " + itemASerExcluido.TabelaPreco.NomeTabela + " ?";

                if (MessageBox.Show(mensagemConfirmacaoExclusao, "Deseja excluir esta comissão ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    _listaComissoes.Remove(itemASerExcluido);

                    PreenchaGridComissoes();

                    LimpeCamposComissao();
                }
            }
        }

        private void PreenchaGridComissoes()
        {
            List<ComissaoGrid> listaComissaoGrid = new List<ComissaoGrid>();

            foreach (var comissao in _listaComissoes)
            {
                ComissaoGrid comissaoGrid = new ComissaoGrid();

                comissaoGrid.Comissao = comissao.ValorComissao.ToString("0.00");
                comissaoGrid.DescontoMaximo = comissao.DescontoMaximo.ToString("0.00");
                comissaoGrid.Funcao = comissao.FuncaoPessoaComissao.Descricao();
                comissaoGrid.Id = comissao.Id;
                comissaoGrid.MetaFaturamento = comissao.MetaFaturamento.ToString("0.00");
                comissaoGrid.TabelaPreco = comissao.TabelaPreco.NomeTabela;

                listaComissaoGrid.Add(comissaoGrid);
            }

            gcComissoes.DataSource = listaComissaoGrid;
            gcComissoes.RefreshDataSource();
        }

        private void EditeComissao()
        {
            if (_listaComissoes != null && _listaComissoes.Count > 0)
            {
                var comissao = _listaComissoes.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

                PreenchaCamposComissao(comissao);
            }
        }

        private void PreenchaCamposComissao(Comissao comissao)
        {
            _comissaoEmEdicao = comissao;

            if (comissao != null)
            {
                cboFuncao.EditValue = comissao.FuncaoPessoaComissao;
                cboTabelaPrecos.EditValue = comissao.TabelaPreco != null ? (int?)comissao.TabelaPreco.Id : null;
                txtMeta.Text = comissao.MetaFaturamento.ToString("0.00");
                txtDescontoMaximo.Text = comissao.DescontoMaximo.ToString("0.00");
                txtComissao.Text = comissao.ValorComissao.ToString("0.00");

                rdbDescontoMaximoValor.Checked = true;
                rdbDescontoMaximoPercentual.Checked = comissao.DescontoMaximoEhPercentual;

                rdbComissaoValor.Checked = true;
                rdbComissaoPercentual.Checked = comissao.ValorComissaoEhPercentual;

                btnInserirAtualizarComissao.Image = Properties.Resources.icon_atualizar;

                cboFuncao.Focus();
            }
            else
            {
                cboFuncao.EditValue = null;
                cboTabelaPrecos.EditValue = null;
                txtMeta.Text = string.Empty;
                txtDescontoMaximo.Text = string.Empty;
                txtComissao.Text = string.Empty;
                rdbDescontoMaximoPercentual.Checked = true;
                rdbComissaoPercentual.Checked = true;

                btnInserirAtualizarComissao.Image = Properties.Resources.icones2_19;
            }
        }

        private void LimpeCamposComissao()
        {
            PreenchaCamposComissao(null);

            cboFuncao.Focus();
        }

        #endregion

        #endregion

        #region " CLASSES AUXILIARES "

        public class ComissaoGrid
        {
            public int Id { get; set; }

            public string Funcao { get; set; }

            public string TabelaPreco { get; set; }

            public string MetaFaturamento { get; set; }

            public string DescontoMaximo { get; set; }

            public string Comissao { get; set; }
        }

        #endregion

        private void picFoto_Click(object sender, EventArgs e)
        {

        }
    }
}
