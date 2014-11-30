using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;

namespace DAO.Firebird.Utils
{
    /// <summary>
    /// Слой управления соединениями
    /// </summary>
    public class Database : IDisposable
    {
        private string _connectionString;

        private DbConnection _connection;

        private IDataReader _dataReader;

        private Database(string connectionString)
        {
            this._connectionString = connectionString;
        }

        /// <summary>
        ///  Создать Firebird базу
        /// </summary>
        /// <returns></returns>
        public static Database CreateFirebirdDatabase()
        {
            return new Database(ConfigurationManager.ConnectionStrings["FirebirdConnectionString"].ConnectionString);
        }

        public void ExecuteNonQuery(DbCommand command)
        {
            using (_connection = new FbConnection(_connectionString))
            {
                command.Connection = _connection;
                _connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(DbCommand command)
        {
            using (_connection = new FbConnection(_connectionString))
            {
                command.Connection = _connection;
                _connection.Open();
                return command.ExecuteScalar();
            }
        }

        public IDataReader ExecuteReader(DbCommand command)
        {
            _connection = new FbConnection(_connectionString);
            command.Connection = _connection;
            _connection.Open();
            _dataReader = command.ExecuteReader();
            return _dataReader;
        }

        /// <summary>
        /// Добавить входной параметр
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddInParameter(DbCommand command, string name, string value)
        {
            command.Parameters.Add(new FbParameter(name, value));
        }

        /// <summary>
        /// Получить команду
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DbCommand GetCommand(string commandText)
        {
            return new FbCommand(commandText);
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            if (_dataReader != null)
            {
                _dataReader.Close();
            }
        }
    }
}

