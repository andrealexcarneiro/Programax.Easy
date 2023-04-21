using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class PessoaMetodosAuxiliares
    {
        public EnumTipoInscricaoICMS? RetorneTipoInscricaoIcms(int IdParceiro)
        {
            if (IdParceiro != 0)
            {
                var pessoa = new ServicoPessoa().Consulte(IdParceiro);

                if (pessoa != null)
                {
                    if (pessoa.EmpresaPessoa != null)
                    {
                        if (pessoa.EmpresaPessoa.TipoInscricaoICMS != null)
                        {
                            return pessoa.EmpresaPessoa.TipoInscricaoICMS;
                        }
                        else
                        {
                            return !string.IsNullOrEmpty(pessoa.EmpresaPessoa.InscricaoEstadual) ? EnumTipoInscricaoICMS.CONTRIBUINTEICMS : EnumTipoInscricaoICMS.NAOCONTRIBUINTEICMS;
                        }
                    }
                }
            }

            return null;
        }
    }
}
