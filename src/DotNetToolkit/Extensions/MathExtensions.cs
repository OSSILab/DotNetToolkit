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
using System.Linq;

namespace System.Mathematics
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for working with specific
    /// kinds of collections.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Generate all possible permutations of n objects using B. R. Heap's algorithm.
        /// </summary>
        /// <typeparam name="T">The type of objects.</typeparam>
        /// <param name="objects">The objects used to generate permutations.</param>
        /// <returns>All possible permutations for the specified <paramref name="objects"/></returns>
        public static IEnumerable<IReadOnlyList<T>> Permutations<T>(this IEnumerable<T> objects)
        {
            if (objects == null)
            {
                throw new ArgumentNullException(nameof(objects));
            }
            T[] objectsArray = objects.ToArray();
            int objectsArrayLength = objectsArray.Length;
            int[] c = new int[objectsArrayLength];

            T[] objectsArrayCopy = new T[objectsArrayLength];
            objectsArray.CopyTo(objectsArrayCopy, 0);
            yield return objectsArrayCopy;

            int i = 0;
            while (i < objectsArrayLength)
            {
                if (c[i] < i)
                {
                    T temp = objectsArray[i];
                    if (i % 2 == 0)
                    {
                        objectsArray[i] = objectsArray[0];
                        objectsArray[0] = temp;
                    }
                    else
                    {
                        objectsArray[i] = objectsArray[c[i]];
                        objectsArray[c[i]] = temp;
                    }
        
                    T[] copy = new T[objectsArrayLength];
                    objectsArray.CopyTo(copy, 0);
                    yield return copy;
        
                    c[i] += 1;
                    i = 0;
                }
                else
                {
                    c[i] = 0;
                    i += 1;
                }
            }
        }
    }
}