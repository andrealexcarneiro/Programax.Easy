using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco
{
    public partial class FormSelecaoBancos : Form
    {
        public List<BancoParaMovimento> _listaBancos = new List<BancoParaMovimento>();

        public FormSelecaoBancos()
        {
            InitializeComponent();

            PreencheListaBancos();

            lstBancos.UnSelectAll();
        }

        private void FormSelecaoBancos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F2)
            {
                LimparSelecao();
            }
            else if (e.KeyCode == Keys.F3)
            {
                FinalizarSelecao();
            }
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparSelecao();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            FinalizarSelecao();
        }

        private void LimparSelecao()
        {
            lstBancos.Items.Clear();
            PreencheListaBancos();
            lstBancos.UnSelectAll();
        }

        private void FinalizarSelecao()
        {
            _listaBancos = new List<BancoParaMovimento>();

            for (int i = 0; i < lstBancos.SelectedItems.Count; i++)
            {
                string textoBanco = lstBancos.SelectedItems[i].ToString();
                string[] codBanco;

                codBanco = textoBanco.Split('-');

                BancoParaMovimento banco = lstBancos.SelectedItem != null ? new BancoParaMovimento { Id = codBanco[0].Trim().ToInt() } : null;

                _listaBancos.Add(banco);
            }

            this.Close();
        }

        private void PreencheListaBancos()
        {
            ServicoBancoParaMovimento servicoBanco = new ServicoBancoParaMovimento();
            var lista = servicoBanco.ConsulteLista(string.Empty,"A");

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(banco =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = banco.Id + " - " + banco.NomeBanco, Valor = banco.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            foreach (var itens in listaObjetoValor)
            {
                int i = 0;
                lstBancos.Items.Add(itens.Descricao);
                i++;
            }
        }
    }
}
