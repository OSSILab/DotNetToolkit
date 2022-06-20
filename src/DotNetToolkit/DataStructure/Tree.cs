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

using System.Linq;

// modify the namespace as you wish
namespace System.Collections.Generic
{
    /// <summary>
    /// Represents a tree data structure in which each element is attached to one or more elements directly beneath it.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class Tree<TValue> : ITree<TValue>,IReadOnlyTree<TValue>
    {
        private List<Tree<TValue>> _children;

        /// <summary>Initializes a new instance of the <see cref="Tree{Value}"/> class.</summary>
        public Tree() { }

        /// <summary>Initializes a new instance of the <see cref="Tree{Value}"/> class.</summary>
        /// <param name="value">The data value stored by the current tree.</param>
        public Tree(TValue value)
        {
            Value = value;
        }

        /// <summary>Indicating if this tree have a parent tree.</summary>
        public bool HaveParent { get { return Parent != null; } }

        /// <summary>Indicating if this tree have children.</summary>
        public bool HaveChildren
        {
            get
            {
                if (_children == null)
                {
                    return false;
                }
                return _children.Count > 0;
            }
        }

        /// <summary>Gets the parent of the current tree.</summary>
        public Tree<TValue> Parent { get; private set; }

        /// <summary>The data value stored by the current tree.</summary>
        public TValue Value { get; set; }


        /// <summary>Creates a new <see cref="Tree{TValue}"/> using a specified value 
        /// and adds it to the end of the <see cref="Children"/> collection.</summary>
        /// <param name="childValue">The value stored by the tree.</param>
        /// <returns>The tree that was added to the <see cref="Children"/> collection.</returns>
        public Tree<TValue> AddChild(TValue childValue = default)
        {
            Tree<TValue> child = new Tree<TValue>(childValue);
            child.Parent = this;
            InitializeChildrenIfNull();
            _children.Add(child);
            return child;
        }

        /// <summary>
        /// Creates a new <see cref="Tree{TValue}" /> using a specified value
        /// and inserts it at a specified index to the <see cref="Children" /> collection.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="childValue">The value stored by the tree.</param>
        /// <returns>The tree that was inserted to the <see cref="Children" /> collection.</returns>
        public Tree<TValue> InsertChild(int index, TValue childValue = default)
        {
            Tree<TValue> child = new Tree<TValue>(childValue);
            child.Parent = this;
            InitializeChildrenIfNull();
            _children.Insert(index, child);
            return child;
        }

        /// <summary>Removes all children from the <see cref="Children"/> collection.</summary>
        public void RemoveChildren()
        {
            if (_children != null)
            {
                _children.Clear();
            }
        }

        /// <summary> Removes the first occurrence of a tree with a specific value from the <see cref="Children" /> collection.</summary>
        /// <param name="child">The tree to be removed from the <see cref="Children" /> collection.</param>
        /// <returns><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
        public bool RemoveChild(Tree<TValue> child)
        {
            if (_children == null)
            {
                return false;
            }
            return _children.Remove(child);
        }

        /// <summary>Gets the children of the current tree.</summary>
        public IEnumerable<Tree<TValue>> Children()
        {
            return _children;
        }

        /// <summary>Returns a collection of items that contain the ancestors of this tree.</summary>
        public IEnumerable<Tree<TValue>> Ancestors()
        {
            Tree<TValue> tree = Parent;
            while (tree != null)
            {
                yield return tree;
                tree = tree.Parent;
            }
        }

        /// <summary>Returns a collection of items that contain this tree, and the ancestors of this tree.</summary>
        public IEnumerable<Tree<TValue>> AncestorsAndSelf()
        {
            yield return this;
            foreach (var ancestor in Ancestors())
            {
                yield return ancestor;
            }
        }

        /// <summary>Returns a collection of items that contain the descendants of this tree.</summary>
        public IEnumerable<Tree<TValue>> Descendants()
        {
            if (_children != null)
            {
                foreach (Tree<TValue> child in _children)
                {
                    yield return child;
                    foreach (Tree<TValue> descendant in child.Descendants())
                    {
                        yield return descendant;
                    }
                }
            }
        }

        /// <summary>Returns a collection of items that contain this tree, and the descendants of this tree.</summary>
        public IEnumerable<Tree<TValue>> DescendantsAndSelf()
        {
            yield return this;
            foreach (var descendant in Descendants())
            {
                yield return descendant;
            }
        }

        /// <summary>Returns a collection of items that contain the siblings of this tree.</summary>
        public IEnumerable<Tree<TValue>> Siblings()
        {
            return Parent.Children().Where(sibling => sibling != this);
        }

        /// <summary>Returns a collection of items that contain this tree, and the siblings of this tree.</summary>
        public IEnumerable<Tree<TValue>> SiblingsAndSelf()
        {
            yield return this;
            foreach (var sibling in Siblings())
            {
                yield return sibling;
            }
        }

        private void InitializeChildrenIfNull()
        {
            if (_children == null)
            {
                _children = new List<Tree<TValue>>();
            }
        }

        #region ITree

        bool ITree<TValue>.HaveParent => HaveParent;

        bool ITree<TValue>.HaveChildren => HaveChildren;

        ITree<TValue> ITree<TValue>.Parent => Parent;

        TValue ITree<TValue>.Value { get => Value; set => Value = value; }


        ITree<TValue> ITree<TValue>.AddChild(TValue childValue)
        {
            return AddChild(childValue);
        }

        ITree<TValue> ITree<TValue>.InsertChild(int index, TValue childValue)
        {
            return InsertChild(index, childValue);
        }

        void ITree<TValue>.RemoveChildren()
        {
            RemoveChildren();
        }

        bool ITree<TValue>.RemoveChild(ITree<TValue> child)
        {
            if (child is Tree<TValue> childToRemove)
            {
                return RemoveChild(childToRemove);
            }
            return false;
        }

        IEnumerable<ITree<TValue>> ITree<TValue>.Children()
        {
            return Children();
        }

        IEnumerable<ITree<TValue>> ITree<TValue>.Ancestors()
        {
            return Ancestors();
        }

        IEnumerable<ITree<TValue>> ITree<TValue>.AncestorsAndSelf()
        {
            return AncestorsAndSelf();
        }

        IEnumerable<ITree<TValue>> ITree<TValue>.Descendants()
        {
            return Descendants();
        }

        IEnumerable<ITree<TValue>> ITree<TValue>.DescendantsAndSelf()
        {
            return DescendantsAndSelf();
        }

        IEnumerable<ITree<TValue>> ITree<TValue>.Siblings()
        {
            return Siblings();
        }

        IEnumerable<ITree<TValue>> ITree<TValue>.SiblingsAndSelf()
        {
            return SiblingsAndSelf();
        }

        #endregion

        #region IReadOnlyTree

        bool IReadOnlyTree<TValue>.HaveParent => HaveParent;

        bool IReadOnlyTree<TValue>.HaveChildren => HaveChildren;

        IReadOnlyTree<TValue> IReadOnlyTree<TValue>.Parent => Parent;

        TValue IReadOnlyTree<TValue>.Value => Value;

        IEnumerable<IReadOnlyTree<TValue>> IReadOnlyTree<TValue>.Children()
        {
            return Children();
        }

        IEnumerable<IReadOnlyTree<TValue>> IReadOnlyTree<TValue>.Ancestors()
        {
            return Ancestors();
        }

        IEnumerable<IReadOnlyTree<TValue>> IReadOnlyTree<TValue>.AncestorsAndSelf()
        {
            return AncestorsAndSelf();
        }

        IEnumerable<IReadOnlyTree<TValue>> IReadOnlyTree<TValue>.Descendants()
        {
            return Descendants();
        }

        IEnumerable<IReadOnlyTree<TValue>> IReadOnlyTree<TValue>.DescendantsAndSelf()
        {
            return DescendantsAndSelf();
        }

        IEnumerable<IReadOnlyTree<TValue>> IReadOnlyTree<TValue>.Siblings()
        {
            return Siblings();
        }

        IEnumerable<IReadOnlyTree<TValue>> IReadOnlyTree<TValue>.SiblingsAndSelf()
        {
            return SiblingsAndSelf();
        }

        #endregion


    }
}