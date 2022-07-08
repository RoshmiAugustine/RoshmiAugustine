using System;
using System.Collections.Generic;
using System.Text;

namespace InviteToCompleteTriggerProcess.DTO
{
    public class ReminderInviteMailReceiversDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public InviteMailReceiversDetailsResponseDTO result { get; set; }
    }
    public class InviteMailReceiversDetailsResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public List<InviteMailReceiversInDetailDTO> iniviteToCompleteMailDetails { get; set; }
    }
    public class InviteMailReceiversInDetailDTO
    {
        public long PersonQuestionnaireID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int VoiceTypeID { get; set; }
        //Refers to the PK ID of voicetype /inviteReceiverstype selected
        public long? VoiceTypeFKID { get; set; }
        public string EmailID { get; set; }
        public bool TextPermission { get; set; }
        public bool EmailPermission { get; set; }
        public string Phone { get; set; }
        public string PhoneCode { get; set; }
        public string DisplayName { get; set; }
        public int AssessmentStatusID { get; set; }
        public int? HelperId { get; set; }
        public long? PersonId { get; set; }
        public Guid PersonIndex { get; set; }
        public string InviteReceiverType { get; set; }
        public long AgencyID { get; set; }
        public int InviteToCompleteReceiverID { get; set; }
    }
}
