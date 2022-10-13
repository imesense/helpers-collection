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

    /// <summary>
    /// Compares field with new value. If the value has changed, raises
    /// <see cref="PropertyChanging"/> event, updates the property with new
    /// value, then raises <see cref="PropertyChanged"/> event
    /// </summary>
    /// <typeparam name="T">Type of the property that changed</typeparam>
    /// <param name="field">Field with the property value</param>
    /// <param name="newValue">New value of the property</param>
    /// <param name="propertyName">Name of the property that changed</param>
    /// <returns><see langword="true"/> if the property was changed<br /><see langword="false"/> otherwise</returns>
    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, newValue)) {
            return false;
        }

        OnPropertyChanging(propertyName);
        field = newValue;
        OnPropertyChanged(propertyName);

        return true;
    }

    /// <summary>
    /// Compares field with new value. If the value has changed, raises
    /// <see cref="PropertyChanging"/> event, updates the property with new
    /// value, then raises <see cref="PropertyChanged"/> event
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="field">Old value of the property</param>
    /// <param name="newValue">New value of the property</param>
    /// <param name="model">Model of the property being updated</param>
    /// <param name="callback">Callback to invoke to set the target property value</param>
    /// <param name="propertyName">Name of the property that changed</param>
    /// <returns><see langword="true"/> if the property was changed<br /><see langword="false"/> otherwise</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="model" /> or <paramref name="callback" /> are <see langword="null" /></exception>
    protected bool SetProperty<TModel, T>(T field, T newValue, TModel model, Action<TModel, T> callback,
                                          [CallerMemberName] string? propertyName = null) where TModel : class {
        if (model == null) {
            throw new ArgumentNullException(nameof(model));
        }
        if (callback == null) {
            throw new ArgumentNullException(nameof(callback));
        }

        if (EqualityComparer<T>.Default.Equals(field, newValue)) {
            return false;
        }

        OnPropertyChanging(propertyName);
        callback(model, newValue);
        OnPropertyChanged(propertyName);

        return true;
    }
}
