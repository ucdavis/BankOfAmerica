using System;
using System.Text;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models.Item
{
    /// <summary>
    /// One or more Detail Addendum Records will follow each related Detail Record and contains five fields. This record will
    /// include the details of the reason(s) a file was rejected, if applicable. If a file is rejected due to more than one type of
    /// error, additional Detail Addendum Records will be included to report each additional error reason.
    /// 
    /// NOTE: If an error message contains too many characters to fit into one detail addendum record, additional detail
    /// addendum records will be created until the entire error message is report
    /// </summary>
    public class FileDetailAddendumRecord
    {
        /// <summary>
        /// Ordinal Number of Addendum Record
        /// 
        /// Note: The first addendum record following the
        /// detail record will be '001', the second '002' and so
        /// forth. The last ordinal should match the Addendum
        /// Record Count of the Detail Record.
        /// </summary>
        public int AddendumRecordNumber { get; set; }

        /// <summary>
        /// Number of the current file error being reported.
        /// Each file error will start on a new Detail Addendum
        /// Record.
        /// </summary>
        public int FileRejectReasonNumber { get; set; }

        /// <summary>
        /// Reason file is being rejected if applicable, otherwise
        /// spaces. File reject reasons may change from time
        /// to time; sample error messages include:
        /// 
        /// The bundle item count exceeds the configured
        ///     max
        /// Business Date is a stale date
        /// </summary>
        public string FileRejectReason { get; set; }

        public static FileDetailAddendumRecord FromString(string source)
        {
            // check record type
            if (source.Substring(0, 2) != ItemRecordTypes.FileDetailAddendumRecord)
                throw new ArgumentException();

            return new FileDetailAddendumRecord()
            {
                AddendumRecordNumber   = int.Parse(source.Substring(2, 3)),
                FileRejectReasonNumber = int.Parse(source.Substring(5, 2)),
                FileRejectReason       = source.Substring(7, 73).Trim()
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
            builder.Append(ItemRecordTypes.FileDetailAddendumRecord);
            builder.Append(AddendumRecordNumber.ToString("D3"));
            builder.Append(FileRejectReasonNumber.ToString("D2"));
            builder.Append(FileRejectReason.PadRight(73));
            builder.AppendLine();
        }
    }
}
