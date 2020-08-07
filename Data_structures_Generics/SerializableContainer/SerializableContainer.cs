using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializableContainer
{
    /// <summary>
    /// Defines methods for serializing to Xml, Json and Binary files.
    /// </summary>
    /// <typeparam name="T">Class for serializing.</typeparam>
    public static class SerializableContainer<T>
        where T : class
    {
        /// <summary>
        /// Serializes object to the Xml file.
        /// </summary>
        /// <param name="serializableObject">Serializable object.</param>
        /// <param name="path">File path.</param>
        public static void XmlSerializing(T serializableObject, string path)
        {
            var formatter = new XmlSerializer(typeof(T));
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(writer, serializableObject);
            }
        }

        /// <summary>
        /// Serializes collection to the Xml file.
        /// </summary>
        /// <param name="serializableCollection">Serializable collection of the object.</param>
        /// <param name="path">File path.</param>
        public static void XmlSerializing(ICollection<T> serializableCollection, string path)
        {
            var list = serializableCollection.ToList();
            var formatter = new XmlSerializer(typeof(List<T>));
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(writer, list);
            }
        }

        /// <summary>
        /// Deserializes object from the Xml file.
        /// </summary>
        /// <param name="deserializableObject">Deserializable object.</param>
        /// <param name="path">File path.</param>
        public static void XmlDeserializing(ref T deserializableObject, string path)
        {
            if (IsValidVersion())
            {
                var formatter = new XmlSerializer(typeof(T));
                using (var reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    deserializableObject = (T)formatter.Deserialize(reader);
                }
            }
        }

        /// <summary>
        /// Deserializes collection from the Xml file.
        /// </summary>
        /// <param name="deserializableCollection">Deserializable collection of the object.</param>
        /// <param name="path">File path.</param>
        public static void XmlDeserializing(ICollection<T> deserializableCollection, string path)
        {
            if (IsValidVersion())
            {
                var formatter = new XmlSerializer(typeof(List<T>));
                ICollection<T> collection;
                using (var reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    collection = (ICollection<T>)formatter.Deserialize(reader);
                }

                FillCollection(deserializableCollection, collection);
            }
        }

        /// <summary>
        /// Serializes object to the Json file.
        /// </summary>
        /// <param name="serializableObject">Serializable object.</param>
        /// <param name="path">File path.</param>
        public static void JsonSerializing(T serializableObject, string path)
        {
            var formatter = new DataContractJsonSerializer(typeof(T));
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.WriteObject(writer, serializableObject);
            }
        }

        /// <summary>
        /// Serializes collection to the Json file.
        /// </summary>
        /// <param name="serializableCollection">Serializable collection of the object.</param>
        /// <param name="path">File path.</param>
        public static void JsonSerializing(ICollection<T> serializableCollection, string path)
        {
            var list = serializableCollection.ToList();
            var formatter = new DataContractJsonSerializer(typeof(List<T>));
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.WriteObject(writer, list);
            }
        }

        /// <summary>
        /// Deserializes object from the Json file.
        /// </summary>
        /// <param name="deserializableObject">Deserializable object.</param>
        /// <param name="path">File path.</param>
        public static void JsonDeserializing(ref T deserializableObject, string path)
        {
            if (IsValidVersion())
            {
                var formatter = new DataContractJsonSerializer(typeof(T));
                using (var reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    deserializableObject = (T)formatter.ReadObject(reader);
                }
            }
        }

        /// <summary>
        /// Deserializes collection from the Json file.
        /// </summary>
        /// <param name="deserializableCollection">Deserializable collection of the object.</param>
        /// <param name="path">File path.</param>
        public static void JsonDeserializing(ICollection<T> deserializableCollection, string path)
        {
            if (IsValidVersion())
            {
                var formatter = new DataContractJsonSerializer(typeof(List<T>));
                ICollection<T> collection;
                using (var reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    collection = (ICollection<T>)formatter.ReadObject(reader);
                }

                FillCollection(deserializableCollection, collection);
            }
        }

        /// <summary>
        /// Serializes object to the Binary file.
        /// </summary>
        /// <param name="serializableObject">Serializable object.</param>
        /// <param name="path">File path.</param>
        public static void BinarySerializing(T serializableObject, string path)
        {
            var formatter = new BinaryFormatter();
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(writer, serializableObject);
            }
        }

        /// <summary>
        /// Serializes collection to the Binary file.
        /// </summary>
        /// <param name="serializableCollection">Serializable collection of the object.</param>
        /// <param name="path">File path.</param>
        public static void BinarySerializing(ICollection<T> serializableCollection, string path)
        {
            var formatter = new BinaryFormatter();
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(writer, serializableCollection);
            }
        }

        /// <summary>
        /// Deserializes object from the Binary file.
        /// </summary>
        /// <param name="deserializableObject">Deserializable object.</param>
        /// <param name="path">File path.</param>
        public static void BinaryDeserializing(ref T deserializableObject, string path)
        {
            if (IsValidVersion())
            {
                var formatter = new BinaryFormatter();
                using (var reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    deserializableObject = (T)formatter.Deserialize(reader);
                }
            }
        }

        /// <summary>
        /// Deserializes collection from the Binary file.
        /// </summary>
        /// <param name="deserializableCollection">Deserializable collection of the object.</param>
        /// <param name="path">File path.</param>
        public static void BinaryDeserializing(ICollection<T> deserializableCollection, string path)
        {
            if (IsValidVersion())
            {
                var formatter = new BinaryFormatter();
                ICollection<T> collection;
                using (var reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    collection = (ICollection<T>)formatter.Deserialize(reader);
                }

                FillCollection(deserializableCollection, collection);
            }
        }

        /// <summary>
        /// Checks the version of the serialezed object and version of the current class.
        /// </summary>
        /// <returns>True if versions are equal, False otherwise.</returns>
        private static bool IsValidVersion()
        {
            using (var reader = new StreamReader("Version.txt"))
            {
                var fileVersion = typeof(T).Assembly.GetName().Version.ToString();
                var savedVersion = reader.ReadLine();
                if (string.Equals(fileVersion, savedVersion))
                    return true;

                throw new Exception();
            }
        }

        /// <summary>
        /// Saves version of the current class.
        /// </summary>
        private static void SaveVersion()
        {
            using (var writer = new StreamWriter("Version.txt"))
            {
                var fileVersion = typeof(T).Assembly.GetName().Version;

                writer.WriteLine(fileVersion);
            }
        }

        /// <summary>
        /// Fills the returned collection with deserialized objects.
        /// </summary>
        /// <param name="deserializableCollection">Returned collection.</param>
        /// <param name="collection">Collection of desirealizabled objects.</param>
        private static void FillCollection(ICollection<T> deserializableCollection, ICollection<T> collection)
        {
            foreach (var item in collection)
            {
                deserializableCollection.Add(item);
            }
        }
    }
}
