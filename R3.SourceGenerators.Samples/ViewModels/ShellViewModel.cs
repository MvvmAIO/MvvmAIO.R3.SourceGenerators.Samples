namespace R3.SourceGenerators.Samples.ViewModels;

public sealed class ShellViewModel : ViewModelBase
{
    private readonly IReadOnlyList<NavEntry> _navEntries;
    private NavEntry _selectedEntry;
    private ViewModelBase? _currentViewModel;

    public ShellViewModel()
    {
        _navEntries =
        [
            new NavEntry { Title = "Overview", CreateViewModel = () => new HomeViewModel() },
            new NavEntry { Title = "ObservableEvents · pointer", CreateViewModel = () => new ObservableEventsInputViewModel() },
            new NavEntry { Title = "ObservableEvents · raised events", CreateViewModel = () => new ObservableEventsClassEventsViewModel() },
            new NavEntry { Title = "FromEventHandlers · EventHandler", CreateViewModel = () => new FromEventHandlersViewModel() },
            new NavEntry { Title = "ObservableEvents · inheritance", CreateViewModel = () => new InheritedObservableEventsViewModel() },
            new NavEntry { Title = "[R3Command] · Commands", CreateViewModel = () => new R3CommandViewModel() },
        ];

        _selectedEntry = _navEntries[0];
        _currentViewModel = _selectedEntry.CreateViewModel();
    }

    public IReadOnlyList<NavEntry> NavEntries => _navEntries;

    public NavEntry SelectedEntry
    {
        get => _selectedEntry;
        set
        {
            if (ReferenceEquals(_selectedEntry, value))
                return;
            if (_currentViewModel is IDisposable disposable)
                disposable.Dispose();

            _selectedEntry = value;
            OnPropertyChanged(nameof(SelectedEntry));
            _currentViewModel = value.CreateViewModel();
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    public ViewModelBase? CurrentViewModel => _currentViewModel;
}
