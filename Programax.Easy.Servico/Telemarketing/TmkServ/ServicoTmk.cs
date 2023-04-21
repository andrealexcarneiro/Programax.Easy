using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using System.Collections.Generic;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;
using System;

namespace Programax.Easy.Servico.Telemarketing.TmkServ
{
    [Funcionalidade(EnumFuncionalidade.TELEMARKETING)]
    public class ServicoTmk : ServicoAkilSmallBusiness<Tmk, ValidacaoTmk, ConversorTmk>
    {
        protected IRepositorioTmk _repositorioTmk;        

        #region " CONSTRUTOR "

        public ServicoTmk()
        {
            RetorneRepositorio();
        }

        public ServicoTmk(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<Tmk> RetorneRepositorio()
        {
            if (_repositorioTmk == null)
            {
                _repositorioTmk = FabricaDeRepositorios.Crie<IRepositorioTmk>();
            }

            return _repositorioTmk;
        }

        #endregion


        #region "Consultas"

        public List<Tmk> ConsulteLista(int idTmk)
        {
            return _repositorioTmk.ConsulteLista(idTmk);
        }

        public List<Tmk> ConsulteListaParaTMK(Pessoa pessoa, EnumStatusAtendimento? statusTMK,
                                                                           DateTime? dataInicialPeriodo,
                                                                           DateTime? dataFinalPeriodo,
                                                                           int marcaId, int Carteira)
        {
            return _repositorioTmk.ConsulteListaParaTMK(pessoa, statusTMK, dataInicialPeriodo, dataFinalPeriodo, marcaId, Carteira);
        }

        public List<GerenciarTmk> ConsulteListaParaGerenciarTMK(Pessoa pessoa, EnumStatusAtendimento? statusTMK,
                                                                          DateTime? dataInicialPeriodo,
                                                                          DateTime? dataFinalPeriodo)
        {
            return _repositorioTmk.ConsulteListaParaGerenciarTMK(pessoa, statusTMK, dataInicialPeriodo, dataFinalPeriodo);
        }

        #endregion

    }
}
