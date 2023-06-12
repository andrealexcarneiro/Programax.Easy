using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.RamoAtividadeServ;
using Programax.Easy.Report.RelatoriosDevExpress.Cadastros;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.FabricanteServ;
using Programax.Easy.Servico.Cadastros.TamanhoServ;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.Servico.Cadastros.UnidadeMedidaServ;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioProdutos : FormularioBase
    {
        private object funcao;
        #region " CONSTRUTOR "

        public FormRelatorioProdutos()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboCategorias();
            PreenchaCboFabricantes();
            PreenchaCboTamanhos();
            PreenchaCboMarcas();

            this.ActiveControl = chkOrdenarPorCodigo;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;
           
                DateTime dataInicial = txtDataInicialPeriodo.Text.ToDate();
                DateTime dataFinal = txtDataFinalPeriodo.Text.ToDate();
            //if (rdnFiscal.Checked)
            //{
            //    string inconsistencias = string.Empty;

            //    inconsistencias += dataInicial == DateTime.MinValue ? "Informe a data inicial do período.\n\n" : string.Empty;
            //    inconsistencias += dataFinal == DateTime.MinValue ? "Informe a data final do período.\n\n" : string.Empty;

            //    if (!string.IsNullOrEmpty(inconsistencias))
            //    {
            //        MessageBox.Show(inconsistencias);
            //        this.Cursor = Cursors.Default;
            //        return;
            //    }
            //}
            Categoria categoria = cboCategorias.EditValue != null ? new Categoria { Id = cboCategorias.EditValue.ToInt() } : null;
            Grupo grupo = cboGrupos.EditValue != null ? new Grupo { Id = cboGrupos.EditValue.ToInt() } : null;
            SubGrupo subgrupo = cboSubGrupos.EditValue != null ? new SubGrupo { Id = cboSubGrupos.EditValue.ToInt() } : null;
            Marca marca = cboMarcas.EditValue != null ? new Marca { Id = cboMarcas.EditValue.ToInt() } : null;
            Fabricante fabricante = cboFabricantes.EditValue != null ? new Fabricante { Id = cboFabricantes.EditValue.ToInt() } : null;
            Tamanho tamanho = cboTamanhos.EditValue != null ? new Tamanho { Id = cboTamanhos.EditValue.ToInt() } : null;

            bool mostrarNcms = chkMostrarNcms.Checked;
            bool itensComEstoqueMinimo = rdbItensComEstoqueMinimo.Checked;
            int? ddvAbaixoDe = txtDdvAbaixeDe.Text.ToIntNullabel();
            string status = cboStatus.EditValue.ToString();
            int? estoqueMaiorQue = txtEstoqueMaiorQue.Text != string.Empty? txtEstoqueMaiorQue.Text.ToIntNullabel():null;

            EnumOrdenacaoPesquisaProduto ordenacaoPesquisa = (EnumOrdenacaoPesquisaProduto)pnlOrdenacao.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            RelatorioProdutos relatorioProdutos = new RelatorioProdutos(categoria, grupo, subgrupo, marca, fabricante, tamanho, itensComEstoqueMinimo, mostrarNcms,ddvAbaixoDe, ordenacaoPesquisa, status, rdnFiscal.Checked, dataInicial, dataFinal, estoqueMaiorQue);
            
            TratamentosDeTela.ExibirRelatorio(relatorioProdutos);

            this.Cursor = Cursors.Default;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbOrdemListaCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

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

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " PREENCHIMENTO DE CAMPOS "

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

            cboGrupos.EditValue = null;

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

        private void PreenchaCboFabricantes()
        {
            ServicoFabricante servicoFabricante = new ServicoFabricante();

            var fabricantes = servicoFabricante.ConsulteListaAtiva();

            fabricantes.Insert(0, null);

            cboFabricantes.Properties.DataSource = fabricantes;
            cboFabricantes.Properties.DisplayMember = "Descricao";
            cboFabricantes.Properties.ValueMember = "Id";
        }

        private void PreenchaCboTamanhos()
        {
            ServicoTamanho servicoTamanho = new ServicoTamanho();

            var tamanhos = servicoTamanho.ConsulteListaAtiva();

            tamanhos.Insert(0, null);

            cboTamanhos.Properties.DataSource = tamanhos;
            cboTamanhos.Properties.DisplayMember = "Descricao";
            cboTamanhos.Properties.ValueMember = "Id";
        }

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            ObjetoParaComboBox objetoComboBoxTodos = new ObjetoParaComboBox();
            objetoComboBoxTodos.Valor = "T";
            objetoComboBoxTodos.Descricao = "Todos";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);
            listaDeItensParaOComboBox.Add(objetoComboBoxTodos);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = "A";
        }

        #endregion

        private void rdbItensDDVAbaixoDe_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbItensDDVAbaixoDe.Checked)
            {
                txtDdvAbaixeDe.Enabled = true;
            }
            else
            {
                txtDdvAbaixeDe.Enabled = false;
            }
        }

        #endregion

        private void rdnFiscal_CheckedChanged(object sender, EventArgs e)
        {
            if(rdnFiscal.Checked)
            {
                txtDataInicialPeriodo.Enabled = true;
                txtDataFinalPeriodo.Enabled = true;
            }
            else
            {
                txtDataInicialPeriodo.Enabled = false;
                txtDataFinalPeriodo.Enabled = false;
            }
        }
    }
}
