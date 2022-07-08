using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using Opeeka.PICS.Infrastructure.Data.Repositories.Base;
using Opeeka.PICS.Infrastructure.Enums;
using Opeeka.PICS.Domain.Interfaces;
using Opeeka.PICS.Domain.DTO.Output;

namespace Opeeka.PICS.Infrastructure.Data.Repositories
{
    public class LookupRepository : BaseRepository<CountryState>, ILookupRepository
    {
        private readonly IMapper mapper;
        private readonly OpeekaDBContext _dbContext;
        private readonly ICache _cache;

        public LookupRepository(OpeekaDBContext dbContext, IMapper mapper, ICache cache)
            : base(dbContext)
        {
            this.mapper = mapper;
            this._dbContext = dbContext;
            this._cache = cache;
        }

        /// <summary>
        /// To add agent details.
        /// </summary>
        /// <returns> CountryStateDTO.</returns>
        public async Task<List<CountryStateDTO>> GetAllState()
        {
            try
            {
                var readFromCache = this._cache.Get<List<CountryStateDTO>>(PCISEnum.Caching.GetAllState);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    CountryStateDTO countryStateDTO = new CountryStateDTO();
                    var countryState = await this.GetAsync(x => !x.IsRemoved);
                    var response = this.mapper.Map<List<CountryStateDTO>>(countryState.OrderBy(x => x.ListOrder));
                    this._cache.Post(PCISEnum.Caching.GetAllState, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all countries
        /// </summary>
        /// <returns> CountryStateDTO.</returns>
        public List<CountryLookupDTO> GetAllCountries()
        {
            try
            {
                var readFromCache = this._cache.Get<List<CountryLookupDTO>>(PCISEnum.Caching.GetAllCountries);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var countries = _dbContext.Countries.Where(x => !x.IsRemoved).OrderBy(s => s.ListOrder).ToList();
                    var response = this.mapper.Map<List<CountryLookupDTO>>(countries);
                    this._cache.Post(PCISEnum.Caching.GetAllCountries, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllRolesLookup.
        /// </summary>
        /// <returns>RoleLookupResponseDTO.</returns>
        public List<RoleLookupDTO> GetAllRolesLookup()
        {
            try
            {
                var readFromCache = this._cache.Get<List<RoleLookupDTO>>(PCISEnum.Caching.GetAllRolesLookup);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var response = _dbContext.SystemRoles.Where(x => !x.IsRemoved && !x.IsExternal).OrderBy(s => s.ListOrder).
                    Select(x => new RoleLookupDTO { SystemRoleID = x.SystemRoleID, Name = x.Name, ListOrder = x.ListOrder, IsExternal = x.IsExternal }).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllRolesLookup, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllHelperLookup.
        /// </summary>
        /// <returns>RoleLookupResponseDTO.</returns>
        public List<HelperLookupDTO> GetAllHelperLookup()
        {
            try
            {
                var readFromCache = this._cache.Get<List<HelperLookupDTO>>(PCISEnum.Caching.GetAllHelperLookup);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    List<HelperLookupDTO> response = new List<HelperLookupDTO>();
                    string query = string.Empty;
                    query = @"select HelperID, HelperIndex, FirstName+ ' '+isnull(MiddleName+' ', '')+ LastName as HelperName from helper
                                    where IsRemoved = 0" +
                                    " AND ISNULL(EndDate, CAST(GETDATE() AS DATE))>= CAST(GETDATE() AS DATE)" +
                                    "order by HelperName";

                    var data = ExecuteSqlQuery(query, x => new HelperLookupDTO
                    {
                        HelperID = x[0] == DBNull.Value ? 0 : (int)x[0],
                        HelperIndex = x[1] == DBNull.Value ? null : (Guid?)x[1],
                        HelperName = x[2] == DBNull.Value ? null : (string)x[2],
                    }).OrderBy(y => y.HelperName).ToList();

                    this._cache.Post(PCISEnum.Caching.GetAllHelperLookup, readFromCache = data);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllItemResponseBehavior.
        /// </summary>
        /// <returns>ItemResponseBehaviorResponseDTO.</returns>
        public List<ItemResponseBehaviorDTO> GetAllItemResponseBehavior()
        {
            try
            {
                List<ItemResponseBehaviorDTO> readFromCache = this._cache.Get<List<ItemResponseBehaviorDTO>>(PCISEnum.Caching.GetAllItemResponseBehavior);
                if (readFromCache == null || readFromCache?.Count == 0)
                {

                    string query = $@"select I.ItemResponseBehaviorID,I.ItemResponseTypeID,I.Name, I.ListOrder,I.Abbrev,I.Description
                                  from info.ItemResponseBehavior I where I.IsRemoved = 0 Order By I.ListOrder";

                    var response = ExecuteSqlQuery(query, x => new ItemResponseBehaviorDTO
                    {
                        ItemResponseBehaviorID = x["ItemResponseBehaviorID"] == DBNull.Value ? 0 : (int)x["ItemResponseBehaviorID"],
                        ItemResponseTypeID = x["ItemResponseTypeID"] == DBNull.Value ? 0 : (int)x["ItemResponseTypeID"],
                        Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                        ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                        Abbrev = x["Abbrev"] == DBNull.Value ? null : (string)x["Abbrev"],
                        Description = x["Description"] == DBNull.Value ? null : (string)x["Description"],
                    }).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllItemResponseBehavior, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllItemResponseType.
        /// </summary>
        /// <returns>ItemResponseTypeDTO.</returns>
        public List<ItemResponseTypeDTO> GetAllItemResponseType()
        {
            try
            {
                var readFromCache = this._cache.Get<List<ItemResponseTypeDTO>>(PCISEnum.Caching.GetAllItemResponseType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var response = _dbContext.ItemResponseTypes.Where(x => !x.IsRemoved).
                    Select(x => new ItemResponseTypeDTO
                    {
                        ItemResponseTypeID = x.ItemResponseTypeID,
                        Name = x.Name,
                        ListOrder = x.ListOrder,
                        Abbrev = x.Abbrev,
                        Description = x.Description
                    }).OrderBy(y => y.ListOrder).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllItemResponseType, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// To get all type of Collaboration.
        /// </summary>
        /// <returns> CollaborationTypesResponseDTO.</returns>
        public List<CollaborationDataDTO> GetAllCollaboration()
        {
            try
            {
                var readFromCache = this._cache.Get<List<CollaborationDataDTO>>(PCISEnum.Caching.GetAllCollaboration);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var response = _dbContext.Collaborations.Where(x => !x.IsRemoved).
                    Select(x => new CollaborationDataDTO { CollaborationID = x.CollaborationID, Name = x.Name, CollaborationIndex = x.CollaborationIndex, Code = x.Code })
                    .OrderBy(y => y.Name).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllCollaboration, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// GetAllNotificationLevel.
        /// </summary>
        /// <returns>NotificationLevelResponseDTO.</returns>
        public List<NotificationLevelDTO> GetAllNotificationLevel()
        {
            try
            {
                var readFromCache = this._cache.Get<List<NotificationLevelDTO>>(PCISEnum.Caching.GetAllNotificationLevel);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var response = _dbContext.NotificationLevels.Where(x => !x.IsRemoved).
                                Select(x => new NotificationLevelDTO
                                {
                                    NotificationLevelID = x.NotificationLevelID,
                                    NotificationTypeID = x.NotificationTypeID,
                                    Name = x.Name,
                                    ListOrder = x.ListOrder,
                                    Abbrev = x.Abbrev,
                                    Description = x.Description
                                }).OrderBy(y => y.ListOrder).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllNotificationLevel, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnaireItems.
        /// </summary>
        /// <param name="id">Questionnaire ID.</param>
        /// <returns>QuestionnaireItemsResponseDTO.</returns>
        public List<QuestionnaireItemDTO> GetAllQuestionnaireItems(int id)
        {
            try
            {
                List<QuestionnaireItemDTO> questionnaireItemDTO = new List<QuestionnaireItemDTO>();
                var readFromCache = this._cache.Get<List<QuestionnaireItemDTO>>(PCISEnum.Caching.GetAllQuestionnaireItems);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    string query = $@"select Q.QuestionnaireItemID,Q.QuestionnaireID,I.ItemID,I.Name , I.ListOrder,I.ResponseValueTypeID
                                from QuestionnaireItem Q
                                join Item I on Q.ItemID=I.ItemID 
								join info.ResponseValueType RV  ON I.ResponseValueTypeID = RV.ResponseValueTypeID
                           WHERE RV.Name NOT IN ({PCISEnum.ResponseValueType.ExcludedValueTypes})";


                    var data = ExecuteSqlQuery(query, x => new QuestionnaireItemDTO
                    {
                        QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? 0 : (int)x["QuestionnaireItemID"],
                        QuestionnaireID = x["QuestionnaireID"] == DBNull.Value ? 0 : (int)x["QuestionnaireID"],
                        ItemID = x["ItemID"] == DBNull.Value ? 0 : (int)x["ItemID"],
                        Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                        ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                        ResponseValueTypeID = x["ResponseValueTypeID"] == DBNull.Value ? 0 : (int)x["ResponseValueTypeID"],
                    });
                    this._cache.Post(PCISEnum.Caching.GetAllQuestionnaireItems, readFromCache = data);
                }
                questionnaireItemDTO = readFromCache?.Where(x => x.QuestionnaireID == id).OrderBy(x => x.ListOrder).ToList();
                return questionnaireItemDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnaireItems.
        /// </summary>
        /// <param name="id">Questionnaire ID.</param>
        /// <returns>QuestionnaireItemsResponseDTO.</returns>
        public List<SkipLogicItemsDTO> GetCategoryAndItemforSkipLogic(int questionnireId)
        {
            try
            {
                List<SkipLogicItemsDTO> questionnaireItemDTO = new List<SkipLogicItemsDTO>();
                string query = $@"select * from (select QI.QuestionnaireItemID, null as CategoryID, 2 as ActionTypeID, I.ItemID, I.Name, I.ListOrder, null as ParentItemID,null as ParentName from QuestionnaireItem QI
                                    join item I on QI.ItemID=I.itemID
                                    where QI.QuestionnaireID= { questionnireId }
                                    union
                                    select Distinct null as QuestionnaireItemID, C.CategoryID, 1 as ActionTypeID, null as ItemID, C.Name,c.ListOrder,null as ParentItemID, null as ParentName from QuestionnaireItem QI
                                    join info.Category C on QI.CategoryID=C.CategoryID
                                    where QI.QuestionnaireID= { questionnireId }
                                    union
                                    select Distinct null as QuestionnaireItemID, null as CategoryID, 3 as ActionTypeID, I.ItemID, I.Name,I.ListOrder,I.ParentItemID, I2.Name as ParentName  from item I
									join QuestionnaireItem QI on QI.itemId=I.parentItemID
									join item I2 on I2.itemId=I.parentItemID
									where QI.QuestionnaireID= { questionnireId } and I.parentItemID is not null
                                    ) as totalList order by ListOrder";

                questionnaireItemDTO = ExecuteSqlQuery(query, x => new SkipLogicItemsDTO
                {
                    QuestionnaireItemID = x["QuestionnaireItemID"] == DBNull.Value ? null : (int?)x["QuestionnaireItemID"],
                    CategoryID = x["CategoryID"] == DBNull.Value ? null : (int?)x["CategoryID"],
                    ItemID = x["ItemID"] == DBNull.Value ? null : (int?)x["ItemID"],
                    Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                    ActionTypeID = x["ActionTypeID"] == DBNull.Value ? 0 : (int)x["ActionTypeID"],
                    ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],
                    ParentItemID = x["ParentItemID"] == DBNull.Value ? null : (int?)x["ParentItemID"],
                    ParentName = x["ParentName"] == DBNull.Value ? null : (string)x["ParentName"],
                }).ToList();
                return questionnaireItemDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllQuestionnaire.
        /// </summary>
        /// <returns>QuestionnaireDTO.</returns>
        public List<QuestionnaireDTO> GetAllQuestionnaire()
        {
            try
            {
                var readFromCache = this._cache.Get<List<QuestionnaireDTO>>(PCISEnum.Caching.GetAllQuestionnaire);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    string query = @"Select Q.QuestionnaireID,  str(Q.QuestionnaireID) + '-' + I.Abbrev + '-' + Q.Abbrev as QuestionnaireName from Questionnaire Q
                                    left join info.Instrument I on  I.InstrumentID = Q.InstrumentID 
                                    where Q.IsRemoved = 0 and Q.IsBaseQuestionnaire=0";

                    var data = ExecuteSqlQuery(query, x => new QuestionnaireDTO
                    {
                        QuestionnaireID = x[0] == DBNull.Value ? 0 : (int)x[0],
                        QuestionnaireName = x[1] == DBNull.Value ? null : ((string)x[1]).Trim()
                    }).OrderBy(y => y.QuestionnaireID).ToList();

                    this._cache.Post(PCISEnum.Caching.GetAllQuestionnaire, readFromCache = data);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllActionType.
        /// </summary>
        /// <returns>ActionTypeDTO.</returns>
        public List<ActionTypeDTO> GetAllActionType()
        {
            try
            {
                var readFromCache = this._cache.Get<List<ActionTypeDTO>>(PCISEnum.Caching.GetAllActionType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    string query = @"Select ActionTypeID,Name,ListOrder from info.ActionType";

                    var data = ExecuteSqlQuery(query, x => new ActionTypeDTO
                    {
                        ActionTypeID = x["ActionTypeID"] == DBNull.Value ? 0 : (int)x["ActionTypeID"],
                        Name = x["Name"] == DBNull.Value ? null : (string)x["Name"],
                        ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"],

                    }).OrderBy(y => y.ListOrder).ToList();

                    this._cache.Post(PCISEnum.Caching.GetAllActionType, readFromCache = data);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllCategories.
        /// </summary>
        /// <returns>CollaborationTagTypeDTO List.</returns>
        public List<CollaborationTagTypeDTO> GetAllCategories()
        {
            try
            {
                var readFromCache = this._cache.Get<List<CollaborationTagTypeDTO>>(PCISEnum.Caching.GetAllCategories);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var response = _dbContext.CollaborationTagType.Where(x => !x.IsRemoved).
                                 Select(x => new CollaborationTagTypeDTO
                                 {
                                     CollaborationTagTypeID = x.CollaborationTagTypeID,
                                     Name = x.Name,
                                     Abbrev = x.Abbrev,
                                     ListOrder = x.ListOrder,
                                     AgencyID = x.AgencyID,
                                 }).OrderBy(y => y.ListOrder).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllCategories, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllLeads.
        /// </summary>
        /// <returns>HelperLookupDTO List.</returns>
        public List<HelperLookupDTO> GetAllLeads()
        {
            var readFromCache = this._cache.Get<List<HelperLookupDTO>>(PCISEnum.Caching.GetAllLeads);
            if (readFromCache == null || readFromCache?.Count == 0)
            {
                string query = @"Select HelperID, HelperIndex,  FirstName+ ' '+isnull(MiddleName+' ', '')+ LastName as HelperName from Helper
                                where IsRemoved = 0" +
                                " AND ISNULL(EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)";

                var data = ExecuteSqlQuery(query, x => new HelperLookupDTO
                {
                    HelperID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    HelperIndex = x[1] == DBNull.Value ? null : (Guid?)x[1],
                    HelperName = x[2] == DBNull.Value ? null : (string)x[2]
                }).OrderBy(y => y.HelperName).ToList();

                this._cache.Post(PCISEnum.Caching.GetAllLeads, readFromCache = data);
            }
            return readFromCache;
        }

        /// <summary>
        /// GetAllLevels.
        /// </summary>
        /// <returns>CollaborationLevelDTO List.</returns>
        public List<CollaborationLevelDTO> GetAllLevels()
        {
            try
            {
                var readFromCache = this._cache.Get<List<CollaborationLevelDTO>>(PCISEnum.Caching.GetAllLevels);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var response = _dbContext.CollaborationLevel.Where(x => !x.IsRemoved).
                                Select(x => new CollaborationLevelDTO
                                {
                                    CollaborationLevelID = x.CollaborationLevelID,
                                    Name = x.Name,
                                    Abbrev = x.Abbrev,
                                    ListOrder = x.ListOrder,
                                    AgencyID = x.AgencyID,
                                }).OrderBy(y => y.ListOrder).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllLevels, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllTherapyTypes.
        /// </summary>
        /// <returns>TherapyTypeDTO List.</returns>
        public List<TherapyTypeDTO> GetAllTherapyTypes()
        {
            try
            {
                var readFromCache = this._cache.Get<List<TherapyTypeDTO>>(PCISEnum.Caching.GetAllTherapyTypes);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var response = _dbContext.TherapyType.Where(x => !x.IsRemoved).
                                Select(x => new TherapyTypeDTO
                                {
                                    TherapyTypeID = x.TherapyTypeID,
                                    Name = x.Name,
                                    Abbrev = x.Abbrev,
                                    ListOrder = x.ListOrder,
                                    AgencyID = x.AgencyID
                                }).OrderBy(y => y.ListOrder).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllTherapyTypes, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllResponse.
        /// </summary>
        /// <returns>ItemResponseLookupDTO.</returns>
        public List<ResponseDTO> GetAllResponse()
        {
            try
            {

                var readFromCache = this._cache.Get<List<ResponseDTO>>(PCISEnum.Caching.GetAllResponse);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var response = _dbContext.Responses.Where(x => !x.IsRemoved).
                               Select(x => new ResponseDTO
                               {
                                   ResponseID = x.ResponseID,
                                   ItemID = x.ItemID,
                                   BackgroundColorPaletteID = x.BackgroundColorPaletteID,
                                   Label = x.Label,
                                   IsItemResponseBehaviorDisabled = x.IsItemResponseBehaviorDisabled,
                                   DefaultItemResponseBehaviorID = x.DefaultItemResponseBehaviorID,
                                   MaxRangeValue = x.MaxRangeValue,
                                   Value = x.Value,
                                   Description = x.Description,
                                   KeyCodes = x.KeyCodes,
                                   ListOrder = x.ListOrder
                               }).OrderBy(y => y.ListOrder).ToList();
                    this._cache.Post(PCISEnum.Caching.GetAllResponse, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetCollaborationLookupForOrgAdmin.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationDataDTO List.</returns>
        public List<CollaborationDataDTO> GetCollaborationLookupForOrgAdmin(long agencyID, bool activeCollaborations = true)
        {
            try
            {
                var activeColbCondition = activeCollaborations ? "AND ISNULL(C.EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)" : "";
                List <CollaborationDataDTO> collaborationDataDTO = new List<CollaborationDataDTO>();
                var query = string.Empty;
                query = @$"Select C.CollaborationID, C.CollaborationIndex,C.[Name], C.Code , C.StartDate, C.EndDate from Collaboration C
                            left join Agency A on A.AgencyID = C.AgencyID
                            left join info.CollaborationLevel CL on CL.CollaborationLevelID = C.CollaborationLevelID
                            left join info.TherapyType T on T.TherapyTypeID = C.TherapyTypeID where C.IsRemoved = 0  and C.AgencyID = { agencyID }  {activeColbCondition} ORDER BY C.[Name]";

                var data = ExecuteSqlQuery(query, x => new CollaborationDataDTO
                {
                    CollaborationID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    CollaborationIndex = x[1] == DBNull.Value ? Guid.Empty : (Guid)x[1],
                    Name = x[2] == DBNull.Value ? null : (string)x[2],
                    Code = x[3] == DBNull.Value ? null : (string)x[3],
                    StartDate = x[4] == DBNull.Value ? DateTime.Now : (DateTime)x[4],
                    EndDate = x[5] == DBNull.Value ? null : (DateTime?)x[5],
                });
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllAgencyLeads.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>HelperLookupDTO List.</returns>
        public List<HelperLookupDTO> GetAllAgencyLeads(long agencyID, bool activeHelpers = true)
        {
            var activeHelperCondition = activeHelpers ? "AND ISNULL(EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)" : "";
            string query = @$"Select HelperID, HelperIndex, FirstName+ ' '+isnull(MiddleName+' ', '')+ LastName as HelperName,StartDate,EndDate from Helper where AgencyID = { agencyID } and IsRemoved = 0 {activeHelperCondition}";

            var data = ExecuteSqlQuery(query, x => new HelperLookupDTO
            {
                HelperID = x["HelperID"] == DBNull.Value ? 0 : (int)x["HelperID"],
                HelperIndex = x["HelperIndex"] == DBNull.Value ? null : (Guid?)x["HelperIndex"],
                HelperName = x["HelperName"] == DBNull.Value ? null : (string)x["HelperName"],
                StartDate = x["StartDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["StartDate"],
                EndDate = x["EndDate"] == DBNull.Value ? null : (DateTime?)x["EndDate"]
            }).OrderBy(y => y.HelperName).ToList();

            return data;
        }

        /// <summary>
        /// GetCollaborationLevelList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of CollaborationLevelDTO.</returns>
        public List<CollaborationLevelDTO> GetCollaborationLevelLookup(long agencyID)
        {
            try
            {
                List<CollaborationLevelDTO> CollaborationLevelDTO = new List<CollaborationLevelDTO>();
                var readFromCache = this._cache.Get<List<CollaborationLevelDTO>>(PCISEnum.Caching.GetCollaborationLevelLookup);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    //var CollaborationLevel = this._dbContext.CollaborationLevel.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                    var CollaborationLevel = this._dbContext.CollaborationLevel.Where(x => !x.IsRemoved).ToList();
                    this.mapper.Map<List<CollaborationLevel>, List<CollaborationLevelDTO>>(CollaborationLevel, CollaborationLevelDTO);
                    this._cache.Post(PCISEnum.Caching.GetCollaborationLevelLookup, readFromCache = CollaborationLevelDTO);
                }
                CollaborationLevelDTO = readFromCache?.Where(x => x.AgencyID == agencyID).OrderBy(y => y.ListOrder).ToList();
                return CollaborationLevelDTO;
            }
            catch (Exception)
            {
                throw;
            }
        } /// <summary>
          /// GetCollaborationLevelList.
          /// </summary>
          /// <param name="agencyID">agencyID.</param>
          /// <returns>List of CollaborationLevelDTO.</returns>
        public List<CollaborationLevelDTO> GetCollaborationLevels(long agencyID)
        {
            try
            {
                string query = string.Empty;
                query = @$"select CollaborationLevelID, Name,ListOrder  from info.CollaborationLevel
                                    where IsRemoved = 0 and AgencyID = { agencyID }  order by ListOrder";
                var data = ExecuteSqlQuery(query, x => new CollaborationLevelDTO
                {
                    CollaborationLevelID = x["CollaborationLevelID"] == DBNull.Value ? 0 : (int)x["CollaborationLevelID"],
                    Name = x["Name"] == DBNull.Value ? string.Empty : (string?)x["Name"],
                    ListOrder = x["ListOrder"] == DBNull.Value ? 0 : (int)x["ListOrder"]
                }).OrderBy(y => y.ListOrder).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Therapy Types list
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>TherapyTypeDTO</returns>
        public List<TherapyTypeDTO> GetAgencyTherapyTypeList(long agencyID)
        {
            try
            {
                List<TherapyTypeDTO> therapyTypeDTO = new List<TherapyTypeDTO>();
                var readFromCache = this._cache.Get<List<TherapyTypeDTO>>(PCISEnum.Caching.GetAgencyTherapyTypeList);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    //var therapyType = readFromCache.Where(x => x.AgencyID == agencyID).OrderBy(y => y.ListOrder).ToList();
                    var therapyType = this._dbContext.TherapyType.Where(x => !x.IsRemoved).ToList();
                    this.mapper.Map<List<TherapyType>, List<TherapyTypeDTO>>(therapyType, therapyTypeDTO);
                    this._cache.Post(PCISEnum.Caching.GetAgencyTherapyTypeList, readFromCache = therapyTypeDTO);
                }
                therapyTypeDTO = readFromCache?.Where(x => x.AgencyID == agencyID).OrderBy(y => y.ListOrder).ToList();
                return therapyTypeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Get the Therapy Types list
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>TherapyTypeDTO</returns>
        public List<TherapyTypeDTO> GetAgencyTherapyTypes(long agencyID)
        {
            try
            {
                var query = string.Empty;
                query = @$"Select AgencyID, TherapyTypeID,ListOrder from info.TherapyType where IsRemoved = 0 and AgencyID ={agencyID}";

                var data = ExecuteSqlQuery(query, x => new TherapyTypeDTO
                {
                    AgencyID = (long)x[0],
                    TherapyTypeID = x["TherapyTypeID"] == DBNull.Value ? 0 : (int)x["TherapyTypeID"]
                }).OrderBy(y => y.ListOrder).ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllHelperLookup.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>RoleLookupResponseDTO.</returns>
        public List<HelperLookupDTO> GetAllAgencyHelperLookup(long agencyID, bool activeHelpers = true)
        {
            try
            {
                var activeHelperCondition = activeHelpers ? "AND ISNULL(EndDate, CAST(GETDATE() AS DATE))>=CAST(GETDATE() AS DATE)" : "";
                List <HelperLookupDTO> response = new List<HelperLookupDTO>();
                string query = string.Empty;
                query = @$"select HelperID, HelperIndex, FirstName+ ' '+isnull(MiddleName+' ', '')+ LastName as HelperName from helper
                                    where IsRemoved = 0 and AgencyID ={ agencyID }  {activeHelperCondition} order by HelperName";
                var data = ExecuteSqlQuery(query, x => new HelperLookupDTO
                {
                    HelperID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    HelperIndex = x[1] == DBNull.Value ? null : (Guid?)x[1],
                    HelperName = x[2] == DBNull.Value ? null : (string)x[2],
                }).OrderBy(y => y.HelperName).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAgencyNotificationLevelList.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>List of NotificationLevelDTO.</returns>
        public List<NotificationLevelDTO> GetAgencyNotificationLevelList(long agencyID)
        {
            try
            {
                List<NotificationLevelDTO> NotificationLevelDTO = new List<NotificationLevelDTO>();
                var readFromCache = this._cache.Get<List<NotificationLevelDTO>>(PCISEnum.Caching.GetAgencyNotificationLevelList);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    //var NotificationLevel = this._dbContext.NotificationLevels.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                    var NotificationLevel = this._dbContext.NotificationLevels.Where(x => !x.IsRemoved).ToList();
                    this.mapper.Map<List<NotificationLevel>, List<NotificationLevelDTO>>(NotificationLevel, NotificationLevelDTO);
                    this._cache.Post(PCISEnum.Caching.GetAgencyNotificationLevelList, readFromCache = NotificationLevelDTO);
                }
                NotificationLevelDTO = readFromCache?.Where(x => x.AgencyID == agencyID).OrderBy(y => y.ListOrder).ToList();
                return NotificationLevelDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllAgencyQuestionnaire.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>QuestionnaireDTO.</returns>
        public List<QuestionnaireDTO> GetAllAgencyQuestionnaire(long agencyID)
        {
            try
            {
                string query = @"Select Q.QuestionnaireID, Q.Name AS QuestionnaireName from Questionnaire Q
                                    left join info.Instrument I on  I.InstrumentID = Q.InstrumentID 
                                    where Q.AgencyID = " + agencyID + " and Q.IsRemoved = 0 and Q.IsBaseQuestionnaire=0";

                var data = ExecuteSqlQuery(query, x => new QuestionnaireDTO
                {
                    QuestionnaireID = x[0] == DBNull.Value ? 0 : (int)x[0],
                    QuestionnaireName = x[1] == DBNull.Value ? null : ((string)x[1]).Trim()
                }).OrderBy(y => y.QuestionnaireID).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Collaboration Tag Types list with agency.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationTagTypeDTO</returns>
        public List<CollaborationTagTypeDTO> GetAgencyTagTypeList(long agencyID)
        {
            try
            {
                List<CollaborationTagTypeDTO> collaborationTagTypeDTO = new List<CollaborationTagTypeDTO>();
                var readFromCache = this._cache.Get<List<CollaborationTagTypeDTO>>(PCISEnum.Caching.GetAgencyTagTypeList);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    //var collaborationTagType = this._dbContext.CollaborationTagType.Where(x => x.AgencyID == agencyID && !x.IsRemoved).OrderBy(y => y.ListOrder).ToList();
                    var collaborationTagType = this._dbContext.CollaborationTagType.Where(x => !x.IsRemoved).ToList();
                    this.mapper.Map<List<CollaborationTagType>, List<CollaborationTagTypeDTO>>(collaborationTagType, collaborationTagTypeDTO);
                    this._cache.Post(PCISEnum.Caching.GetAgencyTagTypeList, readFromCache = collaborationTagTypeDTO);
                }
                collaborationTagTypeDTO = readFromCache?.Where(x => x.AgencyID == agencyID).OrderBy(y => y.ListOrder).ToList();
                return collaborationTagTypeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Collaboration Tag Types list with agency.
        /// </summary>
        /// <param name="agencyID">agencyID.</param>
        /// <returns>CollaborationTagTypeDTO</returns>
        public List<CollaborationTagTypeDTO> GetAgencyTagTypes(long agencyID)
        {
            try
            {
                var query = string.Empty;
                query = @$"Select AgencyID, CollaborationTagTypeID, ListOrder from info.CollaborationTagType where IsRemoved = 0 and AgencyID = {agencyID}";

                var data = ExecuteSqlQuery(query, x => new CollaborationTagTypeDTO
                {
                    AgencyID = (long)x[0],
                    CollaborationTagTypeID = x["CollaborationTagTypeID"] == DBNull.Value ? 0 : (int)x["CollaborationTagTypeID"]
                }).OrderBy(y => y.ListOrder).ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// GetAllAssessments.
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <returns>AssessmentsDTO List.</returns>
        public List<AssessmentsDTO> GetAllAssessments(int personQuestionnaireID)
        {
            try
            {
                List<AssessmentsDTO> AssessmentsDTO = new List<AssessmentsDTO>();
                var query = string.Empty;

                query = $@";WITH CTE AS
                        (
                        SELECT
                        ROW_NUMBER() OVER(ORDER BY a.DateTaken ASC) [RNo],
                        a.AssessmentID,
                        a.DateTaken,
                        ast.Name [AssessmentStatus],
                        a.AssessmentStatusID,
                        asr.Name [AssessmentReason],
                        a.AssessmentReasonID
                        FROM
                        PersonQuestionnaire pq
                        JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID AND pq.PersonQuestionnaireID={personQuestionnaireID} AND a.IsRemoved=0
                        JOIN info.AssessmentStatus ast ON ast.AssessmentStatusID=a.AssessmentStatusID AND ast.Name IN ('Returned','Submitted', 'Approved')
                        JOIN info.AssessmentReason asr ON asr.AssessmentReasonID=a.AssessmentReasonID
                        )
                        SELECT
                        'Time ' + CAST(CTE.RNo AS varchar(10)) [Time],
                        CTE.*
                        FROM
                        CTE
                        ORDER BY CTE.DateTaken";

                AssessmentsDTO = ExecuteSqlQuery(query, x => new AssessmentsDTO
                {
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    Time = x["Time"] == DBNull.Value ? null : (string)x["Time"],
                    AssessmentStatusID = x["AssessmentStatusID"] == DBNull.Value ? 0 : (int)x["AssessmentStatusID"],
                    AssessmentStatus = x["AssessmentStatus"] == DBNull.Value ? null : (string)x["AssessmentStatus"],
                    AssessmentReasonID = x["AssessmentReasonID"] == DBNull.Value ? 0 : (int)x["AssessmentReasonID"],
                    AssessmentReason = x["AssessmentReason"] == DBNull.Value ? null : (string)x["AssessmentReason"],
                    DateTaken = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                });
                return AssessmentsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllAssessments.-Reports
        /// </summary>
        /// <param name="personQuestionnaireID">personQuestionnaireID.</param>
        /// <param name="personCollaborationID">personCollaborationID.</param>
        /// <param name="voiceTypeID">voiceTypeID.</param>
        /// <returns>AssessmentsDTO List.</returns>
        public List<AssessmentsDTO> GetAllAssessments(long personID, long personQuestionnaireID, long personCollaborationID, int voiceTypeID, long voiceTypeFKID, SharedDetailsDTO sharedIDs, SharedDetailsDTO helperColbIDs)
        {
            try
            {
                var sharedWhereConditionQIDs = string.IsNullOrEmpty(sharedIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({sharedIDs.SharedQuestionnaireIDs})";
                var sharedWhereConditionCIDs = string.IsNullOrEmpty(sharedIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({sharedIDs.SharedCollaborationIDs})";

                var helperAssessmentIDs = string.IsNullOrEmpty(helperColbIDs.SharedAssessmentIDs) ? "" : $@"AND AssessmentID IN ({helperColbIDs.SharedAssessmentIDs})";
                var helperWhereConditionQIDs = string.IsNullOrEmpty(helperColbIDs.SharedQuestionnaireIDs) ? "" : $@"AND PQ.QuestionnaireID IN ({helperColbIDs.SharedQuestionnaireIDs})";
                var helperWhereConditionCIDs = string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs) ? "" : $@"AND PC.CollaborationID IN ({helperColbIDs.SharedCollaborationIDs})";

                var helperpersonCollaborationID = string.IsNullOrEmpty(helperColbIDs.SharedCollaborationIDs) ? personCollaborationID : -1;

                List<AssessmentsDTO> AssessmentsDTO = new List<AssessmentsDTO>();
                var query = string.Empty;

                query = $@";WITH WindowOffsets AS
                        (
	                        SELECT
		                        q.QuestionnaireID,ar.Name [Reason],qw.WindowOpenOffsetDays,qw.WindowCloseOffsetDays
	                        FROM 
	                        PersonQuestionnaire pq
	                        JOIN Questionnaire q ON q.QuestionnaireID=pq.QuestionnaireID 
                            AND pq.PersonQuestionnaireID={personQuestionnaireID} AND pq.IsRemoved=0 
	                        JOIN QuestionnaireWindow qw ON qw.QuestionnaireID=q.QuestionnaireID 
	                        JOIN info.AssessmentReason ar ON ar.AssessmentReasonID=qw.AssessmentReasonID
                            WHERE 1=1 {helperWhereConditionQIDs}
                        )
                        ,SelectedCollaboration AS
                        (
	                        SELECT-- C.CollaborationID,C.Name AS CollaborationName,
							MIN(PC.EnrollDate) EnrollDate,PC.PersonID,
                                            MAX(PC.EndDate) EndDate--, PC.PersonCollaborationID,PC.IsPrimary, PC.IsCurrent
                                  	 FROM PersonCollaboration PC 
                                  	      JOIN Collaboration C ON C.CollaborationID = PC.CollaborationID
                                  	 WHERE PC.IsRemoved=0 AND PC.PersonID = {personID}
                                     AND (PC.PersonCollaborationID = {personCollaborationID} or {personCollaborationID} = 0) {sharedWhereConditionCIDs} {helperWhereConditionCIDs}
                                     GRoup by personid
                        )
                        ,CTE AS
                        (
                            SELECT
                                ROW_NUMBER() OVER(ORDER BY a.DateTaken ASC) [RNo],
		                        pq.PersonID,
                                a.AssessmentID,
                                a.DateTaken,
                                a.AssessmentStatusID,
                                a.AssessmentReasonID,
		                        a.VoiceTypeID,a.VoiceTypeFKID,
		                        wo_init.WindowOpenOffsetDays,
		                        wo_disc.WindowCloseOffsetDays,
		                        DATEDIFF(DAY,sc.EnrollDate,a.DateTaken) [DaysInEpisode],
		                        (CASE WHEN ({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT  MIN(CAST(EnrollDate AS DATE)) FROM SelectedCollaboration) END) [EnrollDate],
		                        (CASE WHEN ({personCollaborationID}=0AND {helperpersonCollaborationID} = 0) THEN NULL ELSE (SELECT MAX(CAST(ISNULL(EndDate,GETDATE()) AS DATE)) FROM SelectedCollaboration) END) [EndDate]
                            FROM
                            PersonQuestionnaire pq
                            JOIN Assessment a ON a.PersonQuestionnaireID=pq.PersonQuestionnaireID 
                            AND pq.QuestionnaireID = (SELECT QuestionnaireID FROM PersonQuestionnaire WHERE PersonQuestionnaireID={personQuestionnaireID})
                            AND a.IsRemoved=0 AND pq.IsRemoved=0 {sharedWhereConditionQIDs}
							JOIN SelectedCollaboration sc ON sc.PersonID=pq.PersonID
	                        LEFT JOIN (SELECT * FROM WindowOffsets WHERE Reason='Initial')wo_init ON wo_init.QuestionnaireID=pq.QuestionnaireID 
	                        LEFT JOIN (SELECT * FROM WindowOffsets WHERE Reason='Discharge')wo_disc ON wo_disc.QuestionnaireID=pq.QuestionnaireID 
                            where 1=1  {helperAssessmentIDs}
                        )
                        SELECT
                            'Time ' + CAST(CTE.RNo AS varchar(10)) [Time],
                            CTE.AssessmentID,
                            CTE.DateTaken,
                            CTE.AssessmentStatusID,
                            CTE.AssessmentReasonID,
	                        ast.Name [AssessmentStatus],
	                        asr.Name [AssessmentReason],
	                        DaysInEpisode
                        FROM
                        CTE
                        JOIN info.AssessmentStatus ast ON ast.AssessmentStatusID=CTE.AssessmentStatusID AND ast.Name IN ('Returned','Submitted', 'Approved')  
                            AND (({voiceTypeID}=0 OR CTE.VoiceTypeID={voiceTypeID}) AND ({voiceTypeFKID}=0 OR ISNULL(CTE.VoiceTypeFKID,0)={voiceTypeFKID}) )
                        JOIN info.AssessmentReason asr ON asr.AssessmentReasonID=CTE.AssessmentReasonID
                        WHERE 
                        (
	                        ({personCollaborationID}=0 AND {helperpersonCollaborationID} = 0) OR
	                        CAST(CTE.DateTaken AS DATE) 
	                        BETWEEN
	                        DATEADD(DAY,0-ISNULL(CTE.WindowOpenOffsetDays,0),CAST(CTE.EnrollDate AS DATE)) 
	                        AND 
	                        DATEADD(DAY,ISNULL(CTE.WindowCloseOffsetDays,0),ISNULL(CTE.EndDate, CAST(GETDATE() AS DATE)))
                        ) 
                        ORDER BY CTE.DateTaken";

                AssessmentsDTO = ExecuteSqlQuery(query, x => new AssessmentsDTO
                {
                    AssessmentID = x["AssessmentID"] == DBNull.Value ? 0 : (int)x["AssessmentID"],
                    Time = x["Time"] == DBNull.Value ? null : (string)x["Time"],
                    AssessmentStatusID = x["AssessmentStatusID"] == DBNull.Value ? 0 : (int)x["AssessmentStatusID"],
                    AssessmentStatus = x["AssessmentStatus"] == DBNull.Value ? null : (string)x["AssessmentStatus"],
                    AssessmentReasonID = x["AssessmentReasonID"] == DBNull.Value ? 0 : (int)x["AssessmentReasonID"],
                    AssessmentReason = x["AssessmentReason"] == DBNull.Value ? null : (string)x["AssessmentReason"],
                    DaysInEpisode = x["DaysInEpisode"] == DBNull.Value ? 0 : (int)x["DaysInEpisode"],
                    DateTaken = x["DateTaken"] == DBNull.Value ? DateTime.MinValue : (DateTime)x["DateTaken"],
                });
                return AssessmentsDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetTimeFrameDetails
        /// </summary>
        /// <param name="daysInService">daysInService</param>
        /// <returns></returns>
        public TimeFrame GetTimeFrameDetails(int daysInService)
        {
            try
            {
                TimeFrame timeFrame = new TimeFrame();
                var lst_timeFrame = this._dbContext.TimeFrames.Where(x => x.DaysInService == daysInService).ToList();
                if (lst_timeFrame.Count > 0)
                {
                    timeFrame = lst_timeFrame[0];
                }

                return timeFrame;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetItemResponseBehavior.
        /// </summary>
        /// <returns>ItemResponseBehaviorResponseDTO.</returns>
        public ItemResponseBehaviorDTO GetItemResponseBehaviorById(int id)
        {
            try
            {
                var response = _dbContext.ItemResponseBehaviors.Where(x => x.ItemResponseBehaviorID == id).
                   Select(x => new ItemResponseBehaviorDTO
                   {
                       ItemResponseBehaviorID = x.ItemResponseBehaviorID,
                       ItemResponseTypeID = x.ItemResponseTypeID,
                       Name = x.Name,
                       ListOrder = x.ListOrder,
                       Abbrev = x.Abbrev,
                       Description = x.Description
                   }).OrderBy(y => y.ListOrder).ToList().FirstOrDefault();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetItemResponseBehavior.
        /// </summary>
        /// <returns>ItemResponseBehaviorResponseDTO.</returns>
        public ItemResponseType GetItemResponseTypeById(int id)
        {
            try
            {
                var response = _dbContext.ItemResponseTypes.Where(x => x.ItemResponseTypeID == id).
                   Select(x => new ItemResponseType
                   {
                       ItemResponseTypeID = x.ItemResponseTypeID,
                       Name = x.Name,
                       ListOrder = x.ListOrder,
                       Abbrev = x.Abbrev,
                       Description = x.Description
                   }).OrderBy(y => y.ListOrder).ToList().FirstOrDefault();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Helper> GetAllHelperByAgencyID(long agencyID)
        {
            try
            {
                return this._dbContext.Helper.Where(x => x.IsRemoved == false && x.AgencyID == agencyID).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TimeZonesDTO> GetAllTimeZones()
        {
            try
            {
                var readFromCache = this._cache.Get<List<TimeZonesDTO>>(PCISEnum.Caching.GetAllTimeZones);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var timeZones = _dbContext.TimeZones.Where(x => !x.IsRemoved).OrderBy(s => s.ListOrder).ToList();
                    var response = this.mapper.Map<List<TimeZonesDTO>>(timeZones);
                    this._cache.Post(PCISEnum.Caching.GetAllTimeZones, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<OffsetTypeDTO> GetAllOffsetType()
        {
            try
            {
                var readFromCache = this._cache.Get<List<OffsetTypeDTO>>(PCISEnum.Caching.GetAllOffsetType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var offsetTypes = _dbContext.OffsetTypes.Where(x => !x.IsRemoved).OrderBy(s => s.ListOrder).ToList();
                    var response = this.mapper.Map<List<OffsetTypeDTO>>(offsetTypes);
                    this._cache.Post(PCISEnum.Caching.GetAllOffsetType, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RecurrenceEndTypeDTO> GetAllRecurrenceEndType()
        {
            try
            {
                var readFromCache = this._cache.Get<List<RecurrenceEndTypeDTO>>(PCISEnum.Caching.GetAllRecurrenceEndType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var recurrenceEndTypes = _dbContext.RecurrenceEndTypes.Where(x => !x.IsRemoved).OrderBy(s => s.ListOrder).ToList();
                    var response = this.mapper.Map<List<RecurrenceEndTypeDTO>>(recurrenceEndTypes);
                    this._cache.Post(PCISEnum.Caching.GetAllRecurrenceEndType, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RecurrencePatternDTO> GetAllRecurrencePattern()
        {
            try
            {
                var readFromCache = this._cache.Get<List<RecurrencePatternDTO>>(PCISEnum.Caching.GetAllRecurrencePattern);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var recurrencePatterns = _dbContext.RecurrencePatterns.Where(x => !x.IsRemoved).OrderBy(s => s.ListOrder).ToList();
                    var response = this.mapper.Map<List<RecurrencePatternDTO>>(recurrencePatterns);
                    this._cache.Post(PCISEnum.Caching.GetAllRecurrencePattern, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<RecurrenceOrdinalDTO> GetAllRecurrenceOrdinal()
        {
            try
            {
                var readFromCache = this._cache.Get<List<RecurrenceOrdinalDTO>>(PCISEnum.Caching.GetAllRecurrenceOrdinal);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var recurrenceOrdinals = _dbContext.RecurrenceOrdinals.Where(x => !x.IsRemoved).OrderBy(s => s.ListOrder).ToList();
                    var response = this.mapper.Map<List<RecurrenceOrdinalDTO>>(recurrenceOrdinals);
                    this._cache.Post(PCISEnum.Caching.GetAllRecurrenceOrdinal, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<RecurrenceDayDTO> GetAllRecurrenceDay()
        {
            try
            {
                var readFromCache = this._cache.Get<List<RecurrenceDayDTO>>(PCISEnum.Caching.GetAllRecurrenceDay);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var recurrenceDays = _dbContext.RecurrenceDays.Where(x => !x.IsRemoved).OrderBy(s => s.ListOrder).ToList();
                    var response = this.mapper.Map<List<RecurrenceDayDTO>>(recurrenceDays);
                    this._cache.Post(PCISEnum.Caching.GetAllRecurrenceDay, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<RecurrenceMonthDTO> GetAllRecurrenceMonth()
        {
            try
            {
                var readFromCache = this._cache.Get<List<RecurrenceMonthDTO>>(PCISEnum.Caching.GetAllRecurrenceMonth);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var recurrenceMonth = _dbContext.RecurrenceMonths.Where(x => !x.IsRemoved).OrderBy(s => s.ListOrder).ToList();
                    var response = this.mapper.Map<List<RecurrenceMonthDTO>>(recurrenceMonth);
                    this._cache.Post(PCISEnum.Caching.GetAllRecurrenceMonth, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<InviteToCompleteReceiverDTO> GetAllInviteToCompleteReceivers()
        {
            try
            {
                var readFromCache = this._cache.Get<List<InviteToCompleteReceiverDTO>>(PCISEnum.Caching.GetAllInviteToComplete);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var recurrenceMonth = _dbContext.InviteToCompleteReceivers.Where(x => !x.IsRemoved).OrderBy(s => s.ListOrder).ToList();
                    var response = this.mapper.Map<List<InviteToCompleteReceiverDTO>>(recurrenceMonth);
                    this._cache.Post(PCISEnum.Caching.GetAllInviteToComplete, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetAllResponseValueTypes
        /// </summary>
        /// <returns></returns>
        public List<ResponseValueTypeDTO> GetAllResponseValueTypes()
        {
            try
            {
                var readFromCache = this._cache.Get<List<ResponseValueTypeDTO>>(PCISEnum.Caching.GetAllResponseValueType);
                if (readFromCache == null || readFromCache?.Count == 0)
                {
                    var responseValueTypes = _dbContext.ResponseValueTypes.Where(x => !x.IsRemoved).OrderBy(s => s.ListOrder).ToList();
                    var response = this.mapper.Map<List<ResponseValueTypeDTO>>(responseValueTypes);
                    this._cache.Post(PCISEnum.Caching.GetAllResponseValueType, readFromCache = response);
                }
                return readFromCache;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
