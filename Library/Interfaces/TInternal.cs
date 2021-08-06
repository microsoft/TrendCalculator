//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace TrendsCalculator.Library.Interfaces
//{
//    internal interface TInternal: TInterface
//    {
//        [System.ComponentModel.DefaultValue(0)]
//        double LocalZ { get; set; }
//        [System.ComponentModel.DefaultValue(0)]
//        double GlobalZ { get; set; }
//    }

//    //internal interface InterfaceAdaptor
//    //{
//    //    TInternal ConvertTInterfaceToTInternal(TInterface input);
//    //}

//    internal class Internal : TInternal
//    {
//        public double LocalZ { get; set; }
//        public double GlobalZ { get; set; }
//        public List<int> CountWithPeriods { get; set; }
//        public int SupplyQuantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//        public double DemandSupplyQuotient { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//    }
//}
