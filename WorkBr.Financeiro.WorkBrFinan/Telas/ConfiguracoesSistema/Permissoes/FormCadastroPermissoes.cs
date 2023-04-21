using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.GrupoAcessoServ;
using Programax.Easy.Servico.ConfiguracoesSistema.PermissaoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Telas.ConfiguracoesSistema.GruposAcesso;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.Permissoes
{
    public partial class FormCadastroPermissoes : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Parametros _parametros;

        private List<Permissao> _listaPermissoes = new List<Permissao>();

        private List<Permissao> _listaPermissoesParceiro = new List<Permissao>();
        private List<Permissao> _listaPermissoesFiscal = new List<Permissao>();
        private List<Permissao> _listaPermissoesFinanceiro = new List<Permissao>();
        private List<Permissao> _listaPermissoesEstoque = new List<Permissao>();
        private List<Permissao> _listaPermissoesFrenteDeLoja = new List<Permissao>();
        private List<Permissao> _listaPermissoesVendas = new List<Permissao>();
        private List<Permissao> _listaPermissoesAdmSistema = new List<Permissao>();
        private List<Permissao> _listaPermissoesRelatorios = new List<Permissao>();

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroPermissoes()
        {
            InitializeComponent();

            ServicoParametros servicoParametros = new ServicoParametros();
            _parametros = servicoParametros.ConsulteParametros();

            CrieListaParceiros();
            CrieListaFiscal();
            CrieListaFinanceiro();
            CrieListaEstoque();
            CrieListaVendas();
            CrieListaFrenteDeLoja();
            CrieListaAdmSistema();
            CrieListaRelatorios();

            PreenchaGrids();

            this.ActiveControl = txtId;

            this.NomeDaTela = "Permissões";
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormCadastroPermissoes_Load(object sender, EventArgs e)
        {

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Selecione um grupo de acesso.");

                return;
            }

            Action actionSalvar = () =>
            {
                ServicoPermissao servicoPermissao = new ServicoPermissao();

                servicoPermissao.CadastreLista(_listaPermissoes);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, "Permissões salvas com sucesso.\n\nAs permissões só terão efeito quando o usuário entrar no sistema novamente.");

            MessageBox.Show("O sistema irá FECHAR automaticamente. Para efetivar as alterações.", "Permissões", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Application.OpenForms["FormPrincipal"].Close();

            this.Close();
            
        }

        private void btnHerdarPermissoes_Click(object sender, EventArgs e)
        {
            var idGrupo = txtId.Text.ToInt();
            var nomeGrupo = txtDescricao.Text;

            FormGruposAcessoPesquisa formGruposAcessoPesquisa = new FormGruposAcessoPesquisa();

            var grupoAcessoHeranca = formGruposAcessoPesquisa.ExibaPesquisaDeGruposAcesso();

            if (grupoAcessoHeranca != null)
            {
                EditeGrupoAcesso(grupoAcessoHeranca);

                txtId.Text = idGrupo.ToString();
                txtDescricao.Text = nomeGrupo;

                GrupoAcesso grupoAcesso = new GrupoAcesso();
                grupoAcesso.Id = idGrupo;
                grupoAcesso.Descricao = nomeGrupo;

                _listaPermissoes.ForEach(permissao => permissao.GrupoAcesso = grupoAcesso);
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

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormGruposAcessoPesquisa formGruposAcessoPesquisa = new FormGruposAcessoPesquisa();

            var grupoAcesso = formGruposAcessoPesquisa.ExibaPesquisaDeGruposAcesso();

            if (grupoAcesso != null)
            {
                EditeGrupoAcesso(grupoAcesso);
            }
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (txtId.Enabled && !string.IsNullOrEmpty(txtId.Text))
            {
                ServicoGrupoAcesso servicoGrupoAcesso = new ServicoGrupoAcesso();
                var grupoAcesso = servicoGrupoAcesso.Consulte(txtId.Text.ToInt());

                EditeGrupoAcesso(grupoAcesso);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void chkMarcarTodosConsulta_CheckedChanged(object sender, EventArgs e)
        {
            _listaPermissoes.ForEach(permissao => permissao.Acessar = chkMarcarTodosConsulta.Checked);

            PreenchaGrids();
        }

        private void chkMarcarTodosInserir_CheckedChanged(object sender, EventArgs e)
        {
            _listaPermissoes.ForEach(permissao => permissao.Alterar = chkMarcarTodosInserir.Checked);

            PreenchaGrids();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void CrieListaParceiros()
        {
            Permissao permissaoPessoa = new Permissao();
            permissaoPessoa.Funcionalidade = EnumFuncionalidade.PESSOAS;
            permissaoPessoa.NomeMenu = "tspMenuPessoas";

            Permissao permissaoComissoes = new Permissao();
            permissaoComissoes.Funcionalidade = EnumFuncionalidade.COMISSOES;
            permissaoComissoes.NomeMenu = "tspMenuComissoes";

            Permissao permissaoCidades = new Permissao();
            permissaoCidades.Funcionalidade = EnumFuncionalidade.CIDADES;
            permissaoCidades.NomeMenu = "tspMenuCidades";

            Permissao permissaoEnderecos = new Permissao();
            permissaoEnderecos.Funcionalidade = EnumFuncionalidade.ENDERECOS;
            permissaoEnderecos.NomeMenu = "tspMenuEnderecos";

            Permissao permissaoRamosAtividades = new Permissao();
            permissaoRamosAtividades.Funcionalidade = EnumFuncionalidade.RamoAtividade;
            permissaoRamosAtividades.NomeMenu = "tspMenuRamosAtividades";

            Permissao permissaoOrigensClientes = new Permissao();
            permissaoOrigensClientes.Funcionalidade = EnumFuncionalidade.OrigemCliente;
            permissaoOrigensClientes.NomeMenu = "tspMenuOrigensClientes";

            _listaPermissoesParceiro.Add(permissaoPessoa);
            _listaPermissoesParceiro.Add(permissaoComissoes);
            _listaPermissoesParceiro.Add(permissaoCidades);
            _listaPermissoesParceiro.Add(permissaoEnderecos);
            _listaPermissoesParceiro.Add(permissaoRamosAtividades);
            _listaPermissoesParceiro.Add(permissaoOrigensClientes);

            _listaPermissoes.AddRange(_listaPermissoesParceiro);
        }

        private void CrieListaFiscal()
        {
            Permissao permissaoCfop = new Permissao();
            permissaoCfop.Funcionalidade = EnumFuncionalidade.CFOP;
            permissaoCfop.NomeMenu = "tspMenuCfop";

            Permissao permissaoCnae = new Permissao();
            permissaoCnae.Funcionalidade = EnumFuncionalidade.CNAE;
            permissaoCnae.NomeMenu = "tspMenuCnae";

            Permissao permissaoNcm = new Permissao();
            permissaoNcm.Funcionalidade = EnumFuncionalidade.NCM;
            permissaoNcm.NomeMenu = "tspMenuNcm";

            Permissao permissaoCest = new Permissao();
            permissaoCest.Funcionalidade = EnumFuncionalidade.CEST;
            permissaoCest.NomeMenu = "tspMenuCest";

            Permissao permissaoPautaIcms = new Permissao();
            permissaoPautaIcms.Funcionalidade = EnumFuncionalidade.PAUTAICMS;
            permissaoPautaIcms.NomeMenu = "tspMenuPautaIcms";

            Permissao permissaoNaturezaOperacao = new Permissao();
            permissaoNaturezaOperacao.Funcionalidade = EnumFuncionalidade.NATUREZAOPERACAO;
            permissaoNaturezaOperacao.NomeMenu = "tspMenuNaturezaOperacao";

            Permissao permissaoNotaFiscal = new Permissao();
            permissaoNotaFiscal.Funcionalidade = EnumFuncionalidade.NOTAFISCAL;
            permissaoNotaFiscal.NomeMenu = "tspMenuNotasFiscais";

            Permissao permissaoOutrasSaidas = new Permissao();
            permissaoNotaFiscal.Funcionalidade = EnumFuncionalidade.OUTRASSAIDAS;
            permissaoNotaFiscal.NomeMenu = "tspMenuOutrasSaidas";

            Permissao permissaoConfiguracaoNfe = new Permissao();
            permissaoConfiguracaoNfe.Funcionalidade = EnumFuncionalidade.CONFIGURACAONFE;
            permissaoConfiguracaoNfe.NomeMenu = "tspMenuConfiguracoesNfe";

            Permissao permissaoCancelamentoNotaFiscal = new Permissao();
            permissaoCancelamentoNotaFiscal.Funcionalidade = EnumFuncionalidade.CANCELAMENTONOTAFISCAL;
            permissaoCancelamentoNotaFiscal.NomeMenu = "tspMenuCancelamentoDeNotasFiscais";

            Permissao permissaoInutilizacaoNumeracaoNota = new Permissao();
            permissaoInutilizacaoNumeracaoNota.Funcionalidade = EnumFuncionalidade.INUTILIZACAONUMERACAONOTA;
            permissaoInutilizacaoNumeracaoNota.NomeMenu = "tspMenuInutilizacaoNumeracaoNota";

            Permissao permissaoGrupoTributacaoIcms = new Permissao();
            permissaoGrupoTributacaoIcms.Funcionalidade = EnumFuncionalidade.GRUPOTRIBUTACAOICMS;
            permissaoGrupoTributacaoIcms.NomeMenu = "tspMenuGrupoTributacaoIcms";

            Permissao permissaoGrupoTributacaoFederal = new Permissao();
            permissaoGrupoTributacaoFederal.Funcionalidade = EnumFuncionalidade.GRUPOTRIBUTACAOFEDERAL;
            permissaoGrupoTributacaoFederal.NomeMenu = "tspMenuGrupoTributacaoFederal";

            Permissao permissaoIcmsInterestadual = new Permissao();
            permissaoIcmsInterestadual.Funcionalidade = EnumFuncionalidade.ICMSINTERESTADUAL;
            permissaoIcmsInterestadual.NomeMenu = "tspMenuIcmsInterestadual";

            Permissao permissaoSpedFiscal = new Permissao();
            permissaoSpedFiscal.Funcionalidade = EnumFuncionalidade.SPEDFISCAL;
            permissaoSpedFiscal.NomeMenu = "tspMenuSpedFiscal";



            _listaPermissoesFiscal.Add(permissaoCfop);
            _listaPermissoesFiscal.Add(permissaoCnae);
            _listaPermissoesFiscal.Add(permissaoNcm);
            _listaPermissoesFiscal.Add(permissaoCest);
            _listaPermissoesFiscal.Add(permissaoPautaIcms);
            _listaPermissoesFiscal.Add(permissaoNaturezaOperacao);
            _listaPermissoesFiscal.Add(permissaoNotaFiscal);
            _listaPermissoesFiscal.Add(permissaoConfiguracaoNfe);
            _listaPermissoesFiscal.Add(permissaoCancelamentoNotaFiscal);
            _listaPermissoesFiscal.Add(permissaoInutilizacaoNumeracaoNota);
            _listaPermissoesFiscal.Add(permissaoGrupoTributacaoIcms);
            _listaPermissoesFiscal.Add(permissaoGrupoTributacaoFederal);

            _listaPermissoesFiscal.Add(permissaoIcmsInterestadual);
            _listaPermissoesFiscal.Add(permissaoSpedFiscal);

            _listaPermissoes.AddRange(_listaPermissoesFiscal);
        }

        private void CrieListaFinanceiro()
        {
            Permissao PermissaoBanco = new Permissao();
            PermissaoBanco.Funcionalidade = EnumFuncionalidade.BANCOS;
            PermissaoBanco.NomeMenu = "tspMenuBancos";

            Permissao PermissaoAgencias = new Permissao();
            PermissaoAgencias.Funcionalidade = EnumFuncionalidade.AGENCIAS;
            PermissaoAgencias.NomeMenu = "tspMenuAgencias";

            Permissao PermissaoContasBancarias = new Permissao();
            PermissaoContasBancarias.Funcionalidade = EnumFuncionalidade.CONTASBANCARIAS;
            PermissaoContasBancarias.NomeMenu = "tspMenuContasBancarias";

            Permissao PermissaoFormaPagamento = new Permissao();
            PermissaoFormaPagamento.Funcionalidade = EnumFuncionalidade.FORMAPAGAMENTO;
            PermissaoFormaPagamento.NomeMenu = "tspMenuFormaPagamento";

            Permissao PermissaoCondicaoPagamento = new Permissao();
            PermissaoCondicaoPagamento.Funcionalidade = EnumFuncionalidade.CONDICOESPAGAMENTO;
            PermissaoCondicaoPagamento.NomeMenu = "tspMenuCondicaoPagamento";

            Permissao PermissaoPlanoDeContas = new Permissao();
            PermissaoPlanoDeContas.Funcionalidade = EnumFuncionalidade.PLANODECONTAS;
            PermissaoPlanoDeContas.NomeMenu = "tspMenuPlanoDeContas";

           

            Permissao PermissaoContasPagar = new Permissao();
            PermissaoContasPagar.Funcionalidade = EnumFuncionalidade.CONTASPAGAR;
            PermissaoContasPagar.NomeMenu = "tspMenuContasPagar";

            Permissao PermissaoContasReceber = new Permissao();
            PermissaoContasReceber.Funcionalidade = EnumFuncionalidade.CONTASRECEBER;
            PermissaoContasReceber.NomeMenu = "tspMenuContasReceber";

            Permissao PermissaoManutencaoContasPagar = new Permissao();
            PermissaoManutencaoContasPagar.Funcionalidade = EnumFuncionalidade.MANUTENCAOCONTASPAGAR;
            PermissaoManutencaoContasPagar.NomeMenu = string.Empty;

            Permissao PermissaoProrrogacaoContasPagar = new Permissao();
            PermissaoProrrogacaoContasPagar.Funcionalidade = EnumFuncionalidade.PRORROGACAOCONTASPAGAR;
            PermissaoProrrogacaoContasPagar.NomeMenu = string.Empty;

            Permissao PermissaoManutencaoContasReceber = new Permissao();
            PermissaoManutencaoContasReceber.Funcionalidade = EnumFuncionalidade.MANUTENCAOCONTASRECEBER;
            PermissaoManutencaoContasReceber.NomeMenu = string.Empty;

            Permissao PermissaoProrrogacaoContasReceber = new Permissao();
            PermissaoProrrogacaoContasReceber.Funcionalidade = EnumFuncionalidade.PRORROGACAOCONTASRECEBER;
            PermissaoProrrogacaoContasReceber.NomeMenu = string.Empty;

            Permissao PermissaoCadastroCheque = new Permissao();
            PermissaoCadastroCheque.Funcionalidade = EnumFuncionalidade.CHEQUES;
            PermissaoCadastroCheque.NomeMenu = "tspMenuCheques";

            Permissao PermissaoCadastroCaixa = new Permissao();
            PermissaoCadastroCaixa.Funcionalidade = EnumFuncionalidade.CADASTROCAIXA;
            PermissaoCadastroCaixa.NomeMenu = "tspMenuCadastroCaixa";

            Permissao PermissaoMovimentacaoCaixa = new Permissao();
            PermissaoMovimentacaoCaixa.Funcionalidade = EnumFuncionalidade.MOVIMENTACAOCAIXA;
            PermissaoMovimentacaoCaixa.NomeMenu = "tspMenuMovimentacaoCaixa";

            Permissao PermissaoRecebimento = new Permissao();
            PermissaoRecebimento.Funcionalidade = EnumFuncionalidade.RECEBIMENTO;
            PermissaoRecebimento.NomeMenu = "tspMenuRecebimento";

            Permissao PermissaoAnaliseCredito = new Permissao();
            PermissaoAnaliseCredito.Funcionalidade = EnumFuncionalidade.CREDIARIO;
            PermissaoAnaliseCredito.NomeMenu = "tspMenuCrediario";

            Permissao PermissaoConfiguracaoEmissaoBoleto = new Permissao();
            PermissaoConfiguracaoEmissaoBoleto.Funcionalidade = EnumFuncionalidade.CONFIGURACAOEMISSAOBOLETO;
            PermissaoConfiguracaoEmissaoBoleto.NomeMenu = "tspMenuConfigurarBoletos";

            Permissao PermissaoRetornoBoletos = new Permissao();
            PermissaoRetornoBoletos.Funcionalidade = EnumFuncionalidade.RETORNOBOLETOS;
            PermissaoRetornoBoletos.NomeMenu = "tspMenuRetornoBoletos";

            Permissao PermissaoResumoFinanceiro = new Permissao();
            PermissaoResumoFinanceiro.Funcionalidade = EnumFuncionalidade.RESUMOFINANCEIRO;
            PermissaoResumoFinanceiro.NomeMenu = "btnResumoFinanceiro";

            //Conciliação Bancária
            Permissao PermissaoGrupoCategoria = new Permissao();
            PermissaoGrupoCategoria.Funcionalidade = EnumFuncionalidade.GRUPOCATEGORIA;
            PermissaoGrupoCategoria.NomeMenu = "tspMenuGrupoCategoria";

            Permissao PermissaoCategoriasFinanceiras = new Permissao();
            PermissaoCategoriasFinanceiras.Funcionalidade = EnumFuncionalidade.CATEGORIAFINANCEIRA;
            PermissaoCategoriasFinanceiras.NomeMenu = "tspMenuCategoriasFinanceiras";

            Permissao PermissaoOperadorasCartao = new Permissao();
            PermissaoOperadorasCartao.Funcionalidade = EnumFuncionalidade.OPERADORASCARTAO;
            PermissaoOperadorasCartao.NomeMenu = "tspMenuOperadorasCartao";

            Permissao PermissaoBancosParaMovimento = new Permissao();
            PermissaoBancosParaMovimento.Funcionalidade = EnumFuncionalidade.BANCOPARAMOVIMENTO;
            PermissaoBancosParaMovimento.NomeMenu = "tspMenuBancoParaMovimento";

            Permissao PermissaoMovimentacoesBanco = new Permissao();
            PermissaoMovimentacoesBanco.Funcionalidade = EnumFuncionalidade.MOVIMENTACAOBANCO;
            PermissaoMovimentacoesBanco.NomeMenu = "tspMenuMovimentacoesBanco";

            Permissao PermissaoPlanoDeContasDRE = new Permissao();
            PermissaoPlanoDeContasDRE.Funcionalidade = EnumFuncionalidade.DRE;
            PermissaoPlanoDeContasDRE.NomeMenu = "tspMenuPlanoDeContasDRE";

            Permissao PermissaoDRE = new Permissao();
            PermissaoDRE.Funcionalidade = EnumFuncionalidade.DRE;
            PermissaoDRE.NomeMenu = "tspMenuDRE";

            _listaPermissoesFinanceiro.Add(PermissaoBanco);
            _listaPermissoesFinanceiro.Add(PermissaoAgencias);
            _listaPermissoesFinanceiro.Add(PermissaoContasBancarias);
            _listaPermissoesFinanceiro.Add(PermissaoFormaPagamento);
            _listaPermissoesFinanceiro.Add(PermissaoCondicaoPagamento);
            _listaPermissoesFinanceiro.Add(PermissaoPlanoDeContas);
            _listaPermissoesFinanceiro.Add(PermissaoContasPagar);
            _listaPermissoesFinanceiro.Add(PermissaoManutencaoContasPagar);
            _listaPermissoesFinanceiro.Add(PermissaoProrrogacaoContasPagar);
            _listaPermissoesFinanceiro.Add(PermissaoContasReceber);
            _listaPermissoesFinanceiro.Add(PermissaoManutencaoContasReceber);
            _listaPermissoesFinanceiro.Add(PermissaoProrrogacaoContasReceber);
            _listaPermissoesFinanceiro.Add(PermissaoCadastroCheque);
            _listaPermissoesFinanceiro.Add(PermissaoCadastroCaixa);
            _listaPermissoesFinanceiro.Add(PermissaoMovimentacaoCaixa);
            _listaPermissoesFinanceiro.Add(PermissaoRecebimento);
            _listaPermissoesFinanceiro.Add(PermissaoAnaliseCredito);
            _listaPermissoesFinanceiro.Add(PermissaoConfiguracaoEmissaoBoleto);
            _listaPermissoesFinanceiro.Add(PermissaoRetornoBoletos);
            _listaPermissoesFinanceiro.Add(PermissaoResumoFinanceiro);
            _listaPermissoesFinanceiro.Add(PermissaoPlanoDeContasDRE);
            _listaPermissoesFinanceiro.Add(PermissaoDRE);

            //Conciliação Bancária            
            _listaPermissoesFinanceiro.Add(PermissaoGrupoCategoria);
            _listaPermissoesFinanceiro.Add(PermissaoCategoriasFinanceiras);
            _listaPermissoesFinanceiro.Add(PermissaoOperadorasCartao);
            _listaPermissoesFinanceiro.Add(PermissaoBancosParaMovimento);
            _listaPermissoesFinanceiro.Add(PermissaoMovimentacoesBanco);

            _listaPermissoes.AddRange(_listaPermissoesFinanceiro);
        }

        private void CrieListaEstoque()
        {
            Permissao PermissaoTamanho = new Permissao();
            PermissaoTamanho.Funcionalidade = EnumFuncionalidade.TAMANHOS;
            PermissaoTamanho.NomeMenu = "tspMenuTamanhos";

            Permissao PermissaoCategoria = new Permissao();
            PermissaoCategoria.Funcionalidade = EnumFuncionalidade.CATEGORIAS;
            PermissaoCategoria.NomeMenu = "tspMenuCategoriaProdutos";

            Permissao PermissaoItens = new Permissao();
            PermissaoItens.Funcionalidade = EnumFuncionalidade.ITENS;
            PermissaoItens.NomeMenu = "tspMenuItens";

            Permissao PermissaoUnidadesMedida = new Permissao();
            PermissaoUnidadesMedida.Funcionalidade = EnumFuncionalidade.UNIDADESMEDIAS;
            PermissaoUnidadesMedida.NomeMenu = "tspMenuUnidadesMedida";

            Permissao PermissaoCores = new Permissao();
            PermissaoCores.Funcionalidade = EnumFuncionalidade.CORES;
            PermissaoCores.NomeMenu = "tspMenuCores";

            Permissao PermissaoGrupos = new Permissao();
            PermissaoGrupos.Funcionalidade = EnumFuncionalidade.GRUPODEPRODUTOS;
            PermissaoGrupos.NomeMenu = "tspMenuGrupoProdutos";

            Permissao PermissaoSubGrupo = new Permissao();
            PermissaoSubGrupo.Funcionalidade = EnumFuncionalidade.SUBGRUPODEPRODUTOS;
            PermissaoSubGrupo.NomeMenu = "tspMenuSubgrupoProdutos";

            Permissao PermissaoMarca = new Permissao();
            PermissaoMarca.Funcionalidade = EnumFuncionalidade.MARCAS;
            PermissaoMarca.NomeMenu = "tspMenuMarcas";

            Permissao PermissaoFabricantes = new Permissao();
            PermissaoFabricantes.Funcionalidade = EnumFuncionalidade.FABRICANTES;
            PermissaoFabricantes.NomeMenu = "tspMenuFabricantes";

            Permissao PermissaoTabelaPreco = new Permissao();
            PermissaoTabelaPreco.Funcionalidade = EnumFuncionalidade.TABELAPRECO;
            PermissaoTabelaPreco.NomeMenu = "tspMenuTabelaPrecos";

            Permissao PermissaoCorrecaoEstoque = new Permissao();
            PermissaoCorrecaoEstoque.Funcionalidade = EnumFuncionalidade.CORRECAOESTOQUE;
            PermissaoCorrecaoEstoque.NomeMenu = "tspMenuCorrecaoEstoque";

            Permissao PermissaoMotivoCorrecaoEstoque = new Permissao();
            PermissaoMotivoCorrecaoEstoque.Funcionalidade = EnumFuncionalidade.MOTIVOCORRECAOESTOQUE;
            PermissaoMotivoCorrecaoEstoque.NomeMenu = "tspMenuMotivoCorrecaoEstoque";

            Permissao PermissaoEntradaMercadoria = new Permissao();
            PermissaoEntradaMercadoria.Funcionalidade = EnumFuncionalidade.ENTRADAMERCADORIAS;
            PermissaoEntradaMercadoria.NomeMenu = "tspMenuEntradaDeMercadoria";

            Permissao PermissaoEntradaMercadoriamanual = new Permissao();
            PermissaoEntradaMercadoriamanual.Funcionalidade = EnumFuncionalidade.ENTRADAMERCADORIASMANUAL;
            PermissaoEntradaMercadoriamanual.NomeMenu = "tspMenuEntradaDeMercadoriaManual";

            Permissao PermissaoInventario = new Permissao();
            PermissaoInventario.Funcionalidade = EnumFuncionalidade.INVENTARIO;
            PermissaoInventario.NomeMenu = "tspMenuInventario";

            Permissao PermissaoFormacaoPreco = new Permissao();
            PermissaoFormacaoPreco.Funcionalidade = EnumFuncionalidade.FORMACAOPRECO;
            PermissaoFormacaoPreco.NomeMenu = "tspMenuFormacaoPrecoVenda";

            Permissao PermissaoSaidaDeEstoque = new Permissao();
            PermissaoSaidaDeEstoque.Funcionalidade = EnumFuncionalidade.SAIDADEESTOQUE;
            PermissaoSaidaDeEstoque.NomeMenu = "tspMenuSaidaDeEstoque";

            Permissao PermissaoEstornarEstoque = new Permissao();
            PermissaoEstornarEstoque.Funcionalidade = EnumFuncionalidade.ESTORNARSTOQUE;
            PermissaoEstornarEstoque.NomeMenu = "tspMenuEstornarEstoqueSaida";

            Permissao PermissaoCadastroSubEstoque = new Permissao();
            PermissaoCadastroSubEstoque.Funcionalidade = EnumFuncionalidade.SUBESTOQUE;
            PermissaoCadastroSubEstoque.NomeMenu = "tspMenuSaidaDeEstoque";

            Permissao PermissaoCadastroTransferenciaSubEstoque = new Permissao();
            PermissaoCadastroTransferenciaSubEstoque.Funcionalidade = EnumFuncionalidade.TRANSFERENCIASUBESTOQUE;
            PermissaoCadastroTransferenciaSubEstoque.NomeMenu = "tspMenuTransferenciasubestoque";

            _listaPermissoesEstoque.Add(PermissaoTamanho);
            _listaPermissoesEstoque.Add(PermissaoCategoria);
            _listaPermissoesEstoque.Add(PermissaoItens);
            _listaPermissoesEstoque.Add(PermissaoUnidadesMedida);
            _listaPermissoesEstoque.Add(PermissaoCores);
            _listaPermissoesEstoque.Add(PermissaoGrupos);
            _listaPermissoesEstoque.Add(PermissaoSubGrupo);
            _listaPermissoesEstoque.Add(PermissaoMarca);
            _listaPermissoesEstoque.Add(PermissaoFabricantes);
            _listaPermissoesEstoque.Add(PermissaoTabelaPreco);
            _listaPermissoesEstoque.Add(PermissaoCorrecaoEstoque);
            _listaPermissoesEstoque.Add(PermissaoMotivoCorrecaoEstoque);
            _listaPermissoesEstoque.Add(PermissaoEntradaMercadoria);
            _listaPermissoesEstoque.Add(PermissaoEntradaMercadoriamanual);
            _listaPermissoesEstoque.Add(PermissaoInventario);
            _listaPermissoesEstoque.Add(PermissaoFormacaoPreco);
            _listaPermissoesEstoque.Add(PermissaoSaidaDeEstoque);
            _listaPermissoesEstoque.Add(PermissaoEstornarEstoque);
            _listaPermissoesEstoque.Add(PermissaoCadastroSubEstoque);
            _listaPermissoesEstoque.Add(PermissaoCadastroTransferenciaSubEstoque);

            _listaPermissoes.AddRange(_listaPermissoesEstoque);
        }

        private void CrieListaVendas()
        {
            Permissao PermissaoPedidoDeVendas = new Permissao();
            PermissaoPedidoDeVendas.Funcionalidade = EnumFuncionalidade.PEDIDODEVENDAS;
            PermissaoPedidoDeVendas.NomeMenu = "tspMenuPedidoDeVendas";

            Permissao PermissaoVendaRapida = new Permissao();
            PermissaoVendaRapida.Funcionalidade = EnumFuncionalidade.VENDARAPIDA;
            PermissaoVendaRapida.NomeMenu = "tspMenuVendaRapida";

            Permissao PermissaoLiberacaoPedidoDeVendas = new Permissao();
            PermissaoLiberacaoPedidoDeVendas.Funcionalidade = EnumFuncionalidade.LIBERACAODOCUMENTO;
            PermissaoLiberacaoPedidoDeVendas.NomeMenu = "tspMenuLiberacaoPedidoDeVendas";

            Permissao PermissaoConsultaPrecoProduto = new Permissao();
            PermissaoConsultaPrecoProduto.Funcionalidade = EnumFuncionalidade.CONSULTAPRECOPRODUTO;
            PermissaoConsultaPrecoProduto.NomeMenu = "tspMenuConsultaPrecoProduto";

            Permissao PermissaoMotivoTrocaPedidoDeVenda = new Permissao();
            PermissaoMotivoTrocaPedidoDeVenda.Funcionalidade = EnumFuncionalidade.MOTIVOTROCAPEDIDOVENDA;
            PermissaoMotivoTrocaPedidoDeVenda.NomeMenu = "tspMenuMotivoTrocaPedidoDeVenda";

            Permissao PermissaoTrocaPedidoDeVenda = new Permissao();
            PermissaoTrocaPedidoDeVenda.Funcionalidade = EnumFuncionalidade.TROCAPEDIDOVENDA;
            PermissaoTrocaPedidoDeVenda.NomeMenu = "tspMenuTrocaPedidoDeVenda";

            Permissao PermissaoRoteiros = new Permissao();
            PermissaoRoteiros.Funcionalidade = EnumFuncionalidade.ROTEIROS;
            PermissaoRoteiros.NomeMenu = "tspMenuRoteiros";

            Permissao PermissaoAgenda = new Permissao();
            PermissaoAgenda.Funcionalidade = EnumFuncionalidade.AGENDA;
            PermissaoAgenda.NomeMenu = "tspMenuAgendamento";

            Permissao PermissaoTelemarketing = new Permissao();
            PermissaoTelemarketing.Funcionalidade = EnumFuncionalidade.TELEMARKETING;
            PermissaoTelemarketing.NomeMenu = "tspMenuTelemarketing";

            Permissao PermissaoGerenciarTmk = new Permissao();
            PermissaoGerenciarTmk.Funcionalidade = EnumFuncionalidade.GERENCIARTMK;
            PermissaoGerenciarTmk.NomeMenu = "tspMenuGerenciarTmk";

            Permissao PermissaoCriacaoCarteiras = new Permissao();
            PermissaoCriacaoCarteiras.Funcionalidade = EnumFuncionalidade.CRIACAOCARTEIRA;
            PermissaoCriacaoCarteiras.NomeMenu = "tspMenuCarteiras";

            Permissao PermissaoCriacaoCarteirasRefiltek = new Permissao();
            PermissaoCriacaoCarteirasRefiltek.Funcionalidade = EnumFuncionalidade.CRIACAOCARTEIRAREFILTEK;
            PermissaoCriacaoCarteirasRefiltek.NomeMenu = "tspMenuCarteirasRefiltek";

            _listaPermissoesVendas.Add(PermissaoPedidoDeVendas);
            _listaPermissoesVendas.Add(PermissaoLiberacaoPedidoDeVendas);
            _listaPermissoesVendas.Add(PermissaoVendaRapida);
            _listaPermissoesVendas.Add(PermissaoConsultaPrecoProduto);
            _listaPermissoesVendas.Add(PermissaoMotivoTrocaPedidoDeVenda);
            _listaPermissoesVendas.Add(PermissaoTrocaPedidoDeVenda);
            _listaPermissoesVendas.Add(PermissaoRoteiros);
            _listaPermissoesVendas.Add(PermissaoAgenda);
            _listaPermissoesVendas.Add(PermissaoTelemarketing);
            _listaPermissoesVendas.Add(PermissaoGerenciarTmk);
            _listaPermissoesVendas.Add(PermissaoCriacaoCarteiras);
            _listaPermissoesVendas.Add(PermissaoCriacaoCarteirasRefiltek);

            _listaPermissoes.AddRange(_listaPermissoesVendas);
        }

        private void CrieListaFrenteDeLoja()
        {
            Permissao PermissaExportarPreVendaDjPdv = new Permissao();
            PermissaExportarPreVendaDjPdv.Funcionalidade = EnumFuncionalidade.EXPORTARVENDASPDVECF;
            PermissaExportarPreVendaDjPdv.NomeMenu = "tspMenuExportarVendasPdvEcf";

            if (_parametros.ParametrosCadastros.PermiteVendaDiretaNoPDV)
            {
                Permissao PermissaPdv = new Permissao();
                PermissaPdv.Funcionalidade = EnumFuncionalidade.PDV;
                PermissaPdv.NomeMenu = "tspMenuPdv";

                _listaPermissoesFrenteDeLoja.Add(PermissaPdv);
            }

            _listaPermissoesFrenteDeLoja.Add(PermissaExportarPreVendaDjPdv);

            _listaPermissoes.AddRange(_listaPermissoesFrenteDeLoja);
        }

        private void CrieListaAdmSistema()
        {
            Permissao PermissaoEmpresa = new Permissao();
            PermissaoEmpresa.Funcionalidade = EnumFuncionalidade.EMPRESA;
            PermissaoEmpresa.NomeMenu = "tspMenuEmpresa";

            Permissao PermissaoUsuario = new Permissao();
            PermissaoUsuario.Funcionalidade = EnumFuncionalidade.USUARIO;
            PermissaoUsuario.NomeMenu = "tspMenuUsuario";

            Permissao PermissaoGrupoAcesso = new Permissao();
            PermissaoGrupoAcesso.Funcionalidade = EnumFuncionalidade.GRUPOACESSO;
            PermissaoGrupoAcesso.NomeMenu = "tspMenuGrupoDeAcesso";

            Permissao PermissaoPermissoes = new Permissao();
            PermissaoPermissoes.Funcionalidade = EnumFuncionalidade.PERMISSOES;
            PermissaoPermissoes.NomeMenu = "tspMenuPermissoes";

            Permissao PermissaoParametros = new Permissao();
            PermissaoParametros.Funcionalidade = EnumFuncionalidade.PARAMETROS;
            PermissaoParametros.NomeMenu = "tspMenuParametros";

            Permissao PermissaoLicencaDeUso = new Permissao();
            PermissaoLicencaDeUso.Funcionalidade = EnumFuncionalidade.LICENCADEUSO;
            PermissaoLicencaDeUso.NomeMenu = "tspMenuLicencaUso";

            Permissao PermissaoConfiguracoesPdv = new Permissao();
            PermissaoConfiguracoesPdv.Funcionalidade = EnumFuncionalidade.LICENCADEUSO;
            PermissaoConfiguracoesPdv.NomeMenu = "tspMenuConfiguracoesPdv";

            Permissao PermissaoBackup = new Permissao();
            PermissaoBackup.Funcionalidade = EnumFuncionalidade.BACKUP;
            PermissaoBackup.NomeMenu = "tspMenuBackup";

            Permissao PermissaoLiberacao = new Permissao();
            PermissaoLiberacao.Funcionalidade = EnumFuncionalidade.LIBERACAOSISTEMA;
            PermissaoLiberacao.NomeMenu = "tspMenuLiberacao";

            _listaPermissoesAdmSistema.Add(PermissaoEmpresa);
            _listaPermissoesAdmSistema.Add(PermissaoUsuario);
            _listaPermissoesAdmSistema.Add(PermissaoGrupoAcesso);
            _listaPermissoesAdmSistema.Add(PermissaoPermissoes);
            _listaPermissoesAdmSistema.Add(PermissaoParametros);
            _listaPermissoesAdmSistema.Add(PermissaoLicencaDeUso);
            _listaPermissoesAdmSistema.Add(PermissaoConfiguracoesPdv);
            _listaPermissoesAdmSistema.Add(PermissaoBackup);
            _listaPermissoesAdmSistema.Add(PermissaoLiberacao);

            _listaPermissoes.AddRange(_listaPermissoesAdmSistema);
        }

        private void CrieListaRelatorios()
        {
            Permissao PermissaoRelatorioParceiros = new Permissao();
            PermissaoRelatorioParceiros.Funcionalidade = EnumFuncionalidade.RELATORIOPARCEIROS;
            PermissaoRelatorioParceiros.NomeMenu = "tspMenuRelatorioDeParceiros";

            Permissao PermissaoRelatorioItens = new Permissao();
            PermissaoRelatorioItens.Funcionalidade = EnumFuncionalidade.RELATORIOITENS;
            PermissaoRelatorioItens.NomeMenu = "tspMenuRelatorioItens";

            Permissao PermissaoRelatorioContasPagar = new Permissao();
            PermissaoRelatorioContasPagar.Funcionalidade = EnumFuncionalidade.RELATORIOCONTASPAGAR;
            PermissaoRelatorioContasPagar.NomeMenu = "tspMenuRelatorioContasAPagar";

            Permissao PermissaoRelatorioContasReceber = new Permissao();
            PermissaoRelatorioContasReceber.Funcionalidade = EnumFuncionalidade.RELATORIOCONTASRECEBER;
            PermissaoRelatorioContasReceber.NomeMenu = "tspMenuRelatorioContasAReceber";

            Permissao PermissaoRelatorioVendasVendedor = new Permissao();
            PermissaoRelatorioVendasVendedor.Funcionalidade = EnumFuncionalidade.RELATORIOVENDASVENDEDOR;
            PermissaoRelatorioVendasVendedor.NomeMenu = "tspMenuRelatorioVendas";

            Permissao PermissaoRelatorioVendasVendedorVisualizarTodos = new Permissao();
            PermissaoRelatorioVendasVendedorVisualizarTodos.Funcionalidade = EnumFuncionalidade.RELATORIOVENDASVENDEDORVISUALIZARTODOSVENDEDORES;
            PermissaoRelatorioVendasVendedorVisualizarTodos.NomeMenu = string.Empty;

            Permissao PermissaoRelatorioVendasCliente = new Permissao();
            PermissaoRelatorioVendasCliente.Funcionalidade = EnumFuncionalidade.RELATORIOVENDASCLIENTE;
            PermissaoRelatorioVendasCliente.NomeMenu = "tspMenuRelatorioVendasCliente";

            Permissao PermissaoRelatorioEntrada = new Permissao();
            PermissaoRelatorioEntrada.Funcionalidade = EnumFuncionalidade.RELATORIOENTRADA;
            PermissaoRelatorioEntrada.NomeMenu = "tspMenuRelatorioEntrada";

            Permissao PermissaoRelatorioMovimentacaoCaixa = new Permissao();
            PermissaoRelatorioMovimentacaoCaixa.Funcionalidade = EnumFuncionalidade.RELATORIOMOVIMENTACAOCAIXA;
            PermissaoRelatorioMovimentacaoCaixa.NomeMenu = "tspMenuRelatorioMovimentacaoCaixa";

            Permissao PermissaoRelatorioMovimentacaoItens = new Permissao();
            PermissaoRelatorioMovimentacaoItens.Funcionalidade = EnumFuncionalidade.RELATORIOMOVIMENTACAOITENS;
            PermissaoRelatorioMovimentacaoItens.NomeMenu = "tspMenuRelatorioMovimentacaoItens";

            Permissao PermissaoRelatorioClientesSemComprar = new Permissao();
            PermissaoRelatorioClientesSemComprar.Funcionalidade = EnumFuncionalidade.RELATORIOCLIENTESSEMCOMPRAR;
            PermissaoRelatorioClientesSemComprar.NomeMenu = "tspMenuRelatorioClientesSemComprar";

            Permissao PermissaoRelatorioDeTransportes = new Permissao();
            PermissaoRelatorioDeTransportes.Funcionalidade = EnumFuncionalidade.RELATORIODETRANSPORTES;
            PermissaoRelatorioDeTransportes.NomeMenu = "tspMenuRelatorioDeTransportes";

            Permissao PermissaoRelatorioDeLocacao = new Permissao();
            PermissaoRelatorioDeLocacao.Funcionalidade = EnumFuncionalidade.RELATORIODELOCACAO;
            PermissaoRelatorioDeLocacao.NomeMenu = "tspMenuRelatorioLocacao";

            Permissao PermissaoRelatorioVendaPorCondicaoPagamento = new Permissao();
            PermissaoRelatorioVendaPorCondicaoPagamento.Funcionalidade = EnumFuncionalidade.RELATORIODEVENDAPORCONDICAODEPAGAMENTO;
            PermissaoRelatorioVendaPorCondicaoPagamento.NomeMenu = "tspMenuRelatorioCondicaoPagamento";

            Permissao PermissaoRelatorioDeItensVendidosEBaixados = new Permissao();
            PermissaoRelatorioDeItensVendidosEBaixados.Funcionalidade = EnumFuncionalidade.RELATORIODEITENSVENDIDOSEBAIXADOS;
            PermissaoRelatorioDeItensVendidosEBaixados.NomeMenu = "tspMenuRelatorioDeItensVendidosEBaixados";

            Permissao PermissaoRelatorioDeFluxoDeCaixa = new Permissao();
            PermissaoRelatorioDeFluxoDeCaixa.Funcionalidade = EnumFuncionalidade.RELATORIOFLUXOCAIXA;
            PermissaoRelatorioDeFluxoDeCaixa.NomeMenu = "tspMenuFluxoCaixa";

            Permissao PermissaoRelatorioCustoFinanceiro = new Permissao();
            PermissaoRelatorioCustoFinanceiro.Funcionalidade = EnumFuncionalidade.RELATORIOCUSTOFINANCEIROVENDEDOR;
            PermissaoRelatorioCustoFinanceiro.NomeMenu = "relatorioDeCustoFinanceiro";

            _listaPermissoesRelatorios.Add(PermissaoRelatorioParceiros);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioItens);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioContasPagar);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioContasReceber);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioVendasVendedor);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioVendasVendedorVisualizarTodos);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioVendasCliente);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioEntrada);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioMovimentacaoCaixa);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioMovimentacaoItens);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioClientesSemComprar);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioDeTransportes);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioDeLocacao);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioVendaPorCondicaoPagamento);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioDeItensVendidosEBaixados);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioDeFluxoDeCaixa);
            _listaPermissoesRelatorios.Add(PermissaoRelatorioCustoFinanceiro);

            _listaPermissoes.AddRange(_listaPermissoesRelatorios);
        }

        private void PreenchaGrids()
        {
            PreenchaGridParceiros();
            PreenchaGridFiscal();
            PreenchaGridFinanceiro();
            PreenchaGridEstoque();
            PreenchaGridVendas();
            PreenchaGridAdmSistema();
            PreenchaGridRelatorios();
        }

        private void PreenchaGridParceiros()
        {
            gcParceiros.DataSource = _listaPermissoesParceiro;
            gcParceiros.RefreshDataSource();
        }

        private void PreenchaGridFiscal()
        {
            gcFiscal.DataSource = _listaPermissoesFiscal;
            gcFiscal.RefreshDataSource();
        }

        private void PreenchaGridFinanceiro()
        {
            gcFinanceiro.DataSource = _listaPermissoesFinanceiro;
            gcFinanceiro.RefreshDataSource();
        }

        private void PreenchaGridEstoque()
        {
            gcEstoque.DataSource = _listaPermissoesEstoque;
            gcEstoque.RefreshDataSource();
        }

        private void PreenchaGridFrenteDeLoja()
        {
            gcFrenteDeLoja.DataSource = _listaPermissoesFrenteDeLoja;
            gcFrenteDeLoja.RefreshDataSource();
        }

        private void PreenchaGridVendas()
        {
            gcVendas.DataSource = _listaPermissoesVendas;
            gcVendas.RefreshDataSource();
        }

        private void PreenchaGridAdmSistema()
        {
            gcAdmSistema.DataSource = _listaPermissoesAdmSistema;
            gcAdmSistema.RefreshDataSource();
        }

        private void PreenchaGridRelatorios()
        {
            gcRelatorios.DataSource = _listaPermissoesRelatorios;
            gcRelatorios.RefreshDataSource();
        }

        private void EditeGrupoAcesso(GrupoAcesso grupoAcesso)
        {
            if (grupoAcesso != null)
            {
                txtId.Text = grupoAcesso.Id.ToString();
                txtDescricao.Text = grupoAcesso.Descricao;

                PreenchaPermissoesGrupo(grupoAcesso);
            }
            else
            {
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;
            }
        }

        private void PreenchaPermissoesGrupo(GrupoAcesso grupoAcesso)
        {
            ServicoPermissao servicoPermissao = new ServicoPermissao();
            var permissoesBase = servicoPermissao.ConsulteLista(grupoAcesso.Id);

            PreencherGuidPermissoes(_listaPermissoesParceiro, permissoesBase, PreenchaGridParceiros);
            PreencherGuidPermissoes(_listaPermissoesFiscal, permissoesBase, PreenchaGridFiscal);
            PreencherGuidPermissoes(_listaPermissoesFinanceiro, permissoesBase, PreenchaGridFinanceiro);
            PreencherGuidPermissoes(_listaPermissoesEstoque, permissoesBase, PreenchaGridEstoque);
            PreencherGuidPermissoes(_listaPermissoesFrenteDeLoja, permissoesBase, PreenchaGridFrenteDeLoja);
            PreencherGuidPermissoes(_listaPermissoesVendas, permissoesBase, PreenchaGridVendas);
            PreencherGuidPermissoes(_listaPermissoesAdmSistema, permissoesBase, PreenchaGridAdmSistema);
            PreencherGuidPermissoes(_listaPermissoesRelatorios, permissoesBase, PreenchaGridRelatorios);
        }

        private void PreencherGuidPermissoes(List<Permissao> listaDePermissaoBase, List<Permissao> listaDePermissoesBase, Action metodoParaPreencherGrid)
        {
            var grupoAcesso = new GrupoAcesso();
            grupoAcesso.Id = txtId.Text.ToInt();

            listaDePermissaoBase.ForEach(permissao =>
            {
                var permissaoBase = listaDePermissoesBase.FirstOrDefault(permis => permis.Funcionalidade == permissao.Funcionalidade);

                permissaoBase = permissaoBase ?? new Permissao();

                permissao.Alterar = permissaoBase.Alterar;
                permissao.Acessar = permissaoBase.Acessar;

                permissao.GrupoAcesso = grupoAcesso;
            });

            metodoParaPreencherGrid();
        }

        #endregion
    }
}
