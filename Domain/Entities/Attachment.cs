// -----------------------------------------------------------------------
// <copyright file="Attachment.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Opeeka.PICS.Domain.Entities
{
    public class Attachment
    {
        public int AttachmentId { get; set; }

        public string Name { get; set; }

        public String Attachments { get; set; }

        public int ContextType { get; set; }

    }
}
