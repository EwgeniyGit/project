//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EQDataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class sprEquipmentGroup
    {
        public sprEquipmentGroup()
        {
            this.sprEquipment = new HashSet<sprEquipment>();
        }
    
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string SymbolID { get; set; }
    
        public virtual ICollection<sprEquipment> sprEquipment { get; set; }
    }
}
