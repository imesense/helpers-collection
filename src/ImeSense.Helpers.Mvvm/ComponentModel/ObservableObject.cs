using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ImeSense.Helpers.Mvvm.ComponentModel;

/// <summary>
/// Base class for objects whose properties must be observable
/// </summary>
public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging {
    /// <summary>
    /// Raises when the property value is changed
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raises when the property value changes
    /// </summary>
    public event PropertyChangingEventHandler? PropertyChanging;

    /// <summary>
    /// Raises <see cref="PropertyChanged" /> event
    /// </summary>
    /// <param name="eventArgs">Input <see cref="PropertyChangedEventArgs" /> instance</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventArgs" /> is <see langword="null" /></exception>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs) {
        if (eventArgs == null) {
            throw new ArgumentNullException(nameof(eventArgs));
        }

        PropertyChanged?.Invoke(this, eventArgs);
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanging" /> event
    /// </summary>
    /// <param name="eventArgs">Input <see cref="PropertyChangingEventArgs" /> instance</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventArgs" /> is <see langword="null" /></exception>
    protected virtual void OnPropertyChanging(PropertyChangingEventArgs eventArgs) {
        if (eventArgs == null) {
            throw new ArgumentNullException(nameof(eventArgs));
        }

        PropertyChanging?.Invoke(this, eventArgs);
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanged" /> event
    /// </summary>
    /// <param name="propertyName">Name of the property that changed</param>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Raises the <see cref="PropertyChanging" /> event
    /// </summary>
    /// <param name="propertyName">Name of the property that changes</param>
    protected void OnPropertyChanging([CallerMemberName] string? propertyName = null) =>
        OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
}
