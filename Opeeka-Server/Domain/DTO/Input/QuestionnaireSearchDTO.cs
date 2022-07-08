namespace Opeeka.PICS.Domain.DTO.Input
{
    public class QuestionnaireSearchDTO : BaseSearchDTO
    {
        public int pageNumber { get; set; }
        public int pageLimit { get; set; }
        public int questionId { get; set; }
        public long AgencyId { get; set; }
    }
}
