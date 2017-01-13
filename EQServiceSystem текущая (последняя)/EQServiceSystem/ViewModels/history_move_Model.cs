using EQDataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EQServiceSystem.ViewModels
{
    public class history_move_Model
    {
        public int IDEquipment;
        public List<history_elem> History_full = new List<history_elem>(); 
        //public history_move_Model()
        //{
        //    using (PPREntities ppre = new PPREntities())
        //    {
        //        var History = ppre.history_move.ToList();
               
        //        foreach (var item in History)
        //        { 
        //            history_elem el = new history_elem();
        //            el.ID=item.id_Hist;
        //            el.Equipment_ID= item.Equipment_ID;
        //            el.RegNum = item.REGNO;
        //            el.InvNum = ppre.NALICH.Where(x => x.ID == item.Equipment_ID).Single().ToString();
        //            el.NAIMOB=item.NAIMOB;
        //            el.Subdept_send=item.Send;
        //            el.Subdept_rec = item.Rec;
        //            el.Data=item.Data_isp;
        //            History_full.Add(el);
        //        }
        //    }
        //}
        public history_move_Model(int EquipmentID)
        {
            using (PPREntities ppre = new PPREntities())
            {
                var History = ppre.history_move.Where(x => x.Equipment_ID == EquipmentID).ToList();
                foreach (var item in History)
                {
                    history_elem el = new history_elem();
                    el.ID = item.id_Hist;
                    el.Equipment_ID = item.Equipment_ID;
                    el.RegNum = item.REGNO;
                    var invlist = ppre.NALICH.Where(x => x.ID == item.Equipment_ID).Single();
                    el.InvNum = invlist.INWNOM;
                    var buildlist= ppre.sprBuildings.Where(x => x.BuildingID == item.BuildIDOld).Single();
                    el.Building = buildlist.BuldingName;
                    el.Room = item.Room;
                    el.NAIMOB = item.NAIMOB;
                    el.Subdept_send = item.Send;
                    el.Subdept_rec = item.Rec;
                    el.Data = item.Data_isp;
                    el.userFIO = item.FIO;
                    History_full.Add(el);
                }
                IDEquipment = EquipmentID;
            }
        }
        public class history_elem
        {
            public int ID { get; set; }
            public int Equipment_ID { get; set; }
            public string RegNum { get; set; }
            public string InvNum { get; set; }
            public string Room { get; set; }
            public string Building { get; set; }
            public string NAIMOB { get; set; }
            public string Subdept_send { get; set; }
            public string Subdept_rec { get; set; }
            public string userFIO { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
            public DateTime Data { get; set; }
            public history_elem(int ElemID)
            {
                ID = ElemID;
            }
            public history_elem()
            {
            }
        }
    }
}