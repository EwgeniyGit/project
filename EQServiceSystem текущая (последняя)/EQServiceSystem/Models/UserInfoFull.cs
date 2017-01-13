using EQDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQServiceSystem.Models
{
    public class UserInfoFull
    {
        private EQDataModel.tUsers _user;
        public int UserID { get; private set; }
        private EQDataModel.sprSubDept _sprSubDept;
        public string UserName { get; private set; }
        public string UserRole { get; private set; }
        public int? UserRoleID { get; private set; }
        public int? UserSubDeptID { get; private set; }
        public string UserSubDept { get; private set; }
        public int UserSubDeptCode { get; private set; }
        public  UserInfoFull(int SelectedUserID) // получение инф. о пользователе по id
        {
            UserID = SelectedUserID;
            using (PPREntities ppre = new PPREntities())
            {
                _user = ppre.tUsers.Single(u => u.ID == SelectedUserID);
                UserName = _user.FIO;
                UserRole = _user.webpages_Roles.FirstOrDefault().RoleName;
                UserRoleID = _user.webpages_Roles.FirstOrDefault().RoleId;
                UserSubDeptID = _user.SubDeptID;
                _sprSubDept = ppre.sprSubDept.Single(u => u.IdSubDept == UserSubDeptID);
                UserSubDept = _sprSubDept.ShortName;
                UserSubDeptCode = _sprSubDept.SubDeptCode;
            }
        }
    }   
}