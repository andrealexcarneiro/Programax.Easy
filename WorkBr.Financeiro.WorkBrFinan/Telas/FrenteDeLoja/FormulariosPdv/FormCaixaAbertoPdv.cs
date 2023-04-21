using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.InformacaoSistemaServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.ConfiguracoesSistema.ConfiguracoesPdvServ;
using Programax.Easy.View.Telas.Financeiro.MovimentacoesCaixa;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormCaixaAbertoPdv : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private bool _clienteEncontrado;
        private Pessoa _cliente;
        private Pessoa _clientePadrao;

        #endregion

        #region " CONSTRUTOR "

        public FormCaixaAbertoPdv()
        {
            InitializeComponent();

            PreenchaDadosEmpresa();
            PreenchaCaixaUsuario();
            PreenchaCLientePadrao();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void tmrHoraAtual_Tick(object sender, EventArgs e)
        {
            lblDataHoraAtual.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }

        private void FormCaixaAbertoPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                var sairPdv = MessageBoxAkil.Show("Deseja sair do pdv?", "Sair do pdv", MessageBoxButtons.OKCancel);

                if (sairPdv == DialogResult.Cancel)
                {
                    return;
                }

                this.Close();
            }
            else if (e.KeyCode == Keys.F4)
            {
                //FormSangriaPdv formSangriaPdv = new FormSangriaPdv();
                //formSangriaPdv.AbrirTelaModal(true);
            }
            else if (e.KeyCode == Keys.F5)
            {
                FormSuprimentoPdv formSuprimentoPdv = new FormSuprimentoPdv();
                formSuprimentoPdv.AbrirTelaModal(true);
            }
            else if (e.KeyCode == Keys.F6)
            {
                FormCadastroMovimentacoesCaixa formCadastroMovimentacaoCaixa = new FormCadastroMovimentacoesCaixa();
                formCadastroMovimentacaoCaixa.AbrirTelaModal(true);

                //FormVisualizarMovimentacaoCaixa formVisualizarMovimentacaoCaixa = new FormVisualizarMovimentacaoCaixa();
                //formVisualizarMovimentacaoCaixa.AbrirTelaModal(true);
            }
            else if (e.KeyCode == Keys.F7)
            {
                FormFecharCaixaPdv formFecharCaixaPdv = new FormFecharCaixaPdv();
                formFecharCaixaPdv.AbrirTelaModal(true);

                PreenchaCaixaUsuario();
            }
        }

        private void txtCpfCnpj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_clienteEncontrado)
                {  
                    if (_cliente.ListaDeEnderecos.Count == 0)
                    {
                        var configuracoesPdv = new ServicoConfiguracoesPdv().ConsulteUltimaConfiguracaoPdv();

                        if(configuracoesPdv.EmitirNotaFiscalDiretamenteNoPDV)
                        {
                            MessageBox.Show("Para continuar. É necessário cadastrar o endereço do cliente.", "Cliente PDV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    if(_cliente.DadosGerais.Status=="I")
                    {
                        if(MessageBox.Show ("O cliente selecionado está INATIVO. Deseja continuar mesmo assim?","Deseja Continuar?", MessageBoxButtons.YesNo)==DialogResult.No)
                        {
                            return;
                        }
                    }

                    AbraFormPdv();

                    return;
                }

                if (txtCpfCnpj.Text.EstahVazioOuZerado())
                {  
                    _cliente = _clientePadrao;

                    if (_cliente.ListaDeEnderecos.Count == 0)
                    {
                        var configuracoesPdv = new ServicoConfiguracoesPdv().ConsulteUltimaConfiguracaoPdv();

                        if (configuracoesPdv.EmitirNotaFiscalDiretamenteNoPDV)
                        {
                            MessageBox.Show("Para continuar. É necessário cadastrar o endereço do cliente.", "Cliente PDV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }                     

                    AbraFormPdv();
                }
                else
                {
                    string cpfCnpj = txtCpfCnpj.Text;
                    bool verificaCpfCnpjValido = false;

                    if (txtCpfCnpj.Text.Length == 11)
                    {
                        verificaCpfCnpjValido = ValidacoesGerais.CpfEstahValido(cpfCnpj);

                        cpfCnpj = cpfCnpj.Insert(9, "-");
                        cpfCnpj = cpfCnpj.Insert(6, ".");
                        cpfCnpj = cpfCnpj.Insert(3, ".");
                    }
                    else
                    {
                        verificaCpfCnpjValido = ValidacoesGerais.CnpjEstahValido(cpfCnpj);

                        cpfCnpj = cpfCnpj.FormatarParaMascaraCnpj();
                    }

                    if (!verificaCpfCnpjValido)
                    {
                        MessageBoxAkil.Show("Cpf ou Cnpj inválido.", "Cpf ou Cnpj Inválido.");

                        return;
                    }

                    ServicoPessoa servicoPessoa = new ServicoPessoa(false, true);
                    _cliente = servicoPessoa.ConsultePessoaPeloCnpjOuCpf(cpfCnpj);

                    if (_cliente != null)
                    {
                        txtRazaoSocial.Text = _cliente.DadosGerais.Razao;

                        _clienteEncontrado = true;
                    }
                    else
                    {
                        txtRazaoSocial.Text = string.Empty;

                        Pessoa pessoa = new Pessoa();
                        pessoa.DadosGerais.TipoCliente = EnumTipoCliente.CONSUMIDOR;
                        pessoa.DadosGerais.TipoPessoa = cpfCnpj.Length == 14 ? EnumTipoPessoa.PESSOAFISICA : EnumTipoPessoa.PESSOAJURIDICA;
                        pessoa.DadosGerais.EhCliente = true;
                        pessoa.DadosGerais.CpfCnpj = cpfCnpj;
                        pessoa.DadosGerais.DataCadastro = DateTime.Now.Date;
                        pessoa.DadosGerais.Status = "A";
                        pessoa.DadosGerais.Razao = "Consumidor Final";
                        pessoa.DadosGerais.NomeFantasia = "Consumidor Final";

                        servicoPessoa.Cadastre(pessoa);

                        _cliente = pessoa;

                        AbraFormPdv();
                    }
                }
            }
        }

        private void txtCpfCnpj_EditValueChanged(object sender, EventArgs e)
        {
            _clienteEncontrado = false;
            _cliente = null;

            txtRazaoSocial.Text = string.Empty;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaDadosEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            lblEmpresa.Text = empresa.DadosEmpresa.RazaoSocial;

            ServicoInformacaoSistema servicoInformacaoSistema = new ServicoInformacaoSistema();
            var infoSistema = servicoInformacaoSistema.ConsulteUltimaInformacaoSistema();

            lblVersaoSistema.Text = "Versão " + infoSistema.Versao;
        }

        private void PreenchaCaixaUsuario()
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa();
            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null)
            {
                MessageBoxAkil.Show("Usuário logado não contém caixa");

                return;
            }

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa(false, false);
            var movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

            lblNumeroCaixa.Text = caixa.Id.ToString("000");
            lblOperadorCaixa.Text = caixa.Funcionario.DadosGerais.Razao;
            lblStausCaixa.Text = movimentacaoCaixa != null ? movimentacaoCaixa.Status.Descricao() : "FECHADO";

            if (movimentacaoCaixa == null || movimentacaoCaixa.Status == EnumStatusMovimentacaoCaixa.FECHADO)
            {
                FormCaixaFechadoPdv formCaixaFechadoPdv = new FormCaixaFechadoPdv();
                movimentacaoCaixa = formCaixaFechadoPdv.AbraCaixa();

                if (movimentacaoCaixa == null || movimentacaoCaixa.Status == EnumStatusMovimentacaoCaixa.FECHADO)
                {
                    this.Close();
                }
                else
                {
                    lblStausCaixa.Text = movimentacaoCaixa.Status.Descricao();
                }
            }
        }

        private void AbraFormPdv()
        {
            FormPdv formPdv = null;

            if (_cliente != null)
            {
                formPdv = new FormPdv(_cliente);
            }
            else
            {
                formPdv = new FormPdv();
            }

            formPdv.ShowDialog();

            txtCpfCnpj.Text = string.Empty;
            txtRazaoSocial.Text = string.Empty;
            _clienteEncontrado = false;
            _cliente = null;
        }

        private void PreenchaCLientePadrao()
        {
            ServicoConfiguracoesPdv servicoConfiguracoesPdv = new ServicoConfiguracoesPdv();
            var configuracoesPdv = servicoConfiguracoesPdv.ConsulteUltimaConfiguracaoPdv();

            configuracoesPdv.Cliente.CarregueLazyLoad();
            _clientePadrao = configuracoesPdv.Cliente;
        }

        #endregion

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
