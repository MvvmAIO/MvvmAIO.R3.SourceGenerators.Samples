namespace R3.SourceGenerators.Samples.ViewModels;

using System.Threading.Tasks;
using R3;
using R3.SourceGenerators;

/// <summary>
/// Demonstrates [R3Command] usage including CanExecute support.
/// </summary>
public partial class R3CommandViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly Observable<bool> _canExecute = new Observable<bool>(true);
    private int _executionCount;
    private string _lastAction = "None";

    public R3CommandViewModel()
    {
        // Observe property changes via interface event
        PropertyChangedObservable = ((INotifyPropertyChanged)this)
            .FromEventHandlers()
            .PropertyChanged
            .Select(e => e.EventArgs.PropertyName ?? "Unknown");
    }

    /// <summary>
    /// Observable stream of property changes (demonstrates interface events).
    /// </summary>
    public Observable<string> PropertyChangedObservable { get; }

    /// <summary>
    /// Basic command with no parameters.
    /// </summary>
    [MvvmAIO.R3.R3Command]
    private void ExecuteBasic()
    {
        ExecutionCount++;
        LastAction = "Basic command executed";
    }

    /// <summary>
    /// Command with parameter.
    /// </summary>
    [MvvmAIO.R3.R3Command]
    private void ExecuteWithParam(string message)
    {
        ExecutionCount++;
        LastAction = $"Param command: {message}";
    }

    /// <summary>
    /// Async command with CanExecute support.
    /// </summary>
    [MvvmAIO.R3.R3Command(CanExecute = nameof(_canExecute))]
    private async Task ExecuteAsync()
    {
        LastAction = "Async command starting...";
        await Task.Delay(1000);
        ExecutionCount++;
        LastAction = "Async command completed";
    }

    /// <summary>
    /// Toggle CanExecute state to demonstrate command enable/disable.
    /// </summary>
    [MvvmAIO.R3.R3Command]
    private void ToggleCanExecute()
    {
        _canExecute.OnNext(!_canExecute.Value);
        LastAction = $"CanExecute set to: {_canExecute.Value}";
    }

    public int ExecutionCount
    {
        get => _executionCount;
        private set
        {
            if (_executionCount != value)
            {
                _executionCount = value;
                OnPropertyChanged(nameof(ExecutionCount));
            }
        }
    }

    public string LastAction
    {
        get => _lastAction;
        private set
        {
            if (_lastAction != value)
            {
                _lastAction = value;
                OnPropertyChanged(nameof(LastAction));
            }
        }
    }
}
