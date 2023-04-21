using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.InventarioServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.TransferenciaServ;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.SubEstoqueServ;

namespace Programax.Easy.View.Telas.Cadastros.Inventarios
{
    public partial class FormTransferenciaPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<SubEstoque> _listaDeTransferencias;
        private SubEstoque _transferenciaSelecionado;

        #endregion

        #region " CONSTRUTOR "

        public FormTransferenciaPesquisa()
        {
            InitializeComponent();
            //this.ActiveControl = txtDataInicio;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcNcms_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcNcms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void txtPesquiseEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public SubEstoque ExibaPesquisaDeInventarios()
        {
            this.ShowDialog();

            return _transferenciaSelecionado;
        }

        private void Pesquise()
        {
            ServicoSubEstoque servicoSubEstoque = new ServicoSubEstoque();

            EnumStatusInventario? statusInventario = (EnumStatusInventario?)cboStatus.EditValue;
            
            _listaDeTransferencias = servicoSubEstoque.ConsulteLista(null,txtDescricao.Text, "A");

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<TransferenciaAuxiliar> listaDeTransferenciasAuxiliares = new List<TransferenciaAuxiliar>();

            foreach (var transferencia in _listaDeTransferencias)
            {
                TransferenciaAuxiliar transferenciaAuxiliar = new TransferenciaAuxiliar();

                transferenciaAuxiliar.Id = transferencia.Id;

                transferenciaAuxiliar.Descricao = transferencia.Descricao.ToString();
                transferenciaAuxiliar.DataCadastro = transferencia.DataCadastro.ToString("dd/MM/yyy");
                transferenciaAuxiliar.Status = transferencia.Ativo;

                listaDeTransferenciasAuxiliares.Add(transferenciaAuxiliar);
            }

            gcContasBancarias.DataSource = listaDeTransferenciasAuxiliares;
            gcContasBancarias.RefreshDataSource();
        }

        private void Selecione()
        {
            _transferenciaSelecionado = null;

            if (_listaDeTransferencias != null && _listaDeTransferencias.Count > 0)
            {
                ServicoSubEstoque servicoSubEstoque = new ServicoSubEstoque();

                _transferenciaSelecionado = servicoSubEstoque.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId).ToInt()));
            }

            this.Close();
        }

       

        #region " CLASSES AUXILIARES "

        private class TransferenciaAuxiliar
        {
            public int Id { get; set; }

            public string DataCadastro { get; set; }

            public string Status { get; set; }

            public string Descricao { get; set; }

        }

        #endregion
    }
    #endregion
}
