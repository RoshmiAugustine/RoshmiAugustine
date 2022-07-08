// -----------------------------------------------------------------------
// <copyright file="UserDetailsDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class UserDetailsDTO
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int HelperID { get; set; }
        public string HelperEmail { get; set; }
        public string HelperExternalID { get; set; }
    }
}
