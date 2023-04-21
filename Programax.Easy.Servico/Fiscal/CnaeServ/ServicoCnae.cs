using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CnaeObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Fiscal.CnaeServ
{
    [Funcionalidade(EnumFuncionalidade.CNAE)]
    public class ServicoCnae : ServicoAkilSmallBusiness<Cnae, ValidacaoCnae, ConversorCnae>
    {
        IRepositorioCnae _repositorioCnae;

        public ServicoCnae()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Cnae> RetorneRepositorio()
        {
            if (_repositorioCnae == null)
            {
                _repositorioCnae = FabricaDeRepositorios.Crie<IRepositorioCnae>();
            }

            return _repositorioCnae;
        }

        public List<Cnae> ConsulteListaCnae()
        {
            return _repositorioCnae.ConsulteLista();
        }

        public Cnae ConsultePeloCodigo(string codigo)
        {
            return _repositorioCnae.ConsultePeloCodigo(codigo);
        }

        public List<Cnae> ConsulteLista(string codigoCnae, string descricao, EnumAtividadeCnae? atividade, string status)
        {
            return _repositorioCnae.ConsulteLista(codigoCnae, descricao, atividade,status);
        }
    }
}
