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
    //public class PageInfo
    //{
    //    public int PageNumber { get; set; } // номер текущей страницы
    //    public int PageSize { get; set; } // кол-во объектов на странице
    //    public int TotalItems { get; set; } // всего объектов
    //    public int TotalPages  // всего страниц
    //    {
    //        get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
    //    }
    //}
    public class AdminEquipmentCollection
    {
        public List<EquipmentElement> Equipment { get; set; }
        public List<string> All_Type_name_items = new List<string>();
        public List<string> All_Group_name_items = new List<string>();
        public List<string> All_SubGroup_name_items = new List<string>();
        public List<string> All_Marks_name_items = new List<string>();
        //public List<EquipmentElement> NotFilteredCollection = new List<EquipmentElement>();
        public IPagedList<EquipmentElement> PagedEquipment { get; set; }
        //public int CurrentPage { get; set; }
        //public int CurrentPageSize = 100;
        //public List<sprEquipment> All_spr = new List<sprEquipment>();

        //Для админских фильтров
        public List<string> SubDepts = new List<string>();
        public List<string> Buildings = new List<string>();
        public List<string> Rooms = new List<string>();
        public string InvNums;
        public string RegNums;
        public IEnumerable<EquipmentElement> Equip { get; set; }
        public PageInfo PageInfo { get; set; }
        public string nameL { get; set; }
        

      //  public AdminEquipmentCollection()
      //  {
            //CurrentPage = 1;

            //using (PPREntities ppre = new PPREntities())
            //{
            //    var Eq = (from items in ppre.NALICH
            //              join tTypeISGroup in ppre.TypeISGroup on items.id_TypeIsGroup equals tTypeISGroup.id
            //              join tTypeOfEq in ppre.sprTypeOfEquipment on tTypeISGroup.idType equals tTypeOfEq.ID
            //              join tGroup in ppre.sprGroup on tTypeISGroup.idGroup equals tGroup.idGroup
            //              join tSubGroup in ppre.sprSubGroup on tTypeISGroup.idSubGroup equals tSubGroup.idSubGroup
            //              join tMark in ppre.sprMark on tTypeISGroup.idMark equals tMark.id_mark
            //              join tModel in ppre.sprModel on tTypeISGroup.idModel equals tModel.id_model
            //              select new
            //              {
            //                  items.ID,
            //                  items.sprSubDept.ShortName,
            //                  items.sprBuildings.BuldingName,
            //                  items.REGNO,
            //                  items.INWNOM,
            //                  items.tAggregateCodifer.SignCode,
            //                  items.NAIMOB,
            //                  items.W,
            //                  items.G,
            //                  items.P,
            //                  items.POM,
            //                  items.MARKA,
            //                  tTypeOfEq.TypeOfEquipment,
            //                  tGroup.GroupName,
            //                  tSubGroup.SubGroupName,
            //                  tMark.mark,
            //                  tModel.model,
            //                  tTypeOfEq.TypeCode,
            //                  tTypeISGroup.oldValue,
            //                  tGroup.idGroup
            //              }).ToList(); //пока для теста пусть выводит первые 200

            //    All_spr = ppre.sprEquipment.ToList();

            //    foreach (var item in Eq)
            //    {
            //        var elem = new EquipmentElement(item.ID);
            //        elem.AggregateOfType = item.SignCode;
            //        elem.BuildingName = item.BuldingName;
            //        elem.InvNumber = item.INWNOM;
            //        elem.Name = item.NAIMOB;
            //        elem.RegNumber = item.REGNO;
            //        elem.SubDeptName = item.ShortName;
            //        elem.W = item.W;
            //        elem.G = item.G;
            //        elem.P = item.P;
            //        elem.Room = item.POM;
            //        elem.Mark = item.MARKA;
            //        //elem.GroupName = item.G + " " + item.GroupName;
            //        elem.TypeName = item.TypeCode + " " + item.TypeOfEquipment;
            //        elem.GroupName = item.idGroup + " " + item.GroupName;
            //        elem.SubGroupName = item.oldValue + " " + item.SubGroupName;
            //        NotFilteredCollection.Add(elem);
            //        Equipment.Add(elem);
            //    }
            //    Equipment = Equipment.OrderBy(x => x.ID).ToList();
            //    All_Type_name_items.Add(string.Empty);
            //    All_Type_name_items.AddRange(NotFilteredCollection.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
            //    All_Group_name_items.Add(string.Empty);
            //    All_Group_name_items.AddRange(NotFilteredCollection.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
            //    All_SubGroup_name_items.Add(string.Empty);
            //    All_SubGroup_name_items.AddRange(NotFilteredCollection.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
            //    All_Marks_name_items.Add(string.Empty);
            //    All_Marks_name_items.AddRange(NotFilteredCollection.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
            //    //Дальнейшее используется в админских фильтрах
            //    SubDepts.Add(string.Empty);
            //    SubDepts.AddRange(NotFilteredCollection.Select(x => x.SubDeptName).Distinct().OrderBy(x => x));
            //    Buildings.Add(string.Empty);
            //    Buildings.AddRange(NotFilteredCollection.Select(x => x.BuildingName).Distinct().OrderBy(x => x));
            //    Rooms.Add(string.Empty);
            //    Rooms.AddRange(NotFilteredCollection.Select(x => x.Room).Distinct().OrderBy(x => x));
            //}
            //if (Equipment.Count < CurrentPageSize)
            //    CurrentPageSize = Equipment.Count;
            //PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);
       // }

        //public void FilterBy_W(string W)
        //{
           
        //    Equipment.RemoveAll(x => x.TypeName != W);  
        //    All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
        //    All_Type_name_items.Add(W); //Оставляем только один элемент, по которому проводилась фильтрация
        //    All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
        //    All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
        //    All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
        //    //Обработка админских фильтров:
        //    SubDepts.RemoveRange(0, SubDepts.Count);
        //    Buildings.RemoveRange(0, Buildings.Count);
        //    Rooms.RemoveRange(0, Rooms.Count);
        //    All_Group_name_items.Add(string.Empty);
        //    All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
        //    //All_SubGroup_name_items.Add(string.Empty);
        //    All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
        //    All_Marks_name_items.Add(string.Empty);
        //    //Обработка админских фильтров:
        //    SubDepts.Add(string.Empty);
        //    SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
        //    Buildings.Add(string.Empty);
        //    Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
        //    Rooms.Add(string.Empty);
        //    Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
        //    Equip = Equipment.AsEnumerable();
        //    //if (Equipment.Count < CurrentPageSize)
        //    //    CurrentPageSize = Equipment.Count;
        //    //PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

        //    RemoveEmptyStringInAlreadyFiltered();
        //}

        //public void FilterBy_G(string G)
        //{
           
        //    Equipment.RemoveAll(x => x.GroupName != G);   
        //    All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
        //    All_Group_name_items.Add(G); //Оставляем только один элемент, по которому проводилась фильтрация
        //    All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
        //    All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
        //    All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
        //    //Обработка админских фильтров:
        //    SubDepts.RemoveRange(0, SubDepts.Count);
        //    Buildings.RemoveRange(0, Buildings.Count);
        //    Rooms.RemoveRange(0, Rooms.Count);
        //    All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
        //    All_SubGroup_name_items.Add(string.Empty);
        //    All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
        //    All_Marks_name_items.Add(string.Empty);
        //    All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
        //    //Обработка админских фильтров:
        //    SubDepts.Add(string.Empty);
        //    SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
        //    Buildings.Add(string.Empty);
        //    Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
        //    Rooms.Add(string.Empty);
        //    Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());

        //    if (Equipment.Count < CurrentPageSize)
        //        CurrentPageSize = Equipment.Count;
        //    PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

        //    RemoveEmptyStringInAlreadyFiltered();
        //}

        //public void FilterBy_P(string P)
        //{
           
        //    Equipment.RemoveAll(x => x.SubGroupName != P);
        //    All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
        //    All_SubGroup_name_items.Add(P); 
        //    All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);         
        //    All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
        //    All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
        //    All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
        //    All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);    
        //    //Обработка админских фильтров:
        //    SubDepts.RemoveRange(0, SubDepts.Count);
        //    Buildings.RemoveRange(0, Buildings.Count);
        //    Rooms.RemoveRange(0, Rooms.Count);
        //    All_Type_name_items.Add(string.Empty);
        //    All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());

        //    All_Group_name_items.Add(string.Empty);
        //    All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());

        //    //All_Marks.Add(string.Empty);
        //    All_Marks_name_items.Add(string.Empty);
        //    All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
        //    //Обработка админских фильтров:
        //    SubDepts.Add(string.Empty);
        //    SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
        //    Buildings.Add(string.Empty);
        //    Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
        //    Rooms.Add(string.Empty);
        //    Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());

        //     if (Equipment.Count < CurrentPageSize)
        //            CurrentPageSize = Equipment.Count;
        //     PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);
        //    RemoveEmptyStringInAlreadyFiltered();
        //}

        //public void FilterBy_Mark(string Mark)
        //{
           
        //    Equipment.RemoveAll(x => x.Mark != Mark);

        //    All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
        //    All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
        //    All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
        //    All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
        //    All_Marks_name_items.Add(Mark);

        //    //All_G_items.RemoveRange(0, All_G_items.Count);
        //    //All_P_items.RemoveRange(0, All_P_items.Count);
        //    //All_W_items.RemoveRange(0, All_W_items.Count);
        //    //All_Marks.RemoveRange(0, All_Marks.Count);
        //    //All_Marks.Add(Mark); //Оставляем только один элемент, по которому проводилась фильтрация
        //    //Обработка админских фильтров:
        //    SubDepts.RemoveRange(0, SubDepts.Count);
        //    Buildings.RemoveRange(0, Buildings.Count);
        //    Rooms.RemoveRange(0, Rooms.Count);

        //    All_Type_name_items.Add(string.Empty);
        //    All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
        //    All_Group_name_items.Add(string.Empty);
        //    All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
        //    All_SubGroup_name_items.Add(string.Empty);
        //    All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());

        //    //All_W_items.Add(string.Empty);
        //    //All_W_items.AddRange(Equipment.Select(x => x.W).Distinct());
        //    //All_G_items.Add(string.Empty);
        //    //All_G_items.AddRange(Equipment.Select(x => x.G).Distinct());
        //    //All_P_items.Add(string.Empty);
        //    //All_P_items.AddRange(Equipment.Select(x => x.P).Distinct());
        //    //Обработка админских фильтров:
        //    SubDepts.Add(string.Empty);
        //    SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
        //    Buildings.Add(string.Empty);
        //    Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
        //    Rooms.Add(string.Empty);
        //    Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
           
        //     if (Equipment.Count < CurrentPageSize)
        //            CurrentPageSize = Equipment.Count;
        //        PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

        //    RemoveEmptyStringInAlreadyFiltered();
        //}

        //public void FilterBySubDepts(string SubDept)
        //{
           
        //    Equipment.RemoveAll(x => x.SubDeptName != SubDept);
        //    //All_G_items.RemoveRange(0, All_G_items.Count);
        //    //All_P_items.RemoveRange(0, All_P_items.Count);
        //    //All_W_items.RemoveRange(0, All_W_items.Count);
        //    //All_Marks.RemoveRange(0, All_Marks.Count);
        //    SubDepts.RemoveRange(0, SubDepts.Count);
        //    Buildings.RemoveRange(0, Buildings.Count);
        //    Rooms.RemoveRange(0, Rooms.Count);
        //    SubDepts.Add(SubDept); //Оставляем только один элемент, по которому проводилась фильтрация

        //    //All_W_items.Add(string.Empty);
        //    //All_W_items.AddRange(Equipment.Select(x => x.W).Distinct());
        //    //All_G_items.Add(string.Empty);
        //    //All_G_items.AddRange(Equipment.Select(x => x.G).Distinct());
        //    //All_P_items.Add(string.Empty);
        //    //All_P_items.AddRange(Equipment.Select(x => x.P).Distinct());

        //    All_Type_name_items.Add(string.Empty);
        //    All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
        //    All_Group_name_items.Add(string.Empty);
        //    All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
        //    All_SubGroup_name_items.Add(string.Empty);
        //    All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
        //    Buildings.Add(string.Empty);
        //    Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
        //    Rooms.Add(string.Empty);
        //    Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());

        //    if (Equipment.Count < CurrentPageSize)
        //        CurrentPageSize = Equipment.Count;
        //    PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

        //    RemoveEmptyStringInAlreadyFiltered();
        //}

        //public void FilterByBuildings(string BuildName)
        //{
           
        //    Equipment.RemoveAll(x => x.BuildingName != BuildName);
        //    All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
        //    All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
        //    All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
        //    All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
        //    //All_G_items.RemoveRange(0, All_G_items.Count);
        //    //All_P_items.RemoveRange(0, All_P_items.Count);
        //    //All_W_items.RemoveRange(0, All_W_items.Count);
        //    //All_Marks.RemoveRange(0, All_Marks.Count);
        //    SubDepts.RemoveRange(0, SubDepts.Count);
        //    Buildings.RemoveRange(0, Buildings.Count);
        //    Rooms.RemoveRange(0, Rooms.Count);
        //    Buildings.Add(BuildName); //Оставляем только один элемент, по которому проводилась фильтрация

        //    //All_W_items.Add(string.Empty);
        //    //All_W_items.AddRange(Equipment.Select(x => x.W).Distinct());
        //    //All_G_items.Add(string.Empty);
        //    //All_G_items.AddRange(Equipment.Select(x => x.G).Distinct());
        //    //All_P_items.Add(string.Empty);
        //    //All_P_items.AddRange(Equipment.Select(x => x.P).Distinct());

        //    All_Type_name_items.Add(string.Empty);
        //    All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
        //    All_Group_name_items.Add(string.Empty);
        //    All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
        //    All_SubGroup_name_items.Add(string.Empty);
        //    All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
        //    SubDepts.Add(string.Empty);
        //    SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
        //    Rooms.Add(string.Empty);
        //    Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());

        //    if (Equipment.Count < CurrentPageSize)
        //        CurrentPageSize = Equipment.Count;
        //    PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

        //    RemoveEmptyStringInAlreadyFiltered();
        //}

        //public void FilterByRooms(string RoomName)
        //{
          
        //    Equipment.RemoveAll(x => x.Room != RoomName);
        //    //All_G_items.RemoveRange(0, All_G_items.Count);
        //    //All_P_items.RemoveRange(0, All_P_items.Count);
        //    //All_W_items.RemoveRange(0, All_W_items.Count);
        //    //All_Marks.RemoveRange(0, All_Marks.Count);

        //    All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
        //    All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
        //    All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
        //    All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
        //    SubDepts.RemoveRange(0, SubDepts.Count);
        //    Buildings.RemoveRange(0, Buildings.Count);
        //    Rooms.RemoveRange(0, Rooms.Count);
        //    Rooms.Add(RoomName); //Оставляем только один элемент, по которому проводилась фильтрация

        //    All_Type_name_items.Add(string.Empty);
        //    All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
        //    All_Group_name_items.Add(string.Empty);
        //    All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
        //    All_SubGroup_name_items.Add(string.Empty);
        //    All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
        //    SubDepts.Add(string.Empty);
        //    SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
        //    Buildings.Add(string.Empty);
        //    Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());

        //    if (Equipment.Count < CurrentPageSize)
        //        CurrentPageSize = Equipment.Count;
        //    PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

        //    RemoveEmptyStringInAlreadyFiltered();
        //}

        //public void FilterByRegNo(string RegNo)
        //{
           
        //    Equipment.RemoveAll(x => x.RegNumber != RegNo);
        //    //All_G_items.RemoveRange(0, All_G_items.Count);
        //    //All_P_items.RemoveRange(0, All_P_items.Count);
        //    //All_W_items.RemoveRange(0, All_W_items.Count);
        //    //All_Marks.RemoveRange(0, All_Marks.Count);
        //    All_Group_name_items.RemoveRange(0, All_Group_name_items.Count);
        //    All_SubGroup_name_items.RemoveRange(0, All_SubGroup_name_items.Count);
        //    All_Type_name_items.RemoveRange(0, All_Type_name_items.Count);
        //    All_Marks_name_items.RemoveRange(0, All_Marks_name_items.Count);
        //    SubDepts.RemoveRange(0, SubDepts.Count);
        //    Buildings.RemoveRange(0, Buildings.Count);
        //    Rooms.RemoveRange(0, Rooms.Count);

        //    //All_W_items.Add(string.Empty);
        //    //All_W_items.AddRange(Equipment.Select(x => x.W).Distinct());
        //    //All_G_items.Add(string.Empty);
        //    //All_G_items.AddRange(Equipment.Select(x => x.G).Distinct());
        //    //All_P_items.Add(string.Empty);
        //    //All_P_items.AddRange(Equipment.Select(x => x.P).Distinct());
        //    All_Type_name_items.Add(string.Empty);
        //    All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
        //    All_Group_name_items.Add(string.Empty);
        //    All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
        //    All_SubGroup_name_items.Add(string.Empty);
        //    All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
        //    SubDepts.Add(string.Empty);
        //    SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
        //    Buildings.Add(string.Empty);
        //    Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
        //    Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());

        //    CurrentPageSize = Equipment.Count;
        //    PagedEquipment = Equipment.ToPagedList(CurrentPage, CurrentPageSize);

        //    RemoveEmptyStringInAlreadyFiltered();
        //}

        public void RemoveEmptyStringInAlreadyFiltered()
        {
            //if (All_W_items.Count == 2)
            //    All_W_items.Remove(string.Empty);
            //if (All_G_items.Count == 2)
            //    All_G_items.Remove(string.Empty);
            //if (All_P_items.Count == 2)
            //    All_P_items.Remove(string.Empty);
            //if (All_Marks.Count == 2)
            //    All_Marks.Remove(string.Empty);

            if (All_Type_name_items.Count == 2)
                All_Type_name_items.Remove(string.Empty);
            if (All_Group_name_items.Count == 2)
                All_Group_name_items.Remove(string.Empty);
            if (All_SubGroup_name_items.Count == 2)
                All_SubGroup_name_items.Remove(string.Empty);
            if (All_Marks_name_items.Count == 2)
                All_Marks_name_items.Remove(string.Empty);

            if (Buildings.Count == 2)
                Buildings.Remove(string.Empty);
            if (SubDepts.Count == 2)
                SubDepts.Remove(string.Empty);
            if (Rooms.Count == 2)
                Rooms.Remove(string.Empty);
        }
    }
}