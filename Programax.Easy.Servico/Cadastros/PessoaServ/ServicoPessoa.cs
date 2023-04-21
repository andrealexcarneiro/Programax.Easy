using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using System.Transactions;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    [Funcionalidade(EnumFuncionalidade.PESSOAS)]
    public class ServicoPessoa : ServicoAkilSmallBusiness<Pessoa, ValidacaoPessoa, ConversorPessoa>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioPessoa _repositorioPessoa;

        #endregion

        #region " CONSTRUTOR "

        public ServicoPessoa()
        {
            RetorneRepositorio();
        }

        public ServicoPessoa(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<Pessoa> RetorneRepositorio()
        {
            if (_repositorioPessoa == null)
            {
                _repositorioPessoa = FabricaDeRepositorios.Crie<IRepositorioPessoa>();
            }

            return _repositorioPessoa;
        }

        public override int Cadastre(Pessoa objetoDeNegocio)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                int id = base.Cadastre(objetoDeNegocio);

                ServicoParametros servicoParametros = new ServicoParametros(false);
                var parametros = servicoParametros.ConsulteParametros();

                ServicoCrediario servicoAnaliseCredito = new ServicoCrediario(false, false);

                Crediario analiseCredito = new Crediario();
                analiseCredito.Id = id;
                analiseCredito.Pessoa = objetoDeNegocio;
                analiseCredito.PodeAlterarMultaEJuros = true;
                analiseCredito.StatusAnaliseCredito = EnumStatusCrediario.LIBERADO;
                analiseCredito.ValorLimiteCredito = parametros.ParametrosFinanceiro.ValoPadraoCreditoInicial;

                servicoAnaliseCredito.Cadastre(analiseCredito);

                scope.Complete();

                return id;
            }
        }

        #endregion

        #region " CONSULTAS "

        #region " CONSULTA ÚNICA PESSOA "

        public Pessoa ConsulteClienteAtivo(int id)
        {
            return _repositorioPessoa.ConsulteClienteAtivo(id);
        }

        public Pessoa ConsultePessoaAtiva(int id)
        {
            return _repositorioPessoa.ConsultePessoaAtiva(id);
        }

        public Pessoa ConsulteSupervisorAtivo(int id)
        {
            return _repositorioPessoa.ConsulteSupervisorAtivo(id);
        }

        public Pessoa ConsulteVendedorAtivo(int id)
        {
            return _repositorioPessoa.ConsulteVendedorAtivo(id);
        }

        public Pessoa ConsulteAtendenteAtivo(int id)
        {
            return _repositorioPessoa.ConsulteAtendenteAtivo(id);
        }

        public Pessoa ConsulteIndicadorAtivo(int id)
        {
            return _repositorioPessoa.ConsulteIndicadorAtivo(id);
        }

        public Pessoa ConsulteFornecedorAtivo(int id)
        {
            return _repositorioPessoa.ConsulteFornecedorAtivo(id);
        }

        public Pessoa ConsulteFuncionarioAtivo(int id)
        {
            return _repositorioPessoa.ConsulteFuncionarioAtivo(id);
        }

        public Pessoa ConsultePessoaPeloCnpjOuCpf(string cnpjOuCpf)
        {
            return _repositorioPessoa.ConsultePessoaPeloCnpjOuCpf(cnpjOuCpf);
        }

        public Pessoa ConsulteTransportadoraAtiva(int id)
        {
            return _repositorioPessoa.ConsulteTransportadoraAtiva(id);
        }
        
        #endregion

        #region " CONSULTA LISTA "

        public List<Pessoa> ConsulteListaAtendentesAtivos()
        {
            return _repositorioPessoa.ConsulteListaAtendentesAtivos();
        }

        public List<Pessoa> ConsulteListaVendedoresAtivos()
        {
            return _repositorioPessoa.ConsulteListaVendedoresAtivos();
        }

        public List<Pessoa> ConsulteListaClientePelaRazaoSocial(string razaoSocial)
        {
            return _repositorioPessoa.ConsulteListaClientePelaRazaoSocial(razaoSocial);
        }

        public List<Pessoa> ConsulteListaClientePeloNomeFantasia(string nomeFantasia)
        {
            return _repositorioPessoa.ConsulteListaClientePeloNomeFantasia(nomeFantasia);
        }

        public List<Pessoa> ConsulteListaClientePeloCpfCnpj(string cpfCnpj)
        {
            return _repositorioPessoa.ConsulteListaClientePeloCpfCnpj(cpfCnpj);
        }

        public List<Pessoa> ConsulteListaPessoaPelaRazaoSocial(string razaoSocial, EnumTipoEndereco? tipoEndereco, Estado estado, Cidade cidade, string status, bool pesquisePorCliente, bool pesquisePorFornecedor, bool pesquisePorFuncionario, bool pesquisePorVendedor, bool pesquisePorSupervisor, bool pesquisePorAtendente, bool pesquisePorTransportadora, bool pesquisePorIndicador)
        {
            return _repositorioPessoa.ConsulteListaPessoaPelaRazaoSocial(razaoSocial, tipoEndereco, estado, cidade, status, pesquisePorCliente, pesquisePorFornecedor, pesquisePorFuncionario, pesquisePorVendedor, pesquisePorSupervisor, pesquisePorAtendente, pesquisePorTransportadora, pesquisePorIndicador);
        }

        public List<VWPessoasSelecao> ConsulteListaPessoaPelaRazaoSocialLetras(string razaoSocial)
        {
            return _repositorioPessoa.ConsulteListaPessoaPelaRazaoSocialLetras(razaoSocial);
        }

        public List<Pessoa> ConsulteListaPessoaPeloNomeFantasia(string nomeFantasia, string status, bool pesquisePorCliente, bool pesquisePorFornecedor, bool pesquisePorFuncionario)
        {
            return ConsulteListaPessoaPeloNomeFantasia(nomeFantasia, null, null, null, status, pesquisePorCliente, pesquisePorFornecedor, pesquisePorFuncionario, false, false, false, false, false);
        }

        public List<Pessoa> ConsulteListaPessoaPeloNomeFantasia(string nomeFantasia, EnumTipoEndereco? tipoEndereco, Estado estado, Cidade cidade, string status, bool pesquisePorCliente, bool pesquisePorFornecedor, bool pesquisePorFuncionario, bool pesquisePorVendedor, bool pesquisePorSupervisor, bool pesquisePorAtendente, bool pesquisePorTransportadora, bool pesquisePorIndicador)
        {
            return _repositorioPessoa.ConsulteListaPessoaPeloNomeFantasia(nomeFantasia, tipoEndereco, estado, cidade, status, pesquisePorCliente, pesquisePorFornecedor, pesquisePorFuncionario, pesquisePorVendedor, pesquisePorSupervisor, pesquisePorAtendente, pesquisePorTransportadora, pesquisePorIndicador);
        }

        public List<Pessoa> ConsulteListaPessoaPeloCpfCnpj(string cpfCnpj, EnumTipoEndereco? tipoEndereco, Estado estado, Cidade cidade, string status, bool pesquisePorCliente, bool pesquisePorFornecedor, bool pesquisePorFuncionario, bool pesquisePorVendedor, bool pesquisePorSupervisor, bool pesquisePorAtendente, bool pesquisePorTransportadora, bool pesquisePorIndicador)
        {
            return _repositorioPessoa.ConsulteListaPessoaPeloCnpjOuCpf(cpfCnpj, tipoEndereco, estado, cidade, status, pesquisePorCliente, pesquisePorFornecedor, pesquisePorFuncionario, pesquisePorVendedor, pesquisePorSupervisor, pesquisePorAtendente, pesquisePorTransportadora, pesquisePorIndicador);
        }

        public List<Pessoa> ConsulteListaPessoaPeloCpfCnpj(string cpfCnpj)
        {
            return _repositorioPessoa.ConsulteListaPessoaPeloCnpjOuCpf(cpfCnpj);
        }
        
        public List<Pessoa> ConsulteListaFuncionariosAtivos()
        {
            return _repositorioPessoa.ConsulteListaFuncionariosAtivos();
        }

        public List<Pessoa> ConsulteListaSupervisoresAtivos()
        {
            return _repositorioPessoa.ConsulteListaSupervisoresAtivos();
        }

        public List<Pessoa> ConsulteListaIndicadoresAtivos()
        {
            return _repositorioPessoa.ConsulteListaIndicadoresAtivos();
        }

        public List<Pessoa> ConsulteListaTransportadorasAtivas()
        {
            return _repositorioPessoa.ConsulteListaTransportadorasAtivas();
        }

        public List<Pessoa> ConsulteListaPessoas(bool ehCliente, bool ehFornecedor, bool ehFuncionario, bool ehTransportadora, Estado estado, Cidade cidade, Pessoa vendedor, Pessoa atendente, Pessoa indicador, RamoAtividade ramoAtividade, string mesAnoAniversario, EnumOrdenacaoPesquisaPessoa ordenacaoPesquisa, EnumTipoCliente tipoCliente)
        {
            return _repositorioPessoa.ConsulteListaPessoas(ehCliente, ehFornecedor, ehFuncionario, ehTransportadora, estado, cidade, vendedor, atendente, indicador, ramoAtividade, mesAnoAniversario, ordenacaoPesquisa, tipoCliente);
        }

        public List<Pessoa> ConsulteListaPessoasId(List<int> listaIdPessoas)
        {
            return _repositorioPessoa.ConsulteListaPessoasId(listaIdPessoas);
        }

        public bool ExistePessoasComMesmoCpfOuCnpj(string cpfCnpj, int idPessoaDesconsiderar)
        {
            return _repositorioPessoa.ExistePessoasComMesmoCpfOuCnpj(cpfCnpj, idPessoaDesconsiderar);
        }

        public List<VWClienteSemComprar> ConsulteListaVWClientesSemComprar(int? atendenteId,
                                                                                                                        int? vendedorId,
                                                                                                                        int? diasInicialSemComprar,
                                                                                                                        int? diasFimSemComprar,
                                                                                                                        bool somenteClientesQueJahCompraram,
                                                                                                                        string uf,
                                                                                                                        int? cidadeId,
                                                                                                                        string bairro)
        {
            return _repositorioPessoa.ConsulteListaVWClientesSemComprar(atendenteId,
                                                                                                            vendedorId,
                                                                                                            diasInicialSemComprar,
                                                                                                            diasFimSemComprar,
                                                                                                            somenteClientesQueJahCompraram,
                                                                                                            uf,
                                                                                                            cidadeId,
                                                                                                            bairro);
        }

        public List<Telefone> ConsulteListaDeTelefones(string telefone)
        {
            return _repositorioPessoa.ConsulteListaDeTelefones(telefone);
        }
        
        #endregion

        #endregion

        #region " VALIDAÇÕES "

        public void ValideTelefonePessoa(Telefone telefone)
        {
            ValidacaoTelefone validacaoTelefone = new ValidacaoTelefone();

            validacaoTelefone.ValideInclusao();

            validacaoTelefone.Valide(telefone).AssegureSucesso();
        }

        public void ValideEnderecoPessoa(EnderecoPessoa enderecoPessoa, List<EnderecoPessoa> listaEnderecos)
        {
            ValidacaoEnderecoPessoa validacaoEnderecoPessoa = new ValidacaoEnderecoPessoa();

            validacaoEnderecoPessoa.ListaEnderecos = listaEnderecos;

            validacaoEnderecoPessoa.ValideInclusao();

            validacaoEnderecoPessoa.Valide(enderecoPessoa).AssegureSucesso();
        }

        #endregion
    }
}
