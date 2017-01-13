using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;
using EQServiceSystem.Models;
using EQServiceSystem.Models.Logger;
using WebMatrix.WebData;

namespace EQServiceSystem.ViewModels
{
    public class BuildingsEditor
    {
        public List<vSubdeptsInBuildings> Buildings { get; set; }
        private List<ChangedRecord> Changes = new List<ChangedRecord>();

        public BuildingsEditor()
        {
            Buildings = new List<vSubdeptsInBuildings>();
            using (PPREntities ppre = new PPREntities())
            {
                Buildings = (from builds in ppre.vSubdeptsInBuildings
                             orderby builds.BuldingName
                             where builds.NotAvailable == false
                             select builds).ToList();
            }
        }

        public BuildingsEditor(List<vSubdeptsInBuildings> BuildingsUpdated)
        {
            Buildings = new List<vSubdeptsInBuildings>();
            Buildings.AddRange(BuildingsUpdated);
        }

        public void UpdateChanges()
        {
            SaveUpdatedRecords();
            UpdateChangesLog();
        }

        private void SaveUpdatedRecords()
        {
            using (PPREntities ppre = new PPREntities())
            {
                foreach (var item in Buildings)
                {
                    var changedItem = (from items in ppre.sprBuildings
                                       where items.BuildingID == item.BuildingID
                                       select items).FirstOrDefault();
                    if ((changedItem.BuldingName != item.BuldingName) || (changedItem.NotAvailable != item.NotAvailable))
                    {
                        if (Changes.FindIndex(x => x._ChangedRecordID == changedItem.BuildingID) < 0) //т.к записей для одного здания может быть несколько (когда в здании несколько подразделений) проверяем на наличие дубликатов
                        {
                            Changes.Add(new ChangedRecord(WebSecurity.CurrentUserId, "sprBuildings", item.BuildingID, "BuldingName", changedItem.BuldingName, ChangesTypes.Изменение));
                            //Changes.Add(new ChangedRecord(WebSecurity.CurrentUserId, "sprBuildings", item.BuildingID, "NotAvailable", changedItem.NotAvailable, ChangesTypes.Изменение));
                            changedItem.BuldingName = item.BuldingName;
                            //changedItem.NotAvailable = item.NotAvailable;
                        }
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