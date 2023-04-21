using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Servicos;

namespace Programax.Easy.Servico.ConfiguracoesSistema.LicencaDeUsoServ
{
    [Funcionalidade(EnumFuncionalidade.LICENCADEUSO)]
    public class ServicoLicencaDeUso : ServicoAkilSmallBusiness<LicencaDeUso, ValidacaoLicencaDeUso, ConversorLicencaDeUso>
    {
        IRepositorioLicencaDeUso _repositorioLicencaDeUso;

        public ServicoLicencaDeUso()
        {
            RetorneRepositorio();
        }

        public override void Atualize(LicencaDeUso objetoDeNegocio)
        {
            ValideAtualizacao(objetoDeNegocio);

            var objetoParaPersistencia = ConvertaObjetoParaPersistencia(objetoDeNegocio);

            string chaveDescriptografada = objetoDeNegocio.ChaveLiberacao.Descriptografar();

            var parametrosChave = chaveDescriptografada.Split('|');

            objetoParaPersistencia.LiberadoAte = parametrosChave[1].ToDate();
            objetoParaPersistencia.QuantidadeUsuariosContratados = parametrosChave[2].ToInt();

            AtualizeObjetoNaBaseDeDados(objetoParaPersistencia);
        }

        public override IRepositorioBase<LicencaDeUso> RetorneRepositorio()
        {
            if (_repositorioLicencaDeUso == null)
            {
                _repositorioLicencaDeUso = FabricaDeRepositorios.Crie<IRepositorioLicencaDeUso>();
            }

            return _repositorioLicencaDeUso;
        }

        public LicencaDeUso ConsulteUltimaLicencaDeUso()
        {
            return _repositorioLicencaDeUso.ConsulteUltimaLicencaDeUso();
        }
    }
}
