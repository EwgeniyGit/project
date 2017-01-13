using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;
using EQServiceSystem.Models.Units;
using PagedList.Mvc;
using PagedList;
using EQServiceSystem.Models.Equipment;

namespace EQServiceSystem.ViewModels
{
    public class EquipmentCollection
    {
        public List<EquipmentElement> Equipment = new List<EquipmentElement>();
        public List<string> All_Type_name_items = new List<string>();
        public List<string> All_Group_name_items = new List<string>();
        public List<string> All_SubGroup_name_items = new List<string>();
        public List<string> All_Marks_name_items = new List<string>();
        public List<string> SubDept_items = new List<string>();
        public List<string> Buildings_items = new List<string>();
        public List<string> Rooms_items = new List<string>();
        public List<EquipmentElement> NotFilteredCollection = new List<EquipmentElement>();
        public IPagedList<EquipmentElement> PagedEquipment { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageSize = 100;
        public List<sprEquipment> All_spr = new List<sprEquipment>();

        public Dictionary<string, string> GroupNames = new Dictionary<string, string>();

        private tUsers _user;
        public int UserID { get; private set; }
        private sprSubDept _sprSubDept;
        public string UserName { get; private set; }
        public string UserRole { get; private set; }
        public int? UserSubDeptID { get; private set; }
        public string UserSubDept { get; private set; }

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
        public EquipmentCollection(int? page)
        {
            if (page != null && page !=0)
                CurrentPage = (int)page;
            else
                CurrentPage = 1;

            using (PPREntities ppre = new PPREntities())
            {
                UserInfo(WebMatrix.WebData.WebSecurity.CurrentUserId);
                var Eq = (from items in ppre.NALICH
                          join tTypeISGroup in ppre.TypeISGroup on items.id_TypeIsGroup equals tTypeISGroup.id
                          join tTypeOfEq in ppre.sprTypeOfEquipment on tTypeISGroup.idType equals tTypeOfEq.ID
                          join tGroup in ppre.sprGroup on tTypeISGroup.idGroup equals tGroup.idGroup
                          join tSubGroup in ppre.sprSubGroup on tTypeISGroup.idSubGroup equals tSubGroup.idSubGroup
                          join tMark in ppre.sprMark on tTypeISGroup.idMark equals tMark.id_mark
                          join tModel in ppre.sprModel on tTypeISGroup.idModel equals tModel.id_model
                          where items.sprSubDept.ShortName == UserSubDept
                          select new
                          {
                              items.ID,
                              items.sprSubDept.ShortName,
                              items.sprBuildings.BuldingName,
                              items.REGNO,
                              items.INWNOM,
                              items.tAggregateCodifer.SignCode,
                              items.NAIMOB,
                              items.W,
                              items.G,
                              items.P,
                              items.POM,
                              items.MARKA,
                              tTypeOfEq.TypeOfEquipment,
                              tGroup.idGroup,
                              tGroup.GroupName,
                              tSubGroup.SubGroupName,
                              tMark.mark,
                              tModel.model,
                              tTypeOfEq.TypeCode,
                              tTypeISGroup.oldValue
                          }).ToList();

                All_spr = ppre.sprEquipment.ToList();

                foreach (var item in Eq)
                {
                    var elem = new EquipmentElement(item.ID);
                    elem.AggregateOfType = item.SignCode;
                    elem.BuildingName = item.BuldingName;
                    elem.InvNumber = item.INWNOM;
                    elem.Name = item.NAIMOB;
                    elem.RegNumber = item.REGNO;
                    elem.SubDeptName = item.ShortName;
                    elem.W = item.W;
                    elem.G = item.G;
                    elem.P = item.P;
                    elem.Room = item.POM;
                    elem.Mark = item.MARKA;
                    elem.TypeName = item.TypeCode + " " + item.TypeOfEquipment;
                    elem.GroupName = item.idGroup + " " + item.GroupName;
                    elem.SubGroupName = item.oldValue + " " + item.SubGroupName;
                    elem.TypeCode = item.TypeCode;
                    NotFilteredCollection.Add(elem);
                    Equipment.Add(elem);
                }
                    All_Type_name_items.Add(string.Empty);
                    All_Type_name_items.AddRange(NotFilteredCollection.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                    All_Group_name_items.Add(string.Empty);
                    All_Group_name_items.AddRange(NotFilteredCollection.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                    All_SubGroup_name_items.Add(string.Empty);
                    All_SubGroup_name_items.AddRange(NotFilteredCollection.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                    All_Marks_name_items.Add(string.Empty);
                    All_Marks_name_items.AddRange(NotFilteredCollection.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                    SubDept_items.AddRange(NotFilteredCollection.Select(x => x.SubDeptName).Distinct().OrderBy(x => x));
                    Buildings_items.Add(string.Empty);
                    Buildings_items.AddRange(NotFilteredCollection.Select(x => x.BuildingName).Distinct().OrderBy(x => x));
                    Rooms_items.Add(string.Empty);
                    Rooms_items.AddRange(NotFilteredCollection.Select(x => x.Room).Distinct().OrderBy(x => x));
            }
            if (Equipment.Count < CurrentPageSize)
                CurrentPageSize = Equipment.Count;
            PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);
        }

        public EquipmentCollection()
        {
            CurrentPage = 1;

            using (PPREntities ppre = new PPREntities())
            {
                UserInfo(WebMatrix.WebData.WebSecurity.CurrentUserId);
                var Eq = (from items in ppre.NALICH
                          join tTypeISGroup in ppre.TypeISGroup on items.id_TypeIsGroup equals tTypeISGroup.id
                          join tTypeOfEq in ppre.sprTypeOfEquipment on tTypeISGroup.idType equals tTypeOfEq.ID
                          join tGroup in ppre.sprGroup on tTypeISGroup.idGroup equals tGroup.idGroup
                          join tSubGroup in ppre.sprSubGroup on tTypeISGroup.idSubGroup equals tSubGroup.idSubGroup
                          join tMark in ppre.sprMark on tTypeISGroup.idMark equals tMark.id_mark
                          join tModel in ppre.sprModel on tTypeISGroup.idModel equals tModel.id_model
                          where items.sprSubDept.ShortName == UserSubDept
                          select new
                          {
                              items.ID,
                              items.sprSubDept.ShortName,
                              items.sprBuildings.BuldingName,
                              items.REGNO,
                              items.INWNOM,
                              items.tAggregateCodifer.SignCode,
                              items.NAIMOB,
                              items.W,
                              items.G,
                              items.P,
                              items.POM,
                              items.MARKA,
                              tTypeOfEq.TypeOfEquipment,
                              tGroup.idGroup,
                              tGroup.GroupName,
                              tSubGroup.SubGroupName,
                              tMark.mark,
                              tModel.model,
                              tTypeOfEq.TypeCode,
                              tTypeISGroup.oldValue
                          }).ToList();

                All_spr = ppre.sprEquipment.ToList();

                foreach (var item in Eq)
                {
                    var elem = new EquipmentElement(item.ID);
                    elem.AggregateOfType = item.SignCode;
                    elem.BuildingName = item.BuldingName;
                    elem.InvNumber = item.INWNOM;
                    elem.Name = item.NAIMOB;
                    elem.RegNumber = item.REGNO;
                    elem.SubDeptName = item.ShortName;
                    elem.W = item.W;
                    elem.G = item.G;
                    elem.P = item.P;
                    elem.Room = item.POM;
                    elem.Mark = item.MARKA;
                    elem.TypeName = item.TypeCode + " " + item.TypeOfEquipment;
                    elem.GroupName = item.idGroup + " " + item.GroupName;
                    elem.SubGroupName = item.oldValue + " " + item.SubGroupName;
                    NotFilteredCollection.Add(elem);
                    Equipment.Add(elem);
                }
               
                All_Type_name_items.Add(string.Empty);
                All_Type_name_items.AddRange(NotFilteredCollection.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                All_Group_name_items.Add(string.Empty);
                All_Group_name_items.AddRange(NotFilteredCollection.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                All_SubGroup_name_items.Add(string.Empty);
                All_SubGroup_name_items.AddRange(NotFilteredCollection.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                All_Marks_name_items.Add(string.Empty);
                All_Marks_name_items.AddRange(NotFilteredCollection.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                SubDept_items.Add(UserSubDept);
                Buildings_items.Add(string.Empty);
                Buildings_items.AddRange(Equipment.Select(x => x.BuildingName).Distinct().OrderBy(x => x));
                Rooms_items.Add(string.Empty);
                Rooms_items.AddRange(Equipment.Select(x => x.Room).Distinct().OrderBy(x => x));    
            }

            PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);
        }

        public void FilterBy_W(string W)
        {
            Equipment.RemoveAll(x => x.TypeName != W);
            All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
            All_Type_name_items.Add(W); //Оставляем только один элемент, по которому проводилась фильтрация
            All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
            All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
            All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
            All_Group_name_items.Add(string.Empty);
            All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
            All_Marks_name_items.Add(string.Empty);
            Buildings_items.Add(string.Empty);
            Buildings_items.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            Rooms_items.Add(string.Empty);
            Rooms_items.AddRange(Equipment.Select(x => x.Room).Distinct());

            if (Equipment.Count < CurrentPageSize)
                CurrentPageSize = Equipment.Count;
            PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

            RemoveEmptyStringInAlreadyFiltered();
        }

        public void FilterBy_G(string G)
        {
            Equipment.RemoveAll(x => x.GroupName != G);
            All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
            All_Group_name_items.Add(G); //Оставляем только один элемент, по которому проводилась фильтрация
            All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
            All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
            All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
            All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
            All_Marks_name_items.Add(string.Empty);
            All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            Buildings_items.Add(string.Empty);
            Buildings_items.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            Rooms_items.Add(string.Empty);
            Rooms_items.AddRange(Equipment.Select(x => x.Room).Distinct());
            if (Equipment.Count < CurrentPageSize)
                CurrentPageSize = Equipment.Count;
            PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

            RemoveEmptyStringInAlreadyFiltered();
        }

        public void FilterBy_P(string P)
        {
            //Equipment.RemoveAll(x => x.P != P);
            //All_P_items.RemoveRange(0, All_P_items.Count);
            //All_P_items.Add(P); //Оставляем только один элемент, по которому проводилась фильтрация
            //All_W_items.RemoveRange(0, All_W_items.Count);
            //All_G_items.RemoveRange(0, All_G_items.Count);
            //All_Marks.RemoveRange(0, All_Marks.Count);

            //All_W_items.Add(string.Empty);
            //All_W_items.AddRange(Equipment.Select(x => x.W).Distinct());
            //All_G_items.Add(string.Empty);
            //All_G_items.AddRange(Equipment.Select(x => x.G).Distinct());
            //All_Marks.Add(string.Empty);
           
            Equipment.RemoveAll(x => x.SubGroupName != P);
            All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
            All_SubGroup_name_items.Add(P);
            All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
            All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
            All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
            All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
            All_Type_name_items.Add(string.Empty);
            All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            All_Group_name_items.Add(string.Empty);
            All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            All_Marks_name_items.Add(string.Empty);
            All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            Buildings_items.Add(string.Empty);
            Buildings_items.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            Rooms_items.Add(string.Empty);
            Rooms_items.AddRange(Equipment.Select(x => x.Room).Distinct());
            if (Equipment.Count < CurrentPageSize)
                CurrentPageSize = Equipment.Count;
            PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

            RemoveEmptyStringInAlreadyFiltered();
        }

        public void FilterBy_Mark(string Mark)
        {
           
            Equipment.RemoveAll(x => x.Mark != Mark);

            All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
            All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
            All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
            All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
            All_Marks_name_items.Add(Mark);
            All_Type_name_items.Add(string.Empty);
            All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            All_Group_name_items.Add(string.Empty);
            All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            All_SubGroup_name_items.Add(string.Empty);
            All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            Buildings_items.Add(string.Empty);
            Buildings_items.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            Rooms_items.Add(string.Empty);
            Rooms_items.AddRange(Equipment.Select(x => x.Room).Distinct());
            //Equipment.RemoveAll(x => x.Mark != Mark);
            //All_G_items.RemoveRange(0, All_G_items.Count);
            //All_P_items.RemoveRange(0, All_P_items.Count);
            //All_W_items.RemoveRange(0, All_W_items.Count);
            //All_Marks.RemoveRange(0, All_Marks.Count);
            //All_Marks.Add(Mark); //Оставляем только один элемент, по которому проводилась фильтрация

            //All_W_items.Add(string.Empty);
            //All_W_items.AddRange(Equipment.Select(x => x.W).Distinct());
            //All_G_items.Add(string.Empty);
            //All_G_items.AddRange(Equipment.Select(x => x.G).Distinct());
            //All_P_items.Add(string.Empty);
            //All_P_items.AddRange(Equipment.Select(x => x.P).Distinct());

            if (Equipment.Count < CurrentPageSize)
                CurrentPageSize = Equipment.Count;
            PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

            RemoveEmptyStringInAlreadyFiltered();
        }

        public void FilterByBuildings(string BuildName)
        {
           
            Equipment.RemoveAll(x => x.BuildingName != BuildName);
            All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
            All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
            All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
            All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
            SubDept_items.RemoveRange(0, SubDept_items.Count);
            Buildings_items.RemoveRange(0, Buildings_items.Count);
            Rooms_items.RemoveRange(0, Rooms_items.Count);
            Buildings_items.Add(BuildName); //Оставляем только один элемент, по которому проводилась фильтрация
            All_Type_name_items.Add(string.Empty);
            All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            All_Group_name_items.Add(string.Empty);
            All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            All_SubGroup_name_items.Add(string.Empty);
            All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            SubDept_items.Add(string.Empty);
            SubDept_items.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            Rooms_items.Add(string.Empty);
            Rooms_items.AddRange(Equipment.Select(x => x.Room).Distinct());

            if (Equipment.Count < CurrentPageSize)
                CurrentPageSize = Equipment.Count;
            PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

            RemoveEmptyStringInAlreadyFiltered();
        }

        public void FilterByRooms(string RoomName)
        {
           
            Equipment.RemoveAll(x => x.Room != RoomName);  
            All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
            All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
            All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
            All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
            SubDept_items.RemoveRange(0, SubDept_items.Count);
            Buildings_items.RemoveRange(0, Buildings_items.Count);
            Rooms_items.RemoveRange(0, Rooms_items.Count);
            Rooms_items.Add(RoomName); //Оставляем только один элемент, по которому проводилась фильтрация

            All_Type_name_items.Add(string.Empty);
            All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            All_Group_name_items.Add(string.Empty);
            All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            All_SubGroup_name_items.Add(string.Empty);
            All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            SubDept_items.Add(string.Empty);
            SubDept_items.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            Buildings_items.Add(string.Empty);
            Buildings_items.AddRange(Equipment.Select(x => x.BuildingName).Distinct());

            if (Equipment.Count < CurrentPageSize)
                CurrentPageSize = Equipment.Count;
            PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

            RemoveEmptyStringInAlreadyFiltered();
        }

        public void FilterByRegNo(string RegNo)
        {
          
            Equipment.RemoveAll(x => x.RegNumber != RegNo);
            All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
            All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
            All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
            All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
            All_Type_name_items.Add(string.Empty);
            All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            All_Group_name_items.Add(string.Empty);
            All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            All_SubGroup_name_items.Add(string.Empty);
            All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            //Equipment.RemoveAll(x => x.RegNumber != RegNo);
            //All_G_items.RemoveRange(0, All_G_items.Count);
            //All_P_items.RemoveRange(0, All_P_items.Count);
            //All_W_items.RemoveRange(0, All_W_items.Count);
            //All_Marks.RemoveRange(0, All_Marks.Count);            

            //All_W_items.Add(string.Empty);
            //All_W_items.AddRange(Equipment.Select(x => x.W).Distinct());
            //All_G_items.Add(string.Empty);
            //All_G_items.AddRange(Equipment.Select(x => x.G).Distinct());
            //All_P_items.Add(string.Empty);
            //All_P_items.AddRange(Equipment.Select(x => x.P).Distinct());            


            CurrentPageSize = Equipment.Count;
            PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

            RemoveEmptyStringInAlreadyFiltered();
        }

        public void RemoveEmptyStringInAlreadyFiltered()
        {
            if (All_Type_name_items.Count == 2)
                All_Type_name_items.Remove(string.Empty);
            if (All_Group_name_items.Count == 2)
                All_Group_name_items.Remove(string.Empty);
            if (All_SubGroup_name_items.Count == 2)
                All_SubGroup_name_items.Remove(string.Empty);
            if (All_Marks_name_items.Count == 2)
                All_Marks_name_items.Remove(string.Empty);
            //if (All_W_items.Count == 2)
            //    All_W_items.Remove(string.Empty);
            //if (All_G_items.Count == 2)
            //    All_G_items.Remove(string.Empty);
            //if (All_P_items.Count == 2)
            //    All_P_items.Remove(string.Empty);
            //if (All_Marks.Count == 2)
            //    All_Marks.Remove(string.Empty);
        }

    }
}