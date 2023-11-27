using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace lab6
{
    [Serializable]
    public class TimesList
    {
        public List<TimeItem> Items { get; set; } = new List<TimeItem>();

        public void Add(TimeItem item)
        {
            Items.Add(item);
        }

        public void Save(string filename)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(Items, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filename, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        public void Load(string filename)
        {
            try
            {
                string jsonString = File.ReadAllText(filename);
                Items = JsonSerializer.Deserialize<List<TimeItem>>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }

        public void Print()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("| Matrix Order | Repeat Count | C# Execution Time | C++ Execution Time | Execution Time Ratio |");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            foreach (var item in Items)
            {
                Console.WriteLine($"| {item.MatrixOrder,12} | {item.RepeatCount,13} | {item.CSharpExecutionTime,17:F4} | {item.CppExecutionTime,18:F4} | {item.ExecutionTimeRatio,21:F4} |");
            }

            Console.WriteLine("------------------------------------------------------------------------------------------------");
        }
    }
}