using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Servicos;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;

namespace Programax.Easy.Servico.Integracao.TabelasAtualizadasIntegracaoDJServ
{
    public class ServicoTabelasAtualizadasIntegracaoDJ : ServicoBase<TabelasAtualizadasIntegracaoDJ, ValidacaoTabelasAtualizadasIntegracaoDJ, ConversorTabelasAtualizadasIntegracaoDJ>
    {
        IRepositorioTabelasAtualizadasIntegracaoDJ _repositorioTabelasAtualizadasIntegracaoDJ;

        public ServicoTabelasAtualizadasIntegracaoDJ()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<TabelasAtualizadasIntegracaoDJ> RetorneRepositorio()
        {
            if (_repositorioTabelasAtualizadasIntegracaoDJ == null)
            {
                _repositorioTabelasAtualizadasIntegracaoDJ = FabricaDeRepositorios.Crie<IRepositorioTabelasAtualizadasIntegracaoDJ>();
            }

            return _repositorioTabelasAtualizadasIntegracaoDJ;
        }

        public List<TabelasAtualizadasIntegracaoDJ> ConsulteLista()
        {
            return _repositorioTabelasAtualizadasIntegracaoDJ.ConsulteLista();
        }

        public void Exclua(TabelasAtualizadasIntegracaoDJ objeto)
        {
            _repositorioTabelasAtualizadasIntegracaoDJ.Exclua(objeto);
        }

        public void ExcluaLista(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            _repositorioTabelasAtualizadasIntegracaoDJ.ExcluaLista(atualizacoes);
        }

        public void ExportaTodosDados()
        {
            _repositorioTabelasAtualizadasIntegracaoDJ.ExportaTodosDados();
        }
    }
}
