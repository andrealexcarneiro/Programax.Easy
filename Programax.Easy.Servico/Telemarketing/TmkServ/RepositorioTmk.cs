using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate.Transform;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.TeleMarketing.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Telemarketing.TmkServ
{
    public class RepositorioTmk : RepositorioBase<Tmk>, IRepositorioTmk
    {
        public RepositorioTmk(ISession sessao)
            : base(sessao)
        {
        }

        public List<Tmk> ConsulteLista(int idTmk)
        {
            throw new NotImplementedException();
        }
               
        public List<Tmk> ConsulteListaParaTMK(Pessoa pessoa,
                                                                           EnumStatusAtendimento? statusTMK,
                                                                           DateTime? dataInicialPeriodo,
                                                                           DateTime? dataFinalPeriodo,
                                                                           int marcaId, int Carteira)
        {

            string sqlWhere = "pedidosvendas.PEDIDO_CARTEIRA =  " + Carteira + " And pedido_status <> 3";
            string innerJoin = " ";
            int intstatus = 0;
            intstatus = statusTMK.GetHashCode();
            
            if (intstatus == 1)
            {
                intstatus += 1;
            }
            if (statusTMK.ToString() == "CONCLUIDO")
            {
                intstatus = 1  ;
            }

            if (pessoa != null)
            {
                sqlWhere = sqlWhere + " AND pedidosvendas.pedido_cliente_id = " + "'" + pessoa.Id + "'";
            }

            if (statusTMK != null)
            {
                if (statusTMK ==0)
                {
                    sqlWhere = sqlWhere + " AND historicosatendimento.hisat_status is null OR historicosatendimento.hisat_status = " + "'" + statusTMK.GetHashCode() + "'";
                }
                else
                {
                    sqlWhere = sqlWhere + " AND historicosatendimento.hisat_status = " + "'" + intstatus   + "'";
                }
                   
            }
            else
            {
                //sqlWhere = sqlWhere + " AND pedidosvendas.pedido_status_atendimento != " + "'" + 4 + "'";
                //sqlWhere = sqlWhere + " AND pedidosvendas.pedido_status_atendimento != " + "'" + 3 + "'";
            }

            if (dataInicialPeriodo != null)
            {
                sqlWhere = sqlWhere + " AND pedidosvendas.pedido_data_elaboracao Between " + "'" + dataInicialPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 00:00:00") + "' And '" + dataFinalPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 23:59:00") + "'"; 
            }

            
            //innerJoin = innerJoin + " inner join roteirizacao on roteiros.ROT_ROTEIRIZACAO_ID = roteirizacao.roteiro_id ";
            //innerJoin = innerJoin + " inner join pedidosvendas on roteiros.rot_pedido_id = pedidosvendas.pedido_id ";
            innerJoin = innerJoin + " left join historicosatendimento on pedidosvendas.pedido_id  = historicosatendimento.hisat_pedido_id ";
            innerJoin = innerJoin + " inner join pessoas on pedidosvendas.pedido_cliente_id = pessoas.pes_id ";
            innerJoin = innerJoin + " left join agendamentocontato on pedidosvendas.pedido_id = agendamentocontato.pedido ";
            if (marcaId != 0)
            {
                innerJoin = innerJoin + " inner join pedidosvendasitens on pedidosvendasitens.peditem_pedido_id = pedidosvendas.pedido_id ";
                innerJoin = innerJoin + " inner join produtos on pedidosvendasitens.peditem_produto_id = produtos.prod_id ";
                
                sqlWhere = sqlWhere + " AND produtos.prod_marc_id = " + "'" + marcaId + "'";

            }                        

            var sql = " select pedidosvendas.pedido_data_elaboracao as DataCompra, " +
                        " pedidosvendas.pedido_id as NumPedido, pedidosvendas.pedido_cliente_id as ClienteId, " +
                        " historicosatendimento.hisat_status as status, historicosatendimento.hisat_novo_pedido_id as NumPedidoNovo," +
                        " pessoas.pes_razao as DescricaoCliente, agendamentocontato.Data as Agendamento " +

                " FROM  pedidosvendas " +

                innerJoin +

                " WHERE " + sqlWhere + " order by  NumPedido,  hisat_contador DESC";

            var query = _session.CreateSQLQuery(sql);

            query.SetResultTransformer(Transformers.AliasToBean(typeof(Tmk)));

            return query.List<Tmk>().ToList();           

        }

        public List<GerenciarTmk> ConsulteListaParaGerenciarTMK(Pessoa pessoa,
                                                                          EnumStatusAtendimento? statusTMK,
                                                                          DateTime? dataInicialPeriodo,
                                                                          DateTime? dataFinalPeriodo)
        {

            string sqlWhere = "hisat_id > " + "'" + 0 + "'";
            string innerJoin = " ";

            if (pessoa != null)
            {
                sqlWhere = sqlWhere + " AND historicosatendimento.hisat_pes_usuario_id = " + "'" + pessoa.Id + "'";
            }

            if (statusTMK != null)
            {
                sqlWhere = sqlWhere + " AND historicosatendimento.hisat_status = " + "'" + statusTMK.GetHashCode() + "'";
            }            

            if (dataInicialPeriodo != null)
            {
                sqlWhere = sqlWhere + " AND historicosatendimento.hisat_data_historico >= " + "'" + dataInicialPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 00:00:00") + "'";
            }

            if (dataFinalPeriodo != null)
            {
                sqlWhere = sqlWhere + " AND historicosatendimento.hisat_data_historico <= " + "'" + dataFinalPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 23:59:00") + "'";
            }

            innerJoin = innerJoin + " left join pedidosvendas on historicosatendimento.hisat_novo_pedido_id = pedidosvendas.pedido_id ";

            var sql = " select historicosatendimento.hisat_status as Status, " +
                        "historicosatendimento.hisat_id as IdAtendimento, " +
                        " historicosatendimento.hisat_pes_usuario_id as VendedorId, " +
                        "historicosatendimento.hisat_data_historico as Data, " +
                        "historicosatendimento.hisat_tempo_duracao as Duracao, " +
                        "pedidosvendas.pedido_valor_total as ValorVenda " +

                " FROM  historicosatendimento " +

                innerJoin +

                " WHERE " + sqlWhere +
                "ORDER BY VendedorId ASC ";
                

            var query = _session.CreateSQLQuery(sql);

            query.SetResultTransformer(Transformers.AliasToBean(typeof(GerenciarTmk)));

            return query.List<GerenciarTmk>().ToList();

        }

    }
}
