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

namespace System.Collections.Generic
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for working with specific
    /// kinds of collections.
    /// </summary>
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the the <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary for which the elements will be added.</param>
        /// <param name="valuesToAdd">The collection whose elements should be added to the <see cref="IDictionary{TKey,TValue}"/>.</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> valuesToAdd)
        {
            foreach (var valueToAdd in valuesToAdd)
            {
                dictionary.Add(valueToAdd);
            }
        }

        /// <summary>
        /// Adds the elements of the specified collection to the the <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary for which the elements will be added.</param>
        /// <param name="valuesToAdd">The collection whose elements should be added to the <see cref="IDictionary{TKey,TValue}"/>.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TValue> valuesToAdd, Func<TValue, TKey> keySelector)
        {
            foreach (TValue valueToAdd in valuesToAdd)
            {
                TKey key = keySelector(valueToAdd);
                dictionary.Add(key, valueToAdd);
            }
        }

        /// <summary>
        /// Adds the elements of the specified collection to the the <see cref="Queue{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">Specifies the type of elements in the queue.</typeparam>
        /// <param name="queue">The queue for which the elements will be added.</param>
        /// <param name="valuesToEnqueue">The collection whose elements should be added to the <see cref="Queue{TValue}"/>.</param>
        public static void EnqueueRange<TValue>(this Queue<TValue> queue, IEnumerable<TValue> valuesToEnqueue)
        {
            foreach (TValue value in valuesToEnqueue)
            {
                queue.Enqueue(value);
            }
        }

        /// <summary>
        /// Returns duplicated elements from a sequence by using the default equality comparer to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/></typeparam>
        /// <param name="source"></param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains duplicated elements from the source sequence.</returns>
        public static IEnumerable<TSource> FindDuplicates<TSource>(this IEnumerable<TSource> source)
        {
            return FindDuplicates(source, null);
        }

        /// <summary>
        /// Returns duplicated elements from a sequence by using a specified <see cref="IEqualityComparer{TSource}"/> to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer">An <see cref="IEnumerable{T}"/> to compare values.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains duplicated elements from the source sequence.</returns>
        public static IEnumerable<TSource> FindDuplicates<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> sourceSet;
            if (source is ICollection<TSource> sourceCollection)
            {
#if NETSTANDARD2_0
                sourceSet = new HashSet<TSource>(comparer);
#else
                sourceSet = new HashSet<TSource>(sourceCollection.Count, comparer);
#endif

            }
            else
            {
                sourceSet = new HashSet<TSource>(comparer);
            }

            foreach (TSource item in source)
            {
                if (!sourceSet.Add(item))
                {
                    yield return item;
                }
            }
        }
    }
}