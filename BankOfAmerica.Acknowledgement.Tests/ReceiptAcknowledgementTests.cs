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
            var fs = new StreamReader(@"..\..\data\TREGENTS_TREGENTSAdjs20150414125414969.ack");

            var actual = ReceiptAcknowledgement.FromString(fs);

            Assert.IsNotNull(actual);
        }
    }
}
