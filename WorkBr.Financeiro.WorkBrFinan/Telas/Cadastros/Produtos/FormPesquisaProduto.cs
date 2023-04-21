using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CategoriaServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.MarcaServ;
using Programax.Easy.Servico.Cadastros.CorServ;
using Programax.Easy.Servico.Cadastros.TamanhoServ;
using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using MySql.Data.MySqlClient;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;

namespace Programax.Easy.View.Telas.Cadastros.Produtos
{
    public partial class FormPesquisaProduto : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Produto> _listaDeProdutos;
        private Produto _produtoSelecionado;
        private double quantidadesubestoque = 0;
        private string ConectionString;
        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaProduto()
        {
            InitializeComponent();

            PreenchaOStatus();
            PreenchaCboMarcas();
            PreenchaCboLinhas();
            PreenchaCboGrupos();
            PreenchaCboTipoPesquisa();
            PreenchaCboTamanhos();
            PreenchaCboCores();
            PreenchaCboSexo();
            MostraCampoValorVenda();
            carregaconexao();

            this.ActiveControl = txtChave;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public Produto ExibaPesquisaDeProduto(string ChavePesquisa = "")
        {
            txtChave.Text = ChavePesquisa;

            if(ChavePesquisa != "")
            {
                Pesquise();
            }

            this.ShowDialog();

            return _produtoSelecionado;
        }

        public Produto ExibaPesquisaDeProdutoAtivo()
        {
            cboStatus.EditValue = "A";
            cboStatus.Enabled = false;

            this.ShowDialog();

            return _produtoSelecionado;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void ElementoEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecione();
        }

        private void btnPesquisaProduto_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void gcProdutos_DoubleClick(object sender, EventArgs e)
        {
            if (HouveUmClickOuDuploClickNaLinha(gridView5))
            {
                Selecione();
            }
        }

        private void gcProdutos_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void MostraCampoValorVenda()
        {
            var parametros = new ServicoParametros().ConsulteParametros();

            if (!parametros.ParametrosVenda.PermiteMostrarValorVenda)
            {   
                gridView5.Columns.ColumnByName("ValorVenda").Visible = false;
            }

            if(!parametros.ParametrosCadastros.MostrarGrupoTribPesquisaItens)
            {
                gridView5.Columns.ColumnByName("GrupoTrib").Visible = false;
            }
        }

        private void PreenchaCboTipoPesquisa()
        {
            var listaDeValoresEnumerador = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPesquisa>();

            cboTipoPesquisa.Properties.DataSource = listaDeValoresEnumerador;
            cboTipoPesquisa.Properties.ValueMember = "Valor";
            cboTipoPesquisa.Properties.DisplayMember = "Descricao";

            cboTipoPesquisa.EditValue = EnumTipoPesquisa.DESCRICAO;
        }

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivoOuInativo = new ObjetoParaComboBox();
            objetoComboBoxAtivoOuInativo.Valor = string.Empty;
            objetoComboBoxAtivoOuInativo.Descricao = "Ativo ou Inativo";

            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivoOuInativo);
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = "A";
        }

        private void PreencherGrid()
        {
            
        }

        private void PreenchaCboMarcas()
        {
            ServicoMarca servicoMarca = new ServicoMarca();

            var marcas = servicoMarca.ConsulteListaAtiva();

            marcas.Insert(0, null);

            cboMarcas.Properties.DataSource = marcas;
            cboMarcas.Properties.DisplayMember = "Descricao";
            cboMarcas.Properties.ValueMember = "Id";
        }

        private void PreenchaCboLinhas()
        {
            ServicoCategoria servicoLinha = new ServicoCategoria();

            var linhas = servicoLinha.ConsulteListaAtiva();

            linhas.Insert(0, null);

            cboCategorias.Properties.DataSource = linhas;
            cboCategorias.Properties.DisplayMember = "Descricao";
            cboCategorias.Properties.ValueMember = "Id";
        }

        private void PreenchaCboGrupos()
        {
            ServicoGrupo servicoGrupo = new ServicoGrupo();

            var grupos = servicoGrupo.ConsulteListaAtivos();

            grupos.Insert(0, null);

            cboGrupos.Properties.DataSource = grupos;
            cboGrupos.Properties.DisplayMember = "Descricao";
            cboGrupos.Properties.ValueMember = "Id";
        }

        private void Pesquise()
        {
            EnumSexoProduto? sexo;
            var chave = txtChave.Text;
            var tipoDaPesquisa = (EnumTipoPesquisa)cboTipoPesquisa.EditValue;

            var marca = cboMarcas.EditValue != null ? new Marca { Id = cboMarcas.EditValue.ToInt() } : null;
            var grupo = cboGrupos.EditValue != null ? new Grupo { Id = cboGrupos.EditValue.ToString().ToInt() } : null;
            var categoria = cboCategorias.EditValue != null ? new Categoria { Id = cboCategorias.EditValue.ToString().ToInt() } : null;
            var cor = cboCores.EditValue != null ? new Cor { Id = cboCores.EditValue.ToInt() } : null;
            var tamanho = cboTamanhos.EditValue != null ? new Tamanho { Id = cboTamanhos.EditValue.ToInt() } : null;

            if (cboSexo.EditValue != null)
            {
                sexo = (EnumSexoProduto)cboSexo.EditValue;
            }
            else
            {
                sexo = null;
            }

            string status = cboStatus.EditValue != null ? cboStatus.EditValue.ToString() : string.Empty;

            ServicoProduto servicoProduto = new ServicoProduto();

            if (tipoDaPesquisa == EnumTipoPesquisa.DESCRICAO)
            {
                _listaDeProdutos = servicoProduto.ConsulteListasPelaDescricao(chave, status, marca, categoria, grupo, cor, tamanho, sexo);
            }
            else if (tipoDaPesquisa == EnumTipoPesquisa.CODIGOBARRAS)
            {
                _listaDeProdutos = servicoProduto.ConsulteListaQueContemCodigoBarras(chave, status, marca, categoria, grupo, cor, tamanho, sexo);
            }
            else if (tipoDaPesquisa == EnumTipoPesquisa.SERIALNUMBER)
            {
                _listaDeProdutos = servicoProduto.ConsulteListaQueContemSerialNumber(chave, status, marca, categoria, grupo, cor, tamanho, sexo);
            }
            else if (tipoDaPesquisa == EnumTipoPesquisa.FORNECEDOR)
            {
                var produtoFornecedor = servicoProduto.ConsulteProdutoFornecedorPeloCodigo(chave);

                if (produtoFornecedor != null)
                    _listaDeProdutos = servicoProduto.ConsulteListasPeloCodigo(produtoFornecedor.Produto.Id, status, marca, categoria, grupo, cor, tamanho, sexo);
                else
                    _listaDeProdutos = new List<Produto>();
            }
            
            List<ProdutoAuxiliar> listaDeProdutosParaGrid = new List<ProdutoAuxiliar>();


            foreach (var produto in _listaDeProdutos)
            {
                ProdutoAuxiliar produtoAuxiliar = new ProdutoAuxiliar();
                double EstoqueReservado = 0;
                double EstoqueGeral = 0;
                produtoAuxiliar.Descricao = produto.DadosGerais.Descricao;
                produtoAuxiliar.Id = produto.Id;
                produtoAuxiliar.Marca = produto.Principal != null && produto.Principal.Marca != null ? produto.Principal.Marca.Descricao : string.Empty;
                produtoAuxiliar.Status = produto.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";
                produtoAuxiliar.Unidade = produto.DadosGerais.Unidade != null ? produto.DadosGerais.Unidade.Abreviacao : string.Empty;
                produtoAuxiliar.Categoria = produto.Principal != null && produto.Principal.Categoria != null ? produto.Principal.Categoria.Descricao : string.Empty;
                produtoAuxiliar.Grupo = produto.Principal != null && produto.Principal.Grupo != null ? produto.Principal.Grupo.Descricao : string.Empty;

                produtoAuxiliar.CodigoBarras = produto.DadosGerais.CodigoDeBarras;
                produtoAuxiliar.SerialNumber = produto.Principal != null ? produto.Principal.CodigoFabricante : string.Empty;

                produtoAuxiliar.ValorVenda = produto.FormacaoPreco.ValorVenda.ToString();

                produtoAuxiliar.GrupoTrib = produto.ContabilFiscal.GrupoTributacaoIcms != null ? produto.ContabilFiscal.GrupoTributacaoIcms.Id.ToString() : string.Empty;


                if (produto.FormacaoPreco.EstoqueReservado < 0)
                {
                    EstoqueReservado = 0;
                }
                else
                {
                    EstoqueReservado = produto.FormacaoPreco.EstoqueReservado;
                }
                quantidadesubestoque = 0;
                ConsultaSubEstoque(produto.Id);

                if (produto.FormacaoPreco.EstoqueReservado > produto.FormacaoPreco.Estoque)
                {
                    produtoAuxiliar.Disponivel = produto.FormacaoPreco.Estoque.ToString();
                }
                else
                {
                    EstoqueGeral = (produto.FormacaoPreco.Estoque - quantidadesubestoque - EstoqueReservado);
                    if(EstoqueGeral < 0)
                    {
                        EstoqueGeral = 0;
                    }
                    produtoAuxiliar.Disponivel = (EstoqueGeral).ToString();
                }


                listaDeProdutosParaGrid.Add(produtoAuxiliar);
            }

            gcProdutos.DataSource = listaDeProdutosParaGrid;
            gcProdutos.RefreshDataSource();
        }
        public void ConsultaSubEstoque(int ProdutoId)
        {
            
            using (var conn = new MySqlConnection(ConectionString))
            {
                conn.Open();

                string sqlWhere = "  subestoqueitens_ProdId = " + ProdutoId;
                var sql = " select subestoqueitens_quant From subestoqueitens" +
                    " WHERE " + sqlWhere ;

                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();

                while (returnValue.Read())
                {
                    quantidadesubestoque  += returnValue["subestoqueitens_quant"].ToInt();
                    
                }
            }

            
        }
        private void carregaconexao()
        {
            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesStringII);

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

        }
        public void Selecione()
        {
            _produtoSelecionado = null;

            if (_listaDeProdutos != null && _listaDeProdutos.Count > 0)
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                _produtoSelecionado = servicoProduto.Consulte(colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());
            }

            this.Close();
        }

        private void PreenchaCboTamanhos()
        {
            ServicoTamanho servicoTamanho = new ServicoTamanho();

            var tamanhos = servicoTamanho.ConsulteListaAtiva();

            tamanhos.Insert(0, null);

            cboTamanhos.Properties.DataSource = tamanhos;
            cboTamanhos.Properties.DisplayMember = "Descricao";
            cboTamanhos.Properties.ValueMember = "Id";

            if (cboTamanhos.EditValue != null)
            {
                if (!tamanhos.Exists(tamanho => tamanho != null && tamanho.Id == cboTamanhos.EditValue.ToInt()))
                {
                    cboTamanhos.EditValue = null;
                }
            }
        }

        private void PreenchaCboCores()
        {
            ServicoCor servicoCor = new ServicoCor();

            var cores = servicoCor.ConsulteListaAtiva();

            cores.Insert(0, null);

            cboCores.Properties.DataSource = cores;
            cboCores.Properties.DisplayMember = "Descricao";
            cboCores.Properties.ValueMember = "Id";

            if (cboCores.EditValue != null)
            {
                if (!cores.Exists(cor => cor != null && cor.Id == cboCores.EditValue.ToInt()))
                {
                    cboCores.EditValue = null;
                }
            }
        }

        private void PreenchaCboSexo()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumSexoProduto>();
            lista.Insert(0, null);

            cboSexo.Properties.DataSource = lista;
            cboSexo.Properties.DisplayMember = "Descricao";
            cboSexo.Properties.ValueMember = "Valor";
        }

        #endregion

        #region " ENUMERADORES "

        private enum EnumTipoPesquisa
        {
            [Description("DESCRIÇÃO")]
            DESCRICAO,

            [Description("CÓDIGO DE BARRAS")]
            CODIGOBARRAS,

            [Description("SERIAL NUMBER")]
            SERIALNUMBER,

            [Description("FORNECEDOR")]
            FORNECEDOR
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ProdutoAuxiliar
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

            public string GrupoTrib { get; set; }

            public string Marca { get; set; }

            public string Unidade { get; set; }

            public string Status { get; set; }

            public string Disponivel { get; set; }

            public string Grupo { get; set; }

            public string Categoria { get; set; }

            public string CodigoBarras { get; set; }

            public string SerialNumber { get; set; }

            public string ValorVenda { get; set; }
        }

        #endregion

        private void gcProdutos_Click(object sender, EventArgs e)
        {
            var foto = _listaDeProdutos.Find(x => x.Id == Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

            if(foto.DadosGerais.Foto.ToInt() == 0)
            {
                picFoto.Image = Properties.Resources.produtos;
            }
            else
            {
                picFoto.Image = TratamentoDeImagens.ConvertByteToImagem(foto.DadosGerais.Foto).Image;
            }
        }
    }
}
