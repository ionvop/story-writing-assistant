using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace _20231208 {
    internal static class Control {
        // List of books that are being managed by the application
        public static List<Book> Books = new();

        // The currently selected book
        public static Book? CurrentBook { get; set; }

        // The currently selected chapter within the current book
        public static Chapter? CurrentChapter { get; set; }

        // API key used for OpenAI services (initialized to an empty string)
        public static string ApiKey = "";

        // File path where data is saved
        private static readonly string filePath = "Data.json";

        // Save the current state of books and API key to a JSON file
        public static void Save() {
            // Create a new Data object to represent the current state
            Data data = new() {
                Books = Books,
                ApiKey = ApiKey
            };

            // Serialize the Data object to JSON format
            string json = JsonConvert.SerializeObject(data);

            try {
                // Write the JSON data to the specified file path
                File.WriteAllText(filePath, json);
            } catch (Exception ex) {
                // Handle any exceptions that occur during the save operation
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        // Load previously saved data from the JSON file
        public static void Load() {
            try {
                // Check if the data file exists
                if (File.Exists(filePath)) {
                    // Read the JSON data from the file
                    string json = File.ReadAllText(filePath);

                    // Deserialize the JSON data into a Data object
                    Data? data = JsonConvert.DeserializeObject<Data>(json);

                    // Update the application's state based on the loaded data
                    Books = data?.Books ?? new();
                    ApiKey = data?.ApiKey ?? "";
                }
            } catch (Exception ex) {
                // Handle any exceptions that occur during the load operation
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        // Navigate to a specified page
        public static void Navigate(Window window, string uri) {
            // Save the current state before navigating
            Save();

            // Check if the window is of type MainWindow
            if (window is MainWindow mainWindow) {
                // Use the MainWindow's Navigate method to navigate to the specified page
                mainWindow.Navigate(new(uri, UriKind.Relative));
            }
        }
    }

    class Data {
        public List<Book>? Books;
        public string? ApiKey;
    }
}
