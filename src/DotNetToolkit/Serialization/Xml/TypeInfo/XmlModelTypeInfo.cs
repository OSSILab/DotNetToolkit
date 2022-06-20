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

using System.Collections.Generic;
using System.Reflection;

namespace System.Xml.Serialization
{
    internal class XmlModelTypeInfo
    {
        public XmlModelTypeInfo()
        {
            EntitiesInfoKeyedByType = new Dictionary<Type, XmlEntityTypeInfo>();
            SerializationTypesKeyedByName = new Dictionary<string, Type>();
        }
        public IDictionary<Type, XmlEntityTypeInfo> EntitiesInfoKeyedByType { get; }
        public IDictionary<string, Type> SerializationTypesKeyedByName { get; }

        public XmlEntityTypeInfo GetOrCreateTypeInfo(Type entityType)
        {
            if (!EntitiesInfoKeyedByType.TryGetValue(entityType, out XmlEntityTypeInfo xmlEntityTypeInfo))
            {
                xmlEntityTypeInfo = new XmlEntityTypeInfo();
                xmlEntityTypeInfo.EntityType = entityType;
                EntitiesInfoKeyedByType.Add(entityType, xmlEntityTypeInfo);
            }
            return xmlEntityTypeInfo;
        }

        public XmlPropertyTypeInfo GetOrCreatePropertyTypeInfo(Type entityType, string propertyName, PropertyInfo propertyInfo)
        {
            XmlEntityTypeInfo xmlEntityTypeInfo = GetOrCreateTypeInfo(entityType);
            return xmlEntityTypeInfo.GetOrCreatePropertyTypeInfo(propertyName, propertyInfo);
        }

        public XmlConstructorParameterTypeInfo GetOrCreateConstructorArgumentTypeInfo(Type entityType, int constructorParameterIndex)
        {
            XmlEntityTypeInfo xmlEntityTypeInfo = GetOrCreateTypeInfo(entityType);
            return xmlEntityTypeInfo.GetOrCreateConstructorParameterTypeInfo(constructorParameterIndex);
        }

        public XmlModelTypeInfo Copy()
        {
            XmlModelTypeInfo xmlModelTypeInfo = new XmlModelTypeInfo();
            foreach (var xmlEntityTypeInfoByType in EntitiesInfoKeyedByType)
            {
                xmlModelTypeInfo.EntitiesInfoKeyedByType.Add(xmlEntityTypeInfoByType.Key, xmlEntityTypeInfoByType.Value.Copy());
            }
            xmlModelTypeInfo.SerializationTypesKeyedByName.AddRange(SerializationTypesKeyedByName);
            return xmlModelTypeInfo;
        }
    }
}