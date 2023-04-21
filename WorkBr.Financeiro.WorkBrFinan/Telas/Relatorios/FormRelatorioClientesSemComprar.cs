using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Servico.Cadastros.CidadeServ;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioClientesSemComprar : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private ServicoPessoa _servicoPessoa;
        private ServicoCidade _servicoCidade;

        #endregion

        #region " CONSTRUTOR "

        public FormRelatorioClientesSemComprar()
        {
            InitializeComponent();

            _servicoPessoa = new ServicoPessoa();

            PreenchaCboAtendente();
            PreenchaCboVendedores();
            PreenchaCboDiasSemComprar();
            PreenchaCboEstados();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            int? atendenteId = cboAtendentes.EditValue.ToIntNullabel();
            int? vendedorId = cboVendedores.EditValue.ToIntNullabel();

            int? diasSemComprarInicial = null;
            int? diasSemComprarFinal = null;

            int? cidadeId = cboCidade.EditValue.ToIntNullabel();
            string uf = cboEstado.EditValue.ToStringNull();
            string bairro = txtBairro.Text.ToStringEmpty();

            if (cboDiasSemComprar.EditValue != null)
            {
                EnumDiasSemComprar diasSemComprar = (EnumDiasSemComprar)cboDiasSemComprar.EditValue;

                if (diasSemComprar == EnumDiasSemComprar.ATE30DIAS)
                {
                    diasSemComprarFinal = 30;
                }
                else if (diasSemComprar == EnumDiasSemComprar.DE30A60DIAS)
                {
                    diasSemComprarInicial = 30;
                    diasSemComprarFinal = 60;
                }
                else if (diasSemComprar == EnumDiasSemComprar.DE60A90DIAS)
                {
                    diasSemComprarInicial = 60;
                    diasSemComprarFinal = 90;
                }
                else if (diasSemComprar == EnumDiasSemComprar.MAISDE90DIAS)
                {
                    diasSemComprarInicial = 90;
                }
            }

            RelatorioClientesSemComprar relatorioClientesSemComprar = new RelatorioClientesSemComprar(atendenteId,
                                                                                                                                                            vendedorId,
                                                                                                                                                            diasSemComprarInicial,
                                                                                                                                                            diasSemComprarFinal,
                                                                                                                                                            chkSomenteClientesQueJahCompraram.Checked,
                                                                                                                                                            uf,
                                                                                                                                                            cidadeId,
                                                                                                                                                            bairro);

            TratamentosDeTela.ExibirRelatorio(relatorioClientesSemComprar);

            this.Cursor = Cursors.Default;
        }

        private void cboEstado_EditValueChanged(object sender, EventArgs e)
        {
            if (_servicoCidade == null)
            {
                _servicoCidade = new ServicoCidade();
            }

            string uf = cboEstado.EditValue == null ? string.Empty : cboEstado.EditValue.ToString();

            var listaDeCidades = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(uf);

            listaDeCidades.Insert(0, null);

            cboCidade.Properties.DataSource = listaDeCidades;
            cboCidade.Properties.DisplayMember = "Descricao";
            cboCidade.Properties.ValueMember = "Id";

            cboCidade.EditValue = null;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboAtendente()
        {
            var lista = _servicoPessoa.ConsulteListaAtendentesAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboAtendentes.Properties.DisplayMember = "Descricao";
            cboAtendentes.Properties.ValueMember = "Valor";
            cboAtendentes.Properties.DataSource = listaObjetoValor;
        }

        private void PreenchaCboVendedores()
        {
            var lista = _servicoPessoa.ConsulteListaVendedoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboVendedores.Properties.DisplayMember = "Descricao";
            cboVendedores.Properties.ValueMember = "Valor";
            cboVendedores.Properties.DataSource = listaObjetoValor;
        }

        private void PreenchaCboDiasSemComprar()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumDiasSemComprar>();

            cboDiasSemComprar.Properties.DisplayMember = "Descricao";
            cboDiasSemComprar.Properties.ValueMember = "Valor";
            cboDiasSemComprar.Properties.DataSource = lista;
        }

        private void PreenchaCboEstados()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var listaDeEstados = servicoEstado.ConsulteListaEstados();

            listaDeEstados.Insert(0, null);

            cboEstado.Properties.DataSource = listaDeEstados;
            cboEstado.Properties.DisplayMember = "Nome";
            cboEstado.Properties.ValueMember = "UF";
        }

        #endregion

        #region " ENUMERADORES "

        private enum EnumDiasSemComprar
        {
            [Description("ATÉ 30 DIAS")]
            ATE30DIAS,

            [Description("DE 30 A 60 DIAS")]
            DE30A60DIAS,

            [Description("DE 60 A 90 DIAS")]
            DE60A90DIAS,

            [Description("MAIS DE 90 DIAS")]
            MAISDE90DIAS
        }

        #endregion
    }
}
