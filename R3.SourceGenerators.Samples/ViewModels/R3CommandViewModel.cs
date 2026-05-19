namespace R3.SourceGenerators.Samples.ViewModels;

using System.ComponentModel;
using System.Threading.Tasks;
using R3;
using R3.SourceGenerators;

/// <summary>
/// Demonstrates [R3Command] usage including CanExecute support.
/// </summary>
public partial class R3CommandViewModel : ViewModelBase
{
    private int _executionCount;
    private string _lastAction = "None";

    public R3CommandViewModel()
    {
        PropertyChangedObservable = ((INotifyPropertyChanged)this)
            .FromEventHandlers()
            .PropertyChanged
            .Select(t => (t.e as PropertyChangedEventArgs)?.PropertyName ?? "Unknown");
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
    /// Async command (CanExecute wiring is shown in generator tests; async + CanExecute overload order is generator-side).
    /// </summary>
    [MvvmAIO.R3.R3Command]
    private async Task ExecuteAsync()
    {
        LastAction = "Async command starting...";
        await Task.Delay(1000);
        ExecutionCount++;
        LastAction = "Async command completed";
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
