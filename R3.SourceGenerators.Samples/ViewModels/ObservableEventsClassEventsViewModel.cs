using System.Windows;
using R3.ObservableEvents;
using R3.SourceGenerators.Samples.Models;

namespace R3.SourceGenerators.Samples.ViewModels;

/// <summary>
/// Subscribes to <see cref="DemoEventSource"/> events through generated Observable pipelines.
/// </summary>
public sealed class ObservableEventsClassEventsViewModel : ViewModelBase, IDisposable
{
    private readonly DemoEventSource _source = new();
    private readonly IDisposable? _actionSub;
    private readonly IDisposable? _action2Sub;
    private readonly IDisposable? _nullableActionSub;
    private readonly IDisposable? _nullableHandledSub;
    private string _log = "Events appear here when you use the demo buttons.\n";

    public ObservableEventsClassEventsViewModel()
    {
        RaiseActionEvent1Command = new RelayCommand(() =>
            _source.RaiseAction1($"tick-{Guid.NewGuid().ToString("N")[..8]}"));

        RaiseActionEvent2Command = new RelayCommand(() =>
            _source.RaiseAction2("alpha", "beta"));

        RaiseNullablePayloadCommand = new RelayCommand(() =>
            _source.RaiseNullablePayload($"nullable-{Guid.NewGuid().ToString("N")[..6]}"));

        RaiseNullablePayloadNullCommand = new RelayCommand(() =>
            _source.RaiseNullablePayload(null));

        RaiseNullableHandledCommand = new RelayCommand(() =>
            _source.RaiseNullableHandled($"handled-{Guid.NewGuid().ToString("N")[..6]}"));

        RaiseNullableHandledNullCommand = new RelayCommand(() =>
            _source.RaiseNullableHandled(null));

        ClearLogCommand = new RelayCommand(() =>
            Log = "Log cleared.\n");

        _actionSub = _source.ObservableEvents()
            .MyActionEvent1()
            .Subscribe(msg => DispatchLog($"MyActionEvent1(\"{msg}\")"));

        _action2Sub = _source.ObservableEvents()
            .MyActionEvent2()
            .Subscribe(tuple => DispatchLog($"MyActionEvent2({tuple.Item1}, {tuple.Item2})"));

        _nullableActionSub = _source.ObservableEvents()
            .NullablePayloadAction()
            .Subscribe(msg => DispatchLog(
                $"NullablePayloadAction(payload is null={msg is null}; value={(msg ?? "<null>")})"));

        _nullableHandledSub = _source.ObservableEvents()
            .NullableStringHandled()
            .Subscribe(msg => DispatchLog(
                $"NullableStringHandled(payload is null={msg is null}; value={(msg ?? "<null>")})"));
    }

    public string Log
    {
        get => _log;
        private set => SetProperty(ref _log, value);
    }

    public RelayCommand RaiseActionEvent1Command { get; }

    public RelayCommand RaiseActionEvent2Command { get; }

    public RelayCommand RaiseNullablePayloadCommand { get; }

    public RelayCommand RaiseNullablePayloadNullCommand { get; }

    public RelayCommand RaiseNullableHandledCommand { get; }

    public RelayCommand RaiseNullableHandledNullCommand { get; }

    public RelayCommand ClearLogCommand { get; }

    private void DispatchLog(string line)
    {
        var dispatcher = Application.Current?.Dispatcher;
        if (dispatcher is null)
            return;
        _ = dispatcher.BeginInvoke(() => Log += line + Environment.NewLine);
    }

    public void Dispose()
    {
        _actionSub?.Dispose();
        _action2Sub?.Dispose();
        _nullableActionSub?.Dispose();
        _nullableHandledSub?.Dispose();
        GC.SuppressFinalize(this);
    }
}
