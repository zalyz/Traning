using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace SerializableContainer
{
    [Serializable]
    public class TestSerializableClass
    {

        public string Name { get; set; }

        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as TestSerializableClass);
        }

        public bool Equals(TestSerializableClass other)
        {
            return Name == other.Name &&
                Age == other.Age;
        }

        public override int GetHashCode()
        {
            return (Name, Age).GetHashCode();
        }

        public override string ToString()
        {
            return $"Name is {Name} and age is {Age} years old.";
        }
    }
}
