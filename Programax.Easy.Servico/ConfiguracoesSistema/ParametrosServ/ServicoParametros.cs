using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using NFe.Utils;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;
using System.IO;
using Programax.Easy.Servico.Fiscal.ConfiguracaoNfeServ;
using NFe.Servicos;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ
{
    [Funcionalidade(EnumFuncionalidade.PARAMETROS)]
    public class ServicoParametros : ServicoAkilSmallBusiness<Parametros, ValidacaoParametros, ConversorParametros>
    {
        IRepositorioParametros _repositorioParametros;

        public ServicoParametros()
        {
            RetorneRepositorio();
        }

        public ServicoParametros(bool limparSessao)
            : base(true, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Parametros> RetorneRepositorio()
        {
            if (_repositorioParametros == null)
            {
                _repositorioParametros = FabricaDeRepositorios.Crie<IRepositorioParametros>();
            }

            return _repositorioParametros;
        }

        public Parametros ConsulteParametros()
        {
            var parametros = _repositorioParametros.ConsulteParametros();

            parametros = parametros ?? new Parametros();

            return parametros;
        }

        public override int Cadastre(Parametros objetoDeNegocio)
        {
            var parametros = ConsulteParametros();

            if (parametros == null)
            {
                return base.Cadastre(objetoDeNegocio);
            }
            else
            {
                objetoDeNegocio.Id = parametros.Id;

                base.Atualize(objetoDeNegocio);

                return parametros.Id;
            }
        }

        public CSCParametros GereCodigoCsc()
        {
            ServicoConfiguracaoNfe servicoConfiguracaoNfe = new ServicoConfiguracaoNfe(false, false);
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa(false, false);

            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            var configuracoesZeus = servicoConfiguracaoNfe.RetorneConfiguracaoServicoZeus(EnumModeloNotaFiscal.NFCE);

            var servicoNFe = new ServicosNFe(configuracoesZeus);
            var retornoCscAtivo = servicoNFe.AdmCscNFCe(empresa.DadosEmpresa.Cnpj.RemoverCaracteresDeMascara(), IdentificadorOperacaoCsc.ioConsultaCscAtivos);

            if (retornoCscAtivo.Retorno.dadosCsc != null && retornoCscAtivo.Retorno.dadosCsc.Count == 0)
            {
                retornoCscAtivo = servicoNFe.AdmCscNFCe(empresa.DadosEmpresa.Cnpj.RemoverCaracteresDeMascara(), IdentificadorOperacaoCsc.ioSolicitaNovoCsc);
            }

            CSCParametros cscParametros = new CSCParametros();
            cscParametros.CodigoCsc = retornoCscAtivo.Retorno.dadosCsc[0].codigoCsc;
            cscParametros.IdCsc = retornoCscAtivo.Retorno.dadosCsc[0].idCsc;

            return cscParametros;
        }

        public class CSCParametros
        {
            public string IdCsc { get; set; }

            public string CodigoCsc { get; set; }
        }
    }
}
