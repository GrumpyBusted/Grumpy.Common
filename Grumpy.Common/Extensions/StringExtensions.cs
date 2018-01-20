using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Grumpy.Common.Exceptions;

namespace Grumpy.Common.Extensions
{
    /// <summary>
    /// Extension method for the string
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class StringExtensions
    {
        /// <summary>
        /// Creates an object from serialized xml string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The xml string</param>
        /// <returns>The object</returns>
        public static T DeserializeFromXml<T>(this string value)
        {
            using (var reader = new StringReader(value))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
            }
        }

        /// <summary>
        /// Indicate whether the string is null or an empty string.
        /// </summary>
        /// <param name="value">Input string</param>
        /// <returns>True/false</returns>
        public static bool NullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Indicate whether the string is null, an empty string or only contain white spaces.
        /// </summary>
        /// <param name="value">This string</param>
        /// <returns>True/false</returns>
        public static bool NullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Converts string to stream, make sure stream is closed after it's been used.
        /// </summary>
        /// <param name="value">This string</param>
        /// <returns>The stream</returns>
        public static Stream ToStream(this string value)
        {
            if (value == null)
                return null;

            var result = new MemoryStream();
            var writer = new StreamWriter(result);

            writer.Write(value);
            writer.Flush();

            result.Position = 0;

            return result;
        }

        /// <summary>
        /// Make sure that a string starts with a specific string, adding the prefix if not already at the beginning.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="start">Prefix string</param>
        /// <returns>Result string</returns>
        public static string StartWith(this string value, string start)
        {
            return value.StartsWith(start) ? value : start + value;
        }

        /// <summary>
        /// Make sure that a string ends with a specific string, adding the suffix if not already at the end.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="end">Suffix string</param>
        /// <returns>Result string</returns>
        public static string EndWith(this string value, string end)
        {
            return value == null ? null : value.EndsWith(end) ? value : value + end;
        }

        /// <summary>
        /// Compare this string with another and return true is this string is the smallest/first.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="other">Another string</param>
        /// <returns>True/false</returns>
        public static bool Less(this string value, string other)
        {
            return string.Compare(value, other, StringComparison.Ordinal) < 0;
        }

        /// <summary>
        /// Compare this string with another and return true is this string is the largest/last.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="other">Another string</param>
        /// <returns>True/false</returns>
        public static bool Greater(this string value, string other)
        {
            return string.Compare(value, other, StringComparison.Ordinal) > 0;
        }

        /// <summary>
        /// Compare this string with another and return true is this string is the smallest/first or equal.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="other">Another string</param>
        /// <returns>True/false</returns>
        public static bool LessOrEqual(this string value, string other)
        {
            return string.Compare(value, other, StringComparison.Ordinal) <= 0;
        }

        /// <summary>
        /// Compare this string with another and return true is this string is the largest/last or equal.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="other">Another string</param>
        /// <returns>True/false</returns>
        public static bool GreaterOrEqual(this string value, string other)
        {
            return string.Compare(value, other, StringComparison.Ordinal) >= 0;
        }

        /// <summary>
        /// Indicates whether this string starts with any of multiple strings.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="strings">Possible strings</param>
        /// <returns>True/false</returns>
        public static bool StartsWith(this string value, params string[] strings)
        {
            var res = false;

            for (var i = 0; value != null && !res && i < strings.Length; ++i)
                res = value.StartsWith(strings[i]);

            return res;
        }

        /// <summary>
        /// Indicates whether this string contains any of a set of characters.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="characters">Characters to be contained</param>
        /// <returns>True/false</returns>
        public static bool ContainsAnyOf(this string value, params char[] characters)
        {
            return characters.Any(value.Contains);
        }

        /// <summary>
        /// Indicates whether this string contains all of a set of characters.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="characters">Characters to be contained</param>
        /// <returns>True/false</returns>
        public static bool ContainsAllOf(this string value, params char[] characters)
        {
            return characters.All(value.Contains);
        }

        /// <summary>
        /// Indicates whether this string contains any of a list of strings.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="strings">Strings to be contained</param>
        /// <returns>True/false</returns>
        public static bool ContainsAnyOf(this string value, params string[] strings)
        {
            return strings.Any(value.Contains);
        }

        /// <summary>
        /// Indicates whether this string contains all of list of strings.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="strings">Strings to be contained</param>
        /// <returns>True/false</returns>
        public static bool ContainsAllOf(this string value, params string[] strings)
        {
            return strings.All(value.Contains);
        }

        /// <summary>
        /// Type cast this string to an object of another type.
        /// </summary>
        /// <typeparam name="T">Type of result object</typeparam>
        /// <param name="value">This string</param>
        /// <param name="formatProvider">Specific format provider, default using invariant</param>
        /// <returns>Result object</returns>
        public static T To<T>(this string value, IFormatProvider formatProvider = null)
        {
            return (T)value.To(typeof(T), formatProvider);
        }

        /// <summary>
        /// Type cast this string to an object of another type.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="type">Type of result object</param>
        /// <param name="formatProvider">Specific format provider, default using invariant</param>
        /// <returns>Result object</returns>
        public static object To(this string value, Type type, IFormatProvider formatProvider = null)
        {
            if (type.IsEnum && value.Contains(","))
                throw new InvalidCastException("Enums cannot contain commas");

            if (type == typeof(TimeSpan))
                return value.ToTimeSpan();

            if (type.IsEnum)
                return Enum.Parse(type, value);

            var underlyingT = Nullable.GetUnderlyingType(type);

            return underlyingT == null ? Convert.ChangeType(value, type, formatProvider ?? CultureInfo.InvariantCulture) : Convert.ChangeType(value, underlyingT);
        }

        /// <summary>
        /// Returns the string as a Regex object.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="regexOptions">Regular expression option</param>
        /// <returns>Regular expression</returns>
        public static Regex AsRegex(this string value, RegexOptions regexOptions = RegexOptions.None)
        {
            return new Regex(value, regexOptions);
        }

        /// <summary>
        /// Indicates if the string matches a specified regular expression.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="regEx">Regular expression</param>
        /// <param name="regexOptions">Regular expression option for IsMatch</param>
        /// <returns>True/false</returns>
        public static bool MatchesRegex(this string value, string regEx, RegexOptions regexOptions = RegexOptions.None)
        {
            return new Regex(regEx, regexOptions).IsMatch(value);
        }

        /// <summary>
        /// Returns the left part of the string.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="length">Number of characters to extract</param>
        /// <returns>Result string</returns>
        public static string Left(this string value, int length = 1)
        {
            return value == null || length >= value.Length ? value : value.Substring(0, length >= 0 ? length : Math.Max(value.Length + length, 0));
        }

        /// <summary>
        /// Returns the right part of the string.
        /// </summary>
        /// <param name="value">This value</param>
        /// <param name="length">Number of characters to extract</param>
        /// <returns>Result string</returns>
        public static string Right(this string value, int length = 1)
        {
            if (value != null && length < 0)
                length = Math.Max(value.Length + length, 0);

            return value == null || length >= value.Length ? value : value.Substring(Math.Max(value.Length - length, 0), Math.Min(length, value.Length));
        }

        /// <summary>
        /// Split the string in to a list (IEnumerable) of strings, using a specific separator and escape character. 
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="separator">Separation character</param>
        /// <param name="escapeCharacter">Escape character</param>
        /// <returns>List of strings</returns>
        public static IEnumerable<string> SplitWithEscape(this string value, char separator, char escapeCharacter)
        {
            var parts = value.Split(separator);

            StringBuilder sb = null;

            foreach (var subString in parts)
            {
                var part = subString.Replace(escapeCharacter + escapeCharacter.ToString(), escapeCharacter.ToString());

                if (part.EndsWith(escapeCharacter.ToString()))
                {
                    if (sb == null)
                        sb = new StringBuilder();

                    sb.Append(part, 0, part.Length - 1);
                    sb.Append(separator);
                }
                else
                {
                    if (sb == null)
                        yield return part;
                    else
                    {
                        sb.Append(part);
                        yield return sb.ToString();
                        sb = null;
                    }
                }
            }

            if (sb != null)
                yield return sb.ToString();
        }

        /// <summary>
        /// Returns a string where any of a set of characters are replaced with a specific character.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="charactersToReplace">List of characters to replace</param>
        /// <param name="withCharacter">Character to replace with</param>
        /// <returns>Converted string</returns>
        public static string ReplaceAnyOf(this string value, string charactersToReplace, string withCharacter)
        {
            return charactersToReplace.Aggregate(value, (current, c) => current.Replace(c.ToString(), withCharacter));
        }

        /// <summary>
        /// Returns the string where specific characters are escaped.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="charactersToEscape">List of characters to escape</param>
        /// <param name="escapeCharacter">Escape character</param>
        /// <returns>Converted string</returns>
        public static string Escape(this string value, string charactersToEscape, char escapeCharacter)
        {
            var res = value.Replace(escapeCharacter.ToString(), escapeCharacter.ToString() + escapeCharacter);

            return charactersToEscape.Aggregate(res, (current, c) => current.Replace(c.ToString(), escapeCharacter.ToString() + c));
        }

        /// <summary>
        /// Returns the string with quotes if needed (When there blank characters or quotes).
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="quote">String to us as quote</param>
        /// <param name="blank">String to consider as blank character</param>
        /// <returns>Converted string</returns>
        public static string Quotify(this string value, string quote = "\"", string blank = " ")
        {
            return (blank == null || value.Contains(blank)) && (!value.StartsWith(quote) || !value.EndsWith(quote)) ? $"{quote}{value}{quote}" : value;
        }

        /// <summary>
        /// Find a specific occurrence of a string with in this string and return the position, if not found returns -1.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="searchFor">Search for value</param>
        /// <param name="nth">Search for the specified occurrence of the search for value</param>
        /// <param name="startIndex">Start search at index</param>
        /// <returns>Index of the search for value</returns>
        public static int IndexOfNth(this string value, string searchFor, int nth, int startIndex = 0)
        {
                if (nth < 1)
                    throw new NotSupportedException("Param 'nth' must be greater than 0!");

            while (true)
            {
                var indexOfNth = value.IndexOf(searchFor, startIndex, StringComparison.Ordinal);

                if (nth == 1)
                    return indexOfNth;

                nth = --nth;
                startIndex = indexOfNth + 1;
            }
        }

        /// <summary>
        /// Returns number of seconds in a string formatted timestamp.
        /// </summary>
        /// <param name="value">String containing a timestamp</param>
        /// <returns>Seconds as a interger value</returns>
        public static int ToSeconds(this string value)
        {
            return value.ToMilliseconds() / 1000;
        }

        /// <summary>
        /// Returns time span in a string formatted timestamp.
        /// </summary>
        /// <param name="value">String containing a timestamp</param>
        /// <returns>Timestamp as time span</returns>
        public static TimeSpan ToTimeSpan(this string value)
        {
            return TimeSpan.FromMilliseconds(value.ToMilliseconds());
        }

        /// <summary>
        /// Returns number of milliseconds in a string formatted timestamp.
        /// </summary>
        /// <param name="value">String containing a timestamp</param>
        /// <returns>Milliseconds as an integer value</returns>
        public static int ToMilliseconds(this string value)
        {
            var exp = new Regex(@"^(?<quantity>\d+)\s?(?<unit>[hoursmineckM]+)$");

            var quantityPos = exp.GroupNumberFromName("quantity");
            var unitPos = exp.GroupNumberFromName("unit");

            var parts = exp.Split(value);

            if (quantityPos > parts.Length || unitPos > parts.Length)
                throw new InvalidFormatException(value);
        
            var number = int.Parse(parts[quantityPos]);

            int factor;

            switch (parts[unitPos])
            {
                case "h":
                case "hour":
                case "hours":
                    factor = 3600000;
                    break;

                case "m":
                case "min":
                    factor = 60000;
                    break;

                case "s":
                case "sec":
                    factor = 1000;
                    break;

                case "ms":
                    factor = 1;
                    break;

                case "ks":
                    factor = 1000000;
                    break;

                case "Ms":
                    factor = 1000000000;
                    break;

                default:
                    throw new InvalidFormatException(parts.Last());
            }

            return number * factor;
        }

        /// <summary>
        /// Returns a copy of the string with proper case for a name, lower case except for first letter of each word and after hyphen (-). Also known as ToNameCase.
        /// </summary>
        /// <param name="value">This string</param>
        /// <returns>Converted string</returns>
        public static string ToProperName(this string value)
        {
            return value.ToProperName(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a copy of the string with proper case for a name, lower case except for first letter of each word and after hyphen (-). Also known as ToNameCase.
        /// </summary>
        /// <param name="value">This string</param>
        /// <param name="cultureInfo">Specific culture information</param>
        /// <returns>Converted string</returns>
        public static string ToProperName(this string value, CultureInfo cultureInfo)
        {
            if (value.NullOrEmpty())
                return value;

            var characters = value.ToLower(cultureInfo).ToCharArray();

            for (var i = 0; i < characters.Length; i++)
            {
                if (i == 0 || characters[i - 1].In(' ', '-'))
                    characters[i] = characters[i].ToString().ToUpper(cultureInfo)[0];
            }

            return new string(characters);
        }

        /// <summary>
        /// Returns a copy of the string with proper case for a paragraph, lower case except for first letter of each paragraph (.). This implementation is only tested with Danish, Swedish and Norwegian characters. Also known as ConvertStringToLowerSpecial.
        /// char after '. ' is to uppercase also.
        /// </summary>
        /// <param name="value">This string</param>
        /// <returns>Converted string</returns>
        public static string ToProperParagraph(this string value)
        {
            if (value.NullOrEmpty())
                return value;

            var tmp = value.ToLowerInvariant();

            tmp = tmp.ToUpperFirstCharacter();

            return Regex.Replace(tmp, @"\. [a-zæøåêéèëôóòöâáàä]", m => m.Value.ToUpper());
        }

        /// <summary>
        /// Returns a copy of the string with first character in upper case. This implementation is only tested with Danish, Swedish and Norwegian characters. Also known as UpperCaseFirstCharacter.
        /// </summary>
        /// <param name="value">This string</param>
        /// <returns>Converted string</returns>
        public static string ToUpperFirstCharacter(this string value)
        {
            return value.NullOrEmpty() ? value : Regex.Replace(value, "^[a-zæøåêéèëôóòöâáàä]", m => m.Value.ToUpper());
        }

        /// <summary>
        /// Returns a copy of the string with proper case, all lower case except for the very first character (This is upper case). This implementation is only tested with Danish, Swedish and Norwegian characters. Also known as ToLowerAndUpperCaseFirstCharacter.
        /// </summary>
        /// <param name="value">This string</param>
        /// <returns>Converted string</returns>
        public static string ToProperFirst(this string value)
        {
            return value.NullOrEmpty() ? value : Regex.Replace(value.ToLowerInvariant(), "^[a-zæøåêéèëôóòöâáàä]", m => m.Value.ToUpper());
        }

        /// <summary>
        /// Returns a copy of the string converted to proper case, first letter of each word upper case rest lower case. This implementation is only tested with Danish, Swedish and Norwegian characters. Also known as ToLowerAndUpperCaseFirstCharacterInEachWord.
        /// </summary>
        /// <param name="value">This string</param>
        /// <returns>Converted string</returns>
        public static string ToProper(this string value)
        {
            if (value.NullOrEmpty())
                return value;

            var tmp = value.ToLowerInvariant();

            tmp = tmp.ToUpperFirstCharacter();

            return Regex.Replace(tmp, @" [a-zæøåêéèëôóòöâáàä]", m => m.Value.ToUpper());
        }
    }
}