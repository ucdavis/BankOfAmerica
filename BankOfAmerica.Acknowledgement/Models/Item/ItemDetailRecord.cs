using System;
using System.Collections.Generic;
using System.Text;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models.Item
{
    /// <summary>
    /// The Detail Record follows the Cash Letter Header Record or an Item Detail Addendum Record, and contains nine fields.
    /// This record includes information that identifies items that contained an item level error and the type of error that
    /// caused the item to reject. An adjustment will be posted for each rejected item unless the number of items with errors
    /// is excessive.
    /// 
    /// NOTE: If the number of rejected items is excessive the entire cash letter is rejected, however, we will still report these
    /// rejected items to you in the Item Detail Addendum Record(s) up to the maximum threshold for allowable errors.
    /// </summary>
    public class ItemDetailRecord
    {
        public ItemDetailRecord()
        {
            Addendums = new List<ItemDetailAddendumRecord>();
        }

        /// <summary>
        /// Value of field 8, Item Sequence Number, from the
        /// input file Check Detail Record corresponding to the
        /// original item.
        /// </summary>
        public string ItemSequenceNumber { get; set; }

        /// <summary>
        /// Value of field 7, Bundle ID, from the input file
        /// Bundle Header record corresponding to the bundle
        /// of the original item.
        /// </summary>
        public string BundleId { get; set; }

        /// <summary>
        /// Amount of the item being rejected.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Reason for the item being rejected. Possible values
        /// include:
        ///     MICR
        ///     IMAGE
        ///     IQA
        ///     MULTIPLE
        ///     FOREIGN
        ///     NON-ELIGIBLE
        ///     DEPOSIT CORRECTION
        /// 
        /// Note : - A DEPOSIT CORRECTION error does not
        ///     require the item to be re-presented. The item
        ///     has been processed with the adjusted amount
        /// </summary>
        public string ItemErrorReason { get; set; }

        /// <summary>
        /// Overall image quality score assigned by the bank.
        /// </summary>
        public int ReportedOverallIqaScore { get; set; }

        /// <summary>
        /// Spaces. Reserved for future use.
        /// </summary>
        public string Reserved { get; set; }

        /// <summary>
        ///  Detail Addendum Records to follow this Detail Record
        /// </summary>
        public List<ItemDetailAddendumRecord> Addendums { get; set; }

        public static ItemDetailRecord FromString(string source)
        {
            // check record type
            if (source.Substring(0, 2) != ItemRecordTypes.ItemDetailRecord)
                throw new ArgumentException();

            return new ItemDetailRecord()
            {
                ItemSequenceNumber      = source.Substring(2, 15).Trim(),
                BundleId                = source.Substring(17, 10).Trim(),
                Amount                  = int.Parse(source.Substring(27, 10)),
                ItemErrorReason         = source.Substring(37, 30).Trim(),
                ReportedOverallIqaScore = int.Parse(source.Substring(67, 3)),
                Reserved                = source.Substring(73, 7)
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
            builder.Append(ItemRecordTypes.ItemDetailRecord);
            builder.Append(ItemSequenceNumber.PadRight(15));
            builder.Append(BundleId.PadRight(10));
            builder.Append(Amount.ToString("D10"));
            builder.Append(ItemErrorReason.PadRight(30));
            builder.Append(ReportedOverallIqaScore.ToString("D3"));
            builder.Append(Addendums.Count.ToString("D3"));
            builder.Append(Reserved.PadRight(7));
            builder.AppendLine();
        }
    }
}
