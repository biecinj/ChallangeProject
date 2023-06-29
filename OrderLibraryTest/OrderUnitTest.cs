using OrderLibrary;

namespace OrderLibraryTest;

[TestClass]
public class OrderUnitTest
{
    #region isInt()
    /// <summary>
    /// Testing isInt method. All should be true
    /// </summary>
    [TestMethod]
    public void TestIsInt()
    {
        string[] words = { "1", "0", "9999992", "-175", "  71", "-1" };
        foreach (var word in words)
        {
            bool result = Order.isInt(word);
            Assert.IsTrue(result, string.Format("Expected for '{0}': true; Actual: {1}",word, result));
        }
    }

    /// <summary>
    /// Testing isInt method. All should be false
    /// </summary>
    [TestMethod]
    public void TestIsNotInt()
    {
        string[] words = { "11.22", "123test", "T", "55,1222", "", " ", "'", "  7.", string.Empty };

        foreach (var word in words)
        {
            bool result = Order.isInt(word);
            Assert.IsFalse(result,
                   string.Format("Expected for '{0}': false; Actual: {1}",
                                 word, result));
        }
    }
    #endregion
    #region isDouble()
    /// <summary>
    /// Testing isDouble method. All should be true
    /// </summary>
    [TestMethod]
    public void TestIsDouble()
    {
        string[] words = { "  1,11", "0,00000001", "999999,2", "0,15197e-7", "-0,15197e-7", "  7,", ",4", "100", "-0" };
        foreach (var word in words)
        {
            bool result = Order.isDouble(word);
            Assert.IsTrue(result, string.Format("Expected for '{0}': true; Actual: {1}", word, result));
        }
    }

    /// <summary>
    /// Testing isDouble method. All should be false
    /// </summary>
    [TestMethod]
    public void TestIsNotDouble()
    {
        string[] words = { "11.22", "123test", "T", "55.1222", "", " ", "'", string.Empty, " 5. 9"  };

        foreach (var word in words)
        {
            bool result = Order.isDouble(word);
            Assert.IsFalse(result,
                   string.Format("Expected for '{0}': false; Actual: {1}",
                                 word, result));
        }
    }
    #endregion
    #region isIntInRange()
    /// <summary>
    /// Testing isIntInRange method. All should be true
    /// </summary>
    [TestMethod]
    public void TestIsIntInRange()
    {
        string[] words = { "  1", "1", "559", "  7", "100", "999" };
        foreach (var word in words)
        {
            bool result = Order.isIntInRange(word);
            Assert.IsTrue(result, string.Format("Expected for '{0}': true; Actual: {1}", word, result));
        }
    }

    /// <summary>
    /// Testing isIntInRange method. All should be false
    /// </summary>
    [TestMethod]
    public void TestIntIsNotInRange()
    {
        string[] words = { " -5 ", "-0", "1000", "0" };

        foreach (var word in words)
        {
            bool result = Order.isIntInRange(word);
            Assert.IsFalse(result,
                   string.Format("Expected for '{0}': false; Actual: {1}",
                                 word, result));
        }
    }
    #endregion
    #region isDate()
    /// <summary>
    /// Testing isDate method. All should be true
    /// </summary>
    [TestMethod]
    public void TestIsDate()
    {
        string[] words = { "  01.01", "1/1/2", "1-MAY-1", "  01.12.99", "2013/6/23", "2013-06-23", "9/9/9999" };
        foreach (var word in words)
        {
            bool result = Order.isDate(word);
            Assert.IsTrue(result, string.Format("Expected for '{0}': true; Actual: {1}", word, result));
        }
    }

    /// <summary>
    /// Testing isDate method. All should be false
    /// </summary>
    [TestMethod]
    public void TestIsNotDate()
    {
        string[] words = { " -5 ", "T", "1000", " ", "", string.Empty, "41.01.01", "01.13.20", "9/9/99999" };

        foreach (var word in words)
        {
            bool result = Order.isDate(word);
            Assert.IsFalse(result,
                   string.Format("Expected for '{0}': false; Actual: {1}",
                                 word, result));
        }
    }
    #endregion
    #region isDateInRange()
    /// <summary>
    /// Testing isDateInRange method. All should be true (depends on todays date)
    /// </summary>
    [TestMethod]
    public void TestisDateInRange()
    {
        string[] words = {   "1-MAY-24", "  01.12.2099", "2023/7/23", "2024-01-01", "9/9/9999" };
        foreach (var word in words)
        {
            bool result = Order.isDateInRange(word);
            Assert.IsTrue(result, string.Format("Expected for '{0}': true; Actual: {1}", word, result));
        }
    }

    /// <summary>
    /// Testing isDateInRange method. All should be false (depends on todays date)
    /// </summary>
    [TestMethod]
    public void TestDateIsNotInRange()
    {
        string[] words = { "  01.01", "1.12.22", "28/6/2023", "27.06.1000"  };

        foreach (var word in words)
        {
            bool result = Order.isDateInRange(word);
            Assert.IsFalse(result,
                   string.Format("Expected for '{0}': false; Actual: {1}",
                                 word, result));
        }
    }
    #endregion
    #region calculateFullPrice()
    /// <summary>
    /// Testing calculateFullPrice method. All should be true
    /// </summary>
    [TestMethod]
    public void TestCalculateFullPrice()
    {
        var resultList = new List<Tuple<double, double>>
        {
            new Tuple<double, double>(99.98, 1),
            new Tuple<double, double>(99.98, 10),
            new Tuple<double, double>(124.1111, 50),
            new Tuple<double, double>(99.98, 24),
            new Tuple<double, double>(99.98, 999),
            new Tuple<double, double>(-4.15, 2),
            new Tuple<double, double>(0, 2),
            new Tuple<double, double>(100, 2),
            new Tuple<double, double>(0, 0)
        };


        List<double> actualList = new List<double>
        {
            99.98,
            949.81,
            5274.72,
            2279.54,
            84898.02,
            -8.3,
            0,
            200,
            0
        };
        foreach (var numbers in resultList)
        {
            foreach (double actual in actualList)
            {
                double result = Order.calculateFullPrice(numbers.Item1, numbers.Item2);
                Assert.AreEqual(result, actual, "Expected {0}; Actual: {1}", actual, result);

                actualList.Remove(actual);
                break;
            }
        }
    }
    #endregion
    #region saveOrder()
    /// <summary>
    /// Testing saveOrder method. All should be true (depends on todays date)
    /// </summary>
    [TestMethod]
    public void TestSaveOrder()
    {
        List<string> actualList = new List<string>
        {
            @"Order ID: 1 | Customer ID: Test1 | DNA testing kit 1 | Base price: 99,98 | Order count: 10 | Full price: 949,81 | Delivery date: 11.01.2024",
            @"Order ID: 2 | Customer ID: Test2 | DNA testing kit 1 | Base price: 99,98 | Order count: 9 | Full price: 899,82 | Delivery date: 01.07.2023",
            @"Order ID: 3 | Customer ID: test3 | DNA testing kit 1 | Base price: 99,98 | Order count: 50 | Full price: 4249,15 | Delivery date: 12.12.2023",
            @"Order ID: 4 | Customer ID: Test4 | DNA testing kit 2 | Base price: 123 | Order count: 999 | Full price: 104445,45 | Delivery date: 01.11.2023"
        };


        Order.client_id = "Test1";
        Order.order_count = 10;
        Order.delivery_date = DateOnly.Parse("11/01/24");
        Order.saveOrder();

        Order.client_id = "Test2";
        Order.order_count = 9;
        Order.delivery_date = DateOnly.Parse("01/07");
        Order.saveOrder();

        Order.client_id = "test3";
        Order.order_count = 50;
        Order.delivery_date = DateOnly.Parse("12.12.2023");
        Order.saveOrder();

        Order.client_id = "Test4";
        Order.order_count = 999;
        Order.base_price = 123;
        Order.kit_variant = 2;
        Order.delivery_date = DateOnly.Parse("01-NOV-2023");
        List<string> result = Order.saveOrder();

        for(int i = 0; i < result.Count; i++) 
        Assert.AreEqual(result[i], actualList[i]);
    }
    #endregion
}