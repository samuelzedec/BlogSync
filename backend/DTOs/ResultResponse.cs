namespace backend.DTOs;

public class ResultResponse<TKey>
{
    public TKey? Data { get; set; }
    public List<string> Errors { get; set; } = new();

    public ResultResponse(TKey data, List<string> errors)
    {
        Data = data;
        Errors = errors;
    }

    public ResultResponse(TKey data)
        => Data = data;

    public ResultResponse(List<string> errors)
        => Errors = errors;

    public ResultResponse(string error)
        => Errors.Add(error);
}