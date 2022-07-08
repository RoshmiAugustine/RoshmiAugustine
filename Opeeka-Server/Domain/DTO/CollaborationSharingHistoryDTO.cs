using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class CollaborationSharingHistoryDTO
    {
        public int CollaborationSharingHistoryID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool HistoricalView { get; set; }
        public int? RemovedUserID { get; set; }
        public int? RemovedNoteID { get; set; }
        public int CollaborationSharingID { get; set; }

    }
}
