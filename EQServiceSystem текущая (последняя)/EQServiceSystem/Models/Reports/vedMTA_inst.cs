using EQDataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace EQServiceSystem.Models.Reports
{
    public class vedMTA_inst : DbContext
    {
        public List<NALICH> Nalich { get; set; }
        public List<sprSubDept> sprSubDept { get; set; }
        private tUsers _user;
        public int UserID { get; private set; }
        private sprSubDept _sprSubDept;
        public string UserName { get; private set; }
        public string UserRole { get; private set; }
        public int? UserSubDeptID { get; private set; }
        public string UserSubDept { get; private set; }
        public vedMTA_inst()
        {
            using (PPREntities ppre = new PPREntities())
            {
                sprSubDept = ppre.sprSubDept.OrderBy(s=>s.ShortName).ToList();

            }
        }
        public vedMTA_inst(int userID)
        {
            UserInfo(WebSecurity.CurrentUserId);
            using (PPREntities ppre = new PPREntities())
            {
                sprSubDept = (from Items in ppre.tUsers
                              join SubDepth in ppre.sprSubDept on Items.SubDeptID equals SubDepth.IdSubDept
                              where Items.ID == UserID
                              orderby SubDepth.ShortName
                              select SubDepth).ToList();
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