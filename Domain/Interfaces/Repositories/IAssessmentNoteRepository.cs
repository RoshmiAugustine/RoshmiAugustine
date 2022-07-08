using Opeeka.PICS.Domain.Entities;

namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IAssessmentNoteRepository
    {
        /// <summary>
        /// AddAssessmentNote.
        /// </summary>
        /// <param name="assessmentNote">assessmentNote</param>
        /// <returns>AssessmentNote.</returns>
        AssessmentNote AddAssessmentNote(AssessmentNote assessmentNote);
    }
}
