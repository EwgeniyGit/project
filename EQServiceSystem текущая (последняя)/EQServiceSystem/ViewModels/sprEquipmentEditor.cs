using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;
using PagedList;

namespace EQServiceSystem.ViewModels
{
    public class sprEquipmentEditor
    {
        //public List<sprEquipment> EquipmentDeskBook { get; set; }
        public List<EquipmentElementRes> EquipmentDeskBook { get; set; }
        public List<string> Type_items = new List<string>();
        public List<string> Group_items = new List<string>();
        public List<string> SubGroup_items = new List<string>();
        public List<string> Marks_items = new List<string>();
        public List<string> Model_items = new List<string>();
        public IPagedList<EquipmentElementRes> PagedEquipment { get; set; }
        public string InvNums;
        public string RegNums;
        //public IEnumerable<EquipmentElementRes> Equip { get; set; }
        public PageInfo PageInfo { get; set; }
        //public sprEquipmentEditor()
        //{
        //    using (PPREntities ppre = new PPREntities())
        //    {
        //        EquipmentDeskBook = (from eq in ppre.sprEquipment
        //                             orderby eq.W, eq.EquipmentGroup, eq.SubGroup, eq.Model
        //                             select eq).ToList();
        //    }
        //}

        //public List<sprEquipment> EnergyEquipmentDeskBook
        //{
        //    get
        //    {
        //        return (from items in EquipmentDeskBook
        //                    where (items.W == "А") || (items.W == "Э")
        //                    select items).ToList();
        //    }
        //}

        //public List<sprEquipment> MechEquipmentDeskBook
        //{
        //    get
        //    {
        //        return (from items in EquipmentDeskBook
        //                where items.W != "Э"
        //                select items).ToList();
        //    }
        //}
        
        public sprEquipmentEditor()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var Equip = (from tTypeISGroup in ppre.TypeISGroup
                          join tTypeOfEq in ppre.sprTypeOfEquipment on tTypeISGroup.idType equals tTypeOfEq.ID
                          join tGroup in ppre.sprGroup on tTypeISGroup.idGroup equals tGroup.idGroup
                          join tSubGroup in ppre.sprSubGroup on tTypeISGroup.idSubGroup equals tSubGroup.idSubGroup
                          join tMark in ppre.sprMark on tTypeISGroup.idMark equals tMark.id_mark
                          join tModel in ppre.sprModel on tTypeISGroup.idModel equals tModel.id_model
                          select new
                          {
                              tTypeISGroup.id,
                              tTypeOfEq.TypeOfEquipment, //полное наименование
                              tTypeOfEq.TypeCode,
                              tGroup.GroupName,
                              tGroup.idGroup,
                              tSubGroup.SubGroupName,
                              tSubGroup.idSubGroup,
                              tMark.mark,
                              tModel.model,
                              tTypeISGroup.oldValue,
                              tTypeOfEq.ID,
  
                          }).OrderBy(x=>x.TypeCode).ThenBy(x=>x.idGroup).ThenBy(x=>x.oldValue).ThenBy(x=>x.model).ThenBy(x=>x.mark);
                EquipmentDeskBook= new List<EquipmentElementRes>();
                 foreach (var item in Equip)
                 {
                     var elem = new EquipmentElementRes(item.id);
                     elem.TypeCode = item.TypeCode;
                     elem.TypeName = item.TypeCode + " " + item.TypeOfEquipment;
                     if (item.idGroup.ToString().Length < 2)
                         elem.GroupName = "0" + item.idGroup + " " + item.GroupName;
                     else
                         elem.GroupName = item.idGroup + " " + item.GroupName;
                     //elem.GroupName = item.GroupName;
                     //elem.SubGroupName = item.SubGroupName;
                     elem.SubGroupName = item.oldValue + " " + item.SubGroupName;
                     if ((item.model!=null)&&(item.model.Length < 2))
                         elem.Model = "0" + item.model;
                     else
                         elem.Model = item.model;
                     //elem.Model = item.model;
                     elem.Mark = item.mark;
                     elem.idType = item.ID;
                     elem.idGroup = item.idGroup;
                     elem.idSubgroup = item.idSubGroup;
                     elem.OldSubgroup = item.oldValue;
                     elem.GroupNamePrint = item.GroupName;
                     elem.SubGroupNamePrint = item.SubGroupName;  
                     EquipmentDeskBook.Add(elem);
                 }
                 EquipmentDeskBook.OrderBy(x => x.TypeCode).OrderBy(z => z.idGroup).OrderBy(v => v.idSubgroup).OrderBy(n => n.Model).OrderBy(m => m.Mark);
                 PagedEquipment = PagedSelection(EquipmentDeskBook, 1, 100);
                 Type_items.Add(string.Empty);
                 Type_items.AddRange(EquipmentDeskBook.Select(x => x.TypeName).Distinct().OrderBy(x => x));
                 Group_items.Add(string.Empty);
                 Group_items.AddRange(EquipmentDeskBook.Select(x => x.GroupName).Distinct().OrderBy(x => x));
                 SubGroup_items.Add(string.Empty);
                 SubGroup_items.AddRange(EquipmentDeskBook.Select(x => x.SubGroupName).Distinct().OrderBy(x => x));
                 Marks_items.Add(string.Empty);
                 Marks_items.AddRange(EquipmentDeskBook.Select(x => x.Mark).Distinct().OrderBy(x => x));
                 Model_items.Add(string.Empty);
                 Model_items.AddRange(EquipmentDeskBook.Select(x => x.Model).Distinct().OrderBy(x => x));
                 Model_items = SortList(Model_items.ToList());
            }
        }
        public List<string> SortList(List<string> list) //сортировка выпадающих списков с кодами
        {
            List<Tuple<string, string>> countries = new List<Tuple<string, string>>();
            List<Tuple<int, string>> Icountries = new List<Tuple<int, string>>();
            List<string> NewList = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i] != string.Empty) && (list[i] != " ") && (list[i] != null))
                {
                    var code = list[i].Split(' ')[0];
                    var name = list[i].Substring(code.Length, list[i].Length - code.Length);
                    //Tuple<string, string> str = new Tuple<string, string>(code, name);
                    Tuple<int, string> strI = new Tuple<int, string>(Convert.ToInt32(code), name);
                    //countries.Add(str);
                    Icountries.Add(strI);
                }
                else
                {
                    Tuple<string, string> str = new Tuple<string, string>(list[i], list[i]);
                    countries.Add(str);
                }

            }
            foreach (var item in countries)
            {
                if ((item.Item1 == string.Empty) || (item.Item1 == " ") || (item.Item1 == null))
                    NewList.Add(item.Item1);
                //else
                //NewList.Add(item.Item1+item.Item2);
            }
            Icountries.Sort(delegate(Tuple<int, string> us1, Tuple<int, string> us2)
            { return us1.Item1.CompareTo(us2.Item1); }); ;
            foreach (var item in Icountries)
            {
                if (item.Item1.ToString().Length < 2)
                    NewList.Add("0" + item.Item1.ToString() + item.Item2.ToString());
                else
                    NewList.Add(item.Item1.ToString() + item.Item2.ToString());
            }
            return NewList;
        }
        public IPagedList<EquipmentElementRes> EnergyEquipmentDeskBook
        {
            get
            {
                var e= (from items in EquipmentDeskBook
                        where (items.TypeCode == "А") || (items.TypeCode == "Э")
                        select items).ToList();
                //return EquipmentDeskBook = e;
                PagedEquipment = PagedSelection(EquipmentDeskBook, 1, 100);
                return PagedEquipment;
            }        
        }

        public IPagedList<EquipmentElementRes> MechEquipmentDeskBook
        {
            get
            {
                var e= (from items in EquipmentDeskBook
                        where items.TypeCode != "Э"
                        select items).ToList();
                PagedEquipment = PagedSelection(EquipmentDeskBook, 1, 100);          
                return PagedEquipment;
                //return EquipmentDeskBook = e;
            }
        }

        public class EquipmentElementRes
        {
            public int ID { get; set; }
            public string Mark { get; set; }
            public string GroupName { get; set; }
            public string SubGroupName { get; set; }
            public string TypeName { get; set; }
            public string TypeCode { get; set; }
            public int idType { get; set; }
            public int idGroup { get; set; }
            public int idSubgroup { get; set; }
            public string Model { get; set; }
            public string OldSubgroup { get; set; }
            public string GroupNamePrint { get; set; }
            public string SubGroupNamePrint { get; set; }     
            public EquipmentElementRes(int ElemID)
            {
                ID = ElemID;
            }
        }
        public IPagedList<EquipmentElementRes> PagedSelection(List<EquipmentElementRes> EquipmentDeskBook, int page, int pageSize)
        {
            IPagedList<EquipmentElementRes> PagedEquipment;
            IEnumerable<EquipmentElementRes> Equipments = EquipmentDeskBook.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = EquipmentDeskBook.Count };
            PagedEquipment = EquipmentDeskBook.ToPagedList(page, PageInfo.PageSize);
            if (PagedEquipment.Count <= pageSize) PageInfo.PageSize = PagedEquipment.Count;
            return PagedEquipment;
        }
    }
}