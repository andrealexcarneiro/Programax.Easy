using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.RamoAtividadeServ
{
    [Funcionalidade(EnumFuncionalidade.RamoAtividade)]
    public class ServicoRamoAtividade : ServicoAkilSmallBusiness<RamoAtividade, ValidacaoRamoAtividade, ConversorRamoAtividade>
    {
        IRepositorioRamoAtividade _repositorioRamoAtividade;

        public ServicoRamoAtividade()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<RamoAtividade> RetorneRepositorio()
        {
            if (_repositorioRamoAtividade == null)
            {
                _repositorioRamoAtividade = FabricaDeRepositorios.Crie<IRepositorioRamoAtividade>();
            }

            return _repositorioRamoAtividade;
        }

        public List<RamoAtividade> ConsulteLista()
        {
            return _repositorioRamoAtividade.ConsulteLista();
        }

        public List<RamoAtividade> ConsulteListaAtiva()
        {
            return _repositorioRamoAtividade.ConsulteListaAtiva();
        }

        public List<RamoAtividade> ConsulteLista(string descricao, string status)
        {
            return _repositorioRamoAtividade.ConsulteLista(descricao, status);
        }
    }
}
