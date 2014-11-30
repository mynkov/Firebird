using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Common;
using System.Data;
using DAO.Firebird.Utils;
using DAO.Firebird.Entities;

namespace DAO.Firebird.Providers
{
    /// <summary>
    /// Базовый класс для всех провайдеров
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public abstract class Provider<T>
        where T : Entity, new()
    {
        /// <summary>
        /// Получить первую сущность по фильтру
        /// </summary>
        /// <param name="expression">Выражение</param>
        /// <returns></returns>
        public static T First(Expression<Func<T, bool>> expression)
        {
            T result = new T();

            using (Database db = Database.CreateFirebirdDatabase())
            {
                DbCommand command = db.GetCommandFirst(expression);
                IDataReader reader = db.ExecuteReader(command);

                if (reader.Read())
                {
                    result = MappingHelper.GetEntity<T>(reader);
                }
                else
                {
                    throw new Exception(string.Format("Сущность типа \"{0}\" не найдена", typeof (T).Name));
                }
            }
            return result;
        }

        /// <summary>
        /// Получить первую сущность по фильтру или null
        /// </summary>
        /// <param name="expression">Выражение</param>
        /// <returns></returns>
        public static T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            T result = null;

            using (Database db = Database.CreateFirebirdDatabase())
            {
                DbCommand command = db.GetCommandFirst(expression);
                IDataReader reader = db.ExecuteReader(command);

                if (reader.Read())
                {
                    result = MappingHelper.GetEntity<T>(reader);
                }
            }
            return result;
        }

        /// <summary>
        /// Получить список всех сущностей
        /// </summary>
        /// <returns></returns>
        public static List<T> GetAll()
        {
            List<T> result = new List<T>();

            using (Database db = Database.CreateFirebirdDatabase())
            {
                DbCommand command = db.GetCommandWhere<T>();
                IDataReader reader = db.ExecuteReader(command);
               
                while (reader.Read())
                {
                    T entity = MappingHelper.GetEntity<T>(reader);
                    result.Add(entity);
                }
            }

            return result;
        }

        /// <summary>
        /// Получить список всех сущностей по фильтру
        /// </summary>
        /// <returns></returns>
        public static List<T> Where(Expression<Func<T, bool>> expression)
        {
            List<T> result = new List<T>();

            using (Database db = Database.CreateFirebirdDatabase())
            {
                DbCommand command = db.GetCommandWhere<T>(expression);
                IDataReader reader = db.ExecuteReader(command);

                while (reader.Read())
                {
                    T entity = MappingHelper.GetEntity<T>(reader);
                    result.Add(entity);
                }
            }
            return result;
        }

        /// <summary>
        /// Добавить новую сущность
        /// </summary>
        /// <param name="item">Cущность</param>
        /// <returns></returns>
        public static int Insert(T item)
        {
            using (Database db = Database.CreateFirebirdDatabase())
            {
                DbCommand command = db.GetCommandInsert<T>(item);
                return Convert.ToInt32(db.ExecuteScalar(command));
            }
        }

        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="item">сущность</param>
        public static void Update(T item)
        {
            using (Database db = Database.CreateFirebirdDatabase())
            {
                DbCommand command = db.GetCommandUpdate<T>(item);
                db.ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// Удалить сущность по ID
        /// </summary>
        /// <param name="id">ID</param>
        public static void Remove(T item)
        {
            using (Database db = Database.CreateFirebirdDatabase())
            {
                DbCommand command = db.GetCommandDelete<T>(item);
                db.ExecuteNonQuery(command);
            }
        }
    }
}

