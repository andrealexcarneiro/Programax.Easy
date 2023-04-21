using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio
{
    [Serializable]
    public class GrupoAcesso : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual bool Tesoureiro { get; set; }
    }
}
