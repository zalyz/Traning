using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializableContainer
{

    public static class SerializableContainer<T>
        where T : class
    {
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

        private static void SaveVersion()
        {
            using (var writer = new StreamWriter("Version.txt"))
            {
                var fileVersion = typeof(T).Assembly.GetName().Version;

                writer.WriteLine(fileVersion);
            }
        }

        public static void XmlSerializing(T serializableObject, string path)
        {
            var formatter = new XmlSerializer(typeof(T));
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(writer, serializableObject);
            }
        }

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

        public static void JsonSerializing(T serializableObject, string path)
        {
            var formatter = new DataContractJsonSerializer(typeof(T));
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.WriteObject(writer, serializableObject);
            }
        }
        
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

        public static void BinarySerializing(T serializableObject, string path)
        {
            var formatter = new BinaryFormatter();
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(writer, serializableObject);
            }
        }
        
        public static void BinarySerializing(ICollection<T> serializableCollection, string path)
        {
            var formatter = new BinaryFormatter();
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(writer, serializableCollection);
            }
        }

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

        private static void FillCollection(ICollection<T> deserializableCollection, ICollection<T> collection)
        {
            foreach (var item in collection)
            {
                deserializableCollection.Add(item);
            }
        }
    }
}
