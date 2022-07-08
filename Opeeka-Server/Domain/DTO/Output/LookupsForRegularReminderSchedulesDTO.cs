using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Output
{
    /// <summary>
    /// RegularReminderScheduleLookupDTO.
    /// </summary>
    public class RegularReminderScheduleLookupDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<TimeZonesDTO> TimeZones { get; set; }
        public List<OffsetTypeDTO> OffsetType { get; set; }
        public List<RecurrenceEndTypeDTO> RecurrenceEndType { get; set; }
        public List<RecurrenceOrdinalDTO> RecurrenceOrdinal { get; set; }
        public List<RecurrenceDayDTO> RecurrenceDay { get; set; }
        public List<RecurrenceMonthDTO> RecurrenceMonth { get; set; }
        public List<InviteToCompleteReceiverDTO> InviteToCompleteReceiver { get; set; }
        public List<RecurrencePatternGroupDTO> RecurrencePattern { get; set; }
    }

    public class TimeZonesDTO
    {
        public int TimeZonesID { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }

    }
    public class OffsetTypeDTO
    {
        public char OffsetTypeID { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }

    }
    public class RecurrenceEndTypeDTO
    {
        public int RecurrenceEndTypeID { get; set; }
        public string Name { get; set; }
        public string DisplayLabel { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    public class RecurrencePatternDTO
    {
        public int RecurrencePatternID { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    public class RecurrenceOrdinalDTO
    {
        public int RecurrenceOrdinalID { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    public class RecurrenceDayDTO
    {
        public int RecurrenceDayID { get; set; }
        public string Name { get; set; }
        public bool IsWeekDay { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }        
    public class RecurrenceMonthDTO
    {
        public int RecurrenceMonthID { get; set; }
        public string Name { get; set; }
        public int NoOfDays { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    public class InviteToCompleteReceiverDTO
    {
        public int InviteToCompleteReceiverID { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    public class RecurrencePatternGroupDTO
    {
        public string GroupName { get; set; }
        public List<RecurrencePatternDTO> Pattern { get; set; }
    }
}
