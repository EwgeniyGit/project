using EQDataModel;
using EQServiceSystem.Models.Units;
using EQServiceSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EQServiceSystem.Controllers
{

    public class movementController : Controller
    {    
        const int pageSize = 50;
        List<EquipmentElement> Equipment = new List<EquipmentElement>();
        //public List<EquipmentElement> EquipmentFull()
        //{
        // using (PPREntities ppre = new PPREntities())
        //    {
        //        //var SubDeptsList = (from Items in ppre.sprSubDept
        //        //            select new 
        //        //            {
        //        //                Items.SubDeptCode,
        //        //                Items.ShortName
        //        //            }).ToList();
        //        //foreach (var item in SubDeptsList)
        //        //{
        //        //    SubDepts_sender.Add(item.SubDeptCode + " " + item.ShortName);
        //        //}
        //        //SubDepts_recipient = SubDepts_sender;
 
        //        var Eq = (from items in ppre.NALICH
        //                  join tTypeISGroup in ppre.TypeISGroup on items.id_TypeIsGroup equals tTypeISGroup.id
        //                  join tTypeOfEq in ppre.sprTypeOfEquipment on tTypeISGroup.idType equals tTypeOfEq.ID
        //                  join tGroup in ppre.sprGroup on tTypeISGroup.idGroup equals tGroup.idGroup
        //                  join tSubGroup in ppre.sprSubGroup on tTypeISGroup.idSubGroup equals tSubGroup.idSubGroup
        //                  //join tMark in ppre.sprMark on tTypeISGroup.idMark equals tMark.id_mark
        //                  //join tModel in ppre.sprModel on tTypeISGroup.idModel equals tModel.id_model
        //                  select new
        //                  {
        //                      items.ID,
        //                      items.sprSubDept.ShortName,
        //                      items.sprSubDept.SubDeptCode,
        //                      items.sprBuildings.BuldingName,
        //                      items.REGNO,
        //                      items.INWNOM,
        //                      items.tAggregateCodifer.SignCode,
        //                      items.NAIMOB,
        //                      items.W,
        //                      items.G,
        //                      items.P,
        //                      items.POM,
        //                      items.MARKA,
        //                      tTypeOfEq.TypeOfEquipment,
        //                      tGroup.GroupName,
        //                      tSubGroup.SubGroupName,
        //                      //tMark.mark,
        //                      //tModel.model,
        //                      tTypeOfEq.TypeCode,
        //                      tTypeISGroup.oldValue,
        //                      tGroup.idGroup
        //                  }).ToList(); 
        //        Equipment = new List<EquipmentElement>();
        //        foreach (var item in Eq)
        //        {
        //            var elem = new EquipmentElement(item.ID);
        //            elem.AggregateOfType = item.SignCode;
        //            elem.BuildingName = item.BuldingName;
        //            elem.InvNumber = item.INWNOM;
        //            elem.Name = item.NAIMOB;
        //            elem.RegNumber = item.REGNO;
        //            elem.W = item.W;
        //            elem.G = item.G;
        //            //elem.P = item.P;
        //            //elem.Room = item.POM;
        //            //elem.Mark = item.MARKA;
        //            elem.TypeName = item.TypeCode + " " + item.TypeOfEquipment;
        //            if (item.idGroup.ToString().Length < 2)
        //                elem.GroupName = "0" + item.idGroup + " " + item.GroupName;
        //            else
        //                elem.GroupName = item.idGroup + " " + item.GroupName;
        //            //elem.GroupName = item.idGroup + " " + item.GroupName;
        //            elem.TypeCode = item.TypeCode;
        //            elem.SubGroupName = item.oldValue + " " + item.SubGroupName;
        //            elem.SubDeptName = item.SubDeptCode + " " + item.ShortName;
        //            Equipment.Add(elem);
        //        }
        //    }
        // return Equipment;
        //    //var Equipments = Equipment.AsEnumerable();
        //}
    //     public ActionResult Index(int? id)
    //{
    //    int page = id ?? 0;
    //    if (Request.IsAjaxRequest())
    //    {
    //        return PartialView("~/Views/Equipment/move_eq.cshtml", GetItemsPage(page));
    //    }
    //    return View(GetItemsPage(page));
    //}
    //     private List<EquipmentElement> GetItemsPage(int page = 1)
    //     {
    //         var itemsToSkip = page * pageSize;
    //         return EquipmentFull().OrderBy(t => t.SubDeptName).Skip(itemsToSkip).
    //             Take(pageSize).ToList();
    //     }

         //public ActionResult FunctionUpdate(string subdept_send, string all_W, string all_G,  string all_P, string agr, string regNum, string invNum, string name)
         //{
         //    if (Request.IsAjaxRequest())
         //    {
         //        //table_move_Model tmm = new table_move_Model(subdept_send,all_W,all_G,all_P,agr,regNum,invNum,name);
         //       return PartialView("~/Views/Equipment/tablemove.cshtml");
         //    }
         //    return PartialView();          
         //}
         public void FilterBySubDepts(string SubDepts_sender, table_move_Model Model)
         {
                 Model.Equipment.RemoveAll(x => x.SubDeptName != SubDepts_sender);
                 Model.SubDepts_sender.RemoveRange(0, Model.SubDepts_sender.Count);
                 Model.SubDepts_sender.Add(SubDepts_sender);          
         }
         public void FilterBy_W(string all_W, table_move_Model Model)
         {
             Model.Equipment.RemoveAll(x => x.TypeName != all_W);
             Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
             Model.All_Type_name_items.Add(all_W);
         }
         public void FilterBy_G(string all_G, table_move_Model Model)
         {
             Model.Equipment.RemoveAll(x => x.GroupName != all_G);
             Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
             Model.All_Group_name_items.Add(all_G);
         }
         public void FilterBy_P(string all_P, table_move_Model Model)
         {
             Model.Equipment.RemoveAll(x => x.SubGroupName != all_P);
             Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
             Model.All_SubGroup_name_items.Add(all_P);
         }
         public void FilterBy_agr(string agr, table_move_Model Model)
         {
             Model.Equipment.RemoveAll(x => x.AggregateOfType != agr);
             Model.AggregatCodifer.RemoveRange(0, Model.AggregatCodifer.Count);
             Model.AggregatCodifer.Add(agr);
         }
         public void FilterBy_regnum(string regnum, table_move_Model Model)
         {
             Model.Equipment = Model.Equipment.Where(x => x.RegNumber.Contains(regnum)).ToList(); 
             Model.RegNums = regnum;
         }
         public void FilterBy_invnum(string invnum, table_move_Model Model)
         {
             Model.Equipment = Model.Equipment.Where(x => x.InvNumber.Contains(invnum)).ToList();
             Model.InvNums = invnum;
         }
         public void FilterBy_name(string name, table_move_Model Model)
         {
             Model.Equipment = Model.Equipment.Where(x => x.Name.Contains(name)).ToList();
             Model.Nameobr = name;
         }
         public ActionResult MovementPost(table_move_Model Model, string SubDepts_sender, string all_W, string all_G, string all_P, string agr, string regnum, string invnum, string name, string reset)
        {
            var mem = new movement_equipment_Model();
            var table = new table_move_Model();
            if (reset!="Сброс")
            {  
                if ((SubDepts_sender != string.Empty) && (SubDepts_sender != null) && (SubDepts_sender != " ") && (SubDepts_sender != "ОБЩИЙ СПИСОК"))
                    FilterBySubDepts(SubDepts_sender, table);
                if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                    FilterBy_W(all_W, table);
                if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                    FilterBy_G(all_G, table);
                if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                    FilterBy_P(all_P, table);
                if ((agr != string.Empty) && (agr != null) && (agr != " "))
                    FilterBy_agr(agr, table);
                if ((regnum != string.Empty) && (regnum != null) && (regnum != " "))
                    FilterBy_regnum(regnum, table);
                if ((invnum != string.Empty) && (invnum != null) && (invnum != " "))
                    FilterBy_invnum(invnum, table);
                if ((name != string.Empty) && (name != null) && (name != " "))
                    FilterBy_name(name, table);
                #region заполнение выпадающих списков
                var t = table.Equipment.Select(x => x.TypeName).Distinct().ToList();
                if (t.Count > 1)
                {
                    table.All_Type_name_items.Add(" ");
                    table.All_Type_name_items.AddRange(table.Equipment.Select(x => x.TypeName).Distinct());
                }
                else
                {
                    table.All_Type_name_items.AddRange(table.Equipment.Select(x => x.TypeName).Distinct());
                }
                var g = table.Equipment.Select(x => x.GroupName).Distinct().ToList();
                if (g.Count > 1)
                {
                    table.All_Group_name_items.Add(" ");
                    table.All_Group_name_items.AddRange(table.Equipment.Select(x => x.GroupName).Distinct());
                }
                else
                {
                    table.All_Group_name_items.AddRange(table.Equipment.Select(x => x.GroupName).Distinct());
                }
                var p = table.Equipment.Select(x => x.SubGroupName).Distinct().ToList();
                if (p.Count > 1)
                {
                    table.All_SubGroup_name_items.Add(" ");
                    table.All_SubGroup_name_items.AddRange(table.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                }
                else
                {
                    table.All_SubGroup_name_items.AddRange(table.Equipment.Select(x => x.SubGroupName).Distinct());
                }
                var ag = table.Equipment.Select(x => x.AggregateOfType).Distinct().ToList();
                if (ag.Count > 1)
                {
                    table.AggregatCodifer.Add(" ");
                    table.AggregatCodifer.AddRange(table.Equipment.Select(x => x.AggregateOfType).Distinct().OrderBy(x => x));
                }
                else
                {
                    table.AggregatCodifer.AddRange(table.Equipment.Select(x => x.AggregateOfType).Distinct());
                }
                #endregion
            }
            else
            {  
                table.Equipment.RemoveAll(x => x.SubDeptName != SubDepts_sender);
                table.All_Type_name_items.Add(string.Empty);
                table.All_Type_name_items.AddRange(table.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
                table.All_Group_name_items.Add(string.Empty);
                table.All_Group_name_items.AddRange(table.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                table.All_SubGroup_name_items.Add(string.Empty);
                table.All_SubGroup_name_items.AddRange(table.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                table.AggregatCodifer.Add(string.Empty);
                table.AggregatCodifer.AddRange(table.Equipment.Select(x => x.AggregateOfType).Distinct().OrderBy(x => x));
                //ViewBag.param = "reset";
            }


            return PartialView("~/Views/Equipment/tablemove.cshtml", table);
        }

         public ActionResult SpisanieOpen()
         {
             //var mem = new Spisanie_Model();
             var te = new spisanie_table_model();
             te.All_Type_name_items.Add(string.Empty);
             te.All_Type_name_items.AddRange(te.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
             te.All_Group_name_items.Add(string.Empty);
             te.All_Group_name_items.AddRange(te.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
             te.All_SubGroup_name_items.Add(string.Empty);
             te.All_SubGroup_name_items.AddRange(te.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
             te.AggregatCodifer.Add(string.Empty);
             te.AggregatCodifer.AddRange(te.Equipment.Select(x => x.AggregateOfType).Distinct().OrderBy(x => x));
             te.InvNums = "";
             te.Nameobr = "";
             te.RegNums = "";
             return PartialView("~/Views/Equipment/Spisanie.cshtml",te);
         }
         public void FilterBySubDepts(string SubDepts_sender, spisanie_table_model Model)
         {
             Model.Equipment.RemoveAll(x => x.SubDeptName != SubDepts_sender);
             Model.SubDepts_sender.RemoveRange(0, Model.SubDepts_sender.Count);
             Model.SubDepts_sender.Add(SubDepts_sender);
         }
         public void FilterBy_W(string all_W, spisanie_table_model Model)
         {
             Model.Equipment.RemoveAll(x => x.TypeName != all_W);
             Model.All_Type_name_items.RemoveRange(0, Model.All_Type_name_items.Count);
             Model.All_Type_name_items.Add(all_W);
         }
         public void FilterBy_G(string all_G, spisanie_table_model Model)
         {
             Model.Equipment.RemoveAll(x => x.GroupName != all_G);
             Model.All_Group_name_items.RemoveRange(0, Model.All_Group_name_items.Count);
             Model.All_Group_name_items.Add(all_G);
         }
         public void FilterBy_P(string all_P, spisanie_table_model Model)
         {
             Model.Equipment.RemoveAll(x => x.SubGroupName != all_P);
             Model.All_SubGroup_name_items.RemoveRange(0, Model.All_SubGroup_name_items.Count);
             Model.All_SubGroup_name_items.Add(all_P);
         }
         public void FilterBy_agr(string agr, spisanie_table_model Model)
         {
             Model.Equipment.RemoveAll(x => x.AggregateOfType != agr);
             Model.AggregatCodifer.RemoveRange(0, Model.AggregatCodifer.Count);
             Model.AggregatCodifer.Add(agr);
         }
         public void FilterBy_regnum(string regnum, spisanie_table_model Model)
         {
             Model.Equipment = Model.Equipment.Where(x => x.RegNumber.Contains(regnum)).ToList();
             Model.RegNums = regnum;
         }
         public void FilterBy_invnum(string invnum, spisanie_table_model Model)
         {
             Model.Equipment = Model.Equipment.Where(x => x.InvNumber.Contains(invnum)).ToList();
             Model.InvNums = invnum;
         }
         public void FilterBy_name(string name, spisanie_table_model Model)
         {
             Model.Equipment = Model.Equipment.Where(x => x.Name.Contains(name)).ToList();
             Model.Nameobr = name;
         }
         public ActionResult SpisaniePost(spisanie_table_model Model, string SubDepts_sender, string all_W, string all_G, string all_P, string agr, string regnum, string invnum, string name, string reset)
        {
                var table = new spisanie_table_model();
                var mem = new Spisanie_Model();
                if (reset != "Сброс")
                {
                    table.Equipment.RemoveAll(x => x.status_spisanie == false);
                    if ((SubDepts_sender != string.Empty) && (SubDepts_sender != null) && (SubDepts_sender != " ") && (SubDepts_sender != "ОБЩИЙ СПИСОК"))
                        FilterBySubDepts(SubDepts_sender, table);
                    if ((all_W != string.Empty) && (all_W != null) && (all_W != " "))
                        FilterBy_W(all_W, table);
                    if ((all_G != string.Empty) && (all_G != null) && (all_G != " "))
                        FilterBy_G(all_G, table);
                    if ((all_P != string.Empty) && (all_P != null) && (all_P != " "))
                        FilterBy_P(all_P, table);
                    if ((agr != string.Empty) && (agr != null) && (agr != " "))
                        FilterBy_agr(agr, table);
                    if ((regnum != string.Empty) && (regnum != null) && (regnum != " "))
                        FilterBy_regnum(regnum, table);
                    if ((invnum != string.Empty) && (invnum != null) && (invnum != " "))
                        FilterBy_invnum(invnum, table);
                    if ((name != string.Empty) && (name != null) && (name != " "))
                        FilterBy_name(name, table);
                    #region заполнение выпадающих списков
                    var t = table.Equipment.Select(x => x.TypeName).Distinct().ToList();
                    if (t.Count > 1)
                    {
                        table.All_Type_name_items.Add(" ");
                        table.All_Type_name_items.AddRange(table.Equipment.Select(x => x.TypeName).Distinct());
                    }
                    else
                    {
                        table.All_Type_name_items.AddRange(table.Equipment.Select(x => x.TypeName).Distinct());
                    }
                    var g = table.Equipment.Select(x => x.GroupName).Distinct().ToList();
                    if (g.Count > 1)
                    {
                        table.All_Group_name_items.Add(" ");
                        table.All_Group_name_items.AddRange(table.Equipment.Select(x => x.GroupName).Distinct());
                    }
                    else
                    {
                        table.All_Group_name_items.AddRange(table.Equipment.Select(x => x.GroupName).Distinct());
                    }
                    var p = table.Equipment.Select(x => x.SubGroupName).Distinct().ToList();
                    if (p.Count > 1)
                    {
                        table.All_SubGroup_name_items.Add(" ");
                        table.All_SubGroup_name_items.AddRange(table.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                    }
                    else
                    {
                        table.All_SubGroup_name_items.AddRange(table.Equipment.Select(x => x.SubGroupName).Distinct());
                    }
                    var ag = table.Equipment.Select(x => x.AggregateOfType).Distinct().ToList();
                    if (ag.Count > 1)
                    {
                        table.AggregatCodifer.Add(" ");
                        table.AggregatCodifer.AddRange(table.Equipment.Select(x => x.AggregateOfType).Distinct().OrderBy(x => x));
                    }
                    else
                    {
                        table.AggregatCodifer.AddRange(table.Equipment.Select(x => x.AggregateOfType).Distinct());
                    }
                    #endregion
                }
                else
                {
                    table.Equipment.RemoveAll(x => x.status_spisanie == true);
                    table.Equipment.RemoveAll(x => x.SubDeptName != SubDepts_sender);
                    table.All_Type_name_items.Add(string.Empty);
                    table.All_Type_name_items.AddRange(table.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));
                    table.All_Group_name_items.Add(string.Empty);
                    table.All_Group_name_items.AddRange(table.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                    table.All_SubGroup_name_items.Add(string.Empty);
                    table.All_SubGroup_name_items.AddRange(table.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                    table.AggregatCodifer.Add(string.Empty);
                    table.AggregatCodifer.AddRange(table.Equipment.Select(x => x.AggregateOfType).Distinct().OrderBy(x => x));
                    //ViewBag.param = "reset";
                }

                return PartialView("~/Views/Equipment/spisanie_table.cshtml", table);
            }
         public ActionResult history_spisanie()
         {   
             var te = new history_spisanie_Model();
             te.beginDate = DateTime.Now.Date;
             te.endDate = DateTime.Now.Date;
             //te.All_Type_name_items.Add(string.Empty);
             //te.All_Type_name_items.AddRange(te.History_full.Select(x => x.TypeName).Distinct().OrderBy(x => x));
             //te.All_Group_name_items.Add(string.Empty);
             //te.All_Group_name_items.AddRange(te.History_full.Select(x => x.GroupName).Distinct().OrderBy(x => x));
             //te.All_SubGroup_name_items.Add(string.Empty);
             //te.All_SubGroup_name_items.AddRange(te.History_full.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
             ////te.AggregatCodifer.Add(string.Empty);
             ////te.AggregatCodifer.AddRange(te.History_full.Select(x => x.AggregateOfType).Distinct().OrderBy(x => x));
             //te.InvNums = "";
             //te.Nameobr = "";
             //te.RegNums = "";
             return PartialView("~/Views/Equipment/history_spisanie.cshtml",te);
         }
         public ActionResult Spisanie_Find(string beginDate, string endDate, string SubDepts, string name, string invnum, string regnum, List<int> status)
         {
             if (status == null)//кнопка поиск
             {
                 var te = new history_spisanie_Model();
                 if ((SubDepts != string.Empty) && (SubDepts != null) && (SubDepts != " "))
                     FilterBySubDepts(SubDepts, te);
                 if ((name != string.Empty) && (name != null) && (name != " "))
                     FilterBy_name(name, te);
                 if ((invnum != string.Empty) && (invnum != null) && (invnum != " "))
                     FilterBy_invnum(invnum, te);
                 if ((regnum != string.Empty) && (regnum != null) && (regnum != " "))
                     FilterBy_regnum(regnum, te);
                 if ((((beginDate != DateTime.MinValue.ToString()) && (beginDate != "") && (beginDate != " "))) &&
                     (((endDate != DateTime.MaxValue.ToString()) && (endDate != "") && (endDate != " "))))
                     FilterBy_Date(beginDate, endDate, te);
                 return PartialView("~/Views/Equipment/history_spisanie.cshtml", te);
             }
             else //кнопка восстановить
             {
                 using (PPREntities ppre = new PPREntities())
                 {
                     //var nalich = ppre.NALICH.ToList();
                     foreach (var item in status)
                     {
                         var selectedEqupment = ppre.NALICH.Where(x => x.ID == item).Single();
                         selectedEqupment.status_spisanie = true;
                     }
                     ppre.SaveChanges();
                     var te = new history_spisanie_Model();
                     return PartialView("~/Views/Equipment/history_spisanie.cshtml", te);
                 }
             }
         }
         public void FilterBySubDepts(string SubDepts, history_spisanie_Model Model)
         {
             Model.History_List.RemoveAll(x => x.SubDeptName != SubDepts);
             Model.SubDept_items.RemoveRange(0, Model.SubDept_items.Count);
             Model.SubDept_items.Add(SubDepts);
         }
         public void FilterBy_regnum(string regnum, history_spisanie_Model Model)
         {
             Model.History_List = Model.History_List.Where(x => x.RegNum.Contains(regnum)).ToList();
             Model.RegNums = regnum;
         }
         public void FilterBy_invnum(string invnum, history_spisanie_Model Model)
         {
             Model.History_List = Model.History_List.Where(x => x.InvNum.Contains(invnum)).ToList();
             Model.InvNums = invnum;
         }
         public void FilterBy_name(string name, history_spisanie_Model Model)
         {
             Model.History_List = Model.History_List.Where(x => x.NAIMOB.Contains(name)).ToList();
             Model.Nameobr = name;
         }
         public void FilterBy_Date(string beginDate, string endDate, history_spisanie_Model Model)
         {
             var bdate = Convert.ToDateTime(beginDate);
             var edate = Convert.ToDateTime(endDate);
             Model.History_List = Model.History_List.Where(x => (x.Data >= bdate) && (x.Data <= edate)).ToList();
             Model.beginDate = bdate.Date;
             Model.endDate = edate.Date;
         }
         public ActionResult GetHistorySpisanieList(List<string> data) 
         {
             return PartialView();
         }
    }
}