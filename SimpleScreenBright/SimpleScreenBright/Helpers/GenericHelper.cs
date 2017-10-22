using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimpleScreenBright.Helpers
{
    public static class GenericHelper
    {
        public static bool IsNumeric(string s)
        {
            int n;
            bool isNumeric = int.TryParse(s, out n);

            return isNumeric;
        }

        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        public static void WriteToBinaryFileStream<T>(Stream stream, T objectToWrite)
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
        }

        public static T ReadFromBinaryFileStream<T>(Stream stream)
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }

    }
}
