using Fintech.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fintech.Data.Repositories
{
    public class Repository<TEntity> where TEntity : class, new()
    {
        private readonly FintechContext ctx;
        private IDbSet<TEntity> _entities;

        public Repository()
        {
            ctx = new FintechContext();
        }

        private IDbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = ctx.Set<TEntity>();
                return _entities;
            }
        }

        public IQueryable<TEntity> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public TEntity Find(params object[] id)
        {
            return this.Entities.Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Add(entity);
            this.ctx.SaveChanges();

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Attach(entity);
            this.ctx.Entry<TEntity>(entity).State = EntityState.Modified;
            this.ctx.SaveChanges();

            return entity;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Attach(entity);
            this.Entities.Remove(entity);
            this.ctx.SaveChanges();
        }

        public List<TEntity2> ExecuteSqlQuery<TEntity2>(string sql, params object[] parameters)
        {
            return ctx.Database.SqlQuery<TEntity2>(sql, parameters).ToList();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return ctx.Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}
