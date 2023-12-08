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

            if (Session.CurrentChapter is Chapter chapter) {
                inputTitle.Text = chapter.Title;
                inputDescription.Text = chapter.Description;
                inputContent.Text = chapter.Content;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) {
            if (Window.GetWindow(this) is MainWindow mainWindow) {
                mainWindow.Navigate(new Uri("ChapterSelect.xaml", UriKind.Relative));
            }
        }

        private void InputTitle_TextChanged(object sender, TextChangedEventArgs e) {
            if (Session.CurrentChapter is Chapter chapter) {
                chapter.Title = inputTitle.Text;
            }
        }

        private void InputDescription_TextChanged(object sender, TextChangedEventArgs e) {
            if (Session.CurrentChapter is Chapter chapter) {
                chapter.Description = inputDescription.Text;
            }
        }

        private void InputContent_TextChanged(object sender, TextChangedEventArgs e) {
            if (Session.CurrentChapter is Chapter chapter) {
                chapter.Content = inputContent.Text;
            }
        }

        private async void BtnGenerate_Click(object sender, RoutedEventArgs e) {
            btnGenerate.Content = "Loading...";
            btnGenerate.IsEnabled = false;
            int chapterIndex = Session.CurrentBook?.Chapters.IndexOf(Session.CurrentChapter ?? new()) ?? -1;

            string content = "Story title:\n" +
                Session.CurrentBook?.Title + "\n\n" +
                "Summary: \n" +
                Session.CurrentBook?.Description + "\n\n";

            for (int i = 0; i <= chapterIndex; i++) {
                Chapter? chapter = Session.CurrentBook?.Chapters.ElementAt(i);

                content += chapter?.Title + " summary:\n" +
                    chapter?.Description + "\n\n";
            }

            content += Session.CurrentChapter?.Title + " story:\n" +
                Session.CurrentChapter?.Content + "\n\n" +
                "[Continue the story within the same chapter in just one paragraph]";

            List<Message> messages = new() {
                new() {
                    role = "system",
                    content = "You will continue the story based on the given text."
                },

                new() {
                    role = "user",
                    content = content
                }
            };

            Request requestData = new() {
                model = "gpt-3.5-turbo",
                messages = messages
            };

            string json = JsonConvert.SerializeObject(requestData, Formatting.Indented);
            HttpClient client = new();
            HttpRequestMessage request = new(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Authorization", "Bearer " + Session.ApiKey);
            request.Content = new StringContent(json);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();
            Response res = JsonConvert.DeserializeObject<Response>(responseBody) ?? new();
            inputContent.Text += "\n\n" + res.choices?.ElementAt(0).message?.content;
            btnGenerate.Content = "Generate";
            btnGenerate.IsEnabled = true;
        }
    }

    class Message {
        public string? role;
        public string? content;
    }

    class Request {
        public string? model;
        public List<Message>? messages;
    }

    class Response {
        public List<Choice>? choices;
    }

    class Choice {
        public Message? message;
    }
}
