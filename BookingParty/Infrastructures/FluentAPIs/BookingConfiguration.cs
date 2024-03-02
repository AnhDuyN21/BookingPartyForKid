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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Account).WithMany(x => x.Booking).HasForeignKey(x => x.GuestID).HasPrincipalKey(x => x.Id);
            builder.HasOne(x => x.Party).WithMany(x => x.Booking).HasForeignKey(x => x.PartyID);
            builder.HasOne(x => x.Review);
        }
    }
}
