using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DAO.Firebird.Utils
{
    /// <summary>
    /// Helper для рефлексии
    /// </summary>
    public  static class ReflectionHelper
    {
        /// <summary>
        /// Получить первый атрибут нужного типа
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута</typeparam>
        /// <param name="memberInfo">Тип</param>
        /// <returns></returns>
        public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo memberInfo)
            where TAttribute : Attribute
        {
            return (TAttribute)memberInfo.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();
        }
    }
}
