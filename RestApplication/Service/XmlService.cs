using NLog;
using RestApplication.Constant;
using RestApplication.Interfaces.Service;
using RestApplication.Models;
using System;
using System.Text.RegularExpressions;

namespace RestApplication.Service
{
    public class XmlService : IXmlService
    {
        #region Members    
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Methods
        /// <summary>
        /// Extract xml data from content
        /// </summary>
        /// <param name="input">email content</param>
        /// <returns></returns>
        public Expense ExtractData(string input)
        {
            var expenseData = new Expense();

            try
            {
                // Extract cost centre
                var costCentreMatch = Regex.Match(input, @"<cost_centre>(.*?)<\/cost_centre>");
                expenseData.CostCentre = costCentreMatch.Success ? costCentreMatch.Groups[1].Value : "UNKNOWN";

                // Extract total
                var totalMatch = Regex.Match(input, @"<total>(.*?)<\/total>");
                if (!totalMatch.Success)
                    return null; // Missing total, reject message
                expenseData.Total = decimal.Parse(totalMatch.Groups[1].Value);

                // Extract payment method
                var paymentMethodMatch = Regex.Match(input, @"<payment_method>(.*?)<\/payment_method>");
                expenseData.PaymentMethod = paymentMethodMatch.Success ? paymentMethodMatch.Groups[1].Value : null;

                // Extract vendor
                var vendorMatch = Regex.Match(input, @"<vendor>(.*?)<\/vendor>");
                expenseData.Vendor = vendorMatch.Success ? vendorMatch.Groups[1].Value : null;

                // Extract description
                var descriptionMatch = Regex.Match(input, @"<description>(.*?)<\/description>");
                expenseData.Description = descriptionMatch.Success ? descriptionMatch.Groups[1].Value : null;

                // Extract date
                var dateMatch = Regex.Match(input, @"<date>(.*?)<\/date>");
                if (dateMatch.Success)
                    expenseData.Date = DateTime.Parse(dateMatch.Groups[1].Value);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return expenseData;
        }

        /// <summary>
        /// Calculate Tax
        /// </summary>
        /// <param name="data"></param>
        public void CalculateTotals(Expense data)
        {
            try
            {
                data.SalesTax = data.Total * AppConstant.TAXRATE / (1 + AppConstant.TAXRATE);
                data.TotalExcludingTax = data.Total - data.SalesTax;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        #endregion
    }
}