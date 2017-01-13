using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.ViewModels
{
    public class FunctionAccessManager
    {
        public List<tUserFunctions> AllFunctions { get; set; }
        public List<webpages_Roles> AllRoles { get; set; }
        public List<tUsers> AllUsers { get; set; }
        public bool AccessTypeUser { get; set; }
        public int SelectedFunctionID { get; set; }
        public int ObjectToGetAccessID { get; set; }

        public FunctionAccessManager()
        {
            using (PPREntities ppre = new PPREntities())
            {
                AllFunctions = ppre.tUserFunctions.ToList();
                AllRoles = ppre.webpages_Roles.ToList();
                AllUsers = ppre.tUsers.ToList();
            }
            AccessTypeUser = false;
        }

        public bool CreateAccess()
        {
            try
            {
                using (PPREntities ppre = new PPREntities())
                {
                    var item = new tFunctionAccess();
                    item.FunctionID = SelectedFunctionID;
                    item.IsAvailableTo = ObjectToGetAccessID;
                    item.AccessTypeUser = AccessTypeUser;
                    if (!AccessAlreadyExists(item))
                    {
                        ppre.tFunctionAccess.Add(item);
                        ppre.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        private bool AccessAlreadyExists(tFunctionAccess access)
        {
            using (PPREntities ppre = new PPREntities())
            {
                var res = (from items in ppre.tFunctionAccess
                           where (items.FunctionID == access.FunctionID) && (items.IsAvailableTo == access.IsAvailableTo) && (items.AccessTypeUser == access.AccessTypeUser)
                           select items).SingleOrDefault();
                if (res != null)
                    return true;

                return false;
            }
        }

        public bool RemoveAccess()
        {
            try
            {
                using (PPREntities ppre = new PPREntities())
                {
                    var item = new tFunctionAccess();
                    item.FunctionID = SelectedFunctionID;
                    item.IsAvailableTo = ObjectToGetAccessID;
                    item.AccessTypeUser = AccessTypeUser;
                    var Removing = (from items in ppre.tFunctionAccess
                                    where (items.FunctionID == SelectedFunctionID) && (items.IsAvailableTo == ObjectToGetAccessID) && (items.AccessTypeUser == AccessTypeUser)
                                    select items).Single();
                    if (AccessAlreadyExists(item))
                    {
                        ppre.tFunctionAccess.Attach(Removing);
                        ppre.tFunctionAccess.Remove(Removing);
                        ppre.SaveChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}