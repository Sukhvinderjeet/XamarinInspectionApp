using SQLite;
using System;
using System.Collections.Generic;

namespace CompanyX.Data.Model
{
    public class InspectionEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ServerId { get; set; }

        public string Location { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string DueDate { get; set; }

        public string Description { get; set; }

        public string InspectionDetails { get; set; }
        public string UserID { get; set; }                       
    
        public string Attachments { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
