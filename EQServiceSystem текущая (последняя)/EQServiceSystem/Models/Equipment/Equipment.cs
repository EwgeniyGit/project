using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.Models.Equipment
{
    public class Equipment : NALICH
    {
        public string SubDeptName { get; set; }
        public string BuildingName { get; set; }
        public string W_Name { get; set; }
        public string G_Name { get; set; }
        public string P_Name { get; set; }
        public bool SubordinatedRosTechNadzor { get; set; }
        public string AggregateAttachment { get; set; }
        public string Name2 { get; set; }
        public string SecurityClass { get; set; }
        public string ReactorAttachment { get; set; }
    }
}