using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO.Firebird.Attributes;
using DAO.Firebird.Enums;

namespace DAO.Firebird.Entities
{
    /// <summary>
    /// Логи документа
    /// </summary>
    [ClassBind("ReplicationDocumentsLog")]
    public class DocumentsLog : Entity
    {
        [PropertyBind("TransactionLogId", FieldType.Id)]
        public override int Id { get; protected set; }

        [PropertyBind("ReplicaId", FieldType.Data)]
        public int ReplicaId { get; private set; }

        [PropertyBind("DepartmentId", FieldType.Data)]
        public int DepartamentId { get; private set; }

        [PropertyBind("XHDoc", FieldType.Data)]
        public string XHDoc { get; private set; }

        [PropertyBind("FKXHDoc", FieldType.Data)]
        public string FKXHDoc { get; private set; }

        [PropertyBind("Status", FieldType.Data)]
        public int StatusDoc { get; private set; }

        public Status Status
        {
            get { return (Status) StatusDoc; }
        }

        [PropertyBind("FKReplicaId", FieldType.Data)]
        public int FKReplicaId { get; private set; }

        public DocumentsLog()
        {
        }

        public DocumentsLog(int replicaId, int departamentId, string XHDoc, string FKXHDoc, Status status, int FKReplicaId)
        {
            this.ReplicaId = replicaId;
            this.DepartamentId = departamentId;
            this.XHDoc = XHDoc;
            this.FKXHDoc = FKXHDoc;
            this.StatusDoc = (int)status;
            this.FKReplicaId = FKReplicaId;
        }

        public void ChangeStatus(Status newStatus)
        {
            StatusDoc = (int)newStatus;
        }
    }
}
