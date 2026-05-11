using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using R3.SourceGenerators.Samples.Avalonia.ViewModels;

namespace R3.SourceGenerators.Samples.Avalonia;

public partial class MainWindow : Window
{
    private readonly AvaloniaRoutedEventsDemoViewModel _viewModel = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _viewModel;
        Opened += OnWindowOpened;
    }

    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

    private void OnWindowOpened(object? sender, EventArgs e)
    {
        Opened -= OnWindowOpened;
        var primary = this.FindControl<Button>("PrimaryButton")
            ?? throw new InvalidOperationException("PrimaryButton not found.");
        var diagnostics = this.FindControl<Button>("DiagnosticsButton")
            ?? throw new InvalidOperationException("DiagnosticsButton not found.");
        var host = this.FindControl<StackPanel>("AttachedHost")
            ?? throw new InvalidOperationException("AttachedHost not found.");
        _viewModel.Attach(primary, diagnostics, host);
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        _viewModel.Dispose();
        base.OnClosing(e);
    }
}
