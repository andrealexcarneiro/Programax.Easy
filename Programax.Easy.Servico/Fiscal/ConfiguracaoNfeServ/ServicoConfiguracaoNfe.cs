using System;
using System.Security.Cryptography.X509Certificates;
using Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using DFe.Utils;
using NFe.Utils;
using System.IO;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;
using NFe.Impressao.NFCe;
using NFe.Impressao;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using DFe.Classes.Flags;

namespace Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ
{
    [Funcionalidade(EnumFuncionalidade.CONFIGURACAONFE)]
    public class ServicoConfiguracaoNfe : ServicoAkilSmallBusiness<ConfiguracaoNfe, ValidacaoConfiguracaoNfe, ConversorConfiguracaoNfe>
    {
        private IRepositorioConfiguracaoNfe _repositorioConfiguracaoNfe;
        private static ConfiguracaoDanfeNfce _configuracaoDanfeNfce;

        public ServicoConfiguracaoNfe()
        {
            RetorneRepositorio();
        }

        public ServicoConfiguracaoNfe(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<ConfiguracaoNfe> RetorneRepositorio()
        {
            if (_repositorioConfiguracaoNfe == null)
            {
                _repositorioConfiguracaoNfe = FabricaDeRepositorios.Crie<IRepositorioConfiguracaoNfe>();
            }

            return _repositorioConfiguracaoNfe;
        }

        public ConfiguracaoNfe ConsulteConfiguracoesNfe(EnumModeloNotaFiscal ModeloNF = EnumModeloNotaFiscal.NFE)
        {
            return _repositorioConfiguracaoNfe.ConsulteConfiguracoesNfe(ModeloNF);
        }

        public ConfiguracaoServico RetorneConfiguracaoServicoZeus(EnumModeloNotaFiscal modeloNotaFiscal)
        {

            ServicoEmpresa servicoempresa = new ServicoEmpresa();
            var empresa = servicoempresa.ConsulteUltimaEmpresa();

            var configuracoesNfe = ConsulteConfiguracoesNfe(modeloNotaFiscal);

            var configuracaoZeus = ConfiguracaoServico.Instancia;

            if(empresa.DadosEmpresa.Endereco.Cidade.Estado.UF == "GO")
            {
                configuracaoZeus.cUF = DFe.Classes.Entidades.Estado.GO;
            }
            else
            {
                configuracaoZeus.cUF = DFe.Classes.Entidades.Estado.PA;
            }

            //configuracaoZeus.cUF = DFe.Classes.Entidades.Estado.GO;
            

            configuracaoZeus.SalvarXmlServicos = false;
            configuracaoZeus.DiretorioSchemas = Directory.GetCurrentDirectory() + @"\Esquemas\PL_008g";
            
            if(configuracoesNfe.FormatoImpressaoDanfe == Negocio.Fiscal.Enumeradores.EnumFormatoImpressaoDanfe.DANFENFCE
                || configuracoesNfe.FormatoImpressaoDanfe == Negocio.Fiscal.Enumeradores.EnumFormatoImpressaoDanfe.DANFENFCEEMMENSAGEMELETRONICA)
            {
                configuracaoZeus.ModeloDocumento = DFe.Classes.Flags.ModeloDocumento.NFCe;               
            }
            else
                configuracaoZeus.ModeloDocumento = DFe.Classes.Flags.ModeloDocumento.NFe;

            configuracaoZeus.Certificado = new ConfiguracaoCertificado();

            string dirCorrente = Directory.GetCurrentDirectory();

            if (Directory.Exists(dirCorrente + @"\Certificado"))
            {
                configuracaoZeus.Certificado.TipoCertificado = TipoCertificado.A1ByteArray;

                string dir = Directory.GetCurrentDirectory() + @"\Certificado\";

                DirectoryInfo informacoesDiretorio = new DirectoryInfo(dir);

                FileInfo[] arquivoPfx = informacoesDiretorio.GetFiles("*.pfx");

                string path = dir + arquivoPfx[0].Name;

                string[] senha = arquivoPfx[0].Name.Split('-');

                configuracaoZeus.Certificado.ArrayBytesArquivo = File.ReadAllBytes(path);
                configuracaoZeus.Certificado.Senha = senha[0].Trim();
            }
            else
            {
                configuracaoZeus.Certificado.Serial = configuracoesNfe.NumeroSerieCertificado;
            }

            configuracaoZeus.tpAmb = (TipoAmbiente)configuracoesNfe.TipoAmbiente;
            configuracaoZeus.tpEmis = TipoEmissao.teNormal;

            if (configuracoesNfe.TipoAmbiente == EnumTipoAmbiente.HOMOLOGACAO)
                configuracaoZeus.ProtocoloDeSeguranca = System.Net.SecurityProtocolType.Tls12;
            else
                configuracaoZeus.ProtocoloDeSeguranca = System.Net.SecurityProtocolType.Tls12;

            //-- Configurações de serviço para a SEFAZ DE GOIAS --//
            configuracaoZeus.VersaoNFeAutorizacao = VersaoServico.Versao400;
            configuracaoZeus.VersaoNfeConsultaCadastro = VersaoServico.Versao400;
            //configuracaoZeus.VersaoNfeConsultaDest = VersaoServico.ve310
            configuracaoZeus.VersaoNfeConsultaProtocolo = VersaoServico.Versao400;
            //configuracaoZeus.VersaoNFeDistribuicaoDFe
            //configuracaoZeus.VersaoNfeDownloadNF
            configuracaoZeus.VersaoNfeInutilizacao = VersaoServico.Versao400;
            configuracaoZeus.VersaoNfeRecepcao = VersaoServico.Versao400;
            configuracaoZeus.VersaoNFeRetAutorizacao = VersaoServico.Versao400;
            configuracaoZeus.VersaoNfeRetRecepcao = VersaoServico.Versao400;
            configuracaoZeus.VersaoNfeStatusServico = VersaoServico.Versao400;
            //configuracaoZeus.VersaoRecepcaoEvento = VersaoServico.ve310;
            configuracaoZeus.VersaoRecepcaoEventoCceCancelamento = VersaoServico.Versao400;    
            
            configuracaoZeus.TimeOut = 60000;

            return configuracaoZeus;
        }

        public ConfiguracaoDanfeNfce RetorneConfiguracaoDanfeNfceZeus()
        {
            if (_configuracaoDanfeNfce == null)
            {
                ServicoParametros servicoParametros = new ServicoParametros(false);
                ServicoEmpresa servicoEmpresa = new ServicoEmpresa(false, false);
                var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
                var parametros = servicoParametros.ConsulteParametros();

                if (string.IsNullOrEmpty(parametros.ParametrosFiscais.IdCsc))
                {
                    ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);
                    var csc = servicoParametros.GereCodigoCsc();

                    parametros.ParametrosFiscais.IdCsc = csc.IdCsc;
                    parametros.ParametrosFiscais.CodigoCsc = csc.CodigoCsc;

                    servicoParametros.Atualize(parametros);
                }

                _configuracaoDanfeNfce = new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha,
                    NfceDetalheVendaContigencia.UmaLinha,
                    parametros.ParametrosFiscais.IdCsc.PadLeft(6, '0'),
                    parametros.ParametrosFiscais.CodigoCsc,
                    logomarca: empresa.DadosEmpresa.Foto,
                    imprimeDescontoItem: true);
            }

            return _configuracaoDanfeNfce;
        }

        private static X509Store ObterX509Store(OpenFlags openFlags)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(openFlags);
            return store;
        }

        public X509Certificate2 ListareObterDoRepositorio()
        {
            var store = ObterX509Store(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
            var collection = store.Certificates;
            var fcollection = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            var scollection = X509Certificate2UI.SelectFromCollection(fcollection, "Certificados válidos:", "Selecione o certificado que deseja usar",
                X509SelectionFlag.SingleSelection);

            if (scollection.Count == 0)
            {
                throw new Exception("Nenhum certificado foi selecionado!");
            }

            store.Close();
            return scollection[0];
        }

    }
}
