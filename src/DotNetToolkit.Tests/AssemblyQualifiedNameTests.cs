using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace DotNetToolkit.Tests
{
    [TestFixture]
    [Category("UnitTests")]
    public class AssemblyQualifiedNameTests
    {
        public const string DataSourceNameForGetAssemblyNameAndTypeName = "GetDataSourceForAssemblyQualifiedNames";

        class StringDataForAssemblyQualifiedNames : IAssemblyQualifiedNameData
        {
            private readonly ValidDataForAssemblyQualifiedNames _validDataForAssemblyQualifiedNames = new ValidDataForAssemblyQualifiedNames();
            private readonly InvalidDataForAssemblyQualifiedNames _invalidDataForAssemblyQualifiedNames = new InvalidDataForAssemblyQualifiedNames();
            private string _assemblyQualifiedName;
            private string _assemblyFullName;
            private string _assemblyShortName;
            private string _typeFullName;
            private Type _expectedExceptionType;

            public string AssemblyQualifiedName
            {
                get { return _assemblyQualifiedName; }
                set
                {
                    _assemblyQualifiedName = value;
                    _validDataForAssemblyQualifiedNames.AssemblyQualifiedName = value;
                    _invalidDataForAssemblyQualifiedNames.AssemblyQualifiedName = value;
                }
            }

            public string AssemblyFullName
            {
                get { return _assemblyFullName; }
                set
                {
                    _assemblyFullName = value;
                    _validDataForAssemblyQualifiedNames.AssemblyFullName = value;
                    _invalidDataForAssemblyQualifiedNames.AssemblyFullName = value;
                }
            }

            public string AssemblyShortName
            {
                get { return _assemblyShortName; }
                set
                {
                    _assemblyShortName = value;
                    _validDataForAssemblyQualifiedNames.AssemblyShortName = value;
                    _invalidDataForAssemblyQualifiedNames.AssemblyShortName = value;
                }
            }

            public string TypeFullName
            {
                get { return _typeFullName; }
                set
                {
                    _typeFullName = value;
                    _validDataForAssemblyQualifiedNames.TypeFullName = value;
                    _invalidDataForAssemblyQualifiedNames.TypeFullName = value;
                }
            }

            public Type ExpectedExceptionType
            {
                get { return _expectedExceptionType; }
                set
                {
                    _expectedExceptionType = value;
                    _invalidDataForAssemblyQualifiedNames.ExceptionType = value;
                }
            }

            public void Test()
            {
                
                if (ExpectedExceptionType == null)
                {
                    _validDataForAssemblyQualifiedNames.Test();
                }
                else
                {
                    _invalidDataForAssemblyQualifiedNames.Test();
                }
            }
        }

        class GenericDataForAssemblyQualifiedNames<TDataType> : IAssemblyQualifiedNameData
        {
            private readonly Type _dataType = typeof(TDataType);

            private readonly ValidDataForAssemblyQualifiedNames _validDataForAssemblyQualifiedNames;
            private readonly List<InvalidDataForAssemblyQualifiedNames> _invalidDataForAssemblyQualifiedNames;

            public GenericDataForAssemblyQualifiedNames()
            {
                Type argumentNullExceptionType = typeof(ArgumentNullException);
                Type argumentExceptionType = typeof(ArgumentException);

                _validDataForAssemblyQualifiedNames = new ValidDataForAssemblyQualifiedNames
                {
                    AssemblyQualifiedName = _dataType.AssemblyQualifiedName,
                    AssemblyFullName = _dataType.Assembly.FullName,
                    AssemblyShortName = _dataType.Assembly.GetName().Name,
                    TypeFullName = _dataType.FullName
                };
                _invalidDataForAssemblyQualifiedNames = new List<InvalidDataForAssemblyQualifiedNames>();
                _invalidDataForAssemblyQualifiedNames.Add(new InvalidDataForAssemblyQualifiedNames
                {
                    AssemblyQualifiedName = null,
                    AssemblyFullName = _dataType.Assembly.FullName,
                    AssemblyShortName = _dataType.Assembly.GetName().Name,
                    TypeFullName = _dataType.FullName,
                    ExceptionType = argumentNullExceptionType
                });
                _invalidDataForAssemblyQualifiedNames.Add(new InvalidDataForAssemblyQualifiedNames
                {
                    AssemblyQualifiedName = string.Empty,
                    AssemblyFullName = _dataType.Assembly.FullName,
                    AssemblyShortName = _dataType.Assembly.GetName().Name,
                    TypeFullName = _dataType.FullName,
                    ExceptionType = argumentExceptionType
                });

                _invalidDataForAssemblyQualifiedNames.Add(new InvalidDataForAssemblyQualifiedNames
                {
                    AssemblyQualifiedName = _dataType.Assembly.GetName().Name,
                    AssemblyFullName = _dataType.Assembly.FullName,
                    AssemblyShortName = _dataType.Assembly.GetName().Name,
                    TypeFullName = _dataType.FullName,
                    ExceptionType = argumentExceptionType
                });
                _invalidDataForAssemblyQualifiedNames.Add(new InvalidDataForAssemblyQualifiedNames
                {
                    AssemblyQualifiedName = _dataType.FullName,
                    AssemblyFullName = _dataType.Assembly.FullName,
                    AssemblyShortName = _dataType.Assembly.GetName().Name,
                    TypeFullName = _dataType.FullName,
                    ExceptionType = argumentExceptionType
                });

                _invalidDataForAssemblyQualifiedNames.Add(new InvalidDataForAssemblyQualifiedNames
                {
                    AssemblyQualifiedName = _dataType.Assembly.GetName().Name,
                    AssemblyFullName = _dataType.Assembly.FullName,
                    AssemblyShortName = _dataType.Assembly.GetName().Name,
                    TypeFullName = _dataType.FullName,
                    ExceptionType = argumentExceptionType
                });

                _invalidDataForAssemblyQualifiedNames.Add(new InvalidDataForAssemblyQualifiedNames
                {
                    AssemblyQualifiedName = _dataType.Assembly.FullName,
                    AssemblyFullName = _dataType.Assembly.FullName,
                    AssemblyShortName = _dataType.Assembly.GetName().Name,
                    TypeFullName = _dataType.FullName,
                    ExceptionType = argumentExceptionType
                });
            }

            public void Test()
            {
                _validDataForAssemblyQualifiedNames.Test();
                foreach (InvalidDataForAssemblyQualifiedNames invalidDataForAssemblyQualifiedName in _invalidDataForAssemblyQualifiedNames)
                {
                    invalidDataForAssemblyQualifiedName.Test();
                }
            }
        }

        class InvalidDataForAssemblyQualifiedNames : IAssemblyQualifiedNameData
        {
            public string TypeFullName { get; set; }
            public string AssemblyQualifiedName { get; set; }
            public string AssemblyFullName { get; set; }
            public string AssemblyShortName { get; set; }
            public Type ExceptionType { get; set; }

            public void Test()
            {
                Assert.IsFalse(System.Reflection.AssemblyQualifiedName.TryGetAssemblyNameAndTypeName(AssemblyQualifiedName, out _, out _));
                Assert.Throws(ExceptionType, () => System.Reflection.AssemblyQualifiedName.GetAssemblyNameAndTypeName(AssemblyQualifiedName, out _, out _));
            }
        }

        class ValidDataForAssemblyQualifiedNames: IAssemblyQualifiedNameData
        {
            public string TypeFullName { get; set; }
            public string AssemblyQualifiedName { get; set; }
            public string AssemblyFullName { get; set; }
            public string AssemblyShortName { get; set; }

            private IEnumerable<(string ValidAssemblyQualifiedName, string ParsedAssemblyName, string ParsedTypeName)> GetDataForValidAssemblyQualifiedNames()
            {
                //System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e
                yield return (AssemblyQualifiedName, AssemblyFullName, TypeFullName);
                //System.String, System.Private.CoreLib
                yield return ($"{TypeFullName}, {AssemblyShortName}", AssemblyShortName, TypeFullName);
                //System.String,System.Private.CoreLib
                yield return ($"{TypeFullName},{AssemblyShortName}", AssemblyShortName, TypeFullName);
                //System.String,     System.Private.CoreLib
                yield return ($"{TypeFullName},     {AssemblyShortName}", AssemblyShortName, TypeFullName);
            }

            public void Test()
            {
                foreach ((string ValidAssemblyQualifiedName, string ParsedAssemblyName, string ParsedTypeName) dataForValidAssemblyQualifiedName in GetDataForValidAssemblyQualifiedNames())
                {
                    System.Reflection.AssemblyQualifiedName.GetAssemblyNameAndTypeName(dataForValidAssemblyQualifiedName.ValidAssemblyQualifiedName, out AssemblyName assemblyName, out string parsedTypeName);
                    Assert.AreEqual(dataForValidAssemblyQualifiedName.ParsedAssemblyName, assemblyName.FullName);
                    Assert.AreEqual(dataForValidAssemblyQualifiedName.ParsedTypeName, parsedTypeName);

                    System.Reflection.AssemblyQualifiedName.GetAssemblyName(dataForValidAssemblyQualifiedName.ValidAssemblyQualifiedName, out assemblyName);
                    Assert.AreEqual(dataForValidAssemblyQualifiedName.ParsedAssemblyName, assemblyName.FullName);

                    System.Reflection.AssemblyQualifiedName.GetTypeName(dataForValidAssemblyQualifiedName.ValidAssemblyQualifiedName, out parsedTypeName);
                    Assert.AreEqual(dataForValidAssemblyQualifiedName.ParsedTypeName, parsedTypeName);

                    bool success = System.Reflection.AssemblyQualifiedName.TryGetAssemblyNameAndTypeName(dataForValidAssemblyQualifiedName.ValidAssemblyQualifiedName, out assemblyName, out parsedTypeName);
                    Assert.IsTrue(success);
                    Assert.AreEqual(dataForValidAssemblyQualifiedName.ParsedAssemblyName, assemblyName.FullName);
                    Assert.AreEqual(dataForValidAssemblyQualifiedName.ParsedTypeName, parsedTypeName);

                    success = System.Reflection.AssemblyQualifiedName.TryGetAssemblyName(dataForValidAssemblyQualifiedName.ValidAssemblyQualifiedName, out assemblyName);
                    Assert.IsTrue(success);
                    Assert.AreEqual(dataForValidAssemblyQualifiedName.ParsedAssemblyName, assemblyName.FullName);

                    success = System.Reflection.AssemblyQualifiedName.TryGetTypeName(dataForValidAssemblyQualifiedName.ValidAssemblyQualifiedName, out parsedTypeName);
                    Assert.IsTrue(success);
                    Assert.AreEqual(dataForValidAssemblyQualifiedName.ParsedTypeName, parsedTypeName);
                }
            }
        }

        public static IEnumerable<IAssemblyQualifiedNameData> GetDataSourceForAssemblyQualifiedNames()
        {
            Type argumentNullExceptionType = typeof(ArgumentNullException);
            Type argumentExceptionType = typeof(ArgumentException);

            yield return new GenericDataForAssemblyQualifiedNames<string>();
            yield return new GenericDataForAssemblyQualifiedNames<int>();
            yield return new GenericDataForAssemblyQualifiedNames<bool>();
            yield return new GenericDataForAssemblyQualifiedNames<Assembly>();
            yield return new GenericDataForAssemblyQualifiedNames<List<string>>();
            yield return new GenericDataForAssemblyQualifiedNames<List<List<List<string>>>>();
            yield return new GenericDataForAssemblyQualifiedNames<KeyValuePair<List<string>, KeyValuePair<List<string>, List<string>>>>();
            yield return new GenericDataForAssemblyQualifiedNames<string[]>();
            yield return new GenericDataForAssemblyQualifiedNames<object[]>();
            yield return new GenericDataForAssemblyQualifiedNames<object[,]>();
            yield return new GenericDataForAssemblyQualifiedNames<object[][]>();
            yield return new GenericDataForAssemblyQualifiedNames<object[,][,]>();
            yield return new GenericDataForAssemblyQualifiedNames<List<object[]>>();
            yield return new GenericDataForAssemblyQualifiedNames<List<string[,]>>();
            yield return new StringDataForAssemblyQualifiedNames
            {
                AssemblyQualifiedName = "System.Test, System.Private.Test, Version=6.7.0.0, Culture=neutral, PublicKeyToken=7cec80d7bea7798e", 
                AssemblyFullName = "System.Private.Test, Version=6.7.0.0, Culture=neutral, PublicKeyToken=7cec80d7bea7798e",
                AssemblyShortName = "System.Private.Test", 
                TypeFullName = "System.Test"
            };

            yield return new StringDataForAssemblyQualifiedNames
            {
                AssemblyQualifiedName = "System.Test, System.Private.Test, Version=6.7.0.0, PublicKeyToken=7cec80d7bea7798e",
                AssemblyFullName = "System.Private.Test, Version=6.7.0.0, PublicKeyToken=7cec80d7bea7798e",
                AssemblyShortName = "System.Private.Test1",
                TypeFullName = "System.Test"
            };

            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = null, ExpectedExceptionType = argumentNullExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = string.Empty, ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = ",", ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = " ,", ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = " , ", ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = " , , , ,", ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = "Sysyte.Xxx , test, sss", ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = "Sysyte.Xxx , test,  ", ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = "Sysyte.Xxx, Sysyte.Xxx,", ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = "System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = "System.Str, System.Private.CLib, Culture=neutral, PublicKeyToken=7cec85d7bea7798e, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = "System.Str, System.Private.CLib, PublicKeyToken=7cec85d7bea7798e, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", ExpectedExceptionType = argumentExceptionType };
            yield return new StringDataForAssemblyQualifiedNames { AssemblyQualifiedName = "System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e, Test", ExpectedExceptionType = argumentExceptionType };

        }

        public interface IAssemblyQualifiedNameData
        {
            void Test();
        }

        [TestCaseSource(DataSourceNameForGetAssemblyNameAndTypeName)]
        public void GetAssemblyNameAndTypeName_FromMultipleValidQualifiedNames_ShouldReturnExpectedResult(IAssemblyQualifiedNameData assemblyQualifiedNameData)
        {
            assemblyQualifiedNameData.Test();
        }
    }
}