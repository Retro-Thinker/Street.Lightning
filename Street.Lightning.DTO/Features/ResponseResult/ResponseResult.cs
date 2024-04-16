namespace Street.Lightning.DTO.Features.ResponseResult;

public class ResponseResult<T>
{
    public T? Data { get; set; }
    public ResultEnums OperationStatus { get; set; }
    public string? Message { get; set; }
}