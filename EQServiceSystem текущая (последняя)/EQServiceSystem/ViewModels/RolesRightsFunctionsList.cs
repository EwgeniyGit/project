using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.ViewModels
{
    public struct AccessRecord
    {
        public string FunctionName;
        public string AvailableTo;
    }

    public class RolesRightsFunctionsList
    {
        public List<tUserFunctions> UserFunctions { get; set; }
        public List<webpages_Roles> UserRoles { get; set; }
        public List<tFunctionAccess> FunctionAccess { get; set; }
        public List<AccessRecord> AccessRecords { get; private set; }
        public List<tUsers> Users { get; private set; }

        public RolesRightsFunctionsList()
        {
            using (PPREntities ppre = new PPREntities())
            {
                UserFunctions = ppre.tUserFunctions.ToList();
                UserRoles = ppre.webpages_Roles.ToList();
                FunctionAccess = ppre.tFunctionAccess.ToList();
                Users = ppre.tUsers.ToList();
            }
            AccessRecords = new List<AccessRecord>();
            foreach (var item in FunctionAccess)
            {
                string UserFunction = UserFunctions.Find(uf => uf.FunctionID==item.FunctionID).FunctionName;
                string AvailableTo = string.Empty;
                if (item.AccessTypeUser)
                    AvailableTo = Users.Find(u => u.ID == item.IsAvailableTo).FIO;
                else
                    AvailableTo = UserRoles.Find(ur => ur.RoleId == item.IsAvailableTo).RoleName;

                AccessRecord record = new AccessRecord();
                record.FunctionName = UserFunction;
                record.AvailableTo = AvailableTo;
                AccessRecords.Add(record);
            }
        }

        public List<string> UserFunctionNames
        {
            get
            {
                return UserFunctions.Select(f => f.FunctionName).ToList();
            }
        }
    }
}