using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{
    /// <summary>
    /// Store different to be TimeZones available within application.
    /// </summary>
    public class TimeZones : BaseEntity
    {
        public int TimeZonesID { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }

    }
    /// <summary>
    /// Store values for the type of Offset. 
    /// Values = Day/Hours.
    /// </summary>
    public class OffsetType : BaseEntity
    {
        public char OffsetTypeID { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }

    }
    /// <summary>
    /// Store values for Range of reccurence EndType.
    /// EndByEndate/EndByNumberOfOccurences/EndByNoEndate.
    /// </summary>
    public class RecurrenceEndType : BaseEntity
    {
        public int RecurrenceEndTypeID { get; set; }
        public string Name { get; set; }
        public string DisplayLabel { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    /// <summary>
    /// Store values for Different Recurrence pattern with their GroupNames.
    /// Groups = Daily/Weekly/Monthly/Yearly
    /// DailyDays/DailyWeekdays/Weekly/MonthlyByDay/MonthlyByOrdinal/YearlyByMonth/YearlyByOrdinal.
    /// </summary>
    public class RecurrencePattern: BaseEntity
    {
        public int RecurrencePatternID { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    /// <summary>
    /// Stores ordinal values in monthly and yearly recurrence pattern.
    /// First/Second/Third/Fourth/Last.
    /// </summary>
    public class RecurrenceOrdinal : BaseEntity
    {
        public int RecurrenceOrdinalID { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    /// <summary>
    /// Stores the name of days in a week.
    /// </summary>
    public class RecurrenceDay : BaseEntity
    {
        public int RecurrenceDayID { get; set; }
        public string Name { get; set; }
        public bool IsWeekDay { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    /// <summary>
    /// Store all month details.
    /// </summary>
    public class RecurrenceMonth : BaseEntity
    {
        public int RecurrenceMonthID { get; set; }
        public string Name { get; set; }
        public int NoOfDays { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
