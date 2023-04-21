using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Telas.Cadastros.Empresas;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.LicencaDeUsoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.Login
{
    public partial class FormBoasVindas : FormularioBase
    {
        public DialogResult Resultado { get; set; }

        public FormBoasVindas()
        {
            InitializeComponent();

            Resultado = DialogResult.Cancel;
        }

        private void btnCliqueContinuar_Click(object sender, EventArgs e)
        {
            this.Close();

            FormCadastroEmpresa formCadastroEmpresa = new FormCadastroEmpresa(true);
            formCadastroEmpresa.ShowDialog();

            if (formCadastroEmpresa.Resultado == DialogResult.OK)
            {
                ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
                var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

                string chaveLiberacao = empresa.DadosEmpresa.Cnpj + "|" + DateTime.Now.Date.AddDays(15).ToString("dd/MM/yyyy") + "|3";

                string chaveCriptografada = chaveLiberacao.Criptografar();

                ServicoLicencaDeUso servicoLicencaDeUso = new ServicoLicencaDeUso();
                var licenca = servicoLicencaDeUso.ConsulteUltimaLicencaDeUso();

                licenca.ChaveLiberacao = chaveCriptografada;

                servicoLicencaDeUso.Atualize(licenca);
            }

            Resultado = formCadastroEmpresa.Resultado;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
