using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio
{
    [Serializable]
    public class RamoAtividade : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadatro { get; set; }
    }
}
