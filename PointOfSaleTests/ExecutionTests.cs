using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PointOfSale.Tests
{
    [TestClass()]
    public class ExecutionTests
    {
        [TestMethod()]
        public void ChooseCurrencyTest()
        {
            Execution execution = new Execution();
            execution.ChooseCurrency(string.Empty);

            Assert.IsNotNull(execution.ExecuteProgram(99.99m));
        }

        [TestMethod()]
        public void InitializeBillsTest()
        {
            Execution execution = new Execution();
            execution.Culture.Current.Currencies = new Stack<decimal>();
            Assert.IsNotNull(execution.ExecuteProgram(99.99m));
        }
    }
}