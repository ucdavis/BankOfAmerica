using System.Collections.Generic;
using BankOfAmerica.Acknowledgement.Models.Item;

namespace BankOfAmerica.Acknowledgement.Models
{
    public class ItemAcknowledgement
    {
        public ItemAcknowledgement()
        {
            Details = new List<FileDetailRecord>();
        }

        public FileHeaderRecord Header { get; set; }
        public List<FileDetailRecord> Details { get; set; }
        public FileControlRecord Control { get; set; }
    }
}
