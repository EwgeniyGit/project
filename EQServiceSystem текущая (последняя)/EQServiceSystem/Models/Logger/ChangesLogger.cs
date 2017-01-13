using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.Models.Logger
{
    public sealed class ChangesLogger
    {
        private List<ChangedRecord> _records = new List<ChangedRecord>();

        private void AddLogRecord()
        {
            if (LoggerConfig.IsLogSavingEnabled)
            {
                using (PPREntities ppre = new PPREntities())
                {
                    foreach (var item in _records)
                    {
                        var record = new logDataChanges();
                        record.tUsers = ppre.tUsers.First(x => x.ID == item.UserID);
                        record.WhatTableChanged = item.ChangedTableName;
                        record.ChangedRecordID = item._ChangedRecordID;
                        record.ChangedColumnName = item._ChangedTableColumnName;
                        record.ChangesDate = DateTime.Now;
                        if (item._OldValueNumber != null)
                            record.OldValueNumber = item._OldValueNumber;
                        if (item._OldValueText != null)
                            record.OldValueText = item._OldValueText;
                        if (item._OldValueFloat != null)
                            record.OldValueFloat = item._OldValueFloat;
                        if (item._OldValueDate != null)
                            record.OldValueDate = item._OldValueDate;
                        record.tChangesTypes = ppre.tChangesTypes.First(x => x.ChangesTypeID == item.TypeChangesID);
                        ppre.logDataChanges.Add(record);
                    }
                    ppre.SaveChanges();
                }
            }
        }

        public void LogOneRecordChange(ChangedRecord record)
        {
            _records.Add(record);
            AddLogRecord();
        }

        public void LogListRecordsChanges(List<ChangedRecord> records)
        {
            _records.AddRange(records);
            AddLogRecord();
        }

        public void TrackChanges(DbChangeTracker tracker)
        {
            var entities = tracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in entities)
            {
                
            }
        }
    }
}