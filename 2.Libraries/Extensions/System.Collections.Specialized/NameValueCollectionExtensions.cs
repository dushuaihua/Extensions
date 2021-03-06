﻿using System.Linq;
namespace System.Collections.Specialized
{
    /// <summary>
    /// Common extensions of <see cref="NameValueCollection"/>
    /// </summary>
    public static partial class NameValueCollectionExtensions
    {
        /// <summary>
        /// Gets the value of <typeparamref name="T"/> associated with the specified key from the <see cref="NameValueCollection"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="collection">The <see cref="NameValueCollection"/></param>
        /// <param name="key">The key of the entry that contains the values to get.</param>
        /// <param name="defaultValue">The default value for return if not found from the <see cref="NameValueCollection"/></param>
        /// <returns>A <typeparamref name="T"/> with the specified key from the <see cref="NameValueCollection"/> if found; otherwise, default(<typeparamref name="T"/>).</returns>
        public static T GetValue<T>(this NameValueCollection collection, string key, T defaultValue = default(T)) where T : IConvertible
        {
            T returnValue = default(T);
            if (collection.AllKeys.Contains(key))
            {
                returnValue = (T)Convert.ChangeType(collection[key], typeof(T));
            }
            else
            {
                return defaultValue;
            }
            return returnValue;
        }
    }
}