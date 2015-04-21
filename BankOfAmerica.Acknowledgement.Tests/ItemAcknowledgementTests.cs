using System.IO;
using BankOfAmerica.Acknowledgement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankOfAmerica.Acknowledgement.Tests
{
    [TestClass]
    public class ItemAcknowledgementTests
    {
        [TestMethod]
        public void TestFromString1()
        {
            var fs = new StreamReader(@"..\..\data\TREGENTS_TREGENTSRecp150414125131818.ack");

            var actual = ItemAcknowledgement.FromString(fs);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void TestFromString2()
        {
            var fs = new StreamReader(@"..\..\data\TREGENTS_TREGENTSRecp150414125259411.ack");

            var actual = ItemAcknowledgement.FromString(fs);

            Assert.IsNotNull(actual);
        }
    }
}
