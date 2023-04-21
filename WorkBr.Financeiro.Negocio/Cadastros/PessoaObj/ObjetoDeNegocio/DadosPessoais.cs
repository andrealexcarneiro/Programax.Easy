using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class DadosPessoais: ObjetoDeNegocioBase
    {
        public virtual string NomeDoPai { get; set; }

        public virtual string NomeDaMae { get; set; }

        public virtual string Identidade { get; set; }

        public virtual string OrgaoEmissor { get; set; }

        public virtual DateTime? DataEmissao { get; set; }

        public virtual DateTime? DataDeNascimento { get; set; }

        public virtual EnumSexo? Sexo { get; set; }

        public virtual EnumEstadoCivil? EstadoCivil { get; set; }

        public virtual EnumGrauDeInstrucao? GrauDeInstrucao { get; set; }

        public virtual string Nacionalidade { get; set; }

        public virtual Cidade Naturalidade { get; set; }

        public virtual string Formacao { get; set; }

        public virtual string EmpresaQueTrabalha { get; set; }

        public virtual bool? PossuiResidenciaPropria { get; set; }

        public virtual string TempoDeResidencia { get; set; }

        public virtual double? RendaComprovada { get; set; }

        public virtual double? RendaFamiliar { get; set; }

        public virtual string FuncaoExercida { get; set; }

        public virtual string Hobby { get; set; }

        public virtual string EsporteFavorito { get; set; }

        public virtual string TimeFavorito { get; set; }

        public virtual string IdEstrangeiro { get; set; }
    }
}
