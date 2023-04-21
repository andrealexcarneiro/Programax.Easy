using Programax.Easy.Negocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.InformacaoSistemaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.InformacaoSistemaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.LicencaDeUsoServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Caixas;
using Programax.Easy.View.Telas.Cadastros.Categorias;
using Programax.Easy.View.Telas.Cadastros.Cidades;
using Programax.Easy.View.Telas.Cadastros.Comissoes;
using Programax.Easy.View.Telas.Cadastros.Cores;
using Programax.Easy.View.Telas.Cadastros.CorrecoesEstoque;
using Programax.Easy.View.Telas.Cadastros.Empresas;
using Programax.Easy.View.Telas.Cadastros.Enderecos;
using Programax.Easy.View.Telas.Cadastros.EntradaMercadorias;
using Programax.Easy.View.Telas.Cadastros.Fabricantes;
using Programax.Easy.View.Telas.Cadastros.Grupos;
using Programax.Easy.View.Telas.Cadastros.GrupoTributacoesIcms;
using Programax.Easy.View.Telas.Cadastros.Inventarios;
using Programax.Easy.View.Telas.Cadastros.Marcas;
using Programax.Easy.View.Telas.Cadastros.MotivosCorrecoesEstoque;
using Programax.Easy.View.Telas.Cadastros.MotivosTrocaPedidoDeVenda;
using Programax.Easy.View.Telas.Cadastros.NaturezasOperacoes;
using Programax.Easy.View.Telas.Cadastros.OrigensClientes;
using Programax.Easy.View.Telas.Cadastros.PautasIcms;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Easy.View.Telas.Cadastros.RamosAtividades;
using Programax.Easy.View.Telas.Cadastros.SubGrupos;
using Programax.Easy.View.Telas.Cadastros.Tamanhos;
using Programax.Easy.View.Telas.Cadastros.UnidadesMedidas;
using Programax.Easy.View.Telas.ConfiguracoesSistema.AlteracaoSenha;
using Programax.Easy.View.Telas.ConfiguracoesSistema.Backups;
using Programax.Easy.View.Telas.ConfiguracoesSistema.FConfiguracoesPdv;
using Programax.Easy.View.Telas.ConfiguracoesSistema.FormParametros;
using Programax.Easy.View.Telas.ConfiguracoesSistema.GruposAcesso;
using Programax.Easy.View.Telas.ConfiguracoesSistema.Liberacao;
using Programax.Easy.View.Telas.ConfiguracoesSistema.LicencaDeUso;
using Programax.Easy.View.Telas.ConfiguracoesSistema.Login;
using Programax.Easy.View.Telas.ConfiguracoesSistema.Permissoes;
using Programax.Easy.View.Telas.ConfiguracoesSistema.Sobre;
using Programax.Easy.View.Telas.ConfiguracoesSistema.Usuarios;
using Programax.Easy.View.Telas.Estoque.FormacaoPrecoVenda;
using Programax.Easy.View.Telas.Estoque.SaidaEstoque;
using Programax.Easy.View.Telas.Financeiro.Agencias;
using Programax.Easy.View.Telas.Financeiro.Bancos;
using Programax.Easy.View.Telas.Financeiro.Boletos;
using Programax.Easy.View.Telas.Financeiro.Cheques;
using Programax.Easy.View.Telas.Financeiro.CondicoesPagamento;
using Programax.Easy.View.Telas.Financeiro.ContasBancarias;
using Programax.Easy.View.Telas.Financeiro.ContasPagarReceber;
using Programax.Easy.View.Telas.Financeiro.Crediarios;
using Programax.Easy.View.Telas.Financeiro.FormasPagamento;
using Programax.Easy.View.Telas.Financeiro.MovimentacoesCaixa;
using Programax.Easy.View.Telas.Financeiro.PlanosDeContas;
using Programax.Easy.View.Telas.Financeiro.RetornoBoletos;
using Programax.Easy.View.Telas.Fiscal.CancelamentosNotasFiscais;
using Programax.Easy.View.Telas.Fiscal.Cests;
using Programax.Easy.View.Telas.Fiscal.Cfops;
using Programax.Easy.View.Telas.Fiscal.Cnaes;
using Programax.Easy.View.Telas.Fiscal.ConfiguracoesNfe;
using Programax.Easy.View.Telas.Fiscal.FormsIcmsInterestadual;
using Programax.Easy.View.Telas.Fiscal.InutilizacaoDeNumeracaoDeNota;
using Programax.Easy.View.Telas.Fiscal.Ncms;
using Programax.Easy.View.Telas.Fiscal.NotasFiscais;
using Programax.Easy.View.Telas.Fiscal.OutrasSaidas;
using Programax.Easy.View.Telas.FrenteDeLoja.ExportaPedidoVendaPdvEcf;
using Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv;
using Programax.Easy.View.Telas.Produtos.TabelaDePreco;
using Programax.Easy.View.Telas.Relatorios;
using Programax.Easy.View.Telas.Vendas.ConsultaPrecoProduto;
using Programax.Easy.View.Telas.Vendas.LiberacaoPedidoDeVenda;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.View.Telas.Vendas.Recebimentos;
using Programax.Easy.View.Telas.Vendas.TrocaPedidoDeVendas;
using Programax.Easy.View.Telas.Vendas.VendaRapida;
using Programax.Easy.View.Telas.AtualizacaoDeSistema;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using Programax.Easy.View.Telas.Financeiro.Graficos;
using Programax.Easy.View.Telas.Cadastros.GruposCategorias;
using Programax.Easy.View.Telas.Financeiro.Categorias;
using Programax.Easy.View.Telas.Financeiro.OperadorasCartoes;
using Programax.Easy.View.Telas.Financeiro.BancosParaMovimento;
using Programax.Easy.View.Telas.Financeiro.MovimentacoesBanco;
using Programax.Easy.View.Telas.Vendas.Roteiros;
using Programax.Easy.View.Telas.TeleMarketing;

namespace Programax.Easy.View.Telas
{
    public partial class FormPrincipal : Form
    {
        #region " VARIÁVEIS PRIVADAS "

        private Parametros _parametros;
        private List<Form> _listaForms;
        private bool _primeiraVez = true;
        private bool _Acgua = false;
        #endregion

        #region " CONSTRUTOR "

        public FormPrincipal()
        {
            InicieSistema();
            carregarImagemTelaInicial();
            //IniciarTimer();
        }

        #endregion

        #region " EVENTOS MENU CADASTROS "

        private void pessoasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroPessoa formCadastroPessoa = new FormCadastroPessoa();

            AdicionarFormulario(formCadastroPessoa);
        }

        private void comissõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroComissao formCadastroComissao = new FormCadastroComissao();

            AdicionarFormulario(formCadastroComissao);
        }

        private void cidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroCidade formCadastroCidade = new FormCadastroCidade();

            AdicionarFormulario(formCadastroCidade);
        }

        private void endereçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroEndereco formCadastroEndereco = new FormCadastroEndereco();

            AdicionarFormulario(formCadastroEndereco);
        }

        private void tspMenuRamosAtividades_Click(object sender, EventArgs e)
        {
            FormCadastroRamoAtividade formCadastroRamoAtividade = new FormCadastroRamoAtividade();

            AdicionarFormulario(formCadastroRamoAtividade);
        }

        private void tspMenuOrigensClientes_Click(object sender, EventArgs e)
        {
            FormCadastroOrigemCliente formCadastroOrigemCliente = new FormCadastroOrigemCliente();

            AdicionarFormulario(formCadastroOrigemCliente);
        }

        private void tspMenuPautaIcms_Click(object sender, EventArgs e)
        {
            FormCadastroPautaIcms formCadastroPautaIcms = new FormCadastroPautaIcms();

            AdicionarFormulario(formCadastroPautaIcms);
        }

        private void tspMenuMotivoTrocaPedidoDeVenda_Click(object sender, EventArgs e)
        {
            FormCadastroDeMotivoTrocaPedidoDeVenda formCadastroDeMotivoTrocaPedidoDeVenda = new FormCadastroDeMotivoTrocaPedidoDeVenda();
            AdicionarFormulario(formCadastroDeMotivoTrocaPedidoDeVenda);
        }

        private void tspMenuGrupoTributacaoIcms_Click(object sender, EventArgs e)
        {
            FormCadastroGrupoTributacaoIcms formCadastroGrupoTributacaoIcms = new FormCadastroGrupoTributacaoIcms();
            AdicionarFormulario(formCadastroGrupoTributacaoIcms);
        }

        private void tspMenuGrupoTributacaoFederal_Click(object sender, EventArgs e)
        {
            FormCadastroGrupoTributacaoFederal formCadastroGrupoTributacaoFederal = new FormCadastroGrupoTributacaoFederal();
            AdicionarFormulario(formCadastroGrupoTributacaoFederal);
        }

        private void tspMenuIcmsInterestadual_Click(object sender, EventArgs e)
        {
            FormCadastroIcmsInterestadual formCadastroIcmsInterestadual = new FormCadastroIcmsInterestadual();

            AdicionarFormulario(formCadastroIcmsInterestadual);
        }

        #endregion

        #region " EVENTOS MENU FISCAL "

        private void tspMenuCnae_Click(object sender, EventArgs e)
        {
            FormCadastroDeCnae formCadastroDeCnae = new FormCadastroDeCnae();

            AdicionarFormulario(formCadastroDeCnae);
        }

        private void tspMenuCfop_Click(object sender, EventArgs e)
        {
            FormCadastroDeCfop formCadastroDeCfop = new FormCadastroDeCfop();

            AdicionarFormulario(formCadastroDeCfop);
        }

        private void tspMenuNcm_Click(object sender, EventArgs e)
        {
            FormCadastroDeNcm formCadastroDeNcm = new FormCadastroDeNcm();

            AdicionarFormulario(formCadastroDeNcm);
        }

        private void tspMenuCest_Click(object sender, EventArgs e)
        {
            FormCadastroDeCest formCadastroDeCest = new FormCadastroDeCest();

            AdicionarFormulario(formCadastroDeCest);
        }
        
        private void tspMenuNaturezaOperacao_Click(object sender, EventArgs e)
        {
            FormCadastroNaturezaOperacao formCadastroNaturezaOperacao = new FormCadastroNaturezaOperacao();

            AdicionarFormulario(formCadastroNaturezaOperacao);
        }

        private void tspMenuNotasFiscais_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FormPesquisaNotasFiscais formPesquisaNotasFiscais = new FormPesquisaNotasFiscais();
            AdicionarFormulario(formPesquisaNotasFiscais);
            this.Cursor = Cursors.Default;
        }

        private void tspMenuConfiguracoesNfe_Click(object sender, EventArgs e)
        {
            FormConfiguracaoNfe formConfiguracaoNfe = new FormConfiguracaoNfe();
            AdicionarFormulario(formConfiguracaoNfe);
        }

        private void tspMenuCancelamentoDeNotasFiscais_Click(object sender, EventArgs e)
        {
            FormCadastroCancelamentoNotaFiscal formCadastroCancelamentoNotaFiscal = new FormCadastroCancelamentoNotaFiscal();
            AdicionarFormulario(formCadastroCancelamentoNotaFiscal);
        }

        private void tspMenuInutilizacaoNumeracaoNota_Click(object sender, EventArgs e)
        {
            FormCadastroInutilizacaoNumeroDeNota formCadastroInutilizacaoNumeroDeNota = new FormCadastroInutilizacaoNumeroDeNota();
            AdicionarFormulario(formCadastroInutilizacaoNumeroDeNota);
        }

        private void tspMenuOutrasSaidas_Click(object sender, EventArgs e)
        {
            FormOutrasSaidas formOutrasSaidas = new FormOutrasSaidas();
            formOutrasSaidas.Show();
        }
       
        #endregion

        #region " EVENTOS MENU FINANCEIRO "

        private void tspMenuBancos_Click(object sender, EventArgs e)
        {
            
        }

        private void tspMenuFormaPagamento_Click(object sender, EventArgs e)
        {
            FormCadastroFormaPagamento formCadastroFormaPagamento = new FormCadastroFormaPagamento();

            AdicionarFormulario(formCadastroFormaPagamento);
        }

        private void condiçãoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroCondicaoPagamento formCadastroCondicaoPagamento = new FormCadastroCondicaoPagamento();

            AdicionarFormulario(formCadastroCondicaoPagamento);
        }

        private void tspMenuAgencias_Click(object sender, EventArgs e)
        {
           
        }

        private void tspMenuContasBancarias_Click(object sender, EventArgs e)
        {
            
        }

        private void tspMenuPlanoDeContas_Click(object sender, EventArgs e)
        {
            FormCadastroPlanoConta formCadastroPlanoConta = new FormCadastroPlanoConta();

            AdicionarFormulario(formCadastroPlanoConta);
        }

        private void tspMenuContasPagarReceber_Click(object sender, EventArgs e)
        {
            FormContasPagarPesquisa formContasPagarPesquisa = new FormContasPagarPesquisa();

            AdicionarFormulario(formContasPagarPesquisa);
        }

        private void tspMenuConfigurarBoletos_Click(object sender, EventArgs e)
        {
            FormConfiguracaoBoletos formConfiguracaoBoletos = new FormConfiguracaoBoletos();

            AdicionarFormulario(formConfiguracaoBoletos);
        }

        private void tspMenuContasReceber_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            FormContasReceberPesquisa formContasReceberPesquisa = new FormContasReceberPesquisa();

            AdicionarFormulario(formContasReceberPesquisa);

            this.Cursor = Cursors.Default;

        }

        private void tspMenuCheques_Click(object sender, EventArgs e)
        {
            FormChequesPesquisa formChequesPesquisa = new FormChequesPesquisa();

            AdicionarFormulario(formChequesPesquisa);
        }

        private void tspMenuCadastroCaixa_Click(object sender, EventArgs e)
        {
            FormCadastroCaixa formCadastroCaixa = new FormCadastroCaixa();

            AdicionarFormulario(formCadastroCaixa);
        }

        private void tspMenuMovimentacaoCaixa_Click(object sender, EventArgs e)
        {
            FormCadastroMovimentacoesCaixa formCadastroMovimentacoesCaixa = new FormCadastroMovimentacoesCaixa();

            AdicionarFormulario(formCadastroMovimentacoesCaixa);
        }

        private void tspMenuRetornoBoletos_Click(object sender, EventArgs e)
        {
            FormRetornoBoletos formRetornoBoletos = new FormRetornoBoletos();
            AdicionarFormulario(formRetornoBoletos);
        }

        #endregion

        #region " EVENTOS MENU ESTOQUE "
        //Andre
        private void tspMenuItens_Click(object sender, EventArgs e)
        {
            FormCadastroDeProduto formCadastroDeProduto = new FormCadastroDeProduto();

            AdicionarFormulario(formCadastroDeProduto);
        }

        private void tspMenuCategoriaProdutos_Click(object sender, EventArgs e)
        {
            FormCadastroCategoria formCadastroCategoria = new FormCadastroCategoria();

            AdicionarFormulario(formCadastroCategoria);
        }

        private void tspMenuGrupoProdutos_Click(object sender, EventArgs e)
        {
            FormCadastroGrupo formCadastroGrupo = new FormCadastroGrupo();

            AdicionarFormulario(formCadastroGrupo);
        }

        private void tspMenuSubgrupoProdutos_Click(object sender, EventArgs e)
        {
            FormCadastroSubGrupos formCadastroSubGrupos = new FormCadastroSubGrupos();

            AdicionarFormulario(formCadastroSubGrupos);
        }

        private void tspMenuMarcas_Click(object sender, EventArgs e)
        {
            FormCadastroMarca formCadastroMarca = new FormCadastroMarca();

            AdicionarFormulario(formCadastroMarca);
        }

        private void tspMenuTabelaPrecos_Click(object sender, EventArgs e)
        {
            FormTabelaDePreco formTabelaPreco = new FormTabelaDePreco();

            AdicionarFormulario(formTabelaPreco);
        }

        private void tspMenuTamanhos_Click(object sender, EventArgs e)
        {
            FormCadastroDeTamanhos formCadastroDeTamanhos = new FormCadastroDeTamanhos();

            AdicionarFormulario(formCadastroDeTamanhos);
        }

        private void tspMenuCores_Click(object sender, EventArgs e)
        {
            FormCadastroDeCores formCadastroDeCores = new FormCadastroDeCores();

            AdicionarFormulario(formCadastroDeCores);
        }

        private void tspMenuUnidadesMedida_Click(object sender, EventArgs e)
        {
            FormCadastroUnidadeMedida formCadastroUnidadeMedida = new FormCadastroUnidadeMedida();

            AdicionarFormulario(formCadastroUnidadeMedida);
        }

        private void tspMenuCorrecaoEstoque_Click(object sender, EventArgs e)
        {
            FormCorrecaoEstoque formCorrecaoEstoque = new FormCorrecaoEstoque();

            AdicionarFormulario(formCorrecaoEstoque);
        }

        private void tspMenuMotivoCorrecaoEstoque_Click(object sender, EventArgs e)
        {
            FormCadastroDeMotivoCorrecaoEstoque formCadastroDeMotivoCorrecaoEstoque = new FormCadastroDeMotivoCorrecaoEstoque();

            AdicionarFormulario(formCadastroDeMotivoCorrecaoEstoque);
        }

        private void tspMenuEntradaDeMercadoria_Click(object sender, EventArgs e)
        {
            FormEntradaMercadoria formEntradaMercadoria = new FormEntradaMercadoria();

            AdicionarFormulario(formEntradaMercadoria);
        }

        private void inventárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroInventario formCadastroInventario = new FormCadastroInventario();

            AdicionarFormulario(formCadastroInventario);
        }

        private void tspMenuFabricantes_Click(object sender, EventArgs e)
        {
            FormCadastroFabricante formCadastroFabricante = new FormCadastroFabricante();

            AdicionarFormulario(formCadastroFabricante);
        }

        private void tspMenuFormacaoPrecoVenda_Click(object sender, EventArgs e)
        {
            FormFormacaoPrecoVenda formFormacaoPrecoVenda = new FormFormacaoPrecoVenda();
            AdicionarFormulario(formFormacaoPrecoVenda);
        }

        private void tspMenuSaidaDeEstoque_Click(object sender, EventArgs e)
        {
            FormSaidaEstoque formSaidaEstoque = new FormSaidaEstoque();

            AdicionarFormulario(formSaidaEstoque);
        }

        #endregion

        #region " EVENTOS MENU VENDAS "

        private void tspMenuPedidoDeVendas_Click(object sender, EventArgs e)
        {
            FormCadastroPedidoDeVenda formCadastroPedidoDeVenda = new FormCadastroPedidoDeVenda();

            AdicionarFormulario(formCadastroPedidoDeVenda);
        }

        private void tspMenuLiberacaoPedidoDeVendas_Click(object sender, EventArgs e)
        {
            FormLiberacaoPedidoDeVenda formLiberacaoPedidoDeVenda = new FormLiberacaoPedidoDeVenda();

            AdicionarFormulario(formLiberacaoPedidoDeVenda);
        }

        private void tspMenuConsultaPrecoProduto_Click(object sender, EventArgs e)
        {
            FormConsultaPrecoProduto formConsultaPrecoProduto = new FormConsultaPrecoProduto();

            AdicionarFormulario(formConsultaPrecoProduto);
        }

        private void tspMenuVendaRapida_Click(object sender, EventArgs e)
        {
            FormVendaRapida formVendaRapida = new FormVendaRapida(0);

            AdicionarFormulario(formVendaRapida);
        }

        private void tspMenuRecebimento_Click(object sender, EventArgs e)
        {
            FormPesquisaDocumentosRecebimento formPesquisaDocumentosRecebimento = new FormPesquisaDocumentosRecebimento();

            AdicionarFormulario(formPesquisaDocumentosRecebimento);
        }

        private void tspMenuTrocaPedidoDeVenda_Click(object sender, EventArgs e)
        {
            FormCadastroTrocaPedidoDeVenda formCadastroTrocaPedidoDeVenda = new FormCadastroTrocaPedidoDeVenda();
            AdicionarFormulario(formCadastroTrocaPedidoDeVenda);
        }

        private void tspMenuRelatorioCondicaoPagamento_Click(object sender, EventArgs e)
        {
            FormRelatorioCondicaoPagamento formRelatorioCondicaoPagamento = new FormRelatorioCondicaoPagamento();
            AdicionarFormulario(formRelatorioCondicaoPagamento);
        }

        #endregion

        #region " EVENTOS  MENU FRENTE DE LOJA "

        private void tspMenuPdv_Click(object sender, EventArgs e)
        {
            FormCaixaAbertoPdv formCaixaAbertoPdv = new FormCaixaAbertoPdv();
            formCaixaAbertoPdv.ShowDialog();
        }

        private void tspMenuExportarVendasPdvEcf_Click(object sender, EventArgs e)
        {
            FormExportaPedidoDeVendaPdvEcf formExportaPedidoDeVendaPdvEcf = new FormExportaPedidoDeVendaPdvEcf();

            AdicionarFormulario(formExportaPedidoDeVendaPdvEcf);
        }

        #endregion

        #region " EVENTOS MENU RELATÓRIOS "

        private void tspMenuRelatorioDeParceiros_Click(object sender, EventArgs e)
        {
            FormRelatorioParceiro FormRelatorioParceiro = new FormRelatorioParceiro();
            FormRelatorioParceiro.AbrirTelaModal();
        }

        private void tspMenuRelatorioClientesSemComprar_Click(object sender, EventArgs e)
        {
            FormRelatorioClientesSemComprar formRelatorioClientesSemComprar = new FormRelatorioClientesSemComprar();
            formRelatorioClientesSemComprar.AbrirTelaModal();
        }

        private void tspMenuRelatorioItens_Click(object sender, EventArgs e)
        {
            FormRelatorioProdutos formRelatorioProdutos = new FormRelatorioProdutos();
            formRelatorioProdutos.AbrirTelaModal();
        }

        private void tspMenuRelatorioContasAPagar_Click(object sender, EventArgs e)
        {
            FormRelatorioContasPagarReceber formRelatorioContasPagarReceber = new FormRelatorioContasPagarReceber(EnumTipoOperacaoContasPagarReceber.PAGAR);
            formRelatorioContasPagarReceber.AbrirTelaModal();
        }

        private void tspMenuRelatorioContasAReceber_Click(object sender, EventArgs e)
        {
            FormRelatorioContasPagarReceber formRelatorioContasPagarReceber = new FormRelatorioContasPagarReceber(EnumTipoOperacaoContasPagarReceber.RECEBER);
            formRelatorioContasPagarReceber.AbrirTelaModal();
        }

        private void tspMenuRelatorioVendas_Click(object sender, EventArgs e)
        {
            FormRelatorioVendasPorVendedor formRelatorioVendasPorVendedor = new FormRelatorioVendasPorVendedor();
            formRelatorioVendasPorVendedor.AbrirTelaModal();
        }

        private void tspMenuRelatorioVendasCliente_Click(object sender, EventArgs e)
        {
            FormRelatorioVendasPorClientes formRelatorioVendasPorVendedor = new FormRelatorioVendasPorClientes();
            formRelatorioVendasPorVendedor.AbrirTelaModal();
        }

        private void tspMenuConfiguracoesPdv_Click(object sender, EventArgs e)
        {
            FormConfiguracoesPdv formConfiguracoesPdv = new FormConfiguracoesPdv();

            AdicionarFormulario(formConfiguracoesPdv);
        }

        private void tspMenuRelatorioEntrada_Click(object sender, EventArgs e)
        {
            FormRelatorioEntrada formRelatorioEntrada = new FormRelatorioEntrada();
            formRelatorioEntrada.AbrirTelaModal();
        }

        private void tspMenuRelatorioMovimentacaoCaixa_Click(object sender, EventArgs e)
        {
            FormRelatorioMovimentacaoCaixa formRelatorioMovimentacaoCaixa = new FormRelatorioMovimentacaoCaixa();

            formRelatorioMovimentacaoCaixa.AbrirTelaModal();
        }

        private void tspMenuRelatorioMovimentacaoItens_Click(object sender, EventArgs e)
        {
            FormRelatorioMovimentacaoItens formRelatorioMovimentacaoItens = new FormRelatorioMovimentacaoItens();

            formRelatorioMovimentacaoItens.AbrirTelaModal();
        }

        private void tspMenuRelatorioDeItensVendidosEBaixados_Click(object sender, EventArgs e)
        {
            FormRelatorioMovimentacaoSaidaItens formRelatorioMovimentacaoSaidaItens = new FormRelatorioMovimentacaoSaidaItens();

            formRelatorioMovimentacaoSaidaItens.AbrirTelaModal();
        }

        private void tspMenuRelatorioDeTransportes_Click(object sender, EventArgs e)
        {
            FormRelatorioDeTransportes formRelatorioDeTransportes = new FormRelatorioDeTransportes();

            formRelatorioDeTransportes.AbrirTelaModal();
        }

        private void tspMenuRelatorioLocacao_Click(object sender, EventArgs e)
        {
            FormEtiquetasLocacao formEtiquetasLocacao = new FormEtiquetasLocacao();
            formEtiquetasLocacao.AbrirTelaModal();
        }


        #endregion

        #region " EVENTOS MENU ADM SISTEMA "

        private void tspMenuEmpresa_Click(object sender, EventArgs e)
        {
            FormCadastroEmpresa formCadastroEmpresa = new FormCadastroEmpresa();

            AdicionarFormulario(formCadastroEmpresa);
        }

        private void tspMenuUsuario_Click(object sender, EventArgs e)
        {
            FormCadastroUsuario formCadastroUsuario = new FormCadastroUsuario();

            AdicionarFormulario(formCadastroUsuario);
        }

        private void tspMenuGrupoDeAcesso_Click(object sender, EventArgs e)
        {
            FormCadastroGrupoAcesso formCadastroGrupoAcesso = new FormCadastroGrupoAcesso();

            AdicionarFormulario(formCadastroGrupoAcesso);
        }

        private void tspMenuPermissoes_Click(object sender, EventArgs e)
        {
            FormCadastroPermissoes formCadastroPermissoes = new FormCadastroPermissoes();

            AdicionarFormulario(formCadastroPermissoes);
        }

        private void tspMenuParametros_Click(object sender, EventArgs e)
        {
            FormCadastroParametros formCadastroParametros = new FormCadastroParametros();

            AdicionarFormulario(formCadastroParametros);
        }

        private void tspMenuSobre_Click(object sender, EventArgs e)
        {
            FormSobre formSobre = new FormSobre();

            AdicionarFormulario(formSobre);
        }

        private void tspMenuLicencaUso_Click(object sender, EventArgs e)
        {
            FormLicensaDeUso formLicensaDeUso = new FormLicensaDeUso();

            AdicionarFormulario(formLicensaDeUso);
        }

        private void tspMenuBackup_Click(object sender, EventArgs e)
        {
            FormBackup formBackup = new FormBackup();

            AdicionarFormulario(formBackup);
        }

        private void tspMenuAlterarSenha_Click(object sender, EventArgs e)
        {
            FormAlteracaoSenha formAlteracaoSenha = new FormAlteracaoSenha();
            AdicionarFormulario(formAlteracaoSenha);
        }

        private void tspMenuLiberacao_Click(object sender, EventArgs e)
        {
            FormLiberacao formLiberacao = new FormLiberacao();
            formLiberacao.ShowDialog();
        }

        private void tspMenuAnaliseCredito_Click(object sender, EventArgs e)
        {
            FormCadastroCrediario formCadastroAnaliseCredito = new FormCadastroCrediario();

            AdicionarFormulario(formCadastroAnaliseCredito);
        }

        #endregion

        #region " EVENTOS MENU OPÇÕES USUÁRIO "

        private void btnSairSistema_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair do Akil Small Business?", "Fechar Aplicacao", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnAlterarUsuario_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin(false);
            var resultado = formLogin.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                InicieSistema();
            }
            else
            {
                this.Close();
            }
        }

        private void btnSuporte_Click(object sender, EventArgs e)
        {
            FormSobre formSobre = new FormSobre();
            formSobre.ShowDialog();
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://manual.akilsb.programax.com.br/");
        }

        #endregion

        #region " EVENTOS RODAPÉ "

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://programax.com.br/");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void IniciarTimer()
        {
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 1000;            
            timer1.Tick += new EventHandler(timer1_Tick);
            
            if(btnResumoFinanceiro.Enabled)
            {
                if (_parametros.ParametrosFinanceiro.HabilitarResumoFinanceiro)
                {
                    if (_parametros.ParametrosFinanceiro.AbrirResumoFinanceiroAoIniciarAkil)
                    {
                        FormResumoFinanceiro FormResumo = new FormResumoFinanceiro();
                        FormResumo.Show();
                        //FormResumo.KeyPreview = true;
                        //FormResumo.TopLevel = true;
                        FormResumo.TopMost = true;
                        //FormResumo.BringToFront();                        
                        //FormResumo.Focus();
                    }
                }
            }
            
        }

        public void VerificarAtualizacao()
        {
            try
            {
                FtpWebRequest request = FtpWebRequest.Create(@"ftp://site1393677047@ftp.site1393677047.hospedagemdesites.ws" + "/" + "public_html/EXECUTAVEIS/Atualizacao/Akil_Atualizar.zip") as FtpWebRequest;
                request.Credentials = new NetworkCredential("site1393677047", "PHD@2018");

                //Get the DATE & TIME stamp of the file
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                var fileDateTime = response.LastModified;

                string caminhoACBR = "C:\\Programax";

                if (!string.IsNullOrEmpty(_parametros.ParametrosCadastros.CaminhoACBR))
                {
                    caminhoACBR = _parametros.ParametrosCadastros.CaminhoACBR;
                }

                ServicoInformacaoSistema informacoesSistema = new ServicoInformacaoSistema();

                var dadosInformacoesSistema = informacoesSistema.ConsulteUltimaInformacaoSistema();

                var DataHoraArquivoDir = dadosInformacoesSistema.DataVersao;

                var Versao = dadosInformacoesSistema.Versao;


                //if (DataHoraArquivoDir.AddDays(2) < fileDateTime)
                if (Versao != lblVersaoSistema.Text)
                {
                    if (MessageBox.Show("A versão do sistema está desatualizada. Deseja atualizar?", "Atualização do Sistema", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        FormAtualizacaoSistema formAtualizacao = new FormAtualizacaoSistema(lblVersaoSistema.Text);

                        formAtualizacao.Show();
                    }
                }
            }
            catch (Exception e)
            {
                return;               
            }
            
        }

        private void AdicionarFormulario(Form form)
        {
            var formAberto = _listaForms.FirstOrDefault(x => x.GetType() == form.GetType());

            if (formAberto != null && !formAberto.IsDisposed)
            {
                formAberto.WindowState = FormWindowState.Normal;
                formAberto.Focus();
            }
            else
            {
                if (formAberto != null && formAberto.IsDisposed)
                {
                    _listaForms.Remove(formAberto);
                }

                form.Show();
                _listaForms.Add(form);
            }
        }

        private void InicieSistema()
        {
            _listaForms = new List<Form>();

            if (panel2 != null)
            {
                this.Controls.Remove(panel2);
                panel2.Dispose();
            }

            InitializeComponent();

            panel2.Size = new System.Drawing.Size(this.Size.Width - 166, panel2.Size.Height);

            ServicoParametros servicoParametros = new ServicoParametros();
            _parametros = servicoParametros.ConsulteParametros();

            CarregueInformacoesUsuario();
            LibereMenus();
            VerifiqueEAvisoNcmForaDoPrazo();
        }

        private void LibereMenus()
        {
            for (int i = 0; i < menuStrip1.Items.Count; i++)
            {
                var menu = (ToolStripMenuItem)menuStrip1.Items[i];

                menu.Visible = true;
            }

            LibereNosFolhasDoMenu();

            LibereSubMenusQueContenhamSubMenus();

            LibereMenusRaizes();
            if(_Acgua == false)
            {
                tspMenuRoteiros.Visible = false;
                tspMenuTelemarketing.Visible = false;
                tspMenuGerenciarTmk.Visible = false;
                tspMenuConsultaRefiltek.Visible = false;
            }
            else
            {
                tspMenuRoteiros.Visible = true;
                tspMenuTelemarketing.Visible = true;
                tspMenuGerenciarTmk.Visible = true;
                tspMenuConsultaRefiltek.Visible = true;
            }
           
        }

        private void LibereNosFolhasDoMenu()
        {
            foreach (var permissao in Sessao.ListaDePermissoes)
            {
                if (string.IsNullOrWhiteSpace(permissao.NomeMenu))
                {
                    continue;
                }

                if (permissao.NomeMenu == "btnResumoFinanceiro")
                {
                    if (permissao != null && permissao.Acessar)
                    {
                        btnResumoFinanceiro.Enabled = true;
                    }
                    else
                    {
                        btnResumoFinanceiro.Enabled = false;
                    }
                }

                if (permissao.NomeMenu == "tspMenuConfigucaoBoletos")
                {
                    if (permissao != null && permissao.Acessar)
                    {
                        btnResumoFinanceiro.Enabled = true;
                    }
                    else
                    {
                        btnResumoFinanceiro.Enabled = false;
                    }
                }
                if (permissao.NomeMenu == "retornoDeBoletosToolStripMenuItem")
                {
                    if (permissao != null && permissao.Acessar)
                    {
                        btnResumoFinanceiro.Enabled = true;
                    }
                    else
                    {
                        btnResumoFinanceiro.Enabled = false;
                    }
                }
                //if (permissao.NomeMenu == "tspMenuNotasFiscais")
                //{
                //    if (permissao != null && permissao.Acessar)
                //    {
                //        btnResumoFinanceiro.Enabled = true;
                //    }
                //    else
                //    {
                //        btnResumoFinanceiro.Enabled = false;
                //    }
                //}

                var menu = menuStrip1.Items.Find(permissao.NomeMenu, true).ToList().FirstOrDefault();

                if (menu != null)
                {
                    if (permissao != null && permissao.Acessar)
                    {
                        menu.Visible = true;
                        menu.Enabled = true;
                    }
                    else
                    {
                        menu.Visible = false;
                        menu.Enabled = false;
                    }

                }
            }

            if (!_parametros.ParametrosCadastros.PermiteVendaDiretaNoPDV && (tspMenuPdv.Visible = true || tspMenuPdv.Enabled == true))
            {
                tspMenuPdv.Visible = false;
                tspMenuPdv.Enabled = false;
            }

            if (!_parametros.ParametrosVenda.PermiteBaixarEstoqueNaSaida)
            {
                tspMenuSaidaDeEstoque.Visible = false;
                tspMenuSaidaDeEstoque.Enabled = false;
            }
           
            if (!_parametros.ParametrosFinanceiro.HabilitarResumoFinanceiro)
            {
                btnResumoFinanceiro.Enabled = false;
            }
        }

        private void LibereSubMenusQueContenhamSubMenus()
        {
            foreach (ToolStripMenuItem item in tspSubMenuFinanceiro.DropDownItems)
            {
                if (item.Enabled)
                {
                    tspSubMenuFinanceiro.Visible = true;
                    tspSubMenuFinanceiro.Enabled = true;

                    //Conciliação Bancária
                    tspMenuConciliacaoBancaria.Visible = _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria? true : false;
                    tspMenuMovimentacoesBanco.Visible = _parametros.ParametrosFinanceiro.HabilitarConciliacaoBancaria ? true : false;
                }
                else
                {
                    tspSubMenuFinanceiro.Visible = false;
                    tspSubMenuFinanceiro.Enabled = false;

                    break;
                }
            }

            foreach (ToolStripMenuItem item in tspSubMenuEstoque.DropDownItems)
            {
                if (item.Enabled)
                {
                    tspSubMenuEstoque.Visible = true;
                    tspSubMenuEstoque.Enabled = true;
                }
                else
                {
                    tspSubMenuEstoque.Visible = false;
                    tspSubMenuEstoque.Enabled = false;

                    break;
                }
            }

            foreach (ToolStripMenuItem item in tspSubMenuFiscal.DropDownItems)
            {
                if (item.Enabled)
                {
                    tspSubMenuFiscal.Visible = true;
                    tspSubMenuFiscal.Enabled = true;
                }
                else
                {
                    tspSubMenuFiscal.Visible = false;
                    tspSubMenuFiscal.Enabled = false;

                    break;
                }
            }

            foreach (ToolStripMenuItem item in tspSubMenuVendas.DropDownItems)
            {
                if (item.Enabled)
                {
                    tspSubMenuVendas.Visible = true;
                    tspSubMenuVendas.Enabled = true;
                }
                else
                {
                    tspSubMenuVendas.Visible = false;
                    tspSubMenuVendas.Enabled = false;

                    break;
                }
            }
        }

        private void LibereMenusRaizes()
        {
            for (int i = 0; i < menuStrip1.Items.Count; i++)
            {
                var menu = (ToolStripMenuItem)menuStrip1.Items[i];

                bool deixarMenuVisivel = false;

                for (int j = 0; j < menu.DropDownItems.Count; j++)
                {
                    if (menu.DropDownItems[j].Enabled)
                    {
                        deixarMenuVisivel = true;

                        break;
                    }
                }

                menu.Visible = deixarMenuVisivel;
            }
        }

        private void CarregueInformacoesUsuario()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            ServicoLicencaDeUso servicoLicencaDeUso = new ServicoLicencaDeUso();
            var licencaDeUso = servicoLicencaDeUso.ConsulteUltimaLicencaDeUso();

            ServicoInformacaoSistema servicoInformacaoSistema = new ServicoInformacaoSistema();
            var informacaoSistema = servicoInformacaoSistema.ConsulteUltimaInformacaoSistema();

            informacaoSistema = informacaoSistema ?? new InformacaoSistema();

            switch (empresa.DadosEmpresa.Config)
            {
                case 0:
                    pictureBox1.Visible = true;
                    pictureBox3.Visible = false;
                    this.Text = " Akil Small Business";
                    _Acgua = false;
                    break;
                case 1:
                    pictureBox3.Visible = true;
                    pictureBox1.Visible = false;
                    this.Text = " Akil Acqua";
                    _Acgua = true;
                    break;
                default:
                    pictureBox1.Visible = true;
                    pictureBox3.Visible = false;
                    this.Text = " Akil Small Business";
                    _Acgua = false;
                    break;
            }

           
            lblEmpresa.Text = empresa.DadosEmpresa.NomeFantasia;
            lblUsuario.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.NomeFantasia;
            lblDataLicenca.Text = licencaDeUso.LiberadoAte.ToString("dd/MM/yyyy");
            //lblVersaoSistema.Text = informacaoSistema.Versao + " /Refiltek";
            lblVersaoSistema.Text = " 1.1.32.3";
            //VerificarAtualizacao();?

            //if (" " + empresa.DadosEmpresa.Versao.ToString() != lblVersaoSistema.Text.ToString())
            // {
            //     MessageBoxAkil.Show("O Sistema esta desatualizado.\n\nPor favor, atualize o sistema ou entre em contato com o suporte para atualizar!", "Sistema desatualizado");
            // }

        }

        private void VerifiqueEAvisoNcmForaDoPrazo()
        {
            if (_parametros.ParametrosFiscais != null && _parametros.ParametrosFiscais.AvisarQuandoHouverNcmForaDoPrazoValidade)
            {
                ServicoNcm servicoNcm = new ServicoNcm(false, false);
                bool existeAlgumNcmForaDoPrazoDeValidade = servicoNcm.ExisteAlgumNcmForaDoPrazoDeValidade();

                if (existeAlgumNcmForaDoPrazoDeValidade)
                {
                    MessageBoxAkil.Show("Sua tabela de ncm está desatualizada.\n\nPor favor, entre em contato com o suporte!", "Ncms desatualizados");
                }
            }
        }

        private void carregarImagemTelaInicial()
        {
            btnDelImagem.Enabled = false;
            if (File.Exists("C:/Programax/AkilTelaInicial"))
            {
                picFoto.Image = Image.FromFile("C:/Programax/AkilTelaInicial");
                btnDelImagem.Enabled = true;
            }
        }
               

        #endregion

        #region "EVENTOS CONTROLES"

        private void btnAddImagem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (picFoto != null)
                {
                    TratamentoDeImagens.BuscaImagem(picFoto, "Imagens|*.jpg");
                    btnAddImagem.Enabled = true;
                    btnDelImagem.Enabled = true;                    
                    picFoto.Image.Save(Path.GetFullPath("C:/Programax/AkilTelaInicial"), System.Drawing.Imaging.ImageFormat.Jpeg);
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
                        picFoto.Image.Dispose();
                        picFoto.Image = null;
                        if (File.Exists("C:/Programax/AkilTelaInicial"))
                            File.Delete("C:/Programax/AkilTelaInicial");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void btnRedimensionar_Click(object sender, EventArgs e)
        {
            if (_primeiraVez)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                this.Height = 192;
                this.TopMost = true;
                _primeiraVez = false;
                btnRedimensionar.Text = ">>";
            }
            else
            {   
                this.StartPosition = FormStartPosition.CenterScreen;
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.TopMost = false;
                _primeiraVez = true;
                btnRedimensionar.Text = "<<";
            }
            
        }

        private void btnAtualizarSistema_Click(object sender, EventArgs e)
        {   
            if (MessageBox.Show("O sistema irá fechar e atualizar. Poderá demorar alguns minutos... Deseja continuar?", "Atualizar Sistema", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            FormAtualizacaoSistema formAtualizacao = new FormAtualizacaoSistema(lblVersaoSistema.Text);

            formAtualizacao.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Interval > 1030)
            {
                Cursor = Cursors.WaitCursor;
                
                timer1.Stop();
                VerificarAtualizacao();

                Cursor = Cursors.Default;
            }

            timer1.Interval++;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void btnResumoFinanceiro_Click(object sender, EventArgs e)
        {
            FormResumoFinanceiro FormResumo = new FormResumoFinanceiro();            
            FormResumo.Show();
        }

        private void FormPrincipal_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if(Application.OpenForms.OfType<FormResumoFinanceiro>().Count() > 0)
                    Application.OpenForms["FormResumoFinanceiro"].WindowState = FormWindowState.Minimized;               
            }
            else if(this.WindowState == FormWindowState.Maximized)
            {
                if (Application.OpenForms.OfType<FormResumoFinanceiro>().Count() > 0)
                    Application.OpenForms["FormResumoFinanceiro"].WindowState = FormWindowState.Normal;
            }
        }
        
        private void gruposCategoriasFinanceirasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroGruposCategorias formGrupo = new FormCadastroGruposCategorias();
            formGrupo.Show();
        }

        private void categoriasFinanceirasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroCategoriasFinanceiras formCategoriasFinanceiras = new FormCadastroCategoriasFinanceiras();
            formCategoriasFinanceiras.Show();
        }

        private void operadarasDeCartãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroOperadorasCartao formCadastroOperadorasCartao = new FormCadastroOperadorasCartao();
            formCadastroOperadorasCartao.Show();
        }

        private void cadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroBanco formCadastroBanco = new FormCadastroBanco();

            AdicionarFormulario(formCadastroBanco);
        }

        private void agênciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroAgencia formCadastroAgencia = new FormCadastroAgencia();

            AdicionarFormulario(formCadastroAgencia);
        }

        private void contasBancáriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroContaBancaria formCadastroContaBancaria = new FormCadastroContaBancaria();

            AdicionarFormulario(formCadastroContaBancaria);
        }

        private void tspMenuRoteiros_Click(object sender, EventArgs e)
        {
          
            FormRoteirizacaoPesquisa formRoteiro = new FormRoteirizacaoPesquisa();
            AdicionarFormulario(formRoteiro);
                   
        }

        private void BancosParaMovimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroBancoParaMovimento formCadastoBancoMovimento = new FormCadastroBancoParaMovimento();

            formCadastoBancoMovimento.Show();
        }

        private void bancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroMovimentacoesBanco FormBancos = new FormCadastroMovimentacoesBanco();
            FormBancos.Show();
        }

        private void tspMenuConciliacaoBancaria_Click(object sender, EventArgs e)
        {
            int quantosItens=0;

            foreach (ToolStripMenuItem item in tspMenuConciliacaoBancaria.DropDownItems)
            {
                if (item.Visible)
                    return;
                else
                    quantosItens++;
            }

            if(quantosItens == tspMenuConciliacaoBancaria.DropDownItems.Count)
                MessageBox.Show("Para acessar os itens da <CONCILIAÇÃO BANCÁRIA> você precisa ter permissão.",
                                    "Acessar Conciliação Bancária", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fluxoCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRelatorioConciliacaoBancaria RelFluxo = new FormRelatorioConciliacaoBancaria();

            RelFluxo.Show();
        }

        private void menuEstoque_Click(object sender, EventArgs e)
        {

        }

        public void Fechar()
        {
            this.Close();
        }

        private void estornarEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEstornarEstoqueSaida formEstornar = new FormEstornarEstoqueSaida();

            AdicionarFormulario(formEstornar);
        }

        private void tspMenuPlanoDeContasDRE_Click(object sender, EventArgs e)
        {
            FormCadastroPlanoContaDre formCadastroPlanoContaDre = new FormCadastroPlanoContaDre();

            AdicionarFormulario(formCadastroPlanoContaDre);
        }

        private void vendasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tspMenuTelemarketing_Click(object sender, EventArgs e)
        {
            FormBuscaAtendimentos formTmk = new FormBuscaAtendimentos();
            AdicionarFormulario(formTmk);
        }

        private void tspMenuGerenciarTmk_Click(object sender, EventArgs e)
        {
           
            FormGerenciarAtendimentos formGerenciarTmk = new FormGerenciarAtendimentos();
            AdicionarFormulario(formGerenciarTmk);
        }

        private void tspMenuDRE_Click(object sender, EventArgs e)
        {
            FormRelatorioConciliacaoDRE formRelatorioConciliacaoDRE = new FormRelatorioConciliacaoDRE();
            AdicionarFormulario(formRelatorioConciliacaoDRE);
        }

        private void tspMenuConsultaRefiltek_Click(object sender, EventArgs e)
        {
            FormBuscaAtendimentosRefiltek formBuscaAtendimentosRefiltek = new FormBuscaAtendimentosRefiltek();
            AdicionarFormulario(formBuscaAtendimentosRefiltek);
        }

        private void tspMenuconfiguracaoCashBack_Click(object sender, EventArgs e)
        {
            FormCadastroCashBack formCadastroCashBack = new FormCadastroCashBack();
            AdicionarFormulario(formCadastroCashBack);
        }

        private void tspMenuCarteiras_Click(object sender, EventArgs e)
        {
            FormCarteiras formCarteiras = new FormCarteiras();
            AdicionarFormulario(formCarteiras);
        }

        private void tspMenuCategoriasDRE_Click(object sender, EventArgs e)
        {
            FormCadastroCategoriasDre formCadastroCategoriasDre = new FormCadastroCategoriasDre();
            AdicionarFormulario(formCadastroCategoriasDre);
        }

        private void àReceberToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tspMenuSpedFiscal_Click(object sender, EventArgs e)
        {
            FormSpedFiscal formSpedFiscal = new FormSpedFiscal();
            AdicionarFormulario(formSpedFiscal);

        }

        private void tspMenuEntradaDeMercadoriaManual_Click(object sender, EventArgs e)
        {
            FormEntradaMercadoriaManual formEntradaMercadoriaManual = new FormEntradaMercadoriaManual();
            AdicionarFormulario(formEntradaMercadoriaManual);
        }

        private void relatorioDeCustoFinanceiro_Click(object sender, EventArgs e)
        {
                FormRelatorioCustoFinanceiro formRelatorioCustoFinanceiro = new FormRelatorioCustoFinanceiro();
                AdicionarFormulario(formRelatorioCustoFinanceiro);
        }

        private void relatorioDeCustotodos_Click(object sender, EventArgs e)
        {
            FormRelatorioCustoFinanceirotodos formRelatorioCustoFinanceirotodos = new FormRelatorioCustoFinanceirotodos();
            AdicionarFormulario(formRelatorioCustoFinanceirotodos);
        }

        private void tspMenuSubEstoque_Click(object sender, EventArgs e)
        {
            FormCadastroSubEstoque formCadastroSubEstoque = new FormCadastroSubEstoque();
            AdicionarFormulario(formCadastroSubEstoque);
        }

        private void tspMenuTransferenciasubestoque_Click(object sender, EventArgs e)
        {
            FormCadastroTransferencia formCadastroTransferencia = new FormCadastroTransferencia();
            AdicionarFormulario(formCadastroTransferencia);
        }

        private void menuVendas_Click(object sender, EventArgs e)
        {

        }

        private void tspMenuCarteirasRefiltek_Click(object sender, EventArgs e)
        {
            FormCarteirasRefiltek formCarteirasRefiltek = new FormCarteirasRefiltek();
            AdicionarFormulario(formCarteirasRefiltek);
        }

        private void tspMenuverificacaoReservas_Click(object sender, EventArgs e)
        {
            FormReservasPedidos formReservasPedidos = new FormReservasPedidos();
            AdicionarFormulario(formReservasPedidos);
        }
    }
    
    #endregion

}
