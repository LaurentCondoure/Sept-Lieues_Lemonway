using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonWay.TechnicalTest.Utilities.Data
{
    /// <summary>
    /// Structure used to return full qualified answer from Fibonacci method. 
    /// This allow consumers to know why they receive a bad answer from the service
    /// </summary>
    public struct FibonacciResult
    {
        /// <summary>
        /// the Fibonacci Sequece Number corresponding to the BaseNumber paraleter
        /// </summary>
        public int Result;
        /// <summary>
        /// ErrorCode based on the ServiceException Level
        /// -1 : Functionnal error, 0 : OK, 1 execution error
        /// </summary>
        public int ErrorCode;
        /// <summary>
        /// Displayable error message for consumer
        /// </summary>
        public string ErrorMessage;
    }

    /// <summary>
    /// Struct used to return full qualified answer from XmlToJson method. 
    /// This allow consumers to know why they receive a bad answer from the service
    /// </summary>
    public struct XmlToJsonResut
    {
        /// <summary>
        /// Json converted content from xml passed in parameter 
        /// </summary>
        public string Result;
        /// <summary>
        /// ErrorCode based on the ServiceException Level
        /// -1 : Functionnal error, 0 : OK, 1 execution error
        /// </summary>
        public int ErrorCode;
        /// <summary>
        /// Displayable error message for consumer
        /// </summary>
        public string ErrorMessage;
    }

}