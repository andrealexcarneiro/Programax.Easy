using System;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Report.RelatoriosDevExpress.Estoque
{
    public partial class RelatorioEntrada : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _entradaId;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioEntrada(int entradaId)
        {
            InitializeComponent();

            _entradaId = entradaId;
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

            var entrada = servicoEntradaMercadoria.Consulte(_entradaId);

            var entradaRelatorio = RetorneEntradaRelatorio(entrada);

            ConteudoRelatorio.DataSource = new List<EntradaRelatorio> { entradaRelatorio };
        }

        #endregion

        private EntradaRelatorio RetorneEntradaRelatorio(EntradaMercadoria entrada)
        {
            EntradaRelatorio entradaRelatorio = new EntradaRelatorio();

            PreenchaDadosGeraisNota(entrada, entradaRelatorio);
            PreenchaDadosFornecedor(entrada, entradaRelatorio);
            PreenchaDadosProdutos(entrada, entradaRelatorio);
            PreenchaDadosFechamentoEntrada(entrada, entradaRelatorio);
            PreenchaDadosFinanceiro(entrada, entradaRelatorio);

            return entradaRelatorio;
        }

        private void PreenchaDadosGeraisNota(EntradaMercadoria entrada, EntradaRelatorio entradaRelatorio)
        {
            entradaRelatorio.NumeroNota = entrada.NumeroNota;
            entradaRelatorio.Usuario = entrada.UsuarioCadastro != null ? entrada.UsuarioCadastro.DadosGerais.Razao : string.Empty;
            entradaRelatorio.Situacao = entrada.StatusEntrada.Descricao();
            entradaRelatorio.DataEmissao = entrada.DataEmissao != null ? entrada.DataEmissao.Value.ToString("dd/MM/yyyy") : string.Empty;
            entradaRelatorio.DataEntrada = entrada.DataMovimentacao != null ? entrada.DataMovimentacao.Value.ToString("dd/MM/yyyy") : string.Empty;
        }

        private void PreenchaDadosFornecedor(EntradaMercadoria entrada, EntradaRelatorio entradaRelatorio)
        {
            entradaRelatorio.Fornecedor = entrada.Fornecedor.DadosGerais.NomeFantasia;
            entradaRelatorio.Cnpj = entrada.Fornecedor.DadosGerais.CpfCnpj;

            var endereco = entrada.Fornecedor.ListaDeEnderecos.FirstOrDefault(end => end.TipoEndereco == EnumTipoEndereco.PRINCIPAL);

            if (endereco != null)
            {
                entradaRelatorio.Endereco = endereco.Rua;
                entradaRelatorio.Numero = endereco.Numero;
                entradaRelatorio.Complemento = endereco.Complemento;
                entradaRelatorio.Bairro = endereco.Bairro;
                entradaRelatorio.Cidade = endereco.Cidade.Descricao;
                entradaRelatorio.Estado = endereco.Cidade.Estado != null ? endereco.Cidade.Estado.UF : string.Empty;
                entradaRelatorio.Cep = endereco.CEP;
            }

            if (entrada.Fornecedor.EmpresaPessoa != null)
            {
                entradaRelatorio.Email = entrada.Fornecedor.EmpresaPessoa.EmailPrincipal;
                entradaRelatorio.Contato = entrada.Fornecedor.EmpresaPessoa.NomeContato1;
            }

            var telefone = entrada.Fornecedor.ListaDeTelefones.FirstOrDefault(fone => fone.TipoTelefone == EnumTipoTelefone.COMERCIAL);

            if (telefone == null)
            {
                telefone = entrada.Fornecedor.ListaDeTelefones.FirstOrDefault();
            }

            if (telefone != null)
            {
                entradaRelatorio.Telefone = "(" + telefone.Ddd + ") " + telefone.Numero;
            }
        }

        private void PreenchaDadosProdutos(EntradaMercadoria entrada, EntradaRelatorio entradaRelatorio)
        {
            entradaRelatorio.ListaItens = new List<ItemEntradaRelatorio>();

            for (int i = 0; i < entrada.ListaDeItens.Count; i++)
            {
                var itemEntrada = entrada.ListaDeItens[i];

                ItemEntradaRelatorio itemEntradaRelatorio = new ItemEntradaRelatorio();

                itemEntradaRelatorio.Codigo = itemEntrada.Produto.Id.ToString();
                itemEntradaRelatorio.CodigoBarras = itemEntrada.Produto.DadosGerais.CodigoDeBarras;
                itemEntradaRelatorio.DescricaoProduto = itemEntrada.Produto.DadosGerais.Descricao;
                itemEntradaRelatorio.Unidade = itemEntrada.Produto.DadosGerais.Unidade != null ? itemEntrada.Produto.DadosGerais.Unidade.Abreviacao : string.Empty;
                itemEntradaRelatorio.Marca = itemEntrada.Produto.Principal != null && itemEntrada.Produto.Principal.Marca != null ? itemEntrada.Produto.Principal.Marca.Descricao : string.Empty;
                itemEntradaRelatorio.Quantidade = itemEntrada.Quantidade.ToString();
                itemEntradaRelatorio.ValorUniario = itemEntrada.ValorUnitario.GetValueOrDefault().ToString("#,###,##0.00");
                itemEntradaRelatorio.Desconto = (itemEntrada.ValorUnitario.GetValueOrDefault() * itemEntrada.Quantidade - itemEntrada.ValorTotal.GetValueOrDefault()).ToString("#,###,##0.00");
                itemEntradaRelatorio.ValorTotal = itemEntrada.ValorTotal.GetValueOrDefault().ToString("#,###,##0.00");

                entradaRelatorio.ListaItens.Add(itemEntradaRelatorio);
            }
        }

        private void PreenchaDadosFechamentoEntrada(EntradaMercadoria entrada, EntradaRelatorio entradaRelatorio)
        {
            //entradaRelatorio.FormaPagamento
            //entradaRelatorio.CondicaoPagamento
            entradaRelatorio.TipoFrete = entrada.TipoFrete.Descricao();
            entradaRelatorio.Transportadora = entrada.Transportadora != null ? entrada.Transportadora.DadosGerais.NomeFantasia : string.Empty;
            entradaRelatorio.Observacao = entrada.Observacoes;

            double subtotal = entrada.ValorTotalNota + entrada.ValorDesconto;

            subtotal -= entrada.TipoFrete == EnumTipoFrete.PORCONTADODESTINATARIOREMETENTE ? entrada.ValorFrete : 0;

            entradaRelatorio.SubTotal = subtotal.ToString("#,###,##0.00");
            entradaRelatorio.Desconto = entrada.ValorDesconto.ToString("#,###,##0.00");
            entradaRelatorio.Frete = entrada.ValorFrete.ToString("#,###,##0.00");
            entradaRelatorio.Total = entrada.ValorTotalNota.ToString("#,###,##0.00");
        }

        private void PreenchaDadosFinanceiro(EntradaMercadoria entrada, EntradaRelatorio entradaRelatorio)
        {
            entradaRelatorio.ListaFinanceiro = new List<FinanceiroEntradaRelatorio>();

            for (int i = 0; i < entrada.ListaFinanceiroEntrada.Count; i++)
            {
                var financeiroEntrada = entrada.ListaFinanceiroEntrada[i];

                FinanceiroEntradaRelatorio financeiroEntradaRelatorio = new FinanceiroEntradaRelatorio();

                financeiroEntradaRelatorio.FormaPagamento = financeiroEntrada.FormaPagamento != null ? financeiroEntrada.FormaPagamento.Descricao : string.Empty;
                financeiroEntradaRelatorio.NumeroDocumento = financeiroEntrada.NumeroDocumento;
                financeiroEntradaRelatorio.Parcela = financeiroEntrada.Parcela;
                financeiroEntradaRelatorio.Valor = financeiroEntrada.ValorDuplicata.ToString("#,###,##0.00");
                financeiroEntradaRelatorio.Vencimento = financeiroEntrada.DataVencimento.ToString("dd/MM/yyyy");

                entradaRelatorio.ListaFinanceiro.Add(financeiroEntradaRelatorio);
            }
        }

        #region " CLASSES AUXILIARES "

        public class EntradaRelatorio
        {
            public string NumeroNota { get; set; }

            public string Usuario { get; set; }

            public string Situacao { get; set; }

            public string DataEntrada { get; set; }

            public string DataEmissao { get; set; }

            public string Fornecedor { get; set; }

            public string Endereco { get; set; }

            public string Bairro { get; set; }

            public string Telefone { get; set; }

            public string Numero { get; set; }

            public string Cidade { get; set; }

            public string Contato { get; set; }

            public string Cnpj { get; set; }

            public string Complemento { get; set; }

            public string Estado { get; set; }

            public string Cep { get; set; }

            public string Email { get; set; }

            public string TipoFrete { get; set; }

            public string Transportadora { get; set; }

            public string Observacao { get; set; }

            public string SubTotal { get; set; }

            public string Desconto { get; set; }

            public string Frete { get; set; }

            public string Total { get; set; }

            public List<ItemEntradaRelatorio> ListaItens { get; set; }

            public List<FinanceiroEntradaRelatorio> ListaFinanceiro { get; set; }
        }

        public class ItemEntradaRelatorio
        {
            public string Codigo { get; set; }

            public string CodigoBarras { get; set; }

            public string DescricaoProduto { get; set; }

            public string Usuario { get; set; }

            public string Marca { get; set; }

            public string Quantidade { get; set; }

            public string ValorUniario { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }

            public string Unidade { get; set; }
        }

        public class FinanceiroEntradaRelatorio
        {
            public string Parcela { get; set; }

            public string NumeroDocumento { get; set; }

            public string FormaPagamento { get; set; }

            public string Vencimento { get; set; }

            public string Valor { get; set; }
        }

        #endregion
    }
}
