using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ChequeObj.Repositorio
{
    public interface IRepositorioCheque : IRepositorioBase<Cheque>
    {
        List<Cheque> ConsulteLista( Pessoa pessoa, 
                                                  EnumDataFiltrarCheque? dataFiltrarCheque, 
                                                  DateTime? dataInicialPeriodo, 
                                                  DateTime? dataFinalPeriodo, 
                                                  Banco banco, 
                                                  string numeroCheque, 
                                                  bool statusAbertoDepositado, 
                                                  bool statusRecebido, 
                                                  bool statusDevolvidoPrimeira, 
                                                  bool statusDevolvidoSegunda, 
                                                  bool statusReapresentado, 
                                                  bool statusCustodiadoFactoring, 
                                                  bool statusInativo );

        List<Cheque> ConsulteListaChequePorPedido(int numeroDoPedido);

        Cheque ConsulteChequePeloNumeroDocumento(string numeroDocumento);

        Cheque Consulte(Banco banco, string agencia, string conta, string digito, string serie, string numeroCheque);

        Cheque ConsulteJoinComItens(int chequeId);
    }
}
