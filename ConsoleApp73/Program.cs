namespace ConsoleApp73
{
    internal class Program
    {
        static async Task Main()
        {
            Parser.OnSerializing += Parser_OnSerializing;

            Worker[] workers = GetWorkers();

            Array.Sort(workers);

            var xmlSerializingTask = Parser.SerializeAllXml("worker.xml", workers);
            var jsonSerializingTask = Parser.SerializeAllJson("worker.json", workers);

            Parser.WriteAllIntoFile("worker.txt", workers);

            Console.WriteLine("Enter the experience");
            int.TryParse(Console.ReadLine(), out int experience);

            PrintWorkersWithExperienceMoreThan(experience, workers);

            var efw = Parser.DeserializeAllJson("worker.json");

            await Task.WhenAll(xmlSerializingTask, jsonSerializingTask);
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