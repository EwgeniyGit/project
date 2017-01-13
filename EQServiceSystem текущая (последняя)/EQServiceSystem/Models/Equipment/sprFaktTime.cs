using EQDataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace EQServiceSystem.Models
{
    public class sprFaktTime : System.Data.Entity.DbContext
    {
        //public List<tFactWorkTime> FactWorkTimeList { get; set; }
        public List<myFaktTime> myFactWorkTimeList { get; set; }
        public int UserID { get; set; }
        public List<int> PodrID { get; set; }
        int podr { get; set; }
        public List<string> PodrName { get; set; }
        public string MSG { get; set; }
        public sprFaktTime(string rn)
        {
            using (PPREntities ppre = new PPREntities())
            {
                UserID = WebSecurity.CurrentUserId;
                var cur_user = WebSecurity.CurrentUserName;
                RightsChecker CheckedObject = new RightsChecker();
                PodrID = (from Items in ppre.tUsers
                          join SubDepth in ppre.sprSubDept on Items.SubDeptID equals SubDepth.IdSubDept
                          where Items.ID == UserID
                          select SubDepth.IdSubDept).ToList();
                podr = PodrID[0];
                PodrName = (from Items in ppre.sprSubDept
                            where Items.IdSubDept == podr
                            select Items.ShortName).ToList();
                if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "AllLists", AccessType.Read))
                {
                    if (rn != null)
                    {
                        myFactWorkTimeList = (from Items in ppre.tFactWorkTime
                                              join Table in ppre.NALICH on Items.EquipmentID equals Table.ID
                                              join Table2 in ppre.tAggregateCodifer on Table.AggregateCode equals Table2.SignID
                                              where (Items.RecordDate.Year >= DateTime.Now.Year) &&
                                              (Items.EquipmentRegNum == rn)
                                              orderby Items.EquipmentRegNum
                                              select new myFaktTime
                                              {
                                                  Id = Items.RecordID,
                                                  EquipmentRegNum = Items.EquipmentRegNum,
                                                  FactWorkHours = Items.FactWorkHours,
                                                  AverageStatHours = Items.AverageStatHours,
                                                  RecordDate = Items.RecordDate,
                                                  SignName = Table2.SignName,
                                                  NAIMOB = Table.NAIMOB,
                                                  IdSubDepth = Table.IdSubDept,
                                                  EquipmentID = Items.EquipmentID,
                                                  LastPeriodStart = Items.LastPeriodStart,
                                                  LastPeriodEnd = Items.LastPeriodEnd
                                              }).ToList();
                    }
                    else
                    {
                        myFactWorkTimeList = (from Items in ppre.tFactWorkTime
                                              join Table in ppre.NALICH on Items.EquipmentID equals Table.ID
                                              join Table2 in ppre.tAggregateCodifer on Table.AggregateCode equals Table2.SignID
                                              where (Items.RecordDate.Year >= DateTime.Now.Year)
                                              orderby Items.EquipmentRegNum
                                              select new myFaktTime
                                              {
                                                  Id = Items.RecordID,
                                                  EquipmentRegNum = Items.EquipmentRegNum,
                                                  FactWorkHours = Items.FactWorkHours,
                                                  AverageStatHours = Items.AverageStatHours,
                                                  RecordDate = Items.RecordDate,
                                                  SignName = Table2.SignName,
                                                  NAIMOB = Table.NAIMOB,
                                                  IdSubDepth = Table.IdSubDept,
                                                  EquipmentID = Items.EquipmentID,
                                                  LastPeriodStart = Items.LastPeriodStart,
                                                  LastPeriodEnd = Items.LastPeriodEnd
                                              }).ToList();
                    }
                }
                else
                {
                    if (rn != null)
                    {
                        myFactWorkTimeList = (from Items in ppre.tFactWorkTime
                                              join Table in ppre.NALICH on Items.EquipmentID equals Table.ID
                                              join Table2 in ppre.tAggregateCodifer on Table.AggregateCode equals Table2.SignID
                                              where (Items.RecordDate.Year >= DateTime.Now.Year) &&
                                              (Items.EquipmentRegNum == rn) &&
                                              (Table.IdSubDept == podr)
                                              orderby Items.EquipmentRegNum
                                              select new myFaktTime
                                              {
                                                  Id = Items.RecordID,
                                                  EquipmentRegNum = Items.EquipmentRegNum,
                                                  FactWorkHours = Items.FactWorkHours,
                                                  AverageStatHours = Items.AverageStatHours,
                                                  RecordDate = Items.RecordDate,
                                                  SignName = Table2.SignName,
                                                  NAIMOB = Table.NAIMOB,
                                                  IdSubDepth = Table.IdSubDept,
                                                  EquipmentID = Items.EquipmentID,
                                                  LastPeriodStart = Items.LastPeriodStart,
                                                  LastPeriodEnd = Items.LastPeriodEnd,
                                              }).ToList();
                    }
                    else
                    {
                        myFactWorkTimeList = (from Items in ppre.tFactWorkTime
                                              join Table in ppre.NALICH on Items.EquipmentID equals Table.ID
                                              join Table2 in ppre.tAggregateCodifer on Table.AggregateCode equals Table2.SignID
                                              where (Items.RecordDate.Year >= DateTime.Now.Year) && (Table.IdSubDept == podr)
                                              orderby Items.EquipmentRegNum
                                              select new myFaktTime
                                              {
                                                  Id = Items.RecordID,
                                                  EquipmentRegNum = Items.EquipmentRegNum,
                                                  FactWorkHours = Items.FactWorkHours,
                                                  AverageStatHours = Items.AverageStatHours,
                                                  RecordDate = Items.RecordDate,
                                                  SignName = Table2.SignName,
                                                  NAIMOB = Table.NAIMOB,
                                                  IdSubDepth = Table.IdSubDept,
                                                  EquipmentID = Items.EquipmentID,
                                                  LastPeriodStart = Items.LastPeriodStart,
                                                  LastPeriodEnd = Items.LastPeriodEnd
                                              }).ToList();
                    }
                }
                for (int i = 0; i < myFactWorkTimeList.Count; i++)
                {
                    myFactWorkTimeList[i].Replace(myFactWorkTimeList[i].NAIMOB);
                }
            }
        }
        public class myFaktTime
        {
            public int Id { get; set; }
            public string EquipmentRegNum { get; set; }
            public int FactWorkHours { get; set; }
            public int AverageStatHours { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
            public DateTime RecordDate { get; set; }
            public string SignName { get; set; }
            public string NAIMOB { get; set; }
            public int IdSubDepth { get; set; }
            public int EquipmentID { get; set; }
            //[DataType(DataType.Date)]
            //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? LastPeriodStart { get; set; }
            //[DataType(DataType.Date)]
            //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? LastPeriodEnd { get; set; }
            public void Replace(string NaimOb)
            {
                NAIMOB = System.Text.RegularExpressions.Regex.Replace(NaimOb, " +", " "); //удаление пробелов в наименовании
            }

        }
    }
}