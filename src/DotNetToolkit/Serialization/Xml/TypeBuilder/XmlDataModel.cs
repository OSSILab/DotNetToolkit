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

namespace System.Xml.Serialization
{
    /// <summary>
    /// Provide a simple API surface that will allow to define the shape of entities,
    /// the relation between them and how they are mapped during XML serialization process.
    /// </summary>
    public sealed class XmlDataModel
    {
        internal XmlModelTypeInfo XmlModelInfo { get; }

        /// <summary>Initializes a new instance of the <see cref="XmlDataModel"/> class.</summary>
        public XmlDataModel()
        {
            XmlModelInfo = new XmlModelTypeInfo();
            HasKnownEntity<bool>();
            HasKnownEntity<byte>();
            HasKnownEntity<sbyte>();
            HasKnownEntity<char>();
            HasKnownEntity<decimal>();
            HasKnownEntity<double>();
            HasKnownEntity<float>();
            HasKnownEntity<int>();
            HasKnownEntity<uint>();
            HasKnownEntity<long>();
            HasKnownEntity<ulong>();
            HasKnownEntity<object>();
            HasKnownEntity<short>();
            HasKnownEntity<ushort>();
            HasKnownEntity<string>();
        }

        private XmlDataModel(XmlModelTypeInfo xmlModelTypeInfo)
        {
            XmlModelInfo = xmlModelTypeInfo;
        }

        /// <summary>
        /// Specify that an entity type belongs to the XML model.
        /// If the entity type is not already part of the model, it will be added to it.
        /// </summary>
        /// <typeparam name="TEntity">The entity type that belongs to XML model.</typeparam>
        /// <returns>An object that can be used to configure the entity type.</returns>
        public XmlEntityTypeBuilder<TEntity> HasKnownEntity<TEntity>()
        {
            XmlEntityTypeInfo xmlEntityTypeInfo = XmlModelInfo.GetOrCreateTypeInfo(typeof(TEntity));
            return new XmlEntityTypeBuilder<TEntity>(xmlEntityTypeInfo, XmlModelInfo, this);
        }

        /// <summary>
        /// Creates an independent copy of the current model.
        /// </summary>
        /// <returns>An independent copy of the current model.</returns>
        public XmlDataModel Copy()
        {
            XmlModelTypeInfo xmlModelTypeInfo = XmlModelInfo.Copy();
            return new XmlDataModel(xmlModelTypeInfo);
        }
    }
}