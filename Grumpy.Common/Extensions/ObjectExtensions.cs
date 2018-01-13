using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Grumpy.Common.Extensions
{
    /// <summary>
    /// Extension method for the generic object.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns xml string of serialize an object.
        /// </summary>
        /// <typeparam name="T">The type of an object</typeparam>
        /// <param name="obj">The object to serialize to a string</param>
        /// <returns>The string representation of object</returns>
        public static string SerializeToXml<T>(this T obj)
        {
            return obj.SerializeToXml(true);
        }

        /// <summary>
        /// Serialize an object to a xml string.
        /// </summary>
        /// <typeparam name="T">The type of an object</typeparam>
        /// <param name="obj">The object to serialize to a string</param>
        /// <param name="formatXml">Should the XML be formatted</param>
        /// <returns>The string representation of object</returns>
        public static string SerializeToXml<T>(this T obj, bool formatXml)
        {
            var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var settings = new XmlWriterSettings
            {
                Indent = formatXml,
                OmitXmlDeclaration = true
            };

            if (!formatXml)
                settings.NewLineChars = string.Empty;

            return obj.SerializeToXml(ns, settings);
        }

        /// <summary>
        /// Serialize an object to a xml string.
        /// </summary>
        /// <typeparam name="T">The type of an object</typeparam>
        /// <param name="obj">The object to serialize to a string</param>
        /// <param name="xmlSerializerNamespaces">XML Namespaces</param>
        /// <param name="xmlWriterSettings">XML Serializer Settings</param>
        /// <returns>The string representation of object</returns>
        public static string SerializeToXml<T>(this T obj, XmlSerializerNamespaces xmlSerializerNamespaces, XmlWriterSettings xmlWriterSettings)
        {
            using (var stream = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));

                using (var writer = XmlWriter.Create(stream, xmlWriterSettings))
                {
                    serializer.Serialize(writer, obj, xmlSerializerNamespaces);

                    return stream.ToString();
                }
            }
        }

        /// <summary>
        /// Indicate if the object is null.
        /// </summary>
        /// <typeparam name="T">The type of an object</typeparam>
        /// <param name="obj">The object</param>
        /// <returns>True/false</returns>
        public static bool IsNull<T>(this T obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Check if the object is in one of multiple values.
        /// </summary>
        /// <typeparam name="T">The type of an object</typeparam>
        /// <param name="item">The object</param>
        /// <param name="items">Possible values</param>
        /// <returns></returns>
        public static bool In<T>(this T item, params T[] items)
        {
            return items?.Contains(item) ?? item == null;
        }

        /// <summary>
        /// Check if the value of on object is between two values (Inclusive).
        /// </summary>
        /// <typeparam name="T">The type of an object</typeparam>
        /// <param name="item">The object</param>
        /// <param name="start">Lower value</param>
        /// <param name="end">Upper value</param>
        /// <returns>True/false</returns>
        public static bool Between<T>(this T item, T start, T end) where T : IComparable, IComparable<T>
        {
            return Comparer<T>.Default.Compare(item, start) >= 0 && Comparer<T>.Default.Compare(item, end) <= 0;
        }

        /// <summary>
        /// Check if the value of on object is between two values (Exclusive).
        /// </summary>
        /// <typeparam name="T">The type of an object</typeparam>
        /// <param name="item">The object</param>
        /// <param name="start">Lower value</param>
        /// <param name="end">Upper value</param>
        /// <returns>True/false</returns>
        public static bool BetweenExclusive<T>(this T item, T start, T end) where T : IComparable, IComparable<T>
        {
            return Comparer<T>.Default.Compare(item, start) > 0 && Comparer<T>.Default.Compare(item, end) < 0;
        }

        /// <summary>
        /// Return any object as IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of an object</typeparam>
        /// <param name="item">The object</param>
        /// <returns>One occurrence of the object as Enumerable</returns>
        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            return Enumerable.Repeat(item, 1);
        }

        /// <summary>
        /// Indicate it an object is a number
        /// </summary>
        /// <param name="value">This object</param>
        /// <returns>True = Object is a number (int, long etc)</returns>
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                || value is byte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is double
                || value is decimal;
        }
    }
}
