using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.ViewModels
{
    public struct RepairCycleInfo
    {
        public int ID;
        public int EquipmentID;
        public string EquimpentName;
        public string EquipmentGroup;
        public int? MajorCoeff;
        public int? CurrentCoeff;
        public int? InspectionCoeff;
        public int? Period;
    }

    public class sprRepairCylceEditor
    {
        public List<RepairCycleInfo> RepairCycleItems = new List<RepairCycleInfo>();

        public sprRepairCylceEditor()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var items = (from spr in ppre.sprRepairCycles
                             select spr).ToList();

                foreach (var item in items)
                {
                    var element = new RepairCycleInfo();
                    element.ID = item.ID;
                    element.EquipmentID = item.EquipmentID;
                    element.EquimpentName = item.EquipmentName;
                    element.EquipmentGroup = item.sprEquipment.GroupName;
                    element.MajorCoeff = item.MajorCoeff;
                    element.CurrentCoeff = item.CurrentCoeff;
                    element.InspectionCoeff = item.InspectionCoeff;
                    element.Period = item.Period;
                    RepairCycleItems.Add(element);
                }
            }
        }
        
    }
}