using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reviewer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.DAL.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            //builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();
            
            builder.Property(x => x.Salt)
                .IsRequired();
            
            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.HasMany(x => x.Receivers);
        }
    }
}
