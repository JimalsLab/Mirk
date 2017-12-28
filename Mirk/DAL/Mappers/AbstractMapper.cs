using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Mappers
{
    public abstract class AbstractMapper<Model, Bean>
    {
        abstract protected Bean TransformModelToBean(Model m);

        public Bean TransformToBean(Model m)
        {
            if (m != null)
            {
                return TransformModelToBean(m);
            }
            else
            {
                throw new Exception("Erreur le model passé est null");
            }

        }

        public IEnumerable<Bean> TransformToBeans(IEnumerable<Model> models)
        {
            if (models != null)
            {
                ICollection<Bean> listBeans = new List<Bean>();
                foreach (Model m in models)
                {
                    listBeans.Add(TransformToBean(m));
                }
                return listBeans;
            }
            else
            {
                throw new Exception("Erreur la liste de model passée est nulle");
            }
        }

        abstract protected Model TransformBeanToModel(Bean b);

        public Model TransformToModel(Bean b)
        {
            if (b != null)
            {
                return TransformBeanToModel(b);
            }
            else
            {
                throw new Exception("Erreur le bean passé est null");
            }
        }

        public IEnumerable<Model> TransformToModels(IEnumerable<Bean> beans)
        {
            if (beans != null)
            {
                ICollection<Model> listModels = new List<Model>();
                foreach (Bean b in beans)
                {
                    listModels.Add(TransformToModel(b));
                }
                return listModels;
            }
            else
            {
                throw new Exception("Erreur la liste de bean passée est nulle");
            }
        }
    }
}