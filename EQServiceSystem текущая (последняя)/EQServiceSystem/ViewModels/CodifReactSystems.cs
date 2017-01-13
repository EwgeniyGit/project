using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.ViewModels
{
    public struct ReactCodifSystem
    {
        public string SystemCode;
        public string SubSystemCode;
    }

    public class CodifReactSystems
    {
        public List<ReactCodifSystem> Codifiers { get; private set; }

        public CodifReactSystems()
        {
            Codifiers = new List<ReactCodifSystem>();
            using (PPREntities ppre = new PPREntities())
            {
                var items = (from nalich in ppre.NALICH
                             orderby nalich.KSUST
                             where nalich.KSUST != null
                             select new { KSUST = nalich.KSUST, KPUST = nalich.KPUST}).Distinct().ToList();
                foreach (var item in items)
                {
                    var rcs = new ReactCodifSystem();
                    rcs.SystemCode = item.KSUST;
                    rcs.SubSystemCode = item.KPUST;
                    Codifiers.Add(rcs);
                }
                items.Clear();
            }
        }
    }
}