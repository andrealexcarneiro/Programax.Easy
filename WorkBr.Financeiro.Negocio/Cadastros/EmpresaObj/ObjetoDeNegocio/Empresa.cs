using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio
{
    [Serializable]
    public class Empresa : ObjetoDeNegocioBase
    {
        public Empresa()
        {
            DadosEmpresa = new DadosEmpresa();
            DadosContador = new DadosContador();
        }

        public virtual DadosEmpresa DadosEmpresa { get; set; }

        public virtual DadosContador DadosContador { get; set; }
    }
}
