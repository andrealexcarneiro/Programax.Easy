using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using System;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class EmpresaPessoa : ObjetoDeNegocioBase
    {
        public virtual string EmailPrincipal { get; set; }

        public virtual string EmailCobranca { get; set; }

        public virtual string InscricaoEstadual { get; set; }

        public virtual EnumTipoInscricaoICMS? TipoInscricaoICMS { get; set; } 

        public virtual string InscricaoMunicipal { get; set; }

        public virtual RamoAtividade RamoDeAtividade { get; set; }

        public virtual string NomeContato1 { get; set; }

        public virtual string RamalContato1 { get; set; }

        public virtual string DepartamentoContato1 { get; set; }

        public virtual string FuncaoContato1 { get; set; }

        public virtual string NomeContato2 { get; set; }

        public virtual string RamalContato2 { get; set; }

        public virtual string DepartamentoContato2 { get; set; }

        public virtual string FuncaoContato2 { get; set; }

        public virtual string NomeContato3 { get; set; }

        public virtual string RamalContato3 { get; set; }

        public virtual string DepartamentoContato3 { get; set; }

        public virtual string FuncaoContato3 { get; set; }
    }
}
