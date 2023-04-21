using System;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class Funcionario: ObjetoDeNegocioBase
    {
        public virtual DateTime? DataDeAdmissao { get; set; }

        public virtual DateTime? DataDeDemissao { get; set; }

        public virtual string MotivoRescisao { get; set; }

        public virtual string Matricula { get; set; }

        public virtual string Pis { get; set; }

        public virtual double? Salario { get; set; }

        public virtual string Cargo { get; set; }

        public virtual string Departamento { get; set; }

        #region " CTPS "

        public virtual string NumeroCtps { get; set; }

        public virtual string SerieCtps { get; set; }

        public virtual string UfCtps { get; set; }

        public virtual DateTime? DataEmissaoCtps { get; set; }

        #endregion

        #region " CERTIFICADO MILITAR "

        public virtual string NumeroCertificadoMilitar { get; set; }

        public virtual string CategoriaCertificadoMilitar { get; set; }

        #endregion

        #region " TITULO DE ELEITOR "

        public virtual string NumeroTituloEleitor { get; set; }

        public virtual string SerieTiuloEleitor { get; set; }

        public virtual string SecaoTituloEleitor { get; set; }

        #endregion

        #region " CONTA DEPÓSITO SALÁRIO "

        public virtual Banco Banco { get; set; }

        public virtual string Agencia { get; set; }

        public virtual string Operacao { get; set; }

        public virtual string Conta { get; set; }

        #endregion

        #region " CNH "

        public virtual string NumeroCnh { get; set; }

        public virtual string CategoriaCnh { get; set; }

        public virtual DateTime? ValidadeCnh { get; set; }

        #endregion

        #region " CARTEIRA CONSELHO "

        public virtual string ConselhoCarteiraConselho { get; set; }

        public virtual string NumeroCarteiraConselho { get; set; }

        public virtual DateTime? DataInscricaoCarteiraConselho { get; set; }

        #endregion
    }
}
