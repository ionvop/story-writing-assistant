using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace _20231208 {
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Page {
        public Editor() {
            InitializeComponent();

            // Check if there is a current chapter
            if (Control.CurrentChapter is Chapter chapter) {
                // If a current chapter exists, set the UI with its title, description, and content
                inputTitle.Text = chapter.Title;
                inputDescription.Text = chapter.Description;
                inputContent.Text = chapter.Content;
            }
        }

        // Event handler for the "Back" button click
        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            // Navigate back to the Chapter Select page
            Control.Navigate(Window.GetWindow(this), "ChapterSelect.xaml");
        }

        // For the following TextChanged events, they basically update the properties of the selected chapter with the user's new input
        private void InputTitle_TextChanged(object sender, TextChangedEventArgs e) {
            if (Control.CurrentChapter is Chapter chapter) {
                chapter.Title = inputTitle.Text;
            }
        }

        private void InputDescription_TextChanged(object sender, TextChangedEventArgs e) {
            if (Control.CurrentChapter is Chapter chapter) {
                chapter.Description = inputDescription.Text;
            }
        }

        private void InputContent_TextChanged(object sender, TextChangedEventArgs e) {
            if (Control.CurrentChapter is Chapter chapter) {
                chapter.Content = inputContent.Text;
            }
        }

        // Let the AI continue the story for you
        private async void BtnGenerate_Click(object sender, RoutedEventArgs e) {
            // Save the current state before generating content
            Control.Save();

            // Update button appearance to indicate loading
            btnGenerate.Content = "Loading...";
            btnGenerate.IsEnabled = false;

            // Determine the index of the current chapter
            int chapterIndex = Control.CurrentBook?.Chapters.IndexOf(Control.CurrentChapter ?? new()) ?? -1;

            // Prepare the content for OpenAI request
            string content = "Story title:\n" +
                Control.CurrentBook?.Title + "\n\n" +
                "Summary: \n" +
                Control.CurrentBook?.Description + "\n\n";

            // Include the summary and story of the previous chapter if available
            if (chapterIndex > 0) {
                content += Control.CurrentBook?.Chapters.ElementAt(chapterIndex - 1).Title + " summary:\n" +
                    Control.CurrentBook?.Chapters.ElementAt(chapterIndex - 1).Description + "\n\n" +
                    Control.CurrentBook?.Chapters.ElementAt(chapterIndex - 1).Title + " story:\n" +
                    Control.CurrentBook?.Chapters.ElementAt(chapterIndex - 1).Content + "\n\n";
            }

            // Include the summary and story of the current chapter
            content += Control.CurrentBook?.Chapters.ElementAt(chapterIndex).Title + " summary:\n" +
                Control.CurrentBook?.Chapters.ElementAt(chapterIndex).Description + "\n\n" +
                Control.CurrentBook?.Chapters.ElementAt(chapterIndex).Title + " story:\n" +
                Control.CurrentBook?.Chapters.ElementAt(chapterIndex).Content + "\n\n" +
                "[Continue the story within the same chapter in just one paragraph]";

            // Prepare messages for OpenAI request
            List<Message> messages = new() {
                new() {
                    Role = "system",
                    Content = "You will continue the story based on the given text."
                },

                new() {
                    Role = "user",
                    Content = content
                }
            };

            // Prepare the request data for OpenAI API
            Request requestData = new() {
                Model = "gpt-3.5-turbo",
                Messages = messages
            };

            // Serialize the request data to JSON
            string json = JsonConvert.SerializeObject(requestData, Formatting.Indented);

            // Set up HttpClient and prepare the request
            HttpClient client = new();
            HttpRequestMessage request = new(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Authorization", "Bearer " + Control.ApiKey);
            request.Content = new StringContent(json);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Send the request to OpenAI API
            HttpResponseMessage response = await client.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();

            // Check for errors in the response
            if (!response.IsSuccessStatusCode) {
                MessageBox.Show("Error: " + response.ReasonPhrase + "\n\n" + responseBody);
            }

            // Deserialize the OpenAI response
            Response res = JsonConvert.DeserializeObject<Response>(responseBody) ?? new();

            // Add the generated content to the story
            inputContent.Text += "\n\n" + res.Choices?.ElementAt(0).Message?.Content;

            // Restore the button to its original state
            btnGenerate.Content = "Generate";
            btnGenerate.IsEnabled = true;
        }
    }

    // The JsonProperty attribute is used to map class properties to JSON fields during the serialization and deserialization processes
    // This is because the JSON schema for the request and the response uses small letters for its fields
    // Visual Studio complains if I don't capitalize the first letter of a public property

    // Represents a message in a conversation, including its role (system, user) and content
    class Message {
        // Role of the message (system or user)
        [JsonProperty("role")]
        public string? Role { get; set; }

        // Content of the message
        [JsonProperty("content")]
        public string? Content { get; set; }
    }

    // Represents a request to the OpenAI API, including the model to use and a list of messages
    class Request {
        // Model identifier for OpenAI API
        [JsonProperty("model")]
        public string? Model { get; set; }

        // List of messages in the conversation
        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }
    }

    // Represents the response from the OpenAI API, including a list of choices
    class Response {
        // List of choices generated by the API
        [JsonProperty("choices")]
        public List<Choice>? Choices { get; set; }
    }

    // Represents a choice in the response, including the generated message
    class Choice {
        // The generated message as a result of the API request
        [JsonProperty("message")]
        public Message? Message { get; set; }
    }
}
