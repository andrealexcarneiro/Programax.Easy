using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.PlanoContasDreServ
{
    [Funcionalidade(EnumFuncionalidade.PLANODECONTASDRE)]
    public class ServicoPlanoDeContasDre : ServicoAkilSmallBusiness<PlanoContaDre, ValidacaoPlanoContasDre, ConversorPlanoDeContasDre>

    {
        IRepositorioPlanoDeContasDre _repositorioPlanoDeContasDre;

        public ServicoPlanoDeContasDre()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<PlanoContaDre> RetorneRepositorio()
        {
            if (_repositorioPlanoDeContasDre == null)
            {
                _repositorioPlanoDeContasDre = FabricaDeRepositorios.Crie<IRepositorioPlanoDeContasDre>();
            }

            return _repositorioPlanoDeContasDre;
        }

        public List<PlanoContaDre> ConsulteLista(string numeroPlanoContas,
                                                                     string descricao,
                                                                     string status,
                                                                     EnumNaturezaPlanoContas? naturezaPlanoContas,
                                                                     EnumTipoPlanoContas? tipoPlanoContas,
                                                                     string numeroPlanoContasContador, string Grau)
        {
            return _repositorioPlanoDeContasDre.ConsulteLista(numeroPlanoContas, descricao, status, naturezaPlanoContas, tipoPlanoContas, numeroPlanoContasContador, Grau);
        }

        public List<PlanoContaDre> ConsulteLista()
        {
            return _repositorioPlanoDeContasDre.ConsulteLista();
        }

        public PlanoContaDre ConsultePlanoDeContasPeloNumero(string numeroPlanoContas)
        {
            return _repositorioPlanoDeContasDre.ConsultePlanoDeContasPeloNumero(numeroPlanoContas);
        }

        public PlanoContaDre ConsultePlanoDeContasAtivoPeloNumero(string numeroPlanoContas)
        {
            return _repositorioPlanoDeContasDre.ConsultePlanoDeContasAtivoPeloNumero(numeroPlanoContas);
        }
    }
}
