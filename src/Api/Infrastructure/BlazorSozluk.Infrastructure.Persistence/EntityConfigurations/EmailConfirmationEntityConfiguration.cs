using BlazorSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations
{
    public class EmailConfirmationEntityConfiguration : BaseEntityConfiguration<EmailConfirmation>
    {
        public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
        {
            base.Configure(builder);

            builder.ToTable("emailconfirmation");
        }
    }
}
