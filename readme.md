# MPowerKit.Events

This library gives you an ability to facilitate communication between loosely coupled components in an application.

[![NuGet](https://img.shields.io/nuget/v/MPowerKit.Events.svg?maxAge=2592000)](https://www.nuget.org/packages/MPowerKit.Events)

Inspired by [Prism](https://prismlibrary.com/docs/event-aggregator.html)'s EventAggregator.

Note: Main difference from Prism is that this library does not support different threads event publishing/handling. The event will be handled in that thread it was raised in. Also, code was refactored to use latest .net and c# features.

### Usage

Register event aggregator in service collection:

```csharp
serviceCollection.AddSingleton<IEventAggregator, EventAggregator>();
```

or use as singleton

```csharp
EventAggregator.Current.GetEvent<>()
```

Create an event:

```csharp
public class SomeEvent : PubSubEvent { }
```

or generic version

```csharp
public class SomeGenericEvent : PubSubEvent<payload_type> { }
```

Subscribe to an event:

```csharp
IEventAggregator _eventAggregator;

public void EventHandler() { }

_eventAggregator.GetEvent<SomeEvent>().Subscribe(EventHandler);

public void GenericEventHandler(payload_type payload) { }

_eventAggregator.GetEvent<SomeGenericEvent>().Subscribe(GenericEventHandler);
```

Publish an event:

```csharp
IEventAggregator _eventAggregator;

_eventAggregator.GetEvent<SomeEvent>().Publish();

_eventAggregator.GetEvent<SomeGenericEvent>().Publish(payload);
```

Note: you may not unsubscribe from events, because it has weak reference, but better to do unsubscribe
