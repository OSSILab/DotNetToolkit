﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NetStandardUtils.Serialization.Xml
{
    /// <summary>
    /// Provides a simple API for configuring a constructor parameter of the entity type in the XML model.
    /// A specific entity can be deserialized using an user defined constructor.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class XmlConstructorParameterTypeBuilder<TEntity>
    {
        private readonly XmlEntityTypeInfo _xmlEntityTypeInfo;
        private readonly XmlConstructorParameterTypeInfo _xmlConstructorParameterTypeInfo;
        private readonly XmlModelTypeInfo _xmlModelTypeInfo;
        private readonly XmlModelBuilder _xmlModelTypeBuilder;

        internal XmlConstructorParameterTypeBuilder(XmlEntityTypeInfo xmlEntityTypeInfo, XmlConstructorParameterTypeInfo xmlConstructorParameterTypeInfo, XmlModelTypeInfo xmlModelTypeInfo, XmlModelBuilder xmlModelTypeBuilder)
        {
            _xmlEntityTypeInfo = xmlEntityTypeInfo;
            _xmlConstructorParameterTypeInfo = xmlConstructorParameterTypeInfo;
            _xmlModelTypeInfo = xmlModelTypeInfo;
            _xmlModelTypeBuilder = xmlModelTypeBuilder;
        }

        /// <summary>
        /// Excludes the constructor parameter from XML model.
        /// </summary>
        /// <returns><see cref="XmlConstructorTypeBuilder{TEntity}"/></returns>
        public XmlConstructorTypeBuilder<TEntity> Ignore()
        {
            int? paramInfoKey = _xmlEntityTypeInfo.ConstructorParametersTypeInfo.Where(dict => dict.Value == _xmlConstructorParameterTypeInfo).Select(dict => new int?(dict.Key)).FirstOrDefault();
            if (paramInfoKey != null)
            {
                _xmlEntityTypeInfo.ConstructorParametersTypeInfo.Remove(paramInfoKey.Value);
            }
            return new XmlConstructorTypeBuilder<TEntity>(_xmlEntityTypeInfo, _xmlModelTypeInfo, _xmlModelTypeBuilder);
        }


        /// <summary>
        /// Returns an object that can be used to configure the constructor parameter of the entity type.
        /// A specific entity can be deserialized using an user defined constructor.
        /// If the specified constructor parameter is not already part of the model, it will be added. 
        /// </summary>
        /// <param name="parameterIndex"></param>
        /// <returns><see cref="XmlConstructorParameterTypeBuilder{TEntity}"/></returns>
        public XmlConstructorParameterTypeBuilder<TEntity> Parameter(int parameterIndex)
        {
            XmlConstructorParameterTypeInfo constructorParameterTypeInfo = _xmlEntityTypeInfo.GetOrCreateConstructorParameterTypeInfo(parameterIndex);
            return new XmlConstructorParameterTypeBuilder<TEntity>(_xmlEntityTypeInfo, constructorParameterTypeInfo, _xmlModelTypeInfo, _xmlModelTypeBuilder);
        }


        /// <summary>
        /// Returns an object that can be used to configure a property of the entity type.
        /// If the specified property is not already part of the model, it will be added.
        /// </summary>
        /// <typeparam name="T">The type of the property to be configured.</typeparam>
        /// <param name="propertyExpression">A lambda expression representing the property to be configured ( blog => blog.Url).</param>
        /// <returns><see cref="XmlPropertyTypeBuilder{TEntity,TProperty}"/>An object that can be used to configure the property.</returns>
        public XmlPropertyTypeBuilder<TEntity, T> Property<T>(Expression<Func<TEntity, T>> propertyExpression)
        {
            ExpressionHelper.GetPropertyInfo(propertyExpression, out string propertyName, out PropertyInfo propertyInfo);
            XmlPropertyTypeInfo xmlPropertyTypeInfo = _xmlEntityTypeInfo.GetOrCreatePropertyTypeInfo(propertyName, propertyInfo);
            return new XmlPropertyTypeBuilder<TEntity, T>(_xmlEntityTypeInfo, xmlPropertyTypeInfo, _xmlModelTypeInfo, _xmlModelTypeBuilder);
        }


        /// <summary>
        /// Specify if the property that is used as a constructor argument should be updated by the <see cref="XmlSerializer"/>
        /// or by the constructor of <typeparamref name="TEntity"/>.
        /// If the property is set through constructor dependency injection <see cref="AssociatedPropertyValueIsSetByConstructor"/> should be used,
        /// otherwise <see cref="AssociatedPropertyValueIsSetBySerializer"/>.
        /// </summary>
        /// <returns></returns>
        public XmlConstructorParameterTypeBuilder<TEntity> AssociatedPropertyValueIsSetByConstructor()
        {
            _xmlConstructorParameterTypeInfo.AssociatedPropertyValueIsSetBySerializer = false;
            return this;
        }

        /// <summary>
        /// Specify if the property that is used as a constructor argument should be updated by the <see cref="XmlSerializer"/>
        /// or by the constructor of <typeparamref name="TEntity"/>.
        /// If the property is set through constructor dependency injection <see cref="AssociatedPropertyValueIsSetByConstructor"/> should be used,
        /// otherwise <see cref="AssociatedPropertyValueIsSetBySerializer"/>.
        /// </summary>
        /// <returns></returns>
        public XmlConstructorParameterTypeBuilder<TEntity> AssociatedPropertyValueIsSetBySerializer()
        {
            _xmlConstructorParameterTypeInfo.AssociatedPropertyValueIsSetBySerializer = true;
            return this;
        }


        /// <summary>
        /// Specify that a constructor parameter should have the value of the specified property during deserialization.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property associated with the constructor parameter</typeparam>
        /// <param name="propertyExpression">A lambda expression representing the property to be associated with ( blog => blog.Url).</param>
        /// <returns><see cref="XmlConstructorParameterTypeBuilder{TEntity}"/></returns>
        public XmlConstructorParameterTypeBuilder<TEntity> HasAssociatedProperty<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            ExpressionHelper.GetPropertyInfo(propertyExpression, out string propertyName, out _);
            _xmlConstructorParameterTypeInfo.AssociatedPropertyName = propertyName;
            return this;
        }
    }
}