using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

namespace BinaryTree
{
    /// <summary>
    /// Defines methods for working with the binary tree.
    /// </summary>
    /// <typeparam name="T"> Any class to store. Must implement IComparable.</typeparam>
    public class BinaryTree<T> where T : class
    {
        /// <summary>
        /// Root of the binary tree.
        /// </summary>
        private Node _root;

        /// <summary>
        /// Adds a node to the binary tree.
        /// </summary>
        /// <param name="record"> Record for adding.</param>
        public void Add(T record)
        {
            if (record == null)
                throw new ArgumentNullException();

            if (_root == null)
            {
                _root = new Node(record) { Left = null, Right = null };
            }
            else
            {
                var nextNode = _root;
                var comparableRecord = record as IComparable;
                while (true)
                {
                    if (comparableRecord.CompareTo(nextNode.Value) <= 0)
                    {
                        if (nextNode.Left == null)
                        {
                            nextNode.Left = new Node(record) { Left = null, Right = null };
                            break;
                        }
                        else
                        {
                            nextNode = nextNode.Left;
                        }
                    }
                    else
                    {
                        if (nextNode.Right == null)
                        {
                            nextNode.Right = new Node(record) { Left = null, Right = null };
                            break;
                        }
                        else
                        {
                            nextNode = nextNode.Right;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Balancing binary tree.
        /// </summary>
        public void BalanceTheTree()
        {
            TreeBalancing(ref _root);
        }

        /// <summary>
        /// Writes a binary tree to an Xml file.
        /// </summary>
        /// <param name="path"> File path.</param>
        public void WriteToXml(string path)
        {
            List<T> listOfNodes = new List<T>();
            ConvertTreeToList(_root, listOfNodes);
            var xmlFormatter = new XmlSerializer(typeof(List<T>));
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                xmlFormatter.Serialize(writer, listOfNodes);

            }
        }

        /// <summary>
        /// Reades a binary tree from an Xml file.
        /// </summary>
        /// <param name="path">File path.</param>
        public void ReadFromXml(string path)
        {
            List<T> listOfNodes;
            var xmlFormatter = new XmlSerializer(typeof(List<T>));
            using (var reader = new FileStream(path, FileMode.OpenOrCreate))
            {
                listOfNodes = (List<T>)xmlFormatter.Deserialize(reader);

            }

            _root = null;
            CreateTreeFromList(listOfNodes);
        }

        /// <summary>
        /// Gets a list that contains all tree nodes.
        /// </summary>
        /// <returns>Nodes list.</returns>
        public List<T> GetTreeValues()
        {
            var treeValues = new List<T>();
            ConvertTreeToList(_root, treeValues);
            return treeValues;
        }

        /// <summary>
        /// Creates binary tree from list.
        /// </summary>
        /// <param name="list">List with nodes.</param>
        private void CreateTreeFromList(List<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Convers list from binary tree.
        /// </summary>
        /// <param name="node">Start Node for convering.</param>
        /// <param name="list">List whith tree nodes.</param>
        private void ConvertTreeToList(Node node, List<T> list)
        {
            if (node != null)
            {
                list.Add(node.Value);
                ConvertTreeToList(node.Left, list);


                ConvertTreeToList(node.Right, list);
            }
        }

        /// <summary>
        /// Turns binary tree to the left for balancing.
        /// </summary>
        /// <param name="node">The node to rotate.</param>
        private void LeftTurn(ref Node node)
        {
            var oldRoot = node;
            var newRoot = node.Right;
            oldRoot.Right = newRoot.Left;
            newRoot.Left = oldRoot;
            node = newRoot;
        }

        /// <summary>
        /// Turns binary tree to the right for balancing.
        /// </summary>
        /// <param name="node">The node to rotate.</param>
        private void RightTurn(ref Node node)
        {
            var oldRoot = node;
            var newRoot = node.Left;
            oldRoot.Left = newRoot.Right;
            newRoot.Right = oldRoot;
            node = newRoot;
        }

        /// <summary>
        /// Balancing binary tree algorithm.
        /// </summary>
        /// <param name="node">Start Node for balancing.</param>
        private void TreeBalancing(ref Node node)
        {
            if (node != null)
            {
                TreeBalancing(ref node.Left);
                if (node != _root)
                {
                    node = NodeBalansing(node);
                }

                TreeBalancing(ref node.Right);

                if (node == _root)
                {
                    node = NodeBalansing(node);
                }
            }
        }

        /// <summary>
        /// Balancing left and right subtrees.
        /// </summary>
        /// <param name="node">Root of subtrees.</param>
        /// <returns>Balancing root.</returns>
        private Node NodeBalansing(Node node)
        {
            var leftHeight = GetHeight(node.Left);
            var rightHeight = GetHeight(node.Right);
            if (Math.Abs(leftHeight - rightHeight) >= 2)
            {
                if (leftHeight > rightHeight)
                {
                    RightTurn(ref node);
                }
                else
                {
                    LeftTurn(ref node);
                }
            }

            return node;
        }

        /// <summary>
        /// Gets node height.
        /// </summary>
        /// <param name="node">Node to compute.</param>
        /// <returns>Node height.</returns>
        private int GetHeight(Node node)
        {
            if (node == null)
                return 0;

            return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right)); 
        }

        /// <summary>
        /// Represents an instance of a binary tree node.
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Node value.
            /// </summary>
            public T Value;

            /// <summary>
            /// Left subtree.
            /// </summary>
            public Node Left;

            /// <summary>
            /// Right subtree.
            /// </summary>
            public Node Right;

            /// <summary>
            /// Create instance of Node class.
            /// </summary>
            /// <param name="value">Node value.</param>
            public Node(T value)
            {
                Value = value;
            }
        }
    }
}
