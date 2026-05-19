using System.Windows;
using System.Windows.Threading;

using R3;

using R3.SourceGenerators.Samples.Models;

namespace R3.SourceGenerators.Samples.ViewModels;

/// <summary>
/// Demonstrates generated <c>FromEventHandlers()</c> event interfaces that call <c>R3.Observable.FromEventHandler</c>
/// for <see cref="EventHandler"/> / <see cref="EventHandler{TEventArgs}"/>, custom <c>delegate void (object, T)</c> shapes (via <c>Observable.FromEvent</c>), and <see cref="DispatcherTimer.Tick"/>.
/// </summary>
public sealed class FromEventHandlersViewModel : ViewModelBase, IDisposable
{
    private readonly ClassicHandlerEventSource _handlers = new();
    private readonly DispatcherTimer _timer;
    private readonly IDisposable? _counterPulseSub;
    private readonly IDisposable? _payloadSub;
    private readonly IDisposable? _tickSub;
    private readonly IDisposable? _customDelegateSub;

    private string _log =
        "FromEventHandlers: <c>Observable.FromEventHandler</c> for CLR EventHandler types, "
        + "<c>Observable.FromEvent</c> tuple for custom <c>delegate void (object, T)</c>, "
        + "and <c>DispatcherTimer.Tick</c>. Try the buttons below.\n";

    private int _tickCount;

    private bool _timerRunning;

    public FromEventHandlersViewModel()
    {
        RaiseCounterPulseCommand = new RelayCommand(_handlers.RaiseCounterPulse);

        RaisePayloadValueCommand = new RelayCommand(() =>
            _handlers.RaisePayload($"fh-{Guid.NewGuid().ToString("N")[..6]}"));

        RaisePayloadNullCommand = new RelayCommand(() =>
            _handlers.RaisePayload(null));

        RaiseCustomDelegateCommand = new RelayCommand(() =>
            _handlers.RaiseCustomObjectSender($"custom-{Guid.NewGuid().ToString("N")[..6]}"));

        ClearLogCommand = new RelayCommand(() =>
            Log = "Log cleared.\n");

        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.1) };

        ToggleTimerCommand = new RelayCommand(ToggleTimer);

        _counterPulseSub = _handlers
            .FromEventHandlers()
            .CounterPulse
            .Subscribe(t =>
                DispatchLog($"CounterPulse (EventHandler): sender type={t.sender?.GetType().Name ?? "(null)"}"));

        _payloadSub = _handlers
            .FromEventHandlers()
            .PayloadChanged
            .Subscribe(t => DispatchLog(
                $"PayloadChanged: e is null={t.e is null}; value={(t.e ?? "<null>")}"));

        _customDelegateSub = _handlers
            .FromEventHandlers()
            .CustomObjectSender
            .Subscribe(t => DispatchLog(
                $"Custom delegate (object,string): sender type={t.sender?.GetType().Name ?? "(null)"}; token=\"{t.e}\""));

        _tickSub = _timer
            .FromEventHandlers()
            .Tick
            .Subscribe(_ =>
            {
                _tickCount++;
                DispatchLog($"DispatcherTimer.Tick count={_tickCount}");
            });
    }

    public string Log
    {
        get => _log;
        private set => SetProperty(ref _log, value);
    }

    public RelayCommand RaiseCounterPulseCommand { get; }

    public RelayCommand RaisePayloadValueCommand { get; }

    public RelayCommand RaisePayloadNullCommand { get; }

    public RelayCommand RaiseCustomDelegateCommand { get; }

    public RelayCommand ToggleTimerCommand { get; }

    public RelayCommand ClearLogCommand { get; }

    public string TimerToggleLabel => _timerRunning ? "Stop DispatcherTimer (1.1 s)" : "Start DispatcherTimer (1.1 s)";

    private void ToggleTimer()
    {
        if (_timerRunning)
        {
            _timer.Stop();
            _timerRunning = false;
            _tickCount = 0;
        }
        else
        {
            _tickCount = 0;
            _timer.Start();
            _timerRunning = true;
        }

        OnPropertyChanged(nameof(TimerToggleLabel));
    }

    private void DispatchLog(string line)
    {
        var dispatcher = Application.Current?.Dispatcher;
        if (dispatcher is null)
            return;
        _ = dispatcher.BeginInvoke(() => Log += line + Environment.NewLine);
    }

    public void Dispose()
    {
        _timer.Stop();
        _counterPulseSub?.Dispose();
        _payloadSub?.Dispose();
        _tickSub?.Dispose();
        _customDelegateSub?.Dispose();
        GC.SuppressFinalize(this);
    }
}
