using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio
{
    [Serializable]
    public class DadosEmpresa
    {
        public virtual string RazaoSocial { get; set; }

        public virtual string NomeFantasia { get; set; }

        public virtual string Cnpj { get; set; }

        public virtual string InscricaoEstadual { get; set; }

        public virtual string InscricaoMunicipal { get; set; }

        public virtual string Fax { get; set; }

        public virtual string Telefone { get; set; }

        public virtual string WhatsApp { get; set; }

        public virtual string Facebook { get; set; }

        public virtual string Twitter { get; set; }

        public virtual string Instagram { get; set; }
        public virtual string AliqInd { get; set; }
        public virtual string AliqCom { get; set; }
        public virtual string AliqServ { get; set; }
        public virtual string Versao { get; set; }

        public virtual int Config { get; set; }

        public virtual DateTime? DataCadastro { get; set; }

        public virtual EnderecoEmpresaComEmail Endereco { get; set; }

        public virtual EnumCodigoRegimeTributario? CodigoRegimeTributario { get; set; }

        public virtual Cnae Cnae { get; set; }

        public virtual byte[] Foto { get; set; }
    }
}
