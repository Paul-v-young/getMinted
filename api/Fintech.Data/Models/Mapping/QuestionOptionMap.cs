using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Fintech.Data.Models.Mapping
{
    public class QuestionOptionMap : EntityTypeConfiguration<QuestionOption>
    {
        public QuestionOptionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("QuestionOption");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.Score).HasColumnName("Score");

            // Relationships
            //this.HasRequired(t => t.Question)
            //    .WithMany(t => t.QuestionOptions)
            //    .HasForeignKey(d => d.QuestionId);

        }
    }
}
