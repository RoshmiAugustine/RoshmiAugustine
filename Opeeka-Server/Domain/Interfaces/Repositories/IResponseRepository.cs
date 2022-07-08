using Opeeka.PICS.Domain.DTO;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IResponseRepository
    {

        /// <summary>
        /// To get AssessmentResponse details.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>AssessmentResponse.</returns>
        Task<ResponseDTO> GetResponse(int id);

    }
}
