using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Servicos;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;

namespace Programax.Easy.Servico.Cadastros.EstadoServ
{
    public class ServicoEstado : ServicoBase<Estado, ValidacaoEstado, ConversorEstado>
    {
        IRepositorioEstado _repositorioEstado;

        public override IRepositorioBase<Estado> RetorneRepositorio()
        {
            if (_repositorioEstado == null)
            {
                _repositorioEstado = FabricaDeRepositorios.Crie<IRepositorioEstado>();
            }

            return _repositorioEstado;
        }

        public ServicoEstado()
        {
            RetorneRepositorio();
        }

        public List<Estado> ConsulteListaEstados()
        {
            return _repositorioEstado.ConsulteLista();
        }

        public Estado Consulte(string uf)
        {
            return _repositorioEstado.Consulte(uf);
        }
    }
}
