using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace _20231208 {
    internal class Chapter : INotifyPropertyChanged {
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

        public string Description;
        public string Content;

        public Chapter() {
            Id = Guid.NewGuid().ToString();
            Title = "";
            Description = "";
            Content = "";
        }

        public Chapter(string title) {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Description = "";
            Content = "";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
