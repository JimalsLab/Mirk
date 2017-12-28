using Mirk.DAL.Beans;
using Mirk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mirk.DAL.Mappers
{
    public class LinkUserGroupMapper : AbstractMapper<LinkUserGroup, LinkUserGroupBean>
    {
        protected override LinkUserGroup TransformBeanToModel(LinkUserGroupBean b)
        {
            LinkUserGroup lLinkUserGroup = new LinkUserGroup();

            lLinkUserGroup.Id = b.Id;
            lLinkUserGroup.IdUser = b.IdUser;
            lLinkUserGroup.IdGroup = b.IdGroup;
            lLinkUserGroup.IsAdmin = b.IsAdmin;
            lLinkUserGroup.IsFavorite = b.IsFavorite;
            lLinkUserGroup.IsNotNotificated = b.IsNotNotificated;
            lLinkUserGroup.NotificationNumber = b.NotificationNumber;

            return lLinkUserGroup;
        }

        protected override LinkUserGroupBean TransformModelToBean(LinkUserGroup m)
        {
            LinkUserGroupBean lLinkUserGroup = new LinkUserGroupBean();

            lLinkUserGroup.Id = m.Id;
            lLinkUserGroup.IdUser = m.IdUser;
            lLinkUserGroup.IdGroup = m.IdGroup;
            lLinkUserGroup.IsAdmin = m.IsAdmin;
            lLinkUserGroup.IsFavorite = m.IsFavorite;
            lLinkUserGroup.IsNotNotificated = m.IsNotNotificated;
            lLinkUserGroup.NotificationNumber = m.NotificationNumber;

            return lLinkUserGroup;
        }
    }
}