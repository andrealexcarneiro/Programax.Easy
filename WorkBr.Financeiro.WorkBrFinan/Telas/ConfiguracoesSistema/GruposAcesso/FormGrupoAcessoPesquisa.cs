using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.GrupoAcessoServ;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.GruposAcesso
{
    public partial class FormGruposAcessoPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<GrupoAcesso> _listaDeGruposAcesso;
        private GrupoAcesso _GruposAcessoSelecionada;

        #endregion

        #region " CONSTRUTOR "

        public FormGruposAcessoPesquisa()
        {
            InitializeComponent();
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

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            Pesquise();
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

        #endregion

        #region " MÉTODOS AUXILIARES "

        public GrupoAcesso ExibaPesquisaDeGruposAcesso()
        {
            this.ShowDialog();

            return _GruposAcessoSelecionada;
        }

        private void Pesquise()
        {
            ServicoGrupoAcesso servicoGrupoAcesso = new ServicoGrupoAcesso();

            _listaDeGruposAcesso = servicoGrupoAcesso.ConsulteLista(txtDescricao.Text);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<GruposAcessoAuxiliar> listaDeGruposAcessoAuxiliares = new List<GruposAcessoAuxiliar>();

            foreach (var grupoAcesso in _listaDeGruposAcesso)
            {
                GruposAcessoAuxiliar grupoAcessosAuxiliar = new GruposAcessoAuxiliar();

                grupoAcessosAuxiliar.Descricao = grupoAcesso.Descricao;
                grupoAcessosAuxiliar.Id = grupoAcesso.Id;

                listaDeGruposAcessoAuxiliares.Add(grupoAcessosAuxiliar);
            }

            gcGruposAcesso.DataSource = listaDeGruposAcessoAuxiliares;
            gcGruposAcesso.RefreshDataSource();
        }

        private void Selecione()
        {
            _GruposAcessoSelecionada = null;

            if (_listaDeGruposAcesso != null && _listaDeGruposAcesso.Count > 0)
            {
                ServicoGrupoAcesso servicoGruposAcesso = new ServicoGrupoAcesso();

                _GruposAcessoSelecionada = servicoGruposAcesso.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #endregion

        #region " CLASSE AUXILIAR "

        private class GruposAcessoAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }
        }

        #endregion
    }
}
