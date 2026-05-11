using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace R3.SourceGenerators.Samples.Avalonia.ViewModels;

/// <summary>
/// Subscribes to Avalonia routed events via generated <c>FromRoutedEvents</c> / <c>FromAttachedRoutedEvent</c> helpers from MvvmAIO.R3.SourceGenerators.
/// </summary>
public sealed class AvaloniaRoutedEventsDemoViewModel : ViewModelBase, IDisposable
{
    private readonly List<IDisposable> _subscriptions = new();
    private int _primaryClickCount;
    private int _attachedClickCount;
    private string _pointerDiagLine = "Press and move on the diagnostics button.";

    public int PrimaryClickCount
    {
        get => _primaryClickCount;
        private set => SetProperty(ref _primaryClickCount, value);
    }

    public int AttachedClickCount
    {
        get => _attachedClickCount;
        private set => SetProperty(ref _attachedClickCount, value);
    }

    public string PointerDiagLine
    {
        get => _pointerDiagLine;
        private set => SetProperty(ref _pointerDiagLine, value);
    }

    public void Attach(Button primary, Button diagnostics, Panel attachedHost)
    {
        DisposeSubscriptions();

        // Generator discovers this call site and emits per-type routed-event wrappers.
        _subscriptions.Add(primary
            .FromRoutedEvents()
            .Click
            .Subscribe(_ => PostUi(() => PrimaryClickCount++)));

        _subscriptions.Add(diagnostics
            .FromRoutedEvents(RoutingStrategies.Tunnel | RoutingStrategies.Bubble, handledEventsToo: true)
            .PointerPressed
            .Subscribe(e =>
            {
                var src = e.Source?.ToString() ?? "(null)";
                PostUi(() =>
                    PointerDiagLine =
                        $"PointerPressed · Handled={e.Handled.ToString(CultureInfo.InvariantCulture)} · Source={src}");
            }));

        _subscriptions.Add(attachedHost
            .FromAttachedRoutedEvent(Button.ClickEvent, RoutingStrategies.Bubble, handledEventsToo: false)
            .Subscribe(_ => PostUi(() => AttachedClickCount++)));
    }

    private static void PostUi(Action action) =>
        Dispatcher.UIThread.Post(action);

    public void Dispose()
    {
        DisposeSubscriptions();
        GC.SuppressFinalize(this);
    }

    private void DisposeSubscriptions()
    {
        foreach (var d in _subscriptions)
        {
            d.Dispose();
        }

        _subscriptions.Clear();
    }
}
