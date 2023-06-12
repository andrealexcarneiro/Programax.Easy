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
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Servico.Cadastros.PaisServ;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Servico.Cadastros.EnderecoServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ.ClassesAuxiliares;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PaisObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;

namespace Programax.Easy.View.Telas.Fiscal.NotasFiscais
{
    public partial class FormConfirmacaoDadosNotaFiscal : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private PedidoDeVenda _pedidoVenda;
        private ServicoCidade _servicoCidade;
        private List<Cidade> _listaCidades;
        private List<Pais> _listaPaises;
        private string _cepAtual;
        private bool _EhContribuite;

        private EnumTipoInscricaoICMS _inscricaoIcms;

        private DialogResult _resultado;

        #endregion

        #region " PROPRIEDADES "

        public DestinatarioAuxiliarNotaFiscal DestinatarioAuxiliarNotaFiscal { get; set; }
        public EnumModeloNotaFiscal modeloEmissaoNotaFiscal { get; set; }
        public bool notaComDesconto { get; set; }
                
        #endregion

        #region " CONSTRUTOR "

        public FormConfirmacaoDadosNotaFiscal(int pedidoDeVendaId, EnumStatusNotaFiscal statusNF = EnumStatusNotaFiscal.DISPONIVEL, EnumModeloNotaFiscal modeloNF = EnumModeloNotaFiscal.NFE)
        {
            InitializeComponent();

            _servicoCidade = new ServicoCidade();
            
            PreenchaCboTipoDePessoa();
            PreenchaCboPaises();
            PreenchaCboEstados();
            PreenchaCboUFLocalRetirada();

            PreenchaCamposPedido(pedidoDeVendaId);
            
            if (!VerificaStatusNotaFiscal(statusNF, modeloNF))
                SelecionaEmissaoNFPadrao();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void cboTipoPessoa_EditValueChanged(object sender, EventArgs e)
        {
            AltereOTipoDaPessoa();
        }

        private void txtNumeroTelefone_KeyUp(object sender, KeyEventArgs e)
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

            string numero = txtNumeroTelefone.Text;
            int posicaoCursor = txtNumeroTelefone.SelectionStart;

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

            txtNumeroTelefone.Text = numero;
            txtNumeroTelefone.SelectionStart = posicaoCursor;
        }

        private void cboEstado_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCidades();
            cboCidade.EditValue = null;

            if (cboEstado.EditValue == null) return; 

            if (cboEstado.EditValue.ToString() != "GO")
            {
                //rdbNFCe.Checked = false;
                //rdbNFe.Checked = true;
                //rdbNFCe.Enabled = false;
            }
            else
            {   
                rdbNFCe.Enabled = true;
            }

            if (_EhContribuite)
                rdbNFCe.Enabled = false;
        }

        private void txtCepEndereco_Leave(object sender, EventArgs e)
        {
            if (_cepAtual.Equals(txtCep.Text))
            {
                return;
            }

            _cepAtual = txtCep.Text;

            ServicoEndereco servicoEndereco = new ServicoEndereco();
            var endereco = servicoEndereco.Consulte(txtCep.Text);

            if (endereco != null)
            {
                txtBairro.Text = endereco.Bairro;
                txtRua.Text = endereco.Rua;
                txtCep.Text = endereco.CEP;

                if (endereco.Cidade != null)
                {
                    if (endereco.Cidade.Estado != null)
                    {
                        cboEstado.EditValue = endereco.Cidade.Estado.UF;
                    }

                    cboCidade.EditValue = endereco.Cidade.Id;
                }
            }
        }

        private void chkParceiroResideExterior_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParceiroResideExterior.Checked)
            {
                cboPaisParceiro.Enabled = true;

                txtIdEstrangeiro.Enabled = true;

                txtCep.Enabled = false;
                cboEstado.Enabled = false;
                cboCidade.Enabled = false;
                txtBairro.Enabled = false;
                txtRua.Enabled = false;
                txtComplementoEndereco.Enabled = false;
                txtNumeroEndereco.Enabled = false;

                txtCpfCnpj.Enabled = false;
                txtInscricaoEstadual.Enabled = false;

                txtCep.Text = string.Empty;
                cboEstado.EditValue = null;
                cboCidade.EditValue = null;
                txtBairro.Text = string.Empty;
                txtRua.Text = string.Empty;
                txtComplementoEndereco.Text = string.Empty;
                txtNumeroEndereco.Text = string.Empty;

                txtCpfCnpj.Text = string.Empty;
                txtInscricaoEstadual.Text = string.Empty;

                //Informações de Embarque
                cboUFComercioExterior.Enabled = true;
                txtDescricaoLocalDespachoComercioExterior.Enabled = true;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Enabled = true;

                cboUFComercioExterior.Obrigatorio = true;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Obrigatorio = true;
            }
            else
            {
                cboPaisParceiro.Enabled = false;
                cboPaisParceiro.EditValue = null;

                txtIdEstrangeiro.Enabled = false;
                txtIdEstrangeiro.Text = string.Empty;

                txtCep.Enabled = true;
                cboEstado.Enabled = true;
                cboCidade.Enabled = true;
                txtBairro.Enabled = true;
                txtRua.Enabled = true;
                txtComplementoEndereco.Enabled = true;
                txtNumeroEndereco.Enabled = true;

                txtCpfCnpj.Enabled = true;
                txtInscricaoEstadual.Enabled = true;

                //Informações de Embarque
                cboUFComercioExterior.Enabled = false;
                txtDescricaoLocalDespachoComercioExterior.Enabled = false;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Enabled = false;

                cboUFComercioExterior.Obrigatorio = false;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Obrigatorio = false;

                cboUFComercioExterior.Text = string.Empty;
                txtDescricaoLocalDespachoComercioExterior.Text = string.Empty;
                txtDescricaoLocalEmbarqueFronteiraComercioExterior.Text = string.Empty;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Conclua();
        }

        private void FormConfirmacaoDadosNotaFiscal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F3)
            {
                Conclua();
            }
        }

        private void rdbNFe_CheckedChanged(object sender, EventArgs e)
        {
            rdbNFe.Font = new Font(rdbNFe.Font, FontStyle.Bold);
            rdbNFCe.Font = new Font(rdbNFe.Font, FontStyle.Regular);

            rdbConsumidorFinal.Checked = true;
            rdbRevenda.Enabled = true;
        }

        private void rdbNFCe_CheckedChanged(object sender, EventArgs e)
        {
            rdbNFCe.Font = new Font(rdbNFCe.Font, FontStyle.Bold);
            rdbNFe.Font = new Font(rdbNFe.Font, FontStyle.Regular);

            rdbConsumidorFinal.Checked = true;
            rdbRevenda.Enabled = false;
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult ConfirmeDadosDestinatario()
        {
            this.AbrirTelaModal();

            return _resultado;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboUFLocalRetirada()
        {
            List<Estado> listaEstados = new List<Estado>();
                        
            ServicoEstado servicoEstado = new ServicoEstado();
            listaEstados = servicoEstado.ConsulteListaEstados();
            
            var lista = listaEstados.CloneCompleto();

            lista.Insert(0, null);

            cboUFComercioExterior.Properties.DataSource = lista;
            cboUFComercioExterior.Properties.DisplayMember = "UF";
            cboUFComercioExterior.Properties.ValueMember = "UF";
        }

        private void PreenchaCboPaises()
        {
            ServicoPais servicoPais = new ServicoPais();
            _listaPaises = servicoPais.ConsulteLista();

            var lista = _listaPaises.CloneCompleto();
            lista.Insert(0, null);

            cboPaisParceiro.Properties.DataSource = lista;
            cboPaisParceiro.Properties.ValueMember = "Id";
            cboPaisParceiro.Properties.DisplayMember = "NomePais";
        }

        private void PreenchaCboEstados()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var listaDeEstados = servicoEstado.ConsulteListaEstados();

            listaDeEstados.Insert(0, null);

            cboEstado.Properties.DataSource = listaDeEstados;
            cboEstado.Properties.DisplayMember = "Nome";
            cboEstado.Properties.ValueMember = "UF";
        }

        private void PreenchaCboCidades()
        {
            string uf = cboEstado.EditValue == null ? string.Empty : cboEstado.EditValue.ToString();

            _listaCidades = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(uf);

            var lista = _listaCidades.CloneCompleto();

            lista.Insert(0, null);

            cboCidade.Properties.DataSource = lista;
            cboCidade.Properties.DisplayMember = "Descricao";
            cboCidade.Properties.ValueMember = "Id";
        }

        private void AltereOTipoDaPessoa()
        {
            if ((EnumTipoPessoa)cboTipoPessoa.EditValue == EnumTipoPessoa.PESSOAFISICA)
            {
                FacaAlteracoesParaTipoPessoaFisica();
            }
            else
            {
                FacaAlteracoesParaTipoPessoaJuridica();
            }
        }

        private void FacaAlteracoesParaTipoPessoaFisica()
        {
            lblRazaoSocial.Text = "Nome";

            lblCpfCnpj.Text = "CPF";
            txtCpfCnpj.Text = string.Empty;
            txtCpfCnpj.Properties.Mask.EditMask = "000.000.000-00";
        }

        private void FacaAlteracoesParaTipoPessoaJuridica()
        {
            lblRazaoSocial.Text = "Razão Social";

            lblCpfCnpj.Text = "CNPJ";
            txtCpfCnpj.Text = string.Empty;
            txtCpfCnpj.Properties.Mask.EditMask = "00.000.000/0000-00";
        }

        private void PreenchaCboTipoDePessoa()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPessoa>();

            cboTipoPessoa.Properties.DataSource = lista;
            cboTipoPessoa.Properties.ValueMember = "Valor";
            cboTipoPessoa.Properties.DisplayMember = "Descricao";

            cboTipoPessoa.EditValue = EnumTipoPessoa.PESSOAFISICA;
        }

        private void PreenchaCamposPedido(int pedidoDeVendaId)
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            _pedidoVenda = servicoPedidoDeVenda.Consulte(pedidoDeVendaId);

            _inscricaoIcms = _pedidoVenda.Cliente.EmpresaPessoa != null ?
                                                                  _pedidoVenda.Cliente.EmpresaPessoa.TipoInscricaoICMS != null ?
                                                                  (EnumTipoInscricaoICMS)_pedidoVenda.Cliente.EmpresaPessoa.TipoInscricaoICMS
                                                                  : EnumTipoInscricaoICMS.CONTRIBUINTEICMS
                                                                  : EnumTipoInscricaoICMS.CONTRIBUINTEICMS;


            //Alteração feita para que quando for NFCe (modelo 65) e o destinatário for nulo deverá pegar as
            //informações de destinatário no pedido de venda da NFCe corrente.
            if (_pedidoVenda.NotaFiscal == null || 
                                              (_pedidoVenda.NotaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal == 65 && 
                                              _pedidoVenda.NotaFiscal.Destinatario==null))
            {
                cboTipoPessoa.EditValue = _pedidoVenda.Cliente.DadosGerais.TipoPessoa;
                txtRazaoSocial.Text = _pedidoVenda.Cliente.DadosGerais.Razao;
                txtCpfCnpj.Text = _pedidoVenda.Cliente.DadosGerais.CpfCnpj;

                if (_pedidoVenda.TipoCliente == EnumTipoCliente.CONSUMIDOR)
                {
                    rdbConsumidorFinal.Checked = true;
                }
                else
                {
                    rdbRevenda.Checked = true;
                }

                txtInscricaoEstadual.Text = _pedidoVenda.Cliente.EmpresaPessoa != null ? _pedidoVenda.Cliente.EmpresaPessoa.InscricaoEstadual : null;

                txtEmail.Text = _pedidoVenda.Cliente.EmpresaPessoa != null ? _pedidoVenda.Cliente.EmpresaPessoa.EmailPrincipal : null;
                txtIdEstrangeiro.Text = _pedidoVenda.Cliente.EmpresaPessoa != null ? _pedidoVenda.Cliente.DadosPessoais.IdEstrangeiro : null;
                
                txtCep.Text = _pedidoVenda.EnderecoPedidoDeVenda.CEP;
                _cepAtual = txtCep.Text;

                cboEstado.EditValue = _pedidoVenda.EnderecoPedidoDeVenda.Cidade!= null? _pedidoVenda.EnderecoPedidoDeVenda.Cidade.Estado.UF:null;
                cboCidade.EditValue = _pedidoVenda.EnderecoPedidoDeVenda.Cidade!= null? _pedidoVenda.EnderecoPedidoDeVenda.Cidade.Id:0;

                txtBairro.Text = _pedidoVenda.EnderecoPedidoDeVenda.Bairro;
                txtRua.Text = _pedidoVenda.EnderecoPedidoDeVenda.Rua;
                txtComplementoEndereco.Text = _pedidoVenda.EnderecoPedidoDeVenda.Complemento;
                txtNumeroEndereco.Text = _pedidoVenda.EnderecoPedidoDeVenda.Numero;

                chkParceiroResideExterior.Checked = _pedidoVenda.Cliente.DadosGerais.PessoaResideExterior;

                if (_pedidoVenda.Cliente.DadosGerais.Pais != null)
                {
                    cboPaisParceiro.EditValue = _pedidoVenda.Cliente.DadosGerais.Pais.Id;
                }

                var telefone = _pedidoVenda.Cliente.ListaDeTelefones.FirstOrDefault();

                if (telefone != null)
                {
                    txtDdd.Text = telefone.Ddd.GetValueOrDefault().ToString();
                    txtNumeroTelefone.Text = telefone.Numero;
                }
            }
            else
            {
                var destinatario = _pedidoVenda.NotaFiscal.Destinatario;

                cboTipoPessoa.EditValue = destinatario.TipoPessoa;
                txtRazaoSocial.Text = destinatario.RazaoSocialOuNomeDestinatario;
                txtCpfCnpj.Text = destinatario.CnpjCpf;

                if (_pedidoVenda.NotaFiscal.IdentificacaoNotaFiscal.ConsumidorFinal)
                {
                    rdbConsumidorFinal.Checked = true;
                }
                else
                {
                    rdbRevenda.Checked = true;
                }

                txtInscricaoEstadual.Text = destinatario.InscricaoEstadual;
                txtEmail.Text = destinatario.Email;
                txtIdEstrangeiro.Text = destinatario.IdEstrangeiro;

                txtCep.Text = destinatario.Cep;
                _cepAtual = txtCep.Text;

                cboEstado.EditValue = destinatario.UF;
                cboCidade.EditValue = _pedidoVenda.EnderecoPedidoDeVenda.Cidade != null? _pedidoVenda.EnderecoPedidoDeVenda.Cidade.Id:0;

                txtBairro.Text = destinatario.Bairro;
                txtRua.Text = destinatario.Logradouro;
                txtComplementoEndereco.Text = destinatario.Complemento;
                txtNumeroEndereco.Text = destinatario.Numero;

                chkParceiroResideExterior.Checked = destinatario.ParceiroResideExterior;

                if (destinatario.CodigoPais != null)
                {
                    cboPaisParceiro.EditValue = _listaPaises.FirstOrDefault(x => x.CodigoPais == destinatario.CodigoPais).Id;
                }

                if (destinatario.Telefone != null)
                {
                    txtDdd.Text = destinatario.Telefone.Value.ToString().Substring(0, 2);
                    txtNumeroTelefone.Text = destinatario.Telefone.Value.ToString().Substring(2);
                }
            }
        }

        private void Conclua()
        {
            if (!TodosOsCamposEstaoPreenchidosCorretamente())
            {
                return;
            }

            DestinatarioAuxiliarNotaFiscal = new DestinatarioAuxiliarNotaFiscal();

            DestinatarioAuxiliarNotaFiscal.TipoPessoa = (EnumTipoPessoa)cboTipoPessoa.EditValue;
            DestinatarioAuxiliarNotaFiscal.Nome = txtRazaoSocial.Text;
            DestinatarioAuxiliarNotaFiscal.CpfCnpj = txtCpfCnpj.Text;
            DestinatarioAuxiliarNotaFiscal.TipoCliente = rdbConsumidorFinal.Checked ? EnumTipoCliente.CONSUMIDOR : EnumTipoCliente.REVENDA;

            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

            var pedido = servicoPedido.Consulte(_pedidoVenda.Id);

            pedido.TipoCliente = DestinatarioAuxiliarNotaFiscal.TipoCliente;

            pedido.EnderecoPedidoDeVenda.ClienteResideExterior = chkParceiroResideExterior.Checked;

            servicoPedido.Atualize(pedido);

            DestinatarioAuxiliarNotaFiscal.InscricaoEstadual = txtInscricaoEstadual.Text;

            //Vai definir o indicador de inscrição estadual
            DestinatarioAuxiliarNotaFiscal.TipoContribuinteICMS = _inscricaoIcms;

            DestinatarioAuxiliarNotaFiscal.Email = txtEmail.Text;

            DestinatarioAuxiliarNotaFiscal.DddTelefone = txtDdd.Text.ToIntNullabel();
            DestinatarioAuxiliarNotaFiscal.NumeroTelefone = txtNumeroTelefone.Text;

            DestinatarioAuxiliarNotaFiscal.Cep = txtCep.Text;
            DestinatarioAuxiliarNotaFiscal.UF = cboEstado.EditValue != null? cboEstado.EditValue.ToString():null;

            DestinatarioAuxiliarNotaFiscal.IrProximoNumero = chkProximaNota.Checked;

            if (cboCidade.EditValue != null)
            {
                var cidade = _listaCidades.FirstOrDefault(x => x.Id == cboCidade.EditValue.ToInt());

                DestinatarioAuxiliarNotaFiscal.CodigoCidade = cidade.CodigoIbge.ToLong();
                DestinatarioAuxiliarNotaFiscal.NomeCidade = cidade.Descricao;
            }

            DestinatarioAuxiliarNotaFiscal.Bairro = txtBairro.Text;
            DestinatarioAuxiliarNotaFiscal.Logradouro = txtRua.Text;
            DestinatarioAuxiliarNotaFiscal.Complemento = txtComplementoEndereco.Text;

            //Se o numero do endereço for nulo vamos colocar um ponto "."
            if (txtNumeroEndereco.Text == null || txtNumeroEndereco.Text=="")
            {
                DestinatarioAuxiliarNotaFiscal.Numero = ".";
            }
            else
            {
                DestinatarioAuxiliarNotaFiscal.Numero = txtNumeroEndereco.Text;
            }

            //Caso destinatário for estrangeiro
            DestinatarioAuxiliarNotaFiscal.IdEstrangeiro = txtIdEstrangeiro.Text;

            DestinatarioAuxiliarNotaFiscal.ResideNoExterior = chkParceiroResideExterior.Checked;
            
            if (cboPaisParceiro.EditValue != null)
            {
                var pais = _listaPaises.FirstOrDefault(x => x.Id == cboPaisParceiro.EditValue.ToInt());
                DestinatarioAuxiliarNotaFiscal.CodigoPais = pais.CodigoPais;
            }

            DestinatarioAuxiliarNotaFiscal.UFEmbarque = cboUFComercioExterior.EditValue != null?cboUFComercioExterior.EditValue.ToString():null;
            DestinatarioAuxiliarNotaFiscal.DescricaoLocalDespacho = txtDescricaoLocalDespachoComercioExterior.Text;
            DestinatarioAuxiliarNotaFiscal.DescricaoLocalEmbarque = txtDescricaoLocalEmbarqueFronteiraComercioExterior.Text;

            //*** FIM Estrangeiro****

            DestinatarioAuxiliarNotaFiscal.DataCadastro = _pedidoVenda.Cliente.DadosGerais.DataCadastro;
            DestinatarioAuxiliarNotaFiscal.StatusPessoa = _pedidoVenda.Cliente.DadosGerais.Status;
            modeloEmissaoNotaFiscal = rdbNFe.Checked ? EnumModeloNotaFiscal.NFE : EnumModeloNotaFiscal.NFCE;
            notaComDesconto = chkNotaComDesconto.Checked;

            _resultado = DialogResult.OK;

            this.Close();
        }
        
        private bool TodosOsCamposEstaoPreenchidosCorretamente()
        {
            string camposInconsistentes = string.Empty;

            camposInconsistentes += cboTipoPessoa.EditValue == null ? "Tipo Pessoa" : string.Empty;
            camposInconsistentes += string.IsNullOrWhiteSpace(txtRazaoSocial.Text) ? "Razão Social/Nome" : string.Empty;

            if (chkParceiroResideExterior.Checked)
            {
                camposInconsistentes += cboPaisParceiro.EditValue == null ? "País" : string.Empty;
                camposInconsistentes += string.IsNullOrWhiteSpace(txtIdEstrangeiro.Text) ? "Id Estrangeiro" : string.Empty;

                camposInconsistentes += cboUFComercioExterior.EditValue == null ? " UF Comercio Exterior" : String.Empty;
                camposInconsistentes += string.IsNullOrWhiteSpace(txtDescricaoLocalEmbarqueFronteiraComercioExterior.Text) ? " Local de Embarque" : string.Empty;
            }
            else
            {
                if(!rdbNFCe.Checked)
                camposInconsistentes += string.IsNullOrWhiteSpace(txtCpfCnpj.Text) ? "Cpf/Cnpj" : string.Empty;

                camposInconsistentes += string.IsNullOrWhiteSpace(txtCep.Text) ? "CEP" : string.Empty;
                camposInconsistentes += cboEstado.EditValue == null ? "Estado" : string.Empty;
                camposInconsistentes += cboCidade.EditValue == null ? "Cidade" : string.Empty;
                camposInconsistentes += string.IsNullOrWhiteSpace(txtBairro.Text) ? "Bairro" : string.Empty;
                camposInconsistentes += string.IsNullOrWhiteSpace(txtRua.Text) ? "Rua" : string.Empty;
                camposInconsistentes += string.IsNullOrWhiteSpace(txtRua.Text) ? "Número" : string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(camposInconsistentes))
            {
                camposInconsistentes = "Os seguintes campos não foram preenchidos:\n\n" + camposInconsistentes;
                MessageBox.Show(camposInconsistentes);

                return false;
            }

            //Se for NFCe não vamos validar o CPF/CNPJ
            if (rdbNFCe.Checked || chkParceiroResideExterior.Checked)
                return true;

            if ((EnumTipoPessoa)cboTipoPessoa.EditValue == EnumTipoPessoa.PESSOAFISICA)
            {
                if (!ValidacoesGerais.CpfEstahValido(txtCpfCnpj.Text))
                {
                    MessageBox.Show("CPF inconsistente.");

                    return false;
                }
            }
            else
            {
                if (!ValidacoesGerais.CnpjEstahValido(txtCpfCnpj.Text))
                {
                    MessageBox.Show("CNPJ inconsistente.");

                    return false;
                }
            }

            return true;
        }

        private void SelecionaEmissaoNFPadrao()
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNF = new ServicoConfiguracaoNfe();

            var parametros =servicoConfiguracaoNF.ConsulteConfiguracoesNfe();

            if (parametros == null)
            {
                MessageBoxAkil.Show("Para emitir a Nota Fiscal é necessário fazer as devidas configurações no menu configurações. Caso tenha dúvida, contate o suporte!", "Confirmação Nota Fiscal");
                return;
            }

            if (parametros.PadraoModeloNF == EnumModeloNotaFiscal.NFE)
                rdbNFe.Checked = true;
            else
                rdbNFCe.Checked = true;

            VerificaTipoInscricaoContribuinte();
        }

        private void VerificaTipoInscricaoContribuinte()
        {
            var tipoInscricaoIcms = new PessoaMetodosAuxiliares().RetorneTipoInscricaoIcms(_pedidoVenda.Cliente.Id);

            _EhContribuite = false;

            if (tipoInscricaoIcms == EnumTipoInscricaoICMS.CONTRIBUINTEICMS)
            {
                rdbNFe.Checked = true;
                rdbNFCe.Checked = false;
                rdbNFCe.Enabled = false;
                _EhContribuite = true;
            }
        }

        private bool VerificaStatusNotaFiscal(EnumStatusNotaFiscal statusNF, EnumModeloNotaFiscal modeloNF)
        {
            pnlModeloEmissaoNotaFiscal.Enabled = true;

            if (statusNF == EnumStatusNotaFiscal.REJEITADA || statusNF == EnumStatusNotaFiscal.PROCESSANDO)
            {
                if (modeloNF == EnumModeloNotaFiscal.NFCE)
                    rdbNFCe.Checked = true;
                else
                    rdbNFe.Checked = true;

                pnlModeloEmissaoNotaFiscal.Enabled = false;
                return true;
            }
            else
                return false;
        }


        #endregion
       
    }
}
