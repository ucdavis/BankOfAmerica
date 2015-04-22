using System;

namespace BankOfAmerica.Acknowledgement.Resources
{
    public static class ReceiptRecordTypes
    {
        public static string FileHeaderRecord = "01";
        public static string CashLetterHeaderRecord = "10";
        public static string ItemDetailRecord = "25";
        public static string ItemDetailAddendumRecord = "26";
        public static string CashLetterControlRecord = "90";
        public static string FileControlRecord = "99";
    }
}
