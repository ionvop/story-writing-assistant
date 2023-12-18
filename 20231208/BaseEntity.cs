using System;
using System.ComponentModel;

namespace _20231208 {
    internal class BaseEntity : INotifyPropertyChanged {
        // Unique identifier for each entity
        // Used for differentiating between multiple Book/Chapter objects even if they have the same Title
        public string Id = Guid.NewGuid().ToString();

        // Event to notify subscribers when a property changes
        // Used to refresh the books/chapter list when the book's/chapter's title changed
        public event PropertyChangedEventHandler? PropertyChanged;

        // Method to invoke the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
    }
}
