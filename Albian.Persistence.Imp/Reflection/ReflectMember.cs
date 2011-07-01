using System;
using System.Collections.Generic;
using System.Reflection;
using Albian.Persistence.Imp.Cache;
using Albian.Persistence.Imp.Model;
using Albian.Persistence.Model;

namespace Albian.Persistence.Imp.Reflection
{
    public class ReflectMember : IReflectMember
    {
        #region IReflectMember Members

        public IDictionary<string, IMemberAttribute> ReflectMembers(string typeFullName, out string defaultTableName)
        {
            //string typeFullName = entity.Implement;
            if (string.IsNullOrEmpty(typeFullName))
            {
                throw new ArgumentNullException("typeFullName");
            }
            Type type = Type.GetType(typeFullName, true);
            defaultTableName = type.Name;

            PropertyInfo[] propertyInfos =
                type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            if (null == propertyInfos || 0 == propertyInfos.Length)
            {
                throw new Exception(string.Format("Reflect the {0} property is error.", typeFullName));
            }
            PropertyCache.InsertOrUpdate(typeFullName, propertyInfos);

            IDictionary<string, IMemberAttribute> memberAttributes = new Dictionary<string, IMemberAttribute>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IMemberAttribute memberAttribute = ReflectProperty(propertyInfo);
                memberAttributes.Add(memberAttribute.Name, memberAttribute);
            }
            if (0 == memberAttributes.Count)
            {
                return null;
            }

            MemberCache.InsertOrUpdate(typeFullName, memberAttributes);
            return memberAttributes;
        }

        public IMemberAttribute ReflectProperty(PropertyInfo propertyInfo)
        {
            IMemberAttribute memberAttribute = new AlbianMemberAttribute
                                                   {
                                                       Name = propertyInfo.Name,
                                                       PrimaryKey = false,
                                                       FieldName = propertyInfo.Name,
                                                       DBType = ConvertToDbType.Convert(propertyInfo.PropertyType),
                                                       //GeneratorMode = GeneratorMode.Custom,
                                                       IsSave = true,
                                                       AllowNull = Utils.IsNullableType(propertyInfo.PropertyType)
                                                   };
            //MemberCache.InsertOrUpdate(memberAttribute.Name, memberAttribute);
            return memberAttribute;
        }

        #endregion
    }
}