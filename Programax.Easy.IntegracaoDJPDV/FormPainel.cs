using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using System.Threading;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Integracao.TabelasAtualizadasIntegracaoDJServ;
using Programax.Easy.Servico;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Integracao.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.ConfiguracoesPdvObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ConfiguracoesPdvServ;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.UsuarioServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using System.IO;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.RecebimentoServ;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Integracao.PreVendaDjpdvServ;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.IntegracaoDJPDV
{
    public partial class FormPainel : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private Thread _threadCadastros;
        private Thread _threadPreVendas;
        private ConfiguracoesPdv _configuracoesPdv;
        private FormaPagamento _formaPagamentoParcelas;
        private CondicaoPagamento _condicaoPagamentoParcelas;
        private MovimentacaoCaixa _movimentacaoCaixa;

        #endregion

        #region " CONSTRUTOR "

        public FormPainel()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            CarregueConfiguracoesBanco();

            InicieMonitoracao();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void FormPainel_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void notifyIconIntegrador_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {

                Show();
                WindowState = FormWindowState.Normal;
                this.Left = 500;
            }
        }

        private void FormPainel_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void FormPainel_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _threadCadastros.Abort();
                _threadPreVendas.Abort();
            }
            catch (Exception)
            {
            }
        }

        private void btnExportarTodosDados_Click(object sender, EventArgs e)
        {
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "]Iniciando exportação de dados...");

            ServicoTabelasAtualizadasIntegracaoDJ servicoTabelasAtualizadasIntegracaoDJ = new ServicoTabelasAtualizadasIntegracaoDJ();
            servicoTabelasAtualizadasIntegracaoDJ.ExportaTodosDados();

            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "]Exportação de dados concluída");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void ltbLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                StringBuilder texto = new StringBuilder();
                foreach (object o in ltbLog.SelectedItems)
                {
                    texto.AppendLine(o.ToString());
                }
                Clipboard.SetText(texto.ToString());
            }
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void FecharFormulario()
        {
            if (MessageBox.Show("Ao sair do Akill Integrador, sua base de dados não será atualizada.\n\nDeseja sair do Akill Integrador?", "Sair do Akill Integrador", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                base.FecharFormulario();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " MÉTODOS MONITORAÇÃO "

        private void InicieMonitoracao()
        {
            MonitoreDiretorioIntegracao();

            _threadCadastros = new Thread(new ThreadStart(this.InicieThreadMonitoracaoCadastros));
            _threadCadastros.Start();

            _threadPreVendas = new Thread(new ThreadStart(this.InicieThreadMonitoracaoPreVendas));
            _threadPreVendas.Start();
        }

        #region " MONITORAÇÃO DE CADASTROS "

        private void InicieThreadMonitoracaoCadastros()
        {
            CarregueConfiguracoesBanco();
            MonitoreCadastros();
        }

        private void MonitoreCadastros()
        {
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add("MONITORANDO BANCO DE DADOS ...");

            while (true)
            {
                ServicoConfiguracoesPdv servicoConfiguracoesPdv = new ServicoConfiguracoesPdv();

                _configuracoesPdv = servicoConfiguracoesPdv.ConsulteUltimaConfiguracaoPdv();

                var atualizacoes = RetorneListaAtualizacoes();

                EscrevaArquivos(atualizacoes);

                DeleteRegistrosTabelaAtualizacoes(atualizacoes);

                Thread.Sleep(500);
            }
        }

        private void DeleteRegistrosTabelaAtualizacoes(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            ServicoTabelasAtualizadasIntegracaoDJ servicoTabelasAtualizadasIntegracaoDJ = new ServicoTabelasAtualizadasIntegracaoDJ();

            servicoTabelasAtualizadasIntegracaoDJ.ExcluaLista(atualizacoes);
        }

        private List<TabelasAtualizadasIntegracaoDJ> RetorneListaAtualizacoes()
        {
            ServicoTabelasAtualizadasIntegracaoDJ servicoTabelasAtualizadasIntegracaoDJ = new ServicoTabelasAtualizadasIntegracaoDJ();

            return servicoTabelasAtualizadasIntegracaoDJ.ConsulteLista();
        }

        #endregion

        #region " MONITORAÇÃO PRÉ-VENDAS "

        private void InicieThreadMonitoracaoPreVendas()
        {
            CarregueConfiguracoesBanco();
            MonitorePreVendas();
        }

        private void MonitorePreVendas()
        {
            ltbPreVendas.Items.Add(string.Empty);
            ltbPreVendas.Items.Add(string.Empty);
            ltbPreVendas.Items.Add("MONITORANDO BANCO DE DADOS ...");

            while (true)
            {
                ServicoConfiguracoesPdv servicoConfiguracoesPdv = new ServicoConfiguracoesPdv();

                _configuracoesPdv = servicoConfiguracoesPdv.ConsulteUltimaConfiguracaoPdv();

                var preVendas = RetorneListaPreVendas();

                if (preVendas.Count > 0)
                {
                    EscrevaArquivoPreVendas(preVendas);

                    DeleteRegistrosPreVendas(preVendas);
                }

                Thread.Sleep(500);
            }
        }

        private void DeleteRegistrosPreVendas(List<PreVendaDjpdv> atualizacoes)
        {
            ServicoPreVendaDjpdv servicoPreVendaDjpdv = new ServicoPreVendaDjpdv();

            servicoPreVendaDjpdv.ExcluaLista(atualizacoes);
        }

        private List<PreVendaDjpdv> RetorneListaPreVendas()
        {
            ServicoPreVendaDjpdv servicoPreVendaDjpdv = new ServicoPreVendaDjpdv();

            return servicoPreVendaDjpdv.ConsulteLista();
        }

        #endregion

        private void CarregueConfiguracoesBanco()
        {
            ltbLog.Items.Add("CONECTANDO E MAPEANDO BANCO DE DADOS ...");

            StructureMap.ObjectFactory.Initialize(
                   x =>
                   {
                       x.Scan(scan =>
                       {
                           scan.Assembly(typeof(RegistroDeMapeamentos).Assembly);
                           scan.WithDefaultConventions();
                       }
                           );
                       x.AddRegistry<RegistroDeMapeamentos>();
                   });
        }

        #endregion

        #region " MÉTODOS PARA ESCREVER ARQUIVOS DE EXPORTAÇÃO "

        #region " CADASTROS "

        private void EscrevaArquivos(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            if (atualizacoes.Count > 0)
            {
                EscrevaArquivoClientes(atualizacoes);
                EscrevaArquivoVendedores(atualizacoes);
                EscrevaArquivoTransportadoras(atualizacoes);
                EscrevaArquivoProdutos(atualizacoes);
                EscrevaArquivoFormasPagamento(atualizacoes);
                EscrevaArquivoCondicaoPagamento(atualizacoes);
                EscrevaArquivoOperadores(atualizacoes);
            }
        }

        private void EscrevaArquivoClientes(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            var listaAtualizacoesPessoas = atualizacoes.FindAll(x => x.TabelaAtualizada == EnumTabelaAtualizada.PESSOAS).ToList();

            StringBuilder conteudoArquivoPessoa = new StringBuilder();

            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add("COMEÇANDO EXPORTAÇÃO DE CLIENTES");
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);

            ServicoPessoa servicoPessoa = new ServicoPessoa();

            List<int> listaIdsPessoas = new List<int>();
            foreach (var item in listaAtualizacoesPessoas)
            {
                listaIdsPessoas.Add(item.IdRegistro);
            }

            var listaPessoas = servicoPessoa.ConsulteListaPessoasId(listaIdsPessoas);

            bool primeiraIteracao = true;

            foreach (var pessoa in listaPessoas)
            {
                conteudoArquivoPessoa.Append(primeiraIteracao ? string.Empty : "\r\n");

                if (!pessoa.DadosGerais.EhCliente)
                {
                    continue;
                }

                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] EXPORTANDO CLIENTE COM CÓDIGO " + pessoa.Id);

                // 01 - Id Pessoa
                conteudoArquivoPessoa.Append(pessoa.Id.ToString().PadRight(20, ' '));

                // 02 - Tipo Pessoa - Física ou Jurídica
                conteudoArquivoPessoa.Append(pessoa.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAFISICA ? "F" : "J");

                // 03 - Razão Social
                conteudoArquivoPessoa.Append(pessoa.DadosGerais.Razao.PadRight(50, ' ').Substring(0, 50));

                // 04 - Nome Fantasia
                conteudoArquivoPessoa.Append(pessoa.DadosGerais.NomeFantasia.ToStringEmpty().PadRight(30, ' ').Substring(0, 30));

                // 05 - DataCadastro
                conteudoArquivoPessoa.Append(pessoa.DadosGerais.DataCadastro.ToString("ddMMyyyy"));

                // 06 - Data de Nascimento
                conteudoArquivoPessoa.Append(pessoa.DadosPessoais != null && pessoa.DadosPessoais.DataDeNascimento != null ? pessoa.DadosPessoais.DataDeNascimento.Value.ToString("ddMMyyyy") : "00000000");

                // 07 - Última Compra
                conteudoArquivoPessoa.Append("000000000000");

                // 08 - Data Alteração
                conteudoArquivoPessoa.Append("000000000000");

                // 09 - Telefone
                var telefone = pessoa.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.RESIDENCIAL || fone.TipoTelefone == EnumTipoTelefone.COMERCIAL);

                if (telefone != null)
                {
                    string telefoneConcatenado = string.Concat("(", telefone.Ddd, ")", telefone.Numero).PadRight(14, ' ').Substring(0, 14);

                    conteudoArquivoPessoa.Append(telefoneConcatenado);
                }
                else
                {
                    conteudoArquivoPessoa.Append(new string(' ', 14));
                }

                // 10 - Celular
                var celular = pessoa.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.CELULAR || fone.TipoTelefone == EnumTipoTelefone.FAX);

                if (celular != null)
                {
                    string telefoneConcatenado = string.Concat("(", celular.Ddd, ")", celular.Numero).PadRight(14, ' ').Substring(0, 14);

                    conteudoArquivoPessoa.Append(telefoneConcatenado);
                }
                else
                {
                    conteudoArquivoPessoa.Append(new string(' ', 14));
                }

                // 11 - Email
                conteudoArquivoPessoa.Append(pessoa.EmpresaPessoa != null && pessoa.EmpresaPessoa.EmailPrincipal != null ? pessoa.EmpresaPessoa.EmailPrincipal.PadRight(50, ' ').Substring(0, 50) : new string(' ', 50));

                var enderecoPessoa = pessoa.ListaDeEnderecos.FirstOrDefault(endereco => endereco.TipoEndereco.GetValueOrDefault() == EnumTipoEndereco.PRINCIPAL);

                if (enderecoPessoa == null)
                {
                    enderecoPessoa = pessoa.ListaDeEnderecos.FirstOrDefault();
                }

                if (enderecoPessoa != null)
                {
                    // 12 - Endereço ou logradouro
                    conteudoArquivoPessoa.Append(enderecoPessoa.Rua.ToStringEmpty().PadRight(50, ' ').Substring(0, 50));

                    // 13 - Numero
                    conteudoArquivoPessoa.Append(enderecoPessoa.Numero.ToStringEmpty().PadRight(6, ' ').Substring(0, 6));

                    // 14 - Complemento
                    conteudoArquivoPessoa.Append(enderecoPessoa.Complemento.ToStringEmpty().PadRight(30, ' ').Substring(0, 30));

                    // 15 - Bairro
                    conteudoArquivoPessoa.Append(enderecoPessoa.Bairro.ToStringEmpty().PadRight(30, ' ').Substring(0, 30));

                    // 16 - Cidade
                    conteudoArquivoPessoa.Append(enderecoPessoa.Cidade != null ? enderecoPessoa.Cidade.Descricao.PadRight(30, ' ').Substring(0, 30) : new string(' ', 30));

                    // 17 -UF
                    conteudoArquivoPessoa.Append(enderecoPessoa.Cidade != null && enderecoPessoa.Cidade.Estado != null ? enderecoPessoa.Cidade.Estado.UF.PadRight(2, ' ').Substring(0, 2) : "  ");

                    // 18 -CEP
                    conteudoArquivoPessoa.Append(enderecoPessoa.CEP.ToStringEmpty().PadLeft(9, ' '));
                }
                else
                {
                    // 12 - Endereço ou logradouro
                    conteudoArquivoPessoa.Append(new string(' ', 50));

                    // 13 - Numero
                    conteudoArquivoPessoa.Append(new string(' ', 6));

                    // 14 - Complemento
                    conteudoArquivoPessoa.Append(new string(' ', 30));

                    // 15 - Bairro
                    conteudoArquivoPessoa.Append(new string(' ', 30));

                    // 16 - Cidade
                    conteudoArquivoPessoa.Append(new string(' ', 30));

                    // 17 -UF
                    conteudoArquivoPessoa.Append(new string(' ', 2));

                    // 18 -CEP
                    conteudoArquivoPessoa.Append(new string(' ', 9));
                }

                // 19 -Obs Local Entrega
                conteudoArquivoPessoa.Append(new string(' ', 80));

                // 20 - RG ou Inscrição Estadual
                conteudoArquivoPessoa.Append((pessoa.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAJURIDICA ?
                                                                                                                                                        pessoa.EmpresaPessoa != null ? pessoa.EmpresaPessoa.InscricaoEstadual.ToStringEmpty() : string.Empty :
                                                                                                                                                        pessoa.DadosPessoais != null ? pessoa.DadosPessoais.Identidade.ToStringEmpty() : string.Empty).PadRight(20, ' '));

                // 21 - CPF ou CNPJ
                conteudoArquivoPessoa.Append(pessoa.DadosGerais.CpfCnpj.ToStringEmpty().RemoverCaracteresDeMascara().ToStringEmpty().PadRight(20, ' '));

                // 22 - Observação de popup
                conteudoArquivoPessoa.Append(new string(' ', 65));

                // 23 - Observações pessoas
                conteudoArquivoPessoa.Append(pessoa.Atendimento != null ? pessoa.Atendimento.Observacoes.ToStringEmpty().Replace("\r\n", "\\r\\n").PadRight(80, ' ').Substring(0, 80) : new string(' ', 80));

                // 24 - Nível de crédito
                conteudoArquivoPessoa.Append("9");

                // 25 - Nível de crédito
                conteudoArquivoPessoa.Append("0000000000");

                // 26 - Senha
                conteudoArquivoPessoa.Append(new string(' ', 16));

                // 27 - Código classificação do cliente
                conteudoArquivoPessoa.Append("0     ");

                // 28 - Descrição da classificação do cliente
                conteudoArquivoPessoa.Append(new string(' ', 30));

                // 29 - Código Convênio
                conteudoArquivoPessoa.Append(new string('0', 6));

                // 30 - Descrição Convênio
                conteudoArquivoPessoa.Append(new string(' ', 30));

                // 31 - Código Animação
                conteudoArquivoPessoa.Append(new string('0', 6));

                // 32 - Flag ativo ou inativo
                conteudoArquivoPessoa.Append(pessoa.DadosGerais.Status == "A" ? "000001" : "-00001");

                primeiraIteracao = false;
            }

            EscrevaArquivoNoDiretoPdv(conteudoArquivoPessoa, "CLIENTE.txt");

            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add("FIM EXPORTAÇÃO DE CLIENTES");
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);
        }

        private void EscrevaArquivoVendedores(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            var listaAtualizacoesPessoas = atualizacoes.FindAll(x => x.TabelaAtualizada == EnumTabelaAtualizada.PESSOAS).ToList();

            StringBuilder conteudoArquivoPessoa = new StringBuilder();

            ServicoPessoa servicoPessoa = new ServicoPessoa();

            bool primeiraIteracao = true;

            foreach (var atualizacaoPessoa in listaAtualizacoesPessoas)
            {
                conteudoArquivoPessoa.Append(primeiraIteracao ? string.Empty : "\r\n");

                var pessoa = servicoPessoa.Consulte(atualizacaoPessoa.IdRegistro);

                if (pessoa.Vendedor == null || (pessoa.Vendedor != null && !pessoa.Vendedor.EhVendedor))
                {
                    continue;
                }

                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] EXPORTANDO VENDEDOR COM CÓDIGO " + pessoa.Id);

                // 01 - Id Pessoa
                conteudoArquivoPessoa.Append(pessoa.Id.ToString().PadRight(6, ' '));

                // 02 - Nome
                conteudoArquivoPessoa.Append(pessoa.DadosGerais.Razao.PadRight(30, ' ').Substring(0, 30));

                primeiraIteracao = false;
            }

            EscrevaArquivoNoDiretoPdv(conteudoArquivoPessoa, "VENDEDOR.txt");
        }

        private void EscrevaArquivoTransportadoras(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            var listaAtualizacoesPessoas = atualizacoes.FindAll(x => x.TabelaAtualizada == EnumTabelaAtualizada.PESSOAS).ToList();

            StringBuilder conteudoArquivoPessoa = new StringBuilder();

            ServicoPessoa servicoPessoa = new ServicoPessoa();

            bool primeiraIteracao = true;

            foreach (var atualizacaoPessoa in listaAtualizacoesPessoas)
            {
                conteudoArquivoPessoa.Append(primeiraIteracao ? string.Empty : "\r\n");

                var pessoa = servicoPessoa.Consulte(atualizacaoPessoa.IdRegistro);

                if (!pessoa.DadosGerais.EhTransportadora)
                {
                    continue;
                }

                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] EXPORTANDO TRANSPORTADORA COM CÓDIGO " + pessoa.Id);

                // 01 - Id Pessoa
                conteudoArquivoPessoa.Append(pessoa.Id.ToString().PadRight(6, ' '));

                // 02 - CNPJ
                conteudoArquivoPessoa.Append(pessoa.DadosGerais.CpfCnpj.RemoverCaracteresDeMascara().PadRight(14, ' ').Substring(0, 14));

                // 03 - Nome
                conteudoArquivoPessoa.Append(pessoa.DadosGerais.Razao.PadRight(60, ' ').Substring(0, 60));

                var enderecoPessoa = pessoa.ListaDeEnderecos.FirstOrDefault(endereco => endereco.TipoEndereco.GetValueOrDefault() == EnumTipoEndereco.PRINCIPAL);

                if (enderecoPessoa == null)
                {
                    enderecoPessoa = pessoa.ListaDeEnderecos.FirstOrDefault();
                }

                if (enderecoPessoa != null)
                {
                    // 4 - Endereço ou logradouro
                    conteudoArquivoPessoa.Append(enderecoPessoa.Rua.PadRight(60, ' ').Substring(0, 60));

                    // 5 - Numero
                    conteudoArquivoPessoa.Append(enderecoPessoa.Complemento.PadRight(60, ' ').Substring(0, 60));

                    // 6 - Complemento
                    conteudoArquivoPessoa.Append(enderecoPessoa.Complemento.PadRight(60, ' ').Substring(0, 60));

                    // 7 - Bairro
                    conteudoArquivoPessoa.Append(enderecoPessoa.Bairro.PadRight(60, ' ').Substring(0, 60));

                    // 8 - Cidade
                    conteudoArquivoPessoa.Append(enderecoPessoa.Cidade.Descricao.PadRight(30, ' ').Substring(0, 30));

                    // 9 -UF
                    conteudoArquivoPessoa.Append(enderecoPessoa.Cidade.Estado != null ? enderecoPessoa.Cidade.Estado.UF.PadRight(2, ' ').Substring(0, 2) : "  ");

                    // 10 -CEP
                    conteudoArquivoPessoa.Append(enderecoPessoa.CEP.RemoverCaracteresDeMascara().PadLeft(9, ' '));
                }
                else
                {
                    // 4 - Endereço ou logradouro
                    conteudoArquivoPessoa.Append(new string(' ', 60));

                    // 5 - Numero
                    conteudoArquivoPessoa.Append(new string(' ', 60));

                    // 6 - Complemento
                    conteudoArquivoPessoa.Append(new string(' ', 60));

                    // 7 - Bairro
                    conteudoArquivoPessoa.Append(new string(' ', 60));

                    // 8 - Cidade
                    conteudoArquivoPessoa.Append(new string(' ', 30));

                    // 9 -UF
                    conteudoArquivoPessoa.Append(new string(' ', 2));

                    // 10 -CEP
                    conteudoArquivoPessoa.Append(new string(' ', 8));
                }

                // 11 - Telefone
                var telefone = pessoa.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.RESIDENCIAL || fone.TipoTelefone == EnumTipoTelefone.COMERCIAL);

                if (telefone != null)
                {
                    string telefoneConcatenado = string.Concat("(", telefone.Ddd, ")", telefone.Numero).PadRight(14, ' ');

                    conteudoArquivoPessoa.Append(telefoneConcatenado);
                }
                else
                {
                    conteudoArquivoPessoa.Append(new string(' ', 14));
                }

                // 12 - RG ou Inscrição Estadual
                conteudoArquivoPessoa.Append((pessoa.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAJURIDICA ?
                                                                                                                                                        pessoa.EmpresaPessoa != null ? pessoa.EmpresaPessoa.InscricaoEstadual : string.Empty :
                                                                                                                                                        pessoa.DadosPessoais != null ? pessoa.DadosPessoais.Identidade : string.Empty).PadRight(14, ' '));

                // 13 - Email
                conteudoArquivoPessoa.Append(pessoa.EmpresaPessoa != null ? pessoa.EmpresaPessoa.EmailPrincipal.ToStringEmpty().PadRight(60, ' ').Substring(0, 60) : new string(' ', 60));
            }

            EscrevaArquivoNoDiretoPdv(conteudoArquivoPessoa, "TRANSPOR.txt");
        }

        private void EscrevaArquivoProdutos(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            var listaAtualizacoesProdutos = atualizacoes.FindAll(x => x.TabelaAtualizada == EnumTabelaAtualizada.PRODUTOS).ToList();

            StringBuilder conteudoArquivoProduto = new StringBuilder();

            ServicoProduto servicoProduto = new ServicoProduto();

            List<int> listaIdsProdutos = new List<int>();

            foreach (var item in listaAtualizacoesProdutos)
            {
                listaIdsProdutos.Add(item.IdRegistro);
            }

            var listaProdutos = servicoProduto.ConsulteListaPorId(listaIdsProdutos);

            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add("COMEÇANDO EXPORTAÇÃO DE PRODUTOS");
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);

            //string conteudoTxtLog = txtLog.Text + "\r\n\r\nCOMEÇANDO EXPORTAÇÃO DE PRODUTOS\r\n\r\n";

            //txtLog.Text = conteudoTxtLog;

            bool primeiraIteracao = true;

            foreach (var produto in listaProdutos)
            {
                conteudoArquivoProduto.Append(primeiraIteracao ? string.Empty : "\r\n");

                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add("\r\n\r\n[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] EXPORTANDO PRODUTO COM CÓDIGO " + produto.Id);

                //conteudoTxtLog += "\r\n\r\n[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] EXPORTANDO PRODUTO COM CÓDIGO " + produto.Id;

                // 1 - ID
                conteudoArquivoProduto.Append(produto.Id.ToString().PadRight(20, ' '));
                // 2 - Código de Barras
                conteudoArquivoProduto.Append(!string.IsNullOrWhiteSpace(produto.DadosGerais.CodigoDeBarras) ? produto.DadosGerais.CodigoDeBarras.PadRight(20, ' ').Substring(0, 20) : produto.Id.ToString().PadRight(20, ' '));
                // 3 - Descrição
                conteudoArquivoProduto.Append(produto.DadosGerais.Descricao.PadRight(40, ' ').Substring(0, 40));
                // 4 - Complemento
                conteudoArquivoProduto.Append(new string(' ', 20));
                // 5 - Unidade
                conteudoArquivoProduto.Append(produto.DadosGerais.Unidade.Abreviacao.PadRight(4, ' '));
                // 6 - Preço Venda
                conteudoArquivoProduto.Append((produto.FormacaoPreco.EhPromocao.GetValueOrDefault() ? produto.FormacaoPreco.ValorPromocao.GetValueOrDefault() :
                                                                                                                                                 produto.FormacaoPreco.ValorVenda.GetValueOrDefault())
                                                                                                                                                        .ToString("000000000.000").Replace(",", ""));
                // 7 - Desconto
                conteudoArquivoProduto.Append(new string('0', 6));
                // 8 - Situação Tributária
                conteudoArquivoProduto.Append(produto.ContabilFiscal.SituacaoTributariaProduto == null ? "T" :
                                                          produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.ISENTO ? "I" :
                                                          produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.NAOTRIBUTADO ? "N" :
                                                          produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.SUBSTITUICAOTRIBUTARIA ? "F" :
                                                          produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.TRIBUTADAPELOICMS ? "T" :
                                                          produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.TRIBUTADAPELOISSQN ? "S" :
                                                          produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.ISENTOISSQN ? "Q" : " ");

                // 9 - ICMS
                conteudoArquivoProduto.Append(produto.ContabilFiscal.Icms != null ? produto.ContabilFiscal.Icms.Value.ToString("00.00").Replace(",", "") : "1700");
                // 10 - Observação PopUp
                conteudoArquivoProduto.Append(new string(' ', 65));
                // 11 - Calcula Quantidade
                conteudoArquivoProduto.Append("N");
                // 12 - Bloqueia Quantidade Fracionada
                conteudoArquivoProduto.Append(produto.DadosGerais.PermiteVendaFracionada ? "N" : "S");
                // 13 - Bloqueia Quantidade
                conteudoArquivoProduto.Append("N");
                // 14 - Arredonda
                conteudoArquivoProduto.Append("S");
                // 15 - Produção Própria
                conteudoArquivoProduto.Append(produto.ContabilFiscal.NaturezaProduto == EnumNaturezaProduto.FABRICACAOPROPRIA ? "S" : "N");

                // 16 e 17 - Código e Descrição Grupo
                if (produto.Principal.Grupo != null)
                {
                    conteudoArquivoProduto.Append(produto.Principal.Grupo.Id.ToString().PadRight(6, ' '));
                    conteudoArquivoProduto.Append(produto.Principal.Grupo.Descricao.PadRight(30, ' ').Substring(0, 30));
                }
                else
                {
                    conteudoArquivoProduto.Append(new string(' ', 6));
                    conteudoArquivoProduto.Append(new string(' ', 30));
                }

                // 18 - Código Departamento
                conteudoArquivoProduto.Append(new string(' ', 6));
                // 19 - Descrição Departamento
                conteudoArquivoProduto.Append(new string(' ', 30));

                // 20 e 21 - Código e Descrição Marca
                if (produto.Principal.Marca != null)
                {
                    conteudoArquivoProduto.Append(produto.Principal.Marca.Id.ToString().PadRight(6, ' '));
                    conteudoArquivoProduto.Append(produto.Principal.Marca.Descricao.PadRight(30, ' ').Substring(0, 30));
                }
                else
                {
                    conteudoArquivoProduto.Append(new string(' ', 6));
                    conteudoArquivoProduto.Append(new string(' ', 30));
                }

                // 22 - Código Tipo Vasilhame
                conteudoArquivoProduto.Append(new string(' ', 6));
                // 23 - Descrição Tipo Vasilhame
                conteudoArquivoProduto.Append(new string(' ', 30));

                // 24 - Código Animação
                conteudoArquivoProduto.Append("     0");

                // 25 - Flag Ativo ou Inativo
                conteudoArquivoProduto.Append(produto.DadosGerais.Status == "A" ? "000001" : "-00001");

                // 26 - NCM
                conteudoArquivoProduto.Append(produto.ContabilFiscal.Ncm != null ? produto.ContabilFiscal.Ncm.CodigoNcm.PadRight(20, ' ') : new string(' ', 20));

                // 27 - Código Tipo Desconto Adicional
                conteudoArquivoProduto.Append("     0");

                // 28 - GTIN Contabil
                conteudoArquivoProduto.Append(produto.ContabilFiscal != null ? produto.ContabilFiscal.CodigoGtin.PadRight(20, ' ') : new string(' ', 20));
                // 29 - EXT IPI
                conteudoArquivoProduto.Append(new string(' ', 20));
                // 30 - GTIN Tributável
                conteudoArquivoProduto.Append(produto.ContabilFiscal != null ? produto.ContabilFiscal.CodigoGtin.PadRight(20, ' ') : new string(' ', 20));

                // 31 - ID ICMS
                conteudoArquivoProduto.Append(new string(' ', 6));
                // 32 - ID IPI
                conteudoArquivoProduto.Append(new string(' ', 6));
                // 33 - ID ISSQN
                conteudoArquivoProduto.Append(new string(' ', 6));
                // 34 - ID II
                conteudoArquivoProduto.Append(new string(' ', 6));
                // 35 - ID PIS
                conteudoArquivoProduto.Append(new string(' ', 6));
                // 36 - ID PIS ST
                conteudoArquivoProduto.Append(new string(' ', 6));
                // 37 - ID COFINS                
                conteudoArquivoProduto.Append(new string(' ', 6));
                // 38 - ID COFINS ST
                conteudoArquivoProduto.Append(new string(' ', 6));

                // 39 - KIT
                conteudoArquivoProduto.Append('N');
                // 40 - QUANTIDADE ESTOQUE
                conteudoArquivoProduto.Append(produto.FormacaoPreco.Estoque.ToString("000000000.000").Replace(",", ""));

                primeiraIteracao = false;
            }

            EscrevaArquivoNoDiretoPdv(conteudoArquivoProduto, "PRODUTO.txt");

            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add("FIM EXPORTAÇÃO PRODUTOS");
            ltbLog.Items.Add(string.Empty);
            ltbLog.Items.Add(string.Empty);

            //conteudoTxtLog += "\r\n\r\nFIM EXPORTAÇÃO PRODUTOS\r\n\r\n";

            //txtLog.Text = conteudoTxtLog;
        }

        private void EscrevaArquivoFormasPagamento(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            var listaAtualizacoesPessoas = atualizacoes.FindAll(x => x.TabelaAtualizada == EnumTabelaAtualizada.FORMAPAGAMENTO).ToList();

            StringBuilder conteudoArquivoFormaPagamento = new StringBuilder();

            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            bool primeiraIteracao = true;

            foreach (var atualizacaoFormaPagamento in listaAtualizacoesPessoas)
            {
                conteudoArquivoFormaPagamento.Append(primeiraIteracao ? string.Empty : "\r\n");

                var formaPagamento = servicoFormaPagamento.Consulte(atualizacaoFormaPagamento.IdRegistro);

                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] EXPORTANDO FORMA DE PAGAMENTO COM CÓDIGO " + formaPagamento.Id);

                // 01 - Id DJPDV
                conteudoArquivoFormaPagamento.Append(formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO ? "DH" :
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE ? "CH" :
                                                                                                                                            formaPagamento.Id.ToString().PadRight(2, ' '));

                // 02 - Id Forma Pagamento
                conteudoArquivoFormaPagamento.Append(formaPagamento.Id.ToString().PadRight(10, ' '));

                // 03 - Descrição
                conteudoArquivoFormaPagamento.Append(formaPagamento.Descricao.PadRight(20, ' ').Substring(0, 20));

                // 04 - Descrição ECF
                conteudoArquivoFormaPagamento.Append(formaPagamento.Descricao.PadRight(15, ' ').Substring(0, 15));

                // 05 - Reservado
                conteudoArquivoFormaPagamento.Append(new string(' ', 5));

                // 06 - Atalho
                conteudoArquivoFormaPagamento.Append(formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO ? "2" :
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO ? "3" :
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ? "4" :
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE ? "5" :
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.BOLETOBANCARIO ? "6" :
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO ? "7" : "0");

                // 07 - Vendas a Vista
                conteudoArquivoFormaPagamento.Append(formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO ||
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.BOLETOBANCARIO ||
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO ||
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE ||
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ||
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO ||
                                                                                                                                            formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDITOINTERNO ? "S" : "N");

                // 08 - Recebimento
                conteudoArquivoFormaPagamento.Append("S");
                // 09 - Debitos Caixa
                conteudoArquivoFormaPagamento.Append("S");
                // 10 - Creditos Caixa
                conteudoArquivoFormaPagamento.Append("S");

                // 11 - Aprovação
                conteudoArquivoFormaPagamento.Append(formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ||
                                                                                                                                           formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO ?
                                                                                                                                                _configuracoesPdv.TipoCartaoCreditoDebito == EnumTipoCartaoCreditoEDebito.TEFDIALREDECARDVISAAMEX ? "L" :
                                                                                                                                                _configuracoesPdv.TipoCartaoCreditoDebito == EnumTipoCartaoCreditoEDebito.TEFDISCTECBAN ? "C" :
                                                                                                                                                _configuracoesPdv.TipoCartaoCreditoDebito == EnumTipoCartaoCreditoEDebito.TEFSOROCRED ? "S" :
                                                                                                                                                _configuracoesPdv.TipoCartaoCreditoDebito == EnumTipoCartaoCreditoEDebito.TEFGOODCARDGET ? "G" :
                                                                                                                                                _configuracoesPdv.TipoCartaoCreditoDebito == EnumTipoCartaoCreditoEDebito.TEFULTRACARD ? "U" :
                                                                                                                                                _configuracoesPdv.TipoCartaoCreditoDebito == EnumTipoCartaoCreditoEDebito.TEFHIPERTEF ? "H" :
                                                                                                                                                _configuracoesPdv.TipoCartaoCreditoDebito == EnumTipoCartaoCreditoEDebito.TEFCLISITEF ? "D" :
                                                                                                                                                _configuracoesPdv.TipoCartaoCreditoDebito == EnumTipoCartaoCreditoEDebito.INFORMARPAGAMENTOCOMPOS ? "P" : "N" : "N");

                // 12 - Tipo Troco
                conteudoArquivoFormaPagamento.Append(formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO ? "D" : " ");

                // 13 - Valor Minimo
                conteudoArquivoFormaPagamento.Append(new string('0', 12));

                // 14 - Valor Maximo
                conteudoArquivoFormaPagamento.Append(new string('0', 12));

                primeiraIteracao = false;
            }

            EscrevaArquivoNoDiretoPdv(conteudoArquivoFormaPagamento, "FPAGTO.txt");
        }

        private void EscrevaArquivoCondicaoPagamento(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            var listaAtualizacoesCondicaoPagamento = atualizacoes.FindAll(x => x.TabelaAtualizada == EnumTabelaAtualizada.CONDICAOPAGAMENTO).ToList();

            StringBuilder conteudoArquivoCondicaoPagamento = new StringBuilder();

            ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();

            bool primeiraIteracao = true;

            foreach (var atualizacaoCondicaoPagamento in listaAtualizacoesCondicaoPagamento)
            {
                conteudoArquivoCondicaoPagamento.Append(primeiraIteracao ? string.Empty : "\r\n");

                var condicaoPagamento = servicoCondicaoPagamento.Consulte(atualizacaoCondicaoPagamento.IdRegistro);

                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] EXPORTANDO CONDIÇÃO DE PAGAMENTO COM CÓDIGO " + condicaoPagamento.Id);

                // 01 - Codigo Interno
                if (condicaoPagamento.CondicaoPadraoAVista)
                {
                    conteudoArquivoCondicaoPagamento.Append("AV");
                }
                else
                {
                    conteudoArquivoCondicaoPagamento.Append(condicaoPagamento.Id.ToString().PadRight(2, ' ').Substring(0, 2));
                }

                // 02 - Codigo Externo
                conteudoArquivoCondicaoPagamento.Append(condicaoPagamento.Id.ToString().PadRight(10, ' ').Substring(0, 10));

                // 03 - Descrição
                conteudoArquivoCondicaoPagamento.Append(condicaoPagamento.Descricao.PadRight(20, ' ').Substring(0, 20));

                // 04 - Dias Para Entrada
                conteudoArquivoCondicaoPagamento.Append(condicaoPagamento.ListaDeParcelas.FirstOrDefault().Dias.ToString("000"));

                // 05 - Percentual Entrada
                conteudoArquivoCondicaoPagamento.Append(condicaoPagamento.ListaDeParcelas.FirstOrDefault().PercentualRateio.ToString("000.000").Replace(",", ""));

                // 06 - Quantidade de Parcelas
                conteudoArquivoCondicaoPagamento.Append(condicaoPagamento.ListaDeParcelas.Count.ToString("000"));

                if (condicaoPagamento.ListaDeParcelas.Count > 1)
                {
                    var diasEntreParcelas = condicaoPagamento.ListaDeParcelas[1].Dias - condicaoPagamento.ListaDeParcelas[0].Dias;

                    // 07 - Intervalo Entre Parcelas
                    conteudoArquivoCondicaoPagamento.Append(diasEntreParcelas.ToString("000"));
                }
                else
                {
                    // 07 - Intervalo Entre Parcelas
                    conteudoArquivoCondicaoPagamento.Append("000");
                }

                // 08 - Manter Dia de Vencimento
                conteudoArquivoCondicaoPagamento.Append("N");

                // 09 - Exige cheque
                conteudoArquivoCondicaoPagamento.Append("N");

                // 10 - Prazo Médio Máximo
                conteudoArquivoCondicaoPagamento.Append("0000");

                // 11 - Desconto Mínimo
                conteudoArquivoCondicaoPagamento.Append("00000");

                // 12 - Desconto Máximo                
                conteudoArquivoCondicaoPagamento.Append(_configuracoesPdv.DescontoMaximoCaixa.ToString("00.000").Replace(",", ""));

                // 13 - Acréscimo Mínimo
                conteudoArquivoCondicaoPagamento.Append("000000");

                // 14 - Acréscimo Máximo
                conteudoArquivoCondicaoPagamento.Append("000000");

                // 15 - Utilizar Desconto do Produto
                conteudoArquivoCondicaoPagamento.Append("N");

                // 16 - Tipo de Restrição para Forma Pagamento
                conteudoArquivoCondicaoPagamento.Append("T");

                // 17 - Lista Formas Pagamentos
                conteudoArquivoCondicaoPagamento.Append(new string(' ', 30));

                primeiraIteracao = false;
            }

            EscrevaArquivoNoDiretoPdv(conteudoArquivoCondicaoPagamento, "PLAPAGTO.txt");
        }

        private void EscrevaArquivoOperadores(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            var listaAtualizacoesCaixa = atualizacoes.FindAll(x => x.TabelaAtualizada == EnumTabelaAtualizada.OPERADOR).ToList();

            var listaAtualizacoesPessoas = atualizacoes.FindAll(x => x.TabelaAtualizada == EnumTabelaAtualizada.PESSOAS).ToList();

            var listaAtualizacoesUsuarios = atualizacoes.FindAll(x => x.TabelaAtualizada == EnumTabelaAtualizada.USUARIOS).ToList();

            StringBuilder conteudoArquivoOperadores = new StringBuilder();

            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();

            Empresa empresa = null;

            ServicoCaixa servicoCaixa = new ServicoCaixa();
            ServicoUsuario servicoUsuario = new ServicoUsuario();

            foreach (var atualizacaoPessoa in listaAtualizacoesPessoas)
            {
                var caixa = servicoCaixa.ConsultePeloFuncionario(new Pessoa { Id = atualizacaoPessoa.IdRegistro });

                if (caixa != null)
                {
                    listaAtualizacoesCaixa.Add(new TabelasAtualizadasIntegracaoDJ { IdRegistro = caixa.Id });
                }
            }

            foreach (var atualizacaoUsuario in listaAtualizacoesUsuarios)
            {
                var caixa = servicoCaixa.ConsultePeloFuncionario(new Pessoa { Id = atualizacaoUsuario.IdRegistro });

                if (caixa != null)
                {
                    listaAtualizacoesCaixa.Add(new TabelasAtualizadasIntegracaoDJ { IdRegistro = caixa.Id });
                }
            }

            bool primeiraIteracao = true;

            foreach (var atualizacaoCaixa in listaAtualizacoesCaixa)
            {
                if (empresa == null)
                {
                    empresa = servicoEmpresa.ConsulteUltimaEmpresa();
                }

                conteudoArquivoOperadores.Append(primeiraIteracao ? string.Empty : "\r\n");

                var caixa = servicoCaixa.Consulte(atualizacaoCaixa.IdRegistro);

                var usuario = servicoUsuario.Consulte(caixa.Funcionario.Id);

                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add(string.Empty);
                ltbLog.Items.Add("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] EXPORTANDO CAIXA COM CÓDIGO " + caixa.Id);

                // 01 - CNPJ Empresa
                conteudoArquivoOperadores.Append(empresa.DadosEmpresa.Cnpj.RemoverCaracteresDeMascara().PadRight(14, ' ').Substring(0, 14));

                // 02 - Código Externo
                conteudoArquivoOperadores.Append(caixa.Id.ToString().PadRight(20, ' '));

                // 03 - Login
                conteudoArquivoOperadores.Append(usuario.Login.PadRight(10, ' ').Substring(0, 10));

                // 04 - Nome
                conteudoArquivoOperadores.Append(caixa.Funcionario.DadosGerais.Razao.PadRight(30, ' ').Substring(0, 30));

                // 05 - Telefone
                var telefone = caixa.Funcionario.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.RESIDENCIAL);
                string numeroTelefone = telefone != null ? "(" + telefone.Ddd + ")" + telefone.Numero : new string(' ', 14);
                conteudoArquivoOperadores.Append(numeroTelefone.PadRight(14, ' ').Substring(0, 14));

                // 06 - Celular
                var celular = caixa.Funcionario.ListaDeTelefones.FirstOrDefault(x => x.TipoTelefone == EnumTipoTelefone.CELULAR);
                string numeroCelular = celular != null ? "(" + celular.Ddd + ")" + celular.Numero : new string(' ', 14);
                conteudoArquivoOperadores.Append(numeroCelular.PadRight(14, ' ').Substring(0, 14));

                // 07 - Email
                conteudoArquivoOperadores.Append(caixa.Funcionario.EmpresaPessoa != null ? caixa.Funcionario.EmpresaPessoa.EmailPrincipal.PadRight(50, ' ').Substring(0, 50) : new string(' ', 50));

                // 08 - Observação
                conteudoArquivoOperadores.Append(caixa.Funcionario.Atendimento.Observacoes.PadRight(80, ' ').Substring(0, 80));

                // 09 - ID perfil
                conteudoArquivoOperadores.Append("     ");
                conteudoArquivoOperadores.Append(caixa.PerfilCaixa != null ? ((int)caixa.PerfilCaixa.Value).ToString() : " ");

                // 10- Ação, vencimento, senha
                conteudoArquivoOperadores.Append(caixa.Status == "A" ? "I" : "B");

                primeiraIteracao = false;
            }

            EscrevaArquivoNoDiretoPdv(conteudoArquivoOperadores, "OPERADOR.txt");
        }

        #endregion

        #region " PRÉ-VENDAS "

        private void EscrevaArquivoPreVendas(List<PreVendaDjpdv> listaPreVendas)
        {
            StringBuilder conteudoPreVendas = new StringBuilder();

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            foreach (var prevenda in listaPreVendas)
            {
                var pedidoDeVenda = servicoPedidoDeVenda.Consulte(prevenda.PedidoDeVendaId);

                EscrevaPreVendaPRE(pedidoDeVenda, conteudoPreVendas);
                EscrevaPreVendaPIT(pedidoDeVenda, conteudoPreVendas);
                EscrevaPreVendaPPA(pedidoDeVenda, conteudoPreVendas);
                EscrevaPreVendaPFP(pedidoDeVenda, conteudoPreVendas);

                ltbPreVendas.Items.Add("\r\n\r\n[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] PRÉ-VENDA " + pedidoDeVenda.Id + " EXPORTADA");
            }

            EscrevaArquivoNoDiretoPdv(conteudoPreVendas, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".djp");
        }

        private void EscrevaPreVendaPRE(PedidoDeVenda pedidoDeVenda, StringBuilder conteudoPreVendas)
        {
            if (conteudoPreVendas.Length > 0)
            {
                conteudoPreVendas.AppendLine(string.Empty);
            }

            //01 - Tipo Registro
            conteudoPreVendas.Append("PRE|");

            //02 - Número Pré-Venda
            conteudoPreVendas.Append(pedidoDeVenda.Id);
            conteudoPreVendas.Append("|");

            //03 - Data Hora de Emissão
            conteudoPreVendas.Append(DateTime.Now.ToString("ddMMyyyyHHMMss"));
            conteudoPreVendas.Append("|");

            //04 - Código_Externo Cliente
            conteudoPreVendas.Append(pedidoDeVenda.Cliente.Id);
            conteudoPreVendas.Append("|");

            //05 - Nome Cliente
            conteudoPreVendas.Append(pedidoDeVenda.Cliente.DadosGerais.Razao);
            conteudoPreVendas.Append("|");

            //06 - CPF\CNPJ do Cliente
            conteudoPreVendas.Append(pedidoDeVenda.Cliente.DadosGerais.CpfCnpj);
            conteudoPreVendas.Append("|");

            // TODO: Futuramente se necessário, informar planos de pagamentos
            //07 - Código_Externo Plano Pagto.
            conteudoPreVendas.Append(" ");
            //conteudoPreVendas.Append(pedidoDeVenda.CondicaoPagamento.Id);
            conteudoPreVendas.Append("|");

            //08 - Subtotal
            conteudoPreVendas.Append(pedidoDeVenda.ValorTotal.ToString("0.00").Replace(",", "."));
            conteudoPreVendas.Append("|");

            //09 - ValorDesconto
            conteudoPreVendas.Append("0.00");
            conteudoPreVendas.Append("|");

            //10 - Valor Acrescimo
            //conteudoPreVendas.Append(pedidoDeVenda.ValorFrete.ToString("0.00").Replace(",", "."));
            conteudoPreVendas.Append("0.00");
            conteudoPreVendas.Append("|");

            //11 - Total de Itens
            conteudoPreVendas.Append(pedidoDeVenda.ListaItens.Count.ToString("0.000").Replace(",", "."));
            conteudoPreVendas.Append("|");

            //12 - Código_Externo Vendedor
            conteudoPreVendas.Append(pedidoDeVenda.Vendedor != null ? pedidoDeVenda.Vendedor.Id.ToString() : " ");
            conteudoPreVendas.Append("|");

            //13 - Observações
            conteudoPreVendas.Append("|");

            //14 - RG_Inscrição Estadual
            conteudoPreVendas.Append(" |");

            //15 - Endereço
            conteudoPreVendas.Append(" |");

            //16 - Número
            conteudoPreVendas.Append(" |");

            //17 - Complemento
            conteudoPreVendas.Append(" |");

            //18 - Bairro
            conteudoPreVendas.Append(" |");

            //19 - Cidade
            conteudoPreVendas.Append(" |");

            //20 - UF
            conteudoPreVendas.Append(" |");

            //21 - CEP
            conteudoPreVendas.Append(" |");

            //22 - Nível Crédito
            conteudoPreVendas.Append("0|");
        }

        private void EscrevaPreVendaPIT(PedidoDeVenda pedidoDeVenda, StringBuilder conteudoPreVendas)
        {
            int numeroItem = 1;

            foreach (var item in pedidoDeVenda.ListaItens)
            {
                conteudoPreVendas.AppendLine(string.Empty);

                //01 - Tipo Registro
                conteudoPreVendas.Append("PIT|");

                //02 - Sequência
                conteudoPreVendas.Append(numeroItem);
                conteudoPreVendas.Append("|");

                //03 - Código_Externo Produto
                conteudoPreVendas.Append(item.Produto.Id);
                conteudoPreVendas.Append("|");

                //04 - Quantidade
                conteudoPreVendas.Append(item.Quantidade.ToString("0.000").Replace(",", "."));
                conteudoPreVendas.Append("|");

                //05 - Preco Unitário
                conteudoPreVendas.Append(item.ValorUnitario.ToString("0.000").Replace(",", "."));
                conteudoPreVendas.Append("|");

                //06 - Desconto
                conteudoPreVendas.Append(item.TotalDesconto.ToString("0.00").Replace(",", "."));
                conteudoPreVendas.Append("|");

                //07 - Acrescimo
                conteudoPreVendas.Append(item.ValorFrete.ToString("0.00").Replace(",", "."));
                conteudoPreVendas.Append("|");

                //08 - Total Líquido
                conteudoPreVendas.Append(item.ValorTotal.ToString("0.00").Replace(",", "."));
                conteudoPreVendas.Append("|");

                //09 - Código Barras
                conteudoPreVendas.Append(!string.IsNullOrEmpty(item.Produto.DadosGerais.CodigoDeBarras) ? item.Produto.DadosGerais.CodigoDeBarras : item.Produto.Id.ToString());
                conteudoPreVendas.Append("|");

                //10 - Descrição
                conteudoPreVendas.Append(item.Produto.DadosGerais.Descricao);
                conteudoPreVendas.Append("|");

                //11 - Complemento
                conteudoPreVendas.Append(" ");
                conteudoPreVendas.Append("|");

                //12 - Unidade de Medida
                conteudoPreVendas.Append(item.Produto.DadosGerais.Unidade.Abreviacao);
                conteudoPreVendas.Append("|");

                // 13 - Situação Tributária
                conteudoPreVendas.Append(item.Produto.ContabilFiscal.SituacaoTributariaProduto == null ? "T" :
                                                          item.Produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.ISENTO ? "I" :
                                                          item.Produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.NAOTRIBUTADO ? "N" :
                                                          item.Produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.SUBSTITUICAOTRIBUTARIA ? "F" :
                                                          item.Produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.TRIBUTADAPELOICMS ? "T" :
                                                          item.Produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.TRIBUTADAPELOISSQN ? "S" :
                                                          item.Produto.ContabilFiscal.SituacaoTributariaProduto == EnumSituacaoTributariaProduto.ISENTOISSQN ? "Q" : " ");
                conteudoPreVendas.Append("|");

                // 14 - ICMS
                conteudoPreVendas.Append(item.Produto.ContabilFiscal.Icms != null ? item.Produto.ContabilFiscal.Icms.Value.ToString("00.00").Replace(",", ".") : "17.00");
                conteudoPreVendas.Append("|");

                // 15 - Calcula Quantidade
                conteudoPreVendas.Append("N");
                conteudoPreVendas.Append("|");

                // 16 - Bloqueia Quantidade Fracionada
                conteudoPreVendas.Append(item.Produto.DadosGerais.PermiteVendaFracionada ? "N" : "S");
                conteudoPreVendas.Append("|");

                // 17 - Bloqueia Quantidade
                conteudoPreVendas.Append("N");
                conteudoPreVendas.Append("|");

                // 18 - Produção Própria
                conteudoPreVendas.Append(item.Produto.ContabilFiscal.NaturezaProduto == EnumNaturezaProduto.FABRICACAOPROPRIA ? "S" : "N");
                conteudoPreVendas.Append("|");

                // 19 - QUANTIDADE ESTOQUE
                conteudoPreVendas.Append(item.Produto.FormacaoPreco.Estoque.ToString("0.000").Replace(",", "."));
                conteudoPreVendas.Append("|");

                // 20 - Descrição Adicional
                conteudoPreVendas.Append(" ");
                conteudoPreVendas.Append("|");

                // 21 - Código Vendedor
                conteudoPreVendas.Append(pedidoDeVenda.Vendedor != null ? pedidoDeVenda.Vendedor.Id.ToString() : " ");
                conteudoPreVendas.Append("|");

                // 22 - Nome Vendedor
                conteudoPreVendas.Append(pedidoDeVenda.Vendedor != null ? pedidoDeVenda.Vendedor.DadosGerais.Razao : " ");
                conteudoPreVendas.Append("|");

                // 23 - GTIN Contabil
                conteudoPreVendas.Append(item.Produto.ContabilFiscal != null && !string.IsNullOrEmpty(item.Produto.ContabilFiscal.CodigoGtin) ? item.Produto.ContabilFiscal.CodigoGtin : " ");
                conteudoPreVendas.Append("|");

                // 24 - EXT IPI
                conteudoPreVendas.Append(" ");
                conteudoPreVendas.Append("|");

                // 25 - GTIN Tributável
                conteudoPreVendas.Append(item.Produto.ContabilFiscal != null && !string.IsNullOrEmpty(item.Produto.ContabilFiscal.CodigoGtin) ? item.Produto.ContabilFiscal.CodigoGtin : " ");
                conteudoPreVendas.Append("|");

                // 26 - ID ICMS
                conteudoPreVendas.Append("0");
                conteudoPreVendas.Append("|");

                // 27 - ID IPI
                conteudoPreVendas.Append("0");
                conteudoPreVendas.Append("|");

                // 28 - ID ISSQN
                conteudoPreVendas.Append("0");
                conteudoPreVendas.Append("|");

                // 29 - ID II
                conteudoPreVendas.Append("0");
                conteudoPreVendas.Append("|");

                // 30 - ID PIS
                conteudoPreVendas.Append("0");
                conteudoPreVendas.Append("|");

                // 31 - ID PIS ST
                conteudoPreVendas.Append("0");
                conteudoPreVendas.Append("|");

                // 32 - ID COFINS                
                conteudoPreVendas.Append("0");
                conteudoPreVendas.Append("|");

                // 33 - ID COFINS ST
                conteudoPreVendas.Append("0");
                conteudoPreVendas.Append("|");

                // 34 - NCM
                conteudoPreVendas.Append(item.Produto.ContabilFiscal.Ncm != null ? item.Produto.ContabilFiscal.Ncm.CodigoNcm : " ");
                conteudoPreVendas.Append("|");

                numeroItem++;
            }
        }

        private void EscrevaPreVendaPPA(PedidoDeVenda pedidoDeVenda, StringBuilder conteudoPreVendas)
        {
            //foreach (var item in pedidoDeVenda.ListaParcelasPedidoDeVenda)
            //{
            //    conteudoPreVendas.AppendLine(string.Empty);

            //    // 01 - Tipo Registro
            //    conteudoPreVendas.Append("PPA|");

            //    // 02 - Vencimento
            //    conteudoPreVendas.Append(item.DataVencimento.ToString("ddMMyyyy"));
            //    conteudoPreVendas.Append("|");

            //    // 03 - Valor
            //    conteudoPreVendas.Append(item.Valor.ToString("0.00").Replace(",", "."));
            //    conteudoPreVendas.Append("|");

            //    // 04 - Valor
            //    conteudoPreVendas.Append(" ");
            //    conteudoPreVendas.Append("|");
            //}
        }

        private void EscrevaPreVendaPFP(PedidoDeVenda pedidoDeVenda, StringBuilder conteudoPreVendas)
        {
            if (!_configuracoesPdv.FormaPagamentoEntradaIgualPedidoVenda)
            {
                return;
            }

            List<PreVendaPFP> listaPrevendaPFP = new List<PreVendaPFP>();

            foreach (var item in pedidoDeVenda.ListaParcelasPedidoDeVenda)
            {
                var preVendaPFP = listaPrevendaPFP.FirstOrDefault(pfp => pfp.formaPagamentoId == item.FormaPagamento.Id);

                if (item.DataVencimento.Date <= DateTime.Now.Date)
                {
                    if (preVendaPFP == null)
                    {
                        preVendaPFP = new PreVendaPFP();

                        listaPrevendaPFP.Add(preVendaPFP);
                    }

                    preVendaPFP.formaPagamentoId = item.FormaPagamento.Id;
                    preVendaPFP.ValorFormaPagamento += item.Valor;
                }
            }

            foreach (var item in listaPrevendaPFP)
            {
                conteudoPreVendas.AppendLine(string.Empty);

                // 01 - Tipo Registro
                conteudoPreVendas.Append("PFP|");

                // 02 - Identificação da forma de pagamento
                conteudoPreVendas.Append(item.formaPagamentoId);
                conteudoPreVendas.Append("|");

                // 03 - Valor
                conteudoPreVendas.Append(item.ValorFormaPagamento.ToString("0.00").Replace(",", "."));
                conteudoPreVendas.Append("|");
            }
        }

        #endregion

        private void EscrevaArquivoNoDiretoPdv(StringBuilder conteudoArquivo, string nomeArquivo)
        {
            if (!string.IsNullOrWhiteSpace(conteudoArquivo.ToString()))
            {
                string diretorioPadraoPDV = @"C:\Programax\PDV\";

                string caminhoCompletoArquivo = diretorioPadraoPDV + nomeArquivo;

                bool primeiraVerificacao = true;

                while (File.Exists(caminhoCompletoArquivo))
                {
                    if (primeiraVerificacao)
                    {
                        ltbLog.Items.Add(string.Empty);
                        ltbLog.Items.Add(string.Empty);
                        ltbLog.Items.Add("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "] AGUARDANDO DJINTEGRAÇÃO LER ARQUIVO " + nomeArquivo + " ...");
                    }

                    primeiraVerificacao = false;

                    Thread.Sleep(50);
                }

                System.IO.File.WriteAllText(caminhoCompletoArquivo, conteudoArquivo.ToString());
            }
        }

        #endregion

        #region " MÉTODOS DE LEITURA DO ARQUIVO DE IMPORTAÇÃO "

        private void MonitoreDiretorioIntegracao()
        {
            System.IO.FileSystemWatcher watcherPDV = new System.IO.FileSystemWatcher();
            watcherPDV.Path = @"C:\Programax\PDV";
            watcherPDV.Filter = "*.djm";
            watcherPDV.IncludeSubdirectories = false;
            watcherPDV.EnableRaisingEvents = true;

            watcherPDV.Created += new FileSystemEventHandler(EventoAoCriarArquivoDeExportacao);

            DirectoryInfo informacoesDiretorio = new DirectoryInfo(@"C:\Programax\PDV");

            FileInfo[] arquivosDjm = informacoesDiretorio.GetFiles("*.djm");

            foreach (FileInfo arquivo in arquivosDjm)
            {
                LeiaArquivoDeExportacao(arquivo.FullName);
            }
        }

        private void EventoAoCriarArquivoDeExportacao(object sender, FileSystemEventArgs e)
        {
            LeiaArquivoDeExportacao(e.FullPath);
        }

        private void LeiaArquivoDeExportacao(string caminhoCompleto)
        {
            if (!File.Exists(caminhoCompleto))
            {
                return;
            }

            ServicoConfiguracoesPdv servicoConfiguracoesPdv = new ServicoConfiguracoesPdv();
            var configPdv = servicoConfiguracoesPdv.ConsulteUltimaConfiguracaoPdv();

            if (configPdv.GereVendaAPartirDoPdv)
            {
                string linha = string.Empty;

                System.IO.StreamReader arquivo = new System.IO.StreamReader(caminhoCompleto);

                PedidoDeVenda pedidoDeVenda = null;

                while ((linha = arquivo.ReadLine()) != null)
                {
                    List<string> listaCampos = linha.Split('|').ToList();
                    listaCampos.Add("somente para começar do primeiro.");

                    pedidoDeVenda = AbraOuFecheTurnoCaixaEFechePedidoCaixaAnterior(listaCampos, pedidoDeVenda);

                    pedidoDeVenda = PreenchaPedidoDeVenda(pedidoDeVenda, listaCampos);
                }

                CadastrePedidoVenda(pedidoDeVenda);

                arquivo.Close();
            }

            File.Delete(caminhoCompleto);
        }

        private PedidoDeVenda PreenchaPedidoDeVenda(PedidoDeVenda pedidoDeVenda, List<string> listaCampos)
        {
            ServicoPedidoDeVenda servicoPedidoVenda = new ServicoPedidoDeVenda(false, false);
            ServicoPessoa servicoPessoa = new ServicoPessoa(false, false);
            ServicoProduto servicoProduto = new ServicoProduto(false, false);
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            if (listaCampos[1] == "DOC")
            {
                CadastrePedidoVenda(pedidoDeVenda);

                pedidoDeVenda = null;

                if (listaCampos[2] == "RV")
                {
                    pedidoDeVenda = new PedidoDeVenda();

                    int idPreVendaAkil = listaCampos[26].ToInt();

                    if (idPreVendaAkil > 0)
                    {
                        #region " PRÉ-VENDA "

                        pedidoDeVenda = servicoPedidoVenda.Consulte(idPreVendaAkil);
                        pedidoDeVenda.PedidoDoPdv = true;

                        servicoPedidoVenda.Atualize(pedidoDeVenda);

                        #endregion
                    }
                    else
                    {
                        #region " QUANDO NÃO É PRÉ-VENDA "

                        pedidoDeVenda.DataElaboracao = listaCampos[10].ToDate();
                        pedidoDeVenda.TipoPedidoVenda = EnumTipoPedidoDeVenda.PEDIDOVENDA;
                        pedidoDeVenda.ObservacoesGeraisVenda = "PEDIDO GERADO A PARTIR DO PDV-ECF.";
                        pedidoDeVenda.ValorTotal = listaCampos[18].ToDouble() + listaCampos[19].ToDouble();
                        pedidoDeVenda.CondicaoPagamento = new CondicaoPagamento { Id = listaCampos[22].ToInt() };
                        pedidoDeVenda.Vendedor = servicoPessoa.Consulte(listaCampos[27].ToInt());
                        pedidoDeVenda.PedidoDoPdv = true;
                        _condicaoPagamentoParcelas = pedidoDeVenda.CondicaoPagamento;

                        int clienteId = listaCampos[13].ToInt();

                        if (clienteId > 0)
                        {
                            pedidoDeVenda.Cliente = servicoPessoa.Consulte(clienteId);
                        }
                        else
                        {
                            if (_configuracoesPdv.Cliente != null)
                            {
                                pedidoDeVenda.Cliente = servicoPessoa.Consulte(_configuracoesPdv.Cliente.Id);
                            }
                        }

                        //Insere Endereço no Pedido De Venda
                        if (pedidoDeVenda.Cliente == null || pedidoDeVenda.Cliente.ListaDeEnderecos.Count == 0)
                        {
                            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
                            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

                            pedidoDeVenda.EnderecoPedidoDeVenda.Complemento = empresa.DadosEmpresa.Endereco.Complemento;
                            pedidoDeVenda.EnderecoPedidoDeVenda.TipoEndereco = EnumTipoEndereco.PRINCIPAL;
                            pedidoDeVenda.EnderecoPedidoDeVenda.Numero = empresa.DadosEmpresa.Endereco.Numero;
                            
                            pedidoDeVenda.EnderecoPedidoDeVenda.CEP = empresa.DadosEmpresa.Endereco.CEP;
                            pedidoDeVenda.EnderecoPedidoDeVenda.Bairro = empresa.DadosEmpresa.Endereco.Bairro;
                            pedidoDeVenda.EnderecoPedidoDeVenda.Rua = empresa.DadosEmpresa.Endereco.Rua;
                            pedidoDeVenda.EnderecoPedidoDeVenda.Cidade = empresa.DadosEmpresa.Endereco.Cidade;
                        }
                        else
                        {
                            var endereco = pedidoDeVenda.Cliente.ListaDeEnderecos.FirstOrDefault(end => end.TipoEndereco == EnumTipoEndereco.PRINCIPAL);

                            if (endereco == null)
                            {
                                endereco = pedidoDeVenda.Cliente.ListaDeEnderecos.FirstOrDefault();
                            }

                            pedidoDeVenda.EnderecoPedidoDeVenda.Complemento = endereco.Complemento;
                            pedidoDeVenda.EnderecoPedidoDeVenda.TipoEndereco = endereco.TipoEndereco;
                            pedidoDeVenda.EnderecoPedidoDeVenda.Numero = endereco.Numero;

                            pedidoDeVenda.EnderecoPedidoDeVenda.CEP = endereco.CEP;
                            pedidoDeVenda.EnderecoPedidoDeVenda.Bairro = endereco.Bairro;
                            pedidoDeVenda.EnderecoPedidoDeVenda.Rua = endereco.Rua;
                            pedidoDeVenda.EnderecoPedidoDeVenda.Cidade = endereco.Cidade;
                        }

                        //Verificar quando cliente não informado ou não existe.
                        if (pedidoDeVenda.Cliente == null && !string.IsNullOrEmpty(listaCampos[16].ToString()))
                        {
                            string nome = listaCampos[17];

                            if (string.IsNullOrEmpty(nome))
                            {
                                nome = "CLIENTE CADASTRADO AUTOMATICAMENTE A PARITR DO PDV";
                            }

                            Pessoa pessoa = new Pessoa();
                            pessoa.DadosGerais.CpfCnpj = listaCampos[16].ToString();
                            pessoa.DadosGerais.DataCadastro = DateTime.Now;
                            pessoa.DadosGerais.EhCliente = true;
                            pessoa.DadosGerais.NomeFantasia = nome;
                            pessoa.DadosGerais.Razao = nome;

                            if (listaCampos[16].Length == 9)
                            {
                                pessoa.DadosGerais.TipoPessoa = EnumTipoPessoa.PESSOAFISICA;
                            }
                            else
                            {
                                pessoa.DadosGerais.TipoPessoa = EnumTipoPessoa.PESSOAJURIDICA;
                            }

                            servicoPessoa.Cadastre(pessoa);
                        }

                        // TODO: Verificar quando for cancelado.
                        // TODO: Verificar Tabela Preço.

                        #endregion
                    }
                }
            }

            if (pedidoDeVenda != null && pedidoDeVenda.Id == 0)
            {
                if (listaCampos[1] == "DIT" && listaCampos[14] == "N")
                {
                    ItemPedidoDeVenda itemPedidoDeVenda = new ItemPedidoDeVenda();
                    itemPedidoDeVenda.Produto = servicoProduto.Consulte(listaCampos[8].ToInt());
                    itemPedidoDeVenda.Quantidade = listaCampos[9].ToDouble();
                    itemPedidoDeVenda.ValorUnitario = listaCampos[10].ToDouble();
                    itemPedidoDeVenda.ValorTotal = listaCampos[17].ToDouble();
                    itemPedidoDeVenda.DescontoEhPercentual = false;

                    if (listaCampos[11].ToDouble() > 0)
                    {
                        itemPedidoDeVenda.ValorUnitario += listaCampos[11].ToDouble();
                    }
                    else
                    {
                        itemPedidoDeVenda.TotalDesconto = Math.Abs(listaCampos[11].ToDouble());
                    }
                }

                if (listaCampos[1] == "DPG")
                {
                    var formaPagamento = servicoFormaPagamento.Consulte(listaCampos[8].ToInt());

                    pedidoDeVenda.FormaPagamento = formaPagamento;

                    ParcelaPedidoDeVenda parcelaPedidoDeVenda = new ParcelaPedidoDeVenda();

                    parcelaPedidoDeVenda.CondicaoPagamento = _condicaoPagamentoParcelas;
                    parcelaPedidoDeVenda.DataVencimento = DateTime.Now.Date;
                    parcelaPedidoDeVenda.FormaPagamento = formaPagamento;
                    parcelaPedidoDeVenda.Valor = listaCampos[9].ToDouble();

                    pedidoDeVenda.ListaParcelasPedidoDeVenda.Add(parcelaPedidoDeVenda);

                    if (formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DUPLICATA || formaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDIARIOPROPRIO)
                    {
                        _formaPagamentoParcelas = formaPagamento;
                    }
                }

                //if (listaCampos[1] == "DPA")
                //{
                //    ParcelaPedidoDeVenda parcelaPedidoDeVenda = new ParcelaPedidoDeVenda();

                //    parcelaPedidoDeVenda.DataVencimento = listaCampos[7].ToDate();
                //    parcelaPedidoDeVenda.FormaPagamento = _formaPagamentoParcelas;
                //    parcelaPedidoDeVenda.Valor = listaCampos[8].ToDouble();

                //    pedidoDeVenda.ListaParcelasPedidoDeVenda.Add(parcelaPedidoDeVenda);
                //}
            }

            return pedidoDeVenda;
        }

        private PedidoDeVenda AbraOuFecheTurnoCaixaEFechePedidoCaixaAnterior(List<string> listaCampos, PedidoDeVenda pedidoDeVendaTurnoAnterior)
        {
            if (listaCampos[1] == "TUR")
            {
                CadastrePedidoVenda(pedidoDeVendaTurnoAnterior);

                DateTime dataHoraAbretura = (listaCampos[6] + " " + listaCampos[7]).ToDate();
                DateTime dataHoraFechamento = (listaCampos[8] + " " + listaCampos[9]).ToDate();

                ServicoCaixa servicoCaixa = new ServicoCaixa();
                ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

                var caixa = servicoCaixa.Consulte(listaCampos[11].ToInt());

                Sessao.PessoaLogada = caixa.Funcionario;

                var movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

                if (movimentacaoCaixa == null)
                {
                    movimentacaoCaixa.Caixa = caixa;
                    movimentacaoCaixa.SaldoInicial = listaCampos[19].ToDouble();
                    movimentacaoCaixa.Status = EnumStatusMovimentacaoCaixa.ABERTO;
                    movimentacaoCaixa.UsuarioAbertura = Sessao.PessoaLogada;
                    movimentacaoCaixa.DataHoraAbertura = dataHoraAbretura;

                    movimentacaoCaixa.ObservacoesAbertura = "Abertura de caixa a partir do PDV";

                    servicoMovimentacaoCaixa.Cadastre(movimentacaoCaixa);

                    movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);
                }

                if (dataHoraFechamento > DateTime.MinValue && dataHoraAbretura.Date == movimentacaoCaixa.DataHoraAbertura.Value.Date)
                {
                    movimentacaoCaixa.DataHoraFechamento = dataHoraFechamento;
                    movimentacaoCaixa.ObservacoesFechamento = "CAIXA FECHADO AUTOMATICAMENTE PELO PDV";
                    movimentacaoCaixa.ResultadoCaixa = EnumResultadoCaixa.SALDOCORRETO;
                    movimentacaoCaixa.Status = EnumStatusMovimentacaoCaixa.FECHADO;
                    movimentacaoCaixa.UsuarioFechamento = Sessao.PessoaLogada;

                    servicoMovimentacaoCaixa.Atualize(movimentacaoCaixa);
                }

                return null;
            }

            return pedidoDeVendaTurnoAnterior;
        }

        private void CadastrePedidoVenda(PedidoDeVenda pedidoDeVenda)
        {
            if (pedidoDeVenda == null || pedidoDeVenda.Id > 0)
            {
                return;
            }

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda(false, false);
            ServicoRecebimento servicoRecebimento = new ServicoRecebimento();

            servicoPedidoDeVenda.FechePedidoDeVenda(pedidoDeVenda);

            var pedidoRecebimento = servicoRecebimento.Consulte(pedidoDeVenda.Id);

            double totalPix = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.PIX ? parcela.Valor : 0), 2);
            double totalDinheiro = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO ? parcela.Valor : 0), 2);
            double totalCheque = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE ? parcela.Valor : 0), 2);
            double totalCartaoDebito = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO ? parcela.Valor : 0), 2);
            double totalCartaoCredito = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ? parcela.Valor : 0), 2);

            servicoRecebimento.FatureRecebimento(pedidoRecebimento,0, totalPix, totalDinheiro, totalCartaoDebito, totalCartaoCredito, totalCheque);
        }

        #endregion

        #endregion

        #region " CLASSES AUXILIARES "

        private class PreVendaPFP
        {
            public int formaPagamentoId { get; set; }

            public double ValorFormaPagamento { get; set; }
        }

        #endregion
    }
}
