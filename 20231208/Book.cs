using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace _20231208 {
    internal class Book : INotifyPropertyChanged {
        public string Id;
        private string _title = "";

        public string Title {
            get { return _title; }
            set {
                if (_title != value) {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string Author;
        public string Description;
        public List<Chapter> Chapters;

        public Book() {
            Id = Guid.NewGuid().ToString();
            Title = "";
            Author = "";
            Description = "";
            Chapters = new List<Chapter>();
        }

        public Book(string title) {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Author = "";
            Description = "";
            Chapters = new List<Chapter>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
