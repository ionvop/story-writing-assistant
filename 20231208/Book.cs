using System;
using System.Collections.Generic;

namespace _20231208 {
    internal class Book : BaseEntity {
        // Private field used to check whether the value of the Title field has been changed or not
        private string _title = "";

        // Title of the book
        public string Title {
            get { return _title; }
            set {
                if (_title != value) {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        // Author of the book
        public string Author { get; set; }

        // Description of the book
        public string Description { get; set; }

        // List of chapters in the book
        public List<Chapter> Chapters { get; set; }

        // Default constructor initializing properties
        public Book() {
            Title = "";
            Author = "";
            Description = "";
            Chapters = new();
        }

        // Constructor with title parameter, using the default constructor and setting the title
        public Book(string title) : this() {
            Title = title;
        }
    }
}
