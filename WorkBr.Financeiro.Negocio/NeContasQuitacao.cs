using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace Programax.Easy.Negocio
{
    public class NeContasQuitacao : Programax.Easy.Core.Persistencia.PsDataAccess
    {
        #region --> Construtor da classe
       
        /// <summary>
        /// Permite criar uma instância do objeto com a passagem de parametros
        /// </summary>
        /// <param name="pConnectString">Informe a string de conexão com a base de dados</param>
        public NeContasQuitacao(string pConnectString)
        {
            _ConnectString = "";
            if (pConnectString != null) _ConnectString = pConnectString;

            ConnectionStringBase = _ConnectString;
        }
        private Component components = new Component();

        #endregion

        #region -- > Destrutor da classe
        ~NeContasQuitacao() { Dispose(); }
      
        #endregion

        #region -- > Propriedades de conexão e de campos de tabela
        private string _ConnectString { get; set; }
        #endregion --------------------------------------------------

        #region --> Propriedades para valores de campo de base dados.
        /// <summary>
        /// Informar o código de identificação da quitação da parcela do contas (pagar ou receber), 
        /// este campo na tabela do banco de dados é autonumeração
        /// </summary>
        public int  Id { get; set; }
        /// <summary>
        /// Informe o codigo do pedido/documento da conta/parcela.
        /// </summary>
        public Int32 PedidoId { get; set; }
        /// <summary>
        /// Informe o numero da parcela.
        /// </summary>
        public int Parcela { get; set; }
        /// <summary>
        /// Informe a data da quitação da conta/parcela.
        /// </summary>
        public string DataPagto { get; set; }
        /// <summary>
        /// Informe a data realização do movimento da conta/parcela.
        /// </summary>
        public string DataMovPagto { get; set; }
        /// <summary>
        /// Informe o valor da conta/parcela.
        /// </summary>
        public decimal Valor { get; set; }
        /// <summary>
        /// Informe o valor dos juros da conta/parcela.
        /// </summary>
        public decimal Juros { get; set; }
        /// <summary>
        /// Informe o valor do desconto da conta/parcela.
        /// </summary>
        public decimal Desconto { get; set; }
        /// <summary>
        /// Informe o valor de quitacao da conta/parcela.
        /// </summary>
        public decimal ValorPagto { get; set; }
        /// <summary>
        /// Informe o valor da quitacao dos juros da conta/parcela.
        /// </summary>
        public decimal JurosPagto { get; set; }
        /// <summary>
        /// Informe o valor da quitacao dos descontos da conta/parcela.
        /// </summary>
        public decimal DescontoPagto { get; set; }
        /// <summary>
        /// Informe o status da conta/parcela (aberto, cancelado, excluido, quitado).
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Informe o codigo do funcionario que fez a quitação.
        /// </summary>
        public Int32 FuncionarioId { get; set; }
        /// <summary>
        /// Informe o nome do funcionario que fez a quitação da parcela.
        /// </summary>
        public string FuncionarioNome { get; set; }
        /// <summary>
        /// Informe o codigo pessoa se (fisica ou juridica).
        /// </summary>
        public Int32 PessoaId { get; set; }
        /// <summary>
        /// Informe a data de emissao da conta/parcela.
        /// </summary>
        public string DataEmissao { get; set; }
        /// <summary>
        /// Informe a data de vencimento da conta/parcela.
        /// </summary>
        public string DataVencimento { get; set; }
        /// <summary>
        /// Informe a data de prorrogação do vencimento.
        /// </summary>
        public string DataProrrogacao { get; set; }

        /// <summary>
        /// Ao pesquisar o registro está propriedade será true- para editando.
        /// </summary>
        public bool Editando { get; set; }
        #endregion --------------------------------------------------

        public void Pesquisar()
        {
            try
            {
                StringBuilder _Builder = new StringBuilder();
                _Builder.Append(" select  conta_id,  conta_pedi_id, conta_parcela, conta_data_pagto, conta_datamov_pagto, ");
                _Builder.Append(" convert(decimal(10,2),conta_valor) as conta_valor, conta_juros, conta_desconto, ");
                _Builder.Append(" conta_valor_pagto, conta_juros_pagto, conta_desconto_pagto, conta_status, conta_func_id, func_nome, pes_razao ");
                _Builder.Append(" from contas ");
                _Builder.Append(" LEFT OUTER JOIN Pessoas ON conta_pes_id = pes_id ");
                _Builder.Append(" LEFT OUTER JOIN funcionarios ON conta_func_id = func_id ");
                _Builder.Append(String.Format(" where conta_id='{0}'", Id));

                // -- > Mova as informações encontradas para as propriedades
                Carregar(ConsulteDReader(_Builder.ToString()));

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void Carregar(SqlDataReader pDr)
        {
            try
            {
                if (pDr != null) // -- > Verifique se não é nulo
                    if (pDr.HasRows) // -- > Verifique se há linhas no resultado final
                        if (pDr.Read()) // -- > Avance o ponte de leitura, se retorna true é porque exite registros
                        {
                            // -- > Mova os registros para as propriedades
                            Id = pDr["conta_id"] != DBNull.Value ? Convert.ToInt32(pDr["conta_id"].ToString()) : 0;
                            PedidoId = pDr["conta_pedi_id"] != DBNull.Value ? Convert.ToInt32(pDr["conta_pedi_id"].ToString()) : 0;
                            DataPagto = pDr["conta_data_pagto"] != DBNull.Value ? pDr["conta_data_pagto"].ToString() : "";
                            DataMovPagto = pDr["conta_datamov_pagto"] != DBNull.Value ? pDr["conta_datamov_pagto"].ToString() : "";
                            Valor = pDr["conta_valor"] != DBNull.Value ? Convert.ToDecimal(pDr["conta_valor"].ToString()) : 0;
                            Juros = pDr["conta_juros"] != DBNull.Value ? Convert.ToDecimal(pDr["conta_juros"].ToString()) : 0;
                            Desconto = pDr["conta_desconto"] != DBNull.Value ? Convert.ToDecimal(pDr["conta_desconto"].ToString()) : 0;
                            ValorPagto = pDr["conta_valor_pagto"] != DBNull.Value ? Convert.ToDecimal(pDr["conta_valor_pagto"].ToString()) : 0;
                            FuncionarioId = pDr["conta_func_id"] != DBNull.Value ? Convert.ToInt32(pDr["conta_func"].ToString()) : 0;
                            FuncionarioNome = pDr["func_nome"] != DBNull.Value ? pDr["func_nome"].ToString() : "";
                            Status = pDr["conta_status"] != DBNull.Value ? pDr["conta_status"].ToString() : "";
                            DataProrrogacao = pDr["conta_data_prorrogado"] != DBNull.Value ? pDr["conta_data_prorrogado"].ToString() : "";
                            Editando = true;
                        }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (!pDr.IsClosed) pDr.Close(); // -- > Feche o datareader}
            }
        }

        /// <summary>
        /// Gravar registros na base de dados utilizando uma procedures
        /// </summary>
        /// <returns>Qualquer valor maior 0 é que o registro foi gravado com sucesso</returns>
        public int Gravar()
        {
            int _Result = 0;
            try
            {  
                // -- > Passe as informações para a coleção de parâmetros, logo após a passagem de valores a mesma será submetida a storedprocedure
                SqlParameter[] _Parameters = new SqlParameter[8];
                _Parameters[0] = Parameter("conta_id", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, Id);
                _Parameters[1] = Parameter("conta_data_pagto", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, DataPagto);
                _Parameters[2] = Parameter("conta_datamov_pagto", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, DataMovPagto);
                _Parameters[3] = Parameter("conta_valor_pagto", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, ValorPagto);
                _Parameters[4] = Parameter("conta_juros_pagto", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, JurosPagto);
                _Parameters[5] = Parameter("conta_desconto_pagto", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, DescontoPagto);
                _Parameters[6] = Parameter("conta_func_id", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, FuncionarioId);
                _Parameters[7] = Parameter("conta_status", System.Data.SqlDbType.VarChar, 1, System.Data.ParameterDirection.Input, Status);

                // -- > Execute a storedprocedure que encontra-se no banco de dados afim de gravar ou editar o registro
                _Result = StoredProcedure("proc_gravar_contasquitacao", _Parameters);

                // -- > Force o fechamento da conexão da conexão com a base dedos
                CloseConnection();
            }
            catch (Exception Ex)
            {
                // -- > Force o fechamento da conexão da conexão com a base dedos
                CloseConnection();
                throw Ex;
            }
            return _Result;// -- > Retorne a quantidade de registro foi afetado pela operação.
        }

        public int Apagar()
        {
            int _Result = 0;
            try
            {
                // -- > Passe as informações para a collection de parametros
                SqlParameter[] _Parameters = new SqlParameter[1];
                _Parameters[0] = Parameter("@conta_id", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, Id);

                // --> Tente apagar o registro, executado a storedprocedure
                _Result = StoredProcedure("proc_apagar_contasquitacao", _Parameters);

                // -- > Force o fechamento da conexão
                CloseConnection();
            }
            catch (Exception Ex)
            {
                // -- > Force o fechamento da conexão
                CloseConnection();
                throw Ex;
            }
            return _Result;
        }

        public DataTable ListaContasQuitacao(string pComplemento)
        {
            DataTable _Result = null;
            try
            {
                StringBuilder _Builder = new StringBuilder();
                _Builder.Append(" select  conta_id,  conta_pedi_id, conta_parcela,convert(nvarchar(10),conta_data_pagto,103), ");
                _Builder.Append(" conta_datamov_pagto, convert(decimal(10,2),conta_valor) as conta_valor, conta_juros, conta_desconto, ");
                _Builder.Append(" conta_valor_pagto, conta_juros_pagto, conta_desconto_pagto, conta_status, conta_func_id, func_nome ");
                _Builder.Append(" from contas ");
                _Builder.Append(" LEFT OUTER JOIN funcionarios ON conta_func_id = func_id ");

                if (!string.IsNullOrEmpty(pComplemento))
                    _Builder.Append(" where " + pComplemento);
                _Builder.Append(" order by conta_pedi_id, conta_parcela ");

                // -- > Retorne o resultado
                _Result = ConsulteDTable(_Builder.ToString());

                // -- > Force o fechamento da conexão
                CloseConnection();
            }
            catch (Exception Ex)
            {
                // -- > Force o fechamento da conexão
                CloseConnection();
                throw Ex;
            }
            return _Result;
        }

        public int GravarQuitacao(NeContasQuitacao pNeContasQuitacao)
        {
            int _Result = 0;
            try
            {
                SqlParameter[] _Parameters = new SqlParameter[8];
                _Parameters[0] = Parameter("conta_id", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, pNeContasQuitacao.Id);
                _Parameters[1] = Parameter("conta_data_pagto", SqlDbType.DateTime, 0, ParameterDirection.Input, pNeContasQuitacao.DataPagto);
                _Parameters[2] = Parameter("conta_datamov_pagto", SqlDbType.DateTime, 0, ParameterDirection.Input, pNeContasQuitacao.DataMovPagto);
                _Parameters[3] = Parameter("conta_valor_pagto", SqlDbType.Decimal, 0, ParameterDirection.Input, pNeContasQuitacao.Valor);
                _Parameters[4] = Parameter("conta_valor_juros", SqlDbType.Decimal, 0, ParameterDirection.Input, pNeContasQuitacao.Juros);
                _Parameters[5] = Parameter("conta_valor_desconto", SqlDbType.Decimal, 0, ParameterDirection.Input, pNeContasQuitacao.Desconto);
                _Parameters[6] = Parameter("conta_func_id", SqlDbType.BigInt, 0, ParameterDirection.Input, pNeContasQuitacao.FuncionarioId);
                _Parameters[7] = Parameter("conta_status", SqlDbType.VarChar, 1, ParameterDirection.Input, pNeContasQuitacao.Status);
                _Result = StoredProcedure("proc_gravar_contasquitacao", _Parameters);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return _Result;
        }

        public int ProrrogarVencimento()
        {
            int _Result = 0;
            try
            {
                SqlParameter[] _Parameters = new SqlParameter[3];
                _Parameters[0] = Parameter("conta_id", SqlDbType.BigInt, 0, ParameterDirection.Input, Id);
                _Parameters[1] = Parameter("conta_func_id", SqlDbType.BigInt, 0, ParameterDirection.Input, FuncionarioId);
                _Parameters[2] = Parameter("conta_data_prorrogado", SqlDbType.DateTime, 0, ParameterDirection.Input, DataProrrogacao);
               
                _Result = StoredProcedure("proc_gravar_contasprorrogacao", _Parameters);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return _Result;
        }

        //public DataSet LerContasHistorico(NeContasQuitacao pNeContasQuitacao)
        //{
        //    try
        //    {
        //        DataSet _Result = null;
        //        StringBuilder _Builder = new StringBuilder();

        //        _Builder.Append(" select conta_valor, isnull(conta_juros,0) as conta_juros, ");
        //        _Builder.Append(" isnull(conta_desconto,0) as conta_desconto, ");
        //        _Builder.Append(" conta_valor - isnull(conta_desconto,0) as vr_documento,  ");
        //        _Builder.Append(" conta_valor_pagto as vr_pago, ");
        //        _Builder.Append(" conta_valor - isnull(conta_desconto,0)- conta_valor as vr_apagar, ");
        //        _Builder.Append(" conta_data_pagto, conta_datamov_pagto, ");
        //        _Builder.Append(" isnull(conta_juros_pagto,0) as conta_juros_pagto, ");
        //        _Builder.Append(" isnull(conta_desconto_pagto,0) as conta_desconto_pagto ");
        //        _Builder.Append(" from contas ");
        //        _Builder.Append(" where conta_id='" + pNeContasQuitacao.Id + "' ");

        //        _Builder.Append(" select conta_id, conta_conta_id, convert(varchar(10), conta_data_pagto,103) ");
        //        _Builder.Append(" as conta_data_pagto, conta_valo_pagto, conta_juros_pagto, conta_desconto_pagto, conta_datamov_pagto, ");
        //        _Builder.Append(" func_nome from contasquitacao inner join funcionarios on conta_func_id = func_id ");
        //        _Builder.Append(" where conta_id='" + pNeContasQuitacao.Id + "' ");
        //        _Builder.Append(" order by conta_datamov_pagto desc ");

        //        _Result = ConsulteDSet(_Builder.ToString());
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //}

          public DataSet LerContasVencidas(NeContasQuitacao pNeContasQuitacao)
          {
              try
              {
                  DateTime _dtHoje = new DateTime();
                  _dtHoje = DateTime.Now;

                  StringBuilder _Builder = new StringBuilder();

                  _Builder.Append(" SELECT conta_id, conta_pedi_id,  conta_numeronf, conta_pes_id, ");
                  _Builder.Append(" conta_data_emissao, conta_data_vencimento, conta_data_prorrogado, ");
                  _Builder.Append(" convert(decimal(18,2),conta_valor) as conta_valor, conta_juros, ");
                  _Builder.Append(" conta_desconto, conta_data_pagto, conta_valor_pagto, conta_juros_pagto, conta_desconto_pagto, ");
                  _Builder.Append(" datediff(day, getdate(), convert(datetime, conta_data_vencimento)) as dias ");
                  _Builder.Append(" FROM contas ");
                  _Builder.Append(" where conta_pes_id='" + pNeContasQuitacao.Id + "' and isdate(conta_data_pagto)=0 and ");
                  _Builder.Append(" datediff(day, getdate(), convert(datetime, conta_data_vencimento)) < 0 ");
                  _Builder.Append(" order by conta_data_vencimento ");
                  return ConsulteDSet(_Builder.ToString());
              }
              catch (Exception Ex)
              {
                  throw Ex;
              }
          }




    }
}
