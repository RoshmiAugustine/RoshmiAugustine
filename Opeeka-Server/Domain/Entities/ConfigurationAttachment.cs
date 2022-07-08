// -----------------------------------------------------------------------
// <copyright file="ConfigurationAttachment.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Opeeka.PICS.Domain.Entities
{
    public class ConfigurationAttachment : BaseEntity
    {
        public int ConfigurationAttachmentID { get; set; }

        public int ConfigurationID { get; set; }

        public int AttachmentID { get; set; }

        public Attachment Attachement { get; set; }
        public Configuration Configuration { get; set; }
    }
}
