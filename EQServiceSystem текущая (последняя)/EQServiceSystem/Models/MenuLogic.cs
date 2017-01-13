using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;
using WebMatrix.WebData;

namespace EQServiceSystem.Models
{
    public class MenuLogic 
    {
        private RightsChecker CurrentUser = new RightsChecker();
        public List<tMainMenuItems> AllMenuItems { get; private set; }

        /// <summary>
        /// Формирует список пунктов меню согласно правам доступа
        /// </summary>
        /// <param name="UserName">Login пользователя, права доступа которого используются</param>
        public MenuLogic(string UserName)
        {
            if (WebSecurity.UserExists(UserName))
            {
                using (PPREntities ppre = new PPREntities())
                {
                    var MenuItems = (from mi in ppre.tMainMenuItems
                                     orderby mi.ParentID, mi.ItemPriority
                                     where mi.ItemPriority > 0
                                     select mi).ToList();

                    var AvailableItems = new List<tMainMenuItems>();
                    var UserID = WebSecurity.GetUserId(UserName);
                    foreach (var item in MenuItems)
                    {
                        if (CurrentUser.CanSeeMenuItem(UserID, item.MenuItemName))
                            AvailableItems.Add(item);
                    }

                    AllMenuItems = AvailableItems;
                }
            }
        }
    }
}