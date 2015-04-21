using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models.Item
{
    /// <summary>
    /// The File Control Record is the last record of the Acknowledgement 1/receipt acknowledgement data file and contains
    /// seven fields. This record includes information from the input file that can be used to match the acknowledgement to
    /// the original input file.
    /// </summary>
    public class FileControlRecord
    {
        /// <summary>
        /// Value of field 2, Cash Letter Count, from the input File Control Record.
        /// </summary>
        public int TotalCashLetters { get; set; }

        /// <summary>
        /// Value of field 3, Total Record Count, from the input File Control Record.
        /// </summary>
        public int TotalRecordCount { get; set; }

        /// <summary>
        /// Value of field 4, Total Item Count, from the input File Control Record.
        /// </summary>
        public int TotalItemCount { get; set; }

        /// <summary>
        /// Value of field 5, File Total Amount, from the input File Control Record.
        /// </summary>
        public int FileTotalAmount { get; set; }

        /// <summary>
        /// Spaces. Reserved for future use
        /// </summary>
        public string Reserved { get; set; }

        public static FileControlRecord FromString(string source)
        {
            // check record type
            if (source.Substring(0, 2) != ItemRecordTypes.FileControlRecord)
                throw new ArgumentException();

            return new FileControlRecord()
            {
                TotalCashLetters = int.Parse(source.Substring(2, 6)),
                TotalRecordCount = int.Parse(source.Substring(8, 8)),
                TotalItemCount   = int.Parse(source.Substring(16, 8)),
                FileTotalAmount  = int.Parse(source.Substring(24, 16)),
                Reserved         = source.Substring(40, 40)
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
            builder.Append(ItemRecordTypes.FileControlRecord);
            builder.Append(TotalCashLetters.ToString().PadLeft(6, '0'));
            builder.Append(TotalRecordCount.ToString().PadLeft(8, '0'));
            builder.Append(TotalItemCount.ToString().PadLeft(8, '0'));
            builder.Append(FileTotalAmount.ToString().PadLeft(16, '0'));
            builder.Append(Reserved.PadRight(40));
            builder.AppendLine();
        }
    }
}
