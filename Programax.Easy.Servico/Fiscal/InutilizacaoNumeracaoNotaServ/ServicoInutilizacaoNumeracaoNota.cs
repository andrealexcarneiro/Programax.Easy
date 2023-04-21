using System;
using System.Collections.Generic;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Fiscal.InutilizacaoNumeracaoNotaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.InutilizacaoNumeracaoNotaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Infraestrutura.Negocio.Utils;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Servicos;
using NFe.Utils;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;

namespace Programax.Easy.Servico.Fiscal.InutilizacaoNumeracaoNotaServ
{
    [Funcionalidade(EnumFuncionalidade.INUTILIZACAONUMERACAONOTA)]
    public class ServicoInutilizacaoNumeracaoNota : ServicoAkilSmallBusiness<InutilizacaoNumeracaoNota, ValidacaoInutilizacaoNumeracaoNota, ConversorInutilizacaoNumeracaoNota>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioInutilizacaoNumeracaoNota _repositorioInutilizacaoNumeracaoNota;

        #endregion

        #region " CONSTRUTOR "

        public ServicoInutilizacaoNumeracaoNota()
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<InutilizacaoNumeracaoNota> RetorneRepositorio()
        {
            if (_repositorioInutilizacaoNumeracaoNota == null)
            {
                _repositorioInutilizacaoNumeracaoNota = FabricaDeRepositorios.Crie<IRepositorioInutilizacaoNumeracaoNota>();
            }

            return _repositorioInutilizacaoNumeracaoNota;
        }

        public override int Cadastre(InutilizacaoNumeracaoNota objetoDeNegocio)
        {
            ValideInclusao(objetoDeNegocio);

            EnvieNumeracaoParaSefaz(objetoDeNegocio);

            var objetoDeNegocioParaPersistencia = ConvertaObjetoParaPersistencia(objetoDeNegocio);

            IncluirObjetoNaBaseDeDados(objetoDeNegocio, objetoDeNegocioParaPersistencia);

            return objetoDeNegocioParaPersistencia.Id;
        }

        private void EnvieNumeracaoParaSefaz(InutilizacaoNumeracaoNota inutilizacaoNumeracaoNota)
        {
            var configuracoesZeus = RetorneConfiguracoesZeus(inutilizacaoNumeracaoNota.ModeloNotaFiscal);

            ServicoEmpresa servicoEmpresa = new ServicoEmpresa(false, false);
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            var servicoNFe = new ServicosNFe(configuracoesZeus);
            var retornoInutilizacao = servicoNFe.NfeInutilizacao(empresa.DadosEmpresa.Cnpj.RemoverCaracteresDeMascara(), Convert.ToInt16(inutilizacaoNumeracaoNota.Ano),
                (DFe.Classes.Flags.ModeloDocumento)inutilizacaoNumeracaoNota.ModeloNotaFiscal, Convert.ToInt16(inutilizacaoNumeracaoNota.Serie), Convert.ToInt16(inutilizacaoNumeracaoNota.NumeroInicial),
                Convert.ToInt16(inutilizacaoNumeracaoNota.NumeroFinal), inutilizacaoNumeracaoNota.Justificativa);

            if (retornoInutilizacao.Retorno.infInut.cStat == 102)
            {
                inutilizacaoNumeracaoNota.Protocolo = retornoInutilizacao.Retorno.infInut.nProt;
            }
            else
            {
                string msgErro = "Ocorreu o seguinte erro ao processar a inutilização:\r\n" +
                                         retornoInutilizacao.Retorno.infInut.cStat + " - " +
                                         retornoInutilizacao.Retorno.infInut.xMotivo;

                throw new Exception(msgErro);
            }
        }

        #endregion

        #region " CONSULTAS "

        public List<InutilizacaoNumeracaoNota> ConsulteLista()
        {
            return _repositorioInutilizacaoNumeracaoNota.ConsulteLista();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private ConfiguracaoServico RetorneConfiguracoesZeus(EnumModeloNotaFiscal modeloNotaFiscal)
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);
            var configuracoesZeus = servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus(modeloNotaFiscal);

            return configuracoesZeus;
        }

        #endregion
    }
}
