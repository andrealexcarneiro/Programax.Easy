using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FluentValidation;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormCadastroContasPagar : FormCadastroContasPagarReceber
    {
        public FormCadastroContasPagar()
        {
            InitializeComponent();

            _tipoOperacao = EnumTipoOperacaoContasPagarReceber.PAGAR;
        }

        protected override ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return new ServicoContasPagar();
        }
    }
}
