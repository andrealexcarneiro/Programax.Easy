using System;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Financeiro.BancosParaMovimento;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.View.Telas.Financeiro.Categorias;
using Programax.Easy.Servico.Financeiro.CategoriaServ;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using System.Collections.Generic;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco
{
    public partial class FormAberturaBanco : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private MovimentacaoBanco _movimentacaoBanco;

        #endregion

        #region " CONSTRUTOR "

        public FormAberturaBanco()
        {
            InitializeComponent();

            if (!Sessao.GrupoAcesso.Tesoureiro)
            {
                txtIdBanco.Properties.ReadOnly = true;
                btnPesquisarBanco.Visible = false;
            }

            PreenchaCboCategorias();
            cboCategoria.EditValue = 24; //Saldo Inicial - > Padrão. 15/08/2021
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnPesquisarBanco_Click(object sender, EventArgs e)
        {
            FormPesquisaBancoParaMovimento formPesquisaDeBancos = new FormPesquisaBancoParaMovimento();

            var banco = formPesquisaDeBancos.PesquiseUmBanco();

            if (banco != null)
            {
                PreenchaInformacoesBanco(banco);
            }
        }

        private void txtIdBanco_Leave(object sender, EventArgs e)
        {
            if (txtIdBanco.Enabled && !string.IsNullOrEmpty(txtIdBanco.Text))
            {
                ServicoBancoParaMovimento servicoBanco = new ServicoBancoParaMovimento();
                var banco = servicoBanco.Consulte(txtIdBanco.Text.ToInt());

                PreenchaInformacoesBanco(banco, exibirMensagemDeNaoEncontrado: true);
            }
        }

        private void btnAbrirBanco_Click(object sender, EventArgs e)
        {
            if (cboCategoria.EditValue == null)
            {
                MessageBox.Show("Informe a Categoria Financeira para continuar.", "Movimentações Bancárias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Action actionMovimentacao = () =>
            {
                BancoParaMovimento banco = new BancoParaMovimento { Id = txtIdBanco.Text.ToInt() };

                MovimentacaoBanco movimentacaoBanco = new MovimentacaoBanco();

                movimentacaoBanco.Banco = banco;
                movimentacaoBanco.ObservacoesAbertura = txtObs.Text;
                movimentacaoBanco.DataHoraAbertura = txtDataHoraAbertura.Text.ToDate();
                movimentacaoBanco.UsuarioAbertura = Sessao.PessoaLogada;
                movimentacaoBanco.Categoria = cboCategoria.EditValue ==null? null: new CategoriaFinanceira { Id = cboCategoria.EditValue.ToInt() };
                
                ServicoMovimentacaoBanco servicoMovimentacaoBanco = new ServicoMovimentacaoBanco();

                //O Registro de abertura é feito em Dinheiro
                movimentacaoBanco.SaldoInicial = txtValorDinheiro.Text.ToDouble();               
                movimentacaoBanco.Status = EnumStatusMovimentacaoCaixa.ABERTO;
                                
                servicoMovimentacaoBanco.Cadastre(movimentacaoBanco);
                
                servicoMovimentacaoBanco = new ServicoMovimentacaoBanco();
                _movimentacaoBanco = servicoMovimentacaoBanco.Consulte(movimentacaoBanco.Id);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionMovimentacao, this, fecharFormAoConcluirOperacao: true, mensagemDeSucesso: "O Banco foi aberto com sucesso!");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnAdicionarCategoria_Click(object sender, EventArgs e)
        {
            FormCadastroCategoriasFinanceiras formCategoria = new FormCadastroCategoriasFinanceiras();
            formCategoria.ShowDialog();

            PreenchaCboCategorias();

            cboCategoria.EditValue = null;

        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public MovimentacaoBanco AbrirBanco(BancoParaMovimento banco)
        {
            PreenchaInformacoesBanco(banco);

            this.ShowDialog();

            return _movimentacaoBanco;
        }

        private void PreenchaCboCategorias()
        {
            List<CategoriaFinanceira> categoria = new List<CategoriaFinanceira>();
            
            categoria = new ServicoCategoria().ConsulteLista(string.Empty, null, "A", EnumTipoCategoria.RECEITA);

            categoria.Insert(0, null);

            cboCategoria.Properties.DisplayMember = "Descricao";
            cboCategoria.Properties.ValueMember = "Id";
            cboCategoria.Properties.DataSource = categoria;

            if (cboCategoria.EditValue != null)
            {
                if (!categoria.Exists(categ => categ != null && categ.Id == cboCategoria.EditValue.ToInt()))
                {
                    cboCategoria.EditValue = null;
                }
            }
        }

        #endregion

        #region " MÉTODOS PRIVADOS "

        private void PreenchaInformacoesBanco(BancoParaMovimento banco, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (banco != null)
            {
                txtIdBanco.Text = banco.Id.ToString();
                txtNomeBanco.Text = banco.NomeBanco;

                txtDataHoraAbertura.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                txtUsuarioAbertura.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;                

                ServicoMovimentacaoBanco servicoBancoMovimentacao = new ServicoMovimentacaoBanco();

                var bancomovimentacao = servicoBancoMovimentacao.ConsulteLista(banco, EnumDataFiltrarMovimentacaoCaixa.DATAFECHAMENTO, DateTime.Now.AddMonths(-1), DateTime.Now, EnumStatusMovimentacaoCaixa.FECHADO);

                //Verifica se tem banco dentro do primeiro mês
                if (bancomovimentacao.Count != 0)
                {
                    if (bancomovimentacao != null)
                    {
                        txtValorDinheiro.Text = bancomovimentacao.LastOrDefault().SaldoFinal != 0 ? bancomovimentacao.LastOrDefault().SaldoFinal.ToString("0.00") : "0.00";                       
                    }
                    else
                    {
                        bancomovimentacao = servicoBancoMovimentacao.ConsulteLista(banco, EnumDataFiltrarMovimentacaoCaixa.DATAFECHAMENTO, DateTime.Now.AddMonths(-7), DateTime.Now, EnumStatusMovimentacaoCaixa.FECHADO);

                        //Senão tiver banco no primeiro mês cai aqui para buscar até o sexto mês anterior
                        if (bancomovimentacao != null)
                        {
                            txtValorDinheiro.Text = bancomovimentacao.LastOrDefault().SaldoFinal != 0 ? bancomovimentacao.LastOrDefault().SaldoFinal.ToString("0.00") : "0.00";                            
                        }
                        else
                        {
                            //Caso usuário não tiver nenhum Banco dentro dos perídos acima
                            txtValorDinheiro.Text = "0.00";                           
                        }
                    }
                }
                else
                {
                    //Caso usuário nunca abriu um banco
                    txtValorDinheiro.Text = "0.00";                   
                }
            }
            else
            {
                txtIdBanco.Text = string.Empty;
                txtNomeBanco.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Banco não encontrado", "Banco não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                this.ActiveControl = txtIdBanco;
                txtIdBanco.Focus();
            }
        }

        #endregion

       
    }
}
