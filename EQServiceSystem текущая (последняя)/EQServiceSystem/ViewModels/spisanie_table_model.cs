using EQDataModel;
using EQServiceSystem.Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQServiceSystem.ViewModels
{
    public class spisanie_table_model:Spisanie_Model
    {
        public List<EquipmentElement> Equipment { get; set; } //весь список
        public List<string> All_Type_name_items = new List<string>(); //фильтр по виду
        public List<string> All_Group_name_items = new List<string>();//по группе
        public List<string> All_SubGroup_name_items = new List<string>(); //по подгруппе
        public List<string> AggregatCodifer = new List<string>(); //принадлежность к агрегату
        public string RegNums; //регистрационный номер
        public string InvNums; //инвентарный номер
        public string Nameobr; //наименование   
        public spisanie_table_model()
        {
            Equipment = EquipmentFullList();
        }
        private List<EquipmentElement> EquipmentFullList()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var Eq = (from items in ppre.NALICH
                          join tTypeISGroup in ppre.TypeISGroup on items.id_TypeIsGroup equals tTypeISGroup.id
                          join tTypeOfEq in ppre.sprTypeOfEquipment on tTypeISGroup.idType equals tTypeOfEq.ID
                          join tGroup in ppre.sprGroup on tTypeISGroup.idGroup equals tGroup.idGroup
                          join tSubGroup in ppre.sprSubGroup on tTypeISGroup.idSubGroup equals tSubGroup.idSubGroup
                          //join tMark in ppre.sprMark on tTypeISGroup.idMark equals tMark.id_mark
                          //join tModel in ppre.sprModel on tTypeISGroup.idModel equals tModel.id_model
                          where items.status_spisanie != false
                          select new
                          {
                              items.ID,
                              items.sprSubDept.ShortName,
                              items.sprSubDept.SubDeptCode,
                              //items.sprBuildings.BuldingName,
                              items.REGNO,
                              items.INWNOM,
                              items.tAggregateCodifer.SignCode,
                              items.NAIMOB,
                              items.W,
                              items.G,
                              items.P,
                              //items.POM,
                              items.MARKA,
                              tTypeOfEq.TypeOfEquipment,
                              tGroup.GroupName,
                              tSubGroup.SubGroupName,
                              //tMark.mark,
                              //tModel.model,
                              tTypeOfEq.TypeCode,
                              tTypeISGroup.oldValue,
                              tGroup.idGroup,
                              items.status_spisanie
                          }).ToList();
                var Equipment = new List<EquipmentElement>();
                foreach (var item in Eq)
                {
                    var elem = new EquipmentElement(item.ID);
                    elem.AggregateOfType = item.SignCode;
                    //elem.BuildingName = item.BuldingName;
                    elem.InvNumber = item.INWNOM;
                    elem.Name = item.NAIMOB;
                    elem.RegNumber = item.REGNO;
                    elem.W = item.W;
                    elem.G = item.G;
                    elem.P = item.P;
                    //elem.Room = item.POM;
                    //elem.Mark = item.MARKA;
                    elem.TypeName = item.TypeCode + " " + item.TypeOfEquipment;
                    if (item.idGroup.ToString().Length < 2)
                        elem.GroupName = "0" + item.idGroup + " " + item.GroupName;
                    else
                        elem.GroupName = item.idGroup + " " + item.GroupName;
                    //elem.GroupName = item.idGroup + " " + item.GroupName;
                    elem.TypeCode = item.TypeCode;
                    elem.SubGroupName = item.oldValue + " " + item.SubGroupName;
                    elem.SubDeptName = item.SubDeptCode + " " + item.ShortName;
                    elem.status_spisanie = item.status_spisanie;
                    Equipment.Add(elem);
                }
                return Equipment;
            }
        }
    }
}