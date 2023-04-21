using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Negocio;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.InformacaoSistemaServ;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormCaixaFechadoPdv : FormularioBase
    {
        private MovimentacaoCaixa _movimentacaoCaixa;

        public FormCaixaFechadoPdv()
        {
            InitializeComponent();

            PreenchaCaixaUsuario();
            PreenchaDadosEmpresa();
        }

        public MovimentacaoCaixa AbraCaixa()
        {
            this.ShowDialog();

            return _movimentacaoCaixa;
        }

        private void FormCaixaFechadoPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F2)
            {
                ServicoCaixa servicoCaixa = new ServicoCaixa();

                var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

                FormAberturaCaixaPdv formAberturaCaixaPdv = new FormAberturaCaixaPdv();
                _movimentacaoCaixa = formAberturaCaixaPdv.AbraMovimentacaoCaixa(caixa);

                if (_movimentacaoCaixa != null)
                {
                    this.Close();
                }
            }
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

        private void PreenchaCaixaUsuario()
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa();
            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null)
            {
                MessageBoxAkil.Show("Usuário logado não contém caixa");

                return;
            }

            lblNumeroCaixa.Text = caixa.Id.ToString("000");
            lblOperadorCaixa.Text = caixa.Funcionario.DadosGerais.Razao;
            lblStausCaixa.Text = "FECHADO";
        }

        private void tmrHoraAtual_Tick(object sender, EventArgs e)
        {
            lblDataHoraAtual.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }
    }
}
