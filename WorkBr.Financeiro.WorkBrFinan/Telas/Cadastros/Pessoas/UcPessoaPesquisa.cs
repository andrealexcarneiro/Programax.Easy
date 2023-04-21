using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors.Mask;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Data;

namespace Programax.Easy.View.Telas.Cadastros.Pessoas
{
    public partial class UcPessoaPesquisa : UserControl
    {
        #region " VARIÁVEIS PRIVADAS "

        private ServicoCidade _servicoCidade;
        private Action<Pessoa> _metodoAposASelecaoDoRegistro;
        private Pessoa _pessoaSelecionada;
        private List<Pessoa> _listaDePessoas;
        private int Idcliente = 0;
        private int refiltek = 0;
        private DataSet mDataSet;

        public string ConectionStringII { get; private set; }

        #endregion

        #region " CONSTRUTOR "

        public UcPessoaPesquisa(int intrefiltek )
        {
            InitializeComponent();
            refiltek = intrefiltek;
            _servicoCidade = new ServicoCidade();

            PreenchaOStatus();
            PreenchaOsEstados();
            PreenchaOsFiltros();
            PreenchaCboTipoEndereco();

            this.ActiveControl = txtDescricao;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        

        private void cboEstadoEndereco_EditValueChanged(object sender, EventArgs e)
        {
            string uf = cboEstadoEndereco.EditValue == null ? string.Empty : cboEstadoEndereco.EditValue.ToString();

            var listaDeCidades = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(uf);

            listaDeCidades.Insert(0, null);

            cboCidadeEndereco.Properties.DataSource = listaDeCidades;
            cboCidadeEndereco.Properties.DisplayMember = "Descricao";
            cboCidadeEndereco.Properties.ValueMember = "Id";

            cboCidadeEndereco.EditValue = null;
        }

        private void cboFiltro_EditValueChanged(object sender, EventArgs e)
        {
            txtDescricao.Text = string.Empty;

            var tipoPesquisa = (TipoPesquisa)cboFiltro.EditValue;

            if (tipoPesquisa == TipoPesquisa.RAZAOSOCIAL || tipoPesquisa == TipoPesquisa.NOMEFANTASIA)
            {
                txtDescricao.Properties.Mask.EditMask = string.Empty;
                txtDescricao.Properties.Mask.MaskType = MaskType.None;
            }
            else if (tipoPesquisa == TipoPesquisa.CPF)
            {
                txtDescricao.Properties.Mask.MaskType = MaskType.Simple;
                txtDescricao.Properties.Mask.EditMask = "999.999.999-99";
            }
            else if (tipoPesquisa == TipoPesquisa.CNPJ)
            {
                txtDescricao.Properties.Mask.MaskType = MaskType.Simple;
                txtDescricao.Properties.Mask.EditMask = "99.999.999/9999-99";
            }
            else if (tipoPesquisa == TipoPesquisa.TELEFONE)
            {
                txtDescricao.Properties.Mask.EditMask = string.Empty;
            }
            else
            {
                txtDescricao.Properties.Mask.MaskType = MaskType.Numeric;
                txtDescricao.Properties.Mask.EditMask = "n0";
            }
        }

        private void txtDescricao_Properties_KeyUp(object sender, KeyEventArgs e)
        {
            var tipoPesquisa = (TipoPesquisa)cboFiltro.EditValue;

            if (tipoPesquisa == TipoPesquisa.TELEFONE)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
                {
                    return;
                }

                bool podeAlterarAPosicaoDoCursor = true;

                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    podeAlterarAPosicaoDoCursor = false;
                }

                string numero = txtDescricao.Text;
                int posicaoCursor = txtDescricao.SelectionStart;

                numero = numero.Replace("-", "");

                if (numero.Length == 9)
                {
                    numero = numero.Insert(5, "-");

                    if (podeAlterarAPosicaoDoCursor && posicaoCursor >= 4)
                    {
                        posicaoCursor++;
                    }
                }
                else if (numero.Length > 4)
                {
                    numero = numero.Insert(4, "-");

                    if (podeAlterarAPosicaoDoCursor && posicaoCursor >= 5)
                    {
                        posicaoCursor++;
                    }
                }

                txtDescricao.Text = numero;
                txtDescricao.SelectionStart = posicaoCursor;
            }
        }

        private void pbPesquisaCadastro_Click(object sender, EventArgs e)
        {
            if (refiltek == 0)
            {
                Pesquise();
            }
            else
            {
                PesquiseRefiltek();
            }
          
        }

        private void gcPessoas_DoubleClick(object sender, EventArgs e)
        {
            Point pt = gridView5.GridControl.PointToClient(Control.MousePosition);

            GridHitInfo info = gridView5.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                Selecione();
            }
        }

        private void gcPessoas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Selecione();
            }
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (refiltek == 0)
                {
                    Pesquise();
                }
                else
                {
                    PesquiseRefiltek();
                }
            }
        }

       
        #endregion

        #region " MÉTODOS AUXILARES "

        public void InformarMetodoDeRetornoDoRegistro(Action<Pessoa> metodoAposASelecaoDoRegistro)
        {
            _metodoAposASelecaoDoRegistro = metodoAposASelecaoDoRegistro;
        }

       
        public void Selecione()
        {
            _pessoaSelecionada = null;
             int id = Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId));
           

            if (refiltek == 0)
            {
                if (_listaDePessoas != null && _listaDePessoas.Count > 0)
                {
                    ServicoPessoa servicoPessoa = new ServicoPessoa();

                    _pessoaSelecionada = servicoPessoa.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));
                }
            }
            else
            {
               
                    ServicoPessoa servicoPessoa = new ServicoPessoa();

                           _pessoaSelecionada = servicoPessoa.Consulte(1);

                _pessoaSelecionada.Id = id.ToInt();
            }
           

            _metodoAposASelecaoDoRegistro(_pessoaSelecionada);
          
        }

        private void Pesquise()
        {
            this.Cursor = Cursors.WaitCursor;

            var tipoPesquisa = (TipoPesquisa)cboFiltro.EditValue;

            Estado estado = cboEstadoEndereco.EditValue == null ? null : new Estado { UF = cboEstadoEndereco.EditValue.ToString() };
            Cidade cidade = cboCidadeEndereco.EditValue == null ? null : new Cidade { Id = Convert.ToInt32(cboCidadeEndereco.EditValue) };
            EnumTipoEndereco? tipoEndereco = (EnumTipoEndereco?)cboTipoEndereco.EditValue;

            ServicoPessoa servicoPessoa = new ServicoPessoa();

            if (tipoPesquisa == TipoPesquisa.RAZAOSOCIAL)
            {
                _listaDePessoas = servicoPessoa.ConsulteListaPessoaPelaRazaoSocial(txtDescricao.Text,
                                                                                                                   tipoEndereco,
                                                                                                                   estado,
                                                                                                                   cidade,
                                                                                                                   cboStatus.EditValue.ToString(),
                                                                                                                   chkEhCliente.Checked,
                                                                                                                   chkEhFornecedor.Checked,
                                                                                                                   chkEhFuncionario.Checked,
                                                                                                                   chkEhVendedor.Checked,
                                                                                                                   chkEhSupervisor.Checked,
                                                                                                                   chkEhAtendente.Checked,
                                                                                                                   chkEhTransportadora.Checked,
                                                                                                                   chkEhIndicador.Checked);
            }
            else if (tipoPesquisa == TipoPesquisa.NOMEFANTASIA)
            {
                _listaDePessoas = servicoPessoa.ConsulteListaPessoaPeloNomeFantasia(txtDescricao.Text,
                                                                                                                      tipoEndereco,
                                                                                                                      estado,
                                                                                                                      cidade,
                                                                                                                      cboStatus.EditValue.ToString(),
                                                                                                                      chkEhCliente.Checked,
                                                                                                                      chkEhFornecedor.Checked,
                                                                                                                      chkEhFuncionario.Checked,
                                                                                                                      chkEhVendedor.Checked,
                                                                                                                      chkEhSupervisor.Checked,
                                                                                                                      chkEhAtendente.Checked,
                                                                                                                      chkEhTransportadora.Checked,
                                                                                                                      chkEhIndicador.Checked);
            }
            else if (tipoPesquisa == TipoPesquisa.CNPJ || tipoPesquisa == TipoPesquisa.CPF)
            {
                _listaDePessoas = servicoPessoa.ConsulteListaPessoaPeloCpfCnpj(txtDescricao.Text,
                                                                                                              tipoEndereco,
                                                                                                              estado,
                                                                                                              cidade,
                                                                                                              cboStatus.EditValue.ToString(),
                                                                                                              chkEhCliente.Checked,
                                                                                                              chkEhFornecedor.Checked,
                                                                                                              chkEhFuncionario.Checked,
                                                                                                              chkEhVendedor.Checked,
                                                                                                              chkEhSupervisor.Checked,
                                                                                                              chkEhAtendente.Checked,
                                                                                                              chkEhTransportadora.Checked,
                                                                                                              chkEhIndicador.Checked);
            }
            else if (tipoPesquisa == TipoPesquisa.CODIGO)
            {
                //  _listaDePessoas.Clear();
                _listaDePessoas = servicoPessoa.ConsulteListaPessoaPeloCpfCnpj(txtDescricao.Text,
                                                                                                          tipoEndereco,
                                                                                                          estado,
                                                                                                          cidade,
                                                                                                          cboStatus.EditValue.ToString(),
                                                                                                          chkEhCliente.Checked,
                                                                                                          chkEhFornecedor.Checked,
                                                                                                          chkEhFuncionario.Checked,
                                                                                                          chkEhVendedor.Checked,
                                                                                                          chkEhSupervisor.Checked,
                                                                                                          chkEhAtendente.Checked,
                                                                                                          chkEhTransportadora.Checked,
                                                                                                          chkEhIndicador.Checked);

                if (!string.IsNullOrEmpty(txtDescricao.Text))
                {
                    var pessoa = servicoPessoa.Consulte(Convert.ToInt32(txtDescricao.Text.Replace(".","")));
                    

                    if (pessoa != null)
                    {
                        _listaDePessoas.Add(pessoa);
                    }
                }
            }
            else if (tipoPesquisa == TipoPesquisa.TELEFONE)
            {   
                var _listaDeTelefones = servicoPessoa.ConsulteListaDeTelefones(txtDescricao.Text);
                _listaDePessoas = new List<Pessoa>();
                foreach (var item in _listaDeTelefones)
                {
                    var pessoa = servicoPessoa.Consulte(item.Pessoa.Id);

                    _listaDePessoas.Add(pessoa);
                }
            }

            PreencherGrid(_listaDePessoas);

            this.Cursor = Cursors.Default;
        }
        private void PesquiseRefiltek()

        {

            this.Cursor = Cursors.WaitCursor;

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

            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionStringII))
            {

                conn.Open();

                string sqlWhere = "  pes_razao like '%" + txtDescricao.Text + "%'";
                //                        sqlWhere = sqlWhere + " AND (historicosatendimento.hisat_status = 1 OR historicosatendimento.hisat_status is null)";

                var sql = "select  * " +
                            " FROM  pessoas " +
                    " WHERE " + sqlWhere;



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                var returnValue = MyCommand.ExecuteReader();


                int numPedido = 0;
                int numQtd = 0;
                List<PessoaParaGrid> listaDePessoasParaGrid = new List<PessoaParaGrid>();

                while (returnValue.Read())
                {

                    PessoaParaGrid pessoaParaGrid = new PessoaParaGrid();

                    pessoaParaGrid.Id = returnValue["pes_id"].ToInt();
                    Idcliente = returnValue["pes_id"].ToInt();
                    // pessoaParaGrid.CpfCnpj = returnValue["pes_id"].ToString();
                    pessoaParaGrid.NomeFantasia = returnValue["pes_fantasia"].ToString();
                    pessoaParaGrid.RazaoSocial = returnValue["pes_razao"].ToString();

                    //if (pessoa.ListaDeEnderecos != null && pessoa.ListaDeEnderecos.Count > 0 && pessoa.ListaDeEnderecos[0].Cidade != null)
                    //{
                    //    if (pessoa.ListaDeEnderecos[0].Cidade.Estado != null)
                    //    {
                    //        pessoaParaGrid.UF = pessoa.ListaDeEnderecos[0].Cidade.Estado.UF;
                    //    }
                    //    else
                    //    {
                    //        pessoaParaGrid.UF = string.Empty;
                    //    }

                    //    pessoaParaGrid.Cidade = pessoa.ListaDeEnderecos[0].Cidade.Descricao;
                    //}
                    //else
                    //{
                    //    pessoaParaGrid.UF = string.Empty;

                    //    pessoaParaGrid.Cidade = string.Empty;
                    //}

                    // pessoaParaGrid.Status = pessoa.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";

                    //pessoaParaGrid.EhCliente = returnValue["pes_razao"].ToInt();
                    //pessoaParaGrid.EhFornecedor = pessoa.DadosGerais.EhFornecedor;
                    //pessoaParaGrid.EhTransportadora = pessoa.DadosGerais.EhTransportadora;
                    //pessoaParaGrid.EhFuncionario = pessoa.DadosGerais.EhFuncionario;

                    //if (pessoa.Vendedor != null)
                    //{
                    //    pessoaParaGrid.EhIndicador = pessoa.Vendedor.EhIndicador;
                    //    pessoaParaGrid.EhVendedor = pessoa.Vendedor.EhVendedor;
                    //    pessoaParaGrid.EhSupervisor = pessoa.Vendedor.EhSupervisor;
                    //    pessoaParaGrid.EhAtendente = pessoa.Vendedor.EhAtendente;
                    //}

                    listaDePessoasParaGrid.Add(pessoaParaGrid);
                   
                }

                gcPessoas.DataSource = listaDePessoasParaGrid;
                gcPessoas.RefreshDataSource();
               
            }
        }
        private void PreencherGrid(List<Pessoa> listaDePessoas)
        {
            List<PessoaParaGrid> listaDePessoasParaGrid = new List<PessoaParaGrid>();

            foreach (var pessoa in listaDePessoas)
            {
                PessoaParaGrid pessoaParaGrid = new PessoaParaGrid();

                pessoaParaGrid.Id = pessoa.Id;
                pessoaParaGrid.CpfCnpj = pessoa.DadosGerais.CpfCnpj;
                pessoaParaGrid.NomeFantasia = pessoa.DadosGerais.NomeFantasia;
                pessoaParaGrid.RazaoSocial = pessoa.DadosGerais.Razao;

                if (pessoa.ListaDeEnderecos != null && pessoa.ListaDeEnderecos.Count > 0 && pessoa.ListaDeEnderecos[0].Cidade != null)
                {
                    if (pessoa.ListaDeEnderecos[0].Cidade.Estado != null)
                    {
                        pessoaParaGrid.UF = pessoa.ListaDeEnderecos[0].Cidade.Estado.UF;
                    }
                    else
                    {
                        pessoaParaGrid.UF = string.Empty;
                    }

                    pessoaParaGrid.Cidade = pessoa.ListaDeEnderecos[0].Cidade.Descricao;
                }
                else
                {
                    pessoaParaGrid.UF = string.Empty;

                    pessoaParaGrid.Cidade = string.Empty;
                }

                pessoaParaGrid.Status = pessoa.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";

                pessoaParaGrid.EhCliente = pessoa.DadosGerais.EhCliente;
                pessoaParaGrid.EhFornecedor = pessoa.DadosGerais.EhFornecedor;
                pessoaParaGrid.EhTransportadora = pessoa.DadosGerais.EhTransportadora;
                pessoaParaGrid.EhFuncionario = pessoa.DadosGerais.EhFuncionario;

                if (pessoa.Vendedor != null)
                {
                    pessoaParaGrid.EhIndicador = pessoa.Vendedor.EhIndicador;
                    pessoaParaGrid.EhVendedor = pessoa.Vendedor.EhVendedor;
                    pessoaParaGrid.EhSupervisor = pessoa.Vendedor.EhSupervisor;
                    pessoaParaGrid.EhAtendente = pessoa.Vendedor.EhAtendente;
                }

                listaDePessoasParaGrid.Add(pessoaParaGrid);
            }

            gcPessoas.DataSource = listaDePessoasParaGrid;
            gcPessoas.RefreshDataSource();
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

        private void PreenchaOsEstados()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var listaDeEstados = servicoEstado.ConsulteListaEstados();

            listaDeEstados.Insert(0, null);

            cboEstadoEndereco.Properties.DataSource = listaDeEstados;
            cboEstadoEndereco.Properties.DisplayMember = "Nome";
            cboEstadoEndereco.Properties.ValueMember = "UF";
        }

        private void PreenchaOsFiltros()
        {
            List<ObjetoParaComboBox> listaDeObjetosParaFiltro = new List<ObjetoParaComboBox>();

            ObjetoParaComboBox objetoParaComboBoxRazaoSocial = new ObjetoParaComboBox();
            ObjetoParaComboBox objetoParaComboBoxNomeFantasia = new ObjetoParaComboBox();
            ObjetoParaComboBox objetoParaComboBoxCnpj = new ObjetoParaComboBox();
            ObjetoParaComboBox objetoParaComboBoxCpf = new ObjetoParaComboBox();
            ObjetoParaComboBox objetoParaComboBoxCodigo = new ObjetoParaComboBox();
            ObjetoParaComboBox objetoParaComboTelefone = new ObjetoParaComboBox();

            objetoParaComboBoxRazaoSocial.Descricao = "Razão Social";
            objetoParaComboBoxRazaoSocial.Valor = TipoPesquisa.RAZAOSOCIAL;

            objetoParaComboBoxNomeFantasia.Descricao = "Nome Fantasia";
            objetoParaComboBoxNomeFantasia.Valor = TipoPesquisa.NOMEFANTASIA;

            objetoParaComboBoxCnpj.Descricao = "CNPJ";
            objetoParaComboBoxCnpj.Valor = TipoPesquisa.CNPJ;

            objetoParaComboBoxCpf.Descricao = "CPF";
            objetoParaComboBoxCpf.Valor = TipoPesquisa.CPF;

            objetoParaComboBoxCodigo.Descricao = "Código";
            objetoParaComboBoxCodigo.Valor = TipoPesquisa.CODIGO;

            objetoParaComboTelefone.Descricao = "Telefone";
            objetoParaComboTelefone.Valor = TipoPesquisa.TELEFONE;

            listaDeObjetosParaFiltro.Add(objetoParaComboBoxRazaoSocial);
            listaDeObjetosParaFiltro.Add(objetoParaComboBoxNomeFantasia);
            listaDeObjetosParaFiltro.Add(objetoParaComboBoxCnpj);
            listaDeObjetosParaFiltro.Add(objetoParaComboBoxCpf);
            listaDeObjetosParaFiltro.Add(objetoParaComboBoxCodigo);
            listaDeObjetosParaFiltro.Add(objetoParaComboTelefone);

            cboFiltro.Properties.DataSource = listaDeObjetosParaFiltro;
            cboFiltro.Properties.ValueMember = "Valor";
            cboFiltro.Properties.DisplayMember = "Descricao";

            cboFiltro.EditValue = TipoPesquisa.RAZAOSOCIAL;
        }

        private void PreenchaCboTipoEndereco()
        {
            var listaDeValoresEnumerador = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoEndereco>();
            listaDeValoresEnumerador.Insert(0, null);

            cboTipoEndereco.Properties.DataSource = listaDeValoresEnumerador;
            cboTipoEndereco.Properties.ValueMember = "Valor";
            cboTipoEndereco.Properties.DisplayMember = "Descricao";

            cboTipoEndereco.EditValue = null;
        }
        
        #endregion

        #region " ENUMERADORES "

        private enum TipoPesquisa
        {
            RAZAOSOCIAL,
            NOMEFANTASIA,
            CNPJ,
            CPF,
            CODIGO,
            TELEFONE            
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class PessoaParaGrid
        {
            public int Id { get; set; }

            public string CpfCnpj { get; set; }

            public string RazaoSocial { get; set; }

            public string NomeFantasia { get; set; }

            public string UF { get; set; }

            public string Cidade { get; set; }

            public string Status { get; set; }

            public bool EhCliente { get; set; }

            public bool EhFornecedor { get; set; }

            public bool EhFuncionario { get; set; }

            public bool EhVendedor { get; set; }

            public bool EhSupervisor { get; set; }

            public bool EhAtendente { get; set; }

            public bool EhTransportadora { get; set; }

            public bool EhIndicador { get; set; }
        }


        #endregion

        private void txtDescricao_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gcPessoas_Click(object sender, EventArgs e)
        {

        }
    }
}
