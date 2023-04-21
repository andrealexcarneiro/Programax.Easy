using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.ConfiguracoesSistema.InformacaoSistemaServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormAgradecimentoPdv : FormularioBase
    {
        #region " CONSTRUTOR "

        public FormAgradecimentoPdv(double valorTotalVenda, Pessoa vendedor, Pessoa cliente)
        {
            InitializeComponent();

            PreenchaCaixaUsuario();
            PreenchaDadosEmpresa();

            PreenchaVendedor(vendedor);
            PreenchaCliente(cliente);

            lblValorTotalVenda.Text = "Valor Total da Compra: R$ " + valorTotalVenda.ToString("#,###,###,##0.00");
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormAgradecimentoPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }
        }

        private void tmrHoraAtual_Tick(object sender, EventArgs e)
        {
            lblDataHoraAtual.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }

        private void PreenchaVendedor(Pessoa vendedor)
        {
            if (vendedor != null)
            {
                lblVendedor.Text = vendedor.Id + " - " + vendedor.DadosGerais.Razao;
            }
            else
            {
                lblVendedor.Text = string.Empty;
            }
        }

        private void PreenchaCliente(Pessoa cliente)
        {
            if (cliente != null)
            {
                lblCliente.Text = cliente.DadosGerais.CpfCnpj + " - " + cliente.DadosGerais.Razao;
            }
            else
            {
                lblCliente.Text = string.Empty;
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

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
        }

        private void PreenchaDadosEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            lblEmpresa.Text = empresa.DadosEmpresa.RazaoSocial;

            ServicoInformacaoSistema servicoInformacaoSistema = new ServicoInformacaoSistema();
            var infoSistema = servicoInformacaoSistema.ConsulteUltimaInformacaoSistema();

            lblVersaoSistema.Text = "Versão " + infoSistema.Versao;
        }

        #endregion
    }
}
