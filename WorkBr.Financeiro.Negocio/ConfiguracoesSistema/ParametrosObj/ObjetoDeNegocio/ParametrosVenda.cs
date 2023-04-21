using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using System;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio
{
    [Serializable]
    public class ParametrosVenda
    {
        public virtual bool PermiteAlterarIndicador { get; set; }

        public virtual bool PermiteAlterarVendedor { get; set; }

        public virtual bool PermiteAlterarAtendente { get; set; }

        public virtual bool PermiteAlterarSupervisor { get; set; }

        public virtual bool PermiteAlterarValorUnitario { get; set; }

        public virtual bool PermiteDescontoNoTotalVenda { get; set; }

        public virtual bool PermiteAlterarValorUnitarioVendaRapida { get; set; }

        public virtual bool PermiteBaixarEstoqueNaSaida { get; set; }

        public virtual bool PermiteMostrarValorVenda { get; set; }

        public virtual bool NaoAceitarEstoqueNegativo { get; set; }

        public virtual bool ReserveEstoqueAoFaturarPedido { get; set; }

        public virtual bool TrabalharComEstoqueReservado { get; set; }

        public virtual bool PedidoEmImpressoraTermica { get; set; }

        public virtual bool PedidoEmDuasVias { get; set; }

        public virtual bool PedidosPorVendedor { get; set; }

        public virtual bool ExibirTodasAsTabelasPrecoPedidoVenda { get; set; }

        public virtual bool ExibirTodasAsTabelasPrecoVendaRapida { get; set; }
        public virtual bool ExibirStatusFaturado { get; set; }
        public virtual bool ExibirTelefonePedido { get; set; }
        public virtual bool ExibirInfoPedido { get; set; }

        public virtual bool Refiltek { get; set; }
        public virtual bool BaixarFaturamento { get; set; }
        public virtual bool StatusFaturado { get; set; }

        public virtual bool AproveitarEnderecoEmpresaParaCadastroRapidoCliente { get; set; }

        public virtual TabelaPreco TabelaPreco { get; set; }

        public virtual Pessoa Atendente { get; set; }

        public virtual Pessoa Vendedor { get; set; }

        public virtual Pessoa Transportadora { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual CondicaoPagamento CondicaoPagamento { get; set; }
        


        public virtual EnumTipoFrete TipoFrete { get; set; }

        public virtual string ObservacoesVendaRapida { get; set; }
        
        public virtual string NomeContrato { get; set; }

        public virtual string TermosContrato { get; set; }

        public virtual int LimiteDiarioManha { get; set; }

        public virtual int LimiteDiarioTarde { get; set; }
    }
}
