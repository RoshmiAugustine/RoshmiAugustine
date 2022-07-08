namespace Opeeka.PICS.Domain.DTO.Input
{
    public class QuestionnaireDetailsSearchDTO : BaseSearchDTO
    {
        public int pageNumber { get; set; }
        public int pageLimit { get; set; }
        public int questionId { get; set; }
    }
}
