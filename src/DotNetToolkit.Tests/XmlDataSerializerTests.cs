using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using DotNetToolkit.Tests.Properties;
using NUnit.Framework;

namespace DotNetToolkit.Tests
{
    class MySerializableEntityBase:MarshalByRefObject
    {
        public MySerializableEntityBase(string baseValue, string baseValue4)
        {
            BaseValue = baseValue;
            BaseValue4 = baseValue;
        }

        public string BaseValue { get; }

        public string BaseValue1 { get; set; }

        public string BaseValue4 { get; set; }
    }

    interface IMySerializableEntity2
    {
        int Value1 { get; set; }
    }

    class MySerializableEntity2: IMySerializableEntity2
    {
        public int Value1 { get; set; }
    }

    class MySerializableEntity:MySerializableEntityBase
    {
        public MySerializableEntity(string value20, string value21):base(value20,null)
        {
            Value20 = value20;
            Value21 = value21;
            Value22 = new List<string>();
        }

        public int ShouldNotBeSerialized { get; set; }

        public string Value0 { get; set; }

        public int Value1 { get; set; }

        public int? Value2 { get; set; }

        public int? Value3 { get; set; }

        public IEnumerable<int> Value4 { get; set; }

        public List<string> Value5 { get; set; }

        public IEnumerable Value6 { get; set; }

        public IList<string> Value7 { get; set; }

        public object[] Value8 { get; set; }

        public object[] Value9 { get; set; }

        public string Value10 { get; set; }

        public ArrayList Value11 { get; set; }

        public IList<IReadOnlyCollection<List<IEnumerable<object[]>>>> Value12 { get; set; }

        public HashSet<int> Value13 { get; set; }

        public Collection<int> Value14 { get; set; }

        public Queue<int> Value15 { get; set; }

        public Stack<int> Value16 { get; set; }

        public ReadOnlyCollection<int> Value17 { get; set; }

        public string Value18 { get; set; }

        public int Value19 { get; set; }

        public string Value20 { get; }

        public string Value21 { get; set; }

        public IList<string> Value22 { get; }

        public Array Value23 { get; set; }

        public int Value24 { get; set; }
        public int Value25 { get; set; }
        public int Value26 { get; set; }
        public int Value27 { get; set; }

        public int Value28 { get; set; }
        public int Value29 { get; set; }
        public int Value30 { get; set; }
        public int Value31 { get; set; }

        public MySerializableEntity Child { get; set; }

        public MySerializableEntity2 Value32 { get; set; }
    }



    [TestFixture]
    [Category("UnitTests")]
    public class XmlDataSerializerTests
    {
        private XmlDataModel _xmlModelTypeBuilder;

        [OneTimeSetUp]
        public void InitializeSerializer()
        {
            XmlDataModel modelTypeBuilder = new XmlDataModel();

            

            modelTypeBuilder.HasKnownEntity<MySerializableEntity>()
                .HasName("MyCustomEntity1")
                .Constructor()
                    .Parameter(0).HasAssociatedProperty(entity => entity.Value20)
                    .Parameter(1).HasAssociatedProperty(entity => entity.Value21)
                .Property(entity => entity.Value0).IsMappedAsXmlElement()
                .Property(entity => entity.Value1).IsMappedAsXmlElement()
                .Property(entity => entity.Value2).IsMappedAsXmlElement()
                .Property(entity => entity.Value3).IsMappedAsXmlElement().HasName("MyCustomValue3")
                .Property(entity => entity.Value4).IsMappedAsXmlElement().HasName("MyCustomValue4")
                .Property(entity => entity.Value5).IsMappedAsXmlElement()
                .Property(entity => entity.Value6).IsMappedAsXmlElement().HasName("MyCustomColectionValue6")
                .Property(entity => entity.Value7).IsMappedAsXmlElement()
                .Property(entity => entity.Value8).IsMappedAsXmlElement()
                .Property(entity => entity.Value9).IsMappedAsXmlElement()
                .Property(entity => entity.Value10).IsMappedAsXmlElement()
                .Property(entity => entity.Value11).IsMappedAsXmlElement()
                .Property(entity => entity.Value12).IsMappedAsXmlElement()
                .Property(entity => entity.Value13).IsMappedAsXmlElement()
                .Property(entity => entity.Value14).IsMappedAsXmlElement()
                .Property(entity => entity.Value15).IsMappedAsXmlElement()
                .Property(entity => entity.Value16).IsMappedAsXmlElement()
                .Property(entity => entity.Value17).IsMappedAsXmlElement()
                .Property(entity => entity.Value18).IsMappedAsXmlAttribute()
                .Property(entity => entity.Value19).IsMappedAsXmlAttribute()
                .Property(entity => entity.Value20).IsMappedAsXmlAttribute()
                .Property(entity => entity.Value21).IsMappedAsXmlElement()
                .Property(entity => entity.Value22).IsMappedAsXmlElement()
                .Property(entity => entity.Value23).IsMappedAsXmlElement()

                .Property(entity => entity.Value24).IsMappedAsXmlElement()
                .Property(entity => entity.Value25).IsMappedAsXmlElement().ShouldNotBeSerializedIfHasDefaultValue()
                .Property(entity => entity.Value26).IsMappedAsXmlAttribute()
                .Property(entity => entity.Value27).IsMappedAsXmlAttribute().ShouldNotBeSerializedIfHasDefaultValue()

                .Property(entity => entity.Value28).IsMappedAsXmlElement()
                .Property(entity => entity.Value29).IsMappedAsXmlElement().ShouldNotBeSerializedIfHasDefaultValue()
                .Property(entity => entity.Value30).IsMappedAsXmlAttribute()
                .Property(entity => entity.Value31).IsMappedAsXmlAttribute().ShouldNotBeSerializedIfHasDefaultValue()
                .Property(entity => entity.Value32).IsMappedAsXmlElement()

                .Property(entity => entity.Child).IsMappedAsXmlElement()
                .Property(entity => entity.BaseValue).IsMappedAsXmlElement()
                .Property(entity => entity.BaseValue1).IsMappedAsXmlElement()
                .Property(entity => entity.BaseValue4).IsMappedAsXmlElement();
            modelTypeBuilder.HasKnownEntity<MySerializableEntityBase>()
                .HasName("MyCustomEntity0")
                .Constructor()
                    .Parameter(0).HasAssociatedProperty(entity => entity.BaseValue)
                    .Parameter(1).HasAssociatedProperty(entity => entity.BaseValue4)
                    .Parameter(1).AssociatedPropertyValueIsSetBySerializer()
                .Property(entity => entity.BaseValue).IsMappedAsXmlElement()
                .Property(entity => entity.BaseValue1).IsMappedAsXmlElement()
                .Property(entity => entity.BaseValue4).IsMappedAsXmlElement();

            modelTypeBuilder.HasKnownEntity<MySerializableEntity2>()
                .Property(entity => entity.Value1).IsMappedAsXmlElement();

            _xmlModelTypeBuilder = modelTypeBuilder;
        }


        [Test]
        public void Serialize_MySerializableEntityFilledWithValidData_ShouldBeEqualsWithXmlContentFrom_MySerializableEntityTest1Data()
        {
            MySerializableEntity mySerializableEntityChild = new MySerializableEntity("Ctor argument11", "Ctor argument22") ;
            mySerializableEntityChild.Value0 = "Value00";
            mySerializableEntityChild.Value2 = 2;
            mySerializableEntityChild.Value19 = 4;
            mySerializableEntityChild.Value18 = "This is my attr value";
            mySerializableEntityChild.BaseValue1 = "base Value11";
            mySerializableEntityChild.Value28 = 28;
            mySerializableEntityChild.Value29 = 29;
            mySerializableEntityChild.Value30 = 30;
            mySerializableEntityChild.Value31 = 31;
            mySerializableEntityChild.Value32 = new MySerializableEntity2 {Value1 = 10};

            MySerializableEntity mySerializableEntityChildChild = new MySerializableEntity("Ctor argument111", "Ctor argument222");
            mySerializableEntityChildChild.Value0 = "Value000";
            mySerializableEntityChildChild.Value2 = 2;
            mySerializableEntityChildChild.Value10 = string.Empty;
            mySerializableEntityChildChild.BaseValue1 = "base Value111";
            mySerializableEntityChildChild.Value28 = 28;
            mySerializableEntityChildChild.Value29 = 29;
            mySerializableEntityChildChild.Value30 = 30;
            mySerializableEntityChildChild.Value31 = 31;
            mySerializableEntityChild.Value32 = new MySerializableEntity2 { Value1 = 11 };

            mySerializableEntityChild.Child = mySerializableEntityChildChild;


            MySerializableEntity2 mySerializableEntity2 = new MySerializableEntity2 { Value1 = 20 };


            MySerializableEntity mySerializableEntity = new MySerializableEntity("Ctor argument1", "Ctor argument2");
            mySerializableEntity.Value0 = "Value0";
            mySerializableEntity.Value1 = 1;
            mySerializableEntity.Value2 = 2;
            mySerializableEntity.Value3 = 3;
            mySerializableEntity.Value4 = new[] {1, 2, 3, 4};
            mySerializableEntity.Value5 = new List<string>
            {
                "11 11",
                "22 22"
            };

            

            mySerializableEntity.Value6 = new object[] { 1, 0.5, mySerializableEntityChild, 33, mySerializableEntity.Value4, new object[] { 1, "test", 3, new object[] { "test", 2, 3, new List<int>{10,13,14},  4 }, 4 }, "test", mySerializableEntity2 };
            mySerializableEntity.Value7 = new List<string>
            {
                "33 44",
                "55 66"
            };

            mySerializableEntity.Value8 = new object[] { 1, 0.5, 33, mySerializableEntityChild,  "test"};
            mySerializableEntity.Value9 = new object[4];
            mySerializableEntity.Value9[2] = mySerializableEntityChild;

            mySerializableEntity.Value10 = string.Empty;
            mySerializableEntity.Child = mySerializableEntityChild;

            mySerializableEntity.Value11 = new ArrayList{10,"Test"};

            mySerializableEntity.Value12 = new List<IReadOnlyCollection<List<IEnumerable<object[]>>>>();
            var complexList = new List<IReadOnlyCollection<List<IEnumerable<object[]>>>>();

            var subList = new List<List<IEnumerable<object[]>>> ();
            var subSubList = new List<IEnumerable<object[]>>();
            var subSubSubList = new List<object[]>();
            

            object[] values = {"aaa","bbb", 3};

            subSubSubList.Add(values);
            subSubSubList.Add(values);


            subSubList.Add(subSubSubList);
            subSubList.Add(new List<object[]>());
            subSubList.Add(new List<object[]>());

            subList.Add(subSubList);
            subList.Add(subSubList);

            complexList.Add(subList);
            complexList.Add(subList);
            mySerializableEntity.Value12 = complexList;

            mySerializableEntity.Value13 = new HashSet<int>{21,31};
            mySerializableEntity.Value14 = new Collection<int>{41,51};
            mySerializableEntity.Value15 = new Queue<int>(new []{61,71});
            mySerializableEntity.Value16 = new Stack<int>(new[] { 91, 81 });
            mySerializableEntity.Value17 = new List<int>(new[] { 100, 101} ).AsReadOnly();
            mySerializableEntity.Value18 = "This is an attribute value";
            mySerializableEntity.Value19 = 10;

            mySerializableEntity.Value22.Add("test1");
            mySerializableEntity.Value22.Add("test2");
            mySerializableEntity.Value22.Add("test3");
            mySerializableEntity.Value23 = new[] {"test1", "test2"};

            mySerializableEntity.Value28 = 28;
            mySerializableEntity.Value29 = 29;
            mySerializableEntity.Value30 = 30;
            mySerializableEntity.Value31 = 31;


            mySerializableEntity.BaseValue1 = "base Value1";

            mySerializableEntity.Value32 = mySerializableEntity2;


            XmlDataSerializer xmlSerializer = new XmlDataSerializer(_xmlModelTypeBuilder);
            XDocument serializedInstance = xmlSerializer.Serialize(mySerializableEntity);

            string xmlOutput = serializedInstance.ToString();
            Assert.AreEqual(Resources.MySerializableEntityTest1Data, xmlOutput);
        }

        [Test]
        public void Deserialize_ContentFrom_MySerializableEntityTest1Data_ShouldDeserializeToExpectedObject()
        {
            XDocument serializedEntity = XDocument.Parse(Resources.MySerializableEntityTest1Data);
            XmlDataSerializer xmlSerializer = new XmlDataSerializer(_xmlModelTypeBuilder);

            //xmlSerializer.ResolverTypes = new[] {typeof(MySerializableEntity), typeof(int), typeof(string), typeof(double)};
            MySerializableEntity deserializedEntity = xmlSerializer.Deserialize<MySerializableEntity>(serializedEntity);

            void ValidateChild(MySerializableEntity child)
            {
                Assert.IsNotNull(child);
                Assert.That(child.Value0, Is.EqualTo("Value00"));
                Assert.That(child.Value1, Is.EqualTo(0));
                Assert.That(child.Value2, Is.EqualTo(2));
                Assert.That(child.Value3, Is.EqualTo(null));
                Assert.That(child.Value4, Is.EqualTo(null));
                Assert.That(child.Value5, Is.EqualTo(null));
                Assert.That(child.Value6, Is.EqualTo(null));
                Assert.That(child.Value7, Is.EqualTo(null));
                Assert.That(child.Value8, Is.EqualTo(null));
                Assert.That(child.Value9, Is.EqualTo(null));
                Assert.That(child.Value10, Is.EqualTo(null));
                Assert.That(child.Value18, Is.EqualTo("This is my attr value"));
                Assert.That(child.Value19, Is.EqualTo(4));
                Assert.That(child.Value20, Is.EqualTo("Ctor argument11"));
                Assert.That(child.Value21, Is.EqualTo("Ctor argument22"));
                Assert.That(child.Value32.Value1, Is.EqualTo(11));

                Assert.IsNotNull(child.Child);
                Assert.That(child.Child.Value0, Is.EqualTo("Value000"));
                Assert.That(child.Child.Value1, Is.EqualTo(0));
                Assert.That(child.Child.Value2, Is.EqualTo(2));
                Assert.That(child.Child.Value3, Is.EqualTo(null));
                Assert.That(child.Child.Value4, Is.EqualTo(null));
                Assert.That(child.Child.Value5, Is.EqualTo(null));
                Assert.That(child.Child.Value6, Is.EqualTo(null));
                Assert.That(child.Child.Value7, Is.EqualTo(null));
                Assert.That(child.Child.Value8, Is.EqualTo(null));
                Assert.That(child.Child.Value9, Is.EqualTo(null));
                Assert.That(child.Child.Value10, Is.EqualTo(string.Empty));
                Assert.That(child.Child.Value20, Is.EqualTo("Ctor argument111"));
                Assert.That(child.Child.Value21, Is.EqualTo("Ctor argument222"));
                Assert.That(child.Child.Value32, Is.EqualTo(null));
            }

            Assert.That(deserializedEntity.ShouldNotBeSerialized, Is.EqualTo(0));

            Assert.IsNotNull(deserializedEntity);
            Assert.That(deserializedEntity.Value0, Is.EqualTo("Value0"));
            Assert.That(deserializedEntity.Value1, Is.EqualTo(1));
            Assert.That(deserializedEntity.Value2, Is.EqualTo(2));
            Assert.That(deserializedEntity.Value3, Is.EqualTo(3));

            Assert.IsNotNull(deserializedEntity.Value4);
            Assert.That(deserializedEntity.Value4.GetType(), Is.EqualTo(typeof(List<int>)));
            Assert.That(deserializedEntity.Value4.Count(), Is.EqualTo(4));
            Assert.That(deserializedEntity.Value4.ElementAt(0), Is.EqualTo(1));
            Assert.That(deserializedEntity.Value4.ElementAt(1), Is.EqualTo(2));
            Assert.That(deserializedEntity.Value4.ElementAt(2), Is.EqualTo(3));
            Assert.That(deserializedEntity.Value4.ElementAt(3), Is.EqualTo(4));

            Assert.IsNotNull(deserializedEntity.Value5);
            Assert.That(deserializedEntity.Value5.Count, Is.EqualTo(2));
            Assert.That(deserializedEntity.Value5.GetType(), Is.EqualTo(typeof(List<string>)));
            Assert.That(deserializedEntity.Value5[0], Is.EqualTo("11 11"));
            Assert.That(deserializedEntity.Value5[1], Is.EqualTo("22 22"));

            Assert.IsNotNull(deserializedEntity.Value6);
            Assert.That(deserializedEntity.Value6.GetType(), Is.EqualTo(typeof(ArrayList)));
            Assert.That(deserializedEntity.Value6.ToGenericEnumerable().Count(), Is.EqualTo(8));
            Assert.That(deserializedEntity.Value6.ToGenericEnumerable().ElementAt(0), Is.EqualTo(1));
            Assert.That(deserializedEntity.Value6.ToGenericEnumerable().ElementAt(1), Is.EqualTo(0.5));
            ValidateChild(deserializedEntity.Value6.ToGenericEnumerable().ElementAt(2) as MySerializableEntity);
            Assert.That(deserializedEntity.Value6.ToGenericEnumerable().ElementAt(3), Is.EqualTo(33));

            IEnumerable<object> fourthElementOfValue6 = ((IEnumerable)deserializedEntity.Value6.ToGenericEnumerable().ElementAt(4)).ToGenericEnumerable();
            Assert.IsNotNull(fourthElementOfValue6);
            Assert.That(fourthElementOfValue6.Count(), Is.EqualTo(4));
            Assert.That(fourthElementOfValue6.ElementAt(0), Is.EqualTo(1));
            Assert.That(fourthElementOfValue6.ElementAt(1), Is.EqualTo(2));
            Assert.That(fourthElementOfValue6.ElementAt(2), Is.EqualTo(3));
            Assert.That(fourthElementOfValue6.ElementAt(3), Is.EqualTo(4));

            IEnumerable<object> fifthElementOfValue6 = ((IEnumerable)deserializedEntity.Value6.ToGenericEnumerable().ElementAt(5)).ToGenericEnumerable();
            Assert.IsNotNull(fifthElementOfValue6);
            Assert.That(fifthElementOfValue6.Count(), Is.EqualTo(5));
            Assert.That(fifthElementOfValue6.ElementAt(0), Is.EqualTo(1));
            Assert.That(fifthElementOfValue6.ElementAt(1), Is.EqualTo("test"));
            Assert.That(fifthElementOfValue6.ElementAt(2), Is.EqualTo(3));
            IEnumerable<object> thirdElementOfFifthElementOfValue6 = ((IEnumerable)fifthElementOfValue6.ElementAt(3)).ToGenericEnumerable();
            Assert.IsNotNull(thirdElementOfFifthElementOfValue6);
            Assert.That(thirdElementOfFifthElementOfValue6.Count(), Is.EqualTo(5));
            Assert.That(thirdElementOfFifthElementOfValue6.ElementAt(0), Is.EqualTo("test"));
            Assert.That(thirdElementOfFifthElementOfValue6.ElementAt(1), Is.EqualTo(2));
            Assert.That(thirdElementOfFifthElementOfValue6.ElementAt(2), Is.EqualTo(3));
            IEnumerable<object> fourthElementOfThirdElementOfFifthElementOfValue6 = ((IEnumerable)thirdElementOfFifthElementOfValue6.ElementAt(3)).ToGenericEnumerable();
            Assert.IsNotNull(fourthElementOfThirdElementOfFifthElementOfValue6);
            Assert.That(fourthElementOfThirdElementOfFifthElementOfValue6.Count(), Is.EqualTo(3));
            Assert.That(fourthElementOfThirdElementOfFifthElementOfValue6.ElementAt(0), Is.EqualTo(10));
            Assert.That(fourthElementOfThirdElementOfFifthElementOfValue6.ElementAt(1), Is.EqualTo(13));
            Assert.That(fourthElementOfThirdElementOfFifthElementOfValue6.ElementAt(2), Is.EqualTo(14));
            Assert.That(thirdElementOfFifthElementOfValue6.ElementAt(4), Is.EqualTo(4));
            Assert.That(fifthElementOfValue6.ElementAt(4), Is.EqualTo(4));

            Assert.That(deserializedEntity.Value6.ToGenericEnumerable().ElementAt(6), Is.EqualTo("test"));

            Assert.That(deserializedEntity.Value6.ToGenericEnumerable().ElementAt(7).GetType(), Is.EqualTo(typeof(MySerializableEntity2)));
            Assert.That(((MySerializableEntity2)deserializedEntity.Value6.ToGenericEnumerable().ElementAt(7)).Value1, Is.EqualTo(20));

            Assert.IsNotNull(deserializedEntity.Value7);
            Assert.That(deserializedEntity.Value7.GetType(), Is.EqualTo(typeof(List<string>)));
            Assert.That(deserializedEntity.Value7.Count, Is.EqualTo(2));
            Assert.That(deserializedEntity.Value7[0], Is.EqualTo("33 44"));
            Assert.That(deserializedEntity.Value7[1], Is.EqualTo("55 66"));

            Assert.IsNotNull(deserializedEntity.Value8);
            Assert.That(deserializedEntity.Value8.GetType(), Is.EqualTo(typeof(object[])));
            Assert.That(deserializedEntity.Value8.Length, Is.EqualTo(5));
            Assert.That(deserializedEntity.Value8[0], Is.EqualTo(1));
            Assert.That(deserializedEntity.Value8[1], Is.EqualTo(0.5));
            Assert.That(deserializedEntity.Value8[2], Is.EqualTo(33));
            ValidateChild(deserializedEntity.Value8[3] as MySerializableEntity);
            Assert.That(deserializedEntity.Value8[4], Is.EqualTo("test"));

            Assert.IsNotNull(deserializedEntity.Value9);
            Assert.That(deserializedEntity.Value9.Length, Is.EqualTo(1));
            ValidateChild(deserializedEntity.Value9[0] as MySerializableEntity);

            Assert.That(deserializedEntity.Value10, Is.EqualTo(string.Empty));

            Assert.IsNotNull(deserializedEntity.Value11);
            Assert.That(deserializedEntity.Value11.GetType(), Is.EqualTo(typeof(ArrayList)));
            Assert.That(deserializedEntity.Value11.Count, Is.EqualTo(2));
            Assert.That(deserializedEntity.Value11[0], Is.EqualTo(10));
            Assert.That(deserializedEntity.Value11[1], Is.EqualTo("Test"));

            Assert.That(deserializedEntity.Value12.GetType(), Is.EqualTo(typeof(List<IReadOnlyCollection<List<IEnumerable<object[]>>>>)));
            Assert.That(deserializedEntity.Value12[0].GetType(), Is.EqualTo(typeof(List<List<IEnumerable<object[]>>>)));
            Assert.That(deserializedEntity.Value12[0].ElementAt(0).GetType(), Is.EqualTo(typeof(List<IEnumerable<object[]>>)));
            Assert.That(deserializedEntity.Value12[0].ElementAt(0)[0].GetType(), Is.EqualTo(typeof(List<object[]>)));
            Assert.That(deserializedEntity.Value12[0].ElementAt(0)[0].ElementAt(0).GetType(), Is.EqualTo(typeof(object[])));

            Assert.That(deserializedEntity.Value12.Count, Is.EqualTo(2));
            Assert.That(deserializedEntity.Value12[0].ElementAt(0).Count, Is.EqualTo(1));
            Assert.That(deserializedEntity.Value12[0].ElementAt(0)[0].Count, Is.EqualTo(2));
            Assert.That(deserializedEntity.Value12[0].ElementAt(0)[0].ElementAt(0).Count, Is.EqualTo(3));

            Assert.That(deserializedEntity.Value12[0].ElementAt(1).Count, Is.EqualTo(1));
            Assert.That(deserializedEntity.Value12[0].ElementAt(1)[0].Count, Is.EqualTo(2));
            Assert.That(deserializedEntity.Value12[0].ElementAt(1)[0].ElementAt(0).Count, Is.EqualTo(3));

            Assert.That(deserializedEntity.Value12[1].ElementAt(0).Count, Is.EqualTo(1));
            Assert.That(deserializedEntity.Value12[1].ElementAt(0)[0].Count, Is.EqualTo(2));
            Assert.That(deserializedEntity.Value12[1].ElementAt(0)[0].ElementAt(0).Count, Is.EqualTo(3));


            Assert.That(deserializedEntity.Value12[0].Count, Is.EqualTo(2));

            Assert.That(deserializedEntity.Value12[0].ElementAt(0)[0].ElementAt(0)[0], Is.EqualTo("aaa"));
            Assert.That(deserializedEntity.Value12[0].ElementAt(0)[0].ElementAt(0)[1], Is.EqualTo("bbb"));

            Assert.That(deserializedEntity.Value13.Count, Is.EqualTo(2));

            Assert.That(deserializedEntity.Value18, Is.EqualTo("This is an attribute value"));
            Assert.That(deserializedEntity.Value19, Is.EqualTo(10));

            Assert.That(deserializedEntity.Value20, Is.EqualTo("Ctor argument1"));
            Assert.That(deserializedEntity.Value21, Is.EqualTo("Ctor argument2"));

            Assert.That(deserializedEntity.Value22.Count, Is.EqualTo(3));
            Assert.That(deserializedEntity.Value22[0], Is.EqualTo("test1"));
            Assert.That(deserializedEntity.Value22[1], Is.EqualTo("test2"));
            Assert.That(deserializedEntity.Value22[2], Is.EqualTo("test3"));

            Assert.That(deserializedEntity.Value23.Length, Is.EqualTo(2));
            Assert.That(deserializedEntity.Value23.GetValue(0), Is.EqualTo("test1"));
            Assert.That(deserializedEntity.Value23.GetValue(1), Is.EqualTo("test2"));

            Assert.That(deserializedEntity.BaseValue, Is.EqualTo(deserializedEntity.Value20));
            Assert.That(deserializedEntity.BaseValue1, Is.EqualTo("base Value1"));
            Assert.That(deserializedEntity.BaseValue4, Is.EqualTo("Ctor argument1"));

            Assert.That(deserializedEntity.Value24, Is.EqualTo(0));
            Assert.That(deserializedEntity.Value25, Is.EqualTo(0));
            Assert.That(deserializedEntity.Value26, Is.EqualTo(0));
            Assert.That(deserializedEntity.Value27, Is.EqualTo(0));

            Assert.That(deserializedEntity.Value28, Is.EqualTo(28));
            Assert.That(deserializedEntity.Value29, Is.EqualTo(29));
            Assert.That(deserializedEntity.Value30, Is.EqualTo(30));
            Assert.That(deserializedEntity.Value31, Is.EqualTo(31));

            Assert.That(deserializedEntity.Value32.Value1, Is.EqualTo(20));

            ValidateChild(deserializedEntity.Child);
        }
    }
}