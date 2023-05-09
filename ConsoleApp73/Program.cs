namespace ConsoleApp73
{
    internal class Program
    {
        static async Task Main()
        {
            Parser.OnSerializing += Parser_OnSerializing;

            Worker[] workers = GetWorkers();

            Array.Sort(workers);

            var xmlSerializingTask = SerializeAllXml("worker.xml", workers);
            var jsonSerializingTask = SerializeAllJson("worker.json", workers);

            WriteAllIntoFile("worker.txt", workers);

            Console.WriteLine("Enter the experience");
            int.TryParse(Console.ReadLine(), out int experience);

            PrintWorkersWithExperienceMoreThan(experience, workers);

            var efw = Parser.DeserializeAllJson("worker.json");

            await Task.WhenAll(xmlSerializingTask, jsonSerializingTask);
        }

        async static Task SerializeAllXml<T>(string filePath, T[] objects) where T : struct
        {
            await Task.Delay(3000);

            await Task.Run(() =>
            {
                foreach (object obj in objects!)
                {
                    Parser.SerializeXml(filePath, obj);
                }
            });
        }

        async static Task SerializeAllJson<T>(string filePath, T[] objects)
        {
            await Task.Delay(4000);

            await Task.Run(() =>
            {
                foreach (var item in objects)
                {
                    Parser.SerializeJson(filePath, item);
                }
            });
        }

        static void WriteAllIntoFile<T>(string filePath, T[] values) where T : struct
        {
            foreach (var item in values)
            {
                Parser.WriteIntoFile(filePath, item);
            }
        }

        static void PrintWorkersWithExperienceMoreThan(int experience, Worker[] workers)
        {
            if (experience < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            workers.Where(x => x.Experience > experience)
                   .ToList()
                   .ForEach(x => Console.WriteLine(x));
        }

        static Worker[] GetWorkers()
        {
            Console.Write("Enter the employees count: ");
            int.TryParse(Console.ReadLine(), out var employeesCount);

            if (employeesCount < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var employees = new Worker[employeesCount];

            for (int i = 0; i < employeesCount; i++)
            {
                employees[i] = ConsoleInputHandler.ReadWorkerInfo();
            }

            return employees;
        }

        private static void Parser_OnSerializing(object? sender, EventArgs e)
        {
            Console.WriteLine("The object has been serialized");
        }
    }
}