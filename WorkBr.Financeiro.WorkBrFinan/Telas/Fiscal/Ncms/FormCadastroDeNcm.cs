using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.NcmServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.IO;
using System.Linq;
using System.Text;
using Programax.Easy.View.Telas.Fiscal.Cests;
using Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Fiscal.Ncms
{
    public partial class FormCadastroDeNcm : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idNcm;
        private string mensagemSucesso = "Arquivo Importado com Sucesso!";

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroDeNcm()
        {
            InitializeComponent();

            PreenchaOStatus();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            this.ActiveControl = txtCodigoNcm;

            this.NomeDaTela = "Cadastro de NCM";
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
                Ncm ncm = new Ncm();

                ncm.Id = _idNcm;
                ncm.Descricao = txtDescricao.Text;
                ncm.Cest = txtCest.Text;
                ncm.CodigoNcm = txtCodigoNcm.Text;
                ncm.DataCadastro = txtDataCadastro.Text.ToDate();
                ncm.Status = cboStatus.EditValue.ToString();

                ncm.ImpostoIbptFederalImportados = txtAliquotaFederalImportacao.Text.ToDouble();
                ncm.ImpostoIbptFederalNacional = txtAliquotaFederalImportacao.Text.ToDouble();
                ncm.ImpostoIbptEstadual = txtAliquotaEstadual.Text.ToDouble();
                ncm.ImpostoIbptMunicipal = txtAliquotaMunicipal.Text.ToDouble();

                ncm.ChaveTabelaIbpt = txtChaveIbpt.Text;
                ncm.DataValidadeIbpt = txtDataValidadeAliquotas.Text.ToDate();

                ServicoNcm servicoNcm = new ServicoNcm();

                if (ncm.Id == 0)
                {
                    servicoNcm.Cadastre(ncm);
                }
                else
                {
                    servicoNcm.Atualize(ncm);
                }

                PesquisePeloCodigoNcm();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormNcmPesquisa formNcmPesquisa = new FormNcmPesquisa();

            var ncm = formNcmPesquisa.ExibaPesquisaDeNcm();

            if (ncm != null)
            {
                EditeNcm(ncm);
            }
        }

        private void txtCodigoNcm_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodigoNcm.Text))
            {
                PesquisePeloCodigoNcm();
            }
        }

        private void pbPesquisaCest_Click(object sender, EventArgs e)
        {
            FormCestPesquisa formCestPesquisa = new FormCestPesquisa();

            var Cest = formCestPesquisa.ExibaPesquisaDeCest();

            if (Cest != null)
            {
                txtCest.Text = string.Format(@"{0:00\.000\.00}", Cest.CodigoCest.ToLong());             
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

        private void LimpeFormulario(bool focoNoCodigoNcm = true)
        {
            _idNcm = 0;
            txtDescricao.Text = string.Empty;

            txtCest.Text = string.Empty;
            txtCodigoNcm.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            cboStatus.EditValue = "A";

            txtAliquotaFederalImportacao.Text = string.Empty;
            txtAliquotaFederalNacional.Text = string.Empty;
            txtAliquotaEstadual.Text = string.Empty;
            txtAliquotaMunicipal.Text = string.Empty;

            if (focoNoCodigoNcm)
            {
                txtCodigoNcm.Focus();
            }
        }

        private void EditeNcm(Ncm ncm)
        {
            if (ncm != null)
            {
                _idNcm = ncm.Id;
                txtDescricao.Text = ncm.Descricao;

                txtCodigoNcm.Text = ncm.CodigoNcm;
                txtCest.Text = ncm.Cest;
                txtDescricao.Text = ncm.Descricao;
                txtDataCadastro.Text = ncm.DataCadastro.ToString("dd/MM/yyyy");
                cboStatus.EditValue = ncm.Status;

                txtDataValidadeAliquotas.DateTime = ncm.DataValidadeIbpt;
                txtChaveIbpt.Text = ncm.ChaveTabelaIbpt;

                txtAliquotaFederalImportacao.Text = ncm.ImpostoIbptFederalImportados.ToString("0.00");
                txtAliquotaFederalNacional.Text = ncm.ImpostoIbptFederalNacional.ToString("0.00");
                txtAliquotaEstadual.Text = ncm.ImpostoIbptEstadual.ToString("0.00");
                txtAliquotaMunicipal.Text = ncm.ImpostoIbptMunicipal.ToString("0.00");

                txtDescricao.Focus();
            }
            else
            {
                _idNcm = 0;
                txtDescricao.Text = string.Empty;

                txtCodigoNcm.Text = string.Empty;
                txtDescricao.Text = string.Empty;
                txtCest.Text = string.Empty;
                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                txtDataValidadeAliquotas.Text = string.Empty;

                cboStatus.EditValue = null;

                txtAliquotaFederalImportacao.Text = string.Empty;
                txtAliquotaFederalNacional.Text = string.Empty;
                txtAliquotaEstadual.Text = string.Empty;
                txtAliquotaMunicipal.Text = string.Empty;

                txtDescricao.Focus();

                MessageBox.Show("Ncm não encontrado", "Ncm não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PesquisePeloCodigoNcm()
        {
            ServicoNcm servicoNcm = new ServicoNcm();
            var ncm = servicoNcm.ConsultePeloCodigoNcm(txtCodigoNcm.Text);

            if (ncm != null)
            {
                EditeNcm(ncm);
            }
            else
            {
                var codigoNcm = txtCodigoNcm.Text;

                LimpeFormulario(focoNoCodigoNcm: false);

                txtCodigoNcm.Text = codigoNcm;
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
                    openFileDialog.Title = "Arquivo de Alíquotas IBPT";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        arquivo = openFileDialog.FileName;
                    }
                    else
                    {
                        mensagemSucesso = "Cancelado";
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

                ServicoNcm servicoNcm = new ServicoNcm();
                servicoNcm.ImporteConteudoArquivoNcm(conteudoArquivo);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionImportar, mensagemDeSucesso: mensagemSucesso);
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            Action actionAtualizar = () =>
            {
                string arquivo = string.Empty;

                if (File.Exists("C:/Programax/Tabela_Ncm_Ibpt.csv"))
                {
                    arquivo = "C:/Programax/Tabela_Ncm_Ibpt.csv";
                }
                else
                {
                    MessageBox.Show("Arquivo não encontrado! O arquivo: Tabela_Ncm_Ibpt - deve estar na pasta Programax. " +
                                    "Contate o suporte! Caso você tenha o arquivo, utilizar a opção: Importar Arquivo.", "Atualização de NCMs");                    
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

                ServicoNcm servicoNcm = new ServicoNcm();
                servicoNcm.ImporteConteudoArquivoNcm(conteudoArquivo);
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionAtualizar, mensagemDeSucesso: "Arquivo Atualizado com Sucesso!");

            this.Cursor = Cursors.Default;
        }
    }
}
