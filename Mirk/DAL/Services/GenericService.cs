using Mirk.DAL.IoC;
using Mirk.DAL.Utils;
using Mirk.DAL.Mappers;
using Mirk.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace Mirk.DAL.Services
{
    public abstract class GenericService<TBean, TEntity> : IGenericService<TBean, TEntity> where TBean : class, new() where TEntity : class, new()
    {
        private IGenericRepository<TEntity> genericRepository;
        protected IUnitOfWork unitOfWork;
        protected AbstractMapper<TEntity, TBean> mapper { get; set; }

        public GenericService(IGenericRepository<TEntity> genericRepository, IUnitOfWork unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.unitOfWork = unitOfWork;
        }

        public virtual int? Create(TBean bean)
        {
            int? entityId = null;
            if (bean != null)
            {
                TEntity entity = mapper.TransformToModel(bean);
                try
                {
                    genericRepository.Create(entity);
                }
                catch (Exception e)
                {
                    this.RollBack(entity);
                    throw new Exception(e.ToString()); // en fait throw e ca marche nn ?
                }
                finally
                {
                    this.Commit(entity);
                    entityId = ServicesUtils.GetIdFromEntity(entity);
                }
            }
            return entityId;

        }

        public virtual IEnumerable<TBean> CreateAll(ICollection<TBean> beans)
        {
            IEnumerable<TBean> listEntitiesModified = null;
            if (CollectionUtils.isNotEmpty(beans))
            {
                IEnumerable<TEntity> entities = mapper.TransformToModels(beans);
                try
                {
                    genericRepository.CreateAll(entities);
                }
                catch (Exception e)
                {
                    // this.RollBack(null); A REVOIR
                    throw new Exception(e.ToString());    
                }
                finally
                {
                    this.Commit(null);
                    listEntitiesModified = mapper.TransformToBeans(entities);
                }
            }
            return listEntitiesModified;
        }

        public virtual void Update(TBean bean,int id)
        {
            if (bean != null)
            {
                TEntity entity = mapper.TransformToModel(bean);
                try
                {
                    genericRepository.Update(entity,id);

                }
                catch (Exception e)
                {
                    this.RollBack(entity);
                    throw new Exception(e.ToString());
                }
                finally
                {
                    this.Commit(entity);
                }
            }
        }

        public virtual void Delete(TBean bean)
        {
            if (bean != null)
            {
                TEntity entity = mapper.TransformToModel(bean);
                try
                {
                    genericRepository.Delete(entity);
                }
                catch (Exception e)
                {
                    this.RollBack(entity);
                    throw new Exception(e.ToString());
                }
                finally
                {
                    this.Commit(entity);
                }
            }
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            if (where != null)
            {
                try
                {
                    genericRepository.Delete(where);
                }
                catch (Exception e)
                {
                    // this.RollBack(null); A REVOIR
                    throw new Exception(e.ToString());
                }
                finally
                {
                    this.Commit(null);
                }
            }
        }

        public virtual TBean GetById(long id)
        {
            TBean bean = null;
            try
            {
                TEntity entity = genericRepository.GetById(id);
                bean = mapper.TransformToBean(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            return bean;
        }

        public virtual TBean GetById(String id)
        {
            TBean bean = null;
            if (StringUtils.isNotNullAndNotEmpty(id))
            {
                try
                {
                    TEntity entity = genericRepository.GetById(id);
                    bean = mapper.TransformToBean(entity);
                }
                catch (Exception e)
                {
                    throw new Exception(e.ToString());
                }
            }
            return bean;
        }

        public virtual IEnumerable<TBean> GetAll()
        {
            IEnumerable<TBean> beans = null;
            try
            {
                IEnumerable<TEntity> entities = genericRepository.GetAll();
                beans = mapper.TransformToBeans(entities);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            return beans;
        }

        public virtual IEnumerable<TBean> GetMany(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TBean> beans = null;
            if (where != null)
            {
                try
                {
                    IEnumerable<TEntity> entities = genericRepository.GetMany(where);
                    beans = mapper.TransformToBeans(entities);
                }
                catch (Exception e)
                {
                    throw new Exception(e.ToString());
                }
            }
            return beans;
        }

        public virtual void Commit(TEntity entity)
        {

            try
            {
                unitOfWork.Commit();
            }
            catch (Exception e)
            {
                if (entity != null)
                {
                    this.RollBack(entity);

                }
                throw e;
            }
        }

        public virtual void RollBack(TEntity entity)
        {
            try
            {
                unitOfWork.RollBack(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
    
}