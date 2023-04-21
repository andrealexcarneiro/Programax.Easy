using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace WorkBr.Financeiro.WorkBrFinanc.Negocio
{
    public class NeLoginNovo : WorkBr.Core.Persistencia.PsDataAccess
    {

                #region --> Construtor da classe

        private string ConnectionStringBase = "";

        /// <summary>
        /// Permite criar uma instância do objeto com a passagem de parametros
        /// </summary>
        /// <param name="pConnectString">Informe a string de conexão com a base de dados</param>
        public NeLoginNovo(string pConnectString)
        {
            
            if (pConnectString != null) _ConnectString = pConnectString;

            ConnectionStringBase = _ConnectString;
        }

        private Component components = new Component();

        #endregion

        #region -- > Destrutor da classe
        ~NeLoginNovo() { Dispose(); }


        #endregion

        #region -- > Propriedades de conexão e de campos de tabela
        private string _ConnectString { get; set; }
        #endregion --------------------------------------------------

        public NeLoginNovo() { Limpar(); }
        public int FuncionarioId { get; set; }
        public string Matricula { get; set; }
        public string Nascimento { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Cpf { get; set; }
        public bool  Desativada { get; set; }
        public bool Logon { get; set; }
        public int PerfilId { get; set; }
        public string Lotado { get; set; }
        public byte[] Foto { get; set; }

        /// <summary>
        /// Informe o se é administrador.
        /// </summary>
        public bool Administrador { get; set; }
        /// <summary>
        /// Informe o se vai criar usuarios.
        /// </summary>
        public bool Criarusuarios { get; set; }
        /// <summary>
        /// Informe o se vai criar login.
        /// </summary>
        public bool CriarLogin { get; set; }
        /// <summary>
        /// Informe o se vai fazer o logoff. 
        /// </summary>
        public bool Logoff { get; set; }


        public void Limpar()
        {
            try
            {
                FuncionarioId = 0;
                PerfilId = 0;
                Matricula = "";
                Nascimento = "";
                Nome = "";
                Cargo = "";
                Login = "";
                Password = "";
                Cpf = "";
                Desativada = false;
                Logon = false;
                Lotado = "";
                Foto = null ;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public void Pesquisar()
        {
            try
            {
                StringBuilder _Builder = new StringBuilder();
                _Builder.Append(" select  perf_id, perf_nome, perf_desativada, per_administrador, perf_criarusuarios, ");
                _Builder.Append(" perf_criarlogin, perf_logoff ");
                _Builder.Append("  from usuarios ");
                //_Builder.Append(String.Format(" where perf_id='{0}'", Id));

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
                            Id = pDr["perf_id"] != DBNull.Value ? Convert.ToInt32(pDr["perf_id"].ToString()) : 0;
                            Nome = pDr["perf_nome"] != DBNull.Value ? pDr["perf_nome"].ToString() : "";
                            Desativada = pDr["perf_desativada"] != DBNull.Value ? Convert.ToBoolean(pDr["perf_desativada"].ToString()) : false;
                            Administrador = pDr["perf_administrador"] != DBNull.Value ? Convert.ToBoolean(pDr["perf_administrador"].ToString()) : false;
                            Criarusuarios = pDr["perf_criarusuarios"] != DBNull.Value ? Convert.ToBoolean(pDr["perf_criarusuarios"].ToString()) : false;
                            CriarLogin = pDr["perf_criarlogin"] != DBNull.Value ? Convert.ToBoolean(pDr["perf_criarlogin"].ToString()) : false;
                            Logoff = pDr["perf_logoff"] != DBNull.Value ? Convert.ToBoolean(pDr["perf_logoff"].ToString()) : false;
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
                SqlParameter[] _Parameters = new SqlParameter[7];
                _Parameters[0] = Parameter("perf_id", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, Id);
                _Parameters[1] = Parameter("perf_nome", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, Nome);
                _Parameters[2] = Parameter("perf_desativada", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, Desativada);
                _Parameters[3] = Parameter("perf_administrador", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, Administrador);
                _Parameters[4] = Parameter("perf_criarlusuarios", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, Criarusuarios);
                _Parameters[5] = Parameter("perf_criarlogin", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CriarLogin);
                _Parameters[6] = Parameter("perf_logoff", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, Logoff);

                // -- > Execute a storedprocedure que encontra-se no banco de dados afim de gravar ou editar o registro
                _Result = StoredProcedure("proc_gravar_loginnovo", _Parameters);

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
                _Parameters[0] = Parameter("@perf_id", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, Id);

                // --> Tente apagar o registro, executado a storedprocedure
                _Result = StoredProcedure("proc_apagar_loginnovo", _Parameters);

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


        public DataTable Listausuarios(string pComplemento)
        {
            DataTable _Result = null;
            try
            {
                StringBuilder _Builder = new StringBuilder();
                _Builder.Append(" select  perf_id, perf_nome, perf_desativada, per_administrador, perf_criarusuarios, ");
                _Builder.Append(" perf_criarlogin, perf_logoff ");
                _Builder.Append("  from usuarios ");

                if (!string.IsNullOrEmpty(pComplemento))
                    _Builder.Append(" where " + pComplemento);
                _Builder.Append(" order by perf_id");

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

    }
}
