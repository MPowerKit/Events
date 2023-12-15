namespace MPowerKit.Events;

/// <summary>
/// Generic arguments class to pass to event handlers that need to receive data.
/// </summary>
/// <typeparam name="TData">The type of data to pass.</typeparam>
/// <remarks>
/// Initializes the DataEventArgs class.
/// </remarks>
/// <param name="value">Information related to the event.</param>
public class DataEventArgs<TData>(TData value) : EventArgs
{
    private readonly TData _value = value;

    /// <summary>
    /// Gets the information related to the event.
    /// </summary>
    /// <value>Information related to the event.</value>
    public TData Value => _value;
}