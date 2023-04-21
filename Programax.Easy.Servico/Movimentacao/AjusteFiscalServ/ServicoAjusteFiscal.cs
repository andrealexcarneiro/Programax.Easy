using System.Collections.Generic;
using Programax.Easy.Negocio.Movimentacao.AjusteFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.AjusteFiscalObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.Servicos;

namespace Programax.Easy.Servico.Movimentacao.AjusteFiscalServ
{
    public class ServicoAjusteFiscal : ServicoBase<AjusteFiscal, ValidacaoAjusteFiscal, ConversorAjusteFiscal>
    {
        protected IRepositorioAjusteFiscal _repositorioAjusteFiscal;

        public ServicoAjusteFiscal()
        {
            RetorneRepositorio();
        }

        public List<AjusteFiscal> ConsulteLista()
        {
            return _repositorioAjusteFiscal.ConsulteLista();
        }

        public override Infraestrutura.Negocio.ObjetosDeNegocio.IRepositorioBase<AjusteFiscal> RetorneRepositorio()
        {
            if (_repositorioAjusteFiscal == null)
            {
                _repositorioAjusteFiscal = FabricaDeRepositorios.Crie<IRepositorioAjusteFiscal>();
            }

            return _repositorioAjusteFiscal;
        }
    }
}
