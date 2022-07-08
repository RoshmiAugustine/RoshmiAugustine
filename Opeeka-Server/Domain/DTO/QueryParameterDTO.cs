// -----------------------------------------------------------------------
// <copyright file="AddressDTO.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Opeeka.PICS.Domain.DTO
{
    public class QueryParameterDTO
    {
        public string Parameter { get; set; }
        public string ParameterValue { get; set; }

    }

    public class DynamicQueryBuilderDTO
    {
        public string WhereCondition { get; set; }
        public string OrderBy { get; set; }
        public string Paginate { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<QueryParameterDTO> QueryParameterDTO { get; set; }
    }
}
