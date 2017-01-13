using EQServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQDataModel;
using System.Web.UI;
using System.Web.Routing;
using WebMatrix.WebData;
using EQServiceSystem.Models.Logger;
using System.Data;


namespace EQServiceSystem.Controllers
{
    public class FaktTimesController : Controller
    {
        string RN;
        RightsChecker CheckedObject = new RightsChecker();
        public void Message(string str)
       {
            ViewBag.message = str; ;
       }

        //
        // GET: /FaktTimes/
        [HttpGet]
        public ActionResult FaktT()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult FaktT(List<FactTimeModels> Model)
        {
            //TODO реализовать
                return PartialView("~/Views/Equipment/FactworkTime.cshtml",new FactTimeModels());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView(); 
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(tFactWorkTime FactTime)                                //новая запись в бд               
        {
            FactTime.RecordDate = DateTime.Now;
            try
            {
                using (PPREntities ppre = new PPREntities())
                {
                    ppre.tFactWorkTime.Add(FactTime);
                    FactTimeModels fm = new FactTimeModels();
                    var ChangesToLog = new List<ChangedRecord>();
                    ChangesToLog.Add(new ChangedRecord(WebSecurity.CurrentUserId, "tFactWorkTime", FactTime.RecordID, "FactWorkHours",DateTime.Now, ChangesTypes.СозданиеЗаписи));
                    ChangesToLog.Add(new ChangedRecord(WebSecurity.CurrentUserId, "tFactWorkTime", FactTime.RecordID, "AverageStatHours", DateTime.Now, ChangesTypes.СозданиеЗаписи));
                    ChangesToLog.Add(new ChangedRecord(WebSecurity.CurrentUserId, "tFactWorkTime", FactTime.RecordID, "RecordDate", DateTime.Now, ChangesTypes.СозданиеЗаписи));
                    ChangesLogger logger = new ChangesLogger();
                    ppre.SaveChanges();
                    logger.LogListRecordsChanges(ChangesToLog);
                    fm.MSG = "Данные сохранены";
                    //var fwt = new tFactWorkTime();      
                    return  PartialView("~/Views/Equipment/FactWorkTime.cshtml",fm);
                }   
            }
            catch (Exception c)
            {
                //Message("Ошибка сохранения " + c.ToString());
                return PartialView();
           }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Finds(string rn)                                                 //поиск
        {
            //var frakt = new sprFaktTime();
            //frakt.sprFaktTimes(rn);
            RN = rn;
            return PartialView("~/Views/Equipment/SprFaktTime.cshtml", new sprFaktTime(rn));
        }

        [HttpGet]
        public ActionResult Update()
        {
            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Equipment/SprFaktTime.cshtml", ppre.tFactWorkTime.ToList());
            }
        }
       [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update(List<EQServiceSystem.Models.sprFaktTime.myFaktTime> myFactWorkTimeList)  //редактирование
        {
            try
            {
                //if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "WriteSubDeptDirectory"))
                {
                    using (PPREntities ppre = new PPREntities())
                    {
                        List<EQServiceSystem.Models.sprFaktTime.myFaktTime> result = new List<EQServiceSystem.Models.sprFaktTime.myFaktTime>();
                        List<tFactWorkTime> before = ppre.tFactWorkTime.ToList(); //исходный список
                        var ChangesToLog = new List<ChangedRecord>(); 
                            result = (from Items in before
                                      join MyFact in myFactWorkTimeList on Items.RecordID equals MyFact.Id
                                      where (Items.AverageStatHours != MyFact.AverageStatHours) || (Items.FactWorkHours != MyFact.FactWorkHours)
                                      select MyFact).ToList();

                           for (int i = 0; i < result.Count; i++)
                           {
                                   ChangesToLog.Add(new ChangedRecord(WebSecurity.CurrentUserId, "tFactWorkTime", result[i].Id, "FactWorkHours", before.First(c => c.RecordID == result[i].Id).FactWorkHours, ChangesTypes.Изменение));
                                   ChangesToLog.Add(new ChangedRecord(WebSecurity.CurrentUserId, "tFactWorkTime", result[i].Id, "AverageStatHours", before.First(c => c.RecordID == result[i].Id).AverageStatHours, ChangesTypes.Изменение));
                                   ChangesToLog.Add(new ChangedRecord(WebSecurity.CurrentUserId, "tFactWorkTime", result[i].Id, "RecordDate", before.First(c => c.RecordID == result[i].Id).RecordDate, ChangesTypes.Изменение));
                           }

                            ChangesLogger logger = new ChangesLogger();
                             foreach (var item in result)
                        {
                            var changedItem = (from items in ppre.tFactWorkTime
                                               where items.RecordID == item.Id
                                               select items).ToList();
                            changedItem[0].AverageStatHours = item.AverageStatHours;
                            changedItem[0].FactWorkHours = item.FactWorkHours;
                            changedItem[0].RecordDate = DateTime.Now;
                            ppre.Entry(changedItem[0]).State = EntityState.Modified;
                        }
                             ppre.SaveChanges();
                             Message("Данные сохранены...");
                             logger.LogListRecordsChanges(ChangesToLog);
                    }
                }

            }
            catch (Exception e)
            {
                Message("Ошибка сохранения " + e.ToString());
            }
            return PartialView("~/Views/Equipment/SprFaktTime.cshtml", new sprFaktTime(null));
        }
    }

}
