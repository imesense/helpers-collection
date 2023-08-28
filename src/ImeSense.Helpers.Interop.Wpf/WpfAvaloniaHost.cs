using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;

using Avalonia.Controls.Embedding;

using AvControl = Avalonia.Controls.Control;

namespace ImeSense.Helpers.Interop.Wpf;

/// <summary>
/// Element that allows to host Avalonia control on WPF page
/// </summary>
[ContentProperty("Content")]
public class WpfAvaloniaHost : HwndHost {
    private EmbeddableControlRoot? _root;
    private AvControl? _content;

    /// <summary>
    /// Initializes new instance of <see cref="WpfAvaloniaHost"/> class
    /// </summary>
    public WpfAvaloniaHost() {
        DataContextChanged += AvaloniaHwndHost_DataContextChanged;
    }

    private void AvaloniaHwndHost_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
        if (Content != null) {
            Content.DataContext = e.NewValue;
        }
    }

    /// <summary>
    /// Gets or sets the Avalonia control hosted by the <see cref="WpfAvaloniaHost"/> element
    /// </summary>
    public AvControl? Content {
        get => _content;
        set {
            if (_content != value) {
                _content = value;
                if (_root is not null) {
                    _root.Content = value;
                }
                if (value != null) {
                    value.DataContext = DataContext;
                }
            }
        }
    }

    /// <inheritdoc />
    protected override HandleRef BuildWindowCore(HandleRef hwndParent) {
        _root = new EmbeddableControlRoot {
            Content = _content
        };
        _root.Prepare();
        _root.StartRendering();

        var handle = _root.TryGetPlatformHandle()?.Handle
            ?? throw new InvalidOperationException($"{nameof(WpfAvaloniaHost)} is unable to create {nameof(EmbeddableControlRoot)}");

        if (PresentationSource.FromVisual(this) is HwndSource source) {
            _ = NativeFunctions.SetParent(handle, source.Handle);
        }

        return new HandleRef(_root, handle);
    }

    /// <inheritdoc />
    protected override void DestroyWindowCore(HandleRef hwnd) {
        _root?.Dispose();
    }
}
