namespace Norison.Taskly.Todos.Domain.Exceptions;

public class NotFoundDomainException(string message) : DomainException(message);