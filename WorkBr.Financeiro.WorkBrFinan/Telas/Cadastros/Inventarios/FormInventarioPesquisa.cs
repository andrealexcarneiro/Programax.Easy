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

namespace Programax.Easy.View.Telas.Cadastros.Inventarios
{
    public partial class FormInventarioPesquisa : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Inventario> _listaDeInventarios;
        private Inventario _inventarioSelecionado;

        #endregion

        #region " CONSTRUTOR "

        public FormInventarioPesquisa()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboMarcas();
            PreenchaCboCategorias();
            PreenchaCboContagens();

            this.ActiveControl = txtDataInicio;
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

        private void cboCategorias_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboGrupos();
        }

        private void cboGrupos_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboSubGrupos();
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

        public Inventario ExibaPesquisaDeInventarios()
        {
            this.ShowDialog();

            return _inventarioSelecionado;
        }

        private void Pesquise()
        {
            ServicoInventario servicoInventario = new ServicoInventario();

            DateTime? dataInicio = txtDataInicio.Text.ToDateNullabel();

            EnumStatusInventario? statusInventario = (EnumStatusInventario?)cboStatus.EditValue;
            int? contagem = cboNumeroContagem.EditValue.ToIntNullabel();

            Marca marca = cboMarcas.EditValue != null ? new Marca { Id = cboMarcas.EditValue.ToInt() } : null;
            Categoria categoria = cboCategorias.EditValue != null ? new Categoria { Id = cboCategorias.EditValue.ToInt() } : null;
            Grupo grupo = cboGrupos.EditValue != null ? new Grupo { Id = cboGrupos.EditValue.ToInt() } : null;
            SubGrupo subGrupo = cboSubGrupos.EditValue != null ? new SubGrupo { Id = cboSubGrupos.EditValue.ToInt() } : null;

            _listaDeInventarios = servicoInventario.ConsulteLista(dataInicio, statusInventario, contagem, marca, categoria, grupo, subGrupo);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<InventarioAuxiliar> listaDeContasBancariasAuxiliares = new List<InventarioAuxiliar>();

            foreach (var inventario in _listaDeInventarios)
            {
                InventarioAuxiliar contaBancariaAuxiliar = new InventarioAuxiliar();

                contaBancariaAuxiliar.Id = inventario.Id;

                contaBancariaAuxiliar.Marca = inventario.Marca != null ? inventario.Marca.Descricao : string.Empty;
                contaBancariaAuxiliar.Categoria = inventario.Categoria != null ? inventario.Categoria.Descricao :  string.Empty;
                contaBancariaAuxiliar.Grupo = inventario.Grupo != null ? inventario.Grupo.Descricao : string.Empty;
                contaBancariaAuxiliar.SubGrupo = inventario.SubGrupo != null ? inventario.SubGrupo.Descricao : string.Empty;

                contaBancariaAuxiliar.DataInicio = inventario.DataInicio.ToString("dd/MM/yyy");
                contaBancariaAuxiliar.Status = inventario.Status.Descricao();
                contaBancariaAuxiliar.Contagem = inventario.ContagemAtual.ToString() + "ª Cont.";

                listaDeContasBancariasAuxiliares.Add(contaBancariaAuxiliar);
            }

            gcContasBancarias.DataSource = listaDeContasBancariasAuxiliares;
            gcContasBancarias.RefreshDataSource();
        }

        private void Selecione()
        {
            _inventarioSelecionado = null;

            if (_listaDeInventarios != null && _listaDeInventarios.Count > 0)
            {
                ServicoInventario servicoInventario = new ServicoInventario();

                _inventarioSelecionado = servicoInventario.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
            }

            this.Close();
        }

        #region " PREENCHIMENTO DE CBOS "

        private void PreenchaOStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusInventario>();

            lista.Insert(0, null);

            cboStatus.Properties.DataSource = lista;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";
        }

        private void PreenchaCboContagens()
        {
            ObjetoParaComboBox objetoComboBoxPrimeiraContagem = new ObjetoParaComboBox();
            objetoComboBoxPrimeiraContagem.Valor = 1;
            objetoComboBoxPrimeiraContagem.Descricao = "PRIMEIRA CONTAGEM";

            ObjetoParaComboBox objetoComboBoxSegundaContagem = new ObjetoParaComboBox();
            objetoComboBoxSegundaContagem.Valor = 2;
            objetoComboBoxSegundaContagem.Descricao = "SEGUNDA CONTAGEM";

            ObjetoParaComboBox objetoComboBoxTerceiraContagem = new ObjetoParaComboBox();
            objetoComboBoxTerceiraContagem.Valor = 3;
            objetoComboBoxTerceiraContagem.Descricao = "TERCEIRA CONTAGEM";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(null);
            listaDeItensParaOComboBox.Add(objetoComboBoxPrimeiraContagem);
            listaDeItensParaOComboBox.Add(objetoComboBoxSegundaContagem);
            listaDeItensParaOComboBox.Add(objetoComboBoxTerceiraContagem);

            cboNumeroContagem.Properties.DataSource = listaDeItensParaOComboBox;
            cboNumeroContagem.Properties.DisplayMember = "Descricao";
            cboNumeroContagem.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboCategorias()
        {
            ServicoCategoria servicoCategoria = new ServicoCategoria();

            var categorias = servicoCategoria.ConsulteListaAtiva();

            categorias.Insert(0, null);

            cboCategorias.Properties.DataSource = categorias;
            cboCategorias.Properties.DisplayMember = "Descricao";
            cboCategorias.Properties.ValueMember = "Id";
        }

        private void PreenchaCboGrupos()
        {
            ServicoGrupo servicoGrupo = new ServicoGrupo();

            var grupos = servicoGrupo.ConsulteListaAtivos(new Categoria { Id = cboCategorias.EditValue.ToInt() });

            grupos.Insert(0, null);

            cboGrupos.Properties.DataSource = grupos;
            cboGrupos.Properties.DisplayMember = "Descricao";
            cboGrupos.Properties.ValueMember = "Id";

            if (string.IsNullOrEmpty(cboGrupos.Text))
            {
                cboGrupos.EditValue = null;
            }

            PreenchaCboSubGrupos();
        }

        private void PreenchaCboSubGrupos()
        {
            Grupo grupo = new Grupo();

            if (cboGrupos.EditValue != null)
            {
                grupo.Id = cboGrupos.EditValue.ToInt();
            }

            ServicoSubGrupo servicoSubGrupo = new ServicoSubGrupo();

            var grupos = servicoSubGrupo.ConsulteListaAtiva(grupo);

            grupos.Insert(0, null);

            cboSubGrupos.Properties.DataSource = grupos;
            cboSubGrupos.Properties.DisplayMember = "Descricao";
            cboSubGrupos.Properties.ValueMember = "Id";

            if (string.IsNullOrEmpty(cboSubGrupos.Text))
            {
                cboSubGrupos.EditValue = null;
            }
        }

        private void PreenchaCboMarcas()
        {
            ServicoMarca servicoMarcas = new ServicoMarca();

            var marcas = servicoMarcas.ConsulteListaAtiva();

            marcas.Insert(0, null);

            cboMarcas.Properties.DataSource = marcas;
            cboMarcas.Properties.DisplayMember = "Descricao";
            cboMarcas.Properties.ValueMember = "Id";
        }

        #endregion

        #endregion

        #region " CLASSES AUXILIARES "

        private class InventarioAuxiliar
        {
            public int Id { get; set; }

            public string DataInicio { get; set; }

            public string Status { get; set; }

            public string Contagem { get; set; }

            public string Marca { get; set; }

            public string Categoria { get; set; }

            public string Grupo { get; set; }

            public string SubGrupo { get; set; }
        }

        #endregion
    }
}
