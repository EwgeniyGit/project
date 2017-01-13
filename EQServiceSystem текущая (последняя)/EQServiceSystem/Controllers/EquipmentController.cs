using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQServiceSystem.ViewModels;
using PagedList.Mvc;
using PagedList;
using EQServiceSystem.Models.Units;
using WebMatrix.WebData;
using EQServiceSystem.Models.Equipment;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using EQServiceSystem.Models.Reports;
using EQDataModel;
using EQServiceSystem.Models.ElectronicArchives;
using NLog;
using EQServiceSystem.Models;
namespace EQServiceSystem.Controllers
{
    public class EquipmentController : Controller
    {
        //
        // GET: /Equipment/
        EquipmentCollection ECollection { get; set; }
        AdminEquipmentCollection AECollection { get; set; }
        List<EquipmentElement> Equipment = new List<EquipmentElement>();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        RightsChecker CheckedObject = new RightsChecker();
        UserInfoFull UserInfo = new UserInfoFull(WebMatrix.WebData.WebSecurity.CurrentUserId);
        public ActionResult Index()
        {
            return PartialView("~/Views/Equipment/GetEquipmentPassport.cshtml");
        }

        [HttpGet]
        public ActionResult GetEqiupmentPassport()
        {
            return PartialView("~/Views/Equipment/GetEquipmentPassport.cshtml");
        }

        [HttpPost]
        public ActionResult GetEqiupmentPassport(int EquipmentID)
        {
            EquipmentEditor ee = new EquipmentEditor(EquipmentID);
            return PartialView("~/Views/Equipment/equipment.cshtml", ee.Unit);
        }

        [HttpGet]
        public ActionResult SearchEquipment()
        {
            return PartialView("~/Views/Equipment/SearchEquipment.cshtml");
        }

        [HttpPost]
        public ActionResult SearchEquipment(string EquipmentID)
        {
            var cur_user = WebSecurity.CurrentUserName;
            var role = System.Web.Security.Roles.GetRolesForUser(cur_user).Single();
            if ((role == "Администратор") || (role == "Главный администратор") || (role == "Разработчик"))
            {
                var aec = new AdminEquipmentCollection();
                AECollection = aec;
                //aec.FilterByRegNo(EquipmentID);
                return PartialView("~/Views/Equipment/AdminEquipmentList.cshtml", aec);
            }
            var ec = new EquipmentCollection();
            ECollection = ec;
            ec.FilterByRegNo(EquipmentID);
            return PartialView("~/Views/Equipment/GetEquipmentList.cshtml", ec);
        }

        [HttpGet]
        public ActionResult GetEquipmentList()
        {
            var cur_user = WebSecurity.CurrentUserName;
            var role = System.Web.Security.Roles.GetRolesForUser(cur_user).Single();
            if ((role == "Администратор") || (role == "Главный администратор") || (role == "Разработчик"))
            {

                return RedirectToAction("Index", "Eq", new { page = 1 });
                //var aec = new AdminEquipmentCollection();
                //AECollection = aec;
                //return PartialView("~/Views/Equipment/AdminEquipmentList.cshtml", aec);
            }
            else return RedirectToAction("Index", "Eq", new { page = 1 });
            //var ec = new EquipmentCollection(page);
            //ECollection = ec;
            //return PartialView(ec);
        }
        //public ActionResult GetEquipment(AdminEquipmentCollection Model,int? page)
        //{
        //    int pageNumber = (page ?? 1);
        //    Equipment = Model.Equipment;
        //    return PartialView(Equipment.ToPagedList(pageNumber,100));
        //}
        [HttpGet]
        public ActionResult ClearFilter()
        {
            //ViewData["PageNum"] = page;
            var cur_user = WebSecurity.CurrentUserName;
            var role = System.Web.Security.Roles.GetRolesForUser(cur_user).Single();
            if ((role == "Администратор") || (role == "Главный администратор") || (role == "Разработчик"))
            {
                var aec = new AdminEquipmentCollection();
                AECollection = aec;
                return PartialView("~/Views/Equipment/AdminEquipmentList.cshtml", aec);
            }
            var ec = new EquipmentCollection();
            ECollection = ec;
            return PartialView(ec);
        }

        [HttpPost]
        public ActionResult GetEquipmentList(EquipmentCollection Model, string all_W, string all_G, string all_P, string all_Marks, string Buildings, string Rooms, int? page = 0)
        {
            if ((all_W != string.Empty) && (Model.All_Type_name_items.Count > 1))
                Model.FilterBy_W(all_W);
            if ((all_G != string.Empty) && (Model.All_Group_name_items.Count > 1))
                Model.FilterBy_G(all_G);
            if ((all_P != string.Empty) && (Model.All_SubGroup_name_items.Count > 1))
                Model.FilterBy_P(all_P);
            if ((all_Marks != string.Empty) && (Model.All_Marks_name_items.Count > 1))
                Model.FilterBy_Mark(all_Marks);
            if ((Buildings != string.Empty) && (Model.Buildings_items.Count > 1))
                Model.FilterByBuildings(Buildings);
            if ((Rooms != string.Empty) && (Model.Rooms_items.Count > 1))
                Model.FilterByRooms(Rooms);
            
         
          
            //Условие Count > 1 позволяет не проводить повторную фильтрацию по уже отфильтрованным параметрам
            //(в этом случае в списке будет всего 1 элемент)
            return PartialView(Model);
        }

       
        [HttpPost]
        public ActionResult GetAdminEquipmentList(AdminEquipmentCollection Model, string all_W, string all_G, string all_P, string all_Marks, string SubDepts, string Buildings, string Rooms)
        {
            AECollection =Model;
            //if ((all_W != string.Empty) && (AECollection.All_Type_name_items.Count > 1) && (all_W != " "))
            //    AECollection.FilterBy_W(all_W);
            //if ((all_G != string.Empty) && (AECollection.All_Group_name_items.Count > 1) && (all_G != " "))
            //    AECollection.FilterBy_G(all_G);
            //if ((all_P != string.Empty) && (AECollection.All_SubGroup_name_items.Count > 1) && (all_P != " "))
            //    AECollection.FilterBy_P(all_P);
            //if ((all_Marks != string.Empty) && (AECollection.All_Marks_name_items.Count > 1) && (all_Marks != " "))
            //    AECollection.FilterBy_Mark(all_Marks);
            //if ((SubDepts != string.Empty) && (AECollection.SubDepts.Count > 1))
            //    AECollection.FilterBySubDepts(SubDepts);
            //if ((Buildings != string.Empty) && (AECollection.Buildings.Count > 1))
            //    AECollection.FilterByBuildings(Buildings);
            //if ((Rooms != string.Empty) && (AECollection.Rooms.Count > 1))
            //    AECollection.FilterByRooms(Rooms);
            //сериализация фильтрованного списка в xml файл
           
            //Условие Count > 1 позволяет не проводить повторную фильтрацию по уже отфильтрованным параметрам
            //(в этом случае в списке будет всего 1 элемент)
            return PartialView("~/Views/Equipment/AdminEquipmentList.cshtml", AECollection);
        }

        [HttpPost]
        public ActionResult GetAction(int EquipmentID, string actions)
        {
            ViewBag.UserRole = UserInfo.UserRole;
            if (actions == "GetElectronicArchive")
            {
               //EQServiceSystem.Models.ElectronicArchive.ElectronicArchive ea = new EQServiceSystem.Models.ElectronicArchive.ElectronicArchive(EquipmentID);
               using (PPREntities context = new PPREntities())
               {
                   ElectronicArchives model = new ElectronicArchives(EquipmentID);
                   model.ElectronicArchive = context.ElectronicArchive.ToList();     
                   //{
                   //    model. = context.ElectronicArchive_subordinate.Where(x => !x.IsDeleted).ToArray().Select(x => new NewsModel());
                   //};

                   return PartialView("~/Views/Equipment/ElectronicArchive.cshtml", model);
               }
                //EquipmentEditor ee = new EquipmentEditor(EquipmentID);
                //return PartialView("~/Views/Equipment/ElectronicArchive.cshtml", ee.Unit);
            }
            else if (actions == "Movement") // история движение
            {
                history_move_Model hm = new history_move_Model(EquipmentID);
                    return PartialView("~/Views/Equipment/history_move.cshtml",hm);
                //открывать историю движения по ID оборудования
                //return RedirectToAction("GetMovement");
            }
            else if (actions == "Repair_history")
            {
                return GetRepair_history(EquipmentID);
            }          
            return PartialView();
        }
        //[HttpGet]
        public ActionResult GetElectronicArchive(int EquipmentID)
        {
            ElectronicArchives ee = new ElectronicArchives(EquipmentID);
            return PartialView("~/Views/Equipment/ElectronicArchive.cshtml", ee.Unit);
        }
        public ActionResult GetMovement()
        { 
            table_move_Model te = new table_move_Model();
            //movement_equipment_Model ee = new movement_equipment_Model();
             te.Equipment.RemoveAll(x => x.status_spisanie == false);
             te.All_Type_name_items.Add(string.Empty);
             te.All_Type_name_items.AddRange(te.Equipment.Select(x => x.TypeName).Distinct().OrderBy(x => x));  
             te.All_Group_name_items.Add(string.Empty);
             te.All_Group_name_items.AddRange(te.Equipment.Select(x => x.GroupName).Distinct().OrderBy(x => x)); 
             te.All_SubGroup_name_items.Add(string.Empty);
             te.All_SubGroup_name_items.AddRange(te.Equipment.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
             te.AggregatCodifer.Add(string.Empty);
             te.AggregatCodifer.AddRange(te.Equipment.Select(x => x.AggregateOfType).Distinct().OrderBy(x => x));   
            return PartialView("~/Views/Equipment/movement_equipment.cshtml", te); 
        }

        public ActionResult GetRepair_history(int EquipmentID)
        { return PartialView(); }
    
        [HttpPost]
        public JsonResult Upload(int ID, string REGNO, int types)
        {
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    string path = Server.MapPath("~/Files/" + REGNO + "_" + ID + "_" + types+ "_" + fileName);
                    logger.Info("Добавление файла: {0}", path);
                    upload.SaveAs(path);
                }
            }
            //RedirectToAction("GetElectronicArchive", "Equipment", new { EquipmentID = ID });
            return Json("файл загружен");
        }

        //[HttpGet]
        public ActionResult GetAttachedFiles(int ID, string REGNO, int type_doc)
        {      
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Files/"));
            //Выбираем файлы относящиеся к оборудованию с данным регистрационным номером и с данным ID и видом документа
            var AttachedFiles = di.GetFiles().Where(f => f.Name.Contains(REGNO + "_" + ID + "_" + type_doc + "_"));
            return PartialView(AttachedFiles);
        }

        [HttpGet]
        public FileResult GetFile(string fileName)
        {         
            string path = Server.MapPath("~/Files/");
            path = path + fileName;
            string file_type = "аpplication/" + fileName.Substring(fileName.IndexOf(".") + 1); //Добавляем тип приложения для открытия файлов
            var fs = new FileStream(path, FileMode.Open);
            return File(fs, file_type, fileName);
        }
        private bool RemoveFileFromServer(string path)
        {
            //var fullPath = Request.MapPath(path);
            if (!System.IO.File.Exists(path)) return false;

            try
            {
                System.IO.File.Delete(path);
                logger.Info("Удален файл: {0}", path);
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
            return false;
        }
       [HttpPost]
        public ActionResult DeleteFiles(List<string> data, int EquipmentID)
        {
                try
                {
                    foreach (var item in data)
                    {
                        string path = Server.MapPath("~/Files/" + item);
                        logger.Info("Попытка удаления файла: {0}", path);
                        RemoveFileFromServer(path);
                    }
                    using (PPREntities context = new PPREntities())
                    {
                        ElectronicArchives model = new ElectronicArchives(EquipmentID);
                        model.ElectronicArchive = context.ElectronicArchive.ToList();


                        return PartialView("~/Views/Equipment/ElectronicArchive.cshtml", model);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.Message);
                    return PartialView();
                }            
        }
       [HttpPost]
       public ActionResult ArchiveUp(int EquipmentID)
       {
           using (PPREntities context = new PPREntities())
           {
               ElectronicArchives model = new ElectronicArchives(EquipmentID);
               model.ElectronicArchive = context.ElectronicArchive.ToList();


               return PartialView("~/Views/Equipment/ElectronicArchive.cshtml", model);
           }
       }
    }
}
