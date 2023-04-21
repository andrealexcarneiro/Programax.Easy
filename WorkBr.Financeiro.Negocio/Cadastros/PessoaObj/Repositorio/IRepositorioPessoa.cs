using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.Repositorio
{
    public interface IRepositorioPessoa : IRepositorioBase<Pessoa>
    {
        Pessoa ConsulteClienteAtivo(int id);

        Pessoa ConsulteSupervisorAtivo(int id);

        Pessoa ConsulteVendedorAtivo(int id);

        Pessoa ConsulteAtendenteAtivo(int id);

        Pessoa ConsulteIndicadorAtivo(int id);
        
        List<Pessoa> ConsulteListaClientePelaRazaoSocial(string razaoSocial);

        List<Pessoa> ConsulteListaClientePeloNomeFantasia(string nomeFantasia);

        List<Pessoa> ConsulteListaClientePeloCpfCnpj(string cpfCnpj);

        List<Pessoa> ConsulteListaPessoaPelaRazaoSocial(string razaoSocial, EnumTipoEndereco? tipoEndereco, Estado estado, Cidade cidade, string status, bool pesquisePorCliente, bool pesquisePorFornecedor, bool pesquisePorFuncionario, bool pesquisePorVendedor, bool pesquisePorSupervisor, bool pesquisePorAtendente, bool pesquisePorTransportadora, bool pesquisePorIndicador);

        List<VWPessoasSelecao> ConsulteListaPessoaPelaRazaoSocialLetras(string razaoSocial);

        List<Pessoa> ConsulteListaPessoaPeloCnpjOuCpf(string CpfCnpj, EnumTipoEndereco? tipoEndereco, Estado estado, Cidade cidade, string status, bool pesquisePorCliente, bool pesquisePorFornecedor, bool pesquisePorFuncionario, bool pesquisePorVendedor, bool pesquisePorSupervisor, bool pesquisePorAtendente, bool pesquisePorTransportadora, bool pesquisePorIndicador);
        
        List<Pessoa> ConsulteListaPessoaPeloNomeFantasia(string nomeFantasia, EnumTipoEndereco? tipoEndereco, Estado estado, Cidade cidade, string status, bool pesquisePorCliente, bool pesquisePorFornecedor, bool pesquisePorFuncionario, bool pesquisePorVendedor, bool pesquisePorSupervisor, bool pesquisePorAtendente, bool pesquisePorTransportadora, bool pesquisePorIndicador);

        Pessoa ConsulteFornecedorAtivo(int id);

        Pessoa ConsulteTransportadoraAtiva(int id);

        Pessoa ConsultePessoaPeloCnpjOuCpf(string cnpjOuCpf);

        Pessoa ConsultePessoaAtiva(int id);

        Pessoa ConsulteFuncionarioAtivo(int id);

        List<Pessoa> ConsulteListaAtendentesAtivos();

        List<Pessoa> ConsulteListaVendedoresAtivos();

        List<Pessoa> ConsulteListaFuncionariosAtivos();

        List<Pessoa> ConsulteListaSupervisoresAtivos();

        List<Pessoa> ConsulteListaIndicadoresAtivos();

        List<Pessoa> ConsulteListaTransportadorasAtivas();

        List<Pessoa> ConsulteListaPessoas(bool ehCliente, bool ehFornecedor, bool ehFuncionario, bool ehTransportadora, Estado estado, Cidade cidade, Pessoa vendedor, Pessoa atendente, Pessoa indicador, RamoAtividadeObj.ObjetoDeNegocio.RamoAtividade ramoAtividade, string mesAnoAniversario, EnumOrdenacaoPesquisaPessoa ordenacaoPesquisa, EnumTipoCliente tipoCliente);

        bool ExistePessoasComMesmoCpfOuCnpj(string cpfCnpj, int idPessoaDesconsiderar);

        List<Pessoa> ConsulteListaPessoaPeloCnpjOuCpf(string cpfCnpj);
        
        List<Pessoa> ConsulteListaPessoasId(List<int> listaIdPessoas);

        List<VWClienteSemComprar> ConsulteListaVWClientesSemComprar(int? atendenteId, 
                                                                                                              int? vendedorId, 
                                                                                                              int? diasInicialSemComprar, 
                                                                                                              int? diasFimSemComprar, 
                                                                                                              bool somenteClientesQueJahCompraram,
                                                                                                              string uf,
                                                                                                              int? cidadeId,
                                                                                                              string bairro);

        List<Telefone> ConsulteListaDeTelefones(string telefone);

    }
}
