using System;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CorrecaoEstoqueServ;
using Programax.Easy.Servico.Cadastros.MotivoCorrecaoEstoqueServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using System.Text.RegularExpressions;

namespace Programax.Easy.View.Telas.Cadastros.CorrecoesEstoque
{
    public partial class FormCorrecaoEstoque : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private bool _calculeDiferenca;
        private bool _calculeSaldoIdentificado;
        private bool _campoCodigoAlterado;

        #endregion

        #region " CONSTRUTOR "

        public FormCorrecaoEstoque()
        {
            InitializeComponent();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.NomeDaTela = "Correção do Estoque";

            PreenchaCboTipoMovimentacaoEstoque();

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormCorrecaoEstoque_Load(object sender, EventArgs e)
        {
            PreenchaCboMotivoCorrecaoEstoque();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Salve();
        }
        
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            PesquiseProduto();
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Enabled)
            {
                ServicoProduto servicoProduto = new ServicoProduto();
                var produto = servicoProduto.Consulte(txtId.Text.ToInt());

                PreenchaDadosProduto(produto, true);
            }
        }

        private void txtId_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

       
        private void txtCodigoDeBarras_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }
        
        private void txtCodigoDeBarras_Leave(object sender, EventArgs e)
        {  
            if (!string.IsNullOrEmpty(txtCodigoDeBarras.Text) && txtCodigoDeBarras.Enabled)
            {
                ServicoProduto servicoProduto = new ServicoProduto();
                var produto = servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(txtCodigoDeBarras.Text);

                PreenchaDadosProduto(produto, true);
            }
        }

        private void txtCodigoDeBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {   
                ServicoProduto servicoProduto = new ServicoProduto();
                var produto = servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(txtCodigoDeBarras.Text);

                PreenchaDadosProduto(produto, true);
            }
        }

        private void txtCodigoDeBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCodigoDeBarras.Text.Length >= 3 && _campoCodigoAlterado && e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete)
            {
                _campoCodigoAlterado = false;

                if (Regex.IsMatch(txtCodigoDeBarras.Text, @"^[ a-zA-Z á]*$"))
                {
                    
                }
            }

            _campoCodigoAlterado = false;
        }

        private void txtDiferencaEstoque_EditValueChanged(object sender, EventArgs e)
        {
            if (_calculeSaldoIdentificado)
            {
                CalculeSaldoIdentificado();

                _calculeSaldoIdentificado = false;
            }
        }

        private void cboTipoMovimentacaoEstoque_EditValueChanged(object sender, EventArgs e)
        {
            CalculeSaldoIdentificado();
        }

        private void txtSaldoIdentificadoEmEstoque_EditValueChanged(object sender, EventArgs e)
        {
            if (_calculeDiferenca)
            {
                CalculeDiferenca();

                _calculeDiferenca = false;
            }
        }

        private void txtSaldoIdentificadoEmEstoque_KeyDown(object sender, KeyEventArgs e)
        {
            _calculeDiferenca = true;
        }

        private void txtDiferencaEstoque_KeyDown(object sender, KeyEventArgs e)
        {
            _calculeSaldoIdentificado = true;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void Salve()
        {
            this.Cursor = Cursors.WaitCursor;
            Action actionSalvar = () =>
            {
                var correcaoEstoque = RetorneMovimentacaoBaseEmEdicao();

                ServicoCorrecaoEstoque servicoCorrecaoEstoque = new ServicoCorrecaoEstoque();

                servicoCorrecaoEstoque.Cadastre(correcaoEstoque);

                ServicoProduto servicoProduto = new ServicoProduto();

                //Deve-se atualizar o estoque reservado para que o estoque disponível fique correto
                foreach ( var produto in correcaoEstoque.ListaDeItens)
                {
                var ProdutoAAtualizar = servicoProduto.Consulte(produto.Produto.Id);

                    if (ProdutoAAtualizar.FormacaoPreco.EstoqueReservado < 0)
                    {
                        ProdutoAAtualizar.FormacaoPreco.EstoqueReservado = 0;
                    }
                   

                    servicoProduto.Atualize(ProdutoAAtualizar);
                }

                LimpeFormulario();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);

            this.Cursor = Cursors.Default;
        }

        private MovimentacaoBase RetorneMovimentacaoBaseEmEdicao()
        {
            MovimentacaoBase movimentacao = new MovimentacaoBase();

            movimentacao.DataCadastro = DateTime.Now;
            movimentacao.DataMovimentacao = movimentacao.DataCadastro;
            movimentacao.Motivo = cboMotivoCorrecaoEstoque.EditValue != null ? new MotivoCorrecaoEstoque { Id = cboMotivoCorrecaoEstoque.EditValue.ToInt() } : null;
            movimentacao.Observacoes = txtObservacoes.Text;
            movimentacao.TipoMovimentacao = (EnumTipoMovimentacao)cboTipoMovimentacaoEstoque.EditValue;
            
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                itemMovimentacao.Produto = new Produto { Id = txtId.Text.ToInt() };
                itemMovimentacao.Quantidade = txtDiferencaEstoque.Text.ToDouble();
                itemMovimentacao.TipoMovimentacao = (EnumTipoMovimentacao)cboTipoMovimentacaoEstoque.EditValue;

                movimentacao.ListaDeItens.Add(itemMovimentacao);
            }

            return movimentacao;
        }

        private void LimpeFormulario()
        {
            PreenchaDadosProduto(null, false);
        }

        private void PreenchaDadosProduto(Produto produto, bool exibirMensagemDeNaoEncontrado)
        {
            if (produto != null)
            {
                txtId.Text = produto.Id.ToString();
                txtCodigoDeBarras.Text = produto.DadosGerais.CodigoDeBarras;
                txtUnidade.Text = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Abreviacao : string.Empty;
                txtStatus.Text = produto.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";
                txtDescricao.Text = produto.DadosGerais.Descricao;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                if (produto.FormacaoPreco != null && produto.FormacaoPreco.Estoque != null)
                {
                    txtSaldoRealEmEstoque.Text = produto.DadosGerais.PermiteVendaFracionada ? produto.FormacaoPreco.Estoque.ToString("0.0000") : produto.FormacaoPreco.Estoque.ToString();
                }
                else
                {
                    txtSaldoRealEmEstoque.Text = produto.DadosGerais.PermiteVendaFracionada ? "0.0000" : "0";
                }

                txtSaldoIdentificadoEmEstoque.Text = string.Empty;
                txtDiferencaEstoque.Text = string.Empty;
                cboMotivoCorrecaoEstoque.EditValue = null;
                txtObservacoes.Text = string.Empty;

                if (produto.DadosGerais.Foto == null)
                {
                    picFoto.Image = Properties.Resources.produtos;
                }
                else
                {
                    picFoto.Image = TratamentoDeImagens.ConvertByteToImagem(produto.DadosGerais.Foto).Image;
                }

                AltereMascaraQuantidadeProduto(produto);

                cboTipoMovimentacaoEstoque.Focus();
            }
            else
            {
                txtId.Text = string.Empty;
                txtCodigoDeBarras.Text = string.Empty;
                txtUnidade.Text = string.Empty;
                txtStatus.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtSaldoRealEmEstoque.Text = string.Empty;
                txtDiferencaEstoque.Text = string.Empty;
                txtSaldoIdentificadoEmEstoque.Text = string.Empty;
                cboMotivoCorrecaoEstoque.EditValue = null;
                txtObservacoes.Text = string.Empty;
                picFoto.Image = Properties.Resources.produtos;

                txtId.Focus();

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Produto não encontrado", "Produto não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void PreenchaCboMotivoCorrecaoEstoque()
        {
            ServicoMotivoCorrecaoEstoque servicoMotivoCorrecaoEstoque = new ServicoMotivoCorrecaoEstoque();
            var listaMotivos = servicoMotivoCorrecaoEstoque.ConsulteLista();

            cboMotivoCorrecaoEstoque.Properties.DataSource = listaMotivos;
            cboMotivoCorrecaoEstoque.Properties.DisplayMember = "Descricao";
            cboMotivoCorrecaoEstoque.Properties.ValueMember = "Id";
        }

        private void PesquiseProduto()
        {
            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProduto();

            if (produto != null)
            {
                PreenchaDadosProduto(produto, false);
            }
        }

        private void AltereMascaraQuantidadeProduto(Produto produto)
        {
            if (produto.DadosGerais.PermiteVendaFracionada)
            {
                txtSaldoIdentificadoEmEstoque.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
                txtDiferencaEstoque.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtSaldoIdentificadoEmEstoque.Properties.Mask.EditMask = @"[0-9]{1,11}";
                txtDiferencaEstoque.Properties.Mask.EditMask = @"[0-9]{1,11}";
            }
        }

        private void PreenchaCboTipoMovimentacaoEstoque()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoMovimentacao>();

            lista.RemoveAt(2);

            cboTipoMovimentacaoEstoque.Properties.DisplayMember = "Descricao";
            cboTipoMovimentacaoEstoque.Properties.ValueMember = "Valor";
            cboTipoMovimentacaoEstoque.Properties.DataSource = lista;

            cboTipoMovimentacaoEstoque.EditValue = EnumTipoMovimentacao.ENTRADA;
        }

        private void CalculeSaldoIdentificado()
        {
            double saldoRealEstoque = txtSaldoRealEmEstoque.Text.ToDouble();
            double diferenca = txtDiferencaEstoque.Text.ToDouble();

            double saldoIdentificadoEstoque = 0;

            if (EnumTipoMovimentacao.ENTRADA == (EnumTipoMovimentacao)cboTipoMovimentacaoEstoque.EditValue)
            {
                saldoIdentificadoEstoque = saldoRealEstoque + diferenca;
            }
            else
            {
                saldoIdentificadoEstoque = saldoRealEstoque - diferenca;
            }

            txtSaldoIdentificadoEmEstoque.Text = saldoIdentificadoEstoque.ToString("0.##");
        }

        private void CalculeDiferenca()
        {
            double saldoRealEstoque = txtSaldoRealEmEstoque.Text.ToDouble();
            double saldoIdentificadoEstoque = txtSaldoIdentificadoEmEstoque.Text.ToDouble();

            double diferenca = saldoIdentificadoEstoque - saldoRealEstoque;

            if (diferenca > 0)
            {
                cboTipoMovimentacaoEstoque.EditValue = EnumTipoMovimentacao.ENTRADA;
            }
            else
            {
                cboTipoMovimentacaoEstoque.EditValue = EnumTipoMovimentacao.SAIDA;
            }

            txtDiferencaEstoque.Text = Math.Abs(diferenca).ToString("0.##");
        }

        #endregion
    }
}
