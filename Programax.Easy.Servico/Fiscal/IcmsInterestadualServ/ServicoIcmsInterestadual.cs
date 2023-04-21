using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Fiscal.IcmsInterestadualServ
{
    [Funcionalidade(EnumFuncionalidade.ICMSINTERESTADUAL)]
    public class ServicoIcmsInterestadual : ServicoAkilSmallBusiness<IcmsInterestadual, ValidacaoIcmsInterestadual, ConversorIcmsInterestadual>
    {
        IRepositorioIcmsInterestadual _repositorioIcmsInterestadual;

        public ServicoIcmsInterestadual()
        {
            RetorneRepositorio();
        }

        public ServicoIcmsInterestadual(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override Infraestrutura.Negocio.ObjetosDeNegocio.IRepositorioBase<IcmsInterestadual> RetorneRepositorio()
        {
            if (_repositorioIcmsInterestadual == null)
            {
                _repositorioIcmsInterestadual = FabricaDeRepositorios.Crie<IRepositorioIcmsInterestadual>();
            }

            return _repositorioIcmsInterestadual;
        }

        public IcmsInterestadual ConsultePorNcm(string codigoNcm)
        {
            return _repositorioIcmsInterestadual.ConsultePorNcm(codigoNcm);
        }

        public IcmsInterestadual ConsultePorNcmEUFDestino(string codigoNcm, string ufDestino)
        {
            return _repositorioIcmsInterestadual.ConsultePorNcmEUFDestino(codigoNcm, ufDestino);
        }

        public bool ExisteAliquotaParaONcmEOEstado(int ncmId, string uf)
        {
            return _repositorioIcmsInterestadual.ExisteAliquotaParaONcmEOEstado(ncmId, uf);
        }

        public void ValidaItemIcmsInterestadual(IcmsInterestadualEstado icmsInterestadualEstado, List<IcmsInterestadualEstado> listaIcmsPorEstado)
        {
            ValidacaoIcmsInterestadualEstado validacaoIcmsInterestadualEstado = new ValidacaoIcmsInterestadualEstado();

            validacaoIcmsInterestadualEstado.ListaIcmsInterestaduais = listaIcmsPorEstado;

            validacaoIcmsInterestadualEstado.ValideInclusao();
            validacaoIcmsInterestadualEstado.Valide(icmsInterestadualEstado).AssegureSucesso();
        }

        public List<IcmsInterestadual> ConsulteListaPorNcms(List<Ncm> _listaNcms)
        {
            return _repositorioIcmsInterestadual.ConsulteListaPorNcms(_listaNcms);
        }

        internal List<IcmsInterestadualEstado> ConsulteListaIcmsEstadoPorCodigosNcmsEUF(List<string> listaCodigosNcm, string uf)
        {
            return _repositorioIcmsInterestadual.ConsulteListaIcmsEstadoPorCodigosNcmsEUF(listaCodigosNcm, uf);
        }
    }
}
