using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerializableContainer.Tests
{
    [TestClass()]
    public class SerializableContainerTests
    {
        private TestSerializableClass _serializableObject;

        private List<TestSerializableClass> _serializableList;

        [TestInitialize]
        public void TestInitialize()
        {
            var firstTestObject = new TestSerializableClass { Name = "Bob", Age = 30 };
            _serializableObject = firstTestObject;
            var secondTestObject = new TestSerializableClass { Name = "Alice", Age = 15 };
            var list = new List<TestSerializableClass>
            {
                firstTestObject,
                secondTestObject
            };

            _serializableList = list;
        }

        [TestMethod()]
        public void XmlSerializing_InputSerializableObject_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.XmlSerializing(_serializableObject, "XmlFile.xml");
        }

        [TestMethod()]
        public void XmlDeserializing_SerializedObjectReturned()
        {
            var expected = _serializableObject;
            var actual = new TestSerializableClass();
            SerializableContainer<TestSerializableClass>.XmlDeserializing(ref actual, "XmlFile.xml");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void XmlSerializing_InputSerializableCollection_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.XmlSerializing(_serializableList, "XmlFile.xml");
        }
        
        [TestMethod()]
        public void XmlDeserializing_SerializedCollectionReturned()
        {
            var actual = new List<TestSerializableClass>();
            SerializableContainer<TestSerializableClass>.XmlDeserializing(actual, "XmlFile.xml");
            CollectionAssert.AreEqual(_serializableList, actual);
        }

        [TestMethod()]
        public void JsonSerializing_InputSerializableObject_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.JsonSerializing(_serializableObject, "JsonFile.json");
        }

        [TestMethod()]
        public void JsonDeserializing_SerializedObjectReturned()
        {
            var expected = _serializableObject;
            var actual = new TestSerializableClass();
            SerializableContainer<TestSerializableClass>.JsonDeserializing(ref actual, "JsonFile.json");
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void JsonSerializing_InputSerializableCollection_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.JsonSerializing(_serializableList, "JsonFile.json");
        }

        [TestMethod()]
        public void JsonDeserializing_SerializedCollectionReturned()
        {
            var actual = new List<TestSerializableClass>();
            SerializableContainer<TestSerializableClass>.JsonDeserializing(actual, "JsonFile.json");
            CollectionAssert.AreEqual(_serializableList, actual);
        }

        [TestMethod()]
        public void BinarySerializing_InputSerializableObject_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.BinarySerializing(_serializableObject, "BinaryFile.bin");
        }

        [TestMethod()]
        public void BinaryDeserializing_SerializedObjectReturned()
        {
            var expected = _serializableObject;
            var actual = new TestSerializableClass();
            SerializableContainer<TestSerializableClass>.BinaryDeserializing(ref actual, "BinaryFile.bin");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinarySerializing_InputSerializableCollection_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.BinarySerializing(_serializableList, "BinaryFile.bin");
        }

        [TestMethod()]
        public void BinaryDeserializing_SerializedCollectionReturned()
        {
            var actual = new List<TestSerializableClass>();
            SerializableContainer<TestSerializableClass>.BinaryDeserializing(actual, "BinaryFile.bin");
            CollectionAssert.AreEqual(_serializableList, actual);
        }
    }
}