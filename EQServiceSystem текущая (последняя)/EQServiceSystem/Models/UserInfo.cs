using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;
using System.Web.Security;
using WebMatrix.WebData;

namespace EQServiceSystem.Models
{
    public sealed class UserInfo
    {
        private tUsers _user;
        public int UserID { get; private set; }
        public string UserName { get; private set; }
        public string UserRole { get; private set; }
        public List<webpages_Roles> AllRoles;
        public List<string> UserFunctions { get; private set; }
        public List<string> UserRules;
        public List<tUserFunctions> AllFunctions;
        public string UserLogin;
        public List<vRolesFunctionAccess> RoleFuncs;
        public List<string> RoleFuncsList;
        public int RoleID;

        //public UserInfo(int SelectedUserID)
        //{
        //    UserID = SelectedUserID;
        //    using (PPREntities ppre = new PPREntities())
        //    {
        //        _user = ppre.tUsers.Single(u => u.ID == SelectedUserID);
        //        UserName = _user.FIO;
        //        UserRole = _user.webpages_Roles.FirstOrDefault().RoleName;
        //        var UFunc = ppre.tFunctionAccess.Where(fa => ((fa.IsAvailableTo == UserID) && (fa.AccessTypeUser == true))).ToList();
        //        UserFunctions = UFunc.Select(uf => uf.tUserFunctions.FunctionName).ToList();

        //        var URules = ppre.tFunctionAccess.Where(fa => (fa.IsAvailableTo == UserID)).ToList();
        //        List<string> UserRules = URules.Select(uf => uf.tUserFunctions.FunctionName).ToList();

        //        AllFunctions = ppre.tUserFunctions.ToList();

        //        var RoleFList = new RoleInfo();
        //        RoleFList.RoleInfor(UserRole);
        //        RoleFuncs = RoleFList.RoleFunctions.ToList();

        //        RoleFuncsList = new List<string>();
        //        for (int i = 0; i < RoleFuncs.Count; i++)
        //        {
        //            RoleFuncsList.Add(RoleFuncs[i].FunctionName);
        //        }
        //    }
        //}

        //TODO перенести в конструктор. Проблема - при передаче из вьюхи в контроллер не срабатывает метод конструктора
        public void UserInfor(int SelectedUserID)
        {
            UserID = SelectedUserID;
            using (PPREntities ppre = new PPREntities())
            {
                _user = ppre.tUsers.Single(u => u.ID == SelectedUserID);
                UserName = _user.FIO;
                UserLogin = _user.Login;
                AllRoles = ppre.webpages_Roles.ToList();
                UserRole = _user.webpages_Roles.FirstOrDefault().RoleName;
                RoleID = _user.webpages_Roles.FirstOrDefault().RoleId;

                var UFunc = ppre.tFunctionAccess.Where(fa => ((fa.IsAvailableTo == UserID) && (fa.AccessTypeUser == true))).ToList();
                UserFunctions = UFunc.Select(uf => uf.tUserFunctions.FunctionName).ToList();

                var URules = ppre.tFunctionAccess.Where(fa => (fa.IsAvailableTo == UserID)).ToList();
                List<string> UserRules = URules.Select(uf => uf.tUserFunctions.FunctionName).ToList();

                AllFunctions = ppre.tUserFunctions.ToList();

                var RoleFList = new RoleInfo();
                RoleFList.RoleInfor(UserRole);
                RoleFuncs = RoleFList.RoleFunctions.ToList();

                RoleFuncsList = new List<string>();
                for (int i = 0; i < RoleFuncs.Count; i++)
                {
                    RoleFuncsList.Add(RoleFuncs[i].FunctionName);
                }
            }
        }

        public void ChangeUserRole(string NewRoleName)
        {
            var roles = (SimpleRoleProvider)Roles.Provider;
            if (roles.RoleExists(NewRoleName))
            {
                if (roles.IsUserInRole(UserName, UserRole))
                    roles.RemoveUsersFromRoles(new[] { UserName }, new[] { UserRole });

                if (!roles.IsUserInRole(UserName, UserRole))
                {
                    roles.AddUsersToRoles(new[] { UserName }, new[] { NewRoleName });
                    UserRole = NewRoleName;
                }
            }
            else
                throw new FormatException("Невозможно изменить роль, так как указанной роли не существует.");
        }

        public void RoleChange(int SelectedRoleID)
        {
            string UserRoleName = (from role in AllRoles
                                   where role.RoleId == SelectedRoleID
                                   select role.RoleName).Single();

            var roles = (SimpleRoleProvider)Roles.Provider;
            if (roles.RoleExists(UserRoleName))
            {
                if (roles.IsUserInRole(UserLogin, UserRole))
                    roles.RemoveUsersFromRoles(new[] { UserLogin }, new[] { UserRole });

                if (!roles.IsUserInRole(UserLogin, UserRole))
                {
                    roles.AddUsersToRoles(new[] { UserLogin }, new[] { UserRoleName });
                    UserRole = UserRoleName;
                }
            }
            else
                throw new FormatException("Невозможно изменить роль, так как указанной роли не существует.");
        }
    }
}