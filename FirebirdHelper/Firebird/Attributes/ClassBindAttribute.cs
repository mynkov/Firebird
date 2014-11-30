using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Firebird.Attributes
{
    /// <summary>
    /// Атрибут для маппинга класса
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassBindAttribute : Attribute
    {
        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName { get; set; }

        public ClassBindAttribute(string tableName)
        {
            this.TableName = tableName;
        }
    }
}
