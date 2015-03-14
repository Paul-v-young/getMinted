using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Fintech.Data.Models.Mapping
{
    public class AgeGroupMap : EntityTypeConfiguration<AgeGroup>
    {
        public AgeGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("AgeGroup");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StartAge).HasColumnName("StartAge");
            this.Property(t => t.EndAge).HasColumnName("EndAge");
        }
    }
}
