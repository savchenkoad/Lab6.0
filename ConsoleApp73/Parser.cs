using Newtonsoft.Json;
using System.Xml.Serialization;

namespace ConsoleApp73
{
    public static class Parser
    {
        public static event EventHandler? OnSerializing;

        public static void WriteIntoFile(string filePath, object? obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Append)))
            {
                sw.Write($"{obj} \n");
            };
        }

        public static void SerializeXml(string filePath, object? obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException();
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worker));

            using (FileStream fs = new FileStream(filePath, FileMode.Append))
            {
                xmlSerializer.Serialize(fs, obj);

                OnSerializing?.Invoke(obj, EventArgs.Empty);
            }
        }

        public static void SerializeJson(string filePath, object? obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException();
            }

            var serializedObject = JsonConvert.SerializeObject(obj);

            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Append)))
            {
                sw.WriteLine(serializedObject);

                OnSerializing?.Invoke(obj, EventArgs.Empty);
            }
        }

        public static List<object> DeserializeAllJson(string filePath)
        {
            var result = new List<object>();

            using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open)))
            {
                if (sr.Peek() > -1)
                {
                    result.Add(JsonConvert.DeserializeObject(sr.ReadLine()));
                }
            }

            return result;
        }
    }
}
