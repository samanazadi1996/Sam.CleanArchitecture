namespace CleanArchitecture.Application.DTOs.AuditLog.Requests
{
    public class GetAllAuditLogsRequest
    {
        public string EntityId { get; set; }
        public string ModifiedBy { get; set; }
        public string EntityName { get; set; }
        public string EntityAssemblyName { get; set; }

    }
}
