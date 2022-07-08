// -----------------------------------------------------------------------
// <copyright file="AddressDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace EmailProcess.DTO
{
    public class SendEmail
    {

        //to email address (email)
        public string ToEmail { get; set; }
        public string ToDisplayName { get; set; }
        public string FromEmail { get; set; }
        public string FromDisplayName { get; set; }

        public string Body { get; set; }

        public string Subject { get; set; }
        public bool IsHtmlEmail { get; set; }

    }


}
