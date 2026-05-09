namespace R3.SourceGenerators.Samples.ViewModels;

public sealed class HomeViewModel : ViewModelBase
{
    public string Heading => "MvvmAIO · R3 · ObservableEvents";

    public string Intro =>
        "This sample uses a small MVVM shell: the main window only sets the data context, "
        + "navigation lives in ShellViewModel, and each feature has its own view + view model.";

    public string Sections =>
        "• Overview — this page.\n"
        + "• ObservableEvents · pointer — MouseMove with ThrottleFirst, subscription owned by the view model.\n"
        + "• ObservableEvents · raised events — subscribe to Action- and EventHandler-based CLR events on a plain model class, including string? (NRT) payloads (generator 0.1.6+).";
}
