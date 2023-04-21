using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using System.Globalization;
using Programax.Infraestrutura.Negocio.Utils;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EnderecoServ;

namespace Programax.Easy.Report.RelatoriosDevExpress.Financeiro
{
    public partial class RelatorioReciboCaixa : RelatorioBaseDev
    {
        private int _idItemMovimentacaoCaixa;
        private int? _idParceiro;

        public RelatorioReciboCaixa(int idItemMovimentacaoCaixa, int? idParceiro)
        {
            InitializeComponent();

            _tituloRelatorio = "RECIBO";
            _idItemMovimentacaoCaixa = idItemMovimentacaoCaixa;
            _idParceiro = idParceiro;
        }

        protected override void CarregueDadosRelatorio()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            ServicoItemMovimentacaoCaixa servicoItemMovimentacaoCaixa = new ServicoItemMovimentacaoCaixa();
            ServicoPessoa servicoPessoa = new ServicoPessoa();

            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            var itemMovimentacaoCaixa = servicoItemMovimentacaoCaixa.Consulte(_idItemMovimentacaoCaixa);

            ReciboCaixaRelatorio reciboCaixaRelatorio = new ReciboCaixaRelatorio();
            reciboCaixaRelatorio.CidadeEData = empresa.DadosEmpresa.Endereco.Cidade.Descricao + ", " +
                                                                itemMovimentacaoCaixa.DataHora.Date.ToString("dd \\de MMMM \\de yyyy", CultureInfo.CreateSpecificCulture("pt-BR"));

            reciboCaixaRelatorio.CpfCnpjEmpresa = empresa.DadosEmpresa.Cnpj;

            reciboCaixaRelatorio.NomeEmpresa = empresa.DadosEmpresa.RazaoSocial;

            reciboCaixaRelatorio.EnderecoEmpresa = string.Concat(empresa.DadosEmpresa.Endereco.Rua, ", ",
                                                                                                   empresa.DadosEmpresa.Endereco.Numero, ", ",
                                                                                                   empresa.DadosEmpresa.Endereco.Bairro, ", ",
                                                                                                   empresa.DadosEmpresa.Endereco.Cidade.Descricao, " ",
                                                                                                   empresa.DadosEmpresa.Endereco.CEP, " ",
                                                                                                   empresa.DadosEmpresa.Endereco.Cidade.Estado.UF);

            if (_idParceiro != null)
            {
                var pessoa = servicoPessoa.Consulte(_idParceiro.Value);

                PreenchaNomeClienteEEndereco(reciboCaixaRelatorio, pessoa);
            }
            else
            {
                PreenchaNomeClienteEEndereco(reciboCaixaRelatorio, itemMovimentacaoCaixa.Parceiro);
            }

            reciboCaixaRelatorio.Observacao = itemMovimentacaoCaixa.HistoricoMovimentacoes;
            reciboCaixaRelatorio.Valor = itemMovimentacaoCaixa.Valor.ToString("#,###,##0.00");
            reciboCaixaRelatorio.ValorExtenso = itemMovimentacaoCaixa.Valor.EscrevaPorExtensoEmReais();

            List<ReciboCaixaRelatorio> listaRecibos = new List<ReciboCaixaRelatorio>();
            listaRecibos.Add(reciboCaixaRelatorio);

            ConteudoRelatorio.DataSource = listaRecibos;
        }

        private void PreenchaNomeClienteEEndereco(ReciboCaixaRelatorio reciboCaixaRelatorio, Pessoa pessoa)
        {
            if (pessoa != null)
            {
                reciboCaixaRelatorio.NomeParceiro = pessoa.DadosGerais.Razao;

                reciboCaixaRelatorio.CpfCnpjParceiro = pessoa.DadosGerais.CpfCnpj;

                var enderecoParceiro = pessoa.ListaDeEnderecos.FirstOrDefault(endereco => endereco.TipoEndereco == EnumTipoEndereco.PRINCIPAL);

                if (enderecoParceiro == null)
                {
                    enderecoParceiro = pessoa.ListaDeEnderecos.FirstOrDefault(endereco => endereco.TipoEndereco == EnumTipoEndereco.PRINCIPAL);
                }

                if (enderecoParceiro != null)
                {
                    var end = new ServicoPessoa().Consulte(pessoa.Id).ListaDeEnderecos;

                    enderecoParceiro = end.FirstOrDefault();

                    reciboCaixaRelatorio.EnderecoParceiro = string.Concat(enderecoParceiro.Rua, ", ", 
                                                                                                   enderecoParceiro.Numero, ", ", 
                                                                                                   enderecoParceiro.Bairro, ", ", 
                                                                                                   enderecoParceiro.Cidade.Descricao, " ", 
                                                                                                   enderecoParceiro.CEP, " ", 
                                                                                                   enderecoParceiro.Cidade.Estado.UF);
                }
            }
        }

        #region " CLASSES AUXILIARES "

        public class ReciboCaixaRelatorio
        {
            public string Valor { get; set; }

            public string ValorExtenso { get; set; }

            public string NomeParceiro { get; set; }

            public string EnderecoParceiro { get; set; }

            public string EnderecoEmpresa { get; set; }

            public string Observacao { get; set; }

            public string CidadeEData { get; set; }

            public string NomeEmpresa { get; set; }

            public string CpfCnpjEmpresa { get; set; }

            public string CpfCnpjParceiro { get; set; }
        }

        #endregion
    }
}
