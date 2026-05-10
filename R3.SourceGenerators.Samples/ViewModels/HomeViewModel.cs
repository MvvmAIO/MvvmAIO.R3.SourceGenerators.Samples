namespace R3.SourceGenerators.Samples.ViewModels;

public sealed class HomeViewModel : ViewModelBase
{
    public string Heading => "MvvmAIO · R3 · ObservableEvents";

    public string Intro =>
        "This sample uses a small MVVM shell: the main window only sets the data context, "
        + "navigation lives in ShellViewModel, and each feature has its own view + view model.";

    public string Sections =>
        "• Overview — this page.\n"
        + "• ObservableEvents · pointer — chain <c>FromEvents().MouseMove</c> (ReactiveMarbles-style: entry extension method, events as properties), ThrottleFirst + subscription in the view model.\n"
        + "• ObservableEvents · raised events — subscribe via <c>FromEvents()</c> then per-event properties (e.g. <c>.MyActionEvent1</c>) for Action- and EventHandler-based CLR events, including string? (NRT) payloads.\n"
        + "• FromEventHandlers · EventHandler — <c>Observable.FromEventHandler</c> for <c>EventHandler</c> / <c>EventHandler&lt;T&gt;</c>; custom <c>delegate void(object, TSecond)</c> uses <c>Observable.FromEvent</c> underneath; plus <c>DispatcherTimer.Tick</c>.\n"
        + "• ObservableEvents · inheritance — one <c>FromEvents()</c> on a derived type; generated wrapper still surfaces public instance events declared on base types (flattened, no wrapper inheritance).";
}
