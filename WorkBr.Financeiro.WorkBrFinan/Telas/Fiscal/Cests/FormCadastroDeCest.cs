using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System.IO;
using System.Text;
using Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.CestServ;
using Programax.Easy.View.Telas.Fiscal.Ncms;
using Programax.Easy.Servico.Fiscal.NcmServ;

namespace Programax.Easy.View.Telas.Fiscal.Cests
{
    public partial class FormCadastroDeCest : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idCest;
        private string _mensagemSucesso = "Arquivo Importado com Sucesso!";

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroDeCest()
        {
            InitializeComponent();

            PreenchaOStatus();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.ActiveControl = txtCodigoCest;

            this.NomeDaTela = "Cadastro de CEST";
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
            Action actionSalvar = () =>
            {
                Cest cest = new Cest();

                cest.Id = chkAtualizarCest.Checked? _idCest:0;
                cest.DescricaoCest = txtDescricaoCest.Text;                
                cest.CodigoCest = txtCodigoCest.Text;
                cest.CodigoNcm = txtCodigoNcm.Text;
                cest.DataCadastro = txtDataCadastro.Text.ToDate();
                cest.Status = cboStatus.EditValue.ToString();
                
                ServicoCest servicoCest = new ServicoCest();

                if (cest.Id == 0)
                {
                    servicoCest.Cadastre(cest);
                }
                else
                {
                    servicoCest.Atualize(cest);
                }

                PesquisePeloCodigoNcm();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void pbPesquisaCest_Click(object sender, EventArgs e)
        {
            FormCestPesquisa formCestPesquisa = new FormCestPesquisa();

            var Cest = formCestPesquisa.ExibaPesquisaDeCest();

            if (Cest != null)
            {
                EditeCest(Cest);
            }
        }

        private void pbPesquisaNcm_Click(object sender, EventArgs e)
        {
            FormNcmPesquisa formCestPesquisa = new FormNcmPesquisa();

            var ncm = formCestPesquisa.ExibaPesquisaDeNcm();

            if (ncm != null)
            {
                txtCodigoNcm.Text = ncm.CodigoNcm;
                txtDescricaoNcm.Text = ncm.Descricao;
            }
        }

        private void txtCodigoNcm_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodigoNcm.Text))
            {
                PesquisePeloCodigoNcm();
            }
        }

        private void txtCodigoCest_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodigoCest.Text))
            {
                PesquisePeloCodigoCest();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

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

        private void LimpeFormulario(bool focoNoCodigoCest = true)
        {
            _idCest = 0;
            txtDescricaoCest.Text = string.Empty;

            txtDescricaoNcm.Text = string.Empty;
            
            txtCodigoCest.Text = string.Empty;
            txtDescricaoCest.Text = string.Empty;
            txtCodigoNcm.Text = string.Empty;
            txtDescricaoCest.Text = string.Empty;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            cboStatus.EditValue = "A";

            chkAtualizarCest.Checked = false;
            
            if (focoNoCodigoCest)
            {
                txtCodigoCest.Focus();
            }
        }
        

        private void EditeCest(Cest cest)
        {
            if (cest != null)
            {
                _idCest = cest.Id;            
                txtCodigoCest.Text = cest.CodigoCest;               
                txtDescricaoCest.Text = cest.DescricaoCest;
                txtDataCadastro.Text = cest.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = cest.Status;

                txtCodigoNcm.Text = cest.CodigoNcm;

                if (cest.CodigoNcm != null)
                    txtDescricaoNcm.Focus();
                else
                    txtDescricaoCest.Focus();                
            }
            else
            {
                _idCest = 0;
                txtCodigoCest.Text = string.Empty;
                txtDescricaoCest.Text = string.Empty;               
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                
                txtDescricaoCest.Focus();

                MessageBox.Show("Cest não encontrado", "Cest não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PesquisePeloCodigoNcm()
        {
            ServicoNcm servicoNcm = new ServicoNcm();
            var ncm = servicoNcm.ConsultePeloCodigoNcm(txtCodigoNcm.Text);

            if (ncm != null)
            {
                txtDescricaoNcm.Text = ncm.Descricao;
            }
            else
            {
                var codigoNcm = txtCodigoNcm.Text;

                //LimpeFormulario(focoNoCodigoCest: false);

                txtCodigoNcm.Text = codigoNcm;
            }
        }

        private void PesquisePeloCodigoCest()
        {
            ServicoCest servicoCest = new ServicoCest();
            var cest = servicoCest.ConsultePeloCodigoCest(txtCodigoCest.Text);

            if (cest != null)
            {
                EditeCest(cest);
            }
            else
            {
                var codigoCest = txtCodigoCest.Text;

                LimpeFormulario(focoNoCodigoCest: false);

                txtCodigoCest.Text = codigoCest;
            }
        }

        #endregion

        private void btnImportar_Click(object sender, EventArgs e)
        {
            Action actionImportar = () =>
            {
                string arquivo = string.Empty;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Arquivo de CESTs";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        arquivo = openFileDialog.FileName;
                    }
                    else
                    {
                        _mensagemSucesso = "Cancelado";
                        return;
                    }
                }

                StringBuilder conteudoArquivo = new StringBuilder();

                using (StreamReader texto = new StreamReader(arquivo, Encoding.Default))
                {
                    string mensagem = string.Empty;

                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        conteudoArquivo.AppendLine(mensagem);
                    }
                }

                ServicoCest servicoCest = new ServicoCest();
                servicoCest.ImporteConteudoArquivoCest(conteudoArquivo);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionImportar, mensagemDeSucesso: _mensagemSucesso);
        }        
    }
}
