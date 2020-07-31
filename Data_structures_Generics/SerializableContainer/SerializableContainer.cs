using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System;

namespace SerializableContainer
{

    public class SerializableContainer<T>
        where T : class
    {
        public T SerializableObject { get; private set; }

        public SerializableContainer(T serializableObject)
        {
            SerializableObject = serializableObject;
        }

        private bool IsValidVersion()
        {
            using (var reader = new StreamReader("Version.txt"))
            {
                if (typeof(T).Assembly.GetName().Version.ToString() == reader.ReadLine())
                    return true;

                throw new Exception();
            }
        }

        private void SaveVersion()
        {
            using (var writer = new StreamWriter("Version.txt"))
            {
                writer.WriteLine(typeof(T).Assembly.GetName().Version.ToString());
            }
        }

        public void XmlSerializing(string path)
        {
            var formatter = new XmlSerializer(typeof(T));
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(writer, SerializableObject);
            }
        }

        public void XmlDeserializing(string path)
        {
            if (IsValidVersion())
            {
                var formatter = new XmlSerializer(typeof(T));
                using (var reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    SerializableObject = (T)formatter.Deserialize(reader);
                }
            }
        }

        public void JsonSerializing(string path)
        {
            var formatter = new DataContractJsonSerializer(typeof(T));
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.WriteObject(writer, SerializableObject);
            }
        }

        public void JsonDeserializing(string path)
        {
            if (IsValidVersion())
            {
                var formatter = new DataContractJsonSerializer(typeof(T));
                using (var reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    SerializableObject = (T)formatter.ReadObject(reader);
                }
            }
        }

        public void BinarySerializing(string path)
        {
            var formatter = new BinaryFormatter();
            SaveVersion();
            using (var writer = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(writer, SerializableObject);
            }
        }

        public void BinaryDeserializing(string path)
        {
            if (IsValidVersion())
            {
                var formatter = new BinaryFormatter();
                using (var reader = new FileStream(path, FileMode.OpenOrCreate))
                {
                    SerializableObject = (T)formatter.Deserialize(reader);
                }
            }
        }
    }
}
