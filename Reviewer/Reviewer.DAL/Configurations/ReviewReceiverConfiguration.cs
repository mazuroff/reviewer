using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reviewer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.DAL.Configurations
{
    public class ReviewReceiverConfiguration : IEntityTypeConfiguration<ReviewReceiverEntity>
    {
        public void Configure(EntityTypeBuilder<ReviewReceiverEntity> builder)
        {
            //builder.ToTable("ReviewReceivers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Url);
            builder.Property(x => x.ImagePath);

            builder.HasMany(x => x.Reviews);

            builder.HasOne(x => x.Creator)
                .WithMany(x => x.Receivers);

        }
    }
}
