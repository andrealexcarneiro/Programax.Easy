using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Report.RelatoriosDevExpress.Estoque;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioEntrada : FormularioBase
    {

#region "METODOS PÚBLICOS"

        public NotaFiscal PesquiseNotaFiscal()
        {
            this.ShowDialog();

            return _notaFiscalSelecionada;
        }

#endregion

        #region " VARIÁVEIS PRIVADAS "

        private List<EntradaMercadoria> _listaEntradasMercadorias;
        private bool _ehOutrasSaidasConfig;
        private NotaFiscal _notaFiscalSelecionada;
        private bool _enviarNumeroNotaEntrada;


        #endregion

        #region " CONSTRUTOR "

        public FormRelatorioEntrada(bool ehOutrasSaidas=false, bool enviarNumeroNotaEntrada=false)
        {
            InitializeComponent();

            _listaEntradasMercadorias = new List<EntradaMercadoria>();

            PreenchaCboStatus();
            PreenchaDatasInicialEFinal();

            configureParaOutrasSaidas(ehOutrasSaidas);

            _enviarNumeroNotaEntrada = enviarNumeroNotaEntrada;

            Pesquise();

            this.ActiveControl = gcNotasEntrada;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void gcNotasEntrada_DoubleClick(object sender, EventArgs e)
        {
            if (_ehOutrasSaidasConfig)
                SelecioneDocumento(_ehOutrasSaidasConfig);
            else
                ImprimaEntrada();
           
        }

        private void gcNotasEntrada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_ehOutrasSaidasConfig)
                    SelecioneDocumento(_ehOutrasSaidasConfig);
                else
                    ImprimaEntrada();                
            }
        }

        private void txtRazaoSocialFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pesquise();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimaEntrada();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            SelecioneDocumento(_ehOutrasSaidasConfig);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void Pesquise()
        {
            EnumStatusEntrada? status = (EnumStatusEntrada?)cboStatus.EditValue;

            ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

            _listaEntradasMercadorias = servicoEntradaMercadoria.ConsulteLista(txtDataInicialEmissao.Text.ToDateNullabel(),
                                                                                                              txtDataFinalEmissao.Text.ToDateNullabel(),
                                                                                                              txtDataInicialEntrada.Text.ToDateNullabel(),
                                                                                                              txtDataFinalEntrada.Text.ToDateNullabel(),
                                                                                                              txtNumeroNfe.Text,
                                                                                                              txtRazaoSocialFornecedor.Text,
                                                                                                              status,2);

            preencherGrid();
        }

        private void preencherGrid()
        {
            List<EntradaAuxiliar> listaDeEntradasAuxiliares = new List<EntradaAuxiliar>();

            foreach (var entrada in _listaEntradasMercadorias)
            {
                EntradaAuxiliar entradaAuxiliar = new EntradaAuxiliar();

                entradaAuxiliar.Id = entrada.Id;
                entradaAuxiliar.DataEmissao = entrada.DataEmissao.GetValueOrDefault();
                entradaAuxiliar.DataEntrada = entrada.DataMovimentacao.GetValueOrDefault();
                entradaAuxiliar.NumeroNota = entrada.NumeroNota;
                entradaAuxiliar.RazaoSocialFornecedor = entrada.Fornecedor != null ? entrada.Fornecedor.DadosGerais.Razao : string.Empty;
                entradaAuxiliar.Status = entrada.StatusEntrada.Descricao();

                listaDeEntradasAuxiliares.Add(entradaAuxiliar);
            }

            gcNotasEntrada.DataSource = listaDeEntradasAuxiliares;
            gcNotasEntrada.RefreshDataSource();
        }

        private void ImprimaEntrada()
        {
            if (_listaEntradasMercadorias != null && _listaEntradasMercadorias.Count > 0)
            {
                RelatorioEntrada relatorioEntrada = new RelatorioEntrada(colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

                TratamentosDeTela.ExibirRelatorio(relatorioEntrada);
            }
        }

        private void PreenchaDatasInicialEFinal()
        {
            txtDataFinalEntrada.DateTime = DateTime.Now;
            txtDataInicialEntrada.DateTime = DateTime.Now.AddDays(-7);
        }

        private void PreenchaCboStatus()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusEntrada>();

            lista.Insert(0, null);

            cboStatus.Properties.DataSource = lista;
            cboStatus.Properties.DisplayMember = "Descricao";
            cboStatus.Properties.ValueMember = "Valor";

            cboStatus.EditValue = 0;
            cboStatus.EditValue = null;
        }

        private void configureParaOutrasSaidas(bool ehOutrasSaidas)
        {
            if (!ehOutrasSaidas) return;

            _ehOutrasSaidasConfig = ehOutrasSaidas;
            btnImprimir.Visible = false;
            btnSelecionar.Visible = true;
            labelControl1.Text = "NOTAS FISCAIS IMPORTADAS";
            this.Text = "PESQUISA NOTA FISCAL IMPORTADA";
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void SelecioneDocumento(bool ehOutrasSaidas)
        {
            if (!ehOutrasSaidas) return;

            var notaDocumentoSelecionado = _listaEntradasMercadorias.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

            if (notaDocumentoSelecionado != null)
            {
                ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

                var entrada = servicoEntradaMercadoria.Consulte(notaDocumentoSelecionado.Id);

                CarregueNotaFiscal(entrada);

                this.Close();

            }
        }

        private void CarregueNotaFiscal(EntradaMercadoria entrada)
        {            
            ConvertaDestinatarioEntradaParaNotaFiscal(entrada);

            ConvertaItensEntradaParaNotaFiscal(entrada);

            CarregaInformacoesNotasReferenciadas(entrada);

            if (_enviarNumeroNotaEntrada)
            {
                _notaFiscalSelecionada = _notaFiscalSelecionada ?? new NotaFiscal();
                _notaFiscalSelecionada.IdentificacaoNotaFiscal.NumeroNota = entrada.NumeroNota.ToInt();
            }
        }

        private void CarregaInformacoesNotasReferenciadas(EntradaMercadoria entrada)
        {
            _notaFiscalSelecionada.InformacoesGeraisNotaFiscal.ChaveDeAcesso = entrada.ChaveDeAcesso;
        }

        private void ConvertaDestinatarioEntradaParaNotaFiscal(EntradaMercadoria entrada)
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();

            var dadosFornecedor = servicoPessoa.Consulte(entrada.Fornecedor.Id);

            if (dadosFornecedor != null)
            {
                _notaFiscalSelecionada = new NotaFiscal();
                
                _notaFiscalSelecionada.Destinatario.Pessoa = new Negocio.Cadastros.PessoaObj.ObjetoDeNegocio.Pessoa();

                _notaFiscalSelecionada.Destinatario.Pessoa.Id = dadosFornecedor.Id;

                _notaFiscalSelecionada.Destinatario.TipoPessoa = dadosFornecedor.DadosGerais.TipoPessoa;

                _notaFiscalSelecionada.Destinatario.CnpjCpf = dadosFornecedor.DadosGerais.CpfCnpj;

                _notaFiscalSelecionada.Destinatario.RazaoSocialOuNomeDestinatario = dadosFornecedor.DadosGerais.Razao;

                _notaFiscalSelecionada.Destinatario.InscricaoEstadual = dadosFornecedor.EmpresaPessoa != null? dadosFornecedor.EmpresaPessoa.InscricaoEstadual:null;

                _notaFiscalSelecionada.Destinatario.Email = dadosFornecedor.EmpresaPessoa != null ? dadosFornecedor.EmpresaPessoa.EmailPrincipal:null;

                _notaFiscalSelecionada.Destinatario.IdEstrangeiro = dadosFornecedor.DadosPessoais != null? dadosFornecedor.DadosPessoais.IdEstrangeiro:null;

                _notaFiscalSelecionada.Destinatario.Telefone = dadosFornecedor.ListaDeTelefones.Count()!=0? dadosFornecedor.ListaDeTelefones.FirstOrDefault().Ddd!= null? (dadosFornecedor.ListaDeTelefones.FirstOrDefault().Ddd + dadosFornecedor.ListaDeTelefones.FirstOrDefault().Numero.Replace("-", "")).ToLongNullabel():null:null;

                _notaFiscalSelecionada.Destinatario.Cep = dadosFornecedor.ListaDeEnderecos.Count !=0? dadosFornecedor.ListaDeEnderecos.FirstOrDefault().CEP:null;

                _notaFiscalSelecionada.Destinatario.UF = dadosFornecedor.ListaDeEnderecos.Count != 0 ? dadosFornecedor.ListaDeEnderecos.FirstOrDefault().Cidade.Estado.UF:null;

                _notaFiscalSelecionada.Destinatario.CodigoMunicipio = dadosFornecedor.ListaDeEnderecos.Count != 0? dadosFornecedor.ListaDeEnderecos.FirstOrDefault().Cidade.CodigoIbge.ToLong():0;

                _notaFiscalSelecionada.Destinatario.Numero = dadosFornecedor.ListaDeEnderecos.Count != 0 ? dadosFornecedor.ListaDeEnderecos.FirstOrDefault().Numero:null;

                _notaFiscalSelecionada.Destinatario.Bairro = dadosFornecedor.ListaDeEnderecos.Count != 0 ? dadosFornecedor.ListaDeEnderecos.FirstOrDefault().Bairro:null;

                _notaFiscalSelecionada.Destinatario.Logradouro = dadosFornecedor.ListaDeEnderecos.Count != 0 ? dadosFornecedor.ListaDeEnderecos.FirstOrDefault().Rua:null;

                _notaFiscalSelecionada.Destinatario.Complemento = dadosFornecedor.ListaDeEnderecos.Count != 0 ? dadosFornecedor.ListaDeEnderecos.FirstOrDefault().Complemento:null;
            }
        }

        private void ConvertaItensEntradaParaNotaFiscal(EntradaMercadoria entrada)
        {
            ServicoEntradaMercadoria servicoEntradaMercadoria = new ServicoEntradaMercadoria();

            var entradaItens = servicoEntradaMercadoria.Consulte(entrada.Id);

            for (int i = 0; i < entradaItens.ListaDeItens.Count; i++)
            {
                var itemEntrada = entradaItens.ListaDeItens[i];

                ItemNotaFiscal itemNota = new ItemNotaFiscal();

                itemNota.Produto = itemEntrada.Produto;

                itemNota.Quantidade = itemEntrada.Quantidade;

                itemNota.UnidadeProduto = itemEntrada.Unidade.Descricao;

                //itemNota.Cfop = itemEntrada.Cfop.Codigo.ToInt(); Não vamos carregar. Deixar para o usuário informar

                itemNota.ValorUnitario = itemEntrada.ValorUnitario.ToDouble();

                itemNota.ValorDesconto = itemEntrada.ValorDesconto;

                itemNota.OutrasDespesas = itemEntrada.OutrasDespesas;

                itemNota.ValorFrete = itemEntrada.ValorFrete;

                itemNota.ValorTotal = itemEntrada.ValorTotal.ToDouble();

                itemNota.Produto.ContabilFiscal.Ncm = itemEntrada.Ncm;

                itemNota.Impostos = new ImpostosNotaFiscal();

                itemNota.Impostos.Icms = new IcmsNotaFiscal();

                itemNota.Impostos.Icms.CstCsosn = itemEntrada.CstCsosn.Value;
                
                itemNota.Impostos.Icms.MotivoDesoneracaoProduto = itemEntrada.MotivoDesoneracaoProduto;

                itemNota.Impostos.Icms.ValorDesoneracaoProduto = itemEntrada.ValorDesoneracaoProduto;

                itemNota.Impostos.Icms.BaseCalculoIcms = itemEntrada.BaseIcms;

                itemNota.Impostos.Icms.ValorIcms = itemEntrada.ValorIcms;

                itemNota.Impostos.Icms.AliquotaReducaoIcms = itemEntrada.PercentualReducao;

                itemNota.Impostos.Icms.AliquotaIcms = itemEntrada.PercentualIcms;

                itemNota.Impostos.Icms.PercentualMargemValorAdicST = itemEntrada.PercentualIva;

                itemNota.Impostos.Icms.AliquotaIva = itemEntrada.PercentualIva;

                itemNota.Impostos.Icms.BaseIcmsSubstituicaoTributaria = itemEntrada.BaseIcmsSt;

                itemNota.Impostos.Icms.AliquotaReducaoIcmsSubstituicaoTributaria = itemEntrada.AliquotaST;

                itemNota.Impostos.Icms.ValorSubstituicaoTributaria = itemEntrada.ValorIcmsSt;
                
                itemNota.Impostos.Ipi = new IpiNotaFiscal();

                itemNota.Impostos.Ipi.AliquotaIpi = itemEntrada.PercentualIpi;

                itemNota.Impostos.Ipi.ValorIpi = itemEntrada.ValorIpi;

                _notaFiscalSelecionada= _notaFiscalSelecionada ?? new NotaFiscal();

                _notaFiscalSelecionada.ListaItens.Add(itemNota);

            }
        }
      

    #endregion

    #region " CLASSES AUXILIARES "

    private class EntradaAuxiliar
        {
            public int Id { get; set; }

            public DateTime DataEntrada { get; set; }

            public DateTime DataEmissao { get; set; }

            public string NumeroNota { get; set; }

            public string RazaoSocialFornecedor { get; set; }

            public string Status { get; set; }
        }

        #endregion
                
    }
}
