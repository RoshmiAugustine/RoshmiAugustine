using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{
    public class HelperSearchInputDTO : Paginate
    {
        public HelperSearchFields SearchFields { get; set; }
    }

    public class HelperSearchFields
    {
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public Guid? HelperIndex { get; set; }
        public Guid? PersonIndex { get; set; }
        public int? HelperID { get; set; }
    }

    public class GetHelperResponseDTOForExternal
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<HelperDetailsListDTO> HelperDetailsList { get; set; }
    }

    public class HelperDetailsListDTO
    {
        public int HelperID { get; set; }

        public int? SupervisorHelperID { get; set; }

        public Guid? HelperIndex { get; set; }
        public Guid? SupervisorIndex { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int? HelperTitleID { get; set; }
        public string HelperTitle { get; set; }

        public int RoleId { get; set; }

        public long AgencyId { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public long StateId { get; set; }

        public string Zip { get; set; }

        public string Role { get; set; }

        public string Agency { get; set; }

        public string Supervisor { get; set; }

        public string State { get; set; }
        public int? CountryStateId { get; set; }
        public int? SystemRoleID { get; set; }
        public int? ReviewerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string HelperExternalID { get; set; }
        public int UserId { get; set; }
        public bool IsEmailReminderAlerts { get; set; }
        public int? CountryID { get; set; }
        public string Country { get; set; }
        public int TotalCount { get; set; }
    }

    public class HelperDetailsInputDTO
    {
        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.HelperName, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateFirstName)]
        public string FirstName { get; set; }

        [RegularExpression(PCISEnum.InputDTOValidationPattern.HelperName, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateMiddleName)]
        public string MiddleName { get; set; }

        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.HelperName, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateLastName)]
        public string LastName { get; set; }

        public int? HelperTitleID { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        public int UpdateUserID { get; set; }

        public long AgencyID { get; set; }

        public int? SupervisorHelperID { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public int? StateId { get; set; }

        public string Zip { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid ReviewerID.")]
        public int? ReviewerID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string HelperExternalID { get; set; }
        public bool IsEmailReminderAlerts { get; set; }
        public int? CountryId { get; set; }
        [DefaultValue(true)]
        public bool SendSignUpMail { get; set; }
    }

    public class HelperDetailsEditInputDTO
    {
        public Guid? HelperIndex { get; set; }

        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.HelperName, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateFirstName)]
        public string FirstName { get; set; }

        [RegularExpression(PCISEnum.InputDTOValidationPattern.HelperName, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateMiddleName)]
        public string MiddleName { get; set; }

        [Required]
        [RegularExpression(PCISEnum.InputDTOValidationPattern.HelperName, ErrorMessage = PCISEnum.InputDTOValidationMessages.ValidateLastName)]
        public string LastName { get; set; }

        public int? HelperTitleID { get; set; }

        [Required]
        public int RoleId { get; set; }

        public int UpdateUserID { get; set; }

        public long AgencyID { get; set; }

        public int? SupervisorHelperID { get; set; }
        public string Phone1 { get; set; }
         public string Phone2 { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public int? StateId { get; set; }

        public string Zip { get; set; }

        [Required]
        public int? ReviewerID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string HelperExternalID { get; set; }
        public bool IsEmailReminderAlerts { get; set; }
        public int? CountryId { get; set; }
    }

    public class CRUDResponseDTOForExternal
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }
       
        public Guid Index { get; set; }

        public int Id { get; set; }

    }
}
