using System.IO;
using BankOfAmerica.Acknowledgement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankOfAmerica.Acknowledgement.Tests
{
    [TestClass]
    public class ReceiptAcknowledgementTests
    {
        [TestMethod]
        public void TestFromString1()
        {
            var fs = new StreamReader(@"..\..\data\TREGENTS_TREGENTSRecp150414125131818.ack");

            var actual = ReceiptAcknowledgement.FromString(fs);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void TestFromString2()
        {
            var fs = new StreamReader(@"..\..\data\TREGENTS_TREGENTSRecp150414125259411.ack");

            var actual = ReceiptAcknowledgement.FromString(fs);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void TestFromString3()
        {
            var fs = new StreamReader(@"..\..\data\TREGENTS_TREGENTSRecp150429133649755.ack");

            var actual = ReceiptAcknowledgement.FromString(fs);

            Assert.IsNotNull(actual);
        }
    }
}
