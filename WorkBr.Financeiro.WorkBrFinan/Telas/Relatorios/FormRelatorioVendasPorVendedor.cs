using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.FabricanteServ;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using BoletoNet.Util;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioVendasPorVendedor : FormularioBase
    {
        Parametros _parametros = new ServicoParametros().ConsulteParametros();

        #region " CONSTRUTOR "

        public FormRelatorioVendasPorVendedor()
        {
            InitializeComponent();

            PreenchaCboFuncoes();
            PreenchaCboVendedores();
            PreenchaPrimeiroEUltimoDiaMes();
            PreenchaCboFabricantes();
            PreenchaCboMarcas();
            PreenchaCboCategorias();
            HabiliteDesabiliteSituacoes();

            DesabiliteSelecaoParceiroSenaoPermitido();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            EnumPesquisaPorFuncao? funcao = (EnumPesquisaPorFuncao?)cboFuncao.EditValue;
           

            bool statusAberto = chkStatusAberto.Checked;
            bool statusOrcamento = chkStatusOrcamento.Checked;
            bool statusCancelado = chkStatusCancelado.Checked;
            bool statusEmLiberacao = chkStatusEmLiberacao.Checked;
            bool statusRecusado = chkStatusRecusado.Checked;
            bool statusReservado = chkStatusReservado.Checked;
            bool statusFaturado = chkStatusFaturado.Checked;
            bool statusEmitidoNFe = chkStatusEmitidoNFe.Checked;
            bool comissaoPorItem = chkComissaoItem.Checked;
            bool comissaoCompartilhada = chkComissaoCompartilhada.Checked;
            bool recebidos = chkRecebidos.Checked;

            bool statusNaoPago = chkNaoPago.Checked;
            bool statusNotaSemRecebimento = _parametros.ParametrosFiscais.EmitirNotaSemReceber;

            bool statusComissaoServico = chkComissaoServicos.Checked;
            bool consolidado = chkConsolidado.Checked;

            



                Marca marca = cboMarcas.EditValue != null ? new Marca { Id = cboMarcas.EditValue.ToInt() } : null;
                if (marca != null)
                    marca.Descricao = cboMarcas.Text;


                Fabricante fabricante = cboFabricantes.EditValue != null ? new Fabricante { Id = cboFabricantes.EditValue.ToInt() } : null;
                if (fabricante != null)
                    fabricante.Descricao = cboFabricantes.Text;


                Categoria categoria = cboCategorias.EditValue != null ? new Categoria { Id = cboCategorias.EditValue.ToInt() } : null;
                if (categoria != null)
                    categoria.Descricao = cboCategorias.Text;

                Grupo grupo = cboGrupos.EditValue != null ? new Grupo { Id = cboGrupos.EditValue.ToInt() } : null;
                if (grupo != null)
                    grupo.Descricao = cboGrupos.Text;


                SubGrupo subgrupo = cboSubGrupos.EditValue != null ? new SubGrupo { Id = cboSubGrupos.EditValue.ToInt() } : null;
                if (subgrupo != null)
                    subgrupo.Descricao = cboSubGrupos.Text;

                //Pessoa pessoa = cboColaboradores.EditValue != null ? new Pessoa { Id = cboColaboradores.EditValue.ToInt() } : null;

                List<Pessoa> listaPessoa = new List<Pessoa>();
                List<Pessoa> listaColaborador = new List<Pessoa>();
            if (consolidado == false)
            {
                if (chktodos.Checked == false)
                {

                    if (lstColaboradores.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Selecione o Vendedor!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }


                    for (int i = 0; i < lstVendedores.SelectedItems.Count; i++)
                    {

                        string textoPessoa = lstVendedores.SelectedItems[i].ToString();
                        string[] codPessoa;

                        codPessoa = textoPessoa.Split('-');

                        Pessoa pessoa = lstVendedores.SelectedItem != null ? new Pessoa { Id = codPessoa[0].Trim().ToInt() } : null;

                        listaPessoa.Add(pessoa);
                    }

                    if (lstVendedores.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < lstColaboradores.SelectedItems.Count; i++)
                        {
                            string textoPessoa = lstColaboradores.SelectedItems[i].ToString();
                            string[] codPessoa;

                            codPessoa = textoPessoa.Split('-');
                        }

                        if (lstColaboradores.SelectedItems.Count > 0)
                        {
                            //for (int i = 0; i < lstColaboradores.SelectedItems.Count; i++)
                            //{
                            //    string textoColaborador = lstColaboradores.SelectedItems[i].ToString();
                            //    string[] codColaborador;

                            //    codColaborador = textoColaborador.Split('-');

                            //    Indicador = codColaborador[0].Trim().ToInt();
                            //}
                            for (int i = 0; i < lstColaboradores.SelectedItems.Count; i++)
                            {
                                string textoColaborador = lstColaboradores.SelectedItems[i].ToString();
                                string[] codColaborador;

                                codColaborador = textoColaborador.Split('-');

                                Pessoa pessoa = lstColaboradores.SelectedItem != null ? new Pessoa { Id = codColaborador[0].Trim().ToInt() } : null;

                                listaColaborador.Add(pessoa);
                            }
                        }
                    }

                    else
                    {
                        for (int i = 0; i < lstColaboradores.SelectedItems.Count; i++)
                        {
                            string textoPessoa = lstColaboradores.SelectedItems[i].ToString();
                            string[] codPessoa;

                            codPessoa = textoPessoa.Split('-');

                            Pessoa pessoa = lstColaboradores.SelectedItem != null ? new Pessoa { Id = codPessoa[0].Trim().ToInt() } : null;

                            listaPessoa.Add(pessoa);
                        }

                        for (int i = 0; i < lstColaboradores.SelectedItems.Count; i++)
                        {
                            string textoColaborador = lstColaboradores.SelectedItems[i].ToString();
                            string[] codColaborador;

                            codColaborador = textoColaborador.Split('-');

                            Pessoa pessoa = lstColaboradores.SelectedItem != null ? new Pessoa { Id = codColaborador[0].Trim().ToInt() } : null;

                            listaColaborador.Add(pessoa);
                        }
                    }

                }

            }

            DateTime dataInicial = rdbPeriodoFaturamento.Checked ? txtDataInicialFaturamento.Text.ToDate() : txtDataInicialEmissao.Text.ToDate();
            DateTime dataFinal = rdbPeriodoFaturamento.Checked ? txtDataFinalFaturamento.Text.ToDate() : txtDataFinalEmissao.Text.ToDate();

            EnumOrdenacaoPesquisaVwVendas ordenacao = (EnumOrdenacaoPesquisaVwVendas)pnlOrdenacao.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            string inconsistencias = string.Empty;

            inconsistencias += funcao == null ? "Informe uma função.\n\n" : string.Empty;

            inconsistencias += dataInicial == DateTime.MinValue ? "Informe a data inicial do período.\n\n" : string.Empty;
            inconsistencias += dataFinal == DateTime.MinValue ? "Informe a data final do período.\n\n" : string.Empty;

            if (!string.IsNullOrEmpty(inconsistencias))
            {
                MessageBox.Show(inconsistencias);
                this.Cursor = Cursors.Default;
                return;
            }

            if (chkEmitirChoque.Checked)
            {
                RelatorioVendasChoque relatorioVendasChoque = new RelatorioVendasChoque(funcao.GetValueOrDefault(),
                                                                                               listaPessoa,
                                                                                               rdbPeriodoFaturamento.Checked,
                                                                                               dataInicial,
                                                                                               dataFinal,
                                                                                               statusAberto,
                                                                                               statusOrcamento,
                                                                                               statusCancelado,
                                                                                               statusEmLiberacao,
                                                                                               statusRecusado,
                                                                                               statusReservado,
                                                                                               statusFaturado,
                                                                                               statusEmitidoNFe,
                                                                                               comissaoPorItem,
                                                                                               comissaoCompartilhada,
                                                                                               recebidos,
                                                                                               ordenacao, marca,
                                                                                               fabricante, subgrupo,
                                                                                               categoria, grupo);

                TratamentosDeTela.ExibirRelatorio(relatorioVendasChoque);
                this.Cursor = Cursors.Default;
            }
            else
            {
                RelatorioVendas relatorioVendas = new RelatorioVendas(funcao.GetValueOrDefault(),
                                                                                               listaPessoa,
                                                                                               listaColaborador,
                                                                                               rdbPeriodoFaturamento.Checked,
                                                                                               dataInicial,
                                                                                               dataFinal,
                                                                                               statusAberto,
                                                                                               statusOrcamento,
                                                                                               statusCancelado,
                                                                                               statusEmLiberacao,
                                                                                               statusRecusado,
                                                                                               statusReservado,
                                                                                               statusFaturado,
                                                                                               statusEmitidoNFe,
                                                                                               comissaoPorItem,
                                                                                               comissaoCompartilhada,
                                                                                               recebidos,
                                                                                               statusNaoPago,
                                                                                               statusNotaSemRecebimento,
                                                                                               ordenacao, marca,
                                                                                               fabricante, subgrupo,
                                                                                               categoria, grupo, statusComissaoServico, 
                                                                                               consolidado);

                TratamentosDeTela.ExibirRelatorio(relatorioVendas);
                this.Cursor = Cursors.Default;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CboVendedores_EditValueChanged(object sender, EventArgs e)
        {
            lstVendedores.SelectedIndex = cboVendedores.ItemIndex;
        }

        private void cboFuncao_EditValueChanged(object sender, EventArgs e)
        {
            chktodos.Visible = false;
            lstColaboradores.Items.Clear();
            lstVendedores.Items.Clear();

            EnumPesquisaPorFuncao? funcao = (EnumPesquisaPorFuncao?)cboFuncao.EditValue;

            if (funcao == null)
            {
                lstColaboradores.Items.Clear();
                lstVendedores.Items.Clear();
            }

            else if (funcao == EnumPesquisaPorFuncao.INDICADOR)
            {
                PreenchalstIndicadores();
                lstColaboradores.Size = new System.Drawing.Size(374, 219);
                lstVendedores.Visible = false;
                lblVendedores.Visible = false;
                
            }

            else if (funcao == EnumPesquisaPorFuncao.ATENDENTE)
            {
                PreenchalstAtendentes();
                lstColaboradores.Size = new System.Drawing.Size(374, 219);
                lstVendedores.Visible = false;
                lblVendedores.Visible = false;
                
            }

            else if (funcao == EnumPesquisaPorFuncao.VENDEDOR)
            {
                PreenchalstVendedores();
                lstColaboradores.Size = new System.Drawing.Size(374, 219);
                lstVendedores.Visible = false;
                lblVendedores.Visible = false;
                chktodos.Visible = true;
            }

            else if (funcao == EnumPesquisaPorFuncao.SUPERVISOR)
            {
                PreenchalstSupervisores();
                lstColaboradores.Size = new System.Drawing.Size(374, 219);
                lstVendedores.Visible = false;
                lblVendedores.Visible = false;
                
            }

            else if (funcao == EnumPesquisaPorFuncao.INDICADORVENDEDOR)
            {
                PreenchalstIndicadores();
                PreenchalstVendedoresNova();
                lstColaboradores.Size = new System.Drawing.Size(374, 86);
                lstVendedores.Visible = true;
                lblVendedores.Visible = true;
                
            }
        }

        private void rdbPeriodoFaturamento_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPeriodoFaturamento.Checked)
            {
                txtDataInicialEmissao.Enabled = false;
                txtDataInicialEmissao.Text = string.Empty;

                txtDataFinalEmissao.Enabled = false;
                txtDataFinalEmissao.Text = string.Empty;

                txtDataInicialFaturamento.Enabled = true;
                txtDataFinalFaturamento.Enabled = true;
               
            }
            else
            {
                txtDataInicialEmissao.Enabled = true;
                txtDataFinalEmissao.Enabled = true;

                txtDataInicialFaturamento.Enabled = false;
                txtDataInicialFaturamento.Text = string.Empty;

                txtDataFinalFaturamento.Enabled = false;
                txtDataFinalFaturamento.Text = string.Empty;
               
                
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboFuncoes()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumPesquisaPorFuncao>();

            lista.Insert(0, null);

            cboFuncao.Properties.DataSource = lista;
            cboFuncao.Properties.ValueMember = "Valor";
            cboFuncao.Properties.DisplayMember = "Descricao";
        }
        
        private void PreenchalstVendedores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaVendedoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            foreach (var itens in listaObjetoValor)
            {
                int i = 0;
                lstColaboradores.Items.Add(itens.Descricao);
                i++;

            }

            lstColaboradores.Items.Add("0 - VENDAS SEM VENDEDOR");        
        }

        private void PreenchalstVendedoresNova()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaVendedoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            foreach (var itens in listaObjetoValor)
            {
                int i = 0;
                lstVendedores.Items.Add(itens.Descricao);
                i++;

            }

            lstVendedores.Items.Add("0 - VENDAS SEM VENDEDOR");
        }

        private void PreenchalstAtendentes()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaAtendentesAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            foreach (var itens in listaObjetoValor)
            {
                int i = 0;
                lstColaboradores.Items.Add(itens.Descricao);
                i++;

            }
        }

        private void PreenchalstIndicadores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaIndicadoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            foreach (var itens in listaObjetoValor)
            {
                int i = 0;
                lstColaboradores.Items.Add(itens.Descricao);
                i++;

            }
        }

        private void PreenchalstSupervisores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaSupervisoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            foreach (var itens in listaObjetoValor)
            {
                int i = 0;
                lstColaboradores.Items.Add(itens.Descricao);
                i++;

            }
        }

        private void PreenchaPrimeiroEUltimoDiaMes()
        {
            var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

            txtDataInicialEmissao.DateTime = primeiroDiaMes;
            txtDataFinalEmissao.DateTime = ultimoDiaMes;
        }

        private void DesabiliteSelecaoParceiroSenaoPermitido()
        {
            var permissaoVisualizarTodosVendedores = Sessao.ListaDePermissoes.FirstOrDefault(permissao => permissao.Funcionalidade == EnumFuncionalidade.RELATORIOVENDASVENDEDORVISUALIZARTODOSVENDEDORES);

            if (permissaoVisualizarTodosVendedores == null || !permissaoVisualizarTodosVendedores.Acessar)
            {
                if (Sessao.PessoaLogada.Vendedor.EhAtendente)
                {
                    cboFuncao.EditValue = EnumPesquisaPorFuncao.ATENDENTE;
                }
                else if (Sessao.PessoaLogada.Vendedor.EhIndicador)
                {
                    cboFuncao.EditValue = EnumPesquisaPorFuncao.INDICADOR;
                }
                else if (Sessao.PessoaLogada.Vendedor.EhVendedor)
                {
                    cboFuncao.EditValue = EnumPesquisaPorFuncao.VENDEDOR;
                }
                else if (Sessao.PessoaLogada.Vendedor.EhSupervisor)
                {
                    cboFuncao.EditValue = EnumPesquisaPorFuncao.SUPERVISOR;
                }

               
                cboFuncao.EditValue = EnumPesquisaPorFuncao.VENDEDOR;
                lstColaboradores.Items.Clear();
                lstColaboradores.Items.Add(Sessao.PessoaLogada.Id + "-" + Sessao.PessoaLogada.DadosGerais.Razao);
                cboFuncao.Properties.ReadOnly = true;               
            }
        }

        private void HabiliteDesabiliteSituacoes()
        {
            if(_parametros.ParametrosFiscais.EmitirNotaSemReceber)
            {
                chkStatusAberto.Visible = false;
                chkStatusEmLiberacao.Visible = false;
                chkStatusOrcamento.Visible = false;
                chkStatusRecusado.Visible = false;
                chkEmitirChoque.Visible = false;
                chkComissaoItem.Visible = true;
                chkComissaoCompartilhada.Visible = false;
                chkRecebidos.Visible = false;
                chkNaoPago.Visible = true;
                chkComissaoServicos.Visible = true;

                chkStatusEmitidoNFe.Checked = false;
            }
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

            if (cboMarcas.EditValue != null)
            {
                if (!marcas.Exists(marca => marca != null && marca.Id == cboMarcas.EditValue.ToInt()))
                {
                    cboMarcas.EditValue = null;
                }
            }
        }


        private void PreenchaCboFabricantes()
        {
            ServicoFabricante servicoFabricante = new ServicoFabricante();

            var fabricantes = servicoFabricante.ConsulteListaAtiva();

            fabricantes.Insert(0, null);

            cboFabricantes.Properties.DataSource = fabricantes;
            cboFabricantes.Properties.DisplayMember = "Descricao";
            cboFabricantes.Properties.ValueMember = "Id";

            if (cboFabricantes.EditValue != null)
            {
                if (!fabricantes.Exists(fabricante => fabricante != null && fabricante.Id == cboFabricantes.EditValue.ToInt()))
                {
                    cboFabricantes.EditValue = null;
                }
            }
        }
       
          
       
    
        private void PreenchaCboVendedores()
        {

            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaVendedoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });
            
            cboVendedores.Properties.DataSource = listaObjetoValor;
            cboVendedores.Properties.DisplayMember = "Descricao";
            cboVendedores.Properties.ValueMember = "Id";

            lstVendedores.Items.Add("0 - VENDAS SEM VENDEDOR");

            //if (string.IsNullOrEmpty(cboVendedores.Text))
            //{
            //    cboVendedores.EditValue = null;
            //}
        }
        
        #endregion

        private void cboCategorias_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboGrupos();
        }

        private void cboSubGrupos_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cboGrupos_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboSubGrupos();
        }

        private void chkNaoPago_CheckedChanged(object sender, EventArgs e)
        {
            if (_parametros.ParametrosFiscais.EmitirNotaSemReceber)
            {
                chkStatusFaturado.Checked = false;
                chkStatusCancelado.Checked = false;
                chkStatusEmitidoNFe.Checked = false;
                chkStatusReservado.Checked = false;
                chkComissaoServicos.Checked = false;
            }
        }

        private void chkStatusFaturado_CheckedChanged(object sender, EventArgs e)
        {
            if (_parametros.ParametrosFiscais.EmitirNotaSemReceber)
            {
                chkNaoPago.Checked = false;
                chkStatusCancelado.Checked = false;
                chkStatusEmitidoNFe.Checked = false;
                //chkStatusReservado.Checked = false;
                chkComissaoServicos.Checked = false;
            }                
        }

        private void chkStatusCancelado_CheckedChanged(object sender, EventArgs e)
        {
            if (_parametros.ParametrosFiscais.EmitirNotaSemReceber)
            {
                chkNaoPago.Checked = false;
                chkStatusFaturado.Checked = false;
                chkComissaoServicos.Checked = false;
            }
        }

        private void chkStatusReservado_CheckedChanged(object sender, EventArgs e)
        {
            if (_parametros.ParametrosFiscais.EmitirNotaSemReceber)
            {
                //chkNaoPago.Checked = false;
                chkStatusFaturado.Checked = false;
                chkComissaoServicos.Checked = false;
            }
        }

        private void chkStatusEmitidoNFe_CheckedChanged(object sender, EventArgs e)
        {
            if (_parametros.ParametrosFiscais.EmitirNotaSemReceber)
            {
                chkNaoPago.Checked = false;
                chkStatusFaturado.Checked = false;
                chkComissaoServicos.Checked = false;
            }
        }

        private void chkComissaoServicos_CheckedChanged(object sender, EventArgs e)
        {
            chkComissaoItem.Checked = false;
            chkNaoPago.Checked = false;
            chkStatusCancelado.Checked = false;
            chkStatusEmitidoNFe.Checked = false;
            chkStatusReservado.Checked = false;
            chkStatusFaturado.Checked = false;
        }

        private void chkComissaoItem_CheckedChanged(object sender, EventArgs e)
        {
            chkComissaoServicos.Checked = false;
        }

        private void FormRelatorioVendasPorVendedor_Load(object sender, EventArgs e)
        {
            lstColaboradores.Size = new System.Drawing.Size(374, 186);
            
        }

        private void chktodos_CheckedChanged(object sender, EventArgs e)
        {
           if (chktodos.Checked == true)
            {
                //lstColaboradores.Items.Clear();
            }
           else
            {
                List<Pessoa> listaPessoa = new List<Pessoa>();
            }
           
        }

        private void chkConsolidado_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    
}
