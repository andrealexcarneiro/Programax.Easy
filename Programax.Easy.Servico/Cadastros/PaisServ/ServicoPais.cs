using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Cadastros.PaisObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PaisObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;

namespace Programax.Easy.Servico.Cadastros.PaisServ
{
    public class ServicoPais : ServicoAkilSmallBusiness<Pais, ValidacaoPais, ConversorPais>
    {
        private IRepositorioPais _repositorioPais;

        public ServicoPais()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Pais> RetorneRepositorio()
        {
            if (_repositorioPais == null)
            {
                _repositorioPais = FabricaDeRepositorios.Crie<IRepositorioPais>();
            }

            return _repositorioPais;
        }

        public List<Pais> ConsulteLista()
        {
            return _repositorioPais.ConsulteLista();
        }
    }
}
