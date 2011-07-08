using System;
using System.Collections.Generic;
using System.Data;
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
            object[] attrs = propertyInfo.GetCustomAttributes(typeof(AlbianMemberAttribute), true);
            IMemberAttribute memberAttribute;
            if (null == attrs || 0 == attrs.Length)
            {
                memberAttribute = new AlbianMemberAttribute
                                                  {
                                                      Name = propertyInfo.Name,
                                                      PrimaryKey = false,
                                                      FieldName = propertyInfo.Name,
                                                      DBType = ConvertToDbType.Convert(propertyInfo.PropertyType),
                                                      IsSave = true,
                                                      AllowNull = Utils.IsNullableType(propertyInfo.PropertyType)
                                                  };
            }
            else
            {
                AlbianMemberAttribute attr = (AlbianMemberAttribute) attrs[0];
                memberAttribute = new AlbianMemberAttribute();
                if (string.IsNullOrEmpty(attr.FieldName))
                    memberAttribute.FieldName = propertyInfo.Name;
                if (string.IsNullOrEmpty(attr.Name))
                    memberAttribute.Name = propertyInfo.Name;

                //it have problem possible
                memberAttribute.AllowNull = attr.AllowNull ? Utils.IsNullableType(propertyInfo.PropertyType) ? true : false : false ;
                memberAttribute.DBType = DbType.Object ==  attr.DBType ? ConvertToDbType.Convert(propertyInfo.PropertyType) :attr.DBType;
                memberAttribute.IsSave = attr.IsSave;
                memberAttribute.Length = attr.Length;
                memberAttribute.PrimaryKey = attr.PrimaryKey;
            }
            return memberAttribute;
        }
        #endregion
    }
}