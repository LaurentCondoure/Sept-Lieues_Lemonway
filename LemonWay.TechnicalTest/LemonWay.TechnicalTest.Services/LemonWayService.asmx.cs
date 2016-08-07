using LemonWay.TechnicalTest.Utilities.Data;
using LemonWay.TechnicalTest.Utilities.Exception;
using log4net;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace LemonWay.TechnicalTest.Services
{
    /// <summary>
    /// Description résumée de LemonWayService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class LemonWayService : System.Web.Services.WebService
    {
        #region Logger
        private static readonly ILog log = LogManager.GetLogger(typeof(LemonWayService));
        #endregion


        #region WebMethod

        /// <summary>
        /// WebMethods used to calcultate the corresponding the Fibonacci sequence number for the number passed as parameter
        /// </summary>
        /// <param name="baseNumber">Number for which the calcul have to be done</param>
        /// <returns>the Fibonacci sequence Number</returns>
        [WebMethod]
        public int Fibonacci(int baseNumber)
        {

            try
            {
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_IncomingCall").ToString(), "WebMethod Fibonacci", baseNumber);
                //CultureInfo used to determine the language of the messages
                CultureInfo currentCulture = GetCurrentCultureInfo() ?? new CultureInfo("en-US");
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_UserCulture").ToString(), currentCulture.Name);
                //Calcultate the corresponding Fibonacci sequence number and return the result
                int iFibonacciNumber = FibonacciNumber(baseNumber, currentCulture);
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod Fibonacci", iFibonacciNumber);
                return iFibonacciNumber;


            }
            //In case of any exception, return -1
            catch (ServiceException)
            {
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod Fibonacci", -1);
                return -1;
            }
            catch (Exception exc)
            {
                //When logging unexpected error, three entry log are made to preserve the log lisibility
                log.ErrorFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_Exception").ToString(), "WebMethod Fibonacci");
                log.Error(exc.Message);
                log.Error(exc.StackTrace);

                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod Fibonacci", -1);
                return -1;
            }
        }

        /// <summary>
        /// WebMethods used to calcultate the corresponding the Fibonacci sequence number for the number passed as parameter
        /// </summary>
        /// <param name="baseNumber">Number for which the calcul have to be done</param>
        /// <returns ref="FibonacciResult"> Structure which full qualify the result with error statment</returns>
        [WebMethod]
        public FibonacciResult FibonacciWithError(int baseNumber)
        {
            FibonacciResult fibonacciResult = new FibonacciResult();
            try
            {
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_IncomingCall").ToString(), "WebMethod FibonacciWithError", baseNumber);
                //CultureInfo used to determine the language of the messages
                CultureInfo currentCulture = GetCurrentCultureInfo() ?? new CultureInfo("en-US");
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_UserCulture").ToString(), currentCulture.Name);
                //Calcultate the corresponding Fibonacci sequence number
                int iFibonacciNumber = FibonacciNumber(baseNumber, currentCulture);
                //Build the response and return it
                fibonacciResult.Result = iFibonacciNumber;
                fibonacciResult.ErrorCode = 0;
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod FibonacciWithError", iFibonacciNumber);
                return fibonacciResult;
            }
            //Construct the error response
            catch (ServiceException serviceException)
            {
                fibonacciResult.Result = -1;
                fibonacciResult.ErrorCode = (int)serviceException.ExceptionLevel;
                fibonacciResult.ErrorMessage = serviceException.Message;
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod FibonacciWithError", -1);
                return fibonacciResult;
            }
            catch (Exception exc)
            {
                //When logging unexpected error, three entry log are made to preserve the log lisibility
                log.ErrorFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_Exception").ToString(), "WebMethod FibonacciWithError");
                log.Error(exc.Message);
                log.Error(exc.StackTrace);

                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod FibonacciWithError", -1);

                fibonacciResult.Result = -1;
                fibonacciResult.ErrorCode = (int)ServiceException.Level.Error;
                fibonacciResult.ErrorMessage = exc.Message;
                return fibonacciResult;
            }
        }

        /// <summary>
        /// Convert the xml string parameter into a json string
        /// </summary>
        /// <param name="xml">Xml string to convert</param>
        /// <returns>Json formatted corresponding to the xml string parameter</returns>
        [WebMethod]
        public string XmlToJson(string xml)
        {
            try
            {
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_IncomingCall").ToString(), "WebMethod XmlToJson", xml);
                //CultureInfo used to determine the language of the messages
                CultureInfo currentCulture = GetCurrentCultureInfo() ?? new CultureInfo("en-US");
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_UserCulture").ToString(), currentCulture.Name);
                //Convert the xml string into a json formatted string and return the result
                string sJsonresult =  XmlToJson(xml, currentCulture);
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod XmlToJson", sJsonresult);
                return sJsonresult;
            }
            //In case of any error, return the error message
            catch (ServiceException serviceException)
            {
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod XmlToJson", serviceException.Message);
                return serviceException.Message;
            }
            catch (Exception exc)
            {
                //When logging unexpected error, three entry log are made to preserve the log lisibility
                log.ErrorFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_Exception").ToString(), "WebMethod XmlToJson");
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod XmlToJson", exc.Message);
                return exc.ToString();
            }
        }

        /// <summary>
        /// Convert the xml string parameter into a json string
        /// </summary>
        /// <param name="xml">Xml string to convert</param>
        /// <returns>Structure which full qualify the result with error statment</returns>
        [WebMethod]
        public XmlToJsonResut XmlToJsonWithError(string xml)
        {
            XmlToJsonResut xmlToJsonResult = new XmlToJsonResut();
            try
            {
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_IncomingCall").ToString(), "WebMethod XmlToJsonWithError", xml);
                //CultureInfo used to determine the language of the messages
                CultureInfo currentCulture = GetCurrentCultureInfo() ?? new CultureInfo("en-US");
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_UserCulture").ToString(), currentCulture.Name);
                //Convert the xml string into a json formatted string 
                string json = XmlToJson(xml, currentCulture);
                //Build the response and return it
                xmlToJsonResult.Result = json;
                xmlToJsonResult.ErrorCode = 0;
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod XmlToJsonWithError", json);
                return xmlToJsonResult;
            }
            //Construct the error response
            //In case of any error, return the error message
            catch (ServiceException serviceException)
            {
                xmlToJsonResult.Result = string.Empty;
                xmlToJsonResult.ErrorCode = (int)serviceException.ExceptionLevel;
                xmlToJsonResult.ErrorMessage = serviceException.Message;
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod XmlToJsonWithError", string.Empty);
                return xmlToJsonResult;
            }
            catch (Exception exc)
            {
                //When logging unexpected error, three entry log are made to preserve the log lisibility
                log.ErrorFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_Exception").ToString(), "WebMethod XmlToJsonWithError");
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod XmlToJsonWithError", string.Empty);

                xmlToJsonResult.Result = string.Empty;
                xmlToJsonResult.ErrorCode = (int)ServiceException.Level.Error;
                xmlToJsonResult.ErrorMessage = exc.Message;
                return xmlToJsonResult;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// retriver the consumer language from the HttpContext in order to localized the message
        /// </summary>
        /// <returns>
        /// The current Culture
        /// </returns>
        private CultureInfo GetCurrentCultureInfo()
        {
            try
            {
                string[] userLanguages = HttpContext.Current.Request.UserLanguages;
                if (userLanguages != null && userLanguages.Length > 0)
                {
                    string userLanguage = userLanguages[0].ToLowerInvariant().Trim();
                    return CultureInfo.CreateSpecificCulture(userLanguage);
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Private method which calculate the value corresponding in the Fibonacci sequence
        /// </summary>
        /// <param name="baseNumber">Number for which the calcul have to be done</param>
        /// <param name="currentCulture">Culture info corresponding to the consumer language. Used to localized all messages</param>
        /// <returns>
        /// the Fibonacci sequence Number
        /// Throw an exception if the baseNumber is not valid
        /// </returns>
        private int FibonacciNumber(int baseNumber, CultureInfo currentCulture)
        {
            try
            {
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_IncomingCall").ToString(), "Private Fibonacci Calcul", baseNumber);
                #region Check Functionnal rules
                //retrieve the min value for the vbase Number configured in the web.Config
                string sMinBaseNumber = ConfigurationManager.AppSettings["FibonacciMinBaseNumber"];
                int iMinBaseNumber = 1;
                if (!int.TryParse(sMinBaseNumber, out iMinBaseNumber))
                {
                    iMinBaseNumber = 1;
                    log.WarnFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_FinobacciMinValueConfigurationWarning").ToString(), sMinBaseNumber);
                }
                //throw Service Exception if minimal value is not respected
                if (baseNumber < iMinBaseNumber)
                {
                    string errorMessage =
                        string.Format(HttpContext.GetGlobalResourceObject("Resource", "Fibonacci_BaseNumberTooSmall", currentCulture).ToString(),
                            baseNumber, iMinBaseNumber);
                    //Inscription dans les logs pour assurer le suivi
                    log.Warn(errorMessage);
                    throw new ServiceException(ServiceException.Level.Warning, errorMessage);
                }
                //retrieve the max value for the vbase Number configured in the web.Config
                string sMaxBaseNumber = ConfigurationManager.AppSettings["FibonacciMaxBaseNumber"];
                int iMaxBaseNumber = 1;
                if (!int.TryParse(sMaxBaseNumber, out iMaxBaseNumber))
                {
                    iMaxBaseNumber = 100;
                    log.WarnFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_FinobacciMaxValueConfigurationWarning").ToString(), sMaxBaseNumber);
                    //log warning configuration appsettings
                }
                //throw Service Exception if maximum value is not respected
                if (baseNumber > iMaxBaseNumber)
                {
                    //Get localized error message
                    string errorMessage = string.Format(HttpContext.GetGlobalResourceObject("Resource", "Fibonacci_BaseNumberTooHigh", currentCulture).ToString(),
                            baseNumber, iMaxBaseNumber);
                    log.Warn(errorMessage);
                    throw new ServiceException(ServiceException.Level.Warning, errorMessage);
                }
                // As the minimal number is now configurable in appsettings section, we have to check if the baseNumber is greater than the first integer in the Fibonacci Sequence
                if (baseNumber < 0)
                {
                    //get localized error message
                    string errorMessage = string.Format(HttpContext.GetGlobalResourceObject("Resource", "Fibonacci_InvalidBaseNumber", currentCulture).ToString(),
                            baseNumber);
                    log.Warn(errorMessage);
                    throw new ServiceException(ServiceException.Level.Warning, errorMessage);
                }
                #endregion
                //The calcul is iterate from the lowest number to the highest. 
                #region Calcul Fibonacci Sequence Number
                // Normally 0 souldn't be taken into account
                // As the minimum value is configurable in the appSettings section, the corresponding Fibonacci Sequence Number is determined
                if (2 > baseNumber)
                    return baseNumber;
                //fibonacciNumber : Number in the fibonacci Sequence corresponding to the current interatif integer
                //f1 : Used to save the previous fibonacci number
                //f2 : Used to save the n-2 fibonacci number
                //i : The current number for which we determine the Fibonacci Sequence Number
                int fibonacciNumber = 0, f1 = 1, f2, i = 1;
                // While the current counter is not greater than the base Number sent to the webMethod
                while (i <= baseNumber)
                {
                    //Save the n-2 Fibonacci Sequence Number
                    f2 = f1;
                    //Save the last Fibonacci Sequence Number (n-1)
                    f1 = fibonacciNumber;
                    //calcultate the new Fibonacci Sequence Number : n-1 + n-2
                    fibonacciNumber = fibonacciNumber + f2;
                    ++i;
                }
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod Fibonacci", fibonacciNumber);
                //retuen the calculated  Fibonacci Sequence Number
                return fibonacciNumber;
                #endregion
            }
            catch (ServiceException serviceException)
            {
                throw serviceException;
            }
            //Catch every unexpected exception
            catch (Exception exc)
            {
                //When logging unexpected error, three entry log are made to preserve the log lisibility
                log.ErrorFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_Exception").ToString(), "Private Fibonacci Calcul");
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
                
                //log as execution error. The only Exception raised by this method must be a ServiceException 
                throw new ServiceException(ServiceException.Level.Error,
                    HttpContext.GetGlobalResourceObject("Resource", "Fibonacci_UnexpectedError", currentCulture).ToString());
            }
        }

        /// <summary>
        /// Convert a xml string in json format
        /// </summary>
        /// <param name="xml">Xml string to format</param>
        /// <param name="currentCulture">Culture info corresponding to the consumer language. Used to localized all messages</param>
        /// <returns></returns>
        private string XmlToJson(string xml, CultureInfo currentCulture)
        {
            try
            {
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_IncomingCall").ToString(), "Private XmlToJson conversion", xml);
                #region Validate request format
                XDocument doc;
                try
                {
                    //try to load the xml content
                    doc = XDocument.Parse(xml);
                }
                //A XmlException is raised if the load method cannot crate load and Parse the string
                catch (XmlException)
                {
                    //get error message
                    string errorMessage = string.Format(HttpContext.GetGlobalResourceObject("Resource", "XmlToJson_BadFormat", currentCulture).ToString(),
                            xml);
                    log.Info(errorMessage);
                    //Raise Service Exception
                    throw new ServiceException(ServiceException.Level.Warning, errorMessage);
                }
                #endregion

                #region Parse to Json
                string sJsonResult = JsonConvert.SerializeXNode(doc, Formatting.Indented, false);
                log.InfoFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_EnddingCall").ToString(), "WebMethod Fibonacci", sJsonResult);
                return sJsonResult;

                #endregion
            }
            catch (ServiceException serviceException)
            {
                throw serviceException;
            }
            //Catch all unexpected error. The only Exception raised by this method must be a ServiceException 
            catch (Exception exc )
            {
                //When logging unexpected error, three entry log are made to preserve the log lisibility
                log.ErrorFormat(HttpContext.GetGlobalResourceObject("Resource", "ServiceMethods_Exception").ToString(), "Private Fibonacci Calcul");
                log.Error(exc.Message);
                log.Error(exc.StackTrace);
                //log as execution error
                throw new ServiceException(ServiceException.Level.Error,
                    HttpContext.GetGlobalResourceObject("Resource", "XmlToJson_UnexpectedError", currentCulture).ToString());
            }
        }

        #endregion
    }
}
