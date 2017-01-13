using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;
using EQServiceSystem.Models.Equipment;
using System.Data.Objects.SqlClient;

namespace EQServiceSystem.Models.ElectronicArchives
{
    public class ElectronicArchives
    {
        public int? Seed { get; set; } //Корневой элемент
        public List<ElectronicArchive> ElectronicArchive { get; set; } //Список 
        public EQServiceSystem.Models.Equipment.Equipment Unit = new EQServiceSystem.Models.Equipment.Equipment();
        public ElectronicArchives(int EquipmentID)
        {
            using (PPREntities ppre = new PPREntities())
            {
                var item = (from items in ppre.NALICH
                            where items.ID == EquipmentID
                            select items).FirstOrDefault();
                Unit.ID = item.ID;
                Unit.REGNO = item.REGNO;
                Unit.INWNOM = item.INWNOM;
                Unit.SubDeptName = item.sprSubDept.SubDeptName;
                Unit.BuildingName = item.sprBuildings.BuldingName;
                Unit.POM = item.POM;
                Unit.DWWOD = item.DWWOD;
                Unit.ZAWOD = item.ZAWOD;
                Unit.ZAWNO = item.ZAWNO;
                Unit.GODWYP = item.GODWYP;
                Unit.OBOZN = item.OBOZN;
                Unit.STOIM = item.STOIM;
                Unit.W = item.W;
                Unit.W_Name = ppre.sprTypeOfEquipment.FirstOrDefault(x => x.TypeCode == item.W).TypeOfEquipment;
                Unit.G = item.G;

                int g = Int32.Parse(item.G);
                Unit.G_Name = (from items in ppre.sprEquipment
                              where items.EquipmentGroup == g
                              select items.GroupName).FirstOrDefault();
                
                Unit.P = item.P;
                int p;
                if (Int32.TryParse(item.P, out p))
                    Unit.P_Name = (from items in ppre.sprEquipment
                                where (items.EquipmentGroup == g) && (items.SubGroup == p)
                                select items.SubGroupName).FirstOrDefault();

                Unit.M = item.M;
                Unit.MARKA = item.MARKA;
                Unit.AggregateAttachment = ppre.tAggregateCodifer.FirstOrDefault(x => x.SignID == item.AggregateCode).SignName;
                int aggregateCode = ppre.tAggregateCodifer.FirstOrDefault(x => x.SignID == item.AggregateCode).SignID;
                if ((aggregateCode == 2) || (aggregateCode == 3))
                    Unit.Name2 = "Добавить наименование агрегата"; //TODO дописать
                else
                    Unit.Name2 = string.Empty;

                Unit.NAIMOB = item.NAIMOB;
                Unit.SHIFWR = item.SHIFWR;
                Unit.KUSM = item.KUSM;
                Unit.KUSE = item.KUSE;
                Unit.KATM = item.KATM;
                Unit.KATE = item.KATE;
                Unit.TRUDK = item.TRUDK;
                Unit.TRUDT = item.TRUDT;
                Unit.TRUDTO = item.TRUDTO;
                Unit.DPR = item.DPR;
                Unit.WPR = item.WPR;
                Unit.NPR = item.NPR;
                Unit.WUST = item.WUST;
                Unit.KSUST = item.KSUST;
                Unit.KPUST = item.KPUST;

                Unit.NTEX = item.NTEX; //Если указан номер - значит оборудование подведомственное Ростехнадзору, иначе - нет
                if (Unit.NTEX != null)
                    Unit.SubordinatedRosTechNadzor = true;
                else
                    Unit.SubordinatedRosTechNadzor = false;

                Unit.SPOSK = item.SPOSK;
                Unit.SPOST = item.SPOST;
                Unit.SPOSTO = item.SPOSTO;
                Unit.SMEN = item.SMEN;
                Unit.WRASCH = item.WRASCH;
                Unit.KOLKSTR = item.KOLKSTR;
                Unit.KOLTSTR = item.KOLTSTR;
                Unit.KOLOSTR = item.KOLOSTR;
                Unit.PER = item.PER;
                Unit.ImportantToSafety = item.ImportantToSafety;
                Unit.SubjectToInspection = item.SubjectToInspection;
                Unit.MainOGE = item.MainOGE;

                if (Unit.ImportantToSafety)
                {
                    Unit.SecurityClass = (from c in ppre.tImportantEquipment
                                          where c.EquipmentID == Unit.ID
                                          select c.sprReactSafetyClasses.ClassName).FirstOrDefault();
                    Unit.ReactorAttachment = (from r in ppre.tImportantEquipment
                                              where r.EquipmentID == Unit.ID
                                              select r.sprReactors.Name).FirstOrDefault();
                }

                Unit.KOLKAP = item.KOLKAP;
                Unit.DPKAP = item.DPKAP;
                Unit.KOLTEK = item.KOLTEK;
                //var Docs = ppre.ElectronicArchive_subordinate.Where(x => !x.IsDeleted).ToArray().Select(x => ppre.ElectronicArchive);
                //foreach (var it in ppre.ElectronicArchive)
                //{ 
                //    NewsModel nm = new NewsModel();
                //    nm.Id = it.id_doc;
                //    nm.Title = it.type_doc_main;        
                //}   
                //ElectronicArchive = ppre.ElectronicArchive.ToList();
            }
        }
    }
}