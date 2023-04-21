using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Report.Relatorios;
using Programax.Easy.Report.Relatorios.Estoque;
using Microsoft.Reporting.WinForms;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class ContainerRelatorio : Form
    {
        public ContainerRelatorio()
        {
            InitializeComponent();
        }

        public void ExibaRelatorio<TRelatorio, TObjetoDeNegocio>(TRelatorio relatorioPadrao, List<TObjetoDeNegocio> listaDeItensRelatorio)
            where TRelatorio : RelatorioPadrao<TObjetoDeNegocio>, new()
        {
            relatorioPadrao.ConstruaRelatorio(reportViewer1.LocalReport, listaDeItensRelatorio);

            reportViewer1.RefreshReport();

            this.ShowDialog();
        }
    }
}
