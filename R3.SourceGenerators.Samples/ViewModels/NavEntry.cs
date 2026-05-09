namespace R3.SourceGenerators.Samples.ViewModels;

public sealed class NavEntry
{
    public required string Title { get; init; }
    public required Func<ViewModelBase> CreateViewModel { get; init; }
}
