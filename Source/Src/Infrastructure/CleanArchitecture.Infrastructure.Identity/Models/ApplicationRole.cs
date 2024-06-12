using Microsoft.AspNetCore.Identity;
using System;

namespace CleanArchitecture.Infrastructure.Identity.Models;

public class ApplicationRole(string name) : IdentityRole<Guid>(name)
{
}
