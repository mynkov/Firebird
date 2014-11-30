using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using DAO.Firebird.Attributes;
using DAO.Firebird.Enums;

namespace DAO.Firebird.Entities
{
    /// <summary>
    /// Информация о переданных или полученных файлах
    /// </summary>
    [ClassBind("ReplicationReplicas")]
    public class Replicas : Entity
    {
        [PropertyBind("ReplicaId", FieldType.Id)]
        public override int Id { get; protected set; }

        [PropertyBind("ReplicaDate", FieldType.Data)]
        public DateTime ReplicaDate { get; private set; }

        [PropertyBind("Description", FieldType.Data)]
        public string Description { get; private set; }

        public Replicas()
        {
        }

        public Replicas(DateTime replicaDate, string description)
        {
            this.ReplicaDate = replicaDate;
            this. Description = description;
        }
    }
}
