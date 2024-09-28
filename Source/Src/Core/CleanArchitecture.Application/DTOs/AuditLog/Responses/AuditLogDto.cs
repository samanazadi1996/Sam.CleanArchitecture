using System;

namespace CleanArchitecture.Application.DTOs.AuditLog.Responses;

public class AuditLogDto
{
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; }
    public string EntityId { get; set; }
    public string EntityName { get; set; }
    public string EntityAssemblyName { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
}
