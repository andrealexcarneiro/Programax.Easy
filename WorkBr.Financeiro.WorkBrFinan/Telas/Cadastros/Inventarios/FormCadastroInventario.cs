using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.InventarioServ;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Telas.Relatorios;
using Programax.Easy.Report.Relatorios.Estoque;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using Programax.Easy.Servico.Cadastros.TransferenciaServ;

namespace Programax.Easy.View.Telas.Cadastros.Inventarios
{
    public partial class FormCadastroInventario : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _numeroContagem;

        private List<ItemInventario> _listaItemInventario;

        private DateTime? _dataInicioContagemUm;
        private DateTime? _dataInicioContagemDois;
        private DateTime? _dataInicioContagemTres;

        private bool _itensBloqueados;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroInventario()
        {
            InitializeComponent();

            _listaItemInventario = new List<ItemInventario>();

            PreenchaCboMarcas();
            PreenchaCboCategorias();
            PreenchaCboTiposInventarios();
            PreenchaCboModalidades();
            PreenchaCboFiltrosSituacaoSaldo();

            BloquearLancamentoContagem();

            this.NomeDaTela = "Inventário";

            ZereNumeroContagem();

            txtDataInicio.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtDataFinal.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            txtStatus.Text = EnumStatusInventario.ABERTO.Descricao();

            chkUtilizaSaldoPrimeiraContagem.Checked = false;
            chkUtilizaSaldoPrimeiraContagem.Checked = true;

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        #region " EVENTOS BARRAS DE BOTÕES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnPrimeiraContagem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja emitir a 1ª Contagem?", "Emitir primeira contagem", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            if(VerificaSeExisteInventarioAberto())
            {
                MessageBox.Show("Existe(m) inventario(s) aberto(s). Consolide para continuar.", "Realizando Inventário", MessageBoxButtons.OK);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            Inventario inventario = RetorneInventarioEmEdicao();

            Action actionSalvar = () =>
            {
                ServicoInventario servicoInventario = new ServicoInventario();

                servicoInventario.EmitaPrimeiraContagem(inventario);

                servicoInventario = new ServicoInventario();

                var inventarioDaBase = servicoInventario.Consulte(inventario.Id);

                EditeInventario(inventarioDaBase);

                EmitirRelatorioInventario(inventarioDaBase);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, "Emissão da 1ª contagem efetuada com suceso.", "1ª Contagem");

            this.Cursor = Cursors.Default;
        }
        
        private bool VerificaSeExisteInventarioAberto()
        {
            ServicoInventario servicoInventarioVericacao = new ServicoInventario();
            var listaInventario = servicoInventarioVericacao.ConsulteLista(DateTime.Now.AddYears(-1), EnumStatusInventario.ABERTO,null,null,null,null,null);

            if (listaInventario != null)
                return true;

            return false;
        }

        private void btnSegundaContagem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja emitir a 2ª Contagem?", "Emitir segunda contagem", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            Inventario inventario = RetorneInventarioEmEdicao();

            Action actionSalvar = () =>
            {
                ServicoInventario servicoInventario = new ServicoInventario();

                servicoInventario.EmitaSegundaContagem(inventario);

                servicoInventario = new ServicoInventario();

                var inventarioDaBase = servicoInventario.Consulte(inventario.Id);

                EditeInventario(inventarioDaBase);

                EmitirRelatorioInventario(inventarioDaBase);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, "Emissão da 2ª contagem efetuada com suceso.", "2ª Contagem");

            this.Cursor = Cursors.Default;
        }

        private void btnTerceiraContagem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja emitir a 3ª Contagem?", "Emitir terceira contagem", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            Inventario inventario = RetorneInventarioEmEdicao();

            Action actionSalvar = () =>
            {
                ServicoInventario servicoInventario = new ServicoInventario();

                servicoInventario.EmitaTerceiraContagem(inventario);

                servicoInventario = new ServicoInventario();

                var inventarioDaBase = servicoInventario.Consulte(inventario.Id);

                EditeInventario(inventarioDaBase);

                EmitirRelatorioInventario(inventarioDaBase);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, "Emissão da 3ª contagem efetuada com suceso.", "3ª Contagem");

            this.Cursor = Cursors.Default;
        }

        private void btnConsolidarInventario_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja consolidar o inventário?", "Consolidar o inventário", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            Inventario inventario = RetorneInventarioEmEdicao();

            Action actionConslidarInventario = () =>
            {
                ServicoInventario servicoInventario = new ServicoInventario();

                servicoInventario.ConsolideInventario(inventario);

                LimpeFormulario();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionConslidarInventario, "Inventário consolidado com sucesso.", "Inventário consolidado");

            this.Cursor = Cursors.Default;
        }

        private void btnItensSemContagens_Click(object sender, EventArgs e)
        {
            List<Produto> listaObjetos = new List<Produto>();

            OrdeneItensInventario();

            foreach (var item in _listaItemInventario)
            {
                if (item.QuantidadeContagemUm == null && _numeroContagem == 1)
                {
                    listaObjetos.Add(item.Produto);
                }

                else if (item.QuantidadeContagemDois == null && _numeroContagem == 2)
                {
                    listaObjetos.Add(item.Produto);
                }

                else if (item.QuantidadeContagemTres == null && _numeroContagem == 3)
                {
                    listaObjetos.Add(item.Produto);
                }
            }

            RelatorioInventario relatorioInventario = new RelatorioInventario();

            var containerRelatorio = new ContainerRelatorio();
            containerRelatorio.ExibaRelatorio(relatorioInventario, listaObjetos);
        }

        #endregion

        #region " EVENTOS CAPA "

        private void cboCategorias_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboGrupos();
        }

        private void cboGrupos_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboSubGrupos();
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                ServicoInventario servicoInventario = new ServicoInventario();
                var inventario = servicoInventario.Consulte(txtId.Text.ToInt());

                chkUtilizaSaldoPrimeiraContagem.Checked = false;
                EditeInventario(inventario);
            }
        }

        private void chkZerarEstoqueAoGerarContagem_CheckedChanged(object sender, EventArgs e)
        {
            chkUtilizaSaldoPrimeiraContagem.Checked = false;
        }

        private void chkUtilizaSaldoPrimeiraContagem_CheckedChanged(object sender, EventArgs e)
        {
            if (_numeroContagem <= 1)
            {
                if (chkUtilizaSaldoPrimeiraContagem.Checked)
                {
                    btnPrimeiraContagem.Visible = false;
                    btnSegundaContagem.Visible = true;
                }
                else
                {
                    btnPrimeiraContagem.Visible = true;
                    btnSegundaContagem.Visible = false;
                }
            }
        }

        private void btnPesquisaInventario_Click(object sender, EventArgs e)
        {
            FormInventarioPesquisa formInventarioPesquisa = new FormInventarioPesquisa();

            var inventario = formInventarioPesquisa.ExibaPesquisaDeInventarios();

            if (inventario != null)
            {
                chkUtilizaSaldoPrimeiraContagem.Checked = false;
                EditeInventario(inventario);
            }
        }

        #endregion

        #region " EVENTOS LANÇAMENTO CONTAGEM "

        private void txtIdProduto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdProduto.Text))
            {
                PreenchaCamposDoProduto(null, casoNuloFocusNoIdProduto: false);
            }
            else
            {
                var itemInventario = _listaItemInventario.FirstOrDefault(item => item.Produto.Id == txtIdProduto.Text.ToInt());
                PreenchaCamposDoProduto(itemInventario);
            }
        }

        private void txtCodigoDeBarrasProduto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoDeBarrasProduto.Text))
            {
                var itemInventario = _listaItemInventario.FirstOrDefault(item => item.Produto.DadosGerais.CodigoDeBarras == txtCodigoDeBarrasProduto.Text);

                PreenchaCamposDoProduto(itemInventario);
            }
        }

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            AtualizeContagem();
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeCamposLancamentoProduto();
        }

        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            var item = _listaItemInventario.FirstOrDefault(x => x.Produto.Id == gridColunaProdutoId.View.GetFocusedRowCellValue(gridColunaProdutoId).ToInt());

            PreenchaCamposDoProduto(item);
        }

        private void txtQuantidadeContada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AtualizeContagem();
            }
        }

        #endregion

        #region " EVENTOS COMUNS "

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        #endregion

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " AÇÕES BOTÕES BARRA DE MENU "

        private void ZereNumeroContagem()
        {
            _numeroContagem = 0;

            btnPrimeiraContagem.Visible = true;
            btnSegundaContagem.Visible = false;
            btnTerceiraContagem.Visible = false;

            btnItensSemContagens.Visible = false;
        }

        private void AltereParaPrimeiraContagem()
        {
            _numeroContagem = 1;

            btnPrimeiraContagem.Visible = false;
            btnSegundaContagem.Visible = true;
            btnTerceiraContagem.Visible = false;

            btnItensSemContagens.Visible = true;

            btnConsolidarInventario.Visible = false;
            btnConsolidarInventario.Visible = true;
        }

        private void AltereParaSegundaContagem()
        {
            _numeroContagem = 2;

            btnPrimeiraContagem.Visible = false;
            btnSegundaContagem.Visible = false;
            btnTerceiraContagem.Visible = true;

            btnItensSemContagens.Visible = true;

            btnConsolidarInventario.Visible = false;
            btnConsolidarInventario.Visible = true;
        }

        private void AltereParaTerceiraContagem()
        {
            _numeroContagem = 3;

            btnPrimeiraContagem.Visible = false;
            btnSegundaContagem.Visible = false;
            btnTerceiraContagem.Visible = false;

            btnItensSemContagens.Visible = true;

            btnConsolidarInventario.Visible = true;
        }

        private void EmitirRelatorioInventario(Inventario inventario)
        {
            OrdeneItensInventario();

            List<Produto> listaObjetos = new List<Produto>();

            foreach (var item in inventario.ListaDeItens)
            {
                listaObjetos.Add(item.Produto);
            }

            RelatorioInventario relatorioInventario = new RelatorioInventario();

            //var containerRelatorio = new ContainerRelatorio();
            //containerRelatorio.ExibaRelatorio(relatorioInventario, listaObjetos);
            this.Cursor = Cursors.WaitCursor;

           // _listaDeRoteiros.CarregueLazyLoad();

            RelatorioListaInventario relatorio = new RelatorioListaInventario(inventario.Id);
            relatorio.GereRelatorio();

            using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
            {
                // Invoke the Ribbon Print Preview form modally, 
                // and load the report document into it.
                printTool.ShowRibbonPreviewDialog();

                // Invoke the Ribbon Print Preview form
                // with the specified look and feel setting.
                printTool.ShowRibbonPreview(UserLookAndFeel.Default);
            }
            this.Cursor = Cursors.Default;
        }

        private void OrdeneItensInventario()
        {
            if (rdbOrdemListaCodigo.Checked)
            {
                _listaItemInventario = _listaItemInventario.OrderBy(x => x.Produto.Id).ToList();
            }
            else if (rdbOrdemListaDescricao.Checked)
            {
                _listaItemInventario = _listaItemInventario.OrderBy(x => x.Produto.DadosGerais.Descricao).ToList();
            }
            //else if (rdbOrdemListaCategoria.Checked)
            //{
            //    _listaItemInventario = _listaItemInventario.OrderBy(x => x.Produto.Principal.Categoria != null ? x.Produto.Principal.Categoria.Descricao : string.Empty).ToList();
            //}
            else if (rdbOrdemListaMarca.Checked)
            {
                _listaItemInventario = _listaItemInventario.OrderBy(x => x.Produto.Principal.Fabricante != null ? x.Produto.Principal.Fabricante.Descricao : string.Empty).ToList();
            }
        }

        #endregion

        #region " PREENCHIMENTO DE CBOS "

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

        private void PreenchaCboTiposInventarios()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoInventario>();

            lista.Insert(0, null);

            cboTiposInventarios.Properties.DataSource = lista;
            cboTiposInventarios.Properties.DisplayMember = "Descricao";
            cboTiposInventarios.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboModalidades()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModalidadeInventario>();

            lista.Insert(0, null);

            cboModalidades.Properties.DataSource = lista;
            cboModalidades.Properties.DisplayMember = "Descricao";
            cboModalidades.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboFiltrosSituacaoSaldo()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumFiltroSituacao>();

            lista.Insert(0, null);

            cboFiltroDeSituacaoDeSaldo.Properties.DataSource = lista;
            cboFiltroDeSituacaoDeSaldo.Properties.DisplayMember = "Descricao";
            cboFiltroDeSituacaoDeSaldo.Properties.ValueMember = "Valor";
        }

        #endregion

        #region " LIMPAR, EDITAR E RETORNAR INVENTARIO EM EDIÇÃO "

        private void LimpeFormulario()
        {
            EditeInventario(null);
        }

        private void EditeInventario(Inventario inventario)
        {
            if (inventario != null)
            {
                txtId.Text = inventario.Id.ToString();

                if (inventario.ContagemAtual == 1)
                {   
                    AltereParaPrimeiraContagem();
                }
                else if (inventario.ContagemAtual == 2)
                {
                    AltereParaSegundaContagem();
                }
                else
                {
                    AltereParaTerceiraContagem();
                }

                _dataInicioContagemUm = inventario.DataInicioPrimeiraContagem;
                _dataInicioContagemDois = inventario.DataInicioSegundaContagem;
                _dataInicioContagemTres = inventario.DataInicioTerceiraContagem;

                _listaItemInventario = inventario.ListaDeItens.ToList();

                cboTiposInventarios.EditValue = inventario.TipoInventario;
                cboModalidades.EditValue = inventario.Modalidade;

                txtDataInicio.Text = inventario.DataInicio.ToString("dd/MM/yyyy");
                txtDataFinal.Text = inventario.DataFinal.ToString("dd/MM/yyyy");

                cboMarcas.EditValue = inventario.Marca != null ? (int?)inventario.Marca.Id : null;
                cboCategorias.EditValue = inventario.Categoria != null ? (int?)inventario.Categoria.Id : null;
                cboGrupos.EditValue = inventario.Grupo != null ? (int?)inventario.Grupo.Id : null;
                cboSubGrupos.EditValue = inventario.SubGrupo != null ? (int?)inventario.SubGrupo.Id : null;

                chkBloquearProdutosMovimentacao.Checked = inventario.BloquearProdutosMovimentacao;
                chkUtilizaSaldoPrimeiraContagem.Checked = inventario.UtilizarSaldoPrimeiraContagem;

                cboFiltroDeSituacaoDeSaldo.EditValue = inventario.FiltroSituacaoSaldo;

                painelOrdenacaoLista.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Tag.ToInt() == (int)inventario.OrdenacaoContagem).Checked = true;

                OrdeneItensInventario();

                PreenchaGridLancamento();

                BloquearCapa();

                txtId.Enabled = false;

                if (inventario.Status == EnumStatusInventario.ABERTO)
                {
                    btnConsolidarInventario.Visible = true;
                    txtStatus.Text = EnumStatusInventario.ABERTO.Descricao();

                    DesbloquearLancamentoContagem();
                }
                else
                {
                    AltereParaTerceiraContagem();

                    btnConsolidarInventario.Visible = false;
                    btnItensSemContagens.Visible = false;
                    txtStatus.Text = EnumStatusInventario.CONSOLIDADO.Descricao();

                    BloquearLancamentoContagem();
                }

                cboTiposInventarios.Focus();
            }
            else
            {
                txtId.Text = string.Empty;

                cboTiposInventarios.EditValue = null;
                cboModalidades.EditValue = null;

                txtDataInicio.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtDataFinal.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                cboMarcas.EditValue = null;
                cboCategorias.EditValue = null;
                cboGrupos.EditValue = null;
                cboSubGrupos.EditValue = null;

                cboFiltroDeSituacaoDeSaldo.EditValue = null;

                _listaItemInventario.Clear();

                PreenchaGridLancamento();

                ZereNumeroContagem();

                DesbloquearCapa();
                BloquearLancamentoContagem();

                btnConsolidarInventario.Visible = false;

                rdbOrdemListaCodigo.Checked = true;

                txtStatus.Text = EnumStatusInventario.ABERTO.Descricao();

                chkBloquearProdutosMovimentacao.Checked = true;

                chkUtilizaSaldoPrimeiraContagem.Checked = false;
                chkUtilizaSaldoPrimeiraContagem.Checked = true;

                txtId.Enabled = true;

                txtId.Focus();
            }
        }

        private Inventario RetorneInventarioEmEdicao()
        {
            Inventario inventario = new Inventario();

            inventario.Id = txtId.Text.ToInt();

            inventario.TipoInventario = (EnumTipoInventario?)cboTiposInventarios.EditValue;
            inventario.Modalidade = (EnumModalidadeInventario?)cboModalidades.EditValue;

            inventario.DataInicio = txtDataInicio.Text.ToDate();
            inventario.DataFinal = txtDataFinal.Text.ToDate();

            inventario.Marca = cboMarcas.EditValue != null ? new Marca { Id = cboMarcas.EditValue.ToInt() } : null;
            inventario.Categoria = cboCategorias.EditValue != null ? new Categoria { Id = cboCategorias.EditValue.ToInt() } : null;
            inventario.Grupo = cboGrupos.EditValue != null ? new Grupo { Id = cboGrupos.EditValue.ToInt() } : null;
            inventario.SubGrupo = cboSubGrupos.EditValue != null ? new SubGrupo { Id = cboSubGrupos.EditValue.ToInt() } : null;

            inventario.BloquearProdutosMovimentacao = chkBloquearProdutosMovimentacao.Checked;
            inventario.UtilizarSaldoPrimeiraContagem = chkUtilizaSaldoPrimeiraContagem.Checked;

            inventario.FiltroSituacaoSaldo = (EnumFiltroSituacao?)cboFiltroDeSituacaoDeSaldo.EditValue;

            inventario.OrdenacaoContagem = (EnumFiltroOrdenacaoContagem)painelOrdenacaoLista.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            inventario.ListaDeItens = _listaItemInventario;

            inventario.DataInicioPrimeiraContagem = _dataInicioContagemUm;
            inventario.DataInicioSegundaContagem = _dataInicioContagemDois;
            inventario.DataInicioTerceiraContagem = _dataInicioContagemTres;

            return inventario;
        }

        #endregion

        #region " BLOQUEAR E DESBLOQUEAR CAPA "

        private void DesbloquearCapa()
        {
            txtId.Enabled = true;
            cboTiposInventarios.Enabled = true;
            cboModalidades.Enabled = true;

            cboMarcas.Enabled = true;
            cboCategorias.Enabled = true;
            cboGrupos.Enabled = true;
            cboSubGrupos.Enabled = true;

            chkBloquearProdutosMovimentacao.Enabled = true;
            chkUtilizaSaldoPrimeiraContagem.Enabled = true;

            cboFiltroDeSituacaoDeSaldo.Enabled = true;
        }

        private void BloquearCapa()
        {
            txtId.Enabled = false;
            cboTiposInventarios.Enabled = false;
            cboModalidades.Enabled = false;

            txtDataInicio.Enabled = false;
            txtDataFinal.Enabled = false;

            cboMarcas.Enabled = false;
            cboCategorias.Enabled = false;
            cboGrupos.Enabled = false;
            cboSubGrupos.Enabled = false;

            chkBloquearProdutosMovimentacao.Enabled = false;
            chkUtilizaSaldoPrimeiraContagem.Enabled = false;

            cboFiltroDeSituacaoDeSaldo.Enabled = false;
        }

        #endregion

        #region " LANÇAMENTO CONTAGEM "

        private void AtualizeContagem()
        {
            Action actionAatualizarItem = () =>
                {
                    var itemInventario = _listaItemInventario.FirstOrDefault(item => item.Produto.Id == txtIdProduto.Text.ToInt());

                    if (itemInventario != null)
                    {
                        if (_numeroContagem == 1)
                        {
                            itemInventario.QuantidadeContagemUm = txtQuantidadeContada.Text.ToDouble();
                        }
                        else if (_numeroContagem == 2)
                        {
                            itemInventario.QuantidadeContagemDois = txtQuantidadeContada.Text.ToDouble();
                        }
                        else if (_numeroContagem == 3)
                        {
                            itemInventario.QuantidadeContagemTres = txtQuantidadeContada.Text.ToDouble();
                        }

                        ServicoInventario servicoInventario = new ServicoInventario();
                        servicoInventario.AtualizeItem(itemInventario);

                        PreenchaGridLancamento();

                        LimpeCamposLancamentoProduto();
                    }
                };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionAatualizarItem, exibirMensagemDeSucesso: false);
        }

        private void DesbloquearLancamentoContagem()
        {
            txtIdProduto.Enabled = true;
            txtCodigoDeBarrasProduto.Enabled = true;
            txtQuantidadeContada.Enabled = true;

            btnInserirAtualizarItem.Enabled = true;
            btnCancelarItem.Enabled = true;

            _itensBloqueados = false;
        }

        private void BloquearLancamentoContagem()
        {
            txtIdProduto.Enabled = false;
            txtCodigoDeBarrasProduto.Enabled = false;
            txtQuantidadeContada.Enabled = false;

            btnInserirAtualizarItem.Enabled = false;
            btnCancelarItem.Enabled = false;

            _itensBloqueados = true;
        }

        private void PreenchaCamposDoProduto(ItemInventario item, bool casoNuloFocusNoIdProduto = true)
        {
            if (item != null && !_itensBloqueados)
            {
                var produto = item.Produto;

                txtIdProduto.Text = produto.Id.ToString();
                txtCodigoDeBarrasProduto.Text = produto.DadosGerais.CodigoDeBarras;
                txtItemDescricaoProduto.Text = produto.DadosGerais.Descricao;
                txtUnidadeEstocar.Text = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                AltereMascaraQuantidadeProduto(produto);

                if (_numeroContagem == 1)
                {
                    txtQuantidadeContada.Text = item.QuantidadeContagemUm.ToStringEmpty();
                }

                else if (_numeroContagem == 2)
                {
                    txtQuantidadeContada.Text = item.QuantidadeContagemDois.ToStringEmpty();
                }

                else if (_numeroContagem == 3)
                {
                    txtQuantidadeContada.Text = item.QuantidadeContagemTres.ToStringEmpty();
                }

                txtQuantidadeContada.Focus();
            }
            else
            {
                txtIdProduto.Text = string.Empty;
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtItemDescricaoProduto.Text = string.Empty;
                txtUnidadeEstocar.Text = string.Empty;

                txtQuantidadeContada.Text = string.Empty;

                if (casoNuloFocusNoIdProduto)
                {
                    txtIdProduto.Focus();
                }
            }
        }

        private void AltereMascaraQuantidadeProduto(Produto produto)
        {
            if (produto.DadosGerais.PermiteVendaFracionada)
            {
                txtQuantidadeContada.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtQuantidadeContada.Properties.Mask.EditMask = @"[0-9]{1,11}";
            }
        }

        private void LimpeCamposLancamentoProduto()
        {
            PreenchaCamposDoProduto(null);

            txtIdProduto.Focus();
        }

        private void PreenchaGridLancamento()
        {
            List<ItemInventarioGrid> listaItemGrid = new List<ItemInventarioGrid>();
            double quantidadesubestoque = 0;
            foreach (var item in _listaItemInventario)
            {
                ItemInventarioGrid itemInventarioGrid = new ItemInventarioGrid();

                itemInventarioGrid.Descricao = item.Produto.DadosGerais.Descricao;
                itemInventarioGrid.IdProduto = item.Produto.Id;

                itemInventarioGrid.QuantidadeContagemUm = item.QuantidadeContagemUm;
                itemInventarioGrid.QuantidadeContagemDois = item.QuantidadeContagemDois;
                itemInventarioGrid.QuantidadeContagemTres = item.QuantidadeContagemTres;

                itemInventarioGrid.QuantidadeEstoque = item.QuantidadeEstoque;
                quantidadesubestoque = 0;

                ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
                var ItemTransferencia = servicoItemTransferencia.ConsulteProduto(item.Produto.Id);
                if (ItemTransferencia.Count !=0)
                {
                    foreach (var itemproduto in ItemTransferencia)
                    {
                        quantidadesubestoque += itemproduto.QuantidadeEstoque;
                    }
                    itemInventarioGrid.QuantidadeSubEstoque = quantidadesubestoque;
                }
                else
                {
                    itemInventarioGrid.QuantidadeSubEstoque = 0;
                }


                itemInventarioGrid.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                listaItemGrid.Add(itemInventarioGrid);
            }

            gcItens.DataSource = listaItemGrid;
            gcItens.RefreshDataSource();

            PreenchaGridAcompanhamento();
        }

        private void PreenchaGridAcompanhamento()
        {
            Acompanhamento acompanhamentoUm = new Acompanhamento();
            acompanhamentoUm.AcompanhamentoOnline = "Contagem 1";

            Acompanhamento acompanhamentoDois = new Acompanhamento();
            acompanhamentoDois.AcompanhamentoOnline = "Contagem 2";

            Acompanhamento acompanhamentoTres = new Acompanhamento();
            acompanhamentoTres.AcompanhamentoOnline = "Contagem 3";

            int totalItens = _listaItemInventario.Count;

            if (totalItens > 0)
            {
                int qtdItensInformadosContagemUm = 0;
                int qtdItensInformadosContagemDois = 0;
                int qtdItensInformadosContagemTres = 0;

                int qtdItensDivergentesContagemUm = 0;
                int qtdItensDivergentesContagemDois = 0;
                int qtdItensDivergentesContagemTres = 0;

                foreach (var item in _listaItemInventario)
                {
                    if (item.QuantidadeContagemUm != null)
                    {
                        qtdItensInformadosContagemUm++;
                        qtdItensDivergentesContagemUm += item.QuantidadeContagemUm != item.QuantidadeEstoque ? 1 : 0;
                    }

                    if (item.QuantidadeContagemDois != null)
                    {
                        qtdItensInformadosContagemDois++;
                        qtdItensDivergentesContagemDois += item.QuantidadeContagemDois != item.QuantidadeEstoque ? 1 : 0;
                    }

                    if (item.QuantidadeContagemTres != null)
                    {
                        qtdItensInformadosContagemTres++;
                        qtdItensDivergentesContagemTres += item.QuantidadeContagemTres != item.QuantidadeEstoque ? 1 : 0;
                    }
                }

                if (_numeroContagem > 0)
                {
                    double percentualConclusao = (qtdItensInformadosContagemUm / (double)totalItens) * 100;

                    acompanhamentoUm.Evolucao = qtdItensInformadosContagemUm + "/" + totalItens + " (" + percentualConclusao.ToString("0.00") + "%)";
                    acompanhamentoUm.Divergencia = qtdItensDivergentesContagemUm.ToString();
                    acompanhamentoUm.DataInicio = _dataInicioContagemUm.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm");
                }

                if (_numeroContagem > 1)
                {
                    double percentualConclusao = (qtdItensInformadosContagemDois / (double)totalItens) * 100;

                    acompanhamentoDois.Evolucao = qtdItensInformadosContagemDois + "/" + totalItens + " (" + percentualConclusao.ToString("0.00") + "%)";
                    acompanhamentoDois.Divergencia = qtdItensDivergentesContagemDois.ToString();
                    acompanhamentoDois.DataInicio = _dataInicioContagemDois.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm");
                }

                if (_numeroContagem > 2)
                {
                    double percentualConclusao = (qtdItensInformadosContagemTres / (double)totalItens) * 100;

                    acompanhamentoTres.Evolucao = qtdItensInformadosContagemTres + "/" + totalItens + " (" + percentualConclusao.ToString("0.00") + "%)";
                    acompanhamentoTres.Divergencia = qtdItensDivergentesContagemTres.ToString();
                    acompanhamentoTres.DataInicio = _dataInicioContagemTres.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm");
                }
            }

            List<Acompanhamento> listaAcompanhamentos = new List<Acompanhamento>();
            listaAcompanhamentos.Add(acompanhamentoUm);
            listaAcompanhamentos.Add(acompanhamentoDois);
            listaAcompanhamentos.Add(acompanhamentoTres);

            gcAcompanhamentoOnline.DataSource = listaAcompanhamentos;
            gcAcompanhamentoOnline.RefreshDataSource();
        }

        #endregion

        #endregion

        #region " CLASSES AUXILIARES "

        private class AgenciaAuxiliarComboBox
        {
            public int Id { get; set; }

            public string Descricao { get; set; }
        }

        private class ItemInventarioGrid
        {
            public int IdProduto { get; set; }

            public string Descricao { get; set; }

            public string Unidade { get; set; }

            public double QuantidadeEstoque { get; set; }

            public double QuantidadeSubEstoque { get; set; }

            public double? QuantidadeContagemUm { get; set; }

            public double? QuantidadeContagemDois { get; set; }

            public double? QuantidadeContagemTres { get; set; }
        }

        private class Acompanhamento
        {
            public string AcompanhamentoOnline { get; set; }

            public string Evolucao { get; set; }

            public string Divergencia { get; set; }

            public string DataInicio { get; set; }
        }

        #endregion

        private void gcItens_Click(object sender, EventArgs e)
        {

        }

        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
