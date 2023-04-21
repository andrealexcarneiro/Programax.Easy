using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.PessoaObj.Repositorio;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class ConversorPessoa : ConversorDeObjetoBasico<Pessoa>, IConversorDeObjeto<Pessoa>
    {
        public Pessoa CopieObjetoParaPersistencia(Pessoa objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPessoa>();

            var pessoaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Pessoa();

            var atendimento = CopieAtendimento(objetoDeNegocio, pessoaDaBase);
            var dadosPessoais = CopieDadosPessoais(objetoDeNegocio, pessoaDaBase);
            var empresaPessoa = CopieEmpresaPessoa(objetoDeNegocio, pessoaDaBase);
            var funcionario = CopieFuncionario(objetoDeNegocio, pessoaDaBase);
            var vendedor = CopieVendedor(objetoDeNegocio, pessoaDaBase);

            var listaDeTelefones = CopieListaDeTelefones(objetoDeNegocio, pessoaDaBase);

            var listaDeEnderecos = CopieListaDeEnderecos(objetoDeNegocio, pessoaDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, pessoaDaBase);

            pessoaDaBase.ListaDeTelefones = listaDeTelefones;

            pessoaDaBase.ListaDeEnderecos = listaDeEnderecos;

            pessoaDaBase.Atendimento = atendimento;
            pessoaDaBase.DadosPessoais = dadosPessoais;
            pessoaDaBase.EmpresaPessoa = empresaPessoa;
            pessoaDaBase.Funcionario = funcionario;
            pessoaDaBase.Vendedor = vendedor;

            return pessoaDaBase;
        }

        private Atendimento CopieAtendimento(Pessoa objetoDeNegocio, Pessoa pessoaBase)
        {
            pessoaBase.Atendimento = pessoaBase.Atendimento ?? new Atendimento();

            int id = pessoaBase.Atendimento.Id;

            CopieTodasAsPropriedades(objetoDeNegocio.Atendimento, pessoaBase.Atendimento);

            pessoaBase.Atendimento.Id = id;

            return pessoaBase.Atendimento;
        }

        private DadosPessoais CopieDadosPessoais(Pessoa objetoDeNegocio, Pessoa pessoaBase)
        {
            pessoaBase.DadosPessoais = pessoaBase.DadosPessoais ?? new DadosPessoais();

            int id = pessoaBase.DadosPessoais.Id;

            CopieTodasAsPropriedades(objetoDeNegocio.DadosPessoais, pessoaBase.DadosPessoais);

            pessoaBase.DadosPessoais.Id = id;

            return pessoaBase.DadosPessoais;
        }

        private EmpresaPessoa CopieEmpresaPessoa(Pessoa objetoDeNegocio, Pessoa pessoaBase)
        {
            pessoaBase.EmpresaPessoa = pessoaBase.EmpresaPessoa ?? new EmpresaPessoa();

            int id = pessoaBase.EmpresaPessoa.Id;

            CopieTodasAsPropriedades(objetoDeNegocio.EmpresaPessoa, pessoaBase.EmpresaPessoa);

            pessoaBase.EmpresaPessoa.Id = id;

            return pessoaBase.EmpresaPessoa;
        }

        private Funcionario CopieFuncionario(Pessoa objetoDeNegocio, Pessoa pessoaBase)
        {
            pessoaBase.Funcionario = pessoaBase.Funcionario ?? new Funcionario();

            int id = pessoaBase.Funcionario.Id;

            CopieTodasAsPropriedades(objetoDeNegocio.Funcionario, pessoaBase.Funcionario);

            pessoaBase.Funcionario.Id = id;

            return pessoaBase.Funcionario;
        }

        private Vendedor CopieVendedor(Pessoa objetoDeNegocio, Pessoa pessoaBase)
        {
            pessoaBase.Vendedor = pessoaBase.Vendedor ?? new Vendedor();

            CopieTodasAsPropriedades(objetoDeNegocio.Vendedor, pessoaBase.Vendedor);

            return pessoaBase.Vendedor;
        }

        private IList<Telefone> CopieListaDeTelefones(Pessoa objetoDeNegocio, Pessoa pessoaBase)
        {
            var listaDeTelefones = pessoaBase.ListaDeTelefones;

            listaDeTelefones.Clear();

            foreach (var item in objetoDeNegocio.ListaDeTelefones)
            {
                var itemCopiado = new Telefone();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.Pessoa = pessoaBase;
                listaDeTelefones.Add(itemCopiado);
            }

            return listaDeTelefones;
        }

        private IList<EnderecoPessoa> CopieListaDeEnderecos(Pessoa objetoDeNegocio, Pessoa pessoaBase)
        {
            var listaDeEnderecos = pessoaBase.ListaDeEnderecos;

            listaDeEnderecos.Clear();

            foreach (var item in objetoDeNegocio.ListaDeEnderecos)
            {
                var itemCopiado = new EnderecoPessoa();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.Pessoa = pessoaBase;
                listaDeEnderecos.Add(itemCopiado);
            }

            return listaDeEnderecos;
        }
    }
}
