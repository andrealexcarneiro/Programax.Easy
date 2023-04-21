using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.Repositorio;
using Programax.Easy.Negocio.Integracao.TabelasAtualizadasIntegracaoDJObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate;

namespace Programax.Easy.Servico.Integracao.TabelasAtualizadasIntegracaoDJServ
{
    public class RepositorioTabelasAtualizadasIntegracaoDJ : RepositorioBase<TabelasAtualizadasIntegracaoDJ>, IRepositorioTabelasAtualizadasIntegracaoDJ
    {
        public RepositorioTabelasAtualizadasIntegracaoDJ(ISession sessao)
            : base(sessao)
        {

        }

        public void ExportaTodosDados()
        {
            _session.CreateSQLQuery("SET SQL_SAFE_UPDATES = 0").ExecuteUpdate();
            _session.CreateSQLQuery("DELETE FROM TABELASATUALIZADASINTEGRACAODJ WHERE 1=1").ExecuteUpdate();

            _session.CreateSQLQuery(@"INSERT INTO TABELASATUALIZADASINTEGRACAODJ (TAB_TABELA,TAB_ID_REGISTRO)  
                                                         SELECT 0, PROD_ID  FROM PRODUTOS;").ExecuteUpdate();

            _session.CreateSQLQuery(@"INSERT INTO TABELASATUALIZADASINTEGRACAODJ (TAB_TABELA,TAB_ID_REGISTRO)  
                                                         SELECT 1, PES_ID  FROM PESSOAS;").ExecuteUpdate();

            _session.CreateSQLQuery(@"INSERT INTO TABELASATUALIZADASINTEGRACAODJ (TAB_TABELA,TAB_ID_REGISTRO)  
                                                         SELECT 2, FORPAG_ID  FROM PAGAMENTOFORMA").ExecuteUpdate();

            _session.CreateSQLQuery(@"INSERT INTO TABELASATUALIZADASINTEGRACAODJ (TAB_TABELA,TAB_ID_REGISTRO)  
                                                         SELECT 3, CAIXA_ID  FROM CAIXAS;").ExecuteUpdate();

            _session.CreateSQLQuery(@"INSERT INTO TABELASATUALIZADASINTEGRACAODJ (TAB_TABELA,TAB_ID_REGISTRO)  
                                                         SELECT 5, COPAG_ID  FROM PAGAMENTOCONDICAO;").ExecuteUpdate();
        }


        public void ExcluaLista(List<TabelasAtualizadasIntegracaoDJ> atualizacoes)
        {
            if (atualizacoes.Count > 0)
            {
                string exclusoes = string.Empty;

                exclusoes += "DELETE FROM TABELASATUALIZADASINTEGRACAODJ WHERE TAB_ID IN(" + string.Join(",", atualizacoes.Select<TabelasAtualizadasIntegracaoDJ, int>(x => x.Id)) + ");";

                _session.CreateSQLQuery(exclusoes).ExecuteUpdate();
            }
        }
    }
}
