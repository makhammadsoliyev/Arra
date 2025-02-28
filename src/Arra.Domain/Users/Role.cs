using Arra.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace Arra.Domain.Users;

public sealed class Role : IdentityRole<Guid>, IEntity
{ }
