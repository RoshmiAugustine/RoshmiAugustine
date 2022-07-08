using System;
using System.Collections.Generic;
using System.Text;

namespace Opeeka.PICS.Domain.Entities
{
    /// <summary>
    /// Store Values of Receivers of InviteToComplete mail.
    /// </summary>
    public class InviteToCompleteReceiver : BaseEntity
    {
        public int InviteToCompleteReceiverID { get; set; }
        public string Name { get; set; }
        public int ListOrder { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
