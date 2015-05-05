using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BankOfAmerica.Acknowledgement.Models.Item;
using BankOfAmerica.Acknowledgement.Resources;

namespace BankOfAmerica.Acknowledgement.Models
{
    public class ItemAcknowledgement
    {
        public ItemAcknowledgement()
        {
            CashLetters = new List<CashLetterRecord>();
        }

        public FileHeaderRecord Header { get; set; }
        public List<CashLetterRecord> CashLetters { get; set; }
        public FileControlRecord Control { get; set; }

        public static ItemAcknowledgement FromString(StreamReader reader)
        {
            var result = new ItemAcknowledgement();

            // first line should be header record
            var line = reader.ReadLine();
            if (line == null || line.Substring(0, 2) != ItemRecordTypes.FileHeaderRecord)
                throw new ArgumentException("stream bad format");

            result.Header = FileHeaderRecord.FromString(line);

            while ((line = reader.ReadLine()) != null)
            {
                var type = line.Substring(0, 2);

                if (type == ItemRecordTypes.CashLetterHeaderRecord)
                {
                    result.CashLetters.Add(new CashLetterRecord());

                    var header = CashLetterHeaderRecord.FromString(line);
                    result.CashLetters.Last().Header = header;
                }
                else if (type == ItemRecordTypes.ItemDetailRecord)
                {
                    var item = ItemDetailRecord.FromString(line);
                    result.CashLetters.Last().Details.Add(item);
                }
                else if (type == ItemRecordTypes.ItemDetailAddendumRecord)
                {
                    var addendum = ItemDetailAddendumRecord.FromString(line);
                    result.CashLetters.Last().Details.Last().Addendums.Add(addendum);
                }
                else if (type == ItemRecordTypes.CashLetterControlRecord)
                {
                    var control = CashLetterControlRecord.FromString(line);
                    result.CashLetters.Last().Control = control;
                }
                else if (type == ItemRecordTypes.FileControlRecord)
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
            CashLetters.ForEach(c => c.ToString(builder));
            Control.ToString(builder);
        }
    }
}
