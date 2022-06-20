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
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Xml.Serialization
{
    internal interface ICollectionHelper
    {
        bool TryFillCollectionValues(object collection, object collectionValues);
        void CreateCollectionFromValues(object collectionValues, out object createdCollection);
    }

    internal interface ISupportedCollection<TCollection, TCollectionValues>
    {
        bool TryFillCollectionValues(TCollection collection, TCollectionValues collectionValues);
        void CreateCollectionFromValues(TCollectionValues collectionValues, out TCollection createdCollection);
    }

    internal interface ISupportedGenericDictionaryCollection<TKey, TValue> :
        ISupportedCollection<IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>>,
        ISupportedCollection<IReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>>,
        ISupportedCollection<Dictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>>,
        ISupportedCollection<ReadOnlyDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>>
    {

    }

    internal interface ISupportedNonGenericCollection :
        ISupportedCollection<IEnumerable, IEnumerable>,
        ISupportedCollection<IList, IEnumerable>,
        ISupportedCollection<ICollection, IEnumerable>,
        ISupportedCollection<Array, IEnumerable>,
        ISupportedCollection<ArrayList, IEnumerable>,
        ISupportedCollection<Queue, IEnumerable>,
        ISupportedCollection<Stack, IEnumerable>

    {

    }

    internal interface ISupportedGenericEnumerableCollection<TCollectionValue> :
        ISupportedCollection<IEnumerable<TCollectionValue>, IEnumerable<TCollectionValue>>,
        ISupportedCollection<ICollection<TCollectionValue>, IEnumerable<TCollectionValue>>,
        ISupportedCollection<IList<TCollectionValue>, IEnumerable<TCollectionValue>>,
        ISupportedCollection<IReadOnlyList<TCollectionValue>, IEnumerable<TCollectionValue>>,
        ISupportedCollection<IReadOnlyCollection<TCollectionValue>, IEnumerable<TCollectionValue>>,
        ISupportedCollection<List<TCollectionValue>, IEnumerable<TCollectionValue>>,
        ISupportedCollection<Collection<TCollectionValue>, IEnumerable<TCollectionValue>>,
        ISupportedCollection<ReadOnlyCollection<TCollectionValue>, IEnumerable<TCollectionValue>>,
        ISupportedCollection<Queue<TCollectionValue>, IEnumerable<TCollectionValue>>,
        ISupportedCollection<Stack<TCollectionValue>, IEnumerable<TCollectionValue>>,
        ISupportedCollection<HashSet<TCollectionValue>, IEnumerable<TCollectionValue>>
    {

    }

    internal abstract class CollectionHelper<TCollection,TCollectionValues> : ICollectionHelper
    {
        private readonly TryFillCollectionValuesDelegate _tryFillCollectionValuesDelegateAction;
        private readonly CreateCollectionFromValuesDelegate _createCollectionFromValuesDelegateAction;

        private delegate bool TryFillCollectionValuesDelegate(TCollection currentPropertyCollectionValues, TCollectionValues collectionValuesToSet);
        private delegate void CreateCollectionFromValuesDelegate(TCollectionValues collectionValuesToSet, out TCollection createdCollection);


        protected CollectionHelper()
        {
            _tryFillCollectionValuesDelegateAction = (TryFillCollectionValuesDelegate)Delegate.CreateDelegate(typeof(TryFillCollectionValuesDelegate), this, nameof(ISupportedCollection<object,object>.TryFillCollectionValues));
            _createCollectionFromValuesDelegateAction = (CreateCollectionFromValuesDelegate)Delegate.CreateDelegate(typeof(CreateCollectionFromValuesDelegate), this, nameof(ISupportedCollection<object, object>.CreateCollectionFromValues));
        }

        public bool TryFillCollectionValues(object collection, object collectionValues)
        {
            TCollection collectionFromObject = GetCollectionFromObject(collection);
            TCollectionValues collectionValuesFromObject = GetCollectionValuesFromObject(collectionValues);
            return _tryFillCollectionValuesDelegateAction(collectionFromObject, collectionValuesFromObject);
        }
        
        public void CreateCollectionFromValues(object collectionValues, out object createdCollection)
        {
            TCollectionValues collectionValuesFromObject = GetCollectionValuesFromObject(collectionValues);
            _createCollectionFromValuesDelegateAction(collectionValuesFromObject, out TCollection createdCollectionValue);
            createdCollection = createdCollectionValue;
        }

        protected abstract TCollection GetCollectionFromObject(object collection);
        protected abstract TCollectionValues GetCollectionValuesFromObject(object collectionValues);
    }

    internal sealed class GenericEnumerableCollectionPropertyHelper<TCollection, TCollectionValue> : 
        CollectionHelper<TCollection, IEnumerable<TCollectionValue>>, 
        ISupportedGenericEnumerableCollection<TCollectionValue>
    {

        protected override TCollection GetCollectionFromObject(object collection)
        {
            return (TCollection)collection;
        }

        protected override IEnumerable<TCollectionValue> GetCollectionValuesFromObject(object collectionValues)
        {
            IEnumerable<object> collectionValuesEnumerable = (IEnumerable<object>)collectionValues;
            return collectionValuesEnumerable.Cast<TCollectionValue>();
        }

        public bool TryFillCollectionValues(Queue<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            collection.Clear();
            foreach (TCollectionValue collectionValue in collectionValues)
            {
                collection.Enqueue(collectionValue);
            }
            return true;
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out Queue<TCollectionValue> createdCollection)
        {
            createdCollection = new Queue<TCollectionValue>(collectionValues);
        }

        public bool TryFillCollectionValues(Stack<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            collection.Clear();
            foreach (TCollectionValue collectionValue in collectionValues)
            {
                collection.Push(collectionValue);
            }
            return true;
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out Stack<TCollectionValue> createdCollection)
        {
            createdCollection = new Stack<TCollectionValue>(collectionValues);
        }

        public bool TryFillCollectionValues(IReadOnlyCollection<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            return false;
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out IReadOnlyCollection<TCollectionValue> createdCollection)
        {
            createdCollection = new List<TCollectionValue>(collectionValues);
        }

        public bool TryFillCollectionValues(ReadOnlyCollection<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            return false;
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out ReadOnlyCollection<TCollectionValue> createdCollection)
        {
            createdCollection = new List<TCollectionValue>(collectionValues).AsReadOnly();
        }

        public bool TryFillCollectionValues(List<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            collection.Clear();
            collection.AddRange(collectionValues);
            return true;
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out List<TCollectionValue> createdCollection)
        {
            createdCollection = new List<TCollectionValue>(collectionValues);
        }

        public bool TryFillCollectionValues(IList<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            collection.Clear();
            foreach (TCollectionValue collectionValue in collectionValues)
            {
                collection.Add(collectionValue);
            }
            return true;
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out IList<TCollectionValue> createdCollection)
        {
            createdCollection = new List<TCollectionValue>(collectionValues);
        }

        public bool TryFillCollectionValues(IReadOnlyList<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            return false;
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out IReadOnlyList<TCollectionValue> createdCollection)
        {
            createdCollection = new List<TCollectionValue>(collectionValues);
        }


        public bool TryFillCollectionValues(Collection<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            ICollection<TCollectionValue> collectionToFill = collection;
            return TryFillCollectionValues(collectionToFill, collectionValues);
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out Collection<TCollectionValue> createdCollection)
        {
            createdCollection = new Collection<TCollectionValue>();
            foreach (TCollectionValue collectionValue in collectionValues)
            {
                createdCollection.Add(collectionValue);
            }
        }

        public bool TryFillCollectionValues(ICollection<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            collection.Clear();
            foreach (TCollectionValue collectionValue in collectionValues)
            {
                collection.Add(collectionValue);
            }
            return true;
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out ICollection<TCollectionValue> createdCollection)
        {
            CreateCollectionFromValues(collectionValues, out Collection<TCollectionValue> newCollection);
            createdCollection = newCollection;
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out IEnumerable<TCollectionValue> createdCollection)
        {
            createdCollection = new List<TCollectionValue>(collectionValues);
        }

        public bool TryFillCollectionValues(IEnumerable<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            return false;
        }

        public bool TryFillCollectionValues(HashSet<TCollectionValue> collection, IEnumerable<TCollectionValue> collectionValues)
        {
            collection.Clear();
            foreach (TCollectionValue collectionValue in collectionValues)
            {
                collection.Add(collectionValue);
            }

            return true;
        }

        public void CreateCollectionFromValues(IEnumerable<TCollectionValue> collectionValues, out HashSet<TCollectionValue> createdCollection)
        {
            createdCollection = new HashSet<TCollectionValue>(collectionValues);
        }
    }


    internal sealed class EnumerableCollectionPropertyHelper<TCollection> : 
        CollectionHelper<TCollection,IEnumerable>,
        ISupportedNonGenericCollection
    {

        protected override TCollection GetCollectionFromObject(object collection)
        {
            return (TCollection)collection;
        }

        protected override IEnumerable GetCollectionValuesFromObject(object collectionValues)
        {
            return (IEnumerable)collectionValues;
        }

        public bool TryFillCollectionValues(IEnumerable collection, IEnumerable collectionValues)
        {
            return false;
        }

        public void CreateCollectionFromValues(IEnumerable collectionValues, out IEnumerable createdCollection)
        {
            CreateCollectionFromValues(collectionValues, out IList createdList);
            createdCollection = createdList;
        }

        public bool TryFillCollectionValues(IList collection, IEnumerable collectionValues)
        {
            if (collection.IsReadOnly)
            {
                return false;
            }
            foreach (object collectionValue in collectionValues)
            {
                collection.Add(collectionValue);
            }
            return true;
        }

        public void CreateCollectionFromValues(IEnumerable collectionValues, out IList createdCollection)
        {
            CreateCollectionFromValues(collectionValues, out ArrayList createdList);
            createdCollection = createdList;
        }

        public bool TryFillCollectionValues(ICollection collection, IEnumerable collectionValues)
        {
            return false;
        }

        public void CreateCollectionFromValues(IEnumerable collectionValues, out ICollection createdCollection)
        {
            CreateCollectionFromValues(collectionValues, out ArrayList createdList);
            createdCollection = createdList;
        }

        public bool TryFillCollectionValues(Array collection, IEnumerable collectionValues)
        {
            return false;
        }

        public void CreateCollectionFromValues(IEnumerable collectionValues, out Array createdCollection)
        {
            CreateCollectionFromValues(collectionValues, out ArrayList createdList);
            createdCollection = createdList.ToArray();
        }

        public bool TryFillCollectionValues(ArrayList collection, IEnumerable collectionValues)
        {
            if (collection.IsReadOnly)
            {
                return false;
            }
            collection.Clear();
            foreach (object collectionValue in collectionValues)
            {
                collection.Add(collectionValue);
            }
            return true;
        }

        public void CreateCollectionFromValues(IEnumerable collectionValues, out ArrayList createdCollection)
        {
            ArrayList arrayList = new ArrayList();
            foreach (object collectionValue in collectionValues)
            {
                arrayList.Add(collectionValue);
            }
            createdCollection = arrayList;
        }

        public bool TryFillCollectionValues(Queue collection, IEnumerable collectionValues)
        {
            collection.Clear();
            foreach (object collectionValue in collectionValues)
            {
                collection.Enqueue(collectionValue);
            }
            return true;
        }

        public void CreateCollectionFromValues(IEnumerable collectionValues, out Queue createdCollection)
        {
            Queue queue = new Queue();
            foreach (object collectionValue in collectionValues)
            {
                queue.Enqueue(collectionValue);
            }
            createdCollection = queue;
        }

        public bool TryFillCollectionValues(Stack collection, IEnumerable collectionValues)
        {
            collection.Clear();
            foreach (object collectionValue in collectionValues)
            {
                collection.Push(collectionValue);
            }
            return true;
        }

        public void CreateCollectionFromValues(IEnumerable collectionValues, out Stack createdCollection)
        {
            Stack stack = new Stack();
            foreach (object collectionValue in collectionValues)
            {
                stack.Push(collectionValue);
            }
            createdCollection = stack;
        }
    }

    internal static class XmlCollectionHelper
    {
        private static readonly Type[] _supportedGenericEnumerableCollectionDefinitionTypes;
        private static readonly Type[] _supportedEnumerableCollectionDefinitionTypes;
        

        static XmlCollectionHelper()
        {
            _supportedGenericEnumerableCollectionDefinitionTypes = FindSupportedCollectionDefinitionTypesOfType(typeof(ISupportedGenericEnumerableCollection<>)).ToArray();
            _supportedEnumerableCollectionDefinitionTypes = FindSupportedCollectionDefinitionTypesOfType(typeof(ISupportedNonGenericCollection)).ToArray();
        }

        private static IEnumerable<Type> FindSupportedCollectionDefinitionTypesOfType(Type type)
        {
            string supportedCollectionTypeName = typeof(ISupportedCollection<,>).Name;
            Type[] interfaces = type.GetInterfaces();
            foreach (Type interfaceType in interfaces)
            {
                if (interfaceType.Name == supportedCollectionTypeName)
                {
                    Type collectionType = interfaceType.GenericTypeArguments[0];
                    if (collectionType.IsGenericType)
                    {
                        yield return collectionType.GetGenericTypeDefinition();
                    }
                    else
                    {
                        yield return collectionType;
                    }
                }
            }
        }

        public static bool IsSupportedCollectionType(Type type)
        {
            if (type.IsEnum || type.IsPrimitive || type == typeof(string))
            {
                return false;
            }

            if (type.IsGenericType)
            {
                if (type.IsValueType)
                {
                    return false;
                }

                if (type.GenericTypeArguments.Length != 1)
                {
                    return false;
                }

                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                return _supportedGenericEnumerableCollectionDefinitionTypes.Any(supportedInterface => supportedInterface == genericTypeDefinition);
            }
            else
            {
                if (type.IsArray)
                {
                    return true;
                }
                return _supportedEnumerableCollectionDefinitionTypes.Any(supportedInterface => supportedInterface == type);
            }
        }

        public static ICollectionHelper Create(Type collectionType)
        {
            if (collectionType.IsGenericType)
            {
                Type genericEnumerableCollectionPropertyHelperType = typeof(GenericEnumerableCollectionPropertyHelper<,>).MakeGenericType(collectionType, collectionType.GenericTypeArguments[0]);
                return (ICollectionHelper)Activator.CreateInstance(genericEnumerableCollectionPropertyHelperType);
            }
            else
            {
                if (collectionType.IsArray)
                {
                    return new EnumerableCollectionPropertyHelper<Array>();
                }
                else
                {
                    return (ICollectionHelper)Activator.CreateInstance(typeof(EnumerableCollectionPropertyHelper<>).MakeGenericType(collectionType));
                }
            }
        }
    }
}