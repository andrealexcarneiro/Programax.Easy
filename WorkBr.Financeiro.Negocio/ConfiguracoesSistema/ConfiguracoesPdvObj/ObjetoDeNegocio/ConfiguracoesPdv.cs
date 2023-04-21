using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.ObjetoDeNegocio
{
    public class ConfiguracoesPdv : ObjetoDeNegocioBase
    {
        public virtual EnumTipoCartaoCreditoEDebito? TipoCartaoCreditoDebito { get; set; }

        public virtual bool GereVendaAPartirDoPdv { get; set; }

        public virtual double DescontoMaximoCaixa { get; set; }

        public virtual bool FormaPagamentoEntradaIgualPedidoVenda { get; set; }

        public virtual bool EmitirNotaFiscalDiretamenteNoPDV { get; set; }

        public virtual bool PermitirFormaECondicaoPagamentoNoPDV { get; set; }

        public virtual Pessoa Cliente { get; set; }

        public virtual Pessoa ClienteTemporario { get; set; }
    }
}
