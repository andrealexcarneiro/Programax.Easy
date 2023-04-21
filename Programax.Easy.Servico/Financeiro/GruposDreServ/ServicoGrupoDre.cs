using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.GruposDreObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposDreObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.GruposDreServ
{
    [Funcionalidade(EnumFuncionalidade.GRUPODRE)]
    public class ServicoGrupoDre : ServicoAkilSmallBusiness<GrupoDre, ValidacaoGrupoDre, ConversorGrupoDre>
    {
        IRepositorioGrupoDre _repositorioGrupoDre;
       

        public ServicoGrupoDre()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<GrupoDre> RetorneRepositorio()
        {
            if (_repositorioGrupoDre == null)
            {
                _repositorioGrupoDre = FabricaDeRepositorios.Crie<IRepositorioGrupoDre>();
            }

            return _repositorioGrupoDre;
        }

        public List<GrupoDre> ConsulteListaAtiva()
        {
            return _repositorioGrupoDre.ConsulteListaAtiva();
        }

        public List<GrupoDre> ConsulteLista(int? idGrupo, string descricao, string status)
        {
            return _repositorioGrupoDre.ConsulteLista(idGrupo, descricao, status);
        }
    }
}
