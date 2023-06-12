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
    public partial class FormCarteiras : FormularioPadrao
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
        private int contador = 0;
        private string[] cod;
        private string DescricaoVend;
        private string codPessoa;
        private string[] codind;
        private string DescricaoInd;
        private string codIndicador;
        private string[] codbairro;
        private string Descricaobairro;
        private string codBairro;
        private string[] codmarca;
        private string Descricaomarca;
        private string codMarca;
        private string[] codcategoria;
        private string Descricaocategoria;
        private string codCategoria;
        private string[] codgrupo;
        private string Descricaogrupo;
        private string codGrupo;
        private string[] codsubgrupo;
        private string Descricaosubgrupo;
        private string codSubgrupo;
        private int numeroTentativas = 0;


        #endregion

        #region " CONSTRUTOR "

        public FormCarteiras(bool somenteImpressao = false)
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
            PreenchaCboPor();
            PreenchaCboTentativa();
            PreenchaCboGrupos();
            PreenchaCboSubGrupos();
            btnAtender.Visible = true;
            CarregueParametros();
            if (somenteImpressao)
            {
                this.NomeDaTela = "Busca Atendimentos";
                btnAtender.Visible = false;
            }

            txtDataInicial.DateTime = DateTime.Now.Date;
            txtDataFinal.DateTime = DateTime.Now.Date;
            ServicoParametros servicoParametros = new ServicoParametros();
            _parametros = servicoParametros.ConsulteParametros();

            gridControl1.Visible = false;

            this.ActiveControl = txtDataInicial;
        }

        #endregion

        #region " EVENTOS CONTROLES "
        private void PreenchalstBairros()
        {
           
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            btnExcluir.Visible = false;
            btnAtender.Visible = true;
            cboCarteiras.EditValue = 0;
            cboVendedores.EditValue = 0;
            txtNomeCarteira.Text = "";
            Novo = false;
           if (cboPor.ItemIndex == 1)
            {
               
                PesquiseCliente();
                Pesquise();
            }
            else
            {
                Pesquise();
            }
            
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
            List<ObjetoDescricaoValor> listaObjetoValorDesc = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor {  Descricao = pessoa.DadosGerais.Razao, Valor = pessoa.Id };
                ObjetoDescricaoValor objetoDescricaoValorDesc = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
                listaObjetoValorDesc.Add(objetoDescricaoValorDesc);
            });

            lstVendedor.Items.Add("");

            foreach (var itens in listaObjetoValorDesc)
            {
                int i = 0;
                lstVendedor.Items.Add(itens.Descricao);
                i++;

            }
            

            listaObjetoValor.Insert(0, null);

       
            cboVendedores.Properties.DisplayMember = "Descricao";
            cboVendedores.Properties.ValueMember = "Valor";
            cboVendedores.Properties.DataSource = listaObjetoValor;

            cboVendedor.Properties.DisplayMember = "Descricao";
            cboVendedor.Properties.ValueMember = "Valor";
            cboVendedor.Properties.DataSource = listaObjetoValor;



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
            
            lstIndicador.Items.Add("");

            foreach (var itens in listaObjetoValor)
            {
                int i = 0;
                lstIndicador.Items.Add(itens.Descricao);
                i++;

            }
            listaObjetoValor.Insert(0, null);

      

            cboIndicadores.Properties.DisplayMember = "Descricao";
            cboIndicadores.Properties.ValueMember = "Valor";
            cboIndicadores.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboIndicadores.Text))
            {
                cboIndicadores.EditValue = null;
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
            lstCategoria.Items.Add("");

            foreach (var itens in categorias)
            {
                int i = 0;
                lstCategoria.Items.Add( itens.Id + "-" + itens.Descricao);
                
                i++;

            }

            categorias.Insert(0, null);

            cboCategorias.Properties.DataSource = categorias;
            cboCategorias.Properties.DisplayMember = "Descricao";
            cboCategorias.Properties.ValueMember = "Id";
        }

        private void PreenchaCboGrupos()
        {
            ServicoGrupo servicoGrupo = new ServicoGrupo();

            var grupos = servicoGrupo.ConsulteLista();

            lstGrupos.Items.Add("");

            foreach (var itens in grupos)
            {
                int i = 0;
                lstGrupos.Items.Add(itens.Id + "-" + itens.Descricao);
               
                i++;

            }

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

            if (lstGrupos.SelectedItems.Count > 1)
            {
                grupo.Id = cboGrupos.EditValue.ToInt();
            }

            ServicoSubGrupo servicoSubGrupo = new ServicoSubGrupo();

            var grupos = servicoSubGrupo.ConsulteLista();
            lstsubgrupo.Items.Add("");

            foreach (var itens in grupos)
            {
                int i = 0;
                lstsubgrupo.Items.Add(itens.Id + "-" + itens.Descricao);
               
                i++;

            }

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
            lstMarca.Items.Add("");

            foreach (var itens in marcas)
            {
                int i = 0;
                lstMarca.Items.Add(itens.Id + "-" + itens.Descricao);
                i++;

            }

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
        private void PreenchaCboPor()
        {
            List<ConsultaPorCBO> listaConsultaPor = new List<ConsultaPorCBO>();


            ConsultaPorCBO listPor = new ConsultaPorCBO();
            {
                listPor.Descricao = "Cliente".ToString();
                listPor.ID = 1;
                listaConsultaPor.Add(listPor);
            }
            listPor = new ConsultaPorCBO();
            {
                listPor.Descricao = "Pedido".ToString();
                listPor.ID = 2;
                listaConsultaPor.Add(listPor);
            }

           
            var lista = listaConsultaPor;

            lista.Insert(0, null);


            cboPor.Properties.DataSource = lista;
            cboPor.Properties.DisplayMember = "Descricao";
            cboPor.Properties.ValueMember = "ID";


        }
        private void PreenchaCboTentativa()
        {
            List<ConsultaTentativaCBO> listaConsultaTentativa = new List<ConsultaTentativaCBO>();


            ConsultaTentativaCBO listTentativa = new ConsultaTentativaCBO();
            {
                listTentativa.Descricao = "0 - Nenhuma Tentativa".ToString();
                listTentativa.ID = 0;
                listaConsultaTentativa.Add(listTentativa);
            }
            listTentativa = new ConsultaTentativaCBO();
            {
                listTentativa.Descricao = "1 - 10 tentativas".ToString();
                listTentativa.ID = 1;
                listaConsultaTentativa.Add(listTentativa);
            }
            listTentativa = new ConsultaTentativaCBO();
            {
                listTentativa.Descricao = "11 - 50 tentativas".ToString();
                listTentativa.ID = 2;
                listaConsultaTentativa.Add(listTentativa);
            }
            listTentativa = new ConsultaTentativaCBO();
            {
                listTentativa.Descricao = "mais 50 tentativas".ToString();
                listTentativa.ID = 3;
                listaConsultaTentativa.Add(listTentativa);
            }



            var lista = listaConsultaTentativa;

            lista.Insert(0, null);


            cboTentativa.Properties.DataSource = lista;
            cboTentativa.Properties.DisplayMember = "Descricao";
            cboTentativa.Properties.ValueMember = "ID";


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


            int marcaId = cboMarcas.EditValue.ToInt();
            int categoriaId = cboCategorias.EditValue.ToInt();
            int GrupoId = cboGrupos.EditValue.ToInt();
            int SubGrupoId = cboSubGrupos.EditValue.ToInt();
            int IndicadorId = cboIndicadores.EditValue.ToInt();
            int cpfcnpjId = cbocpfcnpj.EditValue.ToInt();
            int clienteId = txtIdCliente.Text.ToInt();
            int VendedorId = cboVendedor.EditValue.ToInt();

            string Bairro = string.Empty;
            string DataNasc = string.Empty;


            if (lstBairros.SelectedItems.Count > 1)
            {
                Descricaobairro = "";

                for (int i = 0; i < lstBairros.SelectedItems.Count; i++)
                {
                    string textoPessoa = lstBairros.SelectedItems[i].ToString();

                    if (textoPessoa != string.Empty)
                    {
                        codbairro = textoPessoa.Split('-');
                        Descricaobairro += "'" + codbairro[0].ToString() + "'" + ",";
                    }
                }
                codBairro = Descricaobairro.Substring(0, Descricaobairro.Length - 1);
            }

            if (lstVendedor.SelectedItems.Count > 1)
            {
                DescricaoVend = "";

                for (int i = 0; i < lstVendedor.SelectedItems.Count; i++)
                {
                    string textoPessoa = lstVendedor.SelectedItems[i].ToString();
                  
                    if (textoPessoa != string.Empty)
                    {
                        cod = textoPessoa.Split('-')  ;
                        DescricaoVend += "'" + cod[0].ToString() + "'" + ",";
                    }
                }
                codPessoa = DescricaoVend.Substring(0, DescricaoVend.Length - 1);
            }

            if (lstIndicador.SelectedItems.Count > 1)
            {
                DescricaoInd = "";

                for (int i = 0; i < lstIndicador.SelectedItems.Count; i++)
                {
                    string textoPessoa = lstIndicador.SelectedItems[i].ToString();

                    if (textoPessoa != string.Empty)
                    {
                        codind = textoPessoa.Split('-');
                        DescricaoInd += "'" + codind[0].ToString() + "'" + ",";
                    }
                }
                codIndicador = DescricaoInd.Substring(0, DescricaoInd.Length - 1);
            }
            if (lstMarca.SelectedItems.Count > 1)
            {
                Descricaomarca = "";

                for (int i = 0; i < lstMarca.SelectedItems.Count; i++)
                {
                    string textoPessoa = String.Join("", System.Text.RegularExpressions.Regex.Split(lstMarca.SelectedItems[i].ToString(), @"[^\d]"));

                    if (textoPessoa != string.Empty)
                    {
                        codmarca = textoPessoa.Split('-');
                        Descricaomarca += "'" + codmarca[0].ToString() + "'" + ",";
                    }
                }
                codMarca = Descricaomarca.Substring(0, Descricaomarca.Length - 1);
            }
            if (lstCategoria.SelectedItems.Count > 1)
            {
                Descricaocategoria = "";

                for (int i = 0; i < lstCategoria.SelectedItems.Count; i++)
                {
                    string textoPessoa = String.Join("", System.Text.RegularExpressions.Regex.Split(lstCategoria.SelectedItems[i].ToString(), @"[^\d]"));

                    if (textoPessoa != string.Empty)
                    {
                        codcategoria = textoPessoa.Split('-');
                        Descricaocategoria += "'" + codcategoria[0].ToString() + "'" + ",";
                    }
                }
                codCategoria = Descricaocategoria.Substring(0, Descricaocategoria.Length - 1);
            }
            if (lstGrupos.SelectedItems.Count > 1)
            {
                Descricaogrupo = "";
               
                

                for (int i = 0; i < lstGrupos.SelectedItems.Count; i++)
                {
                    string textoPessoa = String.Join("", System.Text.RegularExpressions.Regex.Split(lstGrupos.SelectedItems[i].ToString(), @"[^\d]"));

                    if (textoPessoa != string.Empty)
                    {
                        codgrupo = textoPessoa.Split('-');
                        Descricaogrupo += "'" + codgrupo[0].ToString() + "'" + ",";
                    }
                }
                codGrupo = Descricaogrupo.Substring(0, Descricaogrupo.Length - 1);
            }
            if (lstsubgrupo.SelectedItems.Count > 1)
            {
                Descricaosubgrupo = "";
               

                for (int i = 0; i < lstsubgrupo.SelectedItems.Count; i++)
                {
                    string textoPessoa = String.Join("", System.Text.RegularExpressions.Regex.Split(lstsubgrupo.SelectedItems[i].ToString(), @"[^\d]"));

                    if (textoPessoa != string.Empty)
                    {
                        codsubgrupo = textoPessoa.Split('-');
                        Descricaosubgrupo += "'" + codsubgrupo[0].ToString() + "'" + ",";
                    }
                }
                codSubgrupo = Descricaosubgrupo.Substring(0, Descricaosubgrupo.Length - 1);
            }
            ServicoTmk servicoTmk = new ServicoTmk();
            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionString))
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
                if  (lstBairros.SelectedItems.Count > 1)
                    {
                  sqlWhere = sqlWhere + " AND pedidosvendas.PEDIDO_BAIRRO_ENDERECO IN " + "(" + codBairro + ")";
                }

                if (lstIndicador.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.pedido_indicador_id IN " + "(" + codIndicador + ")";
                }

                if (lstVendedor.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.pedido_vendedor_id IN " + "(" + codPessoa + ")";
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



                //if (categoriaId !=0 || GrupoId !=0 || SubGrupoId!=0 || marcaId != 0)
                //{
                //    innerJoin = innerJoin + " inner join pedidosvendasitens on pedidosvendasitens.peditem_pedido_id = pedidosvendas.pedido_id ";
                //    innerJoin = innerJoin + " inner join produtos on pedidosvendasitens.peditem_produto_id = produtos.prod_id ";

                //}

                if (lstCategoria.SelectedItems.Count > 1 || lstMarca.SelectedItems.Count > 1 || lstGrupos.SelectedItems.Count > 1 || lstsubgrupo.SelectedItems.Count > 1)
                {
                    innerJoin = innerJoin + " inner join pedidosvendasitens on pedidosvendasitens.peditem_pedido_id = pedidosvendas.pedido_id ";
                    innerJoin = innerJoin + " inner join produtos on pedidosvendasitens.peditem_produto_id = produtos.prod_id ";
                }


                if (lstCategoria.SelectedItems.Count > 1 )
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_grup_id IN " + "(" + codCategoria + ")";
                }
                if (lstMarca.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_marc_id IN " + "(" + codMarca + ")";
                }
                if (lstGrupos.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_grup_id IN " + "(" + codGrupo + ")";
                }
                if (lstsubgrupo.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_subgrp_id IN " + "(" + codSubgrupo + ")";
                }

                if (clienteId != 0)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.pedido_cliente_id = " + "'" + clienteId + "'";
                }

                var sql = " select pedidosvendas.pedido_data_elaboracao as DataCompra, " +
                        " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId, " +
                        " historicosatendimento.hisat_status as status,  pessoas.pes_razao, pessoas.pes_insc_federal, " +
                        " (SELECT count(hisat_id) as contador FROM historicosatendimento where hisat_cliente = ClienteId) as Contador " +
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
                    tmkGrid.Tentativas = returnValue["contador"].ToInt();

                    if (numPedido != tmkGrid.Id)
                    {
                        if (cboTentativa.ItemIndex != 0)
                        {
                            switch (cboTentativa.ItemIndex.ToInt())
                            {
                                case 1:
                                    if (returnValue["contador"].ToInt() == 0)
                                    {
                                        listaTmkGrid.Add(tmkGrid);
                                        contador += 1;
                                    }
                                    break;
                                case 2:
                                    if (returnValue["contador"].ToInt() > 1 && returnValue["contador"].ToInt() < 10)
                                    {
                                        listaTmkGrid.Add(tmkGrid);
                                        contador += 1;
                                    }
                                    break;
                                case 3:
                                    if (returnValue["contador"].ToInt() > 10 && returnValue["contador"].ToInt() < 50)
                                    {
                                        listaTmkGrid.Add(tmkGrid);
                                        contador += 1;
                                    }
                                    break;
                                case 4:
                                    if (returnValue["contador"].ToInt() > 50)
                                    {
                                        listaTmkGrid.Add(tmkGrid);
                                        contador += 1;
                                    }
                                    break;
                            }
                             

                        }
                        else
                        {
                            listaTmkGrid.Add(tmkGrid);
                            contador += 1;
                        }

                        //Tentativas(tmkGrid.CodigoCliente.ToInt());

                        

                        numPedido = tmkGrid.Id.ToInt();
                    }
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
        private void PesquiseCliente()
        {
            this.Cursor = Cursors.WaitCursor;
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


            int marcaId = cboMarcas.EditValue.ToInt();
            int categoriaId = cboCategorias.EditValue.ToInt();
            int GrupoId = cboGrupos.EditValue.ToInt();
            int SubGrupoId = cboSubGrupos.EditValue.ToInt();
            int IndicadorId = cboIndicadores.EditValue.ToInt();
            int cpfcnpjId = cbocpfcnpj.EditValue.ToInt();
            int clienteId = txtIdCliente.Text.ToInt();
            int VendedorId = cboVendedor.EditValue.ToInt();

            string Bairro = string.Empty;
            string DataNasc = string.Empty;


            if (lstBairros.SelectedItems.Count > 1)
            {
                Descricaobairro = "";

                for (int i = 0; i < lstBairros.SelectedItems.Count; i++)
                {
                    string textoPessoa = lstBairros.SelectedItems[i].ToString();

                    if (textoPessoa != string.Empty)
                    {
                        codbairro = textoPessoa.Split('-');
                        Descricaobairro += "'" + codbairro[0].ToString() + "'" + ",";
                    }
                }
                codBairro = Descricaobairro.Substring(0, Descricaobairro.Length - 1);
            }

            if (lstVendedor.SelectedItems.Count > 1)
            {
                DescricaoVend = "";

                for (int i = 0; i < lstVendedor.SelectedItems.Count; i++)
                {
                    string textoPessoa = lstVendedor.SelectedItems[i].ToString();

                    if (textoPessoa != string.Empty)
                    {
                        cod = textoPessoa.Split('-');
                        DescricaoVend += "'" + cod[0].ToString() + "'" + ",";
                    }
                }
                codPessoa = DescricaoVend.Substring(0, DescricaoVend.Length - 1);
            }

            if (lstIndicador.SelectedItems.Count > 1)
            {
                DescricaoInd = "";

                for (int i = 0; i < lstIndicador.SelectedItems.Count; i++)
                {
                    string textoPessoa = lstIndicador.SelectedItems[i].ToString();

                    if (textoPessoa != string.Empty)
                    {
                        codind = textoPessoa.Split('-');
                        DescricaoInd += "'" + codind[0].ToString() + "'" + ",";
                    }
                }
                codIndicador = DescricaoInd.Substring(0, DescricaoInd.Length - 1);
            }
            if (lstMarca.SelectedItems.Count > 1)
            {
                Descricaomarca = "";

                for (int i = 0; i < lstMarca.SelectedItems.Count; i++)
                {
                    string textoPessoa = String.Join("", System.Text.RegularExpressions.Regex.Split(lstMarca.SelectedItems[i].ToString(), @"[^\d]"));

                    if (textoPessoa != string.Empty)
                    {
                        codmarca = textoPessoa.Split('-');
                        Descricaomarca += "'" + codmarca[0].ToString() + "'" + ",";
                    }
                }
                codMarca = Descricaomarca.Substring(0, Descricaomarca.Length - 1);
            }
            if (lstCategoria.SelectedItems.Count > 1)
            {
                Descricaocategoria = "";

                for (int i = 0; i < lstCategoria.SelectedItems.Count; i++)
                {
                    string textoPessoa = String.Join("", System.Text.RegularExpressions.Regex.Split(lstCategoria.SelectedItems[i].ToString(), @"[^\d]"));

                    if (textoPessoa != string.Empty)
                    {
                        codcategoria = textoPessoa.Split('-');
                        Descricaocategoria += "'" + codcategoria[0].ToString() + "'" + ",";
                    }
                }
                codCategoria = Descricaocategoria.Substring(0, Descricaocategoria.Length - 1);
            }
            if (lstGrupos.SelectedItems.Count > 1)
            {
                Descricaogrupo = "";



                for (int i = 0; i < lstGrupos.SelectedItems.Count; i++)
                {
                    string textoPessoa = String.Join("", System.Text.RegularExpressions.Regex.Split(lstGrupos.SelectedItems[i].ToString(), @"[^\d]"));

                    if (textoPessoa != string.Empty)
                    {
                        codgrupo = textoPessoa.Split('-');
                        Descricaogrupo += "'" + codgrupo[0].ToString() + "'" + ",";
                    }
                }
                codGrupo = Descricaogrupo.Substring(0, Descricaogrupo.Length - 1);
            }
            if (lstsubgrupo.SelectedItems.Count > 1)
            {
                Descricaosubgrupo = "";


                for (int i = 0; i < lstsubgrupo.SelectedItems.Count; i++)
                {
                    string textoPessoa = String.Join("", System.Text.RegularExpressions.Regex.Split(lstsubgrupo.SelectedItems[i].ToString(), @"[^\d]"));

                    if (textoPessoa != string.Empty)
                    {
                        codsubgrupo = textoPessoa.Split('-');
                        Descricaosubgrupo += "'" + codsubgrupo[0].ToString() + "'" + ",";
                    }
                }
                codSubgrupo = Descricaosubgrupo.Substring(0, Descricaosubgrupo.Length - 1);
            }
            ServicoTmk servicoTmk = new ServicoTmk();
            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionString))
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
                if (lstBairros.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.PEDIDO_BAIRRO_ENDERECO IN " + "(" + codBairro + ")";
                }

                if (lstIndicador.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.pedido_indicador_id IN " + "(" + codIndicador + ")";
                }

                if (lstVendedor.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.pedido_vendedor_id IN " + "(" + codPessoa + ")";
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



                //if (categoriaId !=0 || GrupoId !=0 || SubGrupoId!=0 || marcaId != 0)
                //{
                //    innerJoin = innerJoin + " inner join pedidosvendasitens on pedidosvendasitens.peditem_pedido_id = pedidosvendas.pedido_id ";
                //    innerJoin = innerJoin + " inner join produtos on pedidosvendasitens.peditem_produto_id = produtos.prod_id ";

                //}

                if (lstCategoria.SelectedItems.Count > 1 || lstMarca.SelectedItems.Count > 1 || lstGrupos.SelectedItems.Count > 1 || lstsubgrupo.SelectedItems.Count > 1)
                {
                    innerJoin = innerJoin + " inner join pedidosvendasitens on pedidosvendasitens.peditem_pedido_id = pedidosvendas.pedido_id ";
                    innerJoin = innerJoin + " inner join produtos on pedidosvendasitens.peditem_produto_id = produtos.prod_id ";
                }


                if (lstCategoria.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_grup_id IN " + "(" + codCategoria + ")";
                }
                if (lstMarca.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_marc_id IN " + "(" + codMarca + ")";
                }
                if (lstGrupos.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_grup_id IN " + "(" + codGrupo + ")";
                }
                if (lstsubgrupo.SelectedItems.Count > 1)
                {
                    sqlWhere = sqlWhere + " AND produtos.prod_subgrp_id IN " + "(" + codSubgrupo + ")";
                }

                if (clienteId != 0)
                {
                    sqlWhere = sqlWhere + " AND pedidosvendas.pedido_cliente_id = " + "'" + clienteId + "'";
                }

                var sql = " select Distinct " +
                        " pedidosvendas.pedido_cliente_id as ClienteId, " +
                        " pessoas.pes_razao, pessoas.pes_insc_federal, " +
                        " (SELECT count(hisat_id) as contador FROM historicosatendimento where hisat_cliente = ClienteId ) as Contador" +

                " FROM  pedidosvendas " +

                    innerJoin +

                    " WHERE " + sqlWhere + "order by  pes_razao ";



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
                while (returnValue.Read())
                {

                    TmkGrid tmkGrid = new TmkGrid();
                    tmkGrid.Seleciona = true;
                    tmkGrid.CodigoCliente = returnValue["ClienteId"].ToInt() != 0 ? returnValue["ClienteId"].ToString() : null;
                    tmkGrid.Cliente = returnValue["pes_razao"].ToString() != "" ? returnValue["pes_razao"].ToString() : null;
                    tmkGrid.Tentativas = returnValue["contador"].ToInt();
                    listaTmkGrid.Add(tmkGrid);
                    contador += 1;
              

                }
               
                gridControl1.DataSource = listaTmkGrid;
                gridControl1.RefreshDataSource();


                _listaTmkGrid = listaTmkGrid;
                txtQtdePedidos.Text = contador.ToString();

                //VerificaContador(dataInicial.ToString(), dataFinal.ToString());
            }
            // _listaTmk = servicoTmk.ConsulteListaParaTMK(cliente, statusTmk, dataInicial, dataFinal, marcaId);



            this.Cursor = Cursors.Default;
        }
        private void VerificaContador(string DataInicial, string DataFinal)
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


          
            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();


                var sql = "Select Distinct LTRIM(PEDIDO_BAIRRO_ENDERECO) as Descricao From pedidosvendas order by LTRIM(PEDIDO_BAIRRO_ENDERECO)";

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                int intNum = 1;
                List<BairroCbo> listaBairro = new List<BairroCbo>();

                lstBairros.Items.Add("");

                while (returnValue.Read())
                {
                    BairroCbo listBairro = new BairroCbo();

                    listBairro.Bairro = returnValue["Descricao"].ToString();
                    listBairro.valor += intNum;
                    listaBairro.Add(listBairro);
                    lstBairros.Items.Add(returnValue["Descricao"].ToString());
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



            string Sql = string.Empty;
            using (var conn = new MySqlConnection(ConectionString))
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
            public int Tentativas { get; set; }

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
        private class ConsultaPorCBO
        {
            public int ID { get; set; }
            public string Descricao { get; set; }


        }
        private class ConsultaTentativaCBO
        {
            public int ID { get; set; }
            public string Descricao { get; set; }


        }


        #endregion

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            //Vai chamar o FormAtendimento para fazer o atendimento selecionado

            var tmk = _listaTmk.Find(x => x.NumPedido == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

            FormAtendimento formAtender = new FormAtendimento(tmk.NumPedido.ToInt(), tmk.DataCompra,tmk.status.ToString(),5, cboCarteiras.EditValue.ToInt(), true);

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


            ServicoTmk servicoTmk = new ServicoTmk();

            using (var conn = new MySqlConnection(ConectionString))
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
                        " historicosatendimento.hisat_status as status,  pessoas.pes_razao, agendamentocontato.Data as Agendamento, " +
                        " historicosatendimento.hisat_novo_pedido_id PedidoNovo, " +
                        "(SELECT count(hisat_id) as contador FROM historicosatendimento where hisat_cliente = ClienteId ) as Contador " +
                " FROM  pedidosvendas " +

                    innerJoin +

                    " WHERE " + sqlWhere + " order by  NumPedido , agendamentocontato.Id DESC, hisat_contador  DESC";



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                int numPedido = 0;
                List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
                contador = 0;

                btnExcluir.Visible = true;

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
                    tmkGrid.NumPedidoNovo = returnValue["PedidoNovo"].ToInt() != 0 ? returnValue["PedidoNovo"].ToInt() : 0;

                    tmkGrid.Status = returnValue["status"].ToInt() != 0 ? returnValue["status"].ToString() : null;
                    int intStatus = returnValue["status"].ToInt();


                    tmkGrid.Status = intStatus != 0 ? tmkGrid.Status.ToString() : null;
                    EnumStatusAtendimento? statusTmk = (EnumStatusAtendimento?)intStatus;
                    tmkGrid.Status = statusTmk.ToString();
                    tmkGrid.Seleciona = true;

                    if (tmkGrid.Status != null)
                    {
                        if (intStatus == 0)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleGreen;
                            tmkGrid.Status = "DISPONIVEL";
                        }

                        else if (intStatus == 1)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleYellow;
                            tmkGrid.Status = "AGENDADO";
                        }
                        else if (intStatus == 2)
                        {
                            if (tmkGrid.NumPedidoNovo != 0)
                            {
                                tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                                tmkGrid.Status = "CONCLUIDO";
                            }
                            else
                            {
                                tmkGrid.Cor = Properties.Resources.CircleGreen;
                                tmkGrid.Status = "DISPONIVEL";
                            }
                        }
                        else if (intStatus == 3)
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
                        else if (intStatus == 4)
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
                    if (_parametros.ParametrosVenda.StatusFaturado != true)
                    {
                        if (tmkGrid.Agendamento != null)
                        {
                            tmkGrid.Cor = Properties.Resources.CircleYellow;
                            tmkGrid.Status = "AGENDADO";
                        }
                        if (tmkGrid.NumPedidoNovo != 0)
                        {
                            tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                            tmkGrid.Status = "CONCLUIDO";
                        }
                    }
                    tmkGrid.Tentativas = returnValue["contador"].ToInt();

                    if (tmkGrid.NumPedidoNovo != 0)
                    {
                        tmkGrid.Tentativas = 0;
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



                //VerificaContadorII(idCarteira);

                VerificaVendedor(idCarteira);







                //this.Cursor = Cursors.WaitCursor;
                //if (idCarteira != 0)
                //{
                //    numCarteira = idCarteira;
                //}

                //btnLimpar.Enabled = true;
                //if (cboCarteiras.Text.ToString() == string.Empty)
                //{
                //    this.Cursor = Cursors.Default;
                //    gcAtendimentos.RefreshDataSource();
                //    return;
                //}

                //int contador = 0;


                //

                //string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

                //ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

                //var item = conexoes.Conexoes[IndiceBancoDados];
                //string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
                //string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
                //string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
                //string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
                //int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                //var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                //if (serverPrincipalOnline)
                //{
                //    ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
                //}
                //else
                //{
                //    ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                //    database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                //    userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                //    senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                //    porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                //    var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                //    if (serverSecundarioOnline)
                //    {
                //        StringConexaoII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                //    }
                //    else
                //    {
                //        //throw new Exception();
                //        //throw new Exception("Servidor de banco de dados não encontrado");
                //    }

                //}


                //ServicoTmk servicoTmk = new ServicoTmk();

                //using (var conn = new MySqlConnection(ConectionString))
                //{
                //    conn.Open();

                //    string sqlWhere = "  pedidosvendas.pedido_status <> 3 ";
                //    string innerJoin = " ";


                //    innerJoin = innerJoin + " left join historicosatendimento on pedidosvendas.pedido_id = historicosatendimento.hisat_pedido_id ";
                //    innerJoin = innerJoin + " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id ";
                //    innerJoin = innerJoin + " left join agendamentocontato on pedidosvendas.pedido_id = agendamentocontato.pedido ";
                //    sqlWhere = sqlWhere + " AND pedidosvendas.PEDIDO_CARTEIRA = " + "'" + idCarteira + "'";


                //    var sql = " select pedidosvendas.pedido_data_elaboracao as DataCompra, " +
                //            " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId, " +
                //            " historicosatendimento.hisat_status as status,  pessoas.pes_razao, agendamentocontato.Data as Agendamento" +

                //    " FROM  pedidosvendas " +

                //        innerJoin +

                //        " WHERE " + sqlWhere + "order by  NumPedido , hisat_contador DESC";



                //    MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                //    MySqlDataReader MyReader2;


                //    var returnValue = MyCommand.ExecuteReader();
                //    int numPedido = 0;
                //    List<TmkGrid> listaTmkGrid = new List<TmkGrid>();
                //    while (returnValue.Read())
                //    {

                //        TmkGrid tmkGrid = new TmkGrid();

                //        tmkGrid.Id = returnValue["NumPedido"].ToInt();


                //        var dt = DateTime.Parse(returnValue["DataCompra"].ToString()).ToString("dd-MM-yyyy");

                //        tmkGrid.DataCompra = dt;

                //        if (returnValue["Agendamento"].ToString() != string.Empty)
                //        {
                //            var dta = DateTime.Parse(returnValue["Agendamento"].ToString()).ToString("dd-MM-yyyy");
                //            if (dta != "01-01-1900")
                //            {
                //                tmkGrid.Agendamento = dta;
                //            }

                //        }


                //        tmkGrid.CodigoCliente = returnValue["ClienteId"].ToInt() != 0 ? returnValue["ClienteId"].ToString() : null;

                //        tmkGrid.Cliente = returnValue["pes_razao"].ToString() != "" ? returnValue["pes_razao"].ToString() : null;
                //        tmkGrid.Status = returnValue["status"].ToInt() != 0 ? returnValue["status"].ToString() : null;
                //        int intStatus = returnValue["status"].ToInt();
                //        tmkGrid.Seleciona = true;




                //        //    if (returnValue["status"].ToString() != null)
                //        //    {
                //        //        if (returnValue["status"].ToInt() == 0)
                //        //        {
                //        //            tmkGrid.Cor = Properties.Resources.CircleGreen;
                //        //            tmkGrid.Status = "DISPONIVEL";
                //        //        }

                //        //        else if (returnValue["status"].ToInt() == 2)
                //        //        {
                //        //            tmkGrid.Cor = Properties.Resources.CircleYellow;
                //        //            tmkGrid.Status = "AGENDADO";
                //        //        }
                //        //        else if (returnValue["status"].ToInt() == 1)
                //        //        {
                //        //            tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                //        //            tmkGrid.Status = "CONCLUIDO";
                //        //        }
                //        //        else if (returnValue["status"].ToInt() == 3)
                //        //        {
                //        //            if (tmkGrid.NumPedidoNovo != 0)
                //        //            {
                //        //                tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                //        //                tmkGrid.Status = "CONCLUIDO";
                //        //            }
                //        //            else
                //        //            {
                //        //                tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                //        //                tmkGrid.Status = "EM ATENDIMENTO";
                //        //            }

                //        //        }
                //        //        else if (returnValue["status"].ToInt() == 4)
                //        //        {
                //        //            tmkGrid.Cor = Properties.Resources.circle_black;
                //        //            tmkGrid.Status = "CANCELADO";
                //        //        }
                //        //        else
                //        //        {
                //        //            tmkGrid.Cor = Properties.Resources.CircleGreen;
                //        //            tmkGrid.Status = "DISPONIVEL";
                //        //        }
                //        //    }



                //        //    if (numPedido != tmkGrid.Id)
                //        //    {
                //        //        listaTmkGrid.Add(tmkGrid);
                //        //        contador += 1;

                //        //    }

                //        //    numPedido = tmkGrid.Id.ToInt();
                //        //}
                //        if (tmkGrid.Status != null)
                //        {
                //            if (intStatus == 0)
                //            {
                //                tmkGrid.Cor = Properties.Resources.CircleGreen;
                //                tmkGrid.Status = "DISPONIVEL";
                //            }

                //            else if (intStatus == 1)
                //            {
                //                tmkGrid.Cor = Properties.Resources.CircleYellow;
                //                tmkGrid.Status = "AGENDADO";
                //            }
                //            else if (intStatus == 2)
                //            {
                //                if (tmkGrid.NumPedidoNovo != 0)
                //                {
                //                    tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                //                    tmkGrid.Status = "CONCLUIDO";
                //                }
                //                else
                //                {
                //                    tmkGrid.Cor = Properties.Resources.CircleGreen;
                //                    tmkGrid.Status = "DISPONIVEL";
                //                }
                //            }
                //            else if (intStatus == 3)
                //            {
                //                if (tmkGrid.NumPedidoNovo != 0)
                //                {
                //                    tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                //                    tmkGrid.Status = "CONCLUIDO";
                //                }
                //                else
                //                {
                //                    tmkGrid.Cor = Properties.Resources.CircleRed16x16;
                //                    tmkGrid.Status = "EM ATENDIMENTO";
                //                }

                //            }
                //            else if (intStatus == 4)
                //            {
                //                tmkGrid.Cor = Properties.Resources.circle_black;
                //                tmkGrid.Status = "CANCELADO";
                //            }
                //            else
                //            {
                //                tmkGrid.Cor = Properties.Resources.CircleGreen;
                //                tmkGrid.Status = "DISPONIVEL";
                //            }
                //        }
                //        if (tmkGrid.Agendamento != null)
                //        {
                //            tmkGrid.Cor = Properties.Resources.CircleYellow;
                //            tmkGrid.Status = "AGENDADO";
                //        }
                //        if (tmkGrid.NumPedidoNovo != 0)
                //        {
                //            tmkGrid.Cor = Properties.Resources.circle_Blue16x16;
                //            tmkGrid.Status = "CONCLUIDO";
                //        }

                //        if (numPedido != tmkGrid.Id)
                //        {
                //            listaTmkGrid.Add(tmkGrid);
                //            contador += 1;
                //        }

                //        numPedido = tmkGrid.Id.ToInt();
                //    }
                //    gcAtendimentos.DataSource = listaTmkGrid;
                //    gcAtendimentos.RefreshDataSource();


                //    _listaTmkGrid = listaTmkGrid;
                //    txtQtdePedidos.Text = contador.ToString();
                //    Novo = true;
                //    VerificaVendedor(idCarteira);
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
           

            {
                using (var conn = new MySqlConnection(ConectionString))

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
                        using (var conn = new MySqlConnection(ConectionString))
                        {

                            conn.Open();

                            string Sql = "update pedidosvendas set PEDIDO_CARTEIRA = " + 0 +
                                         " where pedido_id = " + itemTmk.Id;


                            MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                            MySqlDataReader MyReader2;


                            var returnValue = MyCommand.ExecuteReader();


                        }
                        using (var conn = new MySqlConnection(ConectionString))
                        {

                            conn.Open();

                            string Sql = "update historicosatendimento set hisat_status = " + 0 +
                                         " where hisat_pedido_id = " + itemTmk.Id;


                            MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                            MySqlDataReader MyReader2;


                            var returnValue = MyCommand.ExecuteReader();


                        }
                        using (var conn = new MySqlConnection(ConectionString))
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
            txtNomeCarteira.Enabled = true;
            
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
                    txtNomeCarteira.Enabled = false;
                }
                else
                {
                    btnAtender.Visible = true;
                    btnreagendar.Visible = false;
                    txtNomeCarteira.Enabled = true;
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
                ServicoPessoa servicoPessoa = new ServicoPessoa();
                var cliente = servicoPessoa.ConsultePessoaAtiva(txtIdCliente.Text.ToInt());

                PreenchaCliente(cliente, true);
            }
            else
            {
                PreenchaCliente(null);
            }
        }
      
        private void PreenchaCliente(Pessoa cliente, bool exibirMensagemDeNaoEncontrado = false)
        {
            if (cliente != null)
            {
                txtIdCliente.Text = cliente.Id.ToString();
                txtNomeCliente.Text = cliente.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Cliente nao encontrado!", "Cliente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdCliente.Focus();
                }

                txtIdCliente.Text = string.Empty;
                txtNomeCliente.Text = string.Empty;
            }
        }

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var cliente = formPessoaPesquisa.PesquisePessoaClienteAtiva();

            if (cliente != null)
            {
                PreenchaCliente(cliente);
            }
        }

        private void lstCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
           // PreenchaCboGrupos();
        }

        private void lstGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
           // PreenchaCboSubGrupos();
        }

        private void cbocpfcnpj_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboPor_EditValueChanged(object sender, EventArgs e)
        {
            if (cboPor.ItemIndex == 1)
            {
                gridControl1.Visible = true;
            }
            else
            {
                gridControl1.Visible = false;
            }
        }
    }
}
