using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ
{
    public class VwNotasDocumentosMap : MapeamentoBase<VwNotasDocumentos>
    {
        public VwNotasDocumentosMap()
        {
            Table("VW_NOTAS_DOCUMENTOS");

            CompositeId()
                        .KeyProperty(x => x.Id, "VWNOTA_ID")
                        .KeyProperty(x => x.NumeroDocumento, "VWNOTA_NUMERO_DOCUMENTO")
                        .KeyProperty(x => x.TipoDocumento, "VWNOTA_TIPO_DOCUMENTO").CustomType<EnumTipoDocumento>();

            Map(vwnota => vwnota.AtendenteId).Column("VWNOTA_ATENDENTE_ID");
            Map(vwnota => vwnota.AtendenteNome).Column("VWNOTA_ATENDENTE_NOME");

            Map(vwnota => vwnota.Cep).Column("VWNOTA_CEP");
            Map(vwnota => vwnota.Cidade).Column("VWNOTA_CIDADE");
            Map(vwnota => vwnota.Estado).Column("VWNOTA_ESTADO");

            Map(vwnota => vwnota.ClienteCpfCnpj).Column("VWNOTA_PESSOA_CPF_CNPJ");
            Map(vwnota => vwnota.ClienteDataCadastro).Column("VWNOTA_PESSOA_DATA_CADASTRO");
            Map(vwnota => vwnota.ClienteId).Column("VWNOTA_PESSOA_ID");
            Map(vwnota => vwnota.ClienteInscricaoEstadual).Column("VWNOTA_PESSOA_INSCRICAO_ESTADUAL");
            Map(vwnota => vwnota.ClienteNomeFantasia).Column("VWNOTA_PESSOA_NOME");
            Map(vwnota => vwnota.ClienteStatus).Column("VWNOTA_PESSOA_STATUS");
            Map(vwnota => vwnota.ClienteTipo).Column("VWNOTA_PESSOA_TIPO_PESSOA").CustomType<EnumTipoPessoa>();

            Map(vwnota => vwnota.DataElaboracao).Column("VWNOTA_DATA_ELABORACAO");
            Map(vwnota => vwnota.DataEmissao).Column("VWNOTA_DATA_EMISSAO");

            Map(vwnota => vwnota.NaturezaOperacao).Column("VWNOTA_NATUREZA_OPERACAO_DESCRICAO");
            Map(vwnota => vwnota.NumeroNFe).Column("VWNOTA_NUMERO_NOTA");
            Map(vwnota => vwnota.Serie).Column("VWNOTA_SERIE_NOTA");
            Map(vwnota => vwnota.Modelo).Column("VWNOTA_MODELO_NOTAFISCAL").CustomType<EnumModeloNotaFiscal>();
            Map(vwnota => vwnota.StatusNotaFiscal).Column("VWNOTA_STATUS_NFE").CustomType<EnumStatusNotaFiscal>();

            Map(vwnota => vwnota.ValorTotal).Column("VWNOTA_VALOR_TOTAL_NOTA");

            Map(vwnota => vwnota.UsuarioId).Column("VWNOTA_USUARIO_DOCUMENTO_ID");
            Map(vwnota => vwnota.UsuarioNome).Column("VWNOTA_USUARIO_DOCUMENTO_NOME");

            Map(vwnota => vwnota.VendedorId).Column("VWNOTA_VENDEDOR_ID");
            Map(vwnota => vwnota.VendedorNome).Column("VWNOTA_VENDEDOR_NOME_FANTASIA");

            Map(vwnota => vwnota.MensagemErroNFe).Column("VWNOTA_MENSAGEM_ERRO_NFE");
            Map(vwnota => vwnota.MensagemDevolvida).Column("VWNOTA_MENSAGEM_DEVOLVIDA");

            Map(vwnota => vwnota.JahTemNotaCadastrada).Column("VWNOTA_JAH_TEM_NOTA_CADASTRADA");
        }
    }
}
