namespace R3.SourceGenerators.Samples.Models;

/// <summary>Base issuer: public instance events are included when codegen runs for <see cref="IssuerDerived"/> + <c>FromEvents()</c>.</summary>
public class IssuerBase
{
    public event Action<string>? BaseMessage;

    public void RaiseBase(string message) => BaseMessage?.Invoke(message);
}

public sealed class IssuerDerived : IssuerBase
{
    public event Action? DerivedTick;

    public void RaiseDerived() => DerivedTick?.Invoke();
}
