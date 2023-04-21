﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormProrrogacaoContaReceber : FormProrrogacaoContaPagarReceber
    {
        public FormProrrogacaoContaReceber()
        {
            InitializeComponent();
        }

        protected override ServicoContasPagarReceber RetorneServicoContaPagarOuReceber()
        {
            return new ServicoContasReceber();
        }
    }
}
