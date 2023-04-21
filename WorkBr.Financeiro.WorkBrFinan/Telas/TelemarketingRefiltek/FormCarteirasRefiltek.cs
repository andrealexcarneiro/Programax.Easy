using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Telemarketing.TmkServ;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;

using System.Drawing;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using System.Data;
using MySql.Data.MySqlClient;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.TeleMarketing
{
    public partial class FormCarteirasRefiltek : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Tmk> _listaTmk;
        private List<TmkGrid> _listaTmkGrid;
        private PedidoDeVenda _pedidoDeVendaSelecionado;
        private string ConectionString;
        private DataSet mDataSet;
        private MySqlDataAdapter mAdapter;
        private int UltimoID = 1;
        private bool Novo = false;
        private int numCarteira = 0;
        private Parametros _parametros;
        private string ConectionStringII;
        #endregion

        #region " CONSTRUTOR "

        public FormCarteirasRefiltek(bool somenteImpressao = false)
        {
            InitializeComponent();

            _listaTmk = new List<Tmk>();
            btnExcluir.Visible = false;
            PreenchaCboBairro();
            PreenchaCboMarcas();
            PreenchaCboCategorias();
            PreenchaCboIndicadores();
            PreenchaCboVendedores();
            PreenchaCboCarteiras();
            PreenchaCboCNPJ();
            btnAtender.Visible = true;
            CarregueParametros();
            if (somenteImpressao)
            {
                this.NomeDaTela = "Busca Atendimentos Refiltek";
                btnAtender.Visible = false;
            }

            txtDataInicial.DateTime = DateTime.Now.Date;
            txtDataFinal.DateTime = DateTime.Now.Date;

            this.ActiveControl = txtDataInicial;
        }

        #endregion

        #region " EVENTOS CONTROLES "
        
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            btnExcluir.Visible = false;
            btnAtender.Visible = true;
          
                Novo = false;
          
            Pesquise();
        }
        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

            _parametros = servicoParametros.ConsulteParametros();

            //txtObservacoesGeraisVenda.Text = _parametros.ParametrosVenda.ObservacoesVendaRapida;
        }
        private void PreenchaCboVendedores()
        {


            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaVendedoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor {  Descricao = pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                
                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboVendedores.Properties.DisplayMember = "Descricao";
            cboVendedores.Properties.ValueMember = "Valor";
            cboVendedores.Properties.DataSource = listaObjetoValor;


           
        }


        private void PreenchaCboIndicadores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaIndicadoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboIndicadores.Properties.DisplayMember = "Descricao";
            cboIndicadores.Properties.ValueMember = "Valor";
            cboIndicadores.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboIndicadores.Text))
            {
                cboIndicadores.EditValue = null;
            }
        }
        private void carregaconexaoII()
        {
            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoesII.json");

            ConexoesJsonII conexoesII = JsonConvert.DeserializeObject<ConexoesJsonII>(conexoesStringII);

            var item = conexoesII.ConexoesII[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionStringII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexaoII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }

            }

        }

        private void cboSituacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }
        
        private void btnSelecionar_Click(object sender, EventArgs e)
        {

            if (txtNomeCarteira.Text == string.Empty)
            {
                MessageBox.Show("Para Gravar a Carteira. Você precisa informar a Descrição da Carteira!", "Conclusão da Carteira!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            if (cboVendedores.EditValue.ToInt() == 0)
            {
                MessageBox.Show("Para Gravar a Carteira. Você precisa informar o Vendedor!", "Conclusão da Carteira!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            carregaconexaoII();         


            string NomeCarteira = txtNomeCarteira.Text;
            var dtinicial = "1900-01-01 00:00:00";
            var dtfinal = "1900-01-01 00:00:00";
            DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();

            if (dataInicial != null)
            {
                
                dtinicial = DateTime.Parse(dataInicial.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();

            if (dataFinal != null)
            {
               
                dtfinal = DateTime.Parse(dataFinal.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
           


            string Bairro = string.Empty;
            int marcaId = cboMarcas.EditValue.ToInt();
            if (cboBairro.EditValue.ToInt() != 0)
            {
                 Bairro = cboBairro.EditValue.ToString();
            }
            
            
            int CPF = 0;
            int CNPJ = 0;
            int categoriaId = cboCategorias.EditValue.ToInt();
            int grupoId = cboGrupos.EditValue.ToInt();
            int subgrupoId = cboSubGrupos.EditValue.ToInt();
            int vendedorId = cboVendedores.EditValue.ToInt();
            int indicadorId = cboIndicadores.EditValue.ToInt();
            
            if (cbocpfcnpj.EditValue.ToInt() == 1)
            {
                CPF = 1;
            }
            if (cbocpfcnpj.EditValue.ToInt() == 2)
            {
                CNPJ = 1;
            }

            if (cbocpfcnpj.EditValue.ToInt() == 3)
            {
                CPF = 1;
                CNPJ = 1;
            }

            {
                using (var conn = new MySqlConnection(ConectionStringII))

                {
                    conn.Open();

                  if (Novo == false)
                    {
                        MySqlCommand command = new MySqlCommand("INSERT INTO carteiras (NomeCarteira,DataInicial,DataFinal,Marca,DataNascimento, " +
                                                                                 "Bairro, Idade, CPF, Categoria, Grupo, SubGrupo, CNPJ, Indicador,Vendedor)" +
                                         "VALUES('" + NomeCarteira + "', '" + dtinicial + "', '" + dtfinal + "', " + marcaId + ", '" + "1900-01-01" + "'," +
                                                "'" + Bairro + "', '" + 0 + "', " + CPF + ", " + categoriaId + ", " + grupoId + ", " + subgrupoId + "," +
                                                 CNPJ + "," + indicadorId + "," + vendedorId + ")", conn);

                        command.ExecuteNonQuery();
                        conn.Close();




                        PreenchaCboCarteiras();
                    }
                  else
                    {
                        string Sql = "update Carteiras set Vendedor = " + cboVendedores.EditValue.ToInt() +
                                     " , NomeCarteira = '" + txtNomeCarteira.Text + "'" +
                                     " where Id = " + cboCarteiras.EditValue.ToInt();


                        MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                        MySqlDataReader MyReader2;


                        var returnValue = MyCommand.ExecuteReader();
                        Novo = false;
                        MessageBox.Show("Registro Alterado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PreenchaCboCarteiras();
                        return;
                    }

                 
                }
                

                if (_listaTmkGrid != null)
                {
                    foreach (var itemTmk in _listaTmkGrid)
                    {

                        if (itemTmk.Seleciona == true)
                        {
                            using (var conn = new MySqlConnection(ConectionStringII))
                            {

                                conn.Open();
                                string Sql = "";


                                Sql = "update pedidosvendas set PEDIDO_CARTEIRA = " + this.UltimoID +
                                        " where pedido_id = " + itemTmk.Id;



                                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                                MySqlDataReader MyReader2;


                                var returnValue = MyCommand.ExecuteReader();


                            }
                        }
                    }
                }
                Novo = false;
                MessageBox.Show("Registro Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNomeCarteira.Text = "";
                cboVendedores.Text = string.Empty;
                
            }
        }

        private void gcPedidosDeVenda_DoubleClick(object sender, EventArgs e)
        {
            Selecione();
        }

        private void gcPedidosDeVenda_KeyDown(object sender, KeyEventArgs e)
        {           

            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void gcPedidosDeVenda_Click(object sender, EventArgs e)
        {           

        }

        #endregion

        #region " MÉTODOS AUXILIARES "
        private void PreenchaCboCategorias()
        {
            ServicoCategoria servicoCategoria = new ServicoCategoria();

            var categorias = servicoCategoria.ConsulteListaAtiva();

            categorias.Insert(0, null);

            cboCategorias.Properties.DataSource = categorias;
            cboCategorias.Properties.DisplayMember = "Descricao";
            cboCategorias.Properties.ValueMember = "Id";
        }

        private void PreenchaCboGrupos()
        {
            ServicoGrupo servicoGrupo = new ServicoGrupo();

            var grupos = servicoGrupo.ConsulteListaAtivos(new Categoria { Id = cboCategorias.EditValue.ToInt() });

            grupos.Insert(0, null);

            cboGrupos.Properties.DataSource = grupos;
            cboGrupos.Properties.DisplayMember = "Descricao";
            cboGrupos.Properties.ValueMember = "Id";

            if (string.IsNullOrEmpty(cboGrupos.Text))
            {
                cboGrupos.EditValue = null;
            }

            PreenchaCboSubGrupos();
        }

        private void PreenchaCboSubGrupos()
        {
            Grupo grupo = new Grupo();

            if (cboGrupos.EditValue != null)
            {
                grupo.Id = cboGrupos.EditValue.ToInt();
            }

            ServicoSubGrupo servicoSubGrupo = new ServicoSubGrupo();

            var grupos = servicoSubGrupo.ConsulteListaAtiva(grupo);

            grupos.Insert(0, null);

            cboSubGrupos.Properties.DataSource = grupos;
            cboSubGrupos.Properties.DisplayMember = "Descricao";
            cboSubGrupos.Properties.ValueMember = "Id";

            if (string.IsNullOrEmpty(cboSubGrupos.Text))
            {
                cboSubGrupos.EditValue = null;
            }
        }


        private void PreenchaCboMarcas()
        {



            ServicoMarca servicoMarcas = new ServicoMarca();

            var marcas = servicoMarcas.ConsulteListaAtiva();

            marcas.Insert(0, null);

            cboMarcas.Properties.DataSource = marcas;
            cboMarcas.Properties.DisplayMember = "Descricao";
            cboMarcas.Properties.ValueMember = "Id";

            if (cboMarcas.EditValue != null)
            {
                if (!marcas.Exists(marca => marca != null && marca.Id == cboMarcas.EditValue.ToInt()))
                {
                    cboMarcas.EditValue = null;
                }
            }
        }
        private void PreenchaCboCNPJ()
        {
            List<CnpjCBO> listaCNPJ = new List<CnpjCBO>();


            CnpjCBO listCnpj = new CnpjCBO();
            {
                listCnpj.Descricao = "C.P.F".ToString();
                listCnpj.ID = 1;
                listaCNPJ.Add(listCnpj);
            }
             listCnpj = new CnpjCBO();
            {
                listCnpj.Descricao = "C.N.P.J".ToString();
                listCnpj.ID = 2;
                listaCNPJ.Add(listCnpj);
            }

            listCnpj = new CnpjCBO();
            {
                listCnpj.Descricao = "Todos".ToString();
                listCnpj.ID = 3;
                listaCNPJ.Add(listCnpj);
            }
            var lista = listaCNPJ;
           
            lista.Insert(0, null);


            cbocpfcnpj.Properties.DataSource = lista;
            cbocpfcnpj.Properties.DisplayMember = "Descricao";
            cbocpfcnpj.Properties.ValueMember = "ID";

         
        }
        private void PreenchaCbo()
        {
            ServicoMarca servicoMarcas = new ServicoMarca();

            var marcas = servicoMarcas.ConsulteListaAtiva();

            marcas.Insert(0, null);

            cboMarcas.Properties.DataSource = marcas;
            cboMarcas.Properties.DisplayMember = "Descricao";
            cboMarcas.Properties.ValueMember = "Id";

            if (cboMarcas.EditValue != null)
            {
                if (!marcas.Exists(marca => marca != null && marca.Id == cboMarcas.EditValue.ToInt()))
                {
                    cboMarcas.EditValue = null;
                }
            }
        }

        public PedidoDeVenda ExibaPesquisaDePedidosDeVenda()
        {
            this.ShowDialog();

            return _pedidoDeVendaSelecionado;
        }
        
        public PedidoDeVenda ExibaPesquisaDePedidosDeVendaFaturadosEEmitdosNfe()
        {
            List<ObjetoDescricaoValor> lista = new List<ObjetoDescricaoValor>();

            ObjetoDescricaoValor objetoDescricaoValorFaturado = new ObjetoDescricaoValor();
            objetoDescricaoValorFaturado.Descricao = EnumStatusPedidoDeVenda.FATURADO.Descricao();
            objetoDescricaoValorFaturado.Valor = EnumStatusPedidoDeVenda.FATURADO;

            ObjetoDescricaoValor objetoDescricaoValorEmitidoNfe = new ObjetoDescricaoValor();
            objetoDescricaoValorEmitidoNfe.Descricao = EnumStatusPedidoDeVenda.EMITIDONFE.Descricao();
            objetoDescricaoValorEmitidoNfe.Valor = EnumStatusPedidoDeVenda.EMITIDONFE;

            lista.Add(objetoDescricaoValorFaturado);
            lista.Add(objetoDescricaoValorEmitidoNfe);

          

            this.ShowDialog();

            return _pedidoDeVendaSelecionado;
        }

        private void Pesquise()
        {
            this.Cursor = Cursors.WaitCursor;
            //if (txtCpf.Text != string.Empty)
            //{
            //    MaskedTextBox mskTest = new MaskedTextBox();
            //    string cpfcnpj = txtCpf.Text.Replace("/", "").Replace(".", "").Replace("-", "");
            //    if (cpfcnpj.Length == 11)

            //        txtCpf.Mask = "000,000,000-00";

            //    if (cpfcnpj.Length == 14)

            //        txtCpf.Mask = "00,000,000/0000-00";
            //}
            DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();
            string DataIni = string.Empty;
            string DataFim = string.Empty;
            int contador = 0;

            if (txtDataInicial.Text.ToString() != string.Empty)
            {
                 DataIni = dataInicial.Value.ToString("yyyy/MM/dd");
                 DataFim = dataFinal.Value.ToString("yyyy/MM/dd");
            }



            carregaconexaoII();
           

            int marcaId = cboMarcas.EditValue.ToInt();
            int categoriaId = cboCategorias.EditValue.ToInt();
            int GrupoId = cboGrupos.EditValue.ToInt();
            int SubGrupoId = cboSubGrupos.EditValue.ToInt();
            int IndicadorId = cboIndicadores.EditValue.ToInt();
            int cpfcnpjId = cbocpfcnpj.EditValue.ToInt();
            int clienteId = txtIdCliente.Text.ToInt();

            string Bairro = string.Empty;
            string DataNasc = string.Empty;


            if (cboBairro.Text.ToString() != string.Empty)
            {
                 Bairro = cboBairro.EditValue.ToString();
            }
                


            ServicoTmk servicoTmk = new ServicoTmk();
            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();

                string sqlWhere = "   (pedidosvendas.pedido_status <> 3 OR pedidosvendas.pedido_status is null ) And pedidosvendas.PEDIDO_CARTEIRA = 0 ";
                string innerJoin = " ";

                if (DataIni != string.Empty)
                {
                    sqlWhere = sqlWhere + " And date(pedidosvendas.pedido_data_elaboracao)  Between '" + DataIni + "' And '" + DataFim + "'";
                }
               
                innerJoin = innerJoin + " left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id ";
                innerJoin = innerJoin + " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id ";
                if (marcaId != 0)
                {

                    sqlWhere = sqlWhere + " AND produtos.prod_marc_id = " + "'" + marcaId + "'";

                }
                if (Bairro != string.Empty)
                {
                  sqlWhere = sqlWhere + " AND pedidosvendas.PEDIDO_BAIRRO_ENDERECO = " + "'" + Bairro + "'";
                }

                if (IndicadorId != 0)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.pedido_indicador_id = " + "'" + IndicadorId + "'";
                }
               
                if (cpfcnpjId != 0)
                {
                    switch (cpfcnpjId)
                    {
                        case 1:
                            sqlWhere = sqlWhere + " And char_length(pessoas.pes_insc_federal) = 14 ";
                            break;
                        case 2:
                            sqlWhere = sqlWhere + " And char_length(pessoas.pes_insc_federal) = 18 ";
                            break;
                        case 3:
                           
                            break;
                        

                    }
                    
                }



                if (categoriaId !=0 || GrupoId !=0 || SubGrupoId!=0 || marcaId != 0)
                {
                    innerJoin = innerJoin + " inner join pedidosvendasitens on pedidosvendasitens.peditem_pedido_id = pedidosvendas.pedido_id ";
                    innerJoin = innerJoin + " inner join produtos on pedidosvendasitens.peditem_produto_id = produtos.prod_id ";
                  
                }
                if (categoriaId !=0)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_grup_id = " + "'" + categoriaId + "'";
                }
                if (GrupoId != 0)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_grup_id = " + "'" + categoriaId + "'";
                }
                if (SubGrupoId != 0)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_subgrp_id = " + "'" + categoriaId + "'";
                }

                if (clienteId != 0)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.pedido_cliente_id = " + "'" + clienteId + "'";
                }

                var sql = " select pedidosvendas.pedido_data_elaboracao as DataCompra, " +
                        " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId, " +
                        " historicosatendimento.hisat_status as status,  pessoas.pes_razao, pessoas.pes_insc_federal" +

                " FROM  pedidosvendas " +
                   
                    innerJoin +

                    " WHERE " + sqlWhere + "order by  NumPedido, hisat_contador DESC ";

                
                
                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
                while (returnValue.Read())
                {
                    
                    TmkGrid tmkGrid = new TmkGrid();

                    tmkGrid.Id = returnValue["NumPedido"].ToInt();
                    var dt = DateTime.Parse(returnValue["DataCompra"].ToString()).ToString("dd-MM-yyyy");

                    tmkGrid.DataCompra = dt;

                   

                    tmkGrid.CodigoCliente = returnValue["ClienteId"].ToInt() != 0 ? returnValue["ClienteId"].ToString() : null;

                    tmkGrid.Cliente = returnValue["pes_razao"].ToString() != "" ? returnValue["pes_razao"].ToString() : null;
                    tmkGrid.Status = returnValue["status"].ToInt() != 0 ? returnValue["status"].ToString() : null;
                    tmkGrid.Seleciona = true;

                    if (returnValue["status"].ToString() != null)
                    {
                        if (returnValue["status"].ToInt() == 5)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_black;
                            tmkGrid.Status = "CANCELADO";
                        }
                        else if (returnValue["status"].ToInt() == 2)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleYellow;
                            tmkGrid.Status = "AGENDADO";
                        }
                        else if (returnValue["status"].ToInt() == 3)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                            tmkGrid.Status = "FINALIZADO";
                        }
                        else if (returnValue["status"].ToInt() == 4)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                            tmkGrid.Status = "EM ATENDIMENTO";
                        }
                        else
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }
                    }

                    if (numPedido != tmkGrid.Id)
                    {
                        listaTmkGrid.Add(tmkGrid);
                        contador += 1;
                    }

                    numPedido = tmkGrid.Id.ToInt();
                }
                gcAtendimentos.DataSource = listaTmkGrid;
                gcAtendimentos.RefreshDataSource();


                _listaTmkGrid = listaTmkGrid;
                txtQtdePedidos.Text = contador.ToString();

                //VerificaContador(dataInicial.ToString(), dataFinal.ToString());
            }
           // _listaTmk = servicoTmk.ConsulteListaParaTMK(cliente, statusTmk, dataInicial, dataFinal, marcaId);



            this.Cursor = Cursors.Default;
        }

        private void VerificaContador(string DataInicial, string DataFinal)
        {
            carregaconexaoII();

            //mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionStringII))
            {

                conn.Open();
                string DataIni = string.Empty;
                string DataF = string.Empty;

                if (DataInicial != string.Empty)
                {
                     DataIni = DataInicial.Substring(6, 4) + "-" + DataInicial.Substring(3, 2) + "-" + DataInicial.Substring(0, 2);
                     DataF = DataFinal.Substring(6, 4) + "-" + DataFinal.Substring(3, 2) + "-" + DataFinal.Substring(0, 2);
                }
      
               
                string Sql = string.Empty;

                int marcaId = cboMarcas.EditValue.ToInt();
                int categoriaId = cboCategorias.EditValue.ToInt();
                int GrupoId = cboGrupos.EditValue.ToInt();
                int SubGrupoId = cboSubGrupos.EditValue.ToInt();
                int IndicadorId = cboIndicadores.EditValue.ToInt();
                int cpfcnpjId = cbocpfcnpj.EditValue.ToInt();

                string Bairro = string.Empty;
                string DataNasc = string.Empty;

                if (cboBairro.Text.ToString() != string.Empty)
                {
                    Bairro = cboBairro.EditValue.ToString();
                }


                string sqlWhere = " pedidosvendas.pedido_status <> 3 And pedidosvendas.PEDIDO_CARTEIRA = 0";
                string innerJoin = " ";

                if (DataIni != string.Empty)
                {
                    sqlWhere = sqlWhere + " And pedidosvendas.pedido_data_elaboracao  Between '" + DataIni + "' And '" + DataF + "'";
                }

                innerJoin = innerJoin + " left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id ";
                innerJoin = innerJoin + " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id ";
                if (categoriaId != 0 || GrupoId != 0 || SubGrupoId != 0 || marcaId != 0)
                {
                    innerJoin = innerJoin + " inner join pedidosvendasitens on pedidosvendasitens.peditem_pedido_id = pedidosvendas.pedido_id ";
                    innerJoin = innerJoin + " inner join produtos on pedidosvendasitens.peditem_produto_id = produtos.prod_id ";

                }
                if (marcaId !=0)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_marc_id = " + "'" + marcaId + "'";
                }
                if (Bairro != string.Empty)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.PEDIDO_BAIRRO_ENDERECO = " + "'" + Bairro + "'";
                }

                if (IndicadorId != 0)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.pedido_indicador_id = " + "'" + IndicadorId + "'";
                }

               

                if (categoriaId != 0)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_grup_id = " + "'" + categoriaId + "'";
                }
                if (GrupoId != 0)
                {

                    sqlWhere = sqlWhere + " AND produtos.prod_grup_id = " + "'" + categoriaId + "'";
                }
                if (SubGrupoId != 0)
                {

                    sqlWhere = sqlWhere + " AND produtos.prod_subgrp_id = " + "'" + categoriaId + "'";
                }

                if (cpfcnpjId != 0)
                {
                    switch (cpfcnpjId)
                    {
                        case 1:
                            sqlWhere = sqlWhere + " And char_length(pessoas.pes_insc_federal) = 14 ";
                            break;
                        case 2:
                            sqlWhere = sqlWhere + " And char_length(pessoas.pes_insc_federal) = 18 ";
                            break;
                        case 3:

                            break;


                    }

                }

                var sql = " select count(pedidosvendas.pedido_id) as Contador, pedidosvendas.pedido_data_elaboracao as DataCompra, " +
                        " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId, " +
                        " historicosatendimento.hisat_status as status,  pessoas.pes_razao" +

                " FROM  pedidosvendas " +

                    innerJoin +

                    " WHERE " + sqlWhere + "order by  NumPedido";

                
                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    txtQtdePedidos.Text = returnValue["Contador"].ToString();
                }

            }

        }
        private void VerificaContadorII(int IdCarteira)
        {
            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexaoII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }

            }

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
               
               


                string sqlWhere = " pedidosvendas.pedido_status <> 3 ";
                string innerJoin = " ";

              

                innerJoin = innerJoin + " left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id ";
                innerJoin = innerJoin + " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id ";
                sqlWhere = sqlWhere + " AND pedidosvendas.PEDIDO_CARTEIRA = " + "'" + IdCarteira + "'";
              

                var sql = " select count(pedidosvendas.pedido_id) as Contador, pedidosvendas.pedido_data_elaboracao as DataCompra, " +
                        " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId, " +
                        " historicosatendimento.hisat_status as status,  pessoas.pes_razao" +

                " FROM  pedidosvendas " +

                    innerJoin +

                    " WHERE " + sqlWhere + "order by  NumPedido";


                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    txtQtdePedidos.Text = returnValue["Contador"].ToString();
                }

            }

        }

        private void PreenchaCboStatusAtendimento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusAtendimento>();

            lista.RemoveAt(4);

            lista.Insert(0, null);

         
        }

        
        private void PreenchaCboBairro()
        {
            this.Cursor = Cursors.WaitCursor;

            carregaconexaoII();
          
            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();


                var sql = "Select Distinct LTRIM(PEDIDO_BAIRRO_ENDERECO) as Descricao From pedidosvendas order by LTRIM(PEDIDO_BAIRRO_ENDERECO)";

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                int intNum = 1;
                List<BairroCbo> listaBairro = new List<BairroCbo>();
                while (returnValue.Read())
                {
                    BairroCbo listBairro = new BairroCbo();

                    listBairro.Bairro = returnValue["Descricao"].ToString();
                    listBairro.valor += intNum;
                    listaBairro.Add(listBairro);
                }
                var lista = listaBairro;

                lista.Insert(0, null);

                cboBairro.Properties.DisplayMember = "Bairro";
                cboBairro.Properties.ValueMember = "Bairro";
                cboBairro.Properties.DataSource = lista;

            }
            this.Cursor = Cursors.Default;

           
        }
        private void PreenchaCboCarteiras()
        {
            this.Cursor = Cursors.WaitCursor;


            carregaconexaoII();

            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();

                var sql = "Select ID, NomeCarteira From carteiras order by ID";

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                int intNum = 1;
                List<CarteiraCbo> listaCarteira = new List<CarteiraCbo>();
                while (returnValue.Read())
                {
                    CarteiraCbo listCarteira = new CarteiraCbo();

                    listCarteira.Carteira = returnValue["NomeCarteira"].ToString();
                    listCarteira.ID = returnValue["ID"].ToInt();
                    listaCarteira.Add(listCarteira);
                    UltimoID = returnValue["ID"].ToInt();
                }
                var lista = listaCarteira;

                lista.Insert(0, null);

                cboCarteiras.Properties.DisplayMember = "Carteira";
                cboCarteiras.Properties.ValueMember = "ID";
                cboCarteiras.Properties.DataSource = lista;

            }
            this.Cursor = Cursors.Default;


        }

       

        private void PreenchaGrid()
        {

            List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
            int numPedido = 0;
            foreach (var itemTmk in _listaTmk)
            {
                TmkGrid tmkGrid = new TmkGrid();

                tmkGrid.Id = itemTmk.NumPedido.ToInt();

                tmkGrid.DataCompra = itemTmk.DataCompra.ToString("dd/MM/yyyy");

                tmkGrid.CodigoCliente = itemTmk.ClienteId != 0 ? itemTmk.ClienteId.ToString() : null;

                tmkGrid.Cliente = itemTmk.DescricaoCliente;
                tmkGrid.Status = itemTmk.status != 0 ? itemTmk.status.ToString() : null;
                EnumStatusAtendimento? statusTmk = (EnumStatusAtendimento?)itemTmk.status;
                tmkGrid.Status = statusTmk.ToString();
                tmkGrid.NumPedidoNovo = itemTmk.NumPedidoNovo.ToInt();


                numPedido = itemTmk.NumPedido.ToInt();
            }

           

            gcAtendimentos.DataSource = listaTmkGrid;
            gcAtendimentos.RefreshDataSource();

            
        }

       

        private void Selecione()
        {
            //Vai chamar o FormAtendimento para fazer o atendimento selecionado

            //var tmk = _listaTmk.Find(x=>x.NumPedido == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

            //FormAtendimento formAtender = new FormAtendimento(tmk.NumPedido.ToInt(), tmk.DataCompra,"",0);

            //formAtender.Show();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class TmkGrid
        {
            public int Id { get; set; }
            public int Pedido { get; set; }

            public string DataCompra { get; set; }

            public string CodigoCliente { get; set; }

            public string Cliente { get; set; }                      

            public string Status { get; set; }
            public int  NumPedidoNovo { get; set; }

            public Image Cor { get; set; }

            public double ValorTotal { get; set; }

            public string Agendamento { get; set; }
           
            public virtual bool Seleciona { get; set; }
        }
        private class BairroCbo
        {
            public string valor { get; set; }
            public string Bairro { get; set; }

           
        }
        private class MarcaCbo
        {
            public string valor { get; set; }
            public string Marca { get; set; }


        }
        private class CarteiraCbo
        {
            public int ID { get; set; }
            public string Carteira { get; set; }


        }
        private class CnpjCBO
        {
            public int ID { get; set; }
            public string Descricao { get; set; }


        }


        #endregion

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            //Vai chamar o FormAtendimento para fazer o atendimento selecionado

            var tmk = _listaTmk.Find(x => x.NumPedido == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

            FormAtendimento formAtender = new FormAtendimento(tmk.NumPedido.ToInt(), tmk.DataCompra,tmk.status.ToString(),5, 0,true);

            formAtender.Show();
        }
       
        

        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtIdCliente_EditValueChanged(object sender, EventArgs e)
        {

        }

        //private void txtCpf_KeyDown(object sender, KeyEventArgs e)
        //{
           
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        MaskedTextBox mskTest = new MaskedTextBox();

        //        if (txtCpf.Text.Length == 11)

        //            txtCpf.Mask = "000,000,000-00";

        //        else

        //            txtCpf.Mask = "00,000,000/0000-00";
        //    }
        
        //}

        private void cboCategorias_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboGrupos();
        }

        private void cboGrupos_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboSubGrupos();
        }

        private void cboCarteiras_EditValueChanged(object sender, EventArgs e)
        {
          
        }

       
        
        private void PesquiseCarteira(int idCarteira)
        {
            this.Cursor = Cursors.WaitCursor;
            if (idCarteira != 0)
            {
                numCarteira = idCarteira;
            }

            btnLimpar.Enabled = true;
            if (cboCarteiras.Text.ToString() == string.Empty)
            {
                this.Cursor = Cursors.Default;
                gcAtendimentos.RefreshDataSource();
                return;
            }
            
            int contador = 0;

            
            btnExcluir.Visible = true;
            
           
          
           
            ServicoTmk servicoTmk = new ServicoTmk();
            
            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();

                string sqlWhere = "  pedidosvendas.pedido_status <> 3 ";
                string innerJoin = " ";

              
                innerJoin = innerJoin + " left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id ";
                innerJoin = innerJoin + " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id ";
                innerJoin = innerJoin + " left join agendamentocontato on pedidosvendas.pedido_id = agendamentocontato.pedido ";
                sqlWhere = sqlWhere + " AND pedidosvendas.PEDIDO_CARTEIRA = " + "'" + idCarteira + "'";
             

                var sql = " select pedidosvendas.pedido_data_elaboracao as DataCompra, " +
                        " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId, " +
                        " historicosatendimento.hisat_status as status,  pessoas.pes_razao, agendamentocontato.Data as Agendamento" +

                " FROM  pedidosvendas " +

                    innerJoin +

                    " WHERE " + sqlWhere + "order by  NumPedido , hisat_contador DESC";



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
                while (returnValue.Read())
                {

                    TmkGrid tmkGrid = new TmkGrid();

                    tmkGrid.Id = returnValue["NumPedido"].ToInt();

                  
                    var dt = DateTime.Parse(returnValue["DataCompra"].ToString()).ToString("dd-MM-yyyy");

                    tmkGrid.DataCompra = dt;

                    if (returnValue["Agendamento"].ToString() != string.Empty)
                    {
                        var dta = DateTime.Parse(returnValue["Agendamento"].ToString()).ToString("dd-MM-yyyy");
                        if (dta != "01-01-1900")
                        {
                            tmkGrid.Agendamento = dta;
                        }

                    }
                   

                    tmkGrid.CodigoCliente = returnValue["ClienteId"].ToInt() != 0 ? returnValue["ClienteId"].ToString() : null;

                    tmkGrid.Cliente = returnValue["pes_razao"].ToString() != "" ? returnValue["pes_razao"].ToString() : null;
                    tmkGrid.Status = returnValue["status"].ToInt() != 0 ? returnValue["status"].ToString() : null;
                    tmkGrid.Seleciona = true;

                    //if (returnValue["status"].ToString() != null)
                    //{ 
                    //    if (returnValue["status"].ToInt() == 1)
                    //    {
                    //        tmkGrid.Cor = Properties.Resources.CircleGreen;
                    //        tmkGrid.Status = "DISPONIVEL";
                    //    }
                    //    else if (returnValue["status"].ToInt() == 2)
                    //    {
                    //        tmkGrid.Cor = Properties.Resources.CircleYellow;
                    //        tmkGrid.Status = "AGENDADO";
                    //    }
                    //    else if (returnValue["status"].ToInt() == 3)
                    //    {
                    //        tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                    //        tmkGrid.Status = "EM ATENDIMENTO";
                    //    }
                    //    else if (returnValue["status"].ToInt() == 4)
                    //    {
                    //        tmkGrid.Cor = Properties.Resources.circle_black;
                    //        tmkGrid.Status = "CANCELADO";
                    //    }
                   
                    //    else
                    //    {
                    //        tmkGrid.Cor = Properties.Resources.CircleGreen;
                    //        tmkGrid.Status = "DISPONIVEL";
                    //    }
                    //}



                    if (returnValue["status"].ToString() != null)
                    {
                        if (returnValue["status"].ToInt() == 0)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }

                        else if (returnValue["status"].ToInt() == 2)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleYellow;
                            tmkGrid.Status = "AGENDADO";
                        }
                        else if (returnValue["status"].ToInt() == 1)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                            tmkGrid.Status = "CONCLUIDO";
                        }
                        else if (returnValue["status"].ToInt() == 3)
                        {
                            if (tmkGrid.NumPedidoNovo != 0)
                            {
                                tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                                tmkGrid.Status = "CONCLUIDO";
                            }
                            else
                            {
                                tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                                tmkGrid.Status = "EM ATENDIMENTO";
                            }

                        }
                        else if (returnValue["status"].ToInt() == 4)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_black;
                            tmkGrid.Status = "CANCELADO";
                        }
                        else
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }
                    }



                    if (numPedido != tmkGrid.Id)
                    {
                        listaTmkGrid.Add(tmkGrid);
                        contador += 1;
                       
                    }
                  
                    numPedido = tmkGrid.Id.ToInt();
                }
                gcAtendimentos.DataSource = listaTmkGrid;
                gcAtendimentos.RefreshDataSource();


                _listaTmkGrid = listaTmkGrid;
                txtQtdePedidos.Text = contador.ToString();
                Novo = true;
                VerificaVendedor(idCarteira);
            }
            // _listaTmk = servicoTmk.ConsulteListaParaTMK(cliente, statusTmk, dataInicial, dataFinal, marcaId);

           // cboCarteiras.EditValue = 0;

            this.Cursor = Cursors.Default;
        }
        private void VerificaVendedor(int IdCarteira)
        {
            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexaoII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }

            }

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
                var sql = " SELECT Vendedor, NomeCarteira FROM carteiras " +
                          " Where id = " + IdCarteira;

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    cboVendedores.EditValue = returnValue["Vendedor"].ToInt();
                    txtNomeCarteira.Text = returnValue["NomeCarteira"].ToString();
                }

            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Deseja excluir esta carteira?", "Excluir Carteira", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            carregaconexaoII();


            {
                using (var conn = new MySqlConnection(ConectionStringII))

                {
                   if (numCarteira.ToInt() > 5)
                    {
                        conn.Open();

                        MySqlCommand command = new MySqlCommand("DELETE  From  carteiras Where ID = " + numCarteira.ToInt(), conn);

                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                     




                    //PreenchaCboCarteiras();
                }

                foreach (var itemTmk in _listaTmkGrid)
                {
                   if (itemTmk.Seleciona == true)
                    {
                        using (var conn = new MySqlConnection(ConectionStringII))
                        {

                            conn.Open();

                            string Sql = "update pedidosvendas set PEDIDO_CARTEIRA = " + 0 +
                                         " where pedido_id = " + itemTmk.Id;


                            MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                            MySqlDataReader MyReader2;


                            var returnValue = MyCommand.ExecuteReader();


                        }
                        using (var conn = new MySqlConnection(ConectionStringII))
                        {

                            conn.Open();

                            string Sql = "update historicosatendimento set hisat_status = " + 0 +
                                         " where hisat_pedido_id = " + itemTmk.Id;


                            MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                            MySqlDataReader MyReader2;


                            var returnValue = MyCommand.ExecuteReader();


                        }
                        using (var conn = new MySqlConnection(ConectionStringII))
                        {

                            conn.Open();

                            string Sql = "Delete from agendamentocontato " +
                                         " where pedido = " + itemTmk.Id;


                            MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                            MySqlDataReader MyReader2;


                            var returnValue = MyCommand.ExecuteReader();


                        }
                    }
                   
                }
                MessageBox.Show("Registro Excluido com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PesquiseCarteira(cboCarteiras.EditValue.ToInt());
                btnExcluir.Visible = false;
                Novo = false;
                List<TmkGrid> listaTmkGrid = new List<TmkGrid>();

                TmkGrid tmkGrid = new TmkGrid();
                gcAtendimentos.DataSource = listaTmkGrid;
                gcAtendimentos.RefreshDataSource();
                cboVendedores.EditValue = 0;
                cboCarteiras.EditValue = 0;
                txtNomeCarteira.Text = "";
                txtQtdePedidos.Text = "";

            }
            PreenchaCboCarteiras();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Novo = false;
            List<TmkGrid> listaTmkGrid = new List<TmkGrid>();

            TmkGrid tmkGrid = new TmkGrid();
            gcAtendimentos.DataSource = listaTmkGrid;
            gcAtendimentos.RefreshDataSource();
            cboVendedores.EditValue = 0;
            cboCarteiras.EditValue = 0;
            txtNomeCarteira.Text = "";
            txtQtdePedidos.Text = "";
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //gcAtendimentos.ExibaRelatorio();
           
        }

        private void cboVendedores_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboMarcas_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnpesquisacarteiras_Click(object sender, EventArgs e)
        {
            if(cboCarteiras.EditValue != null)
            {
                numCarteira = cboCarteiras.EditValue.ToInt();
                if (numCarteira <=5)
                {
                    btnAtender.Visible = false;
                    btnreagendar.Visible = true;
                }
                else
                {
                    btnAtender.Visible = true;
                    btnreagendar.Visible = false;
                }
                PesquiseCarteira(cboCarteiras.EditValue.ToInt());
            }
           
        }

        private void cboVendedores_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void btnreagendar_Click(object sender, EventArgs e)
        {
            if (txtNomeCarteira.Text == string.Empty)
            {
                MessageBox.Show("Para Gravar a Carteira. Você precisa informar a Descrição da Carteira!", "Conclusão da Carteira!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            if (cboVendedores.EditValue.ToInt() == 0)
            {
                MessageBox.Show("Para Gravar a Carteira. Você precisa informar o Vendedor!", "Conclusão da Carteira!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }



            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];

            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }


            }
            string NomeCarteira = txtNomeCarteira.Text;
            var dtinicial = "1900-01-01 00:00:00";
            var dtfinal = "1900-01-01 00:00:00";
            DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();

            if (dataInicial != null)
            {

                dtinicial = DateTime.Parse(dataInicial.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }
            DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();

            if (dataFinal != null)
            {

                dtfinal = DateTime.Parse(dataFinal.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            }



            string Bairro = string.Empty;
            int marcaId = cboMarcas.EditValue.ToInt();
            if (cboBairro.EditValue.ToInt() != 0)
            {
                Bairro = cboBairro.EditValue.ToString();
            }


            int CPF = 0;
            int CNPJ = 0;
            int categoriaId = cboCategorias.EditValue.ToInt();
            int grupoId = cboGrupos.EditValue.ToInt();
            int subgrupoId = cboSubGrupos.EditValue.ToInt();
            int vendedorId = cboVendedores.EditValue.ToInt();
            int indicadorId = cboIndicadores.EditValue.ToInt();

            if (cbocpfcnpj.EditValue.ToInt() == 1)
            {
                CPF = 1;
            }
            if (cbocpfcnpj.EditValue.ToInt() == 2)
            {
                CNPJ = 1;
            }

            if (cbocpfcnpj.EditValue.ToInt() == 3)
            {
                CPF = 1;
                CNPJ = 1;
            }

            {
                using (var conn = new MySqlConnection(ConectionString))

                {
                    conn.Open();

                   
                        MySqlCommand command = new MySqlCommand("INSERT INTO carteiras (NomeCarteira,DataInicial,DataFinal,Marca,DataNascimento, " +
                                                                                 "Bairro, Idade, CPF, Categoria, Grupo, SubGrupo, CNPJ, Indicador,Vendedor)" +
                                         "VALUES('" + NomeCarteira + "', '" + dtinicial + "', '" + dtfinal + "', " + marcaId + ", '" + "1900-01-01" + "'," +
                                                "'" + Bairro + "', '" + 0 + "', " + CPF + ", " + categoriaId + ", " + grupoId + ", " + subgrupoId + "," +
                                                 CNPJ + "," + indicadorId + "," + vendedorId + ")", conn);

                        command.ExecuteNonQuery();
                        conn.Close();




                        PreenchaCboCarteiras();
                    


                }


                if (_listaTmkGrid != null)
                {
                    foreach (var itemTmk in _listaTmkGrid)
                    {

                        if (itemTmk.Seleciona == true)
                        {
                            using (var conn = new MySqlConnection(ConectionString))
                            {

                                conn.Open();
                                string Sql = "";


                                Sql = "update pedidosvendas set PEDIDO_CARTEIRA = " + this.UltimoID +
                                        " where pedido_id = " + itemTmk.Id;



                                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                                MySqlDataReader MyReader2;


                                var returnValue = MyCommand.ExecuteReader();


                            }
                        }
                    }
                }
                Novo = false;
                MessageBox.Show("Registro Gravado com Sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNomeCarteira.Text = "";
                cboVendedores.Text = string.Empty;

            }
        }
        private void txtIdCliente_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdCliente.Text))
            {
              
                PreenchaCliente(txtIdCliente.Text.ToInt());
            }
            else
            {
                PreenchaCliente(0);
            }
        }
      
        private void PreenchaCliente(int Cliente)
        {


            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();

                string sqlWhere = "  pes_id = " + Cliente;

                var sql = " select pes_id, pes_razao " +
                    " FROM  pessoas " +
                    " WHERE " + sqlWhere;



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
                while (returnValue.Read())
                {

                    if (returnValue["pes_id"].ToString() != null)
                    {
                        txtIdCliente.Text = returnValue["pes_id"].ToString();
                        txtNomeCliente.Text = returnValue["pes_razao"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Cliente nao encontrado!", "Cliente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIdCliente.Focus();
                        txtIdCliente.Text = string.Empty;
                        txtNomeCliente.Text = string.Empty;
                    }

                }
            }
          
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            //FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            //var cliente = formPessoaPesquisa.PesquisePessoaClienteAtiva();

            //if (cliente != null)
            //{
            //    PreenchaCliente(cliente);
            //}
        }

        private void txtIdCliente_EditValueChanged_1(object sender, EventArgs e)
        {

        }
    }
}
