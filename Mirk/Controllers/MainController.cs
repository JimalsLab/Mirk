using Mirk.Buisness;
using Mirk.DAL.Beans;
using Mirk.DAL.IoC;
using Mirk.DAL.Services;
using Mirk.ViewModelBuilders;
using Mirk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mirk.Controllers
{

    //TO GET PAST LOGIN---- test user :
    //
    // username : jimal
    // password : root

    [Authorize]
    public class MainController : Controller
    {
        #region instanciation
        public Upload uploader = new Upload();

        public IUserService userService;
        public IFriendService friendService;
        public IGroupService groupService;
        public ILinkUserGroupService linkUserGroupService;
        public FriendsViewModelBuilder fvmb;
        public AddFriendsViewModelBuilder afvmb;
        public MainViewModelBuilder mvmb;
        public DetailViewModelBuilder dvmb;
        public FriendsToAddViewModelBuilder ftavmb;
        public TextTreatment tt;
        public static Dictionary<string, int> NbUsersInConf = new Dictionary<string, int>();

        public MainController(IUserService us, IFriendService fs, IGroupService gs, ILinkUserGroupService lugs, FriendsViewModelBuilder f, AddFriendsViewModelBuilder af, MainViewModelBuilder m, DetailViewModelBuilder d, FriendsToAddViewModelBuilder fta, TextTreatment t)
        {
            this.userService = us;
            this.friendService = fs;
            this.groupService = gs;
            this.linkUserGroupService = lugs;
            this.fvmb = f;
            this.afvmb = af;
            this.mvmb = m;
            this.dvmb = d;
            this.ftavmb = fta;
            this.tt = t;
        }

        public class JsonAvailability
        {
            public string availability { get; set; }
        }

        public class JsonNickname
        {
            public string nickname { get; set; }
            public string urll { get; set; }
        }

        public class JsonMessage
        {
            public string uname { get; set; }
            public string msg { get; set; }
            public string sender { get; set; }
            public string group { get; set; }
            public string urll { get; set; }
        }

        public class JsonConfGroup
        {
            public string Id { get; set; }
        }
        #endregion

        #region main

        // GET: Main
        public ActionResult Index()
        {
            return View(mvmb.Build(getSessionUser()));
        }

        public ActionResult Discussion(string id)
        {
            return View("Index",mvmb.BuildDiscussion(getSessionUser(), id));
        }

        public ActionResult Group(string id)
        {
            return View("Index", mvmb.BuildGroup(getSessionUser(), id));
        }

        #endregion

        #region chat commands

        public void KickUser(string username,int groupid)
        {
            int userid = userService.GetMany(u => u.Username == username).FirstOrDefault().Id;
            LinkUserGroupBean lu = linkUserGroupService.GetMany(l => l.IdGroup == groupid && l.IdUser == userid).FirstOrDefault();
            linkUserGroupService.Delete(l => l.Id == lu.Id);
        }

        public void SetGroupAdmin(string username, int groupid)
        {
            int userid = userService.GetMany(u => u.Username == username).FirstOrDefault().Id;
            LinkUserGroupBean lu = linkUserGroupService.GetMany(l => l.IdGroup == groupid && l.IdUser == userid).FirstOrDefault();
            lu.IsAdmin = 1;
            linkUserGroupService.Update(lu, lu.Id);
        }

        #endregion

        #region user profile
        public ActionResult ModifyDetails(DetailViewModel ovm, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                UserBean u = getSessionUser();
                ovm.User.Username = u.Username;
                ovm.User.Id = u.Id;
                ovm.User.Photo = file.FileName;

                uploader.UploadUserImg(file);

                if (ovm.User.Adress == null)
                {
                    ovm.User.Adress = u.Adress;
                }

                if (ovm.User.Password == null)
                {
                    ovm.User.Password = u.Password;
                }

                if (ovm.User.Lastname == null)
                {
                    ovm.User.Lastname = u.Lastname;
                }

                if (ovm.User.Firstname == null)
                {
                    ovm.User.Firstname = u.Firstname;
                }

                if (ovm.User.Email == null)
                {
                    ovm.User.Email = u.Email;
                }

                if (ovm.User.Phone == null)
                {
                    ovm.User.Phone = u.Phone;
                }


                userService.Update(ovm.User, ovm.User.Id);
                return View("UserDetails", dvmb.Build(getSessionUser(), ovm.User.Id.ToString()));
            }

            return View("UserDetails", ovm);

        }

        public ActionResult GoToDetails()
        {
            return View("ModifyDetails", dvmb.Build(getSessionUser(), getSessionUser().Id.ToString()));
        }
#endregion

        #region groups

        public ActionResult CreateRoom(AddGroupViewModel mvm)
        {
            mvm.currUser = getSessionUser();

            return View(mvm);
        }

        public ActionResult ShowOptions(string id)
        {
            return View("UserDetails", dvmb.Build(getSessionUser(), id));
        }

        public ActionResult ViewOptions(int id)
        {
            return View("UserDetails", dvmb.Build(getSessionUser(), id.ToString()));
        }


        public ActionResult AddRoom(AddGroupViewModel mvm, HttpPostedFileBase file)
        {
            //si modelstate.isvalid : enregister le group en db
            if (uploader.UploadProfileImg(file))
                //true means upload failed
            {
                //ModelState.AddModelError("PathPicture", "Please choose a valid group picture");
                //mvm.currUser = getSessionUser();
                //return View("CreateRoom", mvm);
            }
            if (groupService.GetMany(g => g.Name == mvm.RoomName).FirstOrDefault() != null)
            {
                ModelState.AddModelError("RoomName", "Name already taken");
                mvm.currUser = getSessionUser();
                return View("CreateRoom", mvm);
            }
            if (ModelState.IsValid)
            {
                GroupBean lGroup = new GroupBean();
                lGroup.Name = mvm.RoomName;
                lGroup.IsPrivate = mvm.IsPublicRoom ? 0 : 1;
                lGroup.Background = 0;
                if (file != null)
                {
                    lGroup.Image = @"\Ressources\Group_Pictures\" + file.FileName;
                }
                else
                {
                    lGroup.Image = @"\Ressources\Group_Pictures\groupimg_default.png";
                }

                List<GroupBean> GroupList = groupService.GetAll().OrderByDescending(g => g.Id).ToList();

                if (GroupList.Count > 0)
                {
                    lGroup.Id = GroupList.First().Id + 1;
                }
                else
                {
                    lGroup.Id = 0;
                }
                groupService.Create(lGroup);

                LinkUserGroupBean lug = new LinkUserGroupBean();
                lug.IdGroup = lGroup.Id;
                lug.IdUser = getSessionUser().Id;
                lug.IsAdmin = 1;
                lug.IsFavorite = 1;
                lug.IsNotNotificated = 0;
                lug.NotificationNumber = 0;

                List<LinkUserGroupBean> GrList = linkUserGroupService.GetAll().OrderByDescending(g => g.Id).ToList();

                if (GrList.Count > 0)
                {
                    lug.Id = GrList.First().Id + 1;
                }
                else
                {
                    lug.Id = 0;
                }

                linkUserGroupService.Create(lug);

                return RedirectToAction("Index");
            }

            //sinon 
            return View("CreateRoom", mvm);
        }

        public ActionResult AddFriendToGroup(string id)
        {
            return View("FriendsToAdd",ftavmb.Build(getSessionUser(),int.Parse(id)));
        }

        public ActionResult AddIdToGroup(string id,string group)
        {
            LinkUserGroupBean lug = new LinkUserGroupBean();
            lug.IdGroup = int.Parse(group);
            lug.IdUser = int.Parse(id);
            lug.IsAdmin = 0;
            lug.IsFavorite = 0;
            lug.IsNotNotificated = 0;
            lug.NotificationNumber = 0;

            List<LinkUserGroupBean> GroupList = linkUserGroupService.GetAll().OrderByDescending(g => g.Id).ToList();

            if (GroupList.Count > 0)
            {
                lug.Id = GroupList.First().Id + 1;
            }
            else
            {
                lug.Id = 0;
            }

            linkUserGroupService.Create(lug);

            return View("FriendsToAdd", ftavmb.Build(getSessionUser(), int.Parse(group)));
        }

        public ActionResult LeaveGroup(string id)
        {
            int gid = int.Parse(id);
            int uid = getSessionUser().Id;
            LinkUserGroupBean lgb = linkUserGroupService.GetMany(l => l.IdGroup == gid && l.IdUser == uid).FirstOrDefault();

            linkUserGroupService.Delete(ll => ll.Id == lgb.Id);

            if (linkUserGroupService.GetMany(l => l.IdGroup == gid).FirstOrDefault() == null)
            {
                GroupBean gb = groupService.GetById(gid);
                groupService.Delete(gg => gg.Id == gb.Id);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region friends

        public ActionResult Friends()
        {
            return View(fvmb.Build(getSessionUser()));
        }

        public ActionResult AddFriends()
        {
            return View(afvmb.Build(fvmb.Build(getSessionUser())));
        }

        public ActionResult AddUserId(string id)
        {
            FriendBean fbean = new FriendBean();
            fbean.DateCreated = DateTime.Now;
            fbean.IdUser = getSessionUser().Id;
            fbean.IdFriend = int.Parse(id);
            fbean.IsBlocked = 0;
            fbean.IsFavorite = 0;
            fbean.Pseudo = userService.GetById(fbean.IdFriend).Username;

            List<FriendBean> fbList = friendService.GetAll().OrderByDescending(u => u.Id).ToList();

            if (fbList != null)
            {
                fbean.Id = fbList.First().Id + 1;
            }
            else
            {
                fbean.Id = 0;
            }
            friendService.Create(fbean);

            if (friendService.GetMany(f => f.IdFriend == fbean.IdUser && f.IdUser == fbean.IdFriend).FirstOrDefault() != null)
            {
                GroupBean gbean = new GroupBean();
                gbean.Image = "";
                gbean.IsPrivate = 2;
                gbean.Name = fbean.IdUser + "#" + fbean.IdFriend;
                List<GroupBean> grpList = groupService.GetAll().OrderByDescending(u => u.Id).ToList();

                if (grpList != null && grpList.Any())
                {
                    gbean.Id = grpList.First().Id + 1;
                }
                else
                {
                    gbean.Id = 0;
                }
                groupService.Create(gbean);

                LinkUserGroupBean lug1 = new LinkUserGroupBean();
                lug1.IdUser = fbean.IdUser;
                lug1.IsAdmin = 0;
                lug1.IsFavorite = 0;
                lug1.IdGroup = gbean.Id;
                lug1.IsNotNotificated = 0;
                lug1.NotificationNumber = 0;
                List<LinkUserGroupBean> lugList = linkUserGroupService.GetAll().OrderByDescending(u => u.Id).ToList();

                if (lugList != null && lugList.Any())
                {
                    lug1.Id = lugList.First().Id + 1;
                }
                else
                {
                    lug1.Id = 0;
                }
                linkUserGroupService.Create(lug1);

                LinkUserGroupBean lug2 = new LinkUserGroupBean();
                lug1.IdUser = fbean.IdFriend;
                lug1.IsAdmin = 0;
                lug1.IsFavorite = 0;
                lug1.IdGroup = gbean.Id;
                lug1.IsNotNotificated = 0;
                lug1.NotificationNumber = 0;
                List<LinkUserGroupBean> lug2List = linkUserGroupService.GetAll().OrderByDescending(u => u.Id).ToList();

                if (lug2List != null && lug2List.Any())
                {
                    lug2.Id = lug2List.First().Id + 1;
                }
                else
                {
                    lug2.Id = 0;
                }
                linkUserGroupService.Create(lug2);
            }


            return RedirectToAction("AddFriends");
        }

        public ActionResult ToggleFavoriteFriend(string id)
        {
            
            int friendId = int.Parse(id);
            int userId = getSessionUser().Id;

            FriendBean fb = friendService.GetMany(fx => fx.IdFriend == friendId && fx.IdUser == userId).First();
            FriendBean f = friendService.GetById(fb.Id);

            if (f.IsFavorite == 1)
            {
                f.IsFavorite = 0;
            }
            else
            {
                f.IsFavorite = 1;
            }
            
            friendService.Update(f,f.Id);

            return RedirectToAction("Friends");
        }

        public ActionResult ToggleBlockFriend(string id)
        {

            int friendId = int.Parse(id);
            int userId = getSessionUser().Id;

            FriendBean fb = friendService.GetMany(fx => fx.IdFriend == friendId && fx.IdUser == userId).First();
            FriendBean f = friendService.GetById(fb.Id);

            if (f.IsBlocked == 1)
            {
                f.IsBlocked = 0;
            }
            else
            {
                f.IsBlocked = 1;
            }

            friendService.Update(f, f.Id);

            return RedirectToAction("Friends");
        }

        public ActionResult RemoveUserId(string id)
        {
            int friendId = int.Parse(id);
            int userId = getSessionUser().Id;

            FriendBean fb = friendService.GetMany(fx => fx.IdFriend == friendId && fx.IdUser == userId).First();
            friendService.Delete(u=>u.Id == fb.Id);

            return RedirectToAction("Friends");
        }

        public ActionResult Favorites()
        {

            FriendsViewModel fvm = fvmb.Build(getSessionUser());
            List<FriendBean> newFriendList = new List<FriendBean>();
            List<UserBean> newUserList = new List<UserBean>();

            foreach (FriendBean friend in fvm.relations)
            {
                if (friend.IsFavorite == 1)
                {
                    newFriendList.Add(friend);
                    newUserList.Add(userService.GetById(friend.IdFriend));
                }
            }
            fvm.favStatus = 1;
            fvm.friends = newUserList;
            fvm.relations = newFriendList;
            return View("Friends", fvm);
            
            
        }

        public ActionResult Everyone()
        {
            FriendsViewModel fvm = fvmb.Build(getSessionUser());
            fvm.favStatus = 0;
            return View("Friends", fvm);
        }

        #endregion

        #region ajax
        public void SetAvailability(JsonAvailability j)
        {
            UserBean u = getSessionUser();
            switch (j.availability)
            {
                case "1":
                    u.Options_CurrentState = 1;
                    break;
                case "2":
                    u.Options_CurrentState = 2;
                    break;
                case "3":
                    u.Options_CurrentState = 3;
                    break;
                default:
                    u.Options_CurrentState = 0;
                    break;
            }
            userService.Update(u, u.Id);
        }

        [HttpPost]
        public void UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            HttpPostedFileBase file = files.FirstOrDefault();
            uploader.UploadSharedDocument(file);
        }

        public void SetNickname(JsonNickname j)
        {

            int r = int.Parse(j.urll.Split('/').Last());
            FriendBean f = friendService.GetById(r);
            f.Pseudo = j.nickname;

            friendService.Update(f, f.Id);
        }

        public JsonResult TreatMessage(JsonMessage j)
        {
            string msg = j.msg;
            if (msg.Split(' ').Count() > 1)
            {
                UserBean ub = getSessionUser();
                int group = int.Parse(j.urll.Split('/').Last());
                string name = msg.Split(' ').ElementAt(1);

                switch (msg.Split(' ').ElementAt(0))
                {

                    case "/kick":

                        if (j.urll.Split('/').ElementAt(4) == "Group")
                        {

                            if (userService.GetMany(ww => ww.Username == name).FirstOrDefault() != null)
                            {
                                if (linkUserGroupService.GetMany(l => l.IdGroup == group && l.IdUser == ub.Id && l.IsAdmin == 1).FirstOrDefault() != null)
                                {
                                    KickUser(name, group);
                                    msg = "<span style=\"color : #ff7f7f\"> kicked " + name + " from " + groupService.GetById(group).Name + "</span>";
                                    return Json(String.Format(msg));
                                }
                                else
                                {
                                    msg = j.msg;
                                    return Json(String.Format(msg));
                                }
                            }
                            else
                            {
                                msg = j.msg;
                                return Json(String.Format(msg));
                            }
                        }
                        else
                        {
                            msg = j.msg;
                            return Json(String.Format(msg));
                        }

                    case "/op":
                        if (j.urll.Split('/').ElementAt(4) == "Group")
                        {

                            if (userService.GetMany(ww => ww.Username == name).FirstOrDefault() != null)
                            {
                                if (linkUserGroupService.GetMany(l => l.IdGroup == group && l.IdUser == ub.Id && l.IsAdmin == 1).FirstOrDefault() != null)
                                {
                                    SetGroupAdmin(name, group);
                                    msg = "<span style=\"color : #ff7f7f\"> set " + name + " as an admin of " + groupService.GetById(group).Name + "</span>";
                                    return Json(String.Format(msg));
                                }
                                else
                                {
                                    msg = j.msg;
                                    return Json(String.Format(msg));
                                }
                            }
                            else
                            {
                                msg = j.msg;
                                return Json(String.Format(msg));
                            }
                        }
                        else
                        {
                            msg = j.msg;
                            return Json(String.Format(msg));
                        }
                }
         

            }
            

            msg = tt.Refine(j.msg);
            return Json(String.Format(msg));
        }

        public void DecrementGroupBackground(JsonConfGroup j)
        {
            int i = int.Parse(j.Id);
            GroupBean lGroup = groupService.GetMany(x => x.Id == i).FirstOrDefault();
            lGroup.Background = lGroup.Background - 1;

            groupService.Update(lGroup, lGroup.Id);
        }

        #endregion

        #region Session functions
        public UserBean getSessionUser()
        {

            string cookieId = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieId];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            int usrId = int.Parse(ticket.Name);

            UserBean usr = userService.GetById(usrId);
            return usr;
        }

        #endregion

        #region WebRTC

        public ActionResult Chat(ChatViewModel cvm, int id, int IdGroup = -1)
        {
            cvm.currUser = getSessionUser();
            
            string lKeyRoom = "";
            string lKeyRoom2 = "";

            if (id == 0)
            {
                GroupBean lGroup = groupService.GetById(IdGroup);
                lGroup.Background = lGroup.Background == null ? 1 : lGroup.Background + 1;
                groupService.Update(lGroup, lGroup.Id);
                lKeyRoom = "#" + IdGroup.ToString();

                cvm.KeyRoom = lKeyRoom;
                cvm.NbUsersInConf = (int)lGroup.Background;
                cvm.IdGroup = lGroup.Id;
            }
            else
            {
                GroupBean lGroup = groupService.GetMany(x => x.Name == id + "#" + cvm.currUser.Id || x.Name == cvm.currUser.Id + "#" + id).FirstOrDefault();
                lGroup.Background = lGroup.Background == null ? 1 : lGroup.Background + 1;
                groupService.Update(lGroup, lGroup.Id);

                lKeyRoom = (cvm.currUser.Id.ToString() + id.ToString());
                lKeyRoom2 = (id.ToString() + cvm.currUser.Id.ToString());

                cvm.KeyRoom = lGroup.Name;
                cvm.NbUsersInConf = (int)lGroup.Background;
                cvm.IdGroup = lGroup.Id;

            }

            //if (NbUsersInConf.ContainsKey(lKeyRoom))
            //{
            //    NbUsersInConf[lKeyRoom] = NbUsersInConf[lKeyRoom]+ 1;
            //    cvm.KeyRoom = lKeyRoom;
            //    cvm.NbUsersInConf = NbUsersInConf[lKeyRoom];
            //}
            //else if(NbUsersInConf.ContainsKey(lKeyRoom2))
            //{
            //    NbUsersInConf[lKeyRoom2] = NbUsersInConf[lKeyRoom2]+ 1;
            //    cvm.KeyRoom = lKeyRoom2;
            //    cvm.NbUsersInConf = NbUsersInConf[lKeyRoom2];
            //}
            //else
            //{
            //    NbUsersInConf.Add(lKeyRoom, 1);
            //    cvm.KeyRoom = lKeyRoom;
            //    cvm.NbUsersInConf = 1;
            //}


            return View("Chat", cvm);
        }

        #endregion
    }
}