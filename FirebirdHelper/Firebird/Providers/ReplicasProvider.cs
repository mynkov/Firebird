using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using DAO.Firebird.Entities;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using DAO.Firebird.Utils;

namespace DAO.Firebird.Providers
{
    /// <summary>
    /// Провайдер Replicas
    /// </summary>
    public class ReplicasProvider : Provider<Replicas>
    {
        public static DataTable GetList()
        {
            DataTable table = new DataTable();

            using (Database db = Database.CreateFirebirdDatabase())
            {
                DbCommand command = new FbCommand("SELECT ReplicaId, ReplicaDate, Description FROM ReplicationReplicas");
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
                    DbCommand command = new FbCommand("SELECT COUNT(*) FROM ReplicationReplicas");
                    result = Convert.ToInt32(db.ExecuteScalar(command)); 
                }
                return result;
            }
        }
    }
}
