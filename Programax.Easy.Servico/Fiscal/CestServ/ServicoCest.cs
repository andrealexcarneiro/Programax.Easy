using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.CestObj.Repositorio;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Transactions;

namespace Programax.Easy.Servico.Fiscal.CestServ
{
    [Funcionalidade(EnumFuncionalidade.CEST)]
    public class ServicoCest:ServicoAkilSmallBusiness<Cest,ValidacaoCest,ConvercorCest>
    {
        IRepositorioCest _repositorioCest;

        public ServicoCest()
        {
            RetorneRepositorio();
        }

        public ServicoCest(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Cest> RetorneRepositorio()
        {
            if (_repositorioCest == null)
            {
                _repositorioCest = FabricaDeRepositorios.Crie<IRepositorioCest>();
            }

            return _repositorioCest;
        }

        public void ImporteConteudoArquivoCest(StringBuilder conteudoArquivoNcm)
        {
            List<Cest> listaCestsArquivo = new List<Cest>();

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

                Cest cest = new Cest();
                cest.CodigoCest = campos[0];
                cest.CodigoNcm = campos[1];
                cest.DescricaoCest = campos[2];
                cest.DataCadastro = DateTime.Now;
                cest.Status = "A";
                listaCestsArquivo.Add(cest);
            }

            var listaCests = this.ConsulteLista();

            List<Cest> listaCestsExcluidosArquivos = new List<Cest>();
            List<Cest> listaCestsInativar = new List<Cest>();

            foreach (var item in listaCests)
            {
                listaCestsInativar.Add(item);
            }


            foreach (var cestArquivo in listaCestsArquivo)
            {
                var cest = listaCests.FirstOrDefault(x => x.CodigoCest == cestArquivo.CodigoCest);

                if (cest != null)
                {   
                    cest.Status = "A";
                    
                    listaCestsExcluidosArquivos.Add(cestArquivo);
                    listaCestsInativar.Remove(cest);
                }
            }

            foreach (var cestExcluidoArquivo in listaCestsExcluidosArquivos)
            {
                listaCestsArquivo.Remove(cestExcluidoArquivo);
            }

            foreach (var cestInativar in listaCestsInativar)
            {
                cestInativar.Status = "I";
            }

            string cestAnterior = string.Empty;
            List<Cest> listaDuplicada = new List<Cest>();
            foreach (var item in listaCestsArquivo)
            {
                if (cestAnterior == item.CodigoCest)
                {
                    listaDuplicada.Add(item);
                }

                cestAnterior = item.CodigoCest;
            }

            foreach (var item in listaDuplicada)
            {
                listaCestsArquivo.Remove(item);
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                _repositorioCest.AtualizeLista(listaCests);
                _repositorioCest.CadastreLista(listaCestsArquivo);

                scope.Complete();
            }
        }

        public override void Atualize(Cest objetoDeNegocio)
        {
            base.Atualize(objetoDeNegocio);
        }

        public Cest ConsultePeloCodigoCest(string codigoCest)
        {
            return _repositorioCest.ConsultePeloCodigoCest(codigoCest);
        }

        public List<Cest> ConsulteListaDeCodigosNcm(List<string> listaCodigosCest)
        {
            return _repositorioCest.ConsulteListaDeCodigosCest(listaCodigosCest);
        }

        public List<Cest> ConsulteLista()
        {
            return _repositorioCest.ConsulteLista();
        }

        public List<Cest> ConsulteLista(string codigoCest, string descricao, string status)
        {
            return _repositorioCest.ConsulteListaCest(codigoCest, descricao, status);
        }
    }
}
