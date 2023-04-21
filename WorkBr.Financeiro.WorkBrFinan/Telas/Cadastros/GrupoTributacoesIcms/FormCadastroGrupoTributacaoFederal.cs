using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.CfopServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.GrupoTributacaoFederalServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Cadastros.GrupoTributacoesIcms
{
    public partial class FormCadastroGrupoTributacaoFederal : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Cfop> _listaCfop;
        private Empresa _empresa;       
        private List<CofinsNotaFiscal> _listaCofins;
        private List<PisNotaFiscal> _listaPis;
        private List<IpiNotaFiscal> _listaIpi;       
        private CofinsNotaFiscal _cofinsEmEdicao;
        private PisNotaFiscal _pisEmEdicao;
        private IpiNotaFiscal _ipiEmEdicao;
        private bool ItemEmEdicao;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroGrupoTributacaoFederal()
        {
            InitializeComponent();
            
            _listaCofins = new List<CofinsNotaFiscal>();
            _listaIpi = new List<IpiNotaFiscal>();
            _listaPis = new List<PisNotaFiscal>();

            PesquiseEmpresa();
            PreenchaCboRegimeTributario();
            PreenchaCboNaturezaProduto();
            PreenchaCboTipoSaida();
            PreenchaTipoCliente();            
            PreenchaCboEstado();
            PreenchaCboCstIpi();
            PreenchaCboCstPis();
            PreenchaCboCstCofins();
            
            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir essa tributação?", "Excluir tributação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            _listaIpi.Remove(_ipiEmEdicao);

            PreenchaGridIpi();

            LimpeCamposTributacaoIpi();
        }

        private void btnExcluirItemPIS_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir essa tributação?", "Excluir tributação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            _listaPis.Remove(_pisEmEdicao);

            PreenchaGridPis();

            LimpeCamposTributacaoPis();
        }

        private void btnExcluirItemCOFINS_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir essa tributação?", "Excluir tributação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            _listaCofins.Remove(_cofinsEmEdicao);

            PreenchaGridCofins();

            LimpeCamposTributacaoCofins();
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeCamposTributacaoIpi();
        }
        
        private void btnCancelarItemPIS_Click(object sender, EventArgs e)
        {
            LimpeCamposTributacaoPis();
        }

        private void btnCancelarItemCOFINS_Click(object sender, EventArgs e)
        {
            LimpeCamposTributacaoCofins();
        }

        private void gcIPI_DoubleClick(object sender, EventArgs e)
        {
            if (HouveUmClickOuDuploClickNaLinha(gridView4))
            {
                ItemEmEdicao = true;
                SelecioneIpi();
                ItemEmEdicao = false;
            }
        }
        //private void EditeItem()
        //{
        //    if (_listaItensPedidosVenda != null && _listaItensPedidosVenda.Count > 0)
        //    {
        //        var itemPedido = _listaItensPedidosVenda.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());
        //        //itemProduto = itemPedido;
        //        PreenchaCamposItens(itemPedido);
        //    }
        //}
        private void gcIPI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ItemEmEdicao = true;
                SelecioneIpi();
                ItemEmEdicao = false;
            }
        }

        private void gcPIS_DoubleClick(object sender, EventArgs e)
        {
            if (HouveUmClickOuDuploClickNaLinha(gridView1))
            {
                ItemEmEdicao = true;
                SelecionePis();
                ItemEmEdicao = false;
            }
        }

        private void gcPIS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ItemEmEdicao = true;
                SelecionePis();
                ItemEmEdicao = false;
            }
        }

        private void gcCOFINS_DoubleClick(object sender, EventArgs e)
        {
            if (HouveUmClickOuDuploClickNaLinha(gridView5))
            {
                ItemEmEdicao = true;
                SelecioneCofins();
                ItemEmEdicao = false;
            }
        }

        private void gcCOFINS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ItemEmEdicao = true;
                SelecioneCofins();
                ItemEmEdicao = false;
            }
        }
        
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            EditeGrupoTributacaoFederal(null);
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                ServicoGrupoTributacaoFederal servicoGrupoTributacaoFederal = new ServicoGrupoTributacaoFederal();
                var grupoTributacao = servicoGrupoTributacaoFederal.Consulte(txtId.Text.ToInt());

                EditeGrupoTributacaoFederal(grupoTributacao, exibirMensagemDeNaoEncontrado: true);
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                ServicoGrupoTributacaoFederal servicoGrupoTributacaoFederal = new ServicoGrupoTributacaoFederal();

                GrupoTributacaoFederal grupoTributacaoFederal = RetorneGrupoTributacaoEmEdicao();

                if (grupoTributacaoFederal.Id == 0)
                {
                    servicoGrupoTributacaoFederal.Cadastre(grupoTributacaoFederal);
                }
                else
                {
                    servicoGrupoTributacaoFederal.Atualize(grupoTributacaoFederal);
                }

                EditeGrupoTributacaoFederal(grupoTributacaoFederal);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void cboCstIpi_EditValueChanged(object sender, EventArgs e)
        {
            HabiliteOuDesabiliteCstIpi();
        }

        private void cboCstPis_EditValueChanged(object sender, EventArgs e)
        {
            HabiliteOuDesabiliteCstPis();
        }

        private void cboCstCofins_EditValueChanged(object sender, EventArgs e)
        {
            HabiliteOuDesabiliteCstCofins();
        }

        private void btnInserirItemIPI_Click(object sender, EventArgs e)
        {
            AdicioneOuAtualizeTributacaoIpi();
        }
        
        private void btnInserirItemPIS_Click(object sender, EventArgs e)
        {
            AdicioneOuAtualizeTributacaoPis();
        }

        private void btnInserirItemCOFINS_Click(object sender, EventArgs e)
        {
            AdicioneOuAtualizeTributacaoCofins();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboRegimeTributario()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCodigoRegimeTributario>();

            cboRegimeTributario.Properties.DataSource = lista;
            cboRegimeTributario.Properties.DisplayMember = "Descricao";
            cboRegimeTributario.Properties.ValueMember = "Valor";

            if (_empresa.DadosEmpresa.CodigoRegimeTributario == EnumCodigoRegimeTributario.REGIMENORMAL)
                cboRegimeTributario.EditValue = EnumCodigoRegimeTributario.REGIMENORMAL;
            else
                cboRegimeTributario.EditValue = EnumCodigoRegimeTributario.SIMPLESNACIONAL;
            cboRegimeTributario.Properties.ReadOnly = false;
        }

        private void PreenchaCboNaturezaProduto()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumNaturezaProduto>();

            cboNaturezaProduto.Properties.DataSource = lista;
            cboNaturezaProduto.Properties.DisplayMember = "Descricao";
            cboNaturezaProduto.Properties.ValueMember = "Valor";

            cboNaturezaProduto.EditValue = EnumNaturezaProduto.TERCEIROS;
        }

        private void PreenchaCboEstado()
        {
            List<ObjetoDescricaoValor> lista = new List<ObjetoDescricaoValor>();

            ObjetoDescricaoValor vazio = new ObjetoDescricaoValor();
            vazio.Descricao = string.Empty;
            vazio.Valor = null;
            lista.Add(vazio);

            ObjetoDescricaoValor interEstadual = new ObjetoDescricaoValor();
            interEstadual.Descricao = "INTERESTADUAL";
            interEstadual.Valor = "IE";
            lista.Add(interEstadual);

            ObjetoDescricaoValor acre = new ObjetoDescricaoValor();
            acre.Descricao = "ACRE";
            acre.Valor = "AC";
            lista.Add(acre);

            ObjetoDescricaoValor alagoas = new ObjetoDescricaoValor();
            alagoas.Descricao = "ALAGOAS";
            alagoas.Valor = "AL";
            lista.Add(alagoas);

            ObjetoDescricaoValor amapa = new ObjetoDescricaoValor();
            amapa.Descricao = "AMAPA";
            amapa.Valor = "AP";
            lista.Add(amapa);

            ObjetoDescricaoValor amazonas = new ObjetoDescricaoValor();
            amazonas.Descricao = "AMAZONAS";
            amazonas.Valor = "AM";
            lista.Add(amazonas);

            ObjetoDescricaoValor bahia = new ObjetoDescricaoValor();
            bahia.Descricao = "BAHIA";
            bahia.Valor = "BA";
            lista.Add(bahia);

            ObjetoDescricaoValor ceara = new ObjetoDescricaoValor();
            ceara.Descricao = "CEARÁ";
            ceara.Valor = "CE";
            lista.Add(ceara);

            ObjetoDescricaoValor distritoFederal = new ObjetoDescricaoValor();
            distritoFederal.Descricao = "DISTRITO FEDERAL";
            distritoFederal.Valor = "DF";
            lista.Add(distritoFederal);

            ObjetoDescricaoValor espiritoSanto = new ObjetoDescricaoValor();
            espiritoSanto.Descricao = "ESPÍRITO SANTO";
            espiritoSanto.Valor = "ES";
            lista.Add(espiritoSanto);

            ObjetoDescricaoValor goias = new ObjetoDescricaoValor();
            goias.Descricao = "GOIÁS";
            goias.Valor = "GO";
            lista.Add(goias);

            ObjetoDescricaoValor maranhao = new ObjetoDescricaoValor();
            maranhao.Descricao = "MARANHÃO";
            maranhao.Valor = "MA";
            lista.Add(maranhao);

            ObjetoDescricaoValor matoGrosso = new ObjetoDescricaoValor();
            matoGrosso.Descricao = "MATO GROSSO";
            matoGrosso.Valor = "MT";
            lista.Add(matoGrosso);

            ObjetoDescricaoValor matoGrossoSul = new ObjetoDescricaoValor();
            matoGrossoSul.Descricao = "MATO GROSSO DO SUL";
            matoGrossoSul.Valor = "MS";
            lista.Add(matoGrossoSul);

            ObjetoDescricaoValor minasGerais = new ObjetoDescricaoValor();
            minasGerais.Descricao = "MINAS GERAIS";
            minasGerais.Valor = "MG";
            lista.Add(minasGerais);

            ObjetoDescricaoValor para = new ObjetoDescricaoValor();
            para.Descricao = "PARÁ";
            para.Valor = "PA";
            lista.Add(para);

            ObjetoDescricaoValor paraiba = new ObjetoDescricaoValor();
            paraiba.Descricao = "PARAÍBA";
            paraiba.Valor = "PB";
            lista.Add(paraiba);

            ObjetoDescricaoValor parana = new ObjetoDescricaoValor();
            parana.Descricao = "PARANÁ";
            parana.Valor = "PR";
            lista.Add(parana);

            ObjetoDescricaoValor pernambuco = new ObjetoDescricaoValor();
            pernambuco.Descricao = "PERNAMBUCO";
            pernambuco.Valor = "PB";
            lista.Add(pernambuco);

            ObjetoDescricaoValor piaui = new ObjetoDescricaoValor();
            piaui.Descricao = "PIAUÍ";
            piaui.Valor = "PI";
            lista.Add(piaui);

            ObjetoDescricaoValor rioJaneiro = new ObjetoDescricaoValor();
            rioJaneiro.Descricao = "RIO DE JANEIRO";
            rioJaneiro.Valor = "RJ";
            lista.Add(rioJaneiro);

            ObjetoDescricaoValor rioGrandeNorte = new ObjetoDescricaoValor();
            rioGrandeNorte.Descricao = "RIO GRANDE DO NORTE";
            rioGrandeNorte.Valor = "RN";
            lista.Add(rioGrandeNorte);

            ObjetoDescricaoValor rioGrandeSul = new ObjetoDescricaoValor();
            rioGrandeSul.Descricao = "RIO GRANDE DO SUL";
            rioGrandeSul.Valor = "RS";
            lista.Add(rioGrandeSul);

            ObjetoDescricaoValor rondonia = new ObjetoDescricaoValor();
            rondonia.Descricao = "RONDÔNIA";
            rondonia.Valor = "RO";
            lista.Add(rondonia);

            ObjetoDescricaoValor roraima = new ObjetoDescricaoValor();
            roraima.Descricao = "RORAIMA";
            roraima.Valor = "RR";
            lista.Add(roraima);

            ObjetoDescricaoValor santaCatarina = new ObjetoDescricaoValor();
            santaCatarina.Descricao = "SANTA CATARINA";
            santaCatarina.Valor = "SC";
            lista.Add(santaCatarina);

            ObjetoDescricaoValor saoPaulo = new ObjetoDescricaoValor();
            saoPaulo.Descricao = "SÃO PAULO";
            saoPaulo.Valor = "SP";
            lista.Add(saoPaulo);

            ObjetoDescricaoValor sergipe = new ObjetoDescricaoValor();
            sergipe.Descricao = "SERGIPE";
            sergipe.Valor = "SE";
            lista.Add(sergipe);

            ObjetoDescricaoValor tocantins = new ObjetoDescricaoValor();
            tocantins.Descricao = "TOCANTINS";
            tocantins.Valor = "TO";
            lista.Add(tocantins);

            cboEstadoDestino.Properties.DataSource = lista;
            cboEstadoDestino.Properties.DisplayMember = "Descricao";
            cboEstadoDestino.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboTipoSaida()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoSaidaTributacaoIcms>();

            cboTipoSaida.Properties.DataSource = lista;
            cboTipoSaida.Properties.DisplayMember = "Descricao";
            cboTipoSaida.Properties.ValueMember = "Valor";

            cboTipoSaida.EditValue = EnumTipoSaidaTributacaoIcms.SAIDAVENDA;
        }

        private void PreenchaTipoCliente()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoCliente>();

            cboTipoCliente.Properties.DataSource = lista;
            cboTipoCliente.Properties.DisplayMember = "Descricao";
            cboTipoCliente.Properties.ValueMember = "Valor";

            cboTipoCliente.EditValue = EnumTipoCliente.CONSUMIDOR;
        }

        private void PreenchaCboCstIpi()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCstIpi>();

            lista.Insert(0, null);

            if ((EnumTipoSaidaTributacaoIcms)cboTipoSaida.EditValue == EnumTipoSaidaTributacaoIcms.ENTRADADEVOLUCAOVENDA)
                lista.RemoveRange(8, 7);
            else
                lista.RemoveRange(1, 7);

            cboCstIpi.Properties.DataSource = lista;
            cboCstIpi.Properties.DisplayMember = "Descricao";
            cboCstIpi.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboCstPis()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCstPis>();

            lista.Insert(0, null);

            cboCstPis.Properties.DataSource = lista;
            cboCstPis.Properties.DisplayMember = "Descricao";
            cboCstPis.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboCstCofins()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCstCofins>();

            lista.Insert(0, null);

            cboCstCofins.Properties.DataSource = lista;
            cboCstCofins.Properties.DisplayMember = "Descricao";
            cboCstCofins.Properties.ValueMember = "Valor";
        }

        private void PesquiseTodosCfop()
        {
            ServicoCfop servicoCfop = new ServicoCfop();
            _listaCfop = servicoCfop.ConsulteListaAtiva();
        }
        
        private void PesquiseEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            _empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            _empresa.DadosEmpresa.Endereco.Cidade.Estado.CarregueLazyLoad();
        }

        private void HabiliteOuDesabiliteCstIpi()
        {
            var cstIpi = (EnumCstIpi?)cboCstIpi.EditValue;

            if (cstIpi != null && (
                cstIpi == EnumCstIpi.IPI00 ||
                cstIpi == EnumCstIpi.IPI49 ||
                cstIpi == EnumCstIpi.IPI50 ||
                cstIpi == EnumCstIpi.IPI99))
            {
                txtPercentualIpi.Properties.ReadOnly = false;
                txtPercentualIpi.TabStop = true;
                txtValorIpi.Properties.ReadOnly = false;
                txtValorIpi.TabStop = true;
            }
            else
            {
                txtPercentualIpi.Properties.ReadOnly = true;
                txtPercentualIpi.TabStop = false;
                txtValorIpi.Properties.ReadOnly = true;
                txtValorIpi.TabStop = false;

                txtPercentualIpi.Text = string.Empty;
                txtValorIpi.Text = string.Empty;
            }
        }

        private void HabiliteOuDesabiliteCstPis()
        {
            var cstPis = (EnumCstPis?)cboCstPis.EditValue;

            if (!ItemEmEdicao)
            {
                //Limpa todos os campos
                txtValorBaseCalculoPis.Text = string.Empty;
                txtAliquotaPisPercentual.Text = string.Empty;
                txtAliquotaPisReais.Text = string.Empty;
                txtQuantidadeVendidaPis.Text = string.Empty;
                txtValorPis.Text = string.Empty;

                txtBaseCalculoPisST.Text = string.Empty;
                txtAliquotaPisSTPercentual.Text = string.Empty;
                txtAliquotaPisSTReais.Text = string.Empty;
                txtQuantidadeVendidaPisST.Text = string.Empty;
                txtValorPisST.Text = string.Empty;

                //Desabilita todos os campos
                //BC
                txtValorBaseCalculoPis.Properties.ReadOnly = true;
                txtAliquotaPisPercentual.Properties.ReadOnly = true;
                txtAliquotaPisReais.Properties.ReadOnly = true;
                txtQuantidadeVendidaPis.Properties.ReadOnly = true;
                txtValorPis.Properties.ReadOnly = true;

                txtValorBaseCalculoPis.TabStop = false;
                txtAliquotaPisPercentual.TabStop = false;
                txtAliquotaPisReais.TabStop = false;
                txtQuantidadeVendidaPis.TabStop = false;
                txtValorPis.TabStop = false;

                //ST
                txtBaseCalculoPisST.Properties.ReadOnly = true;
                txtAliquotaPisSTPercentual.Properties.ReadOnly = true;
                txtAliquotaPisSTReais.Properties.ReadOnly = true;
                txtQuantidadeVendidaPisST.Properties.ReadOnly = true;
                txtValorPisST.Properties.ReadOnly = true;

                txtBaseCalculoPisST.TabStop = false;
                txtAliquotaPisSTPercentual.TabStop = false;
                txtAliquotaPisSTReais.TabStop = false;
                txtQuantidadeVendidaPisST.TabStop = false;
                txtValorPisST.TabStop = false;
            }

            if (cstPis != null && (
                cstPis == EnumCstPis.pis01 ||
                cstPis == EnumCstPis.pis02))
            {
                txtAliquotaPisPercentual.Properties.ReadOnly = false;
                txtAliquotaPisPercentual.TabStop = true;
            }
            else if (cstPis != null && (
                cstPis == EnumCstPis.pis03))
            {
                txtAliquotaPisReais.Properties.ReadOnly = false;
                txtAliquotaPisReais.TabStop = true;
            }
            else if (cstPis != null && (
                cstPis == EnumCstPis.pis05))
            {
                txtAliquotaPisSTPercentual.Properties.ReadOnly = false;
                txtAliquotaPisSTReais.Properties.ReadOnly = false;

                txtAliquotaPisSTPercentual.TabStop = true;
                txtAliquotaPisSTReais.TabStop = true;
            }
            else if (cstPis != null && (
                cstPis == EnumCstPis.pis49 ||
                cstPis == EnumCstPis.pis50 ||
                cstPis == EnumCstPis.pis51 ||
                cstPis == EnumCstPis.pis52 ||
                cstPis == EnumCstPis.pis53 ||
                cstPis == EnumCstPis.pis54 ||
                cstPis == EnumCstPis.pis55 ||
                cstPis == EnumCstPis.pis56 ||
                cstPis == EnumCstPis.pis60 ||
                cstPis == EnumCstPis.pis61 ||
                cstPis == EnumCstPis.pis62 ||
                cstPis == EnumCstPis.pis63 ||
                cstPis == EnumCstPis.pis64 ||
                cstPis == EnumCstPis.pis65 ||
                cstPis == EnumCstPis.pis66 ||
                cstPis == EnumCstPis.pis67 ||
                cstPis == EnumCstPis.pis70 ||
                cstPis == EnumCstPis.pis71 ||
                cstPis == EnumCstPis.pis72 ||
                cstPis == EnumCstPis.pis73 ||
                cstPis == EnumCstPis.pis74 ||
                cstPis == EnumCstPis.pis75 ||
                cstPis == EnumCstPis.pis98 ||
                cstPis == EnumCstPis.pis99
                ))
            {
                txtAliquotaPisPercentual.Properties.ReadOnly = false;
                txtAliquotaPisReais.Properties.ReadOnly = false;

                txtAliquotaPisPercentual.TabStop = true;
                txtAliquotaPisReais.TabStop = true;
            }
        }

        private void HabiliteOuDesabiliteCstCofins()
        {  
            var cstCofins = (EnumCstCofins?)cboCstCofins.EditValue;

            if (!ItemEmEdicao)
            {
                //Limpa todos os campos
                txtBaseCalculoCofins.Text = string.Empty;
                txtAliquotaCofinsPercentual.Text = string.Empty;
                txtAliquotaCofinsReais.Text = string.Empty;
                txtQuantidadeVendidaCofins.Text = string.Empty;
                txtValorCofins.Text = string.Empty;

                txtBaseCalculoCofinsST.Text = string.Empty;
                txtAliquotaCofinsSTPercentual.Text = string.Empty;
                txtAliquotaCofinsSTReais.Text = string.Empty;
                txtQuantidadeVendidaCofinsST.Text = string.Empty;
                txtValorCofinsST.Text = string.Empty;

                //Desabilita todos os campos
                //BC
                txtBaseCalculoCofins.Properties.ReadOnly = true;
                txtAliquotaCofinsPercentual.Properties.ReadOnly = true;
                txtAliquotaCofinsReais.Properties.ReadOnly = true;
                txtQuantidadeVendidaCofins.Properties.ReadOnly = true;
                txtValorCofins.Properties.ReadOnly = true;

                txtBaseCalculoCofins.TabStop = false;
                txtAliquotaCofinsPercentual.TabStop = false;
                txtAliquotaCofinsReais.TabStop = false;
                txtQuantidadeVendidaCofins.TabStop = false;
                txtValorCofins.TabStop = false;

                //ST
                txtBaseCalculoCofinsST.Properties.ReadOnly = true;
                txtAliquotaCofinsSTPercentual.Properties.ReadOnly = true;
                txtAliquotaCofinsSTReais.Properties.ReadOnly = true;
                txtQuantidadeVendidaCofinsST.Properties.ReadOnly = true;
                txtValorCofinsST.Properties.ReadOnly = true;

                txtBaseCalculoCofinsST.TabStop = false;
                txtAliquotaCofinsSTPercentual.TabStop = false;
                txtAliquotaCofinsSTReais.TabStop = false;
                txtQuantidadeVendidaCofinsST.TabStop = false;
                txtValorCofinsST.TabStop = false;
            }

            if (cstCofins != null && (
                cstCofins == EnumCstCofins.cofins01 ||
                cstCofins == EnumCstCofins.cofins02))
            {
                txtAliquotaCofinsPercentual.Properties.ReadOnly = false;
                txtAliquotaCofinsPercentual.TabStop = true;
            }
            else if (cstCofins != null && (
                cstCofins == EnumCstCofins.cofins03))
            {
                txtAliquotaCofinsReais.Properties.ReadOnly = false;
                txtAliquotaCofinsReais.TabStop = true;
            }
            else if (cstCofins != null && (
                cstCofins == EnumCstCofins.cofins05))
            {
                txtAliquotaCofinsSTPercentual.Properties.ReadOnly = false;
                txtAliquotaCofinsSTReais.Properties.ReadOnly = false;

                txtAliquotaCofinsSTPercentual.TabStop = true;
                txtAliquotaCofinsSTReais.TabStop = true;
            }
            else if (cstCofins != null && (
                cstCofins == EnumCstCofins.cofins49 ||
                cstCofins == EnumCstCofins.cofins50 ||
                cstCofins == EnumCstCofins.cofins51 ||
                cstCofins == EnumCstCofins.cofins52 ||
                cstCofins == EnumCstCofins.cofins53 ||
                cstCofins == EnumCstCofins.cofins54 ||
                cstCofins == EnumCstCofins.cofins55 ||
                cstCofins == EnumCstCofins.cofins56 ||
                cstCofins == EnumCstCofins.cofins60 ||
                cstCofins == EnumCstCofins.cofins61 ||
                cstCofins == EnumCstCofins.cofins62 ||
                cstCofins == EnumCstCofins.cofins63 ||
                cstCofins == EnumCstCofins.cofins64 ||
                cstCofins == EnumCstCofins.cofins65 ||
                cstCofins == EnumCstCofins.cofins66 ||
                cstCofins == EnumCstCofins.cofins67 ||
                cstCofins == EnumCstCofins.cofins70 ||
                cstCofins == EnumCstCofins.cofins71 ||
                cstCofins == EnumCstCofins.cofins72 ||
                cstCofins == EnumCstCofins.cofins73 ||
                cstCofins == EnumCstCofins.cofins74 ||
                cstCofins == EnumCstCofins.cofins75 ||
                cstCofins == EnumCstCofins.cofins98 ||
                cstCofins == EnumCstCofins.cofins99
                ))
            {
                txtAliquotaCofinsPercentual.Properties.ReadOnly = false;
                txtAliquotaCofinsReais.Properties.ReadOnly = false;

                txtAliquotaCofinsPercentual.TabStop = true;
                txtAliquotaCofinsReais.TabStop = true;
            }
        }
        
        private void AdicioneOuAtualizeTributacaoCofins()
        {
            Action actionInserirTributacao = () =>
            {
                CofinsNotaFiscal cofins = RetorneCofinsEmEdicao();
                
                ServicoGrupoTributacaoFederal servicoGrupoTributacaoFederal = new ServicoGrupoTributacaoFederal();

                servicoGrupoTributacaoFederal.ValideTributacaoCofins(cofins, _listaCofins);
                
                _listaCofins.Remove(_cofinsEmEdicao);
                
                if (cofins.Id == 0)
                {
                    _listaCofins.Add(cofins);
                }
                else
                {
                    int posicaoItem = cofins.Id - 1;

                    _listaCofins.Insert(posicaoItem, cofins);
                }
                
                PreenchaGridCofins();
                
                LimpeCamposTributacaoCofins();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirTributacao, exibirMensagemDeSucesso: false);
        }

        private void AdicioneOuAtualizeTributacaoPis()
        {
            Action actionInserirTributacao = () =>
            {
                PisNotaFiscal pis = RetornePisEmEdicao();

                ServicoGrupoTributacaoFederal servicoGrupoTributacaoFederal = new ServicoGrupoTributacaoFederal();

                servicoGrupoTributacaoFederal.ValideTributacaoPis(pis, _listaPis);

                _listaPis.Remove(_pisEmEdicao);

                if (pis.Id == 0)
                {
                    _listaPis.Add(pis);
                }
                else
                {
                    int posicaoItem = pis.Id - 1;

                    _listaPis.Insert(posicaoItem, pis);
                }

                PreenchaGridPis();

                LimpeCamposTributacaoPis();
            };
                TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirTributacao, exibirMensagemDeSucesso: false);
        }

        private void AdicioneOuAtualizeTributacaoIpi()
        {
            Action actionInserirTributacao = () =>
            {   
                IpiNotaFiscal ipi = RetorneIpiEmEdicao();

                ServicoGrupoTributacaoFederal servicoGrupoTributacaoFederal = new ServicoGrupoTributacaoFederal();
                
                servicoGrupoTributacaoFederal.ValideTributacaoIpi(ipi, _listaIpi);
                
                _listaIpi.Remove(_ipiEmEdicao);
                
                if (ipi.Id == 0)
                {
                    _listaIpi.Add(ipi);
                }
                else
                {
                    int posicaoItem = ipi.Id - 1;

                    _listaIpi.Insert(posicaoItem, ipi);
                }
                
                PreenchaGridIpi();

                LimpeCamposTributacaoIpi();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirTributacao, exibirMensagemDeSucesso: false);
        }

        private CofinsNotaFiscal RetorneCofinsEmEdicao()
        {
            _cofinsEmEdicao = _cofinsEmEdicao ?? new CofinsNotaFiscal();

            var cofins = _cofinsEmEdicao.CloneCompleto();

            cofins.CstCofins = cboCstCofins.EditValue != null? (EnumCstCofins)cboCstCofins.EditValue: cofins.CstCofins;
            cofins.BaseDeCalculo = txtBaseCalculoCofins.Text.ToDoubleNullabel();
            cofins.BaseDeCalculoST = txtBaseCalculoCofinsST.Text.ToDoubleNullabel();
            cofins.QuantidadeVendida = txtQuantidadeVendidaCofins.Text.ToDoubleNullabel();
            cofins.QuantidadeVendidaST = txtQuantidadeVendidaCofinsST.Text.ToDoubleNullabel();

            cofins.AliquotaPercentual = txtAliquotaCofinsPercentual.Text.ToDoubleNullabel();
            cofins.AliquotaPercentualST = txtAliquotaCofinsSTPercentual.Text.ToDoubleNullabel();
            cofins.AliquotaReais = txtAliquotaCofinsReais.Text.ToDoubleNullabel();
            cofins.AliquotaReaisST = txtAliquotaCofinsSTReais.Text.ToDoubleNullabel();

            cofins.ValorCofins = txtValorCofins.Text.ToDoubleNullabel();
            cofins.ValorCofinsST = txtValorCofinsST.Text.ToDoubleNullabel();

            cofins.EstadoDestino = cboEstadoDestino.EditValue.ToStringEmpty();
            cofins.TipoCliente = (EnumTipoCliente)cboTipoCliente.EditValue;

            return cofins;
        }

        private PisNotaFiscal RetornePisEmEdicao()
        {
            _pisEmEdicao = _pisEmEdicao ?? new PisNotaFiscal();

            var pis = _pisEmEdicao.CloneCompleto();

            pis.CstPis = cboCstPis.EditValue != null?(EnumCstPis)cboCstPis.EditValue: pis.CstPis;
            pis.BaseDeCalculo = txtValorBaseCalculoPis.Text.ToDoubleNullabel();
            pis.BaseDeCalculoST = txtBaseCalculoPisST.Text.ToDoubleNullabel();
            pis.QuantidadeVendida = txtQuantidadeVendidaPis.Text.ToDoubleNullabel();
            pis.QuantidadeVendidaST = txtQuantidadeVendidaPisST.Text.ToDoubleNullabel();

            pis.AliquotaPercentual = txtAliquotaPisPercentual.Text.ToDoubleNullabel();
            pis.AliquotaPercentualST = txtAliquotaPisSTPercentual.Text.ToDoubleNullabel();
            pis.AliquotaReais = txtAliquotaPisReais.Text.ToDoubleNullabel();
            pis.AliquotaReaisST = txtAliquotaPisSTReais.Text.ToDoubleNullabel();

            pis.ValorPis = txtValorPis.Text.ToDoubleNullabel();
            pis.ValorPisST = txtValorPisST.Text.ToDoubleNullabel();

            pis.EstadoDestino = cboEstadoDestino.EditValue.ToStringEmpty();
            pis.TipoCliente = (EnumTipoCliente)cboTipoCliente.EditValue ;

            return pis;
        }

        private IpiNotaFiscal RetorneIpiEmEdicao()
        {
            _ipiEmEdicao = _ipiEmEdicao ?? new IpiNotaFiscal();

            var ipi = _ipiEmEdicao.CloneCompleto();

            ipi.AliquotaIpi = txtPercentualIpi.Text.ToDoubleNullabel();
            ipi.ValorIpi = txtValorIpi.Text.ToDoubleNullabel();
            ipi.BaseDeCalculo = txtValorIpi.Text.ToDoubleNullabel();
            ipi.CstIpi = cboCstIpi.EditValue != null? (EnumCstIpi)cboCstIpi.EditValue: ipi.CstIpi;
            ipi.EstadoDestino = cboEstadoDestino.EditValue.ToStringEmpty();
            ipi.TipoCliente = (EnumTipoCliente)cboTipoCliente.EditValue ;

            return ipi;
        }

        private void PreenchaGridCofins()
        {
            GereIdFalsoParaOsItensCofins();

            List<TributacaoGrid> listaTributacaoGrid = new List<TributacaoGrid>();

            foreach (var cofins in _listaCofins)
            {
                TributacaoGrid tributacaoGrid = new TributacaoGrid();
                
                tributacaoGrid.Csosn = cofins.CstCofins.Descricao();
                tributacaoGrid.EstadoDestino = cofins.EstadoDestino;
                tributacaoGrid.Id = cofins.Id;
                tributacaoGrid.TipoCliente = cofins.TipoCliente.Descricao();
                tributacaoGrid.TipoSaida = cofins.TipoSaida.Descricao();

                listaTributacaoGrid.Add(tributacaoGrid);
            }

            gcCOFINS.DataSource = listaTributacaoGrid;
            gcCOFINS.RefreshDataSource();
        }

        private void PreenchaGridPis()
        {
            GereIdFalsoParaOsItensPis();

            List<TributacaoGrid> listaTributacaoGrid = new List<TributacaoGrid>();

            foreach (var pis in _listaPis)
            {
                TributacaoGrid tributacaoGrid = new TributacaoGrid();
               
                tributacaoGrid.Csosn = pis.CstPis.Descricao();
                tributacaoGrid.EstadoDestino = pis.EstadoDestino;
                tributacaoGrid.Id = pis.Id;
                tributacaoGrid.TipoCliente = pis.TipoCliente.Descricao();
                tributacaoGrid.TipoSaida = pis.TipoSaida.Descricao();

                listaTributacaoGrid.Add(tributacaoGrid);
            }

            gcPIS.DataSource = listaTributacaoGrid;
            gcPIS.RefreshDataSource();
        }

        private void PreenchaGridIpi()
        {
            GereIdFalsoParaOsItensIpi();

            List<TributacaoGrid> listaTributacaoGrid = new List<TributacaoGrid>();

            foreach (var ipi in _listaIpi)
            {
                TributacaoGrid tributacaoGrid = new TributacaoGrid();

                tributacaoGrid.Csosn = ipi.CstIpi.Descricao();
                tributacaoGrid.EstadoDestino = ipi.EstadoDestino;
                tributacaoGrid.Id = ipi.Id;
                tributacaoGrid.TipoCliente = ipi.TipoCliente.Descricao();
                tributacaoGrid.TipoSaida = ipi.TipoSaida.Descricao();

                listaTributacaoGrid.Add(tributacaoGrid);
            }

            gcIPI.DataSource = listaTributacaoGrid;
            gcIPI.RefreshDataSource();
        }

        private void GereIdFalsoParaOsItensCofins()
        {
            for (int i = 0; i < _listaCofins.Count; i++)
            {
                _listaCofins[i].Id = i + 1;
            }
        }

        private void GereIdFalsoParaOsItensPis()
        {
            for (int i = 0; i < _listaPis.Count; i++)
            {
                _listaPis[i].Id = i + 1;
            }
        }

        private void GereIdFalsoParaOsItensIpi()
        {
            for (int i = 0; i < _listaIpi.Count; i++)
            {
                _listaIpi[i].Id = i + 1;
            }
        }

        private void LimpeCamposTributacaoIpi(bool focoNoCboTipoSaida = true)
        {  
            EditeIpi(null);

            if (focoNoCboTipoSaida)
            {
                cboTipoSaida.Focus();
            }
        }

        private void LimpeCamposTributacaoCofins(bool focoNoCboTipoSaida = true)
        {
            EditeCofins(null);
            
            if (focoNoCboTipoSaida)
            {
                cboTipoSaida.Focus();
            }
        }

        private void LimpeCamposTributacaoPis(bool focoNoCboTipoSaida = true)
        {  
            EditePis(null);
         
            if (focoNoCboTipoSaida)
            {
                cboTipoSaida.Focus();
            }
        }

        private void EditeCofins(CofinsNotaFiscal cofins)
        {
            _cofinsEmEdicao = cofins;

            if (cofins != null)
            {
                cboTipoSaida.EditValue = cofins.TipoSaida;
                cboEstadoDestino.EditValue = cofins.EstadoDestino;
                cboTipoCliente.EditValue = cofins.TipoCliente;
                
                cboCstCofins.EditValue = cofins.CstCofins;

                HabiliteOuDesabiliteCstCofins();

                if (cofins.AliquotaPercentual != null)
                    txtBaseCalculoCofins.Text = cofins.BaseDeCalculo.ToDouble() != 0 ? cofins.BaseDeCalculo.Value.ToString("0.00") : "0.00";

                else if (cofins.AliquotaPercentualST != null)
                    txtBaseCalculoCofinsST.Text = cofins.BaseDeCalculoST != 0 ? cofins.BaseDeCalculoST.Value.ToString("0.00") : "0.00";

                else if (cofins.AliquotaReais != null)
                    txtQuantidadeVendidaCofins.Text = cofins.QuantidadeVendida != null ? cofins.QuantidadeVendida.Value.ToString("#0.00") : string.Empty;

                else
                    txtQuantidadeVendidaCofinsST.Text = cofins.QuantidadeVendidaST != null ? cofins.QuantidadeVendidaST.Value.ToString("#0.00") : string.Empty;

                txtAliquotaCofinsPercentual.Text = cofins.AliquotaPercentual != null ? cofins.AliquotaPercentual.Value.ToString("#0.00") : string.Empty;
                txtAliquotaCofinsSTPercentual.Text = cofins.AliquotaPercentualST != null ? cofins.AliquotaPercentualST.Value.ToString("#0.00") : string.Empty;
                txtAliquotaCofinsReais.Text = cofins.AliquotaReais != null ? cofins.AliquotaReais.Value.ToString("#0.00") : string.Empty;
                txtAliquotaCofinsSTReais.Text = cofins.AliquotaReaisST != null ? cofins.AliquotaReaisST.Value.ToString("#0.00") : string.Empty;

                txtValorCofins.Text = cofins.ValorCofins != null ? cofins.ValorCofins.Value.ToString("#0.00") : string.Empty;
                txtValorCofinsST.Text = cofins.ValorCofinsST != null ? cofins.ValorCofinsST.Value.ToString("#0.00") : string.Empty;

                btnExcluirItemCOFINS.Visible = true;

                btnInserirItemCOFINS.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                cboCstCofins.EditValue = null;
                //_BaseDeCalculoCofins = 0;
                //_BaseDeCalculoCofinsST = 0;

                txtQuantidadeVendidaCofins.Text = string.Empty;
                txtQuantidadeVendidaCofinsST.Text = string.Empty;

                txtAliquotaCofinsPercentual.Text = string.Empty;
                txtAliquotaCofinsSTPercentual.Text = string.Empty;
                txtAliquotaCofinsReais.Text = string.Empty;
                txtAliquotaCofinsSTReais.Text = string.Empty;

                txtValorCofins.Text = string.Empty;
                txtValorCofinsST.Text = string.Empty;

                btnExcluirItemCOFINS.Visible = false;
                btnInserirItemCOFINS.Image = Properties.Resources.icones2_19;
            }
        }

        private void EditePis(PisNotaFiscal pis)
        {
            _pisEmEdicao = pis;

            if (pis != null)
            {
                cboTipoSaida.EditValue = pis.TipoSaida;
                cboEstadoDestino.EditValue = pis.EstadoDestino;
                cboTipoCliente.EditValue = pis.TipoCliente;
                
                cboCstPis.EditValue = pis.CstPis;

                HabiliteOuDesabiliteCstPis();

                if (pis.AliquotaPercentual != null)
                    txtBaseCalculoCofins.Text = pis.BaseDeCalculo.ToDouble() != 0 ? pis.BaseDeCalculo.Value.ToString("0.00") : "0.00";

                else if (pis.AliquotaPercentualST != null)
                    txtBaseCalculoCofinsST.Text = pis.BaseDeCalculoST.ToDouble() != 0 ? pis.BaseDeCalculoST.Value.ToString("0.00") : "0.00";

                else if (pis.AliquotaReais != null)
                    txtQuantidadeVendidaCofins.Text = pis.QuantidadeVendida != null ? pis.QuantidadeVendida.Value.ToString("#0.00") : string.Empty;

                else
                    txtQuantidadeVendidaCofinsST.Text = pis.QuantidadeVendidaST != null ? pis.QuantidadeVendidaST.Value.ToString("#0.00") : string.Empty;

                txtAliquotaPisPercentual.Text = pis.AliquotaPercentual != null ? pis.AliquotaPercentual.Value.ToString("#0.00") : string.Empty;
                txtAliquotaPisSTPercentual.Text = pis.AliquotaPercentualST != null ? pis.AliquotaPercentualST.Value.ToString("#0.00") : string.Empty;
                txtAliquotaPisReais.Text = pis.AliquotaReais != null ? pis.AliquotaReais.Value.ToString("#0.00") : string.Empty;
                txtAliquotaPisSTReais.Text = pis.AliquotaReaisST != null ? pis.AliquotaReaisST.Value.ToString("#0.00") : string.Empty;

                txtValorPis.Text = pis.ValorPis != null ? pis.ValorPis.Value.ToString("#0.00") : string.Empty;
                txtValorCofinsST.Text = pis.ValorPisST != null ? pis.ValorPisST.Value.ToString("#0.00") : string.Empty;

                btnExcluirItemPIS.Visible = true;

                btnInserirItemPIS.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                cboCstPis.EditValue = null;
                //_BaseDeCalculoCofins = 0;
                //_BaseDeCalculoCofinsST = 0;

                txtQuantidadeVendidaPis.Text = string.Empty;
                txtQuantidadeVendidaPisST.Text = string.Empty;

                txtAliquotaPisPercentual.Text = string.Empty;
                txtAliquotaPisSTPercentual.Text = string.Empty;
                txtAliquotaPisReais.Text = string.Empty;
                txtAliquotaPisSTReais.Text = string.Empty;

                txtValorPis.Text = string.Empty;
                txtValorPisST.Text = string.Empty;

                btnExcluirItemPIS.Visible = false;
                btnInserirItemPIS.Image = Properties.Resources.icones2_19;
            }
        }

        private void EditeIpi(IpiNotaFiscal ipi)
        {
            _ipiEmEdicao = ipi;

            if (ipi != null)
            {
                cboTipoSaida.EditValue = ipi.TipoSaida;
                cboEstadoDestino.EditValue = ipi.EstadoDestino;
                cboTipoCliente.EditValue = ipi.TipoCliente;
                
                cboCstIpi.EditValue = ipi.CstIpi;

                HabiliteOuDesabiliteCstIpi();

                txtPercentualIpi.Text = ipi.AliquotaIpi != null ? ipi.AliquotaIpi.Value.ToString("#0.00") : string.Empty;
                txtValorIpi.Text = ipi.ValorIpi != null ? ipi.ValorIpi.Value.ToString("#0.00") : string.Empty;              

                //_BaseDeCalculoIpi = ipi.BaseDeCalculo.ToDouble() != 0 ? ipi.BaseDeCalculo.ToDouble() : 0;

                btnExcluirItemIPI.Visible = true;

                btnInserirItemIPI.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                cboCstIpi.EditValue = null;
                //_BaseDeCalculoCofins = 0;
                //_BaseDeCalculoCofinsST = 0;
               
                txtValorIpi.Text = string.Empty;
                txtPercentualIpi.Text = string.Empty;

                btnExcluirItemIPI.Visible = false;
                btnInserirItemIPI.Image = Properties.Resources.icones2_19;
            }
        }
        
        private void SelecioneCofins()
        {
            var cofins = _listaCofins.FirstOrDefault(trib => trib.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());
           
            EditeCofins(cofins);
        }

        private void SelecionePis()
        {  
            var pis = _listaPis.FirstOrDefault(trib => trib.Id == gridColumn1.View.GetFocusedRowCellValue(gridColumn1).ToInt());
            
            EditePis(pis);            
        }

        private void SelecioneIpi()
        {   
            var ipi = _listaIpi.FirstOrDefault(trib => trib.Id == gridColumn6.View.GetFocusedRowCellValue(gridColumn6).ToInt());
           

            EditeIpi(ipi);
        }

        private void EditeGrupoTributacaoFederal(GrupoTributacaoFederal grupoTributacaofederal, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (grupoTributacaofederal != null)
            {
                txtId.Enabled = false;

                txtId.Text = grupoTributacaofederal.Id.ToString();
                txtDescricao.Text = grupoTributacaofederal.Descricao;
                cboRegimeTributario.EditValue = grupoTributacaofederal.RegimeTributario;
                cboNaturezaProduto.EditValue = grupoTributacaofederal.NaturezaProduto;
                
                _listaCofins = grupoTributacaofederal.ListaCofins.ToList();
                _listaPis = grupoTributacaofederal.ListaPis.ToList();
                _listaIpi = grupoTributacaofederal.ListaIpi.ToList();

                PreenchaGridCofins();
                PreenchaGridPis();
                PreenchaGridIpi();

                LimpeCamposTributacaoCofins(false);
                LimpeCamposTributacaoPis(false);
                LimpeCamposTributacaoIpi(false);
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Grupo de Tributação nao encontrado!", "Grupo de Tributação não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpeCamposTributacaoCofins(false);
                LimpeCamposTributacaoPis(false);
                LimpeCamposTributacaoIpi(false);

                _listaCofins.Clear();
                _listaIpi.Clear();
                _listaPis.Clear();

                PreenchaGridCofins();
                PreenchaGridPis();
                PreenchaGridIpi();

                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                cboRegimeTributario.EditValue = EnumCodigoRegimeTributario.SIMPLESNACIONAL;
                cboNaturezaProduto.EditValue = EnumNaturezaProduto.TERCEIROS;

                txtId.Enabled = true;
                txtId.Focus();
            }
        }
        
        private GrupoTributacaoFederal RetorneGrupoTributacaoEmEdicao()
        {
            GrupoTributacaoFederal grupoTributacao = new GrupoTributacaoFederal();
            grupoTributacao.Descricao = txtDescricao.Text;
            grupoTributacao.Id = txtId.Text.ToInt();
                        
            grupoTributacao.ListaCofins = _listaCofins;
            grupoTributacao.ListaPis = _listaPis;
            grupoTributacao.ListaIpi = _listaIpi;

            grupoTributacao.NaturezaProduto = (EnumNaturezaProduto)cboNaturezaProduto.EditValue;
            grupoTributacao.RegimeTributario = (EnumCodigoRegimeTributario)cboRegimeTributario.EditValue;

            return grupoTributacao;
        }
        

        #endregion

        #region " CLASSES AUXILIARES "

        private class TributacaoGrid
        {
            public int Id { get; set; }

            public string TipoSaida { get; set; }

            public string EstadoDestino { get; set; }

            public string TipoCliente { get; set; }

            public string Cfop { get; set; }

            public string Csosn { get; set; }
        }


        #endregion

        private void pbPesquisaGrupoTributacaoFederal_Click(object sender, EventArgs e)
        {
            FormGrupoTributacaoFederalPesquisa formGrupoTribucaoPesquisa = new FormGrupoTributacaoFederalPesquisa();

            var GrupoTributacao = formGrupoTribucaoPesquisa.ExibaPesquisaDeGrupoDeTributacaoFederal();

            if (GrupoTributacao != null)
            {
                EditeGrupoTributacaoFederal(GrupoTributacao);
            }
        }

        private void gcIPI_Click(object sender, EventArgs e)
        {

        }
    }
}
