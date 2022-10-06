using System.ComponentModel;

namespace ImeSense.Helpers.Mvvm.ComponentModel {
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging {
        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs) {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null) {
                propertyChanged(this, eventArgs);
            }
        }

        protected virtual void OnPropertyChanging(PropertyChangingEventArgs eventArgs) {
            var propertyChanging = PropertyChanging;
            if (propertyChanging != null) {
                propertyChanging(this, eventArgs);
            }
        }

        protected void OnPropertyChanged(string propertyName = null) {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanging(string propertyName = null) {
            OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
        }
    }
}
