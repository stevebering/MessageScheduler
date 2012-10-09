using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MessageScheduler
{
    public static class SharedExtensions
    {
        public static string GetDisplayName(this MemberInfo memberInfo) {
            var displayNameAttributes = memberInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            return displayNameAttributes.Cast<DisplayNameAttribute>().Select(x => x.DisplayName).FirstOrDefault();
        }

        public static object GetValue<TModel>(this MemberInfo memberInfo, TModel element) {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo == null) {
                throw new ArgumentException("No property reference could be found.", "memberInfo");
            }

            return propertyInfo.GetValue(element);
        }

        public static MemberInfo GetPropertyInfo(Expression expression) {
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null) {
                var unaryExpression = expression as UnaryExpression;
                if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert) {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
            }

            if (memberExpression != null && memberExpression.Member.MemberType == MemberTypes.Property) {
                return memberExpression.Member;
            }

            return null;
        }

        public static void AddConfigurationElement<TModel, TProperty>(this NameValueCollection properties, Expression<Func<TModel, TProperty>> expression, TModel element) {
            var propertyInfo = GetPropertyInfo(expression.Body);
            var propertyName = propertyInfo.GetDisplayName();
            string displayValue;
            var value = propertyInfo.GetValue(element);
            if (value is Type) {
                displayValue = ((Type)value).AssemblyQualifiedName;
            }
            else {
                displayValue = value.ToString();
            }
            AddConfigurationElement(properties, propertyName, displayValue);
        }

        //public static void AddConfigurationElement(this NameValueCollection properties, Type element) {
        //    var propertyName = element.GetDisplayName();
        //    properties[propertyName] = element.FullName;
        //}

        public static void AddConfigurationElement(this NameValueCollection properties, string key, string value) {
            properties[key] = value;
        }
    }
}