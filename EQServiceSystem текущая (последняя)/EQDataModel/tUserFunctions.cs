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
    
    public partial class tUserFunctions
    {
        public tUserFunctions()
        {
            this.tFunctionAccess = new HashSet<tFunctionAccess>();
            this.tMainMenuItems = new HashSet<tMainMenuItems>();
        }
    
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }
        public string FunctionShortName { get; set; }
        public string AcessToObject { get; set; }
        public string Action { get; set; }
    
        public virtual ICollection<tFunctionAccess> tFunctionAccess { get; set; }
        public virtual ICollection<tMainMenuItems> tMainMenuItems { get; set; }
    }
}
