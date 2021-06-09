using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PointOfSale.Tests
{
    [TestClass()]
    public class TextExtensionMethodsTests
    {
        [TestMethod()]
        public void SaveExceptionAsyncTest()
        {
            try
            {
                TextExtensionMethods test = new TextExtensionMethods();
                Exception ex = null;
                test.SaveExceptionAsync(ex);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown" + ex.Message);
            }
        }

        [TestMethod()]
        public void GetExceptionMessageLogTest()
        {
            try
            {
                TextExtensionMethods test = new TextExtensionMethods();
                Exception ex = null;
                test.GetExceptionMessageLog(ex);
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown" + ex.Message);
            }
        }

        [TestMethod()]
        public void ValidateDecimalTest()
        {
            try
            {
                TextExtensionMethods test = new TextExtensionMethods();
                test.ValidateDecimal(string.Empty);
                test.ValidateDecimal("ASDFJÑLASDJFÑLKSDJFSDJFÑLASDFJÑKASDFÑKASDJFÑKLASJDFÑLKJSDFLJASDÑFJDSFKJDÑFKLJSDÑLKFJSDÑLKFJDÑSLFKJDÑLASFKJ");
                test.ValidateDecimal("999999999999999999999999999999999999.99999999999999");
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown" + ex.Message);
            }
        }

        [TestMethod()]
        public void CloneTest()
        {
            try
            {
                TextExtensionMethods test = new TextExtensionMethods();
                test.Clone(new List<decimal>());
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown" + ex.Message);
            }
        }
    }
}