using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public class EntryEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.Entry>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.Entry> builder)
        {
            base.Configure(builder);
            builder.ToTable("entry"); // blazorsozlukcontext.default_scheame

            // ilişki 1 kullanıcın birden fazla entrisi olabilir.
            builder.HasOne(i => i.CreatedBy)
                .WithMany(i => i.Entries)
                .HasForeignKey(i => i.CreatedById);
        }
    }
}
