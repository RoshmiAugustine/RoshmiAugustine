// -----------------------------------------------------------------------
// <copyright file="QueryBuilder.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Newtonsoft.Json;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.ExternalAPI;
using Opeeka.PICS.Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Opeeka.PICS.Infrastructure.Common
{
    public class QueryBuilder : IQueryBuilder
    {
        public DynamicQueryBuilderDTO BuildQuery(string filterCriteria, List<QueryFieldMappingDTO> fieldMappingList)
        {
            var decoded = JsonConvert.DeserializeObject<DynamicFilters>(filterCriteria);
            string whereCondition = String.Empty;
            string orderby = String.Empty;
            List<QueryParameterDTO> sqlParameterKeyValues = new List<QueryParameterDTO>();
            DynamicQueryBuilderDTO queryBuilderDTO = new DynamicQueryBuilderDTO();
            // applying where condition
            if (decoded.filters.Count > 0)
            {
                foreach (var mainFilters in decoded.filters)
                {
                    int i = 1;
                    if (!String.IsNullOrEmpty(mainFilters.field))
                    {
                        var searchFieldMapping = fieldMappingList.Where(x => x.fieldAlias == mainFilters.field).FirstOrDefault();
                        if (searchFieldMapping != null && !String.IsNullOrEmpty(mainFilters.op)
                            && (mainFilters.op.ToLower() == "and" || mainFilters.op.ToLower() == "or"))
                        {
                            string searchField = String.Empty;
                            if (!String.IsNullOrEmpty(searchFieldMapping.fieldTable))
                            {
                                searchField = searchFieldMapping.fieldTable + ".";
                            }

                            searchField += searchFieldMapping.fieldName;

                            if(searchFieldMapping.orginalType!= null && searchFieldMapping.orginalType=="date")
                            {
                                searchField = "isnull(cast( " + searchField + " as Date), '1/1/1999')";
                            }

                            if (mainFilters.filters.Count() > 0)
                            {
                                whereCondition += " AND (";
                                foreach (var innerFilters in mainFilters.filters)
                                {

                                    string searchParam = "@" + searchFieldMapping.fieldAlias.Replace('.', '_') + i.ToString();
                                    string searchParam2 = "@" + searchFieldMapping.fieldAlias.Replace('.', '_') + i.ToString() + (i * 10).ToString();
                                    string betweenSearchParam = searchParam + " AND " + searchParam2;
                                    string searchOperator = " = ";
                                    string searchValue = innerFilters.operatorValue;
                                    switch (innerFilters.operatorKey.ToLower())
                                    {
                                        case "equals":
                                            searchOperator = " = ";
                                            break;
                                        case "greaterthan":
                                            searchOperator = " > ";
                                            break;
                                        case "greaterthanorequal":
                                            searchOperator = " >= ";
                                            break;
                                        case "lessthan":
                                            searchOperator = " < ";
                                            break;
                                        case "lessthanorequal":
                                            searchOperator = " <= ";
                                            break;
                                        case "notequals":
                                            searchOperator = " != ";
                                            break;
                                        case "startswith":
                                            searchOperator = " like ";
                                            searchValue = searchValue + "%";
                                            break;
                                        case "endswith":
                                            searchOperator = " like ";
                                            searchValue = "%" + searchValue;
                                            break;
                                        case "contains":
                                            searchOperator = " like ";
                                            searchValue = "%" + searchValue + "%";
                                            break;
                                        case "between":
                                            searchOperator = " between ";
                                            break;
                                    }
                                    if (innerFilters.operatorKey.ToLower() == "between")
                                    {
                                        var betweenValues = searchValue.Split('-');
                                        if (!String.IsNullOrEmpty(innerFilters.operatorKey) && !String.IsNullOrEmpty(innerFilters.operatorValue))
                                        {
                                            whereCondition += " " + searchField + searchOperator + betweenSearchParam + " ";
                                            whereCondition += " " + mainFilters.op.ToUpper();

                                        }
                                        sqlParameterKeyValues.Add(
                                        new QueryParameterDTO { Parameter = searchParam, ParameterValue = betweenValues[0].Trim() }
                                        );
                                        sqlParameterKeyValues.Add(
                                        new QueryParameterDTO { Parameter = searchParam2, ParameterValue = betweenValues[1].Trim() }
                                        );
                                    }
                                    else
                                    {
                                        if (!String.IsNullOrEmpty(innerFilters.operatorKey) && !String.IsNullOrEmpty(innerFilters.operatorValue))
                                        {
                                            whereCondition += " " + searchField + searchOperator + searchParam + " ";
                                            whereCondition += " " + mainFilters.op.ToUpper();

                                        }

                                        sqlParameterKeyValues.Add(
                                            new QueryParameterDTO { Parameter = searchParam, ParameterValue = searchValue }
                                            );
                                    }
                                    ++i;
                                }
                                whereCondition = whereCondition.TrimEnd((mainFilters.op.ToUpper()).ToCharArray());
                                whereCondition += " ) ";
                                queryBuilderDTO.WhereCondition = whereCondition;
                            }
                        }
                    }
                }

            }

            queryBuilderDTO.QueryParameterDTO = sqlParameterKeyValues;
            if (decoded.orderby != null && !String.IsNullOrEmpty(decoded.orderby.field))
            {

                string orderingCondition = "asc";
                var sortingfeilds = decoded.orderby.field.Split(',');
                var sortingcondition = decoded.orderby.value.Split(',');

                if (sortingfeilds.Count() == sortingcondition.Count() && sortingfeilds.Count() > 0)
                {
                    for (int index = 0; index < sortingfeilds.Length; index++)
                    {
                        var searchFieldMapping = fieldMappingList.Where(x => x.fieldAlias == sortingfeilds[index]).FirstOrDefault();
                        if (searchFieldMapping != null && !String.IsNullOrEmpty(searchFieldMapping.fieldName))
                        {
                            string sortField = String.Empty;
                            if (!String.IsNullOrEmpty(searchFieldMapping.fieldTable))
                            {
                                sortField = searchFieldMapping.fieldTable + ".";
                            }


                            sortField += searchFieldMapping.fieldName;
                            if (!String.IsNullOrEmpty(sortField) && !string.IsNullOrWhiteSpace(sortingcondition[index]))
                            {
                                if(sortingcondition[index].ToLower()=="desc")
                                {
                                    orderingCondition = "desc";
                                }

                                orderby += sortField + " " + orderingCondition;
                                if (index < sortingfeilds.Length - 1)
                                {
                                    orderby += ", ";
                                }
                            }

                        }
                    }
                    queryBuilderDTO.OrderBy = " order by " + orderby;

                }

            }
            queryBuilderDTO.Page = decoded.page;
            queryBuilderDTO.PageSize = decoded.size;
            queryBuilderDTO.Paginate = @" OFFSET " + ((decoded.page - 1) * decoded.size) + " ROWS FETCH NEXT " + decoded.size + " ROWS ONLY";
            return queryBuilderDTO;
        }

        /// <summary>
        /// BuildQueryForExternalAPI.Any new search field should be added in the switch case.
        /// </summary>
        /// <param name="searchInputDTO">searchInputDTO should be a class that inherits "Paginate"
        /// and have a property of name "SearchFields" which is an object of a class having the seach fileds defined in it</param>
        /// <param name="fieldMappingList"></param>
        /// <returns></returns>
        public DynamicQueryBuilderDTO BuildQueryForExternalAPI(dynamic searchInputDTO, List<QueryFieldMappingDTO> fieldMappingList)
        {
            var orderby = fieldMappingList.Take(1).ToList();
            fieldMappingList = fieldMappingList.Skip(1).ToList();
            DynamicQueryBuilderDTO dynamicQueryBuilderDTO = new DynamicQueryBuilderDTO();
            DynamicFilters dynamicFilters = new DynamicFilters();
            List<Filter> listFilter = new List<Filter>();
            List<QueryParameterDTO> sqlParameterKeyValues = new List<QueryParameterDTO>();
            var inputFieldProperties = searchInputDTO?.SearchFields?.GetType().GetProperties();
            dynamicFilters.page = searchInputDTO.PageNumber;
            dynamicFilters.size = searchInputDTO.PageSize;
            dynamicFilters.orderby = new Orderby() { field = orderby[0].fieldAlias, value = orderby[0].fieldType };
            if (inputFieldProperties != null)//searchfields in Request body
            {
                foreach (var field in inputFieldProperties)//loop each of the searchfields in Request body
                {
                    var value = field.GetValue(searchInputDTO.SearchFields)?.ToString();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        var operatorKey = string.Empty;
                        var fieldName = field.Name.ToLower();
                        switch (fieldName)
                        {
                            case "name":
                            case "collaborationname":
                                operatorKey = "contains";
                                break;
                            case "email":
                            case "externalid":
                            case "personindex":
                            case "helperindex":
                            case "collaborationindex":
                            case "questionnaireid":
                            case "collaborationid":
                            case "helperid":
                            case "personid":
                            case "assessmentid":
                            case "personsupportid":
                                operatorKey = "equals";
                                break;
                        }
                        List<InnerFilter> innerFilters = new List<InnerFilter>();
                        innerFilters.Add(new InnerFilter() { operatorKey = operatorKey, operatorValue = value });
                        listFilter.Add(new Filter() { field = field.Name, op = "AND", filters = innerFilters });
                    }
                }
            }
            dynamicFilters.filters = listFilter;
            dynamicQueryBuilderDTO = this.BuildQuery(JsonConvert.SerializeObject(dynamicFilters), fieldMappingList);
            return dynamicQueryBuilderDTO;
        }
        public class InnerFilter
        {
            public string operatorKey { get; set; }
            public string operatorValue { get; set; }

        }

        public class Filter
        {
            public string field { get; set; }
            public string op { get; set; }
            public List<InnerFilter> filters { get; set; }

        }

        public class Orderby
        {
            public string field { get; set; }
            public string value { get; set; }

        }

        public class DynamicFilters
        {
            public List<Filter> filters { get; set; }
            public Orderby orderby { get; set; }
            public int page { get; set; }
            public int size { get; set; }

        }

    }
}