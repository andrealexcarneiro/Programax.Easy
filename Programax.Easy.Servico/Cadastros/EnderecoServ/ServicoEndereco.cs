using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;

namespace Programax.Easy.Servico.Cadastros.EnderecoServ
{
    [Funcionalidade(EnumFuncionalidade.ENDERECOS)]
    public class ServicoEndereco : ServicoAkilSmallBusiness<Endereco, ValidacaoEndereco, ConversorEndereco>
    {
        IRepositorioEndereco _repositorioEndereco;

        public ServicoEndereco()
        {
            RetorneRepositorio();
        }

        public ServicoEndereco(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override Infraestrutura.Negocio.ObjetosDeNegocio.IRepositorioBase<Endereco> RetorneRepositorio()
        {
            if (_repositorioEndereco == null)
            {
                _repositorioEndereco = FabricaDeRepositorios.Crie<IRepositorioEndereco>();
            }

            return _repositorioEndereco;
        }

        public Endereco Consulte(string cep)
        {
            return _repositorioEndereco.Consulte(cep);
        }

        public Endereco ConsulteAtivo(string cep)
        {
            return _repositorioEndereco.ConsulteAtivo(cep);
        }

        public List<Endereco> ConsulteLista(string cep, Estado estado, Cidade cidade, string bairro, string rua, string status)
        {
            return _repositorioEndereco.ConsulteLista(cep, estado, cidade, bairro, rua, status);
        }
    }
}
