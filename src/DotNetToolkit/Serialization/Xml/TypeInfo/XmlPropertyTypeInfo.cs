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

using System.Reflection;

namespace System.Xml.Serialization
{
    internal class XmlPropertyTypeInfo
    {
        public PropertyInfo PropertyInfo { get; set; }
        public string XmlName { get; set; }
        public bool SerializeAsXmlAttribute { get; set; }
        public string AssociatedPropertyName { get; set; }
        public bool ShouldNotBeSerializedIfHasDefaultValue { get; set; }

        public XmlPropertyTypeInfo Copy()
        {
            return new XmlPropertyTypeInfo
            {
                PropertyInfo = PropertyInfo,
                XmlName = XmlName,
                SerializeAsXmlAttribute = SerializeAsXmlAttribute,
                AssociatedPropertyName = AssociatedPropertyName,
                ShouldNotBeSerializedIfHasDefaultValue = ShouldNotBeSerializedIfHasDefaultValue
            };
        }
    }
}