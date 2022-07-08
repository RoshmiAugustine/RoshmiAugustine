// -----------------------------------------------------------------------
// <copyright file="IUtility.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Opeeka.PICS.Domain.DTO;
using System.Collections.Generic;

namespace Opeeka.PICS.Domain.Interfaces.Common
{
    public interface IQueryBuilder
    {
        DynamicQueryBuilderDTO BuildQuery(string filterCriteria, List<QueryFieldMappingDTO> fieldMappingList);

        /// <summary>
        /// BuildQueryForExternalAPI.Any new search field should be added in the switch case.
        /// </summary>
        /// <param name="searchInputDTO">searchInputDTO should be a class that inherits "Paginate"
        /// and have a property of name "SearchFields" which is an object of a class having the seach fileds defined in it</param>
        /// <param name="fieldMappingList"></param>
        /// <returns></returns>
        DynamicQueryBuilderDTO BuildQueryForExternalAPI(dynamic searchInputDTO, List<QueryFieldMappingDTO> fieldMappingList);
    }
}
