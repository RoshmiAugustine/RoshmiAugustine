// -----------------------------------------------------------------------
// <copyright file="QuestionsResponseDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using AlertAutoResolveProcess.DTO;

namespace AlertAutoResolveProcess.Output
{
    public class ResponseDetails
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public ResponseDTO response { get; set; }
    }

    public class ResponseDetailsDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public ResponseDetails result { get; set; }
    }
}
