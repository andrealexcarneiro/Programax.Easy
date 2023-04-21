using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.EmpresaServ
{
    [Funcionalidade(EnumFuncionalidade.EMPRESA)]
    public class ServicoEmpresa : ServicoAkilSmallBusiness<Empresa, ValidacaoEmpresa, ConversorEmpresa>
    {
        IRepositorioEmpresa _repositorioEmpresa;

        public ServicoEmpresa()
        {
            RetorneRepositorio();
        }

        public ServicoEmpresa(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override Infraestrutura.Negocio.ObjetosDeNegocio.IRepositorioBase<Empresa> RetorneRepositorio()
        {
            if (_repositorioEmpresa == null)
            {
                _repositorioEmpresa = FabricaDeRepositorios.Crie<IRepositorioEmpresa>();
            }

            return _repositorioEmpresa;
        }

        public Empresa ConsulteUltimaEmpresa()
        {
            return _repositorioEmpresa.ConsulteUltimaEmpresa();
        }
    }
}
