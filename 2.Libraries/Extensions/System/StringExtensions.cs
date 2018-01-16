﻿using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// Common extensions of <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The <see cref="string.Trim()"/> method only trims 0x0009, 0x000a, 0x000b, 0x000c, 0x000d, 0x0085, 0x2028, and 0x2029.
        /// This array adds in control characters.
        /// </summary>
        static readonly char[] nonVisiableCharacters = new char[]
        {
            (char)0x00, (char)0x01, (char)0x02, (char)0x03, (char)0x04, (char)0x05,
            (char)0x06, (char)0x07, (char)0x08, (char)0x09, (char)0x0a, (char)0x0b,
            (char)0x0c, (char)0x0d, (char)0x0e, (char)0x0f, (char)0x10, (char)0x11,
            (char)0x12, (char)0x13, (char)0x14, (char)0x15, (char)0x16, (char)0x17,
            (char)0x18, (char)0x19, (char)0x20, (char)0x1a, (char)0x1b, (char)0x1c,
            (char)0x1d, (char)0x1e, (char)0x1f, (char)0x7f, (char)0x85, (char)0x2028, (char)0x2029
        };
        /// <summary>
        /// Indicates whether the specified string is null or an System.String.Empty string(Include all non visible characters).
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        /// </summary>
        public static bool IsNullOrBlank(this string value)
        {
            if (value == null || string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            if (value.Trim(nonVisiableCharacters).Length == 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Removes all non visible characters from the current System.String object.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// The string that remains after all non visible characters are removed from the
        /// start and end of the current string. If no characters can be trimmed from the
        /// current instance, the method returns the current instance unchanged.
        ///</returns>
        public static string TrimAll(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            else
            {
                string result = TrimBlank(value);
                foreach (var item in nonVisiableCharacters)
                {
                    result = result.Replace(new string(new char[] { item }), string.Empty);
                }
                return result;
            }
        }
        /// <summary>
        /// Removes all leading and trailing non visible characters from the current System.String object.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// The string that remains after all non visible characters are removed from the
        /// start and end of the current string. If no characters can be trimmed from the
        /// current instance, the method returns the current instance unchanged.
        ///</returns>
        public static string TrimBlank(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            else
            {
                return value.Trim(nonVisiableCharacters);
            }
        }
        /// <summary>
        /// Indicates whether the specified string is a correct email address.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is a correct email address; otherwise, false.</returns>
        public static bool IsEmail(this string value)
        {
            if (value.IsNullOrBlank())
            {
                return false;
            }
            return new Regex(@"^[a-zA-Z0-9_+.-]+\@([a-zA-Z0-9-]+\.)+[a-zA-Z0-9]{2,4}$").Match(value).Success;
        }
        /// <summary>
        /// Indicates whether the specified string is a correct chinese mobile phone number.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is a correct chinese mobile phone number; otherwise, false.</returns>
        public static bool IsChineseMobile(this string value)
        {
            if (value.IsNullOrBlank())
            {
                return false;
            }
            return new Regex(@"^1(3[0-9]|4[57]|5[0-35-9]|7[0678]|8[0-9])\d{8}$").Match(value).Success;
        }
        /// <summary>
        /// Indicates whether the specified string is a correct uri.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is a correct uri; otherwise ,false.</returns>
        public static bool IsUrl(this string value)
        {
            return Uri.TryCreate(value, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }
        /// <summary>
        /// Indicates whether the specified string is Chinese character.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is Chinese character; otherwise, false.</returns>
        public static bool IsHans(this string value)
        {
            if (value.IsNullOrBlank())
            {
                return false;
            }
            return Regex.Match(value, @"[\u4e00-\u9fa5]").Success;
        }
        /// <summary>
        /// Trim the System.String without an <see cref="NullReferenceException"/>.
        /// Please refer to <see cref="string.Trim()"/>
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>The trimed string</returns>
        public static string SafeTrim(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value.Trim();
            }
        }
        /// <summary>
        /// Reverse the specified string.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>The reversed string</returns>
        public static string Reverse(this string value)
        {
            if (value.IsNullOrBlank())
            {
                return string.Empty;
            }
            char[] array = value.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
        /// <summary>
        /// Truncate the specified string to the specified length.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="length">The length of remained.</param>
        /// <param name="cutOffReplacement">The replacement of the removed part of the <see cref="string"/>.</param>
        /// <returns>The string truncated.</returns>
        public static string Truncate(this string value, int length, string cutOffReplacement = " ...")
        {
            if (string.IsNullOrEmpty(value) || value.Length <= length)
            {
                return value;
            }
            else
            {
                return $"{value.Remove(length)}{cutOffReplacement}";
            }
        }
        /// <summary>
        /// Indicates whether the specified string is between <paramref name="minLength"/> and <paramref name="maxLength"/>.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="minLength">The min length for validate.</param>
        /// <param name="maxLength">The max length for validate.</param>
        /// <param name="trim">true to trim the non visiable characters; otherwise, false.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>true if the value is valid length;otherwise, false.</returns>
        public static bool IsValidLength(this string value, int minLength, int maxLength, bool trim = false)
        {
            if (minLength < 0)
            {
                throw new ArgumentException("The minLength must be greater than 0.");
            }
            if (maxLength < minLength)
            {
                throw new ArgumentException("The maxLength must be greater than the minLength.");
            }
            if (maxLength > int.MaxValue)
            {
                throw new ArgumentException("The minLength must be less than Int32.MaxValue.");
            }
            if (value == null)
            {
                throw new ArgumentException("value can not be null.");
            }
            if (trim)
            {
                value = value.Trim().TrimBlank();
            }
            return value.Length >= minLength && value.Length <= maxLength;
        }
        /// <summary>
        /// Indicates whether the specified string bytes is between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="min">The min bytes count.</param>
        /// <param name="max">The max bytes count.</param>
        /// <param name="trim">true to trim the non visiable characters; otherwise, false.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>true if the value is valid byte count;otherwise, false.</returns>
        public static bool IsValidByteCount(this string value, int min, int max, bool trim = false)
        {
            if (min < 0)
            {
                throw new ArgumentException("min must be greater than 0.");
            }
            if (max < min)
            {
                throw new ArgumentException("The max must be greater than the min.");
            }
            if (max > int.MaxValue)
            {
                throw new ArgumentException("The max must be less than Int32.MaxValue.");
            }
            if (value == null)
            {
                throw new ArgumentException("value can not be null.");
            }
            if (trim)
            {
                value = value.Trim().TrimBlank();
            }
            int count = Text.Encoding.Default.GetByteCount(value);
            return count >= min && count <= max;
        }
        /// <summary>
        /// Indicates whether the specified string is a correct <see cref="Guid"/>.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="format">GUID format type.</param>
        /// <returns>true if the value parameter is a correct <see cref="Guid"/>; otherwise, false.</returns>
        public static bool IsGuid(this string value, string format = "D")
        {
            string[] formats = new[] { "D", "d", "N", "n", "P", "p", "B", "b", "X", "x" };
            try
            {
                if (!formats.Contains(format))
                {
                    format = formats[0];
                }
                Guid.ParseExact(value, format);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Convert specified string to a <see cref="byte"/> value.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="default">The default value while the convert faild.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="byte"/> value.If the specified string is not a byte value,returns the <paramref name="default"/>.</returns>
        public static byte ToByte(this string value, byte @default = 0)
        {
            try
            {
                return value.ToByte();
            }
            catch (Exception)
            {
                return @default;
            }
        }
        /// <summary>
        /// Convert specified string to a <see cref="short"/> value.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="default">The default value while the convert faild.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="short"/> value.If the specified string is not a <see cref="short"/> value,returns the <paramref name="default"/>.</returns>
        public static short ToInt16(this string value, short @default)
        {
            try
            {
                return value.ToInt16();
            }
            catch (Exception)
            {
                return @default;
            }
        }
        /// <summary>
        /// Convert specified string to an <see cref="int"/> value.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="default">The default value while the convert faild.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="int"/> value.If the specified string is not a <see cref="int"/> value,returns the <paramref name="default"/>.</returns>
        public static int ToInt32(this string value, int @default = 0)
        {
            try
            {
                return value.ToInt32();
            }
            catch (Exception)
            {
                return @default;
            }
        }
        /// <summary>
        /// Convert specified string to a <see cref="long"/> value.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="default">The default value while the convert faild.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="long"/> value.If the specified string is not a <see cref="long"/> value,returns the <paramref name="default"/>.</returns>
        public static long ToInt64(this string value, long @default = 0)
        {
            try
            {
                return value.ToInt64();
            }
            catch (Exception)
            {
                return @default;
            }
        }
        /// <summary>
        /// Convert specified string to a <see cref="byte"/> value.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="byte"/> value.</returns>
        public static byte ToByte(this string value)
        {
            if (!value.IsNullOrBlank() && byte.TryParse(value, out byte result))
            {
                return result;
            }
            throw new ArgumentException("The specified string is not a byte value.");
        }
        /// <summary>
        /// Convert specified string to a <see cref="short"/> value.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="short"/> value.</returns>
        public static short ToInt16(this string value)
        {
            if (!value.IsNullOrBlank() && short.TryParse(value, out short result))
            {
                return result;
            }
            throw new ArgumentException("The specified string is not a short value.");
        }
        /// <summary>
        /// Convert specified string to an <see cref="int"/> value.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="int"/> value.</returns>
        public static int ToInt32(this string value)
        {
            if (!value.IsNullOrBlank() && int.TryParse(value, out int result))
            {
                return result;
            }
            throw new ArgumentException("The specified string is not an int value.");
        }
        /// <summary>
        /// Convert specified string to a <see cref="long"/> value.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="long"/> value.</returns>
        public static long ToInt64(this string value)
        {
            if (!value.IsNullOrBlank() && long.TryParse(value, out long result))
            {
                return result;
            }
            throw new ArgumentException("The specified string is not a long value.");
        }
        /// <summary>
        /// Convert specified string to an <see cref="Enum"/> value.
        /// </summary>
        /// <typeparam name="T">The type of enum.</typeparam>
        /// <param name="value">The string to test.</param>
        /// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="Enum"/> value.</returns>
        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            if (value.IsNullOrBlank())
            {
                throw new ArgumentException("Must specify valid information for parsing in the string.", nameof(value));
            }
            Type t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException("Type provided must be an Enum.", "T");
            }
            return (T)Enum.Parse(t, value, ignoreCase);
        }
        /// <summary>
        /// Convert specified string to a <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="DateTime"/> value.</returns>
        public static DateTime ToDateTime(this string value)
        {
            if (!value.IsNullOrBlank() && DateTime.TryParse(value, out DateTime result))
            {
                return result;
            }
            throw new ArgumentException("The specified string is not an DateTime value.");
        }
        /// <summary>
        /// Convert specified string to a <see cref="Guid"/> value.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="format">GUID format type.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The converted <see cref="Guid"/> value.</returns>
        public static Guid ToGuid(this string value, string format = "D")
        {
            string[] formats = new[] { "D", "d", "N", "n", "P", "p", "B", "b", "X", "x" };
            if (!formats.Contains(format))
            {
                format = formats[0];
            }
            if (IsGuid(value, format))
            {
                return Guid.ParseExact(value, format);
            }
            else
            {
                throw new ArgumentException("Input string is not a valid GUID format.");
            }
        }
        /// <summary>
        /// Return the specified string with first character upper status.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            if (value.Length > 1)
            {
                return char.ToUpper(value[0]) + value.Substring(1);
            }
            return value.ToUpper();
        }
        /// <summary>
        /// Replaces the all special sharacters in <paramref name="value"/> with <paramref name="replacement"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns></returns>
        public static string ReplaceSpecialSharacters(this string value, char replacement = char.MinValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            var chars = new char[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                if (!value[i].IsLetterOrDigit())
                {
                    chars[i] = replacement;
                }
                else
                {
                    chars[i] = value[i];
                }
            }
            if (replacement == char.MinValue)
            {
                return new string(chars.Where(x => x != replacement).ToArray());
            }
            else
            {
                return new string(chars);
            }
        }
        /// <summary>
        /// Determines whether the <paramref name="value"/> is a valid culture identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the <paramref name="value"/> is valid culture identifier; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidCultureIdentifier(this string value)
        {
            try
            {
                CultureInfo c = CultureInfo.GetCultureInfo(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether this instance is byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The number style.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is byte; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsByte(this string value, NumberStyles style = NumberStyles.None)
        {
            try
            {
                byte.Parse(value, style);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether this instance is short.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The number style.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is short; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsShort(this string value, NumberStyles style = NumberStyles.None)
        {
            try
            {
                short.Parse(value, style);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether this instance is int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The number style.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is int32; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInt32(this string value, NumberStyles style = NumberStyles.None)
        {
            try
            {
                int.Parse(value, style);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether this instance is int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The number style.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is int64; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInt64(this string value, NumberStyles style = NumberStyles.None)
        {
            try
            {
                int.Parse(value, style);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether this instance is decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The number style.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is decimal; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDecimal(this string value, NumberStyles style = NumberStyles.None)
        {
            try
            {
                decimal.Parse(value, style);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether this instance is float.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The number style.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is float; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFloat(this string value, NumberStyles style = NumberStyles.None)
        {
            try
            {
                float.Parse(value, style);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}