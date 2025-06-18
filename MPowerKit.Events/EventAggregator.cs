namespace MPowerKit.Events;

/// <summary>
/// Implements <see cref="IEventAggregator"/>.
/// </summary>
public class EventAggregator : IEventAggregator
{
    private static IEventAggregator? _current;

    /// <summary>
    /// Gets or Sets the Current Instance of the <see cref="IEventAggregator"/>
    /// </summary>
    public static IEventAggregator Current
    {
        get => _current ??= new EventAggregator();
        set => _current = value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="EventAggregator"/>
    /// </summary>
    public EventAggregator()
    {
        _current ??= this;
    }

    private readonly Dictionary<Type, EventBase> _events = [];

    /// <summary>
    /// Gets the single instance of the event managed by this EventAggregator. Multiple calls to this method with the same <typeparamref name="TEventType"/> returns the same event instance.
    /// </summary>
    /// <typeparam name="TEventType">The type of event to get. This must inherit from <see cref="EventBase"/>.</typeparam>
    /// <returns>A singleton instance of an event object of type <typeparamref name="TEventType"/>.</returns>
    public TEventType GetEvent<TEventType>() where TEventType : EventBase
    {
        lock (_events)
        {
            if (!_events.TryGetValue(typeof(TEventType), out EventBase? existingEvent))
            {
                TEventType newEvent = Activator.CreateInstance<TEventType>();

                _events[typeof(TEventType)] = newEvent;

                return newEvent;
            }

            return (TEventType)existingEvent;
        }
    }
}