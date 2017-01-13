using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQDataModel;

namespace EQServiceSystem.ViewModels
{
    public struct CalculationNorminfo
    {
        public int RecordID;
        public int SubDeptID;
        public string SubDeptName;
        public int? Year;
        public int? QuarterNumber;
        public double? RepairPriceC;
        public double? RepairPriceO;
        public double? RepairPriceP;
        public double? MaterialPriceC;
        public double? MaterialPriceP;
        public double? MaterialPriceO;
    }

    public class CalculationNormEditor
    {
        public List<CalculationNorminfo> CalcNormCollection = new List<CalculationNorminfo>();
        public List<string> Years = new List<string>();
        private List<int?> YearsInt = new List<int?>();
        public List<string> Quarters = new List<string>();
        public List<CalculationNorminfo> NewQuarterTemplate = new List<CalculationNorminfo>();

        public CalculationNormEditor()
        {
            using (PPREntities ppre = new PPREntities())
            {
                var items = (from elems in ppre.sprRepairCalc
                             select elems).OrderBy(e=>e.Year).ThenBy(e=>e.sprSubDept.ShortName).ToList();

                foreach (var item in items)
                {
                    var element = new CalculationNorminfo();
                    element.RecordID = item.RecordID;
                    element.SubDeptID = item.IdSubDept;
                    element.SubDeptName = item.sprSubDept.ShortName;
                    element.Year = item.Year;
                    element.QuarterNumber = item.QuarterNumber;
                    element.RepairPriceC = item.RepairPriceC;
                    element.RepairPriceO = item.RepairPriceO;
                    element.RepairPriceP = item.RepairPriceP;
                    element.MaterialPriceC = item.MaterialPriceC;
                    element.MaterialPriceP = item.MaterialPriceP;
                    element.MaterialPriceO = item.MaterialPriceO;
                    CalcNormCollection.Add(element);
                }

                Years.Add(string.Empty);
                Years.AddRange(items.Select(x => x.Year.ToString()).Distinct().OrderBy(x => x));
                YearsInt.AddRange(items.Select(x => x.Year).Distinct().OrderBy(x => x));
            }
            
            Quarters.Add(string.Empty);
            Quarters.Add("1");
            Quarters.Add("2");
            Quarters.Add("3");
            Quarters.Add("4");
        }

        public void FilterByYear(string year)
        {
            CalcNormCollection.RemoveAll(x => x.Year.ToString() != year);
            Years.RemoveRange(0, Years.Count);
            Years.Add(year);
        }

        public void CreateNewQuarterTemplate(int year, int quarterNo)
        {
            List<sprSubDept> ActiveSubDepts = new List<sprSubDept>();
            using (PPREntities ppre = new PPREntities())
            {
                //TODO Добавить условие - удалено или нет подразделение
                var active = (from subdepts in ppre.sprSubDept
                              select subdepts).OrderBy(x=>x.ShortName).ToList();
                int? lastYear = LastYearInDB();
                int counter = 0;
                
                    foreach (var item in active)
                    {                        
                        var recordTemplate = new CalculationNorminfo();
                        recordTemplate.SubDeptID = item.IdSubDept;
                        recordTemplate.SubDeptName = item.ShortName;
                        recordTemplate.Year = year;
                        recordTemplate.QuarterNumber = quarterNo;
                        recordTemplate.RepairPriceC = CalcNormCollection.Where(cc => ((cc.SubDeptID == item.IdSubDept) && (cc.Year == lastYear) && (cc.QuarterNumber == quarterNo))).FirstOrDefault().RepairPriceC ?? SetDefaultValue();
                        recordTemplate.MaterialPriceC = CalcNormCollection.Where(cc => ((cc.SubDeptID == item.IdSubDept) && (cc.Year == lastYear) && (cc.QuarterNumber == quarterNo))).FirstOrDefault().MaterialPriceC ?? SetDefaultValue();
                        recordTemplate.RepairPriceP = CalcNormCollection.Where(cc => ((cc.SubDeptID == item.IdSubDept) && (cc.Year == lastYear) && (cc.QuarterNumber == quarterNo))).FirstOrDefault().RepairPriceP ?? SetDefaultValue();
                        recordTemplate.MaterialPriceP = CalcNormCollection.Where(cc => ((cc.SubDeptID == item.IdSubDept) && (cc.Year == lastYear) && (cc.QuarterNumber == quarterNo))).FirstOrDefault().MaterialPriceP ?? SetDefaultValue();
                        recordTemplate.RepairPriceO = CalcNormCollection.Where(cc => ((cc.SubDeptID == item.IdSubDept) && (cc.Year == lastYear) && (cc.QuarterNumber == quarterNo))).FirstOrDefault().RepairPriceO ?? SetDefaultValue();
                        recordTemplate.MaterialPriceO = CalcNormCollection.Where(cc => ((cc.SubDeptID == item.IdSubDept) && (cc.Year == lastYear) && (cc.QuarterNumber == quarterNo))).FirstOrDefault().MaterialPriceO ?? SetDefaultValue();

                        NewQuarterTemplate.Add(recordTemplate);
                        counter++;                        
                    }
            }
        }

        private int? LastYearInDB()
        {
            int? LastYear = YearsInt.Max();
            if (LastYear != DateTime.Now.Year)
                return LastYear;
            else
                return LastYear - 1;
        }

        private double? SetDefaultValue()
        {
            return 0;
        }
    }
}