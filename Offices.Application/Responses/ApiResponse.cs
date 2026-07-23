namespace Offices.Domain.Models;

public class ApiResponse
{
    public bool IsSuccess { get; protected set; }
    public string? Message { get; protected set; }
    public List<string>? Errors { get; protected set; }

    public static ApiResponse Success(string? message = null) =>
        new() { IsSuccess = true, Message = message };

    public static ApiResponse Failure(string error) =>
        new() { IsSuccess = false, Errors = new List<string> { error } };

    public static ApiResponse Failure(List<string> errors, string message) =>
        new() { IsSuccess = false, Errors = errors, Message = message };
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; private set; }

    public static ApiResponse<T> Success(T data, string? message = null) =>
        new() { IsSuccess = true, Data = data, Message = message };
}