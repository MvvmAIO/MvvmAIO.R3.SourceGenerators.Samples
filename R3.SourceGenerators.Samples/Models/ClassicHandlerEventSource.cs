namespace R3.SourceGenerators.Samples.Models;

/// <summary>
/// Only classic <see cref="EventHandler"/> patterns for the <c>FromEventHandlers()</c> sample,
/// keeping it separate from <see cref="DemoEventSource"/> (which uses <c>Action&lt;T&gt;</c> for <c>FromEvents()</c>).
/// </summary>
public sealed class ClassicHandlerEventSource
{
    public delegate void CustomObjectSenderHandler(object sender, string token);

    public event EventHandler? CounterPulse;

    public event EventHandler<string?>? PayloadChanged;

    public event CustomObjectSenderHandler? CustomObjectSender;

    public void RaiseCounterPulse() =>
        CounterPulse?.Invoke(this, EventArgs.Empty);

    public void RaisePayload(string? value) =>
        PayloadChanged?.Invoke(this, value);

    public void RaiseCustomObjectSender(string token) =>
        CustomObjectSender?.Invoke(this, token);
}
