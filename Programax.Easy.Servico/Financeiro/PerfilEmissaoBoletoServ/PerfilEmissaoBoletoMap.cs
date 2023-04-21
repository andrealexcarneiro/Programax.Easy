using Programax.Easy.Negocio.Financeiro.PerfilEmissaoBoletoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Financeiro.PerfilEmissaoBoletoSev
{
    public class PerfilEmissaoBoletoMap : MapeamentoBase<PerfilEmissaoBoleto>
    {
        public PerfilEmissaoBoletoMap()
        {
            Table("PERFILEMISSAOBOLETOS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PEB_ID");

            Map(perfil => perfil.Descricao).Column("PEB_DESCRICAO");
            Map(perfil => perfil.DataCadastro).Column("PEB_DATA_HORA_CRIACAO");
            Map(perfil => perfil.PessoaId).Column ("PEB_PESSOA_ID");
            Map(perfil => perfil.EhPerfilPadrao).Column("PEB_EH_PERFIL_PADRAO");

        }
    }
}
