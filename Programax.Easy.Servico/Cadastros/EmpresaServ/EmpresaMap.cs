using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.EmpresaServ
{
    public class EmpresaMap : MapeamentoBase<Empresa>
    {
        public EmpresaMap()
        {
            Table("EMPRESA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("EMPR_ID");

            MapeieDadosDaEmpresa();
            MapeieDadosContador();
        }

        private void MapeieDadosDaEmpresa()
        {
            Component(empresa => empresa.DadosEmpresa, dadosEmpresa =>
            {
                dadosEmpresa.Map(empresa => empresa.Foto).Column("EMPR_FOTO").Length(2147483647).LazyLoad(); ;

                dadosEmpresa.Map(empresa => empresa.Cnpj).Column("EMPR_FEDERAL");
                dadosEmpresa.Map(empresa => empresa.DataCadastro).Column("EMPR_DATA_CADASTRO");

                dadosEmpresa.Map(empresa => empresa.Fax).Column("EMPR_FAX");
                dadosEmpresa.Map(empresa => empresa.Telefone).Column("EMPR_TELEFONE");
                dadosEmpresa.Map(empresa => empresa.WhatsApp).Column("EMPR_WHATSAPP");
                dadosEmpresa.Map(empresa => empresa.InscricaoEstadual).Column("EMPR_ESTADUAL");
                dadosEmpresa.Map(empresa => empresa.InscricaoMunicipal).Column("EMPR_MUNICIPAL");
                dadosEmpresa.Map(empresa => empresa.NomeFantasia).Column("EMPR_FANTASIA");
                dadosEmpresa.Map(empresa => empresa.RazaoSocial).Column("EMPR_RAZAO");
                dadosEmpresa.Map(empresa => empresa.Facebook).Column("EMPR_FACEBOOK");
                dadosEmpresa.Map(empresa => empresa.Instagram).Column("EMPR_INSTAGRAM");
                dadosEmpresa.Map(empresa => empresa.Twitter).Column("EMPR_TWITTER");
                dadosEmpresa.Map(empresa => empresa.Config).Column("EMPR_ACQUA");
                dadosEmpresa.Map(empresa => empresa.AliqInd).Column("ALIQ_IND");
                dadosEmpresa.Map(empresa => empresa.AliqCom).Column("ALIQ_COM");
                dadosEmpresa.Map(empresa => empresa.AliqServ).Column("ALIQ_SERV");
                

                dadosEmpresa.Map(empresa => empresa.CodigoRegimeTributario).Column("EMPR_CODIGOREGIMETRIBUTARIO").CustomType<EnumCodigoRegimeTributario>(); ;

                dadosEmpresa.References(empresa => empresa.Cnae).Column("EMPR_CNAE_ID").Not.LazyLoad();

                dadosEmpresa.Component(empresa => empresa.Endereco, endereco =>
                {
                    endereco.Map(end => end.Complemento).Column("EMPR_COMPLEMENTO");
                    endereco.Map(end => end.Numero).Column("EMPR_NUMERO");
                    endereco.Map(end => end.Email).Column("EMPR_EMAIL");

                    endereco.Map(end => end.CEP).Column("EMPR_CEP");
                    endereco.Map(end => end.Rua).Column("EMPR_RUA");
                    endereco.Map(end => end.Bairro).Column("EMPR_BAIRRO");
                    endereco.References(end => end.Cidade).Column("EMPR_CIDADE").Not.LazyLoad().Fetch.Join();
                });
            });
        }

        private void MapeieDadosContador()
        {
            Component(empresa => empresa.DadosContador, dadosContador =>
            {
                dadosContador.Map(contador => contador.Escritorio).Column("EMPR_ESCRITORIOCONTADOR");

                dadosContador.Map(contador => contador.Nome).Column("EMPR_NOMECONTADOR");
                dadosContador.Map(contador => contador.Crc).Column("EMPR_CRCCONTADOR");

                dadosContador.Map(contador => contador.CpfContador).Column("EMPR_CPFCONTADOR");

                dadosContador.Map(contador => contador.Cnpj).Column("EMPR_CNPJESCRITORIO");

                dadosContador.Map(contador => contador.Fax).Column("EMPR_FAXCONTADOR");
                dadosContador.Map(contador => contador.Telefone).Column("EMPR_TELEFONECONTADOR");
                dadosContador.Map(contador => contador.Celular).Column("EMPR_CELULARCONTADOR");

                dadosContador.Component(contador => contador.Endereco, endereco =>
                {
                    endereco.Map(end => end.Complemento).Column("EMPR_COMPLEMENTOCONTADOR");
                    endereco.Map(end => end.Numero).Column("EMPR_NUMEROCONTADOR");
                    endereco.Map(end => end.Email).Column("EMPR_EMAILCONTADOR");

                    endereco.Map(end => end.CEP).Column("EMPR_CEPCONTADOR");
                    endereco.Map(end => end.Rua).Column("EMPR_RUACONTADOR");
                    endereco.Map(end => end.Bairro).Column("EMPR_BAIRROCONTADOR");
                    endereco.References(end => end.Cidade).Column("EMPR_CIDADECONTADOR").Not.LazyLoad().Fetch.Join();
                });
            });
        }
    }
}
