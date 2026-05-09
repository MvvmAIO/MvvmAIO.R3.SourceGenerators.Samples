using System.Windows;
using R3.SourceGenerators.Samples.ViewModels;

namespace R3.SourceGenerators.Samples;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new ShellViewModel();
    }
}
