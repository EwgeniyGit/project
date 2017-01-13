using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EQDataModel;
using EQServiceSystem.Models;
using EQServiceSystem.Models.Logger;
using WebMatrix.WebData;

namespace EQServiceSystem.ViewModels
{
    public class SubDeptsEditor
    {
        public List<sprSubDept> SubDeptsList = new List<sprSubDept>();
        private List<ChangedRecord> Changes = new List<ChangedRecord>();

        public SubDeptsEditor()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var items = ppre.sprSubDept.ToList();
                SubDeptsList.AddRange(items);
            }
        }

        public SubDeptsEditor(List<sprSubDept> SubDepts)
        {
            SubDeptsList = SubDepts;
        }

        public void UpdateChanges()
        {
            SaveUpdatedRecords();
            UpdateChangesLog();
        }

        private void SaveUpdatedRecords()
        {
            using (var ppre = new PPREntities())
            {
                foreach (var item in SubDeptsList)
                {
                    var changedItem = (from items in ppre.sprSubDept
                                       where items.IdSubDept == item.IdSubDept
                                       select items).FirstOrDefault();
                    if ((changedItem.SubDeptCode != item.SubDeptCode) || (changedItem.SubDeptName != item.SubDeptName) || (changedItem.ShortName != item.ShortName))
                    {
                        Changes.Add(new ChangedRecord(WebSecurity.CurrentUserId, "sprSubDept", item.IdSubDept, "SubDeptCode", changedItem.SubDeptCode, ChangesTypes.Изменение));
                        Changes.Add(new ChangedRecord(WebSecurity.CurrentUserId, "sprSubDept", item.IdSubDept, "SubDeptName", changedItem.SubDeptName, ChangesTypes.Изменение));
                        Changes.Add(new ChangedRecord(WebSecurity.CurrentUserId, "sprSubDept", item.IdSubDept, "SubDeptName", changedItem.AdjustmentDate, ChangesTypes.Изменение));
                        Changes.Add(new ChangedRecord(WebSecurity.CurrentUserId, "sprSubDept", item.IdSubDept, "ShortName", changedItem.ShortName, ChangesTypes.Изменение));
                        changedItem.SubDeptCode = item.SubDeptCode;
                        changedItem.SubDeptName = item.SubDeptName;
                        changedItem.AdjustmentDate = DateTime.Now;
                    }
                }
                ppre.SaveChanges();
            }
        }

        private void UpdateChangesLog()
        {
            ChangesLogger logger = new ChangesLogger();
            logger.LogListRecordsChanges(Changes);
        }
    }
}