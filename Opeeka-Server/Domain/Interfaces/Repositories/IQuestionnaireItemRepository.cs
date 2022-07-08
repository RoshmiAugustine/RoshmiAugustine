using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.DTO.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireItemRepository
    {
        /// <summary>
        /// GetQuestionnaireDetails.
        /// </summary>
        /// <param name="questionnaireIndex"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<QuestionnaireDetailsDTO> GetQuestionnaireDetails(QuestionnaireDetailsSearchDTO detailsSearchDTO, DynamicQueryBuilderDTO queryBuilderDTO);
        int GetQuestionnaireDetailsCount(int id);

        /// <summary>
        /// GetQuestionnaireItems
        /// </summary>
        /// <param name="questionnaireItemID"></param>
        /// <returns>QuestionnaireItemsDTO</returns>
        Task<QuestionnaireItemsDTO> GetQuestionnaireItems(int questionnaireItemID);

        /// <summary>
        /// UpdateQuestionnaireItem
        /// </summary>
        /// <param name="questionnaireItemDTO"></param>
        /// <returns>QuestionnaireItemsDTO</returns>
        QuestionnaireItemsDTO UpdateQuestionnaireItem(QuestionnaireItemsDTO questionnaireItemDTO);

        /// <summary>
        /// GetQuestionnaireItemsByQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireItemsDTO</returns>
        List<QuestionnaireItemsDTO> GetQuestionnaireItemsByQuestionnaire(int questionnaireID);

        /// <summary>
        /// CloneQuestionnaireItem
        /// </summary>
        /// <param name="questionnaireDTO"></param>
        /// <returns>QuestionnaireItemsDTO</returns>
        void CloneQuestionnaireItem(List<QuestionnaireItemsDTO> questionnaireItemDTO);
    }
}
