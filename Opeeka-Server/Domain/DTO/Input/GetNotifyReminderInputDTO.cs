using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class GetNotifyReminderInputDTO
    {
        public List<long> personQuestionnaireScheduleIDList { get; set; }
    }
}
