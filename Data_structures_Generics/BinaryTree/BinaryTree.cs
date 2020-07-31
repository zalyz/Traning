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
