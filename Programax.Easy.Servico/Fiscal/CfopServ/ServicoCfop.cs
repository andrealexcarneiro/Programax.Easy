using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.CfopServ
{
    [Funcionalidade(EnumFuncionalidade.CFOP)]
    public class ServicoCfop : ServicoAkilSmallBusiness<Cfop, ValidacaoCfop, ConversorCfop>
    {
        IRepositorioCfop _repositorioCfop;

        public ServicoCfop()
        {
            RetorneRepositorio();
        }

        public ServicoCfop(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override Infraestrutura.Negocio.ObjetosDeNegocio.IRepositorioBase<Cfop> RetorneRepositorio()
        {
            if (_repositorioCfop == null)
            {
                _repositorioCfop = FabricaDeRepositorios.Crie<IRepositorioCfop>();
            }

            return _repositorioCfop;
        }

        public List<Cfop> ConsulteLista(string codigoCfop, string descricao, string status, EnumTipoCfop tipoCfop)
        {
            return _repositorioCfop.ConsulteLista(codigoCfop, descricao, status, tipoCfop);
        }

        public List<Cfop> ConsulteListaAtiva(EnumOrigemDestino origemDestino, EnumTipoMovimentacaoNaturezaOperacao tipoMovimentacaoNaturezaOperacao)
        {
            return _repositorioCfop.ConsulteListaAtiva(origemDestino, tipoMovimentacaoNaturezaOperacao);
        }

        public List<Cfop> ConsulteListaAtiva()
        {
            return _repositorioCfop.ConsulteListaAtiva();
        }

        public Cfop ConsultePeloCodigoCfop(string codigoCfop)
        {
            return _repositorioCfop.ConsultePeloCodigoCfop(codigoCfop);
        }

        public List<Cfop> ConsulteListaDeCodigosCfop(List<string> listaCodigosCfop)
        {
            return _repositorioCfop.ConsulteListaDeCodigosCfop(listaCodigosCfop);
        }
    }
}
