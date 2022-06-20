using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Xml.Linq;
using NUnit.Framework;

namespace DotNetToolkit.Tests
{
    [TestFixture]
    [NUnit.Framework.Category("UnitTests")]
    public class ConvertFunctionsTests
    {
        public const string DataSourceNameForConversionFromString = "GetDataSourceForConversionFromString";
        public const string DataSourceNameForConversionToString = "GetDataSourceForConversionToString";

        #region Tests Required Types

        public interface IDataForConversionFromString
        {
            void TestConvertFromStringWithGenerics();
            void TestConvertFromStringWithoutGenerics();

            void TestTryConvertFromStringWithGenerics();
            void TestTryConvertFromStringWithoutGenerics();
        }

        class ValidDataForConversionFromString<TData> : IDataForConversionFromString
        {
            private readonly Type _dataType = typeof(TData);

            public TData ExpectedValue { get; set; }

            public string DataValue { get; set; }

            public void TestConvertFromStringWithGenerics()
            {
                DataValue.Convert(out TData extractedResult);
                Assert.That(extractedResult, Is.EqualTo(ExpectedValue));
            }


            public void TestConvertFromStringWithoutGenerics()
            {
                DataValue.Convert(_dataType, out object extractedResult);
                Assert.That(extractedResult, Is.EqualTo(ExpectedValue));
            }

            public void TestTryConvertFromStringWithGenerics()
            {
                bool conversionSucceed = DataValue.TryConvert(out TData extractedResult);
                Assert.IsTrue(conversionSucceed);
                Assert.That(extractedResult, Is.EqualTo(ExpectedValue));
            }

            public void TestTryConvertFromStringWithoutGenerics()
            {
                bool conversionSucceed = DataValue.TryConvert(_dataType, out object extractedResult);
                Assert.IsTrue(conversionSucceed);
                Assert.That(extractedResult, Is.EqualTo(ExpectedValue));
            }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>
            /// A string that represents the current object.
            /// </returns>
            public override string ToString()
            {
                if (DataValue == string.Empty)
                {
                    return "empty string" + " to ->" + typeof(TData);
                }
                else
                {
                    return (DataValue ?? "null") + " to ->" + typeof(TData);
                }
            }
        }


        class InvalidDataForConversionFromString<TData> : IDataForConversionFromString
        {
            private readonly Type _dataType = typeof(TData);

            public Type ExpectedExceptionType { get; set; }

            public string DataValue { get; set; }

            public void TestConvertFromStringWithGenerics()
            {
                Assert.Throws(ExpectedExceptionType, () => DataValue.Convert(out TData extractedResult));
            }

            public void TestTryConvertFromStringWithGenerics()
            {
                bool conversionSucceed = DataValue.TryConvert(out TData extractedResult);
                Assert.IsFalse(conversionSucceed);
                Assert.That(extractedResult, Is.EqualTo(default(TData)));
            }

            public void TestConvertFromStringWithoutGenerics()
            {
                Assert.Throws(ExpectedExceptionType, () => DataValue.Convert(_dataType, out object extractedResult));
            }

            public void TestTryConvertFromStringWithoutGenerics()
            {
                bool conversionSucceed = DataValue.TryConvert(_dataType, out object extractedResult);
                Assert.IsFalse(conversionSucceed);
                Assert.That(extractedResult, Is.EqualTo(null));
            }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>
            /// A string that represents the current object.
            /// </returns>
            public override string ToString()
            {
                string dataValue = (DataValue ?? "null");
                if (dataValue == string.Empty)
                {
                    dataValue = "empty string";
                }

                string expectedException;
                if (ExpectedExceptionType == null)
                {
                    expectedException = string.Empty;
                }
                else
                {
                    expectedException = ExpectedExceptionType.Name;
                }

                return "data value: " + dataValue + " to " + typeof(TData).Name + " => " + expectedException;
            }
        }

        public interface IDataForConversionToString
        {
            void TestConvertToString();
            void TestTryConvertToString();
        }

        class ValidDataForConversionToString : IDataForConversionToString
        {
            public object DataValue { get; set; }

            public string ExpectedValue { get; set; }


            public void TestConvertToString()
            {
                DataValue.Convert(out string extractedResult);
                Assert.That(extractedResult, Is.EqualTo(ExpectedValue));
            }

            public void TestTryConvertToString()
            {
                bool conversionSucceed = DataValue.TryConvert(out string extractedResult);
                Assert.IsTrue(conversionSucceed);
                Assert.That(extractedResult, Is.EqualTo(ExpectedValue));
            }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>
            /// A string that represents the current object.
            /// </returns>
            public override string ToString()
            {
                if (DataValue != null && DataValue.Equals(string.Empty))
                {
                    return "empty string" + " to -> string";
                }
                else
                {
                    string dataType = DataValue.GetType().Name;
                    return dataType + " with value:" + (DataValue ?? "null") + " to -> string";
                }
            }
        }

        class InvalidDataForConversionToString : IDataForConversionToString
        {
            public object DataValue { get; set; }

            public Type ExpectedExceptionType { get; set; }

            public void TestConvertToString()
            {
                Assert.Throws(ExpectedExceptionType, () => DataValue.Convert(out string extractedResult));
            }

            public void TestTryConvertToString()
            {
                bool conversionSucceed = DataValue.TryConvert(out string extractedResult);
                Assert.IsFalse(conversionSucceed);
            }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>
            /// A string that represents the current object.
            /// </returns>
            public override string ToString()
            {
                object dataValue = DataValue ?? "null";
                if (dataValue != null && dataValue.Equals(string.Empty))
                {
                    dataValue = "empty string";
                }

                string expectedException;
                if (ExpectedExceptionType == null)
                {
                    expectedException = string.Empty;
                }
                else
                {
                    expectedException = ExpectedExceptionType.Name;
                }

                return "data value: " + dataValue + " to string => " + expectedException;
            }
        }

        class DemoTypeConverter:TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                string customConversion = null;
                if (value is DemoClass demoClass)
                {
                    customConversion = demoClass.Value;
                }
                else if (value is DemoStruct demoStruct)
                {
                    customConversion = demoStruct.Value;
                }

                if (customConversion != null)
                {
                    switch (customConversion)
                    {
                        case "returnMeANull":
                            return null;
                        case "returnMeDemoText":
                            return "demo";
                        case "returnMeDemoClassInstance":
                            return DemoClass.Instance;
                        case "returnMeDemoStructInstance":
                            return DemoStruct.Instance;
                    }
                }
                

                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string stringValue)
                {
                    switch (stringValue)
                    {
                        case "returnMeANull":
                            return null;
                        case "returnMeA10":
                            return 10;
                        case "returnMeDemoClassInstance":
                            return DemoClass.Instance;
                        case "returnMeDemoStructInstance":
                            return DemoStruct.Instance;
                    }

                }
                return base.ConvertFrom(context, culture, value);
            }
        }

        class DemoTypeConverter2 : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return false;
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return false;
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                throw new InvalidOperationException();
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                throw new InvalidOperationException();
            }
        }


        [TypeConverter(typeof(DemoTypeConverter))]
        class DemoClass
        {
            public string Value { get; set; }
            public static DemoClass Instance { get; } = new DemoClass();
        }

        [TypeConverter(typeof(DemoTypeConverter))]
        struct DemoStruct
        {
            public string Value { get; set; }
            public static DemoStruct Instance { get; } = new DemoStruct();
        }

        [TypeConverter(typeof(DemoTypeConverter2))]
        class DemoClass1
        {
            
        }

        [TypeConverter(typeof(DemoTypeConverter2))]
        struct DemoStruct1
        {
            
        }

        #endregion

        public static IEnumerable<IDataForConversionFromString> GetDataSourceForConversionFromString()
        {
            yield return new ValidDataForConversionFromString<string> { DataValue = "", ExpectedValue = string.Empty };
            yield return new ValidDataForConversionFromString<string> { DataValue = "123", ExpectedValue = "123" };
            yield return new ValidDataForConversionFromString<bool> { DataValue = "true", ExpectedValue = true };
            yield return new ValidDataForConversionFromString<bool> { DataValue = "false", ExpectedValue = false };
            yield return new ValidDataForConversionFromString<bool> { DataValue = "tRue", ExpectedValue = true };
            yield return new ValidDataForConversionFromString<bool> { DataValue = "faLse", ExpectedValue = false };
            yield return new ValidDataForConversionFromString<int> { DataValue = "123", ExpectedValue = 123 };
            yield return new ValidDataForConversionFromString<int> { DataValue = "-123", ExpectedValue = -123 };

            yield return new ValidDataForConversionFromString<DayOfWeek> { DataValue = "Tuesday", ExpectedValue = DayOfWeek.Tuesday };
            yield return new ValidDataForConversionFromString<DayOfWeek> { DataValue = "tHuRsdAy", ExpectedValue = DayOfWeek.Thursday };
            yield return new ValidDataForConversionFromString<DayOfWeek> { DataValue = "6", ExpectedValue = DayOfWeek.Saturday };

            yield return new ValidDataForConversionFromString<float> { DataValue = "3.54", ExpectedValue = 3.54F };
            yield return new ValidDataForConversionFromString<double> { DataValue = "1234.789", ExpectedValue = 1234.789 };
            yield return new ValidDataForConversionFromString<uint> { DataValue = "42949672", ExpectedValue = 42949672 };
            yield return new ValidDataForConversionFromString<ulong> { DataValue = "429555672", ExpectedValue = 429555672 };
            yield return new ValidDataForConversionFromString<long> { DataValue = "429555672", ExpectedValue = 429555672 };
            yield return new ValidDataForConversionFromString<short> { DataValue = "32767", ExpectedValue = 32767 };
            yield return new ValidDataForConversionFromString<ushort> { DataValue = "32767", ExpectedValue = 32767 };
            yield return new ValidDataForConversionFromString<byte> { DataValue = "231", ExpectedValue = 231 };
            yield return new ValidDataForConversionFromString<sbyte> { DataValue = "-120", ExpectedValue = -120 };

            yield return new ValidDataForConversionFromString<decimal> { DataValue = "300.4", ExpectedValue = 300.4M };

            yield return new ValidDataForConversionFromString<Color> { DataValue = "Black", ExpectedValue = Color.Black };
            yield return new ValidDataForConversionFromString<Color> { DataValue = "Red", ExpectedValue = Color.Red };
            yield return new ValidDataForConversionFromString<Color> { DataValue = "Magenta", ExpectedValue = Color.Magenta };
            yield return new ValidDataForConversionFromString<Color> { DataValue = "MageNta", ExpectedValue = Color.Magenta };
            yield return new ValidDataForConversionFromString<Color> { DataValue = "0xff0000", ExpectedValue = Color.Red };
            yield return new ValidDataForConversionFromString<Color> { DataValue = "255,0,0", ExpectedValue = Color.Red };

            yield return new ValidDataForConversionFromString<DateTime> { DataValue = "03.01.1988", ExpectedValue = new DateTime(1988, 3, 1) };
            yield return new ValidDataForConversionFromString<DateTime> { DataValue = "03/01/1988", ExpectedValue = new DateTime(1988, 3, 1) };

            yield return new ValidDataForConversionFromString<TimeSpan> { DataValue = "02:14:18", ExpectedValue = new TimeSpan(2, 14, 18) };

            yield return new ValidDataForConversionFromString<DateTime> { DataValue = "03.01.1988 8:30:52 AM", ExpectedValue = new DateTime(1988, 3, 1, 8, 30, 52) };
            yield return new ValidDataForConversionFromString<DateTime> { DataValue = "03/01/1988 8:30:52 AM", ExpectedValue = new DateTime(1988, 3, 1, 8, 30, 52) };

            yield return new ValidDataForConversionFromString<Guid> { DataValue = "936DA01F-9ABD-4d9d-80C7-02AF85C822A8", ExpectedValue = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8") };


            yield return new ValidDataForConversionFromString<DemoClass> { DataValue = "returnMeANull", ExpectedValue = null };
            yield return new ValidDataForConversionFromString<DemoClass> { DataValue = "returnMeDemoClassInstance", ExpectedValue = DemoClass.Instance };
            yield return new ValidDataForConversionFromString<DemoStruct> { DataValue = "returnMeDemoStructInstance", ExpectedValue = DemoStruct.Instance };

            //===========================================================================================================================================================================================

            yield return new InvalidDataForConversionFromString<int> { DataValue = "", ExpectedExceptionType = typeof(FormatException) };

            yield return new InvalidDataForConversionFromString<Color> { DataValue = "", ExpectedExceptionType = typeof(FormatException) };
            yield return new InvalidDataForConversionFromString<Color> { DataValue = "", ExpectedExceptionType = typeof(FormatException) };
            yield return new InvalidDataForConversionFromString<Color> { DataValue = "Blue1", ExpectedExceptionType = typeof(FormatException) };

            yield return new InvalidDataForConversionFromString<DayOfWeek> { DataValue = "", ExpectedExceptionType = typeof(FormatException) };
            yield return new InvalidDataForConversionFromString<DayOfWeek> { DataValue = "1Monday", ExpectedExceptionType = typeof(FormatException) };

            yield return new InvalidDataForConversionFromString<int> { DataValue = "123a", ExpectedExceptionType = typeof(FormatException) };
            yield return new InvalidDataForConversionFromString<int> { DataValue = "123123123123123123123123123123123", ExpectedExceptionType = typeof(FormatException) };

            yield return new InvalidDataForConversionFromString<byte> { DataValue = "256", ExpectedExceptionType = typeof(FormatException) };

            yield return new InvalidDataForConversionFromString<XDocument> { DataValue = "123", ExpectedExceptionType = typeof(ArgumentException) };
            yield return new InvalidDataForConversionFromString<object> { DataValue = "123", ExpectedExceptionType = typeof(ArgumentException) };

            yield return new InvalidDataForConversionFromString<string> { DataValue = null, ExpectedExceptionType = typeof(ArgumentNullException) };

            yield return new InvalidDataForConversionFromString<DemoClass> { DataValue = "returnMeA10", ExpectedExceptionType = typeof(Exception)};
            yield return new InvalidDataForConversionFromString<DemoClass> { DataValue = "returnMeDemoStructInstance", ExpectedExceptionType = typeof(Exception)};
            yield return new InvalidDataForConversionFromString<DemoStruct> { DataValue = "returnMeANull", ExpectedExceptionType = typeof(Exception) };
            yield return new InvalidDataForConversionFromString<DemoStruct> { DataValue = "returnMeDemoClassInstance", ExpectedExceptionType = typeof(Exception) };
            yield return new InvalidDataForConversionFromString<DemoClass1> { DataValue = "something", ExpectedExceptionType = typeof(ArgumentException) };
            yield return new InvalidDataForConversionFromString<DemoStruct1> { DataValue = "something", ExpectedExceptionType = typeof(ArgumentException) };
        }

        public static IEnumerable<IDataForConversionToString> GetDataSourceForConversionToString()
        {
            yield return new ValidDataForConversionToString { DataValue = "", ExpectedValue = ""};
            yield return new ValidDataForConversionToString { DataValue = "test test", ExpectedValue = "test test" };
            yield return new ValidDataForConversionToString { DataValue = new object(), ExpectedValue = "System.Object" };
            yield return new ValidDataForConversionToString { DataValue = 1234, ExpectedValue = "1234" };
            yield return new ValidDataForConversionToString { DataValue = 0.5, ExpectedValue = "0.5" };
            yield return new ValidDataForConversionToString { DataValue = DayOfWeek.Friday, ExpectedValue = "Friday" };
            yield return new ValidDataForConversionToString { DataValue = new DemoClass{Value = "returnMeANull" }, ExpectedValue = null };
            yield return new ValidDataForConversionToString { DataValue = new DemoClass { Value = "returnMeDemoText" }, ExpectedValue = "demo" };
            yield return new ValidDataForConversionToString { DataValue = new DemoStruct{ Value = "returnMeDemoText" }, ExpectedValue = "demo" };
            //===========================================================================================================================================================================================
            yield return new InvalidDataForConversionToString { DataValue = new DemoClass { Value = "returnMeDemoStructInstance" }, ExpectedExceptionType = typeof(Exception)};
            yield return new InvalidDataForConversionToString { DataValue = null, ExpectedExceptionType = typeof(ArgumentNullException) };
            yield return new InvalidDataForConversionToString { DataValue = new DemoClass1(), ExpectedExceptionType = typeof(ArgumentException) };
            yield return new InvalidDataForConversionToString { DataValue = new DemoStruct1(), ExpectedExceptionType = typeof(ArgumentException) };
        }

        [TestCaseSource(DataSourceNameForConversionFromString)]
        public void Convert_WellFormattedStringParamValue_ExtractTheValueOfParamIntoExpectedType(IDataForConversionFromString tester)
        {
            tester.TestConvertFromStringWithGenerics();
            tester.TestConvertFromStringWithoutGenerics();
            tester.TestTryConvertFromStringWithGenerics();
            tester.TestTryConvertFromStringWithoutGenerics();
        }

        [TestCaseSource(DataSourceNameForConversionToString)]
        public void Convert_ValidParamValue_ConvertToExpectedStringType(IDataForConversionToString tester)
        {
            tester.TestConvertToString();
            tester.TestTryConvertToString();
        }
    }
}
