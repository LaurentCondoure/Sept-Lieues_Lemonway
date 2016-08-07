using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;


namespace LemonWay.TechnicalTest.Utilities.Exception
{
    /// <summary>
    /// Custom Exception used to send to consumers the original error which cause the result
    /// </summary>
    public class ServiceException : System.Exception
    {
        /// <summary>
        /// Possible error level for thrown exception
        /// </summary>
        public enum Level
        {
            //Functionnal Error
            Warning = -1,
            //Unexpected error
            Error = 1
        }

        #region properties
        /// <summary>
        /// Represent the error level of the current Exception
        /// </summary>
        private Level _exceptionLevel;
        /// <summary>
        /// Accessor for _ExceptionLevel
        /// The value of this _ExceptionLevel is define in the constructor and should not be modified, so property is read only
        /// </summary>
        public Level ExceptionLevel
        {
            get { return _exceptionLevel;}
        }

        /// <summary>
        /// Error message of the current exception
        /// </summary>
        private string _message;

        /// <summary>
        /// Accessore for _Message. This property is read only
        /// </summary>
        public override string Message
        {
            get { return _message;}
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for ServiceException
        /// </summary>
        /// <param name="function">Function where exception is throw</param>
        /// <param name="exceptionLevel">level of the thrown exception</param>
        public ServiceException(Level exceptionLevel, string message)
        {
            _exceptionLevel = exceptionLevel;
            _message = message;
        }

        #endregion

        #region Methods

        #endregion
    }
}