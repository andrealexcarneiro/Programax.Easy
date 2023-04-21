using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio;
using NHibernate.Linq;

namespace Programax.Easy.Servico.Cadastros.PessoaServ
{
    public class RepositorioPessoa : RepositorioBase<Pessoa>, IRepositorioPessoa
    {
        #region " VARIÁVEIS PRIVADAS "

        private EnderecoPessoa _enderecoPessoaDaConsulta;
        private Cidade _cidadeDaConsulta;
        private Atendimento _atendimentoDaConsulta;
        private EmpresaPessoa _empresaPessoaDaConsulta;
        private Pessoa _vendedorDaConsulta;
        private DadosPessoais _dadosPessoaisDaConsulta;

        #endregion

        #region " CONSTRUTOR "

        public RepositorioPessoa(ISession sessao)
            : base(sessao)
        {

        }

        #endregion

        #region " CONSULTE PESSOA "

        public Pessoa ConsultePessoaAtiva(int id)
        {
            return _session.QueryOver<Pessoa>().Where(pessoa => pessoa.Id == id && pessoa.DadosGerais.Status == "A").SingleOrDefault();
        }

        public Pessoa ConsulteClienteAtivo(int id)
        {
            return _session.QueryOver<Pessoa>().Where(pessoa => pessoa.Id == id && pessoa.DadosGerais.EhCliente && pessoa.DadosGerais.Status == "A").Take(1).SingleOrDefault();
        }

        public Pessoa ConsulteSupervisorAtivo(int id)
        {
            return _session.QueryOver<Pessoa>()
                                    .TransformUsing(Transformers.DistinctRootEntity)
                                    .Where(p => p.Id == id && p.DadosGerais.Status == "A" && p.Vendedor != null && p.Vendedor.EhSupervisor).SingleOrDefault();
        }

        public Pessoa ConsulteVendedorAtivo(int id)
        {
            return _session.QueryOver<Pessoa>()
                                    .TransformUsing(Transformers.DistinctRootEntity)
                                    .Where(p => p.Id == id && p.DadosGerais.Status == "A" && p.Vendedor != null && p.Vendedor.EhVendedor).SingleOrDefault();
        }

        public Pessoa ConsulteAtendenteAtivo(int id)
        {
            return _session.QueryOver<Pessoa>()
                                    .TransformUsing(Transformers.DistinctRootEntity)
                                    .Where(p => p.Id == id && p.DadosGerais.Status == "A" && p.Vendedor != null && p.Vendedor.EhAtendente).SingleOrDefault();
        }

        public Pessoa ConsulteIndicadorAtivo(int id)
        {
            return _session.QueryOver<Pessoa>()
                                    .TransformUsing(Transformers.DistinctRootEntity)
                                    .Where(p => p.Id == id && p.DadosGerais.Status == "A" && p.Vendedor != null && p.Vendedor.EhIndicador).SingleOrDefault();
        }

        public Pessoa ConsulteFornecedorAtivo(int id)
        {
            return _session.QueryOver<Pessoa>()
                                    .TransformUsing(Transformers.DistinctRootEntity)
                                    .Where(p => p.Id == id && p.DadosGerais.Status == "A" && p.DadosGerais.EhFornecedor).SingleOrDefault();
        }

        public Pessoa ConsulteTransportadoraAtiva(int id)
        {
            return _session.QueryOver<Pessoa>()
                                    .TransformUsing(Transformers.DistinctRootEntity)
                                    .Where(p => p.Id == id && p.DadosGerais.Status == "A" && p.DadosGerais.EhTransportadora).SingleOrDefault();
        }

        public Pessoa ConsulteFuncionarioAtivo(int id)
        {
            return _session.QueryOver<Pessoa>()
                                    .TransformUsing(Transformers.DistinctRootEntity)
                                    .Where(p => p.Id == id && p.DadosGerais.Status == "A" && p.DadosGerais.EhFuncionario).SingleOrDefault();
        }

        public Pessoa ConsultePessoaPeloCnpjOuCpf(string cnpjOuCpf)
        {
            return _session.QueryOver<Pessoa>()
                                    .TransformUsing(Transformers.DistinctRootEntity)
                                    .Where(p => p.DadosGerais.CpfCnpj == cnpjOuCpf).Take(1).SingleOrDefault();
        }
        
        #endregion

        #region " CONSULTE LISTA DE CLIENTES "

        public List<Pessoa> ConsulteListaClientePelaRazaoSocial(string razaoSocial)
        {
            return _session.QueryOver<Pessoa>().Where(pessoa => pessoa.DadosGerais.Razao.IsLike("%" + razaoSocial + "%") && pessoa.DadosGerais.EhCliente).List().ToList();
        }

        public List<Pessoa> ConsulteListaClientePeloNomeFantasia(string nomeFantasia)
        {
            return _session.QueryOver<Pessoa>().Where(pessoa => pessoa.DadosGerais.NomeFantasia.Contains(nomeFantasia) && pessoa.DadosGerais.EhCliente).List().ToList();
        }

        public List<Pessoa> ConsulteListaClientePeloCpfCnpj(string cpfCnpj)
        {
            return _session.QueryOver<Pessoa>().Where(pessoa => pessoa.DadosGerais.CpfCnpj == cpfCnpj && pessoa.DadosGerais.EhCliente).List().ToList();
        }

        #endregion

        #region " CONSULTA LISTA DE PESSOAS "

        public List<Pessoa> ConsulteListaPessoaPelaRazaoSocial(string razaoSocial,
                                                                                         EnumTipoEndereco? tipoEndereco,
                                                                                         Estado estado,
                                                                                         Cidade cidade,
                                                                                         string status,
                                                                                         bool pesquisePorCliente,
                                                                                         bool pesquisePorFornecedor,
                                                                                         bool pesquisePorFuncionario,
                                                                                         bool pesquisePorVendedor,
                                                                                         bool pesquisePorSupervisor,
                                                                                         bool pesquisePorAtendente,
                                                                                         bool pesquisePorTransportadora,
                                                                                         bool pesquisePorIndicador)
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.DadosGerais.Razao.IsLike("%" + razaoSocial + "%");

            var expressaoDeConsultaGenerica = ConstruaFuncaoDeConsulta(tipoEndereco, estado, cidade, status, pesquisePorCliente, pesquisePorFornecedor, pesquisePorFuncionario, pesquisePorVendedor, pesquisePorSupervisor, pesquisePorAtendente, pesquisePorTransportadora, pesquisePorIndicador);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoDeConsultaGenerica);

            return ConsultePessoas(expressaoParaConsulta);
        }


        public List<VWPessoasSelecao> ConsulteListaPessoaPelaRazaoSocialLetras(string razaoSocial)
        {
            return _session.QueryOver<VWPessoasSelecao>().Where(pessoa => pessoa.Razao.IsLike("%" + razaoSocial + "%")).List().ToList();

        }


        public List<Pessoa> ConsulteListaPessoaPeloCnpjOuCpf(string CpfCnpj,
                                                                                         EnumTipoEndereco? tipoEndereco,
                                                                                         Estado estado,
                                                                                         Cidade cidade,
                                                                                         string status,
                                                                                         bool pesquisePorCliente,
                                                                                         bool pesquisePorFornecedor,
                                                                                         bool pesquisePorFuncionario,
                                                                                         bool pesquisePorVendedor,
                                                                                         bool pesquisePorSupervisor,
                                                                                         bool pesquisePorAtendente,
                                                                                         bool pesquisePorTransportadora,
                                                                                         bool pesquisePorIndicador)
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.DadosGerais.CpfCnpj == CpfCnpj;

            var expressaoDeConsultaGenerica = ConstruaFuncaoDeConsulta(tipoEndereco, estado, cidade, status, pesquisePorCliente, pesquisePorFornecedor, pesquisePorFuncionario, pesquisePorVendedor, pesquisePorSupervisor, pesquisePorAtendente, pesquisePorTransportadora, pesquisePorIndicador);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoDeConsultaGenerica);

            return ConsultePessoas(expressaoParaConsulta);
        }

        public List<Pessoa> ConsulteListaPessoaPeloNomeFantasia(string nomeFantasia,
                                                                                             EnumTipoEndereco? tipoEndereco,
                                                                                             Estado estado,
                                                                                             Cidade cidade,
                                                                                             string status,
                                                                                             bool pesquisePorCliente,
                                                                                             bool pesquisePorFornecedor,
                                                                                             bool pesquisePorFuncionario,
                                                                                             bool pesquisePorVendedor,
                                                                                             bool pesquisePorSupervisor,
                                                                                             bool pesquisePorAtendente,
                                                                                             bool pesquisePorTransportadora,
                                                                                             bool pesquisePorIndicador)
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.DadosGerais.NomeFantasia.IsLike("%" + nomeFantasia + "%");

            var expressaoDeConsultaGenerica = ConstruaFuncaoDeConsulta(tipoEndereco, estado, cidade, status, pesquisePorCliente, pesquisePorFornecedor, pesquisePorFuncionario, pesquisePorVendedor, pesquisePorSupervisor, pesquisePorAtendente, pesquisePorTransportadora, pesquisePorIndicador);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoDeConsultaGenerica);

            return ConsultePessoas(expressaoParaConsulta);
        }

        public List<Pessoa> ConsulteListaAtendentesAtivos()
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.DadosGerais.Status == "A" &&
                                                                                                                   pessoa.Vendedor != null &&
                                                                                                                   pessoa.Vendedor.EhAtendente == true;

            return ConsultePessoas(expressaoParaConsulta);
        }

        public List<Pessoa> ConsulteListaVendedoresAtivos()
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.DadosGerais.Status == "A" &&
                                                                                                                   pessoa.Vendedor != null &&
                                                                                                                   pessoa.Vendedor.EhVendedor == true;

            return ConsultePessoas(expressaoParaConsulta);
        }

        public List<Pessoa> ConsulteListaFuncionariosAtivos()
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.DadosGerais.Status == "A" &&
                                                                                                                   pessoa.DadosGerais.EhFuncionario == true;

            return ConsultePessoas(expressaoParaConsulta);
        }

        public List<Pessoa> ConsulteListaSupervisoresAtivos()
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.DadosGerais.Status == "A" &&
                                                                                                                   pessoa.Vendedor != null &&
                                                                                                                   pessoa.Vendedor.EhSupervisor == true;

            return ConsultePessoas(expressaoParaConsulta);
        }

        public List<Pessoa> ConsulteListaIndicadoresAtivos()
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.DadosGerais.Status == "A" &&
                                                                                                                   pessoa.Vendedor != null &&
                                                                                                                   pessoa.Vendedor.EhIndicador == true;

            return ConsultePessoas(expressaoParaConsulta);
        }

        public List<Pessoa> ConsulteListaTransportadorasAtivas()
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.DadosGerais.Status == "A" &&
                                                                                                                   pessoa.DadosGerais.EhTransportadora == true;

            return ConsultePessoas(expressaoParaConsulta);
        }

        public List<Pessoa> ConsulteListaPessoas(bool ehCliente, bool ehFornecedor, bool ehFuncionario, bool ehTransportadora, Estado estado, Cidade cidade, Pessoa vendedor, Pessoa atendente, Pessoa indicador, RamoAtividade ramoAtividade, string mesAnoAniversario, EnumOrdenacaoPesquisaPessoa ordenacaoPesquisa, EnumTipoCliente tipoCliente)
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.Id > 0;

            var expressaoDeConsultaGenerica = ConstruaFuncaoDeConsulta(null, estado, cidade, string.Empty, ehCliente, ehFornecedor, ehFuncionario, false, false, false, ehTransportadora, false, tipoCliente);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoDeConsultaGenerica);

            if (vendedor != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pessoa => _atendimentoDaConsulta.Vendedor.Id == vendedor.Id);
            }

            if (atendente != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pessoa => _atendimentoDaConsulta.Atendente.Id == atendente.Id);
            }

            if (indicador != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pessoa => _atendimentoDaConsulta.Indicador.Id == indicador.Id);
            }

            if (!string.IsNullOrWhiteSpace(mesAnoAniversario))
            {
                var dataInicial = ("01/" + mesAnoAniversario).ToDate();

                var MesPosterior = dataInicial.AddMonths(1);

                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pessoa => _dadosPessoaisDaConsulta.DataDeNascimento >= dataInicial && _dadosPessoaisDaConsulta.DataDeNascimento < MesPosterior);
            }

            return ConsultePessoas(expressaoParaConsulta, ordenacaoPesquisa: ordenacaoPesquisa, joinComAtendimento: true, joinComEmpresaPessoa: true, joinComDadosPessoais: true);
        }

        public bool ExistePessoasComMesmoCpfOuCnpj(string cpfCnpj, int idPessoaDesconsiderar)
        {
            return _session.QueryOver<Pessoa>().Where(pessoa => pessoa.DadosGerais.CpfCnpj == cpfCnpj && pessoa.Id != idPessoaDesconsiderar).RowCount() > 0;
        }

        public List<Pessoa> ConsulteListaPessoaPeloCnpjOuCpf(string cpfCnpj)
        {
            return _session.QueryOver<Pessoa>().Where(pessoa => pessoa.DadosGerais.CpfCnpj == cpfCnpj).List().ToList();
        }
        
        public List<Pessoa> ConsulteListaPessoasId(List<int> listaIdPessoas)
        {
            EmpresaPessoa empresaPessoa = null;
            Atendimento atendimentoPessoa = null;
            EnderecoPessoa enderecoPessoa = null;
            Cidade cidade = null;
            Estado estado = null;

            return _session.QueryOver<Pessoa>()
                .Where(pessoa => pessoa.Id.IsIn(listaIdPessoas))
                .Left.JoinAlias(pessoa => pessoa.EmpresaPessoa, () => empresaPessoa)
                .Left.JoinAlias(pessoa => pessoa.Atendimento, () => atendimentoPessoa)
                .Left.JoinAlias(pessoa => pessoa.ListaDeEnderecos, () => enderecoPessoa)
                .Left.JoinAlias(pessoa => enderecoPessoa.Cidade, () => cidade)
                .Left.JoinAlias(pessoa => cidade.Estado, () => estado)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List().ToList();
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
            VWEnderecosPessoas vwEnderecosPessoas = null;

            Expression<Func<VWClienteSemComprar, bool>> expressaoParaConsulta = cliente => cliente.Id > 0;

            if (atendenteId != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cliente => cliente.AtendenteId == atendenteId.Value);
            }

            if (vendedorId != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cliente => cliente.VendedorId == vendedorId.Value);
            }

            if (!string.IsNullOrEmpty(uf))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cliente => vwEnderecosPessoas.EstadoUF == uf);
            }

            if (cidadeId != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cliente => vwEnderecosPessoas.CidadeId == cidadeId);
            }

            if (!string.IsNullOrEmpty(bairro))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cliente => vwEnderecosPessoas.Bairro.IsLike("%" + bairro + "%"));
            }

            Expression<Func<VWClienteSemComprar, bool>> expressaoParaConsultaDiasSemComprar = cliente => cliente.Id > 0;

            if (diasInicialSemComprar != null)
            {
                expressaoParaConsultaDiasSemComprar = expressaoParaConsultaDiasSemComprar.AndAlso(cliente => cliente.DiasSemComprar >= diasInicialSemComprar.Value);
            }

            if (diasFimSemComprar != null)
            {
                expressaoParaConsultaDiasSemComprar = expressaoParaConsultaDiasSemComprar.AndAlso(cliente => cliente.DiasSemComprar <= diasFimSemComprar.Value);
            }

            if (somenteClientesQueJahCompraram)
            {
                expressaoParaConsultaDiasSemComprar = expressaoParaConsultaDiasSemComprar.AndAlso(cliente => cliente.JahComprou == true);
            }
            else
            {
                expressaoParaConsultaDiasSemComprar = expressaoParaConsultaDiasSemComprar.OrElse(cliente => cliente.JahComprou == false);
            }

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoParaConsultaDiasSemComprar);

            return _session.QueryOver<VWClienteSemComprar>().Where(expressaoParaConsulta)
                                    .Left.JoinAlias(cliente => cliente.ListaDeEnderecos, () => vwEnderecosPessoas)
                                    .TransformUsing(Transformers.DistinctRootEntity).List().ToList();
        }

        public List<Telefone> ConsulteListaDeTelefones(string telefone)
        {
            return _session.QueryOver<Telefone>().Where(fone => fone.Numero.IsLike( "%" + telefone + "%")).List().ToList();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private Expression<Func<Pessoa, bool>> ConstruaFuncaoDeConsulta(EnumTipoEndereco? tipoEndereco,
                                                                                                            Estado estado,
                                                                                                            Cidade cidade,
                                                                                                            string status,
                                                                                                            bool pesquisePorCliente,
                                                                                                            bool pesquisePorFornecedor,
                                                                                                            bool pesquisePorFuncionario,
                                                                                                            bool pesquisePorVendedor,
                                                                                                            bool pesquisePorSupervisor,
                                                                                                            bool pesquisePorAtendente,
                                                                                                            bool pesquisePorTransportadora,
                                                                                                            bool pesquisePorIndicador,
                                                                                                            EnumTipoCliente tipoCliente = EnumTipoCliente.TODOS)
        {
            Expression<Func<Pessoa, bool>> expressaoParaConsulta = pessoa => pessoa.Id != 0;

            if (tipoEndereco != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pessoa => _enderecoPessoaDaConsulta.TipoEndereco == tipoEndereco);
            }

            if (estado != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pessoa => _cidadeDaConsulta.Estado != null && _cidadeDaConsulta.Estado.UF == estado.UF);
            }

            if (cidade != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pessoa => _cidadeDaConsulta.Id == cidade.Id);
            }

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(p => p.DadosGerais.Status == status);
            }

            if (pesquisePorCliente || pesquisePorFornecedor || pesquisePorFuncionario || pesquisePorTransportadora || pesquisePorVendedor || pesquisePorSupervisor || pesquisePorAtendente || pesquisePorIndicador)
            {
                Expression<Func<Pessoa, bool>> expressaoParaConsultaDeTipoDeCadastro = null;

                if (pesquisePorCliente)
                {
                    if (tipoCliente == EnumTipoCliente.CONSUMIDOR || tipoCliente == EnumTipoCliente.REVENDA)
                    {
                        expressaoParaConsultaDeTipoDeCadastro = expressaoParaConsultaDeTipoDeCadastro.OrElse(p => p.DadosGerais.EhCliente);

                        expressaoParaConsultaDeTipoDeCadastro = expressaoParaConsultaDeTipoDeCadastro.AndAlso(p => p.DadosGerais.TipoCliente == tipoCliente);
                    }
                    else
                        expressaoParaConsultaDeTipoDeCadastro = expressaoParaConsultaDeTipoDeCadastro.OrElse(p => p.DadosGerais.EhCliente);
                }

                if (pesquisePorFornecedor)
                {
                    expressaoParaConsultaDeTipoDeCadastro = expressaoParaConsultaDeTipoDeCadastro.OrElse(p => p.DadosGerais.EhFornecedor);
                }

                if (pesquisePorFuncionario)
                {
                    expressaoParaConsultaDeTipoDeCadastro = expressaoParaConsultaDeTipoDeCadastro.OrElse(p => p.DadosGerais.EhFuncionario);
                }

                if (pesquisePorTransportadora)
                {
                    expressaoParaConsultaDeTipoDeCadastro = expressaoParaConsultaDeTipoDeCadastro.OrElse(p => p.DadosGerais.EhTransportadora);
                }

                if (pesquisePorVendedor)
                {
                    expressaoParaConsultaDeTipoDeCadastro = expressaoParaConsultaDeTipoDeCadastro.OrElse(p => p.Vendedor != null && p.Vendedor.EhVendedor);
                }

                if (pesquisePorSupervisor)
                {
                    expressaoParaConsultaDeTipoDeCadastro = expressaoParaConsultaDeTipoDeCadastro.OrElse(p => p.Vendedor != null && p.Vendedor.EhSupervisor);
                }

                if (pesquisePorAtendente)
                {
                    expressaoParaConsultaDeTipoDeCadastro = expressaoParaConsultaDeTipoDeCadastro.OrElse(p => p.Vendedor != null && p.Vendedor.EhAtendente);
                }

                if (pesquisePorIndicador)
                {
                    expressaoParaConsultaDeTipoDeCadastro = expressaoParaConsultaDeTipoDeCadastro.OrElse(p => p.Vendedor != null && p.Vendedor.EhIndicador);
                }

                expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoParaConsultaDeTipoDeCadastro);
            }

            return expressaoParaConsulta;
        }

        private List<Pessoa> ConsultePessoas(Expression<Func<Pessoa, bool>> expressaoParaConsulta,
                                                               EnumOrdenacaoPesquisaPessoa ordenacaoPesquisa = EnumOrdenacaoPesquisaPessoa.CODIGO,
                                                               bool joinComAtendimento = false,
                                                               bool joinComEmpresaPessoa = false,
                                                               bool joinComDadosPessoais = false)
        {
            var selectConsulta = RetorneQueryOverComJoins(ordenacaoPesquisa, joinComAtendimento, joinComEmpresaPessoa, joinComDadosPessoais);

            selectConsulta.TransformUsing(Transformers.DistinctRootEntity).Where(expressaoParaConsulta);

            OrdeneConsulta(selectConsulta, ordenacaoPesquisa);

            return selectConsulta.List<Pessoa>().ToList();
        }

        private IQueryOver<Pessoa, Pessoa> RetorneQueryOverComJoins(EnumOrdenacaoPesquisaPessoa ordenacaoPesquisa, bool joinComAtendimento, bool joinComEmpresaPessoa, bool joinComDadosPessoais)
        {
            var selectConsulta = _session.QueryOver<Pessoa>()
                                                .Left.JoinAlias(pessoa => pessoa.ListaDeEnderecos, () => _enderecoPessoaDaConsulta)
                                                .Left.JoinAlias(pessoa => _enderecoPessoaDaConsulta.Cidade, () => _cidadeDaConsulta);

            if (joinComAtendimento || ordenacaoPesquisa == EnumOrdenacaoPesquisaPessoa.RAZAOSOCIALVENDEDOR)
            {
                selectConsulta.Left.JoinAlias(pessoa => pessoa.Atendimento, () => _atendimentoDaConsulta);

                if (ordenacaoPesquisa == EnumOrdenacaoPesquisaPessoa.RAZAOSOCIALVENDEDOR)
                {
                    selectConsulta.Left.JoinAlias(pessoa => _atendimentoDaConsulta.Vendedor, () => _vendedorDaConsulta);
                }
            }

            if (joinComEmpresaPessoa)
            {
                selectConsulta.Left.JoinAlias(pessoa => pessoa.EmpresaPessoa, () => _empresaPessoaDaConsulta);
            }

            if (joinComDadosPessoais)
            {
                selectConsulta.Left.JoinAlias(pessoa => pessoa.DadosPessoais, () => _dadosPessoaisDaConsulta);
            }

            return selectConsulta;
        }

        private void OrdeneConsulta(IQueryOver<Pessoa, Pessoa> selectConsulta, EnumOrdenacaoPesquisaPessoa ordenacaoPesquisa)
        {
            switch (ordenacaoPesquisa)
            {
                case EnumOrdenacaoPesquisaPessoa.CODIGO:
                    selectConsulta.OrderBy(pessoa => pessoa.Id).Asc();

                    break;
                case EnumOrdenacaoPesquisaPessoa.RAZAOSOCIAL:
                    selectConsulta.OrderBy(pessoa => pessoa.DadosGerais.Razao).Asc();

                    break;
                case EnumOrdenacaoPesquisaPessoa.RAZAOSOCIALVENDEDOR:
                    selectConsulta.OrderBy(pessoa => _vendedorDaConsulta.DadosGerais.Razao).Asc();

                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
