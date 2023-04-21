using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.LicencaDeUsoServ;
using Programax.Easy.View.ClassesAuxiliares;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.Liberacao
{
    public partial class FormLiberacao : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private DialogResult _resultado;

        #endregion

        #region " CONSTRUTOR "

        public FormLiberacao()
        {
            InitializeComponent();

            _resultado = DialogResult.Cancel;

            CaregueInformacoesEmpresa();

            this.ActiveControl = txtChaveLiberacao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                ServicoLicencaDeUso servicoLicencaDeUso = new ServicoLicencaDeUso();
                var consulteUltimaLicencaDeUso = servicoLicencaDeUso.ConsulteUltimaLicencaDeUso();

                consulteUltimaLicencaDeUso.ChaveLiberacao = txtChaveLiberacao.Text;

                servicoLicencaDeUso.Atualize(consulteUltimaLicencaDeUso);

                _resultado = DialogResult.OK;

                this.Close();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult AlterarChaveLiberacao()
        {
            this.ShowDialog();

            return _resultado;
        }

        #endregion

        #region " MÉTODOS PRIVADOS "

        private void CaregueInformacoesEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            ServicoLicencaDeUso servicoLicensaDeUso = new ServicoLicencaDeUso();

            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            var licenca = servicoLicensaDeUso.ConsulteUltimaLicencaDeUso();

            txtLicencasContratadas.Text = licenca.QuantidadeUsuariosContratados.ToString();

            txtCnpj.Text = empresa.DadosEmpresa.Cnpj;
            txtNomeFantasia.Text = empresa.DadosEmpresa.NomeFantasia;
            txtRazaoSocial.Text = empresa.DadosEmpresa.RazaoSocial;

            txtBairro.Text = empresa.DadosEmpresa.Endereco.Bairro;
            txtCidade.Text = empresa.DadosEmpresa.Endereco.Cidade.Descricao;
            txtEstado.Text = empresa.DadosEmpresa.Endereco.Cidade.Estado.UF;
        }

        #endregion
    }
}
