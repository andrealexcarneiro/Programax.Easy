using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.Servico.Cadastros.CorServ;
using Programax.Easy.Servico.Cadastros.FabricanteServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.Servico.Cadastros.TamanhoServ;
using Programax.Easy.Servico.Cadastros.UnidadeMedidaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Categorias;
using Programax.Easy.View.Telas.Cadastros.Cores;
using Programax.Easy.View.Telas.Cadastros.Fabricantes;
using Programax.Easy.View.Telas.Cadastros.Grupos;
using Programax.Easy.View.Telas.Cadastros.Marcas;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.View.Telas.Cadastros.SubGrupos;
using Programax.Easy.View.Telas.Cadastros.Tamanhos;
using Programax.Easy.View.Telas.Cadastros.UnidadesMedidas;
using Programax.Easy.View.Telas.Fiscal.Ncms;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Cadastros.GrupoTributacaoIcmsServ;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Cadastros.GrupoTributacoesIcms;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.TransferenciaServ;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Programax.Easy.View.Telas.Cadastros.Produtos
{
    public partial class FormCadastroDeProduto : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Produto _produtoEmEdicao;

        private Ncm _ncm;
        private ServicoProduto _servicoProduto;

        private Parametros _parametros;

        private bool _carregarGrupos = true;
        private bool _carregarSubGrupos = true;
        private string ConectionString;

        private List<FornecedorProduto> _listaFornecedoresProdutos;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroDeProduto()
        {
            InitializeComponent();

            _produtoEmEdicao = new Produto();
            _listaFornecedoresProdutos = new List<FornecedorProduto>();

            PreenchaOStatus();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            TrateUsuarioContemPermissaoAtalhos();

            this.NomeDaTela = "Cadastro de Item";

            PreenchaCboCategorias();
            PreenchaCboMarcas();
            PreenchaCboFabricantes();
            PreenchaCboUnidades();
            PreenchaCboTamanhos();
            PreenchaCboCores();
            PreenchaCboSexo();
            PreenchaCboNaturezaProduto();
            PreenchaCboSituacaoTributariaProdutro();
            PreenchaCboOrigemProduto();
            PreenchaCboTipoMovimentacao();
            PreenchaCboOrigemMovimentacao();
            
            _servicoProduto = new ServicoProduto();

            _parametros = new ServicoParametros().ConsulteParametros();

            VerificaParamentosDoItem();

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        #region " DADOS GERAIS "

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text) && txtId.Enabled)
            {
                PesquisePeloId();
            }
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            PesquiseProduto();
        }

        private void btnAddImagem_Click(object sender, EventArgs e)
        {
            try
            {
                if (picFoto != null)
                {
                    TratamentoDeImagens.BuscaImagem(picFoto, "Imagens|*.*");
                    btnAddImagem.Enabled = true;
                }
                else
                    btnAddImagem.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void btnDelImagem_Click(object sender, EventArgs e)
        {
            try
            {
                if (picFoto != null)
                {
                    if (MessageBox.Show(this, "Tem certeza que deseja remover a foto?", "Atenção", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        btnAddImagem.Enabled = true;
                        btnDelImagem.Enabled = false;
                        picFoto.Image = Properties.Resources.produtos;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void btnAtalhoUnidade_Click(object sender, EventArgs e)
        {
            FormCadastroUnidadeMedida formCadastroUnidadeMedida = new FormCadastroUnidadeMedida();
            formCadastroUnidadeMedida.ShowDialog();

            PreenchaCboUnidades();
        }

        #endregion

        #region " PRINCIPAL "

        private void txtIdItemComposicao_Leave(object sender, EventArgs e)
        {
            ServicoProduto servicoProduto = new ServicoProduto();

            if (!string.IsNullOrEmpty(txtIdProdutoSimilar.Text))
            {
                var produto = servicoProduto.ConsulteProdutoAtivo(txtIdProdutoSimilar.Text.ToInt());

                PreenchaProdutoSimilar(produto, true);
            }
            else
            {
                PreenchaProdutoSimilar(null);
            }
        }

        private void btnPesquisaProdutoComposicao_Click(object sender, EventArgs e)
        {
            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProdutoAtivo();

            if (produto != null)
            {
                txtIdProdutoSimilar.Text = produto.Id.ToString();
                txtDescricaoProdutoSimilar.Text = produto.DadosGerais.Descricao;
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

        private void btnAtalhoMarca_Click(object sender, EventArgs e)
        {
            FormCadastroMarca formCadastroMarca = new FormCadastroMarca();
            formCadastroMarca.ShowDialog();

            PreenchaCboMarcas();
        }

        private void btnAtalhoFabricante_Click(object sender, EventArgs e)
        {
            FormCadastroFabricante formCadastroFabricante = new FormCadastroFabricante();
            formCadastroFabricante.ShowDialog();

            PreenchaCboFabricantes();
        }

        private void btnAtalhoCategoria_Click(object sender, EventArgs e)
        {
            FormCadastroCategoria formCadastroCategoria = new FormCadastroCategoria();
            formCadastroCategoria.ShowDialog();

            PreenchaCboCategorias();
        }

        private void btnAtalhoSubGrupo_Click(object sender, EventArgs e)
        {
            var grupo = cboGrupos.EditValue != null ? new Grupo { Id = cboGrupos.EditValue.ToInt() } : null;

            FormCadastroSubGrupos formCadastroSubGrupos = new FormCadastroSubGrupos();
            formCadastroSubGrupos.ExibaCadastroSubGrupo(grupo);

            PreenchaCboSubGrupos();
        }

        private void btnAtalhoGrupo_Click(object sender, EventArgs e)
        {
            var categoria = cboCategorias.EditValue != null ? new Categoria { Id = cboCategorias.EditValue.ToInt() } : null;

            FormCadastroGrupo formCadastroGrupo = new FormCadastroGrupo();
            formCadastroGrupo.ExibaCadastroGrupo(categoria);

            PreenchaCboGrupos();
        }

        #endregion

        #region " VESTUÁRIO "

        private void btnAtalhoTamanho_Click(object sender, EventArgs e)
        {
            FormCadastroDeTamanhos formCadastroDeTamanhos = new FormCadastroDeTamanhos();
            formCadastroDeTamanhos.ShowDialog();

            PreenchaCboTamanhos();
        }

        private void btnAtalhoCor_Click(object sender, EventArgs e)
        {
            FormCadastroDeCores formCadastroDeCores = new FormCadastroDeCores();
            formCadastroDeCores.ShowDialog();

            PreenchaCboCores();
        }

        #endregion

        #region " CONTÁBIL/FISCAL "

        private void txtNcmId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNcmId.Text))
            {
                ServicoNcm servicoNcm = new ServicoNcm();

                var ncm = servicoNcm.ConsultePeloCodigoNcm(txtNcmId.Text);

                PreenchaNcm(ncm);
            }
            else
            {
                PreenchaNcm(null, false);
            }
        }

        private void btnPesquisaNcm_Click(object sender, EventArgs e)
        {
            FormNcmPesquisa formNcmPesquisa = new FormNcmPesquisa();
            var ncm = formNcmPesquisa.ExibaPesquisaDeNcm();

            if (ncm != null)
            {
                PreenchaNcm(ncm);
            }
        }

        private void cboSituacaoTributaria_EditValueChanged(object sender, EventArgs e)
        {
            EnumSituacaoTributariaProduto? situacaoTributaria = (EnumSituacaoTributariaProduto?)cboSituacaoTributaria.EditValue;

            if (situacaoTributaria == null ||
                situacaoTributaria == EnumSituacaoTributariaProduto.ISENTO ||
                situacaoTributaria == EnumSituacaoTributariaProduto.NAOTRIBUTADO ||
                situacaoTributaria == EnumSituacaoTributariaProduto.SUBSTITUICAOTRIBUTARIA ||
                situacaoTributaria == EnumSituacaoTributariaProduto.ISENTOISSQN)
            {
                txtIcms.Text = string.Empty;
                txtIcms.Enabled = false;
            }
            else
            {
                txtIcms.Enabled = true;
            }
        }

        private void txtIdGrupoTributacaoIcms_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtIdGrupoTributacaoIcms.Text))
            {
                ServicoGrupoTributacaoIcms servicoGrupoTributacaoIcms = new ServicoGrupoTributacaoIcms();
                var grupoTributacaoIcms = servicoGrupoTributacaoIcms.Consulte(txtIdGrupoTributacaoIcms.Text.ToInt());

                PreenchaGrupoTributacaoIcms(grupoTributacaoIcms, true);
            }
            else
            {
                PreenchaGrupoTributacaoIcms(null);
            }
        }

        private void pbGrupoTributacaoIcms_Click(object sender, EventArgs e)
        {
            FormGrupoTributacaoIcmsPesquisa formGrupoTribucaoPesquisa = new FormGrupoTributacaoIcmsPesquisa();

            var GrupoTributacao = formGrupoTribucaoPesquisa.ExibaPesquisaDeGrupoDeTributacaoIcms();

            if (GrupoTributacao != null)
            {
                PreenchaGrupoTributacaoIcms(GrupoTributacao);
            }
        }

        private void pbGrupoTributacaoFederal_Click(object sender, EventArgs e)
        {
            FormGrupoTributacaoFederalPesquisa formGrupoTribucaoPesquisa = new FormGrupoTributacaoFederalPesquisa();

            var GrupoTributacao = formGrupoTribucaoPesquisa.ExibaPesquisaDeGrupoDeTributacaoFederal();

            if (GrupoTributacao != null)
            {
                PreenchaGrupoTributacaoFederal(GrupoTributacao);
            }
        }

        #endregion

        #region " FORMAÇÃO DE PREÇO "

        private void txtPercentualReducaoIcmsCompra_EditValueChanged(object sender, EventArgs e)
        {
            //CalcularPrecoCusto(); Estes não vão calcular, pois não entram no cálculo do custo do item. Apenas informativo.
        }

        private void txtPercentualIcmsStCompra_EditValueChanged(object sender, EventArgs e)
        {
            //CalcularPrecoCusto(); Estes não vão calcular, pois não entram no cálculo do custo do item. Apenas informativo.
        }

        private void txtPercentualIpiCompra_EditValueChanged(object sender, EventArgs e)
        {
            CalcularPrecoCusto();
        }

        private void txtPercentualIcmsCompra_EditValueChanged(object sender, EventArgs e)
        {
            CalcularPrecoCusto();
        }

        private void txtFreteCompra_EditValueChanged(object sender, EventArgs e)
        {
            CalcularPrecoCusto();
        }

        //private void txtPrecoCompra_EditValueChanged(object sender, EventArgs e)
        //{
        //    CalcularPrecoCusto();
        //}

        private void txtPrecoCompra_Leave(object sender, EventArgs e)
        {
            CalcularPrecoCusto();
        }

        private void txtPrecoVenda_EditValueChanged(object sender, EventArgs e)
        {
            //double margemDemonstracao = txtValorVendaDemonstracao.Text.ToDouble() / (double)txtPrecoCompra.Text.ToDouble();

            //if (margemDemonstracao.ToDoubleNullabel() != null)
            //{
            //    margemDemonstracao = margemDemonstracao * 100;

            //    txtPercentualMargemDemonstracao.Text = margemDemonstracao.ToString("0.00") + "%";
            //}
            //else
            //{
            //    txtPercentualMargemDemonstracao.Text = string.Empty;
            //}
        }

        private void txtMarkup_EditValueChanged(object sender, EventArgs e)
        {
            CalculaMarkup();
        }

        private void chkEhPromocao_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEhPromocao.Checked)
            {
                txtValorPromocao.Enabled = true;
            }
            else
            {
                txtValorPromocao.Enabled = false;
            }
        }

        private void txtPercentualLucro_EditValueChanged(object sender, EventArgs e)
        {
            CalcularPrecoDeVendaComPercentualDeLucro();
        }

        private void txtPercentuaisVenda_EditValueChanged(object sender, EventArgs e)
        {
            CalcularPrecoDeVendaComPercentualDeLucro();
        }

        #endregion

        #region " OPERAÇÕES BÁSICAS "

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (VerifiqueCodigoProdutoFornecedor()) return;

            if (VerifiqueGrupoTributacao()) return;

            Action actionSalvar = () =>
            {
                var produto = RetorneProdutoEmEdicao();

                ServicoProduto servicoProduto = new ServicoProduto();

                if (string.IsNullOrEmpty(txtId.Text))
                {
                    servicoProduto.Cadastre(produto);
                }
                else
                {
                    servicoProduto.Atualize(produto);
                }

                txtId.Text = produto.Id.ToString();
                PesquisePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void FormCadastroDeProduto_Load(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void txtSoNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpe();
        }

        private void btnParametros_Click(object sender, EventArgs e)
        {
            ServicoParametros servicoParametros = new ServicoParametros();
            var parametros = servicoParametros.ConsulteParametros();

            txtPercentualDespesasFixasVenda.Text = parametros.ParametrosFinanceiro.PercentualDespesasFixas.ToString("0.00");
            txtPercentualDespesasVariaveis.Text = parametros.ParametrosFinanceiro.PercentualDespesasVariaveis.ToString("0.00");
            txtPercentualImpostos.Text = parametros.ParametrosFinanceiro.PercentualImpostos.ToString("0.00");
            txtPercentualOutrasDespesasVenda.Text = parametros.ParametrosFinanceiro.PercentualOutrasDespesas.ToString("0.00");
            txtPercentualFreteVenda.Text = parametros.ParametrosFinanceiro.PercentualFrete.ToString("0.00");
            txtPercentualComissoesVenda.Text = parametros.ParametrosFinanceiro.PercentualComissoes.ToString("0.00");
            txtPercentualLucro.Text = parametros.ParametrosFinanceiro.PercentualLucro.ToString("0.00");
        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja duplicar este produto?\n\nAo duplicar as alterações no produto atual serão perdidas, deseja continuar?", "Duplicar produto", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                var produtoOriginal = RetorneProdutoEmEdicao();

                produtoOriginal.ListaFornecedores.Clear();

                produtoOriginal.Id = 0;

                PreenchaCamposProduto(produtoOriginal);

                txtId.Text = string.Empty;
                txtCodigoDeBarras.Focus();
                txtId.Enabled = true;
            }
        }

        #endregion

        #region " MOVIMENTAÇOES "

        private void btnAtualizarMovimentacoes_Click(object sender, EventArgs e)
        {
            PesquiseMovimentacoesProdutos();
        }

        #endregion

        #region " FORNECEDORES "

        private void txtIdFornecedor_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdFornecedor.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var fornecedor = servicoPessoa.ConsulteFornecedorAtivo(txtIdFornecedor.Text.ToInt());

                PreenchaFornecedor(fornecedor, true);
            }
            else
            {
                PreenchaFornecedor(null);
            }
        }

        private void btnPesquisaFornecedor_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var fornecedor = formPessoaPesquisa.PesquisePessoaFornecedoraAtiva();

            if (fornecedor != null)
            {
                PreenchaFornecedor(fornecedor);
            }
        }

        private void btnInserirFornecedor_Click(object sender, EventArgs e)
        {
            IncluaOuAtualizeFornecedor();
        }

        private void btnCancelarFornecedor_Click(object sender, EventArgs e)
        {
            LimpeCamposFornecedor();
        }

        private void btnExcluirFornecedor_Click(object sender, EventArgs e)
        {
            ExcluaFornecedor();
        }

        private void gcFornecedores_DoubleClick(object sender, EventArgs e)
        {
            EditeFornecedorDoGrid();
        }

        #endregion

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " EDIÇÃO DO PRODUTO "

        public void EditeProduto(Produto produto)
        {
            if (produto != null)
            {
                txtId.Enabled = false;

                PreenchaCamposProduto(produto);

                CalcularPrecoCusto();
                
                this.ActiveControl = txtCodigoDeBarras;

                this.Show();
            }

            PreenchaGridMovimentacoesProdutos(null);
        }

        private void PreenchaCamposProduto(Produto produto)
        {
            _carregarGrupos = false;
            _carregarSubGrupos = false;

            txtId.Text = produto.Id.ToString();

            _listaFornecedoresProdutos = produto.ListaFornecedores.ToList();
            PreenchaGridFornecedores();

            PreenchaCamposDadosGerais(produto.DadosGerais);
            PreenchaCamposGuiaPrincipal(produto.Principal);
            PreenchaCamposGuiaContabilFiscal(produto.ContabilFiscal);
            PreenchaCamposGuiaFormacaoPreco(produto);
            PreenchaCamposGuiaVestuario(produto.Vestuario);

            PreenchaCboGrupos();
        }

        private void PreenchaCamposDadosGerais(DadosGeraisProduto dadosGerais)
        {
            if (dadosGerais != null)
            {
                cboStatus.EditValue = dadosGerais.Status != null ? dadosGerais.Status : "A";
                cboUnidades.EditValue = dadosGerais.Unidade != null ? (int?)dadosGerais.Unidade.Id : null;
                txtDescricao.Text = dadosGerais.Descricao;
                txtCodigoDeBarras.Text = dadosGerais.CodigoDeBarras;
                txtDataCadastro.Text = dadosGerais.DataCadastro > DateTime.MinValue ? dadosGerais.DataCadastro.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy");
                chkProdutoVendaFracionada.Checked = dadosGerais.PermiteVendaFracionada;

                if (dadosGerais.Foto.ToInt() == 0)
                {
                    picFoto.Image = Properties.Resources.produtos;
                }
                else
                {
                    picFoto.Image = TratamentoDeImagens.ConvertByteToImagem(dadosGerais.Foto).Image;
                }
            }
            else
            {
                cboStatus.EditValue = "A";
                cboUnidades.EditValue = null;
                txtDescricao.Text = string.Empty;
                txtCodigoDeBarras.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                chkProdutoVendaFracionada.Checked = false;
            }
        }

        private void PreenchaCamposGuiaPrincipal(PrincipalProduto principalProduto)
        {
            if (principalProduto != null)
            {
                txtCodigoFabricante.Text = principalProduto.CodigoFabricante;

                txtPesoBruto.Text = principalProduto.PesoBruto != null ? principalProduto.PesoBruto.Value.ToString("0.00") : string.Empty;
                txtPesoLiquido.Text = principalProduto.PesoLiquido != null ? principalProduto.PesoLiquido.Value.ToString("0.00") : string.Empty;

                cboMarcas.EditValue = principalProduto.Marca != null ? (int?)principalProduto.Marca.Id : null;
                cboFabricantes.EditValue = principalProduto.Fabricante != null ? (int?)principalProduto.Fabricante.Id : null;
                cboCategorias.EditValue = principalProduto.Categoria != null ? (int?)principalProduto.Categoria.Id : null;
                cboGrupos.EditValue = principalProduto.Grupo != null ? (int?)principalProduto.Grupo.Id : null;
                cboSubGrupos.EditValue = principalProduto.SubGrupo != null ? (int?)principalProduto.SubGrupo.Id : null;

                txtQtdMinimaEstoque.Text = principalProduto.QuantidadeMinima.ToString();
                txtQtdMaximaEstoque.Text = principalProduto.QuantidadeMaxima.ToString();

                PreenchaProdutoSimilar(principalProduto.ProdutoSimilar);

                txtLocacao.Text = principalProduto.Locacao;

                txtObservacoes.Text = principalProduto.Observacao;
            }
            else
            {
                txtCodigoFabricante.Text = string.Empty;
                txtCodigoFornecedor.Text = string.Empty;

                txtPesoBruto.Text = string.Empty;
                txtPesoLiquido.Text = string.Empty;

                cboMarcas.EditValue = null;
                cboFabricantes.EditValue = null;
                cboCategorias.EditValue = null;
                cboGrupos.EditValue = null;
                cboSubGrupos.EditValue = null;

                txtQtdMinimaEstoque.Text = string.Empty;
                txtQtdMaximaEstoque.Text = string.Empty;

                PreenchaProdutoSimilar(null);
                PreenchaFornecedor(null);

                txtObservacoes.Text = string.Empty;
            }
        }

        private void PreenchaCamposGuiaVestuario(VestuarioProduto vestuarioProduto)
        {
            if (vestuarioProduto != null)
            {
                txtColecao.Text = vestuarioProduto.Colecao;
                txtComposicao.Text = vestuarioProduto.Composicao;

                if (vestuarioProduto.Tamanho != null)
                {
                    cboTamanhos.EditValue = vestuarioProduto.Tamanho.Id;
                }
                else
                {
                    cboTamanhos.EditValue = null;
                }

                if (vestuarioProduto.Cor != null)
                {
                    cboCores.EditValue = vestuarioProduto.Cor.Id;
                }
                else
                {
                    cboCores.EditValue = null;
                }

                txtDescricaoDetalhada.Text = vestuarioProduto.DescricaoDetalhada;
                txtMaterialTecido.Text = vestuarioProduto.MaterialTecido;
                txtModelo.Text = vestuarioProduto.Modelo;
                txtReferencia.Text = vestuarioProduto.Referencia;
                cboSexo.EditValue = vestuarioProduto.SexoProduto;
            }
            else
            {
                txtColecao.Text = string.Empty;
                txtComposicao.Text = string.Empty;

                cboCores.EditValue = null;

                txtDescricaoDetalhada.Text = string.Empty;
                txtMaterialTecido.Text = string.Empty;
                txtModelo.Text = string.Empty;
                txtReferencia.Text = string.Empty;
                cboSexo.EditValue = null;
                cboTamanhos.EditValue = null;
            }
        }

        private void PreenchaCamposGuiaContabilFiscal(ContabilFiscalProduto contabilFiscalProduto)
        {
            if (contabilFiscalProduto != null)
            {
                PreenchaNcm(contabilFiscalProduto.Ncm);
                cboNaturezaProduto.EditValue = contabilFiscalProduto.NaturezaProduto;

                cboSituacaoTributaria.EditValue = contabilFiscalProduto.SituacaoTributariaProduto;
                txtIcms.Text = contabilFiscalProduto.Icms != null ? contabilFiscalProduto.Icms.Value.ToString("0.00") : string.Empty;

                cboOrigemProduto.EditValue = contabilFiscalProduto.OrigemProduto;

                txtCodigoGtin.Text = contabilFiscalProduto.CodigoGtin;

                PreenchaGrupoTributacaoIcms(contabilFiscalProduto.GrupoTributacaoIcms);

                PreenchaGrupoTributacaoFederal(contabilFiscalProduto.GrupoTributacaoFederal);
            }
            else
            {
                PreenchaNcm(null);
                cboNaturezaProduto.EditValue = EnumNaturezaProduto.TERCEIROS;

                cboSituacaoTributaria.EditValue = null;
                txtIcms.Text = string.Empty;

                cboOrigemProduto.EditValue = EnumOrigem.NACIONALEXCETOASINDICADASNOSCODIGOS3E5;

                txtCodigoGtin.Text = string.Empty;
                PreenchaGrupoTributacaoIcms(null);
            }
        }
        private void carregaconexao()
        {
            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoesII = JsonConvert.DeserializeObject<ConexoesJson>(conexoesStringII);

            var item = conexoesII.Conexoes[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexaoII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }

            }

        }
        private void ConsultaReserva(int CodProduto)
        {

            carregaconexao();


            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                var sql = "";

                sql = "SELECT sum(PEDITEM_QUANTIDADE) as Reserva FROM pedidosvendasitens where peditem_produto_id = " + CodProduto + " And PEDITEM_RESERVA > 0 ";



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                txtreserva.Text = "0.0000";
                while (returnValue.Read())
                {
                    txtreserva.Text = returnValue["Reserva"].ToString();
                }
            }
        }
        private void PreenchaCamposGuiaFormacaoPreco(Produto produto)
        {
            PreenchaInformacoesUltimaCompra(produto);


            ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
            var ItemTransferencia = servicoItemTransferencia.ConsulteProduto(produto.Id);

            double quantidadeestoque = 0;
            double quantidadesubestoque = 0;

            if (produto != null && produto.FormacaoPreco != null)
            {
                var formacaoPrecoProduto = produto.FormacaoPreco;

                txtPrecoCompra.Text = formacaoPrecoProduto.PrecoCompra != null ? formacaoPrecoProduto.PrecoCompra.Value.ToString("0.00") : string.Empty;
                txtFreteCompra.Text = formacaoPrecoProduto.ValorFreteCompra != null ? formacaoPrecoProduto.ValorFreteCompra.Value.ToString("0.00") : string.Empty;
                txtPercentualIcmsCompra.Text = formacaoPrecoProduto.PercentualIcmsCompra != null ? formacaoPrecoProduto.PercentualIcmsCompra.Value.ToString("0.00") : string.Empty;
                txtPercentualIpiCompra.Text = formacaoPrecoProduto.PercentualIpiCompra != null ? formacaoPrecoProduto.PercentualIpiCompra.Value.ToString("0.00") : string.Empty;
                txtPercentualIcmsStCompra.Text = formacaoPrecoProduto.PercentualIcmsSTCompra != null ? formacaoPrecoProduto.PercentualIcmsSTCompra.Value.ToString("0.00") : string.Empty;
                txtPercentualReducaoIcmsCompra.Text = formacaoPrecoProduto.PercentualReducaoIcmsCompra != null ? formacaoPrecoProduto.PercentualReducaoIcmsCompra.Value.ToString("0.00") : string.Empty;

                var precoCusto = _servicoProduto.CalculePrecoCusto(formacaoPrecoProduto.PrecoCompra.GetValueOrDefault(),
                                                                                            formacaoPrecoProduto.ValorFreteCompra.GetValueOrDefault(),
                                                                                            formacaoPrecoProduto.PercentualIcmsCompra.GetValueOrDefault(),
                                                                                            formacaoPrecoProduto.PercentualIpiCompra.GetValueOrDefault(),
                                                                                            0,
                                                                                            0);

                txtPrecoCusto.Text = precoCusto.ToString("0.00");

                txtPercentualDespesasFixasVenda.Text = formacaoPrecoProduto.PercentualDespesasFixasVenda != null ? formacaoPrecoProduto.PercentualDespesasFixasVenda.Value.ToString("0.00") : string.Empty;
                txtPercentualDespesasVariaveis.Text = formacaoPrecoProduto.PercentualDespesasVariaveisVenda != null ? formacaoPrecoProduto.PercentualDespesasVariaveisVenda.Value.ToString("0.00") : string.Empty;
                txtPercentualImpostos.Text = formacaoPrecoProduto.PercentualIcmsSimplesVenda != null ? formacaoPrecoProduto.PercentualIcmsSimplesVenda.Value.ToString("0.00") : string.Empty;
                txtPercentualOutrasDespesasVenda.Text = formacaoPrecoProduto.PercentualOutrasDespesasVenda != null ? formacaoPrecoProduto.PercentualOutrasDespesasVenda.Value.ToString("0.00") : string.Empty;
                txtPercentualFreteVenda.Text = formacaoPrecoProduto.PercentualFreteVenda != null ? formacaoPrecoProduto.PercentualFreteVenda.Value.ToString("0.00") : string.Empty;
                txtPercentualComissoesVenda.Text = formacaoPrecoProduto.PercentualComissoesVenda != null ? formacaoPrecoProduto.PercentualComissoesVenda.Value.ToString("0.00") : string.Empty;

                //Valores de Serviços
                txtEntrega.Text = formacaoPrecoProduto.ValorEntrega != null ? formacaoPrecoProduto.ValorEntrega.Value.ToString("0.00") : string.Empty;
                txtEntregaAposHorario.Text = formacaoPrecoProduto.ValorEntregaAposHorario != null ? formacaoPrecoProduto.ValorEntregaAposHorario.Value.ToString("0.00") : string.Empty;
                txtInstalacao.Text = formacaoPrecoProduto.ValorInstalacao != null ? formacaoPrecoProduto.ValorInstalacao.Value.ToString("0.00") : string.Empty;
                txtInstalacaoAposHorario.Text = formacaoPrecoProduto.ValorInstalacaoAposHorario != null ? formacaoPrecoProduto.ValorInstalacaoAposHorario.Value.ToString("0.00") : string.Empty;
                txtInstalacaoOutrasCidades.Text = formacaoPrecoProduto.ValorInstalacaoOutrasCidades != null ? formacaoPrecoProduto.ValorInstalacaoOutrasCidades.Value.ToString("0.00") : string.Empty;
                txtDeslocamentoEGarantia.Text = formacaoPrecoProduto.ValorDeslocamentoEGarantia != null ? formacaoPrecoProduto.ValorDeslocamentoEGarantia.Value.ToString("0.00") : string.Empty;
                
                txtPercentualLucro.Text = formacaoPrecoProduto.PercentualLucro != null ? formacaoPrecoProduto.PercentualLucro.Value.ToString("0.00") : string.Empty;
                txtMarkup.Text = formacaoPrecoProduto.Markup != null ? formacaoPrecoProduto.Markup.Value.ToString("0.00") : string.Empty;
                txtValorVenda.Text = formacaoPrecoProduto.ValorVenda != null ? formacaoPrecoProduto.ValorVenda.Value.ToString("0.00") : string.Empty;
                txtValorVendaAtual.Text = formacaoPrecoProduto.ValorVenda.GetValueOrDefault().ToString("0.00");
                txtValorPromocao.Text = formacaoPrecoProduto.ValorPromocao != null ? formacaoPrecoProduto.ValorPromocao.Value.ToString("0.00") : string.Empty;
                chkEhPromocao.Checked = formacaoPrecoProduto.EhPromocao != null ? formacaoPrecoProduto.EhPromocao.GetValueOrDefault() : false;
                txtPercentualDescontoMaximo.Text = formacaoPrecoProduto.PercentualDescontoMaximo != null ? formacaoPrecoProduto.PercentualDescontoMaximo.Value.ToString("0.00") : string.Empty;
                if(ItemTransferencia != null)
                {
                    foreach (var itemproduto in ItemTransferencia)
                    {
                        quantidadesubestoque += itemproduto.QuantidadeEstoque;
                    }

                    quantidadeestoque = formacaoPrecoProduto.Estoque - quantidadesubestoque;
                    if (quantidadeestoque < 0 )
                    {
                        quantidadeestoque = 0;
                    }
                        //if(formacaoPrecoProduto.Estoque < quantidadesubestoque)
                        //{
                        //    txtQtdSub.Text = formacaoPrecoProduto.Estoque.ToString("#0.0000");
                        //}
                        //else
                        //{
                            txtQtdSub.Text = quantidadesubestoque.ToString("#0.0000");
                        //}

                    
                }
                else
                {
                    quantidadeestoque = formacaoPrecoProduto.Estoque;
                    txtQtdSub.Text = "0.0000";
                }

                txtEstoqueTotal.Text = formacaoPrecoProduto.Estoque.ToString("#0.0000");
                txtQtdEstoque.Text = quantidadeestoque.ToString("#0.0000");
                ConsultaReserva(txtId.Text.ToInt());
                if (txtreserva.Text == "")
                {
                    txtreserva.Text = "0.0000";
                }
                //if (formacaoPrecoProduto.EstoqueReservado < 0)
                //{
                //    txtreserva.Text = "0.0000";
                //}
                //else
                //{
                //    txtreserva.Text = formacaoPrecoProduto.EstoqueReservado.ToString("#0.0000");
                //}

            }
            else
            {
                txtPrecoCompra.Text = string.Empty;
                txtFreteCompra.Text = string.Empty;
                txtPercentualIcmsCompra.Text = string.Empty;
                txtPercentualIpiCompra.Text = string.Empty;
                txtPercentualIcmsStCompra.Text = string.Empty;
                txtPercentualReducaoIcmsCompra.Text = string.Empty;

                txtPercentualDespesasFixasVenda.Text = string.Empty;
                txtPercentualDespesasVariaveis.Text = string.Empty;
                txtPercentualImpostos.Text = string.Empty;
                txtPercentualOutrasDespesasVenda.Text = string.Empty;
                txtPercentualFreteVenda.Text = string.Empty;
                txtPercentualComissoesVenda.Text = string.Empty;

                //Valores de Serviços
                txtEntrega.Text = string.Empty;
                txtEntregaAposHorario.Text = string.Empty;
                txtInstalacao.Text = string.Empty;
                txtInstalacaoAposHorario.Text = string.Empty;
                txtInstalacaoOutrasCidades.Text = string.Empty;
                txtDeslocamentoEGarantia.Text = string.Empty;

                txtPercentualLucro.Text = string.Empty;
                txtMarkup.Text = string.Empty;
                txtValorVenda.Text = string.Empty;
                txtValorVendaAtual.Text = string.Empty;
                txtValorPromocao.Text = string.Empty;
                chkEhPromocao.Checked = false;
                txtPercentualDescontoMaximo.Text = string.Empty;

                txtQtdEstoque.Text = string.Empty;

                txtValorPromocao.Enabled = false;
            }
        }

        private void PreenchaInformacoesUltimaCompra(Produto produto)
        {
            ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

            var itemCompra = servicoEntradaMercadoria.ConsulteUltimaEntradaProduto(produto);

            if (itemCompra != null)
            {
                var movimentacaoEntrada = servicoEntradaMercadoria.Consulte(itemCompra.EntradaMercadoria.Id);


                txtDataUltimaCompra.Text = itemCompra.EntradaMercadoria.DataMovimentacao.GetValueOrDefault().ToString("dd/MM/yyyy");


            }
            else
            {
                txtDataUltimaCompra.Text = string.Empty;

            }
        }

        #endregion

        #region " CRUD "

        private Produto RetorneProdutoEmEdicao()
        {
            _produtoEmEdicao = _produtoEmEdicao ?? new Produto();

            _produtoEmEdicao.Id = txtId.Text.ToInt();

            _produtoEmEdicao.DadosGerais = RetorneDadosGeraisProdutoEmEdicao();
            _produtoEmEdicao.Principal = RetornePrincipalProdutoEmEdicao();
            _produtoEmEdicao.ContabilFiscal = RetorneContabilFiscalProdutoEmEdicao();
            _produtoEmEdicao.FormacaoPreco = RetorneFormacaoPrecoProdutoEmEdicao();
            _produtoEmEdicao.Vestuario = RetorneVestorioProdutoEmEdicao();
            _produtoEmEdicao.ListaFornecedores = _listaFornecedoresProdutos;

            return _produtoEmEdicao;
        }

        private DadosGeraisProduto RetorneDadosGeraisProdutoEmEdicao()
        {
            DadosGeraisProduto dadosGerais = new DadosGeraisProduto();

            dadosGerais.CodigoDeBarras = txtCodigoDeBarras.Text;
            dadosGerais.Descricao = txtDescricao.Text;
            dadosGerais.Status = cboStatus.EditValue.ToString();
            dadosGerais.Unidade = cboUnidades.EditValue != null ? new UnidadeMedida { Id = cboUnidades.EditValue.ToInt() } : null;
            dadosGerais.DataCadastro = txtDataCadastro.Text.ToDate();
            dadosGerais.PermiteVendaFracionada = chkProdutoVendaFracionada.Checked;

            List<Byte> listaBytesFoto = TratamentoDeImagens.ConvertImagemToByte(picFoto).ToList();
            List<Byte> listaBytesFotoPadrao = TratamentoDeImagens.ConvertImagemToByte(Properties.Resources.produtos).ToList();

            bool fotoEhAPadrao = true;

            if (listaBytesFotoPadrao.Count != listaBytesFoto.Count)
            {
                fotoEhAPadrao = false;
            }
            else
            {
                for (int i = 0; i < listaBytesFoto.Count; i++)
                {
                    if (listaBytesFoto[i] != listaBytesFotoPadrao[i])
                    {
                        fotoEhAPadrao = false;

                        break;
                    }
                }
            }

            if (fotoEhAPadrao)
            {
                dadosGerais.Foto = null;
            }

            else
            {
                dadosGerais.Foto = TratamentoDeImagens.ConvertImagemToByte(picFoto);
            }

            return dadosGerais;
        }

        private PrincipalProduto RetornePrincipalProdutoEmEdicao()
        {
            PrincipalProduto principalProduto = new PrincipalProduto();

            principalProduto.CodigoFabricante = txtCodigoFabricante.Text;

            principalProduto.PesoBruto = txtPesoBruto.Text.ToDoubleNullabel();
            principalProduto.PesoLiquido = txtPesoLiquido.Text.ToDoubleNullabel();

            principalProduto.Marca = cboMarcas.EditValue != null ? new Marca { Id = cboMarcas.EditValue.ToInt() } : null;
            principalProduto.Fabricante = cboFabricantes.EditValue != null ? new Fabricante { Id = cboFabricantes.EditValue.ToInt() } : null;
            principalProduto.Categoria = cboCategorias.EditValue != null ? new Categoria { Id = cboCategorias.EditValue.ToInt() } : null;
            principalProduto.Grupo = cboGrupos.EditValue != null ? new Grupo { Id = cboGrupos.EditValue.ToInt() } : null;
            principalProduto.SubGrupo = cboSubGrupos.EditValue != null ? new SubGrupo { Id = (int)cboSubGrupos.EditValue.ToInt() } : null;

            principalProduto.QuantidadeMinima = txtQtdMinimaEstoque.Text.ToDoubleNullabel();
            principalProduto.QuantidadeMaxima = txtQtdMaximaEstoque.Text.ToDoubleNullabel();

            principalProduto.ProdutoSimilar = !string.IsNullOrEmpty(txtIdProdutoSimilar.Text) ? new Produto { Id = txtIdProdutoSimilar.Text.ToInt() } : null;

            principalProduto.Locacao = txtLocacao.Text;

            principalProduto.Observacao = txtObservacoes.Text;

            return principalProduto;
        }

        private VestuarioProduto RetorneVestorioProdutoEmEdicao()
        {
            VestuarioProduto vestuarioProduto = new VestuarioProduto();

            vestuarioProduto.Colecao = txtColecao.Text;
            vestuarioProduto.Composicao = txtComposicao.Text;

            if (cboCores.EditValue != null)
            {
                vestuarioProduto.Cor = new Cor { Id = cboCores.EditValue.ToInt() };
            }

            if (cboTamanhos.EditValue != null)
            {
                vestuarioProduto.Tamanho = new Tamanho { Id = cboTamanhos.EditValue.ToInt() };
            }

            vestuarioProduto.DescricaoDetalhada = txtDescricaoDetalhada.Text;
            vestuarioProduto.MaterialTecido = txtMaterialTecido.Text;
            vestuarioProduto.Modelo = txtModelo.Text;
            vestuarioProduto.Referencia = txtReferencia.Text;
            vestuarioProduto.SexoProduto = (EnumSexoProduto?)cboSexo.EditValue;

            return vestuarioProduto;
        }

        private ContabilFiscalProduto RetorneContabilFiscalProdutoEmEdicao()
        {
            ContabilFiscalProduto contabilFiscalProduto = new ContabilFiscalProduto();

            contabilFiscalProduto.Ncm = !string.IsNullOrEmpty(txtNcmId.Text) ? _ncm : null;
            contabilFiscalProduto.NaturezaProduto = (EnumNaturezaProduto)cboNaturezaProduto.EditValue;

            contabilFiscalProduto.Icms = txtIcms.Text.ToDoubleNullabel();
            contabilFiscalProduto.SituacaoTributariaProduto = cboSituacaoTributaria.EditValue != null ? (EnumSituacaoTributariaProduto?)cboSituacaoTributaria.EditValue : _ncm != null ? _ncm.Cest != null ? EnumSituacaoTributariaProduto.SUBSTITUICAOTRIBUTARIA : (EnumSituacaoTributariaProduto?)null : (EnumSituacaoTributariaProduto?)cboSituacaoTributaria.EditValue;
            contabilFiscalProduto.CodigoGtin = txtCodigoGtin.Text;
            contabilFiscalProduto.OrigemProduto = (EnumOrigem)cboOrigemProduto.EditValue;

            contabilFiscalProduto.GrupoTributacaoIcms = !string.IsNullOrWhiteSpace(txtIdGrupoTributacaoIcms.Text) ? new GrupoTributacaoIcms { Id = txtIdGrupoTributacaoIcms.Text.ToInt() } : null;

            contabilFiscalProduto.GrupoTributacaoFederal = !string.IsNullOrWhiteSpace(txtIdGrupoTributacaoFederal.Text) ? new GrupoTributacaoFederal { Id = txtIdGrupoTributacaoFederal.Text.ToInt() } : null;


            return contabilFiscalProduto;
        }

        private FormacaoPrecoProduto RetorneFormacaoPrecoProdutoEmEdicao()
        {
            FormacaoPrecoProduto formacaoPrecoProduto = new FormacaoPrecoProduto();

            formacaoPrecoProduto.PrecoCompra = txtPrecoCompra.Text.ToDoubleNullabel();
            formacaoPrecoProduto.ValorFreteCompra = txtFreteCompra.Text.ToDoubleNullabel();
            formacaoPrecoProduto.PercentualIcmsCompra = txtPercentualIcmsCompra.Text.ToDoubleNullabel();
            formacaoPrecoProduto.PercentualIpiCompra = txtPercentualIpiCompra.Text.ToDoubleNullabel();
            formacaoPrecoProduto.PercentualIcmsSTCompra = txtPercentualIcmsStCompra.Text.ToDoubleNullabel();
            formacaoPrecoProduto.PercentualReducaoIcmsCompra = txtPercentualReducaoIcmsCompra.Text.ToDoubleNullabel();

            formacaoPrecoProduto.PercentualDespesasFixasVenda = txtPercentualDespesasFixasVenda.Text.ToDoubleNullabel();
            formacaoPrecoProduto.PercentualDespesasVariaveisVenda = txtPercentualDespesasVariaveis.Text.ToDoubleNullabel();
            formacaoPrecoProduto.PercentualIcmsSimplesVenda = txtPercentualImpostos.Text.ToDoubleNullabel();
            formacaoPrecoProduto.PercentualOutrasDespesasVenda = txtPercentualOutrasDespesasVenda.Text.ToDoubleNullabel();
            formacaoPrecoProduto.PercentualFreteVenda = txtPercentualFreteVenda.Text.ToDoubleNullabel();
            formacaoPrecoProduto.PercentualComissoesVenda = txtPercentualComissoesVenda.Text.ToDoubleNullabel();

            //Valores de Serviços
            formacaoPrecoProduto.ValorEntrega = txtEntrega.Text.ToDoubleNullabel();
            formacaoPrecoProduto.ValorEntregaAposHorario = txtEntregaAposHorario.Text.ToDoubleNullabel();
            formacaoPrecoProduto.ValorInstalacao = txtInstalacao.Text.ToDoubleNullabel();
            formacaoPrecoProduto.ValorInstalacaoAposHorario = txtInstalacaoAposHorario.Text.ToDoubleNullabel();
            formacaoPrecoProduto.ValorInstalacaoOutrasCidades = txtInstalacaoOutrasCidades.Text.ToDoubleNullabel();
            formacaoPrecoProduto.ValorDeslocamentoEGarantia = txtDeslocamentoEGarantia.Text.ToDoubleNullabel();

            formacaoPrecoProduto.PercentualLucro = txtPercentualLucro.Text.ToDoubleNullabel();
            formacaoPrecoProduto.Markup = txtMarkup.Text.ToDoubleNullabel();
            formacaoPrecoProduto.ValorVenda = txtValorVenda.Text.ToDoubleNullabel();
            formacaoPrecoProduto.ValorPromocao = txtValorPromocao.Text.ToDoubleNullabel();
            formacaoPrecoProduto.EhPromocao = chkEhPromocao.Checked;
            formacaoPrecoProduto.PercentualDescontoMaximo = txtPercentualDescontoMaximo.Text.ToDoubleNullabel();
            formacaoPrecoProduto.Estoque = txtQtdEstoque.Text.ToDouble();

            return formacaoPrecoProduto;
        }

        #endregion

        #region " PREENCHIMENTO DE CAMPOS "

        private void PreenchaCboUnidades()
        {
            ServicoUnidadeMedida servicoUnidadeMedida = new ServicoUnidadeMedida();

            var unidades = servicoUnidadeMedida.ConsulteListaAtiva();

            unidades.Insert(0, null);

            cboUnidades.Properties.DataSource = unidades;
            cboUnidades.Properties.DisplayMember = "Descricao";
            cboUnidades.Properties.ValueMember = "Id";
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
            if (!_carregarGrupos)
            {
                _carregarGrupos = true;

                return;
            }

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
            if (!_carregarSubGrupos)
            {
                _carregarSubGrupos = true;

                return;
            }

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

        private void PreenchaCboTamanhos()
        {
            ServicoTamanho servicoTamanho = new ServicoTamanho();

            var tamanhos = servicoTamanho.ConsulteListaAtiva();

            tamanhos.Insert(0, null);

            cboTamanhos.Properties.DataSource = tamanhos;
            cboTamanhos.Properties.DisplayMember = "Descricao";
            cboTamanhos.Properties.ValueMember = "Id";

            if (cboTamanhos.EditValue != null)
            {
                if (!tamanhos.Exists(tamanho => tamanho != null && tamanho.Id == cboTamanhos.EditValue.ToInt()))
                {
                    cboTamanhos.EditValue = null;
                }
            }
        }

        private void PreenchaCboCores()
        {
            ServicoCor servicoCor = new ServicoCor();

            var cores = servicoCor.ConsulteListaAtiva();

            cores.Insert(0, null);

            cboCores.Properties.DataSource = cores;
            cboCores.Properties.DisplayMember = "Descricao";
            cboCores.Properties.ValueMember = "Id";

            if (cboCores.EditValue != null)
            {
                if (!cores.Exists(cor => cor != null && cor.Id == cboCores.EditValue.ToInt()))
                {
                    cboCores.EditValue = null;
                }
            }
        }

        private void PreenchaCboSexo()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumSexoProduto>();
            lista.Insert(0, null);

            cboSexo.Properties.DataSource = lista;
            cboSexo.Properties.DisplayMember = "Descricao";
            cboSexo.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboNaturezaProduto()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumNaturezaProduto>();

            cboNaturezaProduto.Properties.DataSource = lista;
            cboNaturezaProduto.Properties.DisplayMember = "Descricao";
            cboNaturezaProduto.Properties.ValueMember = "Valor";

            cboNaturezaProduto.EditValue = EnumNaturezaProduto.TERCEIROS;
        }

        private void PreenchaCboSituacaoTributariaProdutro()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumSituacaoTributariaProduto>();

            lista.Insert(0, null);

            cboSituacaoTributaria.Properties.DataSource = lista;
            cboSituacaoTributaria.Properties.DisplayMember = "Descricao";
            cboSituacaoTributaria.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboOrigemProduto()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumOrigem>();

            cboOrigemProduto.Properties.DataSource = lista;
            cboOrigemProduto.Properties.DisplayMember = "Descricao";
            cboOrigemProduto.Properties.ValueMember = "Valor";

            cboOrigemProduto.EditValue = EnumOrigem.NACIONALEXCETOASINDICADASNOSCODIGOS3E5;
        }

        private void PreenchaCboTipoMovimentacao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoMovimentacao>();

            lista.RemoveAt(2);

            lista.Insert(0, null);

            cboTipoMovimentacao.Properties.DataSource = lista;
            cboTipoMovimentacao.Properties.DisplayMember = "Descricao";
            cboTipoMovimentacao.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboOrigemMovimentacao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumOrigemMovimentacao>();

            lista.Insert(0, null);

            cboOrigemMovimentacao.Properties.DataSource = lista;
            cboOrigemMovimentacao.Properties.DisplayMember = "Descricao";
            cboOrigemMovimentacao.Properties.ValueMember = "Valor";
        }

        private void PesquiseProduto()
        {
            string chavePesquisa = "";

            if (txtDescricao.Text != string.Empty)
                chavePesquisa = txtDescricao.Text.Substring(0,5).Trim();

            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProduto(chavePesquisa);

            if (produto != null)
            {
                EditeProduto(produto);
            }
        }

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = "A";
        }

        private void PreenchaNcm(Ncm ncm, bool manterFocusCasoNaoTenhaNcm = true)
        {
            if (ncm != null)
            {
                txtNcmId.Text = ncm.CodigoNcm.ToString();
                txtNcmDescricao.Text = ncm.Descricao;
            }
            else
            {
                txtNcmId.Text = string.Empty;
                txtNcmDescricao.Text = string.Empty;

                if (manterFocusCasoNaoTenhaNcm)
                {
                    txtNcmId.Focus();
                }
            }
            _ncm = ncm;
            cboSituacaoTributaria.EditValue = _ncm != null ? _ncm.Cest != null ? EnumSituacaoTributariaProduto.SUBSTITUICAOTRIBUTARIA : (EnumSituacaoTributariaProduto?)null : (EnumSituacaoTributariaProduto?)null;
        }

        private void PreenchaProdutoSimilar(Produto produtoSimular, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (produtoSimular != null)
            {
                txtIdProdutoSimilar.Text = produtoSimular.Id.ToString();
                txtDescricaoProdutoSimilar.Text = produtoSimular.DadosGerais.Descricao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Produto similar nao encontrado!", "Produto similar não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdProdutoSimilar.Focus();
                }

                txtIdProdutoSimilar.Text = string.Empty;
                txtDescricaoProdutoSimilar.Text = string.Empty;
            }
        }

        private void PreenchaGrupoTributacaoIcms(GrupoTributacaoIcms grupoTributacaoIcms, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (grupoTributacaoIcms != null)
            {
                txtIdGrupoTributacaoIcms.Text = grupoTributacaoIcms.Id.ToString();
                txtDescricaoGrupoTributacaoIcms.Text = grupoTributacaoIcms.Descricao;
                txtNaturezaProdutoGrupoTributacaoIcms.Text = grupoTributacaoIcms.NaturezaProduto.Descricao();
            }
            else
            {
                txtIdGrupoTributacaoIcms.Text = string.Empty;
                txtDescricaoGrupoTributacaoIcms.Text = string.Empty;
                txtNaturezaProdutoGrupoTributacaoIcms.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Grupo de Tributação nao encontrado!", "Grupo de Tributação não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtIdGrupoTributacaoIcms.Focus();
                }
            }
        }

            private void PreenchaGrupoTributacaoFederal(GrupoTributacaoFederal grupoTributacaoFederal, bool exibirMensagemDeNaoEncontrado = false)
            {
                if (grupoTributacaoFederal != null)
                {
                    txtIdGrupoTributacaoFederal.Text = grupoTributacaoFederal.Id.ToString();
                    txtDescricaoGrupoTributacaoFederal.Text = grupoTributacaoFederal.Descricao;
                    txtNaturezaProdutoGrupoTributacaoFederal.Text = grupoTributacaoFederal.NaturezaProduto.Descricao();
                }
                else
                {
                    txtIdGrupoTributacaoFederal.Text = string.Empty;
                    txtDescricaoGrupoTributacaoFederal.Text = string.Empty;
                    txtNaturezaProdutoGrupoTributacaoFederal.Text = string.Empty;

                    if (exibirMensagemDeNaoEncontrado)
                    {
                        MessageBox.Show("Grupo de Tributação nao encontrado!", "Grupo de Tributação não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtIdGrupoTributacaoFederal.Focus();
                    }
                }
            }

        #endregion

        #region " FORMAÇÃO DE PREÇO "

        private void VerificaParamentosDoItem()
        {
            if(_parametros.ParametrosCadastros.AbrirQuantEstoqueItens)
            {
                txtQtdEstoque.Properties.ReadOnly = false;
                txtQtdEstoque.TabStop =true;
            }
        }

        private void CalculaMarkup()
        {
            if(!_parametros.ParametrosCadastros.ValorVendaManual)
            {
                //Preço de venda Markup foi alterado para ((ValorVendaSugerido * Markup) + ValorVendaSugerido
                var precoVenda = _servicoProduto.CalculePrecoVenda(txtValorVendaSugerido.Text.ToDouble(), txtMarkup.Text.ToDouble()) + txtValorVendaSugerido.Text.ToDouble();
                if (precoVenda > 0)
                    txtValorVenda.Text = precoVenda.ToString("0.00");
            }            
        }

        private void CalcularPrecoCusto()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            
            var precoCompra = txtPrecoCompra.Text.ToDouble();
            var freteCompra = txtFreteCompra.Text.ToDouble();
            var percentualIcmsCompra = txtPercentualIcmsCompra.Text.ToDouble();
            var percentualIpiCompra = txtPercentualIpiCompra.Text.ToDouble();
            var percentualIcmsStCompra = txtPercentualIcmsStCompra.Text.ToDouble();
            var percentualReducaoIcmsCompra = txtPercentualReducaoIcmsCompra.Text.ToDouble();


            if (empresa.DadosEmpresa.CodigoRegimeTributario == EnumCodigoRegimeTributario.REGIMENORMAL)
            {
                if(empresa.DadosEmpresa.Cnae.Descricao.Contains("Comercio"))
                { 
                    var precoCusto = _servicoProduto.CalculePrecoCusto(precoCompra,
                                                                                                freteCompra,
                                                                                                percentualIcmsCompra, //é informado para retirar, por isso sem sinal, ou seja, (+). Com sinal negativo (-) acrescenta.
                                                                                                percentualIpiCompra,
                                                                                                0,
                                                                                                0);

                    txtPrecoCusto.Text = precoCusto.ToString("0.00");
                }
                else
                {
                    var precoCusto = _servicoProduto.CalculePrecoCusto(precoCompra,
                                                                                               freteCompra,
                                                                                               percentualIcmsCompra, //é informado para retirar, por isso sem sinal, ou seja, (+). Com sinal negativo (-) acrescenta.
                                                                                               -percentualIpiCompra, 
                                                                                               0,
                                                                                               0);

                    txtPrecoCusto.Text = precoCusto.ToString("0.00");
                }
            }
            else
            {
                var precoCusto = _servicoProduto.CalculePrecoCusto(precoCompra,
                                                                                            freteCompra,
                                                                                            0, // No "Regime Simples" não soma e nem subrai o icms de compra.
                                                                                            percentualIpiCompra,
                                                                                            0,
                                                                                            0);

                txtPrecoCusto.Text = precoCusto.ToString("0.00");
            }

            CalcularPrecoDeVendaComPercentualDeLucro();
            CalculaMarkup();
        }

        private void CalcularPrecoDeVendaComPercentualDeLucro()
        {
            var percentualDespesasFixas = txtPercentualDespesasFixasVenda.Text.ToDouble();
            var percentualDespesasVariaveis = txtPercentualDespesasVariaveis.Text.ToDouble();
            var percentualImpostos = txtPercentualImpostos.Text.ToDouble();
            var percentualOutrasDespesas = txtPercentualOutrasDespesasVenda.Text.ToDouble();
            var percentualFrete = txtPercentualFreteVenda.Text.ToDouble();
            var percentualComissoes = txtPercentualComissoesVenda.Text.ToDouble();
            var percentualLucro = txtPercentualLucro.Text.ToDouble();

            var valorVenda = _servicoProduto.CalculePrecoVenda(txtPrecoCusto.Text.ToDouble(),
                                                                                          percentualDespesasFixas,
                                                                                          percentualDespesasVariaveis,
                                                                                          percentualImpostos,
                                                                                          percentualOutrasDespesas,
                                                                                          percentualFrete,
                                                                                          percentualComissoes,
                                                                                          percentualLucro);
            if (valorVenda > 0)
            {
                if(!_parametros.ParametrosCadastros.ValorVendaManual)
                    txtValorVenda.Text = valorVenda.ToString("0.00");
            }
                txtValorVendaSugerido.Text = valorVenda.ToString("0.00");
                txtValorVendaDemonstracao.Text = valorVenda.ToString("0.00");
            
        }

        #endregion

        private void Limpe()
        {
            Produto produto = new Produto();
            EditeProduto(produto);

            txtId.Text = string.Empty;
            txtId.Enabled = true;

            this.ActiveControl = txtId;

            _produtoEmEdicao = null;

            tbpMovimentacoes.Size = new Size(0, 1);
        }

        private void TrateUsuarioContemPermissaoAtalhos()
        {
            TrateUsuarioNaoTemPermissaoAtalho(pnlUnidade, cboUnidades, btnAtalhoUnidade, EnumFuncionalidade.UNIDADESMEDIAS);
            TrateUsuarioNaoTemPermissaoAtalho(pnlMarca, cboMarcas, btnAtalhoMarca, EnumFuncionalidade.MARCAS);
            TrateUsuarioNaoTemPermissaoAtalho(pnlFabricante, cboFabricantes, btnAtalhoFabricante, EnumFuncionalidade.FABRICANTES);
            TrateUsuarioNaoTemPermissaoAtalho(pnlCategoria, cboCategorias, btnAtalhoCategoria, EnumFuncionalidade.CATEGORIAS);
            TrateUsuarioNaoTemPermissaoAtalho(pnlGrupo, cboGrupos, btnAtalhoGrupo, EnumFuncionalidade.GRUPODEPRODUTOS);
            TrateUsuarioNaoTemPermissaoAtalho(pnlSubGrupo, cboSubGrupos, btnAtalhoSubGrupo, EnumFuncionalidade.SUBGRUPODEPRODUTOS);

            TrateUsuarioNaoTemPermissaoAtalho(pnlTamanho, cboTamanhos, btnAtalhoTamanho, EnumFuncionalidade.TAMANHOS);
            TrateUsuarioNaoTemPermissaoAtalho(pnlCor, cboCores, btnAtalhoCor, EnumFuncionalidade.CORES);
        }

        private void PesquisePeloId()
        {
            ServicoProduto servicoProduto = new ServicoProduto();
            var produto = servicoProduto.Consulte(txtId.Text.ToInt());

            if (produto != null)
            {
                EditeProduto(produto);
            }
            else
            {
                MessageBox.Show("Produto com código " + txtId.Text + " não encontrado!", "Produto não encontrado");
                
                txtId.Text = string.Empty;
                txtId.Focus();
            }
        }

        #region " MOVIMENTAÇÕES PRODUTOS "

        private void PesquiseMovimentacoesProdutos()
        {
            if (txtId.Text.ToInt() == 0)
            {
                MessageBox.Show("Selecione um produto para atualizar suas movimentações");

                return;
            }

            int produtoId = txtId.Text.ToInt();
            DateTime? dataInicial = txtDataInicialMovimentacao.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinalMovimentacao.Text.ToDateNullabel();
            EnumTipoMovimentacao? tipoMovimentacao = (EnumTipoMovimentacao?)cboTipoMovimentacao.EditValue;
            EnumOrigemMovimentacao? origemMovimentacao = (EnumOrigemMovimentacao?)cboOrigemMovimentacao.EditValue;

            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();
            var listaMovimentacoes = servicoMovimentacao.ConsulteVwMovimentacoesProdutos(txtId.Text.ToInt(), dataInicial, dataFinal, tipoMovimentacao, origemMovimentacao);

            PreenchaGridMovimentacoesProdutos(listaMovimentacoes);
        }

        private void PreenchaGridMovimentacoesProdutos(List<VwMovimentacaoProduto> listaVwMovimentacaoProduto)
        {
            int quantidadeMovimentacoes = listaVwMovimentacaoProduto != null ? listaVwMovimentacaoProduto.Count : 0;

            VwMovimentacaoProdutoGrid[] listaVwMovimentacaoprodutoGrid = new VwMovimentacaoProdutoGrid[quantidadeMovimentacoes];

            bool houveAlgumFiltro = !string.IsNullOrEmpty(txtDataInicialMovimentacao.Text) ||
                                                !string.IsNullOrEmpty(txtDataFinalMovimentacao.Text) ||
                                                cboTipoMovimentacao.EditValue != null ||
                                                cboOrigemMovimentacao.EditValue != null ? true : false;

            if (listaVwMovimentacaoProduto != null)
            {
                double saldo = 0;

                for (int i = quantidadeMovimentacoes - 1; i >= 0; i--)
                {
                    var item = listaVwMovimentacaoProduto[i];

                    VwMovimentacaoProdutoGrid vwMovimentacaoProdutoGrid = new VwMovimentacaoProdutoGrid();

                    vwMovimentacaoProdutoGrid.Data = item.DataMovimentacao.ToString("dd/MM/yyyy HH:mm");
                    vwMovimentacaoProdutoGrid.Usuario = item.PessoaId + " - " + item.PessoaNome;
                    vwMovimentacaoProdutoGrid.Tipo = item.TipoMovimentacao.Descricao();
                    vwMovimentacaoProdutoGrid.Origem = item.OrigemMovimentacao.Descricao();
                    vwMovimentacaoProdutoGrid.Observacoes = item.Observacoes;

                    if (item.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA)
                    {
                        vwMovimentacaoProdutoGrid.Entrada = item.Quantidade.ToString("#0.0000");

                        saldo += item.Quantidade;
                    }
                    else
                    {
                        vwMovimentacaoProdutoGrid.Saida = item.Quantidade.ToString("#0.0000");

                        saldo -= item.Quantidade;
                    }

                    if (!houveAlgumFiltro)
                    {
                        vwMovimentacaoProdutoGrid.Saldo = saldo.ToString("#0.0000");
                    }

                    listaVwMovimentacaoprodutoGrid[i] = vwMovimentacaoProdutoGrid;
                }
            }

            gcProdutos.DataSource = listaVwMovimentacaoprodutoGrid;
            gcProdutos.RefreshDataSource();
        }

        #endregion

        #region " FORNECEDORES "

        private void PreenchaFornecedor(Pessoa fornecedor, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (fornecedor != null)
            {
                txtIdFornecedor.Text = fornecedor.Id.ToString();
                txtNomeFornecedor.Text = fornecedor.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Fornecedor nao encontrado!", "Fornecedor não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdFornecedor.Focus();
                }

                txtIdFornecedor.Text = string.Empty;
                txtNomeFornecedor.Text = string.Empty;
            }
        }

        private void PreenchaGridFornecedores()
        {
            List<FornecedorGrid> listaFornecedoresGrid = new List<FornecedorGrid>();

            foreach (var fornecedor in _listaFornecedoresProdutos)
            {
                FornecedorGrid fornecedorGrid = new FornecedorGrid();

                fornecedorGrid.CodigoProdutoFornecedor = fornecedor.CodigoProduto;
                fornecedorGrid.Id = fornecedor.Fornecedor.Id;
                fornecedorGrid.NomeFantasia = fornecedor.Fornecedor.DadosGerais.NomeFantasia;
                
                fornecedorGrid.RazaoSocial = fornecedor.Fornecedor.DadosGerais.Razao;

                listaFornecedoresGrid.Add(fornecedorGrid);
            }

            gcFornecedores.DataSource = listaFornecedoresGrid;
            gcFornecedores.RefreshDataSource();
        }

        private void IncluaOuAtualizeFornecedor()
        {
            if (string.IsNullOrWhiteSpace(txtIdFornecedor.Text))
            {
                MessageBox.Show("Fornecedor não informado.", "Aviso");

                return;
            }

            if (string.IsNullOrWhiteSpace(txtCodigoFornecedor.Text))
            {
                MessageBox.Show("Código do Produto no Fornecedor não informado.", "Aviso");

                return;
            }

            var fornecedor = _listaFornecedoresProdutos.Find(forn => forn.Fornecedor.Id == txtIdFornecedor.Text.ToInt());

            if (fornecedor == null)
            {
                fornecedor = new FornecedorProduto();

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                fornecedor.Fornecedor = servicoPessoa.Consulte(txtIdFornecedor.Text.ToInt());

                _listaFornecedoresProdutos.Add(fornecedor);
            }

            fornecedor.CodigoProduto = txtCodigoFornecedor.Text;

            PreenchaGridFornecedores();

            LimpeCamposFornecedor();
        }

        private void LimpeCamposFornecedor()
        {
            txtIdFornecedor.Text = string.Empty;
            txtNomeFornecedor.Text = string.Empty;
            txtCodigoFornecedor.Text = string.Empty;

            txtIdFornecedor.Focus();
        }

        private void ExcluaFornecedor()
        {
            if (string.IsNullOrWhiteSpace(txtIdFornecedor.Text))
            {
                MessageBox.Show("Fornecedor não informado.", "Aviso");

                return;
            }

            var fornecedor = _listaFornecedoresProdutos.Find(forn => forn.Fornecedor.Id == txtIdFornecedor.Text.ToInt());

            if (fornecedor == null)
            {
                MessageBox.Show("Fornecedor não encontrado.", "Aviso");

                return;
            }

            if (MessageBox.Show("Deseja excluir o fornecedor  " + fornecedor.Fornecedor.Id + " - " + fornecedor.Fornecedor.DadosGerais.Razao + "?", "Excluir Fornecedor", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _listaFornecedoresProdutos.Remove(fornecedor);

                MessageBox.Show("Fornecedor excluído com sucesso.");

                PreenchaGridFornecedores();
                LimpeCamposFornecedor();
            }
        }

        private void EditeFornecedorDoGrid()
        {
            if (_listaFornecedoresProdutos != null && _listaFornecedoresProdutos.Count > 0)
            {
                var fornecedorProduto = _listaFornecedoresProdutos.FirstOrDefault(item => item.Fornecedor.Id == colunaFornecedorId.View.GetFocusedRowCellValue(colunaFornecedorId).ToInt());

                PreenchaFornecedor(fornecedorProduto.Fornecedor);
                txtCodigoFornecedor.Text = fornecedorProduto.CodigoProduto;

                txtCodigoFornecedor.Focus();
            }
        }

        private bool VerifiqueCodigoProdutoFornecedor()
        {
            ServicoProduto servicoProduto = new ServicoProduto();

            int contLista = _listaFornecedoresProdutos.Count;
            int indiceLista = 0;
            while (contLista > 0)
            {
                var produto = servicoProduto.ConsulteProdutoFornecedorPeloCodigo(_listaFornecedoresProdutos[indiceLista].CodigoProduto);

                if (produto != null)
                {
                    if (produto.Produto != null && produto.Produto.Id != txtId.Text.ToInt())
                    {
                        MessageBox.Show("O código do fornecedor já está sendo utilizado por outro produto de Código: " + produto.Produto.Id, "Aviso");
                        return true;
                    }
                }

                indiceLista++;
                contLista--;
            }
            return false;
        }

        private bool VerifiqueGrupoTributacao()
        {
         var empresa = new ServicoEmpresa().ConsulteUltimaEmpresa();

            if(empresa.DadosEmpresa.CodigoRegimeTributario == EnumCodigoRegimeTributario.SIMPLESNACIONAL)
            {
                if(txtIdGrupoTributacaoFederal.Text != string.Empty)
                {
                    MessageBox.Show("O grupo de tributação informado, não é compatível com o seu regime tributário. Para continuar. Informe o código no Grupo de Tribução Estadual.", "Aviso");
                    return true;
                }
            }

            return false;
        }

        #endregion

        #endregion

        #region " CLASSES AUXILIARES "

        private class VwMovimentacaoProdutoGrid
        {
            public string Data { get; set; }

            public string Usuario { get; set; }

            public string Tipo { get; set; }

            public string Origem { get; set; }

            public string Observacoes { get; set; }

            public string Entrada { get; set; }

            public string Saida { get; set; }

            public string Saldo { get; set; }
        }

        private class FornecedorGrid
        {
            public int Id { get; set; }

            public string RazaoSocial { get; set; }

            public string NomeFantasia { get; set; }

            public string CodigoProdutoFornecedor { get; set; }
        }


        #endregion

        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
