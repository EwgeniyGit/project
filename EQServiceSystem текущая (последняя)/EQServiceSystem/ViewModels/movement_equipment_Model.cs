using EQDataModel;
using EQServiceSystem.Models.Units;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQServiceSystem.ViewModels
{
    public class movement_equipment_Model
    {
        public List<string> SubDepts_sender = new List<string>(); //отправитель
        public List<string> SubDepts_recipient = new List<string>(); //получатель
        //public List<string> All_Type_name_items = new List<string>(); //фильтр по виду
        //public List<string> All_Group_name_items = new List<string>();//по группе
        //public List<string> All_SubGroup_name_items = new List<string>(); //по подгруппе
        //public string nameL { get; set; } //фильтр по наименованию
        //public List<string> SubDepts = new List<string>(); //по подраделению
        //public List<string> RegNum = new List<string>(); //по регистрационному номеру
        //public List<string> InvNum= new List<string>(); //по инвентарному номеру
        //public PageInfo PageInfo { get; set; }
        //public System.Collections.IEnumerable Equipments { get; set; }

        public movement_equipment_Model()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var SubDeptsList = (from Items in ppre.sprSubDept
                                    select new
                                    {
                                        Items.SubDeptCode,
                                        Items.ShortName
                                    }).ToList();
                SubDepts_sender.Add("ОБЩИЙ СПИСОК");
                foreach (var item in SubDeptsList)
                {
                    SubDepts_sender.Add(item.SubDeptCode + " " + item.ShortName);
                }
                SubDepts_recipient = SubDepts_sender;         
            }
        }
       
        
        public movement_equipment_Model(int EquipmentID)//для конкретного оборудования
        { }
    }
    
}