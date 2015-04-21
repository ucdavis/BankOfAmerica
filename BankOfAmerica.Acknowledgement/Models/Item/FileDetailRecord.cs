using System;
using System.Collections.Generic;
using System.Text;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models.Item
{
    /// <summary>
    /// The File Detail Record is the second record of the acknowledgement 1/receipt acknowledgement data file, following
    /// the File Header Record, and contains six fields. This record will include the category of the file rejection, if applicable.
    /// </summary>
    public class FileDetailRecord
    {
        public FileDetailRecord()
        {
            Addendums = new List<FileDetailAddendumRecord>();
        }

        /// <summary>
        /// Category of reason for the file reject
        /// File Checking – Missing image, stale date,
        ///     duplicate image record, file size exceeded, file
        ///     format invalid, file out of balance
        /// Deposit Checking – Various file thresholds
        ///     exceeded or out of balance condition
        /// Duplicates – Duplicate file, bundle or Cash
        ///     Letter
        /// Image Compliance (CDM, IQA, Tiff) – Code line
        ///     data match, Image integrity error, Image
        ///     quality, image format error
        /// MICR - MICR field error
        /// Record Validations – Record error
        /// </summary>
        public string FileRejectReasonCategory { get; set; }

        /// <summary>
        /// Cash Letter ID corresponding to cash letter where
        /// cash letter or bundle level error occurred, if
        /// applicable
        /// </summary>
        public string CashLetterId { get; set; }

        /// <summary>
        /// Bundle ID corresponding to bundle where cash letter
        /// or bundle level error occurred, if applicable
        /// </summary>
        public string BundleId { get; set; }

        /// <summary>
        ///  Detail Addendum Records to follow this Detail Record
        /// </summary>
        public List<FileDetailAddendumRecord> Addendums { get; set; }

        public static FileDetailRecord FromString(string source)
        {
            // check record type
            if (source.Substring(0, 2) != ItemRecordTypes.FileDetailRecord)
                throw new ArgumentException();

            return new FileDetailRecord()
            {
                FileRejectReasonCategory = source.Substring(2, 57),
                CashLetterId             = source.Substring(62, 8),
                BundleId                 = source.Substring(70, 10)
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
            builder.Append(ItemRecordTypes.FileDetailRecord);
            builder.Append(FileRejectReasonCategory.PadRight(57));
            builder.Append(Addendums.Count.ToString().PadLeft(3, '0'));
            builder.Append(CashLetterId.PadRight(8));
            builder.Append(BundleId.PadRight(10));
            builder.AppendLine();

            Addendums.ForEach(a => a.ToString(builder));
        }
    }
}
