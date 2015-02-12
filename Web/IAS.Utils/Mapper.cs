using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Linq.Expressions;
using IAS.Utils.Properties;

namespace IAS.Utils
{
    public static class Mapper
    {
        public static List<T> MapToCollection<T>(this DataTable dataTable) where T : class
        {
            List<T> list = new List<T>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                T ent = MapToEntity<T>(dataTable.Rows[i]);
                list.Add(ent);
            }
            return list;
        }

        public static T MapToEntity<T>(this DataRow dataRow) where T : class
        {
            try
            {
                T targetType = (T)Activator.CreateInstance(typeof(T));

                foreach (var property in typeof(T).GetProperties())
                {
                    foreach (DataColumn key in dataRow.Table.Columns)
                    {
                        string columnName = key.ColumnName;
                        if (!String.IsNullOrEmpty(dataRow[columnName].ToString()))
                        {
                            if (property.Name == columnName)
                            {

                                Type t = Nullable.GetUnderlyingType(property.PropertyType)
                                            ?? property.PropertyType;

                                object safeValue = (dataRow[columnName] == null)
                                            ? null
                                            : Convert.ChangeType(dataRow[columnName], t);

                                property.SetValue(targetType, safeValue, null);
                            }
                        }
                    }
                }

                return targetType;
            }
            catch (MissingMethodException)
            {
                return null;
            }
        }

        public static bool MappingToEntity<T>(this T sourceEntity, T targetEntity) where T : class
        {
            return sourceEntity.MappingToEntity<T, T>(targetEntity);
        }

        public static bool MappingToEntity<T, U>(this T sourceEntity, U targetEntity)
            where T : class
            where U : class
        {
            object propertyValue = null;

            if (null == targetEntity)
                throw new NullReferenceException(Resources.errorMapper_001);

            PropertyInfo[] properties = targetEntity.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                propertyValue = null;
                if (property.GetSetMethod() != null && property.CanWrite)
                {
                    //entityProperty of Target
                    PropertyInfo entityProperty = sourceEntity.GetType()
                                                              .GetProperty(property.Name);

                    if (entityProperty != null)
                    {
                        if (entityProperty.PropertyType.BaseType == Type.GetType("System.ValueType") ||
                            entityProperty.PropertyType == Type.GetType("System.String"))
                        {
                            propertyValue = sourceEntity.GetType()
                                                        .GetProperty(property.Name)
                                                        .GetValue(sourceEntity, null);
                            property.SetValue(targetEntity, propertyValue, null);
                        }
                    }
                }
            }
            return true;
        }

        public static string GetPropertyName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }
    }
}
