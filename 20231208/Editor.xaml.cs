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

            if (Control.CurrentChapter is Chapter chapter) {
                inputTitle.Text = chapter.Title;
                inputDescription.Text = chapter.Description;
                inputContent.Text = chapter.Content;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            Control.Navigate(Window.GetWindow(this), "ChapterSelect.xaml");
        }

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

        private async void BtnGenerate_Click(object sender, RoutedEventArgs e) {
            Control.Save();
            btnGenerate.Content = "Loading...";
            btnGenerate.IsEnabled = false;
            int chapterIndex = Control.CurrentBook?.Chapters.IndexOf(Control.CurrentChapter ?? new()) ?? -1;

            string content = "Story title:\n" +
                Control.CurrentBook?.Title + "\n\n" +
                "Summary: \n" +
                Control.CurrentBook?.Description + "\n\n";

            if (chapterIndex > 0) {
                content += Control.CurrentBook?.Chapters.ElementAt(chapterIndex - 1).Title + " summary:\n" +
                    Control.CurrentBook?.Chapters.ElementAt(chapterIndex - 1).Description + "\n\n" +
                    Control.CurrentBook?.Chapters.ElementAt(chapterIndex - 1).Title + " story:\n" +
                    Control.CurrentBook?.Chapters.ElementAt(chapterIndex - 1).Content + "\n\n";
            }

            content += Control.CurrentBook?.Chapters.ElementAt(chapterIndex).Title + " summary:\n" +
                Control.CurrentBook?.Chapters.ElementAt(chapterIndex).Description + "\n\n" +
                Control.CurrentBook?.Chapters.ElementAt(chapterIndex).Title + " story:\n" +
                Control.CurrentBook?.Chapters.ElementAt(chapterIndex).Content + "\n\n" +
                "[Continue the story within the same chapter in just one paragraph]";

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

            Request requestData = new() {
                Model = "gpt-3.5-turbo",
                Messages = messages
            };

            string json = JsonConvert.SerializeObject(requestData, Formatting.Indented);
            HttpClient client = new();
            HttpRequestMessage request = new(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Authorization", "Bearer " + Control.ApiKey);
            request.Content = new StringContent(json);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) {
                MessageBox.Show("Error: " + response.ReasonPhrase + "\n\n" + responseBody);
            }

            Response res = JsonConvert.DeserializeObject<Response>(responseBody) ?? new();
            inputContent.Text += "\n\n" + res.Choices?.ElementAt(0).Message?.Content;
            btnGenerate.Content = "Generate";
            btnGenerate.IsEnabled = true;
        }
    }

    class Message {
        [JsonProperty("role")]
        public string? Role { get; set; }

        [JsonProperty("content")]
        public string? Content { get; set; }
    }

    class Request {
        [JsonProperty("model")]
        public string? Model { get; set; }

        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }
    }

    class Response {
        [JsonProperty("choices")]
        public List<Choice>? Choices { get; set; }
    }

    class Choice {
        [JsonProperty("message")]
        public Message? Message { get; set; }
    }
}
