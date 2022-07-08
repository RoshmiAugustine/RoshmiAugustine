// -----------------------------------------------------------------------
// <copyright file="AgencyDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace Opeeka.PICS.Domain.DTO
{
    public class AgencyDetailsDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public Guid AgencyIndex { get; set; }

        public Guid AddressIndex { get; set; }

        public long AgencyID { get; set; }

        public long AddressID { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Please enter a valid UpdateUserID")]
        public int UpdateUserID { get; set; }

        public string Note { get; set; }

        public string Abbrev { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Email { get; set; }

        public string ContactLastName { get; set; }

        public string ContactFirstName { get; set; }


        public string Address1 { get; set; }

        public string Address2 { get; set; }


        public string Zip { get; set; }

        public string Zip4 { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a valid CountryStateID")]
        public int? CountryStateID { get; set; }

        public string City { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid CountryID")]
        public int? CountryId { get; set; }


    }
}
