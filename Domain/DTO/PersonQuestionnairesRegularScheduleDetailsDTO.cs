using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO
{
    public class PersonQuestionnairesRegularScheduleResponse
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<PersonQuestionnaireRegularScheduleDetailsDTO> PersonQuestionnaireSchedules { get; set; }
        public List<long> list_PersonQuestionnaireIds { get; set; }
        public List<int> list_QuestionnaireIds { get; set; }
    }

    public class PersonQuestionnaireRegularScheduleDetailsDTO
    {

        public long PersonQuestionnaireID { get; set; }
        public DateTime CurrentlyScheduled { get; set; }
        public DateTime? ToBeScheduled { get; set; }
        public DateTime TwelveMonthsFromToday { get; set; }
        public int QuestionnaireWindowID { get; set; }
        public DateTime WindowDueDate { get; set; }
        public int QuestionnaireID { get; set; }

        public int DueDateOffsetDays { get; set; }
        public int WindowOpenOffsetDays { get; set; }
        public int WindowCloseOffsetDays { get; set; }
        public int? RepeatIntervalDays { get; set; }
        public bool IsSelected { get; set; }
        public DateTime? WindowOpenDate { get; set; }
        public DateTime? WindowCloseDate { get; set; }
        public char? OpenOffsetTypeID { get; set; }
        public char? CloseOffsetTypeID { get; set; }
        public int OccurrenceCounter { get; set; }
    }
}
