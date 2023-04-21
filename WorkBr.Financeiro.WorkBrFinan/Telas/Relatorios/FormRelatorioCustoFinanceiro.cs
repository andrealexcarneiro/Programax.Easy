using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.FabricanteServ;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using BoletoNet.Util;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioCustoFinanceiro : FormularioBase
    {
        Parametros _parametros = new ServicoParametros().ConsulteParametros();

        #region " CONSTRUTOR "

        public FormRelatorioCustoFinanceiro()
        {
            InitializeComponent();

            //PreenchaCboFuncoes();
            PreenchalstVendedores();
            //PreenchaPrimeiroEUltimoDiaMes();
            //PreenchaCboFabricantes();
            //PreenchaCboMarcas();
            //PreenchaCboCategorias();
            //HabiliteDesabiliteSituacoes();

            //DesabiliteSelecaoParceiroSenaoPermitido();
        }

        private void PreenchaCboVendedores()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (txtCusto.Text == "")
            {
                MessageBox.Show("Insira o valor do custo!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string inconsistencias = string.Empty;

          

            this.Cursor = Cursors.WaitCursor;


            DateTime? dataInicial = txtDataInicialEmissao.Text.ToDate();
            DateTime? dataFinal = txtDataFinalEmissao.Text.ToDate();

            inconsistencias += dataInicial == DateTime.MinValue ? "Informe a data inicial do período.\n\n" : string.Empty;
            inconsistencias += dataFinal == DateTime.MinValue ? "Informe a data final do período.\n\n" : string.Empty;

            if (!string.IsNullOrEmpty(inconsistencias))
            {
                MessageBox.Show(inconsistencias);

                return;
            }
            bool statusNotaSemRecebimento = _parametros.ParametrosFiscais.EmitirNotaSemReceber;

            List<Pessoa> listaPessoa = new List<Pessoa>();
            List<Pessoa> listaColaborador = new List<Pessoa>();


            for (int i = 0; i < lstVendedores.SelectedItems.Count; i++)
            {

                string textoPessoa = lstVendedores.SelectedItems[i].ToString();
                string[] codPessoa;

                codPessoa = textoPessoa.Split('-');

                Pessoa pessoa = lstVendedores.SelectedItem != null ? new Pessoa { Id = codPessoa[0].Trim().ToInt() } : null;

                listaPessoa.Add(pessoa);
            }

            EnumOrdenacaoPesquisaVwVendas ordenacao = (EnumOrdenacaoPesquisaVwVendas)pnlOrdenacao.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

           


            RelatorioCustoFinanceiro relatorioCustoFinanceiro = new RelatorioCustoFinanceiro(
                                                                                           listaPessoa,
                                                                                           listaColaborador,
                                                                                           dataInicial.Value,
                                                                                           dataFinal.Value, txtCusto.Text.ToDouble()
                                                                                       
                                                                                           );

            TratamentosDeTela.ExibirRelatorio(relatorioCustoFinanceiro);
            this.Cursor = Cursors.Default;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "



        private void PreenchalstVendedores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaVendedoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            foreach (var itens in listaObjetoValor)
            {
                int i = 0;
                lstVendedores.Items.Add(itens.Descricao);
                i++;

            }

            lstVendedores.Items.Add("0 - VENDAS SEM VENDEDOR");
        }

        private void PreenchaPrimeiroEUltimoDiaMes()
        {
            var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

            txtDataInicialEmissao.DateTime = primeiroDiaMes;
            txtDataFinalEmissao.DateTime = ultimoDiaMes;
        }






        //private void PreenchaCboVendedores()
        //{

        //    ServicoPessoa servicoPessoa = new ServicoPessoa();
        //    var lista = servicoPessoa.ConsulteListaVendedoresAtivos();

        //    List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

        //    lista.ForEach(pessoa =>
        //    {
        //        ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.DadosGerais.Razao, Valor = pessoa.Id };

        //        listaObjetoValor.Add(objetoDescricaoValor);
        //    });

        //    cboVendedores.Properties.DataSource = listaObjetoValor;
        //    cboVendedores.Properties.DisplayMember = "Descricao";
        //    cboVendedores.Properties.ValueMember = "Id";

        //    lstVendedores.Items.Add("0 - VENDAS SEM VENDEDOR");

        //    //if (string.IsNullOrEmpty(cboVendedores.Text))
        //    //{
        //    //    cboVendedores.EditValue = null;
        //    //}
        //}

        #endregion






        //private void FormRelatorioVendasPorVendedor_Load(object sender, EventArgs e)
        //{
        //    

        //}


    }

}
    
