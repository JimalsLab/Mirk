using Mirk.DAL.Beans;
using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Mappers
{
    public class GroupMapper : AbstractMapper<Group, GroupBean>
    {
        protected override Group TransformBeanToModel(GroupBean b)
        {
            Group lGroup = new Group();
            
            lGroup.Id = b.Id;
            lGroup.Name = b.Name;
            lGroup.IsPrivate = b.IsPrivate;
            lGroup.Image = b.Image;
            lGroup.Background = b.Background;

            return lGroup;
        }

        protected override GroupBean TransformModelToBean(Group m)
        {
            GroupBean lGroup = new GroupBean();

            lGroup.Id = m.Id;
            lGroup.Name = m.Name;
            lGroup.IsPrivate = m.IsPrivate;
            lGroup.Image = m.Image;
            lGroup.Background = m.Background;

            return lGroup;
        }
    }
}