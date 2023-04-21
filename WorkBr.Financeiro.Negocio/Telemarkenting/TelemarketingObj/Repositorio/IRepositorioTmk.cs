using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;

namespace Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio
{
    public interface IRepositorioTmk : IRepositorioBase<Tmk>
    {
        List<Tmk> ConsulteLista(int idTmk);

        List<Tmk> ConsulteListaParaTMK(Pessoa pessoa, EnumStatusAtendimento? statusTMK, 
                                                                           DateTime? dataInicialPeriodo,
                                                                           DateTime? dataFinalPeriodo,
                                                                           int marcaId, int Carteira);
        List<GerenciarTmk> ConsulteListaParaGerenciarTMK(Pessoa pessoa, EnumStatusAtendimento? statusTMK, DateTime? dataInicialPeriodo, DateTime? dataFinalPeriodo);
    }
}
