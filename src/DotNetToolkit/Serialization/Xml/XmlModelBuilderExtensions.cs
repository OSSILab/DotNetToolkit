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
    /// Provides a set of static (Shared in Visual Basic) methods for working with specific
    /// kinds of xml model builder instances.
    /// </summary>
    public static class XmlModelBuilderExtensions
    {
        /// <summary>
        /// Specifies that the <see cref="XmlDataSerializer"/> should not serialize the property if the value of it is <c>default</c>
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity that belongs to XML model.</typeparam>
        /// <typeparam name="TProperty">The type of the property that belongs to <typeparamref name="TEntity"/> entity in the XML model.</typeparam>
        /// <param name="xmlPropertyTypeBuilder">The property builder to be configured.</param>
        /// <returns><see cref="XmlPropertyTypeBuilder{TEntity,TProperty}"/>An object that can be used to configure the property.</returns>
        public static XmlPropertyTypeBuilder<TEntity, TProperty> ShouldNotBeSerializedIfHasDefaultValue<TEntity, TProperty>(this XmlPropertyTypeBuilder<TEntity, TProperty> xmlPropertyTypeBuilder) where TProperty : struct
        {
            xmlPropertyTypeBuilder.ShouldNotBeSerializedIfValueEqualsWithDefaultValue = true;
            return xmlPropertyTypeBuilder;
        }

        /// <summary>
        /// Specifies that the <see cref="XmlDataSerializer"/> should serialize the property even if the value of it is <c>default</c>
        /// This is the default behavior.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity that belongs to XML model.</typeparam>
        /// <typeparam name="TProperty">The type of the property that belongs to <typeparamref name="TEntity"/> entity in the XML model.</typeparam>
        /// <param name="xmlPropertyTypeBuilder">The property builder to be configured.</param>
        /// <returns><see cref="XmlPropertyTypeBuilder{TEntity,TProperty}"/>An object that can be used to configure the property.</returns>
        public static XmlPropertyTypeBuilder<TEntity, TProperty> ShouldBeSerializedIfHasDefaultValue<TEntity, TProperty>(this XmlPropertyTypeBuilder<TEntity, TProperty> xmlPropertyTypeBuilder) where TProperty : struct
        {
            xmlPropertyTypeBuilder.ShouldNotBeSerializedIfValueEqualsWithDefaultValue = false;
            return xmlPropertyTypeBuilder;
        }
    }
}