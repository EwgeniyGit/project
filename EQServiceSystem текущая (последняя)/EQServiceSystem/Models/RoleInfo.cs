using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.Models
{
    public sealed class RoleInfo
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<vRolesFunctionAccess> RoleFunctions { get; private set; }
        public List<tUserFunctions> AllFunctions;
        public List<string> RoleFuncsList;

        //public RoleInfo(string RoleName)
        //{
        //    if (System.Web.Security.Roles.RoleExists(RoleName))
        //    {
        //        using (PPREntities ppre = new PPREntities())
        //        {
        //            ID = (from items in ppre.vUsersRoles
        //                      where items.RoleName == RoleName
        //                      select items.RoleId).FirstOrDefault();
        //            Name = RoleName;
        //            RoleFunctions = (from items in ppre.vRolesFunctionAccess
        //                             where items.RoleID == ID
        //                             select items).ToList();

        //            AllFunctions = ppre.tUserFunctions.ToList();

        //            RoleFuncsList = new List<string>();
        //            for (int i = 0; i < RoleFunctions.Count; i++)
        //            {
        //                RoleFuncsList.Add(RoleFunctions[i].FunctionName);
        //            }
        //        }
        //    }
        //    else
        //        throw new FormatException("Невозможно инициализировать class RoleInfo - роли с таким именем не существует.");
        //}

        //TODO непонятно зачем тут - исправить
        public void RoleInfor(string RoleName)
        {
            if (System.Web.Security.Roles.RoleExists(RoleName))
            {
                using (PPREntities ppre = new PPREntities())
                {
                    ID = (from items in ppre.vUsersRoles
                          where items.RoleName == RoleName
                          select items.RoleId).FirstOrDefault();
                    Name = RoleName;
                    RoleFunctions = (from items in ppre.vRolesFunctionAccess
                                     where items.RoleID == ID
                                     select items).ToList();

                    AllFunctions = ppre.tUserFunctions.ToList();

                    RoleFuncsList = new List<string>();
                    for (int i = 0; i < RoleFunctions.Count; i++)
                    {
                        RoleFuncsList.Add(RoleFunctions[i].FunctionName);
                    }
                }
            }
            else
                throw new FormatException("Невозможно инициализировать class RoleInfo - роли с таким именем не существует.");
        }
    }
}