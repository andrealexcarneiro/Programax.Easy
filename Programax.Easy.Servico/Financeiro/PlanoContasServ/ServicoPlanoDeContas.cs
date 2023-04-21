using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.PlanoContasServ
{
    [Funcionalidade(EnumFuncionalidade.PLANODECONTAS)]
    public class ServicoPlanoDeContas : ServicoAkilSmallBusiness<PlanoDeContas, ValidacaoPlanoContas, ConversorPlanoDeContas>
    {
        IRepositorioPlanoDeContas _repositorioPlanoDeContas;

        public ServicoPlanoDeContas()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<PlanoDeContas> RetorneRepositorio()
        {
            if (_repositorioPlanoDeContas == null)
            {
                _repositorioPlanoDeContas = FabricaDeRepositorios.Crie<IRepositorioPlanoDeContas>();
            }

            return _repositorioPlanoDeContas;
        }

        public List<PlanoDeContas> ConsulteLista(string numeroPlanoContas,
                                                                     string descricao,
                                                                     string status,
                                                                     EnumNaturezaPlanoContas? naturezaPlanoContas,
                                                                     EnumTipoPlanoContas? tipoPlanoContas,
                                                                     string numeroPlanoContasContador)
        {
            return _repositorioPlanoDeContas.ConsulteLista(numeroPlanoContas, descricao, status, naturezaPlanoContas, tipoPlanoContas, numeroPlanoContasContador);
        }

        public List<PlanoDeContas> ConsulteLista()
        {
            return _repositorioPlanoDeContas.ConsulteLista();
        }

        public PlanoDeContas ConsultePlanoDeContasPeloNumero(string numeroPlanoContas)
        {
            return _repositorioPlanoDeContas.ConsultePlanoDeContasPeloNumero(numeroPlanoContas);
        }

        public PlanoDeContas ConsultePlanoDeContasAtivoPeloNumero(string numeroPlanoContas)
        {
            return _repositorioPlanoDeContas.ConsultePlanoDeContasAtivoPeloNumero(numeroPlanoContas);
        }
    }
}
