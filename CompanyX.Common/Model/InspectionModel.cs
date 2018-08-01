using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyX.Common.Model
{
    public class InspectionModel
    {
        public int Id { get; set; }

        public int ServerId { get; set; }


        public string Location { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string DueDate { get; set; }

        public string Description { get; set; }

        public string InspectionDetails { get; set; }
        public string UserID { get; set; }

        public List<string> Details { get; set; }

        public List<byte[]> Attachments { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
