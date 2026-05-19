namespace R3.SourceGenerators.Samples.Models;

/// <summary>
/// Types used by the generic-constraint sample: one <c>FromEvents()</c> call site
/// inside <c>where T : ConstraintBase, IContributor, IAuditor</c>.
/// </summary>
public class ConstraintBase
{
    public event Action<string>? BaseMessage;

    public void RaiseBase(string message) => BaseMessage?.Invoke(message);
}

public interface IContributor
{
    event EventHandler? Contributed;
}

public interface IAuditor
{
    event Action<int>? Audited;
}

/// <summary>Concrete type satisfying all three constraints.</summary>
public sealed class MultiConstraintIssuer : ConstraintBase, IContributor, IAuditor
{
    public event EventHandler? Contributed;

    public event Action<int>? Audited;

    public void RaiseContributed() => Contributed?.Invoke(this, EventArgs.Empty);

    public void RaiseAudited(int code) => Audited?.Invoke(code);
}
