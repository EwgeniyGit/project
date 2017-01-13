using EQDataModel;
using EQServiceSystem.Models;
using EQServiceSystem.Models.Units;
using EQServiceSystem.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EQServiceSystem.Controllers
{
    public class EqController : Controller
    {
        //
        // GET: /Eq/
        RightsChecker CheckedObject = new RightsChecker();
        FunctionAccessManager famanager;
        List<EquipmentElement> Equipment = new List<EquipmentElement>();
        public List<string> All_Type_name = new List<string>();
        public List<string> All_Group_name = new List<string>();
        public List<string> All_SubGroup_name = new List<string>();
        public List<string> All_Marks_name = new List<string>();
        public List<string> All_SubDepts = new List<string>();
        public List<string> All_Buildings = new List<string>();
        public List<string> All_Rooms = new List<string>();
        public List<string> RY = new List<string>();
        public bool special_option;
        public IPagedList<EquipmentElement> PagedEquip;
        public List<EquipmentElement> NotFilteredModel()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var Eq = (from items in ppre.NALICH
                          join tTypeISGroup in ppre.TypeISGroup on items.id_TypeIsGroup equals tTypeISGroup.id
                          join tTypeOfEq in ppre.sprTypeOfEquipment on tTypeISGroup.idType equals tTypeOfEq.ID
                          join tGroup in ppre.sprGroup on tTypeISGroup.idGroup equals tGroup.idGroup
                          join tSubGroup in ppre.sprSubGroup on tTypeISGroup.idSubGroup equals tSubGroup.idSubGroup
                          join tMark in ppre.sprMark on tTypeISGroup.idMark equals tMark.id_mark
                          join tModel in ppre.sprModel on tTypeISGroup.idModel equals tModel.id_model
                          select new
                          {
                              items.ID,
                              items.sprSubDept.ShortName,
                              items.sprSubDept.SubDeptCode,
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
                              tGroup.GroupName,
                              tSubGroup.SubGroupName,
                              tMark.mark,
                              tModel.model,
                              tTypeOfEq.TypeCode,
                              tTypeISGroup.oldValue,
                              tGroup.idGroup
                          }).ToList();
                foreach (var item in Eq)
                {
                    var elem = new EquipmentElement(item.ID);
                    elem.AggregateOfType = item.SignCode;
                    elem.BuildingName = item.BuldingName;
                    elem.InvNumber = item.INWNOM;
                    elem.Name = item.NAIMOB;
                    elem.RegNumber = item.REGNO;
                    //elem.SubDeptName = item.ShortName;
                    elem.W = item.W;
                    elem.G = item.G;
                    elem.P = item.P;
                    elem.Room = item.POM;
                    elem.Mark = item.MARKA;
                    elem.TypeName = item.TypeCode + " " + item.TypeOfEquipment;
                    if (item.idGroup.ToString().Length < 2)
                        elem.GroupName = "0" + item.idGroup + " " + item.GroupName;
                    else
                        elem.GroupName = item.idGroup + " " + item.GroupName;
                    //elem.GroupName = item.idGroup + " " + item.GroupName;
                    elem.TypeCode = item.TypeCode;
                    elem.SubGroupName = item.oldValue + " " + item.SubGroupName;
                    elem.SubDeptName = item.SubDeptCode + " " + item.ShortName;
                    Equipment.Add(elem);
                }
            }
            return Equipment;
        }
        public List<EquipmentElement> NotFilteredModelRU()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var Eq = (from items in ppre.NALICH
                          join tTypeISGroup in ppre.TypeISGroup on items.id_TypeIsGroup equals tTypeISGroup.id
                          join tTypeOfEq in ppre.sprTypeOfEquipment on tTypeISGroup.idType equals tTypeOfEq.ID
                          join tGroup in ppre.sprGroup on tTypeISGroup.idGroup equals tGroup.idGroup
                          join tSubGroup in ppre.sprSubGroup on tTypeISGroup.idSubGroup equals tSubGroup.idSubGroup
                          join tMark in ppre.sprMark on tTypeISGroup.idMark equals tMark.id_mark
                          join tModel in ppre.sprModel on tTypeISGroup.idModel equals tModel.id_model
                          join tImportantEquipments in ppre.tImportantEquipment on items.ID equals tImportantEquipments.EquipmentID
                          join tReactors in ppre.sprReactors on tImportantEquipments.ReactorID equals tReactors.ID
                          select new
                          {
                              items.ID,
                              items.sprSubDept.ShortName,
                              items.sprSubDept.SubDeptCode,
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
                              tGroup.GroupName,
                              tSubGroup.SubGroupName,
                              tMark.mark,
                              tModel.model,
                              tTypeOfEq.TypeCode,
                              tTypeISGroup.oldValue,
                              tGroup.idGroup,
                              tReactors.Name
                          }).ToList();
                foreach (var item in Eq)
                {
                    var elem = new EquipmentElement(item.ID);
                    elem.AggregateOfType = item.SignCode;
                    elem.BuildingName = item.BuldingName;
                    elem.InvNumber = item.INWNOM;
                    elem.Name = item.NAIMOB;
                    elem.RegNumber = item.REGNO;
                    //elem.SubDeptName = item.ShortName;
                    elem.W = item.W;
                    elem.G = item.G;
                    elem.P = item.P;
                    elem.Room = item.POM;
                    elem.Mark = item.MARKA;
                    elem.TypeName = item.TypeCode + " " + item.TypeOfEquipment;
                    if (item.idGroup.ToString().Length < 2)
                        elem.GroupName = "0" + item.idGroup + " " + item.GroupName;
                    else
                        elem.GroupName = item.idGroup + " " + item.GroupName;
                    //elem.GroupName = item.idGroup + " " + item.GroupName;
                    elem.SubGroupName = item.oldValue + " " + item.SubGroupName;
                    elem.SubDeptName = item.SubDeptCode + " " + item.ShortName;
                    elem.RU = item.Name;
                    Equipment.Add(elem);
                }
            }
            return Equipment;
        }
        public ActionResult Ind(int page, string all_W, string all_G, string all_P, string all_Marks, string SubDepts, string Buildings, string Rooms, string name, string RegNums, string InvNums)
        {
            var Model = new AdminEquipmentCollection();
            if ((all_W != string.Empty) && (all_W != null) && (all_W != " ") ||
                (all_G != string.Empty) && (all_G != null) && (all_G != " ") ||
                (all_P != string.Empty) && (all_P != null) && (all_P != " ") ||
                (all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " ") ||
                (SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " ") ||
                (Buildings != string.Empty) && (Buildings != null) && (Buildings != " ") ||
                (name != string.Empty) && (name != null) && (name != " ") ||
                (Rooms != string.Empty) && (Rooms != null) && (Rooms != " ")||
                (RegNums != string.Empty) && (RegNums != null) && (RegNums != " ")||
                (InvNums != string.Empty) && (InvNums != null) && (InvNums != " "))
            {
                UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
                Model.Equipment = NotFilteredModel();
                //Model.Equipment = Model.Equipment.Where(x => x.SubDeptName == uif.UserSubDeptCode + " " + uif.UserSubDept).ToList();
                if (uif.UserRole == "Механик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "М") || (x.TypeCode == "А")).ToList(); special_option = false; }
                if (uif.UserRole == "Энергетик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; }
                if (uif.UserRole == "Пользователь на РУ") { Equipment = NotFilteredModelRU(); special_option = true; }
                if (uif.UserRole == "ЦСР") { Model.Equipment = Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; Equipment = Model.Equipment; } 
               if ((name != string.Empty) && (name != null) && (name != " "))
                    FilterBy_Name(name, Model);
                if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                    FilterBy_W(all_W, Model);
                if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                    FilterBy_G(all_G, Model);
                if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                    FilterBy_P(all_P, Model);
                if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                    FilterBy_Mark(all_Marks, Model);
                if ((SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " "))
                    FilterBySubDepts(SubDepts, Model);
                if ((Buildings != string.Empty) && (Buildings != null) && (Buildings != " "))
                    FilterByBuildings(Buildings, Model);
                if ((Rooms != string.Empty) && (Rooms != null) && (Rooms != " "))
                    FilterByRooms(Rooms, Model);
                if ((RegNums != string.Empty) && (RegNums != null) && (RegNums != " "))
                    FilterBy_RegNums(RegNums, Model);
                if ((InvNums != string.Empty) && (InvNums != null) && (InvNums != " "))
                    FilterBy_InvNums(InvNums, Model);
                Model.PageInfo.PageNumber = page;
                int pageSize = 100;
                PagedEquip = Model.Equipment.ToPagedList(page, pageSize);
            }
            //FilterBy_W(all_W, Model);
            //FilterBy_G(all_G, Model);
            else
            {
                UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
                //Equipment = Equipment.Where(x => x.SubDeptName == uif.UserSubDeptCode + " " + uif.UserSubDept).ToList();
                Model.Equipment = NotFilteredModel();
                if (uif.UserRole == "Механик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "М") || (x.TypeCode == "А")).ToList(); special_option = false; }
                if (uif.UserRole == "Энергетик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; }
                if (uif.UserRole == "Пользователь на РУ") { Equipment = NotFilteredModelRU(); special_option = true; }
                if (uif.UserRole == "ЦСР") { Model.Equipment = Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; Equipment = Model.Equipment; }        
                if (Model.PageInfo == null) Model.PageInfo = new PageInfo { PageNumber = page, PageSize = 100, TotalItems = Model.Equipment.Count };
                Equipment = Model.Equipment;
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Model.Equipment.Select(x => x.SubDeptName).Distinct().OrderBy(x => x));
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Model.Equipment.Select(x => x.BuildingName).Distinct().OrderBy(x => x));
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Model.Equipment.Select(x => x.Room).Distinct().OrderBy(x => x));
                PagedEquip = Model.Equipment.ToPagedList(page, Model.PageInfo.PageSize);
            }
            IEnumerable<EquipmentElement> Equipments = Model.Equipment.Skip((page - 1) * Model.PageInfo.PageSize).Take(Model.PageInfo.PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = Model.PageInfo.PageSize, TotalItems = Model.Equipment.Count };

            AdminEquipmentCollection ivm = new AdminEquipmentCollection
            {
                PageInfo = pageInfo,
                Equipment = Model.Equipment,
                Equip = Equipments,
                All_Type_name_items = Model.All_Type_name_items,
                All_Group_name_items = Model.All_Group_name_items,
                All_SubGroup_name_items = Model.All_SubGroup_name_items,
                All_Marks_name_items = Model.All_Marks_name_items,
                SubDepts = Model.SubDepts,
                Buildings = Model.Buildings,
                Rooms = Model.Rooms,
                PagedEquipment = PagedEquip,
                nameL=name,
                RegNums=RegNums,
                InvNums=InvNums
            };
            if (ivm.PagedEquipment.Count <= ivm.PageInfo.PageSize) ivm.PageInfo.PageSize = ivm.PagedEquipment.Count;
            return PartialView("~/Views/Equipment/AdminEquipmentList.cshtml", ivm);
        }
        public ActionResult Index(int page)
        {
            UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
            int pageSize = 100; // количество объектов на страницу

                if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "EquipmentSectionRU", AccessType.Read))
                    {
                     Equipment = NotFilteredModel();
                    IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
                    PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
                    special_option = false;
                    All_Type_name.Add(string.Empty);
                    All_Type_name.AddRange(Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                    All_Group_name.Add(string.Empty);
                    All_Group_name.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                    All_Group_name = SortList(All_Group_name.ToList());
                    All_SubGroup_name.Add(string.Empty);
                    All_SubGroup_name.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                    All_SubGroup_name = SortList(All_SubGroup_name.ToList());
                    All_Marks_name.Add(string.Empty);
                    All_Marks_name.AddRange(Equipment.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                    All_SubDepts.Add(string.Empty);
                    All_SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct().OrderBy(x => x));
                    All_SubDepts = SortList(All_SubDepts.ToList());
                    All_Buildings.Add(string.Empty);
                    All_Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct().OrderBy(x => x));
                    All_Rooms.Add(string.Empty);
                    All_Rooms.AddRange(Equipment.Select(x => x.Room).Distinct().OrderBy(x => x));
                    PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);

                    AdminEquipmentFullCollection admFull = new AdminEquipmentFullCollection
                    {
                        PageInfo = pageInfo,
                        Equipment = Equipment,
                        Equip = Equipments,
                        All_Type_name_items = All_Type_name,
                        All_Group_name_items = All_Group_name,
                        All_SubGroup_name_items = All_SubGroup_name,
                        All_Marks_name_items = All_Marks_name,
                        SubDepts = All_SubDepts,
                        Buildings = All_Buildings,
                        Rooms = All_Rooms,
                        PagedEquipment = PagedEquip
                    };
                    if (admFull.PagedEquipment.Count <= pageSize) admFull.PageInfo.PageSize = admFull.PagedEquipment.Count;
                    ViewBag.Param = 1;
                    return PartialView("~/Views/Equipment/AdminEquipmentFullCollection.cshtml", admFull);
                } 
                else
                    {
                        Equipment = NotFilteredModel();
                        //Equipment = Equipment.Where(x => x.SubDeptName == uif.UserSubDeptCode+" "+uif.UserSubDept).ToList();
                        if (uif.UserRole == "Механик") { Equipment = Equipment.Where(x => (x.TypeCode == "М") || (x.TypeCode == "А")).ToList(); special_option = false; }
                        if (uif.UserRole == "Энергетик") { Equipment = Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; }
                        if (uif.UserRole == "Пользователь на РУ") { Equipment.Clear(); Equipment = NotFilteredModelRU(); special_option = true; }
                        if (uif.UserRole == "ЦСР") { Equipment = Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; } 
                    }  
                //NotFilteredModel();
                //special_option = false;
//-----
            if (!special_option)
            {
                IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };

                All_Type_name.Add(string.Empty);
                All_Type_name.AddRange(Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                All_Group_name.Add(string.Empty);
                All_Group_name.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                All_Group_name = SortList(All_Group_name.ToList());
                All_SubGroup_name.Add(string.Empty);
                All_SubGroup_name.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                All_SubGroup_name = SortList(All_SubGroup_name.ToList());
                All_Marks_name.Add(string.Empty);
                All_Marks_name.AddRange(Equipment.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                All_SubDepts.Add(string.Empty);
                All_SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct().OrderBy(x => x));
                All_SubDepts = SortList(All_SubDepts.ToList());
                All_Buildings.Add(string.Empty);
                All_Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct().OrderBy(x => x));
                All_Rooms.Add(string.Empty);
                All_Rooms.AddRange(Equipment.Select(x => x.Room).Distinct().OrderBy(x => x));
                PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
                AdminEquipmentCollection ivm = new AdminEquipmentCollection
                {
                    PageInfo = pageInfo,
                    Equipment = Equipment,
                    Equip = Equipments,
                    All_Type_name_items = All_Type_name,
                    All_Group_name_items = All_Group_name,
                    All_SubGroup_name_items = All_SubGroup_name,
                    All_Marks_name_items = All_Marks_name,
                    SubDepts = All_SubDepts,
                    Buildings = All_Buildings,
                    Rooms = All_Rooms,
                    PagedEquipment = PagedEquip
                };
                if (ivm.PagedEquipment.Count <= pageSize) ivm.PageInfo.PageSize = ivm.PagedEquipment.Count;
                return PartialView("~/Views/Equipment/AdminEquipmentList.cshtml", ivm);
            }
            else
            {
                //Equipment=NotFilteredModelRU();
                IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
                All_Type_name.Add(string.Empty);
                All_Type_name.AddRange(Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                All_Group_name.Add(string.Empty);
                All_Group_name.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                All_Group_name = SortList(All_Group_name.ToList());
                All_SubGroup_name.Add(string.Empty);
                All_SubGroup_name.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                All_SubGroup_name = SortList(All_SubGroup_name.ToList());
                All_Marks_name.Add(string.Empty);
                All_Marks_name.AddRange(Equipment.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                RY.Add(string.Empty);
                RY.AddRange(Equipment.Select(x => x.RU).Distinct().OrderBy(x => x));
                PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
                AdminEquipmentRU ivm = new AdminEquipmentRU
                {
                    PageInfo = pageInfo,
                    Equipment = Equipment,
                    Equip = Equipments,
                    All_Type_name_items = All_Type_name,
                    All_Group_name_items = All_Group_name,
                    All_SubGroup_name_items = All_SubGroup_name,
                    All_Marks_name_items = All_Marks_name,
                    RU = RY,
                    PagedEquipment = PagedEquip
                };
                return PartialView("~/Views/Equipment/AdminEquipmentListRU.cshtml", ivm);
            }
        }
        [HttpPost]
        public ActionResult GetAdminEquipmentList(string all_W, string all_G, string all_P, string all_Marks, string SubDepts, string Buildings, string Rooms, string name, string regnum, string invnum)
        {
            var Model = new AdminEquipmentCollection();
            Model.Equipment = NotFilteredModel();
            UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
            if (uif.UserRole == "Механик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "М") || (x.TypeCode == "А")).ToList(); special_option = false; }
            if (uif.UserRole == "Энергетик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; }
            if (uif.UserRole == "Пользователь на РУ") { Model.Equipment = NotFilteredModelRU(); special_option = true; }
            if (uif.UserRole == "ЦСР") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; }
            if ((name != string.Empty) && (name != null) && (name != " "))
                FilterBy_Name(name, Model);
            if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                FilterBy_W(all_W, Model);
            if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                FilterBy_G(all_G, Model);
            if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                FilterBy_P(all_P, Model);
            if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                FilterBy_Mark(all_Marks, Model);
            if ((SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " "))
                FilterBySubDepts(SubDepts, Model);
            if ((Buildings != string.Empty) && (Buildings != null) && (Buildings != " "))
                FilterByBuildings(Buildings, Model);
            if ((Rooms != string.Empty) && (Rooms != null) && (Rooms != " "))
                FilterByRooms(Rooms, Model);
            if ((regnum != string.Empty) && (regnum != null) && (regnum != " "))
                FilterBy_RegNums(regnum, Model);
            if ((invnum != string.Empty) && (invnum != null) && (invnum != " "))
                FilterBy_InvNums(invnum, Model);
       
            //if ((SubDepts != string.Empty) && (Model.SubDepts.Count > 1))
            //    FilterBySubDepts(SubDepts, Model);
            //if ((Buildings != string.Empty) && (Model.Buildings.Count > 1))
            //    FilterByBuildings(Buildings, Model);
            //if ((Rooms != string.Empty) && (Model.Rooms.Count > 1))
            //    FilterByRooms(Rooms, Model);
            Equipment = Model.Equipment;
            return PartialView("~/Views/Equipment/AdminEquipmentList.cshtml", Model);
        }
        public void FilterBy_W(string W, AdminEquipmentCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Equipment.RemoveAll(x => x.TypeName != W);
            Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_G(string G, AdminEquipmentCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Equipment.RemoveAll(x => x.GroupName != G);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_Group_name_items.Add(G);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);

            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }

            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_P(string P, AdminEquipmentCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.Equipment.RemoveAll(x => x.SubGroupName != P);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_SubGroup_name_items.Add(P);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);

            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_Mark(string Mark, AdminEquipmentCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Equipment.RemoveAll(x => x.Mark != Mark);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.All_Marks_name_items.Add(Mark);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBySubDepts(string SubDept, AdminEquipmentCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Equipment.RemoveAll(x => x.SubDeptName != SubDept);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.SubDepts.Add(SubDept);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);

            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterByBuildings(string BuildName, AdminEquipmentCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Equipment.RemoveAll(x => x.BuildingName != BuildName);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Buildings.Add(BuildName);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterByRooms(string RoomName, AdminEquipmentCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Equipment.RemoveAll(x => x.Room != RoomName);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            Model.Rooms.Add(RoomName);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_Name(string name, AdminEquipmentCollection Model)
        {
            UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
            //Model.Equipment = NotFilteredModel();
            if (uif.UserRole == "Механик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "М") || (x.TypeCode == "А")).ToList(); special_option = false; }
            if (uif.UserRole == "Энергетик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; }
            if (uif.UserRole == "Пользователь на РУ") { Model.Equipment = NotFilteredModelRU(); special_option = true; }
            if (uif.UserRole == "ЦСР") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; Equipment = Model.Equipment; }
            if (Model.PageInfo == null) Model.PageInfo = new PageInfo { PageNumber = 1, PageSize = 100, TotalItems = Model.Equipment.Count };
            Model.Equipment.RemoveAll(x => !x.Name.Contains(name));
            //Equipment = Model.Equipment;
            var t = Model.Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Model.Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Model.Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                //Model.SubGroup_items = SortList(Model.SubGroup_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Model.Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Model.Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Model.Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Model.Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Model.Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Model.Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Model.Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Model.Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Model.Equipment.Select(x => x.Room).Distinct());
            }
            Model.nameL = name;
            PageInfo pageInfo = new PageInfo { PageNumber = 1, PageSize = 100, TotalItems = Model.Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Model.Equipment.Skip((pageInfo.PageNumber - 1) * Model.PageInfo.PageSize).Take(Model.PageInfo.PageSize); ;
            Model.Equip = Equipments;
            PagedEquip = Model.Equipment.ToPagedList(1, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= 100) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_RegNums(string RegNums, AdminEquipmentCollection Model)
        {
            UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
            //Model.Equipment = NotFilteredModel();
            if (uif.UserRole == "Механик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "М") || (x.TypeCode == "А")).ToList(); special_option = false; }
            if (uif.UserRole == "Энергетик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; }
            if (uif.UserRole == "Пользователь на РУ") { Model.Equipment = NotFilteredModelRU(); special_option = true; }
            if (uif.UserRole == "ЦСР") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; Equipment = Model.Equipment; }
            if (Model.PageInfo == null) Model.PageInfo = new PageInfo { PageNumber = 1, PageSize = 100, TotalItems = Model.Equipment.Count };
            Model.Equipment.RemoveAll(x => !x.RegNumber.Contains(RegNums));
            //Equipment = Model.Equipment;
            var t = Model.Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Model.Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Model.Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                //Model.SubGroup_items = SortList(Model.SubGroup_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Model.Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Model.Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Model.Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Model.Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Model.Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Model.Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Model.Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Model.Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Model.Equipment.Select(x => x.Room).Distinct());
            }
            Model.RegNums = RegNums;
            PageInfo pageInfo = new PageInfo { PageNumber = 1, PageSize = 100, TotalItems = Model.Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Model.Equipment.Skip((pageInfo.PageNumber - 1) * Model.PageInfo.PageSize).Take(Model.PageInfo.PageSize); ;
            Model.Equip = Equipments;
            PagedEquip = Model.Equipment.ToPagedList(1, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= 100) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_InvNums(string InvNums, AdminEquipmentCollection Model)
        {
            UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
            //Model.Equipment = NotFilteredModel();
            if (uif.UserRole == "Механик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "М") || (x.TypeCode == "А")).ToList(); special_option = false; }
            if (uif.UserRole == "Энергетик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; }
            if (uif.UserRole == "Пользователь на РУ") { Model.Equipment = NotFilteredModelRU(); special_option = true; }
            if (uif.UserRole == "ЦСР") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); special_option = false; Equipment = Model.Equipment; }
            if (Model.PageInfo == null) Model.PageInfo = new PageInfo { PageNumber = 1, PageSize = 100, TotalItems = Model.Equipment.Count };
            Model.Equipment.RemoveAll(x => !x.InvNumber.Contains(InvNums));
            //Equipment = Model.Equipment;
            var t = Model.Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Model.Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Model.Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                //Model.SubGroup_items = SortList(Model.SubGroup_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Model.Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Model.Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Model.Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Model.Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Model.Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Model.Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Model.Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Model.Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Model.Equipment.Select(x => x.Room).Distinct());
            }
            Model.InvNums = InvNums;
            PageInfo pageInfo = new PageInfo { PageNumber = 1, PageSize = 100, TotalItems = Model.Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Model.Equipment.Skip((pageInfo.PageNumber - 1) * Model.PageInfo.PageSize).Take(Model.PageInfo.PageSize); ;
            Model.Equip = Equipments;
            PagedEquip = Model.Equipment.ToPagedList(1, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= 100) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public List<string> SortList(List<string> list) //сортировка выпадающих списков с кодами
        {
            List<Tuple<string, string>> countries = new List<Tuple<string, string>>();
            List<Tuple<int, string>> Icountries = new List<Tuple<int, string>>();
            List<string> NewList = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i] != string.Empty) && (list[i] != " ") && (list[i] != null))
                {
                    var code = list[i].Split(' ')[0];
                    var name = list[i].Substring(code.Length, list[i].Length - code.Length);
                    //Tuple<string, string> str = new Tuple<string, string>(code, name);
                    Tuple<int, string> strI = new Tuple<int, string>(Convert.ToInt32(code), name);
                    //countries.Add(str);
                    Icountries.Add(strI);
                }
                else
                {
                    Tuple<string, string> str = new Tuple<string, string>(list[i], list[i]);
                    countries.Add(str);
                }

            }
            foreach (var item in countries)
            {
                if ((item.Item1 == string.Empty) || (item.Item1 == " ") || (item.Item1 == null))
                    NewList.Add(item.Item1);
                //else
                //NewList.Add(item.Item1+item.Item2);
            }
            Icountries.Sort(delegate(Tuple<int, string> us1, Tuple<int, string> us2)
            { return us1.Item1.CompareTo(us2.Item1); }); ;
            foreach (var item in Icountries)
            {
                if (item.Item1.ToString().Length < 2)
                    NewList.Add("0" + item.Item1.ToString() + item.Item2.ToString());
                else
                    NewList.Add(item.Item1.ToString() + item.Item2.ToString());
            }
            return NewList;
        }
        public ActionResult GetAdminEquipmentListRU(string all_W, string all_G, string all_P, string all_Marks, string RU, string name, string regnum, string invnum)
        {
            var Model = new AdminEquipmentRU();
            Model.Equipment = NotFilteredModelRU();
            if ((name != string.Empty) && (name != null) && (name != " "))
                FilterBy_Name(name, Model);
            if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                FilterBy_W(all_W, Model);
            if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                FilterBy_G(all_G, Model);
            if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                FilterBy_P(all_P, Model);
            if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                FilterBy_Mark(all_Marks, Model);
            if ((RU != string.Empty) && (RU != null) && (RU != " "))
                FilterByRU(RU, Model);
            if ((regnum != string.Empty) && (regnum != null) && (regnum != ""))
                FilterBy_RegNums(regnum, Model);
            if ((invnum != string.Empty) && (invnum != null) && (invnum != ""))
                FilterBy_InvNums(invnum, Model);
            //if ((SubDepts != string.Empty) && (Model.SubDepts.Count > 1))
            //    FilterBySubDepts(SubDepts, Model);
            //if ((Buildings != string.Empty) && (Model.Buildings.Count > 1))
            //    FilterByBuildings(Buildings, Model);
            //if ((Rooms != string.Empty) && (Model.Rooms.Count > 1))
            //    FilterByRooms(Rooms, Model);
            Model.Equipment = Equipment;
            return PartialView("~/Views/Equipment/AdminEquipmentListRU.cshtml", Model);
        }
        public void FilterBy_W(string W, AdminEquipmentRU Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.TypeName != W);
            Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }

            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_G(string G, AdminEquipmentRU Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.GroupName != G);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_Group_name_items.Add(G);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);

            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }


            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_P(string P, AdminEquipmentRU Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.SubGroupName != P);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_SubGroup_name_items.Add(P);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);

            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }


            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_Mark(string Mark, AdminEquipmentRU Model)
        {
            var page = 1;
            var pageSize = 100;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment = Model.Equipment;
            Equipment.RemoveAll(x => x.Mark != Mark);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.All_Marks_name_items.Add(Mark);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterByRU(string RU, AdminEquipmentRU Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.RU != RU);
            Model.RU.RemoveRange(0, Model.RU.Count);
            Model.RU.Add(RU);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_RegNums(string RegNums, AdminEquipmentRU Model)
        {
            special_option = true;
            Model.Equipment=Model.Equipment.Where(x => x.RegNumber.Contains(RegNums)).ToList();
          //  Model.Equipment.RemoveAll(x => !x.RegNumber.Contains(RegNums));
            //Equipment = Model.Equipment;
            var t = Model.Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Model.Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Model.Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                //Model.SubGroup_items = SortList(Model.SubGroup_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Model.Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            var ru = Model.Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Model.Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Model.Equipment.Select(x => x.RU).Distinct());
            }
            Model.RegNums = RegNums;
            PageInfo pageInfo = new PageInfo { PageNumber = 1, PageSize = 100, TotalItems = Model.Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Model.Equipment.Skip((pageInfo.PageNumber - 1) * Model.PageInfo.PageSize).Take(Model.PageInfo.PageSize); ;
            Model.Equip = Equipments;
            PagedEquip = Model.Equipment.ToPagedList(1, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= 100) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_InvNums(string InvNums, AdminEquipmentRU Model)
        {
            special_option = true;
            Model.Equipment.RemoveAll(x => !x.InvNumber.Contains(InvNums));
            //Equipment = Model.Equipment;
            var t = Model.Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Model.Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Model.Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                //Model.SubGroup_items = SortList(Model.SubGroup_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Model.Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            var ru = Model.Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Model.Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Model.Equipment.Select(x => x.RU).Distinct());
            }
            Model.InvNums = InvNums;
            PageInfo pageInfo = new PageInfo { PageNumber = 1, PageSize = 100, TotalItems = Model.Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Model.Equipment.Skip((pageInfo.PageNumber - 1) * Model.PageInfo.PageSize).Take(Model.PageInfo.PageSize); ;
            Model.Equip = Equipments;
            PagedEquip = Model.Equipment.ToPagedList(1, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= 100) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_Name(string name, AdminEquipmentRU Model)
        {
          //  Model.All_Type_name_items.Clear();
          //  Model.All_Group_name_items.Clear();
          //  Model.All_SubGroup_name_items.Clear();
          //  Model.All_Marks_name_items.Clear();
          //  Model.RU.Clear();
          //  Model.Equipment.Clear();
          //Model.Equipment = NotFilteredModelRU();
          special_option = true;
          Model.Equipment.RemoveAll(x => !x.Name.Contains(name));
            //Equipment = Model.Equipment;
            var t = Model.Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Model.Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Model.Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                //Model.SubGroup_items = SortList(Model.SubGroup_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Model.Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct());
            }
            var ru = Model.Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Model.Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Model.Equipment.Select(x => x.RU).Distinct());
            }
            Model.nameL = name;
            PageInfo pageInfo = new PageInfo { PageNumber = 1, PageSize = 100, TotalItems = Model.Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Model.Equipment.Skip((pageInfo.PageNumber - 1) * Model.PageInfo.PageSize).Take(Model.PageInfo.PageSize); ;
            Model.Equip = Equipments;
            PagedEquip = Model.Equipment.ToPagedList(1, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= 100) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public ActionResult RuInd(int page, string all_W, string all_G, string all_P, string all_Marks, string RU, string name, string RegNums, string InvNums)
        {
            var Model = new AdminEquipmentRU();
            if ((all_W != string.Empty) && (all_W != null) && (all_W != " ") ||
                (all_G != string.Empty) && (all_G != null) && (all_G != " ") ||
                (all_P != string.Empty) && (all_P != null) && (all_P != " ") ||
                (all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " ") ||
                (name != string.Empty) && (name != null) && (name != " ") ||
                (RU != string.Empty) && (RU != null) && (RU != " ")||
                (RegNums != string.Empty) && (RegNums != null) && (RegNums != " ")||
                (InvNums != string.Empty) && (InvNums != null) && (InvNums != " "))
            {
                Model.Equipment = NotFilteredModelRU();
                if ((name != string.Empty) && (name != null) && (name != " "))
                    FilterBy_Name(name, Model);
                if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                    FilterBy_W(all_W, Model);
                if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                    FilterBy_G(all_G, Model);
                if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                    FilterBy_P(all_P, Model);
                if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                    FilterBy_Mark(all_Marks, Model);
                if ((RU != string.Empty) && (RU != null) && (RU != " "))
                    FilterByRU(RU, Model);
                if ((RegNums != string.Empty) && (RegNums != null) && (RegNums != " "))
                    FilterBy_RegNums(RU, Model);
                if ((InvNums != string.Empty) && (InvNums != null) && (InvNums != " "))
                    FilterBy_InvNums(InvNums, Model);
                Model.PageInfo.PageNumber = page;
                int pageSize = 100;
                PagedEquip = Equipment.ToPagedList(page, pageSize);
            }
            else
            {
                Model.Equipment = NotFilteredModelRU();
                if (Model.PageInfo == null) Model.PageInfo = new PageInfo { PageNumber = page, PageSize = 100, TotalItems = Model.Equipment.Count };
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Model.Equipment.Select(x => x.RU).Distinct().OrderBy(x => x));
                PagedEquip = Equipment.ToPagedList(page, Model.PageInfo.PageSize);
            }
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * Model.PageInfo.PageSize).Take(Model.PageInfo.PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = Model.PageInfo.PageSize, TotalItems = Equipment.Count };

            AdminEquipmentRU ivm = new AdminEquipmentRU
            {
                PageInfo = pageInfo,
                Equipment = Equipment,
                Equip = Equipments,
                All_Type_name_items = Model.All_Type_name_items,
                All_Group_name_items = Model.All_Group_name_items,
                All_SubGroup_name_items = Model.All_SubGroup_name_items,
                All_Marks_name_items = Model.All_Marks_name_items,
                PagedEquipment = PagedEquip,
                RU = Model.RU,
                nameL=name
            };
            if (ivm.PagedEquipment.Count <= ivm.PageInfo.PageSize) ivm.PageInfo.PageSize = ivm.PagedEquipment.Count;
            return PartialView("~/Views/Equipment/AdminEquipmentListRU.cshtml", ivm);
        }
        [HttpPost]
        public ActionResult RadioButtonCheck(string i, string all_W, string all_G, string all_P, string all_Marks, string SubDepts, string Buildings, string Rooms, string RU, string name, string regnum, string invnum)
        {
            ModelState.Clear();
            int page = 1;
            int pageSize = 100;
            if (i == "1") //подразделения
            {
                try
                {
                    AdminEquipmentFullCollection admFull;
                    if ((all_W != string.Empty) && (all_W != null) && (all_W != " ") ||
                   (all_G != string.Empty) && (all_G != null) && (all_G != " ") ||
                   (all_P != string.Empty) && (all_P != null) && (all_P != " ") ||
                   (all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " ") ||
                   (SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " ") ||
                   (Buildings != string.Empty) && (Buildings != null) && (Buildings != " ") ||
                   (Rooms != string.Empty) && (Rooms != null) && (Rooms != " ") ||
                   (RU != string.Empty) && (RU != null) && (RU != " ") ||
                   (name != string.Empty) && (name != null) && (name != " ")||
                   (invnum != string.Empty) && (invnum != null) && (invnum != " ") ||
                   (regnum != string.Empty) && (regnum != null) && (regnum != " "))
                    {
                        var Model = new AdminEquipmentFullCollection();
                        //if ((RU != string.Empty) && (RU != null) && (RU != " "))
                        //    Model.Equipment = NotFilteredModelRU();
                        //else
                            Model.Equipment = NotFilteredModel();
                        if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                            FilterBy_W(all_W, Model);
                        if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                            FilterBy_G(all_G, Model);
                        if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                            FilterBy_P(all_P, Model);
                        if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                            FilterBy_Mark(all_Marks, Model);
                        if ((SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " "))
                            FilterBySubDepts(SubDepts, Model);
                        if ((Buildings != string.Empty) && (Buildings != null) && (Buildings != " "))
                            FilterByBuildings(Buildings, Model);
                        if ((Rooms != string.Empty) && (Rooms != null) && (Rooms != " "))
                            FilterByRooms(Rooms, Model);
                        if ((name != string.Empty) && (name != null) && (name != " "))
                            FilterBy_Name(name, Model);
                        if ((invnum != string.Empty) && (invnum != null) && (invnum != " "))
                            FilterBy_invnum(invnum, Model);
                        if ((regnum != string.Empty) && (regnum != null) && (regnum != " "))
                            FilterBy_regnum(regnum, Model);
                        Model.PageInfo.PageNumber = page;
                        pageSize = Model.PageInfo.PageSize;
                        PagedEquip = Equipment.ToPagedList(page, pageSize);
                        IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
                        PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
                        special_option = false;
                        Model.nameL = name;
                        Model.InvNums = invnum;
                        Model.RegNums = regnum;
                        Model.RU.Clear();
                        Model.RU.Add(string.Empty);
                        AdminEquipmentFullCollection model = new AdminEquipmentFullCollection
                        {
                            PageInfo = pageInfo,
                            Equipment = Model.Equipment,
                            Equip = Equipments,
                            All_Type_name_items = Model.All_Type_name_items,
                            All_Group_name_items = Model.All_Group_name_items,
                            All_SubGroup_name_items = Model.All_SubGroup_name_items,
                            All_Marks_name_items = Model.All_Marks_name_items,
                            SubDepts = Model.SubDepts,
                            Buildings = Model.Buildings,
                            Rooms = Model.Rooms,
                            PagedEquipment = PagedEquip,
                            RU = Model.RU,
                            nameL = name,
                            RegNums=regnum,
                            InvNums=invnum
                        };
                        admFull = model;   
                    }
                    else
                    {
                        Equipment = NotFilteredModel();
                        IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
                        PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
                        special_option = false;
                        All_Type_name.Add(string.Empty);
                        All_Type_name.AddRange(Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                        All_Group_name.Add(string.Empty);
                        All_Group_name.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                        All_Group_name = SortList(All_Group_name.ToList());
                        All_SubGroup_name.Add(string.Empty);
                        All_SubGroup_name.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                        All_SubGroup_name = SortList(All_SubGroup_name.ToList());
                        All_Marks_name.Add(string.Empty);
                        All_Marks_name.AddRange(Equipment.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                        All_SubDepts.Add(string.Empty);
                        All_SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct().OrderBy(x => x));
                        All_SubDepts = SortList(All_SubDepts.ToList());
                        All_Buildings.Add(string.Empty);
                        All_Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct().OrderBy(x => x));
                        All_Rooms.Add(string.Empty);
                        All_Rooms.AddRange(Equipment.Select(x => x.Room).Distinct().OrderBy(x => x));
                        RY.Add(string.Empty);
                        RY.AddRange(Equipment.Select(x => x.RU).Distinct().OrderBy(x => x));
                        PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
                        AdminEquipmentFullCollection model = new AdminEquipmentFullCollection
                       {
                           PageInfo = pageInfo,
                           Equipment = Equipment,
                           Equip = Equipments,
                           All_Type_name_items = All_Type_name,
                           All_Group_name_items = All_Group_name,
                           All_SubGroup_name_items = All_SubGroup_name,
                           All_Marks_name_items = All_Marks_name,
                           SubDepts = All_SubDepts,
                           Buildings = All_Buildings,
                           Rooms = All_Rooms,
                           PagedEquipment = PagedEquip
                       };
                        admFull = model;
                        if (admFull.PagedEquipment.Count <= pageSize) admFull.PageInfo.PageSize = admFull.PagedEquipment.Count;
                       }
                    ViewBag.Param = 1;           
                    return PartialView("~/Views/Equipment/AdminEquipmentFullCollection.cshtml", admFull);
                }
                catch
                {
                    return PartialView();
                }
            }
            if (i == "2")//РУ
            {
                try
                {
                    AdminEquipmentFullCollection admFull;
                    //var admFull = new AdminEquipmentFullCollection();
                    if ((all_W != string.Empty) && (all_W != null) && (all_W != " ") ||
                  (all_G != string.Empty) && (all_G != null) && (all_G != " ") ||
                  (all_P != string.Empty) && (all_P != null) && (all_P != " ") ||
                  (all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " ") ||
                  (SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " ") ||
                  (Buildings != string.Empty) && (Buildings != null) && (Buildings != " ") ||
                  (Rooms != string.Empty) && (Rooms != null) && (Rooms != " ") ||
                  (RU != string.Empty) && (RU != null) && (RU != " ") ||
                  (name != string.Empty) && (name != null) && (name != " ")||
                   (invnum != string.Empty) && (invnum != null) && (invnum != " ") ||
                   (regnum != string.Empty) && (regnum != null) && (regnum != " "))
                    {
                        var Model = new AdminEquipmentFullCollection();
                        if ((RU != string.Empty) && (RU != null) && (RU != " "))
                            Model.Equipment = NotFilteredModelRU();
                        else
                            Model.Equipment = NotFilteredModelRU();
                        if ((name != string.Empty) && (name != null) && (name != " "))
                            FilterBy_Name(name, Model);
                        if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                            FilterBy_W(all_W, Model);
                        if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                            FilterBy_G(all_G, Model);
                        if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                            FilterBy_P(all_P, Model);
                        if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                            FilterBy_Mark(all_Marks, Model);
                        if ((SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " "))
                            FilterBySubDepts(SubDepts, Model);
                        if ((Buildings != string.Empty) && (Buildings != null) && (Buildings != " "))
                            FilterByBuildings(Buildings, Model);
                        if ((Rooms != string.Empty) && (Rooms != null) && (Rooms != " "))
                            FilterByRooms(Rooms, Model);
                        if ((RU != string.Empty) && (RU != null) && (RU != " "))
                            FilterByRU(RU, Model);
                        if ((invnum != string.Empty) && (invnum != null) && (invnum != " "))
                            FilterBy_invnum(invnum, Model);
                        if ((regnum != string.Empty) && (regnum != null) && (regnum != " "))
                            FilterBy_regnum(regnum, Model);
                        Model.PageInfo.PageNumber = page;
                        pageSize = Model.PageInfo.PageSize;
                        PagedEquip = Equipment.ToPagedList(page, pageSize);
                        IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
                        PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
                        special_option = false;
                        if (Model.RU[0] == null)
                        {
                            Model.RU.Clear();
                            Model.RU.Add(string.Empty);
                        }
                        AdminEquipmentFullCollection model = new AdminEquipmentFullCollection
                        {
                            PageInfo = pageInfo,
                            Equipment = Model.Equipment,
                            Equip = Equipments,
                            All_Type_name_items = Model.All_Type_name_items,
                            All_Group_name_items = Model.All_Group_name_items,
                            All_SubGroup_name_items = Model.All_SubGroup_name_items,
                            All_Marks_name_items = Model.All_Marks_name_items,
                            SubDepts = Model.SubDepts,
                            Buildings = Model.Buildings,
                            Rooms = Model.Rooms,
                            PagedEquipment = PagedEquip,
                            RU = Model.RU,
                            nameL=name,
                            RegNums=regnum,
                            InvNums=invnum
                        };
                        admFull = model;
                    }
                    else
                    {
                        Equipment = NotFilteredModelRU();
                        IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
                        PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
                        special_option = false;
                        All_Type_name.Add(string.Empty);
                        All_Type_name.AddRange(Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                        All_Group_name.Add(string.Empty);
                        All_Group_name.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                        All_Group_name = SortList(All_Group_name.ToList());
                        All_SubGroup_name.Add(string.Empty);
                        All_SubGroup_name.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                        All_SubGroup_name = SortList(All_SubGroup_name.ToList());
                        All_Marks_name.Add(string.Empty);
                        All_Marks_name.AddRange(Equipment.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                        All_SubDepts.Add(string.Empty);
                        All_SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct().OrderBy(x => x));
                        All_SubDepts = SortList(All_SubDepts.ToList());
                        All_Buildings.Add(string.Empty);
                        All_Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct().OrderBy(x => x));
                        All_Rooms.Add(string.Empty);
                        All_Rooms.AddRange(Equipment.Select(x => x.Room).Distinct().OrderBy(x => x));
                        RY.Add(string.Empty);
                        RY.AddRange(Equipment.Select(x => x.RU).Distinct().OrderBy(x => x));
                        PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
                        AdminEquipmentFullCollection model = new AdminEquipmentFullCollection
                        {
                            PageInfo = pageInfo,
                            Equipment = Equipment,
                            Equip = Equipments,
                            All_Type_name_items = All_Type_name,
                            All_Group_name_items = All_Group_name,
                            All_SubGroup_name_items = All_SubGroup_name,
                            All_Marks_name_items = All_Marks_name,
                            SubDepts = All_SubDepts,
                            Buildings = All_Buildings,
                            Rooms = All_Rooms,
                            PagedEquipment = PagedEquip,
                            RU = RY,
                            nameL=name,
                            RegNums = regnum,
                            InvNums = invnum
                        };
                        admFull = model;
                        if (admFull.PagedEquipment.Count <= pageSize) admFull.PageInfo.PageSize = admFull.PagedEquipment.Count;
                    }
                    ViewBag.Param = 2;
                    return PartialView("~/Views/Equipment/AdminEquipmentFullCollection.cshtml", admFull);
                }
                catch
                {
                    return PartialView();
                }
            }
            else
                return PartialView();
        }
        public ActionResult GetAdminEquipmentFullList(string i, int paged, string all_W, string all_G, string all_P, string all_Marks, string SubDepts, string Buildings, string Rooms, string RU, string name, string regnum, string invnum)
        {
            var Model = new AdminEquipmentFullCollection();
            if ((all_W != string.Empty) && (all_W != null) && (all_W != " ") ||
                (all_G != string.Empty) && (all_G != null) && (all_G != " ") ||
                (all_P != string.Empty) && (all_P != null) && (all_P != " ") ||
                (all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " ") ||
                (SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " ") ||
                (Buildings != string.Empty) && (Buildings != null) && (Buildings != " ") ||
                (Rooms != string.Empty) && (Rooms != null) && (Rooms != " ") ||
                (RU != string.Empty) && (RU != null) && (RU != " ") ||
                (name != string.Empty) && (name != null) && (name != " ")||
                  (invnum != string.Empty) && (invnum != null) && (invnum != " ") ||
                (regnum != string.Empty) && (regnum != null) && (regnum != " "))
            {
                if (i == "2")
                {
                    if ((RU != string.Empty) && (RU != null) && (RU != " "))
                    {
                        Model.Equipment = NotFilteredModelRU();
                        FilterByRU(RU, Model);
                    }
                    else
                        Model.Equipment = NotFilteredModelRU();
                    if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                        FilterBy_W(all_W, Model);
                    if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                        FilterBy_G(all_G, Model);
                    if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                        FilterBy_P(all_P, Model);
                    if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                        FilterBy_Mark(all_Marks, Model);
                    if ((RU != string.Empty) && (RU != null) && (RU != " "))
                        FilterByRU(RU, Model);
                    if ((name != string.Empty) && (name != null) && (name != " "))
                        FilterBy_Name(name, Model);
                    if ((regnum != string.Empty) && (regnum != null) && (regnum != " "))
                        FilterBy_regnum(regnum, Model);
                    if ((invnum != string.Empty) && (invnum != null) && (invnum != " "))
                        FilterBy_invnum(invnum, Model);
                }
                if (i == "1")
                {
                    Model.Equipment = NotFilteredModel();
                    if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                        FilterBy_W(all_W, Model);
                    if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                        FilterBy_G(all_G, Model);
                    if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                        FilterBy_P(all_P, Model);
                    if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                        FilterBy_Mark(all_Marks, Model);
                    if ((SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " "))
                        FilterBySubDepts(SubDepts, Model);
                    if ((Buildings != string.Empty) && (Buildings != null) && (Buildings != " "))
                        FilterByBuildings(Buildings, Model);
                    if ((Rooms != string.Empty) && (Rooms != null) && (Rooms != " "))
                        FilterByRooms(Rooms, Model);
                    if ((name != string.Empty) && (name != null) && (name != " "))
                        FilterBy_Name(name, Model);
                    if ((invnum != string.Empty) && (invnum != null) && (invnum != " "))
                        FilterBy_invnum(invnum, Model);
                    if ((regnum != string.Empty) && (regnum != null) && (regnum != " "))
                        FilterBy_regnum(regnum, Model);
                    if ((Model.RU.Count == 1) && (Model.RU[0]==null)) Model.RU[0] = string.Empty;
                }
                if (Model.PageInfo == null) Model.PageInfo = new PageInfo { PageNumber = paged, PageSize = 100, TotalItems = Model.Equipment.Count };
                PagedEquip = Model.Equipment.ToPagedList(paged, Model.PageInfo.PageSize);
            }
            //FilterBy_W(all_W, Model);
            //FilterBy_G(all_G, Model);
            else
            {
                if (i == "1")
                {
                    Model.Equipment = NotFilteredModel();
                    if (Model.PageInfo == null) Model.PageInfo = new PageInfo { PageNumber = paged, PageSize = 100, TotalItems = Model.Equipment.Count };
                    Model.All_Type_name_items.Add(string.Empty);
                    Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                    Model.All_Group_name_items.Add(string.Empty);
                    Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                    Model.All_SubGroup_name_items.Add(string.Empty);
                    Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                    Model.All_Marks_name_items.Add(string.Empty);
                    Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                    Model.SubDepts.Add(string.Empty);
                    Model.SubDepts.AddRange(Model.Equipment.Select(x => x.SubDeptName).Distinct().OrderBy(x => x));
                    Model.Buildings.Add(string.Empty);
                    Model.Buildings.AddRange(Model.Equipment.Select(x => x.BuildingName).Distinct().OrderBy(x => x));
                    Model.Rooms.Add(string.Empty);
                    Model.Rooms.AddRange(Model.Equipment.Select(x => x.Room).Distinct().OrderBy(x => x));
                    PagedEquip = Equipment.ToPagedList(paged, Model.PageInfo.PageSize);
                }
                else
                {
                    Model.Equipment = NotFilteredModelRU();
                    if (Model.PageInfo == null) Model.PageInfo = new PageInfo { PageNumber = paged, PageSize = 100, TotalItems = Model.Equipment.Count };
                    Model.All_Type_name_items.Add(string.Empty);
                    Model.All_Type_name_items.AddRange(Model.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                    Model.All_Group_name_items.Add(string.Empty);
                    Model.All_Group_name_items.AddRange(Model.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                    Model.All_SubGroup_name_items.Add(string.Empty);
                    Model.All_SubGroup_name_items.AddRange(Model.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                    Model.All_Marks_name_items.Add(string.Empty);
                    Model.All_Marks_name_items.AddRange(Model.Equipment.Select(x => x.Mark).Distinct().OrderBy(x => x)); //марки
                    Model.SubDepts.Add(string.Empty);
                    Model.SubDepts.AddRange(Model.Equipment.Select(x => x.SubDeptName).Distinct().OrderBy(x => x));
                    Model.Buildings.Add(string.Empty);
                    Model.Buildings.AddRange(Model.Equipment.Select(x => x.BuildingName).Distinct().OrderBy(x => x));
                    Model.Rooms.Add(string.Empty);
                    Model.Rooms.AddRange(Model.Equipment.Select(x => x.Room).Distinct().OrderBy(x => x));
                    Model.RU.Add(string.Empty);
                    Model.RU.AddRange(Model.Equipment.Select(x => x.RU).Distinct().OrderBy(x => x));
                    PagedEquip = Equipment.ToPagedList(paged, Model.PageInfo.PageSize);
                }

            }
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((paged - 1) * Model.PageInfo.PageSize).Take(Model.PageInfo.PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = paged, PageSize = Model.PageInfo.PageSize, TotalItems = Equipment.Count };

            AdminEquipmentFullCollection ivm = new AdminEquipmentFullCollection
            {
                PageInfo = pageInfo,
                Equipment = Equipment,
                Equip = Equipments,
                All_Type_name_items = Model.All_Type_name_items,
                All_Group_name_items = Model.All_Group_name_items,
                All_SubGroup_name_items = Model.All_SubGroup_name_items,
                All_Marks_name_items = Model.All_Marks_name_items,
                SubDepts = Model.SubDepts,
                Buildings = Model.Buildings,
                Rooms = Model.Rooms,
                RU=Model.RU,
                PagedEquipment = PagedEquip,
                nameL=name,
                RegNums=regnum,
                InvNums=invnum
            };       
            ViewBag.Param = Convert.ToInt32(i);
            if (ivm.PagedEquipment.Count <= ivm.PageInfo.PageSize) ivm.PageInfo.PageSize = ivm.PagedEquipment.Count;
            return PartialView("~/Views/Equipment/AdminEquipmentFullCollection.cshtml", ivm);
        }
        public void FilterBy_W(string W, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.TypeName != W);
            Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_G(string G, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.GroupName != G);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_Group_name_items.Add(G);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            Model.RU.RemoveRange(0, Model.RU.Count);

            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }

            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_P(string P, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.SubGroupName != P);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_SubGroup_name_items.Add(P);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            Model.RU.RemoveRange(0, Model.RU.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count < pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_Mark(string Mark, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.Mark != Mark);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.All_Marks_name_items.Add(Mark);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            Model.RU.RemoveRange(0, Model.RU.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBySubDepts(string SubDept, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.SubDeptName != SubDept);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.SubDepts.Add(SubDept);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            Model.RU.RemoveRange(0, Model.RU.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count < pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterByBuildings(string BuildName, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.BuildingName != BuildName);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Buildings.Add(BuildName);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            Model.RU.RemoveRange(0, Model.RU.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterByRooms(string RoomName, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.Room != RoomName);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            Model.Rooms.Add(RoomName);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.RU.RemoveRange(0, Model.RU.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterByRU(string RU, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => x.RU != RU);
            Model.RU.RemoveRange(0, Model.RU.Count);
            Model.RU.Add(RU);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }   
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_regnum(string RegNo, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Model.Equipment = NotFilteredModel();
            Equipment.RemoveAll(x => x.RegNumber != RegNo);
            Model.RU.RemoveRange(0, Model.RU.Count);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            Model.RegNums = RegNo;
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_invnum(string invnum, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Model.Equipment = NotFilteredModel();
            Equipment.RemoveAll(x => x.InvNumber != invnum);
            Model.RU.RemoveRange(0, Model.RU.Count);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            Model.InvNums = invnum;
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_Name(string name, AdminEquipmentFullCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Equipment = Model.Equipment;
            Model.All_Type_name_items.Clear();
            Model.All_Group_name_items.Clear();
            Model.All_SubGroup_name_items.Clear();
            Model.All_Marks_name_items.Clear();
            Model.RU.Clear();
            Equipment.RemoveAll(x => !x.Name.Contains(name));
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            Model.RU.RemoveRange(0, Model.RU.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            var ru = Equipment.Select(x => x.RU).Distinct().ToList();
            if (ru.Count > 1)
            {
                Model.RU.Add(string.Empty);
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            else
            {
                Model.RU.AddRange(Equipment.Select(x => x.RU).Distinct());
            }
            Model.nameL = name;
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterByRegNo(string RegNo, AdminEquipmentCollection Model)
        {
            var page = 1;
            var pageSize = 100;
            Model.Equipment = NotFilteredModel();
            Equipment.RemoveAll(x => x.RegNumber != RegNo);
            Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
            Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
            Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
            Model.All_Marks_name_items.RemoveRange(0, Model.All_Marks_name_items.Count);
            Model.SubDepts.RemoveRange(0, Model.SubDepts.Count);
            Model.Buildings.RemoveRange(0, Model.Buildings.Count);
            Model.Rooms.RemoveRange(0, Model.Rooms.Count);
            var t = Equipment.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.All_Type_name_items.Add(string.Empty);
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            else
            {
                Model.All_Type_name_items.AddRange(Equipment.Select(x => x.TypeName).Distinct());
            }
            var g = Equipment.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.All_Group_name_items.Add(string.Empty);
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                Model.All_Group_name_items = SortList(Model.All_Group_name_items.ToList());
            }
            else
            {
                Model.All_Group_name_items.AddRange(Equipment.Select(x => x.GroupName).Distinct());
            }
            var s = Equipment.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.All_SubGroup_name_items.Add(string.Empty);
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                Model.All_SubGroup_name_items = SortList(Model.All_SubGroup_name_items.ToList());
            }
            else
            {
                Model.All_SubGroup_name_items.AddRange(Equipment.Select(x => x.SubGroupName).Distinct());
            }
            var m = Equipment.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.All_Marks_name_items.Add(string.Empty);
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.All_Marks_name_items.AddRange(Equipment.Select(x => x.Mark).Distinct());
            }
            var sd = Equipment.Select(x => x.SubDeptName).Distinct().ToList();
            if (sd.Count > 1)
            {
                Model.SubDepts.Add(string.Empty);
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
                Model.SubDepts = SortList(Model.SubDepts.ToList());
            }
            else
            {
                Model.SubDepts.AddRange(Equipment.Select(x => x.SubDeptName).Distinct());
            }
            var b = Equipment.Select(x => x.BuildingName).Distinct().ToList();
            if (b.Count > 1)
            {
                Model.Buildings.Add(string.Empty);
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            else
            {
                Model.Buildings.AddRange(Equipment.Select(x => x.BuildingName).Distinct());
            }
            var r = Equipment.Select(x => x.Room).Distinct().ToList();
            if (r.Count > 1)
            {
                Model.Rooms.Add(string.Empty);
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            else
            {
                Model.Rooms.AddRange(Equipment.Select(x => x.Room).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Equipment.Count };
            Model.PageInfo = pageInfo;
            IEnumerable<EquipmentElement> Equipments = Equipment.Skip((page - 1) * pageSize).Take(pageSize);
            Model.Equip = Equipments;
            PagedEquip = Equipment.ToPagedList(page, pageInfo.PageSize);
            Model.PagedEquipment = PagedEquip;
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        [HttpPost]
        public ActionResult SearchEquipment(string EquipmentID)
        {
            var cur_user = WebMatrix.WebData.WebSecurity.CurrentUserName;
            var role = System.Web.Security.Roles.GetRolesForUser(cur_user).Single();
            if ((role == "Администратор") || (role == "Разработчик"))
            {
                var aec = new AdminEquipmentCollection();
                FilterByRegNo(EquipmentID,aec);
                return PartialView("~/Views/Equipment/AdminEquipmentList.cshtml", aec);
            }
            if (role == "Главный администратор")
            {
                var aec = new AdminEquipmentFullCollection();
                FilterBy_regnum(EquipmentID,aec);
                return PartialView("~/Views/Equipment/AdminEquipmentFullCollection.cshtml", aec);
            }
            var ec = new AdminEquipmentCollection();
            FilterByRegNo(EquipmentID,ec);
            return PartialView("~/Views/Equipment/AdminEquipmentList.cshtml", ec);
        }
        [HttpPost]
        public ActionResult sprEdit(int page, string all_W, string all_G, string all_P, string all_Marks, string Model)
        {
            var see = new sprEquipmentEditor();
            if ((all_W != string.Empty) && (all_W != null) && (all_W != " ") ||
               (all_G != string.Empty) && (all_G != null) && (all_G != " ") ||
               (all_P != string.Empty) && (all_P != null) && (all_P != " ") ||
               (all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " ") ||
               (Model != string.Empty) && (Model != null) && (Model != " "))
            {
                //перелистывание с фильтром
                if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                    FilterBy_W(all_W, see);
                if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                    FilterBy_G(all_G, see);
                if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                    FilterBy_P(all_P, see);
                if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                    FilterBy_Mark(all_Marks, see);
                if ((Model != string.Empty) && (Model != null) && (Model != " "))
                    FilterBy_Model(Model, see);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = see.PageInfo.PageSize, TotalItems = see.EquipmentDeskBook.Count };
                see.PageInfo = pageInfo;
                see.PagedEquipment = see.PagedSelection(see.EquipmentDeskBook, page, pageInfo.PageSize);     
            }
            else
            {
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = see.PageInfo.PageSize, TotalItems = see.EquipmentDeskBook.Count };
                see.Type_items.Clear();
                see.Group_items.Clear();
                see.SubGroup_items.Clear();
                see.Marks_items.Clear();
                see.Model_items.Clear();
                see.Type_items.Add(string.Empty);
                see.Type_items.AddRange(see.EquipmentDeskBook.Select(x => x.TypeName).Distinct().OrderBy(x => x));
                see.Group_items.Add(string.Empty);
                see.Group_items.AddRange(see.EquipmentDeskBook.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                see.SubGroup_items.Add(string.Empty);
                see.SubGroup_items.AddRange(see.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                see.Marks_items.Add(string.Empty);
                see.Marks_items.AddRange(see.EquipmentDeskBook.Select(x => x.Mark).Distinct().OrderBy(x => x));
                see.Model_items.Add(string.Empty);
                see.Model_items.AddRange(see.EquipmentDeskBook.Select(x => x.Model).Distinct().OrderBy(x => x));
                see.Model_items = SortList(see.Model_items.ToList());    
                see.PagedEquipment = see.PagedSelection(see.EquipmentDeskBook, page, 100);
                see.PageInfo = pageInfo;
                if (see.PagedEquipment.Count <= see.PageInfo.PageSize) see.PageInfo.PageSize = see.PagedEquipment.Count;
            }
            return PartialView("~/Views/Lists/SprEquipment.cshtml", see);
        }
        public ActionResult sprEditFilter(string all_W, string all_G, string all_P, string all_Marks, string all_Model)
        {
            var Model = new sprEquipmentEditor();
            if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                FilterBy_W(all_W, Model);
            if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                FilterBy_G(all_G, Model);
            if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                FilterBy_P(all_P, Model);
            if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                FilterBy_Mark(all_Marks, Model);
            if ((all_Model != string.Empty) && (all_Model != null) && (all_Model != " "))
                FilterBy_Model(all_Model, Model);
            return PartialView("~/Views/Lists/SprEquipment.cshtml", Model);
        }
        public void FilterBy_W(string W, sprEquipmentEditor Model)
        {
            var page = 1;
            var pageSize = 100;
            Model.Type_items.Clear();
            Model.Group_items.Clear();
            Model.SubGroup_items.Clear();
            Model.Marks_items.Clear();
            Model.Model_items.Clear();
            Model.EquipmentDeskBook.RemoveAll(x => x.TypeName != W);
            Model.Type_items.AddRange(Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            var g = Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.Group_items.Add(string.Empty);
                Model.Group_items.AddRange(Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                //Model.Group_items = SortList(Model.Group_items.ToList());
            }
            else
            {
                Model.Group_items.AddRange(Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct());
            }
            var s = Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.SubGroup_items.Add(string.Empty);
                Model.SubGroup_items.AddRange(Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                //Model.SubGroup_items = SortList(Model.SubGroup_items.ToList());
            }
            else
            {
                Model.SubGroup_items.AddRange(Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct());
            }
            var m = Model.EquipmentDeskBook.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.Marks_items.Add(string.Empty);
                Model.Marks_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.Marks_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Mark).Distinct());
            }
            var md = Model.EquipmentDeskBook.Select(x => x.Model).Distinct().ToList();
            if (md.Count > 1)
            {
                Model.Model_items.Add(string.Empty);
                Model.Model_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Model).Distinct());
                Model.Model_items = SortList(Model.Model_items.ToList());
            }
            else
            {
                if ((md.Count() == 1) && (md[0] == null))
                { md[0] = ""; Model.Model_items.Add(string.Empty); }
                else
                    Model.Model_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Model).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Model.EquipmentDeskBook.Count };
            Model.PageInfo = pageInfo;
            Model.PagedEquipment = Model.PagedSelection(Model.EquipmentDeskBook, page, 100);
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_G(string G, sprEquipmentEditor Model)
        {
            var page = 1;
            var pageSize = 100;
            Model.Type_items.Clear();
            Model.Group_items.Clear();
            Model.SubGroup_items.Clear();
            Model.Marks_items.Clear();
            Model.Model_items.Clear();
            Model.EquipmentDeskBook.RemoveAll(x => x.GroupName != G);
            Model.Group_items.AddRange(Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            var t = Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.Type_items.Add(string.Empty);
                Model.Type_items.AddRange(Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct().OrderBy(x => x));
                //Model.Group_items = SortList(Model.Group_items.ToList());
            }
            else
            {
                Model.Type_items.AddRange(Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct());
            }
            var s = Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.SubGroup_items.Add(string.Empty);
                Model.SubGroup_items.AddRange(Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                //Model.SubGroup_items = SortList(Model.SubGroup_items.ToList());
            }
            else
            {
                Model.SubGroup_items.AddRange(Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct());
            }
            var m = Model.EquipmentDeskBook.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.Marks_items.Add(string.Empty);
                Model.Marks_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.Marks_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Mark).Distinct());
            }
            var md = Model.EquipmentDeskBook.Select(x => x.Model).Distinct().ToList();
            if (md.Count > 1)
            {
                Model.Model_items.Add(string.Empty);
                Model.Model_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Model).Distinct());
                Model.Model_items = SortList(Model.Model_items.ToList());
            }
            else
            {
                if ((md.Count() == 1) && (md[0] == null))
                { md[0] = ""; Model.Model_items.Add(string.Empty); }
                else
                    Model.Model_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Model).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Model.EquipmentDeskBook.Count };
            Model.PageInfo = pageInfo;
            Model.PagedEquipment = Model.PagedSelection(Model.EquipmentDeskBook, page, 100);
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_P(string P, sprEquipmentEditor Model)
        {
            var page = 1;
            var pageSize = 100;
            Model.Type_items.Clear();
            Model.Group_items.Clear();
            Model.SubGroup_items.Clear();
            Model.Marks_items.Clear();
            Model.Model_items.Clear();
            Model.EquipmentDeskBook.RemoveAll(x => x.SubGroupName != P);
            Model.SubGroup_items.AddRange(Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
            var t = Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.Type_items.Add(string.Empty);
                Model.Type_items.AddRange(Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct().OrderBy(x => x));
                //Model.Group_items = SortList(Model.Group_items.ToList());
            }
            else
            {
                Model.Type_items.AddRange(Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct());
            }
            var g = Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.Group_items.Add(string.Empty);
                Model.Group_items.AddRange(Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                //Model.Group_items = SortList(Model.Group_items.ToList());
            }
            else
            {
                Model.Group_items.AddRange(Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct());
            }
            var m = Model.EquipmentDeskBook.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.Marks_items.Add(string.Empty);
                Model.Marks_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Mark).Distinct());
            }
            else
            {
                Model.Marks_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Mark).Distinct());
            }
            var md = Model.EquipmentDeskBook.Select(x => x.Model).Distinct().ToList();
            if (md.Count > 1)
            {
                Model.Model_items.Add(string.Empty);
                Model.Model_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Model).Distinct());
                Model.Model_items = SortList(Model.Model_items.ToList());
            }
            else
            {
                if ((md.Count() == 1) && (md[0] == null))
                { md[0] = ""; Model.Model_items.Add(string.Empty); }
                else
                    Model.Model_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Model).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Model.EquipmentDeskBook.Count };
            Model.PageInfo = pageInfo;
            Model.PagedEquipment = Model.PagedSelection(Model.EquipmentDeskBook, page, 100);
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_Mark(string Mark, sprEquipmentEditor Model)
        {
            var page = 1;
            var pageSize = 100;
            Model.Type_items.Clear();
            Model.Group_items.Clear();
            Model.SubGroup_items.Clear();
            Model.Marks_items.Clear();
            Model.Model_items.Clear();
            Model.EquipmentDeskBook.RemoveAll(x => x.Mark != Mark);
            Model.Marks_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Mark).Distinct().OrderBy(x => x));
            var t = Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.Type_items.Add(string.Empty);
                Model.Type_items.AddRange(Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.Type_items.AddRange(Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct());
            }
            var g = Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.Group_items.Add(string.Empty);
                Model.Group_items.AddRange(Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.Group_items.AddRange(Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct());
            }
            var s = Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.SubGroup_items.Add(string.Empty);
                Model.SubGroup_items.AddRange(Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                //Model.SubGroup_items = SortList(Model.SubGroup_items.ToList());
            }
            else
            {
                Model.SubGroup_items.AddRange(Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct());
            }
            
            var md = Model.EquipmentDeskBook.Select(x => x.Model).Distinct().ToList();
            if (md.Count > 1)
            {
                Model.Model_items.Add(string.Empty);
                Model.Model_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Model).Distinct());
                Model.Model_items = SortList(Model.Model_items.ToList());
            }
            else
            {
                if ((md.Count() == 1) && (md[0] == null))
                { md[0] = ""; Model.Model_items.Add(string.Empty); }
                else
                    Model.Model_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Model).Distinct());
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Model.EquipmentDeskBook.Count };
            Model.PageInfo = pageInfo;
            Model.PagedEquipment = Model.PagedSelection(Model.EquipmentDeskBook, page, 100);
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }
        public void FilterBy_Model(string Md, sprEquipmentEditor Model)
        {
            var page = 1;
            var pageSize = 100;
            Model.Type_items.Clear();
            Model.Group_items.Clear();
            Model.SubGroup_items.Clear();
            Model.Marks_items.Clear();
            Model.Model_items.Clear();
            Model.EquipmentDeskBook.RemoveAll(x => x.Model != Md);
            Model.Model_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Model).Distinct().OrderBy(x => x));
            var t = Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct().ToList();
            if (t.Count > 1)
            {
                Model.Type_items.Add(string.Empty);
                Model.Type_items.AddRange(Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.Type_items.AddRange(Model.EquipmentDeskBook.Select(x => x.TypeName).Distinct());
            }
            var g = Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct().ToList();
            if (g.Count > 1)
            {
                Model.Group_items.Add(string.Empty);
                Model.Group_items.AddRange(Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            }
            else
            {
                Model.Group_items.AddRange(Model.EquipmentDeskBook.Select(x => x.GroupName).Distinct());
            }
            var s = Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().ToList();
            if (s.Count > 1)
            {
                Model.SubGroup_items.Add(string.Empty);
                Model.SubGroup_items.AddRange(Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                //Model.SubGroup_items = SortList(Model.SubGroup_items.ToList());
            }
            else
            {
                Model.SubGroup_items.AddRange(Model.EquipmentDeskBook.Select(x => x.SubGroupName).Distinct());
            }
            var m = Model.EquipmentDeskBook.Select(x => x.Mark).Distinct().ToList();
            if (m.Count > 1)
            {
                Model.Marks_items.Add(string.Empty);
                Model.Marks_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Mark).Distinct());
            }
            else
            {
                if ((m.Count() == 1) && (m[0] == null))
                { m[0] = ""; Model.Model_items.Add(string.Empty); }
                else
                    Model.Model_items.AddRange(Model.EquipmentDeskBook.Select(x => x.Model).Distinct());
            }         
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = Model.EquipmentDeskBook.Count };
            Model.PageInfo = pageInfo;
            Model.PagedEquipment = Model.PagedSelection(Model.EquipmentDeskBook, page, 100);
            if (Model.PagedEquipment.Count <= pageSize) Model.PageInfo.PageSize = Model.PagedEquipment.Count;
        }

        [HttpGet]
        public ActionResult Find(string i, string all_W, string all_G, string all_P, string all_Marks, string SubDepts, string Buildings, string Rooms, string RU, string term, string RegNums, string InvNums)
        {
            var list = new List<EquipmentElement>();
            //if ((term != string.Empty) && (term != null) && (term != " "))
            //{
            //AdminEquipmentFullCollection
            //-------------------------------------------------------------------------------
            if (i == "1")
            {
                list = NotFilteredModel();
                var Model = new AdminEquipmentFullCollection();
                Model.Equipment = list;
                if ((all_W != string.Empty) && (all_W != null) && (all_W != " ") ||
            (all_G != string.Empty) && (all_G != null) && (all_G != " ") ||
            (all_P != string.Empty) && (all_P != null) && (all_P != " ") ||
            (all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " ") ||
            (SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " ") ||
            (Buildings != string.Empty) && (Buildings != null) && (Buildings != " ") ||
            (Rooms != string.Empty) && (Rooms != null) && (Rooms != " ") ||
            (RU != string.Empty) && (RU != null) && (RU != " ") && (RU != "undefined") ||
            (term != string.Empty) && (term != null) && (term != " ")||
            (RegNums != string.Empty) && (RegNums != null) && (RegNums != " ")||
            (InvNums != string.Empty) && (InvNums != null) && (InvNums != " "))
                {
                    if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                        FilterBy_W(all_W, Model);
                    if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                        FilterBy_G(all_G, Model);
                    if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                        FilterBy_P(all_P, Model);
                    if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                        FilterBy_Mark(all_Marks, Model);
                    if ((SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " "))
                        FilterBySubDepts(SubDepts, Model);
                    if ((Buildings != string.Empty) && (Buildings != null) && (Buildings != " "))
                        FilterByBuildings(Buildings, Model);
                    if ((Rooms != string.Empty) && (Rooms != null) && (Rooms != " "))
                        FilterByRooms(Rooms, Model);
                    if ((term != string.Empty) && (term != null) && (term != " "))
                        FilterBy_Name(term, Model);
                    if ((RegNums != string.Empty) && (RegNums != null) && (RegNums != " "))
                        FilterBy_regnum(RegNums, Model);
                    if ((InvNums != string.Empty) && (InvNums != null) && (InvNums != " "))
                        FilterBy_invnum(InvNums, Model);
                    list = Model.Equipment;
                }      
            }
            if (i == "2")
            {
                list = NotFilteredModelRU();
                if ((all_W != string.Empty) && (all_W != null) && (all_W != " ") ||
              (all_G != string.Empty) && (all_G != null) && (all_G != " ") ||
              (all_P != string.Empty) && (all_P != null) && (all_P != " ") ||
              (all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " ") ||
              (SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " ") ||
              (Buildings != string.Empty) && (Buildings != null) && (Buildings != " ") ||
              (Rooms != string.Empty) && (Rooms != null) && (Rooms != " ") ||
              (RU != string.Empty) && (RU != null) && (RU != " ")||
              (RegNums != string.Empty) && (RegNums != null) && (RegNums != " ") ||
              (InvNums != string.Empty) && (InvNums != null) && (InvNums != " "))
                {
                    var Modell = new AdminEquipmentFullCollection();
                    if ((RU != string.Empty) && (RU != null) && (RU != " "))
                        Modell.Equipment = list;
                    if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                        FilterBy_W(all_W, Modell);
                    if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                        FilterBy_G(all_G, Modell);
                    if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                        FilterBy_P(all_P, Modell);
                    if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                        FilterBy_Mark(all_Marks, Modell);
                    if ((SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " "))
                        FilterBySubDepts(SubDepts, Modell);
                    if ((Buildings != string.Empty) && (Buildings != null) && (Buildings != " "))
                        FilterByBuildings(Buildings, Modell);
                    if ((Rooms != string.Empty) && (Rooms != null) && (Rooms != " "))
                        FilterByRooms(Rooms, Modell);
                    if ((RU != string.Empty) && (RU != null) && (RU != " "))
                        FilterByRU(RU, Modell);
                    if ((term != string.Empty) && (term != null) && (term != " "))
                        FilterBy_Name(term, Modell);
                    if ((RegNums != string.Empty) && (RegNums != null) && (RegNums != " "))
                        FilterBy_regnum(RegNums, Modell);
                    if ((InvNums != string.Empty) && (InvNums != null) && (InvNums != " "))
                        FilterBy_invnum(InvNums, Modell);
                    list = Modell.Equipment;
                }
            }
            //-------------------------------------------------------------------------------
            //AdminEquipmentList
            //-----------------------------------------------------------------------------
            if (i == "AEList")
            {
                var Model = new AdminEquipmentCollection();
                Model.Equipment = NotFilteredModel();
                UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
                if (uif.UserRole == "Механик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "М") || (x.TypeCode == "А")).ToList(); }
                if (uif.UserRole == "Энергетик") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") || (x.TypeCode == "А")).ToList(); }
                if (uif.UserRole == "Пользователь на РУ") { Model.Equipment = NotFilteredModelRU(); special_option = true; }
                if (uif.UserRole == "ЦСР") { Model.Equipment = Model.Equipment.Where(x => (x.TypeCode == "Э") && (x.TypeCode == "А")).ToList(); }
                if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                    FilterBy_W(all_W, Model);
                if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                    FilterBy_G(all_G, Model);
                if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                    FilterBy_P(all_P, Model);
                if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                    FilterBy_Mark(all_Marks, Model);
                if ((SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " "))
                    FilterBySubDepts(SubDepts, Model);
                if ((Buildings != string.Empty) && (Buildings != null) && (Buildings != " "))
                    FilterByBuildings(Buildings, Model);
                if ((Rooms != string.Empty) && (Rooms != null) && (Rooms != " "))
                    FilterByRooms(Rooms, Model);
                if ((term != string.Empty) && (term != null) && (term != " "))
                    FilterBy_Name(term, Model);
                if ((RegNums != string.Empty) && (RegNums != null) && (RegNums != " "))
                    FilterBy_RegNums(RegNums, Model);
                if ((InvNums != string.Empty) && (InvNums != null) && (InvNums != " "))
                    FilterBy_InvNums(InvNums, Model);
                list = Model.Equipment;
            }
            if (i == "RUList")
            {
                var Model = new AdminEquipmentRU();
                if ((all_W != string.Empty) && (all_W != null) && (all_W != " ") ||
                    (all_G != string.Empty) && (all_G != null) && (all_G != " ") ||
                    (all_P != string.Empty) && (all_P != null) && (all_P != " ") ||
                    (all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " ") ||
                    (RU != string.Empty) && (RU != null) && (RU != " "))
                {
                    Model.Equipment = NotFilteredModelRU();
                    if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                        FilterBy_W(all_W, Model);
                    if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                        FilterBy_G(all_G, Model);
                    if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                        FilterBy_P(all_P, Model);
                    if ((all_Marks != string.Empty) && (all_Marks != null) && (all_Marks != " "))
                        FilterBy_Mark(all_Marks, Model);
                    if ((RU != string.Empty) && (RU != null) && (RU != " "))
                        FilterByRU(RU, Model);
                    if ((term != string.Empty) && (term != null) && (term != " "))
                        FilterBy_Name(term, Model);
                    if ((RegNums != string.Empty) && (RegNums != null) && (RegNums != " "))
                        FilterBy_RegNums(RegNums, Model);
                    if ((InvNums != string.Empty) && (InvNums != null) && (InvNums != " "))
                        FilterBy_InvNums(InvNums, Model);
                }
                list = Model.Equipment;
            }

            var result = list.Select(x => x.Name).Where(x => x.Contains(term)).Distinct().OrderBy(x => x).ToList();
            return Json(result,
              JsonRequestBehavior.AllowGet);
        }


        public int GetIDSubDept(string Code_SubDept)
        {
            if ((Code_SubDept != "ОБЩИЙ СПИСОК")&&(Code_SubDept != null))
            {
                using (PPREntities ppre = new PPREntities())
                {
                    int code = Convert.ToInt32(Code_SubDept.Split(' ')[0]);
                    var sub = ppre.sprSubDept.Where(x => x.SubDeptCode == code).ToList();
                    if (sub.Count == 1)
                    {
                        return sub[0].IdSubDept;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else return 0;
        }

        //[HttpPost]
        //public ActionResult GetAction(string actions, string SubDepts_sender, string SubDepts_recipient, string type) //action-кнопка, type-radiobuttons
        //{
        //    if (actions == "FullMovenent")
        //    {
        //        //return RedirectToAction("FullMovenent", new { SubDepts_sender = SubDepts_sender, SubDepts_recipient = SubDepts_recipient, type = type });
        //    }
        //    else if (actions == "partial_moved")
        //    {

        //        //   return RedirectToAction("GetMoveList", new { SubDepts_sender = SubDepts_sender, SubDepts_recipient = SubDepts_recipient, type = type });
        //        return PartialView();
        //    }
        //    //else if (actions == "Repair_history")
        //    //{
        //    //    return GetRepair_history(EquipmentID);
        //    //}
        //    return PartialView();
        //}

         [HttpPost]
        public ActionResult FullMovenent(string SubDepts_sender, string SubDepts_recipient, string type)  //type-radiobuttons
        {
            if (type == "full_moved")
            {
                using (PPREntities ppre = new PPREntities())
                {
                    var nalich = ppre.NALICH.ToList();
                    int idSubdept_sender = GetIDSubDept(SubDepts_sender);
                    int idSubdept_recipient = GetIDSubDept(SubDepts_recipient);
                    var selectedEqupment = nalich.Where(x => x.IdSubDept == idSubdept_sender).ToList();
                    UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
                    foreach (var item in selectedEqupment)
                    {
                        item.IdSubDept = idSubdept_recipient;
                        History_movement hm = new History_movement();
                        hm.Equipment_ID = item.ID;
                        hm.SubdeptID_sender=idSubdept_sender;
                        hm.SubdeptID_recipient=idSubdept_recipient;
                        hm.Room = item.P;
                        hm.Building = item.sprBuildings.BuildingID;
                        hm.user_id=uif.UserID;
                        hm.Data=DateTime.Now;
                        ppre.History_movement.Add(hm);
                    }

                    ppre.SaveChanges();
                }
            }
            else if (type == "partial_moved")
            {
                /* 1) получение фильтров*/
                /* 2)получение списка на передачу*/
               

                return RedirectToAction("GetMoveList", new { SubDepts_sender = SubDepts_sender, SubDepts_recipient = SubDepts_recipient, type = type });
            }
            movement_equipment_Model ee = new movement_equipment_Model();
            return PartialView("~/Views/Equipment/movement_equipment.cshtml", ee); 
        }
        //[HttpPost]
        //public ActionResult GetMoveList()
        //{
        //   var scroll_body2 = Request.Form["table_send"];
        //    string password = Request.Form["agr"];

        //  return PartialView();
        //}
        public ActionResult Function()
        {
            return PartialView();
        }

        public ActionResult GetMoveList(string SubDepts_sender, string SubDepts_recipient,  List<string> data) //получаем массив для изменения id_subdept
        {
            int idSubdept_sender = GetIDSubDept(SubDepts_sender);  
            int idSubdept_recipient = GetIDSubDept(SubDepts_recipient);
            using (PPREntities ppre = new PPREntities())
            {
                List<int> dataInt = new List<int>();
                UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
                foreach (var item in data)
                {
                    dataInt.Add(Convert.ToInt32(item));
                }
                var nalich = ppre.NALICH.ToList();
                var selectedEqupment = nalich.Where(n => dataInt.Any(t => t == n.ID)).ToList();
                foreach (var item in selectedEqupment)
                {
                   
                    History_movement hm = new History_movement();
                    hm.Equipment_ID = item.ID;
                    if (SubDepts_sender != "ОБЩИЙ СПИСОК")
                    {
                        hm.SubdeptID_sender = idSubdept_sender;
                    }
                    else hm.SubdeptID_sender = item.IdSubDept; 
                    item.IdSubDept = idSubdept_recipient;
                    hm.SubdeptID_recipient = idSubdept_recipient;
                    hm.Room = item.P;
                    hm.Building = item.sprBuildings.BuildingID;
                    hm.user_id = uif.UserID;
                    hm.Data = DateTime.Now;
                    ppre.History_movement.Add(hm);
                }       
                ppre.SaveChanges();
            }
            table_move_Model tmm = new table_move_Model();
            return RedirectToAction("MovementPost", "movement", new {tmm, SubDepts_sender = SubDepts_recipient, all_W = "", all_G = "", all_P = "", agr = "", regnum = "", invnum = "", name = "" });
            //return PartialView();
        }
        [HttpPost]
        public ActionResult GetSpisanieList(string SubDepts_sender, string list) //получаем массив для изменения id_subdept
        {
            var dataInt = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<int>>(list);
            int idSubdept_sender = GetIDSubDept(SubDepts_sender);
            //int idSubdept_recipient = GetIDSubDept(SubDepts_recipient);
            using (PPREntities ppre = new PPREntities())
            {
                //List<int> dataInt = new List<int>();
                UserInfoFull uif = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
                //foreach (var item in list)
                //{
                //    dataInt.Add(Convert.ToInt32(item));
                //}
                var selectedEqupment = ppre.NALICH.Where(n => dataInt.Any(t => t == n.ID)).ToList();
                foreach (var item in selectedEqupment)
                {

                    History_spisanie hm = new History_spisanie();
                    hm.Equipment_ID = item.ID;
                    if (SubDepts_sender != "ОБЩИЙ СПИСОК")
                    {
                        hm.SubdeptID_sender = idSubdept_sender;
                    }
                    else hm.SubdeptID_sender = item.IdSubDept;
                    //item.IdSubDept = idSubdept_sender;
                    item.status_spisanie = false;
                    hm.Room = item.P;
                    hm.Building = item.sprBuildings.BuildingID;
                    hm.user_id = uif.UserID;
                    hm.Data = DateTime.Now;
                    ppre.History_spisanie.Add(hm);
                }
                ppre.SaveChanges();
            }
            //spisanie_table_model te = new spisanie_table_model();
            //te.All_Type_name_items.Add(string.Empty);
            //te.All_Type_name_items.AddRange(te.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
            //te.All_Group_name_items.Add(string.Empty);
            //te.All_Group_name_items.AddRange(te.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
            //te.All_SubGroup_name_items.Add(string.Empty);
            //te.All_SubGroup_name_items.AddRange(te.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
            //te.AggregatCodifer.Add(string.Empty);
            //te.AggregatCodifer.AddRange(te.Equipment.Select(x => x.AggregateOfType).Distinct().OrderBy(x => x));
            //te.InvNums = "";
            //te.Nameobr = "";
            //te.RegNums = "";
            //return PartialView("~/Views/Equipment/spisanie_table.cshtml",te);
           // return RedirectToAction("SpisaniePost", "movement", new { Model=tmm, SubDepts_sender = SubDepts_sender, all_W = "", all_G = "", all_P = "", agr = "", regnum = "", invnum = "", name = "", reset = "" });
           return RedirectToAction("SpisanieOpen", "movement", new System.Web.Mvc.Ajax.AjaxOptions{ HttpMethod = "POST", UpdateTargetId = "LoadedContent" });

        }
        //public static void Savebd(object ppre)
        //{
        //    PPREntities newEntity = (PPREntities)ppre;
        //    newEntity.SaveChanges();
            
        //}
    }
}
