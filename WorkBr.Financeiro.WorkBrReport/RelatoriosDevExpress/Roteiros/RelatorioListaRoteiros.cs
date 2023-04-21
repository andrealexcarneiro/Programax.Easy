using System;


using System.Collections.Generic;
//using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioListaRoteiros : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

      
        private string strEmpresa;
        private List<Roteiro> _listaDeRoteiros;
        private DateTime DataInicial;
        private DateTime DataFinal;
        private int _numeropedidos;
        private double _valortotal;
        public List<ItemRoteiro> ListaItensRoteiro { get; set; }
      
        private string DataRoteiro;
      
        #endregion

        #region " CONSTRUTOR "

        public RelatorioListaRoteiros(DateTime datainicial, DateTime datafinal )
        {
            InitializeComponent();

            CarregueParametros();
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();

            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            if (empresa.DadosEmpresa.NomeFantasia.Length < 8)
            {
                strEmpresa = empresa.DadosEmpresa.NomeFantasia.Substring(0, empresa.DadosEmpresa.NomeFantasia.Length);
            }
            else
            {
                strEmpresa = empresa.DadosEmpresa.NomeFantasia.Substring(0, 8);
            }

            //_numeroRoteiro = CodigoRoteiro;
            //strNome = nome;
            //DataRoteiro = data;

            DataInicial = datainicial;
              DataFinal = datafinal;

            _tituloRelatorio = "RELATÓRIO DE PEDIDOS";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {


            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            ServicoProduto servicoProduto = new ServicoProduto();
            ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

            _listaDeRoteiros = servicoRoteiro.ConsulteLista(null, null, null, EnumDataFiltrarRoteiro.ELABORACAO, DataInicial, DataFinal, 0);
            

            lblData.Text = DataRoteiro;

            RoteiroRelatorio roteiroRelatorios = new RoteiroRelatorio();

            roteiroRelatorios.Id = _listaDeRoteiros[0].Id;
            roteiroRelatorios.NomeCliente = _listaDeRoteiros[0].Usuario.DadosGerais.NomeFantasia;

            foreach (var itemrot in _listaDeRoteiros)
            {

                ItemRoteiro itemRoteiro = new ItemRoteiro();
                itemRoteiro.Funcionario = itemrot.PedidoVenda.Vendedor.ToString() ;
                itemRoteiro.Pedido = itemrot.PedidoVenda.Id.ToString();
                itemRoteiro.Cliente = itemrot.PedidoVenda.Cliente != null ? itemrot.PedidoVenda.Cliente.DadosGerais.Razao : string.Empty;

                itemRoteiro.Periodo = itemrot.Periodo.Descricao();

                itemRoteiro.Endereco = itemrot.PedidoVenda.EnderecoPedidoDeVenda != null ? "Rua: " +
                                           itemrot.PedidoVenda.EnderecoPedidoDeVenda.Rua + " - " + "N.: " +
                                           itemrot.PedidoVenda.EnderecoPedidoDeVenda.Numero + " - " + "Bairro: " +
                                           itemrot.PedidoVenda.EnderecoPedidoDeVenda.Bairro + " - " + "Cidade: " +
                                           itemrot.PedidoVenda.EnderecoPedidoDeVenda.Cidade.Descricao : string.Empty;

                itemRoteiro.DataElaboracao = itemrot.DataElaboracao.ToString("dd/MM/yyyy");
                itemRoteiro.DataConclusao = itemrot.DataConclusao != null ? itemrot.DataConclusao.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty;

                itemRoteiro.Status = itemrot.Status.Descricao();

                itemRoteiro.Observacao = itemrot.DetalheServico;
                itemRoteiro.LigarFone = itemrot.Observacao;
                itemRoteiro.ValorTotal = itemrot.PedidoVenda.ValorTotal.ToDouble();
                _valortotal += itemRoteiro.ValorTotal;

                roteiroRelatorios.ListaItens.Add(itemRoteiro);



                var listaVWItensPedido = servicoPedidoDeVenda.ConsulteJoinComItens(itemRoteiro.Pedido.ToInt());
                itemRoteiro.Funcionario = listaVWItensPedido.Usuario.DadosGerais.Razao.ToString();
                foreach (var itens in listaVWItensPedido.ListaItens)
                {

                   itensRel item = new itensRel();
                   
                    var listaProdutos = servicoProduto.ConsulteListasProdutosAtivos(itens.Produto.DadosGerais.Descricao,
                                                                      null, null, null,
                                                                       null, null);


                    if (listaProdutos.Count > 0)
                    {
                        item.Id = itens.Produto.Id;
                        item.Descricao = itens.Produto.DadosGerais.Descricao;
                        item.Quantidade = itens.Quantidade;
                        item.ValorTotalItem = itens.ValorTotal;

                        _numeropedidos += itens.Quantidade.ToInt();
                        //totalSomaGeral += item.ValorTotalItem;
                        //totalQuantidadeGeral += item.Quantidade;
                        //temPedido = true;

                        itemRoteiro.itensRel.Add(item);
                    }
                }

               

            }
            List<RoteiroRelatorio> roteirolista = new List<RoteiroRelatorio>();
            roteirolista.Add(roteiroRelatorios);

            lblQuantidade.Text = _numeropedidos.ToString();
            lbTotal.Text = _valortotal.ToString("0.00");
            ConteudoRelatorio.DataSource = roteirolista;
        }

        #endregion

        #region "Métodos Auxiliares"

        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

           // _parametros = servicoParametros.ConsulteParametros();
        }

        #endregion

        #region " CLASSES AUXILIARES "
        
        public class RoteiroRelatorio
        {
            public RoteiroRelatorio()
            {
                ListaItens = new List<ItemRoteiro>();

            }

            public int Id { get; set; }

            public string NomeCliente { get; set; }

            public List<ItemRoteiro> ListaItens { get; set; }
           
        }

       
        public class ItemRoteiro
        {
            public ItemRoteiro()
            {
                itensRel = new List<itensRel>();
            }
            public int Id { get; set; }

            public string DataElaboracao { get; set; }

            public string Funcionario { get; set; }

            public string Pedido { get; set; }

            public string Cliente { get; set; }

            public string Endereco { get; set; }

            public string DataConclusao { get; set; }

            public string Status { get; set; }

            public string Periodo { get; set; }

            public string LigarFone { get; set; }

            public string Observacao { get; set; }
            public double ValorTotal { get; set; }


            public List<itensRel> itensRel { get; set; }
        }


        public class itensRel
        {
            public int Id { get; set; }
            public string Descricao { get; set; }
            public double ComissaoItem { get; set; }
            public double ValorTotalItem { get; set; }
            public double Quantidade { get; set; }
        }



        #endregion
    }
}
