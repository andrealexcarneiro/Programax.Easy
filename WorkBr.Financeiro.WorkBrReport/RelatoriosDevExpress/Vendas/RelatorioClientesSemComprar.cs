using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using System.Linq;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioClientesSemComprar : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private int? _atendenteId;
        private int? _vendedorId;
        private int? _diasInicialSemComprar;
        private int? _diasFimSemComprar;
        private bool _somenteClientesQueJahCompraram;
        private string _uf;
        private int? _cidadeId;
        private string _bairro;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioClientesSemComprar(int? atendenteId,
                                                               int? vendedorId,
                                                               int? diasInicialSemComprar,
                                                               int? diasFimSemComprar,
                                                               bool somenteClientesQueJahCompraram,
                                                               string uf,
                                                               int? cidadeId,
                                                               string bairro)
        {
            InitializeComponent();

            _atendenteId = atendenteId;
            _vendedorId = vendedorId;
            _diasInicialSemComprar = diasInicialSemComprar;
            _diasFimSemComprar = diasFimSemComprar;
            _somenteClientesQueJahCompraram = somenteClientesQueJahCompraram;
            _uf = uf;
            _cidadeId = cidadeId;
            _bairro = bairro;

            _tituloRelatorio = "RELATÓRIO DE CLIENTES - SEM COMPRAR";
        }

        #endregion

        #region " MÉTODO SOBRESCRITO "

        protected override void CarregueDadosRelatorio()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();

            List<VWClienteSemComprar> listaClientesSemComprar = servicoPessoa.ConsulteListaVWClientesSemComprar(_atendenteId,
                                                                                                                                                                                 _vendedorId,
                                                                                                                                                                                 _diasInicialSemComprar,
                                                                                                                                                                                 _diasFimSemComprar,
                                                                                                                                                                                 _somenteClientesQueJahCompraram,
                                                                                                                                                                                 _uf,
                                                                                                                                                                                 _cidadeId,
                                                                                                                                                                                 _bairro);

            listaClientesSemComprar = listaClientesSemComprar.ToList().OrderBy(x => x.Nome).ToList();

            List<ClienteSemComprarRelatorio> listaClientesSemComprarRelatorio = new List<ClienteSemComprarRelatorio>();

            foreach (var cliente in listaClientesSemComprar)
            {
                ClienteSemComprarRelatorio clienteSemComprarRelatorio = new ClienteSemComprarRelatorio();

                clienteSemComprarRelatorio.Atendente = cliente.Atendente;
                clienteSemComprarRelatorio.Celular = cliente.Celular;
                clienteSemComprarRelatorio.CpfCnpj = cliente.CpfCnpj;
                clienteSemComprarRelatorio.ClienteId = cliente.Id;
                clienteSemComprarRelatorio.DataUltimoPedido = cliente.DataUltimoPedido != null ? cliente.DataUltimoPedido.Value.ToString("dd/MM/yyyy") : string.Empty;
                clienteSemComprarRelatorio.DiasSemComprar = cliente.JahComprou ? cliente.DiasSemComprar + " dias" : string.Empty;
                clienteSemComprarRelatorio.Nome = cliente.Nome;
                clienteSemComprarRelatorio.Telefone = cliente.Telefone;
                clienteSemComprarRelatorio.ValorUltimoPedido = cliente.ValorUltimoPedido != null ? cliente.ValorUltimoPedido.Value.ToString("#,###,##0.00") : string.Empty;
                clienteSemComprarRelatorio.Vendedor = cliente.Vendedor;

                listaClientesSemComprarRelatorio.Add(clienteSemComprarRelatorio);
            }

            ConteudoRelatorio.DataSource = listaClientesSemComprarRelatorio;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class ClienteSemComprarRelatorio
        {
            public int ClienteId { get; set; }

            public string CpfCnpj { get; set; }

            public string Nome { get; set; }

            public string Celular { get; set; }

            public string Telefone { get; set; }

            public string Atendente { get; set; }

            public string Vendedor { get; set; }

            public string ValorUltimoPedido { get; set; }

            public string DataUltimoPedido { get; set; }

            public string DiasSemComprar { get; set; }
        }

        #endregion
    }
}
