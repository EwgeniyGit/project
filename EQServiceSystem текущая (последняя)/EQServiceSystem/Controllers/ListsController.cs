using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using EQServiceSystem.Models;
using EQServiceSystem.ViewModels;
using EQServiceSystem.Models.Logger;
using WebMatrix.WebData;
using EQDataModel;
using System.Data.Objects;
using System.IO;

namespace EQServiceSystem.Controllers
{
    public class ListsController : Controller
    {
        RightsChecker CheckedObject = new RightsChecker();
        FunctionAccessManager famanager;
        
        [HttpGet]
        public ActionResult SubDeptsList()
        {
            return PartialView("~/Views/Lists/SubDeptsList.cshtml", new SubDeptsEditor().SubDeptsList);
        }

        [HttpPost]
        public ActionResult SubDeptsList(List<sprSubDept> Model)
        {
            if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "SubDeptsList", AccessType.Write))
            {
                var sde = new SubDeptsEditor(Model);
                sde.UpdateChanges();
                return RedirectToAction("SubDeptsList");
            }
            else
            {
                ViewBag.Message = "У вас нет прав для изменения списка подразделений.";
                return PartialView("~/Views/Lists/SubDeptsList.cshtml", new SubDeptsEditor().SubDeptsList);
            }
        }

        public ActionResult UsersList()
        {
            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/UsersList.cshtml", ppre.tUsers.OrderBy(x => x.FIO).ToList());
            }
        }
        
        [HttpGet]
        public ActionResult RolesRightsFunctionsList()
        {
            var rrf = new RolesRightsFunctionsList();
            return PartialView("~/Views/Lists/RolesRightsFunctionsList.cshtml", rrf);
        }

        [HttpPost]
        public ActionResult RolesRightsFunctionsList(object UserFunctions)
        {
            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/AddUserFunction.cshtml", ppre.tUserFunctions);
            }
        }

        [HttpGet]
        public ActionResult AddUserFunction()
        {
            if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "RolesRightsFunctionsList", AccessType.Write))
            {
                return PartialView("~/Views/Lists/AddUserFunction.cshtml", new tUserFunctions());
            }
            else
            {
                ViewBag.Message = "У вас нет прав для добавления новых функций";
                var rrf = new RolesRightsFunctionsList();
                return PartialView("~/Views/Lists/RolesRightsFunctionsList.cshtml", rrf);
            }
        }

        [HttpPost]
        public ActionResult AddUserFunction(tUserFunctions function)
        {
            using (PPREntities ppre = new PPREntities())
            {
                ppre.tUserFunctions.Add(function);
                ppre.SaveChanges();
                var rrf = new RolesRightsFunctionsList();
                return PartialView("~/Views/Lists/RolesRightsFunctionsList.cshtml", rrf);
            }
        }

        [HttpGet]
        public ActionResult AddFunctionAccess()
        {
            if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "RolesRightsFunctionsList", AccessType.Write))
            {
                famanager = new FunctionAccessManager();
                return PartialView("~/Views/Lists/AddFunctionAccess.cshtml", famanager);
            }
            else
            {
                ViewBag.Message = "У вас нет возможности редактирования прав доступа";
                var rrf = new RolesRightsFunctionsList();
                return PartialView("~/Views/Lists/RolesRightsFunctionsList.cshtml", rrf);
            }
        }

        [HttpPost]
        public ActionResult AddFunctionAccess(FunctionAccessManager faccess, int SelectedFunctionID, int SelectedObjectID, string CreateToUser)
        {
            faccess.SelectedFunctionID = SelectedFunctionID;
            faccess.ObjectToGetAccessID = SelectedObjectID;
            if (CreateToUser != null)
                faccess.AccessTypeUser = true;
            faccess.CreateAccess();
            var rrf = new RolesRightsFunctionsList();
            return PartialView("~/Views/Lists/RolesRightsFunctionsList.cshtml", rrf);
        }

        [HttpPost]
        public ActionResult ShowItemsToSelect(string ToRole, string ToUser, FunctionAccessManager loadedfamanager)
        {
            famanager = loadedfamanager;
            if (ToRole == null)
            {
                ViewBag.AccessType = "CreateToUser";
                loadedfamanager.AccessTypeUser = true;
                return PartialView("~/Views/Lists/_UsersToSelectAccess.cshtml", loadedfamanager);
            }
            else
            {
                ViewBag.AccessType = "CreateToRole";
                loadedfamanager.AccessTypeUser = false;
                return PartialView("~/Views/Lists/_RolesToSelectAccess.cshtml", loadedfamanager);
            }
        }

        [HttpGet]
        public ActionResult SprEquipment()
        {
            //sprEquipmentRes see = new sprEquipmentRes();
            sprEquipmentEditor see = new sprEquipmentEditor();
            if (System.Web.Security.Roles.IsUserInRole("Механик"))
                return PartialView("~/Views/Lists/SprEquipment.cshtml", see.MechEquipmentDeskBook);
            else if (System.Web.Security.Roles.IsUserInRole("Энергетик"))
                return PartialView("~/Views/Lists/SprEquipment.cshtml", see.EnergyEquipmentDeskBook);
            else
                return PartialView("~/Views/Lists/SprEquipment.cshtml", see);
        }

        [HttpPost]
        public ActionResult SprEquipment(List<sprEquipment> Model)
        {
            //TODO реализовать
            return PartialView();
        }

        [HttpGet]
        public ActionResult SprSHPZ()
        {
            return PartialView("~/Views/Lists/SprSHPZ.cshtml", new SHPZEditor().SHPZList);
        }

        [HttpPost]
        public ActionResult SprSHPZ(List<sprSHPZ> Model)
        {
            //TODO реализовать
            return PartialView();
        }

        [HttpGet]
        public ActionResult NewUser()
        {
            if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "UsersList", AccessType.Create))
            {
                return PartialView("~/Views/Lists/AddNewUser.cshtml", new UserCreator());
            }
            else
            {
                ViewBag.Message = "У вас нет возможности добавлять новых пользователей";
                using (PPREntities ppre = new PPREntities())
                {
                    return PartialView("~/Views/Lists/UsersList.cshtml", ppre.tUsers.ToList());
                }
            }
        }

        [HttpPost]
        public ActionResult NewUser(UserCreator model, int SelectedRoleID)
        {
            if (model.UserPassword==model.UserPasswordConfirm)
            {
                if (model.Create(SelectedRoleID))
                    ViewBag.Message = model.UserFIO + " успешно добавлен";
                else
                    ViewBag.Message = model.ErrorMessage;
            }
            else
                ViewBag.Message="Пользователь не добавлен, т.к. введенные пароли не совпадают";

            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/UsersList.cshtml", ppre.tUsers.ToList());
            }
        }
                
        [HttpGet]
        public ActionResult UserInfo(int SelectedUserID)
        {
            var SelectedUser = new UserInfo();
            SelectedUser.UserInfor(SelectedUserID);
            return PartialView(SelectedUser);
        }

        [HttpGet]
        public ActionResult sprRepairCalculationNorms()
        {
            return PartialView("~/Views/Lists/RepairCalculationNorms.cshtml", new CalculationNormEditor());
        }

        [HttpPost]
        public ActionResult sprRepairCalculationNorms(string AllYears)
        {
            CalculationNormEditor cne = new CalculationNormEditor();
            cne.FilterByYear(AllYears);
            return PartialView("~/Views/Lists/RepairCalculationNorms.cshtml", cne);
        }

        [HttpGet]
        public ActionResult sprRepairWork()
        {
            using(PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/sprRepairWork.cshtml", ppre.sprReairWork.ToList());
            }
        }

        [HttpGet]
        public ActionResult sprRepairCycles()
        {
            return PartialView("~/Views/Lists/sprRepairCycles.cshtml", new sprRepairCylceEditor());            
        }

        [HttpGet]
        public ActionResult sprSeasonRepairDepend()
        {
            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/sprSeasonRepairDepend.cshtml", ppre.vsprSeasonRepairDepend.ToList());
            }
        }

        [HttpGet]
        public ActionResult sprReasonSheduleDeviation()
        {
            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/sprReasonSheduleDeviation.cshtml", ppre.sprReasonSheduleDeviation.ToList());
            }
        }

        [HttpGet]
        public ActionResult sprReactOperationMode()
        {
            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/sprReactOperationMode.cshtml", ppre.sprReactOperationMode.ToList());
            }
        }

        [HttpGet]
        public ActionResult sprNetsSystemsFacility()
        {
            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/sprNetsSystemsFacility.cshtml", ppre.sprNetsSystemsFacility.ToList());
            }
        }

        [HttpGet]
        public ActionResult ChangaUserRole()
        {
            return PartialView(); //доделать
        }

        [HttpGet]
        public ActionResult RoleInfo(string RoleName)
        {
            var SelectedRole = new RoleInfo();
            SelectedRole.RoleInfor(RoleName);
            return PartialView("~/Views/Administrative/RoleInfo.cshtml", SelectedRole);
        }

        [HttpGet]
        public ActionResult sprReactClassSafety()
        {
            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/sprReactClassSafety.cshtml", ppre.sprReactSafetyClasses.ToList());
            }
        }

        [HttpGet]
        public ActionResult sprCodifReactSystems()
        {
            return PartialView("~/Views/Lists/sprCodifReactSystems.cshtml", new CodifReactSystems());
        }

        [HttpGet]
        public ActionResult sprBuildings()
        {
            return PartialView("~/Views/Lists/sprBuildings.cshtml", new BuildingsEditor().Buildings);
        }

        [HttpPost]
        public ActionResult sprBuildings(List<vSubdeptsInBuildings> Model)
        {
            if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "sprBuildings", AccessType.Write))
            {
                BuildingsEditor be = new BuildingsEditor(Model);
                be.UpdateChanges();
                return RedirectToAction("sprBuildings");
            }
            else
            {
                ViewBag.Message = "У вас нет прав для изменения списка зданий.";
                return PartialView("~/Views/Lists/sprBuildings.cshtml", new BuildingsEditor().Buildings);
            }
        }

        [HttpGet]
        public ActionResult sprBrigad()
        {
            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/sprBrigad.cshtml", ppre.sprBrigad.ToList());
            }
        }

        [HttpGet]
        public ActionResult sprReactors()
        {
            using (PPREntities ppre = new PPREntities())
            {
                return PartialView("~/Views/Lists/sprReactors.cshtml", ppre.sprReactors.ToList());
            }
        }

        [HttpPost]
        public ActionResult ChangeRole(int CurrUserID, int SelectedRoleID, UserInfo model)
        {
            var SelectedUser = new UserInfo();
            SelectedUser.UserInfor(CurrUserID);
            if (SelectedUser.RoleID != SelectedRoleID)
            {
                if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "RolesRightsFunctionsList", AccessType.Write))
                {
                    SelectedUser.RoleChange(SelectedRoleID);
                    return PartialView("~/Views/Lists/UserInfo.cshtml", SelectedUser);
                }
                else
                {
                    ViewBag.Message = "У вас нет возможности изменять роли";
                    return PartialView();
                }
            }
            else
            {
                return PartialView();
            }
        }

        [HttpGet]
        public ActionResult ChangeRoleFunc(int CurrUserID)
        {
            var SelectedUser = new UserInfo();
            SelectedUser.UserInfor(CurrUserID);
            return PartialView("~/Views/Lists/_RoleFuncsChangeList.cshtml", SelectedUser);
        }

        [HttpPost]
        public ActionResult ChangeUserFunc(UserInfo model, List<bool> UserAccess, int UserID)
        {
            if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "RolesRightsFunctionsList", AccessType.Write))
            {
                List<bool> normalFuncsList = new List<bool>();
                for (int i = 0; i < UserAccess.Count; i++) // приведение списка к норм. виду
                {
                    normalFuncsList.Add(UserAccess[i]);
                    if (UserAccess[i])
                    {
                        i++;
                    }
                }
                using (PPREntities ppre = new PPREntities())
                {
                    List<tUserFunctions> AllFunctions = new List<tUserFunctions>();
                    AllFunctions = ppre.tUserFunctions.ToList();

                    for (int i = 0; i < normalFuncsList.Count; i++)
                    {
                        FunctionAccessManager faccess = new FunctionAccessManager();
                        faccess.ObjectToGetAccessID = UserID;
                        faccess.SelectedFunctionID = AllFunctions[i].FunctionID;
                        faccess.AccessTypeUser = true;

                        if (normalFuncsList[i])
                        {
                            //Добавление функции доступа для роли
                            faccess.CreateAccess();
                        }
                        else
                        {
                            //Удаление функции доступа для роли
                            faccess.RemoveAccess();
                        }
                    }
                }
                return PartialView();
            }
            else
            {
                ViewBag.Message = "У вас нет возможности изменять функции роли";
                return PartialView();
            }
        }

        [HttpPost]
        public ActionResult ChangeRoleFunc(RoleInfo model, List<bool> RoleAccess, int RoleID)
        {
            if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "RolesRightsFunctionsList", AccessType.Write))
            {
                List<bool> normalFuncsList = new List<bool>();
                for (int i = 0; i < RoleAccess.Count; i++) // приведение списка к норм. виду
                {
                    normalFuncsList.Add(RoleAccess[i]);
                    if (RoleAccess[i])
                    {
                        i++;
                    }
                }
                using (PPREntities ppre = new PPREntities())
                {
                    List<tUserFunctions> AllFunctions = new List<tUserFunctions>();
                    AllFunctions = ppre.tUserFunctions.ToList();

                for (int i = 0; i < normalFuncsList.Count; i++)
                {
                    FunctionAccessManager faccess = new FunctionAccessManager();
                    faccess.ObjectToGetAccessID = RoleID;
                    faccess.SelectedFunctionID = AllFunctions[i].FunctionID;
                    faccess.AccessTypeUser = false;

                    if (normalFuncsList[i])
                    {
                        //Добавление функции доступа для роли
                        faccess.CreateAccess();
                    }
                    else
                    {
                        //Удаление функции доступа для роли
                        faccess.RemoveAccess();
                    }
                }
                }
                return PartialView();
            }
            else
            {
                ViewBag.Message = "У вас нет возможности изменять функции роли";
                return PartialView();
            }
        }

        [HttpGet]
        public ActionResult Print_Akt2()
        {
            return PartialView("~/Views/Lists/Print_Akt2.cshtml");
        }

        [HttpPost]
        public ActionResult Print_Akt2(List<sprSubDept> Model)
        {
            //TODO реализовать !!Пока закоментировал, т.к не подключены модули у меня
            ////using (Report report = new Report())
            ////{
            ////    string tempFile;
            ////    tempFile = Path.GetTempFileName() + ".pdf";
            ////    MemoryStream strm = new MemoryStream(Properties.Resources.Akt2);
            ////    report.Load(strm);
            ////    //report.Show();
            ////    report.Prepare();
            ////    PDFExport pdf = new PDFExport();
            ////    pdf.ShowProgress = false;
            ////    pdf.Subject = "Subject";
            ////    pdf.Title = "Акт2";
            ////    pdf.Compressed = true;
            ////    pdf.AllowPrint = true;
            ////    pdf.EmbeddingFonts = true;
            ////    report.Export(pdf, tempFile);
            ////    report.Dispose();
            ////    pdf.Dispose();
            ////    System.Diagnostics.Process.Start(tempFile);
            ////}
            return PartialView();
        }//sprSubDept

        [HttpGet]
        public ActionResult Print_Akt1()
        {
            return PartialView("~/Views/Lists/Print_Akt1.cshtml");
        }

        [HttpPost]
        public ActionResult Print_Akt1(List<sprSubDept> Model)
        {
            //TODO реализовать !!Пока закоментировал, т.к не подключены модули у меня
            //using (Report report = new Report())
            //{
            //    string tempFile;
            //    tempFile = Path.GetTempFileName() + ".pdf";
            //    MemoryStream strm = new MemoryStream(Properties.Resources.Akt1);
            //    report.Load(strm);
            //    //report.Show();
            //    report.Prepare();
            //    PDFExport pdf = new PDFExport();
            //    pdf.ShowProgress = false;
            //    pdf.Subject = "Subject";
            //    pdf.Title = "Акт1";
            //    pdf.Compressed = true;
            //    pdf.AllowPrint = true;
            //    pdf.EmbeddingFonts = true;
            //    report.Export(pdf, tempFile);
            //    report.Dispose();
            //    pdf.Dispose();
            //    System.Diagnostics.Process.Start(tempFile);

            //    //tempFile = tempFile.Remove(0, 3); 
            //    //Response.WriteFile(Server.MapPath("~"+tempFile));
            //    //System.IO.File.Delete(Server.MapPath("~" + tempFile));

            //    //Response.WriteFile(tempFile);
            //    //Response.Flush();  //передача пользователю
            //    //System.IO.File.Delete(tempFile);

            //}

            return PartialView();
        }//sprSubDept
        [HttpGet]
        public ActionResult Pasport()
        {
            return PartialView("~/Views/Lists/Pasport.cshtml");
        }
        [HttpPost]
        public ActionResult Pasport(List<sprSHPZ> Model)  //sprshpz
        {
            //TODO реализовать
            return PartialView();
        }

        [HttpGet]
        public ActionResult SprFaktTime()
        {
            //var frakt = new sprFaktTime();
            //frakt.sprFaktTimes(null);
            return PartialView("~/Views/Equipment/SprFaktTime.cshtml", new sprFaktTime(null));
        }

        [HttpPost]
        public ActionResult SprFaktTime(List<tFactWorkTime> Model)
        {
            //TODO реализовать
            return PartialView();
        }

        [HttpGet]
        public ActionResult FactWorkTime()
        {
            return RedirectToAction("SprFaktTime");
        }

        [HttpGet]
        public ActionResult MechanicSection()
        {
            return RedirectToAction("GetEquipmentList", "Equipment");
        }

        [HttpGet]
        public ActionResult SearchEquipment()
        {
            return RedirectToAction("SearchEquipment", "Equipment");
        }

        [HttpPost]
        public ActionResult AddNewRepairCalculationNorms()
        {
            CalculationNormEditor cne = new CalculationNormEditor();
            cne.CreateNewQuarterTemplate(2015,1);
            return PartialView("~/Views/Lists/AddNewRepairCalculationNorms.cshtml", cne);
        }

        [HttpPost]
        public ActionResult SaveNewRepairCalculationNorms()
        {            
            return PartialView("~/Views/Lists/RepairCalculationNorms.cshtml");
        }

        [HttpGet]
        public ActionResult vedMTA_inst1()
        {
            return RedirectToAction("vedMTA_inst_adm", "vedomosti");
        }
        [HttpGet]
        public ActionResult vedMTA_SubDept_adm()
        {
            return RedirectToAction("vedMTA_SubDept_adm", "vedomosti");
        }
        [HttpGet]
        public ActionResult VedMTASubDeptBuild()
        {
            return RedirectToAction("VedMTASubDeptBuild", "vedomosti");
        }
        public ActionResult VedMTASubDeptBuild4()
        {
            return RedirectToAction("VedMTASubDeptBuild4", "vedomosti");
        }
        [HttpGet]
        public ActionResult VedEnergoSubDeptBuild()
        {
            return RedirectToAction("VedEnergoSubDeptBuild", "vedomosti");
        }
        [HttpGet]
        public ActionResult VedMA_Group()
        {
            return RedirectToAction("VedMA_Group", "vedomosti");
        }
        [HttpGet]
        public ActionResult VedMT_RU()
        {
            return RedirectToAction("VedMT_RU", "vedomosti");
        }
       
        [HttpGet]
        public ActionResult IssueEquipment()
        {
            return RedirectToAction("GetMovement", "Equipment"); //передача
        }

        [HttpGet]
        public ActionResult SpisanieEquipment() 
        {
            return RedirectToAction("SpisanieOpen", "movement"); //списание
        }
        [HttpGet]
        public ActionResult ArrivalEquipment()
        {
            var model=new Models.Equipment.Equipment();
            return PartialView("~/Views/Equipment/newEquipment.cshtml",model);
        } 
    } 
}
