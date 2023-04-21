using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.Repositorio
{
    public interface IRepositorioBancoParaMovimento : IRepositorioBase<BancoParaMovimento>
    {
        List<BancoParaMovimento> ConsulteLista(string nomeBanco, string status);

        BancoParaMovimento ConsultePeloNomeBanco(string nomeBanco);

        BancoParaMovimento ConsulteBanco();

        BancoParaMovimento ConsulteBanco(bool ehPadrao);
    }
}
