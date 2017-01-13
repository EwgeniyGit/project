using EQDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQServiceSystem.ViewModels
{
    public class Spisanie_Model
    {
        public List<string> SubDepts_sender = new List<string>(); //отправитель
        //public List<string> SubDepts_recipient = new List<string>(); //получатель
        public Spisanie_Model()
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
                //SubDepts_recipient = SubDepts_sender;         
            }
        }
    }
}