using CleanArchitecture.Application.DTOs.AuditLog.Responses;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.DTOs.AuditLog.Requests;

namespace CleanArchitecture.WebApi.Controllers.v1;

[ApiVersion("1")]
public class AuditLogController(IAuditLogService auditLogService) : BaseApiController
{
    [HttpGet]
    public async Task<IEnumerable<AuditLogDto>> GetAllAuditLogs([FromQuery] GetAllAuditLogsRequest request)
    {
        return await auditLogService.GetAll(request);
    }
}