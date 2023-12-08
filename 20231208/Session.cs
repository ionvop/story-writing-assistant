using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace _20231208 {
    internal static class Session {
        public static List<Book> Books = new();
        public static Book? CurrentBook;
        public static Chapter? CurrentChapter;
        public static string ApiKey = "";
        private static readonly string filePath = "Data.json";

        public static void Save() {
            Data data = new() {
                Books = Books,
                ApiKey = ApiKey
            };

            string json = JsonConvert.SerializeObject(data);

            try {
                File.WriteAllText(filePath, json);
            } catch (Exception ex) {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        public static void Load() {
            try {
                if (File.Exists(filePath)) {
                    string json = File.ReadAllText(filePath);
                    Data? data = JsonConvert.DeserializeObject<Data>(json);
                    Books = data?.Books ?? new List<Book>();
                    ApiKey = data?.ApiKey ?? "";
                }
            } catch (Exception ex) {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }
    }

    class Data {
        public List<Book>? Books;
        public string? ApiKey;
    }
}
