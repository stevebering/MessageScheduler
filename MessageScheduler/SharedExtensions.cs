using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace MessageScheduler
{
    public static class SharedExtensions
    {
        public static string GetDisplayName<T>(this T value) {
            var displayNameAttributes = typeof(T).GetCustomAttributes(typeof(DisplayNameAttribute), false);
            return displayNameAttributes.Cast<DisplayNameAttribute>().Select(x => x.DisplayName).FirstOrDefault();
        }

        public static void AddConfigurationElement<T>(this NameValueCollection properties, T element) {
            var propertyName = element.GetDisplayName();
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