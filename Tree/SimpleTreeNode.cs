// This collection of non-binary tree data structures created by Dan Vanderboom.
// Critical Development blog: http://dvanderboom.wordpress.com
// Original Tree<T> blog article: http://dvanderboom.wordpress.com/2008/03/15/treet-implementing-a-non-binary-tree-in-c/

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// Represents a node in a SimpleTree structure, with a parent node and zero or more child nodes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleTreeNode<T> : IDisposable
    {
        private SimpleTreeNode<T> _Parent;
        public SimpleTreeNode<T> Parent
        {
            get { return _Parent; }
            set
            {
                if (value == _Parent)
                    return;

                if (_Parent != null)
                    _Parent.Children.Remove(this);

                if (value != null && !value.Children.Contains(this))
                    value.Children.Add(this);

                _Parent = value;
            }
        }

        public SimpleTreeNode<T> Root
        {
            get
            {
                //return (Parent == null) ? this : Parent.Root;

                SimpleTreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                }
                return node;
            }
        }

        private SimpleTreeNodeList<T> _Children;
        public SimpleTreeNodeList<T> Children
        {
            get { return _Children; }
            private set { _Children = value; }
        }

        private T _Value;
        public T Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private TreeTraversalType _DisposeTraversalType = TreeTraversalType.PostOrder;

        public TreeTraversalType DisposeTraversalType
        {
            get { return _DisposeTraversalType; }
            set { _DisposeTraversalType = value; }
        }

        public SimpleTreeNode()
        {
            Parent = null;
            Children = new SimpleTreeNodeList<T>(this);
        }

        public SimpleTreeNode(T Value)
        {
            this.Value = Value;
            Children = new SimpleTreeNodeList<T>(this);
        }

        public SimpleTreeNode(SimpleTreeNode<T> Parent)
        {
            this.Parent = Parent;
            Children = new SimpleTreeNodeList<T>(this);
        }

        public SimpleTreeNode(SimpleTreeNodeList<T> Children)
        {
            Parent = null;
            this.Children = Children;
            Children.Parent = this;
        }

        public SimpleTreeNode(SimpleTreeNode<T> Parent, SimpleTreeNodeList<T> Children)
        {
            this.Parent = Parent;
            this.Children = Children;
            Children.Parent = this;
        }

        /// <summary>
        /// Reports a depth of nesting in the tree, starting at 0 for the root.
        /// </summary>
        public int Depth
        {
            get
            {
                //return (Parent == null ? -1 : Parent.Depth) + 1;

                int depth = 0;
                SimpleTreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                    depth++;
                }
                return depth;
            }
        }

        public override string ToString()
        {
            string Description = "[" + (Value == null ? "<null>" : Value.ToString()) + "] ";

            Description += "Depth=" + Depth.ToString() + ", Children=" + Children.Count.ToString();

            if (Root == this)
                Description += " (Root)";

            return Description;
        }

        #region IDisposable

        private bool _IsDisposed;
        public bool IsDisposed
        {
            get { return _IsDisposed; }
        }

        public IEnumerable<SimpleTreeNode<T>> GetEnumerable(TreeTraversalType TraversalType)
        {
            switch (TraversalType)
            {
                case TreeTraversalType.PreOrder: return GetPreOrderEnumerable();
                case TreeTraversalType.BreadthFirst: return GetBreadthFirstEnumerable();
                case TreeTraversalType.PostOrder: return GetPostOrderEnumerable();
            }
            return null;
        }

        public IEnumerable<SimpleTreeNode<T>> GetPreOrderEnumerable()
        {
            var stack = new Stack<SimpleTreeNode<T>>();
            stack.Push(this);


            while (stack.Count != 0)
            {
                var current = stack.Pop();
                foreach (var c in current.Children)
                {
                    stack.Push(c);
                }
                yield return current;
            }
        }

        public IEnumerable<SimpleTreeNode<T>> GetPostOrderEnumerable()
        {
            var child = new Stack<SimpleTreeNode<T>>();
            var parent = new Stack<SimpleTreeNode<T>>();

            child.Push(this);

            while (child.Count != 0)
            {
                var curr = child.Pop();
                parent.Push(curr);
                foreach (var s in curr.Children) child.Push(s);
            }
            return parent;

        }

        // TODO: adjust for traversal direction
        public IEnumerable<SimpleTreeNode<T>> GetBreadthFirstEnumerable()
        {
            var queue = new Queue<SimpleTreeNode<T>>();
            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                yield return node;
                foreach (var s in node.Children) queue.Enqueue(s);
            }
        }

        // TODO: update this to use GetEnumerator once that's working
        public virtual void Dispose()
        {
            CheckDisposed();


            //// clean up contained objects (in Value property)
            //if (DisposeTraversalDirection == TreeTraversalDirection.BottomUp)
            //{
            //    foreach (SimpleTreeNode<T> node in Children)
            //    {
            //        node.Dispose();
            //    }
            //}

            OnDisposing();
            var en = GetEnumerable(DisposeTraversalType);
            foreach (var item in en)
            {
                item.Dispose();

            }
            //if (DisposeTraversalDirection == TreeTraversalDirection.TopDown)
            //{
            //    foreach (SimpleTreeNode<T> node in Children)
            //    {
            //        node.Dispose();
            //    }
            //}
            _IsDisposed = true;
        }

        public event EventHandler Disposing;

        protected void OnDisposing()
        {
            if (Disposing != null)
            {
                Disposing(this, EventArgs.Empty);
            }
        }

        protected void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        #endregion
    }
}