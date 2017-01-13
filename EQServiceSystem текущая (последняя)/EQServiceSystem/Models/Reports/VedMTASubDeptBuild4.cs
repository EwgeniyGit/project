using EQDataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace EQServiceSystem.Models.Reports
{
    public class VedMTASubDeptBuild4 : DbContext
    {
        public List<NALICH> Nalich { get; set; }
        public List<sprSubDept> sprSubDept { get; set; }
        //public IQueryable<ICollection<sprBuildings>> sprBuildings { get; set; }
        public sprSubDept sprSubs { get; set; }
        private tUsers _user;
        public int UserID { get; private set; }
        private sprSubDept _sprSubDept;
        public string UserName { get; private set; }
        public string UserRole { get; private set; }
        public int? UserSubDeptID { get; private set; }
        public string UserSubDept { get; private set; }
        public VedMTASubDeptBuild4()
        {
            using (PPREntities ppre = new PPREntities())
            {
                sprSubDept = ppre.sprSubDept.OrderBy(s => s.ShortName).ToList();
                //sprBuildings = from items in ppre.sprSubDept
                //          select items.sprBuildings;
                sprSubs = (from item in ppre.sprSubDept
                           select item).FirstOrDefault();
                if (sprSubs.sprBuildings.Count == 0)
                    return;
               // sprBuilds = ppre.sprBuildings.ToList();
            }
        }
        public VedMTASubDeptBuild4(int userID)
        {
            UserInfo(WebSecurity.CurrentUserId);
                using (PPREntities ppre = new PPREntities())
                {
                    sprSubDept = (from Items in ppre.tUsers
                                  join SubDepth in ppre.sprSubDept on Items.SubDeptID equals SubDepth.IdSubDept
                                  where Items.ID == UserID
                                  orderby SubDepth.ShortName
                                  select SubDepth).ToList();
                    //sprBuildings = from items in ppre.sprSubDept
                    //                   where items.IdSubDept == UserSubDeptID
                    //                   select items.sprBuildings;

                        sprSubs = (from item in ppre.sprSubDept
                                   where item.IdSubDept == UserSubDeptID
                                   select item).FirstOrDefault();
                        if (sprSubs.sprBuildings.Count == 0)
                            return;
                } 
        }
        public void UserInfo(int SelectedUserID) // получение инф. о пользователе по id
        {
            UserID = SelectedUserID;
            using (PPREntities ppre = new PPREntities())
            {
                _user = ppre.tUsers.Single(u => u.ID == SelectedUserID);
                UserName = _user.FIO;
                UserRole = _user.webpages_Roles.FirstOrDefault().RoleName;
                UserSubDeptID = _user.SubDeptID;
                _sprSubDept = ppre.sprSubDept.Single(u => u.IdSubDept == UserSubDeptID);
                UserSubDept = _sprSubDept.ShortName;
            }
        }
    }
}