using System.Collections.Generic;
using System.Text;

namespace BankOfAmerica.Acknowledgement.Models.Item
{
    /// <summary>
    /// Contains the CheckLetter Header, Control and any Details
    /// Represents multiple records in an acknowledgement
    /// </summary>
    public class CashLetterRecord
    {
        public CashLetterRecord()
        {
            Details = new List<ItemDetailRecord>();
        }

        /// <summary>
        /// CheckLetter Header
        /// </summary>
        public CashLetterHeaderRecord Header { get; set; }

        /// <summary>
        /// List of Item Detail Records for this CashLetter Record
        /// </summary>
        public List<ItemDetailRecord> Details { get; set; }

        /// <summary>
        /// CheckLetter Control
        /// </summary>
        public CashLetterControlRecord Control { get; set; }

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
