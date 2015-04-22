using System;
using System.Text;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models.Receipt
{
    /// <summary>
    /// One or more Item Detail Addendum Records will follow each related Detail Record and contains five fields. This
    /// record will include the details of the reason an item was rejected, if applicable. If a item is rejected due to more than
    /// one type of error, an additional new Detail Addendum Records will follow the related Detail Record in the item
    /// acknowledgement data file to report each additional item adjustment reason.
    /// </summary>
    public class ItemDetailAddendumRecord
    {
        /// <summary>
        /// Ordinal number of Addendum record
        /// 
        /// Note: The first addendum record following the
        /// detail record will be '001', the second '002' and so
        /// forth
        /// </summary>
        public int AddendumRecordNumber { get; set; }
        
        /// <summary>
        /// Number of the current item adjustment reason
        /// being reported
        /// 
        /// NOTE: Each item adjustment reason will start on a
        /// new Detail Addendum Record
        /// </summary>
        public int AdjustmentReasonNumber { get; set; }

        /// <summary>
        /// Reason for adjustment
        /// </summary>
        public string AdjustmentReasonDetail { get; set; }

        public static ItemDetailAddendumRecord FromString(string source)
        {
            // check record type
            if (source.Substring(0, 2) != ReceiptRecordTypes.ItemDetailAddendumRecord)
                throw new ArgumentException();

            return new ItemDetailAddendumRecord()
            {
                AddendumRecordNumber = int.Parse(source.Substring(2, 3)),
                AdjustmentReasonNumber = int.Parse(source.Substring(5, 2)),
                AdjustmentReasonDetail = source.Substring(7, 73).Trim()
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
            builder.Append(ReceiptRecordTypes.ItemDetailAddendumRecord);
            builder.Append(AddendumRecordNumber.ToString("D3"));
            builder.Append(AdjustmentReasonNumber.ToString("D2"));
            builder.Append(AdjustmentReasonDetail.PadRight(73));
            builder.AppendLine();
        }
    }
}
