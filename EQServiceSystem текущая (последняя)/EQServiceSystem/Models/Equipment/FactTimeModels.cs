using EQDataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EQServiceSystem.Models
{
    public class FactTimeModels : DbContext
    {
        public List<MyData> Na { get; set; }
        public string EquipmentRegNum { get; set; }
        public string FactWorkHours { get; set; }
        public string AverageStatHours { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime? LastPeriodStart { get; set; }
        public DateTime? LastPeriodEnd { get; set; }
        public int UserID { get; set; }
        public List<int> PodrID { get; set; }
        int podr { get; set; }
        public string MSG { get; set; }
        public FactTimeModels()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var cur_user = WebSecurity.CurrentUserName;
                //var role = System.Web.Security.Roles.GetRolesForUser(cur_user).Single();
                RightsChecker CheckedObject = new RightsChecker();
                if (CheckedObject.HasRightsTo(WebSecurity.CurrentUserId, "AllLists", AccessType.Read))
                {
                    Na = (from Items in ppre.NALICH
                          join Table in ppre.tAggregateCodifer on Items.AggregateCode equals Table.SignID
                          where Items.REGNO != null
                          select new MyData
                          {
                              Name = Items.NAIMOB,
                              EquipmentRegNum = Items.REGNO,
                              Type = Table.SignName,
                              TypeID = Table.SignID,
                              EquipmentID = Items.ID
                          }).ToList();
                }
                else
                {
                    UserID = WebSecurity.CurrentUserId;
                    PodrID = (from Items in ppre.tUsers
                              join SubDepth in ppre.sprSubDept on Items.SubDeptID equals SubDepth.IdSubDept
                              where Items.ID == UserID
                              select SubDepth.IdSubDept).ToList();
                    podr = PodrID[0];
                    Na = (from Items in ppre.NALICH
                          join Table in ppre.tAggregateCodifer on Items.AggregateCode equals Table.SignID
                          where Items.REGNO != null && Items.IdSubDept == podr
                          select new MyData
                          {
                              Name = Items.NAIMOB,
                              EquipmentRegNum = Items.REGNO,
                              Type = Table.SignName,
                              TypeID = Table.SignID,
                              EquipmentID = Items.ID
                          }).ToList();
                }
                for (int i = 0; i < Na.Count; i++)
                {
                    Na[i].Replace(Na[i].Name);
                }
                Na.Sort(delegate(MyData Num1, MyData Num2)
                { return Num1.EquipmentRegNum.CompareTo(Num2.EquipmentRegNum); });
            }

        }
        public class MyData
        {
            public string Name { get; set; }
            public string EquipmentRegNum { get; set; }
            public string Type { get; set; }
            public int TypeID { get; set; }
            public int EquipmentID { get; set; }
            public void Replace(string Names)
            {
                Name = System.Text.RegularExpressions.Regex.Replace(Names, " +", " "); //удаление пробелов в наименовании
            }
        }
    }
}