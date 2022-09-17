using System;
using System.ComponentModel;

namespace ImeSense.Helpers.Mvvm {
    public abstract class ObservableObject : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs) {
            var propertyChanged = this.PropertyChanged;
            if (propertyChanged != null) {
                propertyChanged(this, eventArgs);
            }
        }

        protected void OnPropertyChanged(string propertyName = null) {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
