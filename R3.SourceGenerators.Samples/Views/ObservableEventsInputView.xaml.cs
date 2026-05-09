using System.Windows;
using System.Windows.Controls;
using R3.SourceGenerators.Samples.ViewModels;

namespace R3.SourceGenerators.Samples.Views;

public partial class ObservableEventsInputView : UserControl
{
    public ObservableEventsInputView()
    {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is ObservableEventsInputViewModel vm)
            vm.Attach(PointerCaptureHost);
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is ObservableEventsInputViewModel vm)
            vm.DisposeSubscription();
    }
}
