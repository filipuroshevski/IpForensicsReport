using Data.Entities.IpForensicsReports;
using Data.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping.IpForensicsReports
{
    public class IpForensicsReportMap : IEntityTypeConfiguration<IpForensicsReport>
    {
        public void Configure(EntityTypeBuilder<IpForensicsReport> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("IpForensicsReport");
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.HasOne<User>(x=>x.User)
                .WithMany(x => x.IpForensicsReports).HasForeignKey(x => x.UserId).IsRequired();
        }
    }
}
