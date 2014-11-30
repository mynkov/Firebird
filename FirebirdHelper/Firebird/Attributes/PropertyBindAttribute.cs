using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO.Firebird.Enums;
using System.Data;

namespace DAO.Firebird.Attributes
{
    /// <summary>
    /// Атрибут для маппинга свойства
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyBindAttribute : Attribute
    {
        /// <summary>
        /// Имя поля
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Тип поля
        /// </summary>
        public FieldType FieldType { get; set; }

        public PropertyBindAttribute(string fieldName, FieldType fieldType)
        {
            this.FieldName = fieldName;
            this.FieldType = fieldType;
        }
    }
}
