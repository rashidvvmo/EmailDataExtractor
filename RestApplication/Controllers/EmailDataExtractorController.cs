//using Microsoft.Practices.Unity;
using NLog;
using RestApplication.Interfaces.Service;
using System;
using System.Web.Http;
using System.Xml;
using Unity;

namespace RestApplication.Controllers
{
    public class EmailDataExtractorController : ApiController
    {
        #region Members        

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IXmlService _xmlService;

        #endregion

        #region Constructor    

        public EmailDataExtractorController(IXmlService xmlService)
        {
            _xmlService = xmlService;
        }

        #endregion

        #region API

        [HttpPost]
        [Route("api/processdata")]
        public IHttpActionResult ProcessData(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return BadRequest("Input text cannot be empty.");
            }

            try
            {
                var expenseData = _xmlService.ExtractData(inputText);
                if (expenseData == null)
                    return BadRequest("Invalid data: Missing <total> or mismatched tags.");

                _xmlService.CalculateTotals(expenseData);
                return Ok(expenseData);
            }
            catch (XmlException ex)
            {
                Logger.Error(ex.Message);
                return BadRequest("Invalid XML structure: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Logger.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
