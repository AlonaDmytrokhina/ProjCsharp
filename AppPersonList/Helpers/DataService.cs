using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AppPersonList.Models;

namespace AppPersonList.Helpers
{
    public static class DataService
    {
        private static string FilePath => "users.json";

        public static List<Person> Load()
        {
            if (!File.Exists(FilePath)) return new List<Person>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();
        }


        public static void Save(IEnumerable<Person> people)
        {
            var json = JsonSerializer.Serialize(people, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
