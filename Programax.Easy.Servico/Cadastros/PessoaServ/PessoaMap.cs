using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class PessoaMap : MapeamentoBase<Pessoa>
    {
        public PessoaMap()
            : base()
        {
            Table("PESSOAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PES_ID");

            MapeieDadosGerais();
            MapeieAtendimento();
            MapeieEmpresaPessoa();
            MapeieDadosPessoais();
            MapeieFuncionario();
            MapeieVendedor();
            MapeieTelefones();
            MapeieEnderecos();
            MapeieComissoes();
        }

        #region " MÉTODOS PARA MAPEAR "

        private void MapeieDadosGerais()
        {
            Component(pessoa => pessoa.DadosGerais, dadosGerais =>
            {
                dadosGerais.Map(x => x.Razao).Column("PES_RAZAO");
                dadosGerais.Map(x => x.TipoPessoa).Column("PES_TIPOINSCRICAO").CustomType<EnumTipoPessoa>();
                dadosGerais.Map(x => x.TipoCliente).Column("PES_TIPO_CLIENTE").CustomType<EnumTipoCliente>();
                dadosGerais.Map(x => x.EhCliente).Column("PES_CLIENTE");
                dadosGerais.Map(x => x.EhFornecedor).Column("PES_FORNECEDOR");
                dadosGerais.Map(x => x.EhFuncionario).Column("PES_FUNCIONARIO");
                dadosGerais.Map(x => x.EhTransportadora).Column("PES_TRANSPORTADORA");
                dadosGerais.Map(x => x.NomeFantasia).Column("PES_FANTASIA");
                dadosGerais.Map(x => x.Status).Column("PES_STATUS");
                dadosGerais.Map(x => x.CpfCnpj).Column("PES_INSC_FEDERAL");
                dadosGerais.Map(x => x.DataCadastro).Column("PES_DATA_CADASTRO");
                dadosGerais.Map(x => x.Foto).Column("PES_FOTO").Length(2147483647).LazyLoad();
                dadosGerais.Map(x => x.PessoaResideExterior).Column("PES_PESSOA_RESIDE_EXTERIOR");
                
                dadosGerais.References(x => x.Pais).Column("PES_PAIS_ID");
            });
        }

        private void MapeieAtendimento()
        {
            References(x => x.Atendimento).Column("PES_PESAT_ID").Cascade.All().Not.LazyLoad();            
        }

        private void MapeieEmpresaPessoa()
        {
            References(x => x.EmpresaPessoa).Column("PES_PESEMPR_ID").Cascade.All().LazyLoad();
        }

        private void MapeieDadosPessoais()
        {
            References(x => x.DadosPessoais).Column("PES_PESPES_ID").Cascade.All().LazyLoad();
        }

        private void MapeieFuncionario()
        {
            References(x => x.Funcionario).Column("PES_PESFUNC_ID").Cascade.All().LazyLoad();
        }

        private void MapeieVendedor()
        {
            Component(pessoa => pessoa.Vendedor, vendedor =>
            {
                vendedor.Map(x => x.EhAtendente).Column("PES_EH_ATENDENTE");
                vendedor.Map(x => x.EhIndicador).Column("PES_EH_INDICADOR");
                vendedor.Map(x => x.EhSupervisor).Column("PES_EH_SUPERVISOR");
                vendedor.Map(x => x.EhVendedor).Column("PES_EH_VENDEDOR");
            });
        }

        private void MapeieTelefones()
        {
            HasMany(pessoa => pessoa.ListaDeTelefones).KeyColumn("TELE_PES_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("TELEFONES");
        }

        private void MapeieEnderecos()
        {
            HasMany(pessoa => pessoa.ListaDeEnderecos).KeyColumn("ENDPES_PES_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("ENDERECOSPESSOAS");
        }

        private void MapeieComissoes()
        {
            HasMany(pessoa => pessoa.ListaDeComissoes).KeyColumn("COMISSAO_PESSOA_ID");
        }

        #endregion
    }
}
