using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reviewer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.DAL.Configurations
{
    public class ReviewConfiguration: IEntityTypeConfiguration<ReviewEntity>
    {
        public void Configure(EntityTypeBuilder<ReviewEntity> builder)
        {
            //builder.ToTable("Reviews");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Feedback)
                .IsRequired();

            builder.Property(x => x.IsAnonymous)
                .IsRequired();

            builder.HasOne(x => x.Reviewer);

            builder.HasOne(x => x.Receiver)
                .WithMany(x => x.Reviews);
        }
    }
}
