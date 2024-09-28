using CleanArchitecture.Application.DTOs.AuditLog.Requests;
using CleanArchitecture.Application.DTOs.AuditLog.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces;

public interface IAuditLogService
{
    void Append<T>(string entityId, Type entityType, T oldValue, T newValue, string modifiedBy);

    Task<IEnumerable<AuditLogDto>> GetAll(GetAllAuditLogsRequest request);

    Task SaveLogsAsync();

}