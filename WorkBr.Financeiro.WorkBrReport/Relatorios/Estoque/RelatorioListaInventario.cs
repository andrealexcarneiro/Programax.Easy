using System;


using System.Collections.Generic;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.InventarioServ;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using System.Linq;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioListaInventario : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<ItemInventario> _listaItemInventario;
        private int IdInventario;
      
        #endregion

        #region " CONSTRUTOR "

        public RelatorioListaInventario(int inventarioId)
        {
            InitializeComponent();

            CarregueParametros();
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            IdInventario = inventarioId;
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            _tituloRelatorio = "INVENTÁRIO";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {

            ServicoInventario servicoInventario = new ServicoInventario();
            servicoInventario = new ServicoInventario();

            var inventarioDaBase = servicoInventario.Consulte(IdInventario);


            EditeInventario(inventarioDaBase);
            InventarioRelatorio inventarioRelatorios = new InventarioRelatorio();
            foreach (var item in _listaItemInventario)
            {

                ItemInventarios itemInventario = new ItemInventarios();
                itemInventario.Id = item.Produto.Id;
                itemInventario.Descricao = item.Produto.DadosGerais.Descricao;
                if(item.Produto.Principal.Grupo != null)
                {
                    itemInventario.Grupo = item.Produto.Principal.Grupo.Descricao != null ? item.Produto.Principal.Grupo.Descricao : string.Empty;
                }
                if (item.Produto.Principal.Marca != null)
                {
                    itemInventario.Marca = item.Produto.Principal.Marca.Descricao;
                }
                    
                itemInventario.Unidade = item.Produto.DadosGerais.Unidade.Descricao;
                
                inventarioRelatorios.ListaItens.Add(itemInventario);

            }
                List<InventarioRelatorio> inventariolista = new List<InventarioRelatorio>();
                inventariolista.Add(inventarioRelatorios);

              
                ConteudoRelatorio.DataSource = inventariolista;
            lblQuantidade.Text = _listaItemInventario.Count.ToString();
        }
        private void EditeInventario(Inventario inventario)
        {
            if (inventario != null)
            {
                _listaItemInventario = inventario.ListaDeItens.ToList();
            }
        }
        #endregion

        #region "Métodos Auxiliares"

        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

        }

        #endregion

        #region " CLASSES AUXILIARES "
        
        public class InventarioRelatorio
        {
            public InventarioRelatorio()
            {
                ListaItens = new List<ItemInventarios>();

            }

            //public int Id { get; set; }

            //public string NomeCliente { get; set; }

            public List<ItemInventarios> ListaItens { get; set; }
           
        }

       
        public class ItemInventarios
        {
            //public ItemInventarios()
            //{
            //    itensRel = new List<itensRel>();
            //}
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string Marca { get; set; }

            public string Unidade { get; set; }

            public string Grupo { get; set; }

          


            //public List<itensRel> itensRel { get; set; }
        }


        //public class itensRel
        //{
        //    public int Id { get; set; }
        //    public string Descricao { get; set; }
        //    public double ComissaoItem { get; set; }
        //    public double ValorTotalItem { get; set; }
        //    public double Quantidade { get; set; }
        //}



        #endregion
    }
}
