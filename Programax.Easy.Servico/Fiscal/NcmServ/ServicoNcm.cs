using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.NcmObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Linq;
using Programax.Infraestrutura.Negocio.Utils;
using System.Text;
using System.Transactions;
using System.Text.RegularExpressions;

namespace Programax.Easy.Servico.Fiscal.NcmServ
{
    [Funcionalidade(EnumFuncionalidade.NCM)]
    public class ServicoNcm : ServicoAkilSmallBusiness<Ncm, ValidacaoNcm, ConvercorNcm>
    {
        IRepositorioNcm _repositorioNcm;

        public ServicoNcm()
        {
            RetorneRepositorio();
        }

        public ServicoNcm(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Ncm> RetorneRepositorio()
        {
            if (_repositorioNcm == null)
            {
                _repositorioNcm = FabricaDeRepositorios.Crie<IRepositorioNcm>();
            }

            return _repositorioNcm;
        }

        public void ImporteConteudoArquivoNcm(StringBuilder conteudoArquivoNcm)
        {
            List<Ncm> listaNcmsArquivo = new List<Ncm>();

            string[] linhasArquivo = Regex.Split(conteudoArquivoNcm.ToString(), "\r\n");
            
            bool primeiraLinha = true;

            foreach (var linha in linhasArquivo)
            {
                if (primeiraLinha)
                {
                    primeiraLinha = false;
                    continue;
                }
                else if (string.IsNullOrEmpty(linha))
                {
                    continue;
                }

                string[] campos = linha.Split(';');

                Ncm ncm = new Ncm();
                ncm.CodigoNcm = campos[0];                
                ncm.DataCadastro = DateTime.Now;
                ncm.DataValidadeIbpt = campos[9].ToDate();
                ncm.Descricao = campos[3].ToUpper().RemovaAcentos();
                ncm.ImpostoIbptEstadual = campos[6].ToDouble();
                ncm.ImpostoIbptFederalImportados = campos[5].ToDouble();
                ncm.ImpostoIbptFederalNacional = campos[4].ToDouble();
                ncm.ImpostoIbptMunicipal = campos[7].ToDouble();
                ncm.Status = "A";
                ncm.ChaveTabelaIbpt = campos[10];

                listaNcmsArquivo.Add(ncm);
            }

            var listaNcms = this.ConsulteLista();

            List<Ncm> listaNcmsExcluidosArquivos = new List<Ncm>();
            List<Ncm> listaNcmsInativar = new List<Ncm>();

            foreach (var item in listaNcms)
            {
                listaNcmsInativar.Add(item);
            }
            

            foreach (var ncmArquivo in listaNcmsArquivo)
            {
                var ncm = listaNcms.FirstOrDefault(x => x.CodigoNcm == ncmArquivo.CodigoNcm);

                if (ncm != null)
                {   
                    ncm.DataValidadeIbpt = ncmArquivo.DataValidadeIbpt;
                    ncm.ImpostoIbptEstadual = ncmArquivo.ImpostoIbptEstadual;
                    ncm.ImpostoIbptFederalImportados = ncmArquivo.ImpostoIbptFederalImportados;
                    ncm.ImpostoIbptFederalNacional = ncmArquivo.ImpostoIbptFederalNacional;
                    ncm.ImpostoIbptMunicipal = ncmArquivo.ImpostoIbptMunicipal;
                    ncm.Status = "A";
                    ncm.ChaveTabelaIbpt = ncmArquivo.ChaveTabelaIbpt;

                    listaNcmsExcluidosArquivos.Add(ncmArquivo);
                    listaNcmsInativar.Remove(ncm);
                }
            }

            foreach (var ncmExcluidoArquivo in listaNcmsExcluidosArquivos)
            {
                listaNcmsArquivo.Remove(ncmExcluidoArquivo);
            }

            foreach (var ncmInativar in listaNcmsInativar)
            {
                ncmInativar.Status = "I";
            }
            
            string ncmAnterior=string.Empty;
            List<Ncm> listaDuplicada = new List<Ncm>();
            foreach (var item in listaNcmsArquivo)
            {
                if(ncmAnterior == item.CodigoNcm)
                {
                    listaDuplicada.Add(item);
                }

                ncmAnterior = item.CodigoNcm;
            }

            foreach (var item in listaDuplicada)
            {
                listaNcmsArquivo.Remove(item);
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                _repositorioNcm.AtualizeLista(listaNcms);
                _repositorioNcm.CadastreLista(listaNcmsArquivo);

                scope.Complete();
            }
        }

        public override void Atualize(Ncm objetoDeNegocio)
        {
            base.Atualize(objetoDeNegocio);
        }

        public Ncm ConsultePeloCodigoNcm(string codigoNcm)
        {
            return _repositorioNcm.ConsultePeloCodigoNcm(codigoNcm);
        }

        public List<Ncm> ConsulteListaDeCodigosNcm(List<string> listaCodigosNcm)
        {
            return _repositorioNcm.ConsulteListaDeCodigosNcm(listaCodigosNcm);
        }

        public List<Ncm> ConsulteLista()
        {
            return _repositorioNcm.ConsulteLista();
        }

        public List<Ncm> ConsulteLista(string codigoNcm, string descricao, string status)
        {
            return _repositorioNcm.ConsulteLista(codigoNcm, descricao, status);
        }

        public bool ExisteAlgumNcmForaDoPrazoDeValidade()
        {
            return _repositorioNcm.ExisteAlgumNcmForaDoPrazoDeValidade();
        }
    }
}
