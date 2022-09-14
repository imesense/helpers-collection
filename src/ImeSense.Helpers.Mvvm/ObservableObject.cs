using System;
using System.ComponentModel;

namespace ImeSense.Helpers.Mvvm {
    public abstract class ObservableObject : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs) {
            if (eventArgs == null) {
                throw new ArgumentNullException(ExceptionMessages.EventArgsIsNull);
            }

            if (PropertyChanged != null) {
                PropertyChanged.Invoke(this, eventArgs);
            }
        }

        protected void OnPropertyChanged(string propertyName = null) {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
