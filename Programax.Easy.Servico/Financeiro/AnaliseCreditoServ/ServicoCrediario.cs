using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio;

namespace Programax.Easy.Servico.Financeiro.CrediarioServ
{
    [Funcionalidade(EnumFuncionalidade.CREDIARIO)]
    public class ServicoCrediario : ServicoAkilSmallBusiness<Crediario, ValidacaoCrediario, ConversorCrediario>
    {
        IRepositorioCrediario _repositorioAnaliseCredito;

        public ServicoCrediario()
        {
            RetorneRepositorio();
        }

        public ServicoCrediario(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Crediario> RetorneRepositorio()
        {
            if (_repositorioAnaliseCredito == null)
            {
                _repositorioAnaliseCredito = FabricaDeRepositorios.Crie<IRepositorioCrediario>();
            }

            return _repositorioAnaliseCredito;
        }

        public override int Cadastre(Crediario objetoDeNegocio)
        {
            objetoDeNegocio.DataUltimaAlteracao = DateTime.Now;
            objetoDeNegocio.UsuarioUltimaAlteracao = Sessao.PessoaLogada;

            return base.Cadastre(objetoDeNegocio);
        }

        public override void Atualize(Crediario objetoDeNegocio)
        {
            objetoDeNegocio.DataUltimaAlteracao = DateTime.Now;
            objetoDeNegocio.UsuarioUltimaAlteracao = Sessao.PessoaLogada;
            
            base.Atualize(objetoDeNegocio);
        }
    }
}
