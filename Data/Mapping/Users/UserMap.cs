using Data.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping.Users
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Map(EntityTypeBuilder<User> builder)
        {

        }

        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");
            entity.HasKey(x => x.Id);


        }
    }
}