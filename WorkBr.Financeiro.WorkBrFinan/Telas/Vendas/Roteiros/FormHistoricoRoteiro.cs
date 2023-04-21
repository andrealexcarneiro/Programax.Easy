using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Programax.Easy.View.Telas.Vendas.Roteiros
{
    public partial class FormHistoricoRoteiro : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "
        
        List<HistoricoRoteiro> _listaHistorico;
        Pessoa _usuario;
        Roteirizacao _roteirizacao;

        #endregion

        #region " CONSTRUTOR "

        public FormHistoricoRoteiro(Roteirizacao roteiro, Pessoa usuario)
        {
            InitializeComponent();

            txtIdRoteiro.Text = roteiro.Id.ToString();
            _roteirizacao = roteiro;
            
            txtUsuario.Text = usuario.Id + " - " + usuario.DadosGerais.Razao;
            _usuario = usuario;

            _listaHistorico = new List<HistoricoRoteiro>();

            PesquiseListaHistorico();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHistorico.Text))
            {  
                MessageBox.Show("Para salvar, é necessário informar o histórico.", 
                                "Histórico de Roteiro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
                return;
            }
            
            Action actionSalvar = () =>
            {
                HistoricoRoteiro historico = new HistoricoRoteiro();

                historico.Roteirizacao = _roteirizacao;
                historico.Usuario = _usuario;
                historico.DescricaoHistorico = txtHistorico.Text;
                historico.DataHistorico = DateTime.Now;

                new ServicoHistoricoRoteiro().Cadastre(historico);

                PesquiseListaHistorico();
                txtHistorico.Text = string.Empty;

                //Altera o status do Roteiro
                ServicoRoteirizacao servicoRoteirizacao = new ServicoRoteirizacao();

                var roteirizacao = servicoRoteirizacao.Consulte(_roteirizacao.Id);

                if(roteirizacao != null)
                {
                    roteirizacao.Status = EnumStatusRoteiro.INCONCLUSO;

                    servicoRoteirizacao.Atualize(roteirizacao);

                    var roteiro = new ServicoRoteiro().ConsulteListaPorRoteirizacao(roteirizacao.Id);

                    foreach (var item in roteiro)
                    {
                        ServicoRoteiro servicoRoteiro = new ServicoRoteiro();
                        
                        var itemRoteiro = servicoRoteiro.Consulte(item.Id);

                        itemRoteiro.Status = EnumStatusRoteiro.INCONCLUSO;

                        servicoRoteiro.Atualize(itemRoteiro);

                        ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                        
                        var pedido = servicoPedido.Consulte(item.PedidoVenda.Id);
                                               
                        pedido.StatusRoteiro = EnumStatusRoteiro.INCONCLUSO;

                        servicoPedido.Atualize(pedido);
                    }
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, this, fecharFormAoConcluirOperacao: true);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "
       
        private void PesquiseListaHistorico()
        {
            _listaHistorico = new ServicoHistoricoRoteiro().ConsulteLista(_roteirizacao.Id);

            PreenchaGrid();
        }

        private void PreenchaGrid()
        {
            List<HistoricoRoteiroGrid> listaHistorico = new List<HistoricoRoteiroGrid>();

            foreach (var item in _listaHistorico)
            {
                HistoricoRoteiroGrid historicoGrid = new HistoricoRoteiroGrid();

                historicoGrid.Id = item.Id;
                historicoGrid.Usuario = item.Usuario.Id + " - " + item.Usuario.DadosGerais.Razao;
                historicoGrid.DescricaoHistorico = item.DescricaoHistorico;
                historicoGrid.DataHistorico = item.DataHistorico.ToString("dd/MM/yyyy");

                listaHistorico.Add(historicoGrid);
            }

            gcHistoricoRoteiro.DataSource = listaHistorico;
            gcHistoricoRoteiro.RefreshDataSource();
        }
        
        #endregion

        #region " CLASSES AUXILIARES "

        private class HistoricoRoteiroGrid
        {
            public int Id { get; set; }

            public virtual string Usuario { get; set; }

            public virtual string DescricaoHistorico { get; set; }

            public virtual string DataHistorico { get; set; }
        }

        #endregion
                
    }
}
