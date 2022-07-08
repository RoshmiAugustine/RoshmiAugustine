// -----------------------------------------------------------------------
// <copyright file="HTMLToPDFResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO.Output
{
    public class HTMLToPDFResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public byte[] PDFByteArray { get; set; }
    }
}
