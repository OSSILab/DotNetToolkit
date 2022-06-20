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

using System.Linq.Expressions;
using System.Reflection;

namespace System.Xml.Serialization
{
    /// <summary>
    /// Provides a simple API for configuring an entity that belongs to XML model.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that belongs to XML model.</typeparam>
    public sealed class XmlEntityTypeBuilder<TEntity>
    {
        private readonly Type _entityType = typeof(TEntity);
        private readonly XmlEntityTypeInfo _xmlEntityTypeInfo;
        private readonly XmlModelTypeInfo _xmlModelTypeInfo;
        private readonly XmlDataModel _xmlModelTypeBuilder;

        internal XmlEntityTypeBuilder(XmlEntityTypeInfo xmlEntityTypeInfo, XmlModelTypeInfo xmlModelTypeInfo, XmlDataModel xmlModelTypeBuilder)
        {
            _xmlEntityTypeInfo = xmlEntityTypeInfo;
            _xmlModelTypeInfo = xmlModelTypeInfo;
            _xmlModelTypeBuilder = xmlModelTypeBuilder;
            _xmlModelTypeBuilder.XmlModelInfo.SerializationTypesKeyedByName[_entityType.Name] = _entityType;
        }

        /// <summary>
        /// Excludes the entity from XML model.
        /// </summary>
        /// <returns><see cref="XmlDataModel"/></returns>
        public XmlDataModel Ignore()
        {
            _xmlModelTypeInfo.EntitiesInfoKeyedByType.Remove(typeof(TEntity));
            return _xmlModelTypeBuilder;
        }

        /// <summary>
        /// Specifies that the <see cref="XmlDataSerializer"/> must use
        /// a particular xml node name for <typeparamref name="TEntity"/> entity.
        /// </summary>
        /// <param name="xmlTypeName">Xml name to be used for <typeparamref name="TEntity"/> entity.</param>
        /// <returns><see cref="XmlEntityTypeBuilder{TEntity}"/></returns>
        public XmlEntityTypeBuilder<TEntity> HasName(string xmlTypeName)
        {
            if (_xmlEntityTypeInfo.XmlName == null)
            {
                _xmlModelTypeBuilder.XmlModelInfo.SerializationTypesKeyedByName.Remove(_entityType.Name);
            }
            else
            {
                _xmlModelTypeBuilder.XmlModelInfo.SerializationTypesKeyedByName.Remove(_xmlEntityTypeInfo.XmlName);
            }
            _xmlModelTypeBuilder.XmlModelInfo.SerializationTypesKeyedByName[xmlTypeName] = _entityType;
            _xmlEntityTypeInfo.XmlName = xmlTypeName;
            return this;
        }

        /// <summary>
        /// Returns an object that can be used to configure a property of the entity type.
        /// If the specified property is not already part of the model, it will be added.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to be configured.</typeparam>
        /// <param name="propertyExpression">A lambda expression representing the property to be configured ( blog => blog.Url).</param>
        /// <returns><see cref="XmlPropertyTypeBuilder{TEntity,TProperty}"/>An object that can be used to configure the property.</returns>
        public XmlPropertyTypeBuilder<TEntity, TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            ExpressionHelper.GetPropertyInfo(propertyExpression, out string propertyName, out PropertyInfo propertyInfo);
            if (propertyInfo.PropertyType.IsInterface && !XmlCollectionHelper.IsSupportedCollectionType(propertyInfo.PropertyType))
            {
                throw new InvalidOperationException($"Serialization/deserialization from interfaces is not allowd. Property:{propertyInfo.DeclaringType.Name}.{propertyName} of type:{propertyInfo.PropertyType.Name}. Choose a concrete type for selected property.");
            }
            XmlPropertyTypeInfo xmlPropertyTypeInfo = _xmlEntityTypeInfo.GetOrCreatePropertyTypeInfo(propertyName, propertyInfo);
            return new XmlPropertyTypeBuilder<TEntity, TProperty>(_xmlEntityTypeInfo, xmlPropertyTypeInfo, _xmlModelTypeInfo, _xmlModelTypeBuilder);
        }

        /// <summary>
        /// Returns an object that can be used to configure the constructor of the entity type.
        /// A specific entity can be deserialized using an user defined constructor.
        /// If the specified constructor is not already part of the model, it will be added.
        /// </summary>
        /// <returns><see cref="XmlConstructorTypeBuilder{TEntity}"/></returns>
        public XmlConstructorTypeBuilder<TEntity> Constructor()
        {
            return new XmlConstructorTypeBuilder<TEntity>(_xmlEntityTypeInfo, _xmlModelTypeInfo, _xmlModelTypeBuilder);
        }
    }
}