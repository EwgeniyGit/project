﻿using EQServiceSystem.Models.Units;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;

namespace EQServiceSystem.ViewModels
{
    public class PageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
    public class AdminEquipmentRU
    {
        public List<EquipmentElement> Equipment { get; set; }
        public List<string> All_Type_name_items = new List<string>();
        public List<string> All_Group_name_items = new List<string>();
        public List<string> All_SubGroup_name_items = new List<string>();
        public List<string> All_Marks_name_items = new List<string>();
        public List<string> RU = new List<string>();
        public IPagedList<EquipmentElement> PagedEquipment { get; set; }
        public IEnumerable<EquipmentElement> Equip { get; set; }
        public PageInfo PageInfo { get; set; }
        public string nameL { get; set; }//фильтр названия
        public string InvNums;
        public string RegNums;
    }
}