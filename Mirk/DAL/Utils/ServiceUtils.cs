using Mirk.DAL.Utils;
using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Services
{
    public class ServicesUtils
    {
        public static int? GetIdFromEntity<TEntity>(TEntity entity)
        {
            int? entityId = null;

            if (entity != null && entity is IEntity)
            {
                IEntity model = (IEntity)entity;
                entityId = model.GetId();
            }

            return entityId;
        }

        public static IList<int?> GetListIdFromEntities<TEntity>(IEnumerable<TEntity> entities)
        {
            IList<int?> listEntitiesId = new List<int?>();

            if (CollectionUtils.isNotEmpty(entities))
            {
                foreach (TEntity entity in entities)
                {
                    listEntitiesId.Add(GetIdFromEntity(entity));
                }
            }

            return listEntitiesId;
        }
    }
}