using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Financeiro.BancoServ
{
    [Funcionalidade(EnumFuncionalidade.BANCOS)]
    public class ServicoBanco : ServicoAkilSmallBusiness<Banco, ValidacaoBanco, ConversorBanco>
    {
        IRepositorioBanco _repositorioBanco;

        public override IRepositorioBase<Banco> RetorneRepositorio()
        {
            if (_repositorioBanco == null)
            {
                _repositorioBanco = FabricaDeRepositorios.Crie<IRepositorioBanco>();
            }

            return _repositorioBanco;
        }

        public ServicoBanco()
        {
            RetorneRepositorio();
        }

        public List<Banco> ConsulteLista()
        {
            return _repositorioBanco.ConsulteLista();
        }

        public List<Banco> ConsulteListaAtiva()
        {
            return _repositorioBanco.ConsulteListaAtiva();
        }

        public List<Banco> ConsulteLista(string descricao, string status)
        {
            return _repositorioBanco.ConsulteLista(descricao, status);
        }

        public Banco ConsultePeloCodigoBanco(string codigoBanco)
        {
            return _repositorioBanco.ConsultePeloCodigoBanco(codigoBanco);
        }
    }
}
