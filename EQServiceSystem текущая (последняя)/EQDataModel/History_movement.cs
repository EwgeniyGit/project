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
    
    public partial class History_movement
    {
        public int id { get; set; }
        public int Equipment_ID { get; set; }
        public int user_id { get; set; }
        public int SubdeptID_sender { get; set; }
        public int SubdeptID_recipient { get; set; }
        public string Room { get; set; }
        public int Building { get; set; }
        public System.DateTime Data { get; set; }
    
        public virtual NALICH NALICH { get; set; }
        public virtual sprSubDept sprSubDept { get; set; }
        public virtual sprSubDept sprSubDept1 { get; set; }
        public virtual tUsers tUsers { get; set; }
    }
}
