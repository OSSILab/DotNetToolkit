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
    internal class XmlEntityTypeInfo
    {
        public XmlEntityTypeInfo()
        {
            PropertiesTypeInfo = new Dictionary<string, XmlPropertyTypeInfo>();
            ConstructorParametersTypeInfo = new Dictionary<int, XmlConstructorParameterTypeInfo>();
        }

        public Type EntityType { get; set; }
        public string XmlName { get; set; }
        public bool SerializeAsXmlAttribute { get; set; }
        public IDictionary<string, XmlPropertyTypeInfo> PropertiesTypeInfo { get; }
        public IDictionary<int, XmlConstructorParameterTypeInfo> ConstructorParametersTypeInfo { get; }

        public XmlPropertyTypeInfo GetOrCreatePropertyTypeInfo(string propertyName, PropertyInfo propertyInfo)
        {
            if (!PropertiesTypeInfo.TryGetValue(propertyName, out XmlPropertyTypeInfo xmlPropertyTypeInfo))
            {
                xmlPropertyTypeInfo = new XmlPropertyTypeInfo();
                xmlPropertyTypeInfo.AssociatedPropertyName = propertyName;
                xmlPropertyTypeInfo.PropertyInfo = propertyInfo;
                PropertiesTypeInfo.Add(propertyName,xmlPropertyTypeInfo);
            }
            return xmlPropertyTypeInfo;
        }

        public XmlConstructorParameterTypeInfo GetOrCreateConstructorParameterTypeInfo(int constructorParameterIndex)
        {
            if (!ConstructorParametersTypeInfo.TryGetValue(constructorParameterIndex, out XmlConstructorParameterTypeInfo xmlConstructorParameterTypeInfo))
            {
                xmlConstructorParameterTypeInfo = new XmlConstructorParameterTypeInfo();
                ConstructorParametersTypeInfo.Add(constructorParameterIndex, xmlConstructorParameterTypeInfo);
            }
            return xmlConstructorParameterTypeInfo;
        }

        public XmlEntityTypeInfo Copy()
        {
            XmlEntityTypeInfo xmlEntityTypeInfo = new XmlEntityTypeInfo();
            xmlEntityTypeInfo.EntityType = EntityType;
            xmlEntityTypeInfo.XmlName = XmlName;
            xmlEntityTypeInfo.SerializeAsXmlAttribute = SerializeAsXmlAttribute;

            foreach (var propertyTypeInfoByName in PropertiesTypeInfo)
            {
                xmlEntityTypeInfo.PropertiesTypeInfo.Add(propertyTypeInfoByName.Key, propertyTypeInfoByName.Value.Copy());
            }

            foreach (var constructorParameterTypeInfoByPosition in ConstructorParametersTypeInfo)
            {
                xmlEntityTypeInfo.ConstructorParametersTypeInfo.Add(constructorParameterTypeInfoByPosition.Key, constructorParameterTypeInfoByPosition.Value.Copy());
            }
            return xmlEntityTypeInfo;
        }
    }
}