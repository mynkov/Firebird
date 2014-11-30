using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO.Firebird.Attributes;
using DAO.Firebird.Enums;

namespace DAO.Firebird.Entities
{
    /// <summary>
    /// Логи отправленного блока
    /// </summary>
    [ClassBind("ReplicationSendBlockLog")]
    public class SendBlockLog : Entity
    {
        [PropertyBind("ReplicationSendBlockLogId", FieldType.Id)]
        public override int Id { get; protected set; }

        [PropertyBind("OutDeptId", FieldType.Data)]
        public int OutDeptId { get; private set; }

        [PropertyBind("ReplicaId", FieldType.Data)]
        public int ReplicaId { get; private set; }

        [PropertyBind("CountSendPart", FieldType.Data)]
        public int CountSendPart { get; private set; }

        [PropertyBind("FileName", FieldType.Data)]
        public string FileName { get; private set; }

        [PropertyBind("TaskGuid", FieldType.Data)]
        public string TaskGuidString { get; private set; }

        [PropertyBind("DateCreated", FieldType.Data)]
        public DateTime DateCreated { get; private set; }

        public Guid TaskGuid
        {
            get { return new Guid(TaskGuidString); }
        }

        public SendBlockLog()
        {
        }

        public SendBlockLog(int outDeptId, int replicaId, string fileName, Guid taskGuid)
        {
            this.OutDeptId = outDeptId;
            this.ReplicaId = replicaId;
            this.FileName = fileName;
            this.TaskGuidString = taskGuid.ToString();
            CountSendPart = 0;
            DateCreated = DateTime.Now;
        }

        public void ChangeCountSendPart(int countSendPart)
        {
            CountSendPart = countSendPart;
        }
    }
}
