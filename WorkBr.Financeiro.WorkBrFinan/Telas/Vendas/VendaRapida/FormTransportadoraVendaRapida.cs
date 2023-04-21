﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Vendas.VendaRapida
{
    public partial class FormTransportadoraVendaRapida : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private DialogResult _resultadoOperacao;

        #endregion

        #region " PROPRIEDADES "

        public Pessoa Transportadora { get; set; }

        public EnumTipoFrete TipoFrete { get; set; }

        public DateTime? DataPrevisaoEntrega { get; set; }

        public double? ValorFrete { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public FormTransportadoraVendaRapida()
        {
            InitializeComponent();

            PreenchaCboTransportadoras();
            PreenchaCboTipoFrete();

            this.ActiveControl = cboTransportadoras;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult EditeTransportadora(Pessoa transportadora, EnumTipoFrete tipoFrete, DateTime? dataPrevisaoEntrega, double? valorFrete)
        {
            Transportadora = transportadora;
            TipoFrete = tipoFrete;
            DataPrevisaoEntrega = dataPrevisaoEntrega;
            ValorFrete = valorFrete;

            cboTransportadoras.EditValue = transportadora != null ? (int?)transportadora.Id : null;
            cboTipoFrete.EditValue = tipoFrete;

            if (dataPrevisaoEntrega != null)
            {
                txtDataPrevisaoEntrega.DateTime = dataPrevisaoEntrega.GetValueOrDefault();
            }
            else
            {
                txtDataPrevisaoEntrega.Text = string.Empty;
            }

            txtValorFrete.Text = valorFrete != null ? valorFrete.Value.ToString("0.00") : string.Empty;

            this.AbrirTelaModal();

            return _resultadoOperacao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            _resultadoOperacao = DialogResult.Cancel;
            this.Close();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Transportadora = cboTransportadoras.EditValue != null ? new Pessoa { Id = cboTransportadoras.EditValue.ToInt() } : null;
            TipoFrete = (EnumTipoFrete)cboTipoFrete.EditValue;
            DataPrevisaoEntrega = txtDataPrevisaoEntrega.Text.ToDateNullabel();
            ValorFrete = txtValorFrete.Text.ToDoubleNullabel();

            _resultadoOperacao = DialogResult.OK;

            this.Close();
        }

        private void cboTipoFrete_EditValueChanged(object sender, EventArgs e)
        {
            if ((EnumTipoFrete)cboTipoFrete.EditValue == EnumTipoFrete.PORCONTADODESTINATARIOREMETENTE)
            {
                txtValorFrete.Enabled = true;
            }
            else
            {
                txtValorFrete.Text = string.Empty;
                txtValorFrete.Enabled = false;
            }
        }

        #endregion

        #region " MÉTODOS PRIVADOS "

        private void PreenchaCboTransportadoras()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaTransportadorasAtivas();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboTransportadoras.Properties.DisplayMember = "Descricao";
            cboTransportadoras.Properties.ValueMember = "Valor";
            cboTransportadoras.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboTransportadoras.Text))
            {
                cboTransportadoras.EditValue = null;
            }
        }

        private void PreenchaCboTipoFrete()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoFrete>();

            cboTipoFrete.Properties.DataSource = lista;
            cboTipoFrete.Properties.DisplayMember = "Descricao";
            cboTipoFrete.Properties.ValueMember = "Valor";
        }

        #endregion
    }
}
