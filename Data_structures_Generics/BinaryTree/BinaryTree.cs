using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

namespace BinaryTree
{
    public class BinaryTree<T> where T : class
    {
        private Node _root;

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

        public void TreeBalancing()
        {
            TreeBalancing(ref _root);
        }

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

        public List<T> GetTreeValues()
        {
            var treeValues = new List<T>();
            ConvertTreeToList(_root, treeValues);
            return treeValues;
        }

        private void CreateTreeFromList(List<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        private void ConvertTreeToList(Node node, List<T> list)
        {
            if (node != null)
            {
                list.Add(node.Value);
                ConvertTreeToList(node.Left, list);


                ConvertTreeToList(node.Right, list);
            }
        }

        private void LeftTurn(ref Node node)
        {
            var oldRoot = node;
            var newRoot = node.Right;
            oldRoot.Right = newRoot.Left;
            newRoot.Left = oldRoot;
            node = newRoot;
        }

        private void RightTurn(ref Node node)
        {
            var oldRoot = node;
            var newRoot = node.Left;
            oldRoot.Left = newRoot.Right;
            newRoot.Right = oldRoot;
            node = newRoot;
        }

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

        private int GetHeight(Node node)
        {
            if (node == null)
                return 0;

            return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right)); 
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
