using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Utils;
using System.Linq;

namespace Programax.Easy.Report.RelatoriosDevExpress.Financeiro
{
    public partial class RelatorioNotaPromissoria : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoa;
        private EnumDataFiltrarContasPagarReceber? _dataFiltrar;
        private DateTime? _dataInicialPeriodo;
        private DateTime? _dataFinalPeriodo;
        private EnumStatusContaPagarReceber? _statusContaPagarReceber;
        private EnumTipoOperacaoContasPagarReceber _tipoOperacao;
        private EnumOrdenacaoPesquisaContasPagarReceber _ordenacaoPesquisaContasPagarReceber;
        private List<ContaPagarReceber> _listaClientesPromissoria = new List<ContaPagarReceber>();
        private int? _numeroPedido;
       
        #endregion

        #region " CONSTRUTOR "

        public RelatorioNotaPromissoria(List<ContaPagarReceber>listaClientesPromissoria, int? numeroPedido)
        {
            InitializeComponent();
            
            _listaClientesPromissoria = listaClientesPromissoria;

            _numeroPedido = numeroPedido;

            CarregueDadosEmpresa();
            
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            if (_numeroPedido != null)
                geraPromissoriaApenasUmClienteSelecionadoETodasParcelas();
            else
                geraPromissoriaVariosClientesEParcelasSelecionados();


        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void geraPromissoriaVariosClientesEParcelasSelecionados()
        {
            List<ContasPagarReceberRelatorio> listaContasPagarReceberRelatorio = new List<ContasPagarReceberRelatorio>();

            for (int i = 0; i < _listaClientesPromissoria.Count; i++)
            {
                ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

                //_pessoa = _listaClientesPromissoria[i].Pessoa;
                                
                var contaPagarReceber = servicoContasPagarReceber.Consulte(_listaClientesPromissoria[i].Id);
                                
                ContasPagarReceberRelatorio contasPagarReceberRelatorio = new ContasPagarReceberRelatorio();

                contasPagarReceberRelatorio.NomeParceiro = contaPagarReceber.Pessoa.DadosGerais.Razao;

                contasPagarReceberRelatorio.CnpjCpfParceiro = contaPagarReceber.Pessoa.DadosGerais.CpfCnpj;

                contasPagarReceberRelatorio.RuaParceiro = contaPagarReceber.Pessoa.ListaDeEnderecos.Count > 0 ? contaPagarReceber.Pessoa.ListaDeEnderecos[0].Rua : null;

                contasPagarReceberRelatorio.BairroParceiro = contaPagarReceber.Pessoa.ListaDeEnderecos.Count > 0 ? contaPagarReceber.Pessoa.ListaDeEnderecos[0].Bairro : null;

                contasPagarReceberRelatorio.CidadeParceiro = contaPagarReceber.Pessoa.ListaDeEnderecos.Count > 0 ? contaPagarReceber.Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;

                contasPagarReceberRelatorio.EstadoParceiro = contaPagarReceber.Pessoa.ListaDeEnderecos.Count > 0 ? contaPagarReceber.Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                contasPagarReceberRelatorio.NumeroDocumento = _listaClientesPromissoria[i].NumeroDocumento;

                contasPagarReceberRelatorio.DataVencimento = _listaClientesPromissoria[i].DataVencimento != null ? _listaClientesPromissoria[i].DataVencimento.Value.ToString("dd/MM/yyyy") : string.Empty;

                contasPagarReceberRelatorio.DataEmissao = DateTime.Now.ToString("dd/MM/yyyy");

                contasPagarReceberRelatorio.Valor = "R$ " + _listaClientesPromissoria[i].ValorParcela.ToString("#,###,##0.00");


                listaContasPagarReceberRelatorio.Add(contasPagarReceberRelatorio);
                
            }
            ConteudoRelatorio.DataSource = listaContasPagarReceberRelatorio;
        }

        private void geraPromissoriaApenasUmClienteSelecionadoETodasParcelas()
        {
            for (int i = 0; i < _listaClientesPromissoria.Count; i++)
            {
                ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

                _pessoa = _listaClientesPromissoria[i].Pessoa;
                _dataFiltrar = EnumDataFiltrarContasPagarReceber.EMISSAO;
                _dataInicialPeriodo = _listaClientesPromissoria[i].DataEmissao;
                _dataFinalPeriodo = _listaClientesPromissoria[i].DataEmissao;
                _statusContaPagarReceber = EnumStatusContaPagarReceber.ABERTO;
                _tipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER;
                _ordenacaoPesquisaContasPagarReceber = EnumOrdenacaoPesquisaContasPagarReceber.NOMEPARCEIRO;


                var listaContasPagarReceber = servicoContasPagarReceber.ConsulteListaFazendoFetchComParceiroEEnderecos(_pessoa,
                                                                                                                       _dataFiltrar,
                                                                                                                       _dataInicialPeriodo,
                                                                                                                       _dataFinalPeriodo,
                                                                                                                       _statusContaPagarReceber,
                                                                                                                       _tipoOperacao,
                                                                                                                       _ordenacaoPesquisaContasPagarReceber);

                List<ContasPagarReceberRelatorio> listaContasPagarReceberRelatorio = new List<ContasPagarReceberRelatorio>();

                foreach (var contaPagarReceber in listaContasPagarReceber)
                {
                    var numeroDoc = contaPagarReceber.NumeroDocumento.Split('-');

                    if (_numeroPedido == numeroDoc[0].Trim().ToInt())
                    {

                        ContasPagarReceberRelatorio contasPagarReceberRelatorio = new ContasPagarReceberRelatorio();

                        contasPagarReceberRelatorio.NomeParceiro = contaPagarReceber.Pessoa.DadosGerais.Razao;

                        contasPagarReceberRelatorio.CnpjCpfParceiro = contaPagarReceber.Pessoa.DadosGerais.CpfCnpj;

                        contasPagarReceberRelatorio.RuaParceiro = contaPagarReceber.Pessoa.ListaDeEnderecos.Count > 0 ? contaPagarReceber.Pessoa.ListaDeEnderecos[0].Rua : null;

                        contasPagarReceberRelatorio.BairroParceiro = contaPagarReceber.Pessoa.ListaDeEnderecos.Count > 0 ? contaPagarReceber.Pessoa.ListaDeEnderecos[0].Bairro : null;

                        contasPagarReceberRelatorio.CidadeParceiro = contaPagarReceber.Pessoa.ListaDeEnderecos.Count > 0 ? contaPagarReceber.Pessoa.ListaDeEnderecos[0].Cidade.Descricao : null;

                        contasPagarReceberRelatorio.EstadoParceiro = contaPagarReceber.Pessoa.ListaDeEnderecos.Count > 0 ? contaPagarReceber.Pessoa.ListaDeEnderecos[0].Cidade.Estado.UF : null;

                        contasPagarReceberRelatorio.NumeroDocumento = contaPagarReceber.NumeroDocumento;

                        contasPagarReceberRelatorio.DataVencimento = contaPagarReceber.DataVencimento != null ? contaPagarReceber.DataVencimento.Value.ToString("dd/MM/yyyy") : string.Empty;

                        contasPagarReceberRelatorio.DataEmissao = DateTime.Now.ToString("dd/MM/yyyy");

                        contasPagarReceberRelatorio.Valor = "R$ " + contaPagarReceber.ValorParcela.ToString("#,###,##0.00");


                        listaContasPagarReceberRelatorio.Add(contasPagarReceberRelatorio);
                    }
                }
                ConteudoRelatorio.DataSource = listaContasPagarReceberRelatorio;
            }
        }

        private void CarregueDadosEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();

            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            txtNomeEmpresa.Text = empresa.DadosEmpresa.NomeFantasia;
            txtNomeEmpresaEmissao.Text = empresa.DadosEmpresa.NomeFantasia;

            txtCNPJEmpresa.Text = empresa.DadosEmpresa.Cnpj;

            txtRuaEmpresa.Text = empresa.DadosEmpresa.Endereco.Rua;
            txtBairroEmpresa.Text = empresa.DadosEmpresa.Endereco.Bairro;
            txtCidadeEmpresa.Text = empresa.DadosEmpresa.Endereco.Cidade.Descricao;
            txtCidadeEmpresaEmissao.Text = empresa.DadosEmpresa.Endereco.Cidade.Descricao;
            txtEstadoEmpresa.Text = empresa.DadosEmpresa.Endereco.Cidade.Estado.UF;
            txtFoneEmpresa.Text = "Fone: " + empresa.DadosEmpresa.Telefone;

            pctLogo2.Image = empresa.DadosEmpresa.Foto.ToImage();

            empresa.DadosEmpresa.Foto.ToImage().Dispose();
        }
        
        #endregion

        #region " CLASSES AUXILIARES "

        public class ContasPagarReceberRelatorio
        {
           
            public string NomeParceiro { get; set; }

            public string CnpjCpfParceiro { get; set; }

            public string RuaParceiro { get; set; }

            public string BairroParceiro { get; set; }

            public string CidadeParceiro { get; set; }

            public string EstadoParceiro { get; set; }

            public string NumeroDocumento { get; set; }
            
            public string DataVencimento { get; set; }

            public string DataEmissao { get; set; }
            
            public string Valor { get; set; }
           
        }

        #endregion
    }
}
