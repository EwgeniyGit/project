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
    
    public partial class ElectronicArchive
    {
        public int id_doc { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
    }
}
