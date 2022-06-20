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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace System.Xml.Serialization
{
    /// <summary>Serializes and deserializes objects into and from XML data format.</summary>
    public sealed class XmlDataSerializer
    {
        private static readonly MethodInfo _valueIsEqualWithDefaultValueMethodInfo = typeof(XmlDataSerializer).GetMethod(nameof(ValueIsEqualWithDefaultValue), BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>Initializes a new instance of the <see cref="XmlDataSerializer"/> class.</summary>
        public XmlDataSerializer() { }

        /// <summary>Initializes a new instance of the <see cref="XmlDataSerializer"/> class.</summary>
        /// <param name="xmlDataModel">
        /// The xml data model that allows to define the shape of xml entities,
        /// the relation between them and how they are mapped during XML serialization process.
        /// </param>
        public XmlDataSerializer(XmlDataModel xmlDataModel)
        {
            XmlDataModel = xmlDataModel;
        }

        /// <summary>
        /// The xml data model that allows to define the shape of xml entities,
        /// the relation between them and how they are mapped during XML serialization process.
        /// </summary>
        public XmlDataModel XmlDataModel { get; set; }


        /// <summary>
        /// Serializes the specified <paramref name="instanceToBeSerialized"/> into an <see cref="XDocument"/> document.
        /// </summary>
        /// <param name="instanceToBeSerialized">The System.Object to serialize.</param>
        /// <returns>The serialized object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="instanceToBeSerialized"/> is null.</exception>
        public XDocument Serialize(object instanceToBeSerialized)
        {
            if (instanceToBeSerialized == null)
            {
                throw new ArgumentNullException(nameof(instanceToBeSerialized));
            }

            ThrowIfModelBuilderIsNotDefined();

            Type instanceToBeSerializedType = instanceToBeSerialized.GetType();

            string rootEntityName = instanceToBeSerializedType.Name;

            if (XmlDataModel.XmlModelInfo.EntitiesInfoKeyedByType.TryGetValue(instanceToBeSerializedType, out XmlEntityTypeInfo xmlEntityTypeInfo))
            {
                if (!string.IsNullOrWhiteSpace(xmlEntityTypeInfo.XmlName))
                {
                    rootEntityName = xmlEntityTypeInfo.XmlName;
                }
            }
            else
            {
                string errorMessage = $"The {instanceToBeSerializedType.FullName} type is unknown. Please check model builder.";
                throw new SerializationException(errorMessage);
            }

            XElement root = SerializeObject(instanceToBeSerialized, rootEntityName);
            return new XDocument(root);
        }


        /// <summary>
        /// Deserializes the XML document contained by the specified <see cref="XElement"/> element.
        /// </summary>
        /// <param name="serializedObject">The xml element to be deserialized.</param>
        /// <returns>The <see cref="object"/> being deserialized.</returns>
        public object Deserialize(XDocument serializedObject)
        {
            ThrowIfModelBuilderIsNotDefined();
            if (serializedObject == null)
            {
                throw new ArgumentNullException(nameof(serializedObject));
            }

            if (!XmlDataModel.XmlModelInfo.SerializationTypesKeyedByName.TryGetValue(serializedObject.Root.Name.LocalName, out Type serializedObjectType))
            {
                string errorMessage = $"Error occurred during xml deserialization. The {serializedObject.Root.Name.LocalName} type cannot be resolved!";
                throw new SerializationException(errorMessage);
            }
            return DeserializeObject(serializedObject.Root, serializedObjectType);
        }


        /// <summary>
        /// Deserializes the XML document contained by the specified <see cref="XElement"/> element.
        /// </summary>
        /// <typeparam name="TDeserializedObject">The type of the object being deserialized.</typeparam>
        /// <param name="serializedObject">The xml element to be deserialized.</param>
        /// <returns>The <typeparamref name="TDeserializedObject"/> being deserialized.</returns>
        public TDeserializedObject Deserialize<TDeserializedObject>(XDocument serializedObject)
        {
            object deserializedObject = Deserialize(serializedObject);
            if (deserializedObject is TDeserializedObject deserializedObjectToReturn)
            {
                return deserializedObjectToReturn;
            }
            throw new SerializationException($"Cannot implicitly convert deserialized object type {deserializedObject.GetType().FullName} to specified {typeof(TDeserializedObject).FullName} type.");
        }


        /// <summary>
        /// Serializes the specified <paramref name="instanceToBeSerialized"/> into an XML document file.
        /// </summary>
        /// <param name="instanceToBeSerialized">The System.Object to serialize.</param>
        /// <param name="fileName">The file to write to.</param>
        public void SerializeToFile(object instanceToBeSerialized, string fileName)
        {
            XDocument serializedObject = Serialize(instanceToBeSerialized);
            serializedObject.Save(fileName);
        }


        /// <summary>
        /// Deserializes the XML document contained by the specified xml file.
        /// </summary>
        /// <typeparam name="TDeserializedObject">The type of the object being deserialized.</typeparam>
        /// <param name="serializedObjectPath">The path to the xml element to be deserialized.</param>
        /// <returns>The <typeparamref name="TDeserializedObject"/> being deserialized.</returns>
        public TDeserializedObject DeserializeFromFile<TDeserializedObject>(string serializedObjectPath)
        {
            if (string.IsNullOrWhiteSpace(serializedObjectPath))
            {
                throw new ArgumentException($"{nameof(serializedObjectPath)} cannot be null or empty.");
            }
            XDocument serializedDocument = XDocument.Load(serializedObjectPath);
            return Deserialize<TDeserializedObject>(serializedDocument);
        }

        /// <summary>
        /// Deserializes the XML document contained by the specified xml file.
        /// </summary>
        /// <param name="serializedObjectPath">The path to the xml element to be deserialized.</param>
        /// <returns>The <see cref="object"/> being deserialized.</returns>
        public object DeserializeFromFile(string serializedObjectPath)
        {
            if (string.IsNullOrWhiteSpace(serializedObjectPath))
            {
                throw new ArgumentException($"{nameof(serializedObjectPath)} cannot be null or empty.");
            }
            XDocument serializedDocument = XDocument.Load(serializedObjectPath);
            return Deserialize(serializedDocument);
        }

        /// <summary>
        /// Verifies that the name is a valid name according to the W3C Extended Markup Language recommendation.
        /// </summary>
        /// <param name="nodeName">Name of the node.</param>
        /// <returns><c>True </c> if the node name is valid.</returns>
        public static bool XmlNameIsValid(string nodeName)
        {
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                return false;
            }
            else
            {
                try
                {
                    XmlConvert.VerifyName(nodeName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private static Type GetCollectionItemType(Type collectionType)
        {
            if (collectionType.IsArray)
            {
                return collectionType.GetElementType();
            }
            else
            {
                if (collectionType.IsGenericType)
                {
                    return collectionType.GetGenericArguments()[0];
                }
                else
                {
                    return typeof(object);
                }
            }
        }

        private string GetSerializationName(Type type)
        {
            if (XmlDataModel.XmlModelInfo.EntitiesInfoKeyedByType.TryGetValue(type, out XmlEntityTypeInfo xmlEntityTypeInfo))
            {
                if (!string.IsNullOrWhiteSpace(xmlEntityTypeInfo.XmlName))
                {
                    return xmlEntityTypeInfo.XmlName;
                }
            }
            return type.Name;
        }

        private string GetXmlObjectName(XmlPropertyTypeInfo xmlPropertyTypeInfo)
        {
            if (!string.IsNullOrWhiteSpace(xmlPropertyTypeInfo.XmlName))
            {
                return xmlPropertyTypeInfo.XmlName;
            }
            else
            {
                return xmlPropertyTypeInfo.AssociatedPropertyName;
            }
        }

        private IEnumerable<object> DeserializeCollectionValues(XElement serializedCollectionObject, Type collectionType)
        {
            foreach (XElement serializedCollectionItem in serializedCollectionObject.Elements())
            {
                if (serializedCollectionItem.Name.LocalName == "Collection")
                {
                    Type collectionItemType = GetCollectionItemType(collectionType);
                    IEnumerable<object> deserializedCollectionValues = DeserializeCollectionValues(serializedCollectionItem, collectionItemType);

                    ICollectionHelper collectionHelper;
                    if (collectionItemType == typeof(object))
                    {
                        collectionHelper = XmlCollectionHelper.Create(typeof(List<object>));
                    }
                    else
                    {
                        collectionHelper = XmlCollectionHelper.Create(collectionItemType);
                    }
                    collectionHelper.CreateCollectionFromValues(deserializedCollectionValues, out object deserializedCollectionObject);
                    yield return deserializedCollectionObject;
                }
                else
                {
                    object deserializedObject = DeserializeObject(serializedCollectionItem, null);
                    yield return deserializedObject;
                }
            }
        }

        private void ThrowIfModelBuilderIsNotDefined()
        {
            if (XmlDataModel == null)
            {
                throw new SerializationException($"{nameof(XmlDataModel)} is required in order to perform requested operation.");
            }
        }

        private void ThrowIfPropertyCannotWrite(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite)
            {
                throw new Exception($"Property {propertyInfo.Name} of type {propertyInfo.PropertyType.Name} declared to {propertyInfo.DeclaringType.FullName} is marked as serializable but doues not have a set accessor.");
            }
        }


        private object DeserializeObject(XElement serializedObject, Type serializedObjectType)
        {
            //todo refactor
            if (serializedObjectType == null)
            {
                if (!XmlDataModel.XmlModelInfo.SerializationTypesKeyedByName.TryGetValue(serializedObject.Name.LocalName, out serializedObjectType))
                {
                    string errorMessage = $"Error occurred during xml deserialization. The {serializedObject.Name.LocalName} type cannot be resolved!";
                    throw new SerializationException(errorMessage);
                }
            }

            if (ElementIsContainer(serializedObject))
            {
                if (XmlCollectionHelper.IsSupportedCollectionType(serializedObjectType))
                {
                    IEnumerable<object> deserializedCollectionValues = DeserializeCollectionValues(serializedObject, serializedObjectType);
                    ICollectionHelper collectionPropertyHelper = XmlCollectionHelper.Create(serializedObjectType);
                    collectionPropertyHelper.CreateCollectionFromValues(deserializedCollectionValues, out object deserializedCollectionObject);
                    return deserializedCollectionObject;
                }

                if (XmlDataModel.XmlModelInfo.EntitiesInfoKeyedByType.TryGetValue(serializedObjectType, out XmlEntityTypeInfo xmlEntityTypeInfo))
                {
                    object[] constructorArgs = null;
                    var propertiesUsedByConstructor = new Dictionary<string, (object PropertyValue, bool ShouldBeUpdatedBySerializer)>();
                    int constructorParametersCount = xmlEntityTypeInfo.ConstructorParametersTypeInfo.Count;
                    if (constructorParametersCount > 0)
                    {
                        constructorArgs = new object[constructorParametersCount];
                        for (int i = 0; i < constructorParametersCount; i++)
                        {
                            XmlConstructorParameterTypeInfo constructorParameterTypeInfo = xmlEntityTypeInfo.ConstructorParametersTypeInfo[i];
                            string associatedPropertyName = constructorParameterTypeInfo.AssociatedPropertyName;
                            if (xmlEntityTypeInfo.PropertiesTypeInfo.TryGetValue(associatedPropertyName, out XmlPropertyTypeInfo xmlPropertyTypeInfo))
                            {
                                string elementName = GetXmlObjectName(xmlPropertyTypeInfo);
                                if (xmlPropertyTypeInfo.SerializeAsXmlAttribute)
                                {
                                    XAttribute serializedPropertyAttribute = serializedObject.Attribute(elementName);
                                    if (serializedPropertyAttribute != null)
                                    {
                                        serializedPropertyAttribute.Value.Convert(xmlPropertyTypeInfo.PropertyInfo.PropertyType, out object convertedValue);
                                        constructorArgs[i] = convertedValue;
                                        propertiesUsedByConstructor[associatedPropertyName] = (convertedValue, constructorParameterTypeInfo.AssociatedPropertyValueIsSetBySerializer);
                                    }
                                    else
                                    {
                                        throw new Exception($"Constructor argument[{i}] of type {serializedObjectType.FullName} is associated with property {associatedPropertyName} but the {elementName} attribute is not present into serialized object body");
                                    }
                                }
                                else
                                {
                                    XElement serializedPropertyElement = serializedObject.Element(elementName);
                                    if (serializedPropertyElement != null)
                                    {
                                        object deserializedObject = DeserializeObject(serializedPropertyElement, xmlPropertyTypeInfo.PropertyInfo.PropertyType);
                                        constructorArgs[i] = deserializedObject;
                                        propertiesUsedByConstructor[associatedPropertyName] = (deserializedObject, constructorParameterTypeInfo.AssociatedPropertyValueIsSetBySerializer);
                                    }
                                    else
                                    {
                                        throw new Exception($"Constructor argument[{i}] of type {serializedObjectType.FullName} is associated with property {associatedPropertyName} but the {elementName} element is not present into serialized object body");
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception($"Constructor argument[{i}] of type {serializedObjectType.FullName} is associated with property {associatedPropertyName} but the property is not configured by model builder.");
                            }
                        }
                    }
                    object instance = Activator.CreateInstance(serializedObjectType, constructorArgs);
                    foreach (XmlPropertyTypeInfo xmlPropertyTypeInfo in xmlEntityTypeInfo.PropertiesTypeInfo.Values)
                    {
                        if (propertiesUsedByConstructor.TryGetValue(xmlPropertyTypeInfo.AssociatedPropertyName, out var constructorProperty))
                        {
                            if (constructorProperty.ShouldBeUpdatedBySerializer)
                            {
                                if (constructorProperty.PropertyValue != null && XmlCollectionHelper.IsSupportedCollectionType(xmlPropertyTypeInfo.PropertyInfo.PropertyType))
                                {
                                    if (xmlPropertyTypeInfo.PropertyInfo.CanRead)
                                    {
                                        object propertyValue = xmlPropertyTypeInfo.PropertyInfo.GetValue(instance, null);
                                        if (propertyValue != null)
                                        {
                                            ICollectionHelper collectionHelper = XmlCollectionHelper.Create(xmlPropertyTypeInfo.PropertyInfo.PropertyType);
                                            if (collectionHelper.TryFillCollectionValues(propertyValue, constructorProperty.PropertyValue))
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                ThrowIfPropertyCannotWrite(xmlPropertyTypeInfo.PropertyInfo);
                                xmlPropertyTypeInfo.PropertyInfo.SetValue(instance, constructorProperty.PropertyValue);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        if (xmlPropertyTypeInfo.PropertyInfo.DeclaringType != serializedObjectType)
                        {
                            if (XmlDataModel.XmlModelInfo.EntitiesInfoKeyedByType.TryGetValue(xmlPropertyTypeInfo.PropertyInfo.DeclaringType, out XmlEntityTypeInfo baseEntityTypeInfo))
                            {
                                XmlConstructorParameterTypeInfo baseConstructorParameterInfoAssociatedWithCurrentProperty = baseEntityTypeInfo.ConstructorParametersTypeInfo.Values.FirstOrDefault(constructorParameterInfo => constructorParameterInfo.AssociatedPropertyName == xmlPropertyTypeInfo.PropertyInfo.Name);
                                if (baseConstructorParameterInfoAssociatedWithCurrentProperty != null && !baseConstructorParameterInfoAssociatedWithCurrentProperty.AssociatedPropertyValueIsSetBySerializer)
                                {
                                    continue;
                                }
                            }
                        }

                        string elementName = GetXmlObjectName(xmlPropertyTypeInfo);
                        if (xmlPropertyTypeInfo.SerializeAsXmlAttribute)
                        {
                            XAttribute serializedPropertyAttribute = serializedObject.Attribute(elementName);
                            if (serializedPropertyAttribute != null)
                            {
                                ThrowIfPropertyCannotWrite(xmlPropertyTypeInfo.PropertyInfo);
                                serializedPropertyAttribute.Value.Convert(xmlPropertyTypeInfo.PropertyInfo.PropertyType, out object convertedValue);
                                xmlPropertyTypeInfo.PropertyInfo.SetValue(instance, convertedValue);
                            }
                        }
                        else
                        {
                            XElement serializedPropertyElement = serializedObject.Element(elementName);
                            if (serializedPropertyElement != null)
                            {
                                if (XmlCollectionHelper.IsSupportedCollectionType(xmlPropertyTypeInfo.PropertyInfo.PropertyType))
                                {
                                    IEnumerable<object> deserializedCollectionValues = DeserializeCollectionValues(serializedPropertyElement, xmlPropertyTypeInfo.PropertyInfo.PropertyType);
                                    ICollectionHelper collectionPropertyHelper = XmlCollectionHelper.Create(xmlPropertyTypeInfo.PropertyInfo.PropertyType);

                                    if (xmlPropertyTypeInfo.PropertyInfo.CanRead)
                                    {
                                        object propertyValue = xmlPropertyTypeInfo.PropertyInfo.GetValue(instance, null);
                                        if (propertyValue != null)
                                        {
                                            ICollectionHelper collectionHelper = XmlCollectionHelper.Create(xmlPropertyTypeInfo.PropertyInfo.PropertyType);
                                            if (collectionHelper.TryFillCollectionValues(propertyValue, deserializedCollectionValues))
                                            {
                                                continue;
                                            }
                                        }
                                    }

                                    ThrowIfPropertyCannotWrite(xmlPropertyTypeInfo.PropertyInfo);
                                    collectionPropertyHelper.CreateCollectionFromValues(deserializedCollectionValues, out object createdCollectionWithValues);
                                    xmlPropertyTypeInfo.PropertyInfo.SetValue(instance, createdCollectionWithValues);
                                    continue;
                                }

                                ThrowIfPropertyCannotWrite(xmlPropertyTypeInfo.PropertyInfo);
                                object deserializedObject = DeserializeObject(serializedPropertyElement, xmlPropertyTypeInfo.PropertyInfo.PropertyType);
                                xmlPropertyTypeInfo.PropertyInfo.SetValue(instance, deserializedObject);
                            }
                        }
                    }
                    return instance;
                }
                else
                {
                    string errorMessage = $"Error occurred during xml deserialization. Unknown entity {serializedObjectType.FullName}";
                    throw new SerializationException(errorMessage);
                }
            }
            else
            {
                serializedObject.Value.Convert(serializedObjectType, out object convertedValue);
                return convertedValue;
            }
        }

        private XElement SerializeObject(object objectToBeSerialized, string objectName)
        {
            if (objectToBeSerialized == null)
            {
                throw new ArgumentNullException(nameof(objectToBeSerialized));
            }
            Type objectToBeSerializedType = objectToBeSerialized.GetType();

            XElement serializedObject = new XElement("Object");

            if (XmlCollectionHelper.IsSupportedCollectionType(objectToBeSerializedType))
            {
                if (objectName == null)
                {
                    objectName = "Collection";
                }
                IEnumerable objectToBeSerializedCollection = (IEnumerable)objectToBeSerialized;
                foreach (object collectionValue in objectToBeSerializedCollection)
                {
                    if (collectionValue != null)
                    {
                        XElement serializedCollectionValue = SerializeObject(collectionValue, null);
                        if (!IsEmptyCollectionNode(serializedCollectionValue))
                        {
                            serializedObject.Add(serializedCollectionValue);
                        }
                    }
                }
            }
            else
            {
                if (objectName == null)
                {
                    objectName = GetSerializationName(objectToBeSerializedType);
                }

                if (XmlDataModel.XmlModelInfo.EntitiesInfoKeyedByType.TryGetValue(objectToBeSerializedType, out XmlEntityTypeInfo xmlEntityTypeInfo))
                {
                    foreach (XmlPropertyTypeInfo xmlPropertyTypeInfo in xmlEntityTypeInfo.PropertiesTypeInfo.Values)
                    {
                        PropertyInfo propertyInfo = objectToBeSerializedType.GetProperty(xmlPropertyTypeInfo.AssociatedPropertyName);
                        object propertyValue = propertyInfo.GetValue(objectToBeSerialized, null);

                        if (propertyValue != null)
                        {
                            string propertyName = propertyInfo.Name;
                            if (!string.IsNullOrWhiteSpace(xmlPropertyTypeInfo.XmlName))
                            {
                                propertyName = xmlPropertyTypeInfo.XmlName;
                            }

                            Type propertyValueType = propertyValue.GetType();
                            if (propertyValueType.IsValueType)
                            {
                                if (xmlPropertyTypeInfo.ShouldNotBeSerializedIfHasDefaultValue)
                                {
                                    if (ValueIsEqualWithDefaultValueOfType(propertyValueType, propertyValue))
                                    {
                                        continue;
                                    }
                                }
                            }

                            if (xmlPropertyTypeInfo.SerializeAsXmlAttribute)
                            {
                                propertyValue.Convert(out string serializedValue);
                                XAttribute propertyAttribute = new XAttribute(propertyName, serializedValue);
                                serializedObject.Add(propertyAttribute);
                            }
                            else
                            {
                                XElement serializedProperty = SerializeObject(propertyValue, propertyName);
                                if (!IsEmptyCollectionNode(serializedProperty))
                                {
                                    serializedObject.Add(serializedProperty);
                                }
                            }
                        }
                    }
                }

                if (!ElementIsContainer(serializedObject))
                {
                    objectToBeSerialized.Convert(out string instanceToBeSerializedValue);
                    serializedObject.Value = instanceToBeSerializedValue;
                }
            }

            serializedObject.Name = objectName;
            return serializedObject;
        }

        private static bool IsEmptyCollectionNode(XElement xElement)
        {
            return xElement.Name.LocalName == "Collection" && !xElement.HasElements;
        }

        private static bool ElementIsContainer(XElement element)
        {
            return element.HasElements || element.HasAttributes;
        }

        private static bool ValueIsEqualWithDefaultValueOfType(Type typeOfValue, object value)
        {
            MethodInfo valueIsEqualWithDefaultValueMethodInfo = _valueIsEqualWithDefaultValueMethodInfo.MakeGenericMethod(typeOfValue);
            return (bool)valueIsEqualWithDefaultValueMethodInfo.Invoke(null, new[] { value });
        }

        private static bool ValueIsEqualWithDefaultValue<TValue>(object value)
        {
            return EqualityComparer<TValue>.Default.Equals((TValue)value, default);
        }
    }
}