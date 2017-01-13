using EQDataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EQServiceSystem.Models.Reports
{
    public class DataTableReport
    {
        public DataTable ReportTable(List<NALICH> query)
    {
        DataTable Table = new DataTable();
        //Table.TableName = "Table";
        Table.Columns.Add("ShortName", typeof(string));                  
        Table.Columns.Add("REGNO", typeof(string));                      
        Table.Columns.Add("GroupName", typeof(string));                  
        Table.Columns.Add("SubDeptCode", typeof(int));
        Table.Columns.Add("IdSubDept", typeof(int));       
        Table.Columns.Add("SignCode", typeof(string));                        
        Table.Columns.Add("P", typeof(string));                          
        Table.Columns.Add("M", typeof(string));                          
        Table.Columns.Add("W", typeof(string));                          
        Table.Columns.Add("G", typeof(string));                          
        Table.Columns.Add("NAIMOB", typeof(string));                     
        Table.Columns.Add("MARKA", typeof(string));                      
        Table.Columns.Add("ZAWNO", typeof(string));                      
        Table.Columns.Add("GODWYP", typeof(string));                     
        Table.Columns.Add("DWWOD", typeof(string));                      
        Table.Columns.Add("ZAWOD", typeof(string));                      
        Table.Columns.Add("WRASCH", typeof(string));                     
        Table.Columns.Add("KOLKSTR", typeof(double));
        Table.Columns.Add("KOLTSTR", typeof(double));
        Table.Columns.Add("KOLOSTR", typeof(double));                     
        Table.Columns.Add("OBOZN", typeof(string));
        Table.Columns.Add("PER", typeof(double));                         
        Table.Columns.Add("BuldingName", typeof(string));
        Table.Columns.Add("BuildingID", typeof(int));    
        Table.Columns.Add("POM", typeof(string));                        
        Table.Columns.Add("SPOSK", typeof(string));                      
        Table.Columns.Add("SPOST", typeof(string));                      
        Table.Columns.Add("SPOSTO", typeof(string));                     
        Table.Columns.Add("INWNOM", typeof(string));                     
        Table.Columns.Add("SHIFWR", typeof(string));
        Table.Columns.Add("SMEN", typeof(double));
        Table.Columns.Add("KUSE", typeof(double));
        Table.Columns.Add("KUSM", typeof(double));
        Table.Columns.Add("KATM", typeof(double));
        Table.Columns.Add("KATE", typeof(double));
        Table.Columns.Add("STOIM", typeof(double));
        Table.Columns.Add("TRUDK", typeof(double));
        Table.Columns.Add("TRUDT", typeof(double));
        Table.Columns.Add("TRUDTO", typeof(double));                      
        Table.Columns.Add("DPKAP", typeof(string));                      
        Table.Columns.Add("WUST", typeof(string));                       
            using (PPREntities ppr = new PPREntities())
            {
                foreach (NALICH item in query)
                {
                    List<string> GroupName = (from sprgroup in ppr.sprEquipmentGroup
                                    where (sprgroup.SymbolID == item.G)
                                    select sprgroup.GroupName).ToList();
                    List<string> SignCode = (from tAgrCode in ppr.tAggregateCodifer     //код принадлежности к агрегату
                                   where (tAgrCode.SignID == item.AggregateCode)
                                   select tAgrCode.SignCode).ToList();
                        Table.Rows.Add(
                            item.sprSubDept.ShortName,
                            item.REGNO,
                            GroupName[0],
                            item.sprSubDept.SubDeptCode,
                            item.sprSubDept.IdSubDept,
                            SignCode[0],
                            item.P,
                            item.M,
                            item.W, 
                            item.G,
                            item.NAIMOB, 
                            item.MARKA,
                            item.ZAWNO,
                            item.GODWYP, 
                            item.DWWOD, 
                            item.ZAWOD, 
                            item.WRASCH,
                            item.KOLKSTR,
                            item.KOLTSTR,
                            item.KOLOSTR,
                            item.OBOZN,
                            item.PER,
                            item.sprBuildings.BuldingName,
                            item.sprBuildings.BuildingID,
                            item.POM,
                            item.SPOSK,
                            item.SPOST,
                            item.SPOSTO,
                            item.INWNOM,
                            item.SHIFWR,
                            item.SMEN, 
                            item.KUSE,
                            item.KUSM,
                            item.KATM, 
                            item.KATE, 
                            item.STOIM,
                            item.TRUDK,
                            item.TRUDT,
                            item.TRUDTO,
                            item.DPKAP, 
                            item.WUST);
                }
        }
        return Table ;
    }
    public DataTable ReportTableReactors(List<NALICH> query)
    {
        DataTable Table = new DataTable();
        //Table.TableName = "Table";
        Table.Columns.Add("Name", typeof(string));                  
        Table.Columns.Add("REGNO", typeof(string));                      
        Table.Columns.Add("GroupName", typeof(string));                  
        Table.Columns.Add("SubDeptCode", typeof(int));
        Table.Columns.Add("IdSubDept", typeof(int));       
        Table.Columns.Add("SignCode", typeof(string));                        
        Table.Columns.Add("P", typeof(string));                          
        Table.Columns.Add("M", typeof(string));                          
        Table.Columns.Add("W", typeof(string));                          
        Table.Columns.Add("G", typeof(string));                          
        Table.Columns.Add("NAIMOB", typeof(string));                     
        Table.Columns.Add("MARKA", typeof(string));                      
        Table.Columns.Add("ZAWNO", typeof(string));                      
        Table.Columns.Add("GODWYP", typeof(string));                     
        Table.Columns.Add("DWWOD", typeof(string));                      
        Table.Columns.Add("ZAWOD", typeof(string));                      
        Table.Columns.Add("WRASCH", typeof(string));                     
        Table.Columns.Add("KOLKSTR", typeof(double));
        Table.Columns.Add("KOLTSTR", typeof(double));
        Table.Columns.Add("KOLOSTR", typeof(double));                     
        Table.Columns.Add("OBOZN", typeof(string));
        Table.Columns.Add("PER", typeof(double));                         
        Table.Columns.Add("BuldingName", typeof(string));
        Table.Columns.Add("BuildingID", typeof(int));    
        Table.Columns.Add("POM", typeof(string));                        
        Table.Columns.Add("SPOSK", typeof(string));                      
        Table.Columns.Add("SPOST", typeof(string));                      
        Table.Columns.Add("SPOSTO", typeof(string));                     
        Table.Columns.Add("INWNOM", typeof(string));                     
        Table.Columns.Add("SHIFWR", typeof(string));
        Table.Columns.Add("SMEN", typeof(double));
        Table.Columns.Add("KUSE", typeof(double));
        Table.Columns.Add("KUSM", typeof(double));
        Table.Columns.Add("KATM", typeof(double));
        Table.Columns.Add("KATE", typeof(double));
        Table.Columns.Add("STOIM", typeof(double));
        Table.Columns.Add("TRUDK", typeof(double));
        Table.Columns.Add("TRUDT", typeof(double));
        Table.Columns.Add("TRUDTO", typeof(double));                      
        Table.Columns.Add("DPKAP", typeof(string));                      
        Table.Columns.Add("WUST", typeof(string));                       
            using (PPREntities ppr = new PPREntities())
            {
                foreach (NALICH item in query)
                {
                    List<string> GroupName = (from sprgroup in ppr.sprEquipmentGroup
                                    where (sprgroup.SymbolID == item.G)
                                    select sprgroup.GroupName).ToList();
                    List<string> SignCode = (from tAgrCode in ppr.tAggregateCodifer     
                                   where (tAgrCode.SignID == item.AggregateCode)
                                   select tAgrCode.SignCode).ToList();
                    List<string> RU = (from reactors in ppr.sprReactors
                                           join important in ppr.tImportantEquipment on reactors.ID equals important.ReactorID
                                           join nalich in ppr.NALICH on important.EquipmentID equals nalich.ID
                                             where (important.EquipmentID == item.ID)
                                             select reactors.Name).ToList();
                        Table.Rows.Add(
                            RU[0],
                            item.REGNO,
                            GroupName[0],
                            item.sprSubDept.SubDeptCode,
                            item.sprSubDept.IdSubDept,
                            SignCode[0],
                            item.P,
                            item.M,
                            item.W, 
                            item.G,
                            item.NAIMOB, 
                            item.MARKA,
                            item.ZAWNO,
                            item.GODWYP, 
                            item.DWWOD, 
                            item.ZAWOD, 
                            item.WRASCH,
                            item.KOLKSTR,
                            item.KOLTSTR,
                            item.KOLOSTR,
                            item.OBOZN,
                            item.PER,
                            item.sprBuildings.BuldingName,
                            item.sprBuildings.BuildingID,
                            item.POM,
                            item.SPOSK,
                            item.SPOST,
                            item.SPOSTO,
                            item.INWNOM,
                            item.SHIFWR,
                            item.SMEN, 
                            item.KUSE,
                            item.KUSM,
                            item.KATM, 
                            item.KATE, 
                            item.STOIM,
                            item.TRUDK,
                            item.TRUDT,
                            item.TRUDTO,
                            item.DPKAP, 
                            item.WUST);
                }
        }
        return Table ;
    }
    }
}