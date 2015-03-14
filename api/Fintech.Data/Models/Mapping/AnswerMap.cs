using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Fintech.Data.Models.Mapping
{
    public class AnswerMap : EntityTypeConfiguration<Answer>
    {
        public AnswerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Answer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.QuestionId).HasColumnName("QuestionId");
            this.Property(t => t.OptionId).HasColumnName("OptionId");
            this.Property(t => t.PlayerId).HasColumnName("PlayerId");
            this.Property(t => t.Age).HasColumnName("Age");

            // Relationships
            //this.HasRequired(t => t.Player)
            //    .WithMany(t => t.Answers)
            //    .HasForeignKey(d => d.PlayerId);
            //this.HasRequired(t => t.Question)
            //    .WithMany(t => t.Answers)
            //    .HasForeignKey(d => d.QuestionId);
            this.HasRequired(t => t.QuestionOption)
                .WithMany(t => t.Answers)
                .HasForeignKey(d => d.OptionId);

        }
    }
}
