using System.Windows;

using R3;

using R3.SourceGenerators.Samples.Models;

namespace R3.SourceGenerators.Samples.ViewModels;

/// <summary>
/// Subscribes through a generic helper so codegen emits a combined constraint interface
/// (<c>IConstraintBase_Contributor_AuditorEvents</c>) instead of per-constraint casts.
/// </summary>
public sealed class GenericConstraintObservableEventsViewModel : ViewModelBase, IDisposable
{
    private readonly MultiConstraintIssuer _issuer = new();
    private readonly IDisposable _baseSub;
    private readonly IDisposable _contributorSub;
    private readonly IDisposable _auditorSub;
    private string _log =
        "Generic helper: <c>where T : ConstraintBase, IContributor, IAuditor</c> → one <c>FromEvents()</c>.\n";

    public GenericConstraintObservableEventsViewModel()
    {
        RaiseBaseCommand = new RelayCommand(() =>
            _issuer.RaiseBase($"base-{Guid.NewGuid().ToString("N")[..6]}"));

        RaiseContributedCommand = new RelayCommand(_issuer.RaiseContributed);

        RaiseAuditedCommand = new RelayCommand(() =>
            _issuer.RaiseAudited(Random.Shared.Next(100, 999)));

        ClearLogCommand = new RelayCommand(() =>
            Log = "Log cleared.\n");

        var subs = GenericConstraintSubscription.Subscribe(_issuer, DispatchLog);
        _baseSub = subs.Base;
        _contributorSub = subs.Contributor;
        _auditorSub = subs.Auditor;
    }

    public string Log
    {
        get => _log;
        private set => SetProperty(ref _log, value);
    }

    public RelayCommand RaiseBaseCommand { get; }

    public RelayCommand RaiseContributedCommand { get; }

    public RelayCommand RaiseAuditedCommand { get; }

    public RelayCommand ClearLogCommand { get; }

    public void Dispose()
    {
        _baseSub.Dispose();
        _contributorSub.Dispose();
        _auditorSub.Dispose();
        GC.SuppressFinalize(this);
    }

    private void DispatchLog(string line)
    {
        var dispatcher = Application.Current?.Dispatcher;
        if (dispatcher is null)
            return;
        _ = dispatcher.BeginInvoke(() => Log += line + Environment.NewLine);
    }

    private static class GenericConstraintSubscription
    {
        internal readonly record struct Subscriptions(
            IDisposable Base,
            IDisposable Contributor,
            IDisposable Auditor);

        /// <summary>Generic call site: <paramref name="source"/> is <c>T</c>, not a concrete type.</summary>
        internal static Subscriptions Subscribe<T>(T source, Action<string> log)
            where T : ConstraintBase, IContributor, IAuditor
        {
            return new Subscriptions(
                source.FromEvents().BaseMessage.Subscribe(msg => log($"BaseMessage(\"{msg}\")")),
                source.FromEvents().Contributed.Subscribe(_ => log("Contributed()")),
                source.FromEvents().Audited.Subscribe(code => log($"Audited({code})")));
        }
    }
}
