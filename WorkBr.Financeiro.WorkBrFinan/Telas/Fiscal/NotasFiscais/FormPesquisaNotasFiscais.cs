using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using NFe.Servicos;
using NFe.Servicos.Retorno;
using NFe.Utils.NFe;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;

namespace Programax.Easy.View.Telas.Fiscal.NotasFiscais
{
    public partial class FormPesquisaNotasFiscais : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private NotaFiscal _documentoSelecionado;
        private List<NotaFiscal> _listaVwNotasDocumentos;
        private List<VwNotaGrid> _listaNotasSelecionadas = new List<VwNotaGrid>();
        private Parametros _parametros;

        #endregion
               
        #region " CONSTRUTOR "

        public FormPesquisaNotasFiscais()
        {            
            InitializeComponent();

            PreenchaCboTipoDocumento();
            PreenchaCboStatusNfe();
            PreenchaCboModeloNotaFiscal();
            PreenchaCboTipoDeEmissao();

            txtDataInicial.DateTime = DateTime.Now;
            txtDataFinal.DateTime = DateTime.Now;

            _parametros = new ServicoParametros().ConsulteParametros();

            Pesquise();

            this.ActiveControl = txtNumeroDocumento;            
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnPesquisaDocumentos_Click(object sender, EventArgs e)
        {
            Pesquise();
        }

        private void gcDocumentos_Click(object sender, EventArgs e)
        {
            SelecioneDocumento();
        }

        private void gcDocumentos_KeyUp(object sender, KeyEventArgs e)
        {
            SelecioneDocumento();

            if (e.KeyCode == Keys.Enter)
            {
                EditeDocumento();
            }
        }

        private void gcDocumentos_DoubleClick(object sender, EventArgs e)
        {
            SelecioneDocumento();
            EditeDocumento();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnGerarEEnviarNFe_Click(object sender, EventArgs e)
        {
            EditeDocumento();
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

        private void PreenchaCboStatusNfe()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumStatusNotaFiscal>();

            lista.Insert(0, null);

            cboStatusNfe.Properties.DataSource = lista;
            cboStatusNfe.Properties.DisplayMember = "Descricao";
            cboStatusNfe.Properties.ValueMember = "Valor";

            cboStatusNfe.EditValue = EnumStatusNotaFiscal.DISPONIVEL;
        }

        private void PreenchaCboModeloNotaFiscal()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumModeloNotaFiscal>();

            lista.Insert(0, null);

            cboModeloNotaFiscal.Properties.DisplayMember = "Descricao";
            cboModeloNotaFiscal.Properties.ValueMember = "Valor";
            cboModeloNotaFiscal.Properties.DataSource = lista;            
        }

        private void PreenchaCboTipoDeEmissao()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoDeEmissaoPesquisa>();

            lista.Insert(0, null);

            cboTipoDeEmissao.Properties.DisplayMember = "Descricao";
            cboTipoDeEmissao.Properties.ValueMember = "Valor";
            cboTipoDeEmissao.Properties.DataSource = lista;

            cboTipoDeEmissao.EditValue = EnumTipoDeEmissaoPesquisa.NORMAL;
        }

        private void Pesquise()
        {
            this.Cursor = Cursors.WaitCursor;
            int? numeroDocumento = txtNumeroDocumento.Text.ToIntNullabel();
            DateTime? dataInicial = txtDataInicial.Text.ToDateNullabel();
            DateTime? dataFinal = txtDataFinal.Text.ToDateNullabel();
            EnumTipoDocumento? tipoDocumento = (EnumTipoDocumento?)cboTipoDocumento.EditValue;
            EnumStatusNotaFiscal? statusNotaFiscal = (EnumStatusNotaFiscal?)cboStatusNfe.EditValue;
            EnumModeloNotaFiscal? modelo = (EnumModeloNotaFiscal?)cboModeloNotaFiscal.EditValue;
            EnumTipoDeEmissaoPesquisa? tipoDeEmissao = (EnumTipoDeEmissaoPesquisa?)cboTipoDeEmissao.EditValue;
            int pedidoId;

            pedidoId = numeroDocumento != null? numeroDocumento.ToInt():0;

            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();


            if (statusNotaFiscal == EnumStatusNotaFiscal.DISPONIVEL && tipoDocumento != EnumTipoDocumento.OUTRASSAIDAS)
                BusqueDocumentosDisponiveis(dataInicial, dataFinal, pedidoId);
            else
                _listaVwNotasDocumentos = servicoNotaFiscal.ConsulteListaDocumentos(numeroDocumento, dataInicial, dataFinal, tipoDocumento, statusNotaFiscal, modelo, tipoDeEmissao);

            GereIdParaDocumentosComIdZero();

            PreenchaGrid();

            TotalizeNotas();

            PreenchaCamposDocumento(null);
            this.Cursor = Cursors.Default;
        }

        private void BusqueDocumentosDisponiveis(DateTime? dataInicial, DateTime? dataFinal, int pedidoId)
        {
            List<PedidoDeVenda> listaVenda = new List<PedidoDeVenda>();
            _listaVwNotasDocumentos = new List<NotaFiscal>();

            ServicoPedidoDeVenda servicoPedidoVenda = new ServicoPedidoDeVenda();
            
            if (pedidoId != 0)
                if (!_parametros.ParametrosFiscais.EmitirNotaSemReceber)
                    listaVenda.Add(servicoPedidoVenda.ConsultePedidoFaturadoOuEmitidoNfe(pedidoId));
                else
                    listaVenda.Add(servicoPedidoVenda.ConsultePedidoFaturadoOuEmitidoNfe(pedidoId, true));
            else
            {
                if(!_parametros.ParametrosFiscais.EmitirNotaSemReceber)
                    listaVenda = servicoPedidoVenda.ConsulteLista(dataInicial, dataFinal, null, null, null, null, EnumStatusPedidoDeVenda.FATURADO);
                else
                    listaVenda = servicoPedidoVenda.ConsulteLista(dataInicial, dataFinal, null, null, null, null, EnumStatusPedidoDeVenda.FATURADO, null, true);
            }
            
            foreach(var pedido in listaVenda)
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

                    NotaDisponivel.Destinatario.InscricaoEstadual = pedido.Cliente.EmpresaPessoa != null ? pedido.Cliente.EmpresaPessoa.InscricaoEstadual : null;
                    
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

        private void PreenchaGrid()
        {
            List<VwNotaGrid> listaVwNotaGrid = new List<VwNotaGrid>();

            foreach (var vwnota in _listaVwNotasDocumentos)
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
                vwnotaGrid.ValorTotal = vwnota.TotaisNotaFiscal.ValorNotaFiscal;
                vwnotaGrid.TipoDocumento = vwnota.InformacoesDocumentoOrigemNotaFiscal.Origem.Descricao();
                vwnotaGrid.DataEmissao = vwnota.IdentificacaoNotaFiscal.DataHoraEmissao != null ? vwnota.IdentificacaoNotaFiscal.DataHoraEmissao.ToString("dd/MM/yyyy") : string.Empty;
                vwnotaGrid.DataEmissao = vwnotaGrid.DataEmissao == "01/01/0001" ? null : vwnotaGrid.DataEmissao;

                listaVwNotaGrid.Add(vwnotaGrid);
            }

            gcDocumentos.DataSource = listaVwNotaGrid;
            gcDocumentos.RefreshDataSource();
        }

        private void GereIdParaDocumentosComIdZero()
        {
            if (_listaVwNotasDocumentos.Count > 0)
            {
                var proximoId = _listaVwNotasDocumentos.Max(x => x.Id) + 1;

                for (int i = 0; i < _listaVwNotasDocumentos.Count; i++)
                {
                    var notaDocumento = _listaVwNotasDocumentos[i];

                    if (notaDocumento.Id == 0)
                    {
                        notaDocumento.Id = proximoId;

                        proximoId++;
                    }
                }
            }
        }

        private void SelecioneDocumento()
        {
            _listaNotasSelecionadas.Clear();

            bool todasNotasEstaoAutorizadas = true;

            var linhasSelecionadas = colunaId.View.GetSelectedRows();

            foreach (var item in linhasSelecionadas)
            {
                VwNotaGrid notaGrid = (VwNotaGrid)colunaId.View.GetRow(item);

                if (notaGrid.StatusNFe != "AUTORIZADA")
                {
                    todasNotasEstaoAutorizadas = false;
                    break;
                }

                _listaNotasSelecionadas.Add(notaGrid);
            }

            if (linhasSelecionadas.Length > 1)
            {
                btnVisualizarNfe.Visible = false;
                btnGerarEEnviarNFe.Visible = false;

                if (todasNotasEstaoAutorizadas)
                {
                    btnSalvarXml.Visible = true;
                }
            }
            else
            {   
                _documentoSelecionado = _listaVwNotasDocumentos.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());
                
                PreenchaCamposDocumento(_documentoSelecionado);
            }
        }

        private void PreenchaCamposDocumento(NotaFiscal documento)
        {
            if (documento != null)
            {
                if (documento.Destinatario != null)
                {                    
                    txtIdCliente.Text = documento.Destinatario.Pessoa.Id.ToString();
                    txtNomeCliente.Text = documento.Destinatario.Pessoa.DadosGerais.NomeFantasia;
                    txtCpfCnpj.Text = documento.Destinatario.CnpjCpf;
                    txtStatusCliente.Text = documento.Destinatario.StatusPessoa == "A" ? "ATIVO" : "INATIVO";

                    txtCep.Text = documento.Destinatario.Cep;
                    txtCidade.Text = documento.Destinatario.NomeMunicipio;
                    txtEstado.Text = documento.Destinatario.UF;

                    txtInscricaoEstadual.Text = documento.Destinatario.InscricaoEstadual;
                    
                    txtNaturezaOperacao.Text = documento.IdentificacaoNotaFiscal.NaturezaOperacao != null ? documento.IdentificacaoNotaFiscal.NaturezaOperacao.Descricao:null;

                    txtTipoPessoa.Text = documento.Destinatario.Pessoa.DadosGerais.TipoCliente.Descricao();
                    txtDataCadastroCliente.Text = documento.Destinatario.DataCadastroPessoa.ToString("dd/M/yyyy");
                    
                    if (!NullReferenceException.ReferenceEquals(documento.Destinatario.Pessoa.Atendimento, null))
                    {
                        txtAtendente.Text = documento.Destinatario.Pessoa.Atendimento.Atendente != null ? documento.Destinatario.Pessoa.Atendimento != null ? documento.Destinatario.Pessoa.Atendimento.Atendente.Id + " - " + documento.Destinatario.Pessoa.Atendimento.Atendente.DadosGerais.Razao : null : null;
                    }
                    else
                        txtAtendente.Text = string.Empty;
                    
                    if (!NullReferenceException.ReferenceEquals(documento.Destinatario.Pessoa.Atendimento, null))
                    {
                        txtVendedor.Text = documento.Destinatario.Pessoa.Atendimento.Vendedor != null ? documento.Destinatario.Pessoa.Atendimento.Vendedor.Id + " - " + documento.Destinatario.Pessoa.Atendimento.Vendedor.DadosGerais.Razao : null;
                    }
                    else
                        txtVendedor.Text = string.Empty;
                }

                txtDataElaboracao.Text = documento.InformacoesDocumentoOrigemNotaFiscal.DataElaboracao.ToString("dd/MM/yyyy");
                txtUsuario.Text = documento.InformacoesDocumentoOrigemNotaFiscal.UsuarioId + " - " + documento.InformacoesDocumentoOrigemNotaFiscal.UsurioNome;

                lblMensagemDevolvida.Text = documento.InformacoesGeraisNotaFiscal.MensagemDevolvida;
                lblMensagemErro.Text = documento.InformacoesGeraisNotaFiscal.MensagemDeErro;

                if (documento.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.AUTORIZADA || documento.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.PROCESSANDO)
                {
                    btnVisualizarNfe.Visible = true;
                    btnGerarEEnviarNFe.Visible = false;

                    if (documento.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.AUTORIZADA)
                    {
                        btnSalvarXml.Visible = true;
                    }
                    else
                    {
                        btnSalvarXml.Visible = false;
                    }
                }
                else
                {
                    btnGerarEEnviarNFe.Visible = true;
                    btnVisualizarNfe.Visible = false;
                    btnSalvarXml.Visible = false;
                }
            }
            else
            {
                txtIdCliente.Text = string.Empty;
                txtNomeCliente.Text = string.Empty;
                txtCpfCnpj.Text = string.Empty;
                txtStatusCliente.Text = string.Empty;

                txtCep.Text = string.Empty;
                txtCidade.Text = string.Empty;
                txtEstado.Text = string.Empty;

                txtInscricaoEstadual.Text = string.Empty;
                txtNaturezaOperacao.Text = string.Empty;
                txtTipoPessoa.Text = string.Empty;
                txtDataCadastroCliente.Text = string.Empty;

                txtAtendente.Text = string.Empty;
                txtVendedor.Text = string.Empty;

                txtDataElaboracao.Text = string.Empty;
                txtUsuario.Text = string.Empty;

                lblMensagemDevolvida.Text = string.Empty;
                lblMensagemErro.Text = string.Empty;

                btnVisualizarNfe.Visible = false;
                btnGerarEEnviarNFe.Visible = false;
            }
        }

        private void TotalizeNotas()
        {
            txtQtdNfeProcessando.Text = _listaVwNotasDocumentos.Count(vwnota => vwnota.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.PROCESSANDO).ToString();
            txtQtdNfeRejeitadas.Text = _listaVwNotasDocumentos.Count(vwnota => vwnota.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.REJEITADA).ToString();
            txtQtdNfeDenegadas.Text = _listaVwNotasDocumentos.Count(vwnota => vwnota.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.DENEGADA).ToString();
            txtQtdNfeDisponiveis.Text = _listaVwNotasDocumentos.Count(vwnota => vwnota.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.DISPONIVEL).ToString();

            txtQtdNfeProblemasTransmissao.Text = _listaVwNotasDocumentos.Count(vwnota => vwnota.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.PROBLEMATRANSMISSAO).ToString();
            txtQtdNfeCanceladas.Text = _listaVwNotasDocumentos.Count(vwnota => vwnota.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.CANCELADA).ToString();
            txtQtdNfeAutorizadas.Text = _listaVwNotasDocumentos.Count(vwnota => vwnota.InformacoesGeraisNotaFiscal.Status == EnumStatusNotaFiscal.AUTORIZADA).ToString();
            txtTotalNfe.Text = _listaVwNotasDocumentos.Count().ToString();
        }

        private void EditeDocumento()
        {            
            FormCadastroNotaFiscal formCadastroNotalFiscal = new FormCadastroNotaFiscal();
            formCadastroNotalFiscal.EditeVwNotaDocumento(_documentoSelecionado);
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

            public double ValorTotal { get; set; }

            public string StatusNFe { get; set; }

            public string DataEmissao { get; set; }

            public string TipoDocumento { get; set; }
        }

        #endregion

        private void btnSalvarXml_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            var resultado = folderBrowserDialog.ShowDialog();

            if (resultado != DialogResult.OK)
            {
                return;
            }

            string diretorio = folderBrowserDialog.SelectedPath;

            List<int> listaIds = _listaNotasSelecionadas.Select<VwNotaGrid, int>(x => x.Id.ToInt()).ToList();
            List<int> listaIdnota = _listaNotasSelecionadas.Select<VwNotaGrid, int>(x => x.NumeroNfe.ToInt()).ToList();

            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
            List<NotaFiscal> listaNotasFiscais = servicoNotaFiscal.ConsulteListaComJoinItens(listaIds);

            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe();
            
            ConversorNotaFiscalAkilParaZeus conversorNotaFiscalAkilParaZeus = new ConversorNotaFiscalAkilParaZeus();

            foreach (var notaId in listaIdnota)
            {
                var nota = servicoNotaFiscal.Consulte(1, notaId, EnumStatusNotaFiscal.AUTORIZADA);

                var notaFiscalZeus = conversorNotaFiscalAkilParaZeus.ConvertaNotaAutorizadaAkilParaZeus(nota);

                servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus((EnumModeloNotaFiscal)nota.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

                notaFiscalZeus.NFe.Assina();

                string nomeNotaFiscal = nota.InformacoesGeraisNotaFiscal.ChaveDeAcesso + ".xml";

                notaFiscalZeus.SalvarArquivoXml(diretorio + @"\" + nomeNotaFiscal);
            }
        }
    }
}
