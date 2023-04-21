using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormPesquisaPedidoVendaPdv : FormularioBase
    {
        private PedidoDeVenda _pedidoDeVenda;

        public FormPesquisaPedidoVendaPdv()
        {
            InitializeComponent();
        }

        private void FormPesquisaPedidoVendaPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public PedidoDeVenda PesquisePedidoDeVenda()
        {
            this.AbrirTelaModal(true);

            return _pedidoDeVenda;
        }
    }
}
