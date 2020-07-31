using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerializableContainer.Tests
{
    [TestClass()]
    public class SerializableContainerTests
    {
        private SerializableContainer<List<TestSerializableClass>> _serializableContainer;

        [TestInitialize]
        public void TestInitialize()
        {
            var firstTestObject = new TestSerializableClass { Name = "Bob", Age = 30 };
            var secondTestObject = new TestSerializableClass { Name = "Alice", Age = 15 };
            var list = new List<TestSerializableClass>
            {
                firstTestObject,
                secondTestObject
            };
            _serializableContainer = new SerializableContainer<List<TestSerializableClass>>(list);
        }

        [TestMethod()]
        public void XmlSerializingTest()
        {
            _serializableContainer.XmlSerializing("XmlFile.xml");
        }

        [TestMethod()]
        public void XmlDeserializingTest()
        {
            var expected = _serializableContainer.SerializableObject;
            _serializableContainer.XmlDeserializing("XmlFile.xml");
            var actual = _serializableContainer.SerializableObject;
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void JsonSerializingTest()
        {
            _serializableContainer.JsonSerializing("JsonFile.json");
        }

        [TestMethod()]
        public void JsonDeserializingTest()
        {
            var expected = _serializableContainer.SerializableObject;
            _serializableContainer.JsonDeserializing("JsonFile.json");
            var actual = _serializableContainer.SerializableObject;
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BinarySerializingTest()
        {
            _serializableContainer.BinarySerializing("BinaryFile.bin");
        }

        [TestMethod()]
        public void BinaryDeserializingTest()
        {
            var expected = _serializableContainer.SerializableObject;
            _serializableContainer.BinaryDeserializing("BinaryFile.bin");
            var actual = _serializableContainer.SerializableObject;
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}