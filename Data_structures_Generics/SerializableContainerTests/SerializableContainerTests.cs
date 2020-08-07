using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SerializableContainer.Tests
{
    /// <summary>
    /// Defines tests methods for the SerializableContainer class.
    /// </summary>
    [TestClass()]
    public class SerializableContainerTests
    {
        /// <summary>
        /// Object for serializing to the file.
        /// </summary>
        private TestSerializableClass _serializableObject;

        /// <summary>
        /// Collection of objects for serializing to the file.
        /// </summary>
        private List<TestSerializableClass> _serializableList;

        /// <summary>
        /// Initializes the serializable object and collection.
        /// </summary>
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

        /// <summary>
        /// Serializes object to the Xml file.
        /// </summary>
        [TestMethod()]
        public void XmlSerializing_InputSerializableObject_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.XmlSerializing(_serializableObject, "XmlFile.xml");
        }

        /// <summary>
        /// Deserializes object from the Xml file.
        /// </summary>
        [TestMethod()]
        public void XmlDeserializing_SerializedObjectReturned()
        {
            var expected = _serializableObject;
            var actual = new TestSerializableClass();
            SerializableContainer<TestSerializableClass>.XmlDeserializing(ref actual, "XmlFile.xml");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Serializes collection to the Xml file.
        /// </summary>
        [TestMethod()]
        public void XmlSerializing_InputSerializableCollection_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.XmlSerializing(_serializableList, "XmlFile.xml");
        }

        /// <summary>
        /// Deserializes collection from the Xml file.
        /// </summary>
        [TestMethod()]
        public void XmlDeserializing_SerializedCollectionReturned()
        {
            var actual = new List<TestSerializableClass>();
            SerializableContainer<TestSerializableClass>.XmlDeserializing(actual, "XmlFile.xml");
            CollectionAssert.AreEqual(_serializableList, actual);
        }

        /// <summary>
        /// Serializes object to the Json file.
        /// </summary>
        [TestMethod()]
        public void JsonSerializing_InputSerializableObject_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.JsonSerializing(_serializableObject, "JsonFile.json");
        }

        /// <summary>
        /// Deserializes object from the Json file.
        /// </summary>
        [TestMethod()]
        public void JsonDeserializing_SerializedObjectReturned()
        {
            var expected = _serializableObject;
            var actual = new TestSerializableClass();
            SerializableContainer<TestSerializableClass>.JsonDeserializing(ref actual, "JsonFile.json");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Serializes collection to the Json file.
        /// </summary>
        [TestMethod()]
        public void JsonSerializing_InputSerializableCollection_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.JsonSerializing(_serializableList, "JsonFile.json");
        }

        /// <summary>
        /// Deserializes collection from the Json file.
        /// </summary>
        [TestMethod()]
        public void JsonDeserializing_SerializedCollectionReturned()
        {
            var actual = new List<TestSerializableClass>();
            SerializableContainer<TestSerializableClass>.JsonDeserializing(actual, "JsonFile.json");
            CollectionAssert.AreEqual(_serializableList, actual);
        }

        /// <summary>
        /// Serializes object to the Binary file.
        /// </summary>
        [TestMethod()]
        public void BinarySerializing_InputSerializableObject_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.BinarySerializing(_serializableObject, "BinaryFile.bin");
        }

        /// <summary>
        /// Deserializes object from the Binary file.
        /// </summary>
        [TestMethod()]
        public void BinaryDeserializing_SerializedObjectReturned()
        {
            var expected = _serializableObject;
            var actual = new TestSerializableClass();
            SerializableContainer<TestSerializableClass>.BinaryDeserializing(ref actual, "BinaryFile.bin");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Serializes collection to the Binary file.
        /// </summary>
        [TestMethod()]
        public void BinarySerializing_InputSerializableCollection_SuccessfullySerialization()
        {
            SerializableContainer<TestSerializableClass>.BinarySerializing(_serializableList, "BinaryFile.bin");
        }

        /// <summary>
        /// Deserializes collection from the Binary file.
        /// </summary>
        [TestMethod()]
        public void BinaryDeserializing_SerializedCollectionReturned()
        {
            var actual = new List<TestSerializableClass>();
            SerializableContainer<TestSerializableClass>.BinaryDeserializing(actual, "BinaryFile.bin");
            CollectionAssert.AreEqual(_serializableList, actual);
        }
    }
}