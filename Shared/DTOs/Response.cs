using System.Text.Json.Serialization;

namespace FinalMS.Shared.DTOs;

public class Response<T>
{
    public T Data { get; private set; }

    [JsonIgnore]
    public int StatusCode { get; set; }

    [JsonIgnore]
    public bool IsSuccesfull { get; set; }
    public List<string> Errors { get; set; }


    //Static Factory methods
    public static Response<T> Success(T data, int statusCode)
    {
        return new Response<T> { Data = data, StatusCode = statusCode, IsSuccesfull = true };
    }

    public static Response<T> Success(int statusCode)
    {
        return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccesfull = true };
    }

    public static Response<T> Fail(List<string> errors, int statusCode)
    {
        return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccesfull = false };
    }

    public static Response<T> Fail(string error, int statusCode)
    {
        return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccesfull = false };
    }
}
