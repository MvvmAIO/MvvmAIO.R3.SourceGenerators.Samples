namespace R3.SourceGenerators.Samples.Models;

/// <summary>
/// Sample CLR events consumed via generated <c>.ObservableEvents()</c> extension methods.
/// </summary>
public sealed class DemoEventSource
{
    public event Action<string>? MyActionEvent1;

    public event Action<string, string>? MyActionEvent2;

    /// <summary>NRT-shaped payload (<c>string?</c>) consumed through generated ObservableEvents.</summary>
    public event Action<string?>? NullablePayloadAction;

    /// <summary>Classic two-argument CLR event with nullable <c>string?</c> event data.</summary>
    public event EventHandler<string?>? NullableStringHandled;

    public void RaiseAction1(string value) => MyActionEvent1?.Invoke(value);

    public void RaiseAction2(string a, string b) => MyActionEvent2?.Invoke(a, b);

    public void RaiseNullablePayload(string? value) => NullablePayloadAction?.Invoke(value);

    public void RaiseNullableHandled(string? value) =>
        NullableStringHandled?.Invoke(this, value);
}
