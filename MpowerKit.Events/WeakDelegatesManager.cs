namespace MPowerKit.Events;

/// <summary>
/// Manage delegates using weak references to prevent keeping target instances longer than expected.
/// </summary>
public class WeakDelegatesManager
{
    private readonly List<DelegateReference> _listeners = [];

    /// <summary>
    /// Adds a weak reference to the specified <see cref="Delegate"/> listener.
    /// </summary>
    /// <param name="listener">The original <see cref="Delegate"/> to add.</param>
    public void AddListener(Delegate listener)
    {
        _listeners.Add(new DelegateReference(listener, false));
    }

    /// <summary>
    /// Removes the weak reference to the specified <see cref="Delegate"/> listener.
    /// </summary>
    /// <param name="listener">The original <see cref="Delegate"/> to remove.</param>
    public void RemoveListener(Delegate listener)
    {
        _listeners.RemoveAll(r => r.TargetEquals(null) || r.TargetEquals(listener));
    }

    /// <summary>
    /// Invoke the delegates for all targets still being alive.
    /// </summary>
    /// <param name="args">An array of objects that are the arguments to pass to the delegates. -or- null, if the method represented by the delegate does not require arguments. </param>
    public void Raise(params object[] args)
    {
        _listeners.RemoveAll(static r => r.TargetEquals(null));

        foreach (var handler in _listeners.Select(static r => r.Target).Where(static r => r is not null).ToList())
        {
            handler?.DynamicInvoke(args);
        }
    }
}