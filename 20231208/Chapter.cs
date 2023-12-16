using System;

namespace _20231208 {
    internal class Chapter : BaseEntity {
        // Private field used to check whether the value of the Title field has been changed or not
        private string _title = "";

        // Title of the chapter
        public string Title {
            get { return _title; }
            set {
                if (_title != value) {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        // Description of the chapter
        public string Description { get; set; }

        // Content of the chapter
        public string Content { get; set; }

        // Default constructor initializing properties
        public Chapter() {
            Title = "";
            Description = "";
            Content = "";
        }

        // Constructor with title parameter, using the default constructor and setting the title
        public Chapter(string title) : this() {
            Title = title;
        }
    }
}
