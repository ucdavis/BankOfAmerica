using System;
using System.Text;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models.Item
{
    /// <summary>
    /// The Cash Letter Header Record follows the File Header Record or a Cash Letter Control Record (if more than one cash
    /// letter is included in the file and contains seven fields. This record includes information from the input file that can be
    /// used to compare and match to your input file, as well as the final status of each cash letter.
    /// </summary>
    public class CashLetterHeaderRecord
    {
        /// <summary>
        /// Cash Letter ID from the Cash Letter ID field of the
        /// Cash Letter Header record corresponding to the
        /// cash letter where the item adjustments are made or
        /// errors are reported
        /// </summary>
        public string CashLetterId { get; set; }

        /// <summary>
        /// Final status of the cash letter. Possible values
        /// include:
        ///     "C/L ACCEPTED-ADJ RPTD" Indicates that the
        ///         cash letter will be processed and a deposit
        ///         posted to your account for the amount of the
        ///         cash letter, however, some items within the
        ///         cash letter were rejected. Adjustments will be
        ///         posted for any rejected items
        ///     "C/L ACCEPTED – NO ADJ RPTD" Indicates that
        ///         the cash letter will be processed and a deposit
        ///         posted to your account for the amount of the
        ///         cash letter; no items within the cash letter
        ///         were rejected
        ///     "CASH LETTER REJECTED" Indicates that the
        ///         cash letter will not be processed and no credits
        ///         or debits will be posted to your account
        /// </summary>
        public string CashLetterStatus { get; set; }

        /// <summary>
        /// Value of field 3, Items within Cash Letter Count,
        /// from the input file Cash Letter Control Record.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Value of field 4, Cash Letter Total Amount, from the
        /// input file Cash Letter Control Record.
        /// </summary>
        public int TotalAmount { get; set; }

        /// <summary>
        /// Spaces. Reserved for future use
        /// </summary>
        public string Reserved { get; set; }

        public static CashLetterHeaderRecord FromString(string source)
        {
            // check record type
            if (source.Substring(0, 2) != ItemRecordTypes.CashLetterHeaderRecord)
                throw new ArgumentException();

            return new CashLetterHeaderRecord()
            {
                CashLetterId     = source.Substring(2, 8).Trim(),
                CashLetterStatus = source.Substring(10, 25).Trim(),
                TotalCount       = int.Parse(source.Substring(35, 8)),
                TotalAmount      = int.Parse(source.Substring(43, 14)),
                Reserved         = source.Substring(57, 23)
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
            builder.Append(ItemRecordTypes.CashLetterHeaderRecord);
            builder.Append(CashLetterId.PadRight(8));
            builder.Append(CashLetterStatus.PadRight(25));
            builder.Append(TotalCount.ToString("D8"));
            builder.Append(TotalAmount.ToString("D14"));
            builder.Append(Reserved.PadRight(23));
            builder.AppendLine();
        }
    }
}
