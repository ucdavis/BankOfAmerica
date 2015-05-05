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
            var fs = new StreamReader(@"..\..\data\TREGENTS_TREGENTSAdjs20150414125414969.ack");

            var actual = ItemAcknowledgement.FromString(fs);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void TestFromString2()
        {
            var fs = new StreamReader(@"..\..\data\TREGENTS_TREGENTSAdjs20150429140803570.ack");

            var actual = ItemAcknowledgement.FromString(fs);

            Assert.IsNotNull(actual);
        }
    }
}
