using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using System.IO;
using Newtonsoft.Json;
using Programax.Easy.Servico;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.ConfiguracoesSistema.Login
{
    public partial class FormEscolhaBancoDeDados : FormularioPadrao
    {
        public FormEscolhaBancoDeDados()
        {
            InitializeComponent();

            this.ActiveControl = cboEmpresa;
        }

        private void PreenchaConexoes()
        {
            string conexoesString = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + @"\conexoes.json");

            RegistroDeMapeamentos.ConexoesJson conexoes = JsonConvert.DeserializeObject<RegistroDeMapeamentos.ConexoesJson>(conexoesString);

            if (conexoes.Conexoes.Count == 1)
            {
                RegistroDeMapeamentos.IndiceBancoDados = 0;

                this.Close();

                return;
            }

            List<ObjetoDescricaoValor> lista = new List<ObjetoDescricaoValor>();

            for (int i = 0; i < conexoes.Conexoes.Count; i++)
            {
                var conexao = conexoes.Conexoes[i];

                ObjetoDescricaoValor objeto = new ObjetoDescricaoValor();
                objeto.Descricao = conexao.NomeConexao;
                objeto.Valor = i;

                lista.Add(objeto);
            }

            cboEmpresa.Properties.DataSource = lista;
            cboEmpresa.Properties.ValueMember = "Valor";
            cboEmpresa.Properties.DisplayMember = "Descricao";

            cboEmpresa.EditValue = 0;
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            RegistroDeMapeamentos.IndiceBancoDados = cboEmpresa.EditValue.ToInt();

            this.Close();
        }

        private void FormEscolhaBancoDeDados_Load(object sender, EventArgs e)
        {
            PreenchaConexoes();
        }
    }
}
