using RestApplication.Models;

namespace RestApplication.Interfaces.Service
{
    public interface IXmlService
    {
        /// <summary>
        /// Extract xml data from content
        /// </summary>
        /// <param name="input">email content</param>
        /// <returns></returns>
        Expense ExtractData(string input);

        /// <summary>
        /// Calculate Tax
        /// </summary>
        /// <param name="data"></param>
        void CalculateTotals(Expense data);
    }
}
