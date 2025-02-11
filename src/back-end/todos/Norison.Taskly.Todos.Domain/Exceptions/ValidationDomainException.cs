namespace Norison.Taskly.Todos.Domain.Exceptions;

public class ValidationDomainException(string message) : DomainException(message);