/*************************************************************************************
* The MIT License(MIT)                                                               *
*                                                                                    *
* Copyright(c) Open Source Software Initiative Contributors                          *
*                                                                                    *
* Permission is hereby granted, free of charge, to any person obtaining a copy       *
* of this software and associated documentation files (the “Software”), to deal      *
* in the Software without restriction, including without limitation the rights       *
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies   *
* of the Software, and to permit persons to whom the Software is furnished to do so. *
*                                                                                    *
* THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS            *
* OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,        *
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE        *
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,  *
* WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN    *
* CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.         *
*************************************************************************************/

using System.ComponentModel;

namespace System
{
    /// <summary>
    /// Exposes a set of extension methods which allows conversion from/to string and different types.
    /// </summary>
    public static class StringConversionExtensions
    {
        /// <summary>Used by conversion functions in order to allow convert from string type.</summary>
        private static readonly Type _stringType = typeof(string);

        /// <summary>
        /// Converts a <see cref="string"/> value into a <typeparamref name="TOutput"/> value type equivalent.
        /// </summary>
        /// <typeparam name="TOutput">The expected output value conversion type.</typeparam>
        /// <param name="value">A string value which will be converted into a <typeparamref name="TOutput"/> value type equivalent.</param>
        /// <param name="convertedValue">The converted value.</param>
        /// 
        /// <remarks>The <see cref="TypeConverter"/> of the expected <typeparamref name="TOutput"/> type will be 
        /// used in conversion in order to translate the value of the <paramref name="value"/> parameter 
        /// into a <typeparamref name="TOutput"/> value type equivalent.</remarks>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// 
        /// <exception cref="FormatException"><paramref name="value"/> was not in a correct format.</exception>
        /// 
        /// <exception cref="ArgumentException">The <paramref name="value"/> is not convertible to the <typeparamref name="TOutput"/> type.</exception>
        public static void Convert<TOutput>(this string value, out TOutput convertedValue)
        {
            ConvertFromStringInternally(value, typeof(TOutput), true, out object conversionResult);
            convertedValue = (TOutput)conversionResult;
        }

        /// <summary>
        /// Converts a <see cref="string"/> value into a <paramref name="outputType"/> value type equivalent.
        /// </summary>
        /// <param name="value">A string value which will be converted into a <paramref name="outputType"/> value type equivalent.</param>
        /// <param name="outputType">The expected output value conversion type.</param>
        /// <param name="convertedValue">The converted value.</param>
        /// 
        /// <remarks>The <see cref="TypeConverter"/> of the expected <paramref name="outputType"/> type will be 
        /// used in conversion in order to translate the value of the <paramref name="value"/> parameter 
        /// into a <paramref name="outputType"/> value type equivalent.</remarks>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// 
        /// <exception cref="FormatException"><paramref name="value"/> was not in a correct format.</exception>
        /// 
        /// <exception cref="ArgumentException">The <paramref name="value"/> is not convertible to the <paramref name="outputType"/> type.</exception>
        public static void Convert(this string value, Type outputType, out object convertedValue)
        {
            ConvertFromStringInternally(value, outputType, true, out convertedValue);
        }


        /// <summary>
        /// Converts a value into a string value type equivalent.
        /// </summary>
        /// <param name="value">A value which will be converted into a string value type equivalent.</param>
        /// <param name="convertedValue">The converted value.</param>
        /// 
        /// <remarks>The <see cref="TypeConverter"/> of the <paramref name="value"/> type will be 
        /// used in conversion in order to translate the value of the <paramref name="value"/> parameter 
        /// into a string value type equivalent.</remarks>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// 
        /// <exception cref="ArgumentException">The <paramref name="value"/> is not convertible to string type.</exception>
        public static void Convert(this object value, out string convertedValue)
        {
            ConvertToStringInternally(value, true, out convertedValue);
        }

        /// <summary>
        /// Tries to convert a <see cref="string"/> value into a <typeparamref name="TOutput"/> value type equivalent.
        /// </summary>
        /// <typeparam name="TOutput">The expected output value conversion type.</typeparam>
        /// <param name="value">A string value which will be converted into a <typeparamref name="TOutput"/> value type equivalent.</param>
        /// <param name="convertedValue">The converted value.</param>
        /// 
        /// <remarks>The <see cref="TypeConverter"/> of the expected <typeparamref name="TOutput"/> type will be 
        /// used in conversion in order to translate the value of the <paramref name="value"/> parameter 
        /// into a <typeparamref name="TOutput"/> value type equivalent.</remarks>
        /// 
        /// <returns><c>True</c> is conversion succeeded, <c>False</c> otherwise.</returns>
        public static bool TryConvert<TOutput>(this string value, out TOutput convertedValue)
        {
            bool conversionSucceed = ConvertFromStringInternally(value, typeof(TOutput), false, out object conversionResult);
            if (conversionSucceed)
            {
                convertedValue = (TOutput)conversionResult;
            }
            else
            {
                convertedValue = default;
            }
            return conversionSucceed;
        }

        /// <summary>
        /// Tries to convert a <see cref="string"/> value into a <paramref name="outputType"/> value type equivalent.
        /// </summary>
        /// <param name="outputType">The expected output value conversion type.</param>
        /// <param name="value">A string value which will be converted into a <paramref name="outputType"/> value type equivalent.</param>
        /// <param name="convertedValue">The converted value.</param>
        /// 
        /// <remarks>The <see cref="TypeConverter"/> of the expected <paramref name="outputType"/> type will be 
        /// used in conversion in order to translate the value of the <paramref name="value"/> parameter 
        /// into a <paramref name="outputType"/> value type equivalent.</remarks>
        /// 
        /// <returns><c>True</c> is conversion succeeded, <c>False</c> otherwise.</returns>
        public static bool TryConvert(this string value, Type outputType, out object convertedValue)
        {
            return ConvertFromStringInternally(value, outputType, false, out convertedValue);
        }

        /// <summary>
        /// Tries to convert a value into a string value type equivalent.
        /// </summary>
        /// <param name="value">A value which will be converted into a string value type equivalent.</param>
        /// <param name="convertedValue">The converted value.</param>
        /// 
        /// <remarks>The <see cref="TypeConverter"/> of the <paramref name="value"/> type will be 
        /// used in conversion in order to translate the value of the <paramref name="value"/> parameter 
        /// into a string value type equivalent.</remarks>
        /// 
        /// <returns><c>True</c> is conversion succeeded, <c>False</c> otherwise.</returns>
        public static bool TryConvert(this object value, out string convertedValue)
        {
            return ConvertToStringInternally(value, false, out convertedValue);
        }

        private static bool ConvertFromStringInternally(string value, Type outputType, bool throwException, out object convertedValue)
        {
            if (outputType == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException($"{nameof(outputType)}");
                }
                convertedValue = default;
                return false;
            }
            TypeConverter paramTypeConverter = TypeDescriptor.GetConverter(outputType);

            if (value == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException($"{nameof(value)}");
                }
                convertedValue = default;
                return false;
            }
            // cannot convert from empty string to something else than string
            else if (outputType != _stringType && string.IsNullOrWhiteSpace(value))
            {
                if (throwException)
                {
                    throw new FormatException($"Cannot convert an empty value type to the {outputType} type.");
                }
                convertedValue = default;
                return false;
            }
            else if (!paramTypeConverter.CanConvertFrom(_stringType))
            {
                if (throwException)
                {
                    throw new ArgumentException($"The {outputType.FullName} does not support conversion from string type.");
                }
                convertedValue = default;
                return false;
            }
            else
            {
                try
                {
                    convertedValue = paramTypeConverter.ConvertFrom(value);
                }
                catch (Exception e)
                {
                    if (throwException)
                    {
                        throw new FormatException($"An error occured during converions from string value type to {outputType} value type:{Environment.NewLine}{e.Message}");
                    }
                    convertedValue = default;
                    return false;
                }

                if (convertedValue == null)
                {
                    if (outputType.IsValueType)
                    {
                        if (throwException)
                        {
                            throw new Exception($"The {paramTypeConverter} returned null. Expected type is:{outputType}.");
                        }
                        convertedValue = default;
                        return false;
                    }
                }
                else if (!outputType.IsInstanceOfType(convertedValue))
                {
                    if (throwException)
                    {
                        throw new Exception($"The {paramTypeConverter} returned an invalid data type. Expected type is:{outputType} but the returned type is {convertedValue.GetType()}");
                    }
                    convertedValue = default;
                    return false;
                }
                return true;
            }
        }

        private static bool ConvertToStringInternally(object value, bool throwException, out string convertedValue)
        {
            if (value == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException($"{nameof(value)}");
                }
                convertedValue = default;
                return false;
            }

            Type typeOfValue = value.GetType();
            if (typeOfValue == _stringType)
            {
                convertedValue = (string)value;
                return true;
            }
            TypeConverter valueTypeConverter = TypeDescriptor.GetConverter(typeOfValue);

            if (!valueTypeConverter.CanConvertTo(_stringType))
            {
                if (throwException)
                {
                    throw new ArgumentException($"The {valueTypeConverter} does not support conversion to string type.");
                }
                convertedValue = default;
                return false;
            }
            else
            {
                try
                {
                    convertedValue = valueTypeConverter.ConvertToString(value);
                    return true;
                }
                catch (Exception e)
                {
                    if (throwException)
                    {
                        throw new Exception($"An error occured during converions from {typeOfValue} value type to string type:{Environment.NewLine}{e.Message}", e);
                    }
                    convertedValue = default;
                    return false;
                }
            }
        }
    }
}