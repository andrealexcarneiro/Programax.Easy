using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TamanhoObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Servico.Cadastros.TamanhoServ
{
    [Funcionalidade(EnumFuncionalidade.TAMANHOS)]
    public class ServicoTamanho : ServicoAkilSmallBusiness<Tamanho, ValidacaoTamanho, ConversorTamanho>
    {
        IRepositorioTamanho _repositorioTamanho;

        public ServicoTamanho()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Tamanho> RetorneRepositorio()
        {
            if (_repositorioTamanho == null)
            {
                _repositorioTamanho = FabricaDeRepositorios.Crie<IRepositorioTamanho>();
            }

            return _repositorioTamanho;
        }

        public List<Tamanho> ConsulteListaAtiva()
        {
            return _repositorioTamanho.ConsulteListaAtiva();
        }

        public List<Tamanho> ConsulteLista(string descricao, string status)
        {
            return _repositorioTamanho.ConsulteLista(descricao, status);
        }
    }
}
