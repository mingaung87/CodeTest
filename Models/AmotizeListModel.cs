using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeTest.Models
{
    public class AmotizeListModel
    {
        public int Month { get; set; }
        public double Payment { get; set; }
        public double Balance { get; set; }
        public double MonthlyInterest { get; set; }
        public double CummulativeInterest { get; set; }
        public double MonthlyPrincipal { get; set; }
        public double CummulativePrincipal { get; set; }
    }
}