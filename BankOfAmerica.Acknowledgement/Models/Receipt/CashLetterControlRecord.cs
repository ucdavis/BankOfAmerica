using System;
using System.Text;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models.Receipt
{
    /// <summary>
    /// The Cash Letter Control Record follows the Cash Letter Header Record if no items are rejected or the last Detail
    /// Addendum Record reporting information on a rejected item for a cash letter and contains five fields. This record
    /// includes the total items that were rejected and reported as a Detail Record.
    /// 
    /// NOTE: If the entire cash letter is rejected due to excessive item level rejects, the rejected items will be reported as
    /// detail records with associated detail addendum records but no adjustments will be posted to your account. If the cash
    /// letter is processed but contains some item rejects, an adjustment will be posted for each rejected item.
    /// </summary>
    public class CashLetterControlRecord
    {
        /// <summary>
        /// Number of items reported as rejected for the cash
        /// letter identified in the preceding Cash Letter
        /// Header Record.
        /// 
        /// NOTE: If the entire cash letter is rejected, only
        /// items up to the maximum excessive items threshold
        /// are reported.
        /// </summary>
        public int TotalRejectedCount { get; set; }

        /// <summary>
        /// Amount of items reported as rejected for the cash
        /// letter identified in the preceding Cash Letter
        /// Header Record.
        /// 
        /// NOTE: If the entire cash letter is rejected, only
        /// items up to the maximum excessive items threshold
        /// are reported.
        /// </summary>
        public int TotalRejectedAmount { get; set; }

        /// <summary>
        /// Spaces. Reserved for future use
        /// </summary>
        public string Reserved { get; set; }

        public static CashLetterControlRecord FromString(string source)
        {
            // check record type
            if (source.Substring(0, 2) != ReceiptRecordTypes.CashLetterControlRecord)
                throw new ArgumentException();

            return new CashLetterControlRecord()
            {
                TotalRejectedCount  = int.Parse(source.Substring(2, 8)),
                TotalRejectedAmount = int.Parse(source.Substring(10, 14)),
                Reserved            = source.Substring(24, 56)
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
            builder.Append(ReceiptRecordTypes.CashLetterControlRecord);
            builder.Append(TotalRejectedCount.ToString("D8"));
            builder.Append(TotalRejectedAmount.ToString("D14"));
            builder.Append(Reserved.PadRight(56));
            builder.AppendLine();
        }
    }
}
