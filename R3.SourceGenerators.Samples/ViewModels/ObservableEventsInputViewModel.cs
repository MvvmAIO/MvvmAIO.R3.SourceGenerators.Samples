using System.Windows;
using System.Windows.Input;

using R3;

namespace R3.SourceGenerators.Samples.ViewModels;

/// <summary>
/// Subscribes to UI pointer moves via generated <c>FromEvents()</c>; view calls <see cref="Attach"/>
/// because the observable source is the WPF visual tree element.
/// </summary>
public sealed class ObservableEventsInputViewModel : ViewModelBase, IDisposable
{
    private IDisposable? _subscription;
    private string _mousePosition = "Move the pointer over the area on the left.";

    public string MousePosition
    {
        get => _mousePosition;
        private set => SetProperty(ref _mousePosition, value);
    }

    public void Attach(UIElement visual)
    {
        
        DisposeSubscription();
        _subscription = visual
            .FromEvents()
            .MouseMove
            .ThrottleFirst(TimeSpan.FromMilliseconds(50))
            .Subscribe(e =>
            {
                var position = e.GetPosition(visual);
                var dispatcher = Application.Current?.Dispatcher;
                if (dispatcher is null)
                    return;
                _ = dispatcher.BeginInvoke(() =>
                    MousePosition = $"X = {position.X:F0}, Y = {position.Y:F0}");
            });
    }

    public void DisposeSubscription()
    {
        _subscription?.Dispose();
        _subscription = null;
    }

    public void Dispose()
    {
        DisposeSubscription();
        GC.SuppressFinalize(this);
    }
}
