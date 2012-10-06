using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace MessageScheduler
{
    public static class SharedExtensions
    {
        public static string GetDisplayName<T>(this T value) {
            var displayNameAttributes = typeof(T).GetCustomAttributes(typeof(DisplayNameAttribute), false);
            return displayNameAttributes.Cast<DisplayNameAttribute>().Select(x => x.DisplayName).FirstOrDefault();
        }

        public static void AddConfigurationElement<TModel, TProperty>(this NameValueCollection properties, Expression<Func<TModel, TProperty>> expression, TModel element) {
            var propertyName = expression.GetDisplayName();
            AddConfigurationElement(properties, propertyName, element.ToString());
        }

        public static void AddConfigurationElement(this NameValueCollection properties, Type element) {
            var propertyName = element.GetDisplayName();
            properties[propertyName] = element.FullName;
        }

        public static void AddConfigurationElement(this NameValueCollection properties, string key, string value) {
            properties[key] = value;
        }
    }
}