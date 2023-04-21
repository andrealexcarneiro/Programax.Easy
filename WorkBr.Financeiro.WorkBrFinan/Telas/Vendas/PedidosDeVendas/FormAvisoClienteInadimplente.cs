using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Vendas.PedidosDeVendas
{
    public partial class FormAvisoClienteInadimplente : FormularioBase
    {
        private static FormAvisoClienteInadimplente _formAvisoClienteInadimplente;
        private int _tempoFechar;

        private int diferencaNoMomento;

        private FormAvisoClienteInadimplente()
        {
            InitializeComponent();
        }

        public static void ExibaAvisoInadimplente(int pessoaId)
        {
            EscondaAvisoInadimplente();

            ServicoContasReceber servicoContasReceber = new ServicoContasReceber();
            
            var possuiTituloAtrasado = servicoContasReceber.PossuiTituloAtrasado(pessoaId);

            if (!possuiTituloAtrasado)
            {
                return;
            }

            if (_formAvisoClienteInadimplente == null || !_formAvisoClienteInadimplente.IsAccessible)
            {
                //**Vamos ver se todas as contas atrasadas são crédito
                Pessoa pessoa = new Pessoa { Id = pessoaId };

                var contasPagarReceber = servicoContasReceber.ConsulteListaAberto(pessoa);

                if (contasPagarReceber != null)
                {
                    if (contasPagarReceber.Count(x => x.DataVencimento < DateTime.Now) > 1)
                    {
                        if (contasPagarReceber.Exists(x => x.FormaPagamento.TipoFormaPagamento != Negocio.Financeiro.Enumeradores.EnumTipoFormaPagamento.CARTAOCREDITO) ||
                            contasPagarReceber.Exists(x => x.FormaPagamento.TipoFormaPagamento != Negocio.Financeiro.Enumeradores.EnumTipoFormaPagamento.CARTAODEBITO))
                        {
                        }
                        else
                            return;
                    }
                    else if (contasPagarReceber.Exists(x => x.FormaPagamento.TipoFormaPagamento == Negocio.Financeiro.Enumeradores.EnumTipoFormaPagamento.CARTAOCREDITO) ||
                             contasPagarReceber.Exists(x => x.FormaPagamento.TipoFormaPagamento == Negocio.Financeiro.Enumeradores.EnumTipoFormaPagamento.CARTAODEBITO))
                    {
                        return;
                    }          
                }

                _formAvisoClienteInadimplente = new FormAvisoClienteInadimplente();
            }

            _formAvisoClienteInadimplente.Show();

            _formAvisoClienteInadimplente._tempoFechar = 15;

            _formAvisoClienteInadimplente.timer.Enabled = true;
            _formAvisoClienteInadimplente.timerFechar.Enabled = true;
        }

        public static void EscondaAvisoInadimplente()
        {
            if (_formAvisoClienteInadimplente != null)
            {
                _formAvisoClienteInadimplente.timer.Enabled = false;
                _formAvisoClienteInadimplente.timerFechar.Enabled = false;

                _formAvisoClienteInadimplente.Close();
                _formAvisoClienteInadimplente = null;
            }
        }

        private void FormAvisoClienteInadimplente_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width, 0);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int posicaoFinal = Screen.PrimaryScreen.Bounds.Width - this.Size.Width;

            if (diferencaNoMomento == this.Width)
            {
                timer.Enabled = false;
                return;
            }

            diferencaNoMomento += 10;

            if (diferencaNoMomento > this.Width)
            {
                diferencaNoMomento = this.Width;
            }

            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - diferencaNoMomento, 0);
        }

        private void timerFechar_Tick(object sender, EventArgs e)
        {
            _tempoFechar--;

            lblTempoFechar.Text = "(" + _tempoFechar.ToString() + ")";

            if (_tempoFechar <= 0)
            {
                EscondaAvisoInadimplente();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            EscondaAvisoInadimplente();
        }
    }
}
