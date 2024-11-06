namespace ToDoApp.Domain.Exceptions;

public class NotFoundException(string type, string message) : Exception($"{type} cant be found by identificator {message}")
{   
}