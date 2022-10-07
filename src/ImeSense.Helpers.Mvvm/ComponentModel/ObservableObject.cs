using System.ComponentModel;

namespace ImeSense.Helpers.Mvvm.ComponentModel {
    /// <summary>
    /// Base class for objects whose properties must be observable
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging {
        /// <summary>
        /// Raises when the property value is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises when the property value changes
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Raises <see cref="PropertyChanged" /> event
        /// </summary>
        /// <param name="eventArgs">Input <see cref="PropertyChangedEventArgs" /> instance</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs) {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null) {
                propertyChanged(this, eventArgs);
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanging" /> event
        /// </summary>
        /// <param name="eventArgs">Input <see cref="PropertyChangingEventArgs" /> instance</param>
        protected virtual void OnPropertyChanging(PropertyChangingEventArgs eventArgs) {
            var propertyChanging = PropertyChanging;
            if (propertyChanging != null) {
                propertyChanging(this, eventArgs);
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">Name of the property that changed</param>
        protected void OnPropertyChanged(string propertyName = null) {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanging" /> event
        /// </summary>
        /// <param name="propertyName">Name of the property that changes</param>
        protected void OnPropertyChanging(string propertyName = null) {
            OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
        }
    }
}
