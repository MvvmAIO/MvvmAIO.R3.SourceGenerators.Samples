using R3.ObservableEvents;

using System.Windows;
namespace R3.SourceGenerators.Samples;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.ObservableEvents()
            .MouseMove()
            .ThrottleFirst(TimeSpan.FromMilliseconds(50))
            .Subscribe(x => Content = x.GetPosition(this));
    }
}