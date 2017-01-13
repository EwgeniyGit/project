using EQDataModel;
using EQServiceSystem.Models.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EQServiceSystem.ViewModels
{
    public class history_spisanie_Model
    {
        public List<history_elem> History_List = new List<history_elem>();
        public List<string> All_Type_name_items = new List<string>(); //фильтр по виду
        public List<string> All_Group_name_items = new List<string>();//по группе
        public List<string> All_SubGroup_name_items = new List<string>(); //по подгруппе
        public List<string> All_Marks_name_items = new List<string>();
        public List<string> All_models_items = new List<string>();
        public List<string> SubDept_items = new List<string>();
        public string Nameobr; //наименование   
        public string RegNums; //регистрационный номер
        public string InvNums; //инвентарный номер
        public DateTime beginDate;
        public DateTime endDate;

        public history_spisanie_Model()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var History = ppre.spisanie_history.ToList();
                foreach (var item in History)
                {
                    history_elem elem = new history_elem();
                    elem.ID_sp = item.id_sp;
                    elem.Equipment_ID = item.ID;
                    elem.TypeCode = item.TypeCode;
                    elem.TypeName = item.TypeCode + " " + item.TypeOfEquipment;
                    elem.idGroup = item.idGroup;
                    if (item.idGroup.ToString().Length < 2)
                        elem.GroupName = "0" + item.idGroup + " " + item.GroupName;
                    else
                        elem.GroupName = item.idGroup + " " + item.GroupName;
                    elem.idSubgroup = item.idSubGroup;
                    elem.idSubgroupOld = item.oldValue;
                    elem.SubGroupName = item.oldValue + " " + item.SubGroupName;
                    elem.Mark = item.mark;
                    elem.models = item.model;
                    elem.Data = item.Data;
                    elem.userFIO = item.FIO;
                    elem.SubDeptCode = item.SubDeptCode;
                    elem.SubDeptName = item.SubDeptCode + " " + item.ShortName;
                    elem.NAIMOB = item.NAIMOB;
                    elem.InvNum = item.INWNOM;
                    elem.RegNum = item.REGNO;
                    elem.AgrSignName = item.SignName;
                    elem.Building = item.BuldingName;
                    elem.Room = item.POM;
                    elem.status_sp_bd = item.status_spisanie;
                    elem.status_spisanie = false;
                    History_List.Add(elem);
                }
                History_List=History_List.OrderByDescending(X => X.Data).ToList();
                All_Type_name_items.Add(string.Empty);
                All_Type_name_items.AddRange(History_List.Select(x => x.TypeName).Distinct().OrderBy(x => x));  //тип
                All_Group_name_items.Add(string.Empty);
                All_Group_name_items.AddRange(History_List.Select(x => x.GroupName).Distinct().OrderBy(x => x));  //группы
                All_SubGroup_name_items.Add(string.Empty);
                All_SubGroup_name_items.AddRange(History_List.Select(x => x.SubGroupName).Distinct().OrderBy(x => x)); //подгруппы
                All_Marks_name_items.Add(string.Empty);
                All_Marks_name_items.AddRange(History_List.Select(x => x.models).Distinct().OrderBy(x => x)); //марки
                All_models_items.Add(string.Empty);
                All_models_items.AddRange(History_List.Select(x => x.models).Distinct().OrderBy(x => x)); 
                SubDept_items.Add(string.Empty);
                SubDept_items.AddRange(History_List.Select(x => x.SubDeptName).Distinct().OrderBy(x => x));
            }
        }
        public class history_elem
        {
            public int ID_sp { get; set; }
            public int Equipment_ID { get; set; }
            public string TypeCode { get; set; }
            public string TypeName { get; set; }
            public int idGroup { get; set; }
            public string GroupName { get; set; }
            public string idSubgroupOld { get; set; }
            public int idSubgroup { get; set; }
            public string SubGroupName { get; set; }
            public string Mark { get; set; }
            public string models { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
            public DateTime Data { get; set; }
            public string userFIO { get; set; }
            public int SubDeptCode { get; set; }
            public string SubDeptName { get; set; }
            public string NAIMOB { get; set; }
            public string InvNum { get; set; }
            public string RegNum { get; set; }
            public string AgrSignName { get; set; }
            public string Building { get; set; }
            public string Room { get; set; }
            public bool status_sp_bd { get; set; }
            public bool status_spisanie { get; set; }     
            public history_elem(int ElemID)
            {
                ID_sp = ElemID;
            }
            public history_elem()
            {
            }
        }
    }
}