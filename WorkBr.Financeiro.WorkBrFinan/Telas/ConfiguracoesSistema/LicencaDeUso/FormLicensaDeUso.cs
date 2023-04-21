using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.LicencaDeUsoServ;
using Programax.Easy.Servico.ConfiguracoesSistema.UsuarioServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.LicencaDeUso
{
    public partial class FormLicensaDeUso : FormularioPadrao
    {
        public FormLicensaDeUso()
        {
            InitializeComponent();

            PreenchaInformacoes();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void PreenchaInformacoes()
        {
            PreenchaDadosEmpresa();
            PreenchaDadosContrato();
        }

        private void PreenchaDadosEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            txtId.Text = empresa.Id.ToString();
            txtNomeFantasia.Text = empresa.DadosEmpresa.NomeFantasia;
            txtDataCadastro.Text = empresa.DadosEmpresa.DataCadastro != null ? empresa.DadosEmpresa.DataCadastro.Value.ToString("dd/MM/yyyy") : string.Empty;
            txtSituacao.Text = "ATIVO";

            txtCpfCnpj.Text = empresa.DadosEmpresa.Cnpj;
            txtRazaoSocial.Text = empresa.DadosEmpresa.RazaoSocial;

            if (empresa.DadosEmpresa.Endereco != null)
            {
                txtCep.Text = empresa.DadosEmpresa.Endereco.CEP;
                txtRua.Text = empresa.DadosEmpresa.Endereco.Rua;
                txtBairro.Text = empresa.DadosEmpresa.Endereco.Bairro;
                txtCidade.Text = empresa.DadosEmpresa.Endereco.Cidade.Descricao;
                txtEstado.Text = empresa.DadosEmpresa.Endereco.Cidade.Estado.UF;
            }

            if (empresa.DadosEmpresa.Foto != null)
            {
                picFoto.Image = empresa.DadosEmpresa.Foto.ToImage();
            }
        }

        private void PreenchaDadosContrato()
        {
            ServicoLicencaDeUso servicoLicencaDeUso = new ServicoLicencaDeUso();
            var licencaDeUso = servicoLicencaDeUso.ConsulteUltimaLicencaDeUso();

            ServicoUsuario servicoUsuario = new ServicoUsuario();
            int quantidadeDeUsuariosAtivos = servicoUsuario.ConsulteQuantidadeDeUsuariosAtivos();

            if (licencaDeUso != null)
            {
                txtIdDatabase.Text = licencaDeUso.IdDatabase.ToString();
                txtContrato.Text = licencaDeUso.Contrato;
                txtLiberadoAte.Text = licencaDeUso.LiberadoAte.ToString("dd/MM/yyyy");

                txtUsersContratados.Text = licencaDeUso.QuantidadeUsuariosContratados.ToString();
                txtUsersLiberados.Text = (licencaDeUso.QuantidadeUsuariosContratados - quantidadeDeUsuariosAtivos).ToString();
                txtUsersUsados.Text = quantidadeDeUsuariosAtivos.ToString();
            }
            else
            {
                txtIdDatabase.Text = string.Empty;
                txtContrato.Text = string.Empty;
                txtLiberadoAte.Text = string.Empty;

                txtUsersContratados.Text = string.Empty;
                txtUsersLiberados.Text = string.Empty;
                txtUsersUsados.Text = quantidadeDeUsuariosAtivos.ToString();
            }
        }
    }
}
