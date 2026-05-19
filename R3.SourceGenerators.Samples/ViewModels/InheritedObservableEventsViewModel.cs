using System.Windows;

using R3;

using R3.SourceGenerators.Samples.Models;

namespace R3.SourceGenerators.Samples.ViewModels;

/// <summary>
/// Single <c>IssuerDerived.FromEvents()</c> entry returns <c>IIssuerDerivedEvents</c> (inherits <c>IIssuerBaseEvents</c>).
/// </summary>
public sealed class InheritedObservableEventsViewModel : ViewModelBase, IDisposable
{
    private readonly IssuerDerived _issuer = new();
    private readonly IDisposable _baseSub;
    private readonly IDisposable _derivedSub;
    private string _log =
        "<c>IssuerDerived.FromEvents()</c> → <c>IIssuerDerivedEvents : IIssuerBaseEvents</c>.\n";

    public InheritedObservableEventsViewModel()
    {
        RaiseBaseCommand = new RelayCommand(() =>
            _issuer.RaiseBase($"base-{Guid.NewGuid().ToString("N")[..6]}"));

        RaiseDerivedCommand = new RelayCommand(_issuer.RaiseDerived);

        ClearLogCommand = new RelayCommand(() =>
            Log = "Log cleared.\n");

        _baseSub = _issuer.FromEvents().BaseMessage.Subscribe(msg =>
            DispatchLog($"BaseMessage(\"{msg}\")"));

        _derivedSub = _issuer.FromEvents().DerivedTick.Subscribe(_ =>
            DispatchLog("DerivedTick()"));
    }

    public string Log
    {
        get => _log;
        private set => SetProperty(ref _log, value);
    }

    public RelayCommand RaiseBaseCommand { get; }

    public RelayCommand RaiseDerivedCommand { get; }

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
        _baseSub.Dispose();
        _derivedSub.Dispose();
        GC.SuppressFinalize(this);
    }
}
