using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.OperadorasCartaoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CategoriaServ;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.View.Telas.Financeiro.BancosParaMovimento;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using System.Linq;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;

namespace Programax.Easy.View.Telas.Financeiro.OperadorasCartoes
{
    public partial class FormCadastroOperadorasCartao : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idOperadoras;
        private int _IdBanco;
        private int _idItem;
        private List<ItemOperadorasCartao> _listaItemOperadora;
               
        #endregion

        #region " CONSTRUTOR "

        public FormCadastroOperadorasCartao()
        {
            InitializeComponent();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            PreenchaOStatus();      
            
            PreenchaCboCategoriaDespesa();

            PreenchaCboCondicaoPagamento();

            _listaItemOperadora = new List<ItemOperadorasCartao>();

            this.ActiveControl = txtCodigoOperadora;

            this.NomeDaTela = "Operadoras de Cartão";
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Salve();
        }

        private void pbPesquisaOperadora_Click(object sender, EventArgs e)
        {
            FormOperadorasCartaoPesquisa formOperadorasCartaoPesquisa = new FormOperadorasCartaoPesquisa();

            var operadoras = formOperadorasCartaoPesquisa.ExibaPesquisaDeOperadoras();

            if (operadoras != null)
            {
                EditeOperadorasCartao(operadoras);
            }
        }

        private void gcOperadoras_DoubleClick(object sender, EventArgs e)
        {
            SelecioneItem();           
        }

        private void gcOperadoras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneItem();               
            }
        }

        private void txtCodigoOperadora_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodigoOperadora.Text))
            {
                ServicoOperadorasCartao servicoOperadorasCartao = new ServicoOperadorasCartao();

                var operadoras = servicoOperadorasCartao.ConsulteOperadorasPeloIdInformado(txtCodigoOperadora.Text.ToInt());

                EditeOperadorasCartao(operadoras);
            }
            else
            {
                EditeOperadorasCartao(null);
            }
        }

        private void pbPesquisaBancos_Click(object sender, EventArgs e)
        {
            FormPesquisaBancoParaMovimento formPesquisaBancoParaMovimento = new FormPesquisaBancoParaMovimento();

            var banco = formPesquisaBancoParaMovimento.PesquiseUmBanco();

            if (banco != null)
            {
                txtBanco.Text = banco.NomeBanco;                
                _IdBanco = banco.Id;
            }
        }

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            insiraOuAtualizeItem();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (_listaItemOperadora.Count > 0)
            {
                var idItem = colunaId.View.GetFocusedRowCellValue(colunaId).ToInt();

                var itemASerExcluido = _listaItemOperadora.FirstOrDefault(x => x.Id == idItem);

                var mensagemConfirmacaoExclusao = "Deseja excluir o item " + itemASerExcluido.CondicaoPagamento.Id + " - " + itemASerExcluido.CondicaoPagamento.Descricao + " ?";

                if (MessageBox.Show(mensagemConfirmacaoExclusao, "Deseja excluir este item ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    _listaItemOperadora.Remove(itemASerExcluido);

                    new ServicoItemOperadorasCartao().Exclua(itemASerExcluido.Id);

                    _idItem = 0;

                    PreencherGrid();
                }
            }

            LimpeCamposItem();
            btnInserirAtualizarItem.Image = Properties.Resources.icones2_19;
        }

        private void chkPermiteParcelamento_CheckedChanged(object sender, EventArgs e)
        {
            PreenchaCboCondicaoPagamento();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void LimpeFormulario()
        {  
            _listaItemOperadora = new List<ItemOperadorasCartao>();
            PreencherGrid();
            EditeOperadorasCartao(null);
        }

        private void EditeItem(ItemOperadorasCartao item)
        {
            if(item != null)
            {   
                _idItem = item.Id;
                cboCondicaoPagamento.EditValue = item.CondicaoPagamento.Id;
                txtParcelaParaComecarCobrarTaxa.Text = item.CobrarApartirDaParcela.ToString();
                txtTaxaAdministracao.Text = item.Taxa.ToString();

                cboCondicaoPagamento.Focus();

                btnInserirAtualizarItem.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                _idItem = 0;

                LimpeCamposItem();
            }
        }

        private void LimpeCamposItem()
        {
            cboCondicaoPagamento.EditValue = null;
            txtParcelaParaComecarCobrarTaxa.Text = string.Empty;
            txtTaxaAdministracao.Text = string.Empty;
        }

        private void EditeOperadorasCartao(OperadorasCartao operadoras)
        {
            if (operadoras != null)
            {
                _idOperadoras = operadoras.Id;
                txtDescricao.Text = operadoras.Descricao;
                txtDataCadastro.Text = operadoras.DataCadastro.ToString("dd/MM/yyyy");
                txtCodigoOperadora.Text = operadoras.Id.ToString();
               
                var banco = new ServicoBancoParaMovimento().Consulte(operadoras.BancoParaMovimento.Id.ToInt());

                txtBanco.Text = banco != null? banco.NomeBanco:string.Empty;                
                _IdBanco = banco != null? banco.Id:0;

                txtPrazoParaCreditar.Text = operadoras.DiasPrazoParaCreditar.ToString();

                chkPermiteParcelamento.Checked = operadoras.PermiteParcelamento;
                chkRecebimentoAntecipado.Checked = operadoras.RecebimentoAntecipado;

                cboCategoriaDespesa.EditValue = operadoras.CategoriaDeDespesa != null? operadoras.CategoriaDeDespesa.Id:0;
                txtParcelaParaComecarCobrarTaxa.Text = operadoras.CobrarTaxaApartirDaParcela.ToString();

                txtTaxaAdministracao.Text = operadoras.TaxaAdministracao.ToString();

                cboStatus.EditValue = operadoras.Status;

                var listaOper = new ServicoItemOperadorasCartao().ConsulteLista(operadoras.Id);

                if (listaOper.Count > 0)
                {
                    _listaItemOperadora.Clear();
                    _listaItemOperadora=listaOper;
                }   
                else
                    _listaItemOperadora = new List<ItemOperadorasCartao>();

                PreenchaCboCondicaoPagamento();
                PreencherGrid();
            }
            else
            {
                _idOperadoras = 0;
                txtCodigoOperadora.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                cboStatus.EditValue = "A";

                txtBanco.Text = string.Empty;
                
                _IdBanco = 0;

                txtPrazoParaCreditar.Text = string.Empty;

                chkPermiteParcelamento.Checked = false;
                chkRecebimentoAntecipado.Checked = false;

                cboCategoriaDespesa.EditValue = null;

                txtParcelaParaComecarCobrarTaxa.Text = string.Empty;

                txtTaxaAdministracao.Text = string.Empty;

                txtDescricao.Enabled = true;
                txtCodigoOperadora.Enabled = true;
                txtDataCadastro.Enabled = true;

                cboStatus.Enabled = true;
                
                txtDescricao.Focus();

                PreenchaCboCondicaoPagamento();

                _listaItemOperadora = new List<ItemOperadorasCartao>();
                PreencherGrid();
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

        private void PreenchaCboCategoriaDespesa()
        {
            ServicoCategoria servicoCategoria = new ServicoCategoria();

            var listaCategorias = servicoCategoria.ConsulteLista("", null, "A", EnumTipoCategoria.DESPESA);

            listaCategorias.Insert(0, null);

            cboCategoriaDespesa.Properties.DataSource = listaCategorias;
            cboCategoriaDespesa.Properties.DisplayMember = "Descricao";
            cboCategoriaDespesa.Properties.ValueMember = "Id";

            if (cboCategoriaDespesa.EditValue != null)
            {
                if (!listaCategorias.Exists(categoria => categoria != null && categoria.Id == cboCategoriaDespesa.EditValue.ToInt()))
                {
                    cboCategoriaDespesa.EditValue = null;
                }
            }
        }
        
        private void PreencherGrid()
        {
            List<OperadorasAuxiliarItem> listaDeOperadoresAuxiliares = new List<OperadorasAuxiliarItem>();

            foreach (var itemOperadoras in _listaItemOperadora)
            {
                OperadorasAuxiliarItem operadorasAuxiliar = new OperadorasAuxiliarItem();

                operadorasAuxiliar.Id = itemOperadoras.Id;
                operadorasAuxiliar.CondicaoPagamento = itemOperadoras.CondicaoPagamento != null ? itemOperadoras.CondicaoPagamento.Id + " - " + itemOperadoras.CondicaoPagamento.Descricao : null;                         
                operadorasAuxiliar.ParcelaCobrarTaxa = itemOperadoras.CobrarApartirDaParcela.ToString();
                operadorasAuxiliar.Taxa = itemOperadoras.Taxa.ToString();
                               
                listaDeOperadoresAuxiliares.Add(operadorasAuxiliar);
            }

            gcOperadoras.DataSource = listaDeOperadoresAuxiliares;
            gcOperadoras.RefreshDataSource();
        }

        private void SelecioneItem()
        {
            if(_listaItemOperadora != null && _listaItemOperadora.Count > 0)
            {
                var item = _listaItemOperadora.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                EditeItem(item);
            }
        }

        private void PesquisePeloId()
        {
            ServicoOperadorasCartao servicoOperadoras = new ServicoOperadorasCartao();
            var operadoras = servicoOperadoras.Consulte(_idOperadoras);

            EditeOperadorasCartao(operadoras);
        }

        private void Salve()
        {
            if( (_listaItemOperadora.Count == 0 && cboCategoriaDespesa.EditValue.ToInt() != 0) && (txtParcelaParaComecarCobrarTaxa.Text.ToInt() == 0 || txtTaxaAdministracao.Text.ToInt() == 0))
            {
                MessageBox.Show("Ao informar a Categoria, deve se informar a parcela e taxa a ser cobrada.", "Cadastro de Operadoras de Cartão", MessageBoxButtons.OK);
                return;
            }
            else if((txtParcelaParaComecarCobrarTaxa.Text.ToInt() != 0 || txtTaxaAdministracao.Text.ToInt() != 0) && (cboCategoriaDespesa.EditValue.ToInt() == 0 && _listaItemOperadora.Count == 0))
            {
                MessageBox.Show("Ao informar a Parcela e Taxa a ser cobrada, deve se informar a Categoria de Despesa.", "Cadastro de Operadoras de Cartão", MessageBoxButtons.OK);
                return;
            }
            
            Action actionSalvar = () =>
            {
                var operadoras = RetorneOperadorasCartaoEmEdicao();

                ServicoOperadorasCartao servicoOperadoras = new ServicoOperadorasCartao();

                if (operadoras.Id == 0)
                {
                    var idOper = servicoOperadoras.Cadastre(operadoras);

                    foreach (var item in _listaItemOperadora)
                    {
                        item.OperadorasId = idOper;
                    }

                    ServicoItemOperadorasCartao servicoItem = new ServicoItemOperadorasCartao();

                    servicoItem.CadastreLista(_listaItemOperadora);
                }
                else
                {
                    servicoOperadoras.Atualize(operadoras);
                }

                _idOperadoras = operadoras.Id;

                PesquisePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private OperadorasCartao RetorneOperadorasCartaoEmEdicao()
        {
            OperadorasCartao operadoras = new OperadorasCartao();

            operadoras.Id = _idOperadoras;
            operadoras.Descricao = txtDescricao.Text;
            operadoras.DataCadastro = txtDataCadastro.Text.ToDate();
            operadoras.DiasPrazoParaCreditar = txtPrazoParaCreditar.Text.ToInt();

            operadoras.PermiteParcelamento = chkPermiteParcelamento.Checked ? true : false;
            operadoras.RecebimentoAntecipado = chkRecebimentoAntecipado.Checked ? true : false;

            operadoras.BancoParaMovimento = new BancoParaMovimento { Id = _IdBanco };
            operadoras.CategoriaDeDespesa = cboCategoriaDespesa.EditValue.ToInt() != 0 ? new CategoriaFinanceira { Id = cboCategoriaDespesa.EditValue.ToInt() } : null;
            operadoras.CobrarTaxaApartirDaParcela = txtParcelaParaComecarCobrarTaxa.Text.ToInt();
            operadoras.TaxaAdministracao = txtTaxaAdministracao.Text.ToDouble();

            operadoras.Status = cboStatus.EditValue.ToString();

            return operadoras;
        }

        public void insiraOuAtualizeItem()
        {
            if (cboCondicaoPagamento.Text == string.Empty || txtParcelaParaComecarCobrarTaxa.Text.ToInt() == 0 || txtTaxaAdministracao.Text.ToInt() == 0)
            {
                MessageBox.Show("Para continuar, você deve informar: a condição de pagamento, a parcela para começar a pagar e a Taxa.","Adicionando Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ItemOperadorasCartao itemOperadoras = new ItemOperadorasCartao();

            itemOperadoras.CondicaoPagamento = cboCondicaoPagamento != null ? new CondicaoPagamento { Id = cboCondicaoPagamento.EditValue.ToInt(), Descricao = cboCondicaoPagamento.Text } : null;
            itemOperadoras.CobrarApartirDaParcela = txtParcelaParaComecarCobrarTaxa.Text.ToInt();
            itemOperadoras.Taxa = txtTaxaAdministracao.Text.ToDouble();
            itemOperadoras.Id = _idItem;
            
            if (itemOperadoras.Id == 0)
            {
                _listaItemOperadora.Add(itemOperadoras);

                if(txtCodigoOperadora.Text.ToInt() != 0)
                {
                    itemOperadoras.OperadorasId = txtCodigoOperadora.Text.ToInt();

                    ServicoItemOperadorasCartao servicoItem = new ServicoItemOperadorasCartao();

                    servicoItem.Cadastre(itemOperadoras);
                }
            }
            else
            {
                var idex = _listaItemOperadora.FindIndex(x=>x.Id== itemOperadoras.Id);

                itemOperadoras.OperadorasId = txtCodigoOperadora.Text.ToInt();

                _listaItemOperadora[idex] = itemOperadoras;

                ServicoItemOperadorasCartao servicoItem = new ServicoItemOperadorasCartao();

                servicoItem.Atualize(itemOperadoras);
            }

            LimpeCamposItem();

            btnInserirAtualizarItem.Image = Properties.Resources.icones2_19;

            PreencherGrid();
        }

        private void PreenchaCboCondicaoPagamento()
        {
            List<CondicaoPagamento> listaCondicoes = new List<CondicaoPagamento>();

            var codigoFormaPgto = chkPermiteParcelamento.Checked ? 7 : 8; //7 - Crédito 8 - Débito

            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
            var formaPagamento = servicoFormaPagamento.Consulte(codigoFormaPgto);


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

            if (listaCondicoes.Count == 2)
            {
                cboCondicaoPagamento.EditValue = listaCondicoes[1].Id;
            }

        }

        #endregion

            #region "Classes Auxiliares"

        private class AgenciaAuxiliarComboBox
        {
            public int Id { get; set; }

            public string Descricao { get; set; }
        }

        private class OperadorasAuxiliarItem
        {
            public int Id { get; set; }

            public string CondicaoPagamento { get; set; }

            public string ParcelaCobrarTaxa { get; set; }

            public string Taxa { get; set; }

        }

        #endregion

    }
}