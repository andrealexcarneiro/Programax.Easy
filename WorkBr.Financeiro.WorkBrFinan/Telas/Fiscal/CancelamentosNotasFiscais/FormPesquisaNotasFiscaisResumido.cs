using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Fiscal.CancelamentosNotasFiscais
{
    public partial class FormPesquisaNotasFiscaisResumido : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private NotaFiscal _notaFiscalSelecionada;

        private List<NotaFiscal> _listaVwNotasDocumentos;

        #endregion

        #region " CONSTRUTOR "

        public FormPesquisaNotasFiscaisResumido()
        {
            InitializeComponent();

            PreenchaCboTipoDocumento();
            PreenchaCboModeloNotaFiscal();
            PreenchaCboStatusNfe();

            //txtDataInicial.DateTime = DateTime.Now.AddMonths(0);
            txtDataInicial.DateTime = DateTime.Now.AddDays(-1);
            txtDataFinal.DateTime = DateTime.Now.AddDays(+1);

            Pesquise();

            this.ActiveControl = txtNumeroDocumento;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnPesquisaDocumentos_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            SelecioneDocumento();
        }

        private void gcDocumentos_DoubleClick(object sender, EventArgs e)
        {
            if (HouveUmClickOuDuploClickNaLinha(gridView5))
            {
                SelecioneDocumento();
            }
        }

        private void gcDocumentos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecioneDocumento();
            }
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public NotaFiscal PesquiseNotaFiscal()
        {
            this.ShowDialog();

            return _notaFiscalSelecionada;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboTipoDocumento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoDocumento>();

            lista.Insert(0, null);

            cboTipoDocumento.Properties.DataSource = lista;
            cboTipoDocumento.Properties.DisplayMember = "Descricao";
            cboTipoDocumento.Properties.ValueMember = "Valor";
        }

        private void PreenchaCboModeloNotaFiscal()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModeloNotaFiscal>();

            cboModeloNotaFiscal.Properties.DisplayMember = "Descricao";
            cboModeloNotaFiscal.Properties.ValueMember = "Valor";
            cboModeloNotaFiscal.Properties.DataSource = lista;

            cboModeloNotaFiscal.EditValue = EnumModeloNotaFiscal.NFE;
        }

        private void PreenchaCboStatusNfe()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusNotaFiscal>();

            lista.Insert(0, null);

            cboStatusNfe.Properties.DataSource = lista;
            cboStatusNfe.Properties.DisplayMember = "Descricao";
            cboStatusNfe.Properties.ValueMember = "Valor";

            cboStatusNfe.EditValue = EnumStatusNotaFiscal.AUTORIZADA;
        }


        private void Pesquise()
        {
            Cursor = Cursors.WaitCursor;

            int? numeroDocumento = txtNumeroDocumento.Text.ToIntNullabel();
            DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();
            EnumTipoDocumento? tipoDocumento = (EnumTipoDocumento?)cboTipoDocumento.EditValue;
            EnumStatusNotaFiscal? statusNotaFiscal = (EnumStatusNotaFiscal?)cboStatusNfe.EditValue;
            EnumModeloNotaFiscal? modelo = (EnumModeloNotaFiscal)cboModeloNotaFiscal.EditValue;
            int pedidoId;

            pedidoId = numeroDocumento != null ? numeroDocumento.ToInt() : 0;
            
            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();

            if (statusNotaFiscal == EnumStatusNotaFiscal.DISPONIVEL && tipoDocumento != EnumTipoDocumento.OUTRASSAIDAS)
                BusqueDocumentosDisponiveis(dataInicial, dataFinal, pedidoId);
            else
                _listaVwNotasDocumentos = servicoNotaFiscal.ConsulteListaDocumentos(numeroDocumento, dataInicial, dataFinal, tipoDocumento, statusNotaFiscal, modelo, null);
            
            PreenchaGrid(_listaVwNotasDocumentos);

            Cursor = Cursors.Default;
        }

        private void BusqueDocumentosDisponiveis(DateTime? dataInicial, DateTime? dataFinal, int pedidoId)
        {
            List<PedidoDeVenda> listaVenda = new List<PedidoDeVenda>();
            _listaVwNotasDocumentos = new List<NotaFiscal>();

            ServicoPedidoDeVenda servicoPedidoVenda = new ServicoPedidoDeVenda();

            if (pedidoId != 0)
                listaVenda.Add(servicoPedidoVenda.ConsultePedidoFaturadoOuEmitidoNfe(pedidoId));
            else
                listaVenda = servicoPedidoVenda.ConsulteLista(dataInicial, dataFinal, null, null, null, null, EnumStatusPedidoDeVenda.FATURADO);

            foreach (var pedido in listaVenda)
            {
                NotaFiscal NotaDisponivel = new NotaFiscal();

                if (pedido != null)
                {
                    NotaDisponivel.Destinatario.Pessoa = NotaDisponivel.Destinatario.Pessoa ?? new Pessoa();

                    NotaDisponivel.Destinatario.Pessoa.Id = pedido.Cliente.Id;
                    NotaDisponivel.Destinatario.CnpjCpf = pedido.Cliente.DadosGerais.CpfCnpj;
                    NotaDisponivel.Id = 0;
                    NotaDisponivel.InformacoesDocumentoOrigemNotaFiscal.DocumentoId = pedido.Id;
                    NotaDisponivel.IdentificacaoNotaFiscal.NumeroNota = 0;
                    NotaDisponivel.Destinatario.Pessoa.DadosGerais.NomeFantasia = pedido.Cliente.DadosGerais.NomeFantasia;
                    NotaDisponivel.IdentificacaoNotaFiscal.Serie = 0;
                    NotaDisponivel.InformacoesGeraisNotaFiscal.Status = EnumStatusNotaFiscal.DISPONIVEL;
                    NotaDisponivel.InformacoesDocumentoOrigemNotaFiscal.UsurioNome = pedido.Usuario.DadosGerais.Razao;
                    NotaDisponivel.InformacoesDocumentoOrigemNotaFiscal.UsuarioId = pedido.Usuario.Id;
                    NotaDisponivel.TotaisNotaFiscal.ValorNotaFiscal = pedido.ValorTotal;
                    NotaDisponivel.InformacoesDocumentoOrigemNotaFiscal.Origem = EnumTipoDocumento.PEDIDODEVENDAS;
                    NotaDisponivel.IdentificacaoNotaFiscal.DataHoraEmissao = DateTime.Parse("01/01/0001 00:00");
                    NotaDisponivel.Destinatario.StatusPessoa = pedido.Cliente.DadosGerais.Status;

                    pedido.Cliente.ListaDeEnderecos = pedido.Cliente.ListaDeEnderecos.Count == 0 ? new List<EnderecoPessoa>() : pedido.Cliente.ListaDeEnderecos;

                    NotaDisponivel.Destinatario.Cep = pedido.Cliente.ListaDeEnderecos.Count != 0 ? pedido.Cliente.ListaDeEnderecos.FirstOrDefault().CEP : null;
                    NotaDisponivel.Destinatario.NomeMunicipio = pedido.Cliente.ListaDeEnderecos.Count != 0 ? pedido.Cliente.ListaDeEnderecos.FirstOrDefault().Cidade != null ? pedido.Cliente.ListaDeEnderecos.FirstOrDefault().Cidade.Descricao : null : null;
                    NotaDisponivel.Destinatario.UF = pedido.Cliente.ListaDeEnderecos.Count != 0 ? pedido.Cliente.ListaDeEnderecos.FirstOrDefault().Cidade != null ? pedido.Cliente.ListaDeEnderecos.FirstOrDefault().Cidade.Estado.UF : null : null;

                    NotaDisponivel.Destinatario.InscricaoEstadual = pedido.Cliente.EmpresaPessoa.InscricaoEstadual;

                    NotaDisponivel.IdentificacaoNotaFiscal.NaturezaOperacao = NotaDisponivel.IdentificacaoNotaFiscal.NaturezaOperacao ?? new NaturezaOperacao();
                    NotaDisponivel.IdentificacaoNotaFiscal.NaturezaOperacao = pedido.NaturezaOperacao;

                    NotaDisponivel.Destinatario.Pessoa.DadosGerais.TipoCliente = pedido.Cliente.DadosGerais.TipoCliente;
                    NotaDisponivel.Destinatario.DataCadastroPessoa = pedido.Cliente.DadosGerais.DataCadastro;

                    NotaDisponivel.Destinatario.Pessoa.Atendimento.Atendente = pedido.Atendente;
                    NotaDisponivel.Destinatario.Pessoa.Atendimento.Vendedor = pedido.Vendedor;

                    NotaDisponivel.InformacoesDocumentoOrigemNotaFiscal.DataElaboracao = pedido.DataElaboracao;

                    NotaDisponivel.InformacoesGeraisNotaFiscal.MensagemDevolvida = null;
                    NotaDisponivel.InformacoesGeraisNotaFiscal.MensagemDeErro = null;

                    _listaVwNotasDocumentos.Add(NotaDisponivel);
                }
            }
        }

        private void PreenchaGrid(List<NotaFiscal> listaVwNotasDocumentos)
        {
            List<VwNotaGrid> listaVwNotaGrid = new List<VwNotaGrid>();

            foreach (var vwnota in listaVwNotasDocumentos)
            {
                VwNotaGrid vwnotaGrid = new VwNotaGrid();
                if (vwnota.Destinatario != null)
                {
                    vwnotaGrid.CodigoCliente = vwnota.Destinatario.Pessoa.Id.ToString();
                    vwnotaGrid.CpfCnpj = vwnota.Destinatario.CnpjCpf;
                    vwnotaGrid.Parceiro = vwnota.Destinatario.Pessoa.DadosGerais.NomeFantasia;
                }

                vwnotaGrid.Id = vwnota.Id;
                vwnotaGrid.NumeroDocumento = vwnota.InformacoesDocumentoOrigemNotaFiscal.DocumentoId.ToString();
                vwnotaGrid.NumeroNfe = vwnota.IdentificacaoNotaFiscal.NumeroNota.ToInt() != 0 ? vwnota.IdentificacaoNotaFiscal.NumeroNota.ToString() : null;

                vwnotaGrid.Serie = vwnota.IdentificacaoNotaFiscal.Serie.ToInt() != 0 ? vwnota.IdentificacaoNotaFiscal.Serie.ToString() : null;
                vwnotaGrid.StatusNFe = vwnota.InformacoesGeraisNotaFiscal.Status.Descricao();
                vwnotaGrid.Usuario = vwnota.InformacoesDocumentoOrigemNotaFiscal.UsurioNome;
                vwnotaGrid.ValorTotal = vwnota.TotaisNotaFiscal.ValorNotaFiscal.ToString("#,###,##0.00"); ;
                vwnotaGrid.TipoDocumento = vwnota.InformacoesDocumentoOrigemNotaFiscal.Origem.Descricao();
                vwnotaGrid.DataEmissao = vwnota.IdentificacaoNotaFiscal.DataHoraEmissao != null ? vwnota.IdentificacaoNotaFiscal.DataHoraEmissao.ToString("dd/MM/yyyy") : string.Empty;
                vwnotaGrid.DataEmissao = vwnotaGrid.DataEmissao == "01/01/0001" ? null : vwnotaGrid.DataEmissao;
                listaVwNotaGrid.Add(vwnotaGrid);
            }

            gcDocumentos.DataSource = listaVwNotaGrid;
            gcDocumentos.RefreshDataSource();
        }

        private void SelecioneDocumento()
        {
            var vwNotaDocumentoSelecionado = _listaVwNotasDocumentos.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

            if (vwNotaDocumentoSelecionado != null)
            {
                ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
                _notaFiscalSelecionada = servicoNotaFiscal.Consulte(vwNotaDocumentoSelecionado.Id);

                this.FecharFormulario();
            }
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class VwNotaGrid
        {
            public int Id { get; set; }

            public string NumeroNfe { get; set; }

            public string Serie { get; set; }

            public string CodigoCliente { get; set; }

            public string CpfCnpj { get; set; }

            public string Parceiro { get; set; }

            public string NumeroDocumento { get; set; }

            public string Usuario { get; set; }

            public string ValorTotal { get; set; }

            public string StatusNFe { get; set; }

            public string DataEmissao { get; set; }

            public string TipoDocumento { get; set; }
        }

        #endregion

        private void gcDocumentos_Click(object sender, EventArgs e)
        {

        }
    }
}
