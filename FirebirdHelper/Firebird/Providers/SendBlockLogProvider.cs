using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using DAO.Firebird.Entities;
using System.Data;
using System.Data.Common;
using DAO.Firebird.Utils;

namespace DAO.Firebird.Providers
{
    /// <summary>
    /// Провайдер логов отправленных блоков
    /// </summary>
    public class SendBlockLogProvider : Provider<SendBlockLog>
    {
        public static DataTable GetList()
        {
            DataTable table = new DataTable();

            using (Database db = Database.CreateFirebirdDatabase())
            {
                DbCommand command = new FbCommand("SELECT ReplicationSendBlockLogId, OutDeptId, ReplicaId, CountSendPart, FileName, TaskGuid, DateCreated FROM ReplicationSendBlockLog");
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
                    DbCommand command = new FbCommand("SELECT COUNT(*) FROM ReplicationSendBlockLog");
                    result = Convert.ToInt32(db.ExecuteScalar(command));
                }
                return result;
            }
        }
    }
}
