using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using NFe.Servicos;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using System.Transactions;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Fiscal.CartaCorrecaoServ
{
    public class ServicoCartaCorrecao : ServicoAkilSmallBusiness<CartaCorrecao, ValidacaoCartaCorrecao, ConversorCartaCorrecao>
    {
        IRepositorioCartaCorrecao _repositorioCartaCorrecao;

        public ServicoCartaCorrecao()
            : base(false, true)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<CartaCorrecao> RetorneRepositorio()
        {
            if (_repositorioCartaCorrecao == null)
            {
                _repositorioCartaCorrecao = FabricaDeRepositorios.Crie<IRepositorioCartaCorrecao>();
            }

            return _repositorioCartaCorrecao;
        }

        public override int Cadastre(CartaCorrecao objetoDeNegocio)
        {
            ServicoNotaFiscal servicoNotaFiscal = new ServicoNotaFiscal();
            var notaFiscal = servicoNotaFiscal.Consulte(objetoDeNegocio.NotaFiscal.Id);

            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);
            var configuracoesZeus = servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus((EnumModeloNotaFiscal)notaFiscal.IdentificacaoNotaFiscal.ModeloDocumentoFiscal);

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                ValideInclusao(objetoDeNegocio);

                var servicoNFe = new ServicosNFe(configuracoesZeus);
                var retornoEnvio = servicoNFe.RecepcaoEventoCartaCorrecao(notaFiscal.IdentificacaoNotaFiscal.NumeroNota,
                                                                                                             objetoDeNegocio.SequenciaEvento,
                                                                                                             notaFiscal.InformacoesGeraisNotaFiscal.ChaveDeAcesso,
                                                                                                             objetoDeNegocio.Correcao.RemovaEspacosEmBrancoDoInicioEFim(),
                                                                                                             notaFiscal.Emitente.CNPJ.RemoverCaracteresDeMascara());

                if (retornoEnvio.Retorno.cStat == 128)
                {
                    if (retornoEnvio.Retorno.retEvento[0].infEvento.cStat == 135)
                    {
                        objetoDeNegocio.DataHoraEmissao = retornoEnvio.Retorno.retEvento[0].infEvento.dhRegEvento;
                        objetoDeNegocio.NumeroProtocolo = retornoEnvio.Retorno.retEvento[0].infEvento.nProt;

                        int id = base.Cadastre(objetoDeNegocio);

                        scope.Complete();

                        return id;
                    }
                    else
                    {
                        throw new Exception(retornoEnvio.Retorno.retEvento[0].infEvento.xMotivo);
                    }
                }
                else
                {
                    throw new Exception(retornoEnvio.Retorno.xMotivo);
                }
            }
        }

        public int CadastreCartaCorrecao(CartaCorrecao objetoDeNegocio)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                ValideInclusao(objetoDeNegocio);
                
                int id = base.Cadastre(objetoDeNegocio);

                scope.Complete();

                return id;
            }
        }

    }
}
