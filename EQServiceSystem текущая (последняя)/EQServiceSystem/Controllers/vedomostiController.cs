using EQDataModel;
using EQServiceSystem.Models;
using EQServiceSystem.Models.Equipment;
using EQServiceSystem.Models.Reports;
using FastReport;
using FastReport.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EQServiceSystem.Controllers
{
    public class vedomostiController : Controller
    {
        //
        // GET: /ved_/
        private tUsers _user;
        public int UserID { get; private set; }
        private sprSubDept _sprSubDept;
        public string UserName { get; private set; }
        public string UserRole { get; private set; }
        public int? UserSubDeptID { get; private set; }
        public string UserSubDept { get; private set; }
        public FileResult GetFile(System.Data.DataTable dataTable, string Path)
        {
            FastReport.Utils.Config.WebMode = true;
            Report rep = new Report();
            rep.Load(Request.PhysicalApplicationPath + Path);
            rep.RegisterData(dataTable, "Table");
            rep.GetDataSource("Table").Enabled = true;
            //rep.Show();
            if (rep.Report.Prepare())
            {
                FastReport.Export.Pdf.PDFExport pdfExport = new FastReport.Export.Pdf.PDFExport();
                pdfExport.ShowProgress = false;
                pdfExport.Subject = "Subject";
                pdfExport.Title = "Отчет";
                pdfExport.Compressed = true;
                pdfExport.AllowPrint = true;
                pdfExport.EmbeddingFonts = true;

                MemoryStream strm = new MemoryStream();
                rep.Report.Export(pdfExport, strm);
                rep.Dispose();
                pdfExport.Dispose();
                strm.Position = 0;
                // return stream in browser
                FileStream file = new FileStream(Server.MapPath("~/Files/"+DateTime.Now.ToShortDateString() + ".pdf"), FileMode.Create, FileAccess.Write);
                strm.WriteTo(file);
                file.Close();
                strm.Close();
                return File(Server.MapPath("~/Files/" + DateTime.Now.ToShortDateString() + ".pdf"), "application/pdf");
                //return File(strm, "application/pdf", "report_" + DateTime.Now.ToShortDateString() + ".pdf");
            }
            else
            {
                return null;
            }
        }//формирование pdf из заданного table(dataTable) и указанного пути к файлу отчета frx
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

        [HttpGet]
        public ActionResult vedMTA_inst_adm()
        {
            UserInfo(WebSecurity.CurrentUserId);
            RightsChecker CheckedObject = new RightsChecker();
            if ((UserRole == "Администратор") || (UserRole == "Разработчик"))
            {
                return PartialView("~/Views/ReportsView/VedMTAinst1.cshtml", new vedMTA_inst());
            }
            else
            {
                ModelState.Clear();
                return PartialView("~/Views/ReportsView/VedMTAinst1.cshtml", new vedMTA_inst(WebSecurity.CurrentUserId)); 
            }
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "ved_inst")]
        public ActionResult vedMTA_inst_adm(int idSubDept)                     //1
        {
            //try
            //{
                using (PPREntities ppr = new PPREntities())
                {
                    var query = (from nalich in ppr.NALICH
                                 join bild in ppr.sprBuildings on nalich.BuildingID equals bild.BuildingID
                                 join subdepts in ppr.sprSubDept on nalich.IdSubDept equals subdepts.IdSubDept
                                 join aggregatecodifer in ppr.tAggregateCodifer on nalich.AggregateCode equals aggregatecodifer.SignID
                                 join sprEquipmentgroup in ppr.sprEquipmentGroup on nalich.G equals sprEquipmentgroup.SymbolID
                                 join sprequiment in ppr.sprEquipment on sprEquipmentgroup.GroupID equals sprequiment.IdEquipment
                                 where (subdepts.IdSubDept == idSubDept) &&
                                 ((aggregatecodifer.SignCode == "А") || (aggregatecodifer.SignCode == "В") ||
                                 (aggregatecodifer.SignCode == "М") || (aggregatecodifer.SignCode == "К"))
                                 select nalich).ToList();
                    DataTableReport dtr = new DataTableReport();
                    return GetFile(dtr.ReportTable(query), "Resources/Отчет.frx");
                }
            //}
            //catch (Exception e)
            //{
            //    ViewBag.Message = e.Message;
            //    return PartialView("~/Views/Partial1.cshtml");
            //}
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "All-inst")]
        public ActionResult vedMTAinst()                                       //1+
        {
            using (PPREntities ppr = new PPREntities())
            {
                var query = (from nalich in ppr.NALICH
                             join bild in ppr.sprBuildings on nalich.BuildingID equals bild.BuildingID
                             join subdepts in ppr.sprSubDept on nalich.IdSubDept equals subdepts.IdSubDept
                             join aggregatecodifer in ppr.tAggregateCodifer on nalich.AggregateCode equals aggregatecodifer.SignID
                             join sprEquipmentgroup in ppr.sprEquipmentGroup on nalich.G equals sprEquipmentgroup.SymbolID
                             join sprequiment in ppr.sprEquipment on sprEquipmentgroup.GroupID equals sprequiment.IdEquipment
                             where ((aggregatecodifer.SignCode == "А") || (aggregatecodifer.SignCode == "В") ||
                                  (aggregatecodifer.SignCode == "М") || (aggregatecodifer.SignCode == "К"))
                             select nalich).ToList();

                DataTableReport dtr = new DataTableReport();
                return GetFile(dtr.ReportTable(query), "Resources/Отчет.frx");
            } 
        }

        [HttpGet]
        public ActionResult vedMTA_SubDept_adm()
        {
            UserInfo(WebSecurity.CurrentUserId);
            RightsChecker CheckedObject = new RightsChecker();
            if ((UserRole == "Администратор") || (UserRole == "Разработчик"))
            {
                return PartialView("~/Views/ReportsView/VedMTASubDept2.cshtml", new vedMTA_inst());
            }
            else
            {
                return PartialView("~/Views/ReportsView/VedMTASubDept2.cshtml", new vedMTA_inst(WebSecurity.CurrentUserId));
            }
        }                           
        [HttpPost]
        public ActionResult vedMTA_SubDept_adm(int idSubDept)                  
        {
            using (PPREntities ppr = new PPREntities())
            {
                var query = (from nalich in ppr.NALICH
                 join bild in ppr.sprBuildings on nalich.BuildingID equals bild.BuildingID
                 join subdepts in ppr.sprSubDept on nalich.IdSubDept equals subdepts.IdSubDept
                 join aggregatecodifer in ppr.tAggregateCodifer on nalich.AggregateCode equals aggregatecodifer.SignID
                 join sprEquipmentgroup in ppr.sprEquipmentGroup on nalich.G equals sprEquipmentgroup.SymbolID
                 join sprequiment in ppr.sprEquipment on sprEquipmentgroup.GroupID equals sprequiment.IdEquipment
                 where (subdepts.IdSubDept == idSubDept) &&
                 ((aggregatecodifer.SignCode == "А") || (aggregatecodifer.SignCode == "В") ||
                 (aggregatecodifer.SignCode == "М") || (aggregatecodifer.SignCode == "К"))
                 select nalich).ToList();
                DataTableReport dtr = new DataTableReport();
                return GetFile(dtr.ReportTable(query), "Resources/Отчет2.frx");
            }
        }
        
        [HttpGet]
        public ActionResult VedMTASubDeptBuild()
        {
            UserInfo(WebSecurity.CurrentUserId);
            RightsChecker CheckedObject = new RightsChecker();
            if ((UserRole == "Администратор") || (UserRole == "Разработчик"))
            {
                return PartialView("~/Views/ReportsView/VedMTASubDeptBuild3.cshtml", new VedMTASubDeptBuild());
            }
            else
            {
                return PartialView("~/Views/ReportsView/VedMTASubDeptBuild3.cshtml", new VedMTASubDeptBuild(WebSecurity.CurrentUserId));
            }
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Report1")]
        public ActionResult VedMTASubDeptBuild(int sprSubDept, int BuildingID)  //3
        {
            using (PPREntities ppr = new PPREntities())
            {
                var query = (from nalich in ppr.NALICH
                             join bild in ppr.sprBuildings on nalich.BuildingID equals bild.BuildingID
                             join subdepts in ppr.sprSubDept on nalich.IdSubDept equals subdepts.IdSubDept
                             join aggregatecodifer in ppr.tAggregateCodifer on nalich.AggregateCode equals aggregatecodifer.SignID
                             join sprEquipmentgroup in ppr.sprEquipmentGroup on nalich.G equals sprEquipmentgroup.SymbolID
                             join sprequiment in ppr.sprEquipment on sprEquipmentgroup.GroupID equals sprequiment.IdEquipment
                             where ((aggregatecodifer.SignCode == "А") || (aggregatecodifer.SignCode == "В") ||
                                  (aggregatecodifer.SignCode == "М") || (aggregatecodifer.SignCode == "К")) && (subdepts.IdSubDept==sprSubDept) 
                                  && (bild.BuildingID==BuildingID)
                             select nalich).ToList();
                DataTableReport dtr = new DataTableReport();
                return GetFile(dtr.ReportTable(query), "Resources/Отчет3.frx");
            }
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "All-in")]
        public ActionResult VedMTASubDeptBuild(int sprSubDept)                  //3+
        {
            using (PPREntities ppr = new PPREntities())
            {
                var query = (from nalich in ppr.NALICH
                             join bild in ppr.sprBuildings on nalich.BuildingID equals bild.BuildingID
                             join subdepts in ppr.sprSubDept on nalich.IdSubDept equals subdepts.IdSubDept
                             join aggregatecodifer in ppr.tAggregateCodifer on nalich.AggregateCode equals aggregatecodifer.SignID
                             join sprEquipmentgroup in ppr.sprEquipmentGroup on nalich.G equals sprEquipmentgroup.SymbolID
                             join sprequiment in ppr.sprEquipment on sprEquipmentgroup.GroupID equals sprequiment.IdEquipment
                             where ((aggregatecodifer.SignCode == "А") || (aggregatecodifer.SignCode == "В") ||
                                  (aggregatecodifer.SignCode == "М") || (aggregatecodifer.SignCode == "К")) && (subdepts.IdSubDept==sprSubDept) 
                             select nalich).ToList();
                DataTableReport dtr = new DataTableReport();
                return GetFile(dtr.ReportTable(query), "Resources/Отчет3.frx");
            }
        }

        [HttpGet]
        public ActionResult VedMTASubDeptBuild4()
        {
            UserInfo(WebSecurity.CurrentUserId);
            RightsChecker CheckedObject = new RightsChecker();
            if ((UserRole == "Администратор") || (UserRole == "Разработчик"))
            {
                return PartialView("~/Views/ReportsView/VedMTASubDeptBuild4.cshtml", new VedMTASubDeptBuild4());
            }
            else
            {
                return PartialView("~/Views/ReportsView/VedMTASubDeptBuild4.cshtml", new VedMTASubDeptBuild4(WebSecurity.CurrentUserId));
            }
        }
        [HttpPost]
        public ActionResult VedMTASubDeptBuild4(int idSubDept, int BuildingID)//4
        {
            using (PPREntities ppr = new PPREntities())
            {
                var query = (from nalich in ppr.NALICH
                             join bild in ppr.sprBuildings on nalich.BuildingID equals bild.BuildingID
                             join subdepts in ppr.sprSubDept on nalich.IdSubDept equals subdepts.IdSubDept
                             join aggregatecodifer in ppr.tAggregateCodifer on nalich.AggregateCode equals aggregatecodifer.SignID
                             join sprEquipmentgroup in ppr.sprEquipmentGroup on nalich.G equals sprEquipmentgroup.SymbolID
                             join sprequiment in ppr.sprEquipment on sprEquipmentgroup.GroupID equals sprequiment.IdEquipment
                             where ((aggregatecodifer.SignCode == "А") || (aggregatecodifer.SignCode == "В") ||
                                  (aggregatecodifer.SignCode == "М") || (aggregatecodifer.SignCode == "К")) && (subdepts.IdSubDept == idSubDept)
                                  && (bild.BuildingID == BuildingID)
                             select nalich).ToList();
                DataTableReport dtr = new DataTableReport();
                return GetFile(dtr.ReportTable(query), "Resources/Отчет4.frx");
            }
        }

        [HttpGet]
        public ActionResult VedEnergoSubDeptBuild()
        {
            UserInfo(WebSecurity.CurrentUserId);
            RightsChecker CheckedObject = new RightsChecker();
            if ((UserRole == "Администратор") || (UserRole == "Разработчик"))
            {
                return PartialView("~/Views/ReportsView/VedEnergoSubDeptBuild5.cshtml", new VedEnergoSubDeptBuild());
            }
            else
            {
                return PartialView("~/Views/ReportsView/VedEnergoSubDeptBuild5.cshtml", new VedEnergoSubDeptBuild(WebSecurity.CurrentUserId));
            }
        }                         
        [HttpPost]
        public ActionResult VedEnergoSubDeptBuild(int idSubDept, int BuildingID)//5
        {
            using (PPREntities ppr = new PPREntities())
            {
                var query = (from nalich in ppr.NALICH
                             join bild in ppr.sprBuildings on nalich.BuildingID equals bild.BuildingID
                             join subdepts in ppr.sprSubDept on nalich.IdSubDept equals subdepts.IdSubDept
                             join aggregatecodifer in ppr.tAggregateCodifer on nalich.AggregateCode equals aggregatecodifer.SignID
                             join sprEquipmentgroup in ppr.sprEquipmentGroup on nalich.G equals sprEquipmentgroup.SymbolID
                             join sprequiment in ppr.sprEquipment on sprEquipmentgroup.GroupID equals sprequiment.IdEquipment
                             where ((aggregatecodifer.SignCode == "А") || (aggregatecodifer.SignCode == "Э")) && (subdepts.IdSubDept == idSubDept)
                                  && (bild.BuildingID == BuildingID)
                             select nalich).ToList();
                DataTableReport dtr = new DataTableReport();
                return GetFile(dtr.ReportTable(query), "Resources/Отчет5.frx");
            }
        }
        [HttpGet]
        public ActionResult VedMA_Group()
        {
            UserInfo(WebSecurity.CurrentUserId);
            RightsChecker CheckedObject = new RightsChecker();
            if ((UserRole == "Администратор") || (UserRole == "Разработчик"))
            {
                return PartialView("~/Views/ReportsView/vedMA_G.cshtml", new vedMA_G());
            }
            else
            {
                return PartialView("~/Views/ReportsView/vedMA_G.cshtml", new vedMA_G(WebSecurity.CurrentUserId));
            }
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "idGroup")]
        public ActionResult VedMA_Group(int idGroup)//6
        {
            using (PPREntities ppr = new PPREntities())
            {
                var query = (from nalich in ppr.NALICH
                             join bild in ppr.sprBuildings on nalich.BuildingID equals bild.BuildingID
                             join subdepts in ppr.sprSubDept on nalich.IdSubDept equals subdepts.IdSubDept
                             join aggregatecodifer in ppr.tAggregateCodifer on nalich.AggregateCode equals aggregatecodifer.SignID
                             join sprEquipmentgroup in ppr.sprEquipmentGroup on nalich.G equals sprEquipmentgroup.SymbolID
                             join sprequiment in ppr.sprEquipment on sprEquipmentgroup.GroupID equals sprequiment.IdEquipment
                             where ((aggregatecodifer.SignCode == "А") || (aggregatecodifer.SignCode == "М"))
                                  && (sprEquipmentgroup.GroupID == idGroup)
                             select nalich).ToList();
                DataTableReport dtr = new DataTableReport();



                //SqlConnection connection = new SqlConnection(ppr.Database.Connection.ConnectionString);
                //SqlCommand sqlcom = new SqlCommand("SELECT sprEquipmentGroup.*, NALICH.*, sprEquipment.*, tAggregateCodifer.*, sprBuildings.*, sprSubDeptsInBuildings.*, sprSubDept.* FROM sprEquipmentGroup INNER JOIN sprEquipment ON sprEquipmentGroup.GroupID = sprEquipment.EquipmentGroup INNER JOIN NALICH ON sprEquipment.EquipmentGroup = NALICH.G INNER JOIN tAggregateCodifer ON NALICH.AggregateCode = tAggregateCodifer.SignID INNER JOIN sprSubDept ON NALICH.IdSubDept = sprSubDept.IdSubDept INNER JOIN sprSubDeptsInBuildings ON sprSubDept.IdSubDept = sprSubDeptsInBuildings.SubDeptID INNER JOIN sprBuildings ON NALICH.BuildingID = sprBuildings.BuildingID AND sprSubDeptsInBuildings.BuildingID = sprBuildings.BuildingID WHERE ((SignCode = N'М') OR (SignCode = N'А')) and (sprEquipmentGroup.GroupID = " + idGroup + ")", connection);
                //SqlDataAdapter adapter = new SqlDataAdapter(sqlcom);
                //DataTable Table = new DataTable();
                //adapter.Fill(Table);
                return GetFile(dtr.ReportTable(query), "Resources/Отчет6.frx");
            }
        }

        [HttpGet]
        public ActionResult VedMT_RU()
        {
            UserInfo(WebSecurity.CurrentUserId);
            RightsChecker CheckedObject = new RightsChecker();
            if ((UserRole == "Администратор") || (UserRole == "Разработчик"))
            {
                return PartialView("~/Views/ReportsView/vedMT_RU.cshtml", new vedMT_RU());
            }
            else
            {
                return PartialView("~/Views/ReportsView/vedMT_RU.cshtml", new vedMT_RU(WebSecurity.CurrentUserId));
            }
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "idRU")]
        public ActionResult VedMT_RU(int idRU)//7
        {
            using (PPREntities ppr = new PPREntities())
            {
                var query = (from nalich in ppr.NALICH
                             join subdepts in ppr.sprSubDept on nalich.IdSubDept equals subdepts.IdSubDept
                             join bild in ppr.sprBuildings on nalich.BuildingID equals bild.BuildingID
                             join aggregatecodifer in ppr.tAggregateCodifer on nalich.AggregateCode equals aggregatecodifer.SignID
                             join sprEquipmentgroup in ppr.sprEquipmentGroup on nalich.G equals sprEquipmentgroup.SymbolID
                             join sprequiment in ppr.sprEquipment on sprEquipmentgroup.GroupID equals sprequiment.IdEquipment
                             join importantEquiment in ppr.tImportantEquipment on nalich.ID equals importantEquiment.EquipmentID
                             join sprreactors in ppr.sprReactors on importantEquiment.ReactorID equals sprreactors.ID
                             join reactSafetyClasses in ppr.sprReactSafetyClasses on importantEquiment.SecurityClassID equals reactSafetyClasses.ID
                             where (sprreactors.ID==idRU) && (nalich.ImportantToSafety)
                             select nalich).ToList();
                DataTableReport dtr = new DataTableReport();
                return GetFile(dtr.ReportTableReactors(query), "Resources/Отчет7РУ.frx");
            }
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "AllRU")]
        public ActionResult VedMT_AllRU()//7 все РУ в институте
        {
            using (PPREntities ppr = new PPREntities())
            {
                var query = (from nalich in ppr.NALICH
                             join subdepts in ppr.sprSubDept on nalich.IdSubDept equals subdepts.IdSubDept
                             join bild in ppr.sprBuildings on nalich.BuildingID equals bild.BuildingID
                             join aggregatecodifer in ppr.tAggregateCodifer on nalich.AggregateCode equals aggregatecodifer.SignID
                             join sprEquipmentgroup in ppr.sprEquipmentGroup on nalich.G equals sprEquipmentgroup.SymbolID
                             join sprequiment in ppr.sprEquipment on sprEquipmentgroup.GroupID equals sprequiment.IdEquipment
                             join importantEquiment in ppr.tImportantEquipment on nalich.ID equals importantEquiment.EquipmentID
                             join sprreactors in ppr.sprReactors on importantEquiment.ReactorID equals sprreactors.ID
                             join reactSafetyClasses in ppr.sprReactSafetyClasses on importantEquiment.SecurityClassID equals reactSafetyClasses.ID
                             where nalich.ImportantToSafety
                             select nalich).ToList();
                DataTableReport dtr = new DataTableReport();
                return GetFile(dtr.ReportTableReactors(query), "Resources/Отчет7РУAll.frx");
            }  
        }


        [HttpPost]
        public ActionResult SelectedSubDept(int depId)
        {
            using (PPREntities db = new PPREntities())
            {
                var sprSubs = (from item in db.sprSubDept
                               where item.IdSubDept == depId
                               select item.sprBuildings).FirstOrDefault().ToList();

                var newList = (from item in sprSubs
                               select new ListBuild() { Id = item.BuildingID, Name = item.BuldingName });


                if (sprSubs == null)
                    return Json(null);

                List<ListBuild> models = (List<ListBuild>)newList.ToList();
                return Json(models);
            }
        }
    }
}
