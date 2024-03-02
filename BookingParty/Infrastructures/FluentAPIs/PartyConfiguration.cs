using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.FluentAPIs
{
    public class PartyConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Account).WithMany(x => x.Party).HasForeignKey(x => x.HostID).HasPrincipalKey(x => x.Id);
            builder.HasMany(x => x.Booking).WithOne(x => x.Party);
            builder.HasMany(x => x.Review).WithOne(x => x.Party);
        }
    }
}
