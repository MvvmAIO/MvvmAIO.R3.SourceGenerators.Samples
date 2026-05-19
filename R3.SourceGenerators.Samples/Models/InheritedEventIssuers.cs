namespace R3.SourceGenerators.Samples.Models;

/// <summary>Base issuer: codegen emits <c>IIssuerBaseEvents</c>; <see cref="IssuerDerived"/> extends it via <c>IIssuerDerivedEvents</c>.</summary>
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
