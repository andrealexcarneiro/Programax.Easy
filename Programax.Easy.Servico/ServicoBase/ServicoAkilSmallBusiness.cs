using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Servico.Servicos;
using Programax.Easy.Negocio;

namespace Programax.Easy.Servico.ServicoBase
{

    public abstract class ServicoAkilSmallBusiness<TObjetoDeNegocio, TValidacao, TConversorDeObjetoParaPersistencia> : ServicoBase<TObjetoDeNegocio, TValidacao, TConversorDeObjetoParaPersistencia>
        where TObjetoDeNegocio : ObjetoDeNegocioBase, new()
        where TValidacao : ValidacaoBase<TObjetoDeNegocio>, new()
        where TConversorDeObjetoParaPersistencia : IConversorDeObjeto<TObjetoDeNegocio>, new()
    {
        private bool _verificarPermissao;

        public ServicoAkilSmallBusiness()
            : base()
        {
            _verificarPermissao = true;
        }

        public ServicoAkilSmallBusiness(bool verificarPermissao, bool limparSessao)
            : base(limparSessao)
        {
            _verificarPermissao = verificarPermissao;
        }

        #region " MÉTODOS SOBRESCRITOS "

        public override int Cadastre(TObjetoDeNegocio objetoDeNegocio)
        {
            if (_verificarPermissao)
            {
                var permissaoUsuario = RetornePermissao();

                if (permissaoUsuario.Funcionalidade != EnumFuncionalidade.SEMVERIFICACAODEPERMISSAO && !permissaoUsuario.Alterar)
                {
                    throw new Exception("Você não possui permissão para inserir este item.");
                }
            }

            return base.Cadastre(objetoDeNegocio);
        }

        public override void Atualize(TObjetoDeNegocio objetoDeNegocio)
        {
            if (_verificarPermissao)
            {
                var permissaoUsuario = RetornePermissao();

                if (permissaoUsuario.Funcionalidade != EnumFuncionalidade.SEMVERIFICACAODEPERMISSAO && !permissaoUsuario.Alterar)
                {
                    throw new Exception("Você não possui permissão para atualizar este item.");
                }
            }

            base.Atualize(objetoDeNegocio);
        }

        public override void CadastreLista(List<TObjetoDeNegocio> ListaObjetoDeNegocio)
        {
            if (_verificarPermissao)
            {
                var permissaoUsuario = RetornePermissao();

                if (!permissaoUsuario.Alterar)
                {
                    throw new Exception("Você não possui permissão para inserir este item.");
                }
            }

            base.CadastreLista(ListaObjetoDeNegocio);
        }

        public override void Exclua(int idObjeto)
        {
            if (_verificarPermissao)
            {
                var permissaoUsuario = RetornePermissao();

                if (permissaoUsuario.Funcionalidade != EnumFuncionalidade.SEMVERIFICACAODEPERMISSAO && !permissaoUsuario.Alterar)
                {
                    throw new Exception("Você não possui permissão para excluir este item.");
                }
            }

            base.Exclua(idObjeto);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private Permissao RetornePermissao()
        {
            var funcionalidade = RetorneFuncionalidadeServico();

            var permissaoUsuario = Sessao.ListaDePermissoes.FirstOrDefault(permissao => permissao.Funcionalidade == funcionalidade);

            permissaoUsuario = permissaoUsuario ?? new Permissao();

            return permissaoUsuario;
        }

        private EnumFuncionalidade RetorneFuncionalidadeServico()
        {
            System.Attribute[] atributos = System.Attribute.GetCustomAttributes(this.GetType());

            foreach (System.Attribute atributo in atributos)
            {
                if (atributo is FuncionalidadeAttribute)
                {
                    FuncionalidadeAttribute funcionalidadeServico = (FuncionalidadeAttribute)atributo;

                    return funcionalidadeServico.Funcionaliade;
                }
            }

            throw new Exception("Não foi informada a funcionalidade do serviço.");
        }

        #endregion
    }
}
