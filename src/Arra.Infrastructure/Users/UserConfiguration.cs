using Arra.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arra.Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.FirstName)
            .HasConversion(firstName => firstName.Value, value => new FirstName(value))
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(user => user.LastName)
            .HasConversion(lastName => lastName.Value, value => new LastName(value))
            .HasMaxLength(50)
            .IsRequired();
    }
}
