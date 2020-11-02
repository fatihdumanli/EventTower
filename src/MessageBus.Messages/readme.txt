For message assemblies.
Includes IMessage, IEvent, ICommand and interfaces
Separating this assembly makes sense because messages that used by multiple assemblies should be included in a common assembly. 
Common assembly can reference this MessageBus.Messages assembly only.