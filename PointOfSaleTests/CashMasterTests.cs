using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PointOfSale.Tests
{
    [TestClass()]
    public class CashMasterTests
    {
        [TestMethod()]
        public void CalculateChangeTest()
        {
            // emptyStack
            CashMaster master = new CashMaster(new Stack<decimal>());

            Assert.IsNotNull(master.CalculateChange(0, new Dictionary<decimal, int>()));

            Assert.IsNotNull(master.CalculateChange(0, null));

            Assert.IsNotNull(master.CalculateChange(-1000000000000, null));
        }
    }
}