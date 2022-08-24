using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Entities;

namespace Validator.Data.Mappings
{
    public class AnoBaseMap : IEntityTypeConfiguration<AnoBase>
    {
        public void Configure(EntityTypeBuilder<AnoBase> builder)
        {
            builder.HasKey(c => c.AnoBaseId);
        }
    }
}
