using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using ObjectContainer;
using Model.DataAccess.Exceptions;
using CostasGIS.Model.DataAccess;

namespace Model.DataAccess.GenericDao
{
    internal class GenericDaoPocoEntityFramework<DS, E, PK> : IGenericDao<E, PK> where DS : class where E : class
    {
        private string dataSourceName;
        private Container container = Container.Instance;

        public virtual E Find(PK id)
        {
            using (DbContext context = this.getDbContext())
            {
                //context.Configuration.ProxyCreationEnabled = false;
                E result = context.Set<E>().Find(id);

                if (result == null)
                {
                    throw new InstanceNotFoundException(id, typeof(E).FullName);
                }
                else
                {
                    return result;
                }
            }
        }

        public virtual IEnumerable<E> FindAll()
        {
            using (DbContext context = this.getDbContext())
            {
                List<E> result = context.Set<E>().ToList<E>();

                return result;
            }
        }

        public virtual E Create(E entity)
        {
            using (DbContext context = this.getDbContext())
            {
                try
                {
                    context.Entry<E>(entity).State = EntityState.Added;
                    context.SaveChanges();
                    return context.Entry<E>(entity).Entity;
                }
                catch (DbEntityValidationException e)
                {
                    throw new DbEntityValidationException(this.GetEntityValidationErrors(e));
                }
                catch (DbUpdateException e)
                {
                    // Hay que recorrer la excepcion hacia arriba (InnerException) hasta que esta tenga un number y este sea 2627
                    // En este caso estaremos en una SQLException cuya propiedad Number (con valor 2627) identifica una excepcion de tipo clave duplicada.

                    if (e.InnerException.InnerException != null)
                    {
                        if (typeof(System.Data.SqlClient.SqlException) == e.InnerException.InnerException.GetType())
                        {
                            System.Data.SqlClient.SqlException ex = (System.Data.SqlClient.SqlException)e.InnerException.InnerException;
                            if (ex.Number == 2627)
                            {
                                throw new DuplicateInstanceException("", typeof(E).FullName);
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    throw;
                }
            }
        }

        public virtual Boolean Exists(PK id)
        {
            using (DbContext context = this.getDbContext())
            {
                E result = context.Set<E>().Find(id);

                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public virtual E Update(E entity)
        {
            using (DbContext context = this.getDbContext())
            {
                try
                {
                    context.Entry<E>(entity).State = EntityState.Modified;
                    context.SaveChanges();
                    return context.Entry<E>(entity).Entity;
                }
                catch (DbEntityValidationException e)
                {
                    throw new DbEntityValidationException(this.GetEntityValidationErrors(e));
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
        }

        public virtual void Remove(PK id)
        {
            using (DbContext context = this.getDbContext())
            {
                try
                {
                    E result = context.Set<E>().Find(id);

                    if (result == null)
                    {
                        throw new InstanceNotFoundException(id, typeof(E).FullName);
                    }
                    else
                    {
                        context.Entry<E>(result).State = EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
                catch (DbEntityValidationException e)
                {
                    throw new DbEntityValidationException(this.GetEntityValidationErrors(e));
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
        }

        protected DbContext getDbContext()
        {
            DbContext dbContext = (DbContext)Activator.CreateInstance(typeof(DS));
            dbContext.Configuration.ProxyCreationEnabled = false;

            return dbContext;
        }

        //ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
        //objectContext.CommandTimeout = 1200;

        protected string GetEntityValidationErrors(DbEntityValidationException e)
        {
            string outputLines = "";
            foreach (var eve in e.EntityValidationErrors)
            {
                outputLines = string.Format(
                    "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                    DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    outputLines = outputLines + System.Environment.NewLine + string.Format(
                        "- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
            return outputLines;
        }
    }
}
