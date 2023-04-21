using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.RamoAtividadeServ;
using Programax.Easy.Report.RelatoriosDevExpress.Cadastros;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioParceiro : FormularioBase
    {
        #region " CONSTRUTOR "

        public FormRelatorioParceiro()
        {
            InitializeComponent();

            PreenchaOsEstados();
            PreenchaCboRamoAtividade();

            if (chkEhCliente.Enabled)
                pnlConsumidorRevenda.Enabled = true;
            
            this.ActiveControl = rdbOrdemListaCodigo;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnPesquisaIndicador_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var indicador = formPessoaPesquisa.PesquisePessoaIndicadora();

            if (indicador != null)
            {
                PreenchaIndicador(indicador);
            }
        }

        private void btnPesquisaAtendente_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var atendente = formPessoaPesquisa.PesquisePessoaAtendente();

            if (atendente != null)
            {
                PreenchaAtendente(atendente);
            }
        }

        private void btnPesquisaVendedor_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();
            var vendedor = formPessoaPesquisa.PesquisePessoaVendedora();

            if (vendedor != null)
            {
                PreenchaVendedor(vendedor);
            }
        }

        private void txtIdVendedor_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdVendedor.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var vendedor = servicoPessoa.ConsulteVendedorAtivo(txtIdVendedor.Text.ToInt());

                PreenchaVendedor(vendedor, true);
            }
            else
            {
                PreenchaVendedor(null);
            }
        }

        private void txtIdAtendente_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdAtendente.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var atendente = servicoPessoa.ConsulteAtendenteAtivo(txtIdAtendente.Text.ToInt());

                PreenchaAtendente(atendente, true);
            }
            else
            {
                PreenchaAtendente(null);
            }
        }

        private void txtIdIndicador_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdIndicador.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();

                var indicador = servicoPessoa.ConsulteIndicadorAtivo(txtIdIndicador.Text.ToInt());

                PreenchaIndicador(indicador, true);
            }
            else
            {
                PreenchaIndicador(null);
            }
        }

        private void cboEstadoEndereco_EditValueChanged(object sender, EventArgs e)
        {
            ServicoCidade servicoCidade = new ServicoCidade();

            string uf = cboEstadoEndereco.EditValue == null ? string.Empty : cboEstadoEndereco.EditValue.ToString();

            var listaDeCidades = servicoCidade.ConsulteListaCidadesAtivasPorEstado(uf);

            listaDeCidades.Insert(0, null);

            cboCidadeEndereco.Properties.DataSource = listaDeCidades;
            cboCidadeEndereco.Properties.DisplayMember = "Descricao";
            cboCidadeEndereco.Properties.ValueMember = "Id";

            cboCidadeEndereco.EditValue = null;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            bool ehCliente = chkEhCliente.Checked;
            bool ehFornecedor = chkEhFornecedor.Checked;
            bool ehFuncionario = chkEhFuncionario.Checked;
            bool ehTransportadora = chkEhTransportadora.Checked;
            EnumTipoCliente tipoCliente = EnumTipoCliente.TODOS;

            this.Cursor = Cursors.WaitCursor;

            if (ehCliente)
            {
                if (!rdbtodos.Checked)
                    tipoCliente = rdbConsumidorFinal.Checked ? EnumTipoCliente.CONSUMIDOR : EnumTipoCliente.REVENDA;
                else
                    tipoCliente = EnumTipoCliente.TODOS;
             }


            Estado estado = cboEstadoEndereco.EditValue != null ? new Estado { UF = cboEstadoEndereco.EditValue.ToString() } : null;
            Cidade cidade = cboCidadeEndereco.EditValue != null ? new Cidade { Id = cboCidadeEndereco.EditValue.ToInt() } : null;

            Pessoa vendedor = !txtIdVendedor.Text.EstahVazioOuZerado() ? new Pessoa { Id = txtIdVendedor.Text.ToInt() } : null;
            Pessoa atendente = !txtIdAtendente.Text.EstahVazioOuZerado() ? new Pessoa { Id = txtIdAtendente.Text.ToInt() } : null;
            Pessoa indicador = !txtIdIndicador.Text.EstahVazioOuZerado() ? new Pessoa { Id = txtIdIndicador.Text.ToInt() } : null;

            RamoAtividade ramoAtividade = cboRamoAtividade.EditValue != null ? new RamoAtividade { Id = cboRamoAtividade.EditValue.ToInt() } : null;

            EnumOrdenacaoPesquisaPessoa ordenacaoPesquisa = (EnumOrdenacaoPesquisaPessoa)pnlOrdenacao.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            RelatorioParceiro relatorioParceiro = new RelatorioParceiro(ehCliente,
                                                                                                 ehFornecedor,
                                                                                                 ehFuncionario,
                                                                                                 ehTransportadora,
                                                                                                 estado,
                                                                                                 cidade,
                                                                                                 vendedor,
                                                                                                 atendente,
                                                                                                 indicador,
                                                                                                 ramoAtividade,
                                                                                                 txtMesAnoAniversario.Text,
                                                                                                 ordenacaoPesquisa,
                                                                                                 tipoCliente);

            TratamentosDeTela.ExibirRelatorio(relatorioParceiro);

            this.Cursor = Cursors.Default;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbOrdemListaCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkEhCliente.Focus();
            }
        }

        private void chkEhCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEhCliente.Checked == false)
                pnlConsumidorRevenda.Enabled = false;
            else
                pnlConsumidorRevenda.Enabled = true;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaOsEstados()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var listaDeEstados = servicoEstado.ConsulteListaEstados();

            listaDeEstados.Insert(0, null);

            cboEstadoEndereco.Properties.DataSource = listaDeEstados;
            cboEstadoEndereco.Properties.DisplayMember = "Nome";
            cboEstadoEndereco.Properties.ValueMember = "UF";
        }

        private void PreenchaCboRamoAtividade()
        {
            ServicoRamoAtividade servicoRamoAtividade = new ServicoRamoAtividade();

            var listaRamosAtividades = servicoRamoAtividade.ConsulteListaAtiva();

            listaRamosAtividades.Insert(0, null);

            cboRamoAtividade.Properties.DataSource = listaRamosAtividades;
            cboRamoAtividade.Properties.ValueMember = "Id";
            cboRamoAtividade.Properties.DisplayMember = "Descricao";

            if (string.IsNullOrEmpty(cboRamoAtividade.Text))
            {
                cboRamoAtividade.EditValue = null;
            }
        }

        private void PreenchaIndicador(Pessoa indicador, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (indicador != null)
            {
                txtIdIndicador.Text = indicador.Id.ToString();
                txtNomeIndicador.Text = indicador.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Indicador nao encontrado!", "Indicador não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdIndicador.Focus();
                }

                txtIdIndicador.Text = string.Empty;
                txtNomeIndicador.Text = string.Empty;
            }
        }

        private void PreenchaAtendente(Pessoa atendente, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (atendente != null)
            {
                txtIdAtendente.Text = atendente.Id.ToString();
                txtNomeAtendente.Text = atendente.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Atendente nao encontrado!", "Atendente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdAtendente.Focus();
                }

                txtIdAtendente.Text = string.Empty;
                txtNomeAtendente.Text = string.Empty;
            }
        }

        private void PreenchaVendedor(Pessoa vendedor, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (vendedor != null)
            {
                txtIdVendedor.Text = vendedor.Id.ToString();
                txtNomeVendedor.Text = vendedor.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Vendedor nao encontrado!", "Vendedor não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdVendedor.Focus();
                }

                txtIdVendedor.Text = string.Empty;
                txtNomeVendedor.Text = string.Empty;
            }
        }

        #endregion
        
    }
}
