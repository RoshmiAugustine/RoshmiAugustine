using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Opeeka.PICS.Infrastructure.Enums;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class CollabrationAddDTOForExternal
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid therapy type")]
        public int TherapyTypeID { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.CollabName, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateName)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid collaboration level")]
        public int CollaborationLevelID { get; set; }

        [RegularExpression(PCISEnum.InputDTOValidationPattern.CollabCode, ErrorMessage = "Please enter a valid code with a length <= 50")]
        public string Code { get; set; }

        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.CollabAbbrev, ErrorMessage = "Please enter a valid abbreviation with length <= 20")]
        public string Abbreviation { get; set; }

        public string Description { get; set; }

        public List<CollaborationQuestionnaireDTOForExternal> Questionnaire { get; set; }

        public List<CollaborationTagDTOForExternal> Category { get; set; }

        public List<CollaborationLeadHistoryDTOForExternal> Lead { get; set; }
    }

    public class CollaborationQuestionnaireDTOForExternal
    {

        public int QuestionnaireID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [DefaultValue(true)]
        public bool IsReminderOn { get; set; }
    }

    public class CollaborationTagDTOForExternal
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a collaboration tag type")]
        public int CollaborationTagTypeID { get; set; }
    }

    public class CollaborationLeadHistoryDTOForExternal
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a collaboration lead user")]
        public int LeadUserID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class AddCollaborationResponseDTOForExternal
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public Guid Index { get; set; }

        public int Id { get; set; }

    }
}

