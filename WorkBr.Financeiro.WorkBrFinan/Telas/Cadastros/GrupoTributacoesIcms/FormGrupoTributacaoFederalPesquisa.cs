using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.GrupoTributacaoFederalServ;
using Programax.Easy.Servico.Cadastros.GrupoTributacaoIcmsServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.View.Telas.Cadastros.GrupoTributacoesIcms
{
    public partial class FormGrupoTributacaoFederalPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private GrupoTributacaoFederal _grupoTributacaoFederal;
        private List<GrupoTributacaoFederal> _listaDeGrupoTributacaoFederal;

        #endregion

        #region " CONSTRUTOR "

        public FormGrupoTributacaoFederalPesquisa()
        {
            InitializeComponent();

            _listaDeGrupoTributacaoFederal = new List<GrupoTributacaoFederal>();
            
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise();
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

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void cboStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        public GrupoTributacaoFederal ExibaPesquisaDeGrupoDeTributacaoFederal()
        {
            this.ShowDialog();

            return _grupoTributacaoFederal;
        }

        private void Pesquise()
        {
            ServicoGrupoTributacaoFederal servicoGrupoTributacaoFederal = new ServicoGrupoTributacaoFederal();

            _listaDeGrupoTributacaoFederal = servicoGrupoTributacaoFederal.ConsulteLista(txtDescricao.Text);

            preencherGrid();
        }

        private void Selecione()
        {
            _grupoTributacaoFederal = null;

            if (_listaDeGrupoTributacaoFederal != null && _listaDeGrupoTributacaoFederal.Count > 0)
            {
                ServicoGrupoTributacaoFederal servicoGrupoTributacaoFederal = new ServicoGrupoTributacaoFederal();

                _grupoTributacaoFederal = servicoGrupoTributacaoFederal.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        private void preencherGrid()
        {
            List<GrupoTributacaoIcmsGrid> listaDeNcmsGrid = new List<GrupoTributacaoIcmsGrid>();

            foreach (var Grupo in _listaDeGrupoTributacaoFederal)
            {
                GrupoTributacaoIcmsGrid ncmGrid = new GrupoTributacaoIcmsGrid();

                ncmGrid.Id = Grupo.Id;
                ncmGrid.Codigo = Grupo.Id.ToString();
                ncmGrid.Natureza = Grupo.NaturezaProduto == EnumNaturezaProduto.TERCEIROS ? "TERCEIROS" : "FABRICAÇÃO PROPRIA";
                ncmGrid.Descricao = Grupo.Descricao;

                listaDeNcmsGrid.Add(ncmGrid);
            }

            gcGrupoTributacaoFederal.DataSource = listaDeNcmsGrid;
            gcGrupoTributacaoFederal.RefreshDataSource();
        }
        
        #endregion

        #region " CLASSE AUXILIAR "

        private class GrupoTributacaoIcmsGrid
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Codigo { get; set; }

            public string Natureza { get; set; }
        }

        #endregion
    }
}
