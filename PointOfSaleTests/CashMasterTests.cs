using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointOfSale;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Tests
{
    [TestClass()]
    public class CashMasterTests
    {
        [TestMethod()]
        public void CalculateChangeEmptyStackTest()
        {
            // emptyStack
            CashMaster master = new CashMaster(new Stack<decimal>());
        }
    }
}