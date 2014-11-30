using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO.Firebird.Entities;
using DAO.Firebird.Attributes;
using DAO.Firebird.Utils;
using System.Reflection;
using DAO.Firebird.Enums;
using System.Data;

namespace DAO.Firebird.Utils
{
    /// <summary>
    /// Helper для маппинга
    /// </summary>
    public class MappingHelper
    {
        /// <summary>
        /// Получить имя таблицы
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <returns></returns>
        public static string GetTableName<T>()
            where T : Entity
        {     
            return typeof(T).GetCustomAttribute<ClassBindAttribute>().TableName;      
        }

        /// <summary>
        /// Получить имя PrimaryKey параметра
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <returns></returns>
        public static string GetNameIdField<T>()
            where T : Entity
        {
            foreach (PropertyInfo propertyInfo in typeof (T).GetProperties())
            {
                PropertyBindAttribute customAttribute = propertyInfo.GetCustomAttribute<PropertyBindAttribute>();

                if (customAttribute != null && customAttribute.FieldType == FieldType.Id)
                {
                    return customAttribute.FieldName;
                }
            }

            throw new Exception(string.Format("Ключевое поле не помечено атрибутом \"{0}\"", typeof(PropertyBindAttribute).FullName));
        }

        /// <summary>
        /// Получить сущность из IDataReader
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="reader">Reader</param>
        /// <returns></returns>
        public static T GetEntity<T>(IDataReader reader)
            where T : Entity, new()
        {
            T result = new T();
            
            foreach (PropertyInfo propertyInfo in typeof (T).GetProperties())
            {
                PropertyBindAttribute customAttribute = propertyInfo.GetCustomAttribute<PropertyBindAttribute>();

                if (customAttribute != null && reader[customAttribute.FieldName] != DBNull.Value)
                {
                    propertyInfo.SetValue(result, reader[customAttribute.FieldName], null);
                }
            }

            return result;
        }

        /// <summary>
        /// Получить имена параметров
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns></returns>
        public static List<string> GetNameFields<T>(bool withId)
            where T : Entity
        {
            List<string> result = new List<string>();

            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                PropertyBindAttribute customAttribute = propertyInfo.GetCustomAttribute<PropertyBindAttribute>();

                if (customAttribute != null && (withId || !withId && customAttribute.FieldType == FieldType.Data))
                {
                    result.Add(customAttribute.FieldName);
                }
            }

            return result;
        }

        /// <summary>
        /// Получить имена параметров и их значения по сущности
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetNameAndValueFields(Entity entity)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {
                PropertyBindAttribute customAttribute = propertyInfo.GetCustomAttribute<PropertyBindAttribute>();

                if (customAttribute != null)
                {
                    object value = propertyInfo.GetValue(entity, null) ?? string.Empty;
                    result.Add(customAttribute.FieldName, value.ToString());
                }
            }

            return result;
        }
    }
}
