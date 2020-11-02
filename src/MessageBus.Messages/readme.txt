For message class libs.
Includes IMessage, IEvent, ICommand and interfaces
Separating this assembly from the main one makes sense because the messages which used by multiple clients should be included in a common classlib. 
Common classlib can reference this MessageBus.Messages assembly only.
