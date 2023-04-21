using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using System.Collections.Generic;
using System.Linq;

namespace Programax.Easy.Report.RelatoriosDevExpress.Cadastros
{
    public partial class RelatorioParceiro : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private bool _ehCliente;
        private bool _ehFornecedor;
        private bool _ehFuncionario;
        private bool _ehTransportadora;
        private Estado _estado;
        private Cidade _cidade;
        private Pessoa _vendedor;
        private Pessoa _atendente;
        private Pessoa _indicador;
        private RamoAtividade _ramoAtividade;
        private string _mesAnoAniversario;
        private EnumOrdenacaoPesquisaPessoa _ordenacaoPesquisa;
        private EnumTipoCliente _tipoCliente;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioParceiro(bool ehCliente,
                                           bool ehFornecedor,
                                           bool ehFuncionario,
                                           bool ehTransportadora,
                                           Estado estado,
                                           Cidade cidade,
                                           Pessoa vendedor,
                                           Pessoa atendente,
                                           Pessoa indicador,
                                           RamoAtividade ramoAtividade,
                                           string mesAnoAniversario,
                                           EnumOrdenacaoPesquisaPessoa ordenacaoPesquisa,
                                           EnumTipoCliente tipoCliente)
        {
            InitializeComponent();

            _tituloRelatorio = " RELATÓRIO DE PARCEIROS ";

            _ehCliente = ehCliente;
            _ehFornecedor = ehFornecedor;
            _ehFuncionario = ehFuncionario;
            _ehTransportadora = ehTransportadora;
            _estado = estado;
            _cidade = cidade;
            _vendedor = vendedor;
            _atendente = atendente;
            _indicador = indicador;
            _ramoAtividade = ramoAtividade;
            _mesAnoAniversario = mesAnoAniversario;
            _ordenacaoPesquisa = ordenacaoPesquisa;
            _tipoCliente = tipoCliente;
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var listaParceiros = servicoPessoa.ConsulteListaPessoas(_ehCliente,
                                                                                           _ehFornecedor,
                                                                                           _ehFuncionario,
                                                                                           _ehTransportadora,
                                                                                           _estado,
                                                                                           _cidade,
                                                                                           _vendedor,
                                                                                           _atendente,
                                                                                           _indicador,
                                                                                           _ramoAtividade,
                                                                                           _mesAnoAniversario,
                                                                                           _ordenacaoPesquisa,
                                                                                           _tipoCliente);

            List<ClienteRelatorio> listaParceirosRelatorio = new List<ClienteRelatorio>();

            foreach (var pessoa in listaParceiros)
            {
                ClienteRelatorio parceiroRelatorio = new ClienteRelatorio();

                parceiroRelatorio.Atividade = pessoa.EmpresaPessoa != null && pessoa.EmpresaPessoa.RamoDeAtividade != null ? pessoa.EmpresaPessoa.RamoDeAtividade.Descricao : string.Empty;
                parceiroRelatorio.CpfCnpj = pessoa.DadosGerais.CpfCnpj;
                parceiroRelatorio.DataCadastro = pessoa.DadosGerais.DataCadastro.ToString("dd/MM/yyyy");
                parceiroRelatorio.Email = pessoa.EmpresaPessoa != null ? pessoa.EmpresaPessoa.EmailPrincipal : string.Empty;
                parceiroRelatorio.Id = pessoa.Id;
                parceiroRelatorio.NomeCliente = pessoa.DadosGerais.Razao;
                parceiroRelatorio.Tipo = pessoa.DadosGerais.TipoPessoa == EnumTipoPessoa.PESSOAFISICA ? "PF" : "PJ";
                parceiroRelatorio.Vendedor = pessoa.DadosGerais.Razao;
                
                if (pessoa.ListaDeEnderecos.Count > 0)
                {
                    var endereco = pessoa.ListaDeEnderecos.FirstOrDefault(end => end.TipoEndereco == EnumTipoEndereco.PRINCIPAL);

                    if (endereco == null)
                    {
                        endereco = pessoa.ListaDeEnderecos.FirstOrDefault();
                    }

                    if (endereco != null)
                    {
                        parceiroRelatorio.Bairro = endereco.Bairro;
                        parceiroRelatorio.Cidade = endereco.Cidade != null? endereco.Cidade.Descricao:string.Empty;
                        parceiroRelatorio.Estado = endereco.Cidade != null ? endereco.Cidade.Estado != null ? endereco.Cidade.Estado.UF : string.Empty:string.Empty;

                        parceiroRelatorio.Cep = endereco.CEP;
                        parceiroRelatorio.Complemento = endereco.Complemento;

                    }
                }

                if (pessoa.ListaDeTelefones.Count > 0)
                {
                    var celular = pessoa.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.CELULAR);

                    var telefone = pessoa.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.RESIDENCIAL);

                    if (telefone == null)
                    {
                        telefone = pessoa.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.COMERCIAL);

                        if (telefone == null && celular == null)
                        {
                            telefone = pessoa.ListaDeTelefones.FirstOrDefault();
                        }
                    }

                    if (telefone != null)
                    {
                        parceiroRelatorio.Telefone = "(" + telefone.Ddd + ")" + " " + telefone.Numero;
                    }

                    if (celular != null)
                    {
                        parceiroRelatorio.Celular = "(" + celular.Ddd + ")" + " " + celular.Numero;
                    }
                }

                listaParceirosRelatorio.Add(parceiroRelatorio);
            }

            ConteudoRelatorio.DataSource = listaParceirosRelatorio;

            txtTotalRegistros.Text = listaParceiros.Count.ToString();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class ClienteRelatorio
        {
            public int Id { get; set; }

            public string CpfCnpj { get; set; }

            public string NomeCliente { get; set; }

            public string Vendedor { get; set; }

            public string Bairro { get; set; }

            public string Cep { get; set; }

            public string Complemento { get; set; }

            public string Cidade { get; set; }

            public string Estado { get; set; }

            public string Telefone { get; set; }

            public string Celular { get; set; }

            public string Email { get; set; }

            public string Atividade { get; set; }

            public string Tipo { get; set; }

            public string DataCadastro { get; set; }
        }

        #endregion
    }
}
