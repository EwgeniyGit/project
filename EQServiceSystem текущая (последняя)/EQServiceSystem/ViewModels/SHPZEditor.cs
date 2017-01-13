using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.ViewModels
{
    public class SHPZ //TODO переписать нормально
    {
        public int IdSubDept { get; set; }
        public string SubDeptName { get; set; }
        public string RepairType { get; set; }
        public string RepairMethod { get; set; }
        public string BalanceBill { get; set; }
        public string CostElements { get; set; }
        public string CostParticle { get; set; }
    }

    public class SHPZEditor
    {
        public List<SHPZ> SHPZList = new List<SHPZ>();

        public SHPZEditor()
        {

            using (PPREntities ppre = new PPREntities())
            {
                var tmplist = new List<SHPZ>(); 
                var items = (from sh in ppre.sprSHPZ
                             orderby sh.IdSubDept, sh.RepairType, sh.RepairMethod
                             select sh).ToList();
                foreach (var item in items)
                {
                    var elem = new SHPZ();
                    elem.IdSubDept = item.IdSubDept;
                    elem.RepairType = item.RepairType;
                    elem.RepairMethod = item.RepairMethod;
                    elem.BalanceBill = item.BalanceBill;
                    elem.CostElements = item.CostElements;
                    elem.CostParticle = item.CostParticle;
                    elem.SubDeptName = item.sprSubDept.SubDeptName;
                    tmplist.Add(elem);
                }
                SHPZList = tmplist.OrderBy(x => x.SubDeptName).ToList();
            }
        }
    }
}