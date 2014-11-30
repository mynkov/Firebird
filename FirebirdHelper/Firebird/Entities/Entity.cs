using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Firebird.Entities
{
    /// <summary>
    /// Базовый класс всех сущностей
    /// </summary>
    public abstract class Entity
    {
        public virtual int Id { get; protected set; }
    }
}
