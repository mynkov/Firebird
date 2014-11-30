using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Expressions;
using DAO.Firebird.Entities;
using DAO.Firebird.Attributes;

namespace DAO.Firebird.Utils
{
    /// <summary>
    /// Построитель запросов
    /// </summary>
    public static class QueryBuilder
    {
        /// <summary>
        /// Получить команду вставки
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public static DbCommand GetCommandInsert<T>(this Database database, Entity entity)
            where T : Entity
        {
            string queryTemplate = "INSERT INTO {0} ({1}) VALUES ({2}) RETURNING {3}";
            Dictionary<string, string> paramValues = new Dictionary<string, string>();
            
            string tableName = MappingHelper.GetTableName<T>();
            string columnNames = GetColumnNames<T>(false);
            string fieldId = MappingHelper.GetNameIdField<T>();
            string insertValues = GetInsertValues<T>(entity, paramValues);

            DbCommand command = database.GetCommand(string.Format(queryTemplate, tableName, columnNames, insertValues, fieldId));

            foreach (var param in paramValues)
            {
                database.AddInParameter(command, param.Key, param.Value);
            }

            return command;
        }

        /// <summary>
        /// Получить команду обновления
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public static DbCommand GetCommandUpdate<T>(this Database database, Entity entity)
            where T : Entity
        {
            string queryTemplate = "UPDATE {0} SET {1} WHERE {2}";
            Dictionary<string, string> paramValues = new Dictionary<string, string>();

            string tableName = MappingHelper.GetTableName<T>();
            string setString = GetSetString<T>(entity, paramValues);
            string whereExpression = GetWhereExpression<T>(entity, paramValues);

            DbCommand command = database.GetCommand(string.Format(queryTemplate, tableName, setString, whereExpression));

            foreach (var param in paramValues)
            {
                database.AddInParameter(command, param.Key, param.Value);
            }

            return command;
        }

        /// <summary>
        /// Получить команду удаления
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public static DbCommand GetCommandDelete<T>(this Database database, Entity entity)
            where T : Entity
        {
            string queryTemplate = "DELETE FROM {0} WHERE {1}";
            Dictionary<string, string> paramValues = new Dictionary<string, string>();

            string tableName = MappingHelper.GetTableName<T>();
            string whereExpression = GetWhereExpression<T>(entity, paramValues);

            DbCommand command = database.GetCommand(string.Format(queryTemplate, tableName, whereExpression));

            foreach (var param in paramValues)
            {
                database.AddInParameter(command, param.Key, param.Value);
            }

            return command;
        }

        /// <summary>
        /// Получить команду для извлечения всех записей
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public static DbCommand GetCommandWhere<T>(this Database database)
            where T : Entity
        {
            string queryTemplate = "SELECT {0} FROM {1}";

            string columnNames = GetColumnNames<T>(true);
            string tableName = MappingHelper.GetTableName<T>();

            DbCommand command = database.GetCommand(string.Format(queryTemplate, columnNames, tableName));

            return command;
        }

        /// <summary>
        /// Получить команду для извлечения всех записей по фильтру
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public static DbCommand GetCommandWhere<T>(this Database database, Expression<Func<T, bool>> expression)
            where T : Entity
        {
            string queryTemplate = "SELECT {0} FROM {1} WHERE {2}";
            Dictionary<string, string> paramValues = new Dictionary<string, string>();

            string columnNames = GetColumnNames<T>(true);
            string tableName = MappingHelper.GetTableName<T>();
            string whereExpression = GetWhereExpression<T>(expression, paramValues);

            DbCommand command = database.GetCommand(string.Format(queryTemplate, columnNames, tableName, whereExpression));

            foreach (var param in paramValues)
            {
                database.AddInParameter(command, param.Key, param.Value);
            }

            return command;
        }

        /// <summary>
        /// Получить команду для получения записей по фильтру
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="database">Database</param>
        /// <param name="expression">Выражение</param>
        /// <returns></returns>
        public static DbCommand GetCommandFirst<T>(this Database database, Expression<Func<T, bool>> expression)
            where T : Entity
        {    
            string queryTemplate = "SELECT {0} FROM {1} WHERE {2}";
            Dictionary<string, string> paramValues = new Dictionary<string, string>();
            

            string columnNames = GetColumnNames<T>(true);
            string tableName = MappingHelper.GetTableName<T>();
            string whereExpression = GetWhereExpression<T>(expression, paramValues);

            DbCommand command = database.GetCommand(string.Format(queryTemplate, columnNames, tableName, whereExpression));

            foreach (var param in paramValues)
            {
                 database.AddInParameter(command, param.Key, param.Value);
            }
           
            return command;
        }

        /// <summary>
        /// Получить имена колонок через запятую
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <returns></returns>
        private static string GetColumnNames<T>(bool withId) 
            where T : Entity
        {
            StringBuilder fieldNames = new StringBuilder();

            foreach (string fieldName in MappingHelper.GetNameFields<T>(withId))
            {
                fieldNames.Append(fieldName + ", ");
            }

            return fieldNames.Remove(fieldNames.Length - 2, 2).ToString();
        }

        /// <summary>
        /// Получить выражение WHERE
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="expression">Выражение</param>
        /// <param name="paramValues">Словарь значений параметров</param>
        /// <returns></returns>
        private static string GetWhereExpression<T>(Expression<Func<T, bool>> expression, Dictionary<string, string> paramValues)
            where T : Entity
        {
            string result = string.Empty;

            BinaryExpression body = expression.Body as BinaryExpression;

            if (body != null)
            {
                result = GetStringFromBinaryExpression<T>(body, paramValues);
            }

            return result;
        }

        /// <summary>
        /// Получить значения вставки
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        private static string GetInsertValues<T>(Entity entity, Dictionary<string, string> paramValues)
            where T : Entity
        {
            StringBuilder result = new StringBuilder();
            string fieldId = MappingHelper.GetNameIdField<T>();

            foreach (var field in MappingHelper.GetNameAndValueFields(entity))
            {
                if (field.Key != fieldId)
                {        
                    result.Append(string.Format("@{0}{1}, ",field.Key, paramValues.Count));
                    paramValues.Add(string.Format("@{0}{1}",field.Key, paramValues.Count), field.Value);
                }
            }


            return result.Remove(result.Length - 2, 2).ToString();
        }

        /// <summary>
        /// Получить значения SET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        private static string GetSetString<T>(Entity entity, Dictionary<string, string> paramValues)
            where T : Entity
        {
            StringBuilder result = new StringBuilder();
            string fieldId = MappingHelper.GetNameIdField<T>();

            foreach (var field in MappingHelper.GetNameAndValueFields(entity))
            {
                if (field.Key != fieldId)
                {
                    result.Append(string.Format("{0} = @{0}{1}, ", field.Key, paramValues.Count));
                    paramValues.Add(string.Format("@{0}{1}", field.Key, paramValues.Count), field.Value);
                }
            }


            return result.Remove(result.Length - 2, 2).ToString();
        }

        /// <summary>
        /// Получить WHERE выражение
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        private static string GetWhereExpression<T>(Entity entity, Dictionary<string, string> paramValues)
            where T : Entity
        {
            string result = string.Format("{0} = @{0}{1}", MappingHelper.GetNameIdField<T>(), paramValues.Count);
            paramValues.Add(string.Format("@{0}{1}",MappingHelper.GetNameIdField<T>(), paramValues.Count), entity.Id.ToString());

            return result;
        }


        /// <summary>
        /// Получить выражение в виде строки
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="body">Бинарное выражение</param>
        /// <param name="paramValues">Словарь значений параметров</param>
        /// <returns></returns>
        private static string GetStringFromBinaryExpression<T>(BinaryExpression body, Dictionary<string, string> paramValues)
            where T : Entity
        {
            string result = string.Empty;

            if (body.Left is MemberExpression)
            {
                string fieldName = GetFieldName<T>((MemberExpression)body.Left);
                string rightValue = GetRightValue((dynamic)body.Right);

                if (rightValue != null)
                {
                    result = string.Format("{0} {1} @{0}{2}", fieldName, GetOperator(body.NodeType), paramValues.Count);
                    paramValues.Add("@" + fieldName + paramValues.Count, rightValue);
                }
                else
                {
                    result = string.Format("{0} {1} NULL", fieldName, GetNullOperator(body.NodeType));
                }
            }
            else if (body.Left is BinaryExpression && body.Right is BinaryExpression)
            {
                result += string.Format("{0} {1} {2}",
                                        GetStringFromBinaryExpression<T>((BinaryExpression)body.Left, paramValues),
                                        GetOperator(body.NodeType),
                                        GetStringFromBinaryExpression<T>((BinaryExpression)body.Right, paramValues));
            }
            else
            {
                throw new Exception("Неизвестный тип частей выражения");
            }

            return result;
        }

        /// <summary>
        /// Получить имя колонки
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="left">Левая часть выражения</param>
        /// <returns></returns>
        private static string GetFieldName<T>(MemberExpression left) 
            where T : Entity
        {
            string fieldName = string.Empty;

            foreach (var property in typeof (T).GetProperties())
            {
                if (property.Name == left.Member.Name)
                {
                    fieldName = property.GetCustomAttribute<PropertyBindAttribute>().FieldName;
                    break;
                }
            }

            return fieldName;
        }

        /// <summary>
        /// Получить правое значение из члена выражения
        /// </summary>
        /// <param name="right">Член выражения</param>
        /// <returns></returns>
        private static string GetRightValue(MemberExpression right)
        {
            ConstantExpression constExpr = (ConstantExpression)right.Expression;
            return ((FieldInfo)right.Member).GetValue(constExpr.Value).ToString();
        }

        /// <summary>
        /// Получить правое значение из константы
        /// </summary>
        /// <param name="right">Константа выражения</param>
        /// <returns></returns>
        private static string GetRightValue(ConstantExpression right)
        {
            return right.Value != null ? right.Value.ToString() : null;
        }

        /// <summary>
        /// Получить правое значение из метода
        /// </summary>
        /// <param name="right">Метод выражения</param>
        /// <returns></returns>
        private static string GetRightValue(MethodCallExpression right)
        {
            MethodInfo methodInfo = right.Method;
            ConstantExpression constExpr = (ConstantExpression)right.Object;
            return methodInfo.Invoke(constExpr.Value, null).ToString();
        }

        /// <summary>
        /// Получить оператор
        /// </summary>
        /// <param name="expressionType">Тип выражения</param>
        /// <returns></returns>
        private static string GetOperator(ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.OrElse:
                    return "OR";
                default:
                    throw new Exception("Неизвестный оператор выражения");
            }
        }

        /// <summary>
        /// Получить NULL оператор
        /// </summary>
        /// <param name="expressionType">Тип выражения</param>
        /// <returns></returns>
        private static string GetNullOperator(ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.Equal:
                    return "IS";
                case ExpressionType.NotEqual:
                    return "IS NOT";
                default:
                    throw new Exception("Неизвестный оператор выражения");
            }
        }

    }
}
