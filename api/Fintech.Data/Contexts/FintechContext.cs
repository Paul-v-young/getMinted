using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Fintech.Data.Models.Mapping;
using System.Data.Entity.Validation;
using System;

namespace Fintech.Data.Models
{
    public partial class FintechContext : DbContext
    {
        static FintechContext()
        {
            Database.SetInitializer<FintechContext>(null);
        }

        public FintechContext()
            : base("Name=FintechContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

       
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<AgeGroup> AgeGroups { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AgeGroupMap());
            modelBuilder.Configurations.Add(new QuestionMap());
            modelBuilder.Configurations.Add(new QuestionOptionMap());
            modelBuilder.Configurations.Add(new PlayerMap());
            modelBuilder.Configurations.Add(new AnswerMap());
        }
    }
}
