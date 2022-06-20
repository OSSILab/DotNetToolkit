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
    /// Represents a readonly tree data structure in which each element is attached to one or more elements directly beneath it.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IReadOnlyTree<out TValue>
    {
        /// <summary>Indicating if this tree have a parent tree.</summary>
        bool HaveParent { get; }

        /// <summary>Indicating if this tree have children.</summary>
        bool HaveChildren { get; }

        /// <summary>Gets the parent of the current tree.</summary>
        IReadOnlyTree<TValue> Parent { get; }

        /// <summary>The data value stored by the current tree.</summary>
        TValue Value { get; }

        /// <summary>Gets the children of the current tree.</summary>
        IEnumerable<IReadOnlyTree<TValue>> Children();

        /// <summary>Returns a collection of items that contain the ancestors of this tree.</summary>
        IEnumerable<IReadOnlyTree<TValue>> Ancestors();

        /// <summary>Returns a collection of items that contain this tree, and the ancestors of this tree.</summary>
        IEnumerable<IReadOnlyTree<TValue>> AncestorsAndSelf();

        /// <summary>Returns a collection of items that contain the descendants of this tree.</summary>
        IEnumerable<IReadOnlyTree<TValue>> Descendants();

        /// <summary>Returns a collection of items that contain this tree, and the descendants of this tree.</summary>
        IEnumerable<IReadOnlyTree<TValue>> DescendantsAndSelf();

        /// <summary>Returns a collection of items that contain the siblings of this tree.</summary>
        IEnumerable<IReadOnlyTree<TValue>> Siblings();

        /// <summary>Returns a collection of items that contain this tree, and the siblings of this tree.</summary>
        IEnumerable<IReadOnlyTree<TValue>> SiblingsAndSelf();
    }
}