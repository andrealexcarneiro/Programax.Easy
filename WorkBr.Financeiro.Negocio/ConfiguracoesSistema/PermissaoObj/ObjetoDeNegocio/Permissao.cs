using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class Permissao : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get { return Funcionalidade.Descricao(); } }

        public virtual string NomeMenu { get; set; }

        public virtual GrupoAcesso GrupoAcesso { get; set; }

        public virtual EnumFuncionalidade Funcionalidade { get; set; }

        public virtual bool Alterar { get; set; }

        public virtual bool Acessar { get; set; }
    }
}
