namespace Opeeka.PICS.Domain.Interfaces.Repositories
{
    public interface IOptionsRepository
    {
        /// <summary>
        /// IsValidListOrder.
        /// </summary>
        /// <param name="listOrder">listOrder.</param>
        /// <param name="tableName">tableName.</param>
        /// <param name="agencyID">agencyID.</param>
        /// <param name="id">id.</param>
        /// <returns>bool.</returns>
        bool IsValidListOrder(int listOrder, string tableName, long agencyID, long id = 0);
    }
}
