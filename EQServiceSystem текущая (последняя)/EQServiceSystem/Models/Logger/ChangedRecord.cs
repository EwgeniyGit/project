using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.Models.Logger
{
    public class ChangedRecord
    {
        public int TypeChangesID { get; private set; }
        public int UserID { get; private set; }
        public int? _OldValueNumber { get; private set; }
        public string _OldValueText { get; private set; }
        public float? _OldValueFloat { get; private set; }
        public string ChangedTableName { get; private set; }
        public int _ChangedRecordID { get; private set; }
        public string _ChangedTableColumnName { get; private set; }
        public DateTime? _OldValueDate { get; private set; }
        public bool _OldValueBool { get; private set; }

        #region Конструкторы
        public ChangedRecord(int WhoChangedID, string WhatTableChanged, int ChangedRecordID, string WhatColumnChanged, int OldValueInteger, ChangesTypes TypeChange)
        {
            UserID = WhoChangedID;
            ChangedTableName = WhatTableChanged;
            _ChangedRecordID = ChangedRecordID;
            _OldValueNumber = OldValueInteger;
            _ChangedTableColumnName = WhatColumnChanged;
            SetChangesType(TypeChange);
        }

        public ChangedRecord(int WhoChangedID, string WhatTableChanged, int ChangedRecordID, string WhatColumnChanged, string OldValueString, ChangesTypes TypeChange)
        {
            UserID = WhoChangedID;
            ChangedTableName = WhatTableChanged;
            _ChangedRecordID = ChangedRecordID;
            _OldValueText = OldValueString;
            _ChangedTableColumnName = WhatColumnChanged;
            SetChangesType(TypeChange);
        }

        public ChangedRecord(int WhoChangedID, string WhatTableChanged, int ChangedRecordID, string WhatColumnChanged, float OldValueFloat, ChangesTypes TypeChange)
        {
            UserID = WhoChangedID;
            ChangedTableName = WhatTableChanged;
            _ChangedRecordID = ChangedRecordID;
            _OldValueFloat = OldValueFloat;
            _ChangedTableColumnName = WhatColumnChanged;
            SetChangesType(TypeChange); ;
        }

        public ChangedRecord(int WhoChangedID, string WhatTableChanged, int ChangedRecordID, string WhatColumnChanged, DateTime? OldValueDateTime, ChangesTypes TypeChange)
        {
            UserID = WhoChangedID;
            ChangedTableName = WhatTableChanged;
            _ChangedRecordID = ChangedRecordID;
            _OldValueDate = OldValueDateTime;
            _ChangedTableColumnName = WhatColumnChanged;
            SetChangesType(TypeChange);
        }

        public ChangedRecord(int WhoChangedID, string WhatTableChanged, int ChangedRecordID, string WhatColumnChanged, bool OldValueBoolean, ChangesTypes TypeChange)
        {
            UserID = WhoChangedID;
            ChangedTableName = WhatTableChanged;
            _ChangedRecordID = ChangedRecordID;
            _OldValueBool = OldValueBoolean;
            _ChangedTableColumnName = WhatColumnChanged;
            SetChangesType(TypeChange);
        }

        public ChangedRecord(int WhoChangedID, string WhatTableChanged, int ChangedRecordID, string WhatColumnChanged)
        {
            UserID = WhoChangedID;
            ChangedTableName = WhatTableChanged;
            _ChangedRecordID = ChangedRecordID;
            _ChangedTableColumnName = WhatColumnChanged;
            SetChangesType(ChangesTypes.СозданиеЗаписи);
        }

        #endregion

        private void SetChangesType(ChangesTypes TypeChange)
        {
            TypeChangesID = (int)TypeChange;
        }
    }
}