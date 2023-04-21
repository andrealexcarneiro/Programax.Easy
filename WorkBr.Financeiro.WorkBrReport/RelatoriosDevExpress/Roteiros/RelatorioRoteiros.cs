using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioRoteiros : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _numeroRoteiro;
        private Parametros _parametros;
        private string strEmpresa;
        private List<Roteiro> _listaDeRoteiros;
        private int _numeropedidos;
        public List<ItemRoteiro> ListaItensRoteiro { get; set; }
        private string strNome;
        private Marca _marca;
        private Fabricante _fabricante;
        private SubGrupo _subgrupo;
        private Categoria _categoria;
        private Grupo _grupo;
        private string DataRoteiro;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioRoteiros(int CodigoRoteiro,string nome, string data)
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

            _numeroRoteiro = CodigoRoteiro;
            strNome = nome;
            DataRoteiro = data;

            _tituloRelatorio = "ROTEIROS";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            double totalComissao = 0;
            double totalComissaoRecebido = 0;
            double totalSomaGeral = 0;
            double totalQuantidadeGeral = 0;
            double ValorComissaoPedido = 0;
            bool temPedido;
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            ServicoProduto servicoProduto = new ServicoProduto();
            ServicoRoteiro servicoRoteiro = new ServicoRoteiro();

     
            _listaDeRoteiros = new ServicoRoteiro().ConsulteListaPorRoteirizacao(_numeroRoteiro);


            lblnomefuncionario.Text = strNome;

            lblCodigoRoteiro.Text = _numeroRoteiro.ToString();
            lblData.Text = DataRoteiro;

            RoteiroRelatorio roteiroRelatorios = new RoteiroRelatorio();
            

            roteiroRelatorios.Id = _listaDeRoteiros[0].Id;
            roteiroRelatorios.NomeCliente = _listaDeRoteiros[0].Usuario.DadosGerais.NomeFantasia;

            foreach (var itemrot in _listaDeRoteiros)
            {

                ItemRoteiro itemRoteiro = new ItemRoteiro();
                itemRoteiro.Funcionario = itemrot.PessoaFuncionario != null ? itemrot.PessoaFuncionario.Id + " - " + itemrot.PessoaFuncionario.DadosGerais.Razao : string.Empty;
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
               
                roteiroRelatorios.ListaItens.Add(itemRoteiro);

                //ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
                var listaVWItensPedido = servicoPedidoDeVenda.ConsulteJoinComItens(itemRoteiro.Pedido.ToInt());

                foreach (var itens in listaVWItensPedido.ListaItens)
                {
                    itensRel item = new itensRel();

                  

                    var listaProdutos = servicoProduto.ConsulteListasProdutosAtivos(itens.Produto.DadosGerais.Descricao,
                                                                      _marca, _fabricante, _categoria,
                                                                       _grupo, _subgrupo);


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
            ConteudoRelatorio.DataSource = roteirolista;
        }

        #endregion

        #region "Métodos Auxiliares"

        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

            _parametros = servicoParametros.ConsulteParametros();
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
