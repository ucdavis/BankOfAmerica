using System;
using System.Text;
using System.Threading;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models.Receipt
{
    /// <summary>
    /// The File Control Record is the last record of the acknowledgement 2 data file and contains nine fields. This record
    ///includes information from the input file that can be used to compare and match to your system, as well as the total
    ///number and amount of adjustments posted to your account(s) for any rejected items.
    /// 
    ///NOTE: If item level errors are excessive and an entire cash letter or file is rejected, these rejected items are not
    ///included in the adjustments total.
    /// </summary>
    public class FileControlRecord
    {
        /// <summary>
        /// Value of field 2, Cash Letter Count, from the input File Control Record.
        /// </summary>
        public int TotalCashLetterCount { get; set; }

        /// <summary>
        /// Value of field 3, Total Letter Count, from the input File Control Record.
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// Value of field 4, Total Item Count, from the input File Control Record.
        /// </summary>
        public int TotalItemsCount { get; set; }

        /// <summary>
        /// Value of field 5, File Total Amount, from the input File Control Record.
        /// </summary>
        public int TotalAmount { get; set; }

        /// <summary>
        /// Number of rejected items for which adjustments
        /// were posted
        /// 
        /// NOTE: This does not include rejected items that
        /// were included in a cash letter that was rejected.
        /// </summary>
        public int TotalRejectedAdjustmentsCount { get; set; }

        /// <summary>
        /// Total amount of rejected items for which
        /// adjustments were posted.
        /// 
        /// NOTE: This does not include rejected items that
        /// were included in a cash letter that was rejected.
        /// </summary>
        public int TotalRejectedAdjustmentsAmount { get; set; }

        /// <summary>
        /// Spaces. Reserved for future use
        /// </summary>
        public string Reserved { get; set; }

        public static FileControlRecord FromString(string source)
        {
            // check record type
            if (source.Substring(0, 2) != ReceiptRecordTypes.FileControlRecord)
                throw new ArgumentException();

            return new FileControlRecord()
            {
                TotalCashLetterCount           = int.Parse(source.Substring(2, 6)),
                TotalRecordsCount              = int.Parse(source.Substring(8, 8)),
                TotalItemsCount                = int.Parse(source.Substring(16, 8)),
                TotalAmount                    = int.Parse(source.Substring(24, 16)),
                TotalRejectedAdjustmentsCount  = int.Parse(source.Substring(40, 8)),
                TotalRejectedAdjustmentsAmount = int.Parse(source.Substring(48, 14)),
                Reserved                       = source.Substring(62, 18)
            };
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            ToString(builder);
            return builder.ToString();
        }

        public void ToString(StringBuilder builder)
        {
            builder.Append(ReceiptRecordTypes.FileControlRecord);
            builder.Append(TotalCashLetterCount.ToString("D6"));
            builder.Append(TotalRecordsCount.ToString("D8"));
            builder.Append(TotalItemsCount.ToString("D8"));
            builder.Append(TotalAmount.ToString("D16"));
            builder.Append(TotalRejectedAdjustmentsCount.ToString("D8"));
            builder.Append(TotalRejectedAdjustmentsAmount.ToString("D14"));
            builder.Append(Reserved.PadRight(18));
            builder.AppendLine();
        }
    }
}
