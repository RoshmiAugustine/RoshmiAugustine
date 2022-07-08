using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Opeeka.PICS.Domain.DTO.ExternalAPI
{

    public class AddPersonSupportDTOForExternal : PersonAddSupportForExternalDTO
    {

        [Range(1, long.MaxValue, ErrorMessage = "Please enter a valid PersonID")]
        public long PersonID { get; set; }

    }
    public class EditPersonSupportDTOForExternal : AddPersonSupportDTOForExternal
    {

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid PersonSupportID")]
        public int PersonSupportID { get; set; }

    }
    public class ExpirePersonSupportDTOForExternal 
    {       

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid PersonSupportID")]
        public int PersonSupportID { get; set; }
        /// <summary>
        /// If null then Endate = Today
        /// If a future date then Support.Iscurrent = true;
        /// If a date less than today then Support.Iscurrent = false;
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// If true then it will remove the support 
        /// </summary>
        public bool IsRemoved { get; set; } = false;

    }
    public class CRUDResponseDTOForExternalPersonSupport
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int Id { get; set; }

    }
    public class PersonSupportSearchInputDTO : Paginate
    {
        public PersonSupportSearchFields SearchFields { get; set; }
    }

    public class PersonSupportSearchFields
    {
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public string Name { get; set; }
        public Guid? PersonIndex { get; set; }
        public long? PersonId { get; set; }
        public int? PersonSupportId { get; set; }
    }

    public class PersonSupportResponseDTOForExternal
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public int TotalCount { get; set; }

        public List<SupportDetailsListDTO> PersonSupportDataList { get; set; }
    }

    public class SupportDetailsListDTO
    {
        public int PersonSupportID { get; set; }
        public long PersonID { get; set; }
        public Guid PersonIndex { get; set; }
        public int SupportTypeID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Email { get; set; }
        public string PhoneCode { get; set; }
        public string Phone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool TextPermission { get; set; }
        public bool EmailPermission { get; set; }
        public int TotalCount { get; set; }
    }
}
