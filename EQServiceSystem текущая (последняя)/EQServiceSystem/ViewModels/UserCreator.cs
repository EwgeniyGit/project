using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using EQDataModel;
using EQServiceSystem.Models;
using WebMatrix.WebData;

namespace EQServiceSystem.ViewModels
{
    public sealed class UserCreator
    {
        //private UsersContext _users;
        private int UserID { get; set; }
        public string UserFIO { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string UserPasswordConfirm { get; set; }
        public int UserRoleID { get; private set; }
        public string UserRoleName { get; private set; }
        public List<webpages_Roles> UserRoles { get; set; }
        public string ErrorMessage;
        public string UserPosition { get; set; }

        public UserCreator()
        {
            //_users = new UsersContext();
            using (PPREntities ppre = new PPREntities())
            {
                UserRoles = ppre.webpages_Roles.ToList();
            }
        }

        public bool Create(int RoleID)
        {
            UserRoleID = RoleID;
            UserRoleName = (from roles in UserRoles
                            where roles.RoleId == UserRoleID
                            select roles.RoleName).Single();

            try
            {
                WebSecurity.CreateUserAndAccount(UserLogin, UserPassword);
                AddUserRole();
                FillUserFIO();
                return true;
            }
            catch (Exception exp)
            {
                ErrorMessage = exp.Message;
                return false;
            }
        }

        private void FillUserFIO()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var user = (from items in ppre.tUsers
                            where items.Login == UserLogin
                            select items).Single();
                user.FIO = UserFIO;
                user.Position = UserPosition;
                ppre.SaveChanges();
            }
        }

        private void AddUserRole()
        {
            var roles = (SimpleRoleProvider)Roles.Provider;

            if (!roles.IsUserInRole(UserLogin, UserRoleName))
                roles.AddUsersToRoles(new[] { UserLogin }, new[] { UserRoleName });
        }
    }
}