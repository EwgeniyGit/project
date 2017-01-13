using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQServiceSystem.Models
{
    public enum ChangesTypes { Изменение = 1, Удаление = 2, СозданиеЗаписи = 3 } //Таблица в базе - tChangesTypes
    public enum AccessType { Read, Write, Create, Delete }

    public class GlobalObjects
    {
    }
}