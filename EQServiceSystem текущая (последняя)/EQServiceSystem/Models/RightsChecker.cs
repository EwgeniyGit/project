using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.Models
{
    public sealed class RightsChecker
    {
        private List<vUserFuncionAccess> UserAccess = new List<vUserFuncionAccess>();
        private List<vRolesFunctionAccess> RoleAccess = new List<vRolesFunctionAccess>();
        private List<webpages_Roles> AllRoles = new List<webpages_Roles>();
        private List<vUsersRoles> UsersInRoles = new List<vUsersRoles>();

        public RightsChecker()
        {
            using (PPREntities ppre = new PPREntities())
            {
                UserAccess = (from ua in ppre.vUserFuncionAccess
                              select ua).ToList();
                RoleAccess = (from ra in ppre.vRolesFunctionAccess
                              select ra).ToList();
                AllRoles = (from r in ppre.webpages_Roles
                         select r).ToList();
                UsersInRoles = (from uir in ppre.vUsersRoles
                                select uir).ToList();
            }
        }

        public bool HasRightsTo(int UserID, string ObjectToAccess, AccessType TypeName)
        {
            if ((RoleHaveAccessToFunction(GetUserRole(UserID), ObjectToAccess, TypeName)) || (UserHaveAccessToFunction(UserID, ObjectToAccess, TypeName)))
                return true;
            else
                return false;
        }

        public bool CanSeeMenuItem(int UserID, string MenuItemName)
        {
            if ((RoleHaveAccessToFunction(GetUserRole(UserID), MenuItemName, AccessType.Read)) || (UserHaveAccessToFunction(UserID, MenuItemName, AccessType.Read)))
                return true;
            else
                return false;
        }

        private bool UserHaveAccessToFunction(int UserID, string ObjectToAccess, AccessType TypeName)
        {
            var AvailableObjects = (from ao in UserAccess
                                    where (ao.UserID == UserID) && (ao.AcessToObject == ObjectToAccess)
                                    select ao.Action).ToList();

            if (AvailableObjects != null)
                if (AvailableObjects.Contains(TypeName.ToString()))
                    return true;
                else
                    return false;
            else
                return false;
        }

        private string GetUserRole(int UserID)
        {
            var UserRole = (from ur in UsersInRoles
                            where ur.UserId == UserID
                            select ur.RoleName).Single();
            return UserRole;
        }

        private int GetRoleIDFromRoleName(string RoleName)
        {
            var RoleID = (from ur in UsersInRoles
                          where ur.RoleName == RoleName
                          select ur.RoleId).FirstOrDefault();
            //В данном случае берется ID первой попавшейся роли с таким же именем, как у проверяемого пользователя,
            //а не роли конкретного проверяемого пользователя т.к. права доступа для роли не меняются.
            //Одна и та же роль для любого пользователя имеет те же самые права доступа.
            //Это позволит избежать написание лишнего кода, не нарушая корректность выдаваемых сведений
            return RoleID;
        }

        private bool RoleHaveAccessToFunction(string RoleName, string ObjectToAccess, AccessType TypeName)
        {
            int RoleID = GetRoleIDFromRoleName(RoleName);
            //Сначала берем все записи, доступные для данной роли и объекта
            var AvailableObjects = (from ao in RoleAccess
                                    where (ao.RoleID == RoleID) && (ao.AcessToObject == ObjectToAccess)
                                    select ao.Action).ToList();

            if (AvailableObjects != null)
                if (AvailableObjects.Contains(TypeName.ToString()))
                    return true;
                else
                    return false;
            else
                return false;
        }
    }
}