using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BankOfAmerica.Acknowledgement.Models.Receipt;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models
{
    public class ReceiptAcknowledgement
    {
        public ReceiptAcknowledgement()
        {
            Details = new List<FileDetailRecord>();
        }

        public FileHeaderRecord Header { get; set; }
        public List<FileDetailRecord> Details { get; set; }
        public FileControlRecord Control { get; set; }

        public static ReceiptAcknowledgement FromString(StreamReader reader)
        {
            var result = new ReceiptAcknowledgement();

            // first line should be header record
            var line = reader.ReadLine();
            if (line == null || line.Substring(0, 2) != ReceiptRecordTypes.FileHeaderRecord)
                throw new ArgumentException("stream bad format");

            result.Header = FileHeaderRecord.FromString(line);

            while ((line = reader.ReadLine()) != null)
            {
                var type = line.Substring(0, 2);
                
                if (type == ReceiptRecordTypes.FileDetailRecord)
                {
                    var detail = FileDetailRecord.FromString(line);
                    result.Details.Add(detail);
                }
                else if (type == ReceiptRecordTypes.FileDetailAddendumRecord)
                {
                    var addendum = FileDetailAddendumRecord.FromString(line);
                    // reference last added detail record
                    result.Details.Last().Addendums.Add(addendum);
                }
                else if (type == ReceiptRecordTypes.FileControlRecord)
                {
                    result.Control = FileControlRecord.FromString(line);
                }
            }

            return result;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            ToString(builder);
            return builder.ToString();
        }

        public void ToString(StringBuilder builder)
        {
            Header.ToString(builder);
            Details.ForEach(d => d.ToString(builder));
            Control.ToString(builder);
        }
    }
}
