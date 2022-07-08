using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireWindowRepository
    {
        /// <summary>
        /// GetQuestionnaireWindow.
        /// </summary>
        /// <param name="questionnaireWindowID">questionnaireWindowID.</param>
        /// <returns>QuestionnaireWindowDTO</returns>
        Task<QuestionnaireWindow> GetQuestionnaireWindow(int questionnaireWindowID);

        /// <summary>
        /// UpdateQuestionnaireWindow.
        /// </summary>
        /// <param name="questionnaireWindow">QuestionnaireWindow.</param>
        /// <returns>QuestionnaireWindowDTO.</returns>
        QuestionnaireWindowDTO UpdateQuestionnaireWindow(QuestionnaireWindow questionnaireWindow);

        /// <summary>
        /// AddQuestionnaireWindow.
        /// </summary>
        /// <param name="QuestionnaireWindowDTO">QuestionnaireWindowDTO.</param>
        /// <returns>long</returns>
        int AddQuestionnaireWindow(QuestionnaireWindowDTO QuestionnaireWindowDTO);

        /// <summary>
        /// GetQuestionnaireWindowsByQuestionnaire
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireWindowsDTO</returns>
        List<QuestionnaireWindowsDTO> GetQuestionnaireWindowsByQuestionnaire(int questionnaireID);

        /// <summary>
        /// CloneQuestionnaireWindow
        /// </summary>
        /// <param name="QuestionnaireWindowDTO"></param>
        /// <returns>QuestionnaireWindowsDTO</returns>
        void CloneQuestionnaireWindow(List<QuestionnaireWindowsDTO> QuestionnaireWindowDTO);
        /// <summary>
        /// GetQuestionnaireWindows
        /// </summary>
        /// <param name="questionnaireID"></param>
        /// <returns>QuestionnaireWindowsDTO</returns>
        List<QuestionnaireWindow> GetAllQuestionnaireWindows(int questionnaireID);
    }
}
