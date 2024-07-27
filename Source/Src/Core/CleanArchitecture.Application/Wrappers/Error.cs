namespace CleanArchitecture.Application.Wrappers;

public class Error(ErrorCode errorCode, string description = null, string fieldName = null)
{
    public ErrorCode ErrorCode { get; set; } = errorCode;
    public string FieldName { get; set; } = fieldName;
    public string Description { get; set; } = description;
}
