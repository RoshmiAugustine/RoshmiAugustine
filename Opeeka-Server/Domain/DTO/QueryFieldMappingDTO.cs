// -----------------------------------------------------------------------
// <copyright file="AddressDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Opeeka.PICS.Domain.DTO
{
    public class QueryFieldMappingDTO
    {
        public string fieldName { get; set; }
        public string fieldAlias { get; set; }
        public string fieldType { get; set; }
        public string fieldTable { get; set; }
        public string orginalType { get; set; }

    }
}
