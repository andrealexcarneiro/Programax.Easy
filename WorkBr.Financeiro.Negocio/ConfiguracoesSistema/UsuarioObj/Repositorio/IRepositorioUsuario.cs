using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.Repositorio
{
    public interface IRepositorioUsuario: IRepositorioBase<Usuario>
    {
        Usuario ConsulteUsuario(string login);

        Usuario ConsulteUsuario(string login, string senha);

        void Cadastre(Usuario objeto, bool atualizarSenha);

        void Atualize(Usuario objeto, bool atualizarSenha);

        int ConsulteQuantidadeDeUsuariosAtivos();
    }
}
