using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{
    /// <summary>
    /// Store Values for Regular Reminder Schedule - PCIS-3225
    /// </summary>
    public class QuestionnaireRegularReminderRecurrence : BaseEntity
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

        public RecurrenceEndType RecurrenceRangeEndType { get; set; }
        public QuestionnaireWindow QuestionnaireWindow { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public RecurrencePattern RecurrencePattern { get; set; }
    }

    /// <summary>
    /// Store the Specific time scheduled for a Questionnaire Regular Reminder.
    /// </summary>
    public class QuestionnaireRegularReminderTimeRule : BaseEntity
    {
        public int QuestionnaireRegularReminderTimeRuleID { get; set; }
        public int QuestionnaireID { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string AMorPM { get; set; }
        public int TimeZonesID { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }

        public TimeZones TimeZones { get; set; }
        public Questionnaire Questionnaire { get; set; }
    }
}
