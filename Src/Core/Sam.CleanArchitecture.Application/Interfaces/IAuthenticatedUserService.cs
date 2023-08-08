using System;
using System.Collections.Generic;
using System.Text;

namespace Sam.CleanArchitecture.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string UserName { get; }
    }
}
