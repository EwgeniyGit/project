using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQServiceSystem.Models.Units
{
    public class EquipmentElement
    {
        public int ID { get;  set; }
        public string SubDeptName { get; set; }
        public string BuildingName { get; set; }
        public string Room { get; set; }
        public string RegNumber { get; set; }
        public string InvNumber { get; set; }
        public string AggregateOfType { get; set; }
        public string Name { get; set; }
        public string W { get; set; }
        public string G { get; set; }
        public string P { get; set; }
        public string Mark { get; set; }
        public string GroupName { get; set; }
        public string SubGroupName { get; set; }
        public string TypeName { get;  set; }
        public string TypeCode { get; set; }
        public int idType { get;  set; }
        public int idGroup { get;  set; }
        public int idSubgroup { get;  set; }
        public string RU { get; set; }
        public bool status_spisanie { get; set; }

        public EquipmentElement(int ElemID)
        {
            ID = ElemID;
        }
        public EquipmentElement()
        {
        }
    }
}