using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class QuestionnaireRegularReminderRecurrenceDTO : RecurrenceLookupsInDetailDTO
    {
        public int QuestionnaireRegularReminderRecurrenceID { get; set; }
        public int QuestionnaireID { get; set; }
        public int? QuestionnaireWindowID { get; set; }
        public int RecurrencePatternID { get; set; }
        public int RecurrenceInterval { get; set; }
        /// <summary>
        /// Comma seperated RecurrenceOrdinalIDs in case of MonthlyByOrdinal/yearlyByOrdinal.
        /// </summary>
        public string RecurrenceOrdinalIDs { get; set; }
        /// <summary>
        /// Comma seperated RecurrenceDayNameIDs in case of weekly/MonthlyByOrdinal/YearlyByOrdinal.
        /// </summary>
        public string RecurrenceDayNameIDs { get; set; }
        /// <summary>
        /// Nth Day of a Month in case of MonthlyByDay/YearlyByMonth.
        /// </summary>
        public int? RecurrenceDayNoOfMonth { get; set; }
        /// <summary>
        /// Comma seperated RecurrenceMonthIDs in case of YearlyByMonth/YearlyByOrdinal
        /// </summary>
        public string RecurrenceMonthIDs { get; set; }
        public DateTime RecurrenceRangeStartDate { get; set; }
        public int RecurrenceRangeEndTypeID { get; set; }
        /// <summary>
        /// If RecurrenceRangeEndTypeID = EndByEndate then its should store an date value.
        /// </summary>
        public DateTime? RecurrenceRangeEndDate { get; set; }
        /// <summary>
        /// If RecurrenceRangeEndTypeID = EndByNumberOfOccurences then its should store an integer counter value.
        /// </summary>
        public int? RecurrenceRangeEndInNumber { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class QuestionnaireRegularReminderTimeRuleDTO
    {
        public int QuestionnaireRegularReminderTimeRuleID { get; set; }
        public int QuestionnaireID { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string AMorPM { get; set; }
        public int TimeZonesID { get; set; }
        public string TimeZoneName { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    public class RecurrenceLookupsInDetailDTO
    {
        public string RecurrencePattern { get; set; }
        public string PatternGroup { get; set; }
        /// <summary>
        /// JSON value - List<string>
        /// </summary>
        public string RecurrenceOrdinals { get; set; }
        /// <summary>
        /// JSON value - List<string>
        /// </summary>
        public string RecurrenceDayNames { get; set; }
        /// <summary>
        /// JSON value - List<string>
        /// </summary>
        public string RecurrenceMonths { get; set; }
        public string RecurrenceRangeEndType { get; set; }
    }
}
