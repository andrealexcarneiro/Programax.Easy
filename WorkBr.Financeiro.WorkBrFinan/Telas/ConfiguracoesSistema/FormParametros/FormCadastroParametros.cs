using System;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;
using System.Collections.Generic;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ComissaoServ;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using NFe.Utils;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.Cadastros.GrupoTributacaoIcmsServ;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using System.Windows.Forms;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.View.Telas.Cadastros.GrupoTributacoesIcms;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.Cadastros.GrupoTributacaoFederalServ;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.FormParametros
{
    public partial class FormCadastroParametros : FormularioPadrao
    {
        #region " CONSTRUTOR "

        public FormCadastroParametros()
        {
            InitializeComponent();

            PreenchaCboAtendentes();
            PreenchaCboVendedores();
            PreenchaCboTransportadoras();

            PreenchaCboTabelaPreco();
            PreenchaCboFormaPagamento();
            PreenchaCboCondicaoPagamento();

            PreenchaCboTipoFrete();

            PreenchaCboPrefixoEan13CodigoBarras();
            PreenchaCboTamanhoCodigoBarras();

            CarregueParametros();

            this.ActiveControl = txtPercentualDespesasFixasVenda;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Salve();
        }

        private void cboFormaPagamento_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCondicaoPagamento();
        }

        private void txtIdGrupoTributacaoIcms_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtIdGrupoTributacaoIcmsTerceiros.Text))
            {
                ServicoGrupoTributacaoIcms servicoGrupoTributacaoIcms = new ServicoGrupoTributacaoIcms();
                var grupoTributacaoIcms = servicoGrupoTributacaoIcms.ConsulteTributacaoTerceirosId(txtIdGrupoTributacaoIcmsTerceiros.Text.ToInt());

                PreenchaGrupoTributacaoIcmsTerceiros(grupoTributacaoIcms, true);
            }
            else
            {
                PreenchaGrupoTributacaoIcmsTerceiros(null);
            }
        }

        private void txtIdGrupoTributacaoIcmsProducaoPropria_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtIdGrupoTributacaoIcmsProducaoPropria.Text))
            {
                ServicoGrupoTributacaoIcms servicoGrupoTributacaoIcms = new ServicoGrupoTributacaoIcms();
                var grupoTributacaoIcms = servicoGrupoTributacaoIcms.ConsulteTributacaoProducaoPropriaId(txtIdGrupoTributacaoIcmsProducaoPropria.Text.ToInt());

                PreenchaGrupoTributacaoIcmsProducaoPropria(grupoTributacaoIcms, true);
            }
            else
            {
                PreenchaGrupoTributacaoIcmsProducaoPropria(null);
            }
        }

        private void btnGerarCsc_Click(object sender, EventArgs e)
        {
            ServicoParametros servicoParametros = new ServicoParametros();
            var csc = servicoParametros.GereCodigoCsc();

            txtIdCsc.Text = csc.IdCsc;
            txtCodigoCsc.Text = csc.CodigoCsc;
        }

        private void chkTrabalharComItensReservados_CheckedChanged(object sender, EventArgs e)
        {
            chkReserveEstoqueAoFaturarPedido.Enabled = chkTrabalharComItensReservados.Checked ? true : false;

            if (!chkReserveEstoqueAoFaturarPedido.Enabled)
                chkReserveEstoqueAoFaturarPedido.Checked = false;
        }

        private void pbPesquisarGrupoTributacaoIcmsTerceiros_Click(object sender, EventArgs e)
        {
            FormGrupoTributacaoIcmsPesquisa formGrupoTribucaoPesquisa = new FormGrupoTributacaoIcmsPesquisa();

            var GrupoTributacao = formGrupoTribucaoPesquisa.ExibaPesquisaDeGrupoDeTributacaoIcms();

            if (GrupoTributacao != null)
            {
                if ((EnumNaturezaProduto)GrupoTributacao.NaturezaProduto == EnumNaturezaProduto.TERCEIROS)
                    PreenchaGrupoTributacaoIcmsTerceiros(GrupoTributacao);
                else
                    PreenchaGrupoTributacaoIcmsProducaoPropria(GrupoTributacao);
            }
        }

        private void pbPesquisarGrupoTributacaoIcmsFabPropria_Click(object sender, EventArgs e)
        {
            FormGrupoTributacaoIcmsPesquisa formGrupoTribucaoPesquisa = new FormGrupoTributacaoIcmsPesquisa();

            var GrupoTributacao = formGrupoTribucaoPesquisa.ExibaPesquisaDeGrupoDeTributacaoIcms();

            if (GrupoTributacao != null)
            {
                if ((EnumNaturezaProduto)GrupoTributacao.NaturezaProduto == EnumNaturezaProduto.TERCEIROS)
                    PreenchaGrupoTributacaoIcmsTerceiros(GrupoTributacao);
                else
                    PreenchaGrupoTributacaoIcmsProducaoPropria(GrupoTributacao);
            }
        }

        private void pbPesquisarGrupoTributacaoFederalTerceiros_Click(object sender, EventArgs e)
        {
            FormGrupoTributacaoFederalPesquisa formGrupoTribucaoPesquisa = new FormGrupoTributacaoFederalPesquisa();

            var GrupoTributacaoFederal = formGrupoTribucaoPesquisa.ExibaPesquisaDeGrupoDeTributacaoFederal();

            if (GrupoTributacaoFederal != null)
            {
                if ((EnumNaturezaProduto)GrupoTributacaoFederal.NaturezaProduto == EnumNaturezaProduto.TERCEIROS)
                    PreenchaGrupoTributacaoFederalTerceiros(GrupoTributacaoFederal);
                else
                    PreenchaGrupoTributacaoFederalProducaoPropria(GrupoTributacaoFederal);
            }
        }

        private void pbPesquisarGrupoTributacaoFederalFabPropria_Click(object sender, EventArgs e)
        {
            FormGrupoTributacaoFederalPesquisa formGrupoTribucaoPesquisa = new FormGrupoTributacaoFederalPesquisa();

            var GrupoTributacaoFederal = formGrupoTribucaoPesquisa.ExibaPesquisaDeGrupoDeTributacaoFederal();

            if (GrupoTributacaoFederal != null)
            {
                if ((EnumNaturezaProduto)GrupoTributacaoFederal.NaturezaProduto == EnumNaturezaProduto.TERCEIROS)
                    PreenchaGrupoTributacaoFederalTerceiros(GrupoTributacaoFederal);
                else
                    PreenchaGrupoTributacaoFederalProducaoPropria(GrupoTributacaoFederal);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboPrefixoEan13CodigoBarras()
        {
            List<ObjetoDescricaoValor> lista = new List<ObjetoDescricaoValor>();

            for (int i = 0; i <= 9; i++)
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor();
                objetoDescricaoValor.Descricao = i.ToString();
                objetoDescricaoValor.Valor = i;

                lista.Add(objetoDescricaoValor);
            }

            cboPrefixoEan13CodigoBarras.Properties.DataSource = lista;
            cboPrefixoEan13CodigoBarras.Properties.ValueMember = "Valor";
            cboPrefixoEan13CodigoBarras.Properties.DisplayMember = "Descricao";

            cboPrefixoEan13CodigoBarras.EditValue = 2;
        }

        private void PreenchaCboTamanhoCodigoBarras()
        {
            List<ObjetoDescricaoValor> lista = new List<ObjetoDescricaoValor>();

            for (int i = 4; i <= 6; i++)
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor();
                objetoDescricaoValor.Descricao = i.ToString();
                objetoDescricaoValor.Valor = i;

                lista.Add(objetoDescricaoValor);
            }

            cboTamanhoCodigoEan13CodigoBarras.Properties.DataSource = lista;
            cboTamanhoCodigoEan13CodigoBarras.Properties.ValueMember = "Valor";
            cboTamanhoCodigoEan13CodigoBarras.Properties.DisplayMember = "Descricao";

            cboTamanhoCodigoEan13CodigoBarras.EditValue = 6;
        }

        private void PreenchaCboAtendentes()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaAtendentesAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboAtendentes.Properties.DisplayMember = "Descricao";
            cboAtendentes.Properties.ValueMember = "Valor";
            cboAtendentes.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboAtendentes.Text))
            {
                cboAtendentes.EditValue = null;
            }
        }

        private void PreenchaCboVendedores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaVendedoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboVendedores.Properties.DisplayMember = "Descricao";
            cboVendedores.Properties.ValueMember = "Valor";
            cboVendedores.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboVendedores.Text))
            {
                cboVendedores.EditValue = null;
            }
        }

        private void PreenchaCboTransportadoras()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaTransportadorasAtivas();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboTransportadoras.Properties.DisplayMember = "Descricao";
            cboTransportadoras.Properties.ValueMember = "Valor";
            cboTransportadoras.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboTransportadoras.Text))
            {
                cboTransportadoras.EditValue = null;
            }
        }

        private void PreenchaCboTabelaPreco()
        {
            ServicoTabelaPreco servioTabelaPreco = new ServicoTabelaPreco();
            var listaTabelaPreco = servioTabelaPreco.ConsulteListaTabelaPrecosAtivas();

            listaTabelaPreco.Insert(0, null);

            cboTabelaPrecos.Properties.DisplayMember = "NomeTabela";
            cboTabelaPrecos.Properties.ValueMember = "Id";
            cboTabelaPrecos.Properties.DataSource = listaTabelaPreco;

            if (string.IsNullOrEmpty(cboTabelaPrecos.Text))
            {
                cboTabelaPrecos.EditValue = null;
            }
        }

        private void PreenchaCboFormaPagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            var lista = servicoFormaPagamento.ConsulteListaAtivos();

            lista.Insert(0, null);

            cboFormaPagamento.Properties.DisplayMember = "Descricao";
            cboFormaPagamento.Properties.ValueMember = "Id";
            cboFormaPagamento.Properties.DataSource = lista;

            if (string.IsNullOrEmpty(cboFormaPagamento.Text))
            {
                cboFormaPagamento.EditValue = null;
            }
        }

        private void PreenchaCboCondicaoPagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            var formaPagamento = servicoFormaPagamento.Consulte(cboFormaPagamento.EditValue.ToInt());

            List<CondicaoPagamento> listaCondicoes = new List<CondicaoPagamento>();

            if (formaPagamento != null &&
                formaPagamento.ListaCondicoesPagamento != null &&
                formaPagamento.ListaCondicoesPagamento.Count > 0)
            {
                foreach (var item in formaPagamento.ListaCondicoesPagamento)
                {
                    if (item.CondicaoPagamento.Status == "A")
                    {
                        listaCondicoes.Add(item.CondicaoPagamento);
                    }
                }
            }

            listaCondicoes.Insert(0, null);

            cboCondicaoPagamento.Properties.DisplayMember = "Descricao";
            cboCondicaoPagamento.Properties.ValueMember = "Id";
            cboCondicaoPagamento.Properties.DataSource = listaCondicoes;

            if (string.IsNullOrEmpty(cboCondicaoPagamento.Text))
            {
                cboCondicaoPagamento.EditValue = null;
            }
        }

        private void PreenchaCboTipoFrete()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoFrete>();

            cboTipoFrete.Properties.DataSource = lista;
            cboTipoFrete.Properties.DisplayMember = "Descricao";
            cboTipoFrete.Properties.ValueMember = "Valor";

            cboTipoFrete.EditValue = EnumTipoFrete.SEMCOBRANCADEFRETE;
        }

        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

            var parametros = servicoParametros.ConsulteParametros();

            CarregueParametrosCadastros(parametros.ParametrosCadastros);
            CarregueParametrosFinanceiro(parametros.ParametrosFinanceiro);
            CarregueParametrosFiscais(parametros.ParametrosFiscais);
            CarregueParametrosVenda(parametros.ParametrosVenda);
            

            if (parametros.ParametrosCadastros.PermiteVendaDiretaNoPDV)
            {
                grpCodigoBarrasBalanca.Visible = true;
            }
            else
            {
                grpCodigoBarrasBalanca.Visible = false;
            }
        }

        private void CarregueParametrosCadastros(ParametrosCadastros parametrosCadastros)
        {
            chkPermitirCadastroParceirosComMesmoCpfCnpj.Checked = parametrosCadastros.PermiteCadastroParceiroComMesmoCpfCnpj;
            chkPermitirSalvarParceiroSemCpfCnpj.Checked = parametrosCadastros.PermiteCadastroParceiroSemCpfCnpj;
            chkPermitirVendaDiretaNoPDV.Checked = parametrosCadastros.PermiteVendaDiretaNoPDV;

            chkValidaEndereco.Checked = parametrosCadastros.ValidaEndereco;

            txtDiretorioACBR.Text = parametrosCadastros.CaminhoACBR;

            //Liberar campo Quantidade - saída de estoque
            chkLiberarCampoQtde.Checked = parametrosCadastros.LiberarCampoQtde;
                        
            chkValorVendaManual.Checked = parametrosCadastros.ValorVendaManual;
            
            //Abrir campo Quantidade de Estoque no Cadastro de Itens
            chkAbrirQuantEstoqueItens.Checked = parametrosCadastros.AbrirQuantEstoqueItens;

            //Mostrar campo Grupo Tributação na pesquisa de itens
            chkMostrarGrupoTribPesquisaItens.Checked = parametrosCadastros.MostrarGrupoTribPesquisaItens;

            cboPrefixoEan13CodigoBarras.EditValue = parametrosCadastros.PrefixoEan13CodigoBarras;
            cboTamanhoCodigoEan13CodigoBarras.EditValue = parametrosCadastros.TamahoCodigoBarras;

            if (parametrosCadastros.TipoCodigoBarrasBalanca == EnumTipoCodigoBarrasBalanca.VALORTOTAL)
            {
                rdbValorTotalCodigoBarras.Checked = true;
            }
            else
            {
                rdbPesoCodigoBarras.Checked = true;
            }

            if (parametrosCadastros.VinculoProdutoCodigoBarrasBalanca == EnumVinculoProdutoCodigoBarrasBalanca.CODIGOPRODUTO)
            {
                rdbCodigoBarrasBalancaCodigoProduto.Checked = true;
            }
            else
            {
                rdbCodigoBarrasBalancaCodigoBarrasProduto.Checked = true;
            }
        }

        private void CarregueParametrosFinanceiro(ParametrosFinanceiro parametrosFinanceiro)
        {
            txtPercentualComissoesVenda.Text = parametrosFinanceiro.PercentualComissoes.ToString("0.00");
            txtPercentualDespesasFixasVenda.Text = parametrosFinanceiro.PercentualDespesasFixas.ToString("0.00");
            txtPercentualDespesasVariaveis.Text = parametrosFinanceiro.PercentualDespesasVariaveis.ToString("0.00");
            txtPercentualFreteVenda.Text = parametrosFinanceiro.PercentualFrete.ToString("0.00");
            txtPercentualImpostos.Text = parametrosFinanceiro.PercentualImpostos.ToString("0.00");
            txtPercentualLucro.Text = parametrosFinanceiro.PercentualLucro.ToString("0.00");
            txtPercentualOutrasDespesasVenda.Text = parametrosFinanceiro.PercentualOutrasDespesas.ToString("0.00");

            txtMultaContasReceber.Text = parametrosFinanceiro.MultaContasReceber.ToString("0.00");
            txtJurosContasReceber.Text = parametrosFinanceiro.JurosContasReceber.ToString("0.00");

            chkQuestionarSeDesejaEmitirNotaAoReceberPedido.Checked = parametrosFinanceiro.QuestionarSeDesejaEmitirNotaAoReceberPedido;
            chkQuestionaExportarPdv.Checked = parametrosFinanceiro.QuestionarSeDesejaExportarVendaParaPdv;

            chkIgnorarCreditoInicial.Checked = parametrosFinanceiro.IgnorarCreditoInicial;

            chkHabilitarResumoFinanceiro.Checked = parametrosFinanceiro.HabilitarResumoFinanceiro;
            chkAbrirResumoFinanceiroAoIniciarAkil.Checked = parametrosFinanceiro.AbrirResumoFinanceiroAoIniciarAkil;

            //Conciliação Bancária
            chkHabilitarConciliacaoBancaria.Checked = parametrosFinanceiro.HabilitarConciliacaoBancaria;
            chkImportacaoAutomatica.Checked = parametrosFinanceiro.ImportacaoAutomaticaExtrato;
            txtDiasAntes.Text = parametrosFinanceiro.DiasAntes.ToString();
            txtDiasDepois.Text = parametrosFinanceiro.DiasDepois.ToString();

            txtValorPadraoCreditoInicial.Text = parametrosFinanceiro.ValoPadraoCreditoInicial.ToString("0.00");
            txtObservacoesCarnePagamento.Text = parametrosFinanceiro.ObservacoesCarnePagamento;

        }

        private void CarregueParametrosVenda(ParametrosVenda parametrosVenda)
        {
            if (parametrosVenda != null)
            {
                chkTelefonePedido.Checked = parametrosVenda.ExibirTelefonePedido;
                chkinfoPedido.Checked = parametrosVenda.ExibirInfoPedido;
                chkAlterarAtendente.Checked = parametrosVenda.PermiteAlterarAtendente;
                chkAlterarIndicador.Checked = parametrosVenda.PermiteAlterarIndicador;
                chkAlterarSupervisor.Checked = parametrosVenda.PermiteAlterarSupervisor;
                chkAlterarVendedor.Checked = parametrosVenda.PermiteAlterarVendedor;
                chkAlterarValorUnitario.Checked = parametrosVenda.PermiteAlterarValorUnitario;
                chkExibirTodasTabelasPrecosPedidoVenda.Checked = parametrosVenda.ExibirTodasAsTabelasPrecoPedidoVenda;
                chkPermiteDescontoNoTotalVenda.Checked = parametrosVenda.PermiteDescontoNoTotalVenda;

                chkPermiteMostrarValorVenda.Checked = parametrosVenda.PermiteMostrarValorVenda;

                chkAlterarValorUnitarioVendaRapida.Checked = parametrosVenda.PermiteAlterarValorUnitarioVendaRapida;
                chkPermiteBaixarEstoqueNaSaida.Checked = parametrosVenda.PermiteBaixarEstoqueNaSaida;
                chkNaoAceitarEstoqueNegativo.Checked = parametrosVenda.NaoAceitarEstoqueNegativo;
                chkReserveEstoqueAoFaturarPedido.Checked = parametrosVenda.ReserveEstoqueAoFaturarPedido;
                chkTrabalharComItensReservados.Checked = parametrosVenda.TrabalharComEstoqueReservado;
                chkImpressoraTermica.Checked = parametrosVenda.PedidoEmImpressoraTermica;

                chkImprimirDuasVias.Checked = parametrosVenda.PedidoEmDuasVias;

                chkPedidosPorVendedor.Checked = parametrosVenda.PedidosPorVendedor;
                chkbaixarpedidosfaturados.Checked = parametrosVenda.BaixarFaturamento;

                chkTodastabelas.Checked = parametrosVenda.ExibirTodasAsTabelasPrecoVendaRapida;
                chkAproveitarEnderecoEstabelecimentoParaCadastroRapidoCliente.Checked = parametrosVenda.AproveitarEnderecoEmpresaParaCadastroRapidoCliente;
                chkStatusConcluido.Checked = parametrosVenda.StatusFaturado;
                chkReservaportitem.Checked = parametrosVenda.ReservaItemPedido;


                cboAtendentes.EditValue = parametrosVenda.Atendente != null ? (int?)parametrosVenda.Atendente.Id : null;
                cboVendedores.EditValue = parametrosVenda.Vendedor != null ? (int?)parametrosVenda.Vendedor.Id : null;
                cboTransportadoras.EditValue = parametrosVenda.Transportadora != null ? (int?)parametrosVenda.Transportadora.Id : null;
                cboFormaPagamento.EditValue = parametrosVenda.FormaPagamento != null ? (int?)parametrosVenda.FormaPagamento.Id : null;
                cboCondicaoPagamento.EditValue = parametrosVenda.CondicaoPagamento != null ? (int?)parametrosVenda.CondicaoPagamento.Id : null;
                cboTabelaPrecos.EditValue = parametrosVenda.TabelaPreco != null ? (int?)parametrosVenda.TabelaPreco.Id : null;
                cboTipoFrete.EditValue = parametrosVenda.TipoFrete;
                
                txtObservacoesVendaRapida.Text = parametrosVenda.ObservacoesVendaRapida;

                txtLimiteManha.Text = parametrosVenda.LimiteDiarioManha.ToString();
                txtLimiteTarde.Text = parametrosVenda.LimiteDiarioTarde.ToString();

                txtnNomeContrato.Text = parametrosVenda.NomeContrato;
                txtTermosContrato.Text = parametrosVenda.TermosContrato;

            }
        }
        
        private void CarregueParametrosFiscais(ParametrosFiscais parametrosFiscais)
        {
            if (parametrosFiscais != null)
            {
                PreenchaGrupoTributacaoIcmsProducaoPropria(parametrosFiscais.GrupoTributacaoIcmsProducaoPropria);
                PreenchaGrupoTributacaoIcmsTerceiros(parametrosFiscais.GrupoTributacaoIcmsTerceiros);

                PreenchaGrupoTributacaoFederalProducaoPropria(parametrosFiscais.GrupoTributacaoFederalProducaoPropria);
                PreenchaGrupoTributacaoFederalTerceiros(parametrosFiscais.GrupoTributacaoFederalTerceiros);

                txtIdCsc.Text = parametrosFiscais.IdCsc;
                txtCodigoCsc.Text = parametrosFiscais.CodigoCsc;

                chkCalcularPartilhaIcms.Checked = parametrosFiscais.CalcularPartilhaIcms;

                chkCalculeFCP.Checked = parametrosFiscais.CalcularFCP;

                chkAvisarNcmForaPrazoValidade.Checked = parametrosFiscais.AvisarQuandoHouverNcmForaDoPrazoValidade;

                chkEmitirSemReceber.Checked = parametrosFiscais.EmitirNotaSemReceber;

                txtObservacoesGeraisNotaFiscal.Text = parametrosFiscais.ObservacoesGeraisNotaFiscal;
            }
            else
            {
                PreenchaGrupoTributacaoIcmsProducaoPropria(null);
                PreenchaGrupoTributacaoIcmsTerceiros(null);

                PreenchaGrupoTributacaoFederalProducaoPropria(null);
                PreenchaGrupoTributacaoFederalTerceiros(null);

                chkCalcularPartilhaIcms.Checked = false;

                chkCalculeFCP.Checked = false;
            }
        }

        private void Salve()
        {
            Action actionSalvar = () =>
            {
                var parametros = RetorneParametrosEmEdicao();

                ServicoParametros servicoParametros = new ServicoParametros();

                servicoParametros.Cadastre(parametros);                
            };            
            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Os parâmetros foram atualizados com sucesso!", tituloMensagemDeSucesso: "Parâmetros atualizados");
            CarregueParametros();
        }

        private Parametros RetorneParametrosEmEdicao()
        {
            Parametros parametros = new Parametros();

            parametros.ParametrosCadastros = RetorneParametrosCadastroEmEdicao();
            parametros.ParametrosFinanceiro = RetorneParametrosFinanceiroEmEdicao();
            parametros.ParametrosVenda = RetorneParametrosVendaEmEdicao();
            parametros.ParametrosFiscais = RetorneParametrosFiscaisemEdicao();

            return parametros;
        }

        private ParametrosCadastros RetorneParametrosCadastroEmEdicao()
        {
            ParametrosCadastros parametrosCadastros = new ParametrosCadastros();

            parametrosCadastros.PermiteCadastroParceiroComMesmoCpfCnpj = chkPermitirCadastroParceirosComMesmoCpfCnpj.Checked;
            parametrosCadastros.PermiteCadastroParceiroSemCpfCnpj = chkPermitirSalvarParceiroSemCpfCnpj.Checked;
            parametrosCadastros.PermiteVendaDiretaNoPDV = chkPermitirVendaDiretaNoPDV.Checked;

            parametrosCadastros.ValorVendaManual = chkValorVendaManual.Checked;
            parametrosCadastros.LiberarCampoQtde = chkLiberarCampoQtde.Checked;

            //Abrir campo Quantidade de Estoque no Cadastro de Itens
            parametrosCadastros.AbrirQuantEstoqueItens = chkAbrirQuantEstoqueItens.Checked;

            //Mostrar campo Grupo Tributação na pesquisa de itens
            parametrosCadastros.MostrarGrupoTribPesquisaItens = chkMostrarGrupoTribPesquisaItens.Checked;

            parametrosCadastros.ValidaEndereco = chkValidaEndereco.Checked;

            parametrosCadastros.CaminhoACBR = txtDiretorioACBR.Text;

            parametrosCadastros.PrefixoEan13CodigoBarras = cboPrefixoEan13CodigoBarras.EditValue.ToInt();
            parametrosCadastros.TamahoCodigoBarras = cboTamanhoCodigoEan13CodigoBarras.EditValue.ToInt();

            if (rdbValorTotalCodigoBarras.Checked)
            {
                parametrosCadastros.TipoCodigoBarrasBalanca = EnumTipoCodigoBarrasBalanca.VALORTOTAL;
            }
            else
            {
                parametrosCadastros.TipoCodigoBarrasBalanca = EnumTipoCodigoBarrasBalanca.PESO;
            }

            if (rdbCodigoBarrasBalancaCodigoProduto.Checked)
            {
                parametrosCadastros.VinculoProdutoCodigoBarrasBalanca = EnumVinculoProdutoCodigoBarrasBalanca.CODIGOPRODUTO;
            }
            else
            {
                parametrosCadastros.VinculoProdutoCodigoBarrasBalanca = EnumVinculoProdutoCodigoBarrasBalanca.CODIGOBARRASPRODUTO;
            }

            return parametrosCadastros;
        }

        private ParametrosFinanceiro RetorneParametrosFinanceiroEmEdicao()
        {
            ParametrosFinanceiro parametrosFinanceiro = new ParametrosFinanceiro();

            parametrosFinanceiro.PercentualComissoes = txtPercentualComissoesVenda.Text.ToDouble();
            parametrosFinanceiro.PercentualDespesasFixas = txtPercentualDespesasFixasVenda.Text.ToDouble();
            parametrosFinanceiro.PercentualDespesasVariaveis = txtPercentualDespesasVariaveis.Text.ToDouble();
            parametrosFinanceiro.PercentualFrete = txtPercentualFreteVenda.Text.ToDouble();
            parametrosFinanceiro.PercentualImpostos = txtPercentualImpostos.Text.ToDouble();
            parametrosFinanceiro.PercentualLucro = txtPercentualLucro.Text.ToDouble();
            parametrosFinanceiro.PercentualOutrasDespesas = txtPercentualOutrasDespesasVenda.Text.ToDouble();

            parametrosFinanceiro.MultaContasReceber = txtMultaContasReceber.Text.ToDouble();
            parametrosFinanceiro.JurosContasReceber = txtJurosContasReceber.Text.ToDouble();

            parametrosFinanceiro.QuestionarSeDesejaEmitirNotaAoReceberPedido = chkQuestionarSeDesejaEmitirNotaAoReceberPedido.Checked;
            parametrosFinanceiro.QuestionarSeDesejaExportarVendaParaPdv = chkQuestionaExportarPdv.Checked;

            parametrosFinanceiro.IgnorarCreditoInicial = chkIgnorarCreditoInicial.Checked;

            parametrosFinanceiro.HabilitarResumoFinanceiro = chkHabilitarResumoFinanceiro.Checked;
            parametrosFinanceiro.AbrirResumoFinanceiroAoIniciarAkil = chkAbrirResumoFinanceiroAoIniciarAkil.Checked;

            //Conciliacao Bancária
            parametrosFinanceiro.HabilitarConciliacaoBancaria = chkHabilitarConciliacaoBancaria.Checked;
            parametrosFinanceiro.ImportacaoAutomaticaExtrato = chkImportacaoAutomatica.Checked;
            parametrosFinanceiro.DiasAntes = txtDiasAntes.Text.ToInt();
            parametrosFinanceiro.DiasDepois = txtDiasDepois.Text.ToInt();

            parametrosFinanceiro.ValoPadraoCreditoInicial = txtValorPadraoCreditoInicial.Text.ToDouble();

            parametrosFinanceiro.ObservacoesCarnePagamento = txtObservacoesCarnePagamento.Text;

            return parametrosFinanceiro;
        }

        private ParametrosVenda RetorneParametrosVendaEmEdicao()
        {
            ParametrosVenda parametrosVenda = new ParametrosVenda();

            parametrosVenda.PermiteAlterarAtendente = chkAlterarAtendente.Checked;
            parametrosVenda.PermiteAlterarIndicador = chkAlterarIndicador.Checked;
            parametrosVenda.PermiteAlterarSupervisor = chkAlterarSupervisor.Checked;
            parametrosVenda.PermiteAlterarVendedor = chkAlterarVendedor.Checked;
            parametrosVenda.PermiteAlterarValorUnitario = chkAlterarValorUnitario.Checked;
            parametrosVenda.ExibirTodasAsTabelasPrecoPedidoVenda = chkExibirTodasTabelasPrecosPedidoVenda.Checked;
            parametrosVenda.AproveitarEnderecoEmpresaParaCadastroRapidoCliente = chkAproveitarEnderecoEstabelecimentoParaCadastroRapidoCliente.Checked;
            parametrosVenda.PermiteDescontoNoTotalVenda = chkPermiteDescontoNoTotalVenda.Checked;

            parametrosVenda.PermiteMostrarValorVenda = chkPermiteMostrarValorVenda.Checked;

            parametrosVenda.PermiteAlterarValorUnitarioVendaRapida = chkAlterarValorUnitarioVendaRapida.Checked;
            parametrosVenda.PermiteBaixarEstoqueNaSaida = chkPermiteBaixarEstoqueNaSaida.Checked;
            parametrosVenda.NaoAceitarEstoqueNegativo = chkNaoAceitarEstoqueNegativo.Checked;
            parametrosVenda.ReserveEstoqueAoFaturarPedido = chkReserveEstoqueAoFaturarPedido.Checked;
            parametrosVenda.TrabalharComEstoqueReservado = chkTrabalharComItensReservados.Checked;
            parametrosVenda.PedidoEmImpressoraTermica = chkImpressoraTermica.Checked;

            parametrosVenda.PedidoEmDuasVias = chkImprimirDuasVias.Checked;

            parametrosVenda.PedidosPorVendedor = chkPedidosPorVendedor.Checked;

            parametrosVenda.ExibirTodasAsTabelasPrecoVendaRapida = chkTodastabelas.Checked;
            parametrosVenda.Atendente = cboAtendentes.EditValue != null ? new Pessoa { Id = cboAtendentes.EditValue.ToInt() } : null;
            parametrosVenda.Vendedor = cboVendedores.EditValue != null ? new Pessoa { Id = cboVendedores.EditValue.ToInt() } : null;
            parametrosVenda.Transportadora = cboTransportadoras.EditValue != null ? new Pessoa { Id = cboTransportadoras.EditValue.ToInt() } : null;

            parametrosVenda.TabelaPreco = cboTabelaPrecos.EditValue != null ? new TabelaPreco { Id = cboTabelaPrecos.EditValue.ToInt() } : null;
            parametrosVenda.FormaPagamento = cboFormaPagamento.EditValue != null ? new FormaPagamento { Id = cboFormaPagamento.EditValue.ToInt() } : null;
            parametrosVenda.CondicaoPagamento = cboCondicaoPagamento.EditValue != null ? new CondicaoPagamento { Id = cboCondicaoPagamento.EditValue.ToInt() } : null;

            parametrosVenda.ObservacoesVendaRapida = txtObservacoesVendaRapida.Text;
            parametrosVenda.TipoFrete = (EnumTipoFrete)cboTipoFrete.EditValue;

            parametrosVenda.LimiteDiarioManha = txtLimiteManha.Text.ToInt();
            parametrosVenda.LimiteDiarioTarde = txtLimiteTarde.Text.ToInt();

            parametrosVenda.NomeContrato = txtnNomeContrato.Text;

            parametrosVenda.TermosContrato = txtTermosContrato.Text;

            parametrosVenda.ExibirTelefonePedido = chkTelefonePedido.Checked;
            parametrosVenda.ExibirInfoPedido = chkinfoPedido.Checked;
            parametrosVenda.BaixarFaturamento = chkbaixarpedidosfaturados.Checked;
            parametrosVenda.StatusFaturado = chkStatusConcluido.Checked;
            parametrosVenda.ReservaItemPedido = chkReservaportitem.Checked;

            return parametrosVenda;
        }

        private ParametrosFiscais RetorneParametrosFiscaisemEdicao()
        {
            ParametrosFiscais parametrosFiscais = new ParametrosFiscais();

            parametrosFiscais.GrupoTributacaoIcmsTerceiros = !string.IsNullOrEmpty(txtIdGrupoTributacaoIcmsTerceiros.Text) ? new GrupoTributacaoIcms { Id = txtIdGrupoTributacaoIcmsTerceiros.Text.ToInt() } : null;
            parametrosFiscais.GrupoTributacaoIcmsProducaoPropria = !string.IsNullOrEmpty(txtIdGrupoTributacaoIcmsProducaoPropria.Text) ? new GrupoTributacaoIcms { Id = txtIdGrupoTributacaoIcmsProducaoPropria.Text.ToInt() } : null;

            parametrosFiscais.GrupoTributacaoFederalTerceiros = !string.IsNullOrEmpty(txtIdGrupoTributacaoFederalTerceiros.Text) ? new GrupoTributacaoFederal { Id = txtIdGrupoTributacaoFederalTerceiros.Text.ToInt() } : null;
            parametrosFiscais.GrupoTributacaoFederalProducaoPropria = !string.IsNullOrEmpty(txtIdGrupoTributacaoFederalProducaoPropria.Text) ? new GrupoTributacaoFederal { Id = txtIdGrupoTributacaoFederalProducaoPropria.Text.ToInt() } : null;

            parametrosFiscais.CodigoCsc = txtCodigoCsc.Text;
            parametrosFiscais.IdCsc = txtIdCsc.Text;

            parametrosFiscais.CalcularPartilhaIcms = chkCalcularPartilhaIcms.Checked;

            parametrosFiscais.CalcularFCP = chkCalculeFCP.Checked;

            parametrosFiscais.EmitirNotaSemReceber = chkEmitirSemReceber.Checked;

            parametrosFiscais.ObservacoesGeraisNotaFiscal = txtObservacoesGeraisNotaFiscal.Text;

            return parametrosFiscais;
        }

        private void PreenchaGrupoTributacaoIcmsTerceiros(GrupoTributacaoIcms grupoTributacaoIcms, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (grupoTributacaoIcms != null)
            {
                txtIdGrupoTributacaoIcmsTerceiros.Text = grupoTributacaoIcms.Id.ToString();
                txtDescricaoGrupoTributacaoIcmsTerceiros.Text = grupoTributacaoIcms.Descricao;
            }
            else
            {
                txtIdGrupoTributacaoIcmsTerceiros.Text = string.Empty;
                txtDescricaoGrupoTributacaoIcmsTerceiros.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Grupo de Tributação nao encontrado!", "Grupo de Tributação não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtIdGrupoTributacaoIcmsTerceiros.Focus();
                }
            }
        }

        private void PreenchaGrupoTributacaoFederalTerceiros(GrupoTributacaoFederal grupoTributacaoFederal, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (grupoTributacaoFederal != null)
            {
                txtIdGrupoTributacaoFederalTerceiros.Text = grupoTributacaoFederal !=null? grupoTributacaoFederal.Id.ToString():string.Empty;

                txtDescricaoGrupoTributacaoFederalTerceiros.Text = grupoTributacaoFederal != null ? grupoTributacaoFederal.Descricao:string.Empty;
            }
            else
            {
                txtIdGrupoTributacaoFederalTerceiros.Text = string.Empty;
                txtDescricaoGrupoTributacaoFederalTerceiros.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Grupo de Tributação nao encontrado!", "Grupo de Tributação não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtIdGrupoTributacaoFederalTerceiros.Focus();
                }
            }
        }

        private void PreenchaGrupoTributacaoIcmsProducaoPropria(GrupoTributacaoIcms grupoTributacaoIcms, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (grupoTributacaoIcms != null)
            {
                txtIdGrupoTributacaoIcmsProducaoPropria.Text = grupoTributacaoIcms.Id.ToString();
                txtDescricaoGrupoTributacaoIcmsProducaoPropria.Text = grupoTributacaoIcms.Descricao;
            }
            else
            {
                txtIdGrupoTributacaoIcmsProducaoPropria.Text = string.Empty;
                txtDescricaoGrupoTributacaoIcmsProducaoPropria.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Grupo de Tributação nao encontrado!", "Grupo de Tributação não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtIdGrupoTributacaoIcmsProducaoPropria.Focus();
                }
            }
        }

        private void PreenchaGrupoTributacaoFederalProducaoPropria(GrupoTributacaoFederal grupoTributacaoFederal, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (grupoTributacaoFederal != null)
            {
                txtIdGrupoTributacaoFederalProducaoPropria.Text = grupoTributacaoFederal.Id.ToString();
                txtDescricaoGrupoTributacaoFederalProducaoPropria.Text = grupoTributacaoFederal.Descricao;
            }
            else
            {
                txtIdGrupoTributacaoFederalProducaoPropria.Text = string.Empty;
                txtDescricaoGrupoTributacaoFederalProducaoPropria.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Grupo de Tributação nao encontrado!", "Grupo de Tributação não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtIdGrupoTributacaoFederalProducaoPropria.Focus();
                }
            }
        }


        #endregion

        private void btnSelecionarDiretorio_Click(object sender, EventArgs e)
        {
            if (dialogDiretorio.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtDiretorioACBR.Text = dialogDiretorio.SelectedPath;
            }
        }
       
    }
}
