using System;
using System.ComponentModel;

namespace _20231208 {
    internal class BaseEntity : INotifyPropertyChanged {
        // Unique identifier for each entity
        public string Id = Guid.NewGuid().ToString();

        // Event to notify subscribers when a property changes
        public event PropertyChangedEventHandler? PropertyChanged;

        // Method to invoke the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
    }
}
