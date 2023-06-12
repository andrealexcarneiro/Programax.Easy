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
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.GrupoTributacaoIcmsServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.View.Telas.Cadastros.GrupoTributacoesIcms
{
    public partial class FormCadastroGrupoTributacaoIcms : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Cfop> _listaCfop;
        private Empresa _empresa;
        private List<TributacaoIcms> _listaTributacoes;
        private TributacaoIcms _tributacaoIcmsEmEdicao;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroGrupoTributacaoIcms()
        {
            InitializeComponent();

            _listaTributacoes = new List<TributacaoIcms>();

            PesquiseEmpresa();
            PreenchaCboRegimeTributario();
            PreenchaCboNaturezaProduto();
            PreenchaCboTipoSaida();
            PreenchaTipoCliente();
            PreenchaCboCsosn(_empresa.DadosEmpresa.CodigoRegimeTributario);
            PreenchaCboEstado();
            PesquiseTodosCfop();
            PreenchaTipoInscricaoIcms();

            this.ActiveControl = txtId;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void cboCsoSn_EditValueChanged(object sender, EventArgs e)
        {
            if ((EnumCodigoRegimeTributario)cboRegimeTributario.EditValue== EnumCodigoRegimeTributario.REGIMENORMAL)            
                HalilitaBotoesCsoRegimeNormal();
            else
                HabilitaBotoesCsoSimplesNacional();           
        }

        private void cboRegimeTributario_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCsosn((EnumCodigoRegimeTributario)cboRegimeTributario.EditValue);
        }

        private void cboTipoSaida_EditValueChanged(object sender, EventArgs e)
        {
            AltereCboCfop();
        }

        private void cboEstadoDestino_EditValueChanged(object sender, EventArgs e)
        {
            AltereCboCfop();
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeCamposTributacaoIcms();
        }

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            AdicioneOuAtualizeTributacao();
        }

        private void gcTributacoes_DoubleClick(object sender, EventArgs e)
        {
            if (HouveUmClickOuDuploClickNaLinha(gridView5))
            {
                Selecione();
            }
        }

        private void gcTributacoes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir essa tributação?", "Excluir tributação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            _listaTributacoes.Remove(_tributacaoIcmsEmEdicao);

            PreenchaGrid();
            LimpeCamposTributacaoIcms();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            EditeGrupoTributacaoIcms(null);
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                ServicoGrupoTributacaoIcms servicoGrupoTributacaoIcms = new ServicoGrupoTributacaoIcms();
                var grupoTributacaoIcms = servicoGrupoTributacaoIcms.Consulte(txtId.Text.ToInt());

                EditeGrupoTributacaoIcms(grupoTributacaoIcms, exibirMensagemDeNaoEncontrado: true);
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                ServicoGrupoTributacaoIcms servicoGrupoTributacaoIcms = new ServicoGrupoTributacaoIcms();

                GrupoTributacaoIcms grupoTributacaoIcms = RetorneGrupoTributacaoIcmsEmEdicao();

                if (grupoTributacaoIcms.Id == 0)
                {
                    servicoGrupoTributacaoIcms.Cadastre(grupoTributacaoIcms);
                }
                else
                {
                    servicoGrupoTributacaoIcms.Atualize(grupoTributacaoIcms);
                }

                EditeGrupoTributacaoIcms(grupoTributacaoIcms);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void pbPesquisaGrupoTributacaoIcms_Click(object sender, EventArgs e)
        {
            FormGrupoTributacaoIcmsPesquisa formGrupoTribucaoPesquisa = new FormGrupoTributacaoIcmsPesquisa();

            var GrupoTributacao = formGrupoTribucaoPesquisa.ExibaPesquisaDeGrupoDeTributacaoIcms();

            if (GrupoTributacao != null)
            {
                EditeGrupoTributacaoIcms(GrupoTributacao);
            }
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

        private void PreenchaTipoInscricaoIcms()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoInscricaoICMS>();

            cboTipoInscricaoICMS.Properties.DataSource = lista;
            cboTipoInscricaoICMS.Properties.DisplayMember = "Descricao";
            cboTipoInscricaoICMS.Properties.ValueMember = "Valor";

            cboTipoInscricaoICMS.EditValue = EnumTipoInscricaoICMS.CONTRIBUINTEICMS;
        }

        private void PreenchaCboCsosn(EnumCodigoRegimeTributario? Regime)
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumCstCsosn>();

            if (Regime == EnumCodigoRegimeTributario.REGIMENORMAL)
            {
                lista.RemoveRange(11, 10);
                groupBox2.Height = 182;
                this.gcTributacoes.Top = 268;
                this.gcTributacoes.Height = 181;
                
            }
            else
            {
                lista.RemoveRange(0, 11);                
                this.gcTributacoes.Height = 235;
                this.gcTributacoes.Top = 220;
                groupBox2.Height = 126;
            }

            cboCsoSn.Properties.DataSource = lista;
            cboCsoSn.Properties.DisplayMember = "Descricao";
            cboCsoSn.Properties.ValueMember = "Valor";
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

        private void AltereCboCfop()
        {
            EnumTipoSaidaTributacaoIcms tipoSaida = (EnumTipoSaidaTributacaoIcms)cboTipoSaida.EditValue;
            string estadoDestino = cboEstadoDestino.EditValue.ToStringEmpty();

            List<ObjetoDescricaoValor> listaDescricaoValor = new List<ObjetoDescricaoValor>();
            List<Cfop> listaCfop = new List<Cfop>();

            if (!string.IsNullOrEmpty(estadoDestino))
            {
                if (tipoSaida == EnumTipoSaidaTributacaoIcms.SAIDAVENDA)
                {
                    if (estadoDestino == _empresa.DadosEmpresa.Endereco.Cidade.Estado.UF)
                    {
                        listaCfop = _listaCfop.FindAll(cfo => cfo.Codigo[0] == '5').ToList();
                    }
                    else
                    {
                        listaCfop = _listaCfop.FindAll(cfo => cfo.Codigo[0] == '6').ToList();
                    }
                }
                else
                {
                    if (estadoDestino == _empresa.DadosEmpresa.Endereco.Cidade.Estado.UF)
                    {
                        listaCfop = _listaCfop.FindAll(cfo => cfo.Codigo[0] == '1').ToList();
                    }
                    else
                    {
                        listaCfop = _listaCfop.FindAll(cfo => cfo.Codigo[0] == '2').ToList();
                    }
                }
            }

            foreach (var cfop in listaCfop)
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor();
                objetoDescricaoValor.Valor = cfop.Id;
                objetoDescricaoValor.Descricao = cfop.Codigo + " - " + cfop.Descricao;

                listaDescricaoValor.Add(objetoDescricaoValor);
            }

            cboCfop.Properties.DataSource = listaDescricaoValor;
            cboCfop.Properties.ValueMember = "Valor";
            cboCfop.Properties.DisplayMember = "Descricao";

            cboCfop.EditValue = null;
        }

        private void AdicioneOuAtualizeTributacao()
        {
            Action actionInserirTributacao = () =>
            {
                TributacaoIcms tributacaoIcms = RetorneTributacaoIcmsEmEdicao();

                ServicoGrupoTributacaoIcms servicoGrupoTributacaoIcms = new ServicoGrupoTributacaoIcms();

                //servicoGrupoTributacaoIcms.ValideTributacaoIcms(tributacaoIcms, _listaTributacoes);

                _listaTributacoes.Remove(_tributacaoIcmsEmEdicao);

                if (tributacaoIcms.Id == 0)
                {
                    _listaTributacoes.Add(tributacaoIcms);
                }
                else
                {
                    int posicaoItem = tributacaoIcms.Id - 1;

                    _listaTributacoes.Insert(posicaoItem, tributacaoIcms);
                }

                PreenchaGrid();

                LimpeCamposTributacaoIcms();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirTributacao, exibirMensagemDeSucesso: false);
        }

        private TributacaoIcms RetorneTributacaoIcmsEmEdicao()
        {
            _tributacaoIcmsEmEdicao = _tributacaoIcmsEmEdicao ?? new TributacaoIcms();

            var tributacaoIcms = _tributacaoIcmsEmEdicao.CloneCompleto();

            tributacaoIcms.AliquotaCreditoST = txtAliquotaCreditoST.Text.ToDoubleNullabel();
            tributacaoIcms.AliquotaDebitoST = txtAliquotaDebitoST.Text.ToDoubleNullabel();
            tributacaoIcms.Cfop = cboCfop.EditValue != null ? new Cfop { Id = cboCfop.EditValue.ToInt() } : null;
            tributacaoIcms.CstCsosn = (EnumCstCsosn?)cboCsoSn.EditValue;
            tributacaoIcms.MVA = txtMVA.Text.ToDoubleNullabel();
            tributacaoIcms.ReducaoBaseST = txtPercentualReducaoBaseST.Text.ToDoubleNullabel();
            tributacaoIcms.PercentualMargemAdicST = txtPercentualMargemAdicST.Text.ToDoubleNullabel();
            tributacaoIcms.AliquotaIcmsST = txtPercentualAliquotaST.Text.ToDoubleNullabel();
            tributacaoIcms.ModalidadeIcmsST = (EnumModBCST?)cboModBCST.EditValue;
            tributacaoIcms.ModalidadeBaseCalculo = (EnumModBC?)cboModBC.EditValue;
            tributacaoIcms.IcmsReducaoBaseCalculo = txtPercentualReducao.Text.ToDoubleNullabel();
            tributacaoIcms.IcmsBaseCalculo = txtPercentualIcms.Text.ToDoubleNullabel();
            tributacaoIcms.TipoCliente = (EnumTipoCliente)cboTipoCliente.EditValue;
            tributacaoIcms.TipoInscricaoICMS = cboTipoInscricaoICMS.EditValue == null? EnumTipoInscricaoICMS.CONTRIBUINTEICMS: (EnumTipoInscricaoICMS)cboTipoInscricaoICMS.EditValue;
            tributacaoIcms.TipoSaida = (EnumTipoSaidaTributacaoIcms)cboTipoSaida.EditValue;
            tributacaoIcms.EstadoDestino = cboEstadoDestino.EditValue.ToStringEmpty();

            return tributacaoIcms;
        }

        private void PreenchaGrid()
        {
            GereIdFalsoParaOsItens();

            List<TributacaoIcmsGrid> listaTributacaoIcmsGrid = new List<TributacaoIcmsGrid>();

            foreach (var tributacaoIcms in _listaTributacoes)
            {
                TributacaoIcmsGrid tributacaoIcmsGrid = new TributacaoIcmsGrid();
                //if (tributacaoIcms.Cfop != null)
                //{
                    var cfop = _listaCfop.FirstOrDefault(cfo => cfo.Id == tributacaoIcms.Cfop.Id);
                    tributacaoIcmsGrid.Cfop = cfop.Codigo;
                //}
                

                
                tributacaoIcmsGrid.Csosn = ((int)tributacaoIcms.CstCsosn).ToString();
                tributacaoIcmsGrid.EstadoDestino = tributacaoIcms.EstadoDestino;
                tributacaoIcmsGrid.Id = tributacaoIcms.Id;
                tributacaoIcmsGrid.TipoCliente = tributacaoIcms.TipoCliente.Descricao();
                tributacaoIcmsGrid.TipoInscricao = tributacaoIcms.TipoInscricaoICMS!=null? tributacaoIcms.TipoInscricaoICMS.Descricao():null;
                tributacaoIcmsGrid.TipoSaida = tributacaoIcms.TipoSaida.Descricao();

                listaTributacaoIcmsGrid.Add(tributacaoIcmsGrid);
            }

            gcTributacoes.DataSource = listaTributacaoIcmsGrid;
            gcTributacoes.RefreshDataSource();
        }

        private void GereIdFalsoParaOsItens()
        {
            for (int i = 0; i < _listaTributacoes.Count; i++)
            {
                _listaTributacoes[i].Id = i + 1;
            }
        }

        private void LimpeCamposTributacaoIcms(bool focoNoCboTipoSaida = true)
        {
            EditeTributacaoIcms(null);

            if (focoNoCboTipoSaida)
            {
                cboTipoSaida.Focus();
            }
        }

        private void EditeTributacaoIcms(TributacaoIcms tributacaoIcms)
        {
            _tributacaoIcmsEmEdicao = tributacaoIcms;

            if (tributacaoIcms != null)
            {
                cboTipoSaida.EditValue = tributacaoIcms.TipoSaida;
                cboEstadoDestino.EditValue = tributacaoIcms.EstadoDestino;
                cboTipoCliente.EditValue = tributacaoIcms.TipoCliente;
                cboTipoInscricaoICMS.EditValue = tributacaoIcms.TipoInscricaoICMS;
                cboCfop.EditValue = tributacaoIcms.Cfop.Id;
                cboCsoSn.EditValue = tributacaoIcms.CstCsosn;
                txtAliquotaCreditoST.Text = tributacaoIcms.AliquotaCreditoST != null ? tributacaoIcms.AliquotaCreditoST.Value.ToString("0.00") : string.Empty;
                txtAliquotaDebitoST.Text = tributacaoIcms.AliquotaDebitoST != null ? tributacaoIcms.AliquotaDebitoST.Value.ToString("0.00") : string.Empty;
                txtMVA.Text = tributacaoIcms.MVA != null ? tributacaoIcms.MVA.Value.ToString("0.00") : string.Empty;
                txtPercentualReducaoBaseST.Text = tributacaoIcms.ReducaoBaseST != null ? tributacaoIcms.ReducaoBaseST.Value.ToString("0.00") : string.Empty;

                txtPercentualMargemAdicST.Text = tributacaoIcms.PercentualMargemAdicST != null ? tributacaoIcms.PercentualMargemAdicST.Value.ToString("0.00") : string.Empty;
                txtPercentualAliquotaST.Text = tributacaoIcms.AliquotaIcmsST != null? tributacaoIcms.AliquotaIcmsST.Value.ToString("0.00"): string.Empty;
                cboModBCST.EditValue = tributacaoIcms.ModalidadeIcmsST;
                cboModBC.EditValue = tributacaoIcms.ModalidadeBaseCalculo;
                txtPercentualReducao.Text = tributacaoIcms.IcmsReducaoBaseCalculo != null? tributacaoIcms.IcmsReducaoBaseCalculo.Value.ToString("0.00"): string.Empty;
                txtPercentualIcms.Text = tributacaoIcms.IcmsBaseCalculo !=null? tributacaoIcms.IcmsBaseCalculo.Value.ToString("0.00"): string.Empty;
                
                btnExcluirItem.Visible = true;
                btnInserirAtualizarItem.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                cboTipoSaida.EditValue = EnumTipoSaidaTributacaoIcms.SAIDAVENDA;
                cboEstadoDestino.EditValue = null;
                cboTipoCliente.EditValue = EnumTipoCliente.CONSUMIDOR;
                cboTipoInscricaoICMS.EditValue = EnumTipoInscricaoICMS.CONTRIBUINTEICMS;
                cboCfop.EditValue = null;
                cboCsoSn.EditValue = null;
                txtAliquotaCreditoST.Text = string.Empty;
                txtAliquotaDebitoST.Text = string.Empty;
                txtMVA.Text = string.Empty;
                txtPercentualReducaoBaseST.Text = string.Empty;

                btnExcluirItem.Visible = false;
                btnInserirAtualizarItem.Image = Properties.Resources.icones2_19;
            }
        }

        private void Selecione()
        {
            var tributacaoIcms = _listaTributacoes.FirstOrDefault(trib => trib.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

            EditeTributacaoIcms(tributacaoIcms);
        }

        private void EditeGrupoTributacaoIcms(GrupoTributacaoIcms grupoTributacaoIcms, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (grupoTributacaoIcms != null)
            {
                txtId.Enabled = false;

                txtId.Text = grupoTributacaoIcms.Id.ToString();
                txtDescricao.Text = grupoTributacaoIcms.Descricao;
                cboRegimeTributario.EditValue = grupoTributacaoIcms.RegimeTributario;
                cboNaturezaProduto.EditValue = grupoTributacaoIcms.NaturezaProduto;

               _listaTributacoes = grupoTributacaoIcms.ListaTributacoesIcms.ToList();

                PreenchaGrid();
                LimpeCamposTributacaoIcms(false);
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Grupo de Tributação nao encontrado!", "Grupo de Tributação não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpeCamposTributacaoIcms(false);
                _listaTributacoes.Clear();
                PreenchaGrid();
                txtId.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                cboRegimeTributario.EditValue = EnumCodigoRegimeTributario.SIMPLESNACIONAL;
                cboNaturezaProduto.EditValue = EnumNaturezaProduto.TERCEIROS;

                txtId.Enabled = true;
                txtId.Focus();
            }
        }

        private GrupoTributacaoIcms RetorneGrupoTributacaoIcmsEmEdicao()
        {
            GrupoTributacaoIcms grupoTributacaoIcms = new GrupoTributacaoIcms();
            grupoTributacaoIcms.Descricao = txtDescricao.Text;
            grupoTributacaoIcms.Id = txtId.Text.ToInt();
            grupoTributacaoIcms.ListaTributacoesIcms = _listaTributacoes;
            grupoTributacaoIcms.NaturezaProduto = (EnumNaturezaProduto)cboNaturezaProduto.EditValue;
            grupoTributacaoIcms.RegimeTributario = (EnumCodigoRegimeTributario)cboRegimeTributario.EditValue;

            return grupoTributacaoIcms;
        }

        private void HabilitaBotoesCsoSimplesNacional()
        {
            var csosn = (EnumCstCsosn?)cboCsoSn.EditValue;

            if (csosn.GetValueOrDefault() == EnumCstCsosn.SIMPLES201 ||
                csosn.GetValueOrDefault() == EnumCstCsosn.SIMPLES202 ||
                csosn.GetValueOrDefault() == EnumCstCsosn.SIMPLES203 ||
                csosn.GetValueOrDefault() == EnumCstCsosn.SIMPLES900)
            {
                txtAliquotaCreditoST.Enabled = true;
                txtAliquotaDebitoST.Enabled = true;
                txtMVA.Enabled = true;
                txtPercentualReducaoBaseST.Enabled = true;
            }
            else
            {
                txtAliquotaCreditoST.Enabled = false;
                txtAliquotaCreditoST.Text = string.Empty;

                txtAliquotaDebitoST.Enabled = false;
                txtAliquotaDebitoST.Text = string.Empty;

                txtMVA.Properties.Enabled = false;
                txtMVA.Text = string.Empty;

                txtPercentualReducaoBaseST.Properties.Enabled = false;
                txtPercentualReducaoBaseST.Text = string.Empty;
            }
        }

        private void HalilitaBotoesCsoRegimeNormal()
        {
            var cstCsosn = (EnumCstCsosn?)cboCsoSn.EditValue;
            
            txtPercentualAliquotaST.Enabled = false;
            txtPercentualAliquotaST.Text = string.Empty;
            txtPercentualReducaoBaseST.Enabled = false;
            txtPercentualReducaoBaseST.Text = string.Empty;
            txtPercentualIcms.Enabled = false;
            txtPercentualIcms.Text = string.Empty;
            txtPercentualReducao.Enabled = false;
            txtPercentualReducao.Text = string.Empty;
            txtPercentualMargemAdicST.Enabled = false;
            txtPercentualMargemAdicST.Text = string.Empty;
            txtAliquotaCreditoST.Enabled = false;            
            txtAliquotaDebitoST.Enabled = false;
            txtMVA.Enabled = false;
            txtPercentualReducaoBaseST.Enabled = false;
            cboModBC.Enabled = false;
            cboModBC.EditValue = null;
            cboModBCST.Enabled = false;
            cboModBCST.EditValue = null;
            
            if (cstCsosn != null && (
                 cstCsosn == EnumCstCsosn.NORMAL00 ||                
                 cstCsosn == EnumCstCsosn.NORMAL51 ||                 
                 cstCsosn == EnumCstCsosn.NORMAL70 ||
                 cstCsosn == EnumCstCsosn.NORMAL90))
            {
                txtPercentualIcms.Enabled = true;
                txtPercentualIcms.Text = string.Empty;

                cboModBC.Enabled = true;
                PreenchaCboModBC();
            }           
            
            if (cstCsosn != null && cstCsosn == EnumCstCsosn.NORMAL10)
            {
                txtPercentualIcms.Enabled = true;
                txtPercentualIcms.Text = string.Empty;

                txtPercentualReducao.Enabled = true;
                txtPercentualReducao.Text = string.Empty;

                txtPercentualAliquotaST.Enabled = true;
                txtPercentualAliquotaST.Text = string.Empty;
                
                txtPercentualReducaoBaseST.Enabled = true;
                txtPercentualReducaoBaseST.Text = string.Empty;

                txtPercentualMargemAdicST.Enabled = true;
                txtPercentualMargemAdicST.Text = string.Empty;

                cboModBCST.Enabled = true;
                PreenchaCboModBCST();
                cboModBC.Enabled = true;
                PreenchaCboModBC();
            }

            if (cstCsosn != null && ( 
                cstCsosn == EnumCstCsosn.NORMAL20 ||                
                cstCsosn == EnumCstCsosn.NORMAL51))
            {
                txtPercentualIcms.Enabled = true;
                txtPercentualIcms.Text = string.Empty;

                txtPercentualReducao.Enabled = true;
                txtPercentualReducao.Text = string.Empty;

                cboModBC.Enabled = true;
                PreenchaCboModBC();
            }

            if (cstCsosn != null && (                
                cstCsosn == EnumCstCsosn.NORMAL30))
            {
                txtPercentualAliquotaST.Enabled = true;
                txtPercentualAliquotaST.Text = string.Empty;

                txtPercentualReducaoBaseST.Enabled = true;
                txtPercentualReducaoBaseST.Text = string.Empty;

                txtPercentualMargemAdicST.Enabled = true;
                txtPercentualMargemAdicST.Text = string.Empty;

                cboModBCST.Enabled = true;
                PreenchaCboModBCST();
            }

            if (cstCsosn != null && 
                (cstCsosn == EnumCstCsosn.NORMAL70 ||
                cstCsosn == EnumCstCsosn.NORMAL90))
            {
                txtPercentualAliquotaST.Enabled = true;
                txtPercentualAliquotaST.Text = string.Empty;

                txtPercentualIcms.Enabled = true;
                txtPercentualIcms.Text = string.Empty;

                txtPercentualReducao.Enabled = true;
                txtPercentualReducao.Text = string.Empty;

                txtPercentualReducaoBaseST.Enabled = true;
                txtPercentualReducaoBaseST.Text = string.Empty;

                txtPercentualMargemAdicST.Enabled = true;
                txtPercentualMargemAdicST.Text = string.Empty;

                cboModBCST.Enabled = true;
                PreenchaCboModBCST();
                cboModBC.Enabled = true;
                PreenchaCboModBC();
            }
        }

        private void PreenchaCboModBC()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModBC>();

            cboModBC.Properties.DataSource = lista;
            cboModBC.Properties.DisplayMember = "Descricao";
            cboModBC.Properties.ValueMember = "Valor";

            cboModBC.EditValue = EnumModBC.MARGEMVALORAGREGADO;
        }

        private void PreenchaCboModBCST()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModBCST>();

            cboModBCST.Properties.DataSource = lista;
            cboModBCST.Properties.DisplayMember = "Descricao";
            cboModBCST.Properties.ValueMember = "Valor";

            cboModBCST.EditValue = EnumModBCST.MARGEMVALORAGREGADOPERCENTUAL;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class TributacaoIcmsGrid
        {
            public int Id { get; set; }

            public string TipoSaida { get; set; }

            public string EstadoDestino { get; set; }

            public string TipoCliente { get; set; }

            public string TipoInscricao { get; set; }

            public string Cfop { get; set; }

            public string Csosn { get; set; }
        }


        #endregion
        
    }
}
