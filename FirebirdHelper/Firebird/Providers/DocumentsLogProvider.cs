using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO.Firebird.Entities;
using FirebirdSql.Data.FirebirdClient;
using DAO.Firebird.Utils;
using System.Data.Common;
using System.Data;

namespace DAO.Firebird.Providers
{
    /// <summary>
    /// Провайдер логов документа
    /// </summary>
    public class DocumentsLogProvider : Provider<DocumentsLog>
    {
        public static DataTable GetList()
        {
            DataTable table = new DataTable();


            using (Database db = Database.CreateFirebirdDatabase())
            {
                DbCommand command = new FbCommand("SELECT TransactionLogId, ReplicaId, DepartmentId, XHDoc, FKXHDoc, Status  FROM ReplicationDocumentsLog");
                IDataReader reader = db.ExecuteReader(command);
                table.Load(reader);
            }
            return table;
        }

        public static int Count
        {
            get
            {
                int result = 0;

                using (Database db = Database.CreateFirebirdDatabase())
                {
                    DbCommand command = new FbCommand("SELECT COUNT(*) FROM ReplicationDocumentsLog");
                    result = Convert.ToInt32(db.ExecuteScalar(command));
                }
                return result;
            }
        }
    }
}
