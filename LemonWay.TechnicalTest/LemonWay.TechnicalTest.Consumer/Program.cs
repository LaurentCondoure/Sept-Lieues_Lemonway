using LemonWay.TechnicalTest.Consumer.LemonWayService;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace LemonWay.TechnicalTest.Consumer
{
    class Program
    {
        private const string fibonacciMethod = "fibonacci";
        private const string fibonacciWithErrorMethod = "fibonacciwitherror";
        private const string xmltojsonMethod = "xmltojson";
        private const string xmlToJsonWithErrorMethod = "xmltojsonwitherror";


        static void Main(string[] args)
        {

            //Get current Culture for message localization
            CultureInfo culture = Thread.CurrentThread.CurrentCulture ?? new CultureInfo("en-US");
            //Get ressources
            ResourceManager resourceManager = new
            ResourceManager("LemonWay.TechnicalTest.Consumer.Resource",
            Assembly.GetExecutingAssembly());
            //Call To Finobacci
            CallFibonacci(10, resourceManager, culture);
            //Call To FinobacciWithError
            CallFibonacciWithError(10, resourceManager, culture);
            //Call To XmlToJson
            CallXmlToJsonMethod("< foo > bar </ foo >", resourceManager, culture);
            //Call To XmlToJsonWithError
            CallXmlToJsonMethodWithError("< foo > bar </ foo >", resourceManager, culture);

            Console.WriteLine(resourceManager.GetString("ServiceMethods_OwnCall", culture));

            //Allow user to make his own test
            string userEntry = string.Empty;
            while (!string.IsNullOrEmpty(userEntry = Console.ReadLine()))
            {
                //Get commandline from user entry
                string[] parameters = userEntry.Split(' ');
                if (parameters.Length != 2)
                {
                    //If the command line is not valid ask again to the user
                    Console.WriteLine(resourceManager.GetString("ServiceMethods_WrongParameterNumber", culture));
                    continue;
                }
                switch (parameters[0].ToLower())
                {
                    case fibonacciMethod:
                        int baseNumber = 0;
                        if (int.TryParse(parameters[1], out baseNumber))
                            CallFibonacci(baseNumber, resourceManager, culture);
                        else
                        {
                            Console.WriteLine(resourceManager.GetString("Finobacci_WrongparameterType", culture));
                        }
                        break;
                    case fibonacciWithErrorMethod:
                        int baseNumberWithError = 0;
                        if (int.TryParse(parameters[1], out baseNumberWithError))
                            CallFibonacciWithError(baseNumberWithError, resourceManager, culture);
                        else
                        {
                            Console.WriteLine(resourceManager.GetString("Finobacci_WrongparameterType", culture));
                        }
                        break;
                    case xmltojsonMethod:
                        CallXmlToJsonMethod(parameters[1], resourceManager, culture);
                        break;
                    case xmlToJsonWithErrorMethod:
                        CallXmlToJsonMethodWithError(parameters[1], resourceManager, culture);
                        break;
                    default:
                        //If the method name is not valid, ask again to the user
                        Console.WriteLine(resourceManager.GetString("ServiceMethods_WrongParameterNumber", culture));
                        continue;
                }

            }
        }

        /// <summary>
        /// Private method to call Fibonacci service method
        /// </summary>
        /// <param name="value">base number for which we want to determine the corresponding Fibonacci sequece number</param>
        /// <param name="resourceManager">ResourceManager to display user messages in the console</param>
        /// <param name="culture">culture used to localize user messages</param>
        private static void CallFibonacci(int value, ResourceManager resourceManager, CultureInfo culture)
        {
            try
            {
                //Creatre webservice communication channel.
                //The webservice is reference through a service reference URL (http://localhost:8080/LemonWayService.asmx), not through project reference
                LemonWayServiceSoapClient lemonWeyService = new LemonWayServiceSoapClient("LemonWayServiceSoap");

                Console.WriteLine(string.Format(resourceManager.GetString("Finobacci_CallMethod", culture), value));
                Console.WriteLine(lemonWeyService.Fibonacci(value));
                Console.WriteLine();

                //close the service channel
                lemonWeyService.Close();
            }
            catch (Exception)
            {
                Console.WriteLine(resourceManager.GetString("ServiceMethods_UnexpectedError", culture));
            }

        }

        /// <summary>
        /// Private method to call FibonacciWithError service method
        /// </summary>
        /// <param name="value">base number for which we want to determine the corresponding Fibonacci sequece number</param>
        /// <param name="resourceManager">ResourceManager to display user messages in the console</param>
        /// <param name="culture">culture used to localize user messages</param>
        private static void CallFibonacciWithError(int value, ResourceManager resourceManager, CultureInfo culture)
        {
            try
            {
                //Creatre webservice communication channel.
                //The webservice is reference through a service reference URL (http://localhost:8080/LemonWayService.asmx), not through project reference
                LemonWayServiceSoapClient lemonWeyService = new LemonWayServiceSoapClient("LemonWayServiceSoap");

                Console.WriteLine(string.Format(resourceManager.GetString("FinobacciWithError_CallMethod", culture), value));
                FibonacciResult result = lemonWeyService.FibonacciWithError(value);
                Console.WriteLine(string.Format(resourceManager.GetString("MethodResult", culture), result.Result));
                Console.WriteLine(string.Format(resourceManager.GetString("MethodErrorCode", culture), result.ErrorCode));
                if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                    Console.WriteLine(string.Format(resourceManager.GetString("MethodErrorMessage", culture), result.ErrorMessage));

                Console.WriteLine();
                //close the service channel
                lemonWeyService.Close();
            }
            catch (Exception)
            {
                Console.WriteLine(resourceManager.GetString("ServiceMethods_UnexpectedError", culture));
            }
        }

        /// <summary>
        /// Private method to call XmlToJson service method
        /// </summary>
        /// <param name="xml">xml to convert to json</param>
        /// <param name="resourceManager">ResourceManager to display user messages in the console</param>
        /// <param name="culture">culture used to localize user messages</param>
        private static void CallXmlToJsonMethod(string xml, ResourceManager resourceManager, CultureInfo culture)
        {
            try
            {
                //Creatre webservice communication channel.
                //The webservice is reference through a service reference URL (http://localhost:8080/LemonWayService.asmx), not through project reference
                LemonWayServiceSoapClient lemonWeyService = new LemonWayServiceSoapClient("LemonWayServiceSoap");

                Console.WriteLine(string.Format(resourceManager.GetString("XmlToJson_CallMethod", culture), xml));
                Console.WriteLine(lemonWeyService.XmlToJson(xml));
                Console.WriteLine();
                //close the service channel
                lemonWeyService.Close();
            }
            catch (Exception)
            {
                Console.WriteLine(resourceManager.GetString("ServiceMethods_UnexpectedError", culture));
            }
        }

        /// <summary>
        /// Private method to call XmlToJsonWithError service method
        /// </summary>
        /// <param name="xml">xml to convert to json</param>
        /// <param name="resourceManager">ResourceManager to display user messages in the console</param>
        /// <param name="culture">culture used to localize user messages</param>
        private static void CallXmlToJsonMethodWithError(string xml, ResourceManager resourceManager, CultureInfo culture)
        {
            try
            {
                //Creatre webservice communication channel.
                //The webservice is reference through a service reference URL (http://localhost:8080/LemonWayService.asmx), not through project reference
                LemonWayServiceSoapClient lemonWeyService = new LemonWayServiceSoapClient("LemonWayServiceSoap");

                Console.WriteLine(string.Format(resourceManager.GetString("XmlToJsonWithError_CallMethod", culture), xml));
                XmlToJsonResut result = lemonWeyService.XmlToJsonWithError(xml);
                Console.WriteLine(string.Format(resourceManager.GetString("MethodResult", culture), result.Result));
                Console.WriteLine(string.Format(resourceManager.GetString("MethodErrorCode", culture), result.ErrorCode));
                if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                    Console.WriteLine(string.Format(resourceManager.GetString("MethodErrorMessage", culture), result.ErrorMessage));

                Console.WriteLine();
                //close the service channel
                lemonWeyService.Close();
            }
            catch (Exception)
            {
                Console.WriteLine(resourceManager.GetString("ServiceMethods_UnexpectedError", culture));
            }
        }
    }
}
