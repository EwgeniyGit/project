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
    
    public partial class sprGroup
    {
        public sprGroup()
        {
            this.TypeISGroup = new HashSet<TypeISGroup>();
        }
    
        public int idGroup { get; set; }
        public string GroupName { get; set; }
    
        public virtual ICollection<TypeISGroup> TypeISGroup { get; set; }
    }
}
