using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.Input
{
    public class RemiderNotificationTriggerTimeDTO
    {
        public DateTime LastRunDatetime {get;set;}
        public DateTime CurrentRunDatetime { get; set; }
    }
}
