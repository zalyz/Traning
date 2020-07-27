using System;

namespace BinaryTree
{
    public class BinaryTree<T> where T : class
    {
        private Node _root;

        public void Add(T record)
        {
            if (record == null)
                throw new ArgumentNullException();

            var nextNode = _root;
            var comparableRecord = record as IComparable;
            while (nextNode != null)
            {
                if (comparableRecord.CompareTo(nextNode.Value) <= 0)
                {
                    nextNode = nextNode.Left;
                }
                else
                {
                    nextNode = nextNode.Right;
                }
            }

            nextNode = new Node(record) { Left = null, Right = null };
        }

        public void LeftTurn()
        {
            var oldRoot = _root;
            var newRoot = _root.Right;
            oldRoot.Right = newRoot.Left;
            newRoot.Left = oldRoot;
            _root = newRoot;
        }

        public void RightTurn()
        {
            var oldRoot = _root;
            var newRoot = _root.Left;
            oldRoot.Left = newRoot.Right;
            newRoot.Right = oldRoot;
            _root = newRoot;
        }

        private class Node
        {
            public T Value;

            public Node Left;

            public Node Right;

            public Node(T value)
            {
                Value = value;
            }
        }
    }
}
