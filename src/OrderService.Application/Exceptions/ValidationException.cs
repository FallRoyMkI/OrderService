namespace OrderService.Application.Exceptions;

public sealed class ValidationException(List<string> exceptions)
    : Exception(string.Join(", ", exceptions));
