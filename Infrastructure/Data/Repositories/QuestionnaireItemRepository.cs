// -----------------------------------------------------------------------
// <copyright file="QuestionnaireItemRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.DTO.Input;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class QuestionnaireItemRepository : BaseRepository<QuestionnaireItem>, IQuestionnaireItemRepository
    {
        private readonly IMapper mapper;
        private readonly ILogger<QuestionnaireItemRepository> logger;
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonCollaborationRepository"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public QuestionnaireItemRepository(ILogger<QuestionnaireItemRepository> logger, OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this._dbContext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// GetQuestionnaireDetails.
        /// </summary>
        /// <param name="questionnaireIndex"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<QuestionnaireDetailsDTO> GetQuestionnaireDetails(QuestionnaireDetailsSearchDTO detailsSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO)
        {

            string query = $@"select
                            QuestionnaireItemID as id,v.CategoryId as category, cat.Abbrev as category_value, i.Name as itemName, I.ListOrder as listOrder, 
                            i.ItemResponseTypeId as 'property', p.Name as property_value, CanOverrideLowerResponseBehavior as minOption, LowerResponseValue as minDefault, 
                            r4.value as minDefault_value, LowerItemResponseBehaviorId as minThreshold, r.Name as minThreshold_value, CanOverrideUpperResponseBehavior as maxOption, 
                            UpperResponseValue as maxDefault,  r5.value as maxDefault_value,  UpperItemResponseBehaviorId as maxThreshold, r3.Name as maxThreshold_value, 
                            CanOverrideMedianResponseBehavior as altOption, MedianItemResponseBehaviorId as altDefault, r2.Name as altDefault_value, IsOptional as focus,i.ItemId,
                            v.StartDate,v.EndDate,v.LowerResponseValue,v.UpperResponseValue,i.ResponseValueTypeID,res.value as minRange,res.maxrangevalue as maxrange, COUNT(*) OVER() AS TotalCount 
                            FROM QuestionnaireItem v 
                            left join info.ItemResponseBehavior r on r.ItemResponseBehaviorId = v.LowerItemResponseBehaviorId 
                            left join info.ItemResponseBehavior r2 on r2.ItemResponseBehaviorId = v.[MedianItemResponseBehaviorId] 
                            left join info.ItemResponseBehavior r3 on r3.ItemResponseBehaviorId = v.[UpperItemResponseBehaviorId] 
                            left join Item i on i.ItemId = v.ItemId and i.isRemoved=0
                            join info.ResponseValueType RV  on I.ResponseValueTypeID = RV.ResponseValueTypeID
							left join response res on res.itemID=i.itemID and i.ResponseValueTypeID=3 and res.IsRemoved=0
                            left join info.ItemResponseType p on i.ItemResponseTypeId = p.ItemResponseTypeId 
                            left join Response r4 on v.LowerResponseValue = r4.ResponseId and r4.IsRemoved=0
                            left join Response r5 on v.UpperResponseValue = r5.ResponseId and r5.IsRemoved=0
                            left join info.Category cat on cat.CategoryId = v.CategoryId 
                            where RV.Name  NOT IN ({PCISEnum.ResponseValueType.ExcludedValueTypes}) and v.IsRemoved = 0 AND v.QuestionnaireID = {detailsSearchDTO.questionId}";


            // query += @" OFFSET " + ((detailsSearchDTO.pageNumber - 1) * detailsSearchDTO.pageLimit) + // "ROWS FETCH NEXT " + detailsSearchDTO.pageLimit + " ROWS ONLY";
            query += queryBuilderDTO.WhereCondition + queryBuilderDTO.OrderBy + queryBuilderDTO.Paginate;

            var data = ExecuteSqlQuery(query, x => new QuestionnaireDetailsDTO
            {
                QuestionnaireItemID = x["id"] == DBNull.Value ? 0 : (int)x["id"],
                CategoryID = x["category"] == DBNull.Value ? 0 : (int)x["category"],
                CatagoryAbbrev = x["category_value"] == DBNull.Value ? null : (string)x["category_value"],
                ItemName = x["itemName"] == DBNull.Value ? null : (string)x["itemName"],
                ListOrder = x["listOrder"] == DBNull.Value ? 0 : (int)x["listOrder"],
                Property = x["property"] == DBNull.Value ? 0 : (int)x["property"],
                PropertyValue = x["property_value"] == DBNull.Value ? null : (string)x["property_value"],
                MinOption = x["minOption"] == DBNull.Value ? false : (bool)x["minOption"],
                MinDefault = x["minDefault"] == DBNull.Value ? 0 : (int)x["minDefault"],
                MinDefaultValue = x["minDefault_value"] == DBNull.Value ? 0 : (decimal)x["minDefault_value"],
                MinThreshold = x["minThreshold"] == DBNull.Value ? 0 : (int)x["minThreshold"],
                MinThresholdValue = x["minThreshold_value"] == DBNull.Value ? null : (string)x["minThreshold_value"],
                MaxOption = x["maxOption"] == DBNull.Value ? false : (bool)x["maxOption"],
                MaxDefault = x["maxDefault"] == DBNull.Value ? 0 : (int)x["maxDefault"],
                MaxDefaultValue = x["maxDefault_value"] == DBNull.Value ? 0 : (decimal)x["maxDefault_value"],
                MaxThreshold = x["maxThreshold"] == DBNull.Value ? 0 : (int)x["maxThreshold"],
                MaxThresholdValue = x["maxThreshold_value"] == DBNull.Value ? null : (string)x["maxThreshold_value"],
                AltOption = x["altOption"] == DBNull.Value ? false : (bool)x["altOption"],
                AltDefault = x["altDefault"] == DBNull.Value ? 0 : (int)x["altDefault"],
                AltDefaultValue = x["altDefault_value"] == DBNull.Value ? null : (string)x["altDefault_value"],
                Focus = x["focus"] == DBNull.Value ? false : (bool)x["focus"],
                ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                StartDate = x["StartDate"] == DBNull.Value ? null : (DateTime?)x["StartDate"],    
                EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"],
                LowerResponseValue = x["LowerResponseValue"] == DBNull.Value ? 0 : (int)x["LowerResponseValue"],
                UpperResponseValue = x["UpperResponseValue"] == DBNull.Value ? 0 : (int)x["UpperResponseValue"],
                ResponseValueTypeID = x["ResponseValueTypeID"] == DBNull.Value ? 0 : (int)x["ResponseValueTypeID"],
                MinRange = x["minRange"] == DBNull.Value ? 0 : (decimal)x["minRange"],
                MaxRange = x["maxrange"] == DBNull.Value ? 0 : (int)x["maxrange"],
                TotalCount = x["TotalCount"] == DBNull.Value ? 0 : (int)x["TotalCount"],
            }, queryBuilderDTO.QueryParameterDTO);
            return data;
        }

        /// <summary>
        /// GetQuestionnaireDetailsCount.
        /// </summary>
        /// <param name="questionnaireIndex"></param>
        /// <returns></returns>
        public int GetQuestionnaireDetailsCount(int id)
        {

            string query = @"select Count(*)
                            From QuestionnaireItem v
                            left join info.ItemResponseBehavior r on r.ItemResponseBehaviorId = v.LowerItemResponseBehaviorId 
                            left join info.ItemResponseBehavior r2 on r2.ItemResponseBehaviorId = v.[MedianItemResponseBehaviorId] 
                            left join info.ItemResponseBehavior r3 on r3.ItemResponseBehaviorId = v.[UpperItemResponseBehaviorId] 
                            left join Item i on i.ItemId = v.ItemId 
                            left join info.ItemResponseType p on i.ItemResponseTypeId = p.ItemResponseTypeId 
                            left join Response r4 on v.LowerItemResponseBehaviorID  = r4.ResponseId 
                            left join Response r5 on v.UpperItemResponseBehaviorID  = r5.ResponseId 
                            left join info.Category cat on cat.CategoryId = v.CategoryId 
                            where v.QuestionnaireID = " + id;


            var data = ExecuteSqlQuery(query, x => new QuestionnaireDetailsDTO
            {
                TotalCount = (int)x[0]
            });

            if (data != null && data.Count > 0)
            {
                return data[0].TotalCount;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// GetQuestionnaireItems
        /// </summary>
        /// <param name="questionnaireItemID"></param>
        /// <returns>QuestionnaireItemsDTO</returns>
        public async Task<QuestionnaireItemsDTO> GetQuestionnaireItems(int questionnaireItemID)
        {
            try
            {
                QuestionnaireItemsDTO questionnaireItemsDTO = new QuestionnaireItemsDTO();
                QuestionnaireItem questionnaireItems = await this.GetRowAsync(x => x.QuestionnaireItemID == questionnaireItemID);
                this.mapper.Map<QuestionnaireItem, QuestionnaireItemsDTO>(questionnaireItems, questionnaireItemsDTO);
                return questionnaireItemsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateQuestionnaireItem
        /// </summary>
        /// <param name="questionnaireItemDTO"></param>
        /// <returns>QuestionnaireItemsDTO</returns>
        public QuestionnaireItemsDTO UpdateQuestionnaireItem(QuestionnaireItemsDTO questionnaireItemDTO)
        {
            try
            {
                QuestionnaireItem questionnaireItem = new QuestionnaireItem();
                this.mapper.Map<QuestionnaireItemsDTO, QuestionnaireItem>(questionnaireItemDTO, questionnaireItem);
                var result = this.UpdateAsync(questionnaireItem).Result;
                this.mapper.Map<QuestionnaireItem, QuestionnaireItemsDTO>(result, questionnaireItemDTO);
                this._cache.Delete(PCISEnum.Caching.GetAllQuestionnaireItems);
                return questionnaireItemDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetQuestionnaireItemsByQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireItemsDTO</returns>
        public List<QuestionnaireItemsDTO> GetQuestionnaireItemsByQuestionnaire(int questionnaireID)
        {
            try
            {
                List<QuestionnaireItemsDTO> questionnaireItemsDTO = new List<QuestionnaireItemsDTO>();
                var questionnaireItems = this._dbContext.QuestionnaireItems.Where(x => x.QuestionnaireID == questionnaireID).ToList();
                this.mapper.Map<List<QuestionnaireItem>, List<QuestionnaireItemsDTO>>(questionnaireItems, questionnaireItemsDTO);
                return questionnaireItemsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// CloneQuestionnaireItem
        /// </summary>
        /// <param name="questionnaireItemDTO"></param>
        /// <returns>QuestionnaireItemsDTO</returns>
        public void CloneQuestionnaireItem(List<QuestionnaireItemsDTO> questionnaireItemsDTO)
        {
            try
            {
                List<QuestionnaireItem> questionnaireItemList = new List<QuestionnaireItem>();
                this.mapper.Map<List<QuestionnaireItemsDTO>, List<QuestionnaireItem>>(questionnaireItemsDTO, questionnaireItemList);
                this.AddBulkAsync(questionnaireItemList).Wait();
                this._cache.Delete(PCISEnum.Caching.GetAllQuestionnaireItems);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
